using AmznMetaLibrary.Policies;
using AmznMetaLibrary.Repo;
using Serilog;
using Microsoft.Extensions.Logging;
using System.Text.Json.Serialization;
using System.Reflection;
using AmznMetaLibrary.JsonDocFilter;
using Microsoft.OpenApi.Models;

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

            Log.Logger = new LoggerConfiguration()
                .ReadFrom.Configuration(builder.Configuration)
                .CreateLogger();

            builder.Host.UseSerilog();

            builder.Services.AddControllers();

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(opts =>
            {

                opts.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "Kundenrezensionen von Amazon.de",
                    Description = "Die API sammelt alle Bewertungen von Amazon und erstellt aus der Bewertung ein Review Model",
                    Contact = new OpenApiContact()
                    {
                        Name = "Krisztian",
                        Url = new Uri("https://github.com/ahkrisztian")
                    }
                });

                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";

                opts.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFile));

                opts.DocumentFilter<JsonPatchDocumentFilter>();
            });

            builder.Services.AddSingleton<IAmznMetaRepo, AmznMetaRepo>();

            var app = builder.Build();


            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI(opts =>
                {
                    //opts.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
                    //opts.RoutePrefix = string.Empty;

                    opts.DefaultModelsExpandDepth(-1);
                });
            }

            app.UseSerilogRequestLogging();

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}