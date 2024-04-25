using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
// ---- BD
/// C:\Windows\Microsoft.Net\assembly\GAC_MSIL\FirebirdSql.Data.FirebirdClient\v4.0_2.7.0.0__3750abcc3150b00c\FirebirdSql.Data.FirebirdClient.dll
using FirebirdSql.Data.FirebirdClient;  // http://www.sql.ru/forum/actualthread.aspx?tid=133383   http://www.firebirdsql.org/en/net-examples-of-use/
using FirebirdSql.Data.Isql;
using System.Data;

// Управление вводом-выводом
using System.IO;
// 
using System.CodeDom;


// Вниимание читать интересно
// http://citforum.univ.kiev.ua/database/interbase/oledb/part1.shtml
// http://kbss.ru/blog/fbdb/


namespace ConectoWorkSpace
{
    /// <summary>
    /// Инструменты для работы с БД (от разных поставщиков)
    /// Пример использования
    /// Создание строки доступа
    /// ConnectionStringBD StringCon = new ConnectionStringBD();
    /// Чтение параметров программы
    /// Data Source=(local)\SQLEXPRESS;Initial Catalog=AutoLot;Integrated Security=SSPI;Pooling=False
    /// StringCon.DataSource_o = @"PROGRAM-PC\SQLCONECTOEXP";
    /// StringCon.InitialCatalog_o = "ATOtest";
    /// if (DBConecto.DBopen(StringCon.ConnectionString(1)))
    /// {
    ///     return true;
    /// }
    /// 
    /// 
    /// </summary>
    public partial class DBConecto
    {
        #region Описание модели ADO.NET
        /// <summary>
        ///                             Модель ADO.NET
        ///  -----------------------------------------------------                           
        ///                                   Приложение .NET
        ///                  /                       |                                  \                                      \
        ///   Поставщик SQL Server        Поставщик SQL Oracle                       Поставщик OLE DB*
        ///              для .NET         ODP.NET (Oracle Data Provider для .NET)       для .NET
        ///                                
        ///                  |                       |                                     |
        ///      База Данных  SQL Server     База Данных Oracle                       Источник данных
        /// 
        /// 
        /// * Технология OLE DB существует уже много лет как часть ADO, поэтому для большинства источников данных предусмотрены драйверы OLE DB (включая SQL Server, Oracle, Access, MySQL и многие другие).
        /// -----------------------------------------------------------------    
        ///   Альтернатива модели ADO.NET - ODBC драйвер.
        ///   
        ///  Важно установить проверку наличия dll библиотеки для работы с БД
        ///  
        ///  ADO.NET расширяет концепцию новым типом DataSet, который представляет локальную копию множества взаимосвязанных таблиц
        ///  Все типы ADO.NET предназначены для выполнения одного набора задач: установить соединение с базой данных,
        ///  создать и заполнить данными объект DataSet, отключиться от хранилища данных, вернуть изменения внесенные в DataSet обратно в базу.
        ///  После создания DataSet и его заполнения данными мы можем программными средствами производить запросы к нему и передвигаться по таблицам. 
        ///  если ADO.NET в целом предполагает работу с данными в отсоединенном режиме, объект DataSet позволяет "скрыть* этот факт и обращаться к информации так, 
        ///  как будто бы она находится в базе данных. 
        /// 
        /// </summary>
        #endregion


        #region Статические потоки FB в памяти
        // --------------------------
        /// <summary>
        /// Соединение С БД FbConnection (Служебная, локальная БД)
        /// </summary>
        public static FbConnection bdFbSystemConect = null;


        // --------------------------
        /// <summary>
        /// Соединение С БД FbConnection по умолчанию (Основаня БД)
        /// </summary>
        public static FbConnection bdFbDefConect = null;
        /// <summary>
        /// Соединение С БД FbConnection c БД 01 (другая БД: шлюз, резервная и пр.)
        /// </summary>
        public static FbConnection bdFb01Conect = null;
        /// <summary>
        /// Соединение С БД FbConnection c БД 02 (другая БД: импорт данных из другого приложения)
        /// </summary>
        public static FbConnection bdFb02Conect = null;
        // --------------------------
        #endregion

        #region Соединение с БД для разных Поставщиков (это для FB Firebird 2.5)

        /// <summary>
        /// Соединение с БД - открытый поток остается в памяти как объект FbConnection.<para></para>
        ///  
        /// <param name="connectionString">Строка соединения class </param><para></para>
        /// <param name="SqlCommand_">Резерв для SQL команд</param><para></para>
        /// <param name="TypeConnection">Тип потока: FbSystem - системная таблица; FbDef - FbConection</param><para></para>
        /// <returns>Код ошибки системы Conecto WorkSpace: 0 - без ошибок</returns>
        /// </summary>
        public static int DBopenFBConectionMemory(string connectionString, string SqlCommand_ = "", string TypeConnection = "FbDef")
        {

            try
            {
                switch (TypeConnection)
                {
                    // ---------------------- 
                    case "FbSystem":
                        if (bdFbSystemConect == null || bdFbSystemConect.State == ConnectionState.Closed)
                        {
                            bdFbSystemConect = new FbConnection(connectionString);
                            bdFbSystemConect.Open();
                        }

                        return 0;
                    case "FbDef":
                        if (bdFbDefConect == null || bdFbDefConect.State == ConnectionState.Closed)
                        {
                            // Опыт не мог обратится ...
                            using (bdFbDefConect = new FbConnection(connectionString))
                            {
                                bdFbDefConect.Open();
                            }
                        }
                        return 0;
                    //break;
                    case "Fb01":
                        bdFb01Conect = new FbConnection(connectionString);
                        bdFb01Conect.Open();
                        return 0;
                    //break;
                    case "Fb02":
                        bdFb02Conect = new FbConnection(connectionString);

                        bdFb02Conect.Open();
                        return 0;
                    //break;

                }
                return -1;
            }
            catch (Exception ex) //)
            {
                // Протоколировать исключение
                SystemConecto.ErorDebag("При подключении к БД (" + TypeConnection + ")," + Environment.NewLine + 
                        " строка подключения: " + connectionString + Environment.NewLine +
                        " возникло исключение: " + Environment.NewLine +
                        " === Message: " + ex.Message.ToString() + Environment.NewLine +
                        " === Exception: " + ex.ToString(), 1);
                
                        int ErrorBD = ExceptionOnConecto(ex.Message.ToString());

                        return ErrorBD;
            }

        }

        #endregion

        #region Закрыть потоки в памяти БД  Firebird
        /// <summary>
        /// Закрыть соединения с БД  Firebird<para></para>
        /// <param name="TypeConnection">Тип соединения: FbDef - FbConection, по умолчанию</param>
        /// </summary>
        public static bool DBcloseFBConectionMemory(string TypeConnection = "FbDef")
        {
            try
            {
                switch (TypeConnection)
                {
                    case "FbSystem":
                        if (bdFbSystemConect != null)
                        {
                            bdFbSystemConect.Close();
                        }
                        return true;
                    case "FbDef":
                        if (bdFbDefConect != null)
                        {
                            bdFbDefConect.Close();
                        }
                        return true;
                    //break;
                    case "Fb01":
                        bdFb01Conect.Close();
                        return true;
                    //break;
                    case "Fb02":
                        bdFb02Conect.Close();
                        return true;
                    // break;

                }


                return false;
            }
            catch (Exception ex)
            {
                ErrorBDEnd.Message = ex.Message;

                // Протоколировать исключение
                SystemConecto.ErorDebag("Ошибка соединения: " + ex.ToString(), 1);
                return false;
            }

        }
        #endregion

        #region Заметки о БД Firebird
        /// ------------------------------------------- Оптимизация БД на машинах и в сети
        /// 
        /// --------------------------------------------------  Firebird 
        /// ------ медленный коннект (и работа) на WinMe/WinXP
        /// Причина в том, что Me и XP содержат так называемую систему восстановления файлов. 
        /// В соответствии со списком расширений любой файл, который изменяется, копируется системой в специальное место 
        /// для возможного восстановления в дальнейшем. В этом списке есть расширение gdb, что приводит для IB к долгому коннекту 
        /// и очень медленной работе.
        /// Данную особенность можно выключить целиком:
        /// На XP: System Properties | System Restore | Turn off System Restore on all drives.
        /// либо убрать из списка расширение gdb, отредактировав файл на XP: $WINNT$\system32\Restore\filelist.xml
        /// 
        /// ------ настройка многопроцесорной архитектуры
        /// SuperServer не может использовать несколько процессоров в силу своей архитектуры. Конечно, загружены будут оба процессора,
        /// но из-за нераспараллеливания операций ввода-вывода каждый процессор будет загружен не более чем на 50% 
        /// (при двух. При четырех - на 25%). Для решения этой проблемы в Firebird и позже в Interbase 6.5 был добавлен параметр 
        /// IBCONFIG - CPU_AFFINITY. Это битовая маска, в которой указываются номера процессоров, которые необходимо задействовать. 
        /// Например, 1 означает работу на первом процессоре, 2 - на втором, 3 - на первых двух и так далее. 
        /// Освободившийся процессор можно занять другими задачами.
        /// --------------------------------------------------  Firebird
        #endregion

        #region Обработка исключений FB разработка

        public static int ExceptionOnConecto(string Message)
        {
            //Dictionary<int, string> Exception = new Dictionary<int,string>();

            int ErrorBD = -1; // не определена системой

            string[] Message_ = Message.Split('"');
            switch (Message_[0])
            {
                case "Your user name and password are not defined. Ask your database administrator to set up a Firebird login.":
                    
                    
                    ErrorBDEnd.ErrorCode = ErrorBD = 8125;
                    ErrorBDEnd.Message = "Неверный пароль или логин";
                    break;
                case "Unable to complete network request to host ": //"192.168.7.231":
                    ErrorBDEnd.ErrorCode = ErrorBD = 8126;
                    ErrorBDEnd.Message = "Нет соединения с сервером";
                    break;
                case "An invalid connection string argument has been supplied or a required connection string argument has not been supplied.":
                    ErrorBDEnd.ErrorCode = ErrorBD = 8127;
                    ErrorBDEnd.Message = "Строка соединения с сервером неверно сформированна";
                    break;
                default:
                    // Ошибка не определена
                    SystemConecto.ErorDebag("Код сообщения не определен: " + Message);
                    ErrorBDEnd.ErrorCode = -1;
                    ErrorBDEnd.Message = "";
                    break;

            }


            

            return  ErrorBD;
        }

        #endregion

        #region Выполнить процедуру с параметрами, адаптивный метод

        /// <summary>
        /// new Param() ключь - имя параметра  БД  Firebird<para></para>
        /// </summary>
        public Dictionary<string, string> Param = new Dictionary<string, string>();
        /// <summary>
        /// Выполнить процедуру с параметрами, адаптивный метод. БД  Firebird<para></para>
        /// <param name="NameProc">Тип выполнения: NameProc название процедуры в БД  Firebird</param>
        /// <param name="connectionString">Тип выполнения: connectionString строка соединения</param>
        /// <param name="TypeExec">Тип выполнения: TypeExec - 0, по умолчанию вернуть масив данных, 1 - вернуть одно значение</param>
        /// </summary>
        public dynamic DBCommandProcedureFB(string NameProc, Dictionary<string, string> Param_, string connectionString, int TypeExec = 0)
        {

            FbCommand cmd = new FbCommand(NameProc);
            cmd.CommandType = CommandType.StoredProcedure;   // System.Data - CommandType
            foreach (KeyValuePair<string, string> kvp in Param_)
            {
                cmd.Parameters.Add(kvp.Key, kvp.Value);     //cmd.Parameters.Add("@BARCODE", barcode); (название параметра, переменная значения)
            }

            using (cmd.Connection = new FbConnection(connectionString))
            {
                cmd.Connection.Open();


                // Вывести результат

                switch (TypeExec)
                {
                    case 1:
                        var result = cmd.ExecuteScalar();
                        return Convert.ToDouble(result);
                    //break;
                    default:

                        break;
                }

            }
            return -1;

        }

        #endregion

        #region Готовая строка соединения с БД []

        /// <summary>
        /// Параметры для StringServerFB<para></para>
        /// 0 - резерв
        /// 1 - логин
        /// 2 - пароль
        /// 3 - БД IP
        /// 4 - БД алиас
        /// 5 - БД путь (доп параметр)
        /// 6 - Кодовая таблица по умолчанию 1251 или UTF 8
        /// </summary>
        public static string[] ParamStringServerFB = new string[7] { "", "", "", "", "", "", "" };
        
        /// <summary>
        /// DBConecto.StringServerFB() подключение к БД c помощью масива параметров ParamStringServerFB
        /// </summary>
        /// <returns></returns>
        public static string StringServerFB()
        {
            /// Создание строки доступа
            FbConnectionStringBuilder StringCon = new FbConnectionStringBuilder();
            // ConnectionStringBD StringCon = new ConnectionStringBD();
            
            /// Чтение параметров программы
            // User = SYSDBA; Password = masterkey; Database = SampleDatabase.fdb; DataSource = localhost;
            // Port=3050;Dialect=3; Charset=NONE;Role=;Connection lifetime=15;Pooling=true; MinPoolSize=0;
            // MaxPoolSize=50;Packet Size=8192;ServerType=0;


            StringCon.UserID = ParamStringServerFB[1] == "" ? "SYSDBA" : ParamStringServerFB[1];
            StringCon.Password = ParamStringServerFB[2] == "" ? "masterkey" : ParamStringServerFB[2]; 

            // StringCon.Charset = ParamStringServerFB[6]=="" ? FbCharset.Windows1251.ToString() : FbCharset.Utf8.ToString();
            StringCon.Dialect = 3; // Диалект 3 всегда

            StringCon.ServerType = FbServerType.Default;
            StringCon.DataSource = ParamStringServerFB[3] == "" ? "localhost" : ParamStringServerFB[3];
            StringCon.Database = ParamStringServerFB[4];
               // StringCon.
            // StringCon.ServerType_o = "Embedded";
            // StringCon.TypeDllPutch = SystemConecto.PutchApp + @"data\fbembed.dll";
            // StringCon.InitialCatalog = SystemConecto.PutchApp + @"data\system.fdb"; // C:\\EMPLOYEE.FDB


            return StringCon.ToString();
        }

        /// <summary>
        /// DBConecto.ConnnStringServer() подключение к основной БД Терминала или Офиса 
        /// </summary>
        /// <returns></returns>
        public static string ConnnStringServer()
        {
            /// Создание строки доступа
            ConnectionStringBD StringCon = new ConnectionStringBD();
            /// Чтение параметров программы
            // User = SYSDBA; Password = masterkey; Database = SampleDatabase.fdb; DataSource = localhost;
            // Port=3050;Dialect=3; Charset=NONE;Role=;Connection lifetime=15;Pooling=true; MinPoolSize=0;
            // MaxPoolSize=50;Packet Size=8192;ServerType=0;
            ///StringCon.DataSource_o = @"PROGRAM-PC\SQLCONECTOEXP";
            /// StringCon.InitialCatalog_o = "ATOtest";
            StringCon.ServerType_o = "Embedded";
            StringCon.UserID_o = "SYSDBA";
            StringCon.Password_o = "masterkey";
            StringCon.TypeDllPutch = SystemConecto.PutchApp + @"data\fbembed.dll";
            StringCon.InitialCatalog_o = SystemConecto.PutchApp + @"data\system.fdb"; // C:\\EMPLOYEE.FDB



            return StringCon.ConnectionString(1);
        }
        #endregion

    }

    #region ConnectionStringBD - Создание правила, строки соединения с БД от разных поставщиков для ADO.NET (для Firebird)
    /// <summary>
    /// Создание правила, строки соединения с БД от разных поставщиков для ADO.NET 
    /// </summary>
    partial class ConnectionStringBD
    {
        /// <summary>
        /// 
        /// <param name="TypeConnecto">TypeConnecto: Тип соединения: 0-соединение по умолчанию;</param><para></para>
        /// <returns>returns: Сформированная строка соединения с БД</returns><para></para>
        /// </summary>
        public string ConnectionStringFB(int TypeConnection = 0)
        {
            string ConnectionString = "";
            // Тип  соединения может зависить не только от поставщика БД но и от параметров соединения
            //switch (TypeConnection)
            //{
            //    case 0:
            //        break;
            //}  



            FbConnectionStringBuilder csb = new FbConnectionStringBuilder();

            // Указываем тип используемого сервера FbServerType.Embedded - локальный
            csb.ServerType = this.ServerType_o == "Embedded" ? FbServerType.Embedded : FbServerType.Default;

            // Путь до файла с базой данных путь или псевдоним = "C:\\EMPLOYEE.FDB"
            csb.Database = this.InitialCatalog_o;

            // Настройка параметров "общения" клиента с сервером
            csb.Charset = this.Charset_o; // По умолчанию None
            csb.Dialect = 3; // Диалект 3 всегда


            // Путь до бибилиотеки- локального сервера Firebird (клиента)
            // Если библиотека находится в тойже папке
            // что и exe фаил - указывать путь не надо
            // csb.ClientLibrary = @"dlls\fbembed.dll";
            if (this.TypeDllPutch != null)
            {
                csb.ClientLibrary = this.TypeDllPutch;
            }


            // Настройки аутентификации "SYSDBA"
            csb.UserID = this.UserID_o;
            csb.Password = this.Password_o;


            ConnectionString = csb.ToString(); // csb.ConnectionString; // csb.ToString();


            return ConnectionString;
        }



        // описание свойства ConnectionString объекта подключения, дает поставщик данных БД.
        // Способы соединения с БД:
        // --------------------------------------------------  Firebird              (соединения указанны не все)

        // --------- Firebird ADO.NET Data Provider
        //Type .NET Framework Class Library
        //Usage
        //Manufacturer Firebird
        // User=SYSDBA;Password=masterkey;Database=SampleDatabase.fdb;DataSource=localhost; 
        // Port=3050;Dialect=3; Charset=NONE;Role=;Connection lifetime=15;Pooling=true; MinPoolSize=0;
        // MaxPoolSize=50;Packet Size=8192;ServerType=0;

    }
    #endregion


    #region Утилиты для работы с служебной (локальной) БД
    /// <summary>
    /// Создание правила, строки соединения с БД от разных поставщиков для ADO.NET <para></para>
    /// SystemConecto.ErorDebag(StringCon.ConnectionString(1).ToString(), 2); // Отладка
    /// </summary>
    partial class SystemBD
    {
        /// <summary>
        /// SystemBD.ConnnStringSystem() подключение к системной БД ConectoWorkSpace<para></para>
        /// кеш, локальные пользовватели
        /// </summary>
        /// <returns></returns>
        public static string ConnnStringSystem()
        {
            /// Создание строки доступа
            ConnectionStringBD StringCon = new ConnectionStringBD();
            /// Чтение параметров программы
            // User = SYSDBA; Password = masterkey; Database = SampleDatabase.fdb; DataSource = localhost;
            // Port=3050;Dialect=3; Charset=NONE;Role=;Connection lifetime=15;Pooling=true; MinPoolSize=0;
            // MaxPoolSize=50;Packet Size=8192;ServerType=0;
            ///StringCon.DataSource_o = @"PROGRAM-PC\SQLCONECTOEXP";
            /// StringCon.InitialCatalog_o = "ATOtest";
            StringCon.ServerType_o = "Embedded";
            StringCon.UserID_o = "SYSDBA";
            StringCon.Password_o = "masterkey";
            StringCon.TypeDllPutch = SystemConecto.PutchApp + @"data\fbembed.dll";
            StringCon.InitialCatalog_o = SystemConecto.PutchApp + @"data\system.fdb"; // C:\\EMPLOYEE.FDB



            return StringCon.ConnectionString(1);
        }

           /// <summary>
        /// SystemBD.CreateBDSystem() создание системной  БД ConectoWorkSpace
        /// http://www.ibase.ru/devinfo/sysqry.htm
        /// </summary>
        /// <returns></returns>
        public static bool ListTableBD(string TypeConnection = "FbDef")
        { 
            
            // Спмсок таблиц
            //string StrCreate = "select R.RDB$RELATION_NAME, R.RDB$FIELD_POSITION, R.RDB$FIELD_NAME, " +
            //    "F.RDB$FIELD_LENGTH, F.RDB$FIELD_TYPE, F.RDB$FIELD_SCALE, F.RDB$FIELD_SUB_TYPE" +
            //    "from RDB$FIELDS F, RDB$RELATION_FIELDS R " +
            //    "where F.RDB$FIELD_NAME = R.RDB$FIELD_SOURCE and R.RDB$SYSTEM_FLAG = 0 " +
            //    "order by R.RDB$RELATION_NAME, R.RDB$FIELD_POSITION";

                //if (!DBConecto.DBopenFB(SystemBD.ConnnStringSystem(), "", "FbSystem"))
                //{
                //    SystemConecto.ErorDebag("Ошибка", 2);
                //}
                     //SystemConecto.ErorDebag(DBConecto.bdFbSystemConect.State.ToString(), 2);  
                // Соединил
                //if(DBConecto.bdFbSystemConect.State == ConnectionState.Open){
                //    SystemConecto.ErorDebag("Есть связь", 2);
                //}

            string StrCreate = "CREATE TABLE UserSystems  (ID  NUMERIC(12)  NOT  NULL, " +
                    "NAMESYS  VARCHAR(100) CHARACTER SET UTF8   NOT NULL, " +
                    "NAMEBD  VARCHAR(15) CHARACTER SET UTF8  NOT NULL, " +
                    "FIRSTNME    VARCHAR(100) CHARACTER SET UTF8   NOT NULL, " +
                    "LASTNAME  VARCHAR(100) CHARACTER SET UTF8  NOT NULL,  " +
                    "FATHERNAME    VARCHAR(100)  CHARACTER SET UTF8   NOT NULL, " +
                    "RESERV1    VARCHAR(100) CHARACTER SET UTF8    NOT NULL, " +
                    "RESERV2    VARCHAR(100)  CHARACTER SET UTF8   NOT NULL, " +
                    "RESERV3    NUMERIC (12)     NOT NULL, " +
                    "RESERV4    NUMERIC (12)     NOT NULL, " +
                    "PRIMARY KEY (ID));";



            try
            {
                
                FbCommand SelectTable = new FbCommand(StrCreate, DBConecto.bdFbSystemConect);
                SelectTable.CommandType = CommandType.Text;
                SelectTable.Connection.Open();
                SelectTable.ExecuteNonQuery(); // ExecuteScalar(); // ExecuteScalar();
                FbDataAdapter Adapter = new FbDataAdapter(SelectTable);


                DataSet ListTable = new DataSet("TableBD");

                if (Adapter != null)
                {
                    // DBAdapter.Fill(DSZapit);
                   // Adapter.Fill(ListTable, "TableBD");




                }
                    
                    //
                    //string fileName = SystemConecto.PutchApp + @"tmp\table.cs";
                    //ListTable.WriteXmlSchema(fileName);
                    // Generate the TypedDataSet
                    //string fileName = SystemConecto.PutchApp + @"temp\table.cs";

                    //StreamWriter tw = new StreamWriter(new FileStream(fileName,
                    //FileMode.Create,
                    //FileAccess.Write));

                    //CodeNamespace cn = new CodeNamespace("TableBD");
                    //CSharpCodeProvider cs = new CSharpCodeProvider();

                
                    //Design.TypedDataSetGenerator.Generate(ListTable, codeNamespace, csharpCodeProvider);

                    //cg.GenerateCodeFromNamespace(cn, tw, null);

                    //tw.Flush();
                    //tw.Close();

                

                

            }
            catch(Exception ex)
            {
                SystemConecto.ErorDebag(ex.ToString(), 2);
            }
            // DBConecto.DBcloseFB("FbSystem");
            return true;  

        }

        /// <summary>
        /// SystemBD.CreateBDSystem() создание системной  БД ConectoWorkSpace
        /// </summary>
        /// <returns></returns>
        public static bool CreateBDSystem()
        {
            try
            {

                /// Создание строки доступа
                ConnectionStringBD StringCon = new ConnectionStringBD();
                /// Чтение параметров программы
                // User = SYSDBA; Password = masterkey; Database = SampleDatabase.fdb; DataSource = localhost;
                // Port=3050;Dialect=3; Charset=NONE;Role=;Connection lifetime=15;Pooling=true; MinPoolSize=0;
                // MaxPoolSize=50;Packet Size=8192;ServerType=0;
                ///StringCon.DataSource_o = @"PROGRAM-PC\SQLCONECTOEXP";
                /// StringCon.InitialCatalog_o = "ATOtest";
                StringCon.ServerType_o = "Embedded";
                StringCon.UserID_o = "SYSDBA";
                StringCon.Password_o = "masterkey";
                StringCon.TypeDllPutch = Path.Combine(SystemConecto.PutchApp + @"data\", "fbembed.dll");
                StringCon.InitialCatalog_o = Path.Combine(SystemConecto.PutchApp + @"data\", "system.fdb"); // SystemConecto.PutchApp + @"data\system.fdb"; // C:\\EMPLOYEE.FDB



                FbConnection.CreateDatabase(StringCon.ConnectionString(1).ToString(), 16384, true, false);
                // Создание системной БД
                // Системные таблицы сервера пользователей.

                DBConecto.DBopenFBConectionMemory(SystemBD.ConnnStringSystem(), "", "FbSystem");
                //DBConecto.bdFbSystemConect;
                //string StrCreate = "SELECT * FROM USER WHERE NOMER_GROUP=" + GroupId.ToString();
                string StrCreate = "CREATE TABLE UserSystems  (ID  INTEGER(12)  NOT  NULL, " +
                    "NAMESYS  VARCHAR(100)  NOT NULL, " +
                    "NAMEBD  VARCHAR(15)  NOT NULL, " +
                    "FIRSTNME    VARCHAR(100)   NOT NULL, " +
                    "LASTNAME  VARCHAR(100)  NOT NULL,  " +
                    "FATHERNAME    VARCHAR(100)     NOT NULL, " +
                    "RESERV1    VARCHAR(100)     NOT NULL, " +
                    "RESERV2    VARCHAR(100)     NOT NULL, " +
                    "RESERV3    INTEGER(12)     NOT NULL, " +
                    "RESERV4    INTEGER(12)     NOT NULL " +
                    "PRIMARY KEY (ID))";

                FbCommand SelectUser = new FbCommand(StrCreate, DBConecto.bdFbSystemConect);

                FbCommand cmd = new FbCommand();
                // Запрос текста
                cmd.CommandType = CommandType.Text;
                using (cmd.Connection = DBConecto.bdFbSystemConect)
                {
                    // Соединение уже открыто пример чего не надо делать
                    //cmd.Connection.Open();

                    // Вернуть один результат
                    //var result = cmd.ExecuteScalar();
                    //var thecost = Convert.ToDouble(result);


                }
                //Запрос процедуры
                // cmd.CommandType = CommandType.StoredProcedure;

                DBConecto.DBcloseFBConectionMemory("FbSystem");


            }
            catch (FirebirdSql.Data.FirebirdClient.FbException exFB)
            {

                // -206 - не ввел параметр
                SystemConecto.ErorDebag("При обращении к БД ..., возникло исключение: " + Environment.NewLine +
                       " === IDCode: " + exFB.ErrorCode.ToString() + Environment.NewLine +
                       " === Message: " + exFB.Message.ToString() + Environment.NewLine +
                       " === Exception: " + exFB.ToString());
                //335544721 - обрыв соединения;
                //if (exFB.ErrorCode == 335544721)
                //{ }
                return false;
            }
            catch (Exception ex)
            {
                // Протоколировать исключение
                SystemConecto.ErorDebag("При обращении к БД ..., возникло исключение:" + Environment.NewLine +
                " === Message: " + ex.Message.ToString() + Environment.NewLine +
                " === Exception: " + ex.ToString());
                return false;
            }


            return true;
        }
    }








    #endregion

    #region Функции сервера

    //  ==== Список пользователей
    //    FirebirdSql.Data.Services.FbSecurity v = new FirebirdSql.Data.Services.FbSecurity();
    //v.ConnectionString = "data source=127.0.0.1;initial catalog=\"C:\\Program Files\\Firebird\\Firebird_2_5\\examples\\empbuild\\EMPLOYEE.FDB\";user id=sysdba;password=masterkey";
    //foreach(FirebirdSql.Data.Services.FbUserData item in v.DisplayUsers()){
    //   Debug.WriteLine("{0} - {1} {2} {3} {4}", 
    //                item.UserName,
    //                Encoding.UTF8.GetString(Encoding.Convert(Encoding.UTF8, Encoding.Default, Encoding.UTF8.GetBytes(item.FirstName))),
    //                Encoding.UTF8.GetString(Encoding.Convert(Encoding.UTF8, Encoding.Default, Encoding.UTF8.GetBytes(item.MiddleName))),
    //                Encoding.UTF8.GetString(Encoding.Convert(Encoding.UTF8, Encoding.Default, Encoding.UTF8.GetBytes(item.LastName))),
    //                item.UserPassword
    //   );
    //}

    //   select RDB$USER from RDB$USER_PRIVILEGES
    //where RDB$USER<>'PUBLIC' group by RDB$USER

    // ==== Настройка портов
    //μTorrent позволяет общаться с маршрутизатор для перенаправления портов без вашего вмешательства.
    //Некоторые устройства не поддерживают Universal Plug и Play (UPnP), так что вы можете все еще должны направить свои порты вручную. 
    //Отключите функцию UPnP, если это так.


    // Список БД
    // есть параметр DatabaseAccess которым можно запретить подключатся не к альясам

    #endregion

}
