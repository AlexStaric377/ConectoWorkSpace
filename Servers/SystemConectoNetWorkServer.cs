using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
// Отладка Messagebox
//using System.Windows;
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
    class SystemConectoNetWorkServer
    {
        // Сетевой сервер обработка сетевых протоколов (служб ConectoWorkSpace)

        // Варианты WCF служба windows
        
        // UDP (англ. User Datagram Protocol — протокол пользовательских датаграмм) — это транспортный протокол для передачи данных в сетях IP без установления соединения. 
        // Он является одним из самых простых протоколов транспортного уровня модели OSI. Его IP-идентификатор — 0x11.
        // В отличие от TCP, UDP не гарантирует доставку пакета, поэтому аббревиатуру иногда расшифровывают как Unreliable Datagram Protocol (протокол ненадёжных датаграмм). 
        // Это позволяет ему гораздо быстрее и эффективнее доставлять данные для приложений, которым требуется большая пропускная способность линий связи, либо требуется малое время доставки данных. 
        
        // TCP — это транспортный механизм, предоставляющий поток данных, с предварительной установкой соединения, за счёт этого дающий уверенность в достоверности получаемых данных, 
        // осуществляет повторный запрос данных в случае потери данных и устраняет дублирование при получении двух копий одного пакета (см. также T/TCP). В отличие от UDP, гарантирует, что приложение получит данные точно в такой же последовательности, в какой они были отправлены, и без потерь.
        // Реализация TCP, как правило, встроена в ядро системы, хотя есть и реализации TCP в контексте приложения.
        // Когда осуществляется передача от компьютера к компьютеру через Internet, TCP работает на верхнем уровне между двумя конечными системами, например, интернет-браузер и интернет-сервер. Также TCP осуществляет надежную передачу потока байт от одной программы на некотором компьютере в другую программу на другом компьютере.
        // Программы для электронной почты и обмена файлами используют TCP. TCP контролирует длину сообщения, скорость обмена сообщениями, сетевой трафик 
        
        /// <summary>
        /// Состояние сервера. Запущен ли он = true
        /// </summary>
        public static bool ServerStart = false;

        /// <summary>
        /// Последняя ошибка в сервере ... (разработка)
        /// </summary>
        public static string ErrorServer = "";

        /// <summary>
        /// Название протокола кторый слушаем, порт на котором слушаем 
        /// </summary>
        public static Dictionary<string, Int32> ListProtokol = new Dictionary<string, Int32>();

        /// <summary>
        /// Клиент, возвращённый слушателем _listenerClient
        /// </summary>
        private static TcpClient _ClientBackServer;

        /// <summary>
        /// Ограничено количеством сетевых адаптеров 256 сетей
        /// Поток, который слушает подключенного клиента и обрабатывает передачу данных
        /// </summary>
        public static Thread[] threadsServer = new Thread[256];
        //private Thread _acceptThread;
        
        /// <summary>
        /// Слушатель входящих соединений Сервер
        /// </summary>
        private static TcpListener _listenerServer;

        /// <summary>
        /// Сокет входящих соединений Сервер Вариант 3
        /// </summary>
        private static Socket _serverSocket;

        private class ConnectionInfo
        {
            public Socket Socket;
            public Thread Thread;
        }

        private static List<ConnectionInfo> _connections =
            new List<ConnectionInfo>();



        #region Запуск NetWorkServer сервера

        /// <summary>
        /// Запуск NetWorkServer сервера<para></para>
        /// AutoServer - Авто старт сервера зависит от настроек сервера<para></para>
        /// StartServer - Принудительный старт сервера игнорирование aParamApp["Autorize_Admin-Conecto"]<para></para>
        /// StopServer - разработка<para></para>
        /// RezetServer - разработка<para></para>
        /// </summary>
        /// <param name="Command"></param>
        public static void NetWorkServer(string Command = null)
        {
            // Загрузка настроек сервера
            ReadConfigServer();
            
            switch (Command)
            {
                case "AutoServer":
                    // Авто старт сервера зависит от настроек сервера
                    //if (SystemConecto.aParamApp["Time_ServerSwitch"] == "1" && !ServerStart)
                    //{

                        // ServerStart
                        // Поиск и иницианализация устройств 
                        if (InitializeServer())
                        {
                            ServerStart = true;
                        }

                    //}
                    break;

                case "StartServer":

                    // Принудительный старт сервера игнорирование aParamApp["Autorize_Admin-Conecto"]

                    if (!ServerStart)
                    {

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
                        // Остановить прослушивание для новых клиентов.
                        _listenerServer.Stop();
                    }

                    break;
                case "RezetServer":

                    // Принудительная перезагрузка сервера 


                    break;
            }

        }

        #endregion

        #region Иницианализация сервера

       

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
            // альтернатива выбрать список всех адресов, а также только с указанного адреса
            // не безопасно!
            IPAddress listenedIp = IPAddress.Any;
            
            // Списк включенных протоколов пример: TCP-MOBILE-UNIVERSAL-I
            foreach (KeyValuePair<string, Int32> listenedProtocol in ListProtokol)
            {


                IPEndPoint peer = new IPEndPoint(listenedIp, listenedProtocol.Value);

                //--------------------------------------------------------- Вариант 3

                // Получаем информацию о локальном компьютере
                //IPHostEntry localMachineInfo =
                //    Dns.GetHostEntry(Dns.GetHostName());
                //IPEndPoint peer = new IPEndPoint(
                //   localMachineInfo.AddressList[0], listenedProtocol.Value);

                // Создаем сокет, привязываем его к адресу
                // и начинаем прослушивание
                //_serverSocket = new Socket(
                //    peer.Address.AddressFamily,
                //    SocketType.Stream, ProtocolType.Tcp);
                //_serverSocket.Bind(peer);
                //_serverSocket.Listen((int)
                //    SocketOptionName.MaxConnections);

                //threadsServer[PotokServer] = new Thread(AcceptConnections);
                //threadsServer[PotokServer].IsBackground = true;
                //threadsServer[PotokServer].Start();


                //--------------------------------------------------------- Вариант 2

                //Описание потока
                threadsServer[PotokServer] = new Thread(() => Listen(peer));
                //Поток фоновый (при завершении программы останавливается)
                threadsServer[PotokServer].IsBackground = true;
                //Инициализация прослушивателя TCP
                _listenerServer = new TcpListener(peer);
                _listenerServer.Start();
                ErorDebag("Инициализация прослушивателя TCP", 3);
                //Старт потока
                threadsServer[PotokServer].Start();

                //--------------------------------------------------------- Вариант 1
                //var IPDevice = SystemConecto.IP_DeviceCcurent(true);
                //foreach (KeyValuePair<string, string> dani in IPDevice)
                //{
                //    // Проверка подключения адаптера к сети
                //    if (SystemConecto.NetworkPC[dani.Key + "_STATUS"] == "Up")
                //    {
                //        // Включить только на сетевых адаптерах примеры  SystemConecto.NetOff () 
                //        // Loopback - нужно включить для некоторых протоколов для локального клиента && SystemConecto.DebagApp - режим отладки
                //        if (SystemConecto.NetworkPC[dani.Key + "_TypeInterf"] == "Ethernet" || (SystemConecto.NetworkPC[dani.Key + "_TypeInterf"] == "Loopback"))
                //        {


                //            // Запуск сервера параметры 
                //            //  SystemConecto.NetworkPC[dani.Key + "_IP"]
                //            // Номер потока PotokServer
                //            // Передача параметров в виде структуры в другой поток
                //            RenderInfo Arguments = new RenderInfo() { argument1 = SystemConecto.NetworkPC[dani.Key + "_IP"], argument2 = PotokServer };
                //            threadsServer[PotokServer] = new Thread(LoadTimeServer);
                //            threadsServer[PotokServer].SetApartmentState(ApartmentState.STA);
                //            threadsServer[PotokServer].IsBackground = true; // Фоновый поток
                //            threadsServer[PotokServer].Start(Arguments);

                //            // Количество потоков
                //            PotokServer++;
                //        }

                //    }
                //}
            }
            // Отладка итоговое количество
            // MessageBox.Show(PotokServer.ToString());
            return true;

        }

        #endregion

        #region Чтение настроек сервера
        /// <summary>
        /// Включение сервера
        /// </summary>
        /// <returns></returns>
        public static void ReadConfigServer()
        {
            // Конвертация порта из настроек
            // Int32 listenedPort = Convert.ToInt32(_viewModel.PeerPort);

            // netstat -a - проверка портов  netstat -a | find "LISTENING"

            // Занят 50560 - для сервера SQLMS 2008
            // 50700 - для мобильных устройств
            // Таблица портов http://ru.wikipedia.org/wiki/%D0%A1%D0%BF%D0%B8%D1%81%D0%BE%D0%BA_%D0%BF%D0%BE%D1%80%D1%82%D0%BE%D0%B2_TCP_%D0%B8_UDP
            // Int32 port = 13;

            ListProtokol.Add("TCP-MOBILE-UNIVERSAL-I", 50700);

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
            public int argument2 { get; set; }
            public string argument3 { get; set; }
            public Socket Socket { get; set; }
        }


        /// <summary>
        /// Поток, который слушает собеседника 
        /// </summary>
        /// <param name="vm"></param>
        /// <param name="peer"></param>
        private static void Listen(IPEndPoint peer)
        {

            //int requestCount = 0;
            //byte[] bytesFrom = new byte[10025];
            //string dataFromClient = null;
            //Byte[] sendBytes = null;
            //string serverResponse = null;
            //string rCount = null;
            //requestCount = 0;
            
            while (true)
            {
                try
                {
                
                    //Ожидание подключения TcpClient 
                    using (_ClientBackServer = _listenerServer.AcceptTcpClient())
                    {
                        //requestCount = requestCount + 1;

                        //Поток данных
                        using (NetworkStream ns = _ClientBackServer.GetStream())
                        {
                            ErorDebag("Подключился клиент: " + _ClientBackServer.Client.RemoteEndPoint, 3);
                            // Отправить сообщение для подключенного TcpServer.
                            
                            string MessageNoAvtorize = "pasword[пароль]: ";
                            // С помощью байтов проблема с кодировкой кирилицы
                            // Кодировки аписание команд GetEncoding http://msdn.microsoft.com/en-us/library/system.text.encodinginfo.getencoding%28VS.85%29.aspx
                            // кодировка для telnet (телнета) windows
                            Encoding cp866 = Encoding.GetEncoding("cp866");
                            Byte[] message = cp866.GetBytes(MessageNoAvtorize);
                            ns.Write(message, 0, message.Length);
                            
                            // С помощью текста
                            //using (StreamWriter sw = new StreamWriter(ns))
                            //{
                            //    sw.Write(MessageNoAvtorize);
                            //}

                            _ClientBackServer.Close();

                            #region Варианты разработок
                            // ------- Не работает
                            //ns.Read(bytesFrom, 0, (int)_ClientBackServer.ReceiveBufferSize == 0 ? 1 : (int)_ClientBackServer.ReceiveBufferSize);
                            //dataFromClient = System.Text.Encoding.ASCII.GetString(bytesFrom);
                            //dataFromClient = dataFromClient.Substring(0, dataFromClient.IndexOf("$"));
                            //ErorDebag(">> От клиента - "  + dataFromClient, 5);    // + clNo


                            //rCount = Convert.ToString(requestCount);
                            //serverResponse = "Server to clinet( Na) " + rCount;    // " + clNo + "
                            //sendBytes = Encoding.ASCII.GetBytes(serverResponse);
                            //ns.Write(sendBytes, 0, sendBytes.Length);
                            //ns.Flush();
                            //ErorDebag(">> " + serverResponse, 5);
                            #endregion


                            //Чтение из потока
                            //using (StreamReader sr = new StreamReader(ns))
                            //{
                                //String message = sr.ReadToEnd(); //Данные
                                //ErorDebag(message, 5);

                                #region разработка
                                //Взаимодействовать с ViewModel из отдельного потока
                                //нужно посредством Dispatcher
                                //Application.Current.Dispatcher.BeginInvoke(
                                //    new Action(() =>
                                //        vm.ChatOutput += FormOutput(peer, message)));

                                // StreamReader sr = new StreamReader(client.GetStream());
                                //    Console.WriteLine("Client : " + sr.ReadLine());

                                //    StreamWriter sw = new StreamWriter(client.GetStream());
                                //    sw.AutoFlush = true;
                                //    Console.WriteLine("Server : Привет");
                                //    sw.WriteLine("Привет");

                                //    Console.WriteLine("Client : " + sr.ReadLine());
                                //    Console.WriteLine("Server : Пока");
                                //    sw.WriteLine("Пока");

                                //----------------------------
                                // Buffer for reading data
                                // Byte[] bytes = new Byte[256];
                                //--------------------------------
                                // Отдельный поток ====== Пример 2
                                //Socket sclientSocket = _listenerServer.AcceptSocket();

                                //RenderInfo Arguments = new RenderInfo() { Socket = sclientSocket };
                                //Слушаем сеть и устанавливаем соединение
                                //string message = "";
                                //UnicodeEncoding encoder = new UnicodeEncoding();
                                //byte[] buffer = encoder.GetBytes(message);
                                //arguments.Socket.Send(buffer, buffer.Length, 0);
                                //arguments.Socket.Close();
                                #endregion

                            //}
                        }
                    }
                }
                catch (Exception e)
                {
                    ErorDebag(e.ToString(), 0);
                }
                finally
                {
                    
                }
            }
        }


        private static void AcceptConnections()
        {
            while (true)
            {
                // Принимаем соединение
                Socket socket = _serverSocket.Accept();
                ConnectionInfo connection = new ConnectionInfo();
                connection.Socket = socket;

                // Создаем поток для получения данных
                connection.Thread = new Thread(ProcessConnection);
                connection.Thread.IsBackground = true;
                connection.Thread.Start(connection);

                // Сохраняем сокет
                lock (_connections) _connections.Add(connection);
            }
        }

        private static void ProcessConnection(object state)
        {
            ConnectionInfo connection = (ConnectionInfo)state;
            byte[] buffer = new byte[255];
            try
            {
                while (true)
                {
                    int bytesRead = connection.Socket.Receive(
                        buffer);

                    if (bytesRead > 0)
                    {
                        lock (_connections)
                        {
                            foreach (ConnectionInfo conn in
                                _connections)
                            {
                                if (conn != connection)
                                {
                                    conn.Socket.Send(
                                        buffer, bytesRead,
                                        SocketFlags.None);
                                }
                            }
                        }
                    }
                    else if (bytesRead == 0) return;
                }
            }
            catch (SocketException exc)
            {
                ErorDebag("Socket exception: " + Environment.NewLine +
                        " === Message: " + exc.Message.ToString() + Environment.NewLine +
                        " === Exception: " + exc.ToString() + Environment.NewLine +
                        " === ErrorCode: " + exc.SocketErrorCode, 0);
            }
            catch (Exception exc)
            {
                SystemConecto.ErorDebag("Socket exception: " + Environment.NewLine +
                       " === Message: " + exc.Message.ToString() + Environment.NewLine +
                       " === Exception: " + exc.ToString(), 0);
            }
            finally
            {
                connection.Socket.Close();
                lock (_connections) _connections.Remove(
                    connection);
            }
        }


        #endregion


        #region Логирование сервера {v 1.6}

        /// <summary>
        /// Код, защищенный таким образом от неопределённости в плане многопотокового исполнения, называется потокобезопасным. 
        /// Все потоки при записи борются за блокировку объекта
        /// </summary>
        private static Object lockerLog = new Object();


    
        /// <summary>
        /// Логирование и вывод сообщений приложения<para></para>
        /// </summary>
        /// <param name="Message">Message: Текст сообщения.</param><para></para>
        /// <param name="TypeError">TypeError: Тип сообщений: <para></para> 0 - исключение;<para></para> 1 - отладка;<para></para> 
        /// 2 - исключение с выводом сообщения на экран;<para></para> 3 - информация.</param><para></para>
        public static void ErorDebag(string Message, int TypeError = 0)
        {
            // Не выводить сообщения при отключенной отладке
            // Откладка отключается при настройки логирования системы в администрировании
            if (!SystemConecto.DebagApp && TypeError == 1)
            {
                return;
            }

            Encoding win1251 = Encoding.GetEncoding("windows-1251");


            // запись в лог код; время; пользователь; тип сообщения; сообщения

            string text = Environment.NewLine;
            string textall = "";
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
                case 5:
                    text = text + "105;";
                    break;

            }
            // Здесь очень опасно вставлять не опереденное время!!! Синхронизировать но как?
            DateTime dateTime = DateTime.Now;
            text = text + dateTime.ToString("dd.MM.yyyy HH:mm:ss") + "; --- ";
            switch (TypeError)
            {
                case 0:
                case 2:
                    text = text + (SystemConecto.aLanguage.ContainsKey("Ошибка") ? SystemConecto.aLanguage["Ошибка"] : "Ошибка") + ": ;";
                    break;
                case 1:
                    text = text + (SystemConecto.aLanguage.ContainsKey("Отладка") ? SystemConecto.aLanguage["Отладка"] : "Отладка") + ": ;";
                    break;
                case 3:
                    text = text + (SystemConecto.aLanguage.ContainsKey("Информация") ? SystemConecto.aLanguage["Информация"] : "Информация") + ": ;";
                    break;
                case 5:
                    text = text + (SystemConecto.aLanguage.ContainsKey("Полученый_запрос") ? SystemConecto.aLanguage["Полученый_запрос"] : "Полученый запрос от клиента") + ": ;";
                    break;

            }
            text = text + Message;

            // Блокировка
            lock (lockerLog)
            {
                // Проверка структуры
                textall = (!(File.Exists(SystemConecto.PutchApp + "netserver.log")) ? "Id;Дата;Тип сообщения;Сообщение;" + Environment.NewLine : "") + text;

                // Отслеживание ошибок в досупе к файлу лога в многопотоковой среде 
                try
                {
                    using (StreamWriter FileSysLog = new StreamWriter(SystemConecto.PutchApp + "netserver.log", true, win1251))
                    {
                        FileSysLog.WriteLine(textall);
                        FileSysLog.Close();
                    }
                }
                catch //(Exception ex)
                {
                    // 1. Запись в БД локальную или центральную
                    // Пробуем записать в локальный лог 
                    string LocalLog = AppDomain.CurrentDomain.BaseDirectory + "local_sys_netserver.log";
                    textall = (!(File.Exists(LocalLog)) ? "Id;Дата;Тип сообщения;Сообщение;" + Environment.NewLine : "") + text;

                    try
                    {
                        using (StreamWriter FileSysLog = new StreamWriter(LocalLog, true, win1251))
                        {
                            FileSysLog.WriteLine(textall);
                            FileSysLog.Close();
                        }

                    }
                    catch //(Exception ex)
                    {


                    }
                }
            }
               
           

        }

        #endregion




    }
}
