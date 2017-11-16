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
using System.Threading.Tasks;

namespace Kelary.Infrastructure.Services
{
    /// <summary>
    /// An interface defining how dialogs should be displayed in various frameworks such
    /// as Windows, Windows Phone, Android, iOS etc.
    /// 100% compatible with MVVM Light IDialogService.
    /// </summary>
    public interface IDialogService
    {
        //
        // Summary:
        //     Displays information about an error.
        //
        // Parameters:
        //   message:
        //     The message to be shown to the user.
        //
        //   title:
        //     The title of the dialog box. This may be null.
        //
        //   buttonText:
        //     The text shown in the only button in the dialog box. If left null, the text "OK"
        //     will be used.
        //
        //   afterHideCallback:
        //     A callback that should be executed after the dialog box is closed by the user.
        //
        // Returns:
        //     A Task allowing this async method to be awaited.
        Task ShowError(string message, string title = null, string buttonText = null, Action afterHideCallback = null);
        //
        // Summary:
        //     Displays information about an error.
        //
        // Parameters:
        //   error:
        //     The exception of which the message must be shown to the user.
        //
        //   title:
        //     The title of the dialog box. This may be null.
        //
        //   buttonText:
        //     The text shown in the only button in the dialog box. If left null, the text "OK"
        //     will be used.
        //
        //   afterHideCallback:
        //     A callback that should be executed after the dialog box is closed by the user.
        //
        // Returns:
        //     A Task allowing this async method to be awaited.
        Task ShowError(Exception error, string title = null, string buttonText = null, Action afterHideCallback = null);
        //
        // Summary:
        //     Displays information to the user. The dialog box will have only one button with
        //     the text "OK".
        //
        // Parameters:
        //   message:
        //     The message to be shown to the user.
        //
        //   title:
        //     The title of the dialog box. This may be null.
        //
        // Returns:
        //     A Task allowing this async method to be awaited.
        Task ShowMessage(string message, string title);
        //
        // Summary:
        //     Displays information to the user. The dialog box will have only one button.
        //
        // Parameters:
        //   message:
        //     The message to be shown to the user.
        //
        //   title:
        //     The title of the dialog box. This may be null.
        //
        //   buttonText:
        //     The text shown in the only button in the dialog box. If left null, the text "OK"
        //     will be used.
        //
        //   afterHideCallback:
        //     A callback that should be executed after the dialog box is closed by the user.
        //
        // Returns:
        //     A Task allowing this async method to be awaited.
        Task ShowMessage(string message, string title, string buttonText, Action afterHideCallback);
        //
        // Summary:
        //     Displays information to the user. The dialog box will have only one button.
        //
        // Parameters:
        //   message:
        //     The message to be shown to the user.
        //
        //   title:
        //     The title of the dialog box. This may be null.
        //
        //   buttonConfirmText:
        //     The text shown in the "confirm" button in the dialog box. If left null, the text
        //     "OK" will be used.
        //
        //   buttonCancelText:
        //     The text shown in the "cancel" button in the dialog box. If left null, the text
        //     "Cancel" will be used.
        //
        //   afterHideCallback:
        //     A callback that should be executed after the dialog box is closed by the user.
        //     The callback method will get a boolean parameter indicating if the "confirm"
        //     button (true) or the "cancel" button (false) was pressed by the user.
        //
        // Returns:
        //     A Task allowing this async method to be awaited. The task will return true or
        //     false depending on the dialog result.
        Task<bool> ShowMessage(string message, string title, string buttonConfirmText, string buttonCancelText, Action<bool> afterHideCallback);
        //
        // Summary:
        //     Displays information to the user in a simple dialog box. The dialog box will
        //     have only one button with the text "OK". This method should be used for debugging
        //     purposes.
        //
        // Parameters:
        //   message:
        //     The message to be shown to the user.
        //
        //   title:
        //     The title of the dialog box. This may be null.
        //
        // Returns:
        //     A Task allowing this async method to be awaited.
        Task ShowMessageBox(string message, string title);
    }
}
