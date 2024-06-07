using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackTest.Application.Features.Orders.Command.vms
{
    public class RegisterOrderResponse
    {
        public string? Codigo { get; set; }
        public decimal? Total { get; set; }
    }
}
