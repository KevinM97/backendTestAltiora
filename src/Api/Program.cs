using BackTest.Api.Middleware;
using BackTest.Application.Features.Products.Queries.GetProductList;
using BackTest.Domain;
using BackTest.Infrastructure;
using BackTest.Infrastructure.Persistence;
using MediatR;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);
//Referencia de BackTest.Infrastructure
builder.Services.AddInfrastructureServices(builder.Configuration);





builder.Services.AddDbContext<AltioraDbContext>(options => 
    options.UseSqlServer(builder.Configuration.GetConnectionString("ConnectionStrings"))
);

// Add services to the container.
builder.Services.AddMediatR(typeof(GetProductListQueryHandler).Assembly);

builder.Services.AddControllers(opt =>
{
    //Todo metodo del controlador nos pedirá authorizacion
    var policy = new AuthorizationPolicyBuilder().RequireAuthenticatedUser().Build();
    opt.Filters.Add(new AuthorizeFilter(policy));
}).AddJsonOptions(x =>
                  x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);


IdentityBuilder identityBuilder = builder.Services.AddIdentityCore<User>();
identityBuilder = new IdentityBuilder(identityBuilder.UserType, identityBuilder.Services);

identityBuilder.AddRoles<IdentityRole>().AddDefaultTokenProviders();
identityBuilder.AddClaimsPrincipalFactory<UserClaimsPrincipalFactory<User, IdentityRole>>();

identityBuilder.AddEntityFrameworkStores<AltioraDbContext>();
identityBuilder.AddSignInManager<SignInManager<User>>();

builder.Services.TryAddSingleton<ISystemClock, SystemClock>();

//config del token
var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JwtSettings:Key"]!));
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = key,
            ValidateAudience = false,
            ValidateIssuer = false,
        };
    });

builder.Services.AddCors(options =>
{
    options.AddPolicy("CorsPolicy", builder => builder.AllowAnyOrigin()
    .AllowAnyMethod()
    .AllowAnyHeader());

});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//app.UseHttpsRedirection();
app.UseMiddleware<ExceptionMiddleware>();
app.UseAuthentication();
app.UseAuthorization();
app.UseCors("CorsPolicy");

app.MapControllers();

using(var scope = app.Services.CreateScope())
{
    var service = scope.ServiceProvider;
    var loggerFactory = service.GetRequiredService<ILoggerFactory>();

    try
    {
        var context = service.GetRequiredService<AltioraDbContext>();
        var usuarioManager = service.GetRequiredService<UserManager<User>>();
        var roleManager = service.GetRequiredService<RoleManager<IdentityRole>>();

        await context.Database.MigrateAsync();
        await AltioraDbContextData.LoadDataAsync(context, usuarioManager, roleManager,loggerFactory);
    }
    catch (Exception ex)
    {
        var logger = loggerFactory.CreateLogger<Program>();
        logger.LogError(ex, "Error en la migracion");
    }
}

app.Run();
