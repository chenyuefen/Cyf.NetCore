using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;

namespace Helpers
{
    //public class SwaggerEnumFilter : IDocumentFilter
    //{
    //    public void Apply(SwaggerDocument swaggerDoc, DocumentFilterContext context)
    //    {
    //        throw new NotImplementedException();
    //    }
    //}

    ///// <summary>
    ///// Add enum value descriptions to Swagger
    ///// </summary>
    //public class SwaggerEnumFilter : IDocumentFilter
    //{
    //    public void Apply(SwaggerDocument swaggerDoc, DocumentFilterContext context)
    //    {
    //        Dictionary<string, Type> dict = GetAllEnum();
    //        foreach (var item in swaggerDoc.Definitions)
    //        {
    //            var property = item.Value;
    //            var typeName = item.Key;
    //            Type itemType = null;
    //            if (property.Enum != null && property.Enum.Count > 0)
    //            {
    //                if (dict.ContainsKey(typeName))
    //                {
    //                    itemType = dict[typeName];
    //                }
    //                else
    //                {
    //                    itemType = null;
    //                }
    //                List<OpenApiInteger> list = new List<OpenApiInteger>();
    //                foreach (var val in property.Enum)
    //                {
    //                    list.Add((OpenApiInteger)val);
    //                }
    //                property.Description += DescribeEnum(itemType, list);
    //            }
    //        }
    //    }

    //    private static Dictionary<string, Type> GetAllEnum()
    //    {
    //        return new Dictionary<string, Type>();
    //        Assembly ass = Assembly.Load("Test.Model");
    //        Type[] types = ass.GetTypes();
    //        Dictionary<string, Type> dict = new Dictionary<string, Type>();

    //        foreach (Type item in types)
    //        {
    //            if (item.IsEnum)
    //            {
    //                dict.Add(item.Name, item);
    //            }
    //        }
    //        return dict;
    //    }

    //    private static string DescribeEnum(Type type, List<OpenApiInteger> enums)
    //    {
    //        var enumDescriptions = new List<string>();
    //        foreach (var item in enums)
    //        {
    //            if (type == null) continue;
    //            var value = Enum.Parse(type, item.Value.ToString());
    //            var desc = GetDescription(type, value);

    //            if (string.IsNullOrEmpty(desc))
    //                enumDescriptions.Add($"{item.Value.ToString()}:{Enum.GetName(type, value)}; ");
    //            else
    //                enumDescriptions.Add($"{item.Value.ToString()}:{Enum.GetName(type, value)},{desc}; ");

    //        }
    //        return $"<br/>{Environment.NewLine}{string.Join("<br/>" + Environment.NewLine, enumDescriptions)}";
    //    }

    //    private static string GetDescription(Type t, object value)
    //    {
    //        foreach (MemberInfo mInfo in t.GetMembers())
    //        {
    //            if (mInfo.Name == t.GetEnumName(value))
    //            {
    //                foreach (Attribute attr in Attribute.GetCustomAttributes(mInfo))
    //                {
    //                    if (attr.GetType() == typeof(DescriptionAttribute))
    //                    {
    //                        return ((DescriptionAttribute)attr).Description;
    //                    }
    //                }
    //            }
    //        }
    //        return string.Empty;
    //    }
    //}
}