using System.Collections.Generic;
using System.Linq;
using _0_Framework.Domain;
using InventoryManagement.Domain.Operation;

namespace InventoryManagement.Domain.InventoryAgg
{
    public class Inventory : BaseEntity
    {
        public long ProductId { get; private set; } //Which Product ?
        public double UnitPrice { get; private set; }
        public bool InStock { get; private set; }
        public List<InventoryOperation> InventoryOperations { get; private set; }

        public Inventory()
        {
            
        }

        public Inventory(long productId, double unitPrice)
        {
            ProductId = productId;
            UnitPrice = unitPrice;
            InStock = false;
            InventoryOperations = new List<InventoryOperation>();
        }

        public void Edit(long productId, double unitPrice)
        {
            ProductId = productId;
            UnitPrice = unitPrice;
        }

        public long CalculateCurrentCount()
        {
            var plus = InventoryOperations.Where(x => x.Operation is true).Sum(x => x.Count);
            var minus = InventoryOperations.Where(x => x.Operation is false).Sum(x => x.Count);

            return (plus - minus);
        }

        public void Increase(long count, long operatorId, string description)
        {
            var currentCount = CalculateCurrentCount() + count;

            var inventoryOperation = new InventoryOperation(true, count, operatorId, currentCount, description, 0, Id);

            InventoryOperations.Add(inventoryOperation);

            InStock = currentCount > 0;
        }

        public void Reduce(long count, long operatorId, long orderId, string description)
        {
            var currentCount = CalculateCurrentCount() - count;

            var inventoryOperation = new InventoryOperation(true, count, operatorId, currentCount, description, orderId, Id);

            InventoryOperations.Add(inventoryOperation);

            InStock = currentCount > 0;
        }
    }
}
