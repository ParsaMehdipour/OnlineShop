using System.Collections.Generic;
using _0_Framework.Domain;
using ShopManagement.Application.Contracts.Comment;

namespace ShopManagement.Domain.CommentAgg
{
    public interface ICommentRepository : IRepsoitory<long,Comment>
    {
        List<CommentViewModel> Search(CommentSearchModel searchModel);
    }
}
