using _01_OnlineShopQuery.Contracts.Product;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ServiceHost.Pages
{
    public class ProductModel : PageModel
    {
        private readonly IProductQuery _query;
        public ProductQueryModel Product { get; set; }

        public ProductModel(IProductQuery query)
        {
            _query = query;
        }

        public void OnGet(string id)
        {
            Product = _query.GetDetails(id);
        }
    }
}
