using BackTest.Application.Features.Orders.Queries.vms;
using BackTest.Application.Persistence;
using BackTest.Domain;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BackTest.Application.Features.Orders.Queries.GetOrdersList
{
    public class GetOrderListQueryHandler : IRequestHandler<GetOrderListQuery, IReadOnlyList<GetOrderResponse>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetOrderListQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<IReadOnlyList<GetOrderResponse>> Handle(GetOrderListQuery request, CancellationToken cancellationToken)
        {
            var orders = await _unitOfWork.Repository<Order>().GetAsync(
                                            predicate: null,
                                            orderBy: null,
                                            includeString: "Customer",
                                            disableTracking: true
                                       );

            if (orders.Count() ==0)
            {
                throw new Exception("No existen ordenes registradas");
            }
            var orderResponses = new List<GetOrderResponse>();

            foreach (var order in orders)
            {
                var orderItems = await _unitOfWork.Repository<OrderItem>().GetAsync(x => x.OrderId == order.Id, 
                    includes: new List<Expression<Func<OrderItem, 
                    object>>> { oi => oi.Product! });

                var orderResponse = new GetOrderResponse
                {
                    Id = order.Id,
                    Codigo = order.Codigo!,
                    Total = order.Total,
                    NombreCustomer = order.Customer!.Nombre!,
                    Items = orderItems.Select(oi => new GetOrderItemResponse
                    {
                        Precio = oi.Precio,
                        Cantidad = oi.Cantidad,
                        NombreProducto = oi.Product!.Nombre!
                    }).ToList()
                };

                orderResponses.Add(orderResponse);
            }

            return orderResponses;
        }
    }
}
