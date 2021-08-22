using System.Collections.Generic;

namespace _01_OnlineShopQuery.Contracts.Product
{
    public interface IProductQuery
    {
        ProductQueryModel GetDetails(string slug);
        List<ProductQueryModel> GetLatestProducts();
        List<ProductQueryModel> Search(string value);
    }
}
