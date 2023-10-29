using CustomersApp.Models;
using CustomersApp.Services.DBHelper;
using System.Data.SqlClient;

namespace CustomersApp.DAO
{
	public class ProductDAOImpl : IProductDAO
	{

		public IList<Product> GetAll()
		{
			String sql = "SELECT * FROM PRODUCTS";
			var products = new List<Product>();

			using SqlConnection? conn = DBUtil.GetConnction();
			conn!.Open();

			using SqlCommand command = new(sql, conn);
			using SqlDataReader reader = command.ExecuteReader();

			while (reader.Read())
			{
				Product product = new Product()
				{
					Id = reader.GetInt32(reader.GetOrdinal("ID")),
					Name = reader.GetString(reader.GetOrdinal("Name")),
					Description = reader.GetString(reader.GetOrdinal("Description")),
					Price = reader.GetDecimal(reader.GetOrdinal("PRICE")),
					Quantity = reader.GetInt32(reader.GetOrdinal("QUANTITY"))
				};
				products.Add(product);
			}
			return products;
		}

		public Product? GetById(int id)
		{
			String sql = "SELECT * FROM PRODUCTS WHERE ID = @id";
			Product? product = null;

			using SqlConnection? conn = DBUtil.GetConnction();
			conn!.Open();

			using SqlCommand command = new(sql, conn);
			command.Parameters.AddWithValue("@id", id);
			using SqlDataReader reader = command.ExecuteReader();

			while (reader.Read())
			{
				product = new Product()
				{
					Id = reader.GetInt32(reader.GetOrdinal("ID")),
					Name = reader.GetString(reader.GetOrdinal("NAME")),
					Description = reader.GetString(reader.GetOrdinal("DESCRIPTION")),
					Price = reader.GetDecimal(reader.GetOrdinal("PRICE")),
					Quantity = reader.GetInt32(reader.GetOrdinal("QUANTITY"))
				};
			}
			return product;
		}

		public Product? Insert(Product product)
		{
			if (product == null)
			{
				return null;
			}

			string sql = "INSERT INTO PRODUCTS (NAME, DESCRIPTION, PRICE, QUANTITY) " +
				"VALUES (@name, @description, @price, @quantity);" +
				"SELECT SCOPE_IDENTITY();";
			Product? productToReturn = null;

			using SqlConnection? conn = DBUtil.GetConnction();
			conn!.Open();

			using SqlCommand insertCommand = new(sql, conn);
			insertCommand.Parameters.AddWithValue("@name", product.Name);
			insertCommand.Parameters.AddWithValue("@description", product.Description);
			insertCommand.Parameters.AddWithValue("@price", product.Price);
			insertCommand.Parameters.AddWithValue("@quantity", product.Quantity);

			object insertedObj = insertCommand.ExecuteScalar();
			int insertedId = 0;

			if (insertedObj is not null)
			{
				if (!int.TryParse(insertedObj.ToString(), out insertedId))
				{
					Console.WriteLine("Error in inserted id");
				}
			}

			string sqlInsertedProduct = "SELECT * FROM PRODUCTS WHERE ID=@id";

			using SqlCommand selectCommand = new(sqlInsertedProduct, conn);
			selectCommand.Parameters.AddWithValue("@id", insertedId);

			using SqlDataReader reader = selectCommand.ExecuteReader();

			if (reader.Read())
			{
				productToReturn = new Product()
				{
					Id = reader.GetInt32(reader.GetOrdinal("ID")),
					Name = reader.GetString(reader.GetOrdinal("NAME")),
					Description = reader.GetString(reader.GetOrdinal("DESCRIPTION")),
					Price = reader.GetDecimal(reader.GetOrdinal("PRICE")),
					Quantity = reader.GetInt32(reader.GetOrdinal("QUANTITY"))
				};
			}
			reader.Close();
			return productToReturn;
		}

		public Product? Update(Product product)
		{
			if (product is null)
			{
				return null;
			}

			string sql = "UPDATE PRODUCTS SET NAME=@name, DESCRIPTION=@description, PRICE=@price, QUANTITY=@quantity " +
				"WHERE ID=@id";
			Product? productToReturn = null;

			using SqlConnection? conn = DBUtil.GetConnction();
			conn!.Open();

			using SqlCommand updateCommand = new(sql, conn);
			updateCommand.Parameters.AddWithValue("@name", product.Name);
			updateCommand.Parameters.AddWithValue("@description", product.Description);
			updateCommand.Parameters.AddWithValue("@price", product.Price);
			updateCommand.Parameters.AddWithValue("@quantity", product.Quantity);
			updateCommand.Parameters.AddWithValue("@id", product.Id);
			updateCommand.ExecuteNonQuery();
			productToReturn = product;

			return productToReturn;
		}

		public void Delete(int id)
		{
			string sql = "DELETE FROM PRODUCTS WHERE ID=@id";
			using SqlConnection? conn = DBUtil.GetConnction();
			conn!.Open();

			using SqlCommand deleteCommand = new(sql, conn);
			deleteCommand.Parameters.AddWithValue("@id", id);
			deleteCommand.ExecuteNonQuery();
		}
	}
}
