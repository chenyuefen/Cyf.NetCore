using Microsoft.Extensions.Options;

namespace TM.Core.Payment.Alipay
{
    public class AlipayOptionsAccessor : IOptions<AlipayOptions>
    {
        public AlipayOptionsAccessor(AlipayOptions options)
        {
            Value = options;
        }

        public AlipayOptions Value { get; }
    }
}
