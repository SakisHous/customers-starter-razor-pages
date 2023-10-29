using CustomersApp.DTO;
using FluentValidation;

namespace CustomersApp.Validators
{
	public class ProductInsertValidator : AbstractValidator<ProductInsertDTO>
	{
		public ProductInsertValidator() 
		{

			RuleFor(s => s.Name)
				.NotEmpty().WithMessage("Product Name is required")
				.Length(3, 255).WithMessage("Product Name must contains 3 - 255 characters");
			RuleFor(s => s.Price)
				.NotEmpty().WithMessage("Price is required")
				.Custom((x, context) =>
				{
					if (!(decimal.TryParse(x, out decimal value)) || value <= 0)
					{
						context.AddFailure($"{x} is not a valid Price");
					}
				});

			RuleFor(s => s.Quantity)
				.NotEmpty().WithMessage("Quantity is required")
				.Must(quantity => quantity >= 0).WithMessage("Quantity must be a non-negative number");

		}

	}
}
