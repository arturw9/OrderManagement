using MediatR;

using OrderManagement.Application.Interfaces;

namespace OrderManagement.Application.Commands.CancelOrder;

public class CancelOrderCommandHandler
    : IRequestHandler<CancelOrderCommand, bool>
{

    private readonly IOrderRepository _repository;

    private readonly IUnitOfWork _unitOfWork;



    public CancelOrderCommandHandler(
        IOrderRepository repository,
        IUnitOfWork unitOfWork)
    {
        _repository = repository;

        _unitOfWork = unitOfWork;
    }



    public async Task<bool> Handle(
        CancelOrderCommand request,
        CancellationToken cancellationToken)
    {


        var order = await _repository.GetByIdAsync(
            request.OrderId,
            cancellationToken);



        if (order is null)
            return false;



        order.Cancel();



        await _unitOfWork.SaveChangesAsync(
            cancellationToken);



        return true;

    }
}