using System.Collections.Generic;

namespace _01_OnlineShopQuery.Contracts.ProductCategory
{
    public interface IProductCategoryQuery
    {
        List<ProductCategoryQueryModel> GetProductCategoryQueryModels();
    }
}