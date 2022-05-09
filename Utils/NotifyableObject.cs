using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace CarRepairApp.Utils
{
    public class NotifyableObject : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public void NotifyFor([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?
                .Invoke(this, 
                        new PropertyChangedEventArgs(nameof(propertyName)));
        }
    }
}
