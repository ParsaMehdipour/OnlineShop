using System.Collections.Generic;

namespace _01_OnlineShopQuery.Contracts.Article
{
    public interface IArticleQuery
    {
        List<ArticleQueryModel> LatestArticles();
        ArticleQueryModel GetArticle(string slug);
    }
}
