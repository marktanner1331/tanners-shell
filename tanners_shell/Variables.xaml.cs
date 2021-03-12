using Avalonia;
using Avalonia.Controls;
using Avalonia.Data;
using Avalonia.Data.Core;
using Avalonia.Data.Core.Plugins;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using Avalonia.Utilities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Dynamic;
using System.Linq;
using tanners_shell.views;

namespace tanners_shell
{
    public class Variables : UserControl
    {
        internal static ExpandoObject expando;
        private static ObservableExpandoObject obserableExpando;
        static Variables()
        {
            expando = new ExpandoObject();
            obserableExpando = new ObservableExpandoObject(expando);
            obserableExpando.CollectionChanged += onExpandoChanged;
        }

        private static void onExpandoChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Add:
                    if(e.NewItems.Count == 1)
                    {
                        History.add(new StringView("Created variable: " + (string)e.NewItems[0]));
                    }
                    break;
                case NotifyCollectionChangedAction.Remove:
                    break;
                case NotifyCollectionChangedAction.Replace:
                    break;
                case NotifyCollectionChangedAction.Move:
                    break;
                case NotifyCollectionChangedAction.Reset:
                    break;
            }
        }

        internal static void addVariable(string key, object value)
        {
            ((IDictionary<string, object>)expando).Add(key, value);
        }

        internal static bool hasVariableWithName(string name) => ((IDictionary<string, object>)expando).ContainsKey(name);

        private ListBox variableListBox;

        public Variables()
        {
            this.InitializeComponent();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
            variableListBox = this.Find<ListBox>("variableListBox");
            variableListBox.Items = new ObservableExpandoObject(expando);
            variableListBox.ItemTemplate = new VariablesListBoxItem.ItemTemplate();
        }
    }
}
