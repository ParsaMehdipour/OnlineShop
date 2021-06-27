using System.Collections.Generic;
using System.Linq;
using ShopManagement.Infrastructure.EfCore;

namespace _01_OnlineShopQuery.Contracts.Slide
{
    public class SlideQuery : ISlideQuery
    {
        private readonly ShopContext _context;

        public SlideQuery(ShopContext context)
        {
            _context = context;
        }
        public List<SlideQueryModel> GetSlides()
        {
            return _context.Sliders.Where(s => s.IsRemoved == false)
                .Select(s => new SlideQueryModel
                {
                    Title = s.Title,
                    Picture = s.Picture,
                    PictureAlt = s.PictureAlt,
                    PictureTitle = s.PictureTitle,
                    BtnText = s.BtnText,
                    Text = s.Text,
                    Heading = s.Heading,
                    Link = s.Link
                }).ToList();
        }
    }
}