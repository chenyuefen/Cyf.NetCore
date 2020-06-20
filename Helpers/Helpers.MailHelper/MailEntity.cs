using System;
using System.Collections.Generic;
using System.Text;

namespace Helpers
{
    public class MailEntity
    {
        public string MessageId { get; set; }
        public string Subject { get; set; }
        public DateTimeOffset Date { get; set; }
        public string TextBody { get; set; }
        public string HtmlBody { get; set; }
    }
}
