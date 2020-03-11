using System;
using System.Data;
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
using MySql.Data.MySqlClient;
using System.Configuration;

namespace PL_TS
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class Main : Window
    {
        public Main()
        {
            InitializeComponent();
        }

        private void dg_maker_Loaded(object sender, RoutedEventArgs e)
        {
            dg_maker_update();
        }
        public bool dg_maker_update()
        {
            string sql = "SELECT * FROM user;";

            MySqlConnection connection = new MySqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);
            MySqlCommand cmdSel = new MySqlCommand(sql, connection);
            MySqlDataAdapter adp = new MySqlDataAdapter(cmdSel);
            DataSet ds = new DataSet();
            adp.Fill(ds, "LoadDataBinding");
            dg_maker.DataContext = ds;
            btn_add_maker.Content = btn_add_maker.Content + " Bearbeiten";
            return true;
        }

        private void dg_maker_Unloaded(object sender, RoutedEventArgs e)
        {

        }
        private void dg_maschine_Loaded(object sender, RoutedEventArgs e)
        {
            string sql = "SELECT maschine.MaschinenID, Bezeichnung, GROUP_CONCAT(Vorname,' ',Nachname) as User, COUNT(Nachname) as Anzahl FROM maschine, zuweisung, user, ibutton WHERE user.iButtonID=ibutton.iButtonID AND ibutton.iButtonID=zuweisung.iButtonID AND zuweisung.MaschinenID=maschine.MaschinenID GROUP BY maschine.MaschinenID";

            MySqlConnection connection = new MySqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);
            MySqlCommand cmdSel = new MySqlCommand(sql, connection);
            MySqlDataAdapter adp = new MySqlDataAdapter(cmdSel);
            DataSet ds = new DataSet();
            adp.Fill(ds, "LoadDataBinding");
            dg_maschine.DataContext = ds;
        }
        private void dg_maschine_Unloaded(object sender, RoutedEventArgs e)
        {

        }
        private void btn_add_maker_Click(object sender, RoutedEventArgs e)
        {
            add_Maker add_MakerOpen = new add_Maker();
            add_MakerOpen.ShowDialog();
        }
        private void btn_add_maschine_Click(object sender, RoutedEventArgs e)
        {

        }
        private void btn_test_maschine_Click(object sender, RoutedEventArgs e)
        {
            Simulationxaml openSimulation = new Simulationxaml();
            openSimulation.ShowDialog();
        }
        private void btn_maker_edit_Click(object sender, RoutedEventArgs e)
        {
            DataRowView rowview = dg_maker.SelectedItem as DataRowView;
            edit_maker edit_makerOpen = new edit_maker(rowview.Row[0].ToString());
            edit_makerOpen.ShowDialog();
        }
        private void btn_maker_edit_password_Click(object sender, RoutedEventArgs e)
        {

        }
        private void btn_maker_delete_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
