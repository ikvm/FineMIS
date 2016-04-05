using System;
using System.ComponentModel;

namespace FineMIS
{
    /// <summary>
    /// 枚举类型值的显示样式
    /// </summary>
    public class CssClassAttribute : Attribute
    {
        public CssClassAttribute(string cssClass)
        {
            _cssClass = cssClass;
        }

        // Keep a variable internally ...
        protected string _cssClass;

        // .. and show a copy to the outside world.
        public string CssClass
        {
            get { return _cssClass; }
            set { _cssClass = CssClass; }
        }
    }

    public abstract class EnumHelper
    {
        /// <summary>
        /// 通过传入的枚举值获取枚举值对应的中文名称
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string GetDescName<TEnum>(TEnum value)
        {
            Type type = typeof(TEnum);

            if (type.IsEnum == false) { return ""; }

            Type typeDescription = typeof(DescriptionAttribute);
            System.Reflection.FieldInfo field = type.GetField(Enum.GetName(type, value));

            string strText = string.Empty;
            object[] arr = field.GetCustomAttributes(typeDescription, true);
            if (arr.Length > 0)
            {
                strText = (arr[0] as DescriptionAttribute).Description;
            }
            else
            {
                strText = field.Name;
            }

            return strText;
        }

        public static string GetDescName<TEnum>(int value)
        {
            Type type = typeof(TEnum);

            if (type.IsEnum == false) { return ""; }

            Type typeDescription = typeof(DescriptionAttribute);
            System.Reflection.FieldInfo field = type.GetField(Enum.GetName(type, value));

            string strText = string.Empty;
            object[] arr = field.GetCustomAttributes(typeDescription, true);
            if (arr.Length > 0)
            {
                strText = (arr[0] as DescriptionAttribute).Description;
            }
            else
            {
                strText = field.Name;
            }

            return strText;
        }

        /// <summary>
        /// 通过传入的枚举值获取枚举值对应的显示样式
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string GetCssClass<TEnum>(TEnum value)
        {
            Type type = typeof(TEnum);

            if (type.IsEnum == false) { return ""; }

            Type typeCssClass = typeof(CssClassAttribute);
            System.Reflection.FieldInfo field = type.GetField(Enum.GetName(type, value));

            string strText = string.Empty;
            object[] arr = field.GetCustomAttributes(typeCssClass, true);
            if (arr.Length > 0)
            {
                strText = (arr[0] as CssClassAttribute).CssClass;
            }
            else
            {
                strText = field.Name;
            }

            return strText;
        }

        public static string GetCssClass<TEnum>(int value)
        {
            Type type = typeof(TEnum);

            if (type.IsEnum == false) { return ""; }

            Type typeCssClass = typeof(CssClassAttribute);
            System.Reflection.FieldInfo field = type.GetField(Enum.GetName(type, value));

            string strText = string.Empty;
            object[] arr = field.GetCustomAttributes(typeCssClass, true);
            if (arr.Length > 0)
            {
                strText = (arr[0] as CssClassAttribute).CssClass;
            }
            else
            {
                strText = field.Name;
            }

            return strText;
        }
    }
}
