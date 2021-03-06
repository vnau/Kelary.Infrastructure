﻿#region Disclaimer / License
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
using System.Reflection;
using System.Windows.Data;

namespace Kelary.Infrastructure.Converters
{
    /// <summary>
    /// Converts class instance to value returned by its method.
    /// Method name specified with ConverterParameter.
    /// Method should have no arguments.
    /// </summary>
    public class MethodToValueConverter : IValueConverter
    {
        /// <summary>
        /// Converts class instance to value returned by its method.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="targetType"></param>
        /// <param name="parameter">Method name (method should have no argumants).</param>
        /// <param name="culture"></param>
        /// <returns></returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var methodName = parameter as string;
            if (value == null || methodName == null)
                return value;

            try
            {
                return value.GetType().InvokeMember(methodName, BindingFlags.OptionalParamBinding | BindingFlags.InvokeMethod, null, value, new Type[0]);
            }
            catch
            {
                // Method not found.
                return value;
            }
        }

        /// <summary>
        /// Back conversion stub.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="targetType"></param>
        /// <param name="parameter"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException("MethodToValueConverter can only be used for one way conversion.");
        }
    }
}
