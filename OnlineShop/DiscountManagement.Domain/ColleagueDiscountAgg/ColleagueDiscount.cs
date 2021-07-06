using _0_Framework.Domain;

namespace DiscountManagement.Domain.ColleagueDiscountAgg
{
    public class ColleagueDiscount : BaseEntity
    {
        public long ProductId { get; private set; }
        public long DiscountRate { get; private set; }
        public bool IsRemoved { get; private set; }
        //public string Reason { get; private set; }

        public ColleagueDiscount(long productId, long discountRate)
        {
            ProductId = productId;
            DiscountRate = discountRate;
            IsRemoved = false;
        }

        public void Edit(long productId, long discountRate)
        {
            ProductId = productId;
            DiscountRate = discountRate;
        }

        public void Remove()
        {
            IsRemoved = true;
        }

        public void Restore()
        {
            IsRemoved = false;
        }
    }
}
