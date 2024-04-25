
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Windows;
using System.Text;

using System.Runtime.InteropServices;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging; // BitmapImage
using Microsoft.Win32;
// --- Process
using System.IO;
using System.Diagnostics;

using System.ServiceProcess;

// WMI
using System.Management;
// Network
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Security.Cryptography;

/// Многопоточность
using System.Threading;
using System.Windows.Threading;
using ConectoWorkSpace.Systems;

using FirebirdSql.Data.FirebirdClient;  // http://www.sql.ru/forum/actualthread.aspx?tid=133383   http://www.firebirdsql.org/en/net-examples-of-use/
using FirebirdSql.Data.Isql;
using IWshRuntimeLibrary;

 // Содержит типы, независимые от провайдеров, например DataSet и DataTable.
using System.Data.SqlClient;    // Содержит типы SQL Server .NET Data Provider


namespace ConectoWorkSpace
{
    public static class InstallB52
    {

        /// <summary>
        /// Количество запушенных потоков
        /// </summary>

        public static int IntThreadStart = 0;
        public static int ThreadStart = 0;
        public static string Temphub = "";
        public static string[] MasSetBackFront = new string[13];
        public static string[] ListTableLog = new string[0];
        public static string[] ArryColumns = new string[0];
        public static string[] ArryColumnsType = new string[0];
        public static string[] ArryFiltr = new string[0];
        public static string[] ArryCopyDate = new string[0];
        public static string[] CountZapTable;

  
        public static class Pic { }
        /// <summary>
        /// Cursor промежуточная таблица (не используется на прямую)
        /// </summary>
        public static DataTable LoadTable = new DataTable();

        /// <summary>
        /// Структура данных для передачи аргументов или параметров
        /// </summary>
        public struct InfoGrdSrv
        {
            public string TCP_PORT { get; set; }
            public string argument2 { get; set; }
            public string argument3 { get; set; }
        }


   
        #region Закладка "Установка  и Деинсталяция  сервера FireBird 2.5 и БД"
        /// <summary>
        /// Установка сервера FireBird 2.5
        /// 
        /// </summary>
        public static void InstallServBD2_5()
        {
            ConectoWorkSpace.Administrator.AdminPanels ConectoWorkSpace_InW = AppStart.LinkMainWindow("WAdminPanels");
            if (ConectoWorkSpace_InW == null)return;
            else
            {
                var TextWindows = "Подождите пожалуйста." + Environment.NewLine + "Устанавливается новый сервер ";
                int AutoClose = 1;
                int MesaggeTop = -170;
                int MessageLeft = 600;
                InstallB52.MessageEnd(TextWindows, AutoClose, MesaggeTop, MessageLeft);
 
                int IndCount = -1;
                DBConecto.DBopenFBConectionMemory(SystemBD.ConnnStringSystem().ToString(), "", "FbSystem");
                string StrCreate = "SELECT * from SERVERACTIVFB25";
                FbCommand SelectTable = new FbCommand(StrCreate, DBConecto.bdFbSystemConect);
                FbDataReader ReadOutTable = SelectTable.ExecuteReader();
                while (ReadOutTable.Read())
                {
                    IndCount++;
                    Administrator.AdminPanels.IdPort[IndCount] = ReadOutTable[0].ToString();
                    Administrator.AdminPanels.NamePuth[IndCount] = ReadOutTable[1].ToString(); 
                    Administrator.AdminPanels.NameServer[IndCount] = ReadOutTable[2].ToString();
                }
                ReadOutTable.Close();
                SelectTable.Dispose();
                DBConecto.DBcloseFBConectionMemory("FbSystem");
                if (IndCount >= 6)
                {
                    string TextWind = "Количество установленных серверов превышает 5" + Environment.NewLine + "Удалите ненужные записи из таблицы и повторите установку ";
                    int AutoCls = 1;
                    int MesaggeTp = 350;
                    int MessageLf = 650;
                    InstallB52.MessageErr(TextWind, AutoCls, MesaggeTp, MessageLf);
                }
                AppStart.RenderInfo Arguments01 = new AppStart.RenderInfo() { };
                Arguments01.argument1 = "3055";
                Arguments01.argument2 = SystemConectoServers.PutchServer + @"Firebird_2_5\";
                Arguments01.argument3 = "ConectoWS";
                Arguments01.argument4 = "0";
                if (IndCount >= 0)
                {
                    for (int i = 0; i <= IndCount; i++)
                    {
                        if (Administrator.AdminPanels.IdPort[i] == Arguments01.argument1)
                        {
                            Arguments01.argument1 = Convert.ToString(3050 + Convert.ToUInt32(i));
                            i = 0;
                        }
                    }
                    for (int i = 0; i <= IndCount; i++)
                    {
                        if (Administrator.AdminPanels.NameServer[i] == Arguments01.argument3)
                        {
                            Arguments01.argument3 = "ConectoWS_2" + Convert.ToString(i);
                            i = 0;
                        }

                    }
                    Arguments01.argument2 = SystemConectoServers.PutchServer + @"Firebird_2_5" + Convert.ToString(IndCount + 1) + @"\";
                }
 
                Thread thStartTimer01 = new Thread(InstallServFB25);
                thStartTimer01.SetApartmentState(ApartmentState.STA);
                thStartTimer01.IsBackground = true; // Фоновый поток
                thStartTimer01.Start(Arguments01);
                IntThreadStart++;

            }
        }

        public static void UnInstallServBD2_5()
        {


            Administrator.AdminPanels.SetName25 = AppStart.TableReestr["NameServer25"];
            Administrator.AdminPanels.SetPuth25 = AppStart.TableReestr["ServerDefault25"];
            Administrator.AdminPanels.ImageObj = "StoptServFB25";
            AppStart.RenderInfo Arguments01 = new AppStart.RenderInfo() { };
            Arguments01.argument1 = "Delete";
            Arguments01.argument2 = "";
            Thread thStartTimer01 = new Thread(UnInstallServFB25);
            thStartTimer01.SetApartmentState(ApartmentState.STA);
            thStartTimer01.IsBackground = true; // Фоновый поток
            thStartTimer01.Start(Arguments01);
            IntThreadStart++;

 
        }

  
        public static int RemoveServerFB25(string PatchSR, string ActivServer)

        {
            int RezChek = 0;
            string PuthFileExe = PatchSR + (Administrator.AdminPanels.ImageObj.Contains("25")  ? @"bin\instsvc.exe" : @"instsvc.exe");
            if (System.IO.File.Exists(PuthFileExe))
            {

                string CurrentServer = "", PuthProcess = "";
                Administrator.AdminPanels.ScanActivFirebird();
                if (Administrator.AdminPanels.IndexActivProces >= 0)
                {
                    for (int id = 0; id <= Administrator.AdminPanels.IndexActivProces; id++)
                    {
                        CurrentServer = Administrator.AdminPanels.NameServer[id].Substring(Administrator.AdminPanels.NameServer[id].IndexOf("-s") + 2, Administrator.AdminPanels.NameServer[id].Length - (Administrator.AdminPanels.NameServer[id].IndexOf("-s") + 2)).Replace(" ", "");
                        if (CurrentServer == ActivServer) PuthProcess = Administrator.AdminPanels.NamePuth[id].ToUpper().Trim();
                    }

                }

                Process mv_prcInstaller = new Process();
                if (PuthProcess != "")
                {
                    mv_prcInstaller.StartInfo.FileName = PuthFileExe;
                    mv_prcInstaller.StartInfo.Arguments = ActivServer == "" ? " stop" : " stop -n " + ActivServer;
                    mv_prcInstaller.StartInfo.UseShellExecute = false;
                    mv_prcInstaller.StartInfo.CreateNoWindow = true;
                    mv_prcInstaller.StartInfo.RedirectStandardOutput = true;
                    mv_prcInstaller.Start();
                    mv_prcInstaller.WaitForExit();
                    mv_prcInstaller.Close();
                    PuthProcess = "";


                    for (int i = 0; i <= 200; i++)
                    {
                        Administrator.AdminPanels.ScanActivFirebird();
                        PuthProcess = "";
                        if (Administrator.AdminPanels.IndexActivProces >= 0)
                        {
                            Thread.Sleep(1000);
                            for (int id = 0; id <= Administrator.AdminPanels.IndexActivProces; id++)
                            {
                                CurrentServer = Administrator.AdminPanels.NameServer[id].Substring(Administrator.AdminPanels.NameServer[id].IndexOf("-s") + 2, Administrator.AdminPanels.NameServer[id].Length - (Administrator.AdminPanels.NameServer[id].IndexOf("-s") + 2)).Replace(" ", "");
                                if (CurrentServer == ActivServer) PuthProcess = Administrator.AdminPanels.NamePuth[id].ToUpper().Trim();
                            }
                            if (PuthProcess == "") break;
                        }

                    }

                    if (PuthProcess == "")
                    {
                        mv_prcInstaller.StartInfo.FileName = PuthFileExe;
                        mv_prcInstaller.StartInfo.Arguments = ActivServer == "" ? " remove -z" : " remove -z -n " + ActivServer;
                        mv_prcInstaller.StartInfo.UseShellExecute = false;
                        mv_prcInstaller.StartInfo.CreateNoWindow = true;
                        mv_prcInstaller.StartInfo.RedirectStandardOutput = true;
                        mv_prcInstaller.Start();
                        mv_prcInstaller.WaitForExit();
                        mv_prcInstaller.Close();
                        return RezChek = 1;
                    }

                }
                else return RezChek = 1;

            }
            string ArgumentCmd = "";
            string PuthFileExePG = PatchSR + (Administrator.AdminPanels.ImageObj.Contains("ServPG") ? @"bin\pg_ctl.exe" : "");
            if (System.IO.File.Exists(PuthFileExePG))
            {
                if (Administrator.AdminPanels.SetActivPG == "Activ") ArgumentCmd = " stop -D \"" + Administrator.AdminPanels.SelectPuthPG+ "\"";
                if (Administrator.AdminPanels.SetActivPG == "Stop") ArgumentCmd = " start -D  \"" + Administrator.AdminPanels.SelectPuthPG+ "\"";
                if (Administrator.AdminPanels.SetActivPG != "") Administrator.AdminPanels.RunProcess(PuthFileExePG, ArgumentCmd,"1");

                if (Administrator.AdminPanels.ImageObj.Contains("RestartServPG"))
                {
                    Thread.Sleep(10000);
                    ArgumentCmd = " start -D  \"" + Administrator.AdminPanels.SelectPuthPG + "\"";
                    Administrator.AdminPanels.RunProcess(PuthFileExePG, ArgumentCmd,"1");
                }
                RezChek = 1; 
            }
                return RezChek;
        }


        public static void RestartFB25(string PatchSR, string ActivServer)

        {

            if (RemoveServerFB25(PatchSR, ActivServer) > 0)
            {
                string PuthFileExe = PatchSR + (Administrator.AdminPanels.ImageObj.Contains("25") ? @"bin\instsvc.exe" : "instsvc.exe");
                Process mv_prcInstaller = new Process();
                mv_prcInstaller.StartInfo.FileName = PatchSR + (Administrator.AdminPanels.ImageObj.Contains("25") ? @"bin\instreg.exe" : "instreg.exe");
                mv_prcInstaller.StartInfo.Arguments = "install -z";
                mv_prcInstaller.StartInfo.UseShellExecute = false;
                mv_prcInstaller.StartInfo.CreateNoWindow = true;
                mv_prcInstaller.StartInfo.RedirectStandardOutput = true;
                mv_prcInstaller.Start();
                mv_prcInstaller.WaitForExit();
                mv_prcInstaller.Close();
                string CmdLine = Administrator.AdminPanels.ImageObj.Contains("30") ? " install -z " : " install -s -a -g ";
                mv_prcInstaller.StartInfo.FileName = PuthFileExe;
                mv_prcInstaller.StartInfo.Arguments = CmdLine + (ActivServer == "" ? "" : " -n " + ActivServer);
                mv_prcInstaller.StartInfo.UseShellExecute = false;
                mv_prcInstaller.StartInfo.CreateNoWindow = true;
                mv_prcInstaller.StartInfo.RedirectStandardOutput = true;
                mv_prcInstaller.Start();
                mv_prcInstaller.WaitForExit();
                mv_prcInstaller.Close();
                CmdLine = " start ";
                mv_prcInstaller.StartInfo.FileName = PuthFileExe;
                mv_prcInstaller.StartInfo.Arguments = CmdLine + (ActivServer == "" ? "" : " -n " + ActivServer);
                mv_prcInstaller.StartInfo.UseShellExecute = false;
                mv_prcInstaller.StartInfo.CreateNoWindow = true;
                mv_prcInstaller.StartInfo.RedirectStandardOutput = true;
                mv_prcInstaller.Start();
                mv_prcInstaller.WaitForExit();
            }
            else Administrator.AdminPanels.ImageObj = "No";

        }
 

        public static void MessageErr(string TextWindows, int AutoClose, int MesaggeTop, int MessageLeft)
        {
            System.Windows.Application.Current.Dispatcher.Invoke(new Action(delegate ()
            {

                ConectoWorkSpace.Administrator.AdminPanels ConectoWorkSpace_InW = AppStart.LinkMainWindow("WAdminPanels");
                Window WinOblakoVerh_Info = new WinMessage(TextWindows, AutoClose, 0); // создаем AutoClose
                WinOblakoVerh_Info.Owner = ConectoWorkSpace_InW;
                WinOblakoVerh_Info.Top = ConectoWorkSpace_InW.Top + MesaggeTop; 
                WinOblakoVerh_Info.Left = ConectoWorkSpace_InW.ScrollViewerd.Width - (WinOblakoVerh_Info.Width * 2);  //ConectoWorkSpace_InW.Left + MessageLeft;
                WinOblakoVerh_Info.ShowActivated = false;
                WinOblakoVerh_Info.Show();

            }));
        }
        public static void MessageEnd(string TextWindows, int AutoClose, int MesaggeTop, int MessageLeft)
        {
            System.Windows.Application.Current.Dispatcher.Invoke(new Action(delegate ()
            {
                ConectoWorkSpace.Administrator.AdminPanels ConectoWorkSpace_InW = AppStart.LinkMainWindow("WAdminPanels");
                //MainWindow ConectoWorkSpace_InW = AppStart.LinkMainWindow();
                var WinOblakoVerh_ = AppStart.WindowActive("WinOblakoVerhW", TextWindows, AutoClose);
                WinOblakoVerh_.Owner = ConectoWorkSpace_InW;
                // размещаем на рабочем столе
                WinOblakoVerh_.Top = ConectoWorkSpace_InW.Top  - MesaggeTop;

                WinOblakoVerh_.Left = ConectoWorkSpace_InW.ScrollViewerd.Width - (WinOblakoVerh_.Width * 2)- MessageLeft;  //ConectoWorkSpace_InW.Left + MessageLeft;
                WinOblakoVerh_.ShowActivated = false;
                WinOblakoVerh_.Show();
                
            }));
        }
        // Процедура замены алиаса в Беке
        public  static void ChangeAliasBack(int BackFont)
        {

            string SetPort = Administrator.AdminPanels.SetPort25;
            SetPort = Convert.ToInt32(AppStart.TableReestr["BackFbd30OnOff"].ToString()) == 2 && Administrator.AdminPanels.CurrentBack == 1 ? Administrator.AdminPanels.SetPort30 : SetPort;
 
            string NewConect = Administrator.AdminPanels.ConectBack.Substring(0, Administrator.AdminPanels.ConectBack.LastIndexOf("/")+1)+ SetPort + ":" + Administrator.AdminPanels.SelectAlias;
            string StrCreate = BackFont == 1 ? "UPDATE REESTRBACK SET CONECT = " + "'" + NewConect + "'" + " WHERE REESTRBACK.PUTH = " + "'" + Administrator.AdminPanels.PuthBack + "'" : 
            "UPDATE REESTRFRONT SET CONECT = " + "'" + NewConect + "'" + " WHERE REESTRFRONT.PUTH = " + "'" + Administrator.AdminPanels.PuthBack + "'" ;

            DBConecto.DBopenFBConectionMemory(SystemBD.ConnnStringSystem().ToString(), "", "FbSystem");
            DBConecto.UniQuery UpdateQuery = new DBConecto.UniQuery(StrCreate, "FB");
            string CountTable = UpdateQuery.ExecuteUNIScalar() == null ? "" : UpdateQuery.ExecuteUNIScalar().ToString();
            DBConecto.DBcloseFBConectionMemory("FbSystem");
            if(BackFont == 1) Administrator.AdminPanels.LoadedGridBack( "SELECT * from REESTRBACK" );
            if (BackFont == 2) Administrator.AdminPanels.LoadedGridFront( "SELECT * from REESTRFRONT");
           
            string PuthBackAptune = Administrator.AdminPanels.PuthBack.Substring(0, Administrator.AdminPanels.PuthBack.LastIndexOf(@"\")) + @"\";

            if (SystemConecto.File_(PuthBackAptune + "aptune.ini", 5))
            {
                // Функция модификации  соединения в файле aptune.ini для Бек Офиса
                int Idcount = 0;
                Encoding code = Encoding.Default;
                string[] fileLines = System.IO.File.ReadAllLines(PuthBackAptune + "aptune.ini", code);
                foreach (string x in fileLines)
                {
                    if (x.Contains("DatabaseName") == true)
                    {
                        fileLines[Idcount] = "DatabaseName =" + NewConect;
                    }
                    Idcount++;
                }
                System.IO.File.WriteAllLines(PuthBackAptune + "aptune.ini", fileLines, code);
            }
 

        }


        /// <summary>
        /// Установка сервера FB Firebird 2.5 
        /// 
        /// </summary>
        public static void InstallServFB25(object ThreadObj)
        {
            // Разбор аргументов
            AppStart.RenderInfo arguments = (AppStart.RenderInfo)ThreadObj;
            string PortServer = arguments.argument1;
            string FolderServer = arguments.argument2;
            string NameServer = arguments.argument3;
            string NumberServer = arguments.argument4;
            System.Windows.Application.Current.Dispatcher.Invoke(new Action(delegate ()
            {
                ConectoWorkSpace.Administrator.AdminPanels ConectoWorkSpace_Add = AppStart.LinkMainWindow("WAdminPanels");
                ConectoWorkSpace_Add.AddSever.Foreground = Brushes.Indigo;
 
            }));
            
            // Права в ОС Win
            if (App.aSystemVirable["UserWindowIdentity"] == "1")
            {
                int Idcount = 0;
                string StrCreate = "";

                string NameDir = "Firebird_2_5"  + (Convert.ToInt32(NumberServer) == 0 ? "" : NumberServer);
                //string NameFile = SystemConecto.OS64Bit == true ? "Firebird-2.5.8.27089-0_x64.zip" : "Firebird-2.5.8.27089-0_Win32.zip";
                string NameFile = SystemConecto.OS64Bit == true ? "Firebird-2.5.9.27139-0_x64.zip" : "Firebird-2.5.9.27139-0_Win32.zip";
                // Firebird - 2.5.9.27139 - 0_Win32.zip.gz   Firebird-2.5.9.27139-0_x64.zip.gz

                // Проверка наличия свободного пространства на диске куда будем ложить 
                DriveInfo di = new DriveInfo(@"C:\");
                long Ffree = (di.TotalFreeSpace / 1024) / 1024;
                string MBFree = Ffree.ToString("#,##") + " MB";
                if (Ffree - 2000 < 0)
                {
                    string TextWin = "Установка БД Firebird 2.5 требует 5Гб свободного пространства" + Environment.NewLine + "Текущее пространство " + MBFree + Environment.NewLine + "Освободите пространство на диске и повторите установку ";
                    int AutoCl = 1;
                    int MesaggeTop = 350;
                    int MessageLeft = 650;
                    MessageErr(TextWin, AutoCl, MesaggeTop, MessageLeft);
                    System.Windows.Application.Current.Dispatcher.Invoke(new Action(delegate ()
                    {
                        ConectoWorkSpace.Administrator.AdminPanels ConectoWorkSpace_ERR = AppStart.LinkMainWindow("WAdminPanels");
                        Interface.CurrentStateInst("ServFB25OnOff", "0", "on_off_1.png", ConectoWorkSpace_ERR.ServFB25OnOff);
                    }));
                    return;
                }
                DBConecto.DBopenFBConectionMemory(SystemBD.ConnnStringSystem().ToString(), "", "FbSystem");
                string StrCount = "SELECT count(*) from SERVERACTIVFB25";
                DBConecto.UniQuery CountQuery = new DBConecto.UniQuery(StrCount, "FB");
                string CountTable = CountQuery.ExecuteUNIScalar() == null ? "" : CountQuery.ExecuteUNIScalar().ToString();
                int AllRecn = Convert.ToInt32(CountTable) > 0 ? Convert.ToInt32(CountTable) : 0;
                DBConecto.DBcloseFBConectionMemory("FbSystem");
                if (AllRecn >= 5)
                {
                    string TextWin = "Количество установленных серверов превышает 5" + Environment.NewLine + "Удалите ненужные записи из таблицы и повторите установку ";
                    int AutoCl = 1;
                    int MesaggeTop = 350;
                    int MessageLeft = 650;
                    MessageErr(TextWin, AutoCl, MesaggeTop, MessageLeft);
                    System.Windows.Application.Current.Dispatcher.Invoke(new Action(delegate ()
                    {
                        ConectoWorkSpace.Administrator.AdminPanels ConectoWorkSpace_ERR = AppStart.LinkMainWindow("WAdminPanels");
                        Interface.CurrentStateInst("ServFB25OnOff", "0", "on_off_1.png", ConectoWorkSpace_ERR.ServFB25OnOff);
                    }));
                    return;
                }
                // Проверка каталогов серверов
                SystemConectoServers.DirServer();


                string SetNameServer = Convert.ToInt32(NumberServer) <= 1 ? NameServer : NameServer + NumberServer;
                string IdPortServer = Convert.ToInt32(NumberServer) <= 1 ? PortServer : PortServer.Substring(0, 3) + Convert.ToString(5 + Convert.ToInt32(NumberServer));
                string PatchSR = Convert.ToInt32(NumberServer) <= 1 ? FolderServer : FolderServer.Substring(0, FolderServer.LastIndexOf(@"\")) + NumberServer + @"\";
 
                // Проверка установки утилит
                if (!SystemConecto.DIR_(PatchSR)) MessageBox.Show("Отсутствует папка " + PatchSR);
                if (!SystemConecto.DIR_(SystemConectoServers.PutchServer + @"tmp\BD")) MessageBox.Show("Отсутствует папка " + SystemConectoServers.PutchServer + @"tmp\BD");

                Dictionary<string, string> fbembedList = new Dictionary<string, string>();
                // Проверка наличия инсталяционного файла Firebird-2.5.8.27089-0_Win32.zip

                fbembedList.Add(SystemConectoServers.PutchServer + @"tmp\BD\" + NameFile, "server_bd/");
                if (SystemConecto.IsFilesPRG(fbembedList, -1, "- Проверка файлов во время установки сервера " + NameDir) != "True")
                {
                    var TextWin = "Отсутствует инсталяционный  файл установки сервера" + Environment.NewLine + NameFile + "  Установка прекращена. ";
                    int AutoCl = 1;
                    int MesaggeTop = 350;
                    int MessageLeft = 650;
                    MessageErr(TextWin, AutoCl, MesaggeTop, MessageLeft);
                    System.Windows.Application.Current.Dispatcher.Invoke(new Action(delegate ()
                    {
                        ConectoWorkSpace.Administrator.AdminPanels ConectoWorkSpace_InW = AppStart.LinkMainWindow("WAdminPanels");
                        Interface.CurrentStateInst("ServFB25OnOff", "0", "on_off_1.png", ConectoWorkSpace_InW.ServFB25OnOff);
                    }));
                    return;
                }
                if (SystemConecto.OS64Bit)
                {
                    if (System.IO.File.Exists(@"c:\Windows\System32\GDS32.DLL")) System.IO.File.Delete(@"c:\Windows\System32\GDS32.DLL");
                    if (System.IO.File.Exists(@"c:\Windows\SysWOW64\\GDS32.DLL")) System.IO.File.Delete(@"c:\Windows\SysWOW64\\GDS32.DLL");
                }

                // Распоковка
                Install.Extract.Unarch_arhive(SystemConectoServers.PutchServer + @"tmp\BD\" + NameFile, PatchSR);

                var GchangeConf25 = ConectoWorkSpace.INI.ReadFile(PatchSR + @"\firebird.conf");
                GchangeConf25.Set("RemoteServicePort", IdPortServer, true);
                GchangeConf25.Set("DefaultDbCachePages", "9999", true);
                GchangeConf25.Set("TempBlockSize", "2048576", true);
                GchangeConf25.Set("TempCacheLimit", SystemConecto.OS64Bit == true ? "967108864" : "367108864", true);
                GchangeConf25.Set("LockHashSlots", SystemConecto.OS64Bit == true ? "20011" : "10011", true);
                GchangeConf25.Set("TempCacheLimit", "367108864", true);
                GchangeConf25.Set("LockHashSlots", "10011", true);
                GchangeConf25.Set("CpuAffinityMask", "3", true);
                GchangeConf25.Set("OldSetClauseSemantics", "1", true);

                // Обновляем security2.fdb 
                System.IO.File.Delete(PatchSR + "security2.fdb");
                fbembedList = new Dictionary<string, string>();
                fbembedList.Add(PatchSR + "security2.fdb", "b52/server_config/");
                if (SystemConecto.IsFilesPRG(fbembedList, -1, "- Проверка файлов во время установки сервера " + "Windows") != "True")
                {
                    var TextWindows = "Отсутствует файл  security2.fdb" + Environment.NewLine + "Установка прекращена ";
                    int AutoClse = 1;
                    int MesageTop = 350;
                    int MesageLeft = 650;
                    MessageEnd(TextWindows, AutoClse, MesageTop, MesageLeft);
                    System.Windows.Application.Current.Dispatcher.Invoke(new Action(delegate ()
                    {
                        ConectoWorkSpace.Administrator.AdminPanels ConectoWorkSpace_InW = AppStart.LinkMainWindow("WAdminPanels");
                        Interface.CurrentStateInst("ServFB25OnOff", "0", "on_off_1.png", ConectoWorkSpace_InW.ServFB25OnOff);
                    }));
                    return;
                }
                // Копирование библиотек
                CopyDll(PatchSR);

                string runfb25 = " install -z ";
                // Запуск Firebird

                Process mv_prcInstaller = new Process();
                mv_prcInstaller.StartInfo.FileName = PatchSR + @"bin\instreg.exe";
                mv_prcInstaller.StartInfo.Arguments = runfb25; 
                mv_prcInstaller.StartInfo.UseShellExecute = false;
                mv_prcInstaller.StartInfo.CreateNoWindow = true;
                mv_prcInstaller.StartInfo.RedirectStandardOutput = true;
                mv_prcInstaller.Start();
                mv_prcInstaller.WaitForExit();
                mv_prcInstaller.Close();
                runfb25 = " install -s -a -g -n " + SetNameServer;
                mv_prcInstaller.StartInfo.FileName = PatchSR + @"bin\instsvc.exe";
                mv_prcInstaller.StartInfo.Arguments = runfb25; // " install -s -a  -n ConectoWS -g";    
                mv_prcInstaller.StartInfo.UseShellExecute = false;
                mv_prcInstaller.StartInfo.CreateNoWindow = true;
                mv_prcInstaller.StartInfo.RedirectStandardOutput = true;
                mv_prcInstaller.Start();
                mv_prcInstaller.WaitForExit();
                mv_prcInstaller.Close();
                runfb25 = " start -n " + SetNameServer;
                mv_prcInstaller.StartInfo.FileName = PatchSR + @"bin\instsvc.exe";
                mv_prcInstaller.StartInfo.Arguments = runfb25; // "start -n ConectoWS";     
                mv_prcInstaller.StartInfo.UseShellExecute = false;
                mv_prcInstaller.StartInfo.CreateNoWindow = true;
                mv_prcInstaller.StartInfo.RedirectStandardOutput = true;
                mv_prcInstaller.Start();
                mv_prcInstaller.WaitForExit();
                mv_prcInstaller.Close();

  

                string InsertPort25 = "";
                Idcount = 0;
                DBConecto.DBopenFBConectionMemory(SystemBD.ConnnStringSystem().ToString(), "", "FbSystem");
                StrCreate = "select * from SERVERACTIVFB25 where SERVERACTIVFB25.PUTH = " + "'" + PatchSR + "'";
                FbCommand InsertTable = new FbCommand(StrCreate, DBConecto.bdFbSystemConect);
                InsertTable.CommandType = CommandType.Text;
                FbDataReader ReadTable = InsertTable.ExecuteReader();
                while (ReadTable.Read())
                {
                    InsertPort25 = ReadTable[0].ToString();
                    Idcount = Idcount + 1;
                }
                ReadTable.Close();
                if (Idcount == 0)
                {
                    string DateCreatFB = (string)AppStart.rkAppSetingAllUser.GetValue("StartSystemFB");
                    StrCreate = "INSERT INTO SERVERACTIVFB25  values (" + IdPortServer + ",'" + PatchSR + "', '" + SetNameServer+ "', '" + DateCreatFB + "','Activ','masterkey')";
                    DBConecto.UniQuery InsertQuery = new DBConecto.UniQuery(StrCreate, "FB");
                    InsertQuery.UserQuery = string.Format(StrCreate, "SERVERACTIVFB25");
                    InsertQuery.ExecuteUNIScalar();

                }
                InsertTable.Dispose();
                DBConecto.DBcloseFBConectionMemory("FbSystem");
                Administrator.AdminPanels.SetServerGrid("SELECT * from SERVERACTIVFB25");
                ConectoWorkSpace.Administrator.AdminPanels.ProcesEnd = 0;

                if (AppStart.TableReestr["NameServer25"] == "" || AppStart.TableReestr["ServerDefault25"] == "")
                {
                    System.Windows.Application.Current.Dispatcher.Invoke(new Action(delegate ()
                    {
                        ConectoWorkSpace.Administrator.AdminPanels ConectoWorkSpace_InW = AppStart.LinkMainWindow("WAdminPanels");
                        Interface.CurrentStateInst("ServFB25OnOff", "2", "on_off_2.png", ConectoWorkSpace_InW.ServFB25OnOff);
                        ConectoWorkSpace_InW.PuthSetServer25.Text = PatchSR;
                        ConectoWorkSpace_InW.SetPuthBD25.IsEnabled = true;
                        ConectoWorkSpace_InW.AddSever.Foreground = Brushes.Indigo;
                    }));
                    Administrator.AdminPanels.UpdateKeyReestr("ServerDefault25", PatchSR);
                    Administrator.AdminPanels.UpdateKeyReestr("NameServer25", SetNameServer);

                }
                System.Windows.Application.Current.Dispatcher.Invoke(new Action(delegate ()
                {
                    ConectoWorkSpace.Administrator.AdminPanels ConectoWorkSpace_Add = AppStart.LinkMainWindow("WAdminPanels");
                    ConectoWorkSpace_Add.AddSever.Foreground = Brushes.White;

                }));
                var TextW = "Установка сервера Firebird 2.5" + Environment.NewLine + " успешно завершена";
                int AutoClose = 1;
                int MesaggeT = -170;
                int MessageL = 600;
                MessageEnd(TextW, AutoClose, MesaggeT, MessageL);
            }
            IntThreadStart--;
        }

        public static void CopyDll(string PatchSR)
        {
            //Копируем в Папку UDF -режим совместимости с B52
            //BLOBC.dll.gz
            //LibPlus.dll.gz
            //LibPlus64.dll.gz
            //blobsaveload.dll.gz
            //ExpressUdfs.dll.gz
            Dictionary<string, string> fbembedList = new Dictionary<string, string>();
            fbembedList.Add(PatchSR + @"\UDF\BLOBC.dll", "b52/server_config/");
            if (SystemConecto.IsFilesPRG(fbembedList, -1, "- Проверка файлов во время установки сервера " + "Windows") != "True")
            {
                var TextWin = "Отсутствует библиотека совместимости с B52 -BLOBC.dll. Установка прекращена.";
                int AutoCl = 1;
                int MesaggeT = 350;
                int MessageL = 650;
                MessageErr(TextWin, AutoCl, MesaggeT, MessageL);
                return;
            }

            if (SystemConecto.OS64Bit)
            {
                fbembedList.Add(PatchSR + @"\UDF\LibPlus64.dll", "b52/server_config/");
                if (SystemConecto.IsFilesPRG(fbembedList, -1, "- Проверка файлов во время установки сервера " + "Windows") != "True")
                {

                    var TextWin = "Отсутствует библиотека совместимости с B52 - LibPlus.dll. Установка прекращена.";
                    int AutoCl = 1;
                    int MesaggeT = 350;
                    int MessageL = 650;
                    MessageErr(TextWin, AutoCl, MesaggeT, MessageL);
                    return;
                }
                System.IO.File.Copy(PatchSR + @"\UDF\LibPlus64.dll", PatchSR + @"\UDF\LibPlus.dll", true);
                System.IO.File.Delete(PatchSR + @"\UDF\LibPlus64.dll");
            }
            else
            {
                fbembedList.Add(PatchSR + @"\UDF\LibPlus.dll", "b52/server_config/");
                if (SystemConecto.IsFilesPRG(fbembedList, -1, "- Проверка файлов во время установки сервера " + "Windows") != "True")
                {
                    var TextWin = "Отсутствует библиотека совместимости с B52 - LibPlus32.dll. Установка прекращена.";
                    int AutoCl = 1;
                    int MesaggeT = 350;
                    int MessageL = 650;
                    MessageErr(TextWin, AutoCl, MesaggeT, MessageL);
                    return;
                }

            }

            fbembedList.Add(PatchSR + @"\UDF\blobsaveload.dll", "b52/server_config/");
            if (SystemConecto.IsFilesPRG(fbembedList, -1, "- Проверка файлов во время установки сервера " + "Windows") != "True")
            {
                var TextWin = "Отсутствует библиотека совместимости с B52 -blobsaveload.dll. Установка прекращена.";
                int AutoCl = 1;
                int MesaggeT = 350;
                int MessageL = 650;
                MessageErr(TextWin, AutoCl, MesaggeT, MessageL);
                return;
            }

            fbembedList.Add(PatchSR + @"\UDF\ExpressUdfs.dll", "b52/server_config/");
            if (SystemConecto.IsFilesPRG(fbembedList, -1, "- Проверка файлов во время установки сервера " + "Windows") != "True")
            {
                var TextWin = "Отсутствует библиотека совместимости с B52 -ExpressUdfs.dll. Установка прекращена.";
                int AutoCl = 1;
                int MesaggeT = 350;
                int MessageL = 650;
                MessageErr(TextWin, AutoCl, MesaggeT, MessageL);
                return;
            }
        }

        public static void InstallTTF16()
        {
            AppStart.RenderInfo Arguments01 = new AppStart.RenderInfo() { };
            Arguments01.argument1 = "1";
            Thread thStartTimer01 = new Thread(Administrator.AdminPanels.CheckTTF16);
            thStartTimer01.SetApartmentState(ApartmentState.STA);
            thStartTimer01.IsBackground = true; // Фоновый поток
            thStartTimer01.Start(Arguments01);
            IntThreadStart++;
 
        }


        public static void UnInstallTTF16()
        {
 
            DllWork.RegSrv32(SystemConectoServers.PutchLib + "TTF16.ocx", "/u");
            System.Windows.Application.Current.Dispatcher.Invoke(new Action(delegate ()
            {
                ConectoWorkSpace.Administrator.AdminPanels ConectoWorkSpace_ttf16 = AppStart.LinkMainWindow("WAdminPanels");
                Interface.CurrentStateInst("CheckTTF16OnOff", "0", "on_off_1.png", ConectoWorkSpace_ttf16.CheckTTF16OnOff);
                Interface.CurrentStateInst("CheckTTF16OnOff_Front", "0", "on_off_1.png", ConectoWorkSpace_ttf16.CheckTTF16OnOff_Front);
            }));

        }

        /// <summary>
        /// Деинсталяция  сервера FireBird 2.5
        /// 
        /// </summary>
        public static void UnInstallServFB25(object ThreadObj)
        {
            int DeletSerever = 0;
            AppStart.RenderInfo Arguments = (AppStart.RenderInfo)ThreadObj;
            // Права в ОС Win
            if (App.aSystemVirable["UserWindowIdentity"] == "1")
            {
                Administrator.AdminPanels.Inst2530 = "25"; string StrCreate = "";
                string NameServer = Arguments.argument1 == "Delete" ? Administrator.AdminPanels.SetName25 : AppStart.TableReestr["NameServer25"];
                string PatchSR = Arguments.argument1 == "Delete" ? Administrator.AdminPanels.SetPuth25 : AppStart.TableReestr["ServerDefault25"];

 
 
                    if (SystemConecto.File_(PatchSR + @"bin\instsvc.exe", 5) && Administrator.AdminPanels.SetActiv25 != "Stop")
                    {
                        DBConecto.DBopenFBConectionMemory(SystemBD.ConnnStringSystem().ToString(), "", "FbSystem");
                        StrCreate = "SELECT * from REESTRBACK where REESTRBACK.SERVER = " + "'" + Administrator.AdminPanels.SetName25 + "'";
                        string TempNameBack = "";
                        int Idcount = 0;
                        DBConecto.DBopenFBConectionMemory(SystemBD.ConnnStringSystem().ToString(), "", "FbSystem");
                        FbCommand SelectTable = new FbCommand(StrCreate, DBConecto.bdFbSystemConect);
                        FbDataReader ReadOutTable = SelectTable.ExecuteReader();
                        while (ReadOutTable.Read())
                        {
                            TempNameBack = ReadOutTable[0].ToString();
                            Idcount = Idcount + 1;
                        }
                        ReadOutTable.Close();
                        if (Idcount != 0)
                        {
                            var TextWindows = "Удаление недопустимо. Установлен БекОфис";
                            int AutoClose = 1;
                            int MesaggeTop = -170;
                            int MessageLeft = 300;
                            MessageEnd(TextWindows, AutoClose, MesaggeTop, MessageLeft);
                            System.Windows.Application.Current.Dispatcher.Invoke(new Action(delegate ()
                            {
                                ConectoWorkSpace.Administrator.AdminPanels ConectoWorkSpace_Del = AppStart.LinkMainWindow("WAdminPanels");
                                ConectoWorkSpace_Del.DeletSrever25.Foreground = Brushes.White;

                            }));
                        
                            return;
                        }
                        DBConecto.DBcloseFBConectionMemory("FbSystem");
                        DBConecto.DBopenFBConectionMemory(SystemBD.ConnnStringSystem().ToString(), "", "FbSystem");
                        StrCreate = "SELECT * from REESTRFRONT where REESTRFRONT.SERVER = " + "'" + Administrator.AdminPanels.SetName25 + "'";
                        Idcount = 0;
                        DBConecto.DBopenFBConectionMemory(SystemBD.ConnnStringSystem().ToString(), "", "FbSystem");
                        FbCommand SelectFront = new FbCommand(StrCreate, DBConecto.bdFbSystemConect);
                        FbDataReader ReadOutFront = SelectFront.ExecuteReader();
                        while (ReadOutFront.Read())
                        {
                            TempNameBack = ReadOutFront[0].ToString();
                            Idcount = Idcount + 1;
                        }
                        ReadOutFront.Close();
                        if (Idcount != 0)
                        {
                            var TextWindows = "Удаление недопустимо. Установлен ФронтОфис";
                            int AutoClose = 1;
                            int MesaggeTop = -170;
                            int MessageLeft = 300;
                            MessageEnd(TextWindows, AutoClose, MesaggeTop, MessageLeft);
                            System.Windows.Application.Current.Dispatcher.Invoke(new Action(delegate ()
                            {
                                ConectoWorkSpace.Administrator.AdminPanels ConectoWorkSpace_Del = AppStart.LinkMainWindow("WAdminPanels");
                                ConectoWorkSpace_Del.DeletSrever25.Foreground = Brushes.White;
                            }));
                            return;
                        }
                        DBConecto.DBcloseFBConectionMemory("FbSystem");
                        if(InstallB52.RemoveServerFB25(PatchSR, NameServer)>0) DeletSerever =1;
 
                    }
 
                if (DeletSerever == 1)
                {
                    StrCreate = (PatchSR == "" ? "DELETE from SERVERACTIVFB25" : "DELETE from SERVERACTIVFB25 where SERVERACTIVFB25.PUTH = ") + "'" + PatchSR + "'";
                    ConectoWorkSpace.Administrator.AdminPanels.ModifyTable(StrCreate); 
                    string StrCount = "SELECT count(*) from SERVERACTIVFB25";
                    DBConecto.UniQuery CountQuery = new DBConecto.UniQuery(StrCount, "FB");
                    string CountTableSERVER = CountQuery.ExecuteUNIScalar() == null ? "" : CountQuery.ExecuteUNIScalar().ToString();

                    StrCreate = "DELETE from CONNECTIONBD25  WHERE CONNECTIONBD25.NAMESERVER =" + "'" + NameServer + "'";
                    ConectoWorkSpace.Administrator.AdminPanels.ModifyTable(StrCreate);

                    if (Administrator.AdminPanels.SetActiv25 == "Stop")
                    {
                        string PatchNoactiv = PatchSR.Substring(0, PatchSR.Length - 1);
                        Directory.Delete(PatchNoactiv, true); // 
                        System.Windows.Application.Current.Dispatcher.Invoke(new Action(delegate ()
                        {
                            ConectoWorkSpace.Administrator.AdminPanels ConectoWorkSpace_Del = AppStart.LinkMainWindow("WAdminPanels");
                            ConectoWorkSpace_Del.DeletSrever25.Foreground = Brushes.White;
                        }));
                        Administrator.AdminPanels.SetServerGrid("SELECT * from SERVERACTIVFB25");
                        return;
                    }


                    if (Convert.ToUInt32(CountTableSERVER) == 1 ) 
                    {
                    
                        StrCreate = "SELECT first 1 * from SERVERACTIVFB25";
                        DBConecto.DBopenFBConectionMemory(SystemBD.ConnnStringSystem().ToString(), "", "FbSystem");
                        FbCommand FirstKey = new FbCommand(StrCreate, DBConecto.bdFbSystemConect);
                        FirstKey.CommandType = CommandType.Text;
                        FbDataReader ReadOutTable = FirstKey.ExecuteReader();
                        while (ReadOutTable.Read())
                        {
                            Administrator.AdminPanels.SetPort25 = ReadOutTable[0].ToString();
                            Administrator.AdminPanels.SetPuth25 = ReadOutTable[1].ToString();
                            Administrator.AdminPanels.SetName25 = ReadOutTable[2].ToString();
                        }
                        ReadOutTable.Close();
                        FirstKey.Dispose();

                        if (Administrator.AdminPanels.SetActiv25 != "Stop")
                        {
                            Administrator.AdminPanels.UpdateKeyReestr("ServerDefault25", Administrator.AdminPanels.SetPuth25);
                            Administrator.AdminPanels.UpdateKeyReestr("NameServer25", Administrator.AdminPanels.SetName25);
                            Administrator.AdminPanels.UpdateKeyReestr("ServFB25OnOff", "2");
                            System.Windows.Application.Current.Dispatcher.Invoke(new Action(delegate ()
                            {
                                ConectoWorkSpace.Administrator.AdminPanels ConectoWorkSpace_Puth = AppStart.LinkMainWindow("WAdminPanels");
                                Interface.CurrentStateInst("ServFB25OnOff", "2", "on_off_2.png", ConectoWorkSpace_Puth.ServFB25OnOff);
                                ConectoWorkSpace_Puth.PuthSetServer25.Text = Administrator.AdminPanels.SetPuth25;
                            }));

                            string PuthBd = "", AliasBd = "", NameServerBd = "";

                            StrCreate = "SELECT first 1 * from CONNECTIONBD25 WHERE CONNECTIONBD25.NAMESERVER =" + "'" + Administrator.AdminPanels.SetName25 + "'";
                            DBConecto.DBopenFBConectionMemory(SystemBD.ConnnStringSystem().ToString(), "", "FbSystem");
                            FbCommand FirstConect = new FbCommand(StrCreate, DBConecto.bdFbSystemConect);
                            FirstConect.CommandType = CommandType.Text;
                            FbDataReader ReadFirstConectTable = FirstConect.ExecuteReader();
                            while (ReadFirstConectTable.Read())
                            {
                                PuthBd = ReadFirstConectTable[0].ToString();
                                AliasBd = ReadFirstConectTable[1].ToString();
                                NameServerBd = ReadFirstConectTable[2].ToString();
                            }
                            ReadFirstConectTable.Close();
                            FirstConect.Dispose();
                            DBConecto.DBcloseFBConectionMemory("FbSystem");
                            Administrator.AdminPanels.Grid_Puth("SELECT * from CONNECTIONBD25 WHERE CONNECTIONBD25.NAMESERVER =" + "'" + Administrator.AdminPanels.SetName25 + "'");
                            System.Windows.Application.Current.Dispatcher.Invoke(new Action(delegate ()
                            {
                                ConectoWorkSpace.Administrator.AdminPanels ConectoWorkSpace_InW = AppStart.LinkMainWindow("WAdminPanels");
                                ConectoWorkSpace_InW.PuthSetBD25.Text = PuthBd;
                                Administrator.AdminPanels.UpdateKeyReestr("PuthSetBD25", PuthBd);
                                Interface.CurrentStateInst("SetPuthBD25", "2", "on_off_2.png", ConectoWorkSpace_InW.SetPuthBD25);
                            }));
                        }
                        else
                        {
                            Administrator.AdminPanels.UpdateKeyReestr("ServerDefault25", "");
                            Administrator.AdminPanels.UpdateKeyReestr("NameServer25", "");
                            Administrator.AdminPanels.UpdateKeyReestr("ServFB25OnOff", "0");
                            System.Windows.Application.Current.Dispatcher.Invoke(new Action(delegate ()
                            {
                                ConectoWorkSpace.Administrator.AdminPanels ConectoWorkSpace_Puth = AppStart.LinkMainWindow("WAdminPanels");
                                Interface.CurrentStateInst("ServFB25OnOff", "0", "on_off_1.png", ConectoWorkSpace_Puth.ServFB25OnOff);
                                ConectoWorkSpace_Puth.PuthSetServer25.Text = "";
                            }));
                        }

                    }
 
                    if (Convert.ToUInt32(CountTableSERVER) == 0)
                    {
                        Administrator.AdminPanels.SetPuth25 = AppStart.TableReestr["ServerDefault25"];
                        Administrator.AdminPanels.UpdateKeyReestr("ServerDefault25", "");
                        Administrator.AdminPanels.UpdateKeyReestr("NameServer25", "");
                        Administrator.AdminPanels.UpdateKeyReestr("BackFbd25OnOff", "0");
                        Administrator.AdminPanels.UpdateKeyReestr("FrontFbd25OnOff", "0");
                        Administrator.AdminPanels.UpdateKeyReestr("InstBackOnOff", "");
                        Administrator.AdminPanels.UpdateKeyReestr("ServFB25OnOff", "");
                        System.Windows.Application.Current.Dispatcher.Invoke(new Action(delegate ()
                        {
                            ConectoWorkSpace.Administrator.AdminPanels ConectoWorkSpace_Puth = AppStart.LinkMainWindow("WAdminPanels");
                            Interface.CurrentStateInst("ServFB25OnOff", "0", "on_off_1.png", ConectoWorkSpace_Puth.ServFB25OnOff);
                            ConectoWorkSpace_Puth.PuthSetServer25.Text = "";
                        }));
 
                        StrCreate = "DELETE * from CONNECTIONBD25";
                        DBConecto.UniQuery DeletFull = new DBConecto.UniQuery(StrCreate, "FB");
                        DeletFull.ExecuteUNIScalar();
                        DBConecto.DBcloseFBConectionMemory("FbSystem");
                        AppStart.RenderInfo Arguments02 = new AppStart.RenderInfo() { };
                        Arguments02.argument1 = "2";
                        Arguments02.argument2 = "";
                        Thread thStartTimer01 = new Thread(UnInstallSetPuthBD25);
                        thStartTimer01.SetApartmentState(ApartmentState.STA);
                        thStartTimer01.IsBackground = true; // Фоновый поток
                        thStartTimer01.Start(Arguments02);
                        IntThreadStart++;
                        Administrator.AdminPanels.Grid_Puth("SELECT * from CONNECTIONBD25");
                    }
                    // Удаление файлов
                    string PatchDelete = PatchSR.Substring(0, PatchSR.Length-1);
                    Directory.Delete(PatchDelete, true); // 
                    Administrator.AdminPanels.SetServerGrid("SELECT * from SERVERACTIVFB25");
                    System.Windows.Application.Current.Dispatcher.Invoke(new Action(delegate ()
                    {
                        ConectoWorkSpace.Administrator.AdminPanels ConectoWorkSpace_Del = AppStart.LinkMainWindow("WAdminPanels");
                        ConectoWorkSpace_Del.DeletSrever25.Foreground = Brushes.White;
                    }));

                }
 
            }

        }

        // Установка БД для  FireBird 2.5
        public static void InstallSetPuthBD2_5()
        {
            ConectoWorkSpace.Administrator.AdminPanels ConectoWorkSpace_InW = AppStart.LinkMainWindow("WAdminPanels");
            if (ConectoWorkSpace_InW == null)
            {
                return;
            }
            else
            {
                AppStart.RenderInfo Arguments01 = new AppStart.RenderInfo() { };
                Arguments01.argument1 = "2";
                Arguments01.argument2 = "";
                Thread thStartTimer01 = new Thread(InstallSetPuthBD25);
                thStartTimer01.SetApartmentState(ApartmentState.STA);
                thStartTimer01.IsBackground = true; // Фоновый поток
                thStartTimer01.Start(Arguments01);
                IntThreadStart++;

            }
        }

        public static void UnInstallSetPuthBD2_5()
        {
   
                AppStart.RenderInfo Arguments01 = new AppStart.RenderInfo() { };
                Arguments01.argument1 = "2";
                Arguments01.argument2 = "";
                Thread thStartTimer01 = new Thread(UnInstallSetPuthBD25);
                thStartTimer01.SetApartmentState(ApartmentState.STA);
                thStartTimer01.IsBackground = true; // Фоновый поток
                thStartTimer01.Start(Arguments01);
                IntThreadStart++;

       
        }
        public static void InstallSetPuthBD25(object ThreadObj)
        {

            if (AppStart.TableReestr["ServFB25OnOff"] != "2")
            {
                var TextWin = "Connection c БД невозможно" + Environment.NewLine + "Установите Firebird_2_5";
                int AutoCl = 1;
                int MesaggeT = 100;
                int MessageL = 850;
                MessageEnd(TextWin, AutoCl, MesaggeT, MessageL);
                return;
            }

            System.Windows.Application.Current.Dispatcher.Invoke(new Action(delegate ()
            {
                Administrator.AdminPanels.NameObj = "SetPuthBD25";
                ConectoWorkSpace.Administrator.AdminPanels ConectoWorkSpace_InW = AppStart.LinkMainWindow("WAdminPanels");
                var pict = (Image)LogicalTreeHelper.FindLogicalNode(ConectoWorkSpace_InW, Administrator.AdminPanels.NameObj);
                var uriSource = new Uri(@"/Conecto®%20WorkSpace;component/Images/on_off_3.png", UriKind.Relative);
                pict.Source = new BitmapImage(uriSource);
            }));

            InstallSetBD25();
            if (Administrator.AdminPanels.ProcesEnd == 2) RunCreatBD25();
        }


        public static void RunCreatBD25()
        {
            AppStart.RenderInfo Arguments01 = new AppStart.RenderInfo() { };
            Arguments01.argument1 = "2";
            Arguments01.argument2 = "";
            Thread thStartTimer01 = new Thread(CreatBD25TH);
            thStartTimer01.SetApartmentState(ApartmentState.STA);
            thStartTimer01.IsBackground = true; // Фоновый поток
            thStartTimer01.Start(Arguments01);
            IntThreadStart++;
        }

        public static void InstallSetBD25()  
        {
            // Права в ОС Win
            if (App.aSystemVirable["UserWindowIdentity"] == "1")
            {
 

                string PatchSRBD = AppStart.TableReestr["PuthSetBD25"] == "" ? SystemConectoServers.PutchServerData + "hub.fdb" : AppStart.TableReestr["PuthSetBD25"];
                string PatchFoliderBD = AppStart.TableReestr["PuthSetBD25"] == "" ? SystemConectoServers.PutchServerData : AppStart.TableReestr["PuthSetBD25"].Substring(1, AppStart.TableReestr["PuthSetBD25"].LastIndexOf(@"\"));
                string PatchSR = AppStart.TableReestr["ServerDefault25"];
                string NameFileBD = "hub.fbk";

                if (Administrator.AdminPanels.NameObj == "NewServerSetPuthBD25")
                {
                    PatchSRBD = ConectoWorkSpace.Administrator.AdminPanels.PathFileBDText;
                    PatchFoliderBD = ConectoWorkSpace.Administrator.AdminPanels.PathFileBDText.Substring(1, ConectoWorkSpace.Administrator.AdminPanels.PathFileBDText.LastIndexOf(@"\"));
                    PatchSR = ConectoWorkSpace.Administrator.AdminPanels.SetPuth25;
                }


                // Проверка наличия свободного пространства на диске куда будем ложить 
                DriveInfo di = new DriveInfo(@"C:\");
                long Ffree = (di.TotalFreeSpace / 1024) / 1024;
                string MBFree = Ffree.ToString("#,##") + " MB";
                if (Ffree - 2000 < 0)
                {
                    var TextWindows = "Установка БД Firebird 2.5 требует 5Гб свободного пространства" + Environment.NewLine + "Текущее пространство " + MBFree + Environment.NewLine + "Освободите пространство на диске и повторите установку ";
                    ErrInstallSetBD25(TextWindows);
                    return;
                }

                Dictionary<string, string> fbembedList = new Dictionary<string, string>();
                // запись БД в папку Servers\data\
                if (!SystemConecto.DIR_(@"c:\temp\")) MessageBox.Show("Отсутствует папка " + @"c:\temp\");
                fbembedList.Add(@"c:\temp\" + NameFileBD, "b52/base/");
                if (SystemConecto.IsFilesPRG(fbembedList, -1, "- Проверка файлов во время установки БД " + PatchSRBD) != "True")
                {

                    var TextWindows = "Отсутствует  файл БД hub.fbk" + Environment.NewLine + "Установка прекращена ";
                    ErrInstallSetBD25(TextWindows);
                    return;
                }

 
                if (!SystemConecto.File_(PatchSR + @"bin\instsvc.exe", 5))
                {
                    var TextWindows = "Отсутствует файл instsvc.exe." + Environment.NewLine + "Установка прекращена ";
                    ErrInstallSetBD25(TextWindows);
                    return;
                }

                if (!SystemConecto.File_(PatchSR + @"bin\gbak.exe", 5))
                {
                    var TextWindows = "Отсутствует файл gbak.exe. Установка БД прекращена. " + Environment.NewLine + "Необходимо проверить наличие утилиты распаковщика БД";
                    ErrInstallSetBD25(TextWindows);
                    return;
                }

                if (!System.IO.File.Exists(PatchSR + "aliases.conf"))
                {
                    var TextWindows = "Отсутствует файл aliases.conf." + Environment.NewLine + "Установка прекращена ";
                    ErrInstallSetBD25(TextWindows);
                    return;
                }
                int IndRecn = 0; string SetSec = "";
                string PortServ = Administrator.AdminPanels.NameObj == "NewServerSetPuthBD25" ? Administrator.AdminPanels.SetPort25 : "3055";
                string[] fileLines = System.IO.File.ReadAllLines(PatchSR + "aliases.conf"); 
                foreach (string x in fileLines)
                {
                    if (x.Contains("=") == true && x.StartsWith(Administrator.AdminPanels.SelectAlias) == true && x.Contains("$") == false && x.Contains("employee") == false) IndRecn++;
                    if (x.Contains("=") == true && x.StartsWith("sec") == true && x.Contains("security2.fdb") == true && x.Contains("employee") == false) SetSec = "sec";
                }
                if (IndRecn == 0 && SetSec == "")
                {
                    System.IO.File.Delete(PatchSR + "aliases.conf");
                    var Change = new ConectoWorkSpace.INI(PatchSR + "aliases.conf");
                    Change.Set("sec", PatchSR + "security2.fdb", true);
                    Change.Set(Administrator.AdminPanels.SelectAlias, PatchSRBD, true);

                    fileLines = System.IO.File.ReadAllLines(PatchSR + "firebird.conf");
                    foreach (string x in fileLines)
                    {
                        if (x.Contains("=") == true && x.StartsWith("RemoteServicePort") == true && x.Contains(PortServ) == true ) IndRecn++;
                    }
                    if (IndRecn == 0)
                    {
                        var GchangeConf25 = ConectoWorkSpace.INI.ReadFile(PatchSR + @"firebird.conf");
                        GchangeConf25.Set("RemoteServicePort", PortServ, true);
                        GchangeConf25.Set("DefaultDbCachePages", "9999", true);
                        GchangeConf25.Set("TempBlockSize", "2048576", true);
                        GchangeConf25.Set("TempCacheLimit",  "367108864", true);
                        GchangeConf25.Set("LockHashSlots",  "10011", true);
                        GchangeConf25.Set("TempCacheLimit", SystemConecto.OS64Bit == true ? "967108864" : "367108864", true);
                        GchangeConf25.Set("LockHashSlots", SystemConecto.OS64Bit == true ? "20011" : "10011", true);
                        GchangeConf25.Set("CpuAffinityMask", "3", true);
                        GchangeConf25.Set("OldSetClauseSemantics", "1", true);

                    }
    
                }
                else
                {
                    if (IndRecn == 0 && SetSec == "sec")
                    {
                        var GchangeaAliases25 = ConectoWorkSpace.INI.ReadFile(PatchSR + "aliases.conf");
                        GchangeaAliases25.Set(Administrator.AdminPanels.SelectAlias, PatchSRBD, true);
                        int Idcount = 0, Idcountout = 0;
                        string[] fileoutLines = new string[50];
                        fileLines = System.IO.File.ReadAllLines(PatchSR + "aliases.conf");
                        foreach (string x in fileLines)
                        {
                            if (x.Count() != 0 && x.Length != 0)
                            {
                                fileoutLines[Idcountout] = fileLines[Idcount];
                                Idcountout++;
                            }
                            Idcount++;
                        }
                        System.IO.File.WriteAllLines(PatchSR + "aliases.conf", fileoutLines);
                    }
 
                }
                // Перезапуск FireBird 
  
                DBConecto.DBopenFBConectionMemory(SystemBD.ConnnStringSystem().ToString(), "", "FbSystem");
                string StrCreate = "select * from SERVERACTIVFB25 where SERVERACTIVFB25.PUTH = " + "'" + PatchSR + "'";
                FbCommand SelectTable = new FbCommand(StrCreate, DBConecto.bdFbSystemConect);
                SelectTable.CommandType = CommandType.Text;
                FbDataReader ReadOutTable = SelectTable.ExecuteReader();
                while (ReadOutTable.Read())
                {
                    Administrator.AdminPanels.SetName25 = ReadOutTable[2].ToString();
                    Administrator.AdminPanels.CurrentPasswFb25 = ReadOutTable[5].ToString();
                }
                ReadOutTable.Close();
                SelectTable.Dispose();
                DBConecto.DBcloseFBConectionMemory("FbSystem");
                string OldImageObj = Administrator.AdminPanels.ImageObj;
                Administrator.AdminPanels.ImageObj = "RestartServFB25";
                RestartFB25(PatchSR, Administrator.AdminPanels.SetName25);
                Administrator.AdminPanels.ImageObj = OldImageObj;
                string PatchHub = SystemConectoServers.PutchServerData + @"hub.fdb";
                if (Administrator.AdminPanels.NameObj == "NewServerSetPuthBD25")PatchHub = ConectoWorkSpace.Administrator.AdminPanels.PathFileBDText;
               
                if (!System.IO.File.Exists(PatchHub))
                {
                    System.Windows.Application.Current.Dispatcher.Invoke(new Action(delegate ()
                    {
                        double length = (System.IO.File.ReadAllBytes(@"c:\temp\hub.fbk").Length / 1024 / 1024) / 1.0;
                        Administrator.AdminPanels.TimeAutoCloseWin = (int)length;
                        MainWindow ConectoWorkSpace_InW = AppStart.LinkMainWindow();
                        WaitMessage WaitWindow = new WaitMessage();
                        WaitWindow.Owner = ConectoWorkSpace_InW; // AppStart.LinkMainWindow(); ;
                        WaitWindow.Top = (ConectoWorkSpace_InW.Top + 7) + 20; //+ this.Close_F.Margin.Top + (this.Close_F.Height - 2)
                        WaitWindow.Left = (ConectoWorkSpace_InW.Left) + 900; //- (WaitWindow.Width - 22)+ this.Close_F.Margin.Left 
                        WaitWindow.Show();
                    }));
                }
                Administrator.AdminPanels.ProcesEnd = 2;
            }
            IntThreadStart--;
        }

        public static void ErrInstallSetBD25(string TextWindows)
        {
            int AutoClose = 1;
            int MesaggeTop = 350;
            int MessageLeft = 650;
            MessageErr(TextWindows, AutoClose, MesaggeTop, MessageLeft);
            System.Windows.Application.Current.Dispatcher.Invoke(new Action(delegate ()
            {
                ConectoWorkSpace.Administrator.AdminPanels ConectoWorkSpace_InW = AppStart.LinkMainWindow("WAdminPanels");
                ConectoWorkSpace_InW.PuthSetBD25.Text = "";
                ConectoWorkSpace_InW.DefNameServer25.Text = "";
                Interface.CurrentStateInst("SetPuthBD25", "0", "on_off_1.png", ConectoWorkSpace_InW.SetPuthBD25);
            }));
            Administrator.AdminPanels.ProcesEnd = 3;
        }


        public static void CreatBD25TH(object ThreadObj)
        {

            string PatchSRBD = AppStart.TableReestr["PuthSetBD25"] == "" ? SystemConectoServers.PutchServerData + "hub.fdb" : AppStart.TableReestr["PuthSetBD25"];
            string PatchFoliderBD = AppStart.TableReestr["PuthSetBD25"] == "" ? SystemConectoServers.PutchServerData : AppStart.TableReestr["PuthSetBD25"].Substring(0, AppStart.TableReestr["PuthSetBD25"].LastIndexOf(@"\"));
            string hubdate = AppStart.TableReestr["SetTextIp4BackOf"] + "/" + AppStart.TableReestr["SetPortServer25"] + ":" + Administrator.AdminPanels.SelectAlias;
            string RunGbak = @"c:\Program Files\Conecto\Servers\Firebird_2_5\bin\gbak.exe";
            if (Administrator.AdminPanels.NameObj == "NewServerSetPuthBD25")
            {
                PatchSRBD = ConectoWorkSpace.Administrator.AdminPanels.PathFileBDText;
                PatchFoliderBD = ConectoWorkSpace.Administrator.AdminPanels.PathFileBDText.Substring(1, ConectoWorkSpace.Administrator.AdminPanels.PathFileBDText.LastIndexOf(@"\"));
                hubdate = AppStart.TableReestr["SetTextIp4BackOf"] + "/" + Administrator.AdminPanels.SetPort25+ ":" + Administrator.AdminPanels.SelectAlias;
                RunGbak = ConectoWorkSpace.Administrator.AdminPanels.SetPuth25 + @"\bin\gbak.exe"; 
            }

            if (!System.IO.File.Exists(PatchSRBD))
            {


                string ArgumentCmd = @" -rep c:\temp\hub.fbk " + hubdate + @" -v -bu 200 -user sysdba -pass " + Administrator.AdminPanels.CurrentPasswFb25; //-y c:\temp\log.txt
 
                     Process CmdDos = new Process();
                    CmdDos.StartInfo.FileName = RunGbak; //  @"C:\Program Files\Conecto\Servers\Firebird_2_5\bin\gbak.exe";   // PatchSR + @"bin\gbak.exe";
                    CmdDos.StartInfo.Arguments = ArgumentCmd; // @"-c c:\temp\hub.fbk 127.0.0.1/3055:hub  -user sysdba -pass masterkey";
                    CmdDos.StartInfo.UseShellExecute = false;
                //CmdDos.StartInfo.CreateNoWindow = true;
                //CmdDos.StartInfo.RedirectStandardOutput = true;
                CmdDos.Start();
                    CmdDos.WaitForExit();
                    CmdDos.Close();
   

                   System.Windows.Application.Current.Dispatcher.Invoke(new Action(delegate ()
                   {

                       ConectoWorkSpace.WaitMessage ConectoWorkSpace_Off = AppStart.LinkMainWindow("WaitMessage_");
                       ConectoWorkSpace_Off.Close();
     
                   }));

                System.Windows.Application.Current.Dispatcher.Invoke(new Action(delegate ()
                {
                    ConectoWorkSpace.Administrator.AdminPanels ConectoWorkSpace_InW = AppStart.LinkMainWindow("WAdminPanels");
                    ConectoWorkSpace_InW.TablPuthBD.IsEnabled = true;
                }));
                

            }
     

            if (System.IO.File.Exists(PatchSRBD))
            {
                // процедура завершена успешно
                Administrator.AdminPanels.IndRecno = 0;

                if (Administrator.AdminPanels.NameObj == "NewServerSetPuthBD25")
                {
                    System.Windows.Application.Current.Dispatcher.Invoke(new Action(delegate ()
                    {
                        ConectoWorkSpace.Administrator.AdminPanels ConectoWorkSpace_Puth = AppStart.LinkMainWindow("WAdminPanels");
                        ConectoWorkSpace_Puth.SelectNewBD25.Visibility = Visibility.Collapsed;
                        ConectoWorkSpace_Puth.TextBox_Fbd25.Visibility = Visibility.Collapsed;
                        ConectoWorkSpace_Puth.Dir_Fbd25.Visibility = Visibility.Collapsed;
                        ConectoWorkSpace_Puth.LabelPuth.Visibility = Visibility.Collapsed;
                        ConectoWorkSpace_Puth.DirSetBD25.Foreground = Brushes.White;
                        ConectoWorkSpace_Puth.DirSetBD25.Content = "Добавить";

                        string StrCreate = "SELECT * from CONNECTIONBD25 WHERE CONNECTIONBD25.NAMESERVER =" + "'" + Administrator.AdminPanels.SetName25 + "'";
                        DBConecto.DBopenFBConectionMemory(SystemBD.ConnnStringSystem().ToString(), "", "FbSystem");
                        DBConecto.UniQuery CountQuery = new DBConecto.UniQuery(StrCreate, "FB");
                        string CountTable = CountQuery.ExecuteUNIScalar() == null ? "" : CountQuery.ExecuteUNIScalar().ToString();
                        DBConecto.DBcloseFBConectionMemory("FbSystem");
                        if (CountTable == "")
                        {
                            ConectoWorkSpace_Puth.PuthSetBD25.Text = PatchSRBD;
                            ConectoWorkSpace_Puth.DefNameServer25.Text = ConectoWorkSpace.Administrator.AdminPanels.SetName25;
                            ConectoWorkSpace_Puth.DirSetBD25.IsEnabled = true;
                            ConectoWorkSpace_Puth.DirSetBD25.Foreground = Brushes.White;
                            Interface.CurrentStateInst("SetPuthBD25", "2", "on_off_2.png", ConectoWorkSpace_Puth.SetPuthBD25);
                        }
                    }));
                    Administrator.AdminPanels.SelectAlias = (Administrator.AdminPanels.PathFileBDText.Substring(Administrator.AdminPanels.PathFileBDText.LastIndexOf(@"\") + 1, Administrator.AdminPanels.PathFileBDText.LastIndexOf(@".") - Administrator.AdminPanels.PathFileBDText.LastIndexOf(@"\") - 1));
                    ConectoWorkSpace.Administrator.AdminPanels.InsertConect(PatchSRBD, Administrator.AdminPanels.SetName25);


                }
                else
                {
                   Administrator.AdminPanels.LoadAlias(AppStart.TableReestr["NameServer25"], AppStart.TableReestr["ServerDefault25"]);
                    Administrator.AdminPanels.UpdateKeyReestr("PuthSetBD25", PatchSRBD);
                    Administrator.AdminPanels.UpdateKeyReestr("СurrentPasswABD25", "masterkey");
                    Administrator.AdminPanels.UpdateKeyReestr("BackOfAdresPorta", "3182");
                    Administrator.AdminPanels.UpdateKeyReestr("AdresPortFront", "3182");

                    System.Windows.Application.Current.Dispatcher.Invoke(new Action(delegate ()
                    {
                        ConectoWorkSpace.Administrator.AdminPanels ConectoWorkSpace_Puth = AppStart.LinkMainWindow("WAdminPanels");
                        ConectoWorkSpace_Puth.PuthSetBD25.Text = PatchSRBD;
                        ConectoWorkSpace_Puth.DefNameServer25.Text = ConectoWorkSpace.Administrator.AdminPanels.SetName25;
                        ConectoWorkSpace_Puth.DirSetBD25.IsEnabled = true;
                        ConectoWorkSpace_Puth.DirSetBD25.Foreground = Brushes.White;
                        Interface.CurrentStateInst("SetPuthBD25", "2", "on_off_2.png", ConectoWorkSpace_Puth.SetPuthBD25);
                    }));
                    ConectoWorkSpace.Administrator.AdminPanels.InsertConect(PatchSRBD, AppStart.TableReestr["NameServer25"]);
                }
                var TextWinEnd = "Присоединение БД к серверу " + Environment.NewLine + "успешно завершено. ";
                int AutoCl = 1;
                int MesaggeT = -170;
                int MessageL = 600;
                MessageEnd(TextWinEnd, AutoCl, MesaggeT, MessageL);
 

            }
            else
            {
                var TextWin = "Присоединение БД к серверу " + Environment.NewLine + "не выполнено. ";
                int AutoCl = 1;
                int MesaggeT = -170;
                int MessageL = 600;
                MessageEnd(TextWin, AutoCl, MesaggeT, MessageL);
                if (Administrator.AdminPanels.NameObj != "NewServerSetPuthBD25")
                {
                    System.Windows.Application.Current.Dispatcher.Invoke(new Action(delegate ()
                    {
                        ConectoWorkSpace.Administrator.AdminPanels ConectoWorkSpace_Puth = AppStart.LinkMainWindow("WAdminPanels");
                        Interface.CurrentStateInst("SetPuthBD25", "1", "on_off_1.png", ConectoWorkSpace_Puth.SetPuthBD25);
                    }));
                }
   
            }
            IntThreadStart--;

        }

        // Удаление  БД для  FireBird 2.5
        public static void UnInstallSetPuthBD25(object ThreadObj)
        {



            string CurrentPuthDef = "";
            System.Windows.Application.Current.Dispatcher.Invoke(new Action(delegate ()
            {
                ConectoWorkSpace.Administrator.AdminPanels ConectoWorkSpace_InW = AppStart.LinkMainWindow("WAdminPanels");
                CurrentPuthDef = ConectoWorkSpace_InW.PuthSetBD25.Text ;
 
            }));

            // Удаление строки из файла
            string PuthAlias =  AppStart.TableReestr["ServerDefault25"] == "" ? Administrator.AdminPanels.SetPuth25 : AppStart.TableReestr["ServerDefault25"];
            int Idcount = 0;
            if (PuthAlias.Length == 0)
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

            if (System.IO.File.Exists(PuthAlias + "aliases.conf"))
            {
               string[] fileLines = System.IO.File.ReadAllLines(PuthAlias + "aliases.conf");
                foreach (string x in fileLines)
                {

                        if (x.Contains(CurrentPuthDef) == true  || x.Length == 0)
                        {
                            fileLines[Idcount] = String.Empty;
                        }
                        Idcount++;

                }
                System.IO.File.WriteAllLines(PuthAlias + "aliases.conf", fileLines);
            }

            string PuthBd = "",  NameServerBd = "";
            string StrCreate = "DELETE from CONNECTIONBD25  WHERE CONNECTIONBD25.PUTHBD =" + "'" + CurrentPuthDef + "'";
            DBConecto.DBopenFBConectionMemory(SystemBD.ConnnStringSystem().ToString(), "", "FbSystem");
            DBConecto.UniQuery DeletCONNECT = new DBConecto.UniQuery(StrCreate, "FB");
            DeletCONNECT.ExecuteUNIScalar();
            StrCreate = "SELECT count(*) from CONNECTIONBD25 ";
            DBConecto.UniQuery CountConect = new DBConecto.UniQuery(StrCreate, "FB");
            string CountTable = CountConect.ExecuteUNIScalar() == null ? "0" : CountConect.ExecuteUNIScalar().ToString();
            DBConecto.DBcloseFBConectionMemory("FbSystem");
            if (Convert.ToInt16(CountTable) > 0)
            {
                StrCreate = "SELECT first 1 * from CONNECTIONBD25 WHERE CONNECTIONBD25.NAMESERVER =" + "'" + AppStart.TableReestr["NameServer25"] + "'";
                DBConecto.DBopenFBConectionMemory(SystemBD.ConnnStringSystem().ToString(), "", "FbSystem");
                FbCommand FirstConect = new FbCommand(StrCreate, DBConecto.bdFbSystemConect);
                FirstConect.CommandType = CommandType.Text;
                FbDataReader ReadFirstConectTable = FirstConect.ExecuteReader();
                while (ReadFirstConectTable.Read())
                {
                    PuthBd = ReadFirstConectTable[0].ToString();
                    NameServerBd = ReadFirstConectTable[2].ToString();
                }
                ReadFirstConectTable.Close();
                FirstConect.Dispose();
                DBConecto.DBcloseFBConectionMemory("FbSystem");
                System.Windows.Application.Current.Dispatcher.Invoke(new Action(delegate ()
                {
                    ConectoWorkSpace.Administrator.AdminPanels ConectoWorkSpace_InW = AppStart.LinkMainWindow("WAdminPanels");
                    ConectoWorkSpace_InW.PuthSetBD25.Text = PuthBd;
                    ConectoWorkSpace_InW.DefNameServer25.Text = NameServerBd;
                    Administrator.AdminPanels.UpdateKeyReestr("PuthSetBD25", PuthBd);
                    Interface.CurrentStateInst("SetPuthBD25", "2", "on_off_2.png", ConectoWorkSpace_InW.SetPuthBD25);
                }));

            }
            else
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
            Administrator.AdminPanels.Grid_Puth("SELECT * from CONNECTIONBD25 WHERE CONNECTIONBD25.NAMESERVER =" + "'" + AppStart.TableReestr["NameServer25"] + "'");

 
    
        }

        #endregion

        #region Закладка "Установка  и Деинсталяция  сервера FireBird 3.0"

        // Установка сервера FireBird 3.0
        public static void InstallServFB3_0()
        {

            ConectoWorkSpace.Administrator.AdminPanels ConectoWorkSpace_InW = AppStart.LinkMainWindow("WAdminPanels");
            if (ConectoWorkSpace_InW == null)return;
            else
            {
                var TextWindows = "Подождите пожалуйста." + Environment.NewLine + "Устанавливается новый сервер ";
                int AutoClose = 1;
                int MesaggeTop = -170;
                int MessageLeft = 600;
                InstallB52.MessageEnd(TextWindows, AutoClose, MesaggeTop, MessageLeft);

                int IndCount = -1;
                DBConecto.DBopenFBConectionMemory(SystemBD.ConnnStringSystem().ToString(), "", "FbSystem");
                string StrCreate = "SELECT * from SERVERACTIVFB30";
                FbCommand SelectTable = new FbCommand(StrCreate, DBConecto.bdFbSystemConect);
                FbDataReader ReadOutTable = SelectTable.ExecuteReader();
                while (ReadOutTable.Read())
                {
                    IndCount++;
                    Administrator.AdminPanels.IdPort[IndCount] = ReadOutTable[0].ToString();
                    Administrator.AdminPanels.NamePuth[IndCount] = ReadOutTable[1].ToString();
                    Administrator.AdminPanels.NameServer[IndCount] = ReadOutTable[2].ToString();
                }
                ReadOutTable.Close();
                SelectTable.Dispose();
                DBConecto.DBcloseFBConectionMemory("FbSystem");
                if (IndCount >= 6)
                {
                    string TextWind = "Количество установленных серверов превышает 5" + Environment.NewLine + "Удалите ненужные записи из таблицы и повторите установку ";
                    int AutoCls = 1;
                    int MesaggeTp = 350;
                    int MessageLf = 650;
                    InstallB52.MessageErr(TextWind, AutoCls, MesaggeTp, MessageLf);
                }
                AppStart.RenderInfo Arguments01 = new AppStart.RenderInfo() { };
                Arguments01.argument1 = "3056";
                Arguments01.argument2 = SystemConectoServers.PutchServer + @"Firebird_3_0\";
                Arguments01.argument3 = "ConectoWS_3";
                Arguments01.argument4 = "0";
                if (IndCount >= 0)
                {
                    for (int i = 0; i <= IndCount; i++)
                    {
                        if (Administrator.AdminPanels.IdPort[i] == Arguments01.argument1)
                        {
                            Arguments01.argument1 = Convert.ToString(3056 + Convert.ToUInt32(i+1));
                            i = 0;
                        }
                    }
                    for (int i = 0; i <= IndCount; i++)
                    {
                        if (Administrator.AdminPanels.NameServer[i] == Arguments01.argument3)
                        {
                            Arguments01.argument3 = "ConectoWS_3" + Convert.ToString(i);
                            i = 0;
                        }

                    }
                    Arguments01.argument2 = SystemConectoServers.PutchServer + @"Firebird_3_0" + Convert.ToString(IndCount+1) + @"\";
                }
                Thread thStartTimer01 = new Thread(InstallB52.InstallServFB30);
                thStartTimer01.SetApartmentState(ApartmentState.STA);
                thStartTimer01.IsBackground = true; // Фоновый поток
                thStartTimer01.Start(Arguments01);
                InstallB52.IntThreadStart++;

            }
        }
        public static void UnInstallServFB3_0()
        {
 
            AppStart.RenderInfo Arguments01 = new AppStart.RenderInfo() { };
            Arguments01.argument1 = "UnInstall";
            Arguments01.argument2 = "";
            Thread thStartTimer01 = new Thread(UnInstallServFB30);
            thStartTimer01.SetApartmentState(ApartmentState.STA);
            thStartTimer01.IsBackground = true; // Фоновый поток
            thStartTimer01.Start(Arguments01);
            IntThreadStart++;


        }
        /// <summary>
        /// Установка сервера FB Firebird 3.0
        /// 
        /// </summary>
        public static void InstallServFB30(object ThreadObj)
        {

            AppStart.RenderInfo arguments = (AppStart.RenderInfo)ThreadObj;
            string IdPortServer = arguments.argument1;
            string PatchSR = arguments.argument2;
            string SetNameServer = arguments.argument3;
            string NumberServer = arguments.argument4;
            // Права в ОС Win
            if (App.aSystemVirable["UserWindowIdentity"] == "1")
            {

                System.Windows.Application.Current.Dispatcher.Invoke(new Action(delegate ()
                {
                    ConectoWorkSpace.Administrator.AdminPanels ConectoWorkSpace_Add = AppStart.LinkMainWindow("WAdminPanels");
                    ConectoWorkSpace_Add.AddSever30.Foreground = Brushes.Indigo;

                }));

                string NameDir = "Firebird_3_0"+ (Convert.ToInt32(NumberServer) == 0 ? "" : NumberServer); 
                string NameFile = "Firebird-3.0.3.32900-0_Win32.zip" ; //SystemConecto.OS64Bit == true ? "Firebird-3.0.3.32900-0_x64.zip" :
  
                // Проверка наличия свободного пространства на диске куда будем ложить 
                DriveInfo di = new DriveInfo(@"C:\");
                long Ffree = (di.TotalFreeSpace / 1024) / 1024;
                string MBFree = Ffree.ToString("#,##") + " MB";
                if (Ffree - 2500 < 0)
                {
                    var TextWindows = "Установка БД Firebird 3.0 требует 5Гб свободного пространства" + Environment.NewLine + "Текущее пространство " + MBFree + Environment.NewLine + "Освободите пространство на диске и повторите установку ";
                    int AutoClose = 1;
                    int MesaggeTop = 350;
                    int MessageLeft = 650;
                    MessageErr(TextWindows, AutoClose, MesaggeTop, MessageLeft);
                    if (Administrator.AdminPanels.SetRunInstal30 == "InstallServFB3_0")
                    {
                        System.Windows.Application.Current.Dispatcher.Invoke(new Action(delegate ()
                        {
                            ConectoWorkSpace.Administrator.AdminPanels ConectoWorkSpace_ERR = AppStart.LinkMainWindow("WAdminPanels");
                            Interface.CurrentStateInst("ServFB30OnOff", "0", "on_off_1.png", ConectoWorkSpace_ERR.ServFB30OnOff);
                        }));
                    }
                    return;
                }
                // Проверка каталогов серверов
                SystemConectoServers.DirServer();

                // Проверка установки утилит
                if (!SystemConecto.DIR_(PatchSR)) MessageBox.Show("Отсутствует папка " + PatchSR);
                if (!SystemConecto.DIR_(SystemConectoServers.PutchServer + @"tmp\BD")) MessageBox.Show("Отсутствует папка " + SystemConectoServers.PutchServer + @"tmp\BD");

                // Список файлов
                Dictionary<string, string> fbembedList = new Dictionary<string, string>();
                // Проверка наличия инсталяционного файла Firebird-3.0.3.32900-0_x64.zip
                fbembedList.Add(SystemConectoServers.PutchServer + @"tmp\BD\" + NameFile, "server_bd/");
                if (SystemConecto.IsFilesPRG(fbembedList, -1, "- Проверка файлов во время установки сервера " + NameDir) != "True")
                {
                    var TextWindows = "Отсутствует инсталяционный  файл установки сервера" + Environment.NewLine + NameFile + Environment.NewLine + "  Установка прекращена. ";
                    int AutoClose = 1;
                    int MesaggeTop = 350;
                    int MessageLeft = 650;
                    MessageErr(TextWindows, AutoClose, MesaggeTop, MessageLeft);
                    if (Administrator.AdminPanels.SetRunInstal30 == "InstallServFB3_0")
                    {
                        System.Windows.Application.Current.Dispatcher.Invoke(new Action(delegate ()
                        {
                            ConectoWorkSpace.Administrator.AdminPanels ConectoWorkSpace_ERR = AppStart.LinkMainWindow("WAdminPanels");
                            Interface.CurrentStateInst("ServFB30OnOff", "0", "on_off_1.png", ConectoWorkSpace_ERR.ServFB30OnOff);
                        }));
                    }
                    return;
                }

                // Распоковка
                Install.Extract.Unarch_arhive(SystemConectoServers.PutchServer + @"tmp\BD\" + NameFile, PatchSR);

                fbembedList.Add(PatchSR + "firebird.conf", "b52/server_config/");
                if (SystemConecto.IsFilesPRG(fbembedList, -1, "- Проверка файлов во время установки сервера " + "Windows") != "True")
                {
                    var TextWindows = "Отсутствует файл конфигурирования сервера firebird.conf" + Environment.NewLine + "  Установка прекращена. ";
                    int AutoClose = 1;
                    int MesaggeTop = 350;
                    int MessageLeft = 650;
                    MessageErr(TextWindows, AutoClose, MesaggeTop, MessageLeft);
                    return;
                }
                // настройка firebird.conf 3_0
                var GchangeConf30 = ConectoWorkSpace.INI.ReadFile(PatchSR + "firebird.conf");
                GchangeConf30.Set("RemoteServicePort", IdPortServer, true);
                GchangeConf30.Set("DefaultDbCachePages", "25000", true);
                GchangeConf30.Set("TempBlockSize", "2M", true);
                GchangeConf30.Set("TempCacheLimit", "364M", true);
                GchangeConf30.Set("LockMemSize", "9M", true);
                GchangeConf30.Set("LockHashSlots", "30011", true);
                GchangeConf30.Set("FileSystemCacheThreshold", "262144", true);
                GchangeConf30.Set("FileSystemCacheSize", "50", true);
                GchangeConf30.Set("GuardianOption", "1", true);
                GchangeConf30.Set("ServerMode", "Super", true);
                GchangeConf30.Set("AuthServer", "Legacy_Auth, Srp, Win_Sspi", true);
                GchangeConf30.Set("AuthClient", "Legacy_Auth, Srp, Win_Sspi", true);
                GchangeConf30.Set("UserManager", "Legacy_UserManager, Srp", true);
                GchangeConf30.Set("WireCompression", "true", true);
                GchangeConf30.Set("WireCrypt", "enabled", true);
                GchangeConf30.Set("TcpRemoteBufferSizet", "32767", true);
                GchangeConf30.Set("CpuAffinityMask", "3", true);

                // Обновляем security3.fdb 
                System.IO.File.Delete(PatchSR + "security3.fdb");
                fbembedList = new Dictionary<string, string>();
                fbembedList.Add(PatchSR + "security3.fdb", "b52/server_config/");
                if (SystemConecto.IsFilesPRG(fbembedList, -1, "- Проверка файлов во время установки сервера " + "Windows") != "True")
                {
                    var TextWindows = "Отсутствует файл  security3.fdb" + Environment.NewLine + "Установка прекращена ";
                    int AutoClose = 1;
                    int MesaggeTop = 350;
                    int MessageLeft = 650;
                    MessageEnd(TextWindows, AutoClose, MesaggeTop, MessageLeft);
                    System.Windows.Application.Current.Dispatcher.Invoke(new Action(delegate ()
                    {
                        ConectoWorkSpace.Administrator.AdminPanels ConectoWorkSpace_InW = AppStart.LinkMainWindow("WAdminPanels");
                        Interface.CurrentStateInst("ServFB30OnOff", "0", "on_off_1.png", ConectoWorkSpace_InW.ServFB30OnOff);
                    }));
                    return;
                }

                //Копируем в Папку UDF -режим совместимости с B52
                //BLOBC.dll.gz
                //LibPlus.dll.gz
                //LibPlus64.dll.gz
                // Копирование библиотек
                CopyDll(PatchSR);
                // Запуск Firebird
                string runfb30 = " install -z -n " + SetNameServer; //  ConectoWS_3
                // Запуск установки
                Process mv_prcInstaller = new Process();
                mv_prcInstaller.StartInfo.FileName = PatchSR + @"instsvc.exe";
                mv_prcInstaller.StartInfo.Arguments = runfb30; 
                mv_prcInstaller.StartInfo.UseShellExecute = false;
                mv_prcInstaller.StartInfo.CreateNoWindow = true;
                mv_prcInstaller.StartInfo.RedirectStandardOutput = true;
                mv_prcInstaller.Start();
                mv_prcInstaller.WaitForExit();
                mv_prcInstaller.Close();
                runfb30 = " start  -n " + SetNameServer;
                mv_prcInstaller.StartInfo.FileName = PatchSR + @"instsvc.exe";
                mv_prcInstaller.StartInfo.Arguments = runfb30; 
                mv_prcInstaller.StartInfo.UseShellExecute = false;
                mv_prcInstaller.StartInfo.CreateNoWindow = true;
                mv_prcInstaller.StartInfo.RedirectStandardOutput = true;
                mv_prcInstaller.Start();
                mv_prcInstaller.WaitForExit();
                mv_prcInstaller.Close();


                int Idcount = 0;
                DBConecto.DBopenFBConectionMemory(SystemBD.ConnnStringSystem().ToString(), "", "FbSystem");
                string StrCreate = "select * from SERVERACTIVFB30 where SERVERACTIVFB30.PUTH = " + "'" + PatchSR + "'";
                FbCommand InsertTable = new FbCommand(StrCreate, DBConecto.bdFbSystemConect);
                InsertTable.CommandType = CommandType.Text;
                FbDataReader ReadTable = InsertTable.ExecuteReader();
                while (ReadTable.Read())
                {
                    IdPortServer = ReadTable[0].ToString();
                    Idcount++;
                }
                ReadTable.Close();
                if (Idcount == 0)

                {

                    string DateCreatFB = (string)AppStart.rkAppSetingAllUser.GetValue("StartSystemFB");
                    StrCreate = "INSERT INTO SERVERACTIVFB30  values (" + IdPortServer + ",'" + PatchSR + "', '" + SetNameServer + "', '" + DateCreatFB + "','Activ','alarm')";
                    DBConecto.UniQuery InsertQuery = new DBConecto.UniQuery(StrCreate, "FB");
                    InsertQuery.UserQuery = string.Format(StrCreate, "SERVERACTIVFB30");
                    InsertQuery.ExecuteUNIScalar();

                }
                InsertTable.Dispose();
                DBConecto.DBcloseFBConectionMemory("FbSystem");
                Administrator.AdminPanels.SetServerGrid("SELECT * from SERVERACTIVFB30");
 
                // Завершение инсталяции
                System.Windows.Application.Current.Dispatcher.Invoke(new Action(delegate ()
                {
                    ConectoWorkSpace.Administrator.AdminPanels ConectoWorkSpace_InW = AppStart.LinkMainWindow("WAdminPanels");
                    ConectoWorkSpace_InW.PuthSetServer30.Text = PatchSR;
                    Interface.CurrentStateInst("ServFB30OnOff", "2", "on_off_2.png", ConectoWorkSpace_InW.ServFB30OnOff);
                    ConectoWorkSpace_InW.SetPuthBD30.IsEnabled = true;
                    ConectoWorkSpace_InW.BackUpLocServerBD30.IsEnabled = true;
                    ConectoWorkSpace_InW.AddSever30.Foreground = Brushes.White;
                    if (AppStart.TableReestr["NameServer30"] == "" || AppStart.TableReestr["ServerDefault30"] == "")
                    {
                        ConectoWorkSpace_InW.DefNameServer30.Text = SetNameServer;
                        Administrator.AdminPanels.UpdateKeyReestr("ServerDefault30", PatchSR);
                        Administrator.AdminPanels.UpdateKeyReestr("NameServer30", SetNameServer);
                    }


                }));
     
                var TextWin = "Установка сервера Firebird 3.0" + Environment.NewLine + "завершена";
                int AutoCl = 1;
                int MesaggeT = -170;
                int MessageL = 600;
                MessageEnd(TextWin, AutoCl, MesaggeT, MessageL);

            }
            IntThreadStart--;

        }

        /// <summary>
        /// Деинсталяция  сервера FireBird 3.0
        /// 
        /// </summary>
        public static void UnInstallServFB30(object ThreadObj)
        {

            AppStart.RenderInfo Arguments = (AppStart.RenderInfo)ThreadObj;

            // Права в ОС Win
            if (App.aSystemVirable["UserWindowIdentity"] == "1")
            {
                int DeletSerever = 0;
                Administrator.AdminPanels.Inst2530 = "30";
                string StrCreate = "";
                string NameServer = Arguments.argument1 == "Delete" ? Administrator.AdminPanels.SetName30 : AppStart.TableReestr["NameServer30"];
                string PatchSR = Arguments.argument1 == "Delete" ? Administrator.AdminPanels.SetPuth30 : AppStart.TableReestr["ServerDefault30"];


                   if (SystemConecto.File_(PatchSR + @"instsvc.exe", 5) && Administrator.AdminPanels.SetActiv30 != "Stop")
                   {
                        DBConecto.DBopenFBConectionMemory(SystemBD.ConnnStringSystem().ToString(), "", "FbSystem");
                        StrCreate = "SELECT * from REESTRBACK where REESTRBACK.SERVER = " + "'" + Administrator.AdminPanels.SetName30 + "'";
                        string TempNameBack = "";
                        int Idcount = 0;
                        DBConecto.DBopenFBConectionMemory(SystemBD.ConnnStringSystem().ToString(), "", "FbSystem");
                        FbCommand SelectTable = new FbCommand(StrCreate, DBConecto.bdFbSystemConect);
                        FbDataReader ReadOutTable = SelectTable.ExecuteReader();
                        while (ReadOutTable.Read())
                        {
                            TempNameBack = ReadOutTable[0].ToString();
                            Idcount = Idcount + 1;
                        }
                        ReadOutTable.Close();
                        if (Idcount != 0)
                        {
                            var TextWindows = "Удаление недопустимо. Установлен БекОфис";
                            int AutoClose = 1;
                            int MesaggeTop = -170;
                            int MessageLeft = 300;
                            MessageEnd(TextWindows, AutoClose, MesaggeTop, MessageLeft);
                            System.Windows.Application.Current.Dispatcher.Invoke(new Action(delegate ()
                            {
                                ConectoWorkSpace.Administrator.AdminPanels ConectoWorkSpace_Del = AppStart.LinkMainWindow("WAdminPanels");
                                ConectoWorkSpace_Del.DeletSrever25.Foreground = Brushes.White;

                            }));

                            return;
                        }
                        DBConecto.DBcloseFBConectionMemory("FbSystem");
                        DBConecto.DBopenFBConectionMemory(SystemBD.ConnnStringSystem().ToString(), "", "FbSystem");
                        StrCreate = "SELECT * from REESTRFRONT where REESTRFRONT.SERVER = " + "'" + Administrator.AdminPanels.SetName30 + "'";
                        Idcount = 0;
                        DBConecto.DBopenFBConectionMemory(SystemBD.ConnnStringSystem().ToString(), "", "FbSystem");
                        FbCommand SelectFront = new FbCommand(StrCreate, DBConecto.bdFbSystemConect);
                        FbDataReader ReadOutFront = SelectFront.ExecuteReader();
                        while (ReadOutFront.Read())
                        {
                            TempNameBack = ReadOutFront[0].ToString();
                            Idcount = Idcount + 1;
                        }
                        ReadOutFront.Close();
                        if (Idcount != 0)
                        {
                            var TextWindows = "Удаление недопустимо. Установлен ФронтОфис";
                            int AutoClose = 1;
                            int MesaggeTop = -170;
                            int MessageLeft = 300;
                            MessageEnd(TextWindows, AutoClose, MesaggeTop, MessageLeft);
                            System.Windows.Application.Current.Dispatcher.Invoke(new Action(delegate ()
                            {
                                ConectoWorkSpace.Administrator.AdminPanels ConectoWorkSpace_Del = AppStart.LinkMainWindow("WAdminPanels");
                                ConectoWorkSpace_Del.DeletSrever25.Foreground = Brushes.White;
                            }));
                            return;
                        }
                        DBConecto.DBcloseFBConectionMemory("FbSystem");
                        Administrator.AdminPanels.ImageObj = "RemoveServerFB30";
                        if (InstallB52.RemoveServerFB25(PatchSR, NameServer) > 0) DeletSerever = 1;

                   }
      
                if (DeletSerever == 1)
                {

                    DBConecto.DBopenFBConectionMemory(SystemBD.ConnnStringSystem().ToString(), "", "FbSystem");
                    StrCreate = PatchSR == "" ? "DELETE from SERVERACTIVFB30" : "DELETE from SERVERACTIVFB30 where SERVERACTIVFB30.PUTH = " + "'" + PatchSR + "'";
                    DBConecto.UniQuery DeletQuery = new DBConecto.UniQuery(StrCreate, "FB");
                    DeletQuery.ExecuteUNIScalar();
                    string StrCount = "SELECT count(*) from SERVERACTIVFB30";
                    DBConecto.UniQuery CountQuery = new DBConecto.UniQuery(StrCount, "FB");
                    string CountTableSERVER = CountQuery.ExecuteUNIScalar() == null ? "" : CountQuery.ExecuteUNIScalar().ToString();

                    StrCreate = "DELETE from CONNECTIONBD30  WHERE CONNECTIONBD30.NAMESERVER =" + "'" + NameServer + "'";
                    DBConecto.DBopenFBConectionMemory(SystemBD.ConnnStringSystem().ToString(), "", "FbSystem");
                    DBConecto.UniQuery DeletCONNECT = new DBConecto.UniQuery(StrCreate, "FB");
                    DeletCONNECT.ExecuteUNIScalar();

                    if (Administrator.AdminPanels.SetActiv30 == "Stop")
                    {
                        string PatchNoactiv = PatchSR.Substring(0, PatchSR.Length - 1);
                        Directory.Delete(PatchNoactiv, true); //
                        Administrator.AdminPanels.SetServerGrid("SELECT * from SERVERACTIVFB30");
                        System.Windows.Application.Current.Dispatcher.Invoke(new Action(delegate ()
                        {
                            ConectoWorkSpace.Administrator.AdminPanels ConectoWorkSpace_Del = AppStart.LinkMainWindow("WAdminPanels");
                            ConectoWorkSpace_Del.DeletSrever30.Foreground = Brushes.White;
                        }));
                        return;
                    }


                    if (Convert.ToUInt32(CountTableSERVER) >= 1 || PatchSR == AppStart.TableReestr["ServerDefault30"])
                    {
                        StrCreate = "SELECT first 1 * from SERVERACTIVFB30";
                        DBConecto.DBopenFBConectionMemory(SystemBD.ConnnStringSystem().ToString(), "", "FbSystem");
                        FbCommand FirstKey = new FbCommand(StrCreate, DBConecto.bdFbSystemConect);
                        FirstKey.CommandType = CommandType.Text;
                        FbDataReader ReadOutTable = FirstKey.ExecuteReader();
                        while (ReadOutTable.Read())
                        {
                            Administrator.AdminPanels.SetPort30 = ReadOutTable[0].ToString();
                            Administrator.AdminPanels.SetPuth30 = ReadOutTable[1].ToString();
                            Administrator.AdminPanels.SetName30 = ReadOutTable[2].ToString();
                        }
                        ReadOutTable.Close();
                        FirstKey.Dispose();

                        if (Administrator.AdminPanels.SetActiv30 != "Stop")
                        {
                           Administrator.AdminPanels.UpdateKeyReestr("ServerDefault30", Administrator.AdminPanels.SetPuth30);
                            Administrator.AdminPanels.UpdateKeyReestr("NameServer30", Administrator.AdminPanels.SetName30);
                            Administrator.AdminPanels.UpdateKeyReestr("ServFB30OnOff", "2");
                            System.Windows.Application.Current.Dispatcher.Invoke(new Action(delegate ()
                            {
                                ConectoWorkSpace.Administrator.AdminPanels ConectoWorkSpace_Puth = AppStart.LinkMainWindow("WAdminPanels");
                                Interface.CurrentStateInst("ServFB30OnOff", "2", "on_off_2.png", ConectoWorkSpace_Puth.ServFB30OnOff);
                                ConectoWorkSpace_Puth.PuthSetServer30.Text = Administrator.AdminPanels.SetPuth30;
                            }));

                            string PuthBd = "", AliasBd = "", NameServerBd = "";
                            StrCreate = "SELECT first 1 * from CONNECTIONBD30 WHERE CONNECTIONBD30.NAMESERVER =" + "'" + Administrator.AdminPanels.SetName30 + "'";
                            DBConecto.DBopenFBConectionMemory(SystemBD.ConnnStringSystem().ToString(), "", "FbSystem");
                            FbCommand FirstConect = new FbCommand(StrCreate, DBConecto.bdFbSystemConect);
                            FirstConect.CommandType = CommandType.Text;
                            FbDataReader ReadFirstConectTable = FirstConect.ExecuteReader();
                            while (ReadFirstConectTable.Read())
                            {
                                PuthBd = ReadFirstConectTable[0].ToString();
                                AliasBd = ReadFirstConectTable[1].ToString();
                                NameServerBd = ReadFirstConectTable[2].ToString();
                            }
                            ReadFirstConectTable.Close();
                            FirstConect.Dispose();
                            DBConecto.DBcloseFBConectionMemory("FbSystem");
                            System.Windows.Application.Current.Dispatcher.Invoke(new Action(delegate ()
                            {
                                ConectoWorkSpace.Administrator.AdminPanels ConectoWorkSpace_InW = AppStart.LinkMainWindow("WAdminPanels");
                                ConectoWorkSpace_InW.PuthSetBD30.Text = PuthBd;
                                Administrator.AdminPanels.UpdateKeyReestr("PuthSetBD30", PuthBd);
                                Interface.CurrentStateInst("SetPuthBD30", "2", "on_off_2.png", ConectoWorkSpace_InW.SetPuthBD30);
                            }));
                        }
                        else
                        {
                            Administrator.AdminPanels.UpdateKeyReestr("ServerDefault30", "");
                            Administrator.AdminPanels.UpdateKeyReestr("NameServer30", "");
                            Administrator.AdminPanels.UpdateKeyReestr("ServFB30OnOff", "0");
                            System.Windows.Application.Current.Dispatcher.Invoke(new Action(delegate ()
                            {
                                ConectoWorkSpace.Administrator.AdminPanels ConectoWorkSpace_Puth = AppStart.LinkMainWindow("WAdminPanels");
                                Interface.CurrentStateInst("ServFB30OnOff", "0", "on_off_1.png", ConectoWorkSpace_Puth.ServFB30OnOff);
                                ConectoWorkSpace_Puth.PuthSetServer30.Text = "";
                            }));
                        }
                        Administrator.AdminPanels.Grid_Puth("SELECT * from CONNECTIONBD30 WHERE CONNECTIONBD30.NAMESERVER =" + "'" + Administrator.AdminPanels.SetName30 + "'");
                    }

                
                    if (Convert.ToUInt32(CountTableSERVER) == 0)
                    {
                        Administrator.AdminPanels.UpdateKeyReestr("ServerDefault30", "");
                        Administrator.AdminPanels.UpdateKeyReestr("NameServer30", "");
                        Administrator.AdminPanels.UpdateKeyReestr("BackFbd30OnOff", "0");
                        Administrator.AdminPanels.UpdateKeyReestr("FrontFbd30OnOff", "0");
                        Administrator.AdminPanels.UpdateKeyReestr("InstBackOnOff", "0");
                        Administrator.AdminPanels.UpdateKeyReestr("ServFB30OnOff", "0");
                        System.Windows.Application.Current.Dispatcher.Invoke(new Action(delegate ()
                        {
                            ConectoWorkSpace.Administrator.AdminPanels ConectoWorkSpace_Puth = AppStart.LinkMainWindow("WAdminPanels");
                            Interface.CurrentStateInst("ServFB30OnOff", "0", "on_off_1.png", ConectoWorkSpace_Puth.ServFB30OnOff);
                            ConectoWorkSpace_Puth.PuthSetServer30.Text = "";
                            ConectoWorkSpace_Puth.DeletSrever30.Foreground = Brushes.White;
                            ConectoWorkSpace_Puth.PuthSetBD30.Text = "";
                            Interface.CurrentStateInst("SetPuthBD30", "0", "on_off_1.png", ConectoWorkSpace_Puth.SetPuthBD30);
                        }));
                        Administrator.AdminPanels.Grid_Puth("SELECT * from CONNECTIONBD30");
                    }
                    // Удаление файлов
                    string PatchDelete = PatchSR.Substring(0, PatchSR.Length - 1);
                    Directory.Delete(PatchDelete, true); //

                    Administrator.AdminPanels.SetServerGrid("SELECT * from SERVERACTIVFB30");
  
                }
 

            }
        }
        // Установка БД для  FireBird 3.0

        public static void InstallSetPuthBD30()
        {
            if (AppStart.TableReestr["ServFB30OnOff"] != "2")
            {
                var TextWin = "Connection c БД невозможно" + Environment.NewLine + "Установите Firebird_3_0";
                int AutoCl = 1;
                int MesaggeT = 100;
                int MessageL = 850;
                MessageEnd(TextWin, AutoCl, MesaggeT, MessageL);
                return;
            }

            SetPuthBD30OnOff3();
            InstallSetBD30();
            if (Administrator.AdminPanels.ProcesEnd == 2) RunCreatBD30();
        }

        public static void SetPuthBD30OnOff3()
        {
            System.Windows.Application.Current.Dispatcher.Invoke(new Action(delegate ()
            {
                Administrator.AdminPanels.NameObj = "SetPuthBD30";
                ConectoWorkSpace.Administrator.AdminPanels ConectoWorkSpace_InW = AppStart.LinkMainWindow("WAdminPanels");
                var pict = (Image)LogicalTreeHelper.FindLogicalNode(ConectoWorkSpace_InW, Administrator.AdminPanels.NameObj);
                var uriSource = new Uri(@"/Conecto®%20WorkSpace;component/Images/on_off_3.png", UriKind.Relative);
                pict.Source = new BitmapImage(uriSource);
            }));
        }


        public static void InstallSetBD30() //object ThreadObj
        {

            // Права в ОС Win
            if (App.aSystemVirable["UserWindowIdentity"] == "1")
            {

  
                string PatchSRBD = AppStart.TableReestr["PuthSetBD30"] == "" ? SystemConectoServers.PutchServerData + "hub3.fdb" : AppStart.TableReestr["PuthSetBD30"];
                string PatchFoliderBD = AppStart.TableReestr["PuthSetBD30"] == "" ? SystemConectoServers.PutchServerData : AppStart.TableReestr["PuthSetBD30"].Substring(1, AppStart.TableReestr["PuthSetBD30"].LastIndexOf(@"\"));
                string NameFileBD = "hub.fbk";
                string PatchSR = AppStart.TableReestr["ServerDefault30"];

                // Проверка наличия свободного пространства на диске куда будем ложить 
                DriveInfo di = new DriveInfo(@"C:\");
                long Ffree = (di.TotalFreeSpace / 1024) / 1024;
                string MBFree = Ffree.ToString("#,##") + " MB";
                if (Ffree - 2500 < 0)
                {
                    var TextWindows = "Установка БД Firebird 3.0 требует 5Гб свободного пространства" + Environment.NewLine + "Текущее пространство " + MBFree + Environment.NewLine + "Освободите пространство на диске и повторите установку ";
                    int AutoClose = 1;
                    int MesaggeTop = 350;
                    int MessageLeft = 650;
                    MessageErr(TextWindows, AutoClose, MesaggeTop, MessageLeft);
                    System.Windows.Application.Current.Dispatcher.Invoke(new Action(delegate ()
                    {
                        ConectoWorkSpace.Administrator.AdminPanels ConectoWorkSpace_InW = AppStart.LinkMainWindow("WAdminPanels");
                        ConectoWorkSpace_InW.PuthSetBD30.Text = "";
                        Interface.CurrentStateInst("SetPuthBD30", "0", "on_off_1.png", ConectoWorkSpace_InW.SetPuthBD30);
                    }));
                    Administrator.AdminPanels.ProcesEnd = 3;
                    return;
                }
                // перезапускаем Firebird 3.0.
                if (!SystemConecto.File_(PatchSR + "instsvc.exe", 5))
                {
                    var TextWindows = "Отсутствует файл instsvc.exe." + Environment.NewLine + "Установка прекращена ";
                    int AutoClose = 1;
                    int MesaggeTop = 3;
                    int MessageLeft = 650;
                    MessageEnd(TextWindows, AutoClose, MesaggeTop, MessageLeft);
                    System.Windows.Application.Current.Dispatcher.Invoke(new Action(delegate ()
                    {
                        ConectoWorkSpace.Administrator.AdminPanels ConectoWorkSpace_InW = AppStart.LinkMainWindow("WAdminPanels");
                        ConectoWorkSpace_InW.PuthSetBD30.Text = "";
                        Interface.CurrentStateInst("SetPuthBD30", "0", "on_off_1.png", ConectoWorkSpace_InW.SetPuthBD30);
                    }));
                    Administrator.AdminPanels.ProcesEnd = 3;
                    return;
                }

                Dictionary<string, string> fbembedList = new Dictionary<string, string>();
                // запись БД в папку Servers\data\
                if (!SystemConecto.DIR_(@"c:\temp\")) MessageBox.Show(@"Отсутствуют папкa c:\temp\");
                fbembedList.Add(@"c:\temp\" + NameFileBD, "b52/base/");
                if (SystemConecto.IsFilesPRG(fbembedList, -1, "- Проверка файлов во время установки БД " + PatchSRBD) != "True")
                {
                    var TextWindows = "Отсутствует  файл БД hub.fbk" + Environment.NewLine + "Установка прекращена ";
                    int AutoClose = 1;
                    int MesaggeTop = 3;
                    int MessageLeft = 650;
                    MessageEnd(TextWindows, AutoClose, MesaggeTop, MessageLeft);
                    System.Windows.Application.Current.Dispatcher.Invoke(new Action(delegate ()
                    {
                        ConectoWorkSpace.Administrator.AdminPanels ConectoWorkSpace_InW = AppStart.LinkMainWindow("WAdminPanels");
                        ConectoWorkSpace_InW.PuthSetBD30.Text = "";
                        Interface.CurrentStateInst("SetPuthBD30", "0", "on_off_1.png", ConectoWorkSpace_InW.SetPuthBD30);
                    }));
                    Administrator.AdminPanels.ProcesEnd = 3;
                    return;
                }

               // перезапись архива БД с фтп серевера на диск c:\temp и распаковка
                if (!SystemConecto.File_(PatchSR + @"gbak.exe", 5))
                {
                    var TextWindows = "Отсутствует файл gbak.exe. Установка БД прекращена. " + Environment.NewLine + "Необходимо проверить наличие утилиты распаковщика БД";
                    int AutoClose = 1;
                    int MesaggeTop = 350;
                    int MessageLeft = 650;
                    MessageErr(TextWindows, AutoClose, MesaggeTop, MessageLeft);
                    System.Windows.Application.Current.Dispatcher.Invoke(new Action(delegate ()
                    {
                        ConectoWorkSpace.Administrator.AdminPanels ConectoWorkSpace_InW = AppStart.LinkMainWindow("WAdminPanels");
                        ConectoWorkSpace_InW.PuthSetBD30.Text = "";
                        Interface.CurrentStateInst("SetPuthBD30", "0", "on_off_1.png", ConectoWorkSpace_InW.SetPuthBD30);
                    }));
                    Administrator.AdminPanels.ProcesEnd = 3;
                    return;

                }

                Administrator.AdminPanels.IndRecno = 0;
                Administrator.AdminPanels.LoadAlias30(AppStart.TableReestr["NameServer30"], AppStart.TableReestr["ServerDefault30"]);

                if (Administrator.AdminPanels.IndRecno == 0)
                {
                    System.IO.File.Delete(PatchSR + "databases.conf");
                    var Change = ConectoWorkSpace.INI.ReadFile(PatchSR + "databases.conf");
                    Change.Set(Administrator.AdminPanels.SelectAlias, PatchSRBD, true);

                    var GchangeConf30 = ConectoWorkSpace.INI.ReadFile(PatchSR + "firebird.conf");
                    GchangeConf30.Set("RemoteServicePort", "3056", true);
                    GchangeConf30.Set("DefaultDbCachePages", "25000", true);
                    GchangeConf30.Set("TempBlockSize", "2M", true);
                    GchangeConf30.Set("TempCacheLimit", "364M", true);
                    GchangeConf30.Set("LockMemSize", "9M", true);
                    GchangeConf30.Set("LockHashSlots", "30011", true);
                    GchangeConf30.Set("FileSystemCacheThreshold", "262144", true);
                    GchangeConf30.Set("FileSystemCacheSize", "50", true);
                    GchangeConf30.Set("GuardianOption", "1", true);
                    GchangeConf30.Set("ServerMode", "Super", true);
                    GchangeConf30.Set("AuthServer", "Legacy_Auth, Srp, Win_Sspi", true);
                    GchangeConf30.Set("AuthClient", "Legacy_Auth, Srp, Win_Sspi", true);
                    GchangeConf30.Set("UserManager", "Legacy_UserManager, Srp", true);
                    GchangeConf30.Set("WireCompression", "true", true);
                    GchangeConf30.Set("WireCrypt", "enabled", true);
                    GchangeConf30.Set("TcpRemoteBufferSizet", "32767", true);
                    GchangeConf30.Set("CpuAffinityMask", "3", true);
 
                }
                    // Перезапуск FireBird 
                    DBConecto.DBopenFBConectionMemory(SystemBD.ConnnStringSystem().ToString(), "", "FbSystem");
                    string StrCreate = "select * from SERVERACTIVFB30 where SERVERACTIVFB30.PUTH = " + "'" + AppStart.TableReestr["ServerDefault30"] + "'";
                    FbCommand SelectTable = new FbCommand(StrCreate, DBConecto.bdFbSystemConect);
                    SelectTable.CommandType = CommandType.Text;
                    FbDataReader ReadOutTable = SelectTable.ExecuteReader();
                    while (ReadOutTable.Read())
                    {
                        Administrator.AdminPanels.SetName30 = ReadOutTable[2].ToString();
                    }
                    ReadOutTable.Close();
                    SelectTable.Dispose();
                    DBConecto.DBcloseFBConectionMemory("FbSystem");
                string OldImageObj = Administrator.AdminPanels.ImageObj;
                Administrator.AdminPanels.ImageObj = "RestartServFB30";
                    RestartFB25(AppStart.TableReestr["ServerDefault30"], Administrator.AdminPanels.SetName30);
                    Administrator.AdminPanels.ProcesEnd = 2;
                Administrator.AdminPanels.ImageObj = OldImageObj;
                string PatchHub = SystemConectoServers.PutchServerData + @"hub3.fdb";
                if (Administrator.AdminPanels.NameObj == "NewServerSetPuthBD30") PatchHub = ConectoWorkSpace.Administrator.AdminPanels.PathFileBDText;

                if (!System.IO.File.Exists(PatchHub))
                {

                    System.Windows.Application.Current.Dispatcher.Invoke(new Action(delegate ()
                    {
                        double length = (System.IO.File.ReadAllBytes(@"c:\temp\hub.fbk").Length / 1024 / 1024) / 2.0;
                        Administrator.AdminPanels.TimeAutoCloseWin = (int)length;
                        MainWindow ConectoWorkSpace_InW = AppStart.LinkMainWindow();
                        WaitMessage WaitWindow = new WaitMessage();
                        WaitWindow.Owner = ConectoWorkSpace_InW;
                        WaitWindow.Top = (ConectoWorkSpace_InW.Top + 7) + 20; //+ this.Close_F.Margin.Top + (this.Close_F.Height - 2)
                        WaitWindow.Left = (ConectoWorkSpace_InW.Left) + 900; //- (WaitWindow.Width - 22)+ this.Close_F.Margin.Left 
                        WaitWindow.Show();
                        
                    }));

 
                }
 
            }
            IntThreadStart--;

  
        }

        public static void RunCreatBD30()
        {
            AppStart.RenderInfo Arguments01 = new AppStart.RenderInfo() { };
            Arguments01.argument1 = "2";
            Arguments01.argument2 = "";
            Thread thStartTimer01 = new Thread(CreatBD30TH);
            thStartTimer01.SetApartmentState(ApartmentState.STA);
            thStartTimer01.IsBackground = true; // Фоновый поток
            thStartTimer01.Start(Arguments01);
            IntThreadStart++;
        }

        public static void CreatBD30TH(object ThreadObj)
        {


            string PatchSRBD = AppStart.TableReestr["PuthSetBD30"] == "" ? SystemConectoServers.PutchServerData + "hub3.fdb" : AppStart.TableReestr["PuthSetBD30"];
            string PatchFoliderBD = AppStart.TableReestr["PuthSetBD30"] == "" ? SystemConectoServers.PutchServerData : AppStart.TableReestr["PuthSetBD30"].Substring(0, AppStart.TableReestr["PuthSetBD30"].LastIndexOf(@"\"));
            string hubdate = AppStart.TableReestr["SetTextIp4BackOf"] + "/" + AppStart.TableReestr["SetPortServer30"] + ":" + Administrator.AdminPanels.SelectAlias;
            if (!SystemConecto.File_(PatchSRBD, 5))
            {

                string RunGbak = @"C:\Program Files\Conecto\Servers\Firebird_3_0\gbak.exe";
                string ArgumentCmd = @"-c c:\temp\hub.fbk " + hubdate + " -v  -user sysdba -pass " + Administrator.AdminPanels.CurrentPasswFb30; // + @"-y c:\temp\log.txt";

                SystemConecto.DIR_(PatchFoliderBD);
                Process CmdDos = new Process();
                CmdDos.StartInfo.FileName = RunGbak; //@"C:\Program Files\Conecto\Servers\Firebird_3_0\gbak.exe";   // PatchSR + @"bin\gbak.exe";-y c:\temp\log.txt
                CmdDos.StartInfo.Arguments = ArgumentCmd; // @"-c c:\temp\hub.fbk 127.0.0.1/3053:hubfb   -user SYSDBA -pass alarm";
                CmdDos.StartInfo.UseShellExecute = false;
                //CmdDos.StartInfo.CreateNoWindow = true;
                //CmdDos.StartInfo.RedirectStandardOutput = true;
                CmdDos.Start();
                CmdDos.WaitForExit();
                CmdDos.Close();

                System.Windows.Application.Current.Dispatcher.Invoke(new Action(delegate ()
                {

                    ConectoWorkSpace.WaitMessage ConectoWorkSpace_Off = AppStart.LinkMainWindow("WaitMessage_");
                    ConectoWorkSpace_Off.Close();

                }));

                System.Windows.Application.Current.Dispatcher.Invoke(new Action(delegate ()
                {
                    ConectoWorkSpace.Administrator.AdminPanels ConectoWorkSpace_InW = AppStart.LinkMainWindow("WAdminPanels");
                    ConectoWorkSpace_InW.TablPuthBD30.IsEnabled = true;
                }));

            }


            if (SystemConecto.File_(PatchSRBD, 5))
            {

 

                // процедура завершена успешно
                Administrator.AdminPanels.UpdateKeyReestr("PuthSetBD30", PatchSRBD);
                Administrator.AdminPanels.UpdateKeyReestr("BackOfAdresPorta", "3182");
                Administrator.AdminPanels.UpdateKeyReestr("AdresPortFront", "3182");
                System.Windows.Application.Current.Dispatcher.Invoke(new Action(delegate ()
                {
                    ConectoWorkSpace.Administrator.AdminPanels ConectoWorkSpace_Puth = AppStart.LinkMainWindow("WAdminPanels");
                    ConectoWorkSpace_Puth.PuthSetBD30.Text = PatchSRBD;
                    ConectoWorkSpace_Puth.DefNameServer30.Text = AppStart.TableReestr["NameServer30"];
                    ConectoWorkSpace_Puth.DirSetBD30.IsEnabled = true;
                    Interface.CurrentStateInst("SetPuthBD30", "2", "on_off_2.png", ConectoWorkSpace_Puth.SetPuthBD30);
                }));
                var TextWin = "Присоединение БД к серверу " + Environment.NewLine + "успешно завершено. ";
                int AutoCl = 1;
                int MesaggeT = -170;
                int MessageL = 600;
                MessageEnd(TextWin, AutoCl, MesaggeT, MessageL);
                ConectoWorkSpace.Administrator.AdminPanels.InsertConect30(PatchSRBD, AppStart.TableReestr["NameServer30"]);
                Administrator.AdminPanels.LoadAlias30(AppStart.TableReestr["NameServer30"], AppStart.TableReestr["ServerDefault30"]);
            }
            else
            {
                var TextWin = "Присоединение БД к серверу " + Environment.NewLine + "не выполнено. ";
                int AutoCl = 1;
                int MesaggeT = -170;
                int MessageL = 600;
                MessageEnd(TextWin, AutoCl, MesaggeT, MessageL);
                System.Windows.Application.Current.Dispatcher.Invoke(new Action(delegate ()
                {
                    ConectoWorkSpace.Administrator.AdminPanels ConectoWorkSpace_Puth = AppStart.LinkMainWindow("WAdminPanels");
                    Interface.CurrentStateInst("SetPuthBD30", "1", "on_off_1.png", ConectoWorkSpace_Puth.SetPuthBD30);
                }));
            }
            IntThreadStart--;

        }
        // Удаление  БД для  FireBird 3.0
        public static void UnInstallSetPuthBD3_0()
        {
            ConectoWorkSpace.Administrator.AdminPanels ConectoWorkSpace_InW = AppStart.LinkMainWindow("WAdminPanels");
            if (ConectoWorkSpace_InW == null)
            {
                return;
            }
            else
            {
                Administrator.AdminPanels.UpdateKeyReestr("PuthSetBD30", "");
                System.Windows.Application.Current.Dispatcher.Invoke(new Action(delegate ()
                {
                    ConectoWorkSpace.Administrator.AdminPanels ConectoWorkSpace_Puth = AppStart.LinkMainWindow("WAdminPanels");
                    ConectoWorkSpace_Puth.PuthSetBD30.Text = "";
                    ConectoWorkSpace_Puth.DefNameServer30.Text = "";
                    Interface.CurrentStateInst("SetPuthBD30", "0", "on_off_1.png", ConectoWorkSpace_Puth.SetPuthBD30);
                }));

            }
        }


        #endregion

        #region Закладка "Установка  и Деинсталяция  сервера Postgresq"

        /// <summary>
        /// Установка сервера Postgresq
        /// </summary>
        public static void InstallServPostgresql()
        {

            ConectoWorkSpace.WinOblakoSetUpdate ConectoWorkSpace_Up = AppStart.LinkMainWindow("WinOblakoSetUpdateW");
            if (ConectoWorkSpace_Up != null)
            { 
                System.Windows.Application.Current.Dispatcher.Invoke(new Action(delegate ()
                {
                    ConectoWorkSpace.WinOblakoSetUpdate ConectoWorkSpace_Off = AppStart.LinkMainWindow("WinOblakoSetUpdateW");
                    ConectoWorkSpace_Off.Close();
                }));
            }

  

            ConectoWorkSpace.Administrator.AdminPanels ConectoWorkSpace_InW = AppStart.LinkMainWindow("WAdminPanels");
            if (ConectoWorkSpace_InW == null)return;
            else
            {
               System.Windows.Application.Current.Dispatcher.Invoke(new Action(delegate ()
               {
                    int AutoCl = 1;               
                    var TextWin = "Путь инсталяции скопируйте из буфера обмена ." + Environment.NewLine + @"c:\Program Files\Conecto\Servers\Postgresql\ "+
                    Environment.NewLine + "Пароль : pgsql"+
                    Environment.NewLine + "Отключить приложение - Stack Builder"+
                    Environment.NewLine + "Все остальные параметры по умолчанию."+
                    Environment.NewLine + "Пожулайста подождите выполняется."+
                   Environment.NewLine + "загрузка инсталятора.";
                   Window WinOblakoVerh_Info = new WinMessage(TextWin, AutoCl, 0); 
                    WinOblakoVerh_Info.Owner = ConectoWorkSpace_InW;
                    WinOblakoVerh_Info.Top = ConectoWorkSpace_InW.Top +100 ;
                    WinOblakoVerh_Info.Left = ConectoWorkSpace_InW.ScrollViewerd.Width - (WinOblakoVerh_Info.Width * 2); 
                    WinOblakoVerh_Info.ShowActivated = false;
                    WinOblakoVerh_Info.Show();

               }));
                
                // Проверка наличия свободного пространства на диске куда будем ложить 
                DriveInfo di = new DriveInfo(@"C:\");
                long Ffree = (di.TotalFreeSpace / 1024) / 1024;
                string MBFree = Ffree.ToString("#,##") + " MB";
                if (Ffree - 2500 < 0)
                {
                    MessageBox.Show("Для установки сервера Firebird 3.0 необходимо свободное пространство 2.5Гб на диске С:." + Environment.NewLine + "Текущее пространство " + MBFree + Environment.NewLine + "Выполнение процедуры остановлено. ");
                    return;
                }
                // Проверка каталогов серверов
                SystemConectoServers.DirServer();

  
                int IndCount = -1;
                DBConecto.DBopenFBConectionMemory(SystemBD.ConnnStringSystem().ToString(), "", "FbSystem");
                string StrCreate = "SELECT * from SERVERACTIVPOSTGRESQL";
                FbCommand SelectTable = new FbCommand(StrCreate, DBConecto.bdFbSystemConect);
                FbDataReader ReadOutTable = SelectTable.ExecuteReader();
                while (ReadOutTable.Read())
                {
                    IndCount++;
                    Administrator.AdminPanels.IdPort[IndCount] = ReadOutTable[0].ToString();
                    Administrator.AdminPanels.NamePuth[IndCount] = ReadOutTable[1].ToString();
                    Administrator.AdminPanels.NameServer[IndCount] = ReadOutTable[2].ToString();
                }
                ReadOutTable.Close();
                SelectTable.Dispose();
                DBConecto.DBcloseFBConectionMemory("FbSystem");
                if (IndCount >= 6)
                {
                    string TextWind = "Количество установленных серверов превышает 5" + Environment.NewLine + "Удалите ненужные записи из таблицы и повторите установку ";
                    int AutoCls = 1;
                    int MesaggeTp = 350;
                    int MessageLf = 650;
                    InstallB52.MessageErr(TextWind, AutoCls, MesaggeTp, MessageLf);
                    return;
                }
                AppStart.RenderInfo Arguments01 = new AppStart.RenderInfo() { };
                Arguments01.argument1 = "5432";
                Arguments01.argument2 = SystemConectoServers.PutchServer + @"Postgresql\";
                Arguments01.argument3 = "postgres";
                Arguments01.argument4 = SystemConectoServers.PutchServer + @"Postgresql\";
                if (IndCount >= 0)
                {
                    for (int i = 0; i <= IndCount; i++)
                    {
                        if (Administrator.AdminPanels.IdPort[i] == Arguments01.argument1)
                        {
                            Arguments01.argument1 = Convert.ToString(5432 + Convert.ToUInt32(i + 1));
                            i = 0;
                        }
                    }
                    for (int i = 0; i <= IndCount; i++)
                    {
                        if (Administrator.AdminPanels.NameServer[i] == Arguments01.argument3)
                        {
                            Arguments01.argument3 = "ConectoPostgre_" + Convert.ToString(i);
                            i = 0;
                        }

                    }
                    Arguments01.argument2 = SystemConectoServers.PutchServer + @"Postgresql" + Convert.ToString(IndCount + 1) + @"\";
                }
                Thread thStartTimer01 = new Thread(InstallB52.InstallServPostGre);
                thStartTimer01.SetApartmentState(ApartmentState.STA);
                thStartTimer01.IsBackground = true; // Фоновый поток
                thStartTimer01.Start(Arguments01);
                InstallB52.IntThreadStart++;

            }
        }

        /// <summary>
        /// Установка сервера  Postgresql 10
        /// </summary>
        public static void InstallServPostGre(object ThreadObj)
        {
            int InstOkPG = 0;
            // Разбор аргументов
            AppStart.RenderInfo arguments = (AppStart.RenderInfo)ThreadObj;
            string IdPortServer = arguments.argument1;
            string PatchSR = arguments.argument2;
            string SetNameServer = arguments.argument3;
            string PuthServerPG = arguments.argument4;
            // Права в ОС Win
            if (App.aSystemVirable["UserWindowIdentity"] == "1")
            {
                string NameDir = "Postgresql";
                string NameFile = "postgresql-10.11-1-windows-x32.exe"; //"postgresql-12.1-1-windows-x64-binaries.zip.001";
                //string NameFileZip = "postgresql-12.1-1-windows-x64.exe"; //"postgresql-12.0-1-windows-x64-binaries.zip";  
                if (SystemConecto.OS64Bit) NameFile = "postgresql-12.1-1-windows-x64.exe";
                if (!System.IO.File.Exists(PuthServerPG + @"\bin\postgres.exe") && Directory.Exists(PuthServerPG + "data")) Directory.Delete(@"c:\Program Files\Conecto\Servers\PostgreSQL", true);
                if (!Directory.Exists(PatchSR)) Directory.CreateDirectory(PatchSR);
                if (!System.IO.File.Exists(SystemConectoServers.PutchServer + @"tmp\BD\" + NameFile))
                {
                    // Список файлов
                    Dictionary<string, string> fbembedList = new Dictionary<string, string>();
                        fbembedList.Add(SystemConectoServers.PutchServer + @"tmp\BD\" + NameFile, "server_bd/");
                        //fbembedList.Add(SystemConectoServers.PutchServer + @"tmp\BD\postgresql-12.0-1-windows-x64-binaries.zip.002", "server_bd/");
                        //fbembedList.Add(SystemConectoServers.PutchServer + @"tmp\BD\postgresql-12.0-1-windows-x64-binaries.zip.003", "server_bd/");
                        //fbembedList.Add(SystemConectoServers.PutchServer + @"tmp\BD\postgresql-12.0-1-windows-x64-binaries.zip.004", "server_bd/");
                        //fbembedList.Add(SystemConectoServers.PutchServer + @"tmp\BD\postgresql-12.0-1-windows-x64-binaries.zip.005", "server_bd/");
                        //fbembedList.Add(SystemConectoServers.PutchServer + @"tmp\BD\postgresql-12.0-1-windows-x64-binaries.zip.006", "server_bd/");
                        if (SystemConecto.IsFilesPRG(fbembedList, -1, "- Проверка файлов во время установки сервера " + NameDir) != "True")
                        {
                            MessageBox.Show("Отсутствует инсталяционный  файл установки сервера" + NameFile + "  Установка прекращена. ");
                            System.Windows.Application.Current.Dispatcher.Invoke(new Action(delegate ()
                            {
                                ConectoWorkSpace.Administrator.AdminPanels ConectoWorkSpace_InW = AppStart.LinkMainWindow("WAdminPanels");
                                Interface.CurrentStateInst("PostgresqlOnOff", "0", "on_off_1.png", ConectoWorkSpace_InW.PostgresqlOnOff);
                            }));
                            return;
                        }
                    // Сборка архива
                    // Распоковка
                    Install.Extract.Unarch_arhive(SystemConectoServers.PutchServer + @"tmp\BD\" + NameFile, SystemConectoServers.PutchServer + @"tmp\BD\");
                    // Распоковка
                    //Install.Extract.Unarch_arhive(SystemConectoServers.PutchServer + @"tmp\BD\" + NameFile, PatchSR);
                }

                // проверка наличия файлов
                // Установлен ли ранее в директории

                if (System.IO.File.Exists(@"C:\Program Files\PostgreSQL\12\bin\postgres.exe")) InstOkPG = 1;
                if (System.IO.File.Exists(PuthServerPG + @"\bin\postgres.exe")) InstOkPG = 2;
                string RunPG = "1";
                if (System.IO.File.Exists(SystemConectoServers.PutchServer + @"tmp\BD\"+ NameFile))
                {
                    RunPG = SystemConectoServers.PutchServer + @"tmp\BD\" + NameFile;
                    //string RunPG = PatchSR + @"12\bin\pg_ctl.exe";
                    string StrArguments = ""; // start  -D "+ SystemConectoServers.PutchServerData + @"Postgresql\data\" ;
  

                    // Проверка повторного запуска инсталятора.
                    Administrator.AdminPanels.ScanActivFirebird(NameFile);
                    if (Administrator.AdminPanels.IndexActivProces < 0)
                    {
                        // Запуск установки PostGreSQL
                        Clipboard.SetText(PuthServerPG);
                        Administrator.AdminPanels.RunProcess(RunPG, StrArguments);
                        // Анализ установки
                        if (!System.IO.File.Exists(PuthServerPG + @"\bin\postgres.exe") && InstOkPG == 1 || (!System.IO.File.Exists(PuthServerPG + @"\bin\postgres.exe") && InstOkPG == 0)) RunPG = "2";
                        if (System.IO.File.Exists(PuthServerPG + @"\bin\postgres.exe") && InstOkPG == 1) RunPG = "0";
                        if (System.IO.File.Exists(PuthServerPG + @"\bin\postgres.exe") && InstOkPG == 0) RunPG = "0";
                        if (System.IO.File.Exists(PuthServerPG + @"\bin\postgres.exe") && InstOkPG == 2) RunPG = "3";
                        if (System.IO.File.Exists(@"c:\Program Files\PostgreSQL\12\bin\postgres.exe") && InstOkPG == 0)
                        {
                            RunPG = "0";
                            PatchSR = @"c:\Program Files\PostgreSQL\12\";
                        }
                        if (RunPG == "0")
                        {

                            int Idcount = 0;
                            DBConecto.DBopenFBConectionMemory(SystemBD.ConnnStringSystem().ToString(), "", "FbSystem");
                            string StrCreate = "select * from SERVERACTIVPOSTGRESQL where SERVERACTIVPOSTGRESQL.PUTH = " + "'" + PatchSR + "'";
                            FbCommand InsertTable = new FbCommand(StrCreate, DBConecto.bdFbSystemConect);
                            InsertTable.CommandType = CommandType.Text;
                            FbDataReader ReadTable = InsertTable.ExecuteReader();
                            while (ReadTable.Read())
                            {
                                IdPortServer = ReadTable[0].ToString();
                                Idcount++;
                            }
                            ReadTable.Close();
                            if (Idcount == 0)

                            {

                                string DateCreatPG = DateTime.Now.ToString("yyyyMMddHHmmss");
                                StrCreate = "INSERT INTO SERVERACTIVPOSTGRESQL  values (" + IdPortServer + ",'" + PatchSR + "', '" + SetNameServer + "', '" + DateCreatPG + "','Activ','pgsql')";
                                DBConecto.UniQuery InsertQuery = new DBConecto.UniQuery(StrCreate, "FB");
                                InsertQuery.UserQuery = string.Format(StrCreate, "SERVERACTIVPOSTGRESQL");
                                InsertQuery.ExecuteUNIScalar();

                            }
                            InsertTable.Dispose();
                            DBConecto.DBcloseFBConectionMemory("FbSystem");
                            Administrator.AdminPanels.SetServerGrid("SELECT * from SERVERACTIVPOSTGRESQL");

                            // Завершение инсталяции
                            System.Windows.Application.Current.Dispatcher.Invoke(new Action(delegate ()
                            {
                                ConectoWorkSpace.Administrator.AdminPanels ConectoWorkSpace_InW = AppStart.LinkMainWindow("WAdminPanels");
                                Interface.CurrentStateInst("PostgresqlOnOff", "2", "on_off_2.png", ConectoWorkSpace_InW.PostgresqlOnOff);
                                ConectoWorkSpace_InW.PuthSetServerPostGre.Text = PatchSR;
                                ConectoWorkSpace_InW.SetBDPostGreSQL.IsEnabled = true;
                                ConectoWorkSpace_InW.AddSeverPostGre.Foreground = Brushes.White;
                                if (AppStart.TableReestr["NameServerPG"] == "" || AppStart.TableReestr["ServerDefaultPG"] == "")
                                {
                                    ConectoWorkSpace_InW.DefNameServerPG.Text = SetNameServer;
                                    Administrator.AdminPanels.UpdateKeyReestr("ServerDefaultPG", PatchSR);
                                    Administrator.AdminPanels.UpdateKeyReestr("NameServerPG", SetNameServer);
                                }


                            }));


                            // Проверяем наличие ярлыка.exe на рабочем столе
                            if (!System.IO.File.Exists(Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "PgAdmin4.lnk"))
                            {
                                // Проверяем есть ли директория pgAdmin 4\bin
                                if (Directory.Exists(PuthServerPG + @"pgAdmin 4\bin\"))
                                {
                                   // Формируем параметры для ярлыка
                                    string NameLnk = "PgAdmin4.lnk";
                                    string PuthExe = PuthServerPG + @"pgAdmin 4\bin\pgAdmin4.exe";
                                    string PuthWork = PuthServerPG + @"pgAdmin 4\bin\";
                                    
                                    // создаем ярлык
                                    App.CreatLnk(PuthExe, PuthExe, @"\"+NameLnk, "", PuthWork);
                                }

                            }

                        var TextWin = "Установка сервера postgresql windows-x64" + Environment.NewLine + "завершена.";
                        int AutoCl = 1;
                        int MesaggeT = -170;
                        int MessageL = 600;
                        MessageEnd(TextWin, AutoCl, MesaggeT, MessageL);
                    }

                }
 
 
                }
                if(RunPG == "1" || RunPG == "2")
                {
                    var TextWin = "Установка сервера postgresql " + Environment.NewLine + "остановлена.";
                    TextWin = RunPG == "1" ?  "Отсутствует файл "+ NameFile : TextWin;
                    int AutoCl = 1;
                    int MesaggeT = -170;
                    int MessageL = 600;
                    MessageEnd(TextWin, AutoCl, MesaggeT, MessageL);
                    System.Windows.Application.Current.Dispatcher.Invoke(new Action(delegate ()
                    {
                        ConectoWorkSpace.Administrator.AdminPanels ConectoWorkSpace_InW = AppStart.LinkMainWindow("WAdminPanels");
                        Interface.CurrentStateInst("PostgresqlOnOff", "0", "on_off_1.png", ConectoWorkSpace_InW.PostgresqlOnOff);
                        ConectoWorkSpace_InW.SetBDPostGreSQL.IsEnabled = true;
                        ConectoWorkSpace_InW.PuthSetServerPostGre.Text = "";

                    }));
  
                }
            }
            IntThreadStart--;
        }

        /// <summary>
        /// Деинсталяция  сервера Postgresql
        /// 
        /// </summary>
        public static void UnInstallServPostgresql()
        {
            ConectoWorkSpace.WinOblakoSetUpdate ConectoWorkSpace_Up = AppStart.LinkMainWindow("WinOblakoSetUpdateW");
            if (ConectoWorkSpace_Up != null)
            {
                System.Windows.Application.Current.Dispatcher.Invoke(new Action(delegate ()
                {
                    ConectoWorkSpace.WinOblakoSetUpdate ConectoWorkSpace_Off = AppStart.LinkMainWindow("WinOblakoSetUpdateW");
                    ConectoWorkSpace_Off.Close();
                }));
            }
            var Text = "Подождите пожалуйста выполняется загрузка" + Environment.NewLine + "деинсталятора сервера.";
            int Auto = 1;
            int Mesagge = -170;
            int Messagl = 600;
            MessageEnd(Text, Auto, Mesagge, Messagl);


            ConectoWorkSpace.Administrator.AdminPanels ConectoWorkSpace_InW = AppStart.LinkMainWindow("WAdminPanels");

            string RunUnInstal = ConectoWorkSpace_InW.PuthSetServerPostGre.Text+"uninstall-postgresql.exe";
            string StrArguments = "";
            if (System.IO.File.Exists(RunUnInstal))
            {
                Administrator.AdminPanels.ImageObj = "StoptServPG";
                Administrator.AdminPanels.SelectPuthPG = AppStart.TableReestr["PuthSetBDPostGreSQL"];
                if (InstallB52.RemoveServerFB25(Administrator.AdminPanels.SetPuthPG, Administrator.AdminPanels.SetNamePG) > 0)
                {
                    Administrator.AdminPanels.RunProcess(RunUnInstal, StrArguments);
                    for (int i = 0; i < 2; i++)
                    {
                        Thread.Sleep(75000);
                        if (!System.IO.File.Exists(RunUnInstal)) break;
                        else i = 0;
                    }
                    string StrCreate = "DELETE from SERVERACTIVPOSTGRESQL WHERE SERVERACTIVPOSTGRESQL.PUTH = " + "'" + ConectoWorkSpace_InW.PuthSetServerPostGre.Text + "'";
                    DBConecto.DBopenFBConectionMemory(SystemBD.ConnnStringSystem().ToString(), "", "FbSystem");
                    DBConecto.UniQuery DeletQuery = new DBConecto.UniQuery(StrCreate, "FB");
                    DeletQuery.UserQuery = string.Format(StrCreate, "SERVERACTIVPOSTGRESQL");
                    DeletQuery.ExecuteUNIScalar();
                    Administrator.AdminPanels.SetServerGrid("SELECT * from SERVERACTIVPOSTGRESQL");
                    Interface.CurrentStateInst("PostgresqlOnOff", "0", "on_off_1.png", ConectoWorkSpace_InW.PostgresqlOnOff);
                    ConectoWorkSpace_InW.PuthSetServerPostGre.Text = "";
                    Administrator.AdminPanels.UpdateKeyReestr("ServerDefaultPG", "");
                    Administrator.AdminPanels.UpdateKeyReestr("NameServerPG", "");
                    Administrator.AdminPanels.UpdateKeyReestr("CurrentPasswABDPG", "");
                    ConectoWorkSpace_InW.DeletSreverPostGre.Foreground = Brushes.White;
                }
                else
                {
                    var TextWin = "Деинсталяция сервера postgresql " + Environment.NewLine + "не выполнена.";
                    int AutoCl = 1;
                    int MesaggeT = -170;
                    int MessageL = 600;
                    MessageEnd(TextWin, AutoCl, MesaggeT, MessageL);
                }

 
            }

 
        }

        #endregion  Postgresql

        // Инсталяция  сервера MsSql
        #region MsSql
        public static void InstallServMsSql()
        {
            ConectoWorkSpace.Administrator.AdminPanels ConectoWorkSpace_InW = AppStart.LinkMainWindow("WAdminPanels");
            if (ConectoWorkSpace_InW == null)
            {
                return;
            }
            else
            {
                Administrator.AdminPanels.SetProcesRun = 1;
                AppStart.RenderInfo Arguments01 = new AppStart.RenderInfo() { };
                Arguments01.argument1 = "2";
                Arguments01.argument2 = "";
                Thread thStartTimer01 = new Thread(InstallServMsSqlTH);
                thStartTimer01.SetApartmentState(ApartmentState.STA);
                thStartTimer01.IsBackground = true; // Фоновый поток
                thStartTimer01.Start(Arguments01);
                IntThreadStart++;

            }
        }
        public static void InstallServMsSqlTH(object ThreadObj)
        {
            MessageBox.Show("Процедура в разработке");
            System.Windows.Application.Current.Dispatcher.Invoke(new Action(delegate ()
            {
                ConectoWorkSpace.Administrator.AdminPanels ConectoWorkSpace_InW = AppStart.LinkMainWindow("WAdminPanels");
                Interface.CurrentStateInst("MsSqlOnOff", "0", "on_off_1.png", ConectoWorkSpace_InW.MsSqlOnOff);
            }));
            return;
        }
        #endregion MsSql

        #region InstalDrv

        /// <summary>
        /// Включение помощи по ключю
        /// Можно скачиват по потребности
        /// </summary>
        public static void InstallKeyHelp()
        {

            // Список файлов
            Dictionary<string, string> fbembedList = new Dictionary<string, string>();
            fbembedList.Add(SystemConecto.PutchApp + @"Utils\KeyUtils\admins_manual.pdf", "b52/keyutils/DOC/");


            if (SystemConecto.IsFilesPRG(fbembedList, -1, "- Проверка файлов во время установки файлов помощи") == "True")
            {

                // Вывод любого сообщения в ощий лог
                // SystemConecto.ErorDebag(ex.ToString());
                // MessageBox.Show("Нет файлов");
            }


        }
        /// <summary>
        /// Установка утилиты IbeXpert для работы с БД сервера FB
        /// </summary>
        public static void InstallUtilServIB()
        {
            // Директории серверов
            // Права в ОС Win
            if (App.aSystemVirable["UserWindowIdentity"] == "1")
            {
                // Проверка установки утилит
                if (!SystemConecto.DIR_(SystemConecto.PutchApp + @"IbeXpert")) SystemConecto.STOP = true;


            }

        }

        //    Инсталяция локального ключа
        public static void InstallTHLocKey()
        {
            AppStart.RenderInfo Arguments01 = new AppStart.RenderInfo() { };
            Arguments01.argument1 = Administrator.AdminPanels.NameObj;
            Arguments01.argument2 = "3182";
            Thread thStartTimer01 = new Thread(InstallKeyDriver);
            thStartTimer01.SetApartmentState(ApartmentState.STA);
            thStartTimer01.IsBackground = true; // Фоновый поток
            thStartTimer01.Start(Arguments01);
            IntThreadStart++;
 

        }
        //   Деинсталяция локального ключа
        public static void UnInstallTHLocKey()
        {
            // Вызов процедуры  или тело процедуры деинсталяции драйверов

            UnInstallKeyDriver(Administrator.AdminPanels.NameObj);
        }

        /// <summary>
        /// 1) Установка драйвера Можно проверить устройство подключенное к USB?
        ///    a) без драйверов
        ///    в) с драйверами
        /// </summary>
        public static void InstallKeyDriver(object ThreadObj)
        {
            AppStart.RenderInfo arguments = (AppStart.RenderInfo)ThreadObj;

            Administrator.AdminPanels.Inst2530 = "grdsrv";
            Administrator.AdminPanels.ScanActivFirebird();
            if (Administrator.AdminPanels.IndexActivProces >= 0)
            {
                System.Windows.Application.Current.Dispatcher.Invoke(new Action(delegate ()
                {
                    ConectoWorkSpace.Administrator.AdminPanels ConectoWorkSpace_InW = AppStart.LinkMainWindow("WAdminPanels");
                    Interface.CurrentStateInst("SetLocKeyOnOff", "2", "on_off_2.png", ConectoWorkSpace_InW.SetLocKeyOnOff);
                }));
                Administrator.AdminPanels.UpdateKeyReestr("SetLocKeyOnOff", "2");
                return;
            }
            string StatusPic = "on_off_1.png";
            string SetKeyValue = "0";
            // Список устройств в лог файл Guardant Stealth II USB dongle
            // формат USB\VID_XXXX&PID_XXXX\Serial_number(& - разделитель, разные порты подключения в конце значения Serial_number : 
            // 6 &30AFBA37&0&12  || 6&30AFBA37&0&13 один комп одно устройство разные порты USB)
            string[] devInfo = new string[] { };
            //if (DevicePC.ListDevicePC(ref devInfo, "USB\\\\VID_0A89&PID_0003")) { Administrator.AdminPanels.ProcesEnd = 0; return; } 
            // Проверка установки утилит
            if (!SystemConecto.DIR_(SystemConecto.PutchApp + @"Utils\Temp\DrvKeyApp")) SetKeyValue = "0";
            // Список файлов
            Dictionary<string, string> fbembedList = new Dictionary<string, string>();
            // Драйвера
            fbembedList.Add(SystemConecto.PutchApp + @"Utils\Temp\DrvKeyApp\GrdDrivers.exe", "b52/keyutils/");
            if (SystemConecto.IsFilesPRG(fbembedList, -1, "- Проверка файлов во время установки драйверов ключа") == "True")
            {
                StatusPic = "on_off_2.png";
                SetKeyValue = "2";
                Process mv_prcInstaller = new Process();
                mv_prcInstaller.StartInfo.FileName = SystemConecto.PutchApp + @"Utils\Temp\DrvKeyApp\GrdDrivers.exe"; 
                mv_prcInstaller.StartInfo.Arguments = "/insall /passive /log LogGuardant.txt";  //quiet
                mv_prcInstaller.Start();
                mv_prcInstaller.WaitForExit();
                mv_prcInstaller.Close();
            }
            System.Windows.Application.Current.Dispatcher.Invoke(new Action(delegate ()
            {
                ConectoWorkSpace.Administrator.AdminPanels ConectoWorkSpace_InW = AppStart.LinkMainWindow("WAdminPanels");
                var pic = (Image)LogicalTreeHelper.FindLogicalNode(ConectoWorkSpace_InW, Administrator.AdminPanels.NameObj);
                var uriSource = new Uri(@"/Conecto®%20WorkSpace;component/Images/" + StatusPic, UriKind.Relative);
                pic.Source = new BitmapImage(uriSource);

            }));
            Administrator.AdminPanels.UpdateKeyReestr(Administrator.AdminPanels.NameObj, SetKeyValue);
            IntThreadStart--;

        }

        /// <summary>
        /// 1) Отключение драйвера подключенное к USB?
        /// 
        /// </summary>
        public static void UnInstallKeyDriver(string KeyOb)
        {
            // Проверка не установлен ли серевер ключей, если нет то деинсталируются драйвера.
            if (Convert.ToInt32(AppStart.TableReestr["BackOfServKeyOnOff"]) != 2)
            {
                Process mv_prcInstaller = new Process();
                mv_prcInstaller.StartInfo.FileName = SystemConecto.PutchApp + @"Utils\Temp\DrvKeyApp\GrdDrivers.exe"; //"msiexec.exe"; //GrdDriversRU.msi
                mv_prcInstaller.StartInfo.Arguments = "/uninsall /passive  /log LogGuardant.txt";///quiet
                //mv_prcInstaller.StartInfo.RedirectStandardInput = true;
                mv_prcInstaller.Start();
                mv_prcInstaller.WaitForExit();
                mv_prcInstaller.Close();
            }
            System.Windows.Application.Current.Dispatcher.Invoke(new Action(delegate ()
            {
                ConectoWorkSpace.Administrator.AdminPanels ConectoWorkSpace_InW = AppStart.LinkMainWindow("WAdminPanels");
                var pic = (Image)LogicalTreeHelper.FindLogicalNode(ConectoWorkSpace_InW, Administrator.AdminPanels.NameObj);
                var uriSource = new Uri(@"/Conecto®%20WorkSpace;component/Images/" + "on_off_1.png", UriKind.Relative);
                pic.Source = new BitmapImage(uriSource);
            }));
            Administrator.AdminPanels.UpdateKeyReestr(Administrator.AdminPanels.NameObj, "0");

        }

        /// <summary>
        /// Установка сервера ключа
        /// InrfLoad - "0" - без интерфецса  "1" - инетерфейс AdminPanel
        /// </summary>
        public static void InstallTHKeyServ()    // 
        {

            AppStart.RenderInfo Arguments01 = new AppStart.RenderInfo() { };
            Arguments01.argument1 = "1";
            Arguments01.argument2 = AppStart.TableReestr["BackOfAdresPorta"]; // "3182"
            Thread thStartTimer01 = new Thread(InstallKeyServ);
            thStartTimer01.SetApartmentState(ApartmentState.STA);
            thStartTimer01.IsBackground = true; // Фоновый поток
            thStartTimer01.Start(Arguments01);
            IntThreadStart++;
        }




        /// <summary>
        ///  Продолжение установки сервера ключа
        /// </summary>
        public static void InstallKeyServ(object ThreadObj)
        {
            // Разбор аргументов
            AppStart.RenderInfo arguments = (AppStart.RenderInfo)ThreadObj;
            int CallExit = Convert.ToInt16(arguments.argument1); 
            // Права в ОС Win
            if (App.aSystemVirable["UserWindowIdentity"] == "1")
            {
                
                // Проверка установки утилит
                if (!SystemConecto.DIR_(SystemConecto.PutchApp + @"Utils\KeyUtils")) ConectoWorkSpace.Administrator.AdminPanels.hubdate = "0";
 
                if (!System.IO.File.Exists(SystemConecto.PutchApp + @"Utils\KeyUtils\grdsrv.exe"))
                {
                    // Список файлов распаковка
                    Dictionary<string, string> fbembedList = new Dictionary<string, string>();
 
                    fbembedList.Add(SystemConecto.PutchApp + @"Utils\KeyUtils\CHKNSK32.EXE", "b52/keyutils/");
                    fbembedList.Add(SystemConecto.PutchApp + @"Utils\KeyUtils\grdmon.exe", "b52/keyutils/");
                    fbembedList.Add(SystemConecto.PutchApp + @"Utils\KeyUtils\grdsrv.exe", "b52/keyutils/");
                    fbembedList.Add(SystemConecto.PutchApp + @"Utils\KeyUtils\NOVEX32.DLL", "b52/keyutils/");
                    fbembedList.Add(SystemConecto.PutchApp + @"Utils\KeyUtils\grdsrv.ini", "b52/keyutils/");
                    fbembedList.Add(SystemConecto.PutchApp + @"Utils\KeyUtils\gnclient.ini", "b52/keyutils/");
 
                    if (SystemConecto.IsFilesPRG(fbembedList, -1, "- Проверка файлов во время установки файлов сервра ключа") == "True") ConectoWorkSpace.Administrator.AdminPanels.hubdate = "1";
 
                }

                string PuthExe = SystemConecto.PutchApp + @"Utils\KeyUtils\grdsrv.exe";
                string NameProces = PuthExe.Substring(PuthExe.LastIndexOf(@"\") + 1, PuthExe.LastIndexOf(".") - (PuthExe.LastIndexOf(@"\") + 1));

                Process[] proc = Process.GetProcessesByName("grdsrv");
                if (proc.Length == 0)
                {
                    //if (proc[0] != null) proc[0].Kill();
 
                    if (!AppStart.SetFocusWindow(NameProces))
                    {

                    //// Запуск сервера как приложения
                    Process mv_prcInstaller = new Process();
                    mv_prcInstaller.StartInfo.FileName = PuthExe;
                    mv_prcInstaller.StartInfo.Arguments = "-f ";
                    mv_prcInstaller.StartInfo.CreateNoWindow = true;
                    mv_prcInstaller.StartInfo.UseShellExecute = false;
                    mv_prcInstaller.StartInfo.RedirectStandardOutput = true;
                    mv_prcInstaller.Start();
                    mv_prcInstaller.WaitForInputIdle();
  
                    }
                    // Устанавливаем нужные размеры 
                    if (TextPasteWindow.SetFocusWindow(NameProces))
                    {
                        if (!TextPasteWindow.SetWindowPos(TextPasteWindow.hControl, HWND.BOTTOM, 400, 400, 0, 0, SWP.HIDEWINDOW | SWP.NOSIZE)) //
                        {
                            SystemConecto.ErorDebag("Ошибка работы функции: SetWindowPos: окно" + "calc");
                        }
                    }
 
  
               }
               Administrator.AdminPanels.UpdateKeyReestr("BackOfServKeyOnOff", "2");
               if (CallExit == 2) return;
 
                // Регистрация сервера как службы
                //DllWork.RegSrvice(SystemConecto.PutchApp + @"Utils\KeyUtils\grdsrv.exe", "-f");

                string TCP_PORT = arguments.argument2.Length == 0 || arguments.argument2.Length < 4 ? "3182" : arguments.argument2;
                    int Idcount = 0;
                    Encoding code = Encoding.Default;
                    string[] fileLines = System.IO.File.ReadAllLines(SystemConecto.PutchApp + @"Utils\KeyUtils\grdsrv.ini", code);
                    foreach (string x in fileLines)
                    {
                        if (x.Contains("TCP_PORT") == true) fileLines[Idcount] = "TCP_PORT =" + TCP_PORT;
                        Idcount++;
                    }
                    System.IO.File.WriteAllLines(SystemConecto.PutchApp + @"Utils\KeyUtils\grdsrv.ini", fileLines, code);
                    System.Windows.Application.Current.Dispatcher.Invoke(new Action(delegate ()
                    {
                        ConectoWorkSpace.Administrator.AdminPanels ConectoWorkSpace_Puth = AppStart.LinkMainWindow("WAdminPanels");
                        var pic = (Image)LogicalTreeHelper.FindLogicalNode(ConectoWorkSpace_Puth, "BackOfServKeyOnOff");
                        var uriSource = new Uri(@"/Conecto®%20WorkSpace;component/Images/on_off_2.png", UriKind.Relative);
                        pic.Source = new BitmapImage(uriSource);
                        Interface.CurrentStateInst("BackOfServKeyOnOff", "2", "on_off_2.png", ConectoWorkSpace_Puth.BackOfServKeyOnOff);
                        Interface.CurrentStateInst("SetLocKeyOnOff", "2", "on_off_2.png", ConectoWorkSpace_Puth.SetLocKeyOnOff);
                    }));

      
            }
            IntThreadStart--;
        }

        /// <summary>
        /// 1) Отключение сервера драйвера ключа подключенного к USB?
        /// 
        /// </summary>
        public static void UnInstallKeyServ()
        {

   
            //Administrator.AdminPanels.UpdateKeyReestr(Administrator.AdminPanels.NameObj, "0");
            if (SystemConecto.File_(SystemConecto.PutchApp + @"Utils\KeyUtils\grdsrv.exe", 5))
            {
                Process[] proc = Process.GetProcessesByName("grdsrv");
                if (proc.Length != 0)
                {
                    if (proc[0] != null ) proc[0].Kill();
                }


   
                // выгрузить  сервер как службу
                //DllWork.RegSrvice(SystemConecto.PutchApp + @"Utils\KeyUtils\grdsrv.exe", "-u");
                // Проверка отключения
                //if (DllWork.StatusService("grdsrv") != "0") ConectoWorkSpace.Administrator.AdminPanels.hubdate = "0";
            }
            System.Windows.Application.Current.Dispatcher.Invoke(new Action(delegate ()
            {
                ConectoWorkSpace.Administrator.AdminPanels ConectoWorkSpace_InW = AppStart.LinkMainWindow("WAdminPanels");
                var pic = (Image)LogicalTreeHelper.FindLogicalNode(ConectoWorkSpace_InW, "BackOfServKeyOnOff");
                var uriSource = new Uri(@"/Conecto®%20WorkSpace;component/Images/on_off_1.png", UriKind.Relative);
                pic.Source = new BitmapImage(uriSource);
                Interface.CurrentStateInst("BackOfServKeyOnOff", "0", "on_off_1.png", ConectoWorkSpace_InW.BackOfServKeyOnOff);

            }));
  

        }


        /// <summary>
        /// 1) Отключение драйвера подключенное к USB?
        /// 
        /// </summary>
        public static void UnInstallKeyDriver()
        {
            Process mv_prcInstaller = new Process();

            mv_prcInstaller.StartInfo.FileName = "msiexec.exe";
            mv_prcInstaller.StartInfo.Arguments = "/x\"" + @SystemConecto.PutchApp + @"Utils\Temp\DrvKeyApp\GrdDriversRU.msi" + "\"" + "/qn";
            mv_prcInstaller.StartInfo.RedirectStandardInput = true;
            mv_prcInstaller.Start();
        }

        #endregion InstalDrv

        #region InstallTestkey

        /// <summary>
        /// Включение тестового ключа фронта
        /// </summary>
        public static void WindowSetkey()
        {
            int TopWin = 0, LeftWin = 0;
            // Константы смещение окна для разных запросов
            if (Administrator.AdminPanels.SetWinSetHub == "AddApp") { TopWin = 400; LeftWin = 1200; }
            if (Administrator.AdminPanels.SetWinSetHub == "Testkey") { TopWin = 200; LeftWin = 1200; }
            if (Administrator.AdminPanels.SetWinSetHub == "Lockey") { TopWin = 400; LeftWin = 1200; }
            if (Administrator.AdminPanels.SetWinSetHub == "Netkey") { TopWin = 300; LeftWin = 1200; }
            // Открыть окно с дата грид для выбора строки   
            System.Windows.Application.Current.Dispatcher.Invoke(new Action(delegate ()
            {
                ConectoWorkSpace.Administrator.AdminPanels ConectoWorkSpace_InW = AppStart.LinkMainWindow("WAdminPanels");
                WinSetKey SetWindow = new WinSetKey();
                SetWindow.Owner = ConectoWorkSpace_InW; 
                SetWindow.Top = ConectoWorkSpace_InW.Top  + TopWin;
                SetWindow.Left = ConectoWorkSpace_InW.ScrollViewerd.Width/SetWindow.Width/4* SetWindow.Width; 
                SetWindow.Show();
            }));
        }

        public static  void AddTabKey(string StrCreate,string InsertTab, string SelectTab, string StrLocKey, string StrNetKey)
        {

            string TcpPort = InsertTab == "TESTKEY" ? "4549" :"", IpName = InsertTab == "TESTKEY" ? "176.106.0.219" : "";
            TcpPort = InsertTab == "LOCKEY" ? "3182" : TcpPort; IpName = InsertTab == "LOCKEY" ? "127.0.0.1" : IpName;
            int Idcount = 0;
            DBConecto.DBopenFBConectionMemory(SystemBD.ConnnStringSystem().ToString(), "", "FbSystem");
            FbCommand SelectLocKey = new FbCommand(StrLocKey, DBConecto.bdFbSystemConect);
            SelectLocKey.CommandType = CommandType.Text;
            FbDataReader ReadOutLocKey = SelectLocKey.ExecuteReader();
            while (ReadOutLocKey.Read()) { Idcount++; }
            ReadOutLocKey.Close();
            if (Idcount != 0)
            {
                var TextWindows = "Недопукается использование одного Бека или фронта с разными ключами одновременно. Повторите выбор.";
                int AutoClose = 1;
                int MesaggeTop = 100;
                int MessageLeft = 850;
                InstallB52.MessageErr(TextWindows, AutoClose, MesaggeTop, MessageLeft);
                return;
            }
            Idcount = 0;
            FbCommand SelectNetKey = new FbCommand(StrNetKey, DBConecto.bdFbSystemConect);
            SelectNetKey.CommandType = CommandType.Text;
            FbDataReader ReadOutNetKey = SelectNetKey.ExecuteReader();
            while (ReadOutNetKey.Read()) {Idcount++;}
            ReadOutNetKey.Close();
            if (Idcount != 0)
            {
                var TextWindows = "Недопукается использование одного Бека или фронта с разными ключами одновременно. Повторите выбор.";
                int AutoClose = 1;
                int MesaggeTop = 100;
                int MessageLeft = 850;
                InstallB52.MessageErr(TextWindows, AutoClose, MesaggeTop, MessageLeft);
                return;
            }
            Idcount = 0;
            FbCommand SelectTable = new FbCommand(StrCreate, DBConecto.bdFbSystemConect);
            SelectTable.CommandType = CommandType.Text;
            FbDataReader ReadOutTable = SelectTable.ExecuteReader();
            while (ReadOutTable.Read()) { Idcount++; }
            ReadOutTable.Close();
            if (Idcount == 0)
            {
                StrCreate = "INSERT INTO "+ InsertTab + "  VALUES ('" + 
                    ConectoWorkSpace.Administrator.AdminPanels.TestKeyType + "','" + 
                    ConectoWorkSpace.Administrator.AdminPanels.TestKeyName + "'" + ", '" +
                    ConectoWorkSpace.Administrator.AdminPanels.TestKeyServer + "', '" +
                    ConectoWorkSpace.Administrator.AdminPanels.TestKeyConect + "', '" +
                    TcpPort + "', '" +
                    IpName + "', '" +
                    Administrator.AdminPanels.TestKeyPuth + "')";
                DBConecto.UniQuery CountQuery = new DBConecto.UniQuery(StrCreate, "FB");
                CountQuery.ExecuteUNIScalar();
            }
            SelectTable.Dispose();
            DBConecto.DBcloseFBConectionMemory("FbSystem");
            Administrator.AdminPanels.Grid_key(SelectTab);
            if (ConectoWorkSpace.Administrator.AdminPanels.TestKeyType == "Back") StrCreate = "UPDATE REESTRBACK SET KEY = " + "'" + InsertTab + "'" + " WHERE REESTRBACK.PUTH = " + "'" + ConectoWorkSpace.Administrator.AdminPanels.TestKeyPuth + "'";
            if (ConectoWorkSpace.Administrator.AdminPanels.TestKeyType == "Front") StrCreate = "UPDATE REESTRFRONT SET KEY = " + "'" + InsertTab + "'" + " WHERE REESTRFRONT.PUTH = " + "'" + ConectoWorkSpace.Administrator.AdminPanels.TestKeyPuth + "'";
            DBConecto.DBopenFBConectionMemory(SystemBD.ConnnStringSystem().ToString(), "", "FbSystem");
            DBConecto.UniQuery UpdateQuery = new DBConecto.UniQuery(StrCreate, "FB");
            string CountTable = UpdateQuery.ExecuteUNIScalar() == null ? "" : UpdateQuery.ExecuteUNIScalar().ToString();
            DBConecto.DBcloseFBConectionMemory("FbSystem");
            if (ConectoWorkSpace.Administrator.AdminPanels.TestKeyType == "Back") ConectoWorkSpace.Administrator.AdminPanels.LoadedGridBack("SELECT * from REESTRBACK");
            if (ConectoWorkSpace.Administrator.AdminPanels.TestKeyType == "Front") ConectoWorkSpace.Administrator.AdminPanels.LoadedGridFront("SELECT * from REESTRFRONT");
            Administrator.AdminPanels.ChangeGnclient(Administrator.AdminPanels.TestKeyPuth.Substring(0, Administrator.AdminPanels.TestKeyPuth.LastIndexOf(@"\")+1), TcpPort, IpName);
        }

        #endregion InstallTestkey


        /// <summary>
        /// Установка BackOfficeB52 розница 
        /// </summary>
        public static void InstallBackB52()
        {
            ConectoWorkSpace.Administrator.AdminPanels ConectoWorkSpace_InW = AppStart.LinkMainWindow("WAdminPanels");
            if (ConectoWorkSpace_InW == null)
            {
                return;
            }
            else
            {

                SetCheckTTF16();

                AppStart.RenderInfo Arguments01 = new AppStart.RenderInfo() { };
                Arguments01.argument1 = "2";
                Arguments01.argument2 = "";
                Thread thStartTimer01 = new Thread(InstallServBackB52);
                thStartTimer01.SetApartmentState(ApartmentState.STA);
                thStartTimer01.IsBackground = true; // Фоновый поток
                thStartTimer01.Start(Arguments01);
                IntThreadStart++;

            }
        }

        public static void SetCheckTTF16()
        {
            if (Convert.ToInt32(AppStart.TableReestr["CheckTTF16"]) == 0)
            {
                AppStart.RenderInfo Argument01 = new AppStart.RenderInfo() { };
                Argument01.argument1 = "1";
                Thread thStart = new Thread(Administrator.AdminPanels.CheckTTF16);
                thStart.SetApartmentState(ApartmentState.STA);
                thStart.IsBackground = true; // Фоновый поток
                thStart.Start(Argument01);
                InstallB52.IntThreadStart++;

            }
        }



        public static void InstallServBackB52(object ThreadObj)
        {

            if (Convert.ToInt32(AppStart.TableReestr["BackFbd30OnOff"].ToString()) == 2 && Convert.ToInt32(AppStart.TableReestr["BackFbd25OnOff"].ToString()) == 2)
            {
                ContentBackFrontOff();
                var TextWindows = "Одновременно установлены в системе Firebird_2_5 и Firebird_3_0." + Environment.NewLine + "Для установки БекОфис Б52 необходмио выбрать один из двух серверов, ненужный выключить. ";
                MessageErorInst(TextWindows);
                return;
            }

  
            if (Administrator.AdminPanels.FolderBackPuth == "")
            {
                ContentBackFrontOff();
                var TextWindows = "Не выбрано устройство для размещения BackB52" + Environment.NewLine + "Выполнение процедуры остановлено ";
                MessageErorInst(TextWindows);
                return;

            }
            // Проверка наличия свободного пространства на диске куда будем ложить 
            string DeviceOn = Administrator.AdminPanels.FolderBackPuth.Substring(0, 3);
            DriveInfo di = new DriveInfo(DeviceOn);
            long Ffree = (di.TotalFreeSpace / 1024) / 1024;
            string MBFree = Ffree.ToString("#,##") + " MB";
            if (Ffree - 500 < 0)
            {
                ContentBackFrontOff();
                var TextWindows = "Для установки сервера BackB52 необходимо свободное пространство 500мб на диске." + Environment.NewLine + "Текущее пространство " + MBFree + Environment.NewLine + "Выполнение процедуры остановлено. ";
                MessageErorInst(TextWindows);
                return;
            }


            // запуск сервера ключа.
            if (Convert.ToInt32(AppStart.TableReestr["BackOfServKeyOnOff"]) == 2)
            {
                Process[] FB25 = Process.GetProcessesByName("grdsrv");
                if (FB25.Length == 0)
                {
                    InstallTHKeyServ();
                    Thread.Sleep(2000);
                }
              
            }

  

            if (App.aSystemVirable["UserWindowIdentity"] == "1")
            {
  
                string PuthFolderBack = Administrator.AdminPanels.NameObj.Substring(0, Administrator.AdminPanels.NameObj.LastIndexOf("OnOff"));
                string NameFolderBack = Administrator.AdminPanels.NameObj.Substring(6, Administrator.AdminPanels.NameObj.LastIndexOf("OnOff")-6 );
                string NameDir = Administrator.AdminPanels.FolderBackPuth + PuthFolderBack + @"\";
                string PatchSRBack = Administrator.AdminPanels.FolderBackPuth + PuthFolderBack + @"\bin\";
                string PatchSR = Convert.ToInt32(AppStart.TableReestr["BackFbd25OnOff"].ToString()) == 2 ? AppStart.TableReestr["ServerDefault25"] : AppStart.TableReestr["ServerDefault30"];


                string StrCreate = "select * from REESTRBACK where REESTRBACK.PUTH = " + "'" + PatchSRBack + "B52BackOffice8.exe" + "'";
                DBConecto.DBopenFBConectionMemory(SystemBD.ConnnStringSystem().ToString(), "", "FbSystem");
                DBConecto.UniQuery DeletQuery = new DBConecto.UniQuery(StrCreate, "FB");
                DeletQuery.UserQuery = string.Format(StrCreate, "REESTRBACK");
                string CountTable = DeletQuery.ExecuteUNIScalar() == null ? "" : DeletQuery.ExecuteUNIScalar().ToString();
                DBConecto.DBcloseFBConectionMemory("FbSystem");
                if (CountTable.Length != 0)
                {
                    ContentBackFrontOff();
                    Administrator.AdminPanels.NameObj = "";
                    var TextWindows = "По выбранному пути Бек Офис ранее  установлен." + Environment.NewLine + " Для установки БекОфис Б52 необходмио выбрать уникальный путь. ";
                    MessageErorInst(TextWindows);
                    ExitErrorInstalBackFront(3);
                    return;
                }

 
                string CurrentSetBD = Convert.ToInt32(AppStart.TableReestr["BackFbd25OnOff"].ToString()) == 2 ? 
                    "select * from SERVERACTIVFB25 where SERVERACTIVFB25.NAME = " + "'" + Administrator.AdminPanels.NameServer25 + "'" :
                    "select * from SERVERACTIVFB30 where SERVERACTIVFB30.NAME = " + "'" + Administrator.AdminPanels.NameServer30 + "'";
                DBConecto.DBopenFBConectionMemory(SystemBD.ConnnStringSystem().ToString(), "", "FbSystem");
                FbCommand ServerKey = new FbCommand(CurrentSetBD, DBConecto.bdFbSystemConect);
                ServerKey.CommandType = CommandType.Text;
                FbDataReader ReaderName = ServerKey.ExecuteReader();
                while (ReaderName.Read()) { PatchSR = ReaderName[1].ToString(); }
                ReaderName.Close();
                ServerKey.Dispose();
                DBConecto.bdFbSystemConect.Close();

  
                string NameDll = "fb2532.zip";
                string PuthSetBD = Administrator.AdminPanels.SelectPuth;
                string Hub = Administrator.AdminPanels.SelectAlias; 

                Administrator.AdminPanels.UpdateKeyReestr("PatchSRBack", PatchSRBack);
                NameDll = Convert.ToInt32(AppStart.TableReestr["BackFbd25OnOff"].ToString()) == 2 && SystemConecto.OS64Bit ? "fb2532.zip" : NameDll; // "fb2564.zip"
                NameDll = Convert.ToInt32(AppStart.TableReestr["BackFbd30OnOff"].ToString()) == 2 && !SystemConecto.OS64Bit ? "fb3032.zip" : NameDll;
                NameDll = Convert.ToInt32(AppStart.TableReestr["BackFbd30OnOff"].ToString()) == 2 && SystemConecto.OS64Bit ? "fb3032.zip" : NameDll;
                string InstFrontOnOff = Convert.ToInt32(AppStart.TableReestr["BackFbd25OnOff"].ToString()) == 2 ? "BackFbd25OnOff" : "BackFbd30OnOff";
                // Проверка каталогов серверов
                SystemConectoServers.DirServer();
                // Проверка установки утилит
                if (!SystemConecto.DIR_(PatchSRBack)) MessageBox.Show(@"Отсутствует папкa "+ PatchSRBack);
                if (!SystemConecto.DIR_(SystemConectoServers.PutchServer + @"tmp\BackB52")) MessageBox.Show(@"Отсутствуют папки с архивом tmp\BackB52\");
                // Список файлов
                string SetBack = "b52/back/Roznica/";
                string NameFile = "B52BackOffice8.zip";
                string SetFolderBd = "b52/base/";
                string SetNameBd = "hub.fbk";
                if (ConectoWorkSpace.Administrator.AdminPanels.NameObj == "BackOfRestoranOnOff")
                {
                    SetBack = "b52/back/Restoran/";
                    NameFile = "B52BackOffice8.zip";
                }
                if (ConectoWorkSpace.Administrator.AdminPanels.NameObj == "BackOfFastFudOnOff")
                {
                    SetBack = "b52/back/Fast/";
                    NameFile = "B52BackOffice8.zip";
                }
                if (ConectoWorkSpace.Administrator.AdminPanels.NameObj == "BackOfFitnesOnOff")
                {
                   SetBack = "b52/back/Fitnes/";
                    NameFile = "B52BackOffice8.zip";
                    SetNameBd = "fitnes.fbk";

                }

                if (ConectoWorkSpace.Administrator.AdminPanels.NameObj == "BackOfHotelOnOff")
                {
                    SetBack = "b52/back/Hotel/";
                    NameFile = "B52BackOffice8.zip";
                    SetNameBd = "hotel.fbk";
                }

                if (ConectoWorkSpace.Administrator.AdminPanels.NameObj == "BackOfMixOnOff")
                {
                    SetBack = "b52/back/Mix/";
                    NameFile = "B52BackOffice8.zip";
                }




                // Поцедура завершения инсталяции Бек или Фронт Б52 1- Back

                MasSetBackFront[0] = PatchSRBack; MasSetBackFront[1] = NameDir; MasSetBackFront[2] = "1"; MasSetBackFront[3] = InstFrontOnOff;
                MasSetBackFront[4] = NameDll; MasSetBackFront[5] = PatchSR; MasSetBackFront[6] = PuthSetBD; MasSetBackFront[7] = Hub;
                MasSetBackFront[8] = NameFolderBack; MasSetBackFront[9] = NameFile; MasSetBackFront[10] = SetBack;
                MasSetBackFront[11] = SetFolderBd; MasSetBackFront[12] = SetNameBd;

                //ReinstalFBD(PatchSRBack, NameDir, 1, InstFrontOnOff, NameDll, PatchSR, PuthSetBD, Hub, NameFolderBack, NameFile);

                if (Administrator.AdminPanels.FolderBackPuth.LastIndexOf(@"\") > 2)
                {
                    Administrator.AdminPanels.Inst2530 = "Back";
                    string TextMesage = "Вы подтверждаете путь установки БекОфиса " + Environment.NewLine + PatchSRBack;
                    Administrator.AdminPanels.DeleteDefaultServer = 0;
                    System.Windows.Application.Current.Dispatcher.Invoke(new Action(delegate ()
                    {
                        ConectoWorkSpace.Administrator.AdminPanels ConectoWorkSpace_InW = AppStart.LinkMainWindow("WAdminPanels");
                        WinSetPuth SetPuth = new WinSetPuth(TextMesage);
                        SetPuth.Owner = ConectoWorkSpace_InW;
                        SetPuth.Top = (ConectoWorkSpace_InW.Top + 7) + ConectoWorkSpace_InW.Close_F.Margin.Top + (ConectoWorkSpace_InW.Close_F.Height - 2) + 170;
                        SetPuth.Left = (ConectoWorkSpace_InW.Left) + ConectoWorkSpace_InW.Close_F.Margin.Left + 300;
                        SetPuth.Show();


                    }));

                }
                else SetPuthB52();

            }
            IntThreadStart--;
        }

        public static  void SetPuthB52()
        {
            var TextFront = "Выполняется установка B52BackOffice." + Environment.NewLine + "Пожалуйста подождите. ";
            int AutoClose = 1;
            int MesaggeTop = -170;
            int MessageLeft = 800;
            InstallB52.MessageEnd(TextFront, AutoClose, MesaggeTop, MessageLeft);

            Dictionary<string, string> fbembedList = new Dictionary<string, string>();
            fbembedList.Add(SystemConectoServers.PutchServer + @"tmp\BackB52\" + MasSetBackFront[9], MasSetBackFront[10]);
            if (SystemConecto.IsFilesPRG(fbembedList, -1, "- Проверка файлов во время установки сервера " + MasSetBackFront[0]) != "True")
            {

                ContentBackFrontOff();
                var TextWindows = "Отсутствует инсталяционный файл B52BackOffice8.zip." + Environment.NewLine + "Выполнение процедуры остановлено ";
                MessageErorInst(TextWindows);
                ExitErrorInstalBackFront(1);
                return;
            }
            // Распоковка
            Install.Extract.Unarch_arhive(SystemConectoServers.PutchServer + @"tmp\BackB52\" + MasSetBackFront[9], MasSetBackFront[0]);
            // проверка наличия файлов
            if (!SystemConecto.File_(MasSetBackFront[0] + "B52BackOffice8.exe", 5))
            {
                ContentBackFrontOff();
                var TextWindows = "Отсутствует инсталяционный файл B52BackOffice8.exe." + Environment.NewLine + "Установка прекращена ";
                MessageErorInst(TextWindows);
                ExitErrorInstalBackFront(1);
                return;
            }
 
            ReinstalFBD();
        }
     



        // Деинсталяция Бек Офис Б52 розница
        public static void UnInstallBackB52()
        {
            //ExitErrorInstalBackFront(1);
            // тело деининсталяции
            // Права в ОС Win
            if (App.aSystemVirable["UserWindowIdentity"] == "1")
            {

                 // Удаление файлов
                string BackPuth = "", BackConect = "", BackServer = "";
                string NameFolderBack = Administrator.AdminPanels.NameObj.Substring(6, Administrator.AdminPanels.NameObj.LastIndexOf("OnOff")-6);
                string StrCreate = "select * from REESTRBACK where REESTRBACK.BACK = " + "'" + NameFolderBack + "'";
                if (Administrator.AdminPanels.PuthBackOff == "")
                {
                    DBConecto.DBopenFBConectionMemory(SystemBD.ConnnStringSystem().ToString(), "", "FbSystem");
                    FbCommand SelectTable = new FbCommand(StrCreate, DBConecto.bdFbSystemConect);
                    SelectTable.CommandTimeout = 3;
                    SelectTable.CommandType = CommandType.Text;
                    FbDataReader ReadTable = SelectTable.ExecuteReader();
                    while (ReadTable.Read())
                    {
                        BackConect = ReadTable[1].ToString();
                        BackServer = ReadTable[2].ToString();
                        BackPuth = ReadTable[3].ToString();
                    }
                    ReadTable.Close();
                    SelectTable.Dispose();
                    StrCreate = "DELETE from REESTRBACK where REESTRBACK.BACK = " + "'" + NameFolderBack + "'";
                }
                else
                {
                    StrCreate = "DELETE from REESTRBACK where REESTRBACK.PUTH = " + "'" + Administrator.AdminPanels.PuthBackOff + "'";
                    BackPuth = Administrator.AdminPanels.PuthBackOff;
                }
                DBConecto.UniQuery DeletQuery = new DBConecto.UniQuery(StrCreate, "FB");
                DeletQuery.UserQuery = string.Format(StrCreate, "REESTRBACK");
                DeletQuery.ExecuteUNIScalar();
 
                StrCreate = "SELECT count(*) from REESTRBACK";
                DeletQuery.UserQuery = string.Format(StrCreate, "REESTRBACK");
                string CountTable = DeletQuery.ExecuteUNIScalar() == null ? "" : DeletQuery.ExecuteUNIScalar().ToString();
                ConectoWorkSpace.Administrator.AdminPanels.AllRecn = Convert.ToInt32(CountTable) > 0 ? Convert.ToInt32(CountTable) : 0;
                StrCreate = "SELECT count(*) from REESTRBACK WHERE REESTRBACK.BACK = " + "'" + NameFolderBack + "'"; ;
                DeletQuery.UserQuery = string.Format(StrCreate, "REESTRBACK");
                string CountBack = DeletQuery.ExecuteUNIScalar() == null ? "" : DeletQuery.ExecuteUNIScalar().ToString();
                ConectoWorkSpace.Administrator.AdminPanels.CurrentBackCount = Convert.ToInt32(CountBack) > 0 ? Convert.ToInt32(CountBack) : 0;

                ConectoWorkSpace.Administrator.AdminPanels.LoadedGridBack("SELECT * from REESTRBACK");

                StrCreate = "DELETE from ACTIVBACKFRONT where ACTIVBACKFRONT.PUTH = " + "'" + BackPuth + "'";
                DBConecto.UniQuery DeletBackFront = new DBConecto.UniQuery(StrCreate, "FB");
                DeletBackFront.UserQuery = string.Format(StrCreate, "ACTIVBACKFRONT");
                DeletBackFront.ExecuteUNIScalar();
 
                DBConecto.DBcloseFBConectionMemory("FbSystem");
                // Удаление файлов в папке установки.
                if (BackPuth.Length != 0)
                {
                     BackPuth = BackPuth.Substring(0, BackPuth.IndexOf(@"\bin")+1);
                }
                if (System.IO.File.Exists(BackPuth + @"bin\aptune.ini"))
                {
                    Administrator.AdminPanels.ArgumentCmd = BackPuth;
                    ConectoWorkSpace.Administrator.AdminPanels.CallProgram = "UnInstallBackB52";
                    string TextMesage = "Сохранить текущую конфигурацию БекОфиса?";
                    ConectoWorkSpace.Administrator.AdminPanels.Inst2530 = "UnInstallBackB52";
                    ConectoWorkSpace.Administrator.AdminPanels.SetUpdateRestore = 0;
                    ConectoWorkSpace.Administrator.AdminPanels ConectoWorkSpace_InW = AppStart.LinkMainWindow("WAdminPanels");
                    WinOblakoSetUpdate OblakoSetWindow = new WinOblakoSetUpdate(TextMesage);
                    OblakoSetWindow.Owner = ConectoWorkSpace_InW;  //AddOwnedForm(OblakoNizWindow);
                    OblakoSetWindow.Top = ConectoWorkSpace_InW.Top + 190;
                    OblakoSetWindow.Left = ConectoWorkSpace_InW.ScrollViewerd.Width / OblakoSetWindow.Width / 2.5 * OblakoSetWindow.Width; //ConectoWorkSpace_InW.ScrollViewerd.Width - (OblakoSetWindow.Width * 2);
                    OblakoSetWindow.Show();

                }
                else InstallB52.DeleteBackB52("");

            }
        }

        public static void PackBackB52()
        {
            AppStart.RenderInfo Arguments01 = new AppStart.RenderInfo() { };
            Arguments01.argument1 = "2";
            Arguments01.argument2 = "";
            Thread thStartTimer01 = new Thread(PackBackB52TH);
            thStartTimer01.SetApartmentState(ApartmentState.STA);
            thStartTimer01.IsBackground = true; // Фоновый поток
            thStartTimer01.Start(Arguments01);
            IntThreadStart++;
        }

        public static void PackBackB52TH(object ThreadObj)
        {
            AppStart.RenderInfo arguments = (AppStart.RenderInfo)ThreadObj;
            string PuthSetBackUp = arguments.argument1;
            string PuthSetBackUp7z = @"c:\temp\" + Administrator.AdminPanels.ArgumentCmd.Substring(3, Administrator.AdminPanels.ArgumentCmd.LastIndexOf(@"\")-4) + ".7z";
            string Cmd = Administrator.AdminPanels.ArgumentCmd.Substring(0, Administrator.AdminPanels.ArgumentCmd.Length-1);
            if (System.IO.File.Exists(PuthSetBackUp7z)) System.IO.File.Delete(PuthSetBackUp7z);
            if (System.IO.File.Exists(@"C:\Program Files\7-Zip\7z.exe"))
            {
                Process pr = new Process();
                pr.StartInfo.FileName =  @"C:\Program Files\7-Zip\7z.exe"; //SystemConecto.PutchApp + @"Utils\7za\7za.exe";
                pr.StartInfo.Arguments = " a -tzip -ssw -mx3 -r0 " + PuthSetBackUp7z + "  " + Cmd;
                pr.StartInfo.UseShellExecute = false;
                pr.StartInfo.CreateNoWindow = true;
                pr.StartInfo.RedirectStandardOutput = true;
                pr.Start();
                pr.WaitForExit();
                pr.Close();
                
            }
            DeleteBackB52(PuthSetBackUp7z);
            IntThreadStart--;
        }
        public static void DeleteBackB52(string PuthSetBackUp7z)
        {
            if (Administrator.AdminPanels.ArgumentCmd.Length != 0)
            {
               //if(Directory.Exists(Administrator.AdminPanels.ArgumentCmd)) Directory.Delete(Administrator.AdminPanels.ArgumentCmd, true );
                if (PuthSetBackUp7z != "")
                {
                    //Directory.CreateDirectory(Administrator.AdminPanels.ArgumentCmd);
                    string ArhivFile = Administrator.AdminPanels.ArgumentCmd + PuthSetBackUp7z.Substring(PuthSetBackUp7z.LastIndexOf(@"\") + 1, PuthSetBackUp7z.Length - (PuthSetBackUp7z.LastIndexOf(@"\") + 1));
                    System.IO.File.Copy(PuthSetBackUp7z, ArhivFile);
                }
            }
 
            
            if (ConectoWorkSpace.Administrator.AdminPanels.AllRecn == 0)
            {
                ConectoWorkSpace.Administrator.AdminPanels.UpdateKeyReestr("InstBackOnOff", "");
                ConectoWorkSpace.Administrator.AdminPanels.UpdateKeyReestr("PatchSRBack", "");

            }
            ConectoWorkSpace.Administrator.AdminPanels.UpdateKeyReestr(Administrator.AdminPanels.NameObj, "0");
            if (ConectoWorkSpace.Administrator.AdminPanels.CurrentBackCount == 0)
            {
               System.Windows.Application.Current.Dispatcher.Invoke(new Action(delegate ()
               {

                    ConectoWorkSpace.Administrator.AdminPanels ConectoWorkSpace_Of = AppStart.LinkMainWindow("WAdminPanels");
                    var pic = (Image)LogicalTreeHelper.FindLogicalNode(ConectoWorkSpace_Of, Administrator.AdminPanels.NameObj);
                    var uriSource = new Uri(@"/Conecto®%20WorkSpace;component/Images/" + "on_off_1.png", UriKind.Relative);
                    pic.Source = new BitmapImage(uriSource);
                    if(Administrator.AdminPanels.NameObj == "BackOfRoznOnOff") ConectoWorkSpace_Of.AddBackRozn.Visibility = Visibility.Hidden;
                    if (Administrator.AdminPanels.NameObj == "BackOfRestoranOnOff") ConectoWorkSpace_Of.AddBackRestoran.Visibility = Visibility.Hidden;
                    if (Administrator.AdminPanels.NameObj == "BackOfFastFudOnOff") ConectoWorkSpace_Of.AddBackFast.Visibility = Visibility.Hidden;
                    if (Administrator.AdminPanels.NameObj == "BackOfFitnesOnOff") ConectoWorkSpace_Of.AddBackFithes.Visibility = Visibility.Hidden;
                    if (Administrator.AdminPanels.NameObj == "BackOfHotelOnOff") ConectoWorkSpace_Of.AddBackHotel.Visibility = Visibility.Hidden;
                    if (Administrator.AdminPanels.NameObj == "BackOfMixOnOff") ConectoWorkSpace_Of.AddBackMix.Visibility = Visibility.Hidden;
               }));
            }
 

        }
        /// <summary>
        /// Запуск на выполнение  БекОфис Б52
        /// </summary>
        public static void RunBackOfficeB52()
        {
            if (Convert.ToInt32(AppStart.TableReestr["BackOfServKeyOnOff"]) == 2)
            {
                // запуск сервера ключа.
                Process[] FB25 = Process.GetProcessesByName("grdsrv");
                if (FB25.Length == 0)
                {
                    InstallTHKeyServ();
                    Thread.Sleep(2000);
                }
            }
            if (Convert.ToInt32(AppStart.TableReestr[Administrator.AdminPanels.NameObj]) == 2)
            {
                AppStart.RenderInfo Arguments01 = new AppStart.RenderInfo() { };
                Arguments01.argument1 = "2";
                Arguments01.argument2 = "";
                Thread thStartTimer01 = new Thread(RunBackB52);
                thStartTimer01.SetApartmentState(ApartmentState.STA);
                thStartTimer01.IsBackground = true; // Фоновый поток
                thStartTimer01.Start(Arguments01);
                IntThreadStart++;
            }



        }

        public static void RunBackB52(object ThreadObj)
        {
 
            string NameBack = Administrator.AdminPanels.NameObj.Substring(6, Administrator.AdminPanels.NameObj.LastIndexOf("OnOff") - 6); 
            string PatchSR = Administrator.AdminPanels.PuthBack;
            string StrCreate = "select * from REESTRBACK where REESTRBACK.BACK = " + "'" + NameBack + "'";
            int Idcount = 0;
            if (Administrator.AdminPanels.PuthBack.Length == 0)
            { 
                DBConecto.DBopenFBConectionMemory(SystemBD.ConnnStringSystem().ToString(), "", "FbSystem");
                FbCommand SelectTable = new FbCommand(StrCreate, DBConecto.bdFbSystemConect);
                SelectTable.CommandType = CommandType.Text;
                FbDataReader ReadOutTable = SelectTable.ExecuteReader();
                while (ReadOutTable.Read())
                {
                    PatchSR = ReadOutTable[3].ToString();
                    Idcount = Idcount + 1;
                }
                ReadOutTable.Close(); 
                SelectTable.Dispose();
                DBConecto.DBcloseFBConectionMemory("FbSystem"); 
            }

            if (PatchSR.Length != 0) 
            {
                //string PatchSRExe = PatchSR + "B52BackOffice8.exe" ;
                Process mv_prcInstaller = new Process();
                mv_prcInstaller.StartInfo.FileName = PatchSR; 
                mv_prcInstaller.StartInfo.Arguments = "";
                mv_prcInstaller.Start();
                mv_prcInstaller.WaitForExit();
                mv_prcInstaller.Close();

            }
            else
            {
                if (!SystemConecto.File_(PatchSR, 5))
                {
                    Administrator.AdminPanels.NameObj = "";
                    var TextWindows = "Отсутствует файл B52BackOffice...exe  " + Environment.NewLine + " Запуск невозможен. ";
                    MessageErorInst(TextWindows);
                    return;
                }
            }

        }




        /// <summary>
        /// Установка FrontB52 розница 
        /// </summary>
        public static void InstallFrontB52()
        {
            SetCheckTTF16();

            AppStart.RenderInfo Arguments01 = new AppStart.RenderInfo() { };
            Arguments01.argument1 = "2";
            Arguments01.argument2 = "";
            Thread thStartTimer01 = new Thread(InstallFrontThB52);
            thStartTimer01.SetApartmentState(ApartmentState.STA);
            thStartTimer01.IsBackground = true; // Фоновый поток
            thStartTimer01.Start(Arguments01);
            IntThreadStart++;


        }

 

        public static void ContentBackFrontOff()
        {
            System.Windows.Application.Current.Dispatcher.Invoke(new Action(delegate ()
            {
                ConectoWorkSpace.Administrator.AdminPanels ConectoWorkSpace_Of = AppStart.LinkMainWindow("WAdminPanels");
                ConectoWorkSpace_Of.FrontPuhtBd.Content = "С алиасом на диске компьютера:";
                ConectoWorkSpace_Of.FrontPuhtSoft.Content = "Шаг 2. Установить путь размещения:";
                ConectoWorkSpace_Of.LabFrontOfNet.Content = "По пути на указанном компьютере:";
                ConectoWorkSpace_Of.LabFrontOfNet.Foreground = Brushes.Black;
                ConectoWorkSpace_Of.FrontPuhtSoft.Foreground = Brushes.Indigo;
                ConectoWorkSpace_Of.FrontPuhtBd.Foreground = Brushes.Black;
                ConectoWorkSpace_Of.SetBackOfPuth.Content = "С алиасом на диске компьютера:";
                ConectoWorkSpace_Of.SetBackOfPuthLoc.Content = "Шаг 2. Установить путь размещения:";
                ConectoWorkSpace_Of.LabBackOfNet.Content = "По пути на указанном компьютере:";
                ConectoWorkSpace_Of.SetBackOfPuth.Foreground = Brushes.Black;
                ConectoWorkSpace_Of.SetBackOfPuthLoc.Foreground = Brushes.Indigo;
                ConectoWorkSpace_Of.LabBackOfNet.Foreground = Brushes.Black;
                ConectoWorkSpace_Of.LabBackOfNet.Visibility = Visibility.Visible;
                ConectoWorkSpace_Of.LabFrontOfNet.Visibility = Visibility.Visible;
                ConectoWorkSpace_Of.SetPuthBdBackNet.Visibility = Visibility.Visible;
                ConectoWorkSpace_Of.SetPuthBdFrontNet.Visibility = Visibility.Visible;
                ConectoWorkSpace_Of.SetPuthBdBackNet.Visibility = Visibility.Visible;
            }));
        }

        public static void InstallFrontThB52(object ThreadObj)
        {
 
            if (Convert.ToInt32(AppStart.TableReestr["FrontFbd30OnOff"].ToString()) == 2 && Convert.ToInt32(AppStart.TableReestr["FrontFbd25OnOff"].ToString()) == 2)
            {
                ContentBackFrontOff();
                var TextWindows = "Одновременно установлены в системе Firebird_2_5 и Firebird_3_0." + Environment.NewLine + "Для установки Фронт Б52 необходмио выбрать один из двух серверов, ненужный выключить.";
                MessageErorInst(TextWindows);
                return;
            }
 
            if (Convert.ToInt32(AppStart.TableReestr["BackOfServKeyOnOff"]) == 2)
            { 
                Process[] FB25 = Process.GetProcessesByName("grdsrv");
                if (FB25.Length == 0)
                {
                    InstallTHKeyServ();
                    Thread.Sleep(2000);
                }           
            }


            string LenchPutch = Convert.ToInt32(AppStart.TableReestr["FrontFbd25OnOff"].ToString()) == 2 ? AppStart.TableReestr["PuthSetBD25"] : "";
            LenchPutch = Convert.ToInt32(AppStart.TableReestr["FrontFbd30OnOff"].ToString()) == 2 ? AppStart.TableReestr["PuthSetBD30"] : LenchPutch;
            if (Administrator.AdminPanels.SetWinSetHub == "SetNetTcpIpBd") LenchPutch = Administrator.AdminPanels.PathFileBDText;

            // Проверка наличия свободного пространства на диске куда будем ложить 
            string DeviceOn = LenchPutch.Substring(0, 3);
            DriveInfo di = new DriveInfo(DeviceOn);
            long Ffree = (di.TotalFreeSpace / 1024) / 1024;
            string MBFree = Ffree.ToString("#,##") + " MB";
            if (Ffree - 500 < 0)
            {
                ContentBackFrontOff();
                var TextWindows = "Для установки сервера FrontB52 необходимо свободное пространство 500mб на диске." + Environment.NewLine + "Текущее пространство " + MBFree + Environment.NewLine + "Выполнение процедуры остановлено. ";
                MessageErorInst(TextWindows);
                return;
            }

            if (Administrator.AdminPanels.FolderFrontPuth == "")
            {
                ContentBackFrontOff();
                var TextWindows = "Не выбрано устройство для размещения  FrontB52" + Environment.NewLine + "Выполнение процедуры остановлено.";
                MessageErorInst(TextWindows);
                return;
            }
            

            if (App.aSystemVirable["UserWindowIdentity"] == "1")
            {

                DriveInfo IdDevice = new DriveInfo("d:");
                string PuthFolderBack = "Front"+Administrator.AdminPanels.NameObj.Substring(0, Administrator.AdminPanels.NameObj.LastIndexOf("ClickOnOff"));
                string NameFolderBack = Administrator.AdminPanels.NameObj.Substring(0, Administrator.AdminPanels.NameObj.LastIndexOf("ClickOnOff") );
                string NameDir = Administrator.AdminPanels.FolderFrontPuth + PuthFolderBack+ @"\";
                string PatchSRBack = Administrator.AdminPanels.FolderFrontPuth + PuthFolderBack + @"\bin\";
                string PatchSR = Convert.ToInt32(AppStart.TableReestr["FrontFbd25OnOff"].ToString()) == 2 ? AppStart.TableReestr["ServerDefault25"] : AppStart.TableReestr["ServerDefault30"];

                string CurrentSetBD = Convert.ToInt32(AppStart.TableReestr["FrontFbd25OnOff"].ToString()) == 2 ?
                    "select * from SERVERACTIVFB25 where SERVERACTIVFB25.NAME = " + "'" + Administrator.AdminPanels.NameServer25 + "'" :
                    "select * from SERVERACTIVFB30 where SERVERACTIVFB30.NAME = " + "'" + Administrator.AdminPanels.NameServer30 + "'";
                DBConecto.DBopenFBConectionMemory(SystemBD.ConnnStringSystem().ToString(), "", "FbSystem");
                FbCommand ServerKey = new FbCommand(CurrentSetBD, DBConecto.bdFbSystemConect);
                FbDataReader ReaderFront = ServerKey.ExecuteReader();
                while (ReaderFront.Read()) { PatchSR = ReaderFront[1].ToString(); }
                ReaderFront.Close();
                ServerKey.Dispose();
 
                string NameDll = "fb2532.zip";
                string PuthSetBD = Administrator.AdminPanels.SelectPuth;
                string Hub = Administrator.AdminPanels.SelectAlias; // Convert.ToInt32(AppStart.TableReestr["FrontFbd25OnOff"].ToString()) == 2 ? "hub" : ":hub3";

                Administrator.AdminPanels.UpdateKeyReestr("PatchSRBack", PatchSRBack);
                NameDll = Convert.ToInt32(AppStart.TableReestr["FrontFbd25OnOff"].ToString()) == 2 && SystemConecto.OS64Bit ? "fb2532.zip" : NameDll; 
                NameDll = Convert.ToInt32(AppStart.TableReestr["FrontFbd30OnOff"].ToString()) == 2 && !SystemConecto.OS64Bit ? "fb3032.zip" : NameDll;
                NameDll = Convert.ToInt32(AppStart.TableReestr["FrontFbd30OnOff"].ToString()) == 2 && SystemConecto.OS64Bit ? "fb3032.zip" : NameDll;
                string InstFrontOnOff = Convert.ToInt32(AppStart.TableReestr["FrontFbd25OnOff"].ToString()) == 2 ? "BackFbd25OnOff" : "BackFbd30OnOff";
 
                // Список файлов
                string SetBack = "b52/Front/Roznica/";
                string NameFile = "B52FrontOffice8.zip";
                string SetFolderBd = "b52/base/";
                string SetNameBd = "hub.fbk";
                SetNameBd = Convert.ToInt32(AppStart.TableReestr["FrontFbd30OnOff"].ToString()) == 2 ? "hub3.fbk" : SetNameBd;
                if (ConectoWorkSpace.Administrator.AdminPanels.NameObj == "RestoranClickOnOff")
                {
                    SetBack = "b52/Front/Restoran/";
                    NameFile = "B52FrontOffice8.zip";
                }
                if (ConectoWorkSpace.Administrator.AdminPanels.NameObj == "FastFudClickOnOff")
                {
                    SetBack = "b52/Front/Fast/";
                    NameFile = "B52FrontOffice8.zip";
                }
                if (ConectoWorkSpace.Administrator.AdminPanels.NameObj == "FitnesClickOnOff")
                {
                    SetBack = "b52/Front/Fitnes/";
                    NameFile = "B52Fitness8.zip";
                    SetNameBd = Convert.ToInt32(AppStart.TableReestr["FrontFbd30OnOff"].ToString()) == 2 ? "fitnes3.fbk" : "fitnes.fbk";
                }

                if (ConectoWorkSpace.Administrator.AdminPanels.NameObj == "HotelClickOnOff")
                {
                    SetBack = "b52/Front/Hotel/";
                    NameFile = "B52Hotel8.zip";
                    SetNameBd = Convert.ToInt32(AppStart.TableReestr["FrontFbd30OnOff"].ToString()) == 2 ? "hotel3.fbk" : "hotel.fbk";
                }

                MasSetBackFront[0] = PatchSRBack; MasSetBackFront[1] = NameDir; MasSetBackFront[2] = "2"; MasSetBackFront[3] = InstFrontOnOff;
                MasSetBackFront[4] = NameDll; MasSetBackFront[5] = PatchSR; MasSetBackFront[6] = PuthSetBD; MasSetBackFront[7] = Hub;
                MasSetBackFront[8] = NameFolderBack; MasSetBackFront[9] = NameFile; MasSetBackFront[10] = SetBack;
                MasSetBackFront[11] = SetFolderBd; MasSetBackFront[12] = SetNameBd;

                if (Administrator.AdminPanels.FolderFrontPuth.LastIndexOf(@"\") > 2)
                {
                    Administrator.AdminPanels.Inst2530 = "Front";
                    string TextMesage = "Вы подтверждаете путь установки ФронтОфиса " + Environment.NewLine + PatchSRBack;
                    Administrator.AdminPanels.DeleteDefaultServer = 0;
                    System.Windows.Application.Current.Dispatcher.Invoke(new Action(delegate ()
                    {
                        ConectoWorkSpace.Administrator.AdminPanels ConectoWorkSpace_InW = AppStart.LinkMainWindow("WAdminPanels");
                        WinSetPuth SetPuth = new WinSetPuth(TextMesage);
                        SetPuth.Owner = ConectoWorkSpace_InW;
                        SetPuth.Top = (ConectoWorkSpace_InW.Top + 7) + ConectoWorkSpace_InW.Close_F.Margin.Top + (ConectoWorkSpace_InW.Close_F.Height - 2) + 170;
                        SetPuth.Left = (ConectoWorkSpace_InW.Left) + ConectoWorkSpace_InW.Close_F.Margin.Left + 300;
                        SetPuth.Show();


                    }));
                }
                else SetPuthFrontB52();
            }
            IntThreadStart--;
        }

        public static  void SetPuthFrontB52()
        {
                var TextFront = "Выполняется установка B52FrontOffice." + Environment.NewLine + "Пожалуйста подождите. ";
                int AutoClose = 1;
                int MesaggeTop = -170;
                int MessageLeft = 800;
                InstallB52.MessageEnd(TextFront, AutoClose, MesaggeTop, MessageLeft);

                if (!Directory.Exists(SystemConectoServers.PutchServer + @"tmp\FrontB52\")) Directory.CreateDirectory(SystemConectoServers.PutchServer + @"tmp\FrontB52\");
                Dictionary<string, string> fbembedList = new Dictionary<string, string>();
                fbembedList.Add(SystemConectoServers.PutchServer + @"tmp\FrontB52\" + MasSetBackFront[9], MasSetBackFront[10]);

                if (SystemConecto.IsFilesPRG(fbembedList, -1, "- Проверка файлов во время установки сервера " + MasSetBackFront[0]) != "True")
                {
                    ContentBackFrontOff();
                    var TextWindows = "Отсутствует инсталяционный файл B52FrontOffice8" + Environment.NewLine + "Установка прекращена.";
                    MessageErorInst(TextWindows);
                    ExitErrorInstalBackFront(2);
                    return;
                }
                // Распоковка
                Install.Extract.Unarch_arhive(SystemConectoServers.PutchServer + @"tmp\FrontB52\" + MasSetBackFront[9], MasSetBackFront[0]);
                //MessageBox.Show("Файл распакован");
                // проверка наличия файлов

                // Пвторная проверка

                string SetExeFront = MasSetBackFront[9].Substring(0, MasSetBackFront[9].LastIndexOf(".")+1)+"exe";
                if (!SystemConecto.File_(MasSetBackFront[0] + SetExeFront, 5))
                {
                    ContentBackFrontOff();
                    var TextWindows = "Отсутствует инсталяционный файл B52FrontOffice8.exe" + Environment.NewLine + "Установка прекращена.";
                    MessageErorInst(TextWindows);
                    ExitErrorInstalBackFront(2);
                    return;
                }

            // Поцедура завершения инсталяции Бек или Фронт Б52  2- Front
            //ReinstalFBD(PatchSRBack, NameDir, 2, InstFrontOnOff, NameDll, PatchSR, PuthSetBD, Hub, NameFolderBack, NameFile);
            ReinstalFBD();

        }
                
 

        // Деинсталяция Бек Офис Б52 розница
        public static void UnInstallFrontB52()
        {

            // тело деининсталяции
            // Права в ОС Win
            if (App.aSystemVirable["UserWindowIdentity"] == "1")
            {

                
                // Удаление файлов
                string BackPuth = "", BackConect = "", BackServer = "";
                string NameFolderBack = Administrator.AdminPanels.NameObj.Substring(0, Administrator.AdminPanels.NameObj.LastIndexOf("ClickOnOff") );
                int Idcount = 0;
                string StrCreate = "select * from REESTRFRONT where REESTRFRONT.FRONT = " + "'" + NameFolderBack + "'";
                DBConecto.DBopenFBConectionMemory(SystemBD.ConnnStringSystem().ToString(), "", "FbSystem");
                FbCommand SelectTable = new FbCommand(StrCreate, DBConecto.bdFbSystemConect);
                SelectTable.CommandTimeout = 3;
                SelectTable.CommandType = CommandType.Text;
                FbDataReader ReadTable = SelectTable.ExecuteReader();
                while (ReadTable.Read())
                {

                    BackConect = ReadTable[1].ToString();
                    BackServer = ReadTable[2].ToString();
                    BackPuth = ReadTable[3].ToString();
                    Idcount = Idcount + 1;
                }
                ReadTable.Close();
                StrCreate = "DELETE from REESTRFRONT where REESTRFRONT.FRONT = " + "'" + NameFolderBack + "'";
                DBConecto.UniQuery DeletQuery = new DBConecto.UniQuery(StrCreate, "FB");
                if (Idcount != 0)
                {
                    DeletQuery.UserQuery = string.Format(StrCreate, "REESTRFRONT");
                    DeletQuery.ExecuteUNIScalar();
                }
                StrCreate = "SELECT count(*) from REESTRFRONT";
                DeletQuery.UserQuery = string.Format(StrCreate, "REESTRFRONT");
                string CountTable = DeletQuery.ExecuteUNIScalar() == null ? "" : DeletQuery.ExecuteUNIScalar().ToString();
                int AllRecn = Convert.ToInt32(CountTable) > 0 ? Convert.ToInt32(CountTable) : 0;
                SelectTable.Dispose();
                DBConecto.DBcloseFBConectionMemory("FbSystem");
                ConectoWorkSpace.Administrator.AdminPanels.LoadedGridFront("SELECT * from REESTRFRONT");
                
                StrCreate = "DELETE from ACTIVBACKFRONT where ACTIVBACKFRONT.PUTH = " + "'" + BackPuth + "'";
                DBConecto.UniQuery DeletBackFront = new DBConecto.UniQuery(StrCreate, "FB");
                if (Idcount != 0)
                {
                    DeletBackFront.UserQuery = string.Format(StrCreate, "ACTIVBACKFRONT");
                    DeletBackFront.ExecuteUNIScalar();
                }
                DBConecto.DBcloseFBConectionMemory("FbSystem");

                // Удаление файлов в Фронте
                if (BackPuth.Length != 0)
                {
                    BackPuth = BackPuth.Substring(0, BackPuth.IndexOf(@"\bin"));

                    Administrator.AdminPanels.ArgumentCmd = BackPuth;
                    ConectoWorkSpace.Administrator.AdminPanels.CallProgram = "UnInstallFrontB52";
                    string TextMesage = "Сохранить текущую конфигурацию Фронт Офиса?";
                    ConectoWorkSpace.Administrator.AdminPanels.Inst2530 = "UnInstallFrontB52";
                    ConectoWorkSpace.Administrator.AdminPanels.SetUpdateRestore = 0;
                    ConectoWorkSpace.Administrator.AdminPanels ConectoWorkSpace_InW = AppStart.LinkMainWindow("WAdminPanels");
                    WinOblakoSetUpdate OblakoSetWindow = new WinOblakoSetUpdate(TextMesage);
                    OblakoSetWindow.Owner = ConectoWorkSpace_InW;
                    OblakoSetWindow.Top = ConectoWorkSpace_InW.Top + 190;
                    OblakoSetWindow.Left = ConectoWorkSpace_InW.ScrollViewerd.Width / OblakoSetWindow.Width / 2.5 * OblakoSetWindow.Width; //ConectoWorkSpace_InW.ScrollViewerd.Width - (OblakoSetWindow.Width * 2);
                    OblakoSetWindow.Show();

                }
                else
                {
                    ConectoWorkSpace.Administrator.AdminPanels.UpdateKeyReestr(Administrator.AdminPanels.NameObj, "0");
                    System.Windows.Application.Current.Dispatcher.Invoke(new Action(delegate ()
                    {
                        ConectoWorkSpace.Administrator.AdminPanels ConectoWorkSpace_Of = AppStart.LinkMainWindow("WAdminPanels");
                        var pic = (Image)LogicalTreeHelper.FindLogicalNode(ConectoWorkSpace_Of, Administrator.AdminPanels.NameObj);
                        var uriSource = new Uri(@"/Conecto®%20WorkSpace;component/Images/" + "on_off_1.png", UriKind.Relative);
                        pic.Source = new BitmapImage(uriSource);

                    }));
                }
  

  
 

            }
        }

        public static void PackFrontB52()
        {
            AppStart.RenderInfo Arguments01 = new AppStart.RenderInfo() { };
            Arguments01.argument1 = "2";
            Arguments01.argument2 = "";
            Thread thStartTimer01 = new Thread(PackFrontB52TH);
            thStartTimer01.SetApartmentState(ApartmentState.STA);
            thStartTimer01.IsBackground = true; // Фоновый поток
            thStartTimer01.Start(Arguments01);
            IntThreadStart++;
        }

        public static void PackFrontB52TH(object ThreadObj)
        {
            AppStart.RenderInfo arguments = (AppStart.RenderInfo)ThreadObj;
            string PuthSetBackUp = arguments.argument1;
            string PuthSetBackUp7z = @"c:\temp\" + Administrator.AdminPanels.ArgumentCmd.Substring(3, Administrator.AdminPanels.ArgumentCmd.LastIndexOf(@"\") - 4) + ".7z";
            string Cmd = Administrator.AdminPanels.ArgumentCmd.Substring(0, Administrator.AdminPanels.ArgumentCmd.Length - 1);
            if (System.IO.File.Exists(PuthSetBackUp7z)) System.IO.File.Delete(PuthSetBackUp7z);
            if (System.IO.File.Exists(@"C:\Program Files\7-Zip\7z.exe"))
            {
                Process pr = new Process();
                pr.StartInfo.FileName = @"C:\Program Files\7-Zip\7z.exe"; //SystemConecto.PutchApp + @"Utils\7za\7za.exe";
                pr.StartInfo.Arguments = " a -tzip -ssw -mx9 -r0 " + PuthSetBackUp7z + "  " + Cmd;
                pr.StartInfo.UseShellExecute = false;
                pr.StartInfo.CreateNoWindow = true;
                pr.StartInfo.RedirectStandardOutput = true;
                pr.Start();
                pr.WaitForExit();
                pr.Close();

            }
            DeleteFrontB52(PuthSetBackUp7z);
            IntThreadStart--;
        }
        public static void DeleteFrontB52(string PuthSetBackUp7z)
        {
            //Directory.Delete(Administrator.AdminPanels.ArgumentCmd, true);
            if (PuthSetBackUp7z != "")
            {
                //Directory.CreateDirectory(Administrator.AdminPanels.ArgumentCmd);
                string ArhivFile = Administrator.AdminPanels.ArgumentCmd + PuthSetBackUp7z.Substring(PuthSetBackUp7z.LastIndexOf(@"\") + 1, PuthSetBackUp7z.Length - (PuthSetBackUp7z.LastIndexOf(@"\") + 1));
                System.IO.File.Copy(PuthSetBackUp7z, ArhivFile);
            }

            if (ConectoWorkSpace.Administrator.AdminPanels.AllRecn == 0)
            {
                ConectoWorkSpace.Administrator.AdminPanels.UpdateKeyReestr("PatchSRFront", "");
                ConectoWorkSpace.Administrator.AdminPanels.UpdateKeyReestr("InstFrontOnOff", "");

            }
            ConectoWorkSpace.Administrator.AdminPanels.UpdateKeyReestr(Administrator.AdminPanels.NameObj, "0");
            System.Windows.Application.Current.Dispatcher.Invoke(new Action(delegate ()
            {
                ConectoWorkSpace.Administrator.AdminPanels ConectoWorkSpace_Of = AppStart.LinkMainWindow("WAdminPanels");
                var pic = (Image)LogicalTreeHelper.FindLogicalNode(ConectoWorkSpace_Of, Administrator.AdminPanels.NameObj);
                var uriSource = new Uri(@"/Conecto®%20WorkSpace;component/Images/" + "on_off_1.png", UriKind.Relative);
                pic.Source = new BitmapImage(uriSource);

            }));

        }



        // Поцедура завершения инсталяции Бек или Фронт Б52
        public static void ReinstalFBD()
        {

            if (ConectoWorkSpace.Administrator.AdminPanels.SetUpdateRestore == 4)
            {
                
                ContentBackFrontOff();
                System.Windows.Application.Current.Dispatcher.Invoke(new Action(delegate ()
                {
                    ConectoWorkSpace.Administrator.AdminPanels ConectoWorkSpace_Of = AppStart.LinkMainWindow("WAdminPanels");
                    var pic = (Image)LogicalTreeHelper.FindLogicalNode(ConectoWorkSpace_Of, Administrator.AdminPanels.NameObj);
                    var uriSource = new Uri(@"/Conecto®%20WorkSpace;component/Images/" + "on_off_1.png", UriKind.Relative);
                    pic.Source = new BitmapImage(uriSource);
                    ConectoWorkSpace.Administrator.AdminPanels.UpdateKeyReestr(Administrator.AdminPanels.NameObj, "0");

                }));
                return;
            }
            int BackFont = Convert.ToInt16(MasSetBackFront[2]);
            string PatchSRBack = MasSetBackFront[0], NameDir = MasSetBackFront[1], instBackFont = MasSetBackFront[3], NameDll = MasSetBackFront[4],
            PatchSR = MasSetBackFront[5], PuthSetBD = MasSetBackFront[6], Hub = MasSetBackFront[7], NameFolderBack = MasSetBackFront[8], NameFile = MasSetBackFront[9],
            SetFolderBd = MasSetBackFront[11], SetNameBd = MasSetBackFront[12] ;
            //string PatchSRBack, string NameDir, int BackFont, string instBackFont, string  string PatchSR, string PuthSetBD, string Hub, string NameFolderBack, string NameFile

            string PuthBackFont = BackFont == 1 ? @"tmp\BackB52\" : @"tmp\FrontB52\";
            if (!Directory.Exists(PuthBackFont)) Directory.CreateDirectory(PuthBackFont);
            Dictionary<string, string> fbembedList = new Dictionary<string, string>();
            fbembedList.Add(SystemConectoServers.PutchServer + PuthBackFont + NameDll, "b52/server_config/");
            if (SystemConecto.IsFilesPRG(fbembedList, -1, "- Проверка файлов во время установки сервера " + PuthBackFont + NameDll) != "True")
            {
                ContentBackFrontOff();
                var TextWindows = "Отсутствует файл библиотек клиента." + NameDll + Environment.NewLine + "Установка прекращена.";
                MessageErorInst(TextWindows);
                ExitErrorInstalBackFront(BackFont);
                return;
            }
            // Распоковка
            Install.Extract.Unarch_arhive(SystemConectoServers.PutchServer + PuthBackFont + NameDll, PatchSRBack);
            string PuthDir = SystemConecto.OS64Bit ? PatchSRBack : NameDir;
            System.IO.File.Copy(PatchSR + "firebird.msg", PuthDir + "firebird.msg", true);
            System.IO.File.Copy(PatchSR + "firebird.msg", NameDir + "firebird.msg", true);

            // формирование файла конфигурации Бак для Firebird_2_5 или Firebird_3_0
            string ServerPort = "", PuthBDfdb = "",  StrCreate ="";
            int Idcount = 0;
            if (Administrator.AdminPanels.BackPutchBD == "" || Administrator.AdminPanels.FrontPutchBD == "")
            {
  
                Administrator.AdminPanels.SetName25 = Administrator.AdminPanels.NameServer25 = AppStart.TableReestr["NameServer25"];
                Administrator.AdminPanels.SetName30 = Administrator.AdminPanels.NameServer30 = AppStart.TableReestr["NameServer30"];
                Hub = Administrator.AdminPanels.SelectAlias = SetNameBd.Substring(0, SetNameBd.LastIndexOf("."));
                SystemConecto.DIR_(PatchSRBack + @"database\");
                PuthSetBD = PatchSRBack + @"database\";
                PuthBDfdb = PatchSRBack + @"database\" + SetNameBd.Substring(0, SetNameBd.LastIndexOf("."))+".fdb";

                StrCreate = "select * from CONNECTIONBD25 where CONNECTIONBD25.ALIASBD = " + "'" + Hub + "'";
                if(Convert.ToInt32(AppStart.TableReestr["BackFbd30OnOff"].ToString()) == 2 && BackFont == 1) StrCreate = "select * from CONNECTIONBD30 where CONNECTIONBD30.ALIASBD = " + "'" + Hub + "'";
                Idcount = 0;
                DBConecto.DBopenFBConectionMemory(SystemBD.ConnnStringSystem().ToString(), "", "FbSystem");
                FbCommand BackFrontHub = new FbCommand(StrCreate, DBConecto.bdFbSystemConect);
                FbDataReader ReadOutTableHub = BackFrontHub.ExecuteReader();
                while (ReadOutTableHub.Read()) { Idcount ++; PuthBDfdb= ReadOutTableHub[0].ToString(); }
                ReadOutTableHub.Close();
                BackFrontHub.Dispose();
                DBConecto.DBcloseFBConectionMemory("FbSystem");
                if (Idcount != 0)
                {
                    if (BackFont == 1) Administrator.AdminPanels.BackPutchBD = PuthBDfdb;
                    if (BackFont == 2) Administrator.AdminPanels.FrontPutchBD = PuthBDfdb;
                    PuthSetBD = PuthBDfdb.Substring(0, PuthBDfdb.LastIndexOf(@"\"));
                }
 
 
                System.Windows.Application.Current.Dispatcher.Invoke(new Action(delegate ()
                {
                    ConectoWorkSpace.Administrator.AdminPanels ConectoWorkSpace_On = AppStart.LinkMainWindow("WAdminPanels");
                    ConectoWorkSpace_On.SetBackOfPuth.Content = "БД: " + PuthBDfdb;
                    ConectoWorkSpace_On.SetBackOfPuth.Foreground = Brushes.Green;
                }));

            }
            string ActivServer = ((BackFont == 1 && Convert.ToInt32(AppStart.TableReestr["BackFbd30OnOff"].ToString()) == 2) ||
               (BackFont == 2 && Convert.ToInt32(AppStart.TableReestr["FrontFbd30OnOff"].ToString()) == 2)) ? Administrator.AdminPanels.SetName30 : Administrator.AdminPanels.SetName25;
            string CurrentPasw = ((BackFont == 1 && Convert.ToInt32(AppStart.TableReestr["BackFbd30OnOff"].ToString()) == 2) ||
               (BackFont == 2 && Convert.ToInt32(AppStart.TableReestr["FrontFbd30OnOff"].ToString()) == 2)) ? Administrator.AdminPanels.CurrentPasswFb30 : Administrator.AdminPanels.CurrentPasswFb25;
            string ServerDefault = Administrator.AdminPanels.SetWinSetHub == "SetNetTcpIpBd" ? AppStart.TableReestr["NameServer25"] : Administrator.AdminPanels.NameServer25;
            if ((BackFont == 1 && Convert.ToInt32(AppStart.TableReestr["BackFbd30OnOff"].ToString()) == 2) ||
               (BackFont == 2 && Convert.ToInt32(AppStart.TableReestr["FrontFbd30OnOff"].ToString()) == 2)) ServerDefault = Administrator.AdminPanels.SetWinSetHub == "SetNetTcpIpBd" ? AppStart.TableReestr["NameServer30"] : Administrator.AdminPanels.NameServer30;
            StrCreate = "select * from SERVERACTIVFB25 where SERVERACTIVFB25.NAME = " + "'" + ServerDefault + "'";
            if((BackFont == 1 && Convert.ToInt32(AppStart.TableReestr["BackFbd30OnOff"].ToString()) == 2) ||
               (BackFont == 2 && Convert.ToInt32(AppStart.TableReestr["FrontFbd30OnOff"].ToString()) == 2)) StrCreate = "select * from SERVERACTIVFB30 where SERVERACTIVFB30.NAME = " + "'" + ServerDefault + "'";
            
            DBConecto.DBopenFBConectionMemory(SystemBD.ConnnStringSystem().ToString(), "", "FbSystem");
            FbCommand SelectTable = new FbCommand(StrCreate, DBConecto.bdFbSystemConect);
            SelectTable.CommandType = CommandType.Text;
            FbDataReader ReadTable = SelectTable.ExecuteReader();
            while (ReadTable.Read())
            {
                ServerPort = ReadTable[0].ToString();
                ActivServer = ReadTable[2].ToString();
                CurrentPasw = ReadTable[5].ToString();
                Idcount++;
            }
            ReadTable.Close();
            if (Idcount == 0)
            {
                ContentBackFrontOff();
                var TextWindows = "Не установлено имя сервера." + ServerPort + Environment.NewLine + "Установка прекращена.";
                MessageErorInst(TextWindows);
                ExitErrorInstalBackFront(BackFont);
                return;
            }
            SelectTable.Dispose();
            DBConecto.DBcloseFBConectionMemory("FbSystem");
            
            string BackFbd2530 = "BackFbd25OnOff";
            string TcpIp = AppStart.TableReestr["FrontAdrIp4"], IpName = AppStart.TableReestr["FrontAdrIp4"];     
            string PortHub = "/" + ServerPort + ":" + (Administrator.AdminPanels.SetWinSetHub == "SetNetTcpIpBd" ? PuthSetBD : Hub);
            string TcpPort = ServerPort; // AppStart.TableReestr["BackOfAdresPorta"];
            if (BackFont == 1) // Установка Back с введеным адресом серевера и порта Установка Бек
            {

                if (Administrator.AdminPanels.AdrIp4LenchBack.Length == 0)
                {
                    string Ip4 = TcpIp;
                    ConectoWorkSpace.Administrator.AdminPanels.UpdateKeyReestr("SetTextIp4BackOf", Ip4);
                    System.Windows.Application.Current.Dispatcher.Invoke(new Action(delegate ()
                    {
                        ConectoWorkSpace.Administrator.AdminPanels ConectoWorkSpace_On = AppStart.LinkMainWindow("WAdminPanels");
                        for (int indPoint = 1; indPoint <= 3; indPoint = indPoint + 1)
                        {
                            int position = Ip4.IndexOf(".");
                            if (position <= 0) { break; }
                            switch (indPoint)
                            {
                                case 1:
                                    ConectoWorkSpace_On.BackOfIp4Text1.Text = Ip4.Substring(0, position);
                                    break;
                                case 2:
                                    ConectoWorkSpace_On.BackOfIp4Text2.Text = Ip4.Substring(0, position);
                                    break;
                                case 3:
                                    ConectoWorkSpace_On.BackOfIp4Text3.Text = Ip4.Substring(0, position);
                                    break;
                            }
                            Ip4 = Ip4.Substring(position + 1);
                        }
                        ConectoWorkSpace_On.BackOfIp4Text4.Text = Ip4.Substring(0);

                    }));

                }
                else TcpIp = AppStart.TableReestr["SetTextIp4BackOf"];
                if(Convert.ToInt32(AppStart.TableReestr["BackFbd30OnOff"].ToString()) == 2) BackFbd2530 = "BackFbd30OnOff";

            }

            if (BackFont == 2) // Установка фронта с введеным адресом серевера и порта Установка Фронт
            {
                TcpIp = Administrator.AdminPanels.AdrIp4LenchFront.Length == 0 ? TcpIp : Administrator.AdminPanels.AdrIp4LenchFront;
                BackFbd2530 = Convert.ToInt32(AppStart.TableReestr["FrontFbd25OnOff"].ToString()) == 2 ? "FrontFbd25OnOff" : "FrontFbd30OnOff";
                TcpPort = AppStart.TableReestr["AdresPortFront"];
            }
            string NameExe = NameFile.Substring(0, NameFile.LastIndexOf(".")) + ".exe";
            System.IO.File.Delete(PatchSRBack + "conecto.ini");
            string StrBackFront = BackFont == 2 ? "Front" : "Back";
            string AliasSrever = ServerDefault;
            var Change = new ConectoWorkSpace.INI(PatchSRBack + "conecto.ini");
            Change.Set(StrBackFront, BackFbd2530, true);
            Change.Set("Name", Administrator.AdminPanels.NameObj, true);
            Change.Set("NameServer", AliasSrever, true);
            Change.Set("NameExe", PatchSRBack + NameExe, true);
            // Функция модификации  соединения в файле aptune.ini для Бек Офиса
            Idcount = 0;
            Encoding code = Encoding.Default;
            string[] fileLines = System.IO.File.ReadAllLines(PatchSRBack + "aptune.ini", code);
            foreach (string x in fileLines)
            {
                if (x.Contains("DatabaseName") == true )fileLines[Idcount] = "DatabaseName =" + TcpIp + PortHub; 
                Idcount++;
            }
            System.IO.File.WriteAllLines(PatchSRBack + "aptune.ini", fileLines, code);

            if ((Administrator.AdminPanels.BackPutchBD == "" && BackFont == 1) || (Administrator.AdminPanels.FrontPutchBD == "" && BackFont == 2))
            { 
                 // установка бд по умолчанию.
                fbembedList = new Dictionary<string, string>();
                // запись БД в папку 

                fbembedList.Add(PuthSetBD + SetNameBd, "b52/base/");
                if (SystemConecto.IsFilesPRG(fbembedList, -1, "- Проверка файлов во время установки БД " + PatchSRBack + @"database\") != "True")
                {
                    var TextWindows = "Отсутствует  файл БД " + SetNameBd + Environment.NewLine + "Установка прекращена ";
                    ErrInstallSetBD25(TextWindows);
                    return;
                }

                // вставить новую запись с описанием БД для бека или фронта.
                if ((BackFont == 1 && Convert.ToInt32(AppStart.TableReestr["BackFbd25OnOff"].ToString()) == 2) ||
                (BackFont == 2 && Convert.ToInt32(AppStart.TableReestr["FrontFbd25OnOff"].ToString()) == 2)) Administrator.AdminPanels.InsertConect(PuthBDfdb, ServerDefault);

                if ((BackFont == 1 && Convert.ToInt32(AppStart.TableReestr["BackFbd30OnOff"].ToString()) == 2) ||
                (BackFont == 2 && Convert.ToInt32(AppStart.TableReestr["FrontFbd30OnOff"].ToString()) == 2)) Administrator.AdminPanels.InsertConect30(PuthBDfdb, ServerDefault);



                string hubdate = TcpIp + PortHub;
                string RunGbak = PatchSR + @"bin\gbak.exe";
                string FileAlias = PatchSR + "aliases.conf";
                Administrator.AdminPanels.ImageObj = "25";
                if ((Convert.ToInt32(AppStart.TableReestr["BackFbd30OnOff"].ToString()) == 2 && BackFont == 1)|| (Convert.ToInt32(AppStart.TableReestr["FrontFbd30OnOff"].ToString()) == 2 && BackFont == 2))
                {
                    FileAlias = PatchSR + "databases.conf";
                    RunGbak = PatchSR + @"gbak.exe";
                    Administrator.AdminPanels.ImageObj = "30";
                }

                Idcount = 0;
                int Idcountout = 0;               
                var GchangeaAliases25 = INI.ReadFile(FileAlias);
                GchangeaAliases25.Set(Administrator.AdminPanels.SelectAlias, PuthBDfdb, true);
                fileLines = System.IO.File.ReadAllLines(FileAlias);
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
                System.IO.File.WriteAllLines(FileAlias, fileoutLines);
                RestartFB25(PatchSR, ActivServer);


                string ArgumentCmd = @" -rep " + PuthSetBD + SetNameBd +"  "+ hubdate + @" -v -bu 200 -user sysdba -pass " + CurrentPasw; //-y c:\temp\log.txt

                Process CmdDos = new Process();
                CmdDos.StartInfo.FileName = RunGbak; //  @"C:\Program Files\Conecto\Servers\Firebird_2_5\bin\gbak.exe";   // PatchSR + @"bin\gbak.exe";
                CmdDos.StartInfo.Arguments = ArgumentCmd; // @"-c c:\temp\hub.fbk 127.0.0.1/3055:hub  -user sysdba -pass masterkey";
                CmdDos.StartInfo.UseShellExecute = false;
                //CmdDos.StartInfo.CreateNoWindow = true;
                //CmdDos.StartInfo.RedirectStandardOutput = true;
                CmdDos.Start();
                CmdDos.WaitForExit();
                CmdDos.Close();          
            }
 
            System.Windows.Application.Current.Dispatcher.Invoke(new Action(delegate ()
            {
                ConectoWorkSpace.Administrator.AdminPanels ConectoWorkSpace_On = AppStart.LinkMainWindow("WAdminPanels");
 
                var pic = (Image)LogicalTreeHelper.FindLogicalNode(ConectoWorkSpace_On, Administrator.AdminPanels.NameObj);
                var uriSource = new Uri(@"/Conecto®%20WorkSpace;component/Images/" + "on_off_2.png", UriKind.Relative);
                pic.Source = new BitmapImage(uriSource);

            }));
            ConectoWorkSpace.Administrator.AdminPanels.UpdateKeyReestr(Administrator.AdminPanels.NameObj, "2");
            ConectoWorkSpace.Administrator.AdminPanels.UpdateKeyReestr(instBackFont.Substring(0, 4) == "Back" ? "InstBackOnOff" : "InstFrontOnOff", instBackFont);
 
 
            StrCreate = "select * from REESTRFRONT where REESTRFRONT.CONECT = " + "'" + PatchSRBack + NameExe + "'";
            if (BackFont == 1) StrCreate = "select * from REESTRBACK where REESTRBACK.PUTH = " + "'" + PatchSRBack + NameExe +  "'";
            string TempNameBack = "", KeyGnclient = "";
            Idcount = 0;
            DBConecto.DBopenFBConectionMemory(SystemBD.ConnnStringSystem().ToString(), "", "FbSystem");
            FbCommand BackFront = new FbCommand(StrCreate, DBConecto.bdFbSystemConect);
            FbDataReader ReadOutTable = BackFront.ExecuteReader();
            while (ReadOutTable.Read())
            {
                TempNameBack = ReadOutTable[2].ToString();
                Idcount = Idcount + 1;
            }
            ReadOutTable.Close();
            if (Idcount == 0)
            {

                string UpdateTime = "";
                StrCreate = (BackFont == 2 ? "INSERT INTO REESTRFRONT  values ('" : "INSERT INTO REESTRBACK  values ('");
                StrCreate = StrCreate + NameFolderBack + "','" + TcpIp + PortHub + "'" + ", '" + AliasSrever + "', '" + PatchSRBack+NameExe + "', '" + KeyGnclient + "', '" + UpdateTime+"')";
                DBConecto.UniQuery CountQuery = new DBConecto.UniQuery(StrCreate, "FB");
                CountQuery.UserQuery = string.Format(StrCreate, (BackFont == 2 ? "REESTRFRONT" : "REESTRBACK"));
                CountQuery.ExecuteUNIScalar();
 
            }
            BackFront.Dispose();
            StrCreate = "select * from ACTIVBACKFRONT where ACTIVBACKFRONT.PUTH = " + "'" + PatchSRBack + NameExe + "'";
            Idcount = 0;
            FbCommand SelectTableActiv = new FbCommand(StrCreate, DBConecto.bdFbSystemConect);
            FbDataReader ReadOutTableActiv = SelectTableActiv.ExecuteReader();
            while (ReadOutTableActiv.Read()) { Idcount++; }
            ReadOutTableActiv.Close();
            if (Idcount == 0)
            {
                string TcpIpPortHub = TcpIp + PortHub;
   
                StrCreate = "INSERT INTO ACTIVBACKFRONT  values ('Back','" + NameFolderBack + "'" + ", '" + AliasSrever + "', '" + TcpIpPortHub + "', '" + PortHub + "', '" + TcpIp + "', '" + PatchSRBack + NameExe + "')";
                DBConecto.UniQuery InsertQuery = new DBConecto.UniQuery(StrCreate, "FB");
                InsertQuery.UserQuery = string.Format(StrCreate, "ACTIVBACKFRONT");
                InsertQuery.ExecuteUNIScalar();
            }
            SelectTable.Dispose();
            SelectTableActiv.Dispose();
            DBConecto.DBcloseFBConectionMemory("FbSystem");

            if (BackFont == 1)
            {
                System.Windows.Application.Current.Dispatcher.Invoke(new Action(delegate ()
                {
                    ConectoWorkSpace.Administrator.AdminPanels ConectoWorkSpace_On = AppStart.LinkMainWindow("WAdminPanels");
                    if (Administrator.AdminPanels.NameObj == "BackOfRoznOnOff") ConectoWorkSpace_On.AddBackRozn.Visibility = Visibility.Visible;
                    if (Administrator.AdminPanels.NameObj == "BackOfRestoranOnOff") ConectoWorkSpace_On.AddBackRestoran.Visibility = Visibility.Visible;
                    if (Administrator.AdminPanels.NameObj == "BackOfFastFudOnOff") ConectoWorkSpace_On.AddBackFast.Visibility = Visibility.Visible;
                    if (Administrator.AdminPanels.NameObj == "BackOfFitnesOnOff") ConectoWorkSpace_On.AddBackFithes.Visibility = Visibility.Visible;
                    if (Administrator.AdminPanels.NameObj == "BackOfHotelOnOff") ConectoWorkSpace_On.AddBackHotel.Visibility = Visibility.Visible;
                    if (Administrator.AdminPanels.NameObj == "BackOfMixOnOff") ConectoWorkSpace_On.AddBackMix.Visibility = Visibility.Visible;
                }));
                Administrator.AdminPanels.BackPutchBD = "";
                ConectoWorkSpace.Administrator.AdminPanels.LoadedGridBack("SELECT * from REESTRBACK");

            }
            if (BackFont == 2)
            {
                Administrator.AdminPanels.FrontPutchBD = "";
                ConectoWorkSpace.Administrator.AdminPanels.LoadedGridFront( "SELECT * from REESTRFRONT");
            } 
            ContentBackFrontOff();
        }

        public static void BackFrontIsEnabledTrue(int BackFont)
        {
            System.Windows.Application.Current.Dispatcher.Invoke(new Action(delegate ()
            {
                ConectoWorkSpace.Administrator.AdminPanels ConectoWorkSpace_On = AppStart.LinkMainWindow("WAdminPanels");
                if (BackFont == 1)
                {
                    ConectoWorkSpace_On.BackOfRoznOnOff.IsEnabled = true;
                    ConectoWorkSpace_On.BackOfRestoranOnOff.IsEnabled = true;
                    ConectoWorkSpace_On.BackOfFastFudOnOff.IsEnabled = true;
                    ConectoWorkSpace_On.BackOfFitnesOnOff.IsEnabled = true;
                    ConectoWorkSpace_On.BackOfHotelOnOff.IsEnabled = true;
                    ConectoWorkSpace_On.BackOfMixOnOff.IsEnabled = true;
                    ConectoWorkSpace_On.AddBackRozn.IsEnabled = true;
                    ConectoWorkSpace_On.AddBackRestoran.IsEnabled = true;
                    ConectoWorkSpace_On.AddBackFast.IsEnabled = true;
                    ConectoWorkSpace_On.AddBackFithes.IsEnabled = true;
                    ConectoWorkSpace_On.AddBackHotel.IsEnabled = true;
                    ConectoWorkSpace_On.AddBackMix.IsEnabled = true;
                }
                
               if (BackFont == 2)
               {
                    ConectoWorkSpace_On.RoznClickOnOff.IsEnabled = true;
                    ConectoWorkSpace_On.RestoranClickOnOff.IsEnabled = true;
                    ConectoWorkSpace_On.FastFudClickOnOff.IsEnabled = true;
                    ConectoWorkSpace_On.FitnesClickOnOff.IsEnabled = true;
                    ConectoWorkSpace_On.HotelClickOnOff.IsEnabled = true;
 
                }
            }));
        }


        public static void ExitErrorInstalBackFront(int BackFont)
        {
            System.Windows.Application.Current.Dispatcher.Invoke(new Action(delegate ()
            {
                if (BackFont == 1) UnInstallBackB52();
                if (BackFont == 2) UnInstallFrontB52();
                ConectoWorkSpace.Administrator.AdminPanels ConectoWorkSpace_On = AppStart.LinkMainWindow("WAdminPanels");
                ConectoWorkSpace_On.SetBackOfPuth.Foreground = Brushes.Black;
                ConectoWorkSpace_On.SetBackOfPuthLoc.Foreground = Brushes.Black;
                ConectoWorkSpace_On.FrontPuhtBd.Foreground = Brushes.Black;
                ConectoWorkSpace_On.FrontPuhtSoft.Foreground = Brushes.Black;
                Administrator.AdminPanels.SelectPuth = "";
                Administrator.AdminPanels.FolderFrontPuth = "";
                Administrator.AdminPanels.FolderBackPuth = "";
                

            }));
        }
        // процедура вывода сообщений об ошибках инсталяции
        public static void MessageErorInst(string TextWindows)
        {
            System.Windows.Application.Current.Dispatcher.Invoke(new Action(delegate ()
            {
                ConectoWorkSpace.Administrator.AdminPanels ConectoWorkSpace_InW = AppStart.LinkMainWindow("WAdminPanels");
                //MainWindow ConectoWorkSpace_InW = AppStart.LinkMainWindow();
                int AutoClose = 1;
                Window WinOblakoVerh_Info = new WinMessage(TextWindows, AutoClose, 0); // создаем AutoClose
                WinOblakoVerh_Info.Owner = ConectoWorkSpace_InW;
                WinOblakoVerh_Info.Top = ConectoWorkSpace_InW.Top+250; 
                //WinOblakoVerh_Info.Left = ConectoWorkSpace_InW.Left + 650;
                WinOblakoVerh_Info.Left = ConectoWorkSpace_InW.ScrollViewerd.Width - (WinOblakoVerh_Info.Width * 2)-150;
                WinOblakoVerh_Info.ShowActivated = false;
                WinOblakoVerh_Info.Show();

            }));
            if (Administrator.AdminPanels.NameObj.Length != 0)
            {
                System.Windows.Application.Current.Dispatcher.Invoke(new Action(delegate ()
                {
                    ConectoWorkSpace.Administrator.AdminPanels ConectoWorkSpace_Of = AppStart.LinkMainWindow("WAdminPanels");
                    var pic = (Image)LogicalTreeHelper.FindLogicalNode(ConectoWorkSpace_Of, Administrator.AdminPanels.NameObj);
                    var uriSource = new Uri(@"/Conecto®%20WorkSpace;component/Images/" + "on_off_1.png", UriKind.Relative);
                    pic.Source = new BitmapImage(uriSource);

                }));
                ConectoWorkSpace.Administrator.AdminPanels.UpdateKeyReestr(Administrator.AdminPanels.NameObj, "0");
            }

 

        }



        /// <summary>
        /// Запуск на выполнение  БекОфис Б52
        /// </summary>
        public static void RunFrontOficceB52()
        {

            if (Convert.ToInt32(AppStart.TableReestr["BackOfServKeyOnOff"]) == 2)
            {
                // запуск сервера ключа.
                Process[] FB25 = Process.GetProcessesByName("grdsrv");
                if (FB25.Length == 0)
                {
                    InstallTHKeyServ();
                    Thread.Sleep(2000);
                }
            }

            if (Convert.ToInt32(AppStart.TableReestr[Administrator.AdminPanels.ImageObj]) == 2)
            {
                Administrator.AdminPanels.SetProcesRun = 1;
                AppStart.RenderInfo Arguments01 = new AppStart.RenderInfo() { };
                Arguments01.argument1 = "2";
                Arguments01.argument2 = "";
                Thread thStartTimer01 = new Thread(RunFrontThB52);
                thStartTimer01.SetApartmentState(ApartmentState.STA);
                thStartTimer01.IsBackground = true; // Фоновый поток
                thStartTimer01.Start(Arguments01);
                IntThreadStart++;
            }
 


        }

        public static void RunFrontThB52(object ThreadObj)
        {
 
            string NameFront = Administrator.AdminPanels.NameObj.Substring(5,Administrator.AdminPanels.NameObj.Length-5);
            string PatchSR = "";
            string StrCreate = "select * from REESTRFRONT where REESTRFRONT.FRONT = " + "'" + NameFront + "'";
            int Idcount = 0;
            DBConecto.DBopenFBConectionMemory(SystemBD.ConnnStringSystem().ToString(), "", "FbSystem");
            FbCommand SelectTable = new FbCommand(StrCreate, DBConecto.bdFbSystemConect);
            FbDataReader ReadOutTable = SelectTable.ExecuteReader();
            while (ReadOutTable.Read())
            {
                PatchSR = ReadOutTable[3].ToString();
                Idcount = Idcount + 1;
            }
            ReadOutTable.Close();
            if (Idcount != 0)
            {
                string PatchSRExe = PatchSR; 
                Process mv_prcInstaller = new Process();
                mv_prcInstaller.StartInfo.FileName = PatchSRExe;
                mv_prcInstaller.StartInfo.Arguments = "";
                mv_prcInstaller.Start();
                mv_prcInstaller.WaitForExit();
                mv_prcInstaller.Close();

            }
            else
            {
                if (!SystemConecto.File_(PatchSR, 5))
                {
                    Administrator.AdminPanels.NameObj = "";
                    var TextWindows = "Отсутствует файл B52Front...exe  " + Environment.NewLine + " Запуск невозможен. ";
                    MessageErorInst(TextWindows);
                    return;
                }
            }
            SelectTable.Dispose();
            DBConecto.DBcloseFBConectionMemory("FbSystem");
        }


        // Процедура создания архива текущей БД  
        public static void BackUpRunTH(object ThreadObj)
        {
            AppStart.RenderInfo arguments = (AppStart.RenderInfo)ThreadObj;
            string PuthSetBackUp = arguments.argument1;
            string DirBackupFdb = arguments.argument2;
            string SatellitePutch = arguments.argument3;
            string CurrentPassw = arguments.argument4;
            string HubdateSatellite = arguments.argument5;
            string ListBackUp = PuthSetBackUp.Substring(0, PuthSetBackUp.LastIndexOf(@"\") + 1);
            string str6 = AppStart.TableReestr["SetDirBackUp25"] + "security2back.fdb";
            string str7 = PuthSetBackUp.Substring(0, PuthSetBackUp.IndexOf(".")) + ".7z";
            if (Administrator.AdminPanels.SetWinSetHub == "BackUpFTP_Fbd25")
            {
                if (!System.IO.File.Exists(DirBackupFdb + "security2back.fdb"))
                { System.IO.File.Copy(Administrator.AdminPanels.CurrentSereverPuth + "security2.fdb", DirBackupFdb + "security2back.fdb"); }
                ListBackUp = DirBackupFdb;
            }
            if (Administrator.AdminPanels.SetWinSetHub == "BackUpFTP_Fbd30")
            {
                if (!System.IO.File.Exists(DirBackupFdb + "security3back.fdb"))
                { System.IO.File.Copy(Administrator.AdminPanels.CurrentSereverPuth + "security3.fdb", DirBackupFdb + "security3back.fdb"); }
                ListBackUp = DirBackupFdb;
            }
            if (Administrator.AdminPanels.SetWinSetHub == "BackUpLoc_Fbd25")
            {
                if (!System.IO.File.Exists(AppStart.TableReestr["SetDirBackUp25"] + "security2back.fdb"))
                { System.IO.File.Copy(Administrator.AdminPanels.CurrentSereverPuth + "security2.fdb", AppStart.TableReestr["SetDirBackUp25"] + "security2back.fdb"); }

            }
            if (Administrator.AdminPanels.SetWinSetHub == "BackUpLoc_Fbd30")
            {
                if (!System.IO.File.Exists(AppStart.TableReestr["SetDirBackUp30"] + "security3back.fdb"))
                { System.IO.File.Copy(Administrator.AdminPanels.CurrentSereverPuth + "security3.fdb", AppStart.TableReestr["SetDirBackUp30"] + "security3back.fdb"); }

            }

            SystemConecto.ErorDebag(Administrator.AdminPanels.RunGbak + Administrator.AdminPanels.ArgumentCmd, 1, 2);
            Administrator.AdminPanels.RunProcess(Administrator.AdminPanels.RunGbak, Administrator.AdminPanels.ArgumentCmd);

            if (SatellitePutch !="")
            {

                Administrator.AdminPanels.ArgumentCmd = "-b " + HubdateSatellite + " " + SatellitePutch + @" -v  -user sysdba -pass " + CurrentPassw;
                SystemConecto.ErorDebag(Administrator.AdminPanels.RunGbak + Administrator.AdminPanels.ArgumentCmd, 1, 2);
                Administrator.AdminPanels.RunProcess(Administrator.AdminPanels.RunGbak, Administrator.AdminPanels.ArgumentCmd);

            }

            string PuthSetBackUp7z = PuthSetBackUp.Substring(0, PuthSetBackUp.IndexOf(".")) + ".7z";
            //string StrArguments = " a " + PuthSetBackUp7z + " " + ListBackUp;
            //string Run7z = SystemConecto.PutchApp + @"Utils\7za\" + "7za.exe";
            //Administrator.AdminPanels.RunProcess(Run7z, StrArguments);

            if (System.IO.File.Exists("c:\\Program Files (x86)\\7-Zip\\7z.exe") || System.IO.File.Exists((string)SystemConecto.PutchApp + "Utils\\7za\\7za.exe"))
            {
                string CmdArguments1 = " a " + str7 + " " + PuthSetBackUp;
                string CmdRun = System.IO.File.Exists("c:\\Program Files (x86)\\7-Zip\\7z.exe") ? "c:\\Program Files (x86)\\7-Zip\\7z.exe" : (string)SystemConecto.PutchApp + "Utils\\7za\\7za.exe";
                Administrator.AdminPanels.RunProcess(CmdRun, CmdArguments1, "");
                string CmdArguments2 = " a " + str7 + " " + str6;
                Administrator.AdminPanels.RunProcess(CmdRun, CmdArguments2, "");
                if (SatellitePutch != "")
                {
                    string CmdArguments3 = " a " + str7 + " " + SatellitePutch;
                    Administrator.AdminPanels.RunProcess(CmdRun, CmdArguments3, "");
                }
                if (Administrator.AdminPanels.SetWinSetHub == "BackUpLoc_Fbd30" || Administrator.AdminPanels.SetWinSetHub == "BackUpLoc_Fbd25")
                {
                    string str8 = "SELECT * FROM SCHEDULEARHIV WHERE SCHEDULEARHIV.PUTH = '" + str7 + "'";
                    int num = 0;
                    DBConecto.DBopenFBConectionMemory(SystemBD.ConnnStringSystem("Dynamic").ToString(), "", "FbSystem");
                    FbCommand fbCommand = new FbCommand(str8, (FbConnection)DBConecto.bdFbSystemConect);
                    fbCommand.CommandType = CommandType.Text;
                    FbDataReader fbDataReader = fbCommand.ExecuteReader();
                    while (fbDataReader.Read())
                        ++num;
                    fbDataReader.Close();
                    string str9 = Administrator.AdminPanels.NameServer25;
                    if (Administrator.AdminPanels.SetWinSetHub == "BackUpLoc_Fbd30")
                        str9 = Administrator.AdminPanels.NameServer30;
                    if (num == 0)
                        str8 = "INSERT INTO SCHEDULEARHIV  values ('" + Administrator.AdminPanels.SelectPuth + "','" + PuthSetBackUp + "', '', '', '" + str9 + "')";
                    DBConecto.UniQuery uniQuery = new DBConecto.UniQuery(str8, "FB");
                    uniQuery.ExecuteUNIScalar("");
                    DBConecto.DBcloseFBConectionMemory("FbSystem");

                    System.Windows.Application.Current.Dispatcher.Invoke(new Action(delegate ()
                    {
                        ConectoWorkSpace.Administrator.AdminPanels ConectoWorkSpace_InW = AppStart.LinkMainWindow("WAdminPanels");
                        ConectoWorkSpace_InW.DateArhivBd30(25);
                    }));


                }
            }


            string DelDirBuckUp = DirBackupFdb.Substring(0, DirBackupFdb.LastIndexOf(@"\"));
            if (Administrator.AdminPanels.SetWinSetHub == "BackUpFTP_Fbd25" || Administrator.AdminPanels.SetWinSetHub == "BackUpFTP_Fbd30")
            {

                if (AppStart.ConnectionAvailable())
                {
 
                    // СПроверка наличия папки на фтп сервере

                    WebRequest ListDir = WebRequest.Create("ftp://conecto.ua/pack/");
                    ListDir.Method = WebRequestMethods.Ftp.ListDirectory;
                    ListDir.Credentials = new NetworkCredential(AppStart.aParamApp["ServerUpdateConecto_USER"], AppStart.aParamApp["ServerUpdateConecto_USER-Passw"]);
                    var RunDirBackUp = (FtpWebResponse)ListDir.GetResponse();
                    Stream responseStream = RunDirBackUp.GetResponseStream();
                    StreamReader reader = new StreamReader(responseStream);
                    string List = reader.ReadToEnd();

                    if (!List.Contains("\r\nBackUp"))
                    {
                        // Создать папку на фтп

                        WebRequest CreateDir = WebRequest.Create("ftp://conecto.ua/pack/BackUp/");
                        CreateDir.Method = WebRequestMethods.Ftp.MakeDirectory;
                        CreateDir.Credentials = new NetworkCredential(AppStart.aParamApp["ServerUpdateConecto_USER"], AppStart.aParamApp["ServerUpdateConecto_USER-Passw"]);
                        var CreateDirBackUp = (FtpWebResponse)CreateDir.GetResponse();

                        // Создать папку на фтп
                        WebRequest DirBackUpBd = WebRequest.Create("ftp://conecto.ua/pack/BackUp/bd/");
                        DirBackUpBd.Method = WebRequestMethods.Ftp.MakeDirectory;
                        DirBackUpBd.Credentials = new NetworkCredential(AppStart.aParamApp["ServerUpdateConecto_USER"], AppStart.aParamApp["ServerUpdateConecto_USER-Passw"]);
                        var RunDirBackUpBd = (FtpWebResponse)DirBackUpBd.GetResponse();

                    }

                    WebRequest ListDirBackUp = WebRequest.Create("ftp://conecto.ua/pack/BackUp/");
                    ListDirBackUp.Method = WebRequestMethods.Ftp.ListDirectory;
                    ListDirBackUp.Credentials = new NetworkCredential(AppStart.aParamApp["ServerUpdateConecto_USER"], AppStart.aParamApp["ServerUpdateConecto_USER-Passw"]);
                    var RunDirBd = (FtpWebResponse)ListDirBackUp.GetResponse();
                    Stream RunDirBdStream = RunDirBd.GetResponseStream();
                    StreamReader readerRunDirBd = new StreamReader(RunDirBdStream);
                    string ListDirBd = readerRunDirBd.ReadToEnd();

                    if (!ListDirBd.Contains("\r\nbd"))
                    {
                        // Создать папку на фтп
                        WebRequest DirBackUpBd = WebRequest.Create("ftp://conecto.ua/pack/BackUp/bd/");
                        DirBackUpBd.Method = WebRequestMethods.Ftp.MakeDirectory;
                        DirBackUpBd.Credentials = new NetworkCredential(AppStart.aParamApp["ServerUpdateConecto_USER"], AppStart.aParamApp["ServerUpdateConecto_USER-Passw"]);
                        var RunDirBackUpBd = (FtpWebResponse)DirBackUpBd.GetResponse();

                    }



                    //Запись на фтп сервер файла любого типа.
                    string FileName = PuthSetBackUp7z.Substring(PuthSetBackUp7z.LastIndexOf(@"\") + 1, (PuthSetBackUp7z.Length - (PuthSetBackUp7z.LastIndexOf(@"\") + 1)));
     
                    FtpWebRequest ftpClient = (FtpWebRequest)FtpWebRequest.Create("ftp://conecto.ua/pack/BackUp/bd/" + FileName);
                    ftpClient.Credentials = new System.Net.NetworkCredential(AppStart.aParamApp["ServerUpdateConecto_USER"], AppStart.aParamApp["ServerUpdateConecto_USER-Passw"]);
                    ftpClient.Method = System.Net.WebRequestMethods.Ftp.UploadFile;
                    ftpClient.UseBinary = true;
                    ftpClient.KeepAlive = true;
                    System.IO.FileInfo fi = new System.IO.FileInfo(PuthSetBackUp7z);
                    ftpClient.ContentLength = fi.Length;
                    byte[] buffer = new byte[4097];
                    int bytes = 0;
                    int total_bytes = (int)fi.Length;
                    System.IO.FileStream fs = fi.OpenRead();
                    System.IO.Stream rs = ftpClient.GetRequestStream();
                    while (total_bytes > 0)
                    {
                        bytes = fs.Read(buffer, 0, buffer.Length);
                        rs.Write(buffer, 0, bytes);
                        total_bytes = total_bytes - bytes;
                    }
                    fs.Close();
                    rs.Close();
                    FtpWebResponse uploadResponse = (FtpWebResponse)ftpClient.GetResponse();
                    var value = uploadResponse.StatusDescription;
                    uploadResponse.Close();
                }
            }

            System.Windows.Application.Current.Dispatcher.Invoke(new Action(delegate ()
            {
                ConectoWorkSpace.WaitMessage ConectoWorkSpace_Off = AppStart.LinkMainWindow("WaitMessage_");
                ConectoWorkSpace_Off.Close();
            }));


            if (System.IO.File.Exists(PuthSetBackUp7z))
            {
                var TextWin = "Архивирование БД " + Environment.NewLine + "выполнено. ";
                int AutoCl = 1;
                int MesaggeT = -170;
                int MessageL = 600;
                MessageEnd(TextWin, AutoCl, MesaggeT, MessageL);
            }
            else
            {
                var TextWin = "Архив БД " + Environment.NewLine + "не создан. ";
                int AutoCl = 1;
                int MesaggeT = -170;
                int MessageL = 600;
                MessageEnd(TextWin, AutoCl, MesaggeT, MessageL);

            }



            System.Windows.Application.Current.Dispatcher.Invoke(new Action(delegate ()
            {
                string PutchBack = "";
                ConectoWorkSpace.Administrator.AdminPanels ConectoWorkSpace_On = AppStart.LinkMainWindow("WAdminPanels");
                ConectoWorkSpace_On.TablAlias.IsEnabled = true;
                if (ConectoWorkSpace.Administrator.AdminPanels.hubdate == "25")
                {
                    PutchBack = "PutchBackUpLoc_Fbd25";
                    ConectoWorkSpace_On.Putch_Fbd25.Text = "";
                    CloseVisibilityButton();
                    ConectoWorkSpace_On.CreateBackUpBd25.Foreground = Brushes.White;
                }
                if (ConectoWorkSpace.Administrator.AdminPanels.hubdate == "30")
                {
                    PutchBack = "PutchBackUpLoc_Fbd30";
                    ConectoWorkSpace_On.Putch_Fbd30.Text = "";
                    CloseVisibilityButton30();
                    ConectoWorkSpace_On.CreateBackUpBd30.Foreground = Brushes.White;
                }
                Administrator.AdminPanels.UpdateKeyReestr(PutchBack, "");
             }));
            IntThreadStart--;
            if (Administrator.AdminPanels.SetWinSetHub == "BackUpFTP_Fbd25") Directory.Delete(DelDirBuckUp, true);
            Administrator.AdminPanels.SetWinSetHub = "";

        }


        // Процедура закрытия видимости кнопок служб БД 
        public static void CloseVisibilityButton()
        {
            System.Windows.Application.Current.Dispatcher.Invoke(new Action(delegate ()
            {
                ConectoWorkSpace.Administrator.AdminPanels ConectoWorkSpace_On = AppStart.LinkMainWindow("WAdminPanels");
                ConectoWorkSpace_On.LocDisk.Visibility = Visibility.Collapsed;
                ConectoWorkSpace_On.GoogleDisk.Visibility = Visibility.Collapsed;
                ConectoWorkSpace_On.FtpDisk.Visibility = Visibility.Collapsed;
                ConectoWorkSpace_On.NameArhiv.Visibility = Visibility.Collapsed;
                ConectoWorkSpace_On.Putch_Fbd25.Visibility = Visibility.Collapsed;
                ConectoWorkSpace_On.DirPath_Fbd25.Visibility = Visibility.Collapsed;
                ConectoWorkSpace_On.BackUpLocServerBD25.Visibility = Visibility.Collapsed;
            }));
        }

        // Процедура закрытия видимости кнопок служб БД 
        public static void CloseVisibilityButton30()
        {
            System.Windows.Application.Current.Dispatcher.Invoke(new Action(delegate ()
            {
                ConectoWorkSpace.Administrator.AdminPanels ConectoWorkSpace_On = AppStart.LinkMainWindow("WAdminPanels");
                ConectoWorkSpace_On.LocDisk_30.Visibility = Visibility.Collapsed;
                ConectoWorkSpace_On.GoogleDisk_30.Visibility = Visibility.Collapsed;
                ConectoWorkSpace_On.FtpDisk_30.Visibility = Visibility.Collapsed;
                ConectoWorkSpace_On.NameArhiv30.Visibility = Visibility.Collapsed;
                ConectoWorkSpace_On.Putch_Fbd30.Visibility = Visibility.Collapsed;
                ConectoWorkSpace_On.DirPath_Fbd30.Visibility = Visibility.Collapsed;
                ConectoWorkSpace_On.BackUpLocServerBD30.Visibility = Visibility.Collapsed;
            }));
        }

        /// <summary>
        /// Очистка текущей БД
        /// </summary>

        public static void LogInactivbd(FbConnection bdFBConect)
        {
            InstallB52.TrigerOnOff("ALT_TR_MN_RC_TOV_RESERV_BIU", bdFBConect, " inactive");
            InstallB52.TrigerOnOff("MN_LINK_IN_OUT_BD", bdFBConect, " inactive");
            InstallB52.TrigerOnOff("PL_RC_ORG_TOV_OUT_BD ", bdFBConect, " inactive");
            for (int index = 0; index < Administrator.AdminPanels.CountNameTriger; ++index)
                InstallB52.TrigerOnOff(CompareBD.DevelopMasName[index], bdFBConect, " inactive");
        }

        public static void TrigerOnOff(string TrigerName, FbConnection bdFBConect, string OnOff)
        {
            string ErrMessage = "";
            int num = 0;
            string cmdText = "SELECT T.RDB$TRIGGER_NAME FROM RDB$TRIGGERS T WHERE T.RDB$TRIGGER_NAME = '" + TrigerName + "'";
            try
            {
                FbCommand fbCommand1 = new FbCommand(cmdText, bdFBConect);
                FbDataReader fbDataReader = fbCommand1.ExecuteReader();
                while (fbDataReader.Read())
                    ++num;
                if ((uint)num > 0U)
                {
                    FbCommand fbCommand2 = new FbCommand("alter trigger " + TrigerName + OnOff, bdFBConect);
                    fbCommand2.ExecuteScalar();
                    fbCommand2.Dispose();
                }
                fbDataReader.Close();
                fbCommand1.Dispose();
            }
            catch (FbException ex)
            {
                SystemConecto.ErorDebag(" возникло исключение: " + Environment.NewLine + " === IDCode: " + ex.ErrorCode.ToString() + Environment.NewLine + " === Message: " + ex.Message.ToString() + Environment.NewLine + " === Exception: " + ex.ToString(), 1, 0, (SystemConecto.StruErrorDebag)null);
                ErrMessage = "Обнаружена ошибка в БД." + Environment.NewLine + ex.ToString();
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
            }
        }

        public static void LogActivBd(FbConnection bdFBConect)
        {
            InstallB52.TrigerOnOff("ALT_TR_MN_RC_TOV_RESERV_BIU", bdFBConect, " active");
            InstallB52.TrigerOnOff("MN_LINK_IN_OUT_BD", bdFBConect, " active");
            InstallB52.TrigerOnOff("PL_RC_ORG_TOV_OUT_BD ", bdFBConect, " active");
            for (int index = 0; index < Administrator.AdminPanels.CountNameTriger; ++index)
                InstallB52.TrigerOnOff(CompareBD.DevelopMasName[index], bdFBConect, " active");
        }

        public static void CheckProc(string ProcName, FbConnection bdFBConect)
        {
            string str = "";
            try
            {
                int num = 0;
                FbCommand fbCommand1 = new FbCommand("SELECT P.RDB$PROCEDURE_NAME FROM RDB$PROCEDURES P WHERE P.RDB$PROCEDURE_NAME = '" + ProcName + "'", bdFBConect);
                FbDataReader fbDataReader = fbCommand1.ExecuteReader();
                while (fbDataReader.Read())
                    ++num;
                fbDataReader.Close();
                fbCommand1.Dispose();
                if ((uint)num <= 0U)
                    return;
                FbCommand fbCommand2 = new FbCommand(ProcName, bdFBConect);
                fbCommand2.CommandType = CommandType.StoredProcedure;
                fbCommand2.ExecuteNonQuery();
                fbCommand2.Dispose();
            }
            catch (FbException ex)
            {
                SystemConecto.ErorDebag(" возникло исключение: " + Environment.NewLine + " === IDCode: " + ex.ErrorCode.ToString() + Environment.NewLine + " === Message: " + ex.Message.ToString() + Environment.NewLine + " === Exception: " + ex.ToString(), 1, 0, (SystemConecto.StruErrorDebag)null);
                str = "Обнаружена ошибка в БД." + Environment.NewLine + ex.ToString().Substring(ex.ToString().IndexOf(Environment.NewLine) + 2, ex.ToString().LastIndexOf(Environment.NewLine) - ex.ToString().IndexOf("ALT_CORR_ERROR_BD"));
                InstallB52.Temphub = "";
            }
            catch (Exception ex)
            {
                SystemConecto.ErorDebag("возникло исключение:" + Environment.NewLine + " возникло исключение: " + Environment.NewLine + " === Message: " + ex.Message.ToString() + Environment.NewLine + " === Exception: " + ex.ToString(), 1, 0, (SystemConecto.StruErrorDebag)null);
                str = "Обнаружена ошибка в БД." + Environment.NewLine + ex.ToString().Substring(ex.ToString().IndexOf(Environment.NewLine) + 2, ex.ToString().LastIndexOf(Environment.NewLine) - ex.ToString().IndexOf("ALT_CORR_ERROR_BD"));
                InstallB52.Temphub = "";
            }
        }

        public static void RunCommand(string TextCommand, FbConnection bdFBConect)
        {
            string str = "";
            try
            {
                FbCommand fbCommand = new FbCommand(TextCommand, bdFBConect);
                fbCommand.ExecuteScalar();
                fbCommand.Dispose();
            }
            catch (FbException ex)
            {
                SystemConecto.ErorDebag(" возникло исключение: " + Environment.NewLine + " === IDCode: " + ex.ErrorCode.ToString() + Environment.NewLine + " === Message: " + ex.Message.ToString() + Environment.NewLine + " === Exception: " + ex.ToString(), 1, 0, (SystemConecto.StruErrorDebag)null);
                str = "Обнаружена ошибка в БД." + Environment.NewLine + ex.ToString().Substring(ex.ToString().IndexOf(Environment.NewLine) + 2, ex.ToString().LastIndexOf(Environment.NewLine) - ex.ToString().IndexOf("mn_ost_podr"));
                InstallB52.Temphub = "";
            }
            catch (Exception ex)
            {
                SystemConecto.ErorDebag("возникло исключение:" + Environment.NewLine + " возникло исключение: " + Environment.NewLine + " === Message: " + ex.Message.ToString() + Environment.NewLine + " === Exception: " + ex.ToString(), 1, 0, (SystemConecto.StruErrorDebag)null);
                str = "Обнаружена ошибка в БД." + Environment.NewLine + ex.ToString().Substring(ex.ToString().IndexOf(Environment.NewLine) + 2, ex.ToString().LastIndexOf(Environment.NewLine) - ex.ToString().IndexOf("mn_ost_podr"));
                InstallB52.Temphub = "";
            }
        }


        public static void InstRunCleanBDTh(object ThreadObj)
        {
            // Разбор аргументов
            string index = ((AppStart.RenderInfo)ThreadObj).argument1;
            InstallB52.Temphub = Administrator.AdminPanels.SelectPuth.Substring(Administrator.AdminPanels.SelectPuth.LastIndexOf("\\") + 1, Administrator.AdminPanels.SelectPuth.IndexOf(".") - Administrator.AdminPanels.SelectPuth.LastIndexOf("\\") - 1);
            string ErrMessage = "";
            FbConnection fbConnection = new FbConnection(DBConecto.StringServerFB().ToString());
            fbConnection.Open();
            InstallB52.LogInactivbd(fbConnection);
            InstallB52.CheckProc("ALT_CORR_ERROR_BD", fbConnection);
            InstallB52.CheckProc("PRC_RESTORE_PAY_SALDO", fbConnection);
            InstallB52.CheckProc("PRC_CLEAR_GENERATORS", fbConnection);
            InstallB52.RunCommand("delete from mn_ost_podr where cnt=0", fbConnection);
            InstallB52.RunCommand("delete from mn_hd_tov_ved where (num is null) or (podr is null)", fbConnection);
            InstallB52.RunCommand("update mn_hd_tov_in hd set summa=(select sum(summa) from mn_rc_tov_in rc where rc.doc=hd.kod)", fbConnection);
            try
            {
                FbCommand fbCommand = new FbCommand("PRC_CLEAR_LOG", fbConnection);
                fbCommand.CommandType = CommandType.StoredProcedure;
                fbCommand.Parameters.Add("@DAT", FbDbType.Date).Value = (object)AppStart.TableReestr[index];
                fbCommand.ExecuteNonQuery();
                fbCommand.Dispose();
            }
            catch (FbException ex)
            {
                SystemConecto.ErorDebag(" возникло исключение: " + Environment.NewLine + " === IDCode: " + ex.ErrorCode.ToString() + Environment.NewLine + " === Message: " + ex.Message.ToString() + Environment.NewLine + " === Exception: " + ex.ToString(), 1, 0, (SystemConecto.StruErrorDebag)null);
                ErrMessage = "Обнаружена ошибка в БД." + Environment.NewLine + ex.ToString().Substring(ex.ToString().IndexOf(Environment.NewLine) + 2, ex.ToString().LastIndexOf(Environment.NewLine) - ex.ToString().IndexOf("PRC_CLEAR_LOG"));
                InstallB52.Temphub = "";
            }
            try
            {
                FbCommand fbCommand1 = new FbCommand("PRC_CLEAR_DATABASE", fbConnection);
                fbCommand1.CommandType = CommandType.StoredProcedure;
                fbCommand1.Parameters.Add("@DAT", FbDbType.Date).Value = (object)AppStart.TableReestr[index];
                fbCommand1.Parameters.Add("@POST", FbDbType.VarChar).Value = (object)Administrator.AdminPanels.KodOrgClearDB;
                fbCommand1.Parameters.Add("@CURR", FbDbType.Integer).Value = (object)"1";
                fbCommand1.Parameters.Add("@KURS", FbDbType.Double).Value = (object)"0";
                fbCommand1.Parameters.Add("@SEPARATE_ORG_SALDO", FbDbType.Integer).Value = (object)"0";
                fbCommand1.Parameters.Add("@USE_PRICE_LIST", FbDbType.Integer).Value = (object)"0";
                fbCommand1.ExecuteNonQuery();
                fbCommand1.Dispose();
                FbCommand fbCommand2 = new FbCommand("PRC_CORR_TOV_OST", fbConnection);
                fbCommand2.CommandType = CommandType.StoredProcedure;
                fbCommand2.Parameters.Add("@USE_CHRON", FbDbType.Integer).Value = (object)"1";
                fbCommand2.ExecuteNonQuery();
                fbCommand2.Dispose();
            }
            catch (FbException ex)
            {
                SystemConecto.ErorDebag(" возникло исключение: " + Environment.NewLine + " === IDCode: " + ex.ErrorCode.ToString() + Environment.NewLine + " === Message: " + ex.Message.ToString() + Environment.NewLine + " === Exception: " + ex.ToString(), 1, 0, (SystemConecto.StruErrorDebag)null);
                ErrMessage = "Обнаружена ошибка в БД." + Environment.NewLine + ex.ToString().Substring(ex.ToString().IndexOf(Environment.NewLine) + 2, ex.ToString().LastIndexOf(Environment.NewLine) - ex.ToString().IndexOf("PRC_CLEAR_DATABASE"));
                InstallB52.Temphub = "";
            }
            catch (Exception ex)
            {
                SystemConecto.ErorDebag("возникло исключение:" + Environment.NewLine + " возникло исключение: " + Environment.NewLine + " === Message: " + ex.Message.ToString() + Environment.NewLine + " === Exception: " + ex.ToString(), 1, 0, (SystemConecto.StruErrorDebag)null);
                ErrMessage = "Обнаружена ошибка в БД." + Environment.NewLine + ex.ToString().Substring(ex.ToString().IndexOf(Environment.NewLine) + 2, ex.ToString().LastIndexOf(Environment.NewLine) - ex.ToString().IndexOf("PRC_CLEAR_DATABASE"));
                InstallB52.Temphub = "";
            }
            InstallB52.LogActivBd(fbConnection);
            fbConnection.Close();
            fbConnection.Dispose();
            FbConnection.ClearAllPools();
            string str = AppStart.TableReestr["SetTextIp4BackOf"] + "/" + Administrator.AdminPanels.SetPort25 + ":" + Administrator.AdminPanels.SelectAlias;
            Administrator.AdminPanels.RunGbak = Administrator.AdminPanels.CurrentSereverPuth + (Administrator.AdminPanels.hubdate == "25" ? "bin\\" : "") + "gfix.exe";
            Administrator.AdminPanels.ArgumentCmd = "-sweep " + str + " -user sysdba -pass " + Administrator.AdminPanels.CurrentPasswFb25;
            SystemConecto.ErorDebag("Чистка  БД от всякого мусора" + Administrator.AdminPanels.RunGbak + Administrator.AdminPanels.ArgumentCmd, 1, 2, (SystemConecto.StruErrorDebag)null);
            Administrator.AdminPanels.RunProcess(Administrator.AdminPanels.RunGbak, Administrator.AdminPanels.ArgumentCmd, "sweep");
            System.Windows.Application.Current.Dispatcher.Invoke(new Action(delegate ()
            {
                ConectoWorkSpace.Administrator.AdminPanels ConectoWorkSpace_Off = AppStart.LinkMainWindow("WAdminPanels");

                if (Administrator.AdminPanels.hubdate == "25")
                {
                    ConectoWorkSpace_Off.datePicker25_Server.Visibility = Visibility.Collapsed;
                    ConectoWorkSpace_Off.CleanBD25_Label.Visibility = Visibility.Collapsed;
                    ConectoWorkSpace_Off.Combo_Label.Visibility = Visibility.Collapsed;
                    ConectoWorkSpace_Off.ComboFiltr.Visibility = Visibility.Collapsed;
                    ConectoWorkSpace_Off.OnOffSborka.Visibility = Visibility.Collapsed;
                    ConectoWorkSpace_Off.LabelSborka.Visibility = Visibility.Collapsed;
                    ConectoWorkSpace_Off.GoNewPeriod.Foreground = Brushes.White;

                }
                if (Administrator.AdminPanels.hubdate == "30")
                {
                    ConectoWorkSpace_Off.datePicker30_Server.Visibility = Visibility.Collapsed;
                    ConectoWorkSpace_Off.CleanBD30_Label.Visibility = Visibility.Collapsed;
                    ConectoWorkSpace_Off.Combo_Label30.Visibility = Visibility.Collapsed;
                    ConectoWorkSpace_Off.ComboFiltr30.Visibility = Visibility.Collapsed;
                    ConectoWorkSpace_Off.OnOffSborka30.Visibility = Visibility.Collapsed;
                    ConectoWorkSpace_Off.LabelSborka30.Visibility = Visibility.Collapsed;
                    ConectoWorkSpace_Off.GoNewPeriod30.Foreground = Brushes.White;

                }

            }));

            System.Windows.Application.Current.Dispatcher.Invoke(new Action(delegate ()
            {
                ConectoWorkSpace.WaitMessage ConectoWorkSpace_Off = AppStart.LinkMainWindow("WaitMessage_");
                ConectoWorkSpace_Off.Close();
            }));
            --InstallB52.IntThreadStart;
            if (InstallB52.Temphub == "")
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
            else
                InstallB52.MessageEnd("Очистка БД " + Environment.NewLine + " успешно завершена.", 1, -170, 600);
        }

        // Процедура чистки БД
        public static void CleanAllBd(object ThreadObj)
        {
            AppStart.RenderInfo renderInfo1 = (AppStart.RenderInfo)ThreadObj;
            string str1 = renderInfo1.argument1;
            string str2 = renderInfo1.argument2;
            string str3 = renderInfo1.argument3;
            string str4 = renderInfo1.argument4;
            string str5 = renderInfo1.argument5;
            string str6 = "";
            string str7 = str5.Length != 0 ? str5.Substring(str5.LastIndexOf("\\") + 1, str5.LastIndexOf(".") - str5.LastIndexOf("\\") - 1).Trim() : str2;
            FbConnectionStringBuilder connectionStringBuilder = new FbConnectionStringBuilder();
            connectionStringBuilder.UserID = "SYSDBA";
            connectionStringBuilder.Password = str1;
            connectionStringBuilder.Dialect = 1;
            connectionStringBuilder.DataSource = "TCP/IP";
            if (Administrator.AdminPanels.hubdate.Contains("25"))
            {
                connectionStringBuilder.Charset = "NONE";
                connectionStringBuilder.Dialect = 3;
                connectionStringBuilder.ServerType = FbServerType.Default;
            }
            connectionStringBuilder.Database = AppStart.TableReestr["SetTextIp4BackOf"] + "/" + str4 + ":" + str7;
            connectionStringBuilder.Port = (int)Convert.ToInt16(str4);
            FbConnection fbConnection = new FbConnection(connectionStringBuilder.ToString());
            fbConnection.Open();
            InstallB52.LogInactivbd(fbConnection);
            InstallB52.TrigerOnOff(" tr_del_spr_group ", fbConnection, " inactive");
            InstallB52.TrigerOnOff(" tr_del_mn_rc_tov_ved ", fbConnection, " inactive");
            InstallB52.TrigerOnOff(" trc_mn_hd_tov_out_bd0 ", fbConnection, " inactive");
            InstallB52.TrigerOnOff(" ht_rc_reservation_bd0 ", fbConnection, " inactive");
            InstallB52.TrigerOnOff(" ht_hd_serv_sch_bd inactive ", fbConnection, " inactive");
            try
            {
                if (str5.Length == 0)
                {
                    int num = 0;
                    string cmdText = "ALT_PRC_DELETE_ALL";
                    FbCommand fbCommand1 = new FbCommand("SELECT P.RDB$PROCEDURE_NAME FROM RDB$PROCEDURES P WHERE P.RDB$PROCEDURE_NAME = '" + cmdText + "'", fbConnection);
                    FbDataReader fbDataReader = fbCommand1.ExecuteReader();
                    while (fbDataReader.Read())
                        ++num;
                    fbDataReader.Close();
                    fbCommand1.Dispose();
                    if (num == 0)
                        cmdText = "PRC_DELETE_ALL";
                    FbCommand fbCommand2 = new FbCommand(cmdText, fbConnection);
                    fbCommand2.CommandType = CommandType.StoredProcedure;
                    fbCommand2.ExecuteNonQuery();
                    fbCommand2.Dispose();
                }
            }
            catch (FbException ex)
            {
                SystemConecto.ErorDebag(" возникло исключение: " + Environment.NewLine + " === IDCode: " + ex.ErrorCode.ToString() + Environment.NewLine + " === Message: " + ex.Message.ToString() + Environment.NewLine + " === Exception: " + ex.ToString(), 1, 0, (SystemConecto.StruErrorDebag)null);
                str6 = "Обнаружена ошибка в БД." + Environment.NewLine + ex.ToString().Substring(ex.ToString().IndexOf(Environment.NewLine) + 2);
            }
            try
            {
                FbCommand fbCommand = new FbCommand("PRC_CLEAR_GENERATORS", fbConnection);
                fbCommand.CommandType = CommandType.StoredProcedure;
                fbCommand.ExecuteNonQuery();
                fbCommand.Dispose();
            }
            catch (FbException ex)
            {
                SystemConecto.ErorDebag(" возникло исключение: " + Environment.NewLine + " === IDCode: " + ex.ErrorCode.ToString() + Environment.NewLine + " === Message: " + ex.Message.ToString() + Environment.NewLine + " === Exception: " + ex.ToString(), 1, 0, (SystemConecto.StruErrorDebag)null);
                str6 = "Обнаружена ошибка в БД." + Environment.NewLine + ex.ToString().Substring(ex.ToString().IndexOf(Environment.NewLine) + 2);
            }
            try
            {
                if (str5.Length == 0)
                {
                    FbCommand fbCommand = new FbCommand("PRC_CLEAR_LOG", fbConnection);
                    fbCommand.CommandType = CommandType.StoredProcedure;
                    fbCommand.Parameters.Add("@DAT", FbDbType.Date).Value = (object)DateTime.Now.ToString("dd.MM.yyyy");
                    fbCommand.ExecuteNonQuery();
                    fbCommand.Dispose();
                }
            }
            catch (FbException ex)
            {
                SystemConecto.ErorDebag(" возникло исключение: " + Environment.NewLine + " === IDCode: " + ex.ErrorCode.ToString() + Environment.NewLine + " === Message: " + ex.Message.ToString() + Environment.NewLine + " === Exception: " + ex.ToString(), 1, 0, (SystemConecto.StruErrorDebag)null);
                str6 = "Обнаружена ошибка в БД." + Environment.NewLine + ex.ToString().Substring(ex.ToString().IndexOf(Environment.NewLine) + 2);
            }
            InstallB52.TrigerOnOff(" tr_del_spr_group ", fbConnection, " active");
            InstallB52.TrigerOnOff(" tr_del_mn_rc_tov_ved ", fbConnection, " active");
            InstallB52.TrigerOnOff(" trc_mn_hd_tov_out_bd0 ", fbConnection, " active");
            InstallB52.TrigerOnOff(" ht_rc_reservation_bd0 ", fbConnection, " active");
            InstallB52.TrigerOnOff(" ht_hd_serv_sch_bd inactive ", fbConnection, " active");
            InstallB52.LogActivBd(fbConnection);
            fbConnection.Close();
            fbConnection.Dispose();
            connectionStringBuilder.Clear();
            AppStart.RenderInfo renderInfo2 = new AppStart.RenderInfo();
            renderInfo2.argument1 = Administrator.AdminPanels.hubdate.Contains("25") ? Administrator.AdminPanels.CurrentPasswFb25 : Administrator.AdminPanels.CurrentPasswFb30;
            renderInfo2.argument2 = str7;
            renderInfo2.argument3 = str3;
            renderInfo2.argument4 = Administrator.AdminPanels.hubdate.Contains("25") ? Administrator.AdminPanels.SetPort25 : Administrator.AdminPanels.SetPort30;
            renderInfo2.argument5 = "CleanSat";
            Thread thread = new Thread(new System.Threading.ParameterizedThreadStart(InstallB52.CleanTrashBD));
            thread.SetApartmentState(ApartmentState.STA);
            thread.IsBackground = true;
            thread.Start((object)renderInfo2);
            ++InstallB52.IntThreadStart;
        }


        public static void CleanTrashBD(object ThreadObj)
        {
            // Разбор аргументов
            AppStart.RenderInfo renderInfo = (AppStart.RenderInfo)ThreadObj;
            string str1 = renderInfo.argument1;
            string str2 = renderInfo.argument2;
            string str3 = renderInfo.argument3;
            string str4 = renderInfo.argument4;
            string str5 = renderInfo.argument5;
            //AppStart.TableReestr["SetTextIp4BackOf"] + "/" + str4 + ":" + str3;
            string str6 = AppStart.TableReestr["SetTextIp4BackOf"] + "/" + str4 + ":" + str2;
            Administrator.AdminPanels.RunGbak = Administrator.AdminPanels.CurrentSereverPuth + (Administrator.AdminPanels.hubdate == "25" ? "bin\\" : "") + "gfix.exe";
            string runGbak = Administrator.AdminPanels.RunGbak;
            if (!System.IO.File.Exists(Administrator.AdminPanels.RunGbak))
            {
                Administrator.AdminPanels.NameObj = "";
                System.Windows.Application.Current.Dispatcher.Invoke(new Action(delegate ()
                {
                    ConectoWorkSpace.Administrator.AdminPanels ConectoWorkSpace_On = AppStart.LinkMainWindow("WAdminPanels");
                    if (Administrator.AdminPanels.hubdate == "25")
                    {
                        ConectoWorkSpace_On.CleanTrashBD25.Foreground = Brushes.White;
                        ConectoWorkSpace_On.TablPuthBD.IsEnabled = true;
                    }
                    if (Administrator.AdminPanels.hubdate == "30")
                    {
                        ConectoWorkSpace_On.CleanTrashBD30.Foreground = Brushes.White;
                        ConectoWorkSpace_On.TablPuthBD30.IsEnabled = true;
                    }
                }));
                InstallB52.MessageErorInst("Отсутствует файл gfix.exe.Очистка БД прекращена. " + Environment.NewLine + "Необходимо проверить наличие утилиты gfix.exe");
            }
            else
            {
                Administrator.AdminPanels.ArgumentCmd = " -sweep " + str6 + " -user sysdba -pass " + str1;
                SystemConecto.ErorDebag(Administrator.AdminPanels.RunGbak + Administrator.AdminPanels.ArgumentCmd, 1, 2, (SystemConecto.StruErrorDebag)null);
                string NoWindow = str5 != "CopyLog" ? str5 : "CopyLog";
                Administrator.AdminPanels.RunProcess(Administrator.AdminPanels.RunGbak, Administrator.AdminPanels.ArgumentCmd, NoWindow);
                System.Windows.Application.Current.Dispatcher.Invoke(new Action(delegate ()
                {
                    ConectoWorkSpace.Administrator.AdminPanels ConectoWorkSpace_On = AppStart.LinkMainWindow("WAdminPanels");
                    if (Administrator.AdminPanels.hubdate == "25")
                    {
                        ConectoWorkSpace_On.CleanTrashBD25.Foreground = Brushes.White;
                        ConectoWorkSpace_On.TablPuthBD.IsEnabled = true;
                    }
                    if (Administrator.AdminPanels.hubdate == "30")
                    {
                        ConectoWorkSpace_On.CleanTrashBD30.Foreground = Brushes.White;
                        ConectoWorkSpace_On.TablPuthBD30.IsEnabled = true;
                    }

                }));
            }
            FbConnection.ClearAllPools();
            if (str5 != "CleanMusor")
            {
                if (!Directory.Exists("c:\\BackupFdb\\"))
                    Directory.CreateDirectory("c:\\BackupFdb\\");
                string str7 = "/" + (Administrator.AdminPanels.hubdate == "25" ? Administrator.AdminPanels.SetPort25 : Administrator.AdminPanels.SetPort30) + ":";
                string str8 = Administrator.AdminPanels.SelectPuth.Substring(Administrator.AdminPanels.SelectPuth.LastIndexOf("\\") + 1, Administrator.AdminPanels.SelectPuth.IndexOf(".") - Administrator.AdminPanels.SelectPuth.LastIndexOf("\\") - 1);
                string str9 = Administrator.AdminPanels.hubdate == "25" ? Administrator.AdminPanels.CurrentPasswFb25 : Administrator.AdminPanels.CurrentPasswFb30;
                string str10 = "  c:\\BackupFdb\\" + str8 + DateTime.Now.ToString("yyMMddHHmmss") + ".fbk";
                Administrator.AdminPanels.RunGbak = Administrator.AdminPanels.CurrentSereverPuth + (Administrator.AdminPanels.hubdate == "25" ? "bin\\" : "") + "gbak.exe";
                Administrator.AdminPanels.ArgumentCmd = "-b " + str6 + str10 + " -v -user sysdba -pass " + str9;
                SystemConecto.ErorDebag("Перваый этап создание архива БД" + Administrator.AdminPanels.RunGbak + Administrator.AdminPanels.ArgumentCmd, 1, 2, (SystemConecto.StruErrorDebag)null);
                Administrator.AdminPanels.RunProcess(Administrator.AdminPanels.RunGbak, Administrator.AdminPanels.ArgumentCmd, "");
                string destFileName = SystemConectoServers.PutchServerData + str8 + DateTime.Now.ToString("yyMMddHHmmss") + ".fdb";
                System.IO.File.Copy(Administrator.AdminPanels.SelectPuth, destFileName, true);
                Administrator.AdminPanels.ArgumentCmd = " -rep " + str10 + "  " + str6 + " -v -bu 200  -user sysdba -pass " + str9;
                Administrator.AdminPanels.RunProcess(Administrator.AdminPanels.RunGbak, Administrator.AdminPanels.ArgumentCmd, "");
            }
            System.Windows.Application.Current.Dispatcher.Invoke(new Action(delegate ()
            {
                ConectoWorkSpace.WaitMessage ConectoWorkSpace_Off = AppStart.LinkMainWindow("WaitMessage_");
                ConectoWorkSpace_Off.Close();
            }));
            if (str5 == "CleanMusor") MainWindow.MessageInstall("Уборка мусора " + Environment.NewLine + " успешно завершена.", 1, -400, 300);
            if (str5 == "CleanSat") MainWindow.MessageInstall("Чистка БД " + Environment.NewLine + " успешно завершена.", 1, -400, 300);
            System.Windows.Application.Current.Dispatcher.Invoke(new Action(delegate ()
            {
                ConectoWorkSpace.Administrator.AdminPanels ConectoWorkSpace_On = AppStart.LinkMainWindow("WAdminPanels");
                ConectoWorkSpace_On.TablPuthBD.IsEnabled = true;
                ConectoWorkSpace_On.CleanConect.Foreground = (Brush)Brushes.White;
            }));

        }


        // Процедура пересборки и обновления БД
        public static void UpdateBD(object ThreadObj) //string CurrentFB, string FileNameBD, string PuthNameBD,string Temphub) 
        {
            // Разбор аргументов
            AppStart.RenderInfo arguments = (AppStart.RenderInfo)ThreadObj;
            string CurrentFB = arguments.argument1;
            string FileNameBD = arguments.argument2;
            string PuthNameBD = arguments.argument3;
            string ServerPort = arguments.argument4;

            if (!Directory.Exists(@"c:\BackupFdb\")) Directory.CreateDirectory(@"c:\BackupFdb\");
            string CleanHub = @"  c:\BackupFdb\" + FileNameBD + DateTime.Now.ToString("yyMMddHHmmss") + ".fbk";          
            string TCPIPBack = AppStart.TableReestr["SetTextIp4BackOf"] +"/"+ ServerPort +":"+ FileNameBD;
 
            string TempFB = Administrator.AdminPanels.hubdate == "25" ? "Firebird_2_5" : "Firebird_3_0";
            Administrator.AdminPanels.RunGbak = Administrator.AdminPanels.CurrentSereverPuth + (Administrator.AdminPanels.hubdate == "25" ? @"bin\gbak.exe" :  @"gbak.exe");
            Administrator.AdminPanels.ArgumentCmd = "-b " + TCPIPBack + CleanHub + @" -v  -user sysdba -pass " + CurrentFB;

            if (!System.IO.File.Exists(Administrator.AdminPanels.RunGbak))
            {
                Administrator.AdminPanels.NameObj = "";
                string MesageText = "Отсутствует файл gbak.exe.Очистка БД прекращена. " + Environment.NewLine + "Необходимо проверить наличие утилиты gbak.exe";
                InstallB52.MessageErorInst(MesageText);
                System.Windows.Application.Current.Dispatcher.Invoke(new Action(delegate ()
                {
                    ConectoWorkSpace.WaitMessage ConectoWorkSpace_Off = AppStart.LinkMainWindow("WaitMessage_");
                    ConectoWorkSpace_Off.Close();
                }));
                System.Windows.Application.Current.Dispatcher.Invoke(new Action(delegate ()
                {
                    ConectoWorkSpace.Administrator.AdminPanels ConectoWorkSpace_On = AppStart.LinkMainWindow("WAdminPanels");
                    ConectoWorkSpace_On.UpdateBD25.Foreground = Brushes.White;
                    ConectoWorkSpace_On.TablPuthBD.IsEnabled = true;

                }));
                Administrator.AdminPanels.NameObj = "";
                return;
            }
            else
            {

                SystemConecto.ErorDebag("Перваый этап создание архива БД" + Administrator.AdminPanels.RunGbak + Administrator.AdminPanels.ArgumentCmd, 1, 2);
                // Перваый этап создание архива БД.

                Process CmdDos = new Process();
                CmdDos.StartInfo.FileName = Administrator.AdminPanels.RunGbak;
                CmdDos.StartInfo.Arguments = Administrator.AdminPanels.ArgumentCmd;
                CmdDos.StartInfo.UseShellExecute = false;
                //CmdDos.StartInfo.CreateNoWindow = true;
                //CmdDos.StartInfo.RedirectStandardOutput = true;
                CmdDos.Start();
                CmdDos.WaitForExit();
                CmdDos.Close();
                System.IO.File.Copy(PuthNameBD, SystemConectoServers.PutchServerData + FileNameBD + DateTime.Now.ToString("yyMMddHHmmss") + ".fdb");

                // Второй этап разархивирование архива.
                Administrator.AdminPanels.ArgumentCmd = @"-rep " + CleanHub + "  " + TCPIPBack + " -bu 200 -v -user sysdba -pass " + CurrentFB;

                SystemConecto.ErorDebag("Второй этап разархивирование архива" + Administrator.AdminPanels.RunGbak + Administrator.AdminPanels.ArgumentCmd, 1, 2);

                CmdDos.StartInfo.FileName = Administrator.AdminPanels.RunGbak;
                CmdDos.StartInfo.Arguments = Administrator.AdminPanels.ArgumentCmd;
                CmdDos.StartInfo.UseShellExecute = false;
                //CmdDos.StartInfo.CreateNoWindow = true;
                //CmdDos.StartInfo.RedirectStandardOutput = true;
                CmdDos.Start();
                CmdDos.WaitForExit();
                CmdDos.Close();

            }

            System.Windows.Application.Current.Dispatcher.Invoke(new Action(delegate ()
            {
                ConectoWorkSpace.WaitMessage ConectoWorkSpace_Off = AppStart.LinkMainWindow("WaitMessage_");
                ConectoWorkSpace_Off.Close();
            }));
            System.Windows.Application.Current.Dispatcher.Invoke(new Action(delegate ()
            {
                ConectoWorkSpace.Administrator.AdminPanels ConectoWorkSpace_On = AppStart.LinkMainWindow("WAdminPanels");
                ConectoWorkSpace_On.TablPuthBD.IsEnabled = true;
                ConectoWorkSpace_On.TablAlias.IsEnabled = true;
                ConectoWorkSpace_On.TablAlias30.IsEnabled = true;
                ConectoWorkSpace_On.UpdateBD25.Foreground = Brushes.White;
                ConectoWorkSpace_On.UpdateBD30.Foreground = Brushes.White;
            }));
            
            var TextMessage = "Пересборка БД " + Environment.NewLine + " успешно завершена.";
            MainWindow.MessageInstall(TextMessage);
            if (Administrator.AdminPanels.NameObj == "UpdateBD25" || Administrator.AdminPanels.NameObj == "UpdateBD30") return;
            
            // ----------------------------------------------------------------------
            // Продолжение. часть 2 выполнение процедуры UpgradeBD25.
  
            // Запуск B52Update.exe
            Process CmdUpgrade = new Process();
            CmdUpgrade.StartInfo.FileName = SystemConecto.PutchApp + @"Repository\B52Update8.exe";
            CmdUpgrade.StartInfo.Arguments = "";
            CmdUpgrade.StartInfo.UseShellExecute = false;
            CmdUpgrade.StartInfo.CreateNoWindow = true;
            CmdUpgrade.StartInfo.RedirectStandardOutput = true;
            CmdUpgrade.Start();
            CmdUpgrade.WaitForExit();
            CmdUpgrade.Close();
 
            string PuthDir = SystemConecto.OS64Bit ? @"c:\Windows\SysWOW64\" : @"c:\Windows\System32\";
            System.IO.File.Delete(PuthDir+ "GDS16.LIB");
            System.IO.File.Delete(PuthDir + "GDS32.dll");
            
            DBConecto.DBopenFBConectionMemory(SystemBD.ConnnStringSystem().ToString(), "", "FbSystem");
            string StrCreate = "UPDATE CONNECTIONBD25 SET UPDATETIME = '"+ Administrator.AdminPanels.ShortDate + "' WHERE CONNECTIONBD25.PUTHBD = " + "'" + Administrator.AdminPanels.SelectPuth + "'";
            DBConecto.UniQuery DeletQuery = new DBConecto.UniQuery(StrCreate, "FB");
            DeletQuery.UserQuery = string.Format(StrCreate, "CONNECTIONBD25");
            DeletQuery.ExecuteUNIScalar();
            DBConecto.DBcloseFBConectionMemory("FbSystem");

            TextMessage = "Обновление БД " + Environment.NewLine + " успешно завершено.";
            MainWindow.MessageInstall(TextMessage);

            System.Windows.Application.Current.Dispatcher.Invoke(new Action(delegate ()
            {
                ConectoWorkSpace.Administrator.AdminPanels ConectoWorkSpace_Off = AppStart.LinkMainWindow("WAdminPanels");
                ConectoWorkSpace_Off.UpgradeBD25.Foreground = Brushes.White;
                ConectoWorkSpace_Off.TextBox_Fbd25.Text = "";
                ConectoWorkSpace_Off.TextBox_Fbd25.Visibility = Visibility.Collapsed;
                ConectoWorkSpace_Off.LabelPuth.Visibility = Visibility.Collapsed;
                ConectoWorkSpace_Off.LabelPuth.Content = "Путь к БД.";
                ConectoWorkSpace_Off.TablPuthBD.IsEnabled = true;

            }));
            
            IntThreadStart--;
        }

        public static void BackUp25TH(object ThreadObj)  
        {

            // Разбор аргументов
            AppStart.RenderInfo arguments = (AppStart.RenderInfo)ThreadObj;
            string PuthSetRestory30 = arguments.argument1;
            string FileBackUp = arguments.argument2;
            string PuthBd25 = arguments.argument3;
            string PuthSrv25 = arguments.argument4;
            // Перваый этап создание архива БД.

            SystemConecto.ErorDebag(Administrator.AdminPanels.RunGbak + Administrator.AdminPanels.ArgumentCmd, 1, 2);

            Process CmdDos = new Process();
            CmdDos.StartInfo.FileName = Administrator.AdminPanels.RunGbak;
            CmdDos.StartInfo.Arguments = Administrator.AdminPanels.ArgumentCmd;
            CmdDos.StartInfo.UseShellExecute = false;
            //CmdDos.StartInfo.CreateNoWindow = true;
            //CmdDos.StartInfo.RedirectStandardOutput = true;
            CmdDos.Start();
            CmdDos.WaitForExit();
            CmdDos.Close();


            string ServerPort = "", ServerPuth = "", ServerAlias = "";
            int Idcount = 0, Idcountout = 0;
            string StrCreate = "SELECT * from SERVERACTIVFB30 WHERE SERVERACTIVFB30.PUTH =" + "'" + AppStart.TableReestr["ServerDefault30"] + "'";
            DBConecto.DBopenFBConectionMemory(SystemBD.ConnnStringSystem().ToString(), "", "FbSystem");
            FbCommand SelectTable = new FbCommand(StrCreate, DBConecto.bdFbSystemConect);
            SelectTable.CommandType = CommandType.Text;
            FbDataReader ReadTable = SelectTable.ExecuteReader();
            while (ReadTable.Read())
            {
                ServerPort = ReadTable[0].ToString();
                ServerPuth = ReadTable[1].ToString();
                ServerAlias = ReadTable[2].ToString();
                Administrator.AdminPanels.CurrentPasswFb30 = ReadTable[5].ToString();
                Idcount++;
            }
            ReadTable.Close();
            if (Idcount == 0)
            {
                System.Windows.Application.Current.Dispatcher.Invoke(new Action(delegate ()
                {
                    ConectoWorkSpace.WaitMessage ConectoWorkSpace_Off = AppStart.LinkMainWindow("WaitMessage_");
                    ConectoWorkSpace_Off.Close();
                }));
                var TextMessage = "Не найден сервер Firebird 3.0 " + Environment.NewLine + " Выполнение процедуры остановлено.";
                int AutoClose = 1;
                int MesaggeTop = -170;
                int MessageLeft = 600;
                InstallB52.MessageEnd(TextMessage, AutoClose, MesaggeTop, MessageLeft);
                IntThreadStart--;
                return;
            } 
            SelectTable.Dispose();
            DBConecto.DBcloseFBConectionMemory("FbSystem");
            if (Administrator.AdminPanels.Inst2530 != "Ok")
            {
                var GchangeaAliases25 = ConectoWorkSpace.INI.ReadFile(ServerPuth + "databases.conf");
                GchangeaAliases25.Set(Administrator.AdminPanels.SelectAlias, PuthSetRestory30, true);
                Idcount = 0;
                string[] fileLines = System.IO.File.ReadAllLines(ServerPuth + "databases.conf");
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
                System.IO.File.WriteAllLines(ServerPuth + "databases.conf", fileoutLines);
                RestartFB25(ServerPuth, ServerAlias);
            }
            if (System.IO.File.Exists(PuthSetRestory30) == true && Administrator.AdminPanels.Inst2530 == "Ok") System.IO.File.Delete(PuthSetRestory30);
           // Второй этап разархивирование архива в формате 3.0.
            string TCPIPBack = AppStart.TableReestr["SetTextIp4BackOf"] + "/" + ServerPort + ":" + Administrator.AdminPanels.SelectAlias.Trim();
            string StrPuth = '"' + PuthSetRestory30 + '"';
            Administrator.AdminPanels.ArgumentCmd = @"-rep " + FileBackUp + "  " + StrPuth + " -v -bu 200  -user sysdba -pass " + Administrator.AdminPanels.CurrentPasswFb30;  //+ @" - y c:\temp\log.txt"
            Administrator.AdminPanels.RunGbak = ServerPuth + @"gbak.exe";

            SystemConecto.ErorDebag(Administrator.AdminPanels.RunGbak + Administrator.AdminPanels.ArgumentCmd, 1, 2);
            Administrator.AdminPanels.RunProcess(Administrator.AdminPanels.RunGbak, Administrator.AdminPanels.ArgumentCmd);
 
            DBConecto.DBopenFBConectionMemory(SystemBD.ConnnStringSystem().ToString(), "", "FbSystem");
            StrCreate = "INSERT INTO CONNECTIONBD30  values ( '" + PuthSetRestory30 + "'" + ", '" + Administrator.AdminPanels.SelectAlias + "', '"+ AppStart.TableReestr["NameServer30"] + "', '" + AppStart.TableReestr["ServerDefault30"] + "','','','')";
            DBConecto.UniQuery InsertQuery = new DBConecto.UniQuery(StrCreate, "FB");
            InsertQuery.UserQuery = string.Format(StrCreate, "CONNECTIONBD30");
            InsertQuery.ExecuteUNIScalar();
            DBConecto.DBcloseFBConectionMemory("FbSystem");
            Administrator.AdminPanels.Grid_Puth("SELECT * from CONNECTIONBD30");

            System.Windows.Application.Current.Dispatcher.Invoke(new Action(delegate ()
            {
                ConectoWorkSpace.WaitMessage ConectoWorkSpace_Off = AppStart.LinkMainWindow("WaitMessage_");
                ConectoWorkSpace_Off.Close();
            }));
            System.Windows.Application.Current.Dispatcher.Invoke(new Action(delegate ()
            {
                Administrator.AdminPanels ConectoWorkSpace_On = AppStart.LinkMainWindow("WAdminPanels");
                ConectoWorkSpace_On.Putch_Fbd25.Text = "";
                ConectoWorkSpace_On.BackUpLocServerBD25.IsEnabled = false;
                ConectoWorkSpace_On.ChangeFbd2530.Foreground = Brushes.White;
                ConectoWorkSpace_On.TablAlias.IsEnabled = true;
                if (Convert.ToInt16(AppStart.TableReestr["SetPuthBD30"]) == 0)
                {
                    ConectoWorkSpace.Administrator.AdminPanels.UpdateKeyReestr("SetPuthBD30", "2");
                    ConectoWorkSpace_On.PuthSetBD30.Text = PuthSetRestory30;
                    ConectoWorkSpace_On.DefNameServer30.Text = AppStart.TableReestr["NameServer30"];
                }

            }));
            System.Windows.Application.Current.Dispatcher.Invoke(new Action(delegate ()
            {

                MainWindow ConectoWorkSpace_InW = AppStart.LinkMainWindow();
                var TextWindows = "Перезапись БД в формат  " + Environment.NewLine + " Firebird 3.0 выполнено";
                int AutoClose = 1;
                var WinOblakoVerh_ = AppStart.WindowActive("WinOblakoVerh_Net", TextWindows, AutoClose);
                WinOblakoVerh_.Owner = ConectoWorkSpace_InW;
                // размещаем на рабочем столе
                WinOblakoVerh_.Top = SystemConecto.WorkAreaDisplayDefault[0] + (WinOblakoVerh_.Height - 58) + 50;
                WinOblakoVerh_.Left = SystemConecto.WorkAreaDisplayDefault[1] + 950; 
                WinOblakoVerh_.ShowActivated = false;
                WinOblakoVerh_.Show();

            }));
            InstallB52.CloseVisibilityButton();
            Administrator.AdminPanels.SetWinSetHub = "";
            InstallB52.IntThreadStart--;

        }

        public static void UpGreateB52(object ThreadObj)
        {
            string[] FolderBack = { "B52BackOffice8.exe", "B52FrontOffice8.exe", "B52Fitness8.exe", "B52Hotel8.exe", "B52Update8.exe", "B52CallCenter8.exe" , "B52Config8.exe" };
            string  CurrentUpdate = "";
            // ФТП Одесса
            //Login = ftp://partner
            //Password = cnelbzgk.c
            //Ip = 195.138.94.90

            // updatework.conecto.ua Чтение паролей из настроек ПО WriterConfigUserXML (Пользователь устанавливается на стороне сервера)
            // updatework.conecto.ua/updatework.conecto.ua/ "update_workspace" "conect1074"
            //string strServer, string NameUser, string PasswdUser, int TypeCommand = 1, string PutchTMPFile = ""
            // Прверка даты модификации  B52Update8.exe.
            
            Uri UriServer = new Uri(@"ftp://195.138.94.90/bin/"); //B52Update8.exe
            string NameUser = "partner";
            string PasswdUser = "cnelbzgk.c";
            
            for (int i = 0; i <= FolderBack.Count()-1; i++)
            {
                long SizeExeRepository, SizeExeFtp;
                var ConectionFTP = SystemConecto.ConntecionFTP(@"195.138.94.90/bin/" + FolderBack[i], NameUser, PasswdUser, 2, SystemConecto.PutchApp + @"bin\" + FolderBack[i]);
                if (ConectionFTP != null)
                { 
                    FileStream LocFileFtp = System.IO.File.OpenRead(SystemConecto.PutchApp + @"bin\" + FolderBack[i]);
                    SizeExeFtp = LocFileFtp.Length;
                    LocFileFtp.Close();
                    if (!System.IO.File.Exists(SystemConecto.PutchApp + @"Repository\" + FolderBack[i])) SizeExeRepository = 0;
                    else
                    { 
                        FileStream LocFile = System.IO.File.OpenRead(SystemConecto.PutchApp + @"Repository\" + FolderBack[i]);
                        SizeExeRepository = LocFile.Length;
                        LocFile.Close();
                    }
                    if (SizeExeRepository != SizeExeFtp)
                    {
                        System.IO.File.Copy(SystemConecto.PutchApp + @"bin\" + FolderBack[i], SystemConecto.PutchApp + @"Repository\" + FolderBack[i], true);
                        CurrentUpdate = DateTime.Now.ToString("dd.MM.yyyy");
                        System.IO.File.Delete(SystemConecto.PutchApp + @"bin\" + FolderBack[i]);
                    }

                }
 
            }
            if (CurrentUpdate  != "") Administrator.AdminPanels.UpdateKeyReestr("UpGreateB52", CurrentUpdate);

        }
        // процедура создания копии БД в качестве спутника для записи логирования.
        public static void SatelliteCreatBd(string PuthSatellite, string fb2530 = "")
        {

            if(!System.IO.File.Exists(PuthSatellite))System.IO.File.Copy(Administrator.AdminPanels.SelectPuth, PuthSatellite, true);

            Administrator.AdminPanels.NameObj = fb2530.Contains("25") ? "CleanConect25": "CleanConect30";
            Administrator.AdminPanels.hubdate = fb2530;
            System.Windows.Application.Current.Dispatcher.Invoke(new Action(delegate ()
            {
                Administrator.AdminPanels ConectoWorkSpace_On = AppStart.LinkMainWindow("WAdminPanels");
                ConectoWorkSpace_On.ModifiConect(Administrator.AdminPanels.NameObj, PuthSatellite);
            }));

        }
        // процедура удаления спутника  БД.
        public static void SatelliteDeletbd()
        { 


        
        }

        // Процедура копирования логов
        public static void CopyLogSatellite(object ThreadObj)
        {



            AppStart.RenderInfo renderInfo = (AppStart.RenderInfo)ThreadObj;
            DateTime dateS = Convert.ToDateTime(renderInfo.argument1);
            DateTime dateP = Convert.ToDateTime(renderInfo.argument2);
            string str1 = renderInfo.argument3;
            string str2 = renderInfo.argument4;
            string CmdRun = Administrator.AdminPanels.CurrentSereverPuth + (Administrator.AdminPanels.hubdate == "25" ? "bin\\" : "") + "gfix.exe";
            string str3 = Administrator.AdminPanels.hubdate.Contains("25") ? Administrator.AdminPanels.SetPort25 : Administrator.AdminPanels.SetPort30;
            string str4 = AppStart.TableReestr["SetTextIp4BackOf"] + "/" + str3 + ":" + Administrator.AdminPanels.SelectAlias.Trim();
            string str5 = Administrator.AdminPanels.hubdate.Contains("25") ? Administrator.AdminPanels.CurrentPasswFb25 : Administrator.AdminPanels.CurrentPasswFb30;
            Administrator.AdminPanels.LoadListTablLog();
            if (Administrator.AdminPanels.CountTabLog == 0)
            {
                System.Windows.Application.Current.Dispatcher.Invoke(new Action(delegate ()
                {
                    ConectoWorkSpace.WaitMessage ConectoWorkSpace_Off = AppStart.LinkMainWindow("WaitMessage_");
                    ConectoWorkSpace_Off.Close();
                }));
                InstallB52.MessageEnd("Список имен таблиц логов пустой. " + Environment.NewLine + " Копирование прекращено.", 1, -170, 600);
            }
            else
            {
                InstallB52.ListTableLog = new string[Administrator.AdminPanels.CountTabLog];
                InstallB52.ArryCopyDate = new string[Administrator.AdminPanels.CountTabLog];
                InstallB52.ArryFiltr = new string[Administrator.AdminPanels.CountTabLog];
                int index1 = 0;
                string cmdText = "SELECT * from LISTTABLLOG";
                DBConecto.DBopenFBConectionMemory(SystemBD.ConnnStringSystem("Dynamic").ToString(), "", "FbSystem");
                FbCommand fbCommand = new FbCommand(cmdText, (FbConnection)DBConecto.bdFbSystemConect);
                FbDataReader fbDataReader = fbCommand.ExecuteReader();
                while (fbDataReader.Read())
                {
                    InstallB52.ListTableLog[index1] = fbDataReader[0].ToString().Trim();
                    InstallB52.ArryCopyDate[index1] = fbDataReader[1].ToString().Trim();
                    InstallB52.ArryFiltr[index1] = fbDataReader[2].ToString().Trim();
                    ++index1;
                }
                fbDataReader.Dispose();
                fbCommand.Dispose();
                DBConecto.DBcloseFBConectionMemory("FbSystem");
                FbConnection.ClearAllPools();
                Administrator.AdminPanels.ConectionFb(Administrator.AdminPanels.hubdate, "BdB52Satellite");
                DateTime dateTime = DateTime.Now;
                SystemConecto.ErorDebag("Старт логирования" + dateTime.ToString("yyMMddHHmmss"), 1, 2, (SystemConecto.StruErrorDebag)null);
                Administrator.AdminPanels.BlokOpisConecto(Administrator.AdminPanels.hubdate);
                FbConnection bdFBConect = new FbConnection(Administrator.AdminPanels.BdB52);
                bdFBConect.Open();
                Administrator.AdminPanels.InitListTrigerLog(bdFBConect);
                InstallB52.LogInactivbd(bdFBConect);
                ThreadPool.GetMaxThreads(out int _, out int _);
                Stopwatch stopwatch = new Stopwatch();
                using (AutoResetEvent e = new AutoResetEvent(false))
                {
                    stopwatch.Start();
                    int countTabLog = Administrator.AdminPanels.CountTabLog;
                    int workerThreads = countTabLog;
                    for (int index2 = 0; index2 < countTabLog; ++index2)
                    {
                        ++InstallB52.ThreadStart;
                        string[] strArray = new string[17];
                        strArray[0] = "SELECT *  FROM ";
                        strArray[1] = InstallB52.ListTableLog[index2];
                        strArray[2] = " WHERE ";
                        strArray[3] = InstallB52.ListTableLog[index2];
                        strArray[4] = ".";
                        strArray[5] = InstallB52.ArryFiltr[index2];
                        strArray[6] = " >= '";
                        dateTime = dateS.Date;
                        strArray[7] = dateTime.ToString();
                        strArray[8] = "' AND ";
                        strArray[9] = InstallB52.ListTableLog[index2];
                        strArray[10] = ".";
                        strArray[11] = InstallB52.ArryFiltr[index2];
                        strArray[12] = " <='";
                        dateTime = dateP.Date;
                        strArray[13] = dateTime.ToString();
                        strArray[14] = "' ORDER BY ";
                        strArray[15] = InstallB52.ArryFiltr[index2];
                        strArray[16] = " ASC";
                        string StrFirst = string.Concat(strArray);
                        string TableLog = InstallB52.ListTableLog[index2];
                        ThreadPool.QueueUserWorkItem((WaitCallback)(x =>
                        {
                            InstallB52.MethodColl(StrFirst, dateS, dateP, TableLog);
                            if (Interlocked.Decrement(ref workerThreads) != 0)
                                return;
                            e.Set();
                        }));
                    }
                    e.WaitOne();
                    stopwatch.Stop();
                    e.Dispose();
                    e.Close();
                }
                while (InstallB52.ThreadStart > 0) Thread.Sleep(1000);

                InstallB52.LogActivBd(bdFBConect);
                bdFBConect.Dispose();
                bdFBConect.Close();
                Administrator.AdminPanels.ArgumentCmd = "-sweep " + str4 + " -user sysdba -pass " + str5;
                SystemConecto.ErorDebag(CmdRun + Administrator.AdminPanels.ArgumentCmd, 1, 2, (SystemConecto.StruErrorDebag)null);
                Administrator.AdminPanels.RunProcess(CmdRun, Administrator.AdminPanels.ArgumentCmd, "sweep");
                int num = 1;
                while (num > 0)
                {
                    if ((uint)Process.GetProcessesByName("gfix").Length > 0U)
                        Thread.Sleep(1000);
                    else
                        num = 0;
                }
                FbConnection.ClearAllPools();
                if (!Directory.Exists("c:\\BackupFdb\\")) Directory.CreateDirectory("c:\\BackupFdb\\");

                Administrator.AdminPanels.RunGbak = Administrator.AdminPanels.CurrentSereverPuth + (Administrator.AdminPanels.hubdate == "25" ? "bin\\gbak.exe" : "gbak.exe");
                string str6 = "  c:\\BackupFdb\\" + Administrator.AdminPanels.SelectAlias.Trim() + DateTime.Now.ToString("yyMMddHHmmss") + ".fbk";
                Administrator.AdminPanels.ArgumentCmd = " -b " + str4 + str6 + " -v  -user sysdba -pass " + str5;
                SystemConecto.ErorDebag("Перваый этап создание архива БД" + Administrator.AdminPanels.RunGbak + Administrator.AdminPanels.ArgumentCmd, 1, 2, (SystemConecto.StruErrorDebag)null);
                if (!System.IO.File.Exists(Administrator.AdminPanels.RunGbak))
                {
                    Administrator.AdminPanels.NameObj = "";
                    InstallB52.MessageErorInst("Отсутствует файл gbak.exe.Пересборка БД прекращена. " + Environment.NewLine + "Необходимо проверить наличие утилиты gbak.exe");
                }
                else
                {
                    Administrator.AdminPanels.RunProcess(Administrator.AdminPanels.RunGbak, Administrator.AdminPanels.ArgumentCmd, "");
                    System.IO.File.Copy(Administrator.AdminPanels.SelectPuth, SystemConectoServers.PutchServerData + Administrator.AdminPanels.SelectAlias.Trim() + DateTime.Now.ToString("yyMMddHHmmss") + ".fdb");
                    Administrator.AdminPanels.ArgumentCmd = " -rep " + str6 + "  " + str4 + "  -bu 200 -v -user sysdba -pass " + str5;
                    SystemConecto.ErorDebag("Второй этап разархивирование архива" + Administrator.AdminPanels.RunGbak + Administrator.AdminPanels.ArgumentCmd, 1, 2, (SystemConecto.StruErrorDebag)null);
                    Administrator.AdminPanels.RunProcess(Administrator.AdminPanels.RunGbak, Administrator.AdminPanels.ArgumentCmd, "");
                }
                System.Windows.Application.Current.Dispatcher.Invoke(new Action(delegate ()
                {
                    ConectoWorkSpace.WaitMessage ConectoWorkSpace_Off = AppStart.LinkMainWindow("WaitMessage_");
                    ConectoWorkSpace_Off.Close();
                }));
                InstallB52.MessageEnd("Копирование логов " + Environment.NewLine + " успешно завершено.", 1, -170, 600);
                SystemConecto.ErorDebag("Конец  логорования" + DateTime.Now.ToString("yyMMddHHmmss"), 1, 2, (SystemConecto.StruErrorDebag)null);

                System.Windows.Application.Current.Dispatcher.Invoke(new Action(delegate ()
                {

                    ConectoWorkSpace.Administrator.AdminPanels ConectoWorkSpace_On = AppStart.LinkMainWindow("WAdminPanels");
                    ConectoWorkSpace_On.TablAlias.IsEnabled = true;
                    ConectoWorkSpace_On.TablAlias30.IsEnabled = true;

                    if (Administrator.AdminPanels.hubdate == "25")
                    {
                        ConectoWorkSpace_On.CopyLogBD25.Visibility = Visibility.Collapsed;
                        ConectoWorkSpace_On.CopyLogPo.Visibility = Visibility.Collapsed;
                        ConectoWorkSpace_On.datePicker25_CopyLogS.Visibility = Visibility.Collapsed;
                        ConectoWorkSpace_On.datePicker25_CopyLogP.Visibility = Visibility.Collapsed;
                        ConectoWorkSpace_On.BackUpLocServerBD25.Visibility = Visibility.Collapsed;
                        ConectoWorkSpace_On.LabelCopyLog.Foreground = (Brush)Brushes.White;
                        ConectoWorkSpace_On.LabDelLogIn.Visibility = Visibility.Collapsed;
                        ConectoWorkSpace_On.DelLogIn_OnOff.Visibility = Visibility.Collapsed;
                    }
                    if (Administrator.AdminPanels.hubdate == "30")
                    { 
                    
                        ConectoWorkSpace_On.CopyLogBD30.Visibility = Visibility.Collapsed;
                        ConectoWorkSpace_On.CopyLogPo30.Visibility = Visibility.Collapsed;
                        ConectoWorkSpace_On.datePicker30_CopyLogS.Visibility = Visibility.Collapsed;
                        ConectoWorkSpace_On.datePicker30_CopyLogP.Visibility = Visibility.Collapsed;
                        ConectoWorkSpace_On.BackUpLocServerBD30.Visibility = Visibility.Collapsed;
                        ConectoWorkSpace_On.CreateBackUpBd30.Foreground = (Brush)Brushes.White; 
                    }
                      
 
                }));
                --InstallB52.IntThreadStart;
                Administrator.AdminPanels.SetWinSetHub = "";
            }

            //--------------------------------------------------------------           

        
        }

        private static void MethodColl(
          string StrFirst,
          DateTime dateS,
          DateTime dateP,
          string TablrLog)
        {
            string[] strArray1 = new string[0];
            string[] strArray2 = new string[0];
            int num1 = 0;
            int num2 = 0;
            int index1 = -1;
            int num3 = 0;
            try
            {
                FbConnection connection1 = new FbConnection(Administrator.AdminPanels.BdB52);
                connection1.Open();
                DataTable dataTable = new DataTable();
                FbCommand fbCommand1 = new FbCommand(StrFirst, connection1);
                FbDataReader fbDataReader1 = fbCommand1.ExecuteReader();
                while (fbDataReader1.Read())
                {
                    if (num1 == 0)
                    {
                        strArray1 = new string[fbDataReader1.FieldCount];
                        strArray2 = new string[fbDataReader1.FieldCount];
                        DataTable schemaTable = fbDataReader1.GetSchemaTable();
                        for (int index2 = 0; index2 < fbDataReader1.FieldCount; ++index2)
                        {
                            strArray1[index2] = schemaTable.Rows[index2]["ColumnName"].ToString();
                            strArray2[index2] = schemaTable.Rows[index2]["DataType"].ToString();
                        }
                    }
                    for (int index2 = 0; index2 < fbDataReader1.FieldCount; ++index2)
                    {
                        if ((uint)fbDataReader1[index2].ToString().Length > 0U)
                        {
                            num1 = num1 == 0 ? Convert.ToInt32(fbDataReader1[index2].ToString()) : num1;
                            index1 = num1 == 0 || index1 != -1 ? index1 : index2;
                            num2 = Convert.ToInt32(fbDataReader1[index2].ToString());
                            if (num2 != 0 && index1 != -1)
                                break;
                        }
                    }
                    try
                    {
                        int num4 = 0;
                        string cmdText = "SELECT * FROM " + TablrLog + " WHERE " + strArray1[index1] + "=" + num2.ToString();
                        FbConnection connection2 = new FbConnection(Administrator.AdminPanels.BdB52Satellite);
                        connection2.Open();
                        FbDataReader fbDataReader2 = new FbCommand(cmdText, connection2).ExecuteReader();
                        while (fbDataReader2.Read())
                            ++num4;
                        fbDataReader2.Close();
                        if (num4 == 0)
                        {
                            string str1 = "INSERT INTO " + TablrLog + "(";
                            string str2 = " VALUES (";
                            for (int index2 = 0; index2 < fbDataReader1.FieldCount; ++index2)
                            {
                                if ((uint)fbDataReader1[index2].ToString().Length > 0U)
                                {
                                    string str3 = strArray2[index2].Contains("Double") ? fbDataReader1[index2].ToString().Replace(",", ".") : fbDataReader1[index2].ToString();
                                    str1 = str1 + (str1.Length == str1.IndexOf("(") + 1 ? "" : ",") + strArray1[index2];
                                    str2 = str2 + (str2.Length == str2.IndexOf("(") + 1 ? "" : ",") + (strArray2[index2].Contains("String") ? "'" : (strArray2[index2].Contains("Date") ? "'" : "")) + str3 + (strArray2[index2].Contains("String") ? "'" : (strArray2[index2].Contains("Date") ? "'" : ""));
                                }
                            }
                            FbCommand fbCommand2 = new FbCommand(str1 + ")" + str2 + ")", connection2);
                            fbCommand2.CommandType = CommandType.Text;
                            fbCommand2.ExecuteScalar();
                            fbCommand2.Dispose();
                            ++num3;
                        }
                        connection2.Close();
                    }
                    catch (FbException ex)
                    {
                        SystemConecto.ErorDebag(" возникло исключение: " + Environment.NewLine + " === IDCode: " + ex.ErrorCode.ToString() + Environment.NewLine + " === Message: " + ex.Message.ToString() + Environment.NewLine + " === Exception: " + ex.ToString(), 1, 0, (SystemConecto.StruErrorDebag)null);
                        return;
                    }
                    catch (Exception ex)
                    {
                        SystemConecto.ErorDebag("возникло исключение:" + Environment.NewLine + " возникло исключение: " + Environment.NewLine + " === Message: " + ex.Message.ToString() + Environment.NewLine + " === Exception: " + ex.ToString(), 1, 0, (SystemConecto.StruErrorDebag)null);
                        return;
                    }
                }
                fbDataReader1.Close();
                fbCommand1.Dispose();
                connection1.Dispose();
                connection1.Close();
                if (Convert.ToInt16(AppStart.TableReestr["OnOffLog_Del"]) == (short)2 && num1 > 0)
                {
                    if (num1 > num2)
                    {
                        int num4 = num1;
                        num1 = num2;
                        num2 = num4;
                    }
                    string str = "DELETE FROM " + TablrLog + " WHERE (" + strArray1[index1] + " >=" + num1.ToString() + ") AND (" + strArray1[index1] + "<=" + num2.ToString() + ")";
                    AppStart.RenderInfo renderInfo = new AppStart.RenderInfo();
                    renderInfo.argument1 = str;
                    Thread thread = new Thread(new System.Threading.ParameterizedThreadStart(InstallB52.DelLogTabTrashBD));
                    thread.SetApartmentState(ApartmentState.STA);
                    thread.IsBackground = true;
                    thread.Start((object)renderInfo);
                    ++InstallB52.IntThreadStart;
                }
            }
            catch (FbException ex)
            {
                SystemConecto.ErorDebag(" возникло исключение: " + Environment.NewLine + " === IDCode: " + ex.ErrorCode.ToString() + Environment.NewLine + " === Message: " + ex.Message.ToString() + Environment.NewLine + " === Exception: " + ex.ToString(), 1, 0, (SystemConecto.StruErrorDebag)null);
                --InstallB52.ThreadStart;
                return;
            }
            catch (Exception ex)
            {
                SystemConecto.ErorDebag("возникло исключение:" + Environment.NewLine + " возникло исключение: " + Environment.NewLine + " === Message: " + ex.Message.ToString() + Environment.NewLine + " === Exception: " + ex.ToString(), 1, 0, (SystemConecto.StruErrorDebag)null);
                --InstallB52.ThreadStart;
                return;
            }
            if ((uint)num3 > 0U)
            {
                string str = "UPDATE LISTTABLLOG SET DATECOPY = '" + dateP.ToString() + "' WHERE NAMETABLE = '" + TablrLog + "'";
                DBConecto.DBopenFBConectionMemory(SystemBD.ConnnStringSystem("Dynamic").ToString(), "", "FbSystem");
                new DBConecto.UniQuery(str, "FB").ExecuteUNIScalar("");
                DBConecto.DBcloseFBConectionMemory("FbSystem");
            }
            if (Convert.ToInt16(AppStart.TableReestr["OnOffLog_Del"]) != (short)2 || num1 != 0)
                return;
            --InstallB52.ThreadStart;
        }

        public static void CopySecurityRestory()
        {
            if (Administrator.AdminPanels.Inst2530 == "Security25")
                System.IO.File.Copy(Administrator.AdminPanels.NameSecurity2, Administrator.AdminPanels.SetPuth25 + "\\security2.fdb", true);
            if (!(Administrator.AdminPanels.Inst2530 == "Security30"))
                return;
            System.IO.File.Copy(Administrator.AdminPanels.NameSecurity3, Administrator.AdminPanels.SetPuth30 + "\\security3.fdb", true);
        }

        public static void DelLogTabTrashBD(object ThreadObj)
        {
            string cmdText = ((AppStart.RenderInfo)ThreadObj).argument1;
            FbConnection connection = new FbConnection(Administrator.AdminPanels.BdB52);
            connection.Open();
            FbCommand fbCommand = new FbCommand(cmdText, connection);
            fbCommand.ExecuteScalar();
            fbCommand.Dispose();
            connection.Dispose();
            connection.Close();
            --InstallB52.ThreadStart;
        }

 

    }
}
