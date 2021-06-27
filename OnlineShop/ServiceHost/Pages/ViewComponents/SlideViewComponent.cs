using _01_OnlineShopQuery.Contracts.Slide;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewComponents;
using ShopManagement.Application.Contracts.Slider;

namespace ServiceHost.Pages.ViewComponents
{
    public class SlideViewComponent : ViewComponent
    {
        private readonly ISlideQuery _query;

        public SlideViewComponent(ISlideQuery query)
        {
            _query = query;
        }

        public IViewComponentResult Invoke()
        {
            var slides = _query.GetSlides();

            return View(slides);
        }
    }
}
