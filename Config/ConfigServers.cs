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
    /// Управление конфигурационными файлами ConfigControll 
    /// </summary>
    public class ConfigControllServer
    {


        #region Структура данных параметров свойств приложения
        /// <summary>
        ///  Структура параметров свойств серверов
        /// </summary>
        public struct ServersOptionStruct
        {

            /// <summary>
            /// код приложения (служебное циф)
            /// </summary>
            public int id { get; set; }

            /// <summary>
            /// Название приложения (служебное англ.)
            /// </summary>
            public string NameServers { get; set; }

            /// <summary>
            /// Название приложения в окне PalyStory (наследуется из PalyStory)
            /// </summary>
            public string CaptionName { get; set; }

            /// <summary>
            /// Версия файла - нужно проверять с параметром WorkSpace - SystemConecto.aParamApp[...]
            /// </summary>
            public string Version { get; set; }

            /// <summary>
            /// При нажатии приложение не запускается, а выводится информационное окно о продукте на экран WorkSpace - Рекламма
            /// </summary>
            public string StartMetod { get; set; }

            /// <summary>
            /// Тип авторизации: -1 - без авторизации, 1 - пин авторизация, 0- авторизация через логин и пароль
            /// </summary>
            public bool AutoStatrt { get; set; }

             /// <summary>
            /// Путь к изображению для ConectoWorkSpace
            /// </summary>
            public string TypeServer { get; set; }

            /// <summary>
            /// Путь к изображению для ConectoWorkSpace
            /// </summary>
            public string PuthFileIm { get; set; }
            

            /// <summary>
            /// FTP сервисы
            /// </summary>
            public AppPlayOptionStructFTP FTP { get; set; }
            /// <summary>
            /// Dll модули
            /// </summary>
            public AppPlayOptionStructdll Dll { get; set; }

            /// <summary>
            /// Дополнительные переменные елементы установленные пользователями (резерв)
            /// </summary>
            public Dictionary<string, string> UserconfigWorkSpace { get; set; }

        }




        #endregion

  
        #region Глобальные параметры (переменные)

        /// <summary>
        /// Параметры Сервера key - id Server
        /// </summary>
        public static Dictionary<int, Dictionary<string, string>> aServersOption = new Dictionary<int, Dictionary<string, string>>();

        #endregion

        #region Операции с файлами конфигурации приложения

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ArrayParam">Дополнительные параметры</param>
        /// <returns></returns>
        public static bool ReadConfigServers(string PuthFileRead, Array[] ArrayParam = null)
        {
            ConfigControll.ReadFileXMLOpcii OpciiRead = new ConfigControll.ReadFileXMLOpcii();
            OpciiRead.TypeReadParam = 0;

            OpciiRead.TextDefault = Properties.Resources.servers;


            //Структура параметров тегов
            Dictionary<string, string> XMLOption = new Dictionary<string, string>();
            XMLOption.Add("id", "");
            XMLOption.Add("NameServers", "");
            XMLOption.Add("CaptionName", "");
            XMLOption.Add("StartMetod", "");
            XMLOption.Add("Version", "");
            XMLOption.Add("AutoStatrt", "");
            XMLOption.Add("TypeServer", "");
            XMLOption.Add("PuthFileIm", "");

            // Доп. парам.
            if (ArrayParam != null)
            {
                for (int i = 0; i < ArrayParam.Length; i++)
                {
                    if (!XMLOption.ContainsKey(ArrayParam[i].ToString()))
                    {
                        XMLOption.Add(ArrayParam[i].ToString(), "");
                    }
                }
            }

            OpciiRead.StructParamTag = XMLOption;

            OpciiRead.PuthFileRead = PuthFileRead;

            //Properties.Resources.servers,

            var aServersOption_ = ConfigControll.ReadConfigXML(OpciiRead);

            if (aServersOption_ == null)
            {
                return false;
            }
            else
            {
                // Перезагрузка опций
                aServersOption = aServersOption_;
            }

            return true;
       
        }

        #endregion

    }


}
