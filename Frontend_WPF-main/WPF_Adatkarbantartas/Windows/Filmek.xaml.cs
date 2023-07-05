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
using System.Windows.Shapes;

namespace WPF_Adatkarbantartas.Windows
{
    /// <summary>
    /// Interaction logic for Filmek.xaml
    /// </summary>
    public partial class Filmek : Window
    {
        bool beolvasva = false;
        int ID = 0;

        public Filmek()
        {
            InitializeComponent();
            AdatBeolvasas();
            
        }

        private void AdatBeolvasas()
        {
            List<Models.Filmek> lista = new List<Models.Filmek>();
            WebClient client = new WebClient();
            client.Headers[HttpRequestHeader.ContentType] = "application/json";
            client.Encoding = Encoding.UTF8;
            string url = $"https://localhost:5001/Film/advanced";
            try
            {
                string result = client.DownloadString(url);
                lista = JsonConvert.DeserializeObject<List<Models.Filmek>>(result);
            }
            catch (Exception ex) { }
            Filmek_adatai.ItemsSource = lista;
            beolvasva = true;
        }

        







        private void MouseDown_Event(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                this.DragMove();
            }
        }
    }
}
