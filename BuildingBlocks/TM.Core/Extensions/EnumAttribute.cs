using System;

namespace TM.Core.Extensions
{
    /// <summary>
    /// 名    称：描述枚举的属性
    /// </summary>
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = false, Inherited = true)]
    public class EnumAttribute : Attribute
    {
        private string _name;
        private string _description;
        private string _expand;

        /// <summary>
        /// 枚举名称
        /// </summary>
        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        /// <summary>
        /// 枚举描述
        /// </summary>
        public string Description
        {
            get { return _description; }
            set { _description = value; }
        }

        /// <summary>
        /// 拓展
        /// </summary>
        public string Expand
        {
            get { return _expand; }
            set { _expand = value; }
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="name">枚举名称</param>
        public EnumAttribute(string name)
        {
            this.Name = name;
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="name">枚举名称</param>
        /// <param name="description">枚举描述</param>
        public EnumAttribute(string name, string description)
        {
            this.Name = name;
            this.Description = description;
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="name">枚举名称</param>
        /// <param name="description">枚举描述</param>
        /// <param name="expand">拓展</param>
        public EnumAttribute(string name, string description, string expand)
        {
            this.Name = name;
            this.Description = description;
            this.Expand = expand;
        }

    }
}
