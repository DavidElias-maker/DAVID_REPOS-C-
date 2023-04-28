using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;
using WebApiTransporte.Models;
using WebApiTransporte.Services.SucursalServices;
using WebApiTransporte.Services.ColaboradorServices;
using WebApiTransporte.Services.TransportistaServices;
using WebApiTransporte.Services.Sucursal_ColaboradorServices;
using WebApiTransporte.Services.ViajeServices;

namespace WebApiTransporte
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers().AddJsonOptions(x => x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);
            services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("defaultConnection")));

            services.AddScoped<IColaboradorService, ColaboradorService>();
            services.AddScoped<ISucursalService, SucursalService>();
            services.AddScoped<ITransportistaService, TransportistaService>();
            services.AddScoped<ISucursal_ColaboradorService, Sucursal_ColaboradorService>();
            services.AddScoped<IViajeService, ViajeService>();
            services.AddAutoMapper(typeof(Program).Assembly);
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();
            services.AddCors(options => options.AddPolicy(name: "ColaboradorOrigins",
                policy => 
                {
                    policy.WithOrigins("http://localhost:4200").AllowAnyMethod().AllowAnyHeader();

            }));
        }
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseCors("ColaboradorOrigins");

            app.UseHttpsRedirection();
            app.UseRouting(); 

            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

           
        }

    }
}
