using ArticleCategory.Application.Article;
using ArticleCategory.Application.ArticleCategory;
using ArticleManagement.Application.Contracts.Article;
using ArticleManagement.Application.Contracts.ArticleCategory;
using ArticleManagement.Domain.ArticleAgg;
using ArticleManagement.Domain.ArticleCategoryAgg;
using ArticleManagement.Infrastructure.EfCore;
using ArticleManagement.Infrastructure.EfCore.Repository;
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

            services.AddTransient<IArticleRepository, ArticleRepository>();
            services.AddTransient<IArticleApplication, ArticleApplication>();

            services.AddDbContext<ArticleContext>(options =>
            {
                options.UseSqlServer(connectionString);
            });
        }
    }
}
