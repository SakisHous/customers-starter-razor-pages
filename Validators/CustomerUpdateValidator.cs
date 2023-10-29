using CustomersApp.DTO;
using FluentValidation;

namespace CustomersApp.Validators
{
	/// <summary>
	/// Validation class for validating <see cref="CustomerUpdateDTO"/> objects.
	/// </summary>
	public class CustomerUpdateValidator : AbstractValidator<CustomerUpdateDTO>
	{
        public CustomerUpdateValidator()
        {
			RuleFor(s => s.Password)
				.NotEmpty().WithMessage("Password is required")
				.Length(3, 255).WithMessage("Password must contains 3 - 255 characters");

			RuleFor(s => s.Email)
				.NotEmpty().WithMessage("Email is required")
				.Length(3, 255).WithMessage("Email must contains 5 - 255 characters");
		}
    }
}
