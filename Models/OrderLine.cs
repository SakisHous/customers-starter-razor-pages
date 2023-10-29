namespace CustomersApp.Models
{
	public class OrderLine
	{
		public int OrderID { get; set; }
		public int CustomerID { get; set; }
		public int ProductID { get; set; }
		public string? ProductName { get; set; }
		public int ProductQty { get; set; }
		public DateTime OrderDate { get; set; }
		public decimal Price { get; set; }
	}
}
