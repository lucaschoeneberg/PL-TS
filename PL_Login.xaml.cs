using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;
using Database;

namespace PL_TS
{
    /// <summary>
    /// Interaktionslogik für PL_Login.xaml
    /// </summary>
    public partial class PL_Login : Window
    {
        Dbase data = new Dbase("localhost","Projektlabor", "root", "");
        public PL_Login()
        { 
            InitializeComponent();
        }

        private void btn_login_Click(object sender, RoutedEventArgs e)
        {
            List<string[]> Logindaten = new List<string[]>();
            Logindaten = data.CommandSelectAsListFrom("user"," WHERE E_Mail='"+tbx_username.Text+"' OR Benutzername='"+tbx_username.Text+"'");
            if(Logindaten.Count!=0)
            {
                if ((tbx_username.Text == Logindaten[0][3] || tbx_username.Text == Logindaten[0][5]) && BCrypt.Net.BCrypt.Verify(tbx_password.Password, Logindaten[0][6]))
                {
                    Main openMain = new Main();
                    this.Visibility = Visibility.Hidden;
                    openMain.ShowDialog();
                }
            }
            else
            {
                MessageBox.Show("Ihre Eingaben sind falsch!");
            }
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                this.DragMove();
        }
    }
}
