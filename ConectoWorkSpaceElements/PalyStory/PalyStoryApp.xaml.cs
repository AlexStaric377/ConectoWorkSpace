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
// Анимированный гиф
using WpfAnimatedGif;

namespace ConectoWorkSpace
{
    /// <summary>
    /// Логика взаимодействия для PalyStoryApp.xaml
    /// </summary>
    public partial class PalyStoryApp : Window
    {
        public PalyStoryApp()
        {
            InitializeComponent();

            ResolutionDisplay();

        }


        public void ResolutionDisplay()
        {
            // 3. Установка размеров окна для того чтобы игнорировать нажатие клавиши раскрыть на весь экран
            // Рабочего стола ([0] - Top; [1] - Left; [2] - Width; [3] - Height; [4] - Right;

            this.Top = SystemConecto.WorkAreaDisplayDefault[0] + 10;
            this.Left = SystemConecto.WorkAreaDisplayDefault[1] + 10;
            this.WinGrid.Width = this.Width = SystemConecto.WorkAreaDisplayDefault[7];
            this.Height = SystemConecto.WorkAreaDisplayDefault[3] - 45;
            this.Okno.Height = this.Height - 100;

            // Вікна які знаходятся у цьоу вікні
            WiknoConecto.Height = this.Okno.Height;

            // Отцентрировать сообщение
            Message1.Margin = new Thickness(0, 0, 0, 0);

        }


        #region События Click (Клик) функцилнальных клавиш рабочего стола
        private void Close__MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            
        }
        private void ImClose__MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            this.Close_();
        }


        #endregion


        #region Изображения как клавиши - Визуализация интерфейса


        #endregion


        #region Обработка событий любой клавиатуры
        private void ConectoW_KeyDown(object sender, KeyEventArgs e)
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

                    this.Close_();

                }

            }


            // Отладка
            // MessageBox.Show(e.Key.ToString());

        }



        #endregion


        #region Выход из окна
        private void Close_()
        {
            //SystemConecto.ErorDebag("Close", 1);
            //this.Visibility = Visibility.Hidden;



            // Закрыть браузер
            // SpecPredlog.Dispose();

            //var Window = SystemConecto.ListWindowMain("WaitFonW");
            //if (Window != null)
            //{

            //    Window.Close();

            //}
            //var Window_ = SystemConecto.ListWindowMain("WaitFonW");
            //if (Window_ != null)
            //{

            //    Window_.Visibility = Visibility.Hidden;
            //    Window_.Hide();
            //    while (Window_.Visibility == Visibility.Visible)
            //    {
            //        //SystemConecto.ErorDebag("Не скріто", 1);
            //        Window_.Visibility = Visibility.Hidden;
            //    }
            //    //SystemConecto.ErorDebag("Скрыл", 1);
            //}

            // Скрыть

            this.Visibility = Visibility.Hidden;

            this.Owner.Focus();
            this.Owner = null;

            this.Close();


        }

        #endregion

        #region Вспомогательное окно внутри окна для вывода сообщений
        /// <summary>
        /// Вывод окна WaitMesage Желтое окно<para></para>
        /// TypeWin: 1 - Отобразить сообщение по центру экрана
        /// 3 - Отобразить сообщение по центру экрана c анимированным баром
        /// 9 - закрыть все окна
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void WaitMessageShow(string Message, int TypeWin = 0)
        {
            // Закрытие другого открытого сообщение во внешнем окне
            dynamic WinActivePanelLeft = SystemConecto.ListWindowMain("SpecPredMessage");
            if (WinActivePanelLeft != null)
            {
                WinActivePanelLeft.CloseMessage();
            }


            // Отобразить сообщение по центру экрана
            if (TypeWin == 0)
            {
                MessageText.Text = Message; // "Отсутствует соединение с интернет сервером www.conecto.ua";
                Message1.Visibility = Visibility.Visible;
                WaitGif.Visibility = Visibility.Collapsed;
            }

            if (TypeWin == 3)
            {
                MessageText.Text = Message; // "Отсутствует соединение с интернет сервером www.conecto.ua";
                Message1.Visibility = Visibility.Visible;

                BitmapImage image = new BitmapImage();
                try
                {
                    // анимационный gif

                    image.BeginInit();
                    image.UriSource = new Uri(@"/Conecto®%20WorkSpace;component/Images/waitmessage_013.gif", UriKind.Relative); //new Uri(fileName);
                    image.EndInit();

                    ImageBehavior.SetAnimatedSource(WaitGifIm, image);
                    WaitGif.Visibility = Visibility.Visible;
                }
                catch (Exception ex)
                {
                    SystemConecto.ErorDebag("Библиотека WpfAnimatedGif вызвала исключение: \r\n === Файл: " + image.UriSource.ToString() + "\r\n === Message: " + ex.Message.ToString() + "\r\n === Exception: " + ex.ToString(), 1);
                }
            }



            // Скрыть все окна
            if (TypeWin == 9)
            {

                Message1.Visibility = Visibility.Hidden;

            }

            // Вывести внешнее окно

            if (TypeWin == 11)
            {
                // Дял привязки окна к основному окну
                //MainWindow ConectoWorkSpace_InW = (MainWindow)Application.Current.MainWindow;
                MainWindow ConectoWorkSpace_InW = AppStart.LinkMainWindow();
                // Створюємо інше вікно з повідомленням
                WinOblakoVerh WinOblakoVerh_Info = new WinOblakoVerh(Message, 0, 2); // создаем AutoClose
                WinOblakoVerh_Info.Name = "SpecPredMessage";
                WinOblakoVerh_Info.Owner = ConectoWorkSpace_InW; //ConectoWorkSpace_InW;
                // Размеры - констаны окна нужно установить.
                WinOblakoVerh_Info.Top = this.Top + this.Height / 2 - 195 / 2; // отнять высоту окна и промежуток межу окнами
                WinOblakoVerh_Info.Left = this.Left + this.Width / 2 - 274 / 2;
                // Не активировать окно - не передавать клавиатурный фокус
                WinOblakoVerh_Info.ShowActivated = false;
                WinOblakoVerh_Info.Show();
                // Разместить...
            }
        }

        #endregion


    }
}
