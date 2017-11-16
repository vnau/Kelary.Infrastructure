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
using System.Globalization;
using System.Windows.Data;

namespace Kelary.Infrastructure.Converters
{
    /// <summary>
    /// Si units to string converter.
    /// </summary>
    [ValueConversion(typeof(double), typeof(string))]
    public class SiUnitsToStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object baseUnits, CultureInfo culture)
        {
            if (value is TimeSpan)
            {
                string BaseUnits = baseUnits as string;
                var val = (double)value;
                return Convert(val, BaseUnits, culture);
            }
            return "";
        }

        /// <summary>
        /// Back conversion not supported yet :(
        /// </summary>
        /// <param name="value"></param>
        /// <param name="targetType"></param>
        /// <param name="parameter"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }

        /// <summary>
		/// Static method for Si units conversion.
        /// </summary>
        /// <param name="Value"></param>
        /// <param name="BaseUnits"></param>
        /// <param name="Culture"></param>
        /// <param name="Delimiter"></param>
        /// <param name="PredefinedPrefix"></param>
        /// <returns></returns>
        public static string Convert(double Value, string BaseUnits = "", CultureInfo Culture = null, string Delimiter = " ", string Prefix = null)
        {
            if (Culture == null)
                Culture = CultureInfo.CurrentUICulture;

            string[] Prefixes;

            if (Culture.TwoLetterISOLanguageName == "ru")
                Prefixes = new string[] { "ф", "п", "н", "мк", "м", "", "к", "М", "Г", "Т" };
            else
                Prefixes = new string[] { "f", "p", "n", "µ", "m", "", "k", "M", "G", "T" };

            int pow;
            int MinPref = -15 / 3;

            // If default prefix specified - use it!
            if (Prefix != null)
            {
                pow = Array.IndexOf(Prefixes, Prefix) + MinPref;
            }
            // Otherwise determine optimal prefix.
            else
            {
                pow = (Value == 0) ? 0 : (int)Math.Floor(Math.Log10(Math.Abs(Value)) / 3);
                pow = Math.Min(Math.Max(pow, MinPref), MinPref + Prefixes.Length - 1);
                Prefix = Prefixes[pow - MinPref];
            }

            if (BaseUnits == "" && Prefix == "")
                Delimiter = "";

            return string.Format(Culture, "{0}{1}{2}{3}", Value / Math.Pow(10, pow * 3), Delimiter, Prefix, BaseUnits);
        }

    }
}
