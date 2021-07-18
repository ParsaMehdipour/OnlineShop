using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using ShopManagement.Application.Contracts.Product;
using ShopManagement.Application.Contracts.ProductCategory;

namespace ServiceHost.Areas.AdminPanel.Pages.Shop.Products
{
    public class IndexModel : PageModel
    {
        [TempData]
        public string Message { get; set; }
        public ProductSearchModel SearchModel;
        private readonly IProductApplication _application;
        private readonly IProductCategoryApplication _productCategoryApplication;
        public List<ProductViewModel> Products;
        public SelectList ProductCategories;

        public IndexModel(IProductApplication application, IProductCategoryApplication productCategoryApplication)
        {
            _application = application;
            _productCategoryApplication = productCategoryApplication;
        }

        public void OnGet(ProductSearchModel searchModel)
        {
            ProductCategories = new SelectList(_productCategoryApplication.GetProductCategories(), "Id", "Name");
            Products = _application.Search(searchModel);
        }

        public IActionResult OnGetCreate()
        {
            var command = new CreateProduct {Categories = _productCategoryApplication.GetProductCategories()};

            return Partial("./Create", command);
        }

        public JsonResult OnPostCreate(CreateProduct command)
        {
            var result = _application.Create(command);

            return new JsonResult(result);
        }

        public IActionResult OnGetEdit(long id)
        {
            var product = _application.GetDetails(id);
            product.Categories = _productCategoryApplication.GetProductCategories();

            return Partial("./Edit", product);
        }

        public JsonResult OnPostEdit(EditProduct command)
        {
            var result = _application.Edit(command);

            return new JsonResult(result);
        }
    }
}
