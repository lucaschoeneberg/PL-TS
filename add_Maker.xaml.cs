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
            }
            if (chb_Keymember.IsChecked == false)
            {
                tbx_Benutzername.Visibility = Visibility.Hidden;
                tbx_password.Visibility = Visibility.Hidden;
                tbx_password_verify.Visibility = Visibility.Hidden;
            }
        }
    }
}
