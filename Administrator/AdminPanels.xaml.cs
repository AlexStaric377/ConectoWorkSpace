using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
// Управление сетью
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;	
using System.Security.Cryptography;

// Управление вводом-выводом
using System.IO;
using System.IO.Compression;
// BD
using FirebirdSql.Data.FirebirdClient;  // http://www.sql.ru/forum/actualthread.aspx?tid=133383   http://www.firebirdsql.org/en/net-examples-of-use/
using FirebirdSql.Data.Isql;

using System.Data;              // Содержит типы, независимые от провайдеров, например DataSet и DataTable.
using System.Data.SqlClient;    // Содержит типы SQL Server .NET Data Provider

// Управление Xml
using System.Xml;
using System.Xml.Linq;
/// Многопоточность
using System.Threading;
using System.Windows.Threading;
using ConectoWorkSpace.Systems;
using System.ServiceProcess;


using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

using Microsoft.Win32;
//using System.Windows.Forms;
// --- Process 
using System.Diagnostics;
using System.ComponentModel;
using System.Management;

// Анимированный гиф
using WpfAnimatedGif;

using mshtml;
using ConectoWorkSpace;



namespace ConectoWorkSpace.Administrator
{

    /// <summary>
    /// Логика взаимодействия для AdminPanels.xaml
    /// </summary>
    /// 

    
    public class NameTablLog
    {
        public NameTablLog(string TableName)
        {
            this.TableName = TableName;
        }
        public string TableName { get; set; }
    }
    public class GridTablLog
    {
        public GridTablLog(string NameTabl, string DateCopy, string ColumnDate)
        {
            this.NameTabl = NameTabl;
            this.DateCopy = DateCopy;
            this.ColumnDate = ColumnDate;
        }
        public string NameTabl { get; set; }
        public string DateCopy { get; set; }
        public string ColumnDate { get; set; }
    }

    public class MySpr
    {
        public MySpr(string Kod, string Name)
        {
            this.Kod = Kod;
            this.Name = Name;
        }
        public string Kod { get; set; }
        public string Name { get; set; }
    }
    public class MyTable
    {
        public MyTable( string Puth, string Alias, string NameServer, string Log, string Satellite)
        {
            
            this.Puth = Puth;
            this.Alias = Alias;
            this.NameServer = NameServer;
            this.Log = Log;
            this.Satellite = Satellite;

        }
        
        public string Puth { get; set; }
        public string Alias { get; set; }
        public string NameServer { get; set; }
        public string CurrentPassw  { get; set; }
        public string Log { get; set; }
        public string Satellite { get; set; }
    }
    public class MyTable30
    {
        public MyTable30( string Puth, string Alias,string NameServer, string Log, string Satellite)
        {
          
            this.Puth = Puth;
            this.Alias = Alias;
            this.NameServer = NameServer;
            this.Log = Log;
            this.Satellite = Satellite;

        }
       
        public string Puth { get; set; }
        public string Alias { get; set; }
        public string NameServer { get; set; }
        public string Log { get; set; }
        public string Satellite { get; set; }

    }

    public class TablePG
    {
        public TablePG(string Puth, string Alias, string NameServer, string Log, string Satellite)
        {

            this.Puth = Puth;
            this.Alias = Alias;
            this.NameServer = NameServer;
            this.Log = Log;
            this.Satellite = Satellite;

        }

        public string Puth { get; set; }
        public string Alias { get; set; }
        public string NameServer { get; set; }
        public string Log { get; set; }
        public string Satellite { get; set; }

    }

    public class ServerTable
    {
        public ServerTable(string Port, string Puth, string Name, string State, string CurrentPassw)
        {
            this.Port = Port;
            this.Puth = Puth;
            this.Name = Name;
            this.State = State;
            this.CurrentPassw = CurrentPassw;
        }
        public string Port { get; set; }
        public string Puth { get; set; }
        public string Name { get; set; }
        public string State { get; set; }
        public string CurrentPassw { get; set; }

    }
    public class ServerTable30
    {
        public ServerTable30(string Port, string Puth, string Name, string State, string CurrentPassw)
        {
            this.Port = Port;
            this.Puth = Puth;
            this.Name = Name;
            this.State = State;
            this.CurrentPassw = CurrentPassw;
        }
        public string Port { get; set; }
        public string Puth { get; set; }
        public string Name { get; set; }
        public string State { get; set; }
        public string CurrentPassw { get; set; }

    }

    public class ServerTablePG
    {
        public ServerTablePG(string Port, string Puth, string Name, string State, string CurrentPassw)
        {
            this.Port = Port;
            this.Puth = Puth;
            this.Name = Name;
            this.State = State;
            this.CurrentPassw = CurrentPassw;
        }
        public string Port { get; set; }
        public string Puth { get; set; }
        public string Name { get; set; }
        public string State { get; set; }
        public string CurrentPassw { get; set; }
    }

    public class GridBack
    {
        public GridBack(string Back, string Conect, string Server, string Puth, string Key)
        {
            this.Back = Back;
            this.Conect = Conect;
            this.Server = Server;
            this.Puth = Puth;
            this.Key = Key;
        }
 
        public string Back { get; set; }
        public string Conect { get; set; }
        public string Server { get; set; }
        public string Puth { get; set; }
        public string Key { get; set; }
    }

    public class GridFront
    {
        public GridFront(string Front, string Conect, string Server, string Puth, string Key)
        {
            this.Front = Front;
            this.Conect = Conect;
            this.Server = Server;
            this.Puth = Puth;
            this.Key = Key;
        }

        public string Front { get; set; }
        public string Conect { get; set; }
        public string Server { get; set; }
        public string Puth { get; set; }
        public string Key { get; set; }


    }

    public class AptuneGridFront
    {
        public AptuneGridFront(string Front, string Conect, string Server, string Puth)
        {
            this.Front = Front;
            this.Conect = Conect;
            this.Server = Server;
            this.Puth = Puth;
        }

        public string Front { get; set; }
        public string Conect { get; set; }
        public string Server { get; set; }
        public string Puth { get; set; }


    }

    public class GnclientTestKey
    {
        public GnclientTestKey(string Type, string Name, string Server, string Conect, string Port, string TcpIp, string Puth)
        {
            this.Type = Type;
            this.Name = Name;
            this.Server = Server;
            this.Conect = Conect;
            this.Port = Port;
            this.TcpIp = TcpIp;
            this.Puth = Puth;
        }
        public string Type { get; set; }
        public string Name { get; set; }
        public string Server { get; set; }
        public string Conect { get; set; }
        public string Port { get; set; }
        public string TcpIp { get; set; }
        public string Puth { get; set; }
    }

    public class GnclientLocKey
    {
        public GnclientLocKey(string Type, string Name, string Server, string Conect, string Port, string TcpIp, string Puth)
        {
            this.Type = Type;
            this.Name = Name;
            this.Server = Server;
            this.Conect = Conect;
            this.Port = Port;
            this.TcpIp = TcpIp;
            this.Puth = Puth;
        }
        public string Type { get; set; }
        public string Name { get; set; }
        public string Server { get; set; }
        public string Conect { get; set; }
        public string Port { get; set; }
        public string TcpIp { get; set; }
        public string Puth { get; set; }
    }

    public class GnclientNetKey
    {
        public GnclientNetKey(string Type, string Name, string Server, string Conect, string Port, string TcpIp, string Puth)
        {
            this.Type = Type;
            this.Name = Name;
            this.Server = Server;
            this.Conect = Conect;
            this.Port = Port;
            this.TcpIp = TcpIp;
            this.Puth = Puth;
        }
        public string Type { get; set; }
        public string Name { get; set; }
        public string Server { get; set; }
        public string Conect { get; set; }
        public string Port { get; set; }
        public string TcpIp { get; set; }
        public string Puth { get; set; }
    }

    public class ActivBackFront
    {
        public ActivBackFront(string Type, string Name, string Server, string Conect, string Port, string TcpIp, string Puth)
        {
            this.Type = Type;
            this.Name = Name;
            this.Server = Server;
            this.Conect = Conect;
            this.Port = Port;
            this.TcpIp = TcpIp;
            this.Puth = Puth;
        }
        public string Type { get; set; }
        public string Name { get; set; }
        public string Server { get; set; }
        public string Conect { get; set; }
        public string Port { get; set; }
        public string TcpIp { get; set; }
        public string Puth { get; set; }
    }

    public class ScheduleArhiv
    {
        public ScheduleArhiv(string Puth, string Arhiv, string SetDay, string SetTime, string Server)
        {
            this.Puth = Puth;
            this.Arhiv = Arhiv;
            this.SetDay = SetDay;
            this.SetTime = SetTime;
            this.Server = Server;

        }
        public string Puth { get; set; }
        public string Arhiv { get; set; }
        public string SetDay { get; set; }
        public string SetTime { get; set; }
        public string Server { get; set; }

    }

    public class GridIpRdp
    {
        public GridIpRdp(string Login, string Password, string TcpIp, string Conect, string Puth, string DatTim, 
            string OverAll_idApp, string OverAll_NameApp, string OverAll_AutorizeType, string OverAll_CaptionNamePlay,
            string OverAll_PuthFileIm, string OverAll_infoWorkSpace, string Panel_idApp, string Panel_LinkPanel, string Panel_TypeLink,
            string Panel_NumberPanel, string Panel_CaptionNameWorkSpace, string Panel_AppStartMetod, string Panel_TypeApp, string OnOff, string Autostart, string Other_Puth)
        {
            this.Login = Login;
            this.Password = Password;
            this.TcpIp = TcpIp;
            this.Conect = Conect;
            this.Puth = Puth;
            this.OverAll_idApp = OverAll_idApp;
            this.OverAll_NameApp = OverAll_NameApp;
            this.OverAll_AutorizeType = OverAll_AutorizeType;
            this.OverAll_CaptionNamePlay = OverAll_CaptionNamePlay;
            this.OverAll_PuthFileIm = OverAll_PuthFileIm;
            this.OverAll_infoWorkSpace = OverAll_infoWorkSpace;
            this.Panel_idApp = Panel_idApp;
            this.Panel_LinkPanel = Panel_LinkPanel;
            this.Panel_TypeLink = Panel_TypeLink;
            this.Panel_NumberPanel = Panel_NumberPanel;
            this.Panel_CaptionNameWorkSpace = Panel_CaptionNameWorkSpace;
            this.Panel_AppStartMetod = Panel_AppStartMetod;
            this.Panel_TypeApp = Panel_TypeApp;
            this.OnOff = OnOff;
            this.Autostart = Autostart;
            this.Other_Puth = Other_Puth;
        }

        public string Login { get; set; }
        public string Password { get; set; }
        public string TcpIp { get; set; }
        public string Conect { get; set; }
        public string Puth { get; set; }
        public string DatTim { get; set; }
        public string OverAll_idApp { get; set; }
        public string OverAll_NameApp { get; set; }
        public string OverAll_AutorizeType { get; set; }
        public string OverAll_CaptionNamePlay { get; set; }
        public string OverAll_PuthFileIm { get; set; }
        public string OverAll_infoWorkSpace { get; set; }
        public string Panel_idApp { get; set; }
        public string Panel_LinkPanel { get; set; }
        public string Panel_TypeLink { get; set; }
        public string Panel_NumberPanel { get; set; }
        public string Panel_CaptionNameWorkSpace { get; set; }
        public string Panel_AppStartMetod { get; set; }
        public string Panel_TypeApp { get; set; }
        public string OnOff { get; set; }
        public string Autostart { get; set; }
        public string Other_Puth { get; set; }
    }

    public class EtalonDeveloper
    {
        public EtalonDeveloper(string Puth, string DateCreate)
        {
            this.Puth = Puth;
            this.DateCreate = DateCreate;
        }
        public string Puth { get; set; }
        public string DateCreate { get; set; }
    }

    public class EtalonDistrybutor
    {
        public EtalonDistrybutor(string Puth, string DateCreate)
        {
            this.Puth = Puth;
            this.DateCreate = DateCreate;
        }
        public string Puth { get; set; }
        public string DateCreate { get; set; }
    }
    public class ProtokolCompare
    {
        public ProtokolCompare(string NubProtokol, string PuthDevelop, string PuthDistrybut, string DateCreate)
        {
            this.NubProtokol = NubProtokol;
            this.PuthDevelop = PuthDevelop;
            this.PuthDistrybut = PuthDistrybut;
            this.DateCreate = DateCreate;
        }
        public string NubProtokol { get; set; }
        public string PuthDevelop { get; set; }
        public string PuthDistrybut { get; set; }
        public string DateCreate { get; set; }
    }


    public partial class AdminPanels : Window
    {
        // Индикатор текущего состояния выполнения процесса установки приложения или инсталяционных ключей.
        // 0 - процесс не выполняется, 1- процесс выполняется. Индикатор устанавливается в 1 пристарте процесса 
        // установки в процедуре ObjektOnOff class Interface

        public static string NameObj = "", ToNameObj = "", PatchServerFB25 = "", PatchServerFB30 = "", PortDefault25 = "", PortDefault30 = "", NameServer25 = "", NameServer30 = "";
        public static int CountConfigSoft = 0, SetProcesRun = 0, LoadTableRestr = 0, TableKeyCount = 0, TimeAutoCloseWin = 0, DefaultFdb25 = 0, DefaultFdb30 = 0, SetNumber30 = 0, NumberLoadAppEvents = 0;
        public static string idApp = "", NameApp = "", AutorizeType = "", CaptionNamePlay = "", PuthFileIm = "", infoWorkSpace = "", OnOff = "", ValuePanel = "", AutostartTimeSec = "";
        public static string Panel_idApp = "", LinkPanel = "", TypeLink = "", NumberPanel = "", CaptionNameWorkSpace = "", AppStartMetod = "", TypeApp = "", TextCarentPasswABD25Txt = "";
        public static string NameSecurity2 = "";
        public static string NameSecurity3 = "";
        public static string[] ButtonPanel;
        public static string[] DeskNumberPanel;
        public static string PuthAptune = "", PuthGnclient = "", SelectAlias30 = "", NamberString = "", SetConf = "", NumberIdApp = "", ClickIcon = "", ShortDate = "", SetActiv25 = "", SetActiv30 = "";
        public static string LoginIprdp = "", PasswordIprdp = "", TcpIpIprdp = "", ConectIprdp = "", PuthIprdp = "", DatTimIprdp = "", PuthBdUpdate = "", PuthUpgradeBD25 = "";
        public static string ImageObj = "", SetPort25 = "", SetPuth25 = "", SetName25 = "", SetPort30 = "", SetPuth30 = "", SetName30 = "", SetRunInstal30 = "", AliasChageFront25 = "", Putch_Satellite ="";
        public static string FolderBack = "", ConectBack = "", ServerBack = "", PuthBack = "", FileKeyBack = "", FolderFront = "", ConectFront = "", ServerFront = "", PuthFront = "", SetWinSetHub = "";
        public static int StartFront = 1, StartBack = 1, StartServisBD = 1, StartServer = 1, StartShluz = 1, SetInitPanellicenziyaKey = 1, CurrentBack = 0, CurrentFront = 0, DeleteDefaultServer = 0, SetInitPanelTerminalRdp = 1, StartConectoServis =1;
        public static int ProcesEnd = 1, SetUpdateRestore = 1, SetInitPanelServerDB = 1, SetInitPanelSborka = 1, IndRecno = 0, IndexActivProces = -1, SetInitPanelSystemSuport = 1, SetInitPanelAdministrirovanie = 1, CountNameTable =0; // TerminalOnOff = 0
        public static int CountListRdp = 0, CountTableConect25 = 0, AllRecn = 0, SborkaOnOff = 0, CurrentBackCount = 0, CountServer25 = 0, CountServer30 = 0, CountTabLog = 0, CountNameTriger = 0;
        public static string[] AdrIp4 = new string[6], ServerName30 = new string[1], NameExeFile30 = new string[1], PortServer30 = new string[1], CurrentPassw30 = new string[1]; // массив адресов IP4  для различных панелей фронт, бек, шлюзы, сереверы. 
        public static string[] AdrIp6 = new string[6], ServerName25 = new string[1], NameExeFile25 = new string[1], PortServer25 = new string[1], CurrentPassw25 = new string[1];// массив адресов IP6  для различных панелей фронт, бек, шлюзы, сереверы. 
        public static string Ip4 = "", Ip6 = "", AdrIp4LenchFront = "", AdrIp6lenchFront = "", AdrIp4LenchBack = "", AdrIp6LenchBack = "", IpAdr = "", PuthServer = "", CurrentSereverPuth = "";
        public static string Inst2530 = "", BackAdresPortServer = "", FrontAdresPortServer = "", SetPortServer25 = "", SetPortServer30 = "", CallProgram = "", PuthBackOff = "";
        public static string hubdate = "", RunGbak = "", ArgumentCmd = "", SlectRow = "", SelectPuth = "", SelectAlias = "", PathFileBDText = "", ItemTextBoxPutchBackUpLoc = "";
        public static string InsertSetHub = "hub", FrontPutchBD = "", BackPutchBD = "", FolderFrontPuth = "", FolderBackPuth = "", ActivServer = "", NameSreverArhiv = "", OutPath = "";
        public static string NetKeyType = "", NetKeyName = "", NetKeyServer = "", NetKeyConect = "", NetKeyPort = "", NetKeyTcpIp = "", NetKeyPuth = "", ValueMetod = "", PathAddDataServer = "";
        public static string LocKeyType = "", LocKeyName = "", LocKeyServer = "", LocKeyConect = "", LocKeyPort = "", LocKeyTcpIp = "", LocKeyPuth = "", CurrentPasswFb25 = "", CurrentPasswFb30 = "";
        public static string TestKeyType = "", TestKeyName = "", TestKeyServer = "", TestKeyConect = "", TestKeyPort = "", TestKeyTcpIp = "", TestKeyPuth = "", ContentPuth = "";
        public static string KodOrgClearDB = "", PortSrv = "", SchedulePuth = "", ScheduleArhivp = "", ScheduleSetDay = "", ScheduleSetTime = "", PuthGrdsrv = "", Arh7Zip_return = "";
        public static string EtalonDeveloperPuth = "", EtalonDeveloperDateCreate = "", EtalonDistributorPuth = "", EtalonDistributorDateCreate = "", ProtokolComparePuth = "", ProtokolCompareDateCreate = "";
        public static string KeySecret = "Staric377", BdB52 = "", BdB52Satellite = "", AliasSatelite = "", SelectNameTablLog = "", SelectDateCopy = "", FiltrDate = "", AddNameTablLog="";
        public static string SetPortPG = "", SetPuthPG = "", SetNamePG = "", CurrentPasswPg = "", SetActivPG="", SelectPuthPG = "", SelectAliasPG = "", NameServerPG ="", Putch_SatellitePG = "";
        public static List<string[]> TableKey = new List<string[]>();
        public static List<string[]> TableConnection = new List<string[]>();
        //public static List<string[]> TableXml = new List<string[]>();
        public DateTime datepiker=  new DateTime();
        public DateTime datepikerS = new DateTime();
        public DateTime datepikerP = new DateTime();
        public static string[] NamePuth = new string[10], NameServer = new string[10], IdPort = new string[10];
        public static string[] SereverPuth25 = new string[7], SereverPort25 = new string[7], SereverName25 = new string[7];
        public static string[] PortServerPG = new string[7], NameExeFilePG = new string[7], ServerNamePG = new string[7], CurrentPasswPG = new string[7];
 
        public static string[] ActionProcess;
        protected Process[] procs;
        public XmlTextWriter WriterConfigXML = null;
        public static List<GridIpRdp> TableXml = new List<GridIpRdp>(1);
        public static bool ClickMouseUp_Servergrid25 = false;
        public AdminPanels()
        {
            InitializeComponent();
            ResolutionDisplay();
        }




        // Процедуры создания таблиц служебной БД для осблуживания сервисных функций установки и модификации серверов, БД, бекофисов, фронтофисов. 
        // Процедура создания таблице активных серверов FB25
        public static void CreateServerActivFB25()
        {
            DBConecto.DBopenFBConectionMemory(SystemBD.ConnnStringSystem().ToString(), "", "FbSystem");
            string strCmd = "select t.rdb$relation_name from rdb$relations t where t.rdb$relation_name = 'SERVERACTIVFB25'";
            DBConecto.UniQuery CountQuery = new DBConecto.UniQuery(strCmd, "FB");
            CountQuery.ExecuteQueryFillTable("Cursor_SERVERACTIVFB25");
            if (CountQuery.CacheDBAdapter != null && CountQuery.CacheDBAdapter.Tables["Cursor_SERVERACTIVFB25"].Rows.Count <= 0)
            {
                string StrCreate = "CREATE TABLE SERVERACTIVFB25  (" +
                                    "PORT  VARCHAR(5)  , " +
                                    "PUTH  VARCHAR(250) , " +
                                    "NAME  VARCHAR(30) , " +
                                    "DATAINSTALL  VARCHAR(15) ," +
                                    "ACTIVONOFF  VARCHAR(10) ," +
                                    "CURRENTPASSW VARCHAR(100) ) ; ";
                FbCommand CreateTable = new FbCommand(StrCreate, DBConecto.bdFbSystemConect);
                CreateTable.CommandType = CommandType.Text;
                CreateTable.ExecuteScalar();
                CreateTable.Dispose();
            }
            else
            {
                int Idcount = 0;
                string StrCreate = "select RDB$FIELD_NAME from rdb$relation_fields where RDB$RELATION_NAME = 'SERVERACTIVFB25'";
                FbCommand SelectTable = new FbCommand(StrCreate, DBConecto.bdFbSystemConect);
                SelectTable.CommandType = CommandType.Text;
                FbDataReader ReadOutTable = SelectTable.ExecuteReader();
                while (ReadOutTable.Read()) { Idcount = Idcount + 1; }
                ReadOutTable.Close();
                if (Idcount < 6)
                {
                    StrCreate = "ALTER TABLE SERVERACTIVFB25";
                    StrCreate = StrCreate + (Idcount < 4 ? " ADD DATAINSTALL  VARCHAR(15)    NOT NULL " : "");
                    StrCreate = StrCreate + (Idcount < 4 ? ", " : "") + (Idcount < 5 ? " ADD ACTIVONOFF  VARCHAR(10)    NOT NULL " : "");
                    StrCreate = StrCreate + (Idcount < 5 ? ", " : "") + (Idcount < 6 ? " ADD CURRENTPASSW VARCHAR(100)    NOT NULL " : "");
                    FbCommand AlterTable = new FbCommand(StrCreate, DBConecto.bdFbSystemConect);
                    AlterTable.CommandTimeout = 3;
                    AlterTable.CommandType = CommandType.Text;
                    AlterTable.ExecuteScalar();
                }
                string LengthField = "";
                StrCreate = "SELECT  F.RDB$CHARACTER_LENGTH FROM RDB$FIELDS F, RDB$RELATION_FIELDS R WHERE F.RDB$FIELD_NAME = R.RDB$FIELD_SOURCE AND R.RDB$RELATION_NAME = 'SERVERACTIVFB25' AND R.RDB$FIELD_NAME = 'PUTH'";
                SelectTable = new FbCommand(StrCreate, DBConecto.bdFbSystemConect);
                SelectTable.CommandType = CommandType.Text;
                ReadOutTable = SelectTable.ExecuteReader();
                while (ReadOutTable.Read())
                {
                    LengthField = ReadOutTable[0].ToString();
                }
                ReadOutTable.Close();
                if (Convert.ToInt16(LengthField) < 250)
                {
                    StrCreate = "ALTER TABLE SERVERACTIVFB25 ALTER PUTH TYPE VARCHAR(250)";
                    FbCommand AlterTable = new FbCommand(StrCreate, DBConecto.bdFbSystemConect);
                    AlterTable.CommandTimeout = 3;
                    AlterTable.CommandType = CommandType.Text;
                    AlterTable.ExecuteScalar();
                }
                SelectTable.Dispose();
                LengthField = "";
                StrCreate = "SELECT  F.RDB$CHARACTER_LENGTH FROM RDB$FIELDS F, RDB$RELATION_FIELDS R WHERE F.RDB$FIELD_NAME = R.RDB$FIELD_SOURCE AND R.RDB$RELATION_NAME = 'SERVERACTIVFB25' AND R.RDB$FIELD_NAME = 'CURRENTPASSW'";
                SelectTable = new FbCommand(StrCreate, DBConecto.bdFbSystemConect);
                SelectTable.CommandType = CommandType.Text;
                ReadOutTable = SelectTable.ExecuteReader();
                while (ReadOutTable.Read())
                {
                    LengthField = ReadOutTable[0].ToString();
                }
                ReadOutTable.Close();
                if (Convert.ToInt16(LengthField) < 100)
                {
                    StrCreate = "ALTER TABLE SERVERACTIVFB25 ALTER CURRENTPASSW TYPE VARCHAR(100)";
                    FbCommand AlterTable = new FbCommand(StrCreate, DBConecto.bdFbSystemConect);
                    AlterTable.CommandTimeout = 3;
                    AlterTable.CommandType = CommandType.Text;
                    AlterTable.ExecuteScalar();
                }
                SelectTable.Dispose();
            }
            DBConecto.DBcloseFBConectionMemory("FbSystem");
        }

        // Процедура создания таблице активных серверов FB30
        public static void CreateServerActivFB30()
        {
            DBConecto.DBopenFBConectionMemory(SystemBD.ConnnStringSystem().ToString(), "", "FbSystem");
            string strCmd = "select t.rdb$relation_name from rdb$relations t where t.rdb$relation_name = 'SERVERACTIVFB30'";
            DBConecto.UniQuery CountQuery = new DBConecto.UniQuery(strCmd, "FB");
            CountQuery.ExecuteQueryFillTable("Cursor_SERVERACTIVFB30");
            if (CountQuery.CacheDBAdapter != null && CountQuery.CacheDBAdapter.Tables["Cursor_SERVERACTIVFB30"].Rows.Count <= 0)
            {
                string StrCreate = "CREATE TABLE SERVERACTIVFB30  (" +
                                    "PORT  VARCHAR(5)  , " +
                                    "PUTH  VARCHAR(250) , " +
                                    "NAME  VARCHAR(30) , " +
                                    "DATAINSTALL  VARCHAR(15)," +
                                    "ACTIVONOFF  VARCHAR(10) ," +
                                    "CURRENTPASSW VARCHAR(100)); ";
                FbCommand CreateTable = new FbCommand(StrCreate, DBConecto.bdFbSystemConect);
                CreateTable.CommandType = CommandType.Text;
                CreateTable.ExecuteScalar();
                CreateTable.Dispose();
            }
            else
            {
                int Idcount = 0;
                string StrCreate = "select RDB$FIELD_NAME from rdb$relation_fields where RDB$RELATION_NAME = 'SERVERACTIVFB30'";
                FbCommand SelectTable = new FbCommand(StrCreate, DBConecto.bdFbSystemConect);
                SelectTable.CommandType = CommandType.Text;
                FbDataReader ReadOutTable = SelectTable.ExecuteReader();
                TableKey.Add(new string[3]);
                while (ReadOutTable.Read()) { Idcount = Idcount + 1; }
                ReadOutTable.Close();
                if (Idcount < 6)
                {
                    StrCreate = "ALTER TABLE SERVERACTIVFB30";
                    StrCreate = StrCreate + (Idcount < 4 ? " ADD DATAINSTALL  VARCHAR(15)    NOT NULL " : "");
                    StrCreate = StrCreate + (Idcount < 4 ? ", " : "") + (Idcount < 5 ? " ADD ACTIVONOFF  VARCHAR(10)    NOT NULL " : "");
                    StrCreate = StrCreate + (Idcount < 5 ? ", " : "") + (Idcount < 6 ? " ADD CURRENTPASSW VARCHAR(100)    NOT NULL " : "");
                    FbCommand AlterTable = new FbCommand(StrCreate, DBConecto.bdFbSystemConect);
                    AlterTable.CommandTimeout = 3;
                    AlterTable.CommandType = CommandType.Text;
                    AlterTable.ExecuteScalar();
                }
                string LengthField = "";
                StrCreate = "SELECT  F.RDB$CHARACTER_LENGTH FROM RDB$FIELDS F, RDB$RELATION_FIELDS R WHERE F.RDB$FIELD_NAME = R.RDB$FIELD_SOURCE AND R.RDB$RELATION_NAME = 'SERVERACTIVFB30' AND R.RDB$FIELD_NAME = 'PUTH'";
                SelectTable = new FbCommand(StrCreate, DBConecto.bdFbSystemConect);
                SelectTable.CommandType = CommandType.Text;
                ReadOutTable = SelectTable.ExecuteReader();
                while (ReadOutTable.Read())
                {
                    LengthField = ReadOutTable[0].ToString();
                }
                ReadOutTable.Close();
                if (Convert.ToInt16(LengthField) < 250)
                {
                    StrCreate = "ALTER TABLE SERVERACTIVFB30 ALTER PUTH TYPE VARCHAR(250)";
                    FbCommand AlterTable = new FbCommand(StrCreate, DBConecto.bdFbSystemConect);
                    AlterTable.CommandTimeout = 3;
                    AlterTable.CommandType = CommandType.Text;
                    AlterTable.ExecuteScalar();
                }
                SelectTable.Dispose();
                LengthField = "";
                StrCreate = "SELECT  F.RDB$CHARACTER_LENGTH FROM RDB$FIELDS F, RDB$RELATION_FIELDS R WHERE F.RDB$FIELD_NAME = R.RDB$FIELD_SOURCE AND R.RDB$RELATION_NAME = 'SERVERACTIVFB30' AND R.RDB$FIELD_NAME = 'CURRENTPASSW'";
                SelectTable = new FbCommand(StrCreate, DBConecto.bdFbSystemConect);
                SelectTable.CommandType = CommandType.Text;
                ReadOutTable = SelectTable.ExecuteReader();
                while (ReadOutTable.Read())
                {
                    LengthField = ReadOutTable[0].ToString();
                }
                ReadOutTable.Close();
                if (Convert.ToInt16(LengthField) < 100)
                {
                    StrCreate = "ALTER TABLE SERVERACTIVFB30 ALTER CURRENTPASSW TYPE VARCHAR(100)";
                    FbCommand AlterTable = new FbCommand(StrCreate, DBConecto.bdFbSystemConect);
                    AlterTable.CommandTimeout = 3;
                    AlterTable.CommandType = CommandType.Text;
                    AlterTable.ExecuteScalar();
                }
                SelectTable.Dispose();
            }
            DBConecto.DBcloseFBConectionMemory("FbSystem");
        }


        // Процедура создания таблице активных серверов FB30
        public static void CreateServerActivPostgresql()
        {
            DBConecto.DBopenFBConectionMemory(SystemBD.ConnnStringSystem().ToString(), "", "FbSystem");
            string strCmd = "select t.rdb$relation_name from rdb$relations t where t.rdb$relation_name = 'SERVERACTIVPOSTGRESQL'";
            DBConecto.UniQuery CountQuery = new DBConecto.UniQuery(strCmd, "FB");
            CountQuery.ExecuteQueryFillTable("Cursor_SERVERACTIVPOSTGRESQL");
            if (CountQuery.CacheDBAdapter != null && CountQuery.CacheDBAdapter.Tables["Cursor_SERVERACTIVPOSTGRESQL"].Rows.Count <= 0)
            {
                string StrCreate = "CREATE TABLE SERVERACTIVPOSTGRESQL  (" +
                                    "PORT  VARCHAR(5)  , " +
                                    "PUTH  VARCHAR(250) , " +
                                    "NAME  VARCHAR(30) , " +
                                    "DATAINSTALL  VARCHAR(15) ," +
                                    "ACTIVONOFF  VARCHAR(10) ," +
                                    "CURRENTPASSW VARCHAR(100)); ";
                FbCommand CreateTable = new FbCommand(StrCreate, DBConecto.bdFbSystemConect);
                CreateTable.CommandType = CommandType.Text;
                CreateTable.ExecuteScalar();
                CreateTable.Dispose();
            }
            DBConecto.DBcloseFBConectionMemory("FbSystem");
        }


        public static void CreateConnectionBD25()
        {
            DBConecto.DBopenFBConectionMemory(SystemBD.ConnnStringSystem().ToString(), "", "FbSystem");
            string strCmd = "select t.rdb$relation_name from rdb$relations t where t.rdb$relation_name = 'CONNECTIONBD25'";
            DBConecto.UniQuery CountQuery = new DBConecto.UniQuery(strCmd, "FB");
            CountQuery.ExecuteQueryFillTable("Cursor_CONNECTIONBD25");
            if (CountQuery.CacheDBAdapter != null && CountQuery.CacheDBAdapter.Tables["Cursor_CONNECTIONBD25"].Rows.Count <= 0)
            {
                string StrCreate = "CREATE TABLE CONNECTIONBD25  ( " +
                                        "PUTHBD  VARCHAR(250)  , " +
                                        "ALIASBD  VARCHAR(70) , " +
                                        "NAMESERVER VARCHAR(30) ," +
                                        "PUTHSERVER  VARCHAR(250)," +
                                        "UPDATETIME VARCHAR(20) ," +
                                        "LOG  VARCHAR(7)," +
                                        "SATELLITE  VARCHAR(250)); ";

                FbCommand CreateTable = new FbCommand(StrCreate, DBConecto.bdFbSystemConect);
                CreateTable.CommandType = CommandType.Text;
                CreateTable.ExecuteScalar();
                CreateTable.Dispose();
            }
            else
            {
                int Idcount = 0;
                string StrCreate = "select RDB$FIELD_NAME from rdb$relation_fields where RDB$RELATION_NAME = 'CONNECTIONBD25'";
                FbCommand SelectTable = new FbCommand(StrCreate, DBConecto.bdFbSystemConect);
                SelectTable.CommandType = CommandType.Text;
                FbDataReader ReadOutTable = SelectTable.ExecuteReader();
                TableKey.Add(new string[3]);
                while (ReadOutTable.Read()) { Idcount = Idcount + 1; }
                ReadOutTable.Close();
                if (Idcount < 7)
                {
                    StrCreate = "ALTER TABLE CONNECTIONBD25";
                    StrCreate = StrCreate + (Idcount < 4 ? " ADD PUTHSERVER  VARCHAR(250) " : "");
                    StrCreate = StrCreate + (Idcount < 4 ? ", " : "") + (Idcount < 5 ? " ADD UPDATETIME VARCHAR(20)  " : "");
                    StrCreate = StrCreate + (Idcount < 5 ? ", " : "") + (Idcount < 6 ? " ADD LOG VARCHAR(7) " : "");
                    StrCreate = StrCreate + (Idcount < 6 ? ", " : "") + (Idcount < 7 ? " ADD SATELLITE VARCHAR(250) " : "");
                    FbCommand AlterTable = new FbCommand(StrCreate, DBConecto.bdFbSystemConect);
                    AlterTable.CommandType = CommandType.Text;
                    AlterTable.ExecuteScalar();
                }

                string LengthField = "";
                StrCreate = "SELECT  F.RDB$CHARACTER_LENGTH FROM RDB$FIELDS F, RDB$RELATION_FIELDS R WHERE F.RDB$FIELD_NAME = R.RDB$FIELD_SOURCE AND R.RDB$RELATION_NAME = 'CONNECTIONBD25' AND R.RDB$FIELD_NAME = 'ALIASBD'";
                SelectTable = new FbCommand(StrCreate, DBConecto.bdFbSystemConect);
                SelectTable.CommandType = CommandType.Text;
                ReadOutTable = SelectTable.ExecuteReader();
                while (ReadOutTable.Read())
                {
                    LengthField = ReadOutTable[0].ToString();
                }
                ReadOutTable.Close();
                if (Convert.ToInt16(LengthField) < 250)
                {
                    StrCreate = "ALTER TABLE CONNECTIONBD25 ALTER ALIASBD TYPE VARCHAR(70)";
                    FbCommand AlterTable = new FbCommand(StrCreate, DBConecto.bdFbSystemConect);
                    AlterTable.CommandTimeout = 3;
                    AlterTable.CommandType = CommandType.Text;
                    AlterTable.ExecuteScalar();
                }
                SelectTable.Dispose();

                LengthField = "";
                StrCreate = "SELECT  F.RDB$CHARACTER_LENGTH FROM RDB$FIELDS F, RDB$RELATION_FIELDS R WHERE F.RDB$FIELD_NAME = R.RDB$FIELD_SOURCE AND R.RDB$RELATION_NAME = 'CONNECTIONBD25' AND R.RDB$FIELD_NAME = 'PUTHBD'";
                SelectTable = new FbCommand(StrCreate, DBConecto.bdFbSystemConect);
                SelectTable.CommandType = CommandType.Text;
                ReadOutTable = SelectTable.ExecuteReader();
                while (ReadOutTable.Read())
                {
                    LengthField = ReadOutTable[0].ToString();
                }
                ReadOutTable.Close();
                if (Convert.ToInt16(LengthField) < 250)
                {
                    StrCreate = "ALTER TABLE CONNECTIONBD25 ALTER PUTHBD TYPE VARCHAR(250)";
                    FbCommand AlterTable = new FbCommand(StrCreate, DBConecto.bdFbSystemConect);
                    AlterTable.CommandTimeout = 3;
                    AlterTable.CommandType = CommandType.Text;
                    AlterTable.ExecuteScalar();
                }
                SelectTable.Dispose();
                // Проверка имени поля в описании таблицы БД
                //string LengthField = "";
                //StrCreate = "SELECT  R.RDB$FIELD_NAME FROM RDB$FIELDS F, RDB$RELATION_FIELDS R WHERE F.RDB$FIELD_NAME = R.RDB$FIELD_SOURCE AND R.RDB$RELATION_NAME = 'CONNECTIONBD25' AND R.RDB$FIELD_NAME = 'UPDATETIME'";
                //FbCommand SelectFIELD = new FbCommand(StrCreate, DBConecto.bdFbSystemConect);
                //SelectTable.CommandType = CommandType.Text;
                //FbDataReader ReadOutFIELD = SelectTable.ExecuteReader();
                //while (ReadOutFIELD.Read())
                //{ LengthField = ReadOutFIELD[4].ToString(); }
                //ReadOutFIELD.Close();
                //if (LengthField != "UPDATETIME")
                //{
                //    StrCreate = "ALTER TABLE CONNECTIONBD25 ADD UPDATETIME VARCHAR(20) NOT NULL";
                //    FbCommand AlterTable = new FbCommand(StrCreate, DBConecto.bdFbSystemConect);
                //    AlterTable.CommandTimeout = 3;
                //    AlterTable.CommandType = CommandType.Text;
                //    AlterTable.ExecuteScalar();
                //}

            }

            DBConecto.DBcloseFBConectionMemory("FbSystem");
        }

        public static void CreateConnectionBD30()
        {
            DBConecto.DBopenFBConectionMemory(SystemBD.ConnnStringSystem().ToString(), "", "FbSystem");
            string strCmd = "select t.rdb$relation_name from rdb$relations t where t.rdb$relation_name = 'CONNECTIONBD30'";
            DBConecto.UniQuery CountQuery = new DBConecto.UniQuery(strCmd, "FB");
            CountQuery.ExecuteQueryFillTable("Cursor_CONNECTIONBD30");
            if (CountQuery.CacheDBAdapter != null && CountQuery.CacheDBAdapter.Tables["Cursor_CONNECTIONBD30"].Rows.Count <= 0)
            {
                string StrCreate = "CREATE TABLE CONNECTIONBD30  ( " +
                                        "PUTHBD  VARCHAR(250)    , " +
                                        "ALIASBD  VARCHAR(70)  , " +
                                        "NAMESERVER VARCHAR(30) ," +
                                        "PUTHSERVER  VARCHAR(250)  ," +
                                        "UPDATETIME VARCHAR(20) ," +
                                        "LOG  VARCHAR(7)," +
                                        "SATELLITE  VARCHAR(250)); ";
                FbCommand CreateTable = new FbCommand(StrCreate, DBConecto.bdFbSystemConect);
                CreateTable.CommandType = CommandType.Text;
                string cret = CreateTable.ExecuteScalar() == null ? "" : CreateTable.ExecuteScalar().ToString();
                CreateTable.Dispose();

            }
            else
            {
                int Idcount = 0;
                string StrCreate = "select RDB$FIELD_NAME from rdb$relation_fields where RDB$RELATION_NAME = 'CONNECTIONBD30'";
                FbCommand SelectTable = new FbCommand(StrCreate, DBConecto.bdFbSystemConect);
                SelectTable.CommandType = CommandType.Text;
                FbDataReader ReadOutTable = SelectTable.ExecuteReader();
                TableKey.Add(new string[3]);
                while (ReadOutTable.Read()) { Idcount = Idcount + 1; }
                ReadOutTable.Close();
                if (Idcount < 7)
                {
                    StrCreate = "ALTER TABLE CONNECTIONBD30";
                    StrCreate = StrCreate + (Idcount < 4 ? " ADD PUTHSERVER  VARCHAR(250)   " : "");
                    StrCreate = StrCreate + (Idcount < 4 ? ", " : "") + (Idcount < 5 ? " ADD UPDATETIME VARCHAR(20)  " : "");
                    StrCreate = StrCreate + (Idcount < 5 ? ", " : "") + (Idcount < 6 ? " ADD LOG VARCHAR(7) " : "");
                    StrCreate = StrCreate + (Idcount < 6 ? ", " : "") + (Idcount < 7 ? " ADD SATELLITE VARCHAR(250) " : "");
                    FbCommand AlterTable = new FbCommand(StrCreate, DBConecto.bdFbSystemConect);
                    AlterTable.CommandType = CommandType.Text;
                    AlterTable.ExecuteScalar();
                }
                SelectTable.Dispose();
                string LengthField = "";
                StrCreate = "SELECT  F.RDB$CHARACTER_LENGTH FROM RDB$FIELDS F, RDB$RELATION_FIELDS R WHERE F.RDB$FIELD_NAME = R.RDB$FIELD_SOURCE AND R.RDB$RELATION_NAME = 'CONNECTIONBD30' AND R.RDB$FIELD_NAME = 'PUTHBD'";
                SelectTable = new FbCommand(StrCreate, DBConecto.bdFbSystemConect);
                SelectTable.CommandType = CommandType.Text;
                ReadOutTable = SelectTable.ExecuteReader();
                while (ReadOutTable.Read())
                {
                    LengthField = ReadOutTable[0].ToString();
                }
                ReadOutTable.Close();
                if (Convert.ToInt16(LengthField) < 250)
                {
                    StrCreate = "ALTER TABLE CONNECTIONBD30 ALTER PUTHBD TYPE VARCHAR(250)";
                    FbCommand AlterTable = new FbCommand(StrCreate, DBConecto.bdFbSystemConect);
                    AlterTable.CommandTimeout = 3;
                    AlterTable.CommandType = CommandType.Text;
                    AlterTable.ExecuteScalar();
                }
                SelectTable.Dispose();
                LengthField = "";
                StrCreate = "SELECT  F.RDB$CHARACTER_LENGTH FROM RDB$FIELDS F, RDB$RELATION_FIELDS R WHERE F.RDB$FIELD_NAME = R.RDB$FIELD_SOURCE AND R.RDB$RELATION_NAME = 'CONNECTIONBD30' AND R.RDB$FIELD_NAME = 'ALIASBD'";
                SelectTable = new FbCommand(StrCreate, DBConecto.bdFbSystemConect);
                SelectTable.CommandType = CommandType.Text;
                ReadOutTable = SelectTable.ExecuteReader();
                while (ReadOutTable.Read())
                {
                    LengthField = ReadOutTable[0].ToString();
                }
                ReadOutTable.Close();
                if (Convert.ToInt16(LengthField) < 250)
                {
                    StrCreate = "ALTER TABLE CONNECTIONBD30 ALTER ALIASBD TYPE VARCHAR(70)";
                    FbCommand AlterTable = new FbCommand(StrCreate, DBConecto.bdFbSystemConect);
                    AlterTable.CommandTimeout = 3;
                    AlterTable.CommandType = CommandType.Text;
                    AlterTable.ExecuteScalar();
                }
                SelectTable.Dispose();
            }
            DBConecto.DBcloseFBConectionMemory("FbSystem");
        }

        public static void CreateConnectionPostgresql()
        {
            DBConecto.DBopenFBConectionMemory(SystemBD.ConnnStringSystem().ToString(), "", "FbSystem");
            string strCmd = "select t.rdb$relation_name from rdb$relations t where t.rdb$relation_name = 'CONNECTIONPOSTGRESQL'";
            DBConecto.UniQuery CountQuery = new DBConecto.UniQuery(strCmd, "FB");
            CountQuery.ExecuteQueryFillTable("Cursor_CONNECTIONPOSTGRESQL");
            if (CountQuery.CacheDBAdapter != null && CountQuery.CacheDBAdapter.Tables["Cursor_CONNECTIONPOSTGRESQL"].Rows.Count <= 0)
            {
                string StrCreate = "CREATE TABLE CONNECTIONPOSTGRESQL  ( " +
                                        "PUTHBD  VARCHAR(250)    , " +
                                        "ALIASBD  VARCHAR(70)  , " +
                                        "NAMESERVER VARCHAR(30) ," +
                                        "PUTHSERVER  VARCHAR(250) ," +
                                        "UPDATETIME VARCHAR(20) ,"+
                                        "LOG  VARCHAR(7)," +
                                        "SATELLITE  VARCHAR(250)); ";
                // ID  NUMERIC(12)  NOT NULL,  "PRIMARY KEY (ID)";
                FbCommand CreateTable = new FbCommand(StrCreate, DBConecto.bdFbSystemConect);
                CreateTable.CommandType = CommandType.Text;
                CreateTable.ExecuteScalar();
                CreateTable.Dispose();
            }
            DBConecto.DBcloseFBConectionMemory("FbSystem");
        }
        // Процедура создания таблицы  установленных Бек Офисов
        public static void CreateBackOfice()
        {
            string StrCreate = "";
            DBConecto.DBopenFBConectionMemory(SystemBD.ConnnStringSystem().ToString(), "", "FbSystem");
            string strCmd = "select t.rdb$relation_name from rdb$relations t where t.rdb$relation_name = 'REESTRBACK'";
            DBConecto.UniQuery CountQuery = new DBConecto.UniQuery(strCmd, "FB");
            CountQuery.ExecuteQueryFillTable("Cursor_REESTRBACK");
            if (CountQuery.CacheDBAdapter != null && CountQuery.CacheDBAdapter.Tables["Cursor_REESTRBACK"].Rows.Count <= 0)
            {
                StrCreate = "CREATE TABLE REESTRBACK  (" +
                                    "BACK  VARCHAR(25)  , " +
                                    "CONECT  VARCHAR(70) , " +
                                    "SERVER  VARCHAR(30) ," +
                                    "PUTH  VARCHAR(250) , " +
                                    "KEY VARCHAR(25)  ," +
                                    "UPDATETIME VARCHAR(20)  )";

                FbCommand CreateTable = new FbCommand(StrCreate, DBConecto.bdFbSystemConect);
                CreateTable.CommandType = CommandType.Text;
                CreateTable.ExecuteScalar();
                CreateTable.Dispose();
            }
            else
            {
                int Idcount = 0;
                StrCreate = "select RDB$FIELD_NAME from rdb$relation_fields where RDB$RELATION_NAME = 'REESTRBACK'";
                FbCommand SelectTable = new FbCommand(StrCreate, DBConecto.bdFbSystemConect);
                SelectTable.CommandType = CommandType.Text;
                FbDataReader ReadOutTable = SelectTable.ExecuteReader();
                while (ReadOutTable.Read()) { Idcount = Idcount + 1; }
                ReadOutTable.Close();
                if (Idcount < 6)
                {
                    StrCreate = "ALTER TABLE REESTRBACK";
                    StrCreate = StrCreate + (Idcount < 6 ? " ADD UPDATETIME VARCHAR(20)   " : "");
                    FbCommand AlterTable = new FbCommand(StrCreate, DBConecto.bdFbSystemConect);
                    AlterTable.CommandType = CommandType.Text;
                    AlterTable.ExecuteScalar();
                }

                string LengthField = "";
                StrCreate = "SELECT  F.RDB$CHARACTER_LENGTH FROM RDB$FIELDS F, RDB$RELATION_FIELDS R WHERE F.RDB$FIELD_NAME = R.RDB$FIELD_SOURCE AND R.RDB$RELATION_NAME = 'REESTRBACK' AND R.RDB$FIELD_NAME = 'PUTH'";
                FbCommand SelectCHAR = new FbCommand(StrCreate, DBConecto.bdFbSystemConect);
                SelectCHAR.CommandType = CommandType.Text;
                FbDataReader ReadOutCHAR = SelectCHAR.ExecuteReader();
                while (ReadOutCHAR.Read())
                { LengthField = ReadOutCHAR[0].ToString(); }
                ReadOutCHAR.Close();
                if (Convert.ToInt16(LengthField) < 250)
                {
                    StrCreate = "ALTER TABLE REESTRBACK ALTER PUTH TYPE VARCHAR(250)";
                    FbCommand AlterTable = new FbCommand(StrCreate, DBConecto.bdFbSystemConect);
                    AlterTable.CommandTimeout = 3;
                    AlterTable.CommandType = CommandType.Text;
                    AlterTable.ExecuteScalar();
                }
                SelectCHAR.Dispose();
            }
            DBConecto.DBcloseFBConectionMemory("FbSystem");
        }
        // Процедура создания таблицы  установленных Фронт Офисов
        public static void CreateFrontOfice()
        {
            string StrCreate = "";
            DBConecto.DBopenFBConectionMemory(SystemBD.ConnnStringSystem().ToString(), "", "FbSystem");
            string strCmd = "select t.rdb$relation_name from rdb$relations t where t.rdb$relation_name = 'REESTRFRONT'";
            DBConecto.UniQuery CountQuery = new DBConecto.UniQuery(strCmd, "FB");
            CountQuery.ExecuteQueryFillTable("Cursor_REESTRFRONT");
            if (CountQuery.CacheDBAdapter != null && CountQuery.CacheDBAdapter.Tables["Cursor_REESTRFRONT"].Rows.Count <= 0)
            {
                StrCreate = "CREATE TABLE REESTRFRONT  (" +
                                    "FRONT  VARCHAR(25)  , " +
                                    "CONECT  VARCHAR(70) , " +
                                    "SERVER  VARCHAR(30)  ," +
                                    "PUTH  VARCHAR(250) , " +
                                    "KEY VARCHAR(25)   ," +
                                    "UPDATETIME VARCHAR(20) )";
                FbCommand CreateTable = new FbCommand(StrCreate, DBConecto.bdFbSystemConect);
                CreateTable.CommandType = CommandType.Text;
                CreateTable.ExecuteScalar();
                CreateTable.Dispose();
            }
            else
            {
                int Idcount = 0;
                StrCreate = "select RDB$FIELD_NAME from rdb$relation_fields where RDB$RELATION_NAME = 'REESTRFRONT'";
                FbCommand SelectFront = new FbCommand(StrCreate, DBConecto.bdFbSystemConect);
                SelectFront.CommandType = CommandType.Text;
                FbDataReader ReadOutFront = SelectFront.ExecuteReader();
                while (ReadOutFront.Read()) { Idcount = Idcount + 1; }
                ReadOutFront.Close();
                if (Idcount < 6)
                {
                    StrCreate = "ALTER TABLE REESTRFRONT";
                    StrCreate = StrCreate + (Idcount < 6 ? " ADD UPDATETIME VARCHAR(20)  " : "");
                    FbCommand AlterTable = new FbCommand(StrCreate, DBConecto.bdFbSystemConect);
                    AlterTable.CommandType = CommandType.Text;
                    AlterTable.ExecuteScalar();
                }
                SelectFront.Dispose();
                string LengthField = "";
                StrCreate = "SELECT  F.RDB$CHARACTER_LENGTH FROM RDB$FIELDS F, RDB$RELATION_FIELDS R WHERE F.RDB$FIELD_NAME = R.RDB$FIELD_SOURCE AND R.RDB$RELATION_NAME = 'REESTRBACK' AND R.RDB$FIELD_NAME = 'PUTH'";
                FbCommand SelectTable = new FbCommand(StrCreate, DBConecto.bdFbSystemConect);
                SelectTable.CommandType = CommandType.Text;
                FbDataReader ReadOutTable = SelectTable.ExecuteReader();
                while (ReadOutTable.Read())
                { LengthField = ReadOutTable[0].ToString();
                }
                ReadOutTable.Close();
                if (Convert.ToInt16(LengthField) < 250)
                {
                    StrCreate = "ALTER TABLE REESTRFRONT ALTER PUTH TYPE VARCHAR(250)";
                    FbCommand AlterTable = new FbCommand(StrCreate, DBConecto.bdFbSystemConect);
                    AlterTable.CommandTimeout = 3;
                    AlterTable.CommandType = CommandType.Text;
                    AlterTable.ExecuteScalar();
                }
                SelectTable.Dispose();
            }
            DBConecto.DBcloseFBConectionMemory("FbSystem");
        }

        // Процедура создания таблицы  беков и фронтов работающих под тестовым ключом
        public static void CreateTestKey()
        {
            DBConecto.DBopenFBConectionMemory(SystemBD.ConnnStringSystem().ToString(), "", "FbSystem");
            string strCmd = "select t.rdb$relation_name from rdb$relations t where t.rdb$relation_name = 'TESTKEY'";
            string StrCreate = "";
            DBConecto.UniQuery CountQuery = new DBConecto.UniQuery(strCmd, "FB");
            CountQuery.ExecuteQueryFillTable("Cursor_TESTKEY");
            if (CountQuery.CacheDBAdapter != null && CountQuery.CacheDBAdapter.Tables["Cursor_TESTKEY"].Rows.Count <= 0)
            {
                StrCreate = "CREATE TABLE TESTKEY  (" +
                                    "TYPE  VARCHAR(25) , " +
                                    "NAME  VARCHAR(25)  , " +
                                    "SERVER  VARCHAR(30)  ," +
                                    "CONECT  VARCHAR(70) , " +
                                    "PORT  VARCHAR(10)  , " +
                                    "TCPIP  VARCHAR(20)  , " +
                                    "PUTH  VARCHAR(250) );";
                FbCommand CreateTable = new FbCommand(StrCreate, DBConecto.bdFbSystemConect);
                CreateTable.CommandType = CommandType.Text;
                CreateTable.ExecuteScalar();
                CreateTable.Dispose();
            }
            else
            {
                string LengthField = "";
                StrCreate = "SELECT  F.RDB$CHARACTER_LENGTH FROM RDB$FIELDS F, RDB$RELATION_FIELDS R WHERE F.RDB$FIELD_NAME = R.RDB$FIELD_SOURCE AND R.RDB$RELATION_NAME = 'TESTKEY' AND R.RDB$FIELD_NAME = 'PUTH'";
                FbCommand SelectTable = new FbCommand(StrCreate, DBConecto.bdFbSystemConect);
                SelectTable.CommandType = CommandType.Text;
                FbDataReader ReadOutTable = SelectTable.ExecuteReader();
                while (ReadOutTable.Read())
                {
                    LengthField = ReadOutTable[0].ToString();
                }
                ReadOutTable.Close();
                if (Convert.ToInt16(LengthField) < 250)
                {
                    StrCreate = "ALTER TABLE TESTKEY ALTER PUTH TYPE VARCHAR(250)";
                    FbCommand AlterTable = new FbCommand(StrCreate, DBConecto.bdFbSystemConect);
                    AlterTable.CommandTimeout = 3;
                    AlterTable.CommandType = CommandType.Text;
                    AlterTable.ExecuteScalar();
                }
                SelectTable.Dispose();
            }
            DBConecto.DBcloseFBConectionMemory("FbSystem");
        }

        // Процедура создания таблицы  беков и фронтов работающих под локальным ключом
        public static void CreateLocKey()
        {
            DBConecto.DBopenFBConectionMemory(SystemBD.ConnnStringSystem().ToString(), "", "FbSystem");
            string StrCreate = "";
            string strCmd = "select t.rdb$relation_name from rdb$relations t where t.rdb$relation_name = 'LOCKEY'";
            DBConecto.UniQuery CountQuery = new DBConecto.UniQuery(strCmd, "FB");
            CountQuery.ExecuteQueryFillTable("Cursor_LOCKEY");
            if (CountQuery.CacheDBAdapter != null && CountQuery.CacheDBAdapter.Tables["Cursor_LOCKEY"].Rows.Count <= 0)
            {
                StrCreate = "CREATE TABLE LOCKEY  (" +
                                    "TYPE  VARCHAR(25)  , " +
                                    "NAME  VARCHAR(25)  , " +
                                    "SERVER  VARCHAR(30)  ," +
                                    "CONECT  VARCHAR(70) , " +
                                    "PORT  VARCHAR(10)  , " +
                                    "TCPIP  VARCHAR(20)  , " +
                                    "PUTH  VARCHAR(250)  );";
                FbCommand CreateTable = new FbCommand(StrCreate, DBConecto.bdFbSystemConect);
                CreateTable.CommandType = CommandType.Text;
                CreateTable.ExecuteScalar();
                CreateTable.Dispose();
            }
            else
            {
                string LengthField = "";
                StrCreate = "SELECT  F.RDB$CHARACTER_LENGTH FROM RDB$FIELDS F, RDB$RELATION_FIELDS R WHERE F.RDB$FIELD_NAME = R.RDB$FIELD_SOURCE AND R.RDB$RELATION_NAME = 'LOCKEY' AND R.RDB$FIELD_NAME = 'PUTH'";
                FbCommand SelectTable = new FbCommand(StrCreate, DBConecto.bdFbSystemConect);
                SelectTable.CommandType = CommandType.Text;
                FbDataReader ReadOutTable = SelectTable.ExecuteReader();
                while (ReadOutTable.Read())
                {
                    LengthField = ReadOutTable[0].ToString();
                }
                ReadOutTable.Close();
                if (Convert.ToInt16(LengthField) < 250)
                {
                    StrCreate = "ALTER TABLE LOCKEY ALTER PUTH TYPE VARCHAR(250)";
                    FbCommand AlterTable = new FbCommand(StrCreate, DBConecto.bdFbSystemConect);
                    AlterTable.CommandTimeout = 3;
                    AlterTable.CommandType = CommandType.Text;
                    AlterTable.ExecuteScalar();
                }
                SelectTable.Dispose();
            }

            DBConecto.DBcloseFBConectionMemory("FbSystem");
        }
        // Процедура создания таблицы  беков и фронтов работающих под сетевым ключом
        public static void CreateNetKey()
        {
            DBConecto.DBopenFBConectionMemory(SystemBD.ConnnStringSystem().ToString(), "", "FbSystem");
            string StrCreate = "";
            string strCmd = "select t.rdb$relation_name from rdb$relations t where t.rdb$relation_name = 'NETKEY'";
            DBConecto.UniQuery CountQuery = new DBConecto.UniQuery(strCmd, "FB");
            CountQuery.ExecuteQueryFillTable("Cursor_NETKEY");
            if (CountQuery.CacheDBAdapter != null && CountQuery.CacheDBAdapter.Tables["Cursor_NETKEY"].Rows.Count <= 0)
            {
                StrCreate = "CREATE TABLE NETKEY  (" +
                                    "TYPE  VARCHAR(25)  , " +
                                    "NAME  VARCHAR(25)  , " +
                                    "SERVER  VARCHAR(30) ," +
                                    "CONECT  VARCHAR(70)  , " +
                                    "PORT  VARCHAR(10)  , " +
                                    "TCPIP  VARCHAR(20) , " +
                                    "PUTH  VARCHAR(250) );";
                FbCommand CreateTable = new FbCommand(StrCreate, DBConecto.bdFbSystemConect);
                CreateTable.CommandType = CommandType.Text;
                CreateTable.ExecuteScalar();
                CreateTable.Dispose();
            }
            else
            {
                string LengthField = "";
                StrCreate = "SELECT  F.RDB$CHARACTER_LENGTH FROM RDB$FIELDS F, RDB$RELATION_FIELDS R WHERE F.RDB$FIELD_NAME = R.RDB$FIELD_SOURCE AND R.RDB$RELATION_NAME = 'NETKEY' AND R.RDB$FIELD_NAME = 'PUTH'";
                FbCommand SelectTable = new FbCommand(StrCreate, DBConecto.bdFbSystemConect);
                SelectTable.CommandType = CommandType.Text;
                FbDataReader ReadOutTable = SelectTable.ExecuteReader();
                while (ReadOutTable.Read())
                {
                    LengthField = ReadOutTable[0].ToString();
                }
                ReadOutTable.Close();
                if (Convert.ToInt16(LengthField) < 250)
                {
                    StrCreate = "ALTER TABLE NETKEY ALTER PUTH TYPE VARCHAR(250)";
                    FbCommand AlterTable = new FbCommand(StrCreate, DBConecto.bdFbSystemConect);
                    AlterTable.CommandTimeout = 3;
                    AlterTable.CommandType = CommandType.Text;
                    AlterTable.ExecuteScalar();
                }
                SelectTable.Dispose();
            }

            DBConecto.DBcloseFBConectionMemory("FbSystem");
        }

        // Процедура создания таблицы  беков и фронтов работающих под сетевым ключом
        public static void CreateActivBackFront()
        {
            DBConecto.DBopenFBConectionMemory(SystemBD.ConnnStringSystem().ToString(), "", "FbSystem");
            string StrCreate = "";
            string strCmd = "select t.rdb$relation_name from rdb$relations t where t.rdb$relation_name = 'ACTIVBACKFRONT'";
            DBConecto.UniQuery CountQuery = new DBConecto.UniQuery(strCmd, "FB");
            CountQuery.ExecuteQueryFillTable("Cursor_ACTIVBACKFRONT");
            if (CountQuery.CacheDBAdapter != null && CountQuery.CacheDBAdapter.Tables["Cursor_ACTIVBACKFRONT"].Rows.Count <= 0)
            {
                StrCreate = "CREATE TABLE ACTIVBACKFRONT  (" +
                                    "TYPE  VARCHAR(25) , " +
                                    "NAME  VARCHAR(25)  , " +
                                    "SERVER  VARCHAR(30)  ," +
                                    "CONECT  VARCHAR(70) , " +
                                    "PORT  VARCHAR(10)  , " +
                                    "TCPIP  VARCHAR(20) , " +
                                    "PUTH  VARCHAR(250)  );";
                FbCommand CreateTable = new FbCommand(StrCreate, DBConecto.bdFbSystemConect);
                CreateTable.CommandType = CommandType.Text;
                CreateTable.ExecuteScalar();
                CreateTable.Dispose();
            }
            else
            {
                string LengthField = "";
                StrCreate = "SELECT  F.RDB$CHARACTER_LENGTH FROM RDB$FIELDS F, RDB$RELATION_FIELDS R WHERE F.RDB$FIELD_NAME = R.RDB$FIELD_SOURCE AND R.RDB$RELATION_NAME = 'ACTIVBACKFRONT' AND R.RDB$FIELD_NAME = 'PUTH'";
                FbCommand SelectTable = new FbCommand(StrCreate, DBConecto.bdFbSystemConect);
                SelectTable.CommandType = CommandType.Text;
                FbDataReader ReadOutTable = SelectTable.ExecuteReader();
                while (ReadOutTable.Read())
                {
                    LengthField = ReadOutTable[0].ToString();
                }
                ReadOutTable.Close();
                if (Convert.ToInt16(LengthField) < 250)
                {
                    StrCreate = "ALTER TABLE ACTIVBACKFRONT ALTER PUTH TYPE VARCHAR(250)";
                    FbCommand AlterTable = new FbCommand(StrCreate, DBConecto.bdFbSystemConect);
                    AlterTable.CommandTimeout = 3;
                    AlterTable.CommandType = CommandType.Text;
                    AlterTable.ExecuteScalar();
                }
                SelectTable.Dispose();
            }


            DBConecto.DBcloseFBConectionMemory("FbSystem");
        }

        public static void CreateScheduleArhiv()
        {
            DBConecto.DBopenFBConectionMemory(SystemBD.ConnnStringSystem().ToString(), "", "FbSystem");
            string StrCreate = "";
            string strCmd = "select t.rdb$relation_name from rdb$relations t where t.rdb$relation_name = 'SCHEDULEARHIV'";
            DBConecto.UniQuery CountQuery = new DBConecto.UniQuery(strCmd, "FB");
            CountQuery.ExecuteQueryFillTable("Cursor_REESTRBACK");
            if (CountQuery.CacheDBAdapter != null && CountQuery.CacheDBAdapter.Tables["Cursor_REESTRBACK"].Rows.Count <= 0)
            {
                StrCreate = "CREATE TABLE SCHEDULEARHIV  (" +
                                    "PUTH  VARCHAR(250) , " +
                                    "ARHIV  VARCHAR(70)  , " +
                                    "SETDAY  VARCHAR(25)  ," +
                                    "SETTIME  VARCHAR(70) , " +
                                    "SERVER VARCHAR(30)  )";
                FbCommand CreateTable = new FbCommand(StrCreate, DBConecto.bdFbSystemConect);
                CreateTable.CommandType = CommandType.Text;
                CreateTable.ExecuteScalar();
                CreateTable.Dispose();
            }
            else
            {
                string LengthField = "";
                StrCreate = "SELECT  F.RDB$CHARACTER_LENGTH FROM RDB$FIELDS F, RDB$RELATION_FIELDS R WHERE F.RDB$FIELD_NAME = R.RDB$FIELD_SOURCE AND R.RDB$RELATION_NAME = 'SCHEDULEARHIV' AND R.RDB$FIELD_NAME = 'PUTH'";
                FbCommand SelectTable = new FbCommand(StrCreate, DBConecto.bdFbSystemConect);
                SelectTable.CommandType = CommandType.Text;
                FbDataReader ReadOutTable = SelectTable.ExecuteReader();
                while (ReadOutTable.Read())
                {
                    LengthField = ReadOutTable[0].ToString();
                }
                ReadOutTable.Close();
                if (Convert.ToInt16(LengthField) < 250)
                {
                    StrCreate = "ALTER TABLE SCHEDULEARHIV ALTER PUTH TYPE VARCHAR(250)";
                    FbCommand AlterTable = new FbCommand(StrCreate, DBConecto.bdFbSystemConect);
                    AlterTable.CommandTimeout = 3;
                    AlterTable.CommandType = CommandType.Text;
                    AlterTable.ExecuteScalar();
                }
                SelectTable.Dispose();
            }
            DBConecto.DBcloseFBConectionMemory("FbSystem");
        }

        public static void CreateListIpRdp()
        {
            DBConecto.DBopenFBConectionMemory(SystemBD.ConnnStringSystem().ToString(), "", "FbSystem");
            string strCmd = "select t.rdb$relation_name from rdb$relations t where t.rdb$relation_name = 'LISTIPRDP'";
            DBConecto.UniQuery CountQuery = new DBConecto.UniQuery(strCmd, "FB");
            CountQuery.ExecuteQueryFillTable("Cursor_LISTIPRDP");
            if (CountQuery.CacheDBAdapter != null && CountQuery.CacheDBAdapter.Tables["Cursor_LISTIPRDP"].Rows.Count <= 0)
            {
                string StrCreate = "CREATE TABLE LISTIPRDP  (" +
                                    "LOGIN  VARCHAR(15) , " +
                                    "PASSWORD  VARCHAR(15), " +
                                    "TCPIP  VARCHAR(25)," +
                                    "NAMECONECT  VARCHAR(30), " +
                                    "PUTH  VARCHAR(250), " +
                                    "DATATIME VARCHAR(15)," +
                                    "OVERALL_IDAPP VARCHAR(5)," +
                        " OVERALL_NAMEAPP VARCHAR(20) ," +
                        " OVERALL_AUTORIZETYPE VARCHAR(3)," +
                        " OVERALL_CAPTIONNAMEPLAY VARCHAR(50) ," +
                        " OVERALL_PUTHFILEIM VARCHAR(250) ," +
                        " OVERALL_INFOWORKSPACE VARCHAR(7) ," +
                        " PANEL_IDAPP VARCHAR(5) ," +
                        " PANEL_LINKPANEL VARCHAR(7)," +
                        " PANEL_TYPELINK VARCHAR(3)," +
                        " PANEL_NUMBERPANEL VARCHAR(3)," +
                        " PANEL_CAPTIONNAMEWORKSPACE VARCHAR(50) ," +
                        " PANEL_APPSTARTMETOD VARCHAR(70) ," +
                        " PANEL_TYPEAPP VARCHAR(30)," +
                        " ONOFF VARCHAR(3)," +
                        " AUTOSTARTTIMESEC VARCHAR(5)," +
                        " PUTHFILE_OTHERAPP VARCHAR(250) )";
                FbCommand CreateTable = new FbCommand(StrCreate, DBConecto.bdFbSystemConect);
                CreateTable.CommandType = CommandType.Text;
                CreateTable.ExecuteScalar();
                CreateTable.Dispose();
            }
            else
            {

                int Idcount = 0;
                string StrCreate = "select RDB$FIELD_NAME from rdb$relation_fields where RDB$RELATION_NAME = 'LISTIPRDP'";
                FbCommand SelectTable = new FbCommand(StrCreate, DBConecto.bdFbSystemConect);
                SelectTable.CommandType = CommandType.Text;
                FbDataReader ReadOutTable = SelectTable.ExecuteReader();
                while (ReadOutTable.Read()) { Idcount = Idcount + 1; }
                ReadOutTable.Close();
                if (Idcount < 22)
                {
                    StrCreate = "ALTER TABLE LISTIPRDP";
                    StrCreate = StrCreate + (Idcount < 21 ? "  ADD AUTOSTARTTIMESEC VARCHAR(5) " : "");
                    StrCreate = StrCreate + (Idcount < 21 ? ", " : "") + (Idcount < 22 ? " ADD PUTHFILE_OTHERAPP VARCHAR(250) " : "");

                    FbCommand AlterTable = new FbCommand(StrCreate, DBConecto.bdFbSystemConect);
                    AlterTable.CommandTimeout = 3;
                    AlterTable.CommandType = CommandType.Text;
                    AlterTable.ExecuteScalar();

                }
                string LengthField = "";
                StrCreate = "SELECT  F.RDB$CHARACTER_LENGTH FROM RDB$FIELDS F, RDB$RELATION_FIELDS R WHERE F.RDB$FIELD_NAME = R.RDB$FIELD_SOURCE AND R.RDB$RELATION_NAME = 'LISTIPRDP' AND R.RDB$FIELD_NAME = 'PUTH'";
                SelectTable = new FbCommand(StrCreate, DBConecto.bdFbSystemConect);
                SelectTable.CommandType = CommandType.Text;
                ReadOutTable = SelectTable.ExecuteReader();
                while (ReadOutTable.Read())
                {
                    LengthField = ReadOutTable[0].ToString();
                }
                ReadOutTable.Close();
                if (Convert.ToInt16(LengthField) < 250)
                {
                    StrCreate = "ALTER TABLE LISTIPRDP ALTER PUTH TYPE VARCHAR(250)";
                    FbCommand AlterTable = new FbCommand(StrCreate, DBConecto.bdFbSystemConect);
                    AlterTable.CommandTimeout = 3;
                    AlterTable.CommandType = CommandType.Text;
                    AlterTable.ExecuteScalar();
                }
                SelectTable.Dispose();

            }
            DBConecto.DBcloseFBConectionMemory("FbSystem");
        }

        // Процедура создания таблицы эталонов производителя
        public static void CreateEtalonDevelop()
        {
            DBConecto.DBopenFBConectionMemory(SystemBD.ConnnStringSystem().ToString(), "", "FbSystem");
            string StrCreate = "";
            string strCmd = "select t.rdb$relation_name from rdb$relations t where t.rdb$relation_name = 'ETALONDEVELOP'";
            DBConecto.UniQuery CountQuery = new DBConecto.UniQuery(strCmd, "FB");
            CountQuery.ExecuteQueryFillTable("Cursor_ETALONDEVELOP");
            if (CountQuery.CacheDBAdapter != null && CountQuery.CacheDBAdapter.Tables["Cursor_ETALONDEVELOP"].Rows.Count <= 0)
            {
                StrCreate = "CREATE TABLE ETALONDEVELOP  (" +
                                    "PUTH  VARCHAR(250)  , " +
                                    "DATECREATE  VARCHAR(14)  );";
                FbCommand CreateTable = new FbCommand(StrCreate, DBConecto.bdFbSystemConect);
                CreateTable.CommandType = CommandType.Text;
                CreateTable.ExecuteScalar();
                CreateTable.Dispose();
            }
            else
            {
                string LengthField = "";
                StrCreate = "SELECT  F.RDB$CHARACTER_LENGTH FROM RDB$FIELDS F, RDB$RELATION_FIELDS R WHERE F.RDB$FIELD_NAME = R.RDB$FIELD_SOURCE AND R.RDB$RELATION_NAME = 'ETALONDEVELOP' AND R.RDB$FIELD_NAME = 'PUTH'";
                FbCommand SelectTable = new FbCommand(StrCreate, DBConecto.bdFbSystemConect);
                SelectTable.CommandType = CommandType.Text;
                FbDataReader ReadOutTable = SelectTable.ExecuteReader();
                while (ReadOutTable.Read())
                {
                    LengthField = ReadOutTable[0].ToString();
                }
                ReadOutTable.Close();
                if (Convert.ToInt16(LengthField) < 250)
                {
                    StrCreate = "ALTER TABLE ETALONDEVELOP ALTER PUTH TYPE VARCHAR(250)";
                    FbCommand AlterTable = new FbCommand(StrCreate, DBConecto.bdFbSystemConect);
                    AlterTable.CommandTimeout = 3;
                    AlterTable.CommandType = CommandType.Text;
                    AlterTable.ExecuteScalar();
                }
                SelectTable.Dispose();
            }
            DBConecto.DBcloseFBConectionMemory("FbSystem");
        }

        // Процедура создания таблицы эталонов дистрибутора
        public static void CreateEtalonDistrybut()
        {
            DBConecto.DBopenFBConectionMemory(SystemBD.ConnnStringSystem().ToString(), "", "FbSystem");
            string StrCreate = "";
            string strCmd = "select t.rdb$relation_name from rdb$relations t where t.rdb$relation_name = 'ETALONDISTRYBUT'";
            DBConecto.UniQuery CountQuery = new DBConecto.UniQuery(strCmd, "FB");
            CountQuery.ExecuteQueryFillTable("Cursor_ETALONDISTRYBUT");
            if (CountQuery.CacheDBAdapter != null && CountQuery.CacheDBAdapter.Tables["Cursor_ETALONDISTRYBUT"].Rows.Count <= 0)
            {
                StrCreate = "CREATE TABLE ETALONDISTRYBUT  (" +
                                    "PUTH  VARCHAR(250)   , " +
                                    "DATECREATE  VARCHAR(14)  );";
                FbCommand CreateTable = new FbCommand(StrCreate, DBConecto.bdFbSystemConect);
                CreateTable.CommandType = CommandType.Text;
                CreateTable.ExecuteScalar();
                CreateTable.Dispose();
            }
            else
            {
                string LengthField = "";
                StrCreate = "SELECT  F.RDB$CHARACTER_LENGTH FROM RDB$FIELDS F, RDB$RELATION_FIELDS R WHERE F.RDB$FIELD_NAME = R.RDB$FIELD_SOURCE AND R.RDB$RELATION_NAME = 'ETALONDISTRYBUT' AND R.RDB$FIELD_NAME = 'PUTH'";
                FbCommand SelectTable = new FbCommand(StrCreate, DBConecto.bdFbSystemConect);
                SelectTable.CommandType = CommandType.Text;
                FbDataReader ReadOutTable = SelectTable.ExecuteReader();
                while (ReadOutTable.Read())
                {
                    LengthField = ReadOutTable[0].ToString();
                }
                ReadOutTable.Close();
                if (Convert.ToInt16(LengthField) < 250)
                {
                    StrCreate = "ALTER TABLE ETALONDISTRYBUT ALTER PUTH TYPE VARCHAR(250)";
                    FbCommand AlterTable = new FbCommand(StrCreate, DBConecto.bdFbSystemConect);
                    AlterTable.CommandTimeout = 3;
                    AlterTable.CommandType = CommandType.Text;
                    AlterTable.ExecuteScalar();
                }
                SelectTable.Dispose();
            }
            DBConecto.DBcloseFBConectionMemory("FbSystem");
        }

        // Процедура создания таблицы эталонов дистрибутора
        public static void CreateProtokolCompare()
        {
            DBConecto.DBopenFBConectionMemory(SystemBD.ConnnStringSystem().ToString(), "", "FbSystem");
            string StrCreate = "";
            string strCmd = "select t.rdb$relation_name from rdb$relations t where t.rdb$relation_name = 'PROTOKOLCOMPARE'";
            DBConecto.UniQuery CountQuery = new DBConecto.UniQuery(strCmd, "FB");
            CountQuery.ExecuteQueryFillTable("Cursor_PROTOKOLCOMPARE");
            if (CountQuery.CacheDBAdapter != null && CountQuery.CacheDBAdapter.Tables["Cursor_PROTOKOLCOMPARE"].Rows.Count <= 0)
            {
                StrCreate = "CREATE TABLE PROTOKOLCOMPARE  (" +
                                   "PROTOKOL  VARCHAR(70)  , " +
                                   "PUTHDEVELOP  VARCHAR(250) , " +
                                   "PUTHDISTRYBUT  VARCHAR(250) , " +
                                   "DATECREATE  VARCHAR(14)  );";
                FbCommand CreateTable = new FbCommand(StrCreate, DBConecto.bdFbSystemConect);
                CreateTable.CommandType = CommandType.Text;
                CreateTable.ExecuteScalar();
                CreateTable.Dispose();
            }
            else
            {
                string LengthField = "";
                StrCreate = "SELECT  F.RDB$CHARACTER_LENGTH FROM RDB$FIELDS F, RDB$RELATION_FIELDS R WHERE F.RDB$FIELD_NAME = R.RDB$FIELD_SOURCE AND R.RDB$RELATION_NAME = 'PROTOKOLCOMPARE' AND R.RDB$FIELD_NAME = 'PUTHDEVELOP'";
                FbCommand SelectTable = new FbCommand(StrCreate, DBConecto.bdFbSystemConect);
                SelectTable.CommandType = CommandType.Text;
                FbDataReader ReadOutTable = SelectTable.ExecuteReader();
                while (ReadOutTable.Read())
                {
                    LengthField = ReadOutTable[0].ToString();
                }
                ReadOutTable.Close();
                if (Convert.ToInt16(LengthField) < 250)
                {
                    StrCreate = "ALTER TABLE PROTOKOLCOMPARE ALTER PUTHDEVELOP TYPE VARCHAR(250), ALTER PUTHDISTRYBUT TYPE VARCHAR(250) ";
                    FbCommand AlterTable = new FbCommand(StrCreate, DBConecto.bdFbSystemConect);
                    AlterTable.CommandTimeout = 3;
                    AlterTable.CommandType = CommandType.Text;
                    AlterTable.ExecuteScalar();
                }
                SelectTable.Dispose();
            }
            DBConecto.DBcloseFBConectionMemory("FbSystem");
        }

        // Процедура создания таблицы эталонов дистрибутора
        public static void CreateRegisterCompare()
        {
            DBConecto.DBopenFBConectionMemory(SystemBD.ConnnStringSystem().ToString(), "", "FbSystem");
            string strCmd = "select t.rdb$relation_name from rdb$relations t where t.rdb$relation_name = 'REGISTERCOMPARE'";
            DBConecto.UniQuery CountQuery = new DBConecto.UniQuery(strCmd, "FB");
            CountQuery.ExecuteQueryFillTable("Cursor_REGISTERCOMPARE");
            if (CountQuery.CacheDBAdapter != null && CountQuery.CacheDBAdapter.Tables["Cursor_REGISTERCOMPARE"].Rows.Count <= 0)
            {
                string StrCreate = "CREATE TABLE REGISTERCOMPARE  (" +
                                   "PUTHDEVELOP  VARCHAR(250)    , " +
                                   "PUTHDISTRYBUT  VARCHAR(250)    , " +
                                   "DATECREATE  VARCHAR(14)  , " +
                                   "NAME_PROCEDURE VARCHAR(70) ," +
                                   "NAME_TABLE VARCHAR(70) ," +
                                   "NAME_TRIGER VARCHAR(70) ," +
                                   "STATUS VARCHAR(10)); ";
                FbCommand CreateTable = new FbCommand(StrCreate, DBConecto.bdFbSystemConect);
                CreateTable.CommandType = CommandType.Text;
                CreateTable.ExecuteScalar();
                CreateTable.Dispose();
            }
            else
            {

                int Idcount = 0;
                string StrCreate = "select RDB$FIELD_NAME from rdb$relation_fields where RDB$RELATION_NAME = 'REGISTERCOMPARE'";
                FbCommand SelectTable = new FbCommand(StrCreate, DBConecto.bdFbSystemConect);
                SelectTable.CommandType = CommandType.Text;
                FbDataReader ReadOutTable = SelectTable.ExecuteReader();
                while (ReadOutTable.Read()) { Idcount = Idcount + 1; }
                ReadOutTable.Close();
                if (Idcount < 7)
                {
                    StrCreate = "ALTER TABLE REGISTERCOMPARE";
                    StrCreate = StrCreate + (Idcount < 7 ? "  ADD STATUS VARCHAR(10) " : "");

                    FbCommand AlterTable = new FbCommand(StrCreate, DBConecto.bdFbSystemConect);
                    AlterTable.CommandType = CommandType.Text;
                    AlterTable.ExecuteScalar();

                }

                string LengthField = "";
                StrCreate = "SELECT  F.RDB$CHARACTER_LENGTH FROM RDB$FIELDS F, RDB$RELATION_FIELDS R WHERE F.RDB$FIELD_NAME = R.RDB$FIELD_SOURCE AND R.RDB$RELATION_NAME = 'REGISTERCOMPARE' AND R.RDB$FIELD_NAME = 'PUTHDEVELOP'";
                SelectTable = new FbCommand(StrCreate, DBConecto.bdFbSystemConect);
                SelectTable.CommandType = CommandType.Text;
                ReadOutTable = SelectTable.ExecuteReader();
                while (ReadOutTable.Read())
                {
                    LengthField = ReadOutTable[0].ToString();
                }
                ReadOutTable.Close();
                if (Convert.ToInt16(LengthField) < 250)
                {
                    StrCreate = "ALTER TABLE REGISTERCOMPARE ALTER PUTHDEVELOP TYPE VARCHAR(250), ALTER PUTHDISTRYBUT TYPE VARCHAR(250) ";
                    FbCommand AlterTable = new FbCommand(StrCreate, DBConecto.bdFbSystemConect);
                    AlterTable.CommandTimeout = 3;
                    AlterTable.CommandType = CommandType.Text;
                    AlterTable.ExecuteScalar();
                }
                SelectTable.Dispose();
                // изменить имя поля
                //StrCreate = "ALTER TABLE REGISTERCOMPARE ALTER COLUMN NAME_TRIGER TO NAME_FUNCTION;";
                //FbCommand AlterFild = new FbCommand(StrCreate, DBConecto.bdFbSystemConect);
                //AlterFild.CommandType = CommandType.Text;
                //AlterFild.ExecuteScalar();


            }
            DBConecto.DBcloseFBConectionMemory("FbSystem");
        }

        public static void CreateListTablLog()
        {
            DBConecto.DBopenFBConectionMemory(SystemBD.ConnnStringSystem().ToString(), "", "FbSystem");
            string strCmd = "select t.rdb$relation_name from rdb$relations t where t.rdb$relation_name = 'LISTTABLLOG'";
            DBConecto.UniQuery CountQuery = new DBConecto.UniQuery(strCmd, "FB");
            CountQuery.ExecuteQueryFillTable("Cursor_LISTTABLLOG");
            if (CountQuery.CacheDBAdapter != null && CountQuery.CacheDBAdapter.Tables["Cursor_LISTTABLLOG"].Rows.Count <= 0)
            {
                string StrCreate = "CREATE TABLE LISTTABLLOG  (" +
                                   "NAMETABLE  VARCHAR(30)    , " +
                                   "DATECOPY VARCHAR(25) ," +
                                   "COLUMNDATE VARCHAR(20)); ";
                FbCommand CreateTable = new FbCommand(StrCreate, DBConecto.bdFbSystemConect);
                CreateTable.CommandType = CommandType.Text;
                CreateTable.ExecuteScalar();
                CreateTable.Dispose();
            }
        }


        #region Построение интерфейса
        /// <summary>
        /// Отрисовка изменений размера экрана
        /// </summary>
        public void ResolutionDisplay()
        {

            var BackValue_RM = this.ResizeMode;
            // Разрешить изменять размер
            if (this.ResizeMode == ResizeMode.NoResize)
            {
                this.ResizeMode = ResizeMode.CanResize;

            }
            // 3. Установка размеров окна для того чтобы игнорировать нажатие клавиши раскрыть на весь экран
            // Рабочего стола ([0] - Top; [1] - Left; [2] - Width; [3] - Height; [4] - Right;

            this.Top = SystemConecto.WorkAreaDisplayDefault[0];
            this.Left = SystemConecto.WorkAreaDisplayDefault[1];
            this.Width = SystemConecto.WorkAreaDisplayDefault[2];
            this.Height = SystemConecto.WorkAreaDisplayDefault[3];
            this.MainGrid.Width = SystemConecto.WorkAreaDisplayDefault[2];
            this.MainGrid.Height = SystemConecto.WorkAreaDisplayDefault[3];
            this.ScrollViewerd.Width = SystemConecto.WorkAreaDisplayDefault[2];
            this.ScrollViewerd.Height = SystemConecto.WorkAreaDisplayDefault[3];



        }
        #endregion

        #region Обработка событий любой клавиатуры
        private void ConectoW_KeyDown(object sender, KeyEventArgs e)
        {
            // Да завершить
            if (e.Key == Key.Return)
            {
                // SystemConecto.EndWorkPC(); // Это работает

            }
            else
            {

                // Нет отказаться
                if (e.Key == Key.Escape)
                {
                    // this.Close();

                }

            }


            // Отладка
            // MessageBox.Show(e.Key.ToString());

        }
        #endregion



        private void Close_F_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            StartFront = 1; StartBack = 1; StartServisBD = 1; StartServer = 1; StartShluz = 1; SetInitPanellicenziyaKey = 1; SetInitPanelServerDB = 1; SetInitPanelSborka = 1;
            SetInitPanelTerminalRdp = 1; SetInitPanelAdministrirovanie = 1;
            var uriSource = new Uri(@"/Conecto®%20WorkSpace;component/Images/knop2_1.png", UriKind.Relative);
            Close_F.Source = new BitmapImage(uriSource);
            Close_AdminPanels(2);
        }

        #region События Click (Клик) функцилнальных клавиш окна
        private void Close_AdminPanels(int TypeClose = 1)
        {

            if (TypeClose == 2)
            {
                // Закрыть авторизацию
                SystemConectoInterfice.UserInterficeClose();
            }

            MainWindow ConectoWorkSpace_InW = AppStart.LinkMainWindow();
            if (ConectoWorkSpace_InW != null)
            {
                System.Windows.Application.Current.Dispatcher.Invoke(new Action(delegate ()
                {

                    // Ссылка на объект и метод
                    ConectoWorkSpace_InW.CloseAdministrator();
                }));
                // Пример - неудачного закрытия окна не исправил глюк с закрытием дочерних окон
                //ConectoWorkSpace_InW.ShowActiveMainAppinOS(); 
            }
            //ConectoWorkSpace_InW.Time_L_hh.Visibility = Visibility.Visible;
            //ConectoWorkSpace_InW.Time_L_mm.Visibility = Visibility.Visible;
            //ConectoWorkSpace_InW.Date.Visibility = Visibility.Visible;
            //ConectoWorkSpace_InW.ConectInternet.Visibility = Visibility.Visible;
            //ConectoWorkSpace_InW.AdminButIm_Old.Visibility = Visibility.Visible;
            ConectoWorkSpace_InW.AdminButIm_.Visibility = Visibility.Visible;
            this.Owner.Focus();
            this.Owner = null;
            this.Close();

        }
        private void Close_F_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            StartFront = 1; StartBack = 1; StartServisBD = 1; StartServer = 1; StartShluz = 1; SetInitPanellicenziyaKey = 1; SetInitPanelServerDB = 1; SetInitPanelAdministrirovanie = 1;
            SetInitPanelTerminalRdp = 1; SetInitPanelSborka = 1; StartConectoServis = 1;
             var uriSource = new Uri(@"/Conecto®%20WorkSpace;component/Images/knop2_2.png", UriKind.Relative);
            Close_F.Source = new BitmapImage(uriSource);
        }
        private void Close_F_MouseLeave(object sender, MouseEventArgs e)
        {
            StartFront = 1; StartBack = 1; StartServisBD = 1; StartServer = 1; StartShluz = 1; SetInitPanellicenziyaKey = 1; SetInitPanelServerDB = 1; StartConectoServis = 1;
            SetInitPanelAdministrirovanie = 1; SetInitPanelTerminalRdp = 1; SetInitPanelSborka = 1;
            var uriSource = new Uri(@"/Conecto®%20WorkSpace;component/Images/knop2_1.png", UriKind.Relative);
            Close_F.Source = new BitmapImage(uriSource);
        }
        #endregion
        #region Клавиша вихода из модуля завершение сессии
        private void ImButExit_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            StartFront = 1; StartBack = 1; StartServisBD = 1; StartServer = 1; StartShluz = 1; SetInitPanellicenziyaKey = 1; SetInitPanelServerDB = 1; StartConectoServis = 1;
            SetInitPanelAdministrirovanie = 1; SetInitPanelTerminalRdp = 1; SetInitPanelSborka = 1;
            MainWindow ConectoWorkSpace_InW = AppStart.LinkMainWindow();
            //Выключить элементы интерфейса свернуть и закрыть

            ConectoWorkSpace_InW.AdminButIm_.Visibility = Visibility.Visible;
            ConectoWorkSpace_InW.ConectInternet.Visibility = Visibility.Visible;
            ConectoWorkSpace_InW.Time_L_hh.Visibility = Visibility.Visible;
            ConectoWorkSpace_InW.Time_L_mm.Visibility = Visibility.Visible;
            ConectoWorkSpace_InW.Date.Visibility = Visibility.Visible;
            // ConectoWorkSpace_InW.AdminButIm_Old.Visibility = Visibility.Visible;
            ConectoWorkSpace_InW.AdminButIm_.Visibility = Visibility.Visible;
            var uriSource = new Uri(@"/Conecto®%20WorkSpace;component/Images/vihod1.png", UriKind.Relative);
            ImButExit.Source = new BitmapImage(uriSource);
            Close_AdminPanels(2);
        }

        private void ImButExit_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            StartFront = 1; StartBack = 1; StartServisBD = 1; StartServer = 1; StartShluz = 1; SetInitPanellicenziyaKey = 1; SetInitPanelServerDB = 1; StartConectoServis = 1;
            SetInitPanelAdministrirovanie = 1; SetInitPanelTerminalRdp = 1; SetInitPanelSborka = 1;
            var uriSource = new Uri(@"/Conecto®%20WorkSpace;component/Images/vihod2.png", UriKind.Relative);
            ImButExit.Source = new BitmapImage(uriSource);
        }

        private void ImButExit_MouseLeave(object sender, MouseEventArgs e)
        {
            StartFront = 1; StartBack = 1; StartServisBD = 1; StartServer = 1; StartShluz = 1; SetInitPanellicenziyaKey = 1; SetInitPanelServerDB = 1; StartConectoServis = 1;
            SetInitPanelAdministrirovanie = 1; SetInitPanelTerminalRdp = 1; SetInitPanelSborka = 1;
            var uriSource = new Uri(@"/Conecto®%20WorkSpace;component/Images/vihod1.png", UriKind.Relative);
            ImButExit.Source = new BitmapImage(uriSource);
        }
        #endregion

        private void Admin_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

        }
        #region Клавиша Администрирование

        private void Admin_MouseLeave(object sender, MouseEventArgs e)
        {


            // var uriSource = new Uri(@"/Conecto®%20WorkSpace;component/Images/admin_1.png", UriKind.Relative);
            // AdminButIm_.Source = new BitmapImage(uriSource);

        }

        private void Admin_MouseMove(object sender, MouseEventArgs e)
        {
            if (SystemConecto.AutorizUser.LoginUserAutoriz == "Autorize_pass-admin-IT" || SystemConecto.AutorizUser.LoginUserAutoriz == "Autorize_Admin-Conecto")
            {
                //var uriSource = new Uri(@"/Conecto®%20WorkSpace;component/Images/admin_2.png", UriKind.Relative);
                //AdminButIm_.Source = new BitmapImage(uriSource);
            }
        }

        #endregion

        #region Шифрование и дешифрование паролей

        /// <summary>
        /// Процедура шифрования паролей БД
        /// </summary>
        /// <param name="src"></param>
        /// <param name="e"></param>
        public static string ToAes256(string src, string Secret)
        {

            //Объявляем объект класса AES
            Aes aes = Aes.Create();
            //Генерируем соль
            aes.GenerateIV();
            //Присваиваем ключ. aeskey - переменная (массив байт), сгенерированная методом GenerateKey() класса AES
            Rfc2898DeriveBytes key = new Rfc2898DeriveBytes(Secret, aes.IV);

            aes.Key = key.GetBytes(aes.KeySize / 8);
            byte[] encrypted;
            ICryptoTransform crypt = aes.CreateEncryptor(aes.Key, aes.IV);
            using (MemoryStream ms = new MemoryStream())
            {
                using (CryptoStream cs = new CryptoStream(ms, crypt, CryptoStreamMode.Write))
                {
                    using (StreamWriter sw = new StreamWriter(cs))
                    {
                        sw.Write(src);
                    }
                }
                //Записываем в переменную encrypted зашиврованный поток байтов
                encrypted = ms.ToArray();
            }
            //Возвращаем поток байт + крепим соль
            return Convert.ToBase64String(encrypted.ToArray());
        }

        /// <summary>
        /// Процедура дешифрования паролей БД
        /// </summary>
        /// <param name="src"></param>
        /// <param name="e"></param>
        public static string FromAes256(string shifr, string Secret)
        {
            //byte[] bytesIv = new byte[16];
            //byte[] mess = new byte[shifr.Length - 16];
            ////Списываем соль
            //for (int i = shifr.Length - 16, j = 0; i < shifr.Length; i++, j++)
            //    bytesIv[j] = shifr[i];
            ////Списываем оставшуюся часть сообщения
            //for (int i = 0; i < shifr.Length - 16; i++)
            //    mess[i] = shifr[i];
            string text = "";
            byte[] bytes = Convert.FromBase64String(shifr);
            using (MemoryStream ms = new MemoryStream(bytes))
            {
                //Объект класса Aes
                Aes aes = Aes.Create();

                //Задаем тот же ключ, что и для шифрования
                Rfc2898DeriveBytes key = new Rfc2898DeriveBytes(Secret, aes.IV);
                aes.Key = key.GetBytes(aes.KeySize / 8);
                //Задаем соль
                aes.IV = ReadByteArray(ms);
                //Строковая переменная для результата

                //byte[] data = Convert.FromBase64String(shifr);// mess;
                ICryptoTransform crypt = aes.CreateDecryptor(aes.Key, aes.IV);

                using (CryptoStream cs = new CryptoStream(ms, crypt, CryptoStreamMode.Read))
                {
                    using (StreamReader sr = new StreamReader(cs))
                    {
                        //Результат записываем в переменную text в вие исходной строки
                        text = sr.ReadToEnd();
                    }
                }
            }
            return text;
        }

        private static byte[] ReadByteArray(Stream s)
        {
            byte[] rawLength = new byte[sizeof(int)];
            if (s.Read(rawLength, 0, rawLength.Length) != rawLength.Length)
            {
                throw new SystemException("Поток не содержит правильно отформатированный байтовый массив");
            }

            byte[] buffer = new byte[BitConverter.ToInt32(rawLength, 0)];
            if (s.Read(buffer, 0, buffer.Length) != buffer.Length)
            {
                throw new SystemException("Не правильно прочитал байтовый массив");
            }

            return buffer;
        }


        #endregion
        /// <summary>
        /// Вызов диспечера задач
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void image12_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (!AppStart.SetFocusWindow("taskmgr"))
            {

                Process procCommand = Process.Start("taskmgr");
                // procCommand.Dispose();
            }

            //    Dim P() As Process = Process.GetProcessesByName("taskmgr")
            //P(0).CloseMainWindow() 'Закрывает как по крестику

        }
        /// <summary>
        /// Консоль cmd c админ правами, оставленна возможность открытия несколько окон
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void image13_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            ProcessStartInfo processInfo = new ProcessStartInfo(@"cmd.exe"); //создаем новый процесс
            processInfo.Verb = "runas"; //в данном случае указываем, что процесс должен быть запущен с правами администратора
            //===! Путь через WorkingDirectory не работает нужно установить в программе путь по умолчанию ...
            //processInfo.WorkingDirectory = @"С:\";
            // processInfo.FileName = @"cmd.exe"; //указываем исполняемый файл (программу) для запуска
            try
            {
                Process.Start(processInfo); //пытаемся запустить процесс
            }
            catch (Win32Exception ex)
            {
                //Ничего не делаем, потому что пользователь, возможно, нажал кнопку "Нет" в ответ на вопрос о запуске программы в окне предупреждения UAC (для Windows 7)
                SystemConecto.ErorDebag(ex.ToString(), 1);
            }
        }
        /// <summary>
        /// Администрирование из панели управления
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void image14_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            ProcessStartInfo processInfo = new ProcessStartInfo(@"control.exe"); //создаем новый процесс
            processInfo.WorkingDirectory = @"%windir%\system32\";  // Альтернатива System.Environment.SystemDirectory
            try
            {
                Process.Start(processInfo); //пытаемся запустить процесс
            }
            catch (Win32Exception ex)
            {
                //Ничего не делаем, потому что пользователь, возможно, нажал кнопку "Нет" в ответ на вопрос о запуске программы в окне предупреждения UAC (для Windows 7)
                SystemConecto.ErorDebag(ex.ToString(), 1);
            }
        }

        private void image16_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            ProcessStartInfo psiOpt = new ProcessStartInfo(@"control.exe", @"admintools");
            psiOpt.CreateNoWindow = true;
            psiOpt.WorkingDirectory = @"%windir%\system32\";  // Альтернатива System.Environment.SystemDirectory
            // запускаем процесс
            Process procCommand = Process.Start(psiOpt);
        }
        private void image17_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            //Explorer /e,/select,c:\
            // команда открывает окно Explorer с двумя панелями, причем содержимое всех накопителей не выводится.
            ProcessStartInfo psiOpt = new ProcessStartInfo(@"Explorer.exe", @"/e,/select,c:\");
            psiOpt.CreateNoWindow = true;
            // psiOpt.WorkingDirectory = @"%windir%\system32\";  // Альтернатива System.Environment.SystemDirectory
            // запускаем процесс
            Process procCommand = Process.Start(psiOpt);
        }

        private void image18_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

            string PatchSR = SystemConectoServers.PutchConecto + @"IBExpert\";
            SystemConecto.DIR_(PatchSR);
            if (!File.Exists(PatchSR + "IBExpert.exe"))
            {
                var TextWindows = "Выполняется установка программы" + Environment.NewLine + "Пожалуйста подождите. ";
                int AutoClose = 1;
                int MesaggeTop = -170;
                int MessageLeft = 600;
                InstallB52.MessageEnd(TextWindows, AutoClose, MesaggeTop, MessageLeft);
                Dictionary<string, string> fbembedList = new Dictionary<string, string>();
                fbembedList.Add(PatchSR + "IBEx.zip", "IBExpert/");
                if (SystemConecto.IsFilesPRG(fbembedList, -1, "- Проверка файлов во время установки сервера " + PatchSR) != "True")
                {
                    MessageBox.Show("Отсутствует инсталяционный  файл установки сервера" + PatchSR + "  Установка прекращена. ");
                    return;
                }
                // Распоковка
                Install.Extract.Unarch_arhive(PatchSR + "IBEx.zip", PatchSR);

            }


            ProcessStartInfo psiOpt = new ProcessStartInfo(PatchSR + "IBExpert.exe");
            psiOpt.CreateNoWindow = true;
            Process procCommand = Process.Start(psiOpt);
        }

        private void AmmyAdmin_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

            string PatchSR = SystemConectoServers.PutchConecto + @"Ammy\";
            SystemConecto.DIR_(PatchSR);
            if (!File.Exists(PatchSR + "AA_v3.exe"))
            {
                var TextWindows = "Выполняется установка программы" + Environment.NewLine + "Пожалуйста подождите. ";
                int AutoClose = 1;
                int MesaggeTop = -170;
                int MessageLeft = 500;
                InstallB52.MessageEnd(TextWindows, AutoClose, MesaggeTop, MessageLeft);
                Dictionary<string, string> fbembedList = new Dictionary<string, string>();
                fbembedList.Add(PatchSR + "AA_v3.exe", "Ammy/");
                if (SystemConecto.IsFilesPRG(fbembedList, -1, "- Проверка файлов во время установки Ammy " + PatchSR) != "True")
                {
                    MessageBox.Show("Отсутствует инсталяционный  файл Ammy" + PatchSR + "  Установка прекращена. ");
                    return;
                }
            }

            ProcessStartInfo psiOpt = new ProcessStartInfo(PatchSR + "AA_v3.exe");
            psiOpt.CreateNoWindow = true;
            Process procCommand = Process.Start(psiOpt);
        }

        private void AnyDesk_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

            string PatchSR = SystemConectoServers.PutchConecto + @"AnyDesk\";
            SystemConecto.DIR_(PatchSR);
            if (!File.Exists(PatchSR + "AnyDesk.exe"))
            {
                var TextWindows = "Выполняется установка программы" + Environment.NewLine + "Пожалуйста подождите. ";
                int AutoClose = 1;
                int MesaggeTop = -170;
                int MessageLeft = 400;
                InstallB52.MessageEnd(TextWindows, AutoClose, MesaggeTop, MessageLeft);
                Dictionary<string, string> fbembedList = new Dictionary<string, string>();
                fbembedList.Add(PatchSR + "AnyDesk.exe", "AnyDesk/");
                if (SystemConecto.IsFilesPRG(fbembedList, -1, "- Проверка файлов во время установки AnyDesk " + PatchSR) != "True")
                {
                    MessageBox.Show("Отсутствует инсталяционный  файл установки AnyDesk" + PatchSR + "  Установка прекращена. ");
                    return;
                }
            }

            ProcessStartInfo psiOpt = new ProcessStartInfo(PatchSR + "AnyDesk.exe");
            psiOpt.CreateNoWindow = true;
            Process procCommand = Process.Start(psiOpt);
        }

        private void WinOptimizer_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

            string PatchSR = SystemConectoServers.PutchConecto + @"Ashampoo.WinOptimizer.17\";
            SystemConecto.DIR_(PatchSR);
            if (!File.Exists(PatchSR + "Ashampoo.WinOptimizer.17.zip"))
            {
                var TextWindows = "Выполняется установка программы" + Environment.NewLine + "Пожалуйста подождите. ";
                int AutoClose = 1;
                int MesaggeTop = -170;
                int MessageLeft = 300;
                InstallB52.MessageEnd(TextWindows, AutoClose, MesaggeTop, MessageLeft);
                Dictionary<string, string> fbembedList = new Dictionary<string, string>();
                fbembedList.Add(PatchSR + "Ashampoo.WinOptimizer.17.zip", "Ashampoo.WinOptimizer.17/");
                if (SystemConecto.IsFilesPRG(fbembedList, -1, "- Проверка файлов во время установки WinOptimizer17Portable " + PatchSR) != "True")
                {
                    MessageBox.Show("Отсутствует инсталяционный  файл установки WinOptimizer17Portable" + PatchSR + "  Установка прекращена. ");
                    return;
                }
                // Распоковка
                Install.Extract.Unarch_arhive(PatchSR + "Ashampoo.WinOptimizer.17.zip", PatchSR);
            }


            ProcessStartInfo psiOpt = new ProcessStartInfo(PatchSR + "AshampooWinOptimizer17Portable.exe");
            psiOpt.CreateNoWindow = true;
            Process procCommand = Process.Start(psiOpt);
        }

        private void TeamViewer_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

            string FileExe = @"c:\TeamViewer\TeamViewerPortable.exe";
            string PatchSR = @"c:\TeamViewer\";
            SystemConecto.DIR_(PatchSR);
            if (!File.Exists(FileExe))
            {
                var TextWindows = "Выполняется установка программы" + Environment.NewLine + "Пожалуйста подождите. ";
                int AutoClose = 1;
                int MesaggeTop = -170;
                int MessageLeft = 300;
                InstallB52.MessageEnd(TextWindows, AutoClose, MesaggeTop, MessageLeft);
                Dictionary<string, string> fbembedList = new Dictionary<string, string>();
                fbembedList.Add(PatchSR + "TeamViewer.zip", "TeamViewerPortable/");
                if (SystemConecto.IsFilesPRG(fbembedList, -1, "- Проверка файлов во время установки TeamViewerPortable " + PatchSR) != "True")
                {
                    MessageBox.Show("Отсутствует инсталяционный  файл установки TeamViewerPortable" + PatchSR + "  Установка прекращена. ");
                    return;
                }
                // Распоковка
                Install.Extract.Unarch_arhive(PatchSR + "TeamViewer.zip", PatchSR);
            }

            if (File.Exists(FileExe))
            {
                ProcessStartInfo psiOpt = new ProcessStartInfo(FileExe);
                psiOpt.CreateNoWindow = true;
                Process procCommand = Process.Start(psiOpt);
            }
        }
        // -----------------------------------------------------------
        // Процедура загрузки закладки администрирование
        private void Administrirovanie_Loaded(object sender, RoutedEventArgs e)
        {
            if (SetInitPanelAdministrirovanie == 1)
            {
                InitTextSystemAdmin();
                TextBoxDirPathBackUp.Text = AppStart.TableReestr["PuthArhivDefault"];
                TextBoxPaswDefaultFb25.Text = AppStart.TableReestr["CurrentPasswABD25"];
                TextBoxPaswDefaultFb30.Text = AppStart.TableReestr["CurrentPasswABD30"];
                if (AppStart.TableReestr["VersiyaBD"] == "") ConectoWorkSpace.Administrator.AdminPanels.UpdateKeyReestr("VersiyaBD", "1.0.1.1.0");
                // Проверка обновлений Б52 при стартовом запуске Conecto.

                AppStart.RenderInfo Argument01 = new AppStart.RenderInfo() { };
                Argument01.argument1 = "1";
                Thread thStart = new Thread(InstallB52.UpGreateB52);
                thStart.SetApartmentState(ApartmentState.STA);
                thStart.IsBackground = true; // Фоновый поток
                thStart.Start(Argument01);
                InstallB52.IntThreadStart++;

                SetInitPanelAdministrirovanie = 0;
            }
        }


        public static void InitTextSystemAdmin()
        {
            AdminPanels.ButtonPanel = new string[3];
            AdminPanels.ButtonPanel[0] = "PuthArhivDefault";
            AdminPanels.ButtonPanel[1] = "UpGreateB52";
            AdminPanels.ButtonPanel[2] = "VersiyaBD";
            //AdminPanels.ButtonPanel[1] = "PaswordRdp";
            //AdminPanels.ButtonPanel[2] = "IpRdpIp4";
            //AdminPanels.ButtonPanel[3] = "NameApp";
            //AdminPanels.ButtonPanel[4] = "OnOff";
            //AdminPanels.ButtonPanel[5] = "LoginAutoInput";
            //AdminPanels.ButtonPanel[6] = "PaswAutoInput";

            if (LoadTableRestr == 0) InitKeySystemFB("InitText");
            InitTextOnOff();
        }


        // -----------------------------------------------------------
        // Процедура загрузки состояния переключателей сервисов On Off
        private void TerminalRdp_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {

            if (SetInitPanelTerminalRdp == 1)
            {
                InitKeyOnOffSystemSuport();
                InitTextSystemSuport();
                RDPOnOff.IsEnabled = false;
                SetInitPanelTerminalRdp = 0;
                InitPanel_SystemSuport();

                string SetKeyValue = "on_off_2.png";
                if ((string)AppStart.rkAppSetingAllUser.GetValue("SerchDateTimeNet") == "0") SetKeyValue = "on_off_1.png";
                var pic = (Image)LogicalTreeHelper.FindLogicalNode(this, "DateTimeNet");
                var uriSource = new Uri(@"/Conecto®%20WorkSpace;component/Images/" + SetKeyValue, UriKind.Relative);
                pic.Source = new BitmapImage(uriSource);


            }

        }
        // Процедура инициации состояния переключателей панели Конфигурация Conecto
        public void InitPanel_SystemSuport()
        {
            ConectoWorkSpace.Administrator.AdminPanels ConectoWorkSpace_Of = AppStart.LinkMainWindow("WAdminPanels");
            //var pict = (Image)LogicalTreeHelper.FindLogicalNode(ConectoWorkSpace_Of, "TerminalOnOff");
            //Uri Filepng = Convert.ToInt32(AppStart.TableReestr["TerminalOnOff"]) == 2 ? new Uri(@"/Conecto®%20WorkSpace;component/Images/on_off_2.png", UriKind.Relative) : new Uri(@"/Conecto®%20WorkSpace;component/Images/on_off_1.png", UriKind.Relative);
            //pict.Source = new BitmapImage(Filepng);
            Interface.CurrentStateInst("TerminalOnOff", Convert.ToInt32(AppStart.TableReestr["TerminalOnOff"]) == 2 ? "2" : "0", Convert.ToInt32(AppStart.TableReestr["TerminalOnOff"]) == 2 ? "on_off_2.png" : "on_off_1.png", TerminalOnOff);

            if (Environment.Is64BitOperatingSystem) AppStart.localMachine = RegistryKey.OpenBaseKey(Microsoft.Win32.RegistryHive.LocalMachine, RegistryView.Registry64);
            // Открыть  системный регистр для модификации ключа Shell
            RegistryKey regKeylocalMachine = AppStart.localMachine.OpenSubKey(@"SOFTWARE\Microsoft\Windows NT\CurrentVersion\Winlogon", true);
            LoginAutoInput.Text = (string)regKeylocalMachine.GetValue("DefaultUserName");
            PaswAutoInput.Text = (string)regKeylocalMachine.GetValue("DefaultPassword");
            var PictRdp = (Image)LogicalTreeHelper.FindLogicalNode(ConectoWorkSpace_Of, "RDPOnOff");
            Uri FilepngRdp = Convert.ToInt32(AppStart.TableReestr["TerminalRdpOnOff"]) == 2 ? new Uri(@"/Conecto®%20WorkSpace;component/Images/on_off_2.png", UriKind.Relative) : new Uri(@"/Conecto®%20WorkSpace;component/Images/on_off_1.png", UriKind.Relative);
            PictRdp.Source = new BitmapImage(FilepngRdp);
            var PictLoad = (Image)LogicalTreeHelper.FindLogicalNode(ConectoWorkSpace_Of, "AvtoloadOSOnOff");
            Uri FilepngLoad = Convert.ToInt32(AppStart.TableReestr["AvtoloadOSOnOff"]) == 2 ? new Uri(@"/Conecto®%20WorkSpace;component/Images/on_off_2.png", UriKind.Relative) : new Uri(@"/Conecto®%20WorkSpace;component/Images/on_off_1.png", UriKind.Relative);
            PictLoad.Source = new BitmapImage(FilepngLoad);

            var PictServis = (Image)LogicalTreeHelper.FindLogicalNode(ConectoWorkSpace_Of, "PerekluchServisOnOff");
            Uri FilepngServis = Convert.ToInt32(AppStart.TableReestr["ServisOnOff"]) == 2 ? new Uri(@"/Conecto®%20WorkSpace;component/Images/on_off_2.png", UriKind.Relative) : new Uri(@"/Conecto®%20WorkSpace;component/Images/on_off_1.png", UriKind.Relative);
            PictServis.Source = new BitmapImage(FilepngServis);

        }
        // -----------------------------------------------------------
        // Процедура загрузки состояния переключателей сервисов On Off
        private void LoadKeyOnOff_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {

            if (SetInitPanelSystemSuport == 1)
            {
                InitKeyOnOffSystemSuport();
                InitTextSystemSuport();
                InitIpRdpDefault();
                //LoadedAppPlay();
                RDPOnOff.IsEnabled = false;
                SetInitPanelSystemSuport = 0;

                ServerRdpChng.IsEnabled = false;
            }
            for (int i = 1; i <= 32; i++) { AppPanelNumber.Items.Add(Convert.ToString(i)); }
        }

        public static void InitKeyOnOffSystemSuport()
        {
            AdminPanels.ButtonPanel = new string[14];
            AdminPanels.ButtonPanel[0] = "TerminalOnOff";
            AdminPanels.ButtonPanel[1] = "TerminalRdpOnOff";
            AdminPanels.ButtonPanel[2] = "TerminalUserOnOff";
            AdminPanels.ButtonPanel[3] = "KassaUserOnOff";
            AdminPanels.ButtonPanel[4] = "TerminalClientOnOff";
            AdminPanels.ButtonPanel[5] = "SysAdminOnOff";
            AdminPanels.ButtonPanel[6] = "TerminalSubUserOnOff";
            AdminPanels.ButtonPanel[7] = "KassaSubUserOnOff";
            AdminPanels.ButtonPanel[8] = "ServisUserOnOff";
            AdminPanels.ButtonPanel[9] = "ServisAdminOnOff";
            AdminPanels.ButtonPanel[10] = "ServisOnOff";
            AdminPanels.ButtonPanel[11] = "PanelVisiblOnOff";
            AdminPanels.ButtonPanel[12] = "AvtoloadOSOnOff";
            AdminPanels.ButtonPanel[13] = "CopyBdUser";
            if (LoadTableRestr == 0) InitKeySystemFB("InitKey");
            InitKeyOnOff();
        }
        public static void InitTextSystemSuport()
        {
            AdminPanels.ButtonPanel = new string[7];
            AdminPanels.ButtonPanel[0] = "LoginRdp";
            AdminPanels.ButtonPanel[1] = "PaswordRdp";
            AdminPanels.ButtonPanel[2] = "IpRdpIp4";
            AdminPanels.ButtonPanel[3] = "NameApp";
            AdminPanels.ButtonPanel[4] = "OnOff";
            AdminPanels.ButtonPanel[5] = "LoginAutoInput";
            AdminPanels.ButtonPanel[6] = "PaswAutoInput";

            if (LoadTableRestr == 0) InitKeySystemFB("InitText");
            InitTextOnOff();
        }




        public void Ip4Rdp(string Ip4)
        {
            for (int indPoint = 1; indPoint <= 3; indPoint++)
            {
                int position = Ip4.IndexOf(".");
                if (position <= 0) { break; }
                switch (indPoint)
                {
                    case 1:
                        IpRdpIp4Text1.Text = Ip4.Substring(0, position);
                        break;
                    case 2:
                        IpRdpIp4Text2.Text = Ip4.Substring(0, position);
                        break;
                    case 3:
                        IpRdpIp4Text3.Text = Ip4.Substring(0, position);
                        break;
                }
                Ip4 = Ip4.Substring(position + 1);
            }
            IpRdpIp4Text4.Text = Ip4.Substring(0);

        }


        private void AppPanelNumberChanged(object sender, SelectionChangedEventArgs e)
        {
            ValuePanel = Convert.ToString(Convert.ToInt32(AppPanelNumber.SelectedIndex.ToString()) + 1);
        }


        // Процедура записи введенного логина
        private void LoginAutoTerminal_KeyUp(object sender, KeyEventArgs e)
        {
            UpdateKeyReestr("LoginAutoInput", LoginAutoInput.Text);
        }
        // Процедура записи введенного пароля
        private void PaswordAutoTerminal_KeyUp(object sender, KeyEventArgs e)
        {
            UpdateKeyReestr("PaswAutoInput", PaswAutoInput.Text);
        }

        public static void LoadedGridIpRdp(string CurrentSetBD = "SELECT * from LISTIPRDP")
        {

            //int CountTableXml = TableXml.Count();
            List<GridIpRdp> result = new List<GridIpRdp>(1);
            DBConecto.DBopenFBConectionMemory(SystemBD.ConnnStringSystem().ToString(), "", "FbSystem");
            FbCommand UpdateKey = new FbCommand(CurrentSetBD, DBConecto.bdFbSystemConect);
            UpdateKey.CommandType = CommandType.Text;
            FbDataReader reader = UpdateKey.ExecuteReader();
            while (reader.Read())
            {
                result.Add(new GridIpRdp(reader[0].ToString(), reader[1].ToString(), reader[2].ToString(), reader[3].ToString(),
                       reader[4].ToString(), reader[5].ToString(), reader[6].ToString(), reader[7].ToString(), reader[8].ToString(),
                       reader[9].ToString(), reader[10].ToString(), reader[11].ToString(), reader[12].ToString(), reader[13].ToString(),
                       reader[14].ToString(), reader[15].ToString(), reader[16].ToString(), reader[17].ToString(), reader[18].ToString(),
                       reader[19].ToString(), reader[20].ToString(), reader[21].ToString()));

            }
            reader.Close();
            UpdateKey.Dispose();
            DBConecto.DBcloseFBConectionMemory("FbSystem");
            TableXml = result;


            //List<string> ListMass = mass.ToList(); // перевод массива в список
            //    string[] Arr = ListMass.ToArray(); // перевод списка в массив
            //    int lenght = ListMass.Count(); // длина списка

        }

        // Процедура анализа состояния таблицы LISTIPRDP. Данные есть или нет.
        // Если нет то заполнеие таблицы из AppPlay.xml

        // Процедура загрузки сервера по умолчанию
        public void InitIpRdpDefault()
        {

            string StrCreate = "";
            int Idcount = 0;
            DBConecto.DBopenFBConectionMemory(SystemBD.ConnnStringSystem().ToString(), "", "FbSystem");
            string InsertExecute = "SELECT count(*) from LISTIPRDP";
            DBConecto.UniQuery CountQuery = new DBConecto.UniQuery(InsertExecute, "FB");
            string CountTable = CountQuery.ExecuteUNIScalar() == null ? "" : CountQuery.ExecuteUNIScalar().ToString();
            NumberLoadAppEvents = Convert.ToInt32(CountTable);

            //if (Idcount == 0)
            //{
            //    UpdateKeyReestr("IpRdpIp4", "195.3.206.110");
            //    UpdateKeyReestr("LoginRdp", "Administrator");
            //    UpdateKeyReestr("PaswordRdp", "2Us8MSvWhQVEY");
            //    StrCreate = "INSERT INTO LISTIPRDP  values ('" + AppStart.TableReestr["LoginRdp"] + "', '" + AppStart.TableReestr["PaswordRdp"] +
            //        "', '" + AppStart.TableReestr["IpRdpIp4"] + "', 'Demo server', '"
            //        + SystemConectoServers.PutchServer + "', '" + DateTime.Now.ToString("yyyyMMddHHmmss") +
            //        "','001','RDPCONECT','-1', 'Demo сервер','/Conecto®%20WorkSpace;component/Images/ico_terminalabon1.png','false'," +
            //        "'001','true','1','2','Demo сервер','ConectoWorkSpace.AppforWorkSpace.LoadAppEvents_001','Integer','On')";
            //    DBConecto.UniQuery InsertQuery = new DBConecto.UniQuery(StrCreate, "FB");
            //    InsertQuery.UserQuery = string.Format(StrCreate, "LISTIPRDP");
            //    InsertQuery.ExecuteUNIScalar();

            //}

            if (Convert.ToUInt32(CountTable) <= 1)
            {
                XmlTextReader reader = null;
                string nameElement = ""; idApp = "0"; Panel_idApp = "0";
                // Чтение обычного файла
                reader = new XmlTextReader(new StringReader(Properties.Resources.appplay));
                reader.WhitespaceHandling = WhitespaceHandling.None; // пропускаем пустые узлы

                while (reader.Read())
                {
                    if (reader.NodeType == XmlNodeType.Element || reader.NodeType == XmlNodeType.EndElement) nameElement = reader.Name;
                    string[] a_nameElement = nameElement.Split('_');
                    switch (a_nameElement[0])
                    {
                        case "":
                            break;
                        case "Параметры-AppPlay":


                            if (Convert.ToInt32(idApp) != 0 && IndRecno == 2)
                            {
                                string Login = NameApp.Trim().Substring(0, (NameApp.Trim().Length <= 15 ? NameApp.Trim().Length : 15));
                                StrCreate = "INSERT INTO LISTIPRDP  values ('" + Login + "', '', '127.0.0.1','" + NameApp.Trim() + "', '', '" + DateTime.Now.ToString("yyyyMMddHHmmss") +
                                     "', '" + idApp.Trim() + "', '" + NameApp.Trim() + "', '" + AutorizeType.Trim() + "', '" + CaptionNamePlay.Trim() + "', '" +
                                     PuthFileIm + "', '" + infoWorkSpace.Trim() + "', '" + Panel_idApp.Trim() + "', '" + LinkPanel.Trim() + "', '" + TypeLink.Trim() +
                                      "', '" + NumberPanel.Trim() + "', '" + CaptionNameWorkSpace.Trim() + "', '" + AppStartMetod.Trim() + "', '" +
                                     TypeApp.Trim() + "', '" + OnOff + "', '" + AutostartTimeSec.Trim() + "', '')";
                                DBConecto.UniQuery AddQuery = new DBConecto.UniQuery(StrCreate, "FB");
                                AddQuery.ExecuteUNIScalar();
                                Idcount++;
                                NumberLoadAppEvents++;

                            }
                            break;
                        case "FileConfig":
                            break;
                        case "idAppOverall":

                            if (Convert.ToInt32(idApp) == 0) idApp = a_nameElement[1];
                            if (Convert.ToInt32(idApp) != Convert.ToInt32(a_nameElement[1]) && IndRecno == 2)
                            {
                                string Login = NameApp.Trim().Substring(0, (NameApp.Trim().Length <= 15 ? NameApp.Trim().Length : 15));
                                StrCreate = "INSERT INTO LISTIPRDP  values ('" + Login + "', '', '127.0.0.1','" + NameApp.Trim() + "', '', '" + DateTime.Now.ToString("yyyyMMddHHmmss") +
                                     "', '" + idApp.Trim() + "', '" + NameApp.Trim() + "', '" + AutorizeType.Trim() + "', '" + CaptionNamePlay.Trim() + "', '" +
                                     PuthFileIm + "', '" + infoWorkSpace.Trim() + "', '" + Panel_idApp.Trim() + "', '" + LinkPanel.Trim() + "', '" + TypeLink.Trim() +
                                      "', '" + NumberPanel.Trim() + "', '" + CaptionNameWorkSpace.Trim() + "', '" + AppStartMetod.Trim() + "', '" +
                                     TypeApp.Trim() + "', '" + OnOff + "', '" + AutostartTimeSec.Trim() + "', '')";
                                DBConecto.UniQuery AddQuery = new DBConecto.UniQuery(StrCreate, "FB");
                                AddQuery.ExecuteUNIScalar();
                                Idcount++;
                                NumberLoadAppEvents++;
                                idApp = a_nameElement[1];
                                Panel_idApp = a_nameElement[1];
                                IndRecno = 0;
                            }

                            // Запись праметров
                            NameApp = reader.GetAttribute("NameApp") ?? "";
                            CaptionNamePlay = reader.GetAttribute("CaptionNamePlay") ?? "";
                            PuthFileIm = reader.GetAttribute("PuthFileIm") ?? "";
                            infoWorkSpace = reader.GetAttribute("infoWorkSpace") ?? "";
                            AutorizeType = reader.GetAttribute("AutorizeType") ?? "";
                            IndRecno++;
                            break;

                        case "idAppPanelWorkSpace":

                            if (Convert.ToInt32(Panel_idApp) == 0) Panel_idApp = a_nameElement[1];

                            // Запись праметров
                            LinkPanel = reader.GetAttribute("LinkPanel") ?? "";
                            TypeLink = reader.GetAttribute("TypeLink") ?? "";
                            NumberPanel = reader.GetAttribute("NumberPanel");
                            CaptionNameWorkSpace = reader.GetAttribute("CaptionNameWorkSpace") ?? "";
                            AppStartMetod = reader.GetAttribute("AppStartMetod") ?? "";
                            TypeApp = reader.GetAttribute("TypeApp") ?? "";
                            AutostartTimeSec = reader.GetAttribute("AutostartTimeSec") ?? "";
                            IndRecno++;
                            break;
                    }
                }

            }
            Idcount = 0;
            InsertExecute = "SELECT count(*) from LISTIPRDP";
            CountQuery = new DBConecto.UniQuery(InsertExecute, "FB");
            CountTable = CountQuery.ExecuteUNIScalar() == null ? "" : CountQuery.ExecuteUNIScalar().ToString();
            if (Convert.ToInt32(CountTable) != 0)
            {
                CountListRdp = Convert.ToInt32(CountTable) - 1;
                DeskNumberPanel = new String[Convert.ToInt32(CountTable)];
                StrCreate = "SELECT * from LISTIPRDP ";
                FbCommand SelectTable = new FbCommand(StrCreate, DBConecto.bdFbSystemConect);
                FbDataReader ReadOutTable = SelectTable.ExecuteReader();
                while (ReadOutTable.Read())
                {

                    AppStart.TableReestr["LoginRdp"] = ReadOutTable[0].ToString();
                    AppStart.TableReestr["PaswordRdp"] = ReadOutTable[1].ToString();
                    AppStart.TableReestr["IpRdpIp4"] = ReadOutTable[2].ToString();
                    AppStart.TableReestr["NameApp"] = ReadOutTable[3].ToString();
                    NumberIdApp = ReadOutTable[6].ToString();
                    DeskNumberPanel[Idcount] = ReadOutTable[15].ToString();
                    Idcount++;
                }
                ReadOutTable.Close();
                SelectTable.Dispose();
            }

            DBConecto.DBcloseFBConectionMemory("FbSystem");
            if (Idcount != 0)
            {
                UpdateKeyReestr("LoginRdp", AppStart.TableReestr["LoginRdp"]);
                UpdateKeyReestr("PaswordRdp", AppStart.TableReestr["PaswordRdp"]);
                UpdateKeyReestr("IpRdpIp4", AppStart.TableReestr["IpRdpIp4"]);
            }


        }

        // Процедура загрузки из БД параметров конфигурации активных приложений.
        // формирование строки  формата xml при считывании из БД и для дальнейшего преобразования в ReadConfigXMLAppPlay c параметром 2 для формирования панели включенных ярлыков
        // при загрузке Conecto. 
        // Читаем из БД из таблицы AppPlay все поля по всем ярлыкам папраметры описанные в AppPlay.xml. Преобразуем в строку затем эту строку передаем 
        // в процедуру ReadConfigXMLAppPlay(2) в ней вставить код обработки строки с параметром 2. 
        public void LoadedAppPlay()
        {
            LoadedGridIpRdp();
            int CountTableXml = TableXml.Count();
            if (CountTableXml == 0) return;
            using (StringWriter sw = new StringWriter())
            {

                // Начало документа                             
                WriterConfigXML = new XmlTextWriter(sw);
                WriterConfigXML.Formatting = Formatting.Indented; // упорядочивания структуры файла
                WriterConfigXML.WriteStartDocument();
                WriterConfigXML.WriteStartElement("Параметры-AppPlay");
                WriterConfigXML.WriteStartElement("FileConfig");
                WriterConfigXML.WriteAttributeString(null, "FileConfig_Ver-File-Config", null, "0.1");
                WriterConfigXML.WriteEndElement();
                // Тело документа
                for (int i = 0; i <= CountTableXml - 1; i++)
                {
                    WriterConfigXML.WriteStartElement("idAppOverall_" + TableXml[i].OverAll_idApp);
                    WriterConfigXML.WriteAttributeString(null, "idApp", null, TableXml[i].OverAll_idApp);
                    WriterConfigXML.WriteAttributeString(null, "NameApp", null, TableXml[i].OverAll_NameApp);
                    if (TableXml[i].OverAll_AutorizeType != "") WriterConfigXML.WriteAttributeString(null, "AutorizeType", null, TableXml[i].OverAll_AutorizeType);
                    WriterConfigXML.WriteAttributeString(null, "CaptionNamePlay", null, TableXml[i].OverAll_CaptionNamePlay);
                    WriterConfigXML.WriteAttributeString(null, "PuthFileIm", null, TableXml[i].OverAll_PuthFileIm);
                    WriterConfigXML.WriteAttributeString(null, "infoWorkSpace", null, TableXml[i].OverAll_infoWorkSpace);
                    WriterConfigXML.WriteEndElement();
                    WriterConfigXML.WriteStartElement("idAppPanelWorkSpace_" + TableXml[i].Panel_idApp);
                    WriterConfigXML.WriteAttributeString(null, "idApp", null, TableXml[i].Panel_idApp);
                    WriterConfigXML.WriteAttributeString(null, "LinkPanel", null, TableXml[i].Panel_LinkPanel);
                    WriterConfigXML.WriteAttributeString(null, "TypeLink", null, TableXml[i].Panel_TypeLink);
                    WriterConfigXML.WriteAttributeString(null, "NumberPanel", null, TableXml[i].Panel_NumberPanel);
                    WriterConfigXML.WriteAttributeString(null, "CaptionNameWorkSpace", null, TableXml[i].Panel_CaptionNameWorkSpace);
                    WriterConfigXML.WriteAttributeString(null, "AppStartMetod", null, TableXml[i].Panel_AppStartMetod);
                    if (TableXml[i].Panel_TypeApp != "") WriterConfigXML.WriteAttributeString(null, "TypeApp", null, TableXml[i].Panel_TypeApp);
                    WriterConfigXML.WriteAttributeString(null, "AutostartTimeSec", null, TableXml[i].Autostart);
                    WriterConfigXML.WriteEndElement();
                }

                //-------------------- конец документа
                WriterConfigXML.WriteEndElement();
                WriterConfigXML.WriteEndDocument();
                AppStart.xmlString = sw.ToString();
                //File.WriteAllText(SystemConectoServers.PutchServer + @"tmp\" + "TableConect.xml", AppStart.xmlString, Encoding.Unicode);
            }
            if (!ConectoWorkSpace.AppStart.SysConf.ReadConfigXMLAppPlay(2)) return;

        }



        public void SelectApp(string IdApp)
        {
            ServerRdpChng.IsEnabled = true;
            PanelVisiblOnOff.IsEnabled = true;
            int Idcount = 0;
            string StrCreate = "SELECT * FROM LISTIPRDP WHERE LISTIPRDP.OVERALL_IDAPP = '" + IdApp + "'";
            DBConecto.DBopenFBConectionMemory(SystemBD.ConnnStringSystem().ToString(), "", "FbSystem");
            FbCommand SelectTable = new FbCommand(StrCreate, DBConecto.bdFbSystemConect);
            FbDataReader ReadOutTable = SelectTable.ExecuteReader();
            while (ReadOutTable.Read())
            {
                LoginRdp.Text = ReadOutTable[0].ToString();
                PaswordRdp.Text = ReadOutTable[1].ToString();
                TcpIpIprdp = ReadOutTable[2].ToString();
                Ip4Rdp(TcpIpIprdp);
                ConectRdp.Text = ReadOutTable[3].ToString();
                PuthFile.Text = ReadOutTable[4].ToString();
                PuthFile_OtherApp.Text = ReadOutTable[21].ToString();
                DatTimIprdp = ReadOutTable[5].ToString();

                OverAllidAppRdp.Text = ReadOutTable[6].ToString();
                OverAllNameApp.Text = ReadOutTable[7].ToString();
                OverAllAutorizeType.Text = ReadOutTable[8].ToString();
                OverAllCaptionNamePlay.Text = ReadOutTable[9].ToString();
                OverAllPuthFileIm.Text = ReadOutTable[10].ToString();
                OverAllinfoWorkSpace.Text = ReadOutTable[11].ToString();
                PanelLinkPanel.Text = ReadOutTable[13].ToString();
                PanelTypeLink.Text = ReadOutTable[14].ToString();
                PanelNumberPanel.Text = ReadOutTable[15].ToString();
                PanelCaptionNameWorkSpace.Text = ReadOutTable[16].ToString();
                PanelAppStartMetod.Text = ReadOutTable[17].ToString();
                PanelTypeApp.Text = ReadOutTable[18].ToString();
                OnOff = ReadOutTable[19].ToString();
                Autostart.Text = ReadOutTable[20].ToString();
                Idcount++;
            }
            ReadOutTable.Close();
            SelectTable.Dispose();
            DBConecto.DBcloseFBConectionMemory("FbSystem");
            if (Idcount == 0) { SetNullIpRdp(); }

            OnOff = PanelLinkPanel.Text == "true" ? "On" : "Off";
            Interface.CurrentStateInst("PerekluchInfo", "0", "on_off_1.png", PerekluchInfo);
            if (OverAllinfoWorkSpace.Text == "true") Interface.CurrentStateInst("PerekluchInfo", "2", "on_off_2.png", PerekluchInfo);
            Interface.CurrentStateInst("PerekluchLinkPanel", "0", "on_off_1.png", PerekluchLinkPanel);
            if (PanelLinkPanel.Text == "true") Interface.CurrentStateInst("PerekluchLinkPanel", "2", "on_off_2.png", PerekluchLinkPanel);
            Interface.CurrentStateInst("PanelVisiblOnOff", "0", "on_off_1.png", PanelVisiblOnOff);
            if (PanelLinkPanel.Text == "true") Interface.CurrentStateInst("PanelVisiblOnOff", "2", "on_off_2.png", PanelVisiblOnOff);
        }

        private void IpRdp_DropDownClosed(object sender, EventArgs e)
        {

        }

        public void SetNullIpRdp()
        {
            LoginRdp.Text = "";
            PaswordRdp.Text = "";
            TcpIpIprdp = "";
            Ip4Rdp(TcpIpIprdp);
            ConectRdp.Text = "";
            PuthFile.Text = "";
            DatTimIprdp = "";

            OverAllidAppRdp.Text = "";
            OverAllNameApp.Text = "";
            OverAllAutorizeType.Text = "";
            OverAllCaptionNamePlay.Text = "";
            OverAllPuthFileIm.Text = "";
            OverAllinfoWorkSpace.Text = "";
            PanelLinkPanel.Text = "";
            PanelTypeLink.Text = "";
            PanelNumberPanel.Text = "";
            PanelCaptionNameWorkSpace.Text = "";
            PanelAppStartMetod.Text = "";
            PanelTypeApp.Text = "";
            OnOff = "Off";
            Autostart.Text = "0";
        }

        // Процедура корретировки соединения       
        private void ChngIpRdp_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (SetConf == "ChngIpRdp")
            {
                ServerRdpChng.Background = Brushes.SkyBlue;
                ServerRdpChng.Foreground = Brushes.Black;
                RdpIsEnableFalse();
                SetConf = "";
                ServerRdpChng.IsEnabled = ServerRdpSave.IsEnabled = false;
                return;
            }
            ServerRdpChng.Background = Brushes.Indigo;
            ServerRdpChng.Foreground = Brushes.White;
            SetConf = "ChngIpRdp";
            RdpIsEnableTrue();

        }

        // Процедура создания нового  соединения
        private void AddIpRdp_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (SetConf == "AddIpRdp")
            {
                SetNullIpRdp();
                ServerRdpAdd.Background = Brushes.SkyBlue;
                ServerRdpAdd.Foreground = Brushes.Black;
                RdpIsEnableFalse();
                IpRdpIp4Text1.Text = "";
                IpRdpIp4Text2.Text = "";
                IpRdpIp4Text3.Text = "";
                IpRdpIp4Text4.Text = "";
                ClickIcon = "";
                SetConf = "";
                ServerRdpChng.IsEnabled = ServerRdpSave.IsEnabled = false;
                return;
            }
            ServerRdpAdd.Background = Brushes.Indigo;
            ServerRdpAdd.Foreground = Brushes.White;
            SetConf = "AddIpRdp";
            RdpIsEnableTrue();
            IpRdpLoaded();
        }

        public void IpRdpLoaded()
        {
            if (NumberIdApp == "") InitIpRdpDefault();
            LoginRdp.Text = "";
            PaswordRdp.Text = "";
            TcpIpIprdp = "";
            Ip4Rdp("127.0.0.1");
            ConectRdp.Text = "";
            PuthFile.Text = "";
            PuthFile_OtherApp.Text = "";
            DatTimIprdp = DateTime.Now.ToString("yyyyMMddHHmmss");

            OverAllidAppRdp.Text = Convert.ToString(Convert.ToInt32(NumberIdApp) + 1);
            OverAllNameApp.Text = "";
            OverAllAutorizeType.Text = "-1";
            OverAllCaptionNamePlay.Text = "";
            OverAllPuthFileIm.Text = ClickIcon;
            OverAllinfoWorkSpace.Text = "false";
            PanelLinkPanel.Text = "true";
            PanelTypeLink.Text = "1";
            Autostart.Text = "0";
            int NumbNewPanel = Convert.ToInt32(DeskNumberPanel[CountListRdp]) + 1;
            for (int i = 0; i <= CountListRdp; i++)
            {
                if (NumbNewPanel < Convert.ToInt32(DeskNumberPanel[i])) NumbNewPanel = Convert.ToInt32(DeskNumberPanel[i]) + 1;

            }
            PanelNumberPanel.Text = Convert.ToString(NumbNewPanel);

            PanelCaptionNameWorkSpace.Text = "";
            PanelAppStartMetod.Text = "ConectoWorkSpace.AppforWorkSpace.LoadAppEvents_" + Convert.ToString(Convert.ToInt32(NumberIdApp) + 1);
            PanelTypeApp.Text = "Integer";
            OnOff = "On";
        }

        public void RdpIsEnableTrue()
        {
            IpRdpIp4Text1.IsEnabled = true;
            IpRdpIp4Text2.IsEnabled = true;
            IpRdpIp4Text3.IsEnabled = true;
            IpRdpIp4Text4.IsEnabled = true;
            PaswordRdp.IsEnabled = true;
            LoginRdp.IsEnabled = true;
            ConectRdp.IsEnabled = true;
            DirPath_App.IsEnabled = true;
            DirPath_OtherApp.IsEnabled = true;
            OverAllNameApp.IsEnabled = true;
            OverAllAutorizeType.IsEnabled = true;
            OverAllCaptionNamePlay.IsEnabled = true;
            OverAllPuthFileIm.IsEnabled = true;
            PanelTypeLink.IsEnabled = true;
            //PanelNumberPanel.IsEnabled = true;
            PanelCaptionNameWorkSpace.IsEnabled = true;
            PanelAppStartMetod.IsEnabled = true;
            PanelTypeApp.IsEnabled = true;
            ServerRdpSave.IsEnabled = true;
            PerekluchLinkPanel.IsEnabled = true;
            PerekluchInfo.IsEnabled = true;
            AppPanelNumber.IsEnabled = true;
            Autostart.IsEnabled = true;
        }

        public void RdpIsEnableFalse()
        {
            IpRdpIp4Text1.IsEnabled = false;
            IpRdpIp4Text2.IsEnabled = false;
            IpRdpIp4Text3.IsEnabled = false;
            IpRdpIp4Text4.IsEnabled = false;
            PaswordRdp.IsEnabled = false;
            LoginRdp.IsEnabled = false;
            ConectRdp.IsEnabled = false;
            OverAllidAppRdp.IsEnabled = false;
            OverAllNameApp.IsEnabled = false;
            OverAllAutorizeType.IsEnabled = false;
            OverAllCaptionNamePlay.IsEnabled = false;
            OverAllPuthFileIm.IsEnabled = false;
            DirPath_App.IsEnabled = false;
            DirPath_OtherApp.IsEnabled = false;
            PanelTypeLink.IsEnabled = false;
            PanelNumberPanel.IsEnabled = false;
            PanelCaptionNameWorkSpace.IsEnabled = false;
            PanelAppStartMetod.IsEnabled = false;
            PanelTypeApp.IsEnabled = false;
            ServerRdpSave.IsEnabled = false;
            PerekluchLinkPanel.IsEnabled = false;
            PerekluchInfo.IsEnabled = false;
            AppPanelNumber.IsEnabled = false;
            Autostart.IsEnabled = false;

        }

        private void SaveIpRdp_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            SaveIpRdp();
        }
        public void SaveIpRdp()
        {
            string StrCreate = "";
            string IdAppCyange = "", IdAppPanel = "";
            string NumbNewPanel = PanelNumberPanel.Text;
            if (ValuePanel != "")
            {
                DBConecto.DBopenFBConectionMemory(SystemBD.ConnnStringSystem().ToString(), "", "FbSystem");
                string StrUpdate = "SELECT * FROM LISTIPRDP WHERE LISTIPRDP.PANEL_NUMBERPANEL = '" + ValuePanel + "'";
                FbCommand SelectTable = new FbCommand(StrUpdate, DBConecto.bdFbSystemConect);
                FbDataReader ReadOutTable = SelectTable.ExecuteReader();
                while (ReadOutTable.Read()) { IdAppCyange = ReadOutTable[12].ToString(); }
                ReadOutTable.Close();
                SelectTable.Dispose();
                StrUpdate = "SELECT * FROM LISTIPRDP WHERE LISTIPRDP.PANEL_NUMBERPANEL = '" + PanelNumberPanel.Text + "'";
                FbCommand TablePanel = new FbCommand(StrUpdate, DBConecto.bdFbSystemConect);
                FbDataReader ReadPanel = TablePanel.ExecuteReader();
                while (ReadPanel.Read()) { IdAppPanel = ReadPanel[12].ToString(); }
                ReadPanel.Close();
                SelectTable.Dispose();
                StrUpdate = "UPDATE LISTIPRDP SET PANEL_NUMBERPANEL = " + "'" + PanelNumberPanel.Text + "'" + " WHERE LISTIPRDP.PANEL_IDAPP = " + "'" + IdAppCyange + "'";
                DBConecto.UniQuery CountQuery = new DBConecto.UniQuery(StrUpdate, "FB");
                CountQuery.ExecuteUNIScalar();
                StrUpdate = "UPDATE LISTIPRDP SET PANEL_NUMBERPANEL = " + "'" + ValuePanel + "'" + " WHERE LISTIPRDP.PANEL_IDAPP = " + "'" + IdAppPanel + "'";
                DBConecto.UniQuery UpdatePanel = new DBConecto.UniQuery(StrUpdate, "FB");
                UpdatePanel.ExecuteUNIScalar();
                DBConecto.DBcloseFBConectionMemory("FbSystem");
                int indexPanel = 0, indexText = 0;
                for (int i = 0; i <= CountListRdp - 1; i++)
                {
                    if (Convert.ToInt32(ValuePanel) == Convert.ToInt32(DeskNumberPanel[i])) indexPanel = i;
                    if (PanelNumberPanel.Text != "") if (Convert.ToInt32(PanelNumberPanel.Text) == Convert.ToInt32(DeskNumberPanel[i])) indexText = i;
                }
                if (indexText != 0) DeskNumberPanel[indexText] = ValuePanel;
                if (indexPanel != 0) DeskNumberPanel[indexPanel] = PanelNumberPanel.Text;
                PanelNumberPanel.Text = ValuePanel;

            }

            if (SetConf == "ChngLinkPanel")
            {
                OnOff = PanelLinkPanel.Text == "true" ? "On" : "Off";
                StrCreate = "UPDATE LISTIPRDP SET PANEL_LINKPANEL = '" + PanelLinkPanel.Text + "', ONOFF = '" + OnOff + "' WHERE LISTIPRDP.OVERALL_IDAPP = " + "'" + OverAllidAppRdp.Text + "'";
            }

            if (SetConf == "ChngIpRdp")
            {

                StrCreate = "UPDATE LISTIPRDP SET LOGIN = " + "'" + LoginRdp.Text + "'" + ", PASSWORD = " + "'" + PaswordRdp.Text + "'" +
                    ", TCPIP = '" + Ip4 + "'" + ", NAMECONECT = " + "'" + ConectRdp.Text + "'"
                   + ", PUTH = '" + PuthFile.Text + "'"
                   + ", DATATIME = '" + DateTime.Now.ToString("yyyyMMddHHmmss") + "'"
                    + ", OVERALL_NAMEAPP = '" + OverAllNameApp.Text + "'"
                    + ", OVERALL_AUTORIZETYPE = '" + OverAllAutorizeType.Text + "'"
                    + ", OVERALL_CAPTIONNAMEPLAY = '" + OverAllCaptionNamePlay.Text + "'"
                    + ", OVERALL_PUTHFILEIM = '" + OverAllPuthFileIm.Text + "'"
                    + ", OVERALL_INFOWORKSPACE = '" + OverAllinfoWorkSpace.Text + "'"
                    + ", PANEL_LINKPANEL = '" + PanelLinkPanel.Text + "'"
                    + ", PANEL_TYPELINK = '" + PanelTypeLink.Text + "'"
                    + ", PANEL_NUMBERPANEL = '" + PanelNumberPanel.Text + "'"
                    + ", PANEL_CAPTIONNAMEWORKSPACE = '" + PanelCaptionNameWorkSpace.Text + "'"
                    + ", PANEL_APPSTARTMETOD = '" + PanelAppStartMetod.Text + "'"
                    + ", PANEL_TYPEAPP = '" + PanelTypeApp.Text + "'"
                    + ", ONOFF = '" + OnOff + "'"
                    + ", AUTOSTARTTIMESEC = '" + Autostart.Text + "'"
                    + ", PUTHFILE_OTHERAPP = '" + PuthFile_OtherApp.Text + "'"
                    + "  WHERE LISTIPRDP.OVERALL_IDAPP = " + "'" + OverAllidAppRdp.Text + "'";
            }

            if (SetConf == "AddIpRdp")
            {
                StrCreate = "INSERT INTO LISTIPRDP  values ('" + LoginRdp.Text + "','" + PaswordRdp.Text +
                          "'" + ", '" + AdrIp4[4] + "', '" + ConectRdp.Text + "', '"
                          + PuthFile.Text + "', '" + DateTime.Now.ToString("yyyyMMddHHmmss") +
                    "', '" + OverAllidAppRdp.Text + "', '" + OverAllNameApp.Text + "', '" + OverAllAutorizeType.Text + "', '" + OverAllCaptionNamePlay.Text + "', '" +
                    OverAllPuthFileIm.Text + "', '" + OverAllinfoWorkSpace.Text + "', '" + OverAllidAppRdp.Text + "', '" + PanelLinkPanel.Text + "', '" + PanelTypeLink.Text +
                     "', '" + PanelNumberPanel.Text + "', '" + PanelCaptionNameWorkSpace.Text + "', '" + PanelAppStartMetod.Text + "', '" +
                    PanelTypeApp.Text + "', '" + OnOff + "', '" + Autostart.Text + "', '" + PuthFile_OtherApp.Text + "')";

            }

            DBConecto.DBopenFBConectionMemory(SystemBD.ConnnStringSystem().ToString(), "", "FbSystem");
            DBConecto.UniQuery UpdateQuery = new DBConecto.UniQuery(StrCreate, "FB");
            UpdateQuery.UserQuery = string.Format(StrCreate, "LISTIPRDP");
            UpdateQuery.ExecuteUNIScalar();
            DBConecto.DBcloseFBConectionMemory("FbSystem");
            ServerRdpAdd.Background = Brushes.SkyBlue;
            ServerRdpChng.Background = Brushes.SkyBlue;
            ServerRdpChng.Foreground = Brushes.Black;
            ServerRdpAdd.Foreground = Brushes.Black;
            RdpIsEnableFalse();
            SetWinSetHub = "";

            LoadedAppPlay();
            System.Windows.Application.Current.Dispatcher.Invoke(new Action(delegate ()
            {
                MainWindow ConectoWorkSpace_InW = AppStart.LinkMainWindow("ConectoWorkSpace");
                ConectoWorkSpace_InW.LoadPanelWorkSpace();

            }));

        }




        // Процедура проверки и установки связи с удаленным сервером
        public void RunIpRdp()
        {

            string StrCreate = "SELECT * FROM LISTIPRDP WHERE LISTIPRDP.TCPIP = " + "'" + TcpIpIprdp + "'";
            int Idcount = 0;
            DBConecto.DBopenFBConectionMemory(SystemBD.ConnnStringSystem().ToString(), "", "FbSystem");
            FbCommand SelectTable = new FbCommand(StrCreate, DBConecto.bdFbSystemConect);
            FbDataReader ReadOutTable = SelectTable.ExecuteReader();
            while (ReadOutTable.Read())
            {
                AppStart.TableReestr["LoginRdp"] = ReadOutTable[0].ToString();
                AppStart.TableReestr["PaswordRdp"] = ReadOutTable[1].ToString();
                AppStart.TableReestr["IpRdpIp4"] = ReadOutTable[2].ToString();
                Idcount++;
            }
            ReadOutTable.Close();
            if (Idcount == 0) return;
            DBConecto.DBcloseFBConectionMemory("FbSystem");
            UpdateKeyReestr("LoginRdp", AppStart.TableReestr["LoginRdp"]);
            UpdateKeyReestr("PaswordRdp", AppStart.TableReestr["PaswordRdp"]);
            UpdateKeyReestr("IpRdpIp4", AppStart.TableReestr["IpRdpIp4"]);
            Idcount = 0;
            string FileRdp = SystemConectoServers.PutchServer + @"rdp\netcom.rdp";
            if (File.Exists(FileRdp) && AppStart.TableReestr["IpRdpIp4"].Length != 0)
            {
                // Функция модификации  соединения в файле 
                Encoding code = Encoding.Default;
                string[] fileLines = File.ReadAllLines(FileRdp, code);
                foreach (string x in fileLines)
                {
                    if (x.Contains("full address") == true)
                    {
                        fileLines[Idcount] = "full address:s:" + AppStart.TableReestr["IpRdpIp4"];
                    }
                    Idcount++;
                }
                File.WriteAllLines(FileRdp, fileLines, code);
            }
            RdpVisibilCls();

        }

        public void RdpVisibilCls()
        {

            Terminal.Visibility = Visibility.Collapsed;
            KassaUser1.Visibility = Visibility.Collapsed;
            Office1.Visibility = Visibility.Collapsed;
            Bank1.Visibility = Visibility.Collapsed;
            Ecsport1C1.Visibility = Visibility.Collapsed;
            Megaplan1.Visibility = Visibility.Collapsed;

        }

        public void RdpVisibilClsChng()
        {
            IpRdplabel.Visibility = Visibility.Collapsed;
            LabelLoginIpRdp.Visibility = Visibility.Collapsed;
            LabelPasIpRdp.Visibility = Visibility.Collapsed;
            PaswordRdp.Visibility = Visibility.Collapsed;
            LoginRdp.Visibility = Visibility.Collapsed;
            IpRdpIp4Text1.Visibility = Visibility.Collapsed;
            IpRdpIp4Text2.Visibility = Visibility.Collapsed;
            IpRdpIp4Text3.Visibility = Visibility.Collapsed;
            IpRdpIp4Text4.Visibility = Visibility.Collapsed;
            Rdpdot1.Visibility = Visibility.Collapsed;
            Rdpdot2.Visibility = Visibility.Collapsed;
            Rdpdot3.Visibility = Visibility.Collapsed;
            LabConectIpRdp.Visibility = Visibility.Collapsed;
            ConectRdp.Visibility = Visibility.Collapsed;
            ServerRdpSave.Visibility = Visibility.Collapsed;

        }






        // Процедура записи введенного логина
        private void LoginRdp_KeyUp(object sender, KeyEventArgs e)
        {
            Administrator.AdminPanels.UpdateKeyReestr("LoginRdp", LoginRdp.Text);
        }
        // Процедура записи введенного пароля
        private void PaswordRdp_KeyUp(object sender, KeyEventArgs e)
        {
            Administrator.AdminPanels.UpdateKeyReestr("PaswordRdp", PaswordRdp.Text);
        }

        // Процедура записи введенного имени соединения
        private void ConectdRdp_KeyUp(object sender, KeyEventArgs e)
        {
            Administrator.AdminPanels.UpdateKeyReestr("ConectRdp", ConectRdp.Text);
        }
        // Процедура записи введенного пароля
        private void IpRdpIp4Text1_KeyUp(object sender, KeyEventArgs e)
        {
            Administrator.AdminPanels.UpdateKeyReestr("IpRdpIp4Text1", IpRdpIp4Text1.Text);
        }
        private void IpRdpIp4Text2_KeyUp(object sender, KeyEventArgs e)
        {
            Administrator.AdminPanels.UpdateKeyReestr("IpRdpIp4Text2", IpRdpIp4Text2.Text);
        }
        private void IpRdpIp4Text3_KeyUp(object sender, KeyEventArgs e)
        {
            Administrator.AdminPanels.UpdateKeyReestr("IpRdpIp4Text3", IpRdpIp4Text3.Text);
        }
        private void IpRdpIp4Text4_KeyUp(object sender, KeyEventArgs e)
        {
            Administrator.AdminPanels.UpdateKeyReestr("IpRdpIp4Text4", IpRdpIp4Text4.Text);
        }
        private void IpRdpIp4_LostFocus_LostFocus(object sender, RoutedEventArgs e)
        {
            AdrIp4[4] = IpRdpIp4Text1.Text + "." + IpRdpIp4Text2.Text + "." + IpRdpIp4Text3.Text + "." + IpRdpIp4Text4.Text;
            AdrIp4[4] = AdrIp4[4] == "..." ? "" : AdrIp4[4];
            Administrator.AdminPanels.UpdateKeyReestr("IpRdpIp4", AdrIp4[4]);
        }

        // Процедура записи введенного времени автозапуска
        private void Autostart_KeyUp(object sender, KeyEventArgs e)
        {
            if (Autostart.Text.Length == 0) Autostart.Text = "0";

            if (Autostart.Text.Substring(Autostart.Text.Length - 1, 1) != "0" && Autostart.Text.Substring(Autostart.Text.Length - 1, 1) != "1" &&
                Autostart.Text.Substring(Autostart.Text.Length - 1, 1) != "2" && Autostart.Text.Substring(Autostart.Text.Length - 1, 1) != "3" &&
                Autostart.Text.Substring(Autostart.Text.Length - 1, 1) != "4" && Autostart.Text.Substring(Autostart.Text.Length - 1, 1) != "5" &&
                Autostart.Text.Substring(Autostart.Text.Length - 1, 1) != "6" && Autostart.Text.Substring(Autostart.Text.Length - 1, 1) != "7" &&
                Autostart.Text.Substring(Autostart.Text.Length - 1, 1) != "8" && Autostart.Text.Substring(Autostart.Text.Length - 1, 1) != "9") Autostart.Text = Autostart.Text.Substring(0, Autostart.Text.Length - 1);
        }

        /// <summary>
        /// Параметр включения выключения терминала
        /// </summary>
        public static bool StatusTerminal = false;



        /// <summary>
        /// Включить режим терминала
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        private void TerminalOnOff_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            NameObj = "TerminalOnOff";
            if (Convert.ToInt32(AppStart.TableReestr["TerminalOnOff"]) == 2)
            {
                TerminalPerecluch(0);
                return;
            }
            if (Convert.ToInt32(AppStart.TableReestr["TerminalOnOff"]) == 1)
            {
                LoginAutoInput.IsEnabled = false;
                PaswAutoInput.IsEnabled = false;
                OkOnTerminal.IsEnabled = false;
                Interface.CurrentStateInst("TerminalOnOff", "0", "on_off_1.png", TerminalOnOff);
                return;
            }
            OkOnTerminal.IsEnabled = true;
            LoginAutoInput.IsEnabled = true;
            PaswAutoInput.IsEnabled = true;
            TerminalPerecluch(2);
            Interface.CurrentStateInst("TerminalOnOff", "2", "on_off_2.png", TerminalOnOff);
        }

        private void AvtoloadOSOnOff_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            NameObj = "AvtoloadOSOnOff";
            if (Convert.ToInt16(AppStart.TableReestr["AvtoloadOSOnOff"]) == 2 || Convert.ToInt32(AppStart.TableReestr["AvtoloadOSOnOff"]) == 1)
            {
                LoginAutoInput.Text = "";
                PaswAutoInput.Text = "";
                UpdateKeyReestr("LoginAutoInput", LoginAutoInput.Text);
                UpdateKeyReestr("PaswAutoInput", PaswAutoInput.Text);
                LoginAutoInput.IsEnabled = false;
                PaswAutoInput.IsEnabled = false;
                TerminalPerecluch(0);
                Interface.CurrentStateInst("AvtoloadOSOnOff", "0", "on_off_1.png", AvtoloadOSOnOff);

            }
            else
            {
                LoginAutoInput.IsEnabled = true;
                PaswAutoInput.IsEnabled = true;
                Interface.CurrentStateInst("AvtoloadOSOnOff", "1", "on_off_3.png", AvtoloadOSOnOff);
            }
            OkOnTerminal.IsEnabled = true;
        }

        private void CopyBdUser_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            NameObj = "CopyBdUser";
            if (Convert.ToInt16(AppStart.TableReestr["CopyBdUser"]) == 2)
            {
                Interface.CurrentStateInst("CopyBdUser", "0", "on_off_1.png", CopyBdUser);
            }
            else
            {
                Interface.CurrentStateInst("CopyBdUser", "2", "on_off_2.png", CopyBdUser);
            }
        }


        private void DateTimeNet_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            string SetKeyValue = "";
            if ((string)AppStart.rkAppSetingAllUser.GetValue("SerchDateTimeNet") == "2")
            {
                AppStart.rkAppSetingAllUser.SetValue("SerchDateTimeNet", "0");
                AppStart.rkAppSetingAllUser.Flush();
                SetKeyValue = "on_off_1.png";
            }
            else
            {
                AppStart.rkAppSetingAllUser.SetValue("SerchDateTimeNet", "2");
                AppStart.rkAppSetingAllUser.Flush();
                SetKeyValue = "on_off_2.png";
                AppStart.RenderInfo Arguments01 = new AppStart.RenderInfo() { };
                Arguments01.argument1 = "1";
                Thread thStartTimer01 = new Thread(SerchDateTimeTH);
                thStartTimer01.SetApartmentState(ApartmentState.STA);
                thStartTimer01.IsBackground = true; // Фоновый поток
                thStartTimer01.Start(Arguments01);
                InstallB52.IntThreadStart++;

            }
            var pic = (Image)LogicalTreeHelper.FindLogicalNode(this, "DateTimeNet");
            var uriSource = new Uri(@"/Conecto®%20WorkSpace;component/Images/" + SetKeyValue, UriKind.Relative);
            pic.Source = new BitmapImage(uriSource);

        }

        public void SerchDateTimeTH(object ThreadObj)
        {
            AppStart.RenderInfo arguments = (AppStart.RenderInfo)ThreadObj;
            SystemConectoTimeServer.ClientTime();
            InstallB52.IntThreadStart--;
        }


        private void ImPerekluchTerminal_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            TerminalPerecluch(2);
        }


        /// <summary>
        /// Ручной метод переключения режима терминал
        /// </summary>
        private void TerminalPerecluch(int OnOff)
        {
            LoginAutoInput.IsEnabled = false;
            PaswAutoInput.IsEnabled = false;
            OkOnTerminal.IsEnabled = false;

            // Определяется какая OS Windows 64 или 32 разрядная
            if (Environment.Is64BitOperatingSystem) AppStart.localMachine = RegistryKey.OpenBaseKey(Microsoft.Win32.RegistryHive.LocalMachine, RegistryView.Registry64);
            // Открыть  системный регистр для модификации ключа Shell
            RegistryKey regKeylocalMachine = AppStart.localMachine.OpenSubKey(@"SOFTWARE\Microsoft\Windows NT\CurrentVersion\Winlogon", true);

            if (Environment.Is64BitOperatingSystem) AppStart.CurrentMachine = RegistryKey.OpenBaseKey(Microsoft.Win32.RegistryHive.CurrentUser, RegistryView.Registry64);
            RegistryKey regKeyCurrentMachine = AppStart.CurrentMachine.OpenSubKey(@"SOFTWARE\Microsoft\Windows NT\CurrentVersion\Winlogon", true);

            RegistryKey AutoStartApp = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);
            //if (Environment.Is64BitOperatingSystem) AppStart.CurrentMachine = RegistryKey.OpenBaseKey(Microsoft.Win32.RegistryHive.CurrentUser, RegistryView.Registry64);
            //AutoStartApp = AppStart.CurrentMachine.OpenSubKey(@"SOFTWARE\Microsoft\Windows\\CurrentVersion\\Run", true);

            AppStart.rkAppSetingAllUser.SetValue("DefaultPassword", PaswAutoInput.Text);
            AppStart.rkAppSetingAllUser.SetValue("DefaultUserName", LoginAutoInput.Text);
            AppStart.rkAppSetingAllUser.Flush();

            if (NameObj == "AvtoloadOSOnOff")
            {
                string PuthConecto = Process.GetCurrentProcess().MainModule.FileName + " -t";
                if (Convert.ToInt32(AppStart.TableReestr["AvtoloadOSOnOff"]) == 1)
                {
                    // Выключить автовход в систему без логина и пароля
                    regKeylocalMachine.SetValue("AutoAdminLogon", "0", RegistryValueKind.String);
                    regKeylocalMachine.SetValue("DefaultPassword", "", RegistryValueKind.String);
                    if (LoginAutoInput.Text.Length != 0 && PaswAutoInput.Text.Length != 0)
                    {
                        // Включить вход в систему c логином и паролем
                        if (regKeylocalMachine.GetValue("DefaultUserName") == null || (string)regKeylocalMachine.GetValue("DefaultUserName") == "") regKeylocalMachine.SetValue("DefaultUserName", LoginAutoInput.Text, RegistryValueKind.String);
                        regKeylocalMachine.SetValue("AutoAdminLogon", "1", RegistryValueKind.String);
                        regKeylocalMachine.SetValue("DefaultPassword", PaswAutoInput.Text, RegistryValueKind.String); //"staric777"
                    }
                    AutoStartApp.SetValue("Conecto", PuthConecto, RegistryValueKind.String);
                    Interface.CurrentStateInst("AvtoloadOSOnOff", "2", "on_off_2.png", AvtoloadOSOnOff);
                }
                if (Convert.ToInt32(AppStart.TableReestr["AvtoloadOSOnOff"]) == 0)
                {
                    // Выключить автовход в систему без логина и пароля
                    regKeylocalMachine.SetValue("AutoAdminLogon", "0", RegistryValueKind.String);
                    regKeylocalMachine.SetValue("DefaultPassword", "", RegistryValueKind.String);
                    if (AutoStartApp.GetValue("Conecto") == null) AutoStartApp.SetValue("Conecto", "", RegistryValueKind.String);
                    AutoStartApp.DeleteValue("Conecto", true);
                }
                regKeylocalMachine.Flush();
                AutoStartApp.Flush();
                return;
            }

            string System_path = System.IO.Path.GetPathRoot(System.Environment.SystemDirectory) + @"Windows\explorer.exe";
            // Дополнительно проверить запуск explorer
            var getAllExploreProcess = Process.GetProcesses().Where(r => r.ProcessName.Contains("explorer"));
            // Открываем ключ для модификации влючения или отключения терминала режима  CurrentUser
            AppStart.rkStartApp.OpenSubKey(@"SOFTWARE\Microsoft\Windows NT\CurrentVersion\Winlogon", true);


            // определяем ключ для модификации влючения или отключения режима РДП
            RegistryKey regApp = AppStart.rkAppSetingAllUser;



            // Проверка включен ли терминал
            if (AppStart.rkAppSetingAllUser.GetValue("ImPerekluchTerminal") != null && (int)AppStart.rkAppSetingAllUser.GetValue("ImPerekluchTerminal") == 1)
            {
                // Выключить автовход в систему без логина и пароля

                regKeylocalMachine.SetValue("AutoAdminLogon", "0", RegistryValueKind.String);

                //regKeylocalMachine.SetValue("DefaultUserName", "", RegistryValueKind.String);
                regKeyCurrentMachine.SetValue("AutoAdminLogon", "0", RegistryValueKind.String);
                //regKeyCurrentMachine.SetValue("DefaultPassword", "", RegistryValueKind.String);
                // выключение запуска ConectoWorkSpace из автозагрузки.
                if (regKeyCurrentMachine.GetValue("Shell") != null) regKeyCurrentMachine.DeleteValue("Shell");



                // выключить РДП
                if (regApp.GetValue("TerminalRDP") != null && (int)regApp.GetValue("TerminalRDP") == 1)
                {
                    UpdateKeyReestr("TerminalRdpOnOff", "0");
                    RDPOnOff.IsEnabled = false;
                    regApp.SetValue("TerminalRDP", (bool)((CheckBox)LogicalTreeHelper.FindLogicalNode(this, "TerminalRDP")).IsChecked ? 1 : 0, RegistryValueKind.DWord);
                }

                // выключить теpминал Администратора
                regApp.SetValue("ImPerekluchTerminal", "0", RegistryValueKind.DWord);
                regApp.Flush();
                //Thread.Sleep(100);
                MainWindow ConectoWorkSpace_InW = AppStart.LinkMainWindow();
                if (ConectoWorkSpace_InW != null)
                {
                    ConectoWorkSpace_InW.MinimiButIm.Visibility = Visibility.Visible;
                    ConectoWorkSpace_InW.Note.Visibility = Visibility.Visible;
                    ConectoWorkSpace_InW.Calcul.Visibility = Visibility.Visible;
                    ConectoWorkSpace_InW.Close_F.Visibility = Visibility.Visible;
                    ConectoWorkSpace_InW.ButCatalog.Visibility = Visibility.Visible;
                }

                //запускаем   Explorer

                ProcessStartInfo processInfo = new ProcessStartInfo(System_path); //создаем новый процесс
                try
                {
                    Process.Start(processInfo);
                }
                catch (Win32Exception ex)
                {
                    //Ничего не делаем, потому что пользователь, возможно, нажал кнопку "Нет" в ответ на вопрос о запуске программы в окне предупреждения UAC (для Windows 7)
                    SystemConecto.ErorDebag(ex.ToString(), 1);
                }
                StatusTerminal = false;
                Interface.CurrentStateInst("TerminalOnOff", "0", "on_off_1.png", TerminalOnOff);
                Thread.Sleep(50);
                // перегрузить конекто WorkSpace
                ConectoWorkSpace_InW.ResizeConectoWorkSpace(WindowState.Normal);
                // Процедура изменение, обновление размера экрана

            }
            else
            {

                // включить теминал Администратора
                regApp.SetValue("ImPerekluchTerminal", "1", RegistryValueKind.DWord);

                // Включить вход в систему c логином и паролем
                if (regKeylocalMachine.GetValue("DefaultUserName") == null || (string)regKeylocalMachine.GetValue("DefaultUserName") == "") regKeylocalMachine.SetValue("DefaultUserName", LoginAutoInput.Text, RegistryValueKind.String);
                regKeylocalMachine.SetValue("AutoAdminLogon", "1", RegistryValueKind.String);
                regKeylocalMachine.SetValue("DefaultPassword", PaswAutoInput.Text, RegistryValueKind.String); //"staric777"

                regKeyCurrentMachine.SetValue("AutoAdminLogon", "1", RegistryValueKind.String);
                regKeyCurrentMachine.SetValue("DefaultPassword", PaswAutoInput.Text, RegistryValueKind.String);

                if (regApp.GetValue("TerminalRDP") != null && (int)regApp.GetValue("TerminalRDP") == 1)
                {
                    regKeyCurrentMachine.SetValue("Shell", System_path, RegistryValueKind.String);
                    regKeyCurrentMachine = AppStart.rkStartApp;
                }

                // Скрыть панель задачь
                // ProcessConecto.StateSystemWindow("Shell_TrayWnd"); // "HHTaskBar"
                // Срыть кнопку пуск
                // ProcessConecto.StateSystemWindow("Button");

                if (ProcessConecto.TerminateProcessSystem("explorer"))
                {
                    StatusTerminal = true;
                    Interface.CurrentStateInst("TerminalOnOff", "2", "on_off_2.png", TerminalOnOff);
                    Thread.Sleep(50);
                    // перегрузить конекто WorkSpace
                    MainWindow ConectoWorkSpace_InW = AppStart.LinkMainWindow();
                    if (ConectoWorkSpace_InW != null)
                    {
                        ConectoWorkSpace_InW.MinimiButIm.Visibility = Visibility.Collapsed;
                        ConectoWorkSpace_InW.Note.Visibility = Visibility.Collapsed;
                        //ConectoWorkSpace_InW.Calcul.Visibility = Visibility.Collapsed;
                        ConectoWorkSpace_InW.Close_F.Visibility = Visibility.Collapsed;
                        ConectoWorkSpace_InW.ButCatalog.Visibility = Visibility.Collapsed;
                        ConectoWorkSpace_InW.ResizeConectoWorkSpace(WindowState.Maximized);
                    }



                    //    // ConectoWorkSpace_InW.WindowState = WindowState.Maximized;
                    //    // Обновим рабочий стол
                    //    ConectoWorkSpace_InW.MainWindow2();
                    // Процедура изменение, обновление размера экрана
                }

                // Доп инфо блокиовка всех клавиш с помощью АПИ (особенно комбинации CNtrl+Schift+Esc) сделано HookSystemKeys.cs
                // http://www.intuit.ru/department/se/prcsharp08/11/2.html
                // Если режим терминала то заблокирвать клавиши
                // ======   Доп панель в панели задач (проверка режима терминал)
                if (SystemConecto.TerminalStatus)
                {
                    HookSystemKeys.FunHook();// Запрещаем системные клавиши
                }

                // Изменение значения ключа "Shell для запуска ConectoWorkSpace при перегрузке системы

                string PuthConecto = Process.GetCurrentProcess().MainModule.FileName + " -t";
                regKeyCurrentMachine.SetValue("Shell", PuthConecto, RegistryValueKind.String);

            }
            regKeyCurrentMachine.Flush();
            regApp.Flush();

        }

        /// <summary>
        /// // включение / выключение терминала РДП
        /// </summary>
        private void TerminalRDP_Click_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            AppStart.rkAppSetingAllUser.SetValue("TerminalRDP", (bool)((CheckBox)LogicalTreeHelper.FindLogicalNode(this, "TerminalRDP")).IsChecked ? 1 : 0, RegistryValueKind.DWord);
            AppStart.rkAppSetingAllUser.Flush();

            if ((int)AppStart.rkAppSetingAllUser.GetValue("TerminalRDP") == 0)
            {
                // выключен РДП, очистить реестр, снять пометку включен в окне терминала


                RDPOnOff.IsEnabled = false;
                UpdateKeyReestr("TerminalRdpOnOff", "0");

                RegistryKey rkStartApp = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\Microsoft\Windows NT\CurrentVersion\Winlogon", true);
                rkStartApp.SetValue("Shell", "", RegistryValueKind.String);
                rkStartApp.Flush();
                // Переоределить ключ какая OS Windows 64 или 32 разрядная
                RegistryKey localMachine = RegistryKey.OpenBaseKey(Microsoft.Win32.RegistryHive.LocalMachine, RegistryView.Registry32);
                if (SystemConecto.OS64Bit)
                {
                    localMachine = RegistryKey.OpenBaseKey(Microsoft.Win32.RegistryHive.LocalMachine, RegistryView.Registry64);
                }
                // Открыть  системный регистр для модификации ключа Shell
                RegistryKey regKey = localMachine.OpenSubKey(@"SOFTWARE\Microsoft\Windows NT\CurrentVersion\Winlogon", true);
                regKey.SetValue("Shell", Process.GetCurrentProcess().MainModule.FileName, RegistryValueKind.String);
                regKey.Flush();

            }
            else
            {
                //UpdateKeyReestr("TerminalRdpOnOff", "1");
                //RdpVisiblOn();
                // принудительное выключение режима теминала чтобы автоматически включить снова
                // для случая включенного терминала и включения РДП

                //if (AppStart.rkAppSetingAllUser.GetValue("ImPerekluchTerminal") != null && (int)AppStart.rkAppSetingAllUser.GetValue("ImPerekluchTerminal") == 1) return;
                AppStart.rkAppSetingAllUser.SetValue("ImPerekluchTerminal", "0", RegistryValueKind.DWord);
                AppStart.rkAppSetingAllUser.Flush();
                TerminalPerecluch(0);
            }
        }



        private void Info_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (OverAllinfoWorkSpace.Text == "true")
            {
                OverAllinfoWorkSpace.Text = "false";
                Interface.CurrentStateInst("PerekluchInfo", "0", "on_off_1.png", PerekluchInfo);
            }
            else
            {
                OverAllinfoWorkSpace.Text = "true";
                Interface.CurrentStateInst("PerekluchInfo", "2", "on_off_2.png", PerekluchInfo);
            }
        }

        private void PanelVisiblOnOff_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            SetConf = "ChngLinkPanel";
            if (PanelLinkPanel.Text == "true")
            {
                PanelLinkPanel.Text = "false";
                Interface.CurrentStateInst("PanelVisiblOnOff", "0", "on_off_1.png", PanelVisiblOnOff);
                Interface.CurrentStateInst("PerekluchLinkPanel", "0", "on_off_1.png", PerekluchLinkPanel);
                SaveIpRdp();
            }
            else
            {
                PanelLinkPanel.Text = "true";
                Interface.CurrentStateInst("PanelVisiblOnOff", "2", "on_off_2.png", PanelVisiblOnOff);
                Interface.CurrentStateInst("PerekluchLinkPanel", "2", "on_off_2.png", PerekluchLinkPanel);
                SaveIpRdp();
            }
        }


        private void Link_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

            if (PanelLinkPanel.Text == "true")
            {
                PanelLinkPanel.Text = "false";
                Interface.CurrentStateInst("PerekluchLinkPanel", "0", "on_off_1.png", PerekluchLinkPanel);
                Interface.CurrentStateInst(" PanelVisiblOnOff", "0", "on_off_1.png", PanelVisiblOnOff);

            }
            else
            {
                PanelLinkPanel.Text = "true";
                Interface.CurrentStateInst("PerekluchLinkPanel", "2", "on_off_2.png", PerekluchLinkPanel);
                Interface.CurrentStateInst(" PanelVisiblOnOff", "2", "on_off_2.png", PanelVisiblOnOff);
            }

        }



        private void image20_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            ServerRdpChng.IsEnabled = true;
            PanelVisiblOnOff.IsEnabled = true;
            SelectApp("200");
        }

        private void Megoplan_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            ServerRdpChng.IsEnabled = true;
            PanelVisiblOnOff.IsEnabled = true;
            SelectApp("121");
        }

        private void KassaUser_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            ServerRdpChng.IsEnabled = true;
            PanelVisiblOnOff.IsEnabled = true;
            SelectApp("123");
        }

        private void Brauser_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            ServerRdpChng.IsEnabled = true;
            PanelVisiblOnOff.IsEnabled = true;
            SelectApp("129");
        }

        private void OficeBrauser_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            ServerRdpChng.IsEnabled = true;
            PanelVisiblOnOff.IsEnabled = true;
            SelectApp("126");
        }
        private void Export1c_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            ServerRdpChng.IsEnabled = true;
            PanelVisiblOnOff.IsEnabled = true;
            SelectApp("122");
        }
        private void BackOfice_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            ServerRdpChng.IsEnabled = true;
            PanelVisiblOnOff.IsEnabled = true;
            ClickIcon = "pack://application:,,,/Conecto® WorkSpace;component/Images/ico_prog_office2.png";
            SelectApp("128");
        }

        private void FrontOfice_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

            ServerRdpChng.IsEnabled = true;
            PanelVisiblOnOff.IsEnabled = true;
            SelectApp("127");
        }

        private void PerekluchServisOnOff_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {

            // Анализ, активен процесс или нет.
            int IndexActivProces = -1;
            string Fb = "Com_Kassa_24";
            ServiceController[] scServices;
            scServices = ServiceController.GetServices();
            foreach (ServiceController scTemp in scServices)
            {
                if (scTemp.ServiceName == Fb) { IndexActivProces++; break; }
            }

            if (Convert.ToInt32(AppStart.TableReestr["ServisOnOff"]) == 0)
            {
                if (IndexActivProces < 0)
                {
                    SystemConecto.DIR_(SystemConectoServers.PutchServer + @"SystemTasks\");
                    Dictionary<string, string> ListTask = new Dictionary<string, string>();
                    ListTask.Add(SystemConectoServers.PutchServer + @"SystemTasks\SystemTasks.exe", "dbservis/");
                    if (SystemConecto.IsFilesPRG(ListTask, -1, "- Проверка файлов при старте") == "True") IndexActivProces = -1;

                    if (System.IO.File.Exists(SystemConectoServers.PutchServer + @"SystemTasks\SystemTasks.exe") == true)
                    {
                        string CmdName = @"c:\Windows\Microsoft.NET\Framework\v4.0.30319\installutil.exe ";
                        string CmdArg = '"' + SystemConectoServers.PutchServer + @"SystemTasks\SystemTasks.exe" + '"';  //@""" c:\Program Files\Conecto\Servers\SystemTasks\SystemTasks.exe""";

                        Process mv_prcInstaller = new Process();
                        mv_prcInstaller.StartInfo.FileName = CmdName;
                        mv_prcInstaller.StartInfo.Arguments = CmdArg;
                        mv_prcInstaller.StartInfo.UseShellExecute = false;
                        mv_prcInstaller.StartInfo.CreateNoWindow = true;
                        mv_prcInstaller.StartInfo.RedirectStandardOutput = true;
                        mv_prcInstaller.Start();
                        mv_prcInstaller.WaitForExit();
                        mv_prcInstaller.Close();

                        IndexActivProces = -1;
                        scServices = ServiceController.GetServices();
                        foreach (ServiceController scTemp in scServices)
                        {
                            if (scTemp.ServiceName == Fb) { IndexActivProces++; break; }
                        }
                        if (IndexActivProces >= 0)
                        {
                            ServiceController service = new ServiceController("Com_Kassa_24");
                            // Проверяем не запущена ли служба
                            if (service.Status != ServiceControllerStatus.Running)
                            {
                                // Запускаем службу
                                service.Start();
                                // В течении минуты ждём статус от службы
                                service.WaitForStatus(ServiceControllerStatus.Running, TimeSpan.FromMinutes(1));
                                SystemConecto.ErorDebag("Служба SystemTasks была успешно запущена!");

                            }
                            else { SystemConecto.ErorDebag("Служба SystemTasks ранее успешно запущена!"); }
                        }
                        Interface.CurrentStateInst("ServisOnOff", "2", "on_off_2.png", PerekluchServisOnOff);
                    }

                }
                else { Interface.CurrentStateInst("ServisOnOff", "0", "on_off_1.png", PerekluchServisOnOff); return; }


            }
            if (Convert.ToInt32(AppStart.TableReestr["ServisOnOff"]) == 2)
            {
                if (IndexActivProces >= 0)
                {
                    // Процесс активен, необходимо выключить.
                    if (System.IO.File.Exists(SystemConectoServers.PutchServer + @"SystemTasks\SystemTasks.exe") == true)
                    {
                        string CmdName = @"c:\Windows\Microsoft.NET\Framework\v4.0.30319\installutil.exe";
                        string CmdArg = " /u " + '"' + SystemConectoServers.PutchServer + @"SystemTasks\SystemTasks.exe" + '"';  //@""" c:\Program Files\Conecto\Servers\SystemTasks\SystemTasks.exe""";

                        Process mv_prcInstaller = new Process();
                        mv_prcInstaller.StartInfo.FileName = CmdName;
                        mv_prcInstaller.StartInfo.Arguments = CmdArg;
                        mv_prcInstaller.StartInfo.UseShellExecute = false;
                        mv_prcInstaller.StartInfo.CreateNoWindow = true;
                        mv_prcInstaller.StartInfo.RedirectStandardOutput = true;
                        mv_prcInstaller.Start();
                        mv_prcInstaller.WaitForExit();
                        mv_prcInstaller.Close();
                    }
                }

            }
            Interface.CurrentStateInst("ServisOnOff", "0", "on_off_1.png", PerekluchServisOnOff);
        }
        public void RefreshKeyOnOff(int OnTerminal, Label ContentLabel, Image ImageKey, Image Perekluch, string ObjectName, Uri Filepng, int ChangeKey)
        {

            // Прорисовка
            if (ChangeKey == 2)
            {
                UpdateKeyReestr(ObjectName, OnTerminal == 1 ? "0" : "1");
                Uri uriSource = OnTerminal == 1 ? new Uri(@"/Conecto®%20WorkSpace;component/Images/pnsys_1_1_vklvykl2.png", UriKind.Relative) :
                   new Uri(@"/Conecto®%20WorkSpace;component/Images/pnsys_1_1_vklvykl1.png", UriKind.Relative);
                Perekluch.Source = new BitmapImage(uriSource);
                return;
            }
            if (OnTerminal == 1 && ChangeKey == 0 || OnTerminal == 0 && ChangeKey == 1)
            {
                ContentLabel.Content = "включен";
                Uri uriSource = Filepng;
                ImageKey.Source = new BitmapImage(uriSource);
                uriSource = new Uri(@"/Conecto®%20WorkSpace;component/Images/pnsys_1_1_vklvykl1.png", UriKind.Relative);
                Perekluch.Source = new BitmapImage(uriSource);
                if (ChangeKey == 1) UpdateKeyReestr(ObjectName, "1");
            }
            else
            {
                Uri uriSource = Filepng;
                ImageKey.Source = new BitmapImage(uriSource);
                uriSource = new Uri(@"/Conecto®%20WorkSpace;component/Images/pnsys_1_1_vklvykl2.png", UriKind.Relative);
                Perekluch.Source = new BitmapImage(uriSource);
                ContentLabel.Content = "выключен";
                if (ChangeKey == 1) UpdateKeyReestr(ObjectName, "0");
            }
        }

        public void RefreshOnOff(string OnTerminal, Image Perekluch, string ObjectName, int WriteKey)
        {

            if ((OnTerminal == "On" && WriteKey == 0) || OnTerminal == "Off" && WriteKey == 1)
            {
                //"выключен значит включить";
                Uri uriSource = new Uri(@"/Conecto®%20WorkSpace;component/Images/pnsys_1_1_vklvykl1.png", UriKind.Relative);
                Perekluch.Source = new BitmapImage(uriSource);
                if (ObjectName != "") UpdateKeyReestr(ObjectName, "1");
                OnOff = "On";
            }
            if ((OnTerminal == "Off" && WriteKey == 0) || OnTerminal == "On" && WriteKey == 1)
            {
                //"включен значит выключить ";               
                Uri uriSource = new Uri(@"/Conecto®%20WorkSpace;component/Images/pnsys_1_1_vklvykl2.png", UriKind.Relative);
                Perekluch.Source = new BitmapImage(uriSource);
                if (ObjectName != "") UpdateKeyReestr(ObjectName, "0");
                OnOff = "Off";
            }
        }


        /// <summary>
        /// Процедуры администрирования состояния переключателей  всех панелей закладки "Решения"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        public static void CheckTTF16(object ThreadObj)
        {
            // Выделяем  информацию о TTF16 Получить все модули внутри процесса
            string strModulePath = "";

            var parent = Registry.ClassesRoot.OpenSubKey("TypeLib", true);
            var subKeys = parent.GetSubKeyNames();
            foreach (var subKey in subKeys)
            {
                var sub = parent.OpenSubKey(subKey);
                if (sub != null)
                {
                    var inFold1 = sub.OpenSubKey(@"6.1\0\win32");
                    if (inFold1 != null)
                    {
                        var val = inFold1.GetValue(null);
                        if (val != null)
                        {
                            var name = val.ToString();
                            if (!string.IsNullOrWhiteSpace(name)) strModulePath = name;
                            if (strModulePath.Contains("TTF16.ocx") == true)

                                if (strModulePath != SystemConectoServers.PutchLib + "TTF16.ocx")
                                {
                                    if (!System.IO.File.Exists(strModulePath))
                                    {
                                        parent.DeleteSubKeyTree(subKey);
                                    }
                                    else DllWork.RegSrv32(strModulePath, "/u");
                                    string DeletTime = DateTime.Now.ToString("yyyyMMddHHmmss");
                                    // Записываем данные в файл протокол и удаляем неизвестную "TTF16.ocx"
                                    using (System.IO.StreamWriter file = new System.IO.StreamWriter(SystemConectoServers.PutchLib + "TTF16.log", true))
                                    {
                                        file.WriteLine("Удалена библиотека TTF16.ocx запущенная из папки :" + strModulePath + "  Время выполнения операции :" + DeletTime);
                                    }
                                    if (!System.IO.File.Exists(SystemConectoServers.PutchLib + "TTF16.ocx"))
                                    {
                                        Dictionary<string, string> TTF16List = new Dictionary<string, string>();
                                        TTF16List.Add(SystemConectoServers.PutchLib + @"TTF16.ocx", "b52/libs/");
                                        if (SystemConecto.IsFilesPRG(TTF16List, -1, "- Проверка файлов во время установки сервера ") != "True")
                                        {
                                            var TextWindows = "Отсутствует библиотека TTF16.ocx. ";
                                            InstallB52.MessageErorInst(TextWindows);
                                        }
                                    }
                                    // Регистрация бибилиотеки
                                    if (System.IO.File.Exists(SystemConectoServers.PutchLib + @"TTF16.ocx")) DllWork.RegSrv32(SystemConectoServers.PutchLib + "TTF16.ocx", "/s");


                                }
                        }
                    }
                    else DllWork.RegSrv32(SystemConectoServers.PutchLib + "TTF16.ocx", "/s");
                }
            }
            ConectoWorkSpace.Administrator.AdminPanels.UpdateKeyReestr("CheckTTF16OnOff", "2");
            ConectoWorkSpace.Administrator.AdminPanels.UpdateKeyReestr("CheckTTF16", "2");
            System.Windows.Application.Current.Dispatcher.Invoke(new Action(delegate ()
            {
                ConectoWorkSpace.Administrator.AdminPanels ConectoWorkSpace_ttf16 = AppStart.LinkMainWindow("WAdminPanels");
                Interface.CurrentStateInst("CheckTTF16OnOff", "2", "on_off_2.png", ConectoWorkSpace_ttf16.CheckTTF16OnOff);
                Interface.CurrentStateInst("CheckTTF16OnOff_Front", "2", "on_off_2.png", ConectoWorkSpace_ttf16.CheckTTF16OnOff_Front);
            }));
            InstallB52.IntThreadStart--;
        }

        //Process[] ObjModulesList = Process.GetProcessesByName("TTF16"); для 64 разрядных процессов
        //foreach (Process nobjModule in ObjModulesList)
        //{
        //    // Заполнить коллекцию модулей
        //    ProcessModuleCollection ObjModules = ObjModulesList[0].Modules;
        //    // Итерация по коллекции модулей.
        //    foreach (ProcessModule objModule in ObjModules)
        //    {
        //        //Получить правильный путь к модулю
        //        string strModulePath = objModule.FileName.ToString();
        //        //Если модуль существует
        //        if (System.IO.File.Exists(objModule.FileName.ToString()))
        //        {
        //            //Читать версию
        //            string strFileVersion = objModule.FileVersionInfo.FileVersion.ToString();
        //            //Читать размер файла
        //            string strFileSize = objModule.ModuleMemorySize.ToString();
        //            //Читать дату модификации
        //            FileInfo objFileInfo = new FileInfo(objModule.FileName.ToString());
        //            string strFileModificationDate = objFileInfo.LastWriteTime.ToShortDateString();
        //            //Читать описание файла
        //            string strFileDescription = objModule.FileVersionInfo.FileDescription.ToString();
        //            //Читать имя продукта
        //            string strProductName = objModule.FileVersionInfo.ProductName.ToString();
        //            //Читать версию продукта
        //            string strProductVersion = objModule.FileVersionInfo.ProductVersion.ToString();
        //            if (strModulePath != SystemConectoServers.PutchLib + "TTF16.ocx")
        //            {
        //                // Записываем данные в файл протокол и удаляем неизвестную "TTF16.ocx"
        //                using (System.IO.StreamWriter file = new System.IO.StreamWriter(SystemConectoServers.PutchLib + "TTF16.log", true))
        //                {
        //                    file.WriteLine(strModulePath, strFileVersion, strFileSize, strFileModificationDate, strFileDescription,
        //                              strProductName, strProductVersion);
        //                }
        //            }
        //        }
        //        if (!System.IO.File.Exists(objModule.FileName.ToString()) || strModulePath != SystemConectoServers.PutchLib + "TTF16.ocx" )
        //        {
        //            DllWork.RegSrv32(SystemConectoServers.PutchLib + "TTF16.ocx", "/u");
        //            if (!System.IO.File.Exists(SystemConectoServers.PutchLib + @"TTF16.ocx"))
        //            {
        //                Dictionary<string, string> TTF16List = new Dictionary<string, string>();
        //                TTF16List.Add(SystemConectoServers.PutchLib + @"TTF16.ocx", "b52/libs/");
        //                if (SystemConecto.IsFilesPRG(fbembedList, -1, "- Проверка файлов во время установки сервера "  ) != "True") 
        //                {
        //                    var TextWindows = "Отсутствует библиотека TTF16.ocx. ";
        //                    InstallB52.MessageErorInst(TextWindows);
        //                }
        //            }
        //            // Регистрация бибилиотеки
        //            if (System.IO.File.Exists(SystemConectoServers.PutchLib + @"TTF16.ocx")) DllWork.RegSrv32(SystemConectoServers.PutchLib + "TTF16.ocx", "/s");
        //        }
        //    }
        //}



        public static void InitPanel_()
        {
            int isOn = 0;
            string StatusPic = "";
            ConectoWorkSpace.Administrator.AdminPanels ConectoWorkSpace_InW = AppStart.LinkMainWindow("WAdminPanels");
            foreach (string NameCheck in AdminPanels.ButtonPanel)
            {
                isOn = Convert.ToInt32(AppStart.TableReestr[NameCheck].ToString());
                switch (isOn)
                {
                    case 0:
                        StatusPic = "on_off_1.png";
                        break;
                    case 1:
                        StatusPic = "on_off_3.png";
                        break;
                    case 2:
                        StatusPic = "on_off_2.png";
                        break;
                    case 3:
                        StatusPic = "on_off_4.png";
                        break;
                }
                var uriSource = new Uri(@"/Conecto®%20WorkSpace;component/Images/" + StatusPic, UriKind.Relative);
                var pic = (Image)LogicalTreeHelper.FindLogicalNode(ConectoWorkSpace_InW, NameCheck);
                pic.Source = new BitmapImage(uriSource);

            }
        }
        //MessageBox.Show(isOn.ToString()); 
        // Процедура инициализации переключателей в реестре Windows

        public static void InitKeyOnOff()
        {
            foreach (string NameCheck in AdminPanels.ButtonPanel)
            {
                InitKeyReestr(NameCheck, "0");
            }
        }

        // Процедура инициализации переключателей в реестре Windows

        public static void InitKeyOnOffOne(string NameCheck)
        {

            InitKeyReestr(NameCheck, "0");

        }

        // Процедура инициализации текстовых констант в реестре Windows
        public static void InitTextOnOff()
        {
            foreach (string NameCheck in AdminPanels.ButtonPanel)
            {
                InitKeyReestr(NameCheck, "");
            }
        }

        // заставка активной закладки 
        private void OffAdmin_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var uriSource = new Uri(@"/Conecto®%20WorkSpace;component/Images/admin_1.png", UriKind.Relative);
            // Rechenie.SetValue. = new BitmapImage(uriSource);
        }

        //        // процессы получить информацию об активном процессе
        //            procs = Process.GetProcessesByName("Notepad");
        //      MessageBox.Show("Всего : " + procs.Length.ToString());
        //      foreach(var notepad in procs)
        //      notepad.Kill();


        public static void AnalizUpdate(string[] AppUpdate)
        {
            //string[] AppUpdate = { "B52BackOffice8.exe", "B52Fitness8", "B52Hotel8", "B52FrontOffice8" };
            int AppUpdateCount = AppUpdate.Count() - 1;
            string OdecaUri = @"ftp://195.138.94.90/bin/";
            string NameUser = "partner";
            string PasswdUser = "cnelbzgk.c";

            if (SystemConecto.ConnectionAvailable())
            {
                for (int i = 0; i <= AppUpdateCount; i++)
                {
                    Uri UriServer = new Uri(OdecaUri + AppUpdate[i]);
                    string StringServer = @"195.138.94.90/bin/" + AppUpdate[i];
                    if (!System.IO.File.Exists(SystemConecto.PutchApp + @"Repository\" + AppUpdate[i]))
                    {
                        // Первая загрузка в репозитарий  B52BackOffice8.exe.
                        var ConectionFTPUpdate = SystemConecto.ConntecionFTP(StringServer, NameUser, PasswdUser, 2, SystemConecto.PutchApp + @"Repository\" + AppUpdate[i]);
                        if (ConectionFTPUpdate == null)
                        {
                            var TextWindows = "Отсутствует " + AppUpdate[i] + Environment.NewLine + "Обновление невыполено. ";
                            MainWindow.MessageInstall(TextWindows);
                        }
                    }
                    else
                    {
                        // проверка даты последней модификации
                        //FtpWebRequest request = (FtpWebRequest)WebRequest.Create(UriServer);
                        //request.Method = WebRequestMethods.Ftp.GetDateTimestamp;
                        //request.Credentials = new NetworkCredential(NameUser, PasswdUser);
                        //FtpWebResponse response = (FtpWebResponse)request.GetResponse();
                        //DateTime UpdateB52BD = response.LastModified;
                        //response.Close();
                        // считываем с фтп сервера.
                        // Проверка контрольной суммы размера файла.B52BackOffice8.exe

                        WebRequest FileChecksum = WebRequest.Create(UriServer);
                        FileChecksum.Credentials = new NetworkCredential(NameUser, PasswdUser);
                        WebResponse fs1 = FileChecksum.GetResponse();
                        var fs = fs1.GetResponseStream();
                        var md5 = MD5.Create();
                        byte[] checkSum = md5.ComputeHash(fs);
                        string Ftpresult = BitConverter.ToString(checkSum).Replace("-", String.Empty);
                        fs.Close();

                        if (File.Exists(SystemConecto.PutchApp + @"Repository\" + AppUpdate[i]))
                        {
                            FileStream LocFile = System.IO.File.OpenRead(SystemConecto.PutchApp + @"Repository\" + AppUpdate[i]);
                            MD5 Locmd5 = new MD5CryptoServiceProvider();
                            byte[] fileData = new byte[LocFile.Length];
                            LocFile.Read(fileData, 0, (int)LocFile.Length);
                            byte[] LoccheckSum = Locmd5.ComputeHash(fileData);
                            string Locresult = BitConverter.ToString(LoccheckSum).Replace("-", String.Empty);
                            LocFile.Close();

                            if (Ftpresult != Locresult)
                            {
                                // Обновляем  B52BackOffice8.exe.
                                File.Delete(SystemConecto.PutchApp + @"Repository\" + AppUpdate[i]);
                                var ConectionFTPUpdate = SystemConecto.ConntecionFTP(StringServer, NameUser, PasswdUser, 2, SystemConecto.PutchApp + @"Repository\" + AppUpdate[i]);
                                if (ConectionFTPUpdate == null)
                                {
                                    var TextWindows = "Отсутствует" + AppUpdate[i] + Environment.NewLine + "Обновление невыполено. ";
                                    MainWindow.MessageInstall(TextWindows);

                                }

                            }
                        }
                        else
                        {
                            var ConectionFTPUpdate = SystemConecto.ConntecionFTP(StringServer, NameUser, PasswdUser, 2, SystemConecto.PutchApp + @"Repository\" + AppUpdate[i]);
                            if (ConectionFTPUpdate == null)
                            {
                                var TextWindows = "Отсутствует" + AppUpdate[i] + Environment.NewLine + "Обновление невыполено. ";
                                MainWindow.MessageInstall(TextWindows);

                            }
                        }


                    }
                }



            }
            else
            {
                var TextWindows = "Отсутствует связь с интернетом." + Environment.NewLine + "Обновление невозможно. ";
                MainWindow.MessageInstall(TextWindows);
            }

        }


        // Проверка установки Бекофис
        public static void CheckInstalBack25()
        {

            string[] FolderBack = { "BackOfRozn", "BackOfRestoran", "BackOfFastFud", "BackOfFitnes", "BackOfHotel", "BackOfMix" };
            string[] FolderFront = { "Rozn", "Restoran", "FastFud", "Fitnes", "Hotel", "Mix" };
            string[] AppUpdate = { "B52BackOffice8.exe" };
            //AnalizUpdate(AppUpdate);

            DBConecto.DBopenFBConectionMemory(SystemBD.ConnnStringSystem().ToString(), "", "FbSystem");
            string InsertExecute = "SELECT count(*) from REESTRBACK";
            DBConecto.UniQuery CountQuery = new DBConecto.UniQuery(InsertExecute, "FB");
            string CountTable = CountQuery.ExecuteUNIScalar() == null ? "" : CountQuery.ExecuteUNIScalar().ToString();
            //if (Convert.ToUInt32(CountTable) == 0) return;
            string[] ServerName = new string[Convert.ToUInt32(CountTable)];
            string[] NameExeFile = new string[Convert.ToUInt32(CountTable)];
            string StrCreate = "SELECT * from REESTRBACK";
            int Idcount = -1;
            DBConecto.DBopenFBConectionMemory(SystemBD.ConnnStringSystem().ToString(), "", "FbSystem");
            FbCommand SelectTableFront = new FbCommand(StrCreate, DBConecto.bdFbSystemConect);
            FbDataReader ReadOutTableFront = SelectTableFront.ExecuteReader();
            while (ReadOutTableFront.Read())
            {
                Idcount++;
                ServerName[Idcount] = ReadOutTableFront[2].ToString();
                NameExeFile[Idcount] = ReadOutTableFront[3].ToString();

            }
            ReadOutTableFront.Close();
            DBConecto.DBcloseFBConectionMemory("FbSystem");

            if (Idcount >= 0)
            {
                for (int i = 0; i <= Idcount; i++)
                {
                    if (!File.Exists(NameExeFile[i]))
                    {
                        for (int ind = 0; ind <= 4; ind++)
                        {
                            if (NameExeFile[i].Contains(FolderBack[ind]))
                            {
                                UpdateKeyReestr(FolderBack[ind] + "OnOff", "0");
                                ConectoWorkSpace.Administrator.AdminPanels ConectoWorkSpace_Of = AppStart.LinkMainWindow("WAdminPanels");
                                var pic = (Image)LogicalTreeHelper.FindLogicalNode(ConectoWorkSpace_Of, FolderBack[ind] + "OnOff");
                                var uriSource = new Uri(@"/Conecto®%20WorkSpace;component/Images/" + "on_off_1.png", UriKind.Relative);
                                pic.Source = new BitmapImage(uriSource);
                                //Directory.Delete(NameExeFile[i].Substring(0,NameExeFile[i].LastIndexOf(@"\")-1), true);
                            }

                        }

                    }
                    else
                    {
                        for (int ind = 0; ind <= 4; ind++)
                        {
                            if (NameExeFile[i].Contains(FolderBack[ind]))
                            {
                                UpdateKeyReestr(FolderBack[ind] + "OnOff", "2");
                                ConectoWorkSpace.Administrator.AdminPanels ConectoWorkSpace_Of = AppStart.LinkMainWindow("WAdminPanels");
                                var pic = (Image)LogicalTreeHelper.FindLogicalNode(ConectoWorkSpace_Of, FolderBack[ind] + "OnOff");
                                var uriSource = new Uri(@"/Conecto®%20WorkSpace;component/Images/" + "on_off_2.png", UriKind.Relative);
                                pic.Source = new BitmapImage(uriSource);
                                //Directory.Delete(NameExeFile[i].Substring(0,NameExeFile[i].LastIndexOf(@"\")-1), true);
                            }

                        }

                    }

                }
                LoadedGridFront("SELECT * from REESTRBACK");
            }
            else
            {
                //DriveInfo IdDevice = new DriveInfo("d:");
                string PatchSRBack = "", NameFileExe = "";
                DriveInfo[] allDrives = DriveInfo.GetDrives();
                for (int i = 0; i <= 5; i++)
                {
                    PatchSRBack = "";
                    foreach (DriveInfo IdDevice in allDrives)
                    {
                        if (IdDevice.IsReady == true)
                        {
                            if (Directory.Exists(IdDevice.Name + FolderBack[i] + @"\bin\"))
                            {
                                if (File.Exists(IdDevice.Name + FolderBack[i] + @"\bin\aptune.ini"))
                                {
                                    PatchSRBack = IdDevice.Name + FolderBack[i] + @"\bin\";
                                    break;
                                }
                                else Directory.Delete(IdDevice.Name + FolderBack[i], true);
                            }

                        }

                    }
                    if (PatchSRBack != "")
                    {
                        string NameServerIns = NameServer25;
                        string Back = "BackFbd25OnOff", NameBack = "", TcpIpPortHub = "", FileKey = "";
                        if (!SystemConecto.File_(PatchSRBack + "aptune.ini", 5))
                        {
                            UpdateKeyReestr(FolderBack[i] + "OnOff", "0");
                            ConectoWorkSpace.Administrator.AdminPanels ConectoWorkSpace_Of = AppStart.LinkMainWindow("WAdminPanels");
                            var pic = (Image)LogicalTreeHelper.FindLogicalNode(ConectoWorkSpace_Of, FolderBack[i] + "OnOff");
                            var uriSource = new Uri(@"/Conecto®%20WorkSpace;component/Images/" + "on_off_1.png", UriKind.Relative);
                            pic.Source = new BitmapImage(uriSource);
                        }
                        else
                        {
                            if (File.Exists(PatchSRBack + "conecto.ini"))
                            {
                                //Back = File.ReadLines(PatchSRBack + "conecto.ini").First(x => x.StartsWith("Back")).Split('=')[1].Replace(" ", "");
                                string[] fileLines = File.ReadAllLines(PatchSRBack + "conecto.ini");
                                IndRecno = 0;
                                foreach (string x in fileLines)
                                {
                                    if (x.Length == 0) break;
                                    string[] data = x.Split('=');
                                    if (data[0].Trim() == "Back") Back = data[1].Trim();
                                    if (data[0].Trim() == "NameServer") NameServerIns = data[1].Trim();
                                    if (data[0].Trim() == "NameExe") NameFileExe = data[1].Trim();
                                }

                            }

                            if (NameFileExe != "")
                            {

                                NameBack = FolderBack[i].Substring(6, FolderBack[i].Length - 6);
                                UpdateKeyReestr(Back, "2");
                                if (Back == "BackFbd25OnOff") UpdateKeyReestr("InstBackOnOff", "BackFbd25OnOff");
                                if (Back == "BackFbd30OnOff") UpdateKeyReestr("InstBackOnOff", "BackFbd30OnOff");
                                UpdateKeyReestr(FolderBack[i] + "OnOff", "2");

                                TcpIpPortHub = File.ReadLines(PatchSRBack + "aptune.ini").First(x => x.StartsWith("DatabaseName")).Split('=')[1].Replace(" ", "");
                                UpdateKeyReestr("SetTextIp4BackOf", TcpIpPortHub.Substring(0, TcpIpPortHub.LastIndexOf(@"/")));
                                UpdateKeyReestr("BackOfAdresPortServer", TcpIpPortHub.Substring(TcpIpPortHub.LastIndexOf(@"/") + 1, TcpIpPortHub.LastIndexOf(":") - TcpIpPortHub.LastIndexOf(@"/") - 1));

                                StrCreate = "SELECT * FROM LOCKEY WHERE LOCKEY.PUTH = " + "'" + PatchSRBack + "'";
                                string NameServer = "";
                                Idcount = 0;
                                DBConecto.DBopenFBConectionMemory(SystemBD.ConnnStringSystem().ToString(), "", "FbSystem");
                                FbCommand SelectTable = new FbCommand(StrCreate, DBConecto.bdFbSystemConect);
                                FbDataReader ReadOutTable = SelectTable.ExecuteReader();
                                while (ReadOutTable.Read()) { Idcount++; }
                                ReadOutTable.Close();
                                if (Idcount != 0) FileKey = "LOCKEY";
                                if (Idcount == 0)
                                {
                                    StrCreate = "SELECT * FROM NETKEY WHERE NETKEY.PUTH = " + "'" + PatchSRBack + "'";
                                    FbCommand SelectNetKey = new FbCommand(StrCreate, DBConecto.bdFbSystemConect);
                                    FbDataReader ReadNetKey = SelectNetKey.ExecuteReader();
                                    while (ReadNetKey.Read()) { Idcount++; }
                                    ReadNetKey.Close();
                                    if (Idcount != 0) FileKey = "NETKEY";
                                    if (Idcount == 0)
                                    {
                                        StrCreate = "SELECT * FROM TESTKEY WHERE TESTKEY.PUTH = " + "'" + PatchSRBack + "'";
                                        FbCommand SelectTestKey = new FbCommand(StrCreate, DBConecto.bdFbSystemConect);
                                        FbDataReader ReadTestKey = SelectTestKey.ExecuteReader();
                                        while (ReadTestKey.Read()) { Idcount++; }
                                        ReadTestKey.Close();
                                        SelectTestKey.Dispose();
                                    }
                                    if (Idcount != 0) FileKey = "TESTKEY";
                                    SelectNetKey.Dispose();
                                }
                                SelectTable.Dispose();
                                DBConecto.DBcloseFBConectionMemory("FbSystem");
                                StrCreate = "select * from REESTRBACK where REESTRBACK.CONECT = " + "'" + TcpIpPortHub + "' AND REESTRBACK.PUTH ='" + NameFileExe + "'";
                                NameServer = "";
                                string UpdateBack = "";
                                Idcount = 0;
                                DBConecto.DBopenFBConectionMemory(SystemBD.ConnnStringSystem().ToString(), "", "FbSystem");
                                FbCommand SelectTableBack = new FbCommand(StrCreate, DBConecto.bdFbSystemConect);
                                SelectTableBack.CommandType = CommandType.Text;
                                FbDataReader ReadOutTableBack = SelectTableBack.ExecuteReader();
                                while (ReadOutTableBack.Read())
                                {
                                    NameServer = ReadOutTableBack[2].ToString();
                                    NameFileExe = ReadOutTableBack[3].ToString();
                                    Idcount++;
                                }
                                ReadOutTableBack.Close();

                                if (Idcount == 0) StrCreate = "INSERT INTO REESTRBACK  values ('" + NameBack + "','" + TcpIpPortHub + "'" + ", '" + NameServerIns + "', '" + NameFileExe + "', '" + FileKey + "', '" + UpdateBack + "')";
                                else StrCreate = "UPDATE REESTRBACK SET KEY = " + "'" + FileKey + "'" + " WHERE REESTRBACK.PUTH = " + "'" + NameFileExe + "'";
                                DBConecto.UniQuery ModifyKey = new DBConecto.UniQuery(StrCreate, "FB");
                                ModifyKey.ExecuteUNIScalar();


                                StrCreate = "select * from ACTIVBACKFRONT where ACTIVBACKFRONT.PUTH = " + "'" + NameFileExe + "'";
                                Idcount = 0;
                                FbCommand SelectTableActiv = new FbCommand(StrCreate, DBConecto.bdFbSystemConect);
                                FbDataReader ReadOutTableActiv = SelectTableActiv.ExecuteReader();
                                while (ReadOutTableActiv.Read()) { Idcount++; }
                                ReadOutTableActiv.Close();
                                if (Idcount == 0)
                                {
                                    string TcpIp = TcpIpPortHub.Substring(0, TcpIpPortHub.LastIndexOf(@"/"));
                                    string IpPort = TcpIpPortHub.Substring(TcpIpPortHub.LastIndexOf(@"/") + 1, (TcpIpPortHub.IndexOf(":") - TcpIpPortHub.LastIndexOf(@"/") - 1));
                                    StrCreate = "INSERT INTO ACTIVBACKFRONT  values ('Back','" + NameBack + "'" + ", '" + NameServerIns + "', '" + TcpIpPortHub + "', '" + IpPort + "', '" + TcpIp + "', '" + NameFileExe + "')";
                                    DBConecto.UniQuery InsertQuery = new DBConecto.UniQuery(StrCreate, "FB");
                                    InsertQuery.UserQuery = string.Format(StrCreate, "ACTIVBACKFRONT");
                                    InsertQuery.ExecuteUNIScalar();
                                }
                                SelectTable.Dispose();
                                SelectTableActiv.Dispose();
                                DBConecto.DBcloseFBConectionMemory("FbSystem");
                                LoadedGridBack("SELECT * from REESTRBACK");
                            }
                            else
                            {
                                UpdateKeyReestr(FolderBack[i] + "OnOff", "0");
                                ConectoWorkSpace.Administrator.AdminPanels ConectoWorkSpace_Of = AppStart.LinkMainWindow("WAdminPanels");
                                var pic = (Image)LogicalTreeHelper.FindLogicalNode(ConectoWorkSpace_Of, FolderBack[i] + "OnOff");
                                var uriSource = new Uri(@"/Conecto®%20WorkSpace;component/Images/" + "on_off_1.png", UriKind.Relative);
                                pic.Source = new BitmapImage(uriSource);
                            }

                        }
                    }
                    else
                    {
                        UpdateKeyReestr(FolderBack[i] + "OnOff", "0");
                        ConectoWorkSpace.Administrator.AdminPanels ConectoWorkSpace_Of = AppStart.LinkMainWindow("WAdminPanels");
                        var pic = (Image)LogicalTreeHelper.FindLogicalNode(ConectoWorkSpace_Of, FolderBack[i] + "OnOff");
                        var uriSource = new Uri(@"/Conecto®%20WorkSpace;component/Images/" + "on_off_1.png", UriKind.Relative);
                        pic.Source = new BitmapImage(uriSource);
                    }

                }
                for (int i = 0; i <= 5; i++)
                {

                    StrCreate = "select * from REESTRBACK where REESTRBACK.BACK = " + "'" + FolderFront[i] + "'";
                    string TempNameBack = "";
                    Idcount = 0;
                    DBConecto.DBopenFBConectionMemory(SystemBD.ConnnStringSystem().ToString(), "", "FbSystem");
                    FbCommand SelectTable = new FbCommand(StrCreate, DBConecto.bdFbSystemConect);
                    FbDataReader ReadOutTable = SelectTable.ExecuteReader();
                    while (ReadOutTable.Read())
                    {
                        TempNameBack = ReadOutTable[2].ToString();
                        Idcount = Idcount + 1;
                    }
                    ReadOutTable.Close();
                    if (Idcount != 0)
                    {
                        UpdateKeyReestr(FolderBack[i] + "OnOff", "2");
                        ConectoWorkSpace.Administrator.AdminPanels ConectoWorkSpace_Of = AppStart.LinkMainWindow("WAdminPanels");
                        var pic = (Image)LogicalTreeHelper.FindLogicalNode(ConectoWorkSpace_Of, FolderBack[i] + "OnOff");
                        var uriSource = new Uri(@"/Conecto®%20WorkSpace;component/Images/" + "on_off_2.png", UriKind.Relative);
                        pic.Source = new BitmapImage(uriSource);

                    }
                    SelectTable.Dispose();
                    DBConecto.DBcloseFBConectionMemory("FbSystem");
                }
            }





        }
        // Проверка установки Фронтофис
        public static void CheckInstalFront25()
        {
            string[] findFiles = new string[1];
            string[] FolderBack = { "FrontRozn", "FrontRestoran", "FrontFastFud", "FrontFitnes", "FrontHotel" };
            string[] FolderFront = { "Rozn", "Restoran", "FastFud", "Fitnes", "Hotel" };
            string[] AppUpdate = { "B52Fitness8.exe", "B52Hotel8.exe", "B52FrontOffice8.exe" };
            string PatchSRBack = "";
            //AnalizUpdate(AppUpdate);

            DBConecto.DBopenFBConectionMemory(SystemBD.ConnnStringSystem().ToString(), "", "FbSystem");
            string InsertExecute = "SELECT count(*) from REESTRFRONT";
            DBConecto.UniQuery CountQuery = new DBConecto.UniQuery(InsertExecute, "FB");
            string CountTable = CountQuery.ExecuteUNIScalar() == null ? "" : CountQuery.ExecuteUNIScalar().ToString();

            string[] ServerName = new string[Convert.ToUInt32(CountTable)];
            string[] NameExeFile = new string[Convert.ToUInt32(CountTable)];
            string StrCreate = "SELECT * from REESTRFRONT";
            int Idcount = -1;
            DBConecto.DBopenFBConectionMemory(SystemBD.ConnnStringSystem().ToString(), "", "FbSystem");
            FbCommand SelectTableFront = new FbCommand(StrCreate, DBConecto.bdFbSystemConect);
            FbDataReader ReadOutTableFront = SelectTableFront.ExecuteReader();
            while (ReadOutTableFront.Read())
            {
                Idcount++;
                ServerName[Idcount] = ReadOutTableFront[2].ToString();
                NameExeFile[Idcount] = ReadOutTableFront[3].ToString();

            }
            ReadOutTableFront.Close();
            DBConecto.DBcloseFBConectionMemory("FbSystem");


            if (Idcount >= 0)
            {
                for (int i = 0; i <= Idcount; i++)
                {
                    if (!File.Exists(NameExeFile[i]))
                    {
                        for (int ind = 0; ind <= 4; ind++)
                        {
                            if (NameExeFile[i].Contains(FolderBack[ind]))
                            {
                                UpdateKeyReestr(FolderFront[ind] + "ClickOnOff", "0");
                                ConectoWorkSpace.Administrator.AdminPanels ConectoWorkSpace_Of = AppStart.LinkMainWindow("WAdminPanels");
                                var pic = (Image)LogicalTreeHelper.FindLogicalNode(ConectoWorkSpace_Of, FolderFront[ind] + "ClickOnOff");
                                var uriSource = new Uri(@"/Conecto®%20WorkSpace;component/Images/" + "on_off_1.png", UriKind.Relative);
                                pic.Source = new BitmapImage(uriSource);
                                //Directory.Delete(NameExeFile[i].Substring(0,NameExeFile[i].LastIndexOf(@"\")-1), true);
                            }

                        }

                    }
                    PatchSRBack = NameExeFile[i].Substring(0, NameExeFile[i].LastIndexOf(@"\"));
                    UpdateKeyReestr(FolderFront[i] + "ClickOnOff", "2");
                    ConectoWorkSpace.Administrator.AdminPanels ConectoWorkSpace_On = AppStart.LinkMainWindow("WAdminPanels");
                    var picon = (Image)LogicalTreeHelper.FindLogicalNode(ConectoWorkSpace_On, FolderFront[i] + "ClickOnOff");
                    var uriSourceOn = new Uri(@"/Conecto®%20WorkSpace;component/Images/" + "on_off_2.png", UriKind.Relative);
                    picon.Source = new BitmapImage(uriSourceOn);

                }
                LoadedGridFront("SELECT * from REESTRFRONT");
            }
            else
            {
                StrCreate = ""; string NameFileExe = "";
                DriveInfo[] allDrives = DriveInfo.GetDrives();
                for (int i = 0; i <= 4; i++)
                {
                    PatchSRBack = "";
                    foreach (DriveInfo IdDevice in allDrives)
                    {
                        if (IdDevice.IsReady == true)
                        {

                            if (Directory.Exists(IdDevice.Name + FolderBack[i] + @"\bin\"))
                            {
                                if (File.Exists(IdDevice.Name + FolderBack[i] + @"\bin\aptune.ini"))
                                {
                                    PatchSRBack = IdDevice.Name + FolderBack[i] + @"\bin\";
                                    break;
                                }
                                else Directory.Delete(IdDevice.Name + FolderBack[i], true);
                            }

                        }

                    }
                    if (PatchSRBack != "")
                    {
                        string Back = "FrontFbd25OnOff", NameBack = "", TcpIpPortHub = "", FileKey = "", NameServerIns = "";
                        if (!File.Exists(PatchSRBack + "aptune.ini"))
                        {
                            UpdateKeyReestr(FolderFront[i] + "ClickOnOff", "0");
                            ConectoWorkSpace.Administrator.AdminPanels ConectoWorkSpace_Of = AppStart.LinkMainWindow("WAdminPanels");
                            var pic = (Image)LogicalTreeHelper.FindLogicalNode(ConectoWorkSpace_Of, FolderFront[i] + "ClickOnOff");
                            var uriSource = new Uri(@"/Conecto®%20WorkSpace;component/Images/" + "on_off_1.png", UriKind.Relative);
                            pic.Source = new BitmapImage(uriSource);
                        }
                        else
                        {
                            if (File.Exists(PatchSRBack + "conecto.ini"))
                            {
                                string[] fileLines = File.ReadAllLines(PatchSRBack + "conecto.ini");
                                IndRecno = 0;
                                foreach (string x in fileLines)
                                {
                                    if (x.Length == 0) break;
                                    string[] data = x.Split('=');
                                    if (data[0].Trim() == "Front") Back = data[1].Trim();
                                    if (data[0].Trim() == "NameServer") NameServerIns = data[1].Trim();
                                    if (data[0].Trim() == "NameExe") NameFileExe = data[1].Trim();

                                }

                            }

                            if (NameFileExe != "")
                            {

                                NameBack = FolderFront[i];
                                UpdateKeyReestr(Back, "2");

                                if (Back == "FrontFbd25OnOff") UpdateKeyReestr("InstFrontOnOff", "FrontFbd25OnOff");
                                if (Back == "FrontFbd30OnOff") UpdateKeyReestr("InstFrontOnOff", "FrontFbd30OnOff");

                                UpdateKeyReestr(FolderFront[i] + "ClickOnOff", "2");
                                TcpIpPortHub = File.ReadLines(PatchSRBack + "aptune.ini").First(x => x.StartsWith("DatabaseName")).Split('=')[1]; //.Replace(" ", "")
                                UpdateKeyReestr("FrontAdrIp4", TcpIpPortHub.Substring(0, TcpIpPortHub.LastIndexOf(@"/")));
                                UpdateKeyReestr("AdresPortServer", TcpIpPortHub.Substring(TcpIpPortHub.LastIndexOf(@"/") + 1, TcpIpPortHub.LastIndexOf(":") - TcpIpPortHub.LastIndexOf(@"/") - 1));

                                StrCreate = "SELECT * FROM LOCKEY WHERE LOCKEY.PUTH = " + "'" + PatchSRBack + "'";
                                string NameServer = "";
                                Idcount = 0;
                                DBConecto.DBopenFBConectionMemory(SystemBD.ConnnStringSystem().ToString(), "", "FbSystem");
                                FbCommand SelectTable = new FbCommand(StrCreate, DBConecto.bdFbSystemConect);
                                FbDataReader ReadOutTable = SelectTable.ExecuteReader();
                                while (ReadOutTable.Read())
                                {
                                    NameServer = ReadOutTable[2].ToString();
                                    Idcount++;
                                }
                                ReadOutTable.Close();
                                if (Idcount != 0) FileKey = "LOCKEY";
                                if (Idcount == 0)
                                {
                                    StrCreate = "SELECT * FROM NETKEY WHERE NETKEY.PUTH = " + "'" + PatchSRBack + "'";
                                    FbCommand SelectNetKey = new FbCommand(StrCreate, DBConecto.bdFbSystemConect);
                                    FbDataReader ReadNetKey = SelectNetKey.ExecuteReader();
                                    while (ReadNetKey.Read())
                                    {
                                        NameServer = ReadNetKey[2].ToString();
                                        Idcount++;
                                    }
                                    ReadNetKey.Close();
                                    if (Idcount != 0) FileKey = "NETKEY";
                                    if (Idcount == 0)
                                    {
                                        StrCreate = "SELECT * FROM TESTKEY WHERE TESTKEY.PUTH = " + "'" + PatchSRBack + "'";
                                        FbCommand SelectTestKey = new FbCommand(StrCreate, DBConecto.bdFbSystemConect);
                                        FbDataReader ReadTestKey = SelectTestKey.ExecuteReader();
                                        while (ReadTestKey.Read())
                                        {
                                            NameServer = ReadTestKey[2].ToString();
                                            Idcount++;
                                        }
                                        if (Idcount != 0) FileKey = "TESTKEY";
                                        ReadTestKey.Close();
                                        SelectTestKey.Dispose();
                                    }
                                    SelectNetKey.Dispose();
                                }
                                SelectTable.Dispose();
                                DBConecto.DBcloseFBConectionMemory("FbSystem");


                                StrCreate = "select * from REESTRFRONT where REESTRFRONT.CONECT = " + "'" + TcpIpPortHub + "' AND REESTRFRONT.PUTH ='" + NameFileExe + "'"; // 
                                NameServer = "";
                                string UpdateBack = "";
                                Idcount = 0;
                                DBConecto.DBopenFBConectionMemory(SystemBD.ConnnStringSystem().ToString(), "", "FbSystem");
                                FbCommand TableFront = new FbCommand(StrCreate, DBConecto.bdFbSystemConect);
                                FbDataReader ReadTableFront = TableFront.ExecuteReader();
                                while (ReadTableFront.Read())
                                {
                                    NameServer = ReadOutTableFront[2].ToString();
                                    NameFileExe = ReadOutTableFront[3].ToString();
                                    Idcount++;
                                }
                                ReadTableFront.Close();
                                if (Idcount == 0) StrCreate = "INSERT INTO REESTRFRONT  values ('" + NameBack + "','" + TcpIpPortHub + "'" + ", '" + NameServerIns + "', '" + NameFileExe + "', '" + FileKey + "', '" + UpdateBack + "')";
                                else StrCreate = "UPDATE REESTRFRONT SET KEY = " + "'" + FileKey + "'" + " WHERE REESTRFRONT.PUTH = " + "'" + NameFileExe + "'";
                                DBConecto.UniQuery ModifyFront = new DBConecto.UniQuery(StrCreate, "FB");
                                ModifyFront.ExecuteUNIScalar();

                                StrCreate = "select * from ACTIVBACKFRONT where ACTIVBACKFRONT.PUTH = " + "'" + NameFileExe + "'";
                                Idcount = 0;
                                FbCommand SelectTableActiv = new FbCommand(StrCreate, DBConecto.bdFbSystemConect);
                                FbDataReader ReadOutTableActiv = SelectTableActiv.ExecuteReader();
                                while (ReadOutTableActiv.Read()) { Idcount++; }
                                ReadOutTableActiv.Close();
                                if (Idcount == 0)
                                {
                                    string TcpIp = TcpIpPortHub.Substring(0, TcpIpPortHub.LastIndexOf(@"/"));
                                    string IpPort = TcpIpPortHub.Substring(TcpIpPortHub.LastIndexOf(@"/") + 1, TcpIpPortHub.IndexOf(":") - TcpIpPortHub.LastIndexOf(@"/") - 1);
                                    StrCreate = "INSERT INTO ACTIVBACKFRONT  values ('Front','" + NameBack + "'" + ", '" + NameServerIns + "', '" + TcpIpPortHub + "', '" + IpPort + "', '" + TcpIp + "', '" + NameFileExe + "')";
                                    DBConecto.UniQuery InsertQuery = new DBConecto.UniQuery(StrCreate, "FB");
                                    InsertQuery.UserQuery = string.Format(StrCreate, "ACTIVBACKFRONT");
                                    InsertQuery.ExecuteUNIScalar();
                                }

                                SelectTableFront.Dispose();
                                SelectTableActiv.Dispose();
                                DBConecto.DBcloseFBConectionMemory("FbSystem");
                                LoadedGridFront("SELECT * from REESTRFRONT");
                            }
                            else
                            {
                                SystemConecto.ErorDebag("Имя фронтофиса не указано в conecto.ini. NameExe= '' ");
                                UpdateKeyReestr(FolderFront[i] + "ClickOnOff", "0");
                                ConectoWorkSpace.Administrator.AdminPanels ConectoWorkSpace_Of = AppStart.LinkMainWindow("WAdminPanels");
                                var pic = (Image)LogicalTreeHelper.FindLogicalNode(ConectoWorkSpace_Of, FolderFront[i] + "ClickOnOff");
                                var uriSource = new Uri(@"/Conecto®%20WorkSpace;component/Images/" + "on_off_1.png", UriKind.Relative);
                                pic.Source = new BitmapImage(uriSource);

                            }


                        }


                    }
                    else
                    {
                        UpdateKeyReestr(FolderFront[i] + "ClickOnOff", "0");
                        ConectoWorkSpace.Administrator.AdminPanels ConectoWorkSpace_Of = AppStart.LinkMainWindow("WAdminPanels");
                        var pic = (Image)LogicalTreeHelper.FindLogicalNode(ConectoWorkSpace_Of, FolderFront[i] + "ClickOnOff");
                        var uriSource = new Uri(@"/Conecto®%20WorkSpace;component/Images/" + "on_off_1.png", UriKind.Relative);
                        pic.Source = new BitmapImage(uriSource);
                    }
                }
            }



            for (int i = 0; i <= 4; i++)
            {
                StrCreate = "select * from REESTRFRONT where REESTRFRONT.FRONT = " + "'" + FolderFront[i] + "'";
                string TempNameBack = "";
                Idcount = 0;
                DBConecto.DBopenFBConectionMemory(SystemBD.ConnnStringSystem().ToString(), "", "FbSystem");
                FbCommand SelectTable = new FbCommand(StrCreate, DBConecto.bdFbSystemConect);
                FbDataReader ReadOutTable = SelectTable.ExecuteReader();
                while (ReadOutTable.Read())
                {
                    TempNameBack = ReadOutTable[2].ToString();
                    Idcount = Idcount + 1;
                }
                ReadOutTable.Close();
                if (Idcount != 0)
                {
                    UpdateKeyReestr(FolderFront[i] + "ClickOnOff", "2");
                    ConectoWorkSpace.Administrator.AdminPanels ConectoWorkSpace_Of = AppStart.LinkMainWindow("WAdminPanels");
                    var pic = (Image)LogicalTreeHelper.FindLogicalNode(ConectoWorkSpace_Of, FolderFront[i] + "ClickOnOff");
                    var uriSource = new Uri(@"/Conecto®%20WorkSpace;component/Images/" + "on_off_2.png", UriKind.Relative);
                    pic.Source = new BitmapImage(uriSource);

                }
                SelectTable.Dispose();
                DBConecto.DBcloseFBConectionMemory("FbSystem");
            }
        }

        // Процедура загрузки текущих алиасов взаимодействующих с сервером по умолчанию для PostGreSQL
        public static void LoadAliasPG(string SetServer, string PuthServer)
        {
 
            ShortDate = "";

        }

        // Процедура загрузки текущих алиасов взаимодействующих с сервером по умолчанию для firebird 30
        public static void LoadAlias30(string SetServer, string PuthServer)
        {
            int Idcount = 0; string StrCreate = "";
            ShortDate = AppStart.rkAppSetingAllUser.GetValue("UpdateB52").ToString();
            string[] fileLines = File.ReadAllLines(PuthServer + "databases.conf");
            foreach (string x in fileLines)
            {

                if (x.Contains("=") == true && x.StartsWith("#") == false && x.Contains("$") == false && x.Contains("employee") == false && x.Length != 0)
                {
                    string[] data = x.Split('=');
                    if (!File.Exists(data[1].Trim().Substring(0, data[1].Length - 1))) fileLines[Idcount] = String.Empty;
                }
                Idcount++;

            }
            File.WriteAllLines(PuthServer + "databases.conf", fileLines);


            if (File.Exists(PuthServer + "databases.conf"))
            {
                fileLines = File.ReadAllLines(PuthServer + "databases.conf"); //AppStart.TableReestr["ServerDefault25"]
                IndRecno = 0;
                foreach (string x in fileLines)
                {
                    if (x.Contains("=") == true && x.StartsWith("#") == false && x.Contains("$") == false && x.Contains("employee") == false)
                    {
                        string[] data = x.Split('=');
                        if (data[1].Contains("sec") == false)
                        {
                            if (data[1].ToUpper().Contains(".FDB") == true || data[1].ToUpper().Contains(".GDB") == true)
                            {
                                string SetAlias = data[0].Trim();
                                string SetPuth = data[1].Trim().Substring(0, data[1].Length - 1);
                                if (AppStart.TableReestr["PuthSetBD30"] == "" && SetServer != "No activ")
                                {
                                    UpdateKeyReestr("PuthSetBD30", SetPuth);
                                }
                                if (AppStart.TableReestr["PuthSetBD30"] != "")
                                {
                                    System.Windows.Application.Current.Dispatcher.Invoke(new Action(delegate ()
                                    {
                                        ConectoWorkSpace.Administrator.AdminPanels ConectoWorkSpace_InW = AppStart.LinkMainWindow("WAdminPanels");
                                        ConectoWorkSpace_InW.PuthSetBD30.Text = AppStart.TableReestr["PuthSetBD30"];
                                        ConectoWorkSpace_InW.DefNameServer30.Text = SetServer;
                                        Interface.CurrentStateInst("SetPuthBD30", "2", "on_off_2.png", ConectoWorkSpace_InW.SetPuthBD30);
                                    }));
                                }
                                StrCreate = "select * from CONNECTIONBD30 where CONNECTIONBD30.PUTHBD = " + "'" + SetPuth + "' AND CONNECTIONBD30.ALIASBD ='" + SetAlias + "' AND CONNECTIONBD30.NAMESERVER ='" + SetServer + "'";
                                string Strcount = DateTime.Now.ToString("MMddHHmmsstt");
                                string StrInsert = "INSERT INTO CONNECTIONBD30  values (";
                                string Conect = "CONNECTIONBD30";
                                string Log = "", Satellite = "";
                                InsertDataGrid(StrCreate, Strcount, StrInsert, Conect, SetPuth, SetAlias, SetServer, PuthServer, ShortDate, Log, Satellite);
                                IndRecno++;
                            }
                        }
                    }
                }

            }
            if (IndRecno == 0)
            {
                UpdateKeyReestr("PuthSetBD30", "");
                System.Windows.Application.Current.Dispatcher.Invoke(new Action(delegate ()
                {
                    ConectoWorkSpace.Administrator.AdminPanels ConectoWorkSpace_InW = AppStart.LinkMainWindow("WAdminPanels");
                    ConectoWorkSpace_InW.PuthSetBD30.Text = "";
                    Interface.CurrentStateInst("SetPuthBD30", "0", "on_off_1.png", ConectoWorkSpace_InW.SetPuthBD30);
                }));
            }
            Grid_Puth("SELECT * from CONNECTIONBD30 WHERE CONNECTIONBD30.NAMESERVER =" + "'" + SetServer + "'");
        }

        // Процедура загрузки текущих алиасов взаимодействующих с сервером по умолчанию для firebird 25
        public static void LoadAlias(string SetServer, string PuthServer)
        {
            int Idcount = 0; string StrCreate = "";
            ShortDate = AppStart.rkAppSetingAllUser.GetValue("UpdateB52").ToString();
            if (File.Exists(PuthServer + "aliases.conf"))
            {

                // Удаление строки несуществующих БД  из файла
                Idcount = 0;
                string[] fileLines = File.ReadAllLines(PuthServer + "aliases.conf");
                foreach (string x in fileLines)
                {

                    if (x.Contains("=") == true && x.StartsWith("#") == false && x.Contains("$") == false && x.Contains("employee") == false && x.Length != 0)
                    {
                        string[] data = x.Split('=');
                        if (!File.Exists(data[1].Trim().Substring(0, data[1].Length - 1))) fileLines[Idcount] = String.Empty;

                    }
                    Idcount++;

                }
                File.WriteAllLines(PuthServer + "aliases.conf", fileLines);



                fileLines = File.ReadAllLines(PuthServer + "aliases.conf");

                foreach (string x in fileLines)
                {
                    if (x.Contains("=") == true && x.StartsWith("#") == false && x.Contains("$") == false && x.Contains("employee") == false)
                    {
                        string[] data = x.Split('=');
                        if (data[1].Contains("sec") == false)
                        {
                            if (data[1].ToUpper().Contains(".FDB") == true || data[1].ToUpper().Contains(".GDB") == true)
                            {
                                string SetAlias = data[0].Trim();
                                string SetPuth = data[1].Trim().Substring(0, data[1].Length - 1);
                                if (AppStart.TableReestr["PuthSetBD25"] == "" && SetServer != "No activ")
                                {
                                    UpdateKeyReestr("PuthSetBD25", SetPuth);
                                }
                                System.Windows.Application.Current.Dispatcher.Invoke(new Action(delegate ()
                                {
                                    ConectoWorkSpace.Administrator.AdminPanels ConectoWorkSpace_InW = AppStart.LinkMainWindow("WAdminPanels");
                                    if (ConectoWorkSpace_InW.PuthSetBD25.Text == "")
                                    {
                                        ConectoWorkSpace_InW.PuthSetBD25.Text = AppStart.TableReestr["PuthSetBD25"];
                                        ConectoWorkSpace_InW.DefNameServer25.Text = SetServer;
                                        Interface.CurrentStateInst("SetPuthBD25", "2", "on_off_2.png", ConectoWorkSpace_InW.SetPuthBD25);
                                    }
                                }));

                                StrCreate = "select * from CONNECTIONBD25 where CONNECTIONBD25.PUTHBD = " + "'" + SetPuth + "' AND CONNECTIONBD25.ALIASBD ='" + SetAlias + "' AND CONNECTIONBD25.NAMESERVER ='" + SetServer + "'";
                                string Strcount = DateTime.Now.ToString("MMddHHmmsstt");
                                string StrInsert = "INSERT INTO CONNECTIONBD25  values (";
                                string Conect = "CONNECTIONBD25";
                                string Log = "", Satellite = "";
                                InsertDataGrid(StrCreate, Strcount, StrInsert, Conect, SetPuth, SetAlias, SetServer, PuthServer, ShortDate, Log, Satellite);
                                IndRecno++;
                            }
                        }
                    }
                }
            }

            if (IndRecno == 0)
            {
                UpdateKeyReestr("PuthSetBD25", "");
                System.Windows.Application.Current.Dispatcher.Invoke(new Action(delegate ()
                {
                    ConectoWorkSpace.Administrator.AdminPanels ConectoWorkSpace_InW = AppStart.LinkMainWindow("WAdminPanels");
                    ConectoWorkSpace_InW.PuthSetBD25.Text = "";
                    Interface.CurrentStateInst("SetPuthBD25", "0", "on_off_1.png", ConectoWorkSpace_InW.SetPuthBD25);
                }));
            }
            Grid_Puth("SELECT * from CONNECTIONBD25 WHERE CONNECTIONBD25.NAMESERVER =" + "'" + SetServer + "'");
        }
        // Процедура обнаружения активных серверов Firebird
        public static void ScanActivFirebird(string proces = "")
        {
            for (int id = 0; id <= 9; id++) { NamePuth[id] = ""; NameServer[id] = ""; }
            IndexActivProces = -1;
            string Fb = Inst2530 == "grdsrv" ? Inst2530 : (Inst2530 == "25" ? "fbserver" :(Inst2530 == "30" ?"firebird" : "postgres" ));
            Fb = proces == "" ? Fb : proces;
            var wmiQueryString = "SELECT ProcessId, ExecutablePath, CommandLine FROM Win32_Process";
            using (var searcher = new ManagementObjectSearcher(wmiQueryString))
            using (var results = searcher.Get())
            {
                var query = from p in Process.GetProcessesByName(Fb)
                            join mo in results.Cast<ManagementObject>()
                            on p.Id equals (int)(uint)mo["ProcessId"]
                            select new
                            {
                                Process = p,
                                Path = (string)mo["ExecutablePath"],
                                CommandLine = (string)mo["CommandLine"],
                            };
                foreach (var item in query)
                {
                    IndexActivProces++;
                    NamePuth[IndexActivProces] = item.Path;
                    NameServer[IndexActivProces] = item.CommandLine;

                }
            }
        }

        public static void SyncProcessFolder(string file, int indPoint)
        {
            ActivServer = "No activ";
            if (IndexActivProces >= 0)
            {

                for (int id = 0; id <= IndexActivProces; id++)
                {

                    string PuthProcess = NamePuth[id].ToUpper().Replace(" ", ""), PuthFiles = file.ToUpper().Replace(" ", "");
                    if (PuthProcess == PuthFiles) ActivServer = NameServer[id].Substring(NameServer[id].IndexOf("-s") + 2, NameServer[id].Length - (NameServer[id].IndexOf("-s") + 2)).Replace(" ", "");
                }
            }

            IdPort[indPoint] = Inst2530 == "25" ? "3050" : "3056";
            string DefaultDbCachePages = "";
            //PuthServer = "c" + (Inst2530 == "25" ? file.Substring(1, file.IndexOf(@"\bin")) : file.Substring(1, file.IndexOf(@"\firebird.exe")));
            PuthServer = Inst2530 == "25" ? file.Substring(1, file.IndexOf(@"\bin")) : file.Substring(1, file.IndexOf(@"\firebird.exe"));

            if (!File.Exists(PuthServer + "aliases.conf") && Inst2530 == "25")
            {
                ActivServer = "";
                return;
            }
            if (!File.Exists(PuthServer + "databases.conf") && Inst2530 == "30")
            {
                ActivServer = "";
                return;
            }
            if (File.Exists(PuthServer + "firebird.conf"))
            {
                foreach (string strk in File.ReadLines(PuthServer + "firebird.conf"))
                {
                    if (strk.StartsWith("RemoteServicePort") == true)
                    {
                        IdPort[indPoint] = File.ReadLines(PuthServer + "firebird.conf").First(xx => xx.StartsWith("RemoteServicePort")).Split('=')[1].Replace(" ", "");
                    }
                    if (Inst2530 == "25") if (strk.StartsWith("DefaultDbCachePages = 9999") == true) DefaultDbCachePages = "9999";
                }
                if (DefaultDbCachePages == "" && Inst2530 == "25")
                {
                    if (ActivServer != "Stop") InstallB52.RemoveServerFB25(PuthServer, ActivServer);
                    var GchangeConf25 = ConectoWorkSpace.INI.ReadFile(PuthServer + "firebird.conf");
                    GchangeConf25.Set("DefaultDbCachePages", "9999", true);
                    GchangeConf25.Set("TempBlockSize", "2048576", true);
                    GchangeConf25.Set("TempCacheLimit", SystemConecto.OS64Bit == true ? "967108864" : "367108864", true);
                    GchangeConf25.Set("LockHashSlots", SystemConecto.OS64Bit == true ? "20011" : "10011", true);
                    GchangeConf25.Set("CpuAffinityMask", "255", true);
                    GchangeConf25.Set("OldSetClauseSemantics", "1", true);
                    if (ActivServer != "No activ") InstallB52.RestartFB25(PuthServer, ActivServer);
                }
            }
            else
            {
                ActivServer = "";
                return;
            }


            int Idcount = 0;
            DBConecto.DBopenFBConectionMemory(SystemBD.ConnnStringSystem().ToString(), "", "FbSystem");
            string StrCreate = (Inst2530 == "25" ? "select * from SERVERACTIVFB25 where SERVERACTIVFB25.PUTH = " : "select * from SERVERACTIVFB30 where SERVERACTIVFB30.PUTH = ") + "'" + PuthServer + "'";
            FbCommand SelectTable = new FbCommand(StrCreate, DBConecto.bdFbSystemConect);
            SelectTable.CommandTimeout = 3;
            SelectTable.CommandType = CommandType.Text;
            FbDataReader ReadOutTable = SelectTable.ExecuteReader();
            TableKey.Add(new string[3]);
            while (ReadOutTable.Read())
            {
                TableKey[TableKey.Count - 1][0] = ReadOutTable[0].ToString();
                Idcount = Idcount + 1;
            }
            ReadOutTable.Close();
            if (Idcount == 0)
            {
                string DateCreatFB = (string)AppStart.rkAppSetingAllUser.GetValue("StartSystemFB");
                StrCreate = (Inst2530 == "25" ? "INSERT INTO SERVERACTIVFB25  values (" : "INSERT INTO SERVERACTIVFB30  values (") + IdPort[indPoint] + ",'" + PuthServer + "'" + ", '" + ActivServer + "', '" + DateCreatFB + "')";
                DBConecto.UniQuery InsertQuery = new DBConecto.UniQuery(StrCreate, "FB");
                InsertQuery.UserQuery = string.Format(StrCreate, "SERVERACTIVFB25");
                InsertQuery.ExecuteUNIScalar();
                if (AppStart.TableReestr[Inst2530 == "25" ? "ServerDefault25" : "ServerDefault30"] == "")
                {
                    AppStart.TableReestr[Inst2530 == "25" ? "ServerDefault25" : "ServerDefault30"] = PuthServer;
                    UpdateKeyReestr(Inst2530 == "25" ? "ServFB25OnOff" : "ServFB30OnOff", "2");
                    UpdateKeyReestr(Inst2530 == "25" ? "ServerDefault25" : "ServerDefault30", PuthServer);
                    UpdateKeyReestr(Inst2530 == "25" ? "NameServer25" : "NameServer30", ActivServer);
                    UpdateKeyReestr("AdresPortServer", IdPort[indPoint]);
                    UpdateKeyReestr("BackOfAdresPortServer", IdPort[indPoint]);
                    UpdateKeyReestr(Inst2530 == "25" ? "SetPortServer25" : "SetPortServer30", IdPort[indPoint]);
                }

                System.Windows.Application.Current.Dispatcher.Invoke(new Action(delegate ()
                {
                    ConectoWorkSpace.Administrator.AdminPanels ConectoWorkSpace_InW = AppStart.LinkMainWindow("WAdminPanels");
                    if (Inst2530 == "25") ConectoWorkSpace_InW.PuthSetServer25.Text = AppStart.TableReestr["ServerDefault25"];
                    if (Inst2530 == "30") ConectoWorkSpace_InW.PuthSetServer30.Text = AppStart.TableReestr["ServerDefault30"];
                    Interface.CurrentStateInst(Inst2530 == "25" ? "ServFB25OnOff" : "ServFB30OnOff", "2", "on_off_2.png", Inst2530 == "25" ? ConectoWorkSpace_InW.ServFB25OnOff : ConectoWorkSpace_InW.ServFB30OnOff);
                }));

            }
            SelectTable.Dispose();
            DBConecto.DBcloseFBConectionMemory("FbSystem");
        }

        // Процедура поиска установленных серверов Fire Bird 2.5, 3.0
        public void SearchInstFB()
        {
            //if (AppStart.StartReshenie != 0) return;
            // Проверка проверки наличия установленных сереверов  в папках в виде файлов exe Firebird_3_0
            // Проверяем наличие установленных серверов в таблице активных серверов
            string[] findFiles = new string[1];
            Inst2530 = "25";
            int Idcount = -1;
            string CurrentIdPort = "";
            string StrCreate = "";
            DBConecto.DBopenFBConectionMemory(SystemBD.ConnnStringSystem().ToString(), "", "FbSystem");
            string InsertExecute = "SELECT count(*) from SERVERACTIVFB25";
            DBConecto.UniQuery CountQuery = new DBConecto.UniQuery(InsertExecute, "FB");
            CountServer25 = CountQuery.ExecuteUNIScalar() == null ? 0 : Convert.ToInt16(CountQuery.ExecuteUNIScalar().ToString());
            if (CountServer25 != 0)
            {
                ServerName25 = new string[CountServer25];
                NameExeFile25 = new string[CountServer25];
                PortServer25 = new string[CountServer25];
                CurrentPassw25 = new string[CountServer25];

                StrCreate = "SELECT * from SERVERACTIVFB25";

                DBConecto.DBopenFBConectionMemory(SystemBD.ConnnStringSystem().ToString(), "", "FbSystem");
                FbCommand SelectTableFront = new FbCommand(StrCreate, DBConecto.bdFbSystemConect);
                FbDataReader ReadOutTableFront = SelectTableFront.ExecuteReader();
                while (ReadOutTableFront.Read())
                {
                    Idcount++;
                    PortServer25[Idcount] = ReadOutTableFront[0].ToString();
                    NameExeFile25[Idcount] = ReadOutTableFront[1].ToString();
                    ServerName25[Idcount] = ReadOutTableFront[2].ToString();
                    CurrentPassw25[Idcount] = ReadOutTableFront[5].ToString();

                }
                ReadOutTableFront.Close();

                for (int i = 0; i <= Idcount; i++)
                {
 
                    if (CurrentPassw25[i] == "" || CurrentPassw25[i] == null)
                    {
                        
                        string StrUpdate = "UPDATE SERVERACTIVFB25 SET CURRENTPASSW = " + "'" + AppStart.TableReestr["CurrentPasswABD25"] + "'" + " WHERE SERVERACTIVFB25.PUTH = " + "'" + NameExeFile25[i] + "'";
                        MessageBox.Show(StrUpdate);
                        DBConecto.UniQuery InsertQuery = new DBConecto.UniQuery(StrUpdate, "FB");
                        InsertQuery.ExecuteUNIScalar();
                    }
                }
                DBConecto.DBcloseFBConectionMemory("FbSystem");
                for (int i = 0; i <= Idcount; i++)
                {
                    if (!File.Exists(NameExeFile25[i] + @"bin\fbserver.exe"))
                    {
                        DBConecto.DBopenFBConectionMemory(SystemBD.ConnnStringSystem().ToString(), "", "FbSystem");
                        StrCreate = "DELETE from SERVERACTIVFB25 WHERE SERVERACTIVFB25.PUTH = " + "'" + NameExeFile25[i] + "'";
                        DBConecto.UniQuery DeletQuery = new DBConecto.UniQuery(StrCreate, "FB");
                        DeletQuery.UserQuery = string.Format(StrCreate, "SERVERACTIVFB25");
                        DeletQuery.ExecuteUNIScalar();

                        StrCreate = "DELETE from CONNECTIONBD25 WHERE CONNECTIONBD25.NAMESERVER = " + "'" + ServerName25[i] + "'";
                        DeletQuery = new DBConecto.UniQuery(StrCreate, "FB");
                        DeletQuery.UserQuery = string.Format(StrCreate, "CONNECTIONBD25");
                        DeletQuery.ExecuteUNIScalar();
                        DBConecto.DBcloseFBConectionMemory("FbSystem");


                    }
                    else
                    {
                        if (AppStart.TableReestr["ServerDefault25"] == "")
                        {
                            UpdateKeyReestr("ServerDefault25", NameExeFile25[i]);
                            UpdateKeyReestr("NameServer25", ServerName25[i]);
                        }


                        string PuthProcess = "";
                        ScanActivFirebird();
 
                        if (IndexActivProces >= 0)
                        {
 
                            for (int id = 0; id <= IndexActivProces; id++)
                            {

                                ActivServer = NameServer[id].Substring(NameServer[id].IndexOf("-s") + 2, NameServer[id].Length - (NameServer[id].IndexOf("-s") + 2)).Trim();
                                if (ServerName25[i] == ActivServer) PuthProcess = NamePuth[id].ToUpper().Trim();
                            }
                            if (File.Exists(NameExeFile25[i] + "firebird.conf"))
                            {
                                foreach (string strk in File.ReadLines(NameExeFile25[i] + "firebird.conf"))
                                {
                                    if (strk.StartsWith("RemoteServicePort") == true)
                                    {
                                        CurrentIdPort = File.ReadLines(NameExeFile25[i] + "firebird.conf").First(xx => xx.StartsWith("RemoteServicePort")).Split('=')[1].Trim();
                                    }
                                }
                            }
                            if (CurrentIdPort != PortServer25[i])
                            {
                                string StrUpdate = "UPDATE SERVERACTIVFB25 SET PORT = " + "'" + CurrentIdPort + "'" + " WHERE SERVERACTIVFB25.PUTH = " + "'" + NameExeFile25[i] + "'";
                                DBConecto.UniQuery InsertQuery = new DBConecto.UniQuery(StrUpdate, "FB");
                                InsertQuery.ExecuteUNIScalar();
                                InstallB52.RestartFB25(PuthServer, ActivServer);
                            }

                        }
          
                        if (PuthProcess == "") StrCreate = "UPDATE SERVERACTIVFB25 SET ACTIVONOFF = 'Stop' WHERE SERVERACTIVFB25.PUTH = " + "'" + NameExeFile25[i] + "'";
                        else StrCreate = "UPDATE SERVERACTIVFB25 SET ACTIVONOFF = 'Activ' WHERE SERVERACTIVFB25.PUTH = " + "'" + NameExeFile25[i] + "'";
    
                        DBConecto.DBopenFBConectionMemory(SystemBD.ConnnStringSystem().ToString(), "", "FbSystem");
                        DBConecto.UniQuery UpdateQuery = new DBConecto.UniQuery(StrCreate, "FB");
                        UpdateQuery.ExecuteUNIScalar();

                        LoadAlias(ServerName25[i], NameExeFile25[i]);
                    }

                }

            }
            else
            {
                Idcount = -1;
                // Проверяем активные процессы
                ScanActivFirebird();
                findFiles = System.IO.Directory.GetFiles(@"C:\Program Files", "fbserver.exe", System.IO.SearchOption.AllDirectories);
                if (findFiles.Length != 0)
                {
                    foreach (string file in findFiles)
                    {
                        ActivServer = "No activ"; Idcount = 0;
                        if (IndexActivProces >= 0)
                        {

                            for (int id = 0; id <= IndexActivProces; id++)
                            {
                                string PuthProcess = NamePuth[id].ToUpper().Replace(" ", ""), PuthFiles = file.ToUpper().Replace(" ", "");
                                if (PuthProcess == PuthFiles) ActivServer = NameServer[id].Substring(NameServer[id].IndexOf("-s") + 2, NameServer[id].Length - (NameServer[id].IndexOf("-s") + 2)).Replace(" ", "");
                            }
                        }
                        //PuthServer = "c" + file.Substring(1, file.IndexOf("bin")-1);
                        if (file.IndexOf("bin") > 0)
                        {
                            PuthServer = file.Substring(0, file.IndexOf("bin")); // -1
                                                                                 // с буквой с после функции GetFiles это круто
                        }
                        else
                        {

                            PuthServer = System.IO.Path.GetFullPath(file) + @"\";
                        }

                        if (File.Exists(PuthServer + "firebird.conf"))
                        {
                            foreach (string strk in File.ReadLines(PuthServer + "firebird.conf"))
                            {
                                if (strk.StartsWith("RemoteServicePort") == true)
                                {
                                    CurrentIdPort = File.ReadLines(PuthServer + "firebird.conf").First(xx => xx.StartsWith("RemoteServicePort")).Split('=')[1].Replace(" ", "");
                                }
                            }
                        }
                        CurrentIdPort = CurrentIdPort == "" ? "3050" : CurrentIdPort;
                        string IdPort = "";
                        StrCreate = "SELECT * FROM SERVERACTIVFB25 where SERVERACTIVFB25.PUTH = " + "'" + PuthServer + "' AND SERVERACTIVFB25.NAME = '" + ActivServer + "'";
                        DBConecto.DBopenFBConectionMemory(SystemBD.ConnnStringSystem().ToString(), "", "FbSystem");
                        FbCommand SelectTableFront = new FbCommand(StrCreate, DBConecto.bdFbSystemConect);
                        FbDataReader ReadOutTableFront = SelectTableFront.ExecuteReader();
                        while (ReadOutTableFront.Read()) { IdPort = ReadOutTableFront[0].ToString(); Idcount++; }
                        ReadOutTableFront.Close();
                        SelectTableFront.Dispose();
                        if (Idcount == 0)
                        {

                            string DateCreatFB = DateTime.Now.ToString("yyyyMMddHHmm");
                            StrCreate = "INSERT INTO SERVERACTIVFB25  values ('" + CurrentIdPort + "','" + PuthServer + "', '" + ActivServer + "', '" + DateCreatFB + "','Activ','masterkey')";
                            DBConecto.UniQuery InsertQuery = new DBConecto.UniQuery(StrCreate, "FB");
                            InsertQuery.UserQuery = string.Format(StrCreate, "SERVERACTIVFB25");
                            InsertQuery.ExecuteUNIScalar();
                            if (AppStart.TableReestr["ServerDefault25"] == "")
                            {
                                ConectoWorkSpace.Administrator.AdminPanels.UpdateKeyReestr("ServerDefault25", PuthServer);
                                ConectoWorkSpace.Administrator.AdminPanels.UpdateKeyReestr("NameServer25", ActivServer);
                            }
                        }
                        else
                        {
                            if (IdPort != CurrentIdPort)
                            {
                                string StrUpdate = "UPDATE SERVERACTIVFB25 SET PORT = " + "'" + CurrentIdPort + "'" + " WHERE SERVERACTIVFB25.PUTH = " + "'" + PuthServer + "'";
                                DBConecto.UniQuery InsertQuery = new DBConecto.UniQuery(StrUpdate, "FB");
                                InsertQuery.ExecuteUNIScalar();
                                InstallB52.RestartFB25(PuthServer, ActivServer);
                            }
                        }
                        LoadAlias(ActivServer, PuthServer);

                    }

                    DBConecto.DBopenFBConectionMemory(SystemBD.ConnnStringSystem().ToString(), "", "FbSystem");
                    InsertExecute = "SELECT count(*) from SERVERACTIVFB25";
                    CountQuery = new DBConecto.UniQuery(InsertExecute, "FB");
                    CountServer25 = CountQuery.ExecuteUNIScalar() == null ? 0 : Convert.ToInt16(CountQuery.ExecuteUNIScalar().ToString());
                    if (CountServer25 == 1)
                    {
                        ConectoWorkSpace.Administrator.AdminPanels.UpdateKeyReestr("ServerDefault25", PuthServer);
                        ConectoWorkSpace.Administrator.AdminPanels.UpdateKeyReestr("NameServer25", ActivServer);

                    }
                }
            }

            if (AppStart.TableReestr["ServerDefault25"] != "")
            {
                Grid_Puth("SELECT * from CONNECTIONBD25 WHERE CONNECTIONBD25.NAMESERVER = " + "'" + AppStart.TableReestr["NameServer25"] + "'");
                System.Windows.Application.Current.Dispatcher.Invoke(new Action(delegate ()
                {
                    ConectoWorkSpace.Administrator.AdminPanels ConectoWorkSpace_InW = AppStart.LinkMainWindow("WAdminPanels");
                    ConectoWorkSpace_InW.PuthSetServer25.Text = AppStart.TableReestr["ServerDefault25"];
                    Interface.CurrentStateInst("ServFB25OnOff", "2", "on_off_2.png", ConectoWorkSpace_InW.ServFB25OnOff);
                }));
            }
            else
            {
                System.Windows.Application.Current.Dispatcher.Invoke(new Action(delegate ()
                {
                    ConectoWorkSpace.Administrator.AdminPanels ConectoWorkSpace_InW = AppStart.LinkMainWindow("WAdminPanels");
                    ConectoWorkSpace_InW.PuthSetServer25.Text = "";
                    Interface.CurrentStateInst("ServFB25OnOff", "0", "on_off_1.png", ConectoWorkSpace_InW.ServFB25OnOff);
                }));
            }
            Inst2530 = "30";
            Idcount = -1;

            DBConecto.DBopenFBConectionMemory(SystemBD.ConnnStringSystem().ToString(), "", "FbSystem");
            InsertExecute = "SELECT count(*) from SERVERACTIVFB30";
            CountQuery = new DBConecto.UniQuery(InsertExecute, "FB");
            CountServer30 = CountQuery.ExecuteUNIScalar() == null ? 0 : Convert.ToInt16(CountQuery.ExecuteUNIScalar().ToString());
            if (CountServer30 != 0)
            {
                ServerName30 = new string[CountServer30];
                NameExeFile30 = new string[CountServer30];
                PortServer30 = new string[CountServer30];
                CurrentPassw30 = new string[CountServer30];


                StrCreate = "SELECT * from SERVERACTIVFB30";

                DBConecto.DBopenFBConectionMemory(SystemBD.ConnnStringSystem().ToString(), "", "FbSystem");
                FbCommand SelectTableFront = new FbCommand(StrCreate, DBConecto.bdFbSystemConect);
                FbDataReader ReadOutTableFront = SelectTableFront.ExecuteReader();
                while (ReadOutTableFront.Read())
                {
                    Idcount++;
                    PortServer30[Idcount] = ReadOutTableFront[0].ToString();
                    NameExeFile30[Idcount] = ReadOutTableFront[1].ToString();
                    ServerName30[Idcount] = ReadOutTableFront[2].ToString();
                    CurrentPassw30[Idcount] = ReadOutTableFront[5].ToString();
                }
                ReadOutTableFront.Close();

                for (int i = 0; i <= Idcount; i++)
                {
                    if (CurrentPassw30[i] == "")
                    {
                        string StrUpdate = "UPDATE SERVERACTIVFB30 SET CURRENTPASSW = " + "'" + AppStart.TableReestr["CurrentPasswABD30"] + "'" + " WHERE SERVERACTIVFB30.PUTH = " + "'" + NameExeFile30[i] + "'";
                        DBConecto.UniQuery InsertQuery = new DBConecto.UniQuery(StrUpdate, "FB");
                        InsertQuery.ExecuteUNIScalar();
                    }
                }
                DBConecto.DBcloseFBConectionMemory("FbSystem");

                for (int i = 0; i <= Idcount; i++)
                {
                    if (!File.Exists(NameExeFile30[i] + "firebird.exe"))
                    {
                        DBConecto.DBopenFBConectionMemory(SystemBD.ConnnStringSystem().ToString(), "", "FbSystem");
                        StrCreate = "DELETE from SERVERACTIVFB25 WHERE SERVERACTIVFB30.PUTH = " + "'" + NameExeFile30[i] + "'";
                        DBConecto.UniQuery DeletQuery = new DBConecto.UniQuery(StrCreate, "FB");
                        DeletQuery.UserQuery = string.Format(StrCreate, "SERVERACTIVFB30");
                        DeletQuery.ExecuteUNIScalar();

                        StrCreate = "DELETE from CONNECTIONBD30 WHERE CONNECTIONBD30.NAMESERVER = " + "'" + ServerName30[i] + "'";
                        DeletQuery = new DBConecto.UniQuery(StrCreate, "FB");
                        DeletQuery.UserQuery = string.Format(StrCreate, "CONNECTIONBD30");
                        DeletQuery.ExecuteUNIScalar();
                        DBConecto.DBcloseFBConectionMemory("FbSystem");


                    }
                    else
                    {
                        if (AppStart.TableReestr["ServerDefault30"] == "")
                        {
                            ConectoWorkSpace.Administrator.AdminPanels.UpdateKeyReestr("ServerDefault30", NameExeFile30[i]);
                            ConectoWorkSpace.Administrator.AdminPanels.UpdateKeyReestr("NameServer30", ServerName30[i]);
                        }

                        string PuthProcess = "";
                        ScanActivFirebird();


                        if (IndexActivProces >= 0)
                        {
                            for (int id = 0; id <= IndexActivProces; id++)
                            {

                                ActivServer = NameServer[id].Substring(NameServer[id].IndexOf("-s") + 2, NameServer[id].Length - (NameServer[id].IndexOf("-s") + 2)).Replace(" ", "");
                                if (ServerName30[i] == ActivServer) PuthProcess = NamePuth[id].ToUpper().Replace(" ", "");
                            }

                        }
                        if (PuthProcess == "") StrCreate = "UPDATE SERVERACTIVFB30 SET ACTIVONOFF = 'Stop' WHERE SERVERACTIVFB30.PUTH = " + "'" + NameExeFile30[i] + "'";
                        else StrCreate = "UPDATE SERVERACTIVFB30 SET ACTIVONOFF = 'Activ' WHERE SERVERACTIVFB30.PUTH = " + "'" + NameExeFile30[i] + "'";
                        DBConecto.DBopenFBConectionMemory(SystemBD.ConnnStringSystem().ToString(), "", "FbSystem");
                        DBConecto.UniQuery UpdateQuery = new DBConecto.UniQuery(StrCreate, "FB");
                        UpdateQuery.ExecuteUNIScalar();

                        LoadAlias30(ServerName30[i], NameExeFile30[i]);
                    }
                }

            }
            else
            {
                // Проверка проверки наличия установленных сереверов  в папках в виде файлов exe Firebird_3_0
                // Проверяем наличие установленных серверов в таблице активных серверов

                ScanActivFirebird();
                findFiles = System.IO.Directory.GetFiles(@"C:\Program Files", "firebird.exe", System.IO.SearchOption.AllDirectories);
                if (findFiles.Length != 0)
                {
                    foreach (string file in findFiles)
                    {
                        ActivServer = "Stop"; Idcount = 0;
                        if (IndexActivProces >= 0)
                        {

                            for (int id = 0; id <= IndexActivProces; id++)
                            {
                                string PuthProcess = NamePuth[id].ToUpper().Replace(" ", ""), PuthFiles = file.ToUpper().Replace(" ", "");
                                if (PuthProcess == PuthFiles) ActivServer = NameServer[id].Substring(NameServer[id].IndexOf("-s") + 2, NameServer[id].Length - (NameServer[id].IndexOf("-s") + 2)).Replace(" ", "");
                            }
                        }

                        if (file.IndexOf(".exe") > 0)
                        {
                            PuthServer = file.Substring(0, file.LastIndexOf(@"\") + 1); // -1
                                                                                        // с буквой с после функции GetFiles это круто
                        }
                        else
                        {

                            PuthServer = System.IO.Path.GetFullPath(file) + @"\";
                        }
                        if (File.Exists(PuthServer + "firebird.conf"))
                        {
                            foreach (string strk in File.ReadLines(PuthServer + "firebird.conf"))
                            {
                                if (strk.StartsWith("RemoteServicePort") == true)
                                {
                                    CurrentIdPort = File.ReadLines(PuthServer + "firebird.conf").First(xx => xx.StartsWith("RemoteServicePort")).Split('=')[1].Replace(" ", "");
                                }
                            }
                        }
                        CurrentIdPort = CurrentIdPort == "" ? "3056" : CurrentIdPort;
                        string IdPort = "";
                        StrCreate = "SELECT * FROM SERVERACTIVFB30 where SERVERACTIVFB30.PUTH = " + "'" + PuthServer + "' AND SERVERACTIVFB30.NAME = '" + ActivServer + "'";
                        DBConecto.DBopenFBConectionMemory(SystemBD.ConnnStringSystem().ToString(), "", "FbSystem");
                        FbCommand SelectTableFront = new FbCommand(StrCreate, DBConecto.bdFbSystemConect);
                        FbDataReader ReadOutTableFront = SelectTableFront.ExecuteReader();
                        while (ReadOutTableFront.Read()) { IdPort = ReadOutTableFront[0].ToString(); Idcount++; }
                        ReadOutTableFront.Close();
                        SelectTableFront.Dispose();
                        if (Idcount == 0)
                        {

                            string DateCreatFB = DateTime.Now.ToString("yyyyMMddHHmm");
                            StrCreate = "INSERT INTO SERVERACTIVFB30  values ('" + CurrentIdPort + "','" + PuthServer + "', '" + ActivServer + "', '" + DateCreatFB + "','Activ','alarm')";
                            DBConecto.UniQuery InsertQuery = new DBConecto.UniQuery(StrCreate, "FB");
                            InsertQuery.UserQuery = string.Format(StrCreate, "SERVERACTIVFB30");
                            InsertQuery.ExecuteUNIScalar();
                        }
                        else
                        {
                            if (IdPort != CurrentIdPort)
                            {
                                string StrUpdate = "UPDATE SERVERACTIVFB30 SET PORT = " + "'" + CurrentIdPort + "'" + " WHERE SERVERACTIVFB30.PUTH = " + "'" + PuthServer + "'";
                                DBConecto.UniQuery InsertQuery = new DBConecto.UniQuery(StrUpdate, "FB");
                                InsertQuery.ExecuteUNIScalar();
                                InstallB52.RestartFB25(PuthServer, ActivServer);
                            }
                        }


                    }
                    LoadAlias30(ActivServer, PuthServer);
                    InsertExecute = "SELECT count(*) from SERVERACTIVFB30";
                    CountQuery = new DBConecto.UniQuery(InsertExecute, "FB");
                    CountServer25 = CountQuery.ExecuteUNIScalar() == null ? 0 : Convert.ToInt16(CountQuery.ExecuteUNIScalar().ToString());
                    if (CountServer25 == 1)
                    {
                        ConectoWorkSpace.Administrator.AdminPanels.UpdateKeyReestr("ServerDefault30", PuthServer);
                        ConectoWorkSpace.Administrator.AdminPanels.UpdateKeyReestr("NameServer30", ActivServer);

                    }
                }
            }
            if (AppStart.TableReestr["ServerDefault30"] != "")
            {
                Grid_Puth("SELECT * from CONNECTIONBD30 WHERE CONNECTIONBD30.NAMESERVER = " + "'" + AppStart.TableReestr["NameServer30"] + "'");

                System.Windows.Application.Current.Dispatcher.Invoke(new Action(delegate ()
                {
                    if (AppStart.TableReestr["ServerDefault30"] != "")
                    {
                        ConectoWorkSpace.Administrator.AdminPanels ConectoWorkSpace_InW = AppStart.LinkMainWindow("WAdminPanels");
                        ConectoWorkSpace_InW.PuthSetServer30.Text = AppStart.TableReestr["ServerDefault30"];
                        Interface.CurrentStateInst("ServFB30OnOff", "2", "on_off_2.png", ConectoWorkSpace_InW.ServFB30OnOff);
                    }
                    else
                    {
                        ConectoWorkSpace.Administrator.AdminPanels ConectoWorkSpace_InW = AppStart.LinkMainWindow("WAdminPanels");
                        ConectoWorkSpace_InW.PuthSetServer30.Text = "";
                        Interface.CurrentStateInst("ServFB30OnOff", "0", "on_off_1.png", ConectoWorkSpace_InW.ServFB30OnOff);
                    }
                }));
            }
            if (AppStart.TableReestr["InstBackOnOff"] == "BackFbd25OnOff" || AppStart.TableReestr["InstFrontOnOff"] == "FrontFbd25OnOff" || AppStart.TableReestr["InstBackOnOff"] == "BackFbd30OnOff" || AppStart.TableReestr["InstFrontOnOff"] == "FrontFbd30OnOff")
            {
                if (!SystemConecto.File_(SystemConectoServers.PutchLib + @"TTF16.ocx", 5))
                {
                    Dictionary<string, string> fbembedList = new Dictionary<string, string>();
                    fbembedList.Add(SystemConectoServers.PutchLib + @"TTF16.ocx", "b52/libs/");
                    if (SystemConecto.IsFilesPRG(fbembedList, -1, "- Проверка файлов во время установки сервера " + AppStart.TableReestr["ServerDefault25"]) != "True")
                    {
                        var TextWindows = "Отсутствует библиотека TTF16.ocx. ";
                        InstallB52.MessageErorInst(TextWindows);
                    }
                    if (SystemConecto.File_(SystemConectoServers.PutchLib + @"TTF16.ocx", 5))
                    {
                        DllWork.RegSrv32(SystemConectoServers.PutchLib + "TTF16.ocx", "/s");
                        ConectoWorkSpace.Administrator.AdminPanels.UpdateKeyReestr("CheckTTF16OnOff", "2");
                    }

                }

            }

            // Анализ наличия установки posgresql
            Inst2530 = "postgres";
            Idcount = -1;
            CurrentIdPort = ""; StrCreate = "";
            DBConecto.DBopenFBConectionMemory(SystemBD.ConnnStringSystem().ToString(), "", "FbSystem");
            InsertExecute = "SELECT count(*) from SERVERACTIVPOSTGRESQL";
            DBConecto.UniQuery CountPostgres = new DBConecto.UniQuery(InsertExecute, "FB");
            CountServer25 = CountPostgres.ExecuteUNIScalar() == null ? 0 : Convert.ToInt16(CountPostgres.ExecuteUNIScalar().ToString());
            if (CountServer25 != 0)
            {
                ServerNamePG = new string[CountServer25];
                NameExeFilePG = new string[CountServer25];
                PortServerPG = new string[CountServer25];
                CurrentPasswPG = new string[CountServer25];

                StrCreate = "SELECT * from SERVERACTIVPOSTGRESQL";
                DBConecto.DBopenFBConectionMemory(SystemBD.ConnnStringSystem().ToString(), "", "FbSystem");
                FbCommand SelectTableFront = new FbCommand(StrCreate, DBConecto.bdFbSystemConect);
                FbDataReader ReadOutTableFront = SelectTableFront.ExecuteReader();
                while (ReadOutTableFront.Read())
                {
                    Idcount++;
                    PortServerPG[Idcount] = ReadOutTableFront[0].ToString();
                    NameExeFilePG[Idcount] = ReadOutTableFront[1].ToString();
                    ServerNamePG[Idcount] = ReadOutTableFront[2].ToString();
                    CurrentPasswPG[Idcount] = ReadOutTableFront[5].ToString();

                }
                ReadOutTableFront.Close();
                DBConecto.DBcloseFBConectionMemory("FbSystem");

                for (int i = 0; i <= Idcount; i++)
                {

                    if (CurrentPasswPG[i] == "" || CurrentPasswPG[i] == null)
                    {
                        ModifyTable("UPDATE SERVERACTIVPOSTGRESQL SET CURRENTPASSW = " + "'" + AppStart.TableReestr["CurrentPasswABDPG"] + "'" + " WHERE SERVERACTIVPOSTGRESQL.PUTH = " + "'" + NameExeFilePG[i] + "'");
                    }
                }
                for (int i = 0; i <= Idcount; i++)
                {
                    if (!File.Exists(NameExeFilePG[i] + @"bin\postgres.exe"))
                    {
                        ModifyTable("DELETE from SERVERACTIVPOSTGRESQL WHERE SERVERACTIVPOSTGRESQL.PUTH = " + "'" + NameExeFilePG[i] + "'");
                        ModifyTable("DELETE from CONNECTIONPOSTGRESQL WHERE CONNECTIONPOSTGRESQL.NAMESERVER = " + "'" + ServerNamePG[i] + "'");
                    }
                    else
                    {
                        if (AppStart.TableReestr["ServerDefaultPG"] == "")
                        {
                            UpdateKeyReestr("ServerDefaultPG", NameExeFilePG[i]);
                            UpdateKeyReestr("NameServerPG", ServerNamePG[i]);
                            UpdateKeyReestr("CurrentPasswABDPG", CurrentPasswPG[i]);
                        }

                        string PuthProcess = "";
                        ScanActivFirebird();

                        if (IndexActivProces >= 0)
                        {
                            for (int id = 0; id <= IndexActivProces; id++)if (NameServer[id].Contains(ServerNamePG[i])) PuthProcess = NamePuth[id].ToUpper().Trim();
           
                            if (File.Exists(NameExeFilePG[i] + @"data\postgresql.conf"))
                            {
                                foreach (string strk in File.ReadLines(NameExeFilePG[i] + @"data\postgresql.conf"))
                                {
                                    if (strk.StartsWith("port") == true)
                                    {
                                        CurrentIdPort = strk.Substring(strk.LastIndexOf("=")+1,5).Trim();
                                    }
                                }
                            }
                            if (CurrentIdPort != PortServerPG[i])
                            {
                                string StrUpdate = "UPDATE SERVERACTIVPOSTGRESQL SET PORT = " + "'" + CurrentIdPort + "'" + " WHERE SERVERACTIVPOSTGRESQL.PUTH = " + "'" + NameExeFilePG[i] + "'";
                                ModifyTable(StrCreate);
                                //InstallB52.RestartFB25(PuthServer, ActivServer);
                            }

                        }

                        if (PuthProcess == "") StrCreate = "UPDATE SERVERACTIVPOSTGRESQL SET ACTIVONOFF = 'Stop' WHERE SERVERACTIVPOSTGRESQL.PUTH = " + "'" + NameExeFilePG[i] + "'";
                        else StrCreate = "UPDATE SERVERACTIVPOSTGRESQL SET ACTIVONOFF = 'Activ' WHERE SERVERACTIVPOSTGRESQL.PUTH = " + "'" + NameExeFilePG[i] + "'";
                        StoptServPostGre.Content = (PuthProcess == "" ?  "Запустить" : "Остановить");
                        ModifyTable(StrCreate);

                        // путь к БД;
                        if (AppStart.TableReestr["PuthSetBDPostGreSQL"] == "" ) AppStart.TableReestr["PuthSetBDPostGreSQL"] = AppStart.TableReestr["ServerDefaultPG"] + "data";

                        //LoadAlias(ServerNamePG[i], NameExeFilePG[i]);
                    }

                }

                DBConecto.DBopenFBConectionMemory(SystemBD.ConnnStringSystem().ToString(), "", "FbSystem");
                InsertExecute = "SELECT count(*) from SERVERACTIVPOSTGRESQL";
                DBConecto.UniQuery CountPG = new DBConecto.UniQuery(InsertExecute, "FB");
                CountServer25 = CountPG.ExecuteUNIScalar() == null ? 0 : Convert.ToInt16(CountPG.ExecuteUNIScalar().ToString());
                if (CountServer25 == 0)
                {
                    PuthSetServerPostGre.Text = "";
                    Interface.CurrentStateInst("PostgresqlOnOff", "0", "on_off_1.png", PostgresqlOnOff);
                    UpdateKeyReestr("ServerDefaultPG", "");
                    UpdateKeyReestr("NameServerPG", "");
                }
            }
            else
            {
                Idcount = -1; findFiles = new string[1];
                // Проверяем активные процессы
                ScanActivFirebird();
                if (Directory.Exists(@"C:\Program Files\PostgreSQL"))findFiles = System.IO.Directory.GetFiles(@"C:\Program Files\PostgreSQL", "postgres.exe", System.IO.SearchOption.AllDirectories);      
 
                if (findFiles[0] == null)findFiles = System.IO.Directory.GetFiles(@"c:\Program Files\Conecto\Servers\", "postgres.exe", System.IO.SearchOption.AllDirectories); 
                if(findFiles.Length !=0)
                {
                    if (findFiles[0] != null)
                    { 
                        foreach (string file in findFiles)
                        {
                            ActivServer = "Stop"; Idcount = 0;
                            if (IndexActivProces >= 0)
                            {

                                for (int id = 0; id <= IndexActivProces; id++)
                                {
                                    string PuthProcess = NamePuth[id].ToUpper().Trim(), PuthFiles = file.ToUpper().Trim();
                                    if (PuthFiles.Contains(PuthProcess)) ActivServer = "postgres";


                                }
                            }

                            PuthServer = System.IO.Path.GetFullPath(file) + @"\";
                            if (file.IndexOf("bin") > 0) PuthServer = file.Substring(0, file.IndexOf("bin")); 
 
                            if (File.Exists(PuthServer + @"data\postgresql.conf"))
                            {
                                foreach (string strk in File.ReadLines(PuthServer + @"data\postgresql.conf"))
                                {
                                    if (strk.StartsWith("port") == true)
                                    {
                                        CurrentIdPort = strk.Substring(strk.LastIndexOf("=") + 1, 5).Trim();
                                    }
                                }
                            }
                            CurrentIdPort = CurrentIdPort == "" ? "5432" : CurrentIdPort;
                            string IdPort = "";
                            StrCreate = "SELECT * FROM SERVERACTIVPOSTGRESQL where SERVERACTIVPOSTGRESQL.PUTH = " + "'" + PuthServer + "' AND SERVERACTIVPOSTGRESQL.NAME = '" + ActivServer + "'";
                            DBConecto.DBopenFBConectionMemory(SystemBD.ConnnStringSystem().ToString(), "", "FbSystem");
                            FbCommand SelectTableFront = new FbCommand(StrCreate, DBConecto.bdFbSystemConect);
                            FbDataReader ReadOutTableFront = SelectTableFront.ExecuteReader();
                            while (ReadOutTableFront.Read()) { IdPort = ReadOutTableFront[0].ToString(); Idcount++; }
                            ReadOutTableFront.Close();
                            SelectTableFront.Dispose();
                            if (Idcount == 0)
                            {

                                string DateCreatFB = DateTime.Now.ToString("yyyyMMddHHmm");
                                StrCreate = "INSERT INTO SERVERACTIVPOSTGRESQL  values ('" + CurrentIdPort + "','" + PuthServer + "', '" + ActivServer + "', '" + DateCreatFB + "','Activ','psgresql')";
                                DBConecto.UniQuery InsertQuery = new DBConecto.UniQuery(StrCreate, "FB");
                                InsertQuery.UserQuery = string.Format(StrCreate, "SERVERACTIVPOSTGRESQL");
                                InsertQuery.ExecuteUNIScalar();
                                if (AppStart.TableReestr["ServerDefaultPG"] == "")
                                {
                                    UpdateKeyReestr("ServerDefaultPG", PuthServer);
                                    UpdateKeyReestr("NameServerPG", ActivServer);
                                }
                            }
                            else
                            {
                                if (IdPort != CurrentIdPort)
                                {
                                    string StrUpdate = "UPDATE SERVERACTIVPOSTGRESQL SET PORT = " + "'" + CurrentIdPort + "'" + " WHERE SERVERACTIVPOSTGRESQL.PUTH = " + "'" + PuthServer + "'";
                                    DBConecto.UniQuery InsertQuery = new DBConecto.UniQuery(StrUpdate, "FB");
                                    InsertQuery.ExecuteUNIScalar();
                                    //InstallB52.RestartFB25(PuthServer, ActivServer);
                                }
                            }
                            //LoadAlias(ActivServer, PuthServer);

                        }

                        DBConecto.DBopenFBConectionMemory(SystemBD.ConnnStringSystem().ToString(), "", "FbSystem");
                        InsertExecute = "SELECT count(*) from SERVERACTIVPOSTGRESQL";
                        CountQuery = new DBConecto.UniQuery(InsertExecute, "FB");
                        CountServer25 = CountQuery.ExecuteUNIScalar() == null ? 0 : Convert.ToInt16(CountQuery.ExecuteUNIScalar().ToString());
                        if (CountServer25 == 1)
                        {
                            UpdateKeyReestr("ServerDefaultPG", PuthServer);
                            UpdateKeyReestr("NameServerPG", ActivServer);

                        }                   
                    }
 
                }
                else
                {
                    if (Directory.Exists(@"c:\Program Files\Conecto\Servers\PostgreSQL"))Directory.Delete(@"c:\Program Files\Conecto\Servers\PostgreSQL",true);
                    PuthSetServerPostGre.Text = "";
                    Interface.CurrentStateInst("PostgresqlOnOff", "0", "on_off_1.png", PostgresqlOnOff);
                    UpdateKeyReestr("ServerDefaultPG", "");
                    UpdateKeyReestr("NameServerPG", "");
                }
            }

            if (AppStart.TableReestr["ServerDefaultPG"] != "")
            {
                SetServerGrid("SELECT * from SERVERACTIVPOSTGRESQL WHERE SERVERACTIVPOSTGRESQL.NAME = " + "'" + AppStart.TableReestr["NameServerPG"] + "'");

                System.Windows.Application.Current.Dispatcher.Invoke(new Action(delegate ()
                {

                    if (AppStart.TableReestr["ServerDefaultPG"] != "")
                    {
                        ConectoWorkSpace.Administrator.AdminPanels ConectoWorkSpace_InW = AppStart.LinkMainWindow("WAdminPanels");
                        ConectoWorkSpace_InW.PuthSetServerPostGre.Text = AppStart.TableReestr["ServerDefaultPG"];
                        Interface.CurrentStateInst("PostgresqlOnOff", "2", "on_off_2.png", ConectoWorkSpace_InW.PostgresqlOnOff);
                    }
                    else
                    { 
                        ConectoWorkSpace.Administrator.AdminPanels ConectoWorkSpace_InW = AppStart.LinkMainWindow("WAdminPanels");
                        ConectoWorkSpace_InW.PuthSetServerPostGre.Text = "";
                        Interface.CurrentStateInst("PostgresqlOnOff", "0", "on_off_1.png", ConectoWorkSpace_InW.PostgresqlOnOff);                   
                    }

                }));
            }
 
            AppStart.StartReshenie = 1;
            System.Windows.Application.Current.Dispatcher.Invoke(new Action(delegate ()
            {
                ConectoWorkSpace.WinOblakoLeft ConectoWorkSpace_Off = AppStart.LinkMainWindow("WinOblakoLeft");
                ConectoWorkSpace_Off.Close();
            }));
        }

        // Процедура активации доски (екрана) с выбором видов  различных установок
        private void ClickReshenie_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            Reshenie();
        }
        public void Reshenie()
        {
            // Настройка закладок Бека и Фронта.
            if (AppStart.TableReestr["BackOfAdresPorta"] == "")
            {
                BackOfAdresPorta.Text = AppStart.TableReestr["BackOfAdresPorta"] = "3182";
                Administrator.AdminPanels.UpdateKeyReestr("BackOfAdresPorta", BackOfAdresPorta.Text);
            }
            if (AppStart.TableReestr["SetTextIp4BackOf"] == "")
            {
                Administrator.AdminPanels.UpdateKeyReestr("SetTextIp4BackOf", "127.0.0.1");
            }



            if (AppStart.TableReestr["BackOfAdresPortServer"] == "")
            {
                BackOfAdresPortServer.Text = AppStart.TableReestr["BackOfAdresPortServer"] = "3055";
                Administrator.AdminPanels.UpdateKeyReestr("BackOfAdresPortServer", BackOfAdresPortServer.Text);

            }
            if (AppStart.TableReestr["AdresPortServer"] == "")
            {
                AdresPortServer.Text = AppStart.TableReestr["AdresPortServer"] = "3055";
                Administrator.AdminPanels.UpdateKeyReestr("AdresPortServer", AdresPortServer.Text);

            }
            if (AppStart.TableReestr["SetTextAdrDataIp4BackOf"] == "")
            {
                IPHostEntry host1 = Dns.GetHostEntry(Dns.GetHostName());
                foreach (IPAddress ip in host1.AddressList)
                {
                    AppStart.TableReestr["SetTextAdrDataIp4BackOf"] = ip.ToString();
                }
            }
            if (AppStart.TableReestr["FrontAdrIp411"] == "")
            {
                IPHostEntry host1 = Dns.GetHostEntry(Dns.GetHostName());
                foreach (IPAddress ip in host1.AddressList)
                {
                    AppStart.TableReestr["FrontAdrIp411"] = ip.ToString();
                }
            }

            AdresPortServer.Text = AppStart.TableReestr["AdresPortServer"];
            BackOfAdresPortServer.Text = AppStart.TableReestr["BackOfAdresPortServer"];
            AppStart.TableReestr["SetPortServer25"] = AppStart.TableReestr["SetPortServer25"] == "" ? "3055" : AppStart.TableReestr["SetPortServer25"];
            Administrator.AdminPanels.UpdateKeyReestr("SetPortServer25", AppStart.TableReestr["SetPortServer25"]);
            AppStart.TableReestr["SetPortServer30"] = AppStart.TableReestr["SetPortServer30"] == "" ? "3056" : AppStart.TableReestr["SetPortServer30"];
            Administrator.AdminPanels.UpdateKeyReestr("SetPortServer30", AppStart.TableReestr["SetPortServer30"]);
            AppStart.TableReestr["TcpPort"] = AppStart.TableReestr["TcpPort"] == "" ? "3182" : AppStart.TableReestr["TcpPort"];
            AppStart.TableReestr["IpName"] = AppStart.TableReestr["IpName"] == "" ? "127.0.0.1" : AppStart.TableReestr["IpName"];
            AppStart.TableReestr["BackOfTcpPort"] = AppStart.TableReestr["BackOfTcpPort"] == "" ? "3182" : AppStart.TableReestr["BackOfTcpPort"];
            AppStart.TableReestr["BackOfIpName"] = AppStart.TableReestr["BackOfIpName"] == "" ? "127.0.0.1" : AppStart.TableReestr["BackOfIpName"];
            if (AppStart.TableReestr["CurrentPasswABD25"] == "") Administrator.AdminPanels.UpdateKeyReestr("CurrentPasswABD25", "masterkey");
            if (AppStart.TableReestr["CurrentPasswABD30"] == "") Administrator.AdminPanels.UpdateKeyReestr("CurrentPasswABD30", "alarm");


        }


        // Процедура проверки наличия записей в таблице CONFIGSOFT базы данных SystemFB
        public static void InitKeySystemFB(string intvar)
        {
            if (LoadTableRestr == 0)
            {
                //если соединение закрыто - откроем его; Перечисление ConnectionState содержит состояния соединения (подключено/отключено)
                DBConecto.DBopenFBConectionMemory(SystemBD.ConnnStringSystem().ToString(), "", "FbSystem");
                foreach (string NameKey in AdminPanels.ButtonPanel)
                {

                    int Idcount = 0;
                    string StrCreate = "select * from CONFIGSOFT where CONFIGSOFT.NAMEVAR = " + "'" + NameKey + "'";

                    FbCommand SelectTable = new FbCommand(StrCreate, DBConecto.bdFbSystemConect);
                    SelectTable.CommandType = CommandType.Text;
                    FbDataReader ReadOutTable = SelectTable.ExecuteReader();
                    while (ReadOutTable.Read()) { Idcount++; }
                    ReadOutTable.Close();
                    if (Idcount == 0)
                    {
                        string SetVar = intvar == "InitKey" ? "0" : "";
                        string InsertExecute = "SELECT count(*) from CONFIGSOFT";
                        DBConecto.UniQuery CountQuery = new DBConecto.UniQuery(InsertExecute, "FB");
                        string CountTable = CountQuery.ExecuteUNIScalar() == null ? "" : CountQuery.ExecuteUNIScalar().ToString();
                        int InsertCount = Convert.ToInt32(CountTable) + 1;
                        StrCreate = "INSERT INTO CONFIGSOFT  values (" + InsertCount + ",'" + NameKey + "'" + ", '" + SetVar + "')";
                        CountQuery.UserQuery = string.Format(StrCreate, "CONFIGSOFT");
                        CountQuery.ExecuteUNIScalar();
                        CountQuery.UserQuery = string.Format(InsertExecute, "CONFIGSOFT");
                        CountTable = CountQuery.ExecuteUNIScalar() == null ? "" : CountQuery.ExecuteUNIScalar().ToString();
                        int InsertTable = Convert.ToInt32(CountTable);
                        if (InsertTable == InsertCount) AppStart.TableReestr.Add(NameKey, SetVar);
                    }
                    SelectTable.Dispose();

                }
                DBConecto.DBcloseFBConectionMemory("FbSystem");
            }
        }

        // Процедура проверки наличия записей в таблице CONFIGSOFT базы данных SystemFB
        public static void InitKeyReestr(string KeyOb, string SetVar)
        {

            DBConecto.DBopenFBConectionMemory(SystemBD.ConnnStringSystem().ToString(), "", "FbSystem");
            int Idcount = 0;
            string StrCreate = "select * from CONFIGSOFT where CONFIGSOFT.NAMEVAR = " + "'" + KeyOb + "'";
            FbCommand SelectTable = new FbCommand(StrCreate, DBConecto.bdFbSystemConect);
            FbDataReader ReadOutTable = SelectTable.ExecuteReader();
            string KeyObValue = "", NameKey = "";
            while (ReadOutTable.Read())
            {
                NameKey = ReadOutTable[1].ToString();
                KeyObValue = ReadOutTable[2].ToString();
                Idcount = Idcount + 1;
            }
            ReadOutTable.Close();
            if (Idcount == 0)
            {
                string InsertExecute = "SELECT count(*) from CONFIGSOFT";
                DBConecto.UniQuery CountQuery = new DBConecto.UniQuery(InsertExecute, "FB");
                string CountTable = CountQuery.ExecuteUNIScalar() == null ? "" : CountQuery.ExecuteUNIScalar().ToString();
                int InsertCount = Convert.ToInt32(CountTable) + 1;
                StrCreate = "INSERT INTO CONFIGSOFT  values (" + InsertCount + ",'" + KeyOb + "'" + ", '" + SetVar + "')";
                CountQuery.UserQuery = string.Format(StrCreate, "CONFIGSOFT");
                CountQuery.ExecuteUNIScalar();
                CountQuery.UserQuery = string.Format(InsertExecute, "CONFIGSOFT");
                CountTable = CountQuery.ExecuteUNIScalar() == null ? "" : CountQuery.ExecuteUNIScalar().ToString();
                int InsertTable = Convert.ToInt32(CountTable);
                if (InsertTable == InsertCount) AppStart.TableReestr.Add(KeyOb, SetVar);
            }
            else if (KeyObValue == "") AppStart.TableReestr[KeyOb] = SetVar;
            SelectTable.Dispose();
            DBConecto.DBcloseFBConectionMemory("FbSystem");
        }

        // Процедура проверки наличия записей в таблице CONFIGSOFT базы данных SystemFB
        public static void UpdateKeyReestr(string KeyOb, string SetKey)
        {
            DBConecto.DBopenFBConectionMemory(SystemBD.ConnnStringSystem().ToString(), "", "FbSystem");
            string StrCreate = "UPDATE CONFIGSOFT SET SETVAR =  '" + SetKey + "'  WHERE CONFIGSOFT.NAMEVAR = " + "'" + KeyOb + "'";
            DBConecto.UniQuery CountQuery = new DBConecto.UniQuery(StrCreate, "FB");
            string CountTable = CountQuery.ExecuteUNIScalar() == null ? "" : CountQuery.ExecuteUNIScalar().ToString();
            AppStart.TableReestr[KeyOb] = SetKey;
            DBConecto.DBcloseFBConectionMemory("FbSystem");
        }

        // Процедура проверки наличия записей в таблице CONFIGSOFT базы данных SystemFB
        public static void SetCurrentAlias(string StrCreate)
        {
            int Idcount = 0;
            DBConecto.DBopenFBConectionMemory(SystemBD.ConnnStringSystem().ToString(), "", "FbSystem");
            //StrCreate = "select * from CONNECTIONBD25 where CONNECTIONBD25.PUTHBD = " + "'" + AppStart.TableReestr["PuthSetBD25"] + "'";
            FbCommand SelectTable = new FbCommand(StrCreate, DBConecto.bdFbSystemConect);
            FbDataReader ReadOutTable = SelectTable.ExecuteReader();
            while (ReadOutTable.Read())
            {
                SelectAlias = ReadOutTable[1].ToString();
                Idcount = Idcount + 1;
            }
            ReadOutTable.Close();
            DBConecto.DBcloseFBConectionMemory("FbSystem");
        }


        #region Закладка "Фронт офис"

        // Процедура анализа состояния переключателей панели Фронт клиент касса Б52
        private void InitPanel_FrontClientKasa(object sender, MouseButtonEventArgs e)
        {
            InitPanelFrontClientKasa();
        }
        // Процедура анализа состояния переключателей панели Фронт клиент касса Б52
        private void InitPanelFrontClientKasa()
        {
            // Активная закладка Фронт клиент Б52
            CurrentBack = 0; CurrentFront = 1;
            if (StartFront == 1)
            {
                var TextWindows = "Выполняется идентификация данных." + Environment.NewLine + "Пожалуйста подождите. ";
                int AutoClose = 1;
                int MesaggeTop = -170;
                int MessageLeft = 300;
                ConectoWorkSpace.MainWindow.MessageEnd(TextWindows, AutoClose, MesaggeTop, MessageLeft);
                InitKeyOnOffFront();
                InitPanel_();
                InitTextFront();
                CheckInstalFront25();
                //if (StartBack == 1) { CheckInstalBack25(); }

                // Получение текущего IP адреса компа
                var host = Dns.GetHostEntry(Dns.GetHostName());
                foreach (var ip in host.AddressList)
                {
                    if (ip.AddressFamily == AddressFamily.InterNetwork)
                    {
                        IpAdresHome_Front.Content = ip.ToString();

                    }
                }
                //OffInstalfrontB52.Visibility = Visibility.Collapsed;
                ChageAliasFront25.IsEnabled = false;
                string StatusPic = Convert.ToInt32(AppStart.TableReestr["BackOfServKeyOnOff"].ToString()) == 2 ? "2" : "";
                string SetKeyValue = Convert.ToInt32(AppStart.TableReestr["BackOfServKeyOnOff"].ToString()) == 2 ? "on_off_2.png" : "on_off_1.png";
                var pic = (Image)LogicalTreeHelper.FindLogicalNode(this, "FrontKeyServOnOff");
                var uriSource = new Uri(@"/Conecto®%20WorkSpace;component/Images/" + SetKeyValue, UriKind.Relative);
                pic.Source = new BitmapImage(uriSource);
                Administrator.AdminPanels.UpdateKeyReestr("FrontKeyServOnOff", StatusPic);
                string StatusLoc = Convert.ToInt32(AppStart.TableReestr["SetLocKeyOnOff"].ToString()) == 2 ? "on_off_2.png" : "on_off_1.png";
                var PicLoc = (Image)LogicalTreeHelper.FindLogicalNode(this, "KeyLocFront");
                var uriSourceLoc = new Uri(@"/Conecto®%20WorkSpace;component/Images/" + StatusLoc, UriKind.Relative);
                PicLoc.Source = new BitmapImage(uriSourceLoc);

                if (Convert.ToInt32(AppStart.TableReestr["CheckTTF16"]) == 0)
                {
                    AppStart.RenderInfo Arguments01 = new AppStart.RenderInfo() { };
                    Arguments01.argument1 = "1";
                    Arguments01.argument2 = "";
                    Thread thStartTimer01 = new Thread(CheckTTF16);
                    thStartTimer01.SetApartmentState(ApartmentState.STA);
                    thStartTimer01.IsBackground = true; // Фоновый поток
                    thStartTimer01.Start(Arguments01);
                    InstallB52.IntThreadStart++;

                }
                string SetTTF16Value = Convert.ToInt32(AppStart.TableReestr["CheckTTF16OnOff"].ToString()) == 2 ? "on_off_2.png" : "on_off_1.png";
                var picTTF16 = (Image)LogicalTreeHelper.FindLogicalNode(this, "CheckTTF16OnOff");
                var uriSourceTTF16 = new Uri(@"/Conecto®%20WorkSpace;component/Images/" + SetTTF16Value, UriKind.Relative);
                pic.Source = new BitmapImage(uriSource);
                var picTTF16_Front = (Image)LogicalTreeHelper.FindLogicalNode(this, "CheckTTF16OnOff_Front");
                var uriSourceTTF16_Front = new Uri(@"/Conecto®%20WorkSpace;component/Images/" + SetTTF16Value, UriKind.Relative);
                pic.Source = new BitmapImage(uriSource);
                Inst2530 = AppStart.TableReestr["InstFrontOnOff"];
                Interface.CurrentStateInst("FrontFbd25OnOff", Convert.ToInt32(AppStart.TableReestr["ServFB25OnOff"].ToString()) == 2 ? "2" : "0", Convert.ToInt32(AppStart.TableReestr["ServFB25OnOff"].ToString()) == 2 ? "on_off_2.png" : "on_off_1.png", FrontFbd25OnOff);
                Interface.CurrentStateInst("FrontFbd30OnOff", Convert.ToInt32(AppStart.TableReestr["ServFB30OnOff"].ToString()) == 2 ? "2" : "0", Convert.ToInt32(AppStart.TableReestr["ServFB30OnOff"].ToString()) == 2 ? "on_off_2.png" : "on_off_1.png", FrontFbd30OnOff);
            }







            AdresPortServer.Text = AppStart.TableReestr["AdresPortServer"];
            AdrIp4LenchFront = Ip4 = AppStart.TableReestr["FrontAdrIp4"];
            if (Ip4.Length == 0)
            {
                AdrIp4LenchFront = Ip4 = "127.0.0.1";
                Administrator.AdminPanels.UpdateKeyReestr("FrontAdrIp4", Ip4);
                FrontTextIp41.IsEnabled = true;
                FrontTextIp42.IsEnabled = true;
                FrontTextIp43.IsEnabled = true;
                FrontTextIp44.IsEnabled = true;
            }
            if (StartFront == 1)
            {
                FrontOfAdrDataIp4Text1.Text = "";
                FrontOfAdrDataIp4Text2.Text = "";
                FrontOfAdrDataIp4Text3.Text = "";
                FrontOfAdrDataIp4Text4.Text = "";
                FrontAdresPorta.Text = "";

                if ((Convert.ToInt32(AppStart.TableReestr["AdrServerDate_IP4"].ToString()) == 0 || Convert.ToInt32(AppStart.TableReestr["AdrServerDate_IP4"].ToString()) == 3) && Ip4.Length == 0)
                {
                    FrontTextIp41.IsEnabled = false;
                    FrontTextIp42.IsEnabled = false;
                    FrontTextIp43.IsEnabled = false;
                    FrontTextIp44.IsEnabled = false;

                }
                else
                {
                    if (Convert.ToInt32(AppStart.TableReestr["FrontFbd25OnOff"].ToString()) == 0 && Convert.ToInt32(AppStart.TableReestr["FrontFbd30OnOff"].ToString()) == 0)
                    {
                        FrontTextIp41.IsEnabled = false;
                        FrontTextIp42.IsEnabled = false;
                        FrontTextIp43.IsEnabled = false;
                        FrontTextIp44.IsEnabled = false;
                        AdrServerDate_IP4.IsEnabled = false;
                        Interface.CurrentStateInst("AdrServerDate_IP4", "0", "on_off_1.png", AdrServerDate_IP4);
                    }
                    else
                    {
                        FrontTextIp41.IsEnabled = true;
                        FrontTextIp42.IsEnabled = true;
                        FrontTextIp43.IsEnabled = true;
                        FrontTextIp44.IsEnabled = true;
                        AdrServerDate_IP4.IsEnabled = true;
                        Interface.CurrentStateInst("AdrServerDate_IP4", "2", "on_off_2.png", AdrServerDate_IP4);

                    }

                    FrontIP61.IsEnabled = false;
                    FrontIP62.IsEnabled = false;
                    FrontIP63.IsEnabled = false;
                    FrontIP64.IsEnabled = false;
                    FrontIP65.IsEnabled = false;
                    FrontIP66.IsEnabled = false;
                }
            }

            for (int indPoint = 1; indPoint <= 3; indPoint = indPoint + 1)
            {
                int position = Ip4.IndexOf(".");
                if (position <= 0) { break; }
                switch (indPoint)
                {
                    case 1:
                        FrontTextIp41.Text = Ip4.Substring(0, position);
                        break;
                    case 2:
                        FrontTextIp42.Text = Ip4.Substring(0, position);
                        break;
                    case 3:
                        FrontTextIp43.Text = Ip4.Substring(0, position);
                        break;
                }
                Ip4 = Ip4.Substring(position + 1);
            }
            FrontTextIp44.Text = Ip4.Substring(0);


            if (Convert.ToInt32(AppStart.TableReestr["AdrServerDate_IP6"].ToString()) == 0 || Convert.ToInt32(AppStart.TableReestr["AdrServerDate_IP6"].ToString()) == 3)
            {
                FrontIP61.IsEnabled = false;
                FrontIP62.IsEnabled = false;
                FrontIP63.IsEnabled = false;
                FrontIP64.IsEnabled = false;
                FrontIP65.IsEnabled = false;
                FrontIP66.IsEnabled = false;
            }
            else
            {
                FrontTextIp41.IsEnabled = false;
                FrontTextIp42.IsEnabled = false;
                FrontTextIp43.IsEnabled = false;
                FrontTextIp44.IsEnabled = false;
            }
            AdrIp6lenchFront = Ip6 = AppStart.TableReestr["FrontAdrIp6"];

            for (int indPoint = 1; indPoint <= 5; indPoint = indPoint + 1)
            {
                int position = Ip6.IndexOf(".");
                if (position <= 0) { break; }
                switch (indPoint)
                {
                    case 1:
                        FrontIP61.Text = Ip6.Substring(0, position);
                        break;
                    case 2:
                        FrontIP62.Text = Ip6.Substring(0, position);
                        break;
                    case 3:
                        FrontIP63.Text = Ip6.Substring(0, position);
                        break;
                    case 4:
                        FrontIP64.Text = Ip6.Substring(0, position);
                        break;
                    case 5:
                        FrontIP65.Text = Ip6.Substring(0, position);
                        break;
                }
                Ip6 = Ip6.Substring(position + 1);
            }
            FrontIP66.Text = Ip6.Substring(0);

            //if (AppStart.TableReestr["PuthSetBD25"].ToString().Length ==0 && AppStart.TableReestr["PuthSetBD30"].ToString().Length == 0)  
            //{
            //    RoznClickOnOff.IsEnabled = false;
            //    RestoranClickOnOff.IsEnabled = false;
            //    FastFudClickOnOff.IsEnabled = false;
            //    FitnesClickOnOff.IsEnabled = false;
            //    HotelClickOnOff.IsEnabled = false;
            //    //OffInstalfrontB52.Visibility = Visibility.Visible;
            //    AdresPortServer.IsEnabled = false;

            //}
            //else
            //{
            //    RoznClickOnOff.IsEnabled = true;
            //    RestoranClickOnOff.IsEnabled = true;
            //    FastFudClickOnOff.IsEnabled = true;
            //    FitnesClickOnOff.IsEnabled = true;
            //    HotelClickOnOff.IsEnabled = true;
            //    AdresPortServer.IsEnabled = true;
            //    //OffInstalfrontB52.Visibility = Visibility.Collapsed;

            //}

            StartFront = 0;
        }

        public static void InitKeyOnOffFront()
        {
            AdminPanels.ButtonPanel = new string[16];
            AdminPanels.ButtonPanel[0] = "RoznClickOnOff";
            AdminPanels.ButtonPanel[1] = "RestoranClickOnOff";
            AdminPanels.ButtonPanel[2] = "FastFudClickOnOff";
            AdminPanels.ButtonPanel[3] = "FitnesClickOnOff";
            AdminPanels.ButtonPanel[4] = "HotelClickOnOff";
            AdminPanels.ButtonPanel[5] = "AvtoServOnOff";
            AdminPanels.ButtonPanel[6] = "AdrServerDate_IP4";
            AdminPanels.ButtonPanel[7] = "AdrServerDate_IP6";
            AdminPanels.ButtonPanel[8] = "RdpOnOff";
            AdminPanels.ButtonPanel[9] = "FrontFbd25OnOff";
            AdminPanels.ButtonPanel[10] = "FrontFbd30OnOff";
            AdminPanels.ButtonPanel[11] = "KeyLocFront";
            AdminPanels.ButtonPanel[12] = "FrontKeyNetOnOff";
            AdminPanels.ButtonPanel[13] = "FrontKeyTestOnOff";
            AdminPanels.ButtonPanel[14] = "FrontOfAdrOnOff_IP4";
            AdminPanels.ButtonPanel[15] = "CheckTTF16OnOff_Front";

            if (LoadTableRestr == 0) InitKeySystemFB("InitKey");
            InitKeyOnOff();
        }

        public static void InitTextFront()
        {
            AdminPanels.ButtonPanel = new string[11];
            AdminPanels.ButtonPanel[0] = "NamePsevdoData";
            AdminPanels.ButtonPanel[1] = "InstFrontOnOff";
            AdminPanels.ButtonPanel[2] = "FrontAdrIp4";
            AdminPanels.ButtonPanel[3] = "FrontAdrIp411";
            AdminPanels.ButtonPanel[4] = "FrontAdrIp6";
            AdminPanels.ButtonPanel[5] = "FrontAdrIp611";
            AdminPanels.ButtonPanel[6] = "AdresPortServer";
            AdminPanels.ButtonPanel[7] = "AdresPortFront";
            AdminPanels.ButtonPanel[8] = "PatchSRFront";
            AdminPanels.ButtonPanel[9] = "TcpPort";
            AdminPanels.ButtonPanel[10] = "IpName";

            if (LoadTableRestr == 0) InitKeySystemFB("InitText");
            InitTextOnOff();

        }

        private void UpdateFront_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            string[] AppUpdate = { "B52Fitness8.exe", "B52Hotel8.exe", "B52FrontOffice8.exe" };
            AnalizUpdate(AppUpdate);

            string Reposit = "", CurrentBack = "";

            string NameFront = PuthBack.Substring(PuthBack.LastIndexOf(@"\") + 1, PuthBack.Length - (PuthBack.LastIndexOf(@"\") + 1));


            if (File.Exists(SystemConecto.PutchApp + @"Repository\" + NameFront))
            {
                FileStream LocFile = System.IO.File.OpenRead(SystemConecto.PutchApp + @"Repository\" + NameFront);
                MD5 Locmd5 = new MD5CryptoServiceProvider();
                byte[] fileData = new byte[LocFile.Length];
                LocFile.Read(fileData, 0, (int)LocFile.Length);
                byte[] LoccheckSum = Locmd5.ComputeHash(fileData);
                Reposit = BitConverter.ToString(LoccheckSum).Replace("-", String.Empty);
                LocFile.Close();
            }

            if (File.Exists(PuthBack))
            {
                FileStream LocFile = System.IO.File.OpenRead(PuthBack);
                MD5 Locmd5 = new MD5CryptoServiceProvider();
                byte[] fileData = new byte[LocFile.Length];
                LocFile.Read(fileData, 0, (int)LocFile.Length);
                byte[] LoccheckSum = Locmd5.ComputeHash(fileData);
                CurrentBack = BitConverter.ToString(LoccheckSum).Replace("-", String.Empty);
                LocFile.Close();
            }


            string Versia = FileVersionInfo.GetVersionInfo(SystemConecto.PutchApp + @"Repository\" + NameFront).ToString();  // версия файла.
            Versia = Versia.Substring(Versia.IndexOf("FileVersion"), Versia.IndexOf("FileDescription") - Versia.IndexOf("FileVersion"));
            if (CurrentBack != Reposit)
            {
                File.Copy(SystemConecto.PutchApp + @"Repository\" + NameFront, PuthBack, true);
                var TextWindows = "Обновление выполено. " + Environment.NewLine + NameFront + Versia;

                System.Windows.Application.Current.Dispatcher.Invoke(new Action(delegate ()
                {
                    MainWindow ConectoWorkSpace_InW = AppStart.LinkMainWindow();
                    Window WinOblakoVerh_Info = new WinMessage(TextWindows, 1, 0); // создаем AutoClose
                    WinOblakoVerh_Info.Owner = ConectoWorkSpace_InW;
                    WinOblakoVerh_Info.Top = SystemConecto.WorkAreaDisplayDefault[0] + 350;
                    WinOblakoVerh_Info.Left = SystemConecto.WorkAreaDisplayDefault[1] + 650;
                    WinOblakoVerh_Info.ShowActivated = false;
                    WinOblakoVerh_Info.Show();

                }));
            }
            else
            {
                var TextWindows = "Обновление не требуется. " + Environment.NewLine + NameFront + Versia;

                System.Windows.Application.Current.Dispatcher.Invoke(new Action(delegate ()
                {
                    MainWindow ConectoWorkSpace_InW = AppStart.LinkMainWindow();
                    Window WinOblakoVerh_Info = new WinMessage(TextWindows, 1, 0); // создаем AutoClose
                    WinOblakoVerh_Info.Owner = ConectoWorkSpace_InW;
                    WinOblakoVerh_Info.Top = SystemConecto.WorkAreaDisplayDefault[0] + 350;
                    WinOblakoVerh_Info.Left = SystemConecto.WorkAreaDisplayDefault[1] + 650;
                    WinOblakoVerh_Info.ShowActivated = false;
                    WinOblakoVerh_Info.Show();

                }));
            }

        }


        // Процедура выбора БД под которую ставится фронт
        private void SetPuthBdFront_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

            if (Convert.ToInt32(AppStart.TableReestr["FrontFbd30OnOff"].ToString()) == 2 && Convert.ToInt32(AppStart.TableReestr["FrontFbd25OnOff"].ToString()) == 2)
            {
                var TextWindows = "Одновременно установлены в системе Firebird_2_5 и Firebird_3_0." + Environment.NewLine + "Для установки Фронт Б52 необходмио выбрать один из двух серверов, ненужный выключить.";
                InstallB52.MessageErorInst(TextWindows);
                return;
            }

            Administrator.AdminPanels.SetWinSetHub = "SetTcpIpFront";
            if (FrontPutchBD != "")
            {
                FrontPutchBD = SelectPuth = "";
                LabFrontOfNet.Visibility = Visibility.Visible;
                SetPuthBdFrontNet.Visibility = Visibility.Visible;
                FrontPuhtBd.Content = "С алиасом на диске компьютера:";
                FrontPuhtBd.Foreground = Brushes.Black;
                return;
            }
            // Открыть окно с дата грид для выбора текущего алиаса БД с которым будет работать Бек офис. 
            System.Windows.Application.Current.Dispatcher.Invoke(new Action(delegate ()
            {
                ConectoWorkSpace.Administrator.AdminPanels ConectoWorkSpace_InW = AppStart.LinkMainWindow("WAdminPanels");
                WinSetHub SetWindow = new WinSetHub();
                SetWindow.Owner = ConectoWorkSpace_InW; //this;
                SetWindow.Top = ConectoWorkSpace_InW.Top + 350;
                SetWindow.Left = ConectoWorkSpace_InW.Left + 700;
                SetWindow.Show();

            }));

        }

        // Процедура выбора БД под которую ставится бек по пути на любом компьютере
        private void SetPuthBdFrontNet_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

            if (Convert.ToInt32(AppStart.TableReestr["FrontFbd30OnOff"].ToString()) == 2 && Convert.ToInt32(AppStart.TableReestr["FrontFbd25OnOff"].ToString()) == 2)
            {
                var TextWindows = "Одновременно установлены в системе Firebird_2_5 и Firebird_3_0." + Environment.NewLine + "Для установки БекОфис Б52 необходмио выбрать один из двух серверов, ненужный выключить. ";
                InstallB52.MessageErorInst(TextWindows);
                return;
            }
            Administrator.AdminPanels.SetWinSetHub = "SetNetTcpIpBd";
            if (FrontPutchBD != "")
            {
                FrontPutchBD = SelectPuth = "";
                FrontPuhtBd.Visibility = Visibility.Visible;
                SetPuthBdFront.Visibility = Visibility.Visible;
                LabFrontOfNet.Content = "По пути на указанном компьютере:";
                LabFrontOfNet.Foreground = Brushes.Black;
                return;
            }
            PathFileBD_Click();
            if ((!PathFileBDText.ToUpper().Contains(".FDB") && !PathFileBDText.ToUpper().Contains(".GDB")) || PathFileBDText.Length == 0)
            {
                var TextWindows = "Расширение файла выбранной БД не является БД FireBird" + Environment.NewLine + "Необходмио выбрать другой файл. ";
                InstallB52.MessageErorInst(TextWindows);
                return;
            }
            ContentPuth = PathFileBDText.Length > 38 ? "..." + PathFileBDText.Substring(PathFileBDText.Length - 35, 35) : PathFileBDText;
            LabFrontOfNet.Content = ContentPuth;
            LabFrontOfNet.Foreground = Brushes.Green;
            FrontPutchBD = SelectPuth = PathFileBDText;
            FrontPuhtBd.Visibility = Visibility.Hidden;
            SetPuthBdFront.Visibility = Visibility.Hidden;
        }




        private void SetPuthFront_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Administrator.AdminPanels.NameObj = "";
            if (Convert.ToInt32(AppStart.TableReestr["FrontFbd30OnOff"].ToString()) == 2 && Convert.ToInt32(AppStart.TableReestr["FrontFbd25OnOff"].ToString()) == 2)
            {
                var TextWindows = "Одновременно установлены в системе Firebird_2_5 и Firebird_3_0." + Environment.NewLine + "Для установки БекОфис Б52 необходмио выбрать один из двух серверов, ненужный выключить. ";
                InstallB52.MessageErorInst(TextWindows);
                return;
            }

            FolderFrontPuth = "";
            var dlg = new System.Windows.Forms.FolderBrowserDialog();
            dlg.Description = "Путь размещения фронта";
            System.Windows.Forms.DialogResult result = dlg.ShowDialog();
            if (result == System.Windows.Forms.DialogResult.OK)
            {
                FolderFrontPuth = dlg.SelectedPath + (dlg.SelectedPath.LastIndexOf(@"\") <= 2 && dlg.SelectedPath.Length <= 3 ? "" : @"\");
                ContentPuth = FolderFrontPuth.Length > 38 ? "..." + FolderFrontPuth.Substring(FolderFrontPuth.Length - 35, 35) : FolderFrontPuth;
                FrontPuhtSoft.Content = "Фронт: " + ContentPuth;
                FrontPuhtSoft.Foreground = Brushes.Green;
            }
            else
            {
                FrontPuhtSoft.Content = "Шаг 2. Установить путь размещения:";
                FrontPuhtSoft.Foreground = Brushes.Indigo;
                return;
            }
            InstallB52.BackFrontIsEnabledTrue(2);
        }

        // процедура  включения установки Бек Офиса Б52 на базе FieBird 2.5.
        private void FrontFbd25OnOff_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            string StatusPic = "on_off_2.png";
            string SetKeyValue = "2";
            FrontFbd30OnOff.IsEnabled = true;
            FrontFbd25OnOff.IsEnabled = true;
            if (FrontFbd25OnOff.IsEnabled == true && Convert.ToInt32(AppStart.TableReestr["FrontFbd25OnOff"].ToString()) == 2)
            {
                if (Convert.ToInt32(AppStart.TableReestr["FrontFbd30OnOff"].ToString()) == 2)
                {
                    StatusPic = "on_off_1.png";
                    SetKeyValue = "0";
                    Interface.CurrentStateInst("FrontFbd25OnOff", SetKeyValue, StatusPic, FrontFbd25OnOff);
                    AdresPortServer.Text = "3056";
                    Administrator.AdminPanels.UpdateKeyReestr("AdresPortServer", AdresPortServer.Text);

                }

            }
            else
            {
                if (Convert.ToInt32(AppStart.TableReestr["FrontFbd30OnOff"].ToString()) == 2)
                {

                    Interface.CurrentStateInst("FrontFbd25OnOff", SetKeyValue, StatusPic, FrontFbd25OnOff);
                    AdresPortServer.Text = "3055";
                    Administrator.AdminPanels.UpdateKeyReestr("AdresPortServer", AdresPortServer.Text);
                    StatusPic = "on_off_1.png";
                    SetKeyValue = "0";
                    Interface.CurrentStateInst("FrontFbd30OnOff", SetKeyValue, StatusPic, FrontFbd30OnOff);

                }
            }

        }

        // процедура  включения установки Бек Офиса Б52 на базе FieBird 2.5.
        private void FrontFbd30OnOff_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            string StatusPic = "on_off_2.png";
            string SetKeyValue = "2";
            FrontFbd25OnOff.IsEnabled = true;
            FrontFbd30OnOff.IsEnabled = true;
            if (FrontFbd30OnOff.IsEnabled == true && Convert.ToInt32(AppStart.TableReestr["FrontFbd30OnOff"].ToString()) == 2)
            {
                if (Convert.ToInt32(AppStart.TableReestr["FrontFbd25OnOff"].ToString()) == 2)
                {
                    StatusPic = "on_off_1.png";
                    SetKeyValue = "0";
                    Interface.CurrentStateInst("FrontFbd30OnOff", SetKeyValue, StatusPic, FrontFbd30OnOff);
                    AdresPortServer.Text = "3055";
                    Administrator.AdminPanels.UpdateKeyReestr("AdresPortServer", AdresPortServer.Text);

                }
            }
            else
            {
                if (Convert.ToInt32(AppStart.TableReestr["FrontFbd25OnOff"].ToString()) == 2)
                {

                    Interface.CurrentStateInst("FrontFbd30OnOff", SetKeyValue, StatusPic, FrontFbd30OnOff);
                    AdresPortServer.Text = "3056";
                    Administrator.AdminPanels.UpdateKeyReestr("AdresPortServer", AdresPortServer.Text);
                    StatusPic = "on_off_1.png";
                    SetKeyValue = "0";
                    Interface.CurrentStateInst("FrontFbd25OnOff", SetKeyValue, StatusPic, FrontFbd25OnOff);

                }
            }

        }

        // 4 процедуры установки фронт Б52 для розницы, кафе бара, фаст фуда, селф линия, доставка еды.

        private void FrontRoznClickOnOff_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {


            Administrator.AdminPanels.NameObj = "RoznClickOnOff";
            Interface.ObjektOnOff("RoznClickOnOff", ref RoznClickOnOff, new Install.Metods { NameClass = "ConectoWorkSpace.InstallB52", Install = "InstallFrontB52", UnInstal = "UnInstallFrontB52" });

        }
        private void FrontKafeClickOnOff_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {

            Administrator.AdminPanels.NameObj = "RestoranClickOnOff";
            Interface.ObjektOnOff("RestoranClickOnOff", ref RestoranClickOnOff, new Install.Metods { NameClass = "ConectoWorkSpace.InstallB52", Install = "InstallFrontB52", UnInstal = "UnInstallFrontB52" });

        }

        private void FrontFastClickOnOff_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {

            Administrator.AdminPanels.NameObj = "FastFudClickOnOff";
            Interface.ObjektOnOff("FastFudClickOnOff", ref FastFudClickOnOff, new Install.Metods { NameClass = "ConectoWorkSpace.InstallB52", Install = "InstallFrontB52", UnInstal = "UnInstallFrontB52" });

        }

        private void FrontSelfClickOnOff_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {

            Administrator.AdminPanels.NameObj = "FitnesClickOnOff";
            Interface.ObjektOnOff("FitnesClickOnOff", ref FitnesClickOnOff, new Install.Metods { NameClass = "ConectoWorkSpace.InstallB52", Install = "InstallFrontB52", UnInstal = "UnInstallFrontB52" });

        }

        private void FrontFoodClickOnOff_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {

            Administrator.AdminPanels.NameObj = "HotelClickOnOff";
            Interface.ObjektOnOff("HotelClickOnOff", ref HotelClickOnOff, new Install.Metods { NameClass = "ConectoWorkSpace.InstallB52", Install = "InstallFrontB52", UnInstal = "UnInstallFrontB52" });

        }



        // выполнить запуск на выполнение ФронтОфисБ52 

        private void FrontRoznRun_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            Administrator.AdminPanels.NameObj = "FrontRozn";
            ImageObj = "RoznClickOnOff";
            InstallB52.RunFrontOficceB52();
        }
        private void FrontRestoranRun_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            Administrator.AdminPanels.NameObj = "FrontRestoran";
            ImageObj = "RestoranClickOnOff";
            InstallB52.RunFrontOficceB52();
        }
        private void FrontFastFudRun_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            Administrator.AdminPanels.NameObj = "FrontFastFud";
            ImageObj = "FastFudClickOnOff";
            InstallB52.RunFrontOficceB52();
        }
        private void FrontFitnesRun_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            Administrator.AdminPanels.NameObj = "FrontFitnes";
            ImageObj = "FitnesClickOnOff";
            InstallB52.RunFrontOficceB52();
        }
        private void FrontHotelRun_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            Administrator.AdminPanels.NameObj = "FrontHotel";
            ImageObj = "HotelClickOnOff";
            InstallB52.RunFrontOficceB52();
        }

        // Включить Автопределение сервера данных. Фронт клиент - касса
        private void AvtoServOnOff_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            Interface.ObjektOnOff("AvtoServOnOff", ref AvtoServOnOff, new Install.Metods { NameClass = "None", Install = "None", UnInstal = "None" });
        }

        // Включить РДП терминал Фронт клиент - касса
        private void RdpOnOff_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            Interface.ObjektOnOff("RdpOnOff", ref RdpOnOff, new Install.Metods { NameClass = "None", Install = "None", UnInstal = "None" });
        }


        // Включить выключить ввод  адреса IP4 серевера данных Фронт клиент - касса
        private void AdrServerDate_IP4_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {

            Interface.MouseLeftButtonUp("AdrServerDate_IP4", "AdrServerDate_IP6", ref AdrServerDate_IP4, ref AdrServerDate_IP6);
            Interface.TextBoxTrueFalse("AdrServerDate_IP4", "WAdminPanels", FrontIP61, 6, FrontTextIp41);
        }

        // Включить выключить ввод  адреса IP6 серевера данных Фронт клиент - касса
        private void AdrServerDate_IP6_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            Interface.MouseLeftButtonUp("AdrServerDate_IP6", "AdrServerDate_IP4", ref AdrServerDate_IP6, ref AdrServerDate_IP4);
            Interface.TextBoxTrueFalse("AdrServerDate_IP6", "WAdminPanels", FrontTextIp41, 6, FrontIP61);

        }

        // Первый адрес IP4 Фронт офис
        private void FrontTextIp41_KeyUp(object sender, KeyEventArgs e)
        {
            Interface.SaveTextBox("FrontTextIp41", FrontTextIp41, 0);
        }
        // Второй адрес IP4 Фронт офис
        private void FrontTextIp42_KeyUp(object sender, KeyEventArgs e)
        {
            Interface.SaveTextBox("FrontTextIp42", FrontTextIp42, 0);

        }
        // Третий адрес IP4 Фронт офис
        private void FrontTextIp43_KeyUp(object sender, KeyEventArgs e)
        {
            Interface.SaveTextBox("FrontTextIp43", FrontTextIp43, 0);
        }
        // Четвертый адрес IP4 Фронт офис
        private void FrontTextIp44_KeyUp(object sender, KeyEventArgs e)
        {
            Interface.SaveTextBox("FrontTextIp44", FrontTextIp44, 0);
        }

        private void FrontIp44_LostFocus(object sender, RoutedEventArgs e)
        {
            AdrIp4[0] = FrontTextIp41.Text + "." + FrontTextIp42.Text + "." + FrontTextIp43.Text + "." + FrontTextIp44.Text;
            Administrator.AdminPanels.UpdateKeyReestr("FrontAdrIp4", AdrIp4[0]);

        }

        private void FrontIP61_KeyUp(object sender, KeyEventArgs e)
        {
            Interface.SaveTextBox("FrontIP61", FrontIP61, 0);
        }
        private void FrontIP62_KeyUp(object sender, KeyEventArgs e)
        {
            Interface.SaveTextBox("FrontIP62", FrontIP62, 0);
        }
        private void FrontIP63_KeyUp(object sender, KeyEventArgs e)
        {
            Interface.SaveTextBox("FrontIP63", FrontIP63, 0);
        }
        private void FrontIP64_KeyUp(object sender, KeyEventArgs e)
        {
            Interface.SaveTextBox("FrontIP64", FrontIP64, 0);
        }
        private void FrontIP65_KeyUp(object sender, KeyEventArgs e)
        {
            Interface.SaveTextBox("FrontIP65", FrontIP65, 0);
        }
        private void FrontIP66_KeyUp(object sender, KeyEventArgs e)
        {
            Interface.SaveTextBox("FrontIP66", FrontIP66, 0);
        }
        private void FrontIP66_LostFocus(object sender, RoutedEventArgs e)
        {
            AdrIp6[0] = FrontIP61.Text + "." + FrontIP62.Text + "." + FrontIP63.Text + "." + FrontIP64.Text + "." + FrontIP65.Text + "." + FrontIP66.Text;
            Administrator.AdminPanels.UpdateKeyReestr("FrontAdrIp6", AdrIp6[0]);

        }


        private void GnclientFront_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

        }

        public static void AptuneGrid_Puth(string CurrentSetBD)
        {
            List<AptuneGridFront> result = new List<AptuneGridFront>(1);
            DBConecto.DBopenFBConectionMemory(SystemBD.ConnnStringSystem().ToString(), "", "FbSystem");
            FbCommand UpdateKey = new FbCommand(CurrentSetBD, DBConecto.bdFbSystemConect);
            UpdateKey.CommandTimeout = 3;
            UpdateKey.CommandType = CommandType.Text;
            FbDataReader reader = UpdateKey.ExecuteReader();
            while (reader.Read())
            {
                result.Add(new AptuneGridFront(reader[0].ToString(), reader[1].ToString(), reader[2].ToString(), reader[3].ToString()));
            }
            reader.Close();
            UpdateKey.Dispose();
            DBConecto.DBcloseFBConectionMemory("FbSystem");
            System.Windows.Application.Current.Dispatcher.Invoke(new Action(delegate ()
            {
                WinSetAptune ConectoWorkSpace_InW = AppStart.LinkMainWindow("WinSetAptuneW");
                ConectoWorkSpace_InW.TablAptune.ItemsSource = result;
            }));

        }

        // Процедура выбора строки с настройками порта, алиаса Ип адреса для фронта
        public static void AptuneGrid()
        {
            string InsertExecute = "SELECT count(*) from REESTRFRONT";
            if (Administrator.AdminPanels.SetWinSetHub == "AptuneBack" || Administrator.AdminPanels.SetWinSetHub == "GnclientBack")
            {
                InsertExecute = "SELECT count(*) from REESTRBACK";
            }
            DBConecto.DBopenFBConectionMemory(SystemBD.ConnnStringSystem().ToString(), "", "FbSystem");
            DBConecto.UniQuery CountQuery = new DBConecto.UniQuery(InsertExecute, "FB");
            string CountTable = CountQuery.ExecuteUNIScalar() == null ? "" : CountQuery.ExecuteUNIScalar().ToString();
            if (Convert.ToUInt32(CountTable) != 0)
            {
                System.Windows.Application.Current.Dispatcher.Invoke(new Action(delegate ()
                {
                    WinSetAptune ConectoWorkSpace_InW = AppStart.LinkMainWindow("WinSetAptuneW");
                    if (ConectoWorkSpace_InW.TablAptune.SelectedItem != null)
                    {
                        AptuneGridFront path = ConectoWorkSpace_InW.TablAptune.SelectedItem as AptuneGridFront;
                        FolderBack = path.Front;
                        ConectBack = path.Conect;
                        ServerBack = path.Server;
                        PuthBack = path.Puth;


                    }
                }));
            }
            DBConecto.DBcloseFBConectionMemory("FbSystem");
        }

        #endregion


        #region Закладка "Бек офис"

        // Процедура анализа состояния переключателей закладки Бек Офис Б52
        private void InitPanel_BackOffice(object sender, MouseButtonEventArgs e)
        {
            InitPanelBackOffice();
        }
        // Процедура анализа состояния переключателей закладки Бек Офис Б52
        public void InitPanelBackOffice()
        {
            // Активная закладка Бек Офис Б52
            CurrentBack = 1; CurrentFront = 0;
            if (StartBack == 1)
            {
                AddBackRozn.Visibility = Visibility.Hidden;
                AddBackRestoran.Visibility = Visibility.Hidden;
                AddBackFast.Visibility = Visibility.Hidden;
                AddBackFithes.Visibility = Visibility.Hidden;
                AddBackHotel.Visibility = Visibility.Hidden;
                AddBackMix.Visibility = Visibility.Hidden;
                var TextWindows = "Выполняется идентификация данных." + Environment.NewLine + "Пожалуйста подождите. ";
                int AutoClose = 1;
                int MesaggeTop = -170;
                int MessageLeft = 300;
                ConectoWorkSpace.MainWindow.MessageEnd(TextWindows, AutoClose, MesaggeTop, MessageLeft);
                InitKeyOnOffBack();
                InitPanel_();
                InitTextBack();
                CheckInstalBack25();
                ChangeAliasBack.IsEnabled = false;
                // Получение текущего IP адреса компа
                var host = Dns.GetHostEntry(Dns.GetHostName());
                foreach (var ip in host.AddressList)
                {
                    if (ip.AddressFamily == AddressFamily.InterNetwork)
                    {
                        IpAdresHome_Back.Content = ip.ToString();
                    }
                }
                string StatusPic = Convert.ToInt32(AppStart.TableReestr["BackOfServKeyOnOff"].ToString()) == 2 ? "2" : "";
                string SetKeyValue = Convert.ToInt32(AppStart.TableReestr["BackOfServKeyOnOff"].ToString()) == 2 ? "on_off_2.png" : "on_off_1.png";
                var pic = (Image)LogicalTreeHelper.FindLogicalNode(this, "BackKeyServOnOff");
                var uriSource = new Uri(@"/Conecto®%20WorkSpace;component/Images/" + SetKeyValue, UriKind.Relative);
                pic.Source = new BitmapImage(uriSource);
                Administrator.AdminPanels.UpdateKeyReestr("BackKeyServOnOff", StatusPic);
                string StatusLoc = Convert.ToInt32(AppStart.TableReestr["SetLocKeyOnOff"].ToString()) == 2 ? "on_off_2.png" : "on_off_1.png";
                var PicLoc = (Image)LogicalTreeHelper.FindLogicalNode(this, "BackKeyLocOnOff");
                var uriSourceLoc = new Uri(@"/Conecto®%20WorkSpace;component/Images/" + StatusLoc, UriKind.Relative);
                PicLoc.Source = new BitmapImage(uriSourceLoc);

                if (Convert.ToInt32(AppStart.TableReestr["CheckTTF16"]) == 0)
                {
                    AppStart.RenderInfo Arguments01 = new AppStart.RenderInfo() { };
                    Arguments01.argument1 = "1";
                    Arguments01.argument2 = "";
                    Thread thStartTimer01 = new Thread(CheckTTF16);
                    thStartTimer01.SetApartmentState(ApartmentState.STA);
                    thStartTimer01.IsBackground = true; // Фоновый поток
                    thStartTimer01.Start(Arguments01);
                    InstallB52.IntThreadStart++;

                }
                string SetTTF16Value = Convert.ToInt32(AppStart.TableReestr["CheckTTF16OnOff"].ToString()) == 2 ? "on_off_2.png" : "on_off_1.png";
                AppStart.TableReestr["CheckTTF16"] = AppStart.TableReestr["CheckTTF16OnOff"];
                var picTTF16 = (Image)LogicalTreeHelper.FindLogicalNode(this, "CheckTTF16OnOff");
                var uriSourceTTF16 = new Uri(@"/Conecto®%20WorkSpace;component/Images/" + SetTTF16Value, UriKind.Relative);
                pic.Source = new BitmapImage(uriSource);
                var picTTF16_Front = (Image)LogicalTreeHelper.FindLogicalNode(this, "CheckTTF16OnOff_Front");
                var uriSourceTTF16_Front = new Uri(@"/Conecto®%20WorkSpace;component/Images/" + SetTTF16Value, UriKind.Relative);
                pic.Source = new BitmapImage(uriSource);

                Interface.CurrentStateInst("BackFbd25OnOff", Convert.ToInt32(AppStart.TableReestr["ServFB25OnOff"].ToString()) == 2 ? "2" : "0", Convert.ToInt32(AppStart.TableReestr["ServFB25OnOff"].ToString()) == 2 ? "on_off_2.png" : "on_off_1.png", BackFbd25OnOff);
                Interface.CurrentStateInst("BackFbd30OnOff", Convert.ToInt32(AppStart.TableReestr["ServFB30OnOff"].ToString()) == 2 ? "2" : "0", Convert.ToInt32(AppStart.TableReestr["ServFB30OnOff"].ToString()) == 2 ? "on_off_2.png" : "on_off_1.png", BackFbd30OnOff);




                if (Convert.ToInt32(AppStart.TableReestr["BackOfRoznOnOff"].ToString()) == 2) AddBackRozn.Visibility = Visibility.Visible;
                if (Convert.ToInt32(AppStart.TableReestr["BackOfRestoranOnOff"].ToString()) == 2) AddBackRestoran.Visibility = Visibility.Visible;
                if (Convert.ToInt32(AppStart.TableReestr["BackOfFastFudOnOff"].ToString()) == 2) AddBackFast.Visibility = Visibility.Visible;
                if (Convert.ToInt32(AppStart.TableReestr["BackOfFitnesOnOff"].ToString()) == 2) AddBackFithes.Visibility = Visibility.Visible;
                if (Convert.ToInt32(AppStart.TableReestr["BackOfHotelOnOff"].ToString()) == 2) AddBackHotel.Visibility = Visibility.Visible;
                if (Convert.ToInt32(AppStart.TableReestr["BackOfMixOnOff"].ToString()) == 2) AddBackMix.Visibility = Visibility.Visible;

                StartBack = 0;
            }

            AdrIp4LenchBack = Ip4 = AppStart.TableReestr["SetTextIp4BackOf"];
            if (StartBack == 1)
            {
                if (Ip4.Length == 0)
                {
                    AdrIp4LenchBack = Ip4 = "127.0.0.1";
                    Administrator.AdminPanels.UpdateKeyReestr("SetTextIp4BackOf", Ip4);
                    BackOfIp4Text1.IsEnabled = true;
                    BackOfIp4Text2.IsEnabled = true;
                    BackOfIp4Text3.IsEnabled = true;
                    BackOfIp4Text4.IsEnabled = true;
                }

                if ((Convert.ToInt32(AppStart.TableReestr["BackOfAdrServerDate_IP4"].ToString()) == 0 || Convert.ToInt32(AppStart.TableReestr["BackOfAdrServerDate_IP4"].ToString()) == 3) && Ip4.Length == 0)
                {
                    BackOfIp4Text1.IsEnabled = false;
                    BackOfIp4Text2.IsEnabled = false;
                    BackOfIp4Text3.IsEnabled = false;
                    BackOfIp4Text4.IsEnabled = false;

                }
                else
                {
                    if (Convert.ToInt32(AppStart.TableReestr["BackFbd25OnOff"].ToString()) == 0 && Convert.ToInt32(AppStart.TableReestr["BackFbd30OnOff"].ToString()) == 0)
                    {
                        BackOfIp4Text1.IsEnabled = false;
                        BackOfIp4Text2.IsEnabled = false;
                        BackOfIp4Text3.IsEnabled = false;
                        BackOfIp4Text4.IsEnabled = false;
                        BackOfAdrServerDate_IP4.IsEnabled = false;
                        Interface.CurrentStateInst("BackOfAdrServerDate_IP4", "0", "on_off_1.png", BackOfAdrServerDate_IP4);

                    }
                    else
                    {
                        BackOfIp4Text1.IsEnabled = true;
                        BackOfIp4Text2.IsEnabled = true;
                        BackOfIp4Text3.IsEnabled = true;
                        BackOfIp4Text4.IsEnabled = true;
                        BackOfAdrServerDate_IP4.IsEnabled = true;
                        Interface.CurrentStateInst("BackOfAdrServerDate_IP4", "2", "on_off_2.png", BackOfAdrServerDate_IP4);

                    }

                    BackOfIp6Text1.IsEnabled = false;
                    BackOfIp6Text2.IsEnabled = false;
                    BackOfIp6Text3.IsEnabled = false;
                    BackOfIp6Text4.IsEnabled = false;
                    BackOfIp6Text5.IsEnabled = false;
                    BackOfIp6Text6.IsEnabled = false;
                }

            }

            for (int indPoint = 1; indPoint <= 3; indPoint = indPoint + 1)
            {
                int position = Ip4.IndexOf(".");
                if (position <= 0) { break; }
                switch (indPoint)
                {
                    case 1:
                        BackOfIp4Text1.Text = Ip4.Substring(0, position);
                        break;
                    case 2:
                        BackOfIp4Text2.Text = Ip4.Substring(0, position);
                        break;
                    case 3:
                        BackOfIp4Text3.Text = Ip4.Substring(0, position);
                        break;
                }
                Ip4 = Ip4.Substring(position + 1);
            }
            BackOfIp4Text4.Text = Ip4.Substring(0);

            if (Convert.ToInt32(AppStart.TableReestr["BackOfAdrServerDate_IP6"].ToString()) == 0 || Convert.ToInt32(AppStart.TableReestr["BackOfAdrServerDate_IP6"].ToString()) == 3)
            {
                BackOfIp6Text1.IsEnabled = false;
                BackOfIp6Text2.IsEnabled = false;
                BackOfIp6Text3.IsEnabled = false;
                BackOfIp6Text4.IsEnabled = false;
                BackOfIp6Text5.IsEnabled = false;
                BackOfIp6Text6.IsEnabled = false;
            }
            else
            {
                BackOfIp4Text1.IsEnabled = false;
                BackOfIp4Text2.IsEnabled = false;
                BackOfIp4Text3.IsEnabled = false;
                BackOfIp4Text4.IsEnabled = false;
            }
            AdrIp6LenchBack = Ip6 = AppStart.TableReestr["SetTextIp6BackOf"]; // (string)AppStart.rkAppSetingAllUser.GetValue("SetTextIp6BackOf");
            for (int indPoint = 1; indPoint <= 5; indPoint = indPoint + 1)
            {
                int position = Ip6.IndexOf(".");
                if (position <= 0) { break; }
                switch (indPoint)
                {
                    case 1:
                        BackOfIp6Text1.Text = Ip6.Substring(0, position);
                        break;
                    case 2:
                        BackOfIp6Text2.Text = Ip6.Substring(0, position);
                        break;
                    case 3:
                        BackOfIp6Text3.Text = Ip6.Substring(0, position);
                        break;
                    case 4:
                        BackOfIp6Text4.Text = Ip6.Substring(0, position);
                        break;
                    case 5:
                        BackOfIp6Text5.Text = Ip6.Substring(0, position);
                        break;
                }
                Ip6 = Ip6.Substring(position + 1);
            }
            BackOfIp6Text6.Text = Ip6.Substring(0);

            if (Convert.ToInt32(AppStart.TableReestr["ServFB25OnOff"].ToString()) == 0 && Convert.ToInt32(AppStart.TableReestr["ServFB30OnOff"].ToString()) == 0)
            {
                BackOfAdrServerDate_IP4.IsEnabled = false;
                BackOfAdrServerDate_IP6.IsEnabled = false;
                BackOfAvtoServOnOff.IsEnabled = false;
                BackOfRdpOnOff.IsEnabled = false;
                BackOfRoznOnOff.IsEnabled = false;
                BackOfRestoranOnOff.IsEnabled = false;
                BackOfFastFudOnOff.IsEnabled = false;
                BackOfFitnesOnOff.IsEnabled = false;
                BackOfHotelOnOff.IsEnabled = false;
                BackFbd25OnOff.IsEnabled = false;
                BackFbd30OnOff.IsEnabled = false;
                BackOfRoznOnOff.IsEnabled = false;
                BackRunRestoran.IsEnabled = false;
                BackRunFast.IsEnabled = false;
                BackRunFitnes.IsEnabled = false;
                BackRunHotel.IsEnabled = false;
                BackOfAdresPortServer.IsEnabled = false;
                LabInstalB52.Visibility = Visibility.Collapsed;
                //OffInstalB52.Visibility = Visibility.Visible;
            }
            else
            {
                BackOfAdrServerDate_IP4.IsEnabled = true;
                BackOfAdrServerDate_IP6.IsEnabled = true;
                BackOfAvtoServOnOff.IsEnabled = true;
                BackOfRdpOnOff.IsEnabled = true;
                BackOfRoznOnOff.IsEnabled = true;
                BackOfRestoranOnOff.IsEnabled = true;
                BackOfFastFudOnOff.IsEnabled = true;
                BackOfFitnesOnOff.IsEnabled = true;
                BackOfHotelOnOff.IsEnabled = true;
                BackFbd25OnOff.IsEnabled = true;
                BackFbd30OnOff.IsEnabled = true;
                BackOfRoznOnOff.IsEnabled = true;
                BackRunRestoran.IsEnabled = true;
                BackRunFast.IsEnabled = true;
                BackRunFitnes.IsEnabled = true;
                BackRunHotel.IsEnabled = true;
                BackOfAdresPortServer.IsEnabled = true;
                LabInstalB52.Visibility = Visibility.Visible;
                //OffInstalB52.Visibility = Visibility.Collapsed;
            }


        }

        private void TTF16OnOff_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            var TextWindows = "Выполняется установка TTF16.ocx." + Environment.NewLine + "Пожалуйста подождите. ";
            int AutoClose = 1;
            int MesaggeTop = -600;
            int MessageLeft = 0;
            InstallB52.MessageEnd(TextWindows, AutoClose, MesaggeTop, MessageLeft);
            NameObj = ImageObj = "CheckTTF16OnOff";
            Interface.ObjektOnOff("CheckTTF16OnOff", ref CheckTTF16OnOff, new Install.Metods { NameClass = "ConectoWorkSpace.InstallB52", Install = "InstallTTF16", UnInstal = "UnInstallTTF16" });
        }


        private void TTF16OnOff_Front_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            var TextWindows = "Выполняется установка TTF16.ocx." + Environment.NewLine + "Пожалуйста подождите. ";
            int AutoClose = 1;
            int MesaggeTop = -600;
            int MessageLeft = 0;
            InstallB52.MessageEnd(TextWindows, AutoClose, MesaggeTop, MessageLeft);
            NameObj = ImageObj = "CheckTTF16OnOff_Front";
            Interface.ObjektOnOff("CheckTTF16OnOff_Front", ref CheckTTF16OnOff_Front, new Install.Metods { NameClass = "ConectoWorkSpace.InstallB52", Install = "InstallTTF16", UnInstal = "UnInstallTTF16" });
        }

        public static void InitKeyOnOffBack()
        {
            AdminPanels.ButtonPanel = new string[17];
            AdminPanels.ButtonPanel[0] = "BackOfRoznOnOff"; // розница
            AdminPanels.ButtonPanel[1] = "BackOfFitnesOnOff"; // фитнес
            AdminPanels.ButtonPanel[2] = "BackOfRestoranOnOff"; // ресторан
            AdminPanels.ButtonPanel[3] = "BackOfFastFudOnOff"; // Фаст-фуд
            AdminPanels.ButtonPanel[4] = "BackOfHotelOnOff"; // Готель
            AdminPanels.ButtonPanel[5] = "BackOfAvtoServOnOff";
            AdminPanels.ButtonPanel[6] = "BackOfAdrServerDate_IP4";
            AdminPanels.ButtonPanel[7] = "BackOfAdrServerDate_IP6";
            AdminPanels.ButtonPanel[8] = "BackOfRdpOnOff";
            AdminPanels.ButtonPanel[9] = "BackKeyLocOnOff";
            AdminPanels.ButtonPanel[10] = "BackKeyNetOnOff";
            AdminPanels.ButtonPanel[11] = "BackKeyServOnOff";
            AdminPanels.ButtonPanel[12] = "BackFbd25OnOff";
            AdminPanels.ButtonPanel[13] = "BackFbd30OnOff";
            AdminPanels.ButtonPanel[14] = "BackKeyTestOnOff";
            AdminPanels.ButtonPanel[15] = "CheckTTF16OnOff";
            AdminPanels.ButtonPanel[16] = "BackOfMixOnOff";

            if (LoadTableRestr == 0) InitKeySystemFB("InitKey");
            InitKeyOnOff();
        }
        // Процедура инициализации текстовых констант в беке
        public static void InitTextBack()
        {
            AdminPanels.ButtonPanel = new string[11];
            AdminPanels.ButtonPanel[0] = "BackOfNamePsevdoData";
            AdminPanels.ButtonPanel[1] = "SetTextIp4BackOf";
            AdminPanels.ButtonPanel[2] = "SetTextIp6BackOf";
            AdminPanels.ButtonPanel[3] = "SetTextAdrDataIp4BackOf";
            AdminPanels.ButtonPanel[4] = "SetTextAdrDataIp6BackOf";
            AdminPanels.ButtonPanel[5] = "InstBackOnOff";
            AdminPanels.ButtonPanel[6] = "BackOfAdresPortServer";
            AdminPanels.ButtonPanel[7] = "BackPortServKey";
            AdminPanels.ButtonPanel[8] = "BackOfTcpPort";
            AdminPanels.ButtonPanel[9] = "BackOfIpName";
            AdminPanels.ButtonPanel[10] = "BackOfAlias";

            if (LoadTableRestr == 0) InitKeySystemFB("InitText");
            InitTextOnOff();

        }
        // Функция модификации  соединения в файле gnclient.ini для бек и фронт Офиса и лицензионных ключей. 
        public static void ChangeGnclient(string PatchFile, string TcpPort, string IpName)
        {

            if (SystemConecto.File_(PatchFile + "gnclient.ini", 5))
            {
                int Idcount = 0;
                Encoding code = Encoding.Default;
                string[] fileLines = File.ReadAllLines(PatchFile + "gnclient.ini", code);
                foreach (string x in fileLines)
                {
                    if (x.Contains("TCP_PORT") == true) fileLines[Idcount] = "TCP_PORT =" + TcpPort;
                    if (x.Contains("IP_NAME") == true) fileLines[Idcount] = "IP_NAME =" + IpName;
                    Idcount++;
                }
                File.WriteAllLines(PatchFile + "gnclient.ini", fileLines, code);
            }
        }


        private void DeleteBack_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            PuthBackOff = PuthBack;
            NameObj = ImageObj = "BackOf" + FolderBack + "OnOff";
            InstallB52.UnInstallBackB52();


        }


        private void UpdateBack_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            string[] AppUpdate = { "B52BackOffice8.exe" };
            AnalizUpdate(AppUpdate);


            string Reposit = "", CurrentBack = "";

            if (File.Exists(SystemConecto.PutchApp + @"Repository\B52BackOffice8.exe"))
            {
                FileStream LocFile = System.IO.File.OpenRead(SystemConecto.PutchApp + @"Repository\B52BackOffice8.exe");
                MD5 Locmd5 = new MD5CryptoServiceProvider();
                byte[] fileData = new byte[LocFile.Length];
                LocFile.Read(fileData, 0, (int)LocFile.Length);
                byte[] LoccheckSum = Locmd5.ComputeHash(fileData);
                Reposit = BitConverter.ToString(LoccheckSum).Replace("-", String.Empty);
                LocFile.Close();
            }

            if (File.Exists(PuthBack))
            {
                FileStream LocFile = System.IO.File.OpenRead(PuthBack);
                MD5 Locmd5 = new MD5CryptoServiceProvider();
                byte[] fileData = new byte[LocFile.Length];
                LocFile.Read(fileData, 0, (int)LocFile.Length);
                byte[] LoccheckSum = Locmd5.ComputeHash(fileData);
                CurrentBack = BitConverter.ToString(LoccheckSum).Replace("-", String.Empty);
                LocFile.Close();
            }


            string Versia = FileVersionInfo.GetVersionInfo(SystemConecto.PutchApp + @"Repository\B52BackOffice8.exe").ToString();  // версия файла.
            Versia = Versia.Substring(Versia.IndexOf("FileVersion"), Versia.IndexOf("FileDescription") - Versia.IndexOf("FileVersion"));
            if (CurrentBack != Reposit)
            {
                File.Copy(SystemConecto.PutchApp + @"Repository\B52BackOffice8.exe", PuthBack, true);
                var TextWindows = "Обновление выполено. " + Environment.NewLine + "B52BackOffice8.exe. " + Versia;

                System.Windows.Application.Current.Dispatcher.Invoke(new Action(delegate ()
                {
                    MainWindow ConectoWorkSpace_InW = AppStart.LinkMainWindow();
                    Window WinOblakoVerh_Info = new WinMessage(TextWindows, 1, 0); // создаем AutoClose
                    WinOblakoVerh_Info.Owner = ConectoWorkSpace_InW;
                    WinOblakoVerh_Info.Top = SystemConecto.WorkAreaDisplayDefault[0] + 350;
                    WinOblakoVerh_Info.Left = SystemConecto.WorkAreaDisplayDefault[1] + 650;
                    WinOblakoVerh_Info.ShowActivated = false;
                    WinOblakoVerh_Info.Show();

                }));
            }
            else
            {
                var TextWindows = "Обновление не требуется. " + Environment.NewLine + "B52BackOffice8.exe. " + Versia;

                System.Windows.Application.Current.Dispatcher.Invoke(new Action(delegate ()
                {
                    MainWindow ConectoWorkSpace_InW = AppStart.LinkMainWindow();
                    Window WinOblakoVerh_Info = new WinMessage(TextWindows, 1, 0); // создаем AutoClose
                    WinOblakoVerh_Info.Owner = ConectoWorkSpace_InW;
                    WinOblakoVerh_Info.Top = SystemConecto.WorkAreaDisplayDefault[0] + 350;
                    WinOblakoVerh_Info.Left = SystemConecto.WorkAreaDisplayDefault[1] + 650;
                    WinOblakoVerh_Info.ShowActivated = false;
                    WinOblakoVerh_Info.Show();

                }));
            }

        }

        // Процедура выбора БД под которую ставится бек
        private void SetPuthBdBack_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

            if (Convert.ToInt32(AppStart.TableReestr["BackFbd30OnOff"].ToString()) == 2 && Convert.ToInt32(AppStart.TableReestr["BackFbd25OnOff"].ToString()) == 2)
            {
                var TextWindows = "Одновременно установлены в системе Firebird_2_5 и Firebird_3_0." + Environment.NewLine + "Для установки БекОфис Б52 необходмио выбрать один из двух серверов, ненужный выключить. ";
                InstallB52.MessageErorInst(TextWindows);
                return;
            }

            SetWinSetHub = "SetTcpIpBack";
            if (SelectPuth != "")
            {
                BackPutchBD = SelectPuth = "";
                LabBackOfNet.Visibility = Visibility.Visible;
                SetPuthBdBackNet.Visibility = Visibility.Visible;
                SetBackOfPuth.Content = "С алиасом на диске компьютера:";
                SetBackOfPuth.Foreground = Brushes.Black;
                return;
            }
            // Открыть окно с дата грид для выбора текущего алиаса БД с которым будет работать Бек офис. 
            System.Windows.Application.Current.Dispatcher.Invoke(new Action(delegate ()
            {
                AdminPanels ConectoWorkSpace_InW = AppStart.LinkMainWindow("WAdminPanels");
                WinSetHub SetWindow = new WinSetHub();
                SetWindow.Owner = ConectoWorkSpace_InW; //this;
                SetWindow.Top = (ConectoWorkSpace_InW.Top + 7) + 300;
                SetWindow.Left = ConectoWorkSpace_InW.ScrollViewerd.Width - (SetWindow.Width * 1.7); //(ConectoWorkSpace_InW.Left) +  700
                SetWindow.Show();

            }));

        }
        // Процедура выбора БД под которую ставится бек по пути на любом компьютере
        private void SetPuthBdBackNet_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

            if (Convert.ToInt32(AppStart.TableReestr["BackFbd30OnOff"].ToString()) == 2 && Convert.ToInt32(AppStart.TableReestr["BackFbd25OnOff"].ToString()) == 2)
            {
                var TextWindows = "Одновременно установлены в системе Firebird_2_5 и Firebird_3_0." + Environment.NewLine + "Для установки БекОфис Б52 необходмио выбрать один из двух серверов, ненужный выключить. ";
                InstallB52.MessageErorInst(TextWindows);
                return;
            }
            Administrator.AdminPanels.SetWinSetHub = "SetNetTcpIpBd";
            if (BackPutchBD != "")
            {
                BackPutchBD = SelectPuth = "";
                SetBackOfPuth.Visibility = Visibility.Visible;
                SetPuthBdBack.Visibility = Visibility.Visible;
                LabBackOfNet.Content = "По пути на указанном компьютере:";
                LabBackOfNet.Foreground = Brushes.Black;
                return;
            }

            PathFileBD_Click();
            if ((!PathFileBDText.ToUpper().Contains(".FDB") && !PathFileBDText.ToUpper().Contains(".GDB")) || PathFileBDText.Length == 0)
            {
                var TextWindows = "Расширение файла выбранной БД не является БД FireBird" + Environment.NewLine + "Необходмио выбрать другой файл. ";
                InstallB52.MessageErorInst(TextWindows);
                return;
            }
            ContentPuth = PathFileBDText.Length > 38 ? "..." + PathFileBDText.Substring(PathFileBDText.Length - 35, 35) : PathFileBDText;
            LabBackOfNet.Content = ContentPuth;
            LabBackOfNet.Foreground = Brushes.Green;
            BackPutchBD = SelectPuth = PathFileBDText;
            SetBackOfPuth.Visibility = Visibility.Hidden;
            SetPuthBdBack.Visibility = Visibility.Hidden;

        }


        // Процедура выбора пути куда ставить бек
        private void SetPuthBack_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

            if (Convert.ToInt32(AppStart.TableReestr["BackFbd30OnOff"].ToString()) == 2 && Convert.ToInt32(AppStart.TableReestr["BackFbd25OnOff"].ToString()) == 2)
            {
                Administrator.AdminPanels.NameObj = "";
                var TextWindows = "Одновременно установлены в системе Firebird_2_5 и Firebird_3_0." + Environment.NewLine + "Для установки БекОфис Б52 необходмио выбрать один из двух серверов, ненужный выключить. ";
                InstallB52.MessageErorInst(TextWindows);
                return;
            }

            var dlg = new System.Windows.Forms.FolderBrowserDialog();
            dlg.Description = "Путь размещения Бека";
            System.Windows.Forms.DialogResult result = dlg.ShowDialog();
            if (result == System.Windows.Forms.DialogResult.OK)
            {
                FolderBackPuth = dlg.SelectedPath + (dlg.SelectedPath.LastIndexOf(@"\") <= 2 && dlg.SelectedPath.Length <= 3 ? "" : @"\");
                ContentPuth = FolderBackPuth.Length > 38 ? "..." + FolderBackPuth.Substring(FolderBackPuth.Length - 35, 35) : FolderBackPuth;
                SetBackOfPuthLoc.Content = "Бек: " + ContentPuth;
                SetBackOfPuthLoc.Foreground = Brushes.Green;

            }
            else
            {
                SetBackOfPuthLoc.Content = "Шаг 2. Установить путь размещения:";
                SetBackOfPuthLoc.Foreground = Brushes.Black;
                return;
            }
            InstallB52.BackFrontIsEnabledTrue(1);
        }

        // процедура  включения установки Бек Офиса Б52 на базе FieBird 2.5.
        private void BackFbd25OnOff_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {

            string StatusPic = "on_off_2.png";
            string SetKeyValue = "2";
            if (Convert.ToInt32(AppStart.TableReestr["BackFbd25OnOff"].ToString()) == 2)
            {
                if (Convert.ToInt32(AppStart.TableReestr["BackFbd30OnOff"].ToString()) == 2)
                {
                    StatusPic = "on_off_1.png";
                    SetKeyValue = "0";
                    Interface.CurrentStateInst("BackFbd25OnOff", SetKeyValue, StatusPic, BackFbd25OnOff);
                    BackOfAdresPortServer.Text = "3056";
                    Administrator.AdminPanels.UpdateKeyReestr("BackOfAdresPortServer", BackOfAdresPortServer.Text);

                }

            }
            else
            {
                if (Convert.ToInt32(AppStart.TableReestr["BackFbd30OnOff"].ToString()) == 2)
                {

                    Interface.CurrentStateInst("BackFbd25OnOff", SetKeyValue, StatusPic, BackFbd25OnOff);
                    BackOfAdresPortServer.Text = "3055";
                    Administrator.AdminPanels.UpdateKeyReestr("BackOfAdresPortServer", BackOfAdresPortServer.Text);
                    StatusPic = "on_off_1.png";
                    SetKeyValue = "0";
                    Interface.CurrentStateInst("BackFbd30OnOff", SetKeyValue, StatusPic, BackFbd30OnOff);

                }
            }

        }


        // процедура  включения установки Бек Офиса Б52 на базе FieBird 3.0.
        private void BackFbd30OnOff_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            string StatusPic = "on_off_2.png";
            string SetKeyValue = "2";

            if (Convert.ToInt32(AppStart.TableReestr["BackFbd30OnOff"].ToString()) == 2)
            {
                if (Convert.ToInt32(AppStart.TableReestr["BackFbd25OnOff"].ToString()) == 2)
                {
                    StatusPic = "on_off_1.png";
                    SetKeyValue = "0";
                    Interface.CurrentStateInst("BackFbd30OnOff", SetKeyValue, StatusPic, BackFbd30OnOff);
                    BackOfAdresPortServer.Text = "3055";
                    Administrator.AdminPanels.UpdateKeyReestr("BackOfAdresPortServer", BackOfAdresPortServer.Text);

                }
            }
            else
            {
                if (Convert.ToInt32(AppStart.TableReestr["BackFbd25OnOff"].ToString()) == 2)
                {

                    Interface.CurrentStateInst("BackFbd30OnOff", SetKeyValue, StatusPic, BackFbd30OnOff);
                    BackOfAdresPortServer.Text = "3056";
                    Administrator.AdminPanels.UpdateKeyReestr("BackOfAdresPortServer", BackOfAdresPortServer.Text);
                    StatusPic = "on_off_1.png";
                    SetKeyValue = "0";

                }
            }

        }

        // Процедура переназначения  алиса для установленых Бек офисов.
        private void ChangeAliasBack_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (Convert.ToInt32(AppStart.TableReestr["BackFbd30OnOff"].ToString()) == 2 && Convert.ToInt32(AppStart.TableReestr["BackFbd25OnOff"].ToString()) == 2)
            {
                NameObj = "";
                var TextWindows = "Одновременно установлены в системе Firebird_2_5 и Firebird_3_0." + Environment.NewLine + "Для установки БекОфис Б52 необходмио выбрать один из двух серверов, ненужный выключить. ";
                InstallB52.MessageErorInst(TextWindows);
                return;
            }
            SetWinSetHub = "ChangeAliasBack";
            // Открыть окно с дата грид для выбора текущего алиаса БД с которым будет работать Бек офис. 

            ConectoWorkSpace.Administrator.AdminPanels ConectoWorkSpace_InW = AppStart.LinkMainWindow("WAdminPanels");
            WinSetHub SetWindow = new WinSetHub();
            SetWindow.Owner = ConectoWorkSpace_InW;
            SetWindow.Top = ConectoWorkSpace_InW.Top + 370;
            SetWindow.Left = ConectoWorkSpace_InW.ScrollViewerd.Width - (SetWindow.Width * 2);
            SetWindow.Show();

        }

        // Процедуры загрузки и выбора из списка установленных Бек Офисов
        private void DataGridBack_Loaded(object sender, RoutedEventArgs e)
        {
            LoadedGridBack("SELECT * from REESTRBACK");
        }

        private void DataGridBack_MouseUp(object sender, MouseButtonEventArgs e)
        {

            DBConecto.DBopenFBConectionMemory(SystemBD.ConnnStringSystem().ToString(), "", "FbSystem");
            string InsertExecute = "SELECT count(*) from REESTRBACK";
            DBConecto.UniQuery CountQuery = new DBConecto.UniQuery(InsertExecute, "FB");
            string CountTable = CountQuery.ExecuteUNIScalar() == null ? "" : CountQuery.ExecuteUNIScalar().ToString();
            if (Convert.ToUInt32(CountTable) != 0)
            {
                if (DataGridBack.SelectedItem != null)
                {
                    GridBack path = DataGridBack.SelectedItem as GridBack;
                    FolderBack = path.Back;
                    ConectBack = path.Conect;
                    ServerBack = path.Server;
                    PuthBack = path.Puth;
                    FileKeyBack = path.Key;
                    ChangeAliasBack.IsEnabled = true;
                    DeleteBack.IsEnabled = true;

                    if (FileKeyBack != "") listBackKeyOnOff();
                    else
                    {
                        string PuthB52 = PuthBack.Substring(0, PuthBack.IndexOf(@"\B52") + 1);
                        if (File.Exists(PuthB52 + "gnclient.ini"))
                        {
                            string PortKey = "", Ip4Key = "";
                            Encoding code = Encoding.Default;
                            string[] fileLines = File.ReadAllLines(PuthB52 + "gnclient.ini", code);
                            foreach (string x in fileLines)
                            {
                                string[] data = x.Split('=');
                                if (data[0] == "TCP_PORT") PortKey = data[1];
                                if (data[0] == "IP_NAME") Ip4Key = data[1];
                            }

                            System.Windows.Application.Current.Dispatcher.Invoke(new Action(delegate ()
                            {
                                ConectoWorkSpace.Administrator.AdminPanels ConectoWorkSpace_Ip = AppStart.LinkMainWindow("WAdminPanels");
                                ConectoWorkSpace_Ip.BackPortServKey.Text = PortKey;
                                for (int indPoint = 1; indPoint <= 3; indPoint = indPoint + 1)
                                {
                                    int position = Ip4Key.IndexOf(".");
                                    if (position <= 0) { break; }
                                    switch (indPoint)
                                    {
                                        case 1:
                                            ConectoWorkSpace_Ip.BackOfKeyServIp4Text1.Text = Ip4Key.Substring(0, position);
                                            break;
                                        case 2:
                                            ConectoWorkSpace_Ip.BackOfKeyServIp4Text2.Text = Ip4Key.Substring(0, position);
                                            break;
                                        case 3:
                                            ConectoWorkSpace_Ip.BackOfKeyServIp4Text3.Text = Ip4Key.Substring(0, position);
                                            break;
                                    }
                                    Ip4Key = Ip4Key.Substring(position + 1);

                                }
                                ConectoWorkSpace_Ip.BackOfKeyServIp4Text4.Text = Ip4Key.Substring(0);

                            }));

                        }

                    }

                }
                else return;
            }
            DBConecto.DBcloseFBConectionMemory("FbSystem");

        }
        // Процедура отображения состояния ключей в gnckient.ini
        public static void listBackKeyOnOff()
        {

            string StrLockey = "SELECT * FROM " + FileKeyBack + " WHERE " + FileKeyBack + ".PUTH = " + "'" + PuthBack + "'";
            string GnclientPort = "", GnclientTcpIp = "";
            int IndCount = 0;
            DBConecto.DBopenFBConectionMemory(SystemBD.ConnnStringSystem().ToString(), "", "FbSystem");
            FbCommand UpdateKey = new FbCommand(StrLockey, DBConecto.bdFbSystemConect);
            FbDataReader reader = UpdateKey.ExecuteReader();
            while (reader.Read())
            {
                GnclientPort = reader[4].ToString();
                GnclientTcpIp = reader[5].ToString();
                IndCount++;
            }
            reader.Close();
            UpdateKey.Dispose();
            DBConecto.DBcloseFBConectionMemory("FbSystem");

            if (IndCount != 0)
            {

                System.Windows.Application.Current.Dispatcher.Invoke(new Action(delegate ()
                {
                    ConectoWorkSpace.Administrator.AdminPanels ConectoWorkSpace_On = AppStart.LinkMainWindow("WAdminPanels");
                    if (FileKeyBack == "LOCKEY") Interface.CurrentStateInst("BackKeyLocOnOff", "2", "on_off_2.png", ConectoWorkSpace_On.BackKeyLocOnOff);
                    if (FileKeyBack == "NETKEY") Interface.CurrentStateInst("BackKeyNetOnOff", "2", "on_off_2.png", ConectoWorkSpace_On.BackKeyNetOnOff);
                    if (FileKeyBack == "TESTKEY") Interface.CurrentStateInst("BackKeyTestOnOff", "2", "on_off_2.png", ConectoWorkSpace_On.BackKeyTestOnOff);
                    ConectoWorkSpace_On.BackPortServKey.Text = GnclientPort;
                    for (int indPoint = 1; indPoint <= 3; indPoint = indPoint + 1)
                    {
                        int position = GnclientTcpIp.IndexOf(".");
                        if (position <= 0) { break; }
                        switch (indPoint)
                        {
                            case 1:
                                ConectoWorkSpace_On.BackOfKeyServIp4Text1.Text = GnclientTcpIp.Substring(0, position);
                                break;
                            case 2:
                                ConectoWorkSpace_On.BackOfKeyServIp4Text2.Text = GnclientTcpIp.Substring(0, position);
                                break;
                            case 3:
                                ConectoWorkSpace_On.BackOfKeyServIp4Text3.Text = GnclientTcpIp.Substring(0, position);
                                break;
                        }
                        GnclientTcpIp = GnclientTcpIp.Substring(position + 1);
                    }
                    ConectoWorkSpace_On.BackOfKeyServIp4Text4.Text = GnclientTcpIp.Substring(0);
                    if (GnclientTcpIp.Length != 0) Interface.CurrentStateInst("BackOfKeyServ_IP4", "2", "on_off_2.png", ConectoWorkSpace_On.BackOfKeyServ_IP4);
                }));

            }
        }


        public static void LoadedGridBack(string CurrentSetBD)
        {

            List<GridBack> result = new List<GridBack>(1);
            DBConecto.DBopenFBConectionMemory(SystemBD.ConnnStringSystem().ToString(), "", "FbSystem");
            FbCommand UpdateKey = new FbCommand(CurrentSetBD, DBConecto.bdFbSystemConect);
            UpdateKey.CommandTimeout = 3;
            UpdateKey.CommandType = CommandType.Text;
            FbDataReader reader = UpdateKey.ExecuteReader();
            while (reader.Read())
            {
                result.Add(new GridBack(reader[0].ToString(), reader[1].ToString(), reader[2].ToString(), reader[3].ToString(), reader[4].ToString()));
            }
            reader.Close();
            UpdateKey.Dispose();
            DBConecto.DBcloseFBConectionMemory("FbSystem");
            System.Windows.Application.Current.Dispatcher.Invoke(new Action(delegate ()
            {
                AdminPanels ConectoWorkSpace_InW = AppStart.LinkMainWindow("WAdminPanels");
                ConectoWorkSpace_InW.DataGridBack.ItemsSource = result;
                //ConectoWorkSpace_InW.DataGridFront.ItemsSource = result;
            }));

        }



        // Процедура переназначения  алиса для установленых Бек офисов.
        private void ChangeAliasFront_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (Convert.ToInt32(AppStart.TableReestr["FrontFbd30OnOff"].ToString()) == 2 && Convert.ToInt32(AppStart.TableReestr["FrontFbd25OnOff"].ToString()) == 2)
            {
                NameObj = "";
                var TextWindows = "Одновременно установлены в системе Firebird_2_5 и Firebird_3_0." + Environment.NewLine + "Необходмио выбрать один из двух серверов, ненужный выключить.";
                InstallB52.MessageErorInst(TextWindows);
                return;
            }
            SetWinSetHub = "ChangeAliasFront";
            // Открыть окно с дата грид для выбора текущего алиаса БД с которым будет работать Бек офис. 
            ConectoWorkSpace.Administrator.AdminPanels ConectoWorkSpace_InW = AppStart.LinkMainWindow("WAdminPanels");
            WinSetHub SetWindow = new WinSetHub();
            SetWindow.Owner = ConectoWorkSpace_InW;
            SetWindow.Top = ConectoWorkSpace_InW.Top + 370;
            SetWindow.Left = ConectoWorkSpace_InW.ScrollViewerd.Width - (SetWindow.Width * 2); //ConectoWorkSpace_InW.Left + 1000;

            //SetWindow.Top = (ConectoWorkSpace_InW.Top + 7) + ConectoWorkSpace_InW.Close_F.Margin.Top + (ConectoWorkSpace_InW.Close_F.Height - 2) + 370;
            //SetWindow.Left = (ConectoWorkSpace_InW.Left) + ConectoWorkSpace_InW.Close_F.Margin.Left - (SetWindow.Width - 22) + 1120;
            SetWindow.Show();

        }

        // Процедуры загрузки и выбора из списка установленных Бек Офисов
        private void DataGridFront_Loaded(object sender, RoutedEventArgs e)
        {
            LoadedGridFront("SELECT * from REESTRFRONT");
        }

        private void DataGridFront_MouseUp(object sender, MouseButtonEventArgs e)
        {

            DBConecto.DBopenFBConectionMemory(SystemBD.ConnnStringSystem().ToString(), "", "FbSystem");
            string InsertExecute = "SELECT count(*) from REESTRFRONT";
            DBConecto.UniQuery CountQuery = new DBConecto.UniQuery(InsertExecute, "FB");
            string CountTable = CountQuery.ExecuteUNIScalar() == null ? "" : CountQuery.ExecuteUNIScalar().ToString();
            if (Convert.ToUInt32(CountTable) != 0)
            {
                if (DataGridFront.SelectedItem != null)
                {

                    GridFront ReestrFront = DataGridFront.SelectedItem as GridFront;
                    FolderBack = ReestrFront.Front;
                    ConectBack = ReestrFront.Conect;
                    ServerBack = ReestrFront.Server;
                    PuthBack = ReestrFront.Puth;
                    FileKeyBack = ReestrFront.Key;
                    ChageAliasFront25.IsEnabled = true;

                    if (FileKeyBack != "") listFrontKeyOnOff();
                    else
                    {
                        string PuthB52 = PuthBack.Substring(0, PuthBack.IndexOf(@"\B52") + 1);
                        if (File.Exists(PuthB52 + "gnclient.ini"))
                        {
                            string PortKey = "", Ip4Key = "";
                            Encoding code = Encoding.Default;
                            string[] fileLines = File.ReadAllLines(PuthB52 + "gnclient.ini", code);
                            foreach (string x in fileLines)
                            {
                                string[] data = x.Split('=');
                                if (data[0] == "TCP_PORT") PortKey = data[1];
                                if (data[0] == "IP_NAME") Ip4Key = data[1];
                            }

                            System.Windows.Application.Current.Dispatcher.Invoke(new Action(delegate ()
                            {
                                ConectoWorkSpace.Administrator.AdminPanels ConectoWorkSpace_Ip = AppStart.LinkMainWindow("WAdminPanels");
                                ConectoWorkSpace_Ip.FrontAdresPorta.Text = PortKey;
                                for (int indPoint = 1; indPoint <= 3; indPoint = indPoint + 1)
                                {
                                    int position = Ip4Key.IndexOf(".");
                                    if (position <= 0) { break; }
                                    switch (indPoint)
                                    {
                                        case 1:
                                            ConectoWorkSpace_Ip.FrontOfAdrDataIp4Text1.Text = Ip4Key.Substring(0, position);
                                            break;
                                        case 2:
                                            ConectoWorkSpace_Ip.FrontOfAdrDataIp4Text2.Text = Ip4Key.Substring(0, position);
                                            break;
                                        case 3:
                                            ConectoWorkSpace_Ip.FrontOfAdrDataIp4Text3.Text = Ip4Key.Substring(0, position);
                                            break;
                                    }
                                    Ip4Key = Ip4Key.Substring(position + 1);

                                }
                                ConectoWorkSpace_Ip.FrontOfAdrDataIp4Text4.Text = Ip4Key.Substring(0);

                            }));
                        }
                    }

                }
                else return;
            }
            DBConecto.DBcloseFBConectionMemory("FbSystem");

        }

        // Процедура отображения состояния ключей в gnckient.ini
        public static void listFrontKeyOnOff()
        {

            string StrLockey = "SELECT * FROM " + FileKeyBack + " WHERE " + FileKeyBack + ".PUTH = " + "'" + PuthBack + "'";
            string GnclientPort = "", GnclientTcpIp = "";
            int IndCount = 0;
            DBConecto.DBopenFBConectionMemory(SystemBD.ConnnStringSystem().ToString(), "", "FbSystem");
            FbCommand UpdateKey = new FbCommand(StrLockey, DBConecto.bdFbSystemConect);
            FbDataReader reader = UpdateKey.ExecuteReader();
            while (reader.Read())
            {
                GnclientPort = reader[4].ToString();
                GnclientTcpIp = reader[5].ToString();
                IndCount++;
            }
            reader.Close();
            UpdateKey.Dispose();
            DBConecto.DBcloseFBConectionMemory("FbSystem");

            if (IndCount != 0)
            {

                System.Windows.Application.Current.Dispatcher.Invoke(new Action(delegate ()
                {
                    ConectoWorkSpace.Administrator.AdminPanels ConectoWorkSpace_On = AppStart.LinkMainWindow("WAdminPanels");
                    if (FileKeyBack == "LOCKEY") Interface.CurrentStateInst("KeyLocFront", "2", "on_off_2.png", ConectoWorkSpace_On.KeyLocFront);
                    if (FileKeyBack == "NETKEY") Interface.CurrentStateInst("FrontKeyNetOnOff", "2", "on_off_2.png", ConectoWorkSpace_On.FrontKeyNetOnOff);
                    if (FileKeyBack == "TESTKEY") Interface.CurrentStateInst("FrontKeyTestOnOff", "2", "on_off_2.png", ConectoWorkSpace_On.FrontKeyTestOnOff);
                    ConectoWorkSpace_On.FrontAdresPorta.Text = GnclientPort;
                    for (int indPoint = 1; indPoint <= 3; indPoint = indPoint + 1)
                    {
                        int position = GnclientTcpIp.IndexOf(".");
                        if (position <= 0) { break; }
                        switch (indPoint)
                        {
                            case 1:
                                ConectoWorkSpace_On.FrontOfAdrDataIp4Text1.Text = GnclientTcpIp.Substring(0, position);
                                break;
                            case 2:
                                ConectoWorkSpace_On.FrontOfAdrDataIp4Text2.Text = GnclientTcpIp.Substring(0, position);
                                break;
                            case 3:
                                ConectoWorkSpace_On.FrontOfAdrDataIp4Text3.Text = GnclientTcpIp.Substring(0, position);
                                break;
                        }
                        GnclientTcpIp = GnclientTcpIp.Substring(position + 1);
                    }
                    ConectoWorkSpace_On.FrontOfAdrDataIp4Text4.Text = GnclientTcpIp.Substring(0);
                    if (GnclientTcpIp.Length != 0) Interface.CurrentStateInst("FrontOfAdrOnOff_IP4", "2", "on_off_2.png", ConectoWorkSpace_On.FrontOfAdrOnOff_IP4);
                }));

            }
        }


        public static void LoadedGridFront(string CurrentSetBD)
        {

            List<GridFront> result = new List<GridFront>(1);
            DBConecto.DBopenFBConectionMemory(SystemBD.ConnnStringSystem().ToString(), "", "FbSystem");
            FbCommand UpdateKey = new FbCommand(CurrentSetBD, DBConecto.bdFbSystemConect);
            UpdateKey.CommandTimeout = 3;
            UpdateKey.CommandType = CommandType.Text;
            FbDataReader reader = UpdateKey.ExecuteReader();
            while (reader.Read())
            {
                result.Add(new GridFront(reader[0].ToString(), reader[1].ToString(), reader[2].ToString(), reader[3].ToString(), reader[4].ToString()));
            }
            reader.Close();
            UpdateKey.Dispose();
            DBConecto.DBcloseFBConectionMemory("FbSystem");
            System.Windows.Application.Current.Dispatcher.Invoke(new Action(delegate ()
            {
                AdminPanels ConectoWorkSpace_InW = AppStart.LinkMainWindow("WAdminPanels");
                ConectoWorkSpace_InW.DataGridFront.ItemsSource = result;
            }));

        }

        // 4 процедуры установки Бек Офиса Б52 розница, кафе бар, фаст фуд, доставка еды 

        private void BackOfRoznOnOff_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            PuthBackOff = "";
            NameObj = ImageObj = "BackOfRoznOnOff";
            Interface.ObjektOnOff("BackOfRoznOnOff", ref BackOfRoznOnOff, new Install.Metods { NameClass = "ConectoWorkSpace.InstallB52", Install = "InstallBackB52", UnInstal = "UnInstallBackB52" });
        }

        private void AddBackRozn_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            NameObj = ImageObj = "BackOfRoznOnOff";
            InstallB52.InstallBackB52();
        }

        private void BackOfRestoranOnOff_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            PuthBackOff = "";
            NameObj = ImageObj = "BackOfRestoranOnOff";
            Interface.ObjektOnOff("BackOfRestoranOnOff", ref BackOfRestoranOnOff, new Install.Metods { NameClass = "ConectoWorkSpace.InstallB52", Install = "InstallBackB52", UnInstal = "UnInstallBackB52" });
        }

        private void AddBackRestoran_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            NameObj = ImageObj = "BackOfRestoranOnOff";
            InstallB52.InstallBackB52();
        }

        private void BackOfFastFudOnOff_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            PuthBackOff = "";
            NameObj = ImageObj = "BackOfFastFudOnOff";
            Interface.ObjektOnOff("BackOfFastFudOnOff", ref BackOfFastFudOnOff, new Install.Metods { NameClass = "ConectoWorkSpace.InstallB52", Install = "InstallBackB52", UnInstal = "UnInstallBackB52" });
        }
        private void AddBackFast_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            NameObj = ImageObj = "BackOfFastFudOnOff";
            InstallB52.InstallBackB52();
        }

        // Процедура установки фитнеса
        private void BackOfFitnesOnOff_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            PuthBackOff = "";
            NameObj = ImageObj = "BackOfFitnesOnOff";
            Interface.ObjektOnOff("BackOfFitnesOnOff", ref BackOfFitnesOnOff, new Install.Metods { NameClass = "ConectoWorkSpace.InstallB52", Install = "InstallBackB52", UnInstal = "UnInstallBackB52" });
        }

        private void AddBackFithes_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            NameObj = ImageObj = "BackOfFitnesOnOff";
            InstallB52.InstallBackB52();
        }

        private void BackOfHotelOnOff_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            PuthBackOff = "";
            NameObj = ImageObj = "BackOfHotelOnOff";
            Interface.ObjektOnOff("BackOfHotelOnOff", ref BackOfHotelOnOff, new Install.Metods { NameClass = "ConectoWorkSpace.InstallB52", Install = "InstallBackB52", UnInstal = "UnInstallBackB52" });
        }

        private void AddBackHotel_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            NameObj = ImageObj = "BackOfHotelOnOff";
            InstallB52.InstallBackB52();
        }

        private void BackOfMixOnOff_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            PuthBackOff = "";
            NameObj = ImageObj = "BackOfMixOnOff";
            Interface.ObjektOnOff("BackOfMixOnOff", ref BackOfMixOnOff, new Install.Metods { NameClass = "ConectoWorkSpace.InstallB52", Install = "InstallBackB52", UnInstal = "UnInstallBackB52" });
        }

        private void AddBackMix_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            NameObj = ImageObj = "BackOfMixOnOff";
            InstallB52.InstallBackB52();
        }

        // выполнить запуск на выполнение БекОфисБ52 
        private void BackRunRoznica_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            NameObj = ImageObj = "BackOfRoznOnOff";
            InstallB52.RunBackOfficeB52();
        }

        private void BackRunRestoran_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            NameObj = ImageObj = "BackOfRestoranOnOff";
            InstallB52.RunBackOfficeB52();
        }

        private void BackRunFast_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            NameObj = ImageObj = "BackOfFastFudOnOff";
            InstallB52.RunBackOfficeB52();
        }

        private void BackRunFitnes_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            NameObj = ImageObj = "BackOfFitnesOnOff";
            InstallB52.RunBackOfficeB52();
        }

        private void BackRunHotel_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            NameObj = ImageObj = "BackOfHotelOnOff";
            InstallB52.RunBackOfficeB52();
        }

        private void BackRunMix_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            NameObj = ImageObj = "BackOfMixOnOff";
            InstallB52.RunBackOfficeB52();
        }

        // Включить Автопределение сервера данных. Бек офис Б52
        private void BackOfAvtoServOnOff_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            Interface.ObjektOnOff("BackOfAvtoServOnOff", ref BackOfAvtoServOnOff, new Install.Metods { NameClass = "None", Install = "None", UnInstal = "None" });
        }

   

        // Включить выключить ввод  адреса IP4 серевера данных Бек офис Б52
        private void BackOfAdrServerDate_IP4_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            Interface.MouseLeftButtonUp("BackOfAdrServerDate_IP4", "BackOfAdrServerDate_IP6", ref BackOfAdrServerDate_IP4, ref BackOfAdrServerDate_IP6);
            Interface.TextBoxTrueFalse("BackOfAdrServerDate_IP4", "WAdminPanels", BackOfIp6Text1, 6, BackOfIp4Text1);

        }
        // Включить выключить ввод  адреса IP6 серевера данных Бек офис Б52
        private void BackOfAdrServerDate_IP6_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            Interface.MouseLeftButtonUp("BackOfAdrServerDate_IP6", "BackOfAdrServerDate_IP4", ref BackOfAdrServerDate_IP6, ref BackOfAdrServerDate_IP4);
            Interface.TextBoxTrueFalse("BackOfAdrServerDate_IP6", "WAdminPanels", BackOfIp4Text1, 6, BackOfIp6Text1);

        }

        // Блок 5-ти процедур ввода и схранения значения полей  IP4 адреса закладки Бек Офис
        private void BackOfIp4Text1_KeyUp(object sender, KeyEventArgs e)
        {
            Interface.SaveTextBox("BackOfIp4Text1", BackOfIp4Text1, 1);
        }
        private void BackOfIp4Text2_KeyUp(object sender, KeyEventArgs e)
        {
            Interface.SaveTextBox("BackOfIp4Text2", BackOfIp4Text2, 1);
        }
        private void BackOfIp4Text3_KeyUp(object sender, KeyEventArgs e)
        {
            Interface.SaveTextBox("BackOfIp4Text3", BackOfIp4Text3, 1);
        }
        private void BackOfIp4Text4_KeyUp(object sender, KeyEventArgs e)
        {
            Interface.SaveTextBox("BackOfIp4Text4", BackOfIp4Text4, 1);
        }
        private void BackOfIp4Text1_LostFocus(object sender, RoutedEventArgs e)
        {
            AdrIp4[4] = BackOfIp4Text1.Text + "." + BackOfIp4Text2.Text + "." + BackOfIp4Text3.Text + "." + BackOfIp4Text4.Text;
            AdrIp4[4] = AdrIp4[4] == "..." ? "" : AdrIp4[4];
            Administrator.AdminPanels.UpdateKeyReestr("SetTextIp4BackOf", AdrIp4[4]);

        }

        // Блок 7-ти процедур ввода и схранения значения полей  IP6 адреса закладки Бек Офис
        private void BackOfIp6Text1_KeyUp(object sender, KeyEventArgs e)
        {
            Interface.SaveTextBox("BackOfIp6Text1", BackOfIp6Text1, 1);
        }
        private void BackOfIp6Text2_KeyUp(object sender, KeyEventArgs e)
        {
            Interface.SaveTextBox("BackOfIp6Text2", BackOfIp6Text2, 1);
        }
        private void BackOfIp6Text3_KeyUp(object sender, KeyEventArgs e)
        {
            Interface.SaveTextBox("BackOfIp6Text3", BackOfIp6Text3, 1);
        }
        private void BackOfIp6Text4_KeyUp(object sender, KeyEventArgs e)
        {
            Interface.SaveTextBox("BackOfIp6Text4", BackOfIp6Text4, 1);
        }
        private void BackOfIp6Text5_KeyUp(object sender, KeyEventArgs e)
        {
            Interface.SaveTextBox("BackOfIp6Text5", BackOfIp6Text5, 1);
        }
        private void BackOfIp6Text6_KeyUp(object sender, KeyEventArgs e)
        {
            Interface.SaveTextBox("BackOfIp6Text6", BackOfIp6Text6, 1);
        }
        private void BackOfIp6Text1_LostFocus(object sender, RoutedEventArgs e)
        {
            AdrIp6[4] = BackOfIp6Text1.Text + "." + BackOfIp6Text2.Text + "." + BackOfIp6Text3.Text + "." + BackOfIp6Text4.Text + "." + BackOfIp6Text5.Text + "." + BackOfIp6Text6.Text;
            Administrator.AdminPanels.UpdateKeyReestr("SetTextIp6BackOf", AdrIp6[4]);

        }

        // Включить выключить РДП терминал
        private void BackOfRdpOnOff_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            Interface.ObjektOnOff("BackOfRdpOnOff", ref BackOfRdpOnOff, new Install.Metods { NameClass = "None", Install = "None", UnInstal = "None" });
        }
        //// Включить выключить автопределение ключа Бек офис Б52
        //private void BackOfAvtoKeyOnOff_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        //{
        //    Interface.ObjektOnOff("BackOfAvtoKeyOnOff", ref BackOfAvtoKeyOnOff, new Install.Metods { NameClass = "None", Install = "None", UnInstal = "None" });
        //}

        // Включить выключить ввод  адреса IP4 данных серевера  Бек офис Б52
        private void BackOfAdrOnOff_IP4_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {

            BackOfAdresPorta.IsEnabled = BackOfAdresPorta.IsEnabled == true ? false : true;
            Interface.MouseLeftButtonUp("BackOfAdrOnOff_IP4", "BackOfAdrOnOff_IP6", ref BackOfAdrOnOff_IP4, ref BackOfAdrOnOff_IP6);
            Interface.TextBoxTrueFalse("BackOfAdrOnOff_IP4", "WAdminPanels", BackOfAdrDataIp6Text1, 6, BackOfAdrDataIp4Text1);

        }



        private void SetOff_IP4_LostFocus(object sender, RoutedEventArgs e)
        {
            //BackOfAdrDataIp4Text1.IsEnabled = true;
            //BackOfAdrDataIp4Text2.IsEnabled = true;
            //BackOfAdrDataIp4Text3.IsEnabled = true;
            //BackOfAdrDataIp4Text4.IsEnabled = true;
            //BackOfAdresPorta.IsEnabled = false;
            //Interface.CurrentStateInst("BackOfAdrOnOff_IP4", "0", "on_off_1.png", BackOfAdrOnOff_IP4);
        }


        // Включить выключить ввод  адреса IP6 данных серевера  Бек офис Б52
        private void BackOfAdrOnOff_IP6_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            Interface.MouseLeftButtonUp("BackOfAdrOnOff_IP6", "BackOfAdrOnOff_IP4", ref BackOfAdrOnOff_IP6, ref BackOfAdrOnOff_IP4);
            Interface.TextBoxTrueFalse("BackOfAdrOnOff_IP6", "WAdminPanels", BackOfAdrDataIp4Text1, 6, BackOfAdrDataIp6Text1);
        }

        // Блок 5-ти процедур ввода и схранения значения полей  IP4 адреса данных серевера закладки Бек Офис
        private void BackOfAdrDataIp4Text1_KeyUp(object sender, KeyEventArgs e)
        {
            Interface.SaveTextBox("BackOfAdrDataIp4Text1", BackOfAdrDataIp4Text1, 1);
        }
        private void BackOfAdrDataIp4Text2_KeyUp(object sender, KeyEventArgs e)
        {
            Interface.SaveTextBox("BackOfAdrDataIp4Text2", BackOfAdrDataIp4Text2, 1);
        }
        private void BackOfAdrDataIp4Text3_KeyUp(object sender, KeyEventArgs e)
        {
            Interface.SaveTextBox("BackOfAdrDataIp4Text3", BackOfAdrDataIp4Text3, 1);
        }
        private void BackOfAdrDataIp4Text4_KeyUp(object sender, KeyEventArgs e)
        {
            Interface.SaveTextBox("BackOfAdrDataIp4Text4", BackOfAdrDataIp4Text4, 1);
        }
        private void BackOfAdrDataIp4Text1_LostFocus(object sender, RoutedEventArgs e)
        {
            AdrIp4[5] = BackOfAdrDataIp4Text1.Text + "." + BackOfAdrDataIp4Text2.Text + "." + BackOfAdrDataIp4Text3.Text + "." + BackOfAdrDataIp4Text4.Text;
            Administrator.AdminPanels.UpdateKeyReestr("SetTextAdrDataIp4BackOf", AdrIp4[5]);

        }
        private void BackOfAdrDataIp4Text4_LostFocus(object sender, RoutedEventArgs e)
        {
            AdrIp4[5] = BackOfAdrDataIp4Text1.Text + "." + BackOfAdrDataIp4Text2.Text + "." + BackOfAdrDataIp4Text3.Text + "." + BackOfAdrDataIp4Text4.Text;
            Administrator.AdminPanels.UpdateKeyReestr("SetTextAdrDataIp4BackOf", AdrIp4[5]);
            BackOfAdrDataIp4Text1.IsEnabled = false;
            BackOfAdrDataIp4Text2.IsEnabled = false;
            BackOfAdrDataIp4Text3.IsEnabled = false;
            BackOfAdrDataIp4Text4.IsEnabled = false;
            BackOfAdresPorta.IsEnabled = false;
            Interface.CurrentStateInst("BackOfAdrOnOff_IP4", "0", "on_off_1.png", BackOfAdrOnOff_IP4);
            string StrCreate = "";
            if (Administrator.AdminPanels.SetWinSetHub == "NETKEY")
            {
                StrCreate = "UPDATE NETKEY SET PORT = " + "'" + BackOfAdresPorta.Text + "'" + ", TCPIP = " + "'" + AdrIp4[5] + "'" + "  WHERE NETKEY.PUTH = " + "'" + NetKeyPuth + "'";
            }

            if (Administrator.AdminPanels.SetWinSetHub == "LOCKEY")
            {
                StrCreate = "UPDATE LOCKEY SET PORT = " + "'" + BackOfAdresPorta.Text + "'" + ", TCPIP = " + "'" + AdrIp4[5] + "'" + "  WHERE LOCKEY.PUTH = " + "'" + LocKeyPuth + "'";
            }
            if (Administrator.AdminPanels.SetWinSetHub == "TESTKEY")
            {
                StrCreate = "UPDATE TESTKEY SET PORT = " + "'" + BackOfAdresPorta.Text + "'" + ", TCPIP = " + "'" + AdrIp4[5] + "'" + "  WHERE TESTKEY.PUTH = " + "'" + TestKeyPuth + "'";
            }
            DBConecto.DBopenFBConectionMemory(SystemBD.ConnnStringSystem().ToString(), "", "FbSystem");
            DBConecto.UniQuery UpdateQuery = new DBConecto.UniQuery(StrCreate, "FB");
            UpdateQuery.ExecuteUNIScalar();
            DBConecto.DBcloseFBConectionMemory("FbSystem");
            Grid_key("SELECT * from " + Administrator.AdminPanels.SetWinSetHub);
            ChangeGnclient(PuthGnclient, BackOfAdresPorta.Text, AdrIp4[5]);

        }
        // Блок 7-ти процедур ввода и схранения значения полей  IP6 адреса закладки Бек Офис
        private void BackOfAdrDataIp6Text1_KeyUp(object sender, KeyEventArgs e)
        {
            Interface.SaveTextBox("BackOfAdrDataIp6Text1", BackOfAdrDataIp6Text1, 1);
        }
        private void BackOfAdrDataIp6Text2_KeyUp(object sender, KeyEventArgs e)
        {
            Interface.SaveTextBox("BackOfAdrDataIp6Text2", BackOfAdrDataIp6Text2, 1);
        }
        private void BackOfAdrDataIp6Text3_KeyUp(object sender, KeyEventArgs e)
        {
            Interface.SaveTextBox("BackOfAdrDataIp6Text3", BackOfAdrDataIp6Text3, 1);
        }
        private void BackOfAdrDataIp6Text4_KeyUp(object sender, KeyEventArgs e)
        {
            Interface.SaveTextBox("BackOfAdrDataIp6Text4", BackOfAdrDataIp6Text4, 1);
        }
        private void BackOfAdrDataIp6Text5_KeyUp(object sender, KeyEventArgs e)
        {
            Interface.SaveTextBox("BackOfAdrDataIp6Text5", BackOfAdrDataIp6Text5, 1);
        }
        private void BackOfAdrDataIp6Text6_KeyUp(object sender, KeyEventArgs e)
        {
            Interface.SaveTextBox("BackOfAdrDataIp6Text6", BackOfAdrDataIp6Text6, 1);
        }
        private void BackOfAdrDataIp6Text1_LostFocus(object sender, RoutedEventArgs e)
        {
            AdrIp6[5] = BackOfAdrDataIp6Text1.Text + "." + BackOfAdrDataIp6Text2.Text + "." + BackOfAdrDataIp6Text3.Text + "." + BackOfAdrDataIp6Text4.Text + "." + BackOfAdrDataIp6Text5.Text + "." + BackOfAdrDataIp6Text6.Text;
            Administrator.AdminPanels.UpdateKeyReestr("SetTextAdrDataIp6BackOf", AdrIp6[5]);

        }


        private void BackOfAdresPortServer_KeyUp(object sender, KeyEventArgs e)
        {
            NameObj = "BackFbd25OnOff";
            Interface.SaveTextBox("BackOfAdresPortServer", BackOfAdresPortServer, 1);
            if (Convert.ToInt32(AppStart.TableReestr["BackFbd25OnOff"].ToString()) == 2)
            {
                AppStart.TableReestr["SetPortServer25"] = BackOfAdresPortServer.Text;
            }
            if (Convert.ToInt32(AppStart.TableReestr["BackFbd30OnOff"].ToString()) == 2)
            {
                AppStart.TableReestr["SetPortServer30"] = BackOfAdresPortServer.Text;
            }
        }

        private void BackOfAdresPorta_KeyUp(object sender, KeyEventArgs e)
        {
            Interface.SaveTextBox("BackOfAdresPorta", BackOfAdresPorta, 1);
        }

        // Запустить редактор приложения Фронт Б52
        private void EditConfApp_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            SetWinSetHub = "AptuneFront";
            string PuthAptune = PuthBack.Substring(0, PuthBack.LastIndexOf(@"\") + 1);
            NotepadIni(PuthAptune, "aptune.ini");


        }
        public static void AptuneFront()
        {
            NotepadIni(PuthBack, "aptune.ini");
        }

        public static void AppFront()
        {
            System.Windows.Application.Current.Dispatcher.Invoke(new Action(delegate ()
            {
                ConectoWorkSpace.Administrator.AdminPanels ConectoWorkSpace_InW = AppStart.LinkMainWindow("WAdminPanels");
                ConectoWorkSpace_InW.PuthFile.Text = TestKeyPuth;
                ConectoWorkSpace_InW.PuthFile_OtherApp.Text = "";
            }));
        }
        // Открыть редактор ключа Фронт Б52
        private void EditConfKey_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

            SetWinSetHub = "GnclientFront";
            string PuthGnclient = PuthBack.Substring(0, PuthBack.LastIndexOf(@"\") + 1);
            NotepadIni(PuthGnclient, "gnclient.ini");

        }

        public static void GnclientFront()
        {
            NotepadIni(PuthBack, "gnclient.ini");
        }

        // Запустить редактор приложения Бек Офиса Б52
        private void BackOfEditAptuneKey_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            string PuthAptune = PuthBack.Substring(0, PuthBack.LastIndexOf(@"\") + 1);
            SetWinSetHub = "AptuneBack";
            NotepadIni(PuthAptune, "aptune.ini");
        }

        public static void AptuneBack()
        {
            NotepadIni(PuthBack, "aptune.ini");
        }
        // Открыть редактор ключа Бек Офиса Б52
        private void BackEditKeyGnclient_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

            string PuthGnclient = PuthBack.Substring(0, PuthBack.LastIndexOf(@"\") + 1);
            SetWinSetHub = "GnclientBack";
            NotepadIni(PuthGnclient, "gnclient.ini");

        }

        public static void GnclientBack()
        {
            NotepadIni(PuthBack, "gnclient.ini");
        }

        // Открыть редактор ключа Бек Офиса Б52
        private void GnclientServer_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            NotepadIni(PuthGnclient.Substring(0, PuthGnclient.LastIndexOf(@"\") + 1), "gnclient.ini");
            GnclientServer.IsEnabled = false;
        }

        // Открыть редактор ключа Бек Офиса Б52
        private void EditKeyPuthGrdsrv_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            NotepadIni(PuthGrdsrv, "grdsrv.ini");
        }

        public static void NotepadIni(string PuthFile, string NameFile)
        {
            //if (!AppStart.SetFocusWindow("notepad"))
            // {
            if (PuthFile != "")
            {
                PuthAptune = PuthFile + NameFile;
                if (SystemConecto.File_(PuthAptune, 5))
                {
                    Process mv_prcInstaller = new Process();
                    mv_prcInstaller.StartInfo.FileName = "notepad";
                    mv_prcInstaller.StartInfo.Arguments = PuthAptune;
                    mv_prcInstaller.Start();
                    mv_prcInstaller.WaitForExit();
                    mv_prcInstaller.Close();
                }
            }
            // }
        }

        #endregion



        public void SelectSpr_Loaded(object sender, RoutedEventArgs e)
        {

            Label Filtr = new Label();
            Filtr.Width = 350;
            Filtr.Height = 30;
            Filtr.Name = "SetFiltr";
            Filtr.HorizontalAlignment = HorizontalAlignment.Left;
            Filtr.VerticalAlignment = VerticalAlignment.Top;
            Filtr.HorizontalContentAlignment = HorizontalAlignment.Left;
            Filtr.Margin = new Thickness(50, 5, 0, 0);
            Filtr.Content = "Установить фильтр:";
            Filtr.FontFamily = new FontFamily("Calibri"); ;
            Filtr.FontSize = 18;
            Filtr.FontWeight = FontWeights.Bold;
            Filtr.Foreground = Brushes.Black;
            Filtr.Background = Brushes.AntiqueWhite;
            Filtr.IsEnabled = true;
            //foo.MouseLeftButtonUp += new MouseButtonEventHandler(ConectoWorkSpace.Administrator.AdminPanels.LocDisk);
            //foo.FontStyle = FontStyles.Italic;
            Filtr.Visibility = Visibility.Visible;
            AddComboSpr(Filtr);



            // Создание кнопки Комбобокса задающей интервал создания архива
            string[] str = new string[] { "Все организации", "Наша организация", "Метод B52 " };
            ComboBox SetComboSpr = new ComboBox();
            SetComboSpr.Width = 150;
            SetComboSpr.Height = 30;
            SetComboSpr.Name = "SprComb";
            SetComboSpr.HorizontalAlignment = HorizontalAlignment.Left;
            SetComboSpr.VerticalAlignment = VerticalAlignment.Top;
            SetComboSpr.HorizontalContentAlignment = HorizontalAlignment.Center;
            SetComboSpr.Margin = new Thickness(400, 5, 0, 0);
            SetComboSpr.FontFamily = new FontFamily("Calibri"); //Courier New
            SetComboSpr.FontSize = 15;
            SetComboSpr.FontWeight = FontWeights.Normal;
            SetComboSpr.Foreground = Brushes.Black;
            SetComboSpr.Background = Brushes.SkyBlue;
            SetComboSpr.IsEnabled = true;
            SetComboSpr.SelectionChanged += new SelectionChangedEventHandler(OnMyComboBoxChanged);
            SetComboSpr.ItemsSource = str;
            SetComboSpr.SelectedIndex = 0;
            SetComboSpr.Visibility = Visibility.Visible;
            AddComboSpr(SetComboSpr);
        }

        public void AddComboSpr(UIElement label)
        {
            Grid.SetColumn(label, 0);
            Grid.SetRow(label, 2);
            ServerBd.Children.Add(label);

        }


        private void OnMyComboBoxChanged(object sender, SelectionChangedEventArgs e)
        {
  
            ValueMetod = ((sender as ComboBox).SelectedItem as ComboBoxItem).Content.ToString();
 
            DateTime dateTimeNull = Convert.ToDateTime("01.01.0001".Trim()).Date;
            if (datepiker == dateTimeNull)
            {
                var TextWindows = "Не установлена дата " + Environment.NewLine + "нового отчетного периода." + Environment.NewLine + "Выполнение процедуры отстановлено.";
                InstallB52.MessageErorInst(TextWindows);
                return;
            }
            ProcCleanBD25Run();
        }

        private void ComboBox_DropDownClosed(object sender, EventArgs e)
        {

        }


        // Процедура установки версии сборки.
        private void Versiya_Loaded(object sender, RoutedEventArgs e)
        {
            if (SetInitPanelSborka == 1)
            {
                string PuthConecto = Process.GetCurrentProcess().MainModule.FileName;
                string Versia = FileVersionInfo.GetVersionInfo(PuthConecto).ToString();  // версия файла.
                string VersiaT = Versia.Substring(Versia.IndexOf("FileVersion") + 12, Versia.IndexOf("FileDescription") - (Versia.IndexOf("FileVersion") + 12)).Replace("\r\n", "").Replace(" ", "");
                AdminPanels.ButtonPanel = new string[3];
                AdminPanels.ButtonPanel[0] = "Sborka";
                AdminPanels.ButtonPanel[1] = "VersyaSborki";
                AdminPanels.ButtonPanel[2] = "LoginUserName";
                InitTextOnOff();
                DateTime dtnow = DateTime.Now;
                if (AppStart.TableReestr["VersyaSborki"] == "") UpdateKeyReestr("VersyaSborki", VersiaT);
                if (AppStart.TableReestr["Sborka"] == "")
                {
                    ConectoWorkSpace.Administrator.AdminPanels.UpdateKeyReestr("Sborka", DateTime.Now.ToString("dd.MM.yyyy"));
                }
                else
                {
                    DateTime dt = DateTime.ParseExact(AppStart.TableReestr["Sborka"], "dd.MM.yyyy", System.Globalization.CultureInfo.InvariantCulture);
                    int VersyaSborki = Convert.ToInt16(VersiaT.Replace(".", ""));
                    if (VersyaSborki > Convert.ToInt16(AppStart.TableReestr["VersyaSborki"].Replace(".", "")))
                    {
                        UpdateKeyReestr("VersyaSborki", VersiaT);
                        ConectoWorkSpace.Administrator.AdminPanels.UpdateKeyReestr("Sborka", DateTime.Now.ToString("dd.MM.yyyy"));

                    }
                }



                SetInitPanelSborka = 0;
                Sborka.Content = Sborka.Content + VersiaT + "  Дата сборки: " + AppStart.TableReestr["Sborka"];
                Versiya_Bd.Content = Versiya_Bd.Content + AppStart.TableReestr["VersiyaBD"];
                NameUser.Text = AppStart.TableReestr["LoginUserName"];
                AliasFb25.Text = AppStart.TableReestr["NameServer25"];
                PuthFb25.Text = AppStart.TableReestr["ServerDefault25"].Length > 40 ? "..." + AppStart.TableReestr["ServerDefault25"].Substring(AppStart.TableReestr["ServerDefault25"].Length - 37, 37) : AppStart.TableReestr["ServerDefault25"];
                AliasFb30.Text = AppStart.TableReestr["NameServer30"];
                PuthFb30.Text = AppStart.TableReestr["ServerDefault30"].Length > 40 ? "..." + AppStart.TableReestr["ServerDefault30"].Substring(AppStart.TableReestr["ServerDefault30"].Length - 37, 37) : AppStart.TableReestr["ServerDefault30"];
                PuthBD.Text = AppStart.TableReestr["PuthSetBD25"].Length > 40 ? "..." + AppStart.TableReestr["PuthSetBD25"].Substring(AppStart.TableReestr["PuthSetBD25"].Length - 37, 37) : AppStart.TableReestr["PuthSetBD25"];
                AliasBd.Text =
                PortFb25.Text = AppStart.TableReestr["SetPortServer25"];
                PortFb30.Text = AppStart.TableReestr["SetPortServer30"];
                DrvKey.Foreground = Convert.ToInt16(AppStart.TableReestr["SetLocKeyOnOff"]) == 2 ? Brushes.Green : Brushes.Black;
                DrvServKey.Foreground = Convert.ToInt16(AppStart.TableReestr["BackOfServKeyOnOff"]) == 2 ? Brushes.Green : Brushes.Black;

                string CurrentSetBD = "SELECT * from CONNECTIONBD25 WHERE CONNECTIONBD25.PUTHBD = " + "'" + AppStart.TableReestr["PuthSetBD25"] + "'";
                DBConecto.DBopenFBConectionMemory(SystemBD.ConnnStringSystem().ToString(), "", "FbSystem");
                FbCommand UpdateKey = new FbCommand(CurrentSetBD, DBConecto.bdFbSystemConect);
                UpdateKey.CommandType = CommandType.Text;
                FbDataReader reader = UpdateKey.ExecuteReader();
                while (reader.Read()) { AliasBd.Text = reader[1].ToString(); }
                reader.Close();
                UpdateKey.Dispose();
                DBConecto.DBcloseFBConectionMemory("FbSystem");
                string[] FolderBack = { "Rozn", "Restoran", "FastFud", "Fitnes", "Hotel", "Mix" };
                CurrentSetBD = "SELECT * from REESTRBACK ";
                DBConecto.DBopenFBConectionMemory(SystemBD.ConnnStringSystem().ToString(), "", "FbSystem");
                int IndStroka = -1;
                string[] MasBack = new string[30];
                FbCommand InstBack = new FbCommand(CurrentSetBD, DBConecto.bdFbSystemConect);
                InstBack.CommandType = CommandType.Text;
                FbDataReader readerBack = InstBack.ExecuteReader();
                while (readerBack.Read())
                {
                    IndStroka++;
                    MasBack[IndStroka] = readerBack[0].ToString();

                }
                reader.Close();
                UpdateKey.Dispose();
                DBConecto.DBcloseFBConectionMemory("FbSystem");
                if (IndStroka >= 0)
                {
                    for (int i = 0; i <= IndStroka; i++)
                    {
                        for (int ind = 0; ind <= 5; ind++)
                        {
                            if (MasBack[i] == FolderBack[ind])
                            {
                                if (ind == 0) RoznicaBack.Foreground = Brushes.Green;
                                if (ind == 1) RestoranBack.Foreground = Brushes.Green;
                                if (ind == 2) FastBack.Foreground = Brushes.Green;
                                if (ind == 3) FitnesBack.Foreground = Brushes.Green;
                                if (ind == 4) HotelBack.Foreground = Brushes.Green;
                                if (ind == 5) KomplekcBack.Foreground = Brushes.Green;
                            }
                        }
                    }
                }

                DBConecto.DBcloseFBConectionMemory("FbSystem");
                string[] FolderFront = { "Rozn", "Restoran", "FastFud", "Fitnes", "Hotel" };
                CurrentSetBD = "SELECT * from REESTRFRONT ";
                DBConecto.DBopenFBConectionMemory(SystemBD.ConnnStringSystem().ToString(), "", "FbSystem");
                IndStroka = -1;
                string[] MasFront = new string[6];
                FbCommand InstFront = new FbCommand(CurrentSetBD, DBConecto.bdFbSystemConect);
                InstFront.CommandType = CommandType.Text;
                FbDataReader readerFront = InstFront.ExecuteReader();
                while (readerFront.Read())
                {
                    IndStroka++;
                    MasFront[IndStroka] = readerFront[0].ToString();
                }
                reader.Close();
                UpdateKey.Dispose();
                DBConecto.DBcloseFBConectionMemory("FbSystem");

                if (IndStroka >= 0)
                {
                    for (int i = 0; i <= IndStroka; i++)
                    {
                        for (int ind = 0; ind <= 4; ind++)
                        {
                            if (MasFront[i] == FolderFront[ind])
                            {
                                if (ind == 0) RoznicaFront.Foreground = Brushes.Green;
                                if (ind == 1) RestoranFront.Foreground = Brushes.Green;
                                if (ind == 2) FastFront.Foreground = Brushes.Green;
                                if (ind == 3) FitnesFront.Foreground = Brushes.Green;
                                if (ind == 4) HotelFront.Foreground = Brushes.Green;
                            }
                        }
                    }
                }


            }

        }

        private void UpdateNameUser_Click(object sender, RoutedEventArgs e)
        {

            if (NameUser.IsEnabled == true) NameUser.IsEnabled = false;
            else NameUser.IsEnabled = true;

        }

        private void LoginUserName_KeyUp(object sender, KeyEventArgs e)
        {
            UpdateKeyReestr("LoginUserName", NameUser.Text);
        }

        private void LocDiskSet_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            string BackUpSetUser = SystemConectoServers.PutchConecto + AppStart.TableReestr["LoginUserName"] + @"\";
            Directory.CreateDirectory(BackUpSetUser);
            PutchSetUser.Text = BackUpSetUser;
            RunSaveConf.IsEnabled = true;
            DirPathSet.IsEnabled = true;
        }

        private void FtpDiskSet_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {

            PutchSetUser.Text = @"ftp://conecto.ua/pack/BackUp/bd/";
            RunSaveConf.IsEnabled = true;
            DirPathSet.IsEnabled = false;

        }

        private void GoogleDiskSet_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            DirPathSet.IsEnabled = false;
        }

        private void RunBackUpSet_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {

        }

        private void DirPathSet_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {


            var dlg = new System.Windows.Forms.FolderBrowserDialog();
            dlg.Description = "Путь к  архиву";
            // Show open file dialog box
            //Nullable<bool> result = dlg.ShowDialog();
            System.Windows.Forms.DialogResult result = dlg.ShowDialog();
            if (result == System.Windows.Forms.DialogResult.OK)
            {
                //Чтение параметра и запись нового значения
                var ItemTextBoxPatch = (TextBox)LogicalTreeHelper.FindLogicalNode(this, "PutchDir_");
                PutchSetUser.Text = dlg.SelectedPath + AppStart.TableReestr["LoginUserName"] + @"\";
            }

        }



        #region Закладка "Сервера данных"

        // Процедура подготовки массива переключателей панели серевера данных и
        // отображения текущего состояния этих переключателей на панели. 
        private void InitPanel_ServerDB30(object sender, MouseButtonEventArgs e)
        {

            SelectAlias = (SelectAlias == "" || SelectAlias == "hub") ? "hub3" : SelectAlias;
            AppStart.TableReestr["SetPortServer30"] = AppStart.TableReestr["SetPortServer30"] == "" ? "3056" : AppStart.TableReestr["SetPortServer30"];
            Administrator.AdminPanels.UpdateKeyReestr("SetPortServer30", AppStart.TableReestr["SetPortServer30"]);
        }

        private void InitPanel_ServerDB25(object sender, MouseButtonEventArgs e)
        {
            SelectAlias = (SelectAlias == "" || SelectAlias == "hub3") ? "hub" : SelectAlias;
            AppStart.TableReestr["SetPortServer25"] = AppStart.TableReestr["SetPortServer25"] == "" ? "3055" : AppStart.TableReestr["SetPortServer25"];
            Administrator.AdminPanels.UpdateKeyReestr("SetPortServer25", AppStart.TableReestr["SetPortServer25"]);

        }
        // 
        private void InitPanel_ServerDB(object sender, MouseButtonEventArgs e)
        {
            InitPanelServerDB();
        }
        private void InitPanelServerDB()
        {

            if (SetInitPanelServerDB == 1 || SetInitPanelServerDB == 2)
            {
                CloseVisible();

                if (SetInitPanelServerDB == 1)
                {
                    var TextWindows = "Выполняется идентификация данных." + Environment.NewLine + "Пожалуйста подождите. ";
                    int AutoClose = 1;
                    int MesaggeTop = -170;
                    int MessageLeft = 300;
                    ConectoWorkSpace.MainWindow.MessageEnd(TextWindows, AutoClose, MesaggeTop, MessageLeft);
                    InitKeyOnOffServBD();
                    InitPanel_();
                    InitTextServBD();
                    ServerDB.IsEnabled = false;
                    SearchInstFB();
                    ServerDB.IsEnabled = true;
                    SetServerGrid("SELECT * from SERVERACTIVFB25");
                    SetServerGrid("SELECT * from SERVERACTIVFB30");
                    SetServerGrid("SELECT * from SERVERACTIVPOSTGRESQL");
                    Grid_Puth("SELECT * from CONNECTIONBD25 WHERE CONNECTIONBD25.NAMESERVER =" + "'" + AppStart.TableReestr["NameServer25"] + "'");
                    Grid_Puth("SELECT * from CONNECTIONBD30 WHERE CONNECTIONBD30.NAMESERVER = " + "'" + AppStart.TableReestr["NameServer30"] + "'");
                    Grid_Puth("SELECT * from CONNECTIONPOSTGRESQL WHERE CONNECTIONPOSTGRESQL.NAMESERVER = " + "'" + AppStart.TableReestr["NameServerPG"] + "'");
                    PuthSetServer25.Text = AppStart.TableReestr["ServerDefault25"];
                    PuthSetServer30.Text = AppStart.TableReestr["ServerDefault30"];
                    PuthSetServerPostGre.Text = AppStart.TableReestr["ServerDefaultPG"];
                }

                SetInitPanelServerDB = 0;
            }

        }
        public void CloseVisible()
        {

            OnOffSborka.Visibility = Visibility.Collapsed;
            LabelSborka.Visibility = Visibility.Collapsed;


            datePicker30_Server.Visibility = Visibility.Collapsed;

            OnChangePas25.Visibility = Visibility.Collapsed;
            ButonChangePas25.Visibility = Visibility.Collapsed;
            CarentPasswABD25Txt.Visibility = Visibility.Collapsed;
            EyeOldPassw25.Visibility = Visibility.Collapsed;
            OldPasswABD25.Visibility = Visibility.Collapsed;
            LabelPas.Visibility = Visibility.Collapsed;
            OnChangePas30.Visibility = Visibility.Collapsed;
            ButonChangePas30.Visibility = Visibility.Collapsed;
            CarentPasswABD30Txt.Visibility = Visibility.Collapsed;
            EyeOldPassw30.Visibility = Visibility.Collapsed;
            OldPasswABD30.Visibility = Visibility.Collapsed;
            LabelPas30.Visibility = Visibility.Collapsed;
            datePicker30_Server.IsDropDownOpen = false;
            SelectNewBD25.Visibility = Visibility.Collapsed;
            TextBox_Fbd25.Visibility = Visibility.Collapsed;
            Dir_Fbd25.Visibility = Visibility.Collapsed;
            LabelPuth.Visibility = Visibility.Collapsed;

            LabelPasPG.Visibility = Visibility.Collapsed;
            OldPasswPG.Visibility = Visibility.Collapsed;
            EyeOldPasswPG.Visibility = Visibility.Collapsed;
            CarentPasswPG.Visibility = Visibility.Collapsed;
            ButonChangePasPG.Visibility = Visibility.Collapsed;
            OnChangePasPG.Visibility = Visibility.Collapsed;
        }


        public static void InitKeyOnOffServBD()
        {
            AdminPanels.ButtonPanel = new string[8];
            AdminPanels.ButtonPanel[0] = "ServFB25OnOff";
            AdminPanels.ButtonPanel[1] = "ServFB30OnOff";
            AdminPanels.ButtonPanel[2] = "PostgresqlOnOff";
            AdminPanels.ButtonPanel[3] = "MsSqlOnOff";
            AdminPanels.ButtonPanel[4] = "SetPuthBD25";
            AdminPanels.ButtonPanel[5] = "SetPuthBD30";
            AdminPanels.ButtonPanel[6] = "SetBDPostGreSQL";
            AdminPanels.ButtonPanel[7] = "SetBDMsSQL";


            if (LoadTableRestr == 0) InitKeySystemFB("InitKey");
            InitKeyOnOff();
        }

        public static void InitTextServBD()
        {
            AdminPanels.ButtonPanel = new string[19];
            AdminPanels.ButtonPanel[0] = "PuthSetBD25";
            AdminPanels.ButtonPanel[1] = "PuthSetBD30";
            AdminPanels.ButtonPanel[2] = "PuthSetBDPostGreSQL";
            AdminPanels.ButtonPanel[3] = "PuthSetBDMsSQL";
            AdminPanels.ButtonPanel[4] = "SetPortServer25";
            AdminPanels.ButtonPanel[5] = "SetPortServer30";
            AdminPanels.ButtonPanel[6] = "ServerDefault25";
            AdminPanels.ButtonPanel[7] = "ServerDefault30";
            AdminPanels.ButtonPanel[8] = "NameServer25";
            AdminPanels.ButtonPanel[9] = "IdPort25";
            AdminPanels.ButtonPanel[10] = "NameServer30";
            AdminPanels.ButtonPanel[11] = "IdPort30";
            AdminPanels.ButtonPanel[12] = "PuthSetServer25";
            AdminPanels.ButtonPanel[13] = "PuthSetServer30";
            AdminPanels.ButtonPanel[14] = "UpdateB52";
            AdminPanels.ButtonPanel[15] = "ServerDefaultPG";
            AdminPanels.ButtonPanel[16] = "NameServerPG";
            AdminPanels.ButtonPanel[17] = "SetPortServerPG";
            AdminPanels.ButtonPanel[18] = "CurrentPasswABDPG";

            if (LoadTableRestr == 0) InitKeySystemFB("InitText");
            InitTextOnOff();
        }
        // процедура установки и присоединения новой БД  к серверу который не имеет присоединенных БД.
        private void SelectNewBD25_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {

            if (TextBox_Fbd25.Text == PathFileBDText) { AddSetBd25(); return; }
            PathFileBDText = TextBox_Fbd25.Text;
            NameObj = "NewServerSetPuthBD25";
            InstallB52.InstallSetBD25();
            if (Administrator.AdminPanels.ProcesEnd == 2) InstallB52.RunCreatBD25();

        }


        private void OnOffSborka_Click(object sender, RoutedEventArgs e)
        {
            if (!File.Exists(SelectPuth))
            {
                var TextWin = "Отсутствует указанный файл БД." + Environment.NewLine + "Процедура прекращена. ";
                int AutoCl = 1;
                int MesaggeT = -170;
                int MessageL = 600;
                InstallB52.MessageEnd(TextWin, AutoCl, MesaggeT, MessageL);
                return;
            }

            if (SborkaOnOff == 0)
            {
                SborkaOnOff = 1;
                if (!Directory.Exists(@"c:\BackupFdb\")) Directory.CreateDirectory(@"c:\BackupFdb\");
                string Port = "/" + (Administrator.AdminPanels.hubdate == "25" ? SetPort25 : SetPort30) + ":";
                string Temphub = SelectPuth.Substring(SelectPuth.LastIndexOf(@"\") + 1, SelectPuth.IndexOf(".") - SelectPuth.LastIndexOf(@"\") - 1);
                string CurrentFB = Administrator.AdminPanels.hubdate == "25" ? CurrentPasswFb25 : CurrentPasswFb30;
                string TCPIPBack = AppStart.TableReestr["SetTextIp4BackOf"] + Port + SelectAlias.Trim();
                string CleanHub = @"  c:\BackupFdb\" + Temphub + DateTime.Now.ToString("yyMMddHHmmss") + ".fbk";
                Administrator.AdminPanels.RunGbak = Administrator.AdminPanels.CurrentSereverPuth + (Administrator.AdminPanels.hubdate == "25" ? @"bin\" : "") + "gbak.exe";

                //Oбновление бд
                // Перваый этап создание архива БД.

                Administrator.AdminPanels.ArgumentCmd = "-b " + TCPIPBack + CleanHub + " -v -user sysdba -pass " + CurrentFB; //+ @" -y c:\BackupFdb\log.txt"
                SystemConecto.ErorDebag("Перваый этап создание архива БД" + Administrator.AdminPanels.RunGbak + Administrator.AdminPanels.ArgumentCmd, 1, 2);

                RunProcess(RunGbak, ArgumentCmd);

                File.Copy(SelectPuth, SystemConectoServers.PutchServerData + Temphub + DateTime.Now.ToString("yyMMddHHmmss") + ".fdb");
                File.Delete(SelectPuth);

                // Второй этап разархивирование архива
                Administrator.AdminPanels.ArgumentCmd = @" -rep " + CleanHub + "  " + TCPIPBack + @" -v -bu 200  -user sysdba -pass " + CurrentFB;

                RunProcess(RunGbak, ArgumentCmd);
            }
            if (SborkaOnOff == 1) { SborkaOnOff = 0; return; }
        }

        public static void BlokOpisConecto(string Fb2530 = "")
        {
            string ErrMessage = "";
 
                string PuthSrv = hubdate == "30" ? SetPort30 : SetPort25;
                DBConecto.ParamStringServerFB[1] = "SYSDBA";
                DBConecto.ParamStringServerFB[2] = hubdate == "25" ? CurrentPasswFb25 : CurrentPasswFb30;
                DBConecto.ParamStringServerFB[3] = "TCP/IP";
                DBConecto.ParamStringServerFB[4] = AppStart.TableReestr["SetTextIp4BackOf"] + "/" + PuthSrv + ":" + SelectAlias.Trim(); //SelectPuth;
                DBConecto.ParamStringServerFB[5] = SelectPuth;
                //DBConecto.ParamStringServerFB[6] = FbCharset.Windows1251.ToString();
                DBConecto.ParamStringServerFB[7] = PuthSrv;
                BdB52 = DBConecto.StringServerFB();

            using (FirebirdSql.Data.FirebirdClient.FbConnection bdFbDefConect = new FirebirdSql.Data.FirebirdClient.FbConnection(BdB52))
            {
                try
                { 
                bdFbDefConect.Open();
                }
                catch (FirebirdSql.Data.FirebirdClient.FbException ex)
                {
                SystemConecto.ErorDebag(" возникло исключение: " + Environment.NewLine +
                   " === IDCode: " + ex.ErrorCode.ToString() + Environment.NewLine +
                   " === Message: " + ex.Message.ToString() + Environment.NewLine +
                   " === Exception: " + ex.ToString(), 1);
                    ErrMessage = "Обнаружена ошибка в БД." + Environment.NewLine + ex.ToString().Substring(ex.ToString().IndexOf(Environment.NewLine) + 2);
 
                }

                try
                {
                    bdFbDefConect.Close();
                }
                catch (FirebirdSql.Data.FirebirdClient.FbException ex)
                {
                    SystemConecto.ErorDebag(" возникло исключение: " + Environment.NewLine +
                       " === IDCode: " + ex.ErrorCode.ToString() + Environment.NewLine +
                       " === Message: " + ex.Message.ToString() + Environment.NewLine +
                       " === Exception: " + ex.ToString(), 1);
                    ErrMessage = "Обнаружена ошибка в БД." + Environment.NewLine + ex.ToString().Substring(ex.ToString().IndexOf(Environment.NewLine) + 2);


                }
            }
        }


        public static void BlokOpisWriteBD(string Fb2530 = "")
        {
            string ErrMessage = "";
            string PuthSrv = hubdate == "30" ? SetPort30 : SetPort25;
            DBConecto.ParamStringServerFB[1] = "SYSDBA";
            DBConecto.ParamStringServerFB[2] = hubdate == "25" ? CurrentPasswFb25 : CurrentPasswFb30;
            DBConecto.ParamStringServerFB[3] = "TCP/IP";
            DBConecto.ParamStringServerFB[4] = AppStart.TableReestr["SetTextIp4BackOf"] + "/" + PuthSrv + ":" + AliasSatelite;
            DBConecto.ParamStringServerFB[5] = Putch_Satellite;
            DBConecto.ParamStringServerFB[7] = PuthSrv;
            BdB52Satellite = DBConecto.StringServerFB();

            using (FirebirdSql.Data.FirebirdClient.FbConnection ConectSatellite = new FirebirdSql.Data.FirebirdClient.FbConnection(BdB52Satellite))
            {
                try
                {
                    ConectSatellite.Open();
                }
                catch (FirebirdSql.Data.FirebirdClient.FbException ex)
                {
                    SystemConecto.ErorDebag(" возникло исключение: " + Environment.NewLine +
                       " === IDCode: " + ex.ErrorCode.ToString() + Environment.NewLine +
                       " === Message: " + ex.Message.ToString() + Environment.NewLine +
                       " === Exception: " + ex.ToString(), 1);
                    ErrMessage = "Обнаружена ошибка в БД." + Environment.NewLine + ex.ToString().Substring(ex.ToString().IndexOf(Environment.NewLine) + 2);

                }

                try
                {
                    ConectSatellite.Close();
                }
                catch (FirebirdSql.Data.FirebirdClient.FbException ex)
                {
                    SystemConecto.ErorDebag(" возникло исключение: " + Environment.NewLine +
                       " === IDCode: " + ex.ErrorCode.ToString() + Environment.NewLine +
                       " === Message: " + ex.Message.ToString() + Environment.NewLine +
                       " === Exception: " + ex.ToString(), 1);
                    ErrMessage = "Обнаружена ошибка в БД." + Environment.NewLine + ex.ToString().Substring(ex.ToString().IndexOf(Environment.NewLine) + 2);


                }
            }
        }

        private void OnOffLog_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            OnOffLogirovanie("25");
        }

        private void OnOffLog30_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            OnOffLogirovanie("30");
        }

        public void OnOffLogirovanie(string Fb2530)
        {
            if (!System.IO.File.Exists(AdminPanels.SelectPuth))
            {
                InstallB52.MessageEnd("Не выбрана БД." + Environment.NewLine + "Процедура прекращена. ", 1, -170, 600);
            }
            else
            {
                this.IsEnableButonFalse("OnOffLog");
                FbConnection.ClearAllPools();
                AdminPanels.hubdate = Fb2530;
                AdminPanels.BlokOpisConecto(Fb2530);
                string index = Fb2530.Contains("25") ? "OnOffLog" : "OnOffLog30";
                FbConnection bdFBConect = new FbConnection(AdminPanels.BdB52);
                bdFBConect.Open();
                if (Convert.ToInt32(AppStart.TableReestr[index].ToString()) == 2)
                {
                    InstallB52.MessageEnd("Выполняется включение логирования" + Environment.NewLine + "Подождите пожалуйста. ", 1, -170, 600);
                    InstallB52.LogInactivbd(bdFBConect);
                    this.LogOnOffBd("0", "", Fb2530);
                }
                else
                {
                    InstallB52.MessageEnd("Выполняется отключение логирования" + Environment.NewLine + "Подождите пожалуйста. ", 1, -170, 600);
                    InstallB52.LogActivBd(bdFBConect);
                    this.LogOnOffBd("2", "", Fb2530);
                }
                bdFBConect.Close();
                bdFBConect.Dispose();
                FbConnection.ClearAllPools();
            }

        }
        public void LogOnOffBd( string OnOff, string Satellit = "", string Fb2530 = "")
        {
            int Idcount = 0;
            string BdSatelit = "";
            string StrCreate = Fb2530.Contains("25") ? "select * from CONNECTIONBD25 where CONNECTIONBD25.PUTHBD = " + "'" + SelectPuth + "'" : "";
            StrCreate = Fb2530.Contains("30") ? "select * from CONNECTIONBD30 where CONNECTIONBD30.PUTHBD = " + "'" + SelectPuth + "'" : StrCreate;
            DBConecto.DBopenFBConectionMemory(SystemBD.ConnnStringSystem().ToString(), "", "FbSystem");
            FbCommand SelectTable = new FbCommand(StrCreate, DBConecto.bdFbSystemConect);
            FbDataReader ReadOutTable = SelectTable.ExecuteReader();
            while (ReadOutTable.Read()) { Idcount++; BdSatelit = ReadOutTable[6].ToString(); }
            ReadOutTable.Close();
            if (Idcount != 0)
            { 
                StrCreate = Fb2530.Contains("25") ? "UPDATE CONNECTIONBD25 SET LOG = '" + (OnOff == "2" ? "Stop'" : " '")+ " where CONNECTIONBD25.PUTHBD = " + "'" + SelectPuth + "'" : "";
                StrCreate = Fb2530.Contains("30") ? "UPDATE CONNECTIONBD30 SET LOG = '" + (OnOff == "2" ? "Stop'" : " '") + " where CONNECTIONBD30.PUTHBD = " + "'" + SelectPuth + "'" : StrCreate;
                if (Satellit != "" ) StrCreate = (Fb2530.Contains("25") ? "UPDATE CONNECTIONBD25 SET SATELLITE = '": "UPDATE CONNECTIONBD30 SET SATELLITE = '") + (Satellit != SelectPuth ? Satellit : "") + "'" +
                        (Fb2530.Contains("25") ? " where CONNECTIONBD25.PUTHBD = ": " where CONNECTIONBD30.PUTHBD = ") + "'" + SelectPuth + "'"  ; 
                DBConecto.UniQuery ModifyKey = new DBConecto.UniQuery(StrCreate, "FB");
                ModifyKey.ExecuteUNIScalar();           
            }

            ReadOutTable.Dispose();
            DBConecto.DBcloseFBConectionMemory("FbSystem");
            Grid_Puth(Fb2530.Contains("25") ? "SELECT * from CONNECTIONBD25": "SELECT * from CONNECTIONBD30");
            string OnOffPng = OnOff == "2" ? "2" : "1";
            if (Satellit == "") Interface.CurrentStateInst(Fb2530.Contains("25") ? "OnOffLog": "OnOffLog30", OnOff, "on_off_"+ OnOffPng + ".png", Fb2530.Contains("25") ? OnOffLog: OnOffLog30);
            if (Satellit != "") Interface.CurrentStateInst(Fb2530.Contains("25") ? "Satellite": "Satellite30", OnOff, "on_off_" + OnOffPng + ".png", Fb2530.Contains("25") ? Satellite: Satellite30);
        }


        private void Satellite_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            CreatSatellite("25");
        }

        private void Satellite30_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            CreatSatellite("30");
        }

        public void CreatSatellite(string fb2530 = "")
        { 
            if (!File.Exists(SelectPuth))
            {
                var TextWin = "Не выбрана БД." + Environment.NewLine + "Процедура прекращена. ";
                int AutoCl = 1;
                int MesaggeT = -170;
                int MessageL = 600;
                InstallB52.MessageEnd(TextWin, AutoCl, MesaggeT, MessageL);
                return;
            }
            BlokOpisConecto();

            string IdSatellite = fb2530.Contains("25") ? "Satellite" : "Satellite30";
            if (Convert.ToInt32(AppStart.TableReestr[IdSatellite].ToString()) == 2)
            {
                InstallB52.SatelliteDeletbd();
                LogOnOffBd("0", SelectPuth, fb2530);
            }
            else
            {
                var TextWin = "Выполняется создание БД спутник" + Environment.NewLine + "Подождите пожалуйста. ";
                int AutoCl = 1;
                int MesaggeT = -170;
                int MessageL = 600;
                InstallB52.MessageEnd(TextWin, AutoCl, MesaggeT, MessageL);

                string PuthSatellite = SelectPuth.Substring(0, SelectPuth.LastIndexOf(@"\")+1) + "Satellite" + Administrator.AdminPanels.SelectAlias + ".fdb";
                string NameAliasSatelite = "Satellite" + Administrator.AdminPanels.SelectAlias;
                if (fb2530.Contains("25"))
                {
 
                    var GchangeaAliases25 = ConectoWorkSpace.INI.ReadFile(CurrentSereverPuth + "aliases.conf");
                    GchangeaAliases25.Set(NameAliasSatelite, PuthSatellite, true);
                    int Idcount = 0; int Idcountout = 0;
                    string[] fileLines = File.ReadAllLines(CurrentSereverPuth + "aliases.conf");
                    string[] fileoutLines = new string[20];
                    foreach (string x in fileLines)
                    {
                        if (x.Count() != 0 && x.Length != 0)
                        {
                            fileoutLines[Idcountout] = fileLines[Idcount];
                            Idcountout++;
                        }
                        Idcount++;
                    }
                    File.WriteAllLines(CurrentSereverPuth + "aliases.conf", fileoutLines);
                    Inst2530 =ImageObj = "25";
                    InstallB52.RestartFB25(CurrentSereverPuth, NameServer25);

                    string StrInsert = "INSERT INTO CONNECTIONBD25  values ('" + PuthSatellite + "', '" + NameAliasSatelite + "', '" + NameServer25 + "', '" + CurrentSereverPuth + "', '','','')";
                    DBConecto.DBopenFBConectionMemory(SystemBD.ConnnStringSystem().ToString(), "", "FbSystem");
                    DBConecto.UniQuery CountQuery = new DBConecto.UniQuery(StrInsert, "FB");
                    CountQuery.UserQuery = string.Format(StrInsert, "CONNECTIONBD25");
                    CountQuery.ExecuteUNIScalar();
                    DBConecto.DBcloseFBConectionMemory("FbSystem");

                }
                if (fb2530.Contains("30"))
                { 
                    var GchangeaAliases30 = ConectoWorkSpace.INI.ReadFile(CurrentSereverPuth + "databases.conf");
                    GchangeaAliases30.Set(NameAliasSatelite, PuthSatellite, true);
                    int Idcount = 0; int Idcountout = 0;
                    string []fileLines = File.ReadAllLines(CurrentSereverPuth + "databases.conf");
                    string[] fileoutLines = new string[100];
                    foreach (string x in fileLines)
                    {
                        if (x.Count() != 0 && x.Length != 0)
                        {
                            fileoutLines[Idcountout] = fileLines[Idcount];
                            Idcountout++;
                        }
                        Idcount++;
                    }
                    File.WriteAllLines(CurrentSereverPuth + "databases.conf", fileoutLines);
                    Inst2530 = ImageObj = "30";
                    InstallB52.RestartFB25(CurrentSereverPuth, NameServer30);

                    string StrInsert = "INSERT INTO CONNECTIONBD30  values ('" + PuthSatellite + "', '" + NameAliasSatelite + "', '" + NameServer30 + "', '" + CurrentSereverPuth + "', '','','')";
                    DBConecto.DBopenFBConectionMemory(SystemBD.ConnnStringSystem().ToString(), "", "FbSystem");
                    DBConecto.UniQuery CountQuery = new DBConecto.UniQuery(StrInsert, "FB");
                    CountQuery.UserQuery = string.Format(StrInsert, "CONNECTIONBD25");
                    CountQuery.ExecuteUNIScalar();
                    DBConecto.DBcloseFBConectionMemory("FbSystem");
                }
 

                InstallB52.SatelliteCreatBd(PuthSatellite, fb2530);
                LogOnOffBd("2", PuthSatellite, fb2530);
 
            }
        }
    
        public void grid25_Loaded(object sender, RoutedEventArgs e)
        {
            
            if (AppStart.TableReestr["NameServer25"] !="")
            {
                Grid_Puth("SELECT * from CONNECTIONBD25 WHERE CONNECTIONBD25.NAMESERVER =" + "'" + AppStart.TableReestr["NameServer25"] + "'");
            }

        }

        public void gridServisBD25_Loaded(object sender, RoutedEventArgs e)
        {
            Grid_Puth("SELECT * from CONNECTIONBD25");
        }

        public void grid30_Loaded(object sender, RoutedEventArgs e)
        {
            
            if (AppStart.TableReestr["NameServer30"] != "")
            {
                SelectPuth = ""; SelectAlias = ""; NameServer30 = "";
                Grid_Puth("SELECT * from CONNECTIONBD30 WHERE CONNECTIONBD30.NAMESERVER =" + "'" + AppStart.TableReestr["NameServer30"] + "'");
            }
        }
        public static void InsertDataGrid(string Stroka, string Strcount, string StrInsert, string Conect, string SetPuth, string SetAlias, string NameServ,string SetServPuth, string DateUpdate,string Log, string Satellite)
        {
  
                int Idcount = 0;
                DBConecto.DBopenFBConectionMemory(SystemBD.ConnnStringSystem().ToString(), "", "FbSystem");
                FbCommand SelectTable = new FbCommand(Stroka, DBConecto.bdFbSystemConect);
                FbDataReader ReadOutTable = SelectTable.ExecuteReader();
                while (ReadOutTable.Read())
                {
                    SelectAlias = ReadOutTable[2].ToString();
                    Idcount = Idcount + 1;
                }
                ReadOutTable.Close();
                if (Idcount == 0)
                {
                    string StrCreate = StrInsert +  "'" + SetPuth + "'" + ", '" + SetAlias + "', '" + NameServ + "', '" + SetServPuth + "', '" + DateUpdate + "', '" + Log + "', '" + Satellite + "')";
                    DBConecto.UniQuery CountQuery = new DBConecto.UniQuery(StrCreate, "FB");
                    CountQuery.UserQuery = string.Format(StrCreate, Conect); //"CONNECTIONBD25"
                    CountQuery.ExecuteUNIScalar();

                }
                SelectTable.Dispose();


        }

  

        public static void SetServerGrid(string CurrentSetBD)
        {
            List<ServerTable30> result30 = new List<ServerTable30>(1);
            List<ServerTable> result = new List<ServerTable>(1);
            List<ServerTablePG> resultPg = new List<ServerTablePG>(1);
            DBConecto.DBopenFBConectionMemory(SystemBD.ConnnStringSystem().ToString(), "", "FbSystem");
            FbCommand UpdateKey = new FbCommand(CurrentSetBD, DBConecto.bdFbSystemConect);
            UpdateKey.CommandType = CommandType.Text;
            FbDataReader reader = UpdateKey.ExecuteReader();
            while (reader.Read())
            {
                if (CurrentSetBD.Contains("25")) result.Add(new ServerTable(reader[0].ToString(), reader[1].ToString(), reader[2].ToString(), reader[4].ToString(), reader[5].ToString()));
                if (CurrentSetBD.Contains("30")) result30.Add(new ServerTable30(reader[0].ToString(), reader[1].ToString(), reader[2].ToString(), reader[4].ToString(), reader[5].ToString()));
                if (CurrentSetBD.Contains("SQL")) resultPg.Add(new ServerTablePG(reader[0].ToString(), reader[1].ToString(), reader[2].ToString(), reader[4].ToString(), reader[5].ToString()));
            }
            reader.Close();
            UpdateKey.Dispose();
            DBConecto.DBcloseFBConectionMemory("FbSystem");
            System.Windows.Application.Current.Dispatcher.Invoke(new Action(delegate ()
            {
                AdminPanels ConectoWorkSpace_InW = AppStart.LinkMainWindow("WAdminPanels");
                if (CurrentSetBD.Contains("25")) ConectoWorkSpace_InW.TablPuthServer.ItemsSource = result;
                if (CurrentSetBD.Contains("30")) ConectoWorkSpace_InW.TablPuthServer30.ItemsSource = result30;
                if (CurrentSetBD.Contains("SQL")) ConectoWorkSpace_InW.TablPuthServerPostGre.ItemsSource = resultPg;
            }));


        }

        private void datePicker25_Clean_LostFocus(object sender, RoutedEventArgs e)
        {
 
            DeletConect.IsEnabled = false;
            CleanConect.IsEnabled = false;
            UpdateBD25.IsEnabled = false;
            ChangeDefault25.IsEnabled = false;
            SetChangePassw.IsEnabled = false;
            StoptServFB25.IsEnabled = false;
            //ClickMouseUp_Servergrid25 = false;
        }

  
   
        public static void Grid_Puth( string CurrentSetBD)
        {
            List<MyTable30> result30 = new List<MyTable30>(1);
            List<MyTable> result = new List<MyTable>(1);
            List<TablePG> resultPG = new List<TablePG>(1);
            DBConecto.DBopenFBConectionMemory(SystemBD.ConnnStringSystem().ToString(), "", "FbSystem");
            FbCommand UpdateKey = new FbCommand(CurrentSetBD, DBConecto.bdFbSystemConect);
            UpdateKey.CommandType = CommandType.Text;
            FbDataReader reader = UpdateKey.ExecuteReader();
            while (reader.Read())
            {
                if (CurrentSetBD.Contains("25")) result.Add(new MyTable(reader[0].ToString(), reader[1].ToString(), reader[2].ToString(), reader[5].ToString(), reader[6].ToString()));
                if (CurrentSetBD.Contains("30")) result30.Add(new MyTable30( reader[0].ToString(), reader[1].ToString(), reader[2].ToString(), reader[5].ToString(), reader[6].ToString()));
                if (CurrentSetBD.Contains("PG")) resultPG.Add(new TablePG(reader[0].ToString(), reader[1].ToString(), reader[2].ToString(), reader[5].ToString(), reader[6].ToString()));
            }
            reader.Close();
            UpdateKey.Dispose();
            DBConecto.DBcloseFBConectionMemory("FbSystem");
            System.Windows.Application.Current.Dispatcher.Invoke(new Action(delegate ()
            {
                AdminPanels ConectoWorkSpace_InW = AppStart.LinkMainWindow("WAdminPanels");
                if (CurrentSetBD.Contains("25"))
                {
                    ConectoWorkSpace_InW.TablPuthBD.ItemsSource =  result;
                    ConectoWorkSpace_InW.TablAlias.ItemsSource = result;
                    
                }
                if (CurrentSetBD.Contains("30"))
                {
                    ConectoWorkSpace_InW.TablPuthBD30.ItemsSource = result30;
                    ConectoWorkSpace_InW.TablAlias30.ItemsSource = result30;
                }
                if (CurrentSetBD.Contains("PG"))
                {
                    ConectoWorkSpace_InW.TablPuthBDPostGreSql.ItemsSource = resultPG;
                    ConectoWorkSpace_InW.TablAlias_PostGreSQL.ItemsSource = resultPG;

                }
            }));
        }

        public static void HubGrid_Puth(string CurrentSetBD)
        {
            if (CurrentSetBD.Contains("25")) Administrator.AdminPanels.CreateConnectionBD25();
            if (CurrentSetBD.Contains("30")) Administrator.AdminPanels.CreateConnectionBD30();
            List<MyTable> result = new List<MyTable>(1);
            List<MyTable30> result30 = new List<MyTable30>(1);
           
            DBConecto.DBopenFBConectionMemory(SystemBD.ConnnStringSystem().ToString(), "", "FbSystem");
            FbCommand UpdateKey = new FbCommand(CurrentSetBD, DBConecto.bdFbSystemConect);
            UpdateKey.CommandType = CommandType.Text;
            FbDataReader reader = UpdateKey.ExecuteReader();
            while (reader.Read())
            {
                if (CurrentSetBD.Contains("25")) result.Add(new MyTable( reader[0].ToString(), reader[1].ToString(), reader[2].ToString(), reader[5].ToString(), reader[6].ToString()));
                if (CurrentSetBD.Contains("30")) result30.Add(new MyTable30( reader[0].ToString(), reader[1].ToString(), reader[2].ToString(), reader[5].ToString(), reader[6].ToString()));
            }
            reader.Close();
            UpdateKey.Dispose();
            DBConecto.DBcloseFBConectionMemory("FbSystem");
            System.Windows.Application.Current.Dispatcher.Invoke(new Action(delegate ()
            {
                WinSetHub ConectoWorkSpace_InW = AppStart.LinkMainWindow("WinSetHubW");

                if (CurrentSetBD.Contains("25")) ConectoWorkSpace_InW.TablAliasBD.ItemsSource = result;
                if (CurrentSetBD.Contains("30")) ConectoWorkSpace_InW.TablAliasBD.ItemsSource = result30;
            }));


        }
  

        // Процедура дозаписи путей к базам Firebird_2_5
        public static void InsertConect(string PathFileBDText, string DefaultNameSrever)
        {
            DBConecto.DBopenFBConectionMemory(SystemBD.ConnnStringSystem().ToString(), "", "FbSystem");
            int Idcount = 0;
            string StrCreate = "select * from CONNECTIONBD25 where CONNECTIONBD25.PUTHBD = " + "'" + PathFileBDText + "'";
            FbCommand SelectTable = new FbCommand(StrCreate, DBConecto.bdFbSystemConect);
            FbDataReader ReadOutTable = SelectTable.ExecuteReader();
            while (ReadOutTable.Read()) {  Idcount++; }
            ReadOutTable.Close();
            if (Idcount == 0)
            {
                ProcesEnd = 0;
                //Administrator.AdminPanels.SelectAlias = (PathFileBDText.Substring(PathFileBDText.LastIndexOf(@"\")+1, PathFileBDText.LastIndexOf(@".") - PathFileBDText.LastIndexOf(@"\")-1));
                string StrInsert = "INSERT INTO CONNECTIONBD25  values ('" + PathFileBDText + "', '" + SelectAlias + "', '" + DefaultNameSrever + "', '" + SetPuth25 + "', '','','')";
                DBConecto.UniQuery CountQuery = new DBConecto.UniQuery(StrInsert, "FB");
                CountQuery.UserQuery = string.Format(StrInsert, "CONNECTIONBD25");
                CountQuery.ExecuteUNIScalar();
            }
            SelectTable.Dispose();
 
            StrCreate = "UPDATE CONNECTIONBD25 SET UPDATETIME = '' WHERE CONNECTIONBD25.PUTHBD = " + "'" + PathFileBDText + "'";
            DBConecto.UniQuery UpdateName = new DBConecto.UniQuery(StrCreate, "FB");
            UpdateName.ExecuteUNIScalar();
            DBConecto.DBcloseFBConectionMemory("FbSystem");
            Grid_Puth("SELECT * from CONNECTIONBD25 WHERE CONNECTIONBD25.NAMESERVER = " + "'" + DefaultNameSrever + "'");

        }

        // Процедура дозаписи путей к базам Firebird_3_0
        public static void InsertConect30(string PathFileBDText, string DefaultNameSrever)
        {
            DBConecto.DBopenFBConectionMemory(SystemBD.ConnnStringSystem().ToString(), "", "FbSystem");
            int Idcount = 0;
            string StrCreate = "select * from CONNECTIONBD30 where CONNECTIONBD30.PUTHBD = " + "'" + PathFileBDText + "'";
            FbCommand SelectTable = new FbCommand(StrCreate, DBConecto.bdFbSystemConect);
            FbDataReader ReadOutTable = SelectTable.ExecuteReader();
            while (ReadOutTable.Read()) { Idcount ++; }
            ReadOutTable.Close();
            if (Idcount == 0)
            {
               
                string InsertExecute = "SELECT count(*) from CONNECTIONBD30";
                DBConecto.UniQuery CountQuery = new DBConecto.UniQuery(InsertExecute, "FB");
                string CountTable = CountQuery.ExecuteUNIScalar() == null ? "" : CountQuery.ExecuteUNIScalar().ToString();
                string InsertCount = DateTime.Now.ToString("MMddHHmmsstt");
                Administrator.AdminPanels.SelectAlias = (PathFileBDText.Substring(PathFileBDText.LastIndexOf(@"\") + 1, PathFileBDText.LastIndexOf(@".") - PathFileBDText.LastIndexOf(@"\") - 1));
                string StrInsert = "INSERT INTO CONNECTIONBD30  values ('" + PathFileBDText + "', '" + SelectAlias + "', '" + DefaultNameSrever + "', '" + SetPuth30 + "', '','','')";
                CountQuery.UserQuery = string.Format(StrInsert, "CONNECTIONBD30");
                CountQuery.ExecuteUNIScalar();

            }
            SelectTable.Dispose();
            DBConecto.DBcloseFBConectionMemory("FbSystem");
            Grid_Puth("SELECT * from CONNECTIONBD30 WHERE CONNECTIONBD30.NAMESERVER =" + "'" + AppStart.TableReestr["NameServer30"] + "'");


        }


  
        // Поцедура оcтновки серевера FireBird 2.5
        private void StoptServFB25_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            StoptServFB25.Foreground = Brushes.Indigo;
            if (SetActiv25 == "Stop")
            {
                var TextWin = "Не активный сервер стоит " + Environment.NewLine + "Выполнение процедуры невозможно. ";
                int AutoCl = 1;
                int MesaggeTp = -80;
                int MessageLef = 800;
                InstallB52.MessageEnd(TextWin, AutoCl, MesaggeTp, MessageLef);
                return;
            }
            IsEnaledServer25();
            ImageObj = "StoptServFB25";
            if(InstallB52.RemoveServerFB25(SetPuth25, SetName25)>0)
            {

                var TextWindows = "Остановка сервера Firebird 2.5" + Environment.NewLine + " успешно выполнена";
                int AutoClose = 1;
                int MesaggeTop = -170;
                int MessageLeft = 800;
                InstallB52.MessageEnd(TextWindows, AutoClose, MesaggeTop, MessageLeft);

                string StrCreate = "UPDATE SERVERACTIVFB25 SET ACTIVONOFF = 'Stop' WHERE SERVERACTIVFB25.PUTH = " + "'" + SetPuth25 + "'";
                ModifyTable(StrCreate);
                SetServerGrid("SELECT * from SERVERACTIVFB25");
            }
            StoptServFB25.Foreground = Brushes.White;


        }

        public static void ModifyTable(string Comand)
        {
            DBConecto.DBopenFBConectionMemory(SystemBD.ConnnStringSystem().ToString(), "", "FbSystem");
            DBConecto.UniQuery UpdateQuery = new DBConecto.UniQuery(Comand, "FB");
            UpdateQuery.ExecuteUNIScalar();
            DBConecto.DBcloseFBConectionMemory("FbSystem");
        }
        
        
        // Поцедура оатновки серевера FireBird 3.0
        private void StoptServFB30_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            StoptServFB30.Foreground = Brushes.Indigo;
            if (SetActiv30 == "Stop")
            {
                var TextWin = "Не активный сервер стоит " + Environment.NewLine + "Выполнение процедуры невозможно. ";
                int AutoCl = 1;
                int MesaggeTp = -80;
                int MessageLef = 800;
                InstallB52.MessageEnd(TextWin, AutoCl, MesaggeTp, MessageLef);
                return;
            }
            ImageObj = "StoptServFB30";
            if (InstallB52.RemoveServerFB25(SetPuth30, SetName30) > 0)
            {
                var TextWindows = "Остановка сервера Firebird 3.0" + Environment.NewLine + " успешно выполнена";
                int AutoClose = 1;
                int MesaggeTop = -170;
                int MessageLeft = 800;
                InstallB52.MessageEnd(TextWindows, AutoClose, MesaggeTop, MessageLeft);

                string StrCreate = "UPDATE SERVERACTIVFB30 SET ACTIVONOFF = 'Stop' WHERE SERVERACTIVFB30.PUTH = " + "'" + SetPuth30 + "'";
                ModifyTable(StrCreate);
                SetServerGrid("SELECT * from SERVERACTIVFB30");
                Grid_Puth("SELECT * from CONNECTIONBD30 WHERE CONNECTIONBD30.NAMESERVER = " + "'" + SetName30 + "'");
                StoptServFB30.Foreground = Brushes.White;
            }

        }

        // Поцедура перезапуска серевера FireBird 2.5
        private void RestartServFB25_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            string TextWindows = "Перезапуск сервера Firebird 2.5" + Environment.NewLine + " успешно выполнен";
            ImageObj = "RestartServFB25"; Inst2530 = "25";
            IsEnaledServer25();
            InstallB52.RestartFB25(SetPuth25, SetName25);
            if (Administrator.AdminPanels.ImageObj != "No")
            {
                string StrCreate = "UPDATE SERVERACTIVFB25 SET ACTIVONOFF = 'Activ' WHERE SERVERACTIVFB25.PUTH = " + "'" + SetPuth25 + "'";
                ModifyTable(StrCreate);
                SetServerGrid("SELECT * from SERVERACTIVFB25");
                Grid_Puth("SELECT * from CONNECTIONBD25 WHERE CONNECTIONBD25.NAMESERVER = " + "'" + SetName25 + "'");
 
                
            }
            else TextWindows = "Перезапуск сервера Firebird 2.5" + Environment.NewLine + " Не выполнен";
            int AutoClose = 1;
            int MesaggeTop = -170;
            int MessageLeft = 800;
            InstallB52.MessageEnd(TextWindows, AutoClose, MesaggeTop, MessageLeft);

        }

        // Поцедура перезапуска серевера FireBird 3.0
        private void RestartServFB30_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            string TextWindows = "Перезапуск сервера Firebird 3.0" + Environment.NewLine + " успешно выполнен";
            ImageObj = "RestartServFB30";
            IsEnaledServer30();
            InstallB52.RestartFB25(SetPuth30, SetName30);
            if (Administrator.AdminPanels.ImageObj != "No")
            {
                string StrCreate = "UPDATE SERVERACTIVFB30 SET ACTIVONOFF = 'Activ' WHERE SERVERACTIVFB30.PUTH = " + "'" + SetPuth30 + "'";
                ModifyTable(StrCreate);
                SetServerGrid("SELECT * from SERVERACTIVFB30");
                Grid_Puth("SELECT * from CONNECTIONBD30 WHERE CONNECTIONBD30.NAMESERVER = " + "'" + SetName30 + "'");
                IsEnaledServer30();

            }
            else TextWindows = "Перезапуск сервера Firebird 3.0" + Environment.NewLine + " Не выполнен";
            int AutoClose = 1;
            int MesaggeTop = -170;
            int MessageLeft = 800;
            InstallB52.MessageEnd(TextWindows, AutoClose, MesaggeTop, MessageLeft);

        }

        // Процедура инсталяции FireBird 2.5
        private void ServFB25OnOff_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            DeleteDefaultServer = 1;
            CallProgram = "ServFB25OnOff";
            ImageObj = "ServFB25OnOff";
            Interface.ObjektOnOff("ServFB25OnOff", ref ServFB25OnOff, new Install.Metods { NameClass = "ConectoWorkSpace.InstallB52", Install = "InstallServBD2_5", UnInstal = "UnInstallServBD2_5" });

        }
        // Процедура распаковки и установки БД для FireBird 2.5
        private void SetPuthBD25_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            ImageObj = "SetPuthBD25";
            Interface.ObjektOnOff("SetPuthBD25", ref SetPuthBD25, new Install.Metods { NameClass = "ConectoWorkSpace.InstallB52", Install = "InstallSetPuthBD2_5", UnInstal = "UnInstallSetPuthBD2_5" });

        }


        // Процедура инсталяции FireBird 3.0
        private void ServFB30OnOff_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            DeleteDefaultServer = 1;
            ImageObj = "ServFB30OnOff";
            Interface.ObjektOnOff("ServFB30OnOff", ref ServFB30OnOff, new Install.Metods { NameClass = "ConectoWorkSpace.InstallB52", Install = "InstallServFB3_0", UnInstal = "UnInstallServFB3_0" });

        }
        // Процедура распаковки и установки БД для FireBird 3.0
        private void SetPuthBD30_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            ImageObj = "SetPuthBD30";
            Interface.ObjektOnOff("SetPuthBD30", ref SetPuthBD30, new Install.Metods { NameClass = "ConectoWorkSpace.InstallB52", Install = "InstallSetPuthBD30", UnInstal = "UnInstallSetPuthBD3_0" });
        }

  
 

        // Процедура загрузки датагрид из таблицы активных серверов.
        private void Servergrid25_Loaded(object sender, RoutedEventArgs e)
        {
             CreateServerActivFB25();
             SetServerGrid("SELECT * from SERVERACTIVFB25");
        }

 
        public void IsEnaledServer25()
        {
            DeletSrever25.IsEnabled = false;
            RestartServFB25.IsEnabled = false;
            SetDefaultServer.IsEnabled = false;
            SetChangePassw.IsEnabled = false;
            StoptServFB25.IsEnabled = false;
            CaptionConfServ.IsEnabled = false;
            CaptionAliasBD.IsEnabled = false;
            //ClickMouseUp_Servergrid25 = false;
        }

        public void IsEnaledServer30()
        {
            DeletSrever30.IsEnabled = false;
            RestartServFB30.IsEnabled = false;
            SetDefaultServer30.IsEnabled = false;
            SetChangePassw30.IsEnabled = false;
            StoptServFB30.IsEnabled = false;
        }
        
        // Процедура выбора активного сервера.
        private void Servergrid25_MouseUp(object sender, MouseButtonEventArgs e)
        {
            DBConecto.DBopenFBConectionMemory(SystemBD.ConnnStringSystem().ToString(), "", "FbSystem");
            string InsertExecute = "SELECT count(*) from SERVERACTIVFB25";
            DBConecto.UniQuery CountQuery = new DBConecto.UniQuery(InsertExecute, "FB");
            string CountTable = CountQuery.ExecuteUNIScalar() == null ? "" : CountQuery.ExecuteUNIScalar().ToString();

            if (Convert.ToUInt32(CountTable) != 0)
            {
                if (TablPuthServer.SelectedItem != null)
                {
                    ServerTable path = TablPuthServer.SelectedItem as ServerTable;
                    SetPort25 = Convert.ToString(path.Port);
                    SetPuth25 = path.Puth;
                    SetName25 = path.Name;
                    SetActiv25 = path.State;
                    CurrentPasswFb25 = path.CurrentPassw; 

                    //byte[] CurrentPas = Encoding.Default.GetBytes(path.CurrentPassw); /*GetBytes(path.CurrentPassw);*/
                    // FromAes256(path.CurrentPassw, KeySecret);
                    
                    DeletSrever25.IsEnabled = true;
                    RestartServFB25.IsEnabled = true;
                    SetDefaultServer.IsEnabled = true;
                    SetChangePassw.IsEnabled = true;
                    StoptServFB25.IsEnabled = true;
                    ClickMouseUp_Servergrid25 = true;
                    CaptionConfServ.IsEnabled = true;
                    CaptionAliasBD.IsEnabled = true;
                    NewSecurity.IsEnabled = true;
                   


                    CountConect25("SELECT count(*) from CONNECTIONBD25 WHERE CONNECTIONBD25.NAMESERVER =" + "'" + SetName25 + "'");
                    Grid_Puth("SELECT * from CONNECTIONBD25 WHERE CONNECTIONBD25.NAMESERVER =" + "'" + SetName25 + "'");
                }
            }
            DBConecto.DBcloseFBConectionMemory("FbSystem");
        }

        public byte[] GetBytes(string str)
        {

            int NumberChars = str.Length;
            byte[] bytes = new byte[NumberChars / 2];
            for (int i = 0; i < NumberChars; i += 2)
                bytes[i / 2] = Convert.ToByte(str.Substring(i, 2), 16);
            //byte[] bytes = new byte[str.Length * sizeof(char)];
            //System.Buffer.BlockCopy(str.ToCharArray(), 0, bytes, 0, bytes.Length);
            return bytes;
        }



        public void CountConect25(string CountConect25)
        {

            DBConecto.DBopenFBConectionMemory(SystemBD.ConnnStringSystem().ToString(), "", "FbSystem");
            DBConecto.UniQuery CountQuery = new DBConecto.UniQuery(CountConect25, "FB");
            string CountTable = CountQuery.ExecuteUNIScalar() == null ? "" : CountQuery.ExecuteUNIScalar().ToString();
            if(CountConect25.Contains("25")) DirSetBD25.Content = Convert.ToUInt32(CountTable) == 0 ? "Создать" : "Добавить";
            if (CountConect25.Contains("30")) DirSetBD30.Content = Convert.ToUInt32(CountTable) == 0 ? "Создать" : "Добавить";
            if (CountConect25.Contains("PG")) AddConectPostGreSql.Content = Convert.ToUInt32(CountTable) == 0 ? "Создать" : "Добавить";          
            DBConecto.DBcloseFBConectionMemory("FbSystem");
        }

        // Процедура назначения серевера по умолчаню
        private void SetDefaultServer_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            string PatchSR = SetPuth25;
            if (SetActiv25 == "Stop")
            {
                var TextWindows = "Не активный сервер нельзя использовать по умолчанию " + Environment.NewLine + "Выполнение процедуры невозможно. ";
                int AutoClose = 1;
                int MesaggeTop = -170;
                int MessageLeft = 800;
                InstallB52.MessageEnd(TextWindows, AutoClose, MesaggeTop, MessageLeft);
                return;
            }

            if (!SystemConecto.File_(PatchSR + @"bin\instsvc.exe", 5))
            {
                var TextWindows = "Отсутствует файл instsvc.exe." + Environment.NewLine + "Изменение пути невозможно ";
                int AutoClose = 1;
                int MesaggeTop = 3;
                int MessageLeft = 650;
                InstallB52.MessageEnd(TextWindows, AutoClose, MesaggeTop, MessageLeft);
                return;
            }
            System.Windows.Application.Current.Dispatcher.Invoke(new Action(delegate ()
            {
                ConectoWorkSpace.Administrator.AdminPanels ConectoWorkSpace_InW = AppStart.LinkMainWindow("WAdminPanels");
                ConectoWorkSpace_InW.PuthSetServer25.Text = PatchSR;
                ConectoWorkSpace_InW.PuthSetBD25.Text = "";
                ConectoWorkSpace_InW.DefNameServer25.Text = ""; 
                Interface.CurrentStateInst("SetPuthBD25", "0", "on_off_1.png", ConectoWorkSpace_InW.SetPuthBD25);
            }));
            UpdateKeyReestr("ServerDefault25", SetPuth25);
            UpdateKeyReestr("NameServer25", SetName25);
            UpdateKeyReestr("PuthSetBD25", "");
            IsEnaledServer25();
            // Перегрузить таблицу алиасов
            IndRecno = 0;
            LoadAlias(SetName25, SetPuth25);
            Grid_Puth("SELECT * from CONNECTIONBD25 WHERE CONNECTIONBD25.NAMESERVER = " + "'" + SetName25 + "'");
        }

        // Процедура добавления в список активных серверов
        private void AddServer_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            ConectoWorkSpace.Administrator.AdminPanels ConectoWorkSpace_InW = AppStart.LinkMainWindow("WAdminPanels");
            if (ConectoWorkSpace_InW == null) return;           
            AddSever.Foreground = Brushes.Indigo;
            CallProgram = "AddServer";
            string TextMesage = "Вы действительно хотите установить новый сервер?";
            Inst2530 = "25";
            SetUpdateRestore = 0;
            WinOblakoSetUpdate OblakoSetWindow = new WinOblakoSetUpdate(TextMesage);
            OblakoSetWindow.Owner = ConectoWorkSpace_InW;
            OblakoSetWindow.Top = ConectoWorkSpace_InW.Top + 150;
            OblakoSetWindow.Left = ConectoWorkSpace_InW.ScrollViewerd.Width - (OblakoSetWindow.Width * 2) - 100;
            OblakoSetWindow.Show();
        }


        // Процедура удаления информации о сервере из списка активных серверов
        private void DeletSrever25_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DeletSrever25.Foreground = Brushes.Indigo;
            ConectoWorkSpace.Administrator.AdminPanels ConectoWorkSpace_InW = AppStart.LinkMainWindow("WAdminPanels");
            if (ConectoWorkSpace_InW == null)return;
            IsEnaledServer25();
            Administrator.AdminPanels.ImageObj = "StoptServFB25";
            string TextMesage = "Вы действительно хотите удалить сервер?";
            Inst2530 = "del25";
            SetUpdateRestore = 0;

            WinOblakoSetUpdate OblakoSetWindow = new WinOblakoSetUpdate(TextMesage);
            OblakoSetWindow.Owner = ConectoWorkSpace_InW; 
            OblakoSetWindow.Top = ConectoWorkSpace_InW.Top + 100;
            OblakoSetWindow.Left = ConectoWorkSpace_InW.ScrollViewerd.Width - (OblakoSetWindow.Width * 2)-100;
            OblakoSetWindow.Show();

 
        }

        public static  void SreverFB25Delete()
        {
             AppStart.RenderInfo Arguments01 = new AppStart.RenderInfo() { };
             Arguments01.argument1 = "Delete";
             Arguments01.argument2 = SetActiv25;
            Thread thStartTimer01 = new Thread(InstallB52.UnInstallServFB25);
                thStartTimer01.SetApartmentState(ApartmentState.STA);
                thStartTimer01.IsBackground = true; // Фоновый поток
                thStartTimer01.Start(Arguments01);
                InstallB52.IntThreadStart++;
 
        }

        // Открыть редактор конфигурации сервера
        private void CaptionConfServ_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            string CurrentIdPort = "";
            SetWinSetHub = "FirebirdConf";
            NotepadIni(SetPuth25, "firebird.conf");
            foreach (string strk in File.ReadLines(SetPuth25 + "firebird.conf"))
            {
                if (strk.StartsWith("RemoteServicePort") == true)
                {
                    CurrentIdPort = File.ReadLines(SetPuth25 + "firebird.conf").First(xx => xx.StartsWith("RemoteServicePort")).Split('=')[1].Trim();
                }
            }

            if (CurrentIdPort != SetPort25)
            {
                string StrCreate = "select * from SERVERACTIVFB25 where SERVERACTIVFB25.PORT = " + "'" + CurrentIdPort + "'";
                if (SelectTable(StrCreate) == 0)
                {
                    string StrUpdate = "UPDATE SERVERACTIVFB25 SET PORT = " + "'" + CurrentIdPort + "'" + " WHERE SERVERACTIVFB25.PUTH = " + "'" + SetPuth25 + "'";
                    ModifyTable(StrUpdate);
                    InstallB52.RestartFB25(SetPuth25, SetName25);
                    SetServerGrid("SELECT * from SERVERACTIVFB25");
                }
                else
                {
                    var GchangeConf25 = ConectoWorkSpace.INI.ReadFile(SetPuth25 + @"firebird.conf");
                    GchangeConf25.Set("RemoteServicePort", SetPort25, true);
                    var TextWin = "Введнный прорт активен " + Environment.NewLine + "Замена порта запрещена. ";
                    int AutoCl = 1;
                    int MesaggeTp = -80;
                    int MessageLef = 800;
                    InstallB52.MessageEnd(TextWin, AutoCl, MesaggeTp, MessageLef);

                }
 
            }
        }

        // Открыть редактор конфигурации сервера
        private void CaptionConfServ30_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            string CurrentIdPort = "";
            SetWinSetHub = "FirebirdConf";
            NotepadIni(SetPuth30, "firebird.conf");
            foreach (string strk in File.ReadLines(SetPuth30 + "firebird.conf"))
            {
                if (strk.StartsWith("RemoteServicePort") == true)
                {
                    CurrentIdPort = File.ReadLines(SetPuth30 + "firebird.conf").First(xx => xx.StartsWith("RemoteServicePort")).Split('=')[1].Trim();
                }
            }

            if (CurrentIdPort != SetPort30)
            {
                string StrCreate = "select * from SERVERACTIVFB30 where SERVERACTIVFB30.PORT = " + "'" + CurrentIdPort + "'";
                if (SelectTable(StrCreate) == 0)
                {
                    
                    string StrUpdate = "UPDATE SERVERACTIVFB30 SET PORT = " + "'" + CurrentIdPort + "'" + " WHERE SERVERACTIVFB30.PUTH = " + "'" + SetPuth30 + "'";
                    ModifyTable(StrUpdate);
                    InstallB52.RestartFB25(SetPuth30, SetName30);
                    SetServerGrid("SELECT * from SERVERACTIVFB30");
                }
                else
                {
                    var GchangeConf30 = ConectoWorkSpace.INI.ReadFile(SetPuth30 + @"firebird.conf");
                    GchangeConf30.Set("RemoteServicePort", SetPort30, true);
                    var TextWin = "Введнный прорт активен " + Environment.NewLine + "Замена порта запрещена. ";
                    int AutoCl = 1;
                    int MesaggeTp = -80;
                    int MessageLef = 800;
                    InstallB52.MessageEnd(TextWin, AutoCl, MesaggeTp, MessageLef);

                }

            }
        }


        public static  int SelectTable(string Command)
        {
            int Idcount = 0;
            DBConecto.DBopenFBConectionMemory(SystemBD.ConnnStringSystem().ToString(), "", "FbSystem");
            FbCommand SelectTable = new FbCommand(Command, DBConecto.bdFbSystemConect);
            FbDataReader ReadOutTable = SelectTable.ExecuteReader();
            while (ReadOutTable.Read()) { Idcount++; }
            ReadOutTable.Close();
            SelectTable.Dispose();
            DBConecto.DBcloseFBConectionMemory("FbSystem");
            return Idcount;
        }


        // Открыть редактор конфигурации сервера
        private void CaptionAliasBD_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

            SetWinSetHub = "AliasesConf";
            NotepadIni(SetPuth25, "aliases.conf");

        }

        // Открыть редактор конфигурации сервера
        private void CaptionAliasBD30_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

            SetWinSetHub = "DatabasesConf";
            NotepadIni(SetPuth30, "databases.conf");

        }

        private void NewSecurity_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var TextWindows = "";
            int AutoClose = 1;
            int MesaggeTop = -170;
            int MessageLeft = 800;
            if (ClickMouseUp_Servergrid25 == false)
            {
                TextWindows = "Не выбран серевер БД." + Environment.NewLine + "Процедура прекращена ";
                InstallB52.MessageEnd(TextWindows, AutoClose, MesaggeTop, MessageLeft);
                return;
            }
            FalseIsEnabled25();
            NewSecurity.Foreground = Brushes.Indigo;
            PathFileBD_Click();
            if (!PathFileBDText.Contains("security2.fdb")  || PathFileBDText.Length == 0)
            {
                NewSecurity.Foreground = Brushes.White;
                TextWindows = "Не выбран security2.fdb." + Environment.NewLine + "Процедура прекращена ";
                InstallB52.MessageEnd(TextWindows, AutoClose, MesaggeTop, MessageLeft);
                NewSecurity.Foreground = Brushes.White;
                return;
            }

            // Удалить старый security2.fdb
            File.Delete(SetPuth25+ "security2.fdb");
            // Записать новый security2.fdb
            File.Copy(PathFileBDText, SetPuth25 + "security2.fdb");
            NewSecurity.Foreground = Brushes.White;
            TextWindows = "Обновление security2.fdb." + Environment.NewLine + "Успешно завершено. ";
            InstallB52.MessageEnd(TextWindows, AutoClose, MesaggeTop, MessageLeft);

        }

        private void NewSecurity30_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var TextWindows = "";
            int AutoClose = 1;
            int MesaggeTop = -170;
            int MessageLeft = 800;
            if (ClickMouseUp_Servergrid25 == false)
            {
                TextWindows = "Не выбран серевер БД." + Environment.NewLine + "Процедура прекращена ";
                InstallB52.MessageEnd(TextWindows, AutoClose, MesaggeTop, MessageLeft);
                return;
            }
            FalseIsEnabled30();
            NewSecurity.Foreground = Brushes.Indigo;
            PathFileBD_Click();
            if (!PathFileBDText.Contains("security3.fdb") || PathFileBDText.Length == 0)
            {
                NewSecurity.Foreground = Brushes.White;
                TextWindows = "Не выбран security3.fdb." + Environment.NewLine + "Процедура прекращена ";
                InstallB52.MessageEnd(TextWindows, AutoClose, MesaggeTop, MessageLeft);
                NewSecurity.Foreground = Brushes.White;
                return;
            }

            // Удалить старый security3.fdb
            File.Delete(SetPuth30 + "security3.fdb");
            // Записать новый security3.fdb
            File.Copy(PathFileBDText, SetPuth30 + "security3.fdb");
            NewSecurity.Foreground = Brushes.White;
            TextWindows = "Обновление security3.fdb." + Environment.NewLine + "Успешно завершено. ";
            InstallB52.MessageEnd(TextWindows, AutoClose, MesaggeTop, MessageLeft);

        }



        public static void SreverFB30Delete()
        {
            AppStart.RenderInfo Arguments01 = new AppStart.RenderInfo() { };
            Arguments01.argument1 = "Delete";
            Arguments01.argument2 = Administrator.AdminPanels.SetActiv30;
            Thread thStartTimer01 = new Thread(InstallB52.UnInstallServFB30);
            thStartTimer01.SetApartmentState(ApartmentState.STA);
            thStartTimer01.IsBackground = true; // Фоновый поток
            thStartTimer01.Start(Arguments01);
            InstallB52.IntThreadStart++;

        }



        // Процедурa выбоа  БД для соединения с сервером
        // 1.1. Fbd25
        private void DirSetBD25_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

            if (DirSetBD25.Foreground == Brushes.Indigo)
            {
 
                DirSetBD25.Foreground = Brushes.White;
                TablPuthBD.IsEnabled = true;
                return;
            }

            if (ClickMouseUp_Servergrid25 == false)
            {

                if (CountServer25 > 1 )
                {
                    TablPuthBD.IsEnabled = true;
                    var TextWindows = "Не выбран серевер БД." + Environment.NewLine + "Процедура прекращена ";
                    int AutoClose = 1;
                    int MesaggeTop = -170;
                    int MessageLeft = 800;
                    InstallB52.MessageEnd(TextWindows, AutoClose, MesaggeTop, MessageLeft);
                    return;
                }
                else
                {
                    SetPort25 = PortServer25[0];
                    SetPuth25 = NameExeFile25[0];
                    SetName25 = ServerName25[0];
                    CurrentPasswFb25 = CurrentPassw25[0];
                }

            }
            FalseIsEnabled25();
            if (!File.Exists(SetPuth25 + @"bin\gbak.exe"))
            {
                TablPuthBD.IsEnabled = true;
                var TextWindows = "Не выбран серевер БД." + Environment.NewLine + "Процедура прекращена ";
                int AutoClose = 1;
                int MesaggeTop = -170;
                int MessageLeft = 800;
                InstallB52.MessageEnd(TextWindows, AutoClose, MesaggeTop, MessageLeft);
                return;
            }
            DirSetBD25.Foreground = Brushes.Indigo;

            PathFileBD_Click();
            if ((!PathFileBDText.ToUpper().Contains(".FDB") && !PathFileBDText.ToUpper().Contains(".GDB") && !PathFileBDText.ToUpper().Contains(".FBK")
                && !PathFileBDText.ToUpper().Contains(".ZIP") && !PathFileBDText.ToUpper().Contains(".RAR") && !PathFileBDText.ToUpper().Contains(".7Z")) || PathFileBDText.Length == 0)
            {
                DirSetBD25.Foreground = Brushes.White;
                TablPuthBD.IsEnabled = true;
            }
            else
            {
                if (!System.IO.File.Exists("c:\\Program Files (x86)\\7-Zip\\7z.exe") && !System.IO.File.Exists((string)SystemConecto.PutchApp + "Utils\\7za\\7za.exe"))
                {
                    AdminPanels.Arh7Zip_return = "";
                    AdminPanels.Arh7Zip();
                    if (AdminPanels.Arh7Zip_return != "") return;
                    for (int index = 0; index <= 1000; ++index)
                    {
                        Thread.Sleep(5000);
                        if (System.IO.File.Exists("c:\\Program Files (x86)\\7-Zip\\7z.exe")) break;
                    }
                }
                this.TextBox_Fbd25.Text = AdminPanels.PathFileBDText;
                this.AddSetBd25();
            }



        }

        public static int CheckFdb()
        {
            int num1 = 1;
            DBConecto.DBopenFBConectionMemory(SystemBD.ConnnStringSystem("Dynamic").ToString(), "", "FbSystem");
            int num2 = 0;
            string cmdText = "select * from CONNECTIONBD25 where CONNECTIONBD25.PUTHBD = '" + AdminPanels.PathAddDataServer + "'";
            if (AdminPanels.Inst2530 == "30")
                cmdText = "select * from CONNECTIONBD30 where CONNECTIONBD30.PUTHBD = '" + AdminPanels.PathAddDataServer + "'";
            FbCommand fbCommand = new FbCommand(cmdText, (FbConnection)DBConecto.bdFbSystemConect);
            FbDataReader fbDataReader = fbCommand.ExecuteReader();
            while (fbDataReader.Read())
                ++num2;
            fbDataReader.Close();
            if ((uint)num2 > 0U)
            {
                System.Windows.Application.Current.Dispatcher.Invoke(new Action(delegate ()
                {
                    ConectoWorkSpace.Administrator.AdminPanels ConectoWorkSpace_InW = AppStart.LinkMainWindow("WAdminPanels");
                    if (AdminPanels.Inst2530 == "30")
                {
                        ConectoWorkSpace_InW.TablPuthBD30.IsEnabled = true;
                        ConectoWorkSpace_InW.DirSetBD30.Foreground = (Brush)Brushes.White;
                }
                else
                {
                        ConectoWorkSpace_InW.TablPuthBD.IsEnabled = true;
                        ConectoWorkSpace_InW.DirSetBD25.Foreground = (Brush)Brushes.White;
                }
                }));
                InstallB52.MessageEnd("Выбранная БД" + Environment.NewLine + "Ранее добавлена ", 1, -570, 800);
                int num3;
                return num3 = 0;
            }
            fbCommand.Dispose();
            DBConecto.DBcloseFBConectionMemory("FbSystem");
            return num1;
            }
        
        public void AddSetBd25()
        {
            string PatchSR = AdminPanels.Inst2530 == "30" ? AdminPanels.SetPuth30 : AdminPanels.SetPuth25;
            if (!System.IO.File.Exists(PatchSR + (AdminPanels.Inst2530 == "30" ? "instsvc.exe" : "bin\\instsvc.exe")))
            {
                InstallB52.MessageEnd("Отсутствует файл instsvc.exe." + Environment.NewLine + "Процедура прекращена ", 1, -170, 800);
                if (AdminPanels.Inst2530 == "30")
                {
                    this.TablPuthBD30.IsEnabled = true;
                    this.DirSetBD30.Foreground = (Brush)Brushes.White;
                }
                else
                {
                    this.TablPuthBD.IsEnabled = true;
                    this.DirSetBD25.Foreground = (Brush)Brushes.White;
                }
            }
            else if (AdminPanels.PathFileBDText.Contains(AdminPanels.Inst2530 == "30" ? "security3back.fdb" : "security2back.fdb"))
            {
                if (AdminPanels.Inst2530 == "30")
                {
                    this.TablPuthBD30.IsEnabled = true;
                    this.DirSetBD30.Foreground = (Brush)Brushes.White;
                }
                else
                {
                    this.TablPuthBD.IsEnabled = true;
                    this.DirSetBD25.Foreground = (Brush)Brushes.White;
                }
            }
            else
            {
                string str1 = AdminPanels.SelectAlias = AdminPanels.PathFileBDText.Substring(AdminPanels.PathFileBDText.LastIndexOf("\\") + 1, AdminPanels.PathFileBDText.LastIndexOf(".") - AdminPanels.PathFileBDText.LastIndexOf("\\") - 1).Trim();
                string pathFileBdText = AdminPanels.PathFileBDText;
                string NameBd1 = AdminPanels.SelectAlias.Length <= 12 ? AdminPanels.SelectAlias : AdminPanels.SelectAlias.Substring(0, AdminPanels.SelectAlias.Length - 12);
                AdminPanels.PathAddDataServer = AdminPanels.PathFileBDText.ToUpper().Contains(".FDB") || AdminPanels.PathFileBDText.ToUpper().Contains(".GDB") ? AdminPanels.PathFileBDText : SystemConectoServers.PutchServerData + NameBd1 + ".fdb";
                if (pathFileBdText.ToUpper().Contains(".FBK"))
                    AdminPanels.PathAddDataServer = AdminPanels.PathFileBDText.Substring(0, AdminPanels.PathFileBDText.LastIndexOf("\\") + 1) + NameBd1 + ".fdb";
                AdminPanels.SelectAlias = NameBd1;
                AdminPanels.OutPath = pathFileBdText.Substring(0, pathFileBdText.LastIndexOf("\\")) + "\\temp";
                if (AdminPanels.CheckFdb() == 0)
                    return;
                AdminPanels.PathAddDataServer = (AdminPanels.PathFileBDText.Substring(0, AdminPanels.PathFileBDText.LastIndexOf("\\") + 1) + NameBd1 + ".fdb").ToUpper();
                if (AdminPanels.CheckFdb() == 0)
                    return;
                InstallB52.MessageEnd("Подождите пожалуйста" + Environment.NewLine + "Выполняется обработка файла ", 1, -570, 800);
                if (pathFileBdText.ToUpper().Contains(".FDB") || pathFileBdText.ToUpper().Contains(".GDB"))
                {
                    this.UpdateAlias(pathFileBdText, NameBd1, PatchSR);
                    if (AdminPanels.Inst2530 == "25")
                        AdminPanels.InsertConect(AdminPanels.PathAddDataServer, AdminPanels.SetName25);
                    if (AdminPanels.Inst2530 == "30")
                        AdminPanels.InsertConect30(AdminPanels.PathAddDataServer, AdminPanels.SetName30);
                }
                else if (pathFileBdText.ToUpper().Contains(".FBK"))
                {
                    AdminPanels.PathAddDataServer = AdminPanels.PathFileBDText.Substring(0, AdminPanels.PathFileBDText.LastIndexOf("\\") + 1) + NameBd1 + ".fdb";
                    this.UpdateAlias(AdminPanels.PathAddDataServer, NameBd1, PatchSR);
                    AdminPanels.hubdate = AppStart.TableReestr["SetTextIp4BackOf"] + "/" + (AdminPanels.Inst2530 == "30" ? AdminPanels.SetPort30 : AdminPanels.SetPort25) + ":" + NameBd1;
                    AdminPanels.RunGbak = AdminPanels.Inst2530 == "30" ? AdminPanels.SetPuth30 + "gbak.exe" : AdminPanels.SetPuth25 + "bin\\gbak.exe";
                    string str2 = " -c ";
                    if (System.IO.File.Exists(AdminPanels.PathAddDataServer))
                        str2 = " -rep ";
                    AdminPanels.ArgumentCmd = str2 + AdminPanels.PathFileBDText + " " + AdminPanels.hubdate + " -v  -bu 200 -user sysdba -pass " + (AdminPanels.Inst2530 == "30" ? AdminPanels.CurrentPasswFb30 : AdminPanels.CurrentPasswFb25);
                    SystemConecto.ErorDebag(AdminPanels.RunGbak + AdminPanels.ArgumentCmd, 1, 2, (SystemConecto.StruErrorDebag)null);
                    AdminPanels.RunProcess(AdminPanels.RunGbak, AdminPanels.ArgumentCmd, "");
                    if (AdminPanels.Inst2530 == "25")
                        AdminPanels.InsertConect(AdminPanels.PathAddDataServer, AdminPanels.SetName25);
                    if (AdminPanels.Inst2530 == "30")
                        AdminPanels.InsertConect30(AdminPanels.PathAddDataServer, AdminPanels.SetName30);
                }
                else
                {
                    Directory.CreateDirectory(AdminPanels.OutPath);
                    if (pathFileBdText.ToUpper().Contains(".7Z") || pathFileBdText.ToUpper().Contains(".ZIP"))
                    {
                        if (System.IO.File.Exists("c:\\Program Files (x86)\\7-Zip\\7z.exe") || System.IO.File.Exists((string)SystemConecto.PutchApp + "Utils\\7za\\7za.exe"))
                        {
                            string CmdArguments = "  e  \"" + pathFileBdText + "\" -o\"" + AdminPanels.OutPath + "\" -y";
                            string CmdRun = System.IO.File.Exists("c:\\Program Files (x86)\\7-Zip\\7z.exe") ? "c:\\Program Files (x86)\\7-Zip\\7z.exe" : (string)SystemConecto.PutchApp + "Utils\\7za\\7za.exe";
                            SystemConecto.ErorDebag(CmdRun + CmdArguments, 1, 2, (SystemConecto.StruErrorDebag)null);
                            AdminPanels.RunProcess(CmdRun, CmdArguments, "");
                        }
                        else
                        {
                            if (AdminPanels.Inst2530 == "30")
                            {
                                this.TablPuthBD30.IsEnabled = true;
                                this.DirSetBD30.Foreground = (Brush)Brushes.White;
                            }
                            else
                            {
                                this.TablPuthBD.IsEnabled = true;
                                this.DirSetBD25.Foreground = (Brush)Brushes.White;
                            }
                            InstallB52.MessageEnd("Распаковка не выполнена." + Environment.NewLine + "Отсутствует архиватор 7-Zip\\7z.exe ", 1, -170, 800);
                            return;
                        }
                    }
                    if (pathFileBdText.ToUpper().Contains(".RAR"))
                    {
                        string CmdRun = "";
                        if (System.IO.File.Exists("c:\\Program Files\\WinRAR\\rar.exe"))
                            CmdRun = "c:\\Program Files\\WinRAR\\rar.exe";
                        if (System.IO.File.Exists("c:\\Program Files (x86)\\WinRAR\\rar.exe"))
                            CmdRun = "c:\\Program Files (x86)\\WinRAR\\rar.exe";
                        if (CmdRun != "")
                        {
                            string CmdArguments = "  x  \"" + pathFileBdText + "\" \"" + AdminPanels.OutPath + "\"";
                            AdminPanels.RunProcess(CmdRun, CmdArguments, "");
                        }
                        else
                        {
                            if (AdminPanels.Inst2530 == "30")
                            {
                                this.TablPuthBD30.IsEnabled = true;
                                this.DirSetBD30.Foreground = (Brush)Brushes.White;
                            }
                            else
                            {
                                this.TablPuthBD.IsEnabled = true;
                                this.DirSetBD25.Foreground = (Brush)Brushes.White;
                            }
                            InstallB52.MessageEnd("Распаковка не выполнена." + Environment.NewLine + "Отсутствует архиватор WinRAR\\rar.exe ", 1, -170, 800);
                            return;
                        }
                    }
                    string[] strArray = new string[10];
                    int index = -1;
                    foreach (string file in Directory.GetFiles(AdminPanels.OutPath))
                    {
                        ++index;
                        string str2 = file.Substring(file.LastIndexOf("\\") + 1, file.Length - (file.LastIndexOf("\\") + 1));
                        if (str2.ToUpper().Contains(".FBK"))
                        {
                            string NameBd2 = str2.Substring(0, str2.LastIndexOf("."));
                            if (pathFileBdText.ToUpper().Contains(".7Z") && !str2.Contains("Satellite"))
                                NameBd2 = NameBd2.Length <= 12 ? NameBd2 : NameBd2.Substring(0, NameBd2.Length - 12);
                            AdminPanels.SelectAlias = NameBd2;
                            AdminPanels.PathAddDataServer = AdminPanels.PathFileBDText.Substring(0, AdminPanels.PathFileBDText.LastIndexOf("\\") + 1) + NameBd2 + ".fdb";
                            this.UpdateAlias(AdminPanels.PathAddDataServer, NameBd2, PatchSR);
                            AdminPanels.hubdate = AppStart.TableReestr["SetTextIp4BackOf"] + "/" + (AdminPanels.Inst2530 == "30" ? AdminPanels.SetPort30 : AdminPanels.SetPort25) + ":" + NameBd2;
                            AdminPanels.RunGbak = AdminPanels.Inst2530 == "30" ? AdminPanels.SetPuth30 + "gbak.exe" : AdminPanels.SetPuth25 + "bin\\gbak.exe";
                            AdminPanels.ArgumentCmd = " -c " + AdminPanels.OutPath + "\\" + str2 + " " + AdminPanels.hubdate + " -v  -bu 200 -user sysdba -pass " + (AdminPanels.Inst2530 == "30" ? AdminPanels.CurrentPasswFb30 : AdminPanels.CurrentPasswFb25);
                            SystemConecto.ErorDebag(AdminPanels.RunGbak + AdminPanels.ArgumentCmd, 1, 2, (SystemConecto.StruErrorDebag)null);
                            AdminPanels.RunProcess(AdminPanels.RunGbak, AdminPanels.ArgumentCmd, "");
                            if (!System.IO.File.Exists(AdminPanels.PathAddDataServer))
                            {
                                string str3 = "gbak" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".log";
                                AdminPanels.hubdate = AppStart.TableReestr["SetTextIp4BackOf"] + "/" + (AdminPanels.Inst2530 == "30" ? AdminPanels.SetPort30 : AdminPanels.SetPort25) + ":" + NameBd2;
                                AdminPanels.RunGbak = AdminPanels.Inst2530 == "30" ? AdminPanels.SetPuth30 + "gbak.exe" : AdminPanels.SetPuth25 + "bin\\gbak.exe";
                                AdminPanels.ArgumentCmd = " -c " + str2 + " " + AdminPanels.hubdate + " -v  -y \"" + (string)SystemConecto.PutchApp + str3 + "\" -bu 200 -user sysdba -pass " + (AdminPanels.Inst2530 == "30" ? AdminPanels.CurrentPasswFb30 : AdminPanels.CurrentPasswFb25);
                                SystemConecto.ErorDebag(AdminPanels.RunGbak + AdminPanels.ArgumentCmd, 1, 2, (SystemConecto.StruErrorDebag)null);
                                AdminPanels.RunProcess(AdminPanels.RunGbak, AdminPanels.ArgumentCmd, "1");
                                this.TablPuthBD.IsEnabled = true;
                                this.DirSetBD25.Foreground = (Brush)Brushes.White;
                                InstallB52.MessageEnd("Распаковка не выполнена." + Environment.NewLine + "Протокол смотри в gbak.log ", 1, -170, 800);
                                return;
                            }
                            if (AdminPanels.Inst2530 == "25")
                                AdminPanels.InsertConect(AdminPanels.PathAddDataServer, AdminPanels.SetName25);
                            if (AdminPanels.Inst2530 == "30")
                                AdminPanels.InsertConect30(AdminPanels.PathAddDataServer, AdminPanels.SetName30);
                        }
                        else
                        {
                            string str3 = file.Substring(file.LastIndexOf("\\") + 1, file.Length - (file.LastIndexOf("\\") + 1));
                            System.IO.File.Copy(file, pathFileBdText.Substring(0, pathFileBdText.LastIndexOf("\\") + 1) + str3, true);
                            if ((str3.ToUpper().Contains(".FDB") || str3.ToUpper().Contains(".GDB")) && !str3.Contains("security2back.fdb") && !str3.Contains("security3back.fdb"))
                            {
                                AdminPanels.PathAddDataServer = pathFileBdText.Substring(0, pathFileBdText.LastIndexOf("\\") + 1) + str3;
                                string NameBd2 = str3.Substring(0, str3.LastIndexOf("."));
                                AdminPanels.SelectAlias = NameBd2;
                                this.UpdateAlias(AdminPanels.PathAddDataServer, NameBd2, PatchSR);
                                if (AdminPanels.Inst2530 == "25")
                                    AdminPanels.InsertConect(AdminPanels.PathAddDataServer, AdminPanels.SetName25);
                                if (AdminPanels.Inst2530 == "30")
                                    AdminPanels.InsertConect30(AdminPanels.PathAddDataServer, AdminPanels.SetName30);
                            }
                        }
                        strArray[index] = AdminPanels.PathAddDataServer;
                    }
                    if (index >= 1)
                    {
                        string str2 = "UPDATE CONNECTIONBD25 SET SATELLITE = '" + strArray[1] + "' WHERE CONNECTIONBD25.PUTHBD = '" + strArray[0] + "'";
                        if (AdminPanels.Inst2530 == "30")
                            str2 = "UPDATE CONNECTIONBD30 SET SATELLITE = '" + strArray[1] + "' WHERE CONNECTIONBD30.PUTHBD = '" + strArray[0] + "'";
                        DBConecto.DBopenFBConectionMemory(SystemBD.ConnnStringSystem("Dynamic").ToString(), "", "FbSystem");
                        new DBConecto.UniQuery(str2, "FB").ExecuteUNIScalar("");
                        DBConecto.DBcloseFBConectionMemory("FbSystem");
                        if (AdminPanels.Inst2530 == "25")
                            AdminPanels.Grid_Puth("SELECT * from CONNECTIONBD25 WHERE CONNECTIONBD25.NAMESERVER = '" + AdminPanels.SetName25 + "'");
                        if (AdminPanels.Inst2530 == "30")
                            AdminPanels.Grid_Puth("SELECT * from CONNECTIONBD30 WHERE CONNECTIONBD30.NAMESERVER = '" + AdminPanels.SetName30 + "'");
                    }
                    if (System.IO.File.Exists(AdminPanels.OutPath + (AdminPanels.Inst2530 == "30" ? "\\security3back.fdb" : "\\security2back.fdb")))
                    {
                        string TextMesage = "Обновить из архива security" + (AdminPanels.Inst2530 == "30" ? "3.fdb?" : "2.fdb? ");
                        System.Windows.Application.Current.Dispatcher.Invoke(new Action(delegate ()
                        {
                            ConectoWorkSpace.Administrator.AdminPanels ConectoWorkSpace_InW = AppStart.LinkMainWindow("WAdminPanels");
                            new WinSetPuth(TextMesage)
                            {
                                Owner = ((Window)ConectoWorkSpace_InW),
                                Top = (ConectoWorkSpace_InW.Top + 7.0 + ConectoWorkSpace_InW.Close_F.Margin.Top + (ConectoWorkSpace_InW.Close_F.Height - 2.0) + 470.0),
                                Left = (ConectoWorkSpace_InW.Left + ConectoWorkSpace_InW.Close_F.Margin.Left + 700.0)
                            }.Show();
                        }));
                        return;
                    }
                }
                AdminPanels.ExitAddSetBd25();
            }
        }

        public void UpdateAlias(string PathFileadd, string NameBd, string PatchSR)
        {
            if (!PathFileadd.ToUpper().Contains(".FDB") && !PathFileadd.ToUpper().Contains(".GDB"))
                return;
            int num = 0;
            int index1 = 0;
            string path = PatchSR + (AdminPanels.Inst2530 == "30" ? "databases.conf" : "aliases.conf");
            foreach (string readAllLine in System.IO.File.ReadAllLines(path))
            {
                if (readAllLine.StartsWith(NameBd))
                    return;
                ++num;
            }
            if (AdminPanels.Inst2530 == "25" && AdminPanels.SetName25.Length == 0)
                AdminPanels.SetName25 = AppStart.TableReestr["NameServer25"];
            if (AdminPanels.Inst2530 == "30" && AdminPanels.SetName30.Length == 0)
                AdminPanels.SetName30 = AppStart.TableReestr["NameServer30"];
            INI.ReadFile(PatchSR + (AdminPanels.Inst2530 == "30" ? "databases.conf" : "aliases.conf"), new INI.Flags?()).Set(NameBd, AdminPanels.PathAddDataServer, true);
            int index2 = 0;
            string[] strArray = System.IO.File.ReadAllLines(path);
            string[] contents = new string[100];
            foreach (string source in strArray)
            {
                if (source.Count<char>() != 0 && (uint)source.Length > 0U)
                {
                    contents[index1] = strArray[index2];
                    ++index1;
                }
                ++index2;
            }
            System.IO.File.WriteAllLines(path, contents);
            AdminPanels.ImageObj = AdminPanels.Inst2530;
            InstallB52.RestartFB25(PatchSR, AdminPanels.Inst2530 == "30" ? AdminPanels.SetName30 : AdminPanels.SetName25);
        }

        public static void ExitAddSetBd25()
        {
            string PathFileadd = PathFileBDText;
            if (!File.Exists(PathFileadd)) PathFileadd = PathFileadd.Substring(0, PathFileadd.IndexOf("."))+ ".gdb";  
            if (File.Exists(PathFileadd))
            {
                if (SetUpdateRestore == 3)
                {
                    File.Copy(OutPath + "security2back.fdb", SetPuth25+"security2.fdb", true);
                    InstallB52.RestartFB25(SetPuth25, SetName25);
                }
                


                if (PathFileadd.ToUpper().Contains(@"C:\BACKUPFDB\") && File.Exists(PathFileadd))
                {
                    File.Copy(PathFileadd, SystemConectoServers.PutchServerData + PathFileBDText.Substring(PathFileBDText.LastIndexOf(@"\") + 1, PathFileBDText.Length - (PathFileBDText.LastIndexOf(@"\") + 1)), true);
                    PathFileBDText = SystemConectoServers.PutchServerData + PathFileBDText.Substring(PathFileBDText.LastIndexOf(@"\") + 1, PathFileBDText.Length - (PathFileBDText.LastIndexOf(@"\") + 1));

                    int Idcount = 0, Idcountout = 0;
                    string NewSelectAlias = (PathFileBDText.Substring(PathFileBDText.LastIndexOf(@"\") + 1, PathFileBDText.LastIndexOf(".") - PathFileBDText.LastIndexOf(@"\") - 1)); ;
                    string[] fileLines = File.ReadAllLines(SetPuth25 + "aliases.conf");
                    foreach (string x in fileLines)
                    {
                        if (x.StartsWith(NewSelectAlias)) NewSelectAlias = NewSelectAlias + Convert.ToString(Idcount);
                        Idcount++;
                    }
                    var GchangeaAliases25 = ConectoWorkSpace.INI.ReadFile(SetPuth25 + "aliases.conf");
                    GchangeaAliases25.Set(NewSelectAlias, PathFileBDText, true);
  
                    fileLines = File.ReadAllLines(SetPuth25 + "aliases.conf");
                    string[] fileoutLines = new string[20];
                    foreach (string x in fileLines)
                    {

                        if (x.Count() != 0 && x.Length != 0)
                        {
                            if ((x.Contains(SelectAlias) == true) || x.Length == 0)
                            {
                                fileLines[Idcount] = String.Empty;
                            }
                            fileoutLines[Idcountout] = fileLines[Idcount];
                           
                            Idcountout++;
                        }
                        Idcount++;
                    }
                    File.WriteAllLines(SetPuth25 + "aliases.conf", fileoutLines);
                    InstallB52.RestartFB25(SetPuth25, SetName25);
                }
                InsertConect(PathFileadd, SetName25);

                if (AppStart.TableReestr["PuthSetBD25"] == "")
                {
                       Administrator.AdminPanels.UpdateKeyReestr("PuthSetBD25", PathFileadd);
                        System.Windows.Application.Current.Dispatcher.Invoke(new Action(delegate ()
                        {
                            ConectoWorkSpace.Administrator.AdminPanels ConectoWorkSpace_Puth = AppStart.LinkMainWindow("WAdminPanels");
                            ConectoWorkSpace_Puth.PuthSetBD25.Text = PathFileadd;
                            ConectoWorkSpace_Puth.DefNameServer25.Text = AppStart.TableReestr["NameServer25"];
                            Interface.CurrentStateInst("SetPuthBD25", "2", "on_off_2.png", ConectoWorkSpace_Puth.SetPuthBD25);
                        }));
                }
            }
 
            System.Windows.Application.Current.Dispatcher.Invoke(new Action(delegate ()
            {
                    ConectoWorkSpace.Administrator.AdminPanels ConectoWorkSpace_Puth = AppStart.LinkMainWindow("WAdminPanels");
                    ConectoWorkSpace_Puth.DirSetBD25.Foreground = Brushes.White;
                    ConectoWorkSpace_Puth.DirSetBD25.Content = "Добавить";
                    ConectoWorkSpace_Puth.TablPuthBD.IsEnabled = true;
                    ConectoWorkSpace_Puth.DirSetBD25.Foreground = Brushes.White;
            }));
 
        }


 

        public static void RunProcess(string CmdRun, string CmdArguments, string NoWindow = "")
        {
 
            Process pr = new Process();
            pr.StartInfo.FileName = CmdRun;
            pr.StartInfo.Arguments = CmdArguments;
            pr.StartInfo.UseShellExecute = false;
            if (NoWindow != "")
            {
                pr.StartInfo.CreateNoWindow = true;
                pr.StartInfo.RedirectStandardOutput = true;
            }

            pr.Start();
            pr.WaitForExit();
            pr.Close();
     
        }


        public static void SprGrid(string CurrentSetBD)
        {
            string ErrMessage = "", Temphub = hubdate;
            List <MySpr> ResultSpr = new List<MySpr>(1);
            var BdB52 = DBConecto.StringServerFB();
      
            try
            {
                FbConnection bdFBSqlConect = new FbConnection(BdB52.ToString());
                bdFBSqlConect.Open();
                FbCommand UpdateKey = new FbCommand(CurrentSetBD, bdFBSqlConect);
                UpdateKey.CommandType = CommandType.Text;
                FbDataReader reader = UpdateKey.ExecuteReader();
                while (reader.Read()) { ResultSpr.Add(new MySpr(reader[0].ToString(), reader[1].ToString())); }
                reader.Close();
                UpdateKey.Dispose();
                bdFBSqlConect.Dispose();
                bdFBSqlConect.Close();
            }
            catch (FirebirdSql.Data.FirebirdClient.FbException ex)
            {
                SystemConecto.ErorDebag(" возникло исключение: " + Environment.NewLine +
                           " === IDCode: " + ex.ErrorCode.ToString() + Environment.NewLine +
                           " === Message: " + ex.Message.ToString() + Environment.NewLine +
                           " === Exception: " + ex.ToString(), 1);
                ErrMessage = "Обнаружена ошибка в БД." + Environment.NewLine + ex.ToString().Substring(ex.ToString().IndexOf(Environment.NewLine) + 2, ex.ToString().LastIndexOf(Environment.NewLine) - ex.ToString().IndexOf("PRC_CLEAR_DATABASE"));
                Temphub = "";
            }

            catch (Exception ex) //)
            {
                SystemConecto.ErorDebag("возникло исключение:" + Environment.NewLine +
                        " возникло исключение: " + Environment.NewLine +
                        " === Message: " + ex.Message.ToString() + Environment.NewLine +
                        " === Exception: " + ex.ToString(), 1);
                ErrMessage = "Обнаружена ошибка в БД." + Environment.NewLine + ex.ToString().Substring(ex.ToString().IndexOf(Environment.NewLine) + 2, ex.ToString().LastIndexOf(Environment.NewLine) - ex.ToString().IndexOf("PRC_CLEAR_DATABASE"));
                Temphub = "";
            }

  
            BdB52 = "";          
            if (Temphub == "")
            {
                System.Windows.Application.Current.Dispatcher.Invoke(new Action(delegate ()
                {

                    MainWindow ConectoWorkSpace_InW = AppStart.LinkMainWindow();
                    Window WinOblakoVerh_Info = new WinMessage(ErrMessage, 0, 0); // создаем AutoClose
                    WinOblakoVerh_Info.Owner = ConectoWorkSpace_InW;
                    WinOblakoVerh_Info.Top = SystemConecto.WorkAreaDisplayDefault[0] + +350; // (WinOblakoVerh_Info.Height - 58)
                    WinOblakoVerh_Info.Left = SystemConecto.WorkAreaDisplayDefault[1] + 650;
                    WinOblakoVerh_Info.ShowActivated = false;
                    WinOblakoVerh_Info.Show();
                }));
                return;
            }
            System.Windows.Application.Current.Dispatcher.Invoke(new Action(delegate ()
            {
                WinSetSpr ConectoWorkSpace_InW = AppStart.LinkMainWindow("WinSetSprW");
                ConectoWorkSpace_InW.TablSpr.ItemsSource = ResultSpr;
 
            }));

        }


        public static void GridSpr()
        {
            System.Windows.Application.Current.Dispatcher.Invoke(new Action(delegate ()
            {

                WinSetSpr ConectoWorkSpace_InW = AppStart.LinkMainWindow("WinSetSprW");
                if (ConectoWorkSpace_InW.TablSpr.SelectedItem != null)
                {
                    MySpr path = ConectoWorkSpace_InW.TablSpr.SelectedItem as MySpr;
                    KodOrgClearDB = path.Kod;
                }
            }));
            ProcCleanBD25Run2();
  


        }

        private void DateArhivBd25_Loaded(object sender, RoutedEventArgs e)
        {
            DateArhivBd30(25);
        }

        private void DateArhivBd30_Loaded(object sender, RoutedEventArgs e)
        {
            DateArhivBd30(30);
        }

        public void DateArhivBd30(int Bd)
        {
            string CurrentSetBD = "", CurrentSelectBD ="";
            if (Bd == 25) CurrentSetBD = "select * from SERVERACTIVFB25 where SERVERACTIVFB25.PUTH = " + "'" + AppStart.TableReestr["ServerDefault25"] + "'";
            if (Bd == 30) CurrentSetBD = "select * from SERVERACTIVFB30 where SERVERACTIVFB30.PUTH = " + "'" + AppStart.TableReestr["ServerDefault30"] + "'";
 
            DBConecto.DBopenFBConectionMemory(SystemBD.ConnnStringSystem().ToString(), "", "FbSystem");
            FbCommand ServerKey = new FbCommand(CurrentSetBD, DBConecto.bdFbSystemConect);
            ServerKey.CommandType = CommandType.Text;
            FbDataReader ReaderName = ServerKey.ExecuteReader();
            while (ReaderName.Read()) { NameSreverArhiv = ReaderName[2].ToString();  }
            CurrentSelectBD = "SELECT * FROM SCHEDULEARHIV WHERE SCHEDULEARHIV.SERVER = " + "'" + NameSreverArhiv + "'";
            List<ScheduleArhiv> ResultArhiv = new List<ScheduleArhiv>(1);
            DBConecto.DBopenFBConectionMemory(SystemBD.ConnnStringSystem().ToString(), "", "FbSystem");
            FbCommand UpdateKey = new FbCommand(CurrentSelectBD, DBConecto.bdFbSystemConect);
            UpdateKey.CommandType = CommandType.Text;
            FbDataReader reader = UpdateKey.ExecuteReader();
            while (reader.Read()) { ResultArhiv.Add(new ScheduleArhiv(reader[0].ToString(), reader[1].ToString(), reader[2].ToString(), reader[3].ToString(), reader[4].ToString())); }
            reader.Close();
            UpdateKey.Dispose();
            DBConecto.bdFbSystemConect.Close();
            if (Bd == 30) TablDateArhivBd.ItemsSource = ResultArhiv; 
            if (Bd == 25) TablDateArhivBd25.ItemsSource = ResultArhiv;
        }


        private void ListGridArhiv(int Bd)
        {
            string CurrentSelectBD = "SELECT * FROM SCHEDULEARHIV WHERE SCHEDULEARHIV.SERVER = " + "'" + NameSreverArhiv + "'";
            List<ScheduleArhiv> ResultArhiv = new List<ScheduleArhiv>(1);
            DBConecto.DBopenFBConectionMemory(SystemBD.ConnnStringSystem().ToString(), "", "FbSystem");
            FbCommand UpdateKey = new FbCommand(CurrentSelectBD, DBConecto.bdFbSystemConect);
            UpdateKey.CommandType = CommandType.Text;
            FbDataReader reader = UpdateKey.ExecuteReader();
            while (reader.Read()) { ResultArhiv.Add(new ScheduleArhiv(reader[0].ToString(), reader[1].ToString(), reader[2].ToString(), reader[3].ToString(), reader[4].ToString())); }
            reader.Close();
            UpdateKey.Dispose();
            DBConecto.bdFbSystemConect.Close();
            if (Bd == 30) TablDateArhivBd.ItemsSource = ResultArhiv;
            if (Bd == 25) TablDateArhivBd25.ItemsSource = ResultArhiv;

        }



        private void DateArhivBd25_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            DateArhivBd(25);
        }

        private void DateArhivBd30_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            DateArhivBd(30);
        }

        private void DateArhivBd(int Fb)
        {

           DBConecto.DBopenFBConectionMemory(SystemBD.ConnnStringSystem().ToString(), "", "FbSystem");
            string InsertExecute = "SELECT count(*) from SCHEDULEARHIV";
            DBConecto.UniQuery CountQuery = new DBConecto.UniQuery(InsertExecute, "FB");
            string CountTable = CountQuery.ExecuteUNIScalar() == null ? "" : CountQuery.ExecuteUNIScalar().ToString();
            if (Convert.ToUInt32(CountTable) != 0)
            {
                if (TablDateArhivBd.SelectedItem != null || TablDateArhivBd25.SelectedItem != null)
                {
                    ScheduleArhiv path = Fb == 25 ? TablDateArhivBd25.SelectedItem as ScheduleArhiv : TablDateArhivBd.SelectedItem as ScheduleArhiv;
                    SchedulePuth = path.Puth;
                    ScheduleArhivp = path.Arhiv;
                    ScheduleSetDay = path.SetDay;
                    ScheduleSetDay = path.SetTime;
                    NameSreverArhiv = path.Server;
                    Delete_Str_Backup.IsEnabled = true;
                    Change_Str_Backup.IsEnabled = true;
                    Delete_Str_Backup30.IsEnabled = true;
                    Change_Str_Backup30.IsEnabled = true;

                }
            }
            DBConecto.DBcloseFBConectionMemory("FbSystem");
        }

        private void grid25_MouseUp(object sender, MouseButtonEventArgs e)
        {
            DBConecto.DBopenFBConectionMemory(SystemBD.ConnnStringSystem().ToString(), "", "FbSystem");
            string InsertExecute = "SELECT count(*) from CONNECTIONBD25";
            DBConecto.UniQuery CountQuery = new DBConecto.UniQuery(InsertExecute, "FB");
            string CountTable = CountQuery.ExecuteUNIScalar() == null ? "" : CountQuery.ExecuteUNIScalar().ToString();

            if (Convert.ToUInt32(CountTable) != 0)
            {
                if (TablPuthBD.SelectedItem != null || TablAlias.SelectedItem != null)
                {
                    MyTable path = TablAlias.SelectedItem != null ? TablAlias.SelectedItem as MyTable : TablPuthBD.SelectedItem as MyTable;
                  
                    SelectPuth = path.Puth;
                    SelectAlias = path.Alias;
                    NameServer25 = path.NameServer;
                    Putch_Satellite = path.Satellite;
                    DeletConect.IsEnabled = true;
                    CleanConect.IsEnabled = true;
                    CleanTrashBD25.IsEnabled = true;
                    ChangeDefault25.IsEnabled = true;
                    CreateBackUpBd25.IsEnabled = true;
                    RestoryBd25.IsEnabled = true;
                    ChangeFbd2530.IsEnabled = true;
                    CreateListArhivBd25.IsEnabled = true;
                    UpgradeBD25.IsEnabled = true;
                    GoNewPeriod.IsEnabled = true;
                    UpdateBD25.IsEnabled = true;
                    OnOffLog.IsEnabled = true;
                    LabelCopyLog.IsEnabled = true;
                    Interface.CurrentStateInst("OnOffLog", "0", "on_off_1.png", OnOffLog);
                    Interface.CurrentStateInst("Satellite", "0", "on_off_1.png", Satellite);
                    if (path.Log == "Stop")Interface.CurrentStateInst("OnOffLog", "2", "on_off_2.png", OnOffLog);
                    if (path.Satellite.Trim() != "") Interface.CurrentStateInst("Satellite", "2", "on_off_2.png", Satellite);
                    DBConecto.DBopenFBConectionMemory(SystemBD.ConnnStringSystem().ToString(), "", "FbSystem");
                    string CurrentSetBD = "select * from SERVERACTIVFB25 where SERVERACTIVFB25.NAME = " + "'" + NameServer25 + "'";
                    FbCommand ServerKey = new FbCommand(CurrentSetBD, DBConecto.bdFbSystemConect);
                    ServerKey.CommandType = CommandType.Text;
                    FbDataReader ReaderName = ServerKey.ExecuteReader();
                    while (ReaderName.Read())
                    {
                            SetPort25 = ReaderName[0].ToString();
                            CurrentSereverPuth = ReaderName[1].ToString();
                            CurrentPasswFb25 = ReaderName[5].ToString();
                    }
                    ReaderName.Close();
                    ServerKey.Dispose();
                }
 
            }
            DBConecto.DBcloseFBConectionMemory("FbSystem");
        }

        public static void Hubgrid25_MouseUp(string InsertExecute)
        {
            DBConecto.DBopenFBConectionMemory(SystemBD.ConnnStringSystem().ToString(), "", "FbSystem");
            DBConecto.UniQuery CountQuery = new DBConecto.UniQuery(InsertExecute, "FB");
            string CountTable = CountQuery.ExecuteUNIScalar() == null ? "" : CountQuery.ExecuteUNIScalar().ToString();
 
            if (Convert.ToUInt32(CountTable) != 0)
            {

                System.Windows.Application.Current.Dispatcher.Invoke(new Action(delegate ()
                {
                    WinSetHub ConectoWorkSpace_InW = AppStart.LinkMainWindow("WinSetHubW");
                    if (ConectoWorkSpace_InW.TablAliasBD.SelectedItem != null)
                    {
                        if (InsertExecute.Contains("25"))
                        {
                            MyTable path = ConectoWorkSpace_InW.TablAliasBD.SelectedItem as MyTable;
                           
                            SelectPuth = path.Puth;
                            SelectAlias = path.Alias;
                            NameServer25 = path.NameServer;
                            AppStart.TableReestr["PuthSetBD25"] = SelectPuth;
                            UpdateKeyReestr("PuthSetBD25", SelectPuth);

                            string CurrentSetBD = "select * from SERVERACTIVFB25 where SERVERACTIVFB25.NAME = " + "'" + NameServer25 + "'";
                            DBConecto.DBopenFBConectionMemory(SystemBD.ConnnStringSystem().ToString(), "", "FbSystem");
                            FbCommand ServerSet = new FbCommand(CurrentSetBD, DBConecto.bdFbSystemConect);
                            ServerSet.CommandType = CommandType.Text;
                            FbDataReader ReaderName = ServerSet.ExecuteReader();
                            while (ReaderName.Read())
                            {
                                SetPort25 = ReaderName[0].ToString();
                                CurrentSereverPuth = ReaderName[1].ToString();
                                CurrentPasswFb25 = ReaderName[5].ToString();
                            }
                            ReaderName.Close();
                            ServerSet.Dispose();

                        }
                        if (InsertExecute.Contains("30"))
                        {
                            MyTable30 path = ConectoWorkSpace_InW.TablAliasBD.SelectedItem as MyTable30;
                           
                            SelectPuth = path.Puth;
                            SelectAlias = path.Alias;
                            NameServer30 = path.NameServer;
                            AppStart.TableReestr["PuthSetBD30"] = SelectPuth;
                            UpdateKeyReestr("PuthSetBD30", SelectPuth);

                            string CurrentSetBD = "select * from SERVERACTIVFB30 where SERVERACTIVFB30.NAME = " + "'" + NameServer30 + "'";
                            DBConecto.DBopenFBConectionMemory(SystemBD.ConnnStringSystem().ToString(), "", "FbSystem");
                            FbCommand ServerSet30 = new FbCommand(CurrentSetBD, DBConecto.bdFbSystemConect);
                            ServerSet30.CommandType = CommandType.Text;
                            FbDataReader ReaderName = ServerSet30.ExecuteReader();
                            while (ReaderName.Read())
                            {
                                SetPort30 = ReaderName[0].ToString();
                                CurrentSereverPuth = ReaderName[1].ToString();
                                CurrentPasswFb30 = ReaderName[5].ToString();
                            }
                            ReaderName.Close();
                            ServerSet30.Dispose();
                        }

 

                    }
                }));

            }
            DBConecto.DBcloseFBConectionMemory("FbSystem");

        }

        private void grid30_MouseUp(object sender, MouseButtonEventArgs e)
        {
            DBConecto.DBopenFBConectionMemory(SystemBD.ConnnStringSystem().ToString(), "", "FbSystem");
            string InsertExecute = "SELECT count(*) from CONNECTIONBD30";
            DBConecto.UniQuery CountQuery = new DBConecto.UniQuery(InsertExecute, "FB");
            string CountTable = CountQuery.ExecuteUNIScalar() == null ? "" : CountQuery.ExecuteUNIScalar().ToString();

            if (Convert.ToUInt32(CountTable) != 0)
            {
                if (TablPuthBD30.SelectedItem != null || TablAlias30.SelectedItem != null)
                {
                    MyTable30 path = TablPuthBD30.SelectedItem != null ? TablPuthBD30.SelectedItem as MyTable30 : TablAlias30.SelectedItem as MyTable30;

                    SelectPuth = path.Puth;
                    SelectAlias = path.Alias;
                    NameServer30 = path.NameServer;
                    Putch_Satellite = path.Satellite;
                    DeletConect30.IsEnabled = true;
                    CleanConect30.IsEnabled = true;
                    CleanTrashBD30.IsEnabled = true;
                    SetDefault30.IsEnabled = true;
                    CreateBackUpBd30.IsEnabled = true;
                    RestoryBd30.IsEnabled = true;
                    CreateListArhivBd30.IsEnabled = true;
                    UpdateBD30.IsEnabled = true;
                    GoNewPeriod30.IsEnabled = true;
                    LabelCopyLog30.IsEnabled = true;

                    string CurrentSetBD = "select * from SERVERACTIVFB30 where SERVERACTIVFB30.NAME = " + "'" + NameServer30 + "'";
                    FbCommand ServerKey = new FbCommand(CurrentSetBD, DBConecto.bdFbSystemConect);
                    ServerKey.CommandType = CommandType.Text;
                    FbDataReader ReaderName = ServerKey.ExecuteReader();
                    while (ReaderName.Read())
                    {
                            SetPort30 = ReaderName[0].ToString();
                            CurrentSereverPuth = ReaderName[1].ToString();
                            CurrentPasswFb30 = ReaderName[5].ToString();
                    }
                    ReaderName.Close();
                    ServerKey.Dispose();

                }


            }
            else  { SelectPuth = ""; SelectAlias = ""; NameServer30 = ""; }
            DBConecto.DBcloseFBConectionMemory("FbSystem");
        }


        // Процедура выбора бд для нового сервера
        private void Path_Fbd25_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (NameObj == "UpgradeBD25")
            {
                var dlg = new System.Windows.Forms.FolderBrowserDialog();
                dlg.Description = "Путь размещения Обновления";
                System.Windows.Forms.DialogResult result = dlg.ShowDialog();
                if (result == System.Windows.Forms.DialogResult.OK)
                {
                    FolderBackPuth = dlg.SelectedPath + (dlg.SelectedPath.LastIndexOf(@"\") <= 2 && dlg.SelectedPath.Length <= 3 ? "" : @"\");
                    TextBox_Fbd25.Text = FolderBackPuth;
                }
                else
                {
                    TextBox_Fbd25.Text = "";
                    SelectNewBD25.Visibility = Visibility.Collapsed;
                    TextBox_Fbd25.Visibility = Visibility.Collapsed;
                    Dir_Fbd25.Visibility = Visibility.Collapsed;
                    LabelPuth.Visibility = Visibility.Collapsed;
                    LabelPuth.Content = "Путь к БД.";
                    UpgradeBD25.Foreground = Brushes.White;

                }

            }
            else
            {
                PathFileBD_Click();
                if ((!PathFileBDText.ToUpper().Contains(".FDB") && !PathFileBDText.ToUpper().Contains(".GDB") && !PathFileBDText.ToUpper().Contains(".FBK"))  || PathFileBDText.Length == 0) //&& !PathFileBDText.Contains(".FDB") && !PathFileBDText.Contains(".gdb"))
                {
                    DirSetBD25.Foreground = Brushes.White;
                    return;
                }
                TextBox_Fbd25.Text = PathFileBDText;

            }
  
        }

        private void DeletConnectBD25_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
 
            DeletConect.Foreground = Brushes.Indigo;
            string StrCreate = "select * from REESTRBACK where REESTRBACK.PUTH = " + "'" + SelectPuth + "'";
            int Idcount = 0;
            DBConecto.DBopenFBConectionMemory(SystemBD.ConnnStringSystem().ToString(), "", "FbSystem");
            FbCommand SelectTable = new FbCommand(StrCreate, DBConecto.bdFbSystemConect);
            FbDataReader ReadOutTable = SelectTable.ExecuteReader();
            while (ReadOutTable.Read()) { Idcount = Idcount + 1; }
            ReadOutTable.Close();
            if (Idcount != 0)
            {
                TablPuthBD.IsEnabled = true;
                var TextWindows = "Установлен БекОфис" + Environment.NewLine + "Удаление невозможно ";
                int AutoClose = 1;
                int MesaggeTop = 350;
                int MessageLeft = 650;
                InstallB52.MessageEnd(TextWindows, AutoClose, MesaggeTop, MessageLeft);
                return;
            }
            SelectTable.Dispose();
            DBConecto.DBcloseFBConectionMemory("FbSystem");

            StrCreate = "select * from REESTRFRONT where REESTRFRONT.PUTH = " + "'" + SelectPuth + "'";
            Idcount = 0;
            DBConecto.DBopenFBConectionMemory(SystemBD.ConnnStringSystem().ToString(), "", "FbSystem");
            FbCommand SelectFront = new FbCommand(StrCreate, DBConecto.bdFbSystemConect);
            FbDataReader ReadOutFront = SelectFront.ExecuteReader();
            while (ReadOutFront.Read()) { Idcount = Idcount + 1; }
            ReadOutFront.Close();
            if (Idcount != 0)
            {
                TablPuthBD.IsEnabled = true;
                var TextWindows = "Установлен ФронтОфис" + Environment.NewLine + "Удаление невозможно ";
                int AutoClose = 1;
                int MesaggeTop = -170;
                int MessageLeft = 800;
                InstallB52.MessageEnd(TextWindows, AutoClose, MesaggeTop, MessageLeft);
                return;
            }
            SelectTable.Dispose();
            DBConecto.DBcloseFBConectionMemory("FbSystem");
 
            StrCreate = "DELETE from CONNECTIONBD25 where CONNECTIONBD25.PUTHBD = " + "'" + SelectPuth + "'";
            DBConecto.DBopenFBConectionMemory(SystemBD.ConnnStringSystem().ToString(), "", "FbSystem");
            DBConecto.UniQuery DeletQuery = new DBConecto.UniQuery(StrCreate, "FB");
            DeletQuery.UserQuery = string.Format(StrCreate, "CONNECTIONBD25");
            DeletQuery.ExecuteUNIScalar();
            Administrator.AdminPanels.Grid_Puth("SELECT * from CONNECTIONBD25 WHERE CONNECTIONBD25.NAMESERVER = " + "'" + NameServer25 + "'");
            string InsertExecute = "SELECT count(*) from CONNECTIONBD25";
            DBConecto.UniQuery CountConect = new DBConecto.UniQuery(InsertExecute, "FB");
            string CountTable = CountConect.ExecuteUNIScalar() == null ? "" : CountConect.ExecuteUNIScalar().ToString();
            if (Convert.ToUInt32(CountTable) == 0)
            {
                Administrator.AdminPanels.UpdateKeyReestr("PuthSetBD25", "");
                System.Windows.Application.Current.Dispatcher.Invoke(new Action(delegate ()
                {
                    ConectoWorkSpace.Administrator.AdminPanels ConectoWorkSpace_InW = AppStart.LinkMainWindow("WAdminPanels");
                    ConectoWorkSpace_InW.PuthSetBD25.Text = "";
                    ConectoWorkSpace_InW.DefNameServer25.Text = "";
                    Interface.CurrentStateInst("SetPuthBD25", "0", "on_off_1.png", ConectoWorkSpace_InW.SetPuthBD25);
                }));
            }
            FalseIsEnabled25();
            //  Удаление строки с выбранным алиасом.
            StrCreate = "select * from SERVERACTIVFB25 where SERVERACTIVFB25.NAME = " + "'" + NameServer25 + "'";
            DeleteStrAlias(StrCreate, AppStart.TableReestr["ServerDefault25"], "aliases.conf");
            DeletConect.Foreground = Brushes.White;
            TablPuthBD.IsEnabled = true;
        }
        
        // Процедура поиска  пути сервера для которого модифицируется aliases.conf и удаления строки с выбранным алиасом.
        public static void DeleteStrAlias(string StrCreate, string DefaultServer, string Conf )
        {
 
            DBConecto.DBopenFBConectionMemory(SystemBD.ConnnStringSystem().ToString(), "", "FbSystem");
            string ServerPuth = "";
            int Idcount = 0;
            FbCommand SelectTable = new FbCommand(StrCreate, DBConecto.bdFbSystemConect);
            SelectTable.CommandType = CommandType.Text;
            FbDataReader ReadTable = SelectTable.ExecuteReader();
            while (ReadTable.Read())
            {
                ServerPuth = ReadTable[1].ToString();
                Idcount = Idcount + 1;
            }
            ReadTable.Close();
            SelectTable.Dispose();
            DBConecto.DBcloseFBConectionMemory("FbSystem");
           if (Idcount == 0) ServerPuth = DefaultServer;
            // Удаление строки из файла
            Idcount = 0;
            string[] fileLines = File.ReadAllLines(ServerPuth + Conf);
            foreach (string x in fileLines)
            {

                if ((x.Contains(SelectPuth) == true && x.Contains(SelectAlias) == true) || x.Length ==0)
                {
                    fileLines[Idcount] = String.Empty;
                }
                Idcount++;

            }
            File.WriteAllLines(ServerPuth + Conf, fileLines);

        }

        private void CleanTrashBD25_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

            this.CleanTrashBD25.Foreground = (Brush)Brushes.Indigo;
            AdminPanels.NameObj = "CleanTrashBD25";
            AdminPanels.hubdate = "25";
            if (!System.IO.File.Exists(AdminPanels.SelectPuth))
            {
                AdminPanels.NameObj = "";
                InstallB52.MessageErorInst("По указанному пути отсутствует БД " + Environment.NewLine + "Путь: " + Environment.NewLine + AdminPanels.SelectPuth);
                this.CleanTrashBD25.Foreground = (Brush)Brushes.White;
                this.TablPuthBD.IsEnabled = true;
            }
            else
            {
                this.FalseIsEnabled25();
                AppStart.RenderInfo renderInfo = new AppStart.RenderInfo();
                renderInfo.argument1 = AdminPanels.CurrentPasswFb25;
                renderInfo.argument2 = AdminPanels.SelectAlias.Trim();
                renderInfo.argument3 = AdminPanels.SelectPuth;
                renderInfo.argument4 = AdminPanels.SetPort25;
                renderInfo.argument5 = "CleanMusor";
                Thread thread = new Thread(new System.Threading.ParameterizedThreadStart(InstallB52.CleanTrashBD));
                thread.SetApartmentState(ApartmentState.STA);
                thread.IsBackground = true;
                thread.Start((object)renderInfo);
                ++InstallB52.IntThreadStart;
                WaitMessage waitMessage = new WaitMessage();
                waitMessage.Owner = (Window)this;
                waitMessage.Top = (double)SystemConecto.WorkAreaDisplayDefault[0] - 500.0;
                waitMessage.Left = (double)SystemConecto.WorkAreaDisplayDefault[1] - 300.0;
                waitMessage.Show();
            }

        }

        private void CleanTrashBD30_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

            this.CleanTrashBD30.Foreground = (Brush)Brushes.Indigo;
            AdminPanels.NameObj = "CleanTrashBD30";
            AdminPanels.hubdate = "30";
            if (!System.IO.File.Exists(AdminPanels.SelectPuth))
            {
                AdminPanels.NameObj = "";
                InstallB52.MessageErorInst("По указанному пути отсутствует БД " + Environment.NewLine + "Путь: " + Environment.NewLine + AdminPanels.SelectPuth);
                this.CleanTrashBD30.Foreground = (Brush)Brushes.White;
                this.TablPuthBD30.IsEnabled = true;
            }
            else
            {
                this.FalseIsEnabled30();
                AppStart.RenderInfo renderInfo = new AppStart.RenderInfo();
                renderInfo.argument1 = AdminPanels.CurrentPasswFb30;
                renderInfo.argument2 = AdminPanels.SelectAlias.Trim();
                renderInfo.argument3 = AdminPanels.SelectPuth;
                renderInfo.argument4 = AdminPanels.SetPort30;
                Thread thread = new Thread(new System.Threading.ParameterizedThreadStart(InstallB52.CleanTrashBD));
                thread.SetApartmentState(ApartmentState.STA);
                thread.IsBackground = true;
                thread.Start((object)renderInfo);
                ++InstallB52.IntThreadStart;
                WaitMessage waitMessage = new WaitMessage();
                waitMessage.Owner = (Window)this;
                waitMessage.Top = (double)SystemConecto.WorkAreaDisplayDefault[0] - 500.0;
                waitMessage.Left = (double)SystemConecto.WorkAreaDisplayDefault[1] - 300.0;
                waitMessage.Show();
            }

        }



        private void CleanConect25_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            CleanConect.Foreground = Brushes.Indigo;
            NameObj = "CleanConect25";
            hubdate = "25";
            ModifiConect("CleanConect25");

        }


        private void CleanConect30_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

            CleanConect.Foreground = Brushes.Indigo;
            NameObj = "CleanConect30";
            hubdate = "30";
            ModifiConect("CleanConect30");

        }


        private void datePicker25_CopyLogS_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            hubdate = "25";
            datepikerS = datePicker25_CopyLogS.SelectedDate.Value;
        }

        private void datePicker25_CopyLogP_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            hubdate = "25";
            datepikerP = datePicker25_CopyLogP.SelectedDate.Value;
        }


        private void datePicker30_CopyLogS_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            hubdate = "30";
            datepikerS = datePicker30_CopyLogS.SelectedDate.Value;
        }

        private void datePicker30_CopyLogP_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            hubdate = "30";
            datepikerP = datePicker30_CopyLogP.SelectedDate.Value;
        }



        private void datePicker25_Server_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            hubdate = "25";
            datepiker = datePicker25_Server.SelectedDate.Value;
            FalseIsEnabled25();
        }

        private void datePicker30_Server_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            hubdate = "30";
            datepiker = datePicker30_Server.SelectedDate.Value;
            FalseIsEnabled30();
        }

        public void SetConectFb()
        {
            DateTime dateClean = datepiker;
            string DateClean_SqlFB = string.Format("{0}.{1}.{2}", dateClean.Day, dateClean.Month, dateClean.Year);
            string DateClean_Server = hubdate == "30" ? "DateClean_Fbd30" : "DateClean_Fbd25";
            Administrator.AdminPanels.UpdateKeyReestr(DateClean_Server, DateClean_SqlFB);

            string PuthSrv = hubdate == "30" ? SetPort30 : SetPort25;
            DBConecto.ParamStringServerFB[1] = "SYSDBA";
            DBConecto.ParamStringServerFB[2] = hubdate == "25" ? CurrentPasswFb25 : CurrentPasswFb30;
            DBConecto.ParamStringServerFB[3] = "localhost";
            DBConecto.ParamStringServerFB[4] = AppStart.TableReestr["SetTextIp4BackOf"] + "/" + PuthSrv + ":" + SelectAlias.Trim();
            DBConecto.ParamStringServerFB[5] = SelectPuth;
            DBConecto.ParamStringServerFB[6] = FbCharset.Windows1251.ToString();
            DBConecto.ParamStringServerFB[7] = PuthSrv;
        }

        private void FalseIsEnabled25()
        {
            DeletConect.IsEnabled = false;
            CleanTrashBD25.IsEnabled = false;
            ChangeDefault25.IsEnabled = false;
            SetChangePassw.IsEnabled = false;
            StoptServFB25.IsEnabled = false;
            NewSecurity.IsEnabled  = false;
            UpgradeBD25.IsEnabled = false;
            TablPuthBD.IsEnabled = false;
            CleanConect.IsEnabled = false;
        }


        private void FalseIsEnabled30()
        {
            DeletSrever30.IsEnabled = false;
            RestartServFB30.IsEnabled = false;
            SetDefaultServer30.IsEnabled = false;
            SetChangePassw30.IsEnabled = false;
            StoptServFB30.IsEnabled = false;
            UpdateBD30.IsEnabled = false;
            GoNewPeriod30.IsEnabled = false;
        }

        // 1.2. Fbd30

        private void Servergrid30_Loaded(object sender, RoutedEventArgs e)
        {
            CreateServerActivFB30();
            SetServerGrid("SELECT * from SERVERACTIVFB30");
        }

        private void Servergrid30_MouseUp(object sender, MouseButtonEventArgs e)
        {
            DBConecto.DBopenFBConectionMemory(SystemBD.ConnnStringSystem().ToString(), "", "FbSystem");
            string InsertExecute = "SELECT count(*) from SERVERACTIVFB30";
            DBConecto.UniQuery CountQuery = new DBConecto.UniQuery(InsertExecute, "FB");
            string CountTable = CountQuery.ExecuteUNIScalar() == null ? "" : CountQuery.ExecuteUNIScalar().ToString();

            if (Convert.ToUInt32(CountTable) != 0)
            {
                if (TablPuthServer30.SelectedItem != null)
                {
                    ServerTable30 path = TablPuthServer30.SelectedItem as ServerTable30;
                    SetPort30 = Convert.ToString(path.Port);
                    SetPuth30 = path.Puth;
                    SetName30 = path.Name;
                    SetActiv30 = path.State;
                    CurrentPasswFb30 = path.CurrentPassw;
                    DeletSrever30.IsEnabled = true;
                    RestartServFB30.IsEnabled = true;
                    SetDefaultServer30.IsEnabled = true;
                    StoptServFB30.IsEnabled = true;
                    SetChangePassw30.IsEnabled = true;
                    CaptionConfServ30.IsEnabled = true;
                    CaptionAliasBD30.IsEnabled = true;
                    NewSecurity30.IsEnabled = true;
                    ClickMouseUp_Servergrid25 = true;
                    CountConect25("SELECT count(*) from CONNECTIONBD30 WHERE CONNECTIONBD30.NAMESERVER =" + "'" + SetName30 + "'");
                    Grid_Puth("SELECT * from CONNECTIONBD30 WHERE CONNECTIONBD30.NAMESERVER =" + "'" + SetName30 + "'");
                   
                }
            }
            DBConecto.DBcloseFBConectionMemory("FbSystem");
        }

        // Процедура удаления серевера из списка активных доступных серверов
        private void DeletSrever30_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            ConectoWorkSpace.Administrator.AdminPanels ConectoWorkSpace_InW = AppStart.LinkMainWindow("WAdminPanels");
            if (ConectoWorkSpace_InW == null) return;
            IsEnaledServer30();
            Administrator.AdminPanels.ImageObj = "StoptServFB30";
            string TextMesage = "Вы действительно хотите удалить сервер?";
            Inst2530 = "del30";
            DeleteDefaultServer = 0;
            WinOblakoSetUpdate OblakoSetWindow = new WinOblakoSetUpdate(TextMesage);
            OblakoSetWindow.Owner = ConectoWorkSpace_InW;
            OblakoSetWindow.Top = ConectoWorkSpace_InW.Top + 100;
            OblakoSetWindow.Left = ConectoWorkSpace_InW.ScrollViewerd.Width - (OblakoSetWindow.Width * 2) - 100;
            OblakoSetWindow.Show();

        }
        // Процедура добавления к списку нового активного сервера
        private void AddServer30_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            ConectoWorkSpace.Administrator.AdminPanels ConectoWorkSpace_InW = AppStart.LinkMainWindow("WAdminPanels");
            if (ConectoWorkSpace_InW == null) return;
            CallProgram = "AddServer30";
            string TextMesage = "Вы действительно хотите установить новый сервер?";
            Inst2530 = "30";
            SetUpdateRestore = 0;
            WinOblakoSetUpdate OblakoSetWindow = new WinOblakoSetUpdate(TextMesage);
            OblakoSetWindow.Owner = ConectoWorkSpace_InW;
            OblakoSetWindow.Top = ConectoWorkSpace_InW.Top + 150;
            OblakoSetWindow.Left = ConectoWorkSpace_InW.ScrollViewerd.Width - (OblakoSetWindow.Width * 2) - 100;
            OblakoSetWindow.Show();

        }

        // Процедура контроля количества установленых серверов не больше 5
        public static string ContlServer(string SetRun)
        {
            string ReturnSetNumber = "1";
            SetPort30 = "3056"; SetPuth30 = SystemConectoServers.PutchServer + @"Firebird_3_0\"; SetName30 = "ConectoWS_3"; SetNumber30 =1;
            DBConecto.DBopenFBConectionMemory(SystemBD.ConnnStringSystem().ToString(), "", "FbSystem");
            int IndStroka = 1;
            FbCommand UpdateKey = new FbCommand("SELECT * from SERVERACTIVFB30", DBConecto.bdFbSystemConect);
            UpdateKey.CommandTimeout = 3;
            UpdateKey.CommandType = CommandType.Text;
            FbDataReader reader = UpdateKey.ExecuteReader();
            while (reader.Read())
            {
 
                SetPort30 = reader[0].ToString();
                SetPuth30 = reader[1].ToString();
                SetName30 = reader[2].ToString();
                IndStroka++;
            }
            reader.Close();
            UpdateKey.Dispose();
            DBConecto.DBcloseFBConectionMemory("FbSystem");
            if (IndStroka >= 6)
            {
                string TextWind = "Количество установленных серверов превышает 5" + Environment.NewLine + "Удалите ненужные записи из таблицы и повторите установку ";
                int AutoCls = 1;
                int MesaggeTp = 350;
                int MessageLf = 650;
                InstallB52.MessageErr(TextWind, AutoCls, MesaggeTp, MessageLf);
                if (SetRun != "AddServer30")
                {
                    System.Windows.Application.Current.Dispatcher.Invoke(new Action(delegate ()
                    {
                        ConectoWorkSpace.Administrator.AdminPanels ConectoWorkSpace_ERR = AppStart.LinkMainWindow("WAdminPanels");
                        Interface.CurrentStateInst("ServFB30OnOff", "0", "on_off_1.png", ConectoWorkSpace_ERR.ServFB30OnOff);
                    }));
                }
                SetNumber30 = 0;
            }
            ReturnSetNumber = Convert.ToString(IndStroka);
            return ReturnSetNumber;
        }
        


        // Процедура назначения сервера по умлочанию.
        private void SetDefaultServer30_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            string PatchSR = SetPuth30;
            if (SetActiv30 == "Stop")
            {
                var TextWindows = "Не активный сервер нельзя использовать по умолчанию " + Environment.NewLine + "Выполнение процедуры невозможно. ";
                int AutoClose = 1;
                int MesaggeTop = -80;
                int MessageLeft = 650;
                InstallB52.MessageEnd(TextWindows, AutoClose, MesaggeTop, MessageLeft);
                return;
            }
            if (!SystemConecto.File_(PatchSR + "instsvc.exe", 5))
            {
                var TextWindows = "Отсутствует файл instsvc.exe." + Environment.NewLine + "Изменение пути невозможно ";
                int AutoClose = 1;
                int MesaggeTop = 3;
                int MessageLeft = 650;
                InstallB52.MessageEnd(TextWindows, AutoClose, MesaggeTop, MessageLeft);
                return;
            }
            System.Windows.Application.Current.Dispatcher.Invoke(new Action(delegate ()
            {
                ConectoWorkSpace.Administrator.AdminPanels ConectoWorkSpace_InW = AppStart.LinkMainWindow("WAdminPanels");
                ConectoWorkSpace_InW.PuthSetServer30.Text = PatchSR;
                ConectoWorkSpace_InW.PuthSetBD30.Text = "";
                Interface.CurrentStateInst("SetPuthBD30", "0", "on_off_1.png", ConectoWorkSpace_InW.SetPuthBD30);
            }));
            UpdateKeyReestr("ServerDefault30", SetPuth30);
            UpdateKeyReestr("NameServer30", SetName30);
            UpdateKeyReestr("PuthSetBD30", "");

            // Перегрузить таблицу алиасов
            LoadAlias30(AppStart.TableReestr["NameServer30"], AppStart.TableReestr["ServerDefault30"]);
            Grid_Puth("SELECT * from CONNECTIONBD30 WHERE CONNECTIONBD30.NAMESERVER = " + "'" + SetName30 + "'");
        }

  
        private void CleanBD30_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            hubdate = "30";
            // Проверка наличия свободного пространства на диске куда будем ложить 
            DriveInfo di = new DriveInfo(@"C:\");
            long Ffree = (di.TotalFreeSpace / 1024) / 1024;
            string MBFree = Ffree.ToString("#,##") + " MB";
            if (Ffree - 2500 < 0)
            {
                Administrator.AdminPanels.NameObj = "";
                string TextWindows = "Для очистки БД необходимо свободное пространство 5Гб на диске С:." + Environment.NewLine + "Текущее пространство " + MBFree + Environment.NewLine + "Выполнение процедуры остановлено. ";
                InstallB52.MessageErorInst(TextWindows);
                return;
            }

            if (NameServer30 == "")
            {
                var TextWindows = "Неопределен путь к БД. Выполнение процедуры остановлено.";
                int AutoClose = 1;
                int MesaggeTop = -470;
                int MessageLeft = 930;
                InstallB52.MessageEnd(TextWindows, AutoClose, MesaggeTop, MessageLeft);
                return;
            }

            datePicker30_Server.Visibility = Visibility.Visible;

        }


  
        private void datePicker30_Clean_LostFocus(object sender, RoutedEventArgs e)
        {
            FalseIsEnabled30();
        }

        private void DirSetBD30_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

            if (DirSetBD30.Foreground == Brushes.Indigo)
            {

                DirSetBD30.Foreground = Brushes.White;
                TablPuthBD30.IsEnabled = true;
                return;
            }
            if (ClickMouseUp_Servergrid25 == false)
            {

                if (CountServer25 > 1)
                {
                    TablPuthBD30.IsEnabled = true;
                    var TextWindows = "Не выбран серевер БД." + Environment.NewLine + "Процедура прекращена ";
                    int AutoClose = 1;
                    int MesaggeTop = -170;
                    int MessageLeft = 800;
                    InstallB52.MessageEnd(TextWindows, AutoClose, MesaggeTop, MessageLeft);
                    return;
                }
                else
                {
                    SetPort30 = PortServer30[0];
                    SetPuth30 = NameExeFile30[0];
                    SetName30 = ServerName30[0];
                    CurrentPasswFb30 = CurrentPassw30[0];
                }

            }
            if (!File.Exists(SetPuth30 + @"gbak.exe"))
            {
                TablPuthBD.IsEnabled = true;
                var TextWindows = "Не выбран серевер БД." + Environment.NewLine + "Процедура прекращена ";
                int AutoClose = 1;
                int MesaggeTop = -170;
                int MessageLeft = 800;
                InstallB52.MessageEnd(TextWindows, AutoClose, MesaggeTop, MessageLeft);
                return;
            }
            FalseIsEnabled30();
            DirSetBD30.Foreground = Brushes.Indigo;

            PathFileBD_Click();
            if ((!PathFileBDText.ToUpper().Contains(".FDB") && !PathFileBDText.ToUpper().Contains(".GDB") && !PathFileBDText.ToUpper().Contains(".FBK")
                && !PathFileBDText.ToUpper().Contains(".ZIP") && !PathFileBDText.ToUpper().Contains(".RAR") && !PathFileBDText.ToUpper().Contains(".7Z")) || PathFileBDText.Length == 0)
            {
                DirSetBD30.Foreground = Brushes.White;
                TablPuthBD30.IsEnabled = true;
            }
            else
            {
                if (!System.IO.File.Exists("c:\\Program Files (x86)\\7-Zip\\7z.exe") && !System.IO.File.Exists((string)SystemConecto.PutchApp + "Utils\\7za\\7za.exe"))
                {
                    AdminPanels.Arh7Zip_return = "";
                    AdminPanels.Arh7Zip();
                    if (AdminPanels.Arh7Zip_return != "")
                        return;
                    for (int index = 0; index <= 1000; ++index)
                    {
                        Thread.Sleep(5000);
                        if (System.IO.File.Exists("c:\\Program Files (x86)\\7-Zip\\7z.exe"))
                            break;
                    }
                }
                this.AddSetBd25();
            }

        }

        public void AddSetBd30()
        {
 
            string PatchSR = SetPuth30; 
            if (!SystemConecto.File_(PatchSR + @"instsvc.exe", 5))
            {
                var TextWindows = "Отсутствует файл instsvc.exe." + Environment.NewLine + "Процедура прекращена ";
                int AutoClose = 1;
                int MesaggeTop = -170;
                int MessageLeft = 800;
                InstallB52.MessageEnd(TextWindows, AutoClose, MesaggeTop, MessageLeft);
                DirSetBD25.Foreground = Brushes.White;
                TablPuthBD.IsEnabled = true;
                return;
            }

            string PathFileadd = PathFileBDText;
            PathFileBDText = (PathFileBDText.ToUpper().Contains(".FBK") || PathFileBDText.ToUpper().Contains(".RAR") ||
            PathFileBDText.ToUpper().Contains(".ZIP") || PathFileBDText.ToUpper().Contains(".7Z")) ? PathFileBDText.Substring(0, PathFileBDText.LastIndexOf(".")) + ".fdb" : PathFileBDText;
            SelectAlias = (PathFileBDText.Substring(PathFileBDText.LastIndexOf(@"\") + 1, PathFileBDText.LastIndexOf(".") - PathFileBDText.LastIndexOf(@"\") - 1));

            int Idcount = 0, Idcountout = 0;
            string StrCreate = "select * from CONNECTIONBD30 where CONNECTIONBD30.PUTHBD = " + "'" + PathFileBDText + "'";
            DBConecto.DBopenFBConectionMemory(SystemBD.ConnnStringSystem().ToString(), "", "FbSystem");
            FbCommand SelectTable = new FbCommand(StrCreate, DBConecto.bdFbSystemConect);
            FbDataReader ReadOutTable = SelectTable.ExecuteReader();
            while (ReadOutTable.Read()) { Idcount++; }
            ReadOutTable.Close();
            if (Idcount != 0)
            {
                TablPuthBD.IsEnabled = true;
                DirSetBD25.Foreground = Brushes.White;
                var TextWindows = "Выбранная БД" + Environment.NewLine + "Ранее добавлена ";
                int AutoClose = 1;
                int MesaggeTop = -170;
                int MessageLeft = 800;
                InstallB52.MessageEnd(TextWindows, AutoClose, MesaggeTop, MessageLeft);
                return;
            }
            SelectTable.Dispose();
            DBConecto.DBcloseFBConectionMemory("FbSystem");

            Idcount = 0;
            string[] fileLines = File.ReadAllLines(PatchSR + "databases.conf");
            foreach (string x in fileLines)
            {
                if (x.StartsWith(Administrator.AdminPanels.SelectAlias)) Administrator.AdminPanels.SelectAlias = Administrator.AdminPanels.SelectAlias + Convert.ToString(Idcount);
                Idcount++;
            }
            if (SetName30.Length == 0) SetName30 = AppStart.TableReestr["NameServer30"];

            var GchangeaAliases30 = ConectoWorkSpace.INI.ReadFile(PatchSR + "databases.conf");
            GchangeaAliases30.Set(Administrator.AdminPanels.SelectAlias, PathFileBDText, true);
            Idcount = 0; Idcountout = 0;
            fileLines = File.ReadAllLines(PatchSR + "databases.conf");
            string[] fileoutLines = new string[100];
            foreach (string x in fileLines)
            {
                if (x.Count() != 0 && x.Length != 0)
                {
                    fileoutLines[Idcountout] = fileLines[Idcount];
                    Idcountout++;
                }
                Idcount++;
            }
            File.WriteAllLines(PatchSR + "databases.conf", fileoutLines);
            InstallB52.RestartFB25(PatchSR, SetName30);
            string IndexContains = PathFileadd.ToUpper().Contains(".7Z") ? ".7" : "";
            if (IndexContains == "") IndexContains = PathFileadd.ToUpper().Contains(".ZIP") ? ".ZIP" : "";
            if (IndexContains == "") IndexContains = PathFileadd.ToUpper().Contains(".RAR") ? ".RAR" : "";
            OutPath = PathFileadd.Substring(0, PathFileadd.ToUpper().IndexOf(IndexContains)) + @"\";
            RunGbak = SetPuth30 + @"gbak.exe";

            if (PathFileadd.ToUpper().Contains(".FBK"))
            {
                hubdate = AppStart.TableReestr["SetTextIp4BackOf"] + "/" + SetPort30 + ":" + SelectAlias.Trim();
                
                ArgumentCmd = " -c " + PathFileadd + " " + hubdate + @" -v  -bu 200 -user sysdba -pass " + CurrentPasswFb30; // + @" -y c:\temp\log.txt;
                SystemConecto.ErorDebag(Administrator.AdminPanels.RunGbak + Administrator.AdminPanels.ArgumentCmd, 1, 2);
                RunProcess(RunGbak, ArgumentCmd);
            }

            if (PathFileadd.ToUpper().Contains(".7Z") || PathFileadd.ToUpper().Contains(".RAR") || PathFileadd.ToUpper().Contains(".ZIP"))
            {


                if (PathFileadd.ToUpper().Contains(".7Z") || PathFileadd.ToUpper().Contains(".ZIP"))
                {
                    string CmdArguments = "  e  \"" + PathFileadd + "\" -o\"" + OutPath + "\" -y";
                    string Run7z = SystemConecto.PutchApp + @"Utils\7za\" + "7za.exe";
                    SystemConecto.ErorDebag(Run7z + CmdArguments, 1, 2);
                    RunProcess(Run7z, CmdArguments);

                    string NameFbk = OutPath + PathFileadd.Substring(PathFileadd.LastIndexOf(@"\") + 1, (PathFileadd.LastIndexOf(".") - (PathFileadd.LastIndexOf(@"\") + 1))) + ".fbk";
                    if (File.Exists(NameFbk))
                    {
                        hubdate = AppStart.TableReestr["SetTextIp4BackOf"] + "/" + SetPort30 + ":" + SelectAlias.Trim();
                        ArgumentCmd = " -c " + NameFbk + " " + hubdate + @" -v  -bu 200 -user sysdba -pass " + CurrentPasswFb30; // + @" -y c:\temp\log.txt;
                        SystemConecto.ErorDebag(Administrator.AdminPanels.RunGbak + Administrator.AdminPanels.ArgumentCmd, 1, 2);
                        RunProcess(RunGbak, ArgumentCmd);
                    }
                    if (!File.Exists(PathFileBDText))
                    {
                        string FileLog = "gbak"+ DateTime.Now.ToString("yyyyMMddHHmmss")+".log";
                        hubdate = AppStart.TableReestr["SetTextIp4BackOf"] + "/" + SetPort30 + ":" + SelectAlias.Trim();
                        ArgumentCmd = " -c " + NameFbk + " " + hubdate + " -v  -y \""+ SystemConecto.PutchApp+FileLog  + "\" -bu 200 -user sysdba -pass " + CurrentPasswFb30; // + @" -y c:\temp\log.txt;
                        SystemConecto.ErorDebag(Administrator.AdminPanels.RunGbak + Administrator.AdminPanels.ArgumentCmd, 1, 2);
                        RunProcess(RunGbak, ArgumentCmd, "1");
                        TablPuthBD.IsEnabled = true;
                        DirSetBD25.Foreground = Brushes.White;
                        var TextWindows = "Распаковка не выполнена." + Environment.NewLine + "Протокол смотри в gbak.log ";
                        int AutoClose = 1;
                        int MesaggeTop = -170;
                        int MessageLeft = 800;
                        InstallB52.MessageEnd(TextWindows, AutoClose, MesaggeTop, MessageLeft);
                        return;
                    }
                }

                if (PathFileadd.ToUpper().Contains(".RAR"))
                {
                    string RarExe = "";
                    if (File.Exists(@"c:\Program Files\WinRAR\rar.exe")) RarExe = @"c:\Program Files\WinRAR\rar.exe";
                    if (File.Exists(@"c:\Program Files (x86)\WinRAR\rar.exe")) RarExe = @"c:\Program Files (x86)\WinRAR\rar.exe";
                    if (RarExe != "")
                    {
                        string CmdArguments = "  x  \"" + PathFileadd + "\" \"" + OutPath + "\""; //
                        RunProcess(RarExe, CmdArguments);
                    }
                }



                if (File.Exists(OutPath + "security3back.fdb"))
                {
                    Administrator.AdminPanels.Inst2530 = "security3";
                    string TextMesage = "Обновить из архива security3.fdb? ";
                    System.Windows.Application.Current.Dispatcher.Invoke(new Action(delegate ()
                    {
                        ConectoWorkSpace.Administrator.AdminPanels ConectoWorkSpace_InW = AppStart.LinkMainWindow("WAdminPanels");
                        WinSetPuth SetPuth = new WinSetPuth(TextMesage);
                        SetPuth.Owner = ConectoWorkSpace_InW;
                        SetPuth.Top = (ConectoWorkSpace_InW.Top + 7) + ConectoWorkSpace_InW.Close_F.Margin.Top + (ConectoWorkSpace_InW.Close_F.Height - 2) + 570;
                        SetPuth.Left = (ConectoWorkSpace_InW.Left) + ConectoWorkSpace_InW.Close_F.Margin.Left + 700;
                        SetPuth.Show();


                    }));
                    return;
                }


            }
            ExitAddSetBd30();
        }

        public static void ExitAddSetBd30()
        {


            if (File.Exists(PathFileBDText))
            {
                if (SetUpdateRestore == 3)
                {
                    File.Copy(OutPath + "security3back.fdb", SetPuth30 + "security3.fdb", true);
                    InstallB52.RestartFB25(SetPuth30, SetName30);
                }
                string PathFileadd = PathFileBDText;
                if (!File.Exists(PathFileadd)) PathFileadd = PathFileadd.Substring(0, PathFileadd.IndexOf(".")) + ".gdb";

                if (PathFileadd.ToUpper().Contains(@"C:\BACKUPFDB\") && File.Exists(PathFileadd))
                {
                    File.Copy(PathFileadd, SystemConectoServers.PutchServerData + PathFileBDText.Substring(PathFileBDText.LastIndexOf(@"\") + 1, PathFileBDText.Length - (PathFileBDText.LastIndexOf(@"\") + 1)), true);
                    PathFileBDText = SystemConectoServers.PutchServerData + PathFileBDText.Substring(PathFileBDText.LastIndexOf(@"\") + 1, PathFileBDText.Length - (PathFileBDText.LastIndexOf(@"\") + 1));

                    int Idcount = 0, Idcountout = 0;
                    string NewSelectAlias = (PathFileBDText.Substring(PathFileBDText.LastIndexOf(@"\") + 1, PathFileBDText.LastIndexOf(".") - PathFileBDText.LastIndexOf(@"\") - 1)); ;
                    string[] fileLines = File.ReadAllLines(SetPuth30 + "databases.conf");
                    foreach (string x in fileLines)
                    {
                        if (x.StartsWith(NewSelectAlias)) NewSelectAlias = NewSelectAlias + Convert.ToString(Idcount);
                        Idcount++;
                    }
                    var GchangeaAliases30 = ConectoWorkSpace.INI.ReadFile(SetPuth30 + "databases.conf");
                    GchangeaAliases30.Set(NewSelectAlias, PathFileBDText, true);

                    fileLines = File.ReadAllLines(SetPuth25 + "databases.conf");
                    string[] fileoutLines = new string[20];
                    foreach (string x in fileLines)
                    {

                        if (x.Count() != 0 && x.Length != 0)
                        {
                            if ((x.Contains(SelectAlias) == true) || x.Length == 0)
                            {
                                fileLines[Idcount] = String.Empty;
                            }
                            fileoutLines[Idcountout] = fileLines[Idcount];

                            Idcountout++;
                        }
                        Idcount++;
                    }
                    File.WriteAllLines(SetPuth30 + "databases.conf", fileoutLines);
                    InstallB52.RestartFB25(SetPuth30, SetName30);
                }
                InsertConect30(PathFileBDText, SetName30);

                if (AppStart.TableReestr["PuthSetBD30"] == "")
                {
                    Administrator.AdminPanels.UpdateKeyReestr("PuthSetBD30", PathFileBDText);
                    System.Windows.Application.Current.Dispatcher.Invoke(new Action(delegate ()
                    {
                        ConectoWorkSpace.Administrator.AdminPanels ConectoWorkSpace_Puth = AppStart.LinkMainWindow("WAdminPanels");
                        ConectoWorkSpace_Puth.PuthSetBD30.Text = PathFileBDText;
                        ConectoWorkSpace_Puth.DefNameServer30.Text = AppStart.TableReestr["NameServer30"];
                        Interface.CurrentStateInst("SetPuthBD30", "2", "on_off_2.png", ConectoWorkSpace_Puth.SetPuthBD30);
                    }));
                }
            }

            System.Windows.Application.Current.Dispatcher.Invoke(new Action(delegate ()
            {
                ConectoWorkSpace.Administrator.AdminPanels ConectoWorkSpace_Puth = AppStart.LinkMainWindow("WAdminPanels");
                ConectoWorkSpace_Puth.DirSetBD30.Foreground = Brushes.White;
                ConectoWorkSpace_Puth.DirSetBD30.Content = "Добавить";
                ConectoWorkSpace_Puth.TablPuthBD30.IsEnabled = true;
            }));


        }






        private void DeletConnectBD30_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            string StrCreate = "select * from REESTRBACK where REESTRBACK.PUTH = " + "'" + SelectPuth + "'";
            string TempNameBack = "";
            int Idcount = 0;
            DBConecto.DBopenFBConectionMemory(SystemBD.ConnnStringSystem().ToString(), "", "FbSystem");
            FbCommand SelectTable = new FbCommand(StrCreate, DBConecto.bdFbSystemConect);
            FbDataReader ReadOutTable = SelectTable.ExecuteReader();
            while (ReadOutTable.Read())
            {
                TempNameBack = ReadOutTable[2].ToString();
                Idcount = Idcount + 1;
            }
            ReadOutTable.Close();
            if (Idcount != 0)
            {
                var TextWindows = "Установлен БекОфис" + Environment.NewLine + "Удаление невозможно ";
                int AutoClose = 1;
                int MesaggeTop = 350;
                int MessageLeft = 650;
                InstallB52.MessageEnd(TextWindows, AutoClose, MesaggeTop, MessageLeft);
                return;
            }
            SelectTable.Dispose();
            DBConecto.DBcloseFBConectionMemory("FbSystem");

            StrCreate = "select * from REESTRFRONT where REESTRFRONT.PUTH = " + "'" + SelectPuth + "'";
            TempNameBack = "";
            Idcount = 0;
            DBConecto.DBopenFBConectionMemory(SystemBD.ConnnStringSystem().ToString(), "", "FbSystem");
            FbCommand SelectFront = new FbCommand(StrCreate, DBConecto.bdFbSystemConect);
            FbDataReader ReadOutFront = SelectFront.ExecuteReader();
            while (ReadOutFront.Read())
            {
                TempNameBack = ReadOutFront[2].ToString();
                Idcount = Idcount + 1;
            }
            ReadOutFront.Close();
            if (Idcount != 0)
            {
                var TextWindows = "Установлен ФронтОфис" + Environment.NewLine + "Удаление невозможно ";
                int AutoClose = 1;
                int MesaggeTop = 350;
                int MessageLeft = 650;
                InstallB52.MessageEnd(TextWindows, AutoClose, MesaggeTop, MessageLeft);
                return;
            }
            SelectTable.Dispose();
            DBConecto.DBcloseFBConectionMemory("FbSystem");
            string OldPuth = SelectPuth;
            StrCreate = "DELETE from CONNECTIONBD30 where CONNECTIONBD30.PUTHBD = " + "'" + SelectPuth + "'";
            DBConecto.DBopenFBConectionMemory(SystemBD.ConnnStringSystem().ToString(), "", "FbSystem");
            DBConecto.UniQuery DeletQuery = new DBConecto.UniQuery(StrCreate, "FB");
            DeletQuery.ExecuteUNIScalar();

            Administrator.AdminPanels.Grid_Puth("SELECT * from CONNECTIONBD30 WHERE CONNECTIONBD30.NAMESERVER = " + "'" + NameServer30 + "'");
            string InsertExecute = "SELECT count(*) from CONNECTIONBD30";
            DBConecto.UniQuery CountConect = new DBConecto.UniQuery(InsertExecute, "FB");
            string CountTable = CountConect.ExecuteUNIScalar() == null ? "" : CountConect.ExecuteUNIScalar().ToString();
            if (Convert.ToUInt32(CountTable) == 0)
            {
                Administrator.AdminPanels.UpdateKeyReestr("PuthSetBD30", "");
                System.Windows.Application.Current.Dispatcher.Invoke(new Action(delegate ()
                {
                    ConectoWorkSpace.Administrator.AdminPanels ConectoWorkSpace_InW = AppStart.LinkMainWindow("WAdminPanels");
                    ConectoWorkSpace_InW.PuthSetBD30.Text = "";
                    ConectoWorkSpace_InW.DefNameServer30.Text = "";
                    Interface.CurrentStateInst("SetPuthBD30", "0", "on_off_1.png", ConectoWorkSpace_InW.SetPuthBD30);
                }));
            }
            FalseIsEnabled30();
            //  Удаление строки с выбранным алиасом.
            StrCreate = "select * from SERVERACTIVFB30 where SERVERACTIVFB30.NAME = " + "'" + NameServer30 + "'";
            DeleteStrAlias(StrCreate, AppStart.TableReestr["ServerDefault30"], "databases.conf");
            DeletConect.Foreground = Brushes.White;
            TablPuthBD30.IsEnabled = true;

        }

        private void CopyLog_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (Administrator.AdminPanels.SetWinSetHub == "CopyLog_Fbd25")
            {
                LabelCopyLog.Foreground = Brushes.White;
                SetWinSetHub = "";
                CopyLogBD25.Visibility = Visibility.Collapsed;
                datePicker25_CopyLogS.Visibility = Visibility.Collapsed;
                datePicker25_CopyLogP.Visibility = Visibility.Collapsed;
                CopyLogPo.Visibility = Visibility.Collapsed;
                BackUpLocServerBD25.Visibility = Visibility.Collapsed;
            }
            else
            {

                if (Putch_Satellite == "")
                {
                    var TextWindows = "У выбранной БД нет спутника. " + Environment.NewLine + "Копирование невозможно ";
                    int AutoClose = 1;
                    int MesaggeTop = -170;
                    int MessageLeft = 650;
                    InstallB52.MessageEnd(TextWindows, AutoClose, MesaggeTop, MessageLeft);
                    return;
                }

                LabelCopyLog.Foreground = Brushes.Indigo;
                SetWinSetHub = "CopyLog_Fbd25";
                CopyLogBD25.Visibility = Visibility.Visible;
                datePicker25_CopyLogS.Visibility = Visibility.Visible;
                datePicker25_CopyLogP.Visibility = Visibility.Visible;
                CopyLogPo.Visibility = Visibility.Visible;
                BackUpLocServerBD25.Visibility = Visibility.Visible;

            }

        }


        private void CopyLog30_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (Administrator.AdminPanels.SetWinSetHub == "CopyLog_Fbd30")
            {
                LabelCopyLog.Foreground = Brushes.White;
                SetWinSetHub = "";
                CopyLogBD30.Visibility = Visibility.Collapsed;
                datePicker30_CopyLogS.Visibility = Visibility.Collapsed;
                datePicker30_CopyLogP.Visibility = Visibility.Collapsed;
                CopyLogPo30.Visibility = Visibility.Collapsed;
                BackUpLocServerBD30.Visibility = Visibility.Collapsed;
            }
            else
            {

                if (Putch_Satellite == "")
                {
                    var TextWindows = "У выбранной БД нет спутника. " + Environment.NewLine + "Копирование невозможно ";
                    int AutoClose = 1;
                    int MesaggeTop = -170;
                    int MessageLeft = 650;
                    InstallB52.MessageEnd(TextWindows, AutoClose, MesaggeTop, MessageLeft);
                    return;
                }

                LabelCopyLog.Foreground = Brushes.Indigo;
                SetWinSetHub = "CopyLog_Fbd30";
                CopyLogBD30.Visibility = Visibility.Visible;
                datePicker30_CopyLogS.Visibility = Visibility.Visible;
                datePicker30_CopyLogP.Visibility = Visibility.Visible;
                CopyLogPo30.Visibility = Visibility.Visible;
                BackUpLocServerBD30.Visibility = Visibility.Visible;

            }

        }

        // Процедура сжатия БД и очистки от мусора 
        private void UpdateBD25_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            UpdateBD25.Foreground = Brushes.Indigo;
            NameObj = "UpdateBD25";
            hubdate = "25";
            ModifiConect("UpdateBD25");
        }

        // Процедура сжатия БД и очистки от мусора 
        private void UpdateBD30_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            UpdateBD30.Foreground = Brushes.Indigo;
            NameObj = "UpdateBD30";
            hubdate = "30";
            ModifiConect("UpdateBD30");
        }


        public void ModifiConect(string ModifiProc, string Satellite = "")
        {

            if (!System.IO.File.Exists(AdminPanels.SelectPuth) || Satellite != "" && !System.IO.File.Exists(Satellite))
            {
                AdminPanels.NameObj = "";
                InstallB52.MessageErorInst("По указанному пути отсутствует БД " + Environment.NewLine + "Путь: " + Environment.NewLine + AdminPanels.SelectPuth);
                this.UpdateBD25.Foreground = (Brush)Brushes.White;
            }
            else
            {
                string cmdText = "";
                if (AdminPanels.NameObj == "CleanConect25" || ModifiProc == "UpdateBD25")
                {
                    if (Satellite == "")
                        this.FalseIsEnabled25();
                    cmdText = "select * from SERVERACTIVFB25 where SERVERACTIVFB25.NAME = '" + AdminPanels.NameServer25 + "'";
                }
                if (AdminPanels.NameObj == "CleanConect30" || ModifiProc == "UpdateBD30")
                {
                    if (Satellite == "")
                        this.FalseIsEnabled30();
                    cmdText = "select * from SERVERACTIVFB30 where SERVERACTIVFB30.NAME = '" + AdminPanels.NameServer30 + "'";
                }
                string str1 = "";
                int num = 0;
                DBConecto.DBopenFBConectionMemory(SystemBD.ConnnStringSystem("Dynamic").ToString(), "", "FbSystem");
                FbCommand fbCommand = new FbCommand(cmdText, (FbConnection)DBConecto.bdFbSystemConect);
                FbDataReader fbDataReader = fbCommand.ExecuteReader();
                while (fbDataReader.Read())
                {
                    str1 = fbDataReader[0].ToString();
                    ++num;
                }
                fbDataReader.Close();
                if (num == 0)
                    return;
                fbCommand.Dispose();
                DBConecto.DBcloseFBConectionMemory("FbSystem");
                string str2 = AdminPanels.NameObj == "CleanConect25" || ModifiProc == "UpdateBD25" ? AdminPanels.CurrentPasswFb25 : AdminPanels.CurrentPasswFb30;
                AppStart.RenderInfo renderInfo = new AppStart.RenderInfo();
                renderInfo.argument1 = str2;
                renderInfo.argument2 = AdminPanels.SelectAlias.Trim();
                renderInfo.argument3 = AdminPanels.SelectPuth;
                renderInfo.argument4 = str1;
                Thread thread = new Thread(new System.Threading.ParameterizedThreadStart(InstallB52.UpdateBD));
                string str3 = Satellite == "" ? AdminPanels.SelectPuth : Satellite;
                if (AdminPanels.NameObj == "CleanConect25" || AdminPanels.NameObj == "CleanConect30")
                {
                    renderInfo.argument3 = str3;
                    renderInfo.argument5 = Satellite;
                    thread = new Thread(new System.Threading.ParameterizedThreadStart(InstallB52.CleanAllBd));
                }
                thread.SetApartmentState(ApartmentState.STA);
                thread.IsBackground = true;
                thread.Start((object)renderInfo);
                ++InstallB52.IntThreadStart;
                WaitMessage waitMessage = new WaitMessage();
                waitMessage.Owner = (Window)this;
                waitMessage.Top = (double)SystemConecto.WorkAreaDisplayDefault[0] - 500.0;
                waitMessage.Left = (double)SystemConecto.WorkAreaDisplayDefault[1] - 300.0;
                waitMessage.Show();
            }


        }

        // Процедура Обновления БД с помощью программы B52Update.exe
        private void UpgradeBD25_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            UpgradeBD25.Foreground = Brushes.Indigo;
            FalseIsEnabled25();
            NameObj = "UpgradeBD25";
            //SelectNewBD25.Visibility = Visibility.Visible;
            TextBox_Fbd25.Visibility = Visibility.Visible;
            //Dir_Fbd25.Visibility = Visibility.Visible;
            LabelPuth.Visibility = Visibility.Visible;
            LabelPuth.Content = "Путь размещения БекОфиса";

            var dlg = new System.Windows.Forms.FolderBrowserDialog();
            dlg.Description = "Путь размещения Обновления";
            System.Windows.Forms.DialogResult result = dlg.ShowDialog();
            if (result == System.Windows.Forms.DialogResult.OK)
            {
                FolderBackPuth = dlg.SelectedPath + (dlg.SelectedPath.LastIndexOf(@"\") <= 2 && dlg.SelectedPath.Length <= 3 ? "" : @"\");
                TextBox_Fbd25.Text = FolderBackPuth;
            }
            else
            {
                TextBox_Fbd25.Text = "";
                //SelectNewBD25.Visibility = Visibility.Collapsed;
                TextBox_Fbd25.Visibility = Visibility.Collapsed;
                //Dir_Fbd25.Visibility = Visibility.Collapsed;
                LabelPuth.Visibility = Visibility.Collapsed;
                LabelPuth.Content = "Путь к БД.";
                UpgradeBD25.Foreground = Brushes.White;
                return;
                
            }

  
                PuthUpgradeBD25 = TextBox_Fbd25.Text;
                AppStart.RenderInfo Arguments01 = new AppStart.RenderInfo() { };
                Arguments01.argument1 = "1";
                Thread thStartTimer01 = new Thread(UpgradeBD25TH);
                thStartTimer01.SetApartmentState(ApartmentState.STA);
                thStartTimer01.IsBackground = true; // Фоновый поток
                thStartTimer01.Start(Arguments01);
                InstallB52.IntThreadStart++;


                WaitMessage WaitWindow = new WaitMessage();
                WaitWindow.Owner = this;
                // размещаем на рабочем столе
                WaitWindow.Top = SystemConecto.WorkAreaDisplayDefault[0] - 700;
                WaitWindow.Left = SystemConecto.WorkAreaDisplayDefault[1] - 300;
                // Отображаем
                WaitWindow.Show();

      

        }


        public  void UpgradeBD25TH(object ThreadObj)
        {
            
            AppStart.RenderInfo arguments = (AppStart.RenderInfo)ThreadObj;
            string StrCreate = "", CurrentUpdate = "";
            Directory.CreateDirectory(SystemConectoServers.PutchServer + @"\Update\");

            if (SystemConecto.ConnectionAvailable())
            {

                DBConecto.DBopenFBConectionMemory(SystemBD.ConnnStringSystem().ToString(), "", "FbSystem");
                StrCreate = "SELECT * from CONNECTIONBD25  WHERE CONNECTIONBD25.PUTHBD = '" + SelectPuth + "'";
                FbCommand SelectTableBack = new FbCommand(StrCreate, DBConecto.bdFbSystemConect);
                SelectTableBack.CommandType = CommandType.Text;
                FbDataReader ReadOutTableBack = SelectTableBack.ExecuteReader();
                while (ReadOutTableBack.Read()) { CurrentUpdate = ReadOutTableBack[4].ToString(); }
                ReadOutTableBack.Close();

                // ФТП Одесса
                //Login = ftp://partner
                //Password = cnelbzgk.c
                //Ip = 195.138.94.90

                // updatework.conecto.ua Чтение паролей из настроек ПО WriterConfigUserXML (Пользователь устанавливается на стороне сервера)
                // updatework.conecto.ua/updatework.conecto.ua/ "update_workspace" "conect1074"
                //string strServer, string NameUser, string PasswdUser, int TypeCommand = 1, string PutchTMPFile = ""
                // Прверка даты модификации  B52Update8.exe.
                DateTime BDUpdate = DateTime.Today;
                Uri UriServer = new Uri(@"ftp://195.138.94.90/bin/B52Update8.exe");
                string NameUser = "partner";
                string PasswdUser = "cnelbzgk.c";


                FtpWebRequest request = (FtpWebRequest)WebRequest.Create(UriServer);
                request.Method = WebRequestMethods.Ftp.GetDateTimestamp;

                request.Credentials = new NetworkCredential(NameUser, PasswdUser);
                FtpWebResponse response = (FtpWebResponse)request.GetResponse();
                DateTime UpdateB52BD = response.LastModified;
                response.Close();
                ShortDate = UpdateB52BD.ToString("yyyyMMdd");

                // Анализ даты предыдущего обновления
                if (AppStart.TableReestr["UpdateB52"] == "") AppStart.TableReestr["UpdateB52"] = AppStart.rkAppSetingAllUser.GetValue("UpdateB52").ToString();

                if (AppStart.TableReestr["UpdateB52"] != ShortDate)
                {
                    // Обновляем  B52Update8.exe.

                    var ConectionFTPUpdate = SystemConecto.ConntecionFTP(@"195.138.94.90/bin/" + "B52Update8.exe", NameUser, PasswdUser, 2, SystemConecto.PutchApp + @"Repository\B52Update8.exe");

                    if (ConectionFTPUpdate == null)
                    {
                        System.Windows.Application.Current.Dispatcher.Invoke(new Action(delegate ()
                        {
                            ConectoWorkSpace.WaitMessage ConectoWorkSpace_Off = AppStart.LinkMainWindow("WaitMessage_");
                            ConectoWorkSpace_Off.Close();
                        }));
                        var TextWindows = "Отсутствует B52Update8.exe." + Environment.NewLine + "Обновление прекращено. ";
                        MainWindow.MessageInstall(TextWindows);
                        System.Windows.Application.Current.Dispatcher.Invoke(new Action(delegate ()
                        {
                            ConectoWorkSpace.Administrator.AdminPanels ConectoWorkSpace_InW = AppStart.LinkMainWindow("WAdminPanels");
                            ConectoWorkSpace_InW.UpgradeBD25.Foreground = Brushes.White;
                            ConectoWorkSpace_InW.TextBox_Fbd25.Text = "";
                            ConectoWorkSpace_InW.SelectNewBD25.Visibility = Visibility.Collapsed;
                            ConectoWorkSpace_InW.TextBox_Fbd25.Visibility = Visibility.Collapsed;
                            ConectoWorkSpace_InW.Dir_Fbd25.Visibility = Visibility.Collapsed;
                            ConectoWorkSpace_InW.LabelPuth.Visibility = Visibility.Collapsed;
                            ConectoWorkSpace_InW.LabelPuth.Content = "Путь к БД.";
                        }));

                        return;
                    }
                    AppStart.rkAppSetingAllUser.SetValue("UpdateB52", Administrator.AdminPanels.ShortDate);
                    Administrator.AdminPanels.UpdateKeyReestr("UpdateB52", Administrator.AdminPanels.ShortDate);
                }
                else
                {
                    if (CurrentUpdate == ShortDate && CurrentUpdate != "")
                    {
                        System.Windows.Application.Current.Dispatcher.Invoke(new Action(delegate ()
                        {
                            ConectoWorkSpace.WaitMessage ConectoWorkSpace_Off = AppStart.LinkMainWindow("WaitMessage_");
                            ConectoWorkSpace_Off.Close();
                        }));
                        var TextWindows = "База данных не требует обновления" + Environment.NewLine + "Выполнение процедуры прекращено. ";
                        MainWindow.MessageInstall(TextWindows);
                        System.Windows.Application.Current.Dispatcher.Invoke(new Action(delegate ()
                        {
                            ConectoWorkSpace.Administrator.AdminPanels ConectoWorkSpace_InW = AppStart.LinkMainWindow("WAdminPanels");
                            ConectoWorkSpace_InW.UpgradeBD25.Foreground = Brushes.White;
                            ConectoWorkSpace_InW.TextBox_Fbd25.Text = "";
                            ConectoWorkSpace_InW.SelectNewBD25.Visibility = Visibility.Collapsed;
                            ConectoWorkSpace_InW.TextBox_Fbd25.Visibility = Visibility.Collapsed;
                            ConectoWorkSpace_InW.Dir_Fbd25.Visibility = Visibility.Collapsed;
                            ConectoWorkSpace_InW.LabelPuth.Visibility = Visibility.Collapsed;
                            ConectoWorkSpace_InW.LabelPuth.Content = "Путь к БД.";

                        }));
                        return;
                    }


                }

            }
            else
            {
                System.Windows.Application.Current.Dispatcher.Invoke(new Action(delegate ()
                {
                    ConectoWorkSpace.WaitMessage ConectoWorkSpace_Off = AppStart.LinkMainWindow("WaitMessage_");
                    ConectoWorkSpace_Off.Close();
                }));
                var TextWindows = "Отсутствует связь с интернетом." + Environment.NewLine + "Обновление прекращено. ";
                MainWindow.MessageInstall(TextWindows);
                System.Windows.Application.Current.Dispatcher.Invoke(new Action(delegate ()
                {
                    ConectoWorkSpace.Administrator.AdminPanels ConectoWorkSpace_InW = AppStart.LinkMainWindow("WAdminPanels");
                    ConectoWorkSpace_InW.UpgradeBD25.Foreground = Brushes.White;
                    ConectoWorkSpace_InW.TablPuthBD.IsEnabled = true;
                }));
                return;
            }
   
        
            Dictionary<string, string> fbembedList = new Dictionary<string, string>();
            fbembedList.Add(SystemConectoServers.PutchServer + @"Update\updatedll.zip", "b52/update/");
            if (SystemConecto.IsFilesPRG(fbembedList, -1, "- Проверка файлов во время установки обновления БД") != "True")
            {

                var TextWindows = "Отсутствует B52Update8.exe." + Environment.NewLine + "Обновление прекращено. ";
                MainWindow.MessageInstall(TextWindows);
                System.Windows.Application.Current.Dispatcher.Invoke(new Action(delegate ()
                {
                    ConectoWorkSpace.Administrator.AdminPanels ConectoWorkSpace_InW = AppStart.LinkMainWindow("WAdminPanels");
                    ConectoWorkSpace_InW.UpgradeBD25.Foreground = Brushes.White;
                    ConectoWorkSpace_InW.TablPuthBD.IsEnabled = true;
                }));
                return;
            }
            string PuthDir = SystemConecto.OS64Bit ? @"c:\Windows\SysWOW64\" : @"c:\Windows\System32\";
            Install.Extract.Unarch_arhive(SystemConectoServers.PutchServer + @"\Update\updatedll.zip", PuthDir);
            PuthBdUpdate = PuthUpgradeBD25 + @"Db\";  //SystemConecto.OS64Bit ? @"c:\Program Files (x86)
            Directory.CreateDirectory(PuthBdUpdate);
            File.Copy(PuthUpgradeBD25 + "firebird.msg", PuthBdUpdate + "firebird.msg",true);

            NameObj = "UpgradeBD25";
            string ServerPort = "";
            int Idcount = 0;
            StrCreate = "select * from SERVERACTIVFB25 where SERVERACTIVFB25.NAME = " + "'" + NameServer25 + "'";
            DBConecto.DBopenFBConectionMemory(SystemBD.ConnnStringSystem().ToString(), "", "FbSystem");
            FbCommand SelectTable = new FbCommand(StrCreate, DBConecto.bdFbSystemConect);
            SelectTable.CommandType = CommandType.Text;
            FbDataReader ReadTable = SelectTable.ExecuteReader();
            while (ReadTable.Read())
            {
                ServerPort = ReadTable[0].ToString();
                Idcount++;
            }
            ReadTable.Close();
            if (Idcount == 0)
            {
                var TextWindows = "Отсутствует активный сервер" + Environment.NewLine + "Обновление прекращено. ";
                MainWindow.MessageInstall(TextWindows); 
                SelectTable.Dispose();
                DBConecto.DBcloseFBConectionMemory("FbSystem");
                System.Windows.Application.Current.Dispatcher.Invoke(new Action(delegate ()
                {
                    ConectoWorkSpace.Administrator.AdminPanels ConectoWorkSpace_InW = AppStart.LinkMainWindow("WAdminPanels");
                    ConectoWorkSpace_InW.UpgradeBD25.Foreground = Brushes.White;
                    ConectoWorkSpace_InW.TablPuthBD.IsEnabled = true;
                }));
                return;
            } 
            SelectTable.Dispose();
            DBConecto.DBcloseFBConectionMemory("FbSystem");


            Administrator.AdminPanels.hubdate = "25";
            AppStart.RenderInfo Arguments01 = new AppStart.RenderInfo() { };
            Arguments01.argument1 = CurrentPasswFb25;
            Arguments01.argument2 = SelectAlias.Trim();
            Arguments01.argument3 = SelectPuth;
            Arguments01.argument4 = ServerPort;
            Thread thStartTimer01 = new Thread(InstallB52.UpdateBD);
            thStartTimer01.SetApartmentState(ApartmentState.STA);
            thStartTimer01.IsBackground = true; // Фоновый поток
            thStartTimer01.Start(Arguments01);
            InstallB52.IntThreadStart++;
        }



        // Процедура переназначения алиаса по умлочанию
        private void ChangeDefault25_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            ChangeConnectBD25();
        }

        private void ChangeConnectBD25()
        {
            // переназначаем Бд по умолчанию
            if (!File.Exists(SelectPuth))
            {
                var TextWindows = "Отсутствует файл БД." + Environment.NewLine + "Изменение пути невозможно ";
                int AutoClose = 1;
                int MesaggeTop = 3;
                int MessageLeft = 650;
                InstallB52.MessageEnd(TextWindows, AutoClose, MesaggeTop, MessageLeft);
                return;
            }
            Administrator.AdminPanels.UpdateKeyReestr("PuthSetBD25", SelectPuth);
            Administrator.AdminPanels.UpdateKeyReestr("SetPuthBD25", "2");
            System.Windows.Application.Current.Dispatcher.Invoke(new Action(delegate ()
            {
                ConectoWorkSpace.Administrator.AdminPanels ConectoWorkSpace_InW = AppStart.LinkMainWindow("WAdminPanels");
                ConectoWorkSpace_InW.PuthSetBD25.Text = SelectPuth;
                ConectoWorkSpace_InW.DefNameServer25.Text = NameServer25;
                Interface.CurrentStateInst("SetPuthBD25", "2", "on_off_2.png", ConectoWorkSpace_InW.SetPuthBD25);
            }));
            FalseIsEnabled25();
            TablPuthBD.IsEnabled = true;

        }

        private void ChangeDefault30_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            ChangeDefault30();
        }
        private void ChangeDefault30()
        {
            // перезапускаем Firebird 3.0.
            string PatchSR = AppStart.TableReestr["ServerDefault30"];
            if (!File.Exists(SelectPuth))
            {
                var TextWindows = "Отсутствует файл БД." + Environment.NewLine + "Изменение пути невозможно ";
                int AutoClose = 1;
                int MesaggeTop = 3;
                int MessageLeft = 650;
                InstallB52.MessageEnd(TextWindows, AutoClose, MesaggeTop, MessageLeft);
                return;
            }
            Administrator.AdminPanels.UpdateKeyReestr("PuthSetBD30", SelectPuth);
            Administrator.AdminPanels.UpdateKeyReestr("SetPuthBD30", "2");
            System.Windows.Application.Current.Dispatcher.Invoke(new Action(delegate ()
            {
                ConectoWorkSpace.Administrator.AdminPanels ConectoWorkSpace_InW = AppStart.LinkMainWindow("WAdminPanels");
                ConectoWorkSpace_InW.PuthSetBD30.Text = SelectPuth;
                ConectoWorkSpace_InW.DefNameServer30.Text = NameServer30;
                Interface.CurrentStateInst("SetPuthBD30", "2", "on_off_2.png", ConectoWorkSpace_InW.SetPuthBD30);
            }));
            FalseIsEnabled30();

        }

  
        // 1.4. MsSQL
        private void DirSetBDMsSQL_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            PathFileBD_Click();
            string DeviceOn = PuthSetBDMsSQL.Text.Substring(0, 3);
            // Проверка наличия свободного пространства на диске куда будем ложить 
            DriveInfo di = new DriveInfo(DeviceOn);
            long Ffree = (di.TotalFreeSpace / 1024) / 1024;
            string MBFree = Ffree.ToString("#,##") + " MB";
            if (Ffree - 2500 < 0)
            {
                MessageBox.Show("Для установки БД MsSQL необходимо свободное пространство 5Гб на диске." + DeviceOn + Environment.NewLine + "Текущее пространство " + MBFree + Environment.NewLine + "Выполнение процедуры остановлено. ");
                return;
            }

            //InstallB52.PuthSetBDPostGreSQL();
            System.Windows.Application.Current.Dispatcher.Invoke(new Action(delegate ()
            {
                ConectoWorkSpace.Administrator.AdminPanels ConectoWorkSpace_InW = AppStart.LinkMainWindow("WAdminPanels");
                Interface.CurrentStateInst("SetBDMsSQL", "1", "on_off_3.png", ConectoWorkSpace_InW.SetBDMsSQL);
            }));
        }

        #region
        // ---------------------------------------------------------------------------
        // Блок процедур обработки запросов по обслуживанию сервера и БД Postgresql
        // Процедура инсталяции БД Postgresql

        private void PostgresqlOnOff_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            ImageObj = "PostgresqlOnOff";
            if (Convert.ToInt16(AppStart.TableReestr["PostgresqlOnOff"]) == 2)
            { 
                 DeletSreverPostGre.Foreground = Brushes.Indigo;
                Administrator.AdminPanels.ImageObj = "StoptServPostGre";
                string TextMesage = "Вы действительно хотите удалить сервер?";
                Inst2530 = "DelPostGre";
                SetUpdateRestore =0;
                WinOblakoSetUpdate OblakoSetWindow = new WinOblakoSetUpdate(TextMesage);
                OblakoSetWindow.Owner = this;
                OblakoSetWindow.Top = this.Top + 100;
                OblakoSetWindow.Left = this.ScrollViewerd.Width - (OblakoSetWindow.Width * 2) - 100;
                OblakoSetWindow.Show();
                return;
            }

            else AddSeverPostGre.Foreground = Brushes.Indigo;
            Interface.ObjektOnOff("PostgresqlOnOff", ref PostgresqlOnOff, new Install.Metods { NameClass = "ConectoWorkSpace.InstallB52", Install = "InstallServPostgresql", UnInstal = "UnInstallServPostgresql" });
        }

        private void SetBDPostGreSQL_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            MessageBox.Show("Процедура в разработке");
            return;
        }

        private void ServergridPostGre_Loaded(object sender, RoutedEventArgs e)
        {
            SetServerGrid("SELECT * from SERVERACTIVPOSTGRESQL");
        }

        private void ServergridPostGre_MouseUp(object sender, MouseButtonEventArgs e)
        {
            string InsertExecute = "SELECT count(*) from SERVERACTIVPOSTGRESQL";
            DBConecto.DBopenFBConectionMemory(SystemBD.ConnnStringSystem().ToString(), "", "FbSystem");
            DBConecto.UniQuery CountQuery = new DBConecto.UniQuery(InsertExecute, "FB");
            string CountTable = CountQuery.ExecuteUNIScalar() == null ? "" : CountQuery.ExecuteUNIScalar().ToString();

            if (Convert.ToUInt32(CountTable) != 0)
            {
                if (TablPuthServerPostGre.SelectedItem != null)
                {
                    ServerTablePG path = TablPuthServerPostGre.SelectedItem as ServerTablePG;
                    SetPortPG = Convert.ToString(path.Port);
                    SetPuthPG = path.Puth;
                    SetNamePG = path.Name;
                    SetActivPG = path.State;
                    CurrentPasswPg = path.CurrentPassw;
                    DeletSreverPostGre.IsEnabled = true;
                    RestartServPostGre.IsEnabled = true;
                    SetDefaultServerPostGre.IsEnabled = true;
                    StoptServPostGre.IsEnabled = true;
                    SetChangePasswPG.IsEnabled = true;
                    CountConect25("SELECT count(*) from CONNECTIONPOSTGRESQL WHERE CONNECTIONPOSTGRESQL.NAMESERVER =" + "'" + SetNamePG + "'");
                    Grid_Puth("SELECT * from CONNECTIONPOSTGRESQL WHERE CONNECTIONPOSTGRESQL.NAMESERVER =" + "'" + AppStart.TableReestr["NameServerPG"] + "'");

                }
            }
            DBConecto.DBcloseFBConectionMemory("FbSystem");

        }

        private void SetDefaultServerPostGre_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

            if (SetActivPG == "Stop")
            {
                var TextWindows = "Не активный сервер нельзя использовать по умолчанию " + Environment.NewLine + "Выполнение процедуры остановлено. ";
                int AutoClose = 1;
                int MesaggeTop = -80;
                int MessageLeft = 650;
                InstallB52.MessageEnd(TextWindows, AutoClose, MesaggeTop, MessageLeft);
                return;
            }
            if (!File.Exists(SetPuthPG + @"bin\pg_ctl.exe"))
            {
                var TextWindows = "Отсутствует файл pg_ctl.exe." + Environment.NewLine + "Выполнение процедуры остановлено ";
                int AutoClose = 1;
                int MesaggeTop = 3;
                int MessageLeft = 650;
                InstallB52.MessageEnd(TextWindows, AutoClose, MesaggeTop, MessageLeft);
                return;
            }
            System.Windows.Application.Current.Dispatcher.Invoke(new Action(delegate ()
            {
                ConectoWorkSpace.Administrator.AdminPanels ConectoWorkSpace_InW = AppStart.LinkMainWindow("WAdminPanels");
                ConectoWorkSpace_InW.PuthSetServerPostGre.Text = SetPuthPG;
                ConectoWorkSpace_InW.PuthSetBD25PostGre.Text = "";
                Interface.CurrentStateInst("SetBDPostGreSQL", "0", "on_off_1.png", ConectoWorkSpace_InW.SetPuthBD30);
            }));
            UpdateKeyReestr("ServerDefaultPG", SetPuthPG);
            UpdateKeyReestr("NameServerPG", SetNamePG);
            UpdateKeyReestr("PuthSetBDPostGreSQL", "");

            // Перегрузить таблицу алиасов
            LoadAliasPG(AppStart.TableReestr["NameServerPG"], AppStart.TableReestr["ServerDefaultPG"]);
            Grid_Puth("SELECT * from CONNECTIONPOSTGRESQL WHERE CONNECTIONPOSTGRESQL.NAMESERVER =" + "'" + AppStart.TableReestr["NameServerPG"] + "'");
        }

        private void StoptServPostGre_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            IsEnaledServerPG();
            StoptServPostGre.Foreground = Brushes.Indigo;
            ImageObj = "StoptServPG";
 
            SelectPuthPG = AppStart.TableReestr["PuthSetBDPostGreSQL"]; 
            if (InstallB52.RemoveServerFB25(SetPuthPG, SetNamePG) > 0)
            {
 
                if (SetActivPG == "Stop")
                {
                    StoptServPostGre.Content = "Остановить";
                    var TextWin = "Запуск сервера PostGreSQL" + Environment.NewLine + " успешно выполнен";
                    int AutoCl = 1;
                    int MesaggeTp = -80;
                    int MessageLef = 800;
                    InstallB52.MessageEnd(TextWin, AutoCl, MesaggeTp, MessageLef);
                }

                if (SetActivPG == "Activ")
                {
                    StoptServPostGre.Content = "Запустить";
                    var TextWindows = "Остановка сервера PostGreSQL" + Environment.NewLine + " успешно выполнена";
                    int AutoClose = 1;
                    int MesaggeTop = -170;
                    int MessageLeft = 800;
                    InstallB52.MessageEnd(TextWindows, AutoClose, MesaggeTop, MessageLeft);              
                }
 
                string StrCreate = "UPDATE SERVERACTIVPOSTGRESQL SET ACTIVONOFF = '"+ (SetActivPG == "Activ" ? "Stop": "Activ") + "' WHERE SERVERACTIVPOSTGRESQL.PUTH = " + "'" + SetPuthPG + "'";
                ModifyTable(StrCreate);
                SetServerGrid("SELECT * from SERVERACTIVPOSTGRESQL");
                //Grid_Puth("SELECT * from CONNECTIONPOSTGRESQL WHERE CONNECTIONPOSTGRESQL.NAMESERVER = " + "'" + SetNamePG + "'");
                StoptServPostGre.Foreground = Brushes.White;
            }
        }
        // Перезапуск сервера
        private void RestartServPostGre_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            RestartServPostGre.Foreground = Brushes.Indigo;
            string TextWindows = "Перезапуск сервера PostGreSQL" + Environment.NewLine + " успешно выполнен";
            ImageObj = "RestartServPG";
            IsEnaledServerPG();
            InstallB52.RemoveServerFB25(SetPuthPG, SetNamePG);
            Inst2530 = "postgres";
            IndexActivProces = -1;
            ScanActivFirebird();
            if (IndexActivProces == 0)
            { 
                string StrCreate = "UPDATE SERVERACTIVPOSTGRESQL SET ACTIVONOFF = 'Stop' WHERE SERVERACTIVPOSTGRESQL.PUTH = " + "'" + SetPuthPG + "'";
                ModifyTable(StrCreate);
            }
            else TextWindows = "Перезапуск сервера Firebird 3.0" + Environment.NewLine + " Не выполнен";
            SetServerGrid("SELECT * from SERVERACTIVPOSTGRESQL");
            Grid_Puth("SELECT * from CONNECTIONPOSTGRESQL WHERE CONNECTIONPOSTGRESQL.NAMESERVER =" + "'" + AppStart.TableReestr["NameServerPG"] + "'");

            int AutoClose = 1;
            int MesaggeTop = -170;
            int MessageLeft = 800;
            InstallB52.MessageEnd(TextWindows, AutoClose, MesaggeTop, MessageLeft);
            RestartServPostGre.Foreground = Brushes.White;
        }

        private void AddServerPostGre_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            ConectoWorkSpace.Administrator.AdminPanels ConectoWorkSpace_InW = AppStart.LinkMainWindow("WAdminPanels");
            if (ConectoWorkSpace_InW == null) return;
            CallProgram = "AddServerPostGre";
            string TextMesage = "Вы действительно хотите установить новый сервер?";
            Inst2530 = "PostGre";
 
            WinOblakoSetUpdate OblakoSetWindow = new WinOblakoSetUpdate(TextMesage);
            OblakoSetWindow.Owner = ConectoWorkSpace_InW;
            OblakoSetWindow.Top = ConectoWorkSpace_InW.Top + 150;
            OblakoSetWindow.Left = ConectoWorkSpace_InW.ScrollViewerd.Width - (OblakoSetWindow.Width * 2) - 100;
            OblakoSetWindow.Show();
        }

        private void DeletSreverPostGre_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            ConectoWorkSpace.Administrator.AdminPanels ConectoWorkSpace_InW = AppStart.LinkMainWindow("WAdminPanels");
            if (ConectoWorkSpace_InW == null) return;
            IsEnaledServerPG();
            Administrator.AdminPanels.ImageObj = "StoptServPostGre";
            string TextMesage = "Вы действительно хотите удалить сервер?";
            Inst2530 = "DelPostGre";
            DeleteDefaultServer = 0;
            WinOblakoSetUpdate OblakoSetWindow = new WinOblakoSetUpdate(TextMesage);
            OblakoSetWindow.Owner = ConectoWorkSpace_InW;
            OblakoSetWindow.Top = ConectoWorkSpace_InW.Top + 100;
            OblakoSetWindow.Left = ConectoWorkSpace_InW.ScrollViewerd.Width - (OblakoSetWindow.Width * 2) - 100;
            OblakoSetWindow.Show();

        }

        private void GridBDPostGre_Loaded(object sender, RoutedEventArgs e)
        {
            if (AppStart.TableReestr["NameServerPG"] != "")
            {
                SelectPuthPG = ""; SelectAliasPG = ""; NameServerPG = "";
                Grid_Puth("SELECT * from CONNECTIONPOSTGRESQL WHERE CONNECTIONPOSTGRESQL.NAMESERVER =" + "'" + AppStart.TableReestr["NameServerPG"] + "'");
            }
        }

        private void GridBDPostGre_MouseUp(object sender, MouseButtonEventArgs e)
        {
            string InsertExecute = "SELECT count(*) from CONNECTIONPOSTGRESQL";
            DBConecto.DBopenFBConectionMemory(SystemBD.ConnnStringSystem().ToString(), "", "FbSystem");
            DBConecto.UniQuery CountQuery = new DBConecto.UniQuery(InsertExecute, "FB");
            string CountTable = CountQuery.ExecuteUNIScalar() == null ? "" : CountQuery.ExecuteUNIScalar().ToString();

            if (Convert.ToUInt32(CountTable) != 0)
            {
                if (TablPuthBDPostGreSql.SelectedItem != null || TablAlias_PostGreSQL.SelectedItem != null)
                {
                    TablePG path = TablPuthBDPostGreSql.SelectedItem != null ? TablPuthBDPostGreSql.SelectedItem as TablePG : TablAlias_PostGreSQL.SelectedItem as TablePG;

                    SelectPuthPG = path.Puth;
                    SelectAliasPG = path.Alias;
                    NameServerPG = path.NameServer;
                    Putch_SatellitePG = path.Satellite;
                    DeletConectPostGreSql.IsEnabled = true;
                    CleanConectPostGreSql.IsEnabled = true;
                    CleanTrashPostGreSql.IsEnabled = true;
                    ChangeDefaultPostGreSql.IsEnabled = true;
        
                    string CurrentSetBD = "select * from SERVERACTIVPOSTGRESQL where SERVERACTIVPOSTGRESQL.NAME = " + "'" + NameServerPG + "'";
                    FbCommand ServerKey = new FbCommand(CurrentSetBD, DBConecto.bdFbSystemConect);
                    ServerKey.CommandType = CommandType.Text;
                    FbDataReader ReaderName = ServerKey.ExecuteReader();
                    while (ReaderName.Read())
                    {
                        SetPortPG = ReaderName[0].ToString();
                        SetPuthPG = ReaderName[1].ToString();
                        CurrentPasswPg = ReaderName[5].ToString();
                    }
                    ReaderName.Close();
                    ServerKey.Dispose();

                }

            }
            else { SelectPuthPG = ""; SelectAliasPG = ""; NameServerPG = ""; }
            DBConecto.DBcloseFBConectionMemory("FbSystem");
        }

        private void ChangePasswPG_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (OnChangePasPG.IsEnabled == true)
            {
                SetChangePasswPG.Foreground = Brushes.White;
                LabelPasPG.Visibility = Visibility.Collapsed;
                OldPasswPG.Visibility = Visibility.Collapsed;
                EyeOldPasswPG.Visibility = Visibility.Collapsed;
                CarentPasswPG.Visibility = Visibility.Collapsed;
                ButonChangePasPG.Visibility = Visibility.Collapsed;
                OnChangePasPG.Visibility = Visibility.Collapsed;
                OnChangePasPG.IsEnabled = false;

            }
            else
            {
                SetChangePasswPG.Foreground = Brushes.Indigo;
                LabelPasPG.Visibility = Visibility.Visible;
                OldPasswPG.Visibility = Visibility.Visible;
                EyeOldPasswPG.Visibility = Visibility.Visible;
                CarentPasswPG.Visibility = Visibility.Visible;
                ButonChangePasPG.Visibility = Visibility.Visible;
                OnChangePasPG.Visibility = Visibility.Visible;
                OnChangePasPG.IsEnabled = true;
            }

        }


        private void EyeOldPasswPG_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (CarentPasswPG.Text == "")
            {
                CarentPasswPG.Visibility = Visibility.Visible;
                CarentPasswPG.Text = CurrentPasswPg;
            }
            else
            {
                CarentPasswPG.Visibility = Visibility.Collapsed;
                CarentPasswPG.Text = "";
            }


        }

        private void OnChangePasPG_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (OldPasswPG.Text.Length == 0)
            {
                string TextWin = "Не введён новый пароль. " + Environment.NewLine + "Выполнение процедуры остановлено. ";
                int AutoCl = 1;
                int MesaggeTop = -200;
                int MessageLeft = 600;
                InstallB52.MessageEnd(TextWin, AutoCl, MesaggeTop, MessageLeft);
                SetChangePasswPG.Foreground = Brushes.White;
                LabelPasPG.Visibility = Visibility.Collapsed;
                OldPasswPG.Visibility = Visibility.Collapsed;
                EyeOldPasswPG.Visibility = Visibility.Collapsed;
                CarentPasswPG.Visibility = Visibility.Collapsed;
                ButonChangePasPG.Visibility = Visibility.Collapsed;
                OnChangePasPG.Visibility = Visibility.Collapsed;
                return;
            }

            hubdate = "PG";

            string PuthFileExePG = SetPuthPG + @"bin\psql.exe";
            if (File.Exists(PuthFileExePG))
            {

                ArgumentCmd = ""; // " start -D  \"" + Administrator.AdminPanels.SelectPuthPG + "\"";
                Administrator.AdminPanels.RunProcess(PuthFileExePG, ArgumentCmd);
            }

            string StrCreate = "UPDATE SERVERACTIVPOSTGRESQL SET CURRENTPASSW = '" + OldPasswPG.Text.Trim() + "' WHERE SERVERACTIVPOSTGRESQL.PUTH = " + "'" + SetPuthPG + "'";
            ModifyTable(StrCreate);
            SetServerGrid("SELECT * from SERVERACTIVFB25");
            CurrentPasswPg = OldPasswPG.Text.Trim(); 
            SetChangePasswPG.Foreground = Brushes.White;
            LabelPasPG.Visibility = Visibility.Collapsed;
            OldPasswPG.Visibility = Visibility.Collapsed;
            EyeOldPasswPG.Visibility = Visibility.Collapsed;
            CarentPasswPG.Visibility = Visibility.Collapsed;
            ButonChangePasPG.Visibility = Visibility.Collapsed;
            OnChangePasPG.Visibility = Visibility.Collapsed;

        }

            private void OldPasswPG_KeyUp(object sender, KeyEventArgs e)
        {
            Administrator.AdminPanels.UpdateKeyReestr("OldPasswPG", OldPasswPG.Text);
        }
        private void OldPasswPG_LostFocus(object sender, RoutedEventArgs e)
        {
   
        }



        private void DeletConectPostGreSql_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

        }

        private void AddConectPostGreSql_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

        }

        private void CleanConectPostGreSql_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

        }

        private void CleanTrashPostGreSql_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

        }

        private void ChangeDefaultPostGreSql_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

        }

        public void IsEnaledServerPG()
        {
            DeletSreverPostGre.IsEnabled = false;
            RestartServPostGre.IsEnabled = false;
            SetDefaultServerPostGre.IsEnabled = false;
            StoptServPostGre.IsEnabled = false;
            SetChangePasswPG.IsEnabled = false;
        }

        #endregion

        #region
        // ---------------------------------------------------------------------------
        // Блок процедур обработки запросов по обслуживанию сервера и БД MsSql
        // Процедура инсталяции БД MsSql
        private void MsSqlOnOff_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            Interface.ObjektOnOff("MsSqlOnOff", ref MsSqlOnOff, new Install.Metods { NameClass = "ConectoWorkSpace.InstallB52", Install = "InstallServMsSql", UnInstal = "UnInstallServMsSql" });
        }

        private void SetBDMsSQL_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            MessageBox.Show("Процедура в разработке");
            return;
        }
        #endregion

        #endregion

        #region Закладка "Лицензионные ключи"

        private void InitPanel_licenziyaKey(object sender, MouseButtonEventArgs e)
        {
            InitPanellicenziyaKey();
        }

        private void InitPanellicenziyaKey()
        {

            if (SetInitPanellicenziyaKey == 1)
            {
                var TextWindows = "Выполняется идентификация данных." + Environment.NewLine + "Пожалуйста подождите. ";
                int AutoClose = 1;
                int MesaggeTop = -170;
                int MessageLeft = 300;
                ConectoWorkSpace.MainWindow.MessageEnd(TextWindows, AutoClose, MesaggeTop, MessageLeft);
                InitKeyOnOfflicenziyaKey();
                InitPanel_();
                InitTextlicenziyaKey();
                if (StartBack == 1) { CheckInstalBack25(); }
                if (StartFront == 1){ CheckInstalFront25(); }
                Interface.CurrentStateInst("BackOfAdrOnOff_IP4", "0", "on_off_1.png", BackOfAdrOnOff_IP4);
                SetInitPanellicenziyaKey = 0;
                BackOfAdresPorta.Text = AppStart.TableReestr["BackOfAdresPorta"];
                var pic = (Image)LogicalTreeHelper.FindLogicalNode(this, "BackOfServKeyOnOff");
                var uriSource = new Uri(@"/Conecto®%20WorkSpace;component/Images/on_off_1.png", UriKind.Relative);
                pic.Source = new BitmapImage(uriSource);
                UpdateKeyReestr("BackOfServKeyOnOff", "0");
                GnclientServer.IsEnabled = false;
                IpAdresServerKey.Visibility = Visibility.Collapsed;
 
                //IPHostEntry ipHost = Dns.GetHostEntry("localhost");
                // получаем IP-адрес хоста
                var host = Dns.GetHostEntry(Dns.GetHostName());
                foreach (var ip in host.AddressList)
                {
                    if (ip.AddressFamily == AddressFamily.InterNetwork)
                    {
                        IpAdresHome.Content = ip.ToString();
                        AppStart.TableReestr["SetTextAdrDataIp4BackOf"] = ip.ToString();
                    }
                }

                string SetKeyValue = Convert.ToInt32(AppStart.TableReestr["SetLocKeyOnOff"].ToString()) == 2 ? "on_off_2.png" : "on_off_1.png";
                var LocKey = (Image)LogicalTreeHelper.FindLogicalNode(this, "SetLocKeyOnOff");
                var uriLocKey = new Uri(@"/Conecto®%20WorkSpace;component/Images/" + SetKeyValue, UriKind.Relative);
                LocKey.Source = new BitmapImage(uriLocKey);
                SetKeyValue = Convert.ToInt32(AppStart.TableReestr["BackOfServKeyOnOff"].ToString()) == 2 ? "on_off_2.png" : "on_off_1.png";
                var ServKey = (Image)LogicalTreeHelper.FindLogicalNode(this, "BackOfServKeyOnOff");
                var uriServKey = new Uri(@"/Conecto®%20WorkSpace;component/Images/" + SetKeyValue, UriKind.Relative);
                ServKey.Source = new BitmapImage(uriServKey);

            }
            Inst2530 = "grdsrv";
            ScanActivFirebird();
            if (IndexActivProces >= 0)
            {
     

                PuthGrdsrv = NamePuth[IndexActivProces].Substring(0, NamePuth[IndexActivProces].LastIndexOf(@"\")+1);
                if (SystemConecto.File_(PuthGrdsrv + "gnclient.ini", 5))
                {
                    int Idcount = 0;
                    Encoding code = Encoding.Default;
                    string[] fileLines = File.ReadAllLines(PuthGrdsrv + "gnclient.ini", code);
                    foreach (string x in fileLines)
                    {
                        if (x.Contains("TCP_PORT") == true)
                        {
                            string[] data = x.Split('=');
                            AppStart.TableReestr["BackOfAdresPorta"] = data[1];
                        }

                        if (x.Contains("IP_NAME") == true)
                        {
                            string[] data = x.Split('=');
                            IpAdresServerKey.Content = data[1];
                            //IpAdresServerKey.Visibility = Visibility.Visible;
                        }
                        Idcount++;
                    }

                }

            }
        }

        public static void InitKeyOnOfflicenziyaKey()
        {
            AdminPanels.ButtonPanel = new string[4];
            AdminPanels.ButtonPanel[0] = "BackOfAdrOnOff_IP4";
            AdminPanels.ButtonPanel[1] = "BackOfAdrOnOff_IP6";
            AdminPanels.ButtonPanel[2] = "BackOfServKeyOnOff";
            AdminPanels.ButtonPanel[3] = "SetLocKeyOnOff";


            if (LoadTableRestr == 0) InitKeySystemFB("InitKey");
            InitKeyOnOff();
        }
        // Процедура инициализации текстовых констант в беке
        public static void InitTextlicenziyaKey()
        {
            AdminPanels.ButtonPanel = new string[3];
            AdminPanels.ButtonPanel[0] = "SetTextAdrDataIp4BackOf";
            AdminPanels.ButtonPanel[1] = "SetTextAdrDataIp6BackOf";
            AdminPanels.ButtonPanel[2] = "BackOfAdresPorta";

            if (LoadTableRestr == 0) InitKeySystemFB("InitText");
            InitTextOnOff();
        }

        // Добавить тестовый ключ 
        private void TestkeyOnOff_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {

            Administrator.AdminPanels.SetWinSetHub = "Testkey";
            ConectoWorkSpace.InstallB52.WindowSetkey();

        }

        // Включить выключить локальный ключ 
        private void LocKeyOnOff_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            Administrator.AdminPanels.SetWinSetHub = "Lockey";
            ConectoWorkSpace.InstallB52.WindowSetkey();
  
        }

        private void Zip_7_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            AdminPanels.Arh7Zip();
        }

        public static void Arh7Zip()
        {
            string str = SystemConectoServers.PutchConecto + "7-Zip\\";
            SystemConecto.DIR_(str, 0);
            if (!System.IO.File.Exists("c:\\Program Files (x86)\\7-Zip\\7z.exe"))
            {
                InstallB52.MessageEnd("Выполняется установка программы" + Environment.NewLine + "Пожалуйста подождите. ", 1, -170, 400);
                Dictionary<string, string> fbembedList = new Dictionary<string, string>();
                fbembedList.Add(str + "7z1900.exe", "7z/");
                if (SystemConecto.IsFilesPRG(fbembedList, -1, "- Проверка файлов во время установки " + str) != "True")
                {
                    MessageBox.Show("Не загрузился инсталяционный файл установки с ФТП сервера" + Environment.NewLine + str + "7z1900.exe" + Environment.NewLine + "  Установка прекращена.  " + Environment.NewLine +
                        " Проверте наличие интернет доступа и повторите попытку");
                    return;
                }
            }
            Process.Start(new ProcessStartInfo(str + "7z1900.exe")
            {
                CreateNoWindow = true
            });
        }

        // Включить выключить сетевой ключ 
        private void NetKeyOnOff_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            Administrator.AdminPanels.SetWinSetHub = "Netkey";
            ConectoWorkSpace.InstallB52.WindowSetkey();

        }

        private void RunSaveConf_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {

        }

        private void UpgradeBD30_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (AdminPanels.SetPuth30 == "")
            {
                int num = 0;
                string cmdText = "select * from SERVERACTIVFB30 where SERVERACTIVFB30.NAME = '" + AdminPanels.NameServer30 + "'";
                DBConecto.DBopenFBConectionMemory(SystemBD.ConnnStringSystem("Dynamic").ToString(), "", "FbSystem");
                FbCommand fbCommand = new FbCommand(cmdText, (FbConnection)DBConecto.bdFbSystemConect);
                FbDataReader fbDataReader = fbCommand.ExecuteReader();
                while (fbDataReader.Read())
                {
                    AdminPanels.SetPort30 = fbDataReader[0].ToString();
                    AdminPanels.SetPuth30 = fbDataReader[1].ToString();
                    AdminPanels.SetActiv30 = fbDataReader[4].ToString();
                    AdminPanels.CurrentPasswFb30 = fbDataReader[5].ToString();
                    ++num;
                }
                fbDataReader.Close();
                if (num == 0)
                {
                    MainWindow.MessageInstall("Cервер не активный " + Environment.NewLine + "Обновление прекращено. ", 1, -400, 300);
                    fbCommand.Dispose();
                    DBConecto.DBcloseFBConectionMemory("FbSystem");
                    return;
                }
                fbCommand.Dispose();
                DBConecto.DBcloseFBConectionMemory("FbSystem");
            }
            if (AdminPanels.SetActiv30 == "Stop")
            {
                MainWindow.MessageInstall("Cервер не активный " + Environment.NewLine + "Обновление прекращено. ", 1, -400, 300);
            }
            else
            {
 
                
                this.UpgradeBD30.Foreground = (Brush)Brushes.Indigo;
                this.FalseIsEnabled30();
                AdminPanels.NameObj = "UpgradeBD30";
                var dlg = new System.Windows.Forms.FolderBrowserDialog();
                dlg.Description = "Путь размещения Обновления";
                System.Windows.Forms.DialogResult result = dlg.ShowDialog();
                if (result == System.Windows.Forms.DialogResult.OK)
                {
                    FolderBackPuth = dlg.SelectedPath + (dlg.SelectedPath.LastIndexOf(@"\") <= 2 && dlg.SelectedPath.Length <= 3 ? "" : @"\");
                //    TextBox_Fbd30.Text = FolderBackPuth;
             
                //AdminPanels.PuthUpgradeBD30 = AdminPanels.FolderBackPuth;
                    AppStart.RenderInfo renderInfo = new AppStart.RenderInfo();
                    renderInfo.argument1 = "1";
                    Thread thread = new Thread(new System.Threading.ParameterizedThreadStart(this.UpgradeBD25TH));
                    thread.SetApartmentState(ApartmentState.STA);
                    thread.IsBackground = true;
                    thread.Start((object)renderInfo);
                    ++InstallB52.IntThreadStart;
                    WaitMessage waitMessage = new WaitMessage();
                    waitMessage.Owner = (Window)this;
                    waitMessage.Top = (double)SystemConecto.WorkAreaDisplayDefault[0] - 700.0;
                    waitMessage.Left = (double)SystemConecto.WorkAreaDisplayDefault[1] - 300.0;
                    waitMessage.Show();
                }
                else
                {
                    this.UpgradeBD30.Foreground = (Brush)Brushes.White;
                    this.TablPuthBD30.IsEnabled = true;
                }
            }
        }

        private void InitPanel_BusBd(object sender, MouseButtonEventArgs e)
        {
            if (ConectoWorkSpace.Bus.AdminBus.CheckBUSBD() <= 0)
                return;
            MainWindow.MessageInstall("БД отсутствует." + Environment.NewLine + " Работа прекращена.", 1, -400, 300);
            Environment.Exit(0);
        }

        // Включить выключить установку сервера сетевого  ключа 
        private void SetLocKeyOnOff_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            Administrator.AdminPanels.NameObj = ImageObj = "SetLocKeyOnOff";
            Interface.ObjektOnOff("SetLocKeyOnOff", ref SetLocKeyOnOff, new Install.Metods { NameClass = "ConectoWorkSpace.InstallB52", Install = "InstallTHLocKey", UnInstal = "UnInstallTHLocKey" });
        }

        // Включить выключить установку сервера сетевого  ключа 
        private void BackOfServKeyOnOff_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            Administrator.AdminPanels.NameObj = ImageObj = "BackOfServKeyOnOff";
            Interface.ObjektOnOff("BackOfServKeyOnOff", ref BackOfServKeyOnOff, new Install.Metods { NameClass = "ConectoWorkSpace.InstallB52", Install = "InstallTHKeyServ", UnInstal = "UnInstallKeyServ" });
        }

  
        private void DeleteTestkeyOnOff_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            string StrDelete = "DELETE FROM TESTKEY where TESTKEY.PUTH = " + "'" + ConectoWorkSpace.Administrator.AdminPanels.TestKeyPuth + "'";
            DeleteGridKey(StrDelete, "SELECT * from TESTKEY", "TESTKEY");


        }

  
        private void DeleteLockeyOnOff_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            string StrDelete = "DELETE  FROM LOCKEY where LOCKEY.PUTH = " + "'" + ConectoWorkSpace.Administrator.AdminPanels.LocKeyPuth + "'";
            DeleteGridKey(StrDelete, "SELECT * from LOCKEY", "LOCKEY");


        }

 

        private void DeleteNetkeyOnOff_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            string StrDelete = "DELETE FROM NETKEY where NETKEY.PUTH = " + "'" + ConectoWorkSpace.Administrator.AdminPanels.NetKeyPuth + "'";
            DeleteGridKey(StrDelete, "SELECT * from NETKEY", "NETKEY");


        }

        // Процеда удаления строк из таблиц тестового локального и сетевого ключей
        public void DeleteGridKey(string StrDelete, string LoadGrid, string Tablebd)
        {
            DBConecto.DBopenFBConectionMemory(SystemBD.ConnnStringSystem().ToString(), "", "FbSystem");
            DBConecto.UniQuery DeletQuery = new DBConecto.UniQuery(StrDelete, "FB");
            DeletQuery.UserQuery = string.Format(StrDelete, Tablebd);
            DeletQuery.ExecuteUNIScalar();
            DBConecto.DBcloseFBConectionMemory("FbSystem");
            Administrator.AdminPanels.Grid_key(LoadGrid);
            ChangeGnclient(TestKeyPuth.Substring(0, TestKeyPuth.LastIndexOf(@"\")+1), AppStart.TableReestr["BackOfAdresPorta"], AppStart.TableReestr["SetTextAdrDataIp4BackOf"]);

            DelNetKey.IsEnabled = false;
            GhangeNetKey.IsEnabled = false;
            DelLocKey.IsEnabled = false;
            GhangeLocKey.IsEnabled = false;
            DelTestKey.IsEnabled = false;
            GhangeTestKey.IsEnabled = false;
        }
        // Процедура редактирования файла gnclient.ini
        private void ChangeTestkeyOnOff_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            NotepadIni(PuthGnclient, "gnclient.ini");
        }

  

        // Процедура загрузки таблицы фронтов и беков работающих под сетевым ключом
        private void GridNetKey_Loaded(object sender, RoutedEventArgs e)
        {
            Grid_key("SELECT * from NETKEY");
        }

  

        // Процедура загрузки таблицы фронтов и беков работающих под локальным ключом
        private void GridLocKey_Loaded(object sender, RoutedEventArgs e)
        {
            Grid_key("SELECT * from LOCKEY");
        }

  

        // Процедура загрузки таблицы фронтов и беков работающих под тестовым ключом
        private void GridTestKey_Loaded(object sender, RoutedEventArgs e)
        {
            Grid_key("SELECT * from TESTKEY");
        }


        public static void Grid_key(string CurrentSetBD)
        {
            List<GnclientLocKey> resultLocKey   = new List<GnclientLocKey>(1);
            List<GnclientNetKey> resultNetKey   = new List<GnclientNetKey>(1);
            List<GnclientTestKey> resultTestKey = new List<GnclientTestKey>(1);
            List<ActivBackFront> resultBackFront = new List<ActivBackFront>(1);
            DBConecto.DBopenFBConectionMemory(SystemBD.ConnnStringSystem().ToString(), "", "FbSystem");
            int IndStroka = 1;
            FbCommand UpdateKey = new FbCommand(CurrentSetBD, DBConecto.bdFbSystemConect);
            UpdateKey.CommandType = CommandType.Text;
            FbDataReader reader = UpdateKey.ExecuteReader();
            while (reader.Read())
            {
                if (CurrentSetBD.Contains("NETKEY")) resultNetKey.Add(new GnclientNetKey(reader[0].ToString(), reader[1].ToString(), reader[2].ToString(), reader[3].ToString(), reader[4].ToString(), reader[5].ToString(), reader[6].ToString()));
                if (CurrentSetBD.Contains("LOCKEY")) resultLocKey.Add(new GnclientLocKey(reader[0].ToString(), reader[1].ToString(), reader[2].ToString(), reader[3].ToString(), reader[4].ToString(), reader[5].ToString(), reader[6].ToString()));
                if (CurrentSetBD.Contains("TESTKEY")) resultTestKey.Add(new GnclientTestKey(reader[0].ToString(), reader[1].ToString(), reader[2].ToString(), reader[3].ToString(), reader[4].ToString(), reader[5].ToString(), reader[6].ToString()));
                if (CurrentSetBD.Contains("ACTIV")) resultBackFront.Add(new ActivBackFront(reader[0].ToString(), reader[1].ToString(), reader[2].ToString(), reader[3].ToString(), reader[4].ToString(), reader[5].ToString(), reader[6].ToString()));
                IndStroka++;
            }
            reader.Close();
            UpdateKey.Dispose();
            DBConecto.DBcloseFBConectionMemory("FbSystem");
            System.Windows.Application.Current.Dispatcher.Invoke(new Action(delegate ()
            {
                AdminPanels ConectoWorkSpace_InW = AppStart.LinkMainWindow("WAdminPanels");
                if (CurrentSetBD.Contains("NETKEY"))ConectoWorkSpace_InW.GridNetKey.ItemsSource = resultNetKey;
                if (CurrentSetBD.Contains("LOCKEY")) ConectoWorkSpace_InW.GridLocKey.ItemsSource = resultLocKey;
                if (CurrentSetBD.Contains("TESTKEY")) ConectoWorkSpace_InW.GridTestKey.ItemsSource = resultTestKey;
            }));

            System.Windows.Application.Current.Dispatcher.Invoke(new Action(delegate ()
            {
                WinSetKey ConectoWorkSpace_InW = AppStart.LinkMainWindow("WinSetKeyW");
                if (CurrentSetBD.Contains("ACTIV")) ConectoWorkSpace_InW.GridBackFront.ItemsSource = resultBackFront;
            }));
        }
  
        private void GridNetKey_MouseUp(object sender, MouseButtonEventArgs e)
        {
            GridKeyMouseUp("SELECT count(*) from NETKEY", 1);
        }

        private void GridLocKey_MouseUp(object sender, MouseButtonEventArgs e)
        {
            GridKeyMouseUp("SELECT count(*) from LOCKEY", 2);
        }

        private void RunLocBD30_MouseLeftButtonUp_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {

        }

        private void GridTestKey_MouseUp(object sender, MouseButtonEventArgs e)
        {
            GridKeyMouseUp("SELECT count(*) from TESTKEY", 3);
        }


        public void GridKeyMouseUp(string InsertExecute, int NumberKeyGrid )
        {
            string GnclientTcpIp = "";
            DBConecto.DBopenFBConectionMemory(SystemBD.ConnnStringSystem().ToString(), "", "FbSystem");
            DBConecto.UniQuery CountQuery = new DBConecto.UniQuery(InsertExecute, "FB");
            string CountTable = CountQuery.ExecuteUNIScalar() == null ? "" : CountQuery.ExecuteUNIScalar().ToString();

            if (Convert.ToUInt32(CountTable) != 0)
            {
                switch (NumberKeyGrid)
                {
                    case 1:
                        if (GridNetKey.SelectedItem != null )
                        {
                            GnclientNetKey path = GridNetKey.SelectedItem as GnclientNetKey;
                            NetKeyType   = path.Type;
                            NetKeyName   = path.Name;
                            NetKeyServer = path.Server;
                            NetKeyConect = path.Conect;
                            NetKeyPuth   = path.Puth;
                            NetKeyPort   = path.Port;
                            NetKeyTcpIp = path.TcpIp;
                            DelNetKey.IsEnabled = true;
                            GhangeNetKey.IsEnabled = true;
                            BackOfAdresPorta.Text = NetKeyPort;
                            GnclientTcpIp = path.TcpIp;
                            PuthGnclient = path.Puth;
                            Administrator.AdminPanels.SetWinSetHub = "NETKEY";
                            GnclientServer.IsEnabled = true;

                        }
                        break;
                    case 2:
                        if (GridLocKey.SelectedItem != null)
                        {
                            GnclientLocKey path = GridLocKey.SelectedItem as GnclientLocKey;
                            LocKeyType   = path.Type;
                            LocKeyName   = path.Name;
                            LocKeyServer = path.Server;
                            LocKeyConect = path.Conect;
                            LocKeyPuth   = path.Puth;
                            LocKeyPort = path.Port;
                            LocKeyTcpIp = path.TcpIp;
                            DelLocKey.IsEnabled = true;
                            GhangeLocKey.IsEnabled = true;
                            BackOfAdresPorta.Text = LocKeyPort;
                            GnclientTcpIp = path.TcpIp;
                            PuthGnclient = path.Puth;
                            Administrator.AdminPanels.SetWinSetHub = "LOCKEY";
                            GnclientServer.IsEnabled = true;
                        }
                        break;
                    case 3:
                        if (GridTestKey.SelectedItem != null)
                        {
                            GnclientTestKey path = GridTestKey.SelectedItem as GnclientTestKey;
                            TestKeyType   = path.Type;
                            TestKeyName   = path.Name;
                            TestKeyServer = path.Server;
                            TestKeyConect = path.Conect;
                            TestKeyPuth   = path.Puth;
                            TestKeyPort = path.Port;
                            TestKeyTcpIp = path.TcpIp;
                            DelTestKey.IsEnabled = true;
                            GhangeTestKey.IsEnabled = true;
                            BackOfAdresPorta.Text = TestKeyPort;
                            GnclientTcpIp = path.TcpIp;
                            PuthGnclient = path.Puth;
                            Administrator.AdminPanels.SetWinSetHub = "TESTKEY";
                            GnclientServer.IsEnabled = true;
                        }
                        break;
                    case 4:
                        System.Windows.Application.Current.Dispatcher.Invoke(new Action(delegate ()
                        {
                            WinSetKey ConectoWorkSpace_InW = AppStart.LinkMainWindow("WinSetKeyW");
                            if (ConectoWorkSpace_InW.GridBackFront.SelectedItem != null)
                            {
                                ActivBackFront path = ConectoWorkSpace_InW.GridBackFront.SelectedItem as ActivBackFront;
                                TestKeyType = path.Type;
                                TestKeyName = path.Name;
                                TestKeyServer = path.Server;
                                TestKeyConect = path.Conect;
                                TestKeyPuth = path.Puth;
                            }
                        }));
                        break;

                }
                if (NumberKeyGrid != 4)
                {
                    BackOfAdrOnOff_IP4.IsEnabled = true;
                    for (int indPoint = 1; indPoint <= 3; indPoint = indPoint + 1)
                    {
                        int position = GnclientTcpIp.IndexOf(".");
                        if (position <= 0) { break; }
                        switch (indPoint)
                        {
                            case 1:
                                BackOfAdrDataIp4Text1.Text = GnclientTcpIp.Substring(0, position);
                                break;
                            case 2:
                                BackOfAdrDataIp4Text2.Text = GnclientTcpIp.Substring(0, position);
                                break;
                            case 3:
                                BackOfAdrDataIp4Text3.Text = GnclientTcpIp.Substring(0, position);
                                break;
                        }
                        GnclientTcpIp = GnclientTcpIp.Substring(position + 1);
                    }
                    BackOfAdrDataIp4Text4.Text = GnclientTcpIp.Substring(0);
                }
            }
            DBConecto.DBcloseFBConectionMemory("FbSystem");
        }



        #endregion


        #region Закладка "Службы БД"
        // Процедура подготовки массива переключателей панели серевера данных и
        // отображения текущего состояния этих переключателей на панели. 
        private void InitPanel_LostFocus(object sender, RoutedEventArgs e)
        {
            //StartServisBD = 1;
        }

        private void InitPanel_ServisBD(object sender, MouseButtonEventArgs e)
        {
            InitPanelServisBD();
        }

        private void InitPanelServisBD()
        {

            if (StartServisBD == 1)
            {
                VisibleClosedServisBD();
                var TextWindows = "Выполняется идентификация данных." + Environment.NewLine + "Пожалуйста подождите. ";
                int AutoClose = 1;
                int MesaggeTop = -170;
                int MessageLeft = 300;
                ConectoWorkSpace.MainWindow.MessageEnd(TextWindows, AutoClose, MesaggeTop, MessageLeft);

                InitKeyOnOffServisBD();
                InitPanel_();
                InitTextServisBD();
                Interface.CurrentStateInst("OnOffLog", "0", "on_off_1.png", OnOffLog);
                Interface.CurrentStateInst("Satellite", "0", "on_off_1.png", Satellite);
                datePicker25_CopyLogP.SelectedDate = DateTime.Now;
                datePicker25_CopyLogS.SelectedDate = DateTime.Now.Subtract(new TimeSpan(40, 0, 0, 0));//Вычитаем 40 дня; 
                datePicker30_CopyLogP.SelectedDate = DateTime.Now;
                datePicker30_CopyLogS.SelectedDate = DateTime.Now.Subtract(new TimeSpan(40, 0, 0, 0));//Вычитаем 40 дня;
                StartServisBD = 0;
            }
            
 
            Interface.CurrentStateInst("SetBD25", Convert.ToInt32(AppStart.TableReestr["SetPuthBD25"].ToString()) == 2?  "2" : "0", Convert.ToInt32(AppStart.TableReestr["SetPuthBD25"].ToString()) == 2? "on_off_2.png": "on_off_1.png", SetBD25);
            Interface.CurrentStateInst("SetBD30", Convert.ToInt32(AppStart.TableReestr["SetPuthBD30"].ToString()) == 2 ? "2" : "0", Convert.ToInt32(AppStart.TableReestr["SetPuthBD30"].ToString()) == 2 ? "on_off_2.png" : "on_off_1.png", SetBD30);
            Interface.CurrentStateInst("SetPostGreSQL", Convert.ToInt32(AppStart.TableReestr["SetBDPostGreSQL"].ToString()) == 2 ? "2" : "0", Convert.ToInt32(AppStart.TableReestr["SetBDPostGreSQL"].ToString()) == 2 ? "on_off_2.png" : "on_off_1.png", SetPostGreSQL);
            Interface.CurrentStateInst("SetMsSQL", Convert.ToInt32(AppStart.TableReestr["SetBDMsSQL"].ToString()) == 2 ? "2" : "0", Convert.ToInt32(AppStart.TableReestr["SetBDMsSQL"].ToString()) == 2 ? "on_off_2.png" : "on_off_1.png", SetMsSQL);
            
        }


        public void VisibleClosedServisBD()
        {
            OnOffSborka.Visibility = Visibility.Collapsed;
            LabelSborka.Visibility = Visibility.Collapsed;
            CleanBD25_Label.Visibility = Visibility.Collapsed;
            Combo_Label.Visibility = Visibility.Collapsed;
            ComboFiltr.Visibility = Visibility.Collapsed;
            datePicker25_Server.Visibility = Visibility.Collapsed;
            datePicker25_Server.IsDropDownOpen = false;
            LocDisk.Visibility = Visibility.Collapsed;
            GoogleDisk.Visibility = Visibility.Collapsed;
            FtpDisk.Visibility = Visibility.Collapsed;
            NameArhiv.Visibility = Visibility.Collapsed;
            Putch_Fbd25.Visibility = Visibility.Collapsed;
            DirPath_Fbd25.Visibility = Visibility.Collapsed;
            BackUpLocServerBD25.Visibility = Visibility.Collapsed;
            LocDisk_30.Visibility = Visibility.Collapsed;
            GoogleDisk_30.Visibility = Visibility.Collapsed;
            FtpDisk_30.Visibility = Visibility.Collapsed;
            NameArhiv30.Visibility = Visibility.Collapsed;
            Putch_Fbd30.Visibility = Visibility.Collapsed;
            DirPath_Fbd30.Visibility = Visibility.Collapsed;
            BackUpLocServerBD30.Visibility = Visibility.Collapsed;
            NameArhivBd2.Visibility = Visibility.Collapsed;
            SetArhivBd.Visibility = Visibility.Collapsed;
            ArhivSetDay.Visibility = Visibility.Collapsed;
            SetArhivBd2.Visibility = Visibility.Collapsed;
            SetArhivBd3.Visibility = Visibility.Collapsed;
            Hour.Visibility = Visibility.Collapsed;
            sep1.Visibility = Visibility.Collapsed;
            Minute.Visibility = Visibility.Collapsed;
            SetArhivBd25.Visibility = Visibility.Collapsed;
            ArhivSetDay25.Visibility = Visibility.Collapsed;
            SetArhivBd225.Visibility = Visibility.Collapsed;
            SetArhivBd325.Visibility = Visibility.Collapsed;
            Hour25.Visibility = Visibility.Collapsed;
            sep125.Visibility = Visibility.Collapsed;
            Minute25.Visibility = Visibility.Collapsed;
            NameArhivBd25.Visibility = Visibility.Collapsed;
            Delete_Str_Backup.IsEnabled = false;
            Change_Str_Backup.IsEnabled = false;
            Change_Str_Backup30.IsEnabled = false;
            Delete_Str_Backup30.IsEnabled = false;
            PuthArhiv_PostGreSQL.Visibility = Visibility.Collapsed;
            Putch_PostGreSQL.Visibility = Visibility.Collapsed;
            DirPath_PostGreSQL.Visibility = Visibility.Collapsed;
            SetArhivBd_PostGreSQL.Visibility = Visibility.Collapsed;
            ArhivSetDay_PostGreSQL.Visibility = Visibility.Collapsed;
            Hour_PostGreSQL.Visibility = Visibility.Collapsed;
            Day_PostGreSQL.Visibility = Visibility.Collapsed;
            Time_PostGreSQL.Visibility = Visibility.Collapsed;
            sep1_PostGreSQL.Visibility = Visibility.Collapsed;
            Minute_PostGreSQL.Visibility = Visibility.Collapsed;
            LabelRun_PostGreSQL.Visibility = Visibility.Collapsed;
            BackUpLocBD_PostGreSQL.Visibility = Visibility.Collapsed;
            ListArhivBd_PostGreSQL.Visibility = Visibility.Collapsed;
            LocDisk_PostGreSQL.Visibility = Visibility.Collapsed;
            GoogleDisk_PostGreSQL.Visibility = Visibility.Collapsed;
            FtpDisk_PostGreSQL.Visibility = Visibility.Collapsed;
            datePicker30_Server.Visibility = Visibility.Collapsed;
            CleanBD30_Label.Visibility = Visibility.Collapsed;
            Combo_Label30.Visibility = Visibility.Collapsed;
            ComboFiltr30.Visibility = Visibility.Collapsed;
            OnOffSborka30.Visibility = Visibility.Collapsed;
            LabelSborka30.Visibility = Visibility.Collapsed;
            CopyLogBD25.Visibility = Visibility.Collapsed;
            datePicker25_CopyLogS.Visibility = Visibility.Collapsed;
            datePicker25_CopyLogP.Visibility = Visibility.Collapsed;
            CopyLogPo.Visibility = Visibility.Collapsed;
            CopyLogBD30.Visibility = Visibility.Collapsed;
            datePicker30_CopyLogS.Visibility = Visibility.Collapsed;
            datePicker30_CopyLogP.Visibility = Visibility.Collapsed;
            CopyLogPo30.Visibility = Visibility.Collapsed;
        }



        public static void InitKeyOnOffServisBD()
        {
            AdminPanels.ButtonPanel = new string[12];

            //AdminPanels.ButtonPanel[0] = "SetPuthBDPostGreSQL_User";
            AdminPanels.ButtonPanel[0] = "SetPuthBDMsSQL_User";
            //AdminPanels.ButtonPanel[1] = "BackUpLocServerBD25";
            AdminPanels.ButtonPanel[1] = "SetMsSQL";

            //AdminPanels.ButtonPanel[2] = "BackUpLocServerBD30";
 
            //AdminPanels.ButtonPanel[5] = "BackUpLocServerBDPostGreSQL";
            //AdminPanels.ButtonPanel[6] = "BackUpOnOffFTPBDPostGreSQL";
            //AdminPanels.ButtonPanel[7] = "BackUpOnOffBDPostGreSQLGoogleDrive";
            AdminPanels.ButtonPanel[2] = "BackUpLocServerBDMsSQL";
            AdminPanels.ButtonPanel[3] = "BackUpOnOffFTPBDMsSQL";
            AdminPanels.ButtonPanel[4] = "BackUpGoogleDriveOnOff_MsSQL";
 
            AdminPanels.ButtonPanel[5] = "SetBD25";
            AdminPanels.ButtonPanel[6] = "SetBD30";
            AdminPanels.ButtonPanel[7] = "SetPostGreSQL";
            AdminPanels.ButtonPanel[8] = "OnOffLog";
            AdminPanels.ButtonPanel[9] = "Satellite";
            AdminPanels.ButtonPanel[10] = "OnOffLog30";
            AdminPanels.ButtonPanel[11] = "Satellite30";





            if (LoadTableRestr == 0)InitKeySystemFB("InitKey");
            InitKeyOnOff();
        }

        public static void InitTextServisBD()
        {
            AdminPanels.ButtonPanel = new string[21];

            AdminPanels.ButtonPanel[0] = "PuthSetBDPostGreSQL_User";
            AdminPanels.ButtonPanel[1] = "PuthSetBDMsSQL_User";
            AdminPanels.ButtonPanel[2] = "PutchBackUpLoc_Fbd25";
            AdminPanels.ButtonPanel[3] = "PutchBackUpLoc_Fbd30";
            AdminPanels.ButtonPanel[4] = "PutchBackUpLocPostGreSQL";
            AdminPanels.ButtonPanel[5] = "PutchBackUpLocMsSQL";
            AdminPanels.ButtonPanel[6] = "Putch_Fbd25";
  
            AdminPanels.ButtonPanel[7] = "PutchBackUpFTP_PostGreSQL";
            AdminPanels.ButtonPanel[8] = "PutchBackUpFTP_MsSQL";
              AdminPanels.ButtonPanel[9] = "PutchBackUpGoogleDrive_PostGreSQL";
            AdminPanels.ButtonPanel[10] = "PutchBackUpGoogleDrive_MsSQL";
            AdminPanels.ButtonPanel[11] = "OldPasswABD25";
            AdminPanels.ButtonPanel[12] = "OldPasswABD30";
            AdminPanels.ButtonPanel[13] = "DateClean_Fbd25";
            AdminPanels.ButtonPanel[14] = "DateClean_Fbd30";
            AdminPanels.ButtonPanel[15] = "CurrentPasswABD25";
            AdminPanels.ButtonPanel[16] = "CurrentPasswABD30";
 
            AdminPanels.ButtonPanel[17] = "PutchRestoryGoogle_Fbd30";
            AdminPanels.ButtonPanel[18] = "SetDirBackUp25";
            AdminPanels.ButtonPanel[19] = "SetDirBackUp30";
            AdminPanels.ButtonPanel[20] = "PutchBackUpFtp_Fbd25";
            


            if (LoadTableRestr == 0)InitKeySystemFB("InitText");
            InitTextOnOff();
        }
        //Пеprеход на новый отчетный период
        private void GoNewPeriod_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            hubdate = "25";
            if (Administrator.AdminPanels.SetWinSetHub == "GoNewPeriod")
            {
                datePicker25_Server.Visibility = Visibility.Collapsed;
                CleanBD25_Label.Visibility = Visibility.Collapsed;
                Combo_Label.Visibility = Visibility.Collapsed;
                ComboFiltr.Visibility = Visibility.Collapsed;
                OnOffSborka.Visibility = Visibility.Collapsed;
                LabelSborka.Visibility = Visibility.Collapsed;
                GoNewPeriod.Foreground = Brushes.White;
                Administrator.AdminPanels.SetWinSetHub = "";
            }
            else
            {
                Administrator.AdminPanels.SetWinSetHub = "GoNewPeriod";
                RestoryBd25.IsEnabled = ChangeFbd2530.IsEnabled = CreateListArhivBd25.IsEnabled = CreateBackUpBd25.IsEnabled = false;
                datePicker25_Server.Visibility = Visibility.Visible;
                CleanBD25_Label.Visibility = Visibility.Visible;
                Combo_Label.Visibility = Visibility.Visible;
                ComboFiltr.Visibility = Visibility.Visible;
                LabelSborka.Visibility = Visibility.Visible;
                OnOffSborka.Visibility = Visibility.Visible;
                GoNewPeriod.Foreground = Brushes.Indigo;
                GoNewPeriod.IsEnabled = true;
            }
        }

        private void GoNewPeriod30_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            hubdate = "30";
            if (Administrator.AdminPanels.SetWinSetHub == "GoNewPeriod30")
            {
                datePicker30_Server.Visibility = Visibility.Collapsed;
                CleanBD30_Label.Visibility = Visibility.Collapsed;
                Combo_Label30.Visibility = Visibility.Collapsed;
                ComboFiltr30.Visibility = Visibility.Collapsed;
                OnOffSborka30.Visibility = Visibility.Collapsed;
                LabelSborka30.Visibility = Visibility.Collapsed;
                GoNewPeriod30.Foreground = Brushes.White;
                Administrator.AdminPanels.SetWinSetHub = "";
            }
            else
            {
                Administrator.AdminPanels.SetWinSetHub = "GoNewPeriod30";
                RestoryBd30.IsEnabled =  CreateListArhivBd30.IsEnabled = CreateBackUpBd30.IsEnabled = RestoryBd30.IsEnabled = false;
                datePicker30_Server.Visibility = Visibility.Visible;
                CleanBD30_Label.Visibility = Visibility.Visible;
                Combo_Label30.Visibility = Visibility.Visible;
                ComboFiltr30.Visibility = Visibility.Visible;
                LabelSborka30.Visibility = Visibility.Visible;
                OnOffSborka30.Visibility = Visibility.Visible;
                GoNewPeriod30.Foreground = Brushes.Indigo;
                GoNewPeriod30.IsEnabled = true;
            }
        }

        // Строка заголовка Расписания архивации

        public void LabelServisBd_Loaded(object sender, RoutedEventArgs e)
        {

            Label foo = new Label();
            foo.Width = 705;
            foo.Height = 28;
            foo.Name = "SetDey";
            foo.HorizontalAlignment = HorizontalAlignment.Left;
            foo.VerticalAlignment = VerticalAlignment.Top;
            foo.HorizontalContentAlignment = HorizontalAlignment.Center;
            foo.Margin = new Thickness(50, 50, 0, 0);
            foo.Content = "Настройка расписания времени создания резервной копии базы данных";
            foo.FontFamily = new FontFamily("Courier New"); ;
            foo.FontSize = 15;
            foo.FontWeight = FontWeights.Normal;
            foo.Foreground = Brushes.Black;
            foo.Background = Brushes.DarkGray;
            foo.IsEnabled = true;
            //foo.MouseLeftButtonUp += new MouseButtonEventHandler(ConectoWorkSpace.Administrator.AdminPanels.LocDisk);
            //foo.FontStyle = FontStyles.Italic;
            foo.Visibility = Visibility.Visible;
            AddLabel(foo);

            // Создание кнопки Комбобокса задающей интервал создания архива
            string[] str = new string[] { "Каждый день", "Через 10 дней", "Через 20 дней", };           
            ComboBox SetDayArhiv = new ComboBox();
            SetDayArhiv.Width = 150;
            SetDayArhiv.Height = 30;
            SetDayArhiv.Name = "DayArhiv";
            SetDayArhiv.HorizontalAlignment = HorizontalAlignment.Left;
            SetDayArhiv.VerticalAlignment = VerticalAlignment.Top;
            SetDayArhiv.HorizontalContentAlignment = HorizontalAlignment.Center;
            SetDayArhiv.Margin = new Thickness(50, 90, 0, 0);
            SetDayArhiv.FontFamily = new FontFamily("Courier New"); ;
            SetDayArhiv.FontSize = 15;
            SetDayArhiv.FontWeight = FontWeights.Normal;
            SetDayArhiv.Foreground = Brushes.Black;
            SetDayArhiv.Background = Brushes.DarkGray;
            SetDayArhiv.IsEnabled = true;
            SetDayArhiv.SelectionChanged += new SelectionChangedEventHandler(SetArhiv);
            SetDayArhiv.ItemsSource = str;
            SetDayArhiv.SelectedIndex = 0;
            SetDayArhiv.Visibility = Visibility.Visible;
            AddLabel(SetDayArhiv);

            // Создание кнопки Комбобокса задающей устройство на котором необходимо создать архив
            string[] Devicestr = new string[] { "Диск компьютера", "ФТП сервер", "Google Disk", };
            ComboBox DeviceArhiv = new ComboBox();
            DeviceArhiv.Width = 170;
            DeviceArhiv.Height = 30;
            DeviceArhiv.Name = "DayArhiv";
            DeviceArhiv.HorizontalAlignment = HorizontalAlignment.Left;
            DeviceArhiv.VerticalAlignment = VerticalAlignment.Top;
            DeviceArhiv.HorizontalContentAlignment = HorizontalAlignment.Center;
            DeviceArhiv.Margin = new Thickness(210, 90, 0, 0);
            DeviceArhiv.FontFamily = new FontFamily("Courier New"); ;
            DeviceArhiv.FontSize = 15;
            DeviceArhiv.FontWeight = FontWeights.Normal;
            DeviceArhiv.Foreground = Brushes.Black;
            DeviceArhiv.Background = Brushes.DarkGray;
            DeviceArhiv.IsEnabled = true;
            DeviceArhiv.SelectionChanged += new SelectionChangedEventHandler(SetDevice);
            DeviceArhiv.ItemsSource = Devicestr;
            DeviceArhiv.SelectedIndex = 0;
            DeviceArhiv.Visibility = Visibility.Visible;
            AddLabel(DeviceArhiv);




            //TextBlock textBlock = new TextBlock(new Run("A bit of text content..."));

            //textBlock.Background = Brushes.AntiqueWhite;
            //textBlock.Foreground = Brushes.Navy;

            //textBlock.FontFamily = new FontFamily("Century Gothic");
            //textBlock.FontSize = 12;
            //textBlock.FontStretch = FontStretches.UltraExpanded;
            //textBlock.FontStyle = FontStyles.Italic;
            //textBlock.FontWeight = FontWeights.UltraBold;

            //textBlock.LineHeight = Double.NaN;
            //textBlock.Padding = new Thickness(5, 10, 5, 10);
            //textBlock.TextAlignment = TextAlignment.Center;
            //textBlock.TextWrapping = TextWrapping.Wrap;

            //textBlock.Typography.NumeralStyle = FontNumeralStyle.OldStyle;
            //textBlock.Typography.SlashedZero = true;


        }

  

        public void AddLabel(UIElement label)
        {
            Grid.SetColumn(label, 0);
            Grid.SetRow(label, 1);
            GridFbd25.Children.Add(label);

        }

 

        public void SetArhiv(object sender, EventArgs e)
        {
        }

  

        public void SetDevice(object sender, EventArgs e)
        {
        }


        private void InitPuthRestory(string SetPuth, string SetOnOff, TextBox PutchRestory)
        {
            PutchRestory.Text = AppStart.TableReestr[SetPuth]; 
            PutchRestory.IsEnabled = false;
        }

        // Выбор директории где куда необходимо записать архив по событию clck включения переключателя локальное размещение архива
        public static void PathFileBD_Click()
        {

            var openFileDialog = new System.Windows.Forms.OpenFileDialog();
            {
                openFileDialog.InitialDirectory = "*";
                openFileDialog.Filter = "gdb files (*.gdb)|*.gdb|fdb files (*.fdb)|*.fdb|txt files (*.txt)|*.txt|All files (*.*)|*.*";
                openFileDialog.FilterIndex = 4;
                openFileDialog.RestoreDirectory = true;

                if (openFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK) {PathFileBDText = openFileDialog.FileName;}
                else {PathFileBDText = ""; }
            }
  
        }






        // Выбор директории где куда необходимо записать архив по событию clck включения переключателя локальное размещение архива
        private void DirPathBuckUp_Click(string KeyOb, TextBox Puth)
        {

            if (KeyOb == "PutchBackUpLoc_Fbd25" || KeyOb == "PutchBackUpLoc_Fbd30")
            {


                string MemDirSet = KeyOb == "PutchBackUpLoc_Fbd25" ? "SetDirBackUp25" : "SetDirBackUp30";
                ItemTextBoxPutchBackUpLoc =  @"c:\BackupFdb\";
                Puth.Text = @"c:\BackupFdb\" + SelectPuth.Substring(SelectPuth.LastIndexOf(@"\") + 1, (SelectPuth.LastIndexOf(".") - (SelectPuth.LastIndexOf(@"\") + 1))) + DateTime.Now.ToString("yyMMddHHmmss") + ".fbk"; 
                Administrator.AdminPanels.UpdateKeyReestr(KeyOb, Puth.Text);
                Administrator.AdminPanels.UpdateKeyReestr(MemDirSet, ItemTextBoxPutchBackUpLoc);
    
            }
            // конвертация 2.5-3.0
            if (KeyOb == "PutchBackUpRestorybd2530")
            {
                Puth.Text = SelectPuth.Substring(0, SelectPuth.LastIndexOf(@"\") + 1) + SelectPuth.Substring(SelectPuth.LastIndexOf(@"\") + 1, SelectPuth.IndexOf(@".") - (SelectPuth.LastIndexOf(@"\") + 1)) + "3.fdb";
                if (File.Exists(Puth.Text) == true)
                {
                    string TextMesage = "Файл в формате Firebird 3.0 в текущей папке существует." + Environment.NewLine + " Обновить ?";
                    Inst2530 = "BackUpRestory";
                    WinOblakoSetUpdate OblakoSetWindow = new WinOblakoSetUpdate(TextMesage);
                    OblakoSetWindow.Owner = this;  //AddOwnedForm(OblakoNizWindow);
                    OblakoSetWindow.Top = (this.Top + 7) + this.Close_F.Margin.Top + (this.Close_F.Height - 2) + 10;
                    OblakoSetWindow.Left = (this.Left) + this.Close_F.Margin.Left - (OblakoSetWindow.Width - 22) + 1300;
                    // Отображаем
                    OblakoSetWindow.Show();
                    return;
                }
                Administrator.AdminPanels.UpdateKeyReestr(KeyOb, Puth.Text);
            }

            var dlg = new System.Windows.Forms.FolderBrowserDialog();
            dlg.Description = "Путь к  архиву";
            // Show open file dialog box
            //Nullable<bool> result = dlg.ShowDialog();
            System.Windows.Forms.DialogResult result = dlg.ShowDialog();
            if (result == System.Windows.Forms.DialogResult.OK)
            {
                //Чтение параметра и запись нового значения
                var ItemTextBoxPatch = (TextBox)LogicalTreeHelper.FindLogicalNode(this, "PutchDir_");
                if (KeyOb == "PutchBackUpLoc_Fbd25" || KeyOb == "PutchBackUpLoc_Fbd30")
                {
                    Puth.Text = dlg.SelectedPath + @"\BackupFdb\" + SelectPuth.Substring(SelectPuth.LastIndexOf(@"\") + 1, (SelectPuth.Length - (SelectPuth.LastIndexOf(".") + 1))) + DateTime.Now.ToString("yyMMddHHmmss") + ".fbk"; //@"\BackUp" + DateTime.Now.ToString("yyMMddHHmmss") + ".fbk";
                    Administrator.AdminPanels.UpdateKeyReestr(KeyOb, Puth.Text);
                    string MemDirSet = KeyOb == "PutchBackUpLoc_Fbd25" ? "SetDirBackUp25" : "SetDirBackUp30";
                    AppStart.TableReestr[MemDirSet] = dlg.SelectedPath + @"\BackupFdb\"; //ItemTextBoxPutchBackUpLoc =
                    
                }
                if (KeyOb == "PutchBackUpRestorybd2530")
                {
                    Puth.Text = dlg.SelectedPath +@"\"+ SelectPuth.Substring(SelectPuth.LastIndexOf(@"\") + 1, SelectPuth.IndexOf(@".") - (SelectPuth.LastIndexOf(@"\") + 1)) +  "3.fdb";
                    Administrator.AdminPanels.UpdateKeyReestr(KeyOb, Puth.Text);
                }
                else
                {
                    Puth.Text = dlg.SelectedPath;
                    ItemTextBoxPutchBackUpLoc = dlg.SelectedPath;
                    Administrator.AdminPanels.UpdateKeyReestr(KeyOb, ItemTextBoxPutchBackUpLoc);
                }
            }
            //if (KeyOb == "PutchBackUpLoc_Fbd25")Interface.CurrentStateInst( "BackUpLocServerBD25" , "1", "on_off_3.png",  BackUpLocServerBD25 );
            //if (KeyOb == "PutchBackUpLoc_Fbd30") Interface.CurrentStateInst( "BackUpLocServerBD30", "1", "on_off_3.png",  BackUpLocServerBD30);
        }

 


        // Процедура: Размещение BackUp БД  Включить / выключить для всех серверов для локального, ФТП, GoogleDrive.
        private void BackUpOnOffServerBD(string SetOnOff, string SetPutch, Image SaveReg, TextBox PutchBackUp, Image DirPathBuckUp)
        {
            if (Convert.ToInt32(AppStart.TableReestr[SetOnOff].ToString()) == 2 ) 
            {
                PutchBackUp.Text = "";
                Administrator.AdminPanels.UpdateKeyReestr(SetPutch, "");
                DirPathBuckUp.IsEnabled = false;
                PutchBackUp.IsEnabled = false;
                Interface.EndEvensIstallReg(SetOnOff, false);
                Interface.EndEvensIstall(false, ref SaveReg);
            }
        }

        // Процедура очистки БД
        private void ProcCleanBD25Run()
        {
            // Права в ОС Win
            if (App.aSystemVirable["UserWindowIdentity"] == "1")
            {

                DateTime dateClean = datepiker;
                string DateClean_SqlFB = string.Format("{0}.{1}.{2}", dateClean.Day, dateClean.Month, dateClean.Year);
                string DateClean_Server = hubdate == "30" ? "DateClean_Fbd30" : "DateClean_Fbd25";
                string Fb2530 = hubdate;
                UpdateKeyReestr(DateClean_Server, DateClean_SqlFB);
                // Установка блока опсания БД для для подключения к серверу.
                BlokOpisConecto(Fb2530);
 
                if (ValueMetod != "Все организации")
                {
                    System.Windows.Application.Current.Dispatcher.Invoke(new Action(delegate ()
                    {
                        AdminPanels ConectoWorkSpace_InW = AppStart.LinkMainWindow("WAdminPanels");
                        WinSetSpr SetWindow = new WinSetSpr();
                        SetWindow.Owner = ConectoWorkSpace_InW;
                        SetWindow.Top = ConectoWorkSpace_InW.Top + 460;
                        SetWindow.Left = ConectoWorkSpace_InW.Left + 880;
                        SetWindow.Show();

                    }));
                }
                else
                {
                    KodOrgClearDB = "";
                    ProcCleanBD25Run2();
                } 



            }
        }
        public static  void ProcCleanBD25Run2()
        {

            if (ValueMetod != "Все организации")
            {
                System.Windows.Application.Current.Dispatcher.Invoke(new Action(delegate ()
                {
                    ConectoWorkSpace.WinSetSpr ConectoWorkSpace_Off = AppStart.LinkMainWindow("WinSetSprW");
                    ConectoWorkSpace_Off.Close();
 
                }));
 
            }

            AppStart.RenderInfo Arguments01 = new AppStart.RenderInfo() { };
            Arguments01.argument1 = hubdate == "30" ? "DateClean_Fbd30" : "DateClean_Fbd25";
            Thread thStartTimer01 = new Thread(InstallB52.InstRunCleanBDTh);
            thStartTimer01.SetApartmentState(ApartmentState.STA);
            thStartTimer01.IsBackground = true; // Фоновый поток
            thStartTimer01.Start(Arguments01);
            InstallB52.IntThreadStart++;

            WaitMessage WaitWindow = new WaitMessage();
            WaitWindow.Owner = AppStart.LinkMainWindow(); 
            WaitWindow.Top = SystemConecto.WorkAreaDisplayDefault[0] - 500; 
            WaitWindow.Left = SystemConecto.WorkAreaDisplayDefault[1] - 300; 
            WaitWindow.Show();
                            
        }



        private void DirPath_App_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Administrator.AdminPanels.SetWinSetHub = "AddApp";
            ConectoWorkSpace.InstallB52.WindowSetkey();
            
        }


        private void DirPath_OtherApp_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Administrator.AdminPanels.SetWinSetHub = "OtherAddApp";
            PathFileBD_Click();
            if ((!PathFileBDText.Contains(".exe")  && !PathFileBDText.Contains(".EXE")) || PathFileBDText.Length == 0)
            {
                var TextWindows = "Расширение файла выбранного приложения не является исполняемым" + Environment.NewLine + "Необходмио выбрать другой файл. ";
                InstallB52.MessageErorInst(TextWindows);
                return;
            }
            ContentPuth =  PathFileBDText;
            PuthFile_OtherApp.Text = ContentPuth;
            PuthFile.Text = "";
        }
 

        private void DirPath_Fbd25_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

            if (Administrator.AdminPanels.SetWinSetHub == "BackUpLoc_Fbd25" 
                || Administrator.AdminPanels.SetWinSetHub == "BackUpLoc_Fbd30" 
                || Administrator.AdminPanels.SetWinSetHub == "BackUpRestory_Fbd2530" 
                || Administrator.AdminPanels.SetWinSetHub == "DateArhiv_Fbd30"
                || Administrator.AdminPanels.SetWinSetHub == "DateArhiv_Fbd25"
                || Administrator.AdminPanels.SetWinSetHub == "ChangeArhiv_Fbd25"
                || Administrator.AdminPanels.SetWinSetHub == "ChangeArhiv_Fbd30")
            {
                var dlg = new System.Windows.Forms.FolderBrowserDialog();
                dlg.Description = "Путь к  архиву";
                // Show open file dialog box
                //Nullable<bool> result = dlg.ShowDialog();
                System.Windows.Forms.DialogResult result = dlg.ShowDialog();
                if (result == System.Windows.Forms.DialogResult.OK)
                {
                    //Чтение параметра и запись нового значения
                    var ItemTextBoxPatch = (TextBox)LogicalTreeHelper.FindLogicalNode(this, "PutchDir_");
                    if (Administrator.AdminPanels.SetWinSetHub == "BackUpLoc_Fbd25" )
                    {
                        if (!Directory.Exists(@"c:\BackupFdb")) Directory.CreateDirectory(@"c:\BackupFdb");
                        var FileBackupFdb = new FileInfo(dlg.SelectedPath + @"\BackupFdb\" + SelectPuth.Substring(SelectPuth.LastIndexOf(@"\") + 1, (SelectPuth.LastIndexOf(".") - (SelectPuth.LastIndexOf(@"\") + 1))) + @"\" + SelectPuth.Substring(SelectPuth.LastIndexOf(@"\") + 1, (SelectPuth.LastIndexOf(".") - (SelectPuth.LastIndexOf(@"\") + 1))) + DateTime.Now.ToString("yyMMddHHmmss") + ".fbk");
                        FileBackupFdb.Directory.Create();
                        Putch_Fbd25.Text = FileBackupFdb.ToString();

                        //Putch_Fbd25.Text = dlg.SelectedPath + @"\BackupFdb\" + SelectPuth.Substring(SelectPuth.LastIndexOf(@"\") + 1, (SelectPuth.Length - (SelectPuth.LastIndexOf(".") + 1))) + DateTime.Now.ToString("yyMMddHHmmss") + ".fbk"; //@"\BackUp" + DateTime.Now.ToString("yyMMddHHmmss") + ".fbk";


                        AppStart.TableReestr["SetDirBackUp25"] = dlg.SelectedPath + @"\BackupFdb\" + SelectPuth.Substring(SelectPuth.LastIndexOf(@"\") + 1, (SelectPuth.LastIndexOf(".") - (SelectPuth.LastIndexOf(@"\") + 1))) + @"\";
                        Administrator.AdminPanels.UpdateKeyReestr("PutchBackUpLoc_Fbd25", Putch_Fbd25.Text);
                    }
                    if ( Administrator.AdminPanels.SetWinSetHub == "BackUpLoc_Fbd30")
                    {
                        Putch_Fbd30.Text = dlg.SelectedPath + @"\BackupFdb\" + SelectPuth.Substring(SelectPuth.LastIndexOf(@"\") + 1, (SelectPuth.Length - (SelectPuth.LastIndexOf(".") + 1))) + DateTime.Now.ToString("yyMMddHHmmss") + ".fbk"; //@"\BackUp" + DateTime.Now.ToString("yyMMddHHmmss") + ".fbk";
                        AppStart.TableReestr["SetDirBackUp30"] = dlg.SelectedPath + @"\BackupFdb\";
                        Administrator.AdminPanels.UpdateKeyReestr("PutchBackUpLoc_Fbd30", Putch_Fbd30.Text);
                    }
                    if (Administrator.AdminPanels.SetWinSetHub == "BackUpRestory_Fbd2530")
                    {
                        Putch_Fbd25.Text = dlg.SelectedPath + @"\" + SelectPuth.Substring(SelectPuth.LastIndexOf(@"\") + 1, SelectPuth.IndexOf(@".") - (SelectPuth.LastIndexOf(@"\") + 1)) + "3.fdb";
                    }
                    if (Administrator.AdminPanels.SetWinSetHub == "DateArhiv_Fbd30" || Administrator.AdminPanels.SetWinSetHub == "ChangeArhiv_Fbd30") Putch_Fbd30.Text = dlg.SelectedPath + @"\";
                    if (Administrator.AdminPanels.SetWinSetHub == "DateArhiv_Fbd25" || Administrator.AdminPanels.SetWinSetHub == "ChangeArhiv_Fbd25") Putch_Fbd25.Text = dlg.SelectedPath + @"\";
                }
                return;
            }

            PathFileBD_Click();
            if (PathFileBDText.Length != 0)
            {
                if (!PathFileBDText.Contains(".fbk"))
                {
                    MainWindow ConectoWorkSpace_InW = AppStart.LinkMainWindow();
                    var TextWindows = "Файл " + PathFileBDText + Environment.NewLine + " Не архив.";
                    int AutoClose = 1;

                    var WinOblakoVerh_ = AppStart.WindowActive("WinOblakoVerh_Net", TextWindows, AutoClose);
                    WinOblakoVerh_.Owner = ConectoWorkSpace_InW;
                    // размещаем на рабочем столе
                    WinOblakoVerh_.Top = SystemConecto.WorkAreaDisplayDefault[0] + (WinOblakoVerh_.Height - 58) + 3;
                    WinOblakoVerh_.Left = SystemConecto.WorkAreaDisplayDefault[1] + 650;

                    WinOblakoVerh_.ShowActivated = false;
                    WinOblakoVerh_.Show();
                    return;
                }
                if (Administrator.AdminPanels.SetWinSetHub == "BackUpLoc_Fbd25" || Administrator.AdminPanels.SetWinSetHub == "BackUpRestory_Fbd2530" || Administrator.AdminPanels.SetWinSetHub == "Restory_Fbd25")
                {
                    Putch_Fbd25.Text = PathFileBDText;
                    Administrator.AdminPanels.UpdateKeyReestr("PutchRestoryLoc_Fbd25", PathFileBDText);
                    Administrator.AdminPanels.UpdateKeyReestr("PutchBackUpLoc_Fbd25", PathFileBDText);
                    BackUpLocServerBD25.IsEnabled = true;
                }
                if (Administrator.AdminPanels.SetWinSetHub == "BackUpLoc_Fbd30" )
                {
                    Putch_Fbd30.Text = PathFileBDText;
                    Administrator.AdminPanels.UpdateKeyReestr("PutchRestoryLoc_Fbd30", PathFileBDText);
                    BackUpLocServerBD30.IsEnabled = true;
                }
            }
  
        }

   

        private void RunLocBD25_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            RunLocBD25();
        }

 
        public void RunLocBD25()
        {
            TablAlias.IsEnabled = false;
            string StrCreate = "",HubdateSatellite = "";
            AliasSatelite = ""; 
            int Idcount = 0;
            if (Administrator.AdminPanels.SetWinSetHub == "CopyLog_Fbd25")
            {
                hubdate = "25";
                if (Putch_Satellite != "")
                {
                    StrCreate = "select * from CONNECTIONBD25 where CONNECTIONBD25.PUTHBD = '" + Putch_Satellite + "'";
                    DBConecto.DBopenFBConectionMemory(SystemBD.ConnnStringSystem().ToString(), "", "FbSystem");
                    FbCommand SelectTable = new FbCommand(StrCreate, DBConecto.bdFbSystemConect);
                    FbDataReader ReadOutTable = SelectTable.ExecuteReader();
                    while (ReadOutTable.Read())
                    {
                        AliasSatelite = ReadOutTable[1].ToString().Trim();
                        Idcount++;
                    }
                    ReadOutTable.Close();
                    SelectTable.Dispose();
                    DBConecto.DBcloseFBConectionMemory("FbSystem");
                    if (Idcount == 0) Putch_Satellite = "";
                    else HubdateSatellite = AppStart.TableReestr["SetTextIp4BackOf"] + "/" + SetPort25 + ":" + AliasSatelite;

                }

                WaitMessage WindowWait = new WaitMessage();
                WindowWait.Owner = AppStart.LinkMainWindow(); // ; this;
                WindowWait.Top = SystemConecto.WorkAreaDisplayDefault[0] - 500;
                WindowWait.Left = SystemConecto.WorkAreaDisplayDefault[1] - 300;
                WindowWait.Show();

                AppStart.RenderInfo Arguments01 = new AppStart.RenderInfo() { };
                Arguments01.argument1 = datepikerS.ToString();
                Arguments01.argument2 = datepikerP.ToString();
                Arguments01.argument3 = NameServer25;
                Arguments01.argument4 = AliasSatelite;
                Thread thStartTimer01 = new Thread(InstallB52.CopyLogSatellite);
                thStartTimer01.SetApartmentState(ApartmentState.STA);
                thStartTimer01.IsBackground = true; // Фоновый поток
                thStartTimer01.Start(Arguments01);
                InstallB52.IntThreadStart++;



                return;
            }
            
            
            if (Administrator.AdminPanels.SetWinSetHub == "DateArhiv_Fbd25" || Administrator.AdminPanels.SetWinSetHub == "ChangeArhiv_Fbd25")
            {

                // запись в файл расписаний вновь созданного задания
                string PuthChange = Administrator.AdminPanels.SetWinSetHub == "ChangeArhiv_Fbd25" ? SchedulePuth : SelectPuth ;
                StrCreate = "SELECT * FROM SCHEDULEARHIV WHERE SCHEDULEARHIV.PUTH = " + "'" + PuthChange + "'";
                Idcount = 0;
                DBConecto.DBopenFBConectionMemory(SystemBD.ConnnStringSystem().ToString(), "", "FbSystem");
                FbCommand SelectTableBack = new FbCommand(StrCreate, DBConecto.bdFbSystemConect);
                SelectTableBack.CommandType = CommandType.Text;
                FbDataReader ReadOutTableBack = SelectTableBack.ExecuteReader();
                while (ReadOutTableBack.Read()) { Idcount++; }
                ReadOutTableBack.Close();
                if (Idcount == 0) StrCreate = "INSERT INTO SCHEDULEARHIV  values ('" + PuthChange + "','" + Putch_Fbd25.Text + "'" + ", '" + ArhivSetDay25.Text + "', '" + Hour25.Text + ":" + Minute25.Text + "', '" + NameServer25 + "')";
                if (Idcount != 0) StrCreate = "UPDATE SCHEDULEARHIV SET ARHIV = '" + Putch_Fbd25.Text + "', SETDAY = '" + ArhivSetDay25.Text + "', SETTIME = '" + Hour25.Text + ":" + Minute25.Text + "' WHERE SCHEDULEARHIV.PUTH = '" + PuthChange + "'";
                DBConecto.UniQuery CountQuery = new DBConecto.UniQuery(StrCreate, "FB");
                CountQuery.UserQuery = string.Format(StrCreate, "SCHEDULEARHIV");
                CountQuery.ExecuteUNIScalar();
                DBConecto.DBcloseFBConectionMemory("FbSystem");
                DateArhivBd30(25);
                SetArhivBd25.Visibility = Visibility.Collapsed;
                ArhivSetDay25.Visibility = Visibility.Collapsed;
                SetArhivBd225.Visibility = Visibility.Collapsed;
                SetArhivBd325.Visibility = Visibility.Collapsed;
                Hour25.Visibility = Visibility.Collapsed;
                sep125.Visibility = Visibility.Collapsed;
                Minute25.Visibility = Visibility.Collapsed;
                NameArhivBd25.Visibility = Visibility.Collapsed;
                InstallB52.CloseVisibilityButton();
                CreateListArhivBd25.Foreground = Brushes.White;
                TablAlias.IsEnabled = true;
                return;
            }
            // Проверка активности сервера
            Inst2530 = "25";
            ScanActivFirebird();
            if (IndexActivProces < 0)
            {
                var TextWindows = "Сервер Firebird 2.5 не запущен. Выполнение процедуры остановлено.";
                int AutoClose = 1;
                int MesaggeTop = -170;
                int MessageLeft = 800;
                InstallB52.MessageEnd(TextWindows, AutoClose, MesaggeTop, MessageLeft);
                TablAlias.IsEnabled = true;
                return;

            }

            if (Administrator.AdminPanels.SetWinSetHub != "BackUpFTP_Fbd25")
            {
                string DeviceOn = Putch_Fbd25.Text.Substring(0, 3);
                DriveInfo di = new DriveInfo(DeviceOn);
                long Ffree = (di.TotalFreeSpace / 1024) / 1024;
                string MBFree = Ffree.ToString("#,##") + " MB";
                if (Ffree - 2500 < 0)
                {
                    System.Windows.Application.Current.Dispatcher.Invoke(new Action(delegate ()
                    {
                         MainWindow ConectoWorkSpace_InW = AppStart.LinkMainWindow();
                         var TextWindows = "Создание новой копии БД требует 2,5Гб свободного пространства" + Environment.NewLine + "Текущее пространство " + MBFree + Environment.NewLine + "Освободите пространство на диске и повторите BackUp ";
                        int AutoClose = 1;
                        Window WinOblakoVerh_Info = new WinMessage(TextWindows, AutoClose, 0); // создаем AutoClose
                        WinOblakoVerh_Info.Owner = ConectoWorkSpace_InW;
                        WinOblakoVerh_Info.Top = SystemConecto.WorkAreaDisplayDefault[0] + 350; 
                        WinOblakoVerh_Info.Left = SystemConecto.WorkAreaDisplayDefault[1] + 650;
                        WinOblakoVerh_Info.ShowActivated = false;
                        WinOblakoVerh_Info.Show();

                    }));
                    TablAlias.IsEnabled = true;
                    return;
                }
            }

   
            if (CurrentSereverPuth == "")
            {
                var TextWindows = "Неопределен путь к БД. Выполнение процедуры остановлено.";
                int AutoClose = 1;
                int MesaggeTop = -170;
                int MessageLeft = 800;
                InstallB52.MessageEnd(TextWindows, AutoClose, MesaggeTop, MessageLeft);
                TablAlias.IsEnabled = true;
                return;
            }
            DBConecto.DBcloseFBConectionMemory("FbSystem");
            AppStart.TableReestr["NameServer25"] = NameServer25;

            if (!SystemConecto.File_(CurrentSereverPuth + @"bin\gbak.exe", 5))
            {
                MainWindow ConectoWorkSpace_InW = AppStart.LinkMainWindow();
                var TextWindows = "Отсутствует файл gbak.exe " + Environment.NewLine + " Восстановление невозможно.";
                int AutoClose = 1;
                var WinOblakoVerh_ = AppStart.WindowActive("WinOblakoVerh_Net", TextWindows, AutoClose);
                WinOblakoVerh_.Owner = ConectoWorkSpace_InW;
                WinOblakoVerh_.Top = SystemConecto.WorkAreaDisplayDefault[0] + 350;
                WinOblakoVerh_.Left = SystemConecto.WorkAreaDisplayDefault[1] + 650;
                WinOblakoVerh_.ShowActivated = false;
                WinOblakoVerh_.Show();
                TablAlias.IsEnabled = true;
                return;
            }
            if (!SystemConecto.File_(CurrentSereverPuth + @"bin\instsvc.exe", 5))
            {
                MainWindow ConectoWorkSpace_InW = AppStart.LinkMainWindow();
                var TextWindows = "Отсутствует файл instsvc.exe " + Environment.NewLine + " Восстановление невозможно.";
                int AutoClose = 1;
                var WinOblakoVerh_ = AppStart.WindowActive("WinOblakoVerh_Net", TextWindows, AutoClose);
                WinOblakoVerh_.Owner = ConectoWorkSpace_InW;
                WinOblakoVerh_.Top = SystemConecto.WorkAreaDisplayDefault[0] + 350;
                WinOblakoVerh_.Left = SystemConecto.WorkAreaDisplayDefault[1] + 650;
                WinOblakoVerh_.ShowActivated = false;
                WinOblakoVerh_.Show();
                TablAlias.IsEnabled = true;
                return;
            }

            WaitMessage WaitWindow = new WaitMessage();
            WaitWindow.Owner = AppStart.LinkMainWindow(); // ; this;
            WaitWindow.Top = SystemConecto.WorkAreaDisplayDefault[0] - 500; 
            WaitWindow.Left = SystemConecto.WorkAreaDisplayDefault[1] - 300; 
            WaitWindow.Show();

            hubdate = "25";          
            // BackUp 
            if (SetWinSetHub == "BackUpLoc_Fbd25")
            {
 
                SystemConecto.DIR_(AppStart.TableReestr["SetDirBackUp25"]);
                string Sethubdate = AppStart.TableReestr["SetTextIp4BackOf"] + "/" + SetPort25 + ":" + SelectAlias.Trim();
                RunGbak = CurrentSereverPuth + @"bin\gbak.exe";
                ArgumentCmd = @"-b " + Sethubdate + " " + Putch_Fbd25.Text + @" -v  -user sysdba -pass " + CurrentPasswFb25; // + " -y " + PuthLog;  //-y c:\temp\log.txt
 
                Idcount = 0;  AliasSatelite = "";  HubdateSatellite = ""; string SatellitePutch = "";
                if (Putch_Satellite != "")
                {
                    StrCreate = "select * from CONNECTIONBD25 where CONNECTIONBD25.PUTHBD = '" + Putch_Satellite + "'";
                    DBConecto.DBopenFBConectionMemory(SystemBD.ConnnStringSystem().ToString(), "", "FbSystem");
                    FbCommand SelectTable = new FbCommand(StrCreate, DBConecto.bdFbSystemConect);
                    FbDataReader ReadOutTable = SelectTable.ExecuteReader();
                    while (ReadOutTable.Read())
                    {
                        AliasSatelite = ReadOutTable[1].ToString();
                        Idcount ++;
                    }
                    ReadOutTable.Close();
                    SelectTable.Dispose();
                    DBConecto.DBcloseFBConectionMemory("FbSystem");
                    if (Idcount == 0) Putch_Satellite = "";
                    else  HubdateSatellite = AppStart.TableReestr["SetTextIp4BackOf"] + "/" + SetPort25 + ":" + AliasSatelite.Trim();

                }
                SatellitePutch = AliasSatelite != "" ? Putch_Fbd25.Text.Substring(0, Putch_Fbd25.Text.LastIndexOf(@"\")+1)+ AliasSatelite +".fbk" : "";

                AppStart.RenderInfo Arguments01 = new AppStart.RenderInfo() { };
                Arguments01.argument1 = Putch_Fbd25.Text;
                Arguments01.argument2 = Putch_Fbd25.Text;
                Arguments01.argument3 = SatellitePutch;
                Arguments01.argument4 = CurrentPasswFb25;
                Arguments01.argument5 = HubdateSatellite;
                Thread thStartTimer01 = new Thread(InstallB52.BackUpRunTH);
                thStartTimer01.SetApartmentState(ApartmentState.STA);
                thStartTimer01.IsBackground = true; // Фоновый поток
                thStartTimer01.Start(Arguments01);
                InstallB52.IntThreadStart++;
                return;
            }

            // Restory 
            if (Administrator.AdminPanels.SetWinSetHub == "Restory_Fbd25")
            {

                AppStart.RenderInfo Arguments01 = new AppStart.RenderInfo() { };
                Arguments01.argument1 = SelectAlias.Trim();
                Arguments01.argument2 = SetPort25;
                Arguments01.argument3 = CurrentSereverPuth;

                Thread thStartTimer01 = new Thread(Restory25BDRunTH);
                thStartTimer01.SetApartmentState(ApartmentState.STA);
                thStartTimer01.IsBackground = true; // Фоновый поток
                thStartTimer01.Start(Arguments01);
                InstallB52.IntThreadStart++;
            }

            // Процедура перезаписии Бд из формати FireBird 2.5 в формат БД FireBird 3.0,
            if (Administrator.AdminPanels.SetWinSetHub == "BackUpRestory_Fbd2530")
            {
                if (AppStart.TableReestr["ServerDefault30"] == "")
                {
                    System.Windows.Application.Current.Dispatcher.Invoke(new Action(delegate ()
                    {
                        MainWindow ConectoWorkSpace_InW = AppStart.LinkMainWindow();
                        var TextWindows = "Не установлен сервер FireBird 3.0." + Environment.NewLine + "Для выполнения процедуры необходимо установть FireBird 3.0 ";
                        int AutoClose = 1;
                        Window WinOblakoVerh_Info = new WinMessage(TextWindows, AutoClose, 0); // создаем AutoClose
                        WinOblakoVerh_Info.Owner = ConectoWorkSpace_InW;
                        WinOblakoVerh_Info.Top = SystemConecto.WorkAreaDisplayDefault[0] + +350; // (WinOblakoVerh_Info.Height - 58)
                        WinOblakoVerh_Info.Left = SystemConecto.WorkAreaDisplayDefault[1] + 650;
                        WinOblakoVerh_Info.ShowActivated = false;
                        WinOblakoVerh_Info.Show();
                        TablAlias.IsEnabled = true;
                        return;
                    }));
                }


                string TCPIPBack = AppStart.TableReestr["SetTextIp4BackOf"] + "/" + SetPort25 + ":" + SelectAlias.Trim();
                string CleanHub = @"  c:\BackupFdb\hub" + DateTime.Now.ToString("yyMMddHHmmss") + ".fbk";
                RunGbak = CurrentSereverPuth + @"bin\gbak.exe";
                ArgumentCmd = "-b " + TCPIPBack + CleanHub + " -v -user sysdba -pass " + CurrentPasswFb25;
 
 
                AppStart.RenderInfo Arguments01 = new AppStart.RenderInfo() { };
                Arguments01.argument1 = Putch_Fbd25.Text;
                Arguments01.argument2 = CleanHub;
                Arguments01.argument3 = SelectPuth;
                Arguments01.argument4 = CurrentSereverPuth;

                Thread thStartTimer01 = new Thread(InstallB52.BackUp25TH);
                thStartTimer01.SetApartmentState(ApartmentState.STA);
                thStartTimer01.IsBackground = true; // Фоновый поток
                thStartTimer01.Start(Arguments01);
                InstallB52.IntThreadStart++;

            }
            // Процедура записи архива на FTP серевер 
            if (Administrator.AdminPanels.SetWinSetHub == "BackUpFTP_Fbd25")
            {
                if (!Directory.Exists(@"c:\BackupFdb")) Directory.CreateDirectory(@"c:\BackupFdb");
                var FileBackupFdb = new FileInfo(@"c:\BackupFdb\" + SelectPuth.Substring(SelectPuth.LastIndexOf(@"\") + 1, (SelectPuth.LastIndexOf(".") - (SelectPuth.LastIndexOf(@"\") + 1))) + @"\" + SelectPuth.Substring(SelectPuth.LastIndexOf(@"\") + 1, (SelectPuth.LastIndexOf(".") - (SelectPuth.LastIndexOf(@"\") + 1))) + DateTime.Now.ToString("yyMMddHHmmss") + ".fbk");
                string DirBackupFdb = @"c:\BackupFdb\" + SelectPuth.Substring(SelectPuth.LastIndexOf(@"\") + 1, (SelectPuth.LastIndexOf(".") - (SelectPuth.LastIndexOf(@"\") + 1))) + @"\";
                FileBackupFdb.Directory.Create();
                string Putch_Fbd25 = FileBackupFdb.ToString();
                string Sethubdate = AppStart.TableReestr["SetTextIp4BackOf"] + "/" + SetPort25 + ":" + SelectAlias.Trim();
                RunGbak = CurrentSereverPuth + @"bin\gbak.exe";
                ArgumentCmd = @"-b " + Sethubdate + " " + Putch_Fbd25 + @" -v -user sysdba -pass " + CurrentPasswFb25;  //-y c:\temp\log.txt
 

                AppStart.RenderInfo Arguments01 = new AppStart.RenderInfo() { };
                Arguments01.argument1 = Putch_Fbd25;
                Arguments01.argument2 = DirBackupFdb;
                Thread thStartTimer01 = new Thread(InstallB52.BackUpRunTH);
                thStartTimer01.SetApartmentState(ApartmentState.STA);
                thStartTimer01.IsBackground = true; // Фоновый поток
                thStartTimer01.Start(Arguments01);
                InstallB52.IntThreadStart++;
 
            }


        }

        private void RunLocBD30_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            RunLocBD30();
        }

        public void RunLocBD30()
        {
            hubdate = "30";
            TablAlias30.IsEnabled = false;
            string StrCreate = "", HubdateSatellite = "";
            AliasSatelite = "";
            int Idcount = 0;
            if (Administrator.AdminPanels.SetWinSetHub == "CopyLog_Fbd30")
            {
                if (Putch_Satellite != "")
                {
                    StrCreate = "select * from CONNECTIONBD30 where CONNECTIONBD30.PUTHBD = '" + Putch_Satellite + "'";
                    DBConecto.DBopenFBConectionMemory(SystemBD.ConnnStringSystem().ToString(), "", "FbSystem");
                    FbCommand SelectTable = new FbCommand(StrCreate, DBConecto.bdFbSystemConect);
                    FbDataReader ReadOutTable = SelectTable.ExecuteReader();
                    while (ReadOutTable.Read())
                    {
                        AliasSatelite = ReadOutTable[1].ToString().Trim();
                        Idcount++;
                    }
                    ReadOutTable.Close();
                    SelectTable.Dispose();
                    DBConecto.DBcloseFBConectionMemory("FbSystem");
                    if (Idcount == 0) Putch_Satellite = "";
                    else HubdateSatellite = AppStart.TableReestr["SetTextIp4BackOf"] + "/" + SetPort30 + ":" + AliasSatelite;

                }

                WaitMessage WindowWait = new WaitMessage();
                WindowWait.Owner = AppStart.LinkMainWindow(); // ; this;
                WindowWait.Top = SystemConecto.WorkAreaDisplayDefault[0] - 500;
                WindowWait.Left = SystemConecto.WorkAreaDisplayDefault[1] - 300;
                WindowWait.Show();

                AppStart.RenderInfo Arguments01 = new AppStart.RenderInfo() { };
                Arguments01.argument1 = datepikerS.ToString();
                Arguments01.argument2 = datepikerP.ToString();
                Arguments01.argument3 = NameServer30;
                Arguments01.argument4 = AliasSatelite;
                Thread thStartTimer01 = new Thread(InstallB52.CopyLogSatellite);
                thStartTimer01.SetApartmentState(ApartmentState.STA);
                thStartTimer01.IsBackground = true; // Фоновый поток
                thStartTimer01.Start(Arguments01);
                InstallB52.IntThreadStart++;



                return;
            }

            if (Administrator.AdminPanels.SetWinSetHub == "DateArhiv_Fbd30" || Administrator.AdminPanels.SetWinSetHub == "ChangeArhiv_Fbd30")
            {

                // запись в файл расписаний вновь созданного задания
                string PuthChange = Administrator.AdminPanels.SetWinSetHub == "ChangeArhiv_Fbd30" ? SchedulePuth : SelectPuth;
                StrCreate = "SELECT * FROM SCHEDULEARHIV WHERE SCHEDULEARHIV.PUTH = " + "'" + PuthChange + "'";
                DBConecto.DBopenFBConectionMemory(SystemBD.ConnnStringSystem().ToString(), "", "FbSystem");
                FbCommand SelectTableBack = new FbCommand(StrCreate, DBConecto.bdFbSystemConect);
                SelectTableBack.CommandType = CommandType.Text;
                FbDataReader ReadOutTableBack = SelectTableBack.ExecuteReader();
                while (ReadOutTableBack.Read()) { Idcount++; }
                ReadOutTableBack.Close();
                if (Idcount == 0) StrCreate = "INSERT INTO SCHEDULEARHIV  values ('" + PuthChange + "','" + Putch_Fbd30.Text + "'" + ", '" + ArhivSetDay.Text + "', '" + Hour.Text + ":" + Minute.Text + "', '" + NameServer30 + "')";
                if (Idcount != 0) StrCreate = "UPDATE SCHEDULEARHIV SET ARHIV = '" + Putch_Fbd30.Text + "', SETDAY = '" + ArhivSetDay.Text + "', SETTIME = '" + Hour.Text + ":" + Minute.Text + "' WHERE SCHEDULEARHIV.PUTH = '" + PuthChange + "'";
                DBConecto.UniQuery CountQuery = new DBConecto.UniQuery(StrCreate, "FB");
                CountQuery.UserQuery = string.Format(StrCreate, "SCHEDULEARHIV");
                CountQuery.ExecuteUNIScalar();
                DBConecto.DBcloseFBConectionMemory("FbSystem");
                DateArhivBd30(30);
                SetArhivBd.Visibility = Visibility.Collapsed;
                ArhivSetDay.Visibility = Visibility.Collapsed;
                SetArhivBd2.Visibility = Visibility.Collapsed;
                SetArhivBd3.Visibility = Visibility.Collapsed;
                Hour.Visibility = Visibility.Collapsed;
                sep1.Visibility = Visibility.Collapsed;
                Minute.Visibility = Visibility.Collapsed;
                NameArhivBd2.Visibility = Visibility.Collapsed;
                InstallB52.CloseVisibilityButton30();
                Delete_Str_Backup.IsEnabled = false;
                Change_Str_Backup.IsEnabled = false;
                Delete_Str_Backup30.IsEnabled = false;
                Change_Str_Backup30.IsEnabled = false;
                return;
            }
            // Проверка активности сервера
            Inst2530 = "firebird";
            ScanActivFirebird();
            if (IndexActivProces < 0)
            {
                var TextWindows = "Сервер Firebird 3.0 не запущен. Выполнение процедуры остановлено.";
                int AutoClose = 1;
                int MesaggeTop = -100;
                int MessageLeft = 850;
                InstallB52.MessageEnd(TextWindows, AutoClose, MesaggeTop, MessageLeft);
                return;

            }

            if (Administrator.AdminPanels.SetWinSetHub != "BackUpFTP_Fbd30")
            {
                string DeviceOn = Putch_Fbd30.Text.Substring(0, 3);
                DriveInfo di = new DriveInfo(DeviceOn);
                long Ffree = (di.TotalFreeSpace / 1024) / 1024;
                string MBFree = Ffree.ToString("#,##") + " MB";
                if (Ffree - 2500 < 0)
                {
                    System.Windows.Application.Current.Dispatcher.Invoke(new Action(delegate ()
                    {
                        MainWindow ConectoWorkSpace_InW = AppStart.LinkMainWindow();
                        var TextWindows = "Создание новой копии БД требует 2,5Гб свободного пространства" + Environment.NewLine + "Текущее пространство " + MBFree + Environment.NewLine + "Освободите пространство на диске и повторите BackUp ";
                        int AutoClose = 1;
                        Window WinOblakoVerh_Info = new WinMessage(TextWindows, AutoClose, 0); // создаем AutoClose
                        WinOblakoVerh_Info.Owner = ConectoWorkSpace_InW;
                        WinOblakoVerh_Info.Top = SystemConecto.WorkAreaDisplayDefault[0] + 350; // (WinOblakoVerh_Info.Height - 58)
                        WinOblakoVerh_Info.Left = SystemConecto.WorkAreaDisplayDefault[1] + 650;
                        WinOblakoVerh_Info.ShowActivated = false;
                        WinOblakoVerh_Info.Show();

                    }));
                    return;
                }

            }

 
            if (CurrentSereverPuth == "")
            {
                var TextWindows = "Неопределен путь к БД. Выполнение процедуры остановлено.";
                int AutoClose = 1;
                int MesaggeTop = -100;
                int MessageLeft = 850;
                InstallB52.MessageEnd(TextWindows, AutoClose, MesaggeTop, MessageLeft);
                return;
            }
            DBConecto.DBcloseFBConectionMemory("FbSystem");
            AppStart.TableReestr["NameServer30"] = NameServer30;

            if (!SystemConecto.File_(CurrentSereverPuth + "gbak.exe", 5))
            {
                MainWindow ConectoWorkSpace_InW = AppStart.LinkMainWindow();
                var TextWindows = "Отсутствует файл gbak.exe " + Environment.NewLine + " Восстановление невозможно.";
                int AutoClose = 1;
                var WinOblakoVerh_ = AppStart.WindowActive("WinOblakoVerh_Net", TextWindows, AutoClose);
                WinOblakoVerh_.Owner = ConectoWorkSpace_InW;
                WinOblakoVerh_.Top = SystemConecto.WorkAreaDisplayDefault[0] + (WinOblakoVerh_.Height - 58) + 3;
                WinOblakoVerh_.Left = SystemConecto.WorkAreaDisplayDefault[1] + 650;
                WinOblakoVerh_.ShowActivated = false;
                WinOblakoVerh_.Show();
                return;
            }
            if (!SystemConecto.File_(CurrentSereverPuth + @"instsvc.exe", 5))
            {
                MainWindow ConectoWorkSpace_InW = AppStart.LinkMainWindow();
                var TextWindows = "Отсутствует файл instsvc.exe " + Environment.NewLine + " Восстановление невозможно.";
                int AutoClose = 1;
                var WinOblakoVerh_ = AppStart.WindowActive("WinOblakoVerh_Net", TextWindows, AutoClose);
                WinOblakoVerh_.Owner = ConectoWorkSpace_InW;
                WinOblakoVerh_.Top = SystemConecto.WorkAreaDisplayDefault[0] + (WinOblakoVerh_.Height - 58) + 3;
                WinOblakoVerh_.Left = SystemConecto.WorkAreaDisplayDefault[1] + 650;
                WinOblakoVerh_.ShowActivated = false;
                WinOblakoVerh_.Show();
                return;
            }

            WaitMessage WaitWindow = new WaitMessage();
            WaitWindow.Owner = AppStart.LinkMainWindow(); // ; this;
            WaitWindow.Top = SystemConecto.WorkAreaDisplayDefault[0] - 500;
            WaitWindow.Left = SystemConecto.WorkAreaDisplayDefault[1] - 300;
            WaitWindow.Show();

            // BackUp 
            if (Administrator.AdminPanels.SetWinSetHub == "BackUpLoc_Fbd30")
            {

                SystemConecto.DIR_(AppStart.TableReestr["SetDirBackUp30"]);
                string Sethubdate = AppStart.TableReestr["SetTextIp4BackOf"] + "/" + SetPort30 + ":" + SelectAlias;
                RunGbak = CurrentSereverPuth + "gbak.exe";
                ArgumentCmd = "-b " + Sethubdate + " " + Putch_Fbd30.Text + " -v  -user sysdba -pass " + CurrentPasswFb30;  //-y c:\temp\log.txt

                Idcount = 0; string AliasSatelite = ""; HubdateSatellite = "";string SatellitePutch = ""; ;
                if (Putch_Satellite != "")
                {
                    StrCreate = "select * from CONNECTIONBD30 where CONNECTIONBD30.PUTHBD = '" + Putch_Satellite + "'";
                    DBConecto.DBopenFBConectionMemory(SystemBD.ConnnStringSystem().ToString(), "", "FbSystem");
                    FbCommand SelectTable30 = new FbCommand(StrCreate, DBConecto.bdFbSystemConect);
                    FbDataReader ReadOutTable30 = SelectTable30.ExecuteReader();
                    while (ReadOutTable30.Read())
                    {
                        AliasSatelite = ReadOutTable30[1].ToString();
                        Idcount++;
                    }
                    ReadOutTable30.Close();
                    SelectTable30.Dispose();
                    DBConecto.DBcloseFBConectionMemory("FbSystem");
                    if (Idcount == 0) Putch_Satellite = "";
                    else HubdateSatellite = AppStart.TableReestr["SetTextIp4BackOf"] + "/" + SetPort30 + ":" + AliasSatelite.Trim();

                }
                SatellitePutch = AliasSatelite != "" ? Putch_Fbd30.Text.Substring(0, Putch_Fbd30.Text.LastIndexOf(@"\") + 1) + AliasSatelite + ".fbk" : "";

                AppStart.RenderInfo Arguments01 = new AppStart.RenderInfo() { };
                Arguments01.argument1 = Putch_Fbd30.Text;
                Arguments01.argument2 = Putch_Fbd30.Text;
                Arguments01.argument3 = SatellitePutch;
                Arguments01.argument4 = CurrentPasswFb30;
                Arguments01.argument5 = HubdateSatellite;
                Thread thStartTimer01 = new Thread(InstallB52.BackUpRunTH);
                thStartTimer01.SetApartmentState(ApartmentState.STA);
                thStartTimer01.IsBackground = true; // Фоновый поток
                thStartTimer01.Start(Arguments01);
                InstallB52.IntThreadStart++;
                return;
            }

            // Restory 
            if (Administrator.AdminPanels.SetWinSetHub == "Restory_Fbd30")
            {

                AppStart.RenderInfo Arguments01 = new AppStart.RenderInfo() { };
                Arguments01.argument1 = SelectAlias;
                Arguments01.argument2 = PortSrv;
                Arguments01.argument3 = CurrentSereverPuth;
                Arguments01.argument4 = SelectPuth;

                Thread thStartTimer01 = new Thread(Restory30BDRunTH);
                thStartTimer01.SetApartmentState(ApartmentState.STA);
                thStartTimer01.IsBackground = true; // Фоновый поток
                thStartTimer01.Start(Arguments01);
                InstallB52.IntThreadStart++;
            }

            // Процедура записи архива на FTP серевер 
            if (Administrator.AdminPanels.SetWinSetHub == "BackUpFTP_Fbd30")
            {
                if (!Directory.Exists(@"c:\BackupFdb")) Directory.CreateDirectory(@"c:\BackupFdb");
                var FileBackupFdb = new FileInfo(@"c:\BackupFdb\" + SelectPuth.Substring(SelectPuth.LastIndexOf(@"\") + 1, (SelectPuth.LastIndexOf(".") - (SelectPuth.LastIndexOf(@"\") + 1))) + @"\" + SelectPuth.Substring(SelectPuth.LastIndexOf(@"\") + 1, (SelectPuth.LastIndexOf(".") - (SelectPuth.LastIndexOf(@"\") + 1))) + DateTime.Now.ToString("yyMMddHHmmss") + ".fbk");
                string DirBackupFdb = @"c:\BackupFdb\" + SelectPuth.Substring(SelectPuth.LastIndexOf(@"\") + 1, (SelectPuth.LastIndexOf(".") - (SelectPuth.LastIndexOf(@"\") + 1))) + @"\";
                FileBackupFdb.Directory.Create();
                string Putch_Fbd30 = FileBackupFdb.ToString();
                string Sethubdate = AppStart.TableReestr["SetTextIp4BackOf"] + "/" + SetPort30 + ":" + SelectAlias.Trim();
                RunGbak = CurrentSereverPuth + "gbak.exe";
                ArgumentCmd = @"-b " + Sethubdate + " " + Putch_Fbd30 + @" -v -user sysdba -pass " + CurrentPasswFb30;  //-y c:\temp\log.txt

                AppStart.RenderInfo Arguments01 = new AppStart.RenderInfo() { };
                Arguments01.argument1 = Putch_Fbd30;
                Arguments01.argument2 = DirBackupFdb;
                Thread thStartTimer01 = new Thread(InstallB52.BackUpRunTH);
                thStartTimer01.SetApartmentState(ApartmentState.STA);
                thStartTimer01.IsBackground = true; // Фоновый поток
                thStartTimer01.Start(Arguments01);
                InstallB52.IntThreadStart++;
            }
        }
        public static void Restory25BDRunTH(object ThreadObj)
        {
            // Разбор аргументов
            AppStart.RenderInfo arguments = (AppStart.RenderInfo)ThreadObj;
            string PuthSetSrv = arguments.argument3;
            Administrator.AdminPanels.SelectAlias = arguments.argument1;
            string PortSrv = arguments.argument2;
            if (File.Exists(SelectPuth))
            {
                File.Copy(SelectPuth, SelectPuth.Substring(0, SelectPuth.LastIndexOf(@".")) + "old.fdb", true);
                File.Delete(SelectPuth);
  
                StreamWriter file2 = new StreamWriter(PuthSetSrv + "aliases.conf", true);
                file2.WriteLine((Administrator.AdminPanels.SelectAlias+"old").Replace(" ","") + "=" + SelectPuth.Substring(0, SelectPuth.LastIndexOf(@".")) + "old.fdb");
                file2.Close();
                int Idcount = 0, Idcountout = 0;
                string[] fileLines = File.ReadAllLines(PuthSetSrv + "aliases.conf");
                string[] fileoutLines = new string[20];
                foreach (string x in fileLines)
                {
                    if (x.Count() != 0 && x.Length != 0)
                    {
                        fileoutLines[Idcountout] = fileLines[Idcount];
                        Idcountout++;
                    }
                    Idcount++;
                }
                File.WriteAllLines(PuthSetSrv + "aliases.conf", fileoutLines);
                ImageObj = "RestartServFB25";
                InstallB52.RestartFB25(PuthSetSrv, AppStart.TableReestr["NameServer25"]);
            }
            


            // перезапись архива БД с фтп серевера на диск c:\temp и распаковка
            if (!SystemConecto.File_(SelectPuth, 5))
            {

                try
                {
  
                    hubdate = AppStart.TableReestr["SetTextIp4BackOf"] + "/" + PortSrv + ":" + Administrator.AdminPanels.SelectAlias.Trim();
                    RunGbak = PuthSetSrv + @"bin\gbak.exe";
                    ArgumentCmd = " -rep " + AppStart.TableReestr["PutchBackUpLoc_Fbd25"] + " " + hubdate + @" -v  -bu 200 -user sysdba -pass " + CurrentPasswFb25; // + @" -y c:\temp\log.txt;

                    SystemConecto.ErorDebag(Administrator.AdminPanels.RunGbak + Administrator.AdminPanels.ArgumentCmd, 1, 2);

                    Process CmdDos = new Process();
                    CmdDos.StartInfo.FileName = RunGbak;
                    CmdDos.StartInfo.Arguments = ArgumentCmd;
                    CmdDos.StartInfo.UseShellExecute = false;
                    //CmdDos.StartInfo.CreateNoWindow = true;
                    //CmdDos.StartInfo.RedirectStandardOutput = true;
                    CmdDos.Start();
                    CmdDos.WaitForExit();
                    CmdDos.Close();
                }
                catch (Exception ex)
                {
                    SystemConecto.ErorDebag("При восстановлении БД из архива, возникло исключение: " + Environment.NewLine +
                        " === Message: " + ex.Message.ToString() + Environment.NewLine +
                        " === Exception: " + ex.ToString(), 1);
                    File.Copy(SelectPuth.Substring(0, SelectPuth.LastIndexOf(@".")) + "old.fdb", SelectPuth,  true);
                }

            }

            DBConecto.DBopenFBConectionMemory(SystemBD.ConnnStringSystem().ToString(), "", "FbSystem");
            string StrInsert = "INSERT INTO CONNECTIONBD25  values ('" + SelectPuth.Substring(0, SelectPuth.LastIndexOf(@".")) + "old.fdb" + "', '" + (SelectAlias + "old").Trim() + "', '" + AppStart.TableReestr["NameServer25"] + "', '" + PuthSetSrv + "', '','','')";
            DBConecto.UniQuery CountQuery = new DBConecto.UniQuery(StrInsert, "FB");
            CountQuery.UserQuery = string.Format(StrInsert, "CONNECTIONBD25");
            CountQuery.ExecuteUNIScalar();
            DBConecto.DBcloseFBConectionMemory("FbSystem");


            System.Windows.Application.Current.Dispatcher.Invoke(new Action(delegate ()
            {
                ConectoWorkSpace.WaitMessage ConectoWorkSpace_Off = AppStart.LinkMainWindow("WaitMessage_");
                ConectoWorkSpace_Off.Close();
            }));
            // процедура завершена успешно
            Administrator.AdminPanels.InsertConect(SelectPuth, AppStart.TableReestr["NameServer25"]);
           var TextWin = "Восстановление БД " + Environment.NewLine + "выполнено. ";
           int AutoCl = 1;
           int MesaggeT = -600;
           int MessageL = 0;
           InstallB52.MessageEnd(TextWin, AutoCl, MesaggeT, MessageL);
           System.Windows.Application.Current.Dispatcher.Invoke(new Action(delegate ()
           {
                ConectoWorkSpace.Administrator.AdminPanels ConectoWorkSpace_On = AppStart.LinkMainWindow("WAdminPanels");
                ConectoWorkSpace_On.Putch_Fbd25.Text = "";
               ConectoWorkSpace_On.RestoryBd25.Foreground = Brushes.White;
               ConectoWorkSpace_On.TablAlias.IsEnabled = true;

           }));
           Administrator.AdminPanels.UpdateKeyReestr("PutchBackUpLoc_Fbd25", "");
           InstallB52.CloseVisibilityButton();
           Administrator.AdminPanels.SetWinSetHub = "";
           InstallB52.IntThreadStart--;
        }

        public static void Restory30BDRunTH(object ThreadObj)
        {
            // Разбор аргументов
            AppStart.RenderInfo arguments = (AppStart.RenderInfo)ThreadObj;
            string SetAliasBD = arguments.argument1.Trim() + DateTime.Now.ToString("yyMMddHHmmss");
            string PuthSetSrv = arguments.argument3;
            string PuthBd = arguments.argument4;
            Administrator.AdminPanels.SelectAlias = arguments.argument1;
            string PortSrv = arguments.argument2;
            string Oldbd = PuthBd.Substring(0, PuthBd.LastIndexOf(@"\")+1) + SetAliasBD + ".fdb";
            StreamWriter file2 = new StreamWriter(PuthSetSrv + "databases.conf", true);
            file2.WriteLine(SetAliasBD + "=" + Oldbd);
            file2.Close();
            int Idcount = 0, Idcountout = 0;
            string[] fileLines = File.ReadAllLines(PuthSetSrv + "databases.conf");
            string[] fileoutLines = new string[100];
            foreach (string x in fileLines)
            {
                if (x.Count() != 0 && x.Length != 0)
                {
                    fileoutLines[Idcountout] = fileLines[Idcount];
                    Idcountout++;
                }
                Idcount++;
            }
            File.WriteAllLines(PuthSetSrv + "databases.conf", fileoutLines);
            ImageObj = "RestartServFB30";
            InstallB52.RestartFB25(PuthSetSrv, NameServer30);
            if (File.Exists(Oldbd)) File.Delete(Oldbd); 
            File.Copy(PuthBd, Oldbd, true);
            //File.Delete(PuthBd);
            hubdate = AppStart.TableReestr["SetTextIp4BackOf"] + "/" + PortSrv + ":" + Administrator.AdminPanels.SelectAlias;

            // отключение рользователей и перезапись архива БД с фтп серевера на диск c:\temp и распаковка

            RunGbak = PuthSetSrv + "gbak.exe";
            ArgumentCmd = @" -rep " + AppStart.TableReestr["PutchBackUpLoc_Fbd30"] + " " + hubdate + @" -v  -bu 200 -user sysdba -pass " + CurrentPasswFb30; //-y c:\temp\log.txt
            SystemConecto.ErorDebag(Administrator.AdminPanels.RunGbak + Administrator.AdminPanels.ArgumentCmd, 1, 2);

            try
            {
                Process CmdDos = new Process();
                CmdDos.StartInfo.FileName = RunGbak;
                CmdDos.StartInfo.Arguments = ArgumentCmd;
                CmdDos.StartInfo.UseShellExecute = false;
                //CmdDos.StartInfo.CreateNoWindow = true;
                //CmdDos.StartInfo.RedirectStandardOutput = true;
                CmdDos.Start();
                CmdDos.WaitForExit();
                CmdDos.Close();
            }
            catch (Exception ex)
            {
                SystemConecto.ErorDebag("При восстановлении БД из архива, возникло исключение: " + Environment.NewLine +
                    " === Message: " + ex.Message.ToString() + Environment.NewLine +
                    " === Exception: " + ex.ToString(), 1);
                File.Copy(SelectPuth.Substring(0, SelectPuth.LastIndexOf(@".")) + "old.fdb", SelectPuth, true);
            }

            DBConecto.DBopenFBConectionMemory(SystemBD.ConnnStringSystem().ToString(), "", "FbSystem");
            string StrInsert = "INSERT INTO CONNECTIONBD30  values ('" + Oldbd + "', '" + SetAliasBD + "', '" + NameServer30 + "', '" + PuthSetSrv + "','','','')";
            DBConecto.UniQuery CountQuery = new DBConecto.UniQuery(StrInsert, "FB");
            CountQuery.UserQuery = string.Format(StrInsert, "CONNECTIONBD30");
            CountQuery.ExecuteUNIScalar();
            DBConecto.DBcloseFBConectionMemory("FbSystem");
            Grid_Puth("SELECT * from CONNECTIONBD30 WHERE CONNECTIONBD30.NAMESERVER = " + "'" + NameServer30 + "'");

            System.Windows.Application.Current.Dispatcher.Invoke(new Action(delegate ()
            {
                ConectoWorkSpace.WaitMessage ConectoWorkSpace_Off = AppStart.LinkMainWindow("WaitMessage_");
                ConectoWorkSpace_Off.Close();
            }));
            // процедура завершена успешно
            Administrator.AdminPanels.InsertConect(SelectPuth, AppStart.TableReestr["NameServer25"]);
            var TextWin = "Восстановление БД " + Environment.NewLine + "выполнено. ";
            int AutoCl = 1;
            int MesaggeT = -600;
            int MessageL = 0;
            InstallB52.MessageEnd(TextWin, AutoCl, MesaggeT, MessageL);
            System.Windows.Application.Current.Dispatcher.Invoke(new Action(delegate ()
            {
                ConectoWorkSpace.Administrator.AdminPanels ConectoWorkSpace_On = AppStart.LinkMainWindow("WAdminPanels");
                ConectoWorkSpace_On.Putch_Fbd30.Text = "";
            }));
            Administrator.AdminPanels.UpdateKeyReestr("PutchBackUpLoc_Fbd30", "");
            InstallB52.CloseVisibilityButton30();
            InstallB52.IntThreadStart--;
        }

        // Процедура удаления строк из таблицы расписания архивирования БД
        private void Delete_Str_Backup_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            DeleteStr_Backup(25);
        }

        private void Delete_Str30_Backup_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            DeleteStr_Backup(30);
        }

        // Удаление записи из таблицы расписаний 
        public void DeleteStr_Backup(int Fb)
        {
            string  StrCreate = "DELETE FROM SCHEDULEARHIV WHERE SCHEDULEARHIV.PUTH = " + "'" + SchedulePuth + "'";
            DBConecto.DBopenFBConectionMemory(SystemBD.ConnnStringSystem().ToString(), "", "FbSystem");
            DBConecto.UniQuery DeletQuery = new DBConecto.UniQuery(StrCreate, "FB");
            DeletQuery.UserQuery = string.Format(StrCreate, "SCHEDULEARHIV");
            DeletQuery.ExecuteUNIScalar();
            DBConecto.DBcloseFBConectionMemory("FbSystem");
            ListGridArhiv(Fb);
            Delete_Str_Backup.IsEnabled = false;
            Change_Str_Backup.IsEnabled = false;
            Delete_Str_Backup30.IsEnabled = false;
            Change_Str_Backup30.IsEnabled = false;
        }


        private void ChangeStr_Backup_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            Administrator.AdminPanels.SetWinSetHub = "ChangeArhiv_Fbd25";
            if (SetArhivBd25.Visibility == Visibility.Visible)
            {
                SetArhivBd25.Visibility = Visibility.Collapsed;
                ArhivSetDay25.Visibility = Visibility.Collapsed;
                SetArhivBd225.Visibility = Visibility.Collapsed;
                SetArhivBd325.Visibility = Visibility.Collapsed;
                Hour25.Visibility = Visibility.Collapsed;
                sep125.Visibility = Visibility.Collapsed;
                Minute25.Visibility = Visibility.Collapsed;
                NameArhivBd25.Visibility = Visibility.Collapsed;
                Putch_Fbd25.Visibility = Visibility.Collapsed;
                DirPath_Fbd25.Visibility = Visibility.Collapsed;
                BackUpLocServerBD25.Visibility = Visibility.Collapsed;
                Delete_Str_Backup.IsEnabled = false;
                Change_Str_Backup.IsEnabled = false;

            }
            else ChangeStr_Backup(25);
        }

        private void ChangeStr30_Backup_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            Administrator.AdminPanels.SetWinSetHub = "ChangeArhiv_Fbd30";
            if (SetArhivBd3.Visibility == Visibility.Visible)
            {
                NameArhiv30.Visibility = Visibility.Collapsed;
                Putch_Fbd30.Visibility = Visibility.Collapsed;
                DirPath_Fbd30.Visibility = Visibility.Collapsed;
                BackUpLocServerBD30.Visibility = Visibility.Collapsed;
                SetArhivBd.Visibility = Visibility.Collapsed;
                ArhivSetDay.Visibility = Visibility.Collapsed;
                SetArhivBd2.Visibility = Visibility.Collapsed;
                SetArhivBd3.Visibility = Visibility.Collapsed;
                Hour.Visibility = Visibility.Collapsed;
                sep1.Visibility = Visibility.Collapsed;
                Minute.Visibility = Visibility.Collapsed;
                Delete_Str_Backup30.IsEnabled = false;
                Change_Str_Backup30.IsEnabled = false;
            }
            else ChangeStr_Backup(30);
        }

        private void Image_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            System.Windows.Application.Current.Dispatcher.Invoke(new Action(delegate ()
            {
                ConectoWorkSpace.MainWindow ConectoWorkSpace_InW = AppStart.LinkMainWindow("ConectoWorkSpace");
                ConectoWorkSpace_InW.WindowState = WindowState.Minimized;
            }));

        }
 

        public void ChangeStr_Backup(int Fb)
        {
            
            string CurrentSetBD = "SELECT * FROM SCHEDULEARHIV WHERE SCHEDULEARHIV.PUTH = " + "'" + SchedulePuth + "'";
            DBConecto.DBopenFBConectionMemory(SystemBD.ConnnStringSystem().ToString(), "", "FbSystem");
            FbCommand UpdateKey = new FbCommand(CurrentSetBD, DBConecto.bdFbSystemConect);
            UpdateKey.CommandType = CommandType.Text;
            FbDataReader reader = UpdateKey.ExecuteReader();
            while (reader.Read())
            {
                if (Administrator.AdminPanels.SetWinSetHub == "ChangeArhiv_Fbd30") Putch_Fbd30.Text = reader[1].ToString(); ArhivSetDay.Text = reader[2].ToString(); Hour.Text = reader[3].ToString().Substring(0, reader[3].ToString().LastIndexOf(":")); Minute.Text = reader[3].ToString().Substring(reader[3].ToString().IndexOf(":") + 1, reader[3].ToString().Length - reader[3].ToString().IndexOf(":") - 1);
                if (Administrator.AdminPanels.SetWinSetHub == "ChangeArhiv_Fbd25") Putch_Fbd25.Text = reader[1].ToString(); ArhivSetDay25.Text = reader[2].ToString(); Hour25.Text = reader[3].ToString().Substring(0, reader[3].ToString().LastIndexOf(":")); Minute25.Text = reader[3].ToString().Substring(reader[3].ToString().IndexOf(":") + 1, reader[3].ToString().Length - reader[3].ToString().IndexOf(":") - 1);
            }
            reader.Close();
            UpdateKey.Dispose();
            DBConecto.bdFbSystemConect.Close();
            if (Administrator.AdminPanels.SetWinSetHub == "ChangeArhiv_Fbd25")
            {
                Administrator.AdminPanels.UpdateKeyReestr("PutchBackUpLoc_Fbd25", Putch_Fbd25.Text);
                NameArhivBd25.Content = "Расписание архивирования для БД: " + SchedulePuth;
                SetArhivBd25.Visibility = Visibility.Visible;
                ArhivSetDay25.Visibility = Visibility.Visible;
                SetArhivBd225.Visibility = Visibility.Visible;
                SetArhivBd325.Visibility = Visibility.Visible;
                Hour25.Visibility = Visibility.Visible;
                sep125.Visibility = Visibility.Visible;
                Minute25.Visibility = Visibility.Visible;
                NameArhivBd25.Visibility = Visibility.Visible;
                Putch_Fbd25.Visibility = Visibility.Visible;
                DirPath_Fbd25.Visibility = Visibility.Visible;
                BackUpLocServerBD25.Visibility = Visibility.Visible;
            }


            if (Administrator.AdminPanels.SetWinSetHub == "ChangeArhiv_Fbd30")
            {
                Administrator.AdminPanels.UpdateKeyReestr("PutchBackUpLoc_Fbd30", Putch_Fbd30.Text);
                NameArhiv30.Visibility = Visibility.Visible;
                Putch_Fbd30.Visibility = Visibility.Visible;
                DirPath_Fbd30.Visibility = Visibility.Visible;
                BackUpLocServerBD30.Visibility = Visibility.Visible;
                SetArhivBd.Visibility = Visibility.Visible;
                ArhivSetDay.Visibility = Visibility.Visible;
                SetArhivBd2.Visibility = Visibility.Visible;
                SetArhivBd3.Visibility = Visibility.Visible;
                Hour.Visibility = Visibility.Visible;
                sep1.Visibility = Visibility.Visible;
                Minute.Visibility = Visibility.Visible;


            }

        }

        private void RestoryFtpBD30_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {

        }

  

        private void RestoryGoogleBD30_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {

        }

 

  
  


        // Процедуры установки соединения с БД  клиента для  MsSQL
  
 
        //  1.4. PostMsSQL
        private void SetPuthBDMsSQL_User_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {

        }
        // Процедуры выбора пути размещения папки с установленной БД


        private void DirSetBDMsSQL_User_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

        }

        private void PuthSetBDMsSQL_User_KeyUp(object sender, KeyEventArgs e)
        {

        }

  
        private void PaswordSetMsSQL_User_KeyUp(object sender, KeyEventArgs e)
        {

        }




        // Процедуры размещение BackUp БД для серверов  MsSQL
        // 1. Локальное, на текущем компьютере, размещение BackUp БД .

  

        //  1.2.Включить / выключить FireBird 3.0
 

        //  1.4.Включить / выключить MsSQL
        private void BackUpLocServerBDMsSQL_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            BackUpOnOffServerBD("BackUpLocServerBDMsSQL", "PutchBackUpLocMsSQL", BackUpLocServerBDMsSQL, PutchBackUpLocMsSQL, DirPathBuckUpMsSQL);
        }

        // 2. Размещение BackUp БД на ФТП сервере. 
   
    
        //  2.4.Включить / выключить MsSQL
        private void BackUpOnOffFTPBDMsSQL_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            BackUpOnOffServerBD("BackUpOnOffFTPBDMsSQL", "PutchBackUpFTP_MsSQL", BackUpOnOffFTPBDMsSQL, PutchBackUpFTP_MsSQL, DirPathBuckUpFTP_MsSQL);
        }


  

        private void CreateBackUpBd25_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.IsEnableButonFalse("CreateBackUpBd25");
            this.BackUpLocServerBD25.IsEnabled = false;
            if (AdminPanels.SetWinSetHub == "BackUpLoc_Fbd25")
            {
                InstallB52.CloseVisibilityButton();
                this.CreateBackUpBd25.Foreground = (Brush)Brushes.White;
                this.ButtonIsEnableFalse();
                AdminPanels.SetWinSetHub = "";
            }
            else
            {
                AdminPanels.SetWinSetHub = "BackUpLoc_Fbd25";
                this.CreateBackUpBd25.Foreground = (Brush)Brushes.Indigo;
                this.ButtonVisible();
            }
        }

        private void CreateDateArhivBd25_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (Administrator.AdminPanels.SetWinSetHub == "DateArhiv_Fbd25")
            {
                InstallB52.CloseVisibilityButton();
                CreateListArhivBd25.Foreground = Brushes.White;
                ButtonIsEnableFalse();
                Administrator.AdminPanels.SetWinSetHub = "";
                return;
            }
            Administrator.AdminPanels.SetWinSetHub = "DateArhiv_Fbd25";
            CreateListArhivBd25.Foreground = Brushes.Indigo;
            CreateBackUpBd25.IsEnabled = RestoryBd25.IsEnabled = ChangeFbd2530.IsEnabled = false;
            ButtonVisible();
        }

        private void RestoryFb25_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            BackUpLocServerBD25.IsEnabled = false;
            if (Administrator.AdminPanels.SetWinSetHub == "Restory_Fbd25")
            {
                InstallB52.CloseVisibilityButton();
                RestoryBd25.Foreground = Brushes.White;
                ButtonIsEnableFalse();
                Administrator.AdminPanels.SetWinSetHub = "";
                return;
            }
            Administrator.AdminPanels.SetWinSetHub = "Restory_Fbd25";
            RestoryBd25.Foreground = Brushes.Indigo;
            CreateBackUpBd25.IsEnabled = ChangeFbd2530.IsEnabled = CreateListArhivBd25.IsEnabled = false;
            ButtonVisible();
        }

        private void BackUpRestory2530_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.IsEnableButonFalse("ChangeFbd2530");
            this.BackUpLocServerBD25.IsEnabled = false;
            if (AdminPanels.SetWinSetHub == "BackUpRestory_Fbd2530")
            {
                InstallB52.CloseVisibilityButton();
                this.ChangeFbd2530.Foreground = (Brush)Brushes.White;
                this.ButtonIsEnableFalse();
                AdminPanels.SetWinSetHub = "";
            }
            else
            {
                AdminPanels.SetWinSetHub = "BackUpRestory_Fbd2530";
                this.ChangeFbd2530.Foreground = (Brush)Brushes.Indigo;
                this.ButtonVisible();
            }
        }

        public void IsEnableButonFalse(string NameButon = "")
        {
            if (NameButon != "CreateBackUpBd25")
                this.CreateBackUpBd25.IsEnabled = false;
            if (NameButon != "RestoryBd25")
                this.RestoryBd25.IsEnabled = false;
            if (NameButon != "ChangeFbd2530")
                this.ChangeFbd2530.IsEnabled = false;
            if (NameButon != "CreateListArhivBd25")
                this.CreateListArhivBd25.IsEnabled = false;
            if (NameButon != "UpgradeBD25")
                this.UpgradeBD25.IsEnabled = false;
            if (NameButon != "UpdateBD25")
                this.UpdateBD25.IsEnabled = false;
            if (NameButon != "GoNewPeriod")
                this.GoNewPeriod.IsEnabled = false;
            if (NameButon != "OnOffLog")
                this.OnOffLog.IsEnabled = false;
            if (NameButon != "LabelCopyLog")
                this.LabelCopyLog.IsEnabled = false;
            if (!(NameButon != "Satellite"))
                return;
            this.Satellite.IsEnabled = false;
        }

        public void IsEnableButonTrue(string NameButon = "")
        {
            if (NameButon != "CreateBackUpBd25")
                this.CreateBackUpBd25.IsEnabled = true;
            if (NameButon != "RestoryBd25")
                this.RestoryBd25.IsEnabled = true;
            if (NameButon != "ChangeFbd2530")
                this.ChangeFbd2530.IsEnabled = true;
            if (NameButon != "CreateListArhivBd25")
                this.CreateListArhivBd25.IsEnabled = true;
            if (NameButon != "UpgradeBD25")
                this.UpgradeBD25.IsEnabled = true;
            if (NameButon != "UpdateBD25")
                this.UpdateBD25.IsEnabled = true;
            if (NameButon != "GoNewPeriod")
                this.GoNewPeriod.IsEnabled = true;
            if (NameButon != "OnOffLog")
                this.OnOffLog.IsEnabled = true;
            if (NameButon != "LabelCopyLog")
                this.LabelCopyLog.IsEnabled = true;
            if (!(NameButon != "Satellite"))
                return;
            this.Satellite.IsEnabled = true;
        }

        public void IsEnableButonFalse30(string NameButon = "")
        {
            if (NameButon != "CreateListArhivBd30")
                this.CreateListArhivBd30.IsEnabled = false;
            if (NameButon != "CreateBackUpBd30")
                this.CreateBackUpBd30.IsEnabled = false;
            if (NameButon != "RestoryBd30")
                this.RestoryBd30.IsEnabled = false;
            if (NameButon != "LabelCopyLog30")
                this.LabelCopyLog30.IsEnabled = false;
            if (NameButon != "UpdateBD30")
                this.UpdateBD30.IsEnabled = false;
            if (!(NameButon != "GoNewPeriod30"))
                return;
            this.GoNewPeriod30.IsEnabled = false;
        }

        public void ButtonIsEnableFalse()
        {
            CreateBackUpBd25.IsEnabled = false;
            RestoryBd25.IsEnabled = false;
            ChangeFbd2530.IsEnabled = false;
            CreateListArhivBd25.IsEnabled = false;
            UpdateBD25.IsEnabled = false;
            GoNewPeriod.IsEnabled = false;
            LocDisk.Visibility = Visibility.Collapsed;
            GoogleDisk.Visibility = Visibility.Collapsed;
            FtpDisk.Visibility = Visibility.Collapsed;
            SetArhivBd25.Visibility = Visibility.Collapsed;
            ArhivSetDay25.Visibility = Visibility.Collapsed;
            SetArhivBd225.Visibility = Visibility.Collapsed;
            SetArhivBd325.Visibility = Visibility.Collapsed;
            Hour25.Visibility = Visibility.Collapsed;
            sep125.Visibility = Visibility.Collapsed;
            Minute25.Visibility = Visibility.Collapsed;
            NameArhivBd25.Visibility = Visibility.Collapsed;
        }

        public void ButtonVisible()
        {
            if (LocDisk.Visibility == Visibility.Collapsed )
            {
                LocDisk.Visibility = Visibility.Visible;
                GoogleDisk.Visibility = Visibility.Visible;
                FtpDisk.Visibility = Visibility.Visible;
                if (Administrator.AdminPanels.SetWinSetHub == "DateArhiv_Fbd25")
                {
                    NameArhivBd25.Content = "Расписание архивирования для БД: " + SelectPuth;
                    NameArhivBd25.Visibility = Visibility.Visible;
                }
            }
            else
            {
                LocDisk.Visibility = Visibility.Collapsed;
                GoogleDisk.Visibility = Visibility.Collapsed;
                FtpDisk.Visibility = Visibility.Collapsed;
                NameArhiv.Visibility = Visibility.Collapsed;
                Putch_Fbd25.Visibility = Visibility.Collapsed;
                DirPath_Fbd25.Visibility = Visibility.Collapsed;
                BackUpLocServerBD25.Visibility = Visibility.Collapsed;

                SetArhivBd25.Visibility = Visibility.Collapsed;
                ArhivSetDay25.Visibility = Visibility.Collapsed;
                SetArhivBd225.Visibility = Visibility.Collapsed;
                SetArhivBd325.Visibility = Visibility.Collapsed;
                Hour25.Visibility = Visibility.Collapsed;
                sep125.Visibility = Visibility.Collapsed;
                Minute25.Visibility = Visibility.Collapsed;
                NameArhivBd25.Visibility = Visibility.Collapsed;
            }
        }

        private void ArhivSetDay_KeyUp(object sender, KeyEventArgs e)
        {
            if (ArhivSetDay.Text.Length == 0) return;
            int Indexfor =  ArhivSetDay.Text.Length;
            for (int indPoint = Indexfor; indPoint <= ArhivSetDay.Text.Length; indPoint++)
            {
                if (ArhivSetDay.Text.Substring(indPoint-1,1)!="1" && ArhivSetDay.Text.Substring(indPoint - 1, 1) != "2" && ArhivSetDay.Text.Substring(indPoint - 1, 1) != "3"
                    && ArhivSetDay.Text.Substring(indPoint - 1, 1) != "4" && ArhivSetDay.Text.Substring(indPoint - 1, 1) != "5" && ArhivSetDay.Text.Substring(indPoint - 1, 1) != "6"
                    && ArhivSetDay.Text.Substring(indPoint - 1, 1) != "7" && ArhivSetDay.Text.Substring(indPoint - 1, 1) != "8" && ArhivSetDay.Text.Substring(indPoint - 1, 1) != "9"
                    && ArhivSetDay.Text.Substring(indPoint - 1, 1) != "0") ArhivSetDay.Text =  "";
            }
                
        }

        private void Hour_KeyUp(object sender, KeyEventArgs e)
        {
            if (Hour.Text.Length == 0) return;
            if (Hour.Text.Length > 2) Hour.Text = Hour.Text.Substring(0,2);
            int Indexfor = Hour.Text.Length;
            for (int indPoint = Indexfor; indPoint <= Hour.Text.Length; indPoint++)
            {
                if (Hour.Text.Substring(indPoint - 1, 1) != "1" && Hour.Text.Substring(indPoint - 1, 1) != "2" && Hour.Text.Substring(indPoint - 1, 1) != "3"
                    && Hour.Text.Substring(indPoint - 1, 1) != "4" && Hour.Text.Substring(indPoint - 1, 1) != "5" && Hour.Text.Substring(indPoint - 1, 1) != "6"
                    && Hour.Text.Substring(indPoint - 1, 1) != "7" && Hour.Text.Substring(indPoint - 1, 1) != "8" && Hour.Text.Substring(indPoint - 1, 1) != "9"
                    && Hour.Text.Substring(indPoint - 1, 1) != "0") Hour.Text = "";
            }
        }

        private void Minute_KeyUp(object sender, KeyEventArgs e)
        {
            if (Minute.Text.Length == 0) return;
            if (Minute.Text.Length > 2) Minute.Text = Minute.Text.Substring(0, 2);
            int Indexfor = Minute.Text.Length;
            for (int indPoint = Indexfor; indPoint <= Minute.Text.Length; indPoint++)
            {
                if (Minute.Text.Substring(indPoint - 1, 1) != "1" && Minute.Text.Substring(indPoint - 1, 1) != "2" && Minute.Text.Substring(indPoint - 1, 1) != "3"
                    && Minute.Text.Substring(indPoint - 1, 1) != "4" && Minute.Text.Substring(indPoint - 1, 1) != "5" && Minute.Text.Substring(indPoint - 1, 1) != "6"
                    && Minute.Text.Substring(indPoint - 1, 1) != "7" && Minute.Text.Substring(indPoint - 1, 1) != "8" && Minute.Text.Substring(indPoint - 1, 1) != "9"
                    && Minute.Text.Substring(indPoint - 1, 1) != "0") Minute.Text = "";
            }
        }
        private void LocDisk_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
 
            if (Administrator.AdminPanels.SetWinSetHub == "BackUpLoc_Fbd25")
            {
                BackUpLocServerBD25.IsEnabled = true;
                if (!Directory.Exists(@"c:\BackupFdb" ))Directory.CreateDirectory(@"c:\BackupFdb");

                this.Putch_Fbd25.Text = new FileInfo("c:\\BackupFdb\\" + AdminPanels.SelectPuth.Substring(AdminPanels.SelectPuth.LastIndexOf("\\") + 1, AdminPanels.SelectPuth.LastIndexOf(".") - (AdminPanels.SelectPuth.LastIndexOf("\\") + 1)) + "\\" + AdminPanels.SelectPuth.Substring(AdminPanels.SelectPuth.LastIndexOf("\\") + 1, AdminPanels.SelectPuth.LastIndexOf(".") - (AdminPanels.SelectPuth.LastIndexOf("\\") + 1)) + DateTime.Now.ToString("yyMMddHHmmss") + ".7z").ToString();
                AdminPanels.UpdateKeyReestr("SetDirBackUp25", "c:\\BackupFdb\\" + AdminPanels.SelectPuth.Substring(AdminPanels.SelectPuth.LastIndexOf("\\") + 1, AdminPanels.SelectPuth.LastIndexOf(".") - (AdminPanels.SelectPuth.LastIndexOf("\\") + 1)) + "\\");
            }
            // Restory Bd
            if (Administrator.AdminPanels.SetWinSetHub == "Restory_Fbd25")
            {
                Putch_Fbd25.Text = "";
                string[] findFiles = System.IO.Directory.GetFiles(@"C:\BackupFdb", SelectPuth.Substring(SelectPuth.LastIndexOf(@"\") + 1, (SelectPuth.LastIndexOf(".") - (SelectPuth.LastIndexOf(@"\") + 1)))+"*.fbk", System.IO.SearchOption.AllDirectories);
                if (findFiles.Length != 0)
                {
                    foreach (string file in findFiles)
                    {
                        Putch_Fbd25.Text = file;
                    }
                    BackUpLocServerBD25.IsEnabled = true;
                }
            }
            if (Administrator.AdminPanels.SetWinSetHub == "BackUpLoc_Fbd30")
            {
                Putch_Fbd30.Text = @"c:\BackupFdb\" + SelectPuth.Substring(SelectPuth.LastIndexOf(@"\") + 1, (SelectPuth.LastIndexOf(".") - (SelectPuth.LastIndexOf(@"\") + 1))) + DateTime.Now.ToString("yyMMddHHmmss") + ".7z";
                Administrator.AdminPanels.UpdateKeyReestr("SetDirBackUp30", @"c:\BackupFdb\");
                BackUpLocServerBD30.IsEnabled = true;
            }
            // Restory Bd
            if (Administrator.AdminPanels.SetWinSetHub == "Restory_Fbd30")
            {
                Putch_Fbd30.Text = "";
                string[] findFiles = System.IO.Directory.GetFiles(@"C:\BackupFdb", SelectPuth.Substring(SelectPuth.LastIndexOf(@"\") + 1, (SelectPuth.LastIndexOf(".") - (SelectPuth.LastIndexOf(@"\") + 1))) + "*.fbk", System.IO.SearchOption.AllDirectories);
                if (findFiles.Length != 0)
                {
                    foreach (string file in findFiles)
                    {
                        Putch_Fbd30.Text = file;
                    }
                    BackUpLocServerBD30.IsEnabled = true;
                }
            }
            // конвертация 2.5-3.0
            if (Administrator.AdminPanels.SetWinSetHub == "BackUpRestory_Fbd2530")
            {
                Putch_Fbd25.Text = SelectPuth.Substring(0, SelectPuth.LastIndexOf(@"\") + 1) + SelectPuth.Substring(SelectPuth.LastIndexOf(@"\") + 1, SelectPuth.IndexOf(@".") - (SelectPuth.LastIndexOf(@"\") + 1)) + "3.fdb";
                if (File.Exists(Putch_Fbd25.Text) == true)
                {
                    string TextMesage = "Файл в формате Firebird 3.0 в текущей папке существует." + Environment.NewLine + " Обновить ?";
                    Inst2530 = "BackUpRestory";
                    WinOblakoSetUpdate OblakoSetWindow = new WinOblakoSetUpdate(TextMesage);
                    OblakoSetWindow.Owner = this;  //AddOwnedForm(OblakoNizWindow);
                    OblakoSetWindow.Top = (this.Top + 7) + this.Close_F.Margin.Top + (this.Close_F.Height - 2) + 10;
                    OblakoSetWindow.Left = (this.Left) + this.Close_F.Margin.Left - (OblakoSetWindow.Width - 22) + 1300;
                    // Отображаем
                    OblakoSetWindow.Show();
                    return;
                }
                BackUpLocServerBD25.IsEnabled = true;
            }
            if (Administrator.AdminPanels.SetWinSetHub == "DateArhiv_Fbd30" || Administrator.AdminPanels.SetWinSetHub == "DateArhiv_Fbd25")
            {
                if (Administrator.AdminPanels.SetWinSetHub == "DateArhiv_Fbd25")
                {
                    Putch_Fbd25.Text = @"c:\BackupFdb\";
                    ArhivSetDay25.Text = "5";
                    Hour25.Text = "7";
                    Minute25.Text = "00";
                }
                else
                {
                    Putch_Fbd30.Text = @"c:\BackupFdb\";
                    ArhivSetDay.Text = "5";
                    Hour.Text = "7";
                    Minute.Text = "00";
                }
 
                string InsertExecute = "SELECT count(*) from SCHEDULEARHIV";
                DBConecto.DBopenFBConectionMemory(SystemBD.ConnnStringSystem().ToString(), "", "FbSystem");
                DBConecto.UniQuery CountQuery = new DBConecto.UniQuery(InsertExecute, "FB");
                string CountTable = CountQuery.ExecuteUNIScalar() == null ? "" : CountQuery.ExecuteUNIScalar().ToString();
                if (Convert.ToUInt32(CountTable) != 0)
                {
                    string CurrentSetBD = "SELECT * FROM SCHEDULEARHIV WHERE SCHEDULEARHIV.PUTH = " + "'" + SelectPuth + "'";
                    DBConecto.DBopenFBConectionMemory(SystemBD.ConnnStringSystem().ToString(), "", "FbSystem");
                    FbCommand UpdateKey = new FbCommand(CurrentSetBD, DBConecto.bdFbSystemConect);
                    UpdateKey.CommandType = CommandType.Text;
                    FbDataReader reader = UpdateKey.ExecuteReader();
                    while (reader.Read())
                    {
                        if (Administrator.AdminPanels.SetWinSetHub == "DateArhiv_Fbd30")Putch_Fbd30.Text = reader[1].ToString(); ArhivSetDay.Text = reader[2].ToString(); Hour.Text = reader[3].ToString().Substring(0, reader[3].ToString().LastIndexOf(":")); Minute.Text = reader[3].ToString().Substring(reader[3].ToString().IndexOf(":")+1 , reader[3].ToString().Length - reader[3].ToString().IndexOf(":")-1);
                        if (Administrator.AdminPanels.SetWinSetHub == "DateArhiv_Fbd25") Putch_Fbd25.Text = reader[1].ToString(); ArhivSetDay25.Text = reader[2].ToString(); Hour25.Text = reader[3].ToString().Substring(0, reader[3].ToString().LastIndexOf(":")); Minute25.Text = reader[3].ToString().Substring(reader[3].ToString().IndexOf(":") + 1, reader[3].ToString().Length - reader[3].ToString().IndexOf(":") - 1);
                    }
                    reader.Close();
                    UpdateKey.Dispose();
                    DBConecto.bdFbSystemConect.Close();
                }
                BackUpLocServerBD30.IsEnabled = true;

            }

            if (Administrator.AdminPanels.SetWinSetHub == "BackUpLoc_Fbd30" || Administrator.AdminPanels.SetWinSetHub == "Restory_Fbd30" || Administrator.AdminPanels.SetWinSetHub == "DateArhiv_Fbd30" )
            {

                    NameArhiv30.Visibility = Visibility.Visible;
                    Putch_Fbd30.Visibility = Visibility.Visible;
                    DirPath_Fbd30.Visibility = Visibility.Visible;
                    BackUpLocServerBD30.Visibility = Visibility.Visible;
 
                if (Administrator.AdminPanels.SetWinSetHub == "DateArhiv_Fbd30")
                {
                    SetArhivBd.Visibility = Visibility.Visible;
                    ArhivSetDay.Visibility = Visibility.Visible;
                    SetArhivBd2.Visibility = Visibility.Visible;
                    SetArhivBd3.Visibility = Visibility.Visible;
                    Hour.Visibility = Visibility.Visible;
                    sep1.Visibility = Visibility.Visible;
                    Minute.Visibility = Visibility.Visible;
                }
 

                if (Administrator.AdminPanels.SetWinSetHub == "BackUpLoc_Fbd30" || Administrator.AdminPanels.SetWinSetHub == "Restory_Fbd30")
                {
                    Administrator.AdminPanels.UpdateKeyReestr("PutchBackUpLoc_Fbd30", Putch_Fbd30.Text);
                }
 
            }
            else
            {
               if (Administrator.AdminPanels.SetWinSetHub == "DateArhiv_Fbd25")
               {
                    SetArhivBd25.Visibility = Visibility.Visible;
                    ArhivSetDay25.Visibility = Visibility.Visible;
                    SetArhivBd225.Visibility = Visibility.Visible;
                    SetArhivBd325.Visibility = Visibility.Visible;
                    Hour25.Visibility = Visibility.Visible;
                    sep125.Visibility = Visibility.Visible;
                    Minute25.Visibility = Visibility.Visible;
               }
                Administrator.AdminPanels.UpdateKeyReestr("PutchBackUpLoc_Fbd25", Putch_Fbd25.Text);
                NameArhiv.Visibility = Visibility.Visible;
                Putch_Fbd25.Visibility = Visibility.Visible;
                DirPath_Fbd25.Visibility = Visibility.Visible;
                BackUpLocServerBD25.Visibility = Visibility.Visible;
            }
 
        }

        private void GoogleDisk_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {

        }

        private void FtpDisk_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            //string PuthSetBackUp7z = @"c:\BackupFdb\B52\B52190826183528.7z";
            //string FileName = "B52190826183528.7z";
            //     FtpWebRequest ftpClient = (FtpWebRequest)FtpWebRequest.Create("ftp://conecto.ua/pack/BackUp/bd/" + FileName);
            //        ftpClient.Credentials = new System.Net.NetworkCredential(AppStart.aParamApp["ServerUpdateConecto_USER"], AppStart.aParamApp["ServerUpdateConecto_USER-Passw"]);
            //        ftpClient.Method = System.Net.WebRequestMethods.Ftp.UploadFile;
            //        ftpClient.UseBinary = true;
            //        ftpClient.KeepAlive = true;
            //        System.IO.FileInfo fi = new System.IO.FileInfo(PuthSetBackUp7z);
            //        ftpClient.ContentLength = fi.Length;
            //        byte[] buffer = new byte[4097];
            //        int bytes = 0;
            //        int total_bytes = (int)fi.Length;
            //        System.IO.FileStream fs = fi.OpenRead();
            //        System.IO.Stream rs = ftpClient.GetRequestStream();
            //        while (total_bytes > 0)
            //        {
            //            bytes = fs.Read(buffer, 0, buffer.Length);
            //            rs.Write(buffer, 0, bytes);
            //            total_bytes = total_bytes - bytes;
            //        }
            //        fs.Close();
            //        rs.Close();
            //        FtpWebResponse uploadResponse = (FtpWebResponse)ftpClient.GetResponse();
            //        var value = uploadResponse.StatusDescription;
            //        uploadResponse.Close();



            Administrator.AdminPanels.SetWinSetHub = Administrator.AdminPanels.SetWinSetHub == "BackUpLoc_Fbd25" ? "BackUpFTP_Fbd25" : "RestoryFTP_Fbd25";
            Putch_Fbd25.Text = @"ftp://conecto.ua/pack/BackUp/bd/";

            Administrator.AdminPanels.UpdateKeyReestr("PutchBackUpFtp_Fbd25", Putch_Fbd25.Text);
            NameArhiv.Visibility = Visibility.Visible;
            Putch_Fbd25.Visibility = Visibility.Visible;
            DirPath_Fbd25.Visibility = Visibility.Visible;
            DirPath_Fbd25.IsEnabled = false;
            BackUpLocServerBD25.Visibility = Visibility.Visible;
            BackUpLocServerBD25.IsEnabled = true;

            //@"conecto.ua/pack/"
            //AppStart.aParamApp.Add("ServerUpdateConecto_USER", "update_workspace");
            //AppStart.aParamApp.Add("ServerUpdateConecto_USER-Passw", "conect1074");  // User-Pass2012  
            //if (AppStart.ConnectionAvailable())
            //{
            //    // updatework.conecto.ua Чтение паролей из настроек ПО WriterConfigUserXML (Пользователь устанавливается на стороне сервера)
            //    // updatework.conecto.ua/updatework.conecto.ua/ "update_workspace" "conect1074"
            //    var rezultConectionFTP = AppStart.ConntecionFTP(@"conecto.ua/pack/" + kvPutchFIsPack.Value + MemFilePuthPack.NameFile + ".gz", aParamApp["ServerUpdateConecto_USER"], aParamApp["ServerUpdateConecto_USER-Passw"], 2, MemFilePuthPack.FulTempNameFileGZ);

            //    if (rezultConectionFTP != null && File_(MemFilePuthPack.FulTempNameFileGZ, 5))
            //}
            //string OdecaUri = @"ftp://195.138.94.90/bin/";
            //string NameUser = "partner";
            //string PasswdUser = "cnelbzgk.c";

            //if (SystemConecto.ConnectionAvailable())
            //{
            //    for (int i = 0; i <= AppUpdateCount; i++)
            //    {
            //        Uri UriServer = new Uri(OdecaUri + AppUpdate[i]);
            //        string StringServer = @"195.138.94.90/bin/" + AppUpdate[i];
            //        if (!System.IO.File.Exists(SystemConecto.PutchApp + @"Repository\" + AppUpdate[i]))
            //        {
            //            // Первая загрузка в репозитарий  B52BackOffice8.exe.
            //var ConectionFTPUpdate = SystemConecto.ConntecionFTP(StringServer, NameUser, PasswdUser, 2, SystemConecto.PutchApp + @"Repository\" + AppUpdate[i]);
            //            if (ConectionFTPUpdate == null)
            //            {
            //                var TextWindows = "Отсутствует " + AppUpdate[i] + Environment.NewLine + "Обновление невыполено. ";
            //                MainWindow.MessageInstall(TextWindows);
            //            }
            //        }
            //        else
            //        {
            //            // проверка даты последней модификации
            //            //FtpWebRequest request = (FtpWebRequest)WebRequest.Create(UriServer);
            //            //request.Method = WebRequestMethods.Ftp.GetDateTimestamp;
            //            //request.Credentials = new NetworkCredential(NameUser, PasswdUser);
            //            //FtpWebResponse response = (FtpWebResponse)request.GetResponse();
            //            //DateTime UpdateB52BD = response.LastModified;
            //            //response.Close();
            //            // считываем с фтп сервера.
            //            // Проверка контрольной суммы размера файла.B52BackOffice8.exe

            //            WebRequest FileChecksum = WebRequest.Create(UriServer);
            //            FileChecksum.Credentials = new NetworkCredential(NameUser, PasswdUser);
            //            WebResponse fs1 = FileChecksum.GetResponse();
            //            var fs = fs1.GetResponseStream();
            //            var md5 = MD5.Create();
            //            byte[] checkSum = md5.ComputeHash(fs);
            //            string Ftpresult = BitConverter.ToString(checkSum).Replace("-", String.Empty);

            //        }
            //    }
            //}
            // Запись на фтп сервер файла любого типа.
            //using (StreamReader stream = new StreamReader("C:\\" + fileName))
            //{
            //    byte[] buffer = Encoding.Default.GetBytes(stream.ReadToEnd());

            //    WebRequest request = WebRequest.Create("ftp://" + ftpAddress + "/" + "myfolder" + "/" + fileName);
            //    request.Method = WebRequestMethods.Ftp.UploadFile;
            //    request.Credentials = new NetworkCredential(username, password);
            //    Stream reqStream = request.GetRequestStream();
            //    reqStream.Write(buffer, 0, buffer.Length);
            //    reqStream.Close();
            //}
            //return true;


            //Следующий script отлично работает со мной для загрузки файлов и видео на другого сервера через ftp.

            //FtpWebRequest ftpClient = (FtpWebRequest)FtpWebRequest.Create(ftpurl + "" + username + "_" + filename);
            //ftpClient.Credentials = new System.Net.NetworkCredential(ftpusername, ftppassword);
            //ftpClient.Method = System.Net.WebRequestMethods.Ftp.UploadFile;
            //ftpClient.UseBinary = true;
            //ftpClient.KeepAlive = true;
            //System.IO.FileInfo fi = new System.IO.FileInfo(fileurl);
            //ftpClient.ContentLength = fi.Length;
            //byte[] buffer = new byte[4097];
            //int bytes = 0;
            //int total_bytes = (int)fi.Length;
            //System.IO.FileStream fs = fi.OpenRead();
            //System.IO.Stream rs = ftpClient.GetRequestStream();
            //while (total_bytes > 0)
            //{
            //    bytes = fs.Read(buffer, 0, buffer.Length);
            //    rs.Write(buffer, 0, bytes);
            //    total_bytes = total_bytes - bytes;
            //}
            ////fs.Flush();
            //fs.Close();
            //rs.Close();
            //FtpWebResponse uploadResponse = (FtpWebResponse)ftpClient.GetResponse();
            //value = uploadResponse.StatusDescription;
            //uploadResponse.Close();

            // Создать папку на фтп

            //WebRequest request = WebRequest.Create("ftp://host.com/directory");
            //request.Method = WebRequestMethods.Ftp.MakeDirectory;
            //request.Credentials = new NetworkCredential("user", "pass");
            //var resp = (FtpWebResponse)request.GetResponse() ;

        }


        private void FtpDisk30_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
  

            Administrator.AdminPanels.SetWinSetHub = Administrator.AdminPanels.SetWinSetHub == "BackUpLoc_Fbd30" ? "BackUpFTP_Fbd30" : "RestoryFTP_Fbd30";
            Putch_Fbd30.Text = @"ftp://conecto.ua/pack/BackUp/bd/";

            Administrator.AdminPanels.UpdateKeyReestr("PutchBackUpFtp_Fbd30", Putch_Fbd30.Text);
            NameArhiv.Visibility = Visibility.Visible;
            Putch_Fbd30.Visibility = Visibility.Visible;
            DirPath_Fbd30.Visibility = Visibility.Visible;
            DirPath_Fbd30.IsEnabled = false;
            BackUpLocServerBD30.Visibility = Visibility.Visible;
            BackUpLocServerBD30.IsEnabled = true;

         
        }


        private void CreateBackUpBd30_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

            Administrator.AdminPanels.SetWinSetHub = "BackUpLoc_Fbd30";
            ButtonVisible30();
        }
        private void CreateDateArhivBd30_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
 
            Administrator.AdminPanels.SetWinSetHub = "DateArhiv_Fbd30";
            ButtonVisible30();
        }

        private void RestoryFb30_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            BackUpLocServerBD30.IsEnabled = false;
            Administrator.AdminPanels.SetWinSetHub = "Restory_Fbd30";
            ButtonVisible30();
        }

        public void ButtonVisible30()
        {
            if (LocDisk_30.Visibility == Visibility.Collapsed)
            {
                LocDisk_30.Visibility = Visibility.Visible;
                GoogleDisk_30.Visibility = Visibility.Visible;
                FtpDisk_30.Visibility = Visibility.Visible;
                if (Administrator.AdminPanels.SetWinSetHub == "DateArhiv_Fbd30")
                {
                    NameArhivBd2.Content = "Расписание архивирования для БД: " + SelectPuth;
                    NameArhivBd2.Visibility = Visibility.Visible;
                }

            }
            else
            {
                LocDisk_30.Visibility = Visibility.Collapsed;
                GoogleDisk_30.Visibility = Visibility.Collapsed;
                FtpDisk_30.Visibility = Visibility.Collapsed;
                NameArhiv30.Visibility = Visibility.Collapsed;
                Putch_Fbd30.Visibility = Visibility.Collapsed;
                DirPath_Fbd30.Visibility = Visibility.Collapsed;
                BackUpLocServerBD30.Visibility = Visibility.Collapsed;
                NameArhivBd2.Visibility = Visibility.Collapsed;

                SetArhivBd.Visibility = Visibility.Collapsed;
                ArhivSetDay.Visibility = Visibility.Collapsed;
                SetArhivBd2.Visibility = Visibility.Collapsed;
                SetArhivBd3.Visibility = Visibility.Collapsed;
                Hour.Visibility = Visibility.Collapsed;
                sep1.Visibility = Visibility.Collapsed;
                Minute.Visibility = Visibility.Collapsed;
            }
        }

 

         //  3.4.Включить / выключить MsSQL
        private void BackUpGoogleDriveOnOff_MsSQL_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            BackUpOnOffServerBD("BackUpGoogleDriveOnOff_MsSQL", "PutchBackUpGoogleDrive_MsSQL", BackUpGoogleDriveOnOff_MsSQL, PutchBackUpGoogleDrive_MsSQL, DirPathBuckUpGoogleDrive_MsSQL);
        }

  
        // Процедура ввода нового пароля
        private void OldPasswABD25_KeyUp(object sender, KeyEventArgs e)
        {
            Administrator.AdminPanels.UpdateKeyReestr("OldPasswABD25", OldPasswABD25.Text);
        }
        private void OldPasswABD25_LostFocus(object sender, RoutedEventArgs e)
        {
  
        }

        // Процедура ввода нового пароля
        private void OldPasBD25_KeyUp(object sender, KeyEventArgs e)
        {
            Administrator.AdminPanels.UpdateKeyReestr("CarentPasswABD25Txt", CarentPasswABD25Txt.Text);
            TextCarentPasswABD25Txt = CarentPasswABD25Txt.Text;
            ProcesEnd = 0;
        }
        private void OldPasBD25_LostFocus(object sender, RoutedEventArgs e)
        {
            if (CarentPasswABD25Txt.Text.Length != 0)ProcesEnd = 0;
        }


        // Процедура вывода текущего действующего пароля в БД
        private void EyeOldPassw25_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            
            CarentPasswABD25Txt.Text = CurrentPasswFb25;
            if (CarentPasswABD25Txt.Visibility == Visibility.Visible)
            {
                CarentPasswABD25Txt.Visibility = Visibility.Collapsed;
                
            }
            else
            {
                CarentPasswABD25Txt.Visibility = Visibility.Visible;

            }

            
        }

        // Процедура выполнения смены пароля администратора БД
        private void PasswABD25Run_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (OnChangePas25.IsEnabled == true)
            {
                SetChangePassw.Foreground = Brushes.White;
                OldPasswABD25.Visibility = Visibility.Collapsed;
                LabelPas.Visibility = Visibility.Collapsed;
                OnChangePas25.Visibility = Visibility.Collapsed;
                ButonChangePas25.Visibility = Visibility.Collapsed;
                OnChangePas25.IsEnabled = false;
                CarentPasswABD25Txt.Visibility = Visibility.Collapsed;
                EyeOldPassw25.Visibility = Visibility.Collapsed;
                
            }
            else
            {
                SetChangePassw.Foreground = Brushes.Indigo;
                OldPasswABD25.Visibility = Visibility.Visible;
                LabelPas.Visibility = Visibility.Visible;
                OnChangePas25.Visibility = Visibility.Visible;
                ButonChangePas25.Visibility = Visibility.Visible;
                OnChangePas25.IsEnabled = true;
                EyeOldPassw25.Visibility = Visibility.Visible;
            }
            CarentPasswABD25Txt.Text = "";

        }

  

        private void OnChangePas25_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

            if (OldPasswABD25.Text.Length == 0)
            {
                string TextWin = "Не введён новый пароль. " + Environment.NewLine + "Выполнение процедуры остановлено. ";
                int AutoCl = 1;
                int MesaggeTop = -200;
                int MessageLeft = 600;
                InstallB52.MessageEnd(TextWin, AutoCl, MesaggeTop, MessageLeft);
                OldPasswABD25.Visibility = Visibility.Collapsed;
                OnChangePas25.Visibility = Visibility.Collapsed;
                ButonChangePas25.Visibility = Visibility.Collapsed;
                CarentPasswABD25Txt.Visibility = Visibility.Collapsed;
                EyeOldPassw25.Visibility = Visibility.Collapsed;
                LabelPas.Visibility = Visibility.Collapsed;
                SetChangePassw.Foreground = Brushes.White;
                return;
            }


            hubdate = "25";
            AppStart.RenderInfo Arguments01 = new AppStart.RenderInfo() { };
            Arguments01.argument1 = "2";
            Arguments01.argument2 = "";
            Thread thStartTimer01 = new Thread(PasswABDRunTH);
            thStartTimer01.SetApartmentState(ApartmentState.STA);
            thStartTimer01.IsBackground = true; // Фоновый поток
            thStartTimer01.Start(Arguments01);
            InstallB52.IntThreadStart++;
            IsEnaledServer25();
        }


        // Процедура выполнения смены пароля администратора БД
        private void PasswABD30Run_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (OnChangePas30.IsEnabled == true)
            {
                SetChangePassw30.Foreground = Brushes.White;
                OldPasswABD30.Visibility = Visibility.Collapsed;
                LabelPas30.Visibility = Visibility.Collapsed;
                OnChangePas30.Visibility = Visibility.Collapsed;
                ButonChangePas30.Visibility = Visibility.Collapsed;
                OnChangePas30.IsEnabled = false;
                CarentPasswABD30Txt.Visibility = Visibility.Collapsed;
                EyeOldPassw30.Visibility = Visibility.Collapsed;
            }
            else
            {
                SetChangePassw30.Foreground = Brushes.Indigo;
                OldPasswABD30.Visibility = Visibility.Visible;
                LabelPas30.Visibility = Visibility.Visible;
                OnChangePas30.Visibility = Visibility.Visible;
                ButonChangePas30.Visibility = Visibility.Visible;
                OnChangePas30.IsEnabled = true;
                EyeOldPassw30.Visibility = Visibility.Visible;
            }


        }

        // Процедура вывода текущего действующего пароля в БД
        private void EyeOldPassw30_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (CarentPasswABD30Txt.Text == "")
            {
                CarentPasswABD30Txt.Visibility = Visibility.Visible;
                CarentPasswABD30Txt.Text = CurrentPasswFb30;
            }
            else
            {
                CarentPasswABD30Txt.Visibility = Visibility.Collapsed;
                CarentPasswABD30Txt.Text = "";
            }


        }

        // Процедура ввода старого пароля
        private void OldPasswABD30_KeyUp(object sender, KeyEventArgs e)
        {
            Administrator.AdminPanels.UpdateKeyReestr("OldPasswABD30", OldPasswABD30.Text);

            ProcesEnd = 0;
        }
        private void OldPasswABD30_LostFocus(object sender, RoutedEventArgs e)
        {
            if (OldPasswABD30.Text.Length != 0)
            {

                ProcesEnd = 0;
            }
        }

        private void OnChangePas30_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (OldPasswABD30.Text.Length == 0)
            {
                string TextWin = "Не введён новый пароль. " + Environment.NewLine + "Выполнение процедуры остановлено. ";
                int AutoCl = 1;
                int MesaggeTop = -200;
                int MessageLeft = 600;
                InstallB52.MessageEnd(TextWin, AutoCl, MesaggeTop, MessageLeft);
                OldPasswABD30.Visibility = Visibility.Collapsed;
                OnChangePas30.Visibility = Visibility.Collapsed;
                ButonChangePas30.Visibility = Visibility.Collapsed;
                CarentPasswABD30Txt.Visibility = Visibility.Collapsed;
                EyeOldPassw30.Visibility = Visibility.Collapsed;
                LabelPas30.Visibility = Visibility.Collapsed;
                SetChangePassw30.Foreground = Brushes.White;
                return;
            }

            hubdate = "30";
            AppStart.RenderInfo Arguments01 = new AppStart.RenderInfo() { };
            Arguments01.argument1 = "2";
            Arguments01.argument2 = "";
            Thread thStartTimer01 = new Thread(PasswABDRunTH);
            thStartTimer01.SetApartmentState(ApartmentState.STA);
            thStartTimer01.IsBackground = true; // Фоновый поток
            thStartTimer01.Start(Arguments01);
            InstallB52.IntThreadStart++;
        }


     

            // Процедура смены пароля текущей БД  
            public  void PasswABDRunTH(object ThreadObj)
        {
            string CurrentFB = Administrator.AdminPanels.hubdate == "25" ? CurrentPasswFb25 : CurrentPasswFb30;
            string SecurityPuch = Administrator.AdminPanels.hubdate == "25" ? Administrator.AdminPanels.SetPuth25 + "security2.fdb" : Administrator.AdminPanels.SetPuth30 + "security3.fdb";
            string SetPort = Administrator.AdminPanels.hubdate == "25" ? SetPort25 : SetPort30;
            string TCPIPBack = AppStart.TableReestr["SetTextIp4BackOf"] + "/" + SetPort + ":" + SecurityPuch;
 
            string NewPassw = Administrator.AdminPanels.hubdate == "25" ? "OldPasswABD25" : "OldPasswABD30";
            string CmdGsec = " -user SYSDBA -password " + CurrentFB + " -database " + '"' + TCPIPBack + '"' + " -modify sysdba -pw " + AppStart.TableReestr[NewPassw];
            string CmdFb2530 = Administrator.AdminPanels.hubdate == "25" ? Administrator.AdminPanels.SetPuth25 + @"\bin\gsec.exe" : Administrator.AdminPanels.SetPuth30 + "gsec.exe";
            Process mv_prcInstaller = new Process();
            mv_prcInstaller.StartInfo.FileName = CmdFb2530;
            mv_prcInstaller.StartInfo.Arguments = CmdGsec;
            mv_prcInstaller.StartInfo.UseShellExecute = false;
            mv_prcInstaller.StartInfo.CreateNoWindow = true;
            mv_prcInstaller.StartInfo.RedirectStandardOutput = true;
            mv_prcInstaller.Start();
            mv_prcInstaller.WaitForExit();
            mv_prcInstaller.Close();


            System.Windows.Application.Current.Dispatcher.Invoke(new Action(delegate ()
            {
                ConectoWorkSpace.Administrator.AdminPanels ConectoWorkSpace_On = AppStart.LinkMainWindow("WAdminPanels");


                string CryptoNewPass = ToAes256(AppStart.TableReestr[NewPassw], KeySecret);
                if (Administrator.AdminPanels.hubdate == "25")
                {

                    string StrCreate = "UPDATE SERVERACTIVFB25 SET CURRENTPASSW = '"+ AppStart.TableReestr[NewPassw] + "' WHERE SERVERACTIVFB25.PUTH = " + "'" + SetPuth25 + "'";
                    ModifyTable(StrCreate);
                    SetServerGrid("SELECT * from SERVERACTIVFB25");
                    CurrentPasswFb25 = AppStart.TableReestr[NewPassw];
                    ConectoWorkSpace_On.OldPasswABD25.Text = "";
                    ConectoWorkSpace_On.OldPasswABD25.Visibility = Visibility.Collapsed;
                    ConectoWorkSpace_On.OnChangePas25.Visibility = Visibility.Collapsed;
                    ConectoWorkSpace_On.ButonChangePas25.Visibility = Visibility.Collapsed;
                    ConectoWorkSpace_On.CarentPasswABD25Txt.Visibility = Visibility.Collapsed;
                    ConectoWorkSpace_On.EyeOldPassw25.Visibility = Visibility.Collapsed;
                    ConectoWorkSpace_On.LabelPas.Visibility = Visibility.Collapsed;
                    ConectoWorkSpace_On.SetChangePassw.Foreground = Brushes.White;

                }
                if (Administrator.AdminPanels.hubdate == "30")
                {

                    string StrCreate = "UPDATE SERVERACTIVFB30 SET CURRENTPASSW = '" + AppStart.TableReestr[NewPassw] + "' WHERE SERVERACTIVFB30.PUTH = " + "'" + SetPuth30 + "'";
                    ModifyTable(StrCreate);
                    SetServerGrid("SELECT * from SERVERACTIVFB30");
                    CurrentPasswFb30 = AppStart.TableReestr[NewPassw];
                    ConectoWorkSpace_On.OldPasswABD30.Text = "";
                    ConectoWorkSpace_On.OldPasswABD30.Visibility = Visibility.Collapsed;
                    ConectoWorkSpace_On.OnChangePas30.Visibility = Visibility.Collapsed;
                    ConectoWorkSpace_On.ButonChangePas30.Visibility = Visibility.Collapsed;
                    ConectoWorkSpace_On.CarentPasswABD30Txt.Visibility = Visibility.Collapsed;
                    ConectoWorkSpace_On.EyeOldPassw30.Visibility = Visibility.Collapsed;
                    ConectoWorkSpace_On.LabelPas30.Visibility = Visibility.Collapsed;
                    ConectoWorkSpace_On.SetChangePassw30.Foreground = Brushes.White;
                }
            }));

            var TextWindows = "Смена пароля" + Environment.NewLine + " успешно выполнена";
            int AutoClose = 1;
            int MesaggeTop = -170;
            int MessageLeft = 800;
            InstallB52.MessageEnd(TextWindows, AutoClose, MesaggeTop, MessageLeft);
        }

        #region Блок процедур создания архива, восстановления и расписания архивирования  БД PostGreSQL
        // ----------------------------------------------------------------------------------------
        // Блок процедур создания архива, восстановления и расписания архивирования  БД PostGreSQL

        private void GridAliasBDPostGreSQL_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void GridAliasPostGreSQL_MouseUp(object sender, MouseButtonEventArgs e)
        {

        }

        private void CreateListArhivPostGreSQL_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

        }

        private void CreateBackUpPostGreSQL_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

        }

        private void RestoryBdPostGreSQL_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

        }

        private void LocDisk_PostGreSQL_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {

        }

        private void FtpDisk_PostGreSQL_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {

        }


        private void BackUpLocBD_PostGreSQL_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {

        }

        private void GoogleDisk_PostGreSQL_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {

        }

        private void DirPath_PostGreSQL_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

        }

        private void TablDateArhivBd_PostGreSQL_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void TablDateArhivBd_PostGreSQL_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {

        }

        private void Delete_ZapArhiv_PostGreSQL_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {

        }

        private void Change_ListArhiv_PostGreSQL_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {

        }
        // Конец блока процедур создания архива, восстановления и расписания архивирования  БД PostGreSQL
        #endregion

        // Процедуры ввода в текстовое поле  пути локального (на текущем компьютере) размещения архива БД для всех серверов.


        // 1.4. MsSQL
        private void PutchBackUpLocMsSQL_KeyUp(object sender, KeyEventArgs e)
        {
            Administrator.AdminPanels.UpdateKeyReestr("PutchBackUpLocMsSQL", PutchBackUpLocMsSQL.Text);
 
        }

        // Процедуры ввода в текстовое поле  пути размещения архива БД на ФТП сервере.
   

        private void CursorHand_MouseMove(object sender, MouseEventArgs e)
        {
            //this.Cursor = Cursors.Hand;
        }

 
 
        // 2.4. MsSQL
        private void PutchBackUpFTP_MsSQL_KeyUp(object sender, KeyEventArgs e)
        {
            Administrator.AdminPanels.UpdateKeyReestr("PutchBackUpFTP_MsSQL", PutchBackUpFTP_MsSQL.Text);
 
        }

        // Процедуры ввода в текстовое поле  пути размещения архива БД на GoogleDrive сервере.
 
  
 
        // 3.4. MsSQL
        private void PutchBackUpGoogleDrive_MsSQL_KeyUp(object sender, KeyEventArgs e)
        {
            Administrator.AdminPanels.UpdateKeyReestr("PutchBackUpGoogleDrive_MsSQL", PutchBackUpGoogleDrive_MsSQL.Text);
 
        }


        // Процедуры выбоа директории  куда необходимо записать архив  на текущем компьютере (локально) для всех серверов
  
  
 
        // 1.4. MsSQL
        private void DirPathBuckUpMsSQL_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DirPathBuckUp_Click("PutchBackUpLocMsSQL", PutchBackUpLocMsSQL);
        }

 
        // Процедуры выбоа директории  куда необходимо записать архив  на ФТП сервере
 
 
    
        // 2.4. MsSQL
        private void DirPathBuckUpFTP_MsSQL_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DirPathBuckUp_Click("PutchBackUpFTP_MsSQL", PutchBackUpFTP_MsSQL);
        }

 

        // Процедуры выбоа директории  куда необходимо записать архив  на GoogleDrive сервере
   
  
   

        // 3.4. MsSQL
        private void DirPathBuckUpGoogleDrive_MsSQL_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DirPathBuckUp_Click("PutchBackUpGoogleDrive_MsSQL", PutchBackUpGoogleDrive_MsSQL);
        }

   



        // Процедуры ввода логина  и пароля для соединения с ФТП в случае размещения BackUp БД на ФТП
  

          // 1.4.1  Логин MsSQL
        private void LoginBackUpFTP_MsSQL_KeyUp(object sender, KeyEventArgs e)
        {
            Administrator.AdminPanels.UpdateKeyReestr("LoginBackUpFTP_MsSQL", "");
  
        }


        // 1.4.2  Пароль MsSQL
        private void PaswordBackUpFTP_MsSQL_KeyUp(object sender, KeyEventArgs e)
        {
            Administrator.AdminPanels.UpdateKeyReestr("PaswordBackUpFTP_MsSQL", "");
  
        }

  
        // 2.4.1 Логин MsSQL
        private void LoginBackUpGoogleDrive_MsSQL_KeyUp(object sender, KeyEventArgs e)
        {
            Administrator.AdminPanels.UpdateKeyReestr("LoginBackUpGoogleDrive_MsSQL", "");
   
        }
        // 2.4.2  Пароль MsSQL
        private void PaswordBackUpGoogleDrive_MsSQL_KeyUp(object sender, KeyEventArgs e)
        {
            Administrator.AdminPanels.UpdateKeyReestr("PaswordBackUpGoogleDrive_MsSQL", "");
    
        }

        // Процедура создания архива текущей БД  MsSQL
        private void BackUpMsSQLRun_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

        }


        #endregion Закладка "Службы БД"

        #region Закладка "Шлюзы"
        // Закладка "Шлюзы"

        // Процедура подготовки массива переключателей панели шлюзы и
        // отображения текущего состояния этих переключателей на панели. 
        private void InitPanel_Shluz(object sender, MouseButtonEventArgs e)
        {
            InitKeyOnOffShluz();
            InitPanel_();
            InitTextShluz();

            Interface.InitTextBox("ShluzAdrPortServer", ShluzAdrPortServer);

 
 
            Ip4 = AppStart.TableReestr["ShluzAdrServerIp4"];
            if (Ip4.Length == 0)
            {
                Ip4 = "127.0.0.1";
                UpdateKeyReestr("ShluzAdrServerIp4", Ip4);
 
            }
            UpdateKeyReestr("ShluzAdrServerDate_IP4", "2");
            for (int indPoint = 1; indPoint <= 3; indPoint = indPoint + 1)
            {
                int position = Ip4.IndexOf(".");
                if (position <= 0) { break; }
                switch (indPoint)
                {
                    case 1:
                        ShluzAdrServerIp4Text1.Text = Ip4.Substring(0, position);
                        break;
                    case 2:
                        ShluzAdrServerIp4Text2.Text = Ip4.Substring(0, position);
                        break;
                    case 3:
                        ShluzAdrServerIp4Text3.Text = Ip4.Substring(0, position);
                        break;
                }
                Ip4 = Ip4.Substring(position + 1);
            }
            ShluzAdrServerIp4Text4.Text = Ip4.Substring(0);
            if (Convert.ToInt32(AppStart.TableReestr["ShluzAdrServerDate_IP4"].ToString()) == 0 || Convert.ToInt32(AppStart.TableReestr["ShluzAdrServerDate_IP4"].ToString()) == 3)

            {
                ShluzAdrServerIp4Text1.IsEnabled = false;
                ShluzAdrServerIp4Text2.IsEnabled = false;
                ShluzAdrServerIp4Text3.IsEnabled = false;
                ShluzAdrServerIp4Text4.IsEnabled = false;

            }
            else
            {
                ShluzAdrServerIp6Text1.IsEnabled = false;
                ShluzAdrServerIp6Text2.IsEnabled = false;
                ShluzAdrServerIp6Text3.IsEnabled = false;
                ShluzAdrServerIp6Text4.IsEnabled = false;
                ShluzAdrServerIp6Text5.IsEnabled = false;
                ShluzAdrServerIp6Text6.IsEnabled = false;
            }
            if (Convert.ToInt32(AppStart.TableReestr["ShluzAdrServerDate_IP6"].ToString()) == 0 || Convert.ToInt32(AppStart.TableReestr["ShluzAdrServerDate_IP6"].ToString()) == 3)
            {
                ShluzAdrServerIp6Text1.IsEnabled = false;
                ShluzAdrServerIp6Text2.IsEnabled = false;
                ShluzAdrServerIp6Text3.IsEnabled = false;
                ShluzAdrServerIp6Text4.IsEnabled = false;
                ShluzAdrServerIp6Text5.IsEnabled = false;
                ShluzAdrServerIp6Text6.IsEnabled = false;
            }
            else
            {
                ShluzAdrServerIp4Text1.IsEnabled = false;
                ShluzAdrServerIp4Text2.IsEnabled = false;
                ShluzAdrServerIp4Text3.IsEnabled = false;
                ShluzAdrServerIp4Text4.IsEnabled = false;
            }
            Ip6 = AppStart.TableReestr["ShluzAdrServerIp6"]; 
            for (int indPoint = 1; indPoint <= 5; indPoint = indPoint + 1)
            {
                int position = Ip6.IndexOf(".");
                if (position <= 0) { break; }
                switch (indPoint)
                {
                    case 1:
                        ShluzAdrServerIp6Text1.Text = Ip6.Substring(0, position);
                        break;
                    case 2:
                        ShluzAdrServerIp6Text2.Text = Ip6.Substring(0, position);
                        break;
                    case 3:
                        ShluzAdrServerIp6Text3.Text = Ip6.Substring(0, position);
                        break;
                    case 4:
                        ShluzAdrServerIp6Text4.Text = Ip6.Substring(0, position);
                        break;
                    case 5:
                        ShluzAdrServerIp6Text5.Text = Ip6.Substring(0, position);
                        break;
                }
                Ip6 = Ip6.Substring(position + 1);
            }
            ShluzAdrServerIp6Text6.Text = Ip6.Substring(0);
            PsdoData.Visibility = Visibility.Collapsed;
            ShluzNamePsevdoData.Visibility = Visibility.Collapsed;
 

            if (Convert.ToInt32(AppStart.TableReestr["CreateShluzOn"].ToString()) == 1) 
            {
                NameShluz.Visibility = Visibility.Visible;
                TypeShluz.Visibility = Visibility.Visible;
                BorderOkShluz.Visibility = Visibility.Visible;
                BorderNoShluz.Visibility = Visibility.Visible;
                NoShluz.Visibility = Visibility.Visible;
                OkShluz.Visibility = Visibility.Visible;
                ShluzNameNew.Visibility = Visibility.Visible;
                ShluzTypeNew.Visibility = Visibility.Visible;
            }
            else
            {
                NameShluz.Visibility = Visibility.Collapsed;
                TypeShluz.Visibility = Visibility.Collapsed;
                BorderOkShluz.Visibility = Visibility.Collapsed;
                BorderNoShluz.Visibility = Visibility.Collapsed;
                NoShluz.Visibility = Visibility.Collapsed;
                OkShluz.Visibility = Visibility.Collapsed;
                ShluzNameNew.Visibility = Visibility.Collapsed;
                ShluzTypeNew.Visibility = Visibility.Collapsed;
            }
            StartShluz = 0;
        }

        public static void InitKeyOnOffShluz()
        {
            AdminPanels.ButtonPanel = new string[9];
            AdminPanels.ButtonPanel[0] = "Shluz1COnOff";
            AdminPanels.ButtonPanel[1] = "ShluzBankOnOff";
            AdminPanels.ButtonPanel[2] = "ShluzRepB52OnOff";
            AdminPanels.ButtonPanel[3] = "ShluzConectOnOff";
            AdminPanels.ButtonPanel[4] = "ShluzCashCOnOff";
            AdminPanels.ButtonPanel[5] = "ShluzRdpOnOff";
            AdminPanels.ButtonPanel[6] = "ShluzAvtoServOnOff";
            AdminPanels.ButtonPanel[7] = "ShluzAdrServerDate_IP4";
            AdminPanels.ButtonPanel[8] = "ShluzAdrServerDate_IP6";

            if (LoadTableRestr == 0)InitKeySystemFB("InitKey");
            InitKeyOnOff();

        }

        public static void InitTextShluz()
        {
            AdminPanels.ButtonPanel = new string[4];
            AdminPanels.ButtonPanel[0] = "ShluzNamePsevdoData";
            AdminPanels.ButtonPanel[1] = "ShluzAdrServerIp4";
            AdminPanels.ButtonPanel[2] = "ShluzAdrServerIp6";
            AdminPanels.ButtonPanel[3] = "ShluzAdrPortServer";

            if (LoadTableRestr == 0)InitKeySystemFB("InitText");
            InitTextOnOff();

        }
        // Установить шлюз 1с
        private void Shluz1COnOff_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            Interface.ObjektOnOff("Shluz1COnOff", ref Shluz1COnOff, new Install.Metods { NameClass = "None", Install = "None", UnInstal = "None" });
        }
        // Установить шлюз Банк
        private void ShluzBankOnOff_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            Interface.ObjektOnOff("ShluzBankOnOff", ref ShluzBankOnOff, new Install.Metods { NameClass = "None", Install = "None", UnInstal = "None" });
        }
        // Установить шлюз репликации Б52
        private void ShluzRepB52OnOff_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            Interface.ObjektOnOff("ShluzRepB52OnOff", ref ShluzRepB52OnOff, new Install.Metods { NameClass = "None", Install = "None", UnInstal = "None" });
        }
        // Установить шлюз репликации Conecto
        private void ShluzConectOnOff_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            Interface.ObjektOnOff("ShluzConectOnOff", ref ShluzConectOnOff, new Install.Metods { NameClass = "None", Install = "None", UnInstal = "None" });
        }
        // Установить шлюз выгрузки отчетов  из БД обработки кассовых операций (BUS)
        private void ShluzCashCOnOff_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            Interface.ObjektOnOff("ShluzCashCOnOff", ref ShluzCashCOnOff, new Install.Metods { NameClass = "None", Install = "None", UnInstal = "None" });
        }
        // Выполнить автоопределение сервера хранения данных
        private void ShluzAvtoServOnOff_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            Interface.ObjektOnOff("ShluzAvtoServOnOff", ref ShluzAvtoServOnOff, new Install.Metods { NameClass = "None", Install = "None", UnInstal = "None" });
        }
        // IP4 адрес порта серевера данных 
        private void ShluzAdrServerDate_IP4_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            Interface.MouseLeftButtonUp("ShluzAdrServerDate_IP4", "ShluzAdrServerDate_IP6", ref ShluzAdrServerDate_IP4, ref ShluzAdrServerDate_IP6);
            Interface.TextBoxTrueFalse("ShluzAdrServerDate_IP4", "WAdminPanels", ShluzAdrServerIp6Text1, 6, ShluzAdrServerIp4Text1);

        }
        // IP6 адрес порта серевера данных 
        private void ShluzAdrServerDate_IP6_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            Interface.MouseLeftButtonUp("ShluzAdrServerDate_IP6", "ShluzAdrServerDate_IP4", ref ShluzAdrServerDate_IP6, ref ShluzAdrServerDate_IP4);
            Interface.TextBoxTrueFalse("ShluzAdrServerDate_IP6", "WAdminPanels", ShluzAdrServerIp4Text1, 6, ShluzAdrServerIp6Text1);

        }
        // Адрес порта обработки запросов к БД
        private void ShluzAdrPortServer_KeyUp(object sender, KeyEventArgs e)
        {
            Interface.SaveTextBox("ShluzAdrPortServer", ShluzAdrPortServer, 1);
        }
        // Псевдоним пользователя
        private void ShluzNamePsevdoData_KeyUp(object sender, KeyEventArgs e)
        {
            Interface.SaveTextBox("ShluzNamePsevdoData", ShluzNamePsevdoData, 1);
        }
        // Включить или отключить режим удаленного терминала
        private void ShluzRdpOnOff_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            Interface.ObjektOnOff("ShluzRdpOnOff", ref ShluzRdpOnOff, new Install.Metods { NameClass = "None", Install = "None", UnInstal = "None" });
        }

        private void ShluzAdrServerIp4Text1_KeyUp(object sender, KeyEventArgs e)
        {
            Interface.SaveTextBox("ShluzAdrServerIp4Text1", ShluzAdrServerIp4Text1, 0);
        }
        private void ShluzAdrServerIp4Text2_KeyUp(object sender, KeyEventArgs e)
        {
            Interface.SaveTextBox("ShluzAdrServerIp4Text2", ShluzAdrServerIp4Text2, 0);
        }
        private void ShluzAdrServerIp4Text3_KeyUp(object sender, KeyEventArgs e)
        {
            Interface.SaveTextBox("ShluzAdrServerIp4Text3", ShluzAdrServerIp4Text3, 0);
        }
        private void ShluzAdrServerIp4Text4_KeyUp(object sender, KeyEventArgs e)
        {
            Interface.SaveTextBox("ShluzAdrServerIp4Text4", ShluzAdrServerIp4Text4, 0);
        }

        private void ShluzAdrServerIp4Text1_LostFocus(object sender, RoutedEventArgs e)
        {
            AdrIp4[2] = ShluzAdrServerIp4Text1.Text + "." + ShluzAdrServerIp4Text2.Text + "." + ShluzAdrServerIp4Text3.Text + "." + ShluzAdrServerIp4Text4.Text;
            ConectoWorkSpace.Administrator.AdminPanels.UpdateKeyReestr("ShluzAdrServerIp4", AdrIp4[2]);
            //AppStart.rkAppSetingAllUser = Registry.CurrentUser.OpenSubKey(@"System\Alt-Tab\App\" + AppDomain.CurrentDomain.BaseDirectory , true);SystemConecto.PutchApp
            //AppStart.rkAppSetingAllUser.SetValue("ShluzAdrServerIp4", AdrIp4[2], RegistryValueKind.String);
            //AppStart.rkAppSetingAllUser.Flush();
        }
        private void ShluzAdrServerIp6Text1_KeyUp(object sender, KeyEventArgs e)
        {
            Interface.SaveTextBox("ShluzAdrServerIp6Text1", ShluzAdrServerIp6Text1, 0);
        }
        private void ShluzAdrServerIp6Text2_KeyUp(object sender, KeyEventArgs e)
        {
            Interface.SaveTextBox("ShluzAdrServerIp6Text2", ShluzAdrServerIp6Text2, 0);
        }
        private void ShluzAdrServerIp6Text3_KeyUp(object sender, KeyEventArgs e)
        {
            Interface.SaveTextBox("ShluzAdrServerIp6Text3", ShluzAdrServerIp6Text3, 0);
        }
        private void ShluzAdrServerIp6Text4_KeyUp(object sender, KeyEventArgs e)
        {
            Interface.SaveTextBox("ShluzAdrServerIp6Text4", ShluzAdrServerIp6Text4, 0);
        }
        private void ShluzAdrServerIp6Text5_KeyUp(object sender, KeyEventArgs e)
        {
            Interface.SaveTextBox("ShluzAdrServerIp6Text5", ShluzAdrServerIp6Text5, 0);
        }
        private void ShluzAdrServerIp6Text6_KeyUp(object sender, KeyEventArgs e)
        {
            Interface.SaveTextBox("ShluzAdrServerIp6Text6", ShluzAdrServerIp6Text6, 0);
        }
        private void ShluzAdrServerIp6Text1_LostFocus(object sender, RoutedEventArgs e)
        {
            AdrIp6[2] = ShluzAdrServerIp6Text1.Text + "." + ShluzAdrServerIp6Text2.Text + "." + ShluzAdrServerIp6Text3.Text + "." + ShluzAdrServerIp6Text4.Text + "." + ShluzAdrServerIp6Text5.Text + "." + ShluzAdrServerIp6Text6.Text;
            ConectoWorkSpace.Administrator.AdminPanels.UpdateKeyReestr("ShluzAdrServerIp6", AdrIp6[2]);
            //AppStart.rkAppSetingAllUser = Registry.CurrentUser.OpenSubKey(@"System\Alt-Tab\App\" + AppDomain.CurrentDomain.BaseDirectory , true); SystemConecto.PutchApp
            //AppStart.rkAppSetingAllUser.SetValue("ShluzAdrServerIp6", AdrIp6[2], RegistryValueKind.String);
            //AppStart.rkAppSetingAllUser.Flush();
        }
        // процедура обработки подтверждения создания шлюза
        private void OkShluz_KeyUp(object sender, MouseButtonEventArgs e)
        {
            // Закрыть панель создания шлюза
            ConectoWorkSpace.Administrator.AdminPanels.UpdateKeyReestr("ShluzName", ShluzNameNew.Text);
            ConectoWorkSpace.Administrator.AdminPanels.UpdateKeyReestr("ShluzTypeNew", ShluzTypeNew.Text);
            ConectoWorkSpace.Administrator.AdminPanels.UpdateKeyReestr("CreateShluzOn", "0");
            //AppStart.rkAppSetingAllUser = Registry.CurrentUser.OpenSubKey(@"System\Alt-Tab\App\" + AppDomain.CurrentDomain.BaseDirectory, true); SystemConecto.PutchApp
            //AppStart.rkAppSetingAllUser.SetValue("ShluzName", ShluzNameNew.Text, RegistryValueKind.String);
            //AppStart.rkAppSetingAllUser.Flush();
            //AppStart.rkAppSetingAllUser.SetValue("ShluzTypeNew", ShluzTypeNew.Text, RegistryValueKind.String);
            //AppStart.rkAppSetingAllUser.Flush();
            //AppStart.rkAppSetingAllUser.SetValue("CreateShluzOn", "0", RegistryValueKind.DWord);
            //AppStart.rkAppSetingAllUser.Flush();
            NameShluz.Visibility = Visibility.Collapsed;
            TypeShluz.Visibility = Visibility.Collapsed;
            BorderOkShluz.Visibility = Visibility.Collapsed;
            BorderNoShluz.Visibility = Visibility.Collapsed;
            NoShluz.Visibility = Visibility.Collapsed;
            OkShluz.Visibility = Visibility.Collapsed;
            ShluzNameNew.Visibility = Visibility.Collapsed;
            ShluzTypeNew.Visibility = Visibility.Collapsed;


        }
        // процедура обработки отмены создания шлюза
        private void NoShluz_KeyUp(object sender, MouseButtonEventArgs e)
        {

            // Закрыть панель создания шлюза
            NameShluz.Visibility = Visibility.Collapsed;
            TypeShluz.Visibility = Visibility.Collapsed;
            BorderOkShluz.Visibility = Visibility.Collapsed;
            BorderNoShluz.Visibility = Visibility.Collapsed;
            NoShluz.Visibility = Visibility.Collapsed;
            OkShluz.Visibility = Visibility.Collapsed;
            ShluzNameNew.Visibility = Visibility.Collapsed;
            ShluzTypeNew.Visibility = Visibility.Collapsed;
            ConectoWorkSpace.Administrator.AdminPanels.UpdateKeyReestr("CreateShluzOn", "0");
            //AppStart.rkAppSetingAllUser.SetValue("CreateShluzOn", "0", RegistryValueKind.DWord);
            //AppStart.rkAppSetingAllUser.Flush();
        }




        // Создать новый шлюз
        private void Create_Shluz_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            // MessageBox.Show("Туки");
            // Открыть панель создания шлюза
            NameShluz.Visibility = Visibility.Visible;
            TypeShluz.Visibility = Visibility.Visible;
            BorderOkShluz.Visibility = Visibility.Visible;
            BorderNoShluz.Visibility = Visibility.Visible;
            NoShluz.Visibility = Visibility.Visible;
            OkShluz.Visibility = Visibility.Visible;
            ShluzNameNew.Visibility = Visibility.Visible;
            ShluzTypeNew.Visibility = Visibility.Visible;
            ConectoWorkSpace.Administrator.AdminPanels.UpdateKeyReestr("CreateShluzOn", "1");
            //AppStart.rkAppSetingAllUser.SetValue("CreateShluzOn", "1", RegistryValueKind.DWord);
            //AppStart.rkAppSetingAllUser.Flush();

            // MessageBox.Show("Туки2");
        }

        private void ShluzNameNew_TextChanged(object sender, TextChangedEventArgs e)
        {

        }


        #endregion


        #region Закладка "Системный сервис"
        // Закладка "Системный сервис"

        private void InitPanel_ConectoServis(object sender, MouseButtonEventArgs e)
        {
            InitPanelConectoServis();
        }

        private void InitPanelConectoServis()
        {
            
            if (StartConectoServis == 1)
            {
                InitKeyOnOffConectoServis();
                InitListTablLog();
                LoadListTablLog();
                StartConectoServis = 0;

            }

        }

        public static void InitKeyOnOffConectoServis()
        {
            ButtonPanel = new string[1];
            ButtonPanel[0] = "OnOffLog_Del";

            if (LoadTableRestr == 0) InitKeySystemFB("InitKey");
            InitKeyOnOff();
        }

        // Процедура вывода списка наименования таблиц БД для выбора и записи в список копирования.
        public static void LoadedTableLog()
        {
            List<NameTablLog> LoadedName = new List<NameTablLog>(1);
            for (int i = 0; i < CountNameTable; i++)
            {
                if (!CompareBD.DevelopNameTable[i].Contains("$")) LoadedName.Add(new NameTablLog(CompareBD.DevelopNameTable[i].Trim()));   
            }

            System.Windows.Application.Current.Dispatcher.Invoke(new Action(delegate ()
            {
                WinSetTableLog ConectoWorkSpace_InW = AppStart.LinkMainWindow("WinSetTableLogW");
                ConectoWorkSpace_InW.TablLog.ItemsSource = LoadedName;
            }));

        }
        // Процедура выбора таблицы из списка
        public static void GridTableLog()
        { 
            if (CountNameTable != 0)
            {
                System.Windows.Application.Current.Dispatcher.Invoke(new Action(delegate ()
                {
                    WinSetTableLog ConectoWorkSpace_InW = AppStart.LinkMainWindow("WinSetTableLogW");
                   if (ConectoWorkSpace_InW.TablLog.SelectedItem != null)
                   {
                        NameTablLog path = ConectoWorkSpace_InW.TablLog.SelectedItem as NameTablLog;
                        AddNameTablLog = path.TableName.Trim();

                        FiltrDate = AddNameTablLog.StartsWith("LOG_") ? "LOG_DATE" : "DAT";
                        string StrCreate = "INSERT INTO LISTTABLLOG VALUES('" + AddNameTablLog + "','','" + FiltrDate + "')";
                        DBConecto.DBopenFBConectionMemory(SystemBD.ConnnStringSystem().ToString(), "", "FbSystem");
                        DBConecto.UniQuery DeletQuery = new DBConecto.UniQuery(StrCreate, "FB");
                        DeletQuery.ExecuteUNIScalar();
                        DBConecto.DBcloseFBConectionMemory("FbSystem");
                        GridListTablLog("SELECT * FROM LISTTABLLOG");
                    }
                }));
            }
        }


        public static void ConectionFb(string Conecthub, string BdConectName)
        {
            FbConnectionStringBuilder connectionStringBuilder = new FbConnectionStringBuilder();
            connectionStringBuilder.UserID = "SYSDBA";
            connectionStringBuilder.Password = Conecthub == "25" ? AdminPanels.CurrentPasswFb25 : AdminPanels.CurrentPasswFb30;
            connectionStringBuilder.Charset = "NONE";
            connectionStringBuilder.Dialect = 3;
            connectionStringBuilder.ServerType = FbServerType.Default;
            connectionStringBuilder.DataSource = "TCP/IP";
            connectionStringBuilder.Database = AppStart.TableReestr["SetTextIp4BackOf"] + "/" + (Conecthub == "25" ? AdminPanels.SetPort25 : AdminPanels.SetPort30) + ":" + AdminPanels.SelectAlias;
            connectionStringBuilder.Port = (int)Convert.ToInt16(Conecthub == "25" ? AdminPanels.SetPort25 : AdminPanels.SetPort30);
            if (BdConectName == "BdB52")
                AdminPanels.BdB52 = connectionStringBuilder.ToString();
            if (!(BdConectName == "BdB52Satellite"))
                return;
            AdminPanels.BdB52Satellite = connectionStringBuilder.ToString();
        }

        public static void InitListTablLog(string InitLog = "")
        {
            string cmdText1 = "SELECT count(RDB$RELATION_NAME) FROM RDB$RELATIONS";
            string cmdText2 = "SELECT F.RDB$RELATION_NAME  FROM RDB$RELATIONS F";
            if (InitLog == "")
            {
                string str = AppStart.TableReestr["NameServer25"] == "" ? AppStart.TableReestr["NameServer30"] : AppStart.TableReestr["NameServer25"];
                AdminPanels.hubdate = AppStart.TableReestr["NameServer25"] == "" ? "30" : "25";
                AdminPanels.SetPort25 = AppStart.TableReestr["SetPortServer25"];
                AdminPanels.SetPort30 = AppStart.TableReestr["SetPortServer30"];
                AdminPanels.CurrentPasswFb25 = AppStart.TableReestr["CurrentPasswABD25"];
                AdminPanels.CurrentPasswFb30 = AppStart.TableReestr["CurrentPasswABD30"];
                string cmdText3 = "SELECT FIRST 1 * from CONNECTIONBD25 WHERE CONNECTIONBD25.NAMESERVER = '" + str + "'";
                DBConecto.DBopenFBConectionMemory(SystemBD.ConnnStringSystem("Dynamic").ToString(), "", "FbSystem");
                FbCommand fbCommand = new FbCommand(cmdText3, (FbConnection)DBConecto.bdFbSystemConect);
                fbCommand.CommandType = CommandType.Text;
                FbDataReader fbDataReader = fbCommand.ExecuteReader();
                while (fbDataReader.Read())
                {
                    AdminPanels.SelectPuth = fbDataReader[0].ToString();
                    AdminPanels.SelectAlias = fbDataReader[1].ToString();
                }
                fbDataReader.Close();
                fbCommand.Dispose();
                DBConecto.DBcloseFBConectionMemory("FbSystem");
            }
            FbConnectionStringBuilder connectionStringBuilder = new FbConnectionStringBuilder();
            connectionStringBuilder.UserID = "SYSDBA";
            connectionStringBuilder.Password = AdminPanels.hubdate == "25" ? AdminPanels.CurrentPasswFb25 : AdminPanels.CurrentPasswFb30;
            connectionStringBuilder.Charset = "NONE";
            connectionStringBuilder.Dialect = 3;
            connectionStringBuilder.ServerType = FbServerType.Default;
            connectionStringBuilder.DataSource = "TCP/IP";
            connectionStringBuilder.Database = AppStart.TableReestr["SetTextIp4BackOf"] + "/" + (AdminPanels.hubdate == "25" ? AdminPanels.SetPort25 : AdminPanels.SetPort30) + ":" + AdminPanels.SelectAlias;
            connectionStringBuilder.Port = (int)Convert.ToInt16(AdminPanels.hubdate == "25" ? AdminPanels.SetPort25 : AdminPanels.SetPort30);
            FbConnection connection = new FbConnection(connectionStringBuilder.ToString());
            connection.Open();
            FbCommand fbCommand1 = new FbCommand(cmdText1, connection);
            string str1 = fbCommand1.ExecuteScalar().ToString();
            fbCommand1.Dispose();
            CompareBD.DevelopNameTable = new string[(int)Convert.ToInt16(str1)];
            int index = 0;
            FbCommand fbCommand2 = new FbCommand(cmdText2, connection);
            FbDataReader fbDataReader1 = fbCommand2.ExecuteReader();
            while (fbDataReader1.Read())
            {
                CompareBD.DevelopNameTable[index] = fbDataReader1[0].ToString().Trim();
                ++index;
            }
            fbDataReader1.Close();
            fbCommand2.Dispose();
            connection.Dispose();
            connection.Close();
            connectionStringBuilder.Clear();
            AdminPanels.CountNameTable = index - 1;
        }

       // процедура первоначальной загрузки имен таблиц логов в список копируемых таблиц
        public static void LoadListTablLog()
        {
            DBConecto.DBopenFBConectionMemory(SystemBD.ConnnStringSystem("Dynamic").ToString(), "", "FbSystem");
            new DBConecto.UniQuery("DELETE from ListTablLog", "FB").ExecuteUNIScalar("");
            DBConecto.DBcloseFBConectionMemory("FbSystem");
            AdminPanels.InitListTablLog("LOG_");
            DBConecto.DBopenFBConectionMemory(SystemBD.ConnnStringSystem("Dynamic").ToString(), "", "FbSystem");
            for (int index = 0; index < AdminPanels.CountNameTable; ++index)
            {
                if (CompareBD.DevelopNameTable[index].ToUpper().StartsWith("LOG_") || CompareBD.DevelopNameTable[index].ToUpper().StartsWith("SP_LOG_"))
                {
                    string str1 = CompareBD.DevelopNameTable[index].StartsWith("LOG_") ? "LOG_DATE" : "DAT";
                    string str2 = CompareBD.DevelopNameTable[index].Contains("LOG_HT_KEY_REG") || CompareBD.DevelopNameTable[index].Contains("LOG_CARD_TRANSACTIONS") ? "DAT" : str1;
                    new DBConecto.UniQuery("INSERT INTO ListTablLog VALUES('" + CompareBD.DevelopNameTable[index] + "','','" + str2 + "')", "FB").ExecuteUNIScalar("");
                    ++AdminPanels.CountTabLog;
                }
            }
            DBConecto.DBcloseFBConectionMemory("FbSystem");
        }

        public static void InitListTrigerLog(FbConnection bdFBConect)
        {
            string cmdText1 = "SELECT count(RDB$TRIGGER_NAME) FROM RDB$TRIGGERS";
            string cmdText2 = "SELECT T.RDB$TRIGGER_NAME FROM RDB$TRIGGERS T";
            FbCommand fbCommand1 = new FbCommand(cmdText1, bdFBConect);
            string str = fbCommand1.ExecuteScalar().ToString();
            fbCommand1.Dispose();
            CompareBD.DevelopMasName = new string[(int)Convert.ToInt16(str)];
            int index = 0;
            FbCommand fbCommand2 = new FbCommand(cmdText2, bdFBConect);
            FbDataReader fbDataReader = fbCommand2.ExecuteReader();
            while (fbDataReader.Read())
            {
                if (fbDataReader[0].ToString().ToUpper().Contains("LOG_"))
                {
                    CompareBD.DevelopMasName[index] = fbDataReader[0].ToString().Trim();
                    ++index;
                }
            }
            fbDataReader.Close();
            fbCommand2.Dispose();
            AdminPanels.CountNameTriger = index - 1;
        }


        private void TablGridListLog_Loaded(object sender, RoutedEventArgs e)
        {
            GridListTablLog("SELECT * FROM LISTTABLLOG");
        }

        public static void GridListTablLog(string strselect)
        { 

            List<GridTablLog> LoadedTablLog = new List<GridTablLog>(1);
            DBConecto.DBopenFBConectionMemory(SystemBD.ConnnStringSystem().ToString(), "", "FbSystem");
            FbCommand UpdateKey = new FbCommand(strselect , DBConecto.bdFbSystemConect);
            UpdateKey.CommandType = CommandType.Text;
            FbDataReader reader = UpdateKey.ExecuteReader();
            while (reader.Read()) { LoadedTablLog.Add(new GridTablLog(reader[0].ToString(), reader[1].ToString(), reader[2].ToString())); }
            reader.Close();
            UpdateKey.Dispose();
            DBConecto.DBcloseFBConectionMemory("FbSystem");
            System.Windows.Application.Current.Dispatcher.Invoke(new Action(delegate ()
            {
                AdminPanels ConectoWorkSpace_InW = AppStart.LinkMainWindow("WAdminPanels");
                ConectoWorkSpace_InW.GridTabListlLog.ItemsSource = LoadedTablLog;
            }));
        }

        private void GridTablLog_MouseUp(object sender, MouseButtonEventArgs e)
        {
            DBConecto.DBopenFBConectionMemory(SystemBD.ConnnStringSystem().ToString(), "", "FbSystem");
            string InsertExecute = "SELECT count(*) from LISTTABLLOG";
            DBConecto.UniQuery CountQuery = new DBConecto.UniQuery(InsertExecute, "FB");
            string CountTable = CountQuery.ExecuteUNIScalar() == null ? "" : CountQuery.ExecuteUNIScalar().ToString();

            if (Convert.ToUInt32(CountTable) != 0)
            {
                if (GridTabListlLog.SelectedItem != null)
                {
                    GridTablLog path = GridTabListlLog.SelectedItem as GridTablLog;
                    SelectNameTablLog = path.NameTabl.Trim();
                    SelectDateCopy = path.DateCopy.Trim();
                    FiltrDate = path.ColumnDate.Trim();
                    DeletTablLogList.IsEnabled = true;
                }
            }
            DBConecto.DBcloseFBConectionMemory("FbSystem");
        }

        // Удаление строки из списка таблиц логов.
        private void DeletTablLogList_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            string StrCreate = "DELETE FROM LISTTABLLOG WHERE NAMETABLE = " + "'" + SelectNameTablLog.Trim() + "'";
            DBConecto.DBopenFBConectionMemory(SystemBD.ConnnStringSystem().ToString(), "", "FbSystem");
            DBConecto.UniQuery DeletQuery = new DBConecto.UniQuery(StrCreate, "FB");
            DeletQuery.ExecuteUNIScalar();
            DBConecto.DBcloseFBConectionMemory("FbSystem");
            GridListTablLog("SELECT * FROM LISTTABLLOG");
        }

        private void AddTablLogList_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            System.Windows.Application.Current.Dispatcher.Invoke(new Action(delegate ()
            {
                AdminPanels ConectoWorkSpace_InW = AppStart.LinkMainWindow("WAdminPanels");
                WinSetTableLog SetWindowLog = new WinSetTableLog();
                SetWindowLog.Owner = ConectoWorkSpace_InW;
                SetWindowLog.Top = ConectoWorkSpace_InW.Top + 220;
                SetWindowLog.Left = ConectoWorkSpace_InW.Left + 775;
                SetWindowLog.Show();

            }));
        }

        private void OnOffLog_Del_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if(Convert.ToInt16(AppStart.TableReestr["OnOffLog_Del"]) == 0) Interface.CurrentStateInst("OnOffLog_Del", "2", "on_off_2.png", OnOffLog_Del);
            else Interface.CurrentStateInst("OnOffLog_Del", "0", "on_off_1.png", OnOffLog_Del);
        }

        public void LoadGridEtalonProtokol(string CurrentSetBD)
        {

            List<EtalonDeveloper> Develop = new List<EtalonDeveloper>(1);
            List<EtalonDistrybutor> Distrybut = new List<EtalonDistrybutor>(1);
            List<ProtokolCompare> Protokol = new List<ProtokolCompare>(1);

            DBConecto.DBopenFBConectionMemory(SystemBD.ConnnStringSystem().ToString(), "", "FbSystem");
            FbCommand UpdateKey = new FbCommand(CurrentSetBD, DBConecto.bdFbSystemConect);
            UpdateKey.CommandType = CommandType.Text;
            FbDataReader reader = UpdateKey.ExecuteReader();
            while (reader.Read())
            {
                if (CurrentSetBD.Contains("DEVELOP")) Develop.Add(new EtalonDeveloper(reader[0].ToString(), reader[1].ToString()));
                if (CurrentSetBD.Contains("DISTRYBUT")) Distrybut.Add(new EtalonDistrybutor(reader[0].ToString(), reader[1].ToString()));
                if (CurrentSetBD.Contains("PROTOKOL")) Protokol.Add(new ProtokolCompare(reader[0].ToString(), reader[1].ToString(), reader[2].ToString(), reader[3].ToString()));
            }
            reader.Close();
            UpdateKey.Dispose();
            DBConecto.DBcloseFBConectionMemory("FbSystem");
            if (CurrentSetBD.Contains("DEVELOP")) Etalon_Developer.ItemsSource = Develop;
            if (CurrentSetBD.Contains("DISTRYBUT")) Etalon_Distributor.ItemsSource = Distrybut;
            if (CurrentSetBD.Contains("PROTOKOL")) ProtokolCompare.ItemsSource = Protokol;


        }

        public  void GridMouseUpEtalonProtokol(string InsertExecute, int NumberKeyGrid)
        {
            DBConecto.DBopenFBConectionMemory(SystemBD.ConnnStringSystem().ToString(), "", "FbSystem");
            DBConecto.UniQuery CountQuery = new DBConecto.UniQuery(InsertExecute, "FB");
            string CountTable = CountQuery.ExecuteUNIScalar() == null ? "" : CountQuery.ExecuteUNIScalar().ToString();

            if (Convert.ToUInt32(CountTable) != 0)
            {
                switch (NumberKeyGrid)
                {
                    case 1:
                        if (Etalon_Developer.SelectedItem != null)
                        {
                            EtalonDeveloper path = Etalon_Developer.SelectedItem as EtalonDeveloper;
                            EtalonDeveloperPuth = path.Puth;
                            EtalonDeveloperDateCreate = path.DateCreate;
                            Administrator.AdminPanels.SetWinSetHub = "DEVELOP";
                            DelEtalonDevelop.IsEnabled = true;
                            Putch_Developer.Text = path.Puth;
                            if (Putch_Developer.Text.Length != 0 && Putch_Distributor.Text.Length != 0)
                            {
                                RunCompareCreateBD.IsEnabled = true;
                                RunCompare.IsEnabled = true;
                            } 
                        }
                        break;
                    case 2:
                        if (Etalon_Distributor.SelectedItem != null)
                        {
                            EtalonDistrybutor path = Etalon_Distributor.SelectedItem as EtalonDistrybutor;
                            EtalonDistributorPuth = path.Puth;
                            EtalonDistributorDateCreate = path.DateCreate;
                            Administrator.AdminPanels.SetWinSetHub = "DISTRYBUT";
                            DelEtalonDistributor.IsEnabled = true;
                            Putch_Distributor.Text = path.Puth;
                            if (Putch_Developer.Text.Length != 0 && Putch_Distributor.Text.Length != 0)
                            {
                                RunCompareCreateBD.IsEnabled = true;
                                RunCompare.IsEnabled = true;
                            }
                        }
                        break;
                    case 3:
                        if (ProtokolCompare.SelectedItem != null)
                        {
                            ProtokolCompare path = ProtokolCompare.SelectedItem as ProtokolCompare;
                            ProtokolComparePuth = path.NubProtokol;
                            ProtokolCompareDateCreate = path.DateCreate; 
                            Administrator.AdminPanels.SetWinSetHub = "PROTOKOL";
                            DelProtokol.IsEnabled = true;
                        }
                        break;
                }
  
            }
            DBConecto.DBcloseFBConectionMemory("FbSystem");

        }

        public void AddEtalon(string DevelopDistryb, Label NameLabel)
        {

            NameLabel.Background = Brushes.Indigo;
            NameLabel.Foreground = Brushes.White;
            PathFileBD_Click();
            if ((!PathFileBDText.ToUpper().Contains(".FDB") && !PathFileBDText.ToUpper().Contains(".GDB")) || PathFileBDText.Length == 0)
            {
   
                    var TextWindows = "Выбран файл не структуры БД." + Environment.NewLine + "Процедура прекращена ";
                    int AutoClose = 1;
                    int MesaggeTop = -600;
                    int MessageLeft = 0;
                    InstallB52.MessageEnd(TextWindows, AutoClose, MesaggeTop, MessageLeft);
                    NameLabel.Background = Brushes.SkyBlue;
                    NameLabel.Foreground = Brushes.Black;
                return;

            }
            string DatTimeCreate = DateTime.Now.ToString("yyyyMMddHHmmss");
            DBConecto.DBopenFBConectionMemory(SystemBD.ConnnStringSystem().ToString(), "", "FbSystem");
            int Idcount = 0;
            string StrCreate = "select * from "+ DevelopDistryb+" where "+ DevelopDistryb+".PUTH = " + "'" + PathFileBDText + "'";
            FbCommand SelectTable = new FbCommand(StrCreate, DBConecto.bdFbSystemConect);
            FbDataReader ReadOutTable = SelectTable.ExecuteReader();
            while (ReadOutTable.Read()) { Idcount ++; }
            ReadOutTable.Close();
            if (Idcount == 0)
            {
                ProcesEnd = 0;
                //Administrator.AdminPanels.SelectAlias = (PathFileBDText.Substring(PathFileBDText.LastIndexOf(@"\")+1, PathFileBDText.LastIndexOf(@".") - PathFileBDText.LastIndexOf(@"\")-1));
                string StrInsert = "INSERT INTO "+ DevelopDistryb+" values ('" + PathFileBDText + "', '" + DatTimeCreate + "')";
                DBConecto.UniQuery CountQuery = new DBConecto.UniQuery(StrInsert, "FB");
                CountQuery.UserQuery = string.Format(StrInsert, DevelopDistryb);
                CountQuery.ExecuteUNIScalar();
            }
            SelectTable.Dispose();
            DBConecto.DBcloseFBConectionMemory("FbSystem");
            LoadGridEtalonProtokol("select * from " + DevelopDistryb);
            NameLabel.Background = Brushes.SkyBlue;
            NameLabel.Foreground = Brushes.Black;
 
        }


        public void DeleteOfTable(string DelNameTable, string NetKeyPuth)
        {

            string DelNameField = DelNameTable == "PROTOKOLCOMPARE" ? ".PROTOKOL = " : ".PUTH = ";
            string StrCreate = "DELETE from "+ DelNameTable+" where "+ DelNameTable+ DelNameField + "'" + NetKeyPuth + "'";
            DBConecto.DBopenFBConectionMemory(SystemBD.ConnnStringSystem().ToString(), "", "FbSystem");
            DBConecto.UniQuery DeletQuery = new DBConecto.UniQuery(StrCreate, "FB");
            DeletQuery.UserQuery = string.Format(StrCreate, DelNameTable);
            DeletQuery.ExecuteUNIScalar();
            LoadGridEtalonProtokol("select * from " + DelNameTable);
            DelEtalonDevelop.IsEnabled = false;
            DelProtokol.IsEnabled = false;
            DelEtalonDistributor.IsEnabled = false;
        }

        private void Etalon_Developer_Loaded(object sender, RoutedEventArgs e)
        {
            LoadGridEtalonProtokol("SELECT * FROM ETALONDEVELOP");
        }

        private void Etalon_Developer_MouseUp(object sender, MouseButtonEventArgs e)
        {
            GridMouseUpEtalonProtokol("SELECT count(*) from ETALONDEVELOP", 1);
        }

        private void Etalon_Distributor_Loaded(object sender, RoutedEventArgs e)
        {
            LoadGridEtalonProtokol(" SELECT * FROM ETALONDISTRYBUT");
        }

        private void Etalon_Distributor_MouseUp(object sender, MouseButtonEventArgs e)
        {
            GridMouseUpEtalonProtokol("SELECT count(*) from ETALONDISTRYBUT", 2);
        }

        private void Protokol_Compare_Loaded(object sender, RoutedEventArgs e)
        {
            LoadGridEtalonProtokol("SELECT * FROM PROTOKOLCOMPARE"); 
        }

        private void Protokol_Compare_MouseUp(object sender, MouseButtonEventArgs e)
        {
            GridMouseUpEtalonProtokol("SELECT count(*) from PROTOKOLCOMPARE", 3);
        }

        private void AddEtalonDevelop_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            AddEtalon("ETALONDEVELOP", AddEtalonDevelop);
        }

        private void DelEtalonDevelop_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            DeleteOfTable("ETALONDEVELOP", EtalonDeveloperPuth);
        }

        private void AddEtalonDistributor_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            AddEtalon("ETALONDISTRYBUT", AddEtalonDistributor);
        }

        private void DelEtalonDistributor_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            DeleteOfTable("ETALONDISTRYBUT", EtalonDistributorPuth);
        }

        private void DelProtokol_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            DeleteOfTable("PROTOKOLCOMPARE", ProtokolComparePuth);
        }

        private void RunCompareCreateBD_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            RunCompareCreateBD.Background = Brushes.Indigo;
            RunCompareCreateBD.Foreground = Brushes.White;
            CompareBD.RunCompareCreateBD();
            Putch_Developer.Text = "";
            Putch_Distributor.Text = "";
            RunCompareCreateBD.IsEnabled = false;
            RunCompare.IsEnabled = false;
            RunCompareCreateBD.Background = Brushes.SkyBlue;
            RunCompareCreateBD.Foreground = Brushes.Black;
        }

        private void RunCompare_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            RunCompare.Background = Brushes.Indigo;
            RunCompare.Foreground = Brushes.White;
            CompareBD.RunCompareBD();
            Putch_Developer.Text = "";
            Putch_Distributor.Text = "";
            RunCompareCreateBD.IsEnabled = false;
            RunCompare.IsEnabled = false;
            RunCompare.Background = Brushes.SkyBlue;
            RunCompare.Foreground = Brushes.Black;
        }



        #endregion


        #region Закладка Настройки conecto
        // Закладка Администрорование : Настройки conecto
        // Путь для создания архивов БД
        private void ImageDirPathBackUp_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            string TmpFolderPuth = "";
            var dlg = new System.Windows.Forms.FolderBrowserDialog();
            dlg.Description = "Путь к  архиву";
            System.Windows.Forms.DialogResult result = dlg.ShowDialog();
            if (result == System.Windows.Forms.DialogResult.OK)
            {
                TmpFolderPuth = dlg.SelectedPath + (dlg.SelectedPath.LastIndexOf(@"\") <= 2 && dlg.SelectedPath.Length <= 3 ? "" : @"\");
                ContentPuth = FolderFrontPuth.Length > 38 ? "..." + FolderFrontPuth.Substring(FolderFrontPuth.Length - 35, 35) : FolderFrontPuth;
                TextBoxDirPathBackUp.Text = dlg.SelectedPath;
                UpdateKeyReestr("PuthArhivDefault", TmpFolderPuth); 
            }
        }

        private void CheckBoxPaswDefaultFb25_Click(object sender, RoutedEventArgs e)
        {
            if (TextBoxPaswDefaultFb25.IsEnabled == true) TextBoxPaswDefaultFb25.IsEnabled = false;
            else TextBoxPaswDefaultFb25.IsEnabled = true;
        }

        private void CheckBoxPaswDefaultFb30_Click(object sender, RoutedEventArgs e)
        {
            if (TextBoxPaswDefaultFb30.IsEnabled ==true) TextBoxPaswDefaultFb30.IsEnabled = false;
            else TextBoxPaswDefaultFb30.IsEnabled = true;
        }


        #endregion

    }
}