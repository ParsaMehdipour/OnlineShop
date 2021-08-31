using System.Collections.Generic;
using _01_OnlineShopQuery.Contracts.Article;
using _01_OnlineShopQuery.Contracts.ArticleCategory;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ServiceHost.Pages
{
    public class ArticleCategoryModel : PageModel
    {
        private readonly IArticleCategoryQuery _query;
        private readonly IArticleQuery _articleQuery;
        public ArticleCategoryQueryModel ArticleCategory { get; set; }
        public List<ArticleCategoryQueryModel> ArticleCategories { get; set; }
        public List<ArticleQueryModel> LatestArticles { get; set; }
        public ArticleCategoryModel(IArticleCategoryQuery query , IArticleQuery articleQuery)
        {
            _query = query;
            _articleQuery = articleQuery;
        }
        public void OnGet(string id)
        {
            ArticleCategory = _query.GetArticleCategory(id);
            ArticleCategories = _query.GetAllArticleCategories();
            LatestArticles = _articleQuery.LatestArticles();
        }
    }
}
