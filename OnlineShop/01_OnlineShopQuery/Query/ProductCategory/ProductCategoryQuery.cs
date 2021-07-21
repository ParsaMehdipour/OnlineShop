using System.Collections.Generic;
using System.Linq;
using _01_OnlineShopQuery.Contracts.Product;
using _01_OnlineShopQuery.Contracts.ProductCategory;
using Microsoft.EntityFrameworkCore;
using ShopManagement.Domain.ProductAgg;
using ShopManagement.Infrastructure.EfCore;

namespace _01_OnlineShopQuery.Query.ProductCategory
{
    public class ProductCategoryQuery : IProductCategoryQuery
    {
        private readonly ShopContext _context;

        public ProductCategoryQuery(ShopContext context)
        {
            _context = context;
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
            return _context.ProductCategories
                .Include(x => x.Products)
                .ThenInclude(x => x.ProductCategory)
                .Select(x => new ProductCategoryQueryModel
                {
                    Id = x.Id,
                    Name = x.Name,
                    Products = MapProducts(x.Products)
                }).ToList();
        }

        private static List<ProductQueryModel> MapProducts(List<Product> Products)
        {
            var result = new List<ProductQueryModel>();

            foreach (var item in Products)
            {
                var product = new ProductQueryModel
                {
                    Id = item.Id,
                    Name = item.Name,
                    Category = item.ProductCategory.Name,
                    Picture = item.Picture,
                    PictureAlt = item.PictureAlt,
                    PictureTitle = item.PictureTitle,
                    Slug = item.Slug
                };

                result.Add(product);
            }

            return result;
        }
    }
}