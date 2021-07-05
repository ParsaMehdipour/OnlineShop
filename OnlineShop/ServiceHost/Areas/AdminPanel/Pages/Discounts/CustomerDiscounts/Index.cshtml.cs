using System.Collections.Generic;
using DiscountManagement.Application.Contracts.CustomerDiscount;
using DiscountManagement.Domain.CustomerDiscountAgg;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using ShopManagement.Application.Contracts.Product;
using ShopManagement.Application.Contracts.ProductCategory;

namespace ServiceHost.Areas.AdminPanel.Pages.Discounts.CustomerDiscounts
{
    public class IndexModel : PageModel
    {
        [TempData]
        public string Message { get; set; }
        public CustomerDiscountSearchModel SearchModel;
        private readonly ICustomerDiscountApplication _application;
        private readonly IProductApplication _productApplication;
        public List<CustomerDiscountViewModel> CustomerDiscounts;
        public SelectList Products;

        public IndexModel(IProductApplication productApplication,ICustomerDiscountApplication application)
        {
            _application = application;
            _productApplication = productApplication;
        }

        public void OnGet(CustomerDiscountSearchModel searchModel)
        {
            Products = new SelectList(_productApplication.GetProducts(), "Id", "Name");
            CustomerDiscounts = _application.Search(searchModel);
        }

        public IActionResult OnGetCreate()
        {
            var command = new DefineCustomerDiscount{Products = _productApplication.GetProducts()};

            return Partial("./Create", command);
        }

        public JsonResult OnPostCreate(DefineCustomerDiscount command)
        {
            var result = _application.Define(command);

            return new JsonResult(result);
        }

        public IActionResult OnGetEdit(long id)
        {
            var customerDiscount = _application.GetDetails(id);
            customerDiscount.Products = _productApplication.GetProducts();

            return Partial("./Edit", customerDiscount);
        }

        public JsonResult OnPostEdit(EditCustomerDiscount command)
        {
            var result = _application.Edit(command);

            return new JsonResult(result);
        }
    }
}
