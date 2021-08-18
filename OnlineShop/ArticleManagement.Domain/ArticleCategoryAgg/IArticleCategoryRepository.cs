using System.Collections.Generic;
using _0_Framework.Domain;
using ArticleManagement.Application.Contracts.ArticleCategory;

namespace ArticleManagement.Domain.ArticleCategoryAgg
{
    public interface IArticleCategoryRepository : IRepsoitory<long,ArticleCategory>
    {
        EditArticleCategory GetDetails(long id);
        List<ArticleCategoryViewModel> Search(ArticleCategorySearchModel searchModel);
        string GetSlugWithId(long id);
    }
}
