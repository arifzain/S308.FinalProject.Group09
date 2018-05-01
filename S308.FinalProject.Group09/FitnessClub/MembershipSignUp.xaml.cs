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
    /// Interaction logic for MembershipSignUp.xaml
    /// </summary>
    public partial class MembershipSignUp : Window
    {
        public SignUp InfoFromPrevWindow { get; set; }
        List<Members> membersList;
        public MembershipSignUp()
        {
            InitializeComponent();
        
        }

        public MembershipSignUp(SignUp info)
        {
            InitializeComponent();

            //assigning the property from the member info class that was passed into this overridden constructor
            InfoFromPrevWindow = info;

            InfoFromPrevWindow = new SignUp();

            membersList = new List<Members>();

            string strFilePath = @"../../../Data/members.json";
            try
            {
                StreamReader reader = new StreamReader(strFilePath);
                string jsonData = reader.ReadToEnd();
                reader.Close();

                membersList = JsonConvert.DeserializeObject<List<Members>>(jsonData);
            }

            catch (Exception ex)
            {
                MessageBox.Show("Error in import process: " + ex.Message);
            }

        }



        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            MembershipQuote newWindow = new MembershipQuote();
            newWindow.Show();
            this.Close();
        }

        private void btnMainMenu_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            this.Close();
        }





        private void btnSubmit_Click(object sender, RoutedEventArgs e)
        {
            double dblPhone;

            string strFName, strLName, strPhone, strEmail, strGender, strAge, strWeight, strPersonalGoal;

            int intAt, intPeriod, intEmailLength;

            //For data validation
            strFName = txtFirstName.Text.Trim();
            strLName = txtLastName.Text.Trim();
            strPhone = txtPhone.Text.Trim();
            strEmail = txtEmail.Text.Trim();

            //For the email address validation
            intAt = strEmail.IndexOf("@");
            intPeriod = strEmail.IndexOf(".");
            intEmailLength = strEmail.Length;

            //validate first name
            if (strFName == "")
            {
                MessageBox.Show("Please enter a first name.");
                return;
            }

            //validate last name
            if (strLName == "")
            {
                MessageBox.Show("Please enter a last name.");
                return;
            }

            #region Email Validation
            if (strEmail == "")
            {
                MessageBox.Show("Please enter an email address.");
                return;
            }
            if (strEmail != "" && (!strEmail.Contains("@") || !strEmail.Contains(".")))
            {
                MessageBox.Show("Valid email address format needs to be entered.");
                return;
            }

            if (strEmail != "" && ((intAt < 1) || ((intPeriod - intAt) < 2) || ((intEmailLength - 1 - intPeriod) < 2)))
            {
                MessageBox.Show("Valid email address format needs to be entered.");
                return;
            }
            #endregion

            #region Phone Validation
            if (!double.TryParse(txtPhone.Text, out dblPhone))
            {
                MessageBox.Show("Please enter valid numbers.");
                return;
            }

            if (strPhone == "")
            {
                MessageBox.Show("Please do not leave phone number field empty.");
                return;
            }

            strPhone = Convert.ToString(dblPhone);

            if (strPhone.Length != 10)
            {
                MessageBox.Show("Phone number has to be 10 digits long.");
                return;
            }

            #endregion

            #region CC Validation


            //Naming the variables
            string strCardNum = txtCCNumber.Text.Trim().Replace(" ", "");
            long lngOut;
            bool bolValid = false;
            int i;
            int intCheckDigit;
            int intCheckSum = 0;
            string strCardType;

            //Making sure the CC input is numbers
            txtCCNumber.Background = new SolidColorBrush(Color.FromRgb(255, 255, 255));

            if (!Int64.TryParse(strCardNum, out lngOut))
            {
                MessageBox.Show("Credit card numbers contain only numbers.");
                txtCCNumber.Background = new SolidColorBrush(Color.FromRgb(255, 200, 200));
                return;
            }

            //Making sure the CC input is the right amount of numbers
            if (strCardNum.Length != 13 && strCardNum.Length != 15 && strCardNum.Length != 16)
            {
                MessageBox.Show("Credit card numbers must contain 13, 15, or 16 digits.");
                txtCCNumber.Background = new SolidColorBrush(Color.FromRgb(255, 200, 200));
                return;
            }

            //Matching the CC input to the CC type
            if (strCardNum.StartsWith("34") || strCardNum.StartsWith("37"))
                strCardType = "AMEX";
            else if (strCardNum.StartsWith("6011"))
                strCardType = "Discover";
            else if (strCardNum.StartsWith("51") || strCardNum.StartsWith("52") || strCardNum.StartsWith("53") || strCardNum.StartsWith("54") || strCardNum.StartsWith("55"))
                strCardType = "MasterCard";
            else if (strCardNum.StartsWith("4"))
                strCardType = "VISA";
            else
                strCardType = "Unknown Card Type";

            //Validating the CC # based on the formula
            strCardNum = ReverseString(strCardNum);

            for (i = 0; i < strCardNum.Length; i++)
            {
                intCheckDigit = Convert.ToInt32(strCardNum.Substring(i, 1));

                if ((i + 1) % 2 == 0)
                {
                    intCheckDigit *= 2;

                    if (intCheckDigit > 9)
                    {
                        intCheckDigit -= 9;
                    }
                }

                intCheckSum += intCheckDigit;
            }

            if (intCheckSum % 10 == 0)
            {
                bolValid = true;
            }

            //Changing the cboCCType selection based on the card type
            if (bolValid)
            {
                switch (strCardType)
                {
                    case "AMEX":
                        cboCCType.SelectedIndex = 2;
                        break;
                    case "Discover":
                        cboCCType.SelectedIndex = 3;
                        break;
                    case "MasterCard":
                        cboCCType.SelectedIndex = 1;
                        break;
                    case "VISA":
                        cboCCType.SelectedIndex = 0;
                        break;
                }

                txtCCNumber.Background = new SolidColorBrush(Color.FromRgb(255, 255, 255));
            }
            else
            {
                txtCCNumber.Background = new SolidColorBrush(Color.FromRgb(255, 200, 200));
                MessageBox.Show("Please enter in a valid Credit Card Number.");
                txtCCNumber.Text = "";
                return;
            }
            #endregion

            strGender = cboGender.Text;

            strPersonalGoal = cboGoals.Text;



            double dblAge, dblWeight;

            strAge = txtAge.Text;
            dblAge = Convert.ToDouble(strAge);

            if (strAge == "" && !double.TryParse(strAge, out dblAge))
            {
                MessageBox.Show("Please enter a valid number for age.");
                return;
            }

            strAge = dblAge.ToString();

            strWeight = txtWeight.Text;
            dblWeight = Convert.ToDouble(strWeight);

            if (strWeight == "" && !double.TryParse(strWeight, out dblWeight))
            {
                MessageBox.Show("Please enter a valid number for weight.");
                return;
            }

            strWeight = dblWeight.ToString();

            string strMembershipType, strStartDate, strEndDate, strMembershipCost, strSubtotal, strPersonalTraining, strLockerRental, strTotal;

            strMembershipType = InfoFromPrevWindow.MembershipType;
            strStartDate = InfoFromPrevWindow.StartDate;
            strEndDate = InfoFromPrevWindow.EndDate;
            strMembershipCost = InfoFromPrevWindow.MembershipCost;
            strSubtotal = InfoFromPrevWindow.Subtotal;
            strPersonalTraining = InfoFromPrevWindow.PersonalTraining;
            strLockerRental = InfoFromPrevWindow.LockerRental;
            strTotal = InfoFromPrevWindow.Total;
            


            Members membersNew = new Members(strFName, strLName, strCardType, strCardNum, strPhone, strEmail, strGender, strMembershipType, strStartDate, strEndDate, strMembershipCost, strPersonalTraining, strLockerRental, strTotal, strAge, strWeight, strPersonalGoal);

            //show data extracted from text boxes and combo box in a message box as a way to preview data before adding to list and exporting to the json file
            //user has the option (yes/no) to continue with the data submission process
            MessageBoxResult messageBoxResult = MessageBox.Show("Do you want to add the member below?"
                + Environment.NewLine
                + Environment.NewLine
                , "Add new member?"
                , MessageBoxButton.YesNo);

            //if statement to respond to the above (yes/no) selection
            if (messageBoxResult == MessageBoxResult.Yes)
            {
                //if yes, the new customer created is added to list
                membersList.Add(membersNew);

                //new list is exported to replace the old one by calling upon a method
                ExportToFile(membersNew);



            }
        }



        private void ExportToFile(Members membersNew)
        {
            string strFilePath = @"../../../Data/Members.json";

            try
            {
                StreamWriter writer = new StreamWriter(strFilePath, false);
                string jsonData = JsonConvert.SerializeObject(membersList);
                writer.Write(jsonData);
                writer.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error saving the new customer: " + ex.Message);
            }

            MessageBox.Show("Customer saved." + Environment.NewLine + "File Created: " + strFilePath);
        }
        public static string ReverseString(string s)
        {
            char[] array = s.ToCharArray();
            Array.Reverse(array);
            return new string(array);
        }
    }
}