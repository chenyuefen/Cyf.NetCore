﻿using Microsoft.AspNetCore.Mvc;

namespace TM.Core.Payment.UnionPay
{
    public class UnionPayNotifyResult
    {
        private static readonly ContentResult _ok = new ContentResult() { Content = "ok", ContentType = "text/plain" };

        public static IActionResult Ok => _ok;
    }
}
