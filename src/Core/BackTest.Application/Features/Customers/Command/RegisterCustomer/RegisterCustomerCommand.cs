using BackTest.Application.Features.Customers.Command.vms;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackTest.Application.Features.Customers.Command.RegisterCustomer
{
    public class RegisterCustomerCommand : IRequest<RegisterCustomerResponse>
    {
        [Required(ErrorMessage = "El nombre es requerido")]
        [RegularExpression(@"^[A-Za-záéíóúñÁÉÍÓÚÑ\s]+$", ErrorMessage = "El nombre solo puede contener letras y espacios")]
        public string Nombre { get; set; }
        [Required(ErrorMessage = "El apellido es requerido")]
        [RegularExpression(@"^[A-Za-záéíóúñÁÉÍÓÚÑ\s]+$", ErrorMessage = "El apellido solo puede contener letras y espacios")]
        public string Apellido { get; set; }
    }
}
