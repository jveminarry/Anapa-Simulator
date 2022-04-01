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

namespace Anapa_Simulator
{
    /// <summary>
    /// Interaction logic for IKO.xaml
    /// </summary>
    public partial class IKO : Page
    {
        private MainWindow mainWindow;

        public double xsec;
        public double ysec;

        public IKO()
        {
            //InitializeComponent();
        }

        public IKO(MainWindow mainWindow)
        {
            InitializeComponent();
            this.mainWindow = mainWindow;
        }

        //Tắt vòng bám
        private void iko_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            track1.Stroke = null;
            mainWindow.xu1.Text = null;
            mainWindow.xu2.Text = null;
            mainWindow.xu3.Text = null;
            mainWindow.xu4.Text = null;
            mainWindow.xu5.Text = null; 
            mainWindow.xu6.Text = null; 
            mainWindow.xu7.Text = null;
        }
        //Lấy tọa độ vị trí con trỏ khi rê trên IKO
        private void PolarView_MouseMove(object sender, System.Windows.Input.MouseEventArgs e)
        {
            xsec = Mouse.GetPosition(iko).X;
            ysec = Mouse.GetPosition(iko).Y;
            double rsec = Math.Sqrt(((xsec - 349) * (xsec - 349)) + (ysec - 349) * (ysec - 349));
            double kusec = Math.Round(90 + Math.Asin((ysec - 349) / rsec) * 180 / Math.PI, 2);
            double posec = kusec - mainWindow.HTM;
            if (xsec < 350) { kusec = -kusec; posec = kusec + 360 - mainWindow.HTM; }
            if (posec > 180) { posec -= 360; } else if (posec < -180) { posec += 360; }
            Rsec.Text = Math.Round((rsec / mainWindow.ratio), 2).ToString() + @" м";
            Kusec.Text = kusec.ToString() + @"°";
            Posec.Text = Math.Round(posec, 2).ToString() + @"°";
        }

        //Lấy tọa độ vị trí bám
        private void PolarView_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Canvas.SetLeft(track1, xsec - 7.5);
            Canvas.SetTop(track1, ysec - 7.5);
            track1.Stroke = Brushes.White;

            double rxu = Math.Sqrt(((xsec - 349) * (xsec - 349)) + (ysec - 349) * (ysec - 349));
            double kuxu = Math.Round(90 + Math.Asin((ysec - 349) / rxu) * 180 / Math.PI, 2);
            double poxu = kuxu - mainWindow.HTM;

            if (xsec < 350) { kuxu = -kuxu; poxu = kuxu + 360 - mainWindow.HTM; }
            if (poxu > 180) { poxu -= 360; } else if (poxu < -180) { poxu += 360; }
            mainWindow.xu2.Text = Math.Round((rxu / mainWindow.ratio), 2).ToString();
            mainWindow.xu3.Text = kuxu.ToString() + @"°";
            mainWindow.xu4.Text = Math.Round(poxu, 2).ToString() + @"°";
            mainWindow.xu5.Text = Math.Round(-(0.25 * mainWindow.A), 2).ToString();
            mainWindow.xu6.Text = Math.Round(mainWindow.A, 2).ToString() + @"°";
            mainWindow.xu1.Text = null;
            mainWindow.notrack = 1;
        }

        private void obj_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            mainWindow.entrack = 1;
            mainWindow.notrack = 0;
        }
    }

}
