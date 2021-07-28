using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using _01_OnlineShopQuery.Contracts.ProductCategory;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ServiceHost.Pages
{
    public class ProductCategoryModel : PageModel
    {
        private readonly IProductCategoryQuery _query;
        public ProductCategoryQueryModel ProductCategory { get; set; }

        public ProductCategoryModel(IProductCategoryQuery query)
        {
            _query = query;
        }
        public void OnGet(string id)
        {
            ProductCategory = _query.GetProductCategoryWithProducts(id);
        }
    }
}
