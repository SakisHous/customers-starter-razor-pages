namespace CustomersApp.DTO
{
	/// <summary>
	/// DTO class for handling customer at view.
	/// </summary>
	public class CustomerReadOnlyDTO : BaseDTO
	{
		public string Username { get; set; } = null!;
		public string? Password { get; set; } = null!;
		public string? Firstname { get; set; }
		public string? Lastname { get; set; }
		public string Email { get; set; } = null!;
	}
}
