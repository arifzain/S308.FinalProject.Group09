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

        public string LockerRental { get; set; }

        public SignUp()
        {
            MembershipType = "";
            StartDate = "";
            

        }

        public SignUp(string membershipType, string startDate, string lockerRental)
        {
            MembershipType = membershipType;
            StartDate = startDate;
            LockerRental = lockerRental;
        }
    }
}
