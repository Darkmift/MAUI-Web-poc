//Add Reference to Server
using ServiceReference1;
namespace SouqMobile;
public partial class LoginPage : ContentPage
{
    //Declare Server
    ServiceReference1.Service1Client server = new ServiceReference1.Service1Client();
    public LoginPage()
    {
        InitializeComponent();
    }
    private async void OnLoginButtonClicked(object sender, EventArgs e)
    {
        try
        {
            //Perform Login Operation in Server
            UserLogged user = await server.LoginAsync(txtUsername.Text, txtPassword.Text)
            ;
            if (user == null)
            {
                await DisplayAlert("Error", await server.MessageAsync(), "Try Again");
                txtUsername.Focus();
                return;
            }
            General.session = new Session();
            General.session.Username = txtUsername.Text;
            General.session.Password = txtPassword.Text;
            General.session.Type = user.Type;
            General.session.ID = user.ID;
            General.session.Type = user.Name;
            General.session.AdminType = await server.AdminTypeAsync();
            General.session.UserType = await server.UserTypeAsync();
            General.session.doSelect = await server.doSelectAsync();
            General.session.doInsert = await server.doInsertAsync();
            General.session.doUpdate = await server.doUpdateAsync();
            General.session.doDelete = await server.doDeleteAsync();
            await Navigation.PushAsync(new MainPage());
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", General.ERR_CANT_CONNET_SERVER + "\n" + ex.Message, "Try Again");
        }
    }
    private async void OnSignupButtonClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new SignupPage());
    }
}