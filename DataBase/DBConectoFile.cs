using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
// ---- BD
using System.Data;              // Содержит типы, независимые от провайдеров, например DataSet и DataTable.
using System.Data.OleDb;        // System.Data.OleDb. Содержит типы OLE DB .NET Data Provider.
// Управление вводом-выводом
using System.IO;


// Серезная альтернатива драйверу OLEDB Microsoft.Jet.OLEDB.4.0 размещен в низу ConsoleApplication37
using System.Text.RegularExpressions;



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

        #endregion

        #region Строка подключения
        //Provider=VFPOLEDB.1;
        //Data Source=D:\codeC#\shopdb\shop.dbc;
        //Mode=Read;
        //Extended Properties="";
        //User ID="";
        //Password="";
        //Mask Password=False;
        //Cache Authentication=False;
        //Encrypt Password=False;
        //Collating Sequence=MACHINE;
        //DSN="";
        //DELETED=True;
        //CODEPAGE=1251;
        //MVCOUNT=16384;
        //ENGINEBEHAVIOR=90;
        //TABLEVALIDATE=3;
        //REFRESH=5;
        //VARCHARMAPPING=False;
        //ANSI=True;
        //REPROCESS=5.

        #endregion


        //"Source", "Target"
        //"Red", "Красный"
        //"Orange","Оранжевый"
        //"Yellow", "Желтый"
        //"Green", "Зеленый"
        //"Blue", "Синий"
        //"Violet", "Фиолетовый"

        /// <summary>
        /// Чтение файла CSV с носителя разделител по умолчанию {,}
        /// </summary>
        /// <param name="NameCSVFile"></param>
        /// <param name="Putch"></param>
        /// <param name="NameTable"></param>
        /// <param name="?"></param>
        /// <returns></returns>     
        public static DataTable ReadCSVDefault(string NameCSVFile, string Putch, string NameTable="ReadCSV")
        {
            // Определяем Таблицу
            DataTable AllRowsCSV = new DataTable(NameTable);

            //Определяем подключение
            OleDbConnection StrCon = new OleDbConnection(@"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + Putch + ";Extended Properties=text"); // HDR=No;FMT=Delimited"""
            try
            {    
                //Строка для выборки данных
                string Select1 = string.Format("SELECT * FROM [{0}]", NameCSVFile);
                //Создание объекта Command
                using (OleDbCommand comand1 = new OleDbCommand(Select1, StrCon))
                {
                    //Определяем объект Adapter для взаимодействия с источником данных
                    OleDbDataAdapter adapter1 = new OleDbDataAdapter(comand1);

                    //Определяем объект DataSet (кеш не используем)
                    // DataSet AllTables = new DataSet();

                    //Открываем подключение
                    StrCon.Open();

                    //Заполняем DataSet таблицей из источника данных
                    adapter1.Fill(AllRowsCSV); //AllTables
                    //Заполняем обект datagridview для отображения данных на форме
                    //dataGridView1.DataSource = AllTables.Tables[0];
                }
                
            }
            catch (Exception ex)
            {
                // ошибки
                SystemConecto.ErorDebag("File : " + Environment.NewLine +
                    " === Message: " + ex.Message.ToString() + Environment.NewLine +
                    " === Exception: " + ex.ToString(), 1);
            }
            finally
            {
                if(StrCon!=null)
                    StrCon.Close();
            }
            return AllRowsCSV;
        }


        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                StreamWriter sw = new StreamWriter(@"D:\Exp\Dictionary1.txt", false, Encoding.Default);
                //Добавление имен столбцов
                //for (int j = 0; j < dataGridView1.ColumnCount; j++)
                //{s
                //    sw.Write(dataGridView1.Columns[j].HeaderText);
                //    if (j < dataGridView1.ColumnCount - 1)
                //        sw.Write(",");
                //}
                //sw.WriteLine();
                //for (int i = 0; i < dataGridView1.RowCount; i++)
                //{
                //    for (int j = 0; j < dataGridView1.ColumnCount; j++)
                //    {
                //        sw.Write(dataGridView1.Rows[i].Cells[j].Value);
                //        if (j < dataGridView1.ColumnCount - 1)
                //            sw.Write(",");
                //    }
                //    sw.WriteLine();
                //}
                sw.Flush();
                sw.Close();
            }
            catch (Exception ex)
            {
                // ошибки
                SystemConecto.ErorDebag("File : " + Environment.NewLine +
                    " === Message: " + ex.Message.ToString() + Environment.NewLine +
                    " === Exception: " + ex.ToString(), 1);
            }
        }






    }
}




namespace ConsoleApplication37
{
    class Program
    {
        static void Main(string[] args)
        {
            //формируем тестовый CSV файл
            string csv = "1,\"d, 3, can\"\"t\",13\n1,23,\"d, \n3, don\"\"t\"\n1,2,3\n\"1\",2,\"3\"";
            string fileName = Path.GetTempFileName();
            File.WriteAllText(fileName, csv);
            //читаем строки
            CsvParser parser = new CsvParser();
            foreach(var items in parser.Parse(File.ReadLines(fileName)))
            {
                //читаем поля строки
                foreach (var item in items)
                    Console.Write(item + " - ");
                Console.WriteLine();
            }

            Console.ReadLine();
        }
    }

    // Парсер CSV
    public class CsvParser
    {
        readonly char separator=',';
        readonly char quote='"';

        public IEnumerable<List<string>> Parse(IEnumerable<string> lines)
        {
            var e = lines.GetEnumerator();
            while(e.MoveNext())
                yield return ParseLine(e);
        }

        private List<string> ParseLine(IEnumerator<string> e)
        {
            var items = new List<string>();
            foreach (string token in GetToken(e))
                items.Add(token);
            return items;
        }

        private IEnumerable<string> GetToken(IEnumerator<string> e)
        {
            string token = "";
            State state = State.outQuote;

        again:
            foreach(char c in e.Current)
                switch (state)
                {
                    case State.outQuote:
                        if (c == separator)
                        {
                            yield return token;
                            token = "";
                        }else
                        if (c == quote)
                            state = State.inQuote;
                        else
                            token += c;
                        break;
                    case State.inQuote:
                        if (c == quote)
                            state = State.mayBeOutQuote;
                        else
                            token += c;
                        break;
                    case State.mayBeOutQuote:
                        if (c == quote)
                        {
                            //кавычки внутри кавычек
                            state = State.inQuote;
                            token += c;
                        }
                        else
                        {
                            state = State.outQuote;
                            goto case State.outQuote;
                        }
                        break;
                }

                //разрыв строки внутри кавычек
                if (state == State.inQuote && e.MoveNext())
                {
                    token += Environment.NewLine;
                    goto again;
                }

            yield return token;
        }

        enum State { outQuote, inQuote, mayBeOutQuote }


        // ----------------------------------------------- Еще вариант один


        private DataTable CreateTableFromOutputStream(string outputStreamText, string tableName)
        {

            //Process output and return

            string[] split = outputStreamText.Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);



            if (split.Length >= 2)
            {

                int iteration = 0;

                DataTable table = null;



                foreach (string values in split)
                {

                    if (iteration == 0)
                    {

                        string[] columnNames = SplitString(values);

                        table = new DataTable(tableName);



                        List<DataColumn> columnList = new List<DataColumn>();



                        foreach (string columnName in columnNames)
                        {

                            columnList.Add(new DataColumn(columnName));

                        }



                        table.Columns.AddRange(columnList.ToArray());

                    }

                    else
                    {

                        string[] fields = SplitString(values);

                        if (table != null)
                        {

                            table.Rows.Add(fields);

                        }

                    }



                    iteration++;

                }



                return table;

            }



            return null;

        }



        private string[] SplitString(string inputString)
        {

            System.Text.RegularExpressions.RegexOptions options = ((System.Text.RegularExpressions.RegexOptions.IgnorePatternWhitespace | System.Text.RegularExpressions.RegexOptions.Multiline)

                        | System.Text.RegularExpressions.RegexOptions.IgnoreCase);

            Regex reg = new Regex("(?:^|,)(\\\"(?:[^\\\"]+|\\\"\\\")*\\\"|[^,]*)", options);

            MatchCollection coll = reg.Matches(inputString);

            string[] items = new string[coll.Count];

            int i = 0;

            foreach (Match m in coll)
            {

                items[i++] = m.Groups[0].Value.Trim('"').Trim(',').Trim('"').Trim();

            }

            return items;

        }




    }
}