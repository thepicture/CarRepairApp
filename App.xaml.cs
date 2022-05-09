using CarRepairApp.Models.Entities;
using CarRepairApp.Properties;
using CarRepairApp.Services;
using CarRepairApp.ViewModels;
using System;
using System.Data.Entity;
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

            Database.SetInitializer(new DatabaseInitializer());
            try
            {
                new BaseModel().Database.Connection
                    .Open();
            }
            catch (Exception)
            {
                DependencyService
                    .Get<IMessageService>()
                    .InformErrorAsync("Проверьте подключение к базе данных")
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
