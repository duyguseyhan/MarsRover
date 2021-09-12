using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MarsRoverCaseStudy.Utils
{
    public class EnumHelper
    {
        public static string GetSringValue(Enum value)
        {
            string output = null;
            var type = value.GetType();

            var fi = type.GetField(value.ToString());

            if (fi.GetCustomAttributes(typeof(StringValue), false) is StringValue[] attrs && attrs.Length > 0)
            {
                output = attrs[0].Value;
            }

            return output;
        }
    }
}
