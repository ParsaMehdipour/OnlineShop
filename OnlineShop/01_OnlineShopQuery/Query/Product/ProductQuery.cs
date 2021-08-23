using System;
using System.Collections.Generic;
using System.Linq;
using _0_Framework.Application;
using _01_OnlineShopQuery.Contracts.Product;
using DiscountManagement.Infrastructure.EfCore;
using InventoryManagement.Infrastructure.EfCore;
using Microsoft.EntityFrameworkCore;
using ShopManagement.Domain.ProductPictureAgg;
using ShopManagement.Infrastructure.EfCore;

namespace _01_OnlineShopQuery.Query.Product
{
    public class ProductQuery : IProductQuery
    {
        private readonly ShopContext _shopContext;
        private readonly DiscountContext _discountContext;
        private readonly InventoryContext _inventoryContext;

        public ProductQuery(ShopContext shopContext, DiscountContext discountContext, InventoryContext inventoryContext)
        {
            _shopContext = shopContext;
            _discountContext = discountContext;
            _inventoryContext = inventoryContext;
        }

        public ProductQueryModel GetDetails(string slug)
        {
            var inventories = _inventoryContext.Inventory.Select(x => new { x.UnitPrice, x.ProductId, x.InStock }).ToList();
            var discounts = _discountContext.CustomerDiscounts
                .Where(x => x.StartDate < DateTime.Now && x.EndDate > DateTime.Now)
                .Select(x => new { x.DiscountRate, x.ProductId, x.EndDate }).ToList();

            var product = _shopContext.Products
                .Include(x => x.ProductCategory)
                .Include(x=>x.ProductPictures)
                .Select(x => new ProductQueryModel
                {
                    Id = x.Id,
                    Category = x.ProductCategory.Name,
                    Name = x.Name,
                    Picture = x.Picture,
                    PictureAlt = x.PictureAlt,
                    PictureTitle = x.PictureTitle,
                    Slug = x.Slug,
                    CategorySlug = x.ProductCategory.Slug,
                    Code = x.Code,
                    Description = x.Description,
                    Keywords = x.Keywords,
                    MetaDescription = x.MetaDescription,
                    ShortDescription = x.ShortDescription,
                    ProductPictures = MapToProductPictures(x.ProductPictures)
                }).AsNoTracking().FirstOrDefault(x => x.Slug == slug);

            if (product == null)
                return new ProductQueryModel();


            var productInventory = inventories.FirstOrDefault(x => x.ProductId == product.Id);

            if (productInventory != null)
            {
                product.IsInStock = productInventory.InStock;

                var price = productInventory.UnitPrice;

                product.Price = price.ToMoney();

                var productDiscount = discounts.FirstOrDefault(x => x.ProductId == product.Id);

                if (productDiscount != null)
                {
                    var discountRate = productDiscount.DiscountRate;

                    product.DiscountExpirationDate = productDiscount.EndDate.ToDiscountFormat();
                    product.DiscountRate = discountRate;
                    product.HasDiscount = discountRate > 0;

                    var discountAmount = Math.Round((price * discountRate) / 100);

                    product.PriceWithDiscount = (price - discountAmount).ToMoney();
                }
            }


            return product;
        }

        private static List<ProductPictureQueryModel> MapToProductPictures(List<ProductPicture> ProductPictures)
        {
            return ProductPictures.Select(x => new ProductPictureQueryModel
            {
                ProductId = x.ProductId,
                Picture = x.Picture,
                PictureAlt = x.PictureAlt,
                PictureTitle = x.PictureTitle,
                IsRemoved = x.IsRemoved
            }).Where(x=>x.IsRemoved == false).ToList();
        }

        public List<ProductQueryModel> GetLatestProducts()
        {
            var inventories = _inventoryContext.Inventory.Select(x => new { x.UnitPrice, x.ProductId }).ToList();
            var discounts = _discountContext.CustomerDiscounts
                .Where(x => x.StartDate < DateTime.Now && x.EndDate > DateTime.Now)
                .Select(x => new { x.DiscountRate, x.ProductId }).ToList();

            var products = _shopContext.Products
                .Include(x => x.ProductCategory)
                .Select(x => new ProductQueryModel
                {
                    Id = x.Id,
                    Name = x.Name,
                    Category = x.ProductCategory.Name,
                    Picture = x.Picture,
                    PictureAlt = x.PictureAlt,
                    PictureTitle = x.PictureTitle,
                    Slug = x.Slug
                }).AsNoTracking().OrderByDescending(x => x.Id).Take(6).ToList();

            foreach (var product in products)
            {
                var productInventory = inventories.FirstOrDefault(x => x.ProductId == product.Id);

                if (productInventory != null)
                {
                    var price = productInventory.UnitPrice;

                    product.Price = price.ToMoney();

                    var productDiscount = discounts.FirstOrDefault(x => x.ProductId == product.Id);

                    if (productDiscount != null)
                    {
                        var discountRate = productDiscount.DiscountRate;

                        product.DiscountRate = discountRate;
                        product.HasDiscount = discountRate > 0;

                        var discountAmount = Math.Round((price * discountRate) / 100);

                        product.PriceWithDiscount = (price - discountAmount).ToMoney();
                    }
                }
            }

            return products;
        }

        public List<ProductQueryModel> Search(string value)
        {
            var inventories = _inventoryContext.Inventory.Select(x => new { x.UnitPrice, x.ProductId }).ToList();
            var discounts = _discountContext.CustomerDiscounts
                .Where(x => x.StartDate < DateTime.Now && x.EndDate > DateTime.Now)
                .Select(x => new { x.DiscountRate, x.ProductId, x.EndDate }).ToList();

            var query = _shopContext.Products
                .Include(x => x.ProductCategory)
                .Select(product => new ProductQueryModel
                {
                    Id = product.Id,
                    Category = product.ProductCategory.Name,
                    CategorySlug = product.ProductCategory.Slug,
                    Name = product.Name,
                    Picture = product.Picture,
                    PictureAlt = product.PictureAlt,
                    PictureTitle = product.PictureTitle,
                    ShortDescription = product.ShortDescription,
                    Slug = product.Slug
                }).AsNoTracking();

            if (!string.IsNullOrWhiteSpace(value))
                query = query.Where(x => x.Name.Contains(value) || x.ShortDescription.Contains(value));

            var products = query.OrderByDescending(x => x.Id).ToList();

            foreach (var product in products)
            {
                var productInventory = inventories.FirstOrDefault(x => x.ProductId == product.Id);
                if (productInventory != null)
                {
                    var price = productInventory.UnitPrice;
                    product.Price = price.ToMoney();
                    var discount = discounts.FirstOrDefault(x => x.ProductId == product.Id);
                    if (discount == null) continue;

                    var discountRate = discount.DiscountRate;
                    product.DiscountRate = discountRate;
                    product.DiscountExpirationDate = discount.EndDate.ToDiscountFormat();
                    product.HasDiscount = discountRate > 0;
                    var discountAmount = Math.Round((price * discountRate) / 100);
                    product.PriceWithDiscount = (price - discountAmount).ToMoney();
                }
            }

            return products;
        }
    }
}
