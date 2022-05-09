using System.Windows;
using System.Windows.Controls;

namespace CarRepairApp.Views.Views
{
    /// <summary>
    /// Interaction logic for LoginView.xaml
    /// </summary>
    public partial class LoginView : UserControl
    {
        public LoginView()
        {
            InitializeComponent();
        }

        private void OnPasswordChanged(object sender, RoutedEventArgs e)
        {
            ((dynamic)DataContext).User.Password = (sender as PasswordBox).Password;
        }
    }
}
