using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;
using Usuarios.Data;
using Usuarios;
using Usuarios.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Usuarios.Pantallas;

namespace Usuarios
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class Menu : ContentPage
	{
		public Menu ()
		{
			InitializeComponent ();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            NavigationPage.SetHasNavigationBar(this, false);
        }

        private void btnCursos_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new Cursos());
        }

        private void btnEmpleados_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new MainPage());
        }

        private void BtnLogout_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new Login());
        }

        private void btnSCursos_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new Seguimiento());
        }
    }
}