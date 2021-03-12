using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Templates;
using Avalonia.Markup.Xaml;
using System.Collections.Generic;
using tanners_shell.views;

namespace tanners_shell
{
    public class VariablesListBoxItem : UserControl
    {
        public class ItemTemplate : IDataTemplate
        {
            public bool SupportsRecycling => false;

            public IControl Build(object param)
            {
                return new VariablesListBoxItem((KeyValuePair<string, object>)param);
            }

            public bool Match(object data)
            {
                return data is KeyValuePair<string, object>;
            }
        }

        public VariablesListBoxItem()
        {
            this.InitializeComponent();
        }

        public VariablesListBoxItem(KeyValuePair<string, object> variable) : this()
        {
            this.Find<TextBlock>("nameLabel").Text = variable.Key;
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
