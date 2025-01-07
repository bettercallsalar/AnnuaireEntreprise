
using CommunityToolkit.Maui.Views;

namespace AnnuaireEntreprise.Maui.Views;

public partial class ModifyEmployeePopup : Popup
{
    public ModifyEmployeePopup()
    {
        InitializeComponent();
    }
    public void OnClose(object sender, EventArgs e)
    {
        Close();
    }
}
