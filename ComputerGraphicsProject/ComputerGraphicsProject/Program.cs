using ComputerGraphicsProject.Interfaces;
using ComputerGraphicsProject.Services;

namespace ComputerGraphicsProject
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddTransient<INewtonFractalService, NewtonFractalService>();
            builder.Services.AddTransient<IVicsecFractalService, VicsecFractalService>();
            builder.Services.AddTransient<ICmykService, CmykService>();
            builder.Services.AddTransient<IHslService, HslService>();
            builder.Services.AddControllersWithViews();
            


            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
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