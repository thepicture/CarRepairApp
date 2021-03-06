using CarRepairApp.Models.Entities;
using CarRepairApp.Services;
using CarRepairApp.Utils;

namespace CarRepairApp.ViewModels
{
    [PropertyChanged.AddINotifyPropertyChangedInterface]
    public abstract class BaseViewModel : NotifyableObject
    {
        public INavigationService<BaseViewModel> NavigationService =>
            DependencyService.Get<INavigationService<BaseViewModel>>();
        public IIdentityService<User> Identity =>
            DependencyService.Get<IIdentityService<User>>();
        public IMessageService MessageService =>
            DependencyService.Get<IMessageService>();
        public IHashGenerator HashGenerator =>
            DependencyService.Get<IHashGenerator>();
        public IDataStore<UserRole> UserRoleDataStore =>
            DependencyService.Get<IDataStore<UserRole>>();
        public IDataStore<LoginUser> LoginDataStore =>
            DependencyService.Get<IDataStore<LoginUser>>();
        public IDataStore<RegistrationUser> RegistrationDataStore =>
            DependencyService.Get<IDataStore<RegistrationUser>>();
        public IDataStore<User> UserDataStore =>
            DependencyService.Get<IDataStore<User>>();
        public IDataStore<Feedback> FeedbackDataStore =>
            DependencyService.Get<IDataStore<Feedback>>();
        public IDataStore<Car> CarDataStore =>
            DependencyService.Get<IDataStore<Car>>();
        public IDataStore<WorkProcess> WorkProcessDataStore =>
            DependencyService.Get<IDataStore<WorkProcess>>();
        public IDataStore<Part> PartDataStore =>
            DependencyService.Get<IDataStore<Part>>();
        public IDataStore<Customer> CustomerDataStore =>
            DependencyService.Get<IDataStore<Customer>>();

        public bool IsBusy { get; set; }
        public bool IsNotBusy => !IsBusy;
        public bool IsRefreshing { get; set; }
        public string Title { get; set; }
    }
}
