namespace CustomersApp.DTO
{
	/// <summary>
	/// DTO class for updating a customer with
	/// all it's properties, even the username which
	/// it can't be updated.
	/// </summary>
	public class CustomerUpdateDTO : BaseDTO
	{
		public string Username { get; set; } = null!;
		public string Password { get; set; } = null!;
		public string? Firstname { get; set; }
		public string? Lastname { get; set; }
		public string Email { get; set; } = null!;
	}
}
