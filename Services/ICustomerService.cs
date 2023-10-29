using CustomersApp.DTO;
using CustomersApp.Models;

namespace CustomersApp.Services
{
	public interface ICustomerService
	{
		IList<Customer> GetAllCustomers();
		Customer? GetCustomerById(int id);
		Customer? InsertCustomer(CustomerInsertDTO dto);
		Customer? UpdateCustomer(CustomerUpdateDTO dto);
		Customer? DeleteCustomer(int id);
	}
}
