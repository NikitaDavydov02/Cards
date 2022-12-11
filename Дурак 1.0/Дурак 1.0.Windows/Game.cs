using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using Windows.UI.Popups;
using System.ComponentModel;

namespace Дурак_1._0
{
    class Game:INotifyPropertyChanged
    {
        public int indexOfMover;
        private List<Deck> players;
        private Deck stock;
        private Deck beaten;
        private Table table;
        private Suits ruffs;
        private Card ruffsCard;
        public int HowManyHasHuman { get; private set; }
        public int HowManyHasComputer { get; private set; }
        public string Name { get; set; }
        public int HowManyCardsInStock { get; private set; }
        public string BottonCardName { get; private set; }
        public ObservableCollection<string> PlayersCards { get; private set; }
        public event PropertyChangedEventHandler PropertyChanged;
        public string HumanCard { get; private set; }
        public string ComputerCard { get; private set; }
        public Game()
        {
            PlayersCards = new ObservableCollection<string>();
        }
        public void StartTheGame()
        {
            indexOfMover = 0;
            stock = new Deck();
            stock.Shuffle();
            ruffsCard = stock.Peek(35);
            ruffs = ruffsCard.suit;
            players = new List<Deck>()
            {
                new Deck(new List<Card>()),
                new Deck(new List<Card>()),
            };
            beaten = new Deck(new List<Card>());
            table = new Table();
            AddCardsToPlayers();
            players[0].Sort();
            HowManyHasHuman = players[0].Count();
            HowManyHasComputer = players[1].Count();
            HowManyCardsInStock = stock.Count();
            BottonCardName = ruffsCard.ToString();
            PlayersCards.Clear();
            foreach (String card in players[0].GetNamesOfCards())
                PlayersCards.Add(card);
        }
        private void OnPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler propertyChangedEvent = PropertyChanged;
            if (propertyChangedEvent != null)
            {
                propertyChangedEvent(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        public bool PlayOneMove(int indexOfCard)
        {
            if (table.Count() == 0)
                table.Add(players[indexOfMover].Delate(indexOfCard), players[indexOfMover]);
            else
            {
                if (table.CheckCard(players[indexOfMover].Peek(indexOfCard), players[AnotherPlayer(indexOfMover)], ruffs))
                {
                    table.Add(players[indexOfMover].Delate(indexOfCard), players[indexOfMover]);
                    beaten.AddCards(table.DelateAll());
                    AddCardsToPlayers();
                }
                else
                {
                    MessageDialog dialog = new MessageDialog("Вы не можете походить этой картой!");
                    dialog.ShowAsync();
                    return true;
                }
            }
            if (!CheckTheGame())
            {
                GetWinnerName();
                return false;
            }
            if (table.Count() == 0)
            {
                if (indexOfMover == 1)
                    return PlayOneMove(players[1].ToThinkAboutMove(null, ruffs));
                else
                {
                    HowManyHasHuman = players[0].Count();
                    HowManyHasComputer = players[1].Count();
                    HowManyCardsInStock = stock.Count();
                    HumanCard = "";
                    ComputerCard = "";
                    players[0].Sort();
                    PlayersCards.Clear();
                    foreach (String card in players[0].GetNamesOfCards())
                        PlayersCards.Add(card);
                    UpdateProperties();
                    return true;
                }
            }
            else
            {
                if (indexOfMover == 0)
                {
                    indexOfMover = 1;
                    if (players[1].ToThinkAboutMove(table.Peek(players[0]), ruffs) == -1)
                    {
                        if (!PlayerIsTaking(1))
                            return false;
                        else
                        {
                            UpdateProperties();
                            return true;
                        }
                    }
                    return PlayOneMove(players[1].ToThinkAboutMove(table.Peek(players[0]), ruffs));
                }
                else
                {
                    indexOfMover = 0;
                    HowManyHasHuman = players[0].Count();
                    HowManyHasComputer = players[1].Count();
                    HowManyCardsInStock = stock.Count();
                    players[0].Sort();
                    PlayersCards.Clear();
                    foreach (String card in players[0].GetNamesOfCards())
                        PlayersCards.Add(card);
                    HumanCard = "";
                    ComputerCard = table.Peek(players[1]).ToString();
                    UpdateProperties();
                    return true;
                }
            }
        }
        private void UpdateProperties()
        {
            HowManyHasHuman = players[0].Count();
            HowManyHasComputer = players[1].Count();
            HowManyCardsInStock = stock.Count();
            OnPropertyChanged("HowManyHasHuman");
            OnPropertyChanged("HowManyHasComputer");
            OnPropertyChanged("HowManyCardsInStock");
            OnPropertyChanged("HumanCard");
            OnPropertyChanged("ComputerCard");
        }
        private void AddCardsToPlayers()
        {
            if (stock.Count() > 0)
            {
                while (stock.Count() > 1 && (players[0].Count() < 6 || players[1].Count() < 6))
                {
                    if (players[0].Count() < 6)
                        players[0].Add(stock.Delate());
                    if (players[1].Count() < 6)
                        players[1].Add(stock.Delate());
                }
                if (players[0].Count() < 6 && stock.Count() > 0)
                    players[0].Add(stock.Delate());
                if (players[1].Count() < 6 && stock.Count() > 0)
                    players[1].Add(stock.Delate());
            }
        }
        private int AnotherPlayer(int indexOfPlayer)
        {
            if (indexOfPlayer == 0)
                return 1;
            else
                return 0;
        }
        private bool CheckTheGame()
        {
            if (players[0].Count() == 0 || players[1].Count() == 0)
                if (stock.Count() == 0)
                    return false;
            return true;
        }
        private void GetWinnerName()
        {
            UpdateProperties();
            MessageDialog dialog;
            if (players[0].Count() == 0 && players[1].Count() == 0)
                dialog = new MessageDialog("Ничья!");
            if (players[0].Count() == 0)
                dialog = new MessageDialog("Вы победили!");
            else
                dialog = new MessageDialog("Победил компьютер!");
            dialog.ShowAsync();
        }
        public bool PlayerIsTaking(int indexOfPlayer)
        {
            players[indexOfPlayer].AddCards(table.DelateAll());
            AddCardsToPlayers();
            PlayersCards.Clear();
            foreach (String card in players[0].GetNamesOfCards())
                PlayersCards.Add(card);
            if (!CheckTheGame())
                return false;
            if (indexOfMover == 0)
            {
                indexOfMover = 1;
                return PlayOneMove(players[1].ToThinkAboutMove(null, ruffs));
            }
            else
                indexOfMover = 0;
            UpdateProperties();
            return true;
        }
        public int TableCount()
        {
            return table.Count();
        }
    }
}
