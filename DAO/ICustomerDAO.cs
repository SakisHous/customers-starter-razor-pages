using CustomersApp.Models;

namespace CustomersApp.DAO
{
	public interface ICustomerDAO
	{
		Customer? Insert(Customer customer);
		Customer? Update(Customer? customer);
		void Delete(int id);
		Customer? GetById(int id);
		IList<Customer> GetAll();
	}
}
