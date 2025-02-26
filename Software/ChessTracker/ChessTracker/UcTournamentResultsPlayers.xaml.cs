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
    /// Interaction logic for UcTournamentResultsPlayers.xaml
    /// </summary>
    public partial class UcTournamentResultsPlayers : UserControl
    {
        private Tournament tournament;
        public UcTournamentResultsPlayers(Tournament tournament)
        {
            InitializeComponent();
            this.tournament = tournament;
            GetPlayers();
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

        private void DisplayResults()
        {
            var playerResultService = new PlayerResultService();
            var playerResults = playerResultService.GetPlayerResultsByTournamentId(tournament.tournament_id);
            dgResults.ItemsSource = playerResults;
        }

        private void GetPlayers()
        {
            string resultMessage;
            var playerService = new PlayerService();
            var gameRecordService = new GameRecordService();
            var playerResultService = new PlayerResultService();
            var players = playerService.GetPlayersByTournamentId(tournament.tournament_id);
            foreach (var player in players)
            {
                var score = gameRecordService.GetScoreByPlayerId(player.player_id, tournament.tournament_id);

                PlayerResult playerResult = new PlayerResult
                {
                    player_id = player.player_id,
                    tournament_id = tournament.tournament_id,
                    score = score
                };

                playerResultService.AddPlayerResult(playerResult, out resultMessage);
            }
        }
    }
}
