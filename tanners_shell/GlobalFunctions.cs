using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Text;
using tanners_shell.variables;
using tanners_shell.variables.String;

namespace tanners_shell
{
    public static class GlobalFunctions 
    {
        public static ExpandoObject vars = new ExpandoObject();

        public static void print(string message)
        {
            History.add(new StringView(message));
        }

        public static void create(string name, object value)
        {
            IVariable variable = IVariable.fromObject(name, value);
            //TODO check there isn't already a variable with that name etc
            Variables.addVariable(variable);
            History.add(new StringView($"Created variable: '{name}'"));
        }
    }
}
