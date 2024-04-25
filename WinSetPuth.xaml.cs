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
    public partial class WinSetPuth : Window
    {
        public WinSetPuth(string TextWindows = null)
        {
            InitializeComponent();
            if (TextWindows != null)
            {
                  this.textBlock1.Text = TextWindows;
            }
        }

        #region События Click (Клик) функцилнальных клавиш рабочего стола
        private void Ok_SetUpdate_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            ConectoWorkSpace.Administrator.AdminPanels.SetUpdateRestore = 3;
            if (Administrator.AdminPanels.Inst2530 == "security2") ConectoWorkSpace.Administrator.AdminPanels.ExitAddSetBd25();
            if (Administrator.AdminPanels.Inst2530 == "security3") ConectoWorkSpace.Administrator.AdminPanels.ExitAddSetBd30();
            if (Administrator.AdminPanels.Inst2530 == "Front") InstallB52.SetPuthFrontB52();
            else InstallB52.SetPuthB52();
            this.Close();
        }
        private void Close_SetUpdate_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

            ConectoWorkSpace.Administrator.AdminPanels.SetUpdateRestore = 4;
            if (Administrator.AdminPanels.Inst2530 == "Front")InstallB52.ReinstalFBD();
            if (Administrator.AdminPanels.Inst2530 == "security2") ConectoWorkSpace.Administrator.AdminPanels.ExitAddSetBd25();
            if (Administrator.AdminPanels.Inst2530 == "security3") ConectoWorkSpace.Administrator.AdminPanels.ExitAddSetBd30();
            this.Close();
            return;
    
        }
        #endregion


        #region Изображения как клавиши - Визуализация интерфейса 
        private void Ok_SetUpdate_MouseLeave(object sender, MouseEventArgs e)
        {
            this.Ok_But.Background = new ImageBrush(new BitmapImage(new Uri(@"pack://application:,,,/Conecto®%20WorkSpace;component/Images/knp_zelt1.png")));
        }
        private void Ok_SetUpdate_MouseMove(object sender, MouseEventArgs e)
        {
            //var uriSource = new Uri(@"/Conecto®%20WorkSpace;component/Images/knp_zelt1.png", UriKind.Relative); - в фоне не работает
            this.Ok_But.Background = new ImageBrush(new BitmapImage(new Uri(@"pack://application:,,,/Conecto®%20WorkSpace;component/Images/knp_zelt2.png")));
        }

        private void Close_SetUpdate_MouseLeave(object sender, MouseEventArgs e)
        {
            this.Close_But.Background = new ImageBrush(new BitmapImage(new Uri(@"pack://application:,,,/Conecto®%20WorkSpace;component/Images/knp_zelt1.png")));
        }
        private void Close_SetUpdate_MouseMove(object sender, MouseEventArgs e)
        {
 
            this.Close_But.Background = new ImageBrush(new BitmapImage(new Uri(@"pack://application:,,,/Conecto®%20WorkSpace;component/Images/knp_zelt2.png")));
 
        }
        private void Close_Set_MouseMove(object sender, MouseEventArgs e)
        {
            var uriSource = new Uri(@"/Conecto®%20WorkSpace;component/Images/x2.png", UriKind.Relative);
            this.Close_F.Source = new BitmapImage(uriSource);
        }

        private void Close_Set_MouseLeave(object sender, MouseEventArgs e)
        {
            var uriSource = new Uri(@"/Conecto®%20WorkSpace;component/Images/x1.png", UriKind.Relative);
            this.Close_F.Source = new BitmapImage(uriSource);
        }
        #endregion


        #region Обработка событий любой клавиатуры
        private void ConectoW_KeyDown(object sender, KeyEventArgs e)
        {
            // Да завершить
            if (e.Key == Key.Return)
            {
                AppStart.EndWorkPC(); // Это работает

            }
            else
            {

                // Нет отказаться
                if (e.Key == Key.Escape)
                {
                    this.Close();

                }

            }
            

           // Отладка
           // MessageBox.Show(e.Key.ToString());

        }
        #endregion










    }
}
