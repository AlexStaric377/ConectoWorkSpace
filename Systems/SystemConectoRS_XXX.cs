#region импорт следующих имен пространств .NET:
    using System;
    using System.Collections.Generic;
    using System.Linq;
    // Управление вводом-выводом
    using System.IO;
    using System.Text;
    // локаль операционной системы
    using System.Globalization;
    // шифрование данных
    using System.Security.Cryptography;
    // Управление Xml
    using System.Xml;
    using System.Xml.Linq;
    // Управление сетью
    using System.Net;
    using System.Net.NetworkInformation;
    using System.Net.Sockets;
    using System.IO.Compression;
    /// Многопоточность
    using System.Threading;
    using System.Windows.Threading;
    // Удаленное управление компьютером WMI
    // 
    using System.Management;
    // Импорт библиотек Windows DllImport (управление питанием ОС, ...
    using System.Runtime.InteropServices;
    // Управление Изображениями
    using System.ComponentModel;
    // Ссылка в проекте MSV2010 добовляется ...
    using System.Drawing.Text;
    using System.Drawing;
    // Управление БД
    using System.Data;
    //---- объекты ОС Windows (Реестр, {Win Api} 
    using Microsoft.Win32;
    // --- Timer
    using System.Timers;
    // --- Process
    using System.Diagnostics;
    //--- WPF
    using System.Windows.Media;
    using System.Windows;
    using System.Windows.Controls;
    //--- COM-PORT RS-XXX
    using System.IO.Ports;

#endregion

namespace ConectoWorkSpace
{
    /// <summary>
    ///  Разделяемый класс по файлам (ключевое слово - partial)
    /// </summary>

    public partial class SystemConectoRS_XXX
    {
                
        #region Глобальные параметры (переменные)
        
        public static System.Timers.Timer WorkSpaceCOMServersTimer1 = null;

        /// <summary>
        /// Структура данных для многопотоковой среды (передача аргументов)
        /// </summary>
        struct RenderInfo
        {
            public string argument1 { get; set; }
            public string argument2 { get; set; }
            public string argument3 { get; set; }
        }


        #endregion

        public static void Main()
        {
            System.Timers.Timer WorkSpaceCOMServersTimer1 = new System.Timers.Timer();
            WorkSpaceCOMServersTimer1.Elapsed += new ElapsedEventHandler(TaskWorkSpace1);
            WorkSpaceCOMServersTimer1.Interval = 5000; // каждые пять секунд
            WorkSpaceCOMServersTimer1.Start();

            // ------- Выполнение стартового кода

        }





        #region Ядро COM-PORT серверов

        #region Перечень потоков (выполняемых задач) Tick_a
            // Tick_a[0] - Нулевой тик  для Запуска СКД сервера
        #endregion

        /// <summary>
        /// Временные отметки для выполнения кода
        /// </summary>
        private static int[] Tick_a = new int[9] { 0, 0, 0, 0, 0, 0, 0, 0, 0 };
        /// <summary>
        /// Режимы тиков (каждый тик может еще иметь свои режимы: Режим -7 для всех потоков - поток выключен )
        /// </summary>
        private static int[] TickRg_a = new int[9] { 0, 0, 0, 0, 0, 0, 0, 0, 0 };
        /// <summary>
        /// Предыдущие состояния тиков (каждый тик может еще иметь свое предыдущие состояние)
        /// </summary>
        private static int[] TickMemory_a = new int[9] { 0, 0, 0, 0, 0, 0, 0, 0, 0 };


        #region Запуск выпоняемых серверо №1 проверка с периодичностью
        /// <summary>
        /// Описание выпоняемых задач №1
        /// </summary>
        /// <param name="source"></param>
        /// <param name="e"></param>
        public static void TaskWorkSpace1(object source, ElapsedEventArgs e)
        {
            // Тик 1 Время
            if (TickRg_a[0] > -7 && Tick_a[0] > 1 )
            {
                if (TickMemory_a[0] < 3)
                {
                    // проверка Разрешения запуска сервера
                    if (ListSKDCOMServer())
                    {
                        // MessageBox.Show("test");
                        // Сброс тика
                        Tick_a[0] = 0;
                        TickMemory_a[0] = 3;

                        RenderInfo Arguments01 = new RenderInfo() { };
                        Thread thStartTimer01 = new Thread(Server_SKD);
                        thStartTimer01.SetApartmentState(ApartmentState.STA);
                        thStartTimer01.IsBackground = true; // Фоновый поток
                        thStartTimer01.Start(Arguments01);
                    }
                }
                else
                {
                    // Режим чтения (ожидания) данных на COMPORT перегрузка каждые 24 часа (нужно настроить ночью)                    
                    // проверка Разрешения запуска сервера
                    if (ListSKDCOMServer())
                    {
                        //_serialPort.Write("I\r\n");
                    }
                }
            }
            // Нулевой тик  для Запуска СКД сервера
            Tick_a[0] = Tick_a[0] + 1;
        }

 
        /// <summary>
        /// Открытый порт
        /// </summary>
        static SerialPort _serialPort;
        /// <summary>
        /// Чтение данных в строку
        /// </summary>
        static string sBuffer = String.Empty;
        /// <summary>
        /// Чтение данных по байтам
        /// </summary>
        static List<byte> bBuffer = new List<byte>();



        /// <summary>
        /// Работа сервера
        /// </summary>
        /// <param name="ThreadObj"></param>
        private static void Server_SKD(object ThreadObj)
        {
            string T_error = null;
            
            // Запуск
           // string name;
           
           // StringComparer stringComparer = StringComparer.OrdinalIgnoreCase;
            //Thread readThread = new Thread(Read);
            
            // ---- Проверка открытия порта  -- Провести Тестif (_serialPort.IsOpen)
            try
            {
                if (_serialPort == null)
                {
                    // Create a new SerialPort object with default settings.
                    _serialPort = new SerialPort(ListSKDCOMPortName());
                    // Allow the user to set the appropriate properties.
                    // _serialPort.PortName = ListSKDCOMPortName();
                    // var BaudRatePort = GetBaudRate(14);
                    //MessageBox.Show(_serialPort.BaudRate.ToString());
                    _serialPort.BaudRate = GetBaudRate(11)[0];
                    //MessageBox.Show(_serialPort.BaudRate.ToString());
                    //_serialPort.BaudRate = SetPortBaudRate(_serialPort.BaudRate);
                    _serialPort.Parity = Parity.None;
                    //_serialPort.Parity = SetPortParity(_serialPort.Parity);
                    _serialPort.StopBits = StopBits.One;
                    //_serialPort.StopBits = SetPortStopBits(_serialPort.StopBits);
                    _serialPort.DataBits = 8;
                    //_serialPort.DataBits = SetPortDataBits(_serialPort.DataBits);
                    _serialPort.Handshake = Handshake.None;
                    //_serialPort.Handshake = SetPortHandshake(_serialPort.Handshake);

                    // Set the read/write timeouts
                    _serialPort.ReadTimeout = 500;
                    _serialPort.WriteTimeout = 500;

                    // Обработчик события передачи данных
                    _serialPort.DataReceived += new SerialDataReceivedEventHandler(DataReceviedHandler);

                    // Обработчик события ошибок
                    _serialPort.ErrorReceived += new SerialErrorReceivedEventHandler(ErrorReceivedHandler);

                    _serialPort.Open();
                    //_continue = true;
                    //readThread.Start();
                    MessageBox.Show("запуск1"); 
                }
                MessageBox.Show("запускN"); 
                //Console.Write("Name: ");
                //name = Console.ReadLine();

                //Console.WriteLine("Type QUIT to exit");

                // Отправка команды

                // Ente 13	0x0D

                //int intReturnASCII = 0;
                //char charReturnValue = (Char)intReturnASCII;

               //_serialPort.WriteLine("I" + "\r\n");

               //_serialPort.Write("I\r\n");

                // Тест
                //int count = _serialPort.BytesToWrite;
                //if (count > 0)
                //{
                //    MessageBox.Show("Отправил"+_serialPort.ReadExisting());
                //}
               

                Thread.Sleep(216000000);


                //while (_continue)
                //{
                //    // Выключить
                //    if (TickRg_a[0] == -7)
                //    {
                //        _continue = false;


                //    }

                //    //    message = Console.ReadLine();

                //    //    if (stringComparer.Equals("quit", message))
                //    //    {
                //    //        _continue = false;
                //    //    }
                //    //    else
                //    //    {
                //    //        _serialPort.WriteLine(
                //    //            String.Format("<{0}>: {1}", name, message));
                //    // }
                //}

                //readThread.Join();
                //_serialPort.Close();
                TickMemory_a[0] = 1;
            }
            catch (TimeoutException) { T_error = "Долго ждем открытие порта";   }
            catch (UnauthorizedAccessException) { T_error = "ОС запретила доступ к устройству"; }
            catch (IOException) { T_error = "Ошибки ввода-вывода"; }
            catch (ArgumentException) { T_error = "Параметры (аргументы) порта недопустимы"; }

            if(T_error != null){

                if (SystemConecto.DebagApp)
                {
                    MessageBox.Show(T_error+" - "+_serialPort.PortName);
                }
                else
                {
                    SystemConecto.ErorDebag(T_error + " - " + _serialPort.PortName);
                }
            }

        }



        private static void ProcessBuffer(string sBuffer)
        {
            // Поиск в массиве для полезной информации
            // Затем удалить полезные данные из буфера
            //MessageBox.Show("Данные поступили:"+sBuffer);

        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="bBuffer">List <byte> или byte[]</param>
        private static void ProcessBuffer(byte[] bBuffer)
        {
            // Поиск в массиве для полезной информации
            // Затем удалить полезные данные из буфера

            // Когда List <byte>
            //byte[] buffer = bBuffer.ToArray();
            //string dataReceived = Encoding.ASCII.GetString(buffer);
            // Когда byte[]
            string dataReceived = Encoding.ASCII.GetString(bBuffer);

            MessageBox.Show("Что-то есть:" + dataReceived);
            
        }

        /// <summary> Converts an array of bytes into a formatted string of hex digits (ex: E4 CA B2)</summary>
        /// <param name="data"> The array of bytes to be translated into a string of hex digits. </param>
        /// <returns> Returns a well formatted string of hex digits with spacing. </returns>
        private string ByteArrayToHexString(byte[] data)
        {
            StringBuilder sb = new StringBuilder(data.Length * 3);
            foreach (byte b in data)
                sb.Append(Convert.ToString(b, 16).PadLeft(2, '0').PadRight(3, ' '));
            return sb.ToString().ToUpper();
        }




        //static byte[] buffer = new byte[125];
        //static int offset = 0, toRead = 125;


        /// <summary>
        /// Поступили данные в буфер порта
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void DataReceviedHandler(
                        object sender,
                        SerialDataReceivedEventArgs e)
        {
            // Если необходимо переключиться между чтением текста и чтением двоичных данных из потока, выберите протокол,
            // четко различающий текст и двоичные данные, например чтение байтов и декодирование данных вручную.
            
            // Тест данные постпили
            //MessageBox.Show("Тест данные поступили");
            Thread.Sleep(200);
            // Используйте технологию чтения либо бинарный или строку  (но не оба)

            SerialPort com = (SerialPort)sender;
            // Буфер данных строки
            //string indata = com.ReadExisting();
            //sBuffer += com.ReadExisting();
            //ProcessBuffer(sBuffer);

            // Буфер данных и процесс двоичной
            // Способ 1
            //while (com.BytesToRead > 0)
            //    bBuffer.Add((byte)com.ReadByte());
                    
                    // Способ 2
                    //int read;
                    //while (toRead > 0 && (read = com.Read(buffer, offset, toRead)) > 0)
                    //{
                    //    offset += read;
                    //    toRead -= read;
                    //}
                    //if (toRead > 0) throw new EndOfStreamException();
                    // you now have all the data you requested

            // Способ 3
            byte[] bBuffer = new byte[com.ReadBufferSize];
            //Read all bytes received
            var BytesRead = com.Read(bBuffer, 0, bBuffer.Length);

            ProcessBuffer(bBuffer);
        }

        private static void ErrorReceivedHandler(
                        object sender,
                        SerialErrorReceivedEventArgs e)
        {
           //SerialPort sp = (SerialPort)sender;
           // string indata = sp.ReadExisting();
            // Прочел
            SerialPort com = (SerialPort)sender;
            if (SystemConecto.DebagApp)
            {
                MessageBox.Show("Ошибка: " + e.ToString() + " - " + com.PortName);
            }
            else
            {
                SystemConecto.ErorDebag(e.ToString() + " - " + com.PortName);
            }
        }


        #endregion
        #endregion


        #region Тесты чтения портов
        public static void Read()
        {
            //while (_continue)
            //{
            //    try
            //    {
            //        //string message = _serialPort.ReadLine();
            //        //Console.WriteLine(message);
            //        System.Windows.Application.Current.Dispatcher.Invoke(new Action(delegate()
            //        {

            //        }));
            //    }
            //    catch (TimeoutException) { }
            //}
        }

        #endregion



        #region Настройки портов чтение из системы


        /// <summary>
        /// Возвращает списк установленных портов (в том числе эмуляций)
        /// </summary>
        /// <param name="defaultPortName"></param>
        /// <returns></returns>
        public static string[] GetPortName(string defaultPortName="")
        {
            string[] portName = new String[SerialPort.GetPortNames().Length];
            var int_a = 0;
            foreach (string portname in SerialPort.GetPortNames())
            {
                portName[int_a] = portname;
                // comboBox1.Items.Add(portname); //добавить порт в список
                int_a = int_a+1;
                //MessageBox.Show(portname);
            }
            
            // comboBox1.SelectedIndex = 0;
            //if (portName == "")
            //{
            //    portName = defaultPortName;
            //}
            //return portName; //возвращает установленный порт по умолчанию
            return portName;
        }

        /// <summary>
        /// Установка порта по умолчанию (при выборе порта на устройство)
        /// </summary>
        /// <param name="defaultPortName"></param>
        /// <returns></returns>
        public static string SetDefPortName(string defaultPortName)
        {
            string portName;

            Console.WriteLine("Available Ports:");
            foreach (string s in SerialPort.GetPortNames())
            {
                Console.WriteLine("   {0}", s);
            }

            Console.Write("COM port({0}): ", defaultPortName);
            portName = Console.ReadLine();

            if (portName == "")
            {
                portName = defaultPortName;
            }
            return portName;
        }



        /// <summary>
        /// Список скоростей
        /// </summary>
        /// <param name="arrayLink"> Указанная скорость</param>
        /// <returns></returns>
        public static int[] GetBaudRate(int arrayLink = -1)
        {
            int[] listbaudrate = new int[16];
            listbaudrate[0] = 110;
            listbaudrate[1] = 300;
            listbaudrate[2] = 600;
            listbaudrate[3] = 1200;
            listbaudrate[4] = 2400;
            listbaudrate[5] = 4800;
            listbaudrate[6] = 9600;
            listbaudrate[7] = 14400;
            listbaudrate[8] = 19200;
            listbaudrate[9] = 38400;
            listbaudrate[10] = 56000;
            listbaudrate[11] = 57600;
            listbaudrate[12] = 115200;
            listbaudrate[13] = 128000;
            listbaudrate[14] = 230400;
            listbaudrate[15] = 256000;
            if (arrayLink > -1)
            {
                int[] listbaudrate_ = new int[1];
                listbaudrate_[0] = listbaudrate[arrayLink];
                return listbaudrate_;
            }
            //comboBox2.Items.AddRange(listbaudrate);
            //comboBox2.SelectedIndex = 6;
            return listbaudrate;
        }


        public static void ListCOMPortName()
        {

            // Модемы
            //string query = "SELECT * FROM Win32_POTSModem";
            // COM-PORT - SerialPort
            //string query = "SELECT * FROM Win32_SerialPort";
            // Список всех подключаемых устройств по Plag and Play - Win32_PnPEntity - Условие {WHERE ConfigManagerErrorCode = 0}
            // работает без ошибок
            string query = "SELECT * FROM Win32_PnPEntity WHERE ConfigManagerErrorCode = 0";

            // query = "SELECT * FROM Win32_Keyboard";
            

            string[] ModemObjects = new string[250];
            ManagementObjectSearcher searcher = new ManagementObjectSearcher(query);
            foreach (ManagementObject obj in searcher.Get())
            {
                // Win32_POTSModem
                // MessageBox.Show(obj["Name"].ToString() + "(" + obj["AttachedTo"].ToString() + ")");
                // Win32_PnPEntity
                // MessageBox.Show(obj["Name"].ToString());
                foreach (PropertyData property in obj.Properties)
                {
                    SystemConecto.ErorDebag("Устройство: " +property.Name +"//"+ property.Value, 2);
                }

            }

            // objectQuery = new ObjectQuery("SELECT * FROM Win32_PnPEntity WHERE ConfigManagerErrorCode = 0");

            //ManagementObjectSearcher comPortSearcher = new ManagementObjectSearcher(connectionScope, objectQuery);

             //using (comPortSearcher)

             //{



        }

        #endregion


        /// <summary>
        /// Список портов СКД системы контроля доступа
        /// </summary>
        public static string ListSKDCOMPortName()
        {
            return "COM10";
        }

        /// <summary>
        /// Включение сервера СКД
        /// </summary>
        public static bool ListSKDCOMServer()
        {
            // Читаем настройки ПО

            return true;
        }


        //---------------- End Class
    }





}
