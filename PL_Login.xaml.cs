﻿using System.Collections.Generic;
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
            Logindaten = data.CommandSelectAsListFrom("user", " WHERE E_Mail='" + tbx_username.Text + "' OR Benutzername='" + tbx_username.Text + "'"); //Anmeldung über die E-Mail und dem Benutzernamen möglich
            if (Logindaten.Count != 0)
                if (Logindaten[0][4] == "True")//Muss ein Keymebmer sein
                    if ((tbx_username.Text == Logindaten[0][3] || tbx_username.Text == Logindaten[0][5]) && BCrypt.Net.BCrypt.Verify(tbx_password.Password, Logindaten[0][6]))//Passwortverschlüsselung
                    {
                        Main openMain = new Main(); //Öffen des AdminPanels
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

        private void Window_MouseDown(object sender, MouseButtonEventArgs e) // Bewegen des Login Fensters durch Mausklick
        {
            if (e.ChangedButton == MouseButton.Left)
                this.DragMove();
        }

        private void tbx_password_KeyDown(object sender, KeyEventArgs e)// Login Enter Event
        {
            if (e.Key == Key.Enter)
            {
                btn_login_Click(this, new RoutedEventArgs());
            }
        }

        private void btn_close_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown(); //Schließen der kompletten Aplication
        }
    }
}
