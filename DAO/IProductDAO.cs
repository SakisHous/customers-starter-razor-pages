using CustomersApp.Models;

namespace CustomersApp.DAO
{
	public interface IProductDAO
	{	
		IList<Product> GetAll();	
		Product? GetById(int id);
		Product? Insert(Product product);
		Product? Update(Product product);
		void Delete(int id);

	}
}
