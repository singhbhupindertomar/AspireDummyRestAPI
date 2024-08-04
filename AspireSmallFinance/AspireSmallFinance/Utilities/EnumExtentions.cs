using System.ComponentModel;
using System.Reflection;

namespace AspireSmallFinance.Utilities
{
    public static class EnumExtentions
    {
        public static string GetDescription(this Enum value)
        {
            try
            {
                var info = value.GetType().GetField(value.ToString());
                if (info != null)
                {
                    var attribs = (DescriptionAttribute[])info.GetCustomAttributes(typeof(DescriptionAttribute), false);
                    if (attribs != null && attribs.Length > 0)
                    {
                        return attribs[0].Description;
                    }
                    else
                    {
                        return value.ToString();
                    }
                }
                else
                {
                    return value.ToString();
                }
            }
            catch
            {
                return string.Empty;
            }
        }
    }
}
