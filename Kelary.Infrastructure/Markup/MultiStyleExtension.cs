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
using System.Windows;
using System.Windows.Markup;

namespace Kelary.Infrastructure.Markup
{
    /// <summary>
    /// MultiStyleExtension used to merge multiple existing styles.
    /// </summary>
    /// 
    /// * Example:
    /// 	<Window xmlns:local="clr-namespace:FlagstoneRe.WPF.Utilities.UI">
    /// 		<Window.Resources>
    /// 			<Style x:Key="ButtonStyle" TargetType="Button">
    /// 					<Setter Property = "Width" Value="120" />
    /// 					<Setter Property = "Height" Value="25" />
    /// 					<Setter Property = "FontSize" Value="12" />
    /// 			</Style>
    /// 			<Style x:Key="GreenButtonStyle" TargetType="Button">
    /// 					<Setter Property = "Foreground" Value="Green" />
    /// 			</Style>
    /// 			<Style x:Key="RedButtonStyle" TargetType="Button">
    /// 					<Setter Property = "Foreground" Value="Red" />
    /// 			</Style>
    /// 			<Style x:Key="BoldButtonStyle" TargetType="Button">
    /// 					<Setter Property = "FontWeight" Value="Bold" />
    /// 			</Style>
    /// 		</Window.Resources>
    /// 
    /// 		<Button Style = "{local:MultiStyle ButtonStyle GreenButtonStyle}" Content="Green Button" />
    /// 		<Button Style = "{local:MultiStyle ButtonStyle RedButtonStyle}" Content="Red Button" />
    /// 		<Button Style = "{local:MultiStyle ButtonStyle GreenButtonStyle BoldButtonStyle}" Content="green, bold button" />
    /// 		<Button Style = "{local:MultiStyle ButtonStyle RedButtonStyle BoldButtonStyle}" Content="red, bold button" />
    /// 
    /// * Notice how the syntax is just like using multiple CSS classes.
    /// * The current default style for a type can be merged using the "." syntax:
    /// 
    /// 		<Button Style = "{local:MultiStyle . GreenButtonStyle BoldButtonStyle}" Content="Bold Green Button" />
    [MarkupExtensionReturnType(typeof(Style))]
    public class MultiStyleExtension : MarkupExtension
    {
        private string[] resourceKeys;

        /// <summary>
        /// Public constructor.
        /// </summary>
        /// <param name="inputResourceKeys">The constructor input should be a string consisting of one or more style names separated by spaces.</param>
        public MultiStyleExtension(string inputResourceKeys)
        {
            if (inputResourceKeys == null)
                throw new ArgumentNullException("inputResourceKeys");
            this.resourceKeys = inputResourceKeys.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            if (this.resourceKeys.Length == 0)
                throw new ArgumentException("No input resource keys specified.");
        }

        /// <summary>
        /// Returns a style that merges all styles with the keys specified in the constructor.
        /// </summary>
        /// <param name="serviceProvider">The service provider for this markup extension.</param>
        /// <returns>A style that merges all styles with the keys specified in the constructor.</returns>
        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            // Styles could not be found in design-time.
            // So if in design-mode return null
            if (System.ComponentModel.DesignerProperties.GetIsInDesignMode(new DependencyObject()))
                return null;

            Style resultStyle = new Style();
            foreach (string currentResourceKey in resourceKeys)
            {
                object key = currentResourceKey;
                if (currentResourceKey == ".")
                {
                    var service = (IProvideValueTarget)serviceProvider.GetService(typeof(IProvideValueTarget));
                    key = (service.TargetObject is Style)
                        ? ((Style)service.TargetObject).TargetType
                        : service.TargetObject.GetType();
                }
                Style currentStyle = new StaticResourceExtension(key).ProvideValue(serviceProvider) as Style;
                if (currentStyle == null)
                    throw new InvalidOperationException("Could not find style with resource key " + currentResourceKey + ".");
                resultStyle.Merge(currentStyle);
            }
            return resultStyle;
        }
    }

    public static class MultiStyleMethods
    {
        /// <summary>
        /// Merges the two styles passed as parameters. The first style will be modified to include any 
        /// information present in the second. If there are collisions, the second style takes priority.
        /// </summary>
        /// <param name="style1">First style to merge, which will be modified to include information from the second one.</param>
        /// <param name="style2">Second style to merge.</param>
        public static void Merge(this Style style1, Style style2)
        {
            if (style1 == null)
                throw new ArgumentNullException("style1");
            if (style2 == null)
                throw new ArgumentNullException("style2");
            if (style1.TargetType.IsAssignableFrom(style2.TargetType))
                style1.TargetType = style2.TargetType;
            if (style2.BasedOn != null)
                Merge(style1, style2.BasedOn);
            foreach (SetterBase currentSetter in style2.Setters)
                style1.Setters.Add(currentSetter);
            foreach (TriggerBase currentTrigger in style2.Triggers)
                style1.Triggers.Add(currentTrigger);
            // This code is only needed when using DynamicResources.
            foreach (object key in style2.Resources.Keys)
                style1.Resources[key] = style2.Resources[key];
        }
    }
}
