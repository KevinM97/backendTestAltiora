using BackTest.Application.Features.Customers.Queries.vms;
using BackTest.Application.Persistence;
using BackTest.Domain;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackTest.Application.Features.Customers.Queries.GetCustomersList
{
    public class GetCustomersListQueryHandler : IRequestHandler<GetCustomersListQuery, IReadOnlyList<GetCustomersResponse>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetCustomersListQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IReadOnlyList<GetCustomersResponse>> Handle(GetCustomersListQuery request, CancellationToken cancellationToken)
        {
            var customers = await _unitOfWork.Repository<Customer>().GetAllAsync();

            var cliente = customers.Select(x => new GetCustomersResponse
            {
                Id = x.Id,
                Nombre = x.Nombre,
                Apellido = x.Apellido
            }).ToList();

            return cliente;
        }
    }
}
