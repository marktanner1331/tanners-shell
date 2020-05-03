using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Templates;
using Avalonia.Markup.Xaml;
using tanners_shell.variables;

namespace tanners_shell
{
    public class VariablesListBoxItem : UserControl
    {
        public class ItemTemplate : IDataTemplate
        {
            public bool SupportsRecycling => false;

            public IControl Build(object param)
            {
                return new VariablesListBoxItem(param as IVariable);
            }

            public bool Match(object data)
            {
                return data is IVariable;
            }
        }

        public VariablesListBoxItem()
        {
            this.InitializeComponent();
        }

        public VariablesListBoxItem(IVariable variable) : this()
        {
            this.Find<TextBlock>("nameLabel").Text = variable.name;
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
