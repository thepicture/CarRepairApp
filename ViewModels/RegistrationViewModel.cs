using CarRepairApp.Commands;
using CarRepairApp.Models.Entities;
using Microsoft.Win32;
using System.Collections.ObjectModel;
using System.IO;
using System.Windows.Input;

namespace CarRepairApp.ViewModels
{
    [PropertyChanged.AddINotifyPropertyChangedInterface]
    public class RegistrationViewModel : BaseViewModel
    {
        public RegistrationViewModel()
        {
            Title = "Страница регистрации";
            LoadUsersRolesAsync();
        }

        private async void LoadUsersRolesAsync()
        {
            UserRoles = new ObservableCollection<UserRole>(
                await UserRoleDataStore.GetItemsAsync());
        }

        public RegistrationUser User { get; set; } = new RegistrationUser();

        private Command selectPhotoCommand;

        public ICommand SelectPhotoCommand
        {
            get
            {
                if (selectPhotoCommand == null)
                    selectPhotoCommand = new Command(SelectPhotoAsync);

                return selectPhotoCommand;
            }
        }

        private async void SelectPhotoAsync()
        {
            OpenFileDialog imageFileDialog = new OpenFileDialog
            {
                Title = "Выберите аватар"
            };
            bool? isSelectedImage = imageFileDialog.ShowDialog();
            if (isSelectedImage.HasValue && isSelectedImage.Value)
            {
                User.Photo = File.ReadAllBytes(imageFileDialog.FileName);
                await MessageService.InformAsync("Аватар прикреплён");
            }
        }

        private Command registerCommand;

        public ICommand RegisterCommand
        {
            get
            {
                if (registerCommand == null)
                    registerCommand = new Command(RegisterAsync);

                return registerCommand;
            }
        }

        private async void RegisterAsync()
        {
            if (await RegistrationDataStore.AddItemAsync(User))
            {
                NavigationService.NavigateToRoot();
            }
        }

        public ObservableCollection<UserRole> UserRoles { get; set; }
    }
}
