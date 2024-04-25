using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
// Отладка Messagebox
using System.Windows;
// DllImport
using System.Runtime.InteropServices;
// Регулярные выражения
using System.Text.RegularExpressions;
// INotifyPropertyChanged - Уведомляет клиентов об изменении значения свойства.
using System.ComponentModel;
// Управление вводом-выводом
using System.IO;
// Управление сетью
using System.Net;
using System.Net.Sockets;
/// Многопоточность
using System.Threading;
// Перечень ошибок
using System.Collections;



namespace ConectoWorkSpace
{
    /// <summary>
    /// Класс управления временем на устройстве в системе <para></para>
    /// Клиент - контроль изменения времени, синхронизация с локальным или интернет сервером системы<para></para>
    /// Сервер - в разработке (оптимизация систем, сокращение избытка трафика, стабилизация локальных систем)
    /// (система - набор устройств соединенных в сеть, решающие основные задачи) <para></para>
    /// </summary>
    public static class SystemConectoTimeServer
    {
        /// <summary>
        /// Состояние сервера. Запущен ли он = true
        /// </summary>
        public static bool ServerStart = false;

        /// <summary>
        /// Последняя ошибка в сервере ... нужно изменить
        /// </summary>
        public static string ErrorServer = "";

        /// <summary>
        /// Возвращает или устанавливает состояния проверки установки времени на устройстве
        /// 0 - проверка в состоянии ожыдания
        /// 1 - осуществляется проверка
        /// </summary>
        public static int IDChekTime = 0;

        #region Запуск NTP Server сервера

        /// <summary>
        /// Запуск NTP Server сервера AutoServer - старт сервера<para></para>
        /// AutoServer - Авто старт сервера зависит от настроек сервера<para></para>
        /// StartServer - Принудительный старт сервера игнорирование aParamApp["Autorize_Admin-Conecto"]<para></para>
        /// StopServer - разработка<para></para>
        /// RezetServer - разработка<para></para>
        /// </summary>
        /// <param name="Command"></param>
        public static void TimeServer(string Command = null)
        {
            switch (Command)
            {
                case "AutoServer":
                    // Авто старт сервера зависит от настроек сервера
                    if (SystemConecto.aParamApp["Time_ServerSwitch"] == "1" && !ServerStart)
                    {
                        // Загрузка настроек сервера
                        ReadConfigTime();
                        // ServerStart
                        // Поиск и иницианализация устройств 
                        if (InitializeServer())
                        {
                            ServerStart = true;
                        }

                    }
                    break;

                case "StartServer":

                    // Принудительный старт сервера игнорирование aParamApp["Autorize_Admin-Conecto"]

                    if (!ServerStart)
                    {
                        // Загрузка настроек сервера
                        ReadConfigTime();
                        // Поиск и иницианализация устройств 
                        if (InitializeServer())
                        {
                            ServerStart = true;
                        }
                    }


                    break;
                case "StopServer":

                    // Принудительная остоновка сервера 
                    if (ServerStart)
                    {

                    }

                    break;
                case "RezetServer":

                    // Принудительная перезагрузка сервера 


                    break;
            }

        }

        #endregion

        #region Иницианализация сервера

        public static Thread[] threadsServer = new Thread[256];

        /// <summary>
        /// Включение сервера
        /// </summary>
        /// <returns></returns>
        public static bool InitializeServer()
        {

            // Если установить на порт 127.0.0.1, то я думаю, что слушать другие IP сетевых адаптеров
            // мы перестанем. Работа сервера будет локальной.
            // Предполагаю, что слушать нужно все адаптеры или настраивать маршрутизацию внутри Сервера

            int PotokServer = 0;

            // Перебор настроек установленных сетевых адаптеров
            var IPDevice = SystemConecto.IP_DeviceCcurent(true);
            foreach (KeyValuePair<string, string> dani in IPDevice)
            {
                // Проверка подключения адаптера к сети
                if (SystemConecto.NetworkPC[dani.Key + "_STATUS"] == "Up")
                {
                    // Включить только на сетевых адаптерах примеры  SystemConecto.NetOff () Loopback - нужно включить только для отладки
                    if (SystemConecto.NetworkPC[dani.Key + "_TypeInterf"] == "Ethernet" || (SystemConecto.NetworkPC[dani.Key + "_TypeInterf"] == "Loopback" && SystemConecto.DebagApp))
                    {

                        
                        // Запуск сервера параметры 
                        //  SystemConecto.NetworkPC[dani.Key + "_IP"]
                        // Номер потока PotokServer
                        // Передача параметров в виде структуры в другой поток
                        RenderInfo Arguments = new RenderInfo() { argument1 = SystemConecto.NetworkPC[dani.Key + "_IP"], argument2 = PotokServer };
                        threadsServer[PotokServer] = new Thread(LoadTimeServer);
                        threadsServer[PotokServer].SetApartmentState(ApartmentState.STA);
                        threadsServer[PotokServer].IsBackground = true; // Фоновый поток
                        threadsServer[PotokServer].Start(Arguments);
                            
                        // Количество потоков
                        PotokServer++;
                    }

                }
            }
            // Отладка итоговое количество
            // MessageBox.Show(PotokServer.ToString());

                


            //TcpListener listner = new TcpListener(new IPEndPoint(IPAddress.Parse("127.0.0.1"), 10013));
            //listner.Start();
            //while (true)
            //{
            //    // Application.DoEvents(); Отсутствует в WPF
                
            //    TcpClient client = listner.AcceptTcpClient();

            //    StreamReader sr = new StreamReader(client.GetStream());
            //    Console.WriteLine("Client : " + sr.ReadLine());

            //    StreamWriter sw = new StreamWriter(client.GetStream());
            //    sw.AutoFlush = true;
            //    Console.WriteLine("Server : Привет");
            //    sw.WriteLine("Привет");

            //    Console.WriteLine("Client : " + sr.ReadLine());
            //    Console.WriteLine("Server : Пока");
            //    sw.WriteLine("Пока");

            //    client.Close();
            //}



            return true;

        }

        #endregion

        #region Чтение настроек сервера
        /// <summary>
        /// Включение сервера
        /// </summary>
        /// <returns></returns>
        public static void ReadConfigTime()
        {


           // return true;

        }

        #endregion


        #region Запуск сервера 
        
        /// <summary>
        /// Структура данных для многопотоковой среды (передача аргументов)
        /// </summary>
        struct RenderInfo
        {
            public string argument1 { get; set; }
            public int    argument2 { get; set; }
            public string argument3 { get; set; }
            public Socket Socket { get; set; }
        }

        /// <summary>
        /// Включение сервера
        /// </summary>
        /// <returns></returns>
        public static void LoadTimeServer(object ThreadObj)
        {
            RenderInfo arguments = (RenderInfo)ThreadObj;
            // arguments.argument1   // Айпи адресс
            // arguments.argument2   // Номер потока

            // Установить TcpListener на порт 13000.
            /// Таблица портов http://ru.wikipedia.org/wiki/%D0%A1%D0%BF%D0%B8%D1%81%D0%BE%D0%BA_%D0%BF%D0%BE%D1%80%D1%82%D0%BE%D0%B2_TCP_%D0%B8_UDP

            Int32 port = 13;
            
            // Один поток, один сервер
            TcpListener server = null;
            int EndLoopServer = 0;

            try
            {

                IPAddress localAddr = IPAddress.Parse(arguments.argument1);

                // TcpListener server = new TcpListener(port);
                server = new TcpListener(localAddr, port);
                // Пример определения
                // server = new TcpListener(new IPEndPoint(IPAddress.Parse("127.0.0.1"), port));

                // Start listening for client requests.
                server.Start();

                // Buffer for reading data
                Byte[] bytes = new Byte[256];
                //String data = null;

                // Enter the listening loop.
                //for (EndLoopServer = 0; EndLoopServer < 2; EndLoopServer++)
                // Цикл while работает умнее по предворительным тестам
                while(true)
                {
                
                    EndLoopServer--;

                    // Отдельный поток в очереди ====== Пример 1
                    //TcpClient tcpClient = server.AcceptTcpClient();
                    //ThreadPool.QueueUserWorkItem(DoProcess, tcpClient);

                    // Отдельный поток ====== Пример 2
                    Socket sclientSocket = server.AcceptSocket();

                    RenderInfo Arguments = new RenderInfo() { Socket = sclientSocket };
                    Thread clientservice = new Thread(DoProcess);// new ThreadStart(DoProcess)если кто-то подцепился создаем для него поток и слушаем дальше порт
                    clientservice.SetApartmentState(ApartmentState.STA);
                    clientservice.Start(Arguments);




                    // Остановить прослушивание для новых клиентов при условии.
                    // server.Stop();

                    //проверка на наличие подключения клиента Использовать нельзя вешает процессор
                    //if (!server.Pending()) continue;

                    //инициализация потока для передачи данных по сети
                    //using (var net = _listener.AcceptTcpClient().GetStream())
                    //{...}



                }

            }
            catch (SocketException e)
            {
                SystemConecto.ErorDebag(e.ToString(), 0, 0);
            }
            finally
            {
                // Остановить прослушивание для новых клиентов.
                server.Stop();
            }

        }


        private static void DoProcess(object ThreadObj)
        {

            // Разбор аргументов
            RenderInfo arguments = (RenderInfo)ThreadObj;
            // arguments.Socket;   // запрос клиента
            
            //Слушаем сеть и устанавливаем соединение
            string message = "";
            UnicodeEncoding encoder = new UnicodeEncoding();
            byte[] buffer = encoder.GetBytes(message);
            arguments.Socket.Send(buffer, buffer.Length, 0);
            arguments.Socket.Close();
          
          


          //using (TcpClient tcpClient = (TcpClient) client)
          //using (NetworkStream stream = tcpClient.GetStream())
          //{
          //      // ...
          //}
        }


        #endregion


        #region Установка времени устройства для синхронизации времени с сервером ConectoWorkSpace-Time или сервером в интернете.

        // Структура
        public struct SystemTime
        {
            public ushort Year;
            public ushort Month;
            public ushort DayOfWeek;
            public ushort Day;
            public ushort Hour;
            public ushort Minute;
            public ushort Second;
            public ushort Millisecond;
        };

        [DllImport("kernel32.dll", EntryPoint = "GetSystemTime", SetLastError = true)]
        public extern static void Win32GetSystemTime(ref SystemTime sysTime);

        [DllImport("kernel32.dll", EntryPoint = "SetSystemTime", SetLastError = true)]
        public extern static bool Win32SetSystemTime(ref SystemTime sysTime);

        /// <summary>
        /// Установка времени в ОС Windows
        /// </summary>
        /// <param name="NewDateTime"></param>
        public static void SetLocalTime(DateTime NewDateTime)
        {

            SystemTime updatedTime = new SystemTime();
            updatedTime.Year = (ushort)NewDateTime.Year;     // 2011;//Год
            updatedTime.Month = (ushort)NewDateTime.Month;  // 7;   //Минуты
            updatedTime.Day = (ushort)NewDateTime.Day;       // 11;  //День

            updatedTime.Hour = (ushort)NewDateTime.Hour;     // 15;  //Час
            updatedTime.Minute = (ushort)NewDateTime.Minute; //  0;//Минуты
            updatedTime.Second = (ushort)NewDateTime.Second; // 0;//Секунды

           // MessageBox.Show(NewDateTime.ToString());

            // Установка времени на устройстве
            Win32SetSystemTime(ref updatedTime);
        }


        #endregion

        /// <summary>
        /// Клиент синхронизации устройства<para></para>
        /// aParamApp["Time_IP"] - "0.0.0.0" - отключен
        /// </summary>
        public static void ClientTime()
        {
            IDChekTime = 1;

            // Проверка переменной включения синхронизации
            if (SystemConecto.aParamApp["Time_IP"] == "0.0.0.0")
            {
                // Синхронизация отключенна

            }
            else 
            {
                // Проверка системного времени на устростве с помощью API 
                // SystemTime currTime = new SystemTime();
                // Win32GetSystemTime(ref currTime);
                // Проверка системного времени на устростве с помощью .Net
                DateTime TimeLocal = new DateTime();
                TimeLocal = DateTime.Now;
                
                
                // Обращаемся к локальному серверу если он в сети
                // SystemConecto.aParamApp["Time_IP"]
                if(1==2){
                    // сервер в локальной сети доступен &&
                    //if (SystemConecto.TickMemory_a[1] > 0 )
                    //{


                    //}
                }
                else
                {
                    // Проверка подключения к Интернету
                    if (AppStart.TickMemory_a[1] == 1)
                    {
                        // Обращаемся к сети если есть

                        DateTime TimeNet = (string)AppStart.rkAppSetingAllUser.GetValue("SerchDateTimeNet") == "2" ? SerchDateTimeNet() : TimeLocal;
                        // Локальное время
                        DateTime TimeNetLocal = TimeNet;
                        TimeNetLocal = TimeNetLocal.ToLocalTime();

                        // Время отличается Вариант Точный
                        // DateTime.Compare(TimeLocal, TimeNIST) < 1

                        // Приблизительно равны
                        if (!RoughlyEquals(TimeNetLocal, TimeLocal))
                        {

                            SystemConecto.ErorDebag(string.Format(
                                " - Сервер NTP Server: Время сервера отличается {0} от мирового времени {1}", 
                                TimeLocal.ToString(), TimeNetLocal.ToString()), 3);

                            // Синхронизация если установленна по умолчанию Да
                            SetLocalTime(TimeNet);
                        }  
                        
                        // Авто выключение после выполнения проверки
                        AppStart.TickRg_a[6] = -7;
                    }
                }

            }
          

            IDChekTime = 0;
        }


        /// <summary>
        ///  Поиск информации о мировом времени для синхронизации устройства
        /// </summary>
        /// <param name="ServerNet">Сервер в локальной сети</param>
        /// <returns></returns>
        public static DateTime SerchDateTimeNet(string ServerNet="")
        {
            DateTime networkDateTime = new DateTime();

            // Время по умолчанию
            networkDateTime = DateTime.Now;

            if (ServerNet == "")
            {

                // Поиск в сети
                // Проверка системного времени в сети .Net
                networkDateTime = GetNISTDate(false);
                if (GetNISTDateRead)
                {
                    // данные прочитаны из интернета

                }
                else
                {
                    // Прочитать с SNTP сервера 
                    networkDateTime = GetSNTPConnect("time.windows.com");
                    /// ! Разработка !
                    // Список серверов
                    // http://www.makak.ru/2009/12/01/spisok-sntp-serverov-vremeni-simple-network-time-protocol-dostupnykh-v-internete/

                }


            }
            else 
            { 
                // Поиск в локальной сети
            
            }
            



            return networkDateTime;
        }



        #region Синхронизация времени через интернет бесплатных сервисов
        // В идеале нужен свой сервис Conecto NiST-Server в интернете

        public static int IndexServer = 0;
        public static bool GetNISTDateRead = false;

        public static DateTime GetNISTDate(bool convertToLocalTime=false)
        {
            Random ran = new Random(DateTime.Now.Millisecond);
            DateTime date = DateTime.Now;
            string serverResponse = string.Empty;

            // Представляет список NIST серверов IP - адресса не используем но можно создать кеш для ускорения работы системы
            //string[] servers = new string[] {
            //             "96.47.67.105",
            //             "207.200.81.113",
            //             "64.113.32.5",
            //             "64.147.116.229",
            //             "64.250.229.100"
            //              };

            // Представляет список NIST серверов DNS - адресса
            // http://tf.nist.gov/tf-cgi/servers.cgi

            //string[] serversDNS = new string[] {
            //             "utcnist2.colorado.edu",
            //             "ntp-nist.ldsbc.edu",
            //             "nist1-lv.ustiming.org",
            //             "nist-time-server.eoni.com",
            //             "nist1.aol-ca.symmetricom.com",
            //             "nist1.symmetricom.com",
            //             "nist1-sj.ustiming.org",
            //             "utcnist.colorado.edu"

            //              };
            string[] serversDNS = new string[] {
                        "utcnist2.colorado.edu",
                         "ntp-nist.ldsbc.edu",
                         "nist1-lv.ustiming.org"
                         //"vega.cbk.poznan.pl",
                         //"Time2.Stupi.SE",
                         //"time.ien.it",
                         //"swisstime.ethz.ch",
                         //"tempo.cstv.to.cnr.it",
                         //"ntp0.fau.de"
 

                          };
            // "vega.cbk.poznan.pl" // для отладки SNTP сервер

            // Попробуйте каждый сервер в случайном порядке, чтобы избежать их бана из-за слишком частого запроса
            // Запросы не более чем 4 - 10 секунд - PauseNet
            int PauseNet = 0;
            for (int i = 0; i < 9; i++)
            {
                // Пауза опроса серверов
                if (PauseNet > 0)
                {
                    Thread.Sleep(PauseNet);
                    PauseNet = 0;
                }

                // Ip использовать статические адреса -  servers[ran.Next(0, servers.Length)], 13).GetStream()
                // Использовать случайное число нельзя так как можно нарушить вариант условия Запросы не более чем 4 - 10 секунд

                var ServerNIST = serversDNS[ran.Next(0, serversDNS.Length)];
                // Для отладки не годится
                //var ServerNIST = serversDNS[IndexServer];
                IndexServer = i == serversDNS.Length ? 0 : i + 1;
                // Бесконечный цикл пока не получим ответ
                i = i == serversDNS.Length ? 0 : i;
                // Включить паузу
                PauseNet = i == serversDNS.Length ? 7 : 0;

                try
                {

                    var addresses = Dns.GetHostEntry(ServerNIST).AddressList;


                    // Открыть StreamReader к случайному серверу времени используя статику или Доменное имя 13 123
                    StreamReader reader = new StreamReader(new System.Net.Sockets.TcpClient(addresses[0].ToString(), 13).GetStream());
                    serverResponse = reader.ReadToEnd();
                    reader.Close();

                    // Убеждаемся, что signiture есть
                    if (serverResponse.Length > 47 && serverResponse.Substring(38, 9).Equals("UTC(NIST)"))
                    {
                        // Parse the date
                        int jd = int.Parse(serverResponse.Substring(1, 5));
                        int yr = int.Parse(serverResponse.Substring(7, 2));
                        int mo = int.Parse(serverResponse.Substring(10, 2));
                        int dy = int.Parse(serverResponse.Substring(13, 2));
                        int hr = int.Parse(serverResponse.Substring(16, 2));
                        int mm = int.Parse(serverResponse.Substring(19, 2));
                        int sc = int.Parse(serverResponse.Substring(22, 2));

                        if (jd > 51544)
                        {
                            yr += 2000;
                        }
                        else
                        {
                            yr += 1999;
                        }
                        date = new DateTime(yr, mo, dy, hr, mm, sc);

                        SystemConecto.ErorDebag(string.Format(" - NTP Сервер Имя в списке {0} передал данные", ServerNIST),0);

                        // Конвертируйте его в текущем часовом поясе при желании
                        if (convertToLocalTime)
                        {
                            // Преобразование времени из одного часового пояса в другой. Net
                            //DateTime dlocal = DateTime.Now;
                            //DateTime dNonLocal = TimeZoneInfo.ConvertTime(dlocal, TimeZoneInfo.FindSystemTimeZoneById("US Mountain Standard Time"));
                            date = date.ToLocalTime();
                        }

                        // Выходим из цикла
                        GetNISTDateRead = true;
                        return date;
                    }

                }
                catch (System.Net.Sockets.SocketException eNet)
                {

                    // ! === Можно сделать отладку большого файла лога и БД если включить запись сообщений

                    // Отключен DNS со временем можно поставить отладку в системе на определение данной ошибки и ее устранение
                    if (eNet.ErrorCode == 11001 || eNet.ErrorCode == 11004)
                    {
                        Thread.Sleep(10000);

                    }
                    else
                    {
                        ErrorServer = string.Format(" - Сервер NTP Server: Имя в списке {0}, код ошибки {2}, сообщение - {1}", ServerNIST, eNet.ToString(), eNet.ErrorCode.ToString());
                        SystemConecto.ErorDebag(ErrorServer, 0);
                    }


                }
                catch (Exception ex)
                {
                    /* Ничего не cделано ... попробуем следующий сервер */
                    ErrorServer = string.Format(" - Сервер NTP Server: Имя в списке {0}, сообщение - {1}", ServerNIST, ex.ToString());

                    // Определить тип ошибки
                    // SystemConecto.ErorDebag(ex.GetType().ToString() , 2);

                    // Пример детелизации ошибок
                    if (ex.Data != null)
                    {
                        foreach (DictionaryEntry de in ex.Data)
                        {
                            //SystemConecto.ErorDebag(string.Format("    The key is '{0}' and the value is: {1}", 
                            //                                de.Key, de.Value), 2);

                        }
                    }


                    SystemConecto.ErorDebag(ErrorServer, 0);
                }
            }

            return date;
        }

        #endregion


        #region Система определения времени через SNTP сервис (default Windows time server) крайний случай


        /// <summary>
        /// Система определения времени через Windows сервис (default Windows time server) крайний случай
        /// </summary>
        /// <returns></returns>
        public static DateTime GetNetworkTimeWindows(string ntpServer="time.windows.com")
        {

            DateTime networkDateTime = new DateTime();

            // NTP размер сообщения - 16 байт дайджеста (стандарт - RFC 2030)
            var ntpData = new byte[48];

            //Установка Leap Indicator, номер версии и режим значения
            ntpData[0] = 0x1B; //LI = 0 (no warning без предупреждения), VN = 3 (IPv4 only), Mode = 3 (Client Mode)

            var addresses = Dns.GetHostEntry(ntpServer).AddressList;

            // UDP порт, назначенный NTP составляет 123
            var ipEndPoint = new IPEndPoint(addresses[0], 123);
            
            
            try
            {

                // NTP использует UDP
                var socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);

                socket.Connect(ipEndPoint);

                socket.Send(ntpData);
                socket.Receive(ntpData); // не работает на 64 битках
                socket.Close();

                //Offset to get to the "Transmit Timestamp" field (time at which the reply 
                //departed the server for the client, in 64-bit timestamp format."
                // Смещение, чтобы добраться до "Transmit Timestamp" поля (время, в которое ответить
                // отправились на сервере для клиента, в 64-битном формате метку ".
                const byte serverReplyTime = 40;

                //Получить секунды части
                ulong intPart = BitConverter.ToUInt32(ntpData, serverReplyTime);

                //Получить секунд фракции
                ulong fractPart = BitConverter.ToUInt32(ntpData, serverReplyTime + 4);

                //Преобразование от старшего к младшему по прямой порядок байтов
                intPart = SwapEndianness(intPart);
                fractPart = SwapEndianness(fractPart);

                double milliseconds = (intPart * 1000) + ((fractPart * 1000) / 0x100000000L);

                //**UTC** time
                networkDateTime = UnixTime_MillisecondstoDateTime(milliseconds);

                // =================================== Альтернатива
                //ulong intpart = 0;
                //ulong fractpart = 0;

                //for (int i = 0; i <= 3; i++)
                //    intpart = 256 * intpart + ntpData[offsetTransmitTime + i];

                //for (int i = 4; i <= 7; i++)
                //    fractpart = 256 * fractpart + ntpData[offsetTransmitTime + i];

                //ulong milliseconds = (intpart * 1000 + (fractpart * 1000) / 0x100000000L);
                //s.Close();

                //TimeSpan timeSpan = TimeSpan.FromTicks((long)milliseconds * TimeSpan.TicksPerMillisecond);

                //DateTime dateTime = new DateTime(1900, 1, 1);
                //dateTime += timeSpan;

                //TimeSpan offsetAmount = TimeZone.CurrentTimeZone.GetUtcOffset(dateTime);
                //DateTime networkDateTime = (dateTime + offsetAmount);

            }

            catch (SocketException sx)
            {
                ErrorServer = " - Сервер NTP Server: " + sx.ToString();
                SystemConecto.ErorDebag(ErrorServer, 0);
 
            }

            return networkDateTime;
        }

        

        #endregion

        /// <summary>
        /// RoughlyEquals - примерно равно
        /// как сравнивать приблизительно равные значения DateTime, допуская небольшую погрешность при объявлении их "равными"
        /// </summary>
        /// <param name="time"></param>
        /// <param name="timeWithWindow"></param>
        /// <param name="windowInSeconds"></param>
        /// <param name="frequencyInSeconds">% погрешности в секундах (по умолчанию)</param>
        /// <returns></returns>
        static bool RoughlyEquals(DateTime time, DateTime timeWithWindow, int frequencyInSeconds=70, int windowInSeconds=25)
        {
            // Находим интервал времени и 
            long delta = (long)((TimeSpan)(timeWithWindow - time)).TotalSeconds + 0; //   % frequencyInSeconds
            int deltaPr = Convert.ToInt32(delta)/ 60 * 100 ;     

           // delta = delta > windowInSeconds ? frequencyInSeconds - delta : delta;

            return frequencyInSeconds > (deltaPr < 0 ? deltaPr * -1 : deltaPr);// Math.Abs(delta) < windowInSeconds;
        }

        /// <summary>
        /// Преобразование DateTime в UnixTime в C#
        /// </summary>
        /// <param name="timestamp"></param>
        /// <returns></returns>
        public static double DateTimetoUnixTime(DateTime DateTime_)
        {
            return Math.Floor((DateTime_ - new DateTime(1970, 1, 1, 0, 0, 0, 0)).TotalSeconds);
            
            // http://aione.ru/unix-timestamp-v-csharp-primeryi-konvertatsii/
        }

        /// <summary>
        /// Преобразование UnixTime в DateTime в C#
        /// </summary>
        /// <param name="timestamp"></param>
        /// <returns></returns>
        public static DateTime UnixTimetoDateTime(double timestamp)
        {
            return (new DateTime(1970, 1, 1, 0, 0, 0, 0)).AddSeconds(timestamp);
        }

        /// <summary>
        /// Преобразование  UnixTime в DateTime  в C# с помощью милисекунд
        /// </summary>
        /// <param name="timestamp"></param>
        /// <returns></returns>
        public static DateTime UnixTime_MillisecondstoDateTime(double Milliseconds)
        {
            return (new DateTime(1970, 1, 1, 0, 0, 0, 0)).AddMilliseconds((long)Milliseconds);
        }

        /// <summary>
        /// Преобразование DateTime в UnixTime в C# с помощью милисекунд
        /// </summary>
        /// <param name="timestamp"></param>
        /// <returns></returns>
        public static double DateTimetoUnixTime_Milliseconds(DateTime DateTime_)
        {
            return (double)(DateTime_ - new DateTime(1970, 1, 1, 0, 0, 0, 0)).TotalMilliseconds;
        }


        // stackoverflow.com/a/3294698/162671
        static uint SwapEndianness(ulong x)
        {
            return (uint)(((x & 0x000000ff) << 24) +
                           ((x & 0x0000ff00) << 8) +
                           ((x & 0x00ff0000) >> 8) +
                           ((x & 0xff000000) >> 24));
        }



        #region SNTPClient класс C # предназначен для подключения к серверам времени в Интернете
        /// SNTPClient класс C # предназначен для подключения к серверам времени в Интернете и
        /// Получить текущую дату и время. При необходимости, он может обновлять время в локальной системе.
        /// Реализация протокола основан на RFC 2030.

        #region переменные, структуры

        // SNTP данных Длина структуры (NTP размер сообщения - 16 байт дайджеста)
        private const byte SNTPDataLength = 48;
        // SNTP Data Structure (как описано в RFC 2030)
        static byte[] SNTPData = new byte[SNTPDataLength];

        // Смещение константы для отметки времени в структуре данных
        private const byte offReferenceID = 12;
        private const byte offReferenceTimestamp = 16;
        private const byte offOriginateTimestamp = 24;
        private const byte offReceiveTimestamp = 32;
        private const byte offTransmitTimestamp = 40;


        //Mode field values
        public enum _Mode
        {
            SymmetricActive,	// 1 - Symmetric active
            SymmetricPassive,	// 2 - Symmetric pasive
            Client,				// 3 - Клиент
            Server,				// 4 - Сервер
            Broadcast,			// 5 - передача
            Unknown				// 0, 6, 7 - зарезервированны
        }


        // Mode
        public static _Mode Mode
        {
            get
            {
                // Изолировать биты 0 - 3
                byte val = (byte)(SNTPData[0] & 0x7);
                switch (val)
                {
                    case 0: goto default;
                    case 6: goto default;
                    case 7: goto default;
                    default:
                        return _Mode.Unknown;
                    case 1:
                        return _Mode.SymmetricActive;
                    case 2:
                        return _Mode.SymmetricPassive;
                    case 3:
                        return _Mode.Client;
                    case 4:
                        return _Mode.Server;
                    case 5:
                        return _Mode.Broadcast;
                }
            }
        }

        #endregion

        #region Подключение к серверу времени и обновлять системное время

        // Подключение клиента или сервера N уровня к серверу времени
        public static DateTime GetSNTPConnect(string ServerSNTP)
        {
            DateTime networkDateTime = new DateTime();
            try
            {
         
                // Адрес сервера
                var addresses = Dns.GetHostEntry(ServerSNTP).AddressList;
                // UDP порт, назначенный NTP составляет 123
                IPEndPoint EPhost = new IPEndPoint(addresses[0], 123);

                // NTP использует UDP
                // var socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
                // socket.Connect(EPhost);
                // socket.Send(ntpData);
                // socket.Receive(SNTPData); // иногда глючит на 64 битках ОС
                // socket.Close();


                //Подключить сервер времени
                UdpClient TimeSocket = new UdpClient();
                TimeSocket.Connect(EPhost);

                // Инициализация структуры данных. Устанавливает структуру данных для подключения.
                Initialize();
                TimeSocket.Send(SNTPData, SNTPData.Length);
                SNTPData = TimeSocket.Receive(ref EPhost);
                if (!IsResponseValid())
                {
                    // Вызов модуля исключения
                    throw new Exception(" Неверный ответ от NTP Server - " + ServerSNTP);
                }
                //Получить секунды части
                ulong intPart = BitConverter.ToUInt32(SNTPData, offTransmitTimestamp);

                //Получить секунд фракции
                ulong fractPart = BitConverter.ToUInt32(SNTPData, offTransmitTimestamp + 4);

                //Преобразование от старшего к младшему по прямой порядок байтов
                intPart = SwapEndianness(intPart);
                fractPart = SwapEndianness(fractPart);

                double milliseconds = (intPart * 1000) + ((fractPart * 1000) / 0x100000000L);

                //**UTC** time
                networkDateTime = UnixTime_MillisecondstoDateTime(milliseconds);

            }
            catch (SocketException e)
            {
                ErrorServer = string.Format(" Ошибка соединения с NTP Server - {0}, сообщение: {1} ", ServerSNTP, e.ToString());
                SystemConecto.ErorDebag(ErrorServer, 0);
                networkDateTime = DateTime.Now;
            }
            return networkDateTime;
        }

        #endregion


        // Инициализация данных NTP-клиент
        private static void Initialize()
        {
            // Установить номер версии 4 и режим для 3 (client)
            //Установка Leap Indicator, номер версии и режим значения
            //LI = 0 (no warning без предупреждения), VN = 3 (IPv4 only), Mode = 3 (Client Mode)
            SNTPData[0] = 0x1B;
            // Инициализация всех других областях с 0
            for (int i = 1; i < 48; i++)
            {
                SNTPData[i] = 0;
            }
            // Инициализация передачи метку
            // TransmitTimestamp = DateTime.Now;
        }


        // Проверьте, является ли ответ от сервера, действительным
        public static bool IsResponseValid()
        {
            if (SNTPData.Length < SNTPDataLength || Mode != _Mode.Server)
            {
                return false;
            }
            else
            {

                return true;
            }
        }



        #endregion



        public enum Month_
        {
            January = 1, // январь
            February = 2, // февраль
            March = 3, // март
            April = 4, //апрель
            May = 5, //май
            June = 6, //июнь
            July = 7, //июль
            August = 8, //август
            September = 9, //September
            October = 10, //октябрь
            November = 11, //ноябрь
            December =12 //декабрь
        }

        /// <summary>
        /// Название месяца на русском
        /// </summary>
        /// <param name="NameMonthEng"></param>
        /// <returns></returns>
        public static string GetNameMonthRu(Month_ NameMonthEng)
        {

                switch (NameMonthEng)
                {
                    case Month_.January: 
                        return "январь";
                    case Month_.February:
                        return "февраль";
                    case Month_.March:
                        return "март";
                    case Month_.April:
                        return "апрель";
                    case Month_.May:
                        return "май";
                    case Month_.June:
                        return "июнь";
                    case Month_.July:
                        return "июль";
                    case Month_.August:
                        return "август";
                    case Month_.September:
                        return "September";
                    case Month_.October:
                        return "октябрь";
                    case Month_.November:
                        return "ноябрь";
                    case Month_.December:
                        return "декабрь";
                }

                return "";    
        }

        /// <summary>
        /// Название месяца на русском
        /// </summary>
        /// <param name="NameMonthEng"></param>
        /// <returns></returns>
        public static string GetNameMonthRu_Int(int Monthint, string zakin = "я")
        {

            switch (Monthint)
            {
                case 1:
                    return "январ" + (zakin == "" ? "ь" : zakin);
                case 2:
                    return "феврал" + (zakin == "" ? "ь" : zakin);
                case 3:
                    return "март" + (zakin == "я" ? "а" : zakin);
                case 4:
                    return "апрел" + (zakin == "" ? "ь" : zakin);
                case 5:
                    return "май" + (zakin == "" ? "" : zakin);
                case 6:
                    return "июн" + (zakin == "" ? "ь" : zakin);
                case 7:
                    return "июл" + (zakin == "" ? "ь" : zakin);
                case 8:
                    return "август" + (zakin == "я" ? "а" : zakin);
                case 9:
                    return "сентябр" + (zakin == "" ? "ь" : zakin);
                case 10:
                    return "октябр" + (zakin == "" ? "ь" : zakin);
                case 11:
                    return "ноябр" + (zakin == "" ? "ь" : zakin);
                case 12:
                    return "декабр" + (zakin == "" ? "ь" : zakin);
            }

            return "";
        }

        /// <summary>
        /// Название дня недели на русском
        /// </summary>
        /// <param name="NameMonthEng"></param>
        /// <returns></returns>
        public static string GetNameDayOfWeekRu_Int(string DayOfWeekInt)
        {

            switch (DayOfWeekInt)
            {
                case "Monday":
                    return "Понедельник";
                case "Tuesday":
                    return "Вторник";
                case "Thursday":
                    return "Четверг";
                case "Wednesday":
                    return "Среда";
                case "Friday":
                    return "Пятница";
                case "Saturday":
                    return "Суббота";
                case "Sunday":
                    return "Воскресенье";
            }

            return "";
        }

//        Monday	Понедельник	'mʌndei
//Tuesday	Вторник	'tju:zdei
//Wednesday	Среда	'wenzdei
//Thursday	Четверг	'θə:zdei
//Friday	Пятница	'fraidei
//Saturday	Суббота	'sætədei
//Sunday	Воскресенье	'sʌndei






    }
}
