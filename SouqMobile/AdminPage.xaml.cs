using ServiceReference1;
namespace SouqMobile;
public partial class AdminPage : ContentPage
{
    //Declare Server
    ServiceReference1.Service1Client server = new ServiceReference1.Service1Client();
    private string Key;
    private string Oper = "";
    public AdminPage()
    {
        InitializeComponent();
        this.Key = "0";
    }
    public AdminPage(string Key)
    {
        InitializeComponent();
        this.Key = Key;
        GetRow();
    }
    private void OnBackClicked(object sender, EventArgs e)
    {
        NavigateBack();
    }
    private async void OnDeleteClicked(object sender, EventArgs e)
    {
        bool answer = await DisplayAlert("Delete", "Are you sure?", "Yes", "No");
        if (answer)
        {
            Oper = General.session.doDelete;
            SetRow();
        }
    }
    private void OnEditClicked(object sender, EventArgs e)
    {
        Oper = General.session.doUpdate;
        EnableRow();
    }
    private void OnCancelClicked(object sender, EventArgs e)
    {
        Oper = General.session.doSelect;
        EnableRow();
    }
    private void OnOkClicked(object sender, EventArgs e)
    {
        if (!IsValidValues())
            SetRow();
    }
    private async void GetRow()
    {
        if (Key == "0")
        {
            Oper = General.session.doInsert;
            txtID.Text = "0";
        }
        else
        {
            Oper = General.session.doSelect;
            AdminsRec admin = await server.AdminsSelectAsync(Convert.ToInt64(Key));
            txtID.Text = admin.ID.ToString();
            txtName.Text = admin.Name;
            txtPhone.Text = admin.Phone;
            txtMail.Text = admin.Mail;
            txtUsername.Text = admin.Username;
        }
        EnableRow();
    }
    private async void SetRow()
    {
        //check if username exists in Passwords
        if (Oper == General.session.doInsert)
        {
            if (await server.PasswordsSelectAsync(txtUsername.Text) != null)
            {
                await DisplayAlert("Error", General.ERR_EXIST_Password, General.ERR_MISSING);
                return;
            }
        }
        try
        {
            AdminsRec admin = new AdminsRec();
            PasswordsRec password = new PasswordsRec();
            admin.ID = Convert.ToInt64(txtID.Text);
            admin.Name = txtName.Text;
            admin.Phone = txtPhone.Text;
            admin.Mail = txtMail.Text;
            admin.Username = txtUsername.Text;
            if (Oper == General.session.doInsert)
            {
                password.Username = txtUsername.Text;
                password.Password = txtPassword.Text;
                password.Type = General.session.AdminType;
            }
            bool bContinue = false;
            if (Oper == General.session.doInsert)
            {
                UserLogged userLogged = await server.AdminsInsertWithPasswordAsync(admin,
                password);
                bContinue = userLogged != null ? true : false;
            }
            else if (Oper == General.session.doUpdate)
                bContinue = await server.AdminsUpdateAsync(admin);
            else if (Oper == General.session.doDelete)
            {
                bContinue = await server.AdminsDeleteAsync(admin.ID) &&
                await server.PasswordsDeleteAsync(admin.Username);
            }
            if (!bContinue)
                await DisplayAlert("Error", await server.MessageAsync(), General.ERR_MISSING);
            else
                NavigateBack();
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", ex.Message, General.ERR_MISSING);
        }
    }
    private async void NavigateBack()
    {
        await Navigation.PopAsync();
    }
    private void EnableRow()
    {
        bool status;
        txtID.IsEnabled = false;
        txtUsername.IsEnabled = (Oper == General.session.doInsert);
        txtPassword.IsEnabled = (Oper == General.session.doInsert);
        lblPassword.IsVisible = (Oper == General.session.doInsert);
        txtPassword.IsVisible = (Oper == General.session.doInsert);
        if (Oper == General.session.doInsert ||
        Oper == General.session.doUpdate)
            status = true;
        else
            status = false;
        txtName.IsEnabled = status;
        txtPhone.IsEnabled = status;
        txtMail.IsEnabled = status;
        btnBack.IsEnabled = !status;
        btnEdit.IsEnabled = !status;
        btnDelete.IsEnabled = !status;
        btnOK.IsEnabled = status;
        btnCancel.IsEnabled = status;
        if (status)
            txtName.Focus();
    }
    private bool IsValidValues()
    {
        if (General.IsEmpty(txtName.Text))
        {
            DisplayAlert("Error", General.ERR_MISSING, General.ERR_MISSING);
            txtName.Focus();
            return false;
        }
        if (General.IsEmpty(txtPhone.Text) || !General.IsValidPhone(txtPhone.Text))
        {
            DisplayAlert("Error", General.ERR_MISSING_Phone, General.ERR_MISSING
            );
            txtPhone.Focus();
            return false;
        }
        if (General.IsEmpty(txtMail.Text) || !General.IsValidMail(txtMail.Text))
        {
            DisplayAlert("Error", General.ERR_MISSING_Mail, General.ERR_MISSING)
            ;
            txtMail.Focus();
            return false;
        }
        if (txtUsername.IsEnabled)
        {
            if (General.IsEmpty(txtUsername.Text))
            {
                DisplayAlert("Error", General.ERR_MISSING_Username, General.ERR_MISSING);
                txtUsername.Focus();
                return false;
            }
            if (General.IsEmpty(txtPassword.Text) || !General.IsValidPassword(txtPassword
            .Text))
            {
                DisplayAlert("Error", General.ERR_MISSING_Password, General.ERR_MISSING);
                txtPassword.Focus();
                return false;
            }
        }
        return true;
    }
}