using CarRepairApp.Models.Entities;
using CarRepairApp.Properties;
using CarRepairApp.Services;
using CarRepairApp.ViewModels;
using System;
using System.Windows;

namespace CarRepairApp
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            DependencyService.Register<MessageService>();

            try
            {
                new BaseModel().Database.Connection
                    .Open();
            }
            catch (Exception ex)
            {
                DependencyService
                    .Get<IMessageService>()
                    .InformErrorAsync(ex)
                    .Wait();
                return;
            }

            DependencyService.Register<ViewModelNavigationService>();
            DependencyService.Register<HashGenerator>();
            DependencyService.Register<UserRoleDataStore>();
            DependencyService.Register<LoginUserDataStore>();
            DependencyService.Register<RegistrationUserDataStore>();
            DependencyService.Register<FeedbackDataStore>();
            DependencyService.Register<RegistrationViewModel>();
            DependencyService.Register<UserIdentityService>();
            DependencyService.Register<UserDataStore>();
            DependencyService.Register<CarDataStore>();
            DependencyService.Register<WorkProcessDataStore>();
            DependencyService.Register<PartDataStore>();
            DependencyService.Register<CustomerDataStore>();

            if (!string.IsNullOrWhiteSpace(
                Settings.Default.SerializedIdentity))
            {
                DependencyService
                .Get<INavigationService<BaseViewModel>>()
                .Navigate<AccountViewModel>();
            }
            else
            {
                DependencyService
               .Get<INavigationService<BaseViewModel>>()
               .Navigate<LoginViewModel>();
            }
        }
    }
}
