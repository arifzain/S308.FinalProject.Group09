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

        public MembershipQuote()
        {
            InitializeComponent();

            InfoFromPrevWindow = new Quote();

            txtMembershipType.Text = InfoFromPrevWindow.MembershipType;

        }

        public MembershipQuote(Quote info)
        {
            //don't forget this line when overriding the constructor for a window
            InitializeComponent();

            //assigning the property from the member info class that was passed into this overridden constructor
            InfoFromPrevWindow = info;

            string strStartDate, strEndDate, strMembershipType, strMembershipTypeTrim, strMonthlyCost, strSubtotal;
            double dblMonthlyCost, dblSubtotal;

            strMembershipTypeTrim = InfoFromPrevWindow.MembershipType.ToString().Trim();
            strMembershipType = InfoFromPrevWindow.MembershipType.ToString();

            strMembershipTypeTrim = strMembershipTypeTrim.Substring(0, strMembershipTypeTrim.IndexOf(":"));
            strMonthlyCost = strMembershipType.Substring(strMembershipType.IndexOf(":") + 1).Trim();

            dblMonthlyCost = Convert.ToDouble(strMonthlyCost);

            strMonthlyCost = dblMonthlyCost.ToString("C2");

            strStartDate = InfoFromPrevWindow.StartDate;
            DateTime datStartDate;

            datStartDate = DateTime.Parse(strStartDate);
            dblSubtotal = 0;
        
            if(strMembershipTypeTrim == "Individual 1 Month")
            {
                datStartDate = datStartDate.AddMonths(1);
                dblSubtotal = dblMonthlyCost;
            }
            else if (strMembershipTypeTrim == "Individual 12 Month")
            {
                datStartDate = datStartDate.AddYears(1);
                dblSubtotal = dblMonthlyCost*12;
            }
            else if (strMembershipTypeTrim == "Two Person 1 Month")
            {
                datStartDate = datStartDate.AddMonths(1);
                dblSubtotal = dblMonthlyCost;
            }
            else if (strMembershipTypeTrim == "Two Person 12 Month")
            {
                datStartDate = datStartDate.AddYears(1);
                dblSubtotal = dblMonthlyCost*12;
            }
            else if (strMembershipTypeTrim == "Family 1 Month")
            {
                datStartDate = datStartDate.AddMonths(1);
                dblSubtotal = dblMonthlyCost;
            }
            else if (strMembershipTypeTrim == "Family 12 Month")
            {
                datStartDate = datStartDate.AddYears(1);
                dblSubtotal = dblMonthlyCost*12;
            }
            
            strSubtotal = dblSubtotal.ToString("C2");

            strEndDate = string.Format("{0:MM/dd/yyyy}", datStartDate);

            string strTraining, strLocker, strTotal;
            double dblTraining, dblLocker, dblTotal;

            strTraining = InfoFromPrevWindow.PersonalTraining;
            strLocker = InfoFromPrevWindow.LockerRental;

            dblTraining = 5;
            dblLocker = 1;
            dblTotal = 0;

            if (strMembershipTypeTrim == "Individual 1 Month" && strTraining == "Yes" && strLocker == "Yes")
            {
                dblTotal = dblSubtotal + (1 * dblTraining) + (1 * dblLocker);
            }
            else if (strMembershipTypeTrim == "Individual 1 Month" && strTraining == "Yes" && strLocker == "No")
            {
                dblTotal = dblSubtotal + (1 * dblTraining);
            }
            else if (strMembershipTypeTrim == "Individual 1 Month" && strTraining == "No" && strLocker == "Yes")
            {
                dblTotal = dblSubtotal + (1 * dblLocker);
            }

            else if (strMembershipTypeTrim == "Individual 12 Month" && strTraining == "Yes" && strLocker == "Yes")
            {
                dblTotal = dblSubtotal + (12 * dblTraining) + (12 * dblLocker);
            }
            else if (strMembershipTypeTrim == "Individual 12 Month" && strTraining == "Yes" && strLocker == "No")
            {
                dblTotal = dblSubtotal + (12 * dblTraining);
            }
            else if (strMembershipTypeTrim == "Individual 12 Month" && strTraining == "No" && strLocker == "Yes")
            {
                dblTotal = dblSubtotal + (12 * dblLocker);
            }


            else if (strMembershipTypeTrim == "Two Person 1 Month" && strTraining == "Yes" && strLocker == "Yes")
            {
                dblTotal = dblSubtotal + (1 * dblTraining) + (1 * dblLocker);
            }
            else if (strMembershipTypeTrim == "Two Person 1 Month" && strTraining == "Yes" && strLocker == "No")
            {
                dblTotal = dblSubtotal + (1 * dblTraining);
            }
            else if (strMembershipTypeTrim == "Two Person 1 Month" && strTraining == "No" && strLocker == "Yes")
            {
                dblTotal = dblSubtotal + (1 * dblLocker);
            }

            else if (strMembershipTypeTrim == "Two Person 12 Month" && strTraining == "Yes" && strLocker == "Yes")
            {
                dblTotal = dblSubtotal + (12 * dblTraining) + (12 * dblLocker);
            }
            else if (strMembershipTypeTrim == "Two Person 12 Month" && strTraining == "Yes" && strLocker == "No")
            {
                dblTotal = dblSubtotal + (12 * dblTraining);
            }
            else if (strMembershipTypeTrim == "Two Person 12 Month" && strTraining == "No" && strLocker == "Yes")
            {
                dblTotal = dblSubtotal + (12 * dblLocker);
            }


            else if (strMembershipTypeTrim == "Family 1 Month" && strTraining == "Yes" && strLocker == "Yes")
            {
                dblTotal = dblSubtotal + (1 * dblTraining) + (1 * dblLocker);
            }
            else if (strMembershipTypeTrim == "Family 1 Month" && strTraining == "Yes" && strLocker == "No")
            {
                dblTotal = dblSubtotal + (1 * dblTraining);
            }
            else if (strMembershipTypeTrim == "Family 1 Month" && strTraining == "No" && strLocker == "Yes")
            {
                dblTotal = dblSubtotal + (1 * dblLocker);
            }

            else if (strMembershipTypeTrim == "Family 12 Month" && strTraining == "Yes" && strLocker == "Yes")
            {
                dblTotal = dblSubtotal + (12 * dblTraining) + (12 * dblLocker);
            }
            else if (strMembershipTypeTrim == "Family 12 Month" && strTraining == "Yes" && strLocker == "No")
            {
                dblTotal = dblSubtotal + (12 * dblTraining);
            }
            else if (strMembershipTypeTrim == "Family 12 Month" && strTraining == "No" && strLocker == "Yes")
            {
                dblTotal = dblSubtotal + (12 * dblLocker);
            }

            else
            {
                dblTotal = dblSubtotal;
            }

            strTotal = dblTotal.ToString("C2");


            txtMembershipType.Text = strMembershipTypeTrim;
            txtStartDate.Text = InfoFromPrevWindow.StartDate;
            txtEndDate.Text = strEndDate;
            txtPersonalTrainingPlan.Text = strTraining;
            txtLockerRental.Text = strLocker;
            txtMembershipCost.Text = strMonthlyCost;
            txtSubtotal.Text = strSubtotal;
            txtTotal.Text = strTotal;
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
