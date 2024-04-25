using System;
using System.IO;
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
    public partial class WinSetHub : Window
    {
        public WinSetHub()
        {
            InitializeComponent();
        }

        private void Close_Grid_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

            this.Close();

        }
        private void Close_Grid_MouseMove(object sender, MouseEventArgs e)
        {
            //var uriSource = new Uri(@"/Conecto®%20WorkSpace;component/Images/x2.png", UriKind.Relative);
            //this.Close_Grid.Source = new BitmapImage(uriSource);
            this.Close();
        }

 
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

 
        // Процедура загрузки таблицы в окно Датагрид.
        private void DateGrid25_Loaded(object sender, RoutedEventArgs e)
        {
            if (Convert.ToInt32(AppStart.TableReestr["BackFbd25OnOff"].ToString()) == 2 || Convert.ToInt32(AppStart.TableReestr["FrontFbd25OnOff"].ToString()) == 2)
            {
                Administrator.AdminPanels.HubGrid_Puth("SELECT * from CONNECTIONBD25");
                return;

            }

 
            if (Convert.ToInt32(AppStart.TableReestr["BackFbd30OnOff"].ToString()) == 2 || Convert.ToInt32(AppStart.TableReestr["FrontFbd30OnOff"].ToString()) == 2)
            {
                Administrator.AdminPanels.HubGrid_Puth("SELECT * from CONNECTIONBD30");
                return;
            }
 
        }

        private void grid25_MouseUp(object sender, MouseButtonEventArgs e)
        {
            string InsertExecute = "";

            if (Administrator.AdminPanels.SetWinSetHub == "SetTcpIpBack" || Administrator.AdminPanels.SetWinSetHub == "ChangeAliasBack" )
            {
                if (Convert.ToInt32(AppStart.TableReestr["BackFbd25OnOff"].ToString()) == 2)  InsertExecute = "SELECT count(*) from CONNECTIONBD25";
                if (Convert.ToInt32(AppStart.TableReestr["BackFbd30OnOff"].ToString()) == 2) InsertExecute = "SELECT count(*) from CONNECTIONBD30";
 
            }

            if (Administrator.AdminPanels.SetWinSetHub == "SetTcpIpFront" || Administrator.AdminPanels.SetWinSetHub == "ChangeAliasFront" )
            {
                if ( Convert.ToInt32(AppStart.TableReestr["FrontFbd25OnOff"].ToString()) == 2) InsertExecute = "SELECT count(*) from CONNECTIONBD25";
                if ( Convert.ToInt32(AppStart.TableReestr["FrontFbd30OnOff"].ToString()) == 2) InsertExecute = "SELECT count(*) from CONNECTIONBD30";

            }
 
            Administrator.AdminPanels.Hubgrid25_MouseUp(InsertExecute);
            System.Windows.Application.Current.Dispatcher.Invoke(new Action(delegate ()
            {
                ConectoWorkSpace.Administrator.AdminPanels ConectoWorkSpace_InW = AppStart.LinkMainWindow("WAdminPanels");
                if (Administrator.AdminPanels.CurrentBack == 1)
                {
                    if (Administrator.AdminPanels.SetWinSetHub == "ChangeAliasBack")
                    {
                        Administrator.AdminPanels.UpdateKeyReestr("BackOfAlias", Administrator.AdminPanels.SelectAlias);
                        ConectoWorkSpace_InW.ChangeAliasBack.IsEnabled = false;
                        InstallB52.ChangeAliasBack(1);



                    }
                    if (Administrator.AdminPanels.SetWinSetHub == "SetTcpIpBack")
                    {
                        string ContentPuth = Administrator.AdminPanels.SelectPuth.Length >38 ? "..."+Administrator.AdminPanels.SelectPuth.Substring(Administrator.AdminPanels.SelectPuth.Length - 35, 35) :Administrator.AdminPanels.SelectPuth;
                        ConectoWorkSpace_InW.SetBackOfPuth.Content = "БД: " + ContentPuth;
                        ConectoWorkSpace_InW.SetBackOfPuth.Foreground = Brushes.Green;
                        Administrator.AdminPanels.BackPutchBD = Administrator.AdminPanels.SelectPuth;
                        ConectoWorkSpace_InW.LabBackOfNet.Visibility = Visibility.Hidden;
                        ConectoWorkSpace_InW.SetPuthBdBackNet.Visibility = Visibility.Hidden;

                    }
                }
                if (Administrator.AdminPanels.CurrentFront == 1)
                {

                    if (Administrator.AdminPanels.SetWinSetHub == "ChangeAliasFront")
                    {
                        Administrator.AdminPanels.UpdateKeyReestr("FrontAlias", Administrator.AdminPanels.SelectAlias);
                        ConectoWorkSpace_InW.ChageAliasFront25.IsEnabled = false;
                        Administrator.AdminPanels.AliasChageFront25 = Administrator.AdminPanels.SelectAlias;
                        InstallB52.ChangeAliasBack(2);
                    }
                    if (Administrator.AdminPanels.SetWinSetHub == "SetTcpIpFront")
                    {
                        string ContentPuth = Administrator.AdminPanels.SelectPuth.Length > 38 ? "..." + Administrator.AdminPanels.SelectPuth.Substring(Administrator.AdminPanels.SelectPuth.Length - 35, 35) : Administrator.AdminPanels.SelectPuth;
                        ConectoWorkSpace_InW.FrontPuhtBd.Content = "БД: " + ContentPuth;
                        ConectoWorkSpace_InW.FrontPuhtBd.Foreground = Brushes.Green;
                        Administrator.AdminPanels.FrontPutchBD = Administrator.AdminPanels.SelectPuth;
                        ConectoWorkSpace_InW.LabFrontOfNet.Visibility = Visibility.Hidden;
                        ConectoWorkSpace_InW.SetPuthBdFrontNet.Visibility = Visibility.Hidden;

                    }
  
                }
            }));
            this.Close();

        }

    }
}
