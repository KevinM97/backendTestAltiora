using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackTest.Application.Features.Products.Command.DeleteProduct
{
    public class DeleteProductCommand : IRequest<bool>
    {
        public int ProductId { get; set; }
    }
}
