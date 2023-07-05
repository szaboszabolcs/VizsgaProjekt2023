using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using MySqlX.XDevAPI.Common;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace WPF_Adatkarbantartas.Windows
{
    /// <summary>
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : Window
    {
        int szamlalo = 3;

        int jogosultsag = 0;

        public Login()
        {
            InitializeComponent();
        }

        // Salt lekérése adatbázisból //
        private string SaltRequest(string UserName)
        {
            WebClient client = new WebClient();
            client.Headers[HttpRequestHeader.ContentType] = "application/json";
            client.Encoding = System.Text.Encoding.UTF8;
            try
            {
                string result = client.UploadString("https://localhost:5001/Login/SaltRequest/" + UserName, "POST");
                return result;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        // Felhasználó bejelentkezése //
        private string[] LoginUser(string nev, string tmpHash)
        {
            string[] valasz = new string[3];
            WebClient client = new WebClient();
            client.Headers[HttpRequestHeader.ContentType] = "application/json";
            client.Encoding = System.Text.Encoding.UTF8;
            string url = $"https://localhost:5001/Login?nev={nev}&tmpHash={tmpHash}";
            try
            {
                string result = client.UploadString(url, "POST");
                valasz = JsonConvert.DeserializeObject<string[]>(result);
                MainWindow.Jogosultsag = int.Parse(valasz[2]);
                jogosultsag = int.Parse(valasz[2]);
                return valasz;
            }
            catch (Exception ex)
            {
                valasz[0] = ex.Message;
                valasz[1] = "";
                return valasz;
            }
        }

        // Ablak mozgatása egérrel //
        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
                DragMove();
        }

        // Ablak tálcára tétele //
        private void btnMinimize_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }
        
        // Ablak bezárása //
        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show($"Biztos, hogy ki szeretne lépni a felületből?",
                   "Kijelentkezés",
               MessageBoxButton.YesNo,
                  MessageBoxImage.Warning) == MessageBoxResult.Yes)
            {
                MessageBox.Show($"\nViszontlátásra  " + WPF_Adatkarbantartas.MainWindow.UserName + "!");
                Application.Current.Shutdown();
            }
        }

        // Bejelentkezési eljárás //
        private void Login_Click(object sender, RoutedEventArgs e)
        {
            string salt = SaltRequest(UserName.Text);
            string tmpHash = MainWindow.CreateSHA256(Password.Password + salt);
            string[] result = LoginUser(UserName.Text, tmpHash);
            if (szamlalo > 0)
            {
                if (jogosultsag==8 || jogosultsag==9)
                {
                    WPF_Adatkarbantartas.MainWindow.uId = result[0];
                    WPF_Adatkarbantartas.MainWindow.UserName = result[1];
                    this.Close();

                    /*MessageBox.Show("Sikeres Bejelentkezés. Átírányítás folyamatban.");
                    MainWindow main = new MainWindow();
                    main.Show();
                    */


                }
                else
                {
                    szamlalo--;
                    MessageBox.Show($"Sikertelen bejelentkezés!\n Önnek nincs jogosultsága bejelentkezni a felületre!");
                    if (szamlalo == 0)
                    {
                        MessageBox.Show("Nincs több próbálkozása!");
                        this.Close();
                    }
                }
            }            
        }
    }
}