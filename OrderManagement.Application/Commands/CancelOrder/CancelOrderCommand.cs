using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace OrderManagement.Application.Commands.CancelOrder;
public class CancelOrderCommand
    : IRequest<bool>
{

    public Guid OrderId { get; set; }


    public CancelOrderCommand(Guid orderId)
    {
        OrderId = orderId;
    }

}