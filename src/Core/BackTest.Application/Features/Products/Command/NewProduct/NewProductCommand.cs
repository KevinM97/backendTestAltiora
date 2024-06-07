using BackTest.Application.Features.Products.Command.vms;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackTest.Application.Features.Products.Command.NewProduct
{
    public class NewProductCommand : IRequest<NewProductResponse>
    {
        [Required(ErrorMessage = "El nombre del producto es requerido")]
        public string? Nombre { get; set; }
        [Range(0, double.MaxValue, ErrorMessage = "El precio debe ser un valor positivo")]
        public decimal Precio { get; set; }

    }
}
