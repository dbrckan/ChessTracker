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

/*Autor: Nika Antolić*/

namespace ChessTracker
{
    /// <summary>
    /// Interaction logic for UcEditPlayer.xaml
    /// </summary>
    public partial class UcEditPlayer : UserControl
    {
        private Player _player;
        public UcEditPlayer(Player player)
        {
            InitializeComponent();
            _player = player;
            LoadStatuses();
            LoadPlayerData();
        }

        private void LoadPlayerData()
        {
            txtFirstName.Text = _player.firstName;
            txtLastName.Text = _player.lastName;
            dpBirthDate.SelectedDate = _player.dateOfBirth;
            txtContact.Text = _player.contact;
            txtRating.Text = _player.rating.ToString();
            if (_player.gender == "Muško")
            {
                rbMale.IsChecked = true;
            }
            else
            {
                rbFemale.IsChecked = true;
            }
            txtUsername.Text = _player.username;
            cmbStatus.SelectedValue = _player.status_id;
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtFirstName.Text) ||
            string.IsNullOrWhiteSpace(txtLastName.Text) ||
            dpBirthDate.SelectedDate == null ||
            string.IsNullOrWhiteSpace(txtContact.Text) ||
            string.IsNullOrWhiteSpace(txtUsername.Text) ||
            cmbStatus.SelectedValue == null ||
            (!rbMale.IsChecked == true && !rbFemale.IsChecked == true))
            {
                txtErrorMessage.Text = "Molimo ispunite sva polja.";
                txtErrorMessage.Visibility = Visibility.Visible;
                return;
            }

            _player.firstName = txtFirstName.Text;
            _player.lastName = txtLastName.Text;
            _player.dateOfBirth = dpBirthDate.SelectedDate.Value;
            _player.contact = txtContact.Text;
            _player.gender = rbMale.IsChecked == true ? "Muško" : "Žensko";
            _player.username = txtUsername.Text;
            _player.status_id = (int)cmbStatus.SelectedValue;

            var playerService = new PlayerService();
            string resultMessage;
            bool isSuccessful = playerService.UpdatePlayer(_player, out resultMessage);

            if (isSuccessful)
            {
                GuiManager.OpenContent(new UcLoadPlayers());
            }

        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            GuiManager.CloseContent();
        }

        private void LoadStatuses()
        {
            var statusServices = new StatusService();
            var statuses = statusServices.GetStatuses();

            cmbStatus.DisplayMemberPath = "name";
            cmbStatus.SelectedValuePath = "status_id";
            cmbStatus.ItemsSource = statuses;
        }
    }
}
