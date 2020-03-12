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
        Dbase data = new Dbase("localhost", "Projektlabor", "root", "");
        public PL_Login()
        {
            InitializeComponent();
        }

        private void btn_login_Click(object sender, RoutedEventArgs e)
        {
            List<string[]> Logindaten = new List<string[]>();
            Logindaten = data.CommandSelectAsListFrom("user", " WHERE E_Mail='" + tbx_username.Text + "' OR Benutzername='" + tbx_username.Text + "'");
            if (Logindaten.Count != 0)
                if (Logindaten[0][4] == "True")
                    if ((tbx_username.Text == Logindaten[0][3] || tbx_username.Text == Logindaten[0][5]) && BCrypt.Net.BCrypt.Verify(tbx_password.Password, Logindaten[0][6]))
                    {
                        Main openMain = new Main();
                        this.Visibility = Visibility.Hidden;
                        openMain.ShowDialog();
                    }
                    else
                        MessageBox.Show("Password oder Benutzername Falsch!");
                else
                    MessageBox.Show("Kein KeyMember mehr!");
            else
                MessageBox.Show("Password und Benutzername eingeben!");
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                this.DragMove();
        }

        private void tbx_password_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                btn_login_Click(this, new RoutedEventArgs());
            }
        }
    }
}
