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

//Autor: David Brckan

namespace ChessTracker
{

    public partial class UcDodajKlub : UserControl
    {
        private readonly ClubService _clubService;
        private readonly ChessFederationService _federationService;

        public UcDodajKlub()
        {
            InitializeComponent();
            _clubService = new ClubService();
            _federationService = new ChessFederationService();
            LoadFederations();
        }

        private void LoadFederations()
        {
            List<ChessFederation> federations = _federationService.GetAllFederations();
            cbFederation.ItemsSource = federations;
            cbFederation.DisplayMemberPath = "name";
            cbFederation.SelectedValuePath = "federation_id";
        }

        private void btnSaveClub_Click(object sender, RoutedEventArgs e)
        {
            lblErrorMessage.Visibility = Visibility.Collapsed;

            if (string.IsNullOrWhiteSpace(txtClubName.Text) ||
                string.IsNullOrWhiteSpace(txtAddress.Text) ||
                string.IsNullOrWhiteSpace(txtContact.Text) ||
                string.IsNullOrWhiteSpace(txtUsername.Text) ||
                string.IsNullOrWhiteSpace(pbPassword.Password) ||
                cbFederation.SelectedValue == null ||
                dpEstablishedDate.SelectedDate == null)
            {
                ShowErrorMessage("Sva polja moraju biti popunjena!");
                return;
            }

            var newClub = new Club
            {
                name = txtClubName.Text,
                address = txtAddress.Text,
                contact = txtContact.Text,
                username = txtUsername.Text,
                password = _clubService.HashPassword(pbPassword.Password),
                dateOfEstablishment = dpEstablishedDate.SelectedDate.Value,
                federation_id = (int)cbFederation.SelectedValue
            };

            bool isSuccess = _clubService.AddClub(newClub, out string message);
            if (isSuccess)
            {
                lblErrorMessage.Foreground = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Colors.Green);
                lblErrorMessage.Content = "Klub uspješno dodan!";
                lblErrorMessage.Visibility = Visibility.Visible;
            }
            else
            {
                ShowErrorMessage(message);
            }
        }

        private void ShowErrorMessage(string message)
        {
            lblErrorMessage.Foreground = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Colors.Red);
            lblErrorMessage.Content = message;
            lblErrorMessage.Visibility = Visibility.Visible;
        }
    }
}
