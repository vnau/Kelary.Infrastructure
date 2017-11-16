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

namespace Kelary.Infrastructure.Converters
{
    /// <summary>
    /// Checks whether enum is equal to specified value.
    /// Value specified by parameter.
    /// </summary>
    [ValueConversion(typeof(object), typeof(bool))]
    public sealed class EqualToBooleanConverter : EqualToDiscreteConverter
    {
        public EqualToBooleanConverter() : base(true, false)
        {
        }
    }


    [Obsolete("Class EnumIsEqualToValueConverter is deprecated. Use EqualToBooleanConverter instead.", true)]
    public sealed class EnumIsEqualToValueConverter : EqualToDiscreteConverter
    {
        public EnumIsEqualToValueConverter() : base(true, false)
        {
        }
    }

}
