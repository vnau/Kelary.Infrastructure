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

using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using Kelary.Infrastructure.Services;
using Microsoft.Practices.ServiceLocation;
using System.Windows.Input;

namespace Kelary.Infrastructure.Showcase.ViewModel
{
    class FirstViewModel : ViewModelBase
    {
        public ICommand CommandForward
        {
            get => new RelayCommand(OnForward);
        }

        public FirstViewModel()
        {
        }

        private void OnForward()
        {
            var Navigation = ServiceLocator.Current.GetInstance<INavigationService>();
            Navigation?.NavigateTo("second", new SecondViewModel());
        }

        public ICommand CommandStart
        {
            get => new RelayCommand(OnStart);
        }


        private async void OnStart()
        {
            var FileDialog = ServiceLocator.Current.GetInstance<IFileDialogService>();
            string FileName = await FileDialog.SaveFileDialog(null, "CSV Files (*.csv)|*.csv|RTF Files (*.rtf)|*.rtf", "File dialog service", "test.rtf");

            if (!string.IsNullOrEmpty(FileName))
            {
                var dialog = ServiceLocator.Current.GetInstance<IDialogService>();
                await dialog?.ShowMessage(string.Format("Save file \"{0}\"", FileName), "Dialog service");
            }
        }

    }
}
