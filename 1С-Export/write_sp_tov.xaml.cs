using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;


// Управление вводом-выводом
using System.IO;
using System.Text;

using System.Data;

using System.Windows.Controls.Primitives;
using System.Threading.Tasks;



namespace ConectoWorkSpace._1С_Export
{
    /// <summary>
    /// Логика взаимодействия для write_act_real.xaml
    /// </summary>
    public partial class write_sp_tov : UserControl
    {
        /// <summary>
        /// Путь пользовательских данных
        /// </summary>
        private static string PuthUser = SystemConecto.PutchApp + @"config\user\";
        /// <summary>
        /// Полный Путь к файлу SQL запроса
        /// </summary>
        private static string Putch1FileSQL = PuthUser + "import_stv.sql.fb.txt";

        /// <summary>
        /// Название файла XML данным - Название полей
        /// </summary>
        private static string NameFXMlColumn = "namecolumn_stv.fb.xml";

        /// <summary>
        /// Полный Путь к XML данным - Название полей
        /// </summary>
        private static string PuthXMlColumn = PuthUser + NameFXMlColumn;

        

        /// <summary>
        /// Чтение перезаписываемого SQL файла
        /// </summary>
        public write_sp_tov()
        {
            InitializeComponent();

            string[] ReadSql = SystemConecto.ReadFile_SqlPass(Putch1FileSQL);

            var FileText = Convert.ToInt32(ReadSql[0]) == 0 ? Properties.Resources.import_stv_sql_fb : ReadSql[1];


            rtxtText_sp_tov.AppendText(FileText);

        }

        /// <summary>
        /// Информация о названии колонок
        /// </summary>
        Dictionary<string, string> InfoColumn = new Dictionary<string, string>();

        /// <summary>
        /// Управление масивом  InfoColumn  - Информация о названии колонок
        /// </summary>
        /// <param name="DopInfoUser">Дополнительное описание введенное пользователем Сохраняется пока осуществляется сеанс (временное сохранение)</param>
        private void LoadInfoColumn(Dictionary<string, string> DopInfoUser)
        {
            // Обнулить
            InfoColumn = new Dictionary<string, string>();
            // Заполнить первичными данными
            InfoColumn.Add("kod", "Номенклатура (код)");
            InfoColumn.Add("name", "Номенклатура (наименование)");
            InfoColumn.Add("edid", "Единица измерения (код)");
            InfoColumn.Add("ed", "Единица измерения (название)");
            InfoColumn.Add("dat", "Дата");
            InfoColumn.Add("num", "Номер");
            InfoColumn.Add("podr", "Склад(код)");
            InfoColumn.Add("podr_name", "Склад (наименование)");
            InfoColumn.Add("org_our", "Наша внутренняя организация (код)");
            InfoColumn.Add("org_our_name", "Наша внутренняя организация (наименование)");
            InfoColumn.Add("tov", "Номенклатура (код)");
            InfoColumn.Add("tov_name", "Номенклатура (наименование)");
            InfoColumn.Add("cnt", "Количество");
            InfoColumn.Add("pricends", "Цена с НДС");
            InfoColumn.Add("summands", "Сумма с НДС");
            InfoColumn.Add("nds", "Ставка НДС");
            InfoColumn.Add("post", "Организация (код)");
            InfoColumn.Add("org_name", "Организация (наименование)");
            InfoColumn.Add("ed_name", "Единица измерения (название)");
            InfoColumn.Add("num_post", "Номер накладной поставщика");

            InfoColumn.Add("type_doc", "Тип списания");
            InfoColumn.Add("type_doc_name", "Тип списания (название)");
            InfoColumn.Add("org", "Организация (код)");
            InfoColumn.Add("day_doc", "Вид списания");
            InfoColumn.Add("day_doc_name", "Вид списания (название)");



            InfoColumn.Add("type_slip", "Тип оплаты");
            InfoColumn.Add("type_slip_name", "Тип оплаты (название)");

            // Добавить Пользовательские данные
            foreach (var pair in DopInfoUser)
            {
                if (InfoColumn.ContainsKey(pair.Key))
                {
                    InfoColumn[pair.Key] = pair.Value;
                }
                else
                {
                    InfoColumn.Add(pair.Key, pair.Value);
                }
            }

        }


        /// <summary>
        /// Событие сохранить
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Toolb_act_real_Click(object sender, RoutedEventArgs e)
        {

            //Видимость панели инструментов
            // Пример обращения к меню MenuItem mItem = sender as MenuItem; // mItem.IsChecked  TextRange allTextRange
            string _Text = new TextRange(rtxtText_sp_tov.Document.ContentStart, rtxtText_sp_tov.Document.ContentEnd).Text;

            this.Cursor = Cursors.Wait;

            Dictionary<string, string> UserInfoColumn = new Dictionary<string, string>();
            // Сохранить пользовательские изменения
            if(OFile!=null){
               UserInfoColumn = OFile.DataTableOneRowtoDictionary(myDataSet.Tables["ColumnQuery"]);
            }
            


            // Валидация запроса пока отсутствует 

            myDataSet = ChekQuerySql(_Text);  // ColumnQuery_Tmp

            // Копирование данных о запросе
            DataTable copyDataTable;
            copyDataTable = myDataSet.Tables["ColumnQuery_Tmp"].Copy();
            copyDataTable.TableName = "Query";
            
            myDataSet.Tables.Add(copyDataTable);
            gridQuery.IsReadOnly = true;

            // Запись файла колонок
            if (myDataSet.Tables.Contains("ColumnQuery_Tmp"))
            {
                // Первый запуск предложенный вариант глобальный масив InfoColumn
                LoadInfoColumn(UserInfoColumn);
                
                try
                {
                    myDataSet.Tables["ColumnQuery_Tmp"].Clear();
                    // Создаем файл
                    if(OFile==null){
                        //myDataSet.Tables["ColumnQuery"].TableName = "NewColumnComent";

                        // Старый пример
                        //string[] TitleCSV = new string[4]{"Номенклатура (код)", "Номенклатура (наименование)", "Единица измерения (код)", 
                        //                    "Единица измерения (название)"};
                    
                        OFile = new ConectoFileXML.ConnectFXML(NameFXMlColumn, PuthUser);
                        // Сохранить структуру и вернуть ее в виде таблицы
                        DataTable NewColumn = OFile.DataTableColumnComment(myDataSet.Tables["ColumnQuery_Tmp"], InfoColumn);
                        NewColumn.TableName = "ColumnQuery";
                        myDataSet.Tables.Add(NewColumn);
                    }
                    else
                    {
                        // Опускаем переменную SaveChengStr = true Пользовательское изменение данных
                        // Обновляем данные при любых событиях
                        DataTable NewColumn = OFile.DataTableColumnComment(myDataSet.Tables["ColumnQuery_Tmp"], InfoColumn, 1);
                        NewColumn.TableName = "ColumnQuery";
                        myDataSet.Tables.Add(NewColumn);


                    }


                }
                catch  //(DataException e)
                {
                    // Process exception and return.
                    //Console.WriteLine("Exception of type {0} occurred.",
                    //    e.GetType());
                }
            }


            OnRefreshData();

            SystemConecto.WriteFile_SqlPass(Putch1FileSQL, ref _Text);

            this.Cursor = null;
        }

        /// <summary>
        /// Печать кода
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Print_act_real(object sender, RoutedEventArgs e)
        {

            PrintDialog pd = new PrintDialog();
            if ((pd.ShowDialog() == true))
            {
                //use either one of the below      
                //pd.PrintVisual(rtxtText_act_real as Visual, "printing as visual");
                //pd.PrintDocument((((IDocumentPaginatorSource)rtxtText_act_real.Document).DocumentPaginator), "printing as paginator");
            }

        }


        #region Новый вариант связи
        /// <summary>
        /// Кеш данных
        /// </summary>
        DataSet myDataSet = null;

        /// <summary>
        /// Состояние изменения данных: true - есть не сохраненные данные
        /// </summary>
        bool SaveChengStr = false;

        /// <summary>
        /// Сссылка на к XML данным - Название полей
        /// </summary>
        ConectoFileXML.ConnectFXML OFile =null; // new ConectoFileXML.ConnectFXML("","{mem}=")

        /// <summary>
        /// Загрузка пользователского окна
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnInit(object sender, EventArgs e)
        {

            #region Разработка

            //string mdbFile = Path.Combine(AppDataPath, "BookData.mdb");
            //string connString = string.Format(
            //    "Provider=Microsoft.Jet.OLEDB.4.0; Data Source={0}", mdbFile);
            //OleDbConnection conn = new OleDbConnection(connString);
            //OleDbDataAdapter adapter = new OleDbDataAdapter("SELECT * FROM BookTable;", conn);
            //=====================================================================
            // Удаление таблицы не работает
            //DataTableCollection tablesCol = myDataSet.Tables;
            //if (tablesCol.Contains("ColumnQuery") && tablesCol.CanRemove(tablesCol["ColumnQuery"])){
            //    tablesCol.Remove("ColumnQuery");
            //} 

            //=====================================================================
            //DataTable n_dataTable = new DataTable("ColumnQuery");

            //n_dataTable.Columns.Add("col0");
            //n_dataTable.Columns.Add("col1");
            //n_dataTable.Columns.Add("col2");

            ////n_dataTable.Rows.Add("data00", "data01", "data02");

            //myDataSet.Tables.Add(n_dataTable);

            //// adapter.Fill(myDataSet, "BookTable");

            //// this.grid1 (myListBox) is a ListBox control.
            //// Set the DataContext of the ListBox to myDataSet
            //this.grid1.DataContext = myDataSet;

            //if (myDataSet.Tables["ColumnQuery"].Rows.Count > 0)
            //{
            //    this.grid1.Visibility = Visibility.Visible;
            //}
            //else
            //{
            //    this.grid1.Visibility = Visibility.Hidden;
            //    this.gridQuery.Visibility = Visibility.Collapsed;
            //    infoGridQuery.Visibility = Visibility.Collapsed;
            //}

            //=====================================================================
            // this.grid1.DataContext = myDataSet;
            // Установите свойство NotifyOnTargetUpdated или NotifyOnSourceUpdated (или оба) в значение true в привязке. 
            //Обработчик, который предоставляется для прослушивания данного события, должен быть присоединен непосредственно к элементу, 
            // об изменениях которого необходимо информировать, или к контексту общих данных, если необходимо информировать обо всех изменениях в контексте. 

            // ="{Binding Path=Rent, Mode=OneWay, NotifyOnTargetUpdated=True}"
            // TargetUpdated="OnTargetUpdated"

            /*
             * 
             * private void OnTargetUpdated(Object sender, DataTransferEventArgs args)
                {

                  // Handle event

                ...

                }
             * 
             */



            #endregion


            if (myDataSet != null)
            {
                OnRefreshData();
            }
            else
            {
                // Загрузка из сохраненного XML файла
                myDataSet = new DataSet();

                // Загрузить конфиг. пользователские файлы
                if (OFile==null && File.Exists(PuthXMlColumn))
                {
                    //
                    OFile = new ConectoFileXML.ConnectFXML(NameFXMlColumn, PuthUser);
                    DataTable NewColumn = OFile.XMLNColumntoDataTable();
                    NewColumn.TableName = "ColumnQuery";
                    myDataSet.Tables.Add(NewColumn);
                }
                else
                {
                    // Нет данных


                }




                OnRefreshData();

            }



        }





        /// <summary>
        /// Обновление данных
        /// </summary>
        private void OnRefreshData()
        {
            if (myDataSet.Tables.Contains("ColumnQuery"))
            {
                // Настройка колонок
                this.grid1.DataContext = myDataSet;

                if (myDataSet.Tables.Contains("Query") && myDataSet.Tables["Query"].Rows.Count > 0)
                {
                    // Ответ сервера в виде таблицы
                    this.gridQuery.DataContext = myDataSet;
                    infoGridQuery.Visibility = Visibility.Visible;
                    this.gridQuery.Visibility = Visibility.Visible;
                }
                else
                {
                    infoGridQuery.Visibility = Visibility.Collapsed;
                    this.gridQuery.Visibility = Visibility.Collapsed;
                }

                // Проверить запрос на ошибку



                //grid1.Items.Refresh();
                this.grid1.Visibility = Visibility.Visible;
            }
            else
            {
                this.grid1.Visibility = Visibility.Hidden;
                infoGridQuery.Visibility = Visibility.Collapsed;
                this.gridQuery.Visibility = Visibility.Collapsed;

            }
        }


        #endregion


        

        /// <summary>
        /// Проверка SQL запроса на стороне сервера
        /// </summary>
        /// <param name="CommandAdapterQuery"></param>
        private DataSet ChekQuerySql(string CommandAdapterQuery)
        {

            int PeriodOpciiQuery = Convert.ToInt32(AppPlayStory_1C.UserconfigWorkSpace["Период_запроса_настройка_експорта"]);
            // Даты
            DateTime dateSs = DateTime.Today.AddDays(-PeriodOpciiQuery);

            string DateS_SqlFB = string.Format("{0}.{1}.{2}", dateSs.Day, dateSs.Month, dateSs.Year);

            DateTime datePos = DateTime.Today;

            // TypeFile - CSV WIN1251 
            // PutchDir_

            string DatePO_SqlFB = string.Format("{0}.{1}.{2}", datePos.Day, datePos.Month, datePos.Year);


            // Тип доступа по подразделениям
            var TypeConfigAutorizeSDR = AppPlayStory_1C.TypeAutorizeSDR(AppPlayStory_1C.UserconfigWorkSpace["TypeAutorizeSDR"]);
            var TypeConfigAutorizeSPORG = AppPlayStory_1C.TypeAutorizeSPORG(AppPlayStory_1C.UserconfigWorkSpace["TypeAutorizeSPORG"]);
            var TypeConfigAutorizeSDRCach = AppPlayStory_1C.TypeAutorizeSDRCach(AppPlayStory_1C.UserconfigWorkSpace["TypeAutorizeSDRCach"]);

            //        CommandAdapterQuery =
            // "Select NUMBER , TRECK_NAME , PERFORMER_NAME , VOCLS , PRO , Language, TypeMuzik  from KARAOKE_CATALOG  ORDER BY TypeMuzik DESC, Performer_Name";
            // Конвертация в формат SQLFB

            /// Подключение к БД FB 2 Local
            
            DBConecto.UniQuery LocalQuery = new DBConecto.UniQuery(
                string.Format(CommandAdapterQuery, DateS_SqlFB, DatePO_SqlFB, TypeConfigAutorizeSDR.RestoranSDR, TypeConfigAutorizeSPORG.RestoranSDR, TypeConfigAutorizeSDRCach.RestoranSDR), "FB");

            // Подключение к БД
            LocalQuery.UserID = SystemConecto.AutorizUser.LoginUserAutoriz;
            LocalQuery.PasswordUser = SystemConecto.AutorizUser.PaswdUserAutoriz;
            LocalQuery.DataSource = AppPlayStory_1C.UserconfigWorkSpace["BDSERVER_IP"];
            LocalQuery.Port = Convert.ToInt16(AppPlayStory_1C.UserconfigWorkSpace["BDSERVER_Port"]);
            LocalQuery.ServerType = "Default";
            LocalQuery.InitialCatalog = AppPlayStory_1C.UserconfigWorkSpace["BDSERVER_Alias"].Length > 0 ? AppPlayStory_1C.UserconfigWorkSpace["BDSERVER_Alias"] :
                AppPlayStory_1C.UserconfigWorkSpace["BDSERVER_Putch-Hide"]; // Одно и тоже что, ParametrsBlock.BDSERVER_Alias и ParametrsBlock.BDSERVER_Putch_Hide - объединил

            LocalQuery.ExecuteQueryFillTable("ColumnQuery_Tmp");

            if (LocalQuery.CacheDBAdapter.Tables.Count == 0 ) //&& LocalQuery.CacheDBAdapter.Tables["KRAOKE_CATALOG"].Rows.Count > 0
            {
                DataSet Tmp = new DataSet();
                DataTable ErrCol = new DataTable("ColumnQuery_Tmp");
                ErrCol.Columns.Add("Ошибка запроса!");
                ErrCol.Rows.Add(LocalQuery.ErrorBD.Message);
                //ErrCol.Rows.Add()
                Tmp.Tables.Add(ErrCol);

                // Отследить изменение данных
                SaveChengStr = true;

                return Tmp;
            }

            SaveChengStr = false;

            return LocalQuery.CacheDBAdapter;


        }


        
        ///// <summary>
        ///// Событие обновление Таблицы
        ///// </summary>
        ///// <param name="sender"></param>
        ///// <param name="args"></param>
        //    private void OnTargetUpdatedColumn(Object sender, DataTransferEventArgs args)
        //        {

        //          // Handle event
        //            SaveChengStr = true;
                

        //        }

        //private void ChangedGrid1(Object sender, SelectionChangedEventArgs args)
        //        {

        //          // Handle event
        //            SaveChengStr = true;
                

        //        }


        //private void DataContextChanged_(object sender, DependencyPropertyChangedEventArgs e)
        //{
        //    // Handle event
        //    SaveChengStr = true;
        //}


        /// <summary>
        /// Фиксация внесения изменений в таблицу DataSet.Table
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EndEditRowColumnQuery(object sender, DataGridRowEditEndingEventArgs e){
           
            SaveChengStr = true;

            //object o = grid1.ItemContainerGenerator.ItemFromContainer(e.Row);

            //object fdf = o;

            //FrameworkElement element_EmpNo = grid1.Columns[0].GetCellContent(e.Row);
            //if (element_EmpNo.GetType() == typeof(String))
            //{
            //    var eno = ((TextBox)element_EmpNo).Text;
            //    var Test = eno;
            //   // objEmpToAdd.EmpNo = Convert.ToInt32(eno);
            //    SaveChengStr = false;
            //}


            //if (e.EditAction == DataGridEditAction.Commit)
            //{

    //SaveChengStr = true;


            //}
           

        }

        #region Таблица полей в запросе Test

        private DataTable _dataTable;

        public DataTable DataTable
        {
            get { return _dataTable; }
            set { _dataTable = value; }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            this._dataTable = new DataTable("table");

            this._dataTable.Columns.Add("col0");
            this._dataTable.Columns.Add("col1");
            this._dataTable.Columns.Add("col2");

            this._dataTable.Rows.Add("data00", "data01", "data02");
            //this._dataTable.Rows.Add("data10", "data11", "data22");
            //this._dataTable.Rows.Add("data20", "data21", "data22");

            this.grid1.DataContext = this;
        }

        #endregion

       

    }


}

