using _01_OnlineShopQuery.Contracts.ProductCategory;
using Microsoft.AspNetCore.Mvc;

namespace ServiceHost.Pages.ViewComponents
{
    public class ProductCategoryViewComponent : ViewComponent
    {
        private readonly IProductCategoryQuery _query;

        public ProductCategoryViewComponent(IProductCategoryQuery query)
        {
            _query = query;
        }

        public IViewComponentResult Invoke()
        {
            var productCategories = _query.GetProductCategoryQueryModels();

            return View(productCategories);
        }
    }
}
