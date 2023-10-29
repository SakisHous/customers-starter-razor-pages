using AutoMapper;
using CustomersApp.DTO;
using CustomersApp.Models;
using CustomersApp.Services;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CustomersApp.Pages.Customers
{
    public class UpdateModel : PageModel
    {
        public CustomerUpdateDTO CustomerUpdateDto { get; set; } = new();
        public List<Error> ErrorsArray { get; set; } = new();

        private readonly ICustomerService _customerService;
        private readonly IMapper _mapper;
        private readonly IValidator<CustomerUpdateDTO> _customerUpdateValidator;

		public UpdateModel(ICustomerService customerService, IMapper mapper, IValidator<CustomerUpdateDTO> customerUpdateValidator)
		{
			_customerService = customerService;
			_mapper = mapper;
			_customerUpdateValidator = customerUpdateValidator;
		}

		public void OnGet(int id)
        {
            try
            {
                Customer? customer = _customerService.GetCustomerById(id);
                CustomerUpdateDto = _mapper.Map<CustomerUpdateDTO>(customer);
            } catch (Exception e)
            {
                ErrorsArray.Add(new Error("", e.Message, ""));
            }
            
        }

        public void OnPost(CustomerUpdateDTO dto)
        {
            CustomerUpdateDto = dto;

            var validationResult = _customerUpdateValidator.Validate(dto);
            if (!validationResult.IsValid)
            {
                ErrorsArray = new();
                foreach (var error in validationResult.Errors)
                {
                    ErrorsArray.Add(new Error(error.ErrorCode, error.ErrorMessage, error.PropertyName));
                }
                return;
            }

            try
            {
                Customer? customer = _customerService.UpdateCustomer(dto);
                Response.Redirect("/Customers/Getall");
            } catch (Exception e)
            {
                ErrorsArray.Add(new Error("Update Error", e.Message, ""));
            }
        }
    }
}
