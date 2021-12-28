using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace hex.mvvm
{
    /// <summary>
    /// abstract MVVM model
    /// </summary>
#pragma warning disable S3881 // "IDisposable" should be implemented correctly
    public abstract class ViewModelBase : INotifyPropertyChanged, IDisposable
#pragma warning restore S3881 // "IDisposable" should be implemented correctly
    {
        private Dictionary<string, IValueWrapper> propertys = new Dictionary<string, IValueWrapper>();

        /// <summary>
        /// Get value of property
        /// </summary>
        /// <typeparam name="T">type</typeparam>
        /// <param name="propertyName">property name</param>
        /// <returns></returns>
        public T Get<T>([CallerMemberName] string propertyName = null)
        {
            return propertys.TryGetValue(propertyName, out IValueWrapper _value) ? ((ValueWrapper<T>)_value).Value : default(T);
        }

        /// <summary>
        /// Set value to property
        /// </summary>
        /// <typeparam name="T">type</typeparam>
        /// <param name="value">value</param>
        /// <param name="propertyName">property name</param>
        protected void Set<T>(T value, [CallerMemberName] string propertyName = null)
        {
            Set(value, false, propertyName);
        }

        /// <summary>
        /// Set value to property
        /// </summary>
        /// <typeparam name="T">type</typeparam>
        /// <param name="value">value</param>
        /// <param name="hiden">not invoke OnPropertyChanged</param>
        /// <param name="propertyName">property name</param>
        protected void Set<T>(T value, bool hiden, [CallerMemberName] string propertyName = null)
        {
            if (propertys.TryGetValue(propertyName, out IValueWrapper _value))
            {
                var wrapper = (ValueWrapper<T>)_value;
                if (EqualityComparer<T>.Default.Equals(wrapper.Value, value)) return;
                wrapper.Value = value;
            }
            else
            {
                propertys[propertyName] = new ValueWrapper<T>(value);
            }

            if (hiden) return;
            OnPropertyChanged(propertyName);
        }

        /// <summary>
        /// Property change event
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Initializes a property change event
        /// </summary>
        /// <param name="propertyName"></param>
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        /// <summary>
        /// Releases unmanaged resources and calls GC
        /// </summary>
        public void Dispose()
        {
            var delegates = PropertyChanged?.GetInvocationList();
            if (delegates != null)
            {
                foreach (var item in delegates)
                {
                    PropertyChanged -= (PropertyChangedEventHandler)item;
                }
            }
            GC.SuppressFinalize(this);
        }
    }
}
