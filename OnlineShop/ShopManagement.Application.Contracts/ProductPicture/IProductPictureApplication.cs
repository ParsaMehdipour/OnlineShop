using System.Collections.Generic;
using _0_Framework.Application;

namespace ShopManagement.Application.Contracts.ProductPicture
{
    public interface IProductPictureApplication
    {
        List<ProductPictureViewModel> Search(ProductPictureSearchModel pictureSearchModel);
        OperationResult Restore(long id);
        OperationResult Remove(long id);
        OperationResult Create(CreateProductPicture command);
        OperationResult EditProductPicture(EditProductPicture command);
        EditProductPicture GetDetails(long id);

    }
}
