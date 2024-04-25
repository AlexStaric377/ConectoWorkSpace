#region импорт следующих имен пространств .NET:
 //---- объекты ОС Windows (Реестр, {Win Api} 
using Microsoft.Win32;
using System;
using System.Collections.Generic;
// Управление Изображениями
    using System.ComponentModel;
// Управление БД
    using System.Data;
// --- Process
    using System.Diagnostics;
using System.Drawing;
// Ссылка в проекте MSV2010 добовляется ...
    using System.Drawing.Text;
// Подключение GAC for Net для Fireberd
    using System.EnterpriseServices.Internal;
// локаль операционной системы
    using System.Globalization;
// Управление вводом-выводом
    using System.IO;
using System.IO.Compression;
using System.Linq;
// Удаленное управление компьютером
    // 
    using System.Management;
// Управление сетью
    using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
//--- для Проверки Сборок
    using System.Reflection;
// Импорт библиотек Windows DllImport (управление питанием ОС, ...
    using System.Runtime.InteropServices;
// шифрование данных
    using System.Security.Cryptography;
using System.Security.Permissions;
using System.Text;
/// Многопоточность
    using System.Threading;
// --- Timer
    using System.Timers;
using System.Windows;
using System.Windows.Controls;
//--- WPF
    using System.Windows.Media;
using System.Windows.Threading;
// Управление Xml
    using System.Xml;
using System.Xml.Linq;
// --- Заставка
using ConectoWorkSpace.Splasher_startWindow;

#endregion

namespace ConectoWorkSpace
{
    /// <summary>
    ///  Разделяемый класс по файлам (ключевое слово - partial)
    /// </summary>

    public partial class SystemConecto
    {
        // http://www.spugachev.com/windows8book
        // http://blogs.msdn.com/b/rudevnews/archive/2012/12/06/windows-8-c.aspx
        // professorweb.ru
        // http://metanit.com/
        // http://www.csharpcoderr.com/p/blog-page_19.html перечень примеров
        // http://kbyte.ru/ru/  перечень примеров MSSQl и прочего
        // http://kbss.ru/blog/lang_c_sharp/ - много интересного плюс сбор на терминале (для инвентаризации)



        // Короткие задачи
        // http://cyber-blog.klan-hub.ru/tag/csharp/  - Сервер
        // http://professorweb.ru/my/WPF/base_WPF/level5/5_12.php - Drag End Drop
        // http://www.harding.edu/fmccown/screensaver/screensaver.html - Хранитель Экрана Screen Saver with C#

        // Информация типы данных
        // http://msdn.microsoft.com/ru-ru/library/ms228360(v=vs.90).aspx


        // Защита приложения
        // http://habrahabr.ru/post/97062/ - обзор http://www.youtube.com/watch?v=-Gval9wYWIw
        // Guard защита - http://www.aktiv-company.ru/news/company-editorial-06-09-2011.html
        // http://www.vgrsoft.com/ru/download/ilp
        // http://www.eziriz.com/dotnet_reactor.htm


        // распараллеливанию вычислений в кластере. Так или иначе придется организовывать взаимодействие процессов между собой. 
        // Сейчас широко используется Message Passing Interface. Boost.MPI - одна из реализаций, 
        // MPI.Net http://osl.iu.edu/research/mpi.net/ - другая реализация.


        #region Глобальные параметры (переменные)

        public string VersijaPO = "a-0-5";                    // Версия ПО (a - альфа версия; первая цифра основные изменения, вторая цифра незначительые изменения)

        public object OSVer = Environment.OSVersion;          // Версия ОС 

        public static Dictionary<string, string> NetworkPC = new Dictionary<string, string>(); // Параметры сети


        public static FileStream XMLConfigFile = null; // Блокировка основного файла кофигурации

        /// <summary>
        ///  Объявить переменную глобально
        ///  public object error;
        /// </summary

        // Путь к ключу, где Windows размещает в реестре автоматичекий запуск приложений после загрузки explorer.exe для пользователей
        public static RegistryKey rkStartApp = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Run", true);
        // Путь к ключу, где Windows размещает в реестре автоматичекий запуск приложений до загрузки explorer.exe для Терминала Fornt
        public static RegistryKey rkStartAppTerminal = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\Windows NT\CurrentVersion\Winlogon", true);
        // Путь к ключу, где ConectoWorkSpace размещает в реестре свои данные
        public static RegistryKey rkAppSeting = Registry.CurrentUser.CreateSubKey(@"System\Alt-Tab\App");

        /// <summary>
        /// Время ожидания подключения сети или подключение к серверам, сервисам (после чего выводим сообщение)
        /// </summary>
        public static TimeSpan WaitNetTimeSec = new TimeSpan(0,0,35);

        public static FtpWebResponse NetFTPResponse = null;

        //private static string PutchPack = null;

        /// <summary>
        /// Режим терминала - программа включается без рабочего стола Windows
        /// </summary>
        public static bool TerminalStatus = false;

     
        /// <summary>
        /// Параметры окна пользователя при авторизации <para></para>
        /// 0 - Системное имя метода класса или метода исполнения изменения интерфейса после авторизации, <para></para>
        /// 1 - Тип авторизации: (-1 - отсутствует, 0 - логин пароль, 1 - пин(карточка), 2 - авто авторизация, 3 - авторизация с носителя),<para></para> 
        /// 2 - Ссылка на доп. Изображение справа,<para></para>
        /// 3 - Ссылка на доп. Изображение сверху, <para></para>
        /// 4 - Текст под картинкой на доп. Изображении справа <para></para>
        /// 5 - Сервер учетных данных (FB, MSSQL, LDAP, WEB server, MySql, UserConecto)<para></para>
        /// 6 - Сервер-IP<para></para>
        /// 7 - Cервер-Alias<para></para>
        /// 8 - Cервер-DopParam<para></para>
        /// 9 - Cервер-DopParam-Тип БД: B52-дополнительный индекс к логину (при смене пароля непонятная ситуация)<para></para>
        /// </summary>
        public static string[] Autirize = new string[9] { "", "", "", "", "", "", "", "", "" };


        /// <summary>
        /// Количестов высокотребовательных запросов, запрещает выход из системы
        /// </summary>
        public static int CountQueryHieght = 0;

        #endregion

        #region  Стартовая проверка необходимых компонентов в ОС для работы Приложния

        public static Dictionary<string, string> FontMain = new Dictionary<string, string>(); // Установка шрифтов
        public static int SystemNoBD = 0; // Системная БД выключенна, ограничения по работе с системой.

        #region Пример работы с шрифтами
        /// <summary>
        /// Частная колекция щрифтов для FORM
        /// </summary>
        //public static System.Drawing.Text.PrivateFontCollection fontPrivate = new System.Drawing.Text.PrivateFontCollection();

        // Добовление шрифтов в ОС Windows
        //[DllImport("gdi32", EntryPoint = "AddFontResource")]
        //public static extern int AddFontResourceA(string lpFileName);

        //Удаление шрифтов
        //[DllImport("gdi32", EntryPoint = "RemoveFontResource")]
        //public static extern int RemoveFontResourceA(string lpFileName);
        #endregion


        /// <summary>
        /// Проверка необходимых компонентов в ОС для работы Приложния
        /// </summary>
        /// <param name="Parametrs"> Проверка только одного компонента (параметра ОС), используется для уточнения</param>
        /// <returns>Возвращаем цыфровой код ответа, 0 - все в порядке</returns>

        [DllImport("Kernel32.dll", CallingConvention = CallingConvention.StdCall)]
        public static extern bool SetDllDirectory(String lpPathName);

        public static int IsCheckOS(string Parametrs = "all")
        {
            #region Подключение дополнительного каталога для dll

            // Добавить dll - путь кдиректории для DllImport и прочих функций, подключает DllWork метод Main не сработал.
            SetDllDirectory(SystemConecto.PutchApp + @"bin\dll");

            #endregion

            #region  Проверка шрифтов
            // Результат проверки
            var RezChek = 1;

            // Список шрифтов
            Dictionary<string, string> FontList = new Dictionary<string, string>(); // Установка шрифтов
            FontList.Add("Swiss921 BT", "SWZ921N.TTF");
            FontList.Add("Myriad Pro Cond", "MYRIADPRO-BOLDCOND.OTF");



            foreach (KeyValuePair<string, string> Fontdani in FontList)
            {

                if (SystemConecto.IsFontInstalled(Fontdani.Key))
                {
                    //FontList.Add("Swiss921 BT", "SWZ921N.TTF");
                    FontMain[Fontdani.Key] = Fontdani.Key;
                    SystemConecto.ErorDebag("Шрифт: " + Fontdani.Key + " подключен на устройстве.", 1);
                }
                else
                {
                    if (File_(SystemConecto.PutchApp + @"bin\" + Fontdani.Value, 5))
                    {
                        // Скачивание файлов
                        FontMain[Fontdani.Key] = "file:///" + SystemConecto.PutchApp + @"bin\#" + Fontdani.Key;
                        // Инсталлирование в системную папку (не получилось)
                        // SystemConecto.IsFilesPack("C:\\Windows\\Fonts\\" + "SWZ921N.TTF");
                        // В свою папку да.
                        // А также можно интегрировать в компиляцию, для этого нужно чтобы в лицензии было указанно 
                        // на возможность интеграции с программами
                        // Пример использования
                        // Create a new FontFamily object, using an absolute URI reference.
                        //myTextBlock.FontFamily = new FontFamily("file:///d:/MyFonts/#Pericles Light");

                        //XAML

                        //<TextBlock FontFamily="file:///d:/MyFonts/#Pericles Light">
                        //  Aegean Sea
                        //</TextBlock>

                    }
                    else
                    {
                        SystemConecto.IsFilesPack(SystemConecto.PutchApp + @"bin\" + Fontdani.Value);
                        if (File_(SystemConecto.PutchApp + @"bin\" + Fontdani.Value, 5))
                        {
                            FontMain[Fontdani.Key] = "file:///" + SystemConecto.PutchApp + @"bin\#" + Fontdani.Key;
                        }
                        else
                        {
                            RezChek = 0;
                        }

                    }
                }

            }

            #region Еще информация о шрифтах
            // Удаление пример для разработки (например удалить шрифт)
            //var result = RemoveFontResourceA(@"C:\Users\Program\Downloads\Отладка\Font\Swiss921-BT-Regular.ttf");

            //if (result > 0)
            //{
            //    //Console.WriteLine((result == 0) ? "Font was not found." : "Font removed successfully.");
            //    RezChek = 1;
            //}

            // Попытка инсталяции в ОС (не получилось)
            // var result = AddFontResourceA(SystemConecto.PutchApp + @"bin\SWZ921N.TTF");

            //-- Для Form запись в частную колекцию (не проверил но вроде работает)
            // fontPrivate.AddFontFile(SystemConecto.PutchApp + @"bin\SWZ921N.TTF");
            // Пример использования
            // label1.Font = new Font(fontPrivate.Families[0], 11);

            // Пример чаастного случая инсталляции шрифта в ОС Windows
            //execute("C:\\Windows\\Fonts\\calibri.ttf" Yes, ExeShowNormal);
            #endregion


            #endregion

            // ======================= Необходимые файлы
            // Проверка файлов Сервера БД
            Dictionary<string, string> fbembedList = new Dictionary<string, string>();
            fbembedList.Add(SystemConecto.PutchApp + @"data\fbembed.dll", "");
            fbembedList.Add(SystemConecto.PutchApp + @"data\icudt30.dll", "");
            fbembedList.Add(SystemConecto.PutchApp + @"data\ib_util.dll", "");
            fbembedList.Add(SystemConecto.PutchApp + @"data\icuin30.dll", "");
            fbembedList.Add(SystemConecto.PutchApp + @"data\icuuc30.dll", "");
            fbembedList.Add(SystemConecto.PutchApp + @"data\Microsoft.VC80.CRT.manifest", "");
            fbembedList.Add(SystemConecto.PutchApp + @"data\msvcp80.dll", "");
            fbembedList.Add(SystemConecto.PutchApp + @"data\msvcr80.dll", "");
            fbembedList.Add(SystemConecto.PutchApp + @"data\firebird.msg", "");
            fbembedList.Add(SystemConecto.PutchApp + @"bin\dll\FirebirdSql.Data.FirebirdClient.dll", "");
            // Флеш YuTube
            fbembedList.Add(SystemConecto.PutchApp + @"bin\dll\Interop.ShockwaveFlashObjects.dll", "");
            fbembedList.Add(SystemConecto.PutchApp + @"bin\dll\AxInterop.ShockwaveFlashObjects.dll", "");
            // Gif Animation
            fbembedList.Add(SystemConecto.PutchApp + @"bin\dll\WpfAnimatedGif.dll", "");



            if (SystemConecto.IsFilesPRG(fbembedList, -1, "- Проверка файлов при старте") == "True")
            {
                SystemNoBD = 1;
                RezChek = 0;

                /// Если же вы хотите использовать общую управляемую библиотеку, то специальный механизм, называемый GAC (Global Assembly Cache), используя механизмы криптографии, 
                /// позаботится об отсутствии дублирования, о том, что нужная вам библиотека — именно та и именно той версии, которую вы ждёте.
                AppDomain currentDomain = AppDomain.CurrentDomain;
                currentDomain.AssemblyResolve += new ResolveEventHandler(SystemConecto.MyResolveEventHandler);

                #region Описание и расположение файла gacutil.exe

                // Чтобы создать сборку со строгим именем, смотри предыдущий пост. Теперь перейдём к GAC. Global Assembly Cache (глобальный кэш сборок) - место, где располагаются совместно используемые сборки. Его можно найти по адресу: C:\Windows\assembly. Но этот кэш только для сборок .NET Framework 2.0 - 3.5. Для .NET Framework 4.0 GAC - C:\Windows\Microsoft.NET\assembly. Это изменение произошло в основном из-за того, чтобы приложения, написанные под CLR v2.0 не видели сборки в кэше, написанные для CLR v4.0.
                // Т.е GAC способна хранить сборки разных версий, то если они для одного и того же CLR (чтобы не "ломать" старые приложения).
                // Каталог обладает особой структурой. Имена вложенных каталогов генерируются по особому алгоритму. Ни в коем случае не стоит копировать файлы сборок в GAC вручную, вместо этого необходимо пользоваться, например, GACUtil.exe или Windows Installer (MSI). Сборку с нестрогим именем в GAC поместить нельзя!  


                // C:\Program Files\Microsoft SDKs\Windows\v7.0A\bin\gacutil.exe
                // C:\Program Files\Microsoft SDKs\Windows\v7.0A\bin\NETFX 4.0 Tools\gacutil.exe

                // Утилита Gacutil.exe позволяет просматривать и управлять содержимым глобальной сборки кеша.
                //  Находится в 2008 студии в C:\Program Files\Microsoft SDKs\Windows\v6.0A\bin.
                //  В 2005 студии в C:\Program Files\Microsoft Visual Studio 8\SDK\v2.0\Bin

                //  gacutil /l имя_сборки - отображает только сборки, что соответствуют указанному названию.

                //  Команда find /i "строка" - выполняет поиск текстовой строки в одном или нескольких файлах без учета регистра символов.

                //  Разделитель | работает как связка двух команд, а именно результат команды find передается в имя_сборки.
                //  Разделитель & используется для перечисления команд в пакетных файлах *.bat.

                //  http://msdn.microsoft.com/en-us/library/aa560649(v=bts.20).aspx

                //  gacutil.exe /if "<path to the assembly .dll file>"

                //<system.data>
                //  <DbProviderFactories>
                //  <add name="FirebirdClient Data Provider" invariant="FirebirdSql.Data.FirebirdClient" description=".Net Framework Data Provider for Firebird" type="FirebirdSql.Data.FirebirdClient.FirebirdClientFactory, FirebirdSql.Data.FirebirdClient, Version=2.7.0.0, Culture=neutral, PublicKeyToken=3750abcc3150b00c"/>
                //</system.data>

                //<configuration>
                //<configSections>
                // <section name="firebirdsql.data.firebirdclient" type="System.Data.Common.DbProviderConfigurationHandler, System.Data, Version=2.7.0.0, Culture=neutral, PublicKeyToken=3750abcc3150b00c"/>



                #endregion

                #region Расположение файла machine.config GAC cashe
                // C:\Windows\Microsoft.NET\Framework\v4.0.30319\Config\machine.config
                // C:\Windows\Microsoft.NET\Framework\v2.0.50727\CONFIG\machine.config
                #endregion

                #region ChekGACFireberd
                try
                {


                    // Проверка зборки FirebirdSql.Data.FirebirdClient.dll, допускаю, что может быть установлена стара версия
                    if (GACGet_FB()) { }

                    // Выключить сборку
                    //Publish publisher = new Publish();
                    //publisher.GacRemove(SystemConecto.PutchApp + @"bin\dll\FirebirdSql.Data.FirebirdClient.dll");

                }
                catch (Exception ex)
                {

                    ErorDebag(ex.ToString());
                    RezChek = 1;
                    return RezChek;

                }

                #endregion

            }
            else
            {
                RezChek = 1;
                return RezChek;
                // MessageBox.Show("Нет файлов");
            }
            //if (SystemConecto.OSWMI != 3)
            MessageListener.Instance.ReceiveMessage(string.Format("Выполняется: {0}", "установка баз данных"));
            
            // Проверка системной БД в каталоге зборки в папке Conecto на FTP
            Dictionary<string, string> dataList = new Dictionary<string, string>();
            dataList.Add(SystemConecto.PutchApp + @"data\system.fdb", "");
            if (SystemConecto.IsFilesPRG(dataList, -1, "- Проверка БД при старте") != "True")
            {
                SystemNoBD = 0;
                try
                {
                    var Test = SystemBD.CreateBDSystem();
                    //Проверка подключения
                }
                catch (Exception ex)
                {
                    // Ошибка
                    ErorDebag(ex.ToString());
                    RezChek = 1;
                    return RezChek;
                }
            }

            // Проверка возможности создания БД по резервной архитектуре
            DBConecto.DBopenFBConectionMemory(SystemBD.ConnnStringSystem(), "", "FbSystem");
            DBConecto.DBcloseFBConectionMemory("FbSystem");


            #region =============== Чистка папки tmp
            DIR_(PutchApp + @"tmp", 2);
            #endregion


            #region ================ Запуск авто служб и их событий

            DevicePC.LoadDeviceUSB();

            #endregion

            // ================ Отладка
            // DevicePC.ListDevicePC();


            return RezChek;
        }
        #endregion


        #region  Последующая проверка необходимых файлов и компонентов в ОС для работы Приложния

        /// <summary>
        /// Проверка наличия файла для Приложения, в случаии отсутствия установить на устройстве<para></para>
        /// <param name="PutchFIsPack"></param>
        /// <param name="CritFile"></param>
        /// <param name="MessageLog"></param>
        /// </summary>
        public static string IsFilesPRG(dynamic FileList, int CritFile = 0, string MessageLog = "")
        {
            var MessageError = "True";
            // Чтение списка
            foreach (KeyValuePair<string, string> Filedani in FileList)
            {

                // Проверка файла на отсутствие
                if (!SystemConecto.File_(Filedani.Key, 5))
                {
                    // Файл отсутствует, попытка установки файла
                    SystemConecto.IsFilesPack(Filedani.Key, CritFile);
                    // Пвторная проверка
                    if (!SystemConecto.File_(Filedani.Key, 5))
                    {
                        // ! Доработать в сообщении определение типа файла по расширению .dll - билиотека; .zip - архив ...! 
                        string TypeFileName = "";
                        MessageError = string.Format("{0} {3} {2} {1}", MessageLog, Filedani.Key, TypeFileName, 
                            returnLanguage("Отсутствует файл"));
                        if (CritFile > -1)
                        {
                            
                            SystemConecto.ErorDebag(MessageError, 2);
                            Environment.Exit(0);
                        }
                        else
                        {
                            // Записать все ошибки
                            MessageError += Environment.NewLine + MessageError;
                        }

                    }

                }
            }

            return MessageError;
        }

        #endregion

        #region Проверка файла в зборочном пакете, необходимого для работы приложения Install Pack
        /// <summary>
        /// Группа путей для поверки файла в системе
        /// </summary>
        public struct FilePuthPack
        {
            /// <summary>
            /// Путь к Файлу который копируется из Pack
            /// </summary>
            public string PutchFIsPack { get; set; }
            /// <summary>
            /// Название файла что проверяем
            /// </summary>
            public string NameFile { get; set; }

            /// <summary>
            /// Название пакета где расположен файл что проверяем
            /// </summary>
            public string NameFileGZ { get; set; }
            /// <summary>
            /// Временный путь к скаченным файлам с FTP, локального сервера, папки
            /// </summary>
            public string FulTempNameFileGZ { get; set; }

            /// <summary>
            /// Путь расположения
            /// </summary>
            public string targetPath { get; set; }

            /// <summary>
            /// Путь к пакету зборки
            /// </summary>
            public string PutchPack { get; set; }
            //    get{
            //        return PutchPack;
            //    }


            //    set
            //    {
            //        PutchPack = value;
            //    }

            //}
        }
        
        
        /// <summary>
        /// Проверка файла в сборочном пакете, необходимого для работы приложения<para></para>
        /// </summary>
        /// <param name="PutchFIsPack">Путь к Файлу который копируется из Pack</param>
        /// <param name="CritFile">Критичность файла: -1 - не критичный;<para></para>
        /// -2 - online файл простой (не отслеживается)
        /// -3 - online файл сложный, шифруется (не отслеживается)</param>
        public static bool IsFilesPack(string PutchFIsPack, int CritFile = 0)
        {

            var MemFilePuthPack = new FilePuthPack();
            MemFilePuthPack.PutchFIsPack = PutchFIsPack;

            // Извлечение файла (ов) из пакета файлов приложения
            string[] aPutchFIsPack = PutchFIsPack.Split('\\');
            /// Получаем пред-последний елемент это название файла
            int length = aPutchFIsPack.Length - 1;
            MemFilePuthPack.NameFile = aPutchFIsPack[length];

            // Вывод информации о подгружаемом файле
            MessageListener.Instance.ReceiveMessage(string.Format("{2}: {0}" + Environment.NewLine +
                "выполняется установка модуля {1}", "проверка компонентов в ОС для работы приложения",
                MemFilePuthPack.NameFile, returnLanguage("Выполняется")));

            MemFilePuthPack.NameFileGZ = MemFilePuthPack.NameFile + ".gz";

            MemFilePuthPack.FulTempNameFileGZ = PutchApp + @"tmp\" + MemFilePuthPack.NameFileGZ;

            MemFilePuthPack.targetPath = MemFilePuthPack.PutchFIsPack.Substring(0, MemFilePuthPack.PutchFIsPack.Length - MemFilePuthPack.NameFile.Length);

            // Если найден локальный пакет, мы извлекаем из него необходимый файл согласно настройкам пакета pack.xml
            MemFilePuthPack.PutchPack = null;

            string MemoryMessageComment =  "Не найден пакет программы, ни на одном из носителей и в сетях." + Environment.NewLine +
                                            "Файл: " + PutchFIsPack.ToString() + " не найден.";

            // Если пакет не найден подключаемся к центральному FTP
            if (SherchPack(MemFilePuthPack) == null)
            {
                // !!! ==================== Проверка сервера ConectoWorkSpace в сети (запрос чтения файла с сервера)

                // правло переподсоединения одного потока
                bool idBlock = true;
                int CountConect = 1; //не более 3 раз

                while(idBlock){
                    // Локальный пакет не найден нужно его взять с Интеренета
                    // проверка соединения с интернетом и с обслуживающим сервером 
                    if (ConnectionAvailable())
                    {
                        // updatework.conecto.ua Чтение паролей из настроек ПО WriterConfigUserXML (Пользователь устанавливается на стороне сервера)
                        // updatework.conecto.ua/updatework.conecto.ua/ "update_workspace" "conect1074"
                        var rezultConectionFTP = ConntecionFTP(@"conecto.ua/pack/" + MemFilePuthPack.NameFile + ".gz", aParamApp["ServerUpdateConecto_USER"], aParamApp["ServerUpdateConecto_USER-Passw"], 2, MemFilePuthPack.FulTempNameFileGZ);

                        ErorDebag((rezultConectionFTP == null ? "Файла нету " : "Файл есть ") + MemFilePuthPack.NameFile, 2);

                        if (rezultConectionFTP != null)
                        {
                            // Перемещение файла от куда  куда (файл проверен)
                            // Проверка отладка
                            //if (!File_(PutchFIsPack,5))
                            //{
                            
                            // Выполняется с незначительной асинхронностью декомпресию в другую директорию Синхронно
                            Compress.ToDecompressStrem(MemFilePuthPack.FulTempNameFileGZ, MemFilePuthPack.targetPath);
                            // Не отслеживать онлан файлы
                            if (CritFile > -2)
                            {
                                ErorDebag("Файл: " + MemFilePuthPack.NameFile + " скопирован с FTP сервера.", 3);
                            }
                            // Простое перемещение файла
                            //File.Move(PutchApp + @"tmp\" + MemFilePuthPack.NameFile, PutchFIsPack);

                            // Удаление происходит после выполнения модуля в целом в фоновом режиме DIR_ (Разработка - Доделать Укрпочта)
                            DeliteFileTmp();

                            // Проверка файла
                            if (File_(PutchFIsPack, 5)){
                                idBlock = false;
                                //return true;
                            }
                            else
                            {
                                if (CountConect>2)
                                {
                                    idBlock = false;
                                    return false;
                                }
                            }



                            //}
                            //else
                            //{
                            //    // Файл не докачен при работе с FTP


                            //}
                        }
                        else
                        {
                            if (CountConect > 2)
                            {
                                string Text = "";
                                // Исключение во время скачивания
                                switch (CritFile)
                                {
                                    case -1:
                                        // Файл не критичен
                                        Text = "Во время соединения с FTP произошло исключение." + Environment.NewLine;
                                        break;

                                    default:
                                        // Файл критичен
                                        Text = "Приложение будет завершено." + Environment.NewLine;
                                        break;
                                }
                                // Пример использования \r\n или \n Environment.NewLine
                                ErorDebag(Text +
                                       MemoryMessageComment, CritFile == -1 ? 0 : 2);
                                // Критичность к остановке программы
                                if (CritFile > -1)
                                {
                                        Environment.Exit(0);
                                }
                                else
                                {
                                        idBlock = false;
                                        return false;
                                }
                            }
                        }

                    }
                    else
                    {

                        if (CountConect > 2)
                        {
                            string Text = "Программа не может соединенится с FTP сервером!" + Environment.NewLine +
                                "Отсутствует соединение с Интернетом" + Environment.NewLine;
                            // Исключение во время скачивания

                            ErorDebag(string.Format("{0} {1}", Text, MemoryMessageComment), CritFile == -1 ? 0 : 2);
                            // Критичность к остановке программы
                            if (CritFile > -1)
                            {
                                // Вывести сообщение
                                    Environment.Exit(0);
                            }
                            else
                            {

                                    idBlock = false;
                                    return false;
                            }
                        }


                    }
                    CountConect++;
                    // конец сетевого цыкла
                }


            }
            // Закрыть окно ожидание
            //fWait.Close();
            return true;

        }

        #region Поиск инсталяционного пакета на дисках
        /// <summary>
        /// Поиск пакета инсталяции на локальном компьюторе
        /// файлы пакета упакованы gz
        /// </summary>
        /// <param name="PutchFIsPack"></param>
        /// <returns></returns>
        private static string SherchPack(FilePuthPack PutchFIsPack)
        {
            // PutchPack - кешируемый путь к пакету файлов программы (разработка)
            // Проверка кеша
            // if (PutchPack == null)

            // 1. Приложение ищит на всех дисках папку Conecto в папке Conecto\pack - обязательно наличие файла pack.xml)
            DriveInfo[] DriveList = DriveInfo.GetDrives();
            // string[] DriveList = Environment.GetLogicalDrives(); // Вариант не очень информативный
            //for (int i = 0; i < DriveList.Length; i++)
            // d.DriveType - тип устройства, проверить все портативные носители - if (drive.DriveType == DriveType.Removable)
 
            // Полный путь проверяемого файла
            string FulPuthPack_ = "";

            foreach (DriveInfo d in DriveList)
            {
                //System.Windows.Forms.MessageBox.Show(DriveList[i]);
                // Выбираем лучшую версию pack подходящию нашей версии (Исключение является обновление ПО, однако обновление осущетвляется на уровне exe файла)
                // 2. Если нашел проверяем версию файла - файл - pack.xml (файл содержащий информацию о пакете) - для какой версии ПО VersijaPO
                // Проверка готовности устройства
                if (d.IsReady == true)
                {

                    

                    // В Папке Conecto\pack разложил упакованые файлы в потоке .gz
                    // && Ищем первый подходящий пак (условие точное совпадение версий VersijaPO)
                    if (File_(d.Name + @"Conecto\pack\pack.xml", 5))
                    {
                        
                        PutchFIsPack.PutchPack = d.Name + @"Conecto\pack\";  // Путь к пакету
                        
                        // 2.1 Читаем файл и сравниваем (если надо распаковываем в папку Temp)

                        // string[] aVersijaPO = VersijaPO.Split('-'); - Получение характеристик версии

                        FulPuthPack_ = PutchFIsPack.PutchPack + PutchFIsPack.NameFileGZ;

                        // Взять из архива - Распоковать
                        // Проверка файлаы
                        if (File_(FulPuthPack_, 5))
                        {
                            //Compress.AddCompressFile(PutchPack_ + @"\" + PutchFIsPack.NameFile);
                            Compress.ToDecompressStrem(FulPuthPack_, PutchFIsPack.targetPath);

                            ErorDebag("Файл: " + PutchFIsPack.NameFile + " взят из архива, " + PutchFIsPack.PutchPack, 3);

                            // =========================== Пример (устарело)
                            // копируем в нужную папку
                            // Используйте путь класса манипулировать файлами и каталогами пути.
                            //string sourceFile = System.IO.Path.Combine(sourcePath, PutchFIsPack.NameFile);
                            //string destFile = System.IO.Path.Combine(targetPath, PutchFIsPack.NameFile);

                            //// To copy a file to another location and 
                            //// overwrite the destination file if it already exists.
                            //File.Copy(sourceFile, destFile, true);



                            // Получили верную версию результат найден, закончить проверку
                            break;
                        }
                        else
                        {
                            PutchFIsPack.PutchPack = null;
                        }

                    }

                }
            }

            return PutchFIsPack.PutchPack;

        }
        #endregion

        #endregion




        #region Логирование и вывод сообщений приложения {v 1.8}

        /// <summary>
        /// Код, защищенный таким образом от неопределённости в плане многопотокового исполнения, называется потокобезопасным. 
        /// Все потоки при записи борются за блокировку объекта
        /// </summary>
        private static Object lockerErr = new Object();


        /// <summary>
        /// Список параметров ошибок
        /// </summary>
        public class StruErrorDebag
        {
            /// <summary>
            /// Возвращает cообщение об ошибке
            /// </summary>
            public string Message { get; set; }
            /// <summary>
            /// Возвращает код HRES ошибки
            /// </summary>
            public int ErrorCode { get; set; }

            /// <summary>
            /// Возвращает код ошибки Number 
            /// </summary>
            public int ErrorNumber { get; set; }

            // ===================== Пример полной формы записи
            //private double _seconds;
            //public double Seconds
            //{
            //    get { return _seconds; }
            //    set { _seconds = value; }
            //}

        }


        /// <summary>
        /// Логирование и вывод сообщений приложения<para></para>
        /// </summary>
        /// <param name="Message">Message: Текст сообщения.</param><para></para>
        /// <param name="TypeError">TypeError: Тип сообщений: <para></para> 0 - исключение;<para></para> 1 - отладка;<para></para> 
        /// 2 - исключение с выводом сообщения на экран;<para></para> 3 - информация.</param><para></para>
        /// <param name="Image">Image: Вывод изображений в информационном окне TypeError = 2.</param><para></para>
        /// <param name="StruErrorDebag">StruErrorDebag: Сообщение ошибки в виде структуры - для табличных логов (разработка).</param><para></para>
        public static void ErorDebag(string Message, int TypeError = 0, int Image = 0, StruErrorDebag StructuraError = null)
        {
            // Не выводить сообщения при отключенной отладке
            // Откладка отключается при настройки логирования системы в администрировании
            if(!DebagApp && TypeError == 1){
                return;
            }

            Encoding win1251 = Encoding.GetEncoding("windows-1251");

            // Кнопки окаобщения (по умолчанию информирование) 
            var ImegLink = System.Windows.MessageBoxImage.Information;
            switch (Image)
            {
                case 0:
                case 1:
                    ImegLink = System.Windows.MessageBoxImage.Information;
                    break;
                case 2:
                    ImegLink = System.Windows.MessageBoxImage.Stop;
                    break;
                default:
                    ImegLink = System.Windows.MessageBoxImage.Information;
                    break;
            }

           
            if (LogFile)
            {
                // запись в лог код; время; пользователь; тип сообщения; сообщения
                string HeadLog = String.Format("Id;{0};{1};{2};", returnLanguage("Дата"), returnLanguage("Тип сообщения"), returnLanguage("Сообщение"));


                string text = Environment.NewLine ;
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

                }
                // Здесь очень опасно вставлять не опереденное время!!! Синхронизировать но как?
                DateTime dateTime = DateTime.Now;
                text = text + dateTime.ToString("dd.MM.yyyy HH:mm:ss") + "; --- ";
                switch (TypeError)
                {
                    case 0:
                    case 2:
                        text = text + returnLanguage("Ошибка") + ": ;";
                        break;
                    case 1:
                        text = text + returnLanguage("Отладка") + ": ;";
                        break;
                    case 3:
                        text = text + returnLanguage("Информация") + ": ;";
                        break;

                }
                text = text + Message;

                // Блокировка
                lock (lockerErr)
                {
                    // Проверка структуры
                    textall = (!(File.Exists(PuthSysLog)) ? HeadLog + Environment.NewLine : "") + text;
                   
                    // Отслеживание ошибок в досупе к файлу лога в многопотоковой среде 
                    try
                    {
                        using (StreamWriter FileSysLog = new StreamWriter(PuthSysLog, true, win1251))
                        {
                            FileSysLog.WriteLine(textall);
                            FileSysLog.Close();
                        }
                    }
                    catch //(Exception ex)
                    {
                        // Отследить ошибки
                        LogFile = false;
                        // 1. Запись в БД локальную или центральную
                        // Пробуем записать в локальный лог 
                        string LocalLog = AppDomain.CurrentDomain.BaseDirectory + "local_sys.log";
                        textall = (!(File.Exists(LocalLog)) ? HeadLog + Environment.NewLine : "") + text;
                       
                        try
                        {
                            using (StreamWriter FileSysLog = new StreamWriter(LocalLog, true, win1251))
                            {
                                FileSysLog.WriteLine(textall);
                                FileSysLog.Close();
                            }
                            // Лог сохранен
                            LogFile = true;
                        }
                        catch //(Exception ex)
                        {


                        }
                    }
                }
                if (TypeError == 2 || !LogFile)
                {

                    // 1. Запись в БД локальную или центральную


                    // 2. Изменить верстку окна ошибок

                    //System.Windows.Forms.MessageBox.Show(Message, "", System.Windows.Forms.MessageBoxButtons.OK, ImegLink);
                    System.Windows.MessageBox.Show(Message, "", System.Windows.MessageBoxButton.OK, ImegLink);
                }
            }
            else
            {
                // 1. Запись в БД локальную или центральную

                System.Windows.MessageBox.Show(Message, "", System.Windows.MessageBoxButton.OK, ImegLink);
                // System.Windows.Forms.MessageBox.Show(Message, "", System.Windows.Forms.MessageBoxButtons.OK, ImegLink);
                // MessageBox.Show("Данные отладки: " + IpVAr[0] + "{}" + IpVAr[1], "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }

        }

        #endregion

       

        #region Проверка записи в реестре
        /// <summary>
        /// Управление записями реестра Windows
        /// <par>0 - По умолчанию проверка системной ветки</par>
        /// <par>1 - Проверка ветки LocalMachine HKLM</par>
        /// <param name="NameKey">Дерево реестра</param>
        /// <param name="TypeChek">Тип управления:</param>
        /// <returns></returns>        
        /// </summary>
        public static bool CHEKREG_(int TypeChek = 0, string prOpenSubKey = null, string prOpenSubKey2 = null, string prOpenSubKey3 = null)
        {
            RegistryKey ChekReg = null;
            
            switch (TypeChek)
            {
                case 0:
                    // Проверка системной ветки
                    if (rkAppSeting == null)
                    {
                        // Запись Ошибки 
                        return false;
                    }
                    ChekReg = rkAppSeting;
                    break;
                case 1:
                    if (prOpenSubKey != null && prOpenSubKey2 != null && prOpenSubKey3 != null)
                    {
                        ChekReg = Registry.LocalMachine.OpenSubKey(prOpenSubKey).OpenSubKey(prOpenSubKey2);
                        if (ChekReg == null)
                        {
                            // Запись Ошибки 
                            return false;
                        }
                        RegistryKey FoundKey = ChekReg.OpenSubKey(prOpenSubKey3);
                        if (FoundKey == null)
                        {
                            // Запись Ошибки 
                            return false;
                        }
                    }
                    
                    break;
                    // Registry.CurrentUser.CreateSubKey(@"System\Alt-Tab\App");
                    //// Чтение
                    //if (rkApp.GetValue("MyApp") == null)
                    //   // Установка
                    //    rkApp.SetValue("MyApp", Application.ExecutablePath.ToString());
                    //// Удаление
                    //    rkApp.DeleteValue("MyApp", false);


            }

            if (ChekReg == null)
            {
                // Запись Ошибки 
                return false;
            }

            return true;
        }

        // Перебор ветки реестра
          //RegistryKey cu = Registry.CurrentUser;

          //  RegistryKey regKey = cu.OpenSubKey("Software");
          //  String[] names = regKey.GetSubKeyNames();

          //  Console.WriteLine("Subkeys of " + regKey.Name);
          //  Console.WriteLine("-----------------------------------------------");
          //  foreach (String s in names)
          //  {
          //      Console.WriteLine(s);
          //  }


        #endregion

        #region Вернуть ссылку на окно по запросу WPF C# {ListWindowMain}
        /// <summary>
        /// Вернуть ссылку на окно по запросу WPF C# {ListWindowMain}
        /// </summary>
        /// <param name="NameWindow"></param>
        /// <returns></returns>
        public static System.Windows.Window ListWindowMain(string NameWindow)
        {

            foreach (System.Windows.Window openWindow in System.Windows.Application.Current.Windows)
            {

                // System.Windows.MessageBox.Show(openWindow.Name.ToString(), "«Open Windows»");
                if (openWindow.Name == NameWindow)
                {
                    // Пример ссылки на объект
                    //System.Windows.Controls.Image g = (System.Windows.Controls.Image)LogicalTreeHelper.FindLogicalNode(openWindow, "ConectInternet");
                    //System.Windows.MessageBox.Show( g.Source.ToString(), "«Open Windows»");
                    
                   
                    // Пример кода
                    // ownedWindow.Hide();
                    // ownedWindow.Close();
                    return openWindow;
                }

            }
            return null;
            #region Варианты ссылок
            // -------------- Еще вариант

            //MainWindow FonWait = (MainWindow)App.Current.MainWindow;
            //foreach (System.Windows.Window ownedWindow in FonWait.OwnedWindows)
            //{
            //    if (ownedWindow.Name == NameWindow)
            //    {
            //        // Пример кода
            //        // ownedWindow.Hide();
            //        // ownedWindow.Close();
            //        return ownedWindow;
            //    }

            //}
            

            // -------------- Еще вариант
            //StringBuilder sb = new StringBuilder();

            //foreach (System.Windows.Window openWindow in System.Windows.Application.Current.Windows)
            //{

            //sb.AppendLine(openWindow.Title);

            //}

            //System.Windows.MessageBox.Show(sb.ToString(), "«Open Windows»");
            #endregion

        }
        #endregion

        #region Функции для управления директориями и файлами
        /// <summary>
        /// Проверка дириктории (Typefunc - 0 - создание , 1 - удаление , 2 - очистка от всех файлов, изменение)<para></para>
        /// Управление каталогами по заданому пути path;
        /// </summary>
        /// <param name="path"></param>
        /// <param name="Typefunc"></param>
        /// <returns></returns> 
        public static bool DIR_(string path, int Typefunc = 0)
        {
            /// 
            if (!(Directory.Exists(path)))
            {

                switch (Typefunc)
                {
                    default:
                        // Создание директории
                        try
                        {
                            Directory.CreateDirectory(path);
                        }
                        catch (Exception error)
                        {
                            
                            ErorDebag(error.Message);
                            // Дополнительная логика проверки создания директории
                            if (CheckDirectory(path))
                            {
                                return true;
                            }
                            
                            
                            return false;
                        }

                        break;
                }


            }
            else
            {
                switch (Typefunc)
                {
                    case 1:
                    case 2:
                        // Асинхронное удаление каталога
                        StructCache_FileDelete ArgumentsTh = new StructCache_FileDelete();
                        ArgumentsTh.dir = path;
                        ArgumentsTh.CreateDir = Typefunc == 2 ? true : false;

                        Thread thStartDelDir = new Thread(FileDelete_Cache);
                        thStartDelDir.SetApartmentState(ApartmentState.STA);
                        thStartDelDir.IsBackground = true; // Фоновый поток
                        thStartDelDir.Priority = ThreadPriority.Lowest;
                        thStartDelDir.Start(ArgumentsTh);

                        break;
                    //default:
                    //    // Директория есть
                    //    break;
                }
            }
            return true;
        }

        #region Создаем уникальную директорию
        /// <summary>
        ///  Создать уникальную директорию по пути<para></para>
        ///  по умолчанию: Старт приложения ... \Temp\
        ///  return - { Путь к созданной директории, имя  каталога уникальное значение }
        /// </summary>
        /// <returns></returns>
        public string[] DIR_CreateUnic(string PuthDirUnic = "")
        {

            PuthDirUnic = PuthDirUnic == "" ? SystemConecto.PStartup + @"\Temp\" : PuthDirUnic;
            string PatchTmpUnic = "";
            bool dircr = true;
            string valuenewkey = "";
            while (dircr)
            {
                Random rnd = new Random(DateTime.Now.Millisecond);

                valuenewkey = rnd.Next(10000000).ToString();

                if (!Directory.Exists(PuthDirUnic + valuenewkey)) //
                {
                    //SystemConecto.ErorDebag("========================  " + fi.Name + " / " + valuekey + "/" + valuenewkey, 1);

                    PatchTmpUnic = PuthDirUnic + valuenewkey;
                    Directory.CreateDirectory(PatchTmpUnic);
                    dircr = false;
                }

            }
            return new string[2] { PatchTmpUnic, valuenewkey };
        }
        #endregion

        /// <summary>
        /// Проверка файла (Typefunc - создание = 0, удаление, изменение, 
        /// 4 - создать но вернуть результат проверки, 5-проверка без создания, 6-блокировка для безопасности)
        /// </summary>
        /// <param name="path">Путь к файлу</param>
        /// <param name="Typefunc"></param>
        /// <param name="LinkFile"></param>
        /// <returns></returns>
        public static bool File_(string path, int Typefunc = 0)
        {
            /// Проверка файла (Typefunc - создание = 0, удаление, изменение, 
            /// 4 - создать но вернуть результат проверки, 5-проверка без создания)
            if (!(File.Exists(path)))
            {
                try
                {
                    if (Typefunc < 5)
                    {
                        using (FileStream NewFile = File.Create(path))
                        {
                            NewFile.Close();
                            if (Typefunc == 4)
                            {
                                return false;
                            }
                        }
                    }
                    else
                    {
                        return false;
                    }

                }
                catch (Exception error)
                {
                    ErorDebag(error.Message);
                    return false;
                }
            }
            
            return true;
        }


        public static bool File_Block(string path, ref FileStream LinkFile)
        {
            /// Проверка файла  6-блокировка для безопасности
            if ((File.Exists(path)))
            {
                try
                {

                    LinkFile = File.Open(path, FileMode.Open, FileAccess.Write, FileShare.None); // FileStream
                    // LinkFile = NewFile;
                    //using (FileStream NewFile = File.Open(path, FileMode.Open, FileAccess.Write, FileShare.None))
                    //{
                    // Файл блокируется внутри конструкции using 

                    // ErorDebag("Хай", 2);
                    //}
                    // LinkFile = new StreamWriter(path, true);

                }
                catch (Exception error)
                {
                    ErorDebag(error.Message);
                    return false;
                }
            }

            return true;
        }


        #region Удаление директории с файлами

        /// <summary>
        /// Структура передаваемых данных для потока удаления файлов
        /// </summary>
        public struct StructCache_FileDelete
        {
            public string dir { get; set; }
            public bool CreateDir { get; set; }

        }
        /// <summary>
        /// Асинхронное удаление с поддержкой кеша
        /// </summary>
        /// <param name="ThreadObj"></param>
        public static void FileDelete_Cache(object ThreadObj) 
        {

            SystemConecto.StructCache_FileDelete arguments = (SystemConecto.StructCache_FileDelete)ThreadObj;
            try
            {
                Thread.Sleep(2000);
                Directory.Delete(arguments.dir, true); //true - если директория не пуста (удалит и файлы и папки)
                // Создать директорию заново
                if (arguments.CreateDir)    // arguments.CreateDir != null && 
                {
                    DIR_(arguments.dir);
                }
            }
            catch (Exception error)
            {
                ErorDebag(error.Message);
            }
        }

        #endregion

        #region  Резерв и разработки private

        // Удаление файла сносителя локально асинхронно
        // try
        //{
        //    // Перенести как свойство в Compress - Так как функция асинхронна
        //    // File.Delete(PutchApp + @"tmp\" + NameFile + ".gz");
        //}
        //catch
        //{

        //}

        private void Write(string path, int Typefunc = 0)
        {
            // Резервная процедура

        }
        #endregion

        #endregion

       

        #region Шифрование файлов XML и прочих

        /// <summary>
        ///  Шифрование файлов XML и прочих
        /// </summary>
        /// <param name="FileName"></param>
        /// <param name="Data"></param>
        /// <param name="TypeCript">Резерв метод шифрования</param>
        public static void EncryptTextToFile(String FileName, String Data,  int TypeCript=0)
        {
            //if (IV == null || IV.Length <= 0)
            //   throw new ArgumentNullException("IV");

            FileStream fStream = null;
            CryptoStream cStream = null;

            try
            {
                using (fStream = new FileStream(FileName, FileMode.OpenOrCreate, FileAccess.Write))
                {

                    //System.Text.ASCIIEncoding encoding = new System.Text.ASCIIEncoding(); //Encoding.Unicode
                    ICryptoTransform desencrypt = des.CreateEncryptor();
                    cStream = new System.Security.Cryptography.CryptoStream(fStream, desencrypt, CryptoStreamMode.Write);
                    // Альтернатива Write
                    //byte[] mybytes = Encoding.UTF8.GetBytes(Data);
                    //cStream.Write(mybytes, 0, mybytes.Length);

                    //Создать StreamWriter с помощью использования CryptoStream.
                    StreamWriter sWriter_ = new StreamWriter(cStream);
                    try
                    {
                        // Запись зашифрованых данных в потоке.
                        sWriter_.Write(Data);
                    }
                    catch (Exception error)
                    {
                        SystemConecto.ErorDebag("Ошибка во время записи зашифрованных данных в файл: " + Environment.NewLine +
                            " === Файл: " + FileName + Environment.NewLine +
                            " === Message: " + error.Message.ToString() + Environment.NewLine +
                            " === Exception: " + error.ToString());
                    }
                    finally
                    {
                        //cStream.FlushFinalBlock();
                        // Закрываем потоки и закрываем файл.
                        sWriter_.Close();
                        cStream.Close();
                        fStream.Close();
                    }


                }
             
            }   
            catch (Exception error)
            {
                SystemConecto.ErorDebag("Ошибка во время зашифровки: " + Environment.NewLine +
               " === Файл: " + FileName + Environment.NewLine +
               " === Message: " + error.Message.ToString() + Environment.NewLine +
               " === Exception: " + error.ToString());
            }
            finally
            {

                // Close the streams and
                // close the file.
                if (cStream!=null)
                    cStream.Close();
                if (fStream!=null)
                    fStream.Close();
            }
        }

         /// <summary>
        ///  Шифрование файлов XML и прочих
        /// </summary>
        /// <param name="FileName"></param>
        /// <param name="TypeCript">Резерв метод шифрования</param>
        public static string DecryptTextToFile(String FileName, int TypeCript = 0)
        {

            // SystemConecto.ErorDebag("----------- Расшифровал:", 3);
            //if (IV == null || IV.Length <= 0)
            //   throw new ArgumentNullException("IV");
            string decryptedtext = "";

            FileStream fsread = null;
            CryptoStream cStream = null;

            try
            {
                using (fsread = new FileStream(FileName, FileMode.Open, FileAccess.ReadWrite))
                {

                    // Чтение зашифрованных данных в string
                    //byte[] encbytes = new byte[fsread.Length];
                    //fsread.Read(encbytes, 0, encbytes.Length);
                    //string ValueEncript = Encoding.Unicode.GetString(encbytes);
                    //fsread.Position = 0;


                    //System.Text.ASCIIEncoding encoding = new System.Text.ASCIIEncoding();

                    ICryptoTransform desdecrypt = des.CreateDecryptor();
                    cStream = new System.Security.Cryptography.CryptoStream(fsread, desdecrypt, CryptoStreamMode.Read);
                    //byte[] decrByte = new byte[fsread.Length];

                    //cStream.Read(decrByte, 0, (int)fsread.Length);
                    //decryptedtext = Encoding.UTF8.GetString(decrByte);
                   // fsread.Position = 0;

                    StreamReader sReader = new StreamReader(cStream);

                    try
                    {   
                        // Чтение только до первого символа начало строки
                        //decryptedtext = sReader.ReadLine();
                        decryptedtext = sReader.ReadToEnd();
                       // File.WriteAllText(SystemConecto.PutchApp + "decript.xml", decryptedtext, Encoding.Unicode);

                    }
                    catch (Exception error)
                    {
                        SystemConecto.ErorDebag("Ошибка во время чтения из файла: " + Environment.NewLine +
                       " === Файл: " + FileName + Environment.NewLine +
                       " === Message: " + error.Message.ToString() + Environment.NewLine +
                       " === Exception: " + error.ToString());
                    }
                    finally
                    {

                         //Close the streams and
                         //close the file.
                        sReader.Close();
                        cStream.Close();
                        fsread.Close();
                    }

                }
             
            }   
            catch (Exception error)
            {
                SystemConecto.ErorDebag("Ошибка во время расшифровки: " + Environment.NewLine +
               " === Файл: " + FileName + Environment.NewLine +
               " === Message: " + error.Message.ToString() + Environment.NewLine +
               " === Exception: " + error.ToString());
            }
            finally
            {

                // Close the streams and
                // close the file.
                if (cStream != null)
                    cStream.Close();
                if (fsread != null)
                    fsread.Close();

            }
            return decryptedtext;
        }
       



        /// <summary>
        /// Включение типа шифрования
        /// </summary>
        /// <param name="ind"></param>
        public static void SetProvider(int ind = 6)
        {

            switch (ind)
            {
                case 1:
                    des = new DESCryptoServiceProvider();
                    break;
                case 2:
                    des = new RC2CryptoServiceProvider();
                    break;
                case 3:
                    des = new RijndaelManaged();
                    break;
                case 4:
                    des = new TripleDESCryptoServiceProvider();
                    break;
                case 5:
                    des = SymmetricAlgorithm.Create();
                    break;
                case 6:
                    des = Rijndael.Create();
                    break;
                    
            }

            //Rfc2898DeriveBytes passwordKey = new Rfc2898DeriveBytes(SystemConfig.sData, Encoding.ASCII.GetBytes("File Encryptor Crypto IV"));

            //des.Key = passwordKey.GetBytes(des.KeySize / 8);
            //des.IV = passwordKey.GetBytes(des.BlockSize / 8);

            des.Padding = PaddingMode.Zeros;

            byte[] Key = { 0x01, 0x14, 0x03, 0x16, 0x08, 0x06, 0x07, 0x05, 0x09, 0x10, 0x11, 0x12, 0x13, 0x02, 0x15, 0x04 };
            byte[] IV = { 0x14, 0x02, 0x09, 0x04, 0x05, 0x06, 0x07, 0x08, 0x03, 0x10, 0x11, 0x01, 0x13, 0x12, 0x15, 0x16 };

            des.Key = Key;
            des.IV = IV;

        }


        #region Разработка шифрования

        // Имя контейнера ключей для
        // Частный / открытый ключ пары значений.
        //const string NameKey = "Key01";

        //public static void EncryptFile(string inFile, String FileName)
        //{
            

        //    RSACryptoServiceProvider rsa = new RSACryptoServiceProvider(cspp);
        //    rsa.PersistKeyInCsp = true;

        //    // Создаем экземпляр для Rijndael
        //    // Асимметричного шифрования данных.
        //    RijndaelManaged rjndl = new RijndaelManaged();
        //    rjndl.KeySize = 256;
        //    rjndl.BlockSize = 256;
        //    rjndl.Mode = CipherMode.CBC;
        //    ICryptoTransform transform = rjndl.CreateEncryptor();

        //    // Используем для RSACryptoServiceProvider
        //    // Enrypt Rijndael ключ.
        //    // RSA предварительно экземпляр:
        //    //    rsa = new RSACryptoServiceProvider(cspp);
        //    byte[] keyEncrypted = rsa.Encrypt(rjndl.Key, false);

        //    // Создаем массивы байтов, чтобы содержать
        //    // Длина значений ключа и IV.
        //    byte[] LenK = new byte[4];
        //    byte[] LenIV = new byte[4];

        //    int lKey = keyEncrypted.Length;
        //    LenK = BitConverter.GetBytes(lKey);
        //    int lIV = rjndl.IV.Length;
        //    LenIV = BitConverter.GetBytes(lIV);

        //   // Записать следующее FileStream
        //     // Для зашифрованного файла (outFs):
        //     // - Длина ключа
        //     // - Длина IV
        //     // - Ecrypted ключ
        //     // - IV
        //     // - Зашифрованный контент шифр

        //    int startFileName = inFile.LastIndexOf("\\") + 1;

        //    using (FileStream outFs = new FileStream(FileName, FileMode.Create))
        //    {

        //        outFs.Write(LenK, 0, 4);
        //        outFs.Write(LenIV, 0, 4);
        //        outFs.Write(keyEncrypted, 0, lKey);
        //        outFs.Write(rjndl.IV, 0, lIV);

        //        // Теперь написать текст с помощью шифра
        //        // CryptoStream для шифрования.
        //        using (CryptoStream outStreamEncrypted = new CryptoStream(outFs, transform, CryptoStreamMode.Write))
        //        {

        //            // По шифрования блок памяти и по
        //            // Время, вы можете сохранить память
        //            // И размещения больших файлов.
        //            int count = 0;
        //            int offset = 0;

        //            // blockSizeBytes может быть любого произвольного размера.
        //            int blockSizeBytes = rjndl.BlockSize / 8;
        //            byte[] data = new byte[blockSizeBytes];
        //            int bytesRead = 0;

        //            using (FileStream inFs = new FileStream(inFile, FileMode.Open))
        //            {
        //                do
        //                {
        //                    count = inFs.Read(data, 0, blockSizeBytes);
        //                    offset += count;
        //                    outStreamEncrypted.Write(data, 0, count);
        //                    bytesRead += blockSizeBytes;
        //                }
        //                while (count > 0);
        //                inFs.Close();
        //            }
        //            outStreamEncrypted.FlushFinalBlock();
        //            outStreamEncrypted.Close();
        //        }
        //        outFs.Close();
        //    }

        //}



        //public static void EncryptTextToFile_test(String Data, String FileName, byte[] Key, byte[] IV)
        //{
        //    try
        //    {
        //        // Create or open the specified file.
        //        using (FileStream fStream = File.Open(FileName, FileMode.OpenOrCreate, FileAccess.Write))
        //        {

        //            // Create a new Rijndael object.
        //            Rijndael RijndaelAlg = Rijndael.Create();

        //            // Create a CryptoStream using the FileStream 
        //            // and the passed key and initialization vector (IV).
        //            CryptoStream cStream = new CryptoStream(fStream,
        //                RijndaelAlg.CreateEncryptor(Key, IV),
        //                CryptoStreamMode.Write);

        //            // Create a StreamWriter using the CryptoStream.
        //            StreamWriter sWriter = new StreamWriter(cStream);

        //            try
        //            {
        //                // Write the data to the stream 
        //                // to encrypt it.
        //                sWriter.WriteLine(Data);
        //            }
        //            catch (Exception e)
        //            {
        //                Console.WriteLine("An error occurred: {0}", e.Message);
        //            }
        //            finally
        //            {
        //                // Close the streams and
        //                // close the file.
        //                sWriter.Close();
        //                cStream.Close();
        //                fStream.Close();
        //            }
        //        }
        //    }
        //    catch (CryptographicException e)
        //    {
        //        Console.WriteLine("A Cryptographic error occurred: {0}", e.Message);
        //    }
        //    catch (UnauthorizedAccessException e)
        //    {
        //        Console.WriteLine("A file error occurred: {0}", e.Message);
        //    }

        //}


        //public static void EncryptTextToFile_test(String Data, String FileName, byte[] Key, byte[] IV)
        //{
        //    try
        //    {
        //        // Create or open the specified file.
        //        using (FileStream fStream = File.Open(FileName, FileMode.OpenOrCreate, FileAccess.Write))
        //        {

        //            // Create a new Rijndael object.
        //            Rijndael RijndaelAlg = Rijndael.Create();

        //            // Create a CryptoStream using the FileStream 
        //            // and the passed key and initialization vector (IV).
        //            CryptoStream cStream = new CryptoStream(fStream,
        //                RijndaelAlg.CreateEncryptor(Key, IV),
        //                CryptoStreamMode.Write);

        //            // Create a StreamWriter using the CryptoStream.
        //            StreamWriter sWriter = new StreamWriter(cStream);

        //            try
        //            {
        //                // Write the data to the stream 
        //                // to encrypt it.
        //                sWriter.WriteLine(Data);
        //            }
        //            catch (Exception e)
        //            {
        //                Console.WriteLine("An error occurred: {0}", e.Message);
        //            }
        //            finally
        //            {
        //                // Close the streams and
        //                // close the file.
        //                sWriter.Close();
        //                cStream.Close();
        //                fStream.Close();
        //            }
        //        }
        //    }
        //    catch (CryptographicException e)
        //    {
        //        Console.WriteLine("A Cryptographic error occurred: {0}", e.Message);
        //    }
        //    catch (UnauthorizedAccessException e)
        //    {
        //        Console.WriteLine("A file error occurred: {0}", e.Message);
        //    }

        //}

        //public static string DecryptTextFromFile(String FileName, byte[] Key, byte[] IV)
        //{
        //    try
        //    {
        //        // Create or open the specified file. 
        //        FileStream fStream = File.Open(FileName, FileMode.OpenOrCreate);

        //        // Create a new Rijndael object.
        //        Rijndael RijndaelAlg = Rijndael.Create();

        //        // Create a CryptoStream using the FileStream 
        //        // and the passed key and initialization vector (IV).
        //        CryptoStream cStream = new CryptoStream(fStream,
        //            RijndaelAlg.CreateDecryptor(Key, IV),
        //            CryptoStreamMode.Read);

        //        // Create a StreamReader using the CryptoStream.
        //        StreamReader sReader = new StreamReader(cStream);

        //        string val = null;

        //        try
        //        {
        //            // Read the data from the stream 
        //            // to decrypt it.
        //            //val = sReader.ReadLine();
        //            string val_ = sReader.ReadToEnd();
        //            MessageBox.Show(val_);
        //            //File.WriteAllText(SystemConecto.PutchApp + "textDecrypr.txt", val);

        //        }
        //        catch (Exception error)
        //        {
        //            SystemConecto.ErorDebag("Ошибка во время расшифровки: " + Environment.NewLine +
        //           " === Файл: " + FileName + Environment.NewLine +
        //           " === Message: " + error.Message.ToString() + Environment.NewLine +
        //           " === Exception: " + error.ToString());
        //            // Console.WriteLine("An error occurred: {0}", e.Message);
        //        }
        //        finally
        //        {

        //            // Close the streams and
        //            // close the file.
        //            sReader.Close();
        //            cStream.Close();
        //            fStream.Close();
        //        }

        //        // Return the string. 
        //        return val;
        //    }
        //    catch (CryptographicException e)
        //    {
        //        Console.WriteLine("A Cryptographic error occurred: {0}", e.Message);
        //        return null;
        //    }
        //    catch (UnauthorizedAccessException e)
        //    {
        //        Console.WriteLine("A file error occurred: {0}", e.Message);
        //        return null;
        //    }
        //}


        ///// <summary>
        /////  Шифрование файлов XML и прочих
        ///// </summary>
        ///// <param name="Data"></param>
        ///// <param name="FileName"></param>
        //public static void EncryptTextToFile_(String Data, String FileName, string Key,  byte[] Key_ , byte[] IV_, string IV = "")
        //{

        //    if (Key == null || Key.Length <= 0)
        //       throw new ArgumentNullException("Key");
        //    //if (IV == null || IV.Length <= 0)
        //    //   throw new ArgumentNullException("IV");

        //    // Создать новый объект Rijndael для генерации ключа и вектором инициализации (IV).

        //    // Пример для открытого потока (файла, эксперимент - результат разные типы WriterConfigXML это XML и IO.Stream)
        //    // Rijndael RijndaelAlg = Rijndael.Create();
        //    // RijndaelAlg.Key, RijndaelAlg.IV, - byte[] Key, byte[] IV,

        //    FileStream fStream = null;
        //    StreamWriter sWriter = null;
        //    CryptoStream cStream = null;

        //    try
        //    {
        //        // Шифрование
        //        System.Text.ASCIIEncoding encoding = new System.Text.ASCIIEncoding();
        //        // Создать новый объект Rijndael для генерации ключа и вектором инициализации (IV).
        //        Rijndael RijndaelAlg_IV = Rijndael.Create();

        //        RijndaelAlg_IV.Key = encoding.GetBytes(Key);

        //        if (IV == null || IV.Length <= 0)
        //        {
        //            // Генерация для авто потоков (разработка)
        //            RijndaelAlg_IV.GenerateIV();
        //        }
        //        else
        //        {
        //            // Initialize the IV explicitly to something random
        //            RijndaelAlg_IV.IV = encoding.GetBytes(IV);
        //        }



        //        // Создайте или откройте указанный файл
        //        FileStream fStream_ = File.Open(FileName, FileMode.OpenOrCreate);

        //        // Шифрование
        //        // Создать новый объект Rijndael для генерации ключа и вектором инициализации (IV).
        //        Rijndael RijndaelAlg__ = Rijndael.Create();

        //        // Создать CryptoStream с помощью FileStream и переданого ключа и вектора инициализации (IV).
        //        CryptoStream cStream_ = new CryptoStream(fStream,
        //            RijndaelAlg__.CreateEncryptor(Key_, IV_),
        //            CryptoStreamMode.Write);

        //        // Создать StreamWriter с помощью использования CryptoStream.
        //        StreamWriter sWriter_ = new StreamWriter(cStream);

        //        try
        //        {
        //            // Запись зашифрованых данных в потоке.
        //            sWriter_.WriteLine();
        //        }
        //        catch (Exception error)
        //        {
        //            ErorDebag(error.Message);
        //        }
        //        finally
        //        {
        //            // Закрываем потоки и закрываем файл.
        //            sWriter_.Close();
        //            cStream_.Close();
        //            fStream_.Close();
        //        }


        //        //// Create the streams used for encryption.
        //        //// Шифровка в памяти
        //        ////using (MemoryStream msEncrypt = new MemoryStream())


        //        //// Создайте или откройте указанный файл
        //        //using (fStream = File.Open(FileName, FileMode.OpenOrCreate, FileAccess.Write))
        //        //{

        //        //    // Стандартный пример
        //        //    //cStream = new CryptoStream(fStream,
        //        //    //    RijndaelAlg.CreateEncryptor(RijndaelAlg.Key, RijndaelAlg.IV),
        //        //    //    CryptoStreamMode.Write);

        //        //    // Create a CryptoStream using the FileStream 
        //        //    // and the passed key and initialization vector (IV).
        //        //    // Создать CryptoStream с помощью FileStream и переданого ключа и вектора инициализации (IV).
        //        //    using (cStream = new CryptoStream(fStream, RijndaelAlg_IV.CreateEncryptor(RijndaelAlg_.Key, RijndaelAlg_.IV), CryptoStreamMode.Write))
        //        //    {
        //        //        // Create a StreamWriter using the CryptoStream
        //        //        // Создать StreamWriter с помощью использования CryptoStream.
        //        //        using (sWriter = new StreamWriter(cStream))
        //        //        {
        //        //            // Write the data to the stream 
        //        //            // to encrypt it
        //        //            // Запись зашифрованых данных в потоке.
        //        //            sWriter.WriteLine();
        //        //            //sWriter.WriteLine(Data);
        //        //            //encrypted = msEncrypt.ToArray();
        //        //        }
        //        //    }
        //        //}
        //        // Генерация для авто потоков (разработка)
        //        if (IV == null || IV.Length <= 0)
        //        {
        //            File.WriteAllBytes(SystemConecto.PutchApp + "idIV", RijndaelAlg_IV.IV);
        //        }


        //    }
        //    catch (CryptographicException error)
        //    {
        //        ErorDebag("Криптографическая ошибка во время Шифрование файлов" + error.Message);
        //    }
        //    catch (UnauthorizedAccessException error)
        //    {
        //        ErorDebag("При записи файла произошла ошибка: {0}" + error.Message);
        //    }
        //    catch (Exception error)
        //    {
        //        ErorDebag(error.Message);
        //    }
        //    finally
        //    {
        //        // Закрываем потоки и закрываем файл.
        //        if (sWriter != null)
        //        {
        //            sWriter.Close();
        //        }
        //        if (cStream != null)
        //        {
        //            cStream.Close();
        //        }
        //        if (fStream != null)
        //        {
        //            fStream.Close();
        //        }
        //    }


        //}
        ///// <summary>
        ///// Расшифровка файла
        ///// </summary>
        ///// <param name="FileName"></param>
        ///// <param name="Key"></param>
        ///// <param name="IV"></param>
        ///// <returns></returns>
        //public static string DecryptTextFromFile_(String FileName, string Key, string IV)
        //{

        //    if (Key == null || Key.Length <= 0)
        //        throw new ArgumentNullException("Key");
        //    if (IV == null || IV.Length <= 0)
        //        throw new ArgumentNullException("IV");

        //    // Создать новый объект Rijndael для генерации ключа и вектором инициализации (IV).
        //    string val = "";
        //    FileStream fStream = null;
        //    StreamReader sReader = null;
        //    CryptoStream cStream = null;

        //    try
        //    {
        //        System.Text.ASCIIEncoding encoding = new System.Text.ASCIIEncoding();

        //        // Создать новый объект Rijndael для генерации ключа и вектором инициализации (IV).
        //        Rijndael RijndaelAlg = Rijndael.Create();

        //        //string Key = "qwertyuiopasdfghjklzxcvbnmqwerty";
        //        RijndaelAlg.Key = encoding.GetBytes(Key);


        //        // Initialize the IV explicitly to something random
        //        RijndaelAlg.IV = encoding.GetBytes(IV);
        //        // Генерация для авто потоков (разработка)
        //        //// Initialize the IV explicitly to something random
        //        //RijndaelAlg.IV = File.ReadAllBytes(SystemConecto.PutchApp + "idIV");


        //        // Create the streams used for decryption.
        //        // расщифровка в памяти
        //        //using (MemoryStream msDecrypt = new MemoryStream(cipherText))


        //        // открыть и прочитать указанный файл. 
        //        using (fStream = File.Open(FileName, FileMode.Open, FileAccess.Read))
        //        {

        //            // Создать CryptoStream с помощью FileStream и переданого ключа и вектора инициализации (IV).
        //            using (cStream = new CryptoStream(fStream, RijndaelAlg.CreateDecryptor(), CryptoStreamMode.Read))
        //            // Стандартный вариант
        //            //cStream = new CryptoStream(fStream,
        //            //    RijndaelAlg.CreateDecryptor(RijndaelAlg.Key, RijndaelAlg.IV),
        //            //    CryptoStreamMode.Read);

        //            // Создать StreamReader с помощью использования CryptoStream. 
        //            using (sReader = new StreamReader(cStream))
        //            {

        //                // Чтение данных из потока для расшифровки.
        //                val = sReader.ReadToEnd();


        //                //val = sReader.ReadLine();
        //            }
        //        }

        //    }
        //    catch (CryptographicException error)
        //    {
        //        SystemConecto.ErorDebag("Криптографическая ошибка: " + Environment.NewLine +
        //            " === Файл: " + FileName + Environment.NewLine +
        //            " === Message: " + error.Message.ToString() + Environment.NewLine +
        //            " === Exception: " + error.ToString());
        //        return null;
        //    }
        //    catch (UnauthorizedAccessException error)
        //    {
        //        SystemConecto.ErorDebag("При чтении файла произошла ошибка: " + Environment.NewLine +
        //            " === Файл: " + FileName + Environment.NewLine +
        //            " === Message: " + error.Message.ToString() + Environment.NewLine +
        //            " === Exception: " + error.ToString());
        //        return null;
        //    }
        //    catch (Exception error)
        //    {
        //        SystemConecto.ErorDebag("При чтении файла произошла ошибка: " + Environment.NewLine +
        //           " === Файл: " + FileName + Environment.NewLine +
        //           " === Message: " + error.Message.ToString() + Environment.NewLine +
        //           " === Exception: " + error.ToString());
        //    }
        //    finally
        //    {

        //        // Close the streams and
        //        // close the file.
        //        if (sReader != null)
        //        {
        //            sReader.Close();
        //        }
        //        if (cStream != null)
        //        {
        //            cStream.Close();
        //        }
        //        if (fStream != null)
        //        {
        //            fStream.Close();
        //        }
        //    }
        //    // Возвращает строку. 
        //    return val;
        //}



        #endregion



        #endregion

        #region Создание соедиения с подключаемыми БД
        public static bool ConectionDefaultSql(string TypeBD = "SqlDef")
        {
            //// Создание строки доступа
            //ConnectionStringBD StringCon = new ConnectionStringBD();
            //// Чтение параметров программы
            //// Data Source=(local)\SQLEXPRESS;Initial Catalog=AutoLot;Integrated Security=SSPI;Pooling=False
            //StringCon.DataSource_o = @"PROGRAM-PC\SQLCONECTOEXP";
            //StringCon.InitialCatalog_o = "ATOtest";
            //if (DBConecto.DBopen(StringCon.ConnectionString()))
            //{
            //    return true;
            //}
            return false;
        }
        #endregion


        #region Управление ролями Рабочего места
        public static string StartRole = null;

        /// <summary>
        /// Найти Роль Устройства
        /// </summary>
        /// <param name="NameRole"></param>
        /// <param name="FindId"></param>
        /// <param name="FuncRolePC"></param>
        /// <returns></returns>
        public static string RolePC(string NameRole = "Server", string FuncRolePC = "", string FindId=null)
        {
            // Роль = Нуль выводит просто окно Рабочего стола
            string Rezult = null;
            string[] RolePC_Servers = null;
            string[] RolePC_Terminal = null;
            string[] RolePC_Office = null;
            string[] RolePC_Portable = null;
            
            switch(NameRole){

                case "StartRole":
                    // Определяем стартовую роль. Роль которая является приоритетной

                    // Планшет (Windows)
                    if(RolePC("Portable", "RoleYes") == "True"){
                        StartRole = "Portable";
                    }else{
                        // Терминал
                        if (RolePC("Terminal", "RoleYes") == "True")
                        {
                            StartRole = "Terminal";
                        }else{
                            // Office
                            if (RolePC("Office", "RoleYes") == "True")
                            {
                                StartRole = "Office";
                            }else{
                                // Server
                                if (RolePC("Server", "RoleYes") == "True")
                                {
                                    StartRole = "Server";
                                } 
                            } 
                        } 
                    }

                 
                    return StartRole;

                case "Portable":
                    // Список активных планшетных рабочих мест (коды, через точку с запятой)
                    RolePC_Portable = aParamApp["RolePC_Portable"].Split(';');
                    for (int i = 0; i < RolePC_Portable.Length; i++)
                    {
                        if (RolePC_Portable[i] == FindId)
                        {
                            return "True";

                        }
                    }
                    break;
                case "Terminal":
                    // Список активных терминалов (ресторан и прочие) (коды, через точку с запятой)
                    RolePC_Terminal = aParamApp["RolePC_Terminal"].Split(';');
                    for (int i = 0; i < RolePC_Terminal.Length; i++)
                    {
                        if (RolePC_Terminal[i] == FindId)
                        {
                            return "True";

                        }
                    }
                    break;
                case "Office":
                    // Список активных офисных рабочих мест (коды, через точку с запятой)
                    RolePC_Office = aParamApp["RolePC_Office"].Split(';');
                    for (int i = 0; i < RolePC_Office.Length; i++)
                    {
                        if (RolePC_Office[i] == FindId)
                        {
                            return "True";

                        }
                    }
                    break;

                case "Server":
                    // Список активных серверов (коды, через точку с запятой)
                    RolePC_Servers = aParamApp["RolePC_Servers"].Split(';');
                    for (int i = 0; i < RolePC_Servers.Length; i++)
                    {
                        if (RolePC_Servers[i] == FindId)
                        {
                            return "True";

                        }
                        // Роль есть
                        if (FuncRolePC == "RoleYes")
                        {
                            return "True";
                        }
                    }
                    break;

            }

            // Роли отсутствуют
            return Rezult;
        }
        #endregion

        #region Управление сервисными окнами WindowForm

        // Открыть поток окна на передний план.
        [DllImport("user32.dll", SetLastError = true)]
        public static extern IntPtr SetForegroundWindow(IntPtr hWnd);
        /// <summary>
        /// Установка фокуса окна 
        /// string NameWinndow - /Handle/ обращение к окну с помощью Хандл процесса. 
        /// </summary>
        /// <param name="NameWinndow"></param>
        public static bool SetFocusWindow(string NameWinndow, int Id_ = 0, string MainWindowTitle = "")
        {

            
            if (NameWinndow == "HandleID")
            {
                Process[] allProc_Id = Process.GetProcesses();

                foreach (Process p in allProc_Id)
                {
                    // Отладка
                    //SystemConecto.ErorDebag(string.Format("Имя окна {0}, Hwnd - {1}", p.ProcessName, p.MainWindowHandle.ToString()), 2);
                    
                    if (p.Id == Id_)
                    {
                        // встречается нулевой Хендел (даже после обновления кеша p.refrech) для процессов без интерфейса Окна ОС
                        IntPtr Hwnd = p.MainWindowHandle;
                        if (Hwnd == IntPtr.Zero)
                        {
                            // Процессов с таким именем может быть много как фоновых задач так и интерфейсов. Фоновые задачи исключаем.
                            Process[] allProc_Name = Process.GetProcessesByName(p.ProcessName).Where(process => process.MainWindowHandle != IntPtr.Zero).ToArray();


                            if (allProc_Name != null && allProc_Name.Length >= 1)
                            {

                                foreach (Process findPr in allProc_Name)
                                {
                                    // Отладка
                                    // SystemConecto.ErorDebag(string.Format("Имя окна {0}, Время - {1}, ID - {2}", findPr.ProcessName, findPr.StartTime, findPr.HandleCount), 2);
                                    
                                    // Первое найденое (не очень прикольно)
                                    // Процессы могут отличатся заголовками findPr.MainWindowTitle тут обычно указываются заголовки файлов или их названия pdf. doc. xls.
                                    Hwnd = findPr.MainWindowHandle;
                                    break;
                                    //if (findPr.MainWindowHandle != IntPtr.Zero)
                                    //{
                                    //    Hwnd = findPr.MainWindowHandle;
                                    //    break;
                                    //}
                                    
                                }
        
                            }

                            // Первое найденое (не очень прикольно, не учитывает все окна)
                            // Hwnd = ProcessConecto.FindWindow(p.ProcessName, null);
                            if (Hwnd == null || Hwnd == IntPtr.Zero)
                            {
                                return false;
                            }

                        }

                        TextPasteWindow.ShowWindow(Hwnd, 9);
                        SetForegroundWindow(Hwnd);
                        return true;
                    }
                }


            }
            
            Process[] allProc = Process.GetProcesses();
           // foreach (Process currProc in allProc)
            //{
               // ErorDebag(currProc.ProcessName);
            //    //ErorDebag(currProc.ProcessName, 2, 2);
            //}

            //Process.Start(@"D:\B52\B52BackOffice.exe");
            // Process[] allProc_ = Process.GetProcessesByName("B52BackOffice");
            Process[] allProc_ = Process.GetProcessesByName(NameWinndow).Where(process => process.MainWindowHandle != IntPtr.Zero).ToArray();
            //Process.GetCurrentProcess

            if (allProc_ != null && allProc_.Length >= 1)
            {
                IntPtr pFoundWindow = allProc_[0].MainWindowHandle; //получаем хендл то же что FindWindow("Notepad", null);
                TextPasteWindow.ShowWindow(pFoundWindow, 9);
                SetForegroundWindow(pFoundWindow);
                return true;

            }
            return false;
        }


        /// <summary>
        /// Вывод окна белого фона
        /// тестовый метод
        /// </summary>
        public void WaitStart(string NameWindow = "Default")
        {
            //if (System.Windows.Forms.Application.OpenForms["WaitFon"] == null)
            //{
            //    WaitFon WaitFonWindow = new WaitFon(NameWindow); // создаем
            //    System.Windows.Forms.Application.OpenForms["ConectoWorkSpace"].AddOwnedForm(WaitFonWindow); 
            //    //MessageBox.Show(AdminWindow.Name);
            //    WaitFonWindow.Show();   
            //}
            //else
            //{
            //    // запись значения
            //    // System.Windows.Forms.Application.OpenForms["WaitFon"].nam
            //    System.Windows.Forms.Application.OpenForms["WaitFon"].Visible = true;
            //    // Запуск таймера
            //    // System.Windows.Forms.Application.OpenForms["WaitFon"].Controls["StartWindow"].Enabled = true;
            //}

        }

        #endregion

        #region Проверка или установка системных шрифтов (Regular)
        public static bool IsFontInstalled(string NameFont="Arial")
        {
            //get
            //{
                bool result;
                using (InstalledFontCollection installedFontCollection = new InstalledFontCollection())
                {
                    // System.Windows.Media.FontFamily
                    System.Drawing.FontFamily[] fontFamilies = installedFontCollection.Families;
                    System.Drawing.FontFamily ff = fontFamilies.FirstOrDefault(f => f.Name == NameFont);
                    result = ff != null; //&& ff.IsStyleAvailable(FontStyle.Regular)
                }
                return result;
            //}
        }

        #endregion


        #region Проверка регистрации библиотеки в сбороке GAC
        public static bool GACGet_FB()
        {
            // Assembly assem = Assembly.GetExecutingAssembly();

            //ErorDebag(string.Format("Full Name:{0}", assem.FullName));
            try
            {

                Type t = typeof(FirebirdSql.Data.FirebirdClient.FbConnectionStringBuilder);
                string s = t.Assembly.FullName.ToString();
                //ErorDebag(string.Format("The fully qualified assembly name " + "containing the specified class is {0}.", s));
            }
            catch (Exception ex)
            {

                ErorDebag(ex.ToString(), 2);
                return false;
            }


            return true;

            // The AssemblyName type can be used to parse the full name.
            //AssemblyName assemName = assem.GetName();
            //Console.WriteLine("\nName: {0}", assemName.Name);
            //Console.WriteLine("Version: {0}.{1}", 
            //    assemName.Version.Major, assemName.Version.Minor);

            //Console.WriteLine("\nAssembly CodeBase:");
            //Console.WriteLine(assem.CodeBase);

            //// Create an object from the assembly, passing in the correct number
            //// and type of arguments for the constructor.
            //Object o = assem.CreateInstance("Example", false, 
            //    BindingFlags.ExactBinding, 
            //    null, new Object[] { 2 }, null, null);

            //// Make a late-bound call to an instance method of the object.    
            //MethodInfo m = assem.GetType("Example").GetMethod("SampleMethod");
            //Object ret = m.Invoke(o, new Object[] { 42 });
            //Console.WriteLine("SampleMethod returned {0}.", ret);

            //Console.WriteLine("\nAssembly entry point:");
            //Console.WriteLine(assem.EntryPoint);
            // }
        }


        private static Assembly MyResolveEventHandler(object sender, ResolveEventArgs args)
        {
            //Этот обработчик вызывается только тогда, когда общая языковая среда выполнения пытается привязать к сборке и не удается..

            //Retrieve the list of referenced assemblies in an array of AssemblyName.
            Assembly MyAssembly, objExecutingAssemblies;
            string strTempAssmbPath = "";

            objExecutingAssemblies = Assembly.GetExecutingAssembly();
            AssemblyName[] arrReferencedAssmbNames = objExecutingAssemblies.GetReferencedAssemblies();

            // Включить только для своих библиотек
            if (args.Name.Substring(0, args.Name.IndexOf(",")) == "FirebirdSql.Data.FirebirdClient")
            {
                // Цикл по массиву ссылка имен сборки.
                foreach (AssemblyName strAssmbName in arrReferencedAssmbNames)
                {
                    //Проверка для сборки имена, которые вызвали "AssemblyResolve" событие.
                    if (strAssmbName.FullName.Substring(0, strAssmbName.FullName.IndexOf(",")) == args.Name.Substring(0, args.Name.IndexOf(",")))
                    {
                        // Построить путь к сборке, откуда он должен быть загружен.				
                        strTempAssmbPath = SystemConecto.PutchApp + @"bin\dll\" + args.Name.Substring(0, args.Name.IndexOf(",")) + ".dll";
                        break;
                    }

                }
            }

            // Включить только для своих библиотек
            if (args.Name.Substring(0, args.Name.IndexOf(",")) == "AxInterop.ShockwaveFlashObjects")
            {
                // Цикл по массиву ссылка имен сборки.
                foreach (AssemblyName strAssmbName in arrReferencedAssmbNames)
                {
                    //Проверка для сборки имена, которые вызвали "AssemblyResolve" событие.
                    if (strAssmbName.FullName.Substring(0, strAssmbName.FullName.IndexOf(",")) == args.Name.Substring(0, args.Name.IndexOf(",")))
                    {
                        // Построить путь к сборке, откуда он должен быть загружен.				
                        strTempAssmbPath = SystemConecto.PutchApp + @"bin\dll\" + args.Name.Substring(0, args.Name.IndexOf(",")) + ".dll";
                        break;
                    }

                }
            }
            // Включить только для своих библиотек
            if (args.Name.Substring(0, args.Name.IndexOf(",")) == "WpfAnimatedGif")
            {
                // Цикл по массиву ссылка имен сборки.
                foreach (AssemblyName strAssmbName in arrReferencedAssmbNames)
                {
                    //Проверка для сборки имена, которые вызвали "AssemblyResolve" событие.
                    if (strAssmbName.FullName.Substring(0, strAssmbName.FullName.IndexOf(",")) == args.Name.Substring(0, args.Name.IndexOf(",")))
                    {
                        // Построить путь к сборке, откуда он должен быть загружен.				
                        strTempAssmbPath = SystemConecto.PutchApp + @"bin\dll\" + args.Name.Substring(0, args.Name.IndexOf(",")) + ".dll";
                        break;
                    }

                }
            }

            // Загрузка во время выполнения
            bool LoadOnline = false;
            Assembly CachMyAssembly = null;
            if (strTempAssmbPath.Length > 0)
            {
                try
                {
                    // Загрузить файл
                    // Assembly SampleAssembly = Assembly.LoadFrom(SystemConecto.PutchApp + @"bin\dll\FirebirdSql.Data.FirebirdClient.dll");
                    //Load the assembly from the specified path. 					
                    MyAssembly = Assembly.LoadFrom(strTempAssmbPath);
                    ErorDebag("Нашел имя и загрузил из системной директории - " + args.Name.Substring(0, args.Name.IndexOf(",")), 1);
                    CachMyAssembly = MyAssembly;
                    LoadOnline = true;
                    // Запускать один раз установку библиотеки
                    Publish publisher = new Publish();
                    publisher.GacInstall(SystemConecto.PutchApp + @"bin\dll\" + args.Name.Substring(0, args.Name.IndexOf(",")) + ".dll"); // FirebirdSql.Data.FirebirdClient.dll");

                }
                catch (Exception ex_)
                {
                    ErorDebag(ex_.ToString(), 1);
                    // Фокус с определением значения через кеш значения
                    MyAssembly = null;
                    if (LoadOnline)
                    {
                        MyAssembly = CachMyAssembly;
                    }


                }
            }
            else
            {
                //ErorDebag("Не нашел имени - " + args.Name , 1); //+ "\r\n" +args.RequestingAssembly.EntryPoint.ToString()
                MyAssembly = null;
            }


            //Возвращаем загруженную сборку.
            CachMyAssembly = null;
            return MyAssembly;
        }



        #endregion

     
        #region Управление в ОС Windows Выключением Компьютера

        //[DllImport("user32.dll")]
        //public static extern int ExitWindowsEx(int uFlags, int dwReason);

        //импортируем API функцию InitiateSystemShutdown
        [DllImport("advapi32.dll", EntryPoint = "InitiateSystemShutdownEx")]
        static extern int InitiateSystemShutdown(string lpMachineName, string lpMessage, int dwTimeout, bool bForceAppsClosed, bool bRebootAfterShutdown);
        //импортируем API функцию AdjustTokenPrivileges
        [DllImport("advapi32.dll", ExactSpelling = true, SetLastError = true)]
        internal static extern bool AdjustTokenPrivileges(IntPtr htok, bool disall,
        ref TokPriv1Luid newst, int len, IntPtr prev, IntPtr relen);
        //импортируем API функцию GetCurrentProcess
        [DllImport("kernel32.dll", ExactSpelling = true)]
        internal static extern IntPtr GetCurrentProcess();
        //импортируем API функцию OpenProcessToken
        [DllImport("advapi32.dll", ExactSpelling = true, SetLastError = true)]
        internal static extern bool OpenProcessToken(IntPtr h, int acc, ref IntPtr phtok);
        //импортируем API функцию LookupPrivilegeValue
        [DllImport("advapi32.dll", SetLastError = true)]
        internal static extern bool LookupPrivilegeValue(string host, string name, ref long pluid);
        //импортируем API функцию LockWorkStation
        [DllImport("user32.dll", EntryPoint = "LockWorkStation")]
        static extern bool LockWorkStation();

        // --------------------- Не работает
        //private void btnStandBy_Click(object sender, EventArgs e)
        //{
        //    //Application.SetSuspendState(PowerState.Suspend, true, true);
        //}
        //private void btnHibernate_Click(object sender, EventArgs e)
        //{
        //    //Application.SetSuspendState(PowerState.Hibernate, true, true);
        //}
        //private void btnLogOff_Click(object sender, EventArgs e)
        //{
        //    ExitWindowsEx(0, 0);
        //}

        // Ждущий режим:
        //  Объект System.Windows.Forms.PowerState

        //Application.SetSuspendState(PowerState.Suspend, true, true);

        // Режим гибернации:

        //Application.SetSuspendState(PowerState.Hibernate, true, true);

        // гибернации или спящий режим
        // System.Diagnostics.Process.Start("rundll32.exe", "powrprof.dll, SetSuspendState");

        // ---------------------- new
 
        
        //объявляем структуру TokPriv1Luid для работы с привилегиями
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        internal struct TokPriv1Luid
        {
            public int Count;
            public long Luid;
            public int Attr;
        }

        //объявляем необходимые, для API функций, константые значения, согласно MSDN
        internal const int SE_PRIVILEGE_ENABLED = 0x00000002;
        internal const int TOKEN_QUERY = 0x00000008;
        internal const int TOKEN_ADJUST_PRIVILEGES = 0x00000020;
        internal const string SE_SHUTDOWN_NAME = "SeShutdownPrivilege";
        
        //функция SetPriv для повышения привилегий процесса
        private static void SetPriv()
        {
            TokPriv1Luid tkp; //экземпляр структуры TokPriv1Luid 
            IntPtr htok = IntPtr.Zero;
            //открываем "интерфейс" доступа для своего процесса
            if (OpenProcessToken(GetCurrentProcess(), TOKEN_ADJUST_PRIVILEGES | TOKEN_QUERY, ref htok))
            {
                //заполняем поля структуры
                tkp.Count = 1;
                tkp.Attr = SE_PRIVILEGE_ENABLED;
                tkp.Luid = 0;
                //получаем системный идентификатор необходимой нам привилегии
                LookupPrivilegeValue(null, SE_SHUTDOWN_NAME, ref tkp.Luid);
                //повышем привилигеию своему процессу
                AdjustTokenPrivileges(htok, false, ref tkp, 0, IntPtr.Zero, IntPtr.Zero);
            }
        }
        /// <summary>
        /// публичный метод для перезагрузки/выключения машины
        /// (true, false)   //мягкая перезагрузка
        /// (true, true)    //жесткая перезагрузка
        /// (false, false)  //мягкое выключение
        /// (false, true)   //жесткое выключение
        /// </summary>
        /// <param name="RebootSh"></param>
        /// <param name="ForceOff">форсировать выключение</param>
        /// <returns></returns>
        public static int ShutdownPC(bool RebootSh, bool ForceOff)
        {
            SetPriv(); //получаем привилегию для своего приложения
            //вызываем функцию InitiateSystemShutdown, передавая ей необходимые параметры
            return InitiateSystemShutdown(null, null, 0, ForceOff, RebootSh);
            // 1 - Если lpMachineName является NULL или пустую строку, функция отключается на локальном компьютере.
            // 2 - Сообщение, которое будет отображаться в диалоговом окне завершения работы. Этот параметр может быть NULL, если сообщение не требуется.
            // 3 - Если dwTimeout равна нулю, компьютер выключается без отображения диалогового окна, а отключение не может быть остановлен AbortSystemShutdown .
            // 4 - 

            // Чтобы выключить удаленный компьютер, вызывающий поток должен иметь SE_REMOTE_SHUTDOWN_NAME привилегии на удаленном компьютере.
            // если возвращает - ERROR_NOT_READY - В этом случае приложение должно подождать некоторое время и повторите вызов.
        }

        /// <summary>
        /// Блокировка компьютера с помощью вывода приглашения ввести пароль пользователя ОС Windows (публичный метод для блокировки операционной системы)
        /// </summary>
        /// <returns></returns>
        public static int Lock()
        {
            if (LockWorkStation())
                return 1;
            else
                return 0;
        }

        /// <summary>
        /// Тестовое выключение компьютера с помощью (WMI - System.Management)
        /// Возможно удаленное выключение компьютера
        /// </summary>
        public void ShutDownComputer()
        {
            ManagementBaseObject outParameters = null;
            ManagementClass sysOS = new ManagementClass("Win32_OperatingSystem");
            sysOS.Get();
            // enables required security privilege.
            sysOS.Scope.Options.EnablePrivileges = true;
            // get our in parameters
            ManagementBaseObject inParameters = sysOS.GetMethodParameters("Win32Shutdown");

            //0 = Log off the network.
            //1 = Shut down the system.
            //2 = Perform a full reboot of the system.
            //4 = Force any applications to quit instead of prompting the user to close them.
            //8 = Shut down the system and, if possible, turn the computer off.

            inParameters["Flags"] = "1";
            inParameters["Reserved"] = "0";
            foreach (ManagementObject manObj in sysOS.GetInstances())
            {
                outParameters = manObj.InvokeMethod("Win32Shutdown", inParameters, null);
            }
        }


        #endregion

        #region Управление сканированием группы портов в сети
        /// <summary>
        ///  Управление сканированием группы портов в сети
        /// </summary>
        /// <param name="ips"></param>
        /// <param name="StartPort"></param>
        /// <param name="EndPort"></param>
        private static Dictionary<int, string> NetScan(IPAddress ips, int StartPort, int EndPort = 0)
        {

            /// Таблица портов http://ru.wikipedia.org/wiki/%D0%A1%D0%BF%D0%B8%D1%81%D0%BE%D0%BA_%D0%BF%D0%BE%D1%80%D1%82%D0%BE%D0%B2_TCP_%D0%B8_UDP

            Dictionary<int, string> InfoPortNet = new Dictionary<int, string>();
            TcpClient TcpScan = new TcpClient();

            // ------------- Один порт
            if (EndPort == 0)
            {

                //ErorDebag();
                // string host = "localhost";

                // int port = 6900;

                // IPAddress addr = (IPAddress)Dns.GetHostAddresses(host)[0];

                try
                {
                    // Проверка локальных портов на локальных адресах 
                    // TcpListener tcpList = new TcpListener(ips, StartPort);
                    // tcpList.Start();
                    // Попытка подключения
                    TcpScan.Connect(ips, StartPort);
                    InfoPortNet[StartPort] = "True";
                }

                catch (SocketException sx)
                {

                    ErorDebag(sx.ToString());
                    // Catch exception here if port is blocked
                    InfoPortNet[StartPort] = sx.ToString();
                }
            }
            else
            {
                // ----------------------- Много портов  
                // Проганяем через порты между портом начала и портом окончания
                for (int CurrPort = StartPort; CurrPort <= EndPort; CurrPort++)
                {

                    try
                    {
                        // Попытка подключения
                        TcpScan.Connect(ips, CurrPort);
                        // Если не сработало исключение, мы можем сказать, что порт открыт

                    }
                    catch
                    {
                        // Сработало исключение, порт для нас закрыт
                    }

                }


            }
            return InfoPortNet;

        }
        #endregion

        #region Режимы выключения программы и компьютера
        /// <summary>
        /// Выключение программы и компьютера
        /// </summary>
        public static void EndWorkPC()
        {
            var Window = SystemConecto.ListWindowMain("WaitFonW");
            if (Window != null)
            {
                Window.Show();
            }
            else
            {
                WaitFon FonWindow = new WaitFon();
                FonWindow.Owner = Window;
                FonWindow.Show();
            }
            
            Environment.Exit(0); // Это работает
        }
        #endregion

        #region Осмотр сети для Администратора
        /// <summary>
        ///  Осмотр сети (в администрировании кнопка Осмотреть сеть)
        /// </summary>
        /// <param name="Type"></param>
        /// <returns></returns>
        public static Dictionary<string, string> ViewNet(int Type = 0)
        {
            // Возвращвет масив данных: Параметр - данные
            Dictionary<string, string> InfoViewNet = new Dictionary<string, string>();
            Dictionary<int, string> IpPrtNet_ml = new Dictionary<int, string>();        // Отсканированные порты
            IPAddress IpAdrr_ml = IPAddress.Parse("127.0.0.1");                         // Айпи Адресс для методов

            // Переопределить масив (очистить)
            InfoPingNet = new Dictionary<string, string>();
            locker_a = new int[2] { 0, 0 };

            // Имя пользователя System.Environment.UserName

            // Проверяем сеть Сеть делится на два типа: Инфраструктура всей сети и сети системы
            // Сеть системы работает с определнными портами:
            //      - порт программы
            // 3050 - Сервер БД Feirb. Для точной проверки запрос на алиас БД 
            // Вся сеть отвечает на порты:
            var IPDevice = IP_DeviceCcurent();
            foreach (KeyValuePair<string, string> dani in IPDevice)
            {

                // Чтения адаптера ID который подключен к сети
                if (NetworkPC[dani.Key + "_STATUS"] == "Up")
                {
                    // ErorDebag("Отладка: Сети " + NetworkPC[dani.Key + "_Description"] + " | " + NetworkPC[dani.Key + "_IP"] + " | " + NetworkPC[dani.Key + "_MAC-ADRESS"] + " | " + NetworkPC[dani.Key + "_TypeInterf"]);
                    // Проходим по адресам локальной группы
                    IpAdrr_ml = IPAddress.Parse(NetworkPC[dani.Key + "_IP"]);
                    var IpSplit = IpAdrr_ml.ToString();
                    var IpSplit_a = IpSplit.Split('.');
                    // Подсети и VPN
                    var IPGoup_a = new String[6] { "1", "2", "3", "100", "23", "123" };
                    if (Array.IndexOf(IPGoup_a, IpSplit_a[2]) < 0)
                    {
                        Array.Resize(ref IPGoup_a, IPGoup_a.Length + 1);
                        IPGoup_a[6] = IpSplit_a[2];
                    }



                    foreach (string DaniGr in IPGoup_a)
                    {
                        // Проверяем группу адресов в кторой находится компьютер
                        if (DaniGr == IpSplit_a[2])
                        {
                            // Смотрим сеть. Маркируем потоки в масиве
                            Thread[] threads = new Thread[256];
                            for (int i = 1; i < 256; i++)
                            {
                                // Формирование адресса
                                IpSplit = IpSplit_a[0] + "." + IpSplit_a[1] + "." + DaniGr + "." + i.ToString();
                                RenderInfo Arguments = new RenderInfo() { argument1 = IpSplit, argument2 = i.ToString(), argument3 = NetworkPC[dani.Key + "_TypeInterf"] };
                                Thread th_ip = new Thread(PingNet_Th);
                                threads[i] = th_ip;
                                threads[i].SetApartmentState(ApartmentState.STA);
                                threads[i].IsBackground = true; // Фоновый поток
                                threads[i].Start(Arguments);
                                //Ждем окончания последнего потока
                                if (i == 255)
                                {
                                    threads[i].Join();
                                    // System.Windows.Forms.MessageBox.Show("Отладка: " + locker_a[0].ToString() + " | " + locker_a[1].ToString());

                                    // Отследить выполнение потоков (пробую заменить while for -ом)
                                    for (int iEnd = 0; iEnd < 2; iEnd++)
                                    {
                                        if (locker_a[0] == locker_a[1])
                                        {
                                            // Потоки завершины
                                            foreach (KeyValuePair<string, string> DeviceDani in InfoPingNet)
                                            {
                                                // System.Windows.Forms.MessageBox.Show("Отладка: " + DeviceDani);
                                                // ErorDebag("Память: " + NetworkPC[dani.Key + "_Description"] + " | " + DeviceDani);
                                                InfoViewNet[DeviceDani.Key] = DeviceDani.Value;
                                            }


                                            iEnd = 2;
                                        }
                                        else
                                        {
                                            // Продолжить
                                            iEnd = 0;
                                        }

                                    }
                                }

                                // IpPrtNet_ml = NetScan(IpAdrr_ml, 3050);
                                //foreach (KeyValuePair<int, string> daniPort in IpPrtNet_ml)
                                //{
                                //   if (daniPort.Value == "True")
                                //   {
                                //        ErorDebag("Отладка: " + daniPort.Key + " | Есть сервер");
                                //   }
                                //}

                            }
                        }
                    }

                }

            }
            return InfoViewNet;
        }
        #endregion

        #region Управление потоками -Thread глобально в системе v1.2


        /// <summary>
        /// Объект кторый  можно передать потоку в многопотоковой среде например структуру данных
        /// </summary>
        public delegate void ParameterizedThreadStart(object ThreadObj);
        /// <summary>
        /// Структура данных для многопотоковой среды (передача аргументов)
        /// </summary>
        struct RenderInfo
        {
            public string argument1 { get; set; }
            public string argument2 { get; set; }
            public string argument3 { get; set; }
        }
        //public static IPAddress ips_s { get; set; } // Айпи адресс кторый запрашивают в многопотоковой среде
        /// <summary>
        /// Код, защищенный таким образом от неопределённости в плане многопотокового исполнения, называется потокобезопасным. 
        /// Все потоки при записи борются за блокировку объекта
        /// </summary>
        static object locker1 = new object();
        static object locker2 = new object();
        /// <summary>
        /// Отслеживание выполнения потоков locker[0] -количество зарегистрировавшихся потоков, 
        /// locker[1] - количество потоков кторые завершили выполнятся
        /// </summary>
        public static int[] locker_a = new int[2] { 0, 0 };
        //static int[] locker_a { get; set; }

        // Пример использования
        // RenderInfo Arguments = new RenderInfo() { argument1 = IpSplit, argument2 = i.ToString(), argument3 = NetworkPC[dani.Key + "_TypeInterf"] };
        // Thread th_ip = new Thread(PingNet_Th);
        // threads[i] = th_ip;
        // threads[i].SetApartmentState(ApartmentState.STA);
        // threads[i].IsBackground = true; // Фоновый поток
        // threads[i].Start(Arguments);
        #endregion

        #region IPPing Device -Thread
        /// <summary>
        /// Масив для IP адрессов сети в многопоточной среде {IP адресс} {Время ответа}
        /// </summary>
        public static Dictionary<string, string> InfoPingNet = new Dictionary<string, string>();

        /// <summary>
        /// Эхо запрос в сети в многопотоковой среде
        /// отсутствуют параметры и результат вывода
        /// </summary>
        private static void PingNet_Th(object ThreadObj)
        {
            // Отслеживание ошибок в птоке для многопотоковой среде 
            try
            {
                // Разбор аргументов
                RenderInfo arguments = (RenderInfo)ThreadObj;
                var ips_s = IPAddress.Parse(arguments.argument1);   // Айпи адресс
                var ThreadNumber = arguments.argument2;             // Номер потока
                var TypeInterf = arguments.argument3;               // Тип интерфейса, сетевого адаптера (Ppp - VPN)
                // Регистрация потока
                lock (locker1)
                {
                    locker_a[0] = locker_a[0] + 1;
                }


                var timeout = TypeInterf == "Ppp" ? 3000 : 290;      // Доступ к другим сетям больше затрачивает времени по доступу
                var buffer = new byte[] { 0, 0, 0, 0 };

                // создаем и отправляем ICMP request
                var ping = new Ping();
                // TTL (время жизни) IP-пакетов
                // у разных операционных систем TTL по умолчанию в пределе от 32 до 128, так например у Linux-систем ttl по умолчанию равно 64, а у Windows - 128, но значение это четное
                var reply = ping.Send(ips_s, timeout, buffer, new PingOptions { Ttl = 128 });

                // если ответ успешен
                if (reply.Status == IPStatus.Success)
                {
                    lock (locker2)
                    {
                        // ErorDebag("Отладка потока: поток " + ThreadNumber +" " + reply.Address + " | " + reply.RoundtripTime + "|" + ConvertIpToMAC(ips_s));
                        InfoPingNet[reply.Address.ToString() + "_IP"] = reply.RoundtripTime.ToString();
                        InfoPingNet[reply.Address.ToString() + "_MAC"] = ConvertIpToMAC(ips_s);
                        // NetBios имя компьютера (замедляет работу, может заменить на протокол ConectoNet)
                        string NameHost_ = "";
                        try
                        {
                            var DnsName = Dns.GetHostEntry(ips_s);
                            NameHost_ = DnsName.HostName;
                        }
                        catch  //(SocketException ExDns) ускоряет код
                        {
                            // Частое исключение System.Net.Sockets.SocketException (0x80004005): Этот хост неизвестен
                            // ErorDebag("Отладка: " +ExDns.ToString());
                        }

                        InfoPingNet[reply.Address.ToString() + "_NETBIOS"] = NameHost_;
                        //Пробуем определить поддерживает ли объект SNMP если нет то FALSE
                        //SNMPObject ob = new SNMPObject("1.3.6.1.2.1.1.5.0");
                        //try
                        //{

                        //    string agent = ob.getSimpleValue(new SNMPAgent(IPClassAddres.IPAddres));

                        //}
                        //catch { }
                        // Завершение потока
                        locker_a[1] = locker_a[1] + 1;
                    }
                }
                else
                {
                    // Закончилось не удачей (конец времени или ...)
                    lock (locker2)
                    {
                        locker_a[1] = locker_a[1] + 1;
                    }
                }

            }
            catch (Exception ex)
            {
                // Отследить ошибки
                lock (locker1)
                {
                    ErorDebag(ex.ToString());
                }
            }


        }
        #endregion

        #region IPPing Device -Return {Изменил под функцию, проверить в соответствии с  IPPing Device -Thread}

        private static bool PingNet(IPAddress ips, int Type = 0)
        {
            // Отслеживание ошибок 
            try
            {
                // Dictionary<string, long> InfoPingNet_ = new Dictionary<string, long>();
                var timeout = 9220;
                var buffer = new byte[] { 0, 0, 0, 0 };

                // создаем и отправляем ICMP request
                var ping = new Ping();
                var reply = ping.Send(ips, timeout, buffer, new PingOptions { Ttl = 128 });

                // если ответ успешен
                if (reply.Status == IPStatus.Success)
                {
                    //ErorDebag("Отладка: " + reply.Address + " | " + reply.RoundtripTime);
                    return true;
                }
                else
                {
                    ErorDebag(reply.Address + " | " + reply.Status, 1);
                }
                // return InfoPingNet_; Dictionary<string, long>
            }
            catch (Exception ex)
            {
                // Отследить ошибки
                ErorDebag(ex.ToString());
            }
           return false;
            
        }
        #endregion

        #region MAC Device -Return
        // Управление сетевыми запросами (Для определения MAC-адресса удаленной машины)
        // Расположение C:\Windows\System32 (C:\Windows\winsxs\x86_microsoft-windows-t..-platform-libraries_31bf3856ad364e35_6.1.7600.16385_none_ea474108fca1c083)
        [DllImport("iphlpapi.dll", ExactSpelling = true)]
        public static extern int SendARP(int DestIP, int SrcIP, [Out] byte[] pMacAddr, ref int PhyAddrLen);
        /// <summary>
        /// Трансформация Ip адресса в MAC адресс с таблицы сетевых адаптеров
        /// </summary>
        /// <param name="ip"></param>
        /// <param name="Type"></param>
        /// <returns></returns>
        public static string ConvertIpToMAC(IPAddress ip, int Type = 0)
        {
            byte[] ab = new byte[6];
            int len = ab.Length;
            int r = SendARP(ip.GetHashCode(), 0, ab, ref len);
            // BitConverter.ToString(ab, 0, 6); // конвертирование в формат 00-00-00-00-00
             //ab.ToString()
            return BitConverter.ToString(ab, 0, 6); ;
            // System.Net.NetworkInformation.PhysicalAddress();
        }
        #endregion

        #region Обзор сетевых интерфейсов и их свойств {v 1.2}

        #region Определение настроек сетевого подключения Структура: 0 - имя компьютера NETBIOS; 1 - _IP (айпи адрес адаптера) {v 1.2};
        /// <summary>
        ///  Определение настроек сетевого подключения <para></para>
        ///  Структура: 0 - имя компьютера NETBIOS; <para></para>
        ///  1 - _IP (айпи адрес адаптера); <para></para>
        ///  3 -_SCHLUZ (шлюз группы сети); <para></para>
        ///  4 - _MAC-ADRESS (вытаскиваем и показываем MAC-адрес (физический адрес адаптера));<para></para>
        ///  5 - _Description (Описание адаптера);<para></para>
        /// NoLoopback: - включения в список адаптеров Loopback (127.0.0.1)
        /// <returns></returns>
        /// </summary>
        public static Dictionary<string, string> IP_DeviceCcurent(bool NoLoopback=false)
        {
            //(только для одного адаптера, обновил для всех адаптеров)

            // для работы нужно импортировать пространство имен System.Net
            // using System.Net;
            // using System.Net.NetworkInformation;
            // using System.Net.Sockets;
            Dictionary<string, string> IPDevice = new Dictionary<string, string>();

            // Получаем список сетевых интерфейсов. интерфейсы могут быть физические (ethernet,
            // wifi и т.п.), и программные/виртуальные, к примеру, VPN-интерфейсы.
            // Если наберете в командной строке «cmd» команду «ipconfig /all», то сетевой
            // интерфейс там будет показан адаптером.

            var networkInterfaces = NetworkInterface.GetAllNetworkInterfaces();
            foreach (var networkInterface in networkInterfaces)
            {
                // у каждого интерфейса выбираем его IP-адреса, если они у него есть
                foreach (var address in networkInterface.GetIPProperties().UnicastAddresses)
                {
                    // Есть еще возможность проверить тип интерейса
                    // для каждого интерфейса Ethernet
                    // if (networkInterface.NetworkInterfaceType == NetworkInterfaceType.Ethernet)
                   

                    NoLoopback = IPAddress.IsLoopback(address.Address) && !NoLoopback ? false : true; // (IPAddress.IsLoopback(address.Address) && NoLoopback ? true : true);

                    //мне нужны только IPv4-адреса, также исключаю loopback-адреса (127.0.0.1), (т.к. и так понятно, что он в любом случае есть.)
                    if (address.Address.AddressFamily == AddressFamily.InterNetwork && NoLoopback)
                    {
                        //MessageBox.Show("IP-" + address.Address.ToString());
                        IPDevice[networkInterface.Id] = "";
                        // Структура: 0 - имя компьютера NETBIOS; 1 - _IP (айпи адрес адаптера);
                        // 3 -_SCHLUZ (шлюз группы сети); 4 - _MAC-ADRESS (вытаскиваем и показываем MAC-адрес (физический адрес адаптера));
                        // 5 - _Description (Описание адаптера);
                        // получаем хост Запись в память лишняя и так читаем с памяти
                        // NetworkPC[networkInterface.Name+"_HOST"] = Dns.GetHostName();
                        // 1 - айпи адрес
                        NetworkPC[networkInterface.Id + "_IP"] = address.Address.ToString();
                        // 2 - Тип интерфейса (резерв)
                        NetworkPC[networkInterface.Id + "_TypeInterf"] = networkInterface.NetworkInterfaceType.ToString();

                        // 3 - шлюз
                        foreach (var SCHLU_address in networkInterface.GetIPProperties().GatewayAddresses)
                        {
                            NetworkPC[networkInterface.Id + "_SCHLUZ"] = SCHLU_address.Address.ToString();
                        }

                        //foreach (var SCHLU_address in networkInterface.GetIPProperties().GatewayAddresses)
                        //{
                        //    NetworkPC[networkInterface.Id + "_SCHLUZ"] = SCHLU_address.ToString();
                        //    break;
                        //}
                        // 4 - вытаскиваем и показываем MAC-адрес (физический адрес адаптера)
                        var MACaddress = networkInterface.GetPhysicalAddress().ToString();

                        // int MACaddress_ = int.Parse(MACaddress); // кодировка в цифру
                        // BitConverter.ToString(Encoding.Unicode.GetBytes(MACaddress), 0, 6); // кодировка в байты
                        // BitConverter.ToString(networkInterface.GetPhysicalAddress().GetAddressBytes(), 0, 6) // конвертирование в формат 00-00-00-00-00

                        NetworkPC[networkInterface.Id + "_MAC-ADRESS"] = !string.IsNullOrWhiteSpace(MACaddress) ? MACaddress : "";
                        // 5 - Описание адаптера
                        NetworkPC[networkInterface.Id + "_Description"] = networkInterface.Description.ToString();


                        // 6 - состояние соединения
                        NetworkPC[networkInterface.Id + "_STATUS"] = networkInterface.OperationalStatus.ToString();
                        // 7 - Тип адреса Статика, DHCP
                        NetworkPC[networkInterface.Id + "_DCHP"] = networkInterface.GetIPProperties().IsDynamicDnsEnabled.ToString();
                        // 8 - скорость соединения



                    }
                }
            }


            //// получаем хост
            //IPDevice[0] = Dns.GetHostName();

            //// получаем IP-адрес хоста
            //IPAddress[] ips = Dns.GetHostAddresses(IPDevice[0]);
            //foreach (IPAddress ip in ips)
            //{
            //    IPDevice[1] = ip.ToString();
            //}
            //string[] aIPGroup = IPDevice[1].Split('.');
            ///// Получаем пред-последний елемент
            //// int length = aIPGroup.Length - 1;
            //IPDevice[2] = aIPGroup[aIPGroup.Length - 1];

            // Не рекомендовано - System.Net.Dns.GetHostByName(myHost).AddressList[0].ToString();

            // получение IP по dns-имени
            //var hostEntry = Dns.GetHostEntry(«www.yandex.ru»);
            //foreach (var address in hostEntry.AddressList)
            //   Console.WriteLine(address);

            //// обратная операция
            //var hostEntry = Dns.GetHostEntry(«212.48.193.37″);
            //Console.WriteLine(hostEntry.HostName); 

            return IPDevice;
        }
        #endregion

        #region Логический метод анализирующий работу сетевых адаптеров
        /// <summary>
        /// Логический метод анализирующий работу сетевых адаптеров 
        /// (адаптеров которые включенны в панели управления, - состояние включенный! Выключенные адаптеры отсутствуют в проверке)
        /// Результат 0- количество адаптеров , 1- включенных адаптеров, <para></para>
        /// 2 - количество выключенных адаптеров  WiFi<para></para>
        /// 3 - количество выключенных адаптеров Ppp <para></para>
        /// 4 - количество выключенных адаптеров Ethernet<para></para>
        /// 5 - количество выключенных других адаптеров<para></para>
        /// <returns>returns: цифровой масив</returns><para></para>
        /// </summary>
        public static int[] NetOff ()
        {
            
            int[] NetOff_a = new int[6] { 0, 0, 0, 0, 0, 0 };

            // Перебор настроек сетевых адаптеров
            var IPDevice = IP_DeviceCcurent();
            foreach (KeyValuePair<string, string> dani in IPDevice)
            {
                // Отладка
                // ErorDebag(NetworkPC[dani.Key + "_TypeInterf"]);
                NetOff_a[0] = NetOff_a[0] + 1;
                // Чтения адаптера ID который подключен к сети
                if (NetworkPC[dani.Key + "_STATUS"] == "Up")
                {
                    NetOff_a[1] = NetOff_a[1] + 1;

                }
                else
                {
                    switch (NetworkPC[dani.Key + "_TypeInterf"])
                    {
                        case "Wireless80211":
                            NetOff_a[2] = NetOff_a[2] + 1;

                            break;
                        case "Ppp":
                            NetOff_a[3] = NetOff_a[3] + 1;

                            break;
                        case "Ethernet":
                            NetOff_a[4] = NetOff_a[4] + 1;

                            break;
                        default:
                            NetOff_a[5] = NetOff_a[4] + 1;

                            break;

                    }
                }

            }
            // ОТладка
            // ErorDebag(NetOff_a[4].ToString());
            return NetOff_a;
        }
        #endregion

        #region Проверка физического соединения со шлюзами локальной сети
        /// <summary>
        /// Проверка физического соединения со шлюзами локальной сети
        /// </summary>
        /// <returns>0-нет соединения, 1-есть соединение, 2-отсутствует шлюз при подключенном адапторе</returns>
        public static int NetGetwai()
        {
            int Conect = 0;
            int Conect_ = 0;
            var IPDevice = IP_DeviceCcurent();
            foreach (KeyValuePair<string, string> dani in IPDevice)
            {
                // Отладка
                // ErorDebag(NetworkPC[dani.Key + "_TypeInterf"]);

                // Чтения адаптера ID который подключен к сети
                if (NetworkPC[dani.Key + "_STATUS"] == "Up")
                {
                    
                    // Проверка шлюза (шлюз может отсутствовать)
                    if(NetworkPC.ContainsKey(dani.Key + "_SCHLUZ")){
                        ErorDebag(NetworkPC[dani.Key + "_SCHLUZ"].Length.ToString());
                        if (NetworkPC[dani.Key + "_SCHLUZ"].ToString() == "0.0.0.0")
                        {
                            // 2-отсутствует шлюз при подключенном адапторе (широковещательное вещание работает)
                            // Возможно подключение через широковещательный шлюз проверяется пингом на сайт google.com
                            Conect_ = 2;
                        }
                        else
                        {
                            if (PingNet(IPAddress.Parse(NetworkPC[dani.Key + "_SCHLUZ"])))
                            {
                                Conect = 1;
                                break;
                            }
                        }
                    } 

                }


            }
            // ОТладка
            // ErorDebag();
            // Проверка результатов, если выключенны все адаптеры, но есть включенный адаптер без шлюза сообщить об этом
            Conect = Conect == 0 ? Conect_ : Conect;
            return Conect;
        }
        #endregion

        #region Событие которое возникает при смене свойств сетевых адаптеров

        // При обнаружении смены настроек адаптеров или установки новых в режиме он лайн
        // Событие перенастраивает все сервера и службы системы 

        #endregion

        #endregion

        #region Удаленное подключение к Компьютеру

        public static void ConectToPC()
        {

            ConnectionOptions options = new ConnectionOptions();
            options.Username = "Администратор";
            options.Password = "";
            //        connection.Authority = "ntlmdomain:";

            ManagementScope scope =
                new ManagementScope("\\\\192.168.1.231\\root\\cimv2", options);
            scope.Connect();

            ObjectQuery query = new ObjectQuery(
                "SELECT * FROM Win32_OperatingSystem");
            ManagementObjectSearcher searcher =
                new ManagementObjectSearcher(scope, query);

            ManagementObjectCollection queryCollection = searcher.Get();
            foreach (ManagementObject m in queryCollection)
            {
                //System.Windows.Forms.MessageBox.Show("Отладка: " + m["csname"].ToString() + " | " + m["WindowsDirectory"].ToString() +
                //    " | " + m["Caption"].ToString() + " | " + m["Manufacturer"].ToString());
                // Display the remote computer information

            }
            Console.ReadLine();

            // Рабочие станции под управлением Windows XP Professional и Vista, не подключенные к домену, по умолчанию не позволяют 
            // локальному администратору аутентифицироваться под собственным именем по сети. Вместо этого, используется политика 
            //"ForceGuest", которая означает, что все удаленные подключения производятся с правами гостевой учетной записи.
            // Однако, как уже было сказано, для сканирования требуются права администратора. Поэтому на каждом удаленном компьютере
            // требуется произвести настройку политики безопасности: "Пуск - Выполнить - secpol.msc - OK - Локальные политики - 
            //Параметры безопасности - Сетевой доступ: модель совместного доступа и безопасности..." - если она имеет значение "Гостевая", 
            // переключите на "Обычная" 

        }


        #endregion

        #region Wake-on-LAN Удаленного компьютора


        public class WOLClass : UdpClient
        {
            // ------------------ Start Class
            public WOLClass()
                : base()
            { }
            //Установим broadcast для отправки сообщений
            public void SetClientToBrodcastMode()
            {
                if (this.Active)
                    this.Client.SetSocketOption(SocketOptionLevel.Socket,
                                              SocketOptionName.Broadcast, 0);
            }
            //---------------- End Class
        }

        /// <summary>
        /// Включить компьютер в сети 
        /// MAC адрес должен выглядеть следующим образом: 013FA04912, а не 00-00-00-00-00
        /// </summary>
        /// <param name="MAC_ADDRESS"></param>
        public static void WakeUP(string MAC_ADDRESS)
        {
            WOLClass client = new WOLClass();
            client.Connect(new IPAddress(0xffffffff), 0x2fff); //Используем порт = 12287
            // Широковешательный способ
            client.SetClientToBrodcastMode();
            // На определенный адрес ... (сделать)

            int counter = 0;
            //буффер для отправки
            byte[] bytes = new byte[1024];
            //Первые 6 бит 0xFF
            for (int y = 0; y < 6; y++)
                bytes[counter++] = 0xFF;
            //Повторим MAC адрес 16 раз
            for (int y = 0; y < 16; y++)
            {
                int i = 0;
                for (int z = 0; z < 6; z++)
                {
                    bytes[counter++] = byte.Parse(MAC_ADDRESS.Substring(i, 2), NumberStyles.HexNumber);
                    i += 2;
                }
            }

            //Отправим магический пакет
            int reterned_value = client.Send(bytes, 1024);
        }



        // Описание задачи
        /*
         *      Управляемый компьютер находится в дежурном режиме (англ. stand-by) и выдаёт питание на сетевой адаптер. 
         * Сетевой адаптер находится в режиме пониженного энергопотребления, просматривая все пакеты, приходящие на его MAC-адрес, 
         * и ничего не отвечая на них. Если одним из пакетов окажется magic packet, сетевой адаптер выдаст сигнал на включение питания компьютера.
         * Magic packet — это специальная последовательность байтов, которую для нормального прохождения по локальным сетям можно вставить в пакеты UDP или IPX.
         * Есть два способа послать широковещательно и через маршрутизатор, запрещающий широковещательные пакеты, можно послать пакет по какому-то определённому адресу.
         * Правило построение, в начале пакета идет так называемая цепочка синхронизации: 6 байт, равных 0xFF. Затем — MAC-адрес сетевой платы, повторённый 16 раз.
         * 
         * На компьюторе необходимо установка в BIOS параметра Wake After Power Fail («пробуждаться после пропадания питания») в значение On («Вкл.»).
        */
        #endregion

        #region Проверка работоспособности соедининий (с БД, сервером Conecto, Интернет и пр.) -Thread
        /// <summary>
        /// Запуск потоков (тиков) -Thread для выполнения задач системы
        /// </summary>
        public static System.Timers.Timer WorkSpaceTimer1 = null;

        /// <summary>
        /// Временные отметки для выполнения кода<para></para>
        /// <para></para>
        /// Перечень потоков (выполняемых задач) Tick_a<para></para>
        /// Tick_a[0] - Нулевой тик  для вывда времени 1 тик 5 секунд 60/5<para></para>
        /// Tick_a[1] - Первый тик индикатор проверки соединения сети<para></para>
        /// Tick_a[2] - Второй тик для проверки соединения С БД {рабочих программ}<para></para>
        /// Tick_a[3] - Третий тик для Окна ожидания Wait, задача закрыть (отмеряем задержку окна, возможно замерить производительность выполняемых задач!)<para></para>
        /// Tick_a[4] - Четвертый тик для Проверки ключа лицензии для B52 ( не проверяет если не установлен режим проверки)<para></para>
        /// Tick_a[5] - Пятый тик для Проверки соединения С БД Терминала ( не проверяет если не установлен режим проверки)<para></para>
        /// Tick_a[6] - Запуск клиента синхронизации времени<para></para>
        /// Tick_a[7] - для удаления файлов в фоне<para></para>
        /// </summary>
        public static int[] Tick_a = new int[18] { 0, 0, 0, 0, -1, -1, 9, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
        /// <summary>
        /// Режимы тиков (каждый тик может еще иметь свои режимы: Режим -7 для всех потоков, поток выключен )
        /// </summary>
        public static int[] TickRg_a = new int[18] { 0, 0, 0, 0, -7, -7, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
        /// <summary>
        /// Предыдущие состояния тиков (каждый тик может еще иметь свое предыдущие состояние)<para></para>
        /// TickMemory_a[1] - состояние подключения к интернету; 1 - подключен
        /// </summary>
        public static int[] TickMemory_a = new int[18] { 0, -1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
        
        /// <summary>
        /// Остановка выполнения потоков до разрешения (1 - вывод информационных окон на рабочий стол запрещен блокировка)
        /// </summary>
        private static int[] WaitMemory_a = new int[2] {0,0};
        //static object lockWaitWindow = new object();
        //static object lockWait = new object();

        /// <summary>
        /// Запуск обнаружения задач в системе
        /// </summary>
        public static void TimerWorkSpace()
        {

            System.Timers.Timer WorkSpaceTimer1 = new System.Timers.Timer();
            WorkSpaceTimer1.Elapsed += new ElapsedEventHandler(TaskWorkSpace1);
            WorkSpaceTimer1.Interval = 3000; // каждые три секунды
            WorkSpaceTimer1.Start();

            // ------- Выполнение стартового кода
            // Расчет тиков
            // 1.  Часы на основном экране
            DateTime now_ = DateTime.Now;
            int Second = now_.Second;
            decimal deltaTick = Second/5;
            Tick_a[0] = (int)Math.Round(deltaTick);

            // 2. проверка соединения сети
            Tick_a[1] = 15;

            // 3. Проверка времени Тик 2 Поток №3
            if (SystemConecto.aParamApp["Time_IP"] == "0.0.0.0")
            {
                TickRg_a[6] = -7;
            }

        
        }

        #region Запуск выпоняемых задач №1 с периодичностью
        /// <summary>
        /// Описание выпоняемых задач №1
        /// </summary>
        /// <param name="source"></param>
        /// <param name="e"></param>
        public static void TaskWorkSpace1(object source, ElapsedEventArgs e)
        {
            // Тик 0 Время
            if (TickRg_a[0]>-7 && Tick_a[0] == 12)
            {
                    // Сброс тика
                    Tick_a[0] = 0;
                    RenderInfo Arguments01 = new RenderInfo() { };
                    Thread thStartTimer01 = new Thread(Time_Th);
                    thStartTimer01.SetApartmentState(ApartmentState.STA);
                    thStartTimer01.IsBackground = true; // Фоновый поток
                    thStartTimer01.Start(Arguments01);

            }
            // Тик 1 Интернет и сеть (если сеть отключена от интернета то проверка сети ускоряется до 5 секунд)
            if (TickRg_a[1] > -7 && (Tick_a[1] == 15 || TickMemory_a[1] == 0 && Tick_a[1] == 5))
            {
                    Tick_a[1] = 0;    
                    RenderInfo Arguments02 = new RenderInfo() { };
                    Thread thStartTimer02 = new Thread(Internet_Th);
                    thStartTimer02.SetApartmentState(ApartmentState.STA);
                    thStartTimer02.IsBackground = true; // Фоновый поток
                    thStartTimer02.Start(Arguments02);

            }
            // Тик 2 Связь с БД
            if (TickRg_a[2] > -7 && Tick_a[2] == 3)
            {
                Tick_a[2] = 0; 
                RenderInfo Arguments03 = new RenderInfo() { };
                Thread thStartTimer03 = new Thread(BDActive_Th);
                thStartTimer03.SetApartmentState(ApartmentState.STA);
                thStartTimer03.IsBackground = true; // Фоновый поток
                thStartTimer03.Start(Arguments03);

            }
            // Тик 3 Сервис закрыть окно Ожидания Wait
            if (TickRg_a[3] > -7 && Tick_a[3] == -1)
            {
                WinWait_Close();

            }
            // 4 тик для Проверки ключа лицензии для B52
            if (TickRg_a[4] > -7 && (Tick_a[4] == 35 && TickMemory_a[4] == 0 || (Tick_a[4] == -1 && TickMemory_a[4] == 0)))
            {
                Tick_a[4] = 0;
                TickMemory_a[4] = 1;
                RenderInfo Arguments04 = new RenderInfo() { };
                Thread thStartTimer04 = new Thread(KeyB52Active);
                thStartTimer04.SetApartmentState(ApartmentState.STA);
                thStartTimer04.IsBackground = true; // Фоновый поток
                thStartTimer04.Start(Arguments04);
                

            }
            // 5 тик для Проверки соединения з БД терминала
            if (TickRg_a[5] > -7 && ( Tick_a[5] == 20 && TickMemory_a[5] == 0 || (Tick_a[5] == -1 && TickMemory_a[5] == 0)))
            {
                Tick_a[5] = 0;
                TickMemory_a[5] = 1;
                RenderInfo Arguments05 = new RenderInfo() { };
                Thread thStartTimer05 = new Thread(BdActiveTerminal);
                thStartTimer05.SetApartmentState(ApartmentState.STA);
                thStartTimer05.IsBackground = true; // Фоновый поток
                thStartTimer05.Start(Arguments05);


            }
            // Тик 6 Время
            if (TickRg_a[6] > -7 && Tick_a[6] == 10)
            {
                // Сброс тика
                Tick_a[6] = 0;
                RenderInfo Arguments06 = new RenderInfo() { };
                Thread thStartTimer06 = new Thread(Synhro_Time);
                thStartTimer06.SetApartmentState(ApartmentState.STA);
                thStartTimer06.IsBackground = true; // Фоновый поток
                thStartTimer06.Start(Arguments06);

            }


            // Нулевой тик  для вывда времени 1 тик 5 секунд 60/5
            if (TickRg_a[0] != -7)
                Tick_a[0] = Tick_a[0] + 1;
            // Первый тик для проверки соединения сети
            if (TickRg_a[1] != -7)
                Tick_a[1] = Tick_a[1] + 1;
            // Третий тик для проверки соединения С БД {рабочих программ}
            if (TickRg_a[2] != -7)
                Tick_a[2] = Tick_a[2] + 1;
            // Четвертый тик для Окна ожидания Wait (отмеряем задержку окна, возможн замерить производительность!)
            if (TickRg_a[3] != -7)
                Tick_a[3] = Tick_a[3] + 1;
            // Пятый тик для Проверки ключа лицензии для B52 ( не проверяет если не установлен режим проверки)
            if (TickRg_a[4] != -7)
                Tick_a[4] = Tick_a[4] + 1;
            // Пятый тик для Проверки соединения С БД Терминала
            if (TickRg_a[5] != -7)
                Tick_a[5] = Tick_a[5] + 1;
            // 6 тик для синхронизации времени устройства
            if(TickRg_a[6] != -7)
                Tick_a[6] = Tick_a[6] + 1;
            // 7 тик для удаления файлов в фоне
            if (TickRg_a[7] != -7)
                Tick_a[7] = Tick_a[7] + 1;
        }
        #endregion

        #region Стартовый поток №1
        /// <summary>
        /// Стартовый поток №1
        /// </summary>
        /// <param name="ThreadObj"></param>
        private static void PrgStart_Th(object ThreadObj)
        {
            ConectionDefaultSql();
        }
        #endregion

        #region Тик 0 Поток №2 Часы (Время) на основном окне WorkSpace
        /// <summary>
        /// Тик 0 Поток №2
        /// </summary>
        /// <param name="ThreadObj"></param>
        private static void Time_Th(object ThreadObj)
        {
            
            //TimeSpan current_time = DateTime.Now.TimeOfDay;
            // текущее время в виде строки ToString("HH:mm:ss")
            //DateTime now = new DateTime;
            //DateTime now_ = new DateTime(); 
            //now = DateTime.Now ; 
            //int hour = now_.Hour;

            System.Windows.Application.Current.Dispatcher.Invoke(new Action(delegate()
            {
                MainWindow ConectoWorkSpace_InW = (MainWindow)App.Current.MainWindow;
                ConectoWorkSpace_InW.Time_L_mm.Content = DateTime.Now.ToString("mm");
                ConectoWorkSpace_InW.Time_L_hh.Content = DateTime.Now.ToString("HH");
                // Если время меняется в полночь (смена даты и дня недели
                ConectoWorkSpace_InW.Date.Content = DateTime.Now.ToString(@"ddd  d.MM", CultureInfo.CreateSpecificCulture("ru-RU"));

                  
            }    ));

            // Проверка формы
            //if (System.Windows.Forms.Application.OpenForms["ConectoWorkSpace"] != null)
            //{
            //    //BeginInvoke(new Action(delegate() { button1.Visible = false; }));
            //    String current_time_str = DateTime.Now.ToString("HH:mm");
            //    System.Windows.Forms.Application.OpenForms["ConectoWorkSpace"].BeginInvoke(new Action(delegate() {
            //        System.Windows.Forms.Application.OpenForms["ConectoWorkSpace"].Controls["Time_L"].Text = current_time_str;
            //        System.Windows.Forms.Application.OpenForms["ConectoWorkSpace"].Controls["Time_L"].Refresh();
                        
            //     }));
            //    //ErorDebag("Отладка", 2);
            //}
        }
        #endregion

        #region Тик 1 Поток №3 Интернет и сеть
        /// <summary>
        /// Интернет и сеть 
        /// Тик 1 Поток №3
        /// </summary>
        /// <param name="ThreadObj"></param>
        private static void Internet_Th(object ThreadObj)
        {
            ////ErorDebag("Отладка", 2);
            //// Тик 2 Интернет и сеть (если сеть отключена от интернета то проверка сети ускоряется до 5 секунд)
            //// Усложняем проверкой соединения интерфейса  физически к комутатору пакетов (тобиш к свичу, точке доступа ...)
  
            // Разные способы проверки связи (ConnectionAvailable_ICMP() - быстрее, ConnectionAvailable() - точнее но медленней)
            var result = TickRg_a[1] == 0 ? ConnectionAvailable_ICMP() : ConnectionAvailable();
            //var result = ConnectionAvailable_ICMP();


            // Проверка физического соединения (кабель, WiFi, VPN)
            var DopInfo = "";
            bool Ehernet = false;
            if (!result)
            {
                int[] ResultNetOff = NetOff();
                // Проверка на отключения всех адаптеров (задел шнур, отключилось комутирующие устройство)
                // ResultNetOff[0] == (ResultNetOff[2] + ResultNetOff[3] + ResultNetOff[4] + ResultNetOff[5])
                if (ResultNetOff[1] == 0)
                {
                    // Все адаптеры выключены
                    DopInfo = Environment.NewLine +
                            "..." + Environment.NewLine +
                            "Все подключения " + Environment.NewLine + 
                            "к сети выключены!";
                }
                else
                {
                    // Проверить доступ к шлюзу (проверка физического соединения)
                    var RezGetwai = NetGetwai();
                    if (RezGetwai > 0)
                    {
                        Ehernet = true;
                        DopInfo = RezGetwai == 2 ? Environment.NewLine +
                            "..." + Environment.NewLine +
                            "Не записан шлюз сети, " + Environment.NewLine +
                            "отключен DCHP сервер" : "";
                    }

                }
                /// Результат 0- количество адаптеров , 1- включенных адаптеров, 
                /// 2 - количество выключенных адаптеров  WiFi
                /// 3 - количество выключенных адаптеров Ppp 
                /// 4 - количество выключенных адаптеров Ethernet
                /// 5 - количество выключенных других адаптеров

            }



            System.Windows.Application.Current.Dispatcher.Invoke(new Action(delegate()
            { 
                MainWindow ConectoWorkSpace_InW = (MainWindow)App.Current.MainWindow;
                // Ссылка на объект
                var pic = ConectoWorkSpace_InW.ConectInternet;
                var wP_SysI = ConectoWorkSpace_InW.wP_SysIndicat;
                double[] TopWidth = ConectoWorkSpace_InW.MessageCoordinatInfoPa(1);

                var TextWindows = "";
                int AutoClose = 0;
                // Если состояние не изменилось то ничего не происходит
                if (result && TickMemory_a[1] == 1 || result == false && TickMemory_a[1] == 0 || Ehernet == true && TickMemory_a[1] == 2)
                {
                    // все окей

                }
                else
                {
                    
                    if (result)
                    {

                        pic.Source = new System.Windows.Media.Imaging.BitmapImage(new Uri(@"pack://application:,,,/Conecto®%20WorkSpace;component/Images/earth1_f.png"));
                        TextWindows = "Рабочее место" + Environment.NewLine +
                                        "подключено к интернету" + DopInfo;
                        TickMemory_a[1] = 1;
                        AutoClose = 1;
                    }
                    else
                    {
                        if (Ehernet)
                        {
                            pic.Source = new System.Windows.Media.Imaging.BitmapImage(new Uri(@"pack://application:,,,/Conecto®%20WorkSpace;component/Images/earth3_f.png"));
                            TextWindows = "Подключение к" + Environment.NewLine +
                                            " интернету отсутствует" + DopInfo;
                            TickMemory_a[1] = 2;
                            AutoClose = 1;
                        }
                        else
                        {

                            pic.Source = new System.Windows.Media.Imaging.BitmapImage(new Uri(@"pack://application:,,,/Conecto®%20WorkSpace;component/Images/earth2_f.png"));


                            TextWindows = "Подключение к" + Environment.NewLine +
                                            "интернету отсутствует" + DopInfo;
                            TickMemory_a[1] = 0;
                        }

                    }

                    // создаем Окно сообщение
                    if (WaitMemory_a[0] == 0)
                    {
                        var WinOblakoVerh_ = WindowActive("WinOblakoVerh_Net", TextWindows, AutoClose);
                        WinOblakoVerh_.Owner = ConectoWorkSpace_InW;
                        // размещаем на рабочем столе
                        WinOblakoVerh_.Top = SystemConecto.SizeDWAreaDef_aD[0] + wP_SysI.Margin.Top - (WinOblakoVerh_.Height - 58);
                        WinOblakoVerh_.Left = SystemConecto.SizeDWAreaDef_aD[1] + TopWidth[1]; //wP_SysI.Margin.Left + pic.Margin.Left - 28
                        // Не активировать окно - не передавать клавиатурный фокус http://msdn.microsoft.com/ru-ru/library/ms748948.aspx
                        WinOblakoVerh_.ShowActivated = false;
                        WinOblakoVerh_.Show(); // не показываем модальным (модальное окно не закрывается методом Close, а также не освобождает ресурсы метод fWait.Dispose(); )
                        //=== !Передать активность в основное окно пока не придумал
                        // ConectoWorkSpace_InW.Focus();
                    }
                    else
                    {
                        // Збрасываем память зачем?
                        // TickMemory_a[1] = 1;
                    }
                }
            }));
            
            // Переключатель режимов
            TickRg_a[1] = TickRg_a[1] == 0 ? 1 : 0 ;

            //-----------------------------------
        }
        #endregion

        /// <summary>
        /// Тик 3 Поток №4
        /// </summary>
        /// <param name="ThreadObj"></param>
        private static void BDActive_Th(object ThreadObj)
        {


        }

        #region Тик 4 Поток №5 Ключ B52
        /// <summary>
        /// Тик 4 поток №5
        /// </summary>
        /// <param name="ThreadObj"></param>
        private static void KeyB52Active(object ThreadObj)
        {
            // ErorDebag("Запустили");
            var PortActive =  NetScan(IPAddress.Parse("192.168.1.23"), 3182);

            System.Windows.Application.Current.Dispatcher.Invoke(new Action(delegate()
            {
                MainWindow ConectoWorkSpace_InW = (MainWindow)App.Current.MainWindow;
                // Ссылка на объект
                var pic = ConectoWorkSpace_InW.KeyB52;
                var wP_SysI = ConectoWorkSpace_InW.wP_SysIndicat;
                double[] TopWidth = ConectoWorkSpace_InW.MessageCoordinatInfoPa(2);

                if (PortActive[3182] == "True")
                {
                    pic.Visibility = System.Windows.Visibility.Collapsed;
                    ConectoWorkSpace_InW.numberElementsInfoPa(-1);

                }
                else
                {
                    pic.Visibility = System.Windows.Visibility.Visible;
                    ConectoWorkSpace_InW.numberElementsInfoPa(1);
                    //-----------------------------------
                    var TextWindows = "Ключ безопасности" + Environment.NewLine +
                            "отключен от сети!";
                    var AutoClose = 0;

                    // создаем Окно сообщение
                    //WinOblakoVerh WinOblakoVerh_ = new WinOblakoVerh(TextWindows, AutoClose); 
                    //WinOblakoVerh_.Name = "WinOblakoVerh_KeyB52";
                    // создаем Окно сообщение
                    if (WaitMemory_a[0] == 0)
                    {
                        var WinOblakoVerh_ = WindowActive("WinOblakoVerh_KeyB52", TextWindows, AutoClose);
                        WinOblakoVerh_.Owner = ConectoWorkSpace_InW;
                        // размещаем на рабочем столе
                        //MessageBox.Show(SystemConecto.SizeDWAreaDef_aD[0].ToString() + "|" + pic.Margin.Top.ToString() + "|" + WinOblakoVerh_.Height.ToString());

                        WinOblakoVerh_.Top = SystemConecto.SizeDWAreaDef_aD[0] + wP_SysI.Margin.Top - (WinOblakoVerh_.Height - 58);
                        WinOblakoVerh_.Left = SystemConecto.SizeDWAreaDef_aD[1] + TopWidth[1];  //wP_SysI.Margin.Left + pic.Margin.Left - 38
                        // Не активировать окно - не передавать клавиатурный фокус
                        WinOblakoVerh_.ShowActivated = false;
                        WinOblakoVerh_.Show(); // не показываем модальным (модальное окно не закрывается методом Close, а также не освобождает ресурсы метод fWait.Dispose(); )

                    }
                    else
                    {
                        // Збрасываем память
                    }
                }

            }));


            #region Код для Windows.Forms
            // ErorDebag("Получили Ответ");
           // Ссылка на объект
           //var pic = System.Windows.Forms.Application.OpenForms["ConectoWorkSpace"].Controls["KeyB52"] as System.Windows.Forms.PictureBox;
           
           //if (PortActive[3182] == "True")
           //{
           //    System.Windows.Forms.Application.OpenForms["ConectoWorkSpace"].BeginInvoke(new Action(delegate()
           //    {
           //        pic.Visible = false;

           //    })); 

           //}
           //else
           //{
           //    System.Windows.Forms.Application.OpenForms["ConectoWorkSpace"].BeginInvoke(new Action(delegate()
           //    {
           //        pic.Visible = true;

           //    }));
           //    //ErorDebag("");
           //    //-----------------------------------
           //    var TextWindows = "Ключ безопасности \n отключен от сети!";
           //    var AutoClose = 0;
           //    // Вывести окно 
           //    // Окно выводится по правилам: - вывести и закрыть окно при подключении к сети интернет (TickMemory_a[1]=1)
           //    // - Если окно открыто то мы его закрываем берем от туда текст и в среднем увеличиваем высоту окна на 21 px на одну строчку текста
           //    if (System.Windows.Forms.Application.OpenForms["WinOblakoVerh"] != null)
           //    {
           //        var Text_old = System.Windows.Forms.Application.OpenForms["WinOblakoVerh"].Controls["Text_L"].Text;
           //        // Формируем двух ярусный текст вывода
           //        TextWindows = Text_old + "\n...\n" + TextWindows;
           //        System.Windows.Forms.Application.OpenForms["ConectoWorkSpace"].BeginInvoke(new Action(delegate()
           //        {
           //            System.Windows.Forms.Application.OpenForms["WinOblakoVerh"].Close();

           //        }));
           //    }

           //    System.Windows.Forms.Application.OpenForms["ConectoWorkSpace"].BeginInvoke(new Action(delegate()
           //    {

           //        WinOblakoVerh WinOblakoVerh_ = new WinOblakoVerh(TextWindows, AutoClose); // создаем
           //        System.Windows.Forms.Application.OpenForms["ConectoWorkSpace"].AddOwnedForm(WinOblakoVerh_);
           //        //pic
           //        // размещаем на рабочем столе
           //        WinOblakoVerh_.Top = pic.Top - WinOblakoVerh_.Height;
           //        WinOblakoVerh_.Left = (pic.Left + pic.Width / 2) - 38;
           //        WinOblakoVerh_.Show(); // не показываем модальным (модальное окно не закрывается методом Close, а также не освобождает ресурсы метод fWait.Dispose(); )
           //    }));

           //    //--------------------------------------





           //}
           //System.Windows.Forms.Application.OpenForms["ConectoWorkSpace"].BeginInvoke(new Action(delegate()
           //{
           //    pic.Refresh();

            //}));
            #endregion


            TickMemory_a[4] = 0;
        }
        #endregion

        #region Тик 5 Поток №6 БД Терминала
        /// <summary>
        /// Тик 5 поток №6
        /// </summary>
        /// <param name="ThreadObj"></param>
        private static void BdActiveTerminal(object ThreadObj)
        {
            // ErorDebag("Запустили");
            var PortActive = NetScan(IPAddress.Parse("192.168.43.1"), 3050);

            System.Windows.Application.Current.Dispatcher.Invoke(new Action(delegate()
            {
                MainWindow ConectoWorkSpace_InW = (MainWindow)App.Current.MainWindow;
                // Ссылка на объект
                var pic = ConectoWorkSpace_InW.Bd_Terminal;
                var wP_SysI = ConectoWorkSpace_InW.wP_SysIndicat;
                double[] TopWidth = ConectoWorkSpace_InW.MessageCoordinatInfoPa(3);

                if (PortActive[3050] == "True")
                {
                    pic.Visibility = System.Windows.Visibility.Collapsed;
                    ConectoWorkSpace_InW.numberElementsInfoPa(-1);

                }
                else
                {
                    pic.Visibility = System.Windows.Visibility.Visible;
                    ConectoWorkSpace_InW.numberElementsInfoPa(1);

                    //-----------------------------------
                    var TextWindows = "Терминал отключен \n от БД в сети!";
                    var AutoClose = 0;

                    // создаем Окно сообщение
                    if (WaitMemory_a[0] == 0)
                    {
                        var WinOblakoVerh_ = WindowActive("WinOblakoVerh_BD", TextWindows, AutoClose);
                        WinOblakoVerh_.Owner = ConectoWorkSpace_InW;
                        // размещаем на рабочем столе
                        WinOblakoVerh_.Top = SystemConecto.SizeDWAreaDef_aD[0] + wP_SysI.Margin.Top - (WinOblakoVerh_.Height - 58);
                        WinOblakoVerh_.Left = SystemConecto.SizeDWAreaDef_aD[1] + TopWidth[1]; //wP_SysI.Margin.Left - 38
                        // Не активировать окно - не передавать клавиатурный фокус
                        WinOblakoVerh_.ShowActivated = false;
                        WinOblakoVerh_.Show(); // не показываем модальным (модальное окно не закрывается методом Close, а также не освобождает ресурсы метод fWait.Dispose(); )
                    }
                    else
                    {
                        // Збрасываем память
                    }
                }

            }));

            TickMemory_a[5] = 0;
        }
        #endregion

        #region Тик 6 Поток №7 Синхронизация времени

        /// <summary>
        /// Тик 6 Поток №7
        /// </summary>
        /// <param name="ThreadObj"></param>
        private static void Synhro_Time(object ThreadObj)
        {
            SystemConectoTimeServer.ClientTime();

        }    

        #endregion

        #region Тик X Поток №8

        #endregion

        #region Создания окон уведомлений для нижних кнопок проверки состояний (Сеть, Ключь, БД)
        /// <summary>
        /// Сформировать новое информационное окно
        /// </summary>
        /// <param name="NameWin"></param>
        /// <param name="TextWindows"></param>
        /// <param name="AutoClose"></param>
        /// <returns></returns>
        public static Window WindowActive(string NameWin, string TextWindows="", int AutoClose=0)
        {
            // Вывести окно 
            // Окно выводится по правилам: - вывести и закрыть окно при подключении к сети интернет (TickMemory_a[1]=1)
            // - Если окно открыто то мы его закрываем берем от туда текст -> и создаем образ текста в новом окне сверху
            //
            // {идея не получилась}(и в среднем увеличиваем высоту окна на 21 px на одну строчку текста)
            
            // Проверка какое окно открыто 0 - все закрыты
            var WinAllOpen = "";
            var WinOblakoVerh_Net = SystemConecto.ListWindowMain("WinOblakoVerh_Net");
            var WinOblakoVerh_KeyB52 = SystemConecto.ListWindowMain("WinOblakoVerh_KeyB52");
            var WinOblakoVerh_BD = SystemConecto.ListWindowMain("WinOblakoVerh_BD");
            var WinOblakoVerh_USBHDD = SystemConecto.ListWindowMain("WinOblakoVerh_USBHDD"); // Оноже системное окно об устройствах

            // Проверка открыто ли окно которое открывается 
            bool WinOpen = false;

            // Окно по умолчанию
            if (SystemConecto.ListWindowMain(NameWin) != null)
            {
                WinOpen = true;
            }

            //switch (NameWin)
            //{
            //    case ("WinOblakoVerh_Net"):
            //        if (WinOblakoVerh_Net != null)
            //        {
            //            WinOpen = true;
            //        }
            //        break;
            //    case ("WinOblakoVerh_KeyB52"):
            //        if (WinOblakoVerh_KeyB52 != null)
            //        {
            //            WinOpen = true;
            //        }
            //        break;

            //    case ("WinOblakoVerh_BD"):
            //        if (WinOblakoVerh_BD != null)
            //        {
            //            WinOpen = true;
            //        }
            //        break;
            //    case ("WinOblakoVerh_USBHDD"):
            //        if (WinOblakoVerh_USBHDD != null)
            //        {
            //            WinOpen = true;
            //        }
            //        break;
            //}

            // Якщо вікно за умовчанням не відчинино, перевіряємо, чи відчинені інші вікна
            // зачиняємо їх та копіюємо текст повідомлення у вікні
            if (!WinOpen)
            {

                TextBlock Message_ = null;
                double TopWindow = 0;
                Window WinOblakoVerh_NetInfo = SystemConecto.ListWindowMain("WinOblakoVerh_NetInfo"),
                        WinOblakoVerh_KeyB52Info = SystemConecto.ListWindowMain("WinOblakoVerh_KeyB52Info"),
                        WinOblakoVerh_BDInfo = SystemConecto.ListWindowMain("WinOblakoVerh_BDInfo"),
                        WinOblakoVerh_USBHDDInfo = SystemConecto.ListWindowMain("WinOblakoVerh_USBHDDInfo"); ;
                if (WinOblakoVerh_Net != null)
                {
                    WinAllOpen = "WinOblakoVerh_NetInfo";
                    TopWindow = WinOblakoVerh_Net.Top;
                    Message_ = (TextBlock)LogicalTreeHelper.FindLogicalNode(WinOblakoVerh_Net, "MessageText");
                    WinOblakoVerh_Net.Close();
                }
                else
                {
                    if (WinOblakoVerh_KeyB52 != null)
                    {
                        WinAllOpen = "WinOblakoVerh_KeyB52Info";
                        TopWindow = WinOblakoVerh_KeyB52.Top;
                        Message_ = (TextBlock)LogicalTreeHelper.FindLogicalNode(WinOblakoVerh_KeyB52, "MessageText");
                        WinOblakoVerh_KeyB52.Close();
                    }
                    else
                    {
                        if (WinOblakoVerh_BD != null)
                        {
                            WinAllOpen = "WinOblakoVerh_BDInfo";
                            TopWindow = WinOblakoVerh_BD.Top;
                            Message_ = (TextBlock)LogicalTreeHelper.FindLogicalNode(WinOblakoVerh_BD, "MessageText");
                            WinOblakoVerh_BD.Close();
                        }
                        else
                        {
                            if (WinOblakoVerh_USBHDD != null)
                            {
                                WinAllOpen = "WinOblakoVerh_USBHDDInfo";
                                TopWindow = WinOblakoVerh_USBHDD.Top;
                                Message_ = (TextBlock)LogicalTreeHelper.FindLogicalNode(WinOblakoVerh_USBHDD, "MessageText");
                                WinOblakoVerh_USBHDD.Close();
                            }

                        }
                    }
                }

                var TextWindowsInfo = "";
                Window CloseTo = null, CloseWan = null;

                // Якщо повідомлення знайденно у інших вікнах
                if (Message_ != null)
                {
                    // Определение высоты инормационных окон (не более двух)
                    var TopWindowTo = 0;
                    if(WinOblakoVerh_NetInfo!=null)
                    {
                        // Окно есть и расположенно
                        if (WinOblakoVerh_NetInfo.Top < TopWindow - 126)
                        {
                            // Размещен на втором уровне (закрыть окно первого уровня)
                            CloseTo = WinOblakoVerh_NetInfo;
                            //MessageBox.Show("NetInfo");

                        }
                        else
                        {
                            TopWindowTo = 125;
                            CloseWan = WinOblakoVerh_NetInfo;
                        }
                    }
                    if(WinOblakoVerh_KeyB52Info!=null)
                    {
                        // Окно есть и расположенно
                        if (WinOblakoVerh_KeyB52Info.Top < TopWindow - 126)
                        {
                            // Размещен на втором уровне (закрыть окно первого уровня)
                            CloseTo = WinOblakoVerh_KeyB52Info;
                            //MessageBox.Show("KeyB52Info");

                        }
                        else
                        {
                            TopWindowTo = 125;
                            CloseWan = WinOblakoVerh_KeyB52Info;
                        }
                    }
                    if (WinOblakoVerh_BDInfo!=null)
                    {
                        //MessageBox.Show(WinOblakoVerh_BDInfo.Top.ToString());
                        // Окно есть и расположенно
                        if (WinOblakoVerh_BDInfo.Top < TopWindow - 126)
                        {
                            // Размещен на втором уровне (закрыть окно первого уровня)
                            CloseTo = WinOblakoVerh_BDInfo;
                            //MessageBox.Show("BDInfo");
                        }
                        else
                        {
                            TopWindowTo = 125;
                            CloseWan = WinOblakoVerh_BDInfo;

                        }
                    }
                    if (WinOblakoVerh_USBHDDInfo != null)
                    {
                        //MessageBox.Show(WinOblakoVerh_BDInfo.Top.ToString());
                        // Окно есть и расположенно
                        if (WinOblakoVerh_USBHDDInfo.Top < TopWindow - 126)
                        {
                            // Размещен на втором уровне (закрыть окно первого уровня)
                            CloseTo = WinOblakoVerh_USBHDDInfo;
                            //MessageBox.Show("BDInfo");
                        }
                        else
                        {
                            TopWindowTo = 125;
                            CloseWan = WinOblakoVerh_USBHDDInfo;

                        }
                    }
                    
                    if (CloseWan !=null && CloseTo!=null)
                    {
                        //MessageBox.Show(CloseWan.Top.ToString() + CloseTo.Top.ToString());
                        // Размещен на втором уровне (закрыть окно первого уровня)
                        CloseWan.Top = CloseTo.Top;
                        CloseTo.Close();
                        TopWindowTo = 0;
                    }


                    TextWindowsInfo = Message_.Text;
                
                    MainWindow ConectoWorkSpace_InW = (MainWindow)App.Current.MainWindow;
                    // Створюємо інше вікно з повідомленням
                    WinOblakoVerh WinOblakoVerh_Info = new WinOblakoVerh(TextWindowsInfo, 0, 1); // создаем AutoClose
                    WinOblakoVerh_Info.Name = WinAllOpen;
                    WinOblakoVerh_Info.Owner = ConectoWorkSpace_InW;
                    WinOblakoVerh_Info.Top = TopWindow - 125 - TopWindowTo; // отнять высоту окна и промежуток межу окнами
                    WinOblakoVerh_Info.Left = -12;
                    // Не активировать окно - не передавать клавиатурный фокус
                    WinOblakoVerh_Info.ShowActivated = false;
                    WinOblakoVerh_Info.Show();
                    // Разместить...
                }

            }
            // Формируем двух ярусный текст вывода
            //TextWindows = Text_old + "\n...\n" + TextWindows;
            // 
            // Создать информационное окно без ссылки на информационную икону

            

            // Если окно по умолчанию не создано создать и передать ссылку на окно
            if (!WinOpen)
            {
                WinOblakoVerh WinOblakoVerh_ = new WinOblakoVerh(TextWindows, AutoClose); // создаем 
                WinOblakoVerh_.Name = NameWin;
                return WinOblakoVerh_;
            }


            // Вернуть ссылку на Окно по умолчанию
            return SystemConecto.ListWindowMain(NameWin);
            //switch (NameWin)
            //{
            //    case ("WinOblakoVerh_Net"):

            //        return WinOblakoVerh_Net;
            //    case ("WinOblakoVerh_KeyB52"):

            //        return WinOblakoVerh_KeyB52;

            //    case ("WinOblakoVerh_BD"):

            //        return WinOblakoVerh_BD;
            //}

            // return null;
        }
        #endregion


        //------------------------------


        /// <summary>
        /// Изменение состояния тиков для выполняемых задач №1
        /// </summary>
        /// <param name="NumberTick">Номер тика</param>
        /// <param name="CountTick">Количество тиков</param>
        public static void TickTask1 (int NumberTick, int CountTick)
        {
            if (Tick_a.Length > NumberTick)
            {
                // Отсутствуют проверки макс тиков
                // Tick_a - Временные отметки для выполнения кода
                // CountTick = -1 выключить
                Tick_a[NumberTick] = CountTick;
            }
        }
        /// <summary>
        /// Изменение предыдущего состояния тиков
        /// </summary>
        /// <param name="NumberTick">Номер тика</param>
        /// <param name="CountTick">состояние</param>
        public static void TickMemory1(int NumberTick, int MemoryTick)
        {
            if (TickMemory_a.Length > NumberTick)
            {
                // Отсутствуют проверки макс тиков
                TickMemory_a[NumberTick] = MemoryTick;
            }
        }
        /// <summary>
        /// Изменение состояния режима Wait для Вывода сообщений на рабочий стол
        /// </summary>
        /// <param name="NumberTick">Номер тика</param>
        /// <param name="CountTick">Количество тиков</param>
        public static void WaitTaskWindow(int Set)
        {
            WaitMemory_a[0] = Set;
        }
        #endregion

        #region Окно Wait в отдельном потоке -Thread

        public static void WinWaitStart()
        {
            RenderInfo Arguments03 = new RenderInfo() { };
            Thread thStartTimer03 = new Thread(WinWait_Th);
            thStartTimer03.SetApartmentState(ApartmentState.STA);
            thStartTimer03.IsBackground = true; // Фоновый поток
            thStartTimer03.Start(Arguments03);

        }
        public static void WinWait_Th(object ThreadObj)
        {
            
            // Процедура требующия окна wait
            WaitMessage WindowWait = new WaitMessage();
            //WindowWait.Owner = ConectoWorkSpace_InW;  //AddOwnedForm(OblakoNizWindow);
            // Не активировать окно - не передавать клавиатурный фокус
            WindowWait.ShowActivated = false;
            // Отображаем не показываем модальным 
            // (модальное окно не закрывается методом Close, а также не освобождает ресурсы метод .Dispose(); )
            WindowWait.Show();

            
            // Поток основного окна
            //System.Windows.Application.Current.Dispatcher.Invoke(new Action(delegate()
            //{
            //    // Ссылка на объект
            //    MainWindow ConectoWorkSpace_InW = (MainWindow)App.Current.MainWindow;
            //    //ConectoWorkSpace_InW.Cursor = System.Windows.Input.Cursors.AppStarting;

            //}));
            
            // Для Форм
            //System.Windows.Forms.Application.OpenForms["ConectoWorkSpace"].BeginInvoke(new Action(delegate()
            //{
            //    Wait fWait = new Wait(); // создаем
            //    System.Windows.Forms.Application.OpenForms["ConectoWorkSpace"].AddOwnedForm(fWait);
            //    fWait.Show(); // не показываем модальным (модальное окно не закрывается методом Close, а также не освобождает ресурсы метод fWait.Dispose(); )
            //}));

        }
        public static void WinWait_Close()
        {
            // Закрыть окно
            System.Windows.Application.Current.Dispatcher.Invoke(new Action(delegate()
            {

                // Ссылка на объект
                MainWindow ConectoWorkSpace_InW = (MainWindow)App.Current.MainWindow;

                var Window = SystemConecto.ListWindowMain("WaitMessageW");
                if (Window != null)
                {
                    Window.Close();
                }

                ConectoWorkSpace_InW.Cursor = System.Windows.Input.Cursors.Cross;

                // Для форм
                //System.Windows.Forms.Application.OpenForms["ConectoWorkSpace"].BeginInvoke(new Action(delegate()
                //{
                //    if (System.Windows.Forms.Application.OpenForms["Wait"] != null)
                //    {
                //        System.Windows.Forms.Application.OpenForms["Wait"].Close();
                //    }
                //}));
            }));
        }
        #endregion

        #region Прочее

        // Частичная копия объекта
        public SystemConecto ShallowCopy()
        {
            return (SystemConecto)this.MemberwiseClone();
        }


        public void CompressUtility()
        {
            // Сжатие данных
        }
        public void ExtractUtility()
        {
            // Извлечение сжатых данных

        }


        #endregion

        #region Заметки на полях
        //        //Обновляем DataGrid
        //if (addDataGrid != null)
        //{
        //    string[] s = { IPClassAddres.IPAddres, host, pingReply.RoundtripTime.ToString(),string.Format("{0}",SNMP) };
        //    addDataGrid(s);
        //}
        #endregion

        // ---------------- New 

        #region Выполнить задачу в отдельном потоке -Thread
        /// <summary>
        /// Структура данных для многопотоковой среды (передача аргументов)
        /// </summary>
        //struct RenderInfo
        //{
        //    public string argument1 { get; set; }
        //    public int    argument2 { get; set; }
        //    public string argument3 { get; set; }
        //}


        //public static void StartNewThread()
        //{
        //    // Передача параметров в виде структуры в другой поток
        //    // RenderInfo Arguments03 = new RenderInfo() { argument1 = SystemConecto.NetworkPC["1" + "_IP"] };
        //    // Thread thStartTimer03 = new Thread(StartWork);
        //    // thStartTimer03.SetApartmentState(ApartmentState.STA);
        //    // thStartTimer03.IsBackground = true; // Фоновый поток
        //    // thStartTimer03.Start(Arguments03);

        //}

        //public static void StartWork(object ThreadObj)
        //{
        //    
            // Разбор аргументов
            //RenderInfo arguments = (RenderInfo)ThreadObj;
            //var ips_s = IPAddress.Parse(arguments.argument1);   // Айпи адресс
            
            /// Тело потока

        //}

        #endregion


        // Версия 1.2
        #region Короткий путь для ОС Windows. Сокращение имен директорий и файлов к формату 8.3. 

        [DllImport("kernel32.dll", CharSet = CharSet.Auto)]
        public static extern int GetShortPathName([MarshalAs(UnmanagedType.LPTStr)]string path, [MarshalAs(UnmanagedType.LPTStr)]
StringBuilder shortPath, int shortPathLength);


        /// <summary>
        /// Короткий путь для ОС Windows (исправление ошибки пробела в пути указанного в ручную),
        /// альтернатива var Puth_ = Path.Combine(@"D:\!Project\SDK ZGuard\", "ZGuard.dll");
        /// ссылка using System.IO;
        /// Подходит для приложений выпоняемых из командной строки.
        /// Не рекомендуется использовать!
        /// </summary>
        /// <param name="path">Путь</param>
        /// <returns>Короткий путь</returns>
        public static string GetShortPathName(string path)
        {
            // Определить версию ОС
            
            // Использую функцию Windows GetShortPathName из kernel32.dll
            System.Text.StringBuilder sb = new System.Text.StringBuilder(250); // Размер пути
            int res = GetShortPathName(path, sb, sb.Capacity);
            // ========== Ошибка формирования ....  (Разработка - RAB)
            // return (res > 0 && res < sb.Capacity) ? sb.ToString() : null;
            return sb.ToString();
        }

        #endregion
        
        #region Серийный номер материнской партии в Windows

        private void SNBoardDevice()
        {
            ManagementObjectSearcher searcher = new ManagementObjectSearcher
           ("SELECT Product, SerialNumber FROM Win32_BaseBoard");

            ManagementObjectCollection information = searcher.Get();
            foreach (ManagementObject obj in information)
            {
                foreach (PropertyData data in obj.Properties)
                {
                    MessageBox.Show(string.Format("{0} = {1}", data.Name, data.Value));
                }
                // listBoxControl1.Items.Add(string.Format("{0} = {1}", data.Name, data.Value));              
            }
        }

        #endregion

        
        #region Определение версии ОС [version 1.4]

        /// <summary>
        /// Код версии Windows знает или нет системный код об исключениях в ОС
        /// Environment.Is64BitOperatingSystem - Определяет разрядность ОС
        /// </summary>
        public static int OSWMI = WMIOS();

        public static int WMIOS()
        {

            // Environment.Is64BitOperatingSystem - Определяет разрядность ОС
            
            // Environment.GetEnvironmentVariable("NameVirable") - считывает значение переменной  в среде в виде текста.

            // Так можно определить какой тип исполняемой среды (по умолчанию 32 bit IntPtr.Size == 4)
            //if (IntPtr.Size == 8)
            //{
                // Только для 64bit
            //}

            string Version = "";  

            string[] Version_ = Environment.OSVersion.Version.ToString().Split('.');
           
            int MaxLen = Version_.Length>3 ? 3 :  Version_.Length;
            for (int i = 0; i < MaxLen; i++)
            {
                Version = Version + (Version == "" ? "" : ".") + Version_[i];
            }

            #region Альтернатива Environment.OSVersion.Version с помощью WMI (Если исключение то WMI выключен)
            // ======== Нюанс состоит в том, что нужно проверять состояние службы WMI для получения результатов
            //string winos = "Select Name, Version from Win32_OperatingSystem";
            //ManagementObjectSearcher mos =
            //    new ManagementObjectSearcher(winos);

            //// Отладка разных свойст системы ОС Windows Память, Сетевая группа, Сервис Пак ОС Windows
            //// MessageBox.Show(string.Format("[Свойство {0}]", mos.ToString()));

            //// Из запроса берем только первую запись
            //foreach (ManagementObject mo in mos.Get())
            //{
            //    Version = mo["Version"].ToString();
            //}
                //// Отладка елементов
                ////foreach (PropertyData data in mo.Properties)
                ////{
                ////    MessageBox.Show(string.Format("[Свойство {0} = {1}]", data.Name, data.Value));
                ////}
           #endregion
            switch (Version)
            {
                case "6.1.7600":
                    // Windows 7 Ultimate 32-bit ломаная

                return 10;
                case "6.1.7601":
                // Windows 7 начальная    
                // break;    


                return 1;
                case "6.0.6002":
                // Windows Vista Home Premium  
                // break;    
                return 2;
                case "5.1.2600":
                // Windows XP Home SP3
                // break;    
                return 3;
                case "6.2.9200":

                // Windows 8
                return 4;
            }
            ErorDebag("Имя ОС: " + Version, SystemConecto.ReleaseCandidate == "Release" ? 0 : 2);
            return -1;

        }
        #endregion

        #region Поиск устройства в списке Windows

        /// <summary>
        /// Поиск устройства в списке Устройств Windows (Адмнистрирование устройств - Панель управления)
        /// </summary>
        /// <param name="NameDevice">Имя устройства</param>
        public static void ListDeviceDriverName(string NameDevice)
        {

            int NumberDivece = 0;

            // Модемы
            //string query = "SELECT * FROM Win32_POTSModem";
            // COM-PORT - SerialPort
            //string query = "SELECT * FROM Win32_SerialPort";
            // Список всех подключаемых устройств по Plag and Play - Win32_PnPEntity - Условие {WHERE ConfigManagerErrorCode = 0}
            // работает без ошибок 
            string query = "SELECT * FROM Win32_PnPEntity";

            string[] ModemObjects = new string[250];
            ManagementObjectSearcher searcher = new ManagementObjectSearcher(query);
            foreach (ManagementObject obj in searcher.Get())
            {
                // Win32_POTSModem
                // MessageBox.Show(obj["Name"].ToString() + "(" + obj["AttachedTo"].ToString() + ")");
                // Win32_PnPEntity
                //var test =obj["Availability"].ToString();
                //string source = "MY INFORMATION TO CHECK";            
                //if(source.IndexOf("information", StringComparison.OrdinalIgnoreCase) >= 0)
                if (obj["Name"].ToString().IndexOf("Z397")>=0)
                {
                    // Присутствует код ConfigManagerErrorCode 28 - Устройство обнаруженно но не установленно
                     // Описание http://msdn.microsoft.com/en-us/library/windows/desktop/aa394353%28v=vs.85%29.aspx

                    // Установка драйвера
                    // Появляется еще одно устройство USB Serial Port Поставщик устройства FTDI

                    // Результат полсе установки
                    // Устройство Контроллер Z397-Guard USB<->485
                    // Устройство Порт Z397-Guard USB<->485 [Serial port] (COM 29)
                    // Присутствует код ConfigManagerErrorCode 0 - Ошибок нету


                    NumberDivece++;
                    // MessageBox.Show("Устройство №" + NumberDivece  + ". " + obj["Name"].ToString() + " / " + obj["ConfigManagerErrorCode"].ToString());
                    // MessageBox.Show(string.Format("{0} = {1}", obj["Name"].ToString(), obj["Value"].ToString()));
                    foreach (PropertyData data in obj.Properties)
                    {
                        
                       // MessageBox.Show(string.Format("[Свойство {0} = {1}]", data.Name, data.Value, NumberDivece));
                    }
                }
                else
                {
                    //Отсутствует
                }
                if (obj["Description"].ToString().IndexOf("Guard") >= 0)
                {
	                 //Присутствует   
                   // MessageBox.Show("/" + obj["Description"].ToString());
                }
                else
                {
                    //Отсутствует
                }
               



                
              
            }

            // objectQuery = new ObjectQuery("SELECT * FROM Win32_PnPEntity WHERE ConfigManagerErrorCode = 0");

            //ManagementObjectSearcher comPortSearcher = new ManagementObjectSearcher(connectionScope, objectQuery);

            //using (comPortSearcher)

            //{



        }

        #endregion

        #region Поиск семных носителей
        /// <summary>
        /// Поиск семных носителей и сбор короткой дополнительной информации
        /// </summary>
        /// <param name="NameDevice"></param>
        /// <returns></returns>
        public static Dictionary<string, string[]> ListDeviceRemovable()
        {
            Dictionary<string, string[]> List = new Dictionary<string, string[]>();
            // 1. Приложение ищит на всех дисках папку Conecto в папке Conecto\pack - обязательно наличие файла pack.xml)
            DriveInfo[] DriveList = DriveInfo.GetDrives();
            // string[] DriveList = Environment.GetLogicalDrives(); // Вариант не очень информативный
            //for (int i = 0; i < DriveList.Length; i++)
            // d.DriveType - тип устройства, проверить все портативные носители - if (drive.DriveType == DriveType.Removable)

            foreach (DriveInfo d in DriveList)
            {
                if (d.DriveType == DriveType.Removable)
                {
                    // Проверка готовности устройства
                    if (d.IsReady == true)
                    {
                        List.Add(d.Name, new string[2] { d.RootDirectory.ToString(), d.AvailableFreeSpace.ToString() });
                    }
                }
            }
            return List;
        }

        #endregion

        #region Удаление временных файлов (можно выполнять в потоке)
        //        Чистка файлов в фоне!!!! 
        //Для ускорения работы системы. Файлы удляются потом.
        //Не хватает списка файлов которые можно удалить.
        //Чтобы не удалить файлы которые нужны в данный момент,
        //предлагаю сделать открытый xml файл, в котором отмечать то, что можно удалить.
        /// <summary>
        /// Чистка файлов в фоне.  Для ускорения работы системы. Файлы удляются потом.
        /// Поток
        /// </summary>
        public static void DeliteFileTmp()
        {


        }

        #endregion

        #region Определения старта приложения в Windows

        /// <summary>
        /// Определения старта приложения в Windows
        /// [true загрузка с винчестера; false - из сети]
        /// </summary>
        /// <returns>true загрузка с винчетера; false - из сети</returns>
        public static bool IsLocalStartProgram()
        {
            DirectoryInfo dir = new DirectoryInfo(AppDomain.CurrentDomain.BaseDirectory);
            return (from d in DriveInfo.GetDrives()
                    where string.Compare(dir.Root.FullName, d.Name, StringComparison.OrdinalIgnoreCase) == 0
                    select (d.DriveType != DriveType.Network)
                    ).FirstOrDefault();
        }

        #endregion

        #region Выключение экрана монитора для экономии електроэнергии

        /// <summary>
        /// 
        /// </summary>
        /// <param name="hWnd"></param>
        /// <param name="hMsg"></param>
        /// <param name="wParam"></param>
        /// <param name="lParam"></param>
        /// <returns></returns>
        [DllImport("user32.dll")]
        private static extern int SendMessage(int hWnd, int hMsg, int wParam, int lParam);

        /// <summary>
        /// Handle - активного окна в Windows
        /// </summary>
        /// <returns></returns>
        [DllImport("user32.dll")]
        public static extern IntPtr GetForegroundWindow();

        public static void MonitorPOWER()
        {
            int WM_SYSCOMMAND = 0x0112;
            int SC_MONITORPOWER = 0xF170;
            var handleActive = GetForegroundWindow();

            //MessageBox.Show(string.Format("{0} = {1}", NameOP, allProc_.ToString()));

            SendMessage(handleActive.ToInt32(), WM_SYSCOMMAND, SC_MONITORPOWER, 2); //this.Handle.ToInt32()

        }

           


        #endregion

        #region Сервер часового пояса и синхронизации времени

        // SystemConectoTimeServer


        #endregion

        #region Создать директорию в специальной папке Windows

        

                //System.IO.Directory.CreateDirectory(System.Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\dir");
                //System.IO.Directory.CreateDirectory(System.Environment.GetFolderPath(Environment.SpecialFolder.MyComputer) + "\\dir");
                //System.IO.Directory.CreateDirectory(System.Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\dir");
                //System.IO.Directory.CreateDirectory(System.Environment.GetFolderPath(Environment.SpecialFolder.MyMusic) + "\\dir");
                //System.IO.Directory.CreateDirectory(System.Environment.GetFolderPath(Environment.SpecialFolder.MyPictures) + "\\dir");
                //System.IO.Directory.CreateDirectory(System.Environment.GetFolderPath(Environment.SpecialFolder.Personal) + "\\dir");
                //System.IO.Directory.CreateDirectory(System.Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles) + "\\dir");
                //System.IO.Directory.CreateDirectory(System.Environment.GetFolderPath(Environment.SpecialFolder.Programs) + "\\dir");
                //System.IO.Directory.CreateDirectory(System.Environment.GetFolderPath(Environment.SpecialFolder.Recent) + "\\dir");
                //System.IO.Directory.CreateDirectory(System.Environment.GetFolderPath(Environment.SpecialFolder.SendTo) + "\\dir");
                //System.IO.Directory.CreateDirectory(System.Environment.GetFolderPath(Environment.SpecialFolder.StartMenu) + "\\dir");
                //System.IO.Directory.CreateDirectory(System.Environment.GetFolderPath(Environment.SpecialFolder.Startup) + "\\dir");
                //System.IO.Directory.CreateDirectory(System.Environment.GetFolderPath(Environment.SpecialFolder.System) + "\\dir");
                //System.IO.Directory.CreateDirectory(System.Environment.GetFolderPath(Environment.SpecialFolder.Templates) + "\\dir");



        #endregion

        #region Найти файл доступа на носителе без ввода пароля (для отладки, управления терминалами, а также как средсвто индивидуальной идентификации)




        #endregion


        #region Чтение защищенного SQL файла

        /// <summary>
        /// Чтение защищенного SQl файла 
        /// </summary>
        /// <param name="PutchFile1">Путь к файлу по приоритету важный</param>
        /// <param name="PutchFile2">менее важный</param>
        /// <returns>Текст файла и его наличие в одной из директории</returns>
        public static string[] ReadFile_SqlPass(string PutchFile1, string PutchFile2="")
        {
            string[] FileTxt = new string[2] { "0", "" };

            // SystemConecto.PutchApp + @"config\user\1cshluz.xml"
            string PuthFileRead = PutchFile1;

            string PuthFileRead_start = PutchFile2;
            
            string ReadFile = "";
            string PuthActive =  "0";

            if(SystemConecto.File_(PuthFileRead, 5)){
                ReadFile = PuthFileRead;
                PuthActive = "1";
            }else{
                if (SystemConecto.File_(PuthFileRead_start, 5))
                {
                    ReadFile = PuthFileRead_start;
                    PuthActive = "2";
                }

            }

            if (ReadFile.Length > 0)
            {
                try
                {
                    // Читаем файл
                    FileTxt[1] = SystemConecto.DecryptTextToFile(ReadFile);
                    FileTxt[0] = PuthActive;
                }
                catch (Exception ex)
                {
                    SystemConecto.ErorDebag("При загрузке файла " + ReadFile.ToString() + ", возникло исключение: " + Environment.NewLine +
                       " === Message: " + ex.Message.ToString() + Environment.NewLine +
                       " === Exception: " + ex.ToString(), 1);
                }
            }
            return FileTxt;
        }

        #endregion

        #region Запись защищенного SQL файла

        /// <summary>
        /// Чтение защищенного SQl файла 
        /// </summary>
        /// <param name="PutchFile1">Путь к файлу по приоритету важный</param>
        /// <param name="txtString">Текст записываемый в файл</param>
        /// <returns></returns>
        public static bool WriteFile_SqlPass(string PutchFile1, string txtString)
        {
            bool Write = false;

            // SystemConecto.PutchApp + @"config\user\1cshluz.xml"
            string PuthFileRead = PutchFile1;

            string WriteFile = PutchFile1;

            if (WriteFile.Length > 0)
            {
                try
                {
                    // Пишем в файл
                    SystemConecto.EncryptTextToFile(PutchFile1, txtString);

                }
                catch (Exception ex)
                {
                    SystemConecto.ErorDebag("При записи файла " + WriteFile.ToString() + ", возникло исключение: " + Environment.NewLine +
                       " === Message: " + ex.Message.ToString() + Environment.NewLine +
                       " === Exception: " + ex.ToString(), 1);
                }
            }
            return Write;
        }

        #endregion


        #region Запуск и обслуживание серверов, а также их служб (Состояние)

        /// <summary>
        /// Служба запускается в отдельном потоке
        /// </summary>
        public static void StartStatusServer()
        {
            // Передача параметров в виде структуры в другой поток
            RenderInfo Arguments03 = new RenderInfo() { };
            //Thread thStartTimer03 = new Thread();
            //thStartTimer03.SetApartmentState(ApartmentState.STA);
            //thStartTimer03.IsBackground = true; // Фоновый поток
            //thStartTimer03.Start(Arguments03);

        }


        #endregion

        #region Создание каталогов по заданому пути v.1.2
        /// <summary>
        /// Создание каталогов по заданому пути аналог DIR_ - она быстрее
        /// позваляет определить ошибку когда в пути есть каталог имя которого совпадает с файлом
        /// </summary>
        /// <param name="dir_"></param>
        public static bool CheckDirectory(string dir_) 
        {
            string[] dir = dir_.Split('\\');

            string dirchek = dir[0]; // диск начало пути

            for (int ind = 1; ind < dir.Length; ind++)
            {
                dirchek = dirchek + @"\" + dir[ind];
                // Определение ошибки совпадения названия файла и Директории
                if (File_(dirchek, 5))
                {
                    ErorDebag("Нельзя создать директорию, так как по указанному пути: " + dirchek.ToString() +" ; уже есть файл с таким именем!");
                    return false;
                }
                if (!DIR_(dirchek))
                {
                    return false;
                }
            }

            //string[] dirs = Directory.GetDirectories(dir_); // список всех txt файлов в директории C:\temp
            //for (int i = 0; i < dirs.Length; i++)
            //{
            //    //рекурсивный вызов сканирования для подпапок
            //    ScanFileDirectory(dirs[i]);  //, ref DTCursorZapit
            //}
            return true;
        }

        #endregion


        #region Разработка кодирования символов здесь часть кода еще в записи в CSV и в какомто класе
        /// <summary>
        /// Перекодировка UTF8 to 1251
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        private string UTF8ToWin1251(string source)
        {

            Encoding utf8 = Encoding.GetEncoding("utf-8");
            Encoding win1251 = Encoding.GetEncoding("windows-1251");

            byte[] utf8Bytes = utf8.GetBytes(source);
            byte[] win1251Bytes = Encoding.Convert(utf8, win1251, utf8Bytes);
            source = win1251.GetString(win1251Bytes);
            return source;

        } 

        private string Win1251ToUTF8(string source)
        {

            Encoding utf8 = Encoding.GetEncoding("utf-8");
            Encoding win1251 = Encoding.GetEncoding("windows-1251");

            byte[] utf8Bytes = win1251.GetBytes(source);
            byte[] win1251Bytes = Encoding.Convert(win1251, utf8, utf8Bytes);
            source = win1251.GetString(win1251Bytes);
            return source;

        }

        /// <summary>
        /// Перокодировка UTF8 CP1251 (UTF-16LE)
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string UTF8ToCP1251(string value)
        {
            byte[]
                ba = Encoding.GetEncoding(1251).GetBytes(value);

            char[]
                ca = new char[Encoding.UTF8.GetDecoder().GetCharCount(ba, 0, ba.Length)];

            Encoding.UTF8.GetDecoder().GetChars(ba, 0, ba.Length, ca, 0);

            return (new string(ca));
        }

        /// <summary>
        /// Перокодировка Unicode CP1251
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string UnicodeToCP1251(string value)
        {
            byte[]
                ba = Encoding.GetEncoding(1251).GetBytes(value);

            char[]
                ca = new char[Encoding.Unicode.GetDecoder().GetCharCount(ba, 0, ba.Length)];

            Encoding.Unicode.GetDecoder().GetChars(ba, 0, ba.Length, ca, 0);

            return (new string(ca));
        }
        #endregion

        // SystemConectoTimeServer.TimeServer("StartServer");

        //---------------- End Class

        #region Класс авторизации пользователя

        public static class AutorizUser
        {
            #region Авторизация пользователя (ей)
            /// <summary>
            /// Логин авторизации
            /// </summary>
            public static string LoginUserAutoriz = null;
            /// <summary>
            /// Значение зашифровано в памяти
            /// </summary>
            public static string PaswdUserAutoriz = null;
            /// <summary>
            /// Код ошибки авторизации
            /// </summary>
            public static string idErrorAutoriz = null;
            /// <summary>
            /// ID код авторизируемого
            /// </summary>
            public static string IDUserAutoriz = null;
            /// <summary>
            /// Логин предыдущий авторизации
            /// </summary>
            public static string LoginUserAutoriz_Back = null;
            /// <summary>
            /// ID код предыдущего авторизируемого
            /// </summary>
            public static string IDUserAutoriz_Back = null;
            /// <summary>
            /// Тип авторизации: администрирование, запуск приложения с авторизацией (Имя метода запускемого при событиях авторизации)
            /// </summary>
            public static string TypeAutoriz = "None";
            //====================================================
            /// <summary>
            /// Авторизация пользователя
            /// </summary>
            /// <param name="Login">логин</param>
            /// <param name="Pasw">пароль</param>
            /// <param name="Type_">Тип авторизации: 0 - ввод с устройства ввода; 1 - чтение ключа флешки; 2 - чтение карточки из БД;</param>
            /// <returns>Истина,Ложь</returns>
            public static int Autoriz(string Login, string Pasw, int Type_ = 0)
            {
                // Супер пользователь
                if (aParamApp["Autorize_Admin-Conecto"] == Login)
                {
                    // Проверка пароля
                    if (aParamApp["Autorize-pass-admin-Conecto"] == Pasw)
                    {
                        LoginUserAutoriz_Back = LoginUserAutoriz;
                        LoginUserAutoriz = "Autorize_Admin-Conecto";
                        if (Autirize[5] == "")
                        {
                            return 0;
                        }
                    }

                }
                else
                {
                    // Пользователь Панели - Пароль (Пин код)
                    if (Login == "UserPanel")
                    {
                        // Если БД подключенна запрос к БД сотрудников
                        //MessageBox.Show("Тест");
                        // Проверка ИТ администратора
                        if (aParamApp["Autorize_pass-admin-IT"] == Pasw)
                        {
                            LoginUserAutoriz_Back = LoginUserAutoriz;
                            LoginUserAutoriz = "Autorize_pass-admin-IT";
                            if (Autirize[5] == "")
                            {
                                return 0;
                            }
                        }

                    }
                    else
                    {

                        // Администратор ИТ
                        if (aParamApp["Autorize_Admin-IT"] == Login)
                        {
                            // Проверка ИТ администратора
                            if (aParamApp["Autorize_pass-admin-IT"] == Pasw)
                            {
                                LoginUserAutoriz_Back = LoginUserAutoriz;
                                LoginUserAutoriz = "Autorize_pass-admin-IT";
                                if (Autirize[5] == "")
                                {
                                    return 0;
                                }
                            }

                        }
                    }


                }
                // Проверка авторизации по серверу учетных записей

                // Если БД подключенна, запрос к БД сотрудников
                // Поиск по логину и паролю
                switch (Autirize[5])
                {
                    case "FB":

                        // под паролем администратора проверка соединения
                        DBConecto.ParamStringServerFB[1] = Login.ToLower();
                        DBConecto.ParamStringServerFB[2] = Pasw;
                        DBConecto.ParamStringServerFB[3] = Autirize[6];
                        DBConecto.ParamStringServerFB[4] = Autirize[7];
                        DBConecto.ParamStringServerFB[5] = Autirize[8];



                        // Проверка метода адаптации пароля
                        if (DBConecto.ParamStringServerFB[1] != "sysdba" && Autirize[9].Length > 0)
                        {
                            System.Reflection.MethodInfo loadAppEvents = typeof(AppforWorkSpace).GetMethod(Autirize[9], new Type[] { typeof(String) });
                            if (loadAppEvents != null)
                            {

                                MainWindow ConectoWorkSpace_InW = (MainWindow)App.Current.MainWindow;
                                // SystemConecto.ErorDebag("LoadAppEvents_" + IdApp, 2);
                                object Return = loadAppEvents.Invoke(ConectoWorkSpace_InW, new object[] { Pasw });
                                if (Return.ToString().Length > 0)
                                {
                                    DBConecto.ParamStringServerFB[2] = Return.ToString();
                                }
                            }
                        }

                        int MessageError = DBConecto.DBopenFBConectionMemory(DBConecto.StringServerFB());
                        if (MessageError == 0)
                        {
                           
                            // Запись авторизации
                            LoginUserAutoriz = DBConecto.ParamStringServerFB[1]; // Login;
                            //LoginUserAutoriz_Back = LoginUserAutoriz;
                            PaswdUserAutoriz = DBConecto.ParamStringServerFB[2]; //Pasw;

                            DBConecto.DBcloseFBConectionMemory();
                            return 0;
                        }


                        //DBConecto.ErrorBDEnd

                        DBConecto.DBcloseFBConectionMemory();
                        return MessageError;



                }



                return -1;
            }
            #endregion



            #region Декодирование пароля 
        /// <summary>
        /// Декодирования текстового поля для исключения просмотра в памяти (сохраняем значение после декодирования, в вебе имитация хеширования) для паролей в БД В52
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string decodeStringB(String str)
        {
                string Result = "";
                int i = 2;

                while ((i) <= (int)(str.Length / 2))
                {
                    char i_4 = str[i - 1];
                    char i_3 = str[i - 1 - 1];
                    char i_2 = str[str.Length - i + 2 - 1];
                    char i_1 = str[str.Length - i + 1 - 1];

                    Result = Result + str[i - 1] + str[i - 1 - 1] + i_2 + i_1;
                    i = i + 2;
                }
                if (i == ((int)(str.Length / 2) + 1))
                    Result = Result + str[str.Length - i + 2 - 1] + str[i - 1 - 1];
                if (((str.Length) % 2) > 0)
                    Result = Result + str[(int)(str.Length / 2) + 1 - 1];



                #region Deplhi
                //function PairRotate(str: String): String;
                //var
                //    i: Integer;
                //begin
                //    Result := '';
                //    i := 2;
                //    while (i <= (Length(str) div 2)) do begin
                //        Result := Result + str[i] + str[i-1] + str[Length(str)-i+2] + str[Length(str)-i+1];
                //        inc(i, 2);
                //    end;
                //    if (i = ((Length(str) div 2)+1)) then
                //        Result := Result + str[Length(str)-i+2] + str[i-1];
                //    if ((Length(str) mod 2) > 0) then
                //        Result := Result + str[(Length(str) div 2)+1];
                //end;
                #endregion


                #region Java
                //String res = "";
                //char[] bytes = str.ToCharArray();

                //int i = 1;
                //while (i < (int)(bytes.Length / 2))
                //{
                //    res += bytes[i] + bytes[i - 1] + bytes[bytes.Length - i] + bytes[bytes.Length - i - 1];
                //    i += 2;
                //}
                //if (i == (int)(bytes.Length / 2))
                //    res += bytes[bytes.Length - i] + bytes[i - 1];
                //if ((bytes.Length % 2) > 0)
                //    res += bytes[(int)(bytes.Length / 2)];
                // return res;
                #endregion

                return Result;
        }

        #endregion




        }
        #endregion

    }
    
    #region  Тестовые процедуры (пока не пригодились)
    public static class ElementsWindow
    {
        /// <summary>
        /// Получить перечисление дочерних визуальных объектов (рекурсивное перечисление)
        /// </summary>

        public static IEnumerable<FrameworkElement> GetTree(FrameworkElement e)
        {
            if (e == null)
                return new FrameworkElement[] { };
            int total = VisualTreeHelper.GetChildrenCount(e);
            return new FrameworkElement[] { e }

                       .Concat(
                            Enumerable.Range(0, total - 1)
                            .SelectMany(x => GetTree(VisualTreeHelper.GetChild(e, x)

                                        as FrameworkElement)))
                       .Where(x => x != null);
        }

        // <summary>
        // усложненный вариант поиска элемента по имени
        // сначала вызывается стандартный метод
        // если он ничего не вернет, то ищем сами
        // </summary>

        public static object FindChildByName(FrameworkElement e, string name)
        {

            //return ;
            return e.FindName(name);

                //?? ElementsWindow.GetTree(e).FirstOrDefault(x => x.Name == name);

        }
    

    
    }

    #endregion

    #region Выгрузка таблицы в файл
    public static class DataTableExtensions
    {
        /// <summary>
        /// Экспорт таблицы в файл
        /// </summary>
        /// <param name="dataTable"></param>
        /// <param name="filePath"></param>
        /// <param name="TitleCSV"></param>
        /// <param name="?"></param>
        public static void WriteToFile(this DataTable dataTable, string filePath, string[] TitleCSV, string TypeFile="CSV utf8")
        {
            StringBuilder fileContent = new StringBuilder();

            // С рашифровкой заголовка
            if (TitleCSV.Count() > 0)
            {
                // Совпадения порядка
                var ind = 0;
                foreach (var col in dataTable.Columns)
                {
                    fileContent.Append(TitleCSV[ind] + ";"); //col.ToString()
                    ind++;
                }
            }
            else
            {
                foreach (var col in dataTable.Columns)
                {
                    fileContent.Append(col.ToString() + ";");
                }
            }
            

            // Последний символ удаляем
            fileContent.Remove(fileContent.Length - 1, 1);
            fileContent.AppendLine();
            // Альтернатива
            // fileContent.Replace(";", System.Environment.NewLine, fileContent.Length - 1, 1);



            foreach (DataRow dr in dataTable.Rows)
            {

                foreach (var column in dr.ItemArray)
                {
                   
                   //fileContent.Append("\"" + column.ToString() + "\";");
                   string ReplaceSpec = column.ToString().Replace(";", "&#160"); // замена символа пробелом
                   fileContent.Append(ReplaceSpec + ";");
                }

                // Последний символ удаляем
                fileContent.Remove(fileContent.Length - 1, 1);
                fileContent.AppendLine();
                // Альтернатива
                // fileContent.Replace(";", System.Environment.NewLine, fileContent.Length - 1, 1);
            }

            // Определим на первое время запись только одного файла
            // Для многих
            // for(){}
            if (TypeFile == "CSV utf8")
            {
                System.IO.File.WriteAllText(filePath.Replace(".csv", " utf.csv"), fileContent.ToString());
            }
            if (TypeFile == "CSV WIN1251")
            {
                Encoding win1251 = Encoding.GetEncoding("windows-1251");

                System.IO.File.WriteAllText(filePath.Replace(".csv", " win1251.csv"), fileContent.ToString(), win1251);

            }
            


            

        }
        
    }
    #endregion



}
