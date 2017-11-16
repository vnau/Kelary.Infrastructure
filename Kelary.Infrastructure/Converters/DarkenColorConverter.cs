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
using System.Windows.Data;
using System.Windows.Media;

namespace Kelary.Infrastructure.Converters
{
    /// <summary>
    /// Makes color or brush darker. Parameter defines a percentage of the original color (0.8 by default).
    /// </summary>
    [ValueConversion(typeof(Color), typeof(Color))]
    [ValueConversion(typeof(Brush), typeof(Brush))]
    public class DarkenColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            double percentage = 0.7; // Default 
            if (parameter != null)
            {
                double.TryParse(parameter.ToString(), out percentage);
            }

            Color color;
            if (value is SolidColorBrush)
            {
                color = (value as SolidColorBrush).Color;
                return new SolidColorBrush(Color.FromRgb((byte)(color.R * percentage), (byte)(color.G * percentage), (byte)(color.B * percentage)));
            }
            else if (value is Color)
            {
                color = (Color)value;
                return Color.FromRgb((byte)(color.R * percentage), (byte)(color.G * percentage), (byte)(color.B * percentage));
            }

            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }
}
