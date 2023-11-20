using System;
using System.IO;
using Usuarios.Data;
using Usuarios.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Usuarios
{

  
    public partial class App : Application
    {
        static SQLiteHelper db;

        public App()
        {
            InitializeComponent();  
            MainPage = new NavigationPage(new Login());
            NavigationPage.SetHasNavigationBar(MainPage, false);
        }

        public static SQLiteHelper SQLiteDB
        {
            get
            {
                if (db==null)
                {
                    db = new SQLiteHelper(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Empresa.db3"));
                }
                return db;
            }
        }

       



        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
