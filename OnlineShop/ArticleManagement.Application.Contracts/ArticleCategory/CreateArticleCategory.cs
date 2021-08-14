﻿using Microsoft.AspNetCore.Http;

namespace ArticleManagement.Application.Contracts.ArticleCategory
{
    public class CreateArticleCategory
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public IFormFile Picture { get; set; }
        public string PictureAlt { get; set; }
        public string PictureTitle { get; set; }
        public string Slug { get; set; }
        public string MetaDescription { get; set; }
        public int ShowOrder { get; set; }
        public string CanonicalAddress { get; set; }
        public string Keywords { get; set; }
    }
}
