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
using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace Kelary.Infrastructure.Services
{
    /// <summary>
    /// Page navigation service.
    /// </summary>
    public class PageNavigationService : IPageNavigationService, IObservable<object>
    {
        #region Fields
        private readonly Dictionary<string, Uri> _pagesByKey;
        private readonly List<string> _historic;
        private string _currentPageKey;
        #endregion

        public PageNavigationService()
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
        }

        public void GoBack()
        {
            if (PageStack.Any())
            {
                var control = PageStack.Pop();
                //control.Close();
                RaiseTop();
            }
        }

        Stack<FrameworkElement> PageStack = new Stack<FrameworkElement>();

        public void NavigateTo(string pageKey)
        {
            NavigateTo(pageKey, null);
        }

        public void NavigateTo(string pageKey, object parameter)
        {
            if (_pagesByKey.ContainsKey(pageKey))
            {
                var uri = _pagesByKey[pageKey];
                var control = Application.LoadComponent(uri) as FrameworkElement;
                if (parameter != null)
                    control.DataContext = parameter;
                PageStack.Push(control);
                _currentPageKey = pageKey;
                RaiseTop();
            }
        }

        private readonly List<IObserver<object>> _observers = new List<IObserver<object>>();


        /// <summary>
        /// Notify all observers.
        /// </summary>
        private void RaiseTop()
        {
            var top = PageStack.FirstOrDefault();
            _observers.ForEach(o => o.OnNext(top));
        }

        private void Completion()
        {
            _observers.ForEach(o => o.OnCompleted());
            _observers.Clear();
        }
        public IDisposable Subscribe(IObserver<object> observer)
        {
            var subscription = new Subscription(() => _observers.Remove(observer));
            _observers.Add(observer);
            // Notify the subscriber 
            if (PageStack.Any())
                observer.OnNext(PageStack.Last());
            return subscription;
        }


        class Subscription : IDisposable
        {
            private readonly Action _onDispose;
            public Subscription(Action onDispose)
            {
                _onDispose = onDispose;
            }

            public void Dispose()
            {
                _onDispose();
            }
        }
    }
}
