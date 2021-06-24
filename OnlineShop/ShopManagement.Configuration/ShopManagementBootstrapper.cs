using System;
using _0_Framework.Domain;
using _0_Framework.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ShopManagement.Application.Contracts.Product;
using ShopManagement.Application.Contracts.ProductCategory;
using ShopManagement.Application.Contracts.ProductPicture;
using ShopManagement.Application.Contracts.Slider;
using ShopManagement.Application.Product;
using ShopManagement.Application.ProductCategory;
using ShopManagement.Application.ProductPicture;
using ShopManagement.Application.Slider;
using ShopManagement.Domain.ProductAgg;
using ShopManagement.Domain.ProductCategoryAgg;
using ShopManagement.Domain.ProductPictureAgg;
using ShopManagement.Domain.SliderAgg;
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
            services.AddTransient<IProductPictureRepository, ProductPictureRepository>();
            services.AddTransient<ISliderRepository, SliderRepository>();

            services.AddTransient<IProductApplication, ProductApplication>();
            services.AddTransient<IProductRepository, ProductRepository>();
            services.AddTransient<IProductPictureApplication, ProductPictureApplication>();
            services.AddTransient<ISliderApplication, SliderApplication>();

            services.AddDbContext<ShopContext>(options =>
            {
                options.UseSqlServer(connectionString);
            });
        }
    }
}
