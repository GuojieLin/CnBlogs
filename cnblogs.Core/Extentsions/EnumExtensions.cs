using CnBlogs.Core.Extentsions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace CnBlogs.Core.Extentsions
{
    public static class EnumExtensions
    {
        public static string GetDisplayName(this Enum e)
        {
            return e.GetAttribute<DisplayAttribute>().Name;
        }
        public static TAttribute GetAttribute<TAttribute>(this Enum enumValue)
           where TAttribute : Attribute
        {
            return enumValue.GetType()
                            .GetMember(enumValue.ToString())
                            .First()
                            .GetCustomAttribute<TAttribute>();
        }
    }
}
