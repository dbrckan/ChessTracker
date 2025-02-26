using BusinessLogicLayer.Services;
using EntitiesLayer;
using EntitiesLayer.Entities;
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
    /// Interaction logic for UcLoadPlayers.xaml
    /// </summary>
    public partial class UcLoadPlayers : UserControl
    {
        private PlayerService playerService = new PlayerService();
        public UcLoadPlayers()
        {
            InitializeComponent();
            DisplayPlayers();
        }

        private void btnAddPlayer_Click(object sender, RoutedEventArgs e)
        {
            GuiManager.OpenContent(new UcAddNewPlayer());
            DisplayPlayers();
        }

        private void btnEditPlayer_Click(object sender, RoutedEventArgs e)
        {
            Player selectedPlayer = dgPlayers.SelectedItem as Player;
            if(selectedPlayer != null)
            {
                UcEditPlayer ucEditPlayer = new UcEditPlayer(selectedPlayer);
                GuiManager.OpenContent(ucEditPlayer);
            }
            else
            {
                txtError.Text = "Molimo odaberite člana za uređivanje.";
            }
            DisplayPlayers();
        }

        private void btnDeactivatePlayer_Click(object sender, RoutedEventArgs e)
        {
            Player selectedPlayer = dgPlayers.SelectedItem as Player;
            if (selectedPlayer != null)
            {
                if (selectedPlayer.status_id == 1)
                {
                    txtError.Text = string.Empty;

                    string message = $"Jeste li ste sigurni da želite deaktivirati člana {selectedPlayer.firstName} {selectedPlayer.lastName}?";

                    MessageBoxResult result = MessageBox.Show(message,
                                                  "Potvrda deaktivacije",
                                                  MessageBoxButton.YesNo,
                                                  MessageBoxImage.Question);

                    if (result == MessageBoxResult.Yes)
                    {
                        selectedPlayer.status_id = 2;
                        var playerService = new PlayerService();
                        string resultMessage;
                        bool isSuccessful = playerService.UpdatePlayer(selectedPlayer, out resultMessage);

                        txtError.Text = resultMessage;

                        if (isSuccessful)
                        {
                            GuiManager.OpenContent(new UcLoadPlayers());
                        }
                    }
                }
                else if (selectedPlayer.status_id == 2)
                {
                    txtError.Text = string.Empty;

                    string message = $"Jeste li ste sigurni da želite aktivirati člana {selectedPlayer.firstName} {selectedPlayer.lastName}?";

                    MessageBoxResult result = MessageBox.Show(message,
                                                  "Potvrda aktivacije",
                                                  MessageBoxButton.YesNo,
                                                  MessageBoxImage.Question);

                    if (result == MessageBoxResult.Yes)
                    {
                        selectedPlayer.status_id = 1;
                        var playerService = new PlayerService();
                        string resultMessage;
                        bool isSuccessful = playerService.UpdatePlayer(selectedPlayer, out resultMessage);

                        txtError.Text = resultMessage;

                        if (isSuccessful)
                        {
                            GuiManager.OpenContent(new UcLoadPlayers());
                        }
                    }
                }
            }
            else
            {
                txtError.Text = "Molimo odaberite člana za deaktivaciju/aktivaciju.";
            }
        }

        private void dgPlayers_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Player selectedPlayer = dgPlayers.SelectedItem as Player;

            if (selectedPlayer != null)
            {
                if (selectedPlayer.status_id == 1)
                {
                    btnDeactivatePlayer.Content = "Deaktiviraj igrača";
                }
                else if (selectedPlayer.status_id == 2)
                {
                    btnDeactivatePlayer.Content = "Aktiviraj igrača";
                }
            }
            else
            {
                btnDeactivatePlayer.Content = "Deaktiviraj igrača";
            }
        }

        private void DisplayPlayers()
        {
            if(CurrentUser.Role == "Klub" && CurrentUser.UserId.HasValue)
            {
                int clubId = CurrentUser.UserId.Value;
                var players = playerService.GetPlayers(clubId);
                dgPlayers.ItemsSource = players;
            }
        }
    }
}
