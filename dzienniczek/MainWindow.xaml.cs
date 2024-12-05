using System.Data.SQLite;
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

namespace dzienniczek
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            List<string> przedmioty = new List<string>();
            string lancuchPolaczenia = "DataSource=sample.db;Version=3;";
            using SQLiteConnection polaczenie = new SQLiteConnection(lancuchPolaczenia);
            {
                try
                {
                    polaczenie.Open();
                    string wyswietlPrzedmioty = "SELECT * FROM przedmioty;";
                    using (SQLiteCommand komenda = new SQLiteCommand(wyswietlPrzedmioty, polaczenie))
                    {
                        using (SQLiteDataReader reader = komenda.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                while (reader.Read())
                                {
                                    int id = Convert.ToInt32(reader[0]);
                                    string nazwa = Convert.ToString(reader[1]);
                                    double srednia = Convert.ToDouble(reader[2]);
                                    TextBlock textBlock = new TextBlock();
                                    textBlock.Text = nazwa;
                                    textBlock.Foreground = Brushes.Chocolate;
                                    panel.Children.Add(textBlock);
                                    przedmioty.Add(nazwa);
                                }
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    komunikat.Text = "Błąd\n" + ex.Message;
                }
            }
        }

        private void dodawaniePrzedmiotu_Click(object sender, RoutedEventArgs e)
        {
            dodawaniePrzedmiotuPopup.IsOpen = true;
        }

        private void usuwaniePrzedmiotu_Click(object sender, RoutedEventArgs e)
        {
            usuwaniePrzedmiotuPopup.IsOpen = true;
        }
    }
}