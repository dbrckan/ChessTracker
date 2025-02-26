using BusinessLogicLayer.Services;
using EntitiesLayer;

using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml.Linq;

/*Autor: Nika Antolić*/

namespace ChessTracker
{
    /// <summary>
    /// Interaction logic for UcReport.xaml
    /// </summary>
    public partial class UcReport : UserControl
    {
        public UcReport()
        {
            InitializeComponent();
        }

        private void InitializeReportType()
        {
            foreach (ReportType reportType in Enum.GetValues(typeof(ReportType)))
            {
                ComboBoxItem item = new ComboBoxItem
                {
                    Content = GetReportTypeDisplayName(reportType)
                };
                cmbTypeReport.Items.Add(item);
            }
        }

        private string GetReportTypeDisplayName(ReportType reportType)
        {
            switch (reportType)
            {
                case ReportType.PlayerStatistic:
                    return "Statistika igrača";
                case ReportType.ClubStatistic:
                    return "Statistika kluba";
                case ReportType.RegisteredPlayers:
                    return "Registrirani korisnici";
                default:
                    return reportType.ToString();
            }
        }

        private void btnCreateReport_Click(object sender, RoutedEventArgs e)
        {
            if (cmbTypeReport.SelectedItem != null)
            {
                ReportType reportType = (ReportType)(cmbTypeReport.SelectedIndex);
                txtError.Text = string.Empty;

                ClubService clubServices = new ClubService();
                var clubs = clubServices.GetClubStatisticsReport();

                PlayerService playerServices = new PlayerService();

                switch (reportType)
                {
                    case ReportType.ClubStatistic:
                        var formattedClubs = clubs.Select(club => new
                        {
                            Name = club.Name,
                            FoundedDate = club.FoundedDate.ToString("dd.MM.yyyy"),
                            Address = club.Address,
                            ActivePlayersCount = club.ActivePlayersCount,
                            InactivePlayersCount = club.InactivePlayersCount
                        }).ToList();

                        dgReports.ItemsSource = formattedClubs;

                        dgReports.Columns[0].Header = "Naziv kluba";
                        dgReports.Columns[1].Header = "Datum osnivanja";
                        dgReports.Columns[2].Header = "Adresa";
                        dgReports.Columns[3].Header = "Aktivni igrači";
                        dgReports.Columns[4].Header = "Neaktivni igrači";
                        break;

                    case ReportType.PlayerStatistic:
                        var playerStatistics = playerServices.GetAllPlayerStatistic();
                        dgReports.ItemsSource = playerStatistics;
                        dgReports.Columns[0].Header = "Ime i prezime";
                        dgReports.Columns[1].Header = "Rating";
                        dgReports.Columns[2].Header = "Broj odigranih partija";
                        break;

                    case ReportType.RegisteredPlayers:
                        var clubs2 = clubServices.GetAllClubs();
                        var allPlayerStatistics = new List<RegisteredPlayerStatistic>();

                        foreach (var club in clubs2)
                        {
                            var playerStatistics2 = playerServices.GetRegisteredPlayerStatistics(club.club_id);
                            allPlayerStatistics.AddRange(playerStatistics2);
                        }

                        dgReports.ItemsSource = allPlayerStatistics;
                        dgReports.Columns[0].Header = "Ime";
                        dgReports.Columns[1].Header = "Prezime";
                        dgReports.Columns[2].Header = "Rating";
                        dgReports.Columns[3].Header = "Klub";
                        break;

                    default:
                        txtError.Text = "Nepoznat tip izveštaja.";
                        break;
                }

            }
            else
            {
                txtError.Text = "Molimo odaberite vrstu izvješća.";
            }
        }

        private void btnSaveReport_Click(object sender, RoutedEventArgs e)
        {
            if (dgReports.ItemsSource != null)
            {
                ReportType reportType = GetCurrentReportType();

                string filePath;
                string reportTitle;

                switch (reportType)
                {
                    case ReportType.RegisteredPlayers:
                        filePath = "Izvjestaj_O_Registriranim_Igracima.pdf";
                        reportTitle = "Izvještaj o registriranim igracima";
                        break;

                    case ReportType.ClubStatistic:
                        filePath = "Izvjestaj_O_Klubovima.pdf";
                        reportTitle = "Izvještaj o statistikama kluba";
                        break;

                    case ReportType.PlayerStatistic:
                        filePath = "Izvjestaj_O_Statistici_Igraca.pdf";
                        reportTitle = "Izvještaj o statistikama igraca";
                        break;

                    default:
                        filePath = "Izvjestaj.pdf";
                        reportTitle = "Izvještaj";
                        break;
                }

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    var document = new Document();
                    PdfWriter.GetInstance(document, stream);
                    document.Open();

                    document.Add(new iTextSharp.text.Paragraph(reportTitle));
                    document.Add(new iTextSharp.text.Paragraph(" "));

                    PdfPTable table;

                    switch (reportType)
                    {
                        case ReportType.ClubStatistic:
                            table = new PdfPTable(5);
                            table.AddCell("Naziv kluba");
                            table.AddCell("Datum osnivanja");
                            table.AddCell("Adresa");
                            table.AddCell("Aktivni igraci");
                            table.AddCell("Neaktivni igraci");
                            break;

                        case ReportType.PlayerStatistic:
                            table = new PdfPTable(3);
                            table.AddCell("Ime i prezime");
                            table.AddCell("Rating");
                            table.AddCell("Broj odigranih partija");
                            break;

                        case ReportType.RegisteredPlayers:
                            table = new PdfPTable(4);
                            table.AddCell("Ime");
                            table.AddCell("Prezime");
                            table.AddCell("Rating");
                            table.AddCell("Klub");
                            break;


                        default:
                            table = new PdfPTable(1);
                            table.AddCell("Podaci");
                            break;
                    }

                    foreach (var item in dgReports.ItemsSource)
                    {
                        var itemData = item.GetType().GetProperties();
                        foreach (var property in itemData)
                        {
                            table.AddCell(property.GetValue(item)?.ToString());
                        }
                    }

                    document.Add(table);
                    document.Close();
                }

                MessageBox.Show($"Izvještaj je sačuvan kao PDF: {filePath}", "Uspjeh", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                MessageBox.Show("Nema podataka za spremanje.", "Greška", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        private ReportType GetCurrentReportType()
        {
            if (cmbTypeReport.SelectedItem is ComboBoxItem selectedItem)
            {
                return (ReportType)cmbTypeReport.Items.IndexOf(selectedItem);
            }
            throw new InvalidOperationException("Nema izabrane stavke.");
        }
    }
}
