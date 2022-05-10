using CarRepairApp.Models.Entities;
using System.Collections.ObjectModel;
using System.Linq;

namespace CarRepairApp.ViewModels
{
    [PropertyChanged.AddINotifyPropertyChangedInterface]
    public class ContactViewModel : BaseViewModel
    {
        private string phoneSearchText;

        public ContactViewModel()
        {
            Title = "Просмотр контактов";
            LoadContactsAsync();
        }

        private async void LoadContactsAsync()
        {
            var currentContacts =
                await CustomerDataStore.GetItemsAsync();
            if (!string.IsNullOrWhiteSpace(PhoneSearchText))
            {
                currentContacts = currentContacts.Where(c =>
                {
                    return c.PhoneNumber.Contains(PhoneSearchText);
                });
            }
            Contacts = new ObservableCollection<Customer>(currentContacts);
        }

        public ObservableCollection<Customer> Contacts { get; set; }
        public string PhoneSearchText
        {
            get => phoneSearchText;
            set
            {
                phoneSearchText = value;
                LoadContactsAsync();
            }
        }
    }
}