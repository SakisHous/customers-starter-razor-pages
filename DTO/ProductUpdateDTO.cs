namespace CustomersApp.DTO
{
	public class ProductUpdateDTO : BaseDTO
	{
		public string? Name { get; set; }
		public string? Description { get; set; }
		public string? Price { get; set; }
		public int Quantity { get; set; }
	}
}
