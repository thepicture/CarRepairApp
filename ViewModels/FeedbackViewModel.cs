using CarRepairApp.Commands;
using CarRepairApp.Models.Entities;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;

namespace CarRepairApp.ViewModels
{
    [PropertyChanged.AddINotifyPropertyChangedInterface]
    public class FeedbackViewModel : BaseViewModel
    {
        public FeedbackViewModel()
        {
            Title = "Моя обратная связь";
            LoadFeedbacksAsync();
            LoadReceiversAsync();
        }

        private async void LoadReceiversAsync()
        {
            IEnumerable<User> receivers = await UserDataStore.GetItemsAsync();
            receivers = receivers.Where(r => r.RoleId != Identity.WeakIdentity.RoleId);
            Receivers = new ObservableCollection<User>(receivers);
        }

        private async void LoadFeedbacksAsync()
        {
            var feedbacks = await FeedbackDataStore.GetItemsAsync();
            feedbacks = feedbacks.Where(f =>
            {
                return f.ReceiverId == Identity.WeakIdentity.Id || f.PosterId == Identity.WeakIdentity.Id;
            });
            MyFeedbacks = new ObservableCollection<Feedback>(feedbacks);
        }

        public ObservableCollection<Feedback> MyFeedbacks { get; set; }

        public Feedback CurrentFeedback { get; set; } = new Feedback();
        public ObservableCollection<User> Receivers { get; set; }

        private Command sendFeedbackCommand;

        public ICommand SendFeedbackCommand
        {
            get
            {
                if (sendFeedbackCommand == null)
                    sendFeedbackCommand = new Command(SendFeedbackAsync);

                return sendFeedbackCommand;
            }
        }

        private async void SendFeedbackAsync()
        {
            if (await FeedbackDataStore.AddItemAsync(CurrentFeedback))
            {
                CurrentFeedback = new Feedback();
                LoadFeedbacksAsync();
            }
        }
    }
}