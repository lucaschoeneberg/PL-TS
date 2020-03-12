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
        Dbase data = new Dbase("localhost", "projektlabor", "root", "");
        List<string[]> zugewiesen = new List<string[]>();
        List<string[]> nichtzugewiesen = new List<string[]>();
        string COM;
        public maschine_zuweisen()
        {
            InitializeComponent();
        }

        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            string maschine=ChangeZuweisung(lBxMaschine, lBxZugewiesen);
            data.CommandInsertInto("zuweisung", "iButtonID, MaschinenID, Datum", "'A4D2E21900EAA100','" + sucheMaschinenID(maschine) + "',CURRENT_DATE");
        }
        private string ChangeZuweisung(ListBox from, ListBox to)
        {
            var selected = from.SelectedItem;
            //if (selected is null) return;
            int index = from.SelectedIndex;
            from.Items.RemoveAt(index);
            to.Items.Add(selected);
            if (index >= from.Items.Count) index--;
            from.SelectedIndex = index;
            return selected.ToString();
        }
        private void BtnRemove_Click(object sender, RoutedEventArgs e)
        {
            string maschine=ChangeZuweisung(lBxZugewiesen, lBxMaschine);
            data.CommandDelete("zuweisung", "iButtonID='A4D2E21900EAA100' AND MaschinenID='" + sucheMaschinenID(maschine) + "'");
        }
        private string sucheMaschinenID(string maschine)
        {
            List<string[]> ID = new List<string[]>();
            ID = data.CommandSelectAsListFrom("maschine", "WHERE bezeichnung='" + maschine + "'");
            return ID[0][0];
        }
        private void cbx_com_Loaded(object sender, RoutedEventArgs e)
        {
            string[] ports = SerialPort.GetPortNames();
            foreach (string port in ports)
            {
                cbx_com.Items.Add(port);
            }
        }

        private void btn_changeIbutton_Click(object sender, RoutedEventArgs e)
        {
            //btn_changeIbutton.IsEnabled = false;
            //UpdateiButton();
            lBxMaschine.Items.Clear();
            lBxZugewiesen.Items.Clear();
            zugewiesen = data.CommandSelectAsListFrom("zuweisung, maschine", "WHERE maschine.MaschinenID=zuweisung.MaschinenID AND IButtonID='A4D2E21900EAA100'");
            int x = 0;
            while (x < zugewiesen.Count)
            {
                lBxZugewiesen.Items.Add(zugewiesen[x][4]);
                x++;
            }
            nichtzugewiesen = data.CommandSelectAsListFrom("maschine", "WHERE bezeichnung NOT IN(SELECT bezeichnung FROM zuweisung, maschine WHERE zuweisung.MaschinenID = maschine.MaschinenID AND iButtonID = 'A4D2E21900EAA100')");
            x = 0;
            while (x < nichtzugewiesen.Count)
            {
                lBxMaschine.Items.Add(nichtzugewiesen[x][1]);
                x++;
            }
        }
        private void cbx_com_DropDownOpened(object sender, EventArgs e)
        {
            cbx_com.Items.Clear();
            string[] ports = SerialPort.GetPortNames();
            foreach (string port in ports)
            {
                cbx_com.Items.Add(port);
            }
        }

        private void cbx_com_Changed(object sender, SelectionChangedEventArgs e)
        {
            UpdateiButton();
            if (COM != "") btn_changeIbutton.IsEnabled = true; else lbl_iButton.Content = false;
        }
        private void UpdateiButton()
        {
            COM = cbx_com.SelectedItem.ToString();
            Thread thread = new Thread(UpdateiButton_Thread);
            thread.Start();
        }
        private void UpdateiButton_Thread()
        {
            string IButton;
            IButton readButton = new IButton();
            Thread.Sleep(TimeSpan.FromSeconds(1));
            try
            {
                IButton = readButton.read_IDs(COM, 115200);
            }
            catch (Exception)
            {
                throw;
            }
            lbl_iButton.Dispatcher.Invoke(new Action(() => lbl_iButton.Content = IButton));
            btn_changeIbutton.Dispatcher.Invoke(new Action(() => btn_changeIbutton.IsEnabled = true));
        }
    }
}
