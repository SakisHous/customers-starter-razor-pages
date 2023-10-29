using AutoMapper;
using CustomersApp.DTO;
using CustomersApp.Models;
using CustomersApp.Services;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CustomersApp.Pages.Products
{
    public class UpdateModel : PageModel
    {
		public List<Error> ErrorsArray { get; set; } = new();
        public ProductUpdateDTO ProductUpdateDto { get; set; } = new();

        private readonly IProductService _productService;
        private readonly IMapper _mapper;
        private readonly IValidator<ProductUpdateDTO> _productUpdateValidator;

		public UpdateModel(IProductService productService, IMapper mapper, IValidator<ProductUpdateDTO> productUpdateValidator)
		{
			_productService = productService;
			_mapper = mapper;
			_productUpdateValidator = productUpdateValidator;
		}

		public void OnGet(int id)
        {
			try
			{
				Product? product = _productService.GetProductById(id);
				ProductUpdateDto = _mapper.Map<ProductUpdateDTO>(product);
			}
			catch (Exception e)
			{
				ErrorsArray.Add(new Error("", e.Message, ""));
			}
		}

		public void OnPost(ProductUpdateDTO dto)
		{
			ProductUpdateDto = dto;

			var validationResult = _productUpdateValidator.Validate(dto);

			if (!validationResult.IsValid)
			{
				ErrorsArray = new();
				foreach(var error in validationResult.Errors)
				{
					ErrorsArray.Add(new Error(error.ErrorCode, error.ErrorMessage, error.PropertyName));
				}
				return;
			}

			try
			{
				Product? product = _productService.UpdateProduct(dto);
				Response.Redirect("/Products/Getall");
			}
			catch (Exception e)
			{
				ErrorsArray.Add(new Error("Update Error", e.Message, ""));
			}
		}
    }
}
