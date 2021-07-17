using System.Collections.Generic;
using DiscountManagement.Application.Contracts.ColleagueDiscount;
using DiscountManagement.Application.Contracts.CustomerDiscount;
using DiscountManagement.Domain.CustomerDiscountAgg;
using InventoryManagement.Application.Contracts.Inventory;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using ShopManagement.Application.Contracts.Product;
using ShopManagement.Application.Contracts.ProductCategory;

namespace ServiceHost.Areas.AdminPanel.Pages.Inventory
{
    public class IndexModel : PageModel
    {
        [TempData]
        public string Message { get; set; }
        public InventorySearchModel SearchModel;
        private readonly IInventoryApplication _application;
        private readonly IProductApplication _productApplication;
        public List<InventoryViewModel> InventoryViewModels;
        public SelectList Products;

        public IndexModel(IProductApplication productApplication,IInventoryApplication application)
        {
            _application = application;
            _productApplication = productApplication;
        }

        public void OnGet(InventorySearchModel searchModel)
        {
            Products = new SelectList(_productApplication.GetProducts(), "Id", "Name");
            InventoryViewModels = _application.Search(searchModel);
        }

        public IActionResult OnGetCreate()
        {
            var command = new CreateInventory{Products = _productApplication.GetProducts()};

            return Partial("./Create", command);
        }

        public JsonResult OnPostCreate(CreateInventory command)
        {
            var result = _application.Create(command);

            return new JsonResult(result);
        }

        public IActionResult OnGetEdit(long id)
        {
            var inventory = _application.GetDetails(id);
            inventory.Products = _productApplication.GetProducts();

            return Partial("./Edit", inventory);
        }

        public JsonResult OnPostEdit(EditInventory command)
        {
            var result = _application.Edit(command);

            return new JsonResult(result);
        }

        public IActionResult OnGetIncrease(long id)
        {
            var command = new IncreaseInventory()
            {
                InventoryId = id
            };

            return Partial("Increase", command);
        }

        public JsonResult OnPostIncrease(IncreaseInventory command)
        {
            var result = _application.Increase(command);

            return new JsonResult(result);
        }


        public IActionResult OnGetReduce(long id)
        {
            var command = new ReduceInventory()
            {
                InventoryId = id
            };

            return Partial("Reduce", command);
        }
        public JsonResult OnPostReduce(ReduceInventory command)
        {
            var result = _application.Reduce(command);

            return new JsonResult(result);
        }

        public IActionResult OnGetLog(long id)
        {
            var log = _application.GetLog(id);

            return Partial("OperationLog", log);
        }
    }
}
