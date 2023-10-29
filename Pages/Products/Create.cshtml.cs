using CustomersApp.DTO;
using CustomersApp.Models;
using CustomersApp.Services;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CustomersApp.Pages.Products
{
    public class CreateModel : PageModel
    {
		public List<Error> ErrorsArray { get; set; } = new();
        public ProductInsertDTO ProductInsertDto { get; set; } = new();
        private readonly IProductService _productService;
        private readonly IValidator<ProductInsertDTO> _productInsertValidator;

		public CreateModel(IProductService productService, IValidator<ProductInsertDTO> validator)
		{
			_productService = productService;
			_productInsertValidator = validator;
		}

		public void OnGet()
        {
			ViewData["title"] = "Insert Product";
		}

		public void OnPost(ProductInsertDTO dto)
		{
			ProductInsertDto = dto;
			var validationResult = _productInsertValidator.Validate(dto);

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
				Product product = _productService.InsertProduct(dto)!;
				Response.Redirect("/Products/Getall");
			}
			catch (Exception e)
			{
				ErrorsArray.Add(new Error("", e.Message, ""));
			}
		}
    }
}
