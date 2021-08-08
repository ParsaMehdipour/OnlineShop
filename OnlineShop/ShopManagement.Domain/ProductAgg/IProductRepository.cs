using System.Collections.Generic;
using _0_Framework.Domain;
using ShopManagement.Application.Contracts.Product;

namespace ShopManagement.Domain.ProductAgg
{
    public interface IProductRepository : IRepsoitory<long , Product>
    {
        Product GetProductWithCategory(long id);
        EditProduct GetDetails(long id);
        List<ProductViewModel> Search(ProductSearchModel searchModel);
        List<ProductViewModel> GetProducts();
    }
}
