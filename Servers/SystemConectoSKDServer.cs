using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
// Отладка Messagebox
using System.Windows;
// DllImport
using System.Runtime.InteropServices;
// Регулярные выражения
using System.Text.RegularExpressions;
// INotifyPropertyChanged - Уведомляет клиентов об изменении значения свойства.
using System.ComponentModel;




namespace ConectoWorkSpace
{
    static class  SystemConectoSKDServer
    {

        public static bool ServerStart = false; // Состояние сервера. Запущен ли он

        public static string ErrorServer = "";  // Последняя ошибка в сервере

        public static List<ColumnsConvertSKD> TableListConvectorSKD = new List<ColumnsConvertSKD>();

        public static List<ColumnsControllerSKD> TableListControllerSKD = new List<ColumnsControllerSKD>();

        static string[] TypeStrs = { "Неизвестный", "COM", "FT", "IP", "RETR", "SIM" };
        static string[] TypePort = { "Неизвестный", "Z397 (Обычный)", "Z397 Guard", "Z397 Guard IP", "Программа-ретранслятор ZRetr (ретранслирует USB-конвертер, имитирует IP-конвертер)"};
        static string[] ModeStrs = { "Неизвестный", "Обычный. Имитация обычного конвертера Z-397.", "Расширенный. С дополнительными функциями Guard.", "Тестовый. Для служебного использования.", "Акцепт. Для служебного использования."};

        static readonly string[] CtrTypeStrs = { "", "Gate 2000", "Matrix II Net", "Z5R Net", "Z5R Net 8000", "Guard Net" };

        public static IntPtr hNotify = new IntPtr();
        public static int nRet = 0;

        public static IntPtr m_hCvt;

        //public static IntPtr m_hCtr;

        public const Byte CtrAddr = 3;
        //public const ZPort.ZP_PORT_TYPE CvtPortType = ZPort.ZP_PORT_TYPE.ZP_PORT_IP;
        //public const string CvtPortName = "90.11.11.56:1000";
        //public const Byte CtrAddr = 4;





        static int m_nCvtCount;     // Количество конвртеров для одного типа TypeConvert вычесление уникального id

       

        #region Описание класса


        /*
         *      skd.xml         - настройки skd
         * 
         *     
         * 
         *      Данный пакет SDK предназначен для интеграции конвертера «Z397», «Z397-Guard» и «Z397-IP» в разрабатываемые системы.
         *      Данный конвертер обеспечивает работу с контроллерами «Matrix II Net», «Z5r-Net», «Z5r-Net 8000», «Guard-Net», «Gate 2000».
         * 
         *      Библиотека SDK   - ZGuard.dll
         *      Библиотека порта подключения устройств - Z2USB.dll
         *      
         *      Описание переменных - ZGuard.cs ZPort.cs
         *      
         * 
         */

        #endregion


        #region Сценарии запуска Сервера СКД
        public static void SKDInterfice()
        {
            // MessageBox.Show(SystemConecto.LoginUserAutoriz);
            // Интерфейс Системного Администратора программы (Интегрированные учетные записи)
            if (SystemConecto.AutorizUser.LoginUserAutoriz == "Autorize_pass-admin-IT" || SystemConecto.AutorizUser.LoginUserAutoriz == "Autorize_Admin-Conecto")
            {
                // Основное окно
                //MainWindow ConectoWorkSpace_InW = (MainWindow)Application.Current.MainWindow;
                MainWindow ConectoWorkSpace_InW = AppStart.LinkMainWindow();
                // Ссылка на объект
                var Admin = ConectoWorkSpace_InW.AdminButIm_;
                Admin.Visibility = Visibility.Visible;

            }

        }

        #endregion

        /// <summary>
        /// Запуск сервера AutoServer - старт сервера
        /// </summary>
        /// <param name="Command"></param>
        public static void SKDServer(string Command = null)
        {
            switch (Command)
            { 
                case "AutoServer":
                    // Авто старт сервера зависит от настроек сервера
                    if (SystemConecto.aParamApp["ServerSKDSwitch"] == "1" && !ServerStart)
                    {
                        // Загрузка настроек сервера
                        ReadConfigSKD();
                        // ServerStart
                        // Поиск и иницианализация устройств 
                        if (InitializeServer())
                        {
                            ServerStart = true;
                        }

                    }
                    break;
               
                case "StartServer":
                    
                    // Принудительный старт сервера игнорирование aParamApp["Autorize_Admin-Conecto"]

                    if (!ServerStart)
                    {
                        // Загрузка настроек сервера
                        ReadConfigSKD();
                        // Поиск и иницианализация устройств 
                        if (InitializeServer())
                        {
                            ServerStart = true;
                        }
                    }


                    break;
                case "StopServer":

                    // Принудительная остоновка сервера 
                    if (ServerStart)
                    {

                    }

                    break;
                case "RezetServer":

                    // Принудительная перезагрузка сервера 


                    break;
            }

        }

        /// <summary>
        /// Чтение настроек СКД сервера
        /// </summary>
        /// <returns></returns>
        private static bool ReadConfigSKD()
        {
            // Чтение конфигурации
            var Config = new SystemConfigControll();

            // Чтение параметров приложения
            if (SystemConecto.File_(SystemConecto.PutchApp + "skd.xml", 4))
            {
                
                // Параметры-SKDServer- считать в память
                Config.CreateConfigXMLSKD(1);
                // Проверка целосности и правильности и Чтение
                if (!Config.ReadConfigXMLSKD())
                {
                    // Перезапуск если разрушена целосность конфигурационного файла
                    if (!Config.ReadConfigXMLSKD())
                    {
                        return false;
                    }
                }
                // Параметры-Пользователя (отсутствуют по умолчанию)
            }
            else
            {
                // Создание и чтение
                if (SystemConecto.File_(SystemConecto.PutchApp + "skd.xml", 5))
                {
                    // Параметры-Администратора
                    Config.CreateConfigXMLSKD();
                    // Чтение
                    if (!Config.ReadConfigXMLSKD(1))
                    {
                        return false;
                    }
                    // Параметры-Пользователя (отсутствуют по умолчанию)
                }
            }
            return true;
        }



        #region Функции обслуживания конвекторов СКД

        /// <summary>
        /// Перечесление конверторов, формирование информации о них
        /// </summary>
        /// <param name="pInfo"></param>
        /// <param name="pUserData"></param>
        /// <returns></returns>
        static bool CvtEnumZ397(ref ZGuard.ZG_ENUM_CVT_INFO pInfo, IntPtr pUserData)
        {
            // Список конвертеров
            List<ColumnsConvertSKD> mlTableListConvectorSKD = new List<ColumnsConvertSKD>();

            mlTableListConvectorSKD = TableListConvectorSKD;

            ColumnsConvertSKD Line = new ColumnsConvertSKD();

            // Количесвто обнаруженных
            m_nCvtCount++;

            //switch (pInfo.nPortType)
            //{
            //    case ZPort.ZP_PORT_TYPE.ZP_PORT_COM:
            //    case ZPort.ZP_PORT_TYPE.ZP_PORT_FT:
            //    case ZPort.ZP_PORT_TYPE.ZP_PORT_IP:
            //    case ZPort.ZP_PORT_TYPE.ZP_PORT_RETR:
            //    default:

            //        break;
            //}

            Line.id = m_nCvtCount-1;
            Line.Pid_status = SystemConfigControll.aParamSKD.ContainsKey(string.Format("Convertor-{0}_Pid-status", string.Format("{0:X2}h", pInfo.nPid))) ? Convert.ToInt32(SystemConfigControll.aParamSKD[string.Format("Convertor-{0}_Pid-status", string.Format("{0:X2}h", pInfo.nPid))]) : 0;
            

            // Чтение данных
            var SpeedCOM = SystemConfigControll.aParamSKD.ContainsKey(string.Format("Convertor-{0}_Speed", string.Format("{0:X2}h", pInfo.nPid))) ? Convert.ToInt32(SystemConfigControll.aParamSKD[string.Format("Convertor-{0}_Speed", string.Format("{0:X2}h", pInfo.nPid))]) : 56700;
            // MessageBox.Show(SpeedCOM.ToString());
            Line.NameNode = SystemConfigControll.aParamSKD.ContainsKey(string.Format("Convertor-{0}_NameNode", string.Format("{0:X2}h", pInfo.nPid))) ? SystemConfigControll.aParamSKD[string.Format("Convertor-{0}_NameNode", string.Format("{0:X2}h", pInfo.nPid))] : "Без названия";

            Line.PortType = pInfo.nPortType;
            Line.PortName = pInfo.szPortName;
            Line.PortFr = pInfo.szFriendlyName;
            // ZGuard.ZG_CVT_SPEED.ZG_SPEED_57600 (ZG_SPEED_19200, ZG_SPEED_57600)
            Line.PortFrSpeed = 56700 == SpeedCOM ? ZGuard.ZG_CVT_SPEED.ZG_SPEED_57600 : ZGuard.ZG_CVT_SPEED.ZG_SPEED_19200;
            Line.nPortFrSpeed = SpeedCOM;
            Line.Status = pInfo.fBusy;
            Line.Pid = string.Format("{0:X2}h", pInfo.nPid); // Pid: {5:X2}h
            Line.Sn = pInfo.nSn;    
            Line.Version = string.Format("{0}.{1}", pInfo.nVersion & 0xFF, (pInfo.nVersion >> 8) & 0xFF); // pInfo.nVersion & 0xFF, (pInfo.nVersion >> 8) & 0xFF,
            // nFlags - флаги: {8:X2}h
            Line.TypeConvert = string.Format("{0}/{1}", TypeStrs[(int)pInfo.nPortType], TypePort[(int)pInfo.nType]);
            // Взять текст до точки
            Match m_ = Regex.Match(ModeStrs[(int)pInfo.nMode], @"(.*?)[.]", RegexOptions.IgnoreCase | RegexOptions.Multiline);
            Line.Mode = (m_.Success)? m_.Groups[1].Value: "";

            mlTableListConvectorSKD.Add(Line);

            // Перезапись с учетом новых данных
            TableListConvectorSKD = mlTableListConvectorSKD;

            return true;
        }

        /// <summary>
        /// Изменения событий конвектора (отключение, включение, обработка занятости устройства)
        /// </summary>
        /// <param name="nMsg"></param>
        /// <param name="nMsgParam"></param>
        /// <param name="pUserData"></param>
        /// <returns></returns>
        static bool ZNotifyCB(UInt32 nMsg, IntPtr nMsgParam, IntPtr pUserData)
        {
            
            
            //switch (nMsg)
            //{
            //    case ZPort.ZPIntf.ZP_N_INSERT:
            //        {
            //            // Устройство подключили
            //            ZGuard.ZGIntf.ZG_EnumConvertersEx(new ZGuard.ZG_ENUMCVTSPROC(CvtEnumZ397));
            //            // MessageBox.Show("Test");
            //            //Admin.RefrechdataGridConvert();

            //            //ZPort.ZP_PORT_INFO pInf1 = (ZPort.ZP_PORT_INFO)Marshal.PtrToStructure(nMsgParam, typeof(ZPort.ZP_PORT_INFO));
            //            //Console.WriteLine("Конвертер подключен: {0}; имя: {1}; др.имя: {2}; занят: {3}",
            //            //    TypeStrs[(int)pInf1.nType],
            //            //    pInf1.szName,
            //            //    pInf1.szFriendly,
            //            //    pInf1.fBusy);
            //        }
            //        break;
            //    case ZPort.ZPIntf.ZP_N_REMOVE:
            //        {
            //            //ZPort.ZP_PORT_INFO pInf1 = (ZPort.ZP_PORT_INFO)Marshal.PtrToStructure(nMsgParam, typeof(ZPort.ZP_PORT_INFO));
            //            //Console.WriteLine("Конвертер отключен: {0}; имя: {1}; др.имя: {2}",
            //            //    TypeStrs[(int)pInf1.nType],
            //            //    pInf1.szName,
            //            //    pInf1.szFriendly);
            //        }
            //        break;
            //    case ZPort.ZPIntf.ZP_N_STATE_CHANGED:
            //        {
            //            //ZPort.ZP_N_CHANGE_STATE pInf1 = (ZPort.ZP_N_CHANGE_STATE)Marshal.PtrToStructure(nMsgParam, typeof(ZPort.ZP_N_CHANGE_STATE));
            //            //string s = "";
            //            //if ((pInf1.nChangeMask & 1) != 0)
            //            //    s = "busy, ";
            //            //if ((pInf1.nChangeMask & 2) != 0)
            //            //    s += "friendly, ";
            //            //if (s != "")
            //            //    s = s.Remove(s.Length - 2);
            //            //Console.WriteLine("Изменилось состояние ({0}): {1}; имя: {2}; др.имя: {3}; занят: {4}",
            //            //    s,
            //            //    TypeStrs[(int)pInf1.rInfo.nType],
            //            //    pInf1.rInfo.szName,
            //            //    pInf1.rInfo.szFriendly,
            //            //    pInf1.rInfo.fBusy);
            //        }
            //        break;
            //}
            return true;
        }

        #endregion





        #region Перечисление конверторов в Объектный список /EnumConvert



        /// <summary>
        /// Включение сервера
        /// </summary>
        /// <returns></returns>
        public static bool InitializeServer()
        {
            // Загрузить список известных систем СКД и их конвертеров
            // FOR
            var ml_nCvtCount = 0;
            var TypeConvert = "Z397";

            // Пока один тип

            // Проверка библиотек и версий SDK перед опросом устройств
            if (CheckSDK(TypeConvert))
            {

                // Инициализируем библиотеку
                if (InitializeSDKDll(TypeConvert, ref nRet, ref hNotify))
                {
                    // Тип конвертора
                    switch (TypeConvert)
                    {
                        case "Z397":
                            //  ==================================
                            try
                            {
                                m_nCvtCount = 0;
                                // Сформировать информацию об подключенных устройсвах, их поиск. Записать в память.
                                // ZG_ENUMCVTSPROC - Функция обратного вызова, возвращающая информацию о каждом подключенном конвертере
                                // Режим отладки
                                nRet = ZGuard.ZGIntf.ZG_EnumConverters(new ZGuard.ZG_ENUMCVTSPROC(CvtEnumZ397));
                                nRet = ZGuard.ZGIntf.ZG_EnumConvertersEx(new ZGuard.ZG_ENUMCVTSPROC(CvtEnumZ397));
                                // Режим отладки Перечисляем конвертеры
                                // nRet = ZGuard.ZGIntf.ZG_EnumConvertersEx(new ZGuard.ZG_ENUMCVTSPROC(CvtEnumZ397), IntPtr.Zero, ZGuard.ZGIntf.ZG_DF_USB, pWait);
                                if (nRet < 0)
                                {
                                    // !===Записать коды ошибок в БД ошибок СКД приложения===!
                                    ErrorServer = " - Сервер СКД: ZG_EnumConverters (" + nRet + ").";
                                    SystemConecto.ErorDebag(ErrorServer, 0);
                                     
                                }
                                ml_nCvtCount = ml_nCvtCount + m_nCvtCount;
                                // Включение обработки событий о конвекторе (изменений его состояний)
                                ZGuard.ZG_NOTIFY_SETTINGS rNS = new ZGuard.ZG_NOTIFY_SETTINGS();
                                rNS.nNMask = ZPort.ZPIntf.ZP_NF_EXIST | ZPort.ZPIntf.ZP_NF_BUSY | ZPort.ZPIntf.ZP_NF_FRIENDLY;
                                rNS.nDevTypes = ZGuard.ZGIntf.ZG_DF_ALL;
                                rNS.pfnCallback = new ZPort.ZP_NOTIFYPROC(ZNotifyCB);
                                // Выдает ошибку
                                // nRet = ZGuard.ZGIntf.ZG_FindNotification(ref hNotify, ref rNS);
                                if (nRet < 0)
                                {
                                   // Устройство подключенно но не работает нормально
                                    ErrorServer = " - Сервер СКД: ZG_FindNotification (" + nRet + ").";
                                    SystemConecto.ErorDebag(ErrorServer, 0);
                                }

                            }
                            finally
                            {
                                // Правильно завершить работу нитей (Thread) библиотеки.
                                if (hNotify != IntPtr.Zero)
                                {
                                    // ZGuard.ZGIntf.ZG_CloseNotification(hNotify);
                                }
                                // ZGuard.ZGIntf.ZG_Finalyze();
                            }
                            break;
                    }


                }


            }

            return ml_nCvtCount > 0 ? true : false;

        }



        #endregion




        #region Открыть порт в конверторе
        /// <summary>
        /// Открыть порт в конверторе
        /// </summary>
        /// <param name="CvtPortName">Имя порта</param>
        /// <returns></returns>
        private static bool OpenPortConvert(ref ColumnsConvertSKD ParamConvertSKD)
        {
            // ZPort.ZP_PORT_TYPE CvtPortType
            // public const ZPort.ZP_PORT_TYPE CvtPortType = ZPort.ZP_PORT_TYPE.ZP_PORT_COM;
            // string CvtPortName = "COM29"
            // Информация о конвертере, возвращаемая функциями: ZG_Cvt_Open, ZG_Cvt_AttachPort и ZG_Cvt_GetInformation.
            ZGuard.ZG_CVT_INFO rInfo = new ZGuard.ZG_CVT_INFO();
            // Параметры открытия конвертера, используемые функциями: ZG_Cvt_Open и ZG_UpdateCvtFirmware.
            ZGuard.ZG_CVT_OPEN_PARAMS rOp = new ZGuard.ZG_CVT_OPEN_PARAMS();
            rOp.nPortType = ParamConvertSKD.PortType; // CvtPortType;
            rOp.pszName = ParamConvertSKD.PortFr; // CvtPortName
            // Скорость конвертера Z-397 и Z-397_Guard в режиме Normal.
            rOp.nSpeed = ParamConvertSKD.PortFrSpeed; // ZGuard.ZG_CVT_SPEED.ZG_SPEED_57600;

            nRet = ZGuard.ZGIntf.ZG_Cvt_Open(ref m_hCvt, ref rOp, rInfo);
            if (nRet < 0)
            {
                ErrorServer = string.Format(" - Сервер СКД: ZG_Cvt_Open ({0}).", nRet);
                SystemConecto.ErorDebag(ErrorServer, 0);
                return false;
            }
            return true;
        }

        #endregion

        #region Читение списка контроллеров для конвертора



        #endregion

        #region Проверка версии SDK для конвертеров  установленной в системных папках (в виде dll приложения-библиотеки)
        /// <summary>
        /// Проверка версии SDK для конвертера «Z397», «Z397-Guard» и «Z397-IP»  установленной в системных папках
        /// </summary>
        /// <returns></returns>
        private static bool CheckSDK(string TypeConvert)
        {
            // Тип конвертора
            switch (TypeConvert)
            {
                case "Z397":
                    
                    // Проверка библиотек
                    Dictionary<string, string> dllList = new Dictionary<string, string>();
                    dllList.Add(SystemConecto.PutchApp + @"bin\dll\ZGuard.dll", "");
                    dllList.Add(SystemConecto.PutchApp + @"bin\dll\ZPort.dll", "");
                    if(SystemConecto.IsFilesPRG(dllList, -1 , "- Сервер СКД:") != "True"){


                        return false;
                    }
                    

                    UInt32 nVersion = new UInt32();

                    nVersion = ZGuard.ZGIntf.ZG_GetVersion();

                    UInt32 nVerMajor = (nVersion & 0xFF);
                    UInt32 nVerMinor = ((nVersion >> 8) & 0xFF);
                    UInt32 nVerBuild = ((nVersion >> 16) & 0xFF);
                    if ((nVerMajor != ZGuard.ZGIntf.ZG_SDK_VER_MAJOR) || (nVerMinor != ZGuard.ZGIntf.ZG_SDK_VER_MINOR))
                    {
                        ErrorServer = " - Сервер СКД: Неправильная версия SDK. ZGuard v" + nVerMajor + "." + nVerMinor + "." + nVerBuild;
                        SystemConecto.ErorDebag(ErrorServer, 0);
                        return false;
                    }

                 break;
                default:
                    // Неизвестный тип
                return false;


            }
            
           
            return true;
        }


        #endregion

        #region Инициализируем библиотеку SDK для конвертеров установленной в системных папках (в виде dll приложения-библиотеки)
        /// <summary>
        /// Инициализируем библиотеку SDK для конвертеров («Z397», «Z397-Guard» и «Z397-IP»), установленной в системных папках
        /// </summary>
        /// <returns></returns>
        private static bool InitializeSDKDll(string TypeConvert, ref int nRet, ref IntPtr hNotify)
        {
            // Тип конвертора
            switch (TypeConvert)
            {
                case "Z397":

                    // 
                     nRet = ZGuard.ZGIntf.ZG_Initialize(ZPort.ZPIntf.ZP_IF_NO_MSG_LOOP);
                    if (nRet < 0)
                    {
                        ErrorServer = " - Сервер СКД: ZG_Initialize (" + nRet + ").";
                        SystemConecto.ErorDebag(ErrorServer, 0);
                        return false;
                    }
                    
                    break;
                default:
                    // Неизвестный тип
                return false;

            }

            return true;
        }



        #endregion

    }

        #region Информация об конвертерах в памяти

        #region Формирование данных


        // Ресурсы данных для DataGrid для Form и Wpf можно формировать несколькими спосабами
        // 1. Object List - TableList
        // 2. DataTable - TableBD
        // 3. 

        // ========================================= Object List
        // Подключение данных this.dataGrid1.ItemsSource = DBConnect.viewAll();
        // Обновление         this.dataGrid1.Items.Refresh();

    /// <summary>
    /// Структура List (я подразумеваю колонки)
    /// </summary>
    /// <returns></returns>
    public class ColumnsConvertSKD : INotifyPropertyChanged // Used in Lists.
        {
            public int id { get; set; }                  // уникальный код
            // ===================== Пример полной формы записи
            //private double _seconds;
            //public double Seconds
            //{
            //    get { return _seconds; }
            //    set { _seconds = value; }
            //}
            private int _Pid_status;
            private string _NameNode;
            private string _Pid;

            
            // ---------------------------------------------- код подключения конвертора в систему (Читать и записывать)
            public int Pid_status
            {
                get { return _Pid_status; }
                set { 
                    // Проверка на изменения значения для уменьшения нагрузки выполнения задачь
                    if (value != this._Pid_status)
                    {
                        _Pid_status = value;
                        // Отследить изменения значения
                        SystemConfigControll.ControllerParamSKD(value.ToString(), string.Format("Convertor-{0}_Pid-status", this._Pid));
                        NotifyPropertyChanged("Pid_status");
                    }
                }
            }
            // ----------------------------------------      // Имя узла                            (Первый этаж)
            public string NameNode
            {
                get { return _NameNode; }
                set
                {
                    // Проверка на изменения значения для уменьшения нагрузки выполнения задачь
                    if (value != this._NameNode)
                    {
                        _NameNode = value;
                        // Отследить изменения значения
                        SystemConfigControll.ControllerParamSKD(value.ToString(), string.Format("Convertor-{0}_NameNode", this._Pid));
                        NotifyPropertyChanged("NameNode");
                    }
                }
            }    

            // =================
            /// <summary>
            /// Тип порта - pInfo.nPortType
            /// </summary>
            public dynamic PortType { get; set; }           // Тип порта 
            public string PortName { get; set; }            // Имя порта                                        (RF005Nxr)
            public string PortFr { get; set; }              // Имя дружественного порта PORTCOM FriendlyName    (COM29)
            /// <summary>
            /// Скорость порта  перечисление SDK  конвертора
            /// </summary>    
            public dynamic PortFrSpeed { get; set; }        // Скорость порта  перечисление SDK  конвертора                                
            public int nPortFrSpeed { get; set; }           // Скорость порта  цифра                           19200 / 57600
            public bool Status { get; set; }                // индикатор занятости                              (False;True)
            //FriendlyBusy    TRUE, если дружественный порт занят.
            public string Pid                               // код (Читать и записывать)                        (1237h)
            {
                get { return _Pid; }
                set { _Pid = value; }
            }

            public int Sn { get; set; }                      // Серийный номер конвертера 
            public string Version { get; set; }                // Версия конвертера
            //Flags         Флаги: бит 0 - "VCP", бит 1 - "WEB", 0xFF - "All".
            public string TypeConvert { get; set; }         // Тип конвертера  int Type + TypeStrs[(int)pInfo.nPortType]
            public dynamic Mode { get; set; }               // режим работы конвертора (Читать и записывать)
            
            public string Comment { get; set; }                // комментарий (Читать и записывать)

            // Тайм-аут и Количество попыток отправить запрос.  ZG_WAIT_SETTINGS
            // Настраиваем параметры ожидания ответа
            // ZGuard.ZG_WAIT_SETTINGS rWS = new ZGuard.ZG_WAIT_SETTINGS(300, 1, IntPtr.Zero);


            //PortType      Тип порта. 
            //PortName      Имя порта. 
            //FriendlyName  Дружественное имя порта. 
            //Busy    TRUE, если порт занят. 
            //FriendlyBusy    TRUE, если дружественный порт занят. 
            //Pid           PID устройства USB-конвертера. 
            //Sn            С/н конвертера. 
            //Version       Версия конвертера. 
            //Flags         Флаги: бит 0 - "VCP", бит 1 - "WEB", 0xFF - "All". 
            //Type          Тип конвертера. 
            //Mode          Режим работы конвертера Guard.


            public event PropertyChangedEventHandler PropertyChanged;
            // This method is called by the Set accessor of each property.
            // The CallerMemberName attribute that is applied to the optional propertyName
            // parameter causes the property name of the caller to be substituted as an argument. [CallerMemberName]
            private void NotifyPropertyChanged(String propertyName = "")
            {
                // Вариант записи
                //PropertyChangedEventHandler handler = PropertyChanged;
                //if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
                
                
                if (PropertyChanged != null)
                {
                    //MessageBox.Show(propertyName);
                    PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
                }
            }
        }


    /// <summary>
    /// Структура List (я подразумеваю колонки)
    /// </summary>
    /// <returns></returns>
    public class ColumnsControllerSKD : INotifyPropertyChanged // Used in Lists.
    {
        public int id { get; set; }                  // уникальный код
        // ===================== Пример полной формы записи
        //private double _seconds;
        //public double Seconds
        //{
        //    get { return _seconds; }
        //    set { _seconds = value; }
        //}
        private int _Pid_status;
        private string _NameNode;
        private string _Pid;


        // ---------------------------------------------- код подключения конвертора в систему (Читать и записывать)
        public int Pid_status
        {
            get { return _Pid_status; }
            set
            {
                // Проверка на изменения значения для уменьшения нагрузки выполнения задачь
                if (value != this._Pid_status)
                {
                    _Pid_status = value;
                    // Отследить изменения значения
                    SystemConfigControll.ControllerParamSKD(value.ToString(), string.Format("Convertor-{0}_Pid-status", this._Pid));
                    NotifyPropertyChanged("Pid_status");
                }
            }
        }
        // ----------------------------------------      // Имя узла                            (Первый этаж)
        public string NameNode
        {
            get { return _NameNode; }
            set
            {
                // Проверка на изменения значения для уменьшения нагрузки выполнения задачь
                if (value != this._NameNode)
                {
                    _NameNode = value;
                    // Отследить изменения значения
                    SystemConfigControll.ControllerParamSKD(value.ToString(), string.Format("Convertor-{0}_NameNode", this._Pid));
                    NotifyPropertyChanged("NameNode");
                }
            }
        }

        // =================
        public dynamic PortType { get; set; }               // Тип порта 
        public string PortName { get; set; }            // Имя порта                                        (RF005Nxr)
        public string PortFr { get; set; }              // Имя дружественного порта PORTCOM FriendlyName    (COM29)
        /// <summary>
        /// Скорость порта  перечисление SDK  конвертора
        /// </summary>
        public dynamic PortFrSpeed { get; set; }        // Скорость порта  перечисление SDK  конвертора                                
        public int nPortFrSpeed { get; set; }           // Скорость порта  цифра                           19200 / 57600
        public bool Status { get; set; }                // индикатор занятости                              (False;True)
        //FriendlyBusy    TRUE, если дружественный порт занят.
        public string Pid                               // код (Читать и записывать)                        (1237h)
        {
            get { return _Pid; }
            set { _Pid = value; }
        }

        public int Sn { get; set; }                      // Серийный номер конвертера 
        public string Version { get; set; }                // Версия конвертера
        //Flags         Флаги: бит 0 - "VCP", бит 1 - "WEB", 0xFF - "All".
        public string TypeConvert { get; set; }         // Тип конвертера  int Type + TypeStrs[(int)pInfo.nPortType]
        public dynamic Mode { get; set; }               // режим работы конвертора (Читать и записывать)

        public string Comment { get; set; }                // комментарий (Читать и записывать)

        // Тайм-аут и Количество попыток отправить запрос.  ZG_WAIT_SETTINGS
        // Настраиваем параметры ожидания ответа
        // ZGuard.ZG_WAIT_SETTINGS rWS = new ZGuard.ZG_WAIT_SETTINGS(300, 1, IntPtr.Zero);


        //PortType      Тип порта. 
        //PortName      Имя порта. 
        //FriendlyName  Дружественное имя порта. 
        //Busy    TRUE, если порт занят. 
        //FriendlyBusy    TRUE, если дружественный порт занят. 
        //Pid           PID устройства USB-конвертера. 
        //Sn            С/н конвертера. 
        //Version       Версия конвертера. 
        //Flags         Флаги: бит 0 - "VCP", бит 1 - "WEB", 0xFF - "All". 
        //Type          Тип конвертера. 
        //Mode          Режим работы конвертера Guard.


        public event PropertyChangedEventHandler PropertyChanged;
        // This method is called by the Set accessor of each property.
        // The CallerMemberName attribute that is applied to the optional propertyName
        // parameter causes the property name of the caller to be substituted as an argument. [CallerMemberName]
        private void NotifyPropertyChanged(String propertyName = "")
        {
            // Вариант записи
            //PropertyChangedEventHandler handler = PropertyChanged;
            //if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));


            if (PropertyChanged != null)
            {
                //MessageBox.Show(propertyName);
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }

        #endregion

        #endregion

}
