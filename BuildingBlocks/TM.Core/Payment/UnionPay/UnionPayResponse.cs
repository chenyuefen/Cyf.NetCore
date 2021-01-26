namespace TM.Core.Payment.UnionPay
{
    public abstract class UnionPayResponse : UnionPayObject
    {
        public string Body { get; set; }
    }
}
