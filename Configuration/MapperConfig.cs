using AutoMapper;
using CustomersApp.DTO;
using CustomersApp.Models;

namespace CustomersApp.Configuration
{
	/// <summary>
	/// This class defines the mapping configuration of the
	/// Models and Data Tranfer Objects (DTOs).
	/// </summary>
	public class MapperConfig : Profile
	{
		public MapperConfig() 
		{
			CreateMap<CustomerInsertDTO, Customer>().ReverseMap();
			CreateMap<CustomerUpdateDTO, Customer>().ReverseMap();
			CreateMap<CustomerReadOnlyDTO, Customer>().ReverseMap();

			CreateMap<ProductInsertDTO, Product>()
				.ForMember(d => d.Price, opt => opt.ConvertUsing<CurrencyFormatter, string>())
				.ReverseMap();

			CreateMap<ProductUpdateDTO, Product>()
				.ForMember(d => d.Price, opt => opt.ConvertUsing<CurrencyFormatter, string>())
				.ReverseMap();

			CreateMap<ProductReadOnlyDTO, Product>()
				.ForMember(d => d.Price, opt => opt.ConvertUsing<CurrencyFormatter, string>())
				.ReverseMap();

			CreateMap<OrderInsertDTO, Order>().ReverseMap();
			CreateMap<OrderUpdateDTO, Order>().ReverseMap();
			CreateMap<OrderLine, OrderReadOnlyDTO>()
				.ForMember(dest => dest.Cost, opt => opt.MapFrom(new CostResolver()))
				.ReverseMap();
		} 
	}
}
