using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Дурак_1._0
{
    class Card
    {
        public Suits suit;
        public Values value;
        public Card(Suits suit, Values value)
        {
            this.suit = suit;
            this.value = value;
        }
        public override string ToString()
        {
            return value.ToString() + " " + suit.ToString();
        }
    }
}
