using System;
using System.Data;
using System.Windows;
using System.Windows.Threading;
using Database;

namespace PL_TS
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class Main : Window
    {
        Dbase data = new Dbase("localhost", "projektlabor", "root", "");
        public Main()
        {
            InitializeComponent();
            DispatcherTimer dispatcherTimer = new System.Windows.Threading.DispatcherTimer();
            dispatcherTimer.Tick += new EventHandler(dispatcherTimer_Tick);
            dispatcherTimer.Interval = new TimeSpan(0, 1, 0);
            dispatcherTimer.Start();
        }

        /// <summary> 
        /// Apply Blur Effect on the window 
        /// </summary> 
        /// <param name="win"></param> 
        private void ApplyEffect(Window win)
        {
            System.Windows.Media.Effects.BlurEffect objBlur =
               new System.Windows.Media.Effects.BlurEffect();
            objBlur.Radius = 4;
            win.Effect = objBlur;
        }
        /// <summary> 
        /// Remove Blur Effects 
        /// </summary> 
        /// <param name="win"></param> 
        private void ClearEffect(Window win)
        {
            win.Effect = null;
        }

        private void dg_maker_Loaded(object sender, RoutedEventArgs e)
        {
            dg_maker_update(); //Füllen der Makerstabelle mit den Eingetragenen Mitglieder
        }

        public bool dg_maker_update()
        {
            dg_maker.DataContext = data.CommandSelectAsDataSet("SELECT * FROM user;", "LoadDataBinding");
            return true;
        }
        public bool dg_maschine_update()//Anzeigen der Tablle unter dem Reiter Maschinen
        {
            string sql = "SELECT maschine.MaschinenID, Bezeichnung, GROUP_CONCAT(Vorname,' ',Nachname) as User, COUNT(Nachname) as Anzahl FROM maschine, zuweisung, user, ibutton WHERE user.iButtonID=ibutton.iButtonID AND ibutton.iButtonID=zuweisung.iButtonID AND zuweisung.MaschinenID=maschine.MaschinenID GROUP BY maschine.MaschinenID";
            dg_maschine.DataContext = data.CommandSelectAsDataSet(sql, "LoadDataBinding");
            return true;
        }

        private void dispatcherTimer_Tick(object sender, EventArgs e)
        {
            dg_maker_update();
            dg_maschine_update();
        }

        private void dg_maschine_Loaded(object sender, RoutedEventArgs e)
        {
            dg_maschine_update();
        }


        private void btn_add_maker_Click(object sender, RoutedEventArgs e)//Button unter dem Reiter Maker//Add
        {
            add_Maker add_MakerOpen = new add_Maker();
            ApplyEffect(this);
            add_MakerOpen.ShowDialog();//Öffen der Form zum Hinzufügen eines Makers
            ClearEffect(this);
        }

        private void btn_add_maschine_Click(object sender, RoutedEventArgs e)
        {
            addMaschine addMaschineOpen = new addMaschine();
            addMaschineOpen.ShowDialog();//Öffen der Form zum Hinzufügen einer Maschine
        }

        private void btn_test_maschine_Click(object sender, RoutedEventArgs e)
        {
            Simulationxaml openSimulation = new Simulationxaml();
            ApplyEffect(this);
            openSimulation.ShowDialog();//Öffen der Simulation für die Maschinen
            dg_maker_update();
            dg_maschine_update();
            ClearEffect(this);
        }

        private void btn_maker_edit_Click(object sender, RoutedEventArgs e)
        {
            DataRowView rowview = dg_maker.SelectedItem as DataRowView;
            edit_maker edit_makerOpen = new edit_maker(rowview.Row[0].ToString());//Übergabe der UserID
            ApplyEffect(this);
            edit_makerOpen.ShowDialog();//Öffen einer Form zum ändern von Angaben eines Makers
            dg_maker_update();
            dg_maschine_update();
            ClearEffect(this);
        }

        private void btn_maker_delete_Click(object sender, RoutedEventArgs e)
        {
            DataRowView rowview = dg_maker.SelectedItem as DataRowView;
            data.CommandDelete("user", "UserID=" + Convert.ToInt32(rowview.Row[0].ToString()));//Löschen eines Makers
        }
        private void btn_maker_maschine_Click(object sender, RoutedEventArgs e)
        {
            DataRowView rowview = dg_maker.SelectedItem as DataRowView;
            maschine_zuweisen maschine_makerOpen = new maschine_zuweisen(true, Convert.ToInt32(rowview.Row[0].ToString()));//Zuweisen einer Maschine. Auswahl über die Tabelle (Übergabe der UserID)
            ApplyEffect(this);
            maschine_makerOpen.ShowDialog(); //Öffen der Form zur Zuweisung der Maschinen
            dg_maker_update();
            dg_maschine_update();
            ClearEffect(this);
        }

        private void Btn_maschine_zuweisen_Click(object sender, RoutedEventArgs e)//Button unter dem Reiter Maschine/Zuweisen
        {
            maschine_zuweisen zuweisen = new maschine_zuweisen(false,-1); //Auswahl nicht über die Tabelle. Es erscheint ein Dropdown zum Auswähl des Makers mit dem Namen. Als ID wird -1 übergeben, damit klar wird, dass es sich hier um keine echte UserID handelt(negative IDs in der Datenbank machen keinen Sinn. Die -1 wird in der Form nicht beachtet und später überschrieben).
            ApplyEffect(this);
            zuweisen.ShowDialog();
            dg_maker_update();
            dg_maschine_update();
            ClearEffect(this);
        }

        private void PL_MAIN_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}
