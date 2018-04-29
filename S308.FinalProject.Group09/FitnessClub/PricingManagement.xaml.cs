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
using System.Windows.Shapes;
using System.IO;
using Newtonsoft.Json;

namespace FitnessClub
{
    /// <summary>
    /// Interaction logic for MembershipQuote.xaml
    /// </summary>
    public partial class PricingManagement : Window
    {

        List<Pricing> priceList;
        public PricingManagement()
        {
            InitializeComponent();

            string strFilePath = GetFilePath("txt");

            try
            {
                StreamReader reader = new StreamReader(strFilePath);
                string price = reader.ReadLine();

                while (price != null)
                {
                    lsbOutput.Items.Add(price);
                    price = reader.ReadLine();
                }
            }

            catch (Exception ex)
            {
                MessageBox.Show("Error in import process: " + ex.Message);
            }


            Pricing pricingNew = new Pricing();
        }


        private string GetFilePath(string extension)
        {
            string strFilePath = @"..\..\..\..\Data\Pricing";
            string strTimestamp = DateTime.Now.Ticks.ToString();

            strFilePath += "." + extension;

            return strFilePath;
        }

        private void btnMainMenu_Click(object sender, RoutedEventArgs e)
        {
            MainWindow newWindow = new MainWindow();
            newWindow.Show();
            this.Close();
        }
    }
}
