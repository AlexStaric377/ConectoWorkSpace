using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Windows;
// --- using System.Windows.Forms;
using System.IO;
using System.Diagnostics;
using IWshRuntimeLibrary;
// --- Многопоточность
using System.Threading;
using System.Windows.Threading;

// --- Заставка
using ConectoWorkSpace.Splasher_startWindow;
// --- События Windows OS
using Microsoft.Win32;
// --- шифрование данных
using System.Security.Cryptography;
using System.Text;
// Подключение GAC for Net для SystemConecto
// using System.EnterpriseServices.Internal;
// Импорт библиотек Windows DllImport (управление питанием ОС, ...
// using System.Runtime.InteropServices;
//--- для Проверки Сборок
using System.Reflection;
//--- Права пользователя
using System.Security;
using System.Security.Principal;
// Регулярные выражения
using System.Text.RegularExpressions;

using System.Runtime.InteropServices;
// Нужен для Mutex
// using System.Threading;
// TaskbarIcon
//using Hardcodet.Wpf.TaskbarNotification;
using Main_Window;
using System.ServiceProcess;
using System.Management;



namespace ConectoWorkSpace
{

    /// <summary>
    /// Логика взаимодействия для App.xaml
    /// </summary>
    public partial class App : System.Windows.Application, System.Windows.Markup.IComponentConnector
    {

        /// <summary>
        /// Словарь системных переменных
        /// </summary>
        public static Dictionary<string, string> aSystemVirable = new Dictionary<string, string>();

        
        public static string NameAutorize = "";
        public static int AutoStartFront = 0;
        /// <summary>
        /// Опеределения пользователя для системы  
        /// 0 - Пользователь не должен быть администратором. 
        /// 1 - Пользователь должен быть администратором 
        /// </summary>
        public static int UserNoRoot = 1;

        /// <summary>
        /// Имя приложения. Используется для идентификации приложения в ОС.
        /// </summary>
        public static string NameOP = "AlTab® WorkSpace";

        /// <summary>
        /// Имя приложения. Используется для идентификации приложения в ОС.
        /// </summary>
        public static string NameOPOS = "Conecto® WorkSpace";

        /// <summary>
        /// Версия FrameWork .Net
        /// </summary>
        public static string VFrameWork = "";
        /// <summary>
        /// Application Entry Point.
        /// </summary>
        [System.STAThreadAttribute()]
        ///[System.Diagnostics.DebuggerNonUserCodeAttribute()] // Изменил базовую настройку
        public static void Main(string[] args)
        {

            //MessageBox.Show("Старт", "Сообщение", MessageBoxButton.OK, MessageBoxImage.Stop);
            //System.Threading.Thread.Sleep(10000);
            //    //[MTAThread]*
            //    /// using System.Threading - Многопоточность

            #region Описание параметров  запуска приложения
            // -c касса имеет два глобальных интерфейса (фронт и офис; переключается как между интерфейсами так и стартует отдельно)
            // -t терминал система с рабочим столом замена explorer или KDE
            // -p панель для удобства пользования с рабочим столом explorer или KDE (содержит доступ к интерфейсу pc report и pc analiz: отчеты и анализ)
            // -s загрузка сервера по умолчанию грузится установленный тип сервера
            //                       **************
            // -concb конслоноое приложение кассы под андроид и IOS тут не реализованно
            // -conuse конслоноое приложение учетной записи под андроид и IOS тут не реализованно
            //             ****************
            // -b оффис кассы с разграничением интерфейса на уровне БД и пользователя (содержит доступ к интерфейсу pc report и pc analiz: отчеты и анализ) (также WEB версия интерфейса)
            // -bu оффис управленческого учета с разграничением интерфейса на уровне БД и пользователя (содержит доступ к интерфейсу pc report и pc analiz: отчеты и анализ) (также WEB версия интерфейса)
            // -bb оффис бухгалтерия с разграничением интерфейса на уровне БД и пользователя (содержит доступ к интерфейсу pc report и pc analiz: отчеты и анализ)(также WEB версия интерфейса)
            //                   ***************
            // *** Второй параметр
            // -us режим пользователя для установки и запуска приложения без админ прав
            //                   ***************
            // без параметров  выполняется запуск кассового места версия может быть терминальной или работать как приложение



            const string subkey = @"SOFTWARE\Microsoft\NET Framework Setup\NDP\v4\Full\";
            using (var ndpKey = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Registry32).OpenSubKey(subkey))
            {
                App.VFrameWork = AppStart.IsFrameWork();
                if (AppStart.IsFrameWork() == "0" || (int)ndpKey.GetValue("Release") < 461308)
                {
                    MessageBox.Show("Отсутствует необходимый Framework.\n" + "Установленная версия:" + App.VFrameWork);
                    // Запустить авто скачку Framework
                    //string FileExe = SystemConectoServers.PutchLib + "NDP471-KB4033344-Web.exe";
                    //System.Diagnostics.Process.Start(FileExe);
                }

            }


            // Запись параметров запуска приложения в массив args_
            string[] args_ = Environment.GetCommandLineArgs();

            // Заметки разделяем строку пути старта приложения для указанного регулярного выражения.
            Regex NameAppMatch = new Regex(@"\s+([^\s])");
            char x_ = ' ';
            string[] IDDir = NameAppMatch.Replace(Path.GetFileNameWithoutExtension(args_[0]), " $1").Split(x_);
            //string[] aDir = AppDomain.CurrentDomain.BaseDirectory.Split(System.IO.Path.DirectorySeparatorChar);

            // args_0 Conecto
            // args_1 Имя приложения, Телефон, ел. адрес.
            // args_2 Если есть только Имя приложения
            // args_3 Остальная запись пользователя любой текст
            //  aSystemVirable["client_id"] - идентификатор клиента демо версии
            // Заставка ConectoWorkSpace
            Splasher.Splash = new SplashScreenCash();
            aSystemVirable["type_app"] = "-p";  //-t -p

            /* Название программы для комментариев*/
            aSystemVirable["CaptionNamePRG"] = "Conecto® CashBox";
 
            aSystemVirable["NameTh"] = "CashBox"; //
            if (args.Count() != 0)
            {
                // Проверка на допустимые параметры
                if (args[0] != "-t" && args[0] != "-p" && args[0] != "-c" && args[0] != "-s" && args[0] != "-b" && args[0] != "-bu" && args[0] != "-bb" ) // && args[0] != "-con"
                {
                    MessageBox.Show("Неправильно задан параметр запуска." + args[0]);
                    Environment.Exit(0);
                }

                aSystemVirable["type_app"] = args[0];
                if (args[0] != "-c" && args[0] != "-b" && args[0] != "-bu" && args[0] != "-bb")
                {
                    aSystemVirable["CaptionNamePRG"] = "Conecto® WorkSpace";
                    Splasher.Splash = new SplashScreenConecto(aSystemVirable["CaptionNamePRG"]);
                    aSystemVirable["NameTh"] = "WorkSpace";  // Conecto - имя привязки к проверке загрузки программы
                    //  исправить
                }
                else
                {
                    if (args[0] != "-c" && args[0] != "-t" && args[0] != "-p" && args[0] != "-s")
                    {
                        // Бек офисы
                        switch (args[0])
                        {
                            case "-b":
                                aSystemVirable["CaptionNamePRG"] = "OfficeSpace CashBox";
                                Splasher.Splash = new SplashScreenConecto(aSystemVirable["CaptionNamePRG"]);
                                aSystemVirable["NameTh"] = "OfficeSpaceCashBox";
                                break;
                            case "-bu":
                                Splasher.Splash = new SplashScreenConecto();
                                aSystemVirable["NameTh"] = "OfficeSpaceManager";
                                break;
                            case "-bb":
                                Splasher.Splash = new SplashScreenConecto();
                                aSystemVirable["NameTh"] = "OfficeSpaceBuh";
                                break;

                        }

                    }

                }

                
            }
            else
            {
 
                // Отключить параметры вручную
                aSystemVirable["NameTh"] = "WorkSpace";
                aSystemVirable["CaptionNamePRG"] = "Conecto® WorkSpace";
                Splasher.Splash = new SplashScreenConecto(aSystemVirable["CaptionNamePRG"]);

                // Заглушка временная запуск без параметров, остановка запуска программы
                MessageBox.Show("Приложение запущенно без параметров, обратитесь к администратору!", "There was an error run");
                Environment.Exit(0);

                // Conecto - имя привязки к проверке загрузки программы
            }

            // Проверка запуска на рабочем столе exe

            string PuthExe = AppDomain.CurrentDomain.BaseDirectory + "Conecto® WorkSpace.exe";
            string PuthWork = AppDomain.CurrentDomain.BaseDirectory.Substring(0, AppDomain.CurrentDomain.BaseDirectory.LastIndexOf(@"\")); 
            //string NameLnk = @"\ConectoWorkSpace.lnk";
            //string Parametr = " -t ";
            //MessageBox.Show("Вход");


            // Следить есть ли ярлык на рабочем столе.
            //if (!System.IO.File.Exists(Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + @"\ConectoWorkSpace.lnk"))
            //{
            //    // Проверка стартовой директории
            //    if (!AppDomain.CurrentDomain.BaseDirectory.Contains((@"c:\Kassa24.com.ua")))
            //    {
            //        // Создать ярлык запуска с пользовательской директории
            //        CreatLnk(PuthExe, PuthExe, NameLnk, Parametr, PuthWork); 
            //    }

            //}

            //  Проверяем наличие ярлыка .exe на рабочем столе
            //if (AppDomain.CurrentDomain.BaseDirectory.Contains(Environment.GetFolderPath(Environment.SpecialFolder.Desktop)))
            //{
            //    // Проверяем есть ли директория c:\Kassa24.com.ua
            //    if (!(Directory.Exists(@"c:\Kassa24.com.ua")))
            //    {
            //        Directory.CreateDirectory(@"c:\Kassa24.com.ua");
            //    }
            //    // мы exe копируем в папку Kassa24.com.ua
            //    System.IO.File.Copy(PuthExe, @"c:\Kassa24.com.ua\Conecto® WorkSpace.exe", true);
            //    // Формируем параметры для ярлыка
            //    PuthExe = @"c:\Kassa24.com.ua\Conecto® WorkSpace.exe";
            //    PuthWork = @"c:\Kassa24.com.ua";
            //    Parametr = " - t -0 -delDesktop";
            //    // создаем ярлык
            //    CreatLnk(PuthExe, PuthExe, NameLnk, Parametr, PuthWork); 

            //    // запускаем ярлык фоном
            //    string shortcutPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + @"\ConectoWorkSpace.lnk";
            //    Process mv_prcInstaller = new Process();
            //    mv_prcInstaller.StartInfo.FileName = shortcutPath;
            //    mv_prcInstaller.StartInfo.Arguments = "";
            //    mv_prcInstaller.Start();
            //    if (Parametr.Contains("delDesktop"))
            //    {
            //        //Редактируем  параметр запуска в ярлыке
            //        WshShell shell = new WshShell();
            //        IWshShortcut shortcut = (IWshShortcut)shell.CreateShortcut(Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + @"\ConectoWorkSpace.lnk");
            //        shortcut.Arguments = " -t"; ; // " - t -0 -delDesktop";
            //        //Сохраняем параметры 
            //        shortcut.Save();
            //    }
            //    Environment.Exit(0);
            //}
 
            //Всегда проверяем рабочий стол
            if (args.Count() > 2)
            {
                switch (args[2])
                {
                    case "-delDesktop":
                        int ind = 0;
                        FileInfo fileInf = new FileInfo(Process.GetCurrentProcess().MainModule.FileName); // разбираем путь FTP
                        string NameFile = fileInf.Name; // это название файла
                        while (System.IO.File.Exists(Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + @"\" + NameFile) && ind <5)
                        {
                            ind++;
                            try
                            {
                                // удаляем файл приложение с рабочего стола
                                Thread.Sleep(2000);
                                System.IO.File.Delete(Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + @"\"+NameFile);
                            }
                            catch (Exception) {} // error
                        }
                     break;
                    case "-us":
                        // -us режим пользователя для установки и запуска приложения без админ прав
                        UserNoRoot = 0;
                    break;



                }
            }



            // Проверка имени приложения
            if (IDDir[1] != "WorkSpace" && IDDir[1] != "CashBox" && IDDir[2] != "WorkSpace" && IDDir[2] != "CashBox")
            {
                MessageBox.Show("Неправильно указано имя приложения exe файла." + IDDir[1]);
                Environment.Exit(0);
            }
            Splasher.ShowSplash();
            #endregion



            //MessageListener.Instance.ReceiveMessage(string.Format("Выполняется: {0}", "загрузка в память " + args[0] + aSystemVirable["type_app"])); //+ args[0] + aSystemVirable["type_app"]

            //MessageListener.Instance.ReceiveMessage(string.Format("Выполняется: {0}", "загрузка в память " + IDDir[1]));   //String.Join(" ", aDir)
            //System.Threading.Thread.Sleep(5000);

            #region Проверка административных прав только Windows

            /// <summary>
            /// Программа может сама проверить запущена ли она с правами администратора: (это большой глюк в системе - ругается на все строки кода которым нужны права администратора)
            /// </summary>
            aSystemVirable["UserWindowIdentity"] = new WindowsPrincipal(WindowsIdentity.GetCurrent()).IsInRole(WindowsBuiltInRole.Administrator) ? "1" : "0";
           // MessageBox.Show("Приложение запущенно без установки, обратитесь к администратору1!", "There was an error run");

            //WindowsIdentity identity = WindowsIdentity.GetCurrent();
            //WindowsPrincipal principal = new WindowsPrincipal(identity);!principal.IsInRole(WindowsBuiltInRole.Administrator)
            if (aSystemVirable["UserWindowIdentity"] == "0")
            {


                // Не работает при отключенном UAC
                bool dfg = UacHelper.IsUacEnabled;



                //MessageListener.Instance.ReceiveMessage(string.Format((dfg ? "y" : "n" ) + "Приложение завершено. {0}", "Запуск осуществляется только с правами администратора."));
                if (dfg){
                    if (Registry.CurrentUser.OpenSubKey(@"System\Alt-Tab\") == null)

                    {
   
                         MessageBox.Show("Приложение запущенно без установки, обратитесь к администратору!", "There was an error run");
                         Environment.Exit(0);
                    }
  
                    aSystemVirable["UserWindowIdentity"] = "-1";
                    
                }
                else
                {
                    // --- Права не ограничены устанавливаем признак для системы
                    aSystemVirable["UserWindowIdentity"] = "1";
                    // Пользовательне не имеет никаких прав в ОС Win

                    RegistryKey regApp = AppStart.rkAppSetingAllUser;

                    regApp.SetValue("TerminalRDP", "0", RegistryValueKind.DWord);
                    regApp.Flush();

                }

                //var processInfo = new ProcessStartInfo(Assembly.GetExecutingAssembly().CodeBase);

                // The following properties run the new process as administrator
                //processInfo.UseShellExecute = true;
                //processInfo.Verb = "runas";

                // Start the new process
                //try
                //{
                //Process.Start(processInfo);
                //   MessageBox.Show("Sorry, this application must be run as Administrator.");
                //}
                //catch (Exception)
                //{
                // The user did not allow the application to run as administrator
                //   MessageBox.Show("Sorry, this application must be run as Administrator.");
                //}

                // Shut down the current process
                // Application.Current.Shutdown();


                //System.Threading.Thread.Sleep(10000);
                //SystemConecto.ErorDebag("Приложение завершено. Нарушено правило запуска программы. Запуск программы осуществляется только с правами администратора.", 0, 2);
                //Environment.Exit(0); // Это работает
                //MessageBox.Show("You must run this application as administrator. Terminating.");
                //Application.Exit();
            }
            else
            {

   
                // MessageBox.Show("Приложение запущенно c административными правами!", "Режим отладки");              
  
  
            }
            // Проверка режима работы включен удаленный доступ к серверу Да -0 запрет включения RDP 1- нет.
            //RegistryKey regApp = AppStart.rkAppSetingAllUser;
            if (AppStart.rkAppSetingAllUser.GetValue("TerminalRDPOnOff") == null )
            { 
                AppStart.rkAppSetingAllUser.SetValue("TerminalRDPOnOff", "0", RegistryValueKind.DWord);
                AppStart.rkAppSetingAllUser.Flush();
                
            }

            //if (MstscOnOff() > 0)regApp.SetValue("TerminalRDPOnOff", "0", RegistryValueKind.DWord);
            //if (MstscOnOff() == 0) regApp.SetValue("TerminalRDPOnOff", "1", RegistryValueKind.DWord);

            

            #endregion


            //System.Threading.Thread.Sleep(4000);

            //var uriSource = new Uri(@"/Conecto®%20WorkSpace;component/Images/logo_shar1.png", UriKind.Relative);
            //this.Pdf1.Source = new BitmapImage(uriSource);
            //SplashScreen splashScreen = new SplashScreen("logo_shar1.png");  //"grimace.png"new BitmapImage(uriSource)

            //splashScreen.Show(true);

            // В многопоточной среде именуем главный поток

            System.Threading.Thread.CurrentThread.Name = aSystemVirable["NameTh"];

            ConectoWorkSpace.App app = new App();
            app.InitializeComponent();
            app.Run();

        }
        // процедура проверки активности подключения режима RDP
        public static int MstscOnOff()
        {
            var RezChek = 0;
            int IndexActivProces = -1;
            string Fb = "mstsc";
            var wmiQueryString = "SELECT ProcessId, ExecutablePath, CommandLine FROM Win32_Process";
            using (var searcher = new ManagementObjectSearcher(wmiQueryString))
            using (var results = searcher.Get())
            {
                var query = from p in Process.GetProcessesByName(Fb)
                            join mo in results.Cast<ManagementObject>()
                            on p.Id equals (int)(uint)mo["ProcessId"]
                            select new
                            {
                                Process = p,
                                Path = (string)mo["ExecutablePath"],
                                CommandLine = (string)mo["CommandLine"],
                            };
                foreach (var item in query)
                {
                    IndexActivProces++;
                }
            }
            if (IndexActivProces >= 0) RezChek = 1;
            return RezChek;
        }
        public static void CreatLnk(string PuthExe, string PuthIco, string NameLnk, string Parametr, string PuthWork)
        {

            //путь к ярлыку
            string shortcutPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + NameLnk; // @"\ConectoWorkSpace.lnk";
 
                // Сздание ярлыка для запуска ConectoWorkSpace
                WshShell shell = new WshShell();

                //создаем объект ярлыка
                IWshShortcut shortcut = (IWshShortcut)shell.CreateShortcut(shortcutPath);
           
                //задаем свойства для ярлыка
                //описание ярлыка в всплывающей подсказке
                shortcut.Description = "Ярлык "+ NameLnk;
                shortcut.WorkingDirectory = PuthWork;
            
                //горячая клавиша
                shortcut.Hotkey = "Ctrl+Shift+C";
                //путь к самой программе
                shortcut.TargetPath = PuthExe; // @"c:\Program Files\Conecto\WorkSpace\D_ConectoWorkSpace_bin_Release_\bin\Conecto® WorkSpace.exe"; //Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles) + @"\Conecto\WorkSpace\D_ConectoWorkSpace_bin_Release_\bin\Conecto® WorkSpace.exe";
                // Путь к иконке
                shortcut.IconLocation = PuthIco; // @"c:\Program Files\Conecto\WorkSpace\D_ConectoWorkSpace_bin_Release_\bin\Conecto® WorkSpace.exe";
                shortcut.Arguments = Parametr; // " -t -0 -delDesktop";
           
                //Сохраняем параметры и создаем ярлык
                shortcut.Save();

 

        }


        //public static void RestartConectoLnk(object ThreadObj)
        //{
        //    string shortcutPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + @"\ConectoWorkSpace.lnk";
        //    Process mv_prcInstaller = new Process();
        //    mv_prcInstaller.StartInfo.FileName = shortcutPath;
        //    mv_prcInstaller.StartInfo.Arguments = "";
        //    mv_prcInstaller.Start();
        //    Thread.Sleep(5000);
        //    // mv_prcInstaller.WaitForExit();
        //     //MessageBox.Show("Выполнился запуск" );
        //    //mv_prcInstaller.Close();
        //}

        //protected override void OnStartup(StartupEventArgs e)
        //{
        //    foreach (string arg in e.Args)
        //    {
        //        // TODO: whatever
        //    }
        //    base.OnStartup(e);
        //}

        void App_Startup(object sender, StartupEventArgs args)
        {
            // MessageBox.Show("Продолжение", "Сообщение", MessageBoxButton.OK, MessageBoxImage.Stop);



            // Проверка файлов для ОС Windows TaskBar (не в ядре exe)
            //if (!File_(DllDataTaskBar, 5))
            //{
            // Запрещаем выход из функции

            // }
            

            // Запуск приложения с рабочим столом.
            AppStart oSystemConecto = new AppStart();       //new SystemConecto();
            oSystemConecto.Start_(args);


            //if (App.aSystemVirable["type_app"] == "-p")
            //{
            //    var DllDataTaskBar = AppStart.PutchApp + @"bin\dll\Hardcodet.Wpf.TaskbarNotification.dll";
            //    AppStart.AppStartIsFilesPack(DllDataTaskBar, -1);


            //    try
            //    {
            //        var myDll = DllWork.LoadWin32Library(DllDataTaskBar);
            //        // Здесь вы можете добавлять, dll проверку версии

            //        // Параметры запуска
            //        switch (App.aSystemVirable["type_app"])
            //        {
            //            case "-p":
            //                // Вариант с помощью ресурса
                           
            //                App.notifyIcon = (Hardcodet.Wpf.TaskbarNotification.TaskbarIcon)Application.Current.FindResource("NotifyIcon");
                            
            //                //Вариант как объект
            //                //notifyIcon = new Hardcodet.Wpf.TaskbarNotification.TaskbarIcon();
            //                //notifyIcon.Icon = Resources. "/Conecto® WorkSpace;component/Images/ICO/Conecto Workspace.ico";
            //                //notifyIcon.Icon = Resources.Led;
            //                //notifyIcon.ToolTipText = "Left-click to open popup";
            //                //notifyIcon.Visibility = Visibility.Visible;


            //                // Вариант события по клику
                           
            //                App.notifyIcon.TrayPopup = new FancyPopup();
                            
            //                break;
            //        }
            //    }
            //    catch (ApplicationException exc)
            //    {
            //        MessageBox.Show(exc.Message, "There was an error during dll loading");
            //        throw exc;
            //    }

            //}






            /* Отключение настройки {StartupUri="MainWindow.xaml"}
             * <Application x:Class="ConectoWorkSpace.App"
             xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
             xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
             StartupUri="MainWindow.xaml"
             Startup="App_Startup"
             Exit="App_Exit"> 
             */
        }

        /// <summary>
        /// Выход из программы аварийно, из ОС принудительно
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void App_Exit(object sender, ExitEventArgs e)
        {
            // ОТключения событий системы
            SystemEvents.DisplaySettingsChanged -= new EventHandler(AppStart.SystemEvents_DisplaySettingsChanged);
            SystemEvents.TimeChanged -= new EventHandler(AppStart.SystemEvents_TimeChanged);



            

            AppStart.EndWorkPC(); // Это работает
            //MessageBox.Show("Финиш", "Сообщение", MessageBoxButton.OK, MessageBoxImage.Stop);


        }
        // Писать
        //C:\Users\ADM-o.zhakulin\Documents\Conecto\Export

        // <summary>
        /// Блок оброботки глобального исключения ошибок в коде или Аварийное завершение программы
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void App_DispatcherUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e)
        {
            Encoding win1251 = Encoding.GetEncoding("windows-1251");

            string MessageErr = string.Format("Необработаное исключение в системе {0}.{1} Система завершила работу",
                 e.Exception.ToString(), Environment.NewLine);
            string PuthSysLog = AppDomain.CurrentDomain.BaseDirectory + "systems.log";
            // запись в лог код; время; пользователь; тип сообщения; сообщения
            string HeadLog = String.Format("Id; {0}; {1}; {2}; {3}; {4}", "Date", "Name User", "Domen User", "Type message", "Message");
            int TypeError = 0;
            // Версия
            string Verciya = " Version install Framework " + VFrameWork; //"Не установлена версия!";
                                                                         //string subkey = @"SOFTWARE\Microsoft\NET Framework Setup\NDP\v4\Full\";
                                                                         //using (var ndpKey = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Registry32).OpenSubKey(subkey))
                                                                         //{
                                                                         //    if (ndpKey != null && ndpKey.GetValue("Release") != null) Verciya = "Version Framework" + (string)ndpKey.GetValue("Release");
                                                                         //}
            string text = Environment.NewLine, textall = "";
            switch (TypeError)
            {
                case 0:
                case 2:
                    text = text + "101;";
                    break;
                case 1:
                    text = text + "103;";
                    break;
                case 3:
                    text = text + "104;";
                    break;

            }
            // Здесь очень опасно вставлять не опереденное время!!! Синхронизировать но как?
            DateTime dateTime = DateTime.Now;
            text = string.Format(text + " {0}; {1}; {2}; --- ", dateTime.ToString("dd.MM.yyyy HH:mm:ss"), "NoName", "NoDomen");
            switch (TypeError)
            {
                case 0:
                case 2:
                    text = text + "Error" + ": ;";
                    break;
                case 1:
                    text = text + "Debug" + ": ;";
                    break;
                case 3:
                    text = text + "Information" + ": ;";
                    break;
            }
            text = text + MessageErr;

            // Проверка структуры
            if (System.IO.File.Exists(PuthSysLog))
            {
                textall = Environment.NewLine + Verciya + Environment.NewLine + text;
                // Проверка размера файла
                System.IO.FileInfo file = new System.IO.FileInfo(PuthSysLog);
                if (file.Length > 1048576)
                {
                    // очитсить содержимое
                    System.IO.File.WriteAllText(PuthSysLog, "");
                    textall = HeadLog + Environment.NewLine + Verciya + Environment.NewLine + text;
                }
            }
            else textall = HeadLog + Environment.NewLine + Verciya + Environment.NewLine + text;

            // Отслеживание ошибок в досупе к файлу лога в многопотоковой среде 
            try
            {
                using (StreamWriter FileSysLog = new StreamWriter(PuthSysLog, true, win1251))
                {
                    FileSysLog.WriteLine(textall);
                    FileSysLog.Close();
                }
            }
            catch //(Exception ex)
            {
                // Отследить ошибки
                // 1. Запись в БД локальную или центральную
                // Пробуем записать в локальный лог 
                string LocalLog = AppDomain.CurrentDomain.BaseDirectory + "local_sys.log";
                textall = (!(System.IO.File.Exists(LocalLog)) ? HeadLog + Environment.NewLine : "") + text;
                try
                {
                    using (StreamWriter FileSysLog = new StreamWriter(LocalLog, true, win1251))
                    {
                        FileSysLog.WriteLine(textall);
                        FileSysLog.Close();
                    }
                    // Лог сохранен

                }
                catch { } //(Exception ex){}

            }
            // Вывод сообщения на экран.
            System.Windows.MessageBox.Show(MessageErr, "", System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Stop);

            // запуск CheckApp (проверка и восстановление ошибок )
            // Предотвратить дефолт обработки необработанных исключений
            // e.Handled = true; // Установить если ошибка не критичная для всей программы
            e.Handled = false;
        }

        /// <summary>
        /// Событие когда завершается сеанс Windows (Выход, Завершение работы, Перезапуск, Спящий режим)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void App_SessionEnding(object sender, SessionEndingCancelEventArgs e)
        {
            // Ask the user if they want to allow the session to end
            string msg = string.Format("{0}. End session?", e.ReasonSessionEnding);
            MessageBoxResult result = MessageBox.Show(msg, "Завершение сессии", MessageBoxButton.YesNo); //Session Ending

            bool AskUser = true;

            if (result == MessageBoxResult.No)
            {
               AskUser = false;
            }
            SystemConecto.ErorDebag(string.Format("{0}. Пользователь ответил: {1}", msg, AskUser ? "Да" : "Нет"));

            // End session, if specified
            if(!AskUser)
                e.Cancel = true;
        }

        //protected override void OnStartup(StartupEventArgs args)
        //{
        //    base.OnStartup(args);
        //    string[] args_ = new String[1];
        //    if (args.Args.Length > 1)
        //    {
        //        args_ = new string[args.Args.Length];
        //    }
        //    for (int i = 0; i != args.Args.Length; ++i)
        //    {
        //        args_[i] = args.Args[i];
        //    }


        //    //SystemConecto oSystemConecto = new SystemConecto();
        //    //oSystemConecto.Start_(args_);
        //    // SystemConecto.Start_(e);

        //}

        //private bool _contentLoaded;

        ///// <summary>
        ///// InitializeComponent
        ///// </summary>
        //[System.Diagnostics.DebuggerNonUserCodeAttribute()]
        //public void InitializeComponent()
        //{

        //    #line 4 "..\..\..\App.xaml"
        //    this.Startup += new System.Windows.StartupEventHandler(this.App_Startup);

        //    #line default
        //    #line hidden

        //    #line 5 "..\..\..\App.xaml"
        //    this.Exit += new System.Windows.ExitEventHandler(this.App_Exit);

        //    #line default
        //    #line hidden

        //    #line 6 "..\..\..\App.xaml"
        //    this.DispatcherUnhandledException += new System.Windows.Threading.DispatcherUnhandledExceptionEventHandler(this.App_DispatcherUnhandledException);

        //    #line default
        //    #line hidden
        //    if (_contentLoaded)
        //    {
        //        return;
        //    }
        //    _contentLoaded = true;
        //    System.Uri resourceLocater = new System.Uri("/Conecto® WorkSpace;component/start.xaml", System.UriKind.Relative);

        //    #line 1 "..\..\..\App.xaml"
        //    System.Windows.Application.LoadComponent(this, resourceLocater);

        //    #line default
        //    #line hidden
        //}

        //[System.Diagnostics.DebuggerNonUserCodeAttribute()]
        //[System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        //[System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        //[System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        //[System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        //void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target)
        //{
        //    this._contentLoaded = true;
        //}

        /// <summary>
        /// Запрет запуска нескольких копий приложения
        /// </summary>
        /// <returns></returns>
        public static Process RunningInstance()
        {
            try
            {


                //var test2 = Win32.ListProcess();
                // Получаем SID Пользователя
                Process current = Process.GetCurrentProcess();
                //Thread.Sleep(200);
                string[] UserInfo = Win32.DumpUserInfo(current.Handle);

                AppStart.USERSID = UserInfo[6];
                AppStart.NAMEUSERSID = UserInfo[4];
                AppStart.DOMENUSERSID = UserInfo[2];

                Dictionary<int, string[]> InfoSID = Win32.ListProcess(current.ProcessName);

                foreach (var processInfo in InfoSID)
                {
                    if (processInfo.Key != current.Id && processInfo.Value[9] == AppStart.USERSID)
                    {
                        // Активировать существующие
                        //SystemConecto.StruErrorDebag Err_ = new SystemConecto.StruErrorDebag();
                        //Err_.NameUSERSID = AppStart.NAMEUSERSID;
                        //Err_.DomenUSERSID = AppStart.DOMENUSERSID;
                        //SystemConecto.ErorDebag(processInfo.Key.ToString() + " " + processInfo.Value[0], 0, 0, Err_); 
                        TextPasteWindow.ShowWindow(Process.GetProcessById(Convert.ToInt32(processInfo.Key)).MainWindowHandle, 1);
                        return Process.GetCurrentProcess();
                    }
                }
            }
            catch
            {

            }

            return null;

            #region
            //var getAllExploreProcess = Process.GetProcesses(); //.Where(r => r.ProcessName.Contains(NameProcess)); //"EXPLORE"
            //uint PROCESS_TERMINATE = 0x1;
            // Отладка
            // SystemConecto.ErorDebag("0", 1);
            // Завершить все процессы с именем NameProcess
            // var fht = "";
            //foreach (Process process in getAllExploreProcess)
            //{
            //    fht  = process.ProcessName;
            //}

           // var d = fht;
            //Process current = Process.GetCurrentProcess();
            //Process[] processes = Process.GetProcessesByName(current.ProcessName);

            //Просматриваем все процессы на совпадения с текущим процессом
            // return processes.Where(process => process.Id != current.Id).FirstOrDefault(process => System.Reflection.Assembly.GetExecutingAssembly().Location.Replace("/", "\\") == current.MainModule.FileName);
            #endregion
        }

        #region Разработка обнаружения вотрого запуска



        //Process p = Process.GetCurrentProcess();
        //string assemblyName = p.ProcessName + ".exe";
        //ErorDebag("================== Наше Имя: " + assemblyName , 1);

         //if (Process.GetProcesses().Where(p => p.ProcessName.Contains("NameOP")).Count() != 0)
         //   {
               
         //   }

        // Проверка всех работающих процессов
         //   Process[] allProc = Process.GetProcesses();
         //   foreach (Process currProc in allProc)
         //   {

               
         //       if (currProc.ProcessName == NameOP)
         //       {
         //          // ErorDebag("================== Нашел: " + currProc.ProcessName, 1);
         //           // Проверка второго запуска
         //           StartApp = StartApp ? false : true;
         //       }
         //       // Среда разработки
         //       if (currProc.ProcessName == NameOP + ".vshost")
         //       {
         //           //NameOP = NameOP + ".vshost";
         //           StartApp = true;
         //           break;
         //       }
         //       // ErorDebag(currProc.ProcessName, 1);
         //   }
            // Запрос на работающий процесс
            //Process[] allProc = Process.GetProcessesByName("notepad");
            //if (allProc != null && allProc.Length >= 1)
            //{
            //    // Проверить все одинаковые процессы

            //}
        #endregion

    }



    // SystemConecto : Form partial SystemConecto
    public partial class AppStart
    {

        /// <summary>
        /// Параметры приложений AppStory в ConectoWorkSpace
        /// </summary>
        public static SystemConfigControll SysConf = null;
        /// <summary>
        /// Идентификатор исполняемого приложения (исключения допустимы)
        /// </summary>
        public static string IDDir;        
        /// <summary>
        /// Путь к файлам приложения
        /// </summary>
        public static string PutchApp = "";
        /// <summary>
        /// КОД SID пользователя запустившего приложение
        /// </summary>
        public static string USERSID = "";
        /// <summary>
        /// Имя пользователя запустившего приложение
        /// </summary>
        public static string NAMEUSERSID = "";
        /// <summary>
        /// Название домена пользователя запустившего приложение
        /// </summary>
        public static string DOMENUSERSID = "";
        /// <summary>
        /// Код ошибки при старте
        /// </summary>
        public static string STOPID = "0";
        /// <summary>
        /// Аварийный выход
        /// </summary>
        public static bool STOP = false;
        /// <summary>
        /// Стартовый путь приложения [Application.StartupPath]
        /// В WPF меняем на String appStartPath = System.IO.Path.GetDirectoryName(Process.GetCurrentProcess().MainModule.FileName);
        /// System.IO.Directory.GetCurrentDirectory()
        /// </summary>
        public static string PStartup = AppDomain.CurrentDomain.BaseDirectory;

        /// <summary>
        /// Режим компиляции приложения Alfa, Beta, Release
        /// </summary>
        public static string ReleaseCandidate = "Release";
        /// <summary>
        /// Параметры приложения по умочанию использовать для изменения параметров на лету
        /// </summary>
        public static Dictionary<string, string> aParamApp = new Dictionary<string, string>();
        /// <summary>
        /// Режим отладки в приложения
        /// </summary>
        public static bool DebagApp = true;
        /// <summary>
        /// Базовый клас симетричного ширования
        /// </summary>
        private static SymmetricAlgorithm des = null;

        public static FileStream XMLConfigFile = null; // Блокировка основного файла кофигурации

        public static string xmlString = "";

        /// <summary>
        /// Старт потока интерфейса программы
        /// </summary>
        /// <param name="args"></param>
        /// <param name="oSystemConecto"></param>
        public void Start_(StartupEventArgs args)
        {
            // (string[] args, SystemConecto oSystemConecto)

            // Проверка директории хранения файлов настройки "c:\Program Files\Conecto\WorkSpace\[Название текущей директории - ключь лицензии]"
            // если совпадет то будет глюк чтения параметров (исправить уникальным ключем лицензирования уникальным кодом) по умолчанию [демо] 
            // Хранит файлы пока на диске C: - исправить, при старте проверка всех дисков.
            // Приложение по умолчанию без своей директории, переносимое. 
            // 

            // Программа может сама проверить запущена ли она с правами администратора: (это большой глюк в системе - ругается на все строки кода которым нужны права администратора)
            // string s = new WindowsPrincipal(WindowsIdentity.GetCurrent()).IsInRole(WindowsBuiltInRole.Administrator) ? "Администратор" : "Обычный пользователь";



            // -1.  (3.) Проверка целосности каталогов и файлов программы
            /// Заметки разделяем строку пути старта приложения 
            string[] aDir = PStartup.Split(System.IO.Path.DirectorySeparatorChar); //'\\'
            // Количество директорий в названии Program Files директории
            int CountDirectoryName = 4;
            // Формирование системного пути в Program Files
            for (int i = aDir.Length - 2; i > 0; i--)
            {
                CountDirectoryName = CountDirectoryName - 1;
                if(CountDirectoryName==0)
                            break;
                
                IDDir = string.Format("{0}_{1}", aDir[i], IDDir);   //Combine(sourcePath, PutchFIsPack.NameFile
            }
            IDDir = string.Format("{0}_{1}", aDir[0].Split(':')[0], IDDir);

            // Права в ОС Win и Новое приложения для клиента без Админ прав режим us
            if (App.UserNoRoot == 0 ){

                PutchApp = PStartup;
            }
            else
            {
                PutchApp = @"c:\Program Files\Conecto\" + App.aSystemVirable["NameTh"] + @"\" + IDDir ;
            }

 

            // Проверка в Виндовс длина пути 170
            bool NoLenDirectory = false;
            if (PutchApp.Length > 170)
            {
                NoLenDirectory = true;
                STOPID = "Length Program Files Directory";
            }

            //if( SystemConecto.OSWMI != 3)
            // Русский текст можно заменить другой локалью Разработка
            MessageListener.Instance.ReceiveMessage(string.Format("{0}: {1}", "Выполняется", "проверка каталогов"));

                // Отладка
                //string[] args_ = Environment.GetCommandLineArgs();
                //MessageListener.Instance.ReceiveMessage(string.Format("{0}: {1}", "", string.Join(" ", args_)));
                //MessageListener.Instance.ReceiveMessage(string.Format("{0}: {1}", "Отладка значение переменной", App.aSystemVirable["parametrs_app"]));


                //System.Threading.Thread.Sleep(4000);

                // if (!SystemConecto.DIR_(SystemConecto.PutchApp + "bin")) SystemConecto.STOP = true;
                // if (!SystemConecto.DIR_(SystemConecto.PutchApp + @"bin\dll")) SystemConecto.STOP = true;
                // if (!SystemConecto.DIR_(SystemConecto.PutchApp + "tmp")) SystemConecto.STOP = true;

                // Сделать возможность переключения прав доступа с админа на пользовательские !!Важно!!
                if (App.aSystemVirable["UserWindowIdentity"] == "1" && ( NoLenDirectory || !DIR_(PutchApp) || !DIR_(PutchApp + @"\bin\dll") || !DIR_(PutchApp + @"\tmp")) )
                {
                    STOP = true;
                    STOPID = "Тo Dir";
                    // Локализация приложения (Мультиязычноcть) старт локали
                    // SystemConectoLanguage.LanguageLoad(ConectoWorkSpace.Properties.Resources.culture_ru_ru_csv,- 1);
                }
                else
                {
                    #region Продолжение проверки мы можем работать в системном каталоге
                    PutchApp = PutchApp + @"\";

                    try{

                        #region Шифрование данных ключи (01)
                        //http://msdn.microsoft.com/ru-ru/library/bb397867%28v=vs.100%29.aspx
                        SetProvider();
                        #endregion


                        #region Системная конфгурация config.xml

                        SysConf = new SystemConfigControll();

                        // Потихоньку убираем в сторону system.fdb метод ильзуем только для внешних приложений, доступ только пользователю с админ првами.
                        //if (App.aSystemVirable["UserWindowIdentity"] == "1")
                        //{
                            // Чтение параметров приложения
                            if (File_(PutchApp + "config.xml", 4))
                            {
                                // Параметры-Администратора- считать в память
                                SysConf.CreateConfigXML(1);
                                // Проверка целосности и правильности и Чтение
                                if (!SysConf.ReadConfigXML())
                                {
                                    // Перезапуск если разрушена целосность конфигурационного файла
                                    if (!SysConf.ReadConfigXML())
                                    {
                                        STOP = true;
                                    }
                                }
                                // Параметры-Пользователя (отсутствуют по умолчанию)
                            }
                            else
                            {
                                // Создание и чтение
                                if (File_(PutchApp + "config.xml", 5))
                                {
                                    // Параметры-Администратора
                                    SysConf.CreateConfigXML();
                                    // Чтение
                                    if (!SysConf.ReadConfigXML(1))
                                    {
                                        STOP = true;
                                    }
                                    // Параметры-Пользователя (отсутствуют по умолчанию)
                                }
                            }
                    // }
                    #endregion

                    //System.Windows.MessageBox.Show("Шаг 9 " + (STOP ? "true" : "false") + "/", "", System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Stop);
                    // System.IO.Path.GetDirectoryName(Process.GetCurrentProcess().MainModule.FileName)
                    var DllDataTaskBar = PutchApp + @"bin\dll\Hardcodet.Wpf.TaskbarNotification.dll";
                    if (App.aSystemVirable["type_app"] == "-p")
                    {

                        // Проверка файлов для ОС Windows TaskBar (не в ядре exe)
                        if (!File_(DllDataTaskBar, 5))
                        {
                            // Запрещаем выход из функции
                            AppStartIsFilesPack(DllDataTaskBar, -1);
                        }
                    }

                    var DllKernel = PutchApp + @"bin\dll\SystemConecto.dll";
                    // Устанавливаем или проверяем ядро
                    // Проверка файлов Сервера БД
                    if (!File_(DllKernel, 5))
                    {
                        // Запрещаем выход из функции
                        AppStartIsFilesPack(DllKernel, -1);
                    }
                    var DllDataB = PutchApp + @"bin\dll\DBConecto.dll";
                    // Проверка файлов Сервера БД
                    if (!File_(DllDataB, 5))
                    {
                        // Запрещаем выход из функции
                        AppStartIsFilesPack(DllDataB, -1);
                    }
                       




                        // Устанавливаем или проверяем ядро
                        if (File_(DllKernel, 5) && File_(DllDataB, 5) && (App.aSystemVirable["type_app"] != "-p" || (App.aSystemVirable["type_app"] == "-p" && File_(DllDataTaskBar, 5))))
                        {
                            #region Подключение дополнительного каталога для dll

                            // Добавить dll - путь к директории для DllImport и прочих функций, подключает DllWork метод Main не сработал.
                            SetDllDirectory(PutchApp + @"bin\dll");



                            // Работает только если нужно ввзять метод, не работает в сборках
                            // Assembly load = Assembly.LoadFrom(DllKernel);

                            /// Если же вы хотите использовать общую управляемую библиотеку, то специальный механизм, называемый GAC (Global Assembly Cache), используя механизмы криптографии, 
                            /// позаботится об отсутствии дублирования, о том, что нужная вам библиотека — именно та и именно той версии, которую вы ждёте.
                            AppDomain currentDomain = AppDomain.CurrentDomain;
                            currentDomain.AssemblyResolve += new ResolveEventHandler(MyResolveEventHandler);

                            #region Описание и расположение файла gacutil.exe

                            // Чтобы создать сборку со строгим именем, смотри предыдущий пост. Теперь перейдём к GAC. Global Assembly Cache (глобальный кэш сборок) - место, где располагаются совместно используемые сборки. Его можно найти по адресу: C:\Windows\assembly. Но этот кэш только для сборок .NET Framework 2.0 - 3.5. Для .NET Framework 4.0 GAC - C:\Windows\Microsoft.NET\assembly. Это изменение произошло в основном из-за того, чтобы приложения, написанные под CLR v2.0 не видели сборки в кэше, написанные для CLR v4.0.
                            // Т.е GAC способна хранить сборки разных версий, то если они для одного и того же CLR (чтобы не "ломать" старые приложения).
                            // Каталог обладает особой структурой. Имена вложенных каталогов генерируются по особому алгоритму. Ни в коем случае не стоит копировать файлы сборок в GAC вручную, вместо этого необходимо пользоваться, например, GACUtil.exe или Windows Installer (MSI). Сборку с нестрогим именем в GAC поместить нельзя!  


                            // C:\Program Files\Microsoft SDKs\Windows\v7.0A\bin\gacutil.exe
                            // C:\Program Files\Microsoft SDKs\Windows\v7.0A\bin\NETFX 4.0 Tools\gacutil.exe

                            // Утилита Gacutil.exe позволяет просматривать и управлять содержимым глобальной сборки кеша.
                            //  Находится в 2008 студии в C:\Program Files\Microsoft SDKs\Windows\v6.0A\bin.
                            //  В 2005 студии в C:\Program Files\Microsoft Visual Studio 8\SDK\v2.0\Bin

                            //  gacutil /l имя_сборки - отображает только сборки, что соответствуют указанному названию.

                            //  Команда find /i "строка" - выполняет поиск текстовой строки в одном или нескольких файлах без учета регистра символов.

                            //  Разделитель | работает как связка двух команд, а именно результат команды find передается в имя_сборки.
                            //  Разделитель & используется для перечисления команд в пакетных файлах *.bat.

                            //  http://msdn.microsoft.com/en-us/library/aa560649(v=bts.20).aspx

                            //  gacutil.exe /if "<path to the assembly .dll file>"

                            //<system.data>
                            //  <DbProviderFactories>
                            //  <add name="FirebirdClient Data Provider" invariant="FirebirdSql.Data.FirebirdClient" description=".Net Framework Data Provider for Firebird" type="FirebirdSql.Data.FirebirdClient.FirebirdClientFactory, FirebirdSql.Data.FirebirdClient, Version=2.7.0.0, Culture=neutral, PublicKeyToken=3750abcc3150b00c"/>
                            //</system.data>

                            //<configuration>
                            //<configSections>
                            // <section name="firebirdsql.data.firebirdclient" type="System.Data.Common.DbProviderConfigurationHandler, System.Data, Version=2.7.0.0, Culture=neutral, PublicKeyToken=3750abcc3150b00c"/>



                            #endregion

                            #region Расположение файла machine.config GAC cache
                            // C:\Windows\Microsoft.NET\Framework\v4.0.30319\Config\machine.config
                            // C:\Windows\Microsoft.NET\Framework\v2.0.50727\CONFIG\machine.config
                            #endregion

                            // Проверка зборки Hardcodet - панель иконка на SYSTEM BAr Windows, допускаю, что может быть установлена стара версия
                            #region ChekGACHardcodet.Wpf
                            try
                            {
                                // Проверка зборки SystemConecto.dll, допускаю, что может быть установлена стара версия
                                if (GACGet_Hardcodet()) { }


                            }
                            catch (Exception) { }// ex
                            #endregion

                            // Проверка зборки SystemConecto, допускаю, что может быть установлена стара версия
                            #region ChekGACSystemConecto

                            try
                            {
                                    // Проверка зборки SystemConecto.dll, допускаю, что может быть установлена стара версия
                                    if (GACGet_SystemConecto()) { }
                                    // Выключить сборку
                                    //Publish publisher = new Publish();
                                    //publisher.GacRemove(SystemConecto.PutchApp + @"bin\dll\FirebirdSql.Data.FirebirdClient.dll");

                                } catch (Exception) { }// ex
                                #endregion

                            // Проверка зборки DBConecto, допускаю, что может быть установлена стара версия
                            #region ChekGACDBConecto
                            try
                            {
                                // Проверка зборки SystemConecto.dll, допускаю, что может быть установлена стара версия
                                if (GACGet_DBConecto()) { }
                                // Выключить сборку
                                //Publish publisher = new Publish();
                                //publisher.GacRemove(SystemConecto.PutchApp + @"bin\dll\FirebirdSql.Data.FirebirdClient.dll");

                            }
                            catch (Exception) { }// ex
                            #endregion


                            #endregion

                            // Предзапускная подготовка к старту
                            WorkApp_(args);


                        }
                        else
                        {
                            // Отсутствует доступ к SystemConecto
                            STOP = true;
                            STOPID = "SystemConecto or DBConecto - no file";
                        }

                        #endregion

                    }catch (Exception)
                    {
                        //STOP = true;
                        //STOPID = "SystemConecto or DBConecto - no file";
                    }


            }
            // Завершение программы
            // Завершение старта приложения или аварийный выход
            if (STOP)
            {
                // -------------------------------- Вывести сообщения о прекращении выполнения приложения!
                System.Windows.MessageBox.Show("Приложение завершено.\nНарушена процедура безопасности - STOP\nIDError: " + STOPID, "", System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Stop);

                // Аварийный выход  - данный вариант не работает
                // Application.Exit();
                Environment.Exit(0); // Это работает
            }

            // Конец выполнения процедуры запуска
            MessageListener.Instance.ReceiveMessage(string.Format("{0}: {1}", "Приложение готово к работе", ""));




            if (App.aSystemVirable["type_app"] == "-p")
            {
                //var DllDataTaskBar = AppStart.PutchApp + @"bin\dll\Hardcodet.Wpf.TaskbarNotification.dll";
               // AppStart.AppStartIsFilesPack(DllDataTaskBar, -1);


                try
                {
                    //var myDll = DllWork.LoadWin32Library(DllDataTaskBar);
                    // Здесь вы можете добавлять, dll проверку версии

                    // Параметры запуска
                    switch (App.aSystemVirable["type_app"])
                    {
                        case "-p":
                            // Вариант с помощью ресурса

                            //App.notifyIcon = (Hardcodet.Wpf.TaskbarNotification.TaskbarIcon)Application.Current.FindResource("NotifyIcon");

                            //Вариант как объект
                            //App.notifyIcon = new Hardcodet.Wpf.TaskbarNotification.TaskbarIcon()
                            //{
                            //    Icon = ConectoWorkSpace.Properties.Resources.NotifyIcon
                            //}       
                            //notifyIcon.Icon = Resources. "/Conecto® WorkSpace;component/Images/ICO/Conecto Workspace.ico";
                            //notifyIcon.Icon = Resources.Led;
                            //notifyIcon.ToolTipText = "Left-click to open popup";
                            //notifyIcon.Visibility = Visibility.Visible;


                            // Вариант события по клику

                            //App.notifyIcon.TrayPopup = new FancyPopup();
                            //SystemConecto.IsNotifyIconWPF(new FancyPopup());

                            //SystemConecto.IsNotifyIconWPF();

                            //SystemConecto.notifyIcon.TrayPopup = new FancyPopup();

                            // 7. закрыть заставку
                            Splasher_startWindow.Splasher.CloseSplash();

                            break;
                    }
                }
                catch (ApplicationException exc)
                {
                    MessageBox.Show(exc.Message, "There was an error during dll loading");
                    throw exc;
                }

            }



        }

        /// <summary>
        /// Запуск программы
        /// </summary>
        /// <param name="args"></param>
        /// <param name="oSystemConecto"></param>
        public void WorkApp_(StartupEventArgs args)
        {


                // Перенос переменных с AppStart в ядро
                // Определение значения переменных
                SystemConecto.StartApp = true;
                SystemConecto.WindowPanelSys_s = null;
                SystemConecto.IDDir = IDDir;
                SystemConecto.PStartup = PStartup;
                SystemConecto.PutchApp = PutchApp;
                SystemConecto.ReleaseCandidate = ReleaseCandidate;
                SystemConecto.aParamApp = aParamApp;
                SystemConecto.DebagApp = DebagApp;

                #region Шифрование данных ключи для внешних приложений (01)
                //http://msdn.microsoft.com/ru-ru/library/bb397867%28v=vs.100%29.aspx
                SystemConecto.SetProvider();
            #endregion



                if (App.aSystemVirable["UserWindowIdentity"] == "1")
                {
                    if (!SystemConecto.DIR_(SystemConecto.PutchApp + "config")) SystemConecto.STOP = true; STOPID = "Dir ..config";
                    if (!SystemConecto.DIR_(SystemConecto.PutchApp + @"config\user")) SystemConecto.STOP = true;
                }
                SystemConecto.PuthSysLog = SystemConecto.PutchApp + "systems.log";

  

                // Локализация приложения (Мультиязычноcть) старт локали
                SystemConectoLanguage.LanguageLoad(ConectoWorkSpace.Properties.Resources.culture_ru_ru_csv);

                // Версия ОС                    
                SystemConecto.OSWMI = SystemConecto.WMIOS();

                
                // Директории серверов
                // Права в ОС Win
                if (App.aSystemVirable["UserWindowIdentity"] == "1")
                {

                    // Продолжить проверку

                    if (!SystemConecto.DIR_(SystemConecto.PutchApp + "data")) SystemConecto.STOP = true;
                    if (!SystemConecto.DIR_(SystemConecto.PutchApp + @"data\intl")) SystemConecto.STOP = true;


                    if (!SystemConecto.DIR_(SystemConecto.PutchApp + "licenses")) SystemConecto.STOP = true;
                    if (!SystemConecto.DIR_(SystemConecto.PutchApp + "images")) SystemConecto.STOP = true;
                    if (!SystemConecto.DIR_(SystemConecto.PutchApp + "Multimedia")) SystemConecto.STOP = true;
                    if (!SystemConecto.DIR_(SystemConecto.PutchApp + @"Multimedia\video")) SystemConecto.STOP = true;
                    if (!SystemConecto.DIR_(SystemConecto.PutchApp + @"Multimedia\pdf")) SystemConecto.STOP = true;

                    if (SystemConecto.OS64Bit)
                    {
                        if (!SystemConecto.DIR_(SystemConecto.PutchApp + @"bin\dll\x64")) SystemConecto.STOP = true;
                        if (!SystemConecto.DIR_(SystemConecto.PutchApp + @"data\x64")) SystemConecto.STOP = true;
                    }

                    SystemConectoServers.DirServer();

                    // Репозитарий клиента
                    if (!SystemConecto.DIR_(SystemConecto.PutchApp + @"Repository")) SystemConecto.STOP = true;

                }



                // Чтение конфигурации
                //if (SystemConecto.OSWMI != 3)
                MessageListener.Instance.ReceiveMessage(string.Format("{0}: {1}", Language.PrLanguage["Выполняется"], "проверка конфигурации"));

                // Отключение отладки в релизе (Отладку можно включить с помощью настройки приложения)
                if (SystemConecto.ReleaseCandidate == "Release")
                {
                    //DebagApp = false;
                }

            //==========================================

            //#region Конфигурация приложений и функций ConectoWorkSpace appplay.xml
            //if (!Load_appplay()) { 
            //        SystemConecto.STOP = true; STOPID = "Load_appplay";
            //}
            //#endregion
 

            // 0. --- Проверка запуска приложения на компьюторе

            MessageListener.Instance.ReceiveMessage(string.Format("{0}: {1}", Language.PrLanguage["Выполняется"], "запуск приложения"));
                if (!SystemConecto.IsLocalStartProgram())
                {
                    SystemConecto.ErorDebag("Приложение завершено. Нарушено правило запуска программы. Запуск программы осуществляется только с локального накопителя.", 0, 2);
                    Environment.Exit(0); // Это работает
                }

            // 1. --- Проверка повторного запуска (два раза, двойная загрузка) (не работате - Mutex mtx = new Mutex(true, Application.ProductName, out StartApp);
            // Реализовывается иначе  ApplicationController однако заменил методом RunningInstance

            MessageListener.Instance.ReceiveMessage(string.Format("{0}: {1}", Language.PrLanguage["Выполняется"], "проверка повторного запуска"));

            // Какой пользователь вошел в ОС
            //User.Identity.Name.ToString()

            // Есть маленький глюк проверки )))
            // MessageBox.Show(AppDomain.CurrentDomain.BaseDirectory.ToUpper());
            // Project
            // !AppDomain.CurrentDomain.BaseDirectory.ToUpper().Contains("PROJECT") &&
            //&& args.Args[2] == null && (args.Args[2] !=null && args.Args[2] == "-delDesktop")1==2


                //var test2 = Win32.ListProcess();
                // Получаем SID Пользователя
                Process current = Process.GetCurrentProcess();
                //Thread.Sleep(200);
                string[] UserInfo = Win32.DumpUserInfo(current.Handle);

                AppStart.USERSID = UserInfo[6];
                AppStart.NAMEUSERSID = UserInfo[4];
                AppStart.DOMENUSERSID = UserInfo[2];

            Dictionary<int, string[]> InfoSID = Win32.ListProcess(current.ProcessName); //
                //OrderBy(x => x.ProcessName) // .Where(x => current.ProcessName) 
                // && p.Id != current.Id
                //Process.GetProcesses().Where(p => p.ProcessName == current.ProcessName ).ToList().ForEach(p =>
                //{
                //   // if (p.ProcessName == current.ProcessName  ) //&& p.Id != current.Id
                //   // {
                //        MessageBox.Show(p.ProcessName + " p.Threads.Count=" + p.Threads.Count + " Id=" + p.Id +" N" );
                //    string[] UserInfoOSer = Win32.DumpUserInfo(p.Handle);
                //    //MessageBox.Show(UserInfoOSer.Count().ToString());
                //    // }

            //    //Console.WriteLine(
            //    //    p.ProcessName + " p.Threads.Count=" + p.Threads.Count + " Id=" + p.Id);
            //});

            MessageBox.Show(InfoSID.Count().ToString());

            foreach (var processInfo in InfoSID)
            {
                if (processInfo.Key != current.Id && processInfo.Value[9] == AppStart.USERSID)
                {
                    // Активировать существующие
                    //SystemConecto.StruErrorDebag Err_ = new SystemConecto.StruErrorDebag();
                    //Err_.NameUSERSID = AppStart.NAMEUSERSID;
                    //Err_.DomenUSERSID = AppStart.DOMENUSERSID;
                    //SystemConecto.ErorDebag(processInfo.Key.ToString() + " " + processInfo.Value[0], 0, 0, Err_); 
                    TextPasteWindow.ShowWindow(Process.GetProcessById(Convert.ToInt32(processInfo.Key)).MainWindowHandle, 1);
                    //return Process.GetCurrentProcess();
                    // 7. закрыть заставку
                    Splasher_startWindow.Splasher.CloseSplash();
                    SystemConecto.StartApp = false;
                    Environment.Exit(0); // Это работает
                }
            }



            //if (App.RunningInstance() != null)
            //{
            //    // 7. закрыть заставку
            //    Splasher_startWindow.Splasher.CloseSplash();
            //    SystemConecto.StartApp = false;
            //    Environment.Exit(0); // Это работает
            //}

            // Определить версию файла. exe dll
            //string file = PutchApp + @"bin\dll\dbconecto.dll";
            //string fileVersion = FileVersionInfo.GetVersionInfo(file).FileVersion;
            //string productVersion = FileVersionInfo.GetVersionInfo(file).ProductVersion;
            //MessageBox.Show(fileVersion + " // " + productVersion);


            // 2. --- Проверка реестра и Инсталяции программы
            //if (SystemConecto.OSWMI != 3)
            if (!SystemConecto.STOP || SystemConecto.StartApp)
             {
                    MessageListener.Instance.ReceiveMessage(string.Format("{0}: {1}", Language.PrLanguage["Выполняется"], "проверка реестра"));
                //SystemConecto.ErorDebag("Реестр отладка 222" + 
                //    Convert.ToString( rkAppSetingApp.GetValue("StartApp_" + App.NameOP) )
                //    , 0, 2);


                #region Реестр чтение и проверка Только для Линукс и IOS
                    //if (!AppStart.CHEKREG_())
                    //{
                        // Только Линукс и IOS
                        //SystemConecto.STOP = true;
                        //SystemConecto.ErorDebag("Приложение завершено. Нарушена процедура безопасности - REGWIN", 0, 2);
                        //Environment.Exit(0); // Это работает
                    //}
                    //else
                    //{
                        // Проверка первого старта
                        //SystemConecto.ErorDebag("Реестр отладка " + rkAppSetingApp.GetValue("StartApp_" + App.NameOP).ToString(), 0, 2);


                         rkAppSetingAllUser = Registry.CurrentUser.CreateSubKey(@"System\Alt-Tab\App\" + AppDomain.CurrentDomain.BaseDirectory);    // SystemConecto.PutchApp
                         rkAppSetingAllUser = Registry.CurrentUser.OpenSubKey(@"System\Alt-Tab\App\" + AppDomain.CurrentDomain.BaseDirectory, true); // SystemConecto.PutchApp

                        if (rkAppSetingAllUser.GetValue("StartApp_" + App.NameOP) == null)
                        {

                            // Программа устанавливается первый раз (как минимум, кроме случаев нарушение работы с реестром)
                            // Пользователь должен принять условия лицензионного соглашения об использувании ПО (приложения)
                            // Данное окно должно быть выполнено в стиле google Android. Должно отображатся информация о программе
                            // О техресурсах что программа использует, ссылка на сайт на лицензионное соглашение.
                            // Окно выступает также окном <О программе>.
                            // Основной элемент управления, я согласен с условиями. (Удаляет или создает в реестре Windows запись о принятии соглашения)

                            //Application.EnableVisualStyles();
                            //Application.Run(new Licenzija());

                            // Принятие соглашения об инсталяции без окна
                            DateTime dateTime = DateTime.Now;  // rkAppSetingApp
                            rkAppSetingAllUser.SetValue("StartApp_" + App.NameOP, dateTime.ToString("dd.MM.yyyy HH:mm:ss"));
                            rkAppSetingAllUser.Flush();

                            // REG QUERY "HKLM\SOFTWARE\Wow6432Node\Microsoft\Windows NT\CurrentVersion\Winlogon" / v Shell

                        }
                        else
                        {
                           // rkAppSetingAllUser = Registry.CurrentUser.OpenSubKey(@"System\Alt-Tab\App\" + AppDomain.CurrentDomain.BaseDirectory, true);
                        }


                if (rkAppSetingAllUser.GetValue("StartSystemFB") == null || (string)rkAppSetingAllUser.GetValue("StartSystemFB") == "")
                {
                    string DateStartConecto = (string)rkAppSetingAllUser.GetValue("StartApp_" + App.NameOP);
                    rkAppSetingAllUser.SetValue("StartSystemFB", DateStartConecto.Replace(" ", "").Substring(0, 15)); //DateTime.Now.ToString("yyMMddHHmm"
                    rkAppSetingAllUser.Flush();
                }
                if (rkAppSetingAllUser.GetValue("SerchDateTimeNet") == null || (string)rkAppSetingAllUser.GetValue("SerchDateTimeNet") == "")
                {
                    rkAppSetingAllUser.SetValue("SerchDateTimeNet", "0");
                    rkAppSetingAllUser.Flush();
                }

                string Versia = FileVersionInfo.GetVersionInfo(AppDomain.CurrentDomain.BaseDirectory + "Conecto® WorkSpace.exe").ToString();  // версия файла.
                Versia = Versia.Substring(Versia.IndexOf("FileVersion"), Versia.IndexOf("FileDescription") - Versia.IndexOf("FileVersion"));
                rkAppSetingAllUser.SetValue("VersiyaConecto", "Versiya " + Versia);
                rkAppSetingAllUser.Flush();

                if (rkAppSetingAllUser.GetValue("UpdateB52") == null || (string)rkAppSetingAllUser.GetValue("UpdateB52") == "")
                { 
                     AppStart.rkAppSetingAllUser.SetValue("UpdateB52", Administrator.AdminPanels.ShortDate);              
                }
 

                // Запись в HKEY_USERS
                //if (rkOpenSubKeyHkeyUsers.GetValue("StartSystemFB") == null || (string)rkOpenSubKeyHkeyUsers.GetValue("StartSystemFB") == "")
                //{
                //    string DateStartConecto = (string)rkOpenSubKeyHkeyUsers.GetValue("StartApp_" + App.NameOP);
                //    rkOpenSubKeyHkeyUsers.SetValue("StartSystemFB", DateStartConecto.Replace(" ", "").Substring(0, 15)); //DateTime.Now.ToString("yyMMddHHmm"
                //    rkOpenSubKeyHkeyUsers.Flush();
                //}
                //if (rkOpenSubKeyHkeyUsers.GetValue("SerchDateTimeNet") == null || (string)rkOpenSubKeyHkeyUsers.GetValue("SerchDateTimeNet") == "")
                //{
                //    rkOpenSubKeyHkeyUsers.SetValue("SerchDateTimeNet", "0");
                //    rkOpenSubKeyHkeyUsers.Flush();
                //}

                //rkOpenSubKeyHkeyUsers.SetValue("VersiyaConecto", "Versiya " + Versia);
                //rkOpenSubKeyHkeyUsers.Flush();


                //}
                #endregion
            }

            //Environment.Exit(0); // Это работает
            // Завершение старта приложения или аварийный выход
            if (STOP || SystemConecto.STOP)
            {
                // -------------------------------- Вывести сообщения о прекращении выполнения приложения!

                Splasher_startWindow.Splasher.CloseSplash();

                MessageBox.Show("Приложение завершено.\nНарушена процедура безопасности - STOP\nIDErrorReg: "+STOPID +"\n" +STOP + "|| " + SystemConecto.STOP, "", System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Stop);

                // Аварийный выход  - данный вариант не работает
                // Application.Exit();
                Environment.Exit(0); // Это работает
            }
            else
            {

                // Реакция на повторыный запуск приложения (пока не работает)
                if (SystemConecto.StartApp)
                {

                    // 3.
                    // Запись старта работы программы
                    SystemConecto.ErorDebag(Language.PrLanguage["Запуск приложения"], 3);

                    // Чтение параметров комндной строки для WPF
                    //if (SystemConecto.OSWMI != 3)
                    MessageListener.Instance.ReceiveMessage(string.Format("{0}: {1}", Language.PrLanguage["Выполняется"], "проверка параметров запуска"));
                    for (int i = 0; i != args.Args.Length; ++i)
                    {
                        SystemConecto.ErorDebag("приложение запущено с параметрами: " + args.Args[i], 1);
                    }
                    // Чтение параметров комндной строки для FORM
                    //foreach (string s in args)



                    //=========================================================================================
                    // Стартовая проверка необходимых компонентов в ОС для работы Приложения
                    //if (SystemConecto.OSWMI != 3)
                    MessageListener.Instance.ReceiveMessage(string.Format("Выполняется: {0}", "проверка компонентов в ОС для работы приложения"));

                    //if (IsCheckUpdateWorkSpace() > 0)
                    //{
                    //    SystemConecto.ErorDebag("Есть файл обновлений" + Environment.NewLine + "Осуществляется проверка версии" );
                    //    if (System.IO.File.Exists(SystemConecto.PutchApp + "UpdateConectoWorkSpace.txt"))
                    //    {

                    //        string[] fileLines = System.IO.File.ReadAllLines(SystemConecto.PutchApp + "UpdateConectoWorkSpace.txt");
                    //        foreach (string x in fileLines)
                    //        {
                    //            if (x.StartsWith((string)rkAppSetingAllUser.GetValue("VersiyaConecto"))) SystemConecto.ErorDebag("Обновлений нет.");
                    //        }
                    //        //Environment.Exit(0); // Это работает
                    //    }
                    //}

                    

                    if (IsCheckOS() > 0)
                    {
                        SystemConecto.ErorDebag("Приложение завершено.\nНарушена процедура безопасности, отсутствуют системные файлы - STOP", 2, 2);
                        Environment.Exit(0); // Это работает

                        // Оброботка результата
                        //MessageBox.Show("Есть такой шрифт", "Сообщение", MessageBoxButton.OK, MessageBoxImage.Stop); //, MessageBoxButtons.OK, MessageBoxIcon.Stop
                    }

                    
                    DevicePC.LoadDeviceUSB();


                    // ==== Смысл проверки Framework есть только при особых условиях, во время установки вероятность запуска этой процедуры равны ноль перенес выше.
                    #region
                    //MessageListener.Instance.ReceiveMessage(string.Format("Выполняется: {0}", "проверка Framework"));
                    //if (IsCheckFrameWork() > 0)
                    //{
                    //    SystemConecto.ErorDebag("Установлена устаревшая версия Framework" + Environment.NewLine + "Отсутствует инсталяционный  файл установки Framework" + Environment.NewLine + "  Установка прекращена. ", 2, 2);
                    //    Environment.Exit(0); // Это работает

                    //}
                    #endregion

                    // Проверка состояния служебной БД
                    MessageListener.Instance.ReceiveMessage(string.Format("Выполняется: {0}", "проверка SYS BD"));

                    if (IsCheckSYSBD() > 0)
                    {
                        SystemConecto.ErorDebag("Приложение завершено.\nНарушена процедура безопасности, отсутствуют системные файлы - STOP", 2, 2);
                        Environment.Exit(0); // Это работает

                    }
 
                    // Проверка состояния служебной БД
                    MessageListener.Instance.ReceiveMessage(string.Format("Выполняется: {0}", "проверка антивирусной блокировка BD"));
                    if (IsCheckUserBD() > 0)
                    {
                        SystemConecto.ErorDebag("Приложение завершено.\nНарушена процедура безопасности, отсутствуют системные файлы - STOP", 2, 2);
                        Environment.Exit(0); // Это работает

                    }

                    // Запуск приложения grdsrv.exe
                    MessageListener.Instance.ReceiveMessage(string.Format("Выполняется: {0}", "проверка служб"));
                    try
                    {
                        if (Convert.ToInt32(AppStart.TableReestr["BackOfServKeyOnOff"]) == 2)
                        {
                            Process[] FB25 = Process.GetProcessesByName("grdsrv");
                            if (FB25.Length == 0)
                            {

                                AppStart.RenderInfo Arguments01 = new AppStart.RenderInfo() { };
                                Arguments01.argument1 = "2";
                                Thread thStartTimer01 = new Thread(ConectoWorkSpace.InstallB52.InstallKeyServ);
                                thStartTimer01.SetApartmentState(ApartmentState.STA);
                                thStartTimer01.IsBackground = true; // Фоновый поток
                                thStartTimer01.Start(Arguments01);
                                ConectoWorkSpace.InstallB52.IntThreadStart++;
                                Thread.Sleep(2000);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        SystemConecto.ErorDebag("возникло исключение:" + Environment.NewLine +
                        " возникло исключение: " + Environment.NewLine +
                        " === Message: " + ex.Message.ToString() + Environment.NewLine +
                        " === Exception: " + ex.ToString(), 1);

                    }
                        
                    MessageListener.Instance.ReceiveMessage(string.Format("Выполняется: {0}", "запуск сервисов"));

                    if (Convert.ToInt32(AppStart.TableReestr["ServisOnOff"]) == 1)
                    {
                        if (RunServis() > 0)
                        {
                            SystemConecto.ErorDebag("Приложение завершено.\nНарушена процедура безопасности, отсутствуют системные файлы - STOP", 2, 2);
                            Environment.Exit(0); // Это работает

                        }
                        //if (SystemConecto.OSWMI != 3)
                       
                    }


                    MessageListener.Instance.ReceiveMessage(string.Format("Выполняется: {0}", "запуск панели приложений"));

                    // Процедура загрузки состояния панели активных приложений
                    LoadedAppPlay();

                    // ------------ Не забудь отключить события при выходе из программы!!
                    // Изменения разрешения экрана пользователем
                    SystemEvents.DisplaySettingsChanged += new EventHandler(SystemEvents_DisplaySettingsChanged);
                    // Изменения системного времени
                    SystemEvents.TimeChanged +=new EventHandler(SystemEvents_TimeChanged);
                    // SystemEvents.TimeChanged
                    // Изменения в остановке подачи питания в системе или режимы энергосбережения
                    // SystemEvents.PowerModeChanged
                    //-------------
                    //var Test = aParamApp; // отладка

                            //Считование конфигурации в память
                            SystemConecto.RolePC("StartRole");
                            //// Запуск интегрированных серверов как служб супер сервера
                            //SystemConectoTimeServer.TimeServer("AutoServer");
                            ////SystemConectoNetWorkServer.NetWorkServer("AutoServer");
                            //// SystemConectoRS_XXX.Main();

                            //SystemConectoServers.ServersStart();

                    // ------ Отладка
                    // ListDeviceDriverName("");
                    // ViewNet();
                    // ConectToPC();

                    // Создание соедиения с подключаемыми БД отсюда переносится к фоновому потоку TaskWorkSpace1
                    // ConectionDefaultSql();


                    // ------- Блокировка выключения ПК для терминальной и серверной версии (Тест)
                    //SystemConecto.Lock();


                    //=========================================================================================

                    // ------- для Form
                    //Application.EnableVisualStyles();
                    // Application.SetCompatibleTextRenderingDefault(false);
                    //Application.Run(new ConectoWorkSpace(oSystemConecto));
                    //Application.Run(new ConectoWorkSpace());



                    // ------- для WPF
                    //if (SystemConecto.OSWMI != 3)
                    MessageListener.Instance.ReceiveMessage(string.Format("Выполняется: {0}", "запуск " + App.aSystemVirable["CaptionNamePRG"]));
                    // Создать основное окно
                    switch (App.aSystemVirable["NameTh"])
                    {
                        case "CashBox":

                            break;
                        case "OfficeSpaceCashBox":

                            break;
                        case "WorkSpace":
                            
                            switch (App.aSystemVirable["type_app"])
                            {
                                case "-p":
                                    
                                        Application.Current.MainWindow = null;

                                        if (IsSystemOSIcoSYBAR() > 0)
                                        {

                                        }

                                        //mainWindow.Show();
                                        break;
                                default:
                                    Application.Current.MainWindow = new MainWindow();  //MainWindow mainWindow
                                    // mainWindow.WindowState = WindowState.Maximized;
                                    Application.Current.MainWindow.Show(); //mainWindow.Show();
                                    break;

                            }

                            break;
                        default:
                            Environment.Exit(0);
                            break;
                    }


                    //--------------
                }
                else
                {
                    // Проверка изменения имени файла
                    FileInfo fileInf = new FileInfo(Process.GetCurrentProcess().MainModule.FileName); // разбираем путь старта приложения
                    // это название файла
                    
                    if (fileInf.Name == App.NameOPOS + ".exe")
                    {
                        // Активировать окно

                        // MessageBox.Show("Приложение уже запущено", "Сообщение", MessageBoxButton.OK, MessageBoxImage.Stop); //, MessageBoxButtons.OK, MessageBoxIcon.Stop
                    }
                    else
                    {
                        SystemConecto.ErorDebag(fileInf.Name + " == " + App.NameOPOS + ".exe");
                        SystemConecto.ErorDebag("Приложение завершено.\nНарушена процедура безопасности - <Изменено имя программы>\nРекомендации:\n- Переименуйте файл программы на " + App.NameOPOS + ".exe", 2, 2);
                    }
                    Environment.Exit(0);
                }


            }

 
            
        }

        //***************************************************

        /// <summary>
        /// Событие изменение разрешения экрана пользователем
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public static void SystemEvents_DisplaySettingsChanged(object sender, EventArgs e)
        {
            SystemConecto.ErorDebag("Изменил разрешение экран на основном мониторе. Систеное сообщение: " + e.ToString(), 3);
            //MainWindow ConectoWorkSpace_InW = (MainWindow)Application.Current.MainWindow;
            MainWindow ConectoWorkSpace_InW = AppStart.LinkMainWindow();
            // 1.  Проверка запущен ли основной экран WorkSpace

            // Динамическая прорисовка интерфейса и Динамические елементы
            ConectoWorkSpace_InW.ResizeConectoWorkSpace(ConectoWorkSpace_InW.WindowState);
        }
        /// <summary>
        ///  Событие изменение времени устройства пользователем
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public static void SystemEvents_TimeChanged(object sender, EventArgs e)
        {
            if(SystemConectoTimeServer.IDChekTime == 0){
                SystemConectoTimeServer.IDChekTime = 1;
                SystemConecto.ErorDebag("Пользователь изменил время. Систеное сообщение: " + e.ToString(), 3);
                if (TickRg_a[6] == -7 && SystemConecto.aParamApp["Time_IP"] != "0.0.0.0")
                {
                    TickRg_a[6] = 0;
                    SystemConecto.ErorDebag("Система автоматически запустила задачу на исправление времени." , 3);
                    // Установка тика в состоянии выполнения задачи
                    Tick_a[6] = 9; 
                }
            }
        }





    
    }

    // Заменил на интерактивный метод Отобразить запущенный процесс вместо окна ограничения доступа - RunningInstance
    //public static class ApplicationController
    //{
    //    private static Mutex _mutex;
    //    private static bool _mutexCreated;
    //    private static readonly string _mutexName = String.Format(CultureInfo.InvariantCulture, UniqueIdentifier);

    //    public static bool IsAlreadyLauched()
    //    {
    //        _mutex = new Mutex(true, _mutexName, out _mutexCreated);

    //        return !_mutexCreated;
    //    }

    //    public static void Clear()
    //    {
    //        if (_mutex == null)
    //            return;

    //        if (_mutexCreated)
    //            _mutex.ReleaseMutex();

    //        _mutex.Close();
    //        _mutex = null;
    //    }

    //    private const string UniqueIdentifier = "1D2C470B-A2A5-4362-B23E-A3008BCDA551";
    //}


    public static class UacHelper
    {
        private const string uacRegistryKey = "Software\\Microsoft\\Windows\\CurrentVersion\\Policies\\System";
        private const string uacRegistryValue = "EnableLUA";

        private static uint STANDARD_RIGHTS_READ = 0x00020000;
        private static uint TOKEN_QUERY = 0x0008;
        private static uint TOKEN_READ = (STANDARD_RIGHTS_READ | TOKEN_QUERY);

        [DllImport("advapi32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool OpenProcessToken(IntPtr ProcessHandle, UInt32 DesiredAccess, out IntPtr TokenHandle);

        [DllImport("kernel32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool CloseHandle(IntPtr hObject);

        [DllImport("advapi32.dll", SetLastError = true)]
        public static extern bool GetTokenInformation(IntPtr TokenHandle, TOKEN_INFORMATION_CLASS TokenInformationClass, IntPtr TokenInformation, uint TokenInformationLength, out uint ReturnLength);

        public enum TOKEN_INFORMATION_CLASS
        {
            TokenUser = 1,
            TokenGroups,
            TokenPrivileges,
            TokenOwner,
            TokenPrimaryGroup,
            TokenDefaultDacl,
            TokenSource,
            TokenType,
            TokenImpersonationLevel,
            TokenStatistics,
            TokenRestrictedSids,
            TokenSessionId,
            TokenGroupsAndPrivileges,
            TokenSessionReference,
            TokenSandBoxInert,
            TokenAuditPolicy,
            TokenOrigin,
            TokenElevationType,
            TokenLinkedToken,
            TokenElevation,
            TokenHasRestrictions,
            TokenAccessInformation,
            TokenVirtualizationAllowed,
            TokenVirtualizationEnabled,
            TokenIntegrityLevel,
            TokenUIAccess,
            TokenMandatoryPolicy,
            TokenLogonSid,
            MaxTokenInfoClass
        }

        public enum TOKEN_ELEVATION_TYPE
        {
            TokenElevationTypeDefault = 1,
            TokenElevationTypeFull,
            TokenElevationTypeLimited
        }

        public static bool IsUacEnabled
        {
            get
            {
                using (RegistryKey uacKey = Registry.LocalMachine.OpenSubKey(uacRegistryKey, false))
                {
                    bool result = uacKey.GetValue(uacRegistryValue).Equals(1);
                    return result;
                }
            }
        }

        public static bool IsProcessElevated
        {
            get
            {
                if (IsUacEnabled)
                {
                    IntPtr tokenHandle = IntPtr.Zero;
                    if (!OpenProcessToken(Process.GetCurrentProcess().Handle, TOKEN_READ, out tokenHandle))
                    {
                        throw new ApplicationException("Could not get process token.  Win32 Error Code: " +
                                                       Marshal.GetLastWin32Error());
                    }

                    try
                    {
                        TOKEN_ELEVATION_TYPE elevationResult = TOKEN_ELEVATION_TYPE.TokenElevationTypeDefault;

                        int elevationResultSize = Marshal.SizeOf(typeof(TOKEN_ELEVATION_TYPE));
                        uint returnedSize = 0;

                        IntPtr elevationTypePtr = Marshal.AllocHGlobal(elevationResultSize);
                        try
                        {
                            bool success = GetTokenInformation(tokenHandle, TOKEN_INFORMATION_CLASS.TokenElevationType,
                                                               elevationTypePtr, (uint)elevationResultSize,
                                                               out returnedSize);
                            if (success)
                            {
                                elevationResult = (TOKEN_ELEVATION_TYPE)Marshal.ReadInt32(elevationTypePtr);
                                bool isProcessAdmin = elevationResult == TOKEN_ELEVATION_TYPE.TokenElevationTypeFull;
                                return isProcessAdmin;
                            }
                            else
                            {
                                throw new ApplicationException("Unable to determine the current elevation.");
                            }
                        }
                        finally
                        {
                            if (elevationTypePtr != IntPtr.Zero)
                                Marshal.FreeHGlobal(elevationTypePtr);
                        }
                    }
                    finally
                    {
                        if (tokenHandle != IntPtr.Zero)
                            CloseHandle(tokenHandle);
                    }
                }
                else
                {
                    WindowsIdentity identity = WindowsIdentity.GetCurrent();
                    WindowsPrincipal principal = new WindowsPrincipal(identity);
                    bool result = principal.IsInRole(WindowsBuiltInRole.Administrator)
                               || principal.IsInRole(0x200); //Domain Administrator
                    return result;
                }
            }
        }
    }

    // ----------------------------------------------------------------------------- Продолжение ядра







    //============================== Пример ручной компиляции

    //public object ExecuteString(string csCode)
    //    {
    //        CSharpCodeProvider csCodeProvider = new CSharpCodeProvider();
    //        CompilerParameters cParams = new CompilerParameters();
    //        cParams.ReferencedAssemblies.Add("system.dll");
    //        cParams.ReferencedAssemblies.Add("system.windows.forms.dll");
    //        cParams.ReferencedAssemblies.Add("system.drawing.dll");
    //        cParams.CompilerOptions = "/optimize+";
    //        cParams.GenerateInMemory = true;

    //        StringBuilder sb = new StringBuilder("");
    //        sb.Append("using System;");
    //        sb.Append("namespace RuntimeCodeCompiler {");
    //        sb.Append("class GeneratedClass {");
    //        sb.Append("public static System.Drawing.Point EvalCode() {");
    //        sb.Append(csCode);
    //        sb.Append("}}}");

    //        CompilerResults cr = csCodeProvider.CompileAssemblyFromSource(cParams, sb.ToString());
    //        System.Reflection.Assembly assembly = cr.CompiledAssembly;

    //        object instance = assembly.CreateInstance("RuntimeCodeCompiler.GeneratedClass");
    //        Type type = instance.GetType();
    //        MethodInfo mi = type.GetMethod("EvalCode");
    //        object result = mi.Invoke(instance, null);

    //        return result;
    //    }

    // -------------------------- Многопоточная среда
    //[MTAThread]
    //static void JadroTim1(string[] args)
    //{
    //    // Запуск фонового приложения (Атрибут необходим для запуска фоновых задач) // Фоновые события ... Timer
    //    // var oSystemConecto = System.Threading.Timer;
    //    // Application.Run();

    //    // Dictionary<string, string> aSystemVirable = new Dictionary<string, string>();

    //    // Создаем событие, сигнализирующее ожидания порог кол-во. Таймер обратного вызова.
    //    AutoResetEvent autoEvent = new AutoResetEvent(false);
    //    StatusChecker statusChecker = new StatusChecker(10);

    //    // Создать вывод делегат, который вызывает методы (задачи) для таймера.
    //    TimerCallback tcb = statusChecker.CheckStatus;

    //    // Создать таймер, который сигнализирует делегату вызов (задач)
    //    // CheckStatus через одну секунду, и каждое 1/4 секунды после этого
    //    System.Threading.Timer stateTimer = new System.Threading.Timer(tcb, autoEvent, 1000, 250);


    //    /*
    //     // Смена периода таймера - Когда autoEvent сигналов, изменения на период до каждого 1/2 секунды.
    //     autoEvent.WaitOne(5000, false);
    //     stateTimer.Change(0, 500);

    //     // Отключение таймера - Когда autoEvent сигналы во второй раз, избавиться от таймера.
    //     autoEvent.WaitOne(5000, false);
    //     stateTimer.Dispose();
    //    */

    //}

    // -------------------------- Для многопотоковых запросов проверка

    //class StatusChecker
    //{
    //    private int invokeCount;
    //    private int maxCount;

    //    public StatusChecker(int count)
    //    {
    //        invokeCount = 0;
    //        maxCount = count;
    //    }

    //    // Этот метод вызывается таймером делегата.
    //    public void CheckStatus(Object stateInfo)
    //    {
    //        AutoResetEvent autoEvent = (AutoResetEvent)stateInfo;
    //        //Console.WriteLine("{0} Checking status {1,2}.",  DateTime.Now.ToString("h:mm:ss.fff"),  (++invokeCount).ToString());
    //        // Выполнить задачу 


    //        if (invokeCount == maxCount)
    //        {
    //            // Сброс счетчика сигналом Main.
    //            invokeCount = 0;
    //            autoEvent.Set();
    //        }
    //    }
    //}
    //--------------------------------------------------------------- 
    // 1. Свой DCHP сервер

}
