using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ShopManagement.Application.Contracts.ProductCategory;

namespace ServiceHost.Areas.AdminPanel.Pages.Shop.ProductCategories
{
    public class IndexModel : PageModel
    {
        public ProductCategorySearchModel SearchModel;
        private readonly IProductCategoryApplication _application;
        public List<ProductCategoryViewModel> ProductCategories;

        public IndexModel(IProductCategoryApplication application)
        {
            _application = application;
        }

        public void OnGet(ProductCategorySearchModel searchModel)
        {
            ProductCategories = _application.Search(searchModel);
        }

        public IActionResult OnGetCreate()
        {
            return Partial("./Create", new CreateProductCategory());
        }

        public JsonResult OnPostCreate(CreateProductCategory command)
        {
            var result = _application.Create(command);

            return new JsonResult(result);
        }
    }
}
