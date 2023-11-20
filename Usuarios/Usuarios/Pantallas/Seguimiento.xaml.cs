using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Usuarios.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Usuarios.Pantallas
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class Seguimiento : ContentPage
	{
		public Seguimiento ()
		{
			InitializeComponent ();
            LlenarDatos();
            txtEstatus.ItemsSource = new string[] {"Estatus", "Programado", "En Progreso", "Completo" };
            txtEstatus.SelectedIndex = 0;
        }

        private async void BtnRegistrar_Clicked(object sender, EventArgs e)
        {
            if (ValidarDatos())
            {
                SCurso scu = new SCurso
                {
                    NombreEmp = txtNEmp.Text,
                    NombreCurso = txtNCurso.Text,
                    Lugar = txtLugar.Text,
                    Fecha = txtFecha.Text,
                    Hora = txtHora.Text,
                    Estatus = (string)txtEstatus.SelectedItem,
                    Calificacion = int.Parse(txtCalificacion.Text),

                };

                await App.SQLiteDB.SaveSCursoAsync(scu);
                await DisplayAlert("Registro", "Se guardo exitosamente", "OK");

                LlenarDatos();
                LimpiarConrtoles();

            }
            else
            {
                await DisplayAlert("Advertencia", "Ingresar todos los datos", "OK");
            }
        }

        public async void LlenarDatos()
        {
            var scursoList = await App.SQLiteDB.GetSCursoAsync();
            if (scursoList != null)
            {
                lstSCurso.ItemsSource = scursoList;
            }
            else
            {

            }
        }

        public bool ValidarDatos()
        {

            bool respuesta;
            if (string.IsNullOrEmpty(txtNEmp.Text))
            {
                respuesta = false;

            }

            else if (string.IsNullOrEmpty(txtNCurso.Text))
            {
                respuesta = false;

            }

            else if (string.IsNullOrEmpty(txtLugar.Text))
            {
                respuesta = false;

            }
            else if (string.IsNullOrEmpty(txtFecha.Text))
            {
                respuesta = false;

            }
            else if (string.IsNullOrEmpty(txtHora.Text))
            {
                respuesta = false;

            }
            else if (string.IsNullOrEmpty(txtCalificacion.Text))
            {
                respuesta = false;

            }
            else if (string.IsNullOrEmpty((string)txtEstatus.SelectedItem))
            {
                respuesta = false;

            }
            else
            {
                respuesta = true;
            }
            return respuesta;

        }

        private async void BtnActualizar_Clicked(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtIdEmpleado.Text))
            {
                SCurso scurso = new SCurso()
                {
                    NombreEmp = txtNEmp.Text,
                    NombreCurso = txtNCurso.Text,
                    Lugar = txtLugar.Text,
                    Fecha = txtFecha.Text,
                    Hora = txtHora.Text,
                    Estatus = (string)txtEstatus.SelectedItem,
                    Calificacion = int.Parse(txtCalificacion.Text),

                };

                await App.SQLiteDB.SaveSCursoAsync(scurso);
                await DisplayAlert("Registro", "Se actualizo de manera exitosa el Empleado", "OK");

                txtIdEmpleado.IsVisible = false;
                BtnActualizar.IsVisible = false;
                BtnRegistrar.IsVisible = true;

                LlenarDatos();
                LimpiarConrtoles();

            }
        }

        private async void lstSCurso_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var obj = (SCurso)e.SelectedItem;
            BtnRegistrar.IsVisible = false;
            txtIdEmpleado.IsVisible = true;
            BtnActualizar.IsVisible = true;
            BtnEliminar.IsVisible = true;

            if (!string.IsNullOrEmpty(obj.IdEmpleado.ToString()))
            {
                var scurso = await App.SQLiteDB.GetSCursoById(obj.IdEmpleado);
                if (scurso != null)
                {
                    txtIdEmpleado.Text = scurso.IdEmpleado.ToString();
                    txtNEmp.Text = scurso.NombreEmp;
                    txtNCurso.Text = scurso.NombreCurso;
                    txtLugar.Text = scurso.Lugar;
                    txtFecha.Text = scurso.Fecha;
                    txtHora.Text = scurso.Hora.ToString();
                    txtEstatus.SelectedItem = scurso.Estatus;
                    txtCalificacion.Text = scurso.Calificacion.ToString();

                }
            }
        }

        private async void BtnEliminar_Clicked(object sender, EventArgs e)
        {
            var scurso = await App.SQLiteDB.GetSCursoById(Convert.ToInt32(txtIdEmpleado.Text));
            if (scurso != null)
            {
                await App.SQLiteDB.DeleteSCursoAsync(scurso);
                await DisplayAlert("Empleado", "Se elimino de manera exitosa", "Ok");

                BtnRegistrar.IsVisible = true;
                txtIdEmpleado.IsVisible = false;
                BtnActualizar.IsVisible = false;
                BtnEliminar.IsVisible = false;

                LimpiarConrtoles();
                LlenarDatos();
            }
        }

        public void LimpiarConrtoles()
        {
            txtIdEmpleado.Text = "";
            txtNEmp.Text = "";
            txtNCurso.Text = "";
            txtLugar.Text = "";
            txtFecha.Text = "";
            txtHora.Text = "";
            txtCalificacion.Text = "";
            txtEstatus.SelectedItem = "";
        }

    }
}