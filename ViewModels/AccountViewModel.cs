using CarRepairApp.Commands;
using CarRepairApp.Models.Entities;
using Microsoft.Win32;
using System.IO;
using System.Windows.Input;

namespace CarRepairApp.ViewModels
{
    [PropertyChanged.AddINotifyPropertyChangedInterface]
    public class AccountViewModel : BaseViewModel
    {
        public AccountViewModel()
        {
            Title = "Личный кабинет";
            User = Identity.WeakIdentity;
        }

        public User User { get; set; }

        private Command selectImageCommand;

        public ICommand SelectImageCommand
        {
            get
            {
                if (selectImageCommand == null)
                    selectImageCommand = new Command(SelectImageAsync);

                return selectImageCommand;
            }
        }

        private async void SelectImageAsync()
        {
            OpenFileDialog imageFileDialog = new OpenFileDialog
            {
                Title = "Выберите новый аватар"
            };
            bool? isSelectedImage = imageFileDialog.ShowDialog();
            if (isSelectedImage.HasValue && isSelectedImage.Value)
            {
                User.Photo = File.ReadAllBytes(imageFileDialog.FileName);
                await MessageService.InformAsync("Фото изменено");
            }
        }

        private Command saveChangesCommand;
        public ICommand SaveChangesCommand
        {
            get
            {
                if (saveChangesCommand == null)
                    saveChangesCommand = new Command(SaveChangesAsync);

                return saveChangesCommand;
            }
        }

        private async void SaveChangesAsync()
        {
            if (await UserDataStore.UpdateItemAsync(User))
            {
                Identity.WeakIdentity = User;
            }
        }

        private Command goToFeedbackViewModel;

        public ICommand GoToFeedbackViewModel
        {
            get
            {
                if (goToFeedbackViewModel == null)
                    goToFeedbackViewModel = new Command(PerformGoToFeedbackViewModel);

                return goToFeedbackViewModel;
            }
        }

        private void PerformGoToFeedbackViewModel()
        {
            NavigationService.Navigate<FeedbackViewModel>();
        }

        private Command goToWorkProcessViewModel;

        public ICommand GoToWorkProcessViewModel
        {
            get
            {
                if (goToWorkProcessViewModel == null)
                    goToWorkProcessViewModel = new Command(PerformGoToWorkProcessViewModel);

                return goToWorkProcessViewModel;
            }
        }

        private void PerformGoToWorkProcessViewModel()
        {
            NavigationService.Navigate<WorkProcessViewModel>();
        }

        private Command goToPartViewModel;

        public ICommand GoToPartViewModel
        {
            get
            {
                if (goToPartViewModel == null)
                    goToPartViewModel = new Command(PerformGoToPartViewModel);

                return goToPartViewModel;
            }
        }

        private void PerformGoToPartViewModel()
        {
            NavigationService.Navigate<PartViewModel>();
        }
    }
}