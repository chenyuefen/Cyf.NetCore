using System;

namespace TM.Core.Extensions
{
    /// <summary>
    /// 枚举转实体
    /// </summary>
    [Serializable]
    public class EnumberEntity
    {
        /// <summary>  
        /// 枚举名称  
        /// </summary>  
        public string Key { set; get; }

        /// <summary>  
        /// 枚举对象的值  
        /// </summary>  
        public int Value { set; get; }

        /// <summary>
        /// 描述
        /// </summary>
        public string Name { get; set; }

        /// <summary>  
        /// 枚举的描述
        /// </summary>  
        public string Desction { set; get; }

        /// <summary>  
        /// 拓展字段
        /// </summary>  
        public string Expand { set; get; }
    }
}
