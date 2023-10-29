using AutoMapper;
using CustomersApp.DTO;
using CustomersApp.Models;
using CustomersApp.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CustomersApp.Pages.Customers
{
    public class IndexModel : PageModel
    {
        public Error? ErrorObj { get; set; }
        public IList<CustomerReadOnlyDTO> CustomersDto { get; set; } = null!;

        private readonly ICustomerService? _customerService;
        private readonly IMapper? _mapper;

		public IndexModel(ICustomerService? customerService, IMapper? mapper)
		{
			_customerService = customerService;
			_mapper = mapper;
		}

		public void OnGet()
        {
            ViewData["title"] = "All Customers";
            try
            {
                ErrorObj = null;
                IList<Customer> customers = _customerService!.GetAllCustomers();
                CustomersDto = new List<CustomerReadOnlyDTO>();

                foreach (Customer customer in customers)
                {
                    CustomerReadOnlyDTO? customerDto = _mapper!.Map<CustomerReadOnlyDTO>(customer);
                    CustomersDto.Add(customerDto);
                }
                
            } catch (Exception e)
            {
                ErrorObj = new Error("", e.Message, "");
            }
        }
    }
}
