namespace CommentManagement.Application.Contracts.Comment
{
    public class CreateComment
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Message { get; set; }
        public string Website { get; set; }
        public int Type { get; set; }
        public long OwnerRecordId { get; set; }
        public long ParentId { get; set; }
    }
}
