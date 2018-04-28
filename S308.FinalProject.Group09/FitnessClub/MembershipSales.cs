using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessClub
{
    public class Quote
    {
        public string MembershipType { get; set; }

        public string StartDate { get; set; }

        public string PersonalTraining { get; set; }

        public string LockerRental { get; set; }

        public Quote()
        {
            MembershipType = "";
            StartDate = "";
            PersonalTraining = "";
            LockerRental = "";

        }

        public Quote(string membershipType, string startDate, string personalTraining, string lockerRental)
        {
            MembershipType = membershipType;
            StartDate = startDate;
            PersonalTraining = personalTraining;
            LockerRental = lockerRental;
        }
    }
}
