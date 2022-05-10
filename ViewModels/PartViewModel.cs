using CarRepairApp.Commands;
using CarRepairApp.Models.Entities;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;

namespace CarRepairApp.ViewModels
{
    public class PartViewModel : BaseViewModel
    {
        private string priceSearchText;
        private Car currentCar;

        public PartViewModel()
        {
            Title = "Просмотр запчастей";
            LoadCarsAsync();
            LoadPartsAsync();
        }

        private async void LoadPartsAsync()
        {
            IEnumerable<Part> currentParts =
                await PartDataStore.GetItemsAsync();
            if (CurrentCar != null && CurrentCar.Id != 0)
            {
                currentParts = currentParts
                    .Where(p => p.CarId == CurrentCar.Id);
            }
            if (!string.IsNullOrWhiteSpace(PriceSearchText))
            {
                currentParts = currentParts
                    .Where(p =>
                    {
                        return p.PriceInRubles
                            .ToString()
                            .Contains(PriceSearchText);
                    });
            }
            Parts = new ObservableCollection<Part>(currentParts);
        }

        private async void LoadCarsAsync()
        {
            Cars = new ObservableCollection<Car>(
                await CarDataStore.GetItemsAsync());
            Cars.Insert(0, new Car { Model = "Все модели машин" });
        }

        public ObservableCollection<Part> Parts { get; set; }
        public ObservableCollection<Car> Cars { get; set; }
        public Car CurrentCar
        {
            get => currentCar;
            set
            {
                currentCar = value;
                LoadPartsAsync();
            }
        }
        public string PriceSearchText
        {
            get => priceSearchText;
            set
            {
                priceSearchText = value;
                LoadPartsAsync();
            }
        }

        private Command goToAnaloguesCommand;
        public bool IsAnalogue { get; set; }

        public ICommand GoToAnaloguesCommand
        {
            get
            {
                if (goToAnaloguesCommand == null)
                    goToAnaloguesCommand = new Command(GoToAnaloguesViewModel);
                return goToAnaloguesCommand;
            }
        }

        private void GoToAnaloguesViewModel(object param)
        {
            NavigationService.NavigateWithParameter
                <AnalogueViewModel, Part>(param as Part);
        }
    }
}