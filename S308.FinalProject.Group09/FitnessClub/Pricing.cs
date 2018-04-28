using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessClub
{
    public class Pricing
    {
        public double Individual1Month { get; set; }

        public double Individual2Month { get; set; }

        public double TwoPerson1Month { get; set; }

        public double TwoPerson12Month { get; set; }

        public double Family1Month { get; set; }

        public double Family12Month { get; set; }

        public Pricing()
        {
            Individual1Month = 0;
            Individual2Month = 0;
            TwoPerson1Month = 0;
            TwoPerson12Month = 0;
            Family1Month = 0;
            Family12Month = 0;

        }

        public Pricing(double ind1, double ind12, double two1, double two12, double fam1, double fam12)
        {
            Individual1Month = ind1;
            Individual2Month = ind12;
            TwoPerson1Month = two1;
            TwoPerson12Month = two12;
            Family1Month = fam1;
            Family12Month = fam12;
        }

    }
}
