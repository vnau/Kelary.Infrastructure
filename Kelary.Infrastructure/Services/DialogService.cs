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
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace Kelary.Infrastructure.Services
{
	/// <summary>
	/// Defining how dialogs should be displayed in Windows.
	/// 100% compatible with MVVM Light IDialogService.
	/// </summary>
	public class DialogService : IDialogService
	{
		public async Task ShowError(Exception error, string title, string buttonText, Action afterHideCallback)
		{
			await ShowMessageBoxInternal(error.Message, title, afterHideCallback, MessageBoxImage.Error);
		}

		public async Task ShowError(string message, string title, string buttonText, Action afterHideCallback)
		{
			await ShowMessageBoxInternal(message, title, afterHideCallback, MessageBoxImage.Error);
		}

		public async Task ShowMessage(string message, string title)
		{
			await ShowMessageBoxInternal(message, title, null, MessageBoxImage.Information);
		}

		public async Task ShowMessage(string message, string title, string buttonText, Action afterHideCallback)
		{
			await ShowMessageBoxInternal(message, title, afterHideCallback, MessageBoxImage.Information);
		}

		public async Task<bool> ShowMessage(string message, string title, string buttonConfirmText, string buttonCancelText, Action<bool> afterHideCallback)
		{
			var MainWindow = Application.Current.MainWindow;

			if (!MainWindow.IsVisible)
			{
				var dialogResult = MessageBox.Show(message, title, buttonConfirmText == "Yes" ? MessageBoxButton.YesNo : MessageBoxButton.OKCancel, MessageBoxImage.Question);
				bool result = (dialogResult == MessageBoxResult.OK || dialogResult == MessageBoxResult.Yes);
				afterHideCallback?.Invoke(result);
				return result;
			}
			return await Application.Current.Dispatcher.InvokeAsync<bool>(() =>
			{
				var dialogResult = MessageBox.Show(MainWindow, message, title, buttonConfirmText == "Yes" ? MessageBoxButton.YesNo : MessageBoxButton.OKCancel, MessageBoxImage.Question);
				bool result = (dialogResult == MessageBoxResult.OK || dialogResult == MessageBoxResult.Yes);
				afterHideCallback?.Invoke(result);
				return result;
			});
		}

		public async Task ShowMessageBox(string message, string title)
		{
			await ShowMessageBoxInternal(message, title, null, MessageBoxImage.Information);
		}

		private async Task ShowMessageBoxInternal(string message, string title, Action afterHideCallback, MessageBoxImage Image)
		{
			var MainWindow = Application.Current.Windows.OfType<Window>().SingleOrDefault(x => x.IsActive);

			if (MainWindow == null || !MainWindow.IsVisible)
			{
				// Show synchronous if main window is invisible.
				MessageBox.Show(message, title, MessageBoxButton.OK, MessageBoxImage.Information);
				afterHideCallback?.Invoke();
			}
			else
				await Application.Current.Dispatcher.InvokeAsync(() =>
				{
					MessageBox.Show(MainWindow, message, title, MessageBoxButton.OK, Image);
					afterHideCallback?.Invoke();
				});
		}

	}
}
