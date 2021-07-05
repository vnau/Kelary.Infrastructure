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

using System.ComponentModel;

namespace Kelary.Infrastructure.Collections
{
    /// <summary>
    /// Provides data for the <see cref="DeepObservableCollection{T}.ItemPropertyChanged"/> event.
    /// </summary>
    public class ItemPropertyChangedEventArgs : PropertyChangedEventArgs
    {
        /// <summary>
        /// Gets the index in the collection for which the property change has occurred.
        /// </summary>
        /// <value>
        /// Index in parent collection.
        /// </value>
        public int CollectionIndex { get; }

        #region constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="ItemPropertyChangedEventArgs"/> class.
        /// </summary>
        /// <param name="index">The index in the collection of changed item.</param>
        /// <param name="name">The name of the property that changed.</param>
        public ItemPropertyChangedEventArgs(int index, string name) : base(name)
        {
            CollectionIndex = index;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ItemPropertyChangedEventArgs"/> class.
        /// </summary>
        /// <param name="index">The index.</param>
        /// <param name="args">The <see cref="PropertyChangedEventArgs"/> instance containing the event data.</param>
        public ItemPropertyChangedEventArgs(int index, PropertyChangedEventArgs args) : this(index, args.PropertyName)
        { }

        #endregion constructors
    }
}
