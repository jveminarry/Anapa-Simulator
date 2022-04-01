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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace Anapa_Simulator
{
    /// <summary>
    /// Interaction logic for SciChartDemo.xaml
    /// </summary>
    public partial class SciChartDemo : Window
    {
        int cnt=0;
        int[] n = new int[300];
        public SciChartDemo()
        {
            InitializeComponent();
            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += timer_Tick;
            timer.Start();
        }
        void timer_Tick(object sender, EventArgs e) //Bộ đếm thời gian (chu kỳ làm việc)  
            //Hàm thời gian
        {
            cnt++;
           

            n[cnt] = cnt;
            lb3.Content = n[cnt].ToString();

            

            n[cnt - 1] = n[cnt];
            n[cnt] = n[cnt + 1];


        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            
           
        }

        private void Window_Loaded_1(object sender, RoutedEventArgs e)
        {
            lb1.Content = cnt.ToString();
            Rectangle obj = new Rectangle();
            obj.Fill = Brushes.Yellow;
            obj.Height = 5;
            obj.Width = 5;
            can1.Children.Add(obj);
            Canvas.SetLeft(obj, 100 + 10 * n[cnt]);
            Canvas.SetTop(obj, 100 + 10 * n[cnt]);
        }
    }
}
