using System;
using System.Linq;

using Usuarios.Models;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using SQLite;
using Usuarios;
using Xamarin.Forms.Shapes;
using Usuarios.Data;
using System.IO;

namespace Usuarios
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Login : ContentPage
    {
        public Login()
        {
            InitializeComponent();
        }

        private async void BtnLogin_Clicked(object sender, EventArgs e)
        {
            var dbPath = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Empresa.db3");
            var db = new SQLiteAsyncConnection(dbPath);
            var myquery = await db.Table<Users>().Where(u => u.Email.Equals(txtEmailLog.Text) && u.Emailpassword.Equals(txtContraLog.Text)).FirstOrDefaultAsync();

            if (myquery != null)
            {
                App.Current.MainPage = new NavigationPage(new Menu());
            }
            else
            {
                Device.BeginInvokeOnMainThread(async () =>
                {
                    var result = await this.DisplayAlert("Error", "Login failed", "Yes", "No");

                    if (result)
                        await Navigation.PushAsync(new Login());
                    else
                    {
                        await Navigation.PushAsync(new Login());
                    }
                });
            }
        }

        private void btnRegister_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new Registro());
        }

        private async void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Registro());
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            NavigationPage.SetHasNavigationBar(this, false);
        }
    }
}