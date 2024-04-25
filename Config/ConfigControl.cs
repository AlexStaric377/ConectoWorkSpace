// ==================================== Управление файлами конфигураций для риложений ConectoWorkSpace
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

// ==================================== Используем функции ядра SystemConecto.dll

namespace ConectoWorkSpace
{
    
    
    
    /// <summary>
    /// Управление конфигурационными файлами ConfigControll. Разделяемый класс по файлам (ключевое слово - partial)
    /// </summary>
    public partial class ConfigControll
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


        #region Файл *.xml    - чтение


        /// <summary>
        /// Чтение в память файла *.xml
        /// </summary>

        /// <param name="PuthFileRead"> Путь к четаемому файлу SystemConecto.PutchApp + "appplay.xml"</param>
        /// 
        /// <param name="ReadFileXMLOpcii_"> Опции для чтения файла (Properties.Resources.appplay)</param>
        /// 
        /// <param name="TypeFunc">Тип чтения файла по умолчанию 0 - обычный файл</param>
        /// 
        /// <param name="ConfigVer"> Необходимая Версия файла 0 - нет требования ReadFileXMLOpcii  </param>
        /// <returns></returns>
        public static Dictionary<int, Dictionary<string, string>> ReadConfigXML(string PuthFileRead, ReadFileXMLOpcii ReadFileXMLOpcii_,  int TypeFunc = 0, int ConfigVer = 0)
        {
            // Объект чтения xml данных
            XmlTextReader reader = null;

            // Название елемента
            string nameElement = "";
            // В потоке сбор информации по группе тегов принадлижащих одному объекту параметров
            //  nameElement - структура [Параметр структуры]_[id]_[Name1]_[Name2]
            int id = 0;
            // Завершения чтения информации в файле
            int EndFileLogic = 0;

            //Структура параметров тегов
            Dictionary<string, string> XMLOption = ReadFileXMLOpcii_.StructParamTag;


            Dictionary<int, Dictionary<string, string>> alOptionXML = new Dictionary<int, Dictionary<string, string>>(); // ReadFileXMLOpcii_.alOptionXML;

            // -------------  Чистка памяти без отката нужно с откатом - перезагрузка параметров



            try
            {
                // Если файл отсутствует читаем из ресурсов
                if (TypeFunc == 1 || !SystemConecto.File_(PuthFileRead, 5))
                {
                    // === Неудачные примеры чтения файлов
                    // Uri PuthFileUri = new Uri(@"/Conecto®%20WorkSpace;/appplay.xml", UriKind.Relative);
                    // System.Windows.Resources.StreamResourceInfo info = System.Windows.Application.GetContentStream(PuthFileUri);
                    // info.Stream

                    //StreamReader sReader = new StreamReader(Properties.Resources.appplay);
                    // ==============
                    // Чтение обычного файла

                    reader = new XmlTextReader(new StringReader(ReadFileXMLOpcii_.FileDefault.ToString()));
                }
                else
                {
                    // Режим Компиляции Релиза приложения
                    if (AppStart.ReleaseCandidate == "Release")
                    {
                        // Чтение зашифрованого файла
                        var TextFile = SystemConecto.DecryptTextToFile(PuthFileRead);
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
                        reader = new XmlTextReader(PuthFileRead);

                    }
                }


                // Свойсва чтения
                reader.WhitespaceHandling = WhitespaceHandling.None; // пропускаем пустые узлы

                while (reader.Read())  //while (reader.Read() && reader.Name == "Товар")
                {
                    if (reader.NodeType == XmlNodeType.Element || reader.NodeType == XmlNodeType.EndElement)
                        nameElement = reader.Name;

                    // Проверка параметров и версии конфигурации, а также целосность данных
                    // Исключение тег ?xml = ""
                    //  nameElement - структура [Параметр структуры]_[id]_[Name1]_[Name2]

                    string[] a_nameElement = nameElement.Split('_');

                    // Модификация базовых спец тегов
                    string[] a_nameElementDef = a_nameElement[0].ToString().Split('-');
                    switch (a_nameElementDef[0])
                    {
                        case "Параметры":
                            a_nameElement[0] = a_nameElementDef[0];
                            break;
                    }

                    switch (a_nameElement[0])
                    {
                        case "":
                            // Пустой пропускаем

                            break;
                        case "Параметры":
                            // Тег по умочанию, модифицирован от Параметры-ттт  начало чтения параметров
                            EndFileLogic++;
                            if (EndFileLogic == 2)
                            {
                                if (id == 0)
                                {

                                }
                                else
                                {
                                    //ReadFileXMLOpcii_.StructXMLFile.Add(id, XMLOption);
                                    alOptionXML.Add(id, XMLOption);
                                }

                            }
                            break;
                        // Проверка версии файла 
                        case "FileConfig":
                            // Дополнительно проверить версию
                            if (ConfigVer > 0)
                            {
                                // проверка отсутствует reader.LocalName == "FileConfig_Ver-File-Config" - && reader.GetAttribute(x) == aParamAppPlay["FileConfig_Ver-File-Config"].Version

                                // Исправить файл конфигурации
                                //SystemConecto.ErorDebag("Конфигурация не верна, приложение приступило к исправлению..."); // Отладка
                                // запись измененных параметров
                                //if (reader != null)
                                //    reader.Close();
                                //CreateConfigXMLAppPlay(2);
                                //return true;
                                alOptionXML = null;
                                return alOptionXML;

                            }

                            if (XMLOption.ContainsKey("Version"))
                            {
                                XMLOption["Version_File_Config"] = reader.GetAttribute("FileConfig_Ver-File-Config");
                            }
                            else
                            {
                                if (ReadFileXMLOpcii_.TypeReadParam == 1)
                                    XMLOption.Add("Version_File_Config", reader.GetAttribute("FileConfig_Ver-File-Config"));
                            }


                            break;
                        default:

                            id = Convert.ToInt32( (a_nameElement.Length > 1 ? a_nameElement[1] : "0") );

                            //  Проверка параметров тега присутствие 
                            //  Расшифровка параметра reader.LocalName - структура [id]_[Name1]_[Name2]

                            // Создаем масив 
                            // SystemConecto.aParamApp.Add(reader.Name, "");  reader.GetAttribute("Дата"); reader.Name
                            // reader.AttributeCount //Количесвто атрибутов
                            // reader.MoveToAttribute   // Перемещает по индексу на атрибут
                            // Чтение отрибутов универсальное
                            if (reader.HasAttributes)
                            {
                                for (int x = 0; x < reader.AttributeCount; x++)
                                {
                                    // Перейти к атрибуту
                                    reader.MoveToAttribute(x);

                                    if (XMLOption.ContainsKey(reader.LocalName.ToString()))
                                    {
                                        XMLOption[reader.LocalName.ToString()] = reader.GetAttribute(x);
                                    }
                                    else
                                    {
                                        if (ReadFileXMLOpcii_.TypeReadParam == 1)
                                            XMLOption.Add(reader.LocalName.ToString(), reader.GetAttribute(x));
                                    }

                                }
                            }


                            break;

                    }

                }


            }
            catch (Exception ex)
            {
                SystemConecto.ErorDebag("При загрузке файла " + PuthFileRead + ", возникло исключение: " + Environment.NewLine +
                    " === Message: " + ex.Message.ToString() + Environment.NewLine +
                    " === Exception: " + ex.ToString(), 1);
                if (reader != null)
                    reader.Close();

                // ------------- Окат переменных при возникновении ошибки

                alOptionXML = null;

                return alOptionXML;
            }
            finally
            {
                if (reader != null)
                    reader.Close();
            }

            return alOptionXML;
        }


        #endregion



        #region Чтение отдельных параметров без изменения глобальных значений

        /// <summary>
        /// Чтение отдельных параметров без изменения глобальных значений
        /// </summary>
        /// <param name="ArrayParam">Список читаемых параметров</param>
        /// <returns></returns>
        public static Dictionary<int, Dictionary<string, string>> ReadParam(string PuthFileRead, string[] ArrayParam = null)
        {
            if (ArrayParam == null)
                return null;
            
            ReadFileXMLOpcii OpciiRead = new ReadFileXMLOpcii();
            OpciiRead.TypeReadParam = 0;

            //OpciiRead.FileDefault = Properties.Resources.servers;


            //Структура параметров тегов
            Dictionary<string, string> XMLOption = new Dictionary<string, string>();

            // Доп. парам.
            for (int i = 0; i < ArrayParam.Length; i++)
            {
                if (!XMLOption.ContainsKey(ArrayParam[i].ToString()))
                {
                    XMLOption.Add(ArrayParam[i].ToString(), "");
                }
            }


            OpciiRead.StructParamTag = XMLOption;


            return ReadConfigXML(PuthFileRead, OpciiRead);

        }

        #endregion

        #region Чтение отдельных параметров без изменения глобальных значений для одного id

        /// <summary>
        /// Чтение отдельных параметров без изменения глобальных значений
        /// </summary>
        /// <param name="ArrayParam">Список читаемых параметров</param>
        /// <returns>null - не определено значение</returns>
        public static Dictionary<string, string> ReadParamID(string PuthFileRead, int ID_, string[] ArrayParam = null)
        {
            if (ArrayParam == null)
                return null;

            var ScherchID = ReadParam(PuthFileRead, ArrayParam);
            
            if(ScherchID.ContainsKey(ID_)){
                return ScherchID[ID_];
            }


            return null;

        }

        #endregion

        #region Чтение отдельных параметров без изменения глобальных значений для одного id возвращение занчение Value

        /// <summary>
        /// Чтение отдельных параметров без изменения глобальных значений
        /// </summary>
        /// <param name="ArrayParam">Список читаемых параметров</param>
        /// <returns>null - не определено значение</returns>
        public static string ReadParamIDValue(string PuthFileRead, int ID_, string[] ArrayParam = null)
        {
            if (ArrayParam == null)
                return "";

            var ScherchID = ReadParam(PuthFileRead, ArrayParam);

            if (ScherchID.ContainsKey(ID_))
            {
                return ScherchID[ID_][ArrayParam[0].ToString()];
            }


            return "";

        }

        #endregion

    }


}
