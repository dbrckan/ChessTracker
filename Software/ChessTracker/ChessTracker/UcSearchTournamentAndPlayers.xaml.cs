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

//Autor: David Brckan

namespace ChessTracker
{
    
    public partial class UcSearchTournamentAndPlayers : UserControl
    {
        private readonly TournamentService _tournamentService;
        private readonly PlayerService _playerService;

        private List<Player> _allPlayers;
        private List<Tournament> _allTournaments;

        public UcSearchTournamentAndPlayers()
        {
            InitializeComponent();
            _tournamentService = new TournamentService();
            _playerService = new PlayerService();

            LoadPlayers();
            LoadTournaments();
        }

        private void LoadPlayers()
        {
            _allPlayers = _playerService.GetAllPlayers();
            dgPlayers.ItemsSource = _allPlayers;
        }

        private void LoadTournaments()
        {
            _allTournaments = _tournamentService.GetAllTournaments();
            dgTournaments.ItemsSource = _allTournaments;
        }

        private void txtSearchPlayers_TextChanged(object sender, TextChangedEventArgs e)
        {
            string searchQuery = txtSearchPlayers.Text.ToLower();

            if (string.IsNullOrEmpty(searchQuery))
            {
                dgPlayers.ItemsSource = _allPlayers;
                return;
            }

            var players = _allPlayers;


            if (decimal.TryParse(searchQuery, out decimal ratingValue))
            {
                players = players.Where(p => p.rating == ratingValue).ToList();
            }
            else
            {
                players = players.Where(p =>
                    p.firstName.ToLower().Contains(searchQuery) ||
                    p.contact.ToLower().Contains(searchQuery) ||
                    p.lastName.ToLower().Contains(searchQuery) ||
                    p.username.ToLower().Contains(searchQuery) ||
                    p.gender.ToLower().Contains(searchQuery) ||
                    p.Club.name.ToLower().Contains(searchQuery)
                ).ToList();
            }

            dgPlayers.ItemsSource = players;
        }

        private void txtSearchTournaments_TextChanged(object sender, TextChangedEventArgs e)
        {
            string searchQuery = txtSearchTournaments.Text.ToLower();

            if (string.IsNullOrEmpty(searchQuery))
            {
                dgTournaments.ItemsSource = _allTournaments;
                return;
            }

            var filteredTournaments = _allTournaments
                .Where(t => (t.place?.ToLower().Contains(searchQuery) ?? false) ||
                            (t.name?.ToLower().Contains(searchQuery) ?? false) ||
                            t.date.ToString("dd.MM.yyyy").Contains(searchQuery) ||
                            t.time.ToString(@"hh\:mm").Contains(searchQuery))
                .ToList();

            dgTournaments.ItemsSource = filteredTournaments;
        }

        private void dgPlayers_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dgPlayers.SelectedItem is Player selectedPlayer)
            {
                var userControl = new UcPlayerProfile(selectedPlayer);
                GuiManager.OpenContent(userControl);
            }
        }

        private void dgTournaments_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
        }
    }
}
