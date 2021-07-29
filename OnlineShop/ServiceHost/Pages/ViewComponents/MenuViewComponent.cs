using _01_LampshadeQuery;
using _01_OnlineShopQuery.Contracts.ProductCategory;
using Microsoft.AspNetCore.Mvc;

namespace ServiceHost.Pages.ViewComponents
{
    public class MenuViewComponent : ViewComponent
    {
        private readonly IProductCategoryQuery _query;

        public MenuViewComponent(IProductCategoryQuery query)
        {
            _query = query;
        }

        public IViewComponentResult Invoke()
        {
            var result = new MenuModel()
            {
                ProductCategories = _query.GetProductCategoryQueryModels()
            };

            return View(result);
        }
    }
}
