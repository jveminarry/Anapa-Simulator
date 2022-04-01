using System;
using System.Windows;
using System.Windows.Media;
using System.Windows.Threading;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Shapes;
using Anapa_Simulator;


namespace Anapa_Simulator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    /// 
    public partial class MainWindow : Window
    {
        //Các biến sử dụng 
        //Biến thời gian và hiển thị LED
        private MediaPlayer mediaPlayer = new MediaPlayer();
        private TimeSpan wrktime = new TimeSpan(0);
        private int counter = 0;
        private bool isEnabled = false;
        private bool check = false;

        //Biến tham số tàu mang
        public double ratio; //Hệ số tỉ lệ cự ly
        public double HTM = 1; //Hướng mũi tàu
        public double A; //Lượng trôi dạt
        Random rand = new Random(); //Tạo lượng trôi dạt ngẫu nhiên
        private double L = 1;//Bù trôi dạt

        //Biến điều khiển vòng bám
        public int appear = -100;
        public int entrack = 0, notrack = 1;

        //Biến điều khiển tắt/hiện mục tiêu
        public bool obj_enb = false;

        //Biến tham số mục tiêu 1
        public double cx1 = 0, cy1 = 0, r1, varphi1, K1, v1, po1;
        public int ky1 = 1, kx1 = 0;

        //Biến tham số mục tiêu 2
        public double cx2 = 0, cy2 = 0, r2, varphi2, K2, v2, po2;
        public int ky2 = 1, kx2 = 0;

        //Biến tham số mục tiêu 3
        public double cx3 = 0, cy3 = 0, r3, varphi3, K3, v3, po3;
        public int ky3 = 1, kx3 = 0;

        //Biến tham số mục tiêu 4
        public double cx4 = 0, cy4 = 0, r4, varphi4, K4, v4, po4;
        public int ky4 = 1, kx4 = 0;

        // Create 2 UI container
        private IKO ikoContainer;
        private SquareView squareView = new SquareView();

        public MainWindow()
        {
            // Init container
            ikoContainer = new IKO(this);

            //Tạo đồng hồ đếm
            InitializeComponent();
            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += timer_Tick;
            timer.Start();

            //Tắt toàn bộ mục tiêu
            if(obj_enb==false)
            {
                Canvas.SetLeft(ikoContainer.obj, 0); Canvas.SetLeft(ikoContainer.obj2, 0); Canvas.SetLeft(ikoContainer.obj3, 0); Canvas.SetLeft(ikoContainer.obj4, 0);
                Canvas.SetTop(ikoContainer.obj, 0); Canvas.SetTop(ikoContainer.obj2, 0); Canvas.SetTop(ikoContainer.obj3, 0); Canvas.SetTop(ikoContainer.obj4, 0);}

            // Init contents
            Polar.Content = ikoContainer;
        }
        
        void timer_Tick(object sender, EventArgs e) //Bộ đếm thời gian (chu kỳ làm việc)  
        {
            //Hàm thời gian
            {
                Dateandtime.Content = DateTime.Now.ToString("dd.MM.yyyy            HH:mm:ss");//Hiển thị ngày giờ làm việc
                counter++;//biến đếm
                wrktime = new TimeSpan(0, 0, counter);
                Workingtime.Text = wrktime.ToString();//Hiển thị thời gian làm việc
            }

            //Điều khiển hiển thị thông số tàu mang
            {
                ikoContainer.htm1.Text = Math.Round(HTM, 2).ToString() + @"°"; ikoContainer.Kd.Text = Math.Round(A, 2).ToString() + @"°";
                ikoContainer.Vd.Text = Math.Round(-(0.25 * A), 2).ToString() + @" м/с";
            }

            // Mô phỏng chuyển động của nhiễu trong IKO và hướng mũi tàu
            {
                ikoContainer.noise01.Transform = new RotateTransform(A* counter, 300, 300);
                ikoContainer.noise02.Transform = new RotateTransform(-A * counter, 250, 250);
                ikoContainer.noise03.Transform = new RotateTransform(0.2*A * counter, 200, 200);
                ikoContainer.noise04.Transform = new RotateTransform(-0.3*A * counter, 150, 150);
                ikoContainer.noise05.Transform = new RotateTransform(0.4*A* counter, 100, 100);
                ikoContainer.HTM_axis.RenderTransform = new RotateTransform(HTM);
            }

            //Bật các công tắc mặc định
            if (counter == 1)
            {
                r300.IsChecked = true;                pwokl.IsChecked = true;                smallbw.IsChecked = true;
                _0db.IsChecked = true;                noiseotkl.IsChecked = true;            akshotkl.IsChecked = true;
                r300co.IsChecked = true;              razd.IsChecked = true;
            }

            //Tạo âm báo động
            //{
            //    Uri Alarm = new Uri(Environment.CurrentDirectory + @"\alarm.mp3");
            //    var player = new MediaPlayer();
            //    if (ikoContainer.obj_enb == true)
            //    { player.Open(Alarm); player.Play(); }
            //    else { player.Stop(); }
            //}

            if(counter>=appear+2)
            {
                if (counter == appear + 3) { obj_enb = true; }
                //Vẽ và mô phỏng chuyển động mục tiêu 1 
                {   //Vẽ mục tiêu
                    double x1 = r1 * Math.Sin(varphi1 * Math.PI / 180);
                    double y1 = -r1 * Math.Cos(varphi1 * Math.PI / 180);
                    Canvas.SetLeft(ikoContainer.obj, (x1 + cx1) * ratio + 347);
                    Canvas.SetTop(ikoContainer.obj, (y1 + cy1) * ratio + 347);
                    Canvas.SetLeft(ikoContainer.objlb, (x1 + cx1) * ratio + 350);
                    Canvas.SetTop(ikoContainer.objlb, (y1 + cy1) * ratio + 350);

                    //Tính toán lượng dịch chuyển
                    K1 += A;
                    v1 += 0.025 * A;
                    double mx1 = v1 * Math.Sin((K1) * Math.PI / 180);
                    double my1 = -v1 * Math.Cos((K1) * Math.PI / 180);

                    //Hệ số điều chỉnh vị trí hiển thị
                    if ((y1 + cy1) * ratio > 0)  {   ky1 = -1;    kx1 = (x1 + cx1) > 0 ? 1 : -1; }
                    else { kx1 = 0;   ky1 = 1; }

                    //Tọa độ sau khi dịch chuyển
                    double r1a = Math.Abs(Math.Sqrt(((x1 + cx1) * (x1 + cx1) + (y1 + cy1) * (y1 + cy1))));
                    double varphi1a = kx1 * 180 + ky1 * Math.Asin((x1 + cx1) / r1a) * 180 / Math.PI;

                    //Góc mạn
                    po1 = varphi1a - HTM;
                    if (po1 > 180) { po1 -= 360; }
                    if (po1 < -180) { po1 += 360; }
                    n13.Text = Math.Round(po1, 2).ToString() + @"°";

                    //Hiển thị mục tiêu và tham số sau khi dịch chuyển
                    n12.Text = Math.Round(varphi1a, 2).ToString() + @"°";
                    n11.Text = Math.Round(r1a, 2).ToString();
                    n16.Text = Math.Round(K1, 2).ToString() + @"°";
                    n15.Text = Math.Round(v1, 2).ToString();
                    ikoContainer.obj.Fill = Brushes.Orange;
                    No1.Text = "1";
                    ikoContainer.objlb.Foreground = Brushes.Yellow;


                    //Điều khiển bám mục tiêu 1
                    double compareX = Canvas.GetLeft(ikoContainer.obj);
                    double compareY = Canvas.GetTop(ikoContainer.obj);

                    if (entrack == 1) 
                    {   
                        xu2.Text = n11.Text; xu1.Text = No1.Text;xu3.Text = n12.Text; 
                        xu4.Text = n13.Text; xu5.Text = n15.Text;xu6.Text = n16.Text;
                        LEDxu.Fill = new SolidColorBrush(Color.FromRgb(79, 255, 0));
                    }

                    if (Math.Abs(ikoContainer.xsec - compareX) < 3 || Math.Abs(ikoContainer.ysec - compareY) < 3) { notrack = 0; }
                    
                    if (notrack==0)
                    {
                        Canvas.SetLeft(ikoContainer.track1, (x1 + cx1) * ratio + 343);
                        Canvas.SetTop(ikoContainer.track1, (y1 + cy1) * ratio + 343);
                        entrack = 1;
                    }
                    else
                    { entrack = 0;
                        LEDxu.Fill = Brushes.Gray;
                    }

                    //Cộng thêm lượng dịch chuyển
                    cx1 += mx1;
                    cy1 += my1;
                    
                    //Tắt hiển thị mục tiêu khi ra khỏi vòng cự ly
                    if (r1a > 1 / ratio * 300 || r1a < 1)
                    {
                        ikoContainer.obj.Fill = null;
                        ikoContainer.objlb.Foreground = null;
                        n11.Text = ""; n12.Text = "";
                        n13.Text = ""; n14.Text = "";
                        n15.Text = ""; n16.Text = "";
                        n17.Text = ""; n18.Text = "";
                        No1.Text = ""; notrack = 1;
                    }
                }

                //Vẽ và mô phỏng chuyển động mục tiêu 2 
                {   //Vẽ mục tiêu
                    double x2 = r2 * Math.Sin(varphi2 * Math.PI / 180);
                    double y2 = -r2 * Math.Cos(varphi2 * Math.PI / 180);
                    Canvas.SetLeft(ikoContainer.obj2, (x2 + cx2) * ratio + 347);
                    Canvas.SetTop(ikoContainer.obj2, (y2 + cy2) * ratio + 347);
                    Canvas.SetLeft(ikoContainer.obj2lb, (x2 + cx2) * ratio + 350);
                    Canvas.SetTop(ikoContainer.obj2lb, (y2 + cy2) * ratio + 350);

                    //Tính toán lượng dịch chuyển
                    double mx2 = v2 * Math.Sin(K2 * Math.PI / 180);
                    double my2 = -v2 * Math.Cos(K2 * Math.PI / 180);

                    //Hệ số điều chỉnh vị trí hiển thị
                    if ((y2 + cy2) * ratio > 0) { ky2 = -1; kx2 = (x2 + cx2) > 0 ? 1 : -1; }
                    else { kx2 = 0; ky2 = 1; }

                    //Tọa độ sau khi dịch chuyển
                    double r2a = Math.Abs(Math.Sqrt(((x2 + cx2) * (x2 + cx2) + (y2 + cy2) * (y2 + cy2))));
                    double varphi2a = kx2 * 180 + ky2 * Math.Asin((x2 + cx2) / r2a) * 180 / Math.PI;

                    //Góc mạn
                    po2 = varphi2a - HTM;
                    if (po2 > 180) { po2 -= 360; }
                    if (po2 < -180) { po2 += 360; }
                    n23.Text = Math.Round(po2, 2).ToString() + @"°";

                    //Hiển thị mục tiêu và tham số sau khi dịch chuyển
                    n22.Text = Math.Round(varphi2a, 2).ToString() + @"°";
                    n21.Text = Math.Round(r2a, 2).ToString();
                    n26.Text = K2.ToString() + @"°";
                    n25.Text = v2.ToString();
                    No2.Text = "2";
                    ikoContainer.obj2.Fill = Brushes.Orange;
                    ikoContainer.obj2lb.Foreground = Brushes.Yellow;

                    //Cộng thêm lượng dịch chuyển
                    cx2 += mx2;
                    cy2 += my2;

                    //Tắt hiển thị mục tiêu khi ra khỏi vòng cự ly
                    if (r2a > 1 / ratio * 300 || r2a < 1)
                    {
                        ikoContainer.obj2.Fill = null;
                        ikoContainer.obj2lb.Foreground = null;
                        n21.Text = ""; n22.Text = "";
                        n23.Text = ""; n24.Text = "";
                        n25.Text = ""; n26.Text = "";
                        n27.Text = ""; n28.Text = "";
                        No2.Text = "";
                    }
                }

                //Vẽ và mô phỏng chuyển động mục tiêu 3 
                {   //Vẽ mục tiêu
                    double x3 = r3 * Math.Sin(varphi3 * Math.PI / 180);
                    double y3 = -r3 * Math.Cos(varphi3 * Math.PI / 180);
                    Canvas.SetLeft(ikoContainer.obj3, (x3 + cx3) * ratio + 347);
                    Canvas.SetTop(ikoContainer.obj3, (y3 + cy3) * ratio + 347);
                    Canvas.SetLeft(ikoContainer.obj3lb, (x3 + cx3) * ratio + 350);
                    Canvas.SetTop(ikoContainer.obj3lb, (y3 + cy3) * ratio + 350);

                    //Tính toán lượng dịch chuyển
                    double mx3 = v3 * Math.Sin(K3 * Math.PI / 180);
                    double my3 = -v3 * Math.Cos(K3 * Math.PI / 180);

                    //Hệ số điều chỉnh vị trí hiển thị
                    if ((y3 + cy3) * ratio > 0) { ky3 = -1; kx3 = (x3 + cx3) > 0 ? 1 : -1; }
                    else { kx3 = 0; ky3 = 1; }

                    //Tọa độ sau khi dịch chuyển
                    double r3a = Math.Abs(Math.Sqrt(((x3 + cx3) * (x3 + cx3) + (y3 + cy3) * (y3 + cy3))));
                    double varphi3a = kx3 * 180 + ky3 * Math.Asin((x3 + cx3) / r3a) * 180 / Math.PI;

                    //Góc mạn
                    po3 = varphi3a - HTM;
                    if (po3 > 180) { po3 -= 360; }
                    if (po3 < -180) { po3 += 360; }
                    n33.Text = Math.Round(po3, 2).ToString() + @"°";

                    //Hiển thị mục tiêu và tham số sau khi dịch chuyển
                    n32.Text = Math.Round(varphi3a, 2).ToString() + @"°";
                    n31.Text = Math.Round(r3a, 2).ToString();
                    n36.Text = K3.ToString() + @"°";
                    n35.Text = v3.ToString();
                    No3.Text = "3";
                    ikoContainer.obj3.Fill = Brushes.Orange;
                    ikoContainer.obj3lb.Foreground = Brushes.Yellow;

                    //Cộng thêm lượng dịch chuyển
                    cx3 += mx3;
                    cy3 += my3;

                    //Tắt hiển thị mục tiêu khi ra khỏi vòng cự ly
                    if (r3a > 1 / ratio * 300 || r3a < 1)
                    {
                        ikoContainer.obj3.Fill = null;
                        ikoContainer.obj3lb.Foreground = null;
                        n31.Text = ""; n32.Text = "";
                        n33.Text = ""; n34.Text = "";
                        n35.Text = ""; n36.Text = "";
                        n37.Text = ""; n38.Text = "";
                        No3.Text = "";
                    }
                    //if (varphi3 < 0) { po3 = 360 + varphi3a - HTM; }

                }

                //Vẽ và mô phỏng chuyển động mục tiêu 4 
                {   //Vẽ mục tiêu
                    double x4 = r4 * Math.Sin(varphi4 * Math.PI / 180);
                    double y4 = -r4 * Math.Cos(varphi4 * Math.PI / 180);
                    Canvas.SetLeft(ikoContainer.obj4, (x4 + cx4) * ratio + 347);
                    Canvas.SetTop(ikoContainer.obj4, (y4 + cy4) * ratio + 347);
                    Canvas.SetLeft(ikoContainer.obj4lb, (x4 + cx4) * ratio + 350);
                    Canvas.SetTop(ikoContainer.obj4lb, (y4 + cy4) * ratio + 350);

                    //Tính toán lượng dịch chuyển
                    double mx4 = v4 * Math.Sin(K4 * Math.PI / 180);
                    double my4 = -v4 * Math.Cos(K4 * Math.PI / 180);

                    //Hệ số điều chỉnh vị trí hiển thị
                    if ((y4 + cy4) * ratio > 0) { ky4 = -1; kx4 = (x4 + cx4) > 0 ? 1 : -1; }
                    else { kx4 = 0; ky4 = 1; }

                    //Tọa độ sau khi dịch chuyển
                    double r4a = Math.Abs(Math.Sqrt(((x4 + cx4) * (x4 + cx4) + (y4 + cy4) * (y4 + cy4))));
                    double varphi4a = kx4 * 180 + ky4 * Math.Asin((x4 + cx4) / r4a) * 180 / Math.PI;

                    //Góc mạn
                    po4 = varphi4a - HTM;
                    if (po4 > 180) { po4 -= 360; }
                    if (po4 < -180) { po4 += 360; }
                    n43.Text = Math.Round(po4, 2).ToString() + @"°";

                    //Hiển thị mục tiêu và tham số sau khi dịch chuyển
                    n42.Text = Math.Round(varphi4a, 2).ToString() + @"°";
                    n41.Text = Math.Round(r4a, 2).ToString();
                    n46.Text = K4.ToString() + @"°";
                    n45.Text = v4.ToString();
                    ikoContainer.obj4.Fill = Brushes.Orange;
                    ikoContainer.obj4lb.Foreground = Brushes.Yellow;
                    No4.Text = "4";

                    //Cộng thêm lượng dịch chuyển
                    cx4 += mx4;
                    cy4 += my4;

                    //Tắt hiển thị mục tiêu khi ra khỏi vòng cự ly
                    if (r4a > 1 / ratio * 300 || r4a < 1)
                    {
                        ikoContainer.obj4.Fill = null;
                        ikoContainer.obj4lb.Foreground = null;
                        n41.Text = ""; n42.Text = "";
                        n43.Text = ""; n44.Text = "";
                        n45.Text = ""; n46.Text = "";
                        n47.Text = ""; n48.Text = "";
                        No4.Text = "";
                    }

                }
            }

            //Điều khiển LED nhấp nháy chu kỳ làm việc
            {
                if (isEnabled) { LED.Fill = Brushes.Gray; } else { LED.Fill = Brushes.SpringGreen;}
                isEnabled = !isEnabled;
                HTM = HTM + A;
                A = Math.Cos(rand.NextDouble() * 2 * Math.PI)*L;
            }
        }

        //Chọn phần không gian theo góc và cự ly để hiển thị trên СО
        private void SectorClick(object sender, RoutedEventArgs e)
        {
            if (check)
            {
                ledsector.Fill = Brushes.Gray;
            }
            else
            {
                ledsector.Fill = Brushes.SpringGreen;
            }
            check = !check;

        }

        //Chọn điểm chuẩn (mục tiêu) trong chế độ bằng tay АКСН  
        private void ReperClick(object sender, RoutedEventArgs e)
        {
            if (check)
            {
                ledperer.Fill = Brushes.Gray;
            }
            else
            {
                ledperer.Fill = Brushes.SpringGreen;
            }
            check = !check;
        }

        //Khẳng định mục tiêu là ПДСС (khi nhấp chuột phải – chọn lệnh trong cửa sổ bổ sung và xác định mục tiêu là không nguy hiểm)
        private void SoprClick(object sender, RoutedEventArgs e)
        {
            if (check)
            {
                ledsopr.Fill = Brushes.Gray;
            }
            else
            {
                ledsopr.Fill = Brushes.SpringGreen;
            }
            check = !check;
        }

        //chọn mục tiêu để tự động chỉ thị mục tiêu (chỉ đối với mục tiêu tự động bám sát) 
        private void TsasClick(object sender, RoutedEventArgs e)
        {
            if (check)
            {
                ledxas.Fill = Brushes.Gray;
            }
            else
            {
                ledxas.Fill = Brushes.SpringGreen;
            }
            check = !check;
        }

        //chọn vị trí (theo góc và theo cự ly) để cấp số liệu chỉ thị mục tiêu ЦУ không phụ thuộc vào có mục tiêu (dẫn theo vị trí)
        private void TsmClick(object sender, RoutedEventArgs e)
        {
            if (check)
            {
                ledxm.Fill = Brushes.Gray;
            }
            else
            {
                ledxm.Fill = Brushes.SpringGreen;
            }
            check = !check;
        }

        private void Button_Click6(object sender, RoutedEventArgs e)
        {
            if (check)
            {
                led6.Fill = Brushes.Gray;
            }
            else
            {
                led6.Fill = Brushes.SpringGreen;
            }
            check = !check;
        }

        private void Button_Click7(object sender, RoutedEventArgs e)
        {
            if (check)
            {
                led7.Fill = Brushes.Gray;
            }
            else
            {
                led7.Fill = Brushes.SpringGreen;
            }
            check = !check;
        }


        //Điều chỉnh công suất phát
        private void pw50_Checked(object sender, RoutedEventArgs e)
        {
            pwokl.IsChecked = false; pw75.IsChecked = false; pw100.IsChecked = false;
        }

        private void pwokl_Checked(object sender, RoutedEventArgs e)
        {
            pw50.IsChecked = false; pw75.IsChecked = false; pw100.IsChecked = false;
        }

        private void akshavt_Unchecked(object sender, RoutedEventArgs e)
        {
            L = 1;
        }

        private void ToggleButton_Unchecked(object sender, RoutedEventArgs e)
        {
            Polar.Content = ikoContainer;
        }

        private void ToggleButton_Checked(object sender, RoutedEventArgs e)
        {
            Polar.Content = squareView;
        }

        private void pw75_Checked(object sender, RoutedEventArgs e)
        {
            pw50.IsChecked = false; pwokl.IsChecked = false; pw100.IsChecked = false;
        }

        private void pw100_Checked(object sender, RoutedEventArgs e)
        {
            pw50.IsChecked = false; pwokl.IsChecked = false; pw75.IsChecked = false;
        }



        //Điều chỉnh búp sóng phát
        private void smallbw_Hit(object sender, RoutedEventArgs e)
        {
            uhn.IsEnabled = true; mediumbw.IsChecked = false; largebw.IsChecked = false;
        }

        private void mediumbw_Checked(object sender, RoutedEventArgs e)
        {
            smallbw.IsChecked = false; largebw.IsChecked = false; uhn.IsEnabled = false;
        }

        private void largebw_Checked(object sender, RoutedEventArgs e)
        {
            smallbw.IsChecked = false; mediumbw.IsChecked = false; uhn.IsEnabled = false;
        }


        //Điều khiển chọn hệ số khuếch đại
        private void _0db_Checked(object sender, RoutedEventArgs e)
        {
            m10db.IsChecked = false; m20db.IsChecked = false; m30db.IsChecked = false;
        }

        private void m20db_Checked(object sender, RoutedEventArgs e)
        {
            m10db.IsChecked = false; _0db.IsChecked = false; m30db.IsChecked = false;
        }

        private void m30db_Checked(object sender, RoutedEventArgs e)
        {
            m10db.IsChecked = false; m20db.IsChecked = false; _0db.IsChecked = false;
        }

        private void m10db_Checked(object sender, RoutedEventArgs e)
        {
            _0db.IsChecked = false; m20db.IsChecked = false; m30db.IsChecked = false;
        }



        //Điều khiển xóa nhiễu địa vật
        private void noiseotkl_Checked(object sender, RoutedEventArgs e)
        {
            noisevkl.IsChecked = false;
        }
        private void noisevkl_Checked(object sender, RoutedEventArgs e)
        {
            noiseotkl.IsChecked = false;
        }


        //Điều khiển bù trôi dạt
        private void akshotkl_Checked(object sender, RoutedEventArgs e)
        {
            akshavt.IsChecked = false; shakhan.IsChecked = false;
        }
        private void akshavt_Checked(object sender, RoutedEventArgs e)
        {
            akshotkl.IsChecked = false; shakhan.IsChecked = false; L = 0.05;
        }
        private void shakhan_Checked(object sender, RoutedEventArgs e)
        {
            akshotkl.IsChecked = false; akshavt.IsChecked = false;
        }


        //Chọn thang đo cự ly trên IKO
        private void r600_Checked(object sender, RoutedEventArgs e)
        {
            r300.IsChecked = false; r150.IsChecked = false;
            ratio = 0.5;
            ikoContainer.dtcrl1.Text = "100"; ikoContainer.dtcrl2.Text = "200"; ikoContainer.dtcrl3.Text = "300"; ikoContainer.dtcrl4.Text = "400"; ikoContainer.dtcrl5.Text = "500";
            squareView.r100square.Text = "100"; squareView.r200square.Text = "200"; squareView.r300square.Text = "300"; squareView.r400square.Text = "400"; squareView.r500square.Text = "500"; squareView.r600square.Text = "600";


        }
        private void r300_Checked(object sender, RoutedEventArgs e)
        {
            r150.IsChecked = false; r600.IsChecked = false;
            ratio = 1;
            ikoContainer.dtcrl1.Text = "50"; ikoContainer.dtcrl2.Text = "100"; ikoContainer.dtcrl3.Text = "150"; ikoContainer.dtcrl4.Text = "200"; ikoContainer.dtcrl5.Text = "250";
            squareView.r100square.Text = "50"; squareView.r200square.Text = "100"; squareView.r300square.Text = "150"; squareView.r400square.Text = "200"; squareView.r500square.Text = "250"; squareView.r600square.Text = "300";

        }
        private void r150_Checked(object sender, RoutedEventArgs e)
        {
            r300.IsChecked = false; r600.IsChecked = false;
            ratio = 2;
            ikoContainer.dtcrl1.Text = "25"; ikoContainer.dtcrl2.Text = "50"; ikoContainer.dtcrl3.Text = "75"; ikoContainer.dtcrl4.Text = "100"; ikoContainer.dtcrl5.Text = "125";
            squareView.r100square.Text = "25"; squareView.r200square.Text = "50"; squareView.r300square.Text = "75"; squareView.r400square.Text = "100"; squareView.r500square.Text = "125"; squareView.r600square.Text = "150";

        }


        //Chọn thang đo cự ly trên CO

        private void r300co_Checked(object sender, RoutedEventArgs e)
        {
            r200co.IsChecked = false; r100co.IsChecked = false;
        }
        private void r200co_Checked(object sender, RoutedEventArgs e)
        {
            r100co.IsChecked = false; r300co.IsChecked = false;
        }
        private void r100co_Checked(object sender, RoutedEventArgs e)
        {
            r300co.IsChecked = false; r200co.IsChecked = false;
        }
    

        //Điều khiển Sector
        private void razd_Checked(object sender, RoutedEventArgs e)
        {
            sovm.IsChecked = false;
        }
        private void sovm_Checked(object sender, RoutedEventArgs e)
        {
            razd.IsChecked = false;
        }


        //Bật cửa sổ đăng nhập chỉ huy
        bandieukhien bdk = new bandieukhien();
        private void commander(object sender, RoutedEventArgs e)
        {
            this.Hide();
            bdk.Show();

        }
        private void tracthumode_Checked(object sender, RoutedEventArgs e)
        {
            chihuymode.IsChecked = false;
        }

        private void smallbw_Unchecked(object sender, RoutedEventArgs e)
        {
            uhn.IsEnabled = false;
        }

        private void chihuymode_Unchecked(object sender, RoutedEventArgs e)
        {
            bdk.Hide();
        }
//Tắt chế độ, quay về màn hình chính
        private void Window_Closed(object sender, EventArgs e)
        {
            MainScreen mainscreen = new MainScreen();
            mainscreen.Show();// close all windows
        }


    }

}