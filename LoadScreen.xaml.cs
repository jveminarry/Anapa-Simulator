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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Anapa_Simulator
{
    /// <summary>
    /// Interaction logic for LoadScreen.xaml
    /// </summary>
    public partial class LoadScreen : Window
    {
        public LoadScreen()
        {
            InitializeComponent();
        }

        private void loadingbar_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            Duration duration = new Duration(TimeSpan.FromSeconds(5));
            DoubleAnimation dblani = new DoubleAnimation(150, duration, FillBehavior.HoldEnd);
            loadingbar.BeginAnimation(ProgressBar.ValueProperty, dblani);
            if(loadingbar.Value==100)
            {
                new Machine_4().Show();
                this.Close();
            }
        }
    }
}
