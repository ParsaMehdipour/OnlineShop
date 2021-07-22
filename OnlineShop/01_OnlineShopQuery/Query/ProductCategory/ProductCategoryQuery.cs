using System.Collections.Generic;
using System.Linq;
using _0_Framework.Application;
using _01_OnlineShopQuery.Contracts.Product;
using _01_OnlineShopQuery.Contracts.ProductCategory;
using InventoryManagement.Infrastructure.EfCore;
using Microsoft.EntityFrameworkCore;
using ShopManagement.Domain.ProductAgg;
using ShopManagement.Infrastructure.EfCore;

namespace _01_OnlineShopQuery.Query.ProductCategory
{
    public class ProductCategoryQuery : IProductCategoryQuery
    {
        private readonly ShopContext _context;
        private readonly InventoryContext _inventoryContext;

        public ProductCategoryQuery(ShopContext context,InventoryContext inventoryContext)
        {
            _context = context;
            _inventoryContext = inventoryContext;
        }

        public List<ProductCategoryQueryModel> GetProductCategoryQueryModels()
        {
            return _context.ProductCategories.Select(p => new ProductCategoryQueryModel
            {
                Name = p.Name,
                Picture = p.Picture,
                PictureAlt = p.PictureAlt,
                PictureTitle = p.PictureTitle,
                Slug = p.Slug
            }).ToList();
        }

        public List<ProductCategoryQueryModel> GetProductCategoryWithProducts()
        {
            var inventory = _inventoryContext.Inventory.Select(x => new {x.ProductId, x.UnitPrice}).ToList();

            var categories = _context.ProductCategories
                .Include(x => x.Products)
                .ThenInclude(x => x.ProductCategory)
                .Select(x => new ProductCategoryQueryModel
                {
                    Id = x.Id,
                    Name = x.Name,
                    Products = MapProducts(x.Products)
                }).ToList();

            foreach (var category in categories)
            {
                foreach (var product in category.Products)
                {
                    product.Price = inventory.FirstOrDefault(x => x.ProductId == product.Id)
                        ?.UnitPrice.ToMoney();
                }
            }

            return categories;

        }

        private static List<ProductQueryModel> MapProducts(List<Product> Products)
        {
            return Products.Select(x => new ProductQueryModel
            {
                Id = x.Id,
                Name = x.Name,
                Category = x.ProductCategory.Name,
                Picture = x.Picture,
                PictureAlt = x.PictureAlt,
                PictureTitle = x.PictureTitle,
                Slug = x.Slug
            }).ToList();
        }
    }
}