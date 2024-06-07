using BackTest.Application.Features.Customers.Command.vms;
using BackTest.Application.Persistence;
using BackTest.Domain;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackTest.Application.Features.Customers.Command.RegisterCustomer
{
    public class RegisterCustomerCommandHandler : IRequestHandler<RegisterCustomerCommand, RegisterCustomerResponse>
    {
        private readonly IUnitOfWork _unitOfWork;

        public RegisterCustomerCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<RegisterCustomerResponse> Handle(RegisterCustomerCommand request, CancellationToken cancellationToken)
        {
            var registered = await _unitOfWork.Repository<Customer>().GetAsync(x=> x.Nombre == request.Nombre && x.Apellido == request.Apellido);
            if (registered.Count() != 0)
            {
                throw new Exception($"El cliente: {request.Nombre + request.Apellido} ya está registrado en el sistema");
            }

            var newcustomer = new Customer
            {
                Nombre = request.Nombre,
                Apellido = request.Apellido
            };

            _unitOfWork.Repository<Customer>().AddEntity(newcustomer);
            var result = await _unitOfWork.Complete();

            if (result <= 0)
            {
                throw new Exception($"Hubo un problema al registrar el cliente");
            }

            var cliente = new RegisterCustomerResponse
            {
                Id = newcustomer.Id,
                Nombre = newcustomer.Nombre,
                Apellido = newcustomer.Apellido
            };

            return cliente;
        }
    }
}
