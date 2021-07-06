using System;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using System.Globalization;
using System.Linq;
using _0_Framework.Application;
using _0_Framework.Infrastructure;
using DiscountManagement.Application.Contracts.CustomerDiscount;
using DiscountManagement.Domain.CustomerDiscountAgg;
using Microsoft.EntityFrameworkCore;
using ShopManagement.Infrastructure.EfCore;

namespace DiscountManagement.Infrastructure.EfCore.Repository
{
    public class CustomerDiscountRepository :BaseRepsitory<long , CustomerDiscount> , ICustomerDiscountRepository
    {
        private readonly DiscountContext _context;
        private readonly ShopContext _shopContext;

        public CustomerDiscountRepository(DiscountContext context , ShopContext shopContext) : base(context)
        {
            _context = context;
            _shopContext = shopContext;
        }

        public List<CustomerDiscountViewModel> Search(CustomerDiscountSearchModel searchModel)
        {
            var products = _shopContext.Products.Select(p => new {p.Id, p.Name}).ToList();

            var query = _context.CustomerDiscounts.Select(c => new CustomerDiscountViewModel
            {
                Id = c.Id,
                DiscountRate = c.DiscountRate,
                ProductId = c.ProductId,
                StartDate = c.StartDate.ToFarsi(),
                StartDateGr = c.StartDate,
                EndDate = c.EndDate.ToFarsi(),
                EndDateGr = c.EndDate,
                Reason = c.Reason,
                CreationDate = c.CreationDate.ToFarsi()
            });

            if (searchModel.ProductId > 0)
                query = query.Where(c => c.ProductId == searchModel.ProductId);

            if (!string.IsNullOrWhiteSpace(searchModel.StartDate))
            {
                query = query.Where(c => c.StartDateGr > searchModel.StartDate.ToGeorgianDateTime());
            }

            if (!string.IsNullOrWhiteSpace(searchModel.EndDate))
            {
                query = query.Where(c => c.EndDateGr < searchModel.EndDate.ToGeorgianDateTime());
            }

            var discounts = query.OrderByDescending(c => c.Id).ToList();

            discounts.ForEach(discount => 
                discount.Product = products.FirstOrDefault(c=>c.Id == discount.ProductId)?.Name);

            return discounts;
        }

        public EditCustomerDiscount GetDetails(long id)
        {
            return _context.CustomerDiscounts.Select(c => new EditCustomerDiscount
            {
                Id = c.Id,
                ProductId = c.ProductId,
                StartDate = c.StartDate.ToFarsi(),
                EndDate = c.EndDate.ToFarsi(),
                Reason = c.Reason,
                DiscountRate = c.DiscountRate
            }).FirstOrDefault(c=>c.Id == id);
        }
    }
}
