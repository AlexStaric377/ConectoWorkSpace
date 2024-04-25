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
    class SystemConectoServers
    {

        #region Глобальные параметры

        /// <summary>
        /// Расположение файлов для серверов
        /// </summary>
        public static string PutchConecto = @"C:\Program Files\Conecto\";

        /// <summary>
        /// Расположение файлов для серверов
        /// </summary>
        public static string PutchServer = @"C:\Program Files\Conecto\Servers\";

        /// <summary>
        /// Расположение БД для серверов
        /// </summary>
        public static string PutchServerData = @"C:\Program Files\Conecto\Servers\data\";  

        /// <summary>
        /// Расположение временных файлов для серверов
        /// </summary>
        public static string PutchServerTmp = @"C:\Program Files\Conecto\Servers\tmp\";


        /// Расположение логов для серверов
        /// </summary>
        public static string PutchServerLog = @"C:\Program Files\Conecto\Servers\log\";

        /// Расположение библиотек для серверов
        /// </summary>
        public static string PutchServerDll = @"C:\Program Files\Conecto\Servers\dll\";

        /// <summary>
        /// Название общего лога
        /// </summary>
        public static string NameLog = "netserver.log";

        /// <summary>
        /// Расположение общих библиотек для серверов
        /// </summary>
        public static string PutchLib = @"C:\Program Files\Conecto\Bin\";

        #endregion


        #region Директории серверов

        public static void DirServer()
        {
            string[] Putch = new string[5] { PutchServerDll, PutchServerData, PutchServerLog, PutchServerTmp, PutchLib };

            for (int i = 0; i < Putch.Length; i++)
            {
                if (!SystemConecto.DIR_(Putch[i]))
                    SystemConecto.STOP = true;
            }
        }


        #endregion


        #region Включение серверов во время загрузки приложения

        // Есть внешнии сервера, сервера службы для Windows, интегрированные


        #region Комментарии и рекомендации для серверов

        // Конвертация порта из настроек
        // Int32 listenedPort = Convert.ToInt32(_viewModel.PeerPort);

        // netstat -a - проверка портов  netstat -a | find "LISTENING"

        // telnet 127.0.0.1 50700

        // Занят 50560 - для сервера SQLMS 2008
        // 50700 - для мобильных устройств
        // Таблица портов http://ru.wikipedia.org/wiki/%D0%A1%D0%BF%D0%B8%D1%81%D0%BE%D0%BA_%D0%BF%D0%BE%D1%80%D1%82%D0%BE%D0%B2_TCP_%D0%B8_UDP
        // Int32 port = 13;

        #endregion



        /// <summary>
        /// Запуск серверов
        /// </summary>
        public static void ServersStart()
        {
            // Параметры находятся в файле читаем с помощью ConfigControl
            // aServersOption = new Dictionary<int, Dictionary<string, string>>()
            string PutchFileConfig = PutchServer + "servers.xml";
            if (ConectoWorkSpace.ConfigControllServer.ReadConfigServers(PutchFileConfig))
            {
                foreach (var OptionServer in ConectoWorkSpace.ConfigControllServer.aServersOption)
                {
                    // Проверка автостарта
                    int IDProcess = Convert.ToInt32(OptionServer.Value["id"]);
                    if (OptionServer.Value["AutoStatrt"] == "true" && IDProcess > 0)
                    {
                        // тип сервера
                        switch (OptionServer.Value["TypeServer"].ToLower())
                        {
                            case "integer":

                                // Проверка метода
                               string[] aClass = OptionServer.Value["StartMetod"].Split('.');
                               if (aClass.Length > 1)
                               {
                                   string NameMetod = aClass[aClass.Length-1];
                                   string NameClass = OptionServer.Value["StartMetod"].Remove(OptionServer.Value["StartMetod"].Length - NameMetod.Length - 1);
                                   // Поиск класса
                                   Type SherchClass = Type.GetType(NameClass);


                                   // SherchClass.GetType() то же самое, что и typeof(ConectoWorkSpace.KaraokeServer), однако если Type это уже SherchClass

                                   if (SherchClass != null)
                                   {
                                       //System.Reflection.MethodInfo loadAppEvents = typeof(ConectoWorkSpace.KaraokeServer).GetMethod(NameMetod, new Type[] { }); // typeof(void)
                                       System.Reflection.MethodInfo loadAppEvents = SherchClass.GetMethod(NameMetod, new Type[] { typeof(string), typeof(Int32) });
                                       ;
                                       if (loadAppEvents != null)
                                       {
                                           // SystemConecto.ErorDebag("LoadAppEvents_" + IdApp, 2);
                                           loadAppEvents.Invoke(new object(), new object[] { PutchFileConfig, IDProcess });
                                       }

                                   }
                               }

                                break;
                            default:
                                break;
                        }

                    }

                }
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
        public static void ErorDebag(string Message, int TypeError = 0, string NameFileLog = "netserver.log")
        {
            // Не выводить сообщения при отключенной отладке
            // Откладка отключается при настройки логирования системы в администрировании
            if (!SystemConecto.DebagApp && TypeError == 1)
            {
                return;
            }

            string _PutchServerLog = PutchServerLog + NameFileLog;

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
                //textall = (!(File.Exists(_PutchServerLog)) ? "Id;Дата;Тип сообщения;Сообщение;" + Environment.NewLine : "") + text;

                string HeadLog = "Id;Дата;Тип сообщения;Сообщение;";


                // Отслеживание ошибок в досупе к файлу лога в многопотоковой среде 
                try
                {               
                    // Проверка структуры
                    if (File.Exists(_PutchServerLog))
                    {
                        textall = text;

                        // Проверка размера файла
                        System.IO.FileInfo file = new System.IO.FileInfo(_PutchServerLog);
                        if (file.Length > 1048576)
                        {
                            // очитсить содержимое
                            File.WriteAllText(_PutchServerLog, "");
                            textall = HeadLog + Environment.NewLine + text;
                        }
                        // File.(PuthSysLog);

                    }
                    else
                    {
                        textall = HeadLog + Environment.NewLine + text;

                    }


                    using (StreamWriter FileSysLog = new StreamWriter(_PutchServerLog, true, win1251))
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
