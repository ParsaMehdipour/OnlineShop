using System.Collections.Generic;
using _0_Framework.Application;
using ShopManagement.Application.Contracts.Comment;
using ShopManagement.Domain.CommentAgg;

namespace ShopManagement.Application.Comment
{
    public class CommentApplication : ICommentApplication
    {
        private readonly ICommentRepository _repository;

        public CommentApplication(ICommentRepository repository)
        {
            _repository = repository;
        }

        public OperationResult Create(CreateComment command)
        {
            var operationResult = new OperationResult();

            var comment = new Domain.CommentAgg.Comment(command.Name, command.Email
                , command.Message, command.ProductId);

            _repository.Create(comment);
            _repository.SaveChanges();

            return operationResult.Succeeded();
        }

        public OperationResult Confirm(long id)
        {
            var operationResult = new OperationResult();

            var comment = _repository.GetById(id);

            if (comment == null)
                return operationResult.Failed(ApplicationMessages.RecordNotFound);

            comment.Confirm();

            _repository.SaveChanges();

            return operationResult.Succeeded();
        }

        public OperationResult Cancel(long id)
        {
            var operationResult = new OperationResult();

            var comment = _repository.GetById(id);

            if (comment == null)
                return operationResult.Failed(ApplicationMessages.RecordNotFound);

            comment.Cancel();

            _repository.SaveChanges();

            return operationResult.Succeeded();
        }

        public List<CommentViewModel> Search(CommentSearchModel searchModel)
        {
            return _repository.Search(searchModel);
        }
    }
}
