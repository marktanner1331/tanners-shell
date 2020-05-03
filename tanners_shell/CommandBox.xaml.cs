using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using tanners_shell.variables.String;

namespace tanners_shell
{
    public class CommandBox : UserControl
    {
        private TextBox value;
        public CommandBox()
        {
            this.InitializeComponent();
            value = this.Find<TextBox>("value");
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }

        private void onRunClick(object sender, RoutedEventArgs args)
        {
            if(string.IsNullOrEmpty(value.Text))
            {
                return;
            }

            ScriptRunner.runCommand(value.Text);
            value.Text = "";
        }
    }
}
