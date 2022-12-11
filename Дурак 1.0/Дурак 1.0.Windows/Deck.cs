using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Дурак_1._0
{
    class Deck
    {
        private List<Card> cards;
        public Deck()
        {
            cards = CreateNewDeck();
        }
        public Deck(List<Card> cards)
        {
            this.cards = cards;
        }
        public Card Peek(int index)
        {
            return cards[index];
        }
        public Card Delate(int index)
        {
            Card cardToReturn = cards[index];
            cards.RemoveAt(index);
            return cardToReturn;
        }
        public Card Delate()
        {
            Card cardToReturn = cards[0];
            cards.RemoveAt(0);
            return cardToReturn;
        }
        public void Add(Card card)
        {
            cards.Add(card);
        }
        public void Shuffle()
        {
            cards.Sort(new RandomComparer());
        }
        public void Sort()
        {
            cards.Sort(new ComparerByValue());
        }
        public int Count()
        {
            return cards.Count;
        }
        public List<string> GetNamesOfCards()
        {
            List<string> names = new List<string>();
            foreach (Card card in cards)
                names.Add(card.ToString());
            return names;
        }
        public int ToThinkAboutMove(Card cardOnTable, Suits ruffs)
        {
            if (cardOnTable == null)
                return ToFindSmallestCard(ruffs);
            else
                return ToFindMoreBiggerCard(cardOnTable, ruffs);
        }
        private int ToFindSmallestCard(Suits ruffs)
        {
            int index = 0;
            int i = 0;
            Card cardToReturn = cards[0];
            foreach(Card card in cards)
            {
                if (card.value < cardToReturn.value && card.suit != ruffs)
                {
                    cardToReturn = card;
                    index = i;
                }
                i++;
            }
            return index;
        }
        private int ToFindMoreBiggerCard(Card cardOnTable, Suits ruffs)
        {
            int i = 0;
            foreach(Card card in cards)
            {
                if (cardOnTable.suit == card.suit && card.value > cardOnTable.value)
                    return i;
                i++;
            }
            i = 0;
            foreach(Card card in cards)
            {
                if (card.suit == ruffs && cardOnTable.suit!=ruffs)
                    return i;
                i++;
            }
            return -1;
        }
        private List<Card> CreateNewDeck()
        {
            List<Card> cardsToReturn = new List<Card>();
            for (int i = 0; i < 4; i++)
                for (int a = 0; a < 9; a++)
                    cardsToReturn.Add(new Card((Suits)i, (Values)a));
            return cardsToReturn;
        }
        public void AddCards(List<Card> cardsForAdding)
        {
            cards.AddRange(cardsForAdding);
        }
    }
    class RandomComparer : IComparer<Card>
    {
        Random random = new Random();
        public int Compare(Card x, Card y)
        {
            return random.Next(-1, 1);
        }
    }
    class ComparerBySuit : IComparer<Card>
    {
        public int Compare(Card x, Card y)
        {
            if (x.suit < y.suit)
                return -1;
            if (x.suit > y.suit)
                return 1;
            else
                return 0;
        }
    }
    class ComparerByValue : IComparer<Card>
    {
        public int Compare(Card x, Card y)
        {
            if (x.value < y.value)
                return -1;
            if (x.value > y.value)
                return 1;
            else
                return 0;
        }
    }
}
