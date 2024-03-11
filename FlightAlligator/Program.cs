using FlightAlligator.Adapters;
using FlightAlligator.Configurations;
using FlightAlligator.Configurations.Models;
using FlightAlligator.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace FlightAlligator
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Logging.ClearProviders();
            builder.Logging.AddConsole();
            builder.Logging.AddDebug();

            builder.Services.AddTransient<AirFlyInfoFlightAdapter>();
            builder.Services.AddTransient<SkyScannerFlightAdapter>();

            builder.Services.Configure<FlightApiSettings>(builder.Configuration.GetSection("FlightApiSettings"));
            builder.Services.AddHttpClient<FlightAPIService>();

            builder.Services.AddScoped<IFlightAPIService, FlightAPIService>();
            builder.Services.AddScoped<IFlightService, FlightService>();

            builder.Services.AddMemoryCache();
            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            
            app.UseSwagger();
            app.UseSwaggerUI();

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
