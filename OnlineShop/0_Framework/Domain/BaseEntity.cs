using System;

namespace _0_Framework.Domain
{
    public class BaseEntity<TKey>
    {
        public TKey Id { get; set; }
        public DateTime CreationDate { get; set; }

        public BaseEntity()
        {
            CreationDate = DateTime.Now;
        }
    }

    public class BaseEntity : BaseEntity<long>
    {

    }
}
