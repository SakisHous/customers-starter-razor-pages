using AutoMapper;
using CustomersApp.DAO;
using CustomersApp.DTO;
using CustomersApp.Models;
using System.Transactions;

namespace CustomersApp.Services
{
	public class OrderServiceImpl : IOrderService
	{
		private readonly IOrderDAO _orderDAO;
		private readonly IMapper _mapper;
		private readonly ILogger<OrderServiceImpl> _logger;

		public OrderServiceImpl(IOrderDAO orderDAO, IMapper mapper, ILogger<OrderServiceImpl> logger)
		{
			_orderDAO = orderDAO;
			_mapper = mapper;
			_logger = logger;
		}

		public IList<Order> GetAllOrders()
		{
			try
			{
				IList<Order> orders =  _orderDAO.GetAll();
				return orders;
			} catch (Exception e)
			{
				_logger.LogError("An Error occured when fetching all orders: {ErrorMessage}", e.Message);
				throw;
			}
		}

		public Order? GetOrder(int customerId, int productId)
		{
			try
			{
				return _orderDAO.Get(customerId, productId);
			} catch (Exception e)
			{
				_logger.LogError("An Error occured when fetching an order: {ErrorMessage}", e.Message);
				throw;
			}
		}

		public IList<OrderLine> GetOrderByCustomerId(int customerId)
		{
			try
			{
				IList<OrderLine> orderLines = _orderDAO.GetByCustomerId(customerId);
				return orderLines;
			}
			catch (Exception e)
			{
				_logger.LogError("An Error occured when fetching an order by customer Id: {ErrorMessage}", e.Message);
				throw;
			}
		}

		public IList<Order> GetOrderByProductId(int productId)
		{
			try
			{
				IList<Order> orders = _orderDAO.GetByProductId(productId);
				return orders;
			}
			catch (Exception e)
			{
				_logger.LogError("An Error occured when fetching an order by product Id: {ErrorMessage}", e.Message);
				throw;
			}
		}

		public Order? InsertOrder(OrderInsertDTO dto)
		{
			if (dto == null)
			{
				return null;
			}

			var order = _mapper.Map<Order>(dto);
			try
			{
				using TransactionScope scope = new();
				Order? insertedOrder = _orderDAO.Insert(order);
				scope.Complete();
				return insertedOrder;
			}
			catch (Exception e)
			{
				_logger.LogError("An Error occured while inserting a new order: {ErrorMessage}", e.Message);
				throw;
			}
		}

		public Order? UpdateOrder(OrderUpdateDTO dto)
		{
			if (dto == null)
			{
				return null;
			}

			Order? order = _mapper.Map<Order>(dto);
			Order? updatedOrder = null;

			try
			{
				using TransactionScope scope = new();
				updatedOrder = _orderDAO.Get(order.CustomerID, order.ProductID);

				if (updatedOrder is null)
				{
					return null;
				}

				updatedOrder = _orderDAO.Update(order);
				scope.Complete();
			}
			catch (Exception e)
			{
				_logger.LogError("An Error occured while updating an order: {ErrorMessage}", e.Message);
				throw;
			}

			return updatedOrder;
		}

		public Order? DeleteOrder(int cutomerId, int productId)
		{
			Order? deletedOrder = null;
			try
			{
				using TransactionScope scope = new();
				deletedOrder = _orderDAO.Get(cutomerId, productId);

				if (deletedOrder is null)
				{
					return null;
				}

				_orderDAO.Delete(cutomerId, productId);
				scope.Complete();
			}
			catch (Exception e)
			{
				_logger.LogError("An Error occured while deleting an order: {ErrorMessage}", e.Message);
				throw;
			}
			return deletedOrder;
		}
	}
}
