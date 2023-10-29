using CustomersApp.Models;
using CustomersApp.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CustomersApp.Pages.Products
{
    public class DeleteModel : PageModel
    {
		public List<Error> ErrorsArray { get; set; } = new();
        private readonly IProductService _productService;

		public DeleteModel(IProductService productService)
		{
			_productService = productService;
		}

		public void OnGet(int id)
        {
			try
			{
				Product? product = _productService.DeleteProduct(id);
				Response.Redirect("/Products/Getall");
			}
			catch (Exception e)
			{
				ErrorsArray.Add(new Error("", e.Message, ""));
			}
		}
    }
}
