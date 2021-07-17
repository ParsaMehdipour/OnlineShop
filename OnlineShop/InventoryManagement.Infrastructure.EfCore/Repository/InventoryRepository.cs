using System.Collections.Generic;
using System.Linq;
using _0_Framework.Application;
using _0_Framework.Domain;
using _0_Framework.Infrastructure;
using InventoryManagement.Application.Contracts.Inventory;
using InventoryManagement.Domain.InventoryAgg;
using Microsoft.EntityFrameworkCore;
using ShopManagement.Infrastructure.EfCore;

namespace InventoryManagement.Infrastructure.EfCore.Repository
{
    public class InventoryRepository : BaseRepsitory<long , Inventory> , IInventoryRepository
    {
        private readonly InventoryContext _context;
        private readonly ShopContext _shopContext;

        public InventoryRepository(InventoryContext context,ShopContext shopContext) : base(context)
        {
            _context = context;
            _shopContext = shopContext;
        }

        public List<InventoryViewModel> Search(InventorySearchModel searchModel)
        {
            var products = _shopContext.Products.Select(x => new { x.Id, x.Name }).ToList();
            var query = _context.Inventory.Select(x => new InventoryViewModel
            {
                Id = x.Id,
                UnitPrice = x.UnitPrice,
                InStock = x.InStock,
                ProductId = x.ProductId,
                CurrentCount = x.CalculateCurrentCount(),
                CreationDate = x.CreationDate.ToFarsi()
            });

            if (searchModel.ProductId > 0)
                query = query.Where(x => x.ProductId == searchModel.ProductId);

            if (searchModel.InStock)
                query = query.Where(x => !x.InStock);

            var inventory = query.OrderByDescending(x => x.Id).ToList();

            inventory.ForEach(item =>
                item.Product = products.FirstOrDefault(x => x.Id == item.ProductId)?.Name);

            return inventory;
        }

        public Inventory GetByProductId(long productId)
        {
            return _context.Inventory.FirstOrDefault(x => x.ProductId == productId);
        }

        public EditInventory GetDetails(long id)
        {
            return _context.Inventory.Select(x => new EditInventory
            {
                Id = x.Id,
                ProductId = x.ProductId,
                UnitPrice = x.UnitPrice,
            }).FirstOrDefault(x => x.Id == id);
        }

        public List<InventoryOperationViewModel> GetLog(long id)
        {
            var inventory = _context.Inventory.FirstOrDefault(x => x.Id == id);

            return inventory.InventoryOperations.Select(x => new InventoryOperationViewModel
            {
                Id = x.Id,
                Operation = x.Operation,
                OperationDate = x.OperationDate.ToFarsi(),
                Operator = "مدیر سیستم",
                OperatorId = x.OperatorId,
                Description = x.Description,
                CurrentCount = x.CurrentCount,
                OrderId = x.OrderId,
                Count = x.Count,
                InventoryId = x.InventoryId
            }).OrderByDescending(x => x.Id).ToList();
        }
    }
}
