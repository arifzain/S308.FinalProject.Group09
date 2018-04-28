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
    public partial class MembershipQuote : Window
    {
        public Quote InfoFromPrevWindow { get; set; }

        List<Pricing> priceList;
        public MembershipQuote()
        {
            InitializeComponent();

            InfoFromPrevWindow = new Quote();

            txtMembershipType.Text = InfoFromPrevWindow.MembershipType;

            priceList = new List<Pricing>();
            string strFilePath = GetFilePath("json");

            try
            {
                StreamReader reader = new StreamReader(strFilePath);
                string jsonData = reader.ReadToEnd();
                reader.Close();

                priceList = JsonConvert.DeserializeObject<List<Pricing>>(jsonData);
            }

            catch (Exception ex)
            {
                MessageBox.Show("Error in import process: " + ex.Message);
            }

            //show message when the application is opened
            MessageBox.Show("Price data successfully imported.");

            Pricing pricingNew = new Pricing();
        }

        public MembershipQuote(Quote info)
        {
            //don't forget this line when overriding the constructor for a window
            InitializeComponent();

            //assigning the property from the member info class that was passed into this overridden constructor
            InfoFromPrevWindow = info;

            Output();    
        }

        private void Output()
        {

            string strStartDate, strEndDate;
                
            strStartDate = InfoFromPrevWindow.StartDate;
            DateTime datStartDate, datEndDate; 
            datStartDate = DateTime.Parse(strStartDate);
            datEndDate = datStartDate.AddYears(1);

            strEndDate = String.Format("{0:MM/dd/yyyy}", datEndDate);




            txtMembershipType.Text = InfoFromPrevWindow.MembershipType;
            txtStartDate.Text = InfoFromPrevWindow.StartDate;
            txtEndDate.Text = strEndDate; strSelectedDate = String.Format("{0:MM/dd/yyyy}", datStartDate);
            txtPersonalTrainingPlan.Text = InfoFromPrevWindow.PersonalTraining;
            txtLockerRental.Text = InfoFromPrevWindow.LockerRental; 


        }

        private string GetFilePath(string extension)
        {
            string strFilePath = @"..\..\..\Data\Pricing";
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

        private void btnSignUp_Click(object sender, RoutedEventArgs e)
        {
            MembershipSignUp newWindow = new MembershipSignUp();
            newWindow.Show();
            this.Close();
        }
    }
}
