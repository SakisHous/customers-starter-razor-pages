using CustomersApp.Models;
using CustomersApp.Services.DBHelper;
using System.Data.SqlClient;

namespace CustomersApp.DAO
{
	public class OrderDAOImpl : IOrderDAO
	{

		public Order? Get(int customerId, int productId)
		{
			string sql = "SELECT * FROM ORDERS WHERE CUSTOMERID = @cid AND PRODUCTID = @pid;";
			Order? order = null;

			using SqlConnection? conn = DBUtil.GetConnction();
			conn!.Open();

			using SqlCommand command = new(sql, conn);
			command.Parameters.AddWithValue("@cid", customerId);
			command.Parameters.AddWithValue("@pid", productId);

			using SqlDataReader reader = command.ExecuteReader();

			if (reader.Read())
			{
				order = new()
				{
					OrderID = reader.GetInt32(reader.GetOrdinal("ORDERID")),
					CustomerID = reader.GetInt32(reader.GetOrdinal("CUSTOMERID")),
					ProductID = reader.GetInt32(reader.GetOrdinal("PRODUCTID")),
					ProductQty = reader.GetInt32(reader.GetOrdinal("PRODUCTQTY")),
					OrderDate = reader.GetDateTime(reader.GetOrdinal("ORDERDATE"))
				};
			}
			return order;
		}
		public IList<OrderLine> GetByCustomerId(int customerId)
		{
			string sql = "SELECT * FROM ORDERS, PRODUCTS WHERE PRODUCTS.ID = ORDERS.PRODUCTID AND CUSTOMERID = @cid;";
			var orderLines = new List<OrderLine>();

			using SqlConnection? conn = DBUtil.GetConnction();
			conn!.Open();

			using SqlCommand command = new(sql, conn);
			command.Parameters.AddWithValue("@cid", customerId);
			
			using SqlDataReader reader = command.ExecuteReader();

			int count = 0;
			while (reader.Read())
			{
				count++;
				OrderLine orderLine = new()
				{
					OrderID = reader.GetInt32(reader.GetOrdinal("ORDERID")),
					CustomerID = reader.GetInt32(reader.GetOrdinal("CUSTOMERID")),
					ProductID = reader.GetInt32(reader.GetOrdinal("PRODUCTID")),
					ProductName = reader.GetString(reader.GetOrdinal("NAME")),
					ProductQty = reader.GetInt32(reader.GetOrdinal("PRODUCTQTY")),
					OrderDate = reader.GetDateTime(reader.GetOrdinal("ORDERDATE")),
					Price = reader.GetDecimal(reader.GetOrdinal("PRICE"))
				};
				orderLines.Add(orderLine);
			}
            Console.WriteLine($"Counting: {count}");
            return orderLines;
		}

		public IList<Order> GetByProductId(int productId)
		{
			string sql = "SELECT * FROM ORDERS WHERE PRODUCTID = @pid;";
			var orders = new List<Order>();

			using SqlConnection? conn = DBUtil.GetConnction();
			conn!.Open();

			using SqlCommand command = new(sql, conn);
			command.Parameters.AddWithValue("@pid", productId);

			using SqlDataReader reader = command.ExecuteReader();

			while (reader.Read())
			{
				Order order = new()
				{
					OrderID = reader.GetInt32(reader.GetOrdinal("ORDERID")),
					CustomerID = reader.GetInt32(reader.GetOrdinal("CUSTOMERID")),
					ProductID = reader.GetInt32(reader.GetOrdinal("PRODUCTID")),
					ProductQty = reader.GetInt32(reader.GetOrdinal("PRODUCTQTY")),
					OrderDate = reader.GetDateTime(reader.GetOrdinal("ORDERDATE"))
				};
				orders.Add(order);
			}
			return orders;
		}

		public IList<Order> GetAll()
		{
			string sql = "SELECT * FROM ORDERS;";
			var orders = new List<Order>();

			using SqlConnection? conn = DBUtil.GetConnction();
			conn!.Open();

			using SqlCommand command = new(sql, conn);

			using SqlDataReader reader = command.ExecuteReader();

			while (reader.Read())
			{
				Order order = new()
				{
					OrderID = reader.GetInt32(reader.GetOrdinal("ORDERID")),
					CustomerID = reader.GetInt32(reader.GetOrdinal("CUSTOMERID")),
					ProductID = reader.GetInt32(reader.GetOrdinal("PRODUCTID")),
					ProductQty = reader.GetInt32(reader.GetOrdinal("PRODUCTQTY")),
					OrderDate = reader.GetDateTime(reader.GetOrdinal("ORDERDATE"))
				};
				orders.Add(order);
			}
			return orders;
		}

		public Order? Insert(Order order)
		{
			if (order is null)
			{
				return null;
			}

			string sql = "INSERT INTO ORDERS (CUSTOMERID, PRODUCTID, ORDERDATE) VALUE (@customerid, @productid, @orderdate);";
			Order? orderToReturn = null;

			using SqlConnection? conn = DBUtil.GetConnction();
			conn!.Open();

			using SqlCommand insertCommand = new(sql, conn);
			insertCommand.Parameters.AddWithValue("@customerid", order.CustomerID);
			insertCommand.Parameters.AddWithValue("@productid", order.ProductID);
			insertCommand.Parameters.AddWithValue("@orderdate", order.OrderDate);

			object insertedObj = insertCommand.ExecuteScalar();

			if (insertedObj is not null)
			{
                Console.WriteLine("Error in inserting new Order");
            }

			string sqlInsertedProduct = "SELECT * FROM ORDERS WHERE CUSTOMERID=@cid AND PRODUCTID=@pid";

			using SqlCommand selectCommand = new(sqlInsertedProduct, conn);
			selectCommand.Parameters.AddWithValue("@cid", order.CustomerID);
			selectCommand.Parameters.AddWithValue("@pid", order.ProductID);

			using SqlDataReader reader = selectCommand.ExecuteReader();

			if (reader.Read())
			{
				orderToReturn = new Order()
				{
					CustomerID = reader.GetInt32(reader.GetOrdinal("CUSTOMERID")),
					ProductID = reader.GetInt32(reader.GetOrdinal("PRODUCTID")),
					OrderDate = reader.GetDateTime(reader.GetOrdinal("ORDERDATE"))
				};
			}
			reader.Close();
			return orderToReturn;
		}

		public Order? Update(Order order)
		{
			if (order is null)
			{
				return null;
			}

			string sql = "UPDATE ORDERS SET ORDERDATE=@orderdate WHERE CUSTOMERID=@cid AND PRODUCTID=@pid;";

			Order? orderToReturn = null;

			using SqlConnection? conn = DBUtil.GetConnction();
			conn!.Open();

			using SqlCommand updateCommand = new(sql, conn);
			updateCommand.Parameters.AddWithValue("@cid", order.CustomerID);
			updateCommand.Parameters.AddWithValue("@pid", order.ProductID);
			updateCommand.ExecuteNonQuery();
			orderToReturn = order;

			return orderToReturn;
		}

		public void Delete(int customerId, int productId)
		{
			string sql = "DELETE FROM ORDERS WHERE CUSTOMERID=@cid AND PRODUCTID=@pid";
			using SqlConnection? conn = DBUtil.GetConnction();
			conn!.Open();

			using SqlCommand deleteCommand = new(sql, conn);
			deleteCommand.Parameters.AddWithValue("@cid", customerId);
			deleteCommand.Parameters.AddWithValue("@pid", productId);
			deleteCommand.ExecuteNonQuery();
		}


	}
}
