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
using System.Windows.Shapes;
using System.Threading;
using Database;
using Serial.IButton;
using System.IO.Ports;

namespace PL_TS
{
    /// <summary>
    /// Interaktionslogik für maschine_zuweisen.xaml
    /// </summary>
    public partial class maschine_zuweisen : Window
    {
        Dbase data = new Dbase("localhost", "projektlabor", "root", ""); //Verbindung zur Datenbank
        List<string[]> zugewiesen = new List<string[]>();
        List<string[]> nichtzugewiesen = new List<string[]>();
        List<string[]> Makersname = new List<string[]>();
        bool user; 
        int UserID;
        string iButtonID;
        public maschine_zuweisen(bool _user, int _UserID)
        {
            InitializeComponent();
            user = _user;
            UserID = _UserID;
            if (user == true) //Auswahl über die Tabelle
            {
                cb_Zuweisungsname.Visibility = Visibility.Hidden;
                lbl_Namensauswahl.Visibility = Visibility.Hidden;
                FillListBox(); //Füllt die ListBoxen
            }
            else //Auswahl über den Reiter "Maschine/Zuweisen"
            {
                cb_Zuweisungsname.Visibility = Visibility.Visible;
                lbl_Namensauswahl.Visibility = Visibility.Visible;
                Makersname=data.CommandSelectAsListFrom("user");
                int x = 0;
                while (x < Makersname.Count)
                {
                    cb_Zuweisungsname.Items.Add(Makersname[x][1] + " " + Makersname[x][2]); //Auswahl der ComboBox mit Vor- und Nachnamen
                    x++;
                }
            }
        }
        private void FillListBox()
        {
            iButtonID = data.CommandSelectAsListFrom("user", "WHERE UserID='" + UserID + "'")[0][7]; //Auslesen der ButtonID des Users
            zugewiesen = data.CommandSelectAsListFrom("zuweisung, maschine", "WHERE maschine.MaschinenID=zuweisung.MaschinenID AND IButtonID='" + iButtonID + "'"); //Zugewiesene Maschinen des Users
            int x = 0;
            while (x < zugewiesen.Count)
            {
                lBxZugewiesen.Items.Add(zugewiesen[x][4]); //Füllen der zugewiesenen Maschinen (Rechte ListBox)
                x++;
            }
            nichtzugewiesen = data.CommandSelectAsListFrom("maschine", "WHERE bezeichnung NOT IN(SELECT bezeichnung FROM zuweisung, maschine WHERE zuweisung.MaschinenID = maschine.MaschinenID AND iButtonID = '" + iButtonID + "')"); //Suchen der nicht zugewiesenen Maschinen
            x = 0;
            while (x < nichtzugewiesen.Count)
            {
                lBxMaschine.Items.Add(nichtzugewiesen[x][1]); //Füllen der nicht zugewiesenen Maschinen (Linke ListBox)
                x++;
            }
        }

        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            string maschine = ChangeZuweisung(lBxMaschine, lBxZugewiesen); //Zuweisung wird hinzugefügt (Verschieben des Items von der rechten ListBox in die linke ListBox
            data.CommandInsertInto("zuweisung", "iButtonID, MaschinenID, Datum", "'" + iButtonID + "','" + sucheMaschinenID(maschine) + "',CURRENT_DATE"); //Einfügen der Zuweisung in die Datenbank mit dem heutigem Datum
        }
        private string ChangeZuweisung(ListBox from, ListBox to)
        {
            var selected = from.SelectedItem;
            //if (selected is null) return;
            int index = from.SelectedIndex;
            from.Items.RemoveAt(index); //Entfernen des Ausgewählten Items aus der ausgewählten Listbox
            to.Items.Add(selected); //Hinzufügen der gegenüberliegenen ListBox
            if (index >= from.Items.Count) index--; //Herunterzählen des Indexcounters der ausgewählten ListBox
            from.SelectedIndex = index; //Setzen des Index 
            return selected.ToString(); //Rückgabe des Maschinennames
        }
        private void BtnRemove_Click(object sender, RoutedEventArgs e)
        {
            string maschine = ChangeZuweisung(lBxZugewiesen, lBxMaschine); //Zuweisung wird entfernt
            data.CommandDelete("zuweisung", "iButtonID='" + iButtonID + "' AND MaschinenID='" + sucheMaschinenID(maschine) + "'"); //Entfernen der Zuweisung aus der Datenbank
        }
        private string sucheMaschinenID(string maschine) //Suchen der MaschinenID (wird in der Methode BtnRemove_Click und BtnAdd_Click verwendet)
        {
            return data.CommandSelectAsListFrom("maschine", "WHERE bezeichnung='" + maschine + "'")[0][0]; //Suchen der ID des Maschinennames
        }

        private void Cb_Zuweisungsname_SelectionChanged(object sender, SelectionChangedEventArgs e) //Wenn der ausgewählte Name in der ComboBox geändert wird
        {
            UserID =Convert.ToInt16(data.CommandSelectAsListFrom("user","WHERE Vorname='"+ cb_Zuweisungsname.SelectedItem.ToString().Split(' ')[0] + "' AND Nachname='"+ cb_Zuweisungsname.SelectedItem.ToString().Split(' ')[1] + "'")[0][0]); //Suchen der UserID mit Hilfe des Names aus der ComboBox
            FillListBox(); //Füllen der ListBoxen
        }
    }
}
