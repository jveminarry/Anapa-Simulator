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

namespace Anapa_Simulator
{
    /// <summary>
    /// Interaction logic for Machine_4.xaml
    /// </summary>
    public partial class Machine_4 : Window
    {
        public Machine_4()
        {
            InitializeComponent();
        }

        private void Ellipse_MouseDown(object sender, MouseButtonEventArgs e)
        {
            led_machine_4.Fill = Brushes.LightGreen;
        }

        private void ToggleButton_Checked(object sender, RoutedEventArgs e)
        {
            led_machine_4.Fill = Brushes.LightGreen;
            new MainScreen().Show();

        }

        private void ToggleButton_Unchecked(object sender, RoutedEventArgs e)
        {
            led_machine_4.Fill = Brushes.DarkGreen;
        }
    }
}
