using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
// ---- BD
using System.Data;              // Содержит типы, независимые от провайдеров, например DataSet и DataTable.
using System.Data.SqlClient;    // Содержит типы SQL Server .NET Data Provider
// using System.Data.OleDb;     // System.Data.OleDb. Содержит типы OLE DB .NET Data Provider.
// System.Data.Оdbc. Содержит типы ODBC .NET Data Provider.
// Локальная БД - ошибка
// using System.Data.SQLite;
// using System.Windows.Forms;

  

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
    /// if (DBConecto.DBopen(StringCon.ConnectionString()))
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
        ///  Подключение БД с помощью скрипта
        ///  =================== http://msdn.microsoft.com/ru-ru/library/ms165673(v=sql.105).aspx
        ///  
        ///  выполнить из командной строки sqlcmd -S Server\Instance  или  Менеджер студия MS
        /// 
        ///  Путь BD AnalizObigu\Server или 1.Товарообіг\Server  Uniсode
        /// 
        /// USE [master]
        //GO
        //CREATE DATABASE [analiz_cdt] ON 
        //( FILENAME = N'D:\1.Товарообіг\Server\analiz_cdt.mdf' )
        // FOR ATTACH ;
        //GO
        ///  ----------------------------- Файл ldf создается сам автоматически, после обнаружения неправильного пути в переносимой БД!
        ///  
        ///  --------------------- Исключили
        ///  , ( FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL10_50.MSSQLSERVER\MSSQL\Data\<database name>.ldf' )
        /// 
        /// ====================================== Отсоединить БД (не рекомендуется использовать, но ...)
        /// USE [master]
        //GO
        //EXEC master.dbo.sp_detach_db @dbname = N'Centr_analiz'
        //GO

        /// 
        /// ============== Порт соединения MS Sql 2008  - 1433
        /// 
        /// 
        /// 
        /// 
        /// 
        /// </summary>
        struct HelpInfoDB
        {
        }
        #endregion

        #region Глобальные переменные

        /// <summary>
        /// Последняя ошибка соединения с БД
        /// </summary>
        public static ErrorBD ErrorBDEnd = new ErrorBD();   // Стр 64.

        /// <summary>
        /// Возврат сообщений событий с БД
        /// </summary>
        public static WindowEventBD WinView = new WindowEventBD();


        #endregion


        #region Ссылки соединения с БД от разных поставщиков

        /// <summary>
        /// Соединение С БД MSSQL 2008 or 2005 по умолчанию (Основаня БД)
        /// </summary>
        public static SqlConnection bdSqlDefConect = null;


        /// <summary>
        /// Соединение С БД MSSQL 2008 or 2005 c БД 01 (другая БД: шлюз, резервная и пр.)
        /// </summary>
        public static SqlConnection bdSql01Conect = null;
        /// <summary>
        /// Соединение С БД MSSQL 2008 or 2005 c БД 02 (другая БД: импорт данных из другого приложения)
        /// </summary>
        public static SqlConnection bdSql02Conect = null;
        // ==========================================================
        /// <summary>
        /// Адаптер работы с БД MSSQL 2008 or 2005 по умолчанию (Основаня БД)
        /// </summary>
        public static SqlDataAdapter bdSqlDefAdapter = null;
        /// <summary>
        /// Адаптер работы с БД MSSQL 2008 or 2005 c БД 01 (другая БД: шлюз, резервная и пр.)
        /// </summary>
        public static SqlDataAdapter bdSql01Adapter = null;
        /// <summary>
        /// Адаптер работы с БД MSSQL 2008 or 2005 c БД 02 (другая БД: импорт данных из другого приложения)
        /// </summary>
        public static SqlDataAdapter bdSql02Adapter = null;


        /// <summary>
        /// Соединение С БД или свободными таблицами dBF по умолчанию (Основаня БД)
        /// </summary>
        public static dynamic bdDBFDefConect = null;
        /// <summary>
        /// Соединение С БД DBF-Connection c БД 01 (другая БД: шлюз, резервная и пр.)
        /// </summary>
        public static dynamic bdDBF01Conect = null;
        /// <summary>
        /// Соединение С БД DBF-Connection c БД 02 (другая БД: импорт данных из другого приложения)
        /// </summary>
        public static dynamic bdDBF02Conect = null;


        #endregion

        /// <summary>
        /// Подготовка работы класса
        /// </summary>
        public static void Load()
        {
            // Проверка библиотек на наличие в системе



        }

        #region Соединение с БД для разных Поставщиков
        /// <summary>
        /// Соединение с БД.
        /// 1.Соединится.
        /// </summary>
        /// <param name="connectionString">Строка соединения class </param>
        /// <param name="SqlCommand_">Резерв для SQL команд</param>
        /// <param name="TypeConnection">Тип соединения: SqlDef - по умолчанию; FbDef - FbConection</param>
        public static bool DBopen(string connectionString, string SqlCommand_ = "", string TypeConnection = "SqlDef")
        {
            //MessageBox.Show(TypeConnection.IndexOf("1").ToString());
            if (TypeConnection.IndexOf("Sql") > -1)
            {

                try
                {
                    switch (TypeConnection)
                    {
                        case "SqlDef":
                            if (bdSqlDefConect == null || bdSqlDefConect.State == ConnectionState.Closed)
                            {
                                bdSqlDefConect = new SqlConnection(connectionString);
                                //Делегат EventHandler связывает метод-обработчик conn_Disposed  
                                //с событием Disposed объекта conn 
                                bdSqlDefConect.Disposed += new EventHandler(conn_Disposed);
                                //Делегат StateChangeEventHandler связывает метод-обработчик conn_StateChange  
                                //с событием StateChange объекта conn 
                                bdSqlDefConect.StateChange += new StateChangeEventHandler(conn_StateChange);
                                //Открыть подключение
                                bdSqlDefConect.Open();
                            }
                            return true;
                        //break;
                        case "Sql01":
                            bdSql01Conect = new SqlConnection(connectionString);
                            bdSql01Conect.Disposed += new EventHandler(conn_Disposed);
                            bdSql01Conect.StateChange += new StateChangeEventHandler(conn_StateChange);
                            bdSql01Conect.Open();
                            return true;
                        //break;
                        case "Sql02":
                            bdSql02Conect = new SqlConnection(connectionString);
                            bdSql02Conect.Disposed += new EventHandler(conn_Disposed);
                            bdSql02Conect.StateChange += new StateChangeEventHandler(conn_StateChange);
                            bdSql02Conect.Open();
                            return true;
                        //break;
                    }
                    return false;
                }
                catch (SqlException ex)
                {
                    ErrorBDEnd.Message = ex.Message;
                    ErrorBDEnd.ErrorCode = ex.ErrorCode;
                    ErrorBDEnd.ErrorNumber = ex.Number;

                    // Протоколировать исключение
                    if (SystemConecto.DebagApp)
                    {
                        SystemConecto.ErorDebag("Ошибка соединения: " + ex.ToString(), 1);
                    }
                    return false;
                }
            }
            if (TypeConnection.IndexOf("Fb") > -1)
            {
                SystemConecto.ErorDebag("Ошибка соединения: Ветвь в разработке", 1);
                return false;
            }
            return false;

        }

        #region Адаптер соединения
        /// <summary>
        /// Адаптер с БД потоки
        /// </summary>
        /// <param name="connectionString">Строка соединения class </param>
        /// <param name="SqlCommand_">Резерв для SQL команд</param>
        /// <param name="TypeConnection">Тип соединения: SqlDef - по умолчанию; FbDef - FbConection</param>
        public static SqlDataAdapter DBAdapter(string SqlCommand_ = "", string TypeConnection = "SqlDef")
        {

            SqlCommand cmd = new SqlCommand(SqlCommand_);
            cmd.CommandTimeout = 500; // sec
            cmd.CommandType = CommandType.Text;

            //MessageBox.Show(TypeConnection.IndexOf("1").ToString());
            if (TypeConnection.IndexOf("Sql") > -1)
            {

                try
                {
                    switch (TypeConnection)
                    {
                        case "SqlDef":
                            cmd.Connection = bdSqlDefConect;
                            bdSqlDefAdapter = new SqlDataAdapter(cmd);
                            //bdSqlDefAdapter = new SqlDataAdapter(SqlCommand_, bdSqlDefConect);
                            return bdSqlDefAdapter;
                        //break;
                        case "Sql01":
                            bdSqlDefAdapter = new SqlDataAdapter(cmd);
                            //bdSql01Adapter = new SqlDataAdapter(SqlCommand_, bdSql01Conect);
                            return bdSqlDefAdapter;
                        //break;
                        case "Sql02":
                            bdSqlDefAdapter = new SqlDataAdapter(cmd);
                            //bdSql02Adapter = new SqlDataAdapter(SqlCommand_, bdSql02Conect);
                            return bdSqlDefAdapter;
                        //break;
                    }
                    return null;
                }
                catch (SqlException ex)
                {
                    ErrorBDEnd.Message = ex.Message;
                    ErrorBDEnd.ErrorCode = ex.ErrorCode;
                    ErrorBDEnd.ErrorNumber = ex.Number;

                    // Протоколировать исключение
                    if (SystemConecto.DebagApp)
                    {
                        SystemConecto.ErorDebag("Ошибка соединения адаптера: " + ex.ToString(), 1);
                    }
                    return null;
                }
            }
            if (TypeConnection.IndexOf("Fb") > -1)
            {
                try
                {
                    switch (TypeConnection)
                    {
                        // ---------------------- 
                        case "FbDef":

                            return null;
                        //break;
                        case "Fb01":

                            return null;
                        //break;
                        case "Fb02":

                            return null;
                        //break;

                    }
                    return null;
                }
                catch (Exception ex)
                {
                    ErrorBDEnd.Message = ex.Message;

                    // Протоколировать исключение
                    if (SystemConecto.DebagApp)
                    {
                        SystemConecto.ErorDebag("Ошибка соединения адаптера: " + ex.ToString(), 1);
                    }
                    return null;
                }
            }
            return null;

        }

        #endregion




        #region Закрыть БД
        /// <summary>
        /// Закрыть соединения с БД
        /// </summary>
        /// <param name="TypeConnection">Тип соединения: SqlDef - по умолчанию; FbDef - FbConection</param>
        public static bool DBclose(string TypeConnection = "SqlDef")  // 
        {
            if (TypeConnection.IndexOf("Sql") > -1)
            {

                try
                {
                    switch (TypeConnection)
                    {
                        case "SqlDef":
                            bdSqlDefConect.Dispose();
                            bdSqlDefConect.Close();
                            return true;
                        // break;
                        case "Sql01":
                            bdSql01Conect.Dispose();
                            bdSql01Conect.Close();
                            return true;
                        // break;
                        case "Sql02":
                            bdSql02Conect.Dispose();
                            bdSql02Conect.Close();
                            return true;
                        //break;

                    }
                    return false;
                }
                catch (SqlException ex)
                {
                    ErrorBDEnd.Message = ex.Message;
                    ErrorBDEnd.ErrorCode = ex.ErrorCode;
                    ErrorBDEnd.ErrorNumber = ex.Number;

                    // Протоколировать исключение
                    if (SystemConecto.DebagApp)
                    {
                        SystemConecto.ErorDebag("Ошибка соединения: " + ex.ToString(), 1);
                    }
                    return false;
                }
            }
            if (TypeConnection.IndexOf("Fb") > -1)
            {

                System.Reflection.MethodInfo mi = typeof(DBConecto).GetMethod("DBcloseFB", new Type[] { typeof(string) });
                if (mi != null)
                {
                    return Convert.ToBoolean(mi.Invoke(null, new object[] { TypeConnection }).ToString());
                }


            }

            if (TypeConnection.IndexOf("DBF") > -1)
            {
                try
                {
                    switch (TypeConnection)
                    {
                        case "DBFDef":

                            return true;
                        // break;
                        case "DBF01":

                            return true;
                        //break;
                        case "DBF02":

                            return true;
                        //break;;

                    }
                    return false;
                }
                catch (Exception ex)
                {
                    ErrorBDEnd.Message = ex.Message;

                    // Протоколировать исключение
                    if (SystemConecto.DebagApp)
                    {
                        SystemConecto.ErorDebag("Ошибка соединения: " + ex.ToString(), 1);
                    }
                    return false;
                }
            }

            return false;
        }
        #endregion


        #region Методы обработчики событий работы с БД закрыть или открыть соединение, освобождение ресурсов

        /// <summary>
        /// Возникает при вызове метода Didpose, метод освобождает занимаемые ресурсы (сборка мусора)
        /// неявно вызывается метод close()
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void conn_Disposed(object sender, EventArgs e)
        {

            var ClassBd = sender as SqlConnection; //SqlConnection;
            // SystemConecto.ErorDebag("Соединение закрылось: " + e.ToString(), 1);

            // WindowView.bdSqlDefName
            // SystemConecto.ErorDebag("Ошибка соединения: " + ex.ToString(), 1);
            // label2.Text += "Событие Dispose";
        }

        /// <summary>
        /// Возникает при открытии или закрытии соединения, информируется о текущем состоянии соединения.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void conn_StateChange(object sender, StateChangeEventArgs e)
        {
            var ClassBd = sender as SqlConnection;
            // SystemConecto.ErorDebag("Соединение : " + e.CurrentState.ToString(), 1);
            // label1.Text += "\nИсходное состояние: " + e.OriginalState.ToString() + "\nТекущее состояние: " + e.CurrentState.ToString();

        }


        #endregion


        #endregion


        #region !! === Разработка Запрос у БД с отдельным подключением
        /// <summary>
        /// Выполнить SQL команду на сервере БД.
        /// 1.Соединится. 2.Выполнить. 3.Отчет о выполнении. 4. Закрыть.
        /// </summary>
        /// <param name="SqlCommand_">SQL команда: SELECT ContactName FROM Customers</param>
        /// <param name="TypeConnection">Тип соединения: 0 - по умолчанию; 1 - FbConection</param>
        //public void DBRequestOne(string SqlCommand_, int TypeConnection = 0)
        //{
        //    switch (TypeConnection)
        //    {
        //        case 0:
        //            // Код обеспечивает очистку ресурсов и гарантирует закрытие любых открытых соединений внутри блока finally.
        //            using (SqlConnection bdSql = new SqlConnection())
        //            {
        //                // Перед запросом обязательно заполнить структуру DBConectionInfo или воспользоватся уже сформированными данными (кеш)
        //                bdSql.ConnectionString = DBConectionInfo.ConnectionString(TypeConnection);
        //                try
        //                {
        //                    //Открыть подключение
        //                    bdSql.Open();





        //                }
        //                catch (SqlException ex)
        //                {
        //                    // Протоколировать исключение

        //                }
        //                finally
        //                {
        //                    // Гарантировать освобождение подключения
        //                    bdSql.Close();
        //                }
        //            }

        //            // ------------------------------------ Пример через OLEDB  - включить using System.Data.OleDb;

        //            //using (OleDbConnection cn = new OleDbConnection("Provider=SQLOLEDB;Data Source=" + server + ";Initial Catalog=" + db + ";Integrated Security=SSPI;"))
        //            //{
        //            //    if (cn.State == ConnectionState.Closed)
        //            //    {
        //            //        cn.Open();
        //            //    }
        //            //    OleDbDataAdapter adapter = new OleDbDataAdapter();
        //            //    adapter.SelectCommand = new OleDbCommand("select * from archive", cn);
        //            //    var ds = new DataSet();
        //            //    adapter.Fill(ds);
        //            //}
        //            break;
        //        // ---------------------- 
        //        case 1:

        //            // Код обеспечивает очистку ресурсов и гарантирует закрытие любых открытых соединений внутри блока finally.
        //            using (FbConnection bdSql = new FbConnection())
        //            {
        //                // Перед запросом обязательно заполнить структуру DBConectionInfo или воспользоватся уже сформированными данными (кеш)
        //                bdSql.ConnectionString = DBConectionInfo.ConnectionString();
        //                try
        //                {
        //                    //Открыть подключение
        //                    bdSql.Open();

        //                    //// Первый параметр - SQL запрос
        //                    //// Второй параметр - ссылка на класс FbConnection
        //                    //FbCommand sqlReqest = new FbCommand("select name, fio, tel from delivers", bdSql);

        //                    //// Выполняем запрос
        //                    //using (FbDataReader r = sqlReqest.ExecuteReader())
        //                    //{
        //                    //    // Читаем результат запроса построчно - строка за строкой
        //                    //    while (r.Read())
        //                    //    {
        //                    //        // Обращение к данным полей запроса осуществляется по их номеру в
        //                    //        // запросе, в данном случае 0 - name, 1 - fio, 2 - tel
        //                    //        string s = r.GetString(0) + " " + r.GetString(1) + r.GetString(2);
        //                    //    }
        //                    //}

        //                    // Если вы собираетесь выполнять запросы изменяющие 
        //                    // данные/структуру БД (запросы insert, delete, update, alter и т.д.), то вам придется использовать 
        //                    // транзакции, реализуемые через класс FbTransaction. Создается он следующим образом:

        //                    // Здесь fbBD - объект типа FbConnection
        //                    // FbTransaction transact = bdSql.BeginTransaction();
        //                    // Все SQL запросы изменяющие содержимое БД должны выполнятся внутри транзакции, для того чтобы это сделать 
        //                    // достаточно передать созданный класс транзакции в конструктор SQL запроса, делается это вот так:
        //                    // Здесь fbBD - класс FbConnection
        //                    // transact - класс транзакци FbTransaction
        //                    // FbCommand sqlReqest = new FbCommand("delete from table1", bdSql, transact);


        //                    // Подтверждаем выполненные транзакции или транзакцию (лучше для каждой транзакции, особенно если следующия нуждается в предыдущей)
        //                    // transact.Commit(); 

        //                }
        //                catch (SqlException ex)
        //                {
        //                    // Протоколировать исключение

        //                }
        //                finally
        //                {

        //                    // Гарантировать освобождение подключения
        //                    bdSql.Close();
        //                }
        //            }
        //            break;
        //    }


        //}
        #endregion

        #region Заметки о БД MS SQL 2008 R2
        /// ------------------------------------------- Оптимизация БД на машинах и в сети
        
        #endregion


    }

    #region Создание правила, строки соединения с БД от разных поставщиков для ADO.NET
    /// <summary>
    /// Создание правила, строки соединения с БД от разных поставщиков для ADO.NET 
    /// </summary>
    partial class ConnectionStringBD
    {
        /// <summary>
        /// Информация о способе подключения к БД (Меняет елементы в зависимости от БД)
        /// Используется для производительности так как структура в C# это не копия в памяти, а прямое распределение
        /// </summary>
        struct DBConnectionInfo
        {
            // ----------------------- Свойства подключения модели ADO.NET используем общие данные структуры
            /// <summary>
            /// имя базы данных, с которой нужно установить сеанс.
            /// </summary>
            public string InitialCatalog { get; set; }
            /// <summary>
            /// определяет имя или адрес машины, на которой расположена база данных. 
            /// Значения елемента: {(local)} текущая локальная машина; {\SQLEXPRESS} сообщает поставщику SQL Server, что вы подключаетесь к стандартной инсталляции SQL Server Express
            /// Примеры: (local)\SQLEXPRESS или 190.190.200.100,1433
            /// </summary>
            public string DataSource { get; set; }
            /// <summary>
            /// время тайм-аймаута для характеристики подключения во время выполнения, пример: Connect Timeout=30
            /// </summary>
            public int ConnectTimeout { get; set; }
            /// <summary>
            /// Определение безопасного соединения Да или НЕТ (Авторизация). Значения елемента:  true, false, SSPI (что эквивалентно true), которое использует для аутентификации пользователя текущие полномочия учетной записи Windows.
            /// Если значение null то определяется - SSPI.
            /// </summary> 
            public bool IntegratedSecurity { get; set; }
            /// <summary>
            /// Имя пользователя БД
            /// </summary>
            public string UserID { get; set; }
            /// <summary>
            /// Пароль пользователя БД
            /// </summary>
            public string Password { get; set; }
            /// <summary>
            /// Сетевая библиотека, используемая для подключения к экземпляру Server.
            /// Это, как использовать TCP / IP вместо Named Pipes. В конце источника данных является порт. 1433 порт по умолчанию для SQL Server.
            /// Если значение null то определяется - DBMSSOCN=TCP/IP.
            /// Поддерживаемые значения: dbnmpntw (Named Pipes), dbmsrpcn (Multiprotocol, Windows RPC), dbmsadsn (Apple Talk), dbmsgnet (VIA), dbmslpcn (Shared Memory, local machine only) and dbmsspxn (IPX/SPX), dbmssocn (TCP/IP) and Dbmsvinn (Banyan Vines).
            /// </summary>             
            public string NetworkLibrary { get; set; }
            /// <summary>
            /// Максимальное и минимальное количество подключений в пуле.
            /// </summary>
            public string MaxPoolSize { get; set; }
            public string MinPoolSize { get; set; }
            /// <summary>
            /// Кодировка БД (подерживается БД Firebird)
            /// </summary>
            public string Charset { get; set; }
        }
        // ----------------- Свойства подключения модели ADO.NET используя объектную модель

        /// <summary>
        /// имя базы данных, с которой нужно установить сеанс.
        /// </summary>
        public string InitialCatalog_o { get; set; }
        /// <summary>
        /// определяет имя или адрес машины, на которой расположена база данных. 
        /// Значения елемента: {(local)} текущая локальная машина; {\SQLEXPRESS} сообщает поставщику SQL Server, что вы подключаетесь к стандартной инсталляции SQL Server Express
        /// Примеры: (local)\SQLEXPRESS или 190.190.200.100,1433
        /// </summary>
        public string DataSource_o { get; set; }
        /// <summary>
        /// время тайм-аута для характеристики подключения во время выполнения, ожидания ответа сервера до генерации ошибки в секундах. 
        /// Значением по умолчанию является 15 (секунд). пример: Connect Timeout=30
        /// </summary>
        public int ConnectTimeout_o { get; set; }
        /// <summary>
        /// Определение безопасного соединения Да или НЕТ (Авторизация). Значения елемента:  true, false, SSPI (что эквивалентно true), которое использует для аутентификации пользователя текущие полномочия учетной записи Windows.
        /// Если значение null то определяется - SSPI.
        /// Второе название Trusted_Connection
        /// </summary> 
        public string IntegratedSecurity_o { get; set; }
        /// <summary>
        /// Имя пользователя БД
        /// </summary>
        public string UserID_o { get; set; }
        /// <summary>
        /// Пароль пользователя БД
        /// </summary>
        public string Password_o { get; set; }
        /// <summary>
        /// Сетевая библиотека, используемая для подключения к экземпляру Server.
        /// Опция Network Library важна, если вы связываетесь с сервером по протоколу, отличному от TCP/IP. Значение по умолчанию для Network Library - это dbmssocn или TCP/IP.
        /// Это, как использовать TCP / IP вместо Named Pipes. В конце источника данных является порт. 1433 порт по умолчанию для SQL Server.
        /// Если значение null то определяется - DBMSSOCN (протокол TCP/IP).
        /// Поддерживаемые значения: dbnmpntw (Named Pipes), dbmsrpcn (Multiprotocol, Windows RPC), dbmsadsn (Apple Talk), dbmsgnet (VIA), dbmslpcn (Shared Memory, local machine only) and dbmsspxn (IPX/SPX), dbmssocn (TCP/IP) and Dbmsvinn (Banyan Vines).
        /// </summary>             
        public string NetworkLibrary_o { get; set; }
        /// <summary>
        /// Максимальное и минимальное количество подключений в пуле.
        /// </summary>
        public string MaxPoolSize_o { get; set; }
        public string MinPoolSize_o { get; set; }

        // -------------------------- другие настройки
        /// <summary>
        /// Кодировка БД (подерживается БД Firebird)
        /// </summary>
        public string Charset_o { get; set; }

        /// <summary>
        /// Тип сервера (локальный или сетевой БД) (подерживается БД Firebird .. разработка для других)
        /// </summary>
        public string ServerType_o { get; set; }

        /// <summary>
        /// Тип библиотеки ее путь (локальный или сетевой БД) (подерживается БД Firebird .. разработка для других)
        /// </summary>
        public string TypeDllPutch { get; set; }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="TypeConnecto">Тип соединения: 0-по умолчанию MS SQL Server;</param>
        /// <returns>Сформированная строка соединения с БД</returns>
        public string ConnectionString(int TypeConnection = 0)
        {
            string ConnectionString = "";
            // Тип  соединения может зависить не только от поставщика БД но и от параметров соединения
            switch (TypeConnection)
            {
                case 0:
                    // Отладка
                    // string ConnectionString = @"Data Source=(local)\SQLEXPRESS;Initial Catalog=AutoLot;Integrated Security=SSPI;Pooling=False";

                    // Настройка соединения с сервером БД
                    //";Network Library=" + (this.NetworkLibrary_o == null ? "DBMSSOCN" : this.NetworkLibrary_o) - затребованный протокол не обрабатывает сервер

                    // Настройка авторизации
                    string Autiriz = "";
                    if (this.UserID_o == null)
                    {
                        Autiriz = this.IntegratedSecurity_o == null ? "Integrated Security=SSPI" : this.IntegratedSecurity_o == "true" ? "Integrated Security=true" : "Integrated Security=false";
                    }
                    else
                    {
                        Autiriz = "User ID=" + this.UserID_o + ";Password=" + this.Password_o;
                    }

                    ConnectionString = @"Data Source=" + this.DataSource_o +
                        ";Initial Catalog=" + this.InitialCatalog_o + ";"
                        + Autiriz;


                    break;
                case 1:
                    // Конструкции поиска и обращения к методам 

                    //    public static System.Reflection.MethodInfo HasMethod(string methodName, object objectToCheck) { var type = objectToCheck.GetType(); return type.GetMethod(methodName); }
                    //    HasMethod("ConnectionStringFB", this);

                    // Создаем экземпляр объекта
                    //

                    // доступ к новому оюбъекту var t = new ConnectionStringBD(); mi.Invoke(t, new object[] { 0 });

                    System.Reflection.MethodInfo mi = typeof(ConnectionStringBD).GetMethod("ConnectionStringFB", new Type[] { typeof(int) });
                    if (mi != null)
                    {
                        ConnectionString = mi.Invoke(this, new object[] { 0 }).ToString();
                    }
                    //else
                    //{
                    //    SystemConecto.ErorDebag("Не нашел метод", 1);
                    //}

                    break;

            }

            return ConnectionString;
        }



        // описание свойства ConnectionString объекта подключения, дает поставщик данных БД.
        // Способы соединения с БД:
        // --------------------------------------------------  SQL Server 2005 or 2008          (соединения указанны не все)

        // --------- .NET Framework Data Provider for SQL Server
        //Type .NET Framework Class Library
        //Usage System.Data.SqlClient.SqlConnection
        //Manufacturer Microsoft
        // Пример формирования: ConnectionString = @"Data Source=MICROSOF-1EA29E\SQLEXPRESS;Initial Catalog=AutoLot;" + "Integrated Security=SSPI;Pooling=False";

        // --------- SQL Server Native Client 10.0 OLE DB Provider
        //Type OLE DB Provider
        //Usage Provider=SQLNCLI10
        //Manufacturer Microsoft
        // Provider=SQLOLEDB.1;Password=123;Persist Security Info=True;User ID=sa;Initial Catalog=DataBaseName;Data Source=USER\SQLEXPRESS

        // --------- .NET Framework Data Provider for OLE DB
        // --------- .NET Framework Data Provider for ODBC
        // --------- SQLXML 4.0 OLEDB Provider

        // Пример подключения по умолчанию класса
        // Data Source=190.190.200.100,1433;Network Library=DBMSSOCN;Initial Catalog=myDataBase;User ID=myUsername;Password=myPassword;


        // --------------------------------------------------  MySQL              (соединения указанны не все)

        // --------- MySQL Connector/Net
        //Type .NET Framework Class Library
        //Usage MySql.Data.MySqlClient.MySqlConnection
        //Manufacturer MySQL
        // Server=myServerAddress;Database=myDataBase;Uid=myUsername;Pwd=myPassword; (Port=3306; -по умолчанию)

        // --------- .NET Framework Data Provider for OLE DB
        //Type .NET Framework Wrapper Class Library
        //Usage System.Data.OleDb.OleDbConnection
        //Manufacturer Microsoft
        // Provider=MySQLProv;Data Source=mydb;User Id=myUsername;Password=myPassword;

        // --------------------------------------------------  Firebird              (соединения указанны не все)

        // --------- Firebird ADO.NET Data Provider
        //Type .NET Framework Class Library
        //Usage
        //Manufacturer Firebird
        // User=SYSDBA;Password=masterkey;Database=SampleDatabase.fdb;DataSource=localhost; 
        // Port=3050;Dialect=3; Charset=NONE;Role=;Connection lifetime=15;Pooling=true; MinPoolSize=0;
        // MaxPoolSize=50;Packet Size=8192;ServerType=0;

        // --------------------------------------------------  Visual FoxPro / FoxPro 2.x (соединения указанны не все)

        // --------- VFP OLE DB Provider
        //Type OLE DB Provider
        //Usage Provider=vfpoledb
        //Manufacturer Microsoft
        // Provider=vfpoledb;Data Source=C:\MyDbFolder\MyDbContainer.dbc;Collating Sequence=machine;
        // Provider=vfpoledb;Data Source=C:\MyDataDirectory\;Collating Sequence=general;

        // --------- .NET Framework Data Provider for OLE DB            (читать дополнительно про класс)
        //Type .NET Framework Wrapper Class Library
        //Usage System.Data.OleDb.OleDbConnection
        //Manufacturer Microsoft
        // Provider=vfpoledb;Data Source=C:\MyDbFolder\MyDbContainer.dbc;Collating Sequence=machine;

        // --------------------------------------------------  DBF / FoxPro ( Смотри Visual FoxPro / FoxPro 2.x. Соединения указанны не все, чтение старых версий.)
        // --------- NET Framework Data Provider for OLE DB
        //Type .NET Framework Wrapper Class Library
        //Usage System.Data.OleDb.OleDbConnection
        //Manufacturer Microsoft
        // Provider=Microsoft.Jet.OLEDB.4.0;Data Source=c:\folder;Extended Properties=dBASE IV;User ID=Admin;Password=;

    }
    #endregion

    #region Функции сервера

    // Список пользователей

    #endregion

    /// <summary>
    /// Список параметров ошибок
    /// </summary>
    public class ErrorBD
    {
        /// <summary>
        /// Возвращает cообщение об ошибке
        /// </summary>
        public string Message { get; set; }
        /// <summary>
        /// Возвращает код HRES ошибки
        /// </summary>
        public int ErrorCode { get; set; }

        /// <summary>
        /// Возвращает код ошибки Number 
        /// </summary>
        public int ErrorNumber { get; set; }

        // ===================== Пример полной формы записи
        //private double _seconds;
        //public double Seconds
        //{
        //    get { return _seconds; }
        //    set { _seconds = value; }
        //}

    }

    /// <summary>
    /// Список окон для вывода сообщений сообытий в базовый объект Окна. 
    /// </summary>
    public class WindowEventBD
    {
        /// <summary>
        /// Имя окна в базовом потоке
        /// </summary>
        public string bdSqlDefName { get; set; }
        /// <summary>
        /// Имя окна в потоке 1
        /// </summary>
        public string bdSql01Name { get; set; }
        /// <summary>
        /// Имя окна в потоке 2
        /// </summary>
        public string bdSql02Name { get; set; }

    }

    //static partial class bdSqlDefConect : SqlConnection
    //{
    //    #region Глобальные переменные

    //    /// <summary>
    //    /// Последняя ошибка соединения с БД
    //    /// </summary>
    //    public static ErrorBD ErrorBDEnd;

    //    public static WindowEventBD WindowView;

    //    #endregion
    //}
}
