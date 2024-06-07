using BackTest.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackTest.Application.Features.Orders.Queries.vms
{
    public class GetOrderResponse
    {
        public int Id { get; set; }
        public string Codigo { get; set; }
        public decimal Total { get; set; }
        public string NombreCustomer { get; set; }
        public List<GetOrderItemResponse> Items { get; set; }

    }
}
