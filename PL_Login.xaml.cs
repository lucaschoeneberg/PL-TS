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

namespace PL_TS
{
    /// <summary>
    /// Interaktionslogik für PL_Login.xaml
    /// </summary>
    public partial class PL_Login : Window
    {
        public PL_Login()
        {
            InitializeComponent();
        }

        private void btn_login_Click(object sender, RoutedEventArgs e)
        {
            /* checkLogin ist eine Methode in der Datenbankklasse
            int check=0;
            List<string> Logindaten = new List<string>();
            MySqlConnection();
            string name = username.Substring(0, 1) + username.Substring(1, username.Length - 1);
            MySqlCommand cmd = new MySqlCommand("SELECT K_ID, K_Vorname, K_Nachname, K_Passwort FROM kunde WHERE K_Vorname LIKE '" + username.Substring(0, 1) + "%' AND K_Nachname='" + username.Substring(1, username.Length - 1) + "' LIMIT 1", conn);
            MySqlDataReader reader = cmd.ExecuteReader();
            try
            {
                reader.Read();
                K_ID = Convert.ToInt16(reader.GetString(0));
                if(username=="Admin" && reader.GetString(3) == passwort)
                    check = 2;
                else if (reader.GetString(3) == passwort)
                    check = 1;

                else
                    MessageBox.Show("Ihr Passwort ist falsch", "Falsches Passwort", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
            }
            catch (MySql.Data.MySqlClient.MySqlException)
            {
                MessageBox.Show("Ihr Username ist falsch", "Falscher Username", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
            }
            MySqlDisconnect();
            return check;
          
            Datenbankoperation Einloggen = new Datenbankoperation();
            int check = Einloggen.checkLogin(txtUsername.Text, txtPasswort.Text);
            if (check == 1)
            {
                F2_iSpeed form = new F2_iSpeed(Einloggen.K_ID);
                Visible = false;
                form.ShowDialog();
            }
            if (check == 2)
            {
                AdminPanel form = new AdminPanel();
                Visible = false;
                form.ShowDialog();
            }
            */
        }
    }
}
