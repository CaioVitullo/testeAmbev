using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Sales.CreateSale
{
    /// <summary>
    /// Validator for CreateSaleCommand that defines validation rules for sale creation command.
    /// </summary>
    public class CreateSaleCommandValidator : AbstractValidator<CreateSaleCommand>
    {
        /// <summary>
        /// Initializes a new instance of the CreateSaleCommandValidator with defined validation rules.
        /// </summary>
        public CreateSaleCommandValidator()
        {
            RuleFor(sale => sale.SaleNumber)
                .GreaterThan(0).WithMessage("Sale number must be greater than zero.");

            RuleFor(sale => sale.SaleDate)
                .NotEmpty().WithMessage("Sale date is required.");

            RuleFor(sale => sale.Customer)
                .NotEmpty().WithMessage("Customer is required.");

            RuleFor(sale => sale.TotalSaleAmount)
                .GreaterThan(0).WithMessage("Total sale amount must be greater than zero.");

            RuleFor(sale => sale.Branch)
                .NotEmpty().WithMessage("Branch is required.");

            RuleFor(sale => sale.Items)
                .NotEmpty().WithMessage("At least one item is required.")
                .Must(items => items.All(item => item.Quantity > 0 && item.UnitPrice > 0))
                .WithMessage("Each item must have a positive quantity and unit price.");
        }
    }
}