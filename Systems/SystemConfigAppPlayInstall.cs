// ==================================== Управление файлами конфигурации установки файлов для приложений ConectoWorkSpace
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
    class SystemConfigAppPlayInstall
    {
        #region Глобальные параметры (переменные)

        /// <summary>
        /// Файл данных содержит информаию об инсталируемых приложениях
        /// </summary>
        string PuthFileData = SystemConecto.PutchApp + "configinstallapp.xml";
        
        /// <summary>
        /// Параметры XML приложений для установки string key - id App
        /// </summary>
        public Dictionary<int, AppPlayOptionStruct> aParamPlayAppInstall = new Dictionary<int, AppPlayOptionStruct>();

        //public static AppPlayOptionStruct AppPlayOption = new AppPlayOptionStruct();


        /// <summary>
        /// Список ярлыков на панели PlayStoryAppInstall установка приложений number, id
        /// </summary>
        public static Dictionary<int, int> ListPlayAppInstall = new Dictionary<int, int>();


        #endregion
        
        
        
        
        #region Операции с файлами конфигурации приложения

        #region Файл configinstallapp.xml    - чтение программ которые можно установить в ConectoPlay


        /// <summary>
        /// Чтение в память файла configinstallapp.xml
        /// </summary>            // Чтение файла
        /// <param name="TypeFunc">тип чтения: 1 - из памяти</param>
        /// <returns></returns>
        public bool ReadConfigXMLAppPlayInstall(int TypeFunc = 0)
        {

            XmlTextReader reader = null;
            int ConfigVer = 1;
            string nameElement = "";
            // В потоке сбор информации о приложении
            int idApp = 0;
            // Завершения чтения информации в файле
            int EndFileLogic = 0;

            AppPlayOptionStruct AppPlayOption = new AppPlayOptionStruct();

            // Чтение файла
            reader = SystemConfigControll.ReadXMLFile(PuthFileData, TypeFunc);

            try
            {
                 while (reader.Read())
                {
                    if (reader.NodeType == XmlNodeType.Element || reader.NodeType == XmlNodeType.EndElement)
                        nameElement = reader.Name;

                    // Проверка параметров и версии конфигурации, а также целосность данных
                    // Исключение тег ?xml = ""
                    //  nameElement - структура [Параметр структуры]_[id]_[Name1]_[Name2]aParamPlayAppInstall

                    string[] a_nameElement = nameElement.Split('_');
                    switch (a_nameElement[0])
                    {
                        case "":

                            break;
                        case "Параметры-AppPlayInstall":
                            EndFileLogic++;
                            if (EndFileLogic == 2)
                            {

                                if (idApp == 0)
                                {

                                }
                                else
                                {
                                    aParamPlayAppInstall.Add(idApp, AppPlayOption);
                                }

                                // AppPlayOption = new AppPlayOptionStruct();
                                // SystemConecto.ErorDebag("Конец чтения тегов...", 1);
                            }
                            break;
                        // Проверка версии файла 
                        case "FileConfig":
                            if (ConfigVer == 1)
                            {
                                // проверка отсутствует reader.LocalName == "FileConfig_Ver-File-Config" - && reader.GetAttribute(x) == aParamPlayAppInstall["FileConfig_Ver-File-Config"].Version
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

                                }
                                else
                                {

                                    aParamPlayAppInstall.Add(idApp, AppPlayOption);
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
                            AppPlayOption.AutorizeType = reader.GetAttribute("AutorizeType") == null ? -1 : Convert.ToInt32(reader.GetAttribute("AutorizeType"));
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

                                    aParamPlayAppInstall.Add(idApp, AppPlayOption);
                                    idApp = Convert.ToInt32(a_nameElement[1]);
                                    AppPlayOption = new AppPlayOptionStruct();

                                }


                            }
                            // Запись праметров
                            PanelWorkSpace PanelWS_ = new PanelWorkSpace();

                            PanelWS_.LinkPanel = Convert.ToBoolean(reader.GetAttribute("LinkPanel"));
                            // Не совсем использую
                            PanelWS_.TypeLink = Convert.ToInt32(reader.GetAttribute("TypeLink"));
                            // Порядок вывода и размещения ярлыка
                            PanelWS_.NumberPanel = Convert.ToInt32(reader.GetAttribute("NumberPanel"));
                            PanelWS_.CaptionNameWorkSpace = reader.GetAttribute("CaptionNameWorkSpace");

                            // Панели
                            ListPlayAppInstall.Add(PanelWS_.NumberPanel, idApp);

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
                  
                }


            }
            catch (Exception ex)
            {
                SystemConecto.ErorDebag("Во время чтения файла: " + PuthFileData + ", возникло исключение: " + Environment.NewLine +
                    " === Message: " + ex.Message.ToString() + Environment.NewLine +
                    " === Exception: " + ex.ToString(), 1);
                return false;
            }
            finally
            {
                if (reader != null)
                    reader.Close();
            }
            return true;
        }


        #endregion

        #region Файл configinstallapp.xml     - создание и изменение настроек системы

        /// <summary>
        /// TypePr - 0 создать файл; 1 - создать только масив; 2 - запись измененных параметров
        /// </summary>
        /// <param name="TypePr"></param>
        public void CreateConfigXMLAppPlay(int TypePr = 0)
        {

            // XML текст String
            string xmlString = null;
            XmlTextWriter WriterConfigXML = null;

        

            try
            {
                // Проверка создания массива данных - настройки программы
                if (!aParamPlayAppInstall.ContainsKey(0))
                {
                    // Приложение такое недопускает
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
                        //WriterConfigXML = new XmlTextWriter(SystemConecto.PutchApp + "configinstallapp.xml", System.Text.Encoding.Unicode);


                        // Создать масив данных XML
                        string[] aStartElement = new string[1];
                        aStartElement[0] = "";
                        string[] aEndElement = new string[1];
                        aEndElement[0] = "";
                        int Start = 0;
                        // -------------                              ---------------  
                        WriterConfigXML.WriteStartDocument();
                        WriterConfigXML.WriteStartElement("Параметры-SKDServer");

                        //foreach (KeyValuePair<string, AppPlayOptionStruct> kvp in aParamPlayAppInstall)
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
                        SystemConecto.EncryptTextToFile(PuthFileData, xmlString);

                    }
                    if (SystemConecto.DebagApp && TypePr == 0)
                    {
                        File.WriteAllText(PuthFileData, xmlString);
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
                    //aParamPlayAppInstall[NameParam] = ParamValue;

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
                    //aParamaParamPlayAppInstallAppPlay[NameParam] = ParamValue;



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

namespace ConectoWorkSpace.Systems
{
   
}
