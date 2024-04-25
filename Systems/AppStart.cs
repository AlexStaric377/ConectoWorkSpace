
using System.Threading.Tasks;
using ConectoWorkSpace;
using System;
using System.Collections;
using System.Drawing.Drawing2D;
using Main_Window;

#region импорт следующих имен пространств .NET:
//---- объекты ОС Windows (Реестр, {Win Api} 
using Microsoft.Win32;

using System.Collections.Generic;
using IWshRuntimeLibrary;
// Управление Изображениями
using System.ComponentModel;
// Управление БД
using System.Data;
// --- Process
using System.Diagnostics;
using System.Drawing;
using System.ServiceProcess;
// Ссылка в проекте MSV2010 добовляется ...
using System.Drawing.Text;
// Подключение GAC for Net для Fireberd
using System.EnterpriseServices.Internal;
// локаль операционной системы
using System.Globalization;
// Управление вводом-выводом
using System.IO;
using System.IO.Compression;
using System.Linq;
using FirebirdSql.Data.FirebirdClient;  
using FirebirdSql.Data.Isql;


using System.Data.SqlClient;
// Удаленное управление компьютером
using System.Management;
// Управление сетью
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
//--- для Проверки Сборок
using System.Reflection;
// Импорт библиотек Windows DllImport (управление питанием ОС, ...
using System.Runtime.InteropServices;
// шифрование данных
using System.Security.Cryptography;
using System.Security.Permissions;
using System.Text;
/// Многопоточность
using System.Threading;
// --- Timer
using System.Timers;
using System.Windows;
using System.Windows.Controls;
//--- WPF
using System.Windows.Media;
using System.Windows.Threading;
// Управление Xml
using System.Xml;
using System.Xml.Linq;
// --- Заставка
using ConectoWorkSpace.Splasher_startWindow;

#endregion


namespace ConectoWorkSpace
{
    public partial class AppStart
    {
        // Для FTP клиента
        public static FtpWebResponse NetFTPResponse = null;

        #region  Стартовая проверка необходимых компонентов в ОС для работы Приложния

        /// <summary>
        ///  Объявить переменную глобально
        ///  public object error;
        /// </summary
        public static Dictionary<string, string> TableReestr = new Dictionary<string, string>();

        // Путь к ключу, где Windows размещает в реестре автоматичекий запуск приложений после загрузки explorer.exe для пользователей
        
 
        public static RegistryKey rkStartApp = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\Microsoft\Windows NT\CurrentVersion\Winlogon", true);
        // Путь к ключу, где Windows размещает в реестре автоматичекий запуск приложений до загрузки explorer.exe для Терминала Fornt

        //public static RegistryKey rkStartAppTerminal = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\Windows NT\CurrentVersion\Winlogon", true);
        //public static RegistryKey AutoAdminLogon = rkStartAppTerminal;


        // Путь к ключу, где ConectoWorkSpace размещает в реестре свои данные
        //public static RegistryKey rkCreateSubKeyHkeyUsers = Registry.Users.CreateSubKey(@"System\Alt-Tab\App");
        //public static RegistryKey rkOpenSubKeyHkeyUsers = Registry.Users.OpenSubKey(@"System\Alt-Tab\App", true);

        public static RegistryKey rkAppSeting = Registry.CurrentUser.CreateSubKey(@"System\Alt-Tab\App",true);
        public static RegistryKey rkAppSetingApp = Registry.CurrentUser.OpenSubKey(@"System\Alt-Tab\App", true); //HKEY_CURRENT_USER\
        public static RegistryKey rkAppSetingAllUser = Registry.CurrentUser.OpenSubKey(@"System\Alt-Tab\App" , true);
        //AppStart.rkAppSetingAllUser = Registry.CurrentUser.OpenSubKey(@"System\Alt-Tab\App\" +  AppDomain.CurrentDomain.BaseDirectory , true); // SystemConecto.PutchApp
        public static RegistryKey reglocalMachine = Registry.CurrentUser.CreateSubKey(@"System\Alt-Tab\App"); //RegistryKey.OpenBaseKey(Microsoft.Win32.RegistryHive.LocalMachine, RegistryView.Registry64)
        public static Dictionary<string, string> FontMain = new Dictionary<string, string>(); // Установка шрифтов
        public static int SystemNoBD = 0; // Системная БД выключенна, ограничения по работе с системой.
        public XmlTextWriter WriterConfigXML = null;

        // Windows  32 разрядная
        public static RegistryKey localMachine = RegistryKey.OpenBaseKey(Microsoft.Win32.RegistryHive.LocalMachine, RegistryView.Registry32);
        public static RegistryKey CurrentMachine = RegistryKey.OpenBaseKey(Microsoft.Win32.RegistryHive.CurrentUser, RegistryView.Registry32);

        #region Пример работы с шрифтами
        /// <summary>
        /// Частная колекция щрифтов для FORM
        /// </summary>
        //public static System.Drawing.Text.PrivateFontCollection fontPrivate = new System.Drawing.Text.PrivateFontCollection();

        // Добовление шрифтов в ОС Windows
        //[DllImport("gdi32", EntryPoint = "AddFontResource")]
        //public static extern int AddFontResourceA(string lpFileName);

        //Удаление шрифтов
        //[DllImport("gdi32", EntryPoint = "RemoveFontResource")]
        //public static extern int RemoveFontResourceA(string lpFileName);
        #endregion


        /// <summary>
        /// Проверка необходимых компонентов в ОС для работы Приложния
        /// </summary>
        /// <param name="Parametrs"> Проверка только одного компонента (параметра ОС), используется для уточнения</param>
        /// <returns>Возвращаем цыфровой код ответа, 0 - все в порядке</returns>




        [DllImport("Kernel32.dll", CallingConvention = CallingConvention.StdCall)]
        public static extern bool SetDllDirectory(String lpPathName);
        public static int StartReshenie = 0;
        /// <summary>
        /// Parametrs -string Parametrs = "all"
        /// </summary>
        /// <param name="Parametrs"></param>
        /// <returns></returns>
        public static int IsCheckOS()
        {



            #region  Проверка шрифтов
            // Результат проверки
            var RezChek = 0;

            // Список шрифтов
            Dictionary<string, string> FontList = new Dictionary<string, string>(); // Установка шрифтов
            FontList.Add("Swiss921 BT", "SWZ921N.TTF");
            FontList.Add("Myriad Pro Cond", "MYRIADPRO-BOLDCOND.OTF");



            foreach (KeyValuePair<string, string> Fontdani in FontList)
            {

                if (SystemConecto.IsFontInstalled(Fontdani.Key))
                {
                    //FontList.Add("Swiss921 BT", "SWZ921N.TTF");
                    FontMain[Fontdani.Key] = Fontdani.Key;
                    SystemConecto.ErorDebag("Шрифт: " + Fontdani.Key + " подключен на устройстве.", 1);
                }
                else
                {
                    if (SystemConecto.File_(SystemConecto.PutchApp + @"bin\" + Fontdani.Value, 5))
                    {
                        // Скачивание файлов
                        FontMain[Fontdani.Key] = "file:///" + SystemConecto.PutchApp + @"bin\#" + Fontdani.Key;
                        // Инсталлирование в системную папку (не получилось)
                        // SystemConecto.IsFilesPack("C:\\Windows\\Fonts\\" + "SWZ921N.TTF");
                        // В свою папку да.
                        // А также можно интегрировать в компиляцию, для этого нужно чтобы в лицензии было указанно 
                        // на возможность интеграции с программами
                        // Пример использования
                        // Create a new FontFamily object, using an absolute URI reference.
                        //myTextBlock.FontFamily = new FontFamily("file:///d:/MyFonts/#Pericles Light");

                        //XAML

                        //<TextBlock FontFamily="file:///d:/MyFonts/#Pericles Light">
                        //  Aegean Sea
                        //</TextBlock>

                    }
                    else
                    {
                        var MessageIFP = new SystemConecto.MessageIFP();
                        SystemConecto.IsFilesPack(new KeyValuePair<string, string>(SystemConecto.PutchApp + @"bin\" + Fontdani.Value, ""), ref MessageIFP);
                        if (SystemConecto.File_(SystemConecto.PutchApp + @"bin\" + Fontdani.Value, 5))
                        {
                            FontMain[Fontdani.Key] = "file:///" + SystemConecto.PutchApp + @"bin\#" + Fontdani.Key;
                        }
                        else
                        {
                            //RezChek = 0;
                        }

                    }
                }

            }

            #region Еще информация о шрифтах
            // Удаление пример для разработки (например удалить шрифт)
            //var result = RemoveFontResourceA(@"C:\Users\Program\Downloads\Отладка\Font\Swiss921-BT-Regular.ttf");

            //if (result > 0)
            //{
            //    //Console.WriteLine((result == 0) ? "Font was not found." : "Font removed successfully.");
            //    RezChek = 1;
            //}

            // Попытка инсталяции в ОС (не получилось)
            // var result = AddFontResourceA(SystemConecto.PutchApp + @"bin\SWZ921N.TTF");

            //-- Для Form запись в частную колекцию (не проверил но вроде работает)
            // fontPrivate.AddFontFile(SystemConecto.PutchApp + @"bin\SWZ921N.TTF");
            // Пример использования
            // label1.Font = new Font(fontPrivate.Families[0], 11);

            // Пример чаастного случая инсталляции шрифта в ОС Windows
            //execute("C:\\Windows\\Fonts\\calibri.ttf" Yes, ExeShowNormal);
            #endregion

            if (App.aSystemVirable["UserWindowIdentity"] != "1")
            {
                return RezChek;
            }
            else
            {
                RezChek = 1;
            }

            #endregion

            #region  Важные файлы

            // Проверка файлов Сервера БД
            Dictionary<string, string> fbembedList = new Dictionary<string, string>();
            fbembedList.Add(SystemConecto.PutchApp + @"data\fbembed.dll", "");
            fbembedList.Add(SystemConecto.PutchApp + @"data\icudt30.dll", "");
            fbembedList.Add(SystemConecto.PutchApp + @"data\ib_util.dll", "");
            fbembedList.Add(SystemConecto.PutchApp + @"data\icuin30.dll", "");
            fbembedList.Add(SystemConecto.PutchApp + @"data\icuuc30.dll", "");
            fbembedList.Add(SystemConecto.PutchApp + @"data\Microsoft.VC80.CRT.manifest", "");
            fbembedList.Add(SystemConecto.PutchApp + @"data\msvcp80.dll", "");
            fbembedList.Add(SystemConecto.PutchApp + @"data\msvcr80.dll", "");
            fbembedList.Add(SystemConecto.PutchApp + @"data\firebird.msg", "");
 
            if (SystemConecto.IsFilesPRG(fbembedList, 1, "- Проверка файлов при старте") == "True")RezChek = 0;
            else return RezChek= 1;
            // MessageBox.Show("Нет файлов");
 
            #endregion


            #region  Очень Важные и Необходимые файлы 

            Dictionary<string, string> fbembedList_ = new Dictionary<string, string>();
 
            fbembedList_.Add(SystemConecto.PutchApp + @"bin\dll\Interop.ShockwaveFlashObjects.dll", "");
            fbembedList_.Add(SystemConecto.PutchApp + @"bin\dll\AxInterop.ShockwaveFlashObjects.dll", "");
            fbembedList_.Add(SystemConecto.PutchApp + @"bin\dll\WpfAnimatedGif.dll", "");
            fbembedList_.Add(SystemConecto.PutchApp + @"bin\dll\Hardcodet.Wpf.TaskbarNotification.dll", "");
            fbembedList_.Add(SystemConecto.PutchApp + @"bin\dll\FirebirdSql.Data.FirebirdClient.dll", "");
            fbembedList_.Add(SystemConecto.PutchApp + @"bin\dll\DBConecto.dll", "");
            //fbembedList_.Add(SystemConecto.PutchApp + @"bin\dll\DBConecto.pdb", "");
            fbembedList_.Add(SystemConecto.PutchApp + @"bin\dll\System.Data.Common.dll", "");
            fbembedList_.Add(SystemConecto.PutchApp + @"bin\dll\System.Diagnostics.StackTrace.dll", "");
            fbembedList_.Add(SystemConecto.PutchApp + @"bin\dll\System.Diagnostics.Tracing.dll", "");
            fbembedList_.Add(SystemConecto.PutchApp + @"bin\dll\System.Globalization.Extensions.dll", "");
            fbembedList_.Add(SystemConecto.PutchApp + @"bin\dll\System.IO.Compression.dll", "");
            fbembedList_.Add(SystemConecto.PutchApp + @"bin\dll\System.Net.Http.dll", "");
            fbembedList_.Add(SystemConecto.PutchApp + @"bin\dll\System.Net.Sockets.dll", "");
            fbembedList_.Add(SystemConecto.PutchApp + @"bin\dll\System.Runtime.CompilerServices.Unsafe.dll", "");
            fbembedList_.Add(SystemConecto.PutchApp + @"bin\dll\System.Runtime.CompilerServices.Unsafe.xml", "");
            fbembedList_.Add(SystemConecto.PutchApp + @"bin\dll\System.Runtime.Serialization.Primitives.dll", "");
            fbembedList_.Add(SystemConecto.PutchApp + @"bin\dll\System.Security.Cryptography.Algorithms.dll", "");
            fbembedList_.Add(SystemConecto.PutchApp + @"bin\dll\System.Security.SecureString.dll", "");
            fbembedList_.Add(SystemConecto.PutchApp + @"bin\dll\System.Text.Encoding.CodePages.dll", "");
            fbembedList_.Add(SystemConecto.PutchApp + @"bin\dll\System.Threading.Overlapped.dll", "");
            fbembedList_.Add(SystemConecto.PutchApp + @"bin\dll\System.Xml.XPath.XDocument.dll", "");
            fbembedList_.Add(SystemConecto.PutchApp + @"bin\dll\SystemConecto.dll", "");
            //fbembedList_.Add(SystemConecto.PutchApp + @"bin\dll\SystemConecto.pdb", "");

            RezChek = 0;
            if (SystemConecto.IsFilesPRG(fbembedList_, -1, " - Проверка файлов при старте") == "True")
            {
                #region ChekGACFireberd
                try
                {
                    // Проверка зборки FirebirdSql.Data.FirebirdClient.dll, допускаю, что может быть установлена стара версия
                    if (GACGet_FB()) { }
                }
                catch (Exception ex)
                {
                    SystemConecto.ErorDebag(ex.ToString());
                    return RezChek = 1;
                }

                #endregion
            }
            else
            {
               SystemConecto.ErorDebag("Нет Очень Важных и Необходимых файлов при старте");
               return RezChek = 1;
            }

            SystemConecto.ErorDebag("Проверка  файлов при старте успешна");

            #endregion

            // Библиотеки слyжбы сервисов Conecto
            Dictionary<string, string> ServiceList_ = new Dictionary<string, string>();

            ServiceList_.Add(SystemConectoServers.PutchLib + "DBServices.dll", "dbservis/");
            if (SystemConecto.IsFilesPRG(ServiceList_, -1, "- Загрузка сервиса при старте") == "True")RezChek = 0;
            else RezChek = 1;
 
            // ================ Запуск авто служб и их событий

            SystemConecto.ErorDebag("Запуск авто служб и их событий успешно");
            return RezChek;
        }
        #endregion

        #region Проверка наличия обновлений ConectoWorkSpace.

        /// <summary>
        /// Parametrs -string Parametrs = "all"
        /// </summary>
        /// <param name="Parametrs"></param>
        /// <returns></returns>
        public static int IsCheckUpdateWorkSpace()
        {
            int RezChek = 0;  
            // Проверка файлов Обновлений
            Dictionary<string, string> fbembedList = new Dictionary<string, string>();
            fbembedList.Add(SystemConecto.PutchApp + @"UpdateConectoWorkSpace.txt", "UpdateWorkSpace/");
            if (SystemConecto.IsFilesPRG(fbembedList, 1, "- Проверка файлов при старте") == "True") return  RezChek = 1;
            else
            {
               // MessageBox.Show("Нет файлов");
                SystemConecto.ErorDebag("Обновлений нет");
                return RezChek;
            } 
 
 
        }
        #endregion

        #region Проверка наличия Framework.

        /// <summary>
        /// Проверка наличия Framework и его версии
        /// </summary>
        /// <returns></returns>
        public static int IsCheckFrameWork()
        {

            var RezChek = 0;  // Результат проверки негативный
            const string subkey = @"SOFTWARE\Microsoft\NET Framework Setup\NDP\v4\Full\";
            using (var ndpKey = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Registry32).OpenSubKey(subkey))
            {
                App.VFrameWork = IsFrameWork();
                if (IsFrameWork() == "0" || (int)ndpKey.GetValue("Release") < 461308)
                {
                    SystemConecto.ErorDebag("Версия Framework " + App.VFrameWork, 0);
                    // Framework не установлен. Надо установить.
                    Dictionary<string, string> ServiceList = new Dictionary<string, string>();
                    ServiceList.Add(SystemConectoServers.PutchLib + "NDP471-KB4033344-Web.exe", "FrameWork/");
                    if (SystemConecto.IsFilesPRG(ServiceList, -1, "- Проверка файлов при старте") != "True")
                    {

                        SystemConecto.ErorDebag("Отсутствует инсталяционный  файл установки Framework" + Environment.NewLine + "  Установка прекращена. ", 0, 1);
                        return RezChek = 1;
                    }
                    RezChek = 1;
                    string FileExe = SystemConectoServers.PutchLib + "NDP471-KB4033344-Web.exe";
                    System.Diagnostics.Process.Start(FileExe);
                }

            }

            return RezChek;

        }


        /// <summary>
        /// Метод проверки версии Framework в рестре Windows
        /// </summary>
        /// <returns></returns>
        public static string IsFrameWork()
        {
            string Verciya = "0";
            const string subkey = @"SOFTWARE\Microsoft\NET Framework Setup\NDP\v4\Full\";
            using (var ndpKey = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Registry32).OpenSubKey(subkey))
            {
                if (ndpKey != null && ndpKey.GetValue("Release") != null)
                {
                    Verciya = CheckFor45PlusVersion((int)ndpKey.GetValue("Release")).ToString();
                }
            }

            return Verciya;
        }

        /// <summary>
        /// Определение версии FrameWork
        /// </summary>
        /// <param name="releaseKey">0 - не определенная версия</param>
        /// <returns></returns>
        public static string CheckFor45PlusVersion(int releaseKey)
        {
            if (releaseKey >= 528040) return "4.8";
            if (releaseKey >= 461808) return "4.7.2";
            if (releaseKey >= 461308) return "4.7.1";
            if (releaseKey >= 460798) return "4.7";
            if (releaseKey >= 394802) return "4.6.2";
            if (releaseKey >= 394254) return "4.6.1";
            if (releaseKey >= 393295) return "4.6";
            if (releaseKey >= 379893) return "4.5.2";
            if (releaseKey >= 378675) return "4.5.1";
            if (releaseKey >= 378389) return "4.5";
            return "0";
        }


        #endregion


        #region Проверка наличия системной БД и ее содержания.

        /// <summary>
        /// Parametrs -string Parametrs = "all"
        /// </summary>
        /// <param name="Parametrs"></param>
        /// <returns></returns>
        public static int IsCheckSYSBD()
        {
            //SystemConecto.ErorDebag("Step 2");
            var RezChek = 1;  // Результат проверки негативный
            // Права пользователь
            if (App.aSystemVirable["UserWindowIdentity"] == "1") 
            {
                 // Проверка наличия системной БД и ее содержания
                if (!File_(SystemConecto.PutchApp + @"data\" + "system.fdb", 5))
                {
                    //DBConecto.DBopenFBConectionMemory(SystemBD.ConnnStringSystem().ToString(), "", "FbSystem");
                    var Test = SystemBD.CreateBDSystem();
                    //DBConecto.DBcloseFBConectionMemory("FbSystem");
                }
                // процедура открытия доступа к БД
                //string Temp = SystemBD.ConnnStringSystem().ToString();
                try
                {
                    // ? что это !!!!
                    if (ConectoWorkSpace.Administrator.AdminPanels.LoadTableRestr == 0)
                    {
                        DBConecto.DBopenFBConectionMemory(SystemBD.ConnnStringSystem().ToString(), "", "FbSystem");
                        // Проверка наличия таблиц в БД
                        DBConecto.UniQuery CheckQuery = new DBConecto.UniQuery("select t.rdb$relation_name from rdb$relations t where t.rdb$relation_name = 'CONFIGSOFT'", "FB"); // разаработать FBnoS подключение для не системных таблиц
                        CheckQuery.ExecuteQueryFillTable("Cursor_CONFIGSOFT");
                        if (CheckQuery.CacheDBAdapter != null && CheckQuery.CacheDBAdapter.Tables["Cursor_CONFIGSOFT"].Rows.Count <= 0)
                        {
                            string StrCreate = "CREATE TABLE ConfigSoft  (ID  NUMERIC(5)  NOT  NULL, " +
                                                    "NAMEVAR  VARCHAR(40)    NOT NULL, " +
                                                    "SETVAR  VARCHAR(50)  NOT NULL, " +
                                                    "PRIMARY KEY (ID));";
                            FbConnection bdFBSqlConect = new FbConnection(SystemBD.ConnnStringSystem().ToString());
                            bdFBSqlConect.Open();
                            FbCommand CreateTable = new FbCommand(StrCreate, DBConecto.bdFbSystemConect);
                            CreateTable.CommandType = CommandType.Text;
                            string cret = CreateTable.ExecuteScalar() == null ? "" : CreateTable.ExecuteScalar().ToString();
                            // Процедуры инициализации переключателей на панелях решений
                            CreateTable.Dispose();
                            bdFBSqlConect.Close();

                            ConectoWorkSpace.Administrator.AdminPanels.InitKeyOnOffServBD();
                            ConectoWorkSpace.Administrator.AdminPanels.InitKeyOnOffBack();
                            ConectoWorkSpace.Administrator.AdminPanels.InitKeyOnOffFront();
                            ConectoWorkSpace.Administrator.AdminPanels.InitKeyOnOffShluz();
                            ConectoWorkSpace.Administrator.AdminPanels.InitKeyOnOffServisBD();
                            ConectoWorkSpace.Administrator.AdminPanels.InitTextFront();
                            ConectoWorkSpace.Administrator.AdminPanels.InitTextBack();
                            ConectoWorkSpace.Administrator.AdminPanels.InitTextShluz();
                            ConectoWorkSpace.Administrator.AdminPanels.InitTextServBD();
                            ConectoWorkSpace.Administrator.AdminPanels.InitTextServisBD();
                            ConectoWorkSpace.Administrator.AdminPanels.InitKeyOnOfflicenziyaKey();
                            ConectoWorkSpace.Administrator.AdminPanels.InitTextlicenziyaKey();
                            ConectoWorkSpace.Administrator.AdminPanels.InitKeyOnOffSystemSuport();
                            ConectoWorkSpace.Administrator.AdminPanels.InitTextSystemSuport();
                            ConectoWorkSpace.Administrator.AdminPanels.InitKeyOnOffConectoServis();
                            ConectoWorkSpace.Administrator.AdminPanels.InitKeyReestr("SetKeyServerData", "0");
                            ConectoWorkSpace.Administrator.AdminPanels.InitKeyReestr("CreateShluzOn", "0");
                            ConectoWorkSpace.Administrator.AdminPanels.InitKeyReestr("CheckTTF16", "0");

                        }
                        else
                        {
                            // Запрос к серверу на чтение таблицы

                            int CountConfigSoft = 0;
                            CheckQuery.UserQuery = string.Format("SELECT count(*) from CONFIGSOFT", "CONFIGSOFT");
                            string CountTable = CheckQuery.ExecuteUNIScalar() == null ? "" : CheckQuery.ExecuteUNIScalar().ToString();
                            if (Convert.ToInt32(CountTable) != 0)
                            {
                                string InsertExecute = "SELECT * from CONFIGSOFT";
                                FbCommand UpdateKey = new FbCommand(InsertExecute, DBConecto.bdFbSystemConect);
                                UpdateKey.CommandType = CommandType.Text;
                                FbDataReader reader = UpdateKey.ExecuteReader();
                                while (reader.Read())
                                {
                                    AppStart.TableReestr.Add(reader[1].ToString(), reader[2].ToString());
                                    CountConfigSoft++;
                                }
                                reader.Close();
                            }

                        }
 
                        ConectoWorkSpace.Administrator.AdminPanels.InitKeyOnOffSystemSuport();
                        ConectoWorkSpace.Administrator.AdminPanels.CreateConnectionBD25();
                        ConectoWorkSpace.Administrator.AdminPanels.CreateConnectionBD30();
                        ConectoWorkSpace.Administrator.AdminPanels.CreateConnectionPostgresql();
                        ConectoWorkSpace.Administrator.AdminPanels.CreateBackOfice();
                        ConectoWorkSpace.Administrator.AdminPanels.CreateFrontOfice();
                        ConectoWorkSpace.Administrator.AdminPanels.CreateServerActivFB25();
                        ConectoWorkSpace.Administrator.AdminPanels.CreateServerActivFB30();
                        ConectoWorkSpace.Administrator.AdminPanels.CreateServerActivPostgresql();
                        ConectoWorkSpace.Administrator.AdminPanels.CreateTestKey();
                        ConectoWorkSpace.Administrator.AdminPanels.CreateLocKey();
                        ConectoWorkSpace.Administrator.AdminPanels.CreateNetKey();
                        ConectoWorkSpace.Administrator.AdminPanels.CreateActivBackFront();
                        ConectoWorkSpace.Administrator.AdminPanels.CreateScheduleArhiv();
                        ConectoWorkSpace.Administrator.AdminPanels.CreateListIpRdp();
                        ConectoWorkSpace.Administrator.AdminPanels.CreateEtalonDevelop();
                        ConectoWorkSpace.Administrator.AdminPanels.CreateEtalonDistrybut();
                        ConectoWorkSpace.Administrator.AdminPanels.CreateProtokolCompare();
                        ConectoWorkSpace.Administrator.AdminPanels.CreateRegisterCompare();
                        ConectoWorkSpace.Administrator.AdminPanels.CreateListTablLog();

                        DBConecto.DBcloseFBConectionMemory("FbSystem");
                        RezChek = 0;

                    }
                }
                catch (FirebirdSql.Data.FirebirdClient.FbException ex)
                {
                    SystemConecto.ErorDebag(" возникло исключение: " + Environment.NewLine +
                               " === IDCode: " + ex.ErrorCode.ToString() + Environment.NewLine +
                               " === Message: " + ex.Message.ToString() + Environment.NewLine +
                               " === Exception: " + ex.ToString(), 1);

                }
                catch (Exception exf)
                {
                    SystemConecto.ErorDebag(" возникло исключение: " + Environment.NewLine +
                               " === Message: " + exf.Message.ToString() + Environment.NewLine +
                               " === Exception: " + exf.ToString(), 1);
                }
                //Копирование данных клиенту только чтение.
               DIR_(AppDomain.CurrentDomain.BaseDirectory + @"data\");
                System.IO.File.Copy(SystemConecto.PutchApp + @"data\" + "system.fdb", AppDomain.CurrentDomain.BaseDirectory + @"data\" + "system.fdb", true);
            }
            else
            {
 
                RezChek = 0;
                //// Копирование данных клиенту только чтение.
                DIR_(AppDomain.CurrentDomain.BaseDirectory + @"data\");
                System.IO.File.Copy(SystemConecto.PutchApp + @"data\" + "system.fdb", AppDomain.CurrentDomain.BaseDirectory + @"data\" + "system.fdb", true);
                //Чтение БД в память

                DBConecto.UniQuery CheckQuery = new DBConecto.UniQuery("select t.rdb$relation_name from rdb$relations t where t.rdb$relation_name = 'CONFIGSOFT'", "FB"); // разаработать FBnoS подключение для не системных таблиц
                CheckQuery.InitialCatalog = AppDomain.CurrentDomain.BaseDirectory + @"data\" + "system.fdb";
                CheckQuery.ExecuteQueryFillTable("Cursor_CONFIGSOFT");
                if (CheckQuery.CacheDBAdapter != null && CheckQuery.CacheDBAdapter.Tables["Cursor_CONFIGSOFT"].Rows.Count > 0)
                {
                    int CountConfigSoft = 0;
                    CheckQuery.UserQuery = string.Format("SELECT count(*) from CONFIGSOFT", "CONFIGSOFT");
                    string CountTable = CheckQuery.ExecuteUNIScalar() == null ? "" : CheckQuery.ExecuteUNIScalar().ToString();
                    if (Convert.ToInt32(CountTable) != 0)
                    {
                        //string InsertExecute = "SELECT * from CONFIGSOFT";
                        DBConecto.DBopenFBConectionMemory(SystemBD.ConnnStringSystem(AppDomain.CurrentDomain.BaseDirectory + @"data\" + "system.fdb").ToString(), "", "FbSystem");
                        FbCommand UpdateKey = new FbCommand("SELECT * from CONFIGSOFT",
                            DBConecto.bdFbSystemConect);
                        UpdateKey.CommandType = CommandType.Text;
                        FbDataReader reader = UpdateKey.ExecuteReader();
                        while (reader.Read())
                        {
                            AppStart.TableReestr.Add(reader[1].ToString(), reader[2].ToString());
                            CountConfigSoft++;
                        }
                        reader.Close();
                        DBConecto.DBcloseFBConectionMemory("FbSystem");
                    }
                }


            }

            ConectoWorkSpace.Administrator.AdminPanels.LoadTableRestr = 1;
            SystemConecto.ErorDebag("Проверка наличия системной БД и ее содержания успешно");
            return RezChek;
        }
        #endregion


        #region Проверка наличия системной БД и ее содержания.

        public static int IsCheckUserBD()
        {
            bool DeviceBd = false;
            var RezChek = 0;
            try
            {
                if (Convert.ToInt16(AppStart.TableReestr["CopyBdUser"]) == 2)
                {
                    // Результат проверки позитивный
                    if (AppStart.TableReestr["PuthSetBD25"] == "") return RezChek;
                    string PuthSetBD25 = AppStart.TableReestr["PuthSetBD25"].Substring(0, AppStart.TableReestr["PuthSetBD25"].IndexOf(".")) + DateTime.Now.ToString("yyyyMMdd") + ".fbd";
                    string NameDevice = PuthSetBD25.Substring(0, PuthSetBD25.IndexOf(@"\") + 1);
                    DriveInfo[] allDrives = DriveInfo.GetDrives();
                    foreach (DriveInfo IdDevice in allDrives)
                    {
                        if (IdDevice.IsReady == true && NameDevice == IdDevice.Name) DeviceBd = true;
                    }
                    if (DeviceBd == true)
                    {
                        if (!System.IO.File.Exists(PuthSetBD25)) System.IO.File.Copy(AppStart.TableReestr["PuthSetBD25"], PuthSetBD25, true);

                        string ErrMessage = "";
                        string RdbProcCount = "SELECT count(*) FROM RDB$PROCEDURES";

                        /// Создание строки доступа
                        FbConnectionStringBuilder StringCon = new FbConnectionStringBuilder();
                        StringCon.UserID = "SYSDBA";
                        StringCon.Password = AppStart.TableReestr["CurrentPasswABD25"] == "" ? "masterkey" : AppStart.TableReestr["CurrentPasswABD25"];
                        StringCon.Dialect = 3;
                        StringCon.ServerType = FbServerType.Embedded;
                        StringCon.DataSource = "localhost";
                        StringCon.Database = PuthSetBD25;
                        StringCon.Port = 3055;

                        FbConnection bdFBConect = new FbConnection(StringCon.ToString());
                        try
                        {
                            bdFBConect.Open();
                            // Считаем количество процедур в БД
                            FbCommand CountProc = new FbCommand(RdbProcCount, bdFBConect);
                            CountProc.CommandType = CommandType.Text;
                            string CountTable = CountProc.ExecuteScalar().ToString();
                            CountProc.Dispose();
                            // формируем список процедур 
                        }
                        catch (FirebirdSql.Data.FirebirdClient.FbException ex)
                        {
                            SystemConecto.ErorDebag(" возникло исключение: " + Environment.NewLine +
                               " === IDCode: " + ex.ErrorCode.ToString() + Environment.NewLine +
                               " === Message: " + ex.Message.ToString() + Environment.NewLine +
                               " === Exception: " + ex.ToString(), 1);
                            ErrMessage = "Обнаружена ошибка в БД." + Environment.NewLine + ex.ToString().Substring(ex.ToString().IndexOf(Environment.NewLine) + 2);

                        }
                        catch (Exception ex) //)
                        {
                            SystemConecto.ErorDebag("возникло исключение:" + Environment.NewLine +
                            " возникло исключение: " + Environment.NewLine +
                            " === Message: " + ex.Message.ToString() + Environment.NewLine +
                            " === Exception: " + ex.ToString(), 1);
                            ErrMessage = "Обнаружена ошибка в БД." + Environment.NewLine + ex.ToString().Substring(ex.ToString().IndexOf(Environment.NewLine) + 2);

                        }
                        bdFBConect.Dispose();
                        bdFBConect.Close();
                    }
                }
            } catch (Exception ex)
            {
                SystemConecto.ErorDebag("возникло исключение:" + Environment.NewLine +
                " возникло исключение: " + Environment.NewLine +
                " === Message: " + ex.Message.ToString() + Environment.NewLine +
                " === Exception: " + ex.ToString(), 1);

            }

            
            return RezChek; 
        }
        #endregion

        #region Инсталяция и запуск службы сервисов Conecto.
        public static int RunServis()
        {

            

            //List<string> fileoutLines = new List<string>();
            //ServiceController[] ListServices;
            //ListServices = ServiceController.GetServices();
            //foreach (ServiceController scTemp in ListServices)
            //{
            //    fileoutLines.Add(scTemp.ServiceName);
            //}
            //System.IO.File.WriteAllLines(SystemConectoServers.PutchServer + "servise.log", fileoutLines);
            var RezChek = 1;// Результат проверки
            int IndexActivProces = -1;
            string Fb = "Com_Kassa_24";
            ServiceController[] scServices;
            scServices = ServiceController.GetServices();
            foreach (ServiceController scTemp in scServices)
            {
                if (scTemp.ServiceName == Fb) {IndexActivProces++; break; }
            }
 
            if (IndexActivProces < 0)
            {
                if (System.IO.File.Exists(SystemConectoServers.PutchServer + @"SystemTasks\SystemTasks.exe") == true)
                {
                    string CmdName = @"c:\Windows\Microsoft.NET\Framework\v4.0.30319\installutil.exe ";
                    string CmdArg = '"'+SystemConectoServers.PutchServer + @"SystemTasks\SystemTasks.exe"+'"';  //@""" c:\Program Files\Conecto\Servers\SystemTasks\SystemTasks.exe""";
                
                    Process mv_prcInstaller = new Process();
                    mv_prcInstaller.StartInfo.FileName = CmdName;
                    mv_prcInstaller.StartInfo.Arguments = CmdArg;
                    mv_prcInstaller.StartInfo.UseShellExecute = false;
                    mv_prcInstaller.StartInfo.CreateNoWindow = true;
                    mv_prcInstaller.StartInfo.RedirectStandardOutput = true;
                    mv_prcInstaller.Start();
                    mv_prcInstaller.WaitForExit();
                    mv_prcInstaller.Close();
                }
                IndexActivProces = -1;
                scServices = ServiceController.GetServices();
                foreach (ServiceController scTemp in scServices)
                {
                    if (scTemp.ServiceName == Fb) { IndexActivProces++; break; }
                }
            }


            if (IndexActivProces >= 0)
            {
                ServiceController service = new ServiceController("Com_Kassa_24");
                // Проверяем не запущена ли служба
                if (service.Status != ServiceControllerStatus.Running)
                {
                    // Запускаем службу
                    service.Start();
                    // В течении минуты ждём статус от службы
                    service.WaitForStatus(ServiceControllerStatus.Running, TimeSpan.FromMinutes(1));
                    SystemConecto.ErorDebag("Служба SystemTasks была успешно запущена!");

                }
                else {SystemConecto.ErorDebag("Служба SystemTasks ранее успешно запущена!");}


            }
            RezChek = 0;
            return RezChek;

            //// Останавливаем службу
            //public static void StopService(string serviceName)
            //{
            //    ServiceController service = new ServiceController(serviceName);
            //    // Если служба не остановлена
            //    if (service.Status != ServiceControllerStatus.Stopped)
            //    {
            //        // Останавливаем службу
            //        service.Stop();
            //        service.WaitForStatus(ServiceControllerStatus.Stopped, TimeSpan.FromMinutes(1));
            //        Console.WriteLine("Служба была успешно остановлена!");
            //    }
            //    else
            //    {
            //        Console.WriteLine("Служба уже остановлена!");
            //    }
            //}

            //// Перезапуск службы
            //public static void RestartService(string serviceName)
            //{
            //    ServiceController service = new ServiceController(serviceName);
            //    TimeSpan timeout = TimeSpan.FromMinutes(1);
            //    if (service.Status != ServiceControllerStatus.Stopped)
            //    {
            //        Console.WriteLine("Перезапуск службы. Останавливаем службу...");
            //        // Останавливаем службу
            //        service.Stop();
            //        service.WaitForStatus(ServiceControllerStatus.Stopped, timeout);
            //        Console.WriteLine("Служба была успешно остановлена!");
            //    }
            //    if (service.Status != ServiceControllerStatus.Running)
            //    {
            //        Console.WriteLine("Перезапуск службы. Запускаем службу...");
            //        // Запускаем службу
            //        service.Start();
            //        service.WaitForStatus(ServiceControllerStatus.Running, timeout);
            //        Console.WriteLine("Служба была успешно запущена!");
            //    }
            //}
        }


        // Процедура загрузки из БД параметров конфигурации активных приложений.
        public void LoadedAppPlay()
        {
            LoadedTableXml();
            int CountTableXml = ConectoWorkSpace.Administrator.AdminPanels.TableXml.Count();
            if (CountTableXml == 0) return;
            List<string> Autostart = new List<string>();
            using (StringWriter sw = new StringWriter())
            {

                // Начало документа                             
                WriterConfigXML = new XmlTextWriter(sw);
                WriterConfigXML.Formatting = Formatting.Indented; // упорядочивания структуры файла
                WriterConfigXML.WriteStartDocument();
                WriterConfigXML.WriteStartElement("Параметры-AppPlay");
                WriterConfigXML.WriteStartElement("FileConfig");
                WriterConfigXML.WriteAttributeString(null, "FileConfig_Ver-File-Config", null, "0.1");
                WriterConfigXML.WriteEndElement();
                // Тело документа
                for (int i = 0; i <= CountTableXml - 1; i++)
                {
                    WriterConfigXML.WriteStartElement("idAppOverall_" + ConectoWorkSpace.Administrator.AdminPanels.TableXml[i].OverAll_idApp);
                    WriterConfigXML.WriteAttributeString(null, "idApp", null, ConectoWorkSpace.Administrator.AdminPanels.TableXml[i].OverAll_idApp);
                    WriterConfigXML.WriteAttributeString(null, "NameApp", null, ConectoWorkSpace.Administrator.AdminPanels.TableXml[i].OverAll_NameApp);
                    if (ConectoWorkSpace.Administrator.AdminPanels.TableXml[i].OverAll_AutorizeType != "") WriterConfigXML.WriteAttributeString(null, "AutorizeType", null, ConectoWorkSpace.Administrator.AdminPanels.TableXml[i].OverAll_AutorizeType);
                    WriterConfigXML.WriteAttributeString(null, "CaptionNamePlay", null, ConectoWorkSpace.Administrator.AdminPanels.TableXml[i].OverAll_CaptionNamePlay);
                    WriterConfigXML.WriteAttributeString(null, "PuthFileIm", null, ConectoWorkSpace.Administrator.AdminPanels.TableXml[i].OverAll_PuthFileIm);
                    WriterConfigXML.WriteAttributeString(null, "infoWorkSpace", null, ConectoWorkSpace.Administrator.AdminPanels.TableXml[i].OverAll_infoWorkSpace);
                    WriterConfigXML.WriteEndElement();
                    WriterConfigXML.WriteStartElement("idAppPanelWorkSpace_" + ConectoWorkSpace.Administrator.AdminPanels.TableXml[i].Panel_idApp);
                    WriterConfigXML.WriteAttributeString(null, "idApp", null, ConectoWorkSpace.Administrator.AdminPanels.TableXml[i].Panel_idApp);
                    WriterConfigXML.WriteAttributeString(null, "LinkPanel", null, ConectoWorkSpace.Administrator.AdminPanels.TableXml[i].Panel_LinkPanel);
                    WriterConfigXML.WriteAttributeString(null, "TypeLink", null, ConectoWorkSpace.Administrator.AdminPanels.TableXml[i].Panel_TypeLink);
                    WriterConfigXML.WriteAttributeString(null, "NumberPanel", null, ConectoWorkSpace.Administrator.AdminPanels.TableXml[i].Panel_NumberPanel);
                    WriterConfigXML.WriteAttributeString(null, "CaptionNameWorkSpace", null, ConectoWorkSpace.Administrator.AdminPanels.TableXml[i].Panel_CaptionNameWorkSpace);
                    WriterConfigXML.WriteAttributeString(null, "AppStartMetod", null, ConectoWorkSpace.Administrator.AdminPanels.TableXml[i].Panel_AppStartMetod);
                    if (ConectoWorkSpace.Administrator.AdminPanels.TableXml[i].Panel_TypeApp != "") WriterConfigXML.WriteAttributeString(null, "TypeApp", null, ConectoWorkSpace.Administrator.AdminPanels.TableXml[i].Panel_TypeApp);
                    WriterConfigXML.WriteAttributeString(null, "AutostartTimeSec", null, ConectoWorkSpace.Administrator.AdminPanels.TableXml[i].Autostart);
                    WriterConfigXML.WriteEndElement();

 

                    Autostart.Add(ConectoWorkSpace.Administrator.AdminPanels.TableXml[i].Autostart);

                }

                //-------------------- конец документа
                WriterConfigXML.WriteEndElement();
                WriterConfigXML.WriteEndDocument();
                // + Environment.NewLine
                AppStart.xmlString = sw.ToString();
                //System.IO.File.WriteAllText(SystemConectoServers.PutchServer + @"tmp\" + "TableConect.xml", AppStart.xmlString, Encoding.Unicode);
            }
            if (!ConectoWorkSpace.AppStart.SysConf.ReadConfigXMLAppPlay(2)) return;
            if (Autostart.Count != 0)
            {
                string[] ArrAutostart = Autostart.ToArray(); // перевод списка в массив
                string ExeFile = "", Other_Puth = "", Puth_File = "", FileExe = "" ;
                for (int i = 0; i <= CountTableXml - 1; i++)
                {
                    if(ArrAutostart[i].Length !=0)
                    {

                        //MessageBox.Show("Автостарт" + ArrAutostart[i]);
                        if (Convert.ToUInt16(ArrAutostart[i]) != 0)
                        {

                            Other_Puth = ConectoWorkSpace.Administrator.AdminPanels.TableXml[i].Other_Puth;
                            ExeFile = ConectoWorkSpace.Administrator.AdminPanels.TableXml[i].Puth;
                            Puth_File = ExeFile.Length == 0 ? Other_Puth : ExeFile;
                            FileExe = Puth_File.Substring(Puth_File.LastIndexOf(@"\") + 1, Puth_File.IndexOf(".") - (Puth_File.LastIndexOf(@"\") + 1));
                            if (Puth_File.Length != 0)
                            {

                                Process[] Front = Process.GetProcessesByName(FileExe);
                                if (Front.Length == 0)
                                {
                                   AppStart.RenderInfo Arguments01 = new AppStart.RenderInfo() { };
                                    Arguments01.argument1 = Puth_File;
                                    Arguments01.argument2 = ArrAutostart[i];
                                    Thread thStartTimer01 = new Thread(RunOtherWork);
                                    thStartTimer01.SetApartmentState(ApartmentState.STA);
                                    thStartTimer01.IsBackground = true; // Фоновый поток
                                    thStartTimer01.Start(Arguments01);

                                }
 
                            }

 
                        }

                    }
 
                }

            }


        }

        // Процедура автозапуска приложения через указанное время
        public static void RunOtherWork(object ThreadObj)
        {
            // Разбор аргументов
            AppStart.RenderInfo arguments = (AppStart.RenderInfo)ThreadObj;
            string PuthExe = arguments.argument1;
            int TimeRunExe = Convert.ToUInt16(arguments.argument2);
            App.AutoStartFront++;
            int Indexrun = 0;
            string NameProces = PuthExe.Substring(PuthExe.LastIndexOf(@"\") + 1, PuthExe.IndexOf(".") - (PuthExe.LastIndexOf(@"\") + 1));

            // Права в ОС Win
            if (App.aSystemVirable["UserWindowIdentity"] == "1")
            {
                if (System.IO.File.Exists(PuthExe))
                {
                    if (!AppStart.SetFocusWindow(NameProces))
                    {

                        var TextWindows = "Выполняется запуск программы" + Environment.NewLine + "Пожалуйста подождите. ";
                        int AutoClose = 1;
                        int MesaggeTop = -170;
                        int MessageLeft = 500;
                        ConectoWorkSpace.MainWindow.MessageInstall(TextWindows, AutoClose, MesaggeTop, MessageLeft);


                        Process p_hendl = System.Diagnostics.Process.Start(PuthExe);
                        p_hendl.WaitForInputIdle();

 
                        for (int i = 0; i <= 0; i++)
                        {
 
                            if (TextPasteWindow.SetFocusWindow(NameProces))
                            {
                                IntPtr handleM = Process.GetCurrentProcess().MainWindowHandle;
                                //TextPasteWindow.SetParent(p_hendl.MainWindowHandle, handleM);
                                AppStart.SetForegroundWindow(handleM);
                                TextPasteWindow.SetParent(p_hendl.MainWindowHandle, handleM);
                                // выбрать фокус фронта
                                AppStart.SetForegroundWindow(p_hendl.MainWindowHandle);
                                if (!TextPasteWindow.SetWindowPos(TextPasteWindow.hControl, HWND.TOPMOST, 200, 200, 0, 0, SWP.SHOWWINDOW | SWP.NOSIZE)) //
                                {
                                    SystemConecto.ErorDebag("Ошибка работы функции: SetWindowPos: окно " + NameProces);
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
            }

        }
        /// <summary>
        /// 
        /// </summary>
        public static void LoadedTableXml()
        {
            int CountTableXml = ConectoWorkSpace.Administrator.AdminPanels.TableXml.Count();
            DBConecto.DBopenFBConectionMemory(((App.aSystemVirable["UserWindowIdentity"] == "1") ? SystemBD.ConnnStringSystem().ToString() : SystemBD.ConnnStringSystem(AppDomain.CurrentDomain.BaseDirectory + @"data\" + "system.fdb")).ToString(), "", "FbSystem");
            FbCommand UpdateKey = new FbCommand("SELECT * from LISTIPRDP", DBConecto.bdFbSystemConect);
            UpdateKey.CommandType = CommandType.Text;
            FbDataReader reader = UpdateKey.ExecuteReader();
            while (reader.Read())
            {
                if (CountTableXml == 0)
                {
                    ConectoWorkSpace.Administrator.AdminPanels.TableXml.Add(new ConectoWorkSpace.Administrator.GridIpRdp(reader[0].ToString(), reader[1].ToString(), reader[2].ToString(), reader[3].ToString(),
                    reader[4].ToString(), reader[5].ToString(), reader[6].ToString(), reader[7].ToString(), reader[8].ToString(),
                    reader[9].ToString(), reader[10].ToString(), reader[11].ToString(), reader[12].ToString(), reader[13].ToString(),
                    reader[14].ToString(), reader[15].ToString(), reader[16].ToString(), reader[17].ToString(), reader[18].ToString(),
                    reader[19].ToString(), reader[20].ToString(), reader[21].ToString()));
                }
            }
            reader.Close();
            UpdateKey.Dispose();
            DBConecto.DBcloseFBConectionMemory("FbSystem");
        }






        #endregion

        #region Загрузка пиктограммы в системный Бар

        public static int IsSystemOSIcoSYBAR()
        {
            // Результат загрузки
            var RezChek = 0;
            // new FancyPopup()
            SystemConecto.IsNotifyIconWPF(null);

            //SystemConecto.IsNotifyIconWPF();

            return RezChek;
        }
        #endregion


            #region Контур проверки и обновления файлов ----- дублируется с SystemConecto (для бесперебойной работы приложения)
            //  Стартовая проверка необходимых компонентов в ОС для работы Приложния


            #region Проверка файла в зборочном пакете, необходимого для работы приложения Install Pack AppStart (Отличается от SystemConecto)
            /// <summary>
            /// Группа путей для поверки файла в системе
            /// </summary>
        public struct FilePuthPack
        {
            /// <summary>
            /// Путь к Файлу который копируется из Pack
            /// </summary>
            public string PutchFIsPack { get; set; }
            /// <summary>
            /// Название файла что проверяем
            /// </summary>
            public string NameFile { get; set; }

            /// <summary>
            /// Название пакета где расположен файл что проверяем
            /// </summary>
            public string NameFileGZ { get; set; }
            /// <summary>
            /// Временный путь к скаченным файлам с FTP, локального сервера, папки
            /// </summary>
            public string FulTempNameFileGZ { get; set; }

            /// <summary>
            /// Путь расположения
            /// </summary>
            public string targetPath { get; set; }

            /// <summary>
            /// Путь к пакету зборки
            /// </summary>
            public string PutchPack { get; set; }
            //    get{
            //        return PutchPack;
            //    }


            //    set
            //    {
            //        PutchPack = value;
            //    }

            //}
        }

  

        /// <summary>
        /// Проверка файла в сборочном пакете, необходимого для работы приложения<para></para>
        /// </summary>
        /// <param name="PutchFIsPack">Путь к Файлу который копируется из Pack</param>
        /// <param name="CritFile">Критичность файла: -1 - не критичный 0 - Критичность к остановке программы;<para></para>
        /// -2 - online файл простой (не отслеживается)
        /// -3 - online файл сложный, шифруется (не отслеживается)</param>
        public static bool AppStartIsFilesPack(string PutchFIsPack, int CritFile = 0)
        {

            var MemFilePuthPack = new FilePuthPack();
            MemFilePuthPack.PutchFIsPack = PutchFIsPack;

            // Извлечение файла (ов) из пакета файлов приложения
            string[] aPutchFIsPack = PutchFIsPack.Split('\\');
            /// Получаем пред-последний елемент это название файла
            int length = aPutchFIsPack.Length - 1;
            MemFilePuthPack.NameFile = aPutchFIsPack[length];

            // Вывод информации о подгружаемом файле
            MessageListener.Instance.ReceiveMessage(string.Format("{2}: {0}" + Environment.NewLine +
                "выполняется установка модуля {1}", "проверка компонентов в ОС для работы приложения",
                MemFilePuthPack.NameFile, "Выполняется")); //returnLanguage("Выполняется")

            MemFilePuthPack.NameFileGZ = MemFilePuthPack.NameFile + ".gz";

            MemFilePuthPack.FulTempNameFileGZ = PutchApp + @"tmp\" + MemFilePuthPack.NameFileGZ;

            MemFilePuthPack.targetPath = MemFilePuthPack.PutchFIsPack.Substring(0, MemFilePuthPack.PutchFIsPack.Length - MemFilePuthPack.NameFile.Length);

            // Если найден локальный пакет, мы извлекаем из него необходимый файл согласно настройкам пакета pack.xml
            MemFilePuthPack.PutchPack = null;

            string MemoryMessageComment = "Не найден пакет программы, ни на одном из носителей и в сетях." + Environment.NewLine +
                                            "Файл: " + PutchFIsPack.ToString() + " не найден.";

            // Если пакет не найден подключаемся к центральному FTP
            if (SherchPack(MemFilePuthPack) == null)
            {
                // !!! ==================== Проверка сервера ConectoWorkSpace в сети (запрос чтения файла с сервера)

                // правло переподсоединения одного потока
                bool idBlock = true;
                int CountConect = 1; //не более 3 раз

                while (idBlock)
                {
                    // Локальный пакет не найден нужно его взять с Интеренета
                    // проверка соединения с интернетом и с обслуживающим сервером 
                    if (ConnectionAvailable())
                    {
                        // updatework.conecto.ua Чтение паролей из настроек ПО WriterConfigUserXML (Пользователь устанавливается на стороне сервера)
                        // updatework.conecto.ua/updatework.conecto.ua/ "update_workspace" "conect1074"
                        var rezultConectionFTP = ConntecionFTP(@"conecto.ua/pack/" + MemFilePuthPack.NameFile + ".gz", aParamApp["ServerUpdateConecto_USER"], aParamApp["ServerUpdateConecto_USER-Passw"], 2, MemFilePuthPack.FulTempNameFileGZ);

                        //ErorDebag((rezultConectionFTP == null ? "Файла нету " : "Файл есть ") + MemFilePuthPack.NameFile, 2);

                        if (rezultConectionFTP != null && File_(MemFilePuthPack.FulTempNameFileGZ, 5))
                        {
                            // Перемещение файла от куда  куда (файл проверен)
                            // Проверка отладка
                            //if (!File_(PutchFIsPack,5))
                            //{

                            // Выполняется с незначительной асинхронностью декомпресию в другую директорию Синхронно
                            Compress.ToDecompressStrem(MemFilePuthPack.FulTempNameFileGZ, MemFilePuthPack.targetPath);
                            // Не отслеживать онлан файлы
                            if (CritFile > -2)
                            {
                                //ErorDebag("Файл: " + MemFilePuthPack.NameFile + " скопирован с FTP сервера.", 3);
                            }
                            // Простое перемещение файла
                            //File.Move(PutchApp + @"tmp\" + MemFilePuthPack.NameFile, PutchFIsPack);

                            // Удаление происходит после выполнения модуля в целом в фоновом режиме DIR_ (Разработка - Доделать Укрпочта)
                            //DeliteFileTmp();

                            // Проверка файла
                            if (File_(PutchFIsPack, 5))
                            {
                                idBlock = false;
                                //return true;
                            }
                            else
                            {
                                if (CountConect > 2)
                                {
                                    idBlock = false;
                                    return false;
                                }
                            }



                            //}
                            //else
                            //{
                            //    // Файл не докачен при работе с FTP


                            //}
                        }
                        else
                        {
                            if (CountConect > 2)
                            {
                                string Text = "";
                                // Исключение во время скачивания
                                switch (CritFile)
                                {
                                    case -1:
                                        // Файл не критичен
                                        Text = "Во время соединения с FTP произошло исключение." + Environment.NewLine;
                                        break;

                                    default:
                                        // Файл критичен
                                        Text = "Приложение будет завершено." + Environment.NewLine;
                                        break;
                                }
                                // Пример использования \r\n или \n Environment.NewLine
                                //ErorDebag(Text +
                                //       MemoryMessageComment, CritFile == -1 ? 0 : 2);
                                // Критичность к остановке программы
                                if (CritFile > -1)
                                {
                                    Environment.Exit(0);
                                }
                                else
                                {
                                    idBlock = false;
                                    return false;
                                }
                            }
                        }

                    }
                    else
                    {

                        if (CountConect > 2)
                        {
                            string Text = "Программа не может соединенится с FTP сервером!" + Environment.NewLine +
                                "Отсутствует соединение с Интернетом" + Environment.NewLine;
                            // Исключение во время скачивания

                            //ErorDebag(string.Format("{0} {1}", Text, MemoryMessageComment), CritFile == -1 ? 0 : 2);
                            // Критичность к остановке программы
                            if (CritFile > -1)
                            {
                                // Вывести сообщение
                                Environment.Exit(0);
                            }
                            else
                            {

                                idBlock = false;
                                return false;
                            }
                        }


                    }
                    CountConect++;
                    // конец сетевого цыкла
                }


            }
            // Закрыть окно ожидание
            //fWait.Close();
            return true;

        }

        #region Поиск инсталяционного пакета на дисках
        /// <summary>
        /// Поиск пакета инсталяции на локальном компьюторе
        /// файлы пакета упакованы gz
        /// </summary>
        /// <param name="PutchFIsPack"></param>
        /// <returns></returns>
        private static string SherchPack(FilePuthPack PutchFIsPack)
        {
            // PutchPack - кешируемый путь к пакету файлов программы (разработка)
            // Проверка кеша
            // if (PutchPack == null)

            // 1. Приложение ищит на всех дисках папку Conecto в папке Conecto\pack - обязательно наличие файла pack.xml)
            DriveInfo[] DriveList = DriveInfo.GetDrives();
            // string[] DriveList = Environment.GetLogicalDrives(); // Вариант не очень информативный
            //for (int i = 0; i < DriveList.Length; i++)
            // d.DriveType - тип устройства, проверить все портативные носители - if (drive.DriveType == DriveType.Removable)

            // Полный путь проверяемого файла
            string FulPuthPack_ = "";

            foreach (DriveInfo d in DriveList)
            {
                //System.Windows.Forms.MessageBox.Show(DriveList[i]);
                // Выбираем лучшую версию pack подходящию нашей версии (Исключение является обновление ПО, однако обновление осущетвляется на уровне exe файла)
                // 2. Если нашел проверяем версию файла - файл - pack.xml (файл содержащий информацию о пакете) - для какой версии ПО VersijaPO
                // Проверка готовности устройства
                if (d.IsReady == true)
                {



                    // В Папке Conecto\pack разложил упакованые файлы в потоке .gz
                    // && Ищем первый подходящий пак (условие точное совпадение версий VersijaPO)
                    if (File_(d.Name + @"\pack\pack.xml", 5))
                    {

                        PutchFIsPack.PutchPack = d.Name + @"\pack\";  // Путь к пакету

                        // 2.1 Читаем файл и сравниваем (если надо распаковываем в папку Temp)

                        // string[] aVersijaPO = VersijaPO.Split('-'); - Получение характеристик версии

                        FulPuthPack_ = PutchFIsPack.PutchPack + PutchFIsPack.NameFileGZ;

                        // Взять из архива - Распоковать
                        // Проверка файлаы
                        if (File_(FulPuthPack_, 5))
                        {
                            //Compress.AddCompressFile(PutchPack_ + @"\" + PutchFIsPack.NameFile);
                            Compress.ToDecompressStrem(FulPuthPack_, PutchFIsPack.targetPath);

                            //ErorDebag("Файл: " + PutchFIsPack.NameFile + " взят из архива, " + PutchFIsPack.PutchPack, 3);

                            // =========================== Пример (устарело)
                            // копируем в нужную папку
                            // Используйте путь класса манипулировать файлами и каталогами пути.
                            //string sourceFile = System.IO.Path.Combine(sourcePath, PutchFIsPack.NameFile);
                            //string destFile = System.IO.Path.Combine(targetPath, PutchFIsPack.NameFile);

                            //// To copy a file to another location and 
                            //// overwrite the destination file if it already exists.
                            //File.Copy(sourceFile, destFile, true);



                            // Получили верную версию результат найден, закончить проверку
                            break;
                        }
                        else
                        {
                            PutchFIsPack.PutchPack = null;
                        }

                    }

                }
            }

            return PutchFIsPack.PutchPack;

        }
        #endregion

        #endregion
        #endregion

        #region Дубль в ядре AppStart для скачивания библиотеки SystemConecto модификация упращение

        #region Проверка соединения с Интерентом а также с указанным WEB узлом
        /// <summary>
        /// Проверка соединения с Интерентом по 80 порту, а также с указанным WEB узлом, если отключен DNS функция выдает ошибку
        /// прокси http://spys.ru/proxys/UA/
        /// </summary>
        /// <param name="strServer">По умочанию указан www.google.com</param>
        /// <param name="OkServerNoComment">По умочанию не комментировать удачное соединение</param>
        /// <returns>Истина или Ложь</returns>
        public static bool ConnectionAvailable(string strServer = "www.google.com", bool OkServerNoComment = false)
        {
            strServer = "https://" + strServer;
            try
            {
                HttpWebRequest reqFP = (HttpWebRequest)WebRequest.Create(strServer);
               // return true;
                HttpWebResponse rspFP = (HttpWebResponse)reqFP.GetResponse();
                if (HttpStatusCode.OK == rspFP.StatusCode)
                {
                    // HTTP = 200 - Интернет безусловно есть!
                    rspFP.Close();
                    //if (OkServerNoComment)
                        //ErorDebag("HTTP = 200 - Интернет безусловно есть с адресом: " + strServer, 1);
                    return true;
                }
                else
                {
                    // сервер вернул отрицательный ответ, возможно что инета нет
                    rspFP.Close();
                    //ErorDebag("Cервер вернул отрицательный ответ: " + rspFP.StatusCode + "." + " Возможно, что связь с адресом: " + strServer + " отсутствует.", 1);
                    return false;
                }
            }
            catch (WebException )//Ex
            {
                // Отследить сообщения Ex.Status
                //if (Ex.Status.ToString() == "NameResolutionFailure")
                //{
                //    // Нужно проверить стсатус сети ... последней проверки
                //    //ErorDebag(" Связь с адресом: " + strServer + " отсутствует.", 1);
                //}
                //else
                //{
                //    // Ошибка, значит интернета у нас нет. Плачем :'(
                //    // Тут нужна дополнительная аналитика например ошибка 403 тоже попадает сюда.
                //    ErorDebag("Cервер вернул отрицательный ответ: " + Ex.Status.ToString() + " сообщение системы: " + Ex + "." + " Связь с адресом: " + strServer + " отсутствует.", 1);
                //}
                return false;
            }
            //return false;
        }
      
        #endregion


        #region Работа с FTP Server - упращенная модифицированная 

        /// <summary>
        /// Синхронное соединение с FTP сервером
        /// </summary>
        /// <param name="strServer"></param>
        /// <param name="NameUser"></param>
        /// <param name="PasswdUser"></param>
        /// <param name="TypeCommand"></param>
        /// <param name="PutchTMPFile"></param>
        /// <returns></returns>
        public static string[] ConntecionFTP(string strServer, string NameUser, string PasswdUser, int TypeCommand = 1, string PutchTMPFile = "")
        {
            string[] aresultFTP = null; // Ответ с сервера | PutchTMPFile - полный путь к временной папке

            // TypeCommand = 1 - проверка ФТП, 2- загрузка файла с сервера, 3 - чтения списка, 7 - создание директории           // TypeCommand = 4;  Тест

            FileInfo fileInf = new FileInfo(strServer); // разбираем путь FTP
            string NameFile = fileInf.Name; // это название файла
            // Получить путь на FTP Сервере где находится файл (если файл отсутствует то результатом будет только путь к директории)
            String targetPath = strServer.Substring(0, strServer.Length - NameFile.Length);
            // проверка Uri FTP
            strServer = "ftp://" + strServer;
            Uri UriServer = new Uri(strServer);
            switch (TypeCommand)
            {
                case 3:
                    UriServer = new Uri("ftp://" + targetPath);
                    break;
                case 7:
                    UriServer = new Uri("ftp://" + targetPath);
                    break;
            }

            if (UriServer.Scheme != Uri.UriSchemeFtp)
            {
                //ErorDebag("URI = " + UriServer.ToString() + ", ошибка адресса FTP. Адресс: " + strServer, 1);
                return aresultFTP;
            }


            StringBuilder result_FTP = new StringBuilder();
            WebResponse response_FTP = null;                    // Ответ Сервера FTP
            StreamReader reader_FTP = null;                     // Чтения потока с FTP

            try
            {

                // Тест соединения с сервером TypeCommand -1 (проверка осуществдяется всегда перед началом работы)
                response_FTP = CreateFtpRequest(UriServer, NameUser, PasswdUser, WebRequestMethods.Ftp.PrintWorkingDirectory).GetResponse();
                FtpWebResponse rspFTP = response_FTP as FtpWebResponse;
                reader_FTP = new StreamReader(response_FTP.GetResponseStream());
                string str1 = rspFTP.StatusDescription.ToString();
                int CodOK = str1.IndexOf("257");
                //ErorDebag("Результат проверки FTP: " + str1.ToString().Replace('\n', ' '), 1);


                //if (reader_FTP != null)
                //{
                //    reader_FTP.Close();
                //}
                //if (response_FTP != null)
                //{
                //    response_FTP.Close();
                //}

                // Метод
                switch (TypeCommand)
                {
                    case 2:
                        // 2 - Чтение файла с записью в папку Temp
                        /// --------- Не работает предположительно для текстового файла
                        // response_FTP = CreateFtpRequest(UriServer, NameUser, PasswdUser, WebRequestMethods.Ftp.DownloadFile).GetResponse();

                        //// Stream strm = response_FTP.GetResponseStream();

                        // reader_FTP = new StreamReader(response_FTP.GetResponseStream());
                        //  StreamWriter writer = new StreamWriter(PutchTMPFile, false); // Path.Combine(FolderToWriteFiles, fileInf.Name)
                        //  writer.Write(reader_FTP.ReadToEnd());
                        //  writer.Close();
                        /// --------- Не работает

                        // ----- Работает только для изображений
                        //using (response_FTP = CreateFtpRequest(UriServer, NameUser, PasswdUser, WebRequestMethods.Ftp.DownloadFile).GetResponse())
                        //using (var stream = response_FTP.GetResponseStream())
                        //using (var img = Image.FromStream(stream))
                        //{
                        //    img.Save(PutchTMPFile);
                        //}

                        // --------- Пример копирования файла через webFTP 

                        // Get the object used to communicate with the server.

                        WebClient request = new WebClient();
                        try
                        {


                            // This example assumes the FTP site uses anonymous logon.
                            request.Credentials = new NetworkCredential(NameUser, PasswdUser);

                            byte[] newFileData = request.DownloadData(UriServer.ToString());
                            System.IO.File.WriteAllBytes(PutchTMPFile, newFileData);

                            // Для текстового файла
                            //string fileString = System.Text.Encoding.UTF8.GetString(newFileData);
                            //File.WriteAllText(PutchTMPFile, fileString);

                            aresultFTP = new string[1] { PutchTMPFile };
                        }
                        catch (WebException) //Ex
                        {
                            //Console.WriteLine(e.ToString());
                            //ErorDebag("Cервер вернул отрицательный ответ: " + Ex + "." + " Связь с адресом: " + strServer + " разорвалась.", 1);
                        }
                        finally
                        {
                            request.Dispose();
                        }


                        // Проверку полученного файла переделать
                        //if (File_(PutchTMPFile, 5))
                        //{

                        //}
                        // return aresultFTP; // конец


                        break;
                    case 3:
                        // 3 - Чтение списки директорий и файлов
                        response_FTP = CreateFtpRequest(UriServer, NameUser, PasswdUser, WebRequestMethods.Ftp.ListDirectoryDetails).GetResponse();


                        //ErorDebag("Результат чтения FTP", 1);
                        //reader_FTP = new StreamReader(response_FTP.GetResponseStream()); 
                        //string line = reader_FTP.ReadLine(); 
                        //while (line != null) { 
                        //    result_FTP.Append(line); 
                        //    result_FTP.Append("\n"); 
                        //    line = reader_FTP.ReadLine(); 
                        //} 
                        //result_FTP.Remove(result_FTP.ToString().LastIndexOf('\n'), 1);

                        //ErorDebag("Результат чтения FTP" + result_FTP.ToString(), 1);

                        // return result_FTP.ToString().Split('\n');
                        break;
                    case 7:
                        // Создать директорию
                        // CreateFtpRequest(new Uri("ftp://updatework.conecto.ua/updatework.conecto.ua/test"), NameUser, PasswdUser, WebRequestMethods.Ftp.MakeDirectory).GetResponse();
                        // CreateFtpRequest(new Uri("ftp://updatework.conecto.ua/updatework.conecto.ua/pack/"), NameUser, PasswdUser, WebRequestMethods.Ftp.ListDirectory).GetResponse();

                        // ftp = CreateFtpRequest(path, WebRequestMethods.Ftp.ListDirectory);

                        break;
                }




            }
            catch (WebException ExNet)
            {
                // Запись последней ошибки FTP соединения (передача объекта в глобальную переменную)
                NetFTPResponse = ExNet.Response as FtpWebResponse;
                // Обрыв соединения
                if (NetFTPResponse == null)
                {
                    //SystemConecto.ErorDebag("При обращении к FTP + {" + strServer + "}, произошел обрыв соединения: " + Environment.NewLine +
                    //" === Message: " + ExNet.Message.ToString() + Environment.NewLine +
                    //" === Exception: " + ExNet.ToString());


                }
                else
                {
                   // SystemConecto.ErorDebag("При обращении к FTP + {" + strServer + "}, сервер вернул отрицательный ответ:  " + Environment.NewLine +
                   //" === Message: " + ExNet.Message.ToString() + Environment.NewLine +
                   //" === Exception: " + ExNet.ToString() + Environment.NewLine +
                   //"Сервер вернул код ошибки: {idcodeftp=" + NetFTPResponse.StatusCode.ToString() + "}" + Environment.NewLine +
                   //"Связь с адресом " + strServer + " прервана.");

                    NetFTPResponse.Close();
                }


                // return NetFTPResponse.StatusCode != FtpStatusCode.ActionNotTakenFileUnavailable;


            }
            finally
            {
                if (reader_FTP != null)
                {
                    reader_FTP.Close();
                }
                if (response_FTP != null)
                {
                    response_FTP.Close();
                }
            }

            return aresultFTP;
        }

        private static FtpWebRequest CreateFtpRequest(Uri Uripath, string NameUser, string PasswdUser, string method, string[] aParamFTP = null)
        {
            // Проверка параметров
            if (aParamFTP == null)
            {
                // KeepAlive - 0 | UseBinary - 1 | Timeout - 2 | UsePassive - 3
                aParamFTP = new string[6] { "false", "true", "3000", "true", null, "" };
            }
            try
            {
                var ftp = FtpWebRequest.Create(Uripath) as FtpWebRequest;
                // FtpWebRequest reqFTP = (FtpWebRequest)FtpWebRequest.Create(Uripath);
                // FtpWebRequest reqFTP = FtpWebRequest.Create(Uripath) as FtpWebRequest;
                // FtpWebRequest reqFTP = (FtpWebRequest)FtpWebRequest.Create(new Uri(Uripath)); //new Uri("ftp://172.29.200.158/")
                ftp.Credentials = new NetworkCredential(NameUser, PasswdUser);
                ftp.KeepAlive = Convert.ToBoolean(aParamFTP[0]);
                ftp.UseBinary = Convert.ToBoolean(aParamFTP[1]);
                ftp.Timeout = Convert.ToInt32(aParamFTP[2]);
                ftp.Proxy = null;
                // ftp.UsePassive = Convert.ToBoolean(aParamFTP[3]);
                // reqFTP.EnableSsl = true; 
                // ServicePointManager.ServerCertificateValidationCallback = new System.Net.Security.RemoteCertificateValidationCallback(AcceptAllCertifications); 
                ftp.Method = method;
                return ftp;
            }
            catch (WebException ExNet)
            {
                // Запись последней ошибки FTP соединения (передача объекта в глобальную переменную)
                NetFTPResponse = ExNet.Response as FtpWebResponse;
                NetFTPResponse.Close();
                // return resp.StatusCode != FtpStatusCode.ActionNotTakenFileUnavailable;
                return null;
            }

        }


        #endregion
        #endregion


        #region Функции для управления директориями и файлами (модификация функции - упрощена в SystemConecto)
        /// <summary>
        /// Проверка дириктории без вывода ошибки (Typefunc - 0 - создание, 5-перепроверка)<para></para>
        /// Управление каталогами по заданому пути - path;
        /// - упрощена, а также логирования
        /// </summary>
        /// <param name="path">каталоги по заданому пути</param>
        /// <param name="Typefunc"></param>
        /// <returns></returns> 
        public static bool DIR_(string path, int Typefunc = 0)
        {
            /// 
            if (!(Directory.Exists(path)))
            {

                switch (Typefunc)
                {
                    default:
                        // Создание директории
                        try
                        {
                            Directory.CreateDirectory(path);
                        }
                        catch (Exception) // error
                        {

                            // Дополнительная логика проверки создания директории
                            if (CheckDirectory(path))
                            {
                                return true;
                            }


                            return false;
                        }

                        break;
                }


            }
           
            return true;
        }

        #region Создание каталогов по заданому пути v.1.2 (модификация функции - упрощена в SystemConecto)
        /// <summary>
        /// Создание каталогов по заданому пути аналог DIR_ - она быстрее
        /// позваляет определить ошибку когда в пути есть каталог имя которого совпадает с файлом
        /// - упрощена, а также логирования
        /// </summary>
        /// <param name="dir_"></param>
        public static bool CheckDirectory(string dir_)
        {
            string[] dir = dir_.Split('\\');

            string dirchek = dir[0]; // диск начало пути

            for (int ind = 1; ind < dir.Length; ind++)
            {
                dirchek = dirchek + @"\" + dir[ind];
                // Определение ошибки совпадения названия файла и Директории
                if (File_(dirchek, 5))
                {
                    return false;
                }
                if (!DIR_(dirchek, 5))
                {
                    return false;
                }
            }
            return true;
        }

        #endregion

        /// <summary>
        /// Проверка файла (Typefunc - создание = 0, удаление, изменение, 
        /// 4 - создать но вернуть результат проверки, 5-проверка без создания, 6-блокировка для безопасности)
        /// - упрощена, а также логирования
        /// </summary>
        /// <param name="path">Путь к файлу</param>
        /// <param name="Typefunc"></param>
        /// <param name="LinkFile"></param>
        /// <returns></returns>
        public static bool File_(string path, int Typefunc = 0)
        {
            /// Проверка файла (Typefunc - создание = 0, удаление, изменение, 
            /// 4 - создать но вернуть результат проверки, 5-проверка без создания)
            if (!(System.IO.File.Exists(path)))
            {
                try
                {
                    if (Typefunc < 5)
                    {
                        using (FileStream NewFile = System.IO.File.Create(path))
                        {
                            NewFile.Close();
                            if (Typefunc == 4)
                            {
                                return false;
                            }
                        }
                    }
                    else
                    {
                        return false;
                    }

                }
                catch (Exception) //error
                {
                    return false;
                }
            }

            return true;
        }

 
        #endregion


        #region Управление сервисными окнами WindowForm

        // Открыть поток окна на передний план.
        [DllImport("user32.dll", SetLastError = true)]
        public static extern IntPtr SetForegroundWindow(IntPtr hWnd);
        /// <summary>
        /// Установка фокуса окна 
        /// string NameWinndow - /Handle/ обращение к окну с помощью Хандл процесса. 
        /// </summary>
        /// <param name="NameWinndow"></param>
        public static bool SetFocusWindow(string NameWinndow, int Id_ = 0, string MainWindowTitle = "")
        {


            if (NameWinndow == "HandleID")
            {
                Process[] allProc_Id = Process.GetProcesses();

                foreach (Process p in allProc_Id)
                {
                    // Отладка
                    //SystemConecto.ErorDebag(string.Format("Имя окна {0}, Hwnd - {1}", p.ProcessName, p.MainWindowHandle.ToString()), 2);

                    if (p.Id == Id_)
                    {
                        // встречается нулевой Хендел (даже после обновления кеша p.refrech) для процессов без интерфейса Окна ОС
                        IntPtr Hwnd = p.MainWindowHandle;
                        if (Hwnd == IntPtr.Zero)
                        {
                            // Процессов с таким именем может быть много как фоновых задач так и интерфейсов. Фоновые задачи исключаем.
                            Process[] allProc_Name = Process.GetProcessesByName(p.ProcessName).Where(process => process.MainWindowHandle != IntPtr.Zero).ToArray();


                            if (allProc_Name != null && allProc_Name.Length >= 1)
                            {

                                foreach (Process findPr in allProc_Name)
                                {
                                    // Отладка
                                    // SystemConecto.ErorDebag(string.Format("Имя окна {0}, Время - {1}, ID - {2}", findPr.ProcessName, findPr.StartTime, findPr.HandleCount), 2);

                                    // Первое найденое (не очень прикольно)
                                    // Процессы могут отличатся заголовками findPr.MainWindowTitle тут обычно указываются заголовки файлов или их названия pdf. doc. xls.
                                    Hwnd = findPr.MainWindowHandle;
                                    break;
                                    //if (findPr.MainWindowHandle != IntPtr.Zero)
                                    //{
                                    //    Hwnd = findPr.MainWindowHandle;
                                    //    break;
                                    //}

                                }

                            }

                            // Первое найденое (не очень прикольно, не учитывает все окна)
                            // Hwnd = ProcessConecto.FindWindow(p.ProcessName, null);
                            if (Hwnd == null || Hwnd == IntPtr.Zero)
                            {
                                return false;
                            }

                        }

                        TextPasteWindow.ShowWindow(Hwnd, 9);
                        SetForegroundWindow(Hwnd);
                        //if (!TextPasteWindow.SetWindowPos(TextPasteWindow.hControl, HWND.TOPMOST, 100, 100, 500, 500, SWP.SHOWWINDOW | SWP.NOSIZE)) //
                        //{
                        //    SystemConecto.ErorDebag("Ошибка работы функции: SetWindowPos: окно Front");
                        //}
                        return true;
                    }
                }


            }

            //Process[] allProc = Process.GetProcesses();

            Process[] allProc_ = Process.GetProcessesByName(NameWinndow).Where(process => process.MainWindowHandle != IntPtr.Zero).ToArray();

            if (allProc_ != null && allProc_.Length >= 1)
            {
                IntPtr pFoundWindow = allProc_[0].MainWindowHandle; //получаем хендл то же что FindWindow("Notepad", null);
                TextPasteWindow.ShowWindow(pFoundWindow, 9);
                SetForegroundWindow(pFoundWindow);
                return true;

            }
            return false;
        }


        /// <summary>
        /// Вывод окна белого фона
        /// тестовый метод
        /// </summary>
        public void WaitStart(string NameWindow = "Default")
        {
            //if (System.Windows.Forms.Application.OpenForms["WaitFon"] == null)
            //{
            //    WaitFon WaitFonWindow = new WaitFon(NameWindow); // создаем
            //    System.Windows.Forms.Application.OpenForms["ConectoWorkSpace"].AddOwnedForm(WaitFonWindow); 
            //    //MessageBox.Show(AdminWindow.Name);
            //    WaitFonWindow.Show();   
            //}
            //else
            //{
            //    // запись значения
            //    // System.Windows.Forms.Application.OpenForms["WaitFon"].nam
            //    System.Windows.Forms.Application.OpenForms["WaitFon"].Visible = true;
            //    // Запуск таймера
            //    // System.Windows.Forms.Application.OpenForms["WaitFon"].Controls["StartWindow"].Enabled = true;
            //}

        }

        #endregion


        #region Вернуть ссылку на главное окно по запросу WPF C# {LinkMainWindow}
        /// <summary>
        /// Вернуть ссылку на окно по запросу WPF C# {ListWindowMain}MainWindow
        /// </summary>
        /// <param name="NameWindow"> Имя главного окна Рабочий стол или Панель</param>
        /// <returns></returns>
        public static dynamic LinkMainWindow(string NameWindow = "ConectoWorkSpace")
        {

            foreach (System.Windows.Window openWindow in System.Windows.Application.Current.Windows)
            {

                //System.Windows.MessageBox.Show(openWindow.Name.ToString(), "«Open Windows»");
                if (openWindow.Name == NameWindow)
                {
                    // Пример ссылки на объект

                    // Пример кода
                    // ownedWindow.Close();MainWindow
                    return (dynamic)openWindow;
                }

            }
            return null;            

        }
        #endregion


        #region Проверка регистрации библиотеки в сбороке GAC

        public static bool GACGet_DBConecto()
        {
            try
            {
                Type t = typeof(DBConecto);
                string s = t.Assembly.FullName.ToString();
                //ErorDebag(string.Format("The fully qualified assembly name " + "containing the specified class is {0}.", s));
            }
            catch (Exception)//ex
            {
                //SystemConecto.ErorDebag(ex.ToString(), 2);
                return false;
            }

            return true;
        }

        public static bool GACGet_Hardcodet()
        {
            try
            {
                Type t = typeof(Hardcodet.Wpf.TaskbarNotification.TaskbarIcon);
                string s = t.Assembly.FullName.ToString();
                // MessageBox.Show(s);
                //ErorDebag(string.Format("The fully qualified assembly name " + "containing the specified class is {0}.", s));
            }
            catch (Exception)//ex
            {
                //SystemConecto.ErorDebag(ex.ToString(), 2);
                return false;
            }
            return true;
        }


        public static bool GACGet_SystemConecto()
        {
            try
            {
                Type t = typeof(SystemConecto.Language);
                string s = t.Assembly.FullName.ToString();
                //SystemConecto.ErorDebag(string.Format("The fully qualified assembly name " + "containing the specified class is {0}.", s));
                
            }
            catch (Exception )//ex
            {
                //SystemConecto.ErorDebag(ex.ToString(), 2);
                return false;
            }

            return true;
        }

        public static bool GACGet_FB()
        {
            // Assembly assem = Assembly.GetExecutingAssembly();

            //ErorDebag(string.Format("Full Name:{0}", assem.FullName));
            try
            {

                Type t = typeof(FirebirdSql.Data.FirebirdClient.FbConnectionStringBuilder);
                string s = t.Assembly.FullName.ToString();
                //ErorDebag(string.Format("The fully qualified assembly name " + "containing the specified class is {0}.", s));
            }
            catch (Exception ex)
            {

                SystemConecto.ErorDebag(ex.ToString(), 2);
                return false;
            }


            return true;

            // The AssemblyName type can be used to parse the full name.
            //AssemblyName assemName = assem.GetName();
            //Console.WriteLine("\nName: {0}", assemName.Name);
            //Console.WriteLine("Version: {0}.{1}", 
            //    assemName.Version.Major, assemName.Version.Minor);

            //Console.WriteLine("\nAssembly CodeBase:");
            //Console.WriteLine(assem.CodeBase);

            //// Create an object from the assembly, passing in the correct number
            //// and type of arguments for the constructor.
            //Object o = assem.CreateInstance("Example", false, 
            //    BindingFlags.ExactBinding, 
            //    null, new Object[] { 2 }, null, null);

            //// Make a late-bound call to an instance method of the object.    
            //MethodInfo m = assem.GetType("Example").GetMethod("SampleMethod");
            //Object ret = m.Invoke(o, new Object[] { 42 });
            //Console.WriteLine("SampleMethod returned {0}.", ret);

            //Console.WriteLine("\nAssembly entry point:");
            //Console.WriteLine(assem.EntryPoint);
            // }
        }


        private static Assembly MyResolveEventHandler(object sender, ResolveEventArgs args)
        {
            //Этот обработчик вызывается только тогда, когда общая языковая среда выполнения пытается привязать к сборке и не удается..

            //Retrieve the list of referenced assemblies in an array of AssemblyName.
            Assembly MyAssembly, objExecutingAssemblies;
            string strTempAssmbPath = "";

            objExecutingAssemblies = Assembly.GetExecutingAssembly();
            AssemblyName[] arrReferencedAssmbNames = objExecutingAssemblies.GetReferencedAssemblies();

            // Отладка имени dll
            // System.Windows.MessageBox.Show(args.Name.ToString(), "", System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Stop);

            // Включить только для своих библиотек
            switch(args.Name.Substring(0, args.Name.IndexOf(",")))
            {
                case "FirebirdSql.Data.FirebirdClient":
                case "Hardcodet.Wpf.TaskbarNotification":
                case "AxInterop.ShockwaveFlashObjects":
                case "WpfAnimatedGif":
                case "SystemConecto":
                case "DBConecto":
                    // Цикл по массиву ссылка имен сборки.
                    foreach (AssemblyName strAssmbName in arrReferencedAssmbNames)
                    {
                        //Проверка для сборки имена, которые вызвали "AssemblyResolve" событие.
                        if (strAssmbName.FullName.Substring(0, strAssmbName.FullName.IndexOf(",")) == args.Name.Substring(0, args.Name.IndexOf(",")))
                        {
                            // Построить путь к сборке, откуда он должен быть загружен.				
                            strTempAssmbPath = PutchApp + @"bin\dll\" + args.Name.Substring(0, args.Name.IndexOf(",")) + ".dll";
                            break;
                        }

                    }


                    break;

            }
            
            //if (args.Name.Substring(0, args.Name.IndexOf(",")) == "FirebirdSql.Data.FirebirdClient")
            //{
                
            //}

            // Загрузка во время выполнения
            bool LoadOnline = false;
            Assembly CachMyAssembly = null;
            if (strTempAssmbPath.Length > 0)
            {
                try
                {
                    // Загрузить файл
                    // Assembly SampleAssembly = Assembly.LoadFrom(SystemConecto.PutchApp + @"bin\dll\FirebirdSql.Data.FirebirdClient.dll");
                    //Load the assembly from the specified path. 					
                    MyAssembly = Assembly.LoadFrom(strTempAssmbPath);
                    //SystemConecto.ErorDebag("Нашел имя и загрузил из системной директории - " + args.Name.Substring(0, args.Name.IndexOf(",")), 1);
                    CachMyAssembly = MyAssembly;
                    LoadOnline = true;
                    // Запускать один раз установку библиотеки
                    Publish publisher = new Publish();
                    publisher.GacInstall(PutchApp + @"bin\dll\" + args.Name.Substring(0, args.Name.IndexOf(",")) + ".dll"); // FirebirdSql.Data.FirebirdClient.dll");

                }
                catch (Exception )//ex_
                {

                    //SystemConecto.ErorDebag(ex_.ToString(), 1);
                    // Фокус с определением значения через кеш значения
                    MyAssembly = null;
                    if (LoadOnline)
                    {
                        MyAssembly = CachMyAssembly;
                    }


                }
            }
            else
            {
                //ErorDebag("Не нашел имени - " + args.Name , 1); //+ "\r\n" +args.RequestingAssembly.EntryPoint.ToString()
                MyAssembly = null;
            }


            //Возвращаем загруженную сборку.
            CachMyAssembly = null;
            return MyAssembly;
        }



        #endregion

             
        
        #region Окно Wait в отдельном потоке -Thread

        public static void WinWaitStart()
        {
            RenderInfo Arguments03 = new RenderInfo() { };
            Thread thStartTimer03 = new Thread(WinWait_Th);
            thStartTimer03.SetApartmentState(ApartmentState.STA);
            thStartTimer03.IsBackground = true; // Фоновый поток
            thStartTimer03.Start(Arguments03);

        }
        public static void WinWait_Th(object ThreadObj)
        {

            // Процедура требующия окна wait
            WaitMessage WindowWait = new WaitMessage();
            //WindowWait.Owner = ConectoWorkSpace_InW;  //AddOwnedForm(OblakoNizWindow);
            // Не активировать окно - не передавать клавиатурный фокус
            WindowWait.ShowActivated = false;
            // Отображаем не показываем модальным 
            // (модальное окно не закрывается методом Close, а также не освобождает ресурсы метод .Dispose(); )
            WindowWait.Show();


            // Поток основного окна
            //System.Windows.Application.Current.Dispatcher.Invoke(new Action(delegate()
            //{
            //    // Ссылка на объект
            //    MainWindow ConectoWorkSpace_InW = (MainWindow)App.Current.MainWindow;
            //    //ConectoWorkSpace_InW.Cursor = System.Windows.Input.Cursors.AppStarting;

            //}));

            // Для Форм
            //System.Windows.Forms.Application.OpenForms["ConectoWorkSpace"].BeginInvoke(new Action(delegate()
            //{
            //    Wait fWait = new Wait(); // создаем
            //    System.Windows.Forms.Application.OpenForms["ConectoWorkSpace"].AddOwnedForm(fWait);
            //    fWait.Show(); // не показываем модальным (модальное окно не закрывается методом Close, а также не освобождает ресурсы метод fWait.Dispose(); )
            //}));

        }
        public static void WinWait_Close()
        {
            // Закрыть окно
            System.Windows.Application.Current.Dispatcher.Invoke(new Action(delegate()
            {

                // Ссылка на объект
                //MainWindow ConectoWorkSpace_InW = (MainWindow)Application.Current.MainWindow;
                MainWindow ConectoWorkSpace_InW = AppStart.LinkMainWindow();

                var Window = SystemConecto.ListWindowMain("WaitMessageW");
                if (Window != null)
                {
                    Window.Close();
                }

                ConectoWorkSpace_InW.Cursor = System.Windows.Input.Cursors.Cross;

                // Для форм
                //System.Windows.Forms.Application.OpenForms["ConectoWorkSpace"].BeginInvoke(new Action(delegate()
                //{
                //    if (System.Windows.Forms.Application.OpenForms["Wait"] != null)
                //    {
                //        System.Windows.Forms.Application.OpenForms["Wait"].Close();
                //    }
                //}));
            }));
        }
        #endregion


        #region Управление потоками -Thread глобально в системе v1.2 (Дублируется с SystemConecto)


        /// <summary>
        /// Объект кторый  можно передать потоку в многопотоковой среде например структуру данных
        /// </summary>
        public delegate void ParameterizedThreadStart(object ThreadObj);
        /// <summary>
        /// Структура данных для многопотоковой среды (передача аргументов)
        /// </summary>
        public struct RenderInfo
        {
            public string argument1 { get; set; }
            public string argument2 { get; set; }
            public string argument3 { get; set; }
            public string argument4 { get; set; }
            public string argument5 { get; set; }
        }
        //public static IPAddress ips_s { get; set; } // Айпи адресс кторый запрашивают в многопотоковой среде
        /// <summary>
        /// Код, защищенный таким образом от неопределённости в плане многопотокового исполнения, называется потокобезопасным. 
        /// Все потоки при записи борются за блокировку объекта
        /// </summary>
        static object locker1 = new object();
        static object locker2 = new object();
        /// <summary>
        /// Отслеживание выполнения потоков locker[0] -количество зарегистрировавшихся потоков, 
        /// locker[1] - количество потоков кторые завершили выполнятся
        /// </summary>
        public static int[] locker_a = new int[2] { 0, 0 };
        //static int[] locker_a { get; set; }

        // Пример использования
        // RenderInfo Arguments = new RenderInfo() { argument1 = IpSplit, argument2 = i.ToString(), argument3 = NetworkPC[dani.Key + "_TypeInterf"] };
        // Thread th_ip = new Thread(PingNet_Th);
        // threads[i] = th_ip;
        // threads[i].SetApartmentState(ApartmentState.STA);
        // threads[i].IsBackground = true; // Фоновый поток
        // threads[i].Start(Arguments);
        #endregion


        #region Режимы выключения программы и компьютера в режиме терминала
        /// <summary>
        /// Выключение программы и компьютера
        /// </summary>
        public static void EndWorkPC()
        {
            var Window = SystemConecto.ListWindowMain("WaitFonW");
            if (Window != null)
            {
                Window.Show();
            }
            else
            {
                WaitFon FonWindow = new WaitFon();
                FonWindow.Owner = Window;
                FonWindow.Show();
            }
            // Параметры запуска
            switch (App.aSystemVirable["type_app"])
            {
                case "-p":
                    // Очень нужное событие для чистки экрана от ТаскБара
                    SystemConecto.notifyIcon.Dispose(); //the icon would clean up automatically, but this is cleaner
                    break;
            }

            Environment.Exit(0); // Это работает
        }
        #endregion


        #region Шифрование файлов XML и прочих копия AppStart упращение

        /// <summary>
        ///  Шифрование файлов XML и прочих
        /// </summary>
        /// <param name="FileName"></param>
        /// <param name="Data"></param>
        /// <param name="TypeCript">Резерв метод шифрования</param>
        public static void EncryptTextToFile(String FileName, String Data, int TypeCript = 0)
        {
            //if (IV == null || IV.Length <= 0)
            //   throw new ArgumentNullException("IV");

            FileStream fStream = null;
            CryptoStream cStream = null;

            try
            {
                using (fStream = new FileStream(FileName, FileMode.OpenOrCreate, FileAccess.Write))
                {

                    //System.Text.ASCIIEncoding encoding = new System.Text.ASCIIEncoding(); //Encoding.Unicode
                    ICryptoTransform desencrypt = des.CreateEncryptor();
                    cStream = new System.Security.Cryptography.CryptoStream(fStream, desencrypt, CryptoStreamMode.Write);
                    // Альтернатива Write
                    //byte[] mybytes = Encoding.UTF8.GetBytes(Data);
                    //cStream.Write(mybytes, 0, mybytes.Length);

                    //Создать StreamWriter с помощью использования CryptoStream.
                    StreamWriter sWriter_ = new StreamWriter(cStream);
                    try
                    {
                        // Запись зашифрованых данных в потоке.
                        sWriter_.Write(Data);
                    }
                    catch (Exception )//error
                    {
                        //SystemConecto.ErorDebag("Ошибка во время записи зашифрованных данных в файл: " + Environment.NewLine +
                        //    " === Файл: " + FileName + Environment.NewLine +
                        //    " === Message: " + error.Message.ToString() + Environment.NewLine +
                        //    " === Exception: " + error.ToString());
                    }
                    finally
                    {
                        //cStream.FlushFinalBlock();
                        // Закрываем потоки и закрываем файл.
                        sWriter_.Close();
                        cStream.Close();
                        fStream.Close();
                    }


                }

            }
            catch (Exception )//error
            {
               // SystemConecto.ErorDebag("Ошибка во время зашифровки: " + Environment.NewLine +
               //" === Файл: " + FileName + Environment.NewLine +
               //" === Message: " + error.Message.ToString() + Environment.NewLine +
               //" === Exception: " + error.ToString());
            }
            finally
            {

                // Close the streams and
                // close the file.
                if (cStream != null)
                    cStream.Close();
                if (fStream != null)
                    fStream.Close();
            }
        }

        /// <summary>
        ///  Шифрование файлов XML и прочих
        /// </summary>
        /// <param name="FileName"></param>
        /// <param name="TypeCript">Резерв метод шифрования</param>
        public static string DecryptTextToFile(String FileName, int TypeCript = 0)
        {

            // SystemConecto.ErorDebag("----------- Расшифровал:", 3);
            //if (IV == null || IV.Length <= 0)
            //   throw new ArgumentNullException("IV");
            string decryptedtext = "";

            FileStream fsread = null;
            CryptoStream cStream = null;

            try
            {
                using (fsread = new FileStream(FileName, FileMode.Open, ((App.aSystemVirable["UserWindowIdentity"] == "1")? FileAccess.ReadWrite : FileAccess.Read)))
                {

                    // Чтение зашифрованных данных в string
                    //byte[] encbytes = new byte[fsread.Length];
                    //fsread.Read(encbytes, 0, encbytes.Length);
                    //string ValueEncript = Encoding.Unicode.GetString(encbytes);
                    //fsread.Position = 0;


                    //System.Text.ASCIIEncoding encoding = new System.Text.ASCIIEncoding();

                    ICryptoTransform desdecrypt = des.CreateDecryptor();
                    cStream = new System.Security.Cryptography.CryptoStream(fsread, desdecrypt, CryptoStreamMode.Read);
                    //byte[] decrByte = new byte[fsread.Length];

                    //cStream.Read(decrByte, 0, (int)fsread.Length);
                    //decryptedtext = Encoding.UTF8.GetString(decrByte);
                    // fsread.Position = 0;

                    StreamReader sReader = new StreamReader(cStream);

                    try
                    {
                        // Чтение только до первого символа начало строки
                        //decryptedtext = sReader.ReadLine();
                        decryptedtext = sReader.ReadToEnd();
                        // File.WriteAllText(SystemConecto.PutchApp + "decript.xml", decryptedtext, Encoding.Unicode);

                    }
                    catch (Exception )//error
                    {
                       // SystemConecto.ErorDebag("Ошибка во время чтения из файла: " + Environment.NewLine +
                       //" === Файл: " + FileName + Environment.NewLine +
                       //" === Message: " + error.Message.ToString() + Environment.NewLine +
                       //" === Exception: " + error.ToString());
                    }
                    finally
                    {

                        //Close the streams and
                        //close the file.
                        sReader.Close();
                        cStream.Close();
                        fsread.Close();
                    }

                }

            }
            catch (Exception )//error
            {
               // SystemConecto.ErorDebag("Ошибка во время расшифровки: " + Environment.NewLine +
               //" === Файл: " + FileName + Environment.NewLine +
               //" === Message: " + error.Message.ToString() + Environment.NewLine +
               //" === Exception: " + error.ToString());
            }
            finally
            {

                // Close the streams and
                // close the file.
                if (cStream != null)
                    cStream.Close();
                if (fsread != null)
                    fsread.Close();

            }
            return decryptedtext;
        }

        /// <summary>
        /// Включение типа шифрования
        /// </summary>
        /// <param name="ind"></param>
        public static void SetProvider(int ind = 6)
        {

            switch (ind)
            {
                case 1:
                    des = new DESCryptoServiceProvider();
                    break;
                case 2:
                    des = new RC2CryptoServiceProvider();
                    break;
                case 3:
                    des = new RijndaelManaged();
                    break;
                case 4:
                    des = new TripleDESCryptoServiceProvider();
                    break;
                case 5:
                    des = SymmetricAlgorithm.Create();
                    break;
                case 6:
                    des = Rijndael.Create();
                    break;

            }

            //Rfc2898DeriveBytes passwordKey = new Rfc2898DeriveBytes(SystemConfig.sData, Encoding.ASCII.GetBytes("File Encryptor Crypto IV"));

            //des.Key = passwordKey.GetBytes(des.KeySize / 8);
            //des.IV = passwordKey.GetBytes(des.BlockSize / 8);

            des.Padding = PaddingMode.Zeros;

            byte[] Key = { 0x01, 0x14, 0x03, 0x16, 0x08, 0x06, 0x07, 0x05, 0x09, 0x10, 0x11, 0x12, 0x13, 0x02, 0x15, 0x04 };
            byte[] IV = { 0x14, 0x02, 0x09, 0x04, 0x05, 0x06, 0x07, 0x08, 0x03, 0x10, 0x11, 0x01, 0x13, 0x12, 0x15, 0x16 };

            des.Key = Key;
            des.IV = IV;

        }




        #endregion


        #region Проверка записи в реестре
        /// <summary>
        /// Управление записями реестра Windows
        /// <par>0 - По умолчанию проверка системной ветки</par>
        /// <par>1 - Проверка ветки LocalMachine HKLM</par>
        /// <param name="NameKey">Дерево реестра</param>
        /// <param name="TypeChek">Тип управления:</param>
        /// <returns></returns>        
        /// </summary>
        public static bool CHEKREG_(int TypeChek = 0, string prOpenSubKey = null, string prOpenSubKey2 = null, string prOpenSubKey3 = null)
        {
            RegistryKey ChekReg = null;

            switch (TypeChek)
            {
                case 0:
                    // Проверка системной ветки
                    if (rkAppSeting == null)
                    {
                        // Запись Ошибки 
                        return false;
                    }
                    ChekReg = rkAppSeting;
                    break;
                case 1:
                    if (prOpenSubKey != null && prOpenSubKey2 != null && prOpenSubKey3 != null)
                    {
                        ChekReg = Registry.LocalMachine.OpenSubKey(prOpenSubKey).OpenSubKey(prOpenSubKey2);
                        if (ChekReg == null)
                        {
                            // Запись Ошибки 
                            return false;
                        }
                        RegistryKey FoundKey = ChekReg.OpenSubKey(prOpenSubKey3);
                        if (FoundKey == null)
                        {
                            // Запись Ошибки 
                            return false;
                        }
                    }

                    break;
                    // Registry.CurrentUser.CreateSubKey(@"System\Alt-Tab\App");
                    //// Чтение
                    //if (rkApp.GetValue("MyApp") == null)
                    //   // Установка
                    //    rkApp.SetValue("MyApp", Application.ExecutablePath.ToString());
                    //// Удаление
                    //    rkApp.DeleteValue("MyApp", false);


            }

            if (ChekReg == null)
            {
                // Запись Ошибки 
                return false;
            }

            return true;
        }

        // Перебор ветки реестра
        //RegistryKey cu = Registry.CurrentUser;

        //  RegistryKey regKey = cu.OpenSubKey("Software");
        //  String[] names = regKey.GetSubKeyNames();

        //  Console.WriteLine("Subkeys of " + regKey.Name);
        //  Console.WriteLine("-----------------------------------------------");
        //  foreach (String s in names)
        //  {
        //      Console.WriteLine(s);
        //  }


        #endregion


        //------------------------------------------------------------------- Загрузка конфиг файлов

        #region Конфигурация приложений и функций ConectoWorkSpace appplay.xml

        //public static bool Load_appplay()
        //{
        //    // Чтение параметров приложения
        //    if (SystemConecto.File_(SystemConecto.PutchApp + "appplay.xml", 5))
        //    {
        //        // Параметры-Приложений- считать в память
        //        // Проверка целосности и правильности и Чтение (авто востановление файла из рез. копии или востановление структуры)
        //        if (!SysConf.ReadConfigXMLAppPlay())
        //        {
        //            // Перезапуск если разрушена целосность конфигурационного файла
        //            if (!SysConf.ReadConfigXMLAppPlay())
        //            {
        //                //SystemConecto.STOP = true;
        //                return false;
        //            }
        //        }
        //    }
        //    else
        //    {
        //        int TypeLoad = 1;
        //        if (ConectoWorkSpace.Administrator.AdminPanels.TableXml.Count() == 0)
        //        {
        //            if (!SysConf.ReadConfigXMLAppPlay(TypeLoad))
        //            {
        //                SystemConecto.STOP = true;
        //                return false;
        //            }
        //        }
        //    }
        //    return true;
        //    // var Test = SystemConecto.SysConf.aParamAppPlay[121].id;
        //}
        #endregion

        //-------------------------------------------------------------------- Дубли контуры  копия в SystemConecto

        #region Сжатие данных   Разработка!
        public class Compress
        {

            #region Пример использования
            // Архивирование
            // Упаковать файлы в архив test.zip из директории DBsevsa
            //Compress.AddDirectory(@"D:\!Project\Temp\DBsevsa");
            //Compress.ToCompressZip(@"D:\!Project\Temp\1\test.zip");

            // Извлечь файлы из архива 26_sevas09.rar в Директорию D:\!Project\Temp\2
            // Compress.ToDecompressFile(@"D:\!Project\Temp\26_sevas09.rar", @"D:\!Project\Temp\2");

            // Compress.AddCompressDirectory(@"D:\!Project\Temp\DBsevsa\1");

            #endregion
            /// <summary>
            /// коментарий при жатии в архивном файле.
            /// </summary>
            public static string Comment_ { get; set; }

            /// <summary>
            /// Путь к последнему упакованому архиву (с названием файла)
            /// </summary>
            public static FileInfo PuthFileComp_ { get; set; }

            /// <summary>
            /// Список путей файлов сжатия AddFile
            /// </summary>
            private static List<string> LFiles_ = new List<string>();
            //  LFiles.Add("C:\readme.txt");

            /// <summary>
            /// Список путей сжатых файлов CompressFile, для распоковки
            /// </summary>
            private static List<string> LFilesComp_ = new List<string>();
            //  LFiles.Add("C:\readme.txt.gz");


            /// установка уровня сжатия CompressionLevel
            /// // устанавливаем пароль к архиву Password

            /// <summary>
            /// Формирует список файлов для архирования
            /// </summary>
            /// <param name="Putch">путь к файлу для сжатия</param>
            public static void AddFile(string Puth)
            {
                // Проверка файла на существование используем класс SystemConecto

                LFiles_.Add(Puth);
                // Удаляем по индексу строку "C:\readme.txt".
                // LFiles_.RemoveAt(0);
                // Перебор масива
                // foreach (var item in LFiles_) { }

            }

            /// <summary>
            /// Формирует список файлов для извлечения
            /// </summary>
            /// <param name="Putch">путь к сжатому файлу</param>
            public static void AddCompressFile(string Puth)
            {
                // Проверка файла на существование используем класс SystemConecto

                LFilesComp_.Add(Puth);
                // Удаляем по индексу строку "C:\readme.txt".
                // LFilesComp_.RemoveAt(0);
                // Перебор масива
                // foreach (var item in LFilesComp_) { }

            }



            /// <summary>
            /// Формирует список из файлов в директории исключая все поддиректории
            /// </summary>
            /// <param name="Putch">Путь сжимаемой директории</param>
            public static void AddDirectory(string directoryPath)
            {

                DirectoryInfo directorySelected = new DirectoryInfo(directoryPath);
                // Только файлы
                foreach (FileInfo fileToCompress in directorySelected.GetFiles())
                {


                    // Уточнение пути 
                    // Предотвращение сжатия уже сжатых файлов.
                    if (fileToCompress.Extension != ".gz")
                    {
                        LFiles_.Add(fileToCompress.FullName);

                    }
                    // Предотвращение сжатия скрытых и уже сжатых файлов.  
                    //if ((File.GetAttributes(fileToCompress.FullName)
                    //    & FileAttributes.Hidden)
                    //    != FileAttributes.Hidden & fileToCompress.Extension != ".gz")
                    //{
                    //}
                }

            }

            /// <summary>
            /// Формирует список из упакованных файлов в директории исключая все поддиректории
            /// </summary>
            /// <param name="Putch">Путь директории в которой лежат упакованные файлы</param>
            /// <param name="ExteCompress">Расширение типа сжатия, по умолчанию - пток .gz</param>
            public static void AddCompressDirectory(string directoryPath, string ExteCompress = ".gz")
            {

                DirectoryInfo directorySelected = new DirectoryInfo(directoryPath);
                // Только файлы
                foreach (FileInfo fileToCompress in directorySelected.GetFiles())
                {

                    // Уточнение пути 
                    // Определения сжатых файлов.
                    if (fileToCompress.Extension == ExteCompress)
                    {
                        LFilesComp_.Add(fileToCompress.FullName);

                    }
                }

            }



            /// <summary>
            /// Упаковка файлов в потоке
            /// </summary>
            /// <param name="NameFileCom">Имя файла сжатого файла</param>
            /// <param name="TypeCompress">тип компресии: Def- по умалчанию компресия файлов</param>
            public static void ToCompressStrem(string DirectoryFileCom = "", string TypeCompress = "Def")   // FileInfo fi
            {
                var PathFileCom = "";
                // Читаем список файлов
                foreach (var item in LFiles_)
                {
                    FileInfo Comfile = new FileInfo(item);
                    // Проверка названия сжимаемого файла
                    if (DirectoryFileCom == "")
                    {
                        PathFileCom = Comfile.FullName + ".gz";
                    }
                    else
                    {
                        // Положить в другую директорию
                        // Проверка директории используем класс SystemConecto

                        PathFileCom = DirectoryFileCom + @"\" + Comfile.Name + ".gz";
                    }


                    // Ошибки файловой структуры
                    try
                    {

                        // Получить поток исходного файла.
                        using (FileStream inFile = Comfile.OpenRead())
                        {

                            // cоздать сжатый файл.
                            using (FileStream outFile = System.IO.File.Create(PathFileCom))
                            {
                                // записать в файл
                                using (GZipStream Compress = new GZipStream(outFile, CompressionMode.Compress))
                                {
                                    // Скопируйте исходный файл в поток сжатия.
                                    inFile.CopyTo(Compress);
                                }
                            }
                            // Последний упакованный
                            PuthFileComp_ = new FileInfo(PathFileCom);
                        }
                    }
                    catch (Exception Ex)
                    {
                        // Ошибка, значит интернета у нас нет. Плачем :'(
                        if (SystemConecto.DebagApp)
                        {
                            // Тут нужна дополнительная аналитика например ошибка 403 тоже попадает сюда.
                            SystemConecto.ErorDebag("Ошибка сжатия файла: " + Ex + "." + " Путь к файлу: [" + Comfile.FullName + "]", 1);
                        }
                    }
                }

            }


            /// <summary>
            /// Распоковка файла в потоке
            /// </summary>
            /// <param name="PuthFile">Путь к сжатому файлу</param>
            /// <param name="PuthFileTo">Путь распоковки</param>
            /// <param name="EncryptTextToFile">Тип шифрование и дешифрование файла</param>
            public static void ToDecompressStrem(string PuthFile = "", string PuthFileTo = "", int TypeEncryptDecriptTextToFile = 0)
            {

                /// Список путей сжатых файлов CompressFile, для распоковки
                List<string> LFilesDecompress = new List<string>();

                if (PuthFile == "")
                {
                    // Последний упакованный файл (как альтернатива пути) LFilesComp_.Count == 0
                    if (PuthFileComp_.ToString().Length > 0)
                    {
                        LFilesDecompress.Add(PuthFileComp_.ToString());
                        //Compress.AddCompressFile(PuthFileComp_.ToString());
                        // FileInfo fi = PuthFileComp_;
                    }
                    else
                    {
                        return;
                    }

                }
                else
                {
                    // Проверка директории используем класс SystemConecto
                    LFilesDecompress.Add(PuthFile);
                    //Compress.AddCompressFile(PuthFile);
                }



                // Читаем список файлов
                foreach (var item in LFilesDecompress)
                {

                    FileStream inFile = null;
                    //Создать распаковать файл.
                    try
                    {

                        FileInfo fi = new FileInfo(item);

                        // Получить поток исходного файла.
                        inFile = fi.OpenRead();
                        //using (inFile = fi.OpenRead())
                        //{

                        // Оригинальное расширение файла, например,
                        // "doc" из report.doc.gz.
                        string curFile = fi.FullName;
                        string origName = curFile.Remove(curFile.Length - fi.Extension.Length);
                        // Путь размещения распаковки
                        if (PuthFileTo != "")
                        {
                            // Проверка директории используем класс SystemConecto
                            var FiorigName = new FileInfo(origName);
                            origName = PuthFileTo + @"\" + FiorigName.Name;
                        }

                        switch (TypeEncryptDecriptTextToFile)
                        {
                            case 1:

                                // Шифрование данных полученных из упакованого файла (без шифрования)
                                using (MemoryStream outFile = new MemoryStream())
                                {
                                    using (GZipStream Decompress = new GZipStream(inFile, CompressionMode.Decompress))
                                    {
                                        // Декомпрессия потока в выходной файл.
                                        Decompress.CopyTo(outFile);
                                    }
                                    // Шифруем
                                    EncryptTextToFile(origName, outFile.ToString());
                                    outFile.Dispose();
                                }

                                break;
                            default:
                                using (FileStream outFile = System.IO.File.Create(origName))
                                {
                                    using (GZipStream Decompress = new GZipStream(inFile, CompressionMode.Decompress))
                                    {
                                        // Декомпрессия потока в выходной файл.
                                        Decompress.CopyTo(outFile);
                                    }
                                }
                                break;
                        }

                        //}

                    }
                    finally
                    {
                        if (inFile != null)
                            inFile.Dispose();
                    }

                }
            }


            /// <summary>
            /// Упаковка файлов zip (на включена в версию Framework 4.0)
            /// </summary>
            /// <param name="NameFileCom">Имя файла сжатого файла</param>
            /// <param name="TypeCompress">тип компресии: Def- по умалчанию компресия файлов</param>
            public static void ToCompressZip(string PathFileCom = "", string TypeCompress = "Def")   // FileInfo fi
            {

                // Проверка соответсвия переменной PathFileCom используем класс SystemConecto

                // Окрыть сжимаемы й файл и наполнить его сфайлами
                // Определяем имя архива
                // Читаем список файлов
                foreach (var item in LFiles_)
                {
                    FileInfo Comfile = new FileInfo(item);
                    // Проверка названия сжимаемого файла
                    if (PathFileCom == "")
                    {
                        PathFileCom = NameFileCompress(Comfile.DirectoryName.ToString());
                        // Нет пути выход
                        if (PathFileCom == "")
                        {
                            return;
                        }
                    }
                    break;
                }
                // Ошибки файловой структуры
                try
                {
                    // cоздать сжатый файл.
                    using (FileStream zipToOpen = new FileStream(PathFileCom, FileMode.OpenOrCreate))
                    // using (FileStream outFile = File.Create(PathFileCom))
                    {
                        // Только для Framework 4.5
                        // Открыть сжатый файл для обновления данными (записать в файл)
                        //using (var archive = new ZipArchive(zipToOpen, ZipArchiveMode.Update))
                        //// using (GZipStream Compress = new GZipStream(outFile, CompressionMode.Compress))
                        //{
                        //    // Упаковка файлов

                        //    // Читаем список файлов
                        //    foreach (var item in LFiles_)
                        //    {

                        //        // Ошибки файловой структуры
                        //        try
                        //        {
                        //            var Comfile = new FileInfo(item);
                        //            var item_com = item;
                        //            if (TypeCompress == "Def")
                        //            {
                        //                item_com = Comfile.Name;
                        //            }

                        //            // ==== Разработка потокового переноса файлов в архив

                        //            //ZipArchiveEntry fileEntry = archive.CreateEntry(item_com);

                        //            //// Получить поток исходного файла.  - using (FileStream inFile = Comfile.OpenRead())
                        //            //using (StreamReader inFile = new StreamReader(item))
                        //            //{
                        //            //    var File = inFile.ReadToEnd(); // Есть сомнения по поводу памяти Компьютера и буфера
                        //            //    // Запись данных
                        //            //    using (StreamWriter writer = new StreamWriter(fileEntry.Open()))
                        //            //    {
                        //            //        writer.Write(File); // fileEntry.Open()
                        //            //    }
                        //            //}

                        //            //============== Для ленивых  - нужна библиотека - Microsoft.NET\Framework\v4.0.30319\System.IO.Compression.FileSystem.dll
                        //            archive.CreateEntryFromFile(item, item_com); 


                        //            // ============== Можно использовать для создания текстовых файлов
                        //            // c необходимыми записями внем.

                        //            // Создать пустой файл
                        //            // ZipArchiveEntry readmeEntry = archive.CreateEntry(item_com);

                        //            // Получить поток исходного файла.  - using (FileStream inFile = Comfile.OpenRead())
                        //            //using (StreamWriter writer = new StreamWriter(readmeEntry.Open()))
                        //            //{
                        //            //    writer.WriteLine("Information about this package.");
                        //            //    writer.WriteLine("========================");
                        //            //}



                        //            // Последний упакованный
                        //            PuthFileComp_ = new FileInfo(PathFileCom);

                        //        }
                        //        catch (Exception Ex)
                        //        {
                        //            // Ошибка, значит интернета у нас нет. Плачем :'(
                        //            if (SystemConecto.DebagApp)
                        //            {
                        //                // Тут нужна дополнительная аналитика например ошибка 403 тоже попадает сюда.
                        //                SystemConecto.ErorDebag("Ошибка сжатия файла: " + Ex + "." + " Путь к файлу: [" + item + "]", 1);
                        //            }
                        //        }
                        //    }
                        //}
                    }

                }
                catch (Exception )//ExArh
                {
                    // Ошибка, значит интернета у нас нет. Плачем :'(
                    //if (SystemConecto.DebagApp)
                    //{
                    //    // Тут нужна дополнительная аналитика например ошибка 403 тоже попадает сюда.
                    //    SystemConecto.ErorDebag("Ошибка файла сжатия : " + ExArh + "." + " Путь к файлу: [" + PathFileCom + "]", 1);
                    //}
                }

            }


            /// <summary>
            /// Распоковка файла из архивов
            /// </summary>
            /// <param name="PuthFile">Путь к сжатому файлу</param>
            /// <param name="PuthFileTo">Путь распоковки</param>
            public static void ToDecompressFile(string PuthFile = "", string PuthFileTo = "")
            {

                if (PuthFile != "")
                {
                    // Проверка директории используем класс SystemConecto
                    Compress.AddCompressFile(PuthFile);
                }

                // Последний упакованный файл (как альтернатива пути)
                if (LFilesComp_.Count == 0)
                {
                    Compress.AddCompressFile(PuthFileComp_.ToString());
                    // FileInfo fi = PuthFileComp_;
                }

                // Читаем список файлов
                foreach (var item in LFilesComp_)
                {

                    FileInfo fi = new FileInfo(item);
                    var origNameExt = "";
                    // Путь размещения распаковки
                    if (PuthFileTo != "")
                    {
                        // Проверка директории используем класс SystemConecto + @"\"
                        origNameExt = PuthFileTo;
                    }
                    else
                    {
                        // Путь архива по умолчанию и он всегда есть
                        origNameExt = fi.DirectoryName.ToString();

                    }


                    try
                    {
                        // Проверка типа файла
                        switch (fi.Extension)
                        {
                            case ".zip":
                                //using (ZipArchive archive = ZipFile.OpenRead(item))
                                //{
                                //    foreach (ZipArchiveEntry entry in archive.Entries)
                                //    {
                                //        entry.ExtractToFile(Path.Combine(origNameExt, entry.FullName));

                                //        // ================= Разархивирование по условию
                                //        //if (entry.FullName.EndsWith(".txt", StringComparison.OrdinalIgnoreCase))
                                //        //{
                                //        //    entry.ExtractToFile(Path.Combine(extractPath, entry.FullName));
                                //        //}
                                //    }
                                //}
                                break;
                            case ".rar":

                              

                                break;
                        }

                    }
                    catch (Exception )//ExArh
                    {
                        // Ошибка, значит интернета у нас нет. Плачем :'(
                        //if (SystemConecto.DebagApp)
                        //{
                        //    // Тут нужна дополнительная аналитика например ошибка 403 тоже попадает сюда.
                        //    SystemConecto.ErorDebag("Ошибка распоковки файла : " + ExArh + "." + " Путь к файлу: [" + item + "]", 1);
                        //}
                    }

                }
            }

            /// <summary>
            /// Формирования название файла сжатия
            /// </summary>
            /// <returns>возвращает название сжатого файла</returns>
            private static string NameFileCompress(string Name)
            {

                DirectoryInfo directorySelected = new DirectoryInfo(Name);
                return Name + @"\" + directorySelected.Name.ToString() + ".zip";
            }



            /// <summary>
            /// Шифрует и сжимает вход. Возвращает сжатый и зашифрованный контент и ключ и IV вектора используется.
            /// </summary>
            /// <param name="input">Массив данных</param>
            /// <param name="key">Ключ, который был использован в алгоритме.</param>
            /// <param name="iv">Вектор IV, который был использован в алгоритме.</param>
            /// <returns></returns>
            public byte[] EncryptAndCompress(byte[] input, out byte[] key, out byte[] iv)
            {
                if (input == null)
                    throw new ArgumentNullException("input");

                // Сжатие массива данных в данный поток памяти.
                MemoryStream stream = new MemoryStream();
                using (GZipStream zip = new GZipStream(stream, CompressionMode.Compress, true))
                {
                    // Запись данных в поток памяти с помощью ZIP потока.
                    zip.Write(input, 0, input.Length);
                }

                // Создание ключей и инициализировать the rijndael.
                RijndaelManaged r = new RijndaelManaged();
                r.GenerateKey();
                r.GenerateIV();
                // Установить сгенерированный ключ и IV вектор.
                key = r.Key;
                iv = r.IV;

                // Шифрование сжатого потока памяти в зашифрованный поток памяти.
                MemoryStream encrypted = new MemoryStream();
                using (CryptoStream cryptor = new CryptoStream(encrypted, r.CreateEncryptor(), CryptoStreamMode.Write))
                {
                    // Записать поток в зашифрованный поток памяти.
                    cryptor.Write(stream.ToArray(), 0, (int)stream.Length);
                    cryptor.FlushFinalBlock();
                    // Вернуть результат
                    return encrypted.ToArray();
                }
            }

            /// <summary>
            /// Decrypts and decompresses the input. Returns the decompressed and decrypted content.
            /// </summary>
            /// <param name="input">The input array.</param>
            /// <param name="key">The key used for decrypt.</param>
            /// <param name="iv">The iv vector used for decrypt.</param>
            /// <returns></returns>
            public byte[] DecryptAndDecompress(byte[] input, byte[] key, byte[] iv)
            {
                if (input == null)
                    throw new ArgumentNullException("input");
                if (key == null)
                    throw new ArgumentNullException("key");
                if (iv == null)
                    throw new ArgumentNullException("iv");

                // Initialize the rijndael
                RijndaelManaged r = new RijndaelManaged();
                // Create the array that holds the result.
                byte[] decrypted = new byte[input.Length];
                // Create the crypto stream that is used for decrypt. The first argument holds the input as memory stream.
                using (CryptoStream decryptor = new CryptoStream(new MemoryStream(input), r.CreateDecryptor(key, iv), CryptoStreamMode.Read))
                {
                    // Read the encrypted values into the decrypted stream. Decrypts the content.
                    decryptor.Read(decrypted, 0, decrypted.Length);
                }

                // Создать сжатый поток для распаковки.
                using (GZipStream zip = new GZipStream(new MemoryStream(decrypted), CompressionMode.Decompress, false))
                {
                    // Читать все байты в сжатый поток и вернуть их.
                    return ReadAllBytes(zip);
                }
            }

            /// <summary>
            /// Читает все байты в данном сжатом потоке и возвращает их.
            /// </summary>
            /// <param name="zip">Сжатый поток, который обрабатывается.</param>
            /// <returns></returns>
            private byte[] ReadAllBytes(GZipStream zip)
            {
                if (zip == null)
                    throw new ArgumentNullException("zip");

                int buffersize = 100;
                byte[] buffer = new byte[buffersize];
                int offset = 0, read = 0, size = 0;
                do
                {
                    // Если буфер не предлагает достаточно места, создаем новый массив двойного размера
                    // Скопируем текущее содержание буфера в этот массив и используем его как новый буфер
                    if (buffer.Length < size + buffersize)
                    {
                        byte[] tmp = new byte[buffer.Length * 2];
                        Array.Copy(buffer, tmp, buffer.Length);
                        buffer = tmp;
                    }

                    // Читает число распакованных данных.
                    read = zip.Read(buffer, offset, buffersize);

                    // Прирост смещение на размер прочитаного.
                    offset += buffersize;
                    size += read;
                } while (read == buffersize); // Прекращается, если мы читаем меньше, чем размер буфера.

                // Копируем только то, количество данных, которое на самом деле было прочитано
                byte[] result = new byte[size];
                Array.Copy(buffer, result, size);
                return result;
            }
        }
        /*
         * Пример использования
         * byte[] key = null;
         * byte[] iv = null;
         * byte[] input = File.ReadAllBytes(“C:\\Users\\Chris\\Documents\\UNI\\Untitled.bmp”);
         * byte[] result = EncryptAndCompressUtility.EncryptAndCompress(input, out key, out iv);
         * File.WriteAllBytes(“C:\\Users\\Chris\\Documents\\UNI\\foo compressed.bmp”, result);
         * byte[] result2 = EncryptAndCompressUtility.DecryptAndDecompress(File.ReadAllBytes(“C:\\Users\\Chris\\Documents\\UNI\\foo compressed.bmp”), key, iv);
         * File.WriteAllBytes(“C:\\Users\\Chris\\Documents\\UNI\\foo.bmp”, result2);
        */

        /* -------------------------------- Изучение других примеров
         * 1. http://msdn.microsoft.com/en-us/library/system.io.compression.gzipstream.aspx
         * 2. http://7-zip.org.ua/ru/sdk.html    (http://sevenzipsharp.codeplex.com/)
         * 5. 
         * 4. using System.IO;  
                using System.IO.Compression;  
                using(FileStream sourceFile = File.OpenRead(@"D:\MyFile.xls"))  
                using(FileStream targetFile = File.Create(@"D:\MyFile.zip"))  
                using (GZipStream gzipStream = new GZipStream(targetFile, CompressionMode.Compress, false))  
                {  
                     try  
                     {  
                          int posByte = sourceFile.ReadByte();  
                      
                          while (posByte != -1)  
                          {  
                               gzipStream.WriteByte((byte)posByte);  
                               posByte = sourceFile.ReadByte();  
                          }  
                     }  
                     catch  
                     {  
                          //  
                     }  
                 }   
         * 
         * 
         * 
         * 
         * 
         * 
         * 
         */

        #endregion



    }
}
