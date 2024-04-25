using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
// --- Process
using System.Diagnostics;
// --- Puth
using System.IO;
using System.Windows;
// Регулярные выражения
using System.Text.RegularExpressions;

namespace ConectoWorkSpace
{
    class DllWork
    {
        
       //  неуправляемой библиотеки (unmanaged dll) запускается через - DllImport
        // managed - управляемая библиотека
        // перечень файлов библиотек http://www.dll.ru/dll/S/32.html

        // Компоненты на .net вроде регистрируются так:
        //regasm.exe ИмяКомпоненты.dll /codebase

        //  Dll не регистрируют. с помощью 
        //  Регистрируют com-сервер
        //  искать путь к dll в разделе HKEY_CLASSES_ROOT\CLSID...\InprocServer32 


        // - Рекомендуют - Чтобы не было проблем со сборкой мусора лучше в функции передавать HandleRef.
        /*
         * [DllImport("kernel32.dll", CharSet=CharSet.Auto)]
         *  private static extern bool FreeLibrary(HandleRef hModule);
         * 
         **/

        /// <summary>
        /// Для загрузки DLL - dllFilePath не должна быть константной - так что можно прочитать путь из реестра
        /// </summary>
        /// <param name="dllFilePath">Путь к файлу с именем файла</param>
        /// <param name="hFile">use IntPtr.Zero</param>
        /// <param name="dwFlags">Что случилось во время загрузки DLL
        /// <para>LOAD_LIBRARY_AS_DATAFILE</para>
        /// <para>DONT_RESOLVE_DLL_REFERENCES</para>
        /// <para>LOAD_WITH_ALTERED_SEARCH_PATH</para>
        /// <para>LOAD_IGNORE_CODE_AUTHZ_LEVEL</para>
        /// </param>
        /// <returns>Pointer to loaded Dll</returns>
        [DllImport("kernel32.dll", CharSet=CharSet.Unicode)]
        private static extern IntPtr LoadLibraryEx(string dllFilePath, IntPtr hFile, uint dwFlags); //, uint dwFlags

        /// <summary>
        /// Чтобы выгрузить библиотеку
        /// </summary>
        /// <param name="dllPointer">Pointer to Dll witch was returned from LoadLibraryEx</param>
        /// <returns>If unloaded library was correct then true, else false</returns>
        [DllImport("kernel32.dll", CharSet=CharSet.Unicode)]
        public extern static bool FreeLibrary(HandleRef dllPointer);
        // public extern static bool FreeLibrary(IntPtr dllPointer);

        /// <summary>
        /// Чтобы получить указатель на функцию из загруженных DLL
        /// </summary>
        /// <param name="dllPointer">Pointer to Dll witch was returned from LoadLibraryEx</param>
        /// <param name="functionName">Имя функции которую необходимо вызвать</param>
        /// <returns>Pointer to function</returns> 
        // Кодировка - CharSet=CharSet.Unicode
        [DllImport("kernel32.dll", CharSet=CharSet.Unicode)]
        public extern static IntPtr GetProcAddress(HandleRef dllPointer, string functionName);
        //public extern static IntPtr GetProcAddress(IntPtr dllPointer, string functionName);

        /// <summary>
        /// Это необходимо для загрузки указанного DLL файла (функция API)
        /// </summary>
        /// <param name="dllFilePath">Dll путь к файлу</param>
        /// <returns>Pointer to loaded dll</returns>
        /// <exception cref="ApplicationException">
        /// when loading dll will failure
        /// </exception>
        public static IntPtr LoadWin32Library(string dllFilePath)
        {
            uint LOAD_WITH_ALTERED_SEARCH_PATH = 0x00000008;  // http://msdn.microsoft.com/en-us/library/windows/desktop/ms684179%28v=vs.85%29.aspx

            System.IntPtr moduleHandle = LoadLibraryEx(dllFilePath, IntPtr.Zero, LOAD_WITH_ALTERED_SEARCH_PATH); //, LOAD_WITH_ALTERED_SEARCH_PATH
            if (moduleHandle == IntPtr.Zero)
            {
                // Получаем последнии ошибки DLL
                int errorCode = Marshal.GetLastWin32Error();
                SystemConecto.ErorDebag(string.Format("Обнаруженна ошибка при загрузке Dll : {0}, error - {1}", dllFilePath, errorCode));
                //throw new ApplicationException(
                //    string.Format("Обнаруженна ошибка при загрузке Dll : {0}, error - {1}", dllFilePath, errorCode)
                //    );


            }

            return moduleHandle;
        }


        // Последняя вещь заключается в создании делегата вызова функции

        //// Делегат должен иметь тот же параметр, что и вызываемая функция (из dll)

        // public delegate RETURN_TYPE DllFunctionDelegate(int a, string b);


        /// Пример использования:
        //public static RETURN_TYPE functionName(int a, string b)
        //{
        //    if (myDll == IntPtr.Zero)
        //        InitializeMyDll();
        //    IntPtr pProc = DllWork.GetProcAddress(myDll, "CallingFunctionNameFromCallingDllFile");
        //    CPVdelegate cpv = (CPVdelegate)Marshal.GetDelegateForFunctionPointer(pProc, typeof(CPVdelegate));
        //    // Now i'm calling delegate, with is calling function from dll file
        //    return cpv(nOper, hWnd, out dwData);
        //}

        /// Пример:  delegate uint MessageBoxFunc(IntPtr hWnd, string Text, string Caption, int Options);        
        /// Пример использования:
        //MessageBoxFunc msgbox = (MessageBoxFunc)Marshal.GetDelegateForFunctionPointer(hFunc, typeof(MessageBoxFunc));
        //uint res = msgbox(this.Handle, "Всё работает!", "Не поверите", 0);
        //MessageBox.Show("Result: " + res.ToString());


        //// Now if you want to call dll function from program code use this
        //// for ex. you want to call c++ function RETURN_TYPE CallingFunctionNameFromCallingDllFile(int nID, LPSTR lpstrName);

        //RETURN_TYPE ret;
        //ret = functionName(2, "name");


        /// <summary>
        /// Регистрация COM сервера в ОС Windows (Разработка - RAB)
        /// Register and Unregister a DLL /s /u
        /// ActiveX Controls Using 
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static string[] RegSrv32(string path, string parametrs = "/s", string parametrsto = "") // /s /u
        {
            Process pr = new Process();

            pr.StartInfo.FileName = "regsvr32.exe";

            pr.StartInfo.Arguments = parametrs+" \""+ path + "\" "+ parametrsto;       //Path.Combine(path, parametrs);

            pr.StartInfo.UseShellExecute = false;
            pr.StartInfo.CreateNoWindow = true;
            pr.StartInfo.RedirectStandardOutput = true;

            pr.Start();

            pr.WaitForExit();
            pr.Close();

            return new string[] { };


 
  

        }

        /// <summary>
        /// Регистрация Программ как служб (Разработка - RAB)
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static string[] RegSrvice(string path, string parametrs = "") 
        {
            Process pr = new Process();

            pr.StartInfo.FileName = path ;

            pr.StartInfo.Arguments = parametrs;       //Path.Combine(path, parametrs);

            pr.StartInfo.UseShellExecute = false;
            pr.StartInfo.CreateNoWindow = true;
            pr.StartInfo.RedirectStandardOutput = true;

            pr.Start();

            pr.WaitForExit();
            pr.Close();

            return new string[] { };

        }



        /// <summary>
        /// Имена (пути) используемых приложением DLL-библиотек в текущем 
        /// DllWork.ListDllMemory();
        /// </summary>
        /// <returns></returns>
        public static void ListDllMemory()
        {
            foreach (var item in AppDomain.CurrentDomain.GetAssemblies())
            {
                MessageBox.Show(string.Format("{0} = {1}", item.Location, item.FullName));

            }


        }


        #region Завершение аварийно сервис или служб
        // Разработка для авто закрытия
        /// <summary>
        /// Проверка статуса служб 
        /// sc.exe queryex <grdsrv>
        /// </summary>
        /// <returns></returns>
        public static string StatusService(string nameservice)
        {
            string StatusServ = "";
            Process pr = new Process();

            pr.StartInfo.FileName = "sc.exe";

            pr.StartInfo.Arguments = " queryex " + nameservice;       //Path.Combine(path, parametrs);

            pr.StartInfo.UseShellExecute = false;
            pr.StartInfo.CreateNoWindow = true;
            pr.StartInfo.RedirectStandardOutput = true;
            
            //Encoding enc = Encoding.GetEncoding("cp1251");

            try
            {
                pr.StartInfo.StandardOutputEncoding = Encoding.GetEncoding(866);    //1251
                pr.Start();

                // получаем ответ запущенного процесса
                StreamReader srIncoming = pr.StandardOutput;
                // выводим результат

                // Анализ
                // с пмомщью указанного регулярного выражения.
                // [SC] EnumQueryServicesStatus:OpenService: ошибка: 1060:
                //Указанная служба не установлена.
                Regex PointTextChek = new Regex(@".*(EnumQueryServicesStatus)+.*(1060)+");
                //если да то возвращаем true, если нет то false
                string ParamValue = srIncoming.ReadToEnd();

                if (PointTextChek.IsMatch(ParamValue))
                {
                    StatusServ = "0";
                }
                else
                {
                    StatusServ = "1";
                }

                SystemConecto.ErorDebag(ParamValue + Environment.NewLine +"End++");



                pr.WaitForExit();

            }
            catch (Exception ServEx)
            {
                // выводим результат
                SystemConecto.ErorDebag(ServEx.Message + Environment.NewLine + "End++");
            }
            pr.Close();



            return StatusServ;
        }

//        Creating a Service:
//sc.exe create PayCalcService binPath = "C:\Program Files\PaymentCalculation\paycalc.exe" DisplayName= "PaymentCalculationService"

//Starting a Service:
//sc.exe start PaymentCalculationService

//Stopping a Service:
//sc.exe stop PaymentCalculationService

//Deleting a Service:
//sc.exe delete PaymentCalculationService



        //        C:\WINDOWS\system32>sc delete grdsrv
        //[SC] DeleteService: ошибка: 1072:

        //Указанная служба была отмечена для удаления.


        //C:\WINDOWS\system32>sc.exe queryex grdsrv

        //Имя_службы: grdsrv
        //        Тип                : 10  WIN32_OWN_PROCESS
        //        Состояние          : 2  START_PENDING
        //                                (NOT_STOPPABLE, NOT_PAUSABLE, IGNORES_SHUTDOWN)
        //        Код_выхода_Win32   : 0  (0x0)
        //        Код_выхода_службы  : 0  (0x0)
        //        Контрольная_точка  : 0x0
        //        Ожидание           : 0x7d0
        //        ID_процесса        : 19504
        //        Флаги              :

        //C:\WINDOWS\system32>taskkill /pid 19504 /f
        //Успешно: Процесс, с идентификатором 19504, успешно завершен.

        //C:\WINDOWS\system32>sc.exe queryex grdsrv
        //[SC] EnumQueryServicesStatus:OpenService: ошибка: 1060:

        //Указанная служба не установлена.

        #endregion





        /// <summary>
        /// Загрузка плагинов dll (тоже самое, что и подключение библиотек)
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        //public Plugin LoadPlugin(String path)
        //{
        //    if (!System.IO.File.Exists(path))
        //        return null;
        //    Assembly a = Assembly.LoadFile(System.IO.Path.GetFullPath(path));
        //    //ЧТО ТУТ ПИСАТЬ? ДЛЯ ТОГО, ЧТОБЫ СБОРКА БЫЛА ЗАГРУЖЕНА?
        //    //AppDomain.CurrentDomain.AppendPrivatePath(System.IO.Path.GetDirectoryName(path));
        //    //AppDomain.CurrentDomain.Load(a.GetName());                       
        //    foreach (Type t in a.GetTypes())
        //    {
        //        if (t.BaseType == Type.GetType("NikotiN.Utils.PluginSystem.Plugin"))
        //        {
        //            Plugin p = (Plugin)Activator.CreateInstance(t);
        //            if ((p != null) && (p.Load(Cmd)))
        //            {
        //                Plugin.Add(p);
        //                return p;
        //            }
        //        }
        //    }
        //    return null;
        //}

        #region Подключение дополнительного каталога для dll (функция API)


        [DllImport("Kernel32.dll", CallingConvention = CallingConvention.StdCall)]
        public static extern bool SetDllDirectory(String lpPathName);

        static void Main()
        {
            SetDllDirectory(SystemConecto.PutchApp + @"bin\dll");
        }


        //=================== Важно, есть гипотиза, что библиотека необязательно грузится из указанной директории 
        //====== Но можно отследить версию!

        //[DllImport("Kernel32.dll", CallingConvention = CallingConvention.StdCall)]
        //public static extern bool SetDllDirectory(String lpPathName);

        // Сбросить поиск dll до умолчания
        //SetDllDirectory(null);

        // Добавить dll - путь кдиректории
        //SetDllDirectory(SystemConecto.PutchApp + @"bin\dll");


        //Каталог из которого загружается приложение.
        //Каталог, определенный параметром lpPathName.
        //Системный каталог. Используйте функцию GetSystemDirectory, чтобы получить путь к этому каталогу. Имя этого каталога - System32.
        //16-разрядный системный каталог. Нет никакой функции, которая получает путь к этому каталогу, но он ищется. Имя этого каталога является System.
        //Каталог Windows. Используйте функцию GetWindowsDirectory, чтобы получить путь к этому каталогу.
        //Каталоги, которые перечислены в переменной окружения PATH.

        //Чтобы возвратиться к заданному по умолчанию пути поиска, используемому LoadLibrary и LoadLibraryEx, вызовите SetDllDirectory  с ПУСТО (NULL).

        // =============================== Альтернатива развиваем =========================

        //AppDomainSetup setupInfo = new AppDomainSetup();
        //setupInfo.PrivateBinPath = path;

        // ===================================== Примеры опытов загрузки библиотеки по альтернативному пути
        //  uint LOAD_WITH_ALTERED_SEARCH_PATH = 0x00000008;

        // Альтернативный путь
        // string path = @"D:\!Project\SDK ZGuard\CSharp\Examples\EnumCvts\";

        // Добавить dll - путь кдиректории для DllImport и прочих функций


        //System.IntPtr moduleHandle = LoadLibraryEx(path, IntPtr.Zero, LOAD_WITH_ALTERED_SEARCH_PATH);
        //if (moduleHandle == IntPtr.Zero)
        //{
        //    //  Получаем последнии ошибки DLL
        //    int errorCode = Marshal.GetLastWin32Error();
        //    return;
        //    //   (string.Format("Обнаруженна ошибка при загрузке Dll : {0}, error - {1}", dllFilePath, errorCode));
        //    //throw new ApplicationException(
        //    //    string.Format("Обнаруженна ошибка при загрузке Dll : {0}, error - {1}", @"D:\!Project\SDK ZGuard\CSharp\Examples\EnumCvts\ZGuard.dll", errorCode)
        //    //     );


        //}


        #endregion

    }
        // **************************************************************************************************************************
        // Вот и все, теперь, как использовать эту функцию
        // **************************************************************************************************************************

        //private IntPtr myDll ;

        //public static void InitializeMyDll()
        //{
        //    try
        //    {
        //        myDll = DllWork.LoadWin32Library("path to my dll with file path name");
        //        // Здесь вы можете добавлять, dll проверку версии
        //    }
        //    catch (ApplicationException exc)
        //    {
        //        //MessageBox.Show(exc.Message, "There was an error during dll loading",MessageBoxButtons.OK,MessageBoxIcon.Error);
        //        throw exc;
        //    }
    //}



    /* Вообщем получилось, только я создал CustomClassLibrary, сразу же добавил новый элемент Окно WPF, и вызвал из другого проекта, всё бы хорошо, но не могу теперь разобраться как динамически подгрузить библиотеку и вызвать конструктор, пытаюсь сделать так:

            Код C#

    
            Assembly load = Assembly.LoadFrom(@"C:\Weather.dll");
            MethodInfo Method = load.GetTypes()[0].GetMethod("WeatherWindow");
            Method.Invoke(null, null);
 

            Сам класс окна в Weather.dll

            Код C#
 
            namespace Weather
            {
                /// <summary>
                /// Логика взаимодействия для MainWindow.xaml
                /// </summary>
                public partial class WeatherWindow : Window
                {
                    public WeatherWindow()
                    {
                        InitializeComponent();
                    }
                }
            }
 

            Если добавить в проект ссылку на эту dll и создать окно, то всё работает

            Код C#

 
            WeatherWindow w = new WeatherWindow();
            w.Show();
    */

}
