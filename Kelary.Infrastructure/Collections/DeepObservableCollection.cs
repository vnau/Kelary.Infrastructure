#region Disclaimer / License
// Copyright (c) 2021 The Kelary Team
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
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;

namespace Kelary.Infrastructure.Collections
{
    /// <summary>
    /// DeepObservableCollection class
    /// </summary>
    /// <typeparam name="T">Type of the collection item.</typeparam>
    public class DeepObservableCollection<T> : ObservableCollection<T>
       where T : INotifyPropertyChanged
    {
        #region public
        /// <summary>
        /// Occurs when a property is changed within an item.
        /// </summary>
        public event EventHandler<ItemPropertyChangedEventArgs> ItemPropertyChanged;

        #endregion public

        #region constructors

        public DeepObservableCollection() : base()
        {
        }

        public DeepObservableCollection(List<T> list) : base(list)
        {
            ObserveAll();
        }

        public DeepObservableCollection(IEnumerable<T> enumerable) : base(enumerable)
        {
            ObserveAll();
        }

        #endregion constructors

        #region protected
        protected override void OnCollectionChanged(NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == NotifyCollectionChangedAction.Remove ||
                e.Action == NotifyCollectionChangedAction.Replace)
            {
                foreach (T item in e.OldItems)
                    item.PropertyChanged -= ChildPropertyChanged;
            }

            if (e.Action == NotifyCollectionChangedAction.Add ||
                e.Action == NotifyCollectionChangedAction.Replace)
            {
                foreach (T item in e.NewItems)
                    item.PropertyChanged += ChildPropertyChanged;
            }

            base.OnCollectionChanged(e);
        }

        protected void OnItemPropertyChanged(ItemPropertyChangedEventArgs e)
        {
            ItemPropertyChanged?.Invoke(this, e);
        }

        protected void OnItemPropertyChanged(int index, PropertyChangedEventArgs e)
        {
            OnItemPropertyChanged(new ItemPropertyChangedEventArgs(index, e));
        }

        protected override void ClearItems()
        {
            foreach (T item in Items)
                item.PropertyChanged -= ChildPropertyChanged;

            base.ClearItems();
        }

        #endregion protected

        #region private

        private void ObserveAll()
        {
            foreach (T item in Items)
                item.PropertyChanged += ChildPropertyChanged;
        }

        private void ChildPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            T typedSender = (T)sender;
            int i = Items.IndexOf(typedSender);

            if (i < 0)
                throw new ArgumentException("Received property notification from item not in collection");

            OnItemPropertyChanged(i, e);
        }

        #endregion private
    }
}
