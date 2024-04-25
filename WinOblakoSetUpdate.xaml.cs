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
    public partial class WinOblakoSetUpdate : Window
    {
        public WinOblakoSetUpdate(string TextWindows = null)
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

            if (ConectoWorkSpace.Administrator.AdminPanels.Inst2530 == "PostGre") InstallB52.InstallServPostgresql();
            if (ConectoWorkSpace.Administrator.AdminPanels.Inst2530 == "DelPostGre") InstallB52.UnInstallServPostgresql();
            if (ConectoWorkSpace.Administrator.AdminPanels.Inst2530 == "25")InstallB52.InstallServBD2_5();
            if (ConectoWorkSpace.Administrator.AdminPanels.Inst2530 == "30") InstallB52.InstallServFB3_0();
            if (ConectoWorkSpace.Administrator.AdminPanels.Inst2530 == "del25")ConectoWorkSpace.Administrator.AdminPanels.SreverFB25Delete();
            if (ConectoWorkSpace.Administrator.AdminPanels.Inst2530 == "del30") ConectoWorkSpace.Administrator.AdminPanels.SreverFB30Delete();
            if (ConectoWorkSpace.Administrator.AdminPanels.Inst2530 == "BackUpRestory")
            {
                ConectoWorkSpace.Administrator.AdminPanels.Inst2530 = "Ok";

                System.Windows.Application.Current.Dispatcher.Invoke(new Action(delegate ()
                {
                    ConectoWorkSpace.Administrator.AdminPanels ConectoWorkSpace_Run = AppStart.LinkMainWindow("WAdminPanels");

                    Administrator.AdminPanels.UpdateKeyReestr("PutchBackUpLoc_Fbd25", ConectoWorkSpace_Run.Putch_Fbd25.Text);
                    ConectoWorkSpace_Run.NameArhiv.Visibility = Visibility.Visible;
                    ConectoWorkSpace_Run.Putch_Fbd25.Visibility = Visibility.Visible;
                    ConectoWorkSpace_Run.DirPath_Fbd25.Visibility = Visibility.Visible;
                    ConectoWorkSpace_Run.BackUpLocServerBD25.Visibility = Visibility.Visible;
                }));
            }
            if (ConectoWorkSpace.Administrator.AdminPanels.Inst2530 == "UnInstallBackB52") InstallB52.PackBackB52();
            if (ConectoWorkSpace.Administrator.AdminPanels.Inst2530 == "UnInstallFrontB52") InstallB52.PackFrontB52();
            ConectoWorkSpace.Administrator.AdminPanels.SetUpdateRestore = 1;
            
            this.Close();
        }
        private void Close_SetUpdate_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            ConectoWorkSpace.Administrator.AdminPanels.SetUpdateRestore = 2;
            if (ConectoWorkSpace.Administrator.AdminPanels.Inst2530 == "UnInstallBackB52") InstallB52.DeleteBackB52("");
            if (ConectoWorkSpace.Administrator.AdminPanels.Inst2530 == "UnInstallFrontB52") InstallB52.DeleteFrontB52("");
            System.Windows.Application.Current.Dispatcher.Invoke(new Action(delegate ()
            {
                ConectoWorkSpace.Administrator.AdminPanels ConectoWorkSpace_Del = AppStart.LinkMainWindow("WAdminPanels");
                ConectoWorkSpace_Del.DeletSrever25.Foreground = Brushes.White;
                ConectoWorkSpace_Del.AddSever.Foreground = Brushes.White;
                ConectoWorkSpace_Del.StoptServPostGre.Foreground = Brushes.White;
            }));
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
