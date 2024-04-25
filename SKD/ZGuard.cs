using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace ZGuard
{
    #region типы
    // Тип конвертера
    public enum ZG_CVT_TYPE
    {
        ZG_CVT_UNDEF = 0,       // Не определено
        ZG_CVT_Z397,            // Z397 (Обычный)
        ZG_CVT_GUARD,           // Z397 Guard
        ZG_CVT_GUARD_IP,        // Z397 Guard IP
        ZG_CVT_RETR             // Программа-ретранслятор ZRetr (ретранслирует USB-конвертер, имитирует IP-конвертер)
    }
    // Режим конвертера Z397 Guard
    public enum ZG_GUARD_MODE
    {
        ZG_GUARD_UNDEF = 0,     // Не определено
        ZG_GUARD_NORMAL,        // Режим "Normal" (эмуляция обычного конвертера Z397)
        ZG_GUARD_ADVANCED,      // Режим "Advanced"
        ZG_GUARD_TEST,          // Режим "Test" (для специалистов)
        ZG_GUARD_ACCEPT         // Режим "Accept" (для специалистов)
    }
    // Скорость конвертера
    public enum ZG_CVT_SPEED
    {
        ZG_SPEED_19200 = 19200,
        ZG_SPEED_57600 = 57600
    }
    // Тип контроллера
    public enum ZG_CTR_TYPE
    {
        ZG_CTR_UNDEF = 0,       // Не определено
        ZG_CTR_GATE2K,			// Gate 2000
        ZG_CTR_MATRIX2NET,		// Matrix II Net
        ZG_CTR_Z5RNET,			// Z5R Net
        ZG_CTR_Z5RNET8K,		// Z5R Net 8000
        ZG_CTR_GUARDNET			// Guard Net
    }
    // Подтип контроллера
    public enum ZG_CTR_SUB_TYPE
    {
        ZG_CS_UNDEF = 0,        // Не определено
        ZG_CS_DOOR,				// Дверь
        ZG_CS_TURNSTILE,		// Турникет
        ZG_CS_GATEWAY,			// Шлюз
        ZG_CS_BARRIER			// Шлакбаум
    }
    // Тип ключа контроллера
    public enum ZG_CTR_KEY_TYPE
    {
        ZG_KEY_UNDEF = 0,       // Не определено
        ZG_KEY_NORMAL,		    // Обычный
        ZG_KEY_BLOCKING,	    // Блокирующий
        ZG_KEY_MASTER		    // Мастер
    }
    // Тип события контроллера
    public enum ZG_CTR_EV_TYPE
    {
        ZG_EV_UNKNOWN = 0,          // Не определено
        ZG_EV_BUT_OPEN,				// Открыто кнопкой изнутри
        ZG_EV_KEY_NOT_FOUND,		// Ключ не найден в банке ключей
        ZG_EV_KEY_OPEN,				// Ключ найден, дверь открыта
        ZG_EV_KEY_ACCESS,			// Ключ найден, доступ не разрешен
        ZG_EV_REMOTE_OPEN,			// Открыто оператором по сети
        ZG_EV_KEY_DOOR_BLOCK,		// Ключ найден, дверь заблокирована
        ZG_EV_BUT_DOOR_BLOCK,		// Попытка открыть заблокированную дверь кнопкой
        ZG_EV_NO_OPEN,				// Дверь взломана
        ZG_EV_NO_CLOSE,				// Дверь оставлена открытой (timeout)
        ZG_EV_PASSAGE,				// Проход состоялся
        ZG_EV_SENSOR1,				// Сработал датчик 1
        ZG_EV_SENSOR2,				// Сработал датчик 2
        ZG_EV_REBOOT,				// Перезагрузка контроллера
        ZG_EV_BUT_BLOCK,			// Заблокирована кнопка открывания
        ZG_EV_DBL_PASSAGE,			// Попытка двойного прохода
        ZG_EV_OPEN,					// Дверь открыта штатно
        ZG_EV_CLOSE,				// Дверь закрыта
        ZG_EV_POWEROFF,				// Пропало питание
        ZG_EV_ELECTRO_ON,			// Включение электропитания
        ZG_EV_ELECTRO_OFF,			// Выключение электропитания
        ZG_EV_LOCK_CONNECT,         // Включение замка (триггер)
        ZG_EV_LOCK_DISCONNECT,      // Отключение замка (триггер)
        ZG_EV_FIRE_STATE,           // Изменение состояния Пожара
        ZG_EV_SECUR_STATE,          // Изменение состояния Охраны
        ZG_EV_UNKNOWN_KEY,          // Неизвестный ключ
        ZG_EV_GATEWAY_PASS,         // Совершен вход в шлюз
        ZG_EV_GATEWAY_BLOCK,        // Заблокирован вход в шлюз (занят)
        ZG_EV_GATEWAY_ALLOWED,      // Разрешен вход в шлюз
        ZG_EV_ANTIPASSBACK          // Заблокирован проход (Антипассбек)
    }
    // Направление прохода контроллера
    public enum ZG_CTR_DIRECT
    {
        ZG_DIRECT_UNDEF = 0,    // Не определено
        ZG_DIRECT_IN,			// Вход
        ZG_DIRECT_OUT			// Выход
    }
    // Условие, вызвавшее событие ElectroControl: ZG_EV_ELECTRO_ON, ZG_EV_ELECTRO_OFF
    public enum ZG_EC_SUB_EV
    {
        ZG_EC_EV_UNDEF = 0,     // Не определено
        ZG_EC_EV_CARD_DELAY,	// Поднесена валидная карта с другой стороны (для входа) запущена задержка
        ZG_EC_EV_RESERVED1,		// (зарезервировано)
        ZG_EC_EV_ON_NET,		// Включено командой по сети
        ZG_EC_EV_OFF_NET,		// Выключено командой по сети
        ZG_EC_EV_ON_SCHED,		// Включено по временной зоне
        ZG_EC_EV_OFF_SHED,		// Выключено по временной зоне
        ZG_EC_EV_CARD,			// Поднесена валидная карта к контрольному устройству
        ZG_EC_EV_RESERVED2,		// (зарезервировано)
        ZG_EC_EV_OFF_TIMEOUT,	// Выключено после отработки таймаута
        ZG_EC_EV_OFF_EXIT		// Выключено по срабатыванию датчика выхода
    }
    // Условие, вызвавшее событие ZG_EV_FIRE_STATE
    public enum ZG_FIRE_SUB_EV
    {
        ZG_FR_EV_UNDEF = 0,     // Не определено
        ZG_FR_EV_OFF_NET,       // выключено по сети
        ZG_FR_EV_ON_NET,        // Включено по сети
        ZG_FR_EV_OFF_INPUT_F,   // Выключено по входу FIRE
        ZG_FR_EV_ON_INPUT_F,    // Включено по входу FIRE
        ZG_FR_EV_OFF_TEMP,      // Выключено по датчику температуры
        ZG_FR_EV_ON_TEMP        // Включено по датчику температуры
    }
    // Условие, вызвавшее событие ZG_EV_SECUR_STATE
    public enum ZG_SECUR_SUB_EV
    {
        ZG_SR_EV_UNDEF = 0,     // Не определено
        ZG_SR_EV_OFF_NET,       // выключено по сети
        ZG_SR_EV_ON_NET,        // Включено по сети
        ZG_SR_EV_OFF_INPUT_A,   // Выключено по входу ALARM
        ZG_SR_EV_ON_INPUT_A,    // Включено по входу ALARM
        ZG_FR_EV_OFF_TAMPERE,   // Выключено по тамперу
        ZG_FR_EV_ON_TAMPERE,    // Включено по тамперу
        ZG_FR_EV_OFF_DOOR,      // Выключено по датчику двери
        ZG_FR_EV_ON_DOOR        // Включено по датчику двери
    }
    // Режим Охрана
    public enum ZG_SECUR_MODE
    {
        ZG_SR_M_UNDEF = 0,      // Не определено
        ZG_SR_M_SECUR_OFF,      // Выключить режим охраны
        ZG_SR_M_SECUR_ON,       // Включить режим охраны
        ZG_SR_M_ALARM_OFF,      // Выключить тревогу
        ZG_SR_M_ALARM_ON        // Включить тревогу
    }

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate bool ZG_ENUMCVTSPROC(ref ZG_ENUM_CVT_INFO pInfo, IntPtr pUserData);
    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate bool ZG_PROCESSCALLBACK(int nPos, int nMax, IntPtr pUserData);
    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate bool ZG_ENUMCTRSPROC(ref ZG_FIND_CTR_INFO pInfo, int nPos, int nMax, IntPtr pUserData);
    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate bool ZG_ENUMCTRTIMEZONESPROC(int nIdx, ref ZG_CTR_TIMEZONE pTz, IntPtr pUserData);
    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate bool ZG_ENUMCTRKEYSPROC(int nIdx, ref ZG_CTR_KEY pKey, int nPos, int nMax, IntPtr pUserData);
    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate bool ZG_ENUMCTREVENTSPROC(int nIdx, ref ZG_CTR_EVENT pEvent, int nPos, int nMax, IntPtr pUserData);
    #endregion

    #region структуры
    // Информация об IP-конвертере
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct ZG_IP_PORT_EXINFO
    {
        public ZG_CVT_TYPE nType;                      // Тип IP-конвертера
        public UInt16 nSn;                             // с/н конвертера
        public UInt16 nVersion;                        // Версия конвертера
        public UInt32 nFlags;                          // Флаги: бит 0 - "VCP", бит 1 - "WEB", 0xFF - "All"
        public UInt16 nL1Port;                         // TCP-порт клиента линии №1 (=0, если свободна)
        public UInt16 nL2Port;                         // TCP-порт клиента линии №2 (=0, если свободна)
        public UInt32 nL1IP;                           // IP клиента линии №1
        public UInt32 nL2IP;                           // IP клиента линии №2
    }
    // Настройки ожидания исполнения функций
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct ZG_WAIT_SETTINGS
    {
        public UInt32 nReplyTimeout;                   // Тайм-аут ожидания ответа на запрос конвертеру
        public int nMaxTry;                            // Количество попыток отправить запрос
        public IntPtr hCancelEvent;                    // Дескриптор стандартного объекта Event для отмены функции
        public ZG_WAIT_SETTINGS(UInt32 _nReplyTimeout, int _nMaxTry, IntPtr _hCancelEvent)
        {
            nReplyTimeout = _nReplyTimeout;
            nMaxTry = _nMaxTry;
            hCancelEvent = _hCancelEvent;
        }
    }
    [StructLayout(LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Unicode)]
    public struct ZG_ENUM_CVT_INFO
    {
        public ZPort.ZP_PORT_TYPE nPortType;    // Тип порта
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
        public string szPortName;               // Имя порта
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
        public string szFriendlyName;           // Дружественное имя порта
        public bool fBusy;                      // True, если занят
        public bool fFriendlyBusy;

        // Только для ZP_PORT_VCOM и ZP_PORT_FTSN
        public UInt32 nPid;

        // Только для ZP_PORT_IP и ZP_PORT_RETR, и для функции ZG_EnumConvertersEx
        public UInt16 nSn;                      // с/н конвертера
        public UInt16 nVersion;                 // Версия конвертера
        public UInt32 nFlags;                   // Флаги: бит 0 - "VCP", бит 1 - "WEB", 0xFF - "All"

        public ZG_CVT_TYPE nType;               // Тип конвертера
        public ZG_GUARD_MODE nMode;             // Режим работы конвертера Guard
    }
    // Информация о конвертере, возвращаемая функциями: ZG_Cvt_Open и ZG_Cvt_GetInformation
    [StructLayout(LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Unicode)]
    public class ZG_CVT_INFO
    {
       public ZG_CVT_TYPE nType;                // Тип конвертера
       public ZG_CVT_SPEED nSpeed;              // Скорость конвертера
       public UInt16 nSn;                       // с/н конвертера
       public UInt16 nVersion;                  // Версия конвертера
       public ZG_GUARD_MODE nMode;              // Режим работы конвертера Guard
       [MarshalAs(UnmanagedType.LPTStr)]
       public string pszLinesBuf;               // Буфер для информационных строк
       public int nLinesBufMax;                 // Размер буфера в символах, включая завершающий '\0'
    }
    // Параметры открытия конвертера, используемые функциями: ZG_Cvt_Open и ZG_UpdateCvtFirmware
    [StructLayout(LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Unicode)]
    public struct ZG_CVT_OPEN_PARAMS
    {
        public ZPort.ZP_PORT_TYPE nPortType;    // Тип порта
        [MarshalAs(UnmanagedType.LPTStr)]
        public string pszName;                  // Имя порта. Если =NULL, то используется hPort
        public IntPtr hPort;                    // Дескриптор порта, полученный функцией ZP_Open
        public ZG_CVT_TYPE nCvtType;            // Тип конвертера. Если =ZG_CVT_UNDEF, то автоопределение
        public ZG_CVT_SPEED nSpeed;             // Скорость конвертера
        public IntPtr pWait;                    // Параметры ожидания. Может быть =NULL.
        public int nLicN;                       // Номер лицензии. Если =0, то используется ZG_DEF_CVT_LICN
        public ZG_CVT_OPEN_PARAMS(ZPort.ZP_PORT_TYPE _nType, string _sName, IntPtr _hPort, ZG_CVT_TYPE _nCvtType,
            ZG_CVT_SPEED _nSpeed, IntPtr _pWait = default(IntPtr), int _nLicN = 0)
        {
            nPortType = _nType;
            pszName = _sName;
            hPort = _hPort;
            nCvtType = _nCvtType;
            nSpeed = _nSpeed;
            pWait = _pWait;
            nLicN = _nLicN;
        }
    }
    // Информация о лицензии конвертера Guard
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct ZG_CVT_LIC_INFO
    {
        public UInt16 nStatus;                         // Статус лицензии
        UInt16 Reserved;
        public int nMaxCtrs;                           // Максимальное количество контроллеров
        public int nMaxKeys;                           // Максимальное количество ключей
        public UInt16 nMaxYear;                        // Дата: год (= 0xFFFF дата неограничена)
        public UInt16 nMaxMon;                         // Дата: месяц
        public UInt16 nMaxDay;                         // Дата: день
        public UInt16 nDownCountTime;                  // Счетчик
    }
    // Информация о найденном контроллере, возвращаемая функцией ZG_Cvt_FindNextCtr
    [StructLayout(LayoutKind.Sequential, Pack=1)]
    public struct ZG_FIND_CTR_INFO
    {
        public ZG_CTR_TYPE nType;                      // Тип контроллера
        public Byte nTypeCode;                         // Код типа контроллера
        public Byte nAddr;                             // Сетевой адрес
        public UInt16 nSn;                             // Заводской номер
        public UInt16 nVersion;                        // Версия прошивки
        public int nMaxKeys;                           // Максимум ключей
        public int nMaxEvents;                         // Максимум событий
        public UInt32 nFlags;                          // Флаги контроллера (ZG_CTR_F_...)
        public ZG_CTR_SUB_TYPE nSubType;               // Подтип контроллера
    }
    // Информация о контроллере, возвращаемая функциями: ZG_Ctr_Open и ZG_Ctr_GetInformation
    [StructLayout(LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Unicode)]
    public struct ZG_CTR_INFO
    {
        public ZG_CTR_TYPE nType;                      // Тип контроллера
        public Byte nTypeCode;                         // Код типа контроллера
        public Byte nAddr;                             // Сетевой адрес
        public UInt16 nSn;                             // Заводской номер
        public UInt16 nVersion;                        // Версия прошивки
        public int nInfoLineCount;                     // Количество строк с информацией
        public int nMaxKeys;                           // Максимум ключей
        public int nMaxEvents;                         // Максимум событий
        public UInt32 nFlags;                          // Флаги контроллера (ZG_CTR_F_...)
        UInt16 Reserved;
        [MarshalAs(UnmanagedType.LPTStr)]
        public string pszLinesBuf;                     // Буфер для информационных строк
        public int nLinesBufMax;                       // Размер буфера в символах, включая завершающий '\0'
        public ZG_CTR_SUB_TYPE nSubType;               // Подтип контроллера
        public int nOptReadItems;                      // Количество элементов, которое может быть считано одним запросом контроллеру 
        public int nOptWriteItems;                     // Количество элементов, которое может быть записано одним запросом контроллеру
    }
    // Временная зона контроллера
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct ZG_CTR_TIMEZONE
    {
        public Byte nDayOfWeeks;                       // Дни недели
        public Byte nBegHour;                          // Начало: час
        public Byte nBegMinute;                        // Начало: минута
        public Byte nEndHour;                          // Конец: час
        public Byte nEndMinute;                        // Конец: минута
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
        Byte[] Reserved;
        public ZG_CTR_TIMEZONE(Byte _nDayOfWeeks, Byte _nBegHour, Byte _nBegMinute, Byte _nEndHour, Byte _nEndMinute)
        {
            nDayOfWeeks = _nDayOfWeeks;
            nBegHour = _nBegHour;
            nBegMinute = _nBegMinute;
            nEndHour = _nEndHour;
            nEndMinute = _nEndMinute;
            Reserved = new Byte[3];
        }
    }
    // Ключ контроллера
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct ZG_CTR_KEY
    {
        public bool fErased;                           // TRUE, если ключ стерт
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
        public Byte[] rNum;                            // Номер ключа
        public ZG_CTR_KEY_TYPE nType;                  // Тип ключа
        public UInt32 nFlags;                          // Флаги ZG_KF_...
        public UInt32 nAccess;                         // Доступ (маска временных зон)
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
        public Byte[] aData1;                          // Другие данные ключа
        public ZG_CTR_KEY(bool _fErased, [In] Byte[] _rNum, ZG_CTR_KEY_TYPE _nType, UInt32 _nFlags, UInt32 _nAccess, [In] Byte[] _aData1)
        {
            fErased = _fErased;
            rNum = _rNum;
            nType = _nType;
            nFlags = _nFlags;
            nAccess = _nAccess;
            aData1 = _aData1;
        }
    }
    // Часы контроллера
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct ZG_CTR_CLOCK
    {
        public bool fStopped;                          // TRUE, если часы остановлены
        public UInt16 nYear;                           // Год
        public UInt16 nMonth;                          // Месяц
        public UInt16 nDay;                            // День
        public UInt16 nHour;                           // Час
        public UInt16 nMinute;                         // Минута
        public UInt16 nSecond;                         // Секунда
        public ZG_CTR_CLOCK(bool _fStopped, UInt16 _nYear, UInt16 _nMonth, UInt16 _nDay, UInt16 _nHour, UInt16 _nMinute, UInt16 _nSecond)
        {
            fStopped = _fStopped;
            nYear = _nYear;
            nMonth = _nMonth;
            nDay = _nDay;
            nHour = _nHour;
            nMinute = _nMinute;
            nSecond = _nSecond;
        }
    }
    // Событие контроллера
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct ZG_CTR_EVENT
    {
        public ZG_CTR_EV_TYPE nType;                    // Тип события
        //public Byte nEvCode;                            // Код события в контроллере
        //[MarshalAs(UnmanagedType.ByValArray, SizeConst = 7)]
        //public Byte[] aParams;                          // Параметры события
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
        public Byte[] aData;                            // Данные события 
        // (используйте функцию декодирования, соответстующую типу события,
        // ZG_Ctr_DecodePassEvent, ZG_Ctr_DecodeEcEvent, ZG_Ctr_DecodeUnkKeyEvent)
    }
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct ZG_EV_TIME
    {
        public Byte nMonth;                             // Месяц
        public Byte nDay;                               // День
        public Byte nHour;                              // Час
        public Byte nMinute;                            // Минута
        public Byte nSecond;                            // Секунда
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
        Byte[] Reserved;
    }
    // Конфигурация управления электропитанием
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct ZG_CTR_ELECTRO_CONFIG
    {
        public UInt32 nPowerConfig;                    // Конфигурация управления питанием
        public UInt32 nPowerDelay;                     // Время задержки в секундах
        public ZG_CTR_TIMEZONE rTz6;                   // Временная зона №6 (считаем от 0)
        public ZG_CTR_ELECTRO_CONFIG(UInt32 _nPowerConfig, UInt32 _nPowerDelay, ZG_CTR_TIMEZONE _rTz6)
        {
            nPowerConfig = _nPowerConfig;
            nPowerDelay = _nPowerDelay;
            rTz6 = _rTz6;
        }
    }
    // Состояние электропитания
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct ZG_CTR_ELECTRO_STATE
    {
        public UInt32 nPowerFlags;                     // Флаги состояния электропитания
        public UInt32 nPowerConfig;                    // Конфигурация управления питанием
        public UInt32 nPowerDelay;                     // Время задержки в секундах
    }
    // Параметры для уведомлений
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct ZG_NOTIFY_SETTINGS
    {
        public UInt32 nNMask;                          // Маска типов уведомлений (см. _ZP_NOTIFY_SETTINGS в ZPort.h)
        public ZPort.ZP_NOTIFYPROC pfnCallback;        // Callback-функция
        public IntPtr pUserData;                       // Параметр для Callback-функции
        public UInt32 nDevTypes;                       // Маска типов устройств USB и(или) IP (ZG_DF_...)
        public ZG_NOTIFY_SETTINGS(UInt32 _nNMask, ZPort.ZP_NOTIFYPROC _pfnCallback, IntPtr _pUserData, UInt32 _nDevTypes)
        {
            nNMask = _nNMask;
            pfnCallback = _pfnCallback;
            pUserData = _pUserData;
            nDevTypes = _nDevTypes;
        }
    }
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct ZG_N_CTR_CHANGE_INFO
    {
        public UInt32 nChangeMask;                      // Маска изменений (бит0 addr, бит1 version, бит2 proximity)
        public ZG_FIND_CTR_INFO rCtrInfo;               // Измененная информация о контроллере
        public UInt16 nOldVersion;                      // Старое значение версии
        public Byte nOldAddr;                           // Старое значение адреса
        Byte Reserved;                                  // Зарезервировано для выравнивания структуры
    }
    // Параметры для уведомлений от конвертера
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public class ZG_CVT_NOTIFY_SETTINGS
    {
        public UInt32 nNMask;                          // Маска типов уведомлений (см. _ZP_NOTIFY_SETTINGS в ZPort.h)
        public ZPort.ZP_NOTIFYPROC pfnCallback;        // Callback-функция
        public IntPtr pUserData;                       // Параметр для Callback-функции
        public UInt32 nScanCtrsPeriod;                 // Период сканирования списка контроллеров в мс (=0 использовать значение по умолчанию, 5000)
		public int nScanCtrsLastAddr;                  // Последней сканируемый адрес контроллера (для простого конвертера Z-397)
        public ZG_CVT_NOTIFY_SETTINGS(UInt32 _nNMask, ZPort.ZP_NOTIFYPROC _pfnCallback, IntPtr _pUserData, UInt32 _nScanCtrsPeriod, int _nScanCtrsLastAddr)
        {
            nNMask = _nNMask;
            pfnCallback = _pfnCallback;
            pUserData = _pUserData;
            nScanCtrsPeriod = _nScanCtrsPeriod;
            nScanCtrsLastAddr = _nScanCtrsLastAddr;
        }
    }
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct ZG_N_NEW_EVENT_INFO
    {
        public int nNewCount;                          // Количество новых событий
        public int nWriteIdx;                          // Указатель записи
        public int nReadIdx;                           // Указатель чтения
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
        public Byte[] rLastNum;                        // Номер последнего поднесенного ключа
    }
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct ZG_N_KEY_TOP_INFO
    {
        public int nBankN;                             // Номер банка ключей
        public int nNewTopIdx;                         // Новое значение верхней границы ключей
        public int nOldTopIdx;                         // Старое значение верхней границы ключей
    }
    // Параметры для уведомлений от контроллера
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public class ZG_CTR_NOTIFY_SETTINGS
    {
        public UInt32 nNMask;                          // Маска типов уведомлений (см. _ZP_NOTIFY_SETTINGS в ZPort.h)
        public ZPort.ZP_NOTIFYPROC pfnCallback;        // Callback-функция
        public IntPtr pUserData;                       // Параметр для Callback-функции
        public int nReadEvIdx;                         // Указатель чтения событий
        public UInt32 nCheckStatePeriod;               // Период проверки состояния контроллера в мс (=0 использовать значение по умолчанию, 1000): часы, указатели событий, верхняя граница ключей
        public UInt32 nClockOffs;                      // Смещение часов контроллера от часов ПК в секундах
        public ZG_CTR_NOTIFY_SETTINGS(UInt32 _nNMask, ZPort.ZP_NOTIFYPROC _pfnCallback, IntPtr _pUserData, int _nReadEvIdx, UInt32 _nCheckStatePeriod, UInt32 _nClockOffs)
        {
            nNMask = _nNMask;
            pfnCallback = _pfnCallback;
            pUserData = _pUserData;
            nReadEvIdx = _nReadEvIdx;
            nCheckStatePeriod = _nCheckStatePeriod;
            nClockOffs = _nClockOffs;
        }
    }
    #endregion

    class ZGIntf
    {
        #region Константы (совместимая версия SDK)
        public const int ZG_SDK_VER_MAJOR = 3;
        public const int ZG_SDK_VER_MINOR = 15;
        #endregion

        #region Константы (коды ошибок)
        public const int ZG_SUCCESS = 0;                    // Операция выполнена успешно
        public const int ZG_E_CANCELLED = 1;                // Отменено пользователем
        public const int ZG_E_NO_MORE_ITEMS = 2;            // Нет больше элементов в списке

        public const int ZG_E_INVALID_PARAM = -1;           // Неправильный параметр
        public const int ZG_E_OPEN_NOT_EXIST = -2;          // Порт не существует
        public const int ZG_E_OPEN_ACCESS = -3;             // Порт занят другой программой
        public const int ZG_E_OPEN_PORT = -4;               // Другая ошибка открытия порта
        public const int ZG_E_PORT_IO_ERROR = -5;           // Ошибка порта (Конвертор отключен от USB?)
        public const int ZG_E_PORT_SETUP = -6;              // Ошибка настройки порта
        public const int ZG_E_LOAD_FTD2XX = -7;             // Неудалось загрузить FTD2XX.DLL
        public const int ZG_E_INIT_SOCKET = -8;             // Не удалось инициализировать сокеты
        public const int ZG_E_SERVERCLOSE = -9;             // Дескриптор закрыт со стороны Сервера
        public const int ZG_E_NOT_ENOUGH_MEMORY = -10;      // Недостаточно памяти для обработки команды
        public const int ZG_E_UNSUPPORT = -11;              // Функция не поддерживается
        public const int ZG_E_NOT_INITALIZED = -12;         // Не проинициализировано с помощью ZP_Initialize

        public const int ZG_E_TOO_LARGE_MSG = -100;         // Слишком большое сообщение для отправки
        public const int ZG_E_INSUFFICIENT_BUFFER = -101;   // Размер буфера слишком мал
        public const int ZG_E_NO_ANSWER = -102;             // Нет ответа
        public const int ZG_E_BAD_ANSWER = -103;            // Нераспознанный ответ
        public const int ZG_E_ONLY_GUARD = -104;            // Функция работает только с конвертером Guard
        public const int ZG_E_BAD_VALUE = -105;             // Неправильное значение (в памяти контроллера)
        public const int ZG_E_DEPEND_EXIST = -106;          // Нельзя закрыть дескриптор если открыты зависимые от него дескрипторы

        public const int ZG_E_G_ONLY_ADVANCED = -200;       // Функция работает только с конвертером Guard в режиме Advanced

        public const int ZG_E_G_OTHER = -250;               // Другая ошибка конвертера
        public const int ZG_E_G_LIC_OTHER = -251;           // Другая ошибка лицензии
        public const int ZG_E_G_LIC_NOT_FOUND = -252;       // Ошибка конвертера: Нет такой лицензии
        public const int ZG_E_G_LIC_EXPIRED = -253;         // Текущая лицензия истекла
        public const int ZG_E_G_LIC_CTR_LIM = -254;         // Ошибка конвертера: ограничение лицензии на число
        public const int ZG_E_G_LIC_RKEY_LIM = -255;        // Ограничение лицензии на число ключей при чтении
        public const int ZG_E_G_LIC_WKEY_LIM = -256;        // Ограничение лицензии на число ключей при записи
        public const int ZG_E_G_LIC_EXPIRED2 = -257;        // Срок лицензии истек (определено при установке даты в контроллере)

        public const int ZG_E_G_BAD_CS = -270;              // Ошибка в контрольной сумме пакета
        public const int ZG_E_G_CTR_NOT_FOUND = -271;       // Неверный адрес контроллера (контроллер не найден)
        public const int ZG_E_G_CMD_UNSUPPORT = -272;       // Команда неподдерживается

        public const int ZG_E_CTR_NACK = -300;              // Контроллер отказал в выполнении команды
        public const int ZG_E_CTR_TRANSFER = -301;          // Конвертер не смог доставить команду контроллеру
        public const int ZG_E_BOOTLOADER_NOSTART = -302;    // Bootloader not started
        public const int ZG_E_FIRMWARE_FILESIZE = -303;     // Filesize does not match the request
        public const int ZG_E_FIRMWARE_NOSTART = -304;      // Not found running firmware. Try restarting the device.

        public const int ZG_E_FW_NO_COMPATIBLE = -305;      // Not compatible for this device
        public const int ZG_E_FW_INVALID_DEV_NUM = -306;    // Not suitable for this device number
        public const int ZG_E_FW_TOOLARGE = -307;           // Too large data firmware can be a mistake
        public const int ZG_E_FW_SEQUENCE_DATA = -308;      // Violated the sequence data
        public const int ZG_E_FW_DATA_INTEGRITY = -309;     // Compromise data integrity
        public const int ZG_E_FW_OTHER = -350;              // Другая ошибка при перепрошивке

        public const int ZG_E_OTHER = -1000;                // Другая ошибка
        #endregion

        #region Константы
        public const uint ZG_DEVTYPE_GUARD = 1;
        public const uint ZG_DEF_CVT_LICN = 5;              // Номер лицензии конвертера по умолчанию
        public const int ZG_MAX_TIMEZONES = 7;              // Максимум временных зон
        #endregion

        #region Константы (флаги для уведомлений конвертера)
        public const uint ZG_NF_CVT_CTR_EXIST = 0x01;       // ZG_N_CVT_CTR_INSERT / ZG_N_CVT_CTR_REMOVE
        public const uint ZG_NF_CVT_CTR_CHANGE = 0x02;      // Изменение параметров контроллера ZG_N_CVT_CTR_CHANGE
        public const uint ZG_NF_CVT_REASSIGN_ADDRS = 0x2000;// Автоматическое переназначение адресов контроллеров (кроме Guard Advanced) (работает только с ZG_NF_CVT_CTR_EXIST)
        public const uint ZG_NF_CVT_ONLY_NOTIFY = 0x8000;   // Только уведомлять о добавлении новых сообщений в очередь
        public const uint ZG_NF_CVT_RESCAN_CTRS = 0x10000;  // Начать заново сканирование контроллеров (для простого конвертера, не Guard)

        public const uint ZG_N_CVT_CTR_INSERT = 1;          // Контроллер подключен PZG_FIND_CTR_INFO(MsgParam) - информация о контроллере
        public const uint ZG_N_CVT_CTR_REMOVE = 2;          // Контроллер отключен PZG_FIND_CTR_INFO(MsgParam) - информация о контроллере
        public const uint ZG_N_CVT_CTR_CHANGE = 3;          // Изменены параметры контроллера PZG_N_CTR_CHANGE_INFO(MsgParam)
        #endregion

        #region Константы (флаги для уведомлений контроллера)
        public const uint ZG_NF_CTR_NEW_EVENT = 0x01;       // ZG_N_CTR_NEW_EVENT
        public const uint ZG_NF_CTR_CLOCK = 0x02;           // ZG_N_CTR_CLOCK
        public const uint ZG_NF_CTR_KEY_TOP = 0x04;         // ZG_N_CTR_KEY_TOP
        public const uint ZG_NF_CTR_ADDR_CHANGE = 0x08;     // ZG_N_CTR_ADDR_CHANGE
        public const uint ZG_NF_CTR_ONLY_NOTIFY = 0x8000;   // Только уведомлять о добавлении новых сообщений в очередь

        public const uint ZG_N_CTR_NEW_EVENT = 1;           // Новые события PZG_N_NEW_EVENT_INFO(MsgParam) - информация
        public const uint ZG_N_CTR_CLOCK = 2;               // Величина рассинхронизации в секундах PINT64(MsgParam)
        public const uint ZG_N_CTR_KEY_TOP = 3;             // Изменилась верхняя граница ключей PZG_N_KEY_TOP_INFO(MsgParam) - информация
        public const uint ZG_N_CTR_ADDR_CHANGE = 4;         // Изменен сетевой адрес контроллера MsgParam = NewAddr
        #endregion

        #region Константы
        // Тип устройств (для EnumConverters, EnumConvertersEx и FindNotification)
        public const uint ZG_DF_USB = 1;
        public const uint ZG_DF_IP = 2;
        public const uint ZG_DF_ALL = 0xFFFFFFFF;
        #endregion

        #region Константы (флаги для ключа)
        public const uint ZG_KF_SHORT_NUM = 1;     // Короткий номер. Если fProximity=False, то контроллер будет проверять только первые 3 байта номера ключа.
        public const uint ZG_KF_ANTIPASSBACK = 2;  // Антипассбэк задействован
        #endregion

        #region Константы (адреса внешних устройств для функции ZG_Ctr_ControlDevices)
        public const uint ZG_DEV_RELE1 = 0;     // реле номер 1
        public const uint ZG_DEV_RELE2 = 1;     // реле номер 2
        public const uint ZG_DEV_SW3 = 2;       // силовой ключ SW3 (ОС) Конт.5 колодки К5
        public const uint ZG_DEV_SW4 = 3;       // силовой ключ SW4 (ОС) Конт.5 колодки К6
        public const uint ZG_DEV_SW0 = 4;       // силовой ключ SW0 (ОС) Конт.1 колодки К4
        public const uint ZG_DEV_SW1 = 5;       // силовой ключ SW1 (ОС) Конт.3 колодки К4
        public const uint ZG_DEV_K65 = 6;       // слаботочный ключ (ОК) Конт.6 колодки К5
        public const uint ZG_DEV_K66 = 7;       // слаботочный ключ (ОК) Конт.6 колодки К6
        #endregion

        #region Константы (флаги контроллера)
        public const uint ZG_CTR_F_2BANKS = 0x01;           // 2 банка / 1 банк
        public const uint ZG_CTR_F_PROXIMITY = 0x02;        // Proximity (Wiegand) / TouchMemory (Dallas)
        public const uint ZG_CTR_F_JOIN = 0x04;             // Объединение двух банков
        public const uint ZG_CTR_F_X2 = 0x08;               // Удвоение ключей
        public const uint ZG_CTR_F_ELECTRO = 0x10;          // Функция ElectroControl (для Matrix II Net)
        #endregion

        #region Константы (флаги конфигурации электропитания)
        public const uint ZG_EC_CF_ENABLED = 0x01;          // задействовать управление питанием
        public const uint ZG_EC_CF_SCHEDULE = 0x02;         // использовать временную зону 6 для включения питания
        public const uint ZG_EC_CF_EXT_READER = 0x04;       // контрольный считыватель: «0» Matrix-II Net, «1» внешний считыватель
        public const uint ZG_EC_CF_INVERT = 0x08;           // инвертировать управляющий выход
        public const uint ZG_EC_CF_EXIT_OFF = 0x10;         // задействовать датчик двери
        public const uint ZG_EC_CF_CARD_OPEN = 0x20;        // не блокировать функцию открывания для контрольного считывателя
        #endregion

        #region Константы (флаги состояния электропитания)
        public const uint ZG_EC_SF_ENABLED = 0x01;          // состояние питания – 1 вкл/0 выкл
        public const uint ZG_EC_SF_SCHEDULE = 0x02;         // активно включение по временной зоне
        public const uint ZG_EC_SF_REMOTE = 0x04;           // включено по команде по сети
        public const uint ZG_EC_SF_DELAY = 0x08;            // идет отработка задержки
        public const uint ZG_EC_SF_CARD = 0x10;             // карта в поле контрольного считывателя
        #endregion

        #region Константы (флаги состояния режима Пожар)
        public const uint ZG_FR_F_ENABLED = 0x01;           // Состояние пожарного режима – 1 вкл/0 выкл
        public const uint ZG_FR_F_INPUT_F = 0x02;           // Активен пожарный режим по входу FIRE
        public const uint ZG_FR_F_TEMP = 0x04;              // Активен пожарный режим по превышению температуры
        public const uint ZG_FR_F_NET = 0x08;               // Активен пожарный режим по внешней команде
        #endregion

        #region Константы (флаги для маски разрешения источников режима Пожар)
        public const uint ZG_FR_SRCF_INPUT_F = 0x01;        // Разрешен пожарный режим по входу FIRE
        public const uint ZG_FR_SRCF_TEMP = 0x02;           // Разрешен пожарный режим по превышению температуры
        #endregion

        #region Константы (флаги состояния режима Охрана)
        public const uint ZG_SR_F_ENABLED = 0x01;           // Состояние охранного режима – 1 вкл/0 выкл
        public const uint ZG_SR_F_ALARM = 0x02;             // Состояние тревоги
        public const uint ZG_SR_F_INPUT_A = 0x04;           // Тревога по входу ALARM
        public const uint ZG_SR_F_TAMPERE = 0x08;           // Тревога по тамперу
        public const uint ZG_SR_F_DOOR = 0x10;              // Тревога по датчику двери
        public const uint ZG_SR_F_NET = 0x20;               // Тревога включена по сети
        #endregion

        #region Константы (флаги для маски разрешения источников режима Охрана)
        public const uint ZG_SR_SRCF_INPUT_F = 0x01;        // Разрешена тревога по входу FIRE
        public const uint ZG_SR_SRCF_TAMPERE = 0x02;        // Разрешена тревога по тамперу
        public const uint ZG_SR_SRCF_DOOR = 0x04;           // Разрешена тревога по датчику двери
        #endregion


        //Функции библиотеки
        [DllImport(@"ZGuard.dll", CallingConvention = CallingConvention.StdCall, EntryPoint = "ZG_GetVersion")]
        public static extern UInt32 ZG_GetVersion();

        [DllImport(@"ZGuard.dll", CallingConvention = CallingConvention.StdCall, EntryPoint = "ZG_Initialize")]
        public static extern int ZG_Initialize(UInt32 nFlags);

        [DllImport(@"ZGuard.dll", CallingConvention = CallingConvention.StdCall, EntryPoint = "ZG_Finalyze")]
        public static extern int ZG_Finalyze();

        [DllImport(@"ZGuard.dll", CallingConvention = CallingConvention.StdCall, EntryPoint = "ZG_EnumConverters")]
        public static extern int ZG_EnumConverters(ZG_ENUMCVTSPROC pEnumProc, IntPtr pUserData = default(IntPtr), 
            UInt32 nDevTypes = ZG_DF_ALL);

        [DllImport(@"ZGuard.dll", CallingConvention = CallingConvention.StdCall, EntryPoint = "ZG_EnumConvertersEx")]
        public static extern int ZG_EnumConvertersEx(ZG_ENUMCVTSPROC pEnumProc, IntPtr pUserData = default(IntPtr),
            UInt32 nDevTypes = ZG_DF_ALL, IntPtr pWait = default(IntPtr));

        [DllImport(@"ZGuard.dll", CallingConvention = CallingConvention.StdCall, EntryPoint = "ZG_FindConverter")]
        public static extern int ZG_FindConverter(string szPort, ZPort.ZP_PORT_TYPE nType, ref ZG_ENUM_CVT_INFO pInfo,
            UInt32 nDevTypes = ZG_DF_ALL, IntPtr pWait = default(IntPtr));

        [DllImport(@"ZGuard.dll", CallingConvention = CallingConvention.StdCall, EntryPoint = "ZG_FindNotification")]
        public static extern int ZG_FindNotification(ref IntPtr pHandle, ref ZG_NOTIFY_SETTINGS pSettings);

        [DllImport(@"ZGuard.dll", CallingConvention = CallingConvention.StdCall, EntryPoint = "ZG_CloseNotification")]
        public static extern int ZG_CloseNotification(IntPtr hHandle);

        [DllImport(@"Z2USB.dll", CallingConvention = CallingConvention.StdCall, EntryPoint = "ZG_ProcessMessages")]
        public static extern int ZG_ProcessMessages(IntPtr hHandle, ZPort.ZP_NOTIFYPROC pEnumProc, IntPtr pUserData = default(IntPtr));

        [DllImport(@"ZGuard.dll", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall, EntryPoint = "ZG_UpdateCvtFirmware")]
        public static extern int ZG_UpdateCvtFirmware(ref ZG_CVT_OPEN_PARAMS pParams, [In] byte[] pData, int nCount,
            ZG_PROCESSCALLBACK pfnCallback, IntPtr pUserData = default(IntPtr));

        [DllImport(@"ZGuard.dll", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall, EntryPoint = "ZG_Cvt_Open")]
        /// <summary>
        /// Открывает конвертер.
        /// </summary>
        /// <param name="pHandle">Возвращаемый дескриптор конвертера.</param>
        /// <param name="pParams">Параметры открытия конвертера.</param>
        /// <param name="pInfo">Информация о конвертере.</param>
        /// <returns></returns>
        public static extern int ZG_Cvt_Open(ref IntPtr pHandle, ref ZG_CVT_OPEN_PARAMS pParams, 
            [In,Out] [MarshalAs(UnmanagedType.LPStruct)] ZG_CVT_INFO pInfo=null);

        [DllImport(@"ZGuard.dll", CallingConvention = CallingConvention.StdCall, EntryPoint = "ZG_Cvt_Close")]
        public static extern int ZG_Cvt_Close(IntPtr hHandle);

        [DllImport(@"ZGuard.dll", CallingConvention = CallingConvention.StdCall, EntryPoint = "ZG_Cvt_DettachPort")]
        public static extern int ZG_Cvt_DettachPort(IntPtr hHandle, ref IntPtr pPortHandle);

        [DllImport(@"ZGuard.dll", CallingConvention = CallingConvention.StdCall, EntryPoint = "ZG_Cvt_GetWaitSettings")]
        public static extern int ZG_Cvt_GetWaitSettings(IntPtr hHandle, ref ZG_WAIT_SETTINGS pSetting);

        [DllImport(@"ZGuard.dll", CallingConvention = CallingConvention.StdCall, EntryPoint = "ZG_Cvt_SetWaitSettings")]
        public static extern int ZG_Cvt_SetWaitSettings(IntPtr hHandle, ref ZG_WAIT_SETTINGS pSetting);

        [DllImport(@"ZGuard.dll", CallingConvention = CallingConvention.StdCall, EntryPoint = "ZG_Cvt_SetCapture")]
        public static extern int ZG_Cvt_SetCapture(IntPtr hHandle);

        [DllImport(@"ZGuard.dll", CallingConvention = CallingConvention.StdCall, EntryPoint = "ZG_Cvt_ReleaseCapture")]
        public static extern int ZG_Cvt_ReleaseCapture(IntPtr hHandle);

        [DllImport(@"ZGuard.dll", CallingConvention = CallingConvention.StdCall, EntryPoint = "ZG_Cvt_Clear")]
        public static extern int ZG_Cvt_Clear(IntPtr hHandle);

        [DllImport(@"ZGuard.dll", CallingConvention = CallingConvention.StdCall, EntryPoint = "ZG_Cvt_Send")]
        public static extern int ZG_Cvt_Send(IntPtr hHandle, [In] byte[] pData, int nCount);

        [DllImport(@"ZGuard.dll", CallingConvention = CallingConvention.StdCall, EntryPoint = "ZG_Cvt_Receive")]
        public static extern int ZG_Cvt_Receive(IntPtr hHandle, [In, Out] byte[] pBuf, int nBufSize, ref int pCount);

        [DllImport(@"ZGuard.dll", CallingConvention = CallingConvention.StdCall, EntryPoint = "ZG_Cvt_Exec")]
        public static extern int ZG_Cvt_Exec(IntPtr hHandle, [In] byte[] pData, int nCount, [In, Out] byte[] pBuf, int nBufSize, ref int pRCount);

        [DllImport(@"ZGuard.dll", CallingConvention = CallingConvention.StdCall, EntryPoint = "ZG_Cvt_EnumControllers")]
        public static extern int ZG_Cvt_EnumControllers(IntPtr hHandle, ZG_ENUMCTRSPROC pEnumProc, IntPtr pUserData = default(IntPtr));

        [DllImport(@"ZGuard.dll", CallingConvention = CallingConvention.StdCall, EntryPoint = "ZG_Cvt_FindController")]
        public static extern int ZG_Cvt_FindController(IntPtr hHandle, Byte nAddr, ref ZG_FIND_CTR_INFO pInfo);

        [DllImport(@"ZGuard.dll", CallingConvention = CallingConvention.StdCall, EntryPoint = "ZG_Cvt_GetInformation")]
        public static extern int ZG_Cvt_GetInformation(IntPtr hHandle, ref ZG_CVT_INFO pInfo);

        [DllImport(@"ZGuard.dll", CallingConvention = CallingConvention.StdCall, EntryPoint = "ZG_Cvt_FindNotification")]
        public static extern int ZG_Cvt_FindNotification(IntPtr hHandle, [In, Out] [MarshalAs(UnmanagedType.LPStruct)] ZG_CVT_NOTIFY_SETTINGS pSettings);

        [DllImport(@"Z2USB.dll", CallingConvention = CallingConvention.StdCall, EntryPoint = "ZG_Cvt_ProcessMessages")]
        public static extern int ZG_Cvt_ProcessMessages(IntPtr hHandle, ZPort.ZP_NOTIFYPROC pEnumProc, IntPtr pUserData = default(IntPtr));

        [DllImport(@"Z2USB.dll", CallingConvention = CallingConvention.StdCall, EntryPoint = "ZG_Cvt_GetScanCtrsState")]
        public static extern int ZG_Cvt_GetScanCtrsState(IntPtr hHandle, ref int nNextAddr);

        [DllImport(@"ZGuard.dll", CallingConvention = CallingConvention.StdCall, EntryPoint = "ZG_Cvt_UpdateFirmware")]
        public static extern int ZG_Cvt_UpdateFirmware(IntPtr hHandle, [In] byte[] pData, int nCount,
            ZG_PROCESSCALLBACK pfnCallback, IntPtr pUserData = default(IntPtr));

        [DllImport(@"ZGuard.dll", CallingConvention = CallingConvention.StdCall, EntryPoint = "ZG_Cvt_GetLicense")]
        public static extern int ZG_Cvt_GetLicense(IntPtr hHandle, Byte nLicN, ref ZG_CVT_LIC_INFO pInfo);

        [DllImport(@"ZGuard.dll", CallingConvention = CallingConvention.StdCall, EntryPoint = "ZG_Cvt_SetLicenseData")]
        public static extern int ZG_Cvt_SetLicenseData(IntPtr hHandle, Byte nLicN, 
            [In] byte[] pData, int nCount, ref UInt16 pLicStatus);

        [DllImport(@"ZGuard.dll", CallingConvention = CallingConvention.StdCall, EntryPoint = "ZG_Cvt_ClearAllLicenses")]
        public static extern int ZG_Cvt_ClearAllLicenses(IntPtr hHandle);

        [DllImport(@"ZGuard.dll", CallingConvention = CallingConvention.StdCall, EntryPoint = "ZG_Cvt_GetShortInfo")]
        public static extern int ZG_Cvt_GetShortInfo(IntPtr hHandle, ref UInt16 pSn, ref ZG_GUARD_MODE pMode);

        [DllImport(@"ZGuard.dll", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall, EntryPoint = "ZG_Cvt_GetLongInfo")]
        public static extern int ZG_Cvt_GetLongInfo(IntPtr hHandle, ref UInt16 pSn, ref UInt16 pVersion, 
            ref ZG_GUARD_MODE pMode, ref string pBuf, int nBufSize, ref int pLen);

        [DllImport(@"ZGuard.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall, EntryPoint = "ZG_Cvt_UpdateCtrFirmware")]
        public static extern int ZG_Cvt_UpdateCtrFirmware(IntPtr hHandle, UInt16 nSn,
            [In] byte[] pData, int nCount, string pszInfoStr, ZG_PROCESSCALLBACK pfnCallback, IntPtr pUserData = default(IntPtr));

        [DllImport(@"ZGuard.dll", CallingConvention = CallingConvention.StdCall, EntryPoint = "ZG_Cvt_SetCtrAddrBySn")]
        public static extern int ZG_Cvt_SetCtrAddrBySn(IntPtr hHandle, UInt16 nSn, Byte nNewAddr);

        [DllImport(@"ZGuard.dll", CallingConvention = CallingConvention.StdCall, EntryPoint = "ZG_Cvt_SetCtrAddr")]
        public static extern int ZG_Cvt_SetCtrAddr(IntPtr hHandle, Byte nOldAddr, Byte nNewAddr);

        [DllImport(@"ZGuard.dll", CallingConvention = CallingConvention.StdCall, EntryPoint = "ZG_Cvt_GetCtrInfoNorm")]
        public static extern int ZG_Cvt_GetCtrInfoNorm(IntPtr hHandle, Byte nAddr, ref Byte pTypeCode, ref UInt16 pSn, ref UInt16 pVersion, ref int pInfoLines, ref UInt32 pFlags);

        [DllImport(@"ZGuard.dll", CallingConvention = CallingConvention.StdCall, EntryPoint = "ZG_Cvt_GetCtrInfoAdv")]
        public static extern int ZG_Cvt_GetCtrInfoAdv(IntPtr hHandle, Byte nAddr, ref Byte pTypeCode, ref UInt16 pSn, ref UInt16 pVersion, ref UInt32 pFlags, ref int pEvWrIdx, ref int pEvRdIdx);

        [DllImport(@"ZGuard.dll", CallingConvention = CallingConvention.StdCall, EntryPoint = "ZG_Cvt_GetCtrInfoBySn")]
        public static extern int ZG_Cvt_GetCtrInfoBySn(IntPtr hHandle, UInt16 nSn, ref Byte pTypeCode, ref Byte pAddr, ref UInt16 pVersion, ref int pInfoLines, ref UInt32 pFlags);

        [DllImport(@"ZGuard.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall, EntryPoint = "ZG_Cvt_GetCtrInfoLine")]
        public static extern int ZG_Cvt_GetCtrInfoLine(IntPtr hHandle, UInt16 nSn, int nLineN, ref string pBuf, int nBufSize, ref int pLen);

        [DllImport(@"ZGuard.dll", CallingConvention = CallingConvention.StdCall, EntryPoint = "ZG_Cvt_GetCtrVersion")]
        public static extern int ZG_Cvt_GetCtrVersion(IntPtr hHandle, Byte nAddr, ref byte[] pVerData5, UInt32 nTimeOut = 0);

        [DllImport(@"ZGuard.dll", CallingConvention = CallingConvention.StdCall, EntryPoint = "ZG_Ctr_Open")]
        public static extern int ZG_Ctr_Open(ref IntPtr hHandle, IntPtr hCvtHandle, Byte nAddr, UInt16 nSn,
            ref ZG_CTR_INFO pInfo);

        [DllImport(@"ZGuard.dll", CallingConvention = CallingConvention.StdCall, EntryPoint = "ZG_Ctr_Close")]
        public static extern int ZG_Ctr_Close(IntPtr hHandle);

        [DllImport(@"ZGuard.dll", CallingConvention = CallingConvention.StdCall, EntryPoint = "ZG_Ctr_GetInformation")]
        public static extern int ZG_Ctr_GetInformation(IntPtr hHandle, ref ZG_CTR_INFO pInfo);

        [DllImport(@"ZGuard.dll", CallingConvention = CallingConvention.StdCall, EntryPoint = "ZG_Ctr_FindNotification")]
        public static extern int ZG_Ctr_FindNotification(IntPtr hHandle, 
            [In, Out] [MarshalAs(UnmanagedType.LPStruct)] ZG_CTR_NOTIFY_SETTINGS pSettings);

        [DllImport(@"Z2USB.dll", CallingConvention = CallingConvention.StdCall, EntryPoint = "ZG_Ctr_ProcessMessages")]
        public static extern int ZG_Ctr_ProcessMessages(IntPtr hHandle, ZPort.ZP_NOTIFYPROC pEnumProc, IntPtr pUserData);

        [DllImport(@"ZGuard.dll", CallingConvention = CallingConvention.StdCall, EntryPoint = "ZG_Ctr_SetNewAddr")]
        public static extern int ZG_Ctr_SetNewAddr(IntPtr hHandle, Byte nNewAddr);

        [DllImport(@"ZGuard.dll", CallingConvention = CallingConvention.StdCall, EntryPoint = "ZG_Ctr_AssignAddr")]
        public static extern int ZG_Ctr_AssignAddr(IntPtr hHandle, Byte nAddr);

        [DllImport(@"ZGuard.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall, EntryPoint = "ZG_Ctr_UpdateFirmware")]
        public static extern int ZG_Ctr_UpdateFirmware(IntPtr hHandle, [In] byte[] pData, int nCount,
            string pszInfoStr, ZG_PROCESSCALLBACK pfnCallback, IntPtr pUserData = default(IntPtr));

        [DllImport(@"ZGuard.dll", CallingConvention = CallingConvention.StdCall, EntryPoint = "ZG_Ctr_OpenLock")]
        public static extern int ZG_Ctr_OpenLock(IntPtr hHandle, int nLockN=0);

        [DllImport(@"ZGuard.dll", CallingConvention = CallingConvention.StdCall, EntryPoint = "ZG_Ctr_DisconnectLocks")]
        public static extern int ZG_Ctr_DisconnectLocks(IntPtr hHandle);

        [DllImport(@"ZGuard.dll", CallingConvention = CallingConvention.StdCall, EntryPoint = "ZG_Ctr_EnableEmergencyUnlocking")]
        public static extern int ZG_Ctr_EnableEmergencyUnlocking(IntPtr hHandle, bool fEnable=true);

        [DllImport(@"ZGuard.dll", CallingConvention = CallingConvention.StdCall, EntryPoint = "ZG_Ctr_IsEmergencyUnlockingEnabled")]
        public static extern int ZG_Ctr_IsEmergencyUnlockingEnabled(IntPtr hHandle, ref bool pEnabled);

        [DllImport(@"ZGuard.dll", CallingConvention = CallingConvention.StdCall, EntryPoint = "ZG_Ctr_Reset")]
        public static extern int ZG_Ctr_Reset(IntPtr hHandle);

        [DllImport(@"ZGuard.dll", CallingConvention = CallingConvention.StdCall, EntryPoint = "ZG_Ctr_UpdateFlash")]
        public static extern int ZG_Ctr_UpdateFlash(IntPtr hHandle);

        [DllImport(@"ZGuard.dll", CallingConvention = CallingConvention.StdCall, EntryPoint = "ZG_Ctr_HardReset")]
        public static extern int ZG_Ctr_HardReset(IntPtr hHandle);

        [DllImport(@"ZGuard.dll", CallingConvention = CallingConvention.StdCall, EntryPoint = "ZG_Ctr_ReadRegs")]
        public static extern int ZG_Ctr_ReadRegs(IntPtr hHandle, UInt32 nAddr, int nCount, [In, Out] Byte[] pBuf);

        [DllImport(@"ZGuard.dll", CallingConvention = CallingConvention.StdCall, EntryPoint = "ZG_Ctr_ReadPorts")]
        public static extern int ZG_Ctr_ReadPorts(IntPtr hHandle, ref UInt32 pData);

        [DllImport(@"ZGuard.dll", CallingConvention = CallingConvention.StdCall, EntryPoint = "ZG_Ctr_ControlDevices")]
        public static extern int ZG_Ctr_ControlDevices(IntPtr hHandle, UInt32 nDevType, bool fActive, UInt32 nTimeMs=0);

        [DllImport(@"ZGuard.dll", CallingConvention = CallingConvention.StdCall, EntryPoint = "ZG_Ctr_ReadData")]
        public static extern int ZG_Ctr_ReadData(IntPtr hHandle, int nBankN, UInt32 nAddr, int nCount,
            [In, Out] Byte[] pBuf, ref int pReaded, 
            ZG_PROCESSCALLBACK pfnCallback, IntPtr pUserData = default(IntPtr));

        [DllImport(@"ZGuard.dll", CallingConvention = CallingConvention.StdCall, EntryPoint = "ZG_Ctr_WriteData")]
        public static extern int ZG_Ctr_WriteData(IntPtr hHandle, int nBankN, UInt32 nAddr,
            [In, Out] Byte[] pData, int nCount, ref int pWritten, 
            ZG_PROCESSCALLBACK pfnCallback, IntPtr pUserData = default(IntPtr));

        [DllImport(@"ZGuard.dll", CallingConvention = CallingConvention.StdCall, EntryPoint = "ZG_Ctr_ReadLockTimes")]
        public static extern int ZG_Ctr_ReadLockTimes(IntPtr hHandle, 
            ref UInt32 pOpenMs, ref UInt32 pLetMs, ref UInt32 pMaxMs, int nBankN = 0);

        [DllImport(@"ZGuard.dll", CallingConvention = CallingConvention.StdCall, EntryPoint = "ZG_Ctr_WriteLockTimes")]
        public static extern int ZG_Ctr_WriteLockTimes(IntPtr hHandle, UInt32 nMask,
            UInt32 nOpenMs, UInt32 nLetMs, UInt32 nMaxMs, int nBankN = 0);

        [DllImport(@"ZGuard.dll", CallingConvention = CallingConvention.StdCall, EntryPoint = "ZG_Ctr_ReadTimeZones")]
        public static extern int ZG_Ctr_ReadTimeZones(IntPtr hHandle, int nIdx,
            [In, Out] ZG_CTR_TIMEZONE[] pBuf, int nCount, 
            ZG_PROCESSCALLBACK pfnCallback, IntPtr pUserData = default(IntPtr), int nBankN = 0);

        [DllImport(@"ZGuard.dll", CallingConvention = CallingConvention.StdCall, EntryPoint = "ZG_Ctr_WriteTimeZones")]
        public static extern int ZG_Ctr_WriteTimeZones(IntPtr hHandle, int nIdx,
            [In, Out] ZG_CTR_TIMEZONE[] pTzs, int nCount,
            ZG_PROCESSCALLBACK pfnCallback, IntPtr pUserData = default(IntPtr), int nBankN = 0);

        [DllImport(@"ZGuard.dll", CallingConvention = CallingConvention.StdCall, EntryPoint = "ZG_Ctr_WriteTimeZones")]
        public static extern int ZG_Ctr_WriteTimeZones(IntPtr hHandle, int nIdx,
            ref ZG_CTR_TIMEZONE pTzs, int nCount,
            ZG_PROCESSCALLBACK pfnCallback, IntPtr pUserData = default(IntPtr), int nBankN = 0);

        [DllImport(@"ZGuard.dll", CallingConvention = CallingConvention.StdCall, EntryPoint = "ZG_Ctr_EnumTimeZones")]
        public static extern int ZG_Ctr_EnumTimeZones(IntPtr hHandle, int nStart,
            ZG_ENUMCTRTIMEZONESPROC fnEnumProc, IntPtr pUserData = default(IntPtr), int nBankN = 0);

        [DllImport(@"ZGuard.dll", CallingConvention = CallingConvention.StdCall, EntryPoint = "ZG_Ctr_ReadKeys")]
        public static extern int ZG_Ctr_ReadKeys(IntPtr hHandle, int nIdx,
            [In, Out] ZG_CTR_KEY[] pBuf, int nCount,
            ZG_PROCESSCALLBACK pfnCallback, IntPtr pUserData = default(IntPtr), int nBankN = 0);

        [DllImport(@"ZGuard.dll", CallingConvention = CallingConvention.StdCall, EntryPoint = "ZG_Ctr_WriteKeys")]
        public static extern int ZG_Ctr_WriteKeys(IntPtr hHandle, int nIdx,
            [In, Out] ZG_CTR_KEY[] pKeys, int nCount,
            ZG_PROCESSCALLBACK pfnCallback, IntPtr pUserData = default(IntPtr), int nBankN = 0);

        [DllImport(@"ZGuard.dll", CallingConvention = CallingConvention.StdCall, EntryPoint = "ZG_Ctr_ClearKeys")]
        public static extern int ZG_Ctr_ClearKeys(IntPtr hHandle, int nIdx, int nCount,
            ZG_PROCESSCALLBACK pfnCallback, IntPtr pUserData = default(IntPtr), int nBankN = 0);

        [DllImport(@"ZGuard.dll", CallingConvention = CallingConvention.StdCall, EntryPoint = "ZG_Ctr_GetKeyTopIndex")]
        public static extern int ZG_Ctr_GetKeyTopIndex(IntPtr hHandle, ref int pIdx, int nBankN = 0);

        [DllImport(@"ZGuard.dll", CallingConvention = CallingConvention.StdCall, EntryPoint = "ZG_Ctr_EnumKeys")]
        public static extern int ZG_Ctr_EnumKeys(IntPtr hHandle, int nStart,
            ZG_ENUMCTRKEYSPROC fnEnumProc, IntPtr pUserData = default(IntPtr), int nBankN = 0);

        [DllImport(@"ZGuard.dll", CallingConvention = CallingConvention.StdCall, EntryPoint = "ZG_Ctr_GetClock")]
        public static extern int ZG_Ctr_GetClock(IntPtr hHandle, ref ZG_CTR_CLOCK pClock);

        [DllImport(@"ZGuard.dll", CallingConvention = CallingConvention.StdCall, EntryPoint = "ZG_Ctr_SetClock")]
        public static extern int ZG_Ctr_SetClock(IntPtr hHandle, ref ZG_CTR_CLOCK pClock);

        [DllImport(@"ZGuard.dll", CallingConvention = CallingConvention.StdCall, EntryPoint = "ZG_Ctr_ReadLastKeyNum")]
        public static extern int ZG_Ctr_ReadLastKeyNum(IntPtr hHandle, [In, Out] Byte[] pNum);

        [DllImport(@"ZGuard.dll", CallingConvention = CallingConvention.StdCall, EntryPoint = "ZG_Ctr_ReadRTCState")]
        public static extern int ZG_Ctr_ReadRTCState(IntPtr hHandle, ref ZG_CTR_CLOCK pClock,
            ref int pWrIdx, ref int pRdIdx, [In, Out] Byte[] pNum);

        [DllImport(@"ZGuard.dll", CallingConvention = CallingConvention.StdCall, EntryPoint = "ZG_Ctr_ReadEventIdxs")]
        public static extern int ZG_Ctr_ReadEventIdxs(IntPtr hHandle, ref int pWrIdx, ref int pRdIdx);

        [DllImport(@"ZGuard.dll", CallingConvention = CallingConvention.StdCall, EntryPoint = "ZG_Ctr_WriteEventIdxs")]
        public static extern int ZG_Ctr_WriteEventIdxs(IntPtr hHandle, UInt32 nMask, int nWrIdx, int nRdIdx);

        [DllImport(@"ZGuard.dll", CallingConvention = CallingConvention.StdCall, EntryPoint = "ZG_Ctr_ReadEvents")]
        public static extern int ZG_Ctr_ReadEvents(IntPtr hHandle, int nIdx,
            [In, Out] ZG_CTR_EVENT[] pBuf, int nCount,
            ZG_PROCESSCALLBACK pfnCallback, IntPtr pUserData = default(IntPtr));

        [DllImport(@"ZGuard.dll", CallingConvention = CallingConvention.StdCall, EntryPoint = "ZG_Ctr_EnumEvents")]
        public static extern int ZG_Ctr_EnumEvents(IntPtr hHandle, int nStart, int nCount,
            ZG_ENUMCTREVENTSPROC fnEnumProc, IntPtr pUserData = default(IntPtr));
        [DllImport(@"ZGuard.dll", CallingConvention = CallingConvention.StdCall, EntryPoint = "ZG_Ctr_DecodePassEvent")]
        /// <summary>
        /// Декодирование событий прохода
        /// </summary>
        /// <param name="hHandle">Дескриптор контроллера</param>
        /// <param name=""></param>
        /// <param name="pData8">Данные события (8 байт)</param>
        /// <param name="pTime">Время события</param>
        /// <param name="pDirect">Направление прохода</param>
        /// <param name="pKeyIdx">Индекс ключа в банке ключей</param>
        /// <param name="pKeyBank">Номер банка ключей</param>
        /// <returns></returns>
        public static extern int ZG_Ctr_DecodePassEvent(IntPtr hHandle, [In, Out] Byte[] pData8,
            ref ZG_EV_TIME pTime, ref ZG_CTR_DIRECT pDirect, ref int pKeyIdx, ref int pKeyBank);
        [DllImport(@"ZGuard.dll", CallingConvention = CallingConvention.StdCall, EntryPoint = "ZG_Ctr_DecodeEcEvent")]
        /// <summary>
        /// Декодирование событий ElectoControl: ZG_EV_ELECTRO_ON, ZG_EV_ELECTRO_OFF
        /// </summary>
        /// <param name="hHandle">Дескриптор контроллера</param>
        /// <param name=""></param>
        /// <param name="pData8">Данные события (8 байт)</param>
        /// <param name="pTime">Время события</param>
        /// <param name="pSubEvent">Условие, вызвавшее событие</param>
        /// <param name="pPowerFlags">Флаги электропитания</param>
        /// <returns></returns>
        public static extern int ZG_Ctr_DecodeEcEvent(IntPtr hHandle, [In, Out] Byte[] pData8,
            ref ZG_EV_TIME pTime, ref ZG_EC_SUB_EV pSubEvent, ref UInt32 pPowerFlags);
        [DllImport(@"ZGuard.dll", CallingConvention = CallingConvention.StdCall, EntryPoint = "ZG_Ctr_DecodeUnkKeyEvent")]
        /// <summary>
        /// Декодирование события со значением ключа: ZG_EV_KEY_VALUE
        /// </summary>
        /// <param name="hHandle">Дескриптор контроллера</param>
        /// <param name=""></param>
        /// <param name="pData8">Данные события (8 байт)</param>
        /// <param name="pKeyNum">Номер ключа</param>
        /// <returns></returns>
        public static extern int ZG_Ctr_DecodeUnkKeyEvent(IntPtr hHandle, [In, Out] Byte[] pData8,
            [In, Out] Byte[] pKeyNum);
        [DllImport(@"ZGuard.dll", CallingConvention = CallingConvention.StdCall, EntryPoint = "ZG_Ctr_DecodeFireEvent")]
        /// <summary>
        /// Декодирование события ZG_EV_FIRE_STATE (Изменение состояния Пожара)
        /// </summary>
        /// <param name="hHandle">Дескриптор контроллера</param>
        /// <param name=""></param>
        /// <param name="pData8">Данные события (8 байт)</param>
        /// <param name="pTime">Время события</param>
        /// <param name="pSubEvent">Условие, вызвавшее событие</param>
        /// <param name="pFireFlags">Флаги состояния режима Пожар (ZG_FR_F_...)</param>
        /// <returns></returns>
        public static extern int ZG_Ctr_DecodeFireEvent(IntPtr hHandle, [In, Out] Byte[] pData8,
            ref ZG_EV_TIME pTime, ref ZG_FIRE_SUB_EV pSubEvent, ref UInt32 pFireFlags);
        [DllImport(@"ZGuard.dll", CallingConvention = CallingConvention.StdCall, EntryPoint = "ZG_Ctr_DecodeSecurEvent")]
        /// <summary>
        /// Декодирование события ZG_EV_SECUR_STATE (Изменение состояния Охрана)
        /// </summary>
        /// <param name="hHandle">Дескриптор контроллера</param>
        /// <param name=""></param>
        /// <param name="pData8">Данные события (8 байт)</param>
        /// <param name="pTime">Время события</param>
        /// <param name="pSubEvent">Условие, вызвавшее событие</param>
        /// <param name="pSecurFlags">Флаги состояния режима Охрана (ZG_SR_F_...)</param>
        /// <returns></returns>
        public static extern int ZG_Ctr_DecodeSecurEvent(IntPtr hHandle, [In, Out] Byte[] pData8,
            ref ZG_EV_TIME pTime, ref ZG_SECUR_SUB_EV pSubEvent, ref UInt32 pSecurFlags);

        [DllImport(@"ZGuard.dll", CallingConvention = CallingConvention.StdCall, EntryPoint = "ZG_Ctr_SetFireMode")]
        public static extern int ZG_Ctr_SetFireMode(IntPtr hHandle, bool fOn=true);

        [DllImport(@"ZGuard.dll", CallingConvention = CallingConvention.StdCall, EntryPoint = "ZG_Ctr_GetFireInfo")]
        /// <summary>
        /// Запрос состояния пожарного режима
        /// </summary>
        /// <param name="hHandle">Дескриптор контроллера</param>
        /// <param name="pFireFlags">Флаги состояния ZG_FR_F_...</param>
        /// <param name="pCurrTemp">Текущая температура</param>
        /// <param name="pSrcMask">Маска разрешенных источников ZG_FR_SRCF_...</param>
        /// <param name="pLimitTemp">Пороговая температура</param>
        /// <returns></returns>
        public static extern int ZG_Ctr_GetFireInfo(IntPtr hHandle, ref UInt32 pFireFlags,
            ref UInt32 pCurrTemp, ref UInt32 pSrcMask, ref UInt32 pLimitTemp);

        [DllImport(@"ZGuard.dll", CallingConvention = CallingConvention.StdCall, EntryPoint = "ZG_Ctr_SetFireConfig")]
        /// <summary>
        /// Установка параметров пожарного режима
        /// </summary>
        /// <param name="hHandle">Дескриптор контроллера</param>
        /// <param name="nSrcMask">Маска разрешенных источников ZG_FR_SRCF_...</param>
        /// <param name="nLimitTemp">Пороговая температура (в градусах)</param>
        /// <param name="pFireFlags">Флаги состояния ZG_FR_F_...</param>
        /// <param name="pCurrTemp">Текущая температура (в градусах)</param>
        /// <returns></returns>
        public static extern int ZG_Ctr_SetFireConfig(IntPtr hHandle, UInt32 nSrcMask,
            UInt32 nLimitTemp, [Out] UInt32 nFireFlags, [Out] UInt32 nCurrTemp);

        [DllImport(@"ZGuard.dll", CallingConvention = CallingConvention.StdCall, EntryPoint = "ZG_Ctr_SetSecurMode")]
        public static extern int ZG_Ctr_SetSecurMode(IntPtr hHandle, ZG_SECUR_MODE nMode);

        [DllImport(@"ZGuard.dll", CallingConvention = CallingConvention.StdCall, EntryPoint = "ZG_Ctr_GetSecurInfo")]
        /// <summary>
        /// Запрос состояния режима Охрана
        /// </summary>
        /// <param name="hHandle">Дескриптор контроллера</param>
        /// <param name="nSecurFlags">Флаги состояния ZG_SR_F_</param>
        /// <param name="nSrcMask">Маска разрешенных источников ZG_SR_SRCF_...</param>
        /// <param name="nAlarmTime">Время звучания сирены (в секундах)</param>
        /// <returns></returns>
        public static extern int ZG_Ctr_GetSecurInfo(IntPtr hHandle, ref UInt32 nSecurFlags,
            ref UInt32 nSrcMask, ref UInt32 nAlarmTime);

        [DllImport(@"ZGuard.dll", CallingConvention = CallingConvention.StdCall, EntryPoint = "ZG_Ctr_SetSecurConfig")]
        /// <summary>
        /// Установка параметров режима Охрана
        /// </summary>
        /// <param name="hHandle">Дескриптор контроллера</param>
        /// <param name="nSrcMask">Маска разрешенных источников ZG_SR_SRCF_...</param>
        /// <param name="nAlarmTime">Время звучания сирены (в секундах)</param>
        /// <param name="nSecurFlags">Флаги состояния ZG_SR_F_</param>
        /// <returns></returns>
        public static extern int ZG_Ctr_SetSecurConfig(IntPtr hHandle, UInt32 nSrcMask,
            UInt32 nAlarmTime, [Out] UInt32 nSecurFlags);

        [DllImport(@"ZGuard.dll", CallingConvention = CallingConvention.StdCall, EntryPoint = "ZG_Ctr_ReadElectroConfig")]
        public static extern int ZG_Ctr_ReadElectroConfig(IntPtr hHandle, ref ZG_CTR_ELECTRO_CONFIG pConfig);

        [DllImport(@"ZGuard.dll", CallingConvention = CallingConvention.StdCall, EntryPoint = "ZG_Ctr_WriteElectroConfig")]
        public static extern int ZG_Ctr_WriteElectroConfig(IntPtr hHandle, ref ZG_CTR_ELECTRO_CONFIG pConfig,
            bool fSetTz=true);

        [DllImport(@"ZGuard.dll", CallingConvention = CallingConvention.StdCall, EntryPoint = "ZG_Ctr_GetElectroState")]
        public static extern int ZG_Ctr_GetElectroState(IntPtr hHandle, ref ZG_CTR_ELECTRO_STATE pState);

        [DllImport(@"ZGuard.dll", CallingConvention = CallingConvention.StdCall, EntryPoint = "ZG_Ctr_SetElectroPower")]
        public static extern int ZG_Ctr_SetElectroPower(IntPtr hHandle, bool fOn=true);
    }
}
