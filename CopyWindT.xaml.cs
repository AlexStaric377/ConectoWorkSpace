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
    public partial class CopyWindT : Window
    {
        public CopyWindT()
        {
            InitializeComponent();

            // 3. Установка размеров окна для того чтобы игнорировать нажатие клавиши раскрыть на весь экран
            // Рабочего стола ([0] - Top; [1] - Left; [2] - Width; [3] - Height; [4] - Right;

            this.Top = SystemConecto.WorkAreaDisplayDefault[0] +10;
            this.Left = SystemConecto.WorkAreaDisplayDefault[1] + 10;
            this.WinGrid.Width = this.Width = SystemConecto.WorkAreaDisplayDefault[2] - (277 + 5 + 10);            
            this.Height = SystemConecto.WorkAreaDisplayDefault[3] - 35;
            this.Okno.Height = this.Height - 100;


            // this.WindGrid.Height = SystemConecto.WorkAreaDisplayDefault[3];
            // this.WinGrid.Width = SystemConecto.WorkAreaDisplayDefault[2] - 140;

            //this.Height = 525;
            //this.Okno.Height = 500;

        }

        #region События Click (Клик) функцилнальных клавиш рабочего стола
        private void Close__MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.Visibility = Visibility.Hidden;
            this.Close();
            //this.Close();
            // this.Owner = null;
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
                    this.Visibility = Visibility.Hidden;
                    this.Close();
                    //this.Close();

                }

            }
            

           // Отладка
           // MessageBox.Show(e.Key.ToString());

        }
        #endregion

        private void ColumnDefinition_ImageFailed(object sender, ExceptionRoutedEventArgs e)
        {

        }










    }
}
