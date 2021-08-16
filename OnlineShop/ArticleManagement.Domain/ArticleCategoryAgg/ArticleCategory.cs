using _0_Framework.Domain;

namespace ArticleManagement.Domain.ArticleCategoryAgg
{
    public class ArticleCategory :BaseEntity
    {
        public string Name { get; private set; }
        public string Description { get; private set; }
        public string Picture { get; private set; }
        public string PictureAlt { get; private set; }
        public string PictureTitle { get; private set; }
        public string Slug { get; private set; }
        public string MetaDescription { get; private set; }
        public int ShowOrder { get; private set; }
        public string CanonicalAddress { get; private set; }
        public string Keywords { get; private set; }

        public ArticleCategory(string name, string description, string picture, string pictureAlt, string pictureTitle, string slug, string metaDescription, int showOrder, string canonicalAddress, string keywords)
        {
            Name = name;
            Description = description;
            Picture = picture;
            PictureAlt = pictureAlt;
            PictureTitle = pictureTitle;
            Slug = slug;
            MetaDescription = metaDescription;
            ShowOrder = showOrder;
            CanonicalAddress = canonicalAddress;
            Keywords = keywords;
        }

        public void Edit(string name, string description, string picture, string pictureAlt, string pictureTitle, string slug, string metaDescription, int showOrder, string canonicalAddress, string keywords)
        {
            Name = name;
            Description = description;
            if(!string.IsNullOrWhiteSpace(picture))
                Picture = picture;
            PictureAlt = pictureAlt;
            PictureTitle = pictureTitle;
            Slug = slug;
            MetaDescription = metaDescription;
            ShowOrder = showOrder;
            CanonicalAddress = canonicalAddress;
            Keywords = keywords;
        }
    }
}
