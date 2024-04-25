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

namespace ConectoWorkSpace
{
    /// <summary>
    /// Логика взаимодействия для PalyStory.xaml
    /// </summary>
    public partial class PalyStory : Window
    {
        public PalyStory()
        {
            InitializeComponent();
            ResolutionDisplay();
            // пример рисования програмнно. В окне закладки описаны стилем.
            //this.WindGrid.Children.Add(CreateRectangleRadius(Color.FromRgb(248, 207, 1)));

            #region Конфигурация приложений и функций ConectoWorkSpace appplay.xml

            // Чтение параметров приложения
            var MessageIFP = new SystemConecto.MessageIFP();
            if (SystemConecto.IsFilesPack(new KeyValuePair<string, string>(SystemConecto.PutchApp + "configinstallapp.xml", "") , ref  MessageIFP, -2))
            {
                // Параметры-Приложений- считать в память
                // Проверка целосности и правильности и Чтение (авто востановление файла из рез. копии или востановление структуры)
                if (!AppStart.SysConf.ReadConfigXMLAppPlay())
                {
                    // Перезапуск если разрушена целосность конфигурационного файла
                    if (!AppStart.SysConf.ReadConfigXMLAppPlay())
                    {
                        //STOP = true;
                    }
                }
            }
            else
            {
                // Загрузка с памяти
                if (!AppStart.SysConf.ReadConfigXMLAppPlay(1))
                {
                    //STOP = true;
                }
            }

            var Test = AppStart.SysConf.aParamAppPlay[121].id;
            #endregion

            // 4.2 Центральная панель WorkSpace Инсталяция продуктов
            LoadPanelWorkSpace();
        }

        #region Построение интерфейса
        /// <summary>
        /// Отрисовка изменений размера экрана
        /// </summary>
        public void ResolutionDisplay()
        {

            // 3. Установка размеров окна для того чтобы игнорировать нажатие клавиши раскрыть на весь экран
            // Рабочего стола ([0] - Top; [1] - Left; [2] - Width; [3] - Height; [4] - Right;
            this.Top = SystemConecto.WorkAreaDisplayDefault[0]; //+ 10;
            this.Left = SystemConecto.WorkAreaDisplayDefault[1]; //+ 10;
            this.WindGrid.Width = this.Width = SystemConecto.WorkAreaDisplayDefault[7];
            // Пример сбросить значение в авто
            // HeightPic.Height = Double.NaN; //
            // Еще примеры авто размеров
            // column1.Width = new GridLength(1, GridUnitType.Auto); // Auto
            // column2.Width = new GridLength(1, GridUnitType.Star); // * this.Okno.Height = 

            this.Height = WindowConecto.Height = SystemConecto.WorkAreaDisplayDefault[3]; 
            
            // Вікна які знаходятся у цьоу вікні
            // WindowConecto
            WindowConecto.Width = SystemConecto.WorkAreaDisplayDefault[7] - 42;

            // Лінія поділу вікна
            razdelit_fon_png.Width = WindowConecto.Width - 62;

            //this.Location = new System.Drawing.Point(SizeDWArea_a[1], SizeDWArea_a[0]);
            //this.ClientSize = new System.Drawing.Size(SizeDWArea_a[2], SizeDWArea_a[3]);
    
        }
        #endregion



        #region Загрузка и формирование панели ... Conecto WorkSpace ... Выбрать продукт для инсталяции
        /// <summary>
        /// Загрузка и формирование панели Conecto WorkSpace
        /// </summary> 
        public void LoadPanelWorkSpace()
        {
            int NumberApp = 1;
            //установить его ширину
            WorkSpacePanel_All.Width = WorkSpacePanelGrid_All.Width = this.Width - WorkSpacePanel_All.Margin.Left - 40;
            // Установить его высоту
            WorkSpacePanel_All.Height = WorkSpacePanelGrid_All.Height = this.Height - TabPanelButton.Margin.Top - 15;

            // Расчет сетки 
            int CountCells = Convert.ToInt32(Math.Truncate(WorkSpacePanel_All.Width / 230));
            //int CountRows = Convert.ToInt32(Math.Truncate(WorkSpacePanel_All.Height / 230));
            int AllApp = 2;
            // Общее количество программ разделить на количество колонок
            int CountRows = CountCells > AllApp ? 2 / CountCells : 1;

            // Id
            var IdApp = 0;

            for (int nCells = 0; nCells < CountCells; nCells++)
            {
                // ============= Сформировать ячейку
                // http://msdn.microsoft.com/ru-ru/library/ms612655%28v=vs.95%29.aspx
                // Сформировать колонку
                ColumnDefinition cl = new ColumnDefinition();
                cl.Width = new GridLength(145, GridUnitType.Pixel);

                WorkSpacePanelGrid_All.ColumnDefinitions.Add(cl);



                for (int nRows = 0; nRows < CountRows; nRows++)
                {
                    // ============= Сформировать ячейку
                    // Сформировать строку
                    if (nCells == 0)
                    {
                        RowDefinition r = new RowDefinition();
                        r.Height = new GridLength(140, GridUnitType.Pixel);
                        // Добавить в грид
                        WorkSpacePanelGrid_All.RowDefinitions.Add(r);


                    }

                    // Расположить ярлык

                    // Проверка приложения

                    IdApp = SystemConfigControll.ListPanelWorkSpace.ContainsKey(NumberApp) && AppStart.SysConf.aParamAppPlay[SystemConfigControll.ListPanelWorkSpace[NumberApp]].PanelWS.LinkPanel ? SystemConfigControll.ListPanelWorkSpace[NumberApp] : 0;

                    if (SystemConfigControll.ListPanelWorkSpace.ContainsKey(NumberApp) && IdApp > 0)
                    {
                        // 
                        //SystemConecto.ErorDebag(nCells.ToString() + " / " + nRows.ToString() + " / " + NumberApp.ToString());
                        var PanelWS_ = AppStart.SysConf.aParamAppPlay[IdApp].PanelWS;


                        Image Image_WorkS = new Image();

                        if (AppStart.SysConf.aParamAppPlay[IdApp].PuthFileIm == "")
                        {

                        }
                        else
                        {
                            Image_WorkS.Source = new BitmapImage(new Uri(AppStart.SysConf.aParamAppPlay[IdApp].PuthFileIm, UriKind.Relative));
                        }

                        Grid.SetRow(Image_WorkS, nRows);
                        Grid.SetColumn(Image_WorkS, nCells);

                        Image_WorkS.Cursor = System.Windows.Input.Cursors.Hand;

                        // Отладка - реализовать запуск с настройки конфиг файла

                        System.Reflection.MethodInfo loadAppEvents = typeof(AppforWorkSpace).GetMethod("LoadAppEvents_" + IdApp, new Type[] { typeof(Image), typeof(Image) });
                        if (loadAppEvents != null)
                        {
                            // SystemConecto.ErorDebag("LoadAppEvents_" + IdApp, 2);
                            loadAppEvents.Invoke(this, new object[] { Image_WorkS, Image_WorkS });
                        }



                        Image_WorkS.HorizontalAlignment = HorizontalAlignment.Left;
                        Image_WorkS.VerticalAlignment = VerticalAlignment.Top;


                        //PanelWS_

                        // AppPlayPanel

                        // Записать в таблицу
                        WorkSpacePanelGrid_All.Children.Add(Image_WorkS);

                       

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

            WorkSpacePanelGrid_All.ColumnDefinitions.Add(cl_);

            RowDefinition r_ = new RowDefinition();
            r_.Height = GridLength.Auto;
            // Добавить в грид
            WorkSpacePanelGrid_All.RowDefinitions.Add(r_);

            //foreach (KeyValuePair<int, int> AppPlayPanel in SystemConfig.ListPanelWorkSpace)
            //{

            //}
        }
        #endregion


        #region События Click (Клик) функцилнальных клавиш рабочего стола
        private void Close__MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.Visibility = Visibility.Hidden;

            this.Owner.Focus();
            this.Owner = null;
            this.Close();

        }
        #endregion


        #region Изображения как клавиши - Визуализация интерфейса

        //private Rectangle CreateRectangleRadius( Color ColorAll, double Width = 70, double Height = 30)
        //{

        //    //ColorAll  = Colors.Yellow;

        //    //Ellipse ellipse = new Ellipse();
        //    Rectangle ellipse = new Rectangle();
        //    ellipse.RadiusX = ellipse.RadiusY = 8;

        //    ellipse.Width = Width;
        //    ellipse.Height = Height;
        //    // Заливка
        //    SolidColorBrush BrushColor = new SolidColorBrush();
        //    BrushColor.Color = ColorAll;
        //    ellipse.Fill = BrushColor;

        //    //Контур
        //    SolidColorBrush strokeBrush = new SolidColorBrush();
        //    strokeBrush.Color = ColorAll;
        //    ellipse.Stroke = strokeBrush;
        //    ellipse.StrokeThickness = 5;
        //    return ellipse;
        //}


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
                    this.Visibility = Visibility.Hidden;
                    this.Close();
                    //this.Close();

                }

            }


            // Отладка
            // MessageBox.Show(e.Key.ToString());

        }
        #endregion

        private void Pdf1_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            // Не Модальное окно 
            var Window = SystemConecto.ListWindowMain("PalyStoryApp_");
            if (Window != null)
            {
                Window.Show();
            }
            else
            {
                // Не Модальное окно
                PalyStoryApp WinSpecPred = new PalyStoryApp();
                WinSpecPred.Owner = this;
                WinSpecPred.Show();

            }
        }
    }
}
