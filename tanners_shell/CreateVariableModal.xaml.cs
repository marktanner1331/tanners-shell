using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using System.Threading;
using tanners_shell.variables;

namespace tanners_shell
{
    public class CreateVariableModal : Window
    {
        public static void show()
        {
            Window mainWindow = (Application.Current.ApplicationLifetime as IClassicDesktopStyleApplicationLifetime).MainWindow;
            Window w = new CreateVariableModal();
            w.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            w.ShowDialog(mainWindow);
        }

        public static void show(string name, string value)
        {
            Window mainWindow = (Application.Current.ApplicationLifetime as IClassicDesktopStyleApplicationLifetime).MainWindow;
            Window w = new CreateVariableModal(name, value);
            w.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            w.ShowDialog(mainWindow);
        }

        public CreateVariableModal()
        {
            this.InitializeComponent();
#if DEBUG
            this.AttachDevTools();
#endif
        }

        public CreateVariableModal(string name, string value)
        {
            this.InitializeComponent();
#if DEBUG
            this.AttachDevTools();
#endif

            this.Find<TextBox>("name").Text = name;
            this.Find<TextBox>("value").Text = value;
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }

        private async void onSelectFolderClick(object sender, RoutedEventArgs args)
        {
            Window mainWindow = (Application.Current.ApplicationLifetime as IClassicDesktopStyleApplicationLifetime).MainWindow;
            OpenFolderDialog dialog = new OpenFolderDialog();

            string folder = await dialog.ShowAsync(mainWindow);
            this.Find<TextBox>("value").Text = folder;
        }

        private async void onSelectFileClick(object sender, RoutedEventArgs args)
        {
            Window mainWindow = (Application.Current.ApplicationLifetime as IClassicDesktopStyleApplicationLifetime).MainWindow;
            OpenFileDialog dialog = new OpenFileDialog();
            
            string[] files = await dialog.ShowAsync(mainWindow);
            if(files.Length == 0)
            {
                return;
            }
            else
            {
                this.Find<TextBox>("value").Text = files[0];
            }
        }

        private void onCancelClick(object sender, RoutedEventArgs args)
        {
            this.Close();
        }

        private void onSaveClick(object sender, RoutedEventArgs args)
        {
            string name = this.Find<TextBox>("name").Text;
            string value = this.Find<TextBox>("value").Text;
            TextBlock errorLabel = this.Find<TextBlock>("error");

            if (string.IsNullOrEmpty(name))
            {
                errorLabel.Text = "Name cannot be empty";
                errorLabel.IsVisible = true;
                return;
            }

            if(Variables.hasVariableWithName(name))
            {
                errorLabel.Text = "A variable with that name already exists";
                errorLabel.IsVisible = true;
                return;
            }

            if (string.IsNullOrEmpty(value))
            {
                errorLabel.Text = "Value cannot be empty";
                errorLabel.IsVisible = true;
                return;
            }

            IVariable variable = IVariable.fromString(name, value);
            Variables.addVariable(variable);

            this.Close();
        }
    }
}
