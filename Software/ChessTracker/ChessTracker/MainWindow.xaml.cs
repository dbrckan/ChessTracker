using EntitiesLayer.Entities;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
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
using System.Windows.Shapes;

namespace ChessTracker
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            SetUserInfo();

            TextBlock welcomeMessage = new TextBlock
            {
                Text = "Za opis korištenja aplikacije pritisnite F1 na tipkovnici",
                FontSize = 16,
                Foreground = Brushes.Black,
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center,
                TextWrapping = TextWrapping.Wrap
            };

            contentPanel.Content = welcomeMessage;

            this.KeyDown += MainWindow_KeyDown;
        }

        /*Autor: Nika Antolić*/
        private void MainWindow_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.Key == Key.F1)
            {
                OpenPdf();
                e.Handled = true;
            }
        }

        private void OpenPdf()
        {
            string role = CurrentUser.Role; 
            string pdfFileName = string.Empty;

            switch (role)
            {
                case "Savez":
                    pdfFileName = "FederationDocumentation.pdf"; 
                    break;
                case "Igrač":
                    pdfFileName = "PlayerDocumentation.pdf"; 
                    break;
                case "Klub":
                    pdfFileName = "ClubDocumentation.pdf";
                    break;
                case "Sudac":
                    pdfFileName = "JudgeDocumentation.pdf";
                    break;
                default:
                    MessageBox.Show("Nepoznata uloga korisnika.", "Greška", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
            }

            string pdfPath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Documentation", pdfFileName);

            if (File.Exists(pdfPath))
            {
                Process.Start(new ProcessStartInfo
                {
                    FileName = pdfPath,
                    UseShellExecute = true
                });
            }
            else
            {
                MessageBox.Show("PDF datoteka nije pronađena.", "Greška", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void SetUserInfo()
        {
            lblUsername.Content = CurrentUser.Username;

            SetButtonVisibility();

        }

        private void SetButtonVisibility()
        {
            if (CurrentUser.Role == "Savez")
            {
                btnDodajSuca.IsEnabled = true;
                btnDodajKlub.IsEnabled = true;
                btnLoad.IsEnabled = false;
                btnAllClubsAndPlayers.IsEnabled = true;
                btnPrijavaNaTurnir.IsEnabled = false;
                btnPrijavaTurnirKlub.IsEnabled = false;
                btnPretragaIgracaITurnira.IsEnabled = true;
                btnReports.IsEnabled = true;
                BtnTournamentManagement.IsEnabled = true;
                BtnViewAllTournaments.IsEnabled = true;
            }
            else if (CurrentUser.Role == "Klub")
            {
                btnDodajSuca.IsEnabled = false;
                btnDodajKlub.IsEnabled = false;
                btnLoad.IsEnabled = true;
                btnAllClubsAndPlayers.IsEnabled = true;
                btnPrijavaNaTurnir.IsEnabled = false;
                btnPrijavaTurnirKlub.IsEnabled = true;
                btnPretragaIgracaITurnira.IsEnabled = true;
                btnReports.IsEnabled = false;
                BtnTournamentManagement.IsEnabled = false;
                BtnViewAllTournaments.IsEnabled = false;
            }
            else if(CurrentUser.Role == "Igrač")
            {
                btnDodajSuca.IsEnabled = false;
                btnDodajKlub.IsEnabled = false;
                btnLoad.IsEnabled = false;
                btnAllClubsAndPlayers.IsEnabled = true;
                btnPrijavaNaTurnir.IsEnabled = true;
                btnPrijavaTurnirKlub.IsEnabled = false;
                btnPretragaIgracaITurnira.IsEnabled = true;
                btnReports.IsEnabled = false;
                BtnTournamentManagement.IsEnabled = false;
                BtnViewAllTournaments.IsEnabled = false;
            }
            else if (CurrentUser.Role == "Sudac")
            {
                btnDodajSuca.IsEnabled = false;
                btnDodajKlub.IsEnabled = false;
                btnLoad.IsEnabled = false;
                btnAllClubsAndPlayers.IsEnabled = true;
                btnPrijavaNaTurnir.IsEnabled = false;
                btnPrijavaTurnirKlub.IsEnabled = false;
                btnPretragaIgracaITurnira.IsEnabled = true;
                btnReports.IsEnabled = false;
                BtnTournamentManagement.IsEnabled = false;
                BtnViewAllTournaments.IsEnabled = true;
            }
        }

        private void btnLogout_Click(object sender, RoutedEventArgs e)
        {
            CurrentUser.UserId = null;
            CurrentUser.Role = null;
            CurrentUser.Username = null;


            LoginWindow loginWindow = new LoginWindow();
            loginWindow.Show();
            this.Close();
        }

        private void btnDodajSuca_Click(object sender, RoutedEventArgs e)
        {
            contentPanel.Content = new UcDodajSuca();
        }

        private void btnDodajKlub_Click(object sender, RoutedEventArgs e)
        {
            contentPanel.Content = new UcDodajKlub();
        }
        private void btnLoad_Click(object sender, RoutedEventArgs e)
        {
            contentPanel.Content = new UcLoadPlayers();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            GuiManager.MainWindow = this;
        }

        private void btnAllClubsAndPlayers_Click(object sender, RoutedEventArgs e)
        {
            contentPanel.Content = new UcLoadAllClubsAndPlayers();
        }

        private void btnPrijavaNaTurnir_Click(object sender, RoutedEventArgs e)
        {
            contentPanel.Content = new UcTournamentRegistration();
        }

        private void btnPrijavaTurnirKlub_Click(object sender, RoutedEventArgs e)
        {
            contentPanel.Content = new UcTournamentRegistrationClub();
        }

        private void btnPretragaIgracaITurnira_Click(object sender, RoutedEventArgs e)
        {
            contentPanel.Content = new UcSearchTournamentAndPlayers();
        }

        private void btnReports_Click(object sender, RoutedEventArgs e)
        {
            contentPanel.Content = new UcReport();
        }

        private void btnReports_Click_1(object sender, RoutedEventArgs e)
        {

        }

        private void BtnTournamentManagement_OnClick(object sender, RoutedEventArgs e)
        {
            contentPanel.Content = new UCTournamentManagement();
        }

        private void BtnViewAllTournaments_OnClick(object sender, RoutedEventArgs e)
        {
            contentPanel.Content = new UCTournamentsOverview();
        }
    }
}
