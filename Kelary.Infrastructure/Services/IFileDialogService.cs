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

using System.Threading.Tasks;

namespace Kelary.Infrastructure.Services
{
	/// <summary>
	/// An interface defining how file dialogs would be displayed.
	/// </summary>
	public interface IFileDialogService
	{
		/// <summary>
		/// Displays open file dialog.
		/// </summary>
		/// <param name="InitialDirectory">The initial directory that is displayed by a file dialog.</param>
		/// <param name="Filter">The default extension string to use to filter the list of files that are displayed.</param> 
		/// <param name="MultiSelect">An option indicationg whether dialog allows users to select multiple files.</param>
		/// <returns>An array that contains one file name for each selected file.</returns>
		Task<string[]> OpenFileDialog(string InitialDirectory, string Filter, string Title = null, bool MultiSelect = false);

		/// <summary>
		/// Displays save file dialog.
		/// </summary>
		/// <param name="InitialDirectory">The initial directory that is displayed by a file dialog.</param>
		/// <param name="Filter">The default extension string to use to filter the list of files that are displayed.</param> 
		/// <param name="FileName">String containing full path of the file selected in a file dialog.</param>
		/// <returns>A string that contains file name for selected file.</returns>
		Task<string> SaveFileDialog(string InitialDirectory, string Filter, string Title = null, string FileName = null);
	}
}
