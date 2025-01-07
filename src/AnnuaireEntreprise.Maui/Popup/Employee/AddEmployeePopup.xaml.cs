
using CommunityToolkit.Maui.Views;

namespace AnnuaireEntreprise.Maui.Views;

public partial class AddEmployeePopup : Popup
{
    public AddEmployeePopup()
    {
        InitializeComponent();
    }
    public void OnClose(object sender, EventArgs e)
    {
        Close();
    }
}
