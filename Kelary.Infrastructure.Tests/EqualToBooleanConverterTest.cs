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

namespace Kelary.Infrastructure.Tests
{
    using System.Windows.Data;
    using TestEnum = ActionTargets;

    [TestFixture]
    public class EqualToBooleanConverterTest
    {
        [TestCase(TestEnum.Default, TestEnum.Default, true)]
        [TestCase(TestEnum.Default, TestEnum.Test, false)]
        [TestCase(TestEnum.Test, null, false)]
        [TestCase(TestEnum.Test, null, false)]
        [TestCase(null, TestEnum.Test, false)]
        [TestCase(null, null, true)]
        [TestCase(12.4, double.NaN, false)]
        [TestCase(double.NaN, double.NaN, true)]
        public void TestEqualToBooleanConverterConvert(object Parameter, object Value, bool Result)
        {
            var Converter = new EqualToBooleanConverter();
            var value = Converter.Convert(Value, typeof(bool), Parameter, CultureInfo.InvariantCulture);
            Assert.That(value, Is.TypeOf<bool>().And.EqualTo(Result));
        }

        [TestCase(TestEnum.Test, true, TestEnum.Test)]
        [TestCase(TestEnum.Test, false, null)]
        [TestCase(null, true, null)]
        [TestCase(null, false, null)]
        public void TestEqualToBooleanConverterConvertBack(TestEnum? Parameter, bool? Value, object Result)
        {
            var Converter = new EqualToBooleanConverter();
            var value = Converter.ConvertBack(Value, typeof(TestEnum), Parameter, CultureInfo.InvariantCulture);
            if (Value == true)
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

        [TestCase(double.NaN, false, null)]
        [TestCase(double.NaN, true, double.NaN)]
        public void TestEqualToBooleanConverterConvertBack(double? Parameter, bool? Value, double Result)
        {
            var Converter = new EqualToBooleanConverter();
            var value = Converter.ConvertBack(Value, typeof(TestEnum), Parameter, CultureInfo.InvariantCulture);
            if (Value == true)
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
