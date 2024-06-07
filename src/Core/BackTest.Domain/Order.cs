using BackTest.Domain.Common;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackTest.Domain
{
    public class Order : BaseDomainModel
    {
        public Order() { }
        public Order(decimal total) {
            Total = total;
        }

        public string? Codigo { get; set; }
        public DateTime Fecha { get; set; }

        [Column(TypeName = "DECIMAL(10,2)")]
        public decimal Total { get; set; }

        public IReadOnlyList<OrderItem>? OrderItems { get; set; }
        public Customer? Customer { get; set; }
        public int CustomerId { get; set; }
    }
}
