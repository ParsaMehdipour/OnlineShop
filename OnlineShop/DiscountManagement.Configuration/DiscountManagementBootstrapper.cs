using DiscountManagement.Application.Contracts.CustomerDiscount;
using DiscountManagement.Application.CustomerDiscount;
using DiscountManagement.Domain.CustomerDiscountAgg;
using DiscountManagement.Infrastructure.EfCore;
using DiscountManagement.Infrastructure.EfCore.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace DiscountManagement.Configuration
{
    public class DiscountManagementBootstrapper
    {
        public static void Configure(IServiceCollection services , string connectionString)
        {
            services.AddTransient<ICustomerDiscountRepository, CustomerDiscountRepository>();

            services.AddTransient<ICustomerDiscountApplication, CustomerDiscountApplication>();

            services.AddDbContext<DiscountContext>(options =>
            {
                options.UseSqlServer(connectionString);
            });
        }
    }
}
