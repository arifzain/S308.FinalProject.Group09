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
        }

        private void btnQuote_Click(object sender, RoutedEventArgs e)
        {
            string strMmbershipType;
           

            DateTime datToday = DateTime.Today;

            //validate customer type selected
            if (cboMembershipType.SelectedIndex == -1)
            {
                MessageBox.Show("Please select a membership type.");
                return;
            }

            strMmbershipType = cboMembershipType.ToString();

            
            //validate date
            datStartDate.DisplayDateStart = datToday;


            if (datStartDate.SelectedDate < datToday)
            {
                MessageBox.Show("Please select a date that is not in the past");
                return;
            }






            
            
            
            

            MembershipQuote newWindow = new MembershipQuote();
            newWindow.Show();
            this.Close();
        }

    }
}