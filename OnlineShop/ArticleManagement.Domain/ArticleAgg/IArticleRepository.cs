using System.Collections.Generic;
using _0_Framework.Domain;
using ArticleManagement.Application.Contracts.Article;
using Microsoft.Extensions.Logging.Abstractions;

namespace ArticleManagement.Domain.ArticleAgg
{
    public interface IArticleRepository : IRepsoitory<long, Article>
    {
        EditArticle GetDetails(long id);
        List<ArticleViewModel> Search(ArticleSearchModel searchModel);
    }
}
