using BackTest.Application.Features.Products.Command.DeleteProduct;
using BackTest.Application.Features.Products.Command.EditProduct;
using BackTest.Application.Features.Products.Command.NewProduct;
using BackTest.Application.Features.Products.Command.vms;
using BackTest.Application.Features.Products.Queries.GetProductList;
using BackTest.Domain;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace BackTest.Api.Controllers
{
    [ApiController]
    [Route("api/altiora/v1/[controller]")]
    public class ProductController : ControllerBase
    {
        private IMediator _mediator;

        public ProductController(IMediator mediator)
        {
            _mediator = mediator;
        }


        [AllowAnonymous]
        [HttpGet("list", Name = "GetProductList")]
        [ProducesResponseType(typeof(IReadOnlyList<Product>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IReadOnlyList<Product>>> GetProductList()
        {
            var query = new GetProductListQuery();
            var productos = await _mediator.Send(query);
            return Ok(productos);
        }

        [AllowAnonymous]
        [HttpPost("registerProduct", Name = "RegisterProduct")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<ActionResult<NewProductResponse>> RegisterProduct([FromForm] NewProductCommand request)
        {
            return await _mediator.Send(request);
        }

        [AllowAnonymous]
        [HttpDelete("{id}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<ActionResult> DeleteProduct(int id)
        {
            var command = new DeleteProductCommand { ProductId = id };
            var result = await _mediator.Send(command);

            if (result)
            {
                return Ok("Producto eliminado correctamente.");
            }
            else
            {
                return NotFound("Producto no encontrado.");
            }
        }

        [AllowAnonymous]
        [HttpPut("{id}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<ActionResult> EditProduct(int id, [FromBody] EditProductCommand command)
        {
            if (id != command.ProductId)
            {
                return BadRequest("El ID del producto en la URL no coincide con el ID del producto en el cuerpo de la solicitud.");
            }

            var result = await _mediator.Send(command);

            if (result)
            {
                return Ok("Producto actualizado correctamente.");
            }
            else
            {
                return NotFound("Producto no encontrado.");
            }
        }
    }
}
