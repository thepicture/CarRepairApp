using CarRepairApp.Commands;
using CarRepairApp.Models.Entities;
using System.Windows.Input;

namespace CarRepairApp.ViewModels
{
    [PropertyChanged.AddINotifyPropertyChangedInterface]
    public class LoginViewModel : BaseViewModel
    {
        public LoginViewModel()
        {
            Title = "Страница авторизации";
        }

        public LoginUser User { get; set; } = new LoginUser();

        private Command loginCommand;

        public ICommand LoginCommand
        {
            get
            {
                if (loginCommand == null)
                {
                    loginCommand = new Command(LoginAsync);
                }

                return loginCommand;
            }
        }

        private async void LoginAsync()
        {
            IsBusy = true;
            if (await LoginDataStore.AddItemAsync(User))
            {
                NavigationService.Navigate<AccountViewModel>();
            }
            IsBusy = false;
        }

        private Command goToRegistrationViewModelCommand;

        public ICommand GoToRegistrationViewModelCommand
        {
            get
            {
                if (goToRegistrationViewModelCommand == null)
                    goToRegistrationViewModelCommand = new Command(GoToRegistrationViewModel);
                return goToRegistrationViewModelCommand;
            }
        }

        private void GoToRegistrationViewModel()
        {
            NavigationService.Navigate<RegistrationViewModel>();
        }
    }
}
