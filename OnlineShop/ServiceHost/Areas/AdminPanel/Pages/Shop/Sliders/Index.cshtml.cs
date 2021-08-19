using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using ShopManagement.Application.Contracts.Product;
using ShopManagement.Application.Contracts.ProductCategory;
using ShopManagement.Application.Contracts.ProductPicture;
using ShopManagement.Application.Contracts.Slider;

namespace ServiceHost.Areas.AdminPanel.Pages.Shop.Sliders
{
    public class IndexModel : PageModel
    {

        [TempData]
        public string Message { get; set; }
        public List<SliderViewModel> Sliders;

        private readonly ISliderApplication _application;

        public IndexModel(ISliderApplication application)
        {
            _application = application;
        }

        public void OnGet()
        {
            Sliders = _application.GetList();
        }

        public IActionResult OnGetCreate()
        {
            var command = new CreateSlider();

            return Partial("./Create", command);
        }

        public JsonResult OnPostCreate(CreateSlider command)
        {
            var result = _application.Create(command);

            return new JsonResult(result);
        }

        public IActionResult OnGetEdit(long id)
        {
            var slider = _application.GetDetails(id);

            return Partial("./Edit", slider);
        }

        public JsonResult OnPostEdit(EditSlider command)
        {
            var result = _application.Edit(command);

            return new JsonResult(result);
        }


        public IActionResult OnGetRemove(long id)
        {
            var result = _application.Remove(id);
            if (result.Succeeded)
                return RedirectToPage("./Index");

            Message = result.Message;
            return RedirectToPage("./Index");
        }

        public IActionResult OnGetRestore(long id)
        {
            var result = _application.Restore(id);
            if (result.Succeeded)
                return RedirectToPage("./Index");

            Message = result.Message;
            return RedirectToPage("./Index");
        }
    }
}
