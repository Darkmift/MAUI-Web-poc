namespace SouqMobile
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            ShowMenu();
        }
        private void ShowMenu()
        {
            lblName.Text = General.TITLE + ", " + General.session.Name;
            string type = General.session.Type;
            btnAdmins.IsVisible = (type == General.session.AdminType);
            btnUsers.IsVisible = (type == General.session.AdminType);
            btnProfile.IsVisible = (type == General.session.UserType);
            btnPassword.IsVisible = (type == General.session.UserType);
            btnCountries.IsVisible = (type == General.session.AdminType);
            btnCategories.IsVisible = (type == General.session.AdminType);
            btnProducts.IsVisible = true;
            btnOrders.IsVisible = true;
            btnReports.IsVisible = true;
            btnExit.IsVisible = true;
        }
        private async void OnExitClicked(object sender, EventArgs e)
        {
            await Navigation.PopToRootAsync(); // Navigate back to LoginPage
        }
        private async void OnAdminsClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Admins());
        }
        private void OnUsersClicked(object sender, EventArgs e)
        {
            //
        }
        private void OnProfileClicked(object sender, EventArgs e)
        {
            //
        }
        private void OnPasswordClicked(object sender, EventArgs e)
        {
            //
        }
        private void OnCountriesClicked(object sender, EventArgs e)
        {
            //
        }
        private void OnCategoriesClicked(object sender, EventArgs e)
        {
            //
        }
        private void OnProductsClicked(object sender, EventArgs e)
        {
            //
        }
        private void OnOrdersClicked(object sender, EventArgs e)
        {
            //
        }
        private void OnReportsClicked(object sender, EventArgs e)
        {
            //
        }
    }
}
