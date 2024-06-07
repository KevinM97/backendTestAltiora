using BackTest.Application.Features.Orders.Command.vms;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackTest.Application.Features.Orders.Command.RegisterOrder
{
    public class RegisterOrderCommand : IRequest<RegisterOrderResponse>
    {
        public int CustomerId { get; set; }
        public List<CreateOrderItemCommand> Items { get; set; }
    }

    public class CreateOrderItemCommand
    {
        public int ProductId { get; set; }
        public int Cantidad { get; set; }
    }
}
