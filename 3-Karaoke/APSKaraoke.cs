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

// Управление Xml
using System.Xml;
using System.Xml.Linq;

// Код приложения
using ConectoWorkSpace.Systems;



namespace ConectoWorkSpace
{
        public partial class AppforWorkSpace // APSDafualt
        {

            #region События нажатия на изображение на панели

            /// <summary>
            /// ссылка на объект изображение основной панели
            /// </summary>
            public static Image animationImage_124 { get; set; }


            /// <summary>
            /// Загрузка События нажатия на изображение на панели
            /// </summary>
            /// <param name="sender"></param>
            public static void LoadAppEvents_124(object sender, Image animationImage_)
            {
                /// <summary>
                /// Объект анимирования для ярлыка на рабочем столе используется для анимации ключа на изображении
                /// современим изменить трансформацией изображения...
                /// </summary>
                // Image animationImage = null;
                animationImage_124 = animationImage_;

                if (sender is Image)
                {
                    Image Image_WorkS = (Image)sender;

                    Image_WorkS.MouseLeftButtonUp += new MouseButtonEventHandler(APSKaraoke.App_MouseLeftButtonUp);

                    Image_WorkS.MouseLeave += new MouseEventHandler(APSKaraoke.App_MouseLeave);

                    Image_WorkS.MouseLeftButtonDown += new MouseButtonEventHandler(APSKaraoke.App_MouseLeftButtonDown);
                }
            }

            #endregion

            #region загрузка окна
            public static void App_124_MainWindow()
            {

                MainWindow ConectoWorkSpace_InW = (MainWindow)App.Current.MainWindow;

                //TypeServer = new string[5] { "FB", AppPlayStory_1C.UserconfigWorkSpace["BDSERVER_IP"], 
                //                AppPlayStory_1C.UserconfigWorkSpace["BDSERVER_Alias"], 
                //                AppPlayStory_1C.UserconfigWorkSpace["BDSERVER_Putch-Hide"], "" };




                var Window = SystemConecto.ListWindowMain("Karaoke");
                if (Window != null)
                {
                    Window.Show();
                }
                else
                {
                    var WinManual = new ConectoWorkSpace._3_Karaoke.Karaoke();
                    WinManual.Owner = ConectoWorkSpace_InW;
                    //363x646  // 73
                    //MessageBox.Show(this.Conector.Margin.Top.ToString());
                    //WinConector.Top = (this.Top) + this.Conector.Margin.Top + (this.Conector.Height) - 625;
                    //WinConector.Left = (this.Left) + this.Conector.Margin.Left + (this.Conector.Width / 2) - (353) / 2;
                    // MainWindow winMain = new Conector_();
                    WinManual.Show();
                    //WinConector.ShowDialog();

                    // Снимок экрана после события Show()
                    //animationImage.Source = ProcessConecto.ScreenCapture(0,0,200,200);
                }
            }

            #endregion

        }

}

namespace ConectoWorkSpace
{
        /// <summary>
        /// Демо класс по умолчанию для создания приложений для ConectoWorkSpace
        /// </summary>
    public static class APSKaraoke
    {

            #region Основные параметры

        public static string PutchImagePanel_1 = @"/Conecto®%20WorkSpace;component/2-KassaExport/Image/ico_kassa1.png";
        public static string PutchImagePanel_2 = @"/Conecto®%20WorkSpace;component/2-KassaExport/Image/ico_kassa2.png";



            #endregion


            #region Событие авторизации

            public static void App_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
            {

                MainWindow ConectoWorkSpace_InW = (MainWindow)App.Current.MainWindow;

                // Пользователь пытается авторизироватся
                // настройки для сервера пользовательские 
                // загрузка настроек пользователя
                //string[] TypeServer = new string[5] { "FB", "", "", "", "" };

                //if (UserConfig())
                //{
                //    TypeServer = new string[5] { "FB", UserconfigWorkSpace["BDSERVER_IP"], 
                //                UserconfigWorkSpace["BDSERVER_Alias"], 
                //                UserconfigWorkSpace["BDSERVER_Putch-Hide"], "decodeB52" };
                //}
                //else
                //{
                //    // string[] TypeServer = new string[5] { "FB", "", "", "", "" };
                //}

                //// Если пользователь авторизирован ТОбиш имеет логин и пароль в памяти (этого мало разработка)
                //if (SystemConecto.AutorizUser.LoginUserAutoriz == null || SystemConecto.AutorizUser.PaswdUserAutoriz == null ||
                //    SystemConecto.AutorizUser.LoginUserAutoriz.Length == 0 || SystemConecto.AutorizUser.PaswdUserAutoriz.Length == 0)
                //{
                //    // Авторизация
                //    ConectoWorkSpace_InW.key_aut_ButtonDown("App_MainWindow", 0, PutchImagePanel_2, @"/Conecto®%20WorkSpace;component/Images/b52_logo.png", "1C шлюз", TypeServer);
                //}
                //else
                //{
                //    // Событие точка (метод) старта приложения
                //    #region альтернатива App_MainWindow
                //    //System.Reflection.MethodInfo loadAppEvents = typeof(AppforWorkSpace).GetMethod(NameAutorize_);
                //    //if (loadAppEvents != null)
                //    //{
                //    //    // SystemConecto.ErorDebag("LoadAppEvents_" + IdApp, 2);
                //    //    loadAppEvents.Invoke(ConectoWorkSpace_InW, new object[] { });
                //    //}
                //    #endregion


                //}
                    
                AppforWorkSpace.App_124_MainWindow();
                
                // Пример кода
                if (sender is Image)
                {
                    Image currentImage = (Image)sender;
                    if (currentImage.Source == new BitmapImage(new Uri(PutchImagePanel_2, UriKind.Relative)))
                        currentImage.Source = new BitmapImage(new Uri(PutchImagePanel_1, UriKind.Relative));
                }

            }
            #endregion


            #region Состояние клавиш на панели
            public static void App_MouseLeave(object sender, MouseEventArgs e)
            {
                AppforWorkSpace.animationImage_124.Source = new BitmapImage(new Uri(PutchImagePanel_1, UriKind.Relative));
            }
            public static void App_MouseLeftButtonDown(object sender, MouseEventArgs e)
            {
                AppforWorkSpace.animationImage_124.Source = new BitmapImage(new Uri(PutchImagePanel_2, UriKind.Relative));

            }


            public static void App_MouseMove(object sender, MouseEventArgs e)
            {
                AppforWorkSpace.animationImage_124.Source = new BitmapImage(new Uri(PutchImagePanel_2, UriKind.Relative));
            }


            #endregion




            /// <summary>
            /// Пользовательские настройки
            /// </summary>
            public static Dictionary<string, string> UserconfigWorkSpace = new Dictionary<string, string>();

            /// <summary>
            /// Пользовательские настройки отката
            /// </summary>
            public static Dictionary<string, string> aParamAppUndo = new Dictionary<string, string>();


            #region Файл user.xml    - чтение настройки караоке системы

            #endregion




    }


    }

