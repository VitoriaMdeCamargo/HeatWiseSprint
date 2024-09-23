using HeatWise_Sprint_2.Net.Persistence;
using HeatWise_Sprint_2.Net.Interface;
using HeatWise_Sprint_2.Net.Repositorios;
using HeatWise_Sprint_2.Net.Persistence.Repositorio;
using Microsoft.EntityFrameworkCore;

namespace HeatWise_Sprint_2.Net
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container(caixa) de injeção de dependência
            builder.Services.AddControllersWithViews();

            // Substituir o Oracle pelo Azure SQL
            builder.Services.AddDbContext<HeatWiseDbContext>(
                options =>
                {
                    options.UseSqlServer(builder.Configuration.GetConnectionString("AzureSqlDb"));
                }
            );

            // Repositórios injetados no container de dependência
            builder.Services.AddScoped<IPlanoRepositorio, PlanoRepositorio>();
            builder.Services.AddScoped<IEmpresaRepositorio, EmpresaRepositorio>();
            builder.Services.AddScoped<ISiteRepositorio, SiteRepositorio>();
            builder.Services.AddScoped<IAnaliseRepositorio, AnaliseRepositorio>();

            var app = builder.Build();

            // Configure o pipeline de requisição HTTP.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // O valor padrão de HSTS é 30 dias.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
