using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackTest.Application.Features.Products.Queries.vms
{
    public class ProductResponse
    {
        public int Id { get; set; }
        public string? Codigo { get; set; }
        public string? Nombre { get; set; }
        public decimal Precio { get; set; }
    }
}
