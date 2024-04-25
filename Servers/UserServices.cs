
using System.Threading.Tasks;
using ConectoWorkSpace;
using System;
using System.Collections;
using System.Drawing.Drawing2D;

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

        #region Проверка работоспособности соедининий (с БД, сервером Conecto, Интернет и пр.) -Thread
        /// <summary>
        /// Запуск потоков (тиков) -Thread для выполнения задач системы
        /// </summary>
        public static System.Timers.Timer WorkSpaceTimer1 = null;

        /// <summary>
        /// Временные отметки для выполнения кода<para></para>
        /// 
        /// <para></para>
        /// Перечень потоков (выполняемых задач) Tick_a<para></para>
        /// Tick_a[0] - 0 тик  для вывда времени 1 тик 5 секунд 60/5<para></para>
        /// Tick_a[1] - 1 тик индикатор проверки соединения сети<para></para>
        /// Tick_a[2] - 2 тик для проверки соединения с сервером WorkSpace {рабочих программ}<para></para>
        /// Tick_a[3] - 3 тик для Окна ожидания Wait, задача закрыть (отмеряем задержку окна, возможно замерить производительность выполняемых задач!)<para></para>
        /// Tick_a[4] - 4 тик для Проверки ключа лицензии для B52 ( не проверяет если не установлен режим проверки)<para></para>
        /// Tick_a[5] - 5 тик для Проверки соединения Терминала с БД ( не проверяет если не установлен режим проверки)<para></para>
        /// Tick_a[6] - 6 тик запуск клиента синхронизации времени<para></para>
        /// Tick_a[7] - 7 тик для удаления файлов в фоне<para></para>
        /// Tick_a[8] - 8 тик Выполнение задачи в системе каждые 10 минут, метода в dll (например: резервное копирование по расписанию и прочее настраивается в system.fdb с помощью интерфейса)<para></para>
        /// Tick_a[9] - 9 тик Выполнение задачи в системе каждые N минут, метода в dll (время настраивается в system.fdb с помощью интерфейса)<para></para>
        /// Tick_a[10] - 10 тик Выполнение задачи в системе каждые 10 минут, метода в dll (например: резервное копирование по расписанию и прочее настраивается в task.ini, файловое управление задачами. Для ускоренных запусков и тестов. )<para></para>
        /// Tick_a[11] - 11 тик Проверка обновления системы<para></para>
        /// Tick_a[12] - 12 тик Проверка оплаты обслуживания и сервисов<para></para>
        /// Tick_a[13] - 13 тик Проверка акций, банеры на рабочем столе <para></para>
        /// </summary>
        public static int[] Tick_a = new int[18] { 0, 0, 0, 0, -1, -1, 9, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };

        /// <summary>
        /// Режимы тиков (каждый тик может еще иметь свои режимы: Режим -7 для всех потоков, поток выключен )
        /// </summary>
        public static int[] TickRg_a = new int[18] { 0, 1, 0, 0, -7, -7, -7, -7, -7, -7, -7, -7, -7, -7, -7, -7, -7, -7 };

        /// <summary>
        /// Предыдущие состояния тиков (каждый тик может еще иметь свое предыдущие состояние)<para></para>
        /// TickMemory_a[1] - состояние подключения к интернету; 1 - подключен
        /// </summary>
        public static int[] TickMemory_a = new int[18] { 0, -1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };

        /// <summary>
        /// Остановка выполнения потоков до разрешения (1 - вывод информационных окон на рабочий стол запрещен блокировка)
        /// </summary>
        private static int[] WaitMemory_a = new int[2] { 0, 0 };
        //static object lockWaitWindow = new object();
        //static object lockWait = new object();


        /// <summary>
        /// Информация об активном терминале на фронте
        /// разбор айпи адресов
        /// IPAddress.Parse("192.168.43.1") "127.0.0.1"
        /// список рабочих подключений к БД и их адресса
        /// </summary>
        public static string[] ListActiveTerminalIP = new string[3] { "", "", "" };

        /// <summary>
        /// Информация об активном беке на компьюторе
        /// разбор айпи адресов
        /// IPAddress.Parse("192.168.43.1") "127.0.0.1"
        /// список рабочих подключений к БД и их адресса
        /// </summary>
        public static string[] ListActiveBackIP = new string[3] { "", "", "" };

        /// <summary>
        /// Запуск обнаружения задач в системе
        /// </summary>
        public static void TimerWorkSpace()
        {

            System.Timers.Timer WorkSpaceTimer1 = new System.Timers.Timer();
            WorkSpaceTimer1.Elapsed += new ElapsedEventHandler(TaskWorkSpace1);
            WorkSpaceTimer1.Interval = 3000; // каждые три секунды
            WorkSpaceTimer1.Start();

            // ------- Выполнение стартового кода
            // Расчет тиков
            // 1.  Часы на основном экране
            DateTime now_ = DateTime.Now;
            int Second = now_.Second;
            decimal deltaTick = Second / 5;
            Tick_a[0] = (int)Math.Round(deltaTick);

            // 2. проверка соединения сети
            Tick_a[1] = 15;

            // 3. Проверка времени Тик 2 
            if (SystemConecto.aParamApp["Time_IP"] == "0.0.0.0")
            {
                TickRg_a[6] = -7;
            }

            // Запуск по умолчанию


        }

        #region Запуск выпоняемых задач №1 с периодичностью
        /// <summary>
        /// Описание выпоняемых задач №1
        /// </summary>
        /// <param name="source"></param>
        /// <param name="e"></param>
        public static void TaskWorkSpace1(object source, ElapsedEventArgs e)
        {
            // Тик 0 Время
            if (TickRg_a[0] > -7 && Tick_a[0] == 12)
            {
                // Сброс тика
                Tick_a[0] = 0;
                RenderInfo Arguments01 = new RenderInfo() { };
                Thread thStartTimer01 = new Thread(Time_Th);
                thStartTimer01.SetApartmentState(ApartmentState.STA);
                thStartTimer01.IsBackground = true; // Фоновый поток
                thStartTimer01.Start(Arguments01);

            }
            // Тик 1 Интернет и сеть (если сеть отключена от интернета то проверка сети ускоряется до 5 секунд)
            if (TickRg_a[1] > -7 && (Tick_a[1] == 15 || TickMemory_a[1] == 0 && Tick_a[1] == 5))
            {
                Tick_a[1] = 0;
                RenderInfo Arguments02 = new RenderInfo() { };
                Thread thStartTimer02 = new Thread(Internet_Th);
                thStartTimer02.SetApartmentState(ApartmentState.STA);
                thStartTimer02.IsBackground = true; // Фоновый поток
                thStartTimer02.Start(Arguments02);

            }
            // Тик 2 Связь с БД
            if (TickRg_a[2] > -7 && Tick_a[2] == 3)
            {
                Tick_a[2] = 0;
                RenderInfo Arguments03 = new RenderInfo() { };
                Thread thStartTimer03 = new Thread(ActiveServiceWS_Th);
                thStartTimer03.SetApartmentState(ApartmentState.STA);
                thStartTimer03.IsBackground = true; // Фоновый поток
                thStartTimer03.Start(Arguments03);

            }
            // Тик 3 Сервис закрыть окно Ожидания Wait
            if (TickRg_a[3] > -7 && Tick_a[3] == -1)
            {
                WinWait_Close();

            }
            // 4 тик для Проверки ключа лицензии для B52
            if (TickRg_a[4] > -7 && (Tick_a[4] == 35 && TickMemory_a[4] == 0 || (Tick_a[4] == -1 && TickMemory_a[4] == 0)))
            {
                Tick_a[4] = 0;
                TickMemory_a[4] = 1;
                RenderInfo Arguments04 = new RenderInfo() { };
                Thread thStartTimer04 = new Thread(KeyB52Active);
                thStartTimer04.SetApartmentState(ApartmentState.STA);
                thStartTimer04.IsBackground = true; // Фоновый поток
                thStartTimer04.Start(Arguments04);


            }
            // 5 тик для Проверки соединения з БД терминала
            if (TickRg_a[5] > -7 && (Tick_a[5] == 20 && TickMemory_a[5] == 0 || (Tick_a[5] == -1 && TickMemory_a[5] == 0)))
            {
                Tick_a[5] = 0;
                TickMemory_a[5] = 1;
                RenderInfo Arguments05 = new RenderInfo() { };
                Thread thStartTimer05 = new Thread(BdActiveTerminal);
                thStartTimer05.SetApartmentState(ApartmentState.STA);
                thStartTimer05.IsBackground = true; // Фоновый поток
                thStartTimer05.Start(Arguments05);


            }
            // Тик 6 Время системе каждые
            if (TickRg_a[6] > -7 && Tick_a[6] == 10)
            {
                // Сброс тика
                Tick_a[6] = 0;
                RenderInfo Arguments06 = new RenderInfo() { };
                Thread thStartTimer06 = new Thread(Synhro_Time);
                thStartTimer06.SetApartmentState(ApartmentState.STA);
                thStartTimer06.IsBackground = true; // Фоновый поток
                thStartTimer06.Start(Arguments06);

            }

            // 7

            // Тик 8 Выполнение задачи в системе каждые 10 минут, метода в dll, system.fdb
            if (TickRg_a[8] > -7 && Tick_a[6] == 120)
            {
                // Сброс тика
                Tick_a[8] = 0;
                RenderInfo Arguments08 = new RenderInfo() { };
                Thread thStartTimer08 = new Thread(TaskinSystem_Th);
                thStartTimer08.SetApartmentState(ApartmentState.STA);
                thStartTimer08.IsBackground = true; // Фоновый поток
                thStartTimer08.Start(Arguments08);

            }
            // Тик 9 Выполнение задачи в системе каждые N минут, метода в dll, system.fdb
            // Читать из настроек ... 10 => найди
            if (TickRg_a[9] > -7 && Tick_a[9] == 120)
            {
                // Сброс тика
                Tick_a[9] = 0;
                RenderInfo Arguments09 = new RenderInfo() { };
                Thread thStartTimer09 = new Thread(TaskinSystemN_Th);
                thStartTimer09.SetApartmentState(ApartmentState.STA);
                thStartTimer09.IsBackground = true; // Фоновый поток
                thStartTimer09.Start(Arguments09);

            }
            // Тик 10 Выполнение задачи в системе каждые 10 минут, метода в dll, task.ini
            if (TickRg_a[10] > -7 && Tick_a[10] == 120)
            {
                // Сброс тика
                Tick_a[10] = 0;
                RenderInfo Arguments10 = new RenderInfo() { };
                Thread thStartTimer10 = new Thread(TaskinFile_Th);
                thStartTimer10.SetApartmentState(ApartmentState.STA);
                thStartTimer10.IsBackground = true; // Фоновый поток
                thStartTimer10.Start(Arguments10);

            }
            // Тик 11 Проверка обновления системы каждые 60 минут работы приложения, дата и время последней проверки записана в system.fdb
            // Обновление проверятся каждую неделю или в ручном режиме
            if (TickRg_a[11] > -7 && Tick_a[11] == 720)
            {
                // Сброс тика
                Tick_a[11] = 0;
                RenderInfo Arguments11 = new RenderInfo() { };
                Thread thStartTimer11 = new Thread(Update_Th);
                thStartTimer11.SetApartmentState(ApartmentState.STA);
                thStartTimer11.IsBackground = true; // Фоновый поток
                thStartTimer11.Start(Arguments11);

            }

            // Тик 12 Проверка оплаты обслуживания и сервисов каждые 10 минут работы приложения, дата и время последней проверки записана в system.fdb 
            if (TickRg_a[12] > -7 && Tick_a[12] == 120)
            {
                // Сброс тика
                Tick_a[12] = 0;
                RenderInfo Arguments12 = new RenderInfo() { };
                Thread thStartTimer12 = new Thread(BayService_Th);
                thStartTimer12.SetApartmentState(ApartmentState.STA);
                thStartTimer12.IsBackground = true; // Фоновый поток
                thStartTimer12.Start(Arguments12);

            }

            // Тик 13 Проверка акций, банеры на рабочем столе каждые 60 минут работы приложения, дата и время последней проверки записана в system.fdb 
            if (TickRg_a[13] > -7 && Tick_a[13] == 120)
            {
                // Сброс тика
                Tick_a[13] = 0;
                RenderInfo Arguments13 = new RenderInfo() { };
                Thread thStartTimer13 = new Thread(Sales_Th);
                thStartTimer13.SetApartmentState(ApartmentState.STA);
                thStartTimer13.IsBackground = true; // Фоновый поток
                thStartTimer13.Start(Arguments13);

            }

            int Idcount = 0;
            // Увеличение Тика 
            foreach (int kvp in TickRg_a)
            {
                if (TickRg_a[0] != -7)
                {
                    Tick_a[Idcount]++; //  Tick_a[Idcount] = Tick_a[Idcount] + 1;
                }

                Idcount++;
            }

        }
        #endregion


        #region Тик 0  для вывда времени 1 тик 5 секунд 60/5 - Часы (Время) на основном окне WorkSpace
        /// <summary>
        /// Тик 0 Поток №2
        /// </summary>
        /// <param name="ThreadObj"></param>
        private static void Time_Th(object ThreadObj)
        {

            //TimeSpan current_time = DateTime.Now.TimeOfDay;
            // текущее время в виде строки ToString("HH:mm:ss")
            //DateTime now = new DateTime;
            //DateTime now_ = new DateTime(); 
            //now = DateTime.Now ; 
            //int hour = now_.Hour;

            System.Windows.Application.Current.Dispatcher.Invoke(new Action(delegate ()
            {
                MainWindow ConectoWorkSpace_InW = LinkMainWindow();
                if (ConectoWorkSpace_InW == null)
                {
                    return;
                }
                ConectoWorkSpace_InW.Time_L_mm.Content = DateTime.Now.ToString("mm");
                ConectoWorkSpace_InW.Time_L_hh.Content = DateTime.Now.ToString("HH");
                // Если время меняется в полночь (смена даты и дня недели
                ConectoWorkSpace_InW.Date.Content = DateTime.Now.ToString(@"ddd  d.MM", CultureInfo.CreateSpecificCulture("ru-RU"));

            }));

            // Проверка формы
            //if (System.Windows.Forms.Application.OpenForms["ConectoWorkSpace"] != null)
            //{
            //    //BeginInvoke(new Action(delegate() { button1.Visible = false; }));
            //    String current_time_str = DateTime.Now.ToString("HH:mm");
            //    System.Windows.Forms.Application.OpenForms["ConectoWorkSpace"].BeginInvoke(new Action(delegate() {
            //        System.Windows.Forms.Application.OpenForms["ConectoWorkSpace"].Controls["Time_L"].Text = current_time_str;
            //        System.Windows.Forms.Application.OpenForms["ConectoWorkSpace"].Controls["Time_L"].Refresh();

            //     }));
            //    //ErorDebag("Отладка", 2);
            //}
        }
        #endregion

        #region Тик 1 индикатор проверки соединения сети Интернет и сеть
        /// <summary>
        /// Интернет и сеть 
        /// Тик 1 Поток №3
        /// TickRg_a[1] == 0 - включен пинг определение доступа
        /// </summary>
        /// <param name="ThreadObj"></param>
        private static void Internet_Th(object ThreadObj)
        {
            ////ErorDebag("Отладка", 2);
            //// Тик 2 Интернет и сеть (если сеть отключена от интернета то проверка сети ускоряется до 5 секунд)
            //// Усложняем проверкой соединения интерфейса  физически к комутатору пакетов (тобиш к свичу, точке доступа ...)

            // Разные способы проверки связи - ConnectionAvailable_ICMP() - быстрее, можно заблокировать;
            // ConnectionAvailable() - точнее но медленней
            // SystemConecto.InternetAvailability.IsConnectedToInternet() - еще быстрее системно
            // Имитация Telnet - Клиент сокет пример, клиент Караоке
            // Таблица портов http://ru.wikipedia.org/wiki/%D0%A1%D0%BF%D0%B8%D1%81%D0%BE%D0%BA_%D0%BF%D0%BE%D1%80%D1%82%D0%BE%D0%B2_TCP_%D0%B8_UDP

            // Методы чередуются, тест
            var result = TickRg_a[1] == 0 ? SystemConecto.InternetAvailability.IsConnectedToInternet() : SystemConecto.ConnectionAvailable();
            //var result = ConnectionAvailable_ICMP();


            // Проверка физического соединения (кабель, WiFi, VPN)
            var DopInfo = "";
            bool Ehernet = false;
            if (!result)
            {
                int[] ResultNetOff = SystemConecto.NetOff();
                // Проверка на отключения всех адаптеров (задел шнур, отключилось комутирующие устройство)
                // ResultNetOff[0] == (ResultNetOff[2] + ResultNetOff[3] + ResultNetOff[4] + ResultNetOff[5])
                if (ResultNetOff[1] == 0)
                {
                    // Все адаптеры выключены
                    DopInfo = Environment.NewLine +
                            "..." + Environment.NewLine +
                            "Все подключения " + Environment.NewLine +
                            "к сети выключены!";
                }
                else
                {
                    // Windows Тупо считать из ОС
                    if (SystemConecto.InternetAvailability.IsConnectedToInternet())
                    {

                    }
                    else
                    {
                        // Если это не работает подать сомнению службу Windows и 
                        // Проверить доступ к шлюзу (проверка физического соединения, пинг может не сработать по причине закрытия порта)

                        var RezGetwai = SystemConecto.NetGetwai();
                        if (RezGetwai > 0)
                        {
                            Ehernet = true;
                            DopInfo = RezGetwai == 2 ? Environment.NewLine +
                                "..." + Environment.NewLine +
                                "Не записан шлюз сети, " + Environment.NewLine +
                                "отключен DCHP сервер" : "";

                            // Если шлюз закрыт на пинг то проверить google

                        }
                        // Если шлюз закрыт на пинг то проверить google


                    }

                }
                /// Результат 0- количество адаптеров , 1- включенных адаптеров, 
                /// 2 - количество выключенных адаптеров  WiFi
                /// 3 - количество выключенных адаптеров Ppp 
                /// 4 - количество выключенных адаптеров Ethernet
                /// 5 - количество выключенных других адаптеров

            }



            System.Windows.Application.Current.Dispatcher.Invoke(new Action(delegate ()
            {
                MainWindow ConectoWorkSpace_InW = LinkMainWindow();
                if (ConectoWorkSpace_InW == null)
                {
                    return;
                }
                // ConectoWorkSpace.MainWindow ConectoWorkSpace_InW = (ConectoWorkSpace.MainWindow)Application.Current.MainWindow;

                // Ссылка на объект
                var pic = ConectoWorkSpace_InW.ConectInternet;
                var wP_SysI = ConectoWorkSpace_InW.wP_SysIndicat;
                double[] TopWidth = ConectoWorkSpace_InW.MessageCoordinatInfoPa(1);

                if (rkAppSetingAllUser.GetValue("TerminalRDP") != null && (int)rkAppSetingAllUser.GetValue("TerminalRDP") == 1)
                {

                    //Process.Start("taskkill.exe", "/F /IM explorer.exe");
                    ConectoWorkSpace_InW.ResizeConectoWorkSpace(WindowState.Maximized);
                }



                var TextWindows = "";
                int AutoClose = 0;
                // Если состояние не изменилось то ничего не происходит
                if (result && TickMemory_a[1] == 1 || result == false && TickMemory_a[1] == 0 || Ehernet == true && TickMemory_a[1] == 2)
                {
                    // все окей

                }
                else
                {

                    if (result)
                    {

                        pic.Source = new System.Windows.Media.Imaging.BitmapImage(new Uri(@"pack://application:,,,/Conecto®%20WorkSpace;component/Images/earth1_f.png"));
                        TextWindows = "Рабочее место" + Environment.NewLine +
                                        "подключено к интернету" + DopInfo;
                        TickMemory_a[1] = 1;
                        AutoClose = 1;
                    }
                    else
                    {
                        if (Ehernet)
                        {
                            pic.Source = new System.Windows.Media.Imaging.BitmapImage(new Uri(@"pack://application:,,,/Conecto®%20WorkSpace;component/Images/earth3_f.png"));
                            TextWindows = "Подключение к" + Environment.NewLine +
                                            " интернету отсутствует" + DopInfo;
                            TickMemory_a[1] = 2;
                            AutoClose = 1;
                        }
                        else
                        {

                            pic.Source = new System.Windows.Media.Imaging.BitmapImage(new Uri(@"pack://application:,,,/Conecto®%20WorkSpace;component/Images/earth2_f.png"));


                            TextWindows = "Подключение к" + Environment.NewLine +
                                            "интернету отсутствует" + DopInfo;
                            TickMemory_a[1] = 0;
                        }

                    }

                    // создаем Окно сообщение
                    if (WaitMemory_a[0] == 0)
                    {
                        var WinOblakoVerh_ = WindowActive("WinOblakoVerh_Net", TextWindows, AutoClose);
                        WinOblakoVerh_.Owner = ConectoWorkSpace_InW;
                        // размещаем на рабочем столе
                        WinOblakoVerh_.Top = SystemConecto.WorkAreaDisplayDefault[0] + wP_SysI.Margin.Top - (WinOblakoVerh_.Height - 58);
                        WinOblakoVerh_.Left = SystemConecto.WorkAreaDisplayDefault[1] + TopWidth[1]; //wP_SysI.Margin.Left + pic.Margin.Left - 28
                        // Не активировать окно - не передавать клавиатурный фокус http://msdn.microsoft.com/ru-ru/library/ms748948.aspx
                        WinOblakoVerh_.ShowActivated = false;
                        WinOblakoVerh_.Show(); // не показываем модальным (модальное окно не закрывается методом Close, а также не освобождает ресурсы метод fWait.Dispose(); )
                        //=== !Передать активность в основное окно пока не придумал
                        // ConectoWorkSpace_InW.Focus();
                    }
                    else
                    {
                        // Збрасываем память зачем?
                        // TickMemory_a[1] = 1;
                    }
                }
            }));

            // Переключатель режимов, чередование
            TickRg_a[1] = TickRg_a[1] == 0 ? 1 : 0;

            //-----------------------------------
        }
        #endregion

        #region Тик 2  для проверки соединения с сервером WorkSpace 
        /// <summary>
        /// Тик 2
        /// </summary>
        /// <param name="ThreadObj"></param>
        private static void ActiveServiceWS_Th(object ThreadObj)
        {


        }
        #endregion

        #region Тик 3 тик для Окна ожидания Wait, задача закрыть (отмеряем задержку окна, возможно замерить производительность выполняемых задач!)
        // не нужен
        #endregion

        #region Тик 4  Ключ B52
        /// <summary>
        /// Тик 4 
        /// </summary>
        /// <param name="ThreadObj"></param>
        private static void KeyB52Active(object ThreadObj)
        {
            // Проверка наличия файла
            // Список файлов
            Dictionary<string, string> fbembedList = new Dictionary<string, string>();

            fbembedList.Add(SystemConecto.PutchApp + @"Utils\KeyUtils\CHKNSK32.EXE", "b52/keyutils/");
            // Локальная
            if (SystemConecto.IsFilesPRG(fbembedList, 0, "- Проверка файлов во время установки файлов сервра ключа") == "True")
            {

                Process mv_prcInstaller = new Process();

                mv_prcInstaller.StartInfo.FileName = @SystemConecto.PutchApp + @"Utils\KeyUtils\CHKNSK32.EXE";
                mv_prcInstaller.StartInfo.Arguments = "";
                mv_prcInstaller.StartInfo.UseShellExecute = false; //--/qn Параметры имеют приоритет над объектом (не выводить интерфейс)
                                                                   //mv_prcInstaller.StartInfo.RedirectStandardInput = true;
                                                                   //mv_prcInstaller.StartInfo.RedirectStandardOutput = true;
                                                                   //mv_prcInstaller.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                                                                   //mv_prcInstaller.Start();

                // получаем ответ запущенного процесса
                //StreamReader srIncoming = mv_prcInstaller.StandardOutput;




                // выводим результат
                //SystemConecto.ErorDebag(srIncoming.ReadToEnd());
                //MessageBox.Show(srIncoming.ReadToEnd());
                //
                //Thread.Sleep(5000);


                // закрываем процесс
                // mv_prcInstaller.WaitForExit();

                //mv_prcInstaller.Close();
            }



            // Сетевая
            int Port = 4549;

            // ErorDebag("Запустили");193.151.90.229 192.168.1.23
            var PortActive = SystemConecto.NetScan(IPAddress.Parse("176.106.0.219"), Port);

            System.Windows.Application.Current.Dispatcher.Invoke(new Action(delegate ()
            {
                //MainWindow ConectoWorkSpace_InW = (MainWindow)Application.Current.MainWindow;
                MainWindow ConectoWorkSpace_InW = LinkMainWindow();
                if (ConectoWorkSpace_InW == null)
                {
                    return;
                }
                // Ссылка на объект
                var pic = ConectoWorkSpace_InW.KeyB52;
                var wP_SysI = ConectoWorkSpace_InW.wP_SysIndicat;
                double[] TopWidth = ConectoWorkSpace_InW.MessageCoordinatInfoPa(2);



                //foreach (KeyValuePair<int, string> dani in PortActive)  {
                //        MessageBox.Show(dani.Key.ToString()+" // "+ dani.Value.ToString());

                //    }


                if (PortActive[Port] == "True")
                {
                    pic.Visibility = System.Windows.Visibility.Collapsed;
                    ConectoWorkSpace_InW.numberElementsInfoPa(-1);

                }
                else
                {
                    pic.Visibility = System.Windows.Visibility.Visible;
                    ConectoWorkSpace_InW.numberElementsInfoPa(1);
                    //-----------------------------------
                    var TextWindows = "Ключ безопасности" + Environment.NewLine +
                            "отключен от сети!";
                    var AutoClose = 0;

                    // создаем Окно сообщение
                    //WinOblakoVerh WinOblakoVerh_ = new WinOblakoVerh(TextWindows, AutoClose); 
                    //WinOblakoVerh_.Name = "WinOblakoVerh_KeyB52";
                    // создаем Окно сообщение
                    if (WaitMemory_a[0] == 0)
                    {
                        var WinOblakoVerh_ = WindowActive("WinOblakoVerh_KeyB52", TextWindows, AutoClose);
                        WinOblakoVerh_.Owner = ConectoWorkSpace_InW;
                        // размещаем на рабочем столе
                        //MessageBox.Show(SystemConecto.WorkAreaDisplayDefault[0].ToString() + "|" + pic.Margin.Top.ToString() + "|" + WinOblakoVerh_.Height.ToString());

                        WinOblakoVerh_.Top = SystemConecto.WorkAreaDisplayDefault[0] + wP_SysI.Margin.Top - (WinOblakoVerh_.Height - 58);
                        WinOblakoVerh_.Left = SystemConecto.WorkAreaDisplayDefault[1] + TopWidth[1];  //wP_SysI.Margin.Left + pic.Margin.Left - 38
                        // Не активировать окно - не передавать клавиатурный фокус
                        WinOblakoVerh_.ShowActivated = false;
                        WinOblakoVerh_.Show(); // не показываем модальным (модальное окно не закрывается методом Close, а также не освобождает ресурсы метод fWait.Dispose(); )

                    }
                    else
                    {
                        // Збрасываем память
                    }
                }

            }));


            #region Код для Windows.Forms
            // ErorDebag("Получили Ответ");
            // Ссылка на объект
            //var pic = System.Windows.Forms.Application.OpenForms["ConectoWorkSpace"].Controls["KeyB52"] as System.Windows.Forms.PictureBox;

            //if (PortActive[3182] == "True")
            //{
            //    System.Windows.Forms.Application.OpenForms["ConectoWorkSpace"].BeginInvoke(new Action(delegate()
            //    {
            //        pic.Visible = false;

            //    })); 

            //}
            //else
            //{
            //    System.Windows.Forms.Application.OpenForms["ConectoWorkSpace"].BeginInvoke(new Action(delegate()
            //    {
            //        pic.Visible = true;

            //    }));
            //    //ErorDebag("");
            //    //-----------------------------------
            //    var TextWindows = "Ключ безопасности \n отключен от сети!";
            //    var AutoClose = 0;
            //    // Вывести окно 
            //    // Окно выводится по правилам: - вывести и закрыть окно при подключении к сети интернет (TickMemory_a[1]=1)
            //    // - Если окно открыто то мы его закрываем берем от туда текст и в среднем увеличиваем высоту окна на 21 px на одну строчку текста
            //    if (System.Windows.Forms.Application.OpenForms["WinOblakoVerh"] != null)
            //    {
            //        var Text_old = System.Windows.Forms.Application.OpenForms["WinOblakoVerh"].Controls["Text_L"].Text;
            //        // Формируем двух ярусный текст вывода
            //        TextWindows = Text_old + "\n...\n" + TextWindows;
            //        System.Windows.Forms.Application.OpenForms["ConectoWorkSpace"].BeginInvoke(new Action(delegate()
            //        {
            //            System.Windows.Forms.Application.OpenForms["WinOblakoVerh"].Close();

            //        }));
            //    }

            //    System.Windows.Forms.Application.OpenForms["ConectoWorkSpace"].BeginInvoke(new Action(delegate()
            //    {

            //        WinOblakoVerh WinOblakoVerh_ = new WinOblakoVerh(TextWindows, AutoClose); // создаем
            //        System.Windows.Forms.Application.OpenForms["ConectoWorkSpace"].AddOwnedForm(WinOblakoVerh_);
            //        //pic
            //        // размещаем на рабочем столе
            //        WinOblakoVerh_.Top = pic.Top - WinOblakoVerh_.Height;
            //        WinOblakoVerh_.Left = (pic.Left + pic.Width / 2) - 38;
            //        WinOblakoVerh_.Show(); // не показываем модальным (модальное окно не закрывается методом Close, а также не освобождает ресурсы метод fWait.Dispose(); )
            //    }));

            //    //--------------------------------------





            //}
            //System.Windows.Forms.Application.OpenForms["ConectoWorkSpace"].BeginInvoke(new Action(delegate()
            //{
            //    pic.Refresh();

            //}));
            #endregion


            TickMemory_a[4] = 0;
        }
        #endregion

        #region Тик 5  для Проверки соединения Терминала с БД  ( не проверяет если не установлен режим проверки)
        /// <summary>
        /// Тик 5
        /// </summary>
        /// <param name="ThreadObj"></param>
        private static void BdActiveTerminal(object ThreadObj)
        {
            // ErorDebag("Запустили");
            // Открыть порт, незабыть закрыть.
            // Проверить к какому терминалу подключен запускаемый фронт или Бек (информация из ярлыка)
            // ListActiveTerminalIP -  ListActiveBackIP
            var PortActive = SystemConecto.NetScan(IPAddress.Parse("192.168.43.1"), 3050);

            System.Windows.Application.Current.Dispatcher.Invoke(new Action(delegate ()
            {
                // MainWindow ConectoWorkSpace_InW = (MainWindow)Application.Current.MainWindow;
                MainWindow ConectoWorkSpace_InW = LinkMainWindow();
                if (ConectoWorkSpace_InW == null)
                {
                    return;
                }
                // Ссылка на объект
                var pic = ConectoWorkSpace_InW.Bd_Terminal;
                var wP_SysI = ConectoWorkSpace_InW.wP_SysIndicat;
                double[] TopWidth = ConectoWorkSpace_InW.MessageCoordinatInfoPa(3);

                // Статус порта
                if (PortActive[3050] == "True")
                {
                    pic.Visibility = System.Windows.Visibility.Collapsed;
                    ConectoWorkSpace_InW.numberElementsInfoPa(-1);

                }
                else
                {
                    pic.Visibility = System.Windows.Visibility.Visible;
                    ConectoWorkSpace_InW.numberElementsInfoPa(1);

                    //-----------------------------------
                    var TextWindows = "Терминал отключен \n от БД в сети!";
                    var AutoClose = 0;

                    // создаем Окно сообщение
                    if (WaitMemory_a[0] == 0)
                    {
                        var WinOblakoVerh_ = WindowActive("WinOblakoVerh_BD", TextWindows, AutoClose);
                        WinOblakoVerh_.Owner = ConectoWorkSpace_InW;
                        // размещаем на рабочем столе
                        WinOblakoVerh_.Top = SystemConecto.WorkAreaDisplayDefault[0] + wP_SysI.Margin.Top - (WinOblakoVerh_.Height - 58);
                        WinOblakoVerh_.Left = SystemConecto.WorkAreaDisplayDefault[1] + TopWidth[1]; //wP_SysI.Margin.Left - 38
                        // Не активировать окно - не передавать клавиатурный фокус
                        WinOblakoVerh_.ShowActivated = false;
                        WinOblakoVerh_.Show(); // не показываем модальным (модальное окно не закрывается методом Close, а также не освобождает ресурсы метод fWait.Dispose(); )
                    }
                    else
                    {
                        // Збрасываем память
                    }
                }

            }));

            TickMemory_a[5] = 0;
        }
        #endregion

        #region Тик 6  Синхронизация времени

        /// <summary>
        /// Тик 6 
        /// </summary>
        /// <param name="ThreadObj"></param>
        private static void Synhro_Time(object ThreadObj)
        {
            SystemConectoTimeServer.ClientTime();

        }

        #endregion

        #region Тик 8 Выполнение задачи в системе каждые 10 минут, метода в dll, system.fdb
        /// <summary>
        /// Тик 8 Выполнение задачи в системе каждые 10 минут, метода в dll, system.fdb
        /// 
        /// </summary>
        /// <param name="ThreadObj"></param>
        private static void TaskinSystem_Th(object ThreadObj)
        {


        }
        #endregion

        #region // Тик 9 Выполнение задачи в системе каждые N минут, метода в dll, system.fdb
        /// <summary>
        /// // Тик 9 Выполнение задачи в системе каждые N минут, метода в dll, system.fdb
        /// 
        /// </summary>
        /// <param name="ThreadObj"></param>
        private static void TaskinSystemN_Th(object ThreadObj)
        {


        }
        #endregion

        #region Тик 10 тик Выполнение задачи в системе каждые 10 минут, метода в dll (например: резервное копирование по расписанию и прочее настраивается в task.ini, файловое управление задачами. Для ускоренных запусков и тестов. )
        /// <summary>
        /// Тик 10 тик Выполнение задачи в системе каждые 10 минут, метода в dll (например: резервное копирование по расписанию и прочее настраивается в task.ini, файловое управление задачами. Для ускоренных запусков и тестов. )
        /// 
        /// </summary>
        /// <param name="ThreadObj"></param>
        private static void TaskinFile_Th(object ThreadObj)
        {


        }
        #endregion

        #region Тик 11 Проверка обновления системы каждые 60 минут работы приложения, дата последней проверки записана в system.fdb
        /// <summary>
        /// Тик 11 Проверка обновления системы каждые 60 минут работы приложения, дата последней проверки записана в system.fdb
        ///  Обновление проверятся каждую неделю или в ручном режиме
        /// </summary>
        /// <param name="ThreadObj"></param>
        private static void Update_Th(object ThreadObj)
        {


        }
        #endregion

        #region Тик 12 Проверка оплаты обслуживания и сервисов каждые 10 минут работы приложения, дата и время последней проверки записана в system.fdb 
        /// <summary>
        /// Тик 12 Проверка оплаты обслуживания и сервисов каждые 10 минут работы приложения, дата и время последней проверки записана в system.fdb 
        /// 
        /// </summary>
        /// <param name="ThreadObj"></param>
        private static void BayService_Th(object ThreadObj)
        {


        }
        #endregion

        #region Тик 13 тик Проверка акций, банеры на рабочем столе каждые 60 минут работы приложения, дата и время последней проверки записана в system.fdb 
        /// <summary>
        /// Тик  Тик 13 тик Проверка акций, банеры на рабочем столе каждые 60 минут работы приложения, дата и время последней проверки записана в system.fdb 
        /// 
        /// </summary>
        /// <param name="ThreadObj"></param>
        private static void Sales_Th(object ThreadObj)
        {


        }
        #endregion

        #region Тик X 

        #endregion

        #region Создания окон уведомлений для нижних кнопок проверки состояний (Сеть, Ключь, БД)
        /// <summary>
        /// Сформировать новое информационное окно
        /// </summary>
        /// <param name="NameWin"></param>
        /// <param name="TextWindows"></param>
        /// <param name="AutoClose"></param>
        /// <returns></returns>
        public static Window WindowActive(string NameWin, string TextWindows = "", int AutoClose = 0)
        {
            // Вывести окно 
            // Окно выводится по правилам: - вывести и закрыть окно при подключении к сети интернет (TickMemory_a[1]=1)
            // - Если окно открыто то мы его закрываем берем от туда текст -> и создаем образ текста в новом окне сверху
            //
            // {идея не получилась}(и в среднем увеличиваем высоту окна на 21 px на одну строчку текста)

            // Проверка какое окно открыто 0 - все закрыты
            var WinAllOpen = "";
            var WinOblakoVerh_Net = SystemConecto.ListWindowMain("WinOblakoVerh_Net");
            var WinOblakoVerh_KeyB52 = SystemConecto.ListWindowMain("WinOblakoVerh_KeyB52");
            var WinOblakoVerh_BD = SystemConecto.ListWindowMain("WinOblakoVerh_BD");
            var WinOblakoVerh_USBHDD = SystemConecto.ListWindowMain("WinOblakoVerh_USBHDD");


            // Проверка открыто ли окно которое открывается 
            bool WinOpen = false;

            // Подождать закрытия окна
            //if (WinOblakoVerh_Net != null || WinOblakoVerh_KeyB52 != null || WinOblakoVerh_BD != null || WinOblakoVerh_USBHDD != null)
            //{

            //    Thread.Sleep(9500);
            //}

            // Окно по умолчанию
            if (SystemConecto.ListWindowMain(NameWin) != null)
            {
                WinOpen = true;
            }

            //switch (NameWin)
            //{
            //    case ("WinOblakoVerh_Net"):
            //        if (WinOblakoVerh_Net != null)
            //        {
            //            WinOpen = true;
            //        }
            //        break;
            //    case ("WinOblakoVerh_KeyB52"):
            //        if (WinOblakoVerh_KeyB52 != null)
            //        {
            //            WinOpen = true;
            //        }
            //        break;

            //    case ("WinOblakoVerh_BD"):
            //        if (WinOblakoVerh_BD != null)
            //        {
            //            WinOpen = true;
            //        }
            //        break;
            //    case ("WinOblakoVerh_USBHDD"):
            //        if (WinOblakoVerh_USBHDD != null)
            //        {
            //            WinOpen = true;
            //        }
            //        break;
            //}

            // Якщо вікно за умовчанням не відчинино, перевіряємо, чи відчинені інші вікна
            // зачиняємо їх та копіюємо текст повідомлення у вікні
            if (!WinOpen)
            {

                TextBlock Message_ = null;
                double TopWindow = 0;
                Window WinOblakoVerh_NetInfo = SystemConecto.ListWindowMain("WinOblakoVerh_NetInfo"),
                        WinOblakoVerh_KeyB52Info = SystemConecto.ListWindowMain("WinOblakoVerh_KeyB52Info"),
                        WinOblakoVerh_BDInfo = SystemConecto.ListWindowMain("WinOblakoVerh_BDInfo"),
                        WinOblakoVerh_USBHDDInfo = SystemConecto.ListWindowMain("WinOblakoVerh_USBHDDInfo");
                if (WinOblakoVerh_Net != null)
                {
                    WinAllOpen = "WinOblakoVerh_NetInfo";
                    TopWindow = WinOblakoVerh_Net.Top;
                    Message_ = (TextBlock)LogicalTreeHelper.FindLogicalNode(WinOblakoVerh_Net, "MessageText");
                    WinOblakoVerh_Net.Close();
                }
                else
                {
                    if (WinOblakoVerh_KeyB52 != null)
                    {
                        WinAllOpen = "WinOblakoVerh_KeyB52Info";
                        TopWindow = WinOblakoVerh_KeyB52.Top;
                        Message_ = (TextBlock)LogicalTreeHelper.FindLogicalNode(WinOblakoVerh_KeyB52, "MessageText");
                        WinOblakoVerh_KeyB52.Close();
                    }
                    else
                    {
                        if (WinOblakoVerh_BD != null)
                        {
                            WinAllOpen = "WinOblakoVerh_BDInfo";
                            TopWindow = WinOblakoVerh_BD.Top;
                            Message_ = (TextBlock)LogicalTreeHelper.FindLogicalNode(WinOblakoVerh_BD, "MessageText");
                            WinOblakoVerh_BD.Close();
                        }
                        else
                        {
                            if (WinOblakoVerh_USBHDD != null)
                            {
                                WinAllOpen = "WinOblakoVerh_USBHDDInfo";
                                TopWindow = WinOblakoVerh_USBHDD.Top;
                                Message_ = (TextBlock)LogicalTreeHelper.FindLogicalNode(WinOblakoVerh_USBHDD, "MessageText");
                                WinOblakoVerh_USBHDD.Close();
                            }


                        }
                    }
                }



                var TextWindowsInfo = "";
                Window CloseTo = null, CloseWan = null;

                // Якщо повідомлення знайденно у інших вікнах
                if (Message_ != null)
                {
                    // Определение высоты инормационных окон (не более двух)
                    var TopWindowTo = 0;
                    if (WinOblakoVerh_NetInfo != null)
                    {
                        // Окно есть и расположенно
                        if (WinOblakoVerh_NetInfo.Top < TopWindow - 126)
                        {
                            // Размещен на втором уровне (закрыть окно первого уровня)
                            CloseTo = WinOblakoVerh_NetInfo;
                            //MessageBox.Show("NetInfo");

                        }
                        else
                        {
                            TopWindowTo = 125;
                            CloseWan = WinOblakoVerh_NetInfo;
                        }
                    }
                    if (WinOblakoVerh_KeyB52Info != null)
                    {
                        // Окно есть и расположенно
                        if (WinOblakoVerh_KeyB52Info.Top < TopWindow - 126)
                        {
                            // Размещен на втором уровне (закрыть окно первого уровня)
                            CloseTo = WinOblakoVerh_KeyB52Info;
                            //MessageBox.Show("KeyB52Info");

                        }
                        else
                        {
                            TopWindowTo = 125;
                            CloseWan = WinOblakoVerh_KeyB52Info;
                        }
                    }
                    if (WinOblakoVerh_BDInfo != null)
                    {
                        //MessageBox.Show(WinOblakoVerh_BDInfo.Top.ToString());
                        // Окно есть и расположенно
                        if (WinOblakoVerh_BDInfo.Top < TopWindow - 126)
                        {
                            // Размещен на втором уровне (закрыть окно первого уровня)
                            CloseTo = WinOblakoVerh_BDInfo;
                            //MessageBox.Show("BDInfo");
                        }
                        else
                        {
                            TopWindowTo = 125;
                            CloseWan = WinOblakoVerh_BDInfo;

                        }
                    }
                    if (WinOblakoVerh_USBHDDInfo != null)
                    {
                        //MessageBox.Show(WinOblakoVerh_BDInfo.Top.ToString());
                        // Окно есть и расположенно
                        if (WinOblakoVerh_USBHDDInfo.Top < TopWindow - 126)
                        {
                            // Размещен на втором уровне (закрыть окно первого уровня)
                            CloseTo = WinOblakoVerh_USBHDDInfo;
                            //MessageBox.Show("BDInfo");
                        }
                        else
                        {
                            TopWindowTo = 125;
                            CloseWan = WinOblakoVerh_USBHDDInfo;

                        }
                    }

                    if (CloseWan != null && CloseTo != null)
                    {
                        //MessageBox.Show(CloseWan.Top.ToString() + CloseTo.Top.ToString());
                        // Размещен на втором уровне (закрыть окно первого уровня)
                        CloseWan.Top = CloseTo.Top;
                        CloseTo.Close();
                        TopWindowTo = 0;
                    }



                    //return SystemConecto.ListWindowMain(NameWin);

                    TextWindowsInfo = Message_.Text;

                    //MainWindow ConectoWorkSpace_InW = (MainWindow)Application.Current.MainWindow;
                    MainWindow ConectoWorkSpace_InW = AppStart.LinkMainWindow();
                    // Створюємо інше вікно з повідомленням
                    Window WinOblakoVerh_Info = new WinOblakoVerh(TextWindowsInfo, 0, 1); // создаем AutoClose
                    //MessageBox.Show("Тук");
                    WinOblakoVerh_Info.Name = WinAllOpen;
                    WinOblakoVerh_Info.Owner = ConectoWorkSpace_InW;
                    WinOblakoVerh_Info.Top = TopWindow - 125 - TopWindowTo; // отнять высоту окна и промежуток межу окнами
                    WinOblakoVerh_Info.Left = -12;
                    // Не активировать окно - не передавать клавиатурный фокус
                    WinOblakoVerh_Info.ShowActivated = false;
                    WinOblakoVerh_Info.Show();
                    // Разместить...
                }

            }
            // Формируем двух ярусный текст вывода
            //TextWindows = Text_old + "\n...\n" + TextWindows;
            // 
            // Создать информационное окно без ссылки на информационную икону



            // Если окно по умолчанию не создано создать и передать ссылку на окно WinOblakoVerh
            if (!WinOpen)
            {
                if (NameWin == "WinOblakoLeft")
                {
                    WinOblakoLeft WinOblakoVerh_ = new WinOblakoLeft(TextWindows, AutoClose); // создаем 
                    WinOblakoVerh_.Name = NameWin;
                    return WinOblakoVerh_;
                }
                else
                {
                    Window WinOblakoVerh_ = new WinOblakoVerh(TextWindows, AutoClose); // создаем 
                    WinOblakoVerh_.Name = NameWin;
                    return WinOblakoVerh_;
                }

            }


            // Вернуть ссылку на Окно по умолчанию
            return SystemConecto.ListWindowMain(NameWin);
            //switch (NameWin)
            //{
            //    case ("WinOblakoVerh_Net"):

            //        return WinOblakoVerh_Net;
            //    case ("WinOblakoVerh_KeyB52"):

            //        return WinOblakoVerh_KeyB52;

            //    case ("WinOblakoVerh_BD"):

            //        return WinOblakoVerh_BD;
            //}

            // return null;
        }
        #endregion


        //------------------------------


        /// <summary>
        /// Изменение состояния тиков для выполняемых задач 
        /// сделано для задачи №: 1
        /// Контроль за временными отметками для выполнения кода
        /// CountTick = -1 выключить тик
        /// 
        /// ps Отсутствуют проверки максимальных значений тиков
        /// </summary>
        /// <param name="NumberTick">Номер тика</param>
        /// <param name="CountTick">Количество тиков</param>
        public static void TickTask1(int NumberTick, int CountTick)
        {
            if (Tick_a.Length > NumberTick)
            {
                // Отсутствуют проверки макс тиков
                // Tick_a - Временные отметки для выполнения кода
                // CountTick = -1 выключить
                Tick_a[NumberTick] = CountTick;
            }
        }
        /// <summary>
        /// Изменение предыдущего состояния тиков
        /// </summary>
        /// <param name="NumberTick">Номер тика</param>
        /// <param name="CountTick">состояние</param>
        public static void TickMemory1(int NumberTick, int MemoryTick)
        {
            if (TickMemory_a.Length > NumberTick)
            {
                // Отсутствуют проверки макс тиков
                TickMemory_a[NumberTick] = MemoryTick;
            }
        }

        // Разные связанные события или свойства.

        #region Изменение состояния режима Wait для Вывода сообщений на рабочий стол
        /// <summary>
        /// Изменение состояния режима Wait для Вывода сообщений на рабочий стол
        /// </summary>
        /// <param name="NumberTick">Номер тика</param>
        /// <param name="CountTick">Количество тиков</param>
        public static void WaitTaskWindow(int Set)
        {
            WaitMemory_a[0] = Set;
        }

        /// <summary>
        /// Включение потока AppStart.TickRg_a[6] = -7;
        /// </summary>
        /// <param name="nTh"></param>
        public static void EnableTh(int nTh)
        {
            AppStart.TickRg_a[nTh] = 1;
        }


        #endregion

        #endregion










    }
}
