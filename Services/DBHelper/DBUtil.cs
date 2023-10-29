using System.Data.SqlClient;

namespace CustomersApp.Services.DBHelper
{
	/// <summary>
	/// It is a Util class. It provides
	/// the utility method for getting a
	/// connections. <see cref="SqlConnection"/> gives a 
	/// new connection from a pool of 100 connections. If
	/// the connection is closed, it is reclaimed 
	/// back to the pool.
	/// </summary>
	public static class DBUtil
	{

		/// <summary>
		/// The utility method for recieving a new connection
		/// from ADO .NET data provider. The first time we want,
		/// it created a connection pool with default 100 connections.
		/// If the connection is closed, it is reclaimed back to
		/// connection pool.
		/// </summary>
		/// <returns></returns>
		public static SqlConnection? GetConnction()
		{
			var configurationBuilder = new ConfigurationBuilder().AddJsonFile("appsettings.json");
			var configuration = configurationBuilder.Build();

			string connectionString = configuration.GetConnectionString("DefaultConnection")!;

			try
			{
				SqlConnection conn = new(connectionString);
				return conn;
			} catch (Exception e)
			{
                Console.WriteLine(e.Message);
				return null;
            }
		}
	}
}
