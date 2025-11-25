using Share.Trading.Application.Infrastructure.Repositories.Exchange;
using Share.Trading.Application.Infrastructure.Repositories.Portfolio;
using Share.Trading.Application.Infrastructure.Services;
using Shares.Trading.Application.Queries.Portfolio;
using Van.Lankschot.Kempen.Api.Filters;

namespace Van.Lankschot.Kempen.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            


            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(GetPortfolioQuery).Assembly));
            builder.Services.AddSingleton<IVKLExchange, VKLExchange>();
            builder.Services.AddSingleton<IPortfolioRepository, PortfolioRepository>();
            builder.Services.AddScoped<IPortfolioService, PortfolioService>();
            builder.Services.AddScoped<IExchangeService, ExchangeService>();
            builder.Services.AddScoped<ApiExceptionFilterAttribute>();
            builder.Services.AddControllers(options =>
            {
                options.Filters.AddService<ApiExceptionFilterAttribute>();
            });


            var app = builder.Build();
            app.UseCors(builder => builder.AllowAnyOrigin()
                                          .AllowAnyHeader()
                                          .AllowAnyMethod());

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
