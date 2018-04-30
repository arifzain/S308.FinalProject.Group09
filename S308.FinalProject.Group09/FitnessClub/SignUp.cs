using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessClub
{
    public class SignUp
    {
        public string MembershipType { get; set; }

        public string StartDate { get; set; }

        public string EndDate { get; set; }

        public string MembershipCost { get; set; }

        public string Subtotal { get; set; }

        public string PersonalTraining { get; set; }

        public string LockerRental { get; set; }

        public string Total { get; set; }

        public SignUp()
        {
            MembershipType = "";
            StartDate = "";
            EndDate = "";
            MembershipCost = "";
            Subtotal = "";
            PersonalTraining = "";
            LockerRental = "";
            Total = "";

            

        }

        public SignUp(string membershipType, string startDate, string endDate, string membershipCost, string subtotal, string personalTraining, string lockerRental, string total)
        {
            MembershipType = membershipType;
            StartDate = startDate;
            EndDate = endDate;
            MembershipCost = membershipCost;
            Subtotal = subtotal;
            PersonalTraining = personalTraining;
            LockerRental = lockerRental;
            Total = total;
        }
    }
}
