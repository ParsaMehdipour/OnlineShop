using System.Collections.Generic;

namespace ShopManagement.Domain.ProductCategoryAgg
{
    public interface IProductCategoryRepository
    {
        void Create(ProductCategory productCategory);
        ProductCategory Get(long id);
        List<ProductCategory> GetAll();
    }
}
