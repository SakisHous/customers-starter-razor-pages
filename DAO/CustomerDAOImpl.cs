
using CustomersApp.Models;
using CustomersApp.Services.DBHelper;
using System.Data.SqlClient;

namespace CustomersApp.DAO
{
	/// <summary>
	/// This class implements the DAO leayer design
	/// pattern and provides the CRUD methods for database
	/// operations.
	/// </summary>
	public class CustomerDAOImpl : ICustomerDAO
	{
		/// <summary>
		/// This method retrieves all customers from
		/// the database.
		/// </summary>
		/// <returns>A list of customers</returns>
		public IList<Customer> GetAll()
		{
			String sql = "SELECT * FROM CUSTOMERS";
			var customers = new List<Customer>();

			using SqlConnection? conn = DBUtil.GetConnction();
			conn!.Open();

			using SqlCommand command = new(sql, conn);
			using SqlDataReader reader = command.ExecuteReader();

			while (reader.Read())
			{
				Customer customer = new()
				{
					Id = reader.GetInt32(reader.GetOrdinal("ID")),
					Username = reader.GetString(reader.GetOrdinal("USERNAME")),
					Password = reader.GetString(reader.GetOrdinal("PASSWORD")),
					Firstname = reader.GetString(reader.GetOrdinal("FIRSTNAME")),
					Lastname = reader.GetString(reader.GetOrdinal("LASTNAME")),
					Email = reader.GetString(reader.GetOrdinal("EMAIL"))
				};
				customers.Add(customer);
			}
			return customers;
		}

		/// <summary>
		/// This class retrieves a customer given by the id.
		/// </summary>
		/// <param name="id">The id (primary key) of the customer in the database</param>
		/// <returns>A <see cref="Customer"/> object</returns>
		public Customer? GetById(int id)
		{
			String sql = "SELECT * FROM CUSTOMERS WHERE ID = @id";
			Customer? customer = null;

			using SqlConnection? conn = DBUtil.GetConnction();
			conn!.Open();

			using SqlCommand command = new(sql, conn);
			command.Parameters.AddWithValue("@id", id);
			using SqlDataReader reader = command.ExecuteReader();

			if (reader.Read())
			{
				customer = new()
				{
					Id = reader.GetInt32(reader.GetOrdinal("ID")),
					Username = reader.GetString(reader.GetOrdinal("USERNAME")),
					Password = reader.GetString(reader.GetOrdinal("PASSWORD")),
					Firstname = reader.GetString(reader.GetOrdinal("FIRSTNAME")),
					Lastname = reader.GetString(reader.GetOrdinal("LASTNAME")),
					Email = reader.GetString(reader.GetOrdinal("EMAIL"))
				};
			}
			return customer;
		}

		/// <summary>
		/// Inserts a new customer in the database.
		/// </summary>
		/// <param name="customer">The customer to be inserted</param>
		/// <returns>An object <see cref="Customer"/> which has inserted</returns>
		public Customer? Insert(Customer customer)
		{
			if (customer == null)
			{
				return null;
			}

			string sql = "INSERT INTO CUSTOMERS (USERNAME, PASSWORD, FIRSTNAME, LASTNAME, EMAIL) " +
				"VALUES (@username, @password, @firstname, @lastname, @email);" +
				"SELECT SCOPE_IDENTITY();";

			Customer? customerToReturn = null;
			using SqlConnection? conn = DBUtil.GetConnction();
			conn!.Open();

			using SqlCommand insertCommand = new(sql, conn);
			insertCommand.Parameters.AddWithValue("@username", customer.Username);
			insertCommand.Parameters.AddWithValue("@password", customer.Password);
			insertCommand.Parameters.AddWithValue("@firstname", customer.Firstname);
			insertCommand.Parameters.AddWithValue("@lastname", customer.Lastname);
			insertCommand.Parameters.AddWithValue("@email", customer.Email);

			object insertedObj = insertCommand.ExecuteScalar();
			int insertedId = 0;

			if (insertedObj is not null)
			{
				if (!int.TryParse(insertedObj.ToString(), out insertedId))
				{
                    Console.WriteLine("Error in inserted id");
                }
			}

			string sqlInsertedStudent = "SELECT * FROM CUSTOMERS WHERE ID=@id";

			using SqlCommand selectCommand = new(sqlInsertedStudent, conn);
			selectCommand.Parameters.AddWithValue("@id", insertedId);

			using SqlDataReader reader = selectCommand.ExecuteReader();
			if (reader.Read())
			{
				customer = new()
				{
					Id = reader.GetInt32(reader.GetOrdinal("ID")),
					Username = reader.GetString(reader.GetOrdinal("USERNAME")),
					Password = reader.GetString(reader.GetOrdinal("PASSWORD")),
					Firstname = reader.GetString(reader.GetOrdinal("FIRSTNAME")),
					Lastname = reader.GetString(reader.GetOrdinal("LASTNAME")),
					Email = reader.GetString(reader.GetOrdinal("EMAIL"))
				};
			}
			reader.Close();
			return customerToReturn;
		}

		/// <summary>
		/// Updates a customer if exists in the database.
		/// </summary>
		/// <param name="customer">The customer to be updated.</param>
		/// <returns>The updated customer if exists, otherwise null</returns>
		public Customer? Update(Customer? customer)
		{
			if (customer is null)
			{
				return null;
			}

			string sql = "UPDATE CUSTOMERS SET PASSWORD=@password, FIRSTNAME=@firstname, LASTNAME=@lastname, EMAIL=@email " +
				"WHERE ID=@id";
			Customer? customerToReturn = null;

			using SqlConnection? conn = DBUtil.GetConnction();
			conn!.Open();

			using SqlCommand updateCommand = new(sql, conn);
			updateCommand.Parameters.AddWithValue("@password", customer.Password);
			updateCommand.Parameters.AddWithValue("@firstname", customer.Firstname);
			updateCommand.Parameters.AddWithValue("@lastname", customer.Lastname);
			updateCommand.Parameters.AddWithValue("@email", customer.Email);
			updateCommand.Parameters.AddWithValue("@id", customer.Id);
			updateCommand.ExecuteNonQuery();
			customerToReturn = customer;

			return customerToReturn;
		}

		/// <summary>
		/// Deletes a customer from the database.
		/// </summary>
		/// <param name="id">The id of the customer to be deleted.</param>
		public void Delete(int id)
		{
			string sql = "DELETE FROM CUSTOMERS WHERE ID=@id";
			using SqlConnection? conn = DBUtil.GetConnction();
			conn!.Open();

			using SqlCommand deleteCommand = new(sql, conn);
			deleteCommand.Parameters.AddWithValue("@id", id);
			deleteCommand.ExecuteNonQuery();
		}
	}
}
