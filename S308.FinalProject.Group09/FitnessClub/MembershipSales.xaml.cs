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
    /// Interaction logic for MembershipSales.xaml
    /// </summary>
    public partial class MembershipSales : Window
    {
        public MembershipSales()
        {
            InitializeComponent();

            string strFilePath = GetFilePath("txt");

            try
            {
                StreamReader reader = new StreamReader(strFilePath);
                string price = reader.ReadLine();

                while (price != null)
                {
                    cboMembershipType.Items.Add(price);
                    price = reader.ReadLine();
                }
            }

            catch (Exception ex)
            {
                MessageBox.Show("Error in import process: " + ex.Message);
            }

            //show message when the application is opened
            MessageBox.Show("Price data successfully imported.");

         

            Pricing pricingNew = new Pricing();
        }

        private void btnQuote_Click(object sender, RoutedEventArgs e)
        {
            string strMembershipType, strSelectedDate, strPersonalTraining, strLockerRental;
            bool? bolPersonalTraining, bolLockerRental;

            bolPersonalTraining = chbPersonalTrainingPlan.IsChecked;
            bolLockerRental = chbLockerRental.IsChecked;

            DateTime datToday = DateTime.Today;

            //validate customer type selected
            if (cboMembershipType.SelectedIndex == -1)
            {
                MessageBox.Show("Please select a membership type.");
                return;
            }

            strMembershipType = cboMembershipType.Text.ToString();


            //validate date

            dtpStartDate.DisplayDateStart = datToday;

            DateTime? datStartDate = dtpStartDate.SelectedDate;

            if (datStartDate < datToday)
            {
                MessageBox.Show("Please select a date that is not in the past");
                return;
            }

            strSelectedDate = String.Format("{0:MM/dd/yyyy}", datStartDate);



            if (bolPersonalTraining == true)
            {
                strPersonalTraining = "Yes";
            }
            else if(bolPersonalTraining == false)
            {
                strPersonalTraining = "No";
            }
            else
            {
                strPersonalTraining = "";
            }

            if (bolLockerRental == true)
            {
                strLockerRental = "Yes";
            }
            else if (bolLockerRental == false)
            {
                strLockerRental = "No";
            }
            else
            {
                strLockerRental = "";
            }
            



            Quote quote = new Quote(strMembershipType, strSelectedDate, strPersonalTraining, strLockerRental);

            MembershipQuote memberQuote = new MembershipQuote(quote);

            memberQuote.Show();

            this.Close();
            
            
            

        }


        private string GetFilePath(string extension)
        {
            string strFilePath = @"..\..\..\..\Data\Pricing";
            string strTimestamp = DateTime.Now.Ticks.ToString();

            strFilePath += "." + extension;

            return strFilePath;
        }

    }
}