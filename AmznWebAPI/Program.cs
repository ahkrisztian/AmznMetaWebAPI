using AmznMetaLibrary.Policies;
using AmznMetaLibrary.Repo;
using Serilog;
using System.Text.Json.Serialization;

namespace AmznWebAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddSingleton<ClientPolicies>(new ClientPolicies());

            builder.Services.AddHttpClient<IAmznMetaRepo, AmznMetaRepo>(client =>
            {
                client.DefaultRequestHeaders.Accept.Clear();
            });

            builder.Services.AddCors(policy =>
            {
                policy.AddPolicy("OpenCorsPolicy", opt => opt.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());
            });

            var logger = new LoggerConfiguration()
                .ReadFrom.Configuration(builder.Configuration)
                .Enrich.FromLogContext()
                .CreateLogger();
            builder.Logging.ClearProviders();
            builder.Logging.AddSerilog(logger);


            builder.Services.AddControllers();

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddSingleton<IAmznMetaRepo, AmznMetaRepo>();

            var app = builder.Build();


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