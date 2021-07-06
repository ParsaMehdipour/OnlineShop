namespace DiscountManagement.Application.Contracts.ColleagueDiscount
{
    public class ColleagueDiscountViewModel
    {
        public long Id { get; set; }
        public long ProductId { get; set; }
        public string Product { get; set; }
        public long DiscountRate { get; set; }
        //public string Reason { get; set; }
        public bool IsRemoved { get; set; }
        public string CreationDate { get; set; }
    }
}
