using System;
using System.Collections.Generic;
using System.Threading;
using System.Windows;
using Database;
using Serial.IButton;
using System.IO.Ports;
using PL_TS;

namespace PL_TS
{
    /// <summary>
    /// Interaktionslogik für edit_maker.xaml
    /// </summary>
    public partial class edit_maker : Window
    {
        Dbase data = new Dbase("localhost", "projektlabor", "root", "");
        IButton readButton = new IButton();
        string password;
        List<string[]> Userdata = new List<string[]>();
        string _makerID;
        public edit_maker(string makerID)
        {
            InitializeComponent();
            _makerID = makerID;
            Userdata = data.CommandSelectAsListFrom("user", "WHERE UserID='" + makerID + "'");
            tbx_Vorname.Text = Userdata[0][1];
            tbx_Nachname.Text = Userdata[0][2];
            tbx_EMail.Text = Userdata[0][3];
            lbl_readiButton.Content = Userdata[0][7];
            if (Userdata[0][4] == "True")
            {
                chb_Keymember.IsChecked = true;
            }
            else
            {
                chb_Keymember.IsChecked = false;
            }
            tbx_Benutzername.Text = Userdata[0][5];
            password = Userdata[0][6];
            tbx_password.Password = Userdata[0][6];
            tbx_password_verify.Password = Userdata[0][6];
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

        private void Btn_updateMaker_Click(object sender, RoutedEventArgs e)
        {
            switch (chb_Keymember.IsChecked)
            {
                case true:
                    if (tbx_password.Password != password)
                    {
                        if (data.CommandUpdate("user", "Vorname='" + tbx_Vorname.Text + "', Nachname='" + tbx_Nachname.Text + "', E_Mail='" + tbx_EMail.Text + "', Keymember=" + 1 + ", Benutzername='" + tbx_Benutzername.Text + "', Passwort='" + BCrypt.Net.BCrypt.HashPassword(tbx_password.Password) + "', iButtonID='" + lbl_readiButton.Content + "'", "WHERE UserID='" + _makerID + "'"))
                            this.Close();
                    }
                    else if (data.CommandUpdate("user", "Vorname='" + tbx_Vorname.Text + "', Nachname='" + tbx_Nachname.Text + "', E_Mail='" + tbx_EMail.Text + "', Keymember=" + 1 + ", Benutzername='" + tbx_Benutzername.Text + "', iButtonID='" + lbl_readiButton.Content + "'", "WHERE UserID='" + _makerID + "'"))
                        this.Close();
                    break;
                case false:
                    if (data.CommandUpdate("user", "Vorname='" + tbx_Vorname.Text + "', Nachname='" + tbx_Nachname.Text + "', E_Mail='" + tbx_EMail.Text + "', Keymember=" + 0 + ", iButtonID='" + lbl_readiButton.Content + "'", "WHERE UserID='" + _makerID + "'"))
                        this.Close();
                    break;
                default:
                    break;
            }
        }

        private void Chb_Keymember_Checked(object sender, RoutedEventArgs e)
        {
            if (chb_Keymember.IsChecked == true)
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
            if (chb_Keymember.IsChecked == false)
            {
                tbx_Benutzername.Visibility = Visibility.Hidden;
                tbx_password.Visibility = Visibility.Hidden;
                tbx_password_verify.Visibility = Visibility.Hidden;
                lbl_Benutzername.Visibility = Visibility.Hidden;
                lbl_Passwort.Visibility = Visibility.Hidden;
                lbl_Passwort_verify.Visibility = Visibility.Hidden;
            }
        }
        private void check_fill()
        {
            if (chb_Keymember.IsChecked == true)
            {
                if (tbx_Vorname.Text != "" && tbx_Vorname.Text != "" && tbx_Nachname.Text != "" && tbx_Benutzername.Text != "")
                {
                    if (password != tbx_password.Password)
                    {
                        if (tbx_password_verify.Password.Length <= 8)
                        {
                            if (tbx_password_verify.Password == tbx_password.Password)
                                btn_updateMaker.IsEnabled = true;
                            else
                                btn_updateMaker.IsEnabled = false;
                        }
                        else
                            btn_updateMaker.IsEnabled = false;
                    }else
                        btn_updateMaker.IsEnabled = true;
                }
                else
                    btn_updateMaker.IsEnabled = false;
            }
            else
            {
                if (tbx_EMail.Text != "" && tbx_Vorname.Text != "" && tbx_Nachname.Text != "")
                    btn_updateMaker.IsEnabled = true;
                else
                    btn_updateMaker.IsEnabled = false;
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
