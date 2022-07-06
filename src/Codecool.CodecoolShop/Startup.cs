using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Codecool.CodecoolShop.Daos;
using Codecool.CodecoolShop.Daos.Implementations;
using Codecool.CodecoolShop.Data;
using Codecool.CodecoolShop.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Codecool.CodecoolShop
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<CodeCoolDBContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("IdentityDBContextConnection"));
            });

            services.AddControllersWithViews();
            services.AddRazorPages().AddRazorRuntimeCompilation();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Product/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
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
                    pattern: "{controller=Product}/{action=Index}/{id?}");
                endpoints.MapRazorPages();
            });

            SetupInMemoryDatabases();
        }

        private void SetupInMemoryDatabases()
        {
            IProductDao productDataStore = ProductDaoMemory.GetInstance();
            IAllOrdersDao ordersDataStore = OrderDaoMemory.GetInstance();
            IProductCategoryDao productCategoryDataStore = ProductCategoryDaoMemory.GetInstance();
            ISupplierDao supplierDataStore = SupplierDaoMemory.GetInstance();
            IUserDao userDataStore = UserDaoMemory.GetInstance();

            Supplier amazon = new Supplier{Name = "Amazon", Description = "Digital content and services"};
            supplierDataStore.Add(amazon);
            Supplier lenovo = new Supplier{Name = "Lenovo", Description = "Computers"};
            supplierDataStore.Add(lenovo);
            ProductCategory tablet = new ProductCategory {Name = "Tablet", Department = "Tablet", Description = "A tablet computer, commonly shortened to tablet, is a thin, flat mobile computer with a touchscreen display." };
            productCategoryDataStore.Add(tablet);
            ProductCategory computer = new ProductCategory { Name = "Computer", Department = "PC", Description = "A tablet computer, commonly shortened to tablet, is a thin, flat mobile computer with a touchscreen display." };
            productCategoryDataStore.Add(computer);
            productDataStore.Add(new Product { Name = "Amazon Fire", DefaultPrice = 49.9m, Currency = "USD", Quantity = 10, MaxInStock = 10, Description = "Fantastic price. Large content ecosystem. Good parental controls. Helpful technical support.", ProductCategory = tablet, Supplier = amazon });
            productDataStore.Add(new Product { Name = "Lenovo IdeaPad Miix 700", DefaultPrice = 479.0m, Currency = "USD", Quantity = 1, MaxInStock = 1, Description = "Keyboard cover is included. Fanless Core m5 processor. Full-size USB ports. Adjustable kickstand.", ProductCategory = tablet, Supplier = lenovo });
            productDataStore.Add(new Product { Name = "Amazon Fire HD 8", DefaultPrice = 89.0m, Currency = "USD", Quantity = 5, MaxInStock = 5, Description = "Amazon's latest Fire HD 8 tablet is a great value for media consumption.", ProductCategory = tablet, Supplier = amazon });
            productDataStore.Add(new Product { Name = "PC1", DefaultPrice = 49.9m, Currency = "USD", Quantity = 0, MaxInStock = 0, Description = "Fantastic price. Large content ecosystem. Good parental controls. Helpful technical support.", ProductCategory = computer, Supplier = amazon });
            productDataStore.Add(new Product { Name = "PC2", DefaultPrice = 479.0m, Currency = "USD", Quantity = 7, MaxInStock = 7, Description = "Keyboard cover is included. Fanless Core m5 processor. Full-size USB ports. Adjustable kickstand.", ProductCategory = computer, Supplier = lenovo });
            User Janusz = new User("Janusz");
            //Janusz.ShoppingCart.Add(new Product { Name = "Amazon Fire", DefaultPrice = 49.9m, Quantity = 10, Currency = "USD", Description = "Fantastic price. Large content ecosystem. Good parental controls. Helpful technical support.", ProductCategory = tablet, Supplier = amazon });
            //Janusz.ShoppingCart.Add(new Product { Name = "Amazon Fire", DefaultPrice = 204.9m, Quantity = 1, Currency = "USD", Description = "Fantastic price. Large content ecosystem. Good parental controls. Helpful technical support.", ProductCategory = tablet, Supplier = amazon });
            userDataStore.Add(Janusz);
            Console.WriteLine(1);

        }
    }
}
