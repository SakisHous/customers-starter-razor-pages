using AutoMapper;
using CustomersApp.DTO;
using CustomersApp.Models;

namespace CustomersApp.Configuration
{
	/// <summary>
	/// This class is used from <see cref="AutoMapper"/> to
	/// calculate the cost of a product in an order.
	/// It takes the quantity and price from the source class
	/// and resolves the value in the cost variable of the 
	/// destination class (performing multiplication).
	/// </summary>
	public class CostResolver : IValueResolver<OrderLine, OrderReadOnlyDTO, decimal>
	{
		public decimal Resolve(OrderLine source, OrderReadOnlyDTO destination, decimal destMember, ResolutionContext context)
		{
			return source.ProductQty * source.Price;
		}
	}
}
