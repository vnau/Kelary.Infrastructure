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
using System.Diagnostics;
using System.Linq;
using System.Windows.Data;
using System.Windows.Media;

namespace Kelary.Infrastructure.Converters
{
    /// <summary>
    /// Perform linear conversion a0 + x * a1 for value.
    /// </summary>
    public class LinearTransformConverter : IValueConverter
    {
        public DoubleCollection Coefficients { get; set; }

        public LinearTransformConverter()
        {
            Coefficients = new DoubleCollection(new double[] { 0, 1 });
        }

        /// <summary>
        /// Perform linear conversion a0 + x * a1 for value.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="targetType"></param>
        /// <param name="parameter"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            double x = GetDoubleValue(value, 0.0);
            var coeff = GetCoefficients(parameter);
            return coeff[0] + x * coeff[1];
        }

        /// <summary>
        /// Back conversion.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="targetType"></param>
        /// <param name="parameter"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            double y = GetDoubleValue(value, 0.0);
            var coeff = GetCoefficients(parameter);
            return (y - coeff[0]) / coeff[1];
        }

        /// <summary>
        /// Get coeffitients from parameter.
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        private double[] GetCoefficients(object parameter)
        {
            if (parameter != null)
            {
                if (parameter is string)
                {

                    var str = parameter as string;
                    var vals = str.Split(',').Select(v => double.Parse(v, System.Globalization.CultureInfo.InvariantCulture)).ToArray();
                    Debug.Assert(vals.Length >= 2);
                    return vals;
                }

            }

            return Coefficients.ToArray();
        }

        /// <summary>
        /// Converts object to double value.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        private static double GetDoubleValue(object value, double defaultValue)
        {
            if (value != null)
                try
                {
                    return System.Convert.ToDouble(value);
                }
                catch
                {
                }

            return defaultValue;
        }
    }
}
