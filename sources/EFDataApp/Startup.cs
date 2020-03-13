using EFDataApp.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
//TESTIRUEM3
namespace EFDataApp
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
            //<<<<<<< HEAD
            // �������� ������ ����������� �� ����� ������������
            string connection = Configuration.GetConnectionString("DefaultConnection");
            // ��������� �������� MobileContext � �������� ������� � ����������
            //=======
            // ïîëó÷àåì ñòðîêó ïîäêëþ÷åíèÿ èç ôàéëà êîíôèãóðàöèè
            //string connection = Configuration.GetConnectionString("DefaultConnection");
            // äîáàâëÿåì êîíòåêñò MobileContext â êà÷åñòâå ñåðâèñà â ïðèëîæåíèå
            //>>>>>>> 6799d570bef34be9b116c06bc5d8a1f1cd54ddb1
            services.AddDbContext<ApplicationContext>(options =>
                options.UseSqlServer(connection));
            services.AddControllersWithViews();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Patients/Error");
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>


            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Patients}/{action=Index}/{id?}");
            });
        }
    }
}
