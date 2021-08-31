using _01_LampshadeQuery;
using _01_OnlineShopQuery.Contracts.ArticleCategory;
using _01_OnlineShopQuery.Contracts.ProductCategory;
using Microsoft.AspNetCore.Mvc;

namespace ServiceHost.Pages.ViewComponents
{
    public class MenuViewComponent : ViewComponent
    {
        private readonly IProductCategoryQuery _query;
        private readonly IArticleCategoryQuery _articleCategoryQuery;

        public MenuViewComponent(IProductCategoryQuery query,IArticleCategoryQuery articleCategoryQuery)
        {
            _query = query;
            _articleCategoryQuery = articleCategoryQuery;
        }

        public IViewComponentResult Invoke()
        {
            var result = new MenuModel()
            {
                ProductCategories = _query.GetProductCategoryQueryModels(),
                ArticleCategories = _articleCategoryQuery.GetAllArticleCategories()
            };

            return View(result);
        }
    }
}
