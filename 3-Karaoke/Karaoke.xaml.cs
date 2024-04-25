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

namespace ConectoWorkSpace._3_Karaoke
{
    /// <summary>
    /// Логика взаимодействия для Karaoke.xaml
    /// </summary>
    public partial class Karaoke : Window
    {
        public Karaoke()
        {
            InitializeComponent();

            ResolutionDisplay();

            #region Заполнение данными
           
            #endregion

        }


        #region Разрешение экрана

        public void ResolutionDisplay()
        {
            // 3. Установка размеров окна для того чтобы игнорировать нажатие клавиши раскрыть на весь экран
            // Рабочего стола ([0] - Top; [1] - Left; [2] - Width; [3] - Height; [4] - Right;


            // 3. Установка размеров окна для того чтобы игнорировать нажатие клавиши раскрыть на весь экран
            // Рабочего стола ([0] - Top; [1] - Left; [2] - Width; [3] - Height; [4] - Right;
            // Размещение окна
            this.Top = SystemConecto.SizeDWAreaDef_aD[0] + 10;
            this.Left = SystemConecto.SizeDWAreaDef_aD[1] + 10;
            // Размер окна и Grid; Grid.Height - растягивает this.Okno.Height
            this.WinGrid.Height = this.Height = SystemConecto.SizeDWAreaDef_aD[3] - 55;
            this.WinGrid.Width = this.Width = SystemConecto.SizeDWAreaDef_aD[2] - 35; //(277 + 5 + 10)
            // Растягивается автоматически TabOkno
            //TabOkno.Width = this.WinGrid.Width - 110; // Слева 10 px и поля по 50px слева и справа

            // Тело окна
            // this.Okno.Height = this.Height - 100;
            //TabOkno.Height = this.Okno.Height - 24;

            // Вікна які знаходятся у цьоу вікні
            //WiknoConecto.Height = this.Okno.Height;

            // Отцентрировать сообщение
            //Message1.Margin = new Thickness(0, 0, 0, 0);



        }




        #endregion

        #region Изображения как клавиши - Визуализация интерфейса рабочего стола
        private void Close_F_MouseMove(object sender, MouseEventArgs e)
        {
            Close_F.Source = new BitmapImage(new Uri(@"/Conecto®%20WorkSpace;component/Images/knop2_2.png", UriKind.Relative));
        }

        private void Close_F_MouseLeave(object sender, MouseEventArgs e)
        {
            Close_F.Source = new BitmapImage( new Uri(@"/Conecto®%20WorkSpace;component/Images/knop2_1.png", UriKind.Relative));
        }
        #endregion

        #region События Click (Клик) функцилнальных клавиш рабочего стола
        private void Close__MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Close_();
        }
        #endregion

        #region Клавиша выхода из окна
        private void ImButExit_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            var uriSource = new Uri(@"/Conecto®%20WorkSpace;component/Images/vihod1.png", UriKind.Relative);
            //ImButExit.Source = new BitmapImage(uriSource);
            Close_PanelSys(2);
        }

        private void ImButExit_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var uriSource = new Uri(@"/Conecto®%20WorkSpace;component/Images/vihod2.png", UriKind.Relative);
            //ImButExit.Source = new BitmapImage(uriSource);
        }

        private void ImButExit_MouseLeave(object sender, MouseEventArgs e)
        {
            var uriSource = new Uri(@"/Conecto®%20WorkSpace;component/Images/vihod1.png", UriKind.Relative);
            //ImButExit.Source = new BitmapImage(uriSource);
        }
        #endregion


        #region События Click (Клик) функцилнальных клавиш окна
        private void Close_PanelSys(int TypeClose = 1)
        {

            if (TypeClose == 2)
            {
                // Закрыть авторизацию
                SystemConectoInterfice.UserInterficeClose();
            }

            Close_();

        }
        #endregion

        #region Выход из окна
        private void Close_()
        {
            // Очистить данные записанные в память


            this.Visibility = Visibility.Hidden;

            this.Owner.Focus();
            this.Owner = null;

            this.Close();


        }
        #endregion

    }
}
