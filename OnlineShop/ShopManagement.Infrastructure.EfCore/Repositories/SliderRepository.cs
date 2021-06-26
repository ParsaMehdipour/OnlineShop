﻿using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using _0_Framework.Infrastructure;
using Microsoft.EntityFrameworkCore;
using ShopManagement.Application.Contracts.Slider;
using ShopManagement.Domain.SliderAgg;

namespace ShopManagement.Infrastructure.EfCore.Repositories
{
    public class SliderRepository : BaseRepsitory<long,Slider> , ISliderRepository
    {
        private readonly ShopContext _context;

        public SliderRepository(ShopContext context) : base(context)
        {
            _context = context;
        }

        public EditSlider GetDetails(long id)
        {
            return _context.Sliders.Select(s => new EditSlider
            {
                Id = s.Id,
                Picture = s.Picture,
                PictureAlt = s.PictureAlt,
                PictureTitle = s.PictureTitle,
                Title = s.Title,
                Text = s.Text,
                BtnText = s.BtnText,
                Heading = s.Heading

            }).FirstOrDefault(s => s.Id == id);
        }

        public List<SliderViewModel> GetList()
        {
            return _context.Sliders.Select(s => new SliderViewModel
            {
                Id = s.Id,
                Title = s.Title,
                Picture = s.Picture,
                PictureAlt = s.PictureAlt,
                PictureTitle = s.PictureTitle,
                BtnText = s.BtnText,
                Text = s.Text,
                Heading = s.Heading,
                CreationDate = s.CreationDate.ToString(CultureInfo.InvariantCulture),
                IsRemoved = s.IsRemoved
            }).OrderByDescending(s=>s.Id).ToList();
        }
    }
}
