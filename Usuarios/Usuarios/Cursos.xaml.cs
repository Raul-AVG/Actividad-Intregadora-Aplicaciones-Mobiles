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
	public partial class Cursos : ContentPage
	{

        public Cursos ()
		{
			InitializeComponent ();
            llenarDatos();
        }

        private async void BtnRegistrar_Clicked(object sender, EventArgs e)
        {
            if (ValidarDatos())
            {
                Curso curs = new Curso
                {
                    NombreDelCurso = txtNombreDelCurso.Text,
                    TipoDeCurso = (string)txtTipoDeCurso.SelectedItem,
                    Descripcion = txtDescripcion.Text,
                    Horas = int.Parse(txtHoras.Text),
                };

                await App.SQLiteDB.SaveCursosAsync(curs);

                await DisplayAlert("Registro", "Se guardo exitosamente", "OK");

                llenarDatos();
                LimpiarConrtoles();

            }
            else
            {
                await DisplayAlert("Advertencia", "Ingresar todos los datos", "OK");
            }

        }

        public async void llenarDatos()
        {
            var cursoList = await App.SQLiteDB.GetCursosAsync();
            if (cursoList != null)
            {
                lstCurso.ItemsSource = cursoList;
            }
            else
            {

            }
        }

        public bool ValidarDatos()
        {

            bool respuesta;
            if (string.IsNullOrEmpty(txtNombreDelCurso.Text))
            {
                respuesta = false;

            }

            else if (string.IsNullOrEmpty((string)txtTipoDeCurso.SelectedItem))
            {
                respuesta = false;

            }

            else if (string.IsNullOrEmpty(txtDescripcion.Text))
            {
                respuesta = false;

            }
            else if (string.IsNullOrEmpty(txtHoras.Text))
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
            if (!string.IsNullOrEmpty(txtIdCurso.Text))
            {
                Curso curso = new Curso()
                {
                    IdCurso = Convert.ToInt32(txtIdCurso.Text),
                    NombreDelCurso = txtNombreDelCurso.Text,
                    TipoDeCurso = (string)txtTipoDeCurso.SelectedItem,
                    Descripcion = txtDescripcion.Text,
                    Horas = Convert.ToInt32(txtHoras.Text),

                };

                await App.SQLiteDB.SaveCursosAsync(curso);
                await DisplayAlert("Registro", "Se actualizo de manera exitosa el Curso", "OK");

                txtIdCurso.IsVisible = false;
                BtnActualizar.IsVisible = false;
                BtnRegistrar.IsVisible = true;
                BtnEliminar.IsVisible = false;

                llenarDatos();
                LimpiarConrtoles();

            }
        }

        private async void BtnEliminar_Clicked(object sender, EventArgs e)
        {
            var curso = await App.SQLiteDB.GetCursosById(Convert.ToInt32(txtIdCurso.Text));
            if (curso != null)
            {
                await App.SQLiteDB.DeleteCursosAsync(curso);
                await DisplayAlert("Curso", "Se elimino de manera exitosa", "Ok");

                BtnRegistrar.IsVisible = true;
                txtIdCurso.IsVisible = false;
                BtnActualizar.IsVisible = false;
                BtnEliminar.IsVisible = false;

                LimpiarConrtoles();
                llenarDatos();
            }
        }

        private async void lstCurso_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var obj = (Curso)e.SelectedItem;
            BtnRegistrar.IsVisible = false;
            txtIdCurso.IsVisible = true;
            BtnActualizar.IsVisible = true;
            BtnEliminar.IsVisible = true;

            if (!string.IsNullOrEmpty(obj.IdCurso.ToString()))
            {
                var curso = await App.SQLiteDB.GetCursosById(obj.IdCurso);
                if (curso != null)
                {
                    txtIdCurso.Text = curso.IdCurso.ToString();
                    txtNombreDelCurso.Text = curso.NombreDelCurso;
                    txtTipoDeCurso.SelectedItem = curso.TipoDeCurso;
                    txtDescripcion.Text = curso.Descripcion;
                    txtHoras.Text = curso.Horas.ToString();

                }
            }
        }

        public void LimpiarConrtoles()
        {
            txtIdCurso.Text = "";
            txtNombreDelCurso.Text = "";
            txtDescripcion.Text = "";
            txtTipoDeCurso.SelectedItem = "";
            txtHoras.Text = "";
        }
    }
}