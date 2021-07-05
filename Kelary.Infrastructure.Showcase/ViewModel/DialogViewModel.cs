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

using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using Kelary.Infrastructure.Services;
using Microsoft.Practices.ServiceLocation;
using System.Windows.Input;

namespace Kelary.Infrastructure.Showcase.ViewModel
{
    class DialogViewModel : ViewModelBase
    {
        public ICommand CommandBack
        {
            get => new RelayCommand(OnBack);
        }

        public DialogViewModel()
        {
        }

        public string Title => "Dialog Title";

        private void OnBack()
        {
            var Navigation = ServiceLocator.Current.GetInstance<IWindowNavigationService>();
            Navigation?.GoBack();
        }

        public ICommand CommandShowMessage
        {
            get => new RelayCommand(OnMessage);
        }

        private async void OnMessage()
        {
            var dialog = ServiceLocator.Current.GetInstance<IDialogService>();
            await dialog?.ShowMessage("Hey there!", "Message");
        }
    }
}
