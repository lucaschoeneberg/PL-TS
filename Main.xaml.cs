﻿using System;
using System.Data;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using MySql.Data.MySqlClient;
using System.Configuration;

namespace PL_TS
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class Main : Window
    {
        public Main()
        {
            InitializeComponent();
        }

        private void dg_maker_Loaded(object sender, RoutedEventArgs e)
        {
            string sql = "SELECT * FROM user;";

            MySqlConnection connection = new MySqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);
            MySqlCommand cmdSel = new MySqlCommand(sql, connection);
            MySqlDataAdapter adp = new MySqlDataAdapter(cmdSel);
            DataSet ds = new DataSet();
            adp.Fill(ds, "LoadDataBinding");
            dg_maker.DataContext = ds;
        }

        private void dg_maker_Unloaded(object sender, RoutedEventArgs e)
        {

        }
        private void dg_maschine_Loaded(object sender, RoutedEventArgs e)
        {

        }
        private void dg_maschine_Unloaded(object sender, RoutedEventArgs e)
        {

        }
        private void btn_add_maker_Click(object sender, RoutedEventArgs e)
        {
        }
        private void btn_add_maschine_Click(object sender, RoutedEventArgs e)
        {

        }
        private void btn_test_maschine_Click(object sender, RoutedEventArgs e)
        {
            Simulationxaml openSimulation = new Simulationxaml();
            openSimulation.ShowDialog();
        }
    }
}
