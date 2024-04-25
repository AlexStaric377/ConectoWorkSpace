using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Data;
// --- Process 
using System.Diagnostics;
using System.ComponentModel; // Win32Exception
/// Многопоточность
using System.Threading;
using System.Windows.Threading;
// Заставка
using ConectoWorkSpace.Splasher_startWindow;
// Реестор Windows
using Microsoft.Win32;
using System.IO;




namespace ConectoWorkSpace
{
    /// <summary>
    /// Логика взаимодействия для Window1.xaml
    /// </summary>
    public partial class Admin : Window
    {
        public static string EditTable = null;  // Режим включения редактирования
        //MainWindow ConectoWorkSpace_InW = (MainWindow)Application.Current.MainWindow;
        public Admin(string VariantWindow = "")
        {
            InitializeComponent();

            // Скрыть таблицы окон
            this.GridOS.Visibility = Visibility.Hidden;
            this.StructGrid.Visibility = Visibility.Hidden;
            this.NetViewGrid.Visibility = Visibility.Hidden;
            this.CoristuvachiGrid.Visibility = Visibility.Hidden;
            this.ConectoGrid.Visibility = Visibility.Hidden;
            this.SKDGrid.Visibility = Visibility.Hidden;

            ResolutionDisplay();
            // Активировать вкладку
            MenuItem();

            // Варианты окон
            if (VariantWindow == "")
            {

            }

        }

        #region Построение интерфейса
        /// <summary>
        /// Отрисовка изменений размера экрана
        /// </summary>
        public void ResolutionDisplay()
        {

            // 3. Установка размеров окна для того чтобы игнорировать нажатие клавиши раскрыть на весь экран
            // Рабочего стола ([0] - Top; [1] - Left; [2] - Width; [3] - Height; [4] - Right;
            this.Top = SystemConecto.WorkAreaDisplayDefault[0];
            this.Left = SystemConecto.WorkAreaDisplayDefault[1]+142;
            this.Width = SystemConecto.WorkAreaDisplayDefault[2];
            PanelWindow.Height = new GridLength(SystemConecto.WorkAreaDisplayDefault[3] - 45 - 32);
            this.Height = this.WindGrid.Height =SystemConecto.WorkAreaDisplayDefault[3]-45;
            
            this.WindGrid.Width = SystemConecto.WorkAreaDisplayDefault[2]-142;
            //this.Location = new System.Drawing.Point(SizeDWArea_a[1], SizeDWArea_a[0]);
            //this.ClientSize = new System.Drawing.Size(SizeDWArea_a[2], SizeDWArea_a[3]);
            
        }
        #endregion


        #region События Click (Клик) функцилнальных клавиш окна
        private void Close_PanelSys(int TypeClose = 1)
        {

            if (TypeClose == 2)
            {
                // Закрыть авторизацию
                SystemConectoInterfice.UserInterficeClose();
            }

            //MainWindow ConectoWorkSpace_InW = (MainWindow)Application.Current.MainWindow;
            MainWindow ConectoWorkSpace_InW = AppStart.LinkMainWindow();
            if (ConectoWorkSpace_InW != null)
            {
                 System.Windows.Application.Current.Dispatcher.Invoke(new Action(delegate()
                 {
  
                    // Ссылка на объект и метод
                    ConectoWorkSpace_InW.CloseAdministrator();
                   }));
                // Пример - неудачного закрытия окна не исправил глюк с закрытием дочерних окон
                //ConectoWorkSpace_InW.ShowActiveMainAppinOS(); 
            }
 



            // Убирает глюк закрытия дочернего окна (потеря фокуса)
            // http://social.msdn.microsoft.com/forums/en-US/wpf/thread/a51ac7b9-7021-4669-accd-af0e86b9cc01/
            // habilitar ventana  
            // this.Owner.IsEnabled = true;

            // enfocar owner  
            this.Owner.Focus();
            this.Owner = null;
            this.Close();

        }

        #region Клавиша вихода из модуля завершение сессии
        private void ImButExit_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {

            if (AppStart.rkAppSetingAllUser.GetValue("ImPerekluchTerminal") != null && (int)AppStart.rkAppSetingAllUser.GetValue("ImPerekluchTerminal") == 1)
            {

                MainWindow ConectoWorkSpace_InW = AppStart.LinkMainWindow();
                if (ConectoWorkSpace_InW == null)
                {
                    return;
                }
                //Выключить элементы интерфейса свернуть и закрыть               
                ConectoWorkSpace_InW.MinimiButIm.Visibility = Visibility.Collapsed;
                ConectoWorkSpace_InW.Note.Visibility = Visibility.Collapsed;
                ConectoWorkSpace_InW.Calcul.Visibility = Visibility.Collapsed;
                ConectoWorkSpace_InW.Close_F.Visibility = Visibility.Collapsed;
                ConectoWorkSpace_InW.ButCatalog.Visibility = Visibility.Collapsed;
            }

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
        #endregion

        #region Клавиша Close 
        private void Close_F_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            var uriSource = new Uri(@"/Conecto®%20WorkSpace;component/Images/knop2_1.png", UriKind.Relative);
            Close_F.Source = new BitmapImage(uriSource);
            Close_PanelSys(2);
        }

        private void Close_F_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var uriSource = new Uri(@"/Conecto®%20WorkSpace;component/Images/knop2_2.png", UriKind.Relative);
            Close_F.Source = new BitmapImage(uriSource);
        }

        private void Close_F_MouseLeave(object sender, MouseEventArgs e)
        {
            var uriSource = new Uri(@"/Conecto®%20WorkSpace;component/Images/knop2_1.png", UriKind.Relative);
            Close_F.Source = new BitmapImage(uriSource);
        }


        #endregion


        #endregion


        #region Изображения как клавиши - Визуализация интерфейса 

        
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

        private void WaitFonW_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            // Ожидать с выводом окон
            if (this.Visibility == Visibility.Visible)
            {
                //MessageBox.Show("запуск");
                //SystemConecto.WaitTaskWindow(1);
            }
            else
            {
                //MessageBox.Show("стоп");
                //SystemConecto.WaitTaskWindow(0);
            }

        }

        #region Итрефейсы вкладок окна

        private string ActiveItemMenu1 = "";
        private string ActiveItemMenu1_label = "";

        private void MenuItem(string ItemName = "GridOS", string ItemLabelNew = "Menu1_1")
        {
            // Отступ сверху и слева
            var TopBack = this.WindGrid.Margin.Top + 55;
            var LeftBack = this.WindGrid.Margin.Left + 18;

            // Закрыть если есть активное окно
            if (ActiveItemMenu1.Length > 0)
            {
                Grid ItemNameGrid = (Grid)LogicalTreeHelper.FindLogicalNode(this, ActiveItemMenu1);
                if(ItemNameGrid!=null){
                    ItemNameGrid.Visibility = Visibility.Hidden;
                }
                // изменить свойства меню
                // Текст
                Label ItemNamLabel = (Label)LogicalTreeHelper.FindLogicalNode(this, ActiveItemMenu1_label);
                if (ItemNamLabel != null)
                {
                    ItemNamLabel.Cursor = Cursors.Hand;
                    ItemNamLabel.Foreground = Brushes.White;
                }
                // Фон
                Border ItemNamBorder = (Border)LogicalTreeHelper.FindLogicalNode(this, "brBut_" +ActiveItemMenu1_label);
                if (ItemNamBorder != null)
                {
                    ItemNamBorder.Background = null;
                    //ItemNamBorder.Foreground = Brushes.White;
                }
            }

            ActiveItemMenu1 = ItemName;
            ActiveItemMenu1_label = ItemLabelNew;


            // изменить свойства меню активная вкладка
            Label ItemNamLabel_new = (Label)LogicalTreeHelper.FindLogicalNode(this, ActiveItemMenu1_label);
            if (ItemNamLabel_new != null)
            {
                ItemNamLabel_new.Cursor = Cursors.Arrow;
                ItemNamLabel_new.Foreground = Brushes.Black;
            }
            // Фон активной вкладки
            Border ItemNamBorder_ = (Border)LogicalTreeHelper.FindLogicalNode(this, "brBut_" +ActiveItemMenu1_label);
            if (ItemNamBorder_ != null)
            {
                ItemNamBorder_.Background = new ImageBrush(new BitmapImage(new Uri(@"pack://application:,,,/Conecto®%20WorkSpace;component/Images/pnsys_zeltoe_vydilen.png")));
            }

            Uri uriSource = null;

            // Открыть Таблицу закладки
            switch (ItemName)
            {
                case ("GridOS"):

                    // Заголовок
                    uriSource = new Uri(@"/Conecto®%20WorkSpace;component/Images/pnsys_operaz_sistem.png", UriKind.Relative);
                    NazvaWindowWhite.Source = new BitmapImage(uriSource);

                    // Функции
                    //TerminalView();

                    GridOSM(LeftBack, TopBack);

                    

                    break;
                
                case ("StructGrid"):

                    // Заголовок
                    uriSource = new Uri(@"/Conecto®%20WorkSpace;component/Images/pnsys_nastroyka.png", UriKind.Relative);
                    NazvaWindowWhite.Source = new BitmapImage(uriSource);

                    StructGridM(LeftBack, TopBack);

                    break;

                case ("NetViewGrid"):

                    // Заголовок
                    uriSource = new Uri(@"/Conecto®%20WorkSpace;component/Images/pnsys_set.png", UriKind.Relative);
                    NazvaWindowWhite.Source = new BitmapImage(uriSource);


                    this.NetViewGrid.Visibility = Visibility.Visible;
                    this.NetViewGrid.Margin = new Thickness(LeftBack, TopBack, 0, 0); ;
                    this.NetViewGrid.Height = this.WindGrid.Height - 55;
                    this.NetViewGrid.Width = this.WindGrid.Width - 50;

                    break;
                case ("CoristuvachiGrid"):

                    // Заголовок
                    uriSource = new Uri(@"/Conecto®%20WorkSpace;component/Images/pnsys_polzovat.png", UriKind.Relative);
                    NazvaWindowWhite.Source = new BitmapImage(uriSource);


                    CoristuvachiGridM(LeftBack, TopBack);

                    break;


                case ("ConectoGrid"):
                    this.ConectoGrid.Visibility = Visibility.Visible;
                    this.ConectoGrid.Margin = new Thickness(LeftBack, TopBack, 0, 0); ;
                    this.ConectoGrid.Height = this.WindGrid.Height - 55;
                    this.ConectoGrid.Width = this.WindGrid.Width - 50;

                    break;
                case ("SKDGrid"):
                    SKDGridM(LeftBack, TopBack);

                    break;
            }

        }


        #region ==== События нажатия на меню

        private void Menu1_1_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
           
        }
        private void Menu1_1_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            MenuItem("GridOS", this.Menu1_1.Name);
        }
        private void TextBlock_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            MenuItem("GridOS", this.Menu1_1.Name);
        }


        private void Menu1_2_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            MenuItem("NetViewGrid", this.Menu1_2.Name);
        }
        private void Menu1_3_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            MenuItem("ConectoGrid", this.Menu1_3.Name);
        }
        private void Menu1_4_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            MenuItem("SKDGrid", this.Menu1_4.Name);
        }
        private void Menu1_5_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            MenuItem("CoristuvachiGrid", this.Menu1_5.Name);
        }
        private void Menu1_6_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            
        }
        private void Menu1_6_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            MenuItem("StructGrid", this.Menu1_6.Name);
        }
        private void TextBlock_MouseLeftButtonUp_1(object sender, MouseButtonEventArgs e)
        {
            MenuItem("StructGrid", this.Menu1_6.Name);
        }



        #endregion


        #region Интерфейс закладки Инструменты Операционной системы
        /// <summary>
        /// Формирование интерфейса закладки Инструменты Операционной системы
        /// </summary>
        private void GridOSM(dynamic LeftBack, dynamic TopBack)
        {
            // === Изменить размеры Grid
            //this.GridOS.RowDefinitions[0].Height = GridLength.Auto; //new GridLength(1, GridUnitType.Star);
            //this.GridOS.RowDefinitions[1].Height = GridLength.Auto;

            // === Без сохранния предыдущего состояния (Скрыть настройки и диагностику)


            this.GridOS.Visibility = Visibility.Visible;
            this.GridOS.Margin = new Thickness(LeftBack, TopBack, 0, 0);
            this.GridOS.Height = this.WindGrid.Height - 55;
            this.GridOS.Width = this.WindGrid.Width - 50;

            // === Настройка интерфейса


            // === Клавиши редактора



            // === Чтение параметров

            // === Дополнительная информация
            // http://bytes.com/topic/c-sharp/answers/605957-launch-printer-control-panel

            
        }
        #endregion

        
        #region Интерфейс закладки  Структура организации рабочего места


        /// <summary>
        /// Формирование интерфейса закладки Структура организации рабочего места (приложений) 
        /// </summary>
        private void StructGridM(dynamic LeftBack, dynamic TopBack)
        {
            // ===  Изменить размеры Grid
            this.StructGrid.RowDefinitions[0].Height = GridLength.Auto; //new GridLength(1, GridUnitType.Star);
            this.StructGrid.RowDefinitions[1].Height = GridLength.Auto;

            // ===  Без сохранния предыдущего состояния (Скрыть настройки и диагностику)
            // this.OpciiStructGrid.Visibility = Visibility.Collapsed;
            this.DiagnStructGrid.Visibility = Visibility.Collapsed;

            

            this.StructGrid.Visibility = Visibility.Visible;
            this.StructGrid.Margin = new Thickness(LeftBack, TopBack, 0, 0); 
            this.StructGrid.Height = this.WindGrid.Height - 55;
            this.StructGrid.Width = this.WindGrid.Width - 50;

            // ===  Настройка интерфейса
            ((Image)LogicalTreeHelper.FindLogicalNode(this, "Undo_ipbd")).Visibility = Visibility.Hidden;


            var ItemImageButtonPatch = (Image)LogicalTreeHelper.FindLogicalNode(this, "Undo_patch");
            ItemImageButtonPatch.Visibility = (ConfigControll.aParamAppUndo.ContainsKey("Desktop_Putch-Fornt"))?  Visibility.Visible : Visibility.Hidden ;

            // ===  Клавиши редактора
            var ItemImageButtonDir = (Image)LogicalTreeHelper.FindLogicalNode(this, "PutchFront1_Dir_Front");
            var ItemImageButtonEditDir = (Image)LogicalTreeHelper.FindLogicalNode(this, "PutchFront1_Edit_Front");
            ItemImageButtonEditDir.Margin = new Thickness(ItemImageButtonDir.Margin.Left, ItemImageButtonDir.Margin.Top, 0, 0);
            ItemImageButtonEditDir.Visibility = Visibility.Hidden;


            // ===  Чтение параметров
            StructGrid_ReadParam();

        }

        /// <summary>
        /// Чтение параметров в поля закладки Структура организации рабочего места (приложений) 
        /// </summary>
        /// <param name="NameParam">Имя параметра. По умолчанию All - все параметры</param>
        private void StructGrid_ReadParam(string NameParam = "All")
        {
            if (NameParam == "All")
            {
                //Чтение всех параметров
                //Чтение параметра и запись нового значения
               // ((TextBox)LogicalTreeHelper.FindLogicalNode(this, "PutchFront1")).Text = SystemConecto.aParamApp["Desktop_Putch-Fornt"];
                ((TextBox)LogicalTreeHelper.FindLogicalNode(this, "TexIp1")).Text = SystemConecto.aParamApp["БДСервер1_IP"];
                
                // ! Пример раскрытого кода !
                var chekTime_ = (CheckBox)LogicalTreeHelper.FindLogicalNode(this, "chekTime");
                chekTime_.IsChecked = (SystemConecto.aParamApp["Time_IP"] == "0.0.0.0" ? false : true);
                

            }
            else{
                // Чтение одного параметра


            }

        }

        #endregion


        #region Интерфейс закладки Пользователи
        /// <summary>
        /// Формирование интерфейса закладки Пользователи
        /// </summary>
        private void CoristuvachiGridM(dynamic LeftBack, dynamic TopBack)
        {


        }
        #endregion


        #region Интерфейс закладки СКД

        /// <summary>
        /// Формирование интерфейса закладки  СКД
        /// </summary>
        /// <param name="LeftBack"></param>
        /// <param name="TopBack"></param>
        private void SKDGridM(dynamic LeftBack, dynamic TopBack)
        {
            this.SKDGrid.Visibility = Visibility.Visible;
            this.SKDGrid.Margin = new Thickness(LeftBack, TopBack, 0, 0); ;
            this.SKDGrid.Height = this.WindGrid.Height - 55;
            this.SKDGrid.Width = this.WindGrid.Width - 50;
            // Настройки 
            this.OpciiSKDGrid.Margin = new Thickness(0, 42, 0, 0);
            //this.OpciiSKDGrid.Width = this.OpciiSKDGrid.Width - 100;
            // Граница
            //this.OpciiSKDGrid.Background = Brushes.Yellow;

            // Диагностика выключенна
            this.DiagnSKDGrid.Visibility = Visibility.Collapsed;

            // Поиск конверторов
            var TextTFindConvert = (TextBlock)LogicalTreeHelper.FindLogicalNode(this, "TFindConvert");
            TextTFindConvert.Text = "Система опрашивает подключенные конверторы";
            // Поиск контроллеров
            var TextTFindController = (TextBlock)LogicalTreeHelper.FindLogicalNode(this, "TFindController");
            TextTFindController.Text = "Система опрашивает подключенные контроллеры";

            // Запуск сервера
            SystemConectoSKDServer.SKDServer("StartServer");

            // Опрос устройств
            var ItemNameDataGrid = (System.Windows.Controls.DataGrid)LogicalTreeHelper.FindLogicalNode(this, "dataGridConvert");
            if (ItemNameDataGrid != null)
            {
                // Не обноружила конверторы
                if (SystemConectoSKDServer.TableListConvectorSKD.Count > 0)
                {
                    TextTFindConvert.Visibility = Visibility.Collapsed;
                }
                else
                {
                    TextTFindConvert.Text = "Система не обноружила подключенные конверторы";

                }


                TextTFindController.Text = "Система не обноружила подключенные контроллеры";


                // Отключить добовлять новую строку
                ItemNameDataGrid.CanUserAddRows = false;
                // Отключить сортировку
               // ItemNameDataGrid.ha AllowSorting = false;

                // Данные для таблицы конвертеров
                ItemNameDataGrid.ItemsSource = SystemConectoSKDServer.TableListConvectorSKD; // DataList.LoadDataConvert();
                // Данные для таблицы контролеров
                ItemNameDataGrid.ItemsSource = SystemConectoSKDServer.TableListControllerSKD;
                // Выбор ячейки  SelectiondataGridConvert();
                ItemNameDataGrid.SelectedIndex = 0;

                
            }

        }


        #endregion

        #endregion

        #region События закладки Инструменты Операционной системы

        #region Верхняя панель
        
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
        /// Панель управления
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

        /// <summary>
        /// Администрирование из панели управления
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void image16_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            ProcessStartInfo psiOpt = new ProcessStartInfo(@"control.exe", @"admintools");
            psiOpt.CreateNoWindow = true;
            psiOpt.WorkingDirectory = @"%windir%\system32\";  // Альтернатива System.Environment.SystemDirectory
            // запускаем процесс
            Process procCommand = Process.Start(psiOpt);
        }
        /// <summary>
        /// Параметр включения выключения терминала
        /// </summary>
        public static  bool TerminalStatus = false;



        /// <summary>
        /// Включить режим терминала
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void image18_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

        }


        private void ImPerekluchTerminal_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            TerminalPerecluch();
        }


        private void image18_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            TerminalPerecluch();
        }
        /// <summary>
        /// Ручной метод переключения режима терминал
        /// </summary>
        private void TerminalPerecluch()
        {

            // MainWindow ConectoWorkSpace_InW = (MainWindow)Application.Current.MainWindow;
            string System_path = System.IO.Path.GetPathRoot(System.Environment.SystemDirectory) + @"Windows\explorer.exe";
            // Дополнительно проверить запуск explorer
            var getAllExploreProcess = Process.GetProcesses().Where(r => r.ProcessName.Contains("explorer"));
            // Открываем ключ для модификации влючения или отключения терминала режима  CurrentUser
            AppStart.rkStartApp.OpenSubKey(@"SOFTWARE\Microsoft\Windows NT\CurrentVersion\Winlogon", true);

            // Определяется какая OS Windows 64 или 32 разрядная
            RegistryKey localMachine = RegistryKey.OpenBaseKey(Microsoft.Win32.RegistryHive.LocalMachine, RegistryView.Registry32);
            if (SystemConecto.OS64Bit)
            {
                localMachine = RegistryKey.OpenBaseKey(Microsoft.Win32.RegistryHive.LocalMachine, RegistryView.Registry64);
            }
            // Открыть  системный регистр для модификации ключа Shell
            RegistryKey regKey = localMachine.OpenSubKey(@"SOFTWARE\Microsoft\Windows NT\CurrentVersion\Winlogon", true);

            // определяем ключ для модификации влючения или отключения режима РДП
            RegistryKey regApp = AppStart.rkAppSetingAllUser;

            //RegistryKey regAutoAdminLogon = regKey;
            //regAutoAdminLogon.OpenSubKey(@"SOFTWARE\Microsoft\Windows NT\CurrentVersion\Winlogon", true);

            // Проверка включен ли терминал
            if (AppStart.rkAppSetingAllUser.GetValue("ImPerekluchTerminal") != null && (int)AppStart.rkAppSetingAllUser.GetValue("ImPerekluchTerminal") == 1)
            {
                // Выключить автовход с систему без логина и пароля
                regKey.SetValue("AutoAdminLogon", "0", RegistryValueKind.String);
                regKey.SetValue("DefaultPassword", "", RegistryValueKind.String);
                // выключение запуска ConectoWorkSpace из автозагрузки.
                AppStart.rkStartApp.SetValue("Shell", "", RegistryValueKind.String);
                // выключить РДП
                if (regApp.GetValue("TerminalRDP") != null && (int)regApp.GetValue("TerminalRDP") == 1)
                {
                    TerminalRDP.IsChecked = false;
                    regApp.SetValue("TerminalRDP", (bool)((CheckBox)LogicalTreeHelper.FindLogicalNode(this, "TerminalRDP")).IsChecked ? 1 : 0, RegistryValueKind.DWord);
                }

                // выключить теминал Администратора
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



                // Изменение значения ключа "Shell для запуска exolorer при перегрузке системы

                regKey.SetValue("Shell", System_path, RegistryValueKind.String);


                ProcessStartInfo processInfo = new ProcessStartInfo(System_path); //создаем новый процесс

                try
                {
                    Process.Start(processInfo); //пытаемся запустить  Explorer

                    // оббязательно указать расположение так как пересекается с Explorer
                    // processInfo.WorkingDirectory = @"%windir%\";
                    // ConectoWorkSpace_InW.WindowState = WindowState.Normal;
                    // ProcessConecto.StateSystemWindow("Shell_TrayWnd", 1);
                    // ProcessConecto.StateSystemWindow("Button", 1);
                    // Обновим ConectoWorkSpace
                    //ConectoWorkSpace_InW.MainWindow2();

                }
                catch (Win32Exception ex)
                {
                    //Ничего не делаем, потому что пользователь, возможно, нажал кнопку "Нет" в ответ на вопрос о запуске программы в окне предупреждения UAC (для Windows 7)
                    SystemConecto.ErorDebag(ex.ToString(), 1);
                }
                TerminalStatus = false;
                TerminalView(2);
                Thread.Sleep(50);
                // перегрузить конекто WorkSpace
                ConectoWorkSpace_InW.ResizeConectoWorkSpace(WindowState.Normal);
                // Процедура изменение, обновление размера экрана



            }
            else
            {

                // включить теминал Администратора
                regApp.SetValue("ImPerekluchTerminal", "1", RegistryValueKind.DWord);

                // Включить вход с систему c логином и паролем
                regKey.SetValue("AutoAdminLogon", "1", RegistryValueKind.String);
                regKey.SetValue("DefaultPassword", "staric777", RegistryValueKind.String);

                if (regApp.GetValue("TerminalRDP") != null && (int)regApp.GetValue("TerminalRDP") == 1)
                {
                    regKey.SetValue("Shell", System_path, RegistryValueKind.String);
                    regKey = AppStart.rkStartApp;
                }

                // Скрыть панель задачь
                // ProcessConecto.StateSystemWindow("Shell_TrayWnd"); // "HHTaskBar"
                // Срыть кнопку пуск
                // ProcessConecto.StateSystemWindow("Button");

                if (ProcessConecto.TerminateProcessSystem("explorer"))
                {
                    TerminalStatus = true;
                    TerminalView(1);
                    Thread.Sleep(50);
                    // перегрузить конекто WorkSpace
                    MainWindow ConectoWorkSpace_InW = AppStart.LinkMainWindow();
                    if (ConectoWorkSpace_InW != null)
                    {
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
                #region Включение терминала в реестре ОС Windows
                // Изменение значения ключа "Shell для запуска ConectoWorkSpace при перегрузке системы
                string PuthConecto = Process.GetCurrentProcess().MainModule.FileName + " -t";

                regKey.SetValue("Shell", PuthConecto, RegistryValueKind.String);

                #endregion
            }
            //System.Windows.MessageBox.Show(regKey.GetValue("Shell").ToString(),"",
            //System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Stop);
            regKey.Flush();
            regApp.Flush();




        }

        /// <summary>
        /// // включение / выключение терминала РДП
        /// </summary>
        private void TerminalRDP_Click(object sender, RoutedEventArgs e)
        {
            AppStart.rkAppSetingAllUser.SetValue("TerminalRDP", (bool)((CheckBox)LogicalTreeHelper.FindLogicalNode(this, "TerminalRDP")).IsChecked ? 1 : 0, RegistryValueKind.DWord);
            AppStart.rkAppSetingAllUser.Flush();

            if ((int)AppStart.rkAppSetingAllUser.GetValue("TerminalRDP") == 0)
            {
                // выключен РДП, очистить реестр, снять пометку включен в окне терминала
                TerminalRDP.IsChecked = false;
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
                // принудительное выключение режима теминала чтобы автоматически включить снова
                // для случая включенного терминала и включения РДП
                AppStart.rkAppSetingAllUser.SetValue("ImPerekluchTerminal", "0", RegistryValueKind.DWord);
                AppStart.rkAppSetingAllUser.Flush();
                TerminalPerecluch();
            }

        }

        /// <summary>
        /// Отображения состояния режима терминал
        /// OnOFFTerminal - 1 - включен
        /// </summary>
        private void TerminalView(int OnOFFTerminal = 0)
        {
            bool OnTerminal = false;
            // Режим авто определения
            if (OnOFFTerminal == 0)
            {
                //var getAllExploreProcess = Process.GetProcesses().Where(r => r.ProcessName.Contains("explorer"));
                // TerminalStatus  && - устарел  
                //getAllExploreProcess.Count() == 0
                if (AppStart.rkAppSetingAllUser.GetValue("ImPerekluchTerminal") != null && (int)AppStart.rkAppSetingAllUser.GetValue("ImPerekluchTerminal") == 1)
                {
                    OnTerminal = true;
                }
                if (AppStart.rkAppSetingAllUser.GetValue("TerminalRDP") != null && (int)AppStart.rkAppSetingAllUser.GetValue("TerminalRDP") == 1)
                {
                    TerminalRDP.IsChecked = true;
                }
                MainWindow ConectoWorkSpace_InW = AppStart.LinkMainWindow();
                //if (ConectoWorkSpace_InW != null)
                //{
                //    ConectoWorkSpace_InW.ResizeConectoWorkSpace(WindowState.Maximized);
                //}

            }
            else
            {
                // Режим указания значения
                OnTerminal = OnOFFTerminal == 1 ? true : false;

            }
            // Прорисовка
            if (OnTerminal)
            {
                TerminalStatusContent.Content = "включен";
                Uri uriSource = new Uri(@"/Conecto®%20WorkSpace;component/Images/pnsys_1_1_terminal_1.png", UriKind.Relative);
                ImTerminal.Source = new BitmapImage(uriSource);
                uriSource = new Uri(@"/Conecto®%20WorkSpace;component/Images/pnsys_1_1_vklvykl1.png", UriKind.Relative);
                ImPerekluchTerminal.Source = new BitmapImage(uriSource);
            }
            else
            {
                Uri uriSource = new Uri(@"/Conecto®%20WorkSpace;component/Images/pnsys_1_1_terminal_2.png", UriKind.Relative);
                ImTerminal.Source = new BitmapImage(uriSource);
                uriSource = new Uri(@"/Conecto®%20WorkSpace;component/Images/pnsys_1_1_vklvykl2.png", UriKind.Relative);
                ImPerekluchTerminal.Source = new BitmapImage(uriSource);
                TerminalStatusContent.Content = "выключен";
            }
        }

        #endregion

        private void image10_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

            // создаем процесс cmd.exe с параметрами "ipconfig /all"
            //ProcessStartInfo psiOpt = new ProcessStartInfo(@"cmd.exe", @"/C ipconfig /all");
            ProcessStartInfo psiOpt = new ProcessStartInfo(@"control.exe", @"printers");
            // скрываем окно запущенного процесса
            // psiOpt.WindowStyle = ProcessWindowStyle.Hidden;
            // psiOpt.RedirectStandardOutput = true;
            // psiOpt.UseShellExecute = false;
            psiOpt.CreateNoWindow = true;
            psiOpt.WorkingDirectory = @"%windir%\system32\";  // Альтернатива System.Environment.SystemDirectory
            // запускаем процесс
            Process procCommand = Process.Start(psiOpt);
            // получаем ответ запущенного процесса
            //StreamReader srIncoming = procCommand.StandardOutput;
            // выводим результат
            //srIncoming.ReadToEnd();
            // закрываем процесс
            // procCommand.WaitForExit();


            // Информация
            // http://msdn.microsoft.com/ru-ru/library/cc144191(v=vs.85).aspx
            // http://support.microsoft.com/kb/192806
            // http://msdn.microsoft.com/en-us/library/ee330741(VS.85).aspx

            // Обход админ прав - тобишь установка текущему пользователю на данное приложение админ прав. Проверка и разработка.
            // @echo off
            // %windir%\System32\runas.exe /user:%USERNAME% "%*"
            // Первые две строки .bat файл
            // sudo notepad.exe %windir%\sudo.bat - это выполнить в консоле sudo это sudo.bat
            // http://andyone.me/post/1525117103/privileges-in-windows-console

        }

        /// <summary>
        /// Сетевые настройки
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void image15_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            ProcessStartInfo psiOpt = new ProcessStartInfo(@"control.exe", @"netconnections");
            psiOpt.CreateNoWindow = true;
            psiOpt.WorkingDirectory = @"%windir%\system32\";  // Альтернатива System.Environment.SystemDirectory
            // запускаем процесс
            Process procCommand = Process.Start(psiOpt);

        }

        /// <summary>
        /// Вызов виртуальной клавиатуры
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void image11_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (!AppStart.SetFocusWindow("osk"))
            {

                Process procCommand = Process.Start("osk");
            }
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

        #endregion

        #region События закладки Структура организации рабочего места (Автоматизации приложений)

        /// <summary>
        /// Выбор директории где расположен фронт не по умолчанию
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Dir_Front_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

            // Configure open file dialog box  link PresentetionFramework
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();
            dlg.FileName = "B52FrontOffice"; // Default file name
            dlg.DefaultExt = ".exe"; // Default file extension
            dlg.Filter = "Файл приложения |*.exe"; // Filter files by extension
            dlg.Title = "Путь к основному фронту";


            // Show open file dialog box
            Nullable<bool> result = dlg.ShowDialog();

            // Process open file dialog box results
            // Улучшить проверку
            if (result == true)
            {
                //Чтение параметра и запись нового значения
                var ItemTextBoxPatch = (TextBox)LogicalTreeHelper.FindLogicalNode(this, "PutchFront1");
                ItemTextBoxPatch.Text = dlg.FileName; 
                Update_PutchFront1(1);

            }

            

        }


        /// <summary>
        /// Изменение состояния Параметра синхронизация времени
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void chekTime_Click(object sender, RoutedEventArgs e)
        {

            // Запись значения
            if (!SystemConfigControll.ControllerParam((bool)((CheckBox)LogicalTreeHelper.FindLogicalNode(this, "chekTime")).IsChecked ? SystemConecto.aParamApp["БДСервер1_IP"] : "0.0.0.0", "Time_IP", 0))
            {
                // Ошибка ... Поменять цвет иконки, а также вывести сообщение в окне диагностики (Разработка)
            }
        }

        /// <summary>
        /// Закончить редактирование параметра - Вввод директории где расположен фронт не по умолчанию
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PutchFront1_Edit_Front_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var NameTextBop = "PutchFront1";
            
            Update_PutchFront1();
            // Емитация нажатия Ентер
            // Клавиши редактора (Могут отсутствовать)
            ((Image)LogicalTreeHelper.FindLogicalNode(this, NameTextBop + "_Dir_Front")).Visibility = Visibility.Visible;
            var ItemImageButtonEditDir = (Image)LogicalTreeHelper.FindLogicalNode(this, NameTextBop + "_Edit_Front");
            ItemImageButtonEditDir.Visibility = Visibility.Hidden;
        }


        /// <summary>
        /// Изменение параметра директории где расположен фронт не по умолчанию
        /// </summary>
        /// <param name="TypeValue">Тип значения 0 - ввод руками</param>
        private void Update_PutchFront1(int TypeValue = 0)
        {
           // MessageBox.Show("Парам");
            // Имя параметра что меняется
            var NameParam = "Desktop_Putch-Fornt";

            //Чтение параметра и запись нового значения
            var ItemTextBoxPatch = (TextBox)LogicalTreeHelper.FindLogicalNode(this, "PutchFront1");

            // Запись предыдущего значения
            SystemConfigControll.UpdateParamUndo(NameParam);
            // Активация отката (сначало красная елси диагностика положительная то зеленая)
            ((Image)LogicalTreeHelper.FindLogicalNode(this, "Undo_patch")).Visibility = Visibility.Visible;
            //SystemConecto.aParamApp
            // Запись значения
            if (!SystemConfigControll.ControllerParam(ItemTextBoxPatch.Text, NameParam, 11))
            {
                // Поменять цвет иконки, а также вывести сообщение в окне диагностики (Разработка)
            }

            // Перезапись новго значения
            if (TypeValue > 0)
            {
                ItemTextBoxPatch.Text = SystemConecto.aParamApp[NameParam];
            }
        }


        /// <summary>
        /// Комбинации клавиш для полей ввода директории где расположен фронт не по умолчанию
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PutchFront1_KeyUp(object sender, KeyEventArgs e)
        {
            KeyUp_TextBox(ref sender, ref e, "PutchFront1", "Desktop_Putch-Fornt");


        }





        /// <summary>
        ///  Откат предыдущего значения IP
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Undo_ipbd_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            // Имя параметра что откатывается
            var NameParam = "БДСервер1_IP";
            
            //Чтение параметра и запись нового значения
            var ItemTextBoxPatch = (TextBox)LogicalTreeHelper.FindLogicalNode(this, "TexIp1");
            ItemTextBoxPatch.Text = ConfigControll.aParamAppUndo[NameParam];

            if (SystemConfigControll.ControllerParam(ConfigControll.aParamAppUndo[NameParam], NameParam, 10))
            {
                // Поменять цвет иконки, а также вывести сообщение в окне диагностики (Разработка)
            }

            // Удалить из памяти
            ConfigControll.aParamAppUndo.Remove(NameParam);
            var ItemImageButtonIpDB = (Image)LogicalTreeHelper.FindLogicalNode(this, "Undo_ipbd");
            ItemImageButtonIpDB.Visibility = Visibility.Hidden;

        }

        /// <summary>
        /// Откат предыдущего значения Путь к фронту
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Undo_patch_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            // Имя параметра что откатывается
            var NameParam = "Desktop_Putch-Fornt";

            //Чтение параметра и запись нового значения
            var ItemTextBoxPatch = (TextBox)LogicalTreeHelper.FindLogicalNode(this, "PutchFront1");
            ItemTextBoxPatch.Text = ConfigControll.aParamAppUndo[NameParam];

            if (SystemConfigControll.ControllerParam(ConfigControll.aParamAppUndo[NameParam], NameParam, 11))
            {
                // Поменять цвет иконки, а также вывести сообщение в окне диагностики (Разработка)
            }

            // Удалить из памяти
            ConfigControll.aParamAppUndo.Remove(NameParam);
            var ItemImageButtonIpDB = (Image)LogicalTreeHelper.FindLogicalNode(this, "Undo_patch");
            ItemImageButtonIpDB.Visibility = Visibility.Hidden;
        }

        /// <summary>
        /// Опции
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OpciiStruct_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {


            // Включить
            if (this.OpciiStructGrid.Visibility == Visibility.Collapsed)
            {
                this.OpciiStructGrid.Visibility = Visibility.Visible;


            }
            else
            {
                //Выключить
                this.OpciiStructGrid.Visibility = Visibility.Collapsed;
            }
        }

        /// <summary>
        /// Диагностика
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DiagnoStruct_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            // Включить
            if (this.DiagnStructGrid.Visibility == Visibility.Collapsed)
            {
                this.DiagnStructGrid.Visibility = Visibility.Visible;
                    //var strPort = SystemConectoRS_XXX.GetPortName();
                    //if (strPort.Length > 0)
                    //{
                    //    DiagnSKDInfo.Text = strPort[0];
                    //    for (int i = 1; i < strPort.Length; i++)
                    //    {
                    //        DiagnSKDInfo.Text = DiagnSKDInfo.Text + "\n" + strPort[i];
                    //    }
                    //    // SystemConectoRS_XXX.ListCOMPortName();
                    //}
            }
            else
            {

                //Выключить
                this.DiagnStructGrid.Visibility = Visibility.Collapsed;

            }

        }


        #region

        /// <summary>
        /// Комбинации клавиш для полей ввода 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void KeyUp_TextBox(ref object sender, ref KeyEventArgs e, string NameTextBop, string NameParam)
        {

            if (e.Key == Key.Escape)
            {
                //Чтение параметра и запись нового значения
                var ItemTextBoxPatch = (TextBox)LogicalTreeHelper.FindLogicalNode(this, NameTextBop);
                ItemTextBoxPatch.Text = SystemConecto.aParamApp[NameParam];

                // Клавиши редактора (Могут отсутствовать)
                var ItemImageButtonDir = (Image)LogicalTreeHelper.FindLogicalNode(this, NameTextBop + "_Dir_Front");
                var ItemImageButtonEditDir = (Image)LogicalTreeHelper.FindLogicalNode(this, NameTextBop + "_Edit_Front");
                ItemImageButtonEditDir.Visibility = Visibility.Hidden;
                ItemImageButtonDir.Visibility = Visibility.Visible;

            }
            else
            {

                // Клавиши редактора
                var ItemImageButtonDir = (Image)LogicalTreeHelper.FindLogicalNode(this, NameTextBop + "_Dir_Front");
                var ItemImageButtonEditDir = (Image)LogicalTreeHelper.FindLogicalNode(this, NameTextBop + "_Edit_Front");
                ItemImageButtonEditDir.Visibility = Visibility.Visible;
                ItemImageButtonDir.Visibility = Visibility.Hidden;

            }



        }



        #endregion



        #endregion


        #region Закладка Осмотр Сети


        /// <summary>
        /// Осмор Сети Конфигурирование
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ViewNet_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

            

           

       
        }
        private void ViewNet_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            Thread thStartNet = new Thread(NetView_);
            thStartNet.SetApartmentState(ApartmentState.STA);
            //thStartWEB.IsBackground = true; // Фоновый поток
            thStartNet.Start();
            thStartNet.Priority = ThreadPriority.Highest;


            //this.ViewNet.Cursor = Cursors.Wait;

            //WaitMessage WindowWait = new WaitMessage();
            //WindowWait.Owner = this;  //AddOwnedForm(OblakoNizWindow);
            //// размещаем на рабочем столе
            ////AutirizWindow.Top = (this.Top + 7) + this.Close_F.Margin.Top + (this.Close_F.Height - 2) - 20;
            ////AutirizWindow.Left = (this.Left + 7) + this.Close_F.Margin.Left - (AutirizWindow.Width - 22) + 20;
            //// Отображаем
            //WindowWait.Show();

        }

        private void NetView_(object obj)
        {
            //MessageBox.Show("Тест");
            var NetAll = SystemConecto.ViewNet();




            // Пример XML кода
            // <StackPanel Grid.Row="1" Height="236" HorizontalAlignment="Left" Margin="23,63,0,0" Name="BlDevice1" VerticalAlignment="Top" Width="203">
            //    <Label Content="Устройство:" FontFamily="Calibri" FontSize="15" FontWeight="Normal" Foreground="Black" Height="29" IsEnabled="True" Name="label2" Target="{Binding}" Width="198" />
            //    <Label FontFamily="Calibri" FontSize="15" FontWeight="Normal" Foreground="Black" Height="8" IsEnabled="True" Name="label4" Target="{Binding}" Width="178" />
            //    <Image Cursor="Hand" FlowDirection="LeftToRight" Height="64" Name="NetD1" OverridesDefaultStyle="True" Source="pack://application:,,,/Conecto® WorkSpace;component/Images/computer-net.png" Stretch="None" StretchDirection="UpOnly" Visibility="Visible" Width="64" />
            //    <Label FontFamily="Calibri" FontSize="15" FontWeight="Normal" Foreground="Black" Height="8" IsEnabled="True" Name="label1" Target="{Binding}" Width="178" />
            //    <Label Content="Роль:" FontFamily="Calibri" FontSize="15" FontWeight="Normal" Foreground="Black" Height="29" IsEnabled="True" Name="Role_L" Target="{Binding}" Width="198" />
            //    <Label Content="IP-адресс:" FontFamily="Calibri" FontSize="15" FontWeight="Normal" Foreground="Black" Height="29" IsEnabled="True" Name="IP_L" Width="198" Target="{Binding}" />
            //    <Label Content="MAC-адресс:" FontFamily="Calibri" FontSize="15" FontWeight="Normal" Foreground="Black" Height="29" IsEnabled="True" Name="MAC_L" Target="{Binding}" Width="198" />
            //</StackPanel>



            var DeviceAddr = "";
            // Управление высотой размещения Устройств на карте сети
            //double[] MoveStack = new double[2] {this.NetGrid.Margin.Left + 10, 10 };
            var IdName = "";
            // Блок карты
            StackPanel BlMapNet = new StackPanel();
            BlMapNet.SetValue(Grid.RowProperty, 1);
            // Блок Устройства
            StackPanel BlDevice = new StackPanel();

            foreach (KeyValuePair<string, string> daniNet in NetAll)
            {


                // Определения устройства
                string[] IPDevice = daniNet.Key.Split('_');
                // Изменить уникально название єлементов устройства  
                IdName = IPDevice[0].Replace(".", "_");

                if (DeviceAddr != IPDevice[0])
                {

                    // Добавить в контент Блок карты скролинга
                    if (DeviceAddr.Length > 0)
                    {
                        BlMapNet.Children.Add(BlDevice);
                    }

                    // Новое устройство
                    DeviceAddr = IPDevice[0];

                    BlDevice = new StackPanel();      //BlDevice.Background = Brushes.Gray; // Пример установки фона
                    BlDevice.Name = "BlDevice_" + IdName; BlDevice.Height = 246; BlDevice.Width = 243;
                    BlDevice.HorizontalAlignment = HorizontalAlignment.Left; BlDevice.VerticalAlignment = VerticalAlignment.Top;
                    BlDevice.Margin = new Thickness(15, 10, 0, 0);
                    // MoveStack[1] = MoveStack[1] + BlDevice.Height;
                    // Описание
                    Label Ustr = new Label();
                    Ustr.Name = "Opis_L_" + IdName;
                    Ustr.Content = "Устройство: " + NetAll[IPDevice[0] + "_NETBIOS"]; Ustr.FontFamily = new FontFamily("Calibri"); Ustr.FontSize = 15; Ustr.Width = BlDevice.Width - 10;
                    BlDevice.Children.Add(Ustr);
                    // Отступ
                    Label Otst = new Label();
                    Otst.Content = ""; Otst.Height = 8;
                    BlDevice.Children.Add(Otst);
                    // Изображение устройства
                    Image ImDevice = new Image();
                    ImDevice.Name = "Devi_I_" + IdName; ImDevice.Cursor = Cursors.Hand; ImDevice.Width = 64; ImDevice.Height = 64;
                    ImDevice.Source = new BitmapImage(new Uri("pack://application:,,,/Conecto® WorkSpace;component/Images/computer-net.png"));
                    BlDevice.Children.Add(ImDevice);
                    // Отступ
                    Otst = new Label();
                    Otst.Content = ""; Otst.Height = 8;
                    BlDevice.Children.Add(Otst);
                    // Роль
                    Label RoleD = new Label();
                    RoleD.Name = "Role_L_" + IdName;
                    RoleD.Content = "Роль: "; RoleD.FontFamily = new FontFamily("Calibri"); RoleD.FontSize = 15; RoleD.Width = BlDevice.Width - 10;
                    BlDevice.Children.Add(RoleD);
                    // IP адресс
                    Label UstrIP = new Label();
                    UstrIP.Name = "OpisIP_L_" + IdName;
                    UstrIP.Content = "IP-адресс: " + IPDevice[0]; UstrIP.FontFamily = new FontFamily("Calibri"); UstrIP.FontSize = 15; UstrIP.Width = BlDevice.Width - 10;
                    BlDevice.Children.Add(UstrIP);
                    //BlDevice.Children.Add(Otst);

                    //BlDevice.SetValue(Grid.RowProperty, 1);
                    //BlDevice.SetValue(Grid.ColumnProperty, 1);




                }
                else
                {
                    // Продалжаем работать с определенным устройством
                    if (IPDevice[1] == "MAC")
                    {
                        // MAC адресс
                        Label UstrMAC = new Label();
                        UstrMAC.Name = "OpisMAC_L_" + IdName;
                        UstrMAC.Content = "MAC-адресс: " + daniNet.Value; UstrMAC.FontFamily = new FontFamily("Calibri"); UstrMAC.FontSize = 15; UstrMAC.Width = BlDevice.Width - 10;
                        BlDevice.Children.Add(UstrMAC);
                    }



                }





            }
            // Добавить в контент Блок карты скролинга
            if (DeviceAddr.Length > 0)
            {
                BlMapNet.Children.Add(BlDevice);
            }
            System.Windows.Application.Current.Dispatcher.Invoke(new Action(delegate()
            {

                // Знести данные структуры Карты сети в контент скролинга
                this.MapNet.Content = BlMapNet;
                //=====================================

                //this.ViewNet.Cursor = Cursors.Hand;
            }));

            // Закрыть окно ожидание
            //Window WindowWait = SystemConecto.ListWindowMain("WaitMessage_");

            //if (WindowWait != null)
            //{
            //    // Не активировать окно - не передавать клавиатурный фокус
            //    WindowWait.Close();
            //}

        }
        #endregion


        #region События закладки СКД система контроля доступа


        private void Opcii_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {


            // Включить
            if (this.OpciiSKDGrid.Visibility == Visibility.Collapsed)
            {
                this.OpciiSKDGrid.Visibility = Visibility.Visible;


            }
            else
            {
                //Выключить
                this.OpciiSKDGrid.Visibility = Visibility.Collapsed;
            }
        }




        /// <summary>
        /// Диагностика
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void label8_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            // Включить
            if (this.DiagnSKDGrid.Visibility == Visibility.Collapsed)
            {
                this.DiagnSKDGrid.Visibility = Visibility.Visible;
                var strPort = SystemConectoRS_XXX.GetPortName();
                if (strPort.Length > 0)
                {
                    DiagnSKDInfo.Text = strPort[0];
                    for (int i = 1; i < strPort.Length; i++)
                    {
                        DiagnSKDInfo.Text = DiagnSKDInfo.Text + "\n" + strPort[i];
                    }
                    // SystemConectoRS_XXX.ListCOMPortName();
                }
            }
            else
            {

                //Выключить
                this.DiagnSKDGrid.Visibility = Visibility.Collapsed;

            }



        }




        /// <summary>
        /// Перемещение по конвертерам
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dataGridConvert_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {


            var ItemNameDataGrid = (System.Windows.Controls.DataGrid)LogicalTreeHelper.FindLogicalNode(this, "dataGridConvert");

            if (ItemNameDataGrid != null)
            {
                // MessageBox.Show(ItemNameDataGrid.SelectedIndex.ToString());
                //DataRowView rowView = ItemNameDataGrid.SelectedValue as DataRowView;
                //MessageBox.Show(rowView[0].ToString());
                //значение строки в какой-то колонке
                //rowView[0].ToString(); // вернет значение выбранной строки в первой колонке
                SelectiondataGridConvert(dataGridConvert_IndexItems(ref ItemNameDataGrid));
                // ItemNameDataGrid.rows


            }

            //dgv_leftPanel.Rows[e.RowIndex].Selected = true;
            //dgv_leftPanel.RowsDefaultCellStyle.SelectionForeColor = Color.Red;
        }

        /// <summary>
        /// Смена выбора конвектора
        /// </summary>
        private void SelectiondataGridConvert(int ItemIndex = 0)
        {
            // Обновление информации
            if (ItemIndex == -1)
            {
                return;
            }
            var LabelItemNameDataGrid = (Label)LogicalTreeHelper.FindLogicalNode(this, "LPortFr");
            LabelItemNameDataGrid.Content = SystemConectoSKDServer.TableListConvectorSKD.Count > ItemIndex ? SystemConectoSKDServer.TableListConvectorSKD[ItemIndex].PortFr : "";

            LabelItemNameDataGrid = (Label)LogicalTreeHelper.FindLogicalNode(this, "LPortName");
            LabelItemNameDataGrid.Content = SystemConectoSKDServer.TableListConvectorSKD.Count > ItemIndex ? SystemConectoSKDServer.TableListConvectorSKD[ItemIndex].PortName : "";

            LabelItemNameDataGrid = (Label)LogicalTreeHelper.FindLogicalNode(this, "LPid");
            LabelItemNameDataGrid.Content = SystemConectoSKDServer.TableListConvectorSKD.Count > ItemIndex ? SystemConectoSKDServer.TableListConvectorSKD[ItemIndex].Pid : "";

            LabelItemNameDataGrid = (Label)LogicalTreeHelper.FindLogicalNode(this, "LSn");
            LabelItemNameDataGrid.Content = SystemConectoSKDServer.TableListConvectorSKD.Count > ItemIndex ? SystemConectoSKDServer.TableListConvectorSKD[ItemIndex].Sn.ToString() : "";

            LabelItemNameDataGrid = (Label)LogicalTreeHelper.FindLogicalNode(this, "LVer");
            LabelItemNameDataGrid.Content = SystemConectoSKDServer.TableListConvectorSKD.Count > ItemIndex ? SystemConectoSKDServer.TableListConvectorSKD[ItemIndex].Version.ToString() : "";

            LabelItemNameDataGrid = (Label)LogicalTreeHelper.FindLogicalNode(this, "LMode");
            LabelItemNameDataGrid.Content = SystemConectoSKDServer.TableListConvectorSKD.Count > ItemIndex ? SystemConectoSKDServer.TableListConvectorSKD[ItemIndex].Mode.ToString() : "";

            //LabelItemNameDataGrid = (Label)LogicalTreeHelper.FindLogicalNode(this, "LPortType");
            //LabelItemNameDataGrid.Content = SystemConectoSKDServer.TableListConvectorSKD.Count > ItemIndex ? SystemConectoSKDServer.TableListConvectorSKD[ItemIndex].PortType.ToString() : "";

            // SystemConectoSKDServer.TableListConvectorSKD[ItemIndex].Mode

            // Список скоростей портов
            var mlTableListConvectorSKDSpeed = new List<int> { 56700, 19200 };
            var ComboItemNameDataGrid = (ComboBox)LogicalTreeHelper.FindLogicalNode(this, "ComboSpeedConvert");
            ComboItemNameDataGrid.ItemsSource = mlTableListConvectorSKDSpeed;
            if (ItemIndex > -1)
            {
                ComboItemNameDataGrid.Text = SystemConectoSKDServer.TableListConvectorSKD[ItemIndex].nPortFrSpeed.ToString();
            }// ComboItemNameDataGrid.SelectedValue = SystemConectoSKDServer.TableListConvectorSKD[ItemIndex].nPortFrSpeed.ToString();


            var ParamConvert = SystemConectoSKDServer.TableListConvectorSKD[ItemIndex] as ColumnsConvertSKD;
            // Чтение контроллеров
            // SystemConectoSKDServer.OpenPortConvert(ref ParamConvert);


        }

        /// <summary>
        /// Смена значения скорости порта
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ComboSpeedConvert_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var ComboItemNameDataGrid = (ComboBox)LogicalTreeHelper.FindLogicalNode(this, "ComboSpeedConvert");
            var ItemNameDataGrid = (System.Windows.Controls.DataGrid)LogicalTreeHelper.FindLogicalNode(this, "dataGridConvert");
            var Select = (int)ComboItemNameDataGrid.SelectedValue;
            var IndexData = dataGridConvert_IndexItems(ref ItemNameDataGrid);
            if (ItemNameDataGrid.SelectedIndex > -1 && SystemConectoSKDServer.TableListConvectorSKD[IndexData].nPortFrSpeed != Select)
            {
                SystemConfigControll.ControllerParamSKD(Select.ToString(), string.Format("Convertor-{0}_Speed", SystemConectoSKDServer.TableListConvectorSKD[IndexData].Pid));
                SystemConectoSKDServer.TableListConvectorSKD[IndexData].nPortFrSpeed = Select;
               // SystemConectoSKDServer.TableListConvectorSKD[IndexData].NameNode = "Привет от комбобокса";
                // MessageBox.Show("Сменил");
            }

        }

        /// <summary>
        /// При смене свойств подключения конвектора, отображение изменений состояния перечня конвекторов и их свойств
        /// Пока не работает
        /// </summary>
        /// <param name="ItemIndex"></param>
        public static void RefrechdataGridConvert(int ItemIndex = 0)
        {
            // Обновление информации
            var WindowAdmin = SystemConecto.ListWindowMain("Admin");
            var ItemNameDataGrid = (System.Windows.Controls.DataGrid)LogicalTreeHelper.FindLogicalNode(WindowAdmin, "dataGridConvert");
            if (ItemNameDataGrid != null)
            {
                ItemNameDataGrid.Items.Refresh();
                dynamic Null = "";
                //dataGridConvert_SelectedCellsChanged(WindowAdmin, Null);
                // Выбор ячейки
                // SelectiondataGridConvert();
            }


            // Чтение контроллеров

        }

        #region Разработки
        //// Редактирование ячейки
        //private void dataGridConvert_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
        //{
           

        //}
        //// Поиск решения, может не нужно
        //private void dataGridConvert_SelectionChanged(object sender, SelectionChangedEventArgs e)
        //{


        //}
        ///// <summary>
        ///// Редактирование строки ...
        ///// </summary>
        ///// <param name="sender"></param>
        ///// <param name="e"></param>
        //private void dataGridConvert_RowEditEnding(object sender, DataGridRowEditEndingEventArgs e)
        //{
        //   // RowEditEnding
        //    //var ItemNameDataGrid = (DataGrid)LogicalTreeHelper.FindLogicalNode(this, "dataGridConvert");
        //    //foreach (ColumnsConvertSKD LineSKD in SystemConectoSKDServer.TableListConvectorSKD)
        //    //{


        //    //    MessageBox.Show(LineSKD.NameNode);

        //    //}
        //}
        #endregion

        #region Мини утилиты

        /// <summary>
        /// Вернуть индекс текущей записи в представлении (масиве данных, а не таблице datagrid)
        /// </summary>
        /// <param name="sender"></param>
        public static int dataGridConvert_IndexItems(ref System.Windows.Controls.DataGrid sender)
        {
            // Нличие ресурса данных в гриде
            if (sender.HasItems)
            {
                // Элемент данных, привязанный к строке отсутсвует
                if (sender.CurrentItem != null)
                {

                    ColumnsConvertSKD Line = sender.CurrentItem as ColumnsConvertSKD;
                    // MessageBox.Show(Line.id.ToString());
                    return Line.id;

                }
                // Проверка на выбор строки без sender.CurrentItem
                if (sender.SelectedIndex > -1)
                {
                    return sender.SelectedIndex;

                }

            }

            // Считать структуру даных и их значения
            // ColumnsConvertSKD Line = ItemNameDataGrid.CurrentItem as ColumnsConvertSKD;
            // MessageBox.Show(Line.Sn.ToString());
            return -1;
        }




























        //======================================

        ///// <summary>
        ///// Тест как добовлять програмнно обработчики событий
        ///// this.Activated событие += new EventHandler(EndEdit);
        ///// </summary>
        //void EndEdit(object sender, EventArgs e)
        //{
        //    MessageBox.Show("Test");

        //}

        #endregion

        #endregion

        #region Изменение бызовых свойств DataGrid Обучение

        /// <summary>
        /// данные в исходной коллекции обновляются не после того как вы закончите редактировать непосредственно ячейку,
        /// а только после того как закончите редактировать всю строку целиком.
        /// Для того чтобы получить нужное поведение грида оказалось достаточным в обработчике события CellEditEnding 
        /// заставлять грид сохранять все имеющиеся изменения.
        /// <DataGrid  ItemsSource="{Binding Tags}" presentation:DataGridHelper.PerCellCommit="True">
        /// 
        /// </summary>
        //public static class DataGridHelper
        //{
        //public static readonly DependencyProperty PerCellCommitProperty =
        //    DependencyProperty.RegisterAttached(
        //        "PerCellCommit",
        //        typeof(bool),
        //        typeof(DataGrid),
        //        new PropertyMetadata(false, PerCellCommitChanged));

        //public static bool GetPerCellCommit(DependencyObject obj)
        //{
        //    // Чтение
        //    return (bool)obj.GetValue(PerCellCommitProperty);
        //}

        //public static void SetPerCellCommit(DependencyObject obj, bool value)
        //{
        //    // Запись
        //    obj.SetValue(PerCellCommitProperty, value);
        //}

        //// Запись Attached property PerCellCommit, которое будет подписывать нужный обработчик на событие CellEditEnding.
        //public static void PerCellCommitChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e)
        //{
        //    var grid = obj as DataGrid;
        //    if (grid == null)
        //        return;

        //    if ((bool)e.NewValue)
        //    {
        //        grid.CellEditEnding += Grid_CellEditEnding;
        //    }
        //    else
        //    {
        //        grid.CellEditEnding -= Grid_CellEditEnding;
        //    }
        //}

        //private static bool _isManualEditCommit;

        //// 
        //private static void Grid_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
        //{
        //    if (_isManualEditCommit)
        //        return;

        //    if (e.EditAction != DataGridEditAction.Commit)
        //        return;

        //    _isManualEditCommit = true;
        //    var grid = (DataGrid)sender;
        //    grid.CommitEdit(DataGridEditingUnit.Row, true);
        //    _isManualEditCommit = false;
        //}




        //}




        //public class DataGridPerCellCommitBehavior : Behavior<DataGrid>
        //{
        //    private static bool _manualEditCommit;

        //    protected override void OnAttached()
        //    {
        //        base.OnAttached();

        //        AssociatedObject.CellEditEnding += Grid_CellEditEnding;
        //    }

        //    protected override void OnDetaching()
        //    {
        //        base.OnDetaching();

        //        AssociatedObject.CellEditEnding -= Grid_CellEditEnding;
        //    }

        //    private void Grid_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
        //    {
        //        if (_manualEditCommit)
        //            return;

        //        if (e.EditAction != DataGridEditAction.Commit)
        //            return;

        //        _manualEditCommit = true;
        //        var grid = (DataGrid)sender;
        //        grid.CommitEdit(DataGridEditingUnit.Row, true);
        //        _manualEditCommit = false;
        //    }
        //}

        #endregion
       






    }



  



}
