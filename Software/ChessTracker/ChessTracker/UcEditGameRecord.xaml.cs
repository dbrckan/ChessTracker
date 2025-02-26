using BusinessLogicLayer.Services;
using DataAccessLayer.Repositories;
using EntitiesLayer;
using Org.BouncyCastle.Asn1.Cmp;
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
    /// Interaction logic for UcEditGameRecord.xaml
    /// </summary>
    public partial class UcEditGameRecord : UserControl
    {
        private Tournament tournament;
        private int recordId;
        private PlayerRepository playerRepository = new PlayerRepository();
        public UcEditGameRecord(int recordId, Tournament tournament)
        {
            InitializeComponent();
            this.recordId = recordId;
            this.tournament = tournament;
            DisplayGameRecord(recordId);
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            string resultMessage;
            var result = txtResult.Text;
            if (string.IsNullOrWhiteSpace(txtResult.Text) || (txtResult.Text != "1" && txtResult.Text != "2" && txtResult.Text != "/"))
            {
                MessageBox.Show("Reultat ne može biti prazan.");
                return;
            }
            var gameRecordService = new GameRecordService();
            var gameRecord = new GameRecord
            {
                record_id = recordId,
                result = result
            };
            bool success = gameRecordService.UpdateGameRecord(gameRecord, out resultMessage );

            if (success)
            {
                gameRecordService.UpdateRatingChanges(gameRecord);
                MessageBox.Show("Rezultat partije je uspješno ažuriran.");
                GuiManager.OpenContent(new UcTournamentProfile(tournament));
            }
            else
            {
                MessageBox.Show("Došlo je do greške prilikom ažuriranja rezultata partije.");
            }
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            GuiManager.OpenContent(new UcTournamentProfile(tournament));
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void DisplayGameRecord(int recordId)
        {
            var gameRecordService = new GameRecordService();
            var record = gameRecordService.GetGameRecordById(recordId);
            txtResult.Text = record.GameResult;
            txtGameId.Text = record.Number.ToString();
            txtPlayers.Text = record.Player1Name + " vs. " + record.Player2Name;

        }
    }
}
