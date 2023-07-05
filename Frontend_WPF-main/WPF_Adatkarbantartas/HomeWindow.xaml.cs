using System;
using System.Collections.Generic;
using System.Linq;
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
using System.Security.Cryptography;
using System.Net;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;
using Newtonsoft.Json;

namespace WPF_Adatkarbantartas
{
    /// <summary>
    /// Interaction logic for HomeWindow.xaml
    /// </summary>
    public partial class HomeWindow : Window
    {
        
        public HomeWindow()
        {
            InitializeComponent();
            
        }

        private void HomeExit_Click(object sender, RoutedEventArgs e)
        {
            string result;
            WebClient client = new WebClient();
            client.Headers[HttpRequestHeader.ContentType] = "application/json";
            client.Encoding = System.Text.Encoding.UTF8;
            try
            {
                result = client.UploadString("https://localhost:5001/Logout/" + WPF_Adatkarbantartas.MainWindow.uId, "POST");
            }
            catch (Exception ex)
            {
                result = ex.Message;
            }

            if (MessageBox.Show($"Biztos, hogy ki szeretne lépni a felületből?",
                    "Kijelentkezés",
                MessageBoxButton.YesNo,
                   MessageBoxImage.Warning) == MessageBoxResult.Yes)
            {
                MessageBox.Show($"{result}\nViszontlátásra  " + WPF_Adatkarbantartas.MainWindow.UserName + "!");
                Application.Current.Shutdown();
            }
            //MessageBox.Show($"{result}\nViszontlátásra  " + WPF_Adatkarbantartas.MainWindow.UserName + "!");
            //Application.Current.Shutdown();
        }


        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                this.DragMove();
            }
        }

        private void pnlControlBar_MouseEnter(object sender, MouseEventArgs e)
        {
            this.MaxHeight = SystemParameters.MaximizedPrimaryScreenHeight;
        }

        // Főoldal bezárása //

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            string result;
            WebClient client = new WebClient();
            client.Headers[HttpRequestHeader.ContentType] = "application/json";
            client.Encoding = System.Text.Encoding.UTF8;
            try
            {
                result = client.UploadString("https://localhost:5001/Logout/" + WPF_Adatkarbantartas.MainWindow.uId, "POST");
            }
            catch (Exception ex)
            {
                result = ex.Message;
            }

            if (MessageBox.Show($"Biztos, hogy ki szeretne lépni a felületből?",
                   "Kijelentkezés",
               MessageBoxButton.YesNo,
                  MessageBoxImage.Warning) == MessageBoxResult.Yes)
            {
                MessageBox.Show($"{result}\nViszontlátásra  " + WPF_Adatkarbantartas.MainWindow.UserName + "!");
                Application.Current.Shutdown();
            }

            //MessageBox.Show($"{result}\nViszontlátásra  " + WPF_Adatkarbantartas.MainWindow.UserName + "!");
            //Application.Current.Shutdown();
        }

        // Főoldal tálcára rakása //

        private void btnMinimize_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        // Főoldal tejles képernyő //
        private void btnMaximize_Click(object sender, RoutedEventArgs e)
        {
            if (this.WindowState == WindowState.Normal)
                this.WindowState = WindowState.Maximized;
            else this.WindowState = WindowState.Normal;
        }

        // Megnyitja a Home felületet //
        private void rbtn1_Checked(object sender, RoutedEventArgs e)
        {
            CC.Content = new UserControl1();
        }

        // Megnyitja a Users felületet //
        private void rbtn2_Checked(object sender, RoutedEventArgs e)
        {
            CC.Content = new Users();
        }

        // Megynyitja a Settings felületet //
        private void rbtn3_Checked(object sender, RoutedEventArgs e)
        {
            CC.Content = new UserControl2();
        }

        private void rbtn4_Checked(object sender, RoutedEventArgs e)
        {
            CC.Content = new FilmsControl();
        }


        
    }
}
