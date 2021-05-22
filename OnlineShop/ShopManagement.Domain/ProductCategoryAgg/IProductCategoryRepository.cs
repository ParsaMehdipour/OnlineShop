using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using ShopManagement.Application.Contracts.ProductCategory;

namespace ShopManagement.Domain.ProductCategoryAgg
{
    public interface IProductCategoryRepository
    {
        void Create(ProductCategory productCategory);
        ProductCategory Get(long id);
        List<ProductCategory> GetAll();
        void Save();
        bool Exists(Expression<Func<ProductCategory, bool>> expression);
        EditProductCategory GetDetails(long id);
        List<ProductCategoryViewModel> Search(ProductCategorySearchModel seraModel);
    }
}
