using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using AxShockwaveFlashObjects;
///
using System.Windows.Forms;  //        xmlns:WinForms="clr-namespace:System.Windows.Forms;assembly=System.Windows.Forms"
// xmlns:WinFormsInt="clr-namespace:System.Windows.Forms.Integration;assembly=WindowsFormsIntegration"
using System.Windows.Forms.Integration;

// Информация о флеш плеере
//http://www.adobe.com/devnet/flash/samples.html
//http://help.adobe.com/ru_RU/ActionScript/3.0_ProgrammingAS3/WS5b3ccc516d4fbf351e63e3d118a9b90204-7cb0.html
//http://www.codeproject.com/Articles/27121/Stream-YouTube-Videos-in-WPF
//http://www.csharpcoderr.com/2013/01/SWF.html

//DirectX
//audiovideoplayback c# wpf


namespace ConectoWorkSpace
{
    /// <summary>
    /// Логика взаимодействия для PlayVideo.xaml
    /// </summary>
    public partial class PlayVideo : Window
    {


        public AxShockwaveFlashObjects.AxShockwaveFlash AxShockwaveFlash;

        public MediaPlayer mp = new MediaPlayer();

        public string _Movie;

        public string P_VariantWindow;

        public bool MaxWin = false;
        public double StartCentrWinTop = 0;
        public double StartCentrWinLeft = 0;

        public Window Owner_ = null;

        public PlayVideo(string VariantWindow = "", string FileUri = "")
        {

            P_VariantWindow = VariantWindow;

            InitializeComponent();

            // 3. Установка размеров окна для того чтобы игнорировать нажатие клавиши раскрыть на весь экран
            // Рабочего стола ([0] - Top; [1] - Left; [2] - Width; [3] - Height; [4] - Right;

            // Окно центрируется по рабочему столу

            //this.Top = SystemConecto.WorkAreaDisplayDefault[0] + 10;
            //this.Left = SystemConecto.WorkAreaDisplayDefault[1] + 10;
            this.WinGrid.Margin = new Thickness(-10, -10, -10, -10);
            this.WinGrid.Width = this.Width = 780;
           // this.WinGrid.Width = 737;
            this.Height = this.WinGrid.Height = 580;
            //this.Okno.Height = this.Height - 100;
            

            //    System.Windows.Forms.Integration.WindowsFormsHost host =
            //new System.Windows.Forms.Integration.WindowsFormsHost();


            if (VariantWindow == "YouTube")
            {

                _Movie = FileUri;
                try
                {
                    ////Инициализуруем новый компонент AxShockwaveFlash
                    AxShockwaveFlash = new AxShockwaveFlash();

                    // AxShockwaveFlash.Location = new System.Drawing.Point(50, 50);
                    // AxShockwaveFlash.Size = new System.Drawing.Size(525, 266);
                    //fl.Name = "YoutubePlay1";
                    ////задаем координаты левого верхнего угла элемента управления
                    ////относительно левого верхнего угла контейнера. 
                    //fl.Location = new Point(10,10);
                    ////задаем высоту и ширину элемента управления.
                    // AxShockwaveFlash.Size = new System.Drawing.Size(525, 266);
                    ////задаем имя элемента управления.
                    // AxShockwaveFlash.Name = "axShockwaveFlash1";
                    AxShockwaveFlash.Visible = true;
                    //AxShockwaveFlash.Menu = false;

                    //AxShockwaveFlash.LoadMovie(0, @"http://www.youtube.com/v/lydHNguw6H0");
                    // fl.Movie = @"http://www.youtube.com/v/lydHNguw6H0";

                    // активировать фильм при отображении объекта в окне
                    this.AxShockwaveFlash.VisibleChanged += new System.EventHandler(this.AxShockwaveFlash_VisibleChanged);

                    ////Добавляем указанный элемент управления в коллекцию элементов
                    ////управления.


                    // размеры
                    //Play2.Width = windowsFormsHost1.Width = WinGrid.Width - 10 ;
                    //Play2.Height = windowsFormsHost1.Height = WinGrid.Height - 70 ;

                    Play2.Width = windowsFormsHost1.Width = 710;
                    Play2.Height = windowsFormsHost1.Height = 510 ;

                    Play2.Margin = new Thickness(10, 10, 10, 10);


                    windowsFormsHost1.Child = AxShockwaveFlash;//все остальные элементы добавляются по аналогии 



                    //this.Controls.Add(fl);

                    //AxShockwaveFlash.LoadMovie(0, @"http://www.youtube.com/v/lydHNguw6H0");

                }
                catch (Exception ex)
                {
                    SystemConecto.ErorDebag(ex.ToString() + "//" + ex.Message, 2);
                }

                Play1.Visibility = Visibility.Collapsed;

            }

            if (VariantWindow == "FilePlay")
            {
                // плеер MediaPlayer Play 1

                try
                {
                    mp.Open(new Uri(FileUri)); //@"D:\!Project\WPF-f4\Urok1_Poisk.mp4"
                    mp.Volume = 100;


                    VideoDrawing vd = new VideoDrawing();
                    vd.Player = mp;
                    vd.Rect = new Rect(0, 0, 710, 500);

                    DrawingBrush db = new DrawingBrush(vd);

                    // размеры
                    Play1.Width =  710;
                    Play1.Height = 500;

                    Play1.Margin = new Thickness(10, 10, 10, 10);

                    Play1.Background = db;

                    //b.Background = db;

                    mp.Play();
                }
                catch (Exception ex)
                {
                    SystemConecto.ErorDebag(ex.ToString() + "\r\n - Message:" + ex.Message, 2);

                }

                Play2.Visibility = Visibility.Collapsed;
            }

           

           

        }

    

        public string Movie
        {
            set { _Movie = value; }
            get { return AxShockwaveFlash.Movie; }
        }



        private void AxShockwaveFlash_VisibleChanged(object sender, EventArgs e)
        {
            if (IsVisible)
            {
                try
                {
                    if (Movie != null || Movie != _Movie)
                    {
                        AxShockwaveFlash.BackgroundColor = 0;
                        this.AxShockwaveFlash.Movie = _Movie;
                    }

                    // SystemConecto.ErorDebag("Запись Movie", 1);
                }
                catch
                {

                }
            }
        }


        private void Close__MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            CloseWindows();
        }



        private void CloseWindows()
        {
            // Скрыть
            this.Hide();

            // Закрыть
            if (P_VariantWindow == "YouTube")
            {
                AxShockwaveFlash.Dispose();
              }
            if (P_VariantWindow == "FilePlay")
            {
                mp.Close();
              }

            //MainWindow FonWait = (MainWindow)App.Current.MainWindow;

            //foreach ( MainWindow ctrl in App.Current.MainWindow[])
            //{

            //}
            var Window = SystemConecto.ListWindowMain("WaitFonW");
            if (Window != null)
            {
                Window.Hide();
            }
            //FonWait.Close();
            this.Close();
            // this.Owner = null;

        }



        #region Обработка событий любой клавиатуры
        private void PlayVideoW_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            // Да завершить
            if (e.Key == Key.Return)
            {
                // SystemConecto.EndWorkPC(); // Это работает

            }
            else
            {

                // Нет отказаться
                if (e.Key == Key.Escape)
                {
                    CloseWindows();

                }

            }


            // Отладка
            // MessageBox.Show(e.Key.ToString());

        }
        #endregion

        private void PlayVideo__Loaded(object sender, RoutedEventArgs e)
        {
            // this.AllowsTransparency = true;
        }

        /// <summary>
        /// Открыть на весь экран, весь экран за исключением системных настроек 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void image1_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            //var WindowFon = SystemConecto.ListWindowMain("WaitFonW");
            //if (WindowFon != null)
            //{
            //    //WindowFon.Hide();
            //    WindowFon.Visibility = Visibility.Hidden;
            //}
          
            
            //Owner_ = this.Owner;
            // Скрыть
            //this.Hide();
           // this.Visibility = Visibility.Hidden;


            this.WinGrid.Visibility = Visibility.Hidden;

            if (MaxWin)
            {
                this.Top = StartCentrWinTop;
                this.Left = StartCentrWinLeft;
                //this.WinGrid.Margin = new Thickness(-10, -10, -10, -10);
                this.WinGrid.Width = this.Width = 780;
                // this.WinGrid.Width = 737;
                this.Height = this.WinGrid.Height = 580;

                if (P_VariantWindow == "YouTube")
                {
                    Play2.Width = windowsFormsHost1.Width = 710; 
                    Play2.Height = windowsFormsHost1.Height = 510; 
                }
                if (P_VariantWindow == "FilePlay")
                {
                    Play1.Width = this.Width - (780 - 710);
                    Play1.Height = this.Height - (580 - 500);

                   // mp.;
                }


                MaxWin = false;
            }
            else
            {
                StartCentrWinTop = this.Top;
                StartCentrWinLeft = this.Left;
                this.Top = SystemConecto.WorkAreaDisplayDefault[0] + 10;
                this.Left = SystemConecto.WorkAreaDisplayDefault[1] + 10;
                //this.WinGrid.Margin = new Thickness(-10, -10, -10, -10);
                this.WinGrid.Width = this.Width = SystemConecto.WorkAreaDisplayDefault[2]-20;
                // this.WinGrid.Width = 737;
                this.Height = this.WinGrid.Height = SystemConecto.WorkAreaDisplayDefault[3]-57;

                if (P_VariantWindow == "YouTube")
                {
                    //AxShockwaveFlash.Dispose();
                    Play2.Width = windowsFormsHost1.Width = this.Width - (780 - 710);
                    Play2.Height = windowsFormsHost1.Height = this.Height - (580 - 510);
                }
                if (P_VariantWindow == "FilePlay")
                {

                    Play1.Width = this.Width - (780 - 710);
                    Play1.Height = this.Height - (580 - 500);
                    //  mp.Close();
                }

                MaxWin = true;
            }
            this.WinGrid.Visibility = Visibility.Visible;

            //WindowFon.Visibility = Visibility.Visible;
            //this.Visibility = Visibility.Visible;
            //WindowFon.Owner = Owner_;
            //WindowFon.Show();

            // MainWindow ConectoWorkSpace_InW = (MainWindow)App.Current.MainWindow;
            // MainWindow ConectoWorkSpace_InW = AppStart.LinkMainWindow();
            //SystemConecto.ListWindowMain("WinOblakoVerh_Net")
            // this.Owner = Owner_;//ConectoWorkSpace_InW;

            //Открыть
            // this.Show();


        }



    }



}
