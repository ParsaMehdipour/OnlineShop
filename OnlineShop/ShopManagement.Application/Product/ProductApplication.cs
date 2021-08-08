using System.Collections.Generic;
using _0_Framework.Application;
using ShopManagement.Application.Contracts.Product;
using ShopManagement.Domain.ProductAgg;
using ShopManagement.Domain.ProductCategoryAgg;

namespace ShopManagement.Application.Product
{
    public class ProductApplication : IProductApplication
    {
        private readonly IProductRepository _repository;
        private readonly IProductCategoryRepository _productCategoryRepository;
        private readonly IFileUploader _fileUploader;

        public ProductApplication(IProductRepository repository,IProductCategoryRepository productCategoryRepository,IFileUploader fileUploader)
        {
            _repository = repository;
            _productCategoryRepository = productCategoryRepository;
            _fileUploader = fileUploader;
        }

        public OperationResult Create(CreateProduct command)
        {
            var result = new OperationResult();

            if (_repository.Exists(x => x.Name == command.Name))
                return result.Failed(ApplicationMessages.DuplicatedRecord);

            var slug = command.Slug.Slugify();

            var categorySlug = _productCategoryRepository.GetSlugById(command.CategoryId);

            var path = $"{categorySlug}/{slug}";

            var fileName = _fileUploader.Upload(command.Picture, path);

            var product = new Domain.ProductAgg.Product(command.Name, command.Code,
                command.ShortDescription, command.Description, fileName,
                command.PictureAlt, command.PictureTitle, slug,
                command.Keywords, command.MetaDescription, command.CategoryId);

            _repository.Create(product);

            _repository.SaveChanges();

            return result.Succedded();
        }

        public OperationResult Edit(EditProduct command)
        {
            var result = new OperationResult();

            var product = _repository.GetProductWithCategory(command.Id);

            if (product == null)
                return result.Failed(ApplicationMessages.RecordNotFound);

            if (_repository.Exists(x => x.Name == command.Name && x.Id != command.Id))
                return result.Failed();

            var slug = command.Slug.Slugify();

            var path = $"{product.ProductCategory.Slug}/{slug}";

            var fileName = _fileUploader.Upload(command.Picture, path);

            product.Edit(command.Name, command.Code,
                command.ShortDescription, command.Description, fileName,
                command.PictureAlt, command.PictureTitle, slug,
                command.Keywords, command.MetaDescription, command.CategoryId);

            _repository.SaveChanges();

            return result.Succedded();
        }

        public List<ProductViewModel> Search(ProductSearchModel searchModel)
        {
            return _repository.Search(searchModel);
        }

        public List<ProductViewModel> GetProducts()
        {
            return _repository.GetProducts();
        }

        public EditProduct GetDetails(long id)
        {
            return _repository.GetDetails(id);
        }
    }
}
