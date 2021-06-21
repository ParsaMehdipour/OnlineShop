﻿using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using _0_Framework.Infrastructure;
using Microsoft.EntityFrameworkCore;
using ShopManagement.Application.Contracts.ProductPicture;
using ShopManagement.Domain.ProductPictureAgg;

namespace ShopManagement.Infrastructure.EfCore.Repositories
{
    public class ProductPictureRepository : BaseRepsitory<long,ProductPicture> , IProductPictureRepository
    {
        private readonly ShopContext _context;

        public ProductPictureRepository(ShopContext context) : base(context)
        {
            _context = context;
        }

        public EditProductPicture GetDetails(long id)
        {
            return _context.ProductPictures.Select(p => new EditProductPicture()
            {
                Id = p.Id,
                Picture = p.Picture,
                PictureAlt = p.PictureAlt,
                PictureTitle = p.PictureTitle,
                ProductId = p.ProductId
            }).FirstOrDefault(p => p.Id == id);
        }

        public List<ProductPictureViewModel> Search(ProductPictureSearchModel searchModel)
        {
            var query = _context.ProductPictures
                .Include(x => x.Product)
                .Select(p => new ProductPictureViewModel()
                {
                    Id = p.Id,
                    Product = p.Product.Name,
                    CreationDate = p.CreationDate.ToString(CultureInfo.InvariantCulture),
                    Picture = p.Picture,
                    ProductId = p.ProductId
                });

            if (searchModel.ProductId != 0)
                query = query.Where(p => p.ProductId == searchModel.ProductId);

            return query.OrderByDescending(p => p.Id).ToList();
        }
    }
}