using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackTest.Application.Features.Products.Command.EditProduct
{
    public class EditProductCommand : IRequest<bool>
    {
        public int ProductId { get; set; }
        public string Nombre { get; set; }
        public decimal Precio { get; set; }
        public string Codigo { get; set; }
    }
}
