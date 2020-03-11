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

        private void Chb_Keymember_Click(object sender, RoutedEventArgs e)
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

        private void Btn_addMaker_Click(object sender, RoutedEventArgs e)
        {
            if(tbx_Vorname.Text!="" || tbx_Nachname.Text!="" || tbx_EMail.Text != "")
            {
                IButton readButton = new IButton();
                Dbase data = new Dbase("localhost","projektlabor","root","");
                string ID = readButton.read_IDs();
                lbl_iButton.Content = ID.Split(';')[0];
                data.CommandInsertInto("ibutton", "iButtonID, Typ", "'" + ID.Split(';')[0] + "','DS1990A'");
                if (chb_Keymember.IsChecked == true)
                {
                    if (tbx_Benutzername.Text != "" || tbx_password.Password != "" || tbx_password_verify.Password != "" && tbx_password.Password==tbx_password_verify.Password)
                    {
                        data.CommandInsertInto("user", "Vorname, Nachname, E_Mail, Keymember, Benutzername, Passwort, iButtonID", "'" + tbx_Vorname.Text + "','" + tbx_Nachname.Text + "','" + tbx_EMail.Text + "'," + 1 + ",'" +tbx_Benutzername.Text+"','"+tbx_password.Password+"','"+ ID.Split(';')[0] + "'");
                    }
                }
                else
                {
                    data.CommandInsertInto("user", "Vorname, Nachname, E_Mail, Keymember, iButtonID", "'" + tbx_Vorname.Text + "','" + tbx_Nachname.Text + "','"+tbx_EMail.Text+"',"+0+",'"+ID.Split(';')[0]+"'");
                    
                }
            }
        }
    }
}
