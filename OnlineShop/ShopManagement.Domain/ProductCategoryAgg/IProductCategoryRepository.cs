using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using _0_Framework.Domain;
using ShopManagement.Application.Contracts.ProductCategory;

namespace ShopManagement.Domain.ProductCategoryAgg
{
    public interface IProductCategoryRepository : IRepsoitory<long, ProductCategory>
    {
        List<ProductCategoryViewModel> GetProductCategories();
        string GetSlugById(long id);
        EditProductCategory GetDetails(long id);
        List<ProductCategoryViewModel> Search(ProductCategorySearchModel searchModel);
    }
}
