using System.Collections.Generic;
using _0_Framework.Application;

namespace ShopManagement.Application.Contracts.Product
{
    public interface IProductApplication
    {
        OperationResult Create(CreateProduct command);
        OperationResult Edit(EditProduct command);
        List<ProductViewModel> Search(ProductSearchModel searchModel);
        List<ProductViewModel> GetProducts();
        EditProduct GetDetails(long id);
        OperationResult InStock(long id);
        OperationResult OutOfStock(long id);
    }
}
