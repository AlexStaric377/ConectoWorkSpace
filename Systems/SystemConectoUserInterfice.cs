using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
//--- Изображения
using System.Windows.Media.Imaging;

namespace ConectoWorkSpace
{
    class SystemConectoInterfice
    {
        #region Сценарии запуска приложения в зависимости от ролей
        /// <summary>
        /// Администрирование интерфейса пользователей, согласно прав доступа
        /// </summary>
        public static void UserInterfice(string NameAutorize_)
        {

            // Основное окно
            //MainWindow ConectoWorkSpace_InW = (MainWindow)Application.Current.MainWindow;
            MainWindow ConectoWorkSpace_InW = AppStart.LinkMainWindow();
            if (ConectoWorkSpace_InW == null)
            {
                return;
            }

            // MessageBox.Show(SystemConecto.LoginUserAutoriz);
            switch (NameAutorize_)
            {
                case "AdminOpcii":

                        // Интерфейс Системного Администратора программы (Интегрированные учетные записи)
                    if (SystemConecto.AutorizUser.LoginUserAutoriz == "Autorize_pass-admin-IT" || SystemConecto.AutorizUser.LoginUserAutoriz == "Autorize_Admin-Conecto")
                        {

                            // Ссылка на объект
                            var uriSource = new Uri(@"/Conecto®%20WorkSpace;component/Images/admin_1.png", UriKind.Relative);
                            ConectoWorkSpace_InW.AdminButIm_.Source = new BitmapImage(uriSource);
                            

                            // Доп панель в панели задач (проверка режима терминал)
                            if (SystemConecto.TerminalStatus)
                            {
                                ConectoWorkSpace_InW.ButCatalog.Visibility = Visibility.Visible;
                                ConectoWorkSpace_InW.ButKeyboard.Visibility = Visibility.Visible;
                                ConectoWorkSpace_InW.Note.Visibility = Visibility.Visible;

                                 HookSystemKeys.FunUnHook();// Запрещаем системные клавиши
                            }

                            // запуск окна
                            ConectoWorkSpace_InW.OpenAdministrator();


                        }


                    break;

                case "AdminPanels":

                    // Интерфейс Системного Администратора программы (Интегрированные учетные записи)
                    if (SystemConecto.AutorizUser.LoginUserAutoriz == "Autorize_pass-admin-IT" || SystemConecto.AutorizUser.LoginUserAutoriz == "Autorize_Admin-Conecto")
                    {

                        // Ссылка на объект
                        var uriSource = new Uri(@"/Conecto®%20WorkSpace;component/Images/admin_1.png", UriKind.Relative);
                        ConectoWorkSpace_InW.AdminButIm_.Source = new BitmapImage(uriSource);


                        // Доп панель в панели задач (проверка режима терминал)
                        if (SystemConecto.TerminalStatus)
                        {
                            ConectoWorkSpace_InW.ButCatalog.Visibility = Visibility.Visible;
                            ConectoWorkSpace_InW.ButKeyboard.Visibility = Visibility.Visible;
                            ConectoWorkSpace_InW.Note.Visibility = Visibility.Visible;

                            HookSystemKeys.FunUnHook();// Запрещаем системные клавиши
                        }

                        // запуск окна
                        ConectoWorkSpace_InW.OpenWindowAdminPanels();


                    }


                    break;


                // Старая разработка
                //case "B52FrontOffice7":case "B52BackOffice":

                default :

                    // AppPlayStory
                    // Событие точка (метод) старта приложения
                    System.Reflection.MethodInfo loadAppEvents = typeof(AppforWorkSpace).GetMethod(NameAutorize_);
                    if (loadAppEvents != null)
                    {
                        // SystemConecto.ErorDebag("LoadAppEvents_" + IdApp, 2);
                        loadAppEvents.Invoke(ConectoWorkSpace_InW, new object[]{});
                    }


                    break;

            }

            // Завершения короткой авторизации если в систему не вошли раньше под полными правами.
            if (NameAutorize_ == "AdminOpcii" || SystemConecto.AutorizUser.TypeAutoriz == "Administrator")
            {
                SystemConecto.AutorizUser.TypeAutoriz = "Administrator";
            }
            else
            {
               // Это затирание авторизации пользователя я не понял зачем это. Тест исправления
                // SystemConecto.AutorizUser.LoginUserAutoriz_Back = SystemConecto.AutorizUser.LoginUserAutoriz;
               // SystemConecto.AutorizUser.LoginUserAutoriz = "";
            }

        }

        /// <summary>
        /// Завершения работы интерфейса пользователей, согласно прав доступа
        /// </summary>
        public static void UserInterficeClose()
        {

            // Первоначальное значение авторизации
            string NameAutorize_ = SystemConecto.AutorizUser.TypeAutoriz;
            SystemConecto.AutorizUser.TypeAutoriz = "";

            
            switch (NameAutorize_)
            {
                case "AdminOpcii":
                case "Administrator":

                    // Интерфейс Системного Администратора программы (Интегрированные учетные записи)LoginUserAutoriz_Back
                    if (SystemConecto.AutorizUser.LoginUserAutoriz == "Autorize_pass-admin-IT" || SystemConecto.AutorizUser.LoginUserAutoriz == "Autorize_Admin-Conecto")
                    {
                        // Очистка переменных авторизации ...
                        SystemConecto.AutorizUser.LoginUserAutoriz_Back = SystemConecto.AutorizUser.LoginUserAutoriz;
                        SystemConecto.AutorizUser.LoginUserAutoriz = "";
                        SystemConecto.AutorizUser.PaswdUserAutoriz = "";

                        // Основное окно
                        //MainWindow ConectoWorkSpace_InW = (MainWindow)Application.Current.MainWindow;
                        MainWindow ConectoWorkSpace_InW = AppStart.LinkMainWindow();
                        if (ConectoWorkSpace_InW == null)
                        {
                            return;
                        }
                        // Ссылка на объект
                        var uriSource = new Uri(@"/Conecto®%20WorkSpace;component/Images/admin_1b.png", UriKind.Relative);
                        ConectoWorkSpace_InW.AdminButIm_.Source = new BitmapImage(uriSource);
                            

                        // Доп панель в панели задач (проверка режима терминал)
                        if (SystemConecto.TerminalStatus)
                        {
                            ConectoWorkSpace_InW.ButCatalog.Visibility = Visibility.Hidden;
                            ConectoWorkSpace_InW.ButKeyboard.Visibility = Visibility.Hidden;
                            ConectoWorkSpace_InW.Note.Visibility = Visibility.Hidden;

                            HookSystemKeys.FunHook();// Запрещаем системные клавиши
                        }

                    }


                    break;
                    // Старая разработка
                    // case "B52FrontOffice7":case "B52BackOffice":
                default:

                    // Очистка переменных авторизации ...
                    SystemConecto.AutorizUser.LoginUserAutoriz_Back = SystemConecto.AutorizUser.LoginUserAutoriz;
                    SystemConecto.AutorizUser.LoginUserAutoriz = "";
                    SystemConecto.AutorizUser.PaswdUserAutoriz = "";
                    break;
                    
            }
             
           

        }
        //======================================================================== Не рабочее
        /// <summary>
        /// Администрирование пользователей права доступа
        /// </summary>
        private static void UserInterficeAdminUser()
        {
            // MessageBox.Show(SystemConecto.LoginUserAutoriz);
            // Интерфейс Системного Администратора программы (Интегрированные учетные записи)
            if (SystemConecto.AutorizUser.LoginUserAutoriz == "Autorize_pass-admin-IT" || SystemConecto.AutorizUser.LoginUserAutoriz == "Autorize_Admin-Conecto")
            {
                // Основное окно
                //MainWindow ConectoWorkSpace_InW = (MainWindow)Application.Current.MainWindow;
                MainWindow ConectoWorkSpace_InW = AppStart.LinkMainWindow();
                if (ConectoWorkSpace_InW == null)
                {
                    return;
                }
                // Ссылка на объект
                var uriSource = new Uri(@"/Conecto®%20WorkSpace;component/Images/admin_1.png", UriKind.Relative);
                ConectoWorkSpace_InW.AdminButIm_.Source = new BitmapImage(uriSource);
                ConectoWorkSpace_InW.ServerButIm_.Visibility = Visibility.Visible;

            }

        }

        #endregion



    }
}
