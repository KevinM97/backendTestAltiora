using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackTest.Application.Features.Orders.Queries.vms
{
    public class GetOrderItemResponse
    {
        public decimal Precio { get; set; }
        public int Cantidad { get; set; }
        public string NombreProducto { get; set; }
    }
}
