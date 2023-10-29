
using IOC.Modulos;
using Repositorio;
using System.Text.Json.Serialization;

namespace heroisAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            string politicaCors = "_politicaCors";
            builder.Services.AddControllers();//.AddJsonOptions(options =>
            //{
            //    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve;
            //}); ;
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            IServiceCollection services = builder.Services;
            FluxoGeralInjecao.Registrar(services);
            services.AddCors(opcoes =>
            {
                opcoes.AddPolicy(name: politicaCors,
                    policy =>
                    {
                        policy.AllowAnyMethod();
                        policy.AllowAnyHeader();
                        policy.AllowAnyOrigin();
                    });
            });
            builder.Services.AddDbContext<AcessoDados>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseCors(politicaCors);
            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}