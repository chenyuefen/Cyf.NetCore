using Microsoft.Extensions.Options;

namespace TM.Core.Payment.WeChatPay
{
    public class WeChatPayOptionsAccessor : IOptions<WeChatPayOptions>
    {
        public WeChatPayOptionsAccessor(WeChatPayOptions options)
        {
            Value = options;
        }

        public WeChatPayOptions Value { get; }
    }
}
