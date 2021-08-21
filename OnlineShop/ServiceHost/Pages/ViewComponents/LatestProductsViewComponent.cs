using _01_OnlineShopQuery.Contracts.Product;
using _01_OnlineShopQuery.Contracts.ProductCategory;
using Microsoft.AspNetCore.Mvc;

namespace ServiceHost.Pages.ViewComponents
{
    public class LatestProductsViewComponent : ViewComponent
    {
        private readonly IProductQuery _query;

        public LatestProductsViewComponent(IProductQuery query)
        {
            _query = query;
        }

        public IViewComponentResult Invoke()
        {
            var query = _query.GetLatestProducts();

            return View(query);
        }
    }
}
