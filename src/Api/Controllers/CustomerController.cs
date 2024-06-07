using BackTest.Application.Features.Customers.Command.RegisterCustomer;
using BackTest.Application.Features.Customers.Command.vms;
using BackTest.Application.Features.Customers.Queries.GetCustomersList;
using BackTest.Application.Features.Products.Command.NewProduct;
using BackTest.Application.Features.Products.Command.vms;
using BackTest.Application.Features.Products.Queries.GetProductList;
using BackTest.Application.Persistence;
using BackTest.Domain;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace BackTest.Api.Controllers
{
    [ApiController]
    [Route("api/altiora/v1/[controller]")]
    public class CustomerController : ControllerBase
    {
        private IMediator _mediator;
        private IUnitOfWork _unitOfWork;

        public CustomerController(IMediator mediator, IUnitOfWork unitOfWork)
        {
            _mediator = mediator;
            _unitOfWork = unitOfWork;
        }

        [AllowAnonymous]
        [HttpGet("list", Name = "GetCustomersList")]
        [ProducesResponseType(typeof(IReadOnlyList<Product>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IReadOnlyList<Product>>> GetCustomersList()
        {
            var query = new GetCustomersListQuery();
            var productos = await _mediator.Send(query);
            return Ok(productos);
        }

        [AllowAnonymous]
        [HttpPost("registerCustomer", Name = "RegisterCustomer")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<ActionResult<RegisterCustomerResponse>> RegisterCustomer([FromForm] RegisterCustomerCommand request)
        {
            return await _mediator.Send(request);
        }
    }
}
