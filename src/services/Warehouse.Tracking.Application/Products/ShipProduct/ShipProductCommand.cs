using FluentResults;
using FluentValidation;
using MediatR;

namespace Warehouse.Tracking.Application.Products.ShipProduct;
public record ShipProductCommand(
    Guid ProductId,
    int ProductQuantity,
    Guid DestinationId) : IRequest<Result>;

public class ShipProductCommandValidator : AbstractValidator<ShipProductCommand>
{
    public ShipProductCommandValidator()
    {
        RuleFor(x => x.ProductId).NotEmpty();
        RuleFor(x => x.ProductQuantity).GreaterThan(0);
        RuleFor(x => x.DestinationId)
            .Must(x => x != Guid.Empty)
            .NotEmpty();
    }
}
