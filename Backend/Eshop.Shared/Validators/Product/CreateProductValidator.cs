using Eshop.Shared.Models.Requests.Product;
using FluentValidation;

namespace Eshop.Shared.Validators.Product
{
    public class CreateProductValidator : AbstractValidator<CreateProductRequest>
    {
        /// <summary>Initializes a new instance of the <see cref="AssetListRequestValidator"/> class.</summary>
        public CreateProductValidator()
        {
            RuleFor(p => p.Name).NotNull().NotEmpty().MaximumLength(100);
        }
    }
}
