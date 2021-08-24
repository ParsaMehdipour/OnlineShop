using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using _0_Framework.Application;
using _0_Framework.Infrastructure;
using Microsoft.EntityFrameworkCore;
using ShopManagement.Application.Contracts.Comment;
using ShopManagement.Domain.CommentAgg;

namespace ShopManagement.Infrastructure.EfCore.Repositories
{
    public class CommentRepository : BaseRepsitory<long, Comment>, ICommentRepository
    {
        private readonly ShopContext _context;

        public CommentRepository(ShopContext context) : base(context)
        {
            _context = context;
        }

        public List<CommentViewModel> Search(CommentSearchModel searchModel)
        {
            var query = _context.Comments
                .Include(x => x.Product)
                .Select(x => new CommentViewModel
                {
                    Id = x.Id,
                    Name = x.Name,
                    ProductId = x.ProductId,
                    CreationDate = x.CreationDate.ToFarsi(),
                    ProductName = x.Product.Name,
                    Email = x.Email,
                    IsCanceled = x.IsCanceled,
                    IsConfirmed = x.IsConfirmed,
                    Message = x.Message
                });

            if (string.IsNullOrWhiteSpace(searchModel.Name) is false)
                query = query.Where(x => x.Name == searchModel.Name);

            if (string.IsNullOrWhiteSpace(searchModel.Email) is false)
                query = query.Where(x => x.Email == searchModel.Email);

            if (searchModel.ProductId != 0)
                query = query.Where(x => x.ProductId == searchModel.ProductId);

            return query.OrderByDescending(x => x.Id).ToList();
        }
    }
}
