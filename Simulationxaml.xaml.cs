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
        List<string[]> Maschine = new List<string[]>();
        List<string[]> benutzteMaschine = new List<string[]>();
        string COM;
        public Simulationxaml()
        {
            InitializeComponent();
            Maschine = data.CommandSelectAsListFrom("maschine"); //Auslesen aller Maschine für das Dropdown zum Auswählen einer Simulationsmaschine
            int x = 0;
            while (x < Maschine.Count)
            {
                cb_maschinen.Items.Add(Maschine[x][1]); //Maschine dem Dropdown hinzufügen
                x++;
            }
        }
        private void cbx_com_Loaded(object sender, RoutedEventArgs e)
        {
            string[] ports = SerialPort.GetPortNames();
            foreach (string port in ports)
            {
                cbx_com.Items.Add(port);//Auswahl des Ports für das Auslesen der ButtonID
            }
        }
        private void btn_changeIbutton_Click(object sender, RoutedEventArgs e)
        {
            btn_changeIbutton.IsEnabled = false;
            UpdateiButton();
            benutzteMaschine = data.CommandSelectAsListFrom("zuweisung, maschine, log, ibutton", "WHERE ibutton.iButtonID = zuweisung.iButtonID AND log.iButtonID = ibutton.iButtonID AND log.MaschinenID = maschine.MaschinenID AND maschine.MaschinenID = zuweisung.MaschinenID AND zuweisung.iButtonID = '"+ lbl_iButton.Content + "' AND Endtime IS NULL"); //Auswahl für die Simulation der nicht genutzen Maschinen
            int x = 0;
            while (x < benutzteMaschine.Count)
            {
                cb_endSim.Items.Add(benutzteMaschine[x][4]); //Hinzufügen der Maschinen in die ComboBox
                x++;
            }
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
            try
            {
                List<string[]> check = new List<string[]>();
                string Button;
                Button = lbl_iButton.Content.ToString(); //iButtonID wird aus dem Label entnommen
                check = data.CommandSelectAsListFrom("zuweisung, maschine", "WHERE maschine.MaschinenID = zuweisung.MaschinenID AND zuweisung.iButtonID = '" + Button.Split(';')[0] + "' AND Bezeichnung = '" + cb_maschinen.Text + "' AND maschine.MaschinenID = '" + Button.Split(';')[1] + "'"); //Überprüfung der Zuweisung
                if (check.Count >= 1)
                {
                    data.CommandInsertInto("log", "iButtonID, MaschinenID, Starttime, Endtime", "'" + lbl_iButton.Content + "','" + Button.Split(';')[1] + "',CURRENT_TIMESTAMP,NUll"); //Einfügen eines Log Eintrages (Beginn der Nutzung der Maschine)
                    MessageBox.Show("Die Simulation der Maschine startet.", "Start der Simulation", MessageBoxButton.OK, MessageBoxImage.Information); //Nutzung wird mit einer MessageBox gemeldet
                }
                if (check.Count == 0)
                {
                    MessageBox.Show("Sie sind für die Maschiene nicht berechtigt!", "Nicht Berechtigt", MessageBoxButton.OK, MessageBoxImage.Error); //Zuweisung nicht korrekt
                }
            }
            catch
            {
                MessageBox.Show("Bitte Überprüfen Sie ihre Eingaben.");
            }
        }

        private void Btn_end_Click(object sender, RoutedEventArgs e)
        {
            List<string[]> MaschinenID = new List<string[]>();
            MaschinenID = data.CommandSelectAsListFrom("maschine", "WHERE Bezeichnung='" + cb_endSim.Text + "'"); //Lesen der Maschinentabelle für die Information der MaschinenID
            data.CommandUpdate("log", "Endtime= CURRENT_TIMESTAMP", "iButtonID='" + lbl_iButton.Content + "' AND MaschinenID='" + MaschinenID[0][0] + "' AND Endtime IS NULL"); //Simulaition wird mit einem Eintrag der Endtime beendet
            MessageBox.Show("Die Simulation der Maschine endet.", "Ende der Simulation", MessageBoxButton.OK, MessageBoxImage.Information);
        }
    }
}
