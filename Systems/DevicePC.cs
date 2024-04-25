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
// WMI
using System.Management;
// Network
using System.Net;
using System.Net.NetworkInformation;

namespace ConectoWorkSpace
{
    public static class DevicePC
    {

        //  Управление устройствами
        //  FormatByteCount - размер USB накопителя

        /// <summary>
        /// Активация слежки за USB устройством
        /// </summary>
        public static void LoadDeviceUSB()
        {
            AddUSBHandler();
            EjectUSBHandler();
            ChekNetwork();
            ChekDeviceUSB();

        }

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

        /// <summary>
        /// Проверка подключенния устройства при перемещении на него и чтении с него данных
        /// NameDevice - Имя устройства
        /// </summary>
        public static string[] ChekDevice(string NameDevice)
        {
            // Добовления префикса для поиска устройства
            NameDevice = NameDevice + @"\";
            // Возвращаем 0 устройство готово 1 свободно емкости 2 резерв
            string[] returnCh = new string[3] { "false", "", "" };

            // 1. Приложение ищит все диски и файлы ключа, ...)
            DriveInfo[] DriveList = DriveInfo.GetDrives();
            foreach (DriveInfo d in DriveList)
            {
                //SystemConecto.ErorDebag(d.Name, 1);
                // c:
                if (d.Name == NameDevice)
                {
                    if (d.IsReady == true)
                    {
                        // Устройство готово и его свободная емкость указана
                        returnCh[0] = "true";
                        returnCh[1] = d.TotalFreeSpace.ToString();
                    }

                    return returnCh;
                }
            }
            return returnCh;
        }


        #region Включения USB устройства в ОС
        /// <summary>
        /// Включения USB устройства в ОС
        /// </summary>
        private static void AddUSBHandler()
	    {
            WqlEventQuery q; // Представляет запрос WMI событий в формате WQL (Windows Query Language)
	 
	        ManagementScope scope = new ManagementScope("root\\CIMV2");

            ManagementEventWatcher w = null;
            // Представляет область (пространство имен) для управления операциями.
 
	        scope.Options.EnablePrivileges = true;
	        try
	        {
	            q = new WqlEventQuery();

                q.EventClassName = "__InstanceCreationEvent";
	            q.WithinInterval = new TimeSpan(0, 0, 3);
	            q.Condition = @"TargetInstance isa 'Win32_LogicalDisk' and (TargetInstance.DriveType=2)";
	            //q.Condition = @"TargetInstance ISA 'Win32_USBControllerdevice'";
	            w = new ManagementEventWatcher(scope, q);

                //добавляет обработчик события, что это вызывается при вызове события
                // w.EventArrived += new EventArrivedEventHandler(USBInseted); // Можно подключить еще метод обработки события
	            w.EventArrived += new EventArrivedEventHandler(OnWMIEvent);
	            w.Start();//run the watcher
	        }
	        catch (Exception e)
	        {
                SystemConecto.ErorDebag("Ошибка в устройстве: " + e.Message, 1);
	            if (w != null)
	                w.Stop();
	        }
          }
        #endregion

        #region Выключения USB устройства в ОС
        /// <summary>
        /// Выключения USB устройства в ОС
        /// </summary>
        private static void EjectUSBHandler()
	    {
            WqlEventQuery q;// Представляет запрос WMI событий в формате WQL (Windows Query Language)
	 
	        ManagementScope scope = new ManagementScope("root\\CIMV2");

            ManagementEventWatcher w1 = null;
            // Представляет область (пространство имен) для управления операциями.
	 
	        scope.Options.EnablePrivileges = true;
	        try
	        {
	            q = new WqlEventQuery();
	            q.EventClassName = "__InstanceDeletionEvent";
	            q.WithinInterval = new TimeSpan(0, 0, 3);
	            q.Condition = @"TargetInstance isa 'Win32_LogicalDisk' and (TargetInstance.DriveType=2)";
	            //  q.Condition = @"TargetInstance ISA 'Win32_USBControllerdevice'";
	            w1 = new ManagementEventWatcher(scope, q);

                //добавляет обработчик события, что это вызывается при вызове события
	            // w1.EventArrived += new EventArrivedEventHandler(USBDelection); // Можно подключить еще метод обработки события
	            w1.EventArrived += new EventArrivedEventHandler(OnWMIEventDelection);
	            w1.Start();//run the watcher
	        }
	        catch (Exception e)
	        {
                SystemConecto.ErorDebag("Ошибка в устройстве: " + e.Message, 1);
	            if (w1 != null)
	                w1.Stop();
	        }
	    }
        #endregion


        #region Изменение свойств сетевых адаптеров
        // Отслеживание измений свойств адаптеров
        private static void ChekNetwork()
	    {
            // Происходит при изменении IP-адреса сетевого интерфейса.
             NetworkChange.NetworkAddressChanged += new 
                NetworkAddressChangedEventHandler(AddressChangedCallback);
             // Происходит при изменении доступности сети.
             NetworkChange.NetworkAvailabilityChanged+= new
                 NetworkAvailabilityChangedEventHandler(AvailabilityChangedCallback);
         }

        /// <summary>
        /// Происходит при изменении IP-адреса сетевого интерфейса.
        /// </summary>
        public static void AddressChangedCallback(object sender, EventArgs e)
        {

            NetworkInterface[] adapters = NetworkInterface.GetAllNetworkInterfaces();
            foreach (NetworkInterface n in adapters)
            {
                //Console.WriteLine("   {0} is {1}", n.Name, n.OperationalStatus);
            }
 
        }

        /// <summary>
        /// Происходит при изменении доступности сети.
        /// </summary>
        public static void AvailabilityChangedCallback(object sender, EventArgs e)
        {

            NetworkInterface[] adapters = NetworkInterface.GetAllNetworkInterfaces();
            foreach (NetworkInterface n in adapters)
            {
                //Console.WriteLine("   {0} is {1}", n.Name, n.OperationalStatus);
            }

        }

        #endregion


        #region Обработка события подключения USB накопителя

        public static void OnWMIEvent(object sender, EventArrivedEventArgs e)
	    {
	        PropertyData p = e.NewEvent.Properties["TargetInstance"];
	        if (p != null)
	        {
            ManagementBaseObject mbo = p.Value as ManagementBaseObject;
	            //
 
	            PropertyData volumeName = mbo.Properties["VolumeName"];
	            // PropertyData model = mbo.Properties["Model"];
	            PropertyData freespace = mbo.Properties["FreeSpace"];//(ulong)volume["FreeSpace"];
	            PropertyData size = mbo.Properties["Size"];
	            PropertyData deviceid = mbo.Properties["DeviceID"]; // Name Имя диска
            PropertyData drivetype = mbo.Properties["DriveType"];
	            PropertyData driveSerial = mbo.Properties["VolumeSerialNumber"];
	            PropertyData driveGUID = mbo.Properties["PNPDeviceID"];

                // Поиск специальных значений заданных устройству
                // Код администратора на устройстве в виде зашифрованного файла



                // Вывод информационного окна

                System.Windows.Application.Current.Dispatcher.Invoke(new Action(delegate()
                {
                    var TextWindows = string.Format("Подключено устройство {1} \n Определенно как диск {0}{2} ", deviceid.Value, volumeName.Value,@"");
                    //MainWindow ConectoWorkSpace_InW = (MainWindow)Application.Current.MainWindow;
                    MainWindow ConectoWorkSpace_InW = AppStart.LinkMainWindow();
                    var WinOblakoVerh_ = AppStart.WindowActive("WinOblakoVerh_USBHDD", TextWindows, 1);
                    WinOblakoVerh_.Owner = ConectoWorkSpace_InW;

                    // Ссылка на объект (по ключ B52 потом компьютер)
                    var pic = ConectoWorkSpace_InW.DeviceOnOff;
                    var wP_SysI = ConectoWorkSpace_InW.wP_SysIndicat;
                    
                    pic.Visibility = System.Windows.Visibility.Visible;
                    ConectoWorkSpace_InW.numberElementsInfoPa(1);
                    double[] TopWidth = ConectoWorkSpace_InW.MessageCoordinatInfoPa(4);

                    // размещаем на рабочем столе
                    WinOblakoVerh_.Top = SystemConecto.WorkAreaDisplayDefault[0] + wP_SysI.Margin.Top - (WinOblakoVerh_.Height - 58);
                    WinOblakoVerh_.Left = SystemConecto.WorkAreaDisplayDefault[1] + (TopWidth[1]); // wP_SysI.Margin.Left - 28
                    WinOblakoVerh_.Show(); // не показываем модальным (модальное окно не закрывается методом Close, а также не освобождает ресурсы метод fWait.Dispose(); )

                }));

                // Пример вывода инфы

                //foreach (PropertyData property in mbo.Properties)
                //{
                //    SystemConecto.ErorDebag("Устройство: " +property.Name +"//"+ property.Value, 1);
                //}
                
                
                // SystemConecto.ErorDebag("Устройство: " + volumeName.Value + " // " + drivetype.Value, 1);
                // SystemConecto.ErorDebag("Устройство имеет свободную емкость: " + FormatByteCount((ulong)freespace.Value), 1);
                //FormatByteCount((ulong)freespace.Value)
	 
                // Вывод событий
//                try
//	            {
//	                if (this.InvokeRequired)
//	                    BeginInvoke(new MethodInvoker(delegate
//	                    {
//	                        listBox1.Items.Add(text);
//	                    }));
//	                else
//	                {
//	                    listBox1.Items.Add(text);
//	                }
//	            }
//	            catch (Exception ex)
//	            {
//	                MessageBox.Show(ex.Message);
//	            }
	           
	        }
	    }

        public static void OnWMIEventDelection(object sender, EventArrivedEventArgs e)
	    {
            PropertyData p = e.NewEvent.Properties["TargetInstance"];
	        if (p != null)
	        {
	            ManagementBaseObject mbo = p.Value as ManagementBaseObject;
	            //
	 
	            PropertyData volumeName = mbo.Properties["VolumeName"];
	            // PropertyData model = mbo.Properties["Model"];
	            PropertyData freespace = mbo.Properties["FreeSpace"];//(ulong)volume["FreeSpace"];
	            PropertyData size = mbo.Properties["Size"];
	            PropertyData deviceid = mbo.Properties["DeviceID"];
	            PropertyData drivetype = mbo.Properties["DriveType"];
	            PropertyData driveSerial = mbo.Properties["VolumeSerialNumber"];
	            PropertyData driveGUID = mbo.Properties["PNPDeviceID"];
                // Вывод информационного окна

                System.Windows.Application.Current.Dispatcher.Invoke(new Action(delegate()
                {
                    var TextWindows = string.Format("Отключенно устройство {1} \n Определенно как диск {0}{2} ", deviceid.Value, volumeName.Value, @"");
                    //MainWindow ConectoWorkSpace_InW = (MainWindow)Application.Current.MainWindow;
                    MainWindow ConectoWorkSpace_InW = AppStart.LinkMainWindow();

                    var WinOblakoVerh_ = AppStart.WindowActive("WinOblakoVerh_USBHDD", TextWindows, 1);
                    WinOblakoVerh_.Owner = ConectoWorkSpace_InW;

                    // Ссылка на объект (по ключ B52 потом компьютер)
                    var pic = ConectoWorkSpace_InW.DeviceOnOff;
                    var wP_SysI = ConectoWorkSpace_InW.wP_SysIndicat;

                    pic.Visibility = System.Windows.Visibility.Collapsed;
                    ConectoWorkSpace_InW.numberElementsInfoPa(-1);
                    double[] TopWidth = ConectoWorkSpace_InW.MessageCoordinatInfoPa(4);

                    // размещаем на рабочем столе
                    WinOblakoVerh_.Top = SystemConecto.WorkAreaDisplayDefault[0] + wP_SysI.Margin.Top - (WinOblakoVerh_.Height - 58);
                    WinOblakoVerh_.Left = SystemConecto.WorkAreaDisplayDefault[1] + (TopWidth[1]);  // wP_SysI.Margin.Left - 28
                    WinOblakoVerh_.Show(); // не показываем модальным (модальное окно не закрывается методом Close, а также не освобождает ресурсы метод fWait.Dispose(); )

                }));

	        }
	    }
        #endregion

        #region Размер накопителя
        private const int KB = 1024;
        private const int MB = KB * 1000;
	    private const int GB = MB * 1000;
 
        /// <summary>
        /// Размер емкости USB накопителя
        /// </summary>
        /// <param name="bytes"></param>
        /// <returns></returns>
	    private static string FormatByteCount(ulong bytes)
	    {
	        string format = null;
	 
	        if (bytes < KB)
	        {
	            format = String.Format("{0} Bytes", bytes);
	        }
	        else if (bytes < MB)
	        {
	            bytes = bytes / KB;
	            format = String.Format("{0} KB", bytes.ToString("N"));
            }
	        else if (bytes < GB)
	        {
	            double dree = bytes / MB;
	            format = String.Format("{0} MB", dree.ToString("N1"));
	        }
	        else
	        {
	            double gree = bytes / GB;
	            format = String.Format("{0} GB", gree.ToString("N1"));
	        }
	 
	        return format;
	    }

        #endregion

        #region Список устройств
        /// <summary>
        /// Режим отладки работы ОС с устройствами
        /// 
        /// </summary>
        /// <param name="IdDev"> Id искомого устройства</param>
        /// <returns></returns> 
        public static bool ListDevicePC( ref string[] DevInfo, string IdDev = "") //params string[][] list
        {

            // Динамические параметры для вывода информации об устройстве
            //for (int i = 0; i < list.Length; i++)
            //{
                //list[i];
            //}

            bool StatusDev = false;

            // http://msdn.microsoft.com/en-us/library/aa394084(VS.85).aspx

            // События устройства миши
            // http://www.c-sharpcorner.com/forums/thread/156523/__cs_myImageUpload.aspx


            // Примеры работы с устройствами
            // http://www.codeproject.com/Articles/17123/Using-Raw-Input-from-C-to-handle-multiple-keyboard

            // Модемы
            //string query = "SELECT * FROM Win32_POTSModem";
            // COM-PORT - SerialPort
            //string query = "SELECT * FROM Win32_SerialPort";
            // Список всех подключаемых устройств по Plag and Play - Win32_PnPEntity - Условие {WHERE ConfigManagerErrorCode = 0}
            // работает без ошибок

            //SELECT * FROM Win32_DiskDrive WHERE Partitions < 2 OR SectorsPerTrack > 100

            //SELECT * FROM Win32_LogicalDisk WHERE (Name = "C:" OR Name = "D:") AND FreeSpace > 2000000 AND FileSystem = "NTFS"

            //SELECT * FROM Win32_NTLogEvent WHERE Logfile = 'Application'

            //SELECT * FROM Meta_Class WHERE __Class LIKE %Win32%

            //SELECT * FROM __InstanceCreationEvent WHERE TargetInstance ISA "Win32_NTLogEvent" GROUP WITHIN 600 BY TargetInstance.SourceName HAVING NumberOfEvents > 25
            
            //query = "SELECT * FROM Win32_Keyboard";

            string query = "SELECT * FROM Win32_PnPEntity WHERE ConfigManagerErrorCode = 0 ";
            if (IdDev.Count() == 0)
            {


            }
            else
            {
                // Guardant Stealth II
                query = "SELECT * FROM Win32_PnPEntity WHERE PNPDeviceID like '%" + IdDev + "%' OR  DeviceID like '%" + IdDev + "%' ";  //"USB\\\\VID_0A89"
                SystemConecto.ErorDebag(query);
            }


            try
            {

                string[] ModemObjects = new string[250];
                ManagementObjectSearcher searcher = new ManagementObjectSearcher(query);
                foreach (ManagementObject obj in searcher.Get())
                {
                    // Win32_POTSModem
                    // MessageBox.Show(obj["Name"].ToString() + "(" + obj["AttachedTo"].ToString() + ")");
                    // Win32_PnPEntity
                    // MessageBox.Show(obj["Name"].ToString());
                    string FindDev = Environment.NewLine + "Устройство: ";
                    foreach (PropertyData property in obj.Properties)
                    {
                        FindDev += property.Name + " || " + property.Value + Environment.NewLine;
                        if (property.Name == "Status" && (string)property.Value == "OK")  StatusDev = true; 
                        //
                    }
                    SystemConecto.ErorDebag(FindDev);

                    // По умолчанию требование к устройству больше чем подключенно.
                    if (StatusDev)
                    {
                        return true;
                    }
                    
                }
            }
            catch (Exception ExD)
            {
                SystemConecto.ErorDebag(ExD.Message);
                return false;
            }


            return false;
            // objectQuery = new ObjectQuery("SELECT * FROM Win32_PnPEntity WHERE ConfigManagerErrorCode = 0");

            //ManagementObjectSearcher comPortSearcher = new ManagementObjectSearcher(connectionScope, objectQuery);

            //using (comPortSearcher)

            //{



        }
        #endregion


    }
}
