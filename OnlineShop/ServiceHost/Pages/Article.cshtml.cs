using System.Collections.Generic;
using _01_OnlineShopQuery.Contracts.Article;
using _01_OnlineShopQuery.Contracts.ArticleCategory;
using CommentManagement.Application.Contracts.Comment;
using CommentManagement.Infrastructure.EfCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ServiceHost.Pages
{
    public class ArticleModel : PageModel
    {
        private readonly IArticleQuery _query;
        private readonly IArticleCategoryQuery _articleCategoryQuery;
        private readonly ICommentApplication _commentApplication;
        public ArticleQueryModel Article { get; set; }
        public List<ArticleQueryModel> Articles { get; set; }
        public List<ArticleCategoryQueryModel> ArticleCategories { get; set; }

        public ArticleModel(IArticleQuery query , IArticleCategoryQuery articleCategoryQuery,ICommentApplication commentApplication)
        {
            _query = query;
            _articleCategoryQuery = articleCategoryQuery;
            _commentApplication = commentApplication;
        }

        public void OnGet(string id)
        {
            Article = _query.GetArticle(id);
            ArticleCategories = _articleCategoryQuery.GetAllArticleCategories();
            Articles = _query.LatestArticles();
        }

        public IActionResult OnPost(CreateComment command, string slug)
        {
            command.Type = CommentType.Article;
            _commentApplication.Create(command);
            return new RedirectToPageResult("/Article", new { Id = slug });

        }
    }
}
