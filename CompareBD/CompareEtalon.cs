using System;
using System.Collections.Generic;

using System.Linq;
using System.Text;
using System.Windows;
using System.Net;
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

using System.Net.Sockets;
// Анимированный гиф
using WpfAnimatedGif;

using mshtml;
using ConectoWorkSpace;



namespace ConectoWorkSpace
{

    
    public class TableProcedure
    {
        public TableProcedure(string Name, string Hash, string Sourse)
        {
            this.Name = Name;
            this.Hash = Hash;
            this.Sourse = Sourse;
        }

        public string Name { get; set; }
        public string Hash { get; set; }
        public string Sourse { get; set; }


        //RDB$PROCEDURE_NAME CHAR(31) Имя процедуры
        //RDB$PROCEDURE Hash Контрольная сумма по коду процедуры
        //RDB$PROCEDURE_SOURCE BLOB TEXT Исходный код процедуры


    }

    
    public static class CompareBD
    {

        public static string[] DevelopName , DevelopHash, DevelopSourse, DistrybutName, DistrybutHash, DistrybutSourse;
        public static string[] DevelopNameFunction, DevelopHashFunction, DevelopSourseFunction, DistrybutNameFunction, DistrybutHashFunction, DistrybutSourseFunction;
        public static string[] DevelopNameTable,  DevelopMasName, DevelopMasField, DevelopMasType;
        public static string[] DistrybutNameTable, DistrybutMasName, DistrybutMasField, DistrybutMasType;
        public static string PuthSrv = "", PortSrv = "";
        public static List<TableProcedure> Develop = new List<TableProcedure>(1);
        public static List<TableProcedure> Distrybut = new List<TableProcedure>(1);
        public static string StrokaNameDevelop = "", StrokaNameDistrybut = "", StrokaNameTableDevelop = "", StrokaNameTableDistrybut = "", StrokaNameFuncDevelop = "", StrokaNameFuncDistrybut = "";
        public static int CountDevelop = 0, CountDistrybut = 0, CountTableDevelop = 0, CountTableDistrybut = 0, CountFunction =0, CountFunctionDevelop = 0, CountFunctionDistrybut = 0;
        public static int IndWrite = 0;
        // Процедура сравнения двух еталонов

        public static void PortServer()
        {

           string StrCreate = "SELECT * from SERVERACTIVFB25 WHERE SERVERACTIVFB25.NAME =" + "'" + AppStart.TableReestr["NameServer25"] + "'";
            DBConecto.DBopenFBConectionMemory(SystemBD.ConnnStringSystem().ToString(), "", "FbSystem");
            FbCommand SelectTable = new FbCommand(StrCreate, DBConecto.bdFbSystemConect);
            FbDataReader ReadOutTable = SelectTable.ExecuteReader();
            while (ReadOutTable.Read())
            {
                PortSrv = ReadOutTable[0].ToString();
                PuthSrv = ReadOutTable[1].ToString();
            }
            ReadOutTable.Close();
            DBConecto.DBcloseFBConectionMemory("FbSystem");
        }

        public static void LoadProcedure(string portBd, string BDPuth, string NameFile)
        {

 
            string  ErrMessage = "";
            string RdbProcCount = "SELECT count(*) FROM RDB$PROCEDURES";
            string strCmd = "SELECT t.RDB$PROCEDURE_NAME, cast(HASH(t.RDB$PROCEDURE_SOURCE) as VARCHAR (100)),  t.RDB$PROCEDURE_SOURCE  FROM RDB$PROCEDURES t";
            string RdbFunctionCount = "SELECT count(*) FROM RDB$TRIGGERS";
            string TableFunction = "SELECT f.RDB$TRIGGER_NAME,cast(HASH(f.RDB$TRIGGER_SOURCE) as VARCHAR (100)),f.RDB$TRIGGER_SOURCE  FROM RDB$TRIGGERS f"; //cast(HASH(f.RDB$FUNCTION_SOURCE) as VARCHAR (100)), f.RDB$FUNCTION_SOURCE 
            string TableCount = "SELECT count(RDB$RELATION_NAME) FROM RDB$RELATIONS";
            string strTable = "SELECT t.RDB$FIELD_ID, t.RDB$RELATION_NAME, t.RDB$RELATION_TYPE  FROM RDB$RELATIONS t"; //RDB$FIELD_ID RDB$RELATION_TYPE



            DBConecto.ParamStringServerFB[1] = "SYSDBA";
            DBConecto.ParamStringServerFB[2] = Administrator.AdminPanels.CurrentPasswFb25;
            DBConecto.ParamStringServerFB[3] = "TCP/IP";
            DBConecto.ParamStringServerFB[4] = AppStart.TableReestr["SetTextIp4BackOf"] + "/" + portBd + ":" + BDPuth;
            DBConecto.ParamStringServerFB[5] = BDPuth;

 
            int Idcount = 0;
            var EtalonDeveloper = DBConecto.StringServerFB();
            FbConnection bdFBConect = new FbConnection(EtalonDeveloper.ToString());
            try
            {
                bdFBConect.Open();
                // Считаем количество функций в БД
                FbCommand CountFunc = new FbCommand(RdbFunctionCount, bdFBConect);
                CountFunc.CommandType = CommandType.Text;
                string AllCountFunc = CountFunc.ExecuteScalar().ToString();
                CountFunc.Dispose();

                // Считаем количество таблиц в БД
                FbCommand CountTableDb = new FbCommand(TableCount, bdFBConect);
                CountTableDb.CommandType = CommandType.Text;
                string AllCountTable = CountTableDb.ExecuteScalar().ToString();
                CountTableDb.Dispose();

                // Считаем количество процедур в БД
                FbCommand CountProc = new FbCommand(RdbProcCount, bdFBConect);
                CountProc.CommandType = CommandType.Text;
                string CountTable = CountProc.ExecuteScalar().ToString();
                if (NameFile == "DEVELOP")
                {
                    DevelopName = new string[Convert.ToInt16(CountTable)];
                    DevelopHash = new string[Convert.ToInt16(CountTable)];
                    DevelopSourse = new string[Convert.ToInt16(CountTable)];
                    CountDevelop = Convert.ToInt16(CountTable);

                    DevelopNameTable = new string[Convert.ToInt16(AllCountTable)];
                    DevelopMasName = new string[Convert.ToInt16(AllCountTable)];
                    DevelopMasField = new string[Convert.ToInt16(AllCountTable)];
                    DevelopMasType = new string[Convert.ToInt16(AllCountTable)];
                    CountTableDevelop = Convert.ToInt16(AllCountTable);
   
                    DevelopNameFunction = new string[Convert.ToInt16(AllCountFunc)];
                    DevelopHashFunction = new string[Convert.ToInt16(AllCountFunc)];
                    DevelopSourseFunction = new string[Convert.ToInt16(AllCountFunc)];
                    CountFunctionDevelop = Convert.ToInt16(AllCountFunc);
                }

                if (NameFile == "DISTRYBUT")
                {
                    DistrybutName = new string[Convert.ToInt16(CountTable)];
                    DistrybutHash = new string[Convert.ToInt16(CountTable)];
                    DistrybutSourse = new string[Convert.ToInt16(CountTable)];
                    CountDistrybut = Convert.ToInt16(CountTable);

                    DistrybutNameTable = new string[Convert.ToInt16(AllCountTable)];
                    DistrybutMasName = new string[Convert.ToInt16(AllCountTable)];
                    DistrybutMasField = new string[Convert.ToInt16(AllCountTable)];
                    DistrybutMasType = new string[Convert.ToInt16(AllCountTable)];
                    CountTableDistrybut = Convert.ToInt16(AllCountTable);

                    DistrybutNameFunction = new string[Convert.ToInt16(AllCountFunc)];
                    DistrybutHashFunction = new string[Convert.ToInt16(AllCountFunc)];
                    DistrybutSourseFunction = new string[Convert.ToInt16(AllCountFunc)];
                    CountFunctionDistrybut = Convert.ToInt16(AllCountFunc);
                }
                CountProc.Dispose();
                // формируем список процедур 
                FbCommand UpdateKey = new FbCommand(strCmd, bdFBConect);
                UpdateKey.CommandType = CommandType.Text;
                FbDataReader reader = UpdateKey.ExecuteReader();

                while (reader.Read())
                {

                    if (NameFile == "DEVELOP")
                    {
                        DevelopName[Idcount] = reader[0].ToString();
                        DevelopHash[Idcount] = reader[1].ToString();
                        DevelopSourse[Idcount] = reader[2].ToString();
                        StrokaNameDevelop = StrokaNameDevelop + DevelopName[Idcount] + ";" + Idcount.ToString() + ";";
                    }
 
                    if (NameFile == "DISTRYBUT")
                    {
                        DistrybutName[Idcount] = reader[0].ToString();
                        DistrybutHash[Idcount] = reader[1].ToString();
                        DistrybutSourse[Idcount] = reader[2].ToString();
                        StrokaNameDistrybut = StrokaNameDistrybut + DistrybutName[Idcount] + ";" + Idcount.ToString() + ";";
                    }
                    Idcount++;
 
                }
                reader.Close();
                UpdateKey.Dispose();
                // формируем массив таблиц 
                Idcount = 0;
                FbCommand GreateMasTable = new FbCommand(strTable, bdFBConect);
                GreateMasTable.CommandType = CommandType.Text;
                FbDataReader Tablereader = GreateMasTable.ExecuteReader();

                while (Tablereader.Read())
                {

                    if (NameFile == "DEVELOP")
                    {

                        DevelopMasField[Idcount] = Tablereader[0].ToString();
                        DevelopMasName[Idcount] = Tablereader[1].ToString();
                        DevelopMasType[Idcount] = Tablereader[2].ToString();
                        StrokaNameTableDevelop = StrokaNameTableDevelop + DevelopMasName[Idcount] + ";" + Idcount.ToString() + ";";

                    }

                    if (NameFile == "DISTRYBUT")
                    {
                        DistrybutMasField[Idcount] = Tablereader[0].ToString();
                        DistrybutMasName[Idcount] = Tablereader[1].ToString();
                        DistrybutMasType[Idcount] = Tablereader[2].ToString();
                        StrokaNameTableDistrybut = StrokaNameTableDistrybut + DistrybutMasName[Idcount] + ";" + Idcount.ToString() + ";";

                    }
                    Idcount++;

                }
                Tablereader.Close();
                GreateMasTable.Dispose();
                // формируем массив функций 
                Idcount = 0;
                FbCommand GreateTableFunc = new FbCommand(TableFunction, bdFBConect);
                GreateTableFunc.CommandType = CommandType.Text;
                FbDataReader TableFuncreader = GreateTableFunc.ExecuteReader();

                while (TableFuncreader.Read())
                {

                    if (NameFile == "DEVELOP")
                    {


                        DevelopNameFunction[Idcount] = TableFuncreader[0].ToString();
                        DevelopHashFunction[Idcount] = TableFuncreader[1].ToString();
                        DevelopSourseFunction[Idcount] = TableFuncreader[2].ToString();
                        StrokaNameFuncDevelop = StrokaNameFuncDevelop + DevelopNameFunction[Idcount] + ";" + Idcount.ToString() + ";";

                    }

                    if (NameFile == "DISTRYBUT")
                    {
                        DistrybutNameFunction[Idcount] = TableFuncreader[0].ToString();
                        DistrybutHashFunction[Idcount] = TableFuncreader[1].ToString();
                        DistrybutSourseFunction[Idcount] = TableFuncreader[2].ToString();
                        StrokaNameFuncDistrybut = StrokaNameFuncDistrybut + DistrybutNameFunction[Idcount] + ";" + Idcount.ToString() + ";";

                    }
                    Idcount++;

                }
                TableFuncreader.Close();
                GreateTableFunc.Dispose();




            }
            catch (FirebirdSql.Data.FirebirdClient.FbException ex)
            {
                SystemConecto.ErorDebag(" возникло исключение: " + Environment.NewLine +
                           " === IDCode: " + ex.ErrorCode.ToString() + Environment.NewLine +
                           " === Message: " + ex.Message.ToString() + Environment.NewLine +
                           " === Exception: " + ex.ToString(), 1);
                ErrMessage = "Обнаружена ошибка в БД." + Environment.NewLine + ex.ToString().Substring(ex.ToString().IndexOf(Environment.NewLine) + 2);
                
            }
            catch (Exception ex) //)
            {
                SystemConecto.ErorDebag("возникло исключение:" + Environment.NewLine +
                        " возникло исключение: " + Environment.NewLine +
                        " === Message: " + ex.Message.ToString() + Environment.NewLine +
                        " === Exception: " + ex.ToString(), 1);
                ErrMessage = "Обнаружена ошибка в БД." + Environment.NewLine + ex.ToString().Substring(ex.ToString().IndexOf(Environment.NewLine) + 2);
                
            }
            bdFBConect.Dispose();
            bdFBConect.Close();
     
        }

        public static void RunCompareBD()
        {

            #region Чтение и сравние файлов или строк
            string ProcSourse = SystemConecto.PutchApp + @"data\ProcSourseCompare.txt";
            string FuncSourse = SystemConecto.PutchApp + @"data\FuncSourseCompare.txt";
            FileStream fstream = new FileStream(ProcSourse, FileMode.OpenOrCreate);
            FileStream Funcstream = new FileStream(FuncSourse, FileMode.OpenOrCreate);
            string StrCreate = "", StrInsert ="";
            PortServer();
            LoadProcedure(PortSrv, Administrator.AdminPanels.EtalonDeveloperPuth, "DEVELOP");
            LoadProcedure(PortSrv, Administrator.AdminPanels.EtalonDistributorPuth, "DISTRYBUT");
    
            // Сравнение процедур
            for (int i = 0; i < CountDevelop; i++)
            {
  
                if (!StrokaNameDistrybut.Contains(DevelopName[i]))
                {
                    StrCreate = "select * from REGISTERCOMPARE where REGISTERCOMPARE.NAME_PROCEDURE = " + "'" + DevelopName[i] + "'";
                    StrInsert = "INSERT INTO REGISTERCOMPARE  values ('" + Administrator.AdminPanels.EtalonDeveloperPuth + "', '" +
                    Administrator.AdminPanels.EtalonDistributorPuth + "', '" + DateTime.Now.ToString("yyyyMMddHHmmss") + "', '" +
                    DevelopName[i] + "', '', '','No')";
                    InsertFb(StrCreate, StrInsert);
                    byte[] array = System.Text.Encoding.Default.GetBytes("<----->" + Environment.NewLine + DevelopSourse[i] + Environment.NewLine);
                    fstream.Write(array, 0, array.Length);

                }
                else
                {
                        string tmp = StrokaNameDistrybut.Substring(StrokaNameDistrybut.IndexOf(DevelopName[i] + ";") + DevelopName[i].Length + 1,
                        StrokaNameDistrybut.Length-(StrokaNameDistrybut.IndexOf(DevelopName[i] + ";") + DevelopName[i].Length + 1));
                        int IndexHesh = Convert.ToInt16(tmp.Substring(0,tmp.IndexOf(";")));

                    if (DevelopHash[i] != DistrybutHash[IndexHesh])
                    {

                        StrCreate = "select * from REGISTERCOMPARE where REGISTERCOMPARE.NAME_PROCEDURE = " + "'" + DevelopName[i] + "'";
                        StrInsert = "INSERT INTO REGISTERCOMPARE  values ('" + Administrator.AdminPanels.EtalonDeveloperPuth + "', '" +
                        Administrator.AdminPanels.EtalonDistributorPuth + "', '" + DateTime.Now.ToString("yyyyMMddHHmmss") + "', '" +
                        DevelopName[i] + "', '', '','Hash')";
                        InsertFb(StrCreate, StrInsert);
                        // преобразуем строку в байты
                        byte[] array = System.Text.Encoding.Default.GetBytes("<----->" + Environment.NewLine + DevelopSourse[i] + Environment.NewLine + "<----->" + Environment.NewLine + DistrybutSourse[IndexHesh] + Environment.NewLine);
                        fstream.Write(array, 0, array.Length);
 
                    }

                }
        
            }

            for (int i = 0; i < CountDistrybut; i++)
            {

                if (!StrokaNameDevelop.Contains(DistrybutName[i]))
                {
     
                    StrCreate = "select * from REGISTERCOMPARE where REGISTERCOMPARE.NAME_PROCEDURE = " + "'" + DistrybutName[i] + "'";
                    StrInsert = "INSERT INTO REGISTERCOMPARE  values ('" + Administrator.AdminPanels.EtalonDeveloperPuth + "', '" +
                    Administrator.AdminPanels.EtalonDistributorPuth + "', '" + DateTime.Now.ToString("yyyyMMddHHmmss") + "', '" +
                    DistrybutName[i] + "', '', '','No')";
                    InsertFb(StrCreate, StrInsert);
                    byte[] array = System.Text.Encoding.Default.GetBytes("<----->" + Environment.NewLine + DistrybutSourse[i] + Environment.NewLine);
                    fstream.Write(array, 0, array.Length);
 
                }
                else
                {
                    string tmp = StrokaNameDevelop.Substring(StrokaNameDevelop.IndexOf(DistrybutName[i] + ";") + DistrybutName[i].Length + 1,
                    StrokaNameDevelop.Length - (StrokaNameDevelop.IndexOf(DistrybutName[i] + ";") + DistrybutName[i].Length + 1));
                    int IndexHesh = Convert.ToInt16(tmp.Substring(0, tmp.IndexOf(";")));
     
                    if (DistrybutHash[i] != DevelopHash[IndexHesh])
                    {

                        StrCreate = "select * from REGISTERCOMPARE where REGISTERCOMPARE.NAME_PROCEDURE = " + "'" + DistrybutName[i] + "'";
                        StrInsert = "INSERT INTO REGISTERCOMPARE  values ('" + Administrator.AdminPanels.EtalonDeveloperPuth + "', '" +
                        Administrator.AdminPanels.EtalonDistributorPuth + "', '" + DateTime.Now.ToString("yyyyMMddHHmmss") + "', '" +
                        DistrybutName[i] + "', '', '','Hash')";
                        InsertFb(StrCreate, StrInsert);
                        byte[] array = System.Text.Encoding.Default.GetBytes("<----->" + Environment.NewLine +DevelopSourse[IndexHesh] + Environment.NewLine + "<----->" + Environment.NewLine + DistrybutSourse[i] + Environment.NewLine);
                        fstream.Write(array, 0, array.Length);
 
                    }

                }

            }
            fstream.Close();

            //// Сравнение функций
            //for (int i = 0; i < CountFunctionDevelop; i++)
            //{

 

            //    if (!StrokaNameFuncDistrybut.Contains(DevelopNameFunction[i]))
            //    {
            //        StrCreate = "select * from REGISTERCOMPARE where REGISTERCOMPARE.NAME_TRIGER = " + "'" + DevelopNameFunction[i] + "'";
            //        StrInsert = "INSERT INTO REGISTERCOMPARE  values ('" + Administrator.AdminPanels.EtalonDeveloperPuth + "', '" +
            //        Administrator.AdminPanels.EtalonDistributorPuth + "', '" + DateTime.Now.ToString("yyyyMMddHHmmss") + "', '', '','" +
            //        DevelopNameFunction[i] + "', 'No')";
            //        InsertFb(StrCreate, StrInsert);
            //        byte[] array = System.Text.Encoding.Default.GetBytes("<----->" + Environment.NewLine+ DevelopNameFunction[i] + Environment.NewLine + 
            //        DevelopSourseFunction[i] + Environment.NewLine);
            //        Funcstream.Write(array, 0, array.Length);

            //    }
            //    else
            //    {
            //        string tmp = StrokaNameFuncDistrybut.Substring(StrokaNameFuncDistrybut.IndexOf(DevelopNameFunction[i] + ";") + DevelopNameFunction[i].Length + 1,
            //        StrokaNameFuncDistrybut.Length - (StrokaNameFuncDistrybut.IndexOf(DevelopNameFunction[i] + ";") + DevelopNameFunction[i].Length + 1));
            //        int IndexHesh = Convert.ToInt16(tmp.Substring(0, tmp.IndexOf(";")));

            //        if (DevelopHashFunction[i] != DistrybutHashFunction[IndexHesh])
            //        {

            //            StrCreate = "select * from REGISTERCOMPARE where REGISTERCOMPARE.NAME_TRIGER = " + "'" + DevelopNameFunction[i] + "'";
            //            StrInsert = "INSERT INTO REGISTERCOMPARE  values ('" + Administrator.AdminPanels.EtalonDeveloperPuth + "', '" +
            //            Administrator.AdminPanels.EtalonDistributorPuth + "', '" + DateTime.Now.ToString("yyyyMMddHHmmss") + "', '', '','" +
            //            DevelopNameFunction[i] + "', 'Hash')";
            //            InsertFb(StrCreate, StrInsert);
            //            // преобразуем строку в байты
            //            byte[] array = System.Text.Encoding.Default.GetBytes("<----->" + Environment.NewLine + DevelopNameFunction[i] + Environment.NewLine +
            //            DevelopSourseFunction[i] + Environment.NewLine + "<----->" + Environment.NewLine + DistrybutNameFunction[IndexHesh] + Environment.NewLine +
            //            DistrybutSourseFunction[IndexHesh] + Environment.NewLine);
            //            Funcstream.Write(array, 0, array.Length);

            //        }

            //    }

            //}

            //for (int i = 0; i < CountFunctionDistrybut; i++)
            //{

            //    if (!StrokaNameFuncDevelop.Contains(DistrybutNameFunction[i]))
            //    {

            //        StrCreate = "select * from REGISTERCOMPARE where REGISTERCOMPARE.NAME_TRIGER = " + "'" + DistrybutNameFunction[i] + "'";
            //        StrInsert = "INSERT INTO REGISTERCOMPARE  values ('" + Administrator.AdminPanels.EtalonDeveloperPuth + "', '" +
            //        Administrator.AdminPanels.EtalonDistributorPuth + "', '" + DateTime.Now.ToString("yyyyMMddHHmmss") + "', '', '','" +
            //        DistrybutNameFunction[i] + "', 'No')";
            //        InsertFb(StrCreate, StrInsert);
            //        byte[] array = System.Text.Encoding.Default.GetBytes("<----->" + Environment.NewLine + DistrybutNameFunction[i] + Environment.NewLine +
            //        DistrybutSourseFunction[i] + Environment.NewLine);
            //        Funcstream.Write(array, 0, array.Length);
            //    }
            //    else
            //    {
            //        string tmp = StrokaNameFuncDevelop.Substring(StrokaNameFuncDevelop.IndexOf(DistrybutHashFunction[i] + ";") + DistrybutHashFunction[i].Length + 1,
            //        StrokaNameFuncDevelop.Length - (StrokaNameFuncDevelop.IndexOf(DistrybutHashFunction[i] + ";") + DistrybutHashFunction[i].Length + 1));
            //        int IndexHesh = Convert.ToInt16(tmp.Substring(0, tmp.IndexOf(";")));

            //        if (DistrybutHashFunction[i] != DevelopHashFunction[IndexHesh])
            //        {

            //            StrCreate = "select * from REGISTERCOMPARE where REGISTERCOMPARE.NAME_TRIGER = " + "'" + DistrybutNameFunction[i] + "'";
            //            StrInsert = "INSERT INTO REGISTERCOMPARE  values ('" + Administrator.AdminPanels.EtalonDeveloperPuth + "', '" +
            //            Administrator.AdminPanels.EtalonDistributorPuth + "', '" + DateTime.Now.ToString("yyyyMMddHHmmss") + "', '', '','" +
            //            DistrybutNameFunction[i] + "', 'Hash')";
            //            InsertFb(StrCreate, StrInsert);
            //            byte[] array = System.Text.Encoding.Default.GetBytes("<----->" + Environment.NewLine + DevelopNameFunction[i] + Environment.NewLine +
            //            DevelopSourseFunction[i] + Environment.NewLine + "<----->" + Environment.NewLine + DistrybutNameFunction[IndexHesh] + Environment.NewLine +
            //            DistrybutSourseFunction[IndexHesh] + Environment.NewLine);
            //            Funcstream.Write(array, 0, array.Length);

            //        }

            //    }

            //}
            //Funcstream.Close();



            // Сравнение количества таблиц
            for (int i = 0; i < CountTableDevelop; i++)
            {
                if (!StrokaNameTableDistrybut.Contains(DevelopMasName[i]))
                {
                    StrCreate = "select * from REGISTERCOMPARE where REGISTERCOMPARE.NAME_TABLE = " + "'" + DevelopMasName[i] + "'";
                    StrInsert = "INSERT INTO REGISTERCOMPARE  values ('" + Administrator.AdminPanels.EtalonDeveloperPuth + "', '" +
                    Administrator.AdminPanels.EtalonDistributorPuth + "', '" + DateTime.Now.ToString("yyyyMMddHHmmss") + "', '','" +
                    DevelopMasName[i] + "',  '','No')";
                    InsertFb(StrCreate, StrInsert);

                }
                else
                {
                    string tmp = StrokaNameTableDistrybut.Substring(StrokaNameTableDistrybut.IndexOf(DevelopMasName[i] + ";") + DevelopMasName[i].Length + 1,
                    StrokaNameTableDistrybut.Length - (StrokaNameTableDistrybut.IndexOf(DevelopMasName[i] + ";") + DevelopMasName[i].Length + 1));
                    int IndexHesh = Convert.ToInt16(tmp.Substring(0, tmp.IndexOf(";")));

                    if (DevelopMasField[i] != DistrybutMasField[IndexHesh])
                    {

                        StrCreate = "select * from REGISTERCOMPARE where REGISTERCOMPARE.NAME_TABLE = " + "'" + DevelopMasName[i] + "'";
                        StrInsert = "INSERT INTO REGISTERCOMPARE  values ('" + Administrator.AdminPanels.EtalonDeveloperPuth + "', '" +
                        Administrator.AdminPanels.EtalonDistributorPuth + "', '" + DateTime.Now.ToString("yyyyMMddHHmmss") + "', '','" +
                        DevelopMasName[i] + "',  '','"+ DevelopMasField[i]+"/"+ DistrybutMasField[IndexHesh]+"')";
                        InsertFb(StrCreate, StrInsert);
                        // преобразуем строку в байты


                    }

                }

            }

            for (int i = 0; i < CountTableDistrybut; i++)
            {

                if (!StrokaNameTableDevelop.Contains(DistrybutMasName[i]))
                {

                    StrCreate = "select * from REGISTERCOMPARE where REGISTERCOMPARE.NAME_TABLE = " + "'" + DistrybutMasName[i] + "'";
                    StrInsert = "INSERT INTO REGISTERCOMPARE  values ('" + Administrator.AdminPanels.EtalonDeveloperPuth + "', '" +
                    Administrator.AdminPanels.EtalonDistributorPuth + "', '" + DateTime.Now.ToString("yyyyMMddHHmmss") + "', '','" +
                    DistrybutMasName[i] + "',  '','No')";
                    InsertFb(StrCreate, StrInsert);

                }
                else
                {
                    string tmp = StrokaNameTableDevelop.Substring(StrokaNameTableDevelop.IndexOf(DistrybutMasName[i] + ";") + DistrybutMasName[i].Length + 1,
                    StrokaNameTableDevelop.Length - (StrokaNameTableDevelop.IndexOf(DistrybutMasName[i] + ";") + DistrybutMasName[i].Length + 1));
                    int IndexHesh = Convert.ToInt16(tmp.Substring(0, tmp.IndexOf(";")));

                    if (DistrybutMasField[i] != DevelopMasField[IndexHesh])
                    {

                        StrCreate = "select * from REGISTERCOMPARE where REGISTERCOMPARE.NAME_TABLE = " + "'" + DistrybutMasName[i] + "'";
                        StrInsert = "INSERT INTO REGISTERCOMPARE  values ('" + Administrator.AdminPanels.EtalonDeveloperPuth + "', '" +
                        Administrator.AdminPanels.EtalonDistributorPuth + "', '" + DateTime.Now.ToString("yyyyMMddHHmmss") + "', '','" +
                        DistrybutMasName[i] + "',  '','" + DistrybutMasField[i] + "/" + DevelopMasField[IndexHesh] + "')";
                        InsertFb(StrCreate, StrInsert);
 

                    }

                }

            }


            StrCreate = "select * from PROTOKOLCOMPARE where PROTOKOLCOMPARE.PUTHDEVELOP = '"  + Administrator.AdminPanels.EtalonDeveloperPuth +
                "' AND PROTOKOLCOMPARE.PUTHDISTRYBUT = '"  + Administrator.AdminPanels.EtalonDistributorPuth + "'";
            StrInsert = "INSERT INTO PROTOKOLCOMPARE  values ('Compere "+Convert.ToString(IndWrite)+"','" + Administrator.AdminPanels.EtalonDeveloperPuth + "', '" +
            Administrator.AdminPanels.EtalonDistributorPuth + "', '" + DateTime.Now.ToString("yyyyMMddHHmmss") + "')";
            InsertFb(StrCreate, StrInsert);



            string CurrentSetBD = "select * from PROTOKOLCOMPARE";
                List< Administrator.ProtokolCompare> result = new List<Administrator.ProtokolCompare>(1);
                DBConecto.DBopenFBConectionMemory(SystemBD.ConnnStringSystem().ToString(), "", "FbSystem");
                FbCommand UpdateKey = new FbCommand(CurrentSetBD, DBConecto.bdFbSystemConect);
                UpdateKey.CommandTimeout = 3;
                UpdateKey.CommandType = CommandType.Text;
                FbDataReader reader = UpdateKey.ExecuteReader();
                while (reader.Read())
                {
                    result.Add(new Administrator.ProtokolCompare(reader[0].ToString(), reader[1].ToString(), reader[2].ToString(), reader[3].ToString()));

                }
                reader.Close();
                UpdateKey.Dispose();
                DBConecto.DBcloseFBConectionMemory("FbSystem");
                System.Windows.Application.Current.Dispatcher.Invoke(new Action(delegate ()
                {
                    ConectoWorkSpace.Administrator.AdminPanels ConectoWorkSpace_InW = AppStart.LinkMainWindow("WAdminPanels");
                    ConectoWorkSpace_InW.ProtokolCompare.ItemsSource = result;

                }));

            var TextWin = "Сравнение завершено" + Environment.NewLine + "завершена";
            int AutoCl = 1;
            int MesaggeT = -170;
            int MessageL = 600;
            InstallB52.MessageEnd(TextWin, AutoCl, MesaggeT, MessageL);
            return;

            //select(cast(HASH(pr.rdb$procedure_source) as VARCHAR (100))) ha  ,  pr.rdb$procedure_name , (substring(pr.rdb$procedure_source FROM 1 FOR 32000)) cv
            // from rdb$procedures pr

 
            #endregion

        }

        public static void InsertFb(string StrCreate,string StrInsert)
        {
            int Idcount = 0;
            DBConecto.DBopenFBConectionMemory(SystemBD.ConnnStringSystem().ToString(), "", "FbSystem");
            FbCommand SelectTableActiv = new FbCommand(StrCreate, DBConecto.bdFbSystemConect);
            FbDataReader ReadOutTableActiv = SelectTableActiv.ExecuteReader();
            while (ReadOutTableActiv.Read()) { Idcount++; }
            ReadOutTableActiv.Close();
            SelectTableActiv.Dispose();
            if (Idcount == 0)
            {
                DBConecto.UniQuery InsertQuery = new DBConecto.UniQuery(StrInsert, "FB");
                InsertQuery.UserQuery = string.Format(StrInsert, "REGISTERCOMPARE");
                InsertQuery.ExecuteUNIScalar();
                IndWrite++;
            }
            DBConecto.DBcloseFBConectionMemory("FbSystem");
        }

        



        // Процедура сравнения и формирования новой редакции базы данных
        public static void RunCompareCreateBD()
        {

        }
    }


 

   
}