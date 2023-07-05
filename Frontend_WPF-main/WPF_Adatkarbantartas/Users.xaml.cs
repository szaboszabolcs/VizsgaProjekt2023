using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WPF_Adatkarbantartas.Models;

namespace WPF_Adatkarbantartas
{
    /// <summary>
    /// Interaction logic for Users.xaml
    /// </summary>
    public partial class Users : UserControl
    {
        bool beolvasva = false;
        int ID = 0;
        string Salt = "";
        string Hash = "";


        public Users()
        {
            InitializeComponent();
            if (!beolvasva)
            {
                AdatBeolvasas();
                List<int> JogLista = new List<int>() { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 };
                List<int> AktivLista = new List<int>() { 0, 1 };
                cmb_Jogosultsag.ItemsSource = JogLista;
                cmb_Aktiv.ItemsSource = AktivLista;


            }
        }

        private void MezokTorlese()
        {
            ID = 0;
            txb_FelhasznaloNev.Text = "";
            pwb_Password.Password = "";
            txb_TeljesNev.Text = "";
            txb_Email.Text = "";
            cmb_Jogosultsag.Text = "0";
            cmb_Aktiv.Text = "1";
        }

        private string Ellenorzes()
        {
            return "";
        }
        

        private void AdatBeolvasas()
        {
            List<Models.Felhasznalok> list = new List<Models.Felhasznalok>();
            WebClient client = new WebClient();
            client.Headers[HttpRequestHeader.ContentType] = "application/json";
            client.Encoding = Encoding.UTF8;
            string url = $"https://localhost:5001/Felhasznalok/{WPF_Adatkarbantartas.MainWindow.uId}";
            MezokTorlese();
            try
            {
                string result = client.DownloadString(url);
                list = JsonConvert.DeserializeObject<List<Models.Felhasznalok>>(result);
            }
            catch (Exception ex) { }
            Felhasznalok.ItemsSource = list;
            beolvasva = true;
        }

        private void Felhasznalok_adatai_Changed(object sender, RoutedEventArgs e)
        {
            try
            {
                Models.Felhasznalok felhasznalo = Felhasznalok.SelectedItems[0] as Models.Felhasznalok;
                ID = felhasznalo.Id;
                txb_FelhasznaloNev.Text = felhasznalo.FelhasznaloNev;
                pwb_Password.Password = "";
                Salt = felhasznalo.Salt;
                Hash = felhasznalo.Hash;
                txb_TeljesNev.Text = felhasznalo.TeljesNev;
                txb_Email.Text = felhasznalo.Email;
                cmb_Jogosultsag.Text = felhasznalo.Jogosultsag.ToString();
                cmb_Aktiv.Text = felhasznalo.Aktiv.ToString();
            }
            catch (Exception ex)
            {
                MezokTorlese();
            }
        }

        private void btn_Tarolas_Click(object sender, RoutedEventArgs e)
        {
            string uzenet = Ellenorzes();
            if (uzenet == "")
            {
                Models.Felhasznalok felhasznalo = new Models.Felhasznalok();
                felhasznalo.FelhasznaloNev = txb_FelhasznaloNev.Text;
                felhasznalo.TeljesNev = txb_TeljesNev.Text;
                felhasznalo.Salt = MainWindow.GenerateSalt();
                felhasznalo.Hash = MainWindow.CreateSHA256(MainWindow.CreateSHA256(pwb_Password.Password + felhasznalo.Salt));
                felhasznalo.Email = txb_Email.Text;
                felhasznalo.Jogosultsag = int.Parse(cmb_Jogosultsag.Text);
                felhasznalo.Aktiv = int.Parse(cmb_Aktiv.Text);
                WebClient client = new WebClient();
                client.Headers[HttpRequestHeader.ContentType] = "application/json";
                client.Encoding = Encoding.UTF8;
                string url = $"https://localhost:5001/Felhasznalok/{WPF_Adatkarbantartas.MainWindow.uId}";
                try
                {
                    string result = client.UploadString(url, "POST", JsonConvert.SerializeObject(felhasznalo));
                    MessageBox.Show(result);
                    AdatBeolvasas();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                MessageBox.Show(uzenet);
            }
        }

        private void btn_Modositas_Click(object sender, RoutedEventArgs e)
        {
            string uzenet = Ellenorzes();
            if (uzenet == "")
            {
                if (ID != 0)
                {
                    Models.Felhasznalok felhasznalo = new Models.Felhasznalok();
                    felhasznalo.Id = ID;
                    felhasznalo.FelhasznaloNev = txb_FelhasznaloNev.Text;
                    felhasznalo.TeljesNev = txb_TeljesNev.Text;
                    if (pwb_Password.Password != "")
                    {
                        felhasznalo.Salt = MainWindow.GenerateSalt();
                        felhasznalo.Hash = MainWindow.CreateSHA256(MainWindow.CreateSHA256(pwb_Password.Password + felhasznalo.Salt));
                    }
                    else
                    {
                        felhasznalo.Salt = Salt;
                        felhasznalo.Hash = Hash;
                    }
                    felhasznalo.Email = txb_Email.Text;
                    felhasznalo.Jogosultsag = int.Parse(cmb_Jogosultsag.Text);
                    felhasznalo.Aktiv = int.Parse(cmb_Aktiv.Text);
                    WebClient client = new WebClient();
                    client.Headers[HttpRequestHeader.ContentType] = "application/json";
                    client.Encoding = Encoding.UTF8;
                    string url = $"https://localhost:5001/Felhasznalok/{WPF_Adatkarbantartas.MainWindow.uId}";
                    try
                    {
                        string result = client.UploadString(url, "PUT", JsonConvert.SerializeObject(felhasznalo));
                        MessageBox.Show(result);
                        AdatBeolvasas();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
            }
            else
            {
                MessageBox.Show(uzenet);
            }
        }

        private void btn_Torles_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show($"Biztosan törli a(z) {txb_TeljesNev.Text} nevű felhasználót?",
                   "Felhasználó törlése",
                   MessageBoxButton.YesNo,
                   MessageBoxImage.Warning) == MessageBoxResult.Yes)
            {
                Models.Felhasznalok felhasznalo = new Models.Felhasznalok();
                felhasznalo.Id = ID;
                WebClient client = new WebClient();
                client.Headers[HttpRequestHeader.ContentType] = "application/json";
                client.Encoding = Encoding.UTF8;
                string url = $"https://localhost:5001/Felhasznalok/{WPF_Adatkarbantartas.MainWindow.uId}?id={ID}";
                try
                {
                    string result = client.UploadString(url, "DELETE", "");
                    MessageBox.Show(result);
                    AdatBeolvasas();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }
    }
}