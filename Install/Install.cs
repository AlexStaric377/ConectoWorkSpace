using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using System.Reflection;
// --- Process
using System.Diagnostics;
// --- Puth
using System.IO;
using System.Windows;
// WMI
using System.Management;
// Network
using System.Net;
using System.Net.NetworkInformation;

// for ini files
using System.Collections.Specialized;
using System.Collections;
using System.Text.RegularExpressions;




namespace ConectoWorkSpace
{
    
    public static class Install
    {

        //  Управление устройствами
        //  FormatByteCount - размер USB накопителя


        /// <summary>
        /// Проверка подключенны устройств до возникновения события вставить флешку ключь или другое назначение
        /// я думаю запустить в отдельном потоке, хотя есть предположения, что при старте всеже прорисовывается интерфейс может и не нужно
        /// важно выбрать компромис скорости или функциональности
        /// </summary>
        public static void ChekDeviceUSB()
        {
            // 1. Приложение ищит все диски и файлы ключа, ...)
            DriveInfo[] DriveList = DriveInfo.GetDrives();
            foreach (DriveInfo d in DriveList)
            {
                if (d.IsReady == true && d.DriveType == DriveType.Removable)
                {
                    // Если находим два ключа берем тот который с максимальными правами (имя ключа - terminalcon.xml)


                }
            }
            // Пример включения пользователя
            // SystemConecto.Autoriz("Conecto_root", "QWas123GHk");

        }




        #region Включения USB устройства в ОС
        /// <summary>
        /// Включения USB устройства в ОС
        /// </summary>
        private static void AddUSBHandler()
        {

        }
        #endregion



        /// <summary>
        /// Класс для чтения/записи INI CFG - файлов с
        /// Для ОС Windows сторонних производителей
        /// </summary>
        public class INIManager
        {
            //Конструктор, принимающий путь к INI-файлу
            public INIManager(string aPath)
            {
                path = aPath;
            }
            string EXE = Assembly.GetExecutingAssembly().GetName().Name;

            //Конструктор без аргументов (путь к INI-файлу нужно будет задать отдельно)
            public INIManager() : this("") { }

            /// <summary>
            /// Возвращает значение из INI-файла (по указанным секции и ключу) 
            /// </summary>
            public string ReadKey(string aSection, string aKey)
            {
                //Для получения значения
                StringBuilder buffer = new StringBuilder(SIZE);

                //Получить значение в buffer
                GetPrivateString(aSection, aKey, null, buffer, SIZE, path);

                //Вернуть полученное значение
                return buffer.ToString();
            }

            /// <summary>
            /// Пишет значение в INI-файл (по указанным секции и ключу) 
            /// </summary>
            public void WriteKey(string aSection, string aKey, string aValue)
            {
                //Записать значение в INI-файл
                WritePrivateString(aSection, aKey, aValue, path);
            }

            private void Write(string Key, string Value, string Section)
            {
                //Записать значение в INI-файл
                WritePrivateString(Section, Key, Value, Path);
            }

            ///  <summary>
            ///  Удаление ключа
            ///   </summary>
            public void DeleteKey(string Key, string Section)
            {
                Write(Key, null, Section);
            }

            ///  <summary>
            ///  Удаление Секции
            ///   </summary>
            public void DeleteSection(string Section)
            {
                Write(null, null, Section);
            }


            ///  <summary>
            ///  Поиск ключа в Секции
            ///  
            /// </summary>
            /// <returns>bool</returns>
            public bool KeyExists(string Key, string Section)
            {
                return ReadKey(Section, Key).Length > 0;
            }


            ///Возвращает или устанавливает путь к INI файлу
            public string Path { get { return path; } set { path = value; } }

            //Поля класса
            private const int SIZE = 2024; //Максимальный размер (для чтения значения из файла)
            private string path = null; //Для хранения пути к INI-файлу

            //Импорт функции GetPrivateProfileString (для чтения значений) из библиотеки kernel32.dll
            [DllImport("kernel32.dll", EntryPoint = "GetPrivateProfileString", CharSet = CharSet.Unicode)]
            private static extern int GetPrivateString(string section, string key, string def, StringBuilder buffer, int size, string path);

            //Импорт функции WritePrivateProfileString (для записи значений) из библиотеки kernel32.dll
            [DllImport("kernel32.dll", EntryPoint = "WritePrivateProfileString", CharSet = CharSet.Unicode)]
            private static extern int WritePrivateString(string section, string key, string str, string path);
        }

        /// <summary>
        /// Класс для чтения/записи INI CFG - файлов с
        /// Для ОС Windows сторонних производителей
        /// </summary>
        public class Extract
        {
            /// <summary>
            /// Разархивирование с помощью Procces консоли 7z архиватора<para></para>
            /// 
            /// <param name="putch_ist">полный путь где находится архив</param><para></para>
            /// <param name="ml_files">список файлов в архиве, все файлы new string[]{}</param><para></para>
            /// <param name="putarh"></param><para>куда разархивировать</para>
            /// <param name="ml_name_arh">имя архива</param><para></para>
            /// <returns></returns>
            /// </summary>
            public static bool Unarch_file(string putch_ist, string[] ml_files, string putarh, string NOCatalog = "e")  //, string[] ml_name_arh
            {
                string PutchArh = SystemConecto.PutchApp + @"Utils\7za\7za.exe";

                // Проверка архиватора
                if (!Install7za())
                {
                    //нет архиватора
                    SystemConecto.ErorDebag("Отсутствует архиватор в каталоге: " + PutchArh);
                    //найти в инсталяшки разработка в новой версии
                    return false;
                }

                //Готовность устройства

                string[] aDir = putch_ist.Split('\\');
                /// Получаем первый елемент
                string aDirDevice = aDir[0];

                var DeviceInfo = DevicePC.ChekDevice(aDirDevice);
                if (Convert.ToBoolean(DeviceInfo[0]))
                {
                }
                else
                {
                    // Устройство не готово
                    SystemConecto.ErorDebag(string.Format("Устройство {0} для получения данных не готово.", aDirDevice));
                }

 
                // D:\ECV\EXE\7za.exe e D:\ECV\*.zip -oD:\ECV -y
                // Распоковать все

                // -y - ответить на все вопросы утвердительно да для фоновой распоковки (перезапись файлов)

                string ml_files_str = string.Join(" ", ml_files);

                // Распоковать только один файл
                // 7za.exe e  BD0501000.7z ModelFan.ru.txt -oTmp -y
                // запускаем процесс
                var pr = new Process();

                pr.StartInfo.FileName = "7za.exe";

                pr.StartInfo.Arguments = " "+ NOCatalog + " " + putch_ist + " " + ml_files_str + " -o" + putarh + " -y";

                pr.StartInfo.CreateNoWindow = true;
                pr.StartInfo.WorkingDirectory = SystemConecto.PutchApp + @"Utils\7za\";
                pr.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                pr.StartInfo.UseShellExecute = false;
                pr.StartInfo.RedirectStandardOutput = true;
                // Отладка
                SystemConecto.ErorDebag(pr.StartInfo.WorkingDirectory + pr.StartInfo.FileName + pr.StartInfo.Arguments, 1);
                pr.Start();
                // получаем ответ запущенного процесса
                StreamReader srIncoming = pr.StandardOutput;
                // выводим результат
                SystemConecto.ErorDebag(srIncoming.ReadToEnd());

                pr.WaitForExit();
                pr.Close();




                return true;
            }

            /// <summary>
            ////        7-Zip (A) 9.20 Copyright (C) 1999-2010 Igor Pavlov 2010-11-18

            ////Использование: 7za <command> [<switches> ...] <archive_name> [<file_names> ...]
            ////        [<@ отсортированном ...>]

            ////<Commands>
            ////   a: Добавить файлы в архив
            ////   b: Benchmark
            ////   d: Удалить файлы из архива
            ////   e: Извлечь файлы из архива (без использования имен каталогов)
            ////   l: список содержимого архива
            ////   t: Тест целостности архива
            ////   u: обновление файлов в архив
            ////   x: Извлечь файлы с полными путями
            ////<switches>
            ////   -ai [Г [- | 0]] {@ листинга | подстановочные!}: Включить архивы
            ////   -ax [Г [- | 0]] {@ листинга | подстановочные!}: исключать архивы
            ////   -bd: Отключение индикатора процент
            ////   -i [г [- | 0]] {! @ листинга | шаблон}: Включить имена
            ////   -m {Параметры}: Метод сжатии
            ////   -o {каталог}: каталог набор выходных
            ////   -p {Пароль}: установить пароль
            ////   -r [- | 0]: подкаталогов Recurse
            ////   -scs {UTF-8 | WIN | DOS}: установить кодировку для списка файлов
            ////   -sfx [{имя}]: Создать SFX-архив
            ////   -si [{имя}]: чтение данных из стандартного ввода
            ////   -slt: показать техническую информацию для л (список) команда
            ////   -so: записать данные на стандартный вывод
            ////   -ssc [-]: множество чувствительных режиме случае
            ////   -ssw: компресс общих файлов
            ////   -t {} Тип: Установить тип архива
            ////   -u [-] [# р] [д #] [# г] [х #] [# у] [г #] [newArchiveName!]: Обновление вариантов
            ////   -v {размер} [б | к | M | G]: Создание томов
            ////   -w [{путь}]: Назначение каталога работ. Пустой путь означает временный каталог
            ////   -x [г [- | 0]]] {@ листинга | подстановочные!}: исключить имена
            ////   -у: предположим, да на все вопросы
            /// </summary>



            /// <summary>
            /// Разархивирование с помощью Procces консоли 7z архиватора всего архива<para></para>
            /// 
            /// <param name="putch_ist">полный путь где находится архив</param><para></para>
            /// <param name="putarh"></param><para>куда разархивировать</para>
            /// <returns></returns>
            /// </summary>
            public static bool Unarch_arhive(string putch_ist, string putarh, bool NOCatalog = false)  
            {
                string PutchArh = SystemConecto.PutchApp + @"Utils\7za\7za.exe";

                // Проверка архиватора
                if (!Install7za())
                {
                    //нет архиватора
                    SystemConecto.ErorDebag("Отсутствует архиватор в каталоге: " + PutchArh);
                    //найти в инсталяшки разработка в новой версии
                    return false;
                }

                //Готовность устройства

                string[] aDir = putch_ist.Split('\\');
                /// Получаем первый елемент
                string aDirDevice = aDir[0];

                var DeviceInfo = DevicePC.ChekDevice(aDirDevice);
                if (Convert.ToBoolean(DeviceInfo[0]))
                {
                }
                else
                {
                    // Устройство не готово
                    SystemConecto.ErorDebag(string.Format("Устройство {0} для получения данных не готово.", aDirDevice));
                }


                // D:\ECV\EXE\7za.exe e D:\ECV\*.zip -oD:\ECV -y
                // Распоковать все

                // -y - ответить на все вопросы утвердительно да для фоновой распоковки (перезапись файлов)

                // Распоковать только один файл
                // 7za.exe e  BD0501000.7z ModelFan.ru.txt -oTmp -y
                // запускаем процесс
                var pr = new Process();

                pr.StartInfo.FileName = SystemConecto.PutchApp + @"Utils\7za\" + "7za.exe";

                pr.StartInfo.Arguments = " "+ ((NOCatalog) ? "e" : "x" ) +"  \"" + putch_ist + "\" -o\"" + putarh + "\" -y";

                pr.StartInfo.CreateNoWindow = true;
                //pr.StartInfo.WorkingDirectory =  SystemConecto.PutchApp + @"Utils\7za\"; //"\"" +
                pr.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                pr.StartInfo.UseShellExecute = false;
                pr.StartInfo.RedirectStandardOutput = true;
                // Отладка
                SystemConecto.ErorDebag(pr.StartInfo.WorkingDirectory + pr.StartInfo.FileName + pr.StartInfo.Arguments, 1);
                pr.Start();
                // получаем ответ запущенного процесса
                StreamReader srIncoming = pr.StandardOutput;
                // выводим результат
                SystemConecto.ErorDebag(srIncoming.ReadToEnd());

                pr.WaitForExit();
                pr.Close();




                return true;
            }


            /// <summary>
            /// Установка утилиты для работы с БД сервера FB
            /// </summary>
            public static bool Install7za()
            {
                // Результат проверки
                var RezChek = false;

                // Проверка установки утилит
                if (!SystemConecto.DIR_(SystemConecto.PutchApp + @"Utils\7za")) RezChek = false;

                // Список файлов
                Dictionary<string, string> fbembedList = new Dictionary<string, string>();
                fbembedList.Add(SystemConecto.PutchApp + @"Utils\7za\7za.exe", "7za/");


                if (SystemConecto.IsFilesPRG(fbembedList, -1, "- Проверка файлов во время установки файлов помощи") == "True")
                {
                    RezChek = true;




                    // Вывод любого сообщения в ощий лог
                    // SystemConecto.ErorDebag(ex.ToString());
                    // MessageBox.Show("Нет файлов");
                }
                else
                {
                    RezChek = false;


                }

                return RezChek;


            }
        }





        /// <summary>
        /// Структура данных для передачи аргументов или параметров
        /// </summary>
        public struct Metods
        {

            /// <summary>
            /// Название класса кторый содержит исполняемые методы
            /// </summary>
            public string NameClass { get; set; }
            public string Install { get; set; }
            public string UnInstal { get; set; }
            public string AdrPort { get; set; }
            public string AdminPanel { get; set; }
        }


    }

    // https://github.com/DeanReynolds/C--INI-Parser-Writer/blob/master/INI.cs

    public class INI
    {
        public string FilePath;
        public char? Delimiter;
        public OrderedDictionary Nodes;

        public Flags? SetFlags = null;
        [Flags] public enum Flags { Quoted = 1, Apostrophized = 2, NoWhitespaces = 4 }
        //

        public INI(Flags? flags = null) { SetFlags = flags; Nodes = new OrderedDictionary(); }
        public INI(string filePath, Flags? flags = null) { SetFlags = flags; FilePath = filePath; Nodes = new OrderedDictionary(); }

        public void Set(string name, string value, bool save = false) { Set(null, name, value, save); }
        public void Set(string section, string name, string value, bool save = false) { string key = ((!string.IsNullOrEmpty(section) ? (section + ".") : string.Empty) + name);
            // Добовлять комментарий нет идей
            if (name.StartsWith(";") || name.StartsWith("#"))
            {
                Nodes.Add((Nodes.Count+1)*-1, name + value);
            }
            else
            {
                if (Nodes.Contains(key))
                {
                    Nodes[key] = value;
                }
                else
                {
                    Nodes.Add(key, value);
                }
            }
            if (save)
            {
                Save();
            }
        }
        public string Get(int index)
        {
            string key = Nodes.KeyFromIndex(index).ToString();
            if (Nodes.Contains(key))
            {
                string value = Nodes[key].ToString();
                if (SetFlags.HasValue)
                {
                    if (SetFlags.Value.HasFlag(Flags.Quoted) && (value.CountOf('"') >= 2)) value = value.Between((value.IndexOf('"') + 1), value.LastIndexOf('"'));
                    else if (SetFlags.Value.HasFlag(Flags.Apostrophized) && (value.CountOf('\'') >= 2)) value = value.Between((value.IndexOf('\'') + 1), value.LastIndexOf('\''));
                    if (SetFlags.Value.HasFlag(Flags.NoWhitespaces)) value = Regex.Replace(value, @"\s+", string.Empty);
                }
                return value;
            }
            else return null;
        }
        public string Get(string section, string name) { return Get(Nodes.IndexOfKey(section + "." + name)); }
        public bool Remove(string key, bool save = false) { if (Nodes.Contains(key)) { Nodes.Remove(key); if (save) Save(); return true; } else return false; }
        public bool Remove(string section, string name, bool save = false) { string key = (section + "." + name); if (Nodes.Contains(key)) { Nodes.Remove(key); if (save) Save(); return true; } else return false; }

        public void Save(string filePath = null)
        {
            if (filePath != null) FilePath = filePath;
            using (StreamWriter writer = new StreamWriter(FilePath))
            {
                if (Delimiter.HasValue) writer.WriteLine("[ini]delimiter='" + Delimiter + "'");
                if (SetFlags.HasValue) writer.WriteLine("[ini]flags" + (!Delimiter.HasValue ? '=' : Delimiter.Value) + SetFlags.ToString());
                // Текущия секция
                string wroteSection = null; string TabGroup = " ";

                for (int i = 0; i < Nodes.Count; i++)
                {
                    string key = Nodes.KeyFromIndex(i).ToString(), section = string.Empty, name = string.Empty, value = Nodes[i].ToString();
                    if (key.Contains((char)13)) //'.'
                    {
                        // Убрать точку из названии секции
                        int indexOfSeparator = key.IndexOf('.');
                        section = key.Substring(0, indexOfSeparator);
                        name = key.Substring((indexOfSeparator + 1), (key.Length - (indexOfSeparator + 1)));
                    }
                    else {

                        // Поддержка групповой политики {}
                        if (value.StartsWith("{"))
                        {
                            TabGroup = "\t";
                        }
                        if (value.StartsWith("}"))
                        {
                            
                            TabGroup = " ";
                        }


                        // Проверка комментария
                        int numCom;
                        bool parsed = Int32.TryParse(key, out numCom);

                        if (parsed && numCom < 0)
                        {
                            if (i == (Nodes.Count - 1))
                            {
                                writer.Write(value);
                            }
                            else
                            {
                                writer.WriteLine(value);
                            }
                                
                            continue;
                        }


                        if (wroteSection != null) section = null; name = key;
                    }
                    if (!string.IsNullOrEmpty(section))
                    {
                        // Запись секции
                        if (section == "ini") { writer.Write("[ini]"); wroteSection = null; }
                        // Запись новойсекции
                        else if (wroteSection != section) { writer.WriteLine("[" + section + "]"); wroteSection = section; }
                    }
                    else if (wroteSection != null) { writer.WriteLine("{global}"); wroteSection = null; }
                    if (!string.IsNullOrEmpty(name) && !string.IsNullOrEmpty(value))
                    {
                        if (i == (Nodes.Count - 1))
                        {
                            writer.Write(name + " " + (!Delimiter.HasValue ? '=' : Delimiter.Value) + TabGroup + value);
                        }
                        else
                        {
                            writer.WriteLine(name + " " + (!Delimiter.HasValue ? '=' : Delimiter.Value) + TabGroup + value);
                        }
                    }
                }
                writer.Close();
            }
        }

        public static INI ReadFile(string file, Flags? flags = null) { if (File.Exists(file)) return ReadString(File.ReadAllText(file), file, true, flags); else return new INI(file, flags); }
        public static INI ReadString(string text, Flags? flags = null) { return ReadString(text, "temp.ini", false, flags); }
        public static INI ReadString(string text, string filePath, bool fromFile, Flags? flags = null)
        {
            if (fromFile && !File.Exists(filePath)) return new INI(filePath, flags);
            INI ini = new INI(filePath, flags);
            string[] lines = text.Split(new string[] { "\r\n", "\n" }, StringSplitOptions.None);
            char? delimiter = null;
            string section = string.Empty;
            for (int i = 0; i < lines.Length; i++)
            {
                string line = lines[i].Trim();
                // Обработка коментариев
                if (line.StartsWith(";") || line.StartsWith("#") || line.StartsWith("{") || line.StartsWith("}") || line.Length == 0)
                {
                    // -1 = ;uhuhnu
                    ini.Nodes.Add((i+1)*-1, lines[i].ToString());
                    continue;
                }
                // Обработка групповых политик (не секции значение в фигурных скобках)
               // if (line.StartsWith("{") || line.StartsWith("}"))
               // {
                    // -1 = {
                    // Обработка Валидности груповых политик - разарботка
                   // ini.Nodes.Add((i + 1) * -1, lines[i].ToString());
                    //continue;
               // }



                if (line.ToLower() == "{global}") section = string.Empty;
                else
                {
                    string newSection = Regex.Match(line, @"(?<=\[).*(?=\])").Value.Trim();
                    if (!string.IsNullOrEmpty(newSection)) section = newSection;
                }
                string name = Regex.Match(line, (@"(?<=^\p{Zs}*|])[^]" + (!delimiter.HasValue ? "=:" : INIExtentions.RegexEscape(delimiter.Value)) + "]*(?=" +
                    (!delimiter.HasValue ? "=|:" : INIExtentions.RegexEscape(delimiter.Value)) + ")")).Value.Trim();
                string value = string.Empty;
                if (ini.SetFlags.HasValue)
                {
                    value = Regex.Match(line, "(?<==|:).*").Value.Trim();
                    if (ini.SetFlags.Value.HasFlag(Flags.Quoted) && (value.CountOf('"') >= 2)) value = value.Between(value.IndexOf('"'), (value.LastIndexOf('"') + 1));
                    else if (ini.SetFlags.Value.HasFlag(Flags.Apostrophized) && (value.CountOf('\'') >= 2)) value = value.Between(value.IndexOf('\''), (value.LastIndexOf('\'') + 1));
                    else value = Regex.Match(value, "[^;#]*").Value.Trim();
                }
                else value = Regex.Match(line, "(?<=" + (!delimiter.HasValue ? "=|:" : INIExtentions.RegexEscape(delimiter.Value)) + ")[^;#]*").Value.Trim();
                if (section == "ini")
                {
                    if (!string.IsNullOrEmpty(name) && !string.IsNullOrEmpty(value))
                        if (name == "delimiter")
                        {
                            if (value.CountOf('"') >= 2) delimiter = value.Between((value.IndexOf('"') + 1), value.LastIndexOf('"'))[0];
                            if (value.CountOf('\'') >= 2) delimiter = value.Between((value.IndexOf('\'') + 1), value.LastIndexOf('\''))[0];
                            ini.Delimiter = delimiter;
                        }
                        else if (name == "flags") ini.SetFlags = (Flags)Enum.Parse(typeof(Flags), value);
                    section = string.Empty;
                }
                else if (!string.IsNullOrEmpty(name) && !string.IsNullOrEmpty(value)) ini.Nodes.Add(((!string.IsNullOrEmpty(section) ? (section + (char)13) : string.Empty) + name), value);
            }
            //Console.WriteLine("flags: " + ini.SetFlags.ToString());
            //for (int i = 0; i < ini.Nodes.Count; i++) Console.WriteLine(string.Format("{0} = {1}.", ini.Nodes.KeyFromIndex(i), ini.Get(i)));
            return ini;
        }
        public static INI ReadStream(Stream stream, Flags? flags = null) { using (StreamReader reader = new StreamReader(stream)) return ReadString(reader.ReadToEnd(), flags); }
        public static INI ReadStream(Stream stream, string file, Flags? flags = null) { using (StreamReader reader = new StreamReader(stream)) return ReadString(reader.ReadToEnd(), file, true, flags); }
    }

    public static class INIExtentions
    {
        public static int IndexOfKey(this OrderedDictionary dictionary, string key) { for (int index = 0; index < dictionary.Count; index++) if (dictionary[index] == dictionary[key]) return index; return -1; }
        public static int IndexOfValue(this OrderedDictionary dictionary, object value) { for (int index = 0; index < dictionary.Count; index++) if (dictionary[index] == value) return index; return -1; }
        public static object KeyFromIndex(this OrderedDictionary dictionary, int index) { return ((dictionary.Count > index) ? dictionary.Cast<DictionaryEntry>().ElementAt(index).Key : null); }
        public static int CountOf(this string @string, char @char) { int count = 0; for (int i = 0; i < @string.Length; i++) if (@string[i] == @char) count++; return count; }
        public static string Between(this string @string, int startIndex, int endIndex) { return @string.Substring(startIndex, (endIndex - startIndex)); }
        public static string RegexEscape(char @char) { return Regex.Escape(@char.ToString()); }
    }
}
