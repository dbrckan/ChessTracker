using BusinessLogicLayer.Services;
using EntitiesLayer.Entities;
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
using DataAccessLayer;

//Autor: David Brckan

namespace ChessTracker
{

    public partial class UcTournamentRegistration : UserControl
    {
        private readonly TournamentRegistrationService _registrationService;
        private readonly ChessModel _context;

        public UcTournamentRegistration()
        {
            InitializeComponent();
            _registrationService = new TournamentRegistrationService();
            _context = new ChessModel();
            LoadTournaments();
        }

        private void LoadTournaments()
        {
            var tournaments = _registrationService.GetUpcomingTournaments();
            dgTournaments.ItemsSource = tournaments;
        }

        private void btnRegisterPlayer_Click(object sender, RoutedEventArgs e)
        {
            lblErrorMessage.Visibility = Visibility.Collapsed;

            if (dgTournaments.SelectedItem == null)
            {
                ShowMessage("Morate odabrati turnir.", false);
                return;
            }

            var selectedTournament = (Tournament)dgTournaments.SelectedItem;
            int tournamentId = selectedTournament.tournament_id;

            var player = _context.Player.FirstOrDefault(p => p.username == CurrentUser.Username);
            if (player == null)
            {
                ShowMessage("Greška: Igrač nije pronađen.", false);
                return;
            }
            string message;
            bool success = _registrationService.RegisterPlayerForTournament(player.player_id, tournamentId, out message);

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
