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
// Управление Xml
using System.Xml;
using System.Xml.Linq;
// шифрование данных
using System.Security.Cryptography;
using System.Data;              // Содержит типы, независимые от провайдеров, например DataSet и DataTable.

// Сжатие
using System.IO.Compression;


namespace ConectoWorkSpace
{
    /// <summary>
    /// Интегрированный сервер для Караоке сервиса
    /// </summary>
    class KaraokeServer
    {
        #region Данные о сервере
        /// <summary>
        /// Состояние сервера. Запущен ли он = true
        /// </summary>
        public static bool ServerStart = false;

        /// <summary>
        /// Тип протокола, Номер порта на котором слушаем используем список так как можем слушать асинхронно несколько портов для предоставления раных сервисов
        /// </summary>
        private static Dictionary<string, string> ListProtokol = new Dictionary<string, string>();

        /// <summary>
        /// Поток, который слушает подключенного клиента и обрабатывает передачу данных
        /// Информация о соединении Сокета
        /// </summary>
        // public static List<Thread> threadsAdapters = new List<Thread>();
        private static List<InfoProtokol> threadsAdapters = new List<InfoProtokol>();


        /// <summary>
        /// ID композиции что звучит
        /// </summary>
        public static Int32 IDMuzicPlay = 0;

        /// <summary>
        /// Название композиции что звучит
        /// </summary>
        public static string NameMuzikPlay = "Название композиции, что звучит.";


        /// <summary>
        /// Название заведения где стоит модуль
        /// </summary>
        public static string NameKaraokeShop = "";

        /// <summary>
        /// Минимальное значение результата голосования которое учавствует в расчете голосования
        /// </summary>
        public static int VoteMinOn = 0;

        /// <summary>
        /// Результат голосования
        /// </summary>
        public static double resultVote = 3.0;

        /// <summary>
        /// Название лог файла
        /// </summary>
        public static string NameFileLog = "serv-karaoke.log";

        /// <summary>
        /// Информация о Сервере согласно протокола
        /// </summary>
        private class InfoProtokol
        {
            /// <summary>
            /// Перебор настроек установленных сетевых адаптеров
            /// </summary>
            public IPAddress listenedIp;
            public Int32 Port;
            /// <summary>
            /// Тип протокола TCP:  SYNH синхронный, ASYN асинхронный
            /// </summary>
            public string[] TypeProtokol;
            public IPEndPoint peer;
            /// <summary>
            /// Информация о соединении Сокета
            /// </summary>
            public Socket Socket;
            public Thread Thread;
            /// <summary>
            /// Количество соединений с сервером
            /// </summary>
            public Int32 IndexConection = 600;
            public Int32 index_threadsAdapters = 0;

            /// <summary>
            /// Протоколирование протокола общения с сервером
            /// </summary>
            public bool DebugInfo = false;

            /// <summary>
            /// Количество передаваемых строк устройству, сделано для настройки корректной передачи данных для более слабых устройств
            /// </summary>
            public Int32 CountRowDataSendDevice = 100;
        }

        #endregion


        #region стартовый метод сервера
        /// <summary>
        /// Включение сервера
        /// Иницианализация сервера <para></para>
        /// 1. Открывает сокет; 2. Назанчает сокет; 3. Слушает входящие соединения
        /// </summary>
        /// <returns></returns>
        public static bool StartKaraoke(string PutchFileConfig, Int32 IDProcess)
        {

            // Считываем доп параметры
            var ValuePort = ConfigControll.ReadParamID(PutchFileConfig, Properties.Resources.servers, IDProcess, new string[1] { "TCP-MOBILE-UNIVERSAL-I" });

            // Проверка необходимых протоколов
            if (ValuePort != null && ValuePort.Count > 0)
            {
                // Информационный порт выдает информацию без авторизации
                foreach (var Protokol in ValuePort)
                {
                    ListProtokol.Add(Protokol.Key, Protokol.Value);

                }

            }
            else
            {   
                // КРИТИЧЕСКАЯ ОШИБКА ЗАПУСКА
                return false;
            }


            // Проверка локальной БД  источники: каталог зборки в папке Conecto, FTP
            Dictionary<string, string> dataList = new Dictionary<string, string>();
            dataList.Add(SystemConectoServers.PutchServerData + @"karaoke.fdb", "");
            if (SystemConecto.IsFilesPRG(dataList, -1, "- Проверка БД при старте Karaoke-Server") != "True")
            {
                try
                {
                    if (!CreateBD_Table(SystemConectoServers.PutchServerData + @"karaoke.fdb"))
                        return false;
                    // var Test = SystemBD.CreateBDSystem();
                    // Проверка подключения
                }
                catch (Exception ex)
                {
                    // КРИТИЧЕСКАЯ ОШИБКА ЗАПУСКА
                    SystemConectoServers.ErorDebag(ex.ToString(), 0, NameFileLog);
                        return false;
                }
            }

            // Запрос к серверу Диагностика
            //DBConecto.UniQuery LocalQuery = new DBConecto.UniQuery("Select * from KARAOKE_CATALOG", "FB");

            //LocalQuery.InitialCatalog = SystemConectoServers.PutchServerData + @"karaoke.fdb";

            //var Table = new DataTable("KARAOKE_CATALOG");

            //LocalQuery.ExecuteQuery("ExecuteQueryLoadTableReader", ref Table); //ExecuteQueryLoadTable("KARAOKE_CATALOG");

            //DataTable Table_ = LocalQuery.ExecuteQueryLoadTable("KARAOKE_CATALOG", "Fill");

            ////DataTable Table_ = 

            //DataTable Tg = Table_;


            // ---------- Иницианализация сервера
            // 1. Открывает сокет

            // ---Устанавливаем для сокета локальную конечную точку
            // IPHostEntry ipHost = Dns.GetHostEntry("localhost");
            // IPHostEntry ipHostInfo = Dns.Resolve("host.contoso.com");
            // Если установить на порт 127.0.0.1, то я думаю, что слушать другие IP сетевых адаптеров
            // мы перестанем. Работа сервера будет локальной.
            // Предполагаю, что слушать нужно все адаптеры или настраивать маршрутизацию внутри Сервера
            // IPAddress ipAddr = Dns.GetHostEntry("localhost").AddressList[0];


            Dictionary<string, string> ValueTemp;
            // Дополнительные настройки
            NameKaraokeShop = ConfigControll.ReadParamIDValue(PutchFileConfig, Properties.Resources.servers, IDProcess, new string[1] { "NameKaraokeShop" });

            VoteMinOn = Convert.ToInt32(ConfigControll.ReadParamIDValue(PutchFileConfig, Properties.Resources.servers, IDProcess, new string[1] { "VoteMinOn" }));

            // Перебор настроек установленных сетевых адаптеров
            // альтернатива выбрать список всех адресов, а также только с указанного адреса
            // не безопасно!
            IPAddress listenedIp = IPAddress.Any;  // ipAddr.AddressFamily OR listenedIp.AddressFamily


            Int32 index_threadsAdapters = 0;



            // Активация включенных протоколов согласно списока, пример: TCP-MOBILE-UNIVERSAL-I
            foreach (KeyValuePair<string, string> listenedProtocol in ListProtokol)
            {

                InfoProtokol _InfoProtokol = new InfoProtokol();
                _InfoProtokol.DebugInfo = Convert.ToBoolean( ConfigControll.ReadParamIDValue(PutchFileConfig, Properties.Resources.servers, IDProcess, new string[1] { "DebugInfo" }) );
                _InfoProtokol.listenedIp = listenedIp;
                _InfoProtokol.Port = Convert.ToInt32(listenedProtocol.Value);
                //IPEndPoint ipEndPoint = new IPEndPoint(ipAddr, Convert.ToInt32(listenedProtocol.Value));

                _InfoProtokol.peer = new IPEndPoint(_InfoProtokol.listenedIp, _InfoProtokol.Port);

                _InfoProtokol.TypeProtokol = listenedProtocol.Key.Split('-');

                _InfoProtokol.index_threadsAdapters = index_threadsAdapters;

                // Порт занят или нет возможности его включить
                Socket StartSocket = SetupServerSocket(_InfoProtokol);
                if(StartSocket==null){
                    continue;
                }
                _InfoProtokol.Socket = StartSocket;

                ValueTemp = ConfigControll.ReadParamID(PutchFileConfig, Properties.Resources.servers, IDProcess, new string[1] { "CountRowDataSendDevice" });
                if(ValuePort != null && ValuePort.Count > 0)
                    _InfoProtokol.CountRowDataSendDevice = Convert.ToInt32(ValueTemp["CountRowDataSendDevice"]);

                // Создаем поток для получения данных
                _InfoProtokol.Thread = new Thread(() => Listen(_InfoProtokol));
                _InfoProtokol.Thread.IsBackground = true;
                _InfoProtokol.Thread.Start();


                // Сохраняем сокет
                lock (threadsAdapters) threadsAdapters.Add(_InfoProtokol);

 

                //Описание потока
                // 2. Назанчает сокет
                // 3. Слушает входящие соединения
                //ThreadsAdaptersControll(listenedIp, peer, TypeProtokol, IndexConection, index_threadsAdapters);
                ////Инициализация прослушивателя TCP
                //_listenerServer = new TcpListener(peer);
                //_listenerServer.Start();
                SystemConectoServers.ErorDebag("Инициализация прослушивателя - " + listenedProtocol.Key, 3, NameFileLog);
                index_threadsAdapters++;
                
                
              
            }


            return true;
        }

        #endregion



        #region методы управления включением сервера
        /// <summary>
        /// Включение сервера<para></para>
        /// StopServer - разработка<para></para>
        /// RezetServer - разработка<para></para>
        /// </summary>
        /// <returns></returns>
        public static void KaraokeControl(string Command = null)
        {

            switch (Command.ToLower())
            {
                case "startserver":

                    // Принудительный старт сервера игнорирование aParamApp["Autorize_Admin-Conecto"]

                    if (!ServerStart)
                    {
                        // Поиск и иницианализация устройств 
                        if (StartKaraoke(@"c:\Program Files\Conecto\Servers\servers.xml", 10))
                        {
                            ServerStart = true;
                        }
                    }


                    break;
                case "stopserver":

                    // Принудительная остоновка сервера 
                    if (ServerStart)
                    {
                        // Остановить прослушивание для новых клиентов.
                        foreach (var item in threadsAdapters)
                        {
                            // _listenerServer.Stop();
                            // Проверить поток на завершение, завершить прослушивание сервера
                            // item.Abort();
                        }
                        
                    }

                    break;
                case "rezetserver":

                    // Принудительная перезагрузка сервера 


                    break;
            }




        }

        #endregion


        #region Настройка сервера сокета
        /// <summary>
        /// Настройка сервера сокета
        /// </summary>
        private static Socket SetupServerSocket(InfoProtokol _InfoProtokol)
        {
            Socket _serverSocket = null;
            try
            {
               
                // Создаем сокет, привязываем его к адресу и начинаем прослушивание
                // Определение типа 
                switch (_InfoProtokol.TypeProtokol[0].ToUpper())
                {
                    case "TCP":

                        // 2. Назанчает сокет
                        // Создаем сокет Tcp/Ip
                        // Перечисление AddressFamily указывает схемы адресации, которые экземпляр класса Socket может использовать для разрешения адреса.
                        // _serverSocket = new Socket(_InfoProtokol.listenedIp.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
                        _serverSocket = new Socket(_InfoProtokol.peer.Address.AddressFamily, SocketType.Stream, ProtocolType.Tcp);

                        break;

                }

                // Следующим шагом должно быть назначение сокета с помощью метода Bind(). 
                // Когда сокет открывается конструктором, ему не назначается имя, а только резервируется дескриптор. 
                // Для назначения имени сокету сервера вызывается метод Bind(). Чтобы сокет клиента мог идентифицировать потоковый сокет TCP,
                // серверная программа должна дать имя своему сокету:
                //sListener.Bind(peer);
                // В параметре определяется задел (backlog), указывающий максимальное число соединений, ожидающих обработки в очереди.
                // В приведенном коде значение параметра допускает накопление в очереди до IndexConection соединений.
                //sListener.Listen(IndexConection); //10

            
                 _serverSocket.Bind(_InfoProtokol.peer);
                _serverSocket.Listen(_InfoProtokol.IndexConection); // Тут может возникнуть ошибка

            }catch(SocketException Sex){

                return null;
            }
            catch (Exception Ex)
            {
                return null;

            }
            //System.Net.Sockets.SocketException не обработано пользовательским кодом
            //  HResult=-2147467259
            //  Message=Обычно разрешается только одно использование адреса сокета (протокол/сетевой адрес/порт)
             


            //_serverSocket.Listen((int)SocketOptionName.MaxConnections);
            return _serverSocket;
        }

        #endregion

       
        #region Поток, который слушает собеседника

        /// <summary>
        /// Поток, который слушает собеседника 
        /// </summary>
        /// <param name="vm"></param>
        /// <param name="peer"></param>
        private static void Listen(InfoProtokol _InfoProtokol, int TypeStart = 0)
        {

            // TypeStart 0 - стартовая загрузка потока 1 - востоновление после сбоя



            byte[] Receivebuffer = new byte[1024];
            byte[] bytes = new byte[1024];
            List<Socket> readSockets = new List<Socket>();
            List<Socket> connectedSockets = new List<Socket>();

            // Назначаем сокет локальной конечной точке и слушаем входящие сокеты
            try
            {
                Double LiveTimeCache = 300; // секунд 5 минут
                Double TimeCacheQueryBD = 0;
                string CacheSend = "";

                connectedSockets.Add(_InfoProtokol.Socket);

                // Начинаем слушать соединения Синхронный сервер
                while (true)
                {

                    // кеш запросов
                    if (TimeCacheQueryBD < SystemConectoTimeServer.DateTimetoUnixTime(DateTime.Now))
                    {
                        TimeCacheQueryBD = LiveTimeCache + SystemConectoTimeServer.DateTimetoUnixTime(DateTime.Now);
                        CacheSend = GETStatusKaraoke();
                    }
                    
                    //if (_InfoProtokol.DebugInfo)
                    //    SystemConectoServers.ErorDebag("Ожидаем соединение через порт {" + _InfoProtokol.peer.ToString() + "}", 3, NameFileLog);


                    // Программа приостанавливается, ожидая входящее соединение
                    //using (Socket handler = sListener.Accept())
                    //{
                    //Socket handler = _InfoProtokol.Socket.Accept();
                    //Unix

                    string data = null;
                    

                    //IPAddress.Parse (((IPEndPoint)s.LocalEndPoint).Address.ToString ()) + "I am connected on port number " + ((IPEndPoint)s.LocalEndPoint).Port.ToString ()



                    // Заполняем список сокетов чтения
                    readSockets.Clear();
                    //readSockets.Add(_InfoProtokol.Socket);
                    readSockets.AddRange(connectedSockets);
                  
                    // Определяем статус сокетов (ожидаем события на сокетах: подключение клиента или передачи данных)
                    Socket.Select(readSockets, null, null, int.MaxValue);

                    // Обрабатываем каждый сокет, требующий
                    // каких-либо действий (клиент передал нам данные)
                    foreach (Socket readSocket in readSockets)
                    {

                        // Подключение на сокет conected
                        if (readSocket == _InfoProtokol.Socket)
                        {
                            // Создаем новый сокет и сохраняем его для обработки потока как одельный запрос
                             Socket handler = readSocket.Accept();  //Socket newSocket = readSocket.Accept();
                            
                             connectedSockets.Add(handler);
                        
                            
                            // Первое подключение 
                            if (_InfoProtokol.DebugInfo)
                                SystemConectoServers.ErorDebag("Подключился клиент: " + IPAddress.Parse(((IPEndPoint)handler.RemoteEndPoint).Address.ToString()) + "  номер порта:  " + ((IPEndPoint)handler.RemoteEndPoint).Port.ToString(), 3, NameFileLog);


                            string MessageSend_ = GETStatusKaraoke(CacheSend, TimeCacheQueryBD);
                            byte[] msg = Encoding.UTF8.GetBytes(MessageSend_);
                            handler.Send(msg, SocketFlags.None);
                            
                            if (_InfoProtokol.DebugInfo)
                                SystemConectoServers.ErorDebag("Отослали клиенту: " + MessageSend_, 3, NameFileLog);
                            // --------------------------------- Быстрый вывод информации
                            
                        
                        }
                        else
                        {
                            // Обработка протокола
                            try
                            {
                                // Читаем и обрабатываем данные
                                int bytesRead = readSocket.Receive(Receivebuffer);
                                if (0 == bytesRead)
                                {
                                    // Выход клиента
                                    connectedSockets.Remove(readSocket);
                                    readSocket.Shutdown(SocketShutdown.Both);
                                    readSocket.Close();
                                    
                                }
                                else
                                {

                                    //TreatmentProtocol


                                    data += Encoding.UTF8.GetString(Receivebuffer, 0, bytesRead);

                                    #region Разработка
                                    //data += Encoding.UTF32.GetString(bytes, 0, bytesRead);Default
                                    //ReadFromBuffer(buffer);

                                    //byte number = 240;
                                    //foreach (string format in formats)
                                    //    Console.WriteLine("'{0}' format specifier: {1}",
                                    //                      format, bytes.ToString(format));
                                    #endregion

                                    // Показываем данные в логе
                                    if (_InfoProtokol.DebugInfo)
                                        SystemConectoServers.ErorDebag("Полученный текст: {" + data + "}\n\n", 3, NameFileLog);

                                    // Сформированный ответ в виде масива байт
                                    byte[] bytesAllSend_ = new byte[0];

                                    // Обработка протокола
                                    string commandProtokol = TreatmentProtocol(data, IPAddress.Parse(((IPEndPoint)readSocket.RemoteEndPoint).Address.ToString()).ToString(), readSocket, _InfoProtokol.DebugInfo, ref bytesAllSend_);


                                    if (commandProtokol.IndexOf("<Send>") > -1)
                                    {

                                        // Отправляем ответ клиенту
                                        // string reply = data.Length.ToString();
                                        // Разммер контента
                                        int _LengthElemSend = bytesAllSend_.Length == 0 ? commandProtokol.Length : bytesAllSend_.Length;

                                        if (_InfoProtokol.DebugInfo)
                                        {

                                            if (_InfoProtokol.DebugInfo && _LengthElemSend > 10000)
                                            {
                                                SystemConectoServers.ErorDebag("Сервер направил большой ответ: " + _LengthElemSend.ToString() + "\n\n", 3, NameFileLog);

                                            }
                                            else
                                            {
                                                SystemConectoServers.ErorDebag("Сервер направил ответ: " + (bytesAllSend_.Length == 0 ? commandProtokol : 
                                                    "Подготовленный масив данных byte[]. " +
                                                    Environment.NewLine + Environment.NewLine + " Частичная расшифровка передачи данных: " + Environment.NewLine + Environment.NewLine +
                                                    commandProtokol) + "\n\n", 3, NameFileLog);
                                            }
                                        }
                                            //reply = "Спасибо за запрос в " + reply + " символов";


                                        //byte[]
                                        //msg 
                                        if(bytesAllSend_.Length == 0){
                                            bytesAllSend_=  Encoding.UTF8.GetBytes(commandProtokol);
                                        }

                                        if (_InfoProtokol.DebugInfo && _LengthElemSend > 10000)
                                        {
                                            SystemConectoServers.ErorDebag("Сервер конвертировал большой ответ в UTF - байтах: " + bytesAllSend_.Length.ToString() + "\n\n", 3, NameFileLog);
                                           // var msg_ = Encoding.ASCII.GetBytes(commandProtokol);
                                           // SystemConectoServers.ErorDebag("Сервер конвертировал большой ответ в ASCII - байтах: " + msg_.Length.ToString() + "\n\n", 3, NameFileLog);
                                        }


                                        readSocket.Send(bytesAllSend_);

                                        // Передать сообщение всем подключенным соединениям если это необходимо широковещательный ответ
                                        if (commandProtokol.IndexOf("<AllSend />") > -1)
                                        {
                                            foreach (Socket connectedSocket in connectedSockets)
                                            {
                                                if (connectedSocket != readSocket)
                                                {
                                                    connectedSocket.Send(bytesAllSend_, SocketFlags.None);
                                                    // connectedSocket.Send(msg, msg.Length, SocketFlags.None);
                                                    // connectedSocket.Send(Receivebuffer, bytesRead, SocketFlags.None);

                                                    // здесь образуется разрыв связки получил принял (ответ от клиента)
                                                }
                                            }

                                        }
                                      

                                    }
                                    // Завершить соединение
                                    if (commandProtokol.IndexOf("<Exit />") > -1 || commandProtokol.IndexOf("<Error />") > -1)
                                    {
                                        if (_InfoProtokol.DebugInfo)
                                            SystemConectoServers.ErorDebag(commandProtokol.IndexOf("<Exit />") > -1 ? "Сервер завершил соединение с клиентом. Команда Exit" :
                                                "Сервер завершил соединение с клиентом. Команда Error во время обработки протокола.", 0, NameFileLog);

                                        connectedSockets.Remove(readSocket);
                                        readSocket.Shutdown(SocketShutdown.Both);
                                        readSocket.Close();
                                    }
                                }

                            }
                            catch (System.Xml.XmlException excXml)
                            {
                                SystemConectoServers.ErorDebag("Ошибка передачи xml данных :  "  + Environment.NewLine +
                                        " === Message: " + excXml.Message.ToString() + Environment.NewLine +
                                        " === Exception: " + excXml.ToString() + Environment.NewLine, 0, NameFileLog);

                            }
                            catch (SocketException exc)
                            {

                                if (exc.NativeErrorCode.Equals(10054))
                                {
                                    if (_InfoProtokol.DebugInfo)
                                        SystemConectoServers.ErorDebag("Клиент отключился!", 0, NameFileLog);
                                    

                                    
                                }
                                else
                                {
                                    SystemConectoServers.ErorDebag("Socket exception: Клиент отключился: код ошибки " + exc.NativeErrorCode + Environment.NewLine +
                                        " === Message: " + exc.Message.ToString() + Environment.NewLine +
                                        " === Exception: " + exc.ToString() + Environment.NewLine +
                                        " === ErrorCode: " + exc.SocketErrorCode, 0, NameFileLog);

                                }

                                connectedSockets.Remove(readSocket);
                                readSocket.Shutdown(SocketShutdown.Both);
                                readSocket.Close();

                            }
                            catch (Exception exc)
                            {
                                SystemConectoServers.ErorDebag("Socket exception: " + Environment.NewLine +
                                        " === Message: " + exc.Message.ToString() + Environment.NewLine +
                                        " === Exception: " + exc.ToString(), 0, NameFileLog);
                            }
                            finally
                            {

                                if (connectedSockets.Count == 0)
                                {
                                    connectedSockets.Add(_InfoProtokol.Socket);
                                }
                            }
                         }
                    }

                }
            }
            catch (SocketException exc)
            {
                
                //threadsAdapters
                
                
                // 10035 == WSAEWOULDBLOCK
                if (exc.NativeErrorCode.Equals(10035))
                    SystemConectoServers.ErorDebag("Клиент подключен, но отправка пакетов заблокирована!", 0, NameFileLog);
                else
                {
                    SystemConectoServers.ErorDebag("Socket exception: Клиент отключился: код ошибки " + exc.NativeErrorCode + Environment.NewLine +
                        " === Message: " + exc.Message.ToString() + Environment.NewLine +
                        " === Exception: " + exc.ToString() + Environment.NewLine +
                        " === ErrorCode: " + exc.SocketErrorCode, 0, NameFileLog);
                }
            }
            catch (Exception exc)
            {
                SystemConectoServers.ErorDebag("Socket exception: " + Environment.NewLine +
                       " === Message: " + exc.Message.ToString() + Environment.NewLine +
                       " === Exception: " + exc.ToString(), 0, NameFileLog);
            }
            finally
            {

            }

        }

        #endregion


        #region Формирование быстрого ответа от сервера о состоянии караоке


        public static string GETStatusKaraoke(string CacheSend ="", double EndCache = -1){
            // --------------------------------- Быстрый вывод информации
            // 0 - Karaoke-Название заведения
            // #0,250,UnixTime(100000000),UnixTime(100000000),UnixTime(100000000)#
            // 1 - колонка воспроизведение композиции: 0 - композиция не воспроизводится   3546546565 - композиция звучит (уникальное число)
            // 2 - колонка код воспроизводимой песни  352
            // 3 - Название песни, что проигрывается 
            // 4 - Результат голсования
            // 5 - обновление информации о каталоге песен (количество песен в каталоге - отказался)
            // 6 - обновление информации о правилах в заведении (отдельно окно): версия обновления ( есть обновления если код обновления поменялся) формат 02/03/2013/22:33 (то бишь время)
            // 7 - обновление информации о новости в заведении (отдельно окно): версия обновления ( есть обновления если код обновления поменялся) формат 02/03/2013/22:33 (то бишь время)
            // 8 - Доп услуги
            // 9 - Афиша

            // С помощью байтов проблема с кодировкой кирилицы
            // Кодировки описание команд GetEncoding http://msdn.microsoft.com/en-us/library/system.text.encodinginfo.getencoding%28VS.85%29.aspx
            // кодировка для telnet (телнета) windows

            // Encoding cp866 = Encoding.GetEncoding("cp866");
            // byte[] msg = cp866.GetBytes(MessageNoAvtorize);

            string TimeLoadCatalog = ConectoWorkSpace.SystemConectoTimeServer.DateTimetoUnixTime_Milliseconds(new DateTime(1970, 1, 1, 0, 0, 0, 0)).ToString();
            string TimeLoadVocab = ConectoWorkSpace.SystemConectoTimeServer.DateTimetoUnixTime_Milliseconds(new DateTime(1970, 1, 1, 0, 0, 0, 0)).ToString();
            string TimeLoadNews = ConectoWorkSpace.SystemConectoTimeServer.DateTimetoUnixTime_Milliseconds(new DateTime(1970, 1, 1, 0, 0, 0, 0)).ToString();
            string TimeLoadDopUslug = ConectoWorkSpace.SystemConectoTimeServer.DateTimetoUnixTime_Milliseconds(new DateTime(1970, 1, 1, 0, 0, 0, 0)).ToString();
            string TimeLoadAficha = ConectoWorkSpace.SystemConectoTimeServer.DateTimetoUnixTime_Milliseconds(new DateTime(1970, 1, 1, 0, 0, 0, 0)).ToString();

            // Обновление без кеша
            if ( EndCache == -1 || EndCache < SystemConectoTimeServer.DateTimetoUnixTime(DateTime.Now) || CacheSend.Length == 0 )
            {
                string UseSelectUpdate = "SELECT FIRST 1 UPDATEROW FROM {0} ORDER BY UPDATEROW DESC";

                // SELECT FIRST 1 UPDATEROW FROM KARAOKE_CATALOG ORDER BY UPDATEROW DESC

                /// Команда подключения к каталогу треков
                string CommandAdapterQuery = string.Format(UseSelectUpdate, "KARAOKE_CATALOG");


                /// Подключение к БД FB 2 Local
                DBConecto.UniQuery LocalQuery = new DBConecto.UniQuery(CommandAdapterQuery, "FB");

                // Подключение к БД

                LocalQuery.InitialCatalog = PutchServerData + @"Karaoke.fdb";

                //LocalQuery.ExecuteQueryFillTable("KRAOKE_CATALOG");

               // LocalQuery.ExecuteQueryFillTable("KARAOKE_CATALOG");
               // var Test = LocalQuery.CacheDBAdapter.Tables["Karaoke_CATALOG"];
                var _TimeLoadCatalog = LocalQuery.ExecuteUNIScalar("DateTimeMillisec");
                if (_TimeLoadCatalog.Length > 0)
                {
                    // Отладка
                    var DateTimeMillesrc = Convert.ToDateTime(_TimeLoadCatalog);
                    var _TimeLoadCatalogDate = ConectoWorkSpace.SystemConectoTimeServer.DateTimetoUnixTime_Milliseconds(DateTimeMillesrc);

                    // Отладка --
                    //var _DateTimeMillesrc = ConectoWorkSpace.SystemConectoTimeServer.UnixTime_MillisecondstoDateTime(_TimeLoadCatalogDate);

                    //var fg = _DateTimeMillesrc.ToString();


                    //var fg = string.Format("{0}.{1}.{2} {3}.{4}.{5}.{6}", _DateTimeMillesrc.Day, _DateTimeMillesrc.Month, _DateTimeMillesrc.Year, 
                    //                            _DateTimeMillesrc.Hour, _DateTimeMillesrc.Minute, _DateTimeMillesrc.Second, _DateTimeMillesrc.Millisecond);
                    
                    //var Str_ = fg.ToString();

                    // Отладка --

                    TimeLoadCatalog = _TimeLoadCatalogDate.ToString();
                }
                // ОТладка 
                //var TestConvertBack = ConectoWorkSpace.SystemConectoTimeServer.UnixTime_MillisecondstoDateTime(Convert.ToDouble(TimeLoadCatalog));

                //LocalQuery.UserQuery = TestConvertBack.ToString();

                LocalQuery.UserQuery = string.Format(UseSelectUpdate, "KARAOKE_AFICHA");

                var _TimeLoadAficha = LocalQuery.ExecuteUNIScalar("DateTimeMillisec");
                if (_TimeLoadAficha.Length > 0)
                {
                    TimeLoadAficha = ConectoWorkSpace.SystemConectoTimeServer.DateTimetoUnixTime_Milliseconds(Convert.ToDateTime(_TimeLoadAficha)).ToString();
                }



                LocalQuery.UserQuery = string.Format(UseSelectUpdate, "KARAOKE_DOPUSLUGA_INVOISPAGE");

                var _TimeLoadDopUslug = LocalQuery.ExecuteUNIScalar("DateTimeMillisec");
                if (_TimeLoadDopUslug.Length > 0)
                {
                    TimeLoadDopUslug = ConectoWorkSpace.SystemConectoTimeServer.DateTimetoUnixTime_Milliseconds(Convert.ToDateTime(_TimeLoadDopUslug)).ToString();
                }

                LocalQuery.UserQuery = string.Format(UseSelectUpdate, "KARAOKE_NEWS");

                var _TimeLoadNews = LocalQuery.ExecuteUNIScalar("DateTimeMillisec");
                if (_TimeLoadNews.Length > 0)
                {
                    TimeLoadNews = ConectoWorkSpace.SystemConectoTimeServer.DateTimetoUnixTime_Milliseconds(Convert.ToDateTime(_TimeLoadNews)).ToString();
                }

            }
            else
            {
                // Чтение кеша Основные блоки
                string[] SpeedRecive_ = CacheSend.Split('#');
                // Запросы к системе
                if (SpeedRecive_.Count() > 1)
                {
                    string[] SpeedStatus_ = SpeedRecive_[1].Split(',');
                    if (SpeedStatus_.Count() > 7)
                    {
                            TimeLoadCatalog = SpeedStatus_[4]; 
                            TimeLoadVocab = SpeedStatus_[5]; 
                            TimeLoadNews = SpeedStatus_[6]; 
                            TimeLoadDopUslug = SpeedStatus_[7]; 
                            TimeLoadAficha = SpeedStatus_[8]; 
                    }
                }


            }

           

            return string.Format("Karaoke-{0}#{1},{2},{3},{4},{5},{6},{7},{8},{9}#\r\npasword[пароль]: ",
                NameKaraokeShop,
                EndVote ? 0 : StatusVote,
                IDMuzicPlay,
                NameMuzikPlay.Replace(",", "&044;"),       // c убираем зарезервированный символ # &044; пимер кодов  http://easywebscripts.net/html/ascii.php
                resultVote.ToString().Replace(",", "&044;"), // &#8225;
                TimeLoadCatalog,
                TimeLoadVocab,
                TimeLoadNews,
                TimeLoadDopUslug,
                TimeLoadAficha);

            

        }
        #endregion

        #region Формирование результатов голосования в системе караоке 

        /// <summary>
        /// список проголововавших
        /// </summary>
        private static Dictionary<string, double> ListVote = new Dictionary<string, double>();

        /// <summary>
        /// Конец голосования 
        /// </summary>
        public static bool EndVote = false;

        /// <summary>
        /// Статус голосования 0 - голосование закончено 1 - старт новой композиции, 2 - старт следующей новой композиции (далее код возвращается к 1)
        /// </summary>
        public static double StatusVote = 0;


        /// <summary>
        /// Принимаем один голос и расчитываем
        /// </summary>
        /// <param name="Vote"></param>
        public static void RezultVote(double Vote, string IPKey, bool Test = false)
        {

            if (!EndVote)
            {
                
                // Тестовый режим
                if (Test)
                {

                        lock (ListVote)
                        ListVote.Clear();

                        ListVote.Add("192.168.5.23", DoubleRand(2, 6));
                        ListVote.Add("192.168.5.14", DoubleRand(2, 6));
                        ListVote.Add("192.168.5.75", DoubleRand(2, 6));

                        ListVote.Add("192.168.5.25", DoubleRand(2, 6));
                        ListVote.Add("192.168.5.18", DoubleRand(2, 6));
                        ListVote.Add("192.168.5.79", DoubleRand(2, 6));

                        //ListVote.Add(IPKey, DoubleRand(3, 5));

                }
                
                // Отсечение результата голосования которое не должно учавствовать в голосовании
                if (Vote > VoteMinOn)
                {
                    //блокировка lock
                    lock(ListVote){
                        if (!ListVote.ContainsKey(IPKey))
                        {
                            ListVote.Add(IPKey, Vote);
                            // с помощью LinQ
                            resultVote = ListVote.Sum(v => v.Value) / ListVote.Count;
                        }

            
                        // Событие изменение результатов голосования


                    }
                }

            }
        }

      
        /// <summary>
        /// Начало голосования
        /// </summary>
        public static void StartVote()
        {
            
            //блокировка lock
            lock (ListVote)
            {
                ListVote.Clear();
                StatusVote = ConectoWorkSpace.SystemConectoTimeServer.DateTimetoUnixTime(DateTime.Now);  // StatusVote == 1 ? 2 : 1;

            }
            // Событие изменение результатов голосования

        }

        #endregion

        #region  Генерация случайных чисел в диапозоне Random

        /// <summary>
        /// Генерация случайных чисел в диапозоне Random
        /// </summary>
        private static double DoubleRand(double _min, double _max)
        {

            const int initRnd = 77;
            Random realRnd = new Random();
            Random repeatRnd = new Random(initRnd);
            // случайные числа в диапазоне [0,1)
            //for (int i = 1; i <= 5; i++) { //1
            //      return    realRnd.NextDouble();
            //}//for1
            // случайные числа в диапазоне[min,max]
            //int min = -100, max = -10;

            return realRnd.Next(Convert.ToInt32(_min), Convert.ToInt32(_max)) + realRnd.NextDouble();

            //for (int i = 1; i <= 5; i++) { //2
            //            Console.WriteLine("Число " + i + "= " + realRnd.Next(min, max));
            //}//for2
            // случайный массив байтов
            //byte[ ] bar = new byte[10];
            //repeatRnd.NextBytes(bar);

            //for (int i = 0; i < 10; i++) {//3
            //            Console.WriteLine("Число " + i + "= " + bar[i]);
            //}//for3

        }
        #endregion

        #region Авторизация устройства

        /// <summary>
        /// Принимаем один голос
        /// </summary>
        /// <param name="Vote"></param>
        public static string LoginDevice(string Login, string Password, string IPKey)
        {

            if (Login == "Mobile_Android_")
            {
                // Пдключить расшифровку
                if (Password == IPKey)
                {

                    return "Level1";
                }
            }


            // возвращают роль отдаваемых данных (право получить или отдать инфрмацию)
            return "";

        }


        #endregion


        #region Обработка сетевого протокола ----------------


        /// <summary>
        /// Расположение БД для серверов
        /// </summary>
        public static string PutchServerData = @"c:\Program Files\Conecto\Servers\data\";


        /// <summary>
        /// Протокол обмена сообщениями между сервером и клиентом:<para></para>
        /// VotePlay votestart="true"/ - голосование начато, звучит композиция новая композиция
        /// VotePlay votestop="true"/ - голосование закончено<para></para>
        /// VotePlay votedevice="4"/ - проголосовал планшет<para></para>
        /// VotePlay votetest="true"/ - голосование в тестовом режиме<para></para>
        /// 
        /// </summary>
        /// <param name="Vote"></param>
        public static string TreatmentProtocol(string data, string IPKey, Socket readSocket, bool DebugInfo, ref byte[] bytesAllSend_)
        {

            XmlTextReader reader = new XmlTextReader(new StringReader(data));
            // Свойсва чтения
            reader.WhitespaceHandling = WhitespaceHandling.None; // пропускаем пустые узлы

            // Название елемента
            string nameElement = "";
             
            // Предворительная авторизация с правами
            string AutorizeClient = "";

            // Ответ сервера
            string SendCode = "";

            // Завершение кода
            string EndCode = "";

            // Проверка наличия данных
            try
            {



                while (reader.Read())  //while (reader.Read() && reader.Name == "Товар")
                {
                    if (reader.NodeType == XmlNodeType.Element || reader.NodeType == XmlNodeType.EndElement)
                        nameElement = reader.Name;


                    switch (nameElement)
                    {

                        case "UpdateAPK":
                            // Обновление приложения
                            SendCode = string.Format("<Send><![CDATA[{0}]]></Send>", false);


                            EndCode = "<Exit />";
                            break;

                        case "Licenzija":
                            // Лицензия приложения

                            SendCode = string.Format("<Send><![CDATA[{0}]]></Send>", false);

                            EndCode = "<Exit />";
                            break;
                        
                        case "":
                            // Пустой пропускаем

                            break;
                        // Проверка версии файла 
                        case "VersionProtokol":
                            // Дополнительно проверить версию

                            break;
                        case "Autoriz":    // <Autoriz login="" passw="" />

                            if (reader.HasAttributes)
                            {
                                string login = ""; string passw = "";

                                for (int x = 0; x < reader.AttributeCount; x++)
                                {
                                    // Перейти к атрибуту
                                    reader.MoveToAttribute(x);

                                    switch (reader.LocalName.ToString())
                                    {
                                        case "login":
                                            login = reader.GetAttribute(x);
                                            break;
                                        case "passw":
                                            passw = reader.GetAttribute(x);
                                            break;
                                    }
                                }

                                // авторизация 
                                AutorizeClient = LoginDevice(login, passw, IPKey);
                                if (AutorizeClient == "")
                                {
                                    SendCode = "<notLoggedIn />";
                                }
                            }


                            break;

                        case "VotePlay":    // <VotePlay votedevice=""/>

                            #region Команды голосования

                            if (AutorizeClient == "Level1")
                            {

                            }

                            if (reader.HasAttributes)
                            {
                                double vote = 0.0;

                                for (int x = 0; x < reader.AttributeCount; x++)
                                {
                                    // Перейти к атрибуту
                                    reader.MoveToAttribute(x);

                                    switch (reader.LocalName.ToString())
                                    {
                                        case "votedevice":
                                            // Голосование с помощь устройства
                                            vote = Convert.ToDouble(reader.GetAttribute(x));

                                            // или но много кода e.InnerText или <![CDATA[
                                            // http://msdn.microsoft.com/ru-ru/library/system.xml.xmlreader.read(v=vs.110).aspx

                                            //if (reader.NodeType == XmlNodeType.CDATA)
                                            // vote =    Convert.ToDouble(reader.Value);

                                            // голосование
                                            RezultVote(vote, IPKey);

                                            break;
                                        case "votestop":
                                            // Голосование закончено
                                            EndVote = Convert.ToBoolean(reader.GetAttribute(x));
                                            // Композиция закончена
                                            // композиция, что до этого звучала остается 
                                            // IDMuzicPlay = 0;
                                            //NameMuzikPlay
                                            break;
                                        case "votestart":
                                            // Голосование начато, звучит композиция
                                            StartVote();
                                            EndVote = false;
                                            IDMuzicPlay = Convert.ToInt32(reader.GetAttribute(x));
                                            // NameMuzikPlay
                                            break;
                                        case "votetest":
                                            // Голосование в тестовом режиме
                                            SendCode = WorkCommand_votetest(ref  reader, ref  IPKey, ref  x, readSocket, DebugInfo);
                                            break;
                                        case "Next":
                                            // Продолжить тестирование

                                            var status = Convert.ToInt32(reader.GetAttribute(x));
                                            if (status == -1)
                                            {
                                                // Тест закончен очистить статус 
                                                // Таблица очищается с момощью команды <VotePlay votestart="true">

                                                //if (ListWorkCommand.ContainsKey(IPKey))
                                                //{
                                                //    ListWorkCommand.Remove(IPKey);
                                                //}

                                            }
                                            else
                                            {
                                                // Генирация случайного кода
                                                RezultVote(DoubleRand(1, 6), IPKey, true);

                                                SendCode = string.Format("<Send><![CDATA[{0}]]></Send>", GETStatusKaraoke());
                                            }
                                            
                                            break;
                                    }
                                }


                            }

                            #endregion

                            break;
                        case "Next":
                            // Получить строку состояния
                            SendCode = string.Format("<Send><![CDATA[{0}]]></Send>", GETStatusKaraoke());
                            break;
                        case "MuzicCatalog":

                            #region Каталог Треков

                            if (AutorizeClient == "Level1")
                            {

                            }

                            if (reader.HasAttributes)
                            {


                                for (int x = 0; x < reader.AttributeCount; x++)
                                {
                                    // Перейти к атрибуту
                                    reader.MoveToAttribute(x);

                                    DateTime TimeServ = DateTime.Now;
                                    string CommandAdapterQuery = "";

                                    DBConecto.UniQuery LocalQuery = null;


                                    switch (reader.LocalName.ToString())
                                    {
                                        
                                        case "timeupdate":
                                        
                                            // Тип Даты
                                            TimeServ = ConectoWorkSpace.SystemConectoTimeServer.UnixTime_MillisecondstoDateTime(Convert.ToDouble(reader.GetAttribute(x)));

                                            // Конвертация в формат SQLFB

                                            /// Команда подключения к каталогу треков
                                           CommandAdapterQuery =
                                                string.Format("Select NUMBER , TRECK_NAME , PERFORMER_NAME , VOCLS , PRO , Language, TypeMuzik  from KARAOKE_CATALOG  Where '{0}' < UPDATEROW   ORDER BY TypeMuzik DESC, Performer_Name",
                                                string.Format("{0}.{1}.{2} {3}:{4}:{5}.{6}", TimeServ.Day, TimeServ.Month, TimeServ.Year,
                                                                        TimeServ.Hour, TimeServ.Minute, TimeServ.Second, TimeServ.Millisecond));
                                            // Конвертация в формат SQLFB

                                            /// Подключение к БД FB 2 Local
                                            LocalQuery = new DBConecto.UniQuery(CommandAdapterQuery, "FB");

                                            // Подключение к БД

                                            LocalQuery.InitialCatalog = PutchServerData + @"karaoke.fdb"; 

                                            LocalQuery.ExecuteQueryFillTable("KRAOKE_CATALOG");

                                            if(LocalQuery.CacheDBAdapter != null && LocalQuery.CacheDBAdapter.Tables["KRAOKE_CATALOG"].Rows.Count > 0){

                                               // "\r\n"
                                                string SendAll = "";
                                                for (int i = 0; i < LocalQuery.CacheDBAdapter.Tables["KRAOKE_CATALOG"].Rows.Count; i++)
			                                    {
                                                    // замена символа ; - на два кинжала
                                                    SendAll += string.Format("{0};{1};{2};{3};{4};{5};{6}\r\n", 
                                                        LocalQuery.CacheDBAdapter.Tables["KRAOKE_CATALOG"].Rows[i][0].ToString(),
                                                        DBConecto.ConvertToCSV(LocalQuery.CacheDBAdapter.Tables["KRAOKE_CATALOG"].Rows[i][1].ToString()) , // .Replace(";", "&#8225")
                                                        DBConecto.ConvertToCSV(LocalQuery.CacheDBAdapter.Tables["KRAOKE_CATALOG"].Rows[i][2].ToString()),
                                                        DBConecto.ConvertToCSV(LocalQuery.CacheDBAdapter.Tables["KRAOKE_CATALOG"].Rows[i][3].ToString()),
                                                        DBConecto.ConvertToCSV(LocalQuery.CacheDBAdapter.Tables["KRAOKE_CATALOG"].Rows[i][4].ToString()),
                                                        DBConecto.ConvertToCSV(LocalQuery.CacheDBAdapter.Tables["KRAOKE_CATALOG"].Rows[i][5].ToString()),
                                                        LocalQuery.CacheDBAdapter.Tables["KRAOKE_CATALOG"].Rows[i][6].ToString()
                                                        );
			                                    }

                                                

                                                // Запрос в БД на получение данных
                                                SendCode = string.Format("<Send><![CDATA[{0}]]></Send>", SendAll);


                                            }




                                            break;
                                        case "ReadAll":
                                            if (Convert.ToBoolean(reader.GetAttribute(x)))
                                            {

                                                

                                                // Конвертация в формат SQLFB

                                                /// Команда подключения к каталогу треков
                                               CommandAdapterQuery =
                                                    "Select NUMBER , TRECK_NAME , PERFORMER_NAME , VOCLS , PRO , Language, TypeMuzik  from KARAOKE_CATALOG  ORDER BY TypeMuzik DESC, Performer_Name";
                                                // Конвертация в формат SQLFB

                                                /// Подключение к БД FB 2 Local
                                                LocalQuery = new DBConecto.UniQuery(CommandAdapterQuery, "FB");

                                                // Подключение к БД

                                                LocalQuery.InitialCatalog = PutchServerData + @"karaoke.fdb";

                                                LocalQuery.ExecuteQueryFillTable("KRAOKE_CATALOG");

                                                if (LocalQuery.CacheDBAdapter != null && LocalQuery.CacheDBAdapter.Tables["KRAOKE_CATALOG"].Rows.Count > 0)
                                                {

                                                    // "\r\n"
                                                    string SendAll = "";
                                                    for (int i = 0; i < LocalQuery.CacheDBAdapter.Tables["KRAOKE_CATALOG"].Rows.Count; i++)
                                                    {
                                                        // замена символа ; - на два кинжала
                                                        SendAll += string.Format("{0};{1};{2};{3};{4};{5};{6}\r\n",
                                                            LocalQuery.CacheDBAdapter.Tables["KRAOKE_CATALOG"].Rows[i][0].ToString(),
                                                            DBConecto.ConvertToCSV(LocalQuery.CacheDBAdapter.Tables["KRAOKE_CATALOG"].Rows[i][1].ToString()),
                                                            DBConecto.ConvertToCSV(LocalQuery.CacheDBAdapter.Tables["KRAOKE_CATALOG"].Rows[i][2].ToString()),
                                                            DBConecto.ConvertToCSV(LocalQuery.CacheDBAdapter.Tables["KRAOKE_CATALOG"].Rows[i][3].ToString()),
                                                            DBConecto.ConvertToCSV(LocalQuery.CacheDBAdapter.Tables["KRAOKE_CATALOG"].Rows[i][4].ToString()),
                                                            DBConecto.ConvertToCSV(LocalQuery.CacheDBAdapter.Tables["KRAOKE_CATALOG"].Rows[i][5].ToString()),
                                                            LocalQuery.CacheDBAdapter.Tables["KRAOKE_CATALOG"].Rows[i][6].ToString()
                                                            );
                                                    }



                                                    // Запрос в БД на получение данных
                                                    SendCode = string.Format("<Send><![CDATA[{0}]]></Send>", SendAll);


                                                }

                                            }
                                            break;
                                        case "WriteRow":
                                            if (Convert.ToBoolean(reader.GetAttribute(x)))
                                            {

                                                // Запись в БД через сервер



                                                // конец предачи
                                                //SendCode = string.Format("<EndData><![CDATA[{0}]]></EndData>", resultVote);

                                            }
                                            break;
                                    }
                                }

                            }
                                #endregion
                            break;

                        case "DopUsluga":


                            #region Прайс Допуслуг

                            if (AutorizeClient == "Level1")
                            {

                            }

                            if (reader.HasAttributes)
                            {


                                for (int x = 0; x < reader.AttributeCount; x++)
                                {
                                    // Перейти к атрибуту
                                    reader.MoveToAttribute(x);

                                    DateTime TimeServ = DateTime.Now;
                                    string CommandAdapterQuery = "";

                                    DBConecto.UniQuery LocalQuery = null;

                                    switch (reader.LocalName.ToString())
                                    {
                                        #region timeupdate
                                        case "timeupdate":

                                            // Тип Даты
                                            TimeServ = ConectoWorkSpace.SystemConectoTimeServer.UnixTime_MillisecondstoDateTime(Convert.ToDouble(reader.GetAttribute(x)));

                                            // Конвертация в формат SQLFB

                                            /// Команда подключения к каталогу треков
                                            CommandAdapterQuery = 
                                                string.Format("Select Name_Tovar , Discription_Cost, ID from KARAOKE_DOPUSLUGA_INVOISPAGE  Where '{0}' < UPDATEROW ",
                                                string.Format("{0}.{1}.{2} {3}:{4}:{5}.{6}", TimeServ.Day, TimeServ.Month, TimeServ.Year,
                                                                        TimeServ.Hour, TimeServ.Minute, TimeServ.Second, TimeServ.Millisecond));


                                            /// Подключение к БД FB 2 Local
                                            LocalQuery = new DBConecto.UniQuery(CommandAdapterQuery, "FB");

                                            // Подключение к БД

                                            LocalQuery.InitialCatalog = PutchServerData + @"karaoke.fdb";

                                            // Проверить "KARAOKE_DOPUSLUGA_INVOISPAGE"
                                            LocalQuery.ExecuteQueryFillTable("KRAOKE_DOPUSLUGA_INVOISPAGE");

                                            if (LocalQuery.CacheDBAdapter != null && LocalQuery.CacheDBAdapter.Tables["KRAOKE_DOPUSLUGA_INVOISPAGE"].Rows.Count > 0)
                                            {

                                                // "\r\n"
                                                string SendAll = "";
                                                for (int i = 0; i < LocalQuery.CacheDBAdapter.Tables["KRAOKE_DOPUSLUGA_INVOISPAGE"].Rows.Count; i++)
                                                {
                                                    // замена символа ; - на два кинжала
                                                    SendAll += string.Format("{0};{1};{2}\r\n",
                                                        LocalQuery.CacheDBAdapter.Tables["KRAOKE_DOPUSLUGA_INVOISPAGE"].Rows[i][2].ToString(),
                                                        DBConecto.ConvertToCSV(LocalQuery.CacheDBAdapter.Tables["KRAOKE_DOPUSLUGA_INVOISPAGE"].Rows[i][0].ToString()),
                                                        DBConecto.ConvertToCSV(LocalQuery.CacheDBAdapter.Tables["KRAOKE_DOPUSLUGA_INVOISPAGE"].Rows[i][1].ToString())
                                                        );
                                                }



                                                // Запрос в БД на получение данных
                                                SendCode = string.Format("<Send><![CDATA[{0}]]></Send>", SendAll);


                                            }




                                            break;
                                        #endregion

                                        #region ReadAll
                                        case "ReadAll":
                                            if (Convert.ToBoolean(reader.GetAttribute(x)))
                                            {

                                                // Конвертация в формат SQLFB

                                                /// Команда подключения к каталогу треков
                                                CommandAdapterQuery =
                                                    "Select Name_Tovar , Discription_Cost, ID from KARAOKE_DOPUSLUGA_INVOISPAGE  ";


                                                /// Подключение к БД FB 2 Local
                                                LocalQuery = new DBConecto.UniQuery(CommandAdapterQuery, "FB");

                                                // Подключение к БД

                                                LocalQuery.InitialCatalog = PutchServerData + @"karaoke.fdb";

                                                // Проверить "KARAOKE_DOPUSLUGA_INVOISPAGE"
                                                LocalQuery.ExecuteQueryFillTable("KRAOKE_DOPUSLUGA_INVOISPAGE");


                                                if (LocalQuery.CacheDBAdapter != null && LocalQuery.CacheDBAdapter.Tables["KRAOKE_DOPUSLUGA_INVOISPAGE"].Rows.Count > 0)
                                                {

                                                    // "\r\n"
                                                    string SendAll = "";
                                                    for (int i = 0; i < LocalQuery.CacheDBAdapter.Tables["KRAOKE_DOPUSLUGA_INVOISPAGE"].Rows.Count; i++)
                                                    {
                                                        // замена символа ; - на два кинжала
                                                        SendAll += string.Format("{0};{1};{2}\r\n",
                                                            LocalQuery.CacheDBAdapter.Tables["KRAOKE_DOPUSLUGA_INVOISPAGE"].Rows[i][2].ToString(),
                                                            DBConecto.ConvertToCSV(LocalQuery.CacheDBAdapter.Tables["KRAOKE_DOPUSLUGA_INVOISPAGE"].Rows[i][0].ToString()),  //.Replace(";", "&#8225")
                                                            DBConecto.ConvertToCSV(LocalQuery.CacheDBAdapter.Tables["KRAOKE_DOPUSLUGA_INVOISPAGE"].Rows[i][1].ToString())
                                                            );
                                                    }



                                                    // Запрос в БД на получение данных
                                                    SendCode = string.Format("<Send><![CDATA[{0}]]></Send>", SendAll);


                                                }

                                            }
                                            break;
                                        #endregion

                                    }
                                }

                            }

                            #endregion

                            break;
                        case "NEWS":

                            #region Публикации Новости

                            if (AutorizeClient == "Level1")
                            {

                            }

                            if (reader.HasAttributes)
                            {


                                for (int x = 0; x < reader.AttributeCount; x++)
                                {
                                    // Перейти к атрибуту
                                    reader.MoveToAttribute(x);

                                    DateTime TimeServ = DateTime.Now;
                                    string CommandAdapterQuery = "";

                                    DBConecto.UniQuery LocalQuery = null;

                                    switch (reader.LocalName.ToString())
                                    {
                                        #region timeupdate
                                        case "timeupdate":

                                            // Тип Даты
                                            TimeServ = ConectoWorkSpace.SystemConectoTimeServer.UnixTime_MillisecondstoDateTime(Convert.ToDouble(reader.GetAttribute(x)));

                                            // Конвертация в формат SQLFB


                  //                                            "Name_News              VARCHAR(150)    CHARACTER SET UTF8 ,  " +
                  //"Anons_txt              VARCHAR(200)    CHARACTER SET UTF8 ,  " +
                  //"Discription_News              ITXT_BLOB  , " +
                  //"Image_Small ITXT_BLOB, " +
                  //"Image_News ITXT_BLOB, " +
                  //"Data             TIMESTAMP  ,  " +
                  //"Image_Baner ITXT_BLOB, " +

                                            /// Команда подключения к каталогу треков
                                            CommandAdapterQuery =
                                                string.Format("Select  Name_News , Anons_txt,  Data, Discription_News_Puth, Image_Small_Puth, Image_News_Puth, Image_Baner_Puth, ID  from KARAOKE_NEWS  Where '{0}' < UPDATEROW ORDER BY DATA",
                                                string.Format("{0}.{1}.{2} {3}:{4}:{5}.{6}", TimeServ.Day, TimeServ.Month, TimeServ.Year,
                                                                        TimeServ.Hour, TimeServ.Minute, TimeServ.Second, TimeServ.Millisecond));


                                            /// Подключение к БД FB 2 Local
                                            LocalQuery = new DBConecto.UniQuery(CommandAdapterQuery, "FB");

                                            // Подключение к БД

                                            LocalQuery.InitialCatalog = PutchServerData + @"karaoke.fdb";

                                            // Проверить "KARAOKE_DOPUSLUGA_INVOISPAGE"
                                            LocalQuery.ExecuteQueryFillTable("KRAOKE_NEWS");

                                            if (LocalQuery.CacheDBAdapter != null && LocalQuery.CacheDBAdapter.Tables["KRAOKE_NEWS"].Rows.Count > 0)
                                            {

                                                // "\r\n"
                                                string SendAll = "";
                                                string FileAll = "";
 
                                                for (int i = 0; i < LocalQuery.CacheDBAdapter.Tables["KRAOKE_NEWS"].Rows.Count; i++)
                                                {
                                                    // Передача текстового файла
                                                    FileAll += string.Format("<SendFileImage namefile='{1}'><![CDATA[{0}]]></SendFileImage>", "", LocalQuery.CacheDBAdapter.Tables["KRAOKE_NEWS"].Rows[i][3].ToString());
                                                    
                                                    // Передача Файлов Изображений
                                                    FileAll += string.Format("<SendFileImage namefile='{1}'><![CDATA[{0}]]></SendFileImage>", "", LocalQuery.CacheDBAdapter.Tables["KRAOKE_NEWS"].Rows[i][4].ToString());
                                                    FileAll += string.Format("<SendFileImage namefile='{1}'><![CDATA[{0}]]></SendFileImage>", "", LocalQuery.CacheDBAdapter.Tables["KRAOKE_NEWS"].Rows[i][5].ToString());
                                                    FileAll += string.Format("<SendFileImage namefile='{1}'><![CDATA[{0}]]></SendFileImage>", "", LocalQuery.CacheDBAdapter.Tables["KRAOKE_NEWS"].Rows[i][6].ToString());
                                                    
                                                    // Формарование передачи Таблицы
                                                    // замена символа ; - на два кинжала
                                                    SendAll += string.Format("{0};{1};{2};{3};{4};{5};{6};{7}\r\n",
                                                        LocalQuery.CacheDBAdapter.Tables["KRAOKE_NEWS"].Rows[i][7].ToString().Replace(";", "&#8225"),
                                                        LocalQuery.CacheDBAdapter.Tables["KRAOKE_NEWS"].Rows[i][0].ToString().Replace(";", "&#8225"),
                                                        LocalQuery.CacheDBAdapter.Tables["KRAOKE_NEWS"].Rows[i][1].ToString().Replace(";", "&#8225"),
                                                        LocalQuery.CacheDBAdapter.Tables["KRAOKE_NEWS"].Rows[i][3].ToString(),

                                                        LocalQuery.CacheDBAdapter.Tables["KRAOKE_NEWS"].Rows[i][4].ToString(),
                                                        LocalQuery.CacheDBAdapter.Tables["KRAOKE_NEWS"].Rows[i][5].ToString(),
                                                        LocalQuery.CacheDBAdapter.Tables["KRAOKE_NEWS"].Rows[i][2].ToString(),
                                                        LocalQuery.CacheDBAdapter.Tables["KRAOKE_NEWS"].Rows[i][6].ToString()
                                                        );
                                                }



                                                // Запрос в БД на получение данных
                                                SendCode = string.Format("{1}<Send><![CDATA[{0}]]></Send>", SendAll, FileAll);


                                            }




                                            break;
                                        #endregion

                                        #region ReadAll
                                        case "ReadAll":
                                        case "ReadAllGz":
                                            if (Convert.ToBoolean(reader.GetAttribute(x)))
                                            {
                                                CommandAdapterQuery =
                                               "Select  Name_News , Anons_txt,  Data, Discription_News_Puth, Image_Small_Puth, Image_News_Puth, Image_Baner_Puth, ID  from KARAOKE_NEWS ORDER BY DATA";


                                                /// Подключение к БД FB 2 Local
                                                LocalQuery = new DBConecto.UniQuery(CommandAdapterQuery, "FB");

                                                // Подключение к БД

                                                LocalQuery.InitialCatalog = PutchServerData + @"karaoke.fdb";

                                                // Проверить "KARAOKE_DOPUSLUGA_INVOISPAGE"
                                                LocalQuery.ExecuteQueryFillTable("KRAOKE_NEWS");

                                                if (LocalQuery.CacheDBAdapter != null && LocalQuery.CacheDBAdapter.Tables["KRAOKE_NEWS"].Rows.Count > 0)
                                                {

                                                    // "\r\n"
                                                    string SendAll = "";
                                                    string FileAll = "";


                                                    byte[] bytesFile1 = Encoding.UTF8.GetBytes("FileSenD");

                                                    var df = bytesFile1.Length;

                                                    // Всего байт
                                                    //int BaAllPack = 0;

                                                    // Формирование пустой строки 1024 байта
                                                    string ReservBlank = "";
                                                    for (int i = 0; i < 128; i++)
                                                    {
                                                        ReservBlank += "        ";
                                                    }

                                                    // FileSenD 8 байт ключ пакета файла / длина всего пакета 18 байт / Путь файла 1024 байта / 18 байт длина пакета файла до следующего заголовка
                                                    // Заголовок:  Путь файла 1024 байта / 18 байт длина пакета файла до следующего заголовка
                                                    SystemConecto.WriteinArrayByte(ref bytesAllSend_,
                                                            string.Format("FileSenD{0}", "000000000000000000") );


                                                    //byte[] bytesFile = Encoding.UTF8.GetBytes("<body>");
                                                    //WriteinArrayByte(ref bytesAllSend_, "<body>");
                                                    //SendAll += "<body>";

                                                    for (int i = 0; i < LocalQuery.CacheDBAdapter.Tables["KRAOKE_NEWS"].Rows.Count; i++)
                                                    {

                                                        // byte[] _bytes = Encoding.UTF8.GetBytes("<body>");

                                                        //var LenStart = bytesFile.Length;

                                                        //Array.Resize(ref bytesFile, bytesFile.Length + _bytes.Length);

                                                        //_bytes.CopyTo(bytesFile, LenStart);

                                                        
                                                        //byte[] _bytes = Encoding.UTF8.GetBytes(string.Format("<SendFileTxt namefile='{1}'><![CDATA[", LocalQuery.CacheDBAdapter.Tables["KRAOKE_NEWS"].Rows[i][3].ToString()));

                                                        // Перенос данных в масив bytesAllSend_ из bytesEndSend_
                                                        //var LenStart2_ = bytesAllSend_.Length;
                                                       // Array.Resize(ref bytesAllSend_, bytesAllSend_.Length + _bytes.Length);
                                                        //_bytes.CopyTo(bytesAllSend_, LenStart2_);



                                                        // Передача текстового файла
                                                        FileAll += string.Format("<SendFileTxt namefile='{1}'><![CDATA[{0}]]></SendFileTxt>",
                                                            File.ReadAllText(string.Format("{0}/file/Txt/{1}", PutchServerData ,new FileInfo(LocalQuery.CacheDBAdapter.Tables["KRAOKE_NEWS"].Rows[i][3].ToString()).Name ) ),
                                                            LocalQuery.CacheDBAdapter.Tables["KRAOKE_NEWS"].Rows[i][3].ToString());

                                                      
                                                        // Перенос данных в масив bytesAllSend_ из текстовой строки
                                                        //WriteinArrayByte(ref bytesAllSend_,
                                                        //    string.Format("<SendFileTxt namefile='{1}'><![CDATA[{0}]]></SendFileTxt>",
                                                        //    File.ReadAllText(string.Format("{0}/file/Txt/{1}", PutchServerData, new FileInfo(LocalQuery.CacheDBAdapter.Tables["KRAOKE_NEWS"].Rows[i][3].ToString()).Name)),
                                                        //    LocalQuery.CacheDBAdapter.Tables["KRAOKE_NEWS"].Rows[i][3].ToString()) );



                                                        // Передача Файлов Изображений
                                                        FileAll += string.Format("<SendFileImage namefile='{1}'><![CDATA[{0}]]></SendFileImage>",
                                                             "Data byte[]",
                                                             LocalQuery.CacheDBAdapter.Tables["KRAOKE_NEWS"].Rows[i][4].ToString());
                                                        
                                                        // Перенос данных в масив bytesAllSend_ из бинарного файла
                                                        //WriteinArrayByte(ref bytesAllSend_,
                                                        //    string.Format("<SendFileImage namefile='{0}'><![CDATA[", LocalQuery.CacheDBAdapter.Tables["KRAOKE_NEWS"].Rows[i][4].ToString()));



                                                        #region Сжатие данных в потоке тестовый запуск

                                                        //string NamePuthSenFile = string.Format("{0}/file/Image/{1}", PutchServerData, new FileInfo(LocalQuery.CacheDBAdapter.Tables["KRAOKE_NEWS"].Rows[i][4].ToString()).Name);

                                                        //    FileInfo Comfile = new FileInfo(NamePuthSenFile);

                                                        //    // Ошибки файловой структуры
                                                        //    try
                                                        //    {
                                                                

                                                        //        // Получить поток исходного файла.
                                                        //        using (FileStream inFile = Comfile.OpenRead())
                                                        //        {

                                                        //            // cоздать сжатый файл.
                                                        //            using (MemoryStream outFile = new MemoryStream())
                                                        //            //using (FileStream outFile = File.Create(PathFileCom))
                                                        //            {
                                                        //                // записать в файл
                                                        //                using (GZipStream Compress = new GZipStream(outFile, CompressionMode.Compress))
                                                        //                {
                                                        //                    // Скопируйте исходный файл в поток сжатия.
                                                        //                    inFile.CopyTo(Compress);
                                                        //                }
                                                        //                //outFile.ToString();

                                                                        


                                                        //                //WriteinArrayByte(ref bytesAllSend_,
                                                        //                //    new String(outFile.ToArray(), "UTF-8")); new String(new byte[] {b});

                                                        //            }
                                                        //        }
                                                        //    }
                                                        //    catch (Exception Ex)
                                                        //    {
                                                        //        // Ошибка, значит интернета у нас нет. Плачем :'(
                                                        //        if (SystemConecto.DebagApp)
                                                        //        {
                                                        //            // Тут нужна дополнительная аналитика например ошибка 403 тоже попадает сюда.
                                                        //            SystemConecto.ErorDebag("Ошибка сжатия файла: " + Ex + "." + " Путь к файлу: [" + Comfile.FullName + "]", 1);
                                                        //        }
                                                        //    }



                                                        #endregion

                                                        #region Не сжатые файлы 4

                                                        // Заголовок - Запись данных в пакет
                                                        int StartNewPacket = bytesAllSend_.Length;
                                                        string Putch = LocalQuery.CacheDBAdapter.Tables["KRAOKE_NEWS"].Rows[i][4].ToString();

                                                        // Чтение файла подготовка данных
                                                        byte[] FileWrBy = new byte[0];
                                                        if (reader.LocalName.ToString() == "ReadAllGz")
                                                        {
                                                            FileWrBy = File.ReadAllBytes(string.Format("{0}/file/Image/{1}", PutchServerData, new FileInfo(LocalQuery.CacheDBAdapter.Tables["KRAOKE_NEWS"].Rows[i][4].ToString()).Name));
                                                        }
                                                        else
                                                        {
                                                            FileWrBy = File.ReadAllBytes(string.Format("{0}/file/Image/{1}", PutchServerData, new FileInfo(LocalQuery.CacheDBAdapter.Tables["KRAOKE_NEWS"].Rows[i][4].ToString()).Name));
                                                        }

                                                        SystemConecto.WriteinArrayByte(ref bytesAllSend_,
                                                            string.Format("{0}{1}", string.Format("{0}{1}",Putch , ReservBlank.Remove(0,Putch.Length) ), string.Format("{0}{1}", "000000000000000000".Remove(18-FileWrBy.Length.ToString().Length), FileWrBy.Length)  ));

                                                        SystemConecto.WriteinArrayByte(ref bytesAllSend_,
                                                              FileWrBy);

                                                        #endregion

                                                        #region Не сжатые файлы 5

                                                        // Чтение файла подготовка данных
                                                        FileWrBy = File.ReadAllBytes(string.Format("{0}/file/Image/{1}", PutchServerData, new FileInfo(LocalQuery.CacheDBAdapter.Tables["KRAOKE_NEWS"].Rows[i][5].ToString()).Name));

                                                        // Заголовок - Запись данных в пакет
                                                        StartNewPacket = bytesAllSend_.Length;
                                                        Putch = LocalQuery.CacheDBAdapter.Tables["KRAOKE_NEWS"].Rows[i][5].ToString();

                                                        SystemConecto.WriteinArrayByte(ref bytesAllSend_,
                                                            string.Format("{0}{1}", string.Format("{0}{1}", Putch, ReservBlank.Remove(0, Putch.Length)), string.Format("{0}{1}", "000000000000000000".Remove(18 - FileWrBy.Length.ToString().Length), FileWrBy.Length)));

                                                        SystemConecto.WriteinArrayByte(ref bytesAllSend_,
                                                              FileWrBy);

                                                        #endregion
                                                        #region Не сжатые файлы 6

                                                        // Чтение файла подготовка данных
                                                        FileWrBy = File.ReadAllBytes(string.Format("{0}/file/Image/{1}", PutchServerData, new FileInfo(LocalQuery.CacheDBAdapter.Tables["KRAOKE_NEWS"].Rows[i][6].ToString()).Name));

                                                        // Заголовок - Запись данных в пакет
                                                        StartNewPacket = bytesAllSend_.Length;
                                                        Putch = LocalQuery.CacheDBAdapter.Tables["KRAOKE_NEWS"].Rows[i][6].ToString();

                                                        SystemConecto.WriteinArrayByte(ref bytesAllSend_,
                                                            string.Format("{0}{1}", string.Format("{0}{1}", Putch, ReservBlank.Remove(0, Putch.Length)), string.Format("{0}{1}", "000000000000000000".Remove(18 - FileWrBy.Length.ToString().Length), FileWrBy.Length)));

                                                        SystemConecto.WriteinArrayByte(ref bytesAllSend_,
                                                              FileWrBy);

                                                        #endregion

                                                        



                                                            //WriteinArrayByte(ref bytesAllSend_,
                                                            //   "]]></SendFileImage>");

                                                        FileAll += string.Format("<SendFileImage namefile='{1}'><![CDATA[{0}]]></SendFileImage>",
                                                             File.ReadAllBytes(string.Format("{0}/file/Image/{1}", PutchServerData, new FileInfo(LocalQuery.CacheDBAdapter.Tables["KRAOKE_NEWS"].Rows[i][5].ToString()).Name)),
                                                             LocalQuery.CacheDBAdapter.Tables["KRAOKE_NEWS"].Rows[i][5].ToString());
                                                        FileAll += string.Format("<SendFileImage namefile='{1}'><![CDATA[{0}]]></SendFileImage>",
                                                             File.ReadAllBytes(string.Format("{0}/file/Image/{1}", PutchServerData, new FileInfo(LocalQuery.CacheDBAdapter.Tables["KRAOKE_NEWS"].Rows[i][6].ToString()).Name)),
                                                             LocalQuery.CacheDBAdapter.Tables["KRAOKE_NEWS"].Rows[i][6].ToString());

                                                       // FileAll += string.Format("<SendFileImage namefile='{1}'><![CDATA[{0}]]></SendFileImage>",
                                                       //     "", LocalQuery.CacheDBAdapter.Tables["KRAOKE_NEWS"].Rows[i][5].ToString());
                                                       // FileAll += string.Format("<SendFileImage namefile='{1}'><![CDATA[{0}]]></SendFileImage>",
                                                       //     "", LocalQuery.CacheDBAdapter.Tables["KRAOKE_NEWS"].Rows[i][6].ToString());

                                                        // Формарование передачи Таблицы
                                                        // замена символа ; - на два кинжала
                                                        SendAll += string.Format("{0};{1};{2};{3};{4};{5};{6};{7}\r\n",
                                                            LocalQuery.CacheDBAdapter.Tables["KRAOKE_NEWS"].Rows[i][7].ToString().Replace(";", "&#8225"),
                                                            LocalQuery.CacheDBAdapter.Tables["KRAOKE_NEWS"].Rows[i][0].ToString().Replace(";", "&#8225"),
                                                            LocalQuery.CacheDBAdapter.Tables["KRAOKE_NEWS"].Rows[i][1].ToString().Replace(";", "&#8225"),
                                                            LocalQuery.CacheDBAdapter.Tables["KRAOKE_NEWS"].Rows[i][3].ToString(),

                                                            LocalQuery.CacheDBAdapter.Tables["KRAOKE_NEWS"].Rows[i][4].ToString(),
                                                            LocalQuery.CacheDBAdapter.Tables["KRAOKE_NEWS"].Rows[i][5].ToString(),
                                                            LocalQuery.CacheDBAdapter.Tables["KRAOKE_NEWS"].Rows[i][2].ToString(),
                                                            LocalQuery.CacheDBAdapter.Tables["KRAOKE_NEWS"].Rows[i][6].ToString()
                                                            );
                                                    }



                                                    // Запрос в БД на получение данных
                                                    SendCode = string.Format("<body>{1}<Send><![CDATA[{0}]]></Send></body>", SendAll, FileAll);
                                                    // Ответ в Байтах <body>


                                                    // Всего байт пакет файлов
                                                    SystemConecto.UpdateinArrayByte(ref bytesAllSend_, bytesAllSend_.Length, 8, 18);


                                                    byte[] bytesEndSend_ = Encoding.UTF8.GetBytes(string.Format("<body>{1}<Send><![CDATA[{0}]]></Send></body>", SendAll, FileAll));

                                                    // Перенос данных в масив bytesAllSend_ из bytesEndSend_
                                                    var LenStart2_ = bytesAllSend_.Length;
                                                    Array.Resize(ref bytesAllSend_, bytesAllSend_.Length + bytesEndSend_.Length);
                                                    bytesEndSend_.CopyTo(bytesAllSend_, LenStart2_);
                                                  

                                                }

                                            }
                                            break;
                                        #endregion

                                    }
                                }

                            }

                            #endregion


                            break;


                        case "AFICHA":

                            #region Публикация Афиша

                            if (AutorizeClient == "Level1")
                            {

                            }

                            if (reader.HasAttributes)
                            {


                                for (int x = 0; x < reader.AttributeCount; x++)
                                {
                                    // Перейти к атрибуту
                                    reader.MoveToAttribute(x);

                                    DateTime TimeServ = DateTime.Now;
                                    string CommandAdapterQuery = "";

                                    DBConecto.UniQuery LocalQuery = null;

                                    switch (reader.LocalName.ToString())
                                    {
                                        #region timeupdate
                                        case "timeupdate":

                                            // Тип Даты
                                           TimeServ = ConectoWorkSpace.SystemConectoTimeServer.UnixTime_MillisecondstoDateTime(Convert.ToDouble(reader.GetAttribute(x)));

                                            // Конвертация в формат SQLFB


                  //"Data             TIMESTAMP  ,  " +
                  //"TimeIn          VARCHAR(10)     CHARACTER SET UTF8  ,  " +
                  //"TimeOut      VARCHAR(10)    CHARACTER SET UTF8  ,  " +
                  //"Name_Group              VARCHAR(100)    CHARACTER SET UTF8 ,  " +
                  //"Discription_Style    
 // public string Data_num{get; set;} //дата(число события 1)
 // public string Data_mon{get; set;} //дата(месяц события текстом: январь)
 // public string Day{get; set;}  //дата(день недели текстом : понедельник)


                                            /// Команда подключения к каталогу треков
                                            CommandAdapterQuery =
                                                string.Format("Select Data , TimeIn,  TimeOut, Name_Group, Discription_Style, ID from KARAOKE_AFICHA  Where '{0}' < UPDATEROW ORDER BY DATA",
                                                string.Format("{0}.{1}.{2} {3}:{4}:{5}.{6}", TimeServ.Day, TimeServ.Month, TimeServ.Year,
                                                                        TimeServ.Hour, TimeServ.Minute, TimeServ.Second, TimeServ.Millisecond));


                                            /// Подключение к БД FB 2 Local
                                           LocalQuery = new DBConecto.UniQuery(CommandAdapterQuery, "FB");

                                            // Подключение к БД

                                            LocalQuery.InitialCatalog = PutchServerData + @"karaoke.fdb";

                                            // Проверить "KARAOKE_DOPUSLUGA_INVOISPAGE"
                                            LocalQuery.ExecuteQueryFillTable("KRAOKE_AFICHA");

                                            if (LocalQuery.CacheDBAdapter != null && LocalQuery.CacheDBAdapter.Tables["KRAOKE_AFICHA"].Rows.Count > 0)
                                            {

                                                // "\r\n"
                                                string SendAll = "";
                                                for (int i = 0; i < LocalQuery.CacheDBAdapter.Tables["KRAOKE_AFICHA"].Rows.Count; i++)
                                                {
                                                    //ConectoWorkSpace.SystemConectoTimeServer
                                                    //DateTime Test = DateTime.Now; dynamic
                                                    dynamic Date_ = LocalQuery.CacheDBAdapter.Tables["KRAOKE_AFICHA"].Rows[i][0];
                                                    var Pole_5 = ConectoWorkSpace.SystemConectoTimeServer.GetNameMonthRu_Int(Date_.Day);
                                                    var Pole_6 = ConectoWorkSpace.SystemConectoTimeServer.GetNameDayOfWeekRu_Int(Date_.DayOfWeek.ToString());
                                                    // замена символа ; - на два кинжала
                                                    // 1;11;Декабря;среда;в 20:00;;Маленькая пятница! Cocktail Party!;Все шоты по 30 грн!
                                                    SendAll += string.Format("{0};{1};{2};{3};{4};{5};{6};{7};{8}\r\n",
                                                        LocalQuery.CacheDBAdapter.Tables["KRAOKE_AFICHA"].Rows[i][5].ToString(),
                                                        Date_.Day,
                                                        Pole_5.ToString(),
                                                        Pole_6.ToString(),
                                                        DBConecto.ConvertToCSV(LocalQuery.CacheDBAdapter.Tables["KRAOKE_AFICHA"].Rows[i][1].ToString()),
                                                        DBConecto.ConvertToCSV(LocalQuery.CacheDBAdapter.Tables["KRAOKE_AFICHA"].Rows[i][2].ToString()),
                                                        DBConecto.ConvertToCSV(LocalQuery.CacheDBAdapter.Tables["KRAOKE_AFICHA"].Rows[i][3].ToString()),
                                                        DBConecto.ConvertToCSV(LocalQuery.CacheDBAdapter.Tables["KRAOKE_AFICHA"].Rows[i][4].ToString()),
                                                        LocalQuery.CacheDBAdapter.Tables["KRAOKE_AFICHA"].Rows[i][0].ToString()
                                                        );
                                                }



                                                // Запрос в БД на получение данных
                                                SendCode = string.Format("<Send><![CDATA[{0}]]></Send>", SendAll);


                                            }




                                            break;
                            #endregion

                                        #region ReadAll
                                        case "ReadAll":
                                            if (Convert.ToBoolean(reader.GetAttribute(x)))
                                            {
                                                /// Команда подключения к каталогу треков
                                                CommandAdapterQuery =
                                                    "Select Data , TimeIn,  TimeOut, Name_Group, Discription_Style, ID from KARAOKE_AFICHA ORDER BY DATA";


                                                /// Подключение к БД FB 2 Local
                                                LocalQuery = new DBConecto.UniQuery(CommandAdapterQuery, "FB");

                                                // Подключение к БД

                                                LocalQuery.InitialCatalog = PutchServerData + @"karaoke.fdb";

                                                // Проверить "KARAOKE_DOPUSLUGA_INVOISPAGE"
                                                LocalQuery.ExecuteQueryFillTable("KRAOKE_AFICHA");

                                                if (LocalQuery.CacheDBAdapter != null && LocalQuery.CacheDBAdapter.Tables["KRAOKE_AFICHA"].Rows.Count > 0)
                                                {

                                                    // "\r\n"
                                                    string SendAll = "";
                                                    for (int i = 0; i < LocalQuery.CacheDBAdapter.Tables["KRAOKE_AFICHA"].Rows.Count; i++)
                                                    {
                                                        //ConectoWorkSpace.SystemConectoTimeServer
                                                        //DateTime Test = DateTime.Now; dynamic
                                                        dynamic Date_ = LocalQuery.CacheDBAdapter.Tables["KRAOKE_AFICHA"].Rows[i][0];
                                                        var Pole_5 = ConectoWorkSpace.SystemConectoTimeServer.GetNameMonthRu_Int(Date_.Month);
                                                        var Pole_6 = ConectoWorkSpace.SystemConectoTimeServer.GetNameDayOfWeekRu_Int(Date_.DayOfWeek.ToString());
                                                        // замена символа ; - на два кинжала
                                                        // 1;11;Декабря;среда;в 20:00;;Маленькая пятница! Cocktail Party!;Все шоты по 30 грн!
                                                        SendAll += string.Format("{0};{1};{2};{3};{4};{5};{6};{7};{8}\r\n",
                                                            LocalQuery.CacheDBAdapter.Tables["KRAOKE_AFICHA"].Rows[i][5].ToString(),
                                                            Date_.Day,
                                                            Pole_5.ToString(),
                                                            Pole_6.ToString(),
                                                            DBConecto.ConvertToCSV(LocalQuery.CacheDBAdapter.Tables["KRAOKE_AFICHA"].Rows[i][1].ToString()),
                                                            DBConecto.ConvertToCSV(LocalQuery.CacheDBAdapter.Tables["KRAOKE_AFICHA"].Rows[i][2].ToString()),
                                                            DBConecto.ConvertToCSV(LocalQuery.CacheDBAdapter.Tables["KRAOKE_AFICHA"].Rows[i][3].ToString()), //.Replace(";", "&#8225"),
                                                            DBConecto.ConvertToCSV(LocalQuery.CacheDBAdapter.Tables["KRAOKE_AFICHA"].Rows[i][4].ToString()),
                                                            LocalQuery.CacheDBAdapter.Tables["KRAOKE_AFICHA"].Rows[i][0].ToString()
                                                            );
                                                    }



                                                    // Запрос в БД на получение данных
                                                    SendCode = string.Format("<Send><![CDATA[{0}]]></Send>", SendAll);


                                                }
                                            }
                                            break;
                                        #endregion

                                    }
                                }

                            }

                            #endregion


                            break;


                        case "SQL-CSV": // Уитываем. что запросы идут через DBConecto



                            break;

                        case "<Exit />":

                            EndCode = "<Exit />";

                            break;

                        default:



                            break;
                    }
                }
                // Has Value

            }
            catch (Exception exc)
            {
                SystemConectoServers.ErorDebag("Socket exception: " + Environment.NewLine +
                      " === Message: " + exc.Message.ToString() + Environment.NewLine +
                      " === Exception: " + exc.ToString(), 0, NameFileLog);
                //SendCode = "<Error />"; // Value=\"Отсутствуют xml данные.\"
            }
            // Сформировать ответ


            // возвращаем ответ для клиента <Send><![CDATA[  ]]></Send> "<?xml version=\"1.0\" encoding=\"utf-16\" ?>" + 
            return  SendCode + EndCode;

        }


        #endregion


        #region Обработка сообщений во вложеных командах (<VotePlay votetest=""/>

        /// <summary>
        /// Проверка статуса выполнения команд по протоколу учитывая IP ключь
        /// </summary>
        /// <param name="?"></param>
        private static StruWorkCommand ChekStatusIPKey(ref string IPKey)
        {
            StruWorkCommand _ListWorkCommandStru = new StruWorkCommand();
            // Проверка статуса 
            if (ListWorkCommand.ContainsKey(IPKey))
            {
                _ListWorkCommandStru = ListWorkCommand[IPKey];
            }
            else
            {
                 // Определено по умолчанию
                 _ListWorkCommandStru.StatusTestVote = 0;

                 ListWorkCommand.Add(IPKey, _ListWorkCommandStru);
               
            }

            return _ListWorkCommandStru;
        }

        /// <summary>
        /// список обработки протаколов
        /// </summary>
        private static Dictionary<string, StruWorkCommand> ListWorkCommand = new Dictionary<string, StruWorkCommand>();

        public struct StruWorkCommand
        {
            /// <summary>
            /// Статус тестового голосования:<para></para>
            /// 0 - тест голосование выключен<para></para>
            /// 1, 2, n - начато голосование 
            /// </summary>
            public int StatusTestVote {get; set;}

        }

        public static string WorkCommand_votetest(ref XmlTextReader reader, ref string IPKey, ref int x, Socket readSocket, bool DebugInfo)
        {


            // Проверка статуса 
            StruWorkCommand _ListWorkCommandStru = ChekStatusIPKey(ref IPKey);


            string _SendCode = "";
            //var Tert = reader.GetAttribute(x);
            //var Te = Convert.ToDouble(Tert);
            double vote = 0.0;
            vote = Convert.ToDouble(reader.GetAttribute(x));
            RezultVote(vote, IPKey, true);


            //byte[] Receivebuffer = new byte[1024];
            try
            {
                //byte[]
                //     msg = msg;
                if (_ListWorkCommandStru.StatusTestVote == 0)
                {
                    readSocket.Send(Encoding.UTF8.GetBytes("<StartTest />"));
                    if (DebugInfo)
                        SystemConectoServers.ErorDebag("Сервер направил ответ: <StartTest />\r\n", 3, NameFileLog);
                }
                        

                // Отправил
                var mesage_ = string.Format( "<Send><![CDATA[{0}]]></Send>", GETStatusKaraoke() );
                readSocket.Send( Encoding.UTF8.GetBytes( mesage_ ) );
                if (DebugInfo)
                    SystemConectoServers.ErorDebag("Сервер направил ответ: " + mesage_ + "\r\n", 3, NameFileLog);
                
                // Запись про отправку
                _ListWorkCommandStru.StatusTestVote++;
                ListWorkCommand[IPKey] = _ListWorkCommandStru;
            }
            catch
            {
                _SendCode = "<Error />";
            }
            _SendCode = "<NoSend />";

            return _SendCode;
        }


        #endregion

      
        // ------------------------------------ --------------------- Редко используются


        #region Создание БД

        /// <summary>
        /// Создание БД
        /// </summary>
        /// <param name="InitialCatalog">Путь к БД</param>
        /// <returns></returns>
        private static bool CreateBD_Table(string InitialCatalog)
        {

            // Подключение к БД
            DBConecto.UniQuery LocalQuery = new DBConecto.UniQuery("","FB");

            LocalQuery.InitialCatalog = InitialCatalog;

            try
            {
                FirebirdSql.Data.FirebirdClient.FbConnection.CreateDatabase(LocalQuery.ConnectionString(), 16384, true, false);
                // Создание БД KARAOKE
                // Таблицы сервера.

                // Определение своего типа полей
                LocalQuery.UserQuery = "CREATE DOMAIN  COD_ID AS INT CHECK (VALUE>0) NOT NULL";

                LocalQuery.ExecuteUNIScalar();

                // Определение своего типа полей
                LocalQuery.UserQuery = "CREATE DOMAIN ITXT_BLOB AS Blob SUB_TYPE 0 SEGMENT SIZE 1024";


                LocalQuery.ExecuteUNIScalar();
                #region Таблица треков

                // Формирование гениратор
                LocalQuery.UserQuery = "CREATE GENERATOR GEN_COD_TRACK";

                LocalQuery.ExecuteUNIScalar();
                LocalQuery.UserQuery = "SET GENERATOR GEN_COD_TRACK TO 0";

                LocalQuery.ExecuteUNIScalar();

                // Integer  NOT  NULL -> COD_ID
                LocalQuery.UserQuery = "CREATE TABLE KARAOKE_CATALOG  (ID  COD_ID,  " +
                    "Number             Integer   NOT  NULL,  " +
                    "Treck_Name          VARCHAR(100)     CHARACTER SET UTF8  ,  " +
                    "Performer_Name      VARCHAR(100)    CHARACTER SET UTF8  ,  " +
                    "Vocls              VARCHAR(10)    CHARACTER SET UTF8 ,  " +
                    "pro                VARCHAR(5)    CHARACTER SET UTF8  , " +
                    "Language            VARCHAR(10)    CHARACTER SET UTF8  ,  " +
                    "TypeMuzik          Integer   , " +
                    "UpdateRow          TIMESTAMP NOT  NULL," +

                    "constraint un_NumbeTr unique (Number) using index Number_Treck )";

                LocalQuery.ExecuteUNIScalar();

                // Формирование авто или уникального ID
                //LocalQuery.UserQuery = "CREATE PROCEDURE GEN_COD RETURNS (KARAOKE_CATALOG INTEGER) AS BEGIN KARAOKE_CATALOG = GEN_ID(GEN_COD_TRACK,1); END";


                //LocalQuery.ExecuteUNIScalar();

                LocalQuery.UserQuery = "CREATE OR ALTER TRIGGER BI_KARAOKE_CATALOG for KARAOKE_CATALOG ACTIVE BEFORE INSERT POSITION 0 AS begin" +
                " if (new.ID is null) then  new.ID = GEN_ID(GEN_COD_TRACK,1); new.UpdateRow = 'now'; " +
                " if (new.Number is null) then  new.Number = 0; if (new.Treck_Name is null) then  new.Treck_Name = ''; if (new.Performer_Name is null) then  new.Performer_Name = ''; " +
                " if (new.Vocls is null) then  new.Vocls = 0; if (new.pro is null) then  new.pro = ''; if (new.Language is null) then  new.Language = ''; " +
                " if (new.TypeMuzik is null) then  new.TypeMuzik = -1; " +
                "END";

                // Insert Into

                LocalQuery.ExecuteUNIScalar();

                #endregion

                #region Таблица Афиши

                // Формирование гениратор
                LocalQuery.UserQuery = "CREATE GENERATOR GEN_COD_AFICHA";

                LocalQuery.ExecuteUNIScalar();
                LocalQuery.UserQuery = "SET GENERATOR GEN_COD_AFICHA TO 0";

                LocalQuery.ExecuteUNIScalar();

                LocalQuery.UserQuery = "CREATE TABLE KARAOKE_AFICHA  (ID  COD_ID,  " +
                  "Data             TIMESTAMP  ,  " +
                  "TimeIn          VARCHAR(10)     CHARACTER SET UTF8  ,  " +
                  "TimeOut      VARCHAR(10)    CHARACTER SET UTF8  ,  " +
                  "Name_Group              VARCHAR(100)    CHARACTER SET UTF8 ,  " +
                  "Discription_Style                VARCHAR(200)    CHARACTER SET UTF8  , " +
                  "UpdateRow          TIMESTAMP NOT  NULL" +
                  " )";

                LocalQuery.ExecuteUNIScalar();

                LocalQuery.UserQuery = "CREATE OR ALTER TRIGGER BI_KARAOKE_AFICHA for KARAOKE_AFICHA ACTIVE BEFORE INSERT POSITION 0 AS begin" +
                " if (new.ID is null) then  new.ID = GEN_ID(GEN_COD_AFICHA,1); new.UpdateRow = 'now'; " +
                " if (new.TimeIn is null) then  new.TimeIn = ''; if (new.TimeOut is null) then  new.TimeOut = ''; " +
                " if (new.Name_Group is null) then  new.Name_Group = ''; if (new.Discription_Style is null) then  new.Discription_Style = ''; " +
                "END";

                // Insert Into

                LocalQuery.ExecuteUNIScalar();



                #endregion


                #region Таблица Доп услуги

                // Формирование гениратор
                LocalQuery.UserQuery = "CREATE GENERATOR GEN_COD_DOPUSLUGA";

                LocalQuery.ExecuteUNIScalar();
                LocalQuery.UserQuery = "SET GENERATOR GEN_COD_DOPUSLUGA TO 0";

                LocalQuery.ExecuteUNIScalar();

                LocalQuery.UserQuery = "CREATE TABLE KARAOKE_DOPUSLUGA_INVOISPAGE  (ID  COD_ID,  " +
                  "Name_Tovar              VARCHAR(150)    CHARACTER SET UTF8 ,  " +
                  "Discription_Cost                VARCHAR(50)    CHARACTER SET UTF8  , " +
                  "UpdateRow          TIMESTAMP NOT  NULL" +
                  " )";

                LocalQuery.ExecuteUNIScalar();

                LocalQuery.UserQuery = "CREATE OR ALTER TRIGGER BI_KARAOKE_DOPUSLUGA_INVOISPAGE for KARAOKE_DOPUSLUGA_INVOISPAGE ACTIVE BEFORE INSERT POSITION 0 AS begin" +
                " if (new.ID is null) then  new.ID = GEN_ID(GEN_COD_DOPUSLUGA,1); new.UpdateRow = 'now'; " +
                " if (new.Name_Tovar is null) then  new.Name_Tovar = ''; if (new.Discription_Cost is null) then  new.Discription_Cost = ''; " +
                "END";

                // Insert Into

                LocalQuery.ExecuteUNIScalar();



                #endregion


                #region Таблица Новости

                // Формирование гениратор
                LocalQuery.UserQuery = "CREATE GENERATOR GEN_COD_NEWS";

                LocalQuery.ExecuteUNIScalar();
                LocalQuery.UserQuery = "SET GENERATOR GEN_COD_NEWS TO 0";

                LocalQuery.ExecuteUNIScalar();

                LocalQuery.UserQuery = "CREATE TABLE KARAOKE_NEWS  (ID  COD_ID,  " +
                  "Name_News              VARCHAR(150)    CHARACTER SET UTF8 ,  " +
                  "Anons_txt              VARCHAR(200)    CHARACTER SET UTF8 ,  " +
                  "Discription_News              ITXT_BLOB  , " +
                  "Discription_News_Puth        VARCHAR(200)    CHARACTER SET UTF8, " +
                  "Image_Small ITXT_BLOB, " +
                  "Image_Small_Puth        VARCHAR(200)    CHARACTER SET UTF8, " +
                  "Image_News ITXT_BLOB, " +
                  "Image_News_Puth        VARCHAR(200)    CHARACTER SET UTF8, " +
                  "Data             TIMESTAMP default 'now' ,  " +
                  "Image_Baner ITXT_BLOB, " +
                  "Image_Baner_Puth        VARCHAR(200)    CHARACTER SET UTF8, " +
                  "UpdateRow          TIMESTAMP default 'now' " +
                  " )";

                LocalQuery.ExecuteUNIScalar();
               
                LocalQuery.UserQuery = "CREATE OR ALTER TRIGGER BI_KARAOKE_NEWS for KARAOKE_NEWS ACTIVE BEFORE INSERT POSITION 0 AS begin" +
                " if (new.ID is null) then  new.ID = GEN_ID(GEN_COD_NEWS,1); " +
                "END";

                // Insert Into

                LocalQuery.ExecuteUNIScalar();



                #endregion



                //var Test = "CREATE TABLE KARAOKECATALOG  (ID  INTEGER  NOT NULL PRIMARY KEY, " +
               //     "NAMESYS  VARCHAR(100)  NOT NULL, " +
               //     "NAMEBD  VARCHAR(15)  NOT NULL, " +
               //     "FIRSTNME    VARCHAR(100)   NOT NULL, " +
               //     "LASTNAME  VARCHAR(100)  NOT NULL,  " +
               //     "FATHERNAME    VARCHAR(100)     NOT NULL, " +
               //     "RESERV1    VARCHAR(100)     NOT NULL, " +
               //     "RESERV2    VARCHAR(100)     NOT NULL, " +
               //     "RESERV3    INTEGER     NOT NULL, " +
               //     "RESERV4    INTEGER     NOT NULL )";


                //LocalQuery.ExecuteUNIScalar();

                // Не совсем обязатльно
                //LocalQuery.ExecuteQueryLoadTable("KARAOKE_CATALOG", "Fill");

                //LocalQuery.FBbdSqlDefAdapter.Update();

            }
            catch (FirebirdSql.Data.FirebirdClient.FbException exFB)
            {

                // -206 - не ввел параметр
                SystemConectoServers.ErorDebag("При обращении к БД ..., возникло исключение: " + Environment.NewLine +
                       " === IDCode: " + exFB.ErrorCode.ToString() + Environment.NewLine +
                       " === Message: " + exFB.Message.ToString() + Environment.NewLine +
                       " === Exception: " + exFB.ToString(), 0, NameFileLog);
                //335544721 - обрыв соединения;
                //if (exFB.ErrorCode == 335544721)
                //{ }
                return false;
            }
            catch (Exception ex)
            {
                // Протоколировать исключение
                SystemConectoServers.ErorDebag("При обращении к БД ..., возникло исключение:" + Environment.NewLine +
                " === Message: " + ex.Message.ToString() + Environment.NewLine +
                " === Exception: " + ex.ToString(), 0, NameFileLog);
                return false;
            }




            return true;
        }

        #endregion


        #region Конвертация byte в string

        const int MAX_BUFFER_SIZE = 2048;
        static Encoding enc8 = Encoding.UTF8;

        /// <summary>
        ///  Конвертация byte в string
        /// </summary>
        /// <param name="fStream"></param>
        /// <returns></returns>
        private static string ReadFromBuffer(FileStream fStream)
        {
            Byte[] bytes = new Byte[MAX_BUFFER_SIZE];
            string output = String.Empty;
            Decoder decoder8 = enc8.GetDecoder();

            while (fStream.Position < fStream.Length)
            {
                int nBytes = fStream.Read(bytes, 0, bytes.Length);
                int nChars = decoder8.GetCharCount(bytes, 0, nBytes);
                char[] chars = new char[nChars];
                nChars = decoder8.GetChars(bytes, 0, nBytes, chars, 0);
                output += new String(chars, 0, nChars);
            }
            return output;
        }

        #endregion


        #region Взять значение атрибута тега их XmlTextReader
        /// <summary>
        /// Взять значение атрибута тега их XmlTextReader
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="Name"></param>
        /// <returns></returns>
        private static string HasAttributesName(XmlTextReader reader, string Name)
        {
            string Value = "";

            if (reader.HasAttributes)
            {
                for (int x = 0; x < reader.AttributeCount; x++)
                {
                    // Перейти к атрибуту
                    reader.MoveToAttribute(x);

                    if (reader.LocalName.ToString() == Name)
                    {

                        Value = reader.GetAttribute(x);

                    }
                }

            }
            return Value;
        }
        #endregion


        #region Поток, который слушает собеседника ---Один поток---- Макет

        /// <summary>
        /// Поток, который слушает собеседника 
        /// </summary>
        /// <param name="vm"></param>
        /// <param name="peer"></param>
        private static void Listen_(InfoProtokol _InfoProtokol, int TypeStart = 0)
        {

            // TypeStart 0 - стартовая загрузка потока 1 - востоновление после сбоя

            //int requestCount = 0;
            //byte[] bytesFrom = new byte[10025];
            //string dataFromClient = null;
            //Byte[] sendBytes = null;
            //string serverResponse = null;
            //string rCount = null;
            //requestCount = 0;

            // Назначаем сокет локальной конечной точке и слушаем входящие сокеты
            try
            {

                string AvtorizeClient = "";

                // Начинаем слушать соединения Синхронный сервер
                while (true)
                {
                    //if (_InfoProtokol.DebugInfo)
                    //    SystemConectoServers.ErorDebag("Ожидаем соединение через порт {" + _InfoProtokol.peer.ToString() + "}", 3, NameFileLog);


                    // Программа приостанавливается, ожидая входящее соединение
                    //using (Socket handler = sListener.Accept())
                    //{
                    Socket handler = _InfoProtokol.Socket.Accept();
                    //Unix


                    string data = null;
                    if (_InfoProtokol.DebugInfo)
                        SystemConectoServers.ErorDebag("Подключился клиент: " + IPAddress.Parse(((IPEndPoint)handler.RemoteEndPoint).Address.ToString()) + "  номер порта:  " + ((IPEndPoint)handler.RemoteEndPoint).Port.ToString(), 3, NameFileLog);

                    //IPAddress.Parse (((IPEndPoint)s.LocalEndPoint).Address.ToString ()) + "I am connected on port number " + ((IPEndPoint)s.LocalEndPoint).Port.ToString ()

                    // --------------------------------- Быстрый вывод информации
                    // #0,250,UnixTime(100000000),UnixTime(100000000),UnixTime(100000000)#
                    // 0 - Karaoke-Название заведения
                    // 1 - колонка воспроизведение композиции: 0 - композиция не воспроизводится  1 - композиция звучит
                    // 2 - колонка код воспроизводимой песни  352
                    // 3 - Название песни, что проигрывается 
                    // 4 - Результат голсования
                    // 5 - обновление информации о каталоге песен (количество песен в каталоге - отказался)
                    // 6 - обновление информации о правилах в заведении (отдельно окно): версия обновления ( есть обновления если код обновления поменялся) формат 02/03/2013/22:33 (то бишь время)
                    // 7 - обновление информации о новости в заведении (отдельно окно): версия обновления ( есть обновления если код обновления поменялся) формат 02/03/2013/22:33 (то бишь время)
                    // 8 - Доп услуги
                    // 9 - Банеры

                    // С помощью байтов проблема с кодировкой кирилицы
                    // Кодировки описание команд GetEncoding http://msdn.microsoft.com/en-us/library/system.text.encodinginfo.getencoding%28VS.85%29.aspx
                    // кодировка для telnet (телнета) windows

                    // Encoding cp866 = Encoding.GetEncoding("cp866");
                    // byte[] msg = cp866.GetBytes(MessageNoAvtorize);

                    var PlayCopozition = 1; var IDPlayCopozition = 234; var TimeLoadCatalog = ConectoWorkSpace.SystemConectoTimeServer.DateTimetoUnixTime(new DateTime(1970, 1, 1, 0, 0, 0, 0));

                    string MessageNoAvtorize = string.Format("Karaoke-#{0},{1},{2}#\r\npasword[пароль]: ", PlayCopozition, IDPlayCopozition, TimeLoadCatalog);
                    byte[] msg = Encoding.UTF8.GetBytes(MessageNoAvtorize);
                    handler.Send(msg);
                    // --------------------------------- Быстрый вывод информации



                    // Установка протокола 

                    // Мы дождались клиента, пытающегося с нами соединиться
                    byte[] bytes = new byte[1024];
                    int bytesRec = handler.Receive(bytes);

                    data += Encoding.UTF8.GetString(bytes, 0, bytesRec);

                    // Показываем данные в логе
                    if (_InfoProtokol.DebugInfo)
                        SystemConectoServers.ErorDebag("Полученный текст: {" + data + "}\n\n", 3, NameFileLog);

                    //Если значение в файле отличается то сейчас заканчиваю авторизацию и запрос на скачивание песен.
                    //Mobile_Android_:{ip_компьютера}
                    //лог пароль -
                    //Потом зашифруем хешом я дам код!

                    // Если удачно получили пароль и логин ответить - 200
                    if (AvtorizeClient.Length > 0)
                    {
                        //data

                        // Обработка команд
                        //switch (switch_on)
                        //{
                        //    default:
                        //}
                    }



                    // Отправляем ответ клиенту\
                    string reply = data.Length.ToString();
                    if (_InfoProtokol.DebugInfo)
                        reply = "Спасибо за запрос в " + reply + " символов";
                    //byte[]
                    msg = Encoding.UTF8.GetBytes(reply);
                    handler.Send(msg);

                    if (data.IndexOf("<Exit />") > -1)
                    {
                        if (_InfoProtokol.DebugInfo)
                            SystemConectoServers.ErorDebag("Сервер завершил соединение с клиентом.", 0, NameFileLog);

                        break;
                    }

                    // SocketShutdown — это перечисление, содержащее три значения для остановки: Both - останавливает отправку и получение данных сокетом,
                    // Receive - останавливает получение данных сокетом и Send - останавливает отправку данных сокетом.
                    // Завершение работы 
                    handler.Shutdown(SocketShutdown.Both);
                    handler.Close();

                    //}

                }
            }
            catch (SocketException exc)
            {
                // 10035 == WSAEWOULDBLOCK
                if (exc.NativeErrorCode.Equals(10035))
                    SystemConectoServers.ErorDebag("Клиент подключен, но отправка пакетов заблокирована!", 0, NameFileLog);
                else
                {
                    SystemConectoServers.ErorDebag("Socket exception: Клиент отключился: код ошибки " + exc.NativeErrorCode + Environment.NewLine +
                        " === Message: " + exc.Message.ToString() + Environment.NewLine +
                        " === Exception: " + exc.ToString() + Environment.NewLine +
                        " === ErrorCode: " + exc.SocketErrorCode, 0, NameFileLog);
                }
            }
            catch (Exception exc)
            {
                SystemConectoServers.ErorDebag("Socket exception: " + Environment.NewLine +
                       " === Message: " + exc.Message.ToString() + Environment.NewLine +
                       " === Exception: " + exc.ToString(), 0, NameFileLog);
            }
            finally
            {

            }

        }

        #endregion



    }
}
