namespace _0_Framework.Application
{
    public class OperationResult
    {
        public string Message { get; set; }
        public bool IsSuccedded { get; set; }

        public OperationResult()
        {
            IsSuccedded = false;
        }

        public OperationResult Succedded(string message = "عملیت با موفقیت انجام شد")
        {
            IsSuccedded = true;
            Message = message;
            return this;
        }

        public OperationResult Failed(string message = "عملیات با شکست مواجه شد. لطفا مجددا تلاش کنید")
        {
            IsSuccedded = false;
            message = Message;
            return this;
        }
    }
}
