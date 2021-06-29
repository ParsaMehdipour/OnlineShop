using System.Collections.Generic;
using System.Linq;
using ShopManagement.Infrastructure.EfCore;

namespace _01_OnlineShopQuery.Contracts.ProductCategory
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
    }
}