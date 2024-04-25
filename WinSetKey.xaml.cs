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
    public partial class WinSetKey : Window
    {
        public WinSetKey()
        {
            InitializeComponent();
        }

        private void Close_Grid_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (Administrator.AdminPanels.SetWinSetHub == "AddApp")
            {
                System.Windows.Application.Current.Dispatcher.Invoke(new Action(delegate ()
                {
                    ConectoWorkSpace.Administrator.AdminPanels ConectoWorkSpace_InW = AppStart.LinkMainWindow("WAdminPanels");
                    ConectoWorkSpace_InW.PuthFile.Text = "";
                }));
                Administrator.AdminPanels.SetWinSetHub ="";

            } 
            this.Close();
        }
        private void Close_Grid_MouseMove(object sender, MouseEventArgs e)
        {
            if (Administrator.AdminPanels.SetWinSetHub == "AddApp")
            {
                System.Windows.Application.Current.Dispatcher.Invoke(new Action(delegate ()
                {
                    ConectoWorkSpace.Administrator.AdminPanels ConectoWorkSpace_InW = AppStart.LinkMainWindow("WAdminPanels");
                    ConectoWorkSpace_InW.PuthFile.Text = "";
                }));
                Administrator.AdminPanels.SetWinSetHub = "";

            }
            this.Close();
        }

        #region Обработка событий любой клавиатуры
        private void ConectoW_KeyDown(object sender, KeyEventArgs e)
        {
            // Да завершить
            if (e.Key == Key.Return)AppStart.EndWorkPC(); // Это работает
            else if (e.Key == Key.Escape)this.Close();
        }

        #endregion

 
        // Процедура загрузки таблицы в окно Датагрид.
 
        private void SelectBackFront_Loaded(object sender, RoutedEventArgs e)
        {
             Administrator.AdminPanels.Grid_key("SELECT * from ACTIVBACKFRONT");
        }
        // Процедура выбора строки из таблицы
        private void SelectBackFront_MouseUp(object sender, MouseButtonEventArgs e)
        {
           System.Windows.Application.Current.Dispatcher.Invoke(new Action(delegate ()
           {
               ConectoWorkSpace.Administrator.AdminPanels ConectoWorkSpace_InW = AppStart.LinkMainWindow("WAdminPanels");
               ConectoWorkSpace_InW.GridKeyMouseUp("SELECT count(*) from ACTIVBACKFRONT", 4);
               if (Administrator.AdminPanels.SetWinSetHub == "AddApp")
               {
                   Administrator.AdminPanels.AppFront();
               }
                   
               if (Administrator.AdminPanels.SetWinSetHub == "Testkey")
               {

                   string StrLockey = "SELECT * FROM LOCKEY WHERE LOCKEY.PUTH = " + "'" + ConectoWorkSpace.Administrator.AdminPanels.TestKeyPuth + "'";
                   string StrNetkey = "SELECT * FROM NETKEY WHERE NETKEY.PUTH = " + "'" + ConectoWorkSpace.Administrator.AdminPanels.TestKeyPuth + "'";
                   string SelectTab = "SELECT * FROM TESTKEY";
                   string InsertTab = "TESTKEY";
                   string StrCreate = "SELECT * FROM TESTKEY WHERE TESTKEY.PUTH = " + "'" + ConectoWorkSpace.Administrator.AdminPanels.TestKeyPuth + "'";
                   ConectoWorkSpace.InstallB52.AddTabKey(StrCreate, InsertTab, SelectTab, StrLockey, StrNetkey);
               }

               if (Administrator.AdminPanels.SetWinSetHub == "Lockey")
               {
                   string StrLockey = "SELECT * FROM TESTKEY WHERE TESTKEY.PUTH = " + "'" + ConectoWorkSpace.Administrator.AdminPanels.TestKeyPuth + "'";
                   string StrNetkey = "SELECT * FROM NETKEY WHERE NETKEY.PUTH = " + "'" + ConectoWorkSpace.Administrator.AdminPanels.TestKeyPuth + "'";
                   string SelectTab = "SELECT * FROM LOCKEY";
                   string InsertTab = "LOCKEY";
                   string StrCreate = "SELECT * FROM LOCKEY WHERE LOCKEY.PUTH = " + "'" + ConectoWorkSpace.Administrator.AdminPanels.TestKeyPuth + "'";
                   ConectoWorkSpace.InstallB52.AddTabKey(StrCreate, InsertTab, SelectTab, StrLockey, StrNetkey);
               }
               if (Administrator.AdminPanels.SetWinSetHub == "Netkey")
               {
                   string StrLockey = "SELECT * FROM LOCKEY WHERE LOCKEY.PUTH = " + "'" + ConectoWorkSpace.Administrator.AdminPanels.TestKeyPuth + "'";
                   string StrNetkey = "SELECT * FROM TESTKEY WHERE TESTKEY.PUTH = " + "'" + ConectoWorkSpace.Administrator.AdminPanels.TestKeyPuth + "'";
                   string SelectTab = "SELECT * FROM NETKEY";
                   string InsertTab = "NETKEY";
                   string StrCreate = "SELECT * FROM NETKEY WHERE NETKEY.PUTH = " + "'" + ConectoWorkSpace.Administrator.AdminPanels.TestKeyPuth + "'";
                   ConectoWorkSpace.InstallB52.AddTabKey(StrCreate, InsertTab, SelectTab, StrLockey, StrNetkey);
               }
           }));
           this.Close();
        }
    }
}
