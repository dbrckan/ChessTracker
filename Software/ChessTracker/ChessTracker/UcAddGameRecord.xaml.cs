using BusinessLogicLayer.Services;
using EntitiesLayer;
using EntitiesLayer.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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
    /// Interaction logic for UcAddGameRecord.xaml
    /// </summary>
    public partial class UcAddGameRecord : UserControl
    {
        private Tournament tournament;
        private int roundId;
        private int player1Id;
        private int player2Id;
        private int recordId;
        private int pairId;
        public UcAddGameRecord(int roundId, Tournament tournament)
        {
            InitializeComponent();
            this.roundId = roundId;
            this.tournament = tournament;
            DisplayGameIds();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            if(string.IsNullOrWhiteSpace(txtResult.Text) || (txtResult.Text != "1" && txtResult.Text != "2" && txtResult.Text != "/"))
            {
                MessageBox.Show("Unesite ispravan format rezultata partije");
                return;
            }

            if (cmbGameId.SelectedItem == null)
            {
                MessageBox.Show("Odaberite partiju.");
                return;
            }



            string resultMessage;
            var gameRecordService = new GameRecordService();
            var gameRecord = new GameRecord
            {
                result = txtResult.Text,
                game_id = (int)cmbGameId.SelectedValue,
                pair_id = pairId
            };
            bool success = gameRecordService.AddGameRecord(gameRecord,out resultMessage);

            if (success)
            {
                recordId = gameRecord.record_id;
                var gameHistoryService = new GameHistoryService();
                var gameHistory1 = new GameHistory
                {
                    record_id = recordId,
                    player_id = player1Id,
                    date = DateTime.Now
                };
                var gameHistory2 = new GameHistory
                {
                    record_id = recordId,
                    player_id = player2Id,
                    date = DateTime.Now
                };

                gameHistoryService.AddGameHistory(gameHistory1, out resultMessage);
                gameHistoryService.AddGameHistory(gameHistory2, out resultMessage);

                /*Autor: Nika Antolić*/
                var playerService = new PlayerService();
                var player1 = playerService.GetPlayerById(player1Id);
                var player2 = playerService.GetPlayerById(player2Id);

                gameRecordService.UpdateChangeOfRating(gameRecord, player1, player2);


                MessageBox.Show("Evidencija partije je uspješno unesena");
                GuiManager.OpenContent(new UcTournamentProfile(tournament));
            }
            else
            {
                MessageBox.Show(resultMessage);
            }
        }

        private void DisplayGameIds()
        {
            var gameService = new GameService();
            var games = gameService.GetGamesByRoundId(roundId);
            cmbGameId.ItemsSource = games;
            cmbGameId.DisplayMemberPath = "number";
            cmbGameId.SelectedValuePath = "game_id";
            cmbGameId.SelectedIndex = 0;
        }

        private void DisplayPairs(int gameId)
        {
            var pairsService = new PairsService();
            var pair = pairsService.GetPairByGameId(gameId);
            txtPlayers.Text = pair.Pair;
            player1Id = pair.Player1Id;
            player2Id = pair.Player2Id;
            pairId = pair.PairId;
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            GuiManager.OpenContent(new UcTournamentProfile(tournament));
        }

        private void cmbGameId_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selectedGame = cmbGameId.SelectedItem as Game;
            DisplayPairs(selectedGame.game_id);
        }
    }
}
