using System;
using InventoryManagement.Domain.InventoryAgg;

namespace InventoryManagement.Domain.Operation
{
    public class InventoryOperation
    {
        public long Id { get; private set; }
        public bool Operation { get; private set; } //Increase or Decrease ?
        public long Count { get; private set; } //How Many Increase or Decrease ?
        public long OperatorId { get; private set; } //Who Is Doing The Operation ?
        public DateTime OperationDate { get; private set; }
        public long CurrentCount { get; private set; } //How Many Is In Inventory Right Now ?
        public string Description { get; private set; }
        public long OrderId { get; private set; } //Which Order ?
        public long InventoryId { get; private set; } //Navigation property
        public Inventory Inventory { get; private set; }

        public InventoryOperation(bool operation, long count, long operatorId, long currentCount
            , string description, long orderId, long inventoryId)
        {
            Operation = operation;
            Count = count;
            OperatorId = operatorId;
            CurrentCount = currentCount;
            Description = description;
            OrderId = orderId;
            InventoryId = inventoryId;
            OperationDate = DateTime.Now;
        }
    }
}