using System.Collections.Generic;
using ArticleManagement.Application.Contracts.Article;
using ArticleManagement.Application.Contracts.ArticleCategory;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ServiceHost.Areas.AdminPanel.Pages.Blog.Articles
{
    public class IndexModel : PageModel
    {
        public ArticleSearchModel SearchModel;
        private readonly IArticleApplication _application;
        private readonly IArticleCategoryApplication _articleCategoryApplication;
        public List<ArticleViewModel> Articles;
        public SelectList ArticleCategories { get; set; }

        public IndexModel(IArticleApplication application,IArticleCategoryApplication articleCategoryApplication)
        {
            _application = application;
            _articleCategoryApplication = articleCategoryApplication;
        }

        public void OnGet(ArticleSearchModel searchModel)
        {
            ArticleCategories = new SelectList(_articleCategoryApplication.GetArticleCategories(), "Id", "Name");
            Articles = _application.Search(searchModel);
        }

        public IActionResult OnGetCreate()
        {
            return Partial("./Create", new CreateArticle());
        }

        public JsonResult OnPostCreate(CreateArticle command)
        {
            var result = _application.Create(command);

            return new JsonResult(result);
        }

        public IActionResult OnGetEdit(long id)
        {
            var article = _application.GetDetails(id);

            return Partial("./Edit", article);
        }

        public JsonResult OnPostEdit(EditArticle command)
        {
            var result = _application.Edit(command);

            return new JsonResult(result);
        }
    }
}
