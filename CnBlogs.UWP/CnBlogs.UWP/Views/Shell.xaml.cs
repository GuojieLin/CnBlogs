using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using CnBlogs.UWP.Commond;

namespace CnBlogs.UWP.Views
{
    public sealed partial class Shell : Page
    {
        private readonly Frame _contentFrame;

        public Shell(Frame frame)
        {
            this._contentFrame = frame;
            this.InitializeComponent();
            this.ShellSplitView.Content = frame;
            var update = new Action(() =>
            {
                // update radiobuttons after frame navigates
                var type = frame.CurrentSourcePageType;
                foreach (var radioButton in AllRadioButtons(this))
                {
                    var target = radioButton.CommandParameter as NavType;
                    if (target == null)
                        continue;
                    radioButton.IsChecked = target.Type == type;
                }
                this.ShellSplitView.IsPaneOpen = false;
                this.BackCommand.RaiseCanExecuteChanged();
            });
            frame.Navigated += (s, e) => update();
            this.Loaded += (s, e) => update();
            this.DataContext = this;
        }

        // back
        Command _backCommand;
        public Command BackCommand => _backCommand ?? (_backCommand = new Command(ExecuteBack, CanBack));

        private bool CanBack()
        {
            return this._contentFrame.CanGoBack;
        }
        private void ExecuteBack()
        {
            this._contentFrame.GoBack();
        }

        // menu
        Command _menuCommand;
        public Command MenuCommand => _menuCommand ?? (_menuCommand = new Command(ExecuteMenu));

        private void ExecuteMenu()
        {
            this.ShellSplitView.IsPaneOpen = !this.ShellSplitView.IsPaneOpen;
        }

        // nav
        Command<NavType> _navCommand;

        public Command<NavType> NavCommand => _navCommand ?? (_navCommand = new Command<NavType>(ExecuteNav));

        private void ExecuteNav(NavType navType)
        {
            var type = navType.Type;

            this._contentFrame.Navigate(navType.Type);
            // when we nav home, clear history
            if (type == typeof(Shell))
            {
                this._contentFrame.BackStack.Clear();
                this.BackCommand?.RaiseCanExecuteChanged();
            }
        }

        // utility
        public List<RadioButton> AllRadioButtons(DependencyObject parent)
        {
            var list = new List<RadioButton>();
            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(parent); i++)
            {
                var child = VisualTreeHelper.GetChild(parent, i);
                if (child is RadioButton)
                {
                    list.Add(child as RadioButton);
                    continue;
                }
                list.AddRange(AllRadioButtons(child));
            }
            return list;
        }

        // prevent check
        private void DontCheck(object s, RoutedEventArgs e)
        {
            // don't let the radiobutton check
            var radioButton = s as RadioButton;
            if (radioButton != null) radioButton.IsChecked = false;
        }
    }

    public class NavType
    {
        public Type Type { get; set; }
        public string Parameter { get; set; }
    }
}
