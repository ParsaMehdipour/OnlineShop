namespace InventoryManagement.Application.Contracts.Inventory
{
    public class ReduceInventory
    {
        public long Count { get; set; }
        public long ProductId { get; set; }
        public long OperatorId { get; set; }
        public long OrderId { get; set; }
        public string Description { get; set; }
        public long InventoryId { get; set; }
    }
}