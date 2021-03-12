using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.Styling;
using System.Collections.Generic;

namespace tanners_shell
{
    public class MainWindow : Window
    {
        public static Settings settings;
        public static MainWindow instance { get; private set; }

        public MainWindow()
        {
            instance = this;
            settings = new Settings();
            InitializeComponent();
#if DEBUG
            this.AttachDevTools();
#endif
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
