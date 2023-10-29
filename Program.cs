using CustomersApp.Configuration;
using CustomersApp.DAO;
using CustomersApp.DTO;
using CustomersApp.Services;
using CustomersApp.Validators;
using FluentValidation;
using Serilog;

namespace CustomersApp
{
	public class Program
	{
		public static void Main(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);

			builder.Host.UseSerilog((context, config) =>
			{
				config.ReadFrom.Configuration(context.Configuration);

					/*.MinimumLevel.Debug()
					.MinimumLevel.Override("Microsoft", Serilog.Events.LogEventLevel.Information)
					.WriteTo.Console()
					.WriteTo.File(
						"Logs/logs.txt",
						rollingInterval: RollingInterval.Day,
						outputTemplate: "[{Timestamp: dd-MM-yyyy HH:mm:ss} {SourceContext} {level}] " +
						"{Message}{NewLine}{Exception}",
						retainedFileCountLimit: null,
						fileSizeLimitBytes: null
					);*/
			});

			// Add services to the container.
			builder.Services.AddRazorPages();
			builder.Services.AddScoped<ICustomerDAO, CustomerDAOImpl>();
			builder.Services.AddScoped<ICustomerService, CustomerServiceImpl>();
			builder.Services.AddScoped<IProductDAO, ProductDAOImpl>();
			builder.Services.AddScoped<IProductService, ProductServiceImpl>();
			builder.Services.AddScoped<IOrderDAO, OrderDAOImpl>();
			builder.Services.AddScoped<IOrderService, OrderServiceImpl>();

			builder.Services.AddScoped<IValidator<CustomerInsertDTO>, CustomerInsertValidator>();
			builder.Services.AddScoped<IValidator<CustomerUpdateDTO>, CustomerUpdateValidator>();
			builder.Services.AddScoped<IValidator<ProductInsertDTO>, ProductInsertValidator>();
			builder.Services.AddScoped<IValidator<ProductUpdateDTO>, ProductUpdateValidator>();
			//builder.Services.AddScoped<IValidator<OrderInsertDTO>, OrderInsertValidator>();
			builder.Services.AddScoped<IValidator<ProductUpdateDTO>, ProductUpdateValidator>();

			builder.Services.AddAutoMapper(typeof(MapperConfig));

			var app = builder.Build();

			// Configure the HTTP request pipeline.
			if (!app.Environment.IsDevelopment())
			{
				app.UseExceptionHandler("/Error");
				// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
				app.UseHsts();
			}

			app.UseHttpsRedirection();
			app.UseStaticFiles();

			app.UseRouting();

			app.UseAuthorization();

			app.MapRazorPages();

			app.Run();
		}
	}
}