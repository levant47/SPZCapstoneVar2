using System;
using System.ComponentModel;

namespace SPZCapstoneVar2.Utilities
{
    public static class EnumUtilities
    {
        public static string? GetDescription(this Enum value)
        {
            var type = value.GetType();
            var name = Enum.GetName(type, value);
            if (name == null)
            {
                return null;
            }
            var field = type.GetField(name);
            if (field == null)
            {
                return null;
            }
            return (Attribute.GetCustomAttribute(field, typeof(DescriptionAttribute)) as DescriptionAttribute)
                ?.Description;
        }
    }
}
