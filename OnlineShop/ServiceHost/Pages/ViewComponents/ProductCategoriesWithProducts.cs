using _01_OnlineShopQuery.Contracts.ProductCategory;
using Microsoft.AspNetCore.Mvc;

namespace ServiceHost.Pages.ViewComponents
{
    public class ProductCategoriesWithProducts : ViewComponent
    {
        private readonly IProductCategoryQuery _query;

        public ProductCategoriesWithProducts(IProductCategoryQuery query)
        {
            _query = query;
        }

        public IViewComponentResult Invoke()
        {
            var query = _query.GetProductCategoryWithProducts();

            return View(query);
        }
    }
}
