
using CommunityToolkit.Maui.Views;

namespace AnnuaireEntreprise.Maui.Views;

public partial class ShowEmployeePopup : Popup
{
    public ShowEmployeePopup()
    {
        InitializeComponent();
    }
    public void OnClose(object sender, EventArgs e)
    {
        Close();
    }
}
