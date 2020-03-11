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
    /// Interaktionslogik für edit_maker.xaml
    /// </summary>
    public partial class edit_maker : Window
    {
        Dbase data = new Dbase("localhost", "projektlabor", "root", "");
        IButton readButton = new IButton();
        List<string[]> Userdata = new List<string[]>();
        string _makerID;
        public edit_maker(string makerID)
        {
            InitializeComponent();
            _makerID = makerID;
            Userdata=data.CommandSelectAsListFrom("user","WHERE UserID='"+makerID+"'");
            tbx_Vorname.Text = Userdata[0][1];
            tbx_Nachname.Text = Userdata[0][2];
            tbx_EMail.Text = Userdata[0][3];
            lbl_readiButton.Content = Userdata[0][7];
            if (Userdata[0][4] == "True")
            {
                chb_Keymember.IsChecked=true;
                tbx_Benutzername.Text = Userdata[0][5];
                tbx_password.Password = Userdata[0][6];
                tbx_password_verify.Password = Userdata[0][6];
            }
            else
            {
                chb_Keymember.IsChecked = false;
            }
        }

        private void Chb_Keymember_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void Btn_addMaker_Click(object sender, RoutedEventArgs e)
        {
            if (chb_Keymember.IsChecked == true)
            {
                data.CommandUpdate("user", "Vorname='" + tbx_Vorname.Text + "', Nachname='" + tbx_Nachname.Text + "', E_Mail='" + tbx_EMail.Text + "', Keymember=" + 1 + ", Benutzername='" + tbx_Benutzername.Text + "', Passwort='" + tbx_password.Password + "', iButtonID='" + lbl_readiButton.Content + "'", "WHERE UserID='" + _makerID+"'");

            }
            if (chb_Keymember.IsChecked == false)
            {
                data.CommandUpdate("user", "Vorname='" + tbx_Vorname.Text + "', Nachname='" + tbx_Nachname.Text + "', E_Mail='" + tbx_EMail.Text + "', Keymember=" + 0 + ", iButtonID='" + lbl_readiButton.Content + "'", "WHERE UserID='" + _makerID+"'");
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
    }
}
