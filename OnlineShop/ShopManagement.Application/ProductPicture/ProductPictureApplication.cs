using System.Collections.Generic;
using _0_Framework.Application;
using _0_Framework.Domain;
using Microsoft.VisualBasic.CompilerServices;
using ShopManagement.Application.Contracts.ProductPicture;
using ShopManagement.Domain.ProductAgg;
using ShopManagement.Domain.ProductPictureAgg;

namespace ShopManagement.Application.ProductPicture
{
    public class ProductPictureApplication : IProductPictureApplication
    {
        private readonly IProductPictureRepository _repository;
        private readonly IProductRepository _productRepository;
        private readonly IFileUploader _fileUploader;

        public ProductPictureApplication(IProductPictureRepository repository, IProductRepository productRepository, IFileUploader fileUploader)
        {
            _repository = repository;
            _productRepository = productRepository;
            _fileUploader = fileUploader;
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

            //if (_repository.Exists(p => p.Picture == command.Picture && p.ProductId == command.ProductId))
            //    return operationResult.Failed(ApplicationMessages.DuplicatedRecord);

            var product = _productRepository.GetProductWithCategory(command.ProductId);

            var path = $"{product.ProductCategory.Slug}//{product.Slug}";
            var fileName = _fileUploader.Upload(command.Picture, path);

            var productPicture = new Domain.ProductPictureAgg.ProductPicture(command.ProductId, fileName
                , command.PictureAlt, command.PictureTitle);

            _repository.Create(productPicture);

            _repository.SaveChanges();

            return operationResult.Succedded();
        }

        public OperationResult Edit(EditProductPicture command)
        {
            var operationResult = new OperationResult();

            var productPicture = _repository.GetWithProductAndCategory(command.Id);

            if (productPicture == null)
                return operationResult.Failed(ApplicationMessages.RecordNotFound);

            //if (_repository.Exists(p =>
            //    p.Picture == command.Picture
            //    && p.ProductId == command.ProductId
            //    && p.Id != command.Id))
            //    return operationResult.Failed(ApplicationMessages.DuplicatedRecord);


            var path = $"{productPicture.Product.ProductCategory.Slug}//{productPicture.Product.Slug}";
            var fileName = _fileUploader.Upload(command.Picture, path);

            productPicture.Edit(command.ProductId, fileName
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
