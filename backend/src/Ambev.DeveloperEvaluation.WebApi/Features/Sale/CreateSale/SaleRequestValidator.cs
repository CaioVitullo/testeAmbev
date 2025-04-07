using Ambev.DeveloperEvaluation.WebApi.Features.Sale.GetSale;
using FluentValidation;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sale.CreateSale
{
    public class SaleRequestValidator : AbstractValidator<SaleRequest>
    {
        public SaleRequestValidator()
        {
            RuleFor(sale => sale.SaleNumber)
                .GreaterThan(0).WithMessage("Sale number must be greater than zero.");

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

            // Additional rules can be added here as required
        }
    }
}
