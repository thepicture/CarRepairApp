using System.Windows;
using System.Windows.Controls;

namespace CarRepairApp.Views.Views
{
    /// <summary>
    /// Interaction logic for RegistrationView.xaml
    /// </summary>
    public partial class RegistrationView : UserControl
    {
        public RegistrationView()
        {
            InitializeComponent();
        }

        private void OnPasswordChanged(object sender, RoutedEventArgs e)
        {
            ((dynamic)DataContext).User.Password = (sender as PasswordBox).Password;
        }
    }
}
