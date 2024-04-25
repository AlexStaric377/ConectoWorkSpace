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
//--- Анимация
using System.Windows.Media.Animation;

namespace ConectoWorkSpace
{
    /// <summary>
    /// Логика взаимодействия для Window1.xaml
    /// </summary>
    public partial class WaitMessage : Window
    {
        //System.Windows.Threading.DispatcherTimer CloseAuto;
        public WaitMessage()
        {
            InitializeComponent();
            Close_.Visibility = Visibility.Hidden;
            //CloseAuto = new System.Windows.Threading.DispatcherTimer();
            //CloseAuto.Tick += new EventHandler(CloseAutoTick);
            //CloseAuto.Interval = new TimeSpan(0, 0, ConectoWorkSpace.Administrator.AdminPanels.TimeAutoCloseWin);
            //CloseAuto.Start();

            // 3. Установка размеров окна для того чтобы игнорировать нажатие клавиши раскрыть на весь экран
            // Рабочего стола ([0] - Top; [1] - Left; [2] - Width; [3] - Height; [4] - Right;
            // Окно центрируется автоматически
            // this.Top = SystemConecto.WorkAreaDisplayDefault[0] + 10;
            // this.Left = SystemConecto.WorkAreaDisplayDefault[1] + 10;
            this.WinGrid.Width = this.Width;
            this.Okno.Height = this.Height - 100;

            // this.Height = 90;
            //this.Width = 50;
            //this.Okno.Height = 500;
            //MessageBox.Show("file://" + SystemConecto.PutchApp + @"images\loading.gif");
            //SystemConecto.ErorDebag("file://" + SystemConecto.PutchApp + @"images\loading.gif");
            //file://c:\Program Files\Conecto\WorkSpace\Release\images\loading.gif
            //this.AnimeGif.Source = new Uri("file://"+SystemConecto.PutchApp + @"images\loading.gif");

            // Бар
            //Duration duration = new Duration(TimeSpan.FromSeconds(85));
            //DoubleAnimation doubleanimation = new DoubleAnimation(100.0, duration);
            //this.progressBar1.BeginAnimation(ProgressBar.ValueProperty, doubleanimation);

        }
        private void CloseAutoTick(object sender, EventArgs e)
        {
            CloseMessage();
        }

        public void CloseMessage()
        {
            this.Visibility = Visibility.Hidden;

            //this.Owner.Focus();
            this.Owner = null;

            this.Close();
        }

        #region События Click (Клик) функцилнальных клавиш рабочего стола
        private void Close__MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            this.Visibility = Visibility.Hidden;

            this.Owner.Focus();
            this.Owner = null;

            this.Close();
        }

        #endregion


        #region Изображения как клавиши - Визуализация интерфейса 

        
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
                    // Надо включать по событию завершения выполнения основного потока
                    // this.Close();

                }

            }
            

           // Отладка
           // MessageBox.Show(e.Key.ToString());

        }
        #endregion

 

    }
}
