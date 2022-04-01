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
    /// Interaction logic for bandieukhien.xaml
    /// </summary>
    public partial class bandieukhien : Window
    {
        public bandieukhien()
        {
            InitializeComponent();
        }

        private void LoginClick(object sender, RoutedEventArgs e)
        {
            string incorrect = "Tên đăng nhập hoặc mật khẩu không chính xác !";
            string correct = "Mật khẩu chính xác ! Xin vui lòng đợi hệ thống !";
            string pleasetext = "Vui lòng nhập đầy đủ thông tin !";
            if (username.Text.Length == 0)
            {
                notice.Content = pleasetext;
            }
            else
            {
                if (username.Text == "admin")
                {
                    if (password.Password == "123456")
                    {
                        notice.Content = correct;
                        dieukhienthamso bdkts = new dieukhienthamso();
                        bdkts.Show();
                        Close();
                    }
                    else
                    {
                        notice.Content = incorrect;
                    }
                }
                else
                {
                    notice.Content = incorrect;
                }
            }
        }

        private void cancelClick(object sender, RoutedEventArgs e)
        {
            Close();
            MainWindow mainwin = new MainWindow();
            mainwin.Show();
            //main.chihuymode.ischecked = false;
            //main.tracthumode.ischecked = true;
            //mainwindow.n11.text = "200";
        }
    }
}
