using EntitiesLayer;
using BusinessLogicLayer.Services;
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
   
    public partial class UcDodajSuca : UserControl
    {
        private readonly JudgeService _judgeService;
        private readonly ChessFederationService _federationService;

        public UcDodajSuca()
        {
            InitializeComponent();
            _judgeService = new JudgeService();
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

        private void btnSaveJudge_Click(object sender, RoutedEventArgs e)
        {
            lblErrorMessage.Visibility = Visibility.Collapsed;

            if (string.IsNullOrWhiteSpace(txtFirstName.Text) ||
                string.IsNullOrWhiteSpace(txtLastName.Text) ||
                string.IsNullOrWhiteSpace(txtContact.Text) ||
                string.IsNullOrWhiteSpace(txtUsername.Text) ||
                string.IsNullOrWhiteSpace(pbPassword.Password) ||
                cbFederation.SelectedValue == null)
            {
                ShowMessage("Sva polja moraju biti popunjena!", false);
                return;
            }

            var newJudge = new Judge
            {
                firstName = txtFirstName.Text,
                lastName = txtLastName.Text,
                contact = txtContact.Text,
                username = txtUsername.Text,
                password = _judgeService.HashPassword(pbPassword.Password),
                federation_id = (int)cbFederation.SelectedValue
            };

            bool isSuccess = _judgeService.AddJudge(newJudge, out string message);
            ShowMessage(message, isSuccess);
        }

        private void ShowMessage(string message, bool isSuccess)
        {
            lblErrorMessage.Foreground = new System.Windows.Media.SolidColorBrush(
                isSuccess ? System.Windows.Media.Colors.Green : System.Windows.Media.Colors.Red);
            lblErrorMessage.Content = message;
            lblErrorMessage.Visibility = Visibility.Visible;
        }
    }

}
