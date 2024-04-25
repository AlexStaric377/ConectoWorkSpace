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
// --- Process 
using System.Diagnostics;
using System.ComponentModel; // Win32Exception
//---- объекты ОС Windows (Реестр, {Win Api} 
using Microsoft.Win32;
// Импорт библиотек Windows DllImport (управление питанием ОС, ...
using System.Runtime.InteropServices;
// Управление вводом-выводом
using System.IO;
/// Многопоточность
using System.Threading;
using System.Windows.Threading;




namespace ConectoWorkSpace
{
    /// <summary>
    /// Логика взаимодействия для VideoWin.xaml
    /// </summary>
    public partial class VideoWin : Window
    {
        public static Dictionary<string, Process> ProcessAdobeFile = new Dictionary<string, Process>(); // Открытые файлы адоба документации


        public VideoWin()
        {
            InitializeComponent();



            // 3. Установка размеров окна для того чтобы игнорировать нажатие клавиши раскрыть на весь экран
            // Рабочего стола ([0] - Top; [1] - Left; [2] - Width; [3] - Height; [4] - Right;

            this.Top = SystemConecto.WorkAreaDisplayDefault[0] +10;
            this.Left = SystemConecto.WorkAreaDisplayDefault[1] + 10;
            this.WinGrid.Width = this.Width = SystemConecto.WorkAreaDisplayDefault[2] - (277 + 5 + 10);            
            this.Height = SystemConecto.WorkAreaDisplayDefault[3] - 35;
            this.Okno.Height = this.Height - 100;


            // this.WindGrid.Height = SystemConecto.WorkAreaDisplayDefault[3];
            // this.WinGrid.Width = SystemConecto.WorkAreaDisplayDefault[2] - 140;

            //this.Height = 525;
            //this.Okno.Height = 500;


        }



        #region События Click (Клик) функцилнальных клавиш рабочего стола
        private void Close__MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.Visibility = Visibility.Hidden;
            this.Close();
            //this.Close();
            // this.Owner = null;
        }
        #endregion


        #region Изображения как клавиши - Визуализация интерфейса 

        
        #endregion


        #region Обработка событий любой клавиатуры
        private void VideoW_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            // Да завершить
            if (e.Key == Key.Return)
            {
               

            }
            else
            {

                // Нет отказаться
                if (e.Key == Key.Escape)
                {
                    this.Visibility = Visibility.Hidden;
                    this.Close();
                    //this.Close();

                }

            }
            

           // Отладка
           // MessageBox.Show(e.Key.ToString());

        }
        #endregion

     
        #region Нажатие на ПДФ
        private void Pdf1_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var uriSource = new Uri(@"/Conecto®%20WorkSpace;component/Images/video_ico2.png", UriKind.Relative);
            this.Pdf1.Source = new BitmapImage(uriSource);

        }

        private void Pdf1_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
        {
            var uriSource = new Uri(@"/Conecto®%20WorkSpace;component/Images/video_ico1.png", UriKind.Relative);
            this.Pdf1.Source = new BitmapImage(uriSource);
        }

        private void Pdf1_MouseMove(object sender, MouseEventArgs e)
        {
            //var uriSource = new Uri(@"/Conecto®%20WorkSpace;component/Images/video_ico2.png", UriKind.Relative);
            //this.Pdf1.Source = new BitmapImage(uriSource);
        }

        private void Pdf1_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            var uriSource = new Uri(@"/Conecto®%20WorkSpace;component/Images/video_ico1.png", UriKind.Relative);
            this.Pdf1.Source = new BitmapImage(uriSource);


            Pdf_MouseLeftButtonUp("YouTube", "http://www.youtube.com/v/lydHNguw6H0");
         
        }

        private bool Pdf_MouseLeftButtonUp(string NameWindow, string NameFileOpen)
        {

            var Window = SystemConecto.ListWindowMain("WaitFonW");
            if (Window != null)
            {
                Window.Show();
            }
            else
            {
                WaitFon FonWindow = new WaitFon("Black");
                FonWindow.Owner = this;
                FonWindow.Show();
            }

            // Модальное окно
            PlayVideo WinPlayVideo = new PlayVideo(NameWindow, NameFileOpen);
            WinPlayVideo.Owner = this;
            //363x646  // 73
            // MessageBox.Show(SystemConecto.WorkAreaDisplayDefault[1].ToString() + "//" +  this.Conector.Margin.Left.ToString() + "//" + this.Conector.Width.ToString());
            //WinConector.Top = SystemConecto.WorkAreaDisplayDefault[0] + this.Conector.Margin.Top + this.Conector.Height - 632; //625
            //WinConector.Left = SystemConecto.WorkAreaDisplayDefault[1] + this.Conector.Margin.Left + (65 / 2) - 353 / 2; //353
            // MainWindow winMain = new Conector_();
            // WinConector.Show();
            WinPlayVideo.ShowDialog();
            


            return true;

        }

        private void Pdf2_MouseLeave(object sender, MouseEventArgs e)
        {
            var uriSource = new Uri(@"/Conecto®%20WorkSpace;component/Images/video_ico1.png", UriKind.Relative);
            this.Pdf2.Source = new BitmapImage(uriSource);
        }

        private void Pdf2_MouseMove(object sender, MouseEventArgs e)
        {
            //var uriSource = new Uri(@"/Conecto®%20WorkSpace;component/Images/video_ico2.png", UriKind.Relative);
            //this.Pdf2.Source = new BitmapImage(uriSource);
        }
        private void Pdf2_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var uriSource = new Uri(@"/Conecto®%20WorkSpace;component/Images/video_ico2.png", UriKind.Relative);
            this.Pdf2.Source = new BitmapImage(uriSource);
        }

        private void Pdf2_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {

            var uriSource = new Uri(@"/Conecto®%20WorkSpace;component/Images/video_ico1.png", UriKind.Relative);
            this.Pdf2.Source = new BitmapImage(uriSource);

            Pdf_MouseLeftButtonUp("FilePlay", @"C:\Program Files\Conecto\WorkSpace\Release\Multimedia\video\Urok1_Poisk.mp4"); //D:\!Project\WPF-f4\Urok1_Poisk.mp4
        }
     

        // =============== Загрузка окна
        private void VideoWinW_Loaded(object sender, RoutedEventArgs e)
        {

          

        }

     

      

       

       
       


        #region Плеер


        //void mediaPlay(Object sender, EventArgs e)
        //{
        //    myMedia.Play();
        //}

        //void mediaPause(Object sender, EventArgs e)
        //{
        //    myMedia.Pause();
        //}

        //void mediaMute(Object sender, EventArgs e)
        //{
        //    if (myMedia.Volume == 100)
        //    {
        //        myMedia.Volume = 0;
        //        muteButt.Content = "Listen";
        //    }
        //    else
        //    {
        //        myMedia.Volume = 100;
        //        muteButt.Content = "Mute";
        //    }
        //}

        #endregion



        #endregion


        #region Примеры XAML видео плеера


        //<StackPanel HorizontalAlignment="Center" VerticalAlignment="Center" Grid.Column="3" Grid.Row="1" Margin="274,96,288,13" Width="316" Height="266">
        //    <MediaElement Name="myMedia" 
        //        LoadedBehavior="Manual" Width="255" Height="189"  />
        //        <StackPanel Orientation="Horizontal" Margin="0,10,0,0">
        //            <Button Content="Play" Margin="0,0,10,0" 
        //        Padding="5" Click="mediaPlay" />
        //            <Button Content="Pause" Margin="0,0,10,0" 
        //        Padding="5" Click="mediaPause" />
        //            <Button x:Name="muteButt" Content="Mute" 
        //        Padding="5" Click="mediaMute" />
        //        </StackPanel>
        //</StackPanel>




        //<MediaElement Name="myMedia2" Width="250" Height="240" Grid.Column="3" Grid.Row="1" HorizontalAlignment="Right" Margin="0,68,32,68" />
        //<Grid.Triggers>
        //    <EventTrigger RoutedEvent="Grid.Loaded">
        //        <EventTrigger.Actions>
        //            <BeginStoryboard>
        //                <Storyboard Storyboard.TargetName="myMedia2">
        //                    <MediaTimeline Source="D:\!Project\WPF-f4\Urok1 Poisk.mp4"
        // BeginTime="00:00:00" Duration="00:05:00" />
        //                </Storyboard>
        //            </BeginStoryboard>
        //        </EventTrigger.Actions>
        //    </EventTrigger>
        //</Grid.Triggers>

        #endregion





    }
}
