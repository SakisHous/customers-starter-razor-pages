namespace CustomersApp.Models
{
	public class Order
	{
        public int OrderID { get; set; }
        public int CustomerID { get; set; }
        public int ProductID { get; set; }
        public int ProductQty { get; set; }
        public DateTime OrderDate { get; set; }
    }
}
