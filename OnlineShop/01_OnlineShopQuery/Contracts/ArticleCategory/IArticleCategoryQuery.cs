using System.Collections.Generic;

namespace _01_OnlineShopQuery.Contracts.ArticleCategory
{
    public interface IArticleCategoryQuery
    {
        List<ArticleCategoryQueryModel> GetAllArticleCategories();
        ArticleCategoryQueryModel GetArticleCategory(string slug);
    }
}
