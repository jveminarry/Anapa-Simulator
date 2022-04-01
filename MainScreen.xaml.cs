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
using Anapa_Simulator;


namespace Anapa_Simulator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainScreen : Window
    {
        public MainScreen()
        {
            InitializeComponent();
        }

        private void Close_System_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Environment.Exit(0);
        }

        private void StudyMode_MouseDown(object sender, MouseButtonEventArgs e)
        {
            MainWindow studymode = new MainWindow();
            studymode.Show();
            this.Close();
        }

        private void WarMode_MouseDown(object sender, MouseButtonEventArgs e)
        {
            MessageBox.Show("CHẾ ĐỘ NÀY CHỈ THỰC HIỆN KHI MÁY 1 ĐƯỢC THẢ, GIAO DIỆN MÔ PHỎNG TƯƠNG TỰ NHƯ CHẾ ĐỘ HỌC TẬP - CÔNG TÁC !","THÔNG BÁO !",MessageBoxButton.OK,MessageBoxImage.Error);

        }
    }
}
