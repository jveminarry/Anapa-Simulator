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
    /// Interaction logic for dieukhienthamso.xaml
    /// </summary>
    public partial class dieukhienthamso : Window
    {
        public dieukhienthamso()
        {
            InitializeComponent();
        }

        MainWindow main = new MainWindow();

        private void apply_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
            ////Tạo âm báo động !!!
            {

                main.Show();
                main.appear = Int32.Parse(appear.Text);
                main.HTM = Math.Round(double.Parse(htm.Text),3);//Hướng mũi tàu
                //Tham số mục tiêu 1
                {
                    main.No1.Text = "1";
                    main.r1 = double.Parse(t11.Text);
                    main.varphi1 = double.Parse(t12.Text);
                    main.n13.Text = t13.Text;
                    main.n14.Text = t14.Text;
                    main.v1 = double.Parse(t15.Text);
                    main.K1 = double.Parse(t16.Text);
                    main.n17.Text = t17.Text;
                    main.n18.Text = t18.Text;
                }

                //Tham số mục tiêu 2
                {
                    main.r2 = double.Parse(t21.Text);
                    main.varphi2 = double.Parse(t22.Text);
                    main.n23.Text = t23.Text;
                    main.n24.Text = t24.Text;
                    main.v2 = double.Parse(t25.Text);
                    main.K2 = double.Parse(t26.Text);
                    main.n27.Text = t27.Text;
                    main.n28.Text = t28.Text;
                }

                //Tham số mục tiêu 3
                {
                    main.r3 = double.Parse(t31.Text);
                    main.varphi3 = double.Parse(t32.Text);
                    main.n33.Text = t33.Text;
                    main.n34.Text = t34.Text;
                    main.v3 = double.Parse(t35.Text);
                    main.K3 = double.Parse(t36.Text);
                    main.n37.Text = t37.Text;
                    main.n38.Text = t38.Text;
                }

                //Tham số mục tiêu 4
                {
                    main.r4 = double.Parse(t41.Text);
                    main.varphi4 = double.Parse(t42.Text);
                    main.n43.Text = t43.Text;
                    main.n44.Text = t44.Text;
                    main.v4 = double.Parse(t45.Text);
                    main.K4 = double.Parse(t46.Text);
                    main.n47.Text = t47.Text;
                    main.n48.Text = t48.Text;
                }

            }

        }

        private void apply_Copy_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
            bandieukhien bdk2 = new bandieukhien();
            bdk2.Show();
        }

    }
}
