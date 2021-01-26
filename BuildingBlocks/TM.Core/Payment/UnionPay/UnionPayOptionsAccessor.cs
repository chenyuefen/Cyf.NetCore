using Microsoft.Extensions.Options;

namespace TM.Core.Payment.UnionPay
{
    public class UnionPayOptionsAccessor : IOptions<UnionPayOptions>
    {
        public UnionPayOptionsAccessor(UnionPayOptions options)
        {
            Value = options;
        }

        public UnionPayOptions Value { get; }
    }
}
