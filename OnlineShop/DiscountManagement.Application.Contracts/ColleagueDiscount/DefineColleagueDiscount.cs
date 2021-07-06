using System.Collections.Generic;
using ShopManagement.Application.Contracts.Product;

namespace DiscountManagement.Application.Contracts.ColleagueDiscount
{
    public class DefineColleagueDiscount
    {
        public string Product { get; set; }
        public long ProductId { get; set; }
        public long DiscountRate { get; set; }
        //public string Reason { get; set; }
        public List<ProductViewModel> Products { get; set; }
    }
}