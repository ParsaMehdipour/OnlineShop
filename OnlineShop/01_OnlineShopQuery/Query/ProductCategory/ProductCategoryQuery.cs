using System;
using System.Collections.Generic;
using System.Linq;
using _0_Framework.Application;
using _01_OnlineShopQuery.Contracts.Product;
using _01_OnlineShopQuery.Contracts.ProductCategory;
using DiscountManagement.Infrastructure.EfCore;
using InventoryManagement.Infrastructure.EfCore;
using Microsoft.EntityFrameworkCore;
using ShopManagement.Infrastructure.EfCore;

namespace _01_OnlineShopQuery.Query.ProductCategory
{
    public class ProductCategoryQuery : IProductCategoryQuery
    {
        private readonly ShopContext _context;
        private readonly InventoryContext _inventoryContext;
        private readonly DiscountContext _discountContext;

        public ProductCategoryQuery(ShopContext context,InventoryContext inventoryContext , DiscountContext discountContext)
        {
            _context = context;
            _inventoryContext = inventoryContext;
            _discountContext = discountContext;
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
            var inventories = _inventoryContext.Inventory.Select(x => new {x.ProductId, x.UnitPrice}).ToList();
            var discounts = _discountContext.CustomerDiscounts.Select(x => new {x.DiscountRate, x.ProductId}).ToList();


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
                    var productInventory = inventories.FirstOrDefault(x => x.ProductId == product.Id);

                    if (productInventory != null)
                    {
                        var price = productInventory.UnitPrice;
                        product.Price = price.ToMoney();

                        var discount = discounts.FirstOrDefault(x => x.ProductId == product.Id);

                        if (discount != null)
                        {
                            int discountRate = discount.DiscountRate;
                            product.DiscountRate = discountRate;
                            product.HasDiscount = discountRate > 0;
                            var discountAmount = Math.Round((price * discountRate) / 100);

                            product.PriceWithDiscount = (price - discountAmount).ToMoney();

                        }
                    }
                }
            }



            return categories;

        }

        public ProductCategoryQueryModel GetProductCategoryWithProducts(string slug)
        {
            var inventories = _inventoryContext.Inventory.Select(x => new { x.ProductId, x.UnitPrice }).ToList();
            var discounts = _discountContext.CustomerDiscounts.Where(x => x.StartDate < DateTime.Now && x.EndDate > DateTime.Now)
                .Select(x => new { x.DiscountRate, x.ProductId, x.EndDate }).ToList();

            var category = _context.ProductCategories
                .Include(x => x.Products)
                .ThenInclude(x => x.ProductCategory)
                .Select(x => new ProductCategoryQueryModel
                {
                    Id = x.Id,
                    Name = x.Name,
                    Slug = x.Slug,
                    Picture = x.Picture,
                    PictureAlt = x.PictureAlt,
                    PictureTitle = x.PictureTitle,
                    Keywords = x.KeyWords,
                    Description = x.Description,
                    MetaDescription = x.MetaDescription,
                    Products = MapProducts(x.Products)
                }).FirstOrDefault(x => x.Slug == slug);

            foreach (var product in category.Products)
            {
                var productInventory = inventories.FirstOrDefault(x => x.ProductId == product.Id);

                if (productInventory != null)
                {
                    var price = productInventory.UnitPrice;
                    product.Price = price.ToMoney();

                    var discount = discounts.FirstOrDefault(x => x.ProductId == product.Id);

                    if (discount != null)
                    {
                        int discountRate = discount.DiscountRate;
                        product.DiscountRate = discountRate;
                        product.HasDiscount = discountRate > 0;
                        product.DiscountExpirationDate = discount.EndDate.ToDiscountFormat();
                        var discountAmount = Math.Round((price * discountRate) / 100);

                        product.PriceWithDiscount = (price - discountAmount).ToMoney();

                    }
                }
            }

            return category;
        }

        private static List<ProductQueryModel> MapProducts(List<ShopManagement.Domain.ProductAgg.Product> products)
        {
            return products.Select(x => new ProductQueryModel
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