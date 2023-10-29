using AutoMapper;
using CustomersApp.DAO;
using CustomersApp.DTO;
using CustomersApp.Models;
using System.Transactions;

namespace CustomersApp.Services
{
	/// <summary>
	/// This class provides the Business Logic regarding
	/// <see cref="Customer"/> Entity in the Service Layer.
	/// </summary>
	public class CustomerServiceImpl : ICustomerService
	{
		private readonly ICustomerDAO _customerDAO;
		private readonly IMapper _mapper;
		private readonly ILogger<CustomerServiceImpl> _logger;

		/// <summary>
		/// Dependency Injection container initializes the following classes.
		/// </summary>
		/// <param name="customerDAO">The DAO class for <see cref="Customer"/></param>
		/// <param name="mapper">A mapper class for converting DTOs to Entities and vice versa.</param>
		/// <param name="logger">A logger which logs information and errors.</param>
		public CustomerServiceImpl(ICustomerDAO customerDAO, IMapper mapper, ILogger<CustomerServiceImpl> logger)
		{
			_customerDAO = customerDAO;
			_mapper = mapper;
			_logger = logger;
		}

		/// <summary>
		/// This method provides all the customers in the database.
		/// </summary>
		/// <returns>A <see cref="List{T}"/> of <see cref="Customer"/></returns>
		public IList<Customer> GetAllCustomers()
		{
			try
			{
				IList<Customer> customers = _customerDAO.GetAll();
				return customers;
			} catch(Exception e)
			{
				_logger.LogError("An Error occured when fetching all customers: {ErrorMessage}", e.Message);
				throw;
			}
		}

		/// <summary>
		/// This method provides a customer with a given id from the database,
		/// if exists, otherwise null.
		/// </summary>
		/// <param name="id">The id of the customer.</param>
		/// <returns>A <see cref="Customer"/> object.</returns>
		public Customer? GetCustomerById(int id)
		{
			try
			{
				return _customerDAO.GetById(id);
			}
			catch (Exception e)
			{
				_logger.LogError("An Error occured when fetching a customer by id: {ErrorMessage}", e.Message);
				throw;
			}
		}

		/// <summary>
		/// This provides the operation for inserting a
		/// new customer in the database.
		/// </summary>
		/// <param name="dto">A <see cref="CustomerInsertDTO"/> object.</param>
		/// <returns>The inserted <see cref="Customer"/>.</returns>
		public Customer? InsertCustomer(CustomerInsertDTO dto)
		{
			if (dto == null)
			{
				return null;
			}

			var customer = _mapper.Map<Customer>(dto);

			try
			{
				using TransactionScope scope = new();
				Customer? insertedCustomer = _customerDAO.Insert(customer);
				scope.Complete();
				return insertedCustomer;
			} catch (Exception e)
			{
				_logger.LogError("An Error occured while inserting a customer: {ErrorMessage}", e.Message);
				throw;
			}
		}

		/// <summary>
		/// This method provides the operation for updating
		/// a cutomer in the database.
		/// </summary>
		/// <param name="dto">A <see cref="CustomerUpdateDTO"/> object.</param>
		/// <returns>The updated <see cref="Customer"/>.</returns>
		public Customer? UpdateCustomer(CustomerUpdateDTO dto)
		{
			if (dto == null)
			{
				return null;
			}

			Customer? customer = _mapper.Map<Customer>(dto);
			Customer? updatedCustomer = null;

			try
			{
				using TransactionScope scope = new();
				updatedCustomer = _customerDAO.GetById(customer.Id);

				if (updatedCustomer is null)
				{
					return null;
				}

				updatedCustomer = _customerDAO.Update(customer);
				scope.Complete();
			}
			catch (Exception e)
			{
				_logger.LogError("An Error occured while updating a customer: {ErrorMessage}", e.Message);
				throw;
			}
			
			return updatedCustomer;
		}

		/// <summary>
		/// This method provides the operation for deleting
		/// a cutomer from the database.
		/// </summary>
		/// <param name="id">The id of the customer to be deleted.</param>
		/// <returns>The deleted <see cref="Customer"/>.</returns>
		public Customer? DeleteCustomer(int id)
		{
			Customer? deletedCustomer = null;
			try
			{
				using TransactionScope scope = new();
				deletedCustomer = _customerDAO.GetById(id);

				if (deletedCustomer is null)
				{
					return null;
				}

				_customerDAO.Delete(id);
				scope.Complete();
			}
			catch (Exception e)
			{
				_logger.LogError("An Error occured while deleting a customer: {ErrorMessage}", e.Message);
				throw;
			}
			return deletedCustomer;
		}
	}
}
