using System;
using System.Collections.Generic;
using System.Data.SQLite;
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

namespace dzienniczek
{
    /// <summary>
    /// Logika interakcji dla klasy DodajPrzedmiot.xaml
    /// </summary>
    public partial class DodajPrzedmiot : UserControl
    {
        public DodajPrzedmiot()
        {
            InitializeComponent();
        }

        private void dodajPrzedmiot_Click(object sender, RoutedEventArgs e)
        {
            string nazwa = nazwaPrzedmiotu.Text;
            string lancuchPolaczenia = "DataSource=sample.db;Version=3;";
            if (nazwa != null)
            {
                using SQLiteConnection polaczenie = new SQLiteConnection(lancuchPolaczenia);
                {
                    try
                    {
                        polaczenie.Open();
                        string utworzenieBazy = "CREATE TABLE IF NOT EXISTS przedmioty (id INTEGER PRIMARY KEY AUTOINCREMENT, nazwa TEXT NOT NULL, srednia REAL NOT NULL);";
                        using (SQLiteCommand komenda = new SQLiteCommand(utworzenieBazy, polaczenie))
                        {
                            komenda.ExecuteNonQuery();
                        }
                        string zapytanie = $"INSERT INTO przedmioty (nazwa, srednia) VALUES ('{nazwa}', 0)";
                        using (SQLiteCommand komenda = new SQLiteCommand(zapytanie, polaczenie))
                        {
                            komenda.ExecuteNonQuery();
                            komunikat.Text = "Dodano przedmiot";
                            Window mainWindow = new MainWindow();
                            mainWindow.Show();
                            Application.Current.MainWindow.Close();
                        }
                    }
                    catch (Exception ex)
                    {
                        komunikat.Text = "Błąd";
                    }
                }
            }
            else
            {
                komunikat.Text = "Wprowadź nazwę przedmiotu";
            }
        }
    }
}
