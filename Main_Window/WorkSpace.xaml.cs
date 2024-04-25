using System;
using Microsoft.Win32;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.ServiceProcess;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Resources;
/// this.Convert_
using System.Drawing.Imaging;
using System.Globalization;
using System.IO;
// --- Другие приложения подключаем возможность использовать WinApi-функции Для открітия других программ
using System.Runtime.InteropServices;
//--- Анимация
using System.Windows.Media.Animation;
/// Многопоточность
using System.Threading;
// --- Process 
using System.Diagnostics;
using System.ComponentModel; // Win32Exception
// Загрузка библиотек
using System.Reflection;
using ConectoWorkSpace.Administrator;

namespace ConectoWorkSpace
{

    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {


        public static string[] NameVBackg_a = new string[2] { "", "" }; // [1] Имя изоражения фона
     
        /// <summary>
        /// Тригерр количества иконок в информационной панели значков в нижнем углу слева
        /// </summary>
        public static int NumberElementInfoPanel = 0;
        /// <summary>
        /// Отображение панели задач: false - закрыта, true - открыта
        /// </summary>
        public bool TaskWinView = false;
        public static int NumberInput = 0, QuantityInput = 5;

        #region MainWindow формирование окна

        public MainWindow()
        {

 
            // 1.
            InitializeComponent();
 
            // Завершение иницианализации настройка интерфейса // Отладка
            // MessageBox.Show(SystemConecto.LoginUserAutoriz);

            // 2. Дополнительно проверить запуск explorer для режима Терминал
            var getAllExploreProcess = Process.GetProcesses().Where(r => r.ProcessName.Contains("explorer"));

            WindowState BackValue_WS = this.WindowState;
            if (SystemConecto.TerminalStatus || getAllExploreProcess.Count() == 0)
            {
                BackValue_WS = WindowState.Maximized;
            }

            // ======   Доп панель в панели задач (проверка режима терминал) Роли и авториация
            if (SystemConecto.TerminalStatus)
            {
                ButCatalog.Visibility = Visibility.Hidden;
                ButKeyboard.Visibility = Visibility.Hidden;
                Note.Visibility = Visibility.Hidden;

                HookSystemKeys.FunHook();// Запрещаем системные клавиши
                

            }

 
            // Выключить элементы интерфейса свернуть и закрыть
            if (AppStart.rkAppSetingAllUser.GetValue("ImPerekluchTerminal") != null && (int)AppStart.rkAppSetingAllUser.GetValue("ImPerekluchTerminal") == 1)
            {
                          
                MinimiButIm.Visibility = Visibility.Collapsed;
                Note.Visibility = Visibility.Collapsed;
                Calcul.Visibility = Visibility.Collapsed;
                Close_F.Visibility = Visibility.Collapsed;
                ButCatalog.Visibility = Visibility.Collapsed;
            }
            if (App.aSystemVirable["UserWindowIdentity"] != "1")
            {
                AdminButIm_.Visibility = Visibility.Hidden;
            }
                

            //AdminButIm_Old.Visibility = Visibility.Collapsed;
            TeamViewer.Visibility = Visibility.Collapsed;
            // 3. Динамическая прорисовка интерфейса и Динамические елементы
            numberElementsInfoPa(1);
            ResizeConectoWorkSpace(BackValue_WS);


            // Статичекая прорисовка при старте системы
            // Левая нижняя панель индикаторов
            this.KeyB52.Visibility = Visibility.Collapsed;
            this.Bd_Terminal.Visibility = Visibility.Collapsed;
            this.DeviceOnOff.Visibility = Visibility.Collapsed;

            // Левая панель управления
            this.ServerButIm_.Visibility = Visibility.Hidden;
            FonButton1.Visibility = Visibility.Hidden;
            FonButton2.Visibility = Visibility.Hidden;

            // this.Time_L.Text = DateTime.Now.ToString("HH:mm");  // Установка времени
            // Проверка шрифта
            this.Time_L_hh.Content = DateTime.Now.ToString("HH", CultureInfo.CreateSpecificCulture("ru-RU"));
            this.Time_L_hh.FontFamily = new FontFamily(AppStart.FontMain["Swiss921 BT"]);  // "file:///" + SystemConecto.PutchApp + @"bin\#Swiss921 BT"SWZ921N.TTF
            this.Time_L_mm.Content = DateTime.Now.ToString("mm", CultureInfo.CreateSpecificCulture("ru-RU"));
            this.Time_L_mm.FontFamily = new FontFamily(AppStart.FontMain["Swiss921 BT"]);
            //this.Time_L_mm.FontFamily = FontFamily.Source.
            this.Date.Content = DateTime.Now.ToString(@"ddd  d.MM", CultureInfo.CreateSpecificCulture("ru-RU"));
            this.Date.FontFamily = new FontFamily(AppStart.FontMain["Myriad Pro Cond"]);

            
            // 4. Прорисовка флеш накопителя
            DeviceRemovable();


            // 5. Включение Автоматических задач (Таймер) - Запуск служб и потоков

            AppStart.TimerWorkSpace();

            // 6. Запуск блокировки файлов - функция безопасности
           // SystemConecto.File_Block(SystemConecto.PutchApp + "config.xml", ref SystemConecto.XMLConfigFile);



            // 7. закрыть заставку - Нужна проверка на наличие ?
            Splasher_startWindow.Splasher.CloseSplash();


            // 8. Тип сборки
            if (SystemConecto.ReleaseCandidate == "Release")
            {
                PrivteCabinet.Visibility = Visibility.Collapsed;
                Consalting.Visibility = Visibility.Collapsed;
                Market.Visibility = Visibility.Collapsed;
                Videoinst.Visibility = Visibility.Collapsed;
                // Manual.Visibility = Visibility.Collapsed;
                TaskWindow.Visibility = Visibility.Collapsed;

                // Режим Терминала или RDP начальный старт
                // Выключить клавиши нет файла ключа на флешки 
                //if ((int)AppStart.rkAppSetingAllUser.GetValue("ImPerekluchTerminal") == 1)
                //{
                //    Note.Visibility = Visibility.Collapsed;
                //    Calcul.Visibility = Visibility.Collapsed;
                //}



            }




            #region Разработки

            // Тест
            //this.progressBar1.Width = 150;
            //this.progressBar1.Height = 25;
            //this.progressBar1.IsIndeterminate = true;
            //this.progressBar1.Value = 1;

            //this.Background = new ImageBrush(this.Convert_(global::ConectoWorkSpace.Properties.Resources._1366_x_768_));
            
            //------------------------------------ Завантаження зображеннь та файлів з ресурсу
            //    <Window.Resources>
            //     <Image Source="_1366_x_768_" x:Key="_1366_x_768_"></Image>
            //    </Window.Resources>
            //tslMode.Image = global::ConectoWorkSpace.Properties.Resources._1366_x_768_;
           

            //StreamResourceInfo sr = Application.GetResourceStream(
            //    new Uri("SilverlightApplication1;component/MyImage.png", UriKind.Relative));
            //BitmapImage bmp = new BitmapImage();
            //bmp.SetSource(sr.Stream);

            //var Test = global::ConectoWorkSpace.Properties.Resources._1366_x_768_);
            //Bitmap myImage = (Bitmap)Test;
            //-----------------------------------

            // this.Background = new ImageBrush(doGetImageSourceFromResource("Conecto®%20WorkSpace", "1366_x_768"));
            //this.Test.Source = this.Convert_(global::ConectoWorkSpace.Properties.Resources.knop1_1);
            #endregion
        }

        #endregion

        #region Изменение размера разрешения экрана
        /// <summary>
        /// Изменение размера разрешения экрана
        /// </summary>
        public void ResolutionDisplay()
        {
            var BackValue_RM = this.ResizeMode;
            // Разрешить изменять размер
            if (this.ResizeMode == ResizeMode.NoResize)
            {
                this.ResizeMode = ResizeMode.CanResize;
                // Востановить предыдущие значение
            }
            // 3. Установка размеров окна для того чтобы игнорировать нажатие клавиши раскрыть на весь экран
            // Рабочего стола ([0] - Top; [1] - Left; [2] - Width; [3] - Height; [4] - Right;
            this.Top = SystemConecto.WorkAreaDisplayDefault[0];
            this.Left = SystemConecto.WorkAreaDisplayDefault[1];
            this.Width = this.WindGrid.Width = SystemConecto.WorkAreaDisplayDefault[2];
            this.Height = this.WindGrid.Height = SystemConecto.WorkAreaDisplayDefault[3];

            //this.Location = new System.Drawing.Point(SizeDWArea_a[1], SizeDWArea_a[0]);
            //this.ClientSize = new System.Drawing.Size(SizeDWArea_a[2], SizeDWArea_a[3]);

            // Востановить предыдущие значение
            this.ResizeMode = BackValue_RM;

        }

       
        #endregion

        #region Значек флешки
        //ListDeviceRemovable
        private void DeviceRemovable()
        {
            var ListDevice = SystemConecto.ListDeviceRemovable();
            if (ListDevice.Count > 0)
            {
                this.DeviceOnOff.Visibility = System.Windows.Visibility.Visible;
            }
        }

        #endregion

        #region Динамическое размещение елементов (кнопок, подписей и ...) в основном окне и его дочерних окнах
        /// <summary>
        /// Обновление ConectoWorkSpace (Прорисовка елементов)
        /// 1 - включить CanResize
        /// </summary>
        public void ResizeConectoWorkSpace(WindowState WinSt = WindowState.Normal, int TypeResize = 0)
        {

            // По умолчанию ResizeMode.NoResize - 0
            if (TypeResize == 0)
            {
                this.ResizeMode = ResizeMode.NoResize;
            }
            else
            {
               this.ResizeMode = ResizeMode.CanResize;
            }

            // Вариант открытия окна
            if (WinSt != this.WindowState)
            {
                var BackValue_RM = this.ResizeMode;
                // Разрешить изменять размер
                if (this.ResizeMode == ResizeMode.NoResize)
                {
                    this.ResizeMode = ResizeMode.CanResize;
                    
                }

                this.WindowState = WinSt;

                // Востановить предыдущие значение
                this.ResizeMode = BackValue_RM;
            }

            // Динамическая прорисовка интерфейса и Динамические елементы
            // Проверка необходимых элементов системы 
            ChekSystems(NameVBackg_a);


            if (NameVBackg_a[1] != "")
            {
                //this.BackgroundImage = Image.FromFile(NameVBackg_a[1]);

                // this.Background = new ImageBrush(new BitmapImage(new Uri( ));
                // this.Background = new ImageBrush(new BitmapImage(new Uri(@"file:///" + NameVBackg_a[1])));

            }

            // Выполнение отрисовки интерфейса при изменении разрешения Шаг 3 -  сама контролирует ResizeMode.CanResize
            ResolutionDisplay();


            // 4. Отрисовка интерфейса
            ControlsElementsSizeStart();

            // 4.2 Центральная панель WorkSpace
            LoadPanelWorkSpace(); 

            // Отключить изменять размеры - Окно не меняет размеров даже если меняеш размер TaskBarWindows 
            // TypeResize == 1 - Оставить без изменений тоесть ResizeMode.CanResize
            //if (TypeResize == 0)
            //{
            //    this.ResizeMode = ResizeMode.NoResize;
            //}

            // Пройти все окна и обновить их разрешениеSystem.Windows.Window
            foreach (dynamic openWindow in System.Windows.Application.Current.Windows)
            {
                if (openWindow.Name == "ConectoWorkSpace")
                {
                    //openWindow.
                }
                else
                {
                    try{
                        openWindow.ResolutionDisplay();
                    }catch
                    {

                    }

                    //openWindow.Visibility = Visibility.Visible;
                }
            }

            //TerminalFront.IsEnabled = false;
            //if (AppStart.TableReestr["TerminalOnOff"] == "1") TerminalFront.IsEnabled = true;

        }

        #endregion

        #region Загрузка и формирование панели ... Conecto WorkSpace ...
        /// <summary>
        /// Загрузка и формирование панели Conecto WorkSpace
        /// </summary> 
        public void LoadPanelWorkSpace()
        {
            int NumberApp = 1;

            WorkSpacePanelGrid.Children.Clear();

             

            //установить его ширину
            WorkSpacePanel.Width = WorkSpacePanelGrid.Width = SystemConecto.WorkAreaDisplayDefault[7] - WorkSpacePanel.Margin.Left - 10;
            // Установить его высоту
            WorkSpacePanel.Height = WorkSpacePanelGrid.Height = Conector.Margin.Top - WorkSpacePanel.Margin.Top - 15;

            // Расчет сетки 
            int CountCells = Convert.ToInt32(Math.Truncate(WorkSpacePanel.Width / 145));
            int CountRows = Convert.ToInt32(Math.Truncate(WorkSpacePanel.Height / 145));

            // Id
            var IdApp = 0;

            for (int nCells = 0; nCells < CountCells; nCells++)
            {
                // ============= Сформировать ячейку
                // http://msdn.microsoft.com/ru-ru/library/ms612655%28v=vs.95%29.aspx
                // Сформировать колонку
                ColumnDefinition cl = new ColumnDefinition();
                cl.Width = new GridLength(145, GridUnitType.Pixel);
                
                WorkSpacePanelGrid.ColumnDefinitions.Add(cl);

               

                for (int nRows = 0; nRows < CountRows; nRows++)
                {
                    // ============= Сформировать ячейку
                    // Сформировать строку
                    if (nCells==0)
                    {
                        RowDefinition r = new RowDefinition();
                        r.Height = new GridLength(140, GridUnitType.Pixel);
                        // Добавить в грид
                        WorkSpacePanelGrid.RowDefinitions.Add(r);

                        
                    }

                    // Расположить ярлык

                    // Проверка отображения приложения на панели LinkPanel
                    IdApp = SystemConfigControll.ListPanelWorkSpace.ContainsKey(NumberApp) && AppStart.SysConf.aParamAppPlay[SystemConfigControll.ListPanelWorkSpace[NumberApp]].PanelWS.LinkPanel ? SystemConfigControll.ListPanelWorkSpace[NumberApp] : 0;
                    //IdApp = SystemConfigControll.ListPanelWorkSpace.ContainsKey(NumberApp) ? SystemConfigControll.ListPanelWorkSpace[NumberApp] : 0;

                    if (SystemConfigControll.ListPanelWorkSpace.ContainsKey(NumberApp) && IdApp > 0)
                    {

                        //SystemConecto.ErorDebag(nCells.ToString() + " / " + nRows.ToString() + " / " + NumberApp.ToString());
                        var PanelWS_ = AppStart.SysConf.aParamAppPlay[IdApp].PanelWS;

                        string Puthdll = SystemConecto.PutchApp + @"bin\dll\" + PanelWS_.TypeApp;

                        if (PanelWS_.TypeApp == "Integer" || SystemConecto.File_(Puthdll, 5))
                        {
                        // 
                            //var TypeApp_ = AppStart.SysConf.aParamAppPlay[SystemConfigControll.ListPanelWorkSpace[NumberApp]].PanelWS.TypeApp;
                            //var AppStartMetod_ = AppStart.SysConf.aParamAppPlay[SystemConfigControll.ListPanelWorkSpace[NumberApp]].PanelWS.AppStartMetod; 


                            System.Reflection.MethodInfo loadAppEvents = null;
                            Assembly testAssembly = null;
                            string PuthImage = "";

                            if(PanelWS_.TypeApp == "Integer")
                            {  PuthImage = AppStart.SysConf.aParamAppPlay[IdApp].PuthFileIm; }
                            else
                            {
                                
                                // dynamically load assembly from file Test.dll
                                testAssembly = Assembly.LoadFile(Puthdll);

                                // Проверить переменную PutchImagePanel_1
                                PuthImage = AppStart.SysConf.aParamAppPlay[IdApp].PuthFileIm;
                            }

                            Image Image_WorkS = new Image();

                            if (AppStart.SysConf.aParamAppPlay[IdApp].PuthFileIm == "")
                            {

                            }
                            else
                            {
                                Image_WorkS.Source = new BitmapImage(new Uri(PuthImage, UriKind.Relative));
                            }

                            Image_WorkS.Stretch = Stretch.None;
                            

                            Grid.SetRow(Image_WorkS, nRows);
                            Grid.SetColumn(Image_WorkS, nCells);

                            Image_WorkS.Cursor = System.Windows.Input.Cursors.Hand;

                            // Через параметры
                            string[] aClass = PanelWS_.AppStartMetod.Split('.');
                            if (aClass.Length > 1)
                            {
                                string NameMetod = aClass[aClass.Length - 1];
                                string NameClass = PanelWS_.AppStartMetod.Remove(PanelWS_.AppStartMetod.Length - NameMetod.Length - 1);
                                //string NameClass = "StarSound.ClientKaraoke";
                
                                // Поиск класса
                                Type SherchClass = null;
                                 
                                if(PanelWS_.TypeApp == "Integer"){
                               
                                    SherchClass = Type.GetType(NameClass);
                                 }
                                else
                                {
                                
                                    // get type of class Calculator from just loaded assembly"Test.Calculator"
                                    SherchClass = testAssembly.GetType(NameClass);    

                                  }
                               

                                //var Nrty = Type.Missing;

                                // SherchClass.GetType() то же самое, что и typeof(ConectoWorkSpace.KaraokeServer), однако если Type это уже SherchClass

                                // Отладка - реализовать запуск с настройки конфиг файла



                                if (SherchClass != null)
                                {
                                    //System.Reflection.MethodInfo loadAppEvents = typeof(ConectoWorkSpace.KaraokeServer).GetMethod(NameMetod, new Type[] { }); // typeof(void)
                                    loadAppEvents = SherchClass.GetMethod(NameMetod, new Type[] { typeof(Window), typeof(Image), typeof(Image) });

                                    if (loadAppEvents != null)
                                    {
                                        // SystemConecto.ErorDebag("LoadAppEvents_" + IdApp, 2);
                                        loadAppEvents.Invoke(new object(), new object[] { this, Image_WorkS, Image_WorkS }); //new object()
                                    }

                                }
                            }

 

                            Image_WorkS.HorizontalAlignment = HorizontalAlignment.Left;
                            Image_WorkS.VerticalAlignment = VerticalAlignment.Top;

                            if (AppStart.SysConf.aParamAppPlay[SystemConfigControll.ListPanelWorkSpace[NumberApp]].PanelWS.LinkPanel)
                            {
                                //PanelWS_
                                Image_WorkS.Visibility = Visibility.Visible;
                                // AppPlayPanel

                                // Записать в таблицу
                                WorkSpacePanelGrid.Children.Add(Image_WorkS);

                                // Проверка авторизации
                                if (AppStart.SysConf.aParamAppPlay[IdApp].AutorizeType > -1)
                                {
                                    Image Image_WorkSSec = new Image();
                                    Image_WorkSSec.Source = new BitmapImage(new Uri("/Conecto®%20WorkSpace;component/Images/keyf.png", UriKind.Relative)); //pack://application:,,,/Conecto®%20WorkSpace;component/Images/keyf.png
                                    Grid.SetRow(Image_WorkSSec, nRows);
                                    Grid.SetColumn(Image_WorkSSec, nCells);
                                    Image_WorkSSec.Width = 38;
                                    Image_WorkSSec.Height = 38;
                                    Image_WorkSSec.HorizontalAlignment = HorizontalAlignment.Left;
                                    Image_WorkSSec.VerticalAlignment = VerticalAlignment.Top;
                                    Image_WorkSSec.Margin = new Thickness(84, 67, 0, 0);
                                    Image_WorkSSec.Cursor = System.Windows.Input.Cursors.Hand;

                                    // Отладка - реализовать запуск с настройки конфиг файла

                                    //loadAppEvents = typeof(AppforWorkSpace).GetMethod("LoadAppEvents_" + IdApp, new Type[] { typeof(Image) });
                                    if (loadAppEvents != null)
                                    {
                                        // SystemConecto.ErorDebag("LoadAppEvents_" + IdApp, 2);
                                        loadAppEvents.Invoke(this, new object[] { this, Image_WorkSSec, Image_WorkS });
                                    }

                                    WorkSpacePanelGrid.Children.Add(Image_WorkSSec);

                                }

                            }
                            else
                            {
                                Image_WorkS.Visibility = Visibility.Collapsed;
                            }
  

                        }
                        // PanelWS_.TypeApp
                    }
                    else
                    {
                        // Отладка
                        //Border Border_ = new Border();


                        //Border_.Height = 140;
                        //Border_.Width = 140;
                        //Grid.SetRow(Border_, nRows);
                        //Grid.SetColumn(Border_, nCells);
                        //Border_.BorderThickness = new Thickness(1, 1, 1, 1);

                        //Border_.BorderBrush = Brushes.Black; //New System.Windows.Media.Bor(Colors.LightGray);

                        //Border_.HorizontalAlignment = HorizontalAlignment.Center;
                        //Border_.VerticalAlignment = VerticalAlignment.Center;
                        //// Записать в таблицу
                        //WorkSpacePanelGrid.Children.Add(Border_);

                    }




                    NumberApp++;

                }
              

            }

            ColumnDefinition cl_ = new ColumnDefinition();
            cl_.Width = GridLength.Auto; // new GridLength(145, GridUnitType.Pixel);

            WorkSpacePanelGrid.ColumnDefinitions.Add(cl_);

            RowDefinition r_ = new RowDefinition();
            r_.Height = GridLength.Auto;
            // Добавить в грид
            WorkSpacePanelGrid.RowDefinitions.Add(r_);

            //foreach (KeyValuePair<int, int> AppPlayPanel in SystemConfig.ListPanelWorkSpace)
            //{

            //}
        }
        #endregion


        #region События Click (Клик) функцилнальных клавиш рабочего стола

        private void Image_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void Close_F_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

            WinOblakoNiz OblakoNizWindow = new WinOblakoNiz();
            OblakoNizWindow.Owner = this;  //AddOwnedForm(OblakoNizWindow);
            // размещаем на рабочем столе
            OblakoNizWindow.Top = (this.Top+7) + this.Close_F.Margin.Top + (this.Close_F.Height-2)-20;
            OblakoNizWindow.Left = (this.Left+7) + this.Close_F.Margin.Left - (OblakoNizWindow.Width - 22)+20;
            // Отображаем
            OblakoNizWindow.Show();
            //OblakoNizWindow.ShowDialog();

            // Закрыть приложение
            //Close();
        }
        private void Conector_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

        }
        private void Conector_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            var Window = SystemConecto.ListWindowMain("WaitFonW");
            if (Window != null)
            {
                Window.Show();
            }
            else
            {
                WaitFon FonWindow = new WaitFon();
                FonWindow.Owner = this;
                FonWindow.Show();
            }

            Conector WinConector = new Conector();
            WinConector.Owner = this;
            //363x646  // 73
            // MessageBox.Show(SystemConecto.WorkAreaDisplayDefault[1].ToString() + "//" +  this.Conector.Margin.Left.ToString() + "//" + this.Conector.Width.ToString());
            WinConector.Top = SystemConecto.WorkAreaDisplayDefault[0] + this.Conector.Margin.Top + this.Conector.Height - 632; //625
            WinConector.Left = SystemConecto.WorkAreaDisplayDefault[1] + this.Conector.Margin.Left + (65 / 2) - 353 / 2; //353
            // MainWindow winMain = new Conector_();
            // WinConector.Show();
            WinConector.ShowDialog();
        }


        /// <summary>
        /// Ссылка на сайт (Логотип)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BanerSite1_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            // Пример с сайтом - Process.Start("IExplore.exe", "http://www.google.ru/");
            System.Diagnostics.Process.Start("http://conecto.ua/");

        }

        #region Запуск функций системных проверок
        private void ConectInternet_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            AppStart.TickMemory1(1, -1);
            AppStart.TickTask1(1, 15);
        }
        #endregion

        #region Клавиши основных ярлыков на рабочем столе
      
       



        private void Terminal_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

        }


        #endregion

        #endregion


        #region Размещение элементов WorkSpace

        public void ControlsElementsSizeStart()
        {

            //Window window = this;
            var WorkAreaWind = Convert.ToInt32(SystemConecto.WorkAreaDisplayDefault[3]); // Convert.ToInt32(SystemParameters.WorkArea.Height);

            var WorkAreaWindWidth = Convert.ToInt32(SystemConecto.WorkAreaDisplayDefault[2]);  // Convert.ToInt32(SystemParameters.WorkArea.Width);

            // Перебор свойств
            // foreach (Control img in window.FindChildren<Image>())
            // {
            //   Console.WriteLine("Image source: " + img.Source);
            // }

            // Сначало картинки потом текст
            //var TypeElements = new string[2] {"Image", "Label" };
            //switch (TypeElements[0])
            //{
            //    case("Label"):
            //        IEnumerable<Label> Elements = window.FindChildren<Label>();
            //        break;
            //    default:
            //        IEnumerable<Image> Elements = window.FindChildren<Image>();
            //        break;
            //}

            // Перебор всех елементов WPF
            //foreach (object ctrl in ccChildren.GetChildren())

            // Перебор кода FORM
            //foreach (Control c in this.Controls)
            //    CheckBox cb = c as CheckBox;


            foreach (Image ctrl in this.FindChildren<Image>())
            {
                // Запомнить размещение до изменения
                ControlsElementsSizeCalc(ctrl, WorkAreaWind, WorkAreaWindWidth);
            }

            foreach (Label ctrl in this.FindChildren<Label>())
            {
                // Запомнить размещение до изменения
                ControlsElementsSizeCalc(ctrl, WorkAreaWind, WorkAreaWindWidth);
            }
            foreach (StackPanel ctrl in this.FindChildren<StackPanel>())
            {
                // Можно исключать двигать панели по имени **

                ControlsElementsSizeCalc(ctrl, WorkAreaWind, WorkAreaWindWidth);
            }
            //foreach (Border ctrl in this.FindChildren<Border>())
            //{
            //    // Можно исключать двигать панели по имени **

            //    ControlsElementsSizeCalc(ctrl, WorkAreaWind, WorkAreaWindWidth);
            //}

            // Размещение нижней панели
            double TopElementMargin = Convert.ToDouble(WorkAreaWind - 42); // -15 Отладка
            this.WindowTask.Margin = new Thickness(0, TopElementMargin, 0, 0);
            this.WindowTask.Width = this.WindowTaskFon.Width = WorkAreaWindWidth;
            // Выравнивание панели (разница двух средин на эту увеличить отступ панели от центра в право)
            this.PanelSpeedMenu1.Margin = new Thickness(WorkAreaWindWidth-528 - this.PanelSpeedMenu1.Width/2, 0, 0, 0);  // this.PanelSpeedMenu1.Margin.Left + (WorkAreaWindWidth > 800 ? WorkAreaWindWidth/2-800/2 : 0)
            this.PanelTaskWindow.Margin = new Thickness(0, 0, 0, 0);

        }


        public static List<SizeFindChildrenWorkSpace> TableListSizeFindChildrenWorkSpace = new List<SizeFindChildrenWorkSpace>();

        public class SizeFindChildrenWorkSpace
        {
            public string Name { get; set; }
            public double Margin_Top { get; set; }
            public double Margin_Left { get; set; }
            public double Width { get; set; }

        }

        /// <summary>
        /// Расчет модели размещения елементов (устаревшая модель FORM перемегрировала)
        /// </summary>
        private void ControlsElementsSizeCalc(dynamic ctrl, int WorkAreaWind, int WorkAreaWindWidth)
        {
            // Блоки размещения елементов
            // --------------------------------------------------------------------
            // |                                                         1 - 1/5      Y + Top
            // --------------------------------------------------------------------
            // |  4         |                              5    |
            // |            |                                   |       2
            // |            |                                   |
            // |            |                                   |
            // |            |                                   |
            // |            |                                   |
            // |            |                                   |
            //----------------------------------|----------------
            //           7  |                 3 |
            //              |                   |
            //----------------------------------------------------------------------
            //  X + Width

            // Запомнить размещение до изменения (разрешение 800x600)
            var ctrl_ = TableListSizeFindChildrenWorkSpace.Find(delegate(SizeFindChildrenWorkSpace Elements)
            {
                return Elements.Name == ctrl.Name;
            });

            if (ctrl_ == null)
            {
                // Отладка
                // SystemConecto.ErorDebag("Сохранение елементов - " + ctrl.Name.ToString(), 1);

                SizeFindChildrenWorkSpace Line = new SizeFindChildrenWorkSpace();
                Line.Name = ctrl.Name;
                Line.Margin_Left = ctrl.Margin.Left;
                Line.Margin_Top = ctrl.Margin.Top;
                Line.Width = ctrl.Width;

                TableListSizeFindChildrenWorkSpace.Add(Line);
                ctrl_ = Line;

                // Отладка
                //if (ctrl_.Name == "Date")
                //{
                //    SystemConecto.ErorDebag("Елемент Date при старте найден" + ctrl_.Margin_Top.ToString(), 1);
                //}
            }
            else
            {
                // Отладка
                //if (ctrl_.Name == "Date")
                //{
                    
                //    SystemConecto.ErorDebag("Елемент Date найден" + ctrl_.Margin_Top.ToString(), 1);
                //}
                // Отладка
                // SystemConecto.ErorDebag("Елемент найден", 1);
            }


            // Смещение
            var mlInSmechTop = 0;
            double TopElementMargin = ctrl_.Margin_Top; //ctrl.Margin.Top;
            //double LeftElementMargin = 0;

            // Определить в каком блоке размещен елемент
            var mlInBlok = 2;
            //--------------------------------------------------------------------
            // 1. Верхнии елементы (расположенны на 1/5 высоты или 30px) не смещаются вниз Location.Y - Margin.Top
            if (ctrl_.Margin_Top < 30) //ctrl.Margin.Top
            {
                mlInBlok = 1;
            }
            // 4. Елементы которые попадают в блок 4 (верхний левый блок шириной 150 и высотой 570) Location.X -Margin.Left
            if (ctrl_.Margin_Left + ctrl_.Width < 150 && ctrl_.Margin_Top < 410) //ctrl.Margin.Left + ctrl.Width < 150 && ctrl.Margin.Top
            {
                mlInBlok = 4;
                mlInSmechTop = 0 - WorkAreaWind / 600 * 40;
                TopElementMargin = ctrl_.Margin_Top + mlInSmechTop; // ctrl.Margin.Top
                //ctrl.Margin = new Thickness(0,ctrl.Margin.Top + mlInSmechTop, 0, 0);
            }
            // 5. Елементы которые попадают в блок 5 прямоугольник расчитанный W = 140*3  H = 140*2 (&& ctrl.Margin.Left < 120 + (140 * 3 - 50))
            if (ctrl_.Margin_Left > 120 && ctrl_.Margin_Left < 800 && ctrl_.Margin_Top < 410 && mlInBlok != 1) //ctrl.Margin.Left > 120 && ctrl.Margin.Left < 800 && ctrl.Margin.Top
            {
                // MessageBox.Show(ctrl.Name);
                mlInBlok = 5;
                // Смещение верха для панели № 5
                mlInSmechTop = WorkAreaWind / 600 * 40;
                TopElementMargin = ctrl_.Margin_Top + mlInSmechTop; //ctrl.Margin.Top
                // ctrl.Margin = new Thickness(0, ctrl.Margin.Top + mlInSmechTop, 0, 0);
            }

            // --- смещение по Y
            if (mlInBlok != 1 && mlInBlok != 4 && mlInBlok != 5)
            {
                TopElementMargin = ctrl_.Margin_Top + ((WorkAreaWind > 600 ? WorkAreaWind : 600) - 600) + mlInSmechTop; // ctrl.Margin.Top
                //ctrl.Margin = new Thickness(0, ctrl.Margin.Top + ((this.Height > 600 ? this.Height : 600) - 600) + mlInSmechTop, 0, 0);
            }
            //--------------------------------------------------------------------
            // 7. 
            if (ctrl_.Margin_Left + ctrl_.Width < 190 && mlInBlok != 4) //ctrl.Margin.Left + ctrl.Width
            {
                mlInBlok = 7;
                ctrl.Margin = new Thickness(ctrl_.Margin_Left, TopElementMargin, 0, 0); // Ставим только высоту //ctrl.Margin.Left
            }
            // 2. Eлементы справа от центра смещаются пропорционально, расчет ведется от разрешения 800x600
            if ((ctrl_.Margin_Left > 800 / 2 || (ctrl.Margin.Left > 800 / 2 && mlInBlok == 1)) && mlInBlok != 5) //ctrl.Margin.Left
            {
                mlInBlok = 2;
            }
            // 3. Елементы слева смещение от центра в лево (привязаны к центру), расчет ведется от разрешения 800x600
            // исключается блок 7
            if (ctrl_.Margin_Left < 800 / 2 && mlInBlok != 4 && mlInBlok != 5 && mlInBlok != 7) //ctrl.Margin.Left
            {
                mlInBlok = 3;
            }

            // 6. Eлементы справа 5 блока кторые прибиты к правому краю смещаются пропорционально от центра, расчет ведется от разрешения 800x600
            if (ctrl_.Margin_Left > 800 / 2 && mlInBlok == 5 && ctrl_.Margin_Left + ctrl_.Width > 700) //ctrl.Margin.Left // + ctrl_.Width
            {
                mlInBlok = 6;
            }
            // --- смещение по X
            if (mlInBlok == 2 || mlInBlok == 6)
            {
                ctrl.Margin = new Thickness(ctrl_.Margin_Left + ((WorkAreaWindWidth > 800 ? WorkAreaWindWidth : 800) - 800), TopElementMargin, 0, 0); //ctrl.Margin.Left
            }

            if (mlInBlok == 3)
            {
                // учитываем смещение от центра влево к елементу в разрешение 800x600 ctrl.Margin.Left - уже содержит длину ctrl.Width
                // смещение центра в право на 95 px
                // MessageBox.Show(ctrl.Name + " " + ctrl.Margin.Left.ToString(), "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                ctrl.Margin = new Thickness(((WorkAreaWindWidth > 800 ? WorkAreaWindWidth : 800) / 6 * 3) - (800 / 4 * 3) + ctrl_.Margin_Left, TopElementMargin, 0, 0); //ctrl.Margin.Left
                // Блок центрируется также как блок №5

            }
            if (mlInBlok == 5)
            {
                // Смещение центра для панели № 5
                //var mlInSmech = 3 * 136; // - mlInSmech
                // учитываем смещение от центра влево к елементу в разрешение 800x600 ctrl.Margin.Left - уже содержит длину ctrl.Width
                ctrl.Margin = new Thickness(((WorkAreaWindWidth > 800 ? WorkAreaWindWidth : 800) / 6 * 3) - (800 / 4 * 3) + ctrl_.Margin_Left, TopElementMargin, 0, 0);  //ctrl.Margin.Left
                //ctrl.Left = ((this.Width > 800 ? this.Width : 800) / 5 * 3 ) - (((800 / 2 + ctrl.Width) - ctrl.Margin.Left));
            }
            //--------------------------------------------------------------------

            //if (ctrl.GetType().ToString() == "System.Windows.Forms.Button")
            //{
            //MessageBox.Show(ctrl.Name);
            //}

            //var rach = (this.Height > 600 ? this.Height : 600) - 600;
            //MessageBox.Show(ctrl.Top.ToString() + "=" + ctrl.Margin.Top.ToString() + "+" + rach.ToString());
        }

        /// <summary>
        /// Расчет количества елементов в информационной панели иконок под часами<para></para>
        /// <param name="Number">Number : состояние кнопки вклчается 1 или отключается -1</param>
        /// </summary>
        public void numberElementsInfoPa(int Number)
        {
            NumberElementInfoPanel += Number;
            if (NumberElementInfoPanel < 4)
            {
                // Панель информационных иконок под часами
                this.wP_SysIndicat.Width = 130;
            }
            else
            {
                this.wP_SysIndicat.Width = (NumberElementInfoPanel-3) * (38 + 5) + 130;
            }

        }

        #region  Расположение елемента в WorkSpace
        /// <summary>
        /// Изменение базового расположения елемента
        /// </summary>
        public void WriteElemetWorkSapce(dynamic ctrl, SizeFindChildrenWorkSpace NewElements)
        {
            var ctrl_ = TableListSizeFindChildrenWorkSpace.Find(delegate(SizeFindChildrenWorkSpace Elements)
            {
                return Elements.Name == ctrl.Name;
            });

            if (ctrl_ != null)
            {
                //SystemConecto.ErorDebag("Сохранение елементов - " + ctrl_.Margin_Top.ToString() + "/" + NewElements.Margin_Top.ToString(), 1);


                // Удалить 
                if (TableListSizeFindChildrenWorkSpace.Remove(ctrl_))
                {
                   
                   // SystemConecto.ErorDebag("Елемент удален, кол. записей" + TableListSizeFindChildrenWorkSpace.Count.ToString(), 1);
                }
                
                // Добавить
                //SizeFindChildrenWorkSpace Line = new SizeFindChildrenWorkSpace();

                //Line.Margin_Left = NewElements.Margin_Left;
                //Line.Margin_Top = NewElements.Margin_Top;
                //Line.Width = NewElements.Width;

                //SystemConecto.ErorDebag("Елемент найден и изменен." + NewElements.Name, 1);

                TableListSizeFindChildrenWorkSpace.Add(NewElements);
                //SystemConecto.ErorDebag("Елемент найден кол. записей" + TableListSizeFindChildrenWorkSpace.Count.ToString(), 1);

                //foreach (SizeFindChildrenWorkSpace Elements_ in TableListSizeFindChildrenWorkSpace)
                //{
                //    SystemConecto.ErorDebag(" - " + Elements_.Name, 1);
                //}

            }
            else
            {
                
                // SystemConecto.ErorDebag("Елемент найден", 1);
            }

        }

        /// <summary>
        /// Изменение базового расположения елемента
        /// </summary>
        public SizeFindChildrenWorkSpace ReadElemetWorkSapce(dynamic ctrl)
        {
            var ctrl_ = TableListSizeFindChildrenWorkSpace.Find(delegate(SizeFindChildrenWorkSpace Elements)
            {
                return Elements.Name == ctrl.Name;
            });

            if (ctrl_ != null)
            {
                return ctrl_;

            }
            else
            {
                // Пустая структура
                //SizeFindChildrenWorkSpace Line = new SizeFindChildrenWorkSpace();
                //Line.Name = "";
                //Line.Margin_Left = 0;
                //Line.Margin_Top = 0;
                //Line.Width = 0;
                return null;
                // SystemConecto.ErorDebag("Елемент найден", 1);
            }

        }
        #endregion

        /// <summary>
        /// Расчет размещения окна сообщения относительно иконок в информационной панели иконок под часами<para></para>
        ///<param name="Number">Number : кнопка по порядку</param>
        /// </summary>
        public double[] MessageCoordinatInfoPa(int Number)
        {
            double[] TopWidth = new double[2] { 0, 0 };
            
            // Высчитываем длину блока по известным для расчета центра
            double centr = this.wP_SysIndicat.Width / 2;
            // Длина картнки
            double WithPic = 38 + 5;

            double RaznicaCentr = centr - ( 38 + (NumberElementInfoPanel - 1) * WithPic) / 2;

            // Определения порядка
            string Name = "";
            switch (Number)
            {
                case 1:
                    Name = "ConectInternet";
                    break;
                case 2:
                    Name = "KeyB52";
                    break;
                case 3:
                    Name = "Bd_Terminal";
                    break;
                case 4:
                    Name = "DeviceOnOff";
                    break;
            }
            // Ссылка на объект
            //var StacPanel = this.wP_SysIndicat;
            int CountPic = 0;
            foreach (Image Pic in this.wP_SysIndicat.FindChildren<Image>())
            {
                if (Pic.Visibility == Visibility.Visible)
                {
                    CountPic += 1;
                    if (Pic.Name == Name)
                    {
                        break;
                    }
                }

            }
            
            // Margin Left 6 + RaznicaCentr ... - 28 бегунок в окне
            TopWidth[1] = 6 + RaznicaCentr + (CountPic - 1) * WithPic - 49; // //+ 38 / 28

           // SystemConecto.ErorDebag(RaznicaCentr.ToString() + "/" + NumberElementInfoPanel.ToString() + "/" + WithPic.ToString());

            return TopWidth;
        }

        #endregion

        #region Проверка размеров рабочего пространства и установка фона (Во время старта и изменения разрешения!)
        // При изменении разрешения экрана картинку не менять! 
        private void ChekSystems(string[] aNameBackground)
        {
            //throw new NotImplementedException(); - пример заглушки

            // Необходим метод кторый будет вычислять в каком состоянии находится панель Пуск Windows и в зависимости от этого строить окно
            // Рабочего стола ([0] - Top; [1] - Left; [2] - Width; [3] - Height; [4] - Right;
            double[] aSizeDWArea = SystemConecto.WorkAreaDisplayDefault = SystemConecto.WorkAreaDisplay(this);
            // Режим теста
            // aSizeDWArea[2] = 800;
            // aSizeDWArea[3] = 599;
            // 1366 768  - о умолчанию            
            bool defFon = false;
            // Проверка по умолчанию
            // SystemConecto.ErorDebag("Я хочу картинку с размерами:" + aSizeDWArea[2].ToString() + "|" + aSizeDWArea[3].ToString() + ", и сназванием " );
            // 800 х 600
            if (aSizeDWArea[2] < 802 + aSizeDWArea[5] && aSizeDWArea[3] < 602 + aSizeDWArea[6])
            {
                aNameBackground[1] = SystemConecto.PutchApp + @"images\back_800_600.jpg";
            }
            else
            {
                // 1024 х 600
                if (aSizeDWArea[2] < 1026 + aSizeDWArea[5] && aSizeDWArea[3] < 602 + aSizeDWArea[6])
                {
                    aNameBackground[1] = SystemConecto.PutchApp + @"images\back_1024_600.jpg";
                }
                else
                {
                    // 1024 х 768
                    if (aSizeDWArea[2] < 1026 + aSizeDWArea[5] && aSizeDWArea[3] < 770 + aSizeDWArea[6])
                    {
                        aNameBackground[1] = SystemConecto.PutchApp + @"images\back_1024_768.jpg";
                    }
                    else
                    {
                        // 1152 х 864
                        if (aSizeDWArea[2] < 1154 + aSizeDWArea[5] && aSizeDWArea[3] < 865 + aSizeDWArea[6])
                        {
                            aNameBackground[1] = SystemConecto.PutchApp + @"images\back_1152_864.jpg";
                        }
                        else
                        {
                            // 1280 х 768
                            if (aSizeDWArea[2] < 1282 + aSizeDWArea[5] && aSizeDWArea[3] < 770 + aSizeDWArea[6])
                            {
                                aNameBackground[1] = SystemConecto.PutchApp + @"images\back_1280_768.jpg";
                            }
                            else
                            {
                                // 1280 х 800
                                if (aSizeDWArea[2] < 1282 + aSizeDWArea[5] && aSizeDWArea[3] < 802 + aSizeDWArea[6])
                                {
                                    aNameBackground[1] = SystemConecto.PutchApp + @"images\back_1280_800.jpg";
                                }
                                else
                                {
                                    if (aSizeDWArea[2] < 1368 + aSizeDWArea[5] && aSizeDWArea[3] < 770 + aSizeDWArea[6])
                                    {
                                        // ...back_1366_768.jpg - фон по умолчанию
                                        defFon = true;
                                        // Отладка
                                        // aNameBackground[1] = SystemConecto.PutchApp + @"images\back_1366_768.jpg";
                                    }
                                    else
                                    {
                                        // Устанавливаем большой размер
                                        aNameBackground[1] = SystemConecto.PutchApp + @"images\back_1600_1200.jpg";
                                        // ...back_1280_800.jpg
                                    }
                                }
                            }
                        }

                    }

                }
            }

            // Проверка изображения
            if (defFon == false && SystemConecto.File_(aNameBackground[1], 5) == false)
            {
                // Файл отсутствует - взять его с пакета приложения ConectoWorkSpace.pack
                SystemConecto.ErorDebag("Я хочу картинку с размерами:" + aSizeDWArea[2].ToString() + "|" + aSizeDWArea[3].ToString() + ", и сназванием " + aNameBackground[1]);
                var MessageIFP = new SystemConecto.MessageIFP();
                SystemConecto.IsFilesPack(new KeyValuePair<string, string>(aNameBackground[1],""), ref MessageIFP);
                // aNameBackground[1] = ""; // Режим теста
                // Отладка - MessageBox.Show(aNameBackground[1], "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
            // Режим теста
            // aSizeDWArea = View_DeviceSize(aSizeDWArea);

        }
     

       
        #endregion

        #region Изображения как клавиши - Визуализация интерфейса рабочего стола

        private void MinimiButIm_MouseMove(object sender, MouseEventArgs e)
        {
            var uriSource = new Uri(@"/Conecto®%20WorkSpace;component/Images/knop1_2.png", UriKind.Relative);
            this.MinimiButIm.Source = new BitmapImage(uriSource);

        }

        private void MinimiButIm_MouseLeave(object sender, MouseEventArgs e)
        {
            var uriSource = new Uri(@"/Conecto®%20WorkSpace;component/Images/knop1_1.png", UriKind.Relative);
            this.MinimiButIm.Source = new BitmapImage(uriSource);
        }

        private void Close_F_MouseMove(object sender, MouseEventArgs e)
        {
            var uriSource = new Uri(@"/Conecto®%20WorkSpace;component/Images/knop2_2.png", UriKind.Relative);
            this.Close_F.Source = new BitmapImage(uriSource);
        }

        private void Close_F_MouseLeave(object sender, MouseEventArgs e)
        {
            var uriSource = new Uri(@"/Conecto®%20WorkSpace;component/Images/knop2_1.png", UriKind.Relative);
            this.Close_F.Source = new BitmapImage(uriSource);
        }

        private void Conector_MouseLeave(object sender, MouseEventArgs e)
        {
            var uriSource = new Uri(@"/Conecto®%20WorkSpace;component/Images/logo_shar1.png", UriKind.Relative);
            this.Conector.Source = new BitmapImage(uriSource);
        }

        private void Conector_MouseMove(object sender, MouseEventArgs e)
        {
            var uriSource = new Uri(@"/Conecto®%20WorkSpace;component/Images/logo_shar2.png", UriKind.Relative);
            this.Conector.Source = new BitmapImage(uriSource);
        }

        private void PrivteCabinet_MouseMove(object sender, MouseEventArgs e)
        {
            var uriSource = new Uri(@"/Conecto®%20WorkSpace;component/Images/kabinet_2.png", UriKind.Relative);
            this.PrivteCabinet.Source = new BitmapImage(uriSource);
        }

        private void PrivteCabinet_MouseLeave(object sender, MouseEventArgs e)
        {
            var uriSource = new Uri(@"/Conecto®%20WorkSpace;component/Images/kabinet_1.png", UriKind.Relative);
            this.PrivteCabinet.Source = new BitmapImage(uriSource);
        }

        private void Consalting_MouseMove(object sender, MouseEventArgs e)
        {
            var uriSource = new Uri(@"/Conecto®%20WorkSpace;component/Images/consalt_2.png", UriKind.Relative);
            this.Consalting.Source = new BitmapImage(uriSource);
        }

        private void Consalting_MouseLeave(object sender, MouseEventArgs e)
        {
            var uriSource = new Uri(@"/Conecto®%20WorkSpace;component/Images/consalt_1.png", UriKind.Relative);
            this.Consalting.Source = new BitmapImage(uriSource);
        }

        private void Market_MouseMove(object sender, MouseEventArgs e)
        {
            var uriSource = new Uri(@"/Conecto®%20WorkSpace;component/Images/market_2.png", UriKind.Relative);
            this.Market.Source = new BitmapImage(uriSource);
        }

        private void Market_MouseLeave(object sender, MouseEventArgs e)
        {
            var uriSource = new Uri(@"/Conecto®%20WorkSpace;component/Images/market_1.png", UriKind.Relative);
            this.Market.Source = new BitmapImage(uriSource);
        }
        private void Manual_MouseMove(object sender, MouseEventArgs e)
        {
            var uriSource = new Uri(@"/Conecto®%20WorkSpace;component/Images/manual_2.png", UriKind.Relative);
            this.Manual.Source = new BitmapImage(uriSource);
        }

        private void Manual_MouseLeave(object sender, MouseEventArgs e)
        {
            var uriSource = new Uri(@"/Conecto®%20WorkSpace;component/Images/manual_1.png", UriKind.Relative);
            this.Manual.Source = new BitmapImage(uriSource);
        }
        private void Videoinst_MouseLeave(object sender, MouseEventArgs e)
        {
            var uriSource = new Uri(@"/Conecto®%20WorkSpace;component/Images/video_12.png", UriKind.Relative);
            this.Videoinst.Source = new BitmapImage(uriSource);
        }

        private void Videoinst_MouseMove(object sender, MouseEventArgs e)
        {
            var uriSource = new Uri(@"/Conecto®%20WorkSpace;component/Images/video_22.png", UriKind.Relative);
            this.Videoinst.Source = new BitmapImage(uriSource);
        }


        private void Megaplan_MouseMove(object sender, MouseEventArgs e)
        {
            var uriSource = new Uri(@"/Conecto®%20WorkSpace;component/Images/ico_prog_megaplan2.png", UriKind.Relative);
            this.Megaplan.Source = new BitmapImage(uriSource);
        }

        private void Megaplan_MouseLeave(object sender, MouseEventArgs e)
        {
            var uriSource = new Uri(@"/Conecto®%20WorkSpace;component/Images/ico_prog_megaplan1.png", UriKind.Relative);
            this.Megaplan.Source = new BitmapImage(uriSource);
        }

      


        //-----------------

    

        #region Клавиша ОФис
        private void KeyPic2_MouseLeave(object sender, MouseEventArgs e)
        {
            Office_MouseLeave(sender, e);
        }
        private void KeyPic2_MouseMove(object sender, MouseEventArgs e)
        {
            Office_MouseMove(sender, e);
        }
        private void Office_MouseLeave(object sender, MouseEventArgs e)
        {
            var uriSource = new Uri(@"/Conecto®%20WorkSpace;component/Images/ico_prog_office1.png", UriKind.Relative);
            this.Office.Source = new BitmapImage(uriSource);
        }

        private void Office_MouseMove(object sender, MouseEventArgs e)
        {
            var uriSource = new Uri(@"/Conecto®%20WorkSpace;component/Images/ico_prog_office2.png", UriKind.Relative);
            this.Office.Source = new BitmapImage(uriSource);
        }
        #endregion

        #region клавиша фронт
        private void KeyPic1_MouseLeave(object sender, MouseEventArgs e)
        {
            TerminalFront_MouseLeave(sender, e);
        }

        private void KeyPic1_MouseMove(object sender, MouseEventArgs e)
        {
            TerminalFront_MouseMove(sender, e);
        }

        private void TerminalFront_MouseLeave(object sender, MouseEventArgs e)
        {
            var uriSource = new Uri(@"/Conecto®%20WorkSpace;component/Images/ico_prog_terminal1.png", UriKind.Relative);
            this.TerminalFront.Source = new BitmapImage(uriSource);
        }

        private void TerminalFront_MouseMove(object sender, MouseEventArgs e)
        {
            var uriSource = new Uri(@"/Conecto®%20WorkSpace;component/Images/ico_prog_terminal2.png", UriKind.Relative);
            this.TerminalFront.Source = new BitmapImage(uriSource);
        }
        #endregion

        #region Клавиша експорт
        private void Ecsport1C_MouseLeave(object sender, MouseEventArgs e)
        {
            var uriSource = new Uri(@"/Conecto®%20WorkSpace;component/Images/export1.png", UriKind.Relative);
            this.Ecsport1C.Source = new BitmapImage(uriSource);
        }
        private void Ecsport1C_MouseMove(object sender, MouseEventArgs e)
        {
            var uriSource = new Uri(@"/Conecto®%20WorkSpace;component/Images/export2.png", UriKind.Relative);
            this.Ecsport1C.Source = new BitmapImage(uriSource);
        }
        #endregion

        #endregion 
       

        #region Запуск окон

        #region Окна
        /// <summary>
        /// Окно идентификации пользователя<para></para>
        ///  NameWindow - Системное имя NameAutorize_,<para></para>
        ///  TypeAutoriz - Тип авторизации,<para></para> 
        ///  2-Ссылка на доп. Изображение справа,<para></para>
        ///  3-Ссылка на доп. Изображение сверху<para></para>
        ///  TypeServer - настройки сервера идентификации
        ///  0 - Сервер учетных данных (FB, MSSQL, LDAP, WEB server, MySql, UserConecto)<para></para>
        ///  1 - Сервер-IP<para></para>
        ///  2 - Cервер-Alias<para></para>
        ///  3 - Cервер-DopParam<para></para>
        ///  4 - Cервер-DopParam-Тип БД: B52-дополнительный индекс к логину (при смене пароля непонятная ситуация)<para></para>
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void key_aut_ButtonDown(string NameWindow, int TypeAutoriz = 0, string LinkPicRight = "", string LinkPicTop = "", string TextPic = "", string[] TypeServer = null)
        {
            TypeServer = TypeServer == null ? new string[6] { "", "", "", "", "", "3055" } : TypeServer;
            // Параметры
            SystemConecto.Autirize = new string[11] { NameWindow, TypeAutoriz.ToString(), LinkPicRight, LinkPicTop, TextPic,
                                TypeServer[0], TypeServer[1], TypeServer[2], TypeServer[3], TypeServer[4], TypeServer[5] };

            Window Window = SystemConecto.ListWindowMain("WaitFonW");

            if (Window != null)
            {
                // Не активировать окно - не передавать клавиатурный фокус
                Window.ShowActivated = false;
                Window.Show();
            }
            else
            {
                Window = new WaitFon();
                Window.Owner = this;
                // Не активировать окно - не передавать клавиатурный фокус
                Window.ShowActivated = false;
                Window.Show();
            }
            // Изменить прозрачность вызываемого окна
            var FonWindow = (Grid)LogicalTreeHelper.FindLogicalNode(Window, "WindGrid");
            FonWindow.Opacity = 0.80;

            Autiriz AutirizWindow = new Autiriz(NameWindow);
            AutirizWindow.Owner = this;  //AddOwnedForm(OblakoNizWindow);
            // размещаем на рабочем столе
            //AutirizWindow.Top = (this.Top + 7) + this.Close_F.Margin.Top + (this.Close_F.Height - 2) - 20;
            //AutirizWindow.Left = (this.Left + 7) + this.Close_F.Margin.Left - (AutirizWindow.Width - 22) + 20;
            // Отображаем
            AutirizWindow.ShowDialog();
        }
        #endregion

        #region Окно документации
        /// <summary>
        ///  Окно документации
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Manual_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

            var Window = SystemConecto.ListWindowMain("ManualW");
            if (Window != null)
            {
                Window.Show();
            }
            else
            {
                ManualWin WinManual = new ManualWin();
                WinManual.Owner = this;
                //363x646  // 73
                //MessageBox.Show(this.Conector.Margin.Top.ToString());
                //WinConector.Top = (this.Top) + this.Conector.Margin.Top + (this.Conector.Height) - 625;
                //WinConector.Left = (this.Left) + this.Conector.Margin.Left + (this.Conector.Width / 2) - (353) / 2;
                // MainWindow winMain = new Conector_();
                WinManual.Show();
                //WinConector.ShowDialog();
            }

        }




        #endregion



        #region Окно видео

        private void Videoinst_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            var Window = SystemConecto.ListWindowMain("VideoWinW");
            if (Window != null)
            {
                Window.Show();
            }
            else
            {
                VideoWin WinManual = new VideoWin();
                WinManual.Owner = this;
                //363x646  // 73
                //MessageBox.Show(this.Conector.Margin.Top.ToString());
                //WinConector.Top = (this.Top) + this.Conector.Margin.Top + (this.Conector.Height) - 625;
                //WinConector.Left = (this.Left) + this.Conector.Margin.Left + (this.Conector.Width / 2) - (353) / 2;
                // MainWindow winMain = new Conector_();
                WinManual.Show();
                //WinConector.ShowDialog();
            }
        }

        #endregion

        

        #endregion

        #region Изменение интерфейса при открытии окна Admin



        #region Клавиша Администрирование

        private void Admin_MouseLeave(object sender, MouseEventArgs e)
        {
            if (SystemConecto.AutorizUser.LoginUserAutoriz == "Autorize_pass-admin-IT" || SystemConecto.AutorizUser.LoginUserAutoriz == "Autorize_Admin-Conecto")
            {
                var uriSource = new Uri(@"/Conecto®%20WorkSpace;component/Images/admin_1.png", UriKind.Relative);
                AdminButIm_.Source = new BitmapImage(uriSource);
            }
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


        private void Admin_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var uriSource = new Uri(@"/Conecto®%20WorkSpace;component/Images/admin_2.png", UriKind.Relative);
            AdminButIm_.Source = new BitmapImage(uriSource);
        }

        /// <summary>
        /// Изменение интерфейса при открытии окна Admin
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AdminButIm__MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {

            var uriSource = new Uri(@"/Conecto®%20WorkSpace;component/Images/admin_1.png", UriKind.Relative);
            AdminButIm_.Source = new BitmapImage(uriSource);

            if (SystemConecto.AutorizUser.LoginUserAutoriz == "Autorize_pass-admin-IT" || SystemConecto.AutorizUser.LoginUserAutoriz == "Autorize_Admin-Conecto")
            {
                if (SystemConecto.WindowPanelSys_s == null)
                {
                    OpenAdministrator();
                }
                else
                {
                    OpenWindowsSysPnel("AdminW");
                }
            }
            else
            {
                // Пользователь пытается авторизироватся
                key_aut_ButtonDown("AdminOpcii", 1, @"/Conecto®%20WorkSpace;component/Images/admin_.png", "", "Администрирование"); //"Администрирование"

            }

        }

        #endregion


        #region Клавиша Сервер - станция

        /// <summary>
        /// Переход по закладке сервер - станция
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ServerButIm__MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            var uriSource = new Uri(@"/Conecto®%20WorkSpace;component/Images/server1.png", UriKind.Relative);
            ServerButIm_.Source = new BitmapImage(uriSource);

            OpenWindowsSysPnel("ServW");
        }

        private void ImServer_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

            var uriSource = new Uri(@"/Conecto®%20WorkSpace;component/Images/server2.png", UriKind.Relative);
            ServerButIm_.Source = new BitmapImage(uriSource);

        }

        private void ImServer_MouseLeave(object sender, MouseEventArgs e)
        {
            var uriSource = new Uri(@"/Conecto®%20WorkSpace;component/Images/server1.png", UriKind.Relative);
            ServerButIm_.Source = new BitmapImage(uriSource);

        }

        private void ImServer_MouseMove(object sender, MouseEventArgs e)
        {

        }

        #endregion


        #region Открытие системных окон с помощью системной панели слева
        /// <summary>
        /// Открытие системных окон с помощью системной панели (Admin,
        /// <param name="NameNewWindow"></param>
        /// </summary>
        private void OpenWindowsSysPnel(string NameNewWindow)
        {
            // Проверка окна на открытие
            //var WinActivePanelLeft = SystemConecto.ListWindowMain(NameNewWindow);
            
            //if (WinActivePanelLeft != null)
            //{
            //    WinActivePanelLeft.Close();
            //    SystemConecto.WindowPanelSys_s = null;

            //}
            //else
            //{

            // Номер закладки
            string ZacladID = "1";

            Window WinActivePanelLeft = null;

            // (Закрыть окно предыдущие)
            if (SystemConecto.WindowPanelSys_s != null && NameNewWindow != SystemConecto.WindowPanelSys_s)
            {
                WinActivePanelLeft = SystemConecto.ListWindowMain(SystemConecto.WindowPanelSys_s);
                if (WinActivePanelLeft != null)
                {
                    WinActivePanelLeft.Close();
                }
                switch (SystemConecto.WindowPanelSys_s)
                {
                        case "AdminW":
                        ZacladID = "1";
                        // курсор
                        AdminButIm_.Cursor = System.Windows.Input.Cursors.Hand;
                        Panel.SetZIndex(AdminBut_, 10-1); 
                             break;
                        case "ServW":
                             ZacladID = "2";
                             ServerButIm_.Cursor = System.Windows.Input.Cursors.Hand;
                             Panel.SetZIndex(ServerBut_, 10 - 2); 
                             break;
                        default:

                             break;
                }
                // Открыть предыдущию открытую закладку && OpenBack
                //NameNewWindow = SystemConecto.WindowPanelSys_s;
                // Активная вкладка
                Image Zaclad_ = (Image)LogicalTreeHelper.FindLogicalNode(this.AdminPanelLet, "FonButton" + ZacladID);
                if (Zaclad_ != null)
                {
                    Zaclad_.Margin = new Thickness(7, 0, 0, 0);
                    Zaclad_.Width = 164;
                    Uri uriSource = new Uri(@"/Conecto®%20WorkSpace;component/Images/knop_ne_activ.png", UriKind.Relative);
                    Zaclad_.Source = new BitmapImage(uriSource);
                    

                }



            }


            // Нажатие сам не себя исключено
            if (NameNewWindow != SystemConecto.WindowPanelSys_s)
            {
                switch (NameNewWindow)
                {
                        case "AdminW":
                            
                            
                            WinActivePanelLeft = new Admin(); // создаем
                            WinActivePanelLeft.Owner = this;
                            //MessageBox.Show(AdminWindow.Name);
                            WinActivePanelLeft.Show();

                            // Активная вкладка
                            ZacladID = "1";

                            // курсор
                            AdminButIm_.Cursor = System.Windows.Input.Cursors.Arrow;
                            // Вкладка отображается сверху
                            Panel.SetZIndex(AdminBut_, 11);

                            break;
                        case "ServW":
                             
                            WinActivePanelLeft = new ServerStation(); // создаем
                            WinActivePanelLeft.Owner = this;
                            //MessageBox.Show(AdminWindow.Name);
                            WinActivePanelLeft.Show();

                            // Активная вкладка
                            ZacladID = "2";
                            
                            // курсор
                            ServerButIm_.Cursor = System.Windows.Input.Cursors.Arrow;

                            Panel.SetZIndex(ServerBut_, 11); 

                            break;
                        default:

                            break;
               }
            }
            // Изменение вкладок на активные
            Image Zaclad = (Image)LogicalTreeHelper.FindLogicalNode(this.AdminPanelLet, "FonButton" + ZacladID);
            if (Zaclad != null)
            {
                Zaclad.Margin = new Thickness(0, 0, 0, 0);
                Zaclad.Width = 171;
                Uri uriSource = new Uri(@"/Conecto®%20WorkSpace;component/Images/knop_activ.png", UriKind.Relative);
                Zaclad.Source = new BitmapImage(uriSource);
                Zaclad.Visibility = Visibility.Visible;
            }

           SystemConecto.WindowPanelSys_s = NameNewWindow;

            //}
        }

        #endregion

        #region Отображение ярлыков, кнопок, панелей в зависимости от прав доступа

        public void OpenAdministrator()
        {
            if (SystemConecto.AutorizUser.LoginUserAutoriz == "Autorize_pass-admin-IT" || SystemConecto.AutorizUser.LoginUserAutoriz == "Autorize_Admin-Conecto")
            {
                // Окно по умолчанию (возможно запомнить последнее открытое)
                OpenWindowsSysPnel("AdminW");

                // === Открыть фон клавиш
                //this.PanelLeftFon.Margin = new Thickness(this.PanelLeftFon.Margin.Left, AdminButIm_.Margin.Top - 3, 0, 0);
                //// Активная вкладка
                //Image Zaclad = (Image)LogicalTreeHelper.FindLogicalNode(this.AdminPanelLet, "FonButton" + "1");
                //    if (Zaclad != null)
                //    {
                //        Zaclad.Margin = new Thickness(0, 0, 0, 0);
                //        Zaclad.Width = 171;
                //        Uri uriSource = new Uri(@"/Conecto®%20WorkSpace;component/Images/knop_activ.png", UriKind.Relative);
                //        Zaclad.Source = new BitmapImage(uriSource);
                //        Zaclad.Visibility = Visibility.Visible;
                //    }
                    // === Альтернативный код с прямым обращением
                        //FonButton1.Margin = new Thickness(0, 0, 0, 0);
                        //var uriSource = new Uri(@"/Conecto®%20WorkSpace;component/Images/knop_activ.png", UriKind.Relative);
                        //FonButton1.Source = new BitmapImage(uriSource);
                
                        //FonButton1.Visibility = Visibility.Visible; // SystemConecto.WindowPanelSys_s == "AdminW" ? Visibility.Hidden : Visibility.Visible;
                //AdminButIm_.Cursor = System.Windows.Input.Cursors.Arrow;
                
                 // SystemConecto.WindowPanelSys_s == "AdminW" ? Visibility.Hidden : Visibility.Visible;
                // === Открыть Закладки клавиши
                ServerButIm_.Visibility = Visibility.Visible;
                FonButton2.Visibility = Visibility.Visible;

                // === Изменения интерфейса рабочего стола
                SizeFindChildrenWorkSpace  TimeFon_Element = ReadElemetWorkSapce(this.TimeFon);
                if (TimeFon_Element != null)
                {
                // === Изменить базовые координаты елементов
                    SizeFindChildrenWorkSpace Line_Element = new SizeFindChildrenWorkSpace();
                    Line_Element.Name = Date.Name;
                    Line_Element.Margin_Left = TimeFon_Element.Margin_Left + 16; //this.TimeFon.Margin.Left + 11;
                    Line_Element.Margin_Top = TimeFon_Element.Margin_Top; // this.TimeFon.Margin.Top;
                    Line_Element.Width = Date.Width;
                    // Перетcкиваем дату в интерфейсе
                    //this.Date.Margin = SystemConecto.WindowPanelSys_s == "AdminW" ? new Thickness(this.TimeFon.Margin.Left + this.TimeFon.Width, this.TimeFon.Margin.Top + 3, 0, 0) : new Thickness(this.TimeFon.Margin.Left+11, this.TimeFon.Margin.Top - this.Date.Height , 0, 0) ;
                    //Date.Margin = new Thickness(this.TimeFon.Margin.Left + 11, this.TimeFon.Margin.Top, 0, 0); //SystemConecto.WindowPanelSys_s == "AdminW" ? new Thickness(this.TimeFon.Margin.Left + this.TimeFon.Width, this.TimeFon.Margin.Top + this.Date.Height + 6, 0, 0) : new Thickness(this.TimeFon.Margin.Left + 11, this.TimeFon.Margin.Top, 0, 0);
                    WriteElemetWorkSapce(Date, Line_Element);

                    // Перемещяем время
                    Line_Element = new SizeFindChildrenWorkSpace();
                    Line_Element.Name = TimeFon.Name;
                    Line_Element.Margin_Left = TimeFon_Element.Margin_Left;
                    Line_Element.Margin_Top = TimeFon_Element.Margin_Top - this.Date.Height - 3;
                    Line_Element.Width = TimeFon.Width;

                    //this.TimeFon.Margin = new Thickness(this.TimeFon.Margin.Left, this.TimeFon.Margin.Top - this.Date.Height - 3, 0, 0);//SystemConecto.WindowPanelSys_s == "AdminW" ? new Thickness(this.TimeFon.Margin.Left, this.TimeFon.Margin.Top + this.Date.Height + 3, 0, 0) : new Thickness(this.TimeFon.Margin.Left, this.TimeFon.Margin.Top - this.Date.Height - 3, 0, 0);
                    WriteElemetWorkSapce(TimeFon, Line_Element);

                }

            }

        }

        public void CloseAdministrator()
        {
            // Проверка выхода из учетной записи - исключил теперь это просто закрыть окно
            //if (SystemConecto.LoginUserAutoriz_Back == "Autorize_pass-admin-IT" || SystemConecto.LoginUserAutoriz_Back == "Autorize_Admin-Conecto")
            //{

                // === Открыть фон клавиш
                //this.PanelLeftFon.Margin = new Thickness(this.PanelLeftFon.Margin.Left, AdminButIm_.Margin.Top - 3, 0, 0);
                // Активная вкладка
                var Zaclad = (Image)LogicalTreeHelper.FindLogicalNode(this.AdminPanelLet, "FonButton" + "1");
                if (Zaclad != null)
                {
                    Zaclad.Margin = new Thickness(7, 0, 0, 0);
                    Zaclad.Width = 164;
                    var uriSource = new Uri(@"/Conecto®%20WorkSpace;component/Images/knop_ne_activ.png", UriKind.Relative);
                    Zaclad.Source = new BitmapImage(uriSource);
                    Zaclad.Visibility = Visibility.Hidden;
                }
                // === Альтернативный код с прямым обращением
                //FonButton1.Margin = new Thickness(0, 0, 0, 0);
                //var uriSource = new Uri(@"/Conecto®%20WorkSpace;component/Images/knop_activ.png", UriKind.Relative);
                //FonButton1.Source = new BitmapImage(uriSource);

                //FonButton1.Visibility = Visibility.Visible; // SystemConecto.WindowPanelSys_s == "AdminW" ? Visibility.Hidden : Visibility.Visible;


                // SystemConecto.WindowPanelSys_s == "AdminW" ? Visibility.Hidden : Visibility.Visible;
                // === Открыть Заклаки клавиши
                ServerButIm_.Visibility = Visibility.Hidden;
                FonButton2.Visibility = Visibility.Hidden;

                // === Изменения интерфейса рабочего стола
                SizeFindChildrenWorkSpace TimeFon_Element = ReadElemetWorkSapce(this.TimeFon);
                if (TimeFon_Element != null)
                {
                    // === Изменить базовые координаты елементов
                    SizeFindChildrenWorkSpace Line_Element = new SizeFindChildrenWorkSpace();
                    Line_Element.Name = Date.Name;
                    Line_Element.Margin_Left = TimeFon_Element.Margin_Left + this.TimeFon.Width; //this.TimeFon.Margin.Left + 11;
                    Line_Element.Margin_Top = TimeFon_Element.Margin_Top + this.Date.Height + 6; // this.TimeFon.Margin.Top;
                    Line_Element.Width = Date.Width;
                    // Перетcкиваем дату в интерфейсе
                    //this.Date.Margin = SystemConecto.WindowPanelSys_s == "AdminW" ? new Thickness(this.TimeFon.Margin.Left + this.TimeFon.Width, this.TimeFon.Margin.Top + 3, 0, 0) : new Thickness(this.TimeFon.Margin.Left+11, this.TimeFon.Margin.Top - this.Date.Height , 0, 0) ;
                   // this.Date.Margin = new Thickness(this.TimeFon.Margin.Left + this.TimeFon.Width, this.TimeFon.Margin.Top + this.Date.Height + 6, 0, 0);
                    WriteElemetWorkSapce(Date, Line_Element);

                    // Перемещяем время
                    Line_Element = new SizeFindChildrenWorkSpace();
                    Line_Element.Name = TimeFon.Name;
                    Line_Element.Margin_Left = TimeFon_Element.Margin_Left;
                    Line_Element.Margin_Top = TimeFon_Element.Margin_Top + this.Date.Height + 3;
                    Line_Element.Width = TimeFon.Width;

                   // this.TimeFon.Margin = new Thickness(this.TimeFon.Margin.Left, this.TimeFon.Margin.Top + this.Date.Height + 3, 0, 0);
                    WriteElemetWorkSapce(TimeFon, Line_Element);

                }

                // Окно по умолчанию закрыть (запомнили последнее открытое)
                //Window WinActivePanelLeft = SystemConecto.ListWindowMain(SystemConecto.WindowPanelSys_s);
                //if (WinActivePanelLeft != null)
                //{
                //    WinActivePanelLeft.Close();
                //}
                SystemConecto.WindowPanelSys_s = null;
            //}

        }

        #endregion

        #region Обработка событий любой клавиатуры
        public static string textKey = "";
        public static DateTime NullTime = new DateTime();
        public static DateTime StartIn = NullTime; //new DateTime() = 00:00:00
        public static TimeSpan EndIn;

        /// <summary>
        /// Слушаем что пользователь нажал или ввел (например код со считователя)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ConectoWorkSpace_KeyDown(object sender, KeyEventArgs e)
        {

            //===============
            // Ввод с считователя - отладка в памяти MessageBox.Show(textKey + "//" + EndIn.TotalMilliseconds.ToString());




            // Проверяем введенный символ реагируем на него для отслеживания начало ввода со считователя
            var KeycodeToChar = TextPasteWindow.KeycodeToChar(e.Key);
            // Отладка
            // SystemConecto.ErorDebag("Test - " + KeycodeToChar + "//" + textKey.Length.ToString(), 1);
            
            if (KeycodeToChar == ";" || EndIn.TotalMilliseconds > 0)
            {


                // Информация об устройствах ввода
                //  http://msdn.microsoft.com/ru-ru/library/system.consolekey.aspx

                // Информация о событиях с устройств ввода
                // http://metanit.com/sharp/wpf/6.php


                // Информация о времени
                // http://msdn.microsoft.com/ru-ru/library/system.timespan.aspx
                StartIn = StartIn == NullTime ? DateTime.Now : StartIn;
                // не более 1,5 сек
                var TimeBack = DateTime.Now - StartIn;

                // Убрать код начало ввода с устройства считования
                if (KeycodeToChar == ";")
                {
                    StartIn = DateTime.Now;
                    EndIn = new TimeSpan(0, 0, 0, 0, 1);
                    return;
                }


                // Отладка ввода - KeycodeToChar  TextPasteWindow.KeycodeToChar(e.Key)         
                // SystemConecto.ErorDebag("Символ : " + KeycodeToChar  + "// Win32 " + KeyInterop.VirtualKeyFromKey(e.Key).ToString() + "//" + DateTime.Now.ToString() + " / / " + StartIn.ToString() + " / / " + TimeBack.TotalMilliseconds, 1);

                // Защита от ввода с клавиатуры или другого устройства
                if (TimeBack.TotalMilliseconds < 380)
                {

                    // Ввод закончен, проверка конца введенного кода textKey
                    if (KeycodeToChar == "?")
                    {
                       // Вывести окно ожидания 
                       // ===! Выполнить передачу кода  имя программы Читать с настроек системы и нстроек пользователя

                        TextPasteWindow.pasteTextWin(";" + textKey + "?", "FrontOffice", "1", @"D:\Flagman\FrontOffice.exe"); //B52FrontOffice7   - D:\Flagman\FrontOffice.exe "B52BackOffice"B52FrontOffice7
                        //TextPasteWindow.pasteTextWin(";" + textKey + "?", "Notepad", "1", @"CMD");
                       // textKey = "";
                       StartIn = DateTime.Now;
                       EndIn = TimeSpan.Zero;
                       textKey = "";
                       return;
                    }
                    
                    // Оригинал
                    // textKey += e.Key.ToString();


                    textKey += KeycodeToChar;
                    EndIn = TimeBack;


                }
                else
                {

                    // Время в ноль начало нового ввода - сброс предыдущего ввода со знака ? - так как код вводится с клавиатуры
                    StartIn = DateTime.Now;
                    EndIn = TimeSpan.Zero;
                    textKey = "";
                    return;
                    // textKey = TextPasteWindow.KeycodeToChar(e.Key);

                }

            }
            else
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
                        // Ввод с клавиатуры
                        // var Size = ProcessConecto.GetWindowPr("Notepad");
                        // MessageBox.Show(Size[3].ToString());
                        //ServiceConectoPrgInterfice.ScreenShotWin();

                    }

                }


            }
        }
    
        #endregion


        #region Нажатия на ярлыки на рабочем столе системы



    

        #region Нажатие клавиши Терминал
        // Нажатие зоны ключа над клавишей Терминал
        private void KeyPic1_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            TerminalFront_MouseLeftButtonDown(sender, e);
        }


        private void TerminalFront_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            
 
            if (!AppStart.SetFocusWindow("B52FrontOffice7"))
            {

                // Пользователь пытается авторизироватся
                key_aut_ButtonDown("B52FrontOffice7", 1, @"/Conecto®%20WorkSpace;component/Images/ico_prog_terminal2.png", @"/Conecto®%20WorkSpace;component/Images/b52_logo.png", "Терминал");

            }

        }
        #endregion

        #region Нажатие клавиши Офиса
        // Нажатие зоны ключа над клавишей Офис
        private void KeyPic2_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Office_MouseLeftButtonDown(sender, e);
        }

            #region Окно Офиса
            /// <summary>
            /// Запуск офиса
            /// </summary>
            /// <param name="sender"></param>
            /// <param name="e"></param>
            private void Office_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
            {
                if (!AppStart.SetFocusWindow("B52BackOffice"))
                {

                    // Пользователь пытается авторизироватся
                    key_aut_ButtonDown("BackOfficeB52", 0, @"/Conecto®%20WorkSpace;component/Images/ico_prog_office2.png", @"/Conecto®%20WorkSpace;component/Images/b52_logo.png", "Офис");
                    
                    
                    // Процедура требующия окна wait
                    //SystemConecto.WinWaitStart();
                    this.Cursor = System.Windows.Input.Cursors.AppStarting;
                    Thread.Sleep(10000);
                    this.Cursor = System.Windows.Input.Cursors.Arrow;
                    //MessageBox.Show(this.Name.ToString());

                   
                    var pr = new Process();
                    pr.StartInfo.FileName = @"D:\B52\B52BackOffice.exe";
                    pr.Start();
                    pr.WaitForInputIdle(); // Ожидание ввода

                    // Закрыть окно ожидание
                    AppStart.WinWait_Close();
                    //SystemConecto.TickTask1(3, -1);

                   
                }
            }

            #endregion

      
        

        #endregion


        #region  Не используемые разработка
        // =====================  Не используемые 

        #region Для WPF изображения из ресурсов
        static internal ImageSource doGetImageSourceFromResource(string psAssemblyName, string psResourceName)
        {
            //"pack://application:,,,/Conecto®%20WorkSpace;component/_1366_x_768_, UriKind.RelativeOrAbsolute"
            Uri oUri = new Uri("pack://application:,,,/" + psAssemblyName + ";component/" + psResourceName, UriKind.RelativeOrAbsolute);
            return BitmapFrame.Create(oUri);
        }
        #endregion

        #region Преобразование изображений из ресурсов Properties.Resources. в ImageSource (не все преобразовует)
        /// <summary>
        /// Преобразование изображений из ресурсов Properties.Resources. в ImageSource
        /// Пример старых параметров
        /// (object value, Type targetType, object parameter, CultureInfo culture)
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public System.Windows.Media.ImageSource Convert_(object value)
        {
            // empty images are empty...
            if (value == null) { return null; }

            var image = (System.Drawing.Image)value;
            // Winforms Image we want to get the WPF Image from...
            var bitmap = new System.Windows.Media.Imaging.BitmapImage();
            bitmap.BeginInit();
            MemoryStream memoryStream = new MemoryStream();
            // Save to a memory stream...
            image.Save(memoryStream, ImageFormat.Bmp);
            // Rewind the stream...
            memoryStream.Seek(0, System.IO.SeekOrigin.Begin);
            bitmap.StreamSource = memoryStream;
            bitmap.EndInit();
            return bitmap;
        }
        #endregion

        private void ConectoWorkSpace_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            // Запустить поток Отслеживания изменения экрана

            //MessageBox.Show("Меняюсь");
            //var aSizeDWArea = View_DeviceSize(SizeDWArea_a);
            // Запись в систему
            // MessageBox.Show(aSizeDWArea[0].ToString() + " | " + aSizeDWArea[1].ToString(), "Отладка");


            // SystemConecto.WorkAreaDisplayDefault = aSizeDWArea;
            //SizeChan();
        }


        //========================= Разработка
        private void progressBar1_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (e.OldValue > 14.00)
            {
                // MessageBox.Show("Ура");

            }
            // MessageBox.Show(e.OldValue.ToString());
            if (e.OldValue == 0.00)
            {
                //MessageBox.Show("Старт");
                Duration duration = new Duration(TimeSpan.FromSeconds(85));
                DoubleAnimation doubleanimation = new DoubleAnimation(100.0, duration);
                this.progressBar1.BeginAnimation(ProgressBar.ValueProperty, doubleanimation);

            }
        }

        #endregion


        #region Нижняя панель быстрых клавиш дополнительных программ

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

        private void ButKeyboard_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (!AppStart.SetFocusWindow("osk"))
            {

                Process procCommand = Process.Start("osk");
            }
        }

        private void ButKeyboard_MouseLeave(object sender, MouseEventArgs e)
        {
            var uriSource = new Uri(@"/Conecto®%20WorkSpace;component/Images/keyboard1.png", UriKind.Relative);
            this.ButKeyboard.Source = new BitmapImage(uriSource);
        }


        private void ButKeyboard_MouseMove(object sender, MouseEventArgs e)
        {
            //***
        }


        private void ButCatalog_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var uriSource = new Uri(@"/Conecto®%20WorkSpace;component/Images/folder2.png", UriKind.Relative);
            this.ButCatalog.Source = new BitmapImage(uriSource);
        }


        private void ButCatalog_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            //Explorer /e,/select,c:\
            // команда открывает окно Explorer с двумя панелями, причем содержимое всех накопителей не выводится.
            ProcessStartInfo psiOpt = new ProcessStartInfo(@"Explorer.exe", @"/e,/select,c:\");
            psiOpt.CreateNoWindow = true;
            // psiOpt.WorkingDirectory = @"%windir%\system32\";  // Альтернатива System.Environment.SystemDirectory
            // запускаем процесс
            Process procCommand = Process.Start(psiOpt);
            // Доп функция определения появления окна в процессах Windows 
            // (некоторые зади после запуска через Start меняют свои свойства как hControl так и тип отображения)
            TextPasteWindow.WaitStartWindowApp("Explorer", "NL%", procCommand.StartTime.ToString());
            //SystemConecto.ErorDebag("Окно Explorer " + procCommand.Handle.ToString() +  " // " + procCommand.HandleCount.ToString() + " / " + procCommand.StartTime.ToString(), 1);
            // Устанавливаем нужные размеры 
            if (TextPasteWindow.SetFocusWindow("Explorer", procCommand.StartTime.ToString()))
            {
                if (!TextPasteWindow.SetWindowPos(TextPasteWindow.hControl, HWND.TOPMOST, 10, 10, Convert.ToInt32(SystemConecto.WorkAreaDisplayDefault[2] - 325), Convert.ToInt32(SystemConecto.WorkAreaDisplayDefault[3] - 60), SWP.SHOWWINDOW)) //
                {
                    SystemConecto.ErorDebag("Ошибка работы функции: SetWindowPos: окно" + "Explorer: Комьютер");
                }
            }
            else
            {
                SystemConecto.ErorDebag("Ошибка работы функции: SetWindowPos: окно Explorer: Комьютер, свойство StartTime");
            }
        }

        private void ButCatalog_MouseLeave(object sender, MouseEventArgs e)
        {
            var uriSource = new Uri(@"/Conecto®%20WorkSpace;component/Images/folder1.png", UriKind.Relative);
            this.ButCatalog.Source = new BitmapImage(uriSource);
        }


        // Перемещение мыши
        private void ButCatalog_MouseMove(object sender, MouseEventArgs e)
        {
           

        }


        private void TaskWindow_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            // Размещение нижней панели
            // Открыта - закрыть

            if (TaskWinView)
            {
                var uriSource = new Uri(@"/Conecto®%20WorkSpace;component/Images/jazichok_down2.png", UriKind.Relative);
                this.TaskWindow.Source = new BitmapImage(uriSource);

            }
            else{

                var uriSource = new Uri(@"/Conecto®%20WorkSpace;component/Images/jazichok_up2.png", UriKind.Relative);
                this.TaskWindow.Source = new BitmapImage(uriSource);

            }
        }


        private void TaskWindow_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {

            // Размещение нижней панели
            // Открыта - закрыть
            if (TaskWinView)
            {

                // развернуть все окна кроме основного
                foreach (System.Windows.Window openWindow in System.Windows.Application.Current.Windows)
                {
                    if (openWindow.Name == "ConectoWorkSpace" || openWindow.Name == "WaitFonW")
                    {

                    }
                    else
                    {
                        
                        openWindow.Visibility = Visibility.Visible;
                    }
                    //SystemConecto.ErorDebag(openWindow.Name, 2);
                    // openWindow.Visibility = Visibility.Collapsed;


                }

                // Развернуть окна процессов
                ProccesWindowMinimize(4);

                

                this.WindowTask.Height = 240;
                this.PanelRow.Height =  new GridLength(40);
                this.WindowTaskFon.Height = 0;

                this.WindowTask.Margin = this.WindowTaskFon.Margin = new Thickness(0, Convert.ToDouble(Convert.ToInt32(SystemConecto.WorkAreaDisplayDefault[3]) - 40), 0, 0);

              
                
                TaskWinView = false;

                var uriSource = new Uri(@"/Conecto®%20WorkSpace;component/Images/jazichok_up1.png", UriKind.Relative);
                this.TaskWindow.Source = new BitmapImage(uriSource);
            }
            else
            {
                // свернуть все окна кроме основного
                foreach (System.Windows.Window openWindow in System.Windows.Application.Current.Windows)
                {
                    if (openWindow.Name == "ConectoWorkSpace" || openWindow.Name == "WaitFonW")
                    {

                    }
                    else
                    {
                        openWindow.Visibility = Visibility.Collapsed;
                    }
                    //SystemConecto.ErorDebag(openWindow.Name, 2);
                    // openWindow.Visibility = Visibility.Collapsed;
                   

                }

                // Свернуть окна процессов
                ProccesWindowMinimize(7);

                

                this.WindowTask.Height = Convert.ToInt32(SystemConecto.WorkAreaDisplayDefault[3]);
                this.WindowTaskFon.Height = Convert.ToInt32(SystemConecto.WorkAreaDisplayDefault[3]);
                this.WindowTask.Margin = this.WindowTaskFon.Margin = new Thickness(0, 0, 0, 0);

                this.PanelRow.Height = new GridLength(Convert.ToInt32(SystemConecto.WorkAreaDisplayDefault[3]) - 200);
                
                

                //this.WindowTask.Margin = new Thickness(0, Convert.ToDouble(this.WindowTask.Margin.Top - 200), 0, 0);
                TaskWinView = true;

                var uriSource = new Uri(@"/Conecto®%20WorkSpace;component/Images/jazichok_down1.png", UriKind.Relative);
                this.TaskWindow.Source = new BitmapImage(uriSource);

            }

        }

        private void TaskWindow_MouseLeave(object sender, MouseEventArgs e)
        {

            if (TaskWinView)
            {
                var uriSource = new Uri(@"/Conecto®%20WorkSpace;component/Images/jazichok_down1.png", UriKind.Relative);
                this.TaskWindow.Source = new BitmapImage(uriSource);

            }
            else
            {

                var uriSource = new Uri(@"/Conecto®%20WorkSpace;component/Images/jazichok_up1.png", UriKind.Relative);
                this.TaskWindow.Source = new BitmapImage(uriSource);

            }

        }


        private void TaskWindow_MouseMove(object sender, MouseEventArgs e)
        {
            //***
        }

        #region Запуск калькулятора
        /// <summary>
        ///  Запуск калькулятора
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Calcul_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var uriSource = new Uri(@"/Conecto®%20WorkSpace;component/Images/calc2.png", UriKind.Relative);
            this.Calcul.Source = new BitmapImage(uriSource);
            
           
        }

        private void Calcul_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            // Калькулятор

            var uriSource = new Uri(@"/Conecto®%20WorkSpace;component/Images/calc1.png", UriKind.Relative);
            Calcul.Source = new BitmapImage(uriSource);

            if (!AppStart.SetFocusWindow("calc"))
            {
                System.Diagnostics.Process.Start("calc");
                // Доп функция определения появления окна в процессах Windows 
                // (некоторые задачи после запуска через Start меняют свои свойства как hControl так и тип отображения)
                TextPasteWindow.WaitStartWindowApp("calc");
            }
             // Устанавливаем нужные размеры 
            if (TextPasteWindow.SetFocusWindow("calc"))
            {
                if (!TextPasteWindow.SetWindowPos(TextPasteWindow.hControl, HWND.TOPMOST, 400, 400, 0, 0, SWP.SHOWWINDOW | SWP.NOSIZE)) //
                {
                    SystemConecto.ErorDebag("Ошибка работы функции: SetWindowPos: окно" + "calc");
                }
            }
        }

        private void Calcul_MouseLeave(object sender, MouseEventArgs e)
        {
            var uriSource = new Uri(@"/Conecto®%20WorkSpace;component/Images/calc1.png", UriKind.Relative);
            Calcul.Source = new BitmapImage(uriSource);
        }

     

        private void Calcul_MouseMove(object sender, MouseEventArgs e)
        {
            //***
            
        }
        #endregion

        #region Запуск Блокнота
        /// <summary>
        ///  Запуск Блокнота
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Note_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

            var uriSource = new Uri(@"/Conecto®%20WorkSpace;component/Images/note2.png", UriKind.Relative);
            this.Note.Source = new BitmapImage(uriSource);

        }

        private void Note_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            // Приложение, которое будем запускать
            if (!AppStart.SetFocusWindow("notepad"))
            {
                System.Diagnostics.Process.Start("notepad");
                 // Событие когда я закрыл блокнот
                
                //proc.EnableRaisingEvents = true;
                //proc.Exited += new EventHandler(proc_Exited);
                //proc.Start();

                TextPasteWindow.WaitStartWindowApp("notepad");
 
            }

            // Устанавливаем нужные размеры - не работает разработка
            if (TextPasteWindow.SetFocusWindow("notepad"))
            {
                if (!TextPasteWindow.SetWindowPos(TextPasteWindow.hControl, HWND.TOPMOST, 100, 100, 500, 150, SWP.SHOWWINDOW)) // | SWP.NOSIZE
                {
                    SystemConecto.ErorDebag("Ошибка работы функции: SetWindowPos: окно" + "notepad");
                }
            }



        }

        //// Будет вызываться при завершении работы Notepad.exe
        // private static void proc_Exited(object Sender, EventArgs e)
        // {
        //    Console.WriteLine("proc_Exited");
        // }

        private void Note_MouseMove(object sender, MouseEventArgs e)
        {
           //***
        }

        private void Note_MouseLeave(object sender, MouseEventArgs e)
        {
            var uriSource = new Uri(@"/Conecto®%20WorkSpace;component/Images/note1.png", UriKind.Relative);
            this.Note.Source = new BitmapImage(uriSource);
        }

        #endregion

        #region Запуск Блокнота

        private void AmmyAdmin_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {

 

            string PatchSR = SystemConectoServers.PutchConecto + @"Ammy\";
            SystemConecto.DIR_(PatchSR);
            if (!File.Exists(PatchSR + "AA_v3.exe"))
            {
                var TextWindows = "Выполняется установка программы" + Environment.NewLine + "Пожалуйста подождите. ";
                int AutoClose = 1;
                int MesaggeTop = -170;
                int MessageLeft = 400;
                MessageEnd(TextWindows, AutoClose, MesaggeTop, MessageLeft);
                Dictionary<string, string> fbembedList = new Dictionary<string, string>();
                fbembedList.Add(PatchSR + "AA_v3.exe", "Ammy/");
                if (SystemConecto.IsFilesPRG(fbembedList, -1, "- Проверка файлов во время установки AnyDesk " + PatchSR) != "True")
                {
                    MessageBox.Show("Отсутствует инсталяционный  файл установки AA_v3" + PatchSR + "  Установка прекращена. ");
                    return;
                }
            }

            string FileExe = SystemConectoServers.PutchConecto + @"Ammy\AA_v3.exe";
            // Приложение, которое будем запускать
            if (AppStart.SetFocusWindow(FileExe))
            {
                System.Diagnostics.Process.Start(FileExe);
                // Событие когда я закрыл блокнот

                //proc.EnableRaisingEvents = true;
                //proc.Exited += new EventHandler(proc_Exited);
                //proc.Start();

                TextPasteWindow.WaitStartWindowApp(FileExe);

            }

            // Устанавливаем нужные размеры - не работает разработка
            if (TextPasteWindow.SetFocusWindow(FileExe))
            {
                if (!TextPasteWindow.SetWindowPos(TextPasteWindow.hControl, HWND.TOPMOST, 100, 100, 500, 150, SWP.SHOWWINDOW)) // | SWP.NOSIZE
                {
                    SystemConecto.ErorDebag("Ошибка работы функции: SetWindowPos: окно" + "AA_v3.exe");
                }
            }



        }
        #endregion

        
        #region Запуск AnyDesk

        private void AnyDesk_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {

            var uriSource = new Uri(@"/Conecto®%20WorkSpace;component/Images/ticket-purchase.png", UriKind.Relative);
            AnyDesk.Source = new BitmapImage(uriSource);

            int NumberStart = 0;
            string FileExe = SystemConectoServers.PutchConecto + @"Ammy\AA_v3.exe";
            string PatchSR = SystemConectoServers.PutchConecto + @"Ammy\";
            SystemConecto.DIR_(PatchSR);
            if (File.Exists(FileExe))
            {
                if (!AppStart.SetFocusWindow("AA_v3"))
                {
                    
                    System.Diagnostics.Process.Start(FileExe);
                    
                    //TextPasteWindow.WaitStartWindowApp(FileExe);

                    //List<string> fileoutLines = new List<string>();
                    //Process[] processlist = Process.GetProcesses();
                    //foreach (Process theprocess in processlist)
                    //{
                    //    fileoutLines.Add(theprocess.ProcessName);
                    //    //Console.WriteLine("Process: {0} ID: {1}", theprocess.ProcessName, theprocess.Id);
                    //}
                    //System.IO.File.WriteAllLines(SystemConectoServers.PutchServer + "ListProcess.log", fileoutLines);

                    for (int i = 0; i <= 0; i++)
                    {
                        
                        if (NumberStart > 30) break;
                        if (TextPasteWindow.SetFocusWindow("AA_v3"))
                        {
                            if (!TextPasteWindow.SetWindowPos(TextPasteWindow.hControl, HWND.TOPMOST, 1000, 550, 510, 295, SWP.SHOWWINDOW)) // | SWP.NOSIZE
                            {
                                SystemConecto.ErorDebag("Ошибка работы функции: SetWindowPos: окно" + "AA_v3");
                            }
                        }
                        else
                        {
                            Thread.Sleep(50);
                            NumberStart++;
                            i = -1;

                        }
                    }
                    
                }
            }

            if (TextPasteWindow.SetFocusWindow("AA_v3")) return;
            Thread.Sleep(500);
            PatchSR = SystemConectoServers.PutchConecto + @"AnyDesk\";
            SystemConecto.DIR_(PatchSR);
            if (!File.Exists(PatchSR + "AnyDesk.exe"))
            {
                var TextWindows = "Выполняется установка программы" + Environment.NewLine + "Пожалуйста подождите. ";
                int AutoClose = 1;
                int MesaggeTop = -400;
                int MessageLeft = 100;
                MessageInstall(TextWindows, AutoClose, MesaggeTop, MessageLeft);
                Dictionary<string, string> fbembedList = new Dictionary<string, string>();
                fbembedList.Add(PatchSR + "AnyDesk.exe", "AnyDesk/");
                if (SystemConecto.IsFilesPRG(fbembedList, -1, "- Проверка файлов во время установки AnyDesk " + PatchSR) != "True")
                {
                    MessageBox.Show("Отсутствует инсталяционный  файл установки AnyDesk" + PatchSR + "  Установка прекращена. ");
                    return;
                }
            }

            FileExe = SystemConectoServers.PutchConecto + @"AnyDesk\AnyDesk.exe";
            // Приложение, которое будем запускать
            if (!AppStart.SetFocusWindow("AnyDesk"))
            {
                System.Diagnostics.Process.Start(FileExe);
                //TextPasteWindow.WaitStartWindowApp("AnyDesk");
            }

            // Устанавливаем нужные размеры - не работает разработка
            if (TextPasteWindow.SetFocusWindow("AnyDesk"))
            {

                for (int i = 0; i <= 0; i++)
                {
                    if (NumberStart > 30) break;
                    if (TextPasteWindow.SetFocusWindow("AA_v3"))
                    {
                        if (!TextPasteWindow.SetWindowPos(TextPasteWindow.hControl, HWND.TOPMOST, 100, 100, 1000, 700, SWP.SHOWWINDOW)) // | SWP.NOSIZE
                        {
                            SystemConecto.ErorDebag("Ошибка работы функции: SetWindowPos: окно" + "AnyDesk");
                        }
                    }
                    else
                    {
                        Thread.Sleep(50);
                        NumberStart++;
                        i = -1;

                    }
                }

  
            }
        }

        public static void MessageEnd(string TextWindows, int AutoClose, int MesaggeTop, int MessageLeft)
        {
            System.Windows.Application.Current.Dispatcher.Invoke(new Action(delegate ()
            {
                Window ConectoWorkSpace_InW = AppStart.LinkMainWindow("ConectoWorkSpace");
                //MainWindow ConectoWorkSpace_InW = AppStart.LinkMainWindow();
                var WinOblakoVerh_ = AppStart.WindowActive("WinOblakoLeft", TextWindows, AutoClose);
                WinOblakoVerh_.Owner = ConectoWorkSpace_InW;
                // размещаем на рабочем столе
                WinOblakoVerh_.Top = ConectoWorkSpace_InW.Top - MesaggeTop;

                WinOblakoVerh_.Left = ConectoWorkSpace_InW.Left + MessageLeft;  //ConectoWorkSpace_InW.Left + MessageLeft;
                WinOblakoVerh_.ShowActivated = false;
                WinOblakoVerh_.Show();

            }));
        }

        public static void MessageInstall(string TextWindows, int AutoClose = 1, int MesaggeTop = -400, int MessageLeft = 300)
        {
            System.Windows.Application.Current.Dispatcher.Invoke(new Action(delegate ()
            {
                Window ConectoWorkSpace_InW = AppStart.LinkMainWindow("ConectoWorkSpace");
                //MainWindow ConectoWorkSpace_InW = AppStart.LinkMainWindow();
                var WinOblakoVerh_ = AppStart.WindowActive("WinOblakoVerh", TextWindows, AutoClose);
                WinOblakoVerh_.Owner = ConectoWorkSpace_InW;
                // размещаем на рабочем столе
                WinOblakoVerh_.Top = SystemConecto.WorkAreaDisplayDefault[0] - MesaggeTop;

                WinOblakoVerh_.Left = SystemConecto.WorkAreaDisplayDefault[1] + MessageLeft;  //ConectoWorkSpace_InW.Left + MessageLeft;
                WinOblakoVerh_.ShowActivated = false;
                WinOblakoVerh_.Show();

            }));
        }

        #endregion

        #region Запуск TeamViewer

        private void Restart_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {

            var uriSource = new Uri(@"/Conecto®%20WorkSpace;component/Images/teamviewer_22483.png", UriKind.Relative);
            TeamViewer.Source = new BitmapImage(uriSource);

            int NumberStart = 0;
            string FileExe = @"c:\TeamViewer\TeamViewerPortable.exe";
            string PatchSR = @"c:\TeamViewer\";
            if (!File.Exists(FileExe))
            {
                var TextWindows = "Выполняется установка программы" + Environment.NewLine + "Пожалуйста подождите. ";
                int AutoClose = 1;
                int MesaggeTop = -400;
                int MessageLeft = 100;
                MessageInstall(TextWindows, AutoClose, MesaggeTop, MessageLeft);
                Dictionary<string, string> fbembedList = new Dictionary<string, string>();
                fbembedList.Add(PatchSR + "TeamViewer.zip", "TeamViewerPortable/");
                if (SystemConecto.IsFilesPRG(fbembedList, -1, "- Проверка файлов во время установки TeamViewer " + PatchSR) != "True")
                {
                    MessageBox.Show("Отсутствует инсталяционный  файл установки TeamViewer" + PatchSR + "  Установка прекращена. ");
                    return;
                }
            }

            if (File.Exists(FileExe))
            {
                if (!AppStart.SetFocusWindow("TeamViewerPortable"))
                {

                    System.Diagnostics.Process.Start(FileExe);
                    for (int i = 0; i <= 0; i++)
                    {

                        if (NumberStart > 30) break;
                        if (TextPasteWindow.SetFocusWindow("TeamViewerPortable"))
                        {
                            if (!TextPasteWindow.SetWindowPos(TextPasteWindow.hControl, HWND.TOPMOST, 300, 300, 500, 300, SWP.SHOWWINDOW)) // | SWP.NOSIZE
                            {
                                SystemConecto.ErorDebag("Ошибка работы функции: SetWindowPos: окно" + "TeamViewerPortable");
                            }
                        }
                        else
                        {
                            Thread.Sleep(50);
                            NumberStart++;
                            i = -1;

                        }
                    }

                }
            }

        }
        #endregion

        #endregion
        private void Ecsport1C_ImageFailed(object sender, ExceptionRoutedEventArgs e)
        {

        }

       
       

      


        #endregion


        #region Свернуть все процессы с графичеким интерфейсом

        private void ProccesWindowMinimize(int Invertyng = 0)
        {
            var MainProccer = Process.GetCurrentProcess().MainWindowHandle;
            // встречается нулевой Хендел (даже после обновления кеша p.refrech) для процессов без интерфейса Окна ОС их мы исключаем из выбора
            Process[] allProc_ = Process.GetProcesses().Where(process => process.MainWindowHandle != IntPtr.Zero).ToArray();
            foreach (Process p in allProc_)
            {
                //SystemConecto.ErorDebag("Ошибка закрытия окна: SetWindowPos: окно - " + p.ProcessName.ToString() +" title: "+p.MainWindowTitle.ToString());
                // 
                if ((p.ProcessName == "explorer" && p.MainWindowTitle.Length == 0) || p.ProcessName == "ConectoWorkSpace" || p.ProcessName == "Conecto® WorkSpace.vshost" || p.ProcessName == "Conecto® WorkSpace")
                {
                    if (p.ProcessName == "ConectoWorkSpace" || p.ProcessName == "Conecto® WorkSpace.vshost" || p.ProcessName == "Conecto® WorkSpace")
                    {
                        MainProccer = p.MainWindowHandle; 
                    }
                    //if (p.ProcessName == "explorer")
                    //{
                    //    SystemConecto.ErorDebag("Поток: explorer с заголовком " + p.MainWindowTitle.ToString() + " / " + p.Handle.ToString() + " / " + " / " + p.StartTime.ToString());
                    //}
                }
                else
                {
                    if (p.ProcessName == "notepad" || p.ProcessName == "calc" || p.ProcessName == "osk" || p.ProcessName == "explorer")
                    {
                        if (!TextPasteWindow.ShowWindow(p.MainWindowHandle, Invertyng))
                        {
                            SystemConecto.ErorDebag("Ошибка закрытия/открытия окна: SetWindowPos: окно - " + p.ProcessName.ToString());
                        }
                    }
                    else
                    {
                        // SystemConecto.ErorDebag("Не зарегестрированный поток: " + p.ProcessName.ToString());

                    }
                }
                //if (!TextPasteWindow.SetWindowPos(p.MainWindowHandle, HWND.BOTTOM, 400, 400, 0, 0, SWP.SHOWWINDOW | SWP.NOSIZE)) //
                //{
                //    SystemConecto.ErorDebag("Ошибка работы функции: SetWindowPos: окно" + "calc");
                //}
            }
            if (!TextPasteWindow.ShowWindow(MainProccer, 4))
            {
                SystemConecto.ErorDebag("Ошибка закрытия окна:/открытия SetWindowPos: окно - " + Process.GetCurrentProcess().ProcessName.ToString());
            }

        }



      

        #endregion


        #region Выбрать свою программу в ОС - пока не спасает Windows переходит на другую программу...
        /// <summary>
        /// Выбрать и активровать свою программу в ОС
        /// </summary>
        /// <param name="Invertyng"></param>
        public bool ShowActiveMainAppinOS(int Invertyng = 0)
        {
            // Выбрать основное окно в конце
            if (!TextPasteWindow.ShowWindow(Process.GetCurrentProcess().MainWindowHandle, 4))
            {
                SystemConecto.ErorDebag("Ошибка закрытия окна:/открытия SetWindowPos: окно - " + Process.GetCurrentProcess().ProcessName.ToString());
                return false;
            }
            return true;
        }
        #endregion

        #region Окно Личный кабинет
        /// <summary>
        ///  Окно документации
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PrivteCabinet__MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {

            // Не Модальное окно 
            var Window = SystemConecto.ListWindowMain("PalyStory_");
            if (Window != null)
            {
                Window.Show();
            }
            else
            {
                // Не Модальное окно
                PalyStory WinSpecPred = new PalyStory();
                WinSpecPred.Owner = this;
                WinSpecPred.Show();

            }

        }

        #endregion


        #region Окно Спецпредложений Маркета
        /// <summary>
        ///  Окно документации
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Market_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {

            // Не Модальное окно с фоном
            var Window = SystemConecto.ListWindowMain("SpecPred_");
            if (Window != null)
            {
                Window.Show();
            }
            else
            {
                //WaitFon FonWindow = new WaitFon("Black");
                //FonWindow.Owner = this;
                //FonWindow.Show();

                // Не Модальное окно
                SpecPred WinSpecPred = new SpecPred();
                WinSpecPred.Owner = this;
                //363x646  // 73
                //MessageBox.Show(this.Conector.Margin.Top.ToString());
                //WinConector.Top = (this.Top) + this.Conector.Margin.Top + (this.Conector.Height) - 625;
                //WinConector.Left = (this.Left) + this.Conector.Margin.Left + (this.Conector.Width / 2) - (353) / 2;
                // MainWindow winMain = new Conector_();
                WinSpecPred.Show();
                //WinConector.ShowDialog();
            }

        }




        #endregion

        // Обновление окна если виндовс сам испортил картинку )
        private void RefrechWindow(object sender, MouseButtonEventArgs e)
        {

            // 2. Дополнительно проверить запуск explorer для режима Терминал
            var getAllExploreProcess = Process.GetProcesses().Where(r => r.ProcessName.Contains("explorer"));
            WindowState BackValue_WS = this.WindowState;
            if (SystemConecto.TerminalStatus || getAllExploreProcess.Count() == 0)
            {
                BackValue_WS = WindowState.Maximized;
            }

            // 3. Динамическая прорисовка интерфейса и Динамические елементы
            numberElementsInfoPa(1);
            ResizeConectoWorkSpace(BackValue_WS);
            

        }


        /// <summary>
        ///  Окно решений и администрирования
            /// </summary>
            /// <param name="sender"></param>
            /// <param name="e"></param>

        private void AdminButImPanels__MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            var uriSource = new Uri(@"/Conecto®%20WorkSpace;component/Images/admin_1.png", UriKind.Relative);
            AdminButIm_.Source = new BitmapImage(uriSource);
            NumberInput = 0;
            if (SystemConecto.AutorizUser.LoginUserAutoriz == "Autorize_pass-admin-IT" || SystemConecto.AutorizUser.LoginUserAutoriz == "Autorize_Admin-Conecto")
            {
                if (SystemConecto.WindowPanelSys_s == null)
                {
                    if (SystemConecto.AutorizUser.LoginUserAutoriz == "Autorize_pass-admin-IT" || SystemConecto.AutorizUser.LoginUserAutoriz == "Autorize_Admin-Conecto")
                    {
                        OpenWindowAdminPanels();
                    }
                       
                }
                else
                {
                    OpenWindowAdminPanels();
                }
            }
            else
            {
                if (MainWindow.NumberInput > MainWindow.QuantityInput)
                {
                    var TextWindows = "Вход в Администрирование заблокирован." + Environment.NewLine + "Пожалуйста ожидайте. ";
                    int AutoClose = 1;
                    int MesaggeTop = -130;
                    int MessageLeft = 50;
                    MessageEnd(TextWindows, AutoClose, MesaggeTop, MessageLeft);
                    return;
                }
                // Пользователь пытается авторизироватся
                key_aut_ButtonDown("AdminPanels", 1, @"/Conecto®%20WorkSpace;component/Images/admin_.png", "", "Администрирование"); //"Администрирование"

            }
        }

        #region Открытие и закрытие админисративного окна
        // Не использую - удалить
        public void OpenAdministratorPanels()
        {
            if (SystemConecto.AutorizUser.LoginUserAutoriz == "Autorize_pass-admin-IT" || SystemConecto.AutorizUser.LoginUserAutoriz == "Autorize_Admin-Conecto")
            {
                // Окно по умолчанию (возможно запомнить последнее открытое)
                OpenWindowAdminPanels();

                // === Открыть фон клавиш
                //this.PanelLeftFon.Margin = new Thickness(this.PanelLeftFon.Margin.Left, AdminButIm_.Margin.Top - 3, 0, 0);
                //// Активная вкладка
                //Image Zaclad = (Image)LogicalTreeHelper.FindLogicalNode(this.AdminPanelLet, "FonButton" + "1");
                //    if (Zaclad != null)
                //    {
                //        Zaclad.Margin = new Thickness(0, 0, 0, 0);
                //        Zaclad.Width = 171;
                //        Uri uriSource = new Uri(@"/Conecto®%20WorkSpace;component/Images/knop_activ.png", UriKind.Relative);
                //        Zaclad.Source = new BitmapImage(uriSource);
                //        Zaclad.Visibility = Visibility.Visible;
                //    }
                // === Альтернативный код с прямым обращением
                //FonButton1.Margin = new Thickness(0, 0, 0, 0);
                //var uriSource = new Uri(@"/Conecto®%20WorkSpace;component/Images/knop_activ.png", UriKind.Relative);
                //FonButton1.Source = new BitmapImage(uriSource);

                //FonButton1.Visibility = Visibility.Visible; // SystemConecto.WindowPanelSys_s == "AdminW" ? Visibility.Hidden : Visibility.Visible;
                //AdminButIm_.Cursor = System.Windows.Input.Cursors.Arrow;

                // SystemConecto.WindowPanelSys_s == "AdminW" ? Visibility.Hidden : Visibility.Visible;
                // === Открыть Закладки клавиши
                ServerButIm_.Visibility = Visibility.Visible;
                FonButton2.Visibility = Visibility.Visible;
                

                // === Изменения интерфейса рабочего стола
                SizeFindChildrenWorkSpace TimeFon_Element = ReadElemetWorkSapce(this.TimeFon);
                if (TimeFon_Element != null)
                {
                    // === Изменить базовые координаты елементов
                    SizeFindChildrenWorkSpace Line_Element = new SizeFindChildrenWorkSpace();
                    Line_Element.Name = Date.Name;
                    Line_Element.Margin_Left = TimeFon_Element.Margin_Left + 16; //this.TimeFon.Margin.Left + 11;
                    Line_Element.Margin_Top = TimeFon_Element.Margin_Top; // this.TimeFon.Margin.Top;
                    Line_Element.Width = Date.Width;
                    // Перетcкиваем дату в интерфейсе
                    //this.Date.Margin = SystemConecto.WindowPanelSys_s == "AdminW" ? new Thickness(this.TimeFon.Margin.Left + this.TimeFon.Width, this.TimeFon.Margin.Top + 3, 0, 0) : new Thickness(this.TimeFon.Margin.Left+11, this.TimeFon.Margin.Top - this.Date.Height , 0, 0) ;
                    Date.Margin = new Thickness(this.TimeFon.Margin.Left + 11, this.TimeFon.Margin.Top, 0, 0); //SystemConecto.WindowPanelSys_s == "AdminW" ? new Thickness(this.TimeFon.Margin.Left + this.TimeFon.Width, this.TimeFon.Margin.Top + this.Date.Height + 6, 0, 0) : new Thickness(this.TimeFon.Margin.Left + 11, this.TimeFon.Margin.Top, 0, 0);
                    WriteElemetWorkSapce(Date, Line_Element);

                    // Перемещяем время
                    Line_Element = new SizeFindChildrenWorkSpace();
                    Line_Element.Name = TimeFon.Name;
                    Line_Element.Margin_Left = TimeFon_Element.Margin_Left;
                    Line_Element.Margin_Top = TimeFon_Element.Margin_Top - this.Date.Height - 3;
                    Line_Element.Width = TimeFon.Width;

                    this.TimeFon.Margin = new Thickness(this.TimeFon.Margin.Left, this.TimeFon.Margin.Top - this.Date.Height - 3, 0, 0);//SystemConecto.WindowPanelSys_s == "AdminW" ? new Thickness(this.TimeFon.Margin.Left, this.TimeFon.Margin.Top + this.Date.Height + 3, 0, 0) : new Thickness(this.TimeFon.Margin.Left, this.TimeFon.Margin.Top - this.Date.Height - 3, 0, 0);
                    WriteElemetWorkSapce(TimeFon, Line_Element);

                }

            }

        }

        public void CloseAdministratorPanels()
        {
            // Проверка выхода из учетной записи - исключил теперь это просто закрыть окно
            //if (SystemConecto.LoginUserAutoriz_Back == "Autorize_pass-admin-IT" || SystemConecto.LoginUserAutoriz_Back == "Autorize_Admin-Conecto")
            //{

            // === Открыть фон клавиш
            //this.PanelLeftFon.Margin = new Thickness(this.PanelLeftFon.Margin.Left, AdminButIm_.Margin.Top - 3, 0, 0);
            // Активная вкладка
            var Zaclad = (Image)LogicalTreeHelper.FindLogicalNode(this.AdminPanelLet, "FonButton" + "1");
            if (Zaclad != null)
            {
                Zaclad.Margin = new Thickness(7, 0, 0, 0);
                Zaclad.Width = 164;
                var uriSource = new Uri(@"/Conecto®%20WorkSpace;component/Images/knop_ne_activ.png", UriKind.Relative);
                Zaclad.Source = new BitmapImage(uriSource);
                Zaclad.Visibility = Visibility.Hidden;
            }

            // SystemConecto.WindowPanelSys_s == "AdminW" ? Visibility.Hidden : Visibility.Visible;
            // === Открыть Заклаки клавиши
            ServerButIm_.Visibility = Visibility.Hidden;
            FonButton2.Visibility = Visibility.Hidden;

            // === Изменения интерфейса рабочего стола
            SizeFindChildrenWorkSpace TimeFon_Element = ReadElemetWorkSapce(this.TimeFon);
            if (TimeFon_Element != null)
            {
                // === Изменить базовые координаты елементов
                SizeFindChildrenWorkSpace Line_Element = new SizeFindChildrenWorkSpace();
                Line_Element.Name = Date.Name;
                Line_Element.Margin_Left = TimeFon_Element.Margin_Left + this.TimeFon.Width; //this.TimeFon.Margin.Left + 11;
                Line_Element.Margin_Top = TimeFon_Element.Margin_Top + this.Date.Height + 6; // this.TimeFon.Margin.Top;
                Line_Element.Width = Date.Width;
                // Перетcкиваем дату в интерфейсе
                //this.Date.Margin = SystemConecto.WindowPanelSys_s == "AdminW" ? new Thickness(this.TimeFon.Margin.Left + this.TimeFon.Width, this.TimeFon.Margin.Top + 3, 0, 0) : new Thickness(this.TimeFon.Margin.Left+11, this.TimeFon.Margin.Top - this.Date.Height , 0, 0) ;
                this.Date.Margin = new Thickness(this.TimeFon.Margin.Left + this.TimeFon.Width, this.TimeFon.Margin.Top + this.Date.Height + 6, 0, 0);
                WriteElemetWorkSapce(Date, Line_Element);

                // Перемещяем время
                Line_Element = new SizeFindChildrenWorkSpace();
                Line_Element.Name = TimeFon.Name;
                Line_Element.Margin_Left = TimeFon_Element.Margin_Left;
                Line_Element.Margin_Top = TimeFon_Element.Margin_Top + this.Date.Height + 3;
                Line_Element.Width = TimeFon.Width;

                this.TimeFon.Margin = new Thickness(this.TimeFon.Margin.Left, this.TimeFon.Margin.Top + this.Date.Height + 3, 0, 0);
                WriteElemetWorkSapce(TimeFon, Line_Element);

            }



        }

        #endregion



        #region Открытие административной панели 
        /// <summary>
        /// Открытие системных окон с помощью системной панели (Admin,
        /// <param name="NameNewWindow"></param>
        /// </summary>
        public void OpenWindowAdminPanels()
        {


            //AdminButIm_.Visibility = Visibility.Collapsed;
            //ConectInternet.Visibility = Visibility.Collapsed;
            //Time_L_hh.Visibility = Visibility.Collapsed;
            //Time_L_mm.Visibility = Visibility.Collapsed;
            //AdminButIm_Old.Visibility = Visibility.Collapsed;
            AdminButIm_.Visibility = Visibility.Collapsed;
            Window WinActivePanelLeft = new AdminPanels(); // создаем
            WinActivePanelLeft.Owner = this;
            //MessageBox.Show(AdminWindow.Name);
            WinActivePanelLeft.Show();



            // курсор
            AdminButIm_.Cursor = System.Windows.Input.Cursors.Arrow;
            // Вкладка отображается сверху
            //Panel.SetZIndex(AdminBut_, 11);
        }

        #endregion


    }


    #region TreeHelper Вспомогательные методы для пользовательского интерфейса задачи, связанные с элементами окон.
    /// <summary>
    /// Вспомогательные методы для пользовательского интерфейса задачи, связанные с элементами окон.
    /// </summary>
    public static class TreeHelper
    {
        #region Поиск родителя данного пункта в визуальном дереве find parent
        /// <summary>
        /// Поиск родителя данного пункта в визуальном дереве.
        /// </summary>
        /// <typeparam name="T">Типа запроса пункт.</typeparam>
        /// <param name="child">A direct or indirect child of the
        /// queried item.</param>
        /// <returns>The first parent item that matches the submitted
        /// type parameter. If not matching item can be found, a null
        /// reference is being returned.</returns>
        public static T TryFindParent<T>(this DependencyObject child)
        where T : DependencyObject
        {
            //get parent item
            DependencyObject parentObject = GetParentObject(child);

            //we've reached the end of the tree
            if (parentObject == null) return null;

            //check if the parent matches the type we're looking for
            T parent = parentObject as T;
            if (parent != null)
            {
                return parent;
            }
            else
            {
                //use recursion to proceed with next level
                return TryFindParent<T>(parentObject);
            }
        }

        /// <summary>
        /// Этот метод является альтернативой в WPF
        /// <see cref="VisualTreeHelper.GetParent"/> Метод, который также поддерживает элементы контента.
        /// Keep in mind that for content element,
        /// this method falls back to the logical tree of the element!
        /// </summary>
        /// <param name="child">The item to be processed.</param>
        /// <returns>The submitted item's parent, if available. Otherwise
        /// null.</returns>
        public static DependencyObject GetParentObject(this DependencyObject child)
        {
            if (child == null) return null;

            //handle content elements separately
            ContentElement contentElement = child as ContentElement;
            if (contentElement != null)
            {
                DependencyObject parent = ContentOperations.GetParent(contentElement);
                if (parent != null) return parent;

                FrameworkContentElement fce = contentElement as FrameworkContentElement;
                return fce != null ? fce.Parent : null;
            }

            //also try searching for parent in framework elements (such as DockPanel, etc)
            FrameworkElement frameworkElement = child as FrameworkElement;
            if (frameworkElement != null)
            {
                DependencyObject parent = frameworkElement.Parent;
                if (parent != null) return parent;
            }

            //if it's not a ContentElement/FrameworkElement, rely on VisualTreeHelper
            return VisualTreeHelper.GetParent(child);
        }

        #endregion

        #region  Найти все элементы данного типа find children
        // Найти все Изображения
        // Window window = this;
        // foreach (Image img in window.FindChildren<Image>())
        // {
        //   Console.WriteLine("Image source: " + img.Source);
        // }

        /// <summary>
        /// Анализирует как визуальное и логическое дерево, чтобы найти все элементы
        ///данного типа, которые являются потомками элемента <paramref name="source"/>
        /// 
        /// </summary>
        /// <typeparam name="T">The type of the queried items.</typeparam>
        /// <param name="source">The root element that marks the source of the search. If the
        /// source is already of the requested type, it will not be included in the result.</param>
        /// <returns>All descendants of <paramref name="source"/> that match the requested type.</returns>
        public static IEnumerable<T> FindChildren<T>(this DependencyObject source) where T : DependencyObject
        {
            if (source != null)
            {
                var childs = GetChildObjects(source);
                foreach (DependencyObject child in childs)
                {
                    //analyze if children match the requested type
                    if (child != null && child is T)
                    {
                        yield return (T)child;
                    }

                    //recurse tree
                    foreach (T descendant in FindChildren<T>(child))
                    {
                        yield return descendant;
                    }
                }
            }
        }


        /// <summary>
        /// Этот метод является альтернативой в WPF
        /// <see cref="VisualTreeHelper.GetChild"/> method, which also
        /// supports content elements. Keep in mind that for content elements,
        /// this method falls back to the logical tree of the element.
        /// </summary>
        /// <param name="parent">The item to be processed.</param>
        /// <returns>The submitted item's child elements, if available.</returns>
        public static IEnumerable<DependencyObject> GetChildObjects(this DependencyObject parent)
        {
            if (parent == null) yield break;

            if (parent is ContentElement || parent is FrameworkElement)
            {
                //use the logical tree for content / framework elements
                foreach (object obj in LogicalTreeHelper.GetChildren(parent))
                {
                    var depObj = obj as DependencyObject;
                    if (depObj != null) yield return (DependencyObject)obj;
                }
            }
            else
            {
                //use the visual tree per default
                int count = VisualTreeHelper.GetChildrenCount(parent);
                for (int i = 0; i < count; i++)
                {
                    yield return VisualTreeHelper.GetChild(parent, i);
                }
            }
        }

        #endregion

        #region find from point

        /// <summary>
        /// Пытается найти данный элемент в визуальном дереве,
        /// начиная с зависимого объекта в заданной позиции. 
        /// </summary>
        /// <typeparam name="T">The type of the element to be found
        /// on the visual tree of the element at the given location.</typeparam>
        /// <param name="reference">The main element which is used to perform
        /// hit testing.</param>
        /// <param name="point">The position to be evaluated on the origin.</param>
        public static T TryFindFromPoint<T>(UIElement reference, Point point)
            where T : DependencyObject
        {
            DependencyObject element = reference.InputHitTest(point) as DependencyObject;

            if (element == null) return null;
            else if (element is T) return (T)element;
            else return TryFindParent<T>(element);
        }
        #endregion

        #region еще один пример
        /// <summary>
        /// Наход элемент в визуальном дереве.
        /// </summary>
        /// <typeparam name="T">Тип искомого элемента.</typeparam>
        /// <param name="obj">Корневой элемент с которого начинается поиск.</param>
        /// <returns>Искомый элемент.</returns>
        private static T FindVisualChild<T>(DependencyObject obj) where T : DependencyObject
        {
            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(obj); i++)
            {
                DependencyObject child = VisualTreeHelper.GetChild(obj, i);

                var typedChild = child as T;

                if (typedChild != null)
                {
                    return typedChild;
                }
                else
                {
                    var childOfChild = FindVisualChild<T>(child);
                    if (childOfChild != null)
                    {
                        return childOfChild;
                    }
                }
            }

            return null;
        }
        #endregion

        
    }
    #endregion
}
