using System.Collections.Generic;
using _0_Framework.Domain;
using InventoryManagement.Application.Contracts.Inventory;

namespace InventoryManagement.Domain.InventoryAgg
{
    public interface IInventoryRepository : IRepsoitory<long, Inventory>
    {
        List<InventoryViewModel> Search(InventorySearchModel searchModel);
        Inventory GetByProductId(long productId);
        EditInventory GetDetails(long id);
        List<InventoryOperationViewModel> GetLog(long id);
    }
}
