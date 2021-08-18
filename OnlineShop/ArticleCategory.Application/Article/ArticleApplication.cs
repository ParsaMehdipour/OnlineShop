using System.Collections.Generic;
using System.IO.Pipelines;
using System.Runtime.InteropServices;
using _0_Framework.Application;
using ArticleManagement.Application.Contracts.Article;
using ArticleManagement.Domain.ArticleAgg;
using ArticleManagement.Domain.ArticleCategoryAgg;
using Microsoft.AspNetCore.Http;

namespace ArticleCategory.Application.Article
{
    public class ArticleApplication : IArticleApplication
    {
        private readonly IArticleRepository _repository;
        private readonly IArticleCategoryRepository _articleCategoryRepository;
        private readonly IFileUploader _fileUploader;

        public ArticleApplication(IArticleRepository repository,IArticleCategoryRepository articleCategoryRepository,IFileUploader fileUploader)
        {
            _repository = repository;
            _articleCategoryRepository = articleCategoryRepository;
            _fileUploader = fileUploader;
        }

        public OperationResult Create(CreateArticle command)
        {
            var operationResult = new OperationResult();

            if (_repository.Exists(x => x.Title == command.Title))
                return operationResult.Failed(ApplicationMessages.DuplicatedRecord);

            var categorySlug = _articleCategoryRepository.GetSlugWithId(command.CategoryId);

            var path = $"{categorySlug}/{command.Slug}";
            var fileName = _fileUploader.Upload(command.Picture, path);

            var publishDate = command.PublishDate.ToGeorgianDateTime();

            var article = new ArticleManagement.Domain.ArticleAgg.Article(command.Title, command.Title,
                command.ShortDescription
                , fileName, command.PictureAlt, command.PictureTitle
                , command.Slug, command.Keywords, command.MetaDescription
                , command.CanonicalAddress,publishDate, command.CategoryId);

            _repository.Create(article);
            _repository.SaveChanges();

            return operationResult.Succedded();
        }

        public OperationResult Edit(EditArticle command)
        {
            var operationResult = new OperationResult();

            var article = _repository.GetById(command.Id);

            if (article is null)
                return operationResult.Failed(ApplicationMessages.RecordNotFound);

            if (_repository.Exists(x => x.Title == command.Title && x.Id != command.Id))
                return operationResult.Failed(ApplicationMessages.DuplicatedRecord);

            var path = $"{article.ArticleCategory.Slug}/{command.Slug}";
            var fileName = _fileUploader.Upload(command.Picture, path);

            var publishDate = command.PublishDate.ToGeorgianDateTime();

            article.Edit(command.Title, command.Title,
                command.ShortDescription
                , fileName, command.PictureAlt, command.PictureTitle
                , command.Slug, command.Keywords, command.MetaDescription
                , command.CanonicalAddress, publishDate, command.CategoryId);

            _repository.SaveChanges();

            return operationResult.Succedded();
        }

        public List<ArticleViewModel> Search(ArticleSearchModel searchModel)
        {
            return _repository.Search(searchModel);
        }

        public EditArticle GetDetails(long id)
        {
            return _repository.GetDetails(id);
        }
    }
}
