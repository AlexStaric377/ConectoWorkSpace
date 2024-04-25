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

using FirebirdSql.Data.FirebirdClient;
using FirebirdSql.Data.Isql;

using System.Data;              
using System.Data.SqlClient;

// Управление Xml
using System.Xml;
using System.Xml.Linq;

// Код шлюза
using ConectoWorkSpace;
using ConectoWorkSpace._Front;


namespace ConectoWorkSpace
{
    public partial class AppforWorkSpace
    {

        
        #region Окно Абоненского терминала

        /// <summary>
        /// ссылка на объект изображение основной панели
        /// </summary>
        public static Image AnimationImage_127 { get; set; }

        /// <summary>
        /// Загрузка событий кнопки 1С
        /// </summary>
        /// <param name="sender"></param>
        public static void LoadAppEvents_127(Window _WindowOwner, object sender, Image animationImage_)
        {
            AnimationImage_127 = animationImage_;

            if (sender is Image)
            {
                Image Image_WorkS = (Image)sender;

                Image_WorkS.MouseLeftButtonUp += new MouseButtonEventHandler(AppPlayStory_Front.Anhor_MouseLeftButtonUp);

                Image_WorkS.MouseLeave += new MouseEventHandler(AppPlayStory_Front.Anhor_MouseLeave);  // старая разработка Ecsport1C_MouseLeave

                Image_WorkS.MouseLeftButtonDown += new MouseButtonEventHandler(AppPlayStory_Front.Anhor_MouseLeftButtonDown);
            }
        }



        #endregion

    }

}

namespace ConectoWorkSpace._Front
{
    class AppPlayStory_Front
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
                "<FileConfig FileConfig_Ver-File-Config=\"0_1\" PuthFile=\"" + @"config\user\" + "\" /><AppOverall idApp=\"127\" />" + Environment.NewLine +
                " <OpciiOverall_127 BDSERVER_Alias=\"NameAliasBD\" BDSERVER_IP=\"127.0.0.1\" BDSERVER_Putch-Hide=\"\" " + Environment.NewLine +
                " ВключениеРежимаОтладки_настройка_модулей=\"true\" /> " + Environment.NewLine +
                "</Параметры-ОбменаДанными>";


        #endregion

        #region Состояние клавиш на панели
        public static void Anhor_MouseLeave(object sender, MouseEventArgs e)
        {
            AppforWorkSpace.AnimationImage_127.Source = new BitmapImage(new Uri(PutchImagePanel_1, UriKind.Relative));
        }
        public static void Anhor_MouseLeftButtonDown(object sender, MouseEventArgs e)
        {
            AppforWorkSpace.AnimationImage_127.Source = new BitmapImage(new Uri(PutchImagePanel_2, UriKind.Relative));

        }


        public static void Ecsport1C_MouseMove(object sender, MouseEventArgs e)
        {
            AppforWorkSpace.AnimationImage_127.Source = new BitmapImage(new Uri(PutchImagePanel_2, UriKind.Relative));
        }
        #endregion


        #region Событие авторизации

        public static void Anhor_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {


            int Idcount = 0; string PuthFile = ""; string Other_PuthFile = ""; string FileExe = "";
            string StrCreate = "SELECT * FROM LISTIPRDP WHERE LISTIPRDP.PANEL_APPSTARTMETOD = 'ConectoWorkSpace.AppforWorkSpace.LoadAppEvents_127'";
            DBConecto.DBopenFBConectionMemory(SystemBD.ConnnStringSystem().ToString(), "", "FbSystem");
            FbCommand SelectTable = new FbCommand(StrCreate, DBConecto.bdFbSystemConect);
            FbDataReader ReadOutTable = SelectTable.ExecuteReader();
            while (ReadOutTable.Read())
            {
                PuthFile = ReadOutTable[4].ToString();
                Other_PuthFile = ReadOutTable[21].ToString();
                Idcount++;
            }
            ReadOutTable.Close();
            SelectTable.Dispose();
            DBConecto.DBcloseFBConectionMemory("FbSystem");

            PuthFile = PuthFile != "" ? PuthFile : Other_PuthFile;
            FileExe = PuthFile != "" ? PuthFile.Substring(PuthFile.LastIndexOf(@"\")+1, PuthFile.IndexOf(".")- (PuthFile.LastIndexOf(@"\")+ 1)) : "";

            if (FileExe == "" || !File.Exists(PuthFile))
            {
                var TextWindows = "Не указан путь к файлу приложения." + Environment.NewLine + "Измените настройки. ";
                int AutoClose = 1;
                int MesaggeTop = -170;
                int MessageLeft = 500;
                ConectoWorkSpace.MainWindow.MessageEnd(TextWindows, AutoClose, MesaggeTop, MessageLeft);
                return;

            }

            Process[] Front = Process.GetProcessesByName(FileExe);
            if (Front.Length == 0)
            {
               AppStart.RenderInfo Arguments01 = new AppStart.RenderInfo() { };
                Arguments01.argument1 = PuthFile;
                Arguments01.argument2 = FileExe;
                Thread thStartTimer01 = new Thread(RunFront);
                thStartTimer01.SetApartmentState(ApartmentState.STA);
                thStartTimer01.IsBackground = true; // Фоновый поток
                thStartTimer01.Start(Arguments01);

            }
        }
        #endregion

        public static void RunFront(object ThreadObj)
        {
            AppStart.RenderInfo arguments = (AppStart.RenderInfo)ThreadObj;
            string PuthFile = arguments.argument1;
            string FileExe = arguments.argument2;
            int Indexrun = 0;
 
            // Запуск фронта
            if (!AppStart.SetFocusWindow(FileExe))
            {

                var TextWindows = "Выполняется запуск программы" + Environment.NewLine + "Пожалуйста подождите. ";
                int AutoClose = 1;
                int MesaggeTop = -170;
                int MessageLeft = 500;
                ConectoWorkSpace.MainWindow.MessageInstall(TextWindows, AutoClose, MesaggeTop, MessageLeft);

                // запуск сервера ключа.
                Process[] FB25 = Process.GetProcessesByName("grdsrv");
                if (FB25.Length == 0)
                {
                    ConectoWorkSpace.InstallB52.InstallTHKeyServ();
                    Thread.Sleep(2000);
                }

                Process p_hendl = System.Diagnostics.Process.Start(PuthFile);
                p_hendl.WaitForInputIdle();

                for (int i = 0; i <= 0; i++)
                {

                    // Устанавливаем нужные размеры FileExe - для фронта может быть другое имя
                    if (TextPasteWindow.SetFocusWindow(FileExe))
                    {

                        IntPtr handleM = Process.GetCurrentProcess().MainWindowHandle;
                        // выбрать фокус conecto
                        //TextPasteWindow.ShowWindow(handleM, 9);
                        AppStart.SetForegroundWindow(handleM);
                        TextPasteWindow.SetParent(p_hendl.MainWindowHandle, handleM);
                        // выбрать фокус фронта
                        AppStart.SetForegroundWindow(p_hendl.MainWindowHandle);
                        if (!TextPasteWindow.SetWindowPos(TextPasteWindow.hControl, HWND.TOPMOST, 200, 200, 0, 0, SWP.SHOWWINDOW | SWP.NOSIZE)) //
                        {
                            SystemConecto.ErorDebag("Ошибка работы функции: SetWindowPos: окно " + FileExe);
                        }
                        // выбрать фокус фронта
                        AppStart.SetForegroundWindow(p_hendl.MainWindowHandle);
                        break;
                    }
                    else
                    {

                        Indexrun++;
                        if (Indexrun > 300) break;
                        Thread.Sleep(1000);
                        i = -1;

                    }
                }
            }
        }


        #region Окна
        /// <summary>
        /// Окно идентификации пользователя<para></para>
        ///  NameWindow - Системное имя NameAutorize_,<para></para>
        ///  TypeAutoriz - Тип авторизации,<para></para> 
        ///  2-Ссылка на доп. Изображение справа,<para></para>
        ///  3-Ссылка на доп. Изображение сверху<para></para>
        ///  TypeServer - настройки сервера идентификации
        ///  0 - Сервер учетных данных (FB, MSSQL, LDAP, WEB server, MySql, UserConecto)<para></para>
        ///  1 - Сервер-IP<para></para>
        ///  2 - Cервер-Alias<para></para>
        ///  3 - Cервер-DopParam<para></para>
        ///  4 - Cервер-DopParam-Тип БД: B52-дополнительный индекс к логину (при смене пароля непонятная ситуация)<para></para>
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public static void key_aut_ButtonDown(string NameWindow, int TypeAutoriz = 0, string LinkPicRight = "", string LinkPicTop = "", string TextPic = "", string[] TypeServer = null)
        {

            MainWindow ConectoWorkSpace_InW = AppStart.LinkMainWindow();

            TypeServer = TypeServer == null ? new string[6] { "", "", "", "", "","3055" } : TypeServer;
            // Параметры
            SystemConecto.Autirize = new string[11] { NameWindow, TypeAutoriz.ToString(), LinkPicRight, LinkPicTop, TextPic,
                                TypeServer[0], TypeServer[1], TypeServer[2], TypeServer[3], TypeServer[4],TypeServer[5] };

            Window Window = SystemConecto.ListWindowMain("WaitFonW");

            if (Window != null)
            {
                // Не активировать окно - не передавать клавиатурный фокус
                Window.ShowActivated = false;
                Window.Show();
            }
            else
            {
                Window = new WaitFon();
                Window.Owner = ConectoWorkSpace_InW; // this;
                // Не активировать окно - не передавать клавиатурный фокус
                Window.ShowActivated = false;
                Window.Show();
            }
            // Изменить прозрачность вызываемого окна
            var FonWindow = (Grid)LogicalTreeHelper.FindLogicalNode(Window, "WindGrid");
            FonWindow.Opacity = 0.80;

            Autiriz AutirizWindow = new Autiriz(NameWindow);
            AutirizWindow.Owner = ConectoWorkSpace_InW; // this;  //AddOwnedForm(OblakoNizWindow);
            // размещаем на рабочем столе
            //AutirizWindow.Top = (this.Top + 7) + this.Close_F.Margin.Top + (this.Close_F.Height - 2) - 20;
            //AutirizWindow.Left = (this.Left + 7) + this.Close_F.Margin.Left - (AutirizWindow.Width - 22) + 20;
            // Отображаем
            AutirizWindow.ShowDialog();
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


