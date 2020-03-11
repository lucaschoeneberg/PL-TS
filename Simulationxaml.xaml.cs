using System;
using System.Collections.Generic;
using System.Threading;
using System.Windows;
using System.IO.Ports;
using Database;
using Serial.IButton;

namespace PL_TS
{
    /// <summary>
    /// Interaktionslogik für Simulationxaml.xaml
    /// </summary>
    public partial class Simulationxaml : Window
    {
        Dbase data = new Dbase("localhost","Projektlabor", "root", "");
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
            btn_changeIbutton.IsEnabled = false;
            UpdateiButton();
        }

        string COM;
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
        private void UpdateiButton()
        {
            COM = cbx_com.SelectedItem.ToString();
            Thread thread = new Thread(UpdateiButton_Thread);
            thread.Start();
        }

        private void cbx_com_Changed(object sender, RoutedEventArgs e)
        {
            UpdateiButton();
            if (COM != "") btn_changeIbutton.IsEnabled = true; else lbl_iButton.Content = false;
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

        private void btn_check_Click(object sender, RoutedEventArgs e)
        {
            List<string[]> check = new List<string[]>();
            string Button;
            Button= lbl_iButton.Content.ToString();
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
