using CustomersApp.DTO;
using CustomersApp.Models;

namespace CustomersApp.Services
{
	public interface IOrderService
	{
		IList<Order> GetAllOrders();
		IList<OrderLine> GetOrderByCustomerId(int customerId);
		IList<Order> GetOrderByProductId(int productId);
		Order? GetOrder(int customerId, int productId);
		Order? InsertOrder(OrderInsertDTO dto);
		Order? UpdateOrder(OrderUpdateDTO dto);
		Order? DeleteOrder(int cutomerId, int productId);
	}
}
