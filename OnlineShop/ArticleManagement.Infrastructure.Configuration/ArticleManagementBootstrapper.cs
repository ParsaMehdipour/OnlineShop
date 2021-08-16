using ArticleCategory.Application.ArticleCategory;
using ArticleManagement.Application.Contracts.ArticleCategory;
using ArticleManagement.Domain.ArticleCategoryAgg;
using ArticleManagement.Infrastructure.EfCore;
using ArticleManagement.Infrastructure.EFCore.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace ArticleManagement.Infrastructure.Configuration
{
    public class ArticleManagementBootstrapper
    {
        public static void Configure(IServiceCollection services, string connectionString)
        {
            services.AddTransient<IArticleCategoryRepository, ArticleCategoryRepository>();
            services.AddTransient<IArticleCategoryApplication, ArticleCategoryApplication>();

            services.AddDbContext<ArticleContext>(options =>
            {
                options.UseSqlServer(connectionString);
            });
        }
    }
}
