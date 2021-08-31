using System.Collections.Generic;
using System.Linq;
using _0_Framework.Application;
using _01_OnlineShopQuery.Contracts.Article;
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

        public ArticleCategoryQueryModel GetArticleCategory(string slug)
        {
            var articleCategory = _context.ArticleCategories
                .Include(x => x.Articles)
                .Select(x => new ArticleCategoryQueryModel
                {
                    Slug = x.Slug,
                    Name = x.Name,
                    Description = x.Description,
                    Picture = x.Picture,
                    PictureAlt = x.PictureAlt,
                    PictureTitle = x.PictureTitle,
                    Keywords = x.Keywords,
                    MetaDescription = x.MetaDescription,
                    CanonicalAddress = x.CanonicalAddress,
                    ArticlesCount = x.Articles.Count,
                    Articles = MapToArticles(x.Articles)
                }).FirstOrDefault(x => x.Slug == slug);

            if (!string.IsNullOrWhiteSpace(articleCategory.Keywords))
                articleCategory.KeywordsList = articleCategory.Keywords.Split(",").ToList();

            return articleCategory;
        }

        private static List<ArticleQueryModel> MapToArticles(List<ArticleManagement.Domain.ArticleAgg.Article> articles)
        {
            return articles.Select(x => new ArticleQueryModel
            {
                Slug = x.Slug,
                ShortDescription = x.ShortDescription,
                Title = x.Title,
                Picture = x.Picture,
                PictureAlt = x.PictureAlt,
                PictureTitle = x.PictureTitle,
                PublishDate = x.PublishDate.ToFarsi(),
            }).ToList();
        }
    }
}
