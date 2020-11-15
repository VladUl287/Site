using Mar.Data;
using Mar.Data.Interfaces;
using Mar.Data.Models;
using Mar.Data.Repository;
using Mar.Data.Validation;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Mar
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
            //применение валидатора пароля
            services.AddTransient<IPasswordValidator<User>, CustomPasswordValidator>(a => new CustomPasswordValidator(6));
        
            services.AddDbContext<ApplicationContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddTransient<IProducts, ProductsRepository>();
            services.AddTransient<IProductsTags, TagRepository>();
            services.AddTransient<IProductsRelations, RelationRepository>();
            services.AddTransient<IProductsCategories, CategoryRepository>();

            services.AddIdentity<User, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationContext>();

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
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Product}/{action=Index}");
            });
            //инициализация бд 
            using (var scope = app.ApplicationServices.CreateScope())
            {
                ApplicationContext content = scope.ServiceProvider.GetRequiredService<ApplicationContext>();
                DbInitilizer.Initial(content);
            }
        }
    }
}
