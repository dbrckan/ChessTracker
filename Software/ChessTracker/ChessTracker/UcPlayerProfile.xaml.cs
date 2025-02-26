using BusinessLogicLayer.Services;
using EntitiesLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ChessTracker
{
    /// <summary>
    /// Interaction logic for UcPlayerProfile.xaml
    /// </summary>
    public partial class UcPlayerProfile : UserControl
    {
        private Player player;
        public UcPlayerProfile(Player selectedPlayer)
        {
            InitializeComponent();
            player = selectedPlayer;
            DisplayGames();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            txtFirstName.Text = player.firstName;
            txtLastName.Text = player.lastName;
            txtContact.Text = player.contact;
            txtRating.Text = player.rating.ToString();
        }

        private void DisplayGames()
        {
            GameHistoryService gameHistoryService = new GameHistoryService();
            var games = gameHistoryService.GetGameHistory(player.player_id);
            dgGames.ItemsSource = games;
        }
    }
}
