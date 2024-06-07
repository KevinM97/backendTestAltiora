using BackTest.Application.Models.Authorization;
using BackTest.Domain;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackTest.Infrastructure.Persistence
{
    public class AltioraDbContextData
    {
        public static async Task LoadDataAsync(
            AltioraDbContext context,
            UserManager<User> userManager,
            RoleManager<IdentityRole> roleManager,
            ILoggerFactory loggerFactory
        )
        {
            try
            {
                if(!roleManager.Roles.Any())
                {
                    await roleManager.CreateAsync(new IdentityRole(Role.Admin));
                    await roleManager.CreateAsync(new IdentityRole(Role.Costumer));
                }
                
                if(!userManager.Users.Any())
                {
                    var usuarioAdmin = new User
                    {
                        Nombre = "Kevin",
                        Apellido = "Mina",
                        Email = "altiora@mail.com",
                        UserName = "KM"
                    };
                    await userManager.CreateAsync(usuarioAdmin, "AdminAltiora2024*");
                    await userManager.AddToRoleAsync(usuarioAdmin,Role.Admin);

                }
                if (!context.Products!.Any())
                {
                    var Monitor = new Product
                    {
                        Codigo = "PR-00001",
                        Precio = 100,
                        Nombre = "Monitor",
                  
                    };
                    await context.AddAsync(Monitor);

                    var Teclado = new Product
                    {
                        Codigo = "PR-00002",
                        Precio = 20,
                        Nombre = "Teclado",

                    };
                    await context.AddAsync(Teclado);
                    await context.SaveChangesAsync();
                }

                if (!context.Customers!.Any())
                {
                    var kev = new Customer
                    {
                        Nombre = "Kevin",
                        Apellido = "Heredia"
                    };
                    await context.AddAsync(kev);

                    var her = new Customer
                    {
                        Nombre = "Hernan",
                        Apellido = "Puruncajas"

                    };
                    await context.AddAsync(her);
                    await context.SaveChangesAsync();
                }

                if (!context.Orders!.Any())
                {
                    var uno = new Order
                    {
                        Codigo = "OC-00001",
                        Fecha = DateTime.Now,
                        Total= 50,
                        CustomerId = 1,
                    };
                    await context.AddAsync(uno);

                    var dos = new Order
                    {
                        Codigo = "OC-00002",
                        Fecha = DateTime.Now,
                        Total = 50,
                        CustomerId = 2,
                    };
                    await context.AddAsync(dos);
                    await context.SaveChangesAsync();
                }

            }
            catch (Exception e)
            {
                var logger = loggerFactory.CreateLogger<AltioraDbContext>();
                logger.LogError(e.Message);
            }
        }
    }
}
