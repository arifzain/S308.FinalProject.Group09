using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessClub
{
    public class Pricing
    {
        public string Individual1Month { get; set; }

        public string Individual12Month { get; set; }

        public string TwoPerson1Month { get; set; }

        public string TwoPerson12Month { get; set; }

        public string Family1Month { get; set; }

        public string Family12Month { get; set; }

        public Pricing()
        {
            Individual1Month = "";
            Individual12Month = "";
            TwoPerson1Month = "";
            TwoPerson12Month = "";
            Family1Month = "";
            Family12Month = "";

        }

        public Pricing(string ind1, string ind12, string two1, string two12, string fam1, string fam12)
        {
            Individual1Month = ind1;
            Individual12Month = ind12;
            TwoPerson1Month = two1;
            TwoPerson12Month = two12;
            Family1Month = fam1;
            Family12Month = fam12;
        }

    }
}
