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


namespace ConectoWorkSpace._2_KassaExport
{
    
    /// <summary>
    /// Логика взаимодействия для KassaExport.xaml
    /// </summary>
    public partial class KassaExport : Window
    {

        // Время ожидания разрыва
        Stopwatch WaitNetTimeStart_EXPORT = new Stopwatch();

        private Grid MessageTextInner = new Grid();

        private Grid TextInfo = new Grid();

        #region Структура данных потока

        // Структура параметров потока обращения к веб серверу
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
            /// Каталог экспорта файлов
            /// </summary>
            public string PutchDir_ { get; set; }
            /// <summary>
            /// Тип файлы CSV 1251
            /// </summary>
            public string TypeFile { get; set; }
            /// <summary>
            /// Номер выполняемого блока
            /// </summary>
            public int idBlock { get; set; }
            /// <summary>
            /// Окно вызова потока
            /// </summary>
            public Window WinOnStart { get; set; }

        }

        #endregion

        public KassaExport()
        {
            InitializeComponent();




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
            this.WinGrid.Height = this.Height = SystemConecto.WorkAreaDisplayDefault[3] - 35;
            this.WinGrid.Width = this.Width = SystemConecto.WorkAreaDisplayDefault[2] - (277 + 5 + 10);
            //TabOkno.Width = this.WinGrid.Width - 110; // Слева 10 px и поля по 50px слева и справа

            //// Тело окна
            //this.Okno.Height = this.Height - 100;
            //TabOkno.Height = this.Okno.Height - 24;

            // Вікна які знаходятся у цьоу вікні
            //WiknoConecto.Height = this.Okno.Height;

            // Отцентрировать сообщение
            //Message1.Margin = new Thickness(0, 0, 0, 0);

        }
        #endregion

        #region События Click (Клик) функцилнальных клавиш рабочего стола
        private void Close__MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Close_();
        }
        #endregion

        #region Клавиша вихода из окна
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

    }
}
