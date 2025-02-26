using System;
using System.Data.Entity.Infrastructure;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using BusinessLogicLayer.Exceptions;
using BusinessLogicLayer.Services;
using DataAccessLayer.Repositories;
using EntitiesLayer;
using EntitiesLayer.Entities;

namespace ChessTracker
{
    public partial class UCTournamentManagement : UserControl
    {
        private TournamentService tournamentService = new TournamentService();

        public UCTournamentManagement()
        {
            InitializeComponent();
            LoadTournaments();
        }

        private void LoadTournaments()
        {
            TournamentList.ItemsSource = tournamentService.GetAllTournaments();
        }

        private void BtnAddTournament_OnClick(object sender, RoutedEventArgs e)
        {
            AddTournamentWindow addWindow = new AddTournamentWindow();
            if (addWindow.ShowDialog() == true)
            {
                var newTournament = new Tournament
                {
                    name = addWindow.TournamentName,
                    date = addWindow.TournamentDate,
                    time = addWindow.TournamentTime,
                    place = addWindow.TournamentPlace,
                    type = "Standard",
                    numberOfRounds = 5,
                    judge_id = int.Parse(addWindow.SelectedJudgeId),
                    federation_id = CurrentUser.UserId.HasValue
                        ? CurrentUser.UserId.Value
                        : throw new UnexpectedArgumentException("Korisnik nije prijavljen ili ne postoji ID!"),
                };
                string resultMessage;
                tournamentService.AddTournament(newTournament, out resultMessage);
                Console.WriteLine(resultMessage);
            }
        }

        private void BtnDeleteTournament_OnClick(object sender, RoutedEventArgs e)
        {
            var selectedTournament = TournamentList.SelectedItem as Tournament;

            if (selectedTournament != null)
            {
                MessageBoxResult result = MessageBox.Show(
                    "Zelite li obrisati ovaj turnir?",
                    "Izbrisi turnir",
                    MessageBoxButton.YesNo,
                    MessageBoxImage.Warning);

                if (result == MessageBoxResult.Yes)
                {
                    try
                    {
                        tournamentService.DeleteTournament(selectedTournament);
                    }
                    catch (DbUpdateException exception)
                    {
                        MessageBox.Show($"Nije moguce izbrisati turnir koji je povezani na druge tablice!");
                        Console.WriteLine(exception);
                    }

                    LoadTournaments();
                }
            }
        }

        private void BtnUpdateTournament_OnClick(object sender, RoutedEventArgs e)
        {
            var selectedTournament = TournamentList.SelectedItem as Tournament;
            if (selectedTournament == null)
            {
                MessageBox.Show("Odaberite turnir.");
                return;
            }
            var updateWindow = new UpdateTournamentWindow(selectedTournament);
            if (updateWindow.ShowDialog() == true)
            {
                try
                {
                    string resultMessage;
                    tournamentService.UpdateTournament(selectedTournament, out resultMessage);
                    Console.WriteLine(resultMessage);
                    LoadTournaments();
                }
                catch (Exception exception)
                {
                    MessageBox.Show(exception.Message);
                }
            }
        }
    }
}