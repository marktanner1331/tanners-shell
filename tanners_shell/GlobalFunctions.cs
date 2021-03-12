using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Text;
using tanners_shell.views;

namespace tanners_shell
{
    public class GlobalFunctions
    {
        public static dynamic vars => Variables.expando;
        public static Settings settings => MainWindow.settings;

        public static void print(string message)
        {
            History.add(new StringView(message));
        }
    }
}
