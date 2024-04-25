using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
// Управление вводом-выводом
using System.IO;
using System.Text;
// локаль операционной системы
using System.Globalization;
// Сжатие
using System.IO.Compression;
// шифрование данных
using System.Security.Cryptography;

using System.Runtime.InteropServices;
// Процессы в ОС Windows
using System.Diagnostics;
using System.Windows;
using System.Windows.Input;
// Ссылка на формс так как использую SendKeys, InputLanguageCollection
// using System.Windows.Forms;
/// Многопоточность
using System.Threading;
using System.Management;

// BitmapImage Предоставляет специализированный BitmapSource, оптимизированный для загрузки изображений с помощью языка разметки XAML
using System.Windows.Media.Imaging;
// ==================================== Используем функции ядра SystemConecto

// Forward declarations
using LUID = System.Int64;
using HANDLE = System.IntPtr;


namespace ConectoWorkSpace
{
    // http://www.pinvoke.net/
    // http://faqs.org.ru/
    
    #region Вставка текста в окно другогой исполняемой программы (*.exe)
    class TextPasteWindow
    {

        #region Пример использования
        // 
        // 

        #endregion


        #region WINAPI

        #region ShowWindow
        /// <summary>
        /// function ShowWindow(hWnd: HWND; nCmdShow: Integer): BOOL; Показывает или прячет окно.<para></para>
        /// hWnd <para></para>
        /// Описатель нужного окна <para></para>
        /// nCmdShow <para></para>
        /// Константа, определяющая, что будет сделано с окном: <para></para>
        /// SW_HIDE <para></para>
        /// SW_SHOWNORMALSW_NORMAL <para></para>
        /// SW_SHOWMINIMIZED <para></para>
        /// SW_SHOWMAXIMIZED <para></para>
        /// SW_MAXIMIZE <para></para>
        /// SW_SHOWNOACTIVATE <para></para>
        /// SW_SHOW <para></para>
        /// SW_MINIMIZE <para></para>
        /// SW_SHOWMINNOACTIVE <para></para>
        /// SW_SHOWNA <para></para>
        /// SW_RESTORE <para></para>
        /// SW_SHOWDEFAULT <para></para>
        /// SW_MAX <para></para>
        /// 
        /// <param name="hWnd"></param>
        /// <param name="nCmdShow"></param>
        /// <returns>returns: BOOL</returns>
        /// </summary>
        [DllImport("user32.dll")]
        public static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);
        #endregion

        #region SetWindowPos - Устанавливает окно в новую позицию (BringWindowToTop(), DeferWindowPos())
        /// <summary>
        /// function SetWindowPos(hWnd: HWND; hWndInsertAfter: HWND; X, Y, cx, cy: Integer; uFlags: UINT): BOOL; stdcall; 
        /// Устанавливает окно в новую позицию
        /// hWnd 
        /// Оптсатель окна 
        /// hWndInsertAfter 
        /// Описатель окна, перед которым в списке Z-Order будет вставлено окно hWnd, или одна из следующих констант: 
        /// HWND_BOTTOM 
        /// Поместить окно на дно списка Z-Order 
        /// HWND_TOP 
        /// Поместить окно на верх списка Z-Order 
        /// X, Y, cx, cy
        /// Соответственно - новые горизонт. , верт. позиции окна (X, Y), а также новая ширина 
        ///  и высота (cx, cy) 
        /// uFlags <para></para>s
        /// Одна или несколько (разделенных OR) следующих констант: <para></para>
        /// SWP_NOSIZE <para></para>
        /// Не изменять размер окна после перемещения (cx, cy игнорируются) <para></para>
        /// SWP_NOZORDER <para></para>
        /// Не изменять положение окна в списке Z-Order<para></para> 
        /// SWP_SHOWWINDOW <para></para>
        /// Сделать окно видимым после перемещения <para></para>
        /// SWP_HIDEWINDOW <para></para>
        /// Спрятать окно после перемещения <para></para>
        /// SWP_NOACTIVATE <para></para>
        /// Не передавать фокус окну после перемещения <para></para>
        /// SWP_NOMOVE <para></para>
        /// Не перемещать окно (игнорируется X, Y)<para></para>
        /// Описание:<para></para>
        /// http://msdn.microsoft.com/ru-ru/library/windows/desktop/ms633545(v=vs.85).aspx <para></para>
        /// </summary>
        /// <param name="hWnd"></param>
        /// <param name="hWndInsertAfter"></param>
        /// <param name="X"></param>
        /// <param name="Y"></param>
        /// <param name="cx"></param>
        /// <param name="cy"></param>
        /// <param name="uFlags"></param>
        /// <returns></returns>
        [DllImport("user32.dll", EntryPoint = "SetWindowPos")]
        public static extern bool SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter, int X, int Y, int cx, int cy, uint uFlags);

        #endregion

        #region Наработки с формой - документация
        // -------------------------------------------------------------------------------
        //[DllImport("user32.dll")] //импорт Api функции
        //private static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);
        ////функция winapi показывает окно
        //[DllImport("user32.dll", EntryPoint = "SetWindowPos")]
        //static extern bool SetWindowPos(int hWnd, int hWndInsertAfter, int X, int Y, int cx, int cy, uint uFlags);
        ////задаем константы
        //private const int SW_SHOWNOACTIVATE = 4; //неактивна
        //private const int HWND_TOPMOST = -1; //поверх всех окон, включая топовые
        //private const uint SWP_NOACTIVATE = 0x0010;

        // ShowWindowAsync(pFoundWindow, 5); //отправляем команду
        // ShowWindow(pFoundWindow, SW_SHOWNOACTIVATE);
        // SetWindowPos(pFoundWindow.ToInt32(), HWND_TOPMOST, Left, Top, Width, Height, SWP_NOACTIVATE);

        //hWnd
        //[in] Handle to the window.
        //nCmdShow
        //[in] Specifies how the window is to be shown. This parameter is ignored the first time an application calls ShowWindow, if the program that launched the application provides a STARTUPINFO structure. Otherwise, the first time ShowWindow is called, the value should be the value obtained by the WinMain function in its nCmdShow parameter. In subsequent calls, this parameter can be one of the following values.
        //SW_FORCEMINIMIZE
        //Windows 2000/XP: Minimizes a window, even if the thread that owns the window is hung. This flag should only be used when minimizing windows from a different thread.
        //SW_HIDE
        //Hides the window and activates another window.
        //SW_MAXIMIZE
        //Maximizes the specified window.
        //SW_MINIMIZE
        //Minimizes the specified window and activates the next top-level window in the Z order.
        //SW_RESTORE
        //Activates and displays the window. If the window is minimized or maximized, the system restores it to its original size and position. An application should specify this flag when restoring a minimized window.
        //SW_SHOW
        //Activates the window and displays it in its current size and position.
        //SW_SHOWDEFAULT
        //Sets the show state based on the SW_ value specified in the STARTUPINFO structure passed to the CreateProcess function by the program that started the application.
        //SW_SHOWMAXIMIZED
        //Activates the window and displays it as a maximized window.
        //SW_SHOWMINIMIZED
        //Activates the window and displays it as a minimized window.
        //SW_SHOWMINNOACTIVE
        //Displays the window as a minimized window. This value is similar to SW_SHOWMINIMIZED, except the window is not activated.
        //SW_SHOWNA
        //Displays the window in its current size and position. This value is similar to SW_SHOW, except the window is not activated.
        //SW_SHOWNOACTIVATE
        //Displays a window in its most recent size and position. This value is similar to SW_SHOWNORMAL, except the window is not actived.
        //SW_SHOWNORMAL
        //Activates and displays a window. If the window is minimized or maximized, the system restores it to its original size and position. An application should specify this flag when displaying the window for the first time.
        // Проверить все одинаковые процессы
        // allProc_[0].;
        // ErorDebag(pFoundWindow.ToString() , 2, 2);
        // -------------------------------------------------------------------------------
        #endregion


        [DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
        public static extern IntPtr GetFocus();

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern IntPtr GetForegroundWindow();

        [DllImport("user32.dll")]   //IntPtr
        public static extern bool SetForegroundWindow(IntPtr hWnd);

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        static extern bool PostMessage(IntPtr hWnd, int Msg, int wParam, int lParam);

        [DllImport("user32.dll")]
        public static extern bool AttachThreadInput(uint idAttach, uint idAttachTo, bool fAttach);

        [DllImport("user32.dll")]
        public static extern uint GetWindowThreadProcessId(IntPtr hWnd, out uint lpdwProcessId);

        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern uint GetCurrentThreadId();

        [DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
        public static extern IntPtr GetKeyboardLayout(int WindowsThreadProcessID);

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        private static extern int GetWindowThreadProcessId(IntPtr handleWindow, out int lpdwProcessID);

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern int LoadKeyboardLayout(string pwszKLID, uint Flags);

        //[DllImport("user32.dll", CharSet = CharSet.Auto)]
        //private static extern IntPtr SendMessage(IntPtr hWnd, uint Msg, int wParam, uint lParam);
        [DllImport("User32.dll", CharSet = CharSet.Auto)]
        public static extern IntPtr SendMessage(IntPtr hWnd, int msg, int wParam, int lParam);

        //[DllImport("user32.dll")]
        //public static extern uint PostThreadMessage(IntPtr hWnd, uint Msg, char wParam, int lParam);

        [DllImport("user32.dll", SetLastError = true)]
        internal static extern IntPtr SetParent(IntPtr hWndChild, IntPtr hWndNewParent);

        #endregion



        #region pasteTextWin
        /// <summary>
        /// Вставка Текста в окно другогой исполняемой программы (*.exe)<para></para>
        /// 
        /// <param name="text">text: Текст, который нужно вставить</param><para></para>
        /// <param name="NameWindowApp">NameWindowApp: Имя окна приложения в котором вводим значение Текста или Титл</param><para></para>
        /// <param name="NameNumbeBox">NameNumbeBox: Имя или номер поля ввода Текста</param><para></para>
        /// <param name="PuthApp">PuthApp: Путь запуска прграммы. CMD - признак отключения проверки наличия файла</param><para></para>
        /// </summary>
        public static void pasteTextWin(string text, string NameWindowApp, string NameNumbeBox = "1", string PuthApp = @"D:\Flagman\FrontOffice.exe") // D:\pizza_boguslav\B52FrontOffice7.exe
        {

            //SystemConecto.ErorDebag("ура-----------------------------------------------" + SetFocusWindow(NameWindowApp).ToString(), 1);
            
            try
            {
                // Проверка запущенного приложения
                if (!SetFocusWindow(NameWindowApp) && ( PuthApp == "CMD" || SystemConecto.File_(PuthApp, 5))) //SystemConecto.SetFocusWindow
                {
                    // Приложение 

                    // Процедура требующия окна wait
                    //Wait fWait = new Wait(); // создаем
                    //AddOwnedForm(fWait);
                    //fWait.Show(); // не показываем модальным (модальное окно не закрывается методом Closeб, а также не освобождает ресурсы метод fWait.Dispose(); )
                    
                    //SystemConecto.WinWaitStart();


                    // В отделном потоке
                    //ProcessStartInfo startInfo = new ProcessStartInfo(PuthApp);
                    ProcessStartInfo psiOpt = new ProcessStartInfo(PuthApp == "CMD" ? PuthApp : System.IO.Path.GetFileName(PuthApp), " "); // @"AcroRd32.exe", mlc_un7z
                    //psiOpt.CreateNoWindow = true;
                    psiOpt.WorkingDirectory = PuthApp == "CMD" ? PuthApp : System.IO.Path.GetDirectoryName(PuthApp);

                    var pr = new Process();
                    pr.StartInfo = psiOpt;
                    //pr.StartInfo.FileName = PuthApp;
                    pr.Start();
                    pr.WaitForInputIdle();

                    Thread.Sleep(1000);

                    // Закрыть поток окна ожидание
                    AppStart.TickTask1(3, -1);
                    //fWait.Close();
                }
                else
                {
                    //SystemConecto.ErorDebag("сброс -----------------------------------------------", 1);
                    if (!SystemConecto.File_(PuthApp, 5) && PuthApp != "CMD")
                    {
                        //Нет программы
                        return;

                    }  
                    

                }
                
                
                //активизируем окно, которое имело фокус
                if (hControl != IntPtr.Zero || SetFocusWindow(NameWindowApp)) // "B52BackOffice", Notepad
                {
                    
                    // sysdba

                    //===========================================
                    // Назвать функцию SetKeyboardLayout(Flags, FLWait, handleWindow)
                    // 0 - для текущего приложения
                    // 1 - для Внешнего с handleWindow
                    // FLWait - 0 -ожидать ответа, игнорировать ответ (асинхронно)
                    //
                    //======================================================================
                    // Определения раскладки текущего приложения
                    // SystemConecto.ErorDebag(GetKeyboardLayoutId(IntPtr.Zero).ToString(), 2);

                    // Очень Важно определить раскладку ввода для приложения
                    // идентификатор языка ввода (прежнее название раскладки клавиатуры) - это параметры которыми оперирует Windows
                    //if (GetKeyboardLayoutId(hControl) == "ENU")
                    //{
                    //    //SystemConecto.ErorDebag("Есть", 2);
                    //}
                    //else
                    //{
                    //}
                    //====================================================

                    uint KLF_ACTIVATE = 0x00000001; // параметр уакзывает если раскладки нет то ее загрузить в ОС
                    int WM_INPUTLANGCHANGEREQUEST = 0x0050; // параметр указывает на смену клавиатуры
                    string Lang_ENG = "00000409";  // 0x0419 Russian // 0x0422 Ukrainian // 0x0409 English
                    SendMessage(hControl, WM_INPUTLANGCHANGEREQUEST, 5, LoadKeyboardLayout(Lang_ENG, KLF_ACTIVATE));
                    Thread.Sleep(200);
                    // PostMessage(hControl, WM_INPUTLANGCHANGEREQUEST, 5, LoadKeyboardLayout(Lang_ENG, KLF_ACTIVATE));
                    
                    // Открыть поток окна в фоне
                     SetWindowPos(hControl, HWND.BOTTOM, 0, 0, 0, 0, SWP.NOSIZE | SWP.NOMOVE);
                    // Открыть поток окна на передний план.
                    // SetForegroundWindow(hControl);



                    //подключаем поиск окна
                    //[DllImport("user32.dll", CharSet = CharSet.Auto)]
                    //public static extern IntPtr FindWindowEx(IntPtr hwndParent, IntPtr hwndChildAfter, string className, string windowName);

                    // IntPtr button = FindWindowEx(progr, IntPtr.Zero, "TButton", "Button1");
                    //ищем в окне calcWnd кнопку класса Button с подписью 1
                    // IntPtr button1 = FindWindowEx(calcWnd, IntPtr.Zero, "Button", "1");

                    //'поиск элементов управления на форме (главное - не перепутать дескрипторы элементов-владельцев)
                    //hwndd = FindWindowEx(hwndt, 0, vbNullString, "") 'Dialog (промежуточное окно)
                    //hwndc = FindWindowEx(hwndd, 0, "ComboBox", "") 'ComboBox
                    //hwndb = FindWindowEx(hwndd, 0, vbNullString, "&Конвертировать") 'кнопка "Конвертировать" (имя кнопки - подсмотрено в том же Spy++)


                    // Смена языка в активном окне своего приложения работает
                    // LoadKeyboardLayout('00000419',0)
                    // ActivateKeyboardLayout(LoadKeyboardLayout('00000419',0),0) - Rus

                    //  private void Form1_Load(object sender, EventArgs e)
                    //{
                    //    HotKeysManager manager = new HotKeysManager();
                    //    manager.AddHotKey(new HotKeyCombination(() => { DESU(); }) { Keys.OemQuestion });
                    //}
 
                    //void DESU()
                    //{
                    //    InputLanguage.CurrentInputLanguage = InputLanguage.FromCulture(new CultureInfo("en-US"));
                          //  InputLanguage.CurrentInputLanguage = InputLanguage.FromCulture(new CultureInfo("ru-RU"));
                    //}


                   // MessageBox.Show("Раскладка!" + GetKeyboardLayoutId(hControl));
           

                    //передаем ему текст посимвольно
                    // int WM_SETTEXT = 0x000C;

                    //  Усовершенствование - SendMessage(hControl, WM_SETTEXT, 0, text);
                    // NewMessage.InteropSetText(hControl, "TLookUpEdit", text);



                    // Обработка по цифровому значению
                    if (Convert.ToInt32(NameNumbeBox) > 0)
                    {
                        // SystemConecto.ErorDebag("Цифра", 2);
                        //SendKeys.SendWait("1");
                        //SendKeys.SendWait("{ENTER}");
                        // SendKeys.Send("K");
                        // SendKeys.Send("K");
                    }
                    else
                    {
                        // Обработка по текстовому значению
                        // SystemConecto.ErorDebag("Текст", 2);
                    }

                    // Определить как параметры
                    LoadKeyboardLayout("00000409",KLF_ACTIVATE);
                    foreach (char ch in text)
                    {

                        //SendKeys.Send("{" + ch + "}"); для FORM
                        //SendKeys.SendWait("{" + ch + "}");
                        System.Windows.Forms.SendKeys.SendWait(ch.ToString()); // для WPF
                        // Отладка
                        //SystemConecto.ErorDebag(ch.ToString() + " /Text", 1);

                        /// Варианты передачи значения другому приложению 
                        
                        ///--- Вариант 1
                        // int WM_CHAR = 0x0102;
                        // PostMessage(hControl, WM_CHAR, ch, 1);
                        // PostMessage(hControl, 0x0C09, 2, 0);
                        // if (PostMessage(hControl, WM_CHAR, ch, 1))
                        // if (PostThreadMessage(hControl, WM_CHAR, ch, 1))

                        //==== ! Описаный выше способ передачи использует функцию PostMessage. У этого способа есть определенные минусы:
                        // - нельзя передать форматированный-rtf текст;
                        // - теоретически нельзя будет передать некоторым приложениям (каким, пока не знаю, но теоретически такое возможно);
                        // - неизвестно, как будет работать в новых операционных системах Microsoft.

                        ///--- Вариант 2
                        // SendKeys.Send("{TAB}");
                        // SendKeys.Send("{" + ch + "}");
                        // SendKeys.Send("{ENTER}");

                        //=== !Обратите внимание, что в этом примере используется SendKeys.SendWait(), а не Send() - это потому, что Send() можно
                        // использовать только из потока, имеющего очередь сообщений. 
                        // Если нужно "нажать" клавишу совместно с каким-то модификатором,
                        // например эмулировать вставку из clipboard'a (CTRL+V)
                        // используйте строку ^V Значения для других модификаторов можно найти в справке по SendKeys.Send().
                        // SendKeys.Send("%{" + Keys.PrintScreen.ToString.ToUpper + "}")
                        // Детально http://msdn.microsoft.com/ru-ru/library/system.windows.forms.sendkeys.send.aspx

                        ///--- Вариант 3 
                        /// Передача через буфер Cntr+C Cntr+V

                        ///--- Вариант 4
                        ///=== ! Важная разработка Нужно точно определить имя поля и попробовать передать ему фокус
                        ///

                        ///--- Вариант 5
                        /// с посмощью WinApi
                        ///[DllImport("user32.dll", SetLastError = true)]
                            //static extern void keybd_event(byte bVk, byte bScan, uint dwFlags, UIntPtr dwExtraInfo);
                            //public static void PressKey(Keys key, bool up) {
                            //    const int KEYEVENTF_EXTENDEDKEY = 0x1;
                            //    const int KEYEVENTF_KEYUP = 0x2;
                            //    if (up) {
                            //        keybd_event((byte) key, 0x45, KEYEVENTF_EXTENDEDKEY | KEYEVENTF_KEYUP, (UIntPtr) 0);
                            //    }
                            //    else {
                            //        keybd_event((byte) key, 0x45, KEYEVENTF_EXTENDEDKEY, (UIntPtr) 0);
                            //    }
                            //}

                            //void TestProc() {
                            //    PressKey(Keys.ControlKey, false);
                            //    PressKey(Keys.P, false);
                            //    PressKey(Keys.P, true);
                            //    PressKey(Keys.ControlKey, true);
                            //}
                    
                    }



                }
                else
                {
                    SystemConecto.ErorDebag("Окно не найдено!");
                }
            }
            catch (Exception error)
            {
                SystemConecto.ErorDebag(error.ToString());
            }
        }

        #endregion


        #region Установка фокуса окна
        public static IntPtr hControl; //хэндл контрола
        /// <summary>
        /// Установка фокуса окна и определение системного хэндла окна приложения<para></para>
        /// 
        /// <param name="NameWinndow">Название окна на которое устанавливаем фокус</param><para></para>
        /// </summary>
        public static bool SetFocusWindow(string NameWinndow, string TimeStart = "")
        {
            hControl = IntPtr.Zero;
            Process[] allProc_ = null;
            // Отладка
            //SystemConecto.ErorDebag("ищем процесс -- " + NameWinndow, 1);

            // получаем хендл Окна похоже на hwnd=FindWindow("HHTaskBar",null);
            // Процессы и окна разные области и методы
            // встречается нулевой Хендел (даже после обновления кеша p.refrech) для процессов без интерфейса Окна ОС их мы исключаем из выбора
            // Проверить окно
            switch (TimeStart)
            {
                case "":
                    allProc_ = Process.GetProcessesByName(NameWinndow).Where(process => process.MainWindowHandle != IntPtr.Zero).ToArray();
                    //allProc_ = Process.GetProcessesByName(NameWinndow);
                    break;

                default:
                    // Очень рисковано погрешность может быть до 3 секунд
                    allProc_ = Process.GetProcessesByName(NameWinndow).Where(process => process.MainWindowHandle != IntPtr.Zero && process.StartTime.ToString() == TimeStart).ToArray();
                    break;

            }
            
            //Process.GetCurrentProcess
            
            //// Отладка процессов
            //Process[] allProc = Process.GetProcesses();
            //foreach (Process currProc in allProc)
            //{
            //    SystemConecto.ErorDebag("найден процесс -- " + currProc.ProcessName.ToString() + "//" + currProc.MainWindowTitle.ToString(), 1);
            //}


            if (allProc_ != null && allProc_.Length >= 1)
            {
                hControl = allProc_[0].MainWindowHandle;
                //hControl = allProc_[0].Handle;

                // Отладка
                // SystemConecto.ErorDebag("найден процесс -- " + allProc_[0].ProcessName.ToString() + "HWD: " + hControl.ToString(), 1);

                // Все  об окнах Попробую перенести в систему
                // http://www.realcoding.net/articles/funktsii-win-api-dlya-raboty-s-oknami.html

                // Уточнение работы функции SetForegroundWindow в ОС
                // http://www.shloemi.com/2012/09/solved-setforegroundwindow-win32-api-not-always-works/
                // Установить передний план окно.
                // SetForegroundWindow(hControl);

                // ShowWindowAsync(pFoundWindow, 5); //отправляем команду
                // ShowWindow(pFoundWindow, SW_SHOWNOACTIVATE);
                // SetWindowPos(pFoundWindow.ToInt32(), HWND_TOPMOST, Left, Top, Width, Height, SWP_NOACTIVATE);

                // SetWindowPos(hControl, HWND.BOTTOM, 0, 0, 0, 0, SWP.NOSIZE | SWP.NOMOVE);

                return true;
            }
            else
            {

                hControl = ProcessConecto.FindWindow(NameWinndow, null);

                // Отладка
                //SystemConecto.ErorDebag("найдено окно -- " + hControl.ToString(), 1);

                if (hControl == null || hControl == IntPtr.Zero)
                {
                    return false;
                }

            }
            return false;

           
        }
        #endregion

        #region Конвертация в Unicode символов с клавиатуры
        /// <summary>
        /// Конвертация в Unicode символов с клавиатуры
        /// </summary>
        /// <param name="keyCode"></param>
        /// <returns></returns>
        public static String KeycodeToChar(Key keyCode)
        {
            //Keys key = (Keys)keyCode;
            // Системные клавиши не учитываем
            if (Key.LeftShift == keyCode | Key.LeftCtrl == keyCode | Key.LeftAlt == keyCode | Key.RightAlt == keyCode |
                Key.RightCtrl == keyCode | Key.RightShift == keyCode)
            {
                return "";
            }

            // Пример конвертации кодов KeyInterop.VirtualKeyFromKey http://reflector.webtropy.com/default.aspx/4@0/4@0/untmp/DEVDIV_TFS/Dev10/Releases/RTMRel/wpf/src/Base/System/Windows/Input/KeyInterop@cs/1305600/KeyInterop@cs
            // Отсутствует определение спец символов. - Oem1

            switch (keyCode)
            {
                case Key.Add: 
                    return "+";
                case Key.Decimal:
                    return ".";
                case Key.Divide:
                    return "/";
                case Key.Multiply:
                    return "*";
                case Key.OemBackslash:
                    return "\\";
                case Key.OemCloseBrackets:
                    return "]";
                case Key.OemMinus:
                    return "-";
                case Key.OemOpenBrackets:
                    return "[";
                case Key.OemPeriod:
                    return ".";
                case Key.OemPipe:
                    return "|";
                case Key.OemQuestion:
                    return "?";
                case Key.OemQuotes:
                    return "\"";
                case Key.OemSemicolon:
                    return ";";
                //case Key.Oemcomma:
                //    return ",";
                case Key.OemComma:
                    return ",";
                //case Key.Oemplus:
                //    return "+";
                case Key.OemPlus:
                    return "+";
                //case Key.Oemtilde:
                //    return "`";
                case Key.OemTilde:
                    return "`";
                case Key.Separator:
                    return "-";
                case Key.Subtract:
                    return "-";
                case Key.D0:
                    return "0";
                case Key.D1:
                    return "1";
                case Key.D2:
                    return "2";
                case Key.D3:
                    return "3";
                case Key.D4:
                    return "4";
                case Key.D5:
                    return "5";
                case Key.D6:
                    return "6";
                case Key.D7:
                    return "7";
                case Key.D8:
                    return "8";
                case Key.D9:
                    return "9";
                case Key.Space:
                    return " ";
                default:
                    KeyConverter converter = new KeyConverter();

                    return converter.ConvertToString(null, CultureInfo.CurrentCulture, keyCode); //converter.ConvertToString(keyCode); // key.ToString();
            }
        }
        #endregion


        #region Разработки работы с окнами других приложений
        /// <summary>
        /// Получить контрол, в котором находится фокус ввода
        /// </summary>
        void GetFocusedControl()
        {
            IntPtr hFocus;
            IntPtr hFore;
            uint id = 0;
            //узнаем в каком окне находится фокус ввода
            hFore = GetForegroundWindow();
            //подключаемся к процессу
            AttachThreadInput(GetWindowThreadProcessId(hFore, out id), GetCurrentThreadId(), true);
            //получаем хэндл фокуса
            hFocus = GetFocus();
            //отключаемся от процесса
            AttachThreadInput(GetWindowThreadProcessId(hFore, out id), GetCurrentThreadId(), false);
            hControl = hFocus;
        }



        private static System.Windows.Forms.InputLanguageCollection _InstalledInputLanguages;
        // Идентификатор активного потока (адрес переменной для идентификатора процесса по умолчанию Null)
        private static int _ProcessId = 0;
        // Текущий язык ввода
        private static string _CurrentInputLanguage;

        /// <summary>
        /// Определения раскладки клавиатуры окна другого приложения
        /// </summary>
        /// <param name="hWnd"></param>
        /// <returns></returns>
        private static string GetKeyboardLayoutId(IntPtr hWnd)
        {
            // Известные раскладки _CurrentInputLanguage
            // ENU  -английская США
            // RUS  - русская
            // UKR  - украинская

            _InstalledInputLanguages = System.Windows.Forms.InputLanguage.InstalledInputLanguages;

            // Получаем хендл активного окна
            // IntPtr hWnd = GetForegroundWindow();

            // Пример кода
            // HandleRef hWnd = new HandleRef(null, hWnd_);

            // Получаем номер потока активного окна
            int WinThreadProcId = GetWindowThreadProcessId(hWnd, out _ProcessId); 

            // Получаем раскладку
            // Получает активный идентификатор языка ввода (прежнее название раскладки клавиатуры)
            // для указанного потока. Если idThread параметр равен нулю, идентификатор языка ввода для активного поток возвращается.
            IntPtr KeybLayout = GetKeyboardLayout(WinThreadProcId);
            // Получение раскладки текущего приложения IntPtr KeybLayout = GetKeyboardLayout(0); // Работает

            // Циклом перебираем все установленные языки для проверки идентификатора
            for (int i = 0; i < _InstalledInputLanguages.Count; i++)
            {
                //SystemConecto.ErorDebag(_InstalledInputLanguages[i].Handle.ToString(), 2);
                if (KeybLayout == _InstalledInputLanguages[i].Handle)
                {
                    _CurrentInputLanguage = _InstalledInputLanguages[i].Culture.ThreeLetterWindowsLanguageName.ToString();
                    return _CurrentInputLanguage;
                    // break;
                }
            }
            return "";
            //return _CurrentInputLanguage;
        }


        [DllImport("user32.dll", SetLastError = true)]
        static extern IntPtr GetWindow(IntPtr hWnd, uint uCmd);

        enum GetWindow_Cmd : uint
        {
            GW_HWNDFIRST = 0,
            GW_HWNDLAST = 1,
            GW_HWNDNEXT = 2,
            GW_HWNDPREV = 3,
            GW_OWNER = 4,
            GW_CHILD = 5,
            GW_ENABLEDPOPUP = 6
        }


        // ====================== Получение языка клавиатуры От майкрософт

        //[DllImport("user32.dll")]
        //private static extern bool GetKeyboardLayoutName(
        //    StringBuilder pwszKLID);
        //private const int KL_NAMELENGTH = 9;

        //private CultureInfo CultureOfCurrentLayout()
        //{
        //    StringBuilder sb = new StringBuilder(KL_NAMELENGTH);

        //    if (GetKeyboardLayoutName(sbKLID))
        //    {
        //        int klid = int.Parse(
        //           sbKLID.ToString().Substring(KL_NAMELENGTH - 1),
        //           NumberStyles.AllowHexSpecifier,
        //           CultureInfo.InvariantCulture);

        //        // Оставляем только младшую половину числа
        //        klid &= 0xffff;

        //        return new CultureInfo(klid, false);
        //    }
        //    return (null);
        //}

        //===============================================



        #endregion


        public enum ShowWindowCommands : int
        {
            /// <summary>
            /// Hides the window and activates another window.
            /// </summary>
            Hide = 0,
            /// <summary>
            /// Activates and displays a window. If the window is minimized or 
            /// maximized, the system restores it to its original size and position.
            /// An application should specify this flag when displaying the window 
            /// for the first time.
            /// </summary>
            Normal = 1,
            /// <summary>
            /// Activates the window and displays it as a minimized window.
            /// </summary>
            ShowMinimized = 2,
            /// <summary>
            /// Maximizes the specified window.
            /// </summary>
            Maximize = 3, // is this the right value?
            /// <summary>
            /// Activates the window and displays it as a maximized window.
            /// </summary>       
            ShowMaximized = 3,
            /// <summary>
            /// Displays a window in its most recent size and position. This value 
            /// is similar to <see cref="Win32.ShowWindowCommand.Normal"/>, except 
            /// the window is not activated.
            /// </summary>
            ShowNoActivate = 4,
            /// <summary>
            /// Activates the window and displays it in its current size and position. 
            /// </summary>
            Show = 5,
            /// <summary>
            /// Minimizes the specified window and activates the next top-level 
            /// window in the Z order.
            /// </summary>
            Minimize = 6,
            /// <summary>
            /// Displays the window as a minimized window. This value is similar to
            /// <see cref="Win32.ShowWindowCommand.ShowMinimized"/>, except the 
            /// window is not activated.
            /// </summary>
            ShowMinNoActive = 7,
            /// <summary>
            /// Displays the window in its current size and position. This value is 
            /// similar to <see cref="Win32.ShowWindowCommand.Show"/>, except the 
            /// window is not activated.
            /// </summary>
            ShowNA = 8,
            /// <summary>
            /// Активизирует и отображает окно. Если окно минимизировано или
            /// максимальное, система восстанавливает его в первоначальном размере и позиции.
            /// Приложение должно определить этот флаг при восстановлении свернутого окна.
            /// Activates and displays the window. If the window is minimized or 
            /// maximized, the system restores it to its original size and position. 
            /// An application should specify this flag when restoring a minimized window.
            /// </summary>
            Restore = 9,
            /// <summary>
            /// Sets the show state based on the SW_* value specified in the 
            /// STARTUPINFO structure passed to the CreateProcess function by the 
            /// program that started the application.
            /// </summary>
            ShowDefault = 10,
            /// <summary>
            ///  <b>Windows 2000/XP:</b> Minimizes a window, even if the thread 
            /// that owns the window is not responding. This flag should only be 
            /// used when minimizing windows from a different thread.
            /// </summary>
            ForceMinimize = 11
        }


        #region Доп функция определения появления окна в процессах Windows 
        // (некоторые зади после запуска через Start меняют свои свойства как hControl так и тип отображения)
        /// <summary>
        /// Доп. функция определения появления окна в процессах Windows <para></para>
        /// NameWindow - Имя окна<para></para>
        /// NameTitle - Название заголовка NL% - Длина заголовка больше Length > 0
        /// StartTime - строгая проверка запуска окна 
        /// </summary>
        /// <param name="NameWindow"></param>
        /// <param name="?"></param>
        /// <returns></returns>
        public static bool WaitStartWindowApp(string NameWindow, string NameTitle = "", string TimeStart = "")
        {
            bool FuncLoad = true;
            int MaxInterval = 0;

            Process[] allProc_ = null;
            
            while (FuncLoad)
            {
                // Проверить окно
                switch(NameTitle)
                {
                    case "":
                        allProc_ = Process.GetProcessesByName(NameWindow).Where(process => process.MainWindowHandle != IntPtr.Zero).ToArray();
                        break;

                    case "NL%":
                        if (TimeStart == "")
                        {
                            allProc_ = Process.GetProcessesByName(NameWindow).Where(process => process.MainWindowHandle != IntPtr.Zero && process.MainWindowTitle.Length > 0).ToArray();
                        }
                        else{
                            allProc_ = Process.GetProcessesByName(NameWindow).Where(process => process.MainWindowHandle != IntPtr.Zero && process.MainWindowTitle.Length > 0 && process.StartTime.ToString() == TimeStart).ToArray();
                        }
                        break;
                    default:
                        if (TimeStart == "")
                        {
                            allProc_ = Process.GetProcessesByName(NameWindow).Where(process => process.MainWindowHandle != IntPtr.Zero && process.MainWindowTitle == NameTitle).ToArray();
                        }
                        else
                        {
                            allProc_ = Process.GetProcessesByName(NameWindow).Where(process => process.MainWindowHandle != IntPtr.Zero && process.MainWindowTitle == NameTitle && process.StartTime.ToString() == TimeStart).ToArray();
                        }
                        break;
                }


                if (allProc_ != null && allProc_.Length >= 1)
                {
                    //hControl = allProc_[0].MainWindowHandle;
                    return true;
                }


                if (MaxInterval == 200)
                {
                    SystemConecto.ErorDebag("Ошибка работы доп. функции определения появления окна в процессах Windows:" + Environment.NewLine +
                                                "NameWindow: " + NameWindow + Environment.NewLine +
                                                "NameTitle: " + NameTitle);
                    FuncLoad = false;
                }
                else
                {
                    MaxInterval++;
                    // Такт остановки
                    Thread.Sleep(10);
                }

                
            }
            
            //"osk"
            return false;
        }

        #endregion


    }
    #endregion

    
    
    
    public static class NewMessage
    {

        [DllImport("user32.dll", SetLastError = false)]
        public static extern IntPtr GetDlgItem(IntPtr hDlg, int nIDDlgItem);
        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = false)]
        public static extern IntPtr SendMessage(IntPtr hWnd, uint Msg, int wParam, string lParam); //HandleRef

        public const uint WM_SETTEXT = 0x000C;

        const Int32 WM_GETTEXT = 0xD;

        [DllImport("user32.dll", CharSet = CharSet.Unicode)]
        public static extern IntPtr FindWindowEx(IntPtr parentHandle, IntPtr childAfter, string lclassName, string windowTitle);

        public static void InteropSetText(IntPtr iptrHWndDialog, string Class, string strTextToSet)
        {
            //IntPtr iptrHWndControl = GetDlgItem(iptrHWndDialog, iControlID);
            //HandleRef hrefHWndTarget = new HandleRef(null, iptrHWndControl);
            // MessageBox.Show(iptrHWndControl.ToString());

            // var ihWnd = FindWindowEx(IntPtr.Zero, IntPtr.Zero, "Notepad", null);
            var ihEdit = FindWindowEx(iptrHWndDialog, IntPtr.Zero, Class, null);
            if (ihEdit == IntPtr.Zero)
            {
                SystemConecto.ErorDebag("Ошибка работы класса NewMessage");
            }
            // 'get the notepad editor window handle

            //var ihEdit = FindWindowEx(iptrHWndDialog, IntPtr.Zero, "Edit", null);
            // var ihEdit = FindWindowEx(ihWnd, IntPtr.Zero, "Edit", null);

            // 'change to intptr for SendMessage to work
            // var phEdit = new IntPtr(ihEdit);

            // IntPtr hWndThirdButton = FindWindowByIndex(iptrHWndDialog, 00010010);
            //MessageBox.Show(hWndThirdButton.ToString());

            //var Text = new StringBuilder(strTextToSet);
            SendMessage(ihEdit, WM_SETTEXT, 0, strTextToSet);
            // Прочитать
            //var st = new StringBuilder(100);
            //SendMessage(iptrHWndDialog, WM_GETTEXT, 100, st);
            //MessageBox.Show(st.Length.ToString());
        }





    }

    public static class ProcessConecto
    {
        /// <summary>
        /// Завершить процесс
        /// http://msdn.microsoft.com/ru-ru/library/windows/desktop/ms686714(v=vs.85).aspx
        /// </summary>
        /// <param name="hProcess"></param>
        /// <param name="uExitCode"></param>
        /// <returns></returns>
        [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError=true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool TerminateProcess(IntPtr hProcess, int uExitCode);

        /// <summary>
        /// Открывает существующий локальный объект процесса.
        /// </summary>
        /// <param name="dwDesiredAccess"></param>
        /// <param name="bInheritHandle"></param>
        /// <param name="dwProcessId"></param>
        /// <returns></returns>
        [DllImport("kernel32.dll", SetLastError = true)]
        static extern IntPtr OpenProcess(uint dwDesiredAccess, bool bInheritHandle, int dwProcessId);

        /// <summary>
        /// Закрывает открытый дескриптор объекта.
        /// </summary>
        /// <param name="hObject"></param>
        /// <returns></returns>
        [DllImport("kernel32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool CloseHandle(IntPtr hObject);


        // Для Windows Mobile, заменить user32.dll на coredll.dll
        [DllImport("user32.dll", SetLastError = true)]
        public static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

        // Найти окно Caption только. Обратите внимание, вы должны пройти IntPtr.Zero в качестве первого параметра.

        [DllImport("user32.dll", EntryPoint = "FindWindow", SetLastError = true)]
        static extern IntPtr FindWindowByCaption(IntPtr ZeroOnly, string lpWindowName);

        // You can also call FindWindow(default(string), lpWindowName) or FindWindow((string)null, lpWindowName)

        #region Определение размещения активного окна
        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool GetWindowRect(HandleRef hWnd, out RECT lpRect);

        [StructLayout(LayoutKind.Sequential)]
        public struct RECT
        {
            public int Left;        // x position of upper-left corner
            public int Top;         // y position of upper-left corner
            public int Right;       // x position of lower-right corner
            public int Bottom;      // y position of lower-right corner
        }

        /// <summary>
        /// Определение размещения активного окна Left Top  Width  Height
        /// </summary>
        /// <param name="NameProcess"></param>
        /// <returns></returns>
        public static int[] GetWindowPr(string NameProcess)
        {
            RECT rct;
            IntPtr pFoundWindow;
            int[] Size = new int[4] { 0, 0, 0, 0 };

            Process[] allProc_ = Process.GetProcessesByName(NameProcess);
            if (allProc_ != null && allProc_.Length >= 1)
            {
                pFoundWindow = allProc_[0].MainWindowHandle;
            }
            else
            {
                pFoundWindow = FindWindow(NameProcess, null);
            }

            if (!GetWindowRect(new HandleRef(null, pFoundWindow), out rct))
            {
                // Ошибка
            }

            // Left Top Width Height
            Size = new int[4] { rct.Left, rct.Top, rct.Right - rct.Left + 1, rct.Bottom - rct.Top + 1 };
            return Size;
        }




        #endregion


        #region Снимок экрана для WPF с помощью WinApi Так как программный GDI+ на поддерживает WPF слоя( нам нужно обратится к Pinvoke (потокам))
        /// <summary>
        /// Получаем дескриптор окна рабочего стола
        /// </summary>
        /// <returns></returns>
        [DllImport("user32")]
        public static extern IntPtr GetDesktopWindow();

        // http://msdn.microsoft.com/en-us/library/dd144871(VS.85).aspx
        [DllImport("user32.dll")]
        public static extern IntPtr GetDC(IntPtr hwnd);

        // http://msdn.microsoft.com/en-us/library/dd183370(VS.85).aspx
        [DllImport("gdi32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool BitBlt(IntPtr hDestDC, int x, int y, int nWidth, int nHeight, IntPtr hSrcDC, int xSrc, int ySrc, Int32 dwRop);

        // http://msdn.microsoft.com/en-us/library/dd183488(VS.85).aspx
        [DllImport("gdi32.dll")]
        public static extern IntPtr CreateCompatibleBitmap(IntPtr hdc, int nWidth, int nHeight);

        // http://msdn.microsoft.com/en-us/library/dd183489(VS.85).aspx
        [DllImport("gdi32.dll", SetLastError = true)]
        public static extern IntPtr CreateCompatibleDC(IntPtr hdc);

        // http://msdn.microsoft.com/en-us/library/dd162957(VS.85).aspx
        [DllImport("gdi32.dll", ExactSpelling = true, PreserveSig = true, SetLastError = true)]
        public static extern IntPtr SelectObject(IntPtr hdc, IntPtr hgdiobj);

        // http://msdn.microsoft.com/en-us/library/dd183539(VS.85).aspx
        [DllImport("gdi32.dll")]
        public static extern bool DeleteObject(IntPtr hObject);

        // http://msdn.microsoft.com/en-us/library/dd162920(VS.85).aspx
        [DllImport("user32.dll")]
        public static extern int ReleaseDC(IntPtr hwnd, IntPtr dc);

        public const int SRCCOPY = 0x00CC0020;

        /// <summary>
        /// Снимок экрана для WPF с помощью WinApi
        /// </summary>
        /// <param name="hWnd"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <returns></returns>
        public static BitmapSource ScreenCaptureWinAPI(int x, int y, int width, int height) //IntPtr hWnd,
        {
            IntPtr sourceDC = IntPtr.Zero;
            IntPtr targetDC = IntPtr.Zero;
            IntPtr compatibleBitmapHandle = IntPtr.Zero;
            BitmapSource bitmap = null;

            try
            {
                // получает основной рабочий стол и все открытые окна
                sourceDC = GetDC(GetDesktopWindow());

                //sourceDC = User32.GetDC(hWnd);
                targetDC = CreateCompatibleDC(sourceDC);

                // create a bitmap compatible with our target DC
                compatibleBitmapHandle = CreateCompatibleBitmap(sourceDC, width, height);

                // gets the bitmap into the target device context
                SelectObject(targetDC, compatibleBitmapHandle);

                // copy from source to destination
                BitBlt(targetDC, 0, 0, width, height, sourceDC, x, y, SRCCOPY);

                // Вот клей WPF, чтобы заставить все это работать. Он преобразует HBITMAP в BitmapSource.
                bitmap = System.Windows.Interop.Imaging.CreateBitmapSourceFromHBitmap(
                                    compatibleBitmapHandle, IntPtr.Zero, Int32Rect.Empty,
                                    BitmapSizeOptions.FromEmptyOptions());

            }
            catch (Exception ex)
            {
                SystemConecto.ErorDebag("BitmapSource ScreenCapture : " + Environment.NewLine +
                    " === Message: " + ex.Message.ToString() + Environment.NewLine +
                    " === Exception: " + ex.ToString(), 1);
                // Отладка throw new ScreenCaptureException(string.Format("Error capturing region {0},{1},{2},{3}", x, y, width, height), ex);
            }
            finally
            {
                DeleteObject(compatibleBitmapHandle);
                ReleaseDC(IntPtr.Zero, sourceDC);
                ReleaseDC(IntPtr.Zero, targetDC);
            }
            return bitmap;

        }

        /// <summary>
        /// Снимок экрана для WPF с помощью WinForm
        /// </summary>
        /// <param name="hWnd"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <returns></returns>
        public static BitmapSource ScreenCapture(int x, int y, int width, int height) //IntPtr hWnd,
        {

            BitmapSource bitmap = null;

            try
            {
                System.Drawing.Bitmap bmpScreenshot = new System.Drawing.Bitmap(width, height, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
                System.Drawing.Graphics gfxScreenshot = System.Drawing.Graphics.FromImage(bmpScreenshot);
                gfxScreenshot.CopyFromScreen(x, y, 0, 0, new System.Drawing.Size(width, height), System.Drawing.CopyPixelOperation.SourceCopy);

                // Вот клей WPF, чтобы заставить все это работать. Он преобразует HBITMAP в BitmapSource.
                bitmap = System.Windows.Interop.Imaging.CreateBitmapSourceFromHBitmap(
                      bmpScreenshot.GetHbitmap(),
                      IntPtr.Zero,
                      System.Windows.Int32Rect.Empty,
                      BitmapSizeOptions.FromWidthAndHeight(bmpScreenshot.Width, bmpScreenshot.Height));

            }
            catch (Exception ex)
            {
                SystemConecto.ErorDebag("BitmapSource ScreenCapture : " + Environment.NewLine +
                    " === Message: " + ex.Message.ToString() + Environment.NewLine +
                    " === Exception: " + ex.ToString(), 1);
                // Отладка throw new ScreenCaptureException(string.Format("Error capturing region {0},{1},{2},{3}", x, y, width, height), ex);
            }

            return bitmap;

        }

        #endregion


        #region Завершение процессов в ОС

        /// <summary>
        /// Завершение процесса пользователя
        /// </summary>
        /// <param name="NameProcess"></param>
        public static bool TerminateProcessSystem(string NameProcess)
        {
            var getAllExploreProcess = Process.GetProcesses().Where(r => r.ProcessName.Contains(NameProcess)); //"EXPLORE"
            uint PROCESS_TERMINATE = 0x1;
            // Отладка
            // SystemConecto.ErorDebag("0", 1);
            // Завершить все процессы с именем NameProcess
            foreach (Process process in getAllExploreProcess)
            {
                // Отладка
                //SystemConecto.ErorDebag("1", 1);
                try
                {
                    IntPtr hProcess = OpenProcess(PROCESS_TERMINATE, false, process.Id);
                    if (hProcess != IntPtr.Zero && TerminateProcess(hProcess, 1) )
                    {
                    }
                    else{
                        // Ошибки доступа - синии экраны
                        // "Процесс является системным и не может быть завершен"
                        return false;
                    }
                    CloseHandle(hProcess);
                    // TerminateProcess(process.MainWindowHandle, process.ExitCode);

                    //process.Kill();
                }
                catch(Exception ex)
                {
                    SystemConecto.ErorDebag(ex.ToString(), 1);
                    return false;
                }
            }
            return true;
        }

        #endregion

        /// <summary>
        /// Свернуть Процесс или окно<para></para>
        /// <param name="NameProcess">NameWinndow: Имя</param>
        /// <returns></returns>
        /// </summary>
        public static bool StateSystemWindow(string NameWinndow, int Command = 0)
        {
            // получаем хендл Окна похоже на hwnd=FindWindow("HHTaskBar",null);
            // Процессы и окна разные области и методы
            Process[] allProc_ = Process.GetProcessesByName(NameWinndow).Where(process => process.MainWindowHandle != IntPtr.Zero).ToArray();
            //Process.GetCurrentProcess

            //Process[] allProc = Process.GetProcesses();
            //foreach (Process currProc in allProc)
            //{
            //    SystemConecto.ErorDebag(currProc.ProcessName, 1);
            //    //  //ErorDebag(currProc.ProcessName, 2, 2);
            //}


            if (allProc_ != null && allProc_.Length >= 1)
            {
                IntPtr pFoundWindow = allProc_[0].MainWindowHandle;
                //ShowWindow(hwnd,SW_HIDE);
                TextPasteWindow.ShowWindow(pFoundWindow, Command); // TextPasteWindow.ShowWindowCommands.Hide
                return true;
            }
            else
            {
                IntPtr Hwnd = FindWindow(NameWinndow, null);
                if (Hwnd == null || Hwnd == IntPtr.Zero)
                {
                    return false;
                }

                if (TextPasteWindow.ShowWindow(Hwnd, Command))
                {
                    return true;
                }

                
            }
            return false;
        }

    }

    /// <summary>
    /// HWND значение для hWndInsertAfter
    /// </summary>
    public static class HWND
    {
        public static readonly IntPtr
        NOTOPMOST = new IntPtr(-2),
        BROADCAST = new IntPtr(0xffff),
        TOPMOST = new IntPtr(-1), //поверх всех окон, включая топовые
        TOP = new IntPtr(0),
        BOTTOM = new IntPtr(1);

    }

    /// <summary>
    /// SetWindowPos Flags параметры
    /// </summary>
    public static class SWP
    {
        public static readonly uint
        NOSIZE = 0x0001,
        NOMOVE = 0x0002,
        NOZORDER = 0x0004,
        NOREDRAW = 0x0008,
        NOACTIVATE = 0x0010,
        DRAWFRAME = 0x0020,
        FRAMECHANGED = 0x0020,
        SHOWWINDOW = 0x0040,
        HIDEWINDOW = 0x0080,
        NOCOPYBITS = 0x0100,
        NOOWNERZORDER = 0x0200,
        NOREPOSITION = 0x0200,
        NOSENDCHANGING = 0x0400,
        DEFERERASE = 0x2000,
        ASYNCWINDOWPOS = 0x4000;

    }


    /// <summary>
    /// Опции для работы с окнами и функциями с интерфейсом Windows с помощью WINAPI 32
    /// </summary>
    static class Win32
    {
        
        #region Размещение окна и его перемещение
        [StructLayout(LayoutKind.Sequential)]
        public struct POINT
        {
            public int X;
            public int Y;

            public POINT(int x, int y)
            {
                this.X = x;
                this.Y = y;
            }
            public POINT(Point pt)
            {
                X = Convert.ToInt32(pt.X);
                Y = Convert.ToInt32(pt.Y);
            }
        };

        [DllImport("user32.dll")]
        internal static extern bool ClientToScreen(IntPtr hWnd, ref POINT lpPoint);

        [DllImport("user32.dll")]
        internal static extern bool MoveWindow(IntPtr hWnd, int X, int Y, int nWidth, int nHeight, bool bRepaint);
        #endregion


        [DllImport("user32.dll", EntryPoint = "keybd_event", SetLastError = true)]
        static extern void keybd_event(byte bVk, byte bScan, uint dwFlags, uint dwExtraInfo);

        [DllImport("user32.dll", EntryPoint = "GetKeyState", SetLastError = true)]
        static extern short GetKeyState(uint nVirtKey);

        private const uint KEYEVENTF_KEYUP = 0x2;

        #region Управление клавишей NumLOCK
            private const byte VK_NUMLOCK = 0x90;

            //NUM-------------------------------------------------------
            /// <summary>
            /// Програмное нажатие на NUMLOCK включить - выключить
            /// </summary>
            /// <param name="newState"></param>
            public static void SetNumLockKey(bool newState)
            {
                bool NumLockSet = GetKeyState(VK_NUMLOCK) != 0;
                if (NumLockSet != newState)
                {
                    keybd_event(VK_NUMLOCK, 0, 0, 0);
                    keybd_event(VK_NUMLOCK, 0, KEYEVENTF_KEYUP, 0);
                }
            }
            /// <summary>
            /// Чтение параметра нажата ли клавиша NUMLOCK
            /// </summary>
            /// <returns></returns>
            public static bool GetNumLockState()
            {
               return GetKeyState(VK_NUMLOCK) != 0;
            }
        #endregion 

        #region Управление клавишей CAPSLOCK
            private const byte VK_CAPITAL = 0x14;

            //Caps-------------------------------------------------------
            /// <summary>
            /// Програмное нажатие на CAPSLOCK включить - выключить
            /// </summary>
            /// <param name="newState"></param>
            public static void SetCapsLockKey(bool newState)
            {
                bool CAPSLockSet = GetKeyState(VK_CAPITAL) != 0;
                if (CAPSLockSet != newState)
	            {
	                keybd_event(VK_CAPITAL, 0, 0, 0);
	                keybd_event(VK_CAPITAL, 0, KEYEVENTF_KEYUP, 0);
	            }
	        }
	 
            /// <summary>
            /// Чтение параметра нажата ли клавиша CAPSLOCK 
            /// </summary>
            /// <returns>bool</returns>
	        public static bool GetCapsLockState()
	        {
	            return GetKeyState(VK_CAPITAL) != 0;
	        }

            #endregion

      





      #region
         

            public const int TOKEN_QUERY = 0X00000008;

            const int ERROR_NO_MORE_ITEMS = 259;

            enum TOKEN_INFORMATION_CLASS
            {
            TokenUser = 1,
            TokenGroups,
            TokenPrivileges,
            TokenOwner,
            TokenPrimaryGroup,
            TokenDefaultDacl,
            TokenSource,
            TokenType,
            TokenImpersonationLevel,
            TokenStatistics,
            TokenRestrictedSids,
            TokenSessionId
            }

            [StructLayout(LayoutKind.Sequential)]
            struct TOKEN_USER
            {
            public _SID_AND_ATTRIBUTES User;
            }

            [StructLayout(LayoutKind.Sequential)]
            public struct _SID_AND_ATTRIBUTES
            {
            public IntPtr Sid;
            public int Attributes;
            }
            [DllImport("advapi32")]
            static extern bool OpenProcessToken(
            HANDLE ProcessHandle, // handle to process
            int DesiredAccess, // desired access to process
            ref IntPtr TokenHandle // handle to open access token
            );

            [DllImport("kernel32")]
            static extern HANDLE GetCurrentProcess();

            [DllImport("advapi32", CharSet=CharSet.Auto)]
            static extern bool GetTokenInformation(
            HANDLE hToken,
            TOKEN_INFORMATION_CLASS tokenInfoClass,
            IntPtr TokenInformation,
            int tokeInfoLength,
            ref int reqLength);

            [DllImport("kernel32")]
            static extern bool CloseHandle(HANDLE handle);

            [DllImport("advapi32", CharSet=CharSet.Auto)]
            static extern bool LookupAccountSid
            (
            [In,MarshalAs(UnmanagedType.LPTStr)] string lpSystemName, // name of local or remote computer
            IntPtr pSid, // security identifier
            StringBuilder Account, // account name buffer
            ref int cbName, // size of account name buffer
            StringBuilder DomainName, // domain name
            ref int cbDomainName, // size of domain name buffer
            ref int peUse // SID type
            // ref _SID_NAME_USE peUse // SID type
            );

            [DllImport("advapi32", CharSet=CharSet.Auto)]
            static extern bool ConvertSidToStringSid(
            IntPtr pSID,
            [In,Out,MarshalAs(UnmanagedType.LPTStr)] ref string pStringSid);

            /// <summary>
            /// Формирование списка всех процессов по указанному имени или всех процессов, согласно структуры:<para></para>
            /// Process Name/Process ID/MachineName/DumpUserInfo
            /// </summary>
            /// <param name="processName"></param>
            public static Dictionary<int, string[]> ListProcess(string processName = "" ) {
            //string processName = Process.GetCurrentProcess().ProcessName;

            // Словарь 
            Dictionary<int, string[]> ListProcess_ = new Dictionary<int, string[]>();
            Dictionary<int, string[]> SerchOk = null;
            Process current = Process.GetCurrentProcess();
            string[] InfoDump = DumpUserInfo(current.Handle);
            // && p.Id == current.Id Только свой процесс 
            Process.GetProcesses().Where(p => p.ProcessName == processName ).ToList().ForEach(p =>
            {
                // Process Name/Process ID/MachineName/DumpUserInfo
                // string[] StruHedleOp = new string[3] { myProcess.ProcessName, myProcess.Id.ToString(), myProcess.MachineName };
                try
                {
                    //Во время доступа к чужой среде могут возникнуть ошибки!! Раньше не было.  p.Id != current.Id
                    InfoDump = DumpUserInfo(p.Handle);
                    MessageBox.Show(p.StartInfo.UserName);
                    ListProcess_.Add(p.Id, new string[11] { p.ProcessName, p.Id.ToString(), p.MachineName, InfoDump[0], InfoDump[1], InfoDump[2], InfoDump[3], InfoDump[4], InfoDump[5], InfoDump[6], p.MainWindowHandle.ToString() });

                }
                catch (Exception ex)
                {
                   // ListProcess_.Add(p.Id, new string[11] { p.ProcessName, p.Id.ToString(), p.MachineName, ex.Message, "", "", "", "", "", "", p.MainWindowHandle.ToString() });

                }

            });
            if (ListProcess_.Count() > 0)
            {
                return ListProcess_;
            }
            else
            {
                SystemConecto.ErorDebag("Не удалось найти процессы по имени - " + processName, 3);
                return SerchOk;
            }


            //Process[] myProcesses = Process.GetProcesses();
            //if (processName.Length > 0)
            //{
            //    myProcesses = Process.GetProcessesByName(processName);
            //    if (myProcesses.Length == 0)
            //    {
            //        SystemConecto.ErorDebag("Не удалось найти процессы по имени - " + processName, 3);
            //        return null;
            //    }
            //}

               

                //foreach(Process myProcess in myProcesses)
                //{
                //    // Process Name/Process ID/MachineName/DumpUserInfo
                //    // string[] StruHedleOp = new string[3] { myProcess.ProcessName, myProcess.Id.ToString(), myProcess.MachineName };
                //    try
                //    {
                //        string[] InfoDump = DumpUserInfo(myProcess.Handle);
                //        ListProcess.Add(myProcess.Id, new string[11] { myProcess.ProcessName, myProcess.Id.ToString(), myProcess.MachineName, InfoDump[0], InfoDump[1], InfoDump[2], InfoDump[3], InfoDump[4], InfoDump[5], InfoDump[6], myProcess.MainWindowHandle.ToString() });
                //    }
                //    catch (Exception ex)
                //    {
                //        ListProcess.Add(myProcess.Id, new string[11] { myProcess.ProcessName, myProcess.Id.ToString(), myProcess.MachineName, ex.Message, "", "", "", "", "", "", myProcess.MainWindowHandle.ToString() });

                //    }
                       
                //       //DumpUserInfo(myProcess.Handle);
                //}
                //return ListProcess;
            }

            /// <summary>
            /// Информация о пользователи который использует приложение, Process.<para></para>
            /// <para></para>
            /// [6] - SID код пользователя
            /// </summary>
            /// <param name="pToken"></param>
            /// <returns></returns>
            public static string[] DumpUserInfo(HANDLE pToken)
            {
                int Access = TOKEN_QUERY;
                //StringBuilder sb = new StringBuilder();
                string[] InfoSB = new string[7] { "", "", "", "", "", "", "" };
                //sb.AppendFormat("\nToken dump performed on {0}\n\n", DateTime.Now);
                InfoSB[0] = string.Format(Environment.NewLine+"Token dump performed on {0}" + Environment.NewLine + Environment.NewLine, DateTime.Now);
                HANDLE procToken = IntPtr.Zero;
                try
                {
                    if (OpenProcessToken(pToken, Access, ref procToken))
                    {
                        InfoSB[1] = string.Format("Process Token:" + Environment.NewLine, DateTime.Now);
                        //sb.Append("Process Token:\n");
                        string[] InfoSBDop = PerformDump(procToken);
                        InfoSB[2] = InfoSBDop[0];
                        InfoSB[3] = InfoSBDop[1];
                        InfoSB[4] = InfoSBDop[2];
                        InfoSB[5] = InfoSBDop[3];
                        InfoSB[6] = InfoSBDop[4];
                           // PerformDump(procToken).ToString();
                        //sb.Append(PerformDump(procToken));
                        CloseHandle(procToken);
                    }
                }
                catch (Exception ex)
                {
                    InfoSB[1] = string.Format("Process Error Token:"+Environment.NewLine, DateTime.Now);
                    //sb.Append("Process Error Token:\n");
                    InfoSB[2] = string.Format(ex.Message);
                    // sb.Append(ex.Message);
                    // SystemConecto.ErorDebag();
                }

                return InfoSB;
            }

            static string[] PerformDump(HANDLE token)
            {
                //StringBuilder sb = new StringBuilder();
                string[] InfoSB = new string[0];
                TOKEN_USER tokUser;
                const int bufLength = 256;
                IntPtr tu = Marshal.AllocHGlobal( bufLength );
                int cb = bufLength;
                GetTokenInformation( token, TOKEN_INFORMATION_CLASS.TokenUser, tu, cb, ref cb );
                tokUser = (TOKEN_USER) Marshal.PtrToStructure(tu, typeof(TOKEN_USER) );
                //sb.Append(DumpAccountSid(tokUser.User.Sid));
                InfoSB = DumpAccountSid(tokUser.User.Sid);
                Marshal.FreeHGlobal( tu );
                return InfoSB;
            }

            static string[] DumpAccountSid(IntPtr SID)
            {
                int cchAccount = 0;
                int cchDomain = 0;
                int snu = 0 ;
                //StringBuilder sb = new StringBuilder();
                string[] InfoSB = new string[5] {"","","","",""};
                // Caller allocated buffer
                StringBuilder Account= null;
                StringBuilder Domain = null;
                bool ret = LookupAccountSid(null, SID, Account, ref cchAccount, Domain, ref cchDomain, ref snu);
                if ( ret == true )
                    if ( Marshal.GetLastWin32Error() == ERROR_NO_MORE_ITEMS )
                        return new string[5] { "Error", "", "", "", "" };
                    try
                    {
                        Account = new StringBuilder( cchAccount );
                        Domain = new StringBuilder( cchDomain );
                        ret = LookupAccountSid(null, SID, Account, ref cchAccount, Domain, ref cchDomain, ref snu);
                        if (ret)
                        {
                            InfoSB[0] = Domain.ToString();
                            //    sb.Append(Domain);
                            InfoSB[1] = @"\\";
                            //                        sb.Append(@"\\");
                            InfoSB[2] = Account.ToString();
                            // sb.Append(Account);
                        }
                        else
                            InfoSB[2] = "logon account (no name)";
                            //SystemConecto.ErorDebag("logon account (no name)  - ", 3);
                        //Console.WriteLine("logon account (no name) ");
                        }
                    catch (Exception ex)
                    {
                        SystemConecto.ErorDebag(ex.Message);
                    }
                    finally
                    {
                    }
                string SidString = null;
                ConvertSidToStringSid(SID, ref SidString);
                InfoSB[3] = "\nSID: ";
                //sb.Append("\nSID: ");
                InfoSB[4] = SidString;
                // sb.Append(SidString);
                return InfoSB; //sb.ToString();
            }


        #endregion
    }   // Win 32 Class
}
