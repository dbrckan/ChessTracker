using BusinessLogicLayer.Services;
using EntitiesLayer;
using EntitiesLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
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
    /// Interaction logic for UcAddNewPlayer.xaml
    /// </summary>
    public partial class UcAddNewPlayer : UserControl
    {
        public UcAddNewPlayer()
        {
            InitializeComponent();
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtFirstName.Text) ||
           string.IsNullOrWhiteSpace(txtLastName.Text) ||
           dpBirthDate.SelectedDate == null ||
           string.IsNullOrWhiteSpace(txtContact.Text) ||
           string.IsNullOrWhiteSpace(txtRating.Text) ||
           string.IsNullOrWhiteSpace(txtUsername.Text) ||
           string.IsNullOrWhiteSpace(pbPassword.Password) ||
           cmbStatus.SelectedValue == null ||
           (!rbMale.IsChecked == true && !rbFemale.IsChecked == true))
            {
                txtErrorMessage.Text = "Molimo ispunite sva polja.";
                txtErrorMessage.Visibility = Visibility.Visible;
                return;
            }

            var player = new Player
            {
                firstName = txtFirstName.Text,
                lastName = txtLastName.Text,
                dateOfBirth = dpBirthDate.SelectedDate.GetValueOrDefault(DateTime.MinValue).Date,
                contact = txtContact.Text,
                rating = decimal.Parse(txtRating.Text),
                gender = rbMale.IsChecked == true ? "Muško" : "Žensko",
                status_id = (int)cmbStatus.SelectedValue,
                username = txtUsername.Text,
                password = HashPassword(pbPassword.Password),
                club_id = (int)CurrentUser.UserId
            };

            var playerService = new PlayerService();
            string resultMessage;
            bool isSuccessful = playerService.AddPlayer(player, out resultMessage);

            if (isSuccessful)
            {
                GuiManager.OpenContent(new UcLoadPlayers());
            }

            txtErrorMessage.Text = resultMessage;
            txtErrorMessage.Visibility = isSuccessful ? Visibility.Collapsed : Visibility.Visible;
        }
        private string HashPassword(string password)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                StringBuilder builder = new StringBuilder();
                foreach (byte b in bytes)
                {
                    builder.Append(b.ToString("x2"));
                }
                return builder.ToString();
            }
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            GuiManager.CloseContent();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            LoadStatuses();
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
