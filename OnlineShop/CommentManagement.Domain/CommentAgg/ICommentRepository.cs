using System.Collections.Generic;
using _0_Framework.Domain;
using CommentManagement.Application.Contracts.Comment;

namespace CommentManagement.Domain.CommentAgg
{
    public interface ICommentRepository : IRepsoitory<long,Comment>
    {
        List<CommentViewModel> Search(CommentSearchModel searchModel);
    }
}
