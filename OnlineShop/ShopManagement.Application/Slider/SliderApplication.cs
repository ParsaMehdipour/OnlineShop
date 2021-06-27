using System.Collections.Generic;
using _0_Framework.Application;
using ShopManagement.Application.Contracts.Slider;
using ShopManagement.Domain.SliderAgg;

namespace ShopManagement.Application.Slider
{
    public class SliderApplication : ISliderApplication
    {
        private readonly ISliderRepository _repository;

        public SliderApplication(ISliderRepository repository)
        {
            _repository = repository;
        }

        public OperationResult Create(CreateSlider command)
        {
            var result = new OperationResult();

            var slider = new Domain.SliderAgg.Slider(command.Picture, command.PictureAlt, command.PictureTitle
                , command.Heading, command.Title, command.Text
                , command.BtnText, command.Link);

            _repository.Create(slider);

            _repository.SaveChanges();

            return result.Succedded();
        }

        public OperationResult Edit(EditSlider command)
        {
            var result = new OperationResult();

            var slider = _repository.GetById(command.Id);

            if (slider == null)
                return result.Failed(ApplicationMessages.RecordNotFound);

            slider.Edit(command.Picture, command.PictureAlt, command.PictureTitle
            , command.Heading, command.Title, command.Text
            , command.BtnText, command.Link);

            _repository.SaveChanges();

            return result.Succedded();
        }

        public OperationResult Remove(long id)
        {
            var result = new OperationResult();

            var slider = _repository.GetById(id);

            if (slider == null)
                return result.Failed(ApplicationMessages.RecordNotFound);

            slider.Remove();

            _repository.SaveChanges();

            return result.Succedded();
        }

        public OperationResult Restore(long id)
        {
            var result = new OperationResult();

            var slider = _repository.GetById(id);

            if (slider == null)
                return result.Failed(ApplicationMessages.RecordNotFound);

            slider.Restore();

            _repository.SaveChanges();

            return result.Succedded();
        }

        public EditSlider GetDetails(long id)
        {
            return _repository.GetDetails(id);
        }

        public List<SliderViewModel> GetList()
        {
            return _repository.GetList();
        }
    }
}
