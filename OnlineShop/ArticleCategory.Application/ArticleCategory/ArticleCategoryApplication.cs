using System.Collections.Generic;
using _0_Framework.Application;
using ArticleManagement.Application.Contracts.ArticleCategory;
using ArticleManagement.Domain.ArticleCategoryAgg;

namespace ArticleCategory.Application.ArticleCategory
{
    public class ArticleCategoryApplication : IArticleCategoryApplication
    {
        private readonly IArticleCategoryRepository _repository;
        private readonly IFileUploader _fileUploader;

        public ArticleCategoryApplication(IArticleCategoryRepository repository,IFileUploader fileUploader)
        {
            _repository = repository;
            _fileUploader = fileUploader;
        }
        public OperationResult Create(CreateArticleCategory command)
        {
            var operationResult = new OperationResult();

            if (_repository.Exists(x => x.Name == command.Name))
                return operationResult.Failed(ApplicationMessages.DuplicatedRecord);

            var slug = command.Slug.Slugify();

            var fileName = _fileUploader.Upload(command.Picture, slug);

            var articleCategory = new ArticleManagement.Domain.ArticleCategoryAgg.ArticleCategory(command.Name,
                command.Description, fileName
                , command.PictureAlt, command.PictureTitle, command.Slug
                , command.MetaDescription, command.ShowOrder, command.CanonicalAddress
                , command.Keywords);

            _repository.Create(articleCategory);
            _repository.SaveChanges();

            return operationResult.Succedded();
        }

        public OperationResult Edit(EditArticleCategory command)
        {
            var operationResult = new OperationResult();

            var articleCategory = _repository.GetById(command.Id);

            if (articleCategory is null)
                return operationResult.Failed(ApplicationMessages.RecordNotFound);

            if (_repository.Exists(x => x.Name == command.Name && x.Id != command.Id))
                return operationResult.Failed(ApplicationMessages.DuplicatedRecord);

            var slug = command.Slug.Slugify();
            var fileName = _fileUploader.Upload(command.Picture, slug);

            articleCategory.Edit(command.Name,command.Description,fileName
            ,command.PictureAlt,command.PictureTitle,command.Slug
            ,command.MetaDescription,command.ShowOrder,command.CanonicalAddress
            ,command.Keywords);

            _repository.SaveChanges();

            return operationResult.Succedded();
        }

        public EditArticleCategory GetDetails(long id)
        {
            return _repository.GetDetails(id);
        }

        public List<ArticleCategoryViewModel> Search(ArticleCategorySearchModel searchModel)
        {
            return _repository.Search(searchModel);
        }
    }
}
