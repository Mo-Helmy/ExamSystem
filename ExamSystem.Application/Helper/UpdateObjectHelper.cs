using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ExamSystem.Application.Helper
{
    public static class UpdateObjectHelper
    {
        /// <summary>
        /// Update the target object from properties of the source object
        /// </summary>
        /// <param name="target">Current object need update.</param>
        /// <param name="source">New object that will update the target object.</param>
        public static void UpdateObject(object target, object source)
        {
            Type targetType = target.GetType();
            Type sourceType = source.GetType();

            PropertyInfo[] sourceProperties = sourceType.GetProperties();
            foreach (PropertyInfo sourceProperty in sourceProperties)
            {
                PropertyInfo targetProperty = targetType.GetProperty(sourceProperty.Name);
                if (targetProperty != null && targetProperty.PropertyType == sourceProperty.PropertyType)
                {
                    object value = sourceProperty.GetValue(source, null);

                    if (value is not null) targetProperty.SetValue(target, value, null);
                }
            }
        }
    }
}
