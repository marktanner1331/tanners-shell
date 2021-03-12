using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Dynamic;
using System.Text;

namespace tanners_shell
{
    class ObservableExpandoObject : ICollection<KeyValuePair<string, object>>, IEnumerable<KeyValuePair<string, object>>, IEnumerable, IDictionary<string, object>, IReadOnlyCollection<KeyValuePair<string, object>>, IReadOnlyDictionary<string, object>, INotifyCollectionChanged, INotifyPropertyChanged
    {
        private ExpandoObject expando;
        private HashSet<string> propertyNames;

        public ObservableExpandoObject(ExpandoObject expando)
        {
            this.expando = expando;
            propertyNames = new HashSet<string>();
            ((INotifyPropertyChanged)expando).PropertyChanged += onPropertyChanged;
        }

        private void onPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            PropertyChanged?.Invoke(sender, e);
            if(propertyNames.Contains(e.PropertyName))
            {
                if(((IDictionary<string, object>)expando).ContainsKey(e.PropertyName))
                {
                    //property was overwritten
                    CollectionChanged?.Invoke(sender, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Replace, e.PropertyName));
                }
                else
                {
                    //property was removed
                    propertyNames.Remove(e.PropertyName);
                    CollectionChanged?.Invoke(sender, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Remove, e.PropertyName));
                }
            }
            else
            {
                propertyNames.Add(e.PropertyName);
                CollectionChanged?.Invoke(sender, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Add, e.PropertyName));
            }
        }

        public object this[string key] { get => ((IDictionary<string, object>)expando)[key]; set => ((IDictionary<string, object>)expando)[key] = value; }

        public int Count => ((IDictionary<string, object>)expando).Count;

        public bool IsReadOnly => ((IDictionary<string, object>)expando).IsReadOnly;

        public ICollection<string> Keys => ((IDictionary<string, object>)expando).Keys;

        public ICollection<object> Values => ((IDictionary<string, object>)expando).Values;

        IEnumerable<string> IReadOnlyDictionary<string, object>.Keys => ((IDictionary<string, object>)expando).Keys;

        IEnumerable<object> IReadOnlyDictionary<string, object>.Values => ((IDictionary<string, object>)expando).Values;

        public event NotifyCollectionChangedEventHandler CollectionChanged;
        public event PropertyChangedEventHandler PropertyChanged;

        public void Add(KeyValuePair<string, object> item) => ((IDictionary<string, object>)expando).Add(item);

        public void Add(string key, object value) => ((IDictionary<string, object>)expando).Add(key, value);

        public void Clear() => ((IDictionary<string, object>)expando).Clear();

        public bool Contains(KeyValuePair<string, object> item) => ((IDictionary<string, object>)expando).Contains(item);

        public bool ContainsKey(string key) => ((IDictionary<string, object>)expando).ContainsKey(key);

        public void CopyTo(KeyValuePair<string, object>[] array, int arrayIndex) => ((IDictionary<string, object>)expando).CopyTo(array, arrayIndex);

        public IEnumerator<KeyValuePair<string, object>> GetEnumerator() => ((IDictionary<string, object>)expando).GetEnumerator();

        public bool Remove(KeyValuePair<string, object> item) => ((IDictionary<string, object>)expando).Remove(item);

        public bool Remove(string key) => ((IDictionary<string, object>)expando).Remove(key);

        public bool TryGetValue(string key, [MaybeNullWhen(false)] out object value) => ((IDictionary<string, object>)expando).TryGetValue(key, out value);

        IEnumerator IEnumerable.GetEnumerator() => ((IDictionary<string, object>)expando).GetEnumerator();
    }
}
