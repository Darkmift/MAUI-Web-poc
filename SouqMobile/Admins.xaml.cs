using ServiceReference1;
namespace SouqMobile;
public partial class Admins : ContentPage
{
    //Declare Server
    ServiceReference1.Service1Client server = new ServiceReference1.Service1Client();
    public Admins()
    {
        InitializeComponent();
    }
    protected override void OnAppearing()
    {
        FillListView();
    }
    protected async void FillListView()
    {
        lsvData.ItemsSource = await server.AdminsSelectAllAsync();
    }
    private async void OnBackClicked(object sender, EventArgs e)
    {
        await Navigation.PopAsync(); //Back
    }
    private void OnAddClicked(object sender, EventArgs e)
    {
        ShowRow("0");
    }
    private void OnItemSelected(object sender, SelectedItemChangedEventArgs e)
    {
        if (e.SelectedItem is AdminsRec adminRec)
        {
            string Key = adminRec.ID.ToString();
            ShowRow(Key);
        }
    }
    protected async void ShowRow(string Key)
    {
        // await Navigation.PushAsync(new Admin(Key));
        // TODO : Add Admin Page
    }
}