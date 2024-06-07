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
    public class OrderItem : BaseDomainModel
    {
        [Column(TypeName = "DECIMAL(10,2)")]
        public decimal Precio { get; set; }
        public int Cantidad { get; set; }
        public Product? Product { get; set; }
        public int ProductId { get; set; }
        public Order? Order { get; set; }
        public int OrderId { get; set; }
    }
}
