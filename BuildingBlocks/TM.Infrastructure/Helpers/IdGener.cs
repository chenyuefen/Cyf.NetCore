using System;
using TM.Infrastructure.IdGenerators.Abstractions;
using TM.Infrastructure.IdGenerators.Core;

namespace TM.Infrastructure.Helpers
{
    /// <summary>
    /// Id 生成器
    /// </summary>
    public static class IdGener
    {
        /// <summary>
        /// Id
        /// </summary>
        private static string _id;

        /// <summary>
        /// Guid 生成器
        /// </summary>
        public static IGuidGenerator GuidGenerator { get; set; } = SequentialGuidGenerator.Current;

        /// <summary>
        /// Long 生成器
        /// </summary>
        public static ILongGenerator LongGenerator { get; set; } = SnowflakeIdGenerator.Current;

        /// <summary>
        /// String 生成器
        /// </summary>
        public static IStringGenerator StringGenerator { get; set; } = TimestampIdGenerator.Current;

        /// <summary>
        /// 设置Id
        /// </summary>
        /// <param name="id">Id</param>
        public static void SetId(string id)
        {
            _id = id;
        }

        /// <summary>
        /// 重置Id
        /// </summary>
        public static void Reset()
        {
            _id = null;
        }

        /// <summary>
        /// 用Guid创建标识，去掉分隔符
        /// </summary>
        /// <returns></returns>
        public static string Guid()
        {
            return string.IsNullOrWhiteSpace(_id) ? System.Guid.NewGuid().ToString("N") : _id;
        }

        /// <summary>
        /// 创建 Guid ID
        /// </summary>
        /// <returns></returns>
        public static Guid GetGuid()
        {
            return GuidGenerator.Create();
        }

        /// <summary>
        /// 创建 Long ID
        /// </summary>
        /// <returns></returns>
        public static long GetLong()
        {
            return LongGenerator.Create();
        }

        /// <summary>
        /// 创建 String ID
        /// </summary>
        /// <returns></returns>
        public static string GetString()
        {
            return StringGenerator.Create();
        }

        #region ----生成随机数----
        /// <summary>
        /// 参考：msdn上的RNGCryptoServiceProvider例子
        /// </summary>
        /// <param name="numSeeds"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        public static int NextRandom(int numSeeds, int length)
        {
            // Create a byte array to hold the random value.  
            byte[] randomNumber = new byte[length];
            // Create a new instance of the RNGCryptoServiceProvider.  
            System.Security.Cryptography.RNGCryptoServiceProvider rng = new System.Security.Cryptography.RNGCryptoServiceProvider();
            // Fill the array with a random value.  
            rng.GetBytes(randomNumber);
            // Convert the byte to an uint value to make the modulus operation easier.  
            uint randomResult = 0x0;
            for (int i = 0; i < length; i++)
            {
                randomResult |= ((uint)randomNumber[i] << ((length - 1 - i) * 8));
            }
            return (int)(randomResult % numSeeds) + 1;
        }

        /// <summary>
        /// 根据日期和随机码生成唯一号
        /// </summary>
        /// <returns></returns>
        public static string GetGenerateDateNumber()
        {
            var time = DateTime.Now.ToString("yyyyMMddHHmmssfff");
            return time + NextRandom(1000, 2).ToString();
        }
        #endregion
    }
}
