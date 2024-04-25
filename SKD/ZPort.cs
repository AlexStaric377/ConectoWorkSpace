using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace ZPort
{
    #region типы
    // Типы считывателей
    public enum ZP_PORT_TYPE
    {
        ZP_PORT_UNDEF = 0,
        ZP_PORT_COM,        // Com-порт
        ZP_PORT_FT,         // Ft-порт (через ftd2xx.dll по с/н USB)
        ZP_PORT_IP,         // Ip-порт (через TCP, соединение с реальным? устройством)
        ZP_PORT_RETR,       // Ip-порт (через TCP, соединение с программой Ретранслятор ZRetr.exe)
        ZP_PORT_SIM         // Порт симулятора (через WM_COPYDATA)
    }

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate bool ZP_ENUMPORTSPROC(ref ZP_PORT_INFO pInfo, IntPtr pUserData);
    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate bool ZP_NOTIFYPROC(UInt32 nMsg, IntPtr nMsgParam, IntPtr pUserData);
    #endregion

    #region структуры

    // Информация о порте
    [StructLayout(LayoutKind.Explicit, CharSet = CharSet.Unicode)]
    public struct ZP_PORT_INFO
    {
        [FieldOffset(0)] 
        public ZP_PORT_TYPE nType;     // Тип порта
        [FieldOffset(4), MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
        public string szName;          // Имя порта
        [FieldOffset(68)] 
        public bool fBusy;            // True, если занят
        [FieldOffset(72), MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
        public string szFriendly;     // Дружественное имя порта
        [FieldOffset(136)]
        public UInt32 nLParam;       // Дополнительная информация
        // Для Ft-порта: LOWORD() = Vid, HIWORD() = Pid
        // Для Ip-порта: LPVOID() = указатель на расширенную структуру
    }
    // Информация об изменении состояния порта
    [StructLayout(LayoutKind.Explicit, CharSet = CharSet.Unicode)]
    public struct ZP_N_CHANGE_STATE
    {
        [FieldOffset(0)]
        public UInt32 nChangeMask;     // Маска изменений (бит0 Busy, бит1 Friendly)
        [FieldOffset(4)]
        public ZP_PORT_INFO rInfo;            // True, если занят
        [FieldOffset(144), MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
        public string szOldFriendly;          // Имя порта
    }
    // Параметры для уведомлений
    [StructLayout(LayoutKind.Explicit)]
    public struct ZP_NOTIFY_SETTINGS
    {
        [FieldOffset(0)]
        public UInt32 nNMask;                          // Маска типов уведомлений (см. _ZP_NOTIFY_SETTINGS в ZPort.h)
        [FieldOffset(4)]
        public ZPort.ZP_NOTIFYPROC pfnCallback;        // Callback-функция
        [FieldOffset(8)]
        public IntPtr pUserData;                       // Параметр для Callback-функции
        [FieldOffset(12), MarshalAs(UnmanagedType.LPArray)]
        public UInt16[] pPids;                         // Pid'ы USB-устройств
        [FieldOffset(16)]
        public int nPidCount;                          // Количество Pid'ов
        [FieldOffset(20), MarshalAs(UnmanagedType.LPArray)]
        public UInt16[] pIpDevs;                       // Типы IP-устройств, зарегистрированные функцией ZP_RegIpDevice
        [FieldOffset(24)]
        public int nIpDevCount;                        // Количество типов IP-устройств
        [FieldOffset(28)]
        public IntPtr hSvcStatus;                      // Дескриптор сервиса, полученный функцией 
        [FieldOffset(32)]
        public UInt32 nCheckUsbPeriod;                 // Период проверки состояния USB-портов (в миллисекундах) (=0 по умолчанию 5000)
        [FieldOffset(36)]
        public UInt32 nCheckIpPeriod;                  // Период проверки состояния IP-портов (в миллисекундах) (=0 по умолчанию 15000)
    }
    #endregion

    class ZPIntf
    {
        #region Константы
        public const int ZP_SDK_VER_MAJOR = 1;
        public const int ZP_SDK_VER_MINOR = 4;
        #endregion

        #region Константы
        public const int ZP_SUCCESS = 0;                    // Операция выполнена успешно
        public const int ZP_E_CANCELLED = 1;               // Отменено пользователем

        public const int ZP_E_INVALID_PARAM = -1;            // Неправильный параметр
        public const int ZP_E_OPEN_NOT_EXIST = -2;           // Порт не существует
        public const int ZP_E_OPEN_ACCESS = -3;              // Порт занят другой программой
        public const int ZP_E_OPEN_PORT = -4;                // Другая ошибка открытия порта
        public const int ZP_E_PORT_IO_ERROR = -5;            // Ошибка порта (Конвертор отключен от USB?)
        public const int ZP_E_PORT_SETUP = -6;               // Ошибка настройки порта
        public const int ZP_E_LOAD_FTD2XX = -7;              // Неудалось загрузить FTD2XX.DLL
        public const int ZP_E_INIT_SOCKET = -8;              // Не удалось инициализировать сокеты
        public const int ZP_E_SERVERCLOSE = -9;              // Дескриптор закрыт со стороны Сервера
        public const int ZP_E_NOT_ENOUGH_MEMORY = -10;       // Недостаточно памяти для обработки команды
        public const int ZP_E_UNSUPPORT = -11;               // Функция не поддерживается
        public const int ZP_E_NOT_INITALIZED = -12;          // Не проинициализировано с помощью ZP_Initialize
        public const int ZP_E_OTHER = -1000;                 // Другая ошибка
        #endregion

        #region Константы
        // ZP_Initialize Flags
        public const uint ZP_IF_NO_MSG_LOOP = 0x01;     // Приложение не имеет цикла обработки сообщений (Console or Service)
        #endregion

        #region Константы
        public const uint ZP_NF_EXIST = 0x01;           // ZP_N_INSERT / ZP_N_REMOVE
        public const uint ZP_NF_BUSY = 0x02;            // ZP_N_STATE_CHANGED
        public const uint ZP_NF_FRIENDLY = 0x04;        // ZP_N_STATE_CHANGED
        public const uint ZP_NF_ONLY_NOTIFY = 0x8000;   // Только уведомлять о добавлении новых сообщений в очередь (для перечисления и обработки сообщений используйте функцию ZP_ProcessMessages)

        public const uint ZP_N_INSERT = 1;              // Подключение порта (PZP_PORT_INFO(MsgParam) - инфо о порте)
        public const uint ZP_N_REMOVE = 2;              // Отключение порта (PZP_PORT_INFO(MsgParam) - инфо о порте)
        public const uint ZP_N_STATE_CHANGED = 3;       // Изменение состояния порта (PZP_N_CHANGE_STATE(MsgParam) - инфо об изменениях)
        #endregion
    }
}
