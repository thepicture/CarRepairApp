using CarRepairApp.Utils;
using CarRepairApp.ViewModels;
using System;
using System.Linq;

namespace CarRepairApp.Services
{
    [PropertyChanged.AddINotifyPropertyChangedInterface]
    public class ViewModelNavigationService : INavigationService<BaseViewModel>
    {
        public ViewModelNavigationService()
        {
            Journal.CollectionChanged += (_, __) =>
            {
                CurrentTarget = Journal.Peek();
                if (Journal.Count < 2)
                {
                    IsCanGoBack = false;
                }
                else
                {
                    BaseViewModel viewModel = Journal
                        .ElementAt(1);
                    IsCanGoBack = !(viewModel is LoginViewModel) || Journal.Peek() is RegistrationViewModel;
                }
            };
        }

        public ObservableStack<BaseViewModel> Journal { get; set; }
            = new ObservableStack<BaseViewModel>();
        public BaseViewModel CurrentTarget { get; set; }

        public void Navigate<TWhere>() where TWhere : BaseViewModel
        {
            Journal.Push(
                Activator.CreateInstance<TWhere>());
        }

        public void NavigateBack()
        {
            Journal.Pop();
        }

        public void NavigateToRoot()
        {
            while (Journal.Count > 1)
            {
                NavigateBack();
            }
        }

        public void NavigateWithParameter<TWhere, TParam>(TParam param)
            where TWhere : BaseViewModel
        {
            Journal.Push(
                (BaseViewModel)
                Activator.CreateInstance(typeof(TWhere),
                                         new object[] { param }));
        }

        public bool IsCanGoBack { get; set; }
    }
}
