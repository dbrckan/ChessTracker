using System.Windows;
using BusinessLogicLayer.Services;
using EntitiesLayer;

namespace ChessTracker
{
    public partial class UpdateTournamentWindow : Window
    {
        public Tournament Tournament { get; private set; }
        private readonly JudgeService _judgeService = new JudgeService();
        public UpdateTournamentWindow(Tournament tournament)
        {
            InitializeComponent();
            this.DataContext = tournament;
            Tournament = tournament;
            
            LoadJudges();
        }

        private void LoadJudges()
        {
            CmbJudgesUpdate.ItemsSource = _judgeService.GetAllJudges();
        }
        private void BtnSaveUpdate_OnClick(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
            this.Close();
        }
    }
}