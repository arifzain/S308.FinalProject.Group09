﻿using System;
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
        List<Members> membersIndex;
        public MemberInformation()
        {
            InitializeComponent();

            membersIndex = GetDataSetFromFile();
        }

        public List<Members> GetDataSetFromFile()
        {
            List<Members> lstMembers = new List<Members>();

            string strFilePath = @"../../../../Data/Members.json";

            try
            {
                string jsonData = File.ReadAllText(strFilePath);
                lstMembers = JsonConvert.DeserializeObject<List<Members>>(jsonData);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error loading Members from file:" + ex.Message);
            }
            return lstMembers;
        }


        private void btnMain_Click(object sender, RoutedEventArgs e)
        {
            MainWindow newWindow = new MainWindow();
            newWindow.Show();
            this.Close();
        }

        private void btnFind_Click(object sender, RoutedEventArgs e)
        {
            List<Members> membersSearch;

            double dblPhone;

            string strLName, strPhone, strEmail;

            int intAt, intPeriod, intEmailLength;
            #region Validation
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
            if (strPhone.Trim() != "")
            {
                if (!double.TryParse(txtPhone.Text, out dblPhone))
                {
                    MessageBox.Show("Please enter valid numbers.");
                    return;
                }



                // validation so that phone number is 10 digits long

                strPhone = Convert.ToString(dblPhone);

                if (strPhone.Length != 10)
                {
                    MessageBox.Show("Phone number has to be 10 digits long.");
                    return;
                }
            }

            // Since an email address needs to contain an 'at mark' after the username and before the domain
            // And a period after the at mark and before the domain, the email address can be validated using the indexing method

            // Make sure email is a valid email address with an at mark after the username and a period before the domain

            if (strEmail.Trim() != "")
            {

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
            #endregion
            if (strPhone == "" && strEmail != "")
            {
                membersSearch = membersIndex.Where(m =>
                    m.LastName.Contains(strLName) &&
                    m.Email.Contains(strEmail)
                ).ToList();
            }
            else if (strEmail == "" && strPhone != "")
            {
                membersSearch = membersIndex.Where(m =>
                    m.LastName.Contains(strLName) &&
                    m.Phone.Contains(strPhone)
                ).ToList();
            }
            else if (strEmail != "" && strPhone != "")
            {
                membersSearch = membersIndex.Where(m =>
                    m.LastName.Contains(strLName) &&
                    m.Phone.Contains(strPhone) &&
                    m.Email.Contains(strEmail)
                ).ToList();
            }
            else
            {
                membersSearch = membersIndex.Where(m =>
                     m.LastName.Contains(strLName)
                     ).ToList();
            }

            foreach (Members m in membersSearch)
            {
                lbxMembers.Items.Add(m.LastName);
            }
            lblNumFound.Content = "(" + membersSearch.Count.ToString() + ")";
        }


        //searching for the member is similar to the pokemon example
        //members where email = email, lastname = lastname, phone number = phone number
        //similar to the first step of the last lab


        //clear button function
        private void btnClear_Click(object sender, RoutedEventArgs e)
        {
            txtLastName.Text = "";
            txtEmail.Text = "";
            txtPhone.Text = "";
            txtDetails.Text = "";
            lbxMembers.Items.Clear();

            //clearing the record search results as well
        }

        private void btnMain_Click_1(object sender, RoutedEventArgs e)
        {
            MainWindow newWindow = new MainWindow();
            newWindow.Show();
            this.Close();
        }

        private void lbxMembers_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            if (lbxMembers.SelectedIndex > -1)
            {
                string strSelectedMember = lbxMembers.SelectedItem.ToString();

                Members membersSelected = membersIndex.Where(m => m.LastName == strSelectedMember).FirstOrDefault();
                txtDetails.Text = membersSelected.ToString();
            }
        }

    }
}




