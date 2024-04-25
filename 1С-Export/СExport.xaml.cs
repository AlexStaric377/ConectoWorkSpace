using System;
using System.Collections.Generic;
using System.Linq;
// Управление вводом-выводом
using System.IO;
using System.Text;

using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
// --- 
using ConectoWorkSpace._1С_Export;

// ---- BD
using System.Data;              // Содержит типы, независимые от провайдеров, например DataSet и DataTable.
// ---- TimeNet
using System.Diagnostics;
// ---- Многопоточность
using System.Threading;
using System.Windows.Threading;



namespace ConectoWorkSpace
{
    /// <summary>
    /// Логика взаимодействия для Window1.xaml  Window1.xaml
    /// </summary>
    public partial class СExport : Window
    {

        // Время ожидания разрыва
        Stopwatch WaitNetTimeStart_EXPORT = new Stopwatch();

        private  Grid MessageTextInner = new Grid();

        private Grid TextInfo = new Grid();

        #region Структура данных потока

        // Структура параметров потока обращения к веб серверу { get; set; }
        struct RenderInfo
        {
            /// <summary>
            /// Параметр экспорта дата с 
            /// </summary>
            public string dateSs { get; set; }
            /// <summary>
            /// Параметр экспорта дата по
            /// </summary>
            public string datePos { get; set; }
            /// <summary>
            /// Каталог файлов
            /// </summary>
            public string PutchDir_ { get; set; }
            /// <summary>
            /// Имя файла dbf, csv, xls
            /// </summary>
            public string NameFile_ { get; set; }
            /// <summary>
            /// Свойства файла
            /// </summary>
            public FileInfo FileInfo_ { get; set; }
            /// <summary>
            /// Тип файлы CSV 1251
            /// </summary>
            public string TypeFile { get; set; }
            /// <summary>
            /// Номер выполняемого блока кода программы ( используется для востановления потока при сбросе сетевого соединения)
            /// </summary>
            public int idBlock { get; set; }
            /// <summary>
            /// Окно вызова потока
            /// </summary>
            public Window WinOnStart { get; set; }

            /// <summary>
            /// логин авторизации
            /// </summary>
            public string LoginUserAutoriz { get; set; }

            /// <summary>
            /// пароль авторизации
            /// </summary>
            public string PaswdUserAutoriz { get; set; }

            /// <summary>
            /// адресс сервера
            /// </summary>
            public string BDSERVER_IP { get; set; }
            
            /// <summary>
            /// порт сервера
            /// </summary>
            public string BDSERVER_Port { get; set; }
            /// <summary>
            /// название сессии псевдоним
            /// </summary>
            public string BDSERVER_Alias { get; set; }

            /// <summary>
            /// название сессии путь
            /// </summary>
            public string BDSERVER_Putch_Hide { get; set; }


            /// <summary>
            /// Параметры импорта в потоке нельзя допускать изменения ReadParametrsImport
            /// </summary>
            public Dictionary<string,string> ParametrsImport { get; set; }

            /// <summary>
            /// Параметры импорта прочитаны в потоке null false - нет, true - да
            /// </summary>
            public bool ParametrsImportRead { get; set; }


            /// <summary>
            /// Таблица импортированные данные из файла(dbf, xls, csv), частичные или все (разработка)
            /// </summary>
            public DataTable TableImData { get; set; }


            /// <summary>
            /// Block обрабатываемых записей из файла(dbf, xls, csv) (блоковая загрузка данных во избежания переполнения файлов, разработка)
            /// </summary>
            public int BlockTableImData { get; set; }

            /// <summary>
            /// Номер обрабатываемой записи из таблицы импорта из файла(dbf, xls, csv)
            /// </summary>
            public int NumRowTableImData { get; set; }


            /// <summary>
            /// ColumnSchemaIm<para></para>
            /// Данные считаной строки согласно схемы импорта в виде кеша потока, согласно структуре базовых данных в виде асоциативного масива
            /// </summary>
            public Dictionary<string, string> SchemaTable { get; set; }

            /// <summary>
            /// SchemaCur<para></para>
            /// Данные схемы импорта валюты в виде асоциативного масива Код Валюты импорта - код основной БД
            /// </summary>
            public Dictionary<string, string> SchemaCur { get; set; }

            /// <summary>
            /// Тестирование
            /// Данные считаной строки согласно схемы импорта в виде кеша потока, согласно структуре базовых данных
            /// </summary>
            public BaseColumn DataRowSchemaTableIm { get; set; }


            /// <summary>
            /// Размер "статус бар" полоски, по ширине. - ход выполнения задач.
            /// </summary>
            public double SizeWidthLineStatusBar { get; set; }

            /// <summary>
            /// Количество всех транзакций в потоке
            /// </summary>
            public double CountTranzactionAll { get; set; }


            /// <summary>
            /// Количество принятых транзакций в потоке
            /// </summary>
            public double CountTranzactionWriteBD { get; set; }


        }


         // Структура параметров полей

        struct BaseColumn
        {
            /// <summary>
            /// номер документа бакновской выписки
            /// </summary>
            public string NUM_DOC { get; set; }
            /// <summary>
            /// дата создания ордера
            /// </summary>
            public string DATE_DOC { get; set; }

            // ---------------------------Отправитель Получатель

            /// <summary>
            /// Тип ордера при систмеме получатель - отправитель<para></para>
            ///  тип определения получателя и отправителя 1-признак PRIZNAK (1-отправитель;2-получатель). Поля, меняются местами согласно изменения признака (1-получатель:2-отправитель)!,<para></para>
            ///  поля COLUM_NAME (соответсвие названий полей), знак числа ZNAK_CHISLA (-;+)<para></para>
            /// </summary>
            public string Type_OTPR_POLU { get; set; }

            /// <summary>
            /// Тип ордера при систмеме получатель - отправитель<para></para>
            /// опции 1-признак PRIZNAK (1-отправленный платеж;2-полученный платеж) => название или номер поля, признака/ поля COLUM_NAME (соответсвие названий полей) => пусто/ знак числа ZNAK_CHISLA (-;+) => можно изменить 
            /// </summary>
            public string Opcii_OTPR_POLU { get; set; }

            

            /// <summary>
            /// организация которая платит (может быть и наша)
            /// </summary>
            public string CODE_OTPROV { get; set; }
            /// <summary>
            /// расчетный  счет данной организции плательщика (может быть и наша)
            /// </summary>
            public string RR_OTPROV { get; set; }

            /// <summary>
            /// код банке где открыт расчетный счет организации плательщик (может быть и наша)
            /// </summary>
            public string CODE_BANK_OTPROV { get; set; }

            /// <summary>
            /// организация получатель денег (может быть и наша)
            /// </summary>
            public string CODE_POLUCH { get; set; }
            /// <summary>
            /// расчетный счет организации получателя (может быть и наша)
            /// </summary>
            public string RR_POLUCH { get; set; }

            /// <summary>
            /// код банке где открыт расчетный счет организации получателя (может быть и наша)
            /// </summary>
            public string CODE_BANK_POLUCH { get; set; }

            // ---------------------------
            
            /// <summary>
            /// сумма банковской выписки (может быть и наша)
            /// </summary>
            public double summa { get; set; }
            /// <summary>
            /// валюта (по умолчанию нужно ставить 1 это гривня, может быть другая)
            /// </summary>
            public double curr { get; set; }
            /// <summary>
            /// Назначение платежа 
            /// </summary>
            public string prim { get; set; }
            /// <summary>
            /// тут рекомендую писать служебый комментарий
            /// </summary>
            public string dop_info { get; set; }


            

            /// <summary>
            /// статья по которой проводится проводка (разработка новой версии импорта)
            /// </summary>
            public string tp_in_out { get; set; }
        }

        #endregion


        public СExport()
        {
            InitializeComponent();

            ResolutionDisplay();



            #region Заполнение данными
            DateTime datePo_ = DateTime.Today;
            

            dateS.SelectedDate = new DateTime(datePo_.Year, datePo_.Month, 1).AddMonths(-1);

            datePo.SelectedDate = datePo_; //new DateTime(datePo_.Year, datePo_.Month, datePo_.Day);

            // ==== проверка пути и заполнение данными форм
            if (AppPlayStory_1C.UserconfigWorkSpace["Export_PutchDefault"].Length == 0)
            {
                AppPlayStory_1C.UserconfigWorkSpace["Export_PutchDefault"] = SystemConecto.PStartup + @"data\export";
            }
            PutchDir_.Text = AppPlayStory_1C.UserconfigWorkSpace["Export_PutchDefault"];
            PutchDir_.TextChanged +=PutchDir__TextChanged;
            


            // ======== Импорт
            if (AppPlayStory_1C.UserconfigWorkSpace["Import_PutchBankDefault"].Length == 0)
            {
                AppPlayStory_1C.UserconfigWorkSpace["Import_PutchBankDefault"] = SystemConecto.PStartup + @"data\import";
            }
            PutchDir_ImBank.Text = AppPlayStory_1C.UserconfigWorkSpace["Import_PutchBankDefault"];
            PutchDir_ImBank.TextChanged += PutchDir_ImBank_TextChanged;

            
            
            #endregion

            #region Для СуперАдминистратора (пока для админа БД)
            if (SystemConecto.AutorizUser.LoginUserAutoriz.ToLower() == "sysdba" && App.aSystemVirable["UserWindowIdentity"] == "1")
            {

            }
            else
            {
                AdminExport.Visibility = Visibility.Collapsed;
                Opcii.Visibility = Visibility.Collapsed;
                Opcii_Bank.Visibility = Visibility.Collapsed;
                Opcii_CinaWEB.Visibility = Visibility.Collapsed;
            }

            #endregion


        }

        #region Разрешение экрана

        public void ResolutionDisplay()
        {
            // 3. Установка размеров окна для того чтобы игнорировать нажатие клавиши раскрыть на весь экран
            // Рабочего стола ([0] - Top; [1] - Left; [2] - Width; [3] - Height; [4] - Right;


            // 3. Установка размеров окна для того чтобы игнорировать нажатие клавиши раскрыть на весь экран
            // Рабочего стола ([0] - Top; [1] - Left; [2] - Width; [3] - Height; [4] - Right;
            // Размещение окна
            this.Top = SystemConecto.WorkAreaDisplayDefault[0] + 10;
            this.Left = SystemConecto.WorkAreaDisplayDefault[1] + 10;
            // Размер окна и Grid; Grid.Height - растягивает this.Okno.Height
            this.WinGrid.Height = this.Height = SystemConecto.WorkAreaDisplayDefault[3]- 35;
            WinGridRowCenter.Height = new GridLength(this.WinGrid.Height - 100);
            this.WinGrid.Width = this.Width = SystemConecto.WorkAreaDisplayDefault[2] - (277 + 5 + 10);


            // Тело окна v 1.2 Растягивается
            // Okno и TabOkno- Авто


            // Вікна які знаходятся у цьому вікні
            //WiknoConecto.Height = this.Okno.Height;

            // Отцентрировать сообщение
            //Message1.Margin = new Thickness(0, 0, 0, 0);



        }
        #endregion

        #region Изображения как клавиши - Визуализация интерфейса    { События Click (Клик) функцилнальных клавиш рабочего стола }
        private void Close__MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Close_();
        }
        #endregion 

        #region Клавиша выхода из окна
        private void ImButExit_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            var uriSource = new Uri(@"/Conecto®%20WorkSpace;component/Images/vihod1.png", UriKind.Relative);
            ImButExit.Source = new BitmapImage(uriSource);
            Close_PanelSys(2);
        }

        private void ImButExit_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var uriSource = new Uri(@"/Conecto®%20WorkSpace;component/Images/vihod2.png", UriKind.Relative);
            ImButExit.Source = new BitmapImage(uriSource);
        }

        private void ImButExit_MouseLeave(object sender, MouseEventArgs e)
        {
            var uriSource = new Uri(@"/Conecto®%20WorkSpace;component/Images/vihod1.png", UriKind.Relative);
            ImButExit.Source = new BitmapImage(uriSource);
        }


        #region События Click (Клик) функцилнальных клавиш окна
        private void Close_PanelSys(int TypeClose = 1)
        {

            if (TypeClose == 2)
            {
                // Закрыть авторизацию
                SystemConectoInterfice.UserInterficeClose();
            }

            Close_();

        }
        #endregion

        #endregion

        #region Выход из окна
        private void Close_()
        {
            // Очистить данные записанные в память
            AppPlayStory_1C.aParamAppUndo = new Dictionary<string, string>();
            AppPlayStory_1C.UserconfigWorkSpace = new Dictionary<string, string>();

            this.Visibility = Visibility.Hidden;

            this.Owner.Focus();
            this.Owner = null;

            this.Close();


        }
        #endregion


        #region Обработка событий любой клавиатуры



        private void ConectoW_KeyDown(object sender, KeyEventArgs e)
        {
            // Да завершить
            if (e.Key == Key.Return)
            {
               

            }
            else
            {

                // Нет отказаться
                if (e.Key == Key.Escape)
                {
                    //Close_();

                }

            }
            

           // Отладка
           // MessageBox.Show(e.Key.ToString());

        }
        #endregion


        #region Путь к файлам експорта


        /// <summary>
        /// Выбор директории где расположен фронт не по умолчанию
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Dir_Front_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

            // Configure open file dialog box  link PresentetionFramework
            //Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();

            var dlg = new System.Windows.Forms.FolderBrowserDialog();

            dlg.Description = "Путь к файлам";

            //dlg.FileName = "B52FrontOffice"; // Default file name
            //dlg.DefaultExt = ".exe"; // Default file extension
            //dlg.Filter = "Файл приложения |*.exe"; // Filter files by extension
            //dlg.Title = "Путь к основному фронту";


            // Show open file dialog box
            //Nullable<bool> result = dlg.ShowDialog();
             System.Windows.Forms.DialogResult result = dlg.ShowDialog();

            // Process open file dialog box results
            // Улучшить проверку
             if (result == System.Windows.Forms.DialogResult.OK)
            {
                //Чтение параметра и запись нового значения
                var ItemTextBoxPatch = (TextBox)LogicalTreeHelper.FindLogicalNode(this, "PutchDir_");
                ItemTextBoxPatch.Text = dlg.SelectedPath; //dlg.FileName;
                Update_Putch(ref sender, "PutchDir_", "Export_PutchDefault", 1);

            }

        }



        /// <summary>
        /// Изменение параметра директории где расположен фронт не по умолчанию
        /// </summary>
        /// <param name="TypeValue">Тип значения 0 - ввод руками</param>
        private void Update_Putch(ref object sender, string NameElement, string NameConfig, int TypeValue = 0)
        {
            // MessageBox.Show("Парам");
            // Имя параметра что меняется
            TextBox ItemTextBoxPatch = null;
            if (sender is TextBox)
            {
                ItemTextBoxPatch = (TextBox)sender;
            }
            else
            {
                ItemTextBoxPatch = (TextBox)LogicalTreeHelper.FindLogicalNode(this, NameElement); 
            }

            //Чтение параметра и запись нового значения
           
            // Запись предыдущего значения
            AppPlayStory_1C.UpdateParamUndo(NameConfig);
            // Активация отката (сначало красная елси диагностика положительная то зеленая)
            ((Label)LogicalTreeHelper.FindLogicalNode(this, NameElement + "Undo_patch")).Visibility = Visibility.Visible;
            //SystemConecto.aParamApp
            // Запись значения
            if (!AppPlayStory_1C.ControllerParam(ItemTextBoxPatch.Text, NameConfig, 11))
            {
                 //Поменять цвет иконки, а также вывести сообщение в окне диагностики (Разработка)
            }

            // Перезапись новго значения
            if (TypeValue > 0)
            {
                ItemTextBoxPatch.Text = AppPlayStory_1C.UserconfigWorkSpace[NameConfig];
            }
        }



        /// <summary>
        /// Начать вввод
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PutchDir__KeyDown(object sender, KeyEventArgs e)
        {

            KeyUp_TextBox(ref sender, ref e, "Export_PutchDefault");
        }

        //private void PutchDir__KeyUp(object sender, KeyEventArgs e)
        //{
        //    // Во время проверки обнаружения нажатия комбинаций клавиш было выявлено дефект в скорости обнаружения ОС, 
        //    // а также только одно событие KeyUp отлавливало на 50% лучше комбинацию
        //    //if ((Keyboard.Modifiers == ModifierKeys.Control) && (e.Key == Key.V))
        //    //{

        //    //    MessageBox.Show("Нажал!");

        //    //} 
            
            
        //    //if (Keyboard.IsKeyDown(Key.LeftCtrl) && (e.Key == Key.V))
        //    //{
        //    //    MessageBox.Show("Нажал!");
        //    //}
            
        //    //if ((Keyboard.Modifiers & ModifierKeys.Control) == ModifierKeys.Control && (e.Key == Key.V))
        //    //{
        //    //    MessageBox.Show("Нажал!");
        //    //}
        //}


        /// <summary>
        /// Комбинации клавиш для полей ввода 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void KeyUp_TextBox(ref object sender, ref KeyEventArgs e, string NameConfig)
        {
            TextBox ItemTextBoxPatch = null;
            if (sender is TextBox)
            {
                ItemTextBoxPatch = (TextBox)sender;
            }
            else
            {
                return;
            }

            // Клавиши редактора (Могут отсутствовать)
            Image ItemImageButtonDir = (Image)LogicalTreeHelper.FindLogicalNode(this, ItemTextBoxPatch.Name + "Brw_");
            Label ItemImageButtonEditDir = (Label)LogicalTreeHelper.FindLogicalNode(this, ItemTextBoxPatch.Name + "Edit_");

          
            switch (e.Key)
            {
                case Key.Escape:
                    //Чтение параметра и запись нового значения
                    //var ItemTextBoxPatch = (TextBox)LogicalTreeHelper.FindLogicalNode(this, NameTextBop);
                    ItemTextBoxPatch.Text = AppPlayStory_1C.UserconfigWorkSpace[NameConfig];
                    ItemImageButtonEditDir.Visibility = Visibility.Collapsed;
                    ItemImageButtonDir.Visibility = Visibility.Visible;
                    break;
                case Key.Return:
                    Update_Putch(ref sender, "PutchDir_", "Export_PutchDefault");
                     // Емитация нажатия Ентер
                     // Клавиши редактора (Могут отсутствовать)
                     ((Image)LogicalTreeHelper.FindLogicalNode(this, "PutchDir_Brw_")).Visibility = Visibility.Visible;
                     ((Label)LogicalTreeHelper.FindLogicalNode(this, "PutchDir_Edit_")).Visibility = Visibility.Collapsed;
                    break;

                //default:
                      
                  
                //   break;

            }


            return;


        }

        /// <summary>
        /// Изменение текста, метод обработки события устанавливается в коде во время создания объекта
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PutchDir__TextChanged(object sender, TextChangedEventArgs e)
        {
            // Пример обращения
            // Клавиши редактора (Могут отсутствовать)
            //((Image)LogicalTreeHelper.FindLogicalNode(this, "PutchDir_Brw_")).Visibility = Visibility.Hidden;
            //((Label)LogicalTreeHelper.FindLogicalNode(this, "PutchDir_Edit_")).Visibility = Visibility.Visible;
            PutchDir_Brw_.Visibility = Visibility.Hidden;
            PutchDir_Edit_.Visibility = Visibility.Visible;
        }

        /// <summary>
        /// Откат предыдущего значения Путь к фронту
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
         private void PutchDir_Undo_patch_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            // Имя параметра что откатывается
            var NameConfig = "Export_PutchDefault";

            //Чтение параметра и запись нового значения
            var ItemTextBoxPatch = (TextBox)LogicalTreeHelper.FindLogicalNode(this, "PutchDir_");
            // Пример динамических переменных
            //ItemTextBoxPatch.Text = AppPlayStory_1C.aParamAppUndo.ContainsKey(NameParam) ? AppPlayStory_1C.aParamAppUndo[NameParam] : "";

            ItemTextBoxPatch.Text = AppPlayStory_1C.aParamAppUndo[NameConfig];

            if (AppPlayStory_1C.ControllerParam(AppPlayStory_1C.UserconfigWorkSpace[NameConfig], NameConfig, 11))
             {
                //Поменять цвет иконки, а также вывести сообщение в окне диагностики (Разработка)
             }

            // Удалить из памяти
            AppPlayStory_1C.aParamAppUndo.Remove(NameConfig);
            ((Label)LogicalTreeHelper.FindLogicalNode(this, "PutchDir_Undo_patch")).Visibility = Visibility.Collapsed;
            ((Image)LogicalTreeHelper.FindLogicalNode(this, "PutchDir_Brw_")).Visibility = Visibility.Visible;
            ((Label)LogicalTreeHelper.FindLogicalNode(this, "PutchDir_Edit_")).Visibility = Visibility.Collapsed;
        }

        /// <summary>
        /// Сохранить измения
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
         private void PutchDir_Edit__MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
         {
             Update_Putch(ref sender, "PutchDir_", "Export_PutchDefault");
             // Емитация нажатия Ентер
             // Клавиши редактора (Могут отсутствовать)
             ((Label)LogicalTreeHelper.FindLogicalNode(this, "PutchDir_Undo_patch")).Visibility = Visibility.Collapsed;
             ((Image)LogicalTreeHelper.FindLogicalNode(this, "PutchDir_Brw_")).Visibility = Visibility.Visible;
             ((Label)LogicalTreeHelper.FindLogicalNode(this, "PutchDir_Edit_")).Visibility = Visibility.Collapsed;

         }

        #endregion


        #region Статус потоков StatusBarCode

         /// <summary>
         /// Отслеживание выполнения потоков locker[0] -количество зарегистрировавшихся потоков,<para></para>
         /// locker[1] - количество потоков кторые завершили выполнятся
         /// </summary>
         static int[] lockerEx { get; set; }
         /// <summary>
         /// Статутс потока в процентом соотношении
         /// </summary>
         static List<int> lockerStatEx { get; set; }
         /// <summary>
         /// Сервисный комментарий потока
         /// </summary>
         static List<int> lockerCommentEx { get; set; }
         /// <summary>
         /// MinWidth - Длина под текст для статуса бара.
         /// </summary>
         static double RezervContentStatusBarEx = 180;
         #endregion


        #region Экспорт данных в 1С с помощью файла

         // Экспортировать
         private void Ecxport_Click(object sender, RoutedEventArgs e)
         {


             #region Статус Выполнения потоков StatusBarCode

             // Защита от повторного нажатия
             if (Ecxport.Cursor == Cursors.Wait)
                 return;

             // Клавиша в ожидании
             Ecxport.Cursor = Cursors.Wait;

             // Включение отслеживание потоков
             lockerEx = new int[] { 0, -1 };
             if (lockerStatEx != null && lockerStatEx.Count > 0)
                 lockerStatEx.Clear();

             if (lockerCommentEx != null && lockerCommentEx.Count > 0)
                 lockerCommentEx.Clear();

             #region Полоска выполнения

             // Вся длина кода border - mergin ImportBank_ProccessBack
             double MaxEnd = this.Width - 100 - 365;
             // Стартовая позиция
             Ecxport_Proccess.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
             Ecxport_Proccess.Width = RezervContentStatusBar;
             Ecxport_Proccess_Label.Content = "Процесс выполенения";
             Ecxport_Proccess.Visibility = Visibility.Visible;



             #endregion

             #endregion



             // idBlock - выполненный блок

             // Даты

             DateTime dateSs = dateS.SelectedDate.Value;

             string DateS_SqlFB = string.Format("{0}.{1}.{2}", dateSs.Day, dateSs.Month, dateSs.Year);

             DateTime datePos = datePo.SelectedDate.Value;

             string DatePO_SqlFB = string.Format("{0}.{1}.{2}", datePos.Day, datePos.Month, datePos.Year);

             RenderInfo PrSendThread = new RenderInfo();

             PrSendThread.dateSs = DateS_SqlFB;

             PrSendThread.datePos = DatePO_SqlFB;

             PrSendThread.TypeFile = TypeFile.Content.ToString();
             PrSendThread.PutchDir_ = PutchDir_.Text;
             PrSendThread.idBlock = 1;
             PrSendThread.WinOnStart = this;

             PrSendThread.LoginUserAutoriz = SystemConecto.AutorizUser.LoginUserAutoriz;
             PrSendThread.PaswdUserAutoriz = SystemConecto.AutorizUser.PaswdUserAutoriz; // SystemConecto.aParamApp["Autorize_Admin_Paswd"]; //"alttab79";
           
             PrSendThread.BDSERVER_IP = AppPlayStory_1C.UserconfigWorkSpace["BDSERVER_IP"];
             PrSendThread.BDSERVER_Port = AppPlayStory_1C.UserconfigWorkSpace["BDSERVER_Port"];
             PrSendThread.BDSERVER_Alias = AppPlayStory_1C.UserconfigWorkSpace["BDSERVER_Alias"];
             PrSendThread.BDSERVER_Putch_Hide = AppPlayStory_1C.UserconfigWorkSpace["BDSERVER_Putch-Hide"];

             //Grid MessageTextInner = new message().WaitMessageShow("Формирование документов:" + Environment.NewLine
             //    + " Акты реализации.", this, 3, "TextInfo");

             //Grid.SetRow(MessageTextInner, 1);
             //Grid.SetColumn(MessageTextInner, 3);

             //MessageTextInner.Name = "TextInfo";

             //WinGrid.Children.Add(MessageTextInner);

             Thread thStartWEB = new Thread(Ecxport_Click_Block);
             thStartWEB.SetApartmentState(ApartmentState.STA);
             thStartWEB.IsBackground = true; // Фоновый поток
             thStartWEB.Start(PrSendThread);

             Thread thStartWEB_ = new Thread(ReadPotokWork);
             thStartWEB_.SetApartmentState(ApartmentState.STA);
             thStartWEB_.IsBackground = true; // Фоновый поток
             thStartWEB_.Start(PrSendThread);

         }

        /// <summary>
        ///Статус Выполнения потоков
        /// </summary>
        /// <param name="PrSendThread"></param>
         private void ReadPotokWork(object PrSendThread)
         {

             RenderInfo ParametrsBlock = (RenderInfo)PrSendThread;

             #region Статус Выполнения потоков StatusBarCode

             // Контроль за потоками и их завершением работы
             // завершение потоков - отслеживания выполнения всех потоков (что делать если хотябы один завис, идея рестарта и остановки с помощью оператора)
             // Отследить выполнение потоков (пробую заменить while for -ом)
             // int Write = 0;
             for (int iEnd = 0; iEnd < 2; iEnd++)
             {
                 if (lockerEx[0] == lockerEx[1])
                 {
                     // Потоки завершины
                     iEnd = 2;

                     //SystemConecto.ErorDebag("Закінчили роботу " + locker_a[1].ToString() + " потоків із " + locker_a[0] + ".", 1);

                     // Изменить интерфейс
                     System.Windows.Application.Current.Dispatcher.Invoke(new Action(delegate()
                     {
                         // var Window_ = SystemConecto.ListWindowMain("SpecPred_");

                         #region Полоска выполнения

                         // Клавиша в курсор
                         Ecxport.Cursor = Cursors.Arrow;

                         // Стартовая позиция
                         Ecxport_Proccess.HorizontalAlignment = System.Windows.HorizontalAlignment.Stretch;

                         Ecxport_Proccess_Label.Content = "Экспорт данных выполнен";

                         // Сохраненная длина
                         // ImportBank_Proccess.Width = ParametrsBlock.SizeWidthLineStatusBar;
                         Ecxport_Proccess.Width = double.NaN;


                         #endregion

                     }));

                 }
                 else
                 {

                     // Защита от первого срабатывания, означает выполнить проверку до того как выполнены все потоки (locker[1] = -1).
                     if (lockerEx[1] == -1)
                     {
                         Interlocked.Increment(ref lockerEx[1]);
                     }
                     Thread.Sleep(150);

                     // Изменить интерфейс
                     System.Windows.Application.Current.Dispatcher.Invoke(new Action(delegate()
                     {

                         #region Полоска выполнения

                         // длина
                         if (lockerEx[0] > 0)
                         {
                             double CountOneProssec = (ParametrsBlock.SizeWidthLineStatusBar - RezervContentStatusBarEx) / lockerEx[0];

                             Ecxport_Proccess.Width = RezervContentStatusBarEx + CountOneProssec * lockerEx[1];
                         }
                         #endregion

                     }));


                     // === Отладка
                     //Write++;
                     //if (Write == 20)
                     //{
                     //    Write = 0;



                     //    //SystemConecto.ErorDebag("Закінчили роботу " + locker[1].ToString() + " потоків із " + locker[0]+".", 1);
                     //}
                     //========
                     // Продолжить
                     iEnd = 0;
                 }


             }



             #endregion

         }


         /// <summary>
         /// Путь пользовательских данных
         /// </summary>
         private static string PuthUser = SystemConecto.PutchApp + @"config\user\";


         // Экспортировать
         private void Ecxport_Click_Block(object PrSendThread)
         {


             #region Статус Выполнения потоков StatusBarCode
             // Поток запущен
             Interlocked.Increment(ref lockerEx[0]);

             #region Статус Выполнения потоков StatusBarCode
             // Поток завершен - перед каждым return и в конце кода
             // Interlocked.Increment(ref lockerEx[1]);
             #endregion
             // Потоко не безопасная конструкция(догадка, переменная одна для всех потоков ее нужно извлечь из памяти произвести увеличение и записать в память)
             // lockerEx[1] = lockerEx[1] + 1;

             #endregion


             RenderInfo ri = (RenderInfo)PrSendThread;

             int idBlock = ri.idBlock;

             string DateS_SqlFB = ri.dateSs;
             string DatePO_SqlFB = ri.datePos;
             string PutchDir_ = ri.PutchDir_;
             string TypeFile = ri.TypeFile;
             //dateS
                 //datePo
             // Емуляция
                 //podr_
                     //organiz_уу




             try
             {

                 // под паролем администратора проверка соединения


                 DBConecto.ParamStringServerFB[1] = ri.LoginUserAutoriz;
                 DBConecto.ParamStringServerFB[2] = ri.PaswdUserAutoriz; // SystemConecto.aParamApp["Autorize_Admin_Paswd"]; //"alttab79";

                 DBConecto.ParamStringServerFB[3] = ri.BDSERVER_IP;
                 DBConecto.ParamStringServerFB[4] = ri.BDSERVER_Alias;
                 DBConecto.ParamStringServerFB[5] = ri.BDSERVER_Putch_Hide;
                 DBConecto.ParamStringServerFB[7] = ri.BDSERVER_Port;


                var Test = DBConecto.StringServerFB();
                 // Опыт
                 using (FirebirdSql.Data.FirebirdClient.FbConnection bdFbDefConect = new FirebirdSql.Data.FirebirdClient.FbConnection(Test))
                 {
                     bdFbDefConect.Open();

                    


                     //4. create a new DataSet
                     DataSet myDataSet = new DataSet();


                     #region промежуточные данные
                     // ====================================================================
//"and (hd.dat between '01.04.2013' and '30.04.2013')"+

//                     "select hd.dat,hd.num," +
//"(select v.podr from MN_HD_TOV_VED v where rc.day_rec=v.kod) as podr ," +
//"(select name from MN_HD_TOV_VED v, spr_podr p where p.kod=v.PODR and rc.day_rec=v.kod) as podr_name ," +
//"(select p.org from spr_podr p, MN_HD_TOV_VED v where rc.day_rec=v.kod and p.kod=v.podr) as org_our," +
//"(select o.name from spr_podr p, MN_HD_TOV_VED v, spr_org o where o.kod=p.org and rc.day_rec=v.kod and p.kod=v.podr) as org_our_name," +
//"rc.TOV, (select name from spr_tov where kod=tov) as tov_name," +
//"rc.DOP_CNT as cnt, rc.DOP_PRICE as priceNds, --round(rc.DOP_PRICE/(1+20/100),5) as priceNoNds, rc.SUMMA as summaNds, 20," +
//"hd.ORG, (select name from spr_org where hd.org=kod) as org_name," +
//"hd.type_doc,  (case hd.TYPE_DOC  when 1 then 'Реализация'  when 2 then 'Отгрузка'  else 'Чек' end) as type_doc_name," +
//"--round(rc.SUMMA/(1+20/100),2) as summaNoNds," +
//"(case rc.ED_NAME    when 'кг' then 1   when 'л' then 2   when 'порц' then 3   when 'гр' then 4   when 'мл' then 6   else 'шт' end) edId,rc.ED_NAME," +
//" type_slip,   (case type_slip   when 2 then 'Безналичная'   when 3 then 'Клубная система'   when 4 then 'Кредитная карта'   else 'Наличная'   end ) as type_slip_name" +
//" from mn_hd_tov_out hd, mn_rc_tov_out rc" +
//"where rc.doc=hd.kod and" +
//" (CHECKKODENTRY(hd.TYPE_DOC, '1;2;6;')>0)" +
//"and hd.DAY_DOC is null" +
//"and (hd.dat between '{0}' and '{1}') " +
//"and  (hd.TYPE_SLIP=2 or (coalesce(rc.TAX_GROUP, '')<>''" +
//"and (hd.Cash_Print_Cnt > 0)))" +
//"and ('{2}'='0' or CHECKKODENTRY(hd.podr,{2})>0)" +
//"and ('{3}'='0' or (EXISTS(select p.kod from MN_HD_TOV_VED v, spr_podr p where p.kod=v.PODR and rc.day_rec=v.kod and p.org='{3}') ))" +
                     //"order by hd.dat,hd.num,rc.tov" +
                     #endregion

                     #region Альтернативный глобальный пример
                     // declare command
                     // ========================= Альтернативный глобальный пример
//                    FirebirdSql.Data.FirebirdClient.FbCommand readCommand = 
//                                new FirebirdSql.Data.FirebirdClient.FbCommand(string.Format("select hd.dat, hd.num, hd.podr, "+
//"rc.DOP_CNT as cnt, rc.DOP_PRICE as priceNds, " +
//"rc.SUMMA as summaNds, hd.nds, " +
//"hd.ORG " +
//"from mn_hd_tov_out hd, mn_rc_tov_out rc " +
//"where rc.doc=hd.kod " +
//"and hd.white=1 " +
//"order by hd.dat,hd.num,rc.tov " +

//                         ""), bdFbDefConect);
//                    FirebirdSql.Data.FirebirdClient.FbDataReader myreader=  readCommand.ExecuteReader();
//                    while(myreader.Read())
//                    {
//                        // load the combobox with the names of the people inside. myreader[0] reads from the 1st Column
//                        //DeleteComboBox.Items.Add(myreader[0]);
                     //                    }

                     #endregion

                     string SelectFD = "";

                     //string[] TitleCSV = null;

                     // Тип доступа по подразделениям
                     var TypeConfigAutorizeSDR = AppPlayStory_1C.TypeAutorizeSDR(AppPlayStory_1C.UserconfigWorkSpace["TypeAutorizeSDR"]);
                     var TypeConfigAutorizeSPORG = AppPlayStory_1C.TypeAutorizeSPORG(AppPlayStory_1C.UserconfigWorkSpace["TypeAutorizeSPORG"]);
                     var TypeConfigAutorizeSDRCach = AppPlayStory_1C.TypeAutorizeSDRCach(AppPlayStory_1C.UserconfigWorkSpace["TypeAutorizeSDRCach"]);

                     // Конфигурация ресторана
                     if (TypeConfigAutorizeSDR.Restoran)
                     {
                         if (idBlock == 1)
                         {
                             #region Акты реализации

                             
                             string[] ReadSql = SystemConecto.ReadFile_SqlPass(SystemConecto.PutchApp + @"config\user\import_act_realiz.sql.fb.txt");

                             // Чтение в текст максимальная скорость (избежал чтения и конвертацию из bytes[])
                             string decryptedtext = Convert.ToInt32(ReadSql[0]) == 0 ? Properties.Resources.import_act_realiz_sql_fb : ReadSql[1];

                             // StringReader sReader = new StringReader(decryptedtext);

                             //// Чтение только до первого символа начало строки
                             //string decryptedtext =  sReader.ReadToEnd();

                             // Новый запрос (использовать replace для кода посчитал лишним по производительности)

                             SelectFD = string.Format(decryptedtext, DateS_SqlFB, DatePO_SqlFB, TypeConfigAutorizeSDR.RestoranSDR, TypeConfigAutorizeSPORG.RestoranSDR, TypeConfigAutorizeSDRCach.RestoranSDR);

                             // Запрос
                             string SelectFD_ = string.Format("select formatdate(hd.dat,'dd.mm.yyyy') as dat, hd.num, " +  //hd.dat
                            "(select v.podr from MN_HD_TOV_VED v where rc.day_rec=v.kod) as podr , " +
                            "(select name from MN_HD_TOV_VED v, spr_podr p where p.kod=v.PODR and rc.day_rec=v.kod) as podr_name , " +
                            "(select p.org from spr_podr p, MN_HD_TOV_VED v where rc.day_rec=v.kod and p.kod=v.podr) as org_our, " +
                            "(select o.name from spr_podr p, MN_HD_TOV_VED v, spr_org o where o.kod=p.org and rc.day_rec=v.kod and p.kod=v.podr) as org_our_name, " +
                            "rc.TOV, (select name from spr_tov where kod=tov) as tov_name, " +
                            " rc.DOP_CNT as cnt, rc.DOP_PRICE as priceNds, " + //--round(rc.DOP_PRICE/(1+20/100),5) as priceNoNds, " +
                            " rc.SUMMA as summaNds, hd.nds, " +
                            " hd.ORG, (select name from spr_org where hd.org=kod) as org_name, " +
                            " hd.type_doc,  (case hd.TYPE_DOC  when 1 then 'Реализация'  when 2 then 'Отгрузка'  else 'Чек' end) as type_doc_name, " +
                                 //"--round(rc.SUMMA/(1+20/100),2) as summaNoNds, " +
                            "(case rc.ED_NAME    when 'кг' then 1   when 'л' then 2   when 'порц' then 3   when 'гр' then 4   when 'мл' then 6   else 'шт' end) edId,rc.ED_NAME, " +
                            " type_slip,   (case type_slip   when 2 then 'Безналичная'   when 3 then 'Клубная система'   when 4 then 'Кредитная карта'   else 'Наличная'   end ) as type_slip_name " +
                            " from mn_hd_tov_out hd, mn_rc_tov_out rc " +
                            "where rc.doc=hd.kod and " +
                            " (CHECKKODENTRY(hd.TYPE_DOC, '1;2;6;')>0) " +
                            "and hd.DAY_DOC is null " +
                            "and (hd.dat between '{0}' and '{1}')  " +
                            "and  (hd.TYPE_SLIP=2 or (coalesce(rc.TAX_GROUP, '')<>'' " +
                            "and (hd.Cash_Print_Cnt > 0))) " +
                            "and ('{2}'='0' or CHECKKODENTRY(hd.podr,'{2}')>0) " +
                            "and ('{3}'='0' or (EXISTS(select p.kod from MN_HD_TOV_VED v, spr_podr p where p.kod=v.PODR and rc.day_rec=v.kod and p.org='{3}') )) " +
                            "order by hd.dat,hd.num,rc.tov " +

                             "", DateS_SqlFB, DatePO_SqlFB, "0", "0");

                             //SystemConecto.ErorDebag(SelectFD, 1);


                             //5. Declare an Adapter to interface with our Table in Firebird

                             FirebirdSql.Data.FirebirdClient.FbDataAdapter myDataAdapter =
                                 new FirebirdSql.Data.FirebirdClient.FbDataAdapter(SelectFD, bdFbDefConect); //:dbeg - {0} :dend - {1} :podr - {2} :org - {3}
                             //DateS_SqlFB, DatePO_SqlFB, "0", "0"

                             //6. Fill the Dataset
                             myDataAdapter.Fill(myDataSet, "AktRealiz");



                             // проверить и создать путь
                             SystemConecto.DIR_(PutchDir_);

                             //TitleCSV = new string[20]{"Дата","Номер","Склад(код)","Склад (наименование)","Наша внутренняя организация (код)", "Наша внутренняя организация (наименование)",
                             //       "Номенклатура (код)", "Номенклатура (наименование)", "Количество", "Цена продажи с НДС", "Сумма продажи с НДС", "Ставка НДС с продаж",
                             //       "Организация (код)", "Организация (наименование)", "Тип списания", "Тип списания (название)", "Единица измерения (код)", 
                             //       "Единица измерения (название)", "Тип оплаты", "Тип оплаты (название)"};

                             ConectoFileXML.ConnectFXML OFile = new ConectoFileXML.ConnectFXML("namecolumn_act_realiz.fb.xml", PuthUser);
                             DataTable NameColumn = OFile.XMLNColumntoDataTable();

                            // Акты реализации
                            myDataSet.Tables["AktRealiz"].WriteToFile(PutchDir_ + @"\Act realizacii.csv", NameColumn, TypeFile);
                             #endregion

                             idBlock++;
                         }

                         if (idBlock == 2)
                         {
                             #region Акты списания ручные и автоматические





                             // Чтение в текст максимальная скорость (избежал чтения и конвертацию из bytes[])
                             string[] ReadSql = SystemConecto.ReadFile_SqlPass(SystemConecto.PutchApp + @"config\user\import_act_spis.sql.fb.txt");

                             // Чтение в текст максимальная скорость (избежал чтения и конвертацию из bytes[])
                             string decryptedtext = Convert.ToInt32(ReadSql[0]) == 0 ? Properties.Resources.import_act_spis_sql_fb : ReadSql[1];
                             
                             //string decryptedtext = Properties.Resources.import_act_spis_sql_fb;
                             // StringReader sReader = new StringReader(decryptedtext);

                             //// Чтение только до первого символа начало строки
                             //string decryptedtext =  sReader.ReadToEnd();

                             // Новый запрос (использовать replace для кода посчитал лишним по производительности)
                             SelectFD = string.Format(decryptedtext, DateS_SqlFB, DatePO_SqlFB, TypeConfigAutorizeSDR.RestoranSDR, TypeConfigAutorizeSPORG.RestoranSDR);


                             // Запрос
                             string SelectFD_ = string.Format("select formatdate(hd.dat,'dd.mm.yyyy') as dat ,hd.num, hd.podr , " + //hd.dat
                                "(select name from  spr_podr p where p.kod=hd.PODR ) as podr_name , " +
                                "(select p.org from spr_podr p where  p.kod=hd.podr) as org_our, " +
                                "(select o.name from spr_podr p, spr_org o where o.kod=p.org and p.kod=hd.podr) as org_our_name, " +
                                "rc.TOV, (select name from spr_tov where kod=tov) as tov_name, " +
                                " rc.DOP_CNT as cnt, rc.DOP_PRICE as priceNds,  " + //--round(rc.DOP_PRICE/(1+20/100),5) as priceNoNds, " +
                                "rc.SUMMA as summaNds, hd.nds, " +
                                 "hd.ORG, (select name from spr_org where hd.org=kod) as org_name, " +
                                 "hd.type_doc,  (case hd.TYPE_DOC  when 8 then 'Не проеденное списание' else 'Акт списание' end) as type_doc_name, " +
                                 //"--round(rc.SUMMA/(1+20/100),2) as summaNoNds, " +
                                "(case rc.ED_NAME    when 'кг' then 1   when 'л' then 2   when 'порц' then 3   when 'гр' then 4   when 'мл' then 6   else 'шт' end) edId,rc.ED_NAME, " +
                                "COALESCE(hd.day_doc,0) as day_doc, (case  COALESCE(hd.day_doc,0) when -1 then 'Автосписание' else 'Списание' end) as day_doc_name " +
                                 "from mn_hd_tov_out hd, mn_rc_tov_out rc " +
                                "where rc.doc=hd.kod and " +
                                 "(CHECKKODENTRY(hd.TYPE_DOC, '3;8;')>0) " +
                                "and hd.white=1 " +
                                "and (hd.dat between '{0}' and '{1}') " +
                                "and ('{2}'='0' or CHECKKODENTRY(hd.podr,'{2}')>0) " +
                                "and ('{3}'='0' or (EXISTS(select p.kod from  spr_podr p where p.kod=hd.PODR  and p.org='{3}') )) " +
                                "order by hd.dat,hd.num,rc.tov " +

                                 "", DateS_SqlFB, DatePO_SqlFB, "0", "0");

                             //SystemConecto.ErorDebag(SelectFD, 1);


                             //5. Declare an Adapter to interface with our Table in Firebird

                             FirebirdSql.Data.FirebirdClient.FbDataAdapter myDataAdapter_2 =
                                  new FirebirdSql.Data.FirebirdClient.FbDataAdapter(SelectFD, bdFbDefConect); //:dbeg - {0} :dend - {1} :podr - {2} :org - {3}
                             //DateS_SqlFB, DatePO_SqlFB, "0", "0"

                             //6. Fill the Dataset
                             myDataAdapter_2.Fill(myDataSet, "AktSpis");



                             // проверить и создать путь
                             SystemConecto.DIR_(PutchDir_);

                             //TitleCSV = new string[20]{"Дата","Номер","Склад(код)","Склад (наименование)","Наша внутренняя организация (код)", "Наша внутренняя организация (наименование)",
                             //           "Номенклатура (код)", "Номенклатура (наименование)", "Количество", "Цена себестоимости списания с НДС", "Сумма себестоимости списания с НДС", "Ставка НДС с продаж",
                             //           "Организация (код)", "Организация (наименование)", "Тип списания", "Тип списания (название)", "Единица измерения (код)", 
                             //           "Единица измерения (название)", "Вид списания", "Вид списания (название)"};

                             //SystemConecto.ErorDebag(myDataSet.Tables.Count.ToString(), 2);
                             ConectoFileXML.ConnectFXML OFile = new ConectoFileXML.ConnectFXML("namecolumn_act_spis.fb.xml", PuthUser);
                             DataTable NameColumn = OFile.XMLNColumntoDataTable();
                            //The given path's format is not supported. Акты списания
                            
                            myDataSet.Tables["AktSpis"].WriteToFile(PutchDir_ + @"\Act spisanie.csv", NameColumn, TypeFile);
                             #endregion

                             idBlock++;
                         }
                        
                         if (idBlock == 3)
                         {
                             #region Приходные накладные


                             // Чтение в текст максимальная скорость (избежал чтения и конвертацию из bytes[])
                             string[] ReadSql = SystemConecto.ReadFile_SqlPass(SystemConecto.PutchApp + @"config\user\import_ptsk.sql.fb.txt");

                             // Чтение в текст максимальная скорость (избежал чтения и конвертацию из bytes[])
                             string decryptedtext = Convert.ToInt32(ReadSql[0]) == 0 ? Properties.Resources.import_ptsk_sql_fb : ReadSql[1];

                             // StringReader sReader = new StringReader(decryptedtext);

                             //// Чтение только до первого символа начало строки
                             //string decryptedtext =  sReader.ReadToEnd();

                             // Новый запрос (использовать replace для кода посчитал лишним по производительности)
                             SelectFD = string.Format(decryptedtext, DateS_SqlFB, DatePO_SqlFB, TypeConfigAutorizeSDR.RestoranSDR, TypeConfigAutorizeSPORG.RestoranSDR);


                             // Запрос
                             string SelectFD_ = string.Format("select formatdate(hd.dat,'dd.mm.yyyy') as dat,hd.num,  hd.podr , " + //hd.dat
                                "(select name from  spr_podr p where p.kod=hd.PODR ) as podr_name , " +
                                "(select p.org from spr_podr p where  p.kod=hd.podr) as org_our, " +
                                "(select o.name from spr_podr p, spr_org o where o.kod=p.org and p.kod=hd.podr) as org_our_name, " +
                                "rc.TOV, (select name from spr_tov where kod=tov) as tov_name, " +
                                "rc.DOP_CNT as cnt, rc.DOP_PRICE as priceNds, " + // --round(rc.DOP_PRICE/(1+20/100),5) as priceNoNds,  " +
                                "rc.SUMMA as summaNds, hd.nds, " +
                                "hd.post, (select name from spr_org where hd.post=kod) as org_name, " +
                                 //"--round(rc.SUMMA/(1+20/100),2) as summaNoNds, " +
                                "(case rc.ED_NAME    when 'кг' then 1   when 'л' then 2   when 'порц' then 3   when 'гр' then 4   when 'мл' then 6   else 'шт' end) edId,rc.ED_NAME,hd.NUM_POST " +
                                 "from mn_hd_tov_in hd, mn_rc_tov_in rc " +
                                "where rc.doc=hd.kod and hd.white=1and (hd.dat between '{0}' and '{1}') and hd.TYPE_DOC=1 " +
                                "and ('{2}'='0' or CHECKKODENTRY(hd.podr,'{2}')>0) " +
                                "and ('{3}'='0' or (EXISTS(select p.kod from  spr_podr p where p.kod=hd.PODR  and p.org='{3}') ))  " +
                                "order by hd.dat,hd.num,rc.tov " +

                                 "", DateS_SqlFB, DatePO_SqlFB, "0", "0");

                             //SystemConecto.ErorDebag(SelectFD, 1);


                             //5. Declare an Adapter to interface with our Table in Firebird

                             FirebirdSql.Data.FirebirdClient.FbDataAdapter myDataAdapter_3 =
                                  new FirebirdSql.Data.FirebirdClient.FbDataAdapter(SelectFD, bdFbDefConect); //:dbeg - {0} :dend - {1} :podr - {2} :org - {3}
                             //DateS_SqlFB, DatePO_SqlFB, "0", "0"

                             //6. Fill the Dataset
                             myDataAdapter_3.Fill(myDataSet, "Ptskb52");



                             // проверить и создать путь
                             SystemConecto.DIR_(PutchDir_);

                             //TitleCSV = new string[17]{"Дата","Номер","Склад(код)","Склад (наименование)","Наша внутренняя организация (код)", "Наша внутренняя организация (наименование)",
                             //           "Номенклатура (код)", "Номенклатура (наименование)", "Количество", "Цена с НДС", "Сумма с НДС", "Ставка НДС",
                             //           "Организация (код)", "Организация (наименование)", "Единица измерения (код)", 
                             //           "Единица измерения (название)", "Номер накладной поставщика"};

                             //SystemConecto.ErorDebag(myDataSet.Tables.Count.ToString(), 2);

                             ConectoFileXML.ConnectFXML OFile = new ConectoFileXML.ConnectFXML("namecolumn_ptsk.fb.xml", PuthUser);
                             DataTable NameColumn = OFile.XMLNColumntoDataTable();

                            //Приходные накладные
                            myDataSet.Tables["Ptskb52"].WriteToFile(PutchDir_ + @"\Prixodnie nakladnie.csv", NameColumn, TypeFile);

                             //myDataSet.Tables[0].WriteToFile(PutchDir_.Text + @"\Акты списания.csv", TitleCSV, TypeFile.Content.ToString());
                             #endregion

                             idBlock++;
                         }

                         // Только по подразделениям ресторана '02','03' Потом отключим

                         if (idBlock == 4)
                         {
                             #region Справочник товаров


                             // Чтение в текст максимальная скорость (избежал чтения и конвертацию из bytes[])
                             string[] ReadSql = SystemConecto.ReadFile_SqlPass(SystemConecto.PutchApp + @"config\user\import_stv.sql.fb.txt");

                             // Чтение в текст максимальная скорость (избежал чтения и конвертацию из bytes[])
                             string decryptedtext = Convert.ToInt32(ReadSql[0]) == 0 ? Properties.Resources.import_stv_sql_fb : ReadSql[1];



                            // Чтение в текст максимальная скорость (избежал чтения и конвертацию из bytes[])
                            // string decryptedtext = Properties.Resources.import_stv_sql_fb;
                            // StringReader sReader = new StringReader(decryptedtext);

                            //// Чтение только до первого символа начало строки
                            //string decryptedtext =  sReader.ReadToEnd();

                            // Новый запрос (использовать replace для кода посчитал лишним по производительности) TypeConfigAutorizeSDR.RestoranSDR - ConvertIn (; - '',)
                            // "'02','03'" - TypeConfigAutorizeSDR.RestoranSDR
                            SelectFD = string.Format(decryptedtext, DateS_SqlFB, DatePO_SqlFB, TypeConfigAutorizeSDR.RestoranSDR, TypeConfigAutorizeSPORG.RestoranSDR);


                             // Запрос
                             //string SelectFD_ = string.Format("select formatdate(hd.dat,'dd.mm.yyyy') as dat,hd.num,  hd.podr , " + //hd.dat
                             //   "(select name from  spr_podr p where p.kod=hd.PODR ) as podr_name , " +
                             //   "(select p.org from spr_podr p where  p.kod=hd.podr) as org_our, " +
                             //   "(select o.name from spr_podr p, spr_org o where o.kod=p.org and p.kod=hd.podr) as org_our_name, " +
                             //   "rc.TOV, (select name from spr_tov where kod=tov) as tov_name, " +
                             //   "rc.DOP_CNT as cnt, rc.DOP_PRICE as priceNds, " + // --round(rc.DOP_PRICE/(1+20/100),5) as priceNoNds,  " +
                             //   "rc.SUMMA as summaNds, hd.nds, " +
                             //   "hd.post, (select name from spr_org where hd.post=kod) as org_name, " +
                             //    //"--round(rc.SUMMA/(1+20/100),2) as summaNoNds, " +
                             //   "(case rc.ED_NAME    when 'кг' then 1   when 'л' then 2   when 'порц' then 3   when 'гр' then 4   when 'мл' then 6   else 'шт' end) edId,rc.ED_NAME,hd.NUM_POST " +
                             //    "from mn_hd_tov_in hd, mn_rc_tov_in rc " +
                             //   "where rc.doc=hd.kod and hd.white=1and (hd.dat between '{0}' and '{1}') and hd.TYPE_DOC=1 " +
                             //   "and ('{2}'='0' or CHECKKODENTRY(hd.podr,'{2}')>0) " +
                             //   "and ('{3}'='0' or (EXISTS(select p.kod from  spr_podr p where p.kod=hd.PODR  and p.org='{3}') ))  " +
                             //   "order by hd.dat,hd.num,rc.tov " +

                             //    "", DateS_SqlFB, DatePO_SqlFB, "'02','03'", "0");

                             //SystemConecto.ErorDebag(SelectFD, 1);


                             //5. Declare an Adapter to interface with our Table in Firebird

                             FirebirdSql.Data.FirebirdClient.FbDataAdapter myDataAdapter_4 =
                                  new FirebirdSql.Data.FirebirdClient.FbDataAdapter(SelectFD, bdFbDefConect); //:dbeg - {0} :dend - {1} :podr - {2} :org - {3}
                             //DateS_SqlFB, DatePO_SqlFB, "0", "0"

                             //6. Fill the Dataset
                             myDataAdapter_4.Fill(myDataSet, "stv52");



                             // проверить и создать путь
                             SystemConecto.DIR_(PutchDir_);

                             //TitleCSV = new string[4]{"Номенклатура (код)", "Номенклатура (наименование)", "Единица измерения (код)", 
                             //           "Единица измерения (название)"};

                             ConectoFileXML.ConnectFXML OFile = new ConectoFileXML.ConnectFXML("namecolumn_stv.fb.xml", PuthUser);
                             DataTable NameColumn = OFile.XMLNColumntoDataTable();


                            //SystemConecto.ErorDebag(myDataSet.Tables.Count.ToString(), 2);
                            // Справочник товаров
                            myDataSet.Tables["stv52"].WriteToFile(PutchDir_ + @"\Spravochnik productov.csv", NameColumn, TypeFile);

                             //myDataSet.Tables[0].WriteToFile(PutchDir_.Text + @"\Акты списания.csv", TitleCSV, TypeFile.Content.ToString());
                             #endregion

                             idBlock++;
                         }

                        if (idBlock == 5)
                        {
                            #region Перемещение товаров


                            // Чтение в текст максимальная скорость (избежал чтения и конвертацию из bytes[])
                            // import_stv_sql_fb
                            string[] ReadSql = SystemConecto.ReadFile_SqlPass(SystemConecto.PutchApp + @"config\user\import_peremech_prod.sql.fb.txt");

                            // Чтение в текст максимальная скорость (избежал чтения и конвертацию из bytes[])
                            string decryptedtext = Convert.ToInt32(ReadSql[0]) == 0 ? Properties.Resources.import_peremech_prod_sql_fb : ReadSql[1];



                            // Чтение в текст максимальная скорость (избежал чтения и конвертацию из bytes[])
                            // string decryptedtext = Properties.Resources.import_stv_sql_fb;
                            // StringReader sReader = new StringReader(decryptedtext);

                            //// Чтение только до первого символа начало строки
                            //string decryptedtext =  sReader.ReadToEnd();

                            // Новый запрос (использовать replace для кода посчитал лишним по производительности) TypeConfigAutorizeSDR.RestoranSDR - ConvertIn (; - '',)
                            SelectFD = string.Format(decryptedtext, DateS_SqlFB, DatePO_SqlFB, TypeConfigAutorizeSDR.RestoranSDR, TypeConfigAutorizeSPORG.RestoranSDR);


                            // Запрос
                            //string SelectFD_ = string.Format("select formatdate(hd.dat,'dd.mm.yyyy') as dat,hd.num,  hd.podr , " + //hd.dat
                            //   "(select name from  spr_podr p where p.kod=hd.PODR ) as podr_name , " +
                            //   "(select p.org from spr_podr p where  p.kod=hd.podr) as org_our, " +
                            //   "(select o.name from spr_podr p, spr_org o where o.kod=p.org and p.kod=hd.podr) as org_our_name, " +
                            //   "rc.TOV, (select name from spr_tov where kod=tov) as tov_name, " +
                            //   "rc.DOP_CNT as cnt, rc.DOP_PRICE as priceNds, " + // --round(rc.DOP_PRICE/(1+20/100),5) as priceNoNds,  " +
                            //   "rc.SUMMA as summaNds, hd.nds, " +
                            //   "hd.post, (select name from spr_org where hd.post=kod) as org_name, " +
                            //    //"--round(rc.SUMMA/(1+20/100),2) as summaNoNds, " +
                            //   "(case rc.ED_NAME    when 'кг' then 1   when 'л' then 2   when 'порц' then 3   when 'гр' then 4   when 'мл' then 6   else 'шт' end) edId,rc.ED_NAME,hd.NUM_POST " +
                            //    "from mn_hd_tov_in hd, mn_rc_tov_in rc " +
                            //   "where rc.doc=hd.kod and hd.white=1and (hd.dat between '{0}' and '{1}') and hd.TYPE_DOC=1 " +
                            //   "and ('{2}'='0' or CHECKKODENTRY(hd.podr,'{2}')>0) " +
                            //   "and ('{3}'='0' or (EXISTS(select p.kod from  spr_podr p where p.kod=hd.PODR  and p.org='{3}') ))  " +
                            //   "order by hd.dat,hd.num,rc.tov " +

                            //    "", DateS_SqlFB, DatePO_SqlFB, "'02','03'", "0");

                            //SystemConecto.ErorDebag(SelectFD, 1);


                            //5. Declare an Adapter to interface with our Table in Firebird

                            FirebirdSql.Data.FirebirdClient.FbDataAdapter myDataAdapter_5 =
                                 new FirebirdSql.Data.FirebirdClient.FbDataAdapter(SelectFD, bdFbDefConect); //:dbeg - {0} :dend - {1} :podr - {2} :org - {3}
                                                                                                             //DateS_SqlFB, DatePO_SqlFB, "0", "0"

                            //6. Fill the Dataset
                            myDataAdapter_5.Fill(myDataSet, "wtsk_ptsk");



                            // проверить и создать путь
                            SystemConecto.DIR_(PutchDir_);

                            //TitleCSV = new string[4]{"Номенклатура (код)", "Номенклатура (наименование)", "Единица измерения (код)", 
                            //           "Единица измерения (название)"};

                            ConectoFileXML.ConnectFXML OFile = new ConectoFileXML.ConnectFXML("namecolumn_peremech_prod.fb.xml", PuthUser);
                            DataTable NameColumn = OFile.XMLNColumntoDataTable();


                            //SystemConecto.ErorDebag(myDataSet.Tables.Count.ToString(), 2);
                            // Справочник товаров
                            myDataSet.Tables["wtsk_ptsk"].WriteToFile(PutchDir_ + @"\Peremeshenie productov.csv", NameColumn, TypeFile);

                            //myDataSet.Tables[0].WriteToFile(PutchDir_.Text + @"\Акты списания.csv", TitleCSV, TypeFile.Content.ToString());
                            #endregion

                            idBlock++;
                        }

                    }

                     if (TypeConfigAutorizeSDR.Otel)
                     {
                         idBlock = idBlock == 1 ? 5 : idBlock;

                         if (idBlock == 5)
                         {
                             #region Выгрузка аналитики по движению товаров и услуг по отелю



                             // Чтение в текст максимальная скорость (избежал чтения и конвертацию из bytes[])
                             string decryptedtext = Properties.Resources.import_stv_oborot_otel_sql_fb;
                             // StringReader sReader = new StringReader(decryptedtext);

                             //// Чтение только до первого символа начало строки
                             //string decryptedtext =  sReader.ReadToEnd();

                             // Новый запрос (использовать replace для кода посчитал лишним по производительности)
                             SelectFD = string.Format(decryptedtext, DateS_SqlFB, DatePO_SqlFB, TypeConfigAutorizeSDR.RestoranSDR, TypeConfigAutorizeSPORG.RestoranSDR);


                             // Запрос
                             //string SelectFD_ = "";

                             //SystemConecto.ErorDebag(SelectFD, 1);


                             //5. Declare an Adapter to interface with our Table in Firebird

                             FirebirdSql.Data.FirebirdClient.FbDataAdapter myDataAdapter_3 =
                                  new FirebirdSql.Data.FirebirdClient.FbDataAdapter(SelectFD, bdFbDefConect); //:dbeg - {0} :dend - {1} :podr - {2} :org - {3}
                             //DateS_SqlFB, DatePO_SqlFB, "0", "0"

                             //6. Fill the Dataset
                             myDataAdapter_3.Fill(myDataSet, "stvoborot52");



                             // проверить и создать путь
                             SystemConecto.DIR_(PutchDir_);

                            //TitleCSV = new string[4]{"Номенклатура (код)", "Номенклатура (наименование)", "Единица измерения (код)", 
                            //           "Единица измерения (название)"};

                            //TitleCSV = new string[]{};
                            //SystemConecto.ErorDebag(myDataSet.Tables.Count.ToString(), 2);
                            //Оборот товаров и услуг отеля
                            myDataSet.Tables["stvoborot52"].WriteToFile(PutchDir_ + @"\Oborot tovarov i uslug otel.csv", new DataTable(), TypeFile);

                             //myDataSet.Tables[0].WriteToFile(PutchDir_.Text + @"\Акты списания.csv", TitleCSV, TypeFile.Content.ToString());
                             #endregion

                             idBlock++;
                         }


                     }

                    DBConecto.DBcloseFBConectionMemory();

                 }


                 //if (DBConecto.DBopenFB())
                 //{
                 //    //DBConecto.bdFbDefConect.BeginTransaction();

                     
                     
                 //}


            //        string ConnectionString = 
            //"User ID=sysdba;Password=masterkey;Database=localhost:D:\\USINGFIREBIRD.FDB;DataSource=localhost;Charset=NONE;"; 
			
            //        FbConnection addDetailsConnection = new FbConnection(ConnectionString);
            //        addDetailsConnection.Open();
            //    FbTransaction addDetailsTransaction = addDetailsConnection.BeginTransaction();
		
            //        string SQLCommandText = " INSERT into Details Values"+"('"+NameBox.Text+"',"+Int32.Parse(AgeBox.Text)+","+"'"+SexBox.Text+"')";
		
		
            //        FbCommand addDetailsCommand = new FbCommand(SQLCommandText,addDetailsConnection,addDetailsTransaction);
            //        addDetailsCommand.ExecuteNonQuery();
            //        addDetailsTransaction.Commit();
		
			

                 //FbCommand SelectTable = new FbCommand(StrCreate, DBConecto.bdFbSystemConect);
                 //SelectTable.CommandType = CommandType.Text;
                 //SelectTable.Connection.Open();
                 //SelectTable.ExecuteNonQuery(); // ExecuteScalar(); // ExecuteScalar();
                 ////SelectTable.Connection.Open();
                 //FbDataAdapter Adapter = new FbDataAdapter(SelectTable);


                 //DataSet ListTable = new DataSet("TableBD");

                 //if (Adapter != null)
                 //{
                 //    // DBAdapter.Fill(DSZapit);
                 //    // Adapter.Fill(ListTable, "TableBD");




                 //}

                 ////
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
             catch (FirebirdSql.Data.FirebirdClient.FbException exFB)
             {

                 // -206 - не ввел параметр
                 SystemConecto.ErorDebag("При обращении к БД ..., возникло исключение: " + Environment.NewLine +
                        " === IDCode: " + exFB.ErrorCode.ToString() + Environment.NewLine +
                        " === Message: " + exFB.Message.ToString() + Environment.NewLine +
                        " === Exception: " + exFB.ToString(), 1);
                 //335544721 - обрыв соединения;
                 if (exFB.ErrorCode == 335544721)
                 {
                     if(WaitNetTimeStart_EXPORT.IsRunning){
                          // Отладка
                          //  SystemConecto.ErorDebag(WaitNetTimeStart_EXPORT.Elapsed.Seconds.ToString() + " / " +
                          // SystemConecto.WaitNetTimeSec.Seconds.ToString() , 1);
                            if (WaitNetTimeStart_EXPORT.Elapsed.Seconds >= Convert.ToInt32(SystemConecto.WaitNetTimeSec.Seconds))
                            {
                                return;

                            }
                            Thread.Sleep(5000);
                     }else{
                        WaitNetTimeStart_EXPORT.Start();
                         Thread.Sleep(5000);
                       
                     }

                     // Повторяем запуск   
                     //Ecxport_Click_Block(sender, e, idBlock);
                     Ecxport_Click_Block(PrSendThread);
                     
                 }
        


             }   
             catch (Exception ex)
             {
                 
                 // -206 - не ввел параметр
                 SystemConecto.ErorDebag("При обращении к БД ..., возникло исключение: " + Environment.NewLine +
                        " === Message: " + ex.Message.ToString() + Environment.NewLine +
                        " === Exception: " + ex.ToString(), 1);

             }
           
             finally
			{
				
				DBConecto.DBcloseFBConectionMemory(); 
			}

             #region Статус Выполнения потоков StatusBarCode
             // Поток завершен - перед каждым return и в конце кода
             Interlocked.Increment(ref lockerEx[1]);
             #endregion
       

         }


        #endregion


        #region Переключение закладки Экспорт настройки 

         private void EcxportOpcii_Click(object sender, RoutedEventArgs e)
         {
             OpciiAll_TabPage.IsSelected = true;
             AdminExport.IsSelected = true;
             
             
         }

         #endregion

         #region Импорт финансовых транзакций (выписка банка, карточки)

         #region Путь к файлам импорта

                 /// <summary>
                 /// Выбор директории где расположен фронт не по умолчанию
                 /// </summary>
                 /// <param name="sender"></param>
                 /// <param name="e"></param>
                 private void DirBank_Front_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
                 {

                     // Configure open file dialog box  link PresentetionFramework
                     //Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();

                     var dlg = new System.Windows.Forms.FolderBrowserDialog();

                     dlg.Description = "Путь к файлам";

                     //dlg.FileName = "B52FrontOffice"; // Default file name
                     //dlg.DefaultExt = ".exe"; // Default file extension
                     //dlg.Filter = "Файл приложения |*.exe"; // Filter files by extension
                     //dlg.Title = "Путь к основному фронту";


                     // Show open file dialog box
                     //Nullable<bool> result = dlg.ShowDialog();
                     System.Windows.Forms.DialogResult result = dlg.ShowDialog();

                     // Process open file dialog box results
                     // Улучшить проверку
                     if (result == System.Windows.Forms.DialogResult.OK)
                     {
                         //Чтение параметра и запись нового значения
                         var ItemTextBoxPatch = (TextBox)LogicalTreeHelper.FindLogicalNode(this, "PutchDir_ImBank");
                         ItemTextBoxPatch.Text = dlg.SelectedPath; //dlg.FileName;
                         Update_Putch_ImBank(ref sender, "PutchDir_ImBank", "Import_PutchBankDefault", 1);

                     }

                 }


                 /// <summary>
                 /// Изменение параметра директории где расположен фронт не по умолчанию
                 /// </summary>
                 /// <param name="TypeValue">Тип значения 0 - ввод руками</param>
                 private void Update_Putch_ImBank(ref object sender, string NameElement, string NameConfig, int TypeValue = 0)
                 {
                     // MessageBox.Show("Парам");
                     // Имя параметра что меняется
                     TextBox ItemTextBoxPatch = null;
                     if (sender is TextBox)
                     {
                         ItemTextBoxPatch = (TextBox)sender;
                     }
                     else
                     {
                         ItemTextBoxPatch = (TextBox)LogicalTreeHelper.FindLogicalNode(this, NameElement);
                     }

                     //Чтение параметра и запись нового значения

                     // Запись предыдущего значения
                     AppPlayStory_1C.UpdateParamUndo(NameConfig);
                     // Активация отката (сначало красная елси диагностика положительная то зеленая)
                     ((Label)LogicalTreeHelper.FindLogicalNode(this, NameElement + "Undo_patch")).Visibility = Visibility.Visible;
                     //SystemConecto.aParamApp
                     // Запись значения
                     if (!AppPlayStory_1C.ControllerParam(ItemTextBoxPatch.Text, NameConfig, 11))
                     {
                         //Поменять цвет иконки, а также вывести сообщение в окне диагностики (Разработка)
                     }

                     // Перезапись новго значения
                     if (TypeValue > 0)
                     {
                         ItemTextBoxPatch.Text = AppPlayStory_1C.UserconfigWorkSpace[NameConfig];
                     }
                 }


                 /// <summary>
                 /// Начать вввод
                 /// </summary>
                 /// <param name="sender"></param>
                 /// <param name="e"></param>
                 private void PutchDir_ImBank_KeyDown(object sender, KeyEventArgs e)
                 {

                     KeyUp_TextBox_ImBank(ref sender, ref e, "Import_PutchBankDefault");
                 }


                 /// <summary>
                 /// Комбинации клавиш для полей ввода 
                 /// </summary>
                 /// <param name="sender"></param>
                 /// <param name="e"></param>
                 private void KeyUp_TextBox_ImBank(ref object sender, ref KeyEventArgs e, string NameConfig)
                 {
                     TextBox ItemTextBoxPatch = null;
                     if (sender is TextBox)
                     {
                         ItemTextBoxPatch = (TextBox)sender;
                     }
                     else
                     {
                         return;
                     }

                     // Клавиши редактора (Могут отсутствовать)
                     Image ItemImageButtonDir = (Image)LogicalTreeHelper.FindLogicalNode(this, ItemTextBoxPatch.Name + "Brw_");
                     Label ItemImageButtonEditDir = (Label)LogicalTreeHelper.FindLogicalNode(this, ItemTextBoxPatch.Name + "Edit_");


                     switch (e.Key)
                     {
                         case Key.Escape:
                             //Чтение параметра и запись нового значения
                             //var ItemTextBoxPatch = (TextBox)LogicalTreeHelper.FindLogicalNode(this, NameTextBop);
                             ItemTextBoxPatch.Text = AppPlayStory_1C.UserconfigWorkSpace[NameConfig];
                             ItemImageButtonEditDir.Visibility = Visibility.Collapsed;
                             ItemImageButtonDir.Visibility = Visibility.Visible;
                             break;
                         case Key.Return:
                             Update_Putch_ImBank(ref sender, "PutchDir_ImBank", "Import_PutchBankDefault");
                             // Емитация нажатия Ентер
                             // Клавиши редактора (Могут отсутствовать)
                             ((Image)LogicalTreeHelper.FindLogicalNode(this, "PutchDir_ImBankBrw_")).Visibility = Visibility.Visible;
                             ((Label)LogicalTreeHelper.FindLogicalNode(this, "PutchDir_ImBankEdit_")).Visibility = Visibility.Collapsed;
                             break;

                         //default:


                         //   break;

                     }


                     return;


                 }


                 /// <summary>
                 /// Изменение текста, метод обработки события устанавливается в коде во время создания объекта
                 /// </summary>
                 /// <param name="sender"></param>
                 /// <param name="e"></param>
                 private void PutchDir_ImBank_TextChanged(object sender, TextChangedEventArgs e)
                 {
                     // Пример обращения
                     // Клавиши редактора (Могут отсутствовать)
                     //((Image)LogicalTreeHelper.FindLogicalNode(this, "PutchDir_Brw_")).Visibility = Visibility.Hidden;
                     //((Label)LogicalTreeHelper.FindLogicalNode(this, "PutchDir_Edit_")).Visibility = Visibility.Visible;
                     PutchDir_ImBankBrw_.Visibility = Visibility.Hidden;
                     PutchDir_ImBankEdit_.Visibility = Visibility.Visible;
                 }


                 /// <summary>
                 /// Откат предыдущего значения Путь к фронту
                 /// </summary>
                 /// <param name="sender"></param>
                 /// <param name="e"></param>
                 private void PutchDir_ImBankUndo_patch__MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
                 {
                     // Имя параметра что откатывается
                     var NameConfig = "Import_PutchBankDefault";

                     //Чтение параметра и запись нового значения
                     var ItemTextBoxPatch = (TextBox)LogicalTreeHelper.FindLogicalNode(this, "PutchDir_ImBank");
                     // Пример динамических переменных
                     //ItemTextBoxPatch.Text = AppPlayStory_1C.aParamAppUndo.ContainsKey(NameParam) ? AppPlayStory_1C.aParamAppUndo[NameParam] : "";

                     ItemTextBoxPatch.Text = AppPlayStory_1C.aParamAppUndo[NameConfig];

                     if (AppPlayStory_1C.ControllerParam(AppPlayStory_1C.UserconfigWorkSpace[NameConfig], NameConfig, 11))
                     {
                         //Поменять цвет иконки, а также вывести сообщение в окне диагностики (Разработка)
                     }

                     // Удалить из памяти
                     AppPlayStory_1C.aParamAppUndo.Remove(NameConfig);
                     ((Label)LogicalTreeHelper.FindLogicalNode(this, "PutchDir_ImBankUndo_patch")).Visibility = Visibility.Collapsed;
                     ((Label)LogicalTreeHelper.FindLogicalNode(this, "PutchDir_ImBankEdit_")).Visibility = Visibility.Collapsed;
                     ((Image)LogicalTreeHelper.FindLogicalNode(this, "PutchDir_ImBankBrw_")).Visibility = Visibility.Visible;

                 }


                 /// <summary>
                 /// Сохранить измения
                 /// </summary>
                 /// <param name="sender"></param>
                 /// <param name="e"></param>
                 private void PutchDir_ImBankEdit__MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
                 {
                     Update_Putch_ImBank(ref sender, "PutchDir_ImBank", "Import_PutchBankDefault");
                     // Емитация нажатия Ентер
                     // Клавиши редактора (Могут отсутствовать)
                     ((Label)LogicalTreeHelper.FindLogicalNode(this, "PutchDir_ImBankUndo_patch")).Visibility = Visibility.Collapsed;
                     ((Image)LogicalTreeHelper.FindLogicalNode(this, "PutchDir_ImBankBrw_")).Visibility = Visibility.Visible;
                     ((Label)LogicalTreeHelper.FindLogicalNode(this, "PutchDir_ImBankEdit_")).Visibility = Visibility.Collapsed;

             

                 }

                #endregion


         #region Импорт данных из файлов dbf, csv, xls



                 // Экспортировать
                 private void ImportBank_Click(object sender, RoutedEventArgs e)
                 {



                     #region Статус Выполнения потоков StatusBarCode

                     // Защита от повторного нажатия
                     if (ImportBank.Cursor == Cursors.Wait)
                         return;

                     // Клавиша в ожидании
                     ImportBank.Cursor = Cursors.Wait;

                     // Включение отслеживание потоков
                     locker = new int[] { 0, -1 };
                     if (lockerStat != null && lockerStat.Count > 0)
                         lockerStat.Clear();

                     if (lockerComment != null && lockerComment.Count > 0)
                         lockerComment.Clear();

                     #region Полоска выполнения

                     // Вся длина кода border - mergin ImportBank_ProccessBack
                     double MaxEnd = this.Width - 100 - 365;
                     // Стартовая позиция
                     ImportBank_Proccess.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
                     ImportBank_Proccess.Width = RezervContentStatusBar;
                     ImportBank_Proccess_Label.Content = "Процесс выполенения";
                     ImportBank_Proccess.Visibility = Visibility.Visible;



                     #endregion

                     #endregion


                     #region Востановить выполнение процессов
                     // idBlock - выполненный блок
                     #endregion

                     DateTime dateSs = dateS.SelectedDate.Value;

                     string DateS_SqlFB = string.Format("{0}.{1}.{2}", dateSs.Day, dateSs.Month, dateSs.Year);

                     DateTime datePos = datePo.SelectedDate.Value;

                     string DatePO_SqlFB = string.Format("{0}.{1}.{2}", datePos.Day, datePos.Month, datePos.Year);

                     RenderInfo PrSendThread = new RenderInfo();

                     

                     PrSendThread.SizeWidthLineStatusBar = MaxEnd;
                     PrSendThread.TypeFile = TypeFile.Content.ToString();
                     PrSendThread.PutchDir_ = PutchDir_ImBank.Text;
                     PrSendThread.idBlock = 1;
                     PrSendThread.WinOnStart = this;

                     PrSendThread.LoginUserAutoriz = SystemConecto.AutorizUser.LoginUserAutoriz;
                     PrSendThread.PaswdUserAutoriz = SystemConecto.AutorizUser.PaswdUserAutoriz; // SystemConecto.aParamApp["Autorize_Admin_Paswd"]; //"alttab79";

                     PrSendThread.BDSERVER_IP = AppPlayStory_1C.UserconfigWorkSpace["BDSERVER_IP"];
                     PrSendThread.BDSERVER_Alias = AppPlayStory_1C.UserconfigWorkSpace["BDSERVER_Alias"];
                     PrSendThread.BDSERVER_Putch_Hide = AppPlayStory_1C.UserconfigWorkSpace["BDSERVER_Putch-Hide"];

                     //Grid MessageTextInner = new message().WaitMessageShow("Формирование документов:" + Environment.NewLine
                     //    + " Акты реализации.", this, 3, "TextInfo");

                     //Grid.SetRow(MessageTextInner, 1);
                     //Grid.SetColumn(MessageTextInner, 3);

                     //MessageTextInner.Name = "TextInfo";

                     //WinGrid.Children.Add(MessageTextInner);

                     Thread thStartWEB = new Thread(ImportBank_Click_Block);
                     thStartWEB.SetApartmentState(ApartmentState.STA);
                     thStartWEB.IsBackground = true; // Фоновый поток
                     thStartWEB.Start(PrSendThread);



                 }


                 /// <summary>
                 /// Импорт данных из файла
                 /// </summary>
                 /// <param name="PrSendThread"></param>
                 private void ImportBank_Click_Block(object PrSendThread)
                 {

                     RenderInfo ParametrsBlock = (RenderInfo)PrSendThread;

                     int idBlock = ParametrsBlock.idBlock;

                     //string PutchDir_ = ParametrsBlock.PutchDir_;
                     //string TypeFile = ParametrsBlock.TypeFile;
                     //dateS
                     //datePo
                     // Емуляция
                     //podr_
                     //organiz_уу

                     try
                     {

                         // под паролем администратора проверка соединения
                         //DBConecto.ParamStringServerFB[1] = ParametrsBlock.LoginUserAutoriz;
                         //DBConecto.ParamStringServerFB[2] = ParametrsBlock.PaswdUserAutoriz; // SystemConecto.aParamApp["Autorize_Admin_Paswd"]; //"alttab79";

                         //DBConecto.ParamStringServerFB[3] = ParametrsBlock.BDSERVER_IP;
                         //DBConecto.ParamStringServerFB[4] = ParametrsBlock.BDSERVER_Alias;
                         //DBConecto.ParamStringServerFB[5] = ParametrsBlock.BDSERVER_Putch_Hide;


                         // Подключение всех файлов директории с раширением dbf. xls. csv
                         // Полный путь к файлам 
                         string[] filesDBF = Directory.GetFiles(ParametrsBlock.PutchDir_, "*.dbf"); // список всех dbf файлов в директории 

                         for (int i = 0; i < filesDBF.Length; i++)
                         {

                             // Определить информацию о файле
                             FileInfo fi = new FileInfo(filesDBF[i]);
                             //Отладка
                             // MessageBox.Show(string.Format("Имя {0}, время {1}, Размер {2}", fi.Name, fi.LastWriteTime, fi.Length));

                             // Размер файла
                             //row["SizeFile"] = DevicePC.FormatByteCount(Convert.ToUInt64(fi.Length));

                             // ---- Разделить потоки чтения файлов увеличивается обработка в несколько раз
                             // Параметры потока ParametrsBlock
                             ParametrsBlock.NameFile_ = fi.Name;
                             ParametrsBlock.FileInfo_ = fi;
                             ParametrsBlock.TypeFile = "dbf_1251";

                             Thread ImportDBF = new Thread(WriteBDData);
                             ImportDBF.SetApartmentState(ApartmentState.STA);
                             ImportDBF.IsBackground = true; // Фоновый поток
                             ImportDBF.Priority = ThreadPriority.Lowest;
                             ImportDBF.Start(ParametrsBlock);

                         }
                         // Подключение всех файлов директории с раширением dbf. xls. csv
                         // Полный путь к файлам 
                         filesDBF = Directory.GetFiles(ParametrsBlock.PutchDir_ + @"\866", "*.dbf"); // список всех dbf файлов в директории 

                         for (int i = 0; i < filesDBF.Length; i++)
                         {

                             // Определить информацию о файле
                             FileInfo fi = new FileInfo(filesDBF[i]);
                             //Отладка
                             // MessageBox.Show(string.Format("Имя {0}, время {1}, Размер {2}", fi.Name, fi.LastWriteTime, fi.Length));

                             // Размер файла
                             //row["SizeFile"] = DevicePC.FormatByteCount(Convert.ToUInt64(fi.Length));

                             // ---- Разделить потоки чтения файлов увеличивается обработка в несколько раз
                             // Параметры потока ParametrsBlock
                             ParametrsBlock.NameFile_ = fi.Name;
                             ParametrsBlock.FileInfo_ = fi;
                             ParametrsBlock.TypeFile = "dbf_866";

                             Thread ImportDBF = new Thread(WriteBDData);
                             ImportDBF.SetApartmentState(ApartmentState.STA);
                             ImportDBF.IsBackground = true; // Фоновый поток
                             ImportDBF.Priority = ThreadPriority.Lowest;
                             ImportDBF.Start(ParametrsBlock);

                         }

                         string[] filesXLS = Directory.GetFiles(ParametrsBlock.PutchDir_, "*.xls"); // список всех xls файлов в директории 

                         for (int i = 0; i < filesXLS.Length; i++)
                         {

                             // Определить информацию о файле
                             FileInfo fi = new FileInfo(filesXLS[i]);
                             //Отладка
                             // MessageBox.Show(string.Format("Имя {0}, время {1}, Размер {2}", fi.Name, fi.LastWriteTime, fi.Length));

                             // Размер файла
                             //row["SizeFile"] = DevicePC.FormatByteCount(Convert.ToUInt64(fi.Length));

                             // ---- Разделить потоки чтения файлов увеличивается обработка в несколько раз
                             // Параметры потока ParametrsBlock
                             ParametrsBlock.NameFile_ = fi.Name;
                             ParametrsBlock.FileInfo_ = fi;
                             ParametrsBlock.TypeFile = "xls_1251";

                             // WriteBDData - Историчское
                             Thread ImportXLS = new Thread(WriteBDData);
                             ImportXLS.SetApartmentState(ApartmentState.STA);
                             ImportXLS.IsBackground = true; // Фоновый поток
                             ImportXLS.Priority = ThreadPriority.Lowest;
                             ImportXLS.Start(ParametrsBlock);

                         }


                         string[] filesCSV = Directory.GetFiles(ParametrsBlock.PutchDir_, "*.csv"); // список всех csv файлов в директории 





                     }

                     catch (Exception ex)
                     {

                         // -206 - не ввел параметр
                         SystemConecto.ErorDebag("При обращении к БД ..., возникло исключение: " + Environment.NewLine +
                                " === Message: " + ex.Message.ToString() + Environment.NewLine +
                                " === Exception: " + ex.ToString(), 1);

                     }

                     //finally
                     //{

                     //    DBConecto.DBcloseFBConectionMemory();
                     //}


                     #region Статус Выполнения потоков StatusBarCode

                     // Контроль за потоками и их завершением работы
                     // завершение потоков - отслеживания выполнения всех потоков (что делать если хотябы один завис, идея рестарта и остановки с помощью оператора)
                     // Отследить выполнение потоков (пробую заменить while for -ом)
                     // int Write = 0;
                     for (int iEnd = 0; iEnd < 2; iEnd++)
                     {
                         if (locker[0] == locker[1])
                         {
                             // Потоки завершины
                             iEnd = 2;

                             //SystemConecto.ErorDebag("Закінчили роботу " + locker_a[1].ToString() + " потоків із " + locker_a[0] + ".", 1);

                             // Изменить интерфейс
                             System.Windows.Application.Current.Dispatcher.Invoke(new Action(delegate()
                             {
                                 // var Window_ = SystemConecto.ListWindowMain("SpecPred_");

                                 #region Полоска выполнения

                                 // Клавиша в курсор
                                 ImportBank.Cursor = Cursors.Arrow;

                                 // Стартовая позиция
                                 ImportBank_Proccess.HorizontalAlignment = System.Windows.HorizontalAlignment.Stretch;

                                 ImportBank_Proccess_Label.Content = "Импорт данных выполнен";

                                 // Сохраненная длина
                                 // ImportBank_Proccess.Width = ParametrsBlock.SizeWidthLineStatusBar;
                                 ImportBank_Proccess.Width = double.NaN;


                                 #endregion

                             }));

                         }
                         else
                         {

                             // Защита от первого срабатывания, означает выполнить проверку до того как выполнены все потоки (locker[1] = -1).
                             if (locker[1] == -1)
                             {
                                 Interlocked.Increment(ref locker[1]);
                             }
                             Thread.Sleep(150);

                             // Изменить интерфейс
                             System.Windows.Application.Current.Dispatcher.Invoke(new Action(delegate()
                             {

                                 #region Полоска выполнения

                                 // длина
                                 if (locker[0] > 0)
                                 {
                                     double CountOneProssec = (ParametrsBlock.SizeWidthLineStatusBar - RezervContentStatusBar) / locker[0];

                                     ImportBank_Proccess.Width = RezervContentStatusBar + CountOneProssec * locker[1];
                                 }
                                 #endregion

                             }));


                             // === Отладка
                             //Write++;
                             //if (Write == 20)
                             //{
                             //    Write = 0;



                             //    //SystemConecto.ErorDebag("Закінчили роботу " + locker[1].ToString() + " потоків із " + locker[0]+".", 1);
                             //}
                             //========
                             // Продолжить
                             iEnd = 0;
                         }


                     }



                     #endregion


                 }


                 #region Потоки записи файлов dbf, xls, csv



                 /// <summary>
                 /// Импорт данных из файла
                 /// </summary>
                 /// <param name="PrSendThread"></param>
                 private void WriteBDData(object PrSendThread)
                 {

                     RenderInfo ParametrsBlock = (RenderInfo)PrSendThread;

                     #region Статус Выполнения потоков StatusBarCode
                     // Поток запущен
                     Interlocked.Increment(ref locker[0]);

                     #region Статус Выполнения потоков StatusBarCode
                     // Поток завершен - перед каждым return и в конце кода
                     // Interlocked.Increment(ref locker[1]);
                     #endregion
                     // Потоко не безопасная конструкция(догадка, переменная одна для всех потоков ее нужно извлечь из памяти произвести увеличение и записать в память)
                     // locker[1] = locker[1] + 1;

                     #endregion


                     // Количество транзакций которые записаны




                     try
                     {
                         // @"export.dbf"
                         // !!!Огрничение памяти есть можно со временем включитьобработку по строкам
                         // var hg = ParametrsBlock.TypeFile;

                         List<string> NameColumnDef = new List<string>();

                         switch (ParametrsBlock.TypeFile)
                         {
                             case "dbf_1251":
                                 ParametrsBlock.TableImData = DBConecto.GetAllRowsDbf(ParametrsBlock.PutchDir_, ParametrsBlock.NameFile_, Encoding.GetEncoding(1251));

                                 break;
                             case "dbf_866":
                                 ParametrsBlock.TableImData = DBConecto.GetAllRowsDbf(ParametrsBlock.PutchDir_ + @"\866", ParametrsBlock.NameFile_, Encoding.GetEncoding(866));

                                 break;

                             case "xls_1251":
                                 ParametrsBlock.TableImData = DBConecto.GetAllRowsExl(ParametrsBlock.PutchDir_, new string[] { ParametrsBlock.NameFile_ });
                                 int fh = ParametrsBlock.TableImData.Columns.Count;

                                 break;
                         }

                         // Запомнить название полей по умолчанию для применения разных схем
                         foreach (DataColumn custColumn in ParametrsBlock.TableImData.Columns)
                         {
                             NameColumnDef.Add(custColumn.Caption.ToString());
                         }

                         // Чтение базовых полей подставновки
                         string[] ColumnBaseName = new string[] { };//ParametrsBlock.ParametrsImport["BaseColumnSchemaIm"].Split(',');


                         if (ParametrsBlock.TableImData == null || ParametrsBlock.TableImData.Rows.Count < 1)
                         {
                             // Записи отсутствуют
                             #region Статус Выполнения потоков StatusBarCode
                             // Поток завершен - перед каждым return и в конце кода
                             Interlocked.Increment(ref locker[1]);
                             #endregion
                             return;
                         }

                         // Поиск схемы Схема это набор соответстия полей тег ColumnSchemaIm

                         // -- Чтение параметров
                         if (!ParametrsBlock.ParametrsImportRead)
                         {
                             // Определение конфигурационного файла
                             string Defputh = ConfigControll.PuthFileDefault(SystemConecto.PutchApp + @"config\user\importbank.xml", SystemConecto.PStartup + @"config\user\importbank.xml", ConectoWorkSpace._1С_Export.Element.configImportBank.TextFile, 1);

                             // Определения параметров
                             if (Defputh.Length > 0)
                             {
                                 // Количство схем Dictionary<string, string> )
                                 var DCountSchem = ConfigControll.ReadParamID(Defputh, ConectoWorkSpace._1С_Export.Element.configImportBank.TextFile, 1, new string[1] { "CountSchem" }, 2);
                                 int df = Convert.ToInt32(DCountSchem["CountSchem"]) + 1;
                                 if (df < 0)
                                 {
                                     // схемы отсустствуют
                                     #region Статус Выполнения потоков StatusBarCode
                                     // Поток завершен - перед каждым return и в конце кода
                                     Interlocked.Increment(ref locker[1]);
                                     #endregion

                                     return;
                                 }
                                 for (int i = 1; i < df; i++)
                                 {
                                     // Загрузка схемы
                                     ParametrsBlock.ParametrsImport = ConfigControll.ReadParamID(Defputh, ConectoWorkSpace._1С_Export.Element.configImportBank.TextFile, i, new string[] { "TypeOper", "NameSchema", "NameGroupOrgani", "IDGroupOrgani",
                    "ColumnSchemaIm", "BaseColumnSchemaIm", "idschema", "Type_OTPR_POLU","Opcii_OTPR_POLU","CurrImport","BaseCurr", "CurrDef","ColumnNameNumRow", "CashUSERID", 
                    "КодПодразделенияКассы", "РазделительСуммы", "Префикс_Документа_Приход_Расход", "Банковские_услуги_по_операциях", 
                    "ArticlesPeremechenija-...", "ArticlesOplataPokupatelja-...", "Articles_SOBST_DOHOD-...", "ArticlesOplataPostavki-...", "ArticlesBankuslugi-...", "ArticlesSpisanijaSotrudnik-...", "ArticlesAvansZvit-...","ArticlesNalogi-..."}, 2);

                                     // Проверка схемы DBF, ... и типа документов: банк, касса
                                     string[] TypeSchem = ParametrsBlock.ParametrsImport["TypeOper"].Split('_');


                                     // Востановление название полей по умолчанию для применения разных схем - особенность работает всегда!!
                                     foreach (DataColumn custColumn in ParametrsBlock.TableImData.Columns)
                                     {
                                         custColumn.ColumnName = NameColumnDef[custColumn.Ordinal];

                                     }



                                     #region Определение и загрузка параметров

                                     if (TypeSchem.Length > 1)  //&& TypeSchem[1] == "dbf"
                                     {

                                         // Тип данных
                                         switch (TypeSchem[1].ToLower())
                                         {
                                             case "dbf":

                                                 if (ParametrsBlock.TypeFile.IndexOf("dbf") == -1) // системный код мал ... .ToLower()
                                                 {
                                                     continue;
                                                 }




                                                 break;

                                             case "xls":

                                                 if (ParametrsBlock.TypeFile.IndexOf("xls") == -1) // системный код мал ... .ToLower()
                                                 {
                                                     continue;
                                                 }




                                                 break;
                                             default:
                                                 continue;

                                         }

                                         // Тип документа: выписка, ордер
                                         switch (TypeSchem[0].ToLower())
                                         {
                                             case "bank":

                                                 ColumnBaseName =

                                                       new string[] { "NUM_DOC", "DATE_DOC", "TYPE_OTPR_POLU", "CODE_OTPROV", "NAME_OTPR", "RR_OTPROV", "CODE_BANK_OTPROV", "NAMEBANK_OTPR", "CODE_POLUCH", "NAME_POLUCH", "RR_POLUCH", "CODE_BANK_POLUCH", "NAMEBANK_POLUCH", "summa", "tp_in_out", "curr", "prim", "dop_info" };


                                                 #region Описание

                                                 // BaseColumnSchemaIm - NUM_DOC,DATE_DOC,CODE_OTPROV,RR_OTPROV,CODE_POLUCH, RR_POLUCH, summa, tp_in_out, curr, prim, dop_info

                                                 //(num) NUM_DOC - номер документа бакновской выписки
                                                 //(dat) DATE_DOC- дата создания ордера
                                                 //(org) CODE_OTPROV- организация которая платит (если чужая то она оплачевает НАМ, если наша то мі кому-то оплачиваем)
                                                 //(rs ) RR_OTPROV- расчетный  счет данной организции плательщика
                                                 //(org_to) CODE_POLUCH - организация получатель денег (если наша организация то мы получаем, если чужая, то мы плательщики, а она получатель)
                                                 //(в примере выше организция '0101' это ПП Альт_таб тоесть она получатель 800 грн)
                                                 //(rs_to) RR_POLUCH - расчетній счет организации получателя
                                                 //summa - сумма банковской віписки
                                                 //tp_in_out -  статья по которой проводится проводка
                                                 //curr - валюта (по умолчанию нужно ставить 1 єто гривня)
                                                 //prim - Назначение платежа (примичание большое которое будет идти с банковской выписки)
                                                 //dop_info - тут рекомендую писать служебый комент например AUTOORDER плюс в примере добавляется дата текущая, когда делался ордер

                                                 #endregion


                                                 break;

                                             case "kassa":

                                                 ColumnBaseName =

                                                        new string[] { "NUM_DOC", "DATE_DOC", "TYPE_OTPR_POLU", "CODE_OTPROV", "CASH_PODR", "SOTR", "summa", "bank_posluga", "tp_in_out", "curr", "prim" };



                                                 #region Описание

                                                 // По умолчанию авансовый отчет, однако может быть взаиморасчет с организацией

                                                 ///< insert into mn_cash (num,dat,org,SOTR,sum_in,sum_out,tp_in_out,CASH_PODR,curr,prim)
                                                 /// values ('112222','06.05.2014','13304',null,800,0,'010003','05101',1,'AUTOORDER'||' '||DATETOSTR('today')); >


                                                 //num - номер документа (должен быть уникальным)
                                                 //dat - дата ордера

                                                 //org - код организации или null, если взаиморасчет по организации (береться из справочника spr_org). Аналог CODE_OTPROV или CODE_POLUCH.
                                                 //SOTR - код сотрудника или null, сотрудник береться из справочника spr_sotr. Аналог CODE_OTPROV или CODE_POLUCH.
                                                 //CASH_PODR - код кассы для ордера (05101 - касса1 стариченко).  Аналог CODE_POLUCH или CODE_OTPROV только мы всегда получатели! 

                                                 //sum_in - если поступление денег в кассу, то число или 0
                                                 //sum_out - сумма расхода или 0
                                                 //tp_in_out - статья денег
                                                 //curr - валюта 1 гривна 2 долары 3 евро
                                                 //prim примичание

                                                 #endregion



                                                 break;
                                             default:
                                                 continue;

                                         }

                                         #region Схема полей соответствий

                                         int NumbColumnSherch = 0;

                                         int BaseColumnSherch = 0;

                                         // --- Чтение полей подставновки
                                         string[] ColumnSchema = ParametrsBlock.ParametrsImport["ColumnSchemaIm"].Split(',');

                                         ParametrsBlock.SchemaTable = new Dictionary<string, string>();


                                         for (int NumCol = 0; NumCol < ColumnSchema.Length; NumCol++)
                                         {
                                             // Выбрать выражения /тут код/
                                             string[] ColumnV = ColumnSchema[NumCol].Split('/');

                                             if (ColumnV[0].Trim().Length > 0)
                                             {
                                                 BaseColumnSherch++;
                                                 ParametrsBlock.SchemaTable.Add(ColumnV[0].Trim().ToLower(), ColumnV.Length > 1 ? ColumnV[1] : "");
                                             }
                                             else
                                             {
                                                 ParametrsBlock.SchemaTable.Add("NoName_" + (NumCol + 1).ToString(), ColumnV.Length > 1 ? ColumnV[1] : "");
                                             }


                                         }
                                         #endregion

                                         // --- Чтение справочника валюты подставнока
                                         #region Чтение справочника валюты подставнока

                                         // Валюта поумочанию
                                         bool OnOffDef = false;
                                         if (ParametrsBlock.ParametrsImport["CurrDef"].Trim().Length < 1)
                                         {

                                             ParametrsBlock.ParametrsImport["CurrDef"] = "980";
                                         }

                                         string[] BaseCurrName = ParametrsBlock.ParametrsImport["BaseCurr"].Split(',');
                                         // защита от дурака
                                         if (BaseCurrName.Length < 1)
                                         {
                                             BaseCurrName = new string[4] { "UA", "USD", "EVRO", "RUB" };
                                         }

                                         string[] ImportCurrID = ParametrsBlock.ParametrsImport["CurrImport"].Split(',');


                                         ParametrsBlock.SchemaCur = new Dictionary<string, string>();

                                         for (int NumCol = 0; NumCol < BaseCurrName.Length; NumCol++)
                                         {

                                             // Поддержка формата строкового определения кода текст_код UA_980
                                             string[] BaseCurrNameID = BaseCurrName[NumCol].Split('_');

                                             // Код импорта - код центрланой БД
                                             string IDCirr_ = "";
                                             if (ImportCurrID.Length > NumCol)
                                             {
                                                 IDCirr_ = ImportCurrID[NumCol].ToString();
                                             }
                                             else
                                             {
                                                 IDCirr_ = BaseCurrNameID.Length > 0 ? BaseCurrNameID[1] : (NumCol + 1).ToString();
                                             }

                                             ParametrsBlock.SchemaCur.Add(IDCirr_, (NumCol + 1).ToString());

                                             // Проверка основной валюты
                                             if (ParametrsBlock.ParametrsImport["CurrDef"] == IDCirr_)
                                             {
                                                 OnOffDef = true;
                                             }

                                         }
                                         // Основная валюта, а также можно использовать запрос в ЦБД на код - 1
                                         if (!OnOffDef)
                                         {
                                             ParametrsBlock.SchemaCur.Add(ParametrsBlock.ParametrsImport["CurrDef"], "1");
                                         }

                                         #endregion


                                         #region Сопостовление схемы импорта структуры

                                         string ColumnNameFile = "";

                                         foreach (DataColumn custColumn in ParametrsBlock.TableImData.Columns)
                                         {
                                             // Проверка поля
                                             if (ParametrsBlock.SchemaTable.ContainsKey(custColumn.Caption.ToString().ToLower()))
                                             {
                                                 NumbColumnSherch++;
                                                 ColumnNameFile += custColumn.Caption.ToString() + ",";
                                             }
                                             else
                                             {
                                                 // Поддержка нумерации полей 1,2,3,4
                                                 if (ParametrsBlock.SchemaTable.ContainsKey((custColumn.Ordinal + 1).ToString()))
                                                 {
                                                     NumbColumnSherch++;
                                                     ColumnNameFile += custColumn.Caption.ToString() + ",";
                                                 }

                                             }
                                         }

                                         // ---- Проверка схемы
                                         if (NumbColumnSherch == BaseColumnSherch)
                                         {
                                             // --- Структура таблицы...

                                             int NumbColumnAlert = 0;



                                             // Проверка базовых полей и соответствий по количеству
                                             if (ColumnBaseName.Length < ParametrsBlock.SchemaTable.Count)
                                             {
                                                 // Неверная схема продолжить
                                                 SystemConecto.ErorDebag("При обращении к схеме idschema " + i + " файла " + ParametrsBlock.NameFile_ + " ..., возникло исключение: " + Environment.NewLine +
                                                               " === Схема подставляемых полей файла не соответствует базовым полям по количеству, поля файла которые задействованы: " + ColumnNameFile
                                                               , 1);
                                                 continue;
                                             }


                                             // Трансформация полученной структуры в таблице согласно схемы
                                             foreach (DataColumn custColumn in ParametrsBlock.TableImData.Columns)
                                             {
                                                 int struNum = -1;
                                                 string NameColumn = "";

                                                 // Изменения названия поля
                                                 foreach (var item in ParametrsBlock.SchemaTable)
                                                 {
                                                     struNum++;
                                                     if (custColumn.ColumnName.ToLower() == item.Key)
                                                     {

                                                         NameColumn = ColumnBaseName[struNum].Trim();
                                                         break;
                                                     }
                                                     else
                                                     {
                                                         // Поддержка числовых полей
                                                         //int NumberColumn = Convert.ToInt32(item.Key);
                                                         int NumberColumn;
                                                         if (Int32.TryParse(item.Key, out NumberColumn) && (custColumn.Ordinal + 1) == NumberColumn)
                                                         {

                                                             NameColumn = ColumnBaseName[struNum].Trim();
                                                             break;
                                                         }

                                                     }


                                                 }

                                                 // Проверка поля
                                                 //if (ParametrsBlock.SchemaTable.ContainsKey(ParametrsBlock.TableImData.Columns[NumbColumnAlert].Caption.ToLower()))
                                                 //{
                                                 //    ParametrsBlock.SchemaTable[ParametrsBlock.TableImData.Columns[NumbColumnAlert].Caption.ToLower()];
                                                 //    ColumnNameFile += custColumn.Caption.ToString() + ",";
                                                 //}
                                                 if (NameColumn.Length > 0)
                                                 {
                                                     ParametrsBlock.TableImData.Columns[NumbColumnAlert].ColumnName = NameColumn;
                                                     //var rty = ParametrsBlock.TableImData.Columns[NumbColumnAlert].ColumnName;
                                                     //var cf = rty;
                                                 }
                                                 NumbColumnAlert++;


                                             }

                                             NumbColumnAlert = 0;
                                         }
                                         else
                                         {
                                             // Неверная схема продолжить
                                             SystemConecto.ErorDebag("При обращении к схеме idschema " + i + " файла " + ParametrsBlock.NameFile_ + " ..., возникло исключение: " + Environment.NewLine +
                                                           " === Схема не соответствует, поля файла которые задействованы: " + ColumnNameFile + Environment.NewLine +
                                                           " === Количество необходимых базовых полей задействованных в схеме: " + BaseColumnSherch.ToString(), 1);
                                             continue;
                                         }

                                         #endregion

                                     }

                                     else
                                     {
                                     #endregion

                                         //if (TypeSchem.Length > 1 && TypeSchem[1] == "xls")
                                         //{

                                         //}
                                         //else
                                         //{
                                         // Неверная схема продолжить
                                         continue;
                                         //}
                                     }




                                     #region Обработка записи

                                     #region Общие параметры
                                     // PRIZNAK (1-отправленный платеж,2-полученный платеж)
                                     string[] VarParOTPO = ParametrsBlock.ParametrsImport["Opcii_OTPR_POLU"].ToUpper().Trim().Split(',');

                                     /// <summary>
                                     /// 1-код кассы источника,2-код соответствия с ЦБД
                                     /// </summary>
                                     string[] VarParKASSA_PODR = ParametrsBlock.ParametrsImport["КодПодразделенияКассы"].ToUpper().Trim().Split('/');

                                     /// <summary>
                                     /// 1-код кассы источника,2-код соответствия с ЦБД
                                     /// </summary>
                                     string[] VarParPREFICS_DOH_RASH = ParametrsBlock.ParametrsImport["Префикс_Документа_Приход_Расход"].ToUpper().Trim().Split(';');


                                     /// <summary>
                                     /// 1-код кассы источника,2-код соответствия с ЦБД
                                     /// </summary>
                                     string[] VarParBANK_USLUGA = ParametrsBlock.ParametrsImport["Банковские_услуги_по_операциях"].ToUpper().Trim().Split(';');

                                     // Количество строк которое не читаем в таблице
                                     int NumberNoReadRows;
                                     if (!Int32.TryParse(ParametrsBlock.ParametrsImport["ColumnNameNumRow"], out NumberNoReadRows))
                                     {
                                         NumberNoReadRows = 0;
                                     }


                                     if (ParametrsBlock.ParametrsImport["РазделительСуммы"].Trim().ToString().Length < 1)
                                     {
                                         SystemConecto.ErorDebag("При обращении к схеме idschema " + i + " файла " + ParametrsBlock.NameFile_ + " ..., возникло исключение: " + Environment.NewLine +
                                                              " === Схема не содержит, РазделительСуммы!", 1);

                                         #region Статус Выполнения потоков StatusBarCode
                                         // Поток завершен - перед каждым return и в конце кода
                                         Interlocked.Increment(ref locker[1]);
                                         #endregion

                                         return;
                                     }


                                     #region Подключение к БД


                                     /// <summary>
                                     /// Подключение к БД FB 2 Network
                                     /// </summary>
                                     DBConecto.UniQuery LocalQuery = new DBConecto.UniQuery("", "FB"); // new DBConecto.UniQuery(CommandAdapterQuery, "FB");

                                     LocalQuery.UserID = ParametrsBlock.LoginUserAutoriz;
                                     LocalQuery.PasswordUser = ParametrsBlock.PaswdUserAutoriz;
                                     LocalQuery.DataSource = ParametrsBlock.BDSERVER_IP;
                                     LocalQuery.Port = Convert.ToInt16(ParametrsBlock.BDSERVER_Port);
                                     LocalQuery.ServerType = "Default";
                                     LocalQuery.InitialCatalog = ParametrsBlock.BDSERVER_Alias.Length>0 ? ParametrsBlock.BDSERVER_Alias : ParametrsBlock.BDSERVER_Putch_Hide; // Одно и тоже что, ParametrsBlock.BDSERVER_Alias и ParametrsBlock.BDSERVER_Putch_Hide - объединил

                                     #endregion



                                     #region Настройка фильтра

                                     #region Создание и проверка Организации которая не определена
                                     // Проверка группы товаров для новых созданных контрагентов  происходит выше в общих параметрах
                                     LocalQuery.UserQuery = "select kod from SPR_ORG_GROUPS  where kod = @IDGroupOrgani ";

                                     LocalQuery.FBParametrsCommandUserQuery.AddWithValue("@IDGroupOrgani", ParametrsBlock.ParametrsImport["IDGroupOrgani"].ToString());

                                     int NumberRow_ = LocalQuery.ExecuteUNINonQuery();
                                     if (NumberRow_ < 1)
                                     {
                                         // запись Отсутствует добавить в справочник
                                         LocalQuery.UserQuery = "insert into SPR_ORG_GROUPS (kod,name,PARENTID) VALUES ( @IDGroupOrgani,@NameGroupOrgani,0)";

                                         LocalQuery.FBParametrsCommandUserQuery.AddWithValue("@NameGroupOrgani", ParametrsBlock.ParametrsImport["NameGroupOrgani"].ToString());

                                         NumberRow_ = LocalQuery.ExecuteUNINonQuery();

                                         //kod -  код может любой но уникальный например можно договориться как в примере 888 сделать
                                         //name -  понятно название
                                         //PARENTID -  родитель если эта група вложенная, если 0 то это корневая группа

                                     }
                                     // Проверка спец Организации "Неопределенная организация" ее код ParametrsBlock.ParametrsImport["IDGroupOrgani"] + "1"
                                     LocalQuery.UserQuery = "select kod from spr_org  where kod = @IDOrgani ";

                                     LocalQuery.FBParametrsCommandUserQuery.AddWithValue("@IDOrgani", ParametrsBlock.ParametrsImport["IDGroupOrgani"] + 1);

                                     NumberRow_ = LocalQuery.ExecuteUNINonQuery();
                                     if (NumberRow_ < 1)
                                     {
                                         // Организация отсутствует создать
                                         // запись организации в справочник
                                         LocalQuery.UserQuery = "insert into spr_org (kod,name,GRP,addr,tel,okpo,prim,FULL_NAME)" +
                                         "values (@IDOrgani, 'Неопределенная организация', @IDGroupOrgani, '','','','','Неопределенная организация')";
                                         NumberRow_ = LocalQuery.ExecuteUNINonQuery();
                                     }

                                     #region Дополнительный код
                                     //// Добовляем организацию не  COALESCE(kod,0)+1 as kod
                                     //LocalQuery.UserQuery = string.Format("select first 1 kod from  spr_org where SUBSTRING(kod from 1 for {0})= '{1}' order by kod desc", ParametrsBlock.ParametrsImport["IDGroupOrgani"].Length.ToString(),
                                     //    ParametrsBlock.ParametrsImport["IDGroupOrgani"].ToString());
                                     //// Получаем код организации
                                     //string ResultID = LocalQuery.ExecuteUNIScalar().Trim();

                                     //if (ResultID.Length < 1)
                                     //{
                                     //    // Организация отсутствует создать
                                     //    ResultID = ParametrsBlock.ParametrsImport["IDGroupOrgani"] + "1";

                                     //}
                                     #endregion


                                     // Чистка параметров
                                     LocalQuery.FBParametrsCommandUserQuery.Clear();

                                     #endregion


                                     // Определение групп движения
                                     // ArticlesPeremechenija - операции перемещения
                                     List<string[]> LArticlesPeremechenija = new List<string[]>();
                                     List<string[]> LArticlesOplataPokupatelja = new List<string[]>();
                                     List<string[]> LArticles_SOBST_DOHOD = new List<string[]>();
                                     List<string[]> LArticlesOplataPostavki = new List<string[]>();
                                     List<string[]> LArticlesSpisanijaSotrudnik = new List<string[]>();
                                     List<string[]> LArticlesAvansZvit = new List<string[]>();
                                     List<string[]> ArticlesBankuslugi = new List<string[]>();
                                     List<string[]> ArticlesNalogi = new List<string[]>();

                                     foreach (KeyValuePair<string, string> item in ParametrsBlock.ParametrsImport)
                                     {
                                         // Сформировать списки
                                         if (item.Key.IndexOf("ArticlesPeremechenija") > -1 && item.Key != "ArticlesPeremechenija-...")
                                         {
                                             string[] ListParam_ = item.Value.Split(',');
                                             for (int iL = 0; iL < ListParam_.Length; iL++)
                                             {
                                                 LArticlesPeremechenija.Add(ListParam_[iL].Split(';'));
                                             }


                                             continue;
                                         }
                                         if (item.Key.IndexOf("ArticlesOplataPokupatelja") > -1 && item.Key != "ArticlesOplataPokupatelja-...")
                                         {
                                             string[] ListParam_ = item.Value.Split(',');
                                             for (int iL = 0; iL < ListParam_.Length; iL++)
                                             {
                                                 LArticlesOplataPokupatelja.Add(ListParam_[iL].Split(';'));
                                             }


                                             continue;
                                         }
                                         if (item.Key.IndexOf("Articles_SOBST_DOHOD") > -1 && item.Key != "Articles_SOBST_DOHOD-...")
                                         {
                                             string[] ListParam_ = item.Value.Split(',');
                                             for (int iL = 0; iL < ListParam_.Length; iL++)
                                             {
                                                 LArticles_SOBST_DOHOD.Add(ListParam_[iL].Split(';'));
                                             }


                                             continue;
                                         }
                                         if (item.Key.IndexOf("ArticlesOplataPostavki") > -1 && item.Key != "ArticlesOplataPostavki-...")
                                         {
                                             string[] ListParam_ = item.Value.Split(',');
                                             for (int iL = 0; iL < ListParam_.Length; iL++)
                                             {
                                                 LArticlesOplataPostavki.Add(ListParam_[iL].Split(';'));
                                             }


                                             continue;
                                         }
                                         if (item.Key.IndexOf("ArticlesSpisanijaSotrudnik") > -1 && item.Key != "ArticlesSpisanijaSotrudnik-...")
                                         {
                                             string[] ListParam_ = item.Value.Split(',');
                                             for (int iL = 0; iL < ListParam_.Length; iL++)
                                             {
                                                 LArticlesSpisanijaSotrudnik.Add(ListParam_[iL].Split(';'));
                                             }


                                             continue;
                                         }
                                         if (item.Key.IndexOf("ArticlesAvansZvit") > -1 && item.Key != "ArticlesAvansZvit-...")
                                         {
                                             string[] ListParam_ = item.Value.Split(',');
                                             for (int iL = 0; iL < ListParam_.Length; iL++)
                                             {
                                                 LArticlesAvansZvit.Add(ListParam_[iL].Split(';'));
                                             }


                                             continue;
                                         }
                                         if (item.Key.IndexOf("ArticlesBankuslugi") > -1 && item.Key != "ArticlesBankuslugi-...")
                                         {
                                             string[] ListParam_ = item.Value.Split(',');
                                             for (int iL = 0; iL < ListParam_.Length; iL++)
                                             {
                                                 ArticlesBankuslugi.Add(ListParam_[iL].Split(';'));
                                             }


                                             continue;
                                         }
                                         if (item.Key.IndexOf("ArticlesNalogi") > -1 && item.Key != "ArticlesNalogi-...")
                                         {
                                             string[] ListParam_ = item.Value.Split(',');
                                             for (int iL = 0; iL < ListParam_.Length; iL++)
                                             {
                                                 ArticlesNalogi.Add(ListParam_[iL].Split(';'));
                                             }


                                             continue;
                                         }
                                     }


                                     #endregion



                                     #endregion

                                     #region kassa


                                     // --- Чтение параметров
                                     if (TypeSchem[0] == "kassa")
                                     {



                                         // Поиск кода пользователя CashUSERID
                                         LocalQuery.UserQuery = "select kod,name, podr from spr_sotr where USER_LOGIN=@login";


                                         LocalQuery.FBParametrsCommandUserQuery.AddWithValue("@login", LocalQuery.UserID);

                                         // Проверка кода
                                         string[] RowDataTableUSER = null;
                                         string CodeOrg = LocalQuery.ExecuteUNIScalar(out RowDataTableUSER).Trim();
                                         // Чистка параметров
                                         LocalQuery.FBParametrsCommandUserQuery.Clear();
                                         if (CodeOrg.Length > 1)
                                         {
                                             ParametrsBlock.ParametrsImport["CashUSERID"] = CodeOrg;
                                         }
                                         else
                                         {
                                             // Проверка введенного кода
                                             LocalQuery.UserQuery = "select kod,name,podr from spr_sotr where USER_LOGIN=@login";


                                             LocalQuery.FBParametrsCommandUserQuery.AddWithValue("@login", ParametrsBlock.ParametrsImport["CashUSERID"]);

                                             // Проверка кода
                                             CodeOrg = LocalQuery.ExecuteUNIScalar(out RowDataTableUSER).Trim();
                                             // Чистка параметров
                                             LocalQuery.FBParametrsCommandUserQuery.Clear();
                                             if (CodeOrg.Length < 1)
                                             {
                                                 // Неверная схема продолжить
                                                 SystemConecto.ErorDebag("При обращении к схеме idschema " + i + " файла " + ParametrsBlock.NameFile_ + " ..., возникло исключение: " + Environment.NewLine +
                                                               " === Схема не содержит, правильный код CashUSERID пользователя или введенный логин не имеет права разносить документы!", 1);

                                                 #region Статус Выполнения потоков StatusBarCode
                                                 // Поток завершен - перед каждым return и в конце кода
                                                 Interlocked.Increment(ref locker[1]);
                                                 #endregion

                                                 return;
                                             }

                                         }



                                         // Признак получателя и отправителя
                                         // - расход / + доход
                                         string minus_rashod = "sum_out";
                                         string pluse_dohod = "sum_in";
                                         switch (ParametrsBlock.ParametrsImport["Type_OTPR_POLU"].ToUpper())
                                         {

                                             case "PRIZNAK":

                                                 break;

                                             case "ZNAK_CHISLA":


                                                 if (VarParOTPO.Length > 0)
                                                 {
                                                     string[] VarParOTPO_ = VarParOTPO[0].Split(';');
                                                     if (VarParOTPO_.Length > 1)
                                                     {
                                                         switch (VarParOTPO_[0])
                                                         {
                                                             case "-":

                                                                 break;
                                                             case "+":
                                                                 minus_rashod = "sum_in";
                                                                 pluse_dohod = "sum_out";
                                                                 break;
                                                         }
                                                     }
                                                 }

                                                 break;

                                         }


                                         // Ищем документ в ЦБД (центральная база данных) по полю NUM_DOC и DATE_DOC и сумме, а также по Подразделению (может отсустствовать номер документа)
                                         // Дополнительно проверяем наличие колонки банковские услуги 
                                         bool ColumnDateDoc = false;
                                         bool BankPosluga = false;
                                         foreach (DataColumn custColumn in ParametrsBlock.TableImData.Columns)
                                         {


                                             if (custColumn.ColumnName == "NUM_DOC")
                                             {
                                                 ColumnDateDoc = true;
                                                 continue;
                                             }
                                             if (custColumn.ColumnName == "bank_posluga")
                                             {
                                                 BankPosluga = true;
                                                 continue;
                                             }
                                         }

                                         double Summa = 0;
                                         int NumberRow = 0;
                                         int NumberNoReadRowsCount = 0;
                                         // Перебор записей
                                         foreach (DataRow custRow in ParametrsBlock.TableImData.Rows)
                                         {
                                             // Игнорируем обработку записи
                                             if (NumberNoReadRows > NumberNoReadRowsCount)
                                             {
                                                 NumberNoReadRowsCount++;
                                                 continue;
                                             }


                                             // Проверка от дурака на числа или текст и разделитель числа для разных культурных различиях 
                                             // System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US"); // - ToDouble с точкой
                                             double NumberSumma;

                                             // string Test = custRow["summa"].ToString().Trim().Replace(",", ".");

                                             if (Double.TryParse(custRow["summa"].ToString().Replace(ParametrsBlock.ParametrsImport["РазделительСуммы"].Trim().ToString(), ","), out NumberSumma))
                                             {

                                                 Summa = NumberSumma; //
                                                 if (Summa == 0)
                                                 {

                                                     continue;
                                                 }

                                             }
                                             else
                                             {
                                                 // Это не число запись неверная
                                                 // Неверная схема продолжить -------- Нет смысла выводить результат этой проверки надо включить режим отладки дополнительный для этого моудуля
                                                 if (Convert.ToBoolean(AppPlayStory_1C.UserconfigWorkSpace["ВключениеРежимаОтладки_настройка_модулей"]))
                                                     SystemConecto.ErorDebag("При обращении к схеме idschema " + i + " файла " + ParametrsBlock.NameFile_ + " ..., возникло исключение: " + Environment.NewLine +
                                                                   " === Поле Summa не соответствует cхеме значение: " + custRow["summa"].ToString(), 1);

                                                 continue;
                                             }



                                             //Для определения, является ли целое число чётным или нечётным, можно использовать сл. метод:

                                             //public static bool IsNechet(int i)
                                             //{
                                             //    int result;
                                             //    Math.DivRem(i, 2, out result);
                                             //    return (result==1);
                                             //}


                                             #region Проверка получателя или отправителя Наша организация

                                             // Проверяем контрагентов сделки в спарвочнике ЦБД CODEOTPROV и CODEPOLUCH (код отправителя и получателя)

                                             // Ошибка при заполнении справочников БД
                                             //bool ErrorCodePodr = true;
                                             string CASHPODR = "";
                                             string IDORGBANK = "";

                                             switch (ParametrsBlock.ParametrsImport["Type_OTPR_POLU"].ToUpper())
                                             {

                                                 case "PRIZNAK":

                                                     break;

                                                 case "ZNAK_CHISLA":

                                                    

                                                     for (int indKass = 0; indKass < VarParKASSA_PODR.Length; indKass++)
                                                     {
                                                         string[] VarPar_ = VarParKASSA_PODR[indKass].Split(';');

                                                         //if (Convert.ToBoolean(AppPlayStory_1C.UserconfigWorkSpace["ВключениеРежимаОтладки_настройка_модулей"]))
                                                         //    SystemConecto.ErorDebag("Данные" + VarParKASSA_PODR[indKass].ToString());

                                                         //if (Convert.ToBoolean(AppPlayStory_1C.UserconfigWorkSpace["ВключениеРежимаОтладки_настройка_модулей"]))
                                                         //    SystemConecto.ErorDebag("cell 1 " + custRow["CASH_PODR"].ToString());


                                                         if (VarPar_.Length > 3)
                                                         {
                                                             // VarPar_[0].IndexOf(custRow["CASH_PODR"].ToString())
                                                             if ((VarPar_[3].ToLower().IndexOf("row") > -1 || VarPar_[3].Trim().Length == 0) && custRow["CASH_PODR"].ToString().ToLower().IndexOf(VarPar_[0].ToLower()) > -1)
                                                             {
                                                                 // Проверка кода кассы ;

                                                                 //if (Convert.ToBoolean(AppPlayStory_1C.UserconfigWorkSpace["ВключениеРежимаОтладки_настройка_модулей"]))
                                                                 //    SystemConecto.ErorDebag("row 1");

                                                                 LocalQuery.UserQuery = "select kod from spr_podr where kod=@CASHPODR";


                                                                 LocalQuery.FBParametrsCommandUserQuery.AddWithValue("@CASHPODR", VarPar_[1]);

                                                                 NumberRow = LocalQuery.ExecuteUNINonQuery();
                                                                 // Чистка параметров
                                                                 LocalQuery.FBParametrsCommandUserQuery.Clear();
                                                                 if (NumberRow > 0)
                                                                 {
                                                                     //ErrorCodePodr = false;
                                                                     CASHPODR = VarPar_[1];
                                                                     IDORGBANK = VarPar_.Length > 2 ? VarPar_[2] : "";
                                                                     break;
                                                                 }

                                                             }
                                                             else
                                                             {
                                                                

                                                                 // Счет указан в ячейки
                                                                 if (VarPar_[3].ToLower().IndexOf("cell") > -1)
                                                                 {
                                                                     // Убираем лишнии символы. Не использовал регульрные выражения посчитал бвстрее будет так
                                                                     string[] CellPodr = VarPar_[3].Trim().ToLower().Replace("cell", "").Replace("[", "").Replace("]", "").Split(',');
                                                                     int RowsPodr = 0;
                                                                     int ColumnsPodr = 0;
                                                                     if (CellPodr.Length > 1 && Int32.TryParse(CellPodr[0], out RowsPodr) && Int32.TryParse(CellPodr[1], out ColumnsPodr)
                                                                         && VarPar_[0].IndexOf(ParametrsBlock.TableImData.Rows[RowsPodr][ColumnsPodr].ToString().Trim()) > -1)
                                                                     {

                                                                         // Проверка кода кассы ;

                                                                         LocalQuery.UserQuery = "select kod from spr_podr where kod=@CASHPODR";

                                                                         LocalQuery.FBParametrsCommandUserQuery.AddWithValue("@CASHPODR", VarPar_[1]);

                                                                         NumberRow = LocalQuery.ExecuteUNINonQuery();
                                                                         // Чистка параметров
                                                                         LocalQuery.FBParametrsCommandUserQuery.Clear();
                                                                         if (NumberRow > 0)
                                                                         {
                                                                             //ErrorCodePodr = false;
                                                                             CASHPODR = VarPar_[1];
                                                                             IDORGBANK = VarPar_.Length > 2 ? VarPar_[2] : "";
                                                                             break;
                                                                         }
                                                                     }


                                                                     // Конец определения кода кассы-подразделения
                                                                 }
                                                             }
                                                         }



                                                     }

                                                     break;

                                             }

                                             // Проверка создания организации (пример дополнтельного контроля)
                                             if (CASHPODR.Length == 0)
                                             {
                                                 // Сообщение
                                                 if (Convert.ToBoolean(AppPlayStory_1C.UserconfigWorkSpace["ВключениеРежимаОтладки_настройка_модулей"]))
                                                     SystemConecto.ErorDebag("При обращении к схеме idschema " + i + " файла " + ParametrsBlock.NameFile_ + " ..., возникло исключение: " + Environment.NewLine +
                                                                   " === Код подразделения CASHPODR в таблице не соответствует cхеме" + Environment.NewLine +
                                                                   " === причины:" + Environment.NewLine +
                                                                   "     1. количество сомволов из файла которые обрабатываются (0 - данные остутствуют):" + custRow["CASH_PODR"].ToString().Length, 1);
                                                 continue;
                                             }

                                             #endregion


                                             // Проверка номера документа
                                             if (ColumnDateDoc)
                                             {




                                                 LocalQuery.UserQuery = "Select num from mn_cash where num = @num and dat = @dat and  CASH_PODR=@CASHPODR ";

                                                 LocalQuery.FBParametrsCommandUserQuery.AddWithValue("@num", custRow["NUM_DOC"].ToString().Trim());

                                                 DateTime ConvertDate_ = Convert.ToDateTime(custRow["DATE_DOC"].ToString().Trim());
                                                 LocalQuery.FBParametrsCommandUserQuery.AddWithValue("@dat", ConvertDate_.ToString("d"));

                                                 LocalQuery.FBParametrsCommandUserQuery.AddWithValue("@CASHPODR", CASHPODR);

                                             }
                                             else
                                             {



                                                 switch (ParametrsBlock.ParametrsImport["Type_OTPR_POLU"].ToUpper())
                                                 {

                                                     case "PRIZNAK":

                                                         break;

                                                     case "ZNAK_CHISLA":


                                                         LocalQuery.UserQuery = string.Format("Select num from mn_cash where dat = @dat and {0} = @sum and  CASH_PODR=@CASHPODR ", (Summa > 0 ? pluse_dohod : minus_rashod));


                                                         break;

                                                 }

                                                 LocalQuery.FBParametrsCommandUserQuery.AddWithValue("@sum", (Summa > 0 ? Summa : (Summa * (-1))));
                                                 DateTime ConvertDate_ = Convert.ToDateTime(custRow["DATE_DOC"].ToString().Trim());
                                                 LocalQuery.FBParametrsCommandUserQuery.AddWithValue("@dat", ConvertDate_.ToString("d"));

                                                 LocalQuery.FBParametrsCommandUserQuery.AddWithValue("@CASHPODR", CASHPODR);

                                             }

                                             // 

                                             // Отладка

                                             NumberRow = LocalQuery.ExecuteUNINonQuery();
                                             // Чистка параметров
                                             LocalQuery.FBParametrsCommandUserQuery.Clear();
                                             if (NumberRow > 0)
                                             {
                                                 // запись присутствует проверяеш дальше
                                                 continue;
                                                 // Отладка
                                                 //LocalQuery.UserQuery = "SELECT GEN_ID(gen_num_mn_cash_in, 1) FROM mn_cash"; // "GEN_ID(gen_num_mn_cash_in, 1)";
                                                 // CodeOrg = LocalQuery.ExecuteUNIScalar().Trim();
                                                 // if (CodeOrg.Length < 1)
                                                 // {

                                                 // }

                                             }


                                             // Отсутствует разноска авансового платежа!!!

                                             #region Запуск авто определение ордера с помощью анализа назначения платежа ArticlesPeremechenija
                                             string Code_tp_in_out = "";

                                             // Разбор спец полей (пока интегрированный сервис со временем можно выделить в dll) пример: номер оплаты документа, код договора, код счета клиента
                                             // __________________________________________________________________

                                             bool Organis = false;

                                             //List<string[]> LArticlesPeremechenija = new List<string[]>();
                                             //List<string[]> LArticlesOplataPokupatelja = new List<string[]>();
                                             //List<string[]> LArticles_SOBST_DOHOD = new List<string[]>();
                                             //List<string[]> LArticlesOplataPostavki = new List<string[]>();

                                             foreach (string[] item in LArticlesPeremechenija)
                                             {
                                                 if (item[0].Trim().Length > 0)
                                                 {
                                                     if (item.Length > 1 && custRow["prim"].ToString().ToLower().IndexOf(item[0].Trim().ToLower()) > -1)
                                                     {
                                                         Code_tp_in_out = item[1].Trim();
                                                         break;
                                                     }
                                                 }
                                                 else
                                                 {
                                                     if (custRow["prim"].ToString().Trim().Length == 0)
                                                     {
                                                         Code_tp_in_out = item[1].Trim();
                                                         break;
                                                     }
                                                 }

                                             }
                                             if (Code_tp_in_out.Length == 0)
                                             {
                                                 foreach (string[] item in LArticlesOplataPokupatelja)
                                                 {
                                                     if (item[0].Trim().Length > 0)
                                                     {
                                                         if (item.Length > 1 && custRow["prim"].ToString().ToLower().IndexOf(item[0].Trim().ToLower()) > -1)
                                                         {
                                                             Code_tp_in_out = item[1].Trim();
                                                             Organis = true;
                                                             break;
                                                         }
                                                     }
                                                     else
                                                     {
                                                         if (custRow["prim"].ToString().Trim().Length == 0)
                                                         {
                                                             Code_tp_in_out = item[1].Trim();
                                                             break;
                                                         }
                                                     }
                                                 }

                                             }
                                             if (Code_tp_in_out.Length == 0)
                                             {
                                                 foreach (string[] item in LArticles_SOBST_DOHOD)
                                                 {
                                                     if (item[0].Trim().Length > 0)
                                                     {
                                                         if (item.Length > 1 && custRow["prim"].ToString().ToLower().IndexOf(item[0].Trim().ToLower()) > -1)
                                                         {
                                                             Code_tp_in_out = item[1].Trim();
                                                             break;
                                                         }
                                                     }
                                                     else
                                                     {
                                                         if (custRow["prim"].ToString().Trim().Length == 0)
                                                         {
                                                             Code_tp_in_out = item[1].Trim();
                                                             break;
                                                         }
                                                     }

                                                 }

                                             }
                                             if (Code_tp_in_out.Length == 0)
                                             {
                                                 foreach (string[] item in LArticlesOplataPostavki)
                                                 {
                                                     if (item[0].Trim().Length > 0)
                                                     {
                                                         if (item.Length > 1 && custRow["prim"].ToString().ToLower().IndexOf(item[0].Trim().ToLower()) > -1)
                                                         {
                                                             Code_tp_in_out = item[1].Trim();
                                                             Organis = true;
                                                             break;
                                                         }
                                                     }
                                                     else
                                                     {
                                                         if (custRow["prim"].ToString().Trim().Length == 0)
                                                         {
                                                             Code_tp_in_out = item[1].Trim();
                                                             break;
                                                         }
                                                     }

                                                 }

                                             }
                                             if (Code_tp_in_out.Length == 0)
                                             {
                                                 foreach (string[] item in LArticlesSpisanijaSotrudnik)
                                                 {
                                                     if (item[0].Trim().Length > 0)
                                                     {
                                                         if (item.Length > 1 && custRow["prim"].ToString().ToLower().IndexOf(item[0].Trim().ToLower()) > -1)
                                                         {
                                                             Code_tp_in_out = item[1].Trim();
                                                             break;
                                                         }
                                                     }
                                                     else
                                                     {
                                                         if (custRow["prim"].ToString().Trim().Length == 0)
                                                         {
                                                             Code_tp_in_out = item[1].Trim();
                                                             break;
                                                         }
                                                     }

                                                 }

                                             }
                                             if (Code_tp_in_out.Length == 0)
                                             {
                                                 foreach (string[] item in LArticlesAvansZvit)
                                                 {
                                                     if (item[0].Trim().Length > 0)
                                                     {
                                                         if (item.Length > 1 && custRow["prim"].ToString().ToLower().IndexOf(item[0].Trim().ToLower()) > -1)
                                                         {
                                                             Code_tp_in_out = item[1].Trim();
                                                             //Organis = true;
                                                             break;
                                                         }
                                                     }
                                                     else
                                                     {
                                                         if (custRow["prim"].ToString().Trim().Length == 0)
                                                         {
                                                             Code_tp_in_out = item[1].Trim();
                                                             break;
                                                         }
                                                     }

                                                 }

                                             }
                                             #endregion



                                             #region Определение номера документа и его резерв - Пример рабочий
                                             // Проверка на автоматическое создание номера документа
                                             if (ColumnDateDoc)
                                             {
                                                 LocalQuery.FBParametrsCommandUserQuery.AddWithValue("@num", custRow["NUM_DOC"].ToString().Trim());
                                             }
                                             else
                                             {
                                                 if (Summa > 0)
                                                 {
                                                     // Приход
                                                     LocalQuery.UserQuery = "SELECT GEN_ID(gen_num_mn_cash_in, 1) FROM mn_cash"; // "GEN_ID(gen_num_mn_cash_in, 1)";
                                                     string NumDoc_ = LocalQuery.ExecuteUNIScalar().Trim();
                                                     if (NumDoc_.Length > 0)
                                                     {
                                                         string Pref = "";
                                                         if (VarParPREFICS_DOH_RASH.Length > 0)
                                                         {
                                                             Pref = VarParPREFICS_DOH_RASH[0];
                                                         }
                                                         LocalQuery.FBParametrsCommandUserQuery.AddWithValue("@num", Pref + NumDoc_);
                                                     }
                                                     else
                                                     {
                                                         LocalQuery.FBParametrsCommandUserQuery.AddWithValue("@num", DBNull.Value);

                                                     }
                                                 }
                                                 else
                                                 {
                                                     // Расход
                                                     LocalQuery.UserQuery = "SELECT GEN_ID(gen_num_mn_cash_out, 1) FROM mn_cash"; // "GEN_ID(gen_num_mn_cash_in, 1)";
                                                     string NumDoc_ = LocalQuery.ExecuteUNIScalar().Trim();
                                                     if (NumDoc_.Length > 0)
                                                     {
                                                         string Pref = "";
                                                         if (VarParPREFICS_DOH_RASH.Length > 1)
                                                         {
                                                             Pref = VarParPREFICS_DOH_RASH[1];
                                                         }
                                                         LocalQuery.FBParametrsCommandUserQuery.AddWithValue("@num", Pref + NumDoc_);
                                                     }
                                                     else
                                                     {
                                                         LocalQuery.FBParametrsCommandUserQuery.AddWithValue("@num", DBNull.Value);
                                                     }
                                                 }
                                             }

                                             #endregion


                                             // Дополниетельное примечание
                                             string PrimWriteValue = "";
                                             int NumCol = 0;
                                             foreach (KeyValuePair<string, string> kvp in ParametrsBlock.SchemaTable)
                                             {

                                                 if (NumCol == ParametrsBlock.SchemaTable.Count - 1)
                                                 {
                                                     PrimWriteValue = kvp.Value;
                                                     break;
                                                 }
                                                 NumCol++;
                                             }


                                             // Регистрируем выписку (сохраняем документ) tp_in_out, @tp_in_out,
                                             LocalQuery.UserQuery = string.Format("insert into mn_cash (num, dat, org, SOTR, podr, sum_in, sum_out, CASH_PODR, curr, prim {3})" +
                                              " values (@num, @dat, @IDOrganisation, @SOTRID, @podr, {1}, {2}, '{0}', @curr,  @prim {4}  )",


                                             CASHPODR,
                                             (Summa > 0 ? "@sum_in" : "null"),
                                             (Summa > 0 ? "null" : "@sum_out"),
                                             (Code_tp_in_out.Length > 0 ? ", tp_in_out" : ""),
                                             (Code_tp_in_out.Length > 0 ? ", @tp_in_out" : "")

                                                         );

                                             DateTime ConvertDate = Convert.ToDateTime(custRow["DATE_DOC"].ToString().Trim());
                                             LocalQuery.FBParametrsCommandUserQuery.AddWithValue("@dat", ConvertDate.ToString("d"));



                                             // Не определенная организация - Обработка Плательщик!
                                             if (Organis)
                                             {
                                                 LocalQuery.FBParametrsCommandUserQuery.AddWithValue("@IDOrganisation", ParametrsBlock.ParametrsImport["IDGroupOrgani"] + 1);
                                                 LocalQuery.FBParametrsCommandUserQuery.AddWithValue("@SOTRID", DBNull.Value);
                                             }
                                             else
                                             {
                                                 LocalQuery.FBParametrsCommandUserQuery.AddWithValue("@SOTRID", ParametrsBlock.ParametrsImport["CashUSERID"]);
                                                 LocalQuery.FBParametrsCommandUserQuery.AddWithValue("@IDOrganisation", DBNull.Value);
                                             }


                                             if (Code_tp_in_out.Length > 0)
                                             {
                                                 LocalQuery.FBParametrsCommandUserQuery.AddWithValue("@tp_in_out", Code_tp_in_out);
                                             }
                                             else
                                             {
                                                 LocalQuery.FBParametrsCommandUserQuery.AddWithValue("@tp_in_out", DBNull.Value);
                                             }

                                             // проверка вверху RowDataTableUSER
                                             LocalQuery.FBParametrsCommandUserQuery.AddWithValue("@podr", RowDataTableUSER[2]);

                                             if (Summa > 0)
                                             {
                                                LocalQuery.FBParametrsCommandUserQuery.AddWithValue("@sum_in", Summa);
                                             }
                                             else
                                             {
                                                LocalQuery.FBParametrsCommandUserQuery.AddWithValue("@sum_out", (Summa * (-1)));
                                             }
                                             
                                             



                                             LocalQuery.FBParametrsCommandUserQuery.AddWithValue("@curr", ParametrsBlock.SchemaTable.ContainsKey("NoName_10") ?
                                                 ParametrsBlock.SchemaTable["NoName_10"].Length > 0 ? ParametrsBlock.SchemaTable["NoName_10"] : ParametrsBlock.SchemaCur[ParametrsBlock.ParametrsImport["CurrDef"]] :
                                                 ParametrsBlock.SchemaCur.ContainsKey(custRow["curr"].ToString().Trim()) ? ParametrsBlock.SchemaCur[custRow["curr"].ToString().Trim()] : ParametrsBlock.SchemaCur[ParametrsBlock.ParametrsImport["CurrDef"]]);

                                             //var Val_ = LocalQuery.FBParametrsCommandUserQuery["@curr"].Value;

                                             //var ghy = Val_;

                                             LocalQuery.FBParametrsCommandUserQuery.AddWithValue("@prim", ParametrsBlock.SchemaTable.ContainsKey("NoName_11") ? ParametrsBlock.SchemaTable["NoName_11"] : string.Format("{0}   {1} {2}.{3}.{4}", custRow["prim"].ToString().Trim(), PrimWriteValue, DateTime.Now.Day, DateTime.Now.Month, DateTime.Now.Year)); // string.Format("{0}   {1}", custRow["prim"].ToString().Trim(), PrimWriteValue)

                                             //LocalQuery.FBParametrsCommandUserQuery[7].


                                             // Создать ордер
                                             NumberRow = LocalQuery.ExecuteUNINonQuery();


                                             // Расчет Банковских услуг
                                             //VarParBANK_USLUGA
                                             if (VarParBANK_USLUGA.Length > 0)
                                             {
                                                 switch (VarParBANK_USLUGA[0].ToUpper())
                                                 {

                                                     case "COLUMN_SUMMA":

                                                         break;

                                                     case "RAZNICA":


                                                         if (BankPosluga)
                                                         {

                                                             // !!! Перед созданием ордера обязательно перепроверить его на наличие в проверку добавить 35 символов из комментария !!! 


                                                             // System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US"); // - ToDouble с точкой
                                                             double Raznica_ = 0;
                                                             if (Double.TryParse(custRow["bank_posluga"].ToString().Replace(ParametrsBlock.ParametrsImport["РазделительСуммы"].Trim().ToString(), ","), out NumberSumma))
                                                             {

                                                                 //double BankPosluga_ = Convert.ToDouble(custRow["bank_posluga"].ToString().Replace(ParametrsBlock.ParametrsImport["РазделительСуммы"].Trim().ToString(), ",")); //
                                                                 // Raznica_ = NumberSumma - (Summa < 0 ? (Summa * (-1)) : Summa);
                                                                 Raznica_ = Math.Round( (NumberSumma < 0 ? (NumberSumma * (-1)) : NumberSumma) - (Summa < 0 ? (Summa * (-1)) : Summa) , 2, MidpointRounding.AwayFromZero );

                                                                 if (NumberSumma == 0 || Raznica_ == 0)
                                                                 {
                                                                     // Чистка параметров
                                                                     LocalQuery.FBParametrsCommandUserQuery.Clear();
                                                                     continue;
                                                                 }

                                                             }
                                                             else
                                                             {
                                                                 // Это не число запись неверная
                                                                 // LocalQuery.FBParametrsCommandUserQuery.Clear();
                                                                 continue;
                                                             }

                                                             // SystemConecto.ErorDebag("Банковская услуга: разница " + Raznica_.ToString() + " номер ордера " + LocalQuery.FBParametrsCommandUserQuery["@num"].Value.ToString());
                                                             // continue;


                                                             if (Summa > 0)
                                                             {
                                                                 LocalQuery.FBParametrsCommandUserQuery.AddWithValue("@sum_out", Raznica_);
                                                             }
                                                             else
                                                             {
                                                                 // LocalQuery.FBParametrsCommandUserQuery.AddWithValue("@sum_out", (Summa * (-1)));
                                                                 LocalQuery.FBParametrsCommandUserQuery["@sum_out"].Value = Raznica_;
                                                             }


                                                             


                                                             #region Определение номера документа и его резерв - Пример рабочий однако не задействован
                                                             // Проверка на автоматическое создание номера документа
                                                             //if (ColumnDateDoc)
                                                             //{
                                                             //    LocalQuery.FBParametrsCommandUserQuery.AddWithValue("@num", custRow["NUM_DOC"].ToString().Trim());
                                                             //}
                                                             //else
                                                             //{
                                                             string NumberOrderBankUsluga = LocalQuery.FBParametrsCommandUserQuery["@num"].Value.ToString();

                                                             // Расход
                                                             LocalQuery.UserQuery = "SELECT GEN_ID(gen_num_mn_cash_out, 1) FROM mn_cash"; // "GEN_ID(gen_num_mn_cash_in, 1)";
                                                             string NumDoc_ = LocalQuery.ExecuteUNIScalar().Trim();
                                                             if (NumDoc_.Length > 0)
                                                             {
                                                                 string Pref = "";
                                                                 if (VarParPREFICS_DOH_RASH.Length > 1)
                                                                 {
                                                                     Pref = VarParPREFICS_DOH_RASH[1];
                                                                 }
                                                                 //LocalQuery.FBParametrsCommandUserQuery.AddWithValue("@num", Pref + NumDoc_);
                                                                 LocalQuery.FBParametrsCommandUserQuery["@num"].Value = Pref + NumDoc_;
                                                             }
                                                             else
                                                             {
                                                                 //LocalQuery.FBParametrsCommandUserQuery.AddWithValue("@num", DBNull.Value);
                                                                 LocalQuery.FBParametrsCommandUserQuery["@num"].Value = DBNull.Value;
                                                             }

                                                             //}

                                                             #endregion

                                                             // Движение статья
                                                             Code_tp_in_out = VarParBANK_USLUGA.Length > 1 ? VarParBANK_USLUGA[1] : "";
                                                             if (Code_tp_in_out.Length > 0)
                                                             {
                                                                 LocalQuery.FBParametrsCommandUserQuery["@tp_in_out"].Value = Code_tp_in_out;
                                                             }


                                                             // Не определенная организация - Обработка Плательщик!
                                                             if (Organis)
                                                             {
                                                                 LocalQuery.FBParametrsCommandUserQuery["@IDOrganisation"].Value = IDORGBANK.Length > 0 ? IDORGBANK : ParametrsBlock.ParametrsImport["IDGroupOrgani"] + 1;
                                                                 LocalQuery.FBParametrsCommandUserQuery["@SOTRID"].Value = DBNull.Value;

                                                             }
                                                             else
                                                             {
                                                                 LocalQuery.FBParametrsCommandUserQuery["@SOTRID"].Value = ParametrsBlock.ParametrsImport["CashUSERID"];
                                                                 LocalQuery.FBParametrsCommandUserQuery["@IDOrganisation"].Value = DBNull.Value;
                                                             }



                                                            LocalQuery.FBParametrsCommandUserQuery["@prim"].Value = string.Format("{0} {1}   {2} {3}.{4}.{5}", 
                                                                 (VarParBANK_USLUGA.Length > 2 ? VarParBANK_USLUGA[2] + "  " : ""), 
                                                                 "по ордеру №" + NumberOrderBankUsluga, 
                                                                 PrimWriteValue, 
                                                                 DateTime.Now.Day, DateTime.Now.Month, DateTime.Now.Year) ; 


                                                             //LocalQuery.FBParametrsCommandUserQuery["@prim"].Value = string.Format("{0} {1}",
                                                             //    (VarParBANK_USLUGA.Length > 2 ? VarParBANK_USLUGA[2] + "  " : ""), "по ордеру №" + NumberOrderBankUsluga); //, LocalQuery.FBParametrsCommandUserQuery["@prim"].Value


                                                             LocalQuery.UserQuery = string.Format("insert into mn_cash (num, dat, org, SOTR, podr, sum_in, sum_out, CASH_PODR, curr, prim {3})" +
                                                                " values (@num, @dat, @IDOrganisation, @SOTRID, @podr, {1}, {2}, '{0}', @curr,  @prim {4} )",


                                                               CASHPODR,
                                                                "null",
                                                               "@sum_out",
                                                                  (Code_tp_in_out.Length > 0 ? ", tp_in_out" : ""),
                                                                  (Code_tp_in_out.Length > 0 ? ", @tp_in_out" : "")

                                                                           );


                                                             // Создать ордер
                                                             NumberRow = LocalQuery.ExecuteUNINonQuery();
                                                         }

                                                         break;

                                                 }
                                             }

                                             // Чистка параметров
                                             LocalQuery.FBParametrsCommandUserQuery.Clear();

                                             ///< insert into mn_cash (num,dat,org,SOTR,sum_in,sum_out,tp_in_out,CASH_PODR,curr,prim)
                                             /// values ('112222','06.05.2014','13304',null,800,0,'010003','05101',1,'AUTOORDER'||' '||DATETOSTR('today')); >




                                         }

                                     }
                                     #endregion

                                     #region bank
                                     // --- Чтение параметров
                                     if (TypeSchem[0] == "bank")
                                     {







                                         // перебор зиписей
                                         foreach (DataRow custRow in ParametrsBlock.TableImData.Rows)
                                         {

                                             int NumberRow = 0;

                                             // Проверяем контрагентов сделки в спарвочнике ЦБД CODEOTPROV и CODEPOLUCH (код отправителя и получателя)
                                             string CODEOTPROV_POLENAM = "";
                                             string NAME_OTPROV_POLENAM = "";
                                             string CODE_BANK_OTPROV_POLENAM = "";
                                             string NAMEBANK_OTPROV_POLENAM = "";
                                             string RR_OTPROV_POLENAM = "";
                                             string CODE_ORG_OTPROV = "";

                                             string CODEPOLUCH_POLENAM = "";
                                             string NAME_POLUCH_POLENAM = "";
                                             string CODE_BANK_POLUCH_POLENAM = "";
                                             string NAMEBANK_POLUCH_POLENAM = "";
                                             string RR_POLUCH_POLENAM = "";
                                             string CODE_ORG_POLUCH = "";



                                             switch (ParametrsBlock.ParametrsImport["Type_OTPR_POLU"].ToUpper())
                                             {

                                                 case "PRIZNAK":
                                                     // Формируем поля по признаку (отправитель всегда мы - по умолчанию)
                                                     string PR_OTPR = custRow["Type_OTPR_POLU"].ToString();

                                                     // Коды получателя и отправителя определены по умолчанию
                                                     string[] OTPPOLID = new string[2] { "1", "2" };

                                                     if (VarParOTPO.Length > 0)
                                                     {
                                                         string[] VarParOTPO_ = VarParOTPO[0].Split(';');
                                                         if (VarParOTPO_.Length > 1)
                                                         {
                                                             OTPPOLID = VarParOTPO_;
                                                         }
                                                     }
                                                     //else{
                                                     //       // По умолчанию

                                                     //}
                                                     // Поля меняются местами

                                                     if (OTPPOLID[0] == PR_OTPR.Trim().ToUpper())
                                                     {
                                                         //1-отправленный платеж от нас 

                                                         CODEOTPROV_POLENAM = "CODE_OTPROV";
                                                         NAME_OTPROV_POLENAM = "NAME_OTPR";
                                                         CODE_BANK_OTPROV_POLENAM = "CODE_BANK_OTPROV";
                                                         NAMEBANK_OTPROV_POLENAM = "NAMEBANK_OTPR";
                                                         RR_OTPROV_POLENAM = "RR_OTPROV";

                                                         CODEPOLUCH_POLENAM = "CODE_POLUCH";
                                                         NAME_POLUCH_POLENAM = "NAME_POLUCH";
                                                         CODE_BANK_POLUCH_POLENAM = "CODE_BANK_POLUCH";
                                                         NAMEBANK_POLUCH_POLENAM = "NAMEBANK_POLUCH";
                                                         RR_POLUCH_POLENAM = "RR_POLUCH";

                                                     }
                                                     else
                                                     {
                                                         if (OTPPOLID[1] == PR_OTPR.Trim().ToUpper())
                                                         {
                                                             //2-полученный платеж нами

                                                             CODEOTPROV_POLENAM = "CODE_POLUCH";
                                                             NAME_OTPROV_POLENAM = "NAME_POLUCH";
                                                             CODE_BANK_OTPROV_POLENAM = "CODE_BANK_POLUCH";
                                                             NAMEBANK_OTPROV_POLENAM = "NAMEBANK_POLUCH";
                                                             RR_OTPROV_POLENAM = "RR_POLUCH";

                                                             CODEPOLUCH_POLENAM = "CODE_OTPROV";
                                                             NAME_POLUCH_POLENAM = "NAME_OTPR";
                                                             CODE_BANK_POLUCH_POLENAM = "CODE_BANK_OTPROV";
                                                             NAMEBANK_POLUCH_POLENAM = "NAMEBANK_OTPR";
                                                             RR_POLUCH_POLENAM = "RR_OTPROV";

                                                         }
                                                     }




                                                     break;

                                                 case "COLUM_NAME":
                                                     // Будет перезаписана таблица импорта
                                                     CODEOTPROV_POLENAM = "CODE_OTPROV";
                                                     NAME_OTPROV_POLENAM = "NAME_OTPR";
                                                     CODE_BANK_OTPROV_POLENAM = "CODE_BANK_OTPROV";
                                                     NAMEBANK_OTPROV_POLENAM = "NAMEBANK_OTPR";
                                                     RR_OTPROV_POLENAM = "RR_OTPROV";

                                                     CODEPOLUCH_POLENAM = "CODE_POLUCH";
                                                     NAME_POLUCH_POLENAM = "NAME_POLUCH";
                                                     CODE_BANK_POLUCH_POLENAM = "CODE_BANK_POLUCH";
                                                     NAMEBANK_POLUCH_POLENAM = "NAMEBANK_POLUCH";
                                                     RR_POLUCH_POLENAM = "RR_POLUCH";
                                                     break;

                                                 case "ZNAK_CHISLA":
                                                     // Формируем поля по знаку
                                                     SystemConecto.ErorDebag("При обращении к схеме idschema " + i + " файла " + ParametrsBlock.NameFile_ + " ..., возникло исключение: " + Environment.NewLine +
                                                               " === Схема не поддерживает определение плательщика с помощью ZNAK_CHISLA, обратитесь к разработчикам.ы", 1);
                                                     return;

                                                     //break;
                                             }

                                             // Проверка полученных данных
                                             if (CODEOTPROV_POLENAM.Length < 1 || CODEPOLUCH_POLENAM.Length < 1)
                                             {

                                                 // Неверная схема продолжить
                                                 SystemConecto.ErorDebag("При обращении к схеме idschema " + i + " файла " + ParametrsBlock.NameFile_ + " ..., возникло исключение: " + Environment.NewLine +
                                                               " === Схема не содержит, полей получятеля или отправителя ", 1);
                                                 continue;
                                             }

                                             // Ошибка при заполнении справочников БД
                                             bool ErrorInsertData = false;
                                            
                                             // Название подразделения отправителя и получателя
                                             string NameCont = custRow[NAME_OTPROV_POLENAM].ToString().Replace("\"", "\u0022"); //"'"
                                             // Отправитель не использовал свой расчетный счет
                                             bool OrgNoBank = true;
                                             string CodeBank = "";


                                             if (NameCont.ToLower().IndexOf("банк".ToLower()) > -1)
                                             {
                                                 // Организация определена 
                                                 // Проверка спец Организации "Неопределенная организация" ее код ParametrsBlock.ParametrsImport["IDGroupOrgani"] + "1"

                                                 #region Банк 80%
                                                 //CodeBank = CodeOrg;

                                                 // CodeOrg = ParametrsBlock.ParametrsImport["IDGroupOrgani"] + "1";
                                                 OrgNoBank = false;

                                                 #endregion
                                             }
                                             //else
                                             //{
                                             //    #region Новая организация 20%  это банк 

                                             //    #endregion
                                             //}



                                             // Проверка получателя и отправителя можно кешировать код проверенной организации
                                             for (int iPriznak = 0; iPriznak < 2; iPriznak++)
                                             {
                                                 // Проверить поля в таблице!!!



                                                 LocalQuery.UserQuery = string.Format("select kod from spr_org  where OKPO = '{0}' ", custRow[(iPriznak == 0 ? CODEOTPROV_POLENAM : CODEPOLUCH_POLENAM)].ToString());
                                                 string CodeOrg = LocalQuery.ExecuteUNIScalar().Trim();
                                                 if (CodeOrg.Length < 1)
                                                 {
                                                     // запись Отсутствует добавить в справочник
                                                     // Проверить ОКПО плательщика отправителя
                                                     // если ОКПО банка то плательщик не определен (проверка в справочнике банка можно только по мфо)
                                                     // используем поиск слова Банк в названии отправителя 80% попадания
                                                     string ResultID = "";



                                                         // Проверка группы товаров для новых созданных контрагентов  происходит выше в общих параметрах
                                                         //LocalQuery.UserQuery = string.Format("select kod from SPR_ORG_GROUPS  where kod = '{0}' ", ParametrsBlock.ParametrsImport["IDGroupOrgani"].ToString());
                                                         //NumberRow = LocalQuery.ExecuteUNINonQuery();
                                                         //if (NumberRow < 1)
                                                         //{
                                                         //    // запись Отсутствует добавить в справочник
                                                         //    LocalQuery.UserQuery = string.Format("insert into SPR_ORG_GROUPS (kod,name,PARENTID) VALUES ('{0}','{1}',0)", ParametrsBlock.ParametrsImport["IDGroupOrgani"].ToString(),
                                                         //        ParametrsBlock.ParametrsImport["NameGroupOrgani"].ToString());
                                                         //    NumberRow = LocalQuery.ExecuteUNINonQuery();

                                                         //    //kod -  код может любой но уникальный например можно договориться как в примере 888 сделать
                                                         //    //name -  понятно название
                                                         //    //PARENTID -  родитель если эта група вложенная, если 0 то это корневая группа

                                                         //}

                                                         // Добовляем организацию не  COALESCE(kod,0)+1 as kod
                                                         LocalQuery.UserQuery = string.Format("select first 1 kod from  spr_org where SUBSTRING(kod from 1 for {0})= '{1}' order by cast(kod as integer) desc", ParametrsBlock.ParametrsImport["IDGroupOrgani"].Length.ToString(),
                                                             ParametrsBlock.ParametrsImport["IDGroupOrgani"].ToString());
                                                         // Получаем код организации
                                                         ResultID = LocalQuery.ExecuteUNIScalar().Trim();

                                                         if (ResultID.Length < 1)
                                                         {
                                                             // Код ParametrsBlock.ParametrsImport["IDGroupOrgani"] + "1" - зарезервирован
                                                             ResultID = ParametrsBlock.ParametrsImport["IDGroupOrgani"] + "2";
                                                         }
                                                         else
                                                         {
                                                             var CountID = ResultID.Substring(ParametrsBlock.ParametrsImport["IDGroupOrgani"].Length);
                                                             ResultID = ParametrsBlock.ParametrsImport["IDGroupOrgani"] + (Convert.ToInt32(CountID) + 1).ToString();

                                                         }

                                                         // Отладка
                                                         //var er = ResultID;


                                                         // запись организации в справочник
                                                         LocalQuery.UserQuery = string.Format("insert into spr_org (kod,name,GRP,addr,tel,okpo,prim,FULL_NAME)" +
                                                         "values ({0}, @name, '{1}', '','',@okpo,'',@nameJUR)", ResultID, ParametrsBlock.ParametrsImport["IDGroupOrgani"].ToString());

                                                         LocalQuery.FBParametrsCommandUserQuery.AddWithValue("@name", NameCont);
                                                         LocalQuery.FBParametrsCommandUserQuery.AddWithValue("@nameJUR", custRow[(iPriznak == 0 ? NAME_OTPROV_POLENAM : NAME_POLUCH_POLENAM)].ToString());
                                                         LocalQuery.FBParametrsCommandUserQuery.AddWithValue("@okpo", custRow[(iPriznak == 0 ? CODEOTPROV_POLENAM : CODEPOLUCH_POLENAM)].ToString());
                                                         NumberRow = LocalQuery.ExecuteUNINonQuery();

                                                         // Проверка создания организации (пример дополнтельного контроля)
                                                         if (LocalQuery.ErrorBD.Code != 0)
                                                         {
                                                             ErrorInsertData = true;

                                                         }

                                                         //insert into spr_org (kod,name,GRP,addr,tel,okpo,prim,FULL_NAME[ старое поле REDUCEDNAME]) values (:KOD_ORG, 'название организации', '888', 'adress','0681245640','31810516','primichanie','название правильное организации');
                                                         //:KOD_ORG  - берется с прыдущего запроса уникальный код организации
                                                         //name - название организации
                                                         //GRP - группа где будет лежать данная организациия в нашем случае в группе 888
                                                         //addr -адресс организации (можно не заполнять)
                                                         //tel - телефон (можно не заполнять)
                                                         //okpo - окпо уникальный код организации
                                                         //prim - примичание например (авто всатвка с клиент банка)
                                                         //REDUCEDNAME - название для документов организации поленое (можно не заполнять, будет браться с поля org)

                                                     



                                                     // Код организации
                                                     //if (iPriznak == 0)
                                                     //{
                                                     //    CODE_ORG_OTPROV = ResultID;
                                                     //}
                                                     //else
                                                     //{
                                                     //    CODE_ORG_POLUCH = ResultID;
                                                     //}

                                                     // Новая организация
                                                     CodeOrg = ResultID;

                                                     // =========== Добовляем расчетный счет
                                                     #region  Проверка банка и Расчетный счет
                                                     //// ---- Проверка банка по МВФ

                                                     //LocalQuery.UserQuery = string.Format("select kod from spr_bank where kod={0}", custRow[(iPriznak == 0 ? CODE_BANK_OTPROV_POLENAM : CODE_BANK_POLUCH_POLENAM)].ToString());
                                                     //string ResultIDMFO = LocalQuery.ExecuteUNIScalar().Trim();

                                                     //if (ResultIDMFO.Length < 1)
                                                     //{
                                                     //    // Создать МФО
                                                     //    LocalQuery.UserQuery = string.Format("insert into spr_bank (kod,name) VALUES ({0}, {1})",
                                                     //        custRow[(iPriznak == 0 ? CODE_BANK_OTPROV_POLENAM : CODE_BANK_POLUCH_POLENAM)].ToString(),
                                                     //        custRow[(iPriznak == 0 ? NAMEBANK_OTPROV_POLENAM : NAMEBANK_POLUCH_POLENAM)].ToString());
                                                     //    NumberRow = LocalQuery.ExecuteUNINonQuery();
                                                     //    //где
                                                     //    //kod -это мфо уникальное
                                                     //    //name -  это название нового банка
                                                     //}



                                                     //// --- Расчетный счет / проверка валюты !!!! а иначе выбрать основную валюту

                                                     //LocalQuery.UserQuery = string.Format("select mfo, rs from SPR_ORG_RS where org={3} and mfo='{0}' and rs='{1}'  and curr={2}",
                                                     //    custRow[(iPriznak == 0 ? CODE_BANK_OTPROV_POLENAM : CODE_BANK_POLUCH_POLENAM)].ToString(),
                                                     //    custRow[(iPriznak == 0 ? RR_OTPROV_POLENAM : RR_POLUCH_POLENAM)].ToString(),
                                                     //    ParametrsBlock.SchemaCur.ContainsKey(custRow["curr"].ToString().Trim()) ? ParametrsBlock.SchemaCur[custRow["curr"].ToString().Trim()] : ParametrsBlock.SchemaCur[ParametrsBlock.ParametrsImport["CurrDef"]],
                                                     //    ResultID);
                                                     //string ResultIDRR = LocalQuery.ExecuteUNIScalar().Trim();
                                                     //if (ResultIDRR.Length < 1)
                                                     //{
                                                     //    LocalQuery.UserQuery = string.Format(" insert into SPR_ORG_RS (org,mfo,rs,main,curr) values ('{0}', '{1}','{2}', 0, {3})",
                                                     //        ResultID, custRow[(iPriznak == 0 ? CODE_BANK_OTPROV_POLENAM : CODE_BANK_POLUCH_POLENAM)].ToString(),
                                                     //        custRow[(iPriznak == 0 ? RR_OTPROV_POLENAM : RR_POLUCH_POLENAM)].ToString(),
                                                     //        ParametrsBlock.SchemaCur.ContainsKey(custRow["curr"].ToString().Trim()) ? ParametrsBlock.SchemaCur[custRow["curr"].ToString().Trim()] : ParametrsBlock.SchemaCur[ParametrsBlock.ParametrsImport["CurrDef"]]);
                                                     //    NumberRow = LocalQuery.ExecuteUNINonQuery();

                                                     //    //org - :KOD_ORG -  код организации для которйо делаем вставку с первого запроса берем
                                                     //    //mfo - мфо банка
                                                     //    //rs - расчетный счет контрагента
                                                     //    //main - если 1 то это означает что это основной счет
                                                     //    //curr -  валюта счета 1 это грвиян
                                                     //}
                                                     #endregion

                                                 }
                                               


                                                 // Код организации уже определен
                                                 if (iPriznak == 0)
                                                 {
                                                     if (OrgNoBank)
                                                     {
                                                         CODE_ORG_OTPROV = CodeOrg;
                                                     }
                                                     else
                                                     {
                                                         CODE_ORG_OTPROV = ParametrsBlock.ParametrsImport["IDGroupOrgani"] + "1";
                                                         CodeBank = CodeOrg; // CodeOrg = 

                                                     }
                                                     
                                                 }
                                                 else
                                                 {
                                                     CODE_ORG_POLUCH = CodeOrg;
                                                 }

                                                 // Проверка расчетного счета. Добовляем расчетный счет если он отсутствует

                                                 #region  Проверка банка и Расчетный счет
                                                 // ---- Проверка банка

                                                 LocalQuery.UserQuery = string.Format("select kod from spr_bank where kod={0}", custRow[(iPriznak == 0 ? CODE_BANK_OTPROV_POLENAM : CODE_BANK_POLUCH_POLENAM)].ToString());
                                                 string ResultIDMFO = LocalQuery.ExecuteUNIScalar().Trim();

                                                 if (ResultIDMFO.Length < 1)
                                                 {
                                                     // Создать МФО
                                                     LocalQuery.UserQuery = string.Format("insert into spr_bank (kod,name) VALUES ({0}, '{1}')",
                                                         custRow[(iPriznak == 0 ? CODE_BANK_OTPROV_POLENAM : CODE_BANK_POLUCH_POLENAM)].ToString(),
                                                         custRow[(iPriznak == 0 ? NAMEBANK_OTPROV_POLENAM : NAMEBANK_POLUCH_POLENAM)].ToString().Replace("\"", "\u0022"));
                                                     NumberRow = LocalQuery.ExecuteUNINonQuery();
                                                     //где
                                                     //kod -это мфо уникальное
                                                     //name -  это название нового банка
                                                 }



                                                 // --- Расчетный счет / проверка валюты !!!! mfo, rs


                                                LocalQuery.UserQuery = string.Format("select org from SPR_ORG_RS where mfo='{0}' and rs='{1}' and curr={2}", //org={3} and // исключил если в БД есть уже этот счет 
                                                custRow[(iPriznak == 0 ? CODE_BANK_OTPROV_POLENAM : CODE_BANK_POLUCH_POLENAM)].ToString(),
                                                custRow[(iPriznak == 0 ? RR_OTPROV_POLENAM : RR_POLUCH_POLENAM)].ToString(),
                                                ParametrsBlock.SchemaCur.ContainsKey(custRow["curr"].ToString().Trim()) ? ParametrsBlock.SchemaCur[custRow["curr"].ToString().Trim()] : ParametrsBlock.SchemaCur[ParametrsBlock.ParametrsImport["CurrDef"]]
                                                //, CodeOrg
                                                );
                                                    string ResultIDRR = LocalQuery.ExecuteUNIScalar().Trim();
                                                    if (ResultIDRR.Length < 1)
                                                    {
                                                        // Реакция если отправитель отправил деньги не со своего расчетного счета Отправитель Банк
                                                        if (OrgNoBank || iPriznak > 0)
                                                        {
                                                            LocalQuery.UserQuery = string.Format(" insert into SPR_ORG_RS (org,mfo,rs,main,curr) values ('{0}', '{1}','{2}', 0, {3})",
                                                                CodeOrg, custRow[(iPriznak == 0 ? CODE_BANK_OTPROV_POLENAM : CODE_BANK_POLUCH_POLENAM)].ToString(),
                                                                custRow[(iPriznak == 0 ? RR_OTPROV_POLENAM : RR_POLUCH_POLENAM)].ToString(),
                                                                ParametrsBlock.SchemaCur.ContainsKey(custRow["curr"].ToString().Trim()) ? ParametrsBlock.SchemaCur[custRow["curr"].ToString().Trim()] : ParametrsBlock.SchemaCur[ParametrsBlock.ParametrsImport["CurrDef"]]);
                                                        }
                                                        else
                                                        {
                                                            LocalQuery.UserQuery = string.Format(" insert into SPR_ORG_RS (org,mfo,rs,main,curr) values ('{0}', '{1}','{2}', 0, {3})",
                                                                CodeBank, custRow[(iPriznak == 0 ? CODE_BANK_OTPROV_POLENAM : CODE_BANK_POLUCH_POLENAM)].ToString(),
                                                                custRow[(iPriznak == 0 ? RR_OTPROV_POLENAM : RR_POLUCH_POLENAM)].ToString(),
                                                                ParametrsBlock.SchemaCur.ContainsKey(custRow["curr"].ToString().Trim()) ? ParametrsBlock.SchemaCur[custRow["curr"].ToString().Trim()] : ParametrsBlock.SchemaCur[ParametrsBlock.ParametrsImport["CurrDef"]]);
                                                        }
                                                        

                                                        NumberRow = LocalQuery.ExecuteUNINonQuery();

                                                        //org - :KOD_ORG -  код организации для которйо делаем вставку с первого запроса берем
                                                        //mfo - мфо банка
                                                        //rs - расчетный счет контрагента
                                                        //main - если 1 то это означает что это основной счет
                                                        //curr -  валюта счета 1 это грвня
                                                    }


                                                 
                                                
                                                 #endregion


                                             }
                                             // for end

                                             // ParametrsBlock.SchemaTable


                                             // Проверка создания организации (пример дополнтельного контроля)
                                             if (ErrorInsertData)
                                             {
                                                 // Неверный запрос игнорирование ордера
                                                 SystemConecto.ErorDebag("Во время записи данных в справочник " + " ..., возникло исключение: " + Environment.NewLine +
                                                               string.Format(" === Обратитесь к администратору. Ордер № {0} от {1} не внесен в БД", custRow["NUM_DOC"].ToString().Trim(), custRow["DATE_DOC"].ToString().Trim()), 1);


                                                 continue;

                                             }


                                             // Ищем документ в ЦБД (центральная база данных) по полю NUM_DOC и DATE_DOC, а также получателе и отправителе

                                             LocalQuery.UserQuery = "Select num from mn_bank where num = @num and dat = @dat and org = @org and org_to = @org_to"; // , );

                                             LocalQuery.FBParametrsCommandUserQuery.AddWithValue("@num", custRow["NUM_DOC"].ToString().Trim());

                                             // Проверка успешности конвертации даты
                                             DateTime dateValue_;
                                             DateTime ConvertDate = new DateTime();
                                             if (DateTime.TryParse(custRow["DATE_DOC"].ToString().Trim(), out dateValue_))
                                             {
                                                 ConvertDate = Convert.ToDateTime(custRow["DATE_DOC"].ToString().Trim());
                                             }
                                             else
                                             {
                                                 // Определяем псевдо форматы год месяц дата => день месяц год
                                                 if (custRow["DATE_DOC"].ToString().Trim().Length == 8)
                                                 {
                                                     string strar =  custRow["DATE_DOC"].ToString().Trim();
                                                     ConvertDate = Convert.ToDateTime(strar.Substring(6, 2) + "/" + strar.Substring(4, 2) + "/" + strar.Substring(0, 4));
                                                 }
                                             }

                                             var DebagDate = ConvertDate.ToString("d") ;

                                             LocalQuery.FBParametrsCommandUserQuery.AddWithValue("@dat", DebagDate);
                                             //LocalQuery.FBParametrsCommandUserQuery.AddWithValue("@dat", custRow["DATE_DOC"].ToString().Trim());

                                             LocalQuery.FBParametrsCommandUserQuery.AddWithValue("@org", CODE_ORG_OTPROV);
                                             LocalQuery.FBParametrsCommandUserQuery.AddWithValue("@org_to", CODE_ORG_POLUCH);

                                             // Отладка
                                             // DataTable Test_ = LocalQuery.ExecuteQueryLoadTable("Test");

                                             // Test_.TableName = "Debag";

                                             NumberRow = LocalQuery.ExecuteUNINonQuery();
                                             // Чистка параметров
                                             LocalQuery.FBParametrsCommandUserQuery.Clear();
                                             
                                             if (NumberRow > 0)
                                             {
                                                 // запись присутствует проверяеш дальше
                                                 continue;
                                             }




                                             #region Запуск авто определение ордера с помощью анализа назначения платежа ArticlesPeremechenija
                                             string Code_tp_in_out = "";

                                             // Разбор спец полей (пока интегрированный сервис со временем можно выделить в dll) пример: номер оплаты документа, код договора, код счета клиента
                                             // __________________________________________________________________

                                             bool Organis = false;

                                             //List<string[]> LArticlesPeremechenija = new List<string[]>();
                                             //List<string[]> LArticlesOplataPokupatelja = new List<string[]>();
                                             //List<string[]> LArticles_SOBST_DOHOD = new List<string[]>();
                                             //List<string[]> LArticlesOplataPostavki = new List<string[]>();

                                             foreach (string[] item in LArticlesPeremechenija)
                                             {
                                                 if (item[0].Trim().Length > 0)
                                                 {
                                                     if (item.Length > 1 && custRow["prim"].ToString().ToLower().IndexOf(item[0].Trim().ToLower()) > -1)
                                                     {
                                                         Code_tp_in_out = item[1].Trim();
                                                         break;
                                                     }
                                                 }
                                                 else
                                                 {
                                                     if (custRow["prim"].ToString().Trim().Length == 0)
                                                     {
                                                         Code_tp_in_out = item[1].Trim();
                                                         break;
                                                     }
                                                 }

                                             }
                                             if (Code_tp_in_out.Length == 0)
                                             {
                                                 foreach (string[] item in LArticlesOplataPokupatelja)
                                                 {
                                                     if (item[0].Trim().Length > 0)
                                                     {
                                                         if (item.Length > 1 && custRow["prim"].ToString().ToLower().IndexOf(item[0].Trim().ToLower()) > -1)
                                                         {
                                                             Code_tp_in_out = item[1].Trim();
                                                             Organis = true;
                                                             break;
                                                         }
                                                     }
                                                     else
                                                     {
                                                         if (custRow["prim"].ToString().Trim().Length == 0)
                                                         {
                                                             Code_tp_in_out = item[1].Trim();
                                                             break;
                                                         }
                                                     }
                                                 }

                                             }
                                             if (Code_tp_in_out.Length == 0)
                                             {
                                                 foreach (string[] item in LArticles_SOBST_DOHOD)
                                                 {
                                                     if (item[0].Trim().Length > 0)
                                                     {
                                                         if (item.Length > 1 && custRow["prim"].ToString().ToLower().IndexOf(item[0].Trim().ToLower()) > -1)
                                                         {
                                                             Code_tp_in_out = item[1].Trim();
                                                             break;
                                                         }
                                                     }
                                                     else
                                                     {
                                                         if (custRow["prim"].ToString().Trim().Length == 0)
                                                         {
                                                             Code_tp_in_out = item[1].Trim();
                                                             break;
                                                         }
                                                     }

                                                 }

                                             }
                                             if (Code_tp_in_out.Length == 0)
                                             {
                                                 foreach (string[] item in LArticlesOplataPostavki)
                                                 {
                                                     if (item[0].Trim().Length > 0)
                                                     {
                                                         if (item.Length > 1 && custRow["prim"].ToString().ToLower().IndexOf(item[0].Trim().ToLower()) > -1)
                                                         {
                                                             Code_tp_in_out = item[1].Trim();
                                                             Organis = true;
                                                             break;
                                                         }
                                                     }
                                                     else
                                                     {
                                                         if (custRow["prim"].ToString().Trim().Length == 0)
                                                         {
                                                             Code_tp_in_out = item[1].Trim();
                                                             break;
                                                         }
                                                     }

                                                 }

                                             }
                                             if (Code_tp_in_out.Length == 0)
                                             {
                                                 foreach (string[] item in LArticlesSpisanijaSotrudnik)
                                                 {
                                                     if (item[0].Trim().Length > 0)
                                                     {
                                                         if (item.Length > 1 && custRow["prim"].ToString().ToLower().IndexOf(item[0].Trim().ToLower()) > -1)
                                                         {
                                                             Code_tp_in_out = item[1].Trim();
                                                             break;
                                                         }
                                                     }
                                                     else
                                                     {
                                                         if (custRow["prim"].ToString().Trim().Length == 0)
                                                         {
                                                             Code_tp_in_out = item[1].Trim();
                                                             break;
                                                         }
                                                     }

                                                 }

                                             }
                                             if (Code_tp_in_out.Length == 0)
                                             {
                                                 foreach (string[] item in LArticlesAvansZvit)
                                                 {
                                                     if (item[0].Trim().Length > 0)
                                                     {
                                                         if (item.Length > 1 && custRow["prim"].ToString().ToLower().IndexOf(item[0].Trim().ToLower()) > -1)
                                                         {
                                                             Code_tp_in_out = item[1].Trim();
                                                             //Organis = true;
                                                             break;
                                                         }
                                                     }
                                                     else
                                                     {
                                                         if (custRow["prim"].ToString().Trim().Length == 0)
                                                         {
                                                             Code_tp_in_out = item[1].Trim();
                                                             break;
                                                         }
                                                     }

                                                 }

                                             } if (Code_tp_in_out.Length == 0)
                                             {
                                                 foreach (string[] item in ArticlesBankuslugi)
                                                 {
                                                     if (item[0].Trim().Length > 0)
                                                     {
                                                         if (item.Length > 1 && custRow["prim"].ToString().ToLower().IndexOf(item[0].Trim().ToLower()) > -1)
                                                         {
                                                             Code_tp_in_out = item[1].Trim();
                                                             //Organis = true;
                                                             break;
                                                         }
                                                     }
                                                     else
                                                     {
                                                         if (custRow["prim"].ToString().Trim().Length == 0)
                                                         {
                                                             Code_tp_in_out = item[1].Trim();
                                                             break;
                                                         }
                                                     }

                                                 }

                                             }
                                             if (Code_tp_in_out.Length == 0)
                                             {
                                                 foreach (string[] item in ArticlesNalogi)
                                                 {
                                                     if (item[0].Trim().Length > 0)
                                                     {
                                                         if (item.Length > 1 && custRow["prim"].ToString().ToLower().IndexOf(item[0].Trim().ToLower()) > -1)
                                                         {
                                                             Code_tp_in_out = item[1].Trim();
                                                             //Organis = true;
                                                             break;
                                                         }
                                                     }
                                                     else
                                                     {
                                                         if (custRow["prim"].ToString().Trim().Length == 0)
                                                         {
                                                             Code_tp_in_out = item[1].Trim();
                                                             break;
                                                         }
                                                     }

                                                 }

                                             }
                                             #endregion



                                             // Регистрируем выписку (сохраняем документ) tp_in_out, @tp_in_out,
                                             LocalQuery.UserQuery = string.Format("insert into mn_bank (num, dat, org, rs, org_to, rs_to, summa, curr,prim,dop_info {2})" +
                                             " values (@num, @dat,'{0}',@rs,'{1}', @rs_to, @Summ, @curr, @prim, @dop_info {3})",
                                                         CODE_ORG_OTPROV,
                                                         CODE_ORG_POLUCH,
                                                         (Code_tp_in_out.Length > 0 ? ", tp_in_out" : ""),
                                                         (Code_tp_in_out.Length > 0 ? ", @tp_in_out" : "")
                                                         );

                                             //(ParametrsBlock.SchemaTable.ContainsKey("NoName_17") ? ParametrsBlock.SchemaTable["NoName_17"] : "@prim") + "," +
                                             //(ParametrsBlock.SchemaTable.ContainsKey("NoName_18") ? ParametrsBlock.SchemaTable["NoName_18"] : "@dop_info") + ") ",

                                             LocalQuery.FBParametrsCommandUserQuery.AddWithValue("@num", custRow["NUM_DOC"].ToString().Trim());

                                             // Проверка успешности конвертации даты
                                             //DateTime dateValue_;
                                             //DateTime ConvertDate = new DateTime();
                                             if (DateTime.TryParse(custRow["DATE_DOC"].ToString().Trim(), out dateValue_))
                                             {
                                                 ConvertDate = Convert.ToDateTime(custRow["DATE_DOC"].ToString().Trim());
                                             }
                                             else
                                             {
                                                 // Определяем псевдо форматы год месяц дата => день месяц год
                                                 if (custRow["DATE_DOC"].ToString().Trim().Length == 8)
                                                 {
                                                     string strar = custRow["DATE_DOC"].ToString().Trim();
                                                     ConvertDate = Convert.ToDateTime(strar.Substring(6, 2) + "/" + strar.Substring(4, 2) + "/" + strar.Substring(0, 4));
                                                 }
                                             }


                                             if (Code_tp_in_out.Length > 0)
                                             {
                                                 LocalQuery.FBParametrsCommandUserQuery.AddWithValue("@tp_in_out", Code_tp_in_out);
                                             }
                                             else
                                             {
                                                 LocalQuery.FBParametrsCommandUserQuery.AddWithValue("@tp_in_out", DBNull.Value);
                                             }


                                             //ConvertDate = Convert.ToDateTime(custRow["DATE_DOC"].ToString().Trim());
                                             LocalQuery.FBParametrsCommandUserQuery.AddWithValue("@dat", ConvertDate.ToString("d"));
                                             //LocalQuery.FBParametrsCommandUserQuery.AddWithValue("@dat", custRow["DATE_DOC"].ToString().Trim());

                                             LocalQuery.FBParametrsCommandUserQuery.AddWithValue("@rs", custRow[RR_OTPROV_POLENAM].ToString().Trim());
                                             LocalQuery.FBParametrsCommandUserQuery.AddWithValue("@rs_to", custRow[RR_POLUCH_POLENAM].ToString().Trim());
                                             //LocalQuery.FBParametrsCommandUserQuery.AddWithValue("@rs_to", custRow[RR_POLUCH_POLENAM].ToString().Trim());

                                             Decimal Summa = Convert.ToDecimal(custRow["summa"].ToString().Trim());

                                             LocalQuery.FBParametrsCommandUserQuery.AddWithValue("@Summ", (Summa > 0 ? Summa : (Summa * (-1))) );

                                             // LocalQuery.FBParametrsCommandUserQuery.AddWithValue("@tp_in_out", ParametrsBlock.SchemaTable.ContainsKey("NoName_15") ? ParametrsBlock.SchemaTable["NoName_15"] : "null");
                                             LocalQuery.FBParametrsCommandUserQuery.AddWithValue("@curr", ParametrsBlock.SchemaTable.ContainsKey("NoName_16") ? ParametrsBlock.SchemaTable["NoName_16"] : ParametrsBlock.SchemaCur.ContainsKey(custRow["curr"].ToString().Trim()) ? ParametrsBlock.SchemaCur[custRow["curr"].ToString().Trim()] : ParametrsBlock.SchemaCur[ParametrsBlock.ParametrsImport["CurrDef"]]);
                                             LocalQuery.FBParametrsCommandUserQuery.AddWithValue("@prim", ParametrsBlock.SchemaTable.ContainsKey("NoName_17") ? ParametrsBlock.SchemaTable["NoName_17"] : custRow["prim"].ToString().Trim());
                                             // AUTOORDER'||' '||DATETOSTR('today')
                                             LocalQuery.FBParametrsCommandUserQuery.AddWithValue("@dop_info", ParametrsBlock.SchemaTable.ContainsKey("NoName_18") ? string.Format("{0} {1}.{2}.{3}", ParametrsBlock.SchemaTable["NoName_18"], DateTime.Now.Day, DateTime.Now.Month, DateTime.Now.Year) : string.Format("AUTOORDER  {0}.{1}.{2} ", DateTime.Now.Day, DateTime.Now.Month, DateTime.Now.Year));

                                             NumberRow = LocalQuery.ExecuteUNINonQuery();
                                             //org - организация которая платит (если чужая то она оплачевает НАМ, если наша то мі кому-то оплачиваем)
                                             //rs - расчетный  счет данной организции плательщика
                                             //org_to - организация получатель денег
                                             //rs_to - расчетній счет организации получателя 

                                             //var tr = LocalQuery.UserQuery;





                                             // Расчет Банковских услуг
                                             // VarParBANK_USLUGA


                                             // Чистка параметров
                                             LocalQuery.FBParametrsCommandUserQuery.Clear();



                                         }
                                     #endregion

                                     }




                                     #endregion


                                 }
                                 // Конец перебора схем


                             }




                         }


                         // Ins_.TableName = "Ins_1";

                     }
                     catch (FirebirdSql.Data.FirebirdClient.FbException exFB)
                     {

                         // -206 - не ввел параметр
                         SystemConecto.ErorDebag("При обращении к БД ..., возникло исключение: " + Environment.NewLine +
                                " === IDCode: " + exFB.ErrorCode.ToString() + Environment.NewLine +
                                " === Message: " + exFB.Message.ToString() + Environment.NewLine +
                                " === Exception: " + exFB.ToString(), 1);
                         //335544721 - обрыв соединения;
                         if (exFB.ErrorCode == 335544721)
                         {
                             if (WaitNetTimeStart_EXPORT.IsRunning)
                             {
                                 // Отладка
                                 //  SystemConecto.ErorDebag(WaitNetTimeStart_EXPORT.Elapsed.Seconds.ToString() + " / " +
                                 // SystemConecto.WaitNetTimeSec.Seconds.ToString() , 1);
                                 if (WaitNetTimeStart_EXPORT.Elapsed.Seconds >= Convert.ToInt32(SystemConecto.WaitNetTimeSec.Seconds))
                                 {
                                     #region Статус Выполнения потоков StatusBarCode
                                     // Поток завершен - перед каждым return и в конце кода
                                     Interlocked.Increment(ref locker[1]);
                                     #endregion

                                     return;

                                 }
                                 Thread.Sleep(5000);
                             }
                             else
                             {
                                 WaitNetTimeStart_EXPORT.Start();
                                 Thread.Sleep(5000);

                             }

                             // Повторяем запуск   
                             //Ecxport_Click_Block(sender, e, idBlock);
                             ImportBank_Click_Block(PrSendThread);

                         }



                     }
                     catch (Exception ex)
                     {

                         // -206 - не ввел параметр
                         SystemConecto.ErorDebag("При обращении к БД ..., возникло исключение: " + Environment.NewLine +
                                " === Message: " + ex.Message.ToString() + Environment.NewLine +
                                " === Exception: " + ex.ToString(), 1);

                     }
                     finally
                     {
                         //#region Статус Выполнения потоков StatusBarCode
                         //// Поток завершен - перед каждым return и в конце кода
                         //Interlocked.Increment(ref locker[1]);
                         //#endregion

                     }

                     #region Статус Выполнения потоков StatusBarCode
                     // Поток завершен - перед каждым return и в конце кода
                     Interlocked.Increment(ref locker[1]);
                     #endregion


                 }

                 #endregion

                 #endregion


         #region Переключение закладки Импорт настройки

         private void ImportOpcii_Click(object sender, RoutedEventArgs e)
         {
            AdminExport.IsSelected = true;
            ImportOpcii_TabPage.IsSelected = true;

         }

         #endregion


         #endregion

       


        #region Статус потоков StatusBarCode

             /// <summary>
             /// Отслеживание выполнения потоков locker[0] -количество зарегистрировавшихся потоков,<para></para>
             /// locker[1] - количество потоков кторые завершили выполнятся
             /// </summary>
             static int[] locker { get; set; }
            /// <summary>
            /// Статутс потока в процентом соотношении
            /// </summary>
             static List<int> lockerStat { get; set; }
             /// <summary>
             /// Сервисный комментарий потока
             /// </summary>
             static List<int> lockerComment { get; set; }
             /// <summary>
             /// MinWidth - Длина под текст для статуса бара.
             /// </summary>
             static double RezervContentStatusBar = 180;
        #endregion




        #region Переключение закладки Обновление цен на сайте настройки 

         private void CinaWEBOpcii_Click(object sender, RoutedEventArgs e)
         {
             ExcportCinaOpcii_TabPage.IsSelected = true;
             AdminExport.IsSelected = true;


         }
         #endregion

        #region Обновление цен на сайте

         private void CinaWEB_Click(object sender, RoutedEventArgs e)
         {
             

         }



        #endregion

  

        /// <summary>
        /// Вызов виртуальной клавиатуры
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButKeyboard_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var uriSource = new Uri(@"/Conecto®%20WorkSpace;component/Images/keyboard2.png", UriKind.Relative);
            this.ButKeyboard.Source = new BitmapImage(uriSource);


        }

        private void ButKeyboard_MouseLeave(object sender, MouseEventArgs e)
        {
            var uriSource = new Uri(@"/Conecto®%20WorkSpace;component/Images/keyboard1.png", UriKind.Relative);
            this.ButKeyboard.Source = new BitmapImage(uriSource);
        }

        private void ButKeyboard_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (!AppStart.SetFocusWindow("osk"))
            {

                Process procCommand = Process.Start("osk");
            }
        }

        private void ButKeyboard_MouseMove(object sender, MouseEventArgs e)
        {
            //***
        }


    }
}
