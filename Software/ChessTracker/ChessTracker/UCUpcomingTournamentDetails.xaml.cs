using System.Collections.Generic;
using System.Linq;
using System.Windows.Controls;
using BusinessLogicLayer.Services;
using EntitiesLayer;

namespace ChessTracker
{
    public partial class UCUpcomingTournamentDetails : UserControl
    {
        public List<string> ClubNames { get; set; }
        public List<string> PlayerNames { get; set; }

        public UCUpcomingTournamentDetails(Tournament tournament)
        {
            InitializeComponent();
            DataContext = tournament; 
            
            var tournamentRegistrationService = new TournamentRegistrationService();
            ClubNames = tournamentRegistrationService.GetRegisteredClubs(tournament.tournament_id).Select(c => c.name)
                .ToList();

            PlayerNames = tournamentRegistrationService.GetRegisteredPlayers(tournament.tournament_id)
                .Select(p => p.firstName + " " + p.lastName).ToList();

            // Manually refresh the UI bindings
            ClubsListBox.ItemsSource = ClubNames;
            PlayersListBox.ItemsSource = PlayerNames;
        }
    }
}

