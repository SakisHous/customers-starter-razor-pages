using AutoMapper;
using CustomersApp.DAO;
using CustomersApp.DTO;
using CustomersApp.Models;
using System.Globalization;
using System.Transactions;

namespace CustomersApp.Services
{
	public class ProductServiceImpl : IProductService
	{
		private readonly IProductDAO _productDAO;
		private readonly IMapper _mapper;
		private readonly ILogger<ProductServiceImpl> _logger;

		public ProductServiceImpl(IProductDAO productDAO, IMapper mapper, ILogger<ProductServiceImpl> logger)
		{
			_productDAO = productDAO;
			_mapper = mapper;
			_logger = logger;
		}

		public IList<Product> GetAllProducts()
		{
			try
			{
				IList<Product> products = _productDAO.GetAll();
				return products;
			} catch (Exception e)
			{
				_logger.LogError("An Error occured when fetching all products: {ErrorMessage}", e.Message);
				throw;
			}
		}

		public Product? GetProductById(int id)
		{
			try
			{
				return _productDAO.GetById(id);
			} catch (Exception e)
			{
				_logger.LogError("An Error occured when fetching a product by: {ErrorMessage}", e.Message);
				throw;
			}
		}

		public Product? InsertProduct(ProductInsertDTO dto)
		{
			if (dto == null)
			{
				return null;
			}

            var product = _mapper.Map<Product>(dto);

            try
			{
				using TransactionScope scope = new();
				Product? insertedProduct = _productDAO.Insert(product);
				scope.Complete();
				return insertedProduct;
			}
			catch (Exception e)
			{
				_logger.LogError("An Error occured while inserting a product: {ErrorMessage}", e.Message);
				throw;
			}
		}

		public Product? UpdateProduct(ProductUpdateDTO dto)
		{
			if (dto == null)
			{
				return null;
			}

            Console.WriteLine(dto.Price!.GetType());
            Console.WriteLine(dto.Price);
			decimal d = decimal.Parse(dto.Price, CultureInfo.InvariantCulture);
			Console.WriteLine(d.ToString(CultureInfo.InvariantCulture));

			Product? product = _mapper.Map<Product>(dto);
			Console.WriteLine(product.Price.GetType());
			Console.WriteLine(product.Price);

			Product? updatedProduct = null;

			try
			{
				using TransactionScope scope = new();
				updatedProduct = _productDAO.GetById(product.Id);

				if (updatedProduct is null)
				{
					return null;
				}
				updatedProduct = _productDAO.Update(product);
				scope.Complete();
			} catch (Exception e)
			{
				_logger.LogError("An Error occured while updating a product: {ErrorMessage}", e.Message);
				throw;
			}
			return updatedProduct;
		}

		public Product? DeleteProduct(int id)
		{
			Product? deletedProduct = null;
			try
			{
				using TransactionScope scope = new();
				deletedProduct = _productDAO.GetById(id);

				if (deletedProduct is null)
				{
					return null;
				}

				_productDAO.Delete(id);
				scope.Complete();
			}
			catch (Exception e)
			{
				_logger.LogError("An Error occured while deleting a product: {ErrorMessage}", e.Message);
				throw;
			}
			return deletedProduct;
		}
	}
}
