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
        public Quote InfoFromPrevWindow { get; set; }
        List<Pricing> pricingList;


        public PricingManagement()
        {
            InitializeComponent();

            InfoFromPrevWindow = new Quote();

            pricingList = new List<Pricing>();

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



        }

        private void btnMainMenu_Click(object sender, RoutedEventArgs e)
        {
            MainWindow newWindow = new MainWindow();
            newWindow.Show();
            this.Close();
        }

        //Assigning variables to prices as well as validating it to be valid numbers
        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            string strInd1, strInd12, strTwo1, strTwo12, strFam1, strFam12,
                strInd1Price, strInd12Price, strTwo1Price, strTwo12Price, strFam1Price, strFam12Price,
                strInd1Cbo, strInd12Cbo, strTwo1Cbo, strTwo12Cbo, strFam1Cbo, strFam12Cbo,
                strOutput, strOutput1, strOutput2, strOutput3, strOutput4, strOutput5, strOutput6;


            double dblInd1Price, dblInd12Price, dblTwo1Price, dblTwo12Price, dblFam1Price, dblFam12Price;

            strInd1 = lblInd1.Content.ToString();
            strInd12 = lblInd12.Content.ToString();
            strTwo1 = lblTwo1.Content.ToString();
            strTwo12 = lblTwo12.Content.ToString();
            strFam1 = lblFam1.Content.ToString();
            strFam12 = lblFam12.Content.ToString();

            strInd1Price = txtInd1.Text;
            strInd12Price = txtInd12.Text;
            strTwo1Price = txtTwo1.Text;
            strTwo12Price = txtTwo12.Text;
            strFam1Price = txtFam1.Text;
            strFam12Price = txtFam12.Text;

            if (!double.TryParse(strInd1Price, out dblInd1Price))
            {
                MessageBox.Show("Please enter valid numbers in this field");
                return;
            }

            if (!double.TryParse(strInd12Price, out dblInd12Price))
            {
                MessageBox.Show("Please enter valid numbers in this field");
                return;
            }

            if (!double.TryParse(strTwo1Price, out dblTwo1Price))
            {
                MessageBox.Show("Please enter valid numbers in this field");
                return;
            }

            if (!double.TryParse(strTwo12Price, out dblTwo12Price))
            {
                MessageBox.Show("Please enter valid numbers in this field");
                return;
            }

            if (!double.TryParse(strFam1Price, out dblFam1Price))
            {
                MessageBox.Show("Please enter valid numbers in this field");
                return;
            }

            if (!double.TryParse(strFam12Price, out dblFam12Price))
            {
                MessageBox.Show("Please enter valid numbers in this field");
                return;
            }


            strInd1Price = dblInd1Price.ToString().Trim();
            strInd12Price = dblInd12Price.ToString().Trim();
            strTwo1Price = dblTwo1Price.ToString().Trim();
            strTwo12Price = dblTwo12Price.ToString().Trim();
            strFam1Price = dblFam1Price.ToString().Trim();
            strFam12Price = dblFam12Price.ToString().Trim();

            strInd1Cbo = cboInd1.Text;
            strInd12Cbo = cboInd12.Text;
            strTwo1Cbo = cboTwo1.Text;
            strTwo12Cbo = cboTwo12.Text;
            strFam1Cbo = cboFam1.Text;
            strFam12Cbo = cboFam12.Text;


            if (strInd1Cbo == "Yes")
            {
                strOutput1 = strInd1 + " " + strInd1Price + Environment.NewLine;
            }
            else
            {
                strOutput1 = null;
            }

            if (strInd12Cbo == "Yes")
            {
                strOutput2 = strInd12 + " " + strInd12Price + Environment.NewLine;
            }
            else
            {
                strOutput2 = null;
            }

            if (strTwo1Cbo == "Yes")
            {
                strOutput3 = strTwo1 + " " + strTwo1Price + Environment.NewLine;
            }
            else
            {
                strOutput3 = null;
            }


            if (strTwo12Cbo == "Yes")
            {
                strOutput4 = strTwo12 + " " + strTwo12Price + Environment.NewLine;
            }
            else
            {
                strOutput4 = null;
            }


            if (strFam1Cbo == "Yes")
            {
                strOutput5 = strFam1 + " " + strFam1Price + Environment.NewLine;
            }
            else
            {
                strOutput5 = null;
            }

            if (strFam12Cbo == "Yes")
            {
                strOutput6 = strFam12 + " " + strFam12Price;
            }
            else
            {
                strOutput6 = null;
            }



            strOutput = strOutput1 + strOutput2 + strOutput3 + strOutput4 + strOutput5 + strOutput6;



            string strFilePath = GetFilePath("txt");

            try
            {
                StreamWriter writer = new StreamWriter(strFilePath, false);
                writer.WriteLine(strOutput);
                writer.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error in saving file: " + ex.Message);
                return;
            }

            lsbOutput.Items.Clear();
            lsbOutput.Items.Add(strOutput);

           

        }

        //Grabbing the pricing file

        private string GetFilePath(string extension)
        {
            string strFilePath = @"..\..\..\..\Data\Pricing";
            string strTimestamp = DateTime.Now.Ticks.ToString();

            strFilePath += "." + extension;

            return strFilePath;
        }

       
    }
}
