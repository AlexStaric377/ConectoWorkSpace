﻿using System;
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

// Код шлюза
using ConectoWorkSpace;
using ConectoWorkSpace._TerminalAbonement;


namespace ConectoWorkSpace
{
    public partial class AppforWorkSpace
    {

        
        #region Окно Абоненского терминала

        /// <summary>
        /// ссылка на объект изображение основной панели
        /// </summary>
        public static Image AnimationImage_126 { get; set; }

        /// <summary>
        /// Загрузка событий кнопки 1С
        /// </summary>
        /// <param name="sender"></param>
        public static void LoadAppEvents_126(Window _WindowOwner, object sender, Image animationImage_)
        {
            AnimationImage_126 = animationImage_;

            if (sender is Image)
            {
                Image Image_WorkS = (Image)sender;

                Image_WorkS.MouseLeftButtonUp += new MouseButtonEventHandler(AppPlayStory_TerminalAbonement.Anhor_MouseLeftButtonUp);

                Image_WorkS.MouseLeave += new MouseEventHandler(AppPlayStory_TerminalAbonement.Anhor_MouseLeave);  // старая разработка Ecsport1C_MouseLeave

                Image_WorkS.MouseLeftButtonDown += new MouseButtonEventHandler(AppPlayStory_TerminalAbonement.Anhor_MouseLeftButtonDown);
            }
        }



        #region загрузка окна
        public static void TerminalAbonement_MainWindow()
        {

            //MainWindow ConectoWorkSpace_InW = (MainWindow)Application.Current.MainWindow;
            MainWindow ConectoWorkSpace_InW = AppStart.LinkMainWindow();

            var Window = SystemConecto.ListWindowMain("TerminalAbonementW"); //СExport_
            if (Window != null)
            {
                Window.Show();
            }
            else
            {
                TerminalAbonementW WinManual = new TerminalAbonementW
                {
                    Owner = ConectoWorkSpace_InW
                }; //СExport
                WinManual.Show();
               
            }
        }

        #endregion

        #endregion

    }

}

namespace ConectoWorkSpace._TerminalAbonement
{
    class AppPlayStory_TerminalAbonement
    {

        #region Основные параметры

        /// <summary>
        /// Пользовательские настройки
        /// </summary>
        public static Dictionary<string, string> UserconfigWorkSpace = new Dictionary<string, string>();

        /// <summary>
        /// Пользовательские настройки отката
        /// </summary>
        public static Dictionary<string, string> aParamAppUndo = new Dictionary<string, string>();


        public static string PutchImagePanel_1 = @"/Conecto®%20WorkSpace;component/Images/ico_terminalabon1.png";
        public static string PutchImagePanel_2 = @"/Conecto®%20WorkSpace;component/Images/ico_terminalabon2.png";


        /// <summary>
        /// Данные не общих настроек можно обратится в будущем к общим настройкам
        /// </summary>
        public static string TextFile =
                "<?xml version=\"1.0\" encoding=\"utf-16\"?><Параметры-ОбменаДанными>" + Environment.NewLine +
                "<FileConfig FileConfig_Ver-File-Config=\"0_1\" PuthFile=\"" + @"config\user\" + "\" /><AppOverall idApp=\"126\" />" + Environment.NewLine +
                " <OpciiOverall_126 BDSERVER_Alias=\"NameAliasBD\" BDSERVER_IP=\"127.0.0.1\" BDSERVER_Putch-Hide=\"\" " + Environment.NewLine +
                " ВключениеРежимаОтладки_настройка_модулей=\"true\" /> " + Environment.NewLine +
                "</Параметры-ОбменаДанными>";


        #endregion


        #region Событие авторизации

        public static void Anhor_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {

            //MainWindow ConectoWorkSpace_InW = (MainWindow)Application.Current.MainWindow;
            MainWindow ConectoWorkSpace_InW = AppStart.LinkMainWindow();

            // Пользователь пытается авторизироватся
            // настройки для сервера пользовательские 
            // загрузка настроек пользователя
            string[] TypeServer = new string[5] { "FB", "", "", "", "" };

            // -- Чтение параметров
            if (ReadMemoryConfigFile())
            {
                    TypeServer = new string[5] { "FB", UserconfigWorkSpace["BDSERVER_IP"], 
                                    UserconfigWorkSpace["BDSERVER_Alias"], 
                                    UserconfigWorkSpace["BDSERVER_Putch-Hide"], "ConectoWorkSpace.SystemConecto+AutorizUser.decodeStringB" }; //decodeB52

              }
            // --- Чтение параметров
          


            //if (UserConfig())
            //{
            //    TypeServer = new string[5] { "FB", UserconfigWorkSpace["BDSERVER_IP"], 
            //                    UserconfigWorkSpace["BDSERVER_Alias"], 
            //                    UserconfigWorkSpace["BDSERVER_Putch-Hide"], "ConectoWorkSpace.SystemConecto+AutorizUser.decodeStringB" }; //decodeB52
            //}


            // Если пользователь авторизирован ТОбиш имеет логин и пароль в памяти (этого мало разработка)
            if (SystemConecto.AutorizUser.LoginUserAutoriz == null || SystemConecto.AutorizUser.PaswdUserAutoriz == null ||
                SystemConecto.AutorizUser.LoginUserAutoriz.Length == 0 || SystemConecto.AutorizUser.PaswdUserAutoriz.Length == 0)
            {
                // Авторизация
                ConectoWorkSpace_InW.key_aut_ButtonDown("TerminalAbonement_MainWindow", 0, PutchImagePanel_2, @"/Conecto®%20WorkSpace;component/Images/b52_logo.png", "Термінал абонементів", TypeServer);
            }
            else
            {
                AppforWorkSpace.Ecsport1C_MainWindow();
            }

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
        public static void Anhor_MouseLeave(object sender, MouseEventArgs e)
        {
            AppforWorkSpace.AnimationImage_126.Source = new BitmapImage(new Uri(PutchImagePanel_1, UriKind.Relative));
        }
        public static void Anhor_MouseLeftButtonDown(object sender, MouseEventArgs e)
        {
            AppforWorkSpace.AnimationImage_126.Source = new BitmapImage(new Uri(PutchImagePanel_2, UriKind.Relative));

        }


        public static void Ecsport1C_MouseMove(object sender, MouseEventArgs e)
        {
            AppforWorkSpace.AnimationImage_126.Source = new BitmapImage(new Uri(PutchImagePanel_2, UriKind.Relative));
        }


        #endregion


        #region Обновление записей настроек в памяти 

        /// <summary>
        /// Обновление записей настроек в памяти <para></para>
        /// из конфигурационного файла
        /// </summary>
        public static bool ReadMemoryConfigFile()
        {
            // -- Чтение параметров
            // Определение конфигурационного файла
            // Обращение к общей конфигурации, а не локальной 1cshluz.xml или прочих
            string Defputh = ConfigControll.PuthFileDefault(SystemConecto.PutchApp + @"config\user\TerminalAbonementiv.xml", SystemConecto.PStartup + @"config\user\TerminalAbonementiv.xml", TextFile, 1);
            // Определения параметров
            if (Defputh.Length > 0)
            {
                UserconfigWorkSpace = ConfigControll.ReadParamID(Defputh, TextFile, 126, new string[] {"BDSERVER_Alias", "BDSERVER_IP",
                "BDSERVER_Putch-Hide", "ВключениеРежимаОтладки_настройка_модулей"}, 2);

                return true;

            }
            // --- Чтение параметров
            return false;
        }
        #endregion


        #region Запись переменной в Undo

        /// <summary>
        /// Запись переменной в Undo (запись в память предыдущего значения)
        /// </summary>
        /// <param name="NameParam">Имя параметра</param>
        public static void UpdateParamUndo(string NameParam)
        {

            // Проверка создания параметра в массива данных aParamAppUndo
            if (!aParamAppUndo.ContainsKey(NameParam))
            {
                aParamAppUndo.Add(NameParam, UserconfigWorkSpace.ContainsKey(NameParam) ?   UserconfigWorkSpace[NameParam] : "");
            }
            else
            {
                aParamAppUndo[NameParam] = UserconfigWorkSpace[NameParam];
            }
        }

        #endregion


        #region - контроль правильности веденния данных

        /// <summary>
        /// Контроллер проверки введенных данных и запись новых данных в память, применение для приложения
        /// </summary>
        /// <param name="ParamValue">Тип контроля введеных данных</param>
        /// <param name="TypeFunc">Тип контроля введеных данных 0 - Текстовая </param>
        /// <returns></returns>
        public static bool ControllerParam(dynamic ParamValue, string NameConfig, int TypeFunc = 0)
        {
            bool LoadParam = false;
            switch (TypeFunc)
            {
                //case 0:
                //    // 0 - Текстовая
                //    //ParamValue.GetType();

                //    // Запись
                //    SystemConecto.aParamApp[NameConfig] = ParamValue;

                //    LoadParam = true;
                //    break;
                //case 10:

                    // Примерв http://skillcoding.com/Default.aspx?id=155
                    // 10 - IP адресс v4
                    // Инициализируем новый экземпляр класса System.Text.RegularExpressions.Regex
                    // для указанного регулярного выражения.
                    // Regex IpMatch = new Regex(@"\b(?:\d{1,3}\.){3}\d{1,3}\b");
                    // Выполняем проверку обнаружено ли в указанной входной строке соответствие регулярному
                    // выражению, заданному в конструкторе System.Text.RegularExpressions.Regex.
                    // если да то возвращаем true, если нет то false

                    // LoadParam = IpMatch.IsMatch(ParamValue);
                    //break;
                case 11:
                    // 11 - проверка пути ...

                    // Запись в память
                    if (!UserconfigWorkSpace.ContainsKey(NameConfig))
                    {
                        UserconfigWorkSpace.Add(NameConfig, ParamValue);
                    }
                    else
                    {
                        UserconfigWorkSpace[NameConfig] = ParamValue;
                    }
                   

                    LoadParam = true;
                    break;

            }
            // Запись в файл если предыдущие значение тоже с ошибкой то записать последний результат (Разработка)
            if (LoadParam)
            {
                // var Config = new SystemConfig();
                // Config.CreateConfigXML(2);
            }

            // Применение (возможно определить метод)


            return LoadParam;
        }
        #endregion 


        ///--------------------------------


    }
    
    
}


