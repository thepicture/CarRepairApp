using CarRepairApp.Models.Entities;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace CarRepairApp.ViewModels
{
    [PropertyChanged.AddINotifyPropertyChangedInterface]
    public class AnalogueViewModel : BaseViewModel
    {
        public AnalogueViewModel(Part part)
        {
            Part = part;
            Title = $"Аналоги запчасти {part.Title}";
            IsAnalogue = true;
            LoadCarsAsync();
            LoadPartsAsync();
        }

        private string priceSearchText;
        private Car currentCar;

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
            currentParts = currentParts.Where(p =>
            {
                bool areConditionsMet = p.BaseCode[0] == Part.BaseCode[0]
                                        || Enumerable.SequenceEqual(p.BaseCode
                                                                        .Skip(1)
                                                                        .Take(3),
                                                                    Part.BaseCode
                                                                        .Skip(1)
                                                                        .Take(3));
                return areConditionsMet && p.Id != Part.Id;
            });
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

        public Part Part { get; }
        public bool IsAnalogue { get; set; }
    }
}
