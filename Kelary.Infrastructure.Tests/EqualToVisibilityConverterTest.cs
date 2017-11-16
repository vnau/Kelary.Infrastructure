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

using Kelary.Infrastructure.Converters;
using NUnit.Framework;
using System.Globalization;
using System.Windows;

using System.Windows.Data;

using TestEnum = NUnit.Framework.ActionTargets;

namespace Kelary.Infrastructure.Tests
{

    [TestFixture]
    public class EqualToVisibilityConverterTest
    {
        [TestCase(TestEnum.Default, TestEnum.Default, Visibility.Visible)]
        [TestCase(TestEnum.Default, TestEnum.Test, Visibility.Collapsed)]
        [TestCase(TestEnum.Test, null, Visibility.Collapsed)]
        [TestCase(TestEnum.Test, null, Visibility.Collapsed)]
        [TestCase(null, TestEnum.Test, Visibility.Collapsed)]
        [TestCase(null, null, Visibility.Visible)]
        [TestCase(12.4, double.NaN, Visibility.Collapsed)]
        [TestCase(double.NaN, double.NaN, Visibility.Visible)]
        public void TestEqualToVisibilityConverterConvert(object Parameter, object Value, Visibility Result)
        {
            var Converter = new EqualToVisibilityConverter();
            var value = Converter.Convert(Value, typeof(Visibility), Parameter, CultureInfo.InvariantCulture);
            Assert.That(value, Is.TypeOf<Visibility>().And.EqualTo(Result));
        }

        [TestCase(TestEnum.Test, Visibility.Visible, TestEnum.Test)]
        [TestCase(TestEnum.Test, Visibility.Collapsed, null)]
        [TestCase(null, Visibility.Visible, null)]
        [TestCase(null, Visibility.Collapsed, null)]
        public void TestEqualToVisibilityConverterConvertBack(TestEnum? Parameter, Visibility? Value, object Result)
        {
            var Converter = new EqualToVisibilityConverter();
            var value = Converter.ConvertBack(Value, typeof(TestEnum), Parameter, CultureInfo.InvariantCulture);
            if (Value == Visibility.Visible)
            {
                if (Parameter != null)
                    Assert.That(value, Is.TypeOf<TestEnum>());

                Assert.That(value, Is.EqualTo(Result));
            }
            else
            {
                Assert.That(value, Is.TypeOf(Binding.DoNothing.GetType()).And.EqualTo(Binding.DoNothing));
            }
        }

        [TestCase(double.NaN, Visibility.Collapsed, null)]
        [TestCase(double.NaN, Visibility.Visible, double.NaN)]
        public void TestEqualToVisibilityConverterConvertBack(double? Parameter, Visibility? Value, double Result)
        {
            var Converter = new EqualToVisibilityConverter();
            var value = Converter.ConvertBack(Value, typeof(TestEnum), Parameter, CultureInfo.InvariantCulture);
            if (Value == Visibility.Visible)
            {
                if (Parameter != null)
                    Assert.That(value, Is.TypeOf<double>());

                Assert.That(value, Is.EqualTo(Result));
            }
            else
            {
                Assert.That(value, Is.TypeOf(Binding.DoNothing.GetType()).And.EqualTo(Binding.DoNothing));
            }
        }

    }

}
