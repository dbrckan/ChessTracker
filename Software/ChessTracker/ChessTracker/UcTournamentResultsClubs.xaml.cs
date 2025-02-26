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
    /// Interaction logic for UcTournamentResultsClubs.xaml
    /// </summary>
    public partial class UcTournamentResultsClubs : UserControl
    {
        private Tournament tournament;
        public UcTournamentResultsClubs(Tournament tournament)
        {
            InitializeComponent();
            this.tournament = tournament;
            GetClubsAndPlayers();
            DisplayResults();

        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            txtName.Text = tournament.name;
        }

        private void btnPrint_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            GuiManager.OpenContent(new UcTournamentProfile(tournament));
        }

        private void GetClubsAndPlayers()
        {
            string resultMessage;
            var clubService = new ClubService();
            var clubResultService = new ClubResultService();
            var playerService = new PlayerService();
            var gameRecordService = new GameRecordService();
            var clubs = clubService.GetClubsByTournamentId(tournament.tournament_id);

            foreach (var club in clubs)
            {
                var players = playerService.GetPlayersByTournamentId(tournament.tournament_id);
                foreach (var player in players)
                {
                    var score = gameRecordService.GetScoreByPlayerIdAndClubId(player.player_id, club.club_id, tournament.tournament_id);
                    
                    var clubResult = new ClubResult
                    {
                        club_id = club.club_id,
                        tournament_id = tournament.tournament_id,
                        score = score
                    };

                    clubResultService.AddClubResult(clubResult, out resultMessage);

                }
            }
        }

        private void DisplayResults()
        {
            var clubResultService = new ClubResultService();
            var clubResults = clubResultService.GetClubResultsByTournamentId(tournament.tournament_id);
            dgResults.ItemsSource = clubResults;
        }
    }
}
