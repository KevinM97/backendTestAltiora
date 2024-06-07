using BackTest.Application.Features.Orders.Command.vms;
using BackTest.Application.Persistence;
using BackTest.Domain;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackTest.Application.Features.Orders.Command.RegisterOrder
{
    public class RegisterOrderCommandHandler : IRequestHandler<RegisterOrderCommand, RegisterOrderResponse>
    {
        private readonly IUnitOfWork _unitOfWork;

        public RegisterOrderCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<RegisterOrderResponse> Handle(RegisterOrderCommand request, CancellationToken cancellationToken)
        {
            var items = new List<OrderItem>();

            foreach (var itemCommand in request.Items)
            {
                var product = await _unitOfWork.Repository<Product>().GetByIdAsync(itemCommand.ProductId);
                if (product == null)
                {
                    throw new ArgumentException($"Producto: {itemCommand.ProductId} no encontrado.");
                }

                var orderItem = new OrderItem
                {
                    ProductId = itemCommand.ProductId,
                    Precio = product.Precio,
                    Cantidad = itemCommand.Cantidad,
                    OrderId = 0
                };

                items.Add(orderItem);
            }

            var order = new Order
            {
                Codigo = GenerateOrderCode(),
                Total = items.Sum(item => item.Precio * item.Cantidad), // Total sumando los precios de los items
                CustomerId = request.CustomerId,
                Fecha = DateTime.Now,
                OrderItems = items
            };

            await _unitOfWork.Repository<Order>().AddAsync(order);


            foreach (var item in items)
            {
                item.OrderId = order.Id;
            }

            // Actualiza los OrderItems en la base de datos después de asignar el Id de la orden
            await _unitOfWork.Complete();

            var response = new RegisterOrderResponse
            {
                Codigo = order.Codigo, Total = order.Total,
            };

            return response;
        }
        private string GenerateOrderCode()
        {
            return "OC-" + (_unitOfWork.Repository<Order>().GetAllAsync().Result.Count() + 1).ToString("D5");
        }
    }
}
