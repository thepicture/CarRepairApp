using CarRepairApp.Models.Entities;
using CarRepairApp.Properties;
using CarRepairApp.Services;
using CarRepairApp.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;

namespace CarRepairApp
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private const string ConnectionStringsPath = "./../../ConnectionStrings.txt";
        private static string currentConnectionString;

        public static string CurrentConnectionString
        {
            get
            {
                if (!string.IsNullOrWhiteSpace(PermamentConnectionString))
                {
                    return PermamentConnectionString;
                }
                return currentConnectionString;
            }

            set => currentConnectionString = value;
        }
        public static string PermamentConnectionString
        {
            get => Settings.Default.WorkingConnectionString;
            set
            {
                Settings.Default.WorkingConnectionString = value;
                Settings.Default.Save();
            }
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            DependencyService.Register<MessageService>();

            if (!File.Exists(ConnectionStringsPath))
            {
                DependencyService
                           .Get<IMessageService>()
                           .InformErrorAsync("Не найден файл со строками подключения ConnectionStrings.txt")
                           .Wait();
                return;
            }

            Stack<string> dataSources;
            dataSources = new Stack<string>(
                File.ReadAllLines(ConnectionStringsPath));

            while (true)
            {
                if (!string.IsNullOrWhiteSpace(PermamentConnectionString))
                {
                    break;
                }
                CurrentConnectionString = $"data source={dataSources.Pop()};"
                                          + $"initial catalog=master;"
                                          + $"integrated security=True;"
                                          + $"MultipleActiveResultSets=True;"
                                          + $"App=EntityFramework;";
                try
                {
                    if (dataSources.Count == 0)
                    {
                        throw new Exception("Все строки подключения недоступны");
                    }
                    using (BaseModel masterDatabase = new BaseModel()) { }
                    using (BaseModel db = new BaseModel())
                    {
                        PermamentConnectionString = CurrentConnectionString.Replace("master",
                                                                                    "CarRepairBase");
                        break;
                    }
                }
                catch (Exception ex)
                {
                    if (dataSources.Count == 0)
                    {
                        DependencyService
                            .Get<IMessageService>()
                            .InformErrorAsync(ex)
                            .Wait();
                        return;
                    }
                }
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
