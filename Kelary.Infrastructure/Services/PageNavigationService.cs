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
using PageTuple = System.Tuple<string, System.Windows.FrameworkElement>;

namespace Kelary.Infrastructure.Services
{
    /// <summary>
    /// Page navigation service.
    /// </summary>
    public class PageNavigationService : IPageNavigationService, IObservable<object>
    {
        #region Fields
        private readonly Dictionary<string, Uri> pagesByKey;
        private Stack<PageTuple> pageStack;
        private readonly List<IObserver<object>> observers = new List<IObserver<object>>();
        #endregion

        public PageNavigationService()
        {
            pagesByKey = new Dictionary<string, Uri>();
            pageStack = new Stack<PageTuple>();
        }

        public void Configure(string key, Uri pageType)
        {
            lock (pagesByKey)
            {
                if (pagesByKey.ContainsKey(key))
                {
                    pagesByKey[key] = pageType;
                }
                else
                {
                    pagesByKey.Add(key, pageType);
                }
            }
        }

        /// <summary>
        /// Returns a key of the current page
        /// </summary>
        public string CurrentPageKey
        {
            get
            {
                return pageStack.FirstOrDefault().Item1;
            }
        }

        public void GoBack()
        {
            if (pageStack.Any())
            {
                var control = pageStack.Pop();
                //control.Close();
                RaiseTop();
            }
        }

        public void NavigateTo(string pageKey)
        {
            NavigateTo(pageKey, null);
        }

        public void NavigateTo(string pageKey, object parameter)
        {
            if (pagesByKey.ContainsKey(pageKey))
            {
                var uri = pagesByKey[pageKey];
                var control = Application.LoadComponent(uri) as FrameworkElement;
                if (parameter != null)
                    control.DataContext = parameter;
                pageStack.Push(new PageTuple(pageKey, control));
                RaiseTop();
            }
        }

        /// <summary>
        /// Notify all observers.
        /// </summary>
        private void RaiseTop()
        {
            var top = pageStack.FirstOrDefault().Item2;
            observers.ForEach(o => o.OnNext(top));
        }

        private void Completion()
        {
            observers.ForEach(o => o.OnCompleted());
            observers.Clear();
        }

        public IDisposable Subscribe(IObserver<object> observer)
        {
            var subscription = new Subscription(() => observers.Remove(observer));
            observers.Add(observer);
            // Notify the subscriber 
            if (pageStack.Any())
                observer.OnNext(pageStack.Last().Item2);
            return subscription;
        }


        class Subscription : IDisposable
        {
            private readonly Action onDispose;
            public Subscription(Action onDispose)
            {
                this.onDispose = onDispose;
            }

            public void Dispose()
            {
                onDispose();
            }
        }
    }
}
