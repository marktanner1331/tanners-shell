using Avalonia.Controls;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Avalonia.Styling;

namespace tanners_shell
{
    public class Settings
    {
        private int _fontSize = 16;
        public int fontSize
        {
            get => _fontSize;
            set
            {
                _fontSize = value;
                MainWindow.instance.Resources["fontSize"] = value;
            }
        }

        public Settings()
        {
            MainWindow.instance.Resources["fontSize"] = _fontSize;
        }
    }
}
