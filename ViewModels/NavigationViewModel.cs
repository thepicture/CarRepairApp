using CarRepairApp.Commands;
using System.Windows.Input;

namespace CarRepairApp.ViewModels
{
    [PropertyChanged.AddINotifyPropertyChangedInterface]
    public class NavigationViewModel : BaseViewModel
    {
        public bool IsCanClearIdentity =>
            Identity.WeakIdentity != null || Identity.StrongIdentity != null;

        private Command goBackCommand;

        public ICommand GoBackCommand
        {
            get
            {
                if (goBackCommand == null)
                {
                    goBackCommand = new Command(GoBack);
                }

                return goBackCommand;
            }
        }

        private void GoBack(object commandParameter)
        {
            NavigationService.NavigateBack();
        }

        private Command clearSessionCommand;

        public ICommand ClearSessionCommand
        {
            get
            {
                if (clearSessionCommand == null)
                {
                    clearSessionCommand = new Command(ClearSessionAsync);
                }

                return clearSessionCommand;
            }
        }

        private async void ClearSessionAsync()
        {
            if (await MessageService.AskAsync("Завершить сессию?"))
            {
                Identity.Logout();
                NavigationService.NavigateToRoot();
            }
        }
    }
}