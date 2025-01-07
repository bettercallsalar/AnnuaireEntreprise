using CommunityToolkit.Maui.Views;

namespace AnnuaireEntreprise.Maui.Views;

public partial class ServicePopup : Popup
{
    public ServicePopup()
    {
        InitializeComponent();
    }
    public void OnClose(object sender, EventArgs e)
    {
        Close();
    }
}
