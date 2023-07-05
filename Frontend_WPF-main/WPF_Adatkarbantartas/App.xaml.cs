using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using WPF_Adatkarbantartas.Windows;

namespace WPF_Adatkarbantartas
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application

    {
        
        void MenuPontokBekapcsolasa(MainWindow window)
        {
            //MessageBox.Show(WPF_Adatkarbantartas.MainWindow. .ToString());
            //MessageBox.Show(WPF_Adatkarbantartas.MainWindow.Id.ToString());
            //MessageBox.Show(WPF_Adatkarbantartas.MainWindow.)
            if (WPF_Adatkarbantartas.MainWindow.Jogosultsag == 9 )
            {

                window.FelhAdatKarb.IsEnabled = true;
                window.FilmAdatKarb.IsEnabled = true;
            }

            if (WPF_Adatkarbantartas.MainWindow.Jogosultsag == 8)
            {

                window.FelhAdatKarb.IsEnabled = true;
                window.FilmAdatKarb.IsEnabled = true;
            }
        }
        
        private void Login(object sender, StartupEventArgs e)
        {
            Login login = new Login();
            Application.Current.MainWindow = login;
            HomeWindow window = new HomeWindow();
            login.ShowDialog();
            if (WPF_Adatkarbantartas.MainWindow.UserName == "")
            {
                MessageBox.Show($"Bezárta a felületet!");
                
                Application.Current.Shutdown();
            }
            else
            {
                MessageBox.Show("Sikeresen bejelentkezve: " + WPF_Adatkarbantartas.MainWindow.UserName);
                //MenuPontokBekapcsolasa(window);                
                window.Show();
            }
        }
    }
}