using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;

namespace MVCUI.Helpers
{
    public static class EnumHelper
    {
        public static IEnumerable<SelectListItem> GetItems(this Type enumType, Int32? selectedValue)
        {
            if (!typeof(Enum).IsAssignableFrom(enumType))
                throw new ArgumentException("This is Failed. Type Must To Be ENUM");

            String[] names = Enum.GetNames(enumType: enumType);
            IEnumerable<Int32> values = Enum.GetValues(enumType: enumType).Cast<Int32>();
            IEnumerable<SelectListItem> items = names.Zip(values, (name, value) =>
                   new SelectListItem
                   {
                       Text = GetName(enumType, name),
                       Value = value.ToString(),
                       Selected = value == selectedValue
                   }
               );
            return items;
        }

        private static String GetName(Type enumType, String name)
        {
            String result = name;
            DisplayAttribute attr = enumType
                .GetField(name)
                .GetCustomAttributes(inherit: false)
                .OfType<DisplayAttribute>()
                .FirstOrDefault();
            if (attr != null)
                result = attr.GetName();
            return result;
        }

        public static String GetEnumDescription(Type enumType, String value)
        {
            String name = Enum.GetNames(enumType).Where(x => x.Equals(value, StringComparison.CurrentCultureIgnoreCase)).Select(x => x).FirstOrDefault();
            if (name.Equals(null))
                return String.Empty;
            FieldInfo field = enumType.GetField(name);
            Object[] customAttr = field.GetCustomAttributes(typeof(DescriptionAttribute), false);
            return customAttr.Length > 0 ? ((DescriptionAttribute)customAttr[0]).Description : name;
        }
    }
}
