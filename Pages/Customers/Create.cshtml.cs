using CustomersApp.DTO;
using CustomersApp.Models;
using CustomersApp.Services;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CustomersApp.Pages.Customers
{
    public class CreateModel : PageModel
    {
        public List<Error> ErrorsArray { get; set; } = new();
        public CustomerInsertDTO CustomerInsertDto { get; set; } = new();
        private readonly ICustomerService _customerService;
        private readonly IValidator<CustomerInsertDTO> _customerInsertValidator;

		public CreateModel(ICustomerService customerService, IValidator<CustomerInsertDTO> customerInsertValidator)
		{
			_customerService = customerService;
			_customerInsertValidator = customerInsertValidator;
		}

		public void OnGet()
        {
            ViewData["title"] = "Insert Customer";
        }

        public void OnPost(CustomerInsertDTO dto)
        {
			ViewData["title"] = "Insert Customer";
            CustomerInsertDto = dto;

            var validationResult = _customerInsertValidator.Validate(dto);

            if (!validationResult.IsValid)
            {
                foreach (var error in validationResult.Errors)
                {
                    ErrorsArray.Add(new Error(error.ErrorCode, error.ErrorMessage, error.PropertyName));
                }
                return;
            }

            try
            {
                Customer customer = _customerService.InsertCustomer(dto)!;
                Response.Redirect("/Customers/Getall");
            } catch (Exception e)
            {
                ErrorsArray.Add(new Error("", e.Message, ""));
            }
		}
    }
}
