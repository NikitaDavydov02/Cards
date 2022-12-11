using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Дурак_1._0
{
    class Table
    {
        private Dictionary<Deck, Card> cardsOnTable;
        public Table()
        {
            cardsOnTable = new Dictionary<Deck, Card>();
        }
        public int Count()
        {
            return cardsOnTable.Count;
        }
        public void Add(Card card, Deck deck)
        {
            cardsOnTable.Add(deck, card);
        }
        public Card Delate(Deck deck)
        {
            Card cardToReturn = cardsOnTable[deck];
            cardsOnTable.Remove(deck);
            return cardToReturn;
        }
        public List<Card> DelateAll()
        {
            IEnumerable<Card> cards = cardsOnTable.Values;
            List<Card> cardsToReturn = new List<Card>(cards);
            cardsOnTable.Clear();
            return cardsToReturn;
        }
        public bool CheckCard(Card cardToCheck,Deck deck,Suits ruffs)
        {
            Card secondCard = cardsOnTable[deck];
            if (cardToCheck.suit == secondCard.suit && cardToCheck.value > secondCard.value)
                return true;
            if (cardToCheck.suit == ruffs && secondCard.suit != ruffs)
                return true;
            return false;
        }
        public Card Peek(Deck deck)
        {
            return cardsOnTable[deck];
        }
    }
}
