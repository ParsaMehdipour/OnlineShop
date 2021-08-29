using System.Collections.Generic;
using System.Linq;
using _01_OnlineShopQuery.Contracts.ArticleCategory;
using ArticleManagement.Infrastructure.EfCore;
using Microsoft.EntityFrameworkCore;

namespace _01_OnlineShopQuery.Query.Article
{
    public class ArticleCategoryQuery : IArticleCategoryQuery
    {
        private readonly ArticleContext _context;

        public ArticleCategoryQuery(ArticleContext context)
        {
            _context = context;
        }

        public List<ArticleCategoryQueryModel> GetAllArticleCategories()
        {
            return _context.ArticleCategories
                .Include(x => x.Articles)
                .Select(x => new ArticleCategoryQueryModel
                {
                    Name = x.Name,
                    Slug = x.Slug,
                    Picture = x.Picture,
                    PictureAlt = x.PictureAlt,
                    PictureTitle = x.PictureTitle,
                    ArticlesCount = x.Articles.Count
                }).ToList();
        }
    }
}
