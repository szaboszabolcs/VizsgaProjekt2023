using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.VisualBasic.ApplicationServices;
using Microsoft.VisualBasic.Logging;
using MySql.Data.MySqlClient;
using MySqlX.XDevAPI;
using MySqlX.XDevAPI.Common;
using Newtonsoft.Json;
using Org.BouncyCastle.Utilities.Collections;
using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WPF_Adatkarbantartas.Models;
using static Google.Protobuf.WellKnownTypes.Field.Types;
using static System.Net.WebRequestMethods;
using File = System.IO.File;
using UserControl = System.Windows.Controls.UserControl;

namespace WPF_Adatkarbantartas
{


    /// <summary>
    /// Interaction logic for FilmsControl.xaml
    /// </summary>
    /// 

    public class ByteArrayToImageSourceConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {

            /*byte[] imageData = (byte[])value;

            BitmapImage biImg = new BitmapImage();
            MemoryStream ms = new MemoryStream(imageData);
            biImg.BeginInit();
            biImg.StreamSource = ms;
            biImg.EndInit();

            ImageSource imgSrc = biImg as ImageSource;

            return imgSrc;
            */

            byte[] imageData = (byte[])value;

            
            BitmapFrame frame = BitmapFrame.Create(
                new MemoryStream(imageData),
                BitmapCreateOptions.None,
                BitmapCacheOption.OnLoad);

            return frame;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public partial class FilmsControl : UserControl
    {

        public FilmsControl()
        {
            InitializeComponent();
            FilmBeolvasas();


        }

       
        private void MezokTorlese()
        {
            ID = 0;
            txb_Cim.Text = "";
            txb_Kategoria.Text = "";
            txb_Leiras.Text = "";
            txb_Ertekeles.Text = "";
        }

        private string Ellenorzes()
        {
            return "";
        }

        bool beolvasas = false;
        int ID = 0;

        private void FilmBeolvasas()
        {
            List<Models.Filmek> filmeks = new List<Models.Filmek>();
            WebClient client = new WebClient();
            client.Headers[HttpRequestHeader.ContentType] = "application/json";
            client.Encoding = Encoding.UTF8;
            string url = $"https://localhost:5001/Film/basic";
            MezokTorlese();
            try
            {
                string result = client.DownloadString(url);
                filmeks = JsonConvert.DeserializeObject<List<Models.Filmek>>(result);
            }
            catch (Exception ex) { }
            Filmek.ItemsSource = filmeks;
            beolvasas = true;
        }

        private void Filmek_adatai_Changed(object sender, RoutedEventArgs e)
        {
            try
            {
                Models.Filmek film = Filmek.SelectedItems[0] as Models.Filmek;
                ID = film.Id;
                txb_Cim.Text = film.Cim;
                txb_Kategoria.Text = film.Kategoria;
                txb_Leiras.Text = film.Leiras;
                txb_Ertekeles.Text = film.Ertekeles.ToString();
                txb_Filmlogo.Text = film.Filmlogo.ToString();
            }
            catch (Exception ex)
            {
                MezokTorlese();
            }
        }

        private void btn_Modosit_Click(object sender, RoutedEventArgs e)
        {
            string uzenet = Ellenorzes();
            if (uzenet == "")
            {
                if (ID != 0)
                {
                    Models.Filmek film = new Models.Filmek();
                    film.Id = ID;
                    film.Cim = txb_Cim.Text;
                    film.Kategoria = txb_Kategoria.Text;
                    film.Leiras = txb_Leiras.Text;
                    film.Ertekeles = int.Parse(txb_Ertekeles.Text);

                    // Retrieve the image data from the database
                    byte[] imageData = RetrieveImageDataFromDatabase(ID);

                    // Update the film object with the retrieved image data
                    film.Filmlogo = imageData;

                    WebClient client = new WebClient();
                    client.Headers[HttpRequestHeader.ContentType] = "application/json";
                    client.Encoding = Encoding.UTF8;
                    string url = $"https://localhost:5001/Film/{WPF_Adatkarbantartas.MainWindow.uId}";
                    try
                    {
                        string result = client.UploadString(url, "PUT", JsonConvert.SerializeObject(film));
                        System.Windows.MessageBox.Show(result);
                        FilmBeolvasas();
                    }
                    catch (Exception ex)
                    {
                        System.Windows.MessageBox.Show(ex.Message);
                    }
                }
            }
            else
            {
                System.Windows.MessageBox.Show(uzenet);
            }
        }

        private byte[] RetrieveImageDataFromDatabase(int id)
        {
            using (var context = new zaroprojektContext())
            {
                // Retrieve the film from the database
                var film = context.Filmeks.Find(id);

                // If the film exists and has image data, return the image data
                if (film != null && film.Filmlogo != null)
                {
                    return film.Filmlogo;
                }
            }

            // If the film does not exist or has no image data, return null
            return null;
        }
        






        private void AddImageButton_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image files (*.jpg, *.jpeg, *.png) | *.jpg; *.jpeg; *.png";
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                string filePath = openFileDialog.FileName;

                // Read the image file into a byte array
                byte[] imageData = File.ReadAllBytes(filePath);

                // Convert the byte array to a Base64 string
                string base64String = Convert.ToBase64String(imageData);

                // Set the text of the Filmlogo textbox to the Base64 string
                txb_Filmlogo.Text = base64String;
            }
        }
        private void btn_Tarolas_Click(object sender, RoutedEventArgs e)
        {
            string uzenet = Ellenorzes();
            if (uzenet == "")
            {
                Models.Filmek film = new Models.Filmek();
                film.Id = ID;
                film.Cim = txb_Cim.Text;
                film.Kategoria = txb_Kategoria.Text;
                film.Leiras = txb_Leiras.Text;
                film.Ertekeles = int.Parse(txb_Ertekeles.Text);

                // Convert the Base64 string back to a byte array
                byte[] imageData = Convert.FromBase64String(txb_Filmlogo.Text);
                film.Filmlogo = imageData;
                WebClient client = new WebClient();
                client.Headers[HttpRequestHeader.ContentType] = "application/json";
                client.Encoding = Encoding.UTF8;
                string url = $"https://localhost:5001/Film/{WPF_Adatkarbantartas.MainWindow.uId}";
                try
                {
                    string result = client.UploadString(url, "POST", JsonConvert.SerializeObject(film));
                    System.Windows.MessageBox.Show(result);
                    FilmBeolvasas();
                }
                catch (Exception ex)
                {
                    System.Windows.MessageBox.Show(ex.Message);
                }
            }
            else
            {
                System.Windows.MessageBox.Show(uzenet);
            }
        }
        private void btn_Torles_Click(object sender, RoutedEventArgs e)
        {
            if (System.Windows.MessageBox.Show($"Biztosan törli a(z) {txb_Cim.Text} nevű filmet?",
            "Film törlése",
            MessageBoxButton.YesNo,
                   MessageBoxImage.Warning) == MessageBoxResult.Yes)
            {
                Models.Filmek film = new Models.Filmek();
                film.Id = ID;
                WebClient client = new WebClient();
                client.Headers[HttpRequestHeader.ContentType] = "application/json";
                client.Encoding = Encoding.UTF8;
                string url = $"https://localhost:5001/Film/{WPF_Adatkarbantartas.MainWindow.uId}?id={ID}";
                try
                {
                    string result = client.UploadString(url, "DELETE", "");
                    System.Windows.MessageBox.Show(result);
                    FilmBeolvasas();
                }
                catch (Exception ex)
                {
                    System.Windows.MessageBox.Show(ex.Message);
                }
            }
        }
    }
}
