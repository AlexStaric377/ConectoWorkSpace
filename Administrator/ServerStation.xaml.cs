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
    /// Логика взаимодействия для ServerStation.xaml
    /// </summary>
    public partial class ServerStation : Window
    {
        public ServerStation()
        {
            InitializeComponent();
            ResolutionDisplay();
            // Активировать вкладку
            MenuItem();
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
            this.Left = SystemConecto.WorkAreaDisplayDefault[1] + 155;
            this.Width = SystemConecto.WorkAreaDisplayDefault[2];
            PanelWindow.Height = new GridLength(SystemConecto.WorkAreaDisplayDefault[3] - 45 - 32);
            this.Height = this.WindGrid.Height = SystemConecto.WorkAreaDisplayDefault[3] - 45;

            this.WindGrid.Width = SystemConecto.WorkAreaDisplayDefault[2] - 155;
            //this.Location = new System.Drawing.Point(SizeDWArea_a[1], SizeDWArea_a[0]);
            //this.ClientSize = new System.Drawing.Size(SizeDWArea_a[2], SizeDWArea_a[3]);

            // Скрыть таблицы окон
            //this.TabApp.Visibility = Visibility.Hidden;
            //this.StructGrid.Visibility = Visibility.Hidden;
            //this.NetViewGrid.Visibility = Visibility.Hidden;
            //this.CoristuvachiGrid.Visibility = Visibility.Hidden;
            //this.ConectoGrid.Visibility = Visibility.Hidden;
            //this.SKDGrid.Visibility = Visibility.Hidden;
        }
        #endregion

       


        #region События Click (Клик) функцилнальных клавиш окна
        private void Close_PanelSys()
        {

            SystemConectoInterfice.UserInterficeClose();

            System.Windows.Application.Current.Dispatcher.Invoke(new Action(delegate()
            {
                //MainWindow ConectoWorkSpace_InW = (MainWindow)Application.Current.MainWindow;
                MainWindow ConectoWorkSpace_InW = AppStart.LinkMainWindow();
                // Ссылка на объект и метод
                ConectoWorkSpace_InW.CloseAdministrator();
            }));

            this.Close();
            // this.Owner = null;
        }

        #region Клавиша вихода из окна
        private void ImButExit_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            var uriSource = new Uri(@"/Conecto®%20WorkSpace;component/Images/vihod1.png", UriKind.Relative);
            ImButExit.Source = new BitmapImage(uriSource);
            Close_PanelSys();


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
            Close_PanelSys();
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

        private void MenuItem(string ItemName = "OpciiTabApp", string ItemLabelNew = "Menu1_1")
        {
            // Отступ сверху и слева
            var TopBack = this.WindGrid.Margin.Top + 55;
            var LeftBack = this.WindGrid.Margin.Left + 18;

            // Закрыть если есть активное окно
            if (ActiveItemMenu1.Length > 0)
            {
                //Grid ItemNameGrid = (Grid)LogicalTreeHelper.FindLogicalNode(this, ActiveItemMenu1);
                //if (ItemNameGrid != null)
                //{
                //    ItemNameGrid.Visibility = Visibility.Hidden;
                //}
                TabOkno.SelectedItem = (TabItem)TabOkno.FindName(ItemName); 
                // изменить свойства меню
                // Текст
                Label ItemNamLabel = (Label)LogicalTreeHelper.FindLogicalNode(this, ActiveItemMenu1_label);
                if (ItemNamLabel != null)
                {
                    ItemNamLabel.Cursor = Cursors.Hand;
                    ItemNamLabel.Foreground = Brushes.White;
                }
                // Фон
                Border ItemNamBorder = (Border)LogicalTreeHelper.FindLogicalNode(this, "brBut_" + ActiveItemMenu1_label);
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
            Border ItemNamBorder_ = (Border)LogicalTreeHelper.FindLogicalNode(this, "brBut_" + ActiveItemMenu1_label);
            if (ItemNamBorder_ != null)
            {
                ItemNamBorder_.Background = new ImageBrush(new BitmapImage(new Uri(@"pack://application:,,,/Conecto®%20WorkSpace;component/Images/pnsys_zeltoe_vydilen.png")));
            }

            //Uri uriSource = null;

            // Открыть Таблицу закладки
            switch (ItemName)
            {
                case ("OpciiTabApp"):

                    // Заголовок
                   // NazvaWindowWhite.Source = new BitmapImage(new Uri(@"/Conecto®%20WorkSpace;component/Images/pnsys_operaz_sistem.png", UriKind.Relative));





                    //GridOSM(LeftBack, TopBack);

                    break;

                case ("DevicesTabApp"):

                    // Заголовок
                  //  uriSource = new Uri(@"/Conecto®%20WorkSpace;component/Images/pnsys_nastroyka.png", UriKind.Relative);
                   // NazvaWindowWhite.Source = new BitmapImage(uriSource);

                   // StructGridM(LeftBack, TopBack);

                    break;

                case ("ServersTabApp"):

                    // Заголовок
                  //  uriSource = new Uri(@"/Conecto®%20WorkSpace;component/Images/pnsys_set.png", UriKind.Relative);
                   // NazvaWindowWhite.Source = new BitmapImage(uriSource);


                    //this.NetViewGrid.Visibility = Visibility.Visible;
                    //this.NetViewGrid.Margin = new Thickness(LeftBack, TopBack, 0, 0); ;
                    //this.NetViewGrid.Height = this.WindGrid.Height - 55;
                    //this.NetViewGrid.Width = this.WindGrid.Width - 50;

                    break;
            }

        }


        #region ==== События нажатия на меню

        private void Menu1_1_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

        }
        private void Menu1_1_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            MenuItem("OpciiTabApp", this.Menu1_1.Name);
        }
        private void TextBlock_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            MenuItem("OpciiTabApp", this.Menu1_1.Name);
        }


        private void Menu1_2_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            MenuItem("DevicesTabApp", this.Menu1_2.Name);
        }
        private void Menu1_3_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            MenuItem("ServersTabApp", this.Menu1_3.Name);
        }
    

        #endregion

        #endregion

    }



}
