using CarRepairApp.ViewModels;
using System.Windows;

namespace CarRepairApp
{
    /// <summary>
    /// Interaction logic for NavigationWindow.xaml
    /// </summary>
    public partial class NavigationWindow : Window
    {
        public NavigationWindow()
        {
            InitializeComponent();
            DataContext = new NavigationViewModel();
        }
    }
}
