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

namespace PL_TS
{
    /// <summary>
    /// Interaktionslogik für addMaschine.xaml
    /// </summary>
    public partial class addMaschine : Window
    {
        Dbase data = new Dbase("localhost", "projektlabor", "root", "");
        public addMaschine()
        {
            InitializeComponent();
        }

        private void Add_Maschine_Click(object sender, RoutedEventArgs e)
        {
            if (tbx_MaschinenID.Text != "" && tbx_Bezeichnung.Text != "")
            {
                data.CommandInsertInto("maschine", "MaschinenID, Bezeichnung", "'" + tbx_MaschinenID.Text + "','" + tbx_Bezeichnung.Text + "'");
            }
            else
            {
                MessageBox.Show("Bitte überprüfen Sie ihre Eingabe!");
            }
        }
    }
}
