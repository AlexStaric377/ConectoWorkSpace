using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Resources;
/// this.Convert_
using System.Drawing.Imaging;
using System.Globalization;
using System.IO;
// --- Другие приложения подключаем возможность использовать WinApi-функции Для открітия других программ
using System.Runtime.InteropServices;
//--- Анимация
using System.Windows.Media.Animation;
/// Многопоточность
using System.Threading;
// --- Process 
using System.Diagnostics;
using System.ComponentModel; // Win32Exception

namespace ConectoWorkSpace
{
    public partial class AppforWorkSpace
    {

        #region Пример построения - Интегрированные парметры, свойства .... приложений AppPlayStory

        /// <summary>
        /// Загрузка событий кнопки на панели WorkSpace
        /// </summary>
        /// <param name="sender"></param>
        public static void LoadAppEvents_(object sender)
        {
            //SystemConecto.ErorDebag("Запуск событий!", 2);
            if (sender is Image)
            {
                Image Image_WorkS = (Image)sender;

                Image_WorkS.MouseLeftButtonUp += new MouseButtonEventHandler(AppforWorkSpace.App_MouseLeftButtonUp);

                Image_WorkS.MouseLeave += new MouseEventHandler(AppforWorkSpace.App_MouseLeave);

                Image_WorkS.MouseLeftButtonDown += new MouseButtonEventHandler(AppforWorkSpace.App_MouseLeftButtonDown);
            }
        }


        /// <summary>
        /// Открытия окна
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public static void App_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            //MainWindow ConectoWorkSpace_InW = (MainWindow)Application.Current.MainWindow;
            MainWindow ConectoWorkSpace_InW = AppStart.LinkMainWindow();

            var Window = SystemConecto.ListWindowMain("СExport_");
            if (Window != null)
            {
                Window.Show();
            }
            else
            {
                СExport WinManual = new СExport();
                WinManual.Owner = ConectoWorkSpace_InW;
                //363x646  // 73
                //MessageBox.Show(this.Conector.Margin.Top.ToString());
                //WinConector.Top = (this.Top) + this.Conector.Margin.Top + (this.Conector.Height) - 625;
                //WinConector.Left = (this.Left) + this.Conector.Margin.Left + (this.Conector.Width / 2) - (353) / 2;
                // MainWindow winMain = new Conector_();
                WinManual.Show();
                //WinConector.ShowDialog();
            }
        }

        #region Клавиша експорт
        public static void App_MouseLeave(object sender, MouseEventArgs e)
        {
            if (sender is Image)
            {
                // Пример кода
                //MessageBox.Show("Test");
                Image currentImage = (Image)sender;
                currentImage.Source = new BitmapImage(new Uri(@"/Conecto®%20WorkSpace;component/Images/export1.png", UriKind.Relative));

            }
          
        }
        public static void App_MouseLeftButtonDown(object sender, MouseEventArgs e)
        {
            if (sender is Image)
            {
                // Пример кода
                //MessageBox.Show("Test");
                Image currentImage = (Image)sender;
                currentImage.Source = new BitmapImage(new Uri(@"/Conecto®%20WorkSpace;component/Images/export1.png", UriKind.Relative));

            }
          
        }
        

        public static void App_MouseMove(object sender, MouseEventArgs e)
        {
            if (sender is Image)
            {
                // Пример кода
                //MessageBox.Show("Test");
                Image currentImage = (Image)sender;
                currentImage.Source = new BitmapImage(new Uri(@"/Conecto®%20WorkSpace;component/Images/export2.png", UriKind.Relative));

            }
        }


        #endregion
        #endregion

 

            //#region Окно експорта в 1С 1СExport

            ///// <summary>
            ///// Загрузка событий кнопки 1С
            ///// </summary>
            ///// <param name="sender"></param>
            //public static void LoadAppEvents_122(object sender)
            //{
            //    SystemConecto.ErorDebag("Запуск событий!", 2);
            //    if (sender is Image)
            //    {
            //        Image Image_WorkS = (Image)sender;

            //        Image_WorkS.MouseLeftButtonUp += new MouseButtonEventHandler(AppforWorkSpace.Ecsport1C_MouseLeftButtonUp);

            //        Image_WorkS.MouseLeave += new MouseEventHandler(AppforWorkSpace.Ecsport1C_MouseLeave);

            //        Image_WorkS.MouseLeftButtonDown += new MouseButtonEventHandler(AppforWorkSpace.Ecsport1C_MouseLeftButtonDown);
            //    }
            //}



            //public static void Ecsport1C_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
            //{

            //    if (sender is Image)
            //    {
            //        // Пример кода
            //        //MessageBox.Show("Test");
            //        Image currentImage = (Image)sender;
            //        currentImage.Source = new BitmapImage(new Uri(@"/Conecto®%20WorkSpace;component/Images/export1.png", UriKind.Relative));

            //    }
                
            //    MainWindow ConectoWorkSpace_InW = (MainWindow)App.Current.MainWindow;

            //    var Window = SystemConecto.ListWindowMain("СExport_");
            //    if (Window != null)
            //    {
            //        Window.Show();
            //    }
            //    else
            //    {
            //        СExport WinManual = new СExport();
            //        WinManual.Owner = ConectoWorkSpace_InW;
            //        //363x646  // 73
            //        //MessageBox.Show(this.Conector.Margin.Top.ToString());
            //        //WinConector.Top = (this.Top) + this.Conector.Margin.Top + (this.Conector.Height) - 625;
            //        //WinConector.Left = (this.Left) + this.Conector.Margin.Left + (this.Conector.Width / 2) - (353) / 2;
            //        // MainWindow winMain = new Conector_();
            //        WinManual.Show();
            //        //WinConector.ShowDialog();
            //    }
            //}

            //#region Клавиша експорт
            //public static void Ecsport1C_MouseLeave(object sender, MouseEventArgs e)
            //{
            //    if (sender is Image)
            //    {
            //        // Пример кода
            //        //MessageBox.Show("Test");
            //        Image currentImage = (Image)sender;
            //        currentImage.Source = new BitmapImage(new Uri(@"/Conecto®%20WorkSpace;component/Images/export1.png", UriKind.Relative));

            //    }

            //}
            //public static void Ecsport1C_MouseLeftButtonDown(object sender, MouseEventArgs e)
            //{
            //    if (sender is Image)
            //    {
            //        // Пример кода
            //        //MessageBox.Show("Test");
            //        Image currentImage = (Image)sender;
            //        currentImage.Source = new BitmapImage(new Uri(@"/Conecto®%20WorkSpace;component/Images/export2.png", UriKind.Relative));

            //    }

            //}


            //public static void Ecsport1C_MouseMove(object sender, MouseEventArgs e)
            //{
            //    if (sender is Image)
            //    {
            //        // Пример кода
            //        //MessageBox.Show("Test");
            //        Image currentImage = (Image)sender;
            //        currentImage.Source = new BitmapImage(new Uri(@"/Conecto®%20WorkSpace;component/Images/export2.png", UriKind.Relative));

            //    }
            //}


            //#endregion
            //#endregion



    }
}
