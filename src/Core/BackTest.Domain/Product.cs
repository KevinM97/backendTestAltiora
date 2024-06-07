using BackTest.Domain.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackTest.Domain
{
    public class Product : BaseDomainModel
    {
        [Column(TypeName = "NVARCHAR(100)")]
        public string? Codigo { get; set; }
        [Column(TypeName = "NVARCHAR(100)")]
        public string? Nombre { get; set; }
        [Column(TypeName = "DECIMAL(10,2)")]
        public decimal Precio { get; set; }

    }
}
