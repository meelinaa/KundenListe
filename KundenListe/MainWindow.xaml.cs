using System.Collections.ObjectModel;
using System.Diagnostics.Metrics;
using System.Text;
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

namespace KundenListe
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            this.DataContext = this;
            InitializeComponent();
        }

        private ObservableCollection<Kunde> kundenListe = new ObservableCollection<Kunde>();

        public ObservableCollection<Kunde> KundenListe
        {
            get { return kundenListe; }
            set { kundenListe = value; }
        }

        // Kunde Löschen
        private void LöschenBTN_Click(object sender, RoutedEventArgs e)
        {
            // Try-Catch bei ObservableCollection nicht nötig, da diese keine Fehler werfen. Dennoch zur Übung an den Stellen richtig
            try
            {
                KundenListe.Remove((Kunde)this.AnzeigeKundenLB.SelectedItem);

                if (KundenListe.Count == 0)
                {
                    ButtonsDeaktivieren();
                }
            }
            catch (Exception error)
            {
                Console.WriteLine(error.Message);
            }
        }

        // Alles Löschen
        private void ClearBTN_Click(object sender, RoutedEventArgs e)
        {
            // Try-Catch bei ObservableCollection nicht nötig, da diese keine Fehler werfen. Dennoch zur Übung an den Stellen richtig
            try
            {
                KundenListe.Clear();
                ButtonsDeaktivieren();
            }
            catch (Exception error)
            {
                Console.WriteLine(error.Message);
            }
        }

        // Kunde im Input anzeigen
        private void AnzeigeKundenLB_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Kunde selectedKunde = (Kunde)this.AnzeigeKundenLB.SelectedItem;
            if (selectedKunde != null)
            {
                String name = selectedKunde.Name;
                String alter = selectedKunde.Alter;
                String stadt = selectedKunde.Stadt;

                this.NameTB.Text = name;
                this.AlterTB.Text = alter;
                this.StadtTB.Text = stadt;
                AktualisierenBTN.IsEnabled = true;
            }
            else 
            {
                InputLeeren();
                AktualisierenBTN.IsEnabled = false;
            }
        }

        // Buttons Aktivieren
        private void ButtonsAktivieren()
        {
            this.LöschenBTN.IsEnabled = true;
            this.ClearBTN.IsEnabled = true;
        }

        // Buttons Deaktivieren
        private void ButtonsDeaktivieren()
        {
            this.LöschenBTN.IsEnabled = false;
            this.ClearBTN.IsEnabled = false;
        }

        // Imput Leeren
        private void InputLeeren()
        {
            this.NameTB.Text = "";
            this.AlterTB.Text = "";
            this.StadtTB.Text = "";
        }

        // Imput Leeren um neuen Kunden anlegen zu können
        private void NeuBTN_Click(object sender, RoutedEventArgs e)
        {
            InputLeeren();
            this.AktualisierenBTN.IsEnabled = false;
        }

        // Neuen Kunden Hinzufügen
        private void HinzufügenBTN_Click(object sender, RoutedEventArgs e)
        {
            String name = (string)this.NameTB.Text;
            String alter = (string)this.AlterTB.Text;
            String stadt = (string)this.StadtTB.Text;

            if (string.IsNullOrWhiteSpace(name) && string.IsNullOrWhiteSpace(stadt) && string.IsNullOrWhiteSpace(alter))
            {
                MessageBox.Show("Du musst schon was eingeben");
                Console.WriteLine("Du musst schon was eingeben");
            }
            else if (string.IsNullOrWhiteSpace(name))
            {
                MessageBox.Show("Der Name darf nicht leer sein");
                Console.WriteLine("Der Name darf nicht leer sein");
            }
            else if (string.IsNullOrWhiteSpace(stadt))
            {
                MessageBox.Show("Die Stadt darf nicht leer sein");
                Console.WriteLine("Die Stadt darf nicht leer sein");
            }
            else if (string.IsNullOrWhiteSpace(alter))
            {
                MessageBox.Show("Das Alter darf nicht leer sein");
                Console.WriteLine("Das Alter darf nicht leer sein");
            }
            else
            {
                Kunde kunde = new(name, alter, stadt);
                KundenListe.Add(kunde);

                if (this.LöschenBTN.IsEnabled == false)
                {
                    ButtonsAktivieren();
                }

                InputLeeren();
            }
        }

        // Kunden Aktualisieren
        private void AktualisierenBTN_Click(object sender, RoutedEventArgs e)
        {
            String name = (string)this.NameTB.Text;
            String alter = (string)this.AlterTB.Text;
            String stadt = (string)this.StadtTB.Text;

            if (string.IsNullOrWhiteSpace(name))
            {
                MessageBox.Show("Der Name darf nicht leer sein");
                Console.WriteLine("Der Name darf nicht leer sein");
            }
            else if (string.IsNullOrWhiteSpace(stadt))
            {
                MessageBox.Show("Die Stadt darf nicht leer sein");
                Console.WriteLine("Die Stadt darf nicht leer sein");
            }
            else if (string.IsNullOrWhiteSpace(alter))
            {
                MessageBox.Show("Das Alter darf nicht leer sein");
                Console.WriteLine("Das Alter darf nicht leer sein");
            }
            else if (AnzeigeKundenLB.SelectedItem is Kunde ausgewaehlterKunde)
            {
                ausgewaehlterKunde.Name = name;
                ausgewaehlterKunde.Alter = alter;
                ausgewaehlterKunde.Stadt = stadt;

                AnzeigeKundenLB.Items.Refresh();
                InputLeeren();
                AnzeigeKundenLB.SelectedItem = null;
                AktualisierenBTN.IsEnabled = false;
            }
        }
    }

    // Objekt Kunde
    public class Kunde
    {
        public string Name { get; set; } = "";
        public string Alter { get; set; } = "";
        public string Stadt { get; set; } = "";
        public Kunde(String name, String alter, String stadt) 
        { 
            this.Name = name;
            this.Alter = alter;
            this.Stadt = stadt;
        }
    }

}