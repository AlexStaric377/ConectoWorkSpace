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
        public static Image animationImage_DEFAULT { get; set; }
        
 
        /// <summary>
        /// Загрузка События нажатия на изображение на панели
        /// </summary>
        /// <param name="sender"></param>
        public static void LoadAppEvents_DEFAULT(object sender, Image animationImage_)
        {
            /// <summary>
            /// Объект анимирования для ярлыка на рабочем столе используется для анимации ключа на изображении
            /// современим изменить трансформацией изображения...
            /// </summary>
            // Image animationImage = null;
            animationImage_DEFAULT = animationImage_;

            if (sender is Image)
            {
                Image Image_WorkS = (Image)sender;

                Image_WorkS.MouseLeftButtonUp += new MouseButtonEventHandler(DefaultAPS.App_MouseLeftButtonUp);

                Image_WorkS.MouseLeave += new MouseEventHandler(DefaultAPS.Ecsport1C_MouseLeave);

                Image_WorkS.MouseLeftButtonDown += new MouseButtonEventHandler(DefaultAPS.Ecsport1C_MouseLeftButtonDown);
            }
        }

        #endregion

        #region загрузка окна
        public static void App_DEFAULT_MainWindow()
        {

            //MainWindow ConectoWorkSpace_InW = (MainWindow)Application.Current.MainWindow;
            MainWindow ConectoWorkSpace_InW = AppStart.LinkMainWindow();

            //TypeServer = new string[5] { "FB", AppPlayStory_1C.UserconfigWorkSpace["BDSERVER_IP"], 
            //                AppPlayStory_1C.UserconfigWorkSpace["BDSERVER_Alias"], 
            //                AppPlayStory_1C.UserconfigWorkSpace["BDSERVER_Putch-Hide"], "" };




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
    public static class DefaultAPS
    {

        #region Основные параметры

        public static string PutchImagePanel_1 = @"/Conecto®%20WorkSpace;component/Images/export1.png";
        public static string PutchImagePanel_2 = @"/Conecto®%20WorkSpace;component/Images/export2.png";


        #endregion


        #region Событие авторизации

        public static void App_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {

            //MainWindow ConectoWorkSpace_InW = (MainWindow)Application.Current.MainWindow;
            MainWindow ConectoWorkSpace_InW = AppStart.LinkMainWindow();

            // Пользователь пытается авторизироватся
            // настройки для сервера пользовательские 
            // загрузка настроек пользователя
            string[] TypeServer = new string[5] { "FB", "", "", "", "" };

            if (UserConfig())
            {
                TypeServer = new string[5] { "FB", UserconfigWorkSpace["BDSERVER_IP"], 
                                UserconfigWorkSpace["BDSERVER_Alias"], 
                                UserconfigWorkSpace["BDSERVER_Putch-Hide"], "ConectoWorkSpace.SystemConecto+AutorizUser.decodeStringB" }; //decodeB52
            }
            else
            {
                // string[] TypeServer = new string[5] { "FB", "", "", "", "" };
            }

            // Если пользователь авторизирован ТОбиш имеет логин и пароль в памяти (этого мало разработка)
            if (SystemConecto.AutorizUser.LoginUserAutoriz == null || SystemConecto.AutorizUser.PaswdUserAutoriz == null ||
                SystemConecto.AutorizUser.LoginUserAutoriz.Length == 0 || SystemConecto.AutorizUser.PaswdUserAutoriz.Length == 0)
            {
                // Авторизация
                ConectoWorkSpace_InW.key_aut_ButtonDown("App_MainWindow", 0, PutchImagePanel_2, @"/Conecto®%20WorkSpace;component/Images/b52_logo.png", "1C шлюз", TypeServer);
            }
            else
            {
                // Событие точка (метод) старта приложения
                #region альтернатива Ecsport1C_MainWindow
                //System.Reflection.MethodInfo loadAppEvents = typeof(AppforWorkSpace).GetMethod(NameAutorize_);
                //if (loadAppEvents != null)
                //{
                //    // SystemConecto.ErorDebag("LoadAppEvents_" + IdApp, 2);
                //    loadAppEvents.Invoke(ConectoWorkSpace_InW, new object[] { });
                //}
                #endregion

                AppforWorkSpace.App_DEFAULT_MainWindow();
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
            AppforWorkSpace.animationImage_DEFAULT.Source = new BitmapImage(new Uri(PutchImagePanel_1, UriKind.Relative));
        }
        public static void Ecsport1C_MouseLeftButtonDown(object sender, MouseEventArgs e)
        {
            AppforWorkSpace.animationImage_DEFAULT.Source = new BitmapImage(new Uri(PutchImagePanel_2, UriKind.Relative));

        }


        public static void Ecsport1C_MouseMove(object sender, MouseEventArgs e)
        {
            AppforWorkSpace.animationImage_DEFAULT.Source = new BitmapImage(new Uri(PutchImagePanel_2, UriKind.Relative));
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
        
        
        #region Файл appplay.xml    - чтение настройки скд системы

        /// <summary>
        /// Чтение в память файла (PutchApp + @"config\user\1cshluz.xml")
        /// </summary>
        /// <param name="TypeFunc"></param>
        /// <returns></returns>
        public static bool UserConfig()
        {
            // Первая запись
            //UserconfigWorkSpace.Add("null", "null");

            XmlTextReader reader = null;
            // Dictionary<string, string> aParamAppChek = new Dictionary<string, string>(); // Параметры приложения проверка
            int ConfigVer = 1;
            string nameElement = "";
            // В потоке сбор информации о приложении
            // int idApp = 0;
            // Завершения чтения информации в файле
            int EndFileLogic = 0;

            AppPlayOptionStruct AppPlayOption = new AppPlayOptionStruct();

            string PuthFileRead = SystemConecto.PutchApp + @"config\user\1cshluz.xml";

            string PuthFileRead_start = SystemConecto.PStartup + @"config\user\1cshluz.xml";
            
            string ReadFile = SystemConecto.File_(PuthFileRead_start, 5)? PuthFileRead_start : SystemConecto.File_(PuthFileRead, 5)? PuthFileRead : ""  ;
            
            if(ReadFile.Length > 0)
            {
                try
                {
                    // Чтение обычного файла
                    reader = new XmlTextReader(ReadFile);

                    // Свойсва чтения
                    reader.WhitespaceHandling = WhitespaceHandling.None; // пропускаем пустые узлы

                    while (reader.Read())
                    {
                        if (reader.NodeType == XmlNodeType.Element || reader.NodeType == XmlNodeType.EndElement)
                            nameElement = reader.Name;
                        

                        // Проверка параметров и версии конфигурации, а также целосность данных
                        // Исключение тег ?xml = ""
                        //  nameElement - структура [Параметр структуры]_[id]_[Name1]_[Name2]

                        //string[] a_nameElement = nameElement.Split('_'); //a_nameElement[0]
                        switch (nameElement)
                        {
                            case "":

                                break;
                            case "Параметры-1С":
                                EndFileLogic++;
                                if (EndFileLogic == 2)
                                {
                                    // конец чтения
                                   
                                }
                                break;
                            // Проверка версии файла 
                            case "FileConfig":
                                if (ConfigVer == 1)
                                {
                                    // проверка отсутствует reader.LocalName == "FileConfig_Ver-File-Config" - && reader.GetAttribute(x) == aParamAppPlay["FileConfig_Ver-File-Config"].Version
                                    ConfigVer = 0;
                                    AppPlayOption.Version = reader.GetAttribute("FileConfig_Ver-File-Config");

                                }
                                break;
                            case "AppOverall":

                              
                                // Запись праметров
                                // AppPlayOption.id = Convert.ToInt32(reader.GetAttribute("idApp"));
                               

                                break;
                            case "OpciiOverall":
                                UserconfigWorkSpace.Add("BDSERVER_Alias", reader.GetAttribute("BDSERVER_Alias"));
                                UserconfigWorkSpace.Add("BDSERVER_IP", reader.GetAttribute("BDSERVER_IP"));
                                UserconfigWorkSpace.Add("BDSERVER_Port", reader.GetAttribute("BDSERVER_Port"));
                                UserconfigWorkSpace.Add("BDSERVER_Putch-Hide", reader.GetAttribute("BDSERVER_Putch-Hide"));
                                UserconfigWorkSpace.Add("Export_PutchDefault", reader.GetAttribute("Export_PutchDefault"));
                                // TYPE;CODE <OTEL;03,RESTORAN;25> <RESTORAN;25> тип и код подразделения авторизации
                                UserconfigWorkSpace.Add("TypeAutorizeSDR", reader.GetAttribute("TypeAutorizeSDR"));
                                UserconfigWorkSpace.Add("TypeAutorizeSPORG", reader.GetAttribute("TypeAutorizeSPORG"));
                                UserconfigWorkSpace.Add("TypeAutorizeSDRCach", reader.GetAttribute("TypeAutorizeSDRCach"));
                                
                                break;
                            default:

                                //  Проверка параметров присутствие Расшифровка параметра reader.LocalName - структура [id]_[Name1]_[Name2]

                                // Создаем масив 
                                // SystemConecto.aParamApp.Add(reader.Name, "");
                                // reader.AttributeCount //Количесвто атрибутов
                                // reader.MoveToAttribute   // Перемещает по индексу на атрибут
                                // Чтение отрибутов универсальное
                                if (reader.HasAttributes)
                                {
                                    for (int x = 0; x < reader.AttributeCount; x++)
                                    {
                                        // Перейти к атрибуту
                                        reader.MoveToAttribute(x);



                                        // Запись в память
                                        // Проверка (на всякий случай) добавленных переменных
                                        if (UserconfigWorkSpace.ContainsKey(reader.LocalName))
                                        {

                                            UserconfigWorkSpace[reader.LocalName] = reader.GetAttribute(x);
                                        }
                                        else
                                        {
                                            // !---Установить проверку динамических параметров---
                                            UserconfigWorkSpace.Add(reader.LocalName, reader.GetAttribute(x));

                                            // Запись отклонений
                                            // SystemConecto.ErorDebag("Отсутствует элемент в конфигурации: " + reader.LocalName + ", значение - " + reader.GetAttribute(x) + " Тег: " + nameElement); // Отладка
                                            // ConfigVer = 2;
                                        }


                                    }
                                }
                                //  <OpciiOverall BDSERVER_Alias="" BDSERVER_IP="" BDSERVER_Putch-Hide=""/>


                                break;

                        }

                    }

                    // Запись в память


                    // Проверка версии файла конфигурации
                    if (ConfigVer > 0)
                    {
                        // Исправить файл конфигурации
                        //SystemConecto.ErorDebag("Конфигурация не верна, приложение приступило к исправлению..."); // Отладка
                        // запись измененных параметров
                        //if (reader != null)
                        //    reader.Close();
                        //CreateConfigXMLAppPlay(2);
                        //return true;
                    }

                    if (reader != null)
                         reader.Close();
                    

                }
                catch (Exception ex)
                {
                    SystemConecto.ErorDebag("При загрузке файла " + ReadFile.ToString() + ", возникло исключение: " + Environment.NewLine +
                        " === Message: " + ex.Message.ToString() + Environment.NewLine +
                        " === Exception: " + ex.ToString(), 1);
                    if (reader != null)
                        reader.Close();
                    
                    // Перезапись по умолчанию параметров (нарушена конфигурация)
                    SystemConecto.ErorDebag("Конфигурация не верна, приложение востанавливается из резервной копии..."); // Отладка
                    //CreateConfigXMLAppPlay(2);
                    return false;
                }
            }
            else
            {
                
                // файл отсутствует
                return false;
            }

            return true;
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


