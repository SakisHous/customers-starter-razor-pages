using AutoMapper;
using CustomersApp.DTO;
using CustomersApp.Models;
using CustomersApp.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CustomersApp.Pages.Orders
{
    public class IndexModel : PageModel
    {
        public Error? ErrorObj { get; set; }
        public IList<OrderReadOnlyDTO> OrdersReadOnlyDto { get; set; } = null!;

        private readonly IOrderService _orderService;
        private readonly IMapper _mapper;

		public IndexModel(IOrderService orderService, IMapper mapper)
		{
			_orderService = orderService;
			_mapper = mapper;
		}

		public void OnGet(int id)
        {
			ViewData["title"] = "All Orders";
			try
			{
				ErrorObj = null;
				IList<OrderLine> orderLines = _orderService.GetOrderByCustomerId(id);

				OrdersReadOnlyDto = new List<OrderReadOnlyDTO>();

				foreach (var line in orderLines)
				{
					OrderReadOnlyDTO? orderDto = _mapper!.Map<OrderReadOnlyDTO>(line);
					OrdersReadOnlyDto.Add(orderDto);
				}

			}
			catch (Exception e)
			{
				ErrorObj = new Error("", e.Message, "");
			}
		}
    }
}
