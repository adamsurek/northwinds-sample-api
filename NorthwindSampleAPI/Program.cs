using Microsoft.EntityFrameworkCore;
using NorthwindSampleAPI.Controllers;
using NorthwindSampleAPI.Models;

namespace NorthwindSampleAPI;

public class Program
{
	public static void Main(string[] args)
	{
		var builder = WebApplication.CreateBuilder(args);

		// Add services to the container.
		builder.Services.AddAuthorization();
		builder.Services.AddControllers();
		
		// Get environment variables
		string? connectionString =
			Environment.GetEnvironmentVariable("northwind-db-connection-string", EnvironmentVariableTarget.Machine);

		// Add Postgres service 
		builder.Services.AddNpgsql<NorthwindContext>(connectionString: connectionString);

		// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
		builder.Services.AddEndpointsApiExplorer();
		builder.Services.AddSwaggerGen();
		
		builder.Services.AddRouting(options => options.LowercaseUrls = true);

		var app = builder.Build();

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