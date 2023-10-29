namespace CustomersApp.Models
{
    /// <summary>
    /// POCO (Plain Old CLR Object)
    /// This Customer Entity represents
    /// a customer and it is the Domain Model
    /// for CUSTOMERS Table.
    /// </summary>
	public class Customer
	{
        public int Id { get; set; }
        public string Username { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string? Firstname { get; set; }
        public string? Lastname { get; set; }
        public string Email { get; set; } = null!;
    }
}
