using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace tanners_shell.views
{
    public class StringView : UserControl
    {
        public StringView()
        {
            this.InitializeComponent();
        }

        public StringView(string s) : this()
        {
            this.Find<TextBox>("label").Text = s;
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
