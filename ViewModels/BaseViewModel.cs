using CarRepairApp.Models.Entities;
using CarRepairApp.Services;

namespace CarRepairApp.ViewModels
{
    [PropertyChanged.AddINotifyPropertyChangedInterface]
    public abstract class BaseViewModel
    {
        public INavigationService<BaseViewModel> NavigationService =>
            DependencyService.Get<INavigationService<BaseViewModel>>();
        public IIdentityService<User> Identity =>
           DependencyService.Get<IIdentityService<User>>();
        public IMessageService MessageService =>
           DependencyService.Get<IMessageService>();
        public IHashGenerator HashGenerator =>
           DependencyService.Get<IHashGenerator>();
        public IDataStore<LoginUser> LoginDataStore =>
           DependencyService.Get<IDataStore<LoginUser>>();

        public bool IsBusy { get; set; }
        public bool IsNotBusy => !IsBusy;
        public bool IsRefreshing { get; set; }
        public string Title { get; set; }
    }
}
