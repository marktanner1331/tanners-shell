using System;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using tanners_shell.views;

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

        protected override void OnKeyDown(KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.Enter:
                    if ((e.KeyModifiers & KeyModifiers.Shift) == 0)
                    {
                        e.Handled = true;
                    }
                    break;
            }

            base.OnKeyDown(e);
        }

        protected override void OnKeyUp(KeyEventArgs e)
        {
            switch(e.Key)
            {
                case Key.Enter:
                    if((e.KeyModifiers & KeyModifiers.Shift) == 0)
                    {
                        onRunClick(this, null);
                        e.Handled = true;
                    }
                    else
                    {
                        value.Text +=  "\n";
                        value.CaretIndex = int.MaxValue;
                    }
                    break;
            }

            base.OnKeyUp(e);
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
