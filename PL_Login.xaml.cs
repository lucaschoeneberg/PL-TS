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
    /// Interaktionslogik für PL_Login.xaml
    /// </summary>
    public partial class PL_Login : Window
    {
        Dbase daten = new Dbase("Projektlabor", "root", "");
        public PL_Login()
        { 
            InitializeComponent();
        }

        private void btn_login_Click(object sender, RoutedEventArgs e)
        {
            List<string[]> Logindaten = new List<string[]>();
            Logindaten = daten.CommandSelectAsListFrom("user"," WHERE E_Mail='"+tbx_username.Text+"' OR Benutzername='"+tbx_username.Text+"' AND Passwort='"+tbx_password.Password+"'");

            if ((tbx_username.Text == Logindaten[3][0] || tbx_username.Text == Logindaten[5][0]) && Logindaten[6][0] == tbx_password.Password)
            {
                string t = "j";
            }
        }
    }
}
