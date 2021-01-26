using System;
using System.Collections.Generic;
using System.Linq;

// ReSharper disable once CheckNamespace
namespace TM.Infrastructure.Extensions
{
    /// <summary>
    /// 随机数(<see cref="Random"/>) 扩展
    /// </summary>
    public static class RandomExtensions
    {
        #region NextLong(获取下一个随机数)

        /// <summary>
        /// 获取下一个随机数。范围：[0,long.MaxValue]
        /// </summary>
        /// <param name="random">范围</param>
        /// <returns></returns>
        public static long NextLong(this Random random) => random.NextLong(0, long.MaxValue);

        /// <summary>
        /// 获取下一个随机数。范围：[0,max]
        /// </summary>
        /// <param name="random">随机数</param>
        /// <param name="max">最大值</param>
        /// <returns></returns>
        public static long NextLong(this Random random, long max) => random.NextLong(0, max);

        /// <summary>
        /// 获取下一个随机数。范围：[min,max]
        /// </summary>
        /// <param name="random">随机数</param>
        /// <param name="min">最小值</param>
        /// <param name="max">最大值</param>
        /// <returns></returns>
        public static long NextLong(this Random random, long min, long max)
        {
            var buf = new byte[8];
            random.NextBytes(buf);
            var longRand = BitConverter.ToInt64(buf, 0);
            return Math.Abs(longRand % (max - min)) + min;
        }

        #endregion

        #region NextDouble(获取下一个随机数)

        /// <summary>
        /// 获取下一个随机数。范围：[0.0,max]
        /// </summary>
        /// <param name="random">随机数</param>
        /// <param name="max">最大值</param>
        /// <returns></returns>
        public static double NextDouble(this Random random, double max) => random.NextDouble() * max;

        /// <summary>
        /// 获取下一个随机数。范围：[min,max]
        /// </summary>
        /// <param name="random">随机数</param>
        /// <param name="min">最小值</param>
        /// <param name="max">最大值</param>
        /// <returns></returns>
        public static double NextDouble(this Random random, double min, double max) =>
            random.NextDouble() * (max - min) + min;

        #endregion

        #region NormalDouble(标准正态分布生成随机双精度浮点数)

        /// <summary>
        /// 标准正态分布生成随机双精度浮点数
        /// </summary>
        /// <param name="random">随机数</param>
        /// <returns></returns>
        public static double NormalDouble(this Random random)
        {
            var u1 = random.NextDouble();
            var u2 = random.NextDouble();
            return Math.Sqrt(-2 * Math.Log(u1)) * Math.Cos(2 * Math.PI * u2);
        }

        /// <summary>
        /// 标准正态分布生成随机双精度浮点数
        /// </summary>
        /// <param name="random">随机数</param>
        /// <param name="mean">均值</param>
        /// <param name="deviation">偏差</param>
        /// <returns></returns>
        public static double NormalDouble(this Random random, double mean, double deviation) =>
            mean + deviation * random.NormalDouble();

        #endregion

        #region NextFloat(获取下一个随机数)

        /// <summary>
        /// 获取下一个随机数。范围：[0.0,1.0]
        /// </summary>
        /// <param name="random">随机数</param>
        /// <returns></returns>
        public static float NextFloat(this Random random) => (float)random.NextDouble();

        /// <summary>
        /// 获取下一个随机数。范围：[0.0,max]
        /// </summary>
        /// <param name="random">随机数</param>
        /// <param name="max">最大值</param>
        /// <returns></returns>
        public static float NextFloat(this Random random, float max) => (float)(random.NextDouble() * max);

        /// <summary>
        /// 获取下一个随机数。范围：[min,max]
        /// </summary>
        /// <param name="random">随机数</param>
        /// <param name="min">最小值</param>
        /// <param name="max">最大值</param>
        /// <returns></returns>
        public static float NextFloat(this Random random, float min, float max) =>
            (float)(random.NextDouble() * (max - min) + min);

        #endregion

        #region NormalFloat(标准正态分布生成随机单精度浮点数)

        /// <summary>
        /// 标准正态分布生成随机单精度浮点数
        /// </summary>
        /// <param name="random">随机数</param>
        /// <returns></returns>
        public static float NormalFloat(this Random random) => (float)random.NormalDouble();

        /// <summary>
        /// 标准正态分布生成随机单精度浮点数
        /// </summary>
        /// <param name="random">随机数</param>
        /// <param name="mean">均值</param>
        /// <param name="deviation">偏差</param>
        /// <returns></returns>
        public static float NormalFloat(this Random random, float mean, float deviation) =>
            mean + (float)(deviation * random.NormalDouble());

        #endregion

        #region NextSign(获取下一个随机数)

        /// <summary>
        /// 获取下一个随机数。范围：[-1,1]
        /// </summary>
        /// <param name="random">随机数</param>
        /// <returns></returns>
        public static int NextSign(this Random random) => 2 * random.Next(2) - 1;

        #endregion

        #region NextBool(获取下一个随机数)

        /// <summary>
        /// 获取下一个随机数。范围：[true,false]
        /// </summary>
        /// <param name="random">随机数</param>
        /// <returns></returns>
        public static bool NextBool(this Random random) => random.NextDouble() < 0.5;

        /// <summary>
        /// 获取下一个随机数。范围：[true,false]
        /// </summary>
        /// <param name="random">随机数</param>
        /// <param name="probability">true的概率。范围：[0.0,1.0]</param>
        /// <returns></returns>
        public static bool NextBool(this Random random, double probability) => random.NextDouble() < probability;

        #endregion

        /// <summary>
        /// 获取随机数
        /// </summary>
        /// <returns></returns>
        public static int GetRandom()
        {
            Random random = new Random();
            var randomNum = random.Next(999999) % 900000 + 100000;

            return randomNum;
        }

        /// <summary>
        /// 获取奇偶个数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="isOdd">是否偶数</param>
        /// <returns></returns>
        public static IList<T> TakeParity<T>(this IList<T> list, bool isEven = true)
        {
            var curentOdd = list.Count % 2 == 0;
            if (curentOdd == isEven)
                return list;
            list.RemoveAt(list.Count - 1);
            return list;
        }

        /// <summary>
        /// 随机取前N个
        /// </summary>
        public static IList<T> RandomTake<T>(this IList<T> list,int n)
        {
            return list.RandomSort()?.Take(n)?.ToList();
        }
        
        /// <summary>
        /// 随机重新排序
        /// </summary>
        public static IList<T> RandomSort<T>(this IList<T> list)
        {
            if (list?.Count > 0)
            {
                list = list.OrderBy(x => Guid.NewGuid()).ToList();
            }
            return list;
        }

        /// <summary>
        /// 随机获取List中的某一对象
        /// </summary>
        public static T GetRandomMember<T>(this IList<T> @this)
        {
            var random = new Random();
            return @this[random.Next(0, @this.Count - 1)];
        }
    }
}
