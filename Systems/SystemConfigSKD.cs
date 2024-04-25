// ==================================== Управление файлами конфигураций для СКД (систем контроля доступа)
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
    /// <summary>
    ///  Разделяемый класс по файлам (ключевое слово - partial)
    /// </summary>

    public partial class SystemConfigControll // SystemConecto
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
         *              skd.xml         - настройки skd
         * 
         * 
         */

        #endregion

        #region Глобальные параметры (переменные)

        /// <summary>
        /// Параметры приложения
        /// </summary>
        public static Dictionary<string, string> aParamSKD = new Dictionary<string, string>();
        
        // Пример блокировки файла конфигурации
        // public static FileStream XMLConfigFile = null; // Блокировка основного файла кофигурации
        // 7. Запуск блокировки файлов - функция безопасности
        // SystemConecto.File_(SystemConecto.PutchApp + "config.xml", 6, SystemConfig.XMLConfigFile);

        // public XmlTextWriter WriterConfigXML = null;        // Объект - параметры конфигурации
        // public XmlTextWriter WriterConfigUserXML = null;    // Объект - параметры пользователя
        // public static string sData = "Mein Kopf Mein Kopf Mein Kopf"; // Создание строки для шифрования


        /// <summary>
        /// SystemConecto.aLanguage - Мультязычнось Локаль приложения [new Dictionary<string, string>();]
        /// SystemConecto.aParamApp - Параметры приложения [new Dictionary<string, string>();]
        /// 
        /// </summary>



        #endregion

        #region Операции с файлами конфигурации приложения

        #region Файл skd.xml     - чтение настройки скд системы

        public bool ReadConfigXMLSKD(int TypeFunc = 0)
        {

            XmlTextReader reader = null;
            // Dictionary<string, string> aParamAppChek = new Dictionary<string, string>(); // Параметры приложения проверка
            int ConfigVer = 1;

            try
            {
                // Режим отладки
                if (SystemConecto.DebagApp)
                {
                    // Чтение обычного файла
                    reader = new XmlTextReader(SystemConecto.PutchApp + "skd.xml");
                }
                else
                {
                    // Чтение зашифрованого файла

                    StringReader FileText = new StringReader(SystemConecto.DecryptTextToFile(SystemConecto.PutchApp + "skd.xml"));
                    reader = new XmlTextReader(FileText);

                    //reader = new XmlTextReader(new StringReader(SystemConecto.DecryptTextFromFile(SystemConecto.PutchApp + "skd.xml", SystemConfig.sData , @"м^ыiЎQRy3іљТФ>")));
                }
                reader.WhitespaceHandling = WhitespaceHandling.None; // пропускаем пустые узлы
                string nameElement = "";
                while (reader.Read())
                {
                    if (reader.NodeType == XmlNodeType.Element)
                        nameElement = reader.Name;
                    // Проверка параметров и версии конфигурации, а также целосность данных
                    // Исключение тег ?xml = ""
                    if (nameElement == "" || nameElement == "Параметры-SKDServer")
                    {
                    }
                    else
                    {
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
                                // Проверка версии файла
                                if (ConfigVer == 1 && nameElement == "FileConfig" && reader.LocalName == "FileConfig_Ver-File-Config" && reader.GetAttribute(x) == aParamSKD["FileConfig_Ver-File-Config"])
                                {
                                    ConfigVer = 0;
                                }
                                // Запись в память
                                // Проверка (на всякий случай)
                                if (aParamSKD.ContainsKey(reader.LocalName))
                                {

                                    aParamSKD[reader.LocalName] = reader.GetAttribute(x);
                                }
                                else
                                {
                                    // !---Установить проверку динамических параметров---
                                    aParamSKD.Add(reader.LocalName, reader.GetAttribute(x));

                                    // Запись отклонений
                                    // SystemConecto.ErorDebag("Отсутствует элемент в конфигурации: " + reader.LocalName + ", значение - " + reader.GetAttribute(x) + " Тег: " + nameElement); // Отладка
                                    // ConfigVer = 2;
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
                    SystemConecto.ErorDebag("Конфигурация не верна, приложение приступило к исправлению..."); // Отладка
                    // запись измененных параметров
                    if (reader != null)
                        reader.Close();
                    CreateConfigXMLSKD(2);
                    return true;
                }

            }
            catch (Exception error)
            {
                SystemConecto.ErorDebag(error.Message);
                if (reader != null)
                    reader.Close();
                // Перезапись по умолчанию параметров (нарушена конфигурация)
                SystemConecto.ErorDebag("Конфигурация не верна, приложение приступило к исправлению настроек по умолчанию..."); // Отладка
                CreateConfigXMLSKD(2);
                return false;
            }
            if (reader != null)
                reader.Close();
            return true;
        }


        #endregion

        #region Файл skd.xml     - создание и изменение настроек системы

        /// <summary>
        /// TypePr - 0 создать файл; 1 - создать только масив; 2 - запись измененных параметров
        /// </summary>
        /// <param name="TypePr"></param>
        public void CreateConfigXMLSKD(int TypePr = 0)
        {

            // XML текст String
            string xmlString = null;

        
            try
            {
                // Проверка создания массива данных - настройки программы
                if (!aParamSKD.ContainsKey("FileConfig_Ver-File-Config"))
                {
                    aParamSKD.Add("FileConfig_Ver-File-Config", "0.1");                 // Версия конфигурационного файла
                    //-------------------- Динамический список настройки конверторов
                    // aParamSKD.Add("Convertor_Pid_Speed", "");
                    
                    //-------------------- Примеры
                    //WriterConfigXML.WriteStartElement("СерверОбновленияConecto");
                    //WriterConfigXML.WriteStartElement("ServerUpdateConecto");
                    //WriterConfigXML.WriteAttributeString("DNS", "updatework.conecto.ua");
                    //aParamSKD.Add("ServerUpdateConecto_DNS", "updatework.conecto.ua");
                    //WriterConfigXML.WriteAttributeString("IP", "");
                    //aParamSKD.Add("ServerUpdateConecto_IP", "");
                    //WriterConfigXML.WriteAttributeString("Ver", "0.1");                        
                    //aParamSKD.Add("ServerUpdateConecto_Ver", "0.1");                            // Версия клиента Update
                    //aParamSKD.Add("ServerUpdateConecto_PortFTP", "21");                        // Авто настройка порта FTP
                    //aParamSKD.Add("ServerUpdateConecto_PortWEB", "80");                        // Авто настройка порта WEB
                   // aParamSKD.Add("ServerUpdateConecto_USER", "AppCoenectoWorkSpace");         // Пользователь Сервера
                    //aParamSKD.Add("ServerUpdateConecto_USER-Passw", "User-Pass2012");          // Пароль Сервера

                    //aParamSKD.Add("ServerSKDSwitch", "0");                                     // Включение SKD Сервера



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
                    
                    
                    //// Примеры и описание ниже
                    //WriterConfigXML = new XmlTextWriter(SystemConecto.PutchApp + "skd.xml", System.Text.Encoding.Unicode);


                        // Создать масив данных XML
                        string[] aStartElement = new string[1];
                        aStartElement[0] = "";
                        string[] aEndElement = new string[1];
                        aEndElement[0] = "";
                        int Start = 0;
                        // -------------                              ---------------  
                        WriterConfigXML.WriteStartDocument();
                        WriterConfigXML.WriteStartElement("Параметры-SKDServer");

                        foreach (KeyValuePair<string, string> kvp in aParamSKD)
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
                    if (!SystemConecto.DebagApp && TypePr == 0)
                    {
                        // Режим отладки выключен и его надо записать в файл
                        //  0 - создать файл;
                        if (!SystemConecto.DebagApp && TypePr == 0)
                        {
                            SystemConecto.EncryptTextToFile(SystemConecto.PutchApp + "skd.xml", xmlString);

                        }
                        if (SystemConecto.DebagApp && TypePr == 0)
                        {
                            File.WriteAllText(SystemConecto.PutchApp + "skd.xml", xmlString);
                        }
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
        public static bool ControllerParamSKD(dynamic ParamValue, string NameParam, int TypeFunc = 0)
        {
            bool LoadParam = false;
            switch (TypeFunc)
            {
                case 0:
                    // 0 - Текстовая
                    //ParamValue.GetType();

                    // Запись
                    aParamSKD[NameParam] = ParamValue;

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
                    aParamSKD[NameParam] = ParamValue;



                    LoadParam = true;
                    break;

            }
            // Запись в файл если предыдущие значение тоже с ошибкой то записать последний результат (Разработка)
            if (LoadParam)
            {
                var Config = new SystemConfigControll();
                Config.CreateConfigXMLSKD(2);
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
