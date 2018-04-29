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
    /// Interaction logic for MemberInformation.xaml
    /// </summary>
    public partial class MemberInformation : Window

    {
        List<Members> memberList;
        public MemberInformation()
        {
            InitializeComponent();

            memberList = new List<Members>();

            dtgMembersOutput.ItemsSource = memberList;

            //import data from json file and deserialize
            string strFilePath = GetFilePath("json");

            try
            {
                StreamReader reader = new StreamReader(strFilePath);
                string jsonData = reader.ReadToEnd();
                reader.Close();

                memberList = JsonConvert.DeserializeObject<List<Members>>(jsonData);

                dtgMembersOutput.ItemsSource = memberList;
            }

            catch (Exception ex)
            {
                MessageBox.Show("Error in import process: " + ex.Message);
            }

            dtgMembersOutput.Items.Refresh();

            //show message when the application is opened
            MessageBox.Show("Customer data successfully imported.");

        }

        private void btnMain_Click(object sender, RoutedEventArgs e)
        {
            MainWindow newWindow = new MainWindow();
            newWindow.Show();
            this.Close();
        }

        private void btnFind_Click(object sender, RoutedEventArgs e)
        {
            

            double dblPhone;

            string strLName, strPhone, strEmail;

            int intAt, intPeriod, intEmailLength;

            //For data validation
            strLName = txtLastName.Text;
            strPhone = txtPhone.Text;
            strEmail = txtEmail.Text;

            //For the email address validation
            intAt = strEmail.IndexOf("@");
            intPeriod = strEmail.IndexOf(".");
            intEmailLength = strEmail.Length;

            // last name validation to not be empty
            if (strLName == "")
            {
                MessageBox.Show("Please enter a last name.");
                return;
            }

            // phone number validation so it contains all numbers and is not empty

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

            // validation so that phone number is 10 digits long

            strPhone = Convert.ToString(dblPhone);

            if (strPhone.Length != 10)
            {
                MessageBox.Show("Phone number has to be 10 digits long.");
                return;
            }

            // Since an email address needs to contain an 'at mark' after the username and before the domain
            // And a period after the at mark and before the domain, the email address can be validated using the indexing method

            // Make sure email is a valid email address with an at mark after the username and a period before the domain

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
        }

        private string GetFilePath(string extension)
        {
            string strFilePath = @"..\..\..\..\Data\Members";
            string strTimestamp = DateTime.Now.Ticks.ToString();

            strFilePath += "." + extension;

            return strFilePath;
        }

        //clear button function
        private void btnClear_Click(object sender, RoutedEventArgs e)
        {
            txtLastName.Text = "";
            txtEmail.Text = "";
            txtPhone.Text = "";

            //clearing the record search results as well
        }
    }
}
