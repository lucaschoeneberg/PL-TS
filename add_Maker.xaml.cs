using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows;
using System.IO.Ports;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Serial.IButton;
using Database;

namespace PL_TS
{
    /// <summary>
    /// Interaktionslogik für add_Maker.xaml
    /// </summary>
    public partial class add_Maker : Window
    {
        public add_Maker()
        {
            InitializeComponent();
        }

        private void Chb_Keymember_Checked(object sender, RoutedEventArgs e)
        {
            if (chb_Keymember.IsChecked == true) //Nur für Keymembereingaben nötig
            {
                tbx_Benutzername.Visibility = Visibility.Visible;
                tbx_password.Visibility = Visibility.Visible;
                tbx_password_verify.Visibility = Visibility.Visible;
                lbl_Benutzername.Visibility = Visibility.Visible;
                lbl_Passwort.Visibility = Visibility.Visible;
                lbl_Passwort_verify.Visibility = Visibility.Visible;
            }
        }
        private void Chb_Keymember_Unchecked(object sender, RoutedEventArgs e)
        {
            if (chb_Keymember.IsChecked == false) //Einige Felder sind für den Nutzer nicht Sichtbar, weil sie keine Keymemberinfos benötigen
            {
                tbx_Benutzername.Visibility = Visibility.Hidden;
                tbx_password.Visibility = Visibility.Hidden;
                tbx_password_verify.Visibility = Visibility.Hidden;
                lbl_Benutzername.Visibility = Visibility.Hidden;
                lbl_Passwort.Visibility = Visibility.Hidden;
                lbl_Passwort_verify.Visibility = Visibility.Hidden;
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
            IButton readButton = new IButton();
            Thread.Sleep(TimeSpan.FromSeconds(1));
            string IButton = readButton.read_IDs(COM, 115200);
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

        private void Btn_addMaker_Click(object sender, RoutedEventArgs e)
        {
            if (tbx_Vorname.Text != "" || tbx_Nachname.Text != "" || tbx_EMail.Text != "") //Wird nur ausgeführt, wenn die Felder gefüllt sind.
            {
                IButton readButton = new IButton();
                Dbase data = new Dbase("localhost", "projektlabor", "root", "");
                string ID = readButton.read_IDs(cbx_com.SelectedItem.ToString(), 115000);
                lbl_iButton.Content = ID.Split(';')[0];
                data.CommandInsertInto("ibutton", "iButtonID, Typ", "'" + ID.Split(';')[0] + "','DS1990A'");//Einfügen eines nicht vorhandenen iButtons
                if (chb_Keymember.IsChecked == true)
                {
                    if (tbx_Benutzername.Text != "" || tbx_password.Password != "" || tbx_password_verify.Password != "" && tbx_password.Password == tbx_password_verify.Password)
                    {
                        data.CommandInsertInto("user", "Vorname, Nachname, E_Mail, Keymember, Benutzername, Passwort, iButtonID", "'" + tbx_Vorname.Text + "','" + tbx_Nachname.Text + "','" + tbx_EMail.Text + "'," + 1 + ",'" + tbx_Benutzername.Text + "','" + tbx_password.Password + "','" + ID.Split(';')[0] + "'"); //Einfügen eines Keymembers
                    }
                }
                else
                {
                    data.CommandInsertInto("user", "Vorname, Nachname, E_Mail, Keymember, iButtonID", "'" + tbx_Vorname.Text + "','" + tbx_Nachname.Text + "','" + tbx_EMail.Text + "'," + 0 + ",'" + ID.Split(';')[0] + "'"); //Einfügen eines normalen Makers

                }
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
        private void check_fill()
        {
            if (chb_Keymember.IsChecked == true)
            {
                if (tbx_Vorname.Text != "" && tbx_Vorname.Text != "" && tbx_Nachname.Text != "" && tbx_Benutzername.Text != "")
                {
                    if (tbx_password_verify.Password.Length <= 8)
                    {
                        if (tbx_password_verify.Password == tbx_password.Password)
                            btn_addMaker.IsEnabled = true;
                        else
                            btn_addMaker.IsEnabled = false;
                    }
                    else
                        btn_addMaker.IsEnabled = false;
                }
                else
                    btn_addMaker.IsEnabled = false;
            }
            else
            {
                if (tbx_EMail.Text != "" && tbx_Vorname.Text != "" && tbx_Nachname.Text != "")
                    btn_addMaker.IsEnabled = true;
                else
                    btn_addMaker.IsEnabled = false;
            }
        }
        private void tbx_password_verify_PasswordChanged(object sender, RoutedEventArgs e)
        {
            check_fill();
        }

        private void tbx_Vorname_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            check_fill();
        }

        private void tbx_Nachname_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            check_fill();
        }

        private void tbx_EMail_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            check_fill();
        }

        private void tbx_Benutzername_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            check_fill();
        }

        private void chb_Keymember_Click(object sender, RoutedEventArgs e)
        {
            check_fill();
        }
    }
}
