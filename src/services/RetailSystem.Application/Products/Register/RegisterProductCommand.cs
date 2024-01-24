using FluentResults;
using FluentValidation;
using MediatR;

namespace Warehouse.Tracking.Application.Products.Register;

public record RegisterProductCommand(string Name, int Quantity, Guid WarehouseId, Guid? StoreId) : IRequest<Result<Guid>>;

public class RegisterPRoductCommandValidator : AbstractValidator<RegisterProductCommand>
{
    public RegisterPRoductCommandValidator()
    {
        RuleFor(x => x.Name).NotEmpty();
        RuleFor(x => x.Quantity)
            .GreaterThan(0)
            .NotEmpty();
        RuleFor(x => x.WarehouseId).NotEmpty();
    }
}
