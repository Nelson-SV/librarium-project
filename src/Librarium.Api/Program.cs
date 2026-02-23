
using Librarium.Data.Database;
using Librarium.Data.Repositories;
using Microsoft.EntityFrameworkCore;

public class Program
{
    public static async Task Main()
    {
        try
        {
            var builder = WebApplication.CreateBuilder();
            ConfigureServices(builder.Services, builder.Configuration);
            var app = builder.Build();
            
            // <snippet_UseSwagger>
            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();
                app.UseSwaggerUi(options =>
                {
                    options.DocumentPath = "/openapi/v1.json";
                });
            }
            // </snippet_UseSwagger>
            
            app.MapControllers();
            
            await app.RunAsync();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
        }
    }

    private static void ConfigureServices(IServiceCollection services, IConfiguration configuration)
    {
        services.AddControllers();
        
        // Configure AppOptions 
        //services.AddAppOptions(configuration);
        
        // Add database
        var connectionString = configuration["AppOptions:DbConnectionString"]
                ?? throw new InvalidOperationException("Connection string is not configured.");
        
        services.AddDbContext<LibrariumDbContext>(options =>
            options.UseSqlServer(connectionString));
        
        // Add repositories
        services.AddScoped<BookRepository>();
        services.AddScoped<LoanRepository>();
        services.AddScoped<MemberRepository>();
        
        services.AddOpenApi();
        
        // Add Services
        //services.AddScoped<CommentsService>();

    }
}