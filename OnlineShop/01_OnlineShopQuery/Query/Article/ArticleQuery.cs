﻿using System.Collections.Generic;
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
                }).ToList();
        }
    }
}