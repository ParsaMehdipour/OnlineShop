using System.Collections.Generic;
using _01_OnlineShopQuery.Contracts.Article;
using _01_OnlineShopQuery.Contracts.ArticleCategory;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ServiceHost.Pages
{
    public class ArticleModel : PageModel
    {
        private readonly IArticleQuery _query;
        private readonly IArticleCategoryQuery _articleCategoryQuery;
        public ArticleQueryModel Article { get; set; }
        public List<ArticleQueryModel> Articles { get; set; }
        public List<ArticleCategoryQueryModel> ArticleCategories { get; set; }

        public ArticleModel(IArticleQuery query , IArticleCategoryQuery articleCategoryQuery)
        {
            _query = query;
            _articleCategoryQuery = articleCategoryQuery;
        }

        public void OnGet(string id)
        {
            Article = _query.GetArticle(id);
            ArticleCategories = _articleCategoryQuery.GetAllArticleCategories();
            Articles = _query.LatestArticles();
        }
    }
}
