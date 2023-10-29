using CustomersApp.DTO;
using CustomersApp.Models;

namespace CustomersApp.Services
{
	public interface IProductService
	{
		IList<Product> GetAllProducts();
		Product? GetProductById(int id);
		Product? InsertProduct(ProductInsertDTO dto);
		Product? UpdateProduct(ProductUpdateDTO dto);
		Product? DeleteProduct(int id);
	}
}
