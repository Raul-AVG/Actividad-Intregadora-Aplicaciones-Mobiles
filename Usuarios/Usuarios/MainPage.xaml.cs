using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Usuarios.Models;
using Xamarin.Forms;

namespace Usuarios
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            LlenarDatos();
            txtTipoEmpleado.ItemsSource = new string[] {"Tipo de Empleado", "Planta", "Temporal" };
            txtTipoEmpleado.SelectedIndex = 0;
        }

        private async void BtnRegistrar_Clicked(object sender, EventArgs e)
        {
            if (ValidarDatos())
            {
                Empleado emp = new Empleado
                {
                    Nombre = txtNombre.Text,
                    Direccion = txtDireccion.Text,
                    Telefono = int.Parse(txtTelefono.Text),
                    Edad = int.Parse(txtEdad.Text),
                    CURP = txtCURP.Text,
                    TipoEmpleado = (string)txtTipoEmpleado.SelectedItem,
                };

                await App.SQLiteDB.SaveEmpleadoAsync(emp);

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
            var empleadoList = await App.SQLiteDB.GetEmpleadosAsync();
            if (empleadoList != null)
            {
                lstEmpleado.ItemsSource = empleadoList;
            }
            else
            {

            }
        }

        public bool ValidarDatos()
        {

            bool respuesta;
            if (string.IsNullOrEmpty(txtNombre.Text))
            {
                respuesta = false;

            }

            else if (string.IsNullOrEmpty(txtDireccion.Text))
            {
                respuesta = false;

            }

            else if (string.IsNullOrEmpty(txtCURP.Text))
            {
                respuesta = false;

            }
            else if (string.IsNullOrEmpty(txtEdad.Text))
            {
                respuesta = false;

            }
            else if (string.IsNullOrEmpty(txtTelefono.Text))
            {
                respuesta = false;

            }
            else if (string.IsNullOrEmpty((string)txtTipoEmpleado.SelectedItem))
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
                Empleado empleado = new Empleado()
                {
                    IdEmpleado = int.Parse(txtIdEmpleado.Text),
                    Nombre = txtNombre.Text,
                    Direccion = txtDireccion.Text,
                    Telefono = int.Parse(txtTelefono.Text),
                    Edad = int.Parse(txtEdad.Text),
                    CURP = txtCURP.Text,
                    TipoEmpleado = (string)txtTipoEmpleado.SelectedItem,

                };

                await App.SQLiteDB.SaveEmpleadoAsync(empleado);
                await DisplayAlert("Registro", "Se actualizo de manera exitosa el Empleado", "OK");

                txtIdEmpleado.IsVisible = false;
                BtnActualizar.IsVisible = false;
                BtnRegistrar.IsVisible = true;

                LlenarDatos();
                LimpiarConrtoles(); 

            }
        }

        private async void LstEmpleado_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var obj = (Empleado)e.SelectedItem;
            BtnRegistrar.IsVisible = false;
            txtIdEmpleado.IsVisible = true;
            BtnActualizar.IsVisible = true;
            BtnEliminar.IsVisible = true;

            if (!string.IsNullOrEmpty(obj.IdEmpleado.ToString()))
            {
                var empleado = await App.SQLiteDB.GetEmpleadoById(obj.IdEmpleado);
                if (empleado!= null)
                {
                    txtIdEmpleado.Text = empleado.IdEmpleado.ToString();
                    txtNombre.Text = empleado.Nombre;
                    txtDireccion.Text = empleado.Direccion;
                    txtTelefono.Text = empleado.Telefono.ToString();
                    txtEdad.Text = empleado.Edad.ToString();
                    txtCURP.Text = empleado.CURP.ToString();
                    txtTipoEmpleado.SelectedItem = empleado.TipoEmpleado.ToString();
                    
                }
            }
        }

        private async void BtnEliminar_Clicked(object sender, EventArgs e)
        {
            var empleado = await App.SQLiteDB.GetEmpleadoById(Convert.ToInt32(txtIdEmpleado.Text));
            if (empleado != null)
            {
                await App.SQLiteDB.DeleteEmpleadoAsync(empleado);
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
            txtNombre.Text = "";
            txtDireccion.Text = "";
            txtTelefono.Text = "";
            txtEdad.Text = "";
            txtCURP.Text = "";
            txtTipoEmpleado.SelectedItem = "";                      
        }
    }
}
