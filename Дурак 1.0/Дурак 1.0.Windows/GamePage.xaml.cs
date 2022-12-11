using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Windows.UI.Popups;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace Дурак_1._0
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class GamePage : Page
    {
        public GamePage()
        {
            this.InitializeComponent();
        }
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            string name = e.Parameter.ToString();
            game.Name = name;
            game.StartTheGame();
            base.OnNavigatedTo(e);
        }
        private void gameOver_Click(object sender, RoutedEventArgs e)
        {
            MessageDialog dialog = new MessageDialog("Выиграл компьютер!");
            dialog.ShowAsync();
            this.Frame.Navigate(typeof(MainPage));
        }
        private void cards_DoubleTapped(object sender, DoubleTappedRoutedEventArgs e)
        {
            if (cards.SelectedIndex >= 0)
            {
                if (!game.PlayOneMove(cards.SelectedIndex))
                    this.Frame.Navigate(typeof(MainPage));
                if (game.TableCount() != 0)
                    take.IsEnabled = true;
                else
                    take.IsEnabled = false;
            }
        }
        private void take_Click(object sender, RoutedEventArgs e)
        {
            if (!game.PlayerIsTaking(0))
                this.Frame.Navigate(typeof(MainPage));
        }
    }
}
