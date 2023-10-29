using CustomersApp.Models;
using CustomersApp.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CustomersApp.Pages.Customers
{
    public class DeleteModel : PageModel
    {
        public List<Error> ErrorsArray { get; set; } = new();
        private readonly ICustomerService _customerService;

		public DeleteModel(ICustomerService customerService)
		{
			_customerService = customerService;
		}

		public void OnGet(int id)
        {
            try
            {
                Customer? customer = _customerService.DeleteCustomer(id);
                Response.Redirect("/Customers/Getall");
            } catch (Exception e)
            {
                ErrorsArray.Add(new Error("", e.Message, ""));
            }
        }
    }
}
