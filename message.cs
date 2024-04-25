using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
// Анимированный гиф
using WpfAnimatedGif;


namespace ConectoWorkSpace
{
    /// <summary>
    /// Вспомогательное окно внутри окна для вывода сообщений, с возможностью вывода внешнего окна которое всегда вверху
    /// </summary>
    class message
    {

        //private static Border MessageBorder = new Border();

        private static Grid MessageGrid = new Grid();

        private static Grid NewGrid1 = new Grid();

        private static TextBlock MessageText = new TextBlock();

        private static Border ImageBorder = new Border();

        private static Grid NewGrid = new Grid();

        public  message()
        {
            //StackPanel BlMapNet = new StackPanel();

            //MessageBorder = new Border();
            //// По умочанию расположен в центре    
            //Grid.SetRow(MessageBorder, 2);
            //Grid.SetColumn(MessageBorder, 2);

            //MessageBorder.Width = 274;
            //MessageBorder.Height = 156;
            //// Центрирование в окне
            //MessageBorder.HorizontalAlignment = HorizontalAlignment.Center;
            //MessageBorder.VerticalAlignment = VerticalAlignment.Center;
            //MessageBorder.Margin = new Thickness(0, 0, 0, 0);

            MessageGrid = new Grid();
            MessageGrid.Name = "WinGrid";
            MessageGrid.Width = 274;
            MessageGrid.Height = 179;

            // Центрирование в окне
            MessageGrid.HorizontalAlignment = HorizontalAlignment.Center;
            MessageGrid.VerticalAlignment = VerticalAlignment.Center;
            MessageGrid.Background = new ImageBrush(new BitmapImage(new Uri(@"pack://application:,,,/Conecto®%20WorkSpace;component/Images/podskazka2.png")));
            MessageGrid.Margin = new Thickness(0, 0, 0, 0);

            // MessageBorder.Child = MessageGrid;

            TextBlock MessageText = new TextBlock();
            MessageText.Name = "MessageText";
            MessageText.Width = 209;
            MessageText.TextWrapping = TextWrapping.Wrap;
            MessageText.FontFamily = new FontFamily(AppStart.FontMain["Myriad Pro Cond"]); // new FontFamily("Consolas");
            MessageText.Height = double.NaN;
            // Центрирование в grid1 по вертикали
            MessageText.HorizontalAlignment = HorizontalAlignment.Left;
            MessageText.VerticalAlignment = VerticalAlignment.Center;

            Grid.SetRow(MessageText, 0);
            //Grid.SetColumn(Image_WorkS, nCells);

            // ========================== Gif  с фоном

            Border ImageBorder = new Border();
            ImageBorder.Name = "WaitGif";
            // По умочанию расположен в центре    
            Grid.SetRow(ImageBorder, 1);
            Grid.SetColumn(ImageBorder, 0);
            ImageBorder.Width = 152;
            ImageBorder.Height = 22;
            // Центрирование в окне
            ImageBorder.HorizontalAlignment = HorizontalAlignment.Center;
            ImageBorder.VerticalAlignment = VerticalAlignment.Center;
            ImageBorder.Margin = new Thickness(28, 0, 0, 0);

            ImageBorder.Background = new ImageBrush(new BitmapImage(new Uri(@"pack://application:,,,/Conecto®%20WorkSpace;component/Images/wait_podloj_begun.png")));


            Image WaitGifIm = new Image();

            BitmapImage image = new BitmapImage();
            try
            {
                // анимационный gif

                image.BeginInit();
                image.UriSource = new Uri(@"/Conecto®%20WorkSpace;component/Images/waitmessage_013.gif", UriKind.Relative); //new Uri(fileName);
                image.EndInit();

                ImageBehavior.SetAnimatedSource(WaitGifIm, image);
            }
            catch (Exception ex)
            {
                SystemConecto.ErorDebag("Библиотека WpfAnimatedGif вызвала исключение: \r\n === Файл: " + image.UriSource.ToString() + "\r\n === Message: " + ex.Message.ToString() + "\r\n === Exception: " + ex.ToString(), 1);
            }

            WaitGifIm.Source = new BitmapImage(new Uri("/Conecto®%20WorkSpace;component/Images/waitmessage_013.gif", UriKind.Relative)); //pack://application:,,,/Conecto®%20WorkSpace;component/Images/keyf.png
            
            WaitGifIm.Stretch = Stretch.None;
            WaitGifIm.Width = double.NaN;
            WaitGifIm.Height = double.NaN;
            WaitGifIm.HorizontalAlignment = HorizontalAlignment.Center;
            WaitGifIm.VerticalAlignment = VerticalAlignment.Center;
            WaitGifIm.Margin = new Thickness(0, 0, 0, 0);

            ImageBorder.Child = WaitGifIm;
            // ============================

            NewGrid1 = new Grid();
            NewGrid1.Name = "grid1_";
            NewGrid1.Width = 209;
            NewGrid1.Height = double.NaN;
            // Центрирование в grid2 по вертикали
            NewGrid1.HorizontalAlignment = HorizontalAlignment.Left;
            NewGrid1.VerticalAlignment = VerticalAlignment.Center;
            NewGrid1.Margin = new Thickness(0, 0, 0, 0);

            NewGrid1.Background  = Brushes.Black;

            // Добавить в грид      
            RowDefinition r_0 = new RowDefinition();
            r_0.Height = GridLength.Auto;
            RowDefinition r_1 = new RowDefinition();
            r_1.Height = GridLength.Auto;
            
            NewGrid1.RowDefinitions.Add(r_0);
            NewGrid1.RowDefinitions.Add(r_1);


            NewGrid = new Grid();
            NewGrid.Name = "grid2_";
            NewGrid.Width = 209;
            NewGrid.Height = 91;
            // Центрирование в окне
            NewGrid.HorizontalAlignment = HorizontalAlignment.Center;
            NewGrid.VerticalAlignment = VerticalAlignment.Center;
            NewGrid.Margin = new Thickness(32, 33, 0, 0);

        }

        /// <summary>
        /// Создание объекта
        /// по умолчанию размещение в окне грида 2:2
        /// Запись в окно
        /// .Children.Add(MessageBorder);
        /// </summary>
        /// <param name="NameBorder"></param>
        /// <returns></returns>
        public Grid NewMessage(string NameBorder)
        {
            MessageGrid.Name = NameBorder;
            return MessageGrid;
        }
        
        #region окно внутри окна для вывода сообщений
        /// <summary>
        /// Вывод окна WaitMesage Желтое окно<para></para>
        /// ObWin - Объект окно, что вызывает класс для выравнвания сообщения по окну<para></para>
        /// TypeWin: 1 - Отобразить сообщение по центру экрана<para></para>
        /// 3 - Отобразить сообщение по центру экрана c анимированным баром<para></para>
        /// 9 - закрыть все окна<para></para>
        /// NameWindow - название внешнего окна сообщения [MessageBoxConecto]<para></para>
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public Grid WaitMessageShow(string Message, Window ObWin, int TypeWin = 0, string NameWindow = "MessageBoxConecto")
        {
           
            // Закрытие другого открытого сообщение во внешнем окне (нужно предусмотреть смещение окна)
            // Пройти все окна
            dynamic WinActivePanelLeft = SystemConecto.ListWindowMain(NameWindow);
            if (WinActivePanelLeft != null)
            {
                // Закрыть только по команде
                if (TypeWin == 9)
                {
                    WinActivePanelLeft.CloseMessage();
                }
            }


            // Отобразить сообщение по центру экрана
            if (TypeWin == 0)
            {
                // --- Сборка окна
                MessageText.Text = Message; // "Отсутствует соединение с интернет сервером conecto.ua";
                // Отображение окна
                //Message1.Visibility = Visibility.Visible;
                // Gif отсутствует
                //WaitGif.Visibility = Visibility.Collapsed;
            }

            if (TypeWin == 3)
            {
                // --- Сборка окна
                
                MessageText.Text = Message; // "Отсутствует соединение с интернет сервером www.conecto.ua";
                //Message1.Visibility = Visibility.Visible;

                //BitmapImage image = new BitmapImage();
                //try
                //{
                //    // анимационный gif

                //    image.BeginInit();
                //    image.UriSource = new Uri(@"/Conecto®%20WorkSpace;component/Images/waitmessage_013.gif", UriKind.Relative); //new Uri(fileName);
                //    image.EndInit();

                //    ImageBehavior.SetAnimatedSource(WaitGifIm, image);
                //    WaitGif.Visibility = Visibility.Visible;
                //}
                //catch (Exception ex)
                //{
                //    SystemConecto.ErorDebag("Библиотека WpfAnimatedGif вызвала исключение: \r\n === Файл: " + image.UriSource.ToString() + "\r\n === Message: " + ex.Message.ToString() + "\r\n === Exception: " + ex.ToString(), 1);
                //}

                NewGrid1.Children.Add(ImageBorder);
            }



            // Скрыть все окна
            if (TypeWin == 9)
            {
                // --- Сборка окна

                MessageGrid.Visibility = Visibility.Hidden;
                //Message1.Visibility = Visibility.Hidden;

            }

            // Вывести внешнее окно

            if (TypeWin == 11)
            {
                // Дял привязки окна к основному окну
                //MainWindow ConectoWorkSpace_InW = (MainWindow)Application.Current.MainWindow;
                MainWindow ConectoWorkSpace_InW = AppStart.LinkMainWindow();
                // Створюємо інше вікно з повідомленням
                WinOblakoVerh WinOblakoVerh_Info = new WinOblakoVerh(Message, 0, 2); // создаем AutoClose
                WinOblakoVerh_Info.Name = NameWindow;
                WinOblakoVerh_Info.Owner = ConectoWorkSpace_InW; //ConectoWorkSpace_InW;
                // Размеры - констаны окна нужно установить.
                WinOblakoVerh_Info.Top = ObWin.Top + ObWin.Height / 2 - 195 / 2; // отнять высоту окна и промежуток межу окнами
                WinOblakoVerh_Info.Left = ObWin.Left + ObWin.Width / 2 - 274 / 2;
                // Не активировать окно - не передавать клавиатурный фокус
                WinOblakoVerh_Info.ShowActivated = false;
                WinOblakoVerh_Info.Show();
                // Разместить...

                MessageGrid = null;
            }

            if (TypeWin != 11)
            {
                // --- Сборка окна

                NewGrid1.Children.Add(MessageText);

                NewGrid.Children.Add(NewGrid1);

                MessageGrid.Children.Add(NewGrid);


                MessageGrid.Name = "Grid_" + NameWindow;
            }

            return MessageGrid;
        }

        #endregion




    }
}
