namespace CustomersApp.DTO
{
	public class ProductReadOnlyDTO : BaseDTO
	{
		public string? Name { get; set; }
		public string? Description { get; set; }
		public string? Price { get; set; }
		public int Quantity { get; set; }
	}
}
