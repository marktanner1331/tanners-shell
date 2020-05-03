using System;
using System.Collections.Generic;
using System.Text;
using Avalonia.Controls;

namespace tanners_shell.variables.String
{
    public class StringModel : IVariable
    {
        private string value;

        public StringModel(string name, string value) : base(name)
        {
            this.value = value;
        }

        public override UserControl getView() => new StringView(value);
    }
}
