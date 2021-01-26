/*----------------------------------------------------------------
// Copyright (C) 2020 广州天美联盟网络科技有限公司
// 版权所有。
//
// 文件名：CreateUserSigApiV1
// 文件功能描述： 生成 UserSig 老版本算法(ECDSA-SHA256)
//
// 创建者：庄欣锴
// 创建时间：2020-05-06 09:30
// 
//----------------------------------------------------------------*/
using tencentyun;

namespace TM.Infrastructure.IM
{
    public class CreateUserSigApiV1
    {
        private readonly string priKeyContent = @"-----BEGIN PRIVATE KEY-----
                                                 MIGHAgEAMBMGByqGSM49AgEGCCqGSM49AwEHBG0wawIBAQQgfK1yIyPWB5mF9TJL
                                                 R6+ezMlm6XPNl5zDv+s7sQp/8LKhRANCAAQ1WXQ97uBxu5jumuy86CqNPBSY3l7j
                                                 RWenfGW5EKrR9CrCQD4NVb/extIl4D4vUObdwJ6Kk8kj+zXWn3H96Wpz
                                                 -----END PRIVATE KEY-----";

        private readonly string pubKeyContent = @"-----BEGIN PUBLIC KEY-----
                                                 MFkwEwYHKoZIzj0CAQYIKoZIzj0DAQcDQgAENVl0Pe7gcbuY7prsvOgqjTwUmN5e
                                                 40Vnp3xluRCq0fQqwkA+DVW/3sbSJeA+L1Dm3cCeipPJI/s11p9x/elqcw==
                                                 -----END PUBLIC KEY-----";

        private readonly int sdkAppid;
        public CreateUserSigApiV1(int sdkAppid)
        {
            this.sdkAppid = sdkAppid;
        }

        #region 计算IM UserSig ==>使用默认过期时间
        public string GetSig(string identifier)
        {
            TLSSigAPI api = new TLSSigAPI(sdkappid: sdkAppid, priKeyContent, pubKeyContent);
            return api.genSig(identifier);
        }
        #endregion

        #region 计算IM UserSig ==>使用指定过期时间
        public string GetSig(string identifier, int expire)
        {
            TLSSigAPI api = new TLSSigAPI(1400000000, priKeyContent, pubKeyContent);
            return api.genSig(identifier, expire);
        }
        #endregion
    }
}
