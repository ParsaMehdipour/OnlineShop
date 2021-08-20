using System;
using System.Collections.Generic;
using System.Linq;
using _0_Framework.Application;
using _0_Framework.Infrastructure;
using ArticleManagement.Application.Contracts.Article;
using ArticleManagement.Domain.ArticleAgg;
using Microsoft.EntityFrameworkCore;

namespace ArticleManagement.Infrastructure.EfCore.Repository
{
    public class ArticleRepository : BaseRepsitory<long, Article>, IArticleRepository

    {
        private readonly ArticleContext _context;

        public ArticleRepository(ArticleContext context) : base(context)
        {
            _context = context;
        }

        public EditArticle GetDetails(long id)
        {
            return _context.Articles.Select(x => new EditArticle
            {
                Id = x.Id,
                CanonicalAddress = x.CanonicalAddress,
                CategoryId = x.CategoryId,
                Description = x.Description,
                Keywords = x.Keywords,
                MetaDescription = x.MetaDescription,
                PictureAlt = x.PictureAlt,
                PictureTitle = x.PictureTitle,
                PublishDate = x.PublishDate.ToFarsi(),
                ShortDescription = x.ShortDescription,
                Slug = x.Slug,
                Title = x.Title
            }).FirstOrDefault(x => x.Id == id);
        }

        public Article GetWithArticleCategory(long id)
        {
            return _context.Articles
                .Include(x => x.ArticleCategory).FirstOrDefault(x => x.Id == id);
        }

        public List<ArticleViewModel> Search(ArticleSearchModel searchModel)
        {
            var query = _context.Articles.Select(x => new ArticleViewModel
            {
                Id = x.Id,
                CategoryId = x.CategoryId,
                Category = x.ArticleCategory.Name,
                Picture = x.Picture,
                PublishDate = x.PublishDate.ToFarsi(),
                CreationDate = x.CreationDate.ToFarsi(),
                ShortDescription = x.ShortDescription,
                Title = x.Title
            });

            if (!string.IsNullOrWhiteSpace(searchModel.Title))
                query = query.Where(x => x.Title.Contains(searchModel.Title));

            if (searchModel.CategoryId > 0)
                query = query.Where(x => x.CategoryId == searchModel.CategoryId);

            return query.OrderByDescending(x => x.Id).ToList();
        }
    }
}
