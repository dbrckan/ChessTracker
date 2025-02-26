using System;
using System.Windows;
using BusinessLogicLayer.Services;

namespace ChessTracker
{
    public partial class AddTournamentWindow : Window
    {
                
        public string TournamentName { get; private set; }
        public DateTime TournamentDate { get; private set; }
        public TimeSpan TournamentTime { get; private set; }
        public string TournamentPlace { get; private set; }
        public string SelectedJudgeId { get; private set; }
        
        public AddTournamentWindow()
        {
            InitializeComponent();
            LoadJudges();
        }

        private void LoadJudges()
        {
            var judgeService = new JudgeService();

            CmbJudges.ItemsSource = judgeService.GetAllJudges();
        }
        
        private void BtnSave_OnClick(object sender, RoutedEventArgs e)
        {
            TournamentName = TxtName.Text;
            TournamentDate = DpDate.SelectedDate.GetValueOrDefault();
            if (TimeSpan.TryParse(TxtTime.Text, out TimeSpan parsedTime))
            {
                TournamentTime = parsedTime; 
            }
            else
            {
                MessageBox.Show("Invalid time format. Please enter in HH:mm format.", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            TournamentPlace = TxtPlace.Text;
            SelectedJudgeId = CmbJudges.SelectedValue?.ToString();

            DialogResult = true;
        }
    }
}