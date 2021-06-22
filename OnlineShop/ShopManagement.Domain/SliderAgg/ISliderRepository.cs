using System.Collections.Generic;
using _0_Framework.Domain;
using ShopManagement.Application.Contracts.Slider;

namespace ShopManagement.Domain.SliderAgg
{
    public interface ISliderRepository : IRepsoitory<long,Slider>
    {
        EditSlider GetDetails(long id);
        List<SliderViewModel> GetList();
    }
}
