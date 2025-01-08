using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace AnnuaireEntreprise.Maui.ViewModels;

public partial class BaseViewModel : INotifyPropertyChanged
{
    // INotifyPropertyChanged implementation
    public event PropertyChangedEventHandler? PropertyChanged;

    // This method is called by the Set accessor of each property.
    protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    // This method is used to set a property and notify the UI of the change.
    protected bool SetProperty<T>(ref T backingStore, T value, [CallerMemberName] string propertyName = "")
    {
        if (EqualityComparer<T>.Default.Equals(backingStore, value))
            return false;

        backingStore = value;
        OnPropertyChanged(propertyName);
        return true;
    }

}
