using BackTest.Application.Features.Orders.Command.RegisterOrder;
using BackTest.Application.Features.Orders.Queries.GetOrdersList;
using BackTest.Application.Features.Orders.Queries.vms;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace BackTest.Api.Controllers
{
    [ApiController]
    [Route("api/altiora/v1/[controller]")]
    public class OrderController : ControllerBase
    {
        private readonly IMediator _mediator;

        public OrderController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [AllowAnonymous]
        [HttpGet("list", Name = "GetOrderList")]
        [ProducesResponseType(typeof(IReadOnlyList<GetOrderResponse>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IReadOnlyList<GetOrderResponse>>> GetOrderList()
        {
            var query = new GetOrderListQuery();
            var orders = await _mediator.Send(query);
            return Ok(orders);
        }

        [AllowAnonymous]
        [HttpPost("registerOrder", Name = "RegisterOrder")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<ActionResult<int>> RegisterOrder([FromBody] RegisterOrderCommand command)
        {
            var orderId = await _mediator.Send(command);
            return Ok(orderId);
        }

    }
}
