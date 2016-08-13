using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace CnBlogs.Common
{
    public static class ControlHelper
    {
        public static T GetAttachedPropertyValue<T>(this Control control, Type owerControlType, string dependencyPropertyName)
        {
            DependencyProperty dependencyProperty = DependencyProperty.RegisterAttached(dependencyPropertyName, typeof(T), owerControlType,new PropertyMetadata(0));
            return (T)control.GetValue(dependencyProperty);
        }
        public static void SetAttachedPropertyValue<T>(this Control control, Type owerControlType, string dependencyPropertyName,T value)
        {
            DependencyProperty dependencyProperty = DependencyProperty.RegisterAttached(dependencyPropertyName, typeof(T), owerControlType, new PropertyMetadata(0));
            control.SetValue(dependencyProperty, value);
        }
    }
}
