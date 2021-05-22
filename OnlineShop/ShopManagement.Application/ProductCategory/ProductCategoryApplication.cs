using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Xml.XPath;
using _0_Framework.Application;
using ShopManagement.Application.Contracts.ProductCategory;
using ShopManagement.Domain.ProductCategoryAgg;

namespace ShopManagement.Application.ProductCategory
{
    public class ProductCategoryApplication : IProductCategoryApplication
    {
        private readonly IProductCategoryRepository _repository;

        public ProductCategoryApplication(IProductCategoryRepository repository)
        {
            _repository = repository;
        }

        public OperationResult Create(CreateProductCategory command)
        {
            var result = new OperationResult();

            if (_repository.Exists(x => x.Name == command.Name))
                return result.Failed("امکان ایجاد کاربر تکراری وجود ندارد. لظفا دوباره تلاش کنید");

            var slug = command.Slug.Slugify();

            var productCategory = new Domain.ProductCategoryAgg.ProductCategory(command.Name,command.Description,command.Picture
            ,command.PictureAlt,command.PictureTitle,command.KeyWords,command.MetaDescription,slug);

            _repository.Create(productCategory);

            _repository.Save();

            return result.Succedded();
        }

        public OperationResult Edit(EditProductCategory command)
        {
            var result = new OperationResult();

            var productCategory = _repository.Get(command.Id);

            if (productCategory == null)
                return result.Failed("رکورد با اطلاعات درخواست داده شده وجود ندارد. لطفا دوباره تلاش کنید");

            if (_repository.Exists(x => x.Name == command.Name && x.Id != command.Id))
                return result.Failed();

            var slug = command.Slug.Slugify();

            productCategory.Edit(command.Name, command.Description, command.Picture
                ,command.PictureAlt, command.PictureTitle, command.KeyWords, command.MetaDescription, slug);

            _repository.Save();

            return result.Succedded();
        }

        public EditProductCategory GetDetails(long id)
        {
            return _repository.GetDetails(id);
        }

        public List<ProductCategoryViewModel> Search(ProductCategorySearchModel searchModel)
        {
            return _repository.Search(searchModel);
        }
    }
}
