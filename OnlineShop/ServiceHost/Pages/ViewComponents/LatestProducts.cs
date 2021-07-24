using _01_OnlineShopQuery.Contracts.Product;
using _01_OnlineShopQuery.Contracts.ProductCategory;
using Microsoft.AspNetCore.Mvc;

namespace ServiceHost.Pages.ViewComponents
{
    public class LatestProducts : ViewComponent
    {
        private readonly IProductQuery _query;

        public LatestProducts(IProductQuery query)
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
