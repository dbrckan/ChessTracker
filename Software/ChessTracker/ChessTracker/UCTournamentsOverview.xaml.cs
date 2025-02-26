using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using BusinessLogicLayer.Services;
using EntitiesLayer;

namespace ChessTracker
{
    public partial class UCTournamentsOverview : UserControl
    {
        private TournamentService _tournamentService;
        public UCTournamentsOverview()
        {
            InitializeComponent();
            _tournamentService = new TournamentService();
            LoadTournaments();
        }

        private void LoadTournaments()
        {
            var tournaments = _tournamentService.GetAllTournaments();
            TournamentsListView.ItemsSource = tournaments;
        }

        private void TournamentsListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (TournamentsListView.SelectedItem is Tournament selectedTournament)
            {
                bool isUpcomingTournament = selectedTournament.date >= DateTime.Now; 
                if (isUpcomingTournament)
                {
                    GuiManager.OpenContent(new UCUpcomingTournamentDetails(selectedTournament)); 
                }
                else
                {
                    GuiManager.OpenContent(new UcTournamentProfile(selectedTournament));
                }
            }
        }

    }
}