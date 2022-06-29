using System.Security;
using System.Windows;
using System.Windows.Controls;

namespace CarRepairApp.Views.Views
{
    /// <summary>
    /// Interaction logic for AccountView.xaml
    /// </summary>
    public partial class AccountView : UserControl
    {
        public AccountView()
        {
            InitializeComponent();
        }

        [SecurityCritical]
        private void OnPasswordChanged(object sender, RoutedEventArgs e)
        {
            ((dynamic)DataContext).User.Password = (sender as PasswordBox).Password;
        }

        [SecurityCritical]
        private void OnIsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (IsVisible)
            {
                ((dynamic)DataContext).OnAppearing();
                PBoxPassword.Password = ((dynamic)DataContext).User.Password;
            }
        }
    }
}
