using System.Collections.Generic;
using _0_Framework.Application;
using _0_Framework.Domain;
using ShopManagement.Application.Contracts.ProductPicture;
using ShopManagement.Domain.ProductPictureAgg;

namespace ShopManagement.Application.ProductPicture
{
    public class ProductPictureApplication : IProductPictureApplication
    {
        private readonly IProductPictureRepository _repository;

        public ProductPictureApplication(IProductPictureRepository repository)
        {
            _repository = repository;
        }

        public List<ProductPictureViewModel> Search(ProductPictureSearchModel pictureSearchModel)
        {
            return _repository.Search(pictureSearchModel);
        }

        public OperationResult Restore(long id)
        {
            var operationResult = new OperationResult();

            var productPicture = _repository.GetById(id);

            if (productPicture == null)
                return operationResult.Failed(ApplicationMessages.RecordNotFound);

            productPicture.Restore();

            _repository.SaveChanges();

            return operationResult.Succedded();
        }

        public OperationResult Remove(long id)
        {
            var operationResult = new OperationResult();

            var productPicture = _repository.GetById(id);

            if (productPicture == null)
                return operationResult.Failed(ApplicationMessages.RecordNotFound);

            productPicture.Remove();

            _repository.SaveChanges();

            return operationResult.Succedded();
        }

        public OperationResult Create(CreateProductPicture command)
        {
            var operationResult = new OperationResult();

            if (_repository.Exists(p => p.Picture == command.Picture && p.ProductId == command.ProductId))
                return operationResult.Failed(ApplicationMessages.DuplicatedRecord);

            var productPicture = new Domain.ProductPictureAgg.ProductPicture(command.ProductId, command.Picture
                , command.PictureAlt, command.PictureTitle);
            
            _repository.Create(productPicture);

            _repository.SaveChanges();

            return operationResult.Succedded();
        }

        public OperationResult EditProductPicture(EditProductPicture command)
        {
            var operationResult = new OperationResult();

            var productPicture = _repository.GetById(command.Id);

            if (productPicture == null)
                return operationResult.Failed(ApplicationMessages.RecordNotFound);

            if (_repository.Exists(p => p.Picture == command.Picture && p.ProductId == command.ProductId))
                return operationResult.Failed(ApplicationMessages.DuplicatedRecord);

            productPicture.Edit(command.ProductId, command.Picture
                , command.PictureAlt, command.PictureTitle);

            _repository.SaveChanges();

            return operationResult.Succedded();
        }

        public EditProductPicture GetDetails(long id)
        {
            return _repository.GetDetails(id);
        }
    }
}
