using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Usuarios.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Usuarios
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Registro : ContentPage
    {
        public Registro()
        {
            InitializeComponent();
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            NavigationPage.SetHasNavigationBar(this, false);
        }
        private async void BtnRegistrar_Clicked(object sender, EventArgs e)
        {
            if (ValidarDatos())
            {
                Users usr = new Users
                {
                    
                    Email = txtEmailReg.Text,
                    Emailpassword = txtContraReg.Text,
                    NombreCompleto = txtNombreReg.Text,
                    Edad = int.Parse(txtEdadReg.Text),
                };

                await App.SQLiteDB.SaveUserAsync(usr);
                await DisplayAlert("Registro", "Se guardo exitosamente", "OK");

            }
            else
            {
                await DisplayAlert("Advertencia", "Ingresar todos los datos", "OK");
            }
        }

        public bool ValidarDatos()
        {
            bool respuesta;
            if (string.IsNullOrEmpty(txtEmailReg.Text))
            {
                respuesta = false;

            }
            else if (string.IsNullOrEmpty(txtContraReg.Text))
            {
                respuesta = false;

            }

            else if (string.IsNullOrEmpty(txtNombreReg.Text))
            {
                respuesta = false;

            }
            else if (string.IsNullOrEmpty(txtEdadReg.Text))
            {
                respuesta = false;

            }
            else
            {
                respuesta = true;
            }
            return respuesta;
        }

        private async void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Login());
        }
    }
}