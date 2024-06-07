using BackTest.Application.Features.Orders.Queries.vms;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackTest.Application.Features.Orders.Queries.GetOrdersList
{
    public class GetOrderListQuery : IRequest<IReadOnlyList<GetOrderResponse>>
    {
    }
}
