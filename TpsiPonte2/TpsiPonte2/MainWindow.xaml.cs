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
using System.Threading;

namespace TpsiPonte2
{
    /// <summary>
    /// Logica di interazione per MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        readonly Uri uriMacchinaDestra = new Uri("Macchina-destra.png", UriKind.Relative);
        static int posMacchinaDestra = 634;
        readonly Uri uriMacchinaSinistra = new Uri("Macchina-sinistra.png", UriKind.Relative);
        static int posMacchinaSinistra = 624;

        private int macchineSinistra;
        private int macchineDestra;

        private static object x = new object();

        public MainWindow()
        {
            InitializeComponent();

            ImageSource imgDx = new BitmapImage(uriMacchinaSinistra);
            imgMacchinaSinistra.Source = imgDx;
            ImageSource imgSx = new BitmapImage(uriMacchinaDestra);
            imgMacchinaDestra.Source = imgSx;

            imgMacchinaDestra.Visibility = Visibility.Hidden;
            imgMacchinaSinistra.Visibility = Visibility.Hidden;

            imgSemaforoVerdeDestra.Visibility = Visibility.Hidden;
            imgSemaforoVerdeSinistra.Visibility = Visibility.Hidden;
        }

        public void MovimentoDestraSinistra()
        {
            int k;
            while (macchineDestra > 0)
            {
                k = 10;

                if (macchineDestra < 10)
                {
                    k = macchineDestra;
                }

                lock (x)
                {
                    this.Dispatcher.BeginInvoke(new Action(() =>
                    {
                        imgSemaforoVerdeDestra.Visibility = Visibility.Visible;
                        imgSemaforoVerdeSinistra.Visibility = Visibility.Hidden;
                        imgSemaforoRossoDestra.Visibility = Visibility.Hidden;
                        imgSemaforoRossoSinistra.Visibility = Visibility.Visible;
                    }));

                    for (int i = 0; i < k; i++)
                    {
                        while (posMacchinaDestra > -463)
                        {
                            posMacchinaDestra -= 2;

                            Thread.Sleep(TimeSpan.FromMilliseconds(1));

                            this.Dispatcher.BeginInvoke(new Action(() =>
                            {
                                imgMacchinaDestra.Margin = new Thickness(posMacchinaDestra, 55, 0, 231);
                            }));
                        }

                        if (posMacchinaDestra <= -463)
                        {
                            posMacchinaDestra = 634;
                        }
                    }
                }
                macchineDestra -= 10;
            }
        }

        public void MovimentoSinistraDestra()
        {
            int k;
            while (macchineSinistra > 0)
            {
                k = 10;

                if (macchineSinistra < 10)
                {
                    k = macchineSinistra;
                }

                lock (x)
                {
                    this.Dispatcher.BeginInvoke(new Action(() =>
                    {
                        imgSemaforoVerdeDestra.Visibility = Visibility.Hidden;
                        imgSemaforoVerdeSinistra.Visibility = Visibility.Visible;
                        imgSemaforoRossoDestra.Visibility = Visibility.Visible;
                        imgSemaforoRossoSinistra.Visibility = Visibility.Hidden;
                    }));

                    for (int i = 0; i < k; i++)
                    {
                        while (posMacchinaSinistra > -463)
                        {
                            posMacchinaSinistra -= 2;

                            Thread.Sleep(TimeSpan.FromMilliseconds(1));

                            this.Dispatcher.BeginInvoke(new Action(() =>
                            {
                                imgMacchinaSinistra.Margin = new Thickness(0, 127, posMacchinaSinistra, 169);
                            }));
                        }

                        if (posMacchinaSinistra <= -463)
                        {
                            posMacchinaSinistra = 624;
                        }
                    }
                }

                macchineSinistra -= 10;
            }
        }

        private void btnStart_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                macchineSinistra = int.Parse(txtNMacchineSinistra.Text);
                macchineDestra = int.Parse(txtNMacchineDestra.Text);

                if (macchineDestra > 0)
                {
                    imgMacchinaDestra.Visibility = Visibility.Visible;
                }

                if (macchineSinistra > 0)
                {
                    imgMacchinaSinistra.Visibility = Visibility.Visible;
                }

                Thread t1 = new Thread(new ThreadStart(MovimentoDestraSinistra));
                Thread t2 = new Thread(new ThreadStart(MovimentoSinistraDestra));

                Random rnd = new Random();
                int r = rnd.Next(1, 2);

                switch (r)
                {
                    case 1:
                        t1.Start();
                        t2.Start();
                        break;

                    case 2:
                        t2.Start();
                        t1.Start();
                        break;
                }

                MessageBox.Show("Macchine create");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
