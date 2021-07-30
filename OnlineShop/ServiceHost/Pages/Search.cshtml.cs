using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using _01_OnlineShopQuery.Contracts.Product;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ServiceHost.Pages
{
    public class SearchModel : PageModel
    {
        private readonly IProductQuery _query;
        public List<ProductQueryModel> Products { get; set; }
        public string Value { get; set; }

        public SearchModel(IProductQuery query)
        {
            _query = query;
        }

        public void OnGet(string value)
        {
            Value = value;
            Products = _query.Search(value);
        }
    }
}
