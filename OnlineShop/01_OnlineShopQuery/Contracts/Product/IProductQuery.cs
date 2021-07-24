using System.Collections.Generic;

namespace _01_OnlineShopQuery.Contracts.Product
{
    public interface IProductQuery
    {
        List<ProductQueryModel> GetLatestProducts();
    }
}
