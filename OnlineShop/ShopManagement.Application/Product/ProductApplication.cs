using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;
using _0_Framework.Application;
using ShopManagement.Application.Contracts.Product;
using ShopManagement.Domain.ProductAgg;

namespace ShopManagement.Application.Product
{
    public class ProductApplication : IProductApplication
    {
        private readonly IProductRepository _repository;

        public ProductApplication(IProductRepository repository)
        {
            _repository = repository;
        }

        public OperationResult Create(CreateProduct command)
        {
            var result = new OperationResult();

            if (_repository.Exists(x => x.Name == command.Name))
                return result.Failed(ApplicationMessages.DuplicatedRecord);

            var slug = command.Slug.Slugify();

            var product = new Domain.ProductAgg.Product(command.Name, command.Code, command.UnitPrice,
                command.ShortDescription, command.Description, command.Picture,
                command.PictureAlt, command.PictureTitle, slug,
                command.Keywords, command.MetaDescription, command.CategoryId);

            _repository.Create(product);

            _repository.SaveChanges();

            return result.Succedded();
        }

        public OperationResult Edit(EditProduct command)
        {
            var result = new OperationResult();

            var product = _repository.GetById(command.Id);

            if (product == null)
                return result.Failed(ApplicationMessages.RecordNotFound);

            if (_repository.Exists(x => x.Name == command.Name && x.Id != command.Id))
                return result.Failed();

            var slug = command.Slug.Slugify();

            product.Edit(command.Name, command.Code, command.UnitPrice,
                command.ShortDescription, command.Description, command.Picture,
                command.PictureAlt, command.PictureTitle, slug,
                command.Keywords, command.MetaDescription, command.CategoryId);

            _repository.SaveChanges();

            return result.Succedded();
        }

        public List<ProductViewModel> Search(ProductSearchModel searchModel)
        {
            return _repository.Search(searchModel);
        }

        public EditProduct GetDetails(long id)
        {
            return _repository.GetDetails(id);
        }

        public OperationResult InStock(long id)
        {
            var result = new OperationResult();

            var product = _repository.GetById(id);

            if (product == null)
                return result.Failed(ApplicationMessages.RecordNotFound);

            product.IsInStock();

            _repository.SaveChanges();

            return result.Succedded();
        }

        public OperationResult OutOfStock(long id)
        {
            var result = new OperationResult();

            var product = _repository.GetById(id);

            if (product == null)
                return result.Failed(ApplicationMessages.RecordNotFound);

            product.NotInStock();

            _repository.SaveChanges();

            return result.Succedded();
        }
    }
}
