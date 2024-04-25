#region импорт следующих имен пространств .NET:
using System;
using System.Collections.Generic;
// Управление БД
using System.Data;
// локаль операционной системы
using System.Globalization;
// Управление вводом-выводом
using System.IO;
using System.IO.Compression;
using System.Text;
using System.Linq;

using System.Windows.Controls;
using ConectoWorkSpace;
#endregion

namespace ConectoWorkSpace
{

    //-------------------------------------- Работа с сетью

    /// <summary>
    ///  Разделяемый класс по файлам (ключевое слово - partial)
    /// </summary>

    public partial class SystemConecto
    {
        /// <summary>
        /// Список локалей по умолчанию List {ru-ru}
        /// </summary>
        public static List<string> DefaultCulture = new List<string>();

        /// <summary>
        /// Мультиязычнось Локаль приложения
        /// </summary>
        public static Dictionary<string, string> aLanguage = new Dictionary<string, string>();


        #region Дополнитеная проверка наличия слов в словоре

        /// <summary>
        /// Возвращает слово в любом случаии даже если его нету в словаре
        /// </summary>
        /// <param name="Word"></param>
        /// <returns></returns>
        public static string returnLanguage(string Word)
        {

            string result;

            if (SystemConecto.aLanguage.TryGetValue(Word, out result))
                return result;
            else
                return Word;

        }
        #endregion



    }


    #region Language : UserControl
    /// <summary>
    /// Чтение данных для xaml из масива
    /// </summary>
    public class Language : UserControl
    {

        /// <summary>
        /// Чтение данных для xaml без проверки перевода из масива
        /// xmlns:lang="clr-namespace:ConectoWorkSpace" - импорт следующих имен пространств .NET
        /// <grid><lang:Language x:Name="LS" /></grid> - размещения масива данных под именем LS  для привязки Language : UserControl
        /// "{Binding PrLanguage[Каталог песен], ElementName=LS}" - вывод текста 
        /// </summary>
        public static Dictionary<string, string> PrLanguage
        {
            get
            {
                //KeyValuePair
                //aLanguage.ContainsKey(Key) ?
                return SystemConecto.aLanguage;
            }

            // Только для чтения пока
            // set
            // {
            //    SystemConecto.aLanguage = value;
            //    //if (_name != value)
            //    //        {
            //    //            _name = value;
            //    //            this.OnPropertyChanged("Name");
            //    //        }
            // }
        }
    }
    #endregion

    public partial class SystemConectoLanguage
    {
        #region Локализация приложения (Мультиязычночть)

        // Оставщиеся вопросы Заглавные буквы в словах и в словарях
        

        #region Загрузка культур для языков версий

        static void LoadCulture()
        {

            SystemConecto.DefaultCulture.Add("ru-ru");
            // Загрузить ползовательскую локаль 20 записей в резерве
            for (int i = 0; i < 21; i++)
            {
                SystemConecto.DefaultCulture.Add("reserve");
            }



        }
        #endregion

        #region Загрузка локали основного приложения

        /// <summary>
        /// Локализация приложения (Мультиязычночть)
        /// </summary>
        /// <param name="UserLoadCulture">Резерв - предусмотреть переключение языка интерфейса пользователя на лету. Код масива DefaultCulture</param>
        public static void LanguageLoad(int UserLoadCulture = 0)
        {

            if (SystemConecto.DefaultCulture.Count == 0)
                LoadCulture();

            // Аварийное завершение программы
            bool STOPApp = UserLoadCulture == -1 ? true : false;
            if (STOPApp)
                UserLoadCulture = 0;

            /// Локализация приложения до загрузки файла система может считовать локаль из процедуры
            /// Можно локализировать через файл в папке language
            /// если файл отсутствует приложение использует по умолчанию три локали Русский, Украинский, Английский 
            /// !!! Со временем необходимо определить месторасположения по умолчанию для корректировки языков и свойств локали

            // Система сильно не разлечает культурные настройки по причине того что пользователь будет сам выбирать предпочитаемый язык
            string language = "";
            // Пользовательская локаль
            if (UserLoadCulture > 0){
                language = SystemConecto.DefaultCulture[UserLoadCulture];
            }
            else{
                language = CultureInfo.CurrentCulture.Name.ToLower();
            }

            string FileName = "culture_" + language + ".csv";
            string FullPatchFileCulture = SystemConecto.PutchApp + @"config\user\" + FileName;


            bool FileCultureFind = true;

            // Исключаем аварийный режим
            if (STOPApp)
            {
                language = SystemConecto.DefaultCulture[0];
            }
            else
            {

                // Проверка файла 
                // Проверка системной БД в каталоге зборки в папке Conecto на FTP
                Dictionary<string, string> dataList = new Dictionary<string, string>();
                dataList.Add(FullPatchFileCulture, "");
                if (SystemConecto.IsFilesPRG(dataList, -1, "- reading a file Culture with FTP") != "True")
                {
                    // файл отсутствует - Язык по умолчанию глобально
                    language = SystemConecto.DefaultCulture[0];
                    FileCultureFind = false;
                }


                #region Проверка языка

                // *.csv
                // Читать константу из конфига о версии языкового файла для обновления
                if (!FileCultureFind)
                {
                    // Запись файла для локального перевода языка интерфейса
                    Encoding win1251 = Encoding.GetEncoding("windows-1251");
                    System.IO.File.WriteAllText(FullPatchFileCulture, ConectoWorkSpace.Properties.Resources.culture_ru_ru_csv, win1251);
                }

            }
           

            #endregion


            // ErorDebag("приложение запущено с параметрами языка: " + language, 1
            // aLanguage["Error1"] = "Ошибка: ";




            // Локаль  поддерживается приложением

            #region Текстовые данные в формате CSV записать в таблицу

            string s = ConectoWorkSpace.Properties.Resources.culture_ru_ru_csv; // "Id,Name ,Dept\r\n1,Mike,IT\r\n2,Joe,HR\r\n3,Peter,IT\r\n";


            string[] tableData = s.Split("\r\n".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
            // Формирование стуктуры таблицы
            //var col = from cl in tableData[0].Split(",".ToCharArray())
            //          select new DataColumn(cl);
            //ReadTableFile.Columns.AddRange(col.ToArray());

            // Нерабочий код
            //(from st in tableData.Skip(1)
            // select ReadTableFile.Rows.Add(st.Split(",".ToCharArray()))).ToList();

            foreach (var item in tableData.Skip(1))
            {
                // Запись в таблицу
                // ReadTableFile.Rows.Add(item.Split(",".ToCharArray()));
                // Запись в масив минуя таблицу
                string[] updaterow = item.Split(",".ToCharArray());
                try
                {
                    SystemConecto.aLanguage.Add(updaterow[0].ToString(), updaterow[0].ToString());
                }
                catch
                {
                    // нарушение структуры файла - дубликат фразы
                    SystemConecto.ErorDebag(string.Format("В словаре {0}: обнаружен дубликат фразы или слова - [{1}]", FileName, updaterow[0].ToString()));

                }
                

            }

            #endregion

         
            // Поиск локалей в памяти приложения при отсутствии загрузить пользовательскую таблицу
            if (SystemConecto.DefaultCulture.IndexOf(language) == -1)
            {
                // Локаль пользователя читается с файла если она описана

                #region Слабый код (замедление программы)

                // Прочитать в таблицу. Эта процедура замедляет работу приложения.
                // Этот файл нужно проверять, только на изменения данных. Изменненные данные переносить в таблицу локальной БД и 
                // на сервер всей системы для обновления клиентов.
                // Если это необходимо. Проверку осуществлять с помощью хеша и даты последней редакции файла (последняя для ускорения загрузки)
                DataTable ReadTableFile = new DataTable();

                ReadTableFile = DBConecto.ReadCSVDefault(FileName, SystemConecto.PutchApp + @"config\user");
                #endregion
                // Проверка целосности информации в файле
                int CountDataColum = ReadTableFile.Columns.Count;

                // Проверка наличия  данных
                if(CountDataColum > 1){
                    //string result;
                   
                    // Вариант работы с таблицей обращение к записи таблицы
                    for (int curRow = 0; curRow < ReadTableFile.Rows.Count; curRow++)
                    {
                        DataRow updaterow = ReadTableFile.Rows[curRow];
                        //Проверка вносимых данных
                        if(updaterow[1].ToString().Length > 0){
                            // исключаем лишнюю проверку
                            //if (SystemConecto.aLanguage.TryGetValue(updaterow[0].ToString(), out result))
                            try
                            {
                                SystemConecto.aLanguage[updaterow[0].ToString()] = updaterow[1].ToString();
                            }
                            catch
                            {
                                // нарушение структуры файла - что-то новое
                                SystemConecto.ErorDebag(string.Format("В словаре {0}: не обнаружена фраза или слова - [{1}]", FileName, updaterow[0].ToString()));
                            }

                        }
                    }
                }
                ReadTableFile.Dispose();
            }


           
            // Тест
            //var Test = aLanguage_["Выполняется"];

            //var Test1 = Test;

            //aLanguage_["Выполняется"] = "Привет";
            //Test = aLanguage_["Выполняется"];
            //Test1 = Test;

            // Загружать локализацию из файла (Структура файла ini)
            // если он есть в корневой директории или в директории bin
            // Сформировать файл кторый вставить в упаковку и прочитать его от туда .... (Разработка) Базовые версии программы

        }
        #endregion

        #region Загрузка локали App APS  AppPlayStory разработка
        /// <summary>
        /// Локализация приложения (Мультиязычночть)
        /// </summary>
        /// <param name="UserLoadCulture">Резерв - предусмотреть переключение языка интерфейса пользователя на лету. Код масива DefaultCulture</param>
        public static void LanguageLoadAPS(int UserLoadCulture = 0)
        {

        }
        #endregion

        #endregion



    }

}