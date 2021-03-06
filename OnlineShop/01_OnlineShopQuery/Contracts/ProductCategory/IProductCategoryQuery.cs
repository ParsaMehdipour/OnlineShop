using System.Collections.Generic;

namespace _01_OnlineShopQuery.Contracts.ProductCategory
{
    public interface IProductCategoryQuery
    {
        List<ProductCategoryQueryModel> GetProductCategoryQueryModels();
        List<ProductCategoryQueryModel> GetProductCategoryWithProducts();
        ProductCategoryQueryModel GetProductCategoryWithProducts(string slug);
    }
}