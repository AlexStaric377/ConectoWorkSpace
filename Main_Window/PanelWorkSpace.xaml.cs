using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace ConectoWorkSpace.Main_Window
{
    /// <summary>
    /// Логика взаимодействия для PanelWorkSpace.xaml
    /// </summary>
    public partial class PanelWorkSpace : Window
    {

        /// <summary>
        /// это смещение курсора мыши от верхнего левого угла окна
        /// this is the offset of the mouse cursor from the top left corner of the window
        /// </summary>
        private Point offset = new Point();


        public PanelWorkSpace()
        {
            InitializeComponent();
            // Размещение окана доп. описание
            // https://professorweb.ru/my/WPF/UI_WPF/level23/23_2.php


            //this.StartPosition = FormStartPosition.Manual;
           // this.Location = new Point(Screen.PrimaryScreen.Bounds.Width - this.Width, Screen.PrimaryScreen.Bounds.Height - this.Height);

        }

        /// <summary>
        /// Перемещение панели на рабочем столе ОС
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            // Доп описание
            // http://www.cyberforum.ru/wpf-silverlight/thread403593.html

            //this.DragMove();

            // *********************** Другой метод
            Point cursorPos = PointToScreen(Mouse.GetPosition(this));
            Point windowPos = new Point(this.Left, this.Top);
            offset = (Point)(cursorPos - windowPos);

            // capturing the mouse here will redirect all events to this window, even if
            // the mouse cursor should leave the window area
            //захват мыши здесь перенаправит все события в это окно, даже если
            //курсор мыши должен покинуть область окна
            Mouse.Capture(this, CaptureMode.Element);


        }

        private void OnMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            Mouse.Capture(null);
        }

        private void OnMouseMove(object sender, MouseEventArgs e)
        {
            if (Mouse.Captured == this && Mouse.LeftButton == MouseButtonState.Pressed)
            {
                Point cursorPos = PointToScreen(Mouse.GetPosition(this));
                double newLeft = cursorPos.X - offset.X;
                double newTop = cursorPos.Y - offset.Y;

                // here you can change the window position and implement
                // the snapping behaviour that you need


                //Здесь вы можете изменить положение окна и реализовать
                //нужное вам поведение

                this.Left = newLeft;
                this.Top = newTop;
            }
        }

    }
}
