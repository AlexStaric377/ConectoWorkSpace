// ==================================== Управление файлами конфигураций
// Conecto.ua
// Starichenko E. A.
// 

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
// Управление Xml
using System.Xml;
using System.Xml.Linq;
// шифрование данных
using System.Security.Cryptography;
// Управление вводом-выводом
using System.IO;
// Регулярные выражения
using System.Text.RegularExpressions;
// Отладка Messagebox
using System.Windows;

// ==================================== Используем функции ядра SystemConecto

namespace ConectoWorkSpace
{
    /// <summary>
    ///  Разделяемый класс по файлам (ключевое слово - partial)
    /// </summary>

    public partial class SystemConfigControll // SystemConecto
    {

        #region Глобальные параметры класса (переменные)


        // 7. Запуск блокировки файлов - функция безопасности
        // AppStart.File_(AppStart.PutchApp + "config.xml", 6, SystemConfig.XMLConfigFile);

        public XmlTextWriter WriterConfigXML = null;        // Объект - параметры конфигурации
        public XmlTextWriter WriterConfigUserXML = null;    // Объект - параметры пользователя
        public static string sData = "MainKopfMainKopfMainKopf7852Kopf"; // Создание строки для шифрования
        //"qwertyuiopasdfghjklzxcvbnmqwerty"
        /// <summary>
        /// Параметры приложения до изменения (откат на одно значение)
        /// </summary>
        public static Dictionary<string, string> aParamAppUndo = new Dictionary<string, string>();

        public static bool DiagnozParam = false; // значение выполнение диагностики последнего параметра

        /// <summary>
        /// AppStart.aLanguage - Мультязычнось Локаль приложения [new Dictionary<string, string>();]
        /// AppStart.aParamApp - Параметры приложения [new Dictionary<string, string>();]
        /// 
        /// </summary>



        #region Структура данных параметров свойств приложения

        /// <summary>
        /// Группа FTP доступа
        /// </summary>
        public struct AppPlayOptionStructFTP
        {
            /// <summary>
            /// код - группы, общая для всех 0 - отсутствует группа
            /// </summary>
            public int idGroupOverall { get; set; }
            /// <summary>
            /// текстовый адресс FTP сервера
            /// </summary>
            public string NameServer { get; set; }

            // public Bitmap Result { get; set; }
        }

        #region dll библиотеки

        /// <summary>
        /// Группа необходимых dll библиотек
        /// </summary>
        public struct AppPlayOptionStructdll
        {
            /// <summary>
            /// код - группы, общая для всех 0 - отсутствует группа
            /// </summary>
            public int idGroupOverall { get; set; }
            /// <summary>
            /// текстовый адресс FTP сервера
            /// </summary>
            public List<AppPlayOptionStructdllFile> File { get; set; }

            // public Bitmap Result { get; set; }
        }
        /// <summary>
        /// Файлы dll - необходимые для приложения
        /// </summary>
        public struct AppPlayOptionStructdllFile
        {
            public string NameFile { get; set; }
            public string versionFile { get; set; }
        }
        #endregion

        #region Опции чтения файла
        /// <summary>
        /// Опции чтения файла
        /// </summary>
        public struct ReadFileXMLOpcii
        {

            public Dictionary<int, Dictionary<string, string>> alOptionXML { get; set; }

            /// <summary>
            /// Структура параметров тега
            /// </summary>
            public dynamic StructParamTag { get; set; }

            /// <summary>
            /// Структура файла XML по умолчанию - динамическая
            /// </summary>
            public dynamic StructXMLFile { get; set; }
            /// <summary>
            /// Файл по умолчанию
            /// </summary>
            public string FileDefault { get; set; }
            /// <summary>
            /// Чтение только определеных параметров в системе -  0; читать параметры все - 1
            /// </summary>
            public int TypeReadParam { get; set; }
        }

        #endregion

        #endregion

        #endregion

        #region Описание класса


        /*
         * 
         * 
         *                                                                                         Чтение 
         *                                                                                         ReadConfigXML - всего файла
         *                                                                                         ReadConfigXML TypeFunc - (1 - чт. списка переменных не имеет смысла такак файл читаем весь!)
         *                                                                                           ||
         *                                                                                                             
         *              Окно пользователя   =>  Объекты окна  =>         Контроллер          =>    Память  <=>       Запись
         *                                      с данными              контроль над веденными    aParamApp[]   Хранение на  диске
         *                                                                данными                               в виде файлов
         *                                                                                                        
         *                                                                      ||
         * 
         *                                                                  Диагностика          =>   Запись в память
         *                                                                                                  ||
         * 
         *              Окно пользователя          <=                                                чтение из памяти
         * 
         *              config.xml     - настройки системы
         *              appplay.xml         - настройки приложений
         * 
         * 
         *      idGroupOverall - группа которая объединяет запросы для ускорения обработки задач. Одинаковые сервера, сервисы и т.д., ответ годится для группы приложений.
         * 
         */

        #endregion


        #region Операции с файлами конфигурации приложения

        #region Файл config.xml     - чтение настроек системы

        public bool ReadConfigXML(int TypeFunc = 0)
        {

            XmlTextReader reader = null;
            // Dictionary<string, string> aParamAppChek = new Dictionary<string, string>(); // Параметры приложения проверка
            int ConfigVer = 1;

            try
            {
                // Режим Компиляции Релиза приложения
                if (AppStart.ReleaseCandidate == "Release")
                {
                    // Чтение зашифрованого файла
                    StringReader FileText = new StringReader(AppStart.DecryptTextToFile(AppStart.PutchApp + "config.xml"));

                    reader = new XmlTextReader(FileText);
                    //var ReadFile = new StringReader(AppStart.DecryptTextFromFile(AppStart.PutchApp + "config.xml", SystemConfig.sData, @"м^ыiЎQRy3іљТФ>"));
                    //reader = new XmlTextReader(ReadFile);
                }
                else
                {
                    // Чтение обычного файла
                    reader = new XmlTextReader(AppStart.PutchApp + "config.xml");
                    
                    
                }
                reader.WhitespaceHandling = WhitespaceHandling.None; // пропускаем пустые узлы
                string nameElement = "";
                while (reader.Read())
                {
                    if (reader.NodeType == XmlNodeType.Element)
                        nameElement = reader.Name;
                    // Проверка параметров и версии конфигурации, а также целосность данных
                    // Исключение тег ?xml = ""
                    if (nameElement == "" || nameElement == "Параметры-Администратора")
                    {
                    }
                    else
                    {
                        // Создаем масив 
                        // AppStart.aParamApp.Add(reader.Name, "");
                        // reader.AttributeCount //Количесвто атрибутов
                        // reader.MoveToAttribute   // Перемещает по индексу на атрибут
                        // Чтение отрибутов универсальное
                        if (reader.HasAttributes)
                        {
                            for (int x = 0; x < reader.AttributeCount; x++)
                            {
                                // Перейти к атрибуту
                                reader.MoveToAttribute(x);
                                // Проверка версии файла
                                if (ConfigVer == 1 && nameElement == "FileConfig" && reader.LocalName == "FileConfig_Ver-File-Config" && reader.GetAttribute(x) == AppStart.aParamApp["FileConfig_Ver-File-Config"])
                                {
                                    ConfigVer = 0;
                                }
                                // Запись в память
                                // Проверка (на всякий случай)
                                if (AppStart.aParamApp.ContainsKey(reader.LocalName))
                                {
                                    AppStart.aParamApp[reader.LocalName] = reader.GetAttribute(x);
                                }
                                else
                                {
                                    // Запись отклонений
                                    //AppStart.ErorDebag("Отсутствует элемент в конфигурации: " + reader.LocalName + ", значение - " + reader.GetAttribute(x) + " Тег: " + nameElement); // Отладка
                                    ConfigVer = 2;
                                }



                                // Проверка параметров присутствие
                                // switch 

                                // aParamAppChek.Add(reader.Name + "-" +reader.LocalName, reader.GetAttribute(x));
                                // ErorDebag(reader.GetAttribute(x),1); // Отладка
                            }
                        }
                    }

                    //if (reader.Name == "Заказ")
                    //{
                    //    // Order order = new Order(reader.GetAttribute("Адрес"), DateTime.Parse(reader.GetAttribute("Дата")));

                    //    // получаем товары в заказе
                    //    while (reader.Read() && reader.Name == "Товар")
                    //    {

                    //    }
                    //        // order.AddGood(reader.GetAttribute("Название"), float.Parse(reader.GetAttribute("Цена")));
                    //}
                }
                // Проверка версии файла конфигурации
                if (ConfigVer > 0)
                {
                    // Исправить файл конфигурации
                    //AppStart.ErorDebag("Конфигурация не верна, приложение приступило к исправлению..."); // Отладка
                    // запись измененных параметров
                    if (reader != null)
                        reader.Close();
                    CreateConfigXML(2);
                    return true;
                }

            }
            catch (Exception ) //error
            {
                //AppStart.ErorDebag("Чтение конфиг файла вызвало исключение: " + Environment.NewLine +
                //    " === Файл: " + AppStart.PutchApp + "config.xml" + Environment.NewLine +
                //    " === Message: " + error.Message.ToString() + Environment.NewLine +
                //    " === Exception: " + error.ToString());
                if (reader != null)
                    reader.Close();
                // Перезапись по умолчанию параметров (нарушена конфигурация)
                //AppStart.ErorDebag("Конфигурация не верна, приложение приступило к исправлению настроек по умолчанию..."); // Отладка
                CreateConfigXML(2);
                return false;
            }
            if (reader != null)
                reader.Close();
            return true;
        }

        #endregion

        #region Файл config.xml     - создание и изменение настроек системы

        /// <summary>
        /// TypePr - 0 создать файл; 1 - создать только масив; 2 - запись измененных параметров
        /// </summary>
        /// <param name="TypePr"></param>
        public void CreateConfigXML(int TypePr = 0)
        {

            // XML текст String
            string xmlString = null;

            // ---- Примеры и информация
            // XmlTextWriter(TextWriter) 	Инициализирует новый экземпляр класса XmlTextWriter с помощью указанного класса TextWriter	
            // XmlTextWriter(Stream, Encoding) 	Создает экземпляр класса XmlTextWriter с помощью указанного потока и кодировки.
            // XmlTextWriter(String, Encoding)
            // Промежуточный текст
            // WriterConfigXML = new XmlTextWriter(new StringWriter());
            // WriterConfigXML.WriteElementString() создает элемент, содержащий одно текстовое значение, такое как <Дата>01.05.04</Дата>. 
            // Функция WriterConfigXML.WriteBinHex() массив байтов в шеснадцатиричном виде,

            // Файл является уникальным хранилищем настроек приложения, которые влияют на получение результата работы всей системы.
            // По умолчанию файл настривается как пользовательский и автономный (тоесть не подключен к интеренту или сети)
            // Алгоритм поведения приложения и файла настроек:
            //   Приложение считает важным файл настроек и пытается его уберечь от уничтожения изменения и прочего.
            //    a) заблокировать файл
            //    b) записать в реестр стартовый режим (если файл удален после стартового режима сообщить о данном инциденте
            //       тремя спосабами: в базу обслуживания копий в зашифрованный файл на компьюторе или реестр ОС, в лог файл)

            //       1. Если приложение определит подключение к интернету или основному серверу обслуживания копий приложения, 
            //   оно предпримет все необходимые шаги, для изменения супер пароля Autorize-pass-admin-Conecto для Conecto_root.
            //  2. Данный код будет доступен через систему считования карточек разного типа; карточки выдаются только сотрудникам Conecto.
            //  3. Возможно логирование входа, сотрудниками Conecto отдельно в зашифрованом файле. Доступ к данной информации ограничить.
            //  4. При уничтожении данного файла предупреждать основной сервер. (условие верно только для подключенных копий к сети).
            //  5. Система фиксирует первый старт в реестра ОС - датой.
            //  6. 

            try
            {
                // Проверка создания массива данных - настройки программы
                if (!AppStart.aParamApp.ContainsKey("FileConfig_Ver-File-Config"))
                {

                    // Версия конфигурационного файла
                    AppStart.aParamApp.Add("FileConfig_Ver-File-Config", "0.1");


                    #region Синхрониация работы рабочего места с сервером Времени
                    // Синхронизация с сервером включенна если указан другой адрес не 127.0.0.1 
                    AppStart.aParamApp.Add("Time_IP", "127.0.0.1");
                    // Активация TimeServer Local Net
                    AppStart.aParamApp.Add("Time_ServerSwitch", "1");
                    // Время синхронизации потока - авто 10 тиков по 5 секунд
                    AppStart.aParamApp.Add("Time_TimeSecond", "700");
                    #endregion

                    #region Общии настройки системы

                    // Не в режиме терминал с Рабочим столом Windows
                    AppStart.aParamApp.Add("Terminal_Explorer", "Yes");
                    AppStart.aParamApp.Add("Terminal_Putch-Terminal-Hiden", AppStart.PStartup + @"Conecto®WorkSpace.exe");
                    // Автостарт WorkSpace в Windows
                    AppStart.aParamApp.Add("Terminal_autostart", "0");
                    // Запсутить приложения через 20 секунд после старта WorkSpace
                    AppStart.aParamApp.Add("Terminal_autostart-time-sec", "20");

                    AppStart.aParamApp.Add("ServerUpdateConecto_DNS", "updatework.conecto.ua");
                    AppStart.aParamApp.Add("ServerUpdateConecto_IP", "");
                    // Версия клиента Update
                    AppStart.aParamApp.Add("ServerUpdateConecto_Ver", "0.1");
                    // Авто настройка порта FTP
                    AppStart.aParamApp.Add("ServerUpdateConecto_PortFTP", "21");
                    // Авто настройка порта WEB    
                    AppStart.aParamApp.Add("ServerUpdateConecto_PortWEB", "80");
                    // Пользователь Сервера  
                    AppStart.aParamApp.Add("ServerUpdateConecto_USER", "update_workspace");  // AppCoenectoWorkSpace
                    // Пароль Сервера
                    AppStart.aParamApp.Add("ServerUpdateConecto_USER-Passw", "conect1074");  // User-Pass2012        
                    #endregion



                    #region Установка ролей Рабочего места --- RolePC()

                    // Список активных серверов (коды, через точку с запятой)
                    AppStart.aParamApp.Add("RolePC_Servers", "");
                    // Список активных терминалов (ресторан и прочие) (коды, через точку с запятой)
                    AppStart.aParamApp.Add("RolePC_Terminal", "");
                    // Список активных офисных рабочих мест (коды, через точку с запятой)
                    AppStart.aParamApp.Add("RolePC_Office", "");
                    // Список активных планшетных рабочих мест (коды, через точку с запятой)
                    AppStart.aParamApp.Add("RolePC_Portable", "");
                    #endregion

                    //---     Авторизация пользователей --- Autoriz()

                    //WriterConfigXML.WriteStartElement("Авторизация");
                    //WriterConfigXML.WriteStartElement("Autorize");
                    //WriterConfigXML.WriteAttributeString("Admin", "sysdba");            
                    AppStart.aParamApp.Add("Autorize_Admin", "sysdba");                          // Администратор БД Fierberd
                    AppStart.aParamApp.Add("Autorize_Admin_Paswd", "alttabnew");                 // Администратор БД Fierberd
                    //WriterConfigXML.WriteAttributeString("pass", "alttab.20");
                    AppStart.aParamApp.Add("Autorize_pass", "B52");
                    //WriterConfigXML.WriteAttributeString("User-Windows", "B52");        
                    AppStart.aParamApp.Add("Autorize_User-Windows", "B52");                      // Учетная запись пользователя Виндовса
                    //WriterConfigXML.WriteAttributeString("pass-user-windows", "B52");
                    AppStart.aParamApp.Add("Autorize_pass-user-windows", "B52");

                    //WriterConfigXML.WriteAttributeString("Admin-Conecto", "B52");      
                    AppStart.aParamApp.Add("Autorize_Admin-Conecto", "Conecto_root");            // Супер пользователь
                    //WriterConfigXML.WriteAttributeString("pass-admin-Conecto", "B52");
                    AppStart.aParamApp.Add("Autorize-pass-admin-Conecto", "QWas123GHk");
                    //WriterConfigXML.WriteAttributeString("Admin-IT", "B52");            
                    AppStart.aParamApp.Add("Autorize_Admin-IT", "B52");                          // ИТ Администратор
                    //WriterConfigXML.WriteAttributeString("pass-admin-IT", "B52");
                    AppStart.aParamApp.Add("Autorize_pass-admin-IT", "37779");                       // 1245
                    AppStart.aParamApp.Add("Autorize_licenzija-id", "noNet");                    // Ключь лицензии Выдается на организацию (БД клиентов)
                    // WriterConfigXML.WriteComment("Авторизация пользователей БД, рабочего стола Conecto и прочего");
                    //WriterConfigXML.WriteEndElement();
                    //WriterConfigXML.WriteEndElement();
                    //--------------------
                    //WriterConfigXML.WriteStartElement("ДополнительныеПриложенияАдминистратора");
                    //WriterConfigXML.WriteStartElement("AddAppAdmin");
                    //WriterConfigXML.WriteAttributeString("Диспечер-задач", "Yes");
                    //AppStart.aParamApp.Add("AddAppAdmin_Диспечер-задач", "Yes");
                    ////WriterConfigXML.WriteAttributeString("Рабочий-стол-открыть-закрыть", "Yes");
                    //AppStart.aParamApp.Add("AddAppAdmin_Рабочий-стол-открыть-закрыть", "Yes");
                    ////WriterConfigXML.WriteAttributeString("Выполнить-команду", "Yes");
                    //AppStart.aParamApp.Add("AddAppAdmin_Выполнить-команду", "Yes");
                    ////WriterConfigXML.WriteAttributeString("Диспечер-устройств", "Yes");
                    //AppStart.aParamApp.Add("AddAppAdmin_Диспечер-устройств", "Yes");
                    ////WriterConfigXML.WriteAttributeString("Управление-службами", "Yes");
                    //AppStart.aParamApp.Add("AddAppAdmin_Управление-службами", "Yes");
                    //WriterConfigXML.WriteAttributeString("Перегрузить-сервер-ключа", "Yes");
                    //AppStart.aParamApp.Add("AddAppAdmin_Перегрузить-сервер-ключа", "Yes");
                    // WriterConfigXML.WriteComment("Запуск дополниельных приложений для Администратора");
                    //WriterConfigXML.WriteEndElement();
                    //WriterConfigXML.WriteEndElement();
                    //--------------------
                    //WriterConfigXML.WriteStartElement("ДополнительныеПриложенияПользователя");
                    //WriterConfigXML.WriteStartElement("AddAppUser");
                    //WriterConfigXML.WriteAttributeString("Экранная-клавиатура", "Yes");
                    //AppStart.aParamApp.Add("AddAppUser_Экранная-клавиатура", "Yes");
                    ////WriterConfigXML.WriteAttributeString("Калькулятор", "Yes");
                    //AppStart.aParamApp.Add("AddAppUser_Калькулятор", "Yes");
                    ////WriterConfigXML.WriteAttributeString("Блокнот", "Yes");
                    //AppStart.aParamApp.Add("AddAppUser_Блокнот", "Yes");
                    // WriterConfigXML.WriteComment("Запуск дополниельных приложений для Администратора");
                    //WriterConfigXML.WriteEndElement();
                    //WriterConfigXML.WriteEndElement();
                    //--------------------
                    //WriterConfigXML.WriteStartElement("СерверОбновленияConecto");
                    //WriterConfigXML.WriteStartElement("ServerUpdateConecto");
                    //WriterConfigXML.WriteAttributeString("DNS", "updatework.conecto.ua");

                    //WriterConfigXML.WriteAttributeString("Ver", "0.1");                        


                    AppStart.aParamApp.Add("ServerSKDSwitch", "0");                                     // Включение SKD Сервера


                    // WriterConfigXML.WriteComment("Доступ к серверу обновлений Приложения ConectoWorkSpase");
                    //WriterConfigXML.WriteEndElement();
                    //WriterConfigXML.WriteEndElement();

                    #region Старая версия
                    // ----------
                    //WriterConfigXML.WriteStartElement("ОпцииФайлаКонфигурации");
                    //WriterConfigXML.WriteStartElement("FileConfig");
                    //WriterConfigXML.WriteAttributeString("Ver-File-Config", "0.1");     

                    //WriterConfigXML.WriteEndElement();
                    //WriterConfigXML.WriteEndElement();
                    // ----------
                    //WriterConfigXML.WriteStartElement("РазмещениеБДFierberd");
                    //WriterConfigXML.WriteStartElement("БДСервер1");
                    //WriterConfigXML.WriteAttributeString("REF", "work");
                    //AppStart.aParamApp.Add("БДСервер1_REF", "work");
                    //WriterConfigXML.WriteAttributeString("IP", "127.0.0.1");
                    //AppStart.aParamApp.Add("БДСервер1_IP", "127.0.0.1");
                    //WriterConfigXML.WriteAttributeString("Putch-Hiden", "");
                    //AppStart.aParamApp.Add("БДСервер1_Putch-Hiden", "");
                    //WriterConfigXML.WriteAttributeString("default", "1");
                    //AppStart.aParamApp.Add("БДСервер1_default", "1");
                    //WriterConfigXML.WriteAttributeString("key", "127.0.0.1");
                    //AppStart.aParamApp.Add("БДСервер1_key", "127.0.0.1");
                    // WriterConfigXML.WriteComment("БД на сервере (путь на сервере или алиас)");
                    //WriterConfigXML.WriteEndElement();
                    //WriterConfigXML.WriteStartElement("БДСервер2");
                    //WriterConfigXML.WriteAttributeString("REF", "work2");
                    //AppStart.aParamApp.Add("БДСервер2_REF", "work2");
                    //WriterConfigXML.WriteAttributeString("IP", "127.0.0.1");
                    //AppStart.aParamApp.Add("БДСервер2_IP", "127.0.0.1");
                    //WriterConfigXML.WriteAttributeString("Putch-Hiden", "");
                    //AppStart.aParamApp.Add("БДСервер2_Putch-Hiden", "");
                    //WriterConfigXML.WriteAttributeString("default", "0");
                    // AppStart.aParamApp.Add("БДСервер2_default", "0");
                    //WriterConfigXML.WriteAttributeString("key", "127.0.0.1");
                    //AppStart.aParamApp.Add("БДСервер2_key", "127.0.0.1");
                    // WriterConfigXML.WriteComment("БД на сервере (путь на сервере или алиас)");
                    //WriterConfigXML.WriteEndElement();
                    //WriterConfigXML.WriteStartElement("БДСервер3");
                    //WriterConfigXML.WriteAttributeString("REF", "back");
                    // AppStart.aParamApp.Add("БДСервер3_REF", "back");
                    //WriterConfigXML.WriteAttributeString("IP", "127.0.0.1");
                    //AppStart.aParamApp.Add("БДСервер3_IP", "127.0.0.1");
                    ////WriterConfigXML.WriteAttributeString("Putch-Hiden", "");
                    //AppStart.aParamApp.Add("БДСервер3_Putch-Hiden", "");
                    ////WriterConfigXML.WriteAttributeString("default", "0");
                    //AppStart.aParamApp.Add("БДСервер3_default", "0");
                    ////WriterConfigXML.WriteAttributeString("key", "127.0.0.1");
                    //AppStart.aParamApp.Add("БДСервер3_key", "127.0.0.1");
                    //// WriterConfigXML.WriteComment("БД на сервере (путь на сервере или алиас)");
                    ////WriterConfigXML.WriteEndElement();
                    ////WriterConfigXML.WriteStartElement("БДСервер4");
                    ////WriterConfigXML.WriteAttributeString("REF", "back2");
                    //AppStart.aParamApp.Add("БДСервер4_REF", "back2");
                    ////WriterConfigXML.WriteAttributeString("IP", "127.0.0.1");
                    //AppStart.aParamApp.Add("БДСервер4_IP", "127.0.0.1");
                    ////WriterConfigXML.WriteAttributeString("Putch-Hiden", "");
                    //AppStart.aParamApp.Add("БДСервер4_Putch-Hiden", "");
                    ////WriterConfigXML.WriteAttributeString("default", "0");
                    //AppStart.aParamApp.Add("БДСервер4_default", "0");
                    ////WriterConfigXML.WriteAttributeString("key", "127.0.0.1");
                    //AppStart.aParamApp.Add("БДСервер4_key", "127.0.0.1");
                    //// WriterConfigXML.WriteComment("БД на сервере (путь на сервере или алиас)");
                    ////WriterConfigXML.WriteEndElement();
                    ////WriterConfigXML.WriteStartElement("БДСервер5");
                    ////WriterConfigXML.WriteAttributeString("REF", "torg");
                    //AppStart.aParamApp.Add("БДСервер5_REF", "torg");
                    ////WriterConfigXML.WriteAttributeString("IP", "127.0.0.1");
                    //AppStart.aParamApp.Add("БДСервер5_IP", "127.0.0.1");
                    ////WriterConfigXML.WriteAttributeString("Putch-Hiden", "");
                    //AppStart.aParamApp.Add("БДСервер5_Putch-Hiden", "");
                    ////WriterConfigXML.WriteAttributeString("default", "0");
                    //AppStart.aParamApp.Add("БДСервер5_default", "0");
                    ////WriterConfigXML.WriteAttributeString("key", "127.0.0.1");
                    //AppStart.aParamApp.Add("БДСервер5_key", "127.0.0.1");
                    //// WriterConfigXML.WriteComment("БД на сервере (путь на сервере или алиас)");
                    ////WriterConfigXML.WriteEndElement();
                    ////WriterConfigXML.WriteStartElement("БДСервер6");
                    ////WriterConfigXML.WriteAttributeString("REF", "torg2");
                    //AppStart.aParamApp.Add("БДСервер6_REF", "torg2");
                    ////WriterConfigXML.WriteAttributeString("IP", "127.0.0.1");
                    //AppStart.aParamApp.Add("БДСервер6_IP", "127.0.0.1");
                    ////WriterConfigXML.WriteAttributeString("Putch-Hiden", "");
                    //AppStart.aParamApp.Add("БДСервер6_Putch-Hiden", "");
                    ////WriterConfigXML.WriteAttributeString("default", "0");
                    //AppStart.aParamApp.Add("БДСервер6_default", "0");
                    ////WriterConfigXML.WriteAttributeString("key", "127.0.0.1");
                    //AppStart.aParamApp.Add("БДСервер6_key", "127.0.0.1");
                    //// WriterConfigXML.WriteComment("БД на сервере (путь на сервере или алиас)");
                    ////WriterConfigXML.WriteEndElement();
                    ////WriterConfigXML.WriteStartElement("БДСервер7");
                    ////WriterConfigXML.WriteAttributeString("REF", "arhive");
                    //AppStart.aParamApp.Add("БДСервер7_REF", "arhive");
                    ////WriterConfigXML.WriteAttributeString("IP", "127.0.0.1");
                    //AppStart.aParamApp.Add("БДСервер7_IP", "127.0.0.1");
                    ////WriterConfigXML.WriteAttributeString("Putch-Hiden", "");
                    //AppStart.aParamApp.Add("БДСервер7_Putch-Hiden", "");
                    ////WriterConfigXML.WriteAttributeString("default", "0");
                    //AppStart.aParamApp.Add("БДСервер7_default", "0");
                    ////WriterConfigXML.WriteAttributeString("key", "127.0.0.1");
                    //AppStart.aParamApp.Add("БДСервер7_key", "127.0.0.1");
                    //// WriterConfigXML.WriteComment("БД на сервере (путь на сервере или алиас)");
                    ////WriterConfigXML.WriteEndElement();
                    ////WriterConfigXML.WriteStartElement("БДСервер8");
                    ////WriterConfigXML.WriteAttributeString("REF", "local");
                    //AppStart.aParamApp.Add("БДСервер8_REF", "local");
                    ////WriterConfigXML.WriteAttributeString("IP", "127.0.0.1");
                    //AppStart.aParamApp.Add("БДСервер8_IP", "127.0.0.1");
                    ////WriterConfigXML.WriteAttributeString("Putch-Hiden", "");
                    //AppStart.aParamApp.Add("БДСервер8_Putch-Hiden", "");
                    ////WriterConfigXML.WriteAttributeString("default", "0");
                    //AppStart.aParamApp.Add("БДСервер8_default", "0");
                    ////WriterConfigXML.WriteAttributeString("key", "127.0.0.1");
                    //AppStart.aParamApp.Add("БДСервер8_key", "127.0.0.1");
                    // WriterConfigXML.WriteComment("БД на на локальной машине при збоии сети (путь на сервере или алиас)");
                    //WriterConfigXML.WriteEndElement();
                    //WriterConfigXML.WriteEndElement();
                    // -------------
                    //WriterConfigXML.WriteStartElement("РазмещениеБДMySQL");
                    //WriterConfigXML.WriteStartElement("БДСервер20");
                    //WriterConfigXML.WriteAttributeString("REF", "work");

                    //WriterConfigXML.WriteAttributeString("IP", "127.0.0.1");

                    //WriterConfigXML.WriteAttributeString("Putch-Hiden", "");

                    //WriterConfigXML.WriteAttributeString("default", "0");

                    //WriterConfigXML.WriteAttributeString("key", "127.0.0.1");

                    // WriterConfigXML.WriteComment("БД на сервере (путь на сервере или алиас для MySQL)");
                    //WriterConfigXML.WriteEndElement();
                    //WriterConfigXML.WriteEndElement();
                    //// -----------
                    //WriterConfigXML.WriteStartElement("РазмещениеБДMySQLSite");
                    //WriterConfigXML.WriteStartElement("БДСервер40");
                    //WriterConfigXML.WriteAttributeString("REF", "work");
                    //WriterConfigXML.WriteAttributeString("IP", "127.0.0.1");
                    //WriterConfigXML.WriteAttributeString("Putch-Hiden", "");
                    //WriterConfigXML.WriteAttributeString("default", "0");
                    //WriterConfigXML.WriteAttributeString("key", "127.0.0.1");
                    //// WriterConfigXML.WriteComment("БД на сервере (путь на сервере или алиас на ВЕБ-сайте)");
                    //WriterConfigXML.WriteEndElement();
                    //WriterConfigXML.WriteEndElement();
                    // ----------------
                    //WriterConfigXML.WriteStartElement("РазмещениеБДOhter");
                    //WriterConfigXML.WriteStartElement("БДСервер50");
                    //WriterConfigXML.WriteAttributeString("REF", "work");
                    //WriterConfigXML.WriteAttributeString("IP", "127.0.0.1");
                    //WriterConfigXML.WriteAttributeString("Putch-Hiden", "");
                    //WriterConfigXML.WriteAttributeString("default", "0");
                    //WriterConfigXML.WriteAttributeString("key", "127.0.0.1");
                    // WriterConfigXML.WriteComment("БД на сервере Другая (путь на сервере или алиас на ВЕБ-сайте)");
                    //WriterConfigXML.WriteEndElement();
                    //WriterConfigXML.WriteEndElement();
                    //// --------------------
                    //WriterConfigXML.WriteStartElement("ВремяСинхронизации");
                    //WriterConfigXML.WriteStartElement("Time");
                    //WriterConfigXML.WriteAttributeString("IP", "127.0.0.1");

                    //// --------------------
                    //WriterConfigXML.WriteStartElement("Терминал");
                    //WriterConfigXML.WriteStartElement("Terminal");
                    //WriterConfigXML.WriteAttributeString("Explorer", "Yes");            

                    //WriterConfigXML.WriteAttributeString("Putch-Terminal-Hiden", PStartup + "Conecto®WorkSpace.exe"); 
                    // Путь к программе
                    //WriterConfigXML.WriteAttributeString("autostart", "0");             

                    //WriterConfigXML.WriteAttributeString("autostart-time-sec", "20");   

                    // WriterConfigXML.WriteComment("Запуск приложения в режиме терминал");
                    //WriterConfigXML.WriteEndElement();
                    //WriterConfigXML.WriteEndElement();
                    ////--------------------
                    //WriterConfigXML.WriteStartElement("Рабочий-стол");
                    //WriterConfigXML.WriteStartElement("Desktop");
                    //WriterConfigXML.WriteAttributeString("Fornt", "Yes");          
                    //AppStart.aParamApp.Add("Desktop_Putch-Fornt", "Yes");                    // Размещение первого фронта (по умолчанию определяется системой)
                    ////WriterConfigXML.WriteAttributeString("Fornt-Desk", "Yes");      
                    //AppStart.aParamApp.Add("Desktop_Fornt-Desk", "Yes");                     // Отобразить фронт на рабочем столе
                    ////WriterConfigXML.WriteAttributeString("Putch-Front-Hiden", "");
                    //AppStart.aParamApp.Add("Desktop_Putch-Front-Hiden", "");
                    ////WriterConfigXML.WriteAttributeString("Fornt2", "No");           
                    //AppStart.aParamApp.Add("Desktop_Fornt2", "No");                          // ?
                    ////WriterConfigXML.WriteAttributeString("Putch-Front2-Hiden", ""); 
                    //AppStart.aParamApp.Add("Desktop_Putch-Front2-Hiden", "");                // Размещение второго фронта
                    ////WriterConfigXML.WriteAttributeString("Fornt2-Desk", "No");     
                    //AppStart.aParamApp.Add("Desktop_Fornt2-Desk", "Yes");                    // Отобразить второй фронт на рабочем столе
                    ////WriterConfigXML.WriteAttributeString("Back", "No");             
                    //AppStart.aParamApp.Add("Desktop_Back", "No");                            // ?
                    ////WriterConfigXML.WriteAttributeString("Back-Desk", "No");        
                    //AppStart.aParamApp.Add("Desktop_Back-Desk", "No");                       // Отобразить бек на рабочем столе
                    ////WriterConfigXML.WriteAttributeString("Putch-Back-Hiden", "");
                    //AppStart.aParamApp.Add("Desktop_Putch-Back-Hiden", "");

                    //WriterConfigXML.WriteAttributeString("Config", "No");
                    //WriterConfigXML.WriteAttributeString("Putch-Config-Hiden", "");
                    //WriterConfigXML.WriteAttributeString("Config-Desk", "No");
                    //WriterConfigXML.WriteAttributeString("Front-Poker", "No");
                    //WriterConfigXML.WriteAttributeString("Putch-Front-Poker-Hiden", "");
                    //WriterConfigXML.WriteAttributeString("Front-Poker-Desk", "No");
                    //WriterConfigXML.WriteAttributeString("Front-Video", "No");
                    //WriterConfigXML.WriteAttributeString("Putch-Front-Video-Hiden", "");
                    //WriterConfigXML.WriteAttributeString("Front-Video-Desk", "No");
                    //WriterConfigXML.WriteAttributeString("Front-Roznica", "No");
                    //WriterConfigXML.WriteAttributeString("Putch-Front-Roznica-Hiden", "");
                    //WriterConfigXML.WriteAttributeString("Front-Roznica-Desk", "No");

                    // WriterConfigXML.WriteComment("Автозапуск и отображение приложений на рабочем столе (по умолчанию система сама проверяет наличие приложений)");
                    //WriterConfigXML.WriteEndElement();
                    //WriterConfigXML.WriteEndElement();

                    #endregion

                }

                #region 2 - запись измененных параметров (до поры до времени то же самое, что и 0)
                if (TypePr == 2)
                {
                    // Полностью очистить файл
                    // File.WriteAllText(PuthApp + "config.xml", "");

                    // запись бинарный файл
                    // BinaryWriter w = new BinaryWriter(XMLConfigFile); 

                    // Чтение файла  
                    // StreamReader sr = new StreamReader(fstr, Encoding.UTF8);


                    // Вариант записи вместо - XMLConfigFile.Write(); //SeekOrigin.End
                    if (AppStart.XMLConfigFile != null)
                    {
                        // --- Такой метод стирания не подходит
                        //StreamWriter sw = new StreamWriter(AppStart.XMLConfigFile, Encoding.UTF8);
                        //sw.Write("");
                        //sw.Close();
                        //AppStart.XMLConfigFile.Flush();
                        AppStart.XMLConfigFile.Close();
                        // MessageBox.Show("Дошел2");
                        //return;
                    }

                }

                // Создать масив данных в конфигурационный файл
                if (TypePr == 0 || TypePr == 2)
                {

                    using (StringWriter sw = new StringWriter())
                    {
                        // Примеры и описание ниже
                        WriterConfigXML = new XmlTextWriter(sw);
                        WriterConfigXML.Formatting = Formatting.Indented; // упорядочивания структуры файла

                        //WriterConfigXML = new XmlTextWriter(AppStart.PutchApp + "config.xml", System.Text.Encoding.Unicode); //UTF 16



                        // Создать масив данных XML
                        string[] aStartElement = new string[1];
                        aStartElement[0] = "";
                        string[] aEndElement = new string[1];
                        aEndElement[0] = "";
                        int Start = 0;
                        // -------------                              ---------------  
                        WriterConfigXML.WriteStartDocument();
                        WriterConfigXML.WriteStartElement("Параметры-Администратора");

                        foreach (KeyValuePair<string, string> kvp in AppStart.aParamApp)
                        {
                            string sStartElement = kvp.Key;         // kvp.Value
                            aStartElement = sStartElement.Split('_');
                            /// Получаем первый елемент
                            if (aStartElement[0] == aEndElement[0])
                            {
                                // Запись атрибута
                                WriterConfigXML.WriteAttributeString(kvp.Key, kvp.Value);
                            }
                            else
                            {
                                // начало нового концовка старого Элемента
                                aEndElement[0] = aStartElement[0];
                                if (Start > 0)
                                {
                                    WriterConfigXML.WriteEndElement();
                                }
                                else
                                {
                                    Start = 1;
                                }
                                WriterConfigXML.WriteStartElement(aStartElement[0]);
                                // Запись атрибута
                                WriterConfigXML.WriteAttributeString(kvp.Key, kvp.Value);
                            }

                        }
                        // Конец Элементов
                        if (Start == 1)
                        {
                            WriterConfigXML.WriteEndElement();
                        }
                        //-------------------- конец документа
                        WriterConfigXML.WriteEndElement();

                        WriterConfigXML.WriteEndDocument();

                        xmlString = sw.ToString();
                    }
                }
                #endregion


                if (WriterConfigXML != null)
                {

                    // 0 - создать файл; 2 - запись измененных параметров
                    if (TypePr == 0 || TypePr == 2)
                    {
                        WriterConfigXML.Close();
                    }
                    //WriterConfigXML = null;

                    // При ReleaseCandidate == "Release" файл шифруется возможно сделать defined
                    //  0 - создать файл;
                    if (AppStart.ReleaseCandidate == "Release" && TypePr == 0)
                    {
                        AppStart.EncryptTextToFile(AppStart.PutchApp + "config.xml", xmlString);
                        // Отладка
                        // File.WriteAllText(AppStart.PutchApp + "config_.xml", xmlString, Encoding.Unicode);
                    }
                    if (AppStart.ReleaseCandidate != "Release" && TypePr == 0)
                    {
                        File.WriteAllText(AppStart.PutchApp + "config.xml", xmlString, Encoding.Unicode);
                    }
                   

                }


            }
            catch (Exception ) //error
            {
                //AppStart.ErorDebag("Создание WriterConfigXML вызвало исключение: " + Environment.NewLine +
                //    " === Файл: " + AppStart.PutchApp + "config.xml" + Environment.NewLine +
                //    " === Message: " + error.Message.ToString() + Environment.NewLine +
                //    " === Exception: " + error.ToString());
            }
            finally
            {
                if (WriterConfigXML != null && (TypePr == 0 || TypePr == 2))
                {
                    WriterConfigXML.Close();
                }
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
        public static bool ControllerParam(dynamic ParamValue, string NameParam,  int TypeFunc = 0)
        {
            bool LoadParam = false;
            switch(TypeFunc)
            {
                case 0:
                    // 0 - Текстовая
                    //ParamValue.GetType();
                    
                    // Запись
                    AppStart.aParamApp[NameParam] = ParamValue;

                    LoadParam = true;
                    break;
                case 10:

                    // Примерв http://skillcoding.com/Default.aspx?id=155
                    // 10 - IP адресс v4
                    //Инициализируем новый экземпляр класса System.Text.RegularExpressions.Regex
                    //для указанного регулярного выражения.
                    Regex IpMatch = new Regex(@"\b(?:\d{1,3}\.){3}\d{1,3}\b");
                    //Выполняем проверку обнаружено ли в указанной входной строке соответствие регулярному
                    //выражению, заданному в конструкторе System.Text.RegularExpressions.Regex.
                    //если да то возвращаем true, если нет то false

                    LoadParam = IpMatch.IsMatch(ParamValue);
                    break;
                case 11:
                    // 11 - проверка пути ...

                        // Запись в память
                        AppStart.aParamApp[NameParam] = ParamValue;



                        LoadParam = true;
                    break;

            }
            // Запись в файл если предыдущие значение тоже с ошибкой то записать последний результат (Разработка)
            if (LoadParam)
            {
                var Config = new SystemConfigControll();
                Config.CreateConfigXML(2);
            }

            // Применение (возможно определить метод)

            // Диагностика параметра
            DiagnosParam(NameParam);

            // Проверка диагностики DiagnozParam

            // Внесение изменений в работу системы
            ReloadParamSystem(NameParam);

            return LoadParam;
        }
        #endregion 

        #region - диагностика введенных и данных в памяти

        /// <summary>
        /// Диагностика введенных и данных в памяти
        /// </summary>
        /// <param name="NameParam">Имя переменной для диагностики</param>
        public static void DiagnosParam(string NameParam)
        {
            // Возвращаем результат диагностики
            DiagnozParam = false;

            // Найти и выполнить метод диагностики для параметра
            var t = new DiagnosticParam();

            System.Reflection.MethodInfo mi = typeof(DiagnosticParam).GetMethod(NameParam + "Diag"); // , new Type[] { typeof(int) }
            if (mi != null)
            {
                mi.Invoke(t, new object[] { });
            }


        }
        #endregion 


        //------------------------- Сервисы

        #region Запись переменной в Undo

        /// <summary>
        /// Запись переменной в Undo (запись в память предыдущего значения)
        /// </summary>
        /// <param name="NameParam">Имя параметра</param>
        public static void UpdateParamUndo(string NameParam){

            // Проверка создания параметра в массива данных aParamAppUndo
            if (!aParamAppUndo.ContainsKey(NameParam))
            {
                aParamAppUndo.Add(NameParam, AppStart.aParamApp[NameParam]);
            }
            else {
                aParamAppUndo[NameParam] = AppStart.aParamApp[NameParam];
            }
        }

        #endregion

        /// <summary>
        /// Загрузка и активация параметров системы (а также диструктивные функции)
        /// </summary>
        public static void ReloadParamSystem(string NameParam)
        {

            switch (NameParam)
            {
                case "Time_IP":
                    // Включения клиента синхронизации времени
                    // Выключение : Включение
                    AppStart.TickRg_a[6] = AppStart.aParamApp["Time_IP"] == "0.0.0.0" ? -7 : 0;
                    AppStart.Tick_a[6] = 9;
                    break;

            }

        }



        #endregion


        #region Операции с файлами XML (по примеру XML config)


        #region Чтение файла XML по указанному пути
        
        /// <summary>
        /// Чтение файла XML по указанному пути
        /// </summary>
        /// <param name="TypeFunc"></param>
        /// <returns></returns>
        public static XmlTextReader ReadXMLFile(string PuthFile, int TypeFunc = 0)
        {
            // Чтение файла
            XmlTextReader reader = null;

            try
            {
                if (TypeFunc == 1)
                {
                    // === Неудачные примеры чтения файлов
                    // Uri PuthFileUri = new Uri(@"/Conecto®%20WorkSpace;/appplay.xml", UriKind.Relative);
                    // System.Windows.Resources.StreamResourceInfo info = System.Windows.Application.GetContentStream(PuthFileUri);
                    // info.Stream

                    //StreamReader sReader = new StreamReader(Properties.Resources.appplay);
                    // ==============

                    // Чтение обычного файла из Ресурсов
                    reader = new XmlTextReader(new StringReader(Properties.Resources.appplay));
                    // Чтение бинарного файла разработать...)
                }
                else
                {
                    // Режим отладки
                    if (AppStart.DebagApp)
                    {
                        // Чтение обычного файла
                        reader = new XmlTextReader(PuthFile);
                    }
                    else
                    {
                        // Чтение зашифрованого файла

                        StringReader FileText = new StringReader(AppStart.DecryptTextToFile(PuthFile));
                        reader = new XmlTextReader(FileText);
                        //reader = new XmlTextReader(new StringReader(AppStart.DecryptTextFromFile(AppStart.PutchApp + "appplay.xml", SystemConfig.sData, @"м^ыiЎQRy3іљТФ>")));
                    }
                }
                // Свойсва чтения
                reader.WhitespaceHandling = WhitespaceHandling.None; // пропускаем пустые узлы

            }
            catch (Exception) // ex
            {
                //AppStart.ErorDebag("При загрузке файла: " + PuthFile + ", возникло исключение: " + Environment.NewLine +
                //    " === Message: " + ex.Message.ToString() + Environment.NewLine +
                //    " === Exception: " + ex.ToString(), 1);
                return null;
            }
            finally
            {
                if (reader != null)
                    reader.Close();
            }

            return reader;

        }

        #endregion
        #region Чтение потока XML по указанному адрессу и порту





        #endregion


        #endregion
    }

    /// <summary>
    /// Конструкции для диагностики параметров
    /// </summary>
    public partial class DiagnosticParam // SystemConecto
    {


    }
}
