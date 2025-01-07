using System.Windows.Input;
using CommunityToolkit.Maui.Views;

namespace AnnuaireEntreprise.Maui.Views;

public partial class SearchEmployeePopup : Popup
{
    public SearchEmployeePopup()
    {
        InitializeComponent();
    }
    public void OnClose(object sender, EventArgs e)
    {
        Close();
    }
}
