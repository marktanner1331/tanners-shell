using Avalonia.Controls;
using System;
using System.Collections.Generic;
using System.Text;
using tanners_shell.variables.String;

namespace tanners_shell.variables
{
    public abstract class IVariable
    {
        public string name;

        public IVariable(string name)
        {
            this.name = name;
        }

        public abstract UserControl getView();

        public static IVariable fromObject(string name, object value)
        {
            if(value is string)
            {
                return new StringModel(name, value as string);
            }
            else
            {
                throw new Exception("Unknown value type: " + value.GetType());
            }
        }

        public static IVariable fromString(string name, string value)
        {
            return new StringModel(name, value);
        }
    }
}
