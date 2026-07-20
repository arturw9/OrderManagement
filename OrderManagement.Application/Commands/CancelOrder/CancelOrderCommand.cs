using MediatR;

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