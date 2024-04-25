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

// Код шлюза
using ConectoWorkSpace._1С_Export;
using ConectoWorkSpace;
using ConectoWorkSpace._2_KassaExport;

namespace ConectoWorkSpace
{
    public partial class AppforWorkSpace
    {

        
        #region Окно експорта в 1С 1СExport

        /// <summary>
        /// ссылка на объект изображение основной панели
        /// </summary>
        public static Image animationImage_122 { get; set; }

        /// <summary>
        /// Загрузка событий кнопки 1С
        /// </summary>
        /// <param name="sender"></param>
        public static void LoadAppEvents_122(Window _WindowOwner, object sender, Image animationImage_)
        {
            animationImage_122 = animationImage_;

            if (sender is Image)
            {
                Image Image_WorkS = (Image)sender;

                Image_WorkS.MouseLeftButtonUp += new MouseButtonEventHandler(AppPlayStory_1C.Ecsport1C_MouseLeftButtonUp);

                Image_WorkS.MouseLeave += new MouseEventHandler(AppPlayStory_1C.Ecsport1C_MouseLeave);

                Image_WorkS.MouseLeftButtonDown += new MouseButtonEventHandler(AppPlayStory_1C.Ecsport1C_MouseLeftButtonDown);
            }
        }



        #region загрузка окна
        public static void Ecsport1C_MainWindow()
        {

            //MainWindow ConectoWorkSpace_InW = (MainWindow)Application.Current.MainWindow;
            MainWindow ConectoWorkSpace_InW = AppStart.LinkMainWindow();

            var Window = SystemConecto.ListWindowMain("СExport_"); //СExport_
            if (Window != null)
            {
                Window.Show();
            }
            else
            {
                СExport WinManual = new СExport(); //СExport
                WinManual.Owner = ConectoWorkSpace_InW;
                WinManual.Show();
               
            }
        }

        #endregion

        #endregion

    }

}

namespace ConectoWorkSpace._1С_Export
{
    class AppPlayStory_1C
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


        public static string PutchImagePanel_1 = @"/Conecto®%20WorkSpace;component/Images/export1.png";
        public static string PutchImagePanel_2 = @"/Conecto®%20WorkSpace;component/Images/export2.png";


        #endregion


        #region Событие авторизации

        public static void Ecsport1C_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {

            //MainWindow ConectoWorkSpace_InW = (MainWindow)Application.Current.MainWindow;
            MainWindow ConectoWorkSpace_InW = AppStart.LinkMainWindow();

            // Пользователь пытается авторизироватся
            // настройки для сервера пользовательские 
            // загрузка настроек пользователя
            string[] TypeServer = new string[6] { "FB", "", "", "", "", "" };

            // -- Чтение параметров
            if (ReadMemoryConfigFile())
            {
                    TypeServer = new string[6] { "FB", UserconfigWorkSpace["BDSERVER_IP"], 
                                    UserconfigWorkSpace["BDSERVER_Alias"], 
                                    UserconfigWorkSpace["BDSERVER_Putch-Hide"], "ConectoWorkSpace.SystemConecto+AutorizUser.decodeStringB", UserconfigWorkSpace["BDSERVER_Port"] }; //decodeB52

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
                ConectoWorkSpace_InW.key_aut_ButtonDown("Ecsport1C_MainWindow", 0, PutchImagePanel_2, @"/Conecto®%20WorkSpace;component/Images/b52_logo.png", "1C шлюз", TypeServer);
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
        public static void Ecsport1C_MouseLeave(object sender, MouseEventArgs e)
        {
            AppforWorkSpace.animationImage_122.Source = new BitmapImage(new Uri(PutchImagePanel_1, UriKind.Relative));
        }
        public static void Ecsport1C_MouseLeftButtonDown(object sender, MouseEventArgs e)
        {
            AppforWorkSpace.animationImage_122.Source = new BitmapImage(new Uri(PutchImagePanel_2, UriKind.Relative));

        }


        public static void Ecsport1C_MouseMove(object sender, MouseEventArgs e)
        {
            AppforWorkSpace.animationImage_122.Source = new BitmapImage(new Uri(PutchImagePanel_2, UriKind.Relative));
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
            string Defputh = ConfigControll.PuthFileDefault(SystemConecto.PutchApp + @"config\user\1cshluz.xml", SystemConecto.PStartup + @"config\user\1cshluz.xml", Element.configEksportDoc.TextFile, 1);
            // Определения параметров
            if (Defputh.Length > 0)
            {
                // TYPE;CODE <OTEL;03,RESTORAN;25> <RESTORAN;25> тип и код подразделения авторизации
                UserconfigWorkSpace = ConfigControll.ReadParamID(Defputh, Element.configEksportDoc.TextFile, 122, new string[] {"BDSERVER_Alias", "BDSERVER_IP", "BDSERVER_Port",
                "BDSERVER_Putch-Hide", "Export_PutchDefault", "TypeAutorizeSDR", "TypeAutorizeSPORG", "TypeAutorizeSDRCach", "Import_PutchBankDefault","DateExport","DateImport","ВключениеРежимаОтладки_настройка_модулей","Период_запроса_настройка_експорта"}, 2);

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


        #region Примитивное разделение прав по подразделениям (ручное, не безопасное)

        /// <summary>
        /// Опеределения типа авторизации прав по подразделениям<para></para>
        /// CodeConfig берем первое значение 
        /// </summary>
        /// <returns></returns>
        public static AutorizeSDR TypeAutorizeSDR(string CodeConfig)
        {
            AutorizeSDR TypeA = new AutorizeSDR();

            TypeA.OtelSDR = "0";
            TypeA.RestoranSDR = "0";

            string[] aConfig = CodeConfig.Split(',');

            foreach (string item in aConfig)
            {
                // Структура OTEL;03;05
                string[] aConfigElemnt = item.Split(';');


                for (int i = 0; i < aConfigElemnt.Length; i++)
                {
                    // проверка группы
                    if (i == 0)
                    {
                        switch (aConfigElemnt[0].ToUpper())
                        {
                            case "OTEL":
                                TypeA.Otel = true;
                                break;
                            case "RESTORAN":
                                 TypeA.Restoran = true;
                                break;

                        }

                    }
                    else
                    {
                        switch (aConfigElemnt[0].ToUpper())
                        {
                            case "OTEL":
                                if (TypeA.OtelSDR == "0")
                                {
                                    TypeA.OtelSDR = aConfigElemnt[i];
                                }
                                else
                                {
                                    TypeA.OtelSDR = TypeA.OtelSDR + ";" + aConfigElemnt[i];
                                }
                                break;
                            case "RESTORAN":
                                if (TypeA.RestoranSDR == "0")
                                {
                                    TypeA.RestoranSDR = aConfigElemnt[i];
                                }
                                else
                                {
                                    TypeA.RestoranSDR = TypeA.RestoranSDR + ";" + aConfigElemnt[i];
                                }
                                break;

                        }
                    }

                    
                }
              
            }


            

            return TypeA;
        }


        public struct AutorizeSDR {

            /// <summary>
            /// Доступ к конфигурации ресторана
            /// </summary>
            public bool Restoran { get; set; }
            /// <summary>
            /// Доступ к конфигурации отеля
            /// </summary>
            public bool Otel { get; set; }
            /// <summary>
            /// Подразделения ресторана
            /// </summary>
            public string RestoranSDR { get; set; }
            /// <summary>
            /// Подразделения Отеля
            /// </summary>
            public string OtelSDR { get; set; }

        }


        #endregion


        #region разделение по организациям (ручное, не безопасное)

        /// <summary>
        /// Опеределения типа авторизации прав по подразделениям<para></para>
        /// CodeConfig берем первое значение 
        /// </summary>
        /// <returns></returns>
        public static AutorizeSDR TypeAutorizeSPORG(string CodeConfig)
        {
            AutorizeSDR TypeA = new AutorizeSDR();

            TypeA.OtelSDR = "0";
            TypeA.RestoranSDR = "0";

            string[] aConfig = CodeConfig.Split(',');

            foreach (string item in aConfig)
            {
                // Структура OTEL;03;05
                string[] aConfigElemnt = item.Split(';');


                for (int i = 0; i < aConfigElemnt.Length; i++)
                {
                    // проверка группы
                    if (i == 0)
                    {
                        switch (aConfigElemnt[0].ToUpper())
                        {
                            case "OTEL":
                                TypeA.Otel = true;
                                break;
                            case "RESTORAN":
                                TypeA.Restoran = true;
                                break;

                        }

                    }
                    else
                    {
                        switch (aConfigElemnt[0].ToUpper())
                        {
                            case "OTEL":
                                // Организация только одна
                                TypeA.OtelSDR = aConfigElemnt[i];
                                //TypeA.OtelSDR = TypeA.OtelSDR + (TypeA.OtelSDR.Length == 0 ? "" : ";") + aConfigElemnt[i];
                                break;
                            case "RESTORAN":
                                // Организация только одна
                                TypeA.RestoranSDR = aConfigElemnt[i];
                                // TypeA.RestoranSDR = TypeA.RestoranSDR + (TypeA.OtelSDR.Length == 0 ? "" : ";") + aConfigElemnt[i];
                                break;

                        }
                    }


                }

            }




            return TypeA;
        }


       


        #endregion


        #region разделение по кассам (ручное, не безопасное)

        /// <summary>
        /// Опеределения типа авторизации прав по подразделениям<para></para>
        /// CodeConfig берем первое значение 
        /// </summary>
        /// <returns></returns>
        public static AutorizeSDR TypeAutorizeSDRCach(string CodeConfig)
        {
            AutorizeSDR TypeA = new AutorizeSDR();

            TypeA.OtelSDR = "0";
            TypeA.RestoranSDR = "0";

            string[] aConfig = CodeConfig.Split(',');

            foreach (string item in aConfig)
            {
                // Структура OTEL;03;05
                string[] aConfigElemnt = item.Split(';');


                for (int i = 0; i < aConfigElemnt.Length; i++)
                {
                    // проверка группы
                    if (i == 0)
                    {
                        switch (aConfigElemnt[0].ToUpper())
                        {
                            case "OTEL":
                                TypeA.Otel = true;
                                break;
                            case "RESTORAN":
                                TypeA.Restoran = true;
                                break;

                        }

                    }
                    else
                    {
                        switch (aConfigElemnt[0].ToUpper())
                        {
                            case "OTEL":
                                if (TypeA.OtelSDR == "0")
                                {
                                    TypeA.OtelSDR = aConfigElemnt[i];
                                }
                                else
                                {
                                    TypeA.OtelSDR = TypeA.OtelSDR +  ";" + aConfigElemnt[i];
                                }
                                break;
                            case "RESTORAN":
                                if (TypeA.RestoranSDR == "0")
                                {
                                    TypeA.RestoranSDR = aConfigElemnt[i];
                                }
                                else
                                {
                                    TypeA.RestoranSDR = TypeA.RestoranSDR + ";" + aConfigElemnt[i];
                                }
                                break;

                        }
                    }


                }

            }




            return TypeA;
        }





        #endregion

    }
    
    
}


