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
            if (await LoginDataStore.AddItemAsync(User))
            {

            }
        }

        private Command goToRegisterViewModelCommand;

        public ICommand GoToRegisterViewModelCommand
        {
            get
            {
                if (goToRegisterViewModelCommand == null)
                    goToRegisterViewModelCommand = new Command(GoToRegisterViewModel);
                return goToRegisterViewModelCommand;
            }
        }

        private void GoToRegisterViewModel()
        {
        }
    }
}
