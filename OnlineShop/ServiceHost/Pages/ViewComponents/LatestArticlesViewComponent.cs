using _01_OnlineShopQuery.Contracts.Article;
using _01_OnlineShopQuery.Contracts.Product;
using _01_OnlineShopQuery.Contracts.ProductCategory;
using Microsoft.AspNetCore.Mvc;

namespace ServiceHost.Pages.ViewComponents
{
    public class LatestArticlesViewComponent : ViewComponent
    {
        private readonly IArticleQuery _query;

        public LatestArticlesViewComponent(IArticleQuery query)
        {
            _query = query;
        }

        public IViewComponentResult Invoke()
        {
            var query = _query.LatestArticles();

            return View(query);
        }
    }
}
