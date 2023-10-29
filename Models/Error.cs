namespace CustomersApp.Models
{
	/// <summary>
	/// This class defines an custom Error class which 
	/// passes the error (e.g. error message) in the view
	/// for the user.
	/// </summary>
	public class Error
	{
        public string? Code { get; set; }
        public string? Message { get; set; }
        public string? Field { get; set; }

		public Error()
		{
		}

		public Error(string? code, string? message, string? field)
		{
			Code = code;
			Message = message;
			Field = field;
		}
	}
}
