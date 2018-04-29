using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessClub
{
    public class Members
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string CardType { get; set; }
        public string CardNumber { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Gender { get; set; }
        public string MembershipType { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public string MonthlyCost { get; set; }
        public string PersonalTrainer { get; set; }
        public string LockerRental { get; set; }
        public string TotalCost { get; set; }
        public string Age { get; set; }
        public string Weight { get; set; }
        public string FitnessGoal { get; set; }

        public Members()
        {
            FirstName = "";
            LastName = "";
            CardType = "";
            CardNumber = "";
            Phone = "";
            Email = "";
            Gender = "";
            MembershipType = "";
            StartDate = "";
            EndDate = "";
            MonthlyCost = "";
            PersonalTrainer = "";
            LockerRental = "";
            TotalCost = "";
            Age = "";
            Weight = "";
            FitnessGoal = "";
        }

        public Members(string fName, string lName, string cardType, string cardNumber, string phone, string email,
            string gender, string membershipType, string startDate, string endDate, string monthlyCost, string personalTrainer, string lockerRental,
            string totalCost, string age, string weight, string fitnessGoal)
        {
            FirstName = fName;
            LastName = lName;
            CardType = cardType;
            CardNumber = cardNumber;
            Phone = phone;
            Email = email;
            Gender = gender;
            MembershipType = membershipType;
            StartDate = startDate;
            EndDate = endDate;
            MonthlyCost = monthlyCost;
            PersonalTrainer = personalTrainer;
            LockerRental = lockerRental;
            TotalCost = totalCost;
            Age = age;
            Weight = weight;
            FitnessGoal = fitnessGoal;
        }

    }
    }
