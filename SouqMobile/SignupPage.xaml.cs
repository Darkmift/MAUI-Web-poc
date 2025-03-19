//Add Reference to Server
using ServiceReference1;

namespace SouqMobile;
public partial class SignupPage : ContentPage
{
    //Declare Server
    ServiceReference1.Service1Client server = new ServiceReference1.Service1Client();
    public SignupPage()
    {
        InitializeComponent();
    }
    private async void OnCancelButtonClicked(object sender, EventArgs e)
    {
        await Navigation.PopToRootAsync(); //Back to Login
    }
    private async void OnSignupButtonClicked(object sender, EventArgs e)
    {
        try
        {
            //Check Valid Values
            if (!IsValidValues()) return;
            
            //check if username exists in Passwords
            if (await server.PasswordsSelectAsync(txtUsername.Text) != null)
            {
                await DisplayAlert("Error", General.ERR_EXIST_Password, "Try Again");
                return;
            }

            //Insert user to Users
            UsersRec user = new UsersRec();
            // Don't set ID - let the database handle it
            user.Name = txtName.Text;
            user.Phone = txtPhone.Text;
            user.Mail = txtMail.Text;
            user.Username = txtUsername.Text;

            PasswordsRec password = new PasswordsRec();
            password.Username = txtUsername.Text;
            password.Password = txtPassword.Text;
            password.Type = await server.UserTypeAsync();

            UserLogged userLogged = await server.UsersInsertWithPasswordAsync(user, password);
            
            if (userLogged == null)
            {
                await DisplayAlert("Error", "Failed to create user account. Please try again.", "OK");
                return;
            }

            General.session = new Session();
            General.session.Username = txtUsername.Text;
            General.session.Password = txtPassword.Text;
            General.session.Type = userLogged.Type;
            General.session.ID = userLogged.ID;
            General.session.Name = userLogged.Name;  // Fixed: Changed Type to Name
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
            await DisplayAlert("Error", $"An error occurred during signup: {ex.Message}", "OK");
        }
    }
    private bool IsValidValues()
    {
        if (General.IsEmpty(txtName.Text))
        {
            DisplayAlert("Error", General.ERR_MISSING, "Try Again");
            txtName.Focus();
            return false;
        }
        if (General.IsEmpty(txtPhone.Text) || !General.IsValidPhone(txtPhone.Text))
        {
            DisplayAlert("Error", General.ERR_MISSING, "Try Again");
            txtPhone.Focus();
            return false;
        }
        if (General.IsEmpty(txtMail.Text) || !General.IsValidMail(txtMail.Text))
        {
            DisplayAlert("Error", General.ERR_MISSING, "Try Again");
            txtMail.Focus();
            return false;
        }
        if (General.IsEmpty(txtUsername.Text) || !General.IsValidPassword(txtUsername.Text))
        {
            DisplayAlert("Error", General.ERR_MISSING, "Try Again");
            txtUsername.Focus();
            return false;
        }
        if (General.IsEmpty(txtPassword.Text) || !General.IsValidPassword(txtPassword.Text))
        {
            DisplayAlert("Error", General.ERR_MISSING, "Try Again");
            txtPassword.Focus();
            return false;
        }
        return true;
    }
}