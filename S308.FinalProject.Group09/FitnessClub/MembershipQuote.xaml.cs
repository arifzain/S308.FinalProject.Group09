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
        //default quote for default constructor
        public Quote InfoFromPrevWindow { get; set; }

        public MembershipQuote()
        {
            InitializeComponent();


            InfoFromPrevWindow = new Quote();

            txtMembershipType.Text = InfoFromPrevWindow.MembershipType;

        }

        public MembershipQuote(Quote info)
        {

            InitializeComponent();

            //assigning the property from the member info class that was passed into this overridden constructor
            InfoFromPrevWindow = info;

            //declare variables
            string strStartDate, strEndDate, strMembershipType, strMembershipTypeTrim, strCost, strMonthlyCost, strSubtotal;
            double dblCost, dblMonthlyCost, dblSubtotal;

            //get raw membership type information from previous window
            strMembershipTypeTrim = InfoFromPrevWindow.MembershipType.ToString().Trim();
            strMembershipType = InfoFromPrevWindow.MembershipType.ToString();

            //extract the relevant substrings from the imported string
            //membership type
            strMembershipTypeTrim = strMembershipTypeTrim.Substring(0, strMembershipTypeTrim.IndexOf(":"));
            //monthly cost
            strCost = strMembershipType.Substring(strMembershipType.IndexOf(":") + 1).Trim();

            //convert monthly cost from previous window to double
            dblCost = Convert.ToDouble(strCost);

            //format monthly cost to currency
            strCost = dblCost.ToString("C2");

            //get raw start date info from previous window
            strStartDate = InfoFromPrevWindow.StartDate;

            //decalre DateTime variable
            DateTime datStartDate;

            //parse string start date data into DateTime format
            datStartDate = DateTime.Parse(strStartDate);

            dblSubtotal = 0;
            dblMonthlyCost = 0;

            //if statements used to return end dates, monthly cost and subtotals based on the duration of the member
            if (strMembershipTypeTrim == "Individual 1 Month")
            {
                datStartDate = datStartDate.AddMonths(1);
                dblMonthlyCost = dblCost;
                dblSubtotal = dblCost;
            }
            else if (strMembershipTypeTrim == "Individual 12 Month")
            {
                datStartDate = datStartDate.AddYears(1);
                dblMonthlyCost = dblCost/12;
                dblSubtotal = dblCost;
            }
            else if (strMembershipTypeTrim == "Two Person 1 Month")
            {
                datStartDate = datStartDate.AddMonths(1);
                dblMonthlyCost = dblCost;
                dblSubtotal = dblCost;
            }
            else if (strMembershipTypeTrim == "Two Person 12 Month")
            {
                datStartDate = datStartDate.AddYears(1);
                dblMonthlyCost = dblCost / 12;
                dblSubtotal = dblCost;
            }
            else if (strMembershipTypeTrim == "Family 1 Month")
            {
                datStartDate = datStartDate.AddMonths(1);
                dblMonthlyCost = dblCost;
                dblSubtotal = dblCost;
            }
            else if (strMembershipTypeTrim == "Family 12 Month")
            {
                datStartDate = datStartDate.AddYears(1);
                dblMonthlyCost = dblCost / 12;
                dblSubtotal = dblCost;
            }
            
            //convert subtotal and monthly cost to string
            strSubtotal = dblSubtotal.ToString("C2");
            strMonthlyCost = dblMonthlyCost.ToString("C2");

            //format end date
            strEndDate = string.Format("{0:MM/dd/yyyy}", datStartDate);

            string strTraining, strLocker, strTotal;
            double dblTraining, dblLocker, dblTotal;

            //fetch training and locker info from previous window
            strTraining = InfoFromPrevWindow.PersonalTraining;
            strLocker = InfoFromPrevWindow.LockerRental;

            //assign values to varaibles
            dblTraining = 5;
            dblLocker = 1;
            dblTotal = 0;

            //if statements were used to determine how much it would cost when training and locker costs are factored in
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

            //convert total to string
            strTotal = dblTotal.ToString("C2");

            //display output
            txtMembershipType.Text = strMembershipTypeTrim;
            txtStartDate.Text = InfoFromPrevWindow.StartDate;
            txtEndDate.Text = strEndDate;
            txtPersonalTrainingPlan.Text = strTraining;
            txtLockerRental.Text = strLocker;
            txtMembershipCost.Text = strMonthlyCost;
            txtSubtotal.Text = strSubtotal;
            txtTotal.Text = strTotal;

            //data to be sent to MembershipSignUp
            SignUp signup = new SignUp(strMembershipTypeTrim, strStartDate, strEndDate, strMonthlyCost, strSubtotal, strTraining, strLocker, strTotal);

            //instantiate the next window and use the overridden constructor that allows sending information in as an argument
            MembershipSignUp memberSignUp = new MembershipSignUp(signup);
            




        }


        private void btnMainMenu_Click(object sender, RoutedEventArgs e)
        {
            MainWindow newWindow = new MainWindow();
            newWindow.Show();
            this.Close();
        }

        private void btnSignUp_Click(object sender, RoutedEventArgs e)
        {


            InitializeComponent();

            //virtually identical to above code to ensure data is transferred over to next window

            //declare variables
            string strStartDate, strEndDate, strMembershipType, strMembershipTypeTrim, strCost, strMonthlyCost, strSubtotal;
            double dblCost, dblMonthlyCost, dblSubtotal;

            //get raw membership type information from previous window
            strMembershipTypeTrim = InfoFromPrevWindow.MembershipType.ToString().Trim();
            strMembershipType = InfoFromPrevWindow.MembershipType.ToString();

            //extract the relevant substrings from the imported string
            //membership type
            strMembershipTypeTrim = strMembershipTypeTrim.Substring(0, strMembershipTypeTrim.IndexOf(":"));
            //monthly cost
            strCost = strMembershipType.Substring(strMembershipType.IndexOf(":") + 1).Trim();

            //convert monthly cost from previous window to double
            dblCost = Convert.ToDouble(strCost);

            //format monthly cost to currency
            strCost = dblCost.ToString("C2");

            //get raw start date info from previous window
            strStartDate = InfoFromPrevWindow.StartDate;

            //decalre DateTime variable
            DateTime datStartDate;

            //parse string start date data into DateTime format
            datStartDate = DateTime.Parse(strStartDate);

            dblSubtotal = 0;
            dblMonthlyCost = 0;

            //if statements used to return end dates, monthly cost and subtotals based on the duration of the member
            if (strMembershipTypeTrim == "Individual 1 Month")
            {
                datStartDate = datStartDate.AddMonths(1);
                dblMonthlyCost = dblCost;
                dblSubtotal = dblCost;
            }
            else if (strMembershipTypeTrim == "Individual 12 Month")
            {
                datStartDate = datStartDate.AddYears(1);
                dblMonthlyCost = dblCost / 12;
                dblSubtotal = dblCost;
            }
            else if (strMembershipTypeTrim == "Two Person 1 Month")
            {
                datStartDate = datStartDate.AddMonths(1);
                dblMonthlyCost = dblCost;
                dblSubtotal = dblCost;
            }
            else if (strMembershipTypeTrim == "Two Person 12 Month")
            {
                datStartDate = datStartDate.AddYears(1);
                dblMonthlyCost = dblCost / 12;
                dblSubtotal = dblCost;
            }
            else if (strMembershipTypeTrim == "Family 1 Month")
            {
                datStartDate = datStartDate.AddMonths(1);
                dblMonthlyCost = dblCost;
                dblSubtotal = dblCost;
            }
            else if (strMembershipTypeTrim == "Family 12 Month")
            {
                datStartDate = datStartDate.AddYears(1);
                dblMonthlyCost = dblCost / 12;
                dblSubtotal = dblCost;
            }

            //convert subtotal and monthly cost to string
            strSubtotal = dblSubtotal.ToString("C2");
            strMonthlyCost = dblMonthlyCost.ToString("C2");

            //format end date
            strEndDate = string.Format("{0:MM/dd/yyyy}", datStartDate);

            string strTraining, strLocker, strTotal;
            double dblTraining, dblLocker, dblTotal;

            //fetch training and locker info from previous window
            strTraining = InfoFromPrevWindow.PersonalTraining;
            strLocker = InfoFromPrevWindow.LockerRental;

            //assign values to varaibles
            dblTraining = 5;
            dblLocker = 1;
            dblTotal = 0;

            //if statements were used to determine how much it would cost when training and locker costs are factored in
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

            //convert total to string
            strTotal = dblTotal.ToString("C2");

            //data to be sent to MembershipSignUp
            SignUp signup = new SignUp(strMembershipTypeTrim, strStartDate, strEndDate, strMonthlyCost, strSubtotal, strTraining, strLocker, strTotal);

            //instantiate the next window and use the overridden constructor that allows sending information in as an argument
            MembershipSignUp memberSignUp = new MembershipSignUp(signup);


            //open MEmbershipSignUp window
            memberSignUp.Show();

            //close this window
            this.Close();
            

        }

    }
}
