// ==================================== Управление файлами конфигураций для gриложений ConectoWorkSpace
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

// ==================================== Используем функции ядра SystemConecto

namespace ConectoWorkSpace
{
    #region Структура данных параметров свойств приложения
    /// <summary>
    ///  Структура параметров свойств приложения
    /// </summary>
    public struct AppPlayOptionStruct
    {

        /// <summary>
        /// Название приложения в окне PalyStory (наследуется из PalyStory)
        /// </summary>
        public string CaptionNamePlay { get; set; }

        
        /// <summary>
        /// Путь к изображению для ConectoWorkSpace
        /// </summary>
        public string PuthFileIm { get; set; }

        /// <summary>
        /// Название приложения (служебное англ.)
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// код приложения (служебное циф)
        /// </summary>
        public int id { get; set; }
        /// <summary>
        /// Версия файла - нужно проверять с параметром WorkSpace - SystemConecto.aParamApp[...]
        /// </summary>
        public string Version { get; set; }
        /// <summary>
        /// При нажатии приложение не запускается, а выводится информационное окно о продукте на экран WorkSpace - Рекламма
        /// </summary>
        public bool infoWorkSpace { get; set; }

        /// <summary>
        /// Тип авторизации: -1 - без авторизации, 1 - пин авторизация, 0- авторизация через логин и пароль, 5 - авторизация текущего пользователя  Windows, 
        /// 6- ручная авторизация пароль логин пользователя Windows  
        /// </summary>
        public int AutorizeType { get; set; }

        /// <summary>
        /// FTP сервисы
        /// </summary>
        public AppPlayOptionStructFTP FTP { get; set; }
        /// <summary>
        /// Dll модули
        /// </summary>
        public AppPlayOptionStructdll Dll { get; set; }

        /// <summary>
        /// Панель управления ярлыками на WorkSpace
        /// </summary>
        public PanelWorkSpace PanelWS { get; set; }

        /// <summary>
        /// Дополнительные переменные елементы 
        /// </summary>
        public Dictionary<string, string> ListVirableAdd { get; set; }

        /// <summary>
        /// Дополнительные переменные елементы установленные пользователями (резерв)
        /// </summary>
        public Dictionary<string, string> UserconfigWorkSpace { get; set; }

    }


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



    public struct PanelWorkSpace
    {
        /// <summary>
        /// Отображать ли Ярлык на панели WorkSpace 
        /// </summary>
        public bool LinkPanel { get; set; }
        /// <summary>
        /// Тип линка: 0 - запуск приложения<para></para>
        /// 1 - запуск инфо приложения Вывод информационного окна о продукте на экран WorkSpace .infoWorkSpace
        /// 2 - запуск Демо приложения
        /// 3 - запуск с авторизацией
        /// 4 - деинсталирован
        /// </summary>
        public int TypeLink { get; set; }
        /// <summary>
        /// Порядок вывода и размещения ярлыка на шахматной панели
        /// </summary>
        public int NumberPanel { get; set; }

        /// <summary>
        /// Название приложения в ConectoWorkSpace и авторизации
        /// </summary>
        public string CaptionNameWorkSpace { get; set; }

        /// <summary>
        /// Тип приложения
        /// </summary>
        public string AppStartMetod { get; set; }

        /// <summary>
        /// Тип приложения
        /// </summary>
        public string TypeApp { get; set; }

        /// <summary>
        /// Время автостарта
        /// </summary>
        public string Autostart { get; set; }
    }


    #endregion

    /// <summary>
    ///  Разделяемый класс по файлам (ключевое слово - partial)
    /// </summary>
    public partial class SystemConfigControll // SystemConecto SystemConfigAppPlayStory
    {
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

        #region Глобальные параметры (переменные)

        /// <summary>
        /// Параметры приложения string key - id App
        /// </summary>
        public Dictionary<int, AppPlayOptionStruct> aParamAppPlay = new Dictionary<int, AppPlayOptionStruct>();

        //public static AppPlayOptionStruct AppPlayOption = new AppPlayOptionStruct();


        /// <summary>
        /// Список ярлыков на панели WorkSpace number, id
        /// </summary>
        public static Dictionary<int, int> ListPanelWorkSpace = new Dictionary<int, int>();


        /// <summary>
        /// Список ярлыков на панели Admin App number, id
        /// </summary>
        public static Dictionary<int, int> ListPanelAdmin = new Dictionary<int, int>();

        /// <summary>
        /// Список неактивных ярлыков на панели PlayStory number, id
        /// </summary>
        public static Dictionary<int, int> ListPlayStory = new Dictionary<int, int>();


            // Пример блокировки файла конфигурации
        // public static FileStream XMLConfigFile = null; // Блокировка основного файла кофигурации
        // 7. Запуск блокировки файлов - функция безопасности
        // SystemConecto.File_(SystemConecto.PutchApp + "config.xml", 6, SystemConfig.XMLConfigFile);

        // public XmlTextWriter WriterConfigXML = null;        // Объект - параметры конфигурации
        // public XmlTextWriter WriterConfigUserXML = null;    // Объект - параметры пользователя
        // public static string sData = "Mein Kopf Mein Kopf Mein Kopf"; // Создание строки для шифрования



        //SystemConecto.aLanguage - Мультязычнось Локаль приложения [new Dictionary<string, string>();]
        //SystemConecto.aParamApp - Параметры приложения [new Dictionary<string, string>();]




        #endregion

        #region Операции с файлами конфигурации приложения

        #region Файл appplay.xml    - чтение настройки скд системы
        

        /// <summary>
        /// Чтение в память файла appplay.xml
        /// </summary>
        /// <param name="TypeFunc"></param>
        /// <returns></returns>
        public bool ReadConfigXMLAppPlay(int TypeFunc = 0)
        {
            
            XmlTextReader reader = null;
            // Dictionary<string, string> aParamAppChek = new Dictionary<string, string>(); // Параметры приложения проверка
            int ConfigVer = 1;
            string nameElement = "";
            // В потоке сбор информации о приложении
            int idApp = 0;
            // Завершения чтения информации в файле
            int EndFileLogic = 0;

            AppPlayOptionStruct AppPlayOption = new AppPlayOptionStruct();

            string PuthFileRead = SystemConecto.PutchApp + "appplay.xml";

            // -------------  Чистка памяти без отката нужно с откатом - перезагрузка параметров
            var ListPanelWorkSpaceUndo = ListPanelWorkSpace;
            ListPanelWorkSpace = new Dictionary<int, int>();
            var ListPanelAdminUndo = ListPanelAdmin;
            ListPanelAdmin = new Dictionary<int, int>();
            var ListPlayStoryUndo = ListPlayStory;
            ListPlayStory = new Dictionary<int, int>();
            var aParamAppPlayUndo = aParamAppPlay;
            aParamAppPlay = new Dictionary<int, AppPlayOptionStruct>();
            //var AppPlayOptionUndo = AppPlayOption;
            //AppPlayOption = ;


            try
            {
                if (TypeFunc == 2)
                {
                    //string pattern = @"\p{C}+";
                    //Regex rgx = new Regex(pattern);
                    //StringReader FileText = new StringReader(rgx.Replace(ConectoWorkSpace.AppStart.xmlString, ""));
                    reader = new XmlTextReader(new StringReader(ConectoWorkSpace.AppStart.xmlString));
                }
                else
                {
                    if (TypeFunc == 1)
                    {
                    // === Неудачные примеры чтения файлов
                    // Uri PuthFileUri = new Uri(@"/Conecto®%20WorkSpace;/appplay.xml", UriKind.Relative);
                    // System.Windows.Resources.StreamResourceInfo info = System.Windows.Application.GetContentStream(PuthFileUri);
                    // info.Stream

                    //StreamReader sReader = new StreamReader(Properties.Resources.appplay);
                    // ==============
                    // Чтение обычного файла
                        reader = new XmlTextReader(new StringReader(Properties.Resources.appplay));
                    }
                    else
                    {
                        // Режим Компиляции Релиза приложения
                        if (AppStart.ReleaseCandidate == "Release")
                        {
                        // Чтение зашифрованого файла
                        var TextFile = SystemConecto.DecryptTextToFile(SystemConecto.PutchApp + "appplay.xml");
                        // защита от пробелов
                        string pattern = @"\p{C}+";
                        Regex rgx = new Regex(pattern);
                        StringReader FileText = new StringReader(rgx.Replace(TextFile, ""));
                        reader = new XmlTextReader(FileText);
                        //reader = new XmlTextReader(new StringReader(SystemConecto.DecryptTextFromFile(SystemConecto.PutchApp + "appplay.xml", SystemConfig.sData, @"м^ыiЎQRy3іљТФ>")));
                        }
                        else
                        {

                            // Чтение обычного файла
                            reader = new XmlTextReader(PuthFileRead); //SystemConecto.PutchApp + "appplay.xml"
                     
                        }
                    }

                }
 


                // Свойсва чтения
                reader.WhitespaceHandling = WhitespaceHandling.None; // пропускаем пустые узлы
  
                while (reader.Read())
                {
                    if (reader.NodeType == XmlNodeType.Element || reader.NodeType == XmlNodeType.EndElement)
                        nameElement = reader.Name;

                    // Проверка параметров и версии конфигурации, а также целосность данных
                    // Исключение тег ?xml = ""
                    //  nameElement - структура [Параметр структуры]_[id]_[Name1]_[Name2]

                    string[] a_nameElement = nameElement.Split('_');
                    switch (a_nameElement[0])
                    {
                        case "":
                        
                            break;
                        case "Параметры-AppPlay":
                            EndFileLogic++;
                            if (EndFileLogic == 2)
                            {

                                if (idApp == 0)
                                {

                                }
                                else
                                {
                                    aParamAppPlay.Add(idApp, AppPlayOption);
                                }
                                
                                // AppPlayOption = new AppPlayOptionStruct();
                                // SystemConecto.ErorDebag("Конец чтения тегов...", 1);
                            }
                            break;
                        // Проверка версии файла 
                        case "FileConfig":
                            if (ConfigVer == 1)
                            {
                                // проверка отсутствует reader.LocalName == "FileConfig_Ver-File-Config" - && reader.GetAttribute(x) == aParamAppPlay["FileConfig_Ver-File-Config"].Version
                                ConfigVer = 0;
                                AppPlayOption.Version = reader.GetAttribute("FileConfig_Ver-File-Config");

                            }
                            break;
                        case "idAppOverall":

                            if (idApp == Convert.ToInt32(a_nameElement[1]))
                            {
                                // Дальнейшая обработка


                            }
                            else
                            {
                                
                                
                                // Начало и конец информации о приложении в потоке
                                
                                if (idApp == 0)
                                {
                                    idApp = Convert.ToInt32(a_nameElement[1]);

                                }else{

                                    aParamAppPlay.Add(idApp, AppPlayOption);
                                    idApp = Convert.ToInt32(a_nameElement[1]);
                                    AppPlayOption = new AppPlayOptionStruct();

                                }
                                

                            }
                            // Запись праметров
                            AppPlayOption.Name = reader.GetAttribute("NameApp");
                            AppPlayOption.id = Convert.ToInt32(reader.GetAttribute("idApp"));
                            AppPlayOption.CaptionNamePlay = reader.GetAttribute("CaptionNamePlay");
                            
                            AppPlayOption.PuthFileIm = reader.GetAttribute("PuthFileIm");
                            AppPlayOption.infoWorkSpace = Convert.ToBoolean(reader.GetAttribute("infoWorkSpace"));
                            AppPlayOption.AutorizeType = reader.GetAttribute("AutorizeType")==null ? -1 : Convert.ToInt32(reader.GetAttribute("AutorizeType"));

                                // Данный пример вернет null
                                //AppPlayOption.CaptionNamePlay = reader.GetAttribute("infoWorkSpace_");

                            break;

                        case "idAppPanelWorkSpace":

                            if (idApp == Convert.ToInt32(a_nameElement[1]))
                            {
                                // Дальнейшая обработка


                            }
                            else
                            {


                                // Начало и конец информации о приложении в потоке

                                if (idApp == 0)
                                {
                                    idApp = Convert.ToInt32(a_nameElement[1]);

                                }
                                else
                                {

                                    aParamAppPlay.Add(idApp, AppPlayOption);
                                    idApp = Convert.ToInt32(a_nameElement[1]);
                                    AppPlayOption = new AppPlayOptionStruct();

                                }


                            }
                            // Запись праметров
                            PanelWorkSpace PanelWS_ = new PanelWorkSpace();

                            // чтение через for с обязательными параметрами
                            PanelWS_.LinkPanel = Convert.ToBoolean(reader.GetAttribute("LinkPanel"));
                            PanelWS_.TypeLink = Convert.ToInt32(reader.GetAttribute("TypeLink"));
                            PanelWS_.NumberPanel = Convert.ToInt32(reader.GetAttribute("NumberPanel"));
                            PanelWS_.CaptionNameWorkSpace = reader.GetAttribute("CaptionNameWorkSpace");
                            // Не обязательный указать по умолчанию загружаемый метод
                            PanelWS_.AppStartMetod = reader.GetAttribute("AppStartMetod");
                            // Не обязательно по умолчанию внешний
                            PanelWS_.TypeApp = reader.GetAttribute("TypeApp");
                            PanelWS_.Autostart = reader.GetAttribute("Autostart");

                            // Панели
                            ListPanelWorkSpace.Add(PanelWS_.NumberPanel, idApp);
                            
                            ListPanelAdmin.Add(PanelWS_.NumberPanel, idApp);

                            ListPlayStory.Add(PanelWS_.NumberPanel, idApp);

                            AppPlayOption.PanelWS = PanelWS_;


                            
                            break;
                        
                        default:

                            //  Проверка параметров присутствие Расшифровка параметра reader.LocalName - структура [id]_[Name1]_[Name2]

                            // Создаем масив 
                            // SystemConecto.aParamApp.Add(reader.Name, "");
                            // reader.AttributeCount //Количесвто атрибутов
                            // reader.MoveToAttribute   // Перемещает по индексу на атрибут
                            // Чтение отрибутов универсальное
                            if (reader.HasAttributes)
                            {
                                for (int x = 0; x < reader.AttributeCount; x++)
                                {
                                    // Перейти к атрибуту
                                    reader.MoveToAttribute(x);



                                    // Запись в память
                                    // Проверка (на всякий случай) добавленных переменных
                                    if (AppPlayOption.ListVirableAdd.ContainsKey(reader.LocalName))
                                    {

                                        AppPlayOption.ListVirableAdd[reader.LocalName] = reader.GetAttribute(x);
                                    }
                                    else
                                    {
                                        // !---Установить проверку динамических параметров---
                                        AppPlayOption.ListVirableAdd.Add(reader.LocalName, reader.GetAttribute(x));

                                        // Запись отклонений
                                        // SystemConecto.ErorDebag("Отсутствует элемент в конфигурации: " + reader.LocalName + ", значение - " + reader.GetAttribute(x) + " Тег: " + nameElement); // Отладка
                                        // ConfigVer = 2;
                                    }


                                }
                            }
                        

                            break;

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

                // Запись в память


                // Проверка версии файла конфигурации
                if (ConfigVer > 0)
                {
                    // Исправить файл конфигурации
                    //SystemConecto.ErorDebag("Конфигурация не верна, приложение приступило к исправлению..."); // Отладка
                    // запись измененных параметров
                    //if (reader != null)
                    //    reader.Close();
                    //CreateConfigXMLAppPlay(2);
                    //return true;
                }

            }
            catch (Exception ex)
            {
                SystemConecto.ErorDebag("При загрузке файла appplay.xml, возникло исключение: " + Environment.NewLine +
                    " === Message: " + ex.Message.ToString() + Environment.NewLine +
                    " === Exception: " + ex.ToString(), 1);
                if (reader != null)
                    reader.Close();
                // Перезапись по умолчанию параметров (нарушена конфигурация)
                SystemConecto.ErorDebag("Конфигурация не верна, приложение востанавливается из резервной копии..."); // Отладка
                //CreateConfigXMLAppPlay(2);

                // ------------- Окат переменных при возникновении ошибки
                ListPanelWorkSpace = ListPanelWorkSpaceUndo;
                ListPanelAdmin = ListPanelAdminUndo;
                ListPlayStory = ListPlayStoryUndo;
                aParamAppPlay = aParamAppPlayUndo;

                return false;
            }
            if (reader != null)
                reader.Close();
            return true;
        }


        #endregion

        #region Файл appplay.xml     - создание и изменение настроек системы

        /// <summary>
        /// TypePr - 0 создать файл; 1 - создать только масив; 2 - запись измененных параметров
        /// </summary>
        /// <param name="TypePr"></param>
        public void CreateConfigXMLAppPlay(int TypePr = 0)
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
                if (!aParamAppPlay.ContainsKey(0)) //"FileConfig_Ver-File-Config"
                {
                    //aParamAppPlay.Add("FileConfig_Ver-File-Config", "0.1");                 // Версия конфигурационного файла
                    //-------------------- Динамический список настройки конверторов
                    // aParamAppPlay.Add("Convertor_Pid_Speed", "");
                    
                    //-------------------- Примеры
                    //WriterConfigXML.WriteStartElement("СерверОбновленияConecto");
                    //WriterConfigXML.WriteStartElement("ServerUpdateConecto");
                    //WriterConfigXML.WriteAttributeString("DNS", "updatework.conecto.ua");
                    //aParamAppPlay.Add("ServerUpdateConecto_DNS", "updatework.conecto.ua");
                    //WriterConfigXML.WriteAttributeString("IP", "");
                    //aParamAppPlay.Add("ServerUpdateConecto_IP", "");
                    //WriterConfigXML.WriteAttributeString("Ver", "0.1");                        
                    //aParamAppPlay.Add("ServerUpdateConecto_Ver", "0.1");                            // Версия клиента Update
                    //aParamAppPlay.Add("ServerUpdateConecto_PortFTP", "21");                        // Авто настройка порта FTP
                    //aParamAppPlay.Add("ServerUpdateConecto_PortWEB", "80");                        // Авто настройка порта WEB
                    //aParamAppPlay.Add("ServerUpdateConecto_USER", "AppCoenectoWorkSpace");         // Пользователь Сервера
                    //aParamAppPlay.Add("ServerUpdateConecto_USER-Passw", "User-Pass2012");          // Пароль Сервера

                    //aParamAppPlay.Add("ServerSKDSwitch", "0");                                     // Включение SKD Сервера



                }

                // 2 - запись измененных параметров (до поры до времени то же самое, что и 0)
                if (TypePr == 2)
                {
                    

                }

                // Создать масив данных в конфигурационный файл
                if (TypePr == 0 || TypePr == 2)
                {

                    using (StringWriter sw = new StringWriter())
                    {
                        // Примеры и описание ниже
                        WriterConfigXML = new XmlTextWriter(sw);
                        WriterConfigXML.Formatting = Formatting.Indented; // упорядочивания структуры файла
                    
                            // Примеры и описание ниже
                            //WriterConfigXML = new XmlTextWriter(SystemConecto.PutchApp + "appplay.xml", System.Text.Encoding.Unicode);


                            // Создать масив данных XML
                            string[] aStartElement = new string[1];
                            aStartElement[0] = "";
                            string[] aEndElement = new string[1];
                            aEndElement[0] = "";
                            int Start = 0;
                            // -------------                              ---------------  
                            WriterConfigXML.WriteStartDocument();
                            WriterConfigXML.WriteStartElement("Параметры-SKDServer");

                            //foreach (KeyValuePair<string, AppPlayOptionStruct> kvp in aParamAppPlay)
                            //{
                            //    string sStartElement = kvp.Key;         // kvp.Value
                            //    aStartElement = sStartElement.Split('_');
                            //    /// Получаем первый елемент
                            //    if (aStartElement[0] == aEndElement[0])
                            //    {
                            //        // Запись атрибута
                            //       // WriterConfigXML.WriteAttributeString(kvp.Key, kvp.Value);
                            //    }
                            //    else
                            //    {
                            //        // начало нового концовка старого Элемента
                            //        aEndElement[0] = aStartElement[0];
                            //        if (Start > 0)
                            //        {
                            //            WriterConfigXML.WriteEndElement();
                            //        }
                            //        else
                            //        {
                            //            Start = 1;
                            //        }
                            //        WriterConfigXML.WriteStartElement(aStartElement[0]);
                            //        // Запись атрибута
                            //       // WriterConfigXML.WriteAttributeString(kvp.Key, kvp.Value);
                            //    }

                            //}
                            // Конец Элементов
                            if (Start == 1)
                            {
                                WriterConfigXML.WriteEndElement();
                            }
                            //--------------------
                            WriterConfigXML.WriteEndElement();

                            WriterConfigXML.WriteEndDocument();
                            
                        xmlString = sw.ToString();
                    }
                }

    
            }
            catch (Exception error)
            {
                SystemConecto.ErorDebag(error.Message);
            }
            finally
            {
                if (WriterConfigXML != null)
                {
                    if (TypePr == 0 || TypePr == 2)
                    {
                        WriterConfigXML.Close();
                    }
                    //WriterConfigXML = null;


                    // Режим отладки выключен и его надо записать в файл
                    //  0 - создать файл;
                    if (!SystemConecto.DebagApp && TypePr == 0)
                    {
                        SystemConecto.EncryptTextToFile(SystemConecto.PutchApp + "appplay.xml", xmlString);

                    }
                    if (SystemConecto.DebagApp && TypePr == 0)
                    {
                        File.WriteAllText(SystemConecto.PutchApp + "appplay.xml", xmlString);
                    }

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
        public static bool ControllerParamAppPlay(dynamic ParamValue, string NameParam, int TypeFunc = 0)
        {
            bool LoadParam = false;
            switch (TypeFunc)
            {
                case 0:
                    // 0 - Текстовая
                    //ParamValue.GetType();

                    // Запись
                    //aParamAppPlay[NameParam] = ParamValue;

                    LoadParam = true;
                    break;
                case 10:
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
                    //aParamAppPlay[NameParam] = ParamValue;



                    LoadParam = true;
                    break;

            }
            // Запись в файл если предыдущие значение тоже с ошибкой то записать последний результат (Разработка)
            if (LoadParam)
            {
                var Config = new SystemConfigControll();
                Config.CreateConfigXMLAppPlay(2);
            }

            // Применение (возможно определить метод)

            // Диагностика параметра
            // DiagnosParam(NameParam);

            // Проверка диагностики DiagnozParam

            return LoadParam;
        }
        #endregion


        #endregion

    }


}
