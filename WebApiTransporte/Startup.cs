using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;
using WebApiTransporte.Models;
using WebApiTransporte.Services.SucursalServices;
using WebApiTransporte.Services.ColaboradorServices;
using WebApiTransporte.Services.TransportistaServices;

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
            services.AddAutoMapper(typeof(Program).Assembly);
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();
        }
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

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
