#region Disclaimer / License
// Copyright (c) 2017 The Kelary Team
//
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
// 
// The above copyright notice and this permission notice shall be included in all
// copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
// SOFTWARE.
#endregion

using System;

namespace Kelary.Infrastructure.Converters
{
#if NETFX_CORE
    using Windows.UI.Xaml.Data;
    using Windows.UI.Xaml;
#else
    using System.Windows;
    using System.Windows.Data;
#endif

    /// <summary>
    /// The Enum value to Visibility converter.
    /// Use parameter to set Enum value for Visibility.
    /// </summary>
    public abstract class EqualToDiscreteConverter : IValueConverter
    {

        private object Positive = Visibility.Visible;
        private object Negative = Visibility.Collapsed;

        public EqualToDiscreteConverter(object Positive, object Negative)
        {
            this.Positive = Positive;
            this.Negative = Negative;
        }

#if NETFX_CORE

        public object Convert(object value, Type targetType, object parameter, string language)
        {
            return InternalConvert(value, targetType, parameter);
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            return InternalConvertBack(value, targetType, parameter);
        }

#else
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return InternalConvert(value, targetType, parameter);
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return InternalConvertBack(value, targetType, parameter);
        }

#endif

        private object InternalConvert(object value, Type targetType, object parameter)
        {
            try
            {
                if (parameter == null)
                    return value == null ? Positive : Negative;

                // Comparison by strings allow to specify enums and integers as string in ConverterParameter
                return parameter.ToString() == value.ToString() ? Positive : Negative;
                //return parameter.Equals(value) ? Positive : Negative;
            }
            catch (Exception)
            {
            }
            return Negative;
        }

        public object InternalConvertBack(object value, Type targetType, object parameter)
        {
            if (parameter == null && value != null && value.Equals(Positive))
                return null;
            return value.Equals(Positive) ? parameter : Binding.DoNothing;
        }
    }
}
