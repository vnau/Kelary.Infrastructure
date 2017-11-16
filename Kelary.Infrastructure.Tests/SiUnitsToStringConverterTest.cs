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

namespace UnitTests
{
    [TestFixture]
    class FormatSiTest
    {
        [TestCase(0, "s", "0s")]
        [TestCase(-999.12, "m", "-999.12m")]
        [TestCase(32.123456789e9, "Hz", "32.123456789GHz")]
        [TestCase(17.1e-6, "m", "17.1µm")]
        [TestCase(3e-17, "s", "0.03fs")]
        [TestCase(12e15, "Hz", "12000THz")]
        public void TestConversionInvariant(double Value, string Units, string Result)
        {
            Assert.AreEqual(Result, SiUnitsToStringConverter.Convert(Value, Units, CultureInfo.InvariantCulture, ""));
        }

        [TestCase(-999.12, "м", "-999,12м")]
        [TestCase(32.123456789e9, "Гц", "32,123456789ГГц")]
        [TestCase(17.1e-6, "м", "17,1мкм")]
        [TestCase(3e-17, "с", "0,03фс")]
        [TestCase(12e15, "Гц", "12000ТГц")]
        public void TestConversionRussian(double Value, string Units, string Result)
        {
            Assert.AreEqual(Result, SiUnitsToStringConverter.Convert(Value, Units, CultureInfo.CreateSpecificCulture("ru"), ""));
        }

    }
}
