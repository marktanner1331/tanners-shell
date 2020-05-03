using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Templates;
using Avalonia.Markup.Xaml;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;

namespace tanners_shell
{
    public class History : UserControl
    {
        private static ObservableCollection<UserControl> messages;
        private static event Action historyChanged;

        static History()
        {
            messages = new ObservableCollection<UserControl>();
        }

        public static void add(UserControl message)
        {
            messages.Add(message);
            historyChanged?.Invoke();
        }

        private ItemsControl output;
        private ScrollViewer scrollViewer;

        public History()
        {
            this.InitializeComponent();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
            output = this.Find<ItemsControl>("output");
            output.Items = messages;
            output.ItemTemplate = new ItemTemplate();

            scrollViewer = this.Find<ScrollViewer>("scrollViewer");
            historyChanged += onHistoryChanged;
        }

        private void onHistoryChanged()
        {
            scrollViewer.Offset = new Vector(scrollViewer.Offset.X, scrollViewer.Extent.Height - scrollViewer.Viewport.Height);
        }

        private class ItemTemplate : IDataTemplate
        {
            public bool SupportsRecycling => false;

            public IControl Build(object param)
            {
                return param as IControl;
            }

            public bool Match(object data)
            {
                return data is IControl;
            }
        }
    }
}
