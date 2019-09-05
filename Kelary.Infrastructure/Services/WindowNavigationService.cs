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

using Kelary.Infrastructure.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace Kelary.Infrastructure.Services
{
    /// <summary>
    /// Window navigation service.
    /// </summary>
	public class WindowNavigationService : IWindowNavigationService
    {
        #region Fields
        private Window _MainFrame;
        private readonly Dictionary<string, Uri> _pagesByKey;
        private readonly List<string> _historic;
        private string _currentPageKey;
        #endregion

        public WindowNavigationService()
        {
            _pagesByKey = new Dictionary<string, Uri>();
            _historic = new List<string>();
        }

        public void Configure(string key, Uri pageType)
        {
            lock (_pagesByKey)
            {
                if (_pagesByKey.ContainsKey(key))
                {
                    _pagesByKey[key] = pageType;
                }
                else
                {
                    _pagesByKey.Add(key, pageType);
                }
            }
        }

        public string CurrentPageKey
        {
            get
            {
                return _currentPageKey;
            }
            /*
            private set
            {
                if (_currentPageKey == value)
                {
                    return;
                }

                _currentPageKey = value;
                OnPropertyChanged("CurrentPageKey");
            }
            */
        }

        public void GoBack()
        {
            if (WindowStack.Any())
            {
                var window = WindowStack.Pop();
                window.Close();
            }
            /*
            if (_historic.Count > 1)
            {
                _historic.RemoveAt(_historic.Count - 1);
                NavigateTo(_historic.Last(), null);
            }
            */
        }

        Stack<Window> WindowStack = new Stack<Window>();
        //List<Window> WindowStack = new List<Window>();

        public void NavigateTo(string pageKey)
        {
            NavigateTo(pageKey, null);
        }

        public void NavigateTo(string pageKey, object parameter)
        {
            //ViewModelBase viewModel = parameter as ViewModelBase;
            //if (viewModel == null)
            //	throw new ArgumentException("Parameter should be derived from ViewModelBase");

            if (EnsureMainFrame())
            {
                Window window = new DialogWindowView();
                window.Owner = _MainFrame;

                if (_pagesByKey.ContainsKey(pageKey))
                {
                    var uri = _pagesByKey[pageKey];
                    var control = Application.LoadComponent(uri) as UserControl;
                    window.Content = control;
                    window.SourceInitialized += (s, e) =>
                    {
                        window.MinWidth = window.ActualWidth;
                        window.MinHeight = window.ActualHeight;
                    };
                    //if (viewModel != null)
                    //	window.DataContext = viewModel;
                    if (parameter != null)
                        window.DataContext = parameter;

                    window.Closed += Window_Closed;
                }
                WindowStack.Push(window);
                _MainFrame.Dispatcher.BeginInvoke((Action)(() => window.ShowDialog()));
            }
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            var window = sender as Window;
            // Remove window on closing only that it is on the top of the stack.
            if (WindowStack.Any() && WindowStack.Peek() == window)
            {
                GoBack();
            }
        }

        private bool EnsureMainFrame()
        {
            if (_MainFrame == null)
                _MainFrame = System.Windows.Application.Current.MainWindow;
            return _MainFrame != null;
        }
    }
}
