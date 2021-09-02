using System.Collections.Generic;
using System.Linq;
using _0_Framework.Application;
using _0_Framework.Infrastructure;
using CommentManagement.Application.Contracts.Comment;
using CommentManagement.Domain.CommentAgg;

namespace CommentManagement.Infrastructure.EfCore.Repositories
{
    public class CommentRepository : BaseRepsitory<long, Comment>, ICommentRepository
    {
        private readonly CommentContext _context;

        public CommentRepository(CommentContext context) : base(context)
        {
            _context = context;
        }

        public List<CommentViewModel> Search(CommentSearchModel searchModel)
        {
            var query = _context.Comments
                .Select(x => new CommentViewModel
                {
                    Id = x.Id,
                    Name = x.Name,
                    Website = x.Website,
                    OwnerRecordId = x.OwnerRecordId,
                    Type = x.Type,
                    CreationDate = x.CreationDate.ToFarsi(),
                    Email = x.Email,
                    IsCanceled = x.IsCanceled,
                    IsConfirmed = x.IsConfirmed,
                    Message = x.Message
                });

            if (string.IsNullOrWhiteSpace(searchModel.Name) is false)
                query = query.Where(x => x.Name == searchModel.Name);

            if (string.IsNullOrWhiteSpace(searchModel.Email) is false)
                query = query.Where(x => x.Email == searchModel.Email);


            return query.OrderByDescending(x => x.Id).ToList();
        }
    }
}
