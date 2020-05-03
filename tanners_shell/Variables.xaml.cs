using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Dynamic;
using System.Linq;
using tanners_shell.variables;

namespace tanners_shell
{
    public class Variables : UserControl
    {
        private static ObservableCollection<IVariable> variables;

        static Variables()
        {
            variables = new ObservableCollection<IVariable>();
        }

        internal static void addVariable(IVariable variable)
        {
            variables.Add(variable);
        }

        internal static bool hasVariableWithName(string name) => variables.Any(x => x.name == name);

        private ListBox variableListBox;

        public Variables()
        {
            this.InitializeComponent();
        }
        
        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);

            variableListBox = this.Find<ListBox>("variableListBox");
            variableListBox.Items = variables;
            variableListBox.ItemTemplate = new VariablesListBoxItem.ItemTemplate();
        }

        private void onNewClick(object sender, RoutedEventArgs args)
        {
            CreateVariableModal.show();
        }
    }
}
