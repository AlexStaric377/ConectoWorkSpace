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
    /// Логика взаимодействия для Window1.xaml
    /// </summary>
    public partial class WaitFon : Window
    {
        public WaitFon(string VariantWindow = "")
        {
            InitializeComponent();
            ResolutionDisplay(VariantWindow);
           
        }

        /// <summary>
        /// Отрисовка изменений размера
        /// </summary>
        public void ResolutionDisplay(string VariantWindow = "")
        {

            // Варианты окон
            if (VariantWindow == "Black")
            {
                WindGrid.Background = Brushes.Black;
            }

            // 3. Установка размеров окна для того чтобы игнорировать нажатие клавиши раскрыть на весь экран
            // Рабочего стола ([0] - Top; [1] - Left; [2] - Width; [3] - Height; [4] - Right;
            this.Top = SystemConecto.WorkAreaDisplayDefault[0];
            this.Left = SystemConecto.WorkAreaDisplayDefault[1];
            this.Width = SystemConecto.WorkAreaDisplayDefault[2];
            this.Height = SystemConecto.WorkAreaDisplayDefault[3];
            this.WindGrid.Height = SystemConecto.WorkAreaDisplayDefault[3];
            this.WindGrid.Width = SystemConecto.WorkAreaDisplayDefault[2];
            //this.Location = new System.Drawing.Point(SizeDWArea_a[1], SizeDWArea_a[0]);
            //this.ClientSize = new System.Drawing.Size(SizeDWArea_a[2], SizeDWArea_a[3]);

        }


        #region События Click (Клик) функцилнальных клавиш рабочего стола
        private void Close_F_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            // Убирает глюк закрытия дочернего окна (потеря фокуса) полная версия окно Admin
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
                AppStart.WaitTaskWindow(1);
            }
            else
            {
                //MessageBox.Show("стоп");
                AppStart.WaitTaskWindow(0);
            }
            
        }











    }
}
