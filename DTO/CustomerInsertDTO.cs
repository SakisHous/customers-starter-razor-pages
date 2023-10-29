namespace CustomersApp.DTO
{
    /// <summary>
    /// DTO class for insert new Customer.
    /// </summary>
	public class CustomerInsertDTO
	{
        public string Username { get; set; } = null!;
        public string? Password { get; set; } = null!;
        public string? Firstname { get; set; }
        public string? Lastname { get; set; }
        public string Email { get; set; } = null!;
    }
}
