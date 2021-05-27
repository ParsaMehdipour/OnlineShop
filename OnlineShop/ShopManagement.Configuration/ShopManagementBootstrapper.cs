using System;
using _0_Framework.Domain;
using _0_Framework.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ShopManagement.Application.Contracts.ProductCategory;
using ShopManagement.Application.ProductCategory;
using ShopManagement.Domain.ProductCategoryAgg;
using ShopManagement.Infrastructure.EfCore;
using ShopManagement.Infrastructure.EfCore.Repositories;

namespace ShopManagement.Configuration
{
    public class ShopManagementBootstrapper
    {
        public static void Configure(IServiceCollection services,string connectionString)
        {
            services.AddTransient<IProductCategoryApplication, ProductCategoryApplication>();
            services.AddTransient<IProductCategoryRepository, ProductCategoryRepository>();

            services.AddDbContext<ShopContext>(options =>
            {
                options.UseSqlServer(connectionString);
            });
        }
    }
}
