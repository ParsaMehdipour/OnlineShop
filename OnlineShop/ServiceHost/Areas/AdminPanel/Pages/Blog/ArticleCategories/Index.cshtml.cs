using System.Collections.Generic;
using ArticleManagement.Application.Contracts.ArticleCategory;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ServiceHost.Areas.AdminPanel.Pages.Blog.ArticleCategories
{
    public class IndexModel : PageModel
    {
        public ArticleCategorySearchModel SearchModel;
        private readonly IArticleCategoryApplication _application;
        public List<ArticleCategoryViewModel> ArticleCategories;

        public IndexModel(IArticleCategoryApplication application)
        {
            _application = application;
        }

        public void OnGet(ArticleCategorySearchModel searchModel)
        {
            ArticleCategories = _application.Search(searchModel);
        }

        public IActionResult OnGetCreate()
        {
            return Partial("./Create", new CreateArticleCategory());
        }

        public JsonResult OnPostCreate(CreateArticleCategory command)
        {
            var result = _application.Create(command);

            return new JsonResult(result);
        }

        public IActionResult OnGetEdit(long id)
        {
            var articleCategory = _application.GetDetails(id);

            return Partial("./Edit", articleCategory);
        }

        public JsonResult OnPostEdit(EditArticleCategory command)
        {
            var result = _application.Edit(command);

            return new JsonResult(result);
        }
    }
}
