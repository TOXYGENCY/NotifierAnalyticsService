using StackExchange.Redis;

namespace NotifierAnalyticsService
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddSingleton<IConnectionMultiplexer>(_ =>
                ConnectionMultiplexer.Connect(builder.Configuration["Redis:ConnectionString"]));
            builder.Services.AddScoped(provider =>
                provider.GetRequiredService<IConnectionMultiplexer>().GetDatabase());

            var app = builder.Build();

            app.Urls.Add("http://*:6123");

            // Configure the HTTP request pipeline.
            if (bool.TryParse(builder.Configuration["UseSwagger"],
                out bool useSwagger) && useSwagger)
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
