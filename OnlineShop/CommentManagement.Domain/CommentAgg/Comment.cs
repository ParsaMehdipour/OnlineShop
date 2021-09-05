using System.Collections.Generic;
using System.Reflection.Metadata.Ecma335;
using _0_Framework.Domain;

namespace CommentManagement.Domain.CommentAgg
{
    public class Comment : BaseEntity
    {
        public string Name { get; private set; }
        public string Email { get; private set; }
        public string Message { get; private set; }
        public bool IsConfirmed { get; private set; }
        public bool IsCanceled { get; private set; }
        public string Website { get; set; }
        public long OwnerRecordId { get; set; }
        public int Type { get; set; }

        public long ParentId { get; set; }
        public Comment Parent { get; set; }


        public Comment(string name, string email, string message, long ownerRecordId,int type,string website)
        {
            Name = name;
            Email = email;
            Message = message;
            Website = website;
            OwnerRecordId = ownerRecordId;
            Type = type;
        }

        public void Confirm()
        {
            IsConfirmed = true;
        }

        public void Cancel()
        {
            IsCanceled = true;
        }
    }
}
