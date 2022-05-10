using CarRepairApp.Models.Entities;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace CarRepairApp.ViewModels
{
    [PropertyChanged.AddINotifyPropertyChangedInterface]
    public class WorkProcessViewModel : BaseViewModel
    {
        private Car currentCar;

        public WorkProcessViewModel()
        {
            Title = "Учёт этапов выполнения работ и оплат";
            LoadCarsAsync();
            LoadWorkProcessesAsync();
        }

        private async void LoadWorkProcessesAsync()
        {
            IEnumerable<WorkProcess> currentWorkProcesses = 
                await WorkProcessDataStore.GetItemsAsync();
            if (CurrentCar != null && CurrentCar.Id != 0)
            {
                currentWorkProcesses = currentWorkProcesses
                    .Where(w => w.CarId == CurrentCar.Id);
            }
            WorkProcesses = new ObservableCollection<WorkProcess>(currentWorkProcesses);
        }

        private async void LoadCarsAsync()
        {
            Cars = new ObservableCollection<Car>(
                await CarDataStore.GetItemsAsync());
            Cars.Insert(0, new Car { Model = "Все модели машин" });
        }

        public ObservableCollection<Car> Cars { get; set; }
        public Car CurrentCar
        {
            get => currentCar;
            set
            {
                currentCar = value;
                LoadWorkProcessesAsync();
            }
        }
        public ObservableCollection<WorkProcess> WorkProcesses { get; set; }
    }
}