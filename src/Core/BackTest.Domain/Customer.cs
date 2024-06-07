using BackTest.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackTest.Domain
{
    public class Customer : BaseDomainModel
    {
        public string? Nombre { get; set; }
        public string? Apellido { get; set; }
        public List<Order>? Order { get; set; }
    }
}
