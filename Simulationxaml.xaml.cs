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
using Database;
using Serial.IButton;

namespace PL_TS
{
    /// <summary>
    /// Interaktionslogik für Simulationxaml.xaml
    /// </summary>
    public partial class Simulationxaml : Window
    {
        Dbase data = new Dbase("Projektlabor", "root", "");
        List<string[]> Maschienen = new List<string[]>();
        public Simulationxaml()
        {
            InitializeComponent();
            Maschienen = data.CommandSelectAsListFrom("maschine");
            int x = 0;
            while (x < Maschienen.Count)
            {
                cb_maschinen.Items.Add(Maschienen[x][1]);
                x++;
            }
        }

        private void btn_check_Click(object sender, RoutedEventArgs e)
        {
            List<string[]> check = new List<string[]>();
            string Button;
            IButton checkIButton = new IButton();
            Button=checkIButton.read_IDs();
            string test= "WHERE maschine.MaschinenID = zuweisung.MaschinenID AND zuweisung.iButtonID = '"+Button.Split(';')[0]+"' AND Bezeichnung = '"+cb_maschinen.Text+"' AND maschine.MaschinenID = '"+Button.Split(';')[1]+"'";
            check =data.CommandSelectAsListFrom("zuweisung, maschine", "WHERE maschine.MaschinenID = zuweisung.MaschinenID AND zuweisung.iButtonID = '"+Button.Split(';')[0]+"' AND Bezeichnung = '"+cb_maschinen.Text+"' AND maschine.MaschinenID = '"+Button.Split(';')[1]+"'");
            if (check.Count >= 1)
            {
                MessageBox.Show("Sie sind für die Maschine berechtigt.","Berechtigt",MessageBoxButton.OK,MessageBoxImage.Information);
            }
            if (check.Count == 0)
            {
                MessageBox.Show("Sie sind für die Maschiene nicht berechtigt!","Nicht Berechtigt",MessageBoxButton.OK,MessageBoxImage.Error);
            }
        }
    }
}
