using System.Collections.Generic;
using System.Reflection.Metadata.Ecma335;
using _01_OnlineShopQuery.Contracts.ArticleCategory;
using _01_OnlineShopQuery.Contracts.ProductCategory;

namespace _01_LampshadeQuery
{
    public class MenuModel
    {
        public List<ProductCategoryQueryModel> ProductCategories { get; set; }
        public List<ArticleCategoryQueryModel> ArticleCategories { get; set; }
    }
}
