using BusinessLogicLayer.Services;
using DataAccessLayer;
using EntitiesLayer.Entities;
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
  
    public partial class UcTournamentRegistrationClub : UserControl
    {
        private readonly TournamentRegistrationService _registrationService;
        private readonly ChessModel _context;

        public UcTournamentRegistrationClub()
        {
            InitializeComponent();
            _registrationService = new TournamentRegistrationService();
            _context = new ChessModel();
            LoadData();
        }

        private void LoadData()
        {

            var tournaments = _registrationService.GetUpcomingTournaments();
            
            dgTournaments.ItemsSource = tournaments;

            
            var clubId = (from c in _context.Club
                          where c.username == CurrentUser.Username
                          select c.club_id).FirstOrDefault();

            if (clubId > 0)
            {
                var players = _context.Player
                    .Where(p => p.club_id == clubId)
                    .Select(p => new
                    {
                        p.player_id,
                        p.firstName,
                        p.lastName,
                        p.rating
                    }).ToList();

                dgPlayers.ItemsSource = players;
            }
        }

        private void btnRegisterClub_Click(object sender, RoutedEventArgs e)
        {
            lblErrorMessage.Visibility = Visibility.Collapsed;

          
            if (dgTournaments.SelectedItem == null)
            {
                ShowMessage("Morate odabrati turnir.", false);
                return;
            }

            var selectedTournament = (dynamic)dgTournaments.SelectedItem;
            int tournamentId = selectedTournament.tournament_id;

           
            var selectedPlayers = dgPlayers.SelectedItems.Cast<dynamic>().Select(p => (int)p.player_id).ToList();
            if (!selectedPlayers.Any())
            {
                ShowMessage("Morate odabrati barem jednog igrača.", false);
                return;
            }

            
            var clubId = (from c in _context.Club
                          where c.username == CurrentUser.Username
                          select c.club_id).FirstOrDefault();

            if (clubId == 0)
            {
                ShowMessage("Greška: Klub nije pronađen.", false);
                return;
            }

            string message;
            bool success = _registrationService.RegisterClubForTournament(clubId, tournamentId, selectedPlayers, out message);

            ShowMessage(message, success);
        }

        private void ShowMessage(string message, bool isSuccess)
        {
            lblErrorMessage.Foreground = new SolidColorBrush(isSuccess ? Colors.Green : Colors.Red);
            lblErrorMessage.Content = message;
            lblErrorMessage.Visibility = Visibility.Visible;
        }
    }
}
