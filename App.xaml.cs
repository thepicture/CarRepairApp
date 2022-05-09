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
                new BaseModel().Database.Connection.Open();
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
            DependencyService.Register<LoginUserDataStore>();
            DependencyService.Register<RegistrationUserDataStore>();
            DependencyService.Register<UserIdentityService>();
            DependencyService.Register<HashGenerator>();

            DependencyService.Get<INavigationService<BaseViewModel>>()
                   .Navigate<LoginViewModel>();
            if (!string.IsNullOrWhiteSpace(
                Settings.Default.SerializedIdentity))
            {
                // TODO: Navigate to AccountViewModel
            }
        }
    }
}
