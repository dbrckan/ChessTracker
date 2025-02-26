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

/*Autor: Nika Antolić*/

namespace ChessTracker
{
    /// <summary>
    /// Interaction logic for UcLoadAllClubsAndPlayers.xaml
    /// </summary>
    public partial class UcLoadAllClubsAndPlayers : UserControl
    {
        private ClubService clubService = new ClubService();
        private PlayerService playerService = new PlayerService();
        public UcLoadAllClubsAndPlayers()
        {
            InitializeComponent();
        }

        private void dgClub_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dgClub.SelectedItem is Club selectedClub)
            {
                DisplayPlayers(selectedClub.club_id);
            }
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            DisplayClubs();
        }

        private void DisplayClubs()
        {
            var clubs = clubService.GetAllClubs();
            dgClub.ItemsSource = clubs;
        }
        private void DisplayPlayers(int clubId)
        {
            var players = playerService.GetPlayers(clubId);
            dgPlayers.ItemsSource = players;
        }

        private void dgPlayers_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dgPlayers.SelectedItem is Player selectedPlayer)
            {
                var userControl = new UcPlayerProfile(selectedPlayer);
                GuiManager.OpenContent(userControl);
            }
        }
    }
}
