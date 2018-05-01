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

            //default blank quote
            InfoFromPrevWindow = new SignUp();

            //new member list
            membersList = new List<Members>();

        }

        public MembershipSignUp(SignUp info)
        {
            InitializeComponent();

            //assigning the property from the member info class that was passed into this overridden constructor
            InfoFromPrevWindow = info;

            //declare variables for data that will be transferred over from previous window
            string strMembershipType, strStartDate, strEndDate, strMembershipCost, strSubtotal, strPersonalTraining, strLockerRental, strTotal;

            //assign info from previous window their respective varaibles
            strMembershipType = InfoFromPrevWindow.MembershipType.ToString();
            strStartDate = InfoFromPrevWindow.StartDate.ToString();
            strEndDate = InfoFromPrevWindow.EndDate.ToString();
            strMembershipCost = InfoFromPrevWindow.MembershipCost.ToString();
            strSubtotal = InfoFromPrevWindow.Subtotal.ToString();
            strPersonalTraining = InfoFromPrevWindow.PersonalTraining.ToString();
            strLockerRental = InfoFromPrevWindow.LockerRental.ToString();
            strTotal = InfoFromPrevWindow.Total.ToString();

            }
    
        private void btnSubmit_Click(object sender, RoutedEventArgs e)
        {
            
            //read json file in preperation for write operation
            string strFilePath = GetFilePath();
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

            //declare variables and assign them to info from previous window again
            //this was the only way we could make the information from the previous window transfer over to the json file
            string strMembershipType, strStartDate, strEndDate, strMembershipCost, strSubtotal, strPersonalTraining, strLockerRental, strTotal;

            strMembershipType = InfoFromPrevWindow.MembershipType.ToString();
            strStartDate = InfoFromPrevWindow.StartDate.ToString();
            strEndDate = InfoFromPrevWindow.EndDate.ToString();
            strMembershipCost = InfoFromPrevWindow.MembershipCost.ToString();
            strSubtotal = InfoFromPrevWindow.Subtotal.ToString();
            strPersonalTraining = InfoFromPrevWindow.PersonalTraining.ToString();
            strLockerRental = InfoFromPrevWindow.LockerRental.ToString();
            strTotal = InfoFromPrevWindow.Total.ToString();

            //declare variables for data entered in this window
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


            //validate the email
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

            //validate the phone #
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

            //validate the cc #
            //1. Declare a variables
            //   - credit card number from the text box and assign (remove spaces)
            //   - counter for loop
            //   - check digit (to hold each digit while working with them)
            //   - check sum (to hold the sum of the digits once modified)
            //   - valid (boolean)
            //   - card type
            //2. Make sure the text entered is numeric
            //       a. message to user that says to enter only numbers
            //       b. show negative result
            //3. Make sure there are 13, 15, 16 digits entered
            //       a. message to the user about the number of digits
            //       b. show negative result
            //4. Determine the card type from the prefix and set the card type variable
            //5. Validate card number
            //       a. reverse all of the characters in the credit card number
            //       b. loop through the characters
            //           - if it is the first, third, fifth, etc digit add it to the check sum
            //           - if it is the second, fourth, sixth, etc digit double before adding to the check sum
            //                   - if after double the digit it is > 9 then add the two numbers before adding to the check sum
            //                   - 12 = 1 + 2 or x - 9
            //       c. if the result is divisible by 10 the card number is a valid number. Set the valid variable
            //6. Show the appropriate result
            //       'a. if valid
            //           - change the card type to the correct type
            //       b. else
            //           - set the text of the result label to Credit Card Is Not Valid
            //           - set the text color to red

            //1.
            string strCardNum = txtCCNumber.Text.Trim().Replace(" ", "");
            long lngOut;
            bool bolValid = false;
            int i;
            int intCheckDigit;
            int intCheckSum = 0;
            string strCardType;

            //2.
            txtCCNumber.Background = new SolidColorBrush(Color.FromRgb(255, 255, 255));

            if (!Int64.TryParse(strCardNum, out lngOut))
            {
                MessageBox.Show("Credit card numbers contain only numbers.");
                txtCCNumber.Background = new SolidColorBrush(Color.FromRgb(255, 200, 200));
                return;
            }

            //3.
            if (strCardNum.Length != 13 && strCardNum.Length != 15 && strCardNum.Length != 16)
            {
                MessageBox.Show("Credit card numbers must contain 13, 15, or 16 digits.");
                txtCCNumber.Background = new SolidColorBrush(Color.FromRgb(255, 200, 200));
                return;
            }

            //4.
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

            //5.
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

            //6.
            if (bolValid)
            {
                switch (strCardType)
                {
                    case "AMEX":

                        break;
                    case "Discover":

                        break;
                    case "MasterCard":

                        break;
                    case "VISA":

                        break;
                }

                txtCCNumber.Background = new SolidColorBrush(Color.FromRgb(255, 255, 255));
            }
            else
            {
                txtCCNumber.Background = new SolidColorBrush(Color.FromRgb(255, 200, 200));
            }

            //get gender and goals data from combo boxes
            strGender = cboGender.Text;
            strPersonalGoal = cboGoals.Text;

            //declare variables
            double dblAge, dblWeight;

            //give varaiables values
            strAge = txtAge.Text;
            dblAge = Convert.ToDouble(strAge);

            //validate age if not empty
            if (strAge == "" && !double.TryParse(strAge, out dblAge))
            {
                MessageBox.Show("Please enter a valid number for age.");
                return;
            }

            strAge = dblAge.ToString();

            strWeight = txtWeight.Text;
            dblWeight = Convert.ToDouble(strWeight);

            //validate weight if not empty
            if (strWeight == "" && !double.TryParse(strWeight, out dblWeight))
            {
                MessageBox.Show("Please enter a valid number for weight.");
                return;
            }

            strWeight = dblWeight.ToString();


            //create a new members list
            Members membersNew = new Members(strFName, strLName, strCardType, strCardNum, strPhone, strEmail, strGender, strMembershipType, strStartDate, strEndDate, strMembershipCost, strPersonalTraining, strLockerRental, strTotal, strAge, strWeight, strPersonalGoal);

            //prompt user to save data
            MessageBoxResult messageBoxResult = MessageBox.Show("Do you want to add a new member?"
                + Environment.NewLine
                , "Confirm?"
                , MessageBoxButton.YesNo);

            //if statement to respond to the above (yes/no) selection
            if (messageBoxResult == MessageBoxResult.Yes)
            {
                //if yes, the new member created is added to list
                membersList.Add(membersNew);

                //new list is exported to replace the old one by calling upon a method
                ExportToFile(membersNew);

            }
        }

        //method to get file path
        private string GetFilePath()
        {
            string strFilePath = @"../../../Data/Members.json";

            return strFilePath;
        }

        //method to export json file
        private void ExportToFile(Members membersNew)
        {
            string strFilePath = GetFilePath();

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
        //method to reverse string
        public static string ReverseString(string s)
        {
            char[] array = s.ToCharArray();
            Array.Reverse(array);
            return new string(array);
        }
        //go back one window
        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            MembershipQuote newWindow = new MembershipQuote();
            newWindow.Show();
            this.Close();
        }

        //go back to main menu
        private void btnMainMenu_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            this.Close();
        }
    }

}
