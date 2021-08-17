using System;
using _0_Framework.Domain;
using ArticleManagement.Domain.ArticleCategoryAgg;

namespace ArticleManagement.Domain.ArticleAgg
{
    public class Article : BaseEntity
    {
        public string Title { get; private set; }
        public string Description { get; private set; }
        public string ShortDescription { get; private set; }
        public string Picture { get; private set; }
        public string PictureAlt { get; private set; }
        public string PictureTitle { get; private set; }
        public string Slug { get; private set; }
        public string Keywords { get; private set; }
        public string MetaDescription { get; private set; }
        public string CanonicalAddress { get; private set; }
        public DateTime PublishDate { get; private set; }
        public long CategoryId { get; private set; }
        public ArticleCategory ArticleCategory { get; private set; }

        public Article(string title, string description, string shortDescription, string picture, string pictureAlt, string pictureTitle, string slug, string keywords, string metaDescription, string canonicalAddress, DateTime publishDate, long categoryId)
        {
            Title = title;
            Description = description;
            ShortDescription = shortDescription;
            Picture = picture;
            PictureAlt = pictureAlt;
            PictureTitle = pictureTitle;
            Slug = slug;
            Keywords = keywords;
            MetaDescription = metaDescription;
            CanonicalAddress = canonicalAddress;
            PublishDate = publishDate;
            CategoryId = categoryId;
        }

        public void Edit(string title, string description, string shortDescription, string picture, string pictureAlt, string pictureTitle, string slug, string keywords, string metaDescription, string canonicalAddress, DateTime publishDate, long categoryId)
        {
            Title = title;
            Description = description;
            ShortDescription = shortDescription;
            Picture = picture;
            PictureAlt = pictureAlt;
            PictureTitle = pictureTitle;
            Slug = slug;
            Keywords = keywords;
            MetaDescription = metaDescription;
            CanonicalAddress = canonicalAddress;
            PublishDate = publishDate;
            CategoryId = categoryId;
        }
    }
}
