using AutoMapper;
using System.Globalization;

namespace CustomersApp.Configuration
{
	/// <summary>
	/// This class defines the way to convert a string
	/// which represent a price to a decimal type.
	/// It is used from the <see cref="AutoMapper"/> to convert
	/// <see cref="Product"/> to <see cref="ProductReadOnly"/>
	/// and vice versa.
	/// </summary>
	public class CurrencyFormatter : IValueConverter<string, decimal>
	{
		public decimal Convert(string sourceMember, ResolutionContext context)
		{
			return decimal.Parse(sourceMember, CultureInfo.InvariantCulture);
		}
	}
}
