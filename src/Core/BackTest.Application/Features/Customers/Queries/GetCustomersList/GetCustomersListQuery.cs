using BackTest.Application.Features.Customers.Queries.vms;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackTest.Application.Features.Customers.Queries.GetCustomersList
{
    public class GetCustomersListQuery : IRequest<IReadOnlyList<GetCustomersResponse>>
    {
    }
}
