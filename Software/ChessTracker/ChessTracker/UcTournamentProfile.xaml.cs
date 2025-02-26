using BusinessLogicLayer.Services;
using EntitiesLayer;
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

namespace ChessTracker
{
    /// <summary>
    /// Interaction logic for UcTournamentProfile.xaml
    /// </summary>
    public partial class UcTournamentProfile : UserControl
    {
        private Tournament tournament;
        private int roundId;
        public UcTournamentProfile(Tournament selectedTournament)
        {
            InitializeComponent();
            tournament = selectedTournament;
            DisplayRounds();
        }

        private void cbRounds_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selectedRound = cbRounds.SelectedItem as Round;
            if(selectedRound == null)
            {
                Console.WriteLine("selectedRound null: ", e);
                return;
            }
            roundId = selectedRound.round_id;
            DisplayGames(selectedRound);
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            txtName.Text = tournament.name;
            txtDate.Text = tournament.date.ToString();
            txtPlace.Text = tournament.place;   
        }

        private void btnAddGameRecord_Click(object sender, RoutedEventArgs e)
        {
            var userControl = new UcAddGameRecord(roundId, tournament);
            GuiManager.OpenContent(userControl);

        }

        private void btnEditGameRecord_Click(object sender, RoutedEventArgs e)
        {
            var selectedGame = dgGames.SelectedItem as GameRecord2;
            if (selectedGame == null)
            {
                MessageBox.Show("Odaberite partiju za azuriranje.");
                return;
            }
            var userControl = new UcEditGameRecord(selectedGame.Id, tournament);
            GuiManager.OpenContent(userControl);
        }

        private void btnGenerateResults_Click(object sender, RoutedEventArgs e)
        {
            if(tournament.type == "Standard")
            {
                var userControl = new UcTournamentResultsPlayers(tournament);
                GuiManager.OpenContent(userControl);
            }
            else if (tournament.type == "Ekipno")
            {
                var userControl = new UcTournamentResultsClubs(tournament);
                GuiManager.OpenContent(userControl);
             }
            /*Autor: Nika Antolić*/
            if (tournament != null)
            {
                GameRecordService gameRecordService = new GameRecordService();
                gameRecordService.UpdatePlayerRatingsAtEndOfTournament(tournament.tournament_id);

                MessageBox.Show("Rejtingi su uspješno ažurirani za sve igrače!");
            }
        }

        private void DisplayRounds()
        {
            RoundService roundService = new RoundService();
            var rounds = roundService.GetRoundsByTournamentId(tournament.tournament_id);
            cbRounds.ItemsSource = rounds;
            cbRounds.DisplayMemberPath = "number";
            cbRounds.SelectedIndex = 0;
        }

        private void DisplayGames(Round selectedRound)
        {
            GameRecordService gameRecordService = new GameRecordService();
            var games = gameRecordService.GetGameRecordsByRoundId(selectedRound.round_id, tournament.tournament_id);
            dgGames.ItemsSource = games;
        }

        private void BtnGeneratePairs_OnClick(object sender, RoutedEventArgs e)
        {
            if (tournament != null)
            {
                try
                {
                    PairsService pairingService = new PairsService();
                    bool success = pairingService.GeneratePairsForNextRound(tournament.tournament_id, roundId);

                    if (success)
                    {
                        MessageBox.Show("Parovi su uspješno generirani!");
                        DisplayRounds(); 
                    }
                    else
                    {
                        MessageBox.Show("Parove nije moguće generirati. Provjerite postoji li dovoljno igrača.");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Greška pri generiranju parova: {ex.Message}");
                }
            }
        }
    }
}
