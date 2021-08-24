namespace ShopManagement.Application.Contracts.Comment
{
    public class CreateComment
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Message { get; set; }
        public long ProductId { get; set; }
    }
}
