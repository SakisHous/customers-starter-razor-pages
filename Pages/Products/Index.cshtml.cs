using AutoMapper;
using CustomersApp.DTO;
using CustomersApp.Models;
using CustomersApp.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CustomersApp.Pages.Products
{
    public class IndexModel : PageModel
    {
		public Error? ErrorObj { get; set; }
        public IList<ProductReadOnlyDTO> ProductsReadOnlyDto { get; set; } = null!;

        private readonly IProductService? _productService;
		private readonly IMapper? _mapper;

		public IndexModel(IProductService? productService, IMapper? mapper)
		{
			this._productService = productService;
			_mapper = mapper;
		}

		public IActionResult OnGet()
        {
			ViewData["title"] = "All Customers";

			try
			{
				ErrorObj = null;
				IList<Product> products = _productService!.GetAllProducts();
				ProductsReadOnlyDto = new List<ProductReadOnlyDTO>();

				foreach(var product in products)
				{
					ProductReadOnlyDTO? productReadOnlyDto = _mapper!.Map<ProductReadOnlyDTO>(product);
					ProductsReadOnlyDto.Add(productReadOnlyDto);
				}
			}
			catch (Exception e)
			{
				ErrorObj = new Error("", e.Message, "");
			}

			return Page();
		}
    }
}
