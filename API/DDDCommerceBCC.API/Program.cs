using DDDCommerceBCC.infra;
using DDDCommerceBCC.infra.Interface;
using DDDCommerceBCC.Infra.Repositories;
using Microsoft.EntityFrameworkCore;

namespace DDDCommerceBCC.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddScoped<IClienteRepository, ClienteRepository>();
            builder.Services.AddScoped<IPedidoRepository, PedidoRepository>();
            builder.Services.AddScoped<IEntregaRepository, EntregaRepository>();
            builder.Services.AddScoped<IItemPedidoRepository, ItemPedidoRepository>();
            builder.Services.AddDbContext<AppDbContext>();


            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

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
