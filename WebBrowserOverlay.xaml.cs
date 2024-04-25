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
using System.Diagnostics;
using System.Windows.Interop;
using System.Runtime.InteropServices;

namespace ConectoWorkSpace
{
    /// <summary>
    /// Displays a WebBrowser control over a given placement target element in a WPF Window.
    /// The owner window can be transparent, but not this one, due mixing DirectX and GDI drawing. 
    /// WebBrowserOverlayWF uses WinForms to avoid this limitation.
    /// Отображает WebBrowser контроль над данным элементом размещения мишени в окно WPF.
    /// Окно владельца может быть прозрачным, но не этот, из-за смешивания рисунок DirectX и GDI.
    /// WebBrowserOverlayWF использует WinForms обойти это ограничение
    /// </summary>
    public partial class WebBrowserOverlay : Window
    {
        FrameworkElement _placementTarget;

        public double Height_Obj = 0;
        public double Width_Obj = 0;

        public WebBrowser WebBrowser { get { return _wb; } }

        public WebBrowserOverlay(FrameworkElement placementTarget, double Height_ = 0, double Width_ = 0)
        {
            
            InitializeComponent();
            // Определение высоты
            if(Height_!=0){
                Height_Obj = Height_; //this.Height = _wb.Height

            }
            // Определение ширины
            if (Width_ != 0)
            {
                Width_Obj = Width_; //this.Width = _wb.Width

            }


            _placementTarget = placementTarget;
            Window owner = Window.GetWindow(placementTarget);
            Debug.Assert(owner != null);

            //owner.SizeChanged += delegate { OnSizeLocationChanged(); };
            owner.LocationChanged += delegate { OnSizeLocationChanged(); };
            _placementTarget.SizeChanged += delegate { OnSizeLocationChanged(); };
            owner.Closed += delegate { OnClosing_(); };

            if (owner.IsVisible)
            {
                Owner = owner;
                Show();
            }
            else
                owner.IsVisibleChanged += delegate
                {
                    if (owner.IsVisible)
                    {
                        Owner = owner;
                        Show();
                    }
                };
            //this.Visibility = Visibility.Hidden;
            // Изменить размеры
            //OnSizeLocationChanged();
            //owner.LayoutUpdated += new EventHandler(OnOwnerLayoutUpdated);
            //this.Visibility = Visibility.Visible;
        }

        protected void OnClosing_()
        {
           // SystemConecto.ErorDebag("OnClosing_", 1);
           this.Close();
        }

        protected override void OnClosing(System.ComponentModel.CancelEventArgs e)
        {
            base.OnClosing(e);
            if (!e.Cancel)
                // Delayed call to avoid crash due to Window bug.
                // Задержка вызова избежать аварии из-за Окно ошибка.
                Dispatcher.BeginInvoke((Action)delegate
                {
                    //SystemConecto.ErorDebag("Вышел", 1);
                    Owner.Close();
                });
        }

        void OnSizeLocationChanged()
        {
            // Проверка создания окна которое владеет данным окном
            if (Owner!=null)
            {
                Point offset = _placementTarget.TranslatePoint(new Point(), Owner);
                Point size = new Point(_placementTarget.ActualWidth, _placementTarget.ActualHeight);
                HwndSource hwndSource = (HwndSource)HwndSource.FromVisual(Owner);
                CompositionTarget ct = hwndSource.CompositionTarget;
                offset = ct.TransformToDevice.Transform(offset);
                size = ct.TransformToDevice.Transform(size);

                Win32.POINT screenLocation = new Win32.POINT(offset);
                Win32.ClientToScreen(hwndSource.Handle, ref screenLocation);
                Win32.POINT screenSize = new Win32.POINT(size);

                Win32.MoveWindow(((HwndSource)HwndSource.FromVisual(this)).Handle, screenLocation.X, screenLocation.Y, screenSize.X, screenSize.Y, true);
                
                // Убираем глюк перерисования окна
                if(Height_Obj!=0){
                     _wb.Height = Height_Obj; //this.Height =

                }
                // Определение ширины
                if (Width_Obj != 0)
                {
                    _wb.Width = Width_Obj; //this.Width =

                }
               

            } 
       }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            // Проверка создания окна которое владеет данным окном
            if (Owner != null)
            {
                // Да 
                if (e.Key == Key.Return)
                {


                }
                else
                {

                    // Нет закрыть
                    if (e.Key == Key.Escape)
                    {

                        Owner.Close();

                    }

                }



            }
        }
    };
}

