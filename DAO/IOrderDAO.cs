using CustomersApp.Models;

namespace CustomersApp.DAO
{
	public interface IOrderDAO
	{
		IList<Order> GetAll();
		IList<OrderLine> GetByCustomerId(int customerId);
		IList<Order> GetByProductId(int productId);
		Order? Get(int customerId, int productId);
		Order? Insert(Order order);
		Order? Update(Order order);
		void Delete(int cutomerId, int productId);
	}
}
