using System.Collections.Generic;
using System.Linq;
using _0_Framework.Application;
using _01_OnlineShopQuery.Contracts.Article;
using ArticleManagement.Infrastructure.EfCore;
using Microsoft.EntityFrameworkCore;

namespace _01_OnlineShopQuery.Query.Article
{
    public class ArticleQuery : IArticleQuery
    {
        private readonly ArticleContext _context;

        public ArticleQuery(ArticleContext context)
        {
            _context = context;
        }

        public List<ArticleQueryModel> LatestArticles()
        {
            return _context.Articles
                .Include(x => x.ArticleCategory)
                .Select(x => new ArticleQueryModel
                {
                    Title = x.Title,
                    Slug = x.Slug,
                    Picture = x.Picture,
                    PictureAlt = x.PictureAlt,
                    PictureTitle = x.PictureTitle,
                    PublishDate = x.PublishDate.ToFarsi(),
                    ShortDescription = x.ShortDescription,
                    CategorySlug = x.ArticleCategory.Slug
                }).ToList();
        }

        public ArticleQueryModel GetArticle(string slug)
        {
            var article = _context.Articles
                .Include(x => x.ArticleCategory)
                .Select(x => new ArticleQueryModel
                {
                    Id = x.Id,
                    Title = x.Title,
                    Slug = x.Slug,
                    Picture = x.Picture,
                    PictureAlt = x.PictureAlt,
                    PictureTitle = x.PictureTitle,
                    Description = x.Description,
                    ArticleCategory = x.ArticleCategory.Name,
                    CategoryId = x.ArticleCategory.Id,
                    PublishDate = x.PublishDate.ToFarsi(),
                    ShortDescription = x.ShortDescription,
                    MetaDescription = x.MetaDescription,
                    Keywords = x.Keywords,
                    CanonicalAddress = x.CanonicalAddress,
                    CategorySlug = x.ArticleCategory.Slug
                }).FirstOrDefault(x => x.Slug == slug);

            if (!string.IsNullOrWhiteSpace(article.Keywords))
                article.KeywordsList = article.Keywords.Split(",").ToList();

            return article;
        }
    }
}
