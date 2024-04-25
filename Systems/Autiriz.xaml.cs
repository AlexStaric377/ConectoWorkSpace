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
// --- Process 
using System.Diagnostics;
using System.ComponentModel; // Win32Exception
/// Многопоточность
using System.Threading;

namespace ConectoWorkSpace
{
    /// <summary>
    /// Логика взаимодействия для Window1.xaml
    /// </summary>
    public partial class Autiriz : Window
    {
        public string Login = "";   // Логин
        public string Passw = "";   // Пароль
        public int ActiveVvod = 1;
 


        /// <summary>
        /// 
        /// </summary>
        /// <param name="NameAutorize">Имя авторизации</param>
        public Autiriz(string NameAutorize_)
        {
            App.NameAutorize = NameAutorize_;

            InitializeComponent();
            SizeChan();

        }

        /// <summary>
        /// Отрисовка изменений размера
        /// </summary>
        public void SizeChan()
        {
            // 2. Отключение елементов рабочего стола
            //MainWindow ConectoWorkSpace_InW = (MainWindow)Application.Current.MainWindow;
            MainWindow ConectoWorkSpace_InW = AppStart.LinkMainWindow();
            if (ConectoWorkSpace_InW == null)
            {
                return;
            }

            // Ссылка на объект
            ConectoWorkSpace_InW.WorkSpacePanel.Visibility = Visibility.Hidden;


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

            // Скрытие елементов
            this.ClearVvodLogin.Visibility = Visibility.Hidden;
            this.fonVvodLogin.Visibility = Visibility.Hidden;
            
            this.TabSpace.Visibility = Visibility.Hidden;
            this.label_nopass.Visibility = Visibility.Hidden;
            this.label_VvediteI.Visibility = Visibility.Hidden;

            ClearSim();
            ClearSimLogin();

            // Тип авторизации - Полная авторизация с логином и паролем
            Login = SystemConecto.Autirize[1] == "1" ? "UserPanel" : "";   // Логин
            ActiveVvod = SystemConecto.Autirize[1] == "1" ? 2 : 1;
            FullUserAut(Login == "UserPanel" ? 1 : 0);
            
            // Включение дополнительных элементов
            AddElements();


        }


        #region События Click (Клик) функцилнальных клавиш рабочего стола

        private void BackClose__MouseLeftButtonDown_1(object sender, MouseButtonEventArgs e)
        {
            Backdelete_();
        }

        /// <summary>
        /// Удаление символа в поле ввода
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Backdelete_()
        {
            // Определить активное поле Login  или Passw
            if (ActiveVvod == 1)
            {
                var Pin = (Label)LogicalTreeHelper.FindLogicalNode(this, "SimL" + Login.Length.ToString());
                if (Pin != null)
                {
                    Pin.Visibility = Visibility.Hidden;
                    // Удаляем символ
                    if (Login.Length > 0)
                    {
                        Login = Login.Remove(Login.Length - 1, 1);
                    }

                    label_nopass.Visibility = Visibility.Hidden;
                }
            }
            else
            {
                var Pin = (Image)LogicalTreeHelper.FindLogicalNode(this, "Sim" + Passw.Length.ToString());
                if (Pin != null)
                {
                    Pin.Visibility = Visibility.Hidden;
                    // Удаляем символ
                    if (Passw.Length > 0)
                    {
                        Passw = Passw.Remove(Passw.Length - 1, 1);
                    }
                    //MessageBox.Show(Passw);
                    label_nopass.Visibility = Visibility.Hidden;
                }
            }
            
          

        }


        private void Ok__Click_MouseLeftButtonDown_1(object sender, MouseButtonEventArgs e)
        {
            this.label_nopass.Visibility = Visibility.Hidden;
            Ok_Autoriz();

        }
        /// <summary>
        /// Проверка пароля и логина
        /// </summary>
        private void Ok_Autoriz(){
            // Проверка пароля
            MainWindow.NumberInput++;
            if (MainWindow.NumberInput > MainWindow.QuantityInput)
            {
                label_nopass.Text = "Вы не авторизированы, Вход в Администрирование заблокирован.";
                this.label_nopass.Visibility = Visibility.Visible;
                return;
            }
            int MessageError = SystemConecto.AutorizUser.Autoriz(Login, Passw);
            if (MessageError==0)
            {
                this.CloseForms();
                // Изменяем интерфейс
                SystemConectoInterfice.UserInterfice(App.NameAutorize);
            }
            else
            {
                switch(MessageError){
                    case 8126:
                        // Нет соединения
                        label_nopass.Text = "Нет соединения с сервером." + Environment.NewLine + "Повторите через 20 секунд...";
                        this.label_nopass.Visibility = Visibility.Visible;
                        break;

                    default:
                        ClearSimLogin();
                        ClearSim();
                        label_nopass.Text = MessageError == 8127 ? "Параметры соединения с сервером" + Environment.NewLine + "не верно сформированны" + (SystemConecto.DebagApp? " IDEr:"+ MessageError : "")  : "Неверный пароль";
                        this.label_nopass.Visibility = Visibility.Visible;
                        //this.label_nopass.Visible = true;
                        break;
                }
                
              
               
            }

        }


        private void ClearVvod_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            TabPolevvoda_(0, 2);
            ClearSim();
            //this.label_nopass.Visible = false;
        }
        private void Close__MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.CloseForms();
        }

        public static void AuturizAdminStopTime(object ThreadObj)
        {
            Thread.Sleep(50000);
            MainWindow.NumberInput = 0;
        }


            //------------------------------------- Методы (функции)
            /// <summary>
            /// Ввод символов
            /// </summary>
            /// <param name="Int_"></param>
            private void NewSim(string Int_)
        {
            // Определить активное поле Passw или Login
            if (MainWindow.NumberInput > MainWindow.QuantityInput)
            {
                AppStart.RenderInfo Arguments01 = new AppStart.RenderInfo() { };
                Arguments01.argument1 = "2";
                Arguments01.argument2 = "";
                Thread thStartTimer01 = new Thread(AuturizAdminStopTime);
                thStartTimer01.SetApartmentState(ApartmentState.STA);
                thStartTimer01.IsBackground = true; // Фоновый поток
                thStartTimer01.Start(Arguments01);
                
                this.CloseForms();
                return;
            }
            if (ActiveVvod == 1)
            {
                //Логин 
                // Не более 8 знаков
                if (Login.Length < 9)
                {
                    this.label_nopass.Visibility = Visibility.Hidden;
                    Login = Login + Int_;
                    var Pin = (Label)LogicalTreeHelper.FindLogicalNode(this, "SimL" + Login.Length.ToString());
                    Pin.Content = Int_;
                    Pin.Visibility = Visibility.Visible;

                }
                else
                {
                    Login = Login + Int_;
                    // игнорировать вывод
                    //label_nopass.Text = "введены все символы";
                    // label_nopass.Visibility = Visibility.Visible;
                }
            }
            else
            {
                // Пароль
                // Не более 8 знаков
                if (Passw.Length < 9)
                {
                    this.label_nopass.Visibility = Visibility.Hidden;
                    Passw = Passw + Int_;
                    var Pin = (Image)LogicalTreeHelper.FindLogicalNode(this, "Sim" + Passw.Length.ToString());
                    Pin.Visibility = Visibility.Visible;

                }
                else
                {
                    Passw = Passw + Int_;
                    //label_nopass.Text = "введены все символы";
                    //label_nopass.Visibility = Visibility.Visible;
                }
            }
            
           
        }
        /// <summary>
        /// Чистка символов Пароля
        /// </summary>
        private void ClearSim()
        {

            label_nopass.Visibility = Visibility.Hidden;
            Passw = "";
            this.Sim1.Visibility = Visibility.Hidden;
            this.Sim2.Visibility = Visibility.Hidden;
            this.Sim3.Visibility = Visibility.Hidden;
            this.Sim4.Visibility = Visibility.Hidden;
            this.Sim5.Visibility = Visibility.Hidden;
            this.Sim6.Visibility = Visibility.Hidden;
            this.Sim7.Visibility = Visibility.Hidden;
            this.Sim8.Visibility = Visibility.Hidden;
            this.Sim9.Visibility = Visibility.Hidden;
        }
        /// <summary>
        /// Чистка символов Логина
        /// </summary>
        private void ClearSimLogin()
        {
            Login =  Login == "UserPanel" ? "UserPanel" : "";
            this.SimL1.Visibility = Visibility.Hidden;
            this.SimL2.Visibility = Visibility.Hidden;
            this.SimL3.Visibility = Visibility.Hidden;
            this.SimL4.Visibility = Visibility.Hidden;
            this.SimL5.Visibility = Visibility.Hidden;
            this.SimL6.Visibility = Visibility.Hidden;
            this.SimL7.Visibility = Visibility.Hidden;
            this.SimL8.Visibility = Visibility.Hidden;
            this.SimL9.Visibility = Visibility.Hidden;
        }
        /// <summary>
        /// Закрытие форм
        /// </summary>
        private void CloseForms()
        {
            // Очистить масив авторизации
             for (int i = 0; i < SystemConecto.Autirize.Count(); i++)
             {
                 SystemConecto.Autirize[i] = "";
             }
          
            this.Visibility = Visibility.Hidden;

            
            //Owner = null;

            Window Window = SystemConecto.ListWindowMain("WaitFonW");
            if (Window != null)
            {

                Window.Hide();
                Grid FonWindow = (Grid)LogicalTreeHelper.FindLogicalNode(Window, "WindGrid");
                FonWindow.Opacity = 0.45;
            }


            // 2. Включение елементов рабочего стола
            //MainWindow ConectoWorkSpace_InW = (MainWindow)Application.Current.MainWindow;

            MainWindow ConectoWorkSpace_InW = AppStart.LinkMainWindow();
            if (ConectoWorkSpace_InW == null)
            {
                return;
            }

            // Ссылка на объект
            ConectoWorkSpace_InW.WorkSpacePanel.Visibility = Visibility.Visible;

            // Закрыть клавиатуру если терминальный режим
            if (Login != "UserPanel" && SystemConecto.TerminalStatus)
            {
                ProcessConecto.TerminateProcessSystem("osk");
            }

            this.Owner.Focus();
            this.Close();
        }


        #region Клик на поле ввода пароля и логина мышью переклчение TAB

        private void fonVvodPasswd_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
 
            TabPolevvoda_(0, 2);

        }


        private void fonVvodLogin_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            TabPolevvoda_(0, 1);
        }


         #region Поле ввода логина
          // =========================== Поле ввода логина
        private void SimL1_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            TabPolevvoda_(0, 1);
        }

        private void SimL2_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            TabPolevvoda_(0, 1);
        }

        private void SimL3_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            TabPolevvoda_(0, 1);
        }

        private void SimL4_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            TabPolevvoda_(0, 1);
        }

        private void SimL5_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            TabPolevvoda_(0, 1);
        }

        private void SimL6_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            TabPolevvoda_(0, 1);
        }

        private void SimL7_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            TabPolevvoda_(0, 1);
        }

        private void SimL8_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            TabPolevvoda_(0, 1);
        }

        private void SimL9_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            TabPolevvoda_(0, 1);
        }

        #endregion

        #endregion

        #region переход между полями

        /// <summary>
        /// TypeVvod - тип ввода 0 - мишь; 1 - клавиатура
        /// PoleVvoda - порядковый номер ввода
        /// </summary>
        /// <param name="TypeVvod"></param>
        private void TabPolevvoda_(int TypeVvod = 1, int PoleVvoda = 1)
        {
            // SystemConecto.ErorDebag("Click", 2);
            // Переключать фокус ввода
            if (Login != "UserPanel")
            {
                // Проверка ссылки самого на себя
                if (TypeVvod == 1 || (TypeVvod == 0 && PoleVvoda != ActiveVvod))
                {
                    ActiveVvod = ActiveVvod == 1 ? 2 : 1;
                    this.TabSpace.Margin = new Thickness(this.TabSpace.Margin.Left, ActiveVvod == 1 ? this.fonVvodLogin.Margin.Top-5 : this.fonVvodPasswd.Margin.Top-5, 0, 0);
                }
            }

        }


        // =========================== Поле ввода пароля
        private void Sim1_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
 
            TabPolevvoda_(0, 2);
        }

        private void Sim2_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            TabPolevvoda_(0, 2);
        }

        private void Sim3_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            TabPolevvoda_(0, 2);
        }

        private void Sim4_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            TabPolevvoda_(0, 2);
        }

        private void Sim5_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            TabPolevvoda_(0, 2);
        }

        private void Sim6_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            TabPolevvoda_(0, 2);
        }

        private void Sim7_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            TabPolevvoda_(0, 2);
        }

        private void Sim8_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            TabPolevvoda_(0, 2);
        }

        private void Sim9_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            TabPolevvoda_(0, 2);
        }

        #endregion



        #region Нажатие цифровых клавиш

        private void But0_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            NewSim("0");
        }

        private void But1_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            NewSim("1");
        }

        private void But2_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            NewSim("2");
        }

        private void But3_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            NewSim("3");
        }

        private void But4_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            NewSim("4");
        }

        private void But5_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            NewSim("5");
        }

        private void But6_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            NewSim("6");
        }

        private void But7_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            NewSim("7");
        }
        private void But8_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            NewSim("8");
        }
        private void But9_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            NewSim("9");
        }
        #endregion



        #endregion



        #region Изображения как клавиши - Визуализация интерфейса
        private void Ok__Click_MouseLeave(object sender, MouseEventArgs e)
        {
            var uriSource = new Uri(@"/Conecto®%20WorkSpace;component/Images/yes1.png", UriKind.Relative);
            this.Ok__Click.Source = new BitmapImage(uriSource);
        }

        private void Ok__Click_MouseMove(object sender, MouseEventArgs e)
        {
            var uriSource = new Uri(@"/Conecto®%20WorkSpace;component/Images/yes2.png", UriKind.Relative);
            this.Ok__Click.Source = new BitmapImage(uriSource);
        }
        private void BackClose__MouseLeave(object sender, MouseEventArgs e)
        {
            var uriSource = new Uri(@"/Conecto®%20WorkSpace;component/Images/beck1.png", UriKind.Relative);
            this.BackClose_.Source = new BitmapImage(uriSource);
        }

        private void BackClose__MouseMove(object sender, MouseEventArgs e)
        {
            var uriSource = new Uri(@"/Conecto®%20WorkSpace;component/Images/beck2.png", UriKind.Relative);
            this.BackClose_.Source = new BitmapImage(uriSource);
        }

        #region Клавиши цифр
        private void But0_MouseLeave(object sender, MouseEventArgs e)
        {
            var uriSource = new Uri(@"/Conecto®%20WorkSpace;component/Images/knopka0.png", UriKind.Relative);
            this.But0.Source = new BitmapImage(uriSource);
        }
        private void But0_MouseMove(object sender, MouseEventArgs e)
        {
            var uriSource = new Uri(@"/Conecto®%20WorkSpace;component/Images/knopka0_2.png", UriKind.Relative);
            this.But0.Source = new BitmapImage(uriSource);
        }

        private void But4_MouseLeave(object sender, MouseEventArgs e)
        {
            var uriSource = new Uri(@"/Conecto®%20WorkSpace;component/Images/knopka4.png", UriKind.Relative);
            this.But4.Source = new BitmapImage(uriSource);
        }

        private void But4_MouseMove(object sender, MouseEventArgs e)
        {
            var uriSource = new Uri(@"/Conecto®%20WorkSpace;component/Images/knopka4_2.png", UriKind.Relative);
            this.But4.Source = new BitmapImage(uriSource);
        }

        private void But1_MouseLeave(object sender, MouseEventArgs e)
        {
            var uriSource = new Uri(@"/Conecto®%20WorkSpace;component/Images/knopka1.png", UriKind.Relative);
            this.But1.Source = new BitmapImage(uriSource);
        }

        private void But1_MouseMove(object sender, MouseEventArgs e)
        {
            var uriSource = new Uri(@"/Conecto®%20WorkSpace;component/Images/knopka1_2.png", UriKind.Relative);
            this.But1.Source = new BitmapImage(uriSource);
        }

        private void But2_MouseLeave(object sender, MouseEventArgs e)
        {
            var uriSource = new Uri(@"/Conecto®%20WorkSpace;component/Images/knopka2.png", UriKind.Relative);
            this.But2.Source = new BitmapImage(uriSource);
        }

        private void But2_MouseMove(object sender, MouseEventArgs e)
        {
            var uriSource = new Uri(@"/Conecto®%20WorkSpace;component/Images/knopka2_2.png", UriKind.Relative);
            this.But2.Source = new BitmapImage(uriSource);
        }

        private void But3_MouseLeave(object sender, MouseEventArgs e)
        {
            var uriSource = new Uri(@"/Conecto®%20WorkSpace;component/Images/knopka3.png", UriKind.Relative);
            this.But3.Source = new BitmapImage(uriSource);
        }

        private void But3_MouseMove(object sender, MouseEventArgs e)
        {
            var uriSource = new Uri(@"/Conecto®%20WorkSpace;component/Images/knopka3_2.png", UriKind.Relative);
            this.But3.Source = new BitmapImage(uriSource);
        }

        

        private void But5_MouseLeave(object sender, MouseEventArgs e)
        {
            var uriSource = new Uri(@"/Conecto®%20WorkSpace;component/Images/knopka5.png", UriKind.Relative);
            this.But5.Source = new BitmapImage(uriSource);
        }
        private void image5_MouseMove(object sender, MouseEventArgs e)
        {
            var uriSource = new Uri(@"/Conecto®%20WorkSpace;component/Images/knopka5_2.png", UriKind.Relative);
            this.But5.Source = new BitmapImage(uriSource);
        }
        private void But6_MouseLeave(object sender, MouseEventArgs e)
        {
            var uriSource = new Uri(@"/Conecto®%20WorkSpace;component/Images/knopka6.png", UriKind.Relative);
            this.But6.Source = new BitmapImage(uriSource);
        }

        private void But6_MouseMove(object sender, MouseEventArgs e)
        {
            var uriSource = new Uri(@"/Conecto®%20WorkSpace;component/Images/knopka6_2.png", UriKind.Relative);
            this.But6.Source = new BitmapImage(uriSource);
        }

        private void But7_MouseLeave(object sender, MouseEventArgs e)
        {
            var uriSource = new Uri(@"/Conecto®%20WorkSpace;component/Images/knopka7.png", UriKind.Relative);
            this.But7.Source = new BitmapImage(uriSource);
        }

        private void But7_MouseMove(object sender, MouseEventArgs e)
        {
            var uriSource = new Uri(@"/Conecto®%20WorkSpace;component/Images/knopka7_2.png", UriKind.Relative);
            this.But7.Source = new BitmapImage(uriSource);
        }

        private void But8_MouseLeave(object sender, MouseEventArgs e)
        {
            var uriSource = new Uri(@"/Conecto®%20WorkSpace;component/Images/knopka8.png", UriKind.Relative);
            this.But8.Source = new BitmapImage(uriSource);
        }

        private void But8_MouseMove(object sender, MouseEventArgs e)
        {
            var uriSource = new Uri(@"/Conecto®%20WorkSpace;component/Images/knopka8_2.png", UriKind.Relative);
            this.But8.Source = new BitmapImage(uriSource);
        }

        private void But9_MouseLeave(object sender, MouseEventArgs e)
        {
            var uriSource = new Uri(@"/Conecto®%20WorkSpace;component/Images/knopka9.png", UriKind.Relative);
            this.But9.Source = new BitmapImage(uriSource);
        }

        private void But9_MouseMove(object sender, MouseEventArgs e)
        {
            var uriSource = new Uri(@"/Conecto®%20WorkSpace;component/Images/knopka9_2.png", UriKind.Relative);
            this.But9.Source = new BitmapImage(uriSource);
        }

        #endregion

        #endregion


        #region Обработка событий любой клавиатуры
        private void ConectoW_KeyDown(object sender, KeyEventArgs e)
        {
            
            // Да завершить
            if (e.Key == Key.Return)
            {
               // Это работает
                // Если я еще не ввел логин и пароль то ждем
                if (Passw.Length > 0 && Login.Length > 0)
                {
                    Ok_Autoriz();
                }
                else
                {
                    TabPolevvoda_();
                   
                }
            }
            else
            {

                // Нет отказаться
                if (e.Key == Key.Escape)
                {
                    CloseForms();

                }

            }

            bool CAPSLockSet = Win32.GetCapsLockState();

            switch (e.Key)
            {
                
                #region Цифровые
                // Ввод цыфр в поля
                case (Key.D0):
                    // Проверка ввода поля
                    NewSim("0");

                    break;
                case (Key.D1):
                    // Проверка ввода поля
                    NewSim("1");

                    break;
                case (Key.D2):
                    // Проверка ввода поля
                    NewSim("2");

                    break;
                case (Key.D3):
                    // Проверка ввода поля
                    NewSim("3");

                    break;
                case (Key.D4):
                    // Проверка ввода поля
                    NewSim("4");

                    break;
                case (Key.D5):
                    // Проверка ввода поля
                    NewSim("5");

                    break;
                case (Key.D6):
                    // Проверка ввода поля
                    NewSim("6");

                    break;
                case (Key.D7):
                    // Проверка ввода поля
                    NewSim("7");

                    break;
                case (Key.D8):
                    // Проверка ввода поля
                    NewSim("8");

                    break;
                case (Key.D9):
                    // Проверка ввода поля
                    NewSim("9");

                    break;
                case (Key.NumPad0):
                    // Проверка ввода поля
                    NewSim("0");

                    break;
                case (Key.NumPad1):
                    // Проверка ввода поля
                    NewSim("1");

                    break;
                case (Key.NumPad2):
                    // Проверка ввода поля
                    NewSim("2");

                    break;
                case (Key.NumPad3):
                    // Проверка ввода поля
                    NewSim("3");

                    break;
                case (Key.NumPad4):
                    // Проверка ввода поля
                    NewSim("4");

                    break;
                case (Key.NumPad5):
                    // Проверка ввода поля
                    NewSim("5");

                    break;
                case (Key.NumPad6):
                    // Проверка ввода поля
                    NewSim("6");

                    break;
                case (Key.NumPad7):
                    // Проверка ввода поля
                    NewSim("7");

                    break;
                case (Key.NumPad8):
                    // Проверка ввода поля
                    NewSim("8");

                    break;
                case (Key.NumPad9):
                    // Проверка ввода поля
                    NewSim("9");

                    break;
                #endregion

                #region Функциональные
                case (Key.Tab):
                    TabPolevvoda_();

                    break;
                case (Key.Back):
                    // Проверка ввода поля
                    Backdelete_();

                    break;

                case (Key.Delete):
                    // Проверка ввода поля
                    if (Login != "UserPanel")
                    {
                        if (ActiveVvod == 1)
                        {
                            ClearSimLogin();
                        }
                        else
                        {
                            ClearSim();
                        }
                    }
                    else
                    {
                        ClearSim();
                    }
                    break;
                #endregion

                #region Буквенные
                case (Key.A):
                    
                    NewSim( CAPSLockSet ? "A" : "a");
                    break;
                //case (Key.C):
                //    //var KeycodeToChar_ = TextPasteWindow.KeycodeToChar(e.Key);
                //    //NewSim(CAPSLockSet ? "C" : "c");
                //    NewSim(TextPasteWindow.KeycodeToChar(e.Key));
                //    break;
                //case (Key.B):

                //    NewSim(CAPSLockSet ? "B" : "b"); 

                //    break;
                #endregion


                // Другие клавиши ...
                default:
                    // Проверяем введенный символ реагируем на него для отслеживания начало ввода со считователя
                    var KeycodeToChar = TextPasteWindow.KeycodeToChar(e.Key);
                    if (KeycodeToChar.Length == 1)
                    {
                        //SystemConecto.ErorDebag(CAPSLockSet.ToString(), 2);
                        //if (CAPSLockSet) { var NewString = Char.ToLower(Convert.ToChar(KeycodeToChar)); }
                        if (!CAPSLockSet) { KeycodeToChar = KeycodeToChar.ToLower(); }
                        NewSim(KeycodeToChar);
                    }
                    break;
                
              
            }

           // Отладка
           // MessageBox.Show(e.Key.ToString());

        }
        #endregion



        //Перерисовываем при нажатии на клавишу полная авторизация
        private void FullUserAut(int Type=0)
        {
            //Login = Type == 0 ? "" : "UserPanel";
            //Перерисовываем
            this.label_nopass.Margin = new Thickness(this.label_nopass.Margin.Left, this.label_nopass.Margin.Top + (Type == 0 ? 16 : -16), 0, 0);
            this.fonVvodPasswd.Margin = new Thickness(this.fonVvodPasswd.Margin.Left, this.fonVvodPasswd.Margin.Top + (Type == 0 ? 25 : -25), 0, 0);
            this.Sim1.Margin = new Thickness(this.Sim1.Margin.Left, this.Sim1.Margin.Top + (Type == 0 ? 25 : -25), 0, 0);
            this.Sim2.Margin = new Thickness(this.Sim2.Margin.Left, this.Sim2.Margin.Top + (Type == 0 ? 25 : -25), 0, 0);
            this.Sim3.Margin = new Thickness(this.Sim3.Margin.Left, this.Sim3.Margin.Top + (Type == 0 ? 25 : -25), 0, 0);
            this.Sim4.Margin = new Thickness(this.Sim4.Margin.Left, this.Sim4.Margin.Top + (Type == 0 ? 25 : -25), 0, 0);
            this.Sim5.Margin = new Thickness(this.Sim5.Margin.Left, this.Sim5.Margin.Top + (Type == 0 ? 25 : -25), 0, 0);
            this.Sim6.Margin = new Thickness(this.Sim6.Margin.Left, this.Sim6.Margin.Top + (Type == 0 ? 25 : -25), 0, 0);
            this.Sim7.Margin = new Thickness(this.Sim7.Margin.Left, this.Sim7.Margin.Top + (Type == 0 ? 25 : -25), 0, 0);
            this.Sim8.Margin = new Thickness(this.Sim8.Margin.Left, this.Sim8.Margin.Top + (Type == 0 ? 25 : -25), 0, 0);
            this.Sim9.Margin = new Thickness(this.Sim9.Margin.Left, this.Sim9.Margin.Top + (Type == 0 ? 25 : -25), 0, 0);
            this.ClearVvod.Margin = new Thickness(this.ClearVvod.Margin.Left, this.ClearVvod.Margin.Top + (Type == 0 ? 25 : -25), 0, 0);
            this.label_Vvedite.Margin = new Thickness(this.label_Vvedite.Margin.Left, this.label_Vvedite.Margin.Top + (Type == 0 ? 60 : -60), 0, 0);

            this.BackClose_.Margin = new Thickness(this.BackClose_.Margin.Left, this.BackClose_.Margin.Top + (Type == 0 ? 25 : -25), 0, 0);
            this.Ok__Click.Margin = new Thickness(this.Ok__Click.Margin.Left, this.Ok__Click.Margin.Top + (Type == 0 ? 25 : -25), 0, 0);
            this.ICOButtAut.Margin = new Thickness(this.ICOButtAut.Margin.Left, this.ICOButtAut.Margin.Top + (Type == 0 ? 25 : -25), 0, 0);

            this.label_VvediteI.Margin = new Thickness(this.label_VvediteI.Margin.Left, this.label_VvediteI.Margin.Top + (Type == 0 ? 65 : -65), 0, 0);
            this.label_VvediteI.Visibility = (Type == 0 ? Visibility.Visible : Visibility.Hidden);
            this.fonVvodLogin.Margin = new Thickness(this.fonVvodLogin.Margin.Left, this.fonVvodLogin.Margin.Top + (Type == 0 ? 65 : -65), 0, 0);
            this.fonVvodLogin.Visibility = (Type == 0 ? Visibility.Visible : Visibility.Hidden);
            this.SimL1.Margin = new Thickness(this.SimL1.Margin.Left, this.SimL1.Margin.Top + (Type == 0 ? 65 : -65), 0, 0);
            //this.SimL1.Visibility = (Type == 0 ? Visibility.Visible : Visibility.Hidden);
            this.SimL2.Margin = new Thickness(this.SimL2.Margin.Left, this.SimL2.Margin.Top + (Type == 0 ? 65 : -65), 0, 0);
           // this.SimL2.Visibility = (Type == 0 ? Visibility.Visible : Visibility.Hidden);
            this.SimL3.Margin = new Thickness(this.SimL3.Margin.Left, this.SimL3.Margin.Top + (Type == 0 ? 65 : -65), 0, 0);
           // this.SimL3.Visibility = (Type == 0 ? Visibility.Visible : Visibility.Hidden);
            this.SimL4.Margin = new Thickness(this.SimL4.Margin.Left, this.SimL4.Margin.Top + (Type == 0 ? 65 : -65), 0, 0);
           // this.SimL4.Visibility = (Type == 0 ? Visibility.Visible : Visibility.Hidden);
            this.SimL5.Margin = new Thickness(this.SimL5.Margin.Left, this.SimL5.Margin.Top + (Type == 0 ? 65 : -65), 0, 0);
           // this.SimL5.Visibility = (Type == 0 ? Visibility.Visible : Visibility.Hidden);
            this.SimL6.Margin = new Thickness(this.SimL6.Margin.Left, this.SimL6.Margin.Top + (Type == 0 ? 65 : -65), 0, 0);
           // this.SimL6.Visibility = (Type == 0 ? Visibility.Visible : Visibility.Hidden);
            this.SimL7.Margin = new Thickness(this.SimL7.Margin.Left, this.SimL7.Margin.Top + (Type == 0 ? 65 : -65), 0, 0);
           // this.SimL7.Visibility = (Type == 0 ? Visibility.Visible : Visibility.Hidden);
            this.SimL8.Margin = new Thickness(this.SimL8.Margin.Left, this.SimL8.Margin.Top + (Type == 0 ? 65 : -65), 0, 0);
          //  this.SimL8.Visibility = (Type == 0 ? Visibility.Visible : Visibility.Hidden);
            this.SimL9.Margin = new Thickness(this.SimL9.Margin.Left, this.SimL9.Margin.Top + (Type == 0 ? 65 : -65), 0, 0);
          //  this.SimL9.Visibility = (Type == 0 ? Visibility.Visible : Visibility.Hidden);
            this.ClearVvodLogin.Margin = new Thickness(this.ClearVvodLogin.Margin.Left, this.ClearVvodLogin.Margin.Top + (Type == 0 ? 65 : -65), 0, 0);
            this.ClearVvodLogin.Visibility = (Type == 0 ? Visibility.Visible : Visibility.Hidden);
            this.TabSpace.Margin = new Thickness(this.TabSpace.Margin.Left, this.TabSpace.Margin.Top + (Type == 0 ? 65 : -65), 0, 0);
            this.TabSpace.Visibility = (Type == 0 ? Visibility.Visible : Visibility.Hidden);
            
            // Отключить большие клавиши
            if (Type == 0 )
            {
                this.But0.Visibility = Visibility.Collapsed;
                this.But1.Visibility = Visibility.Collapsed;
                this.But2.Visibility = Visibility.Collapsed;
                this.But3.Visibility = Visibility.Collapsed;
                this.But4.Visibility = Visibility.Collapsed;
                this.But5.Visibility = Visibility.Collapsed;
                this.But6.Visibility = Visibility.Collapsed;
                this.But7.Visibility = Visibility.Collapsed;
                this.But8.Visibility = Visibility.Collapsed;
                this.But9.Visibility = Visibility.Collapsed;

                if (SystemConecto.TerminalStatus)
                {

                    // Включить клавиатуру если терминальный режим ...
                    if (!AppStart.SetFocusWindow("osk"))
                    {

                        Process procCommand = Process.Start("osk");
                        // Ошибка сWaitForInputIdle - отсутствует графический интерфейс(запускает другой процесс, ищем название)
                        // procCommand.WaitForInputIdle();

                        // Доп функция определения появления окна в процессах Windows 
                        // (некоторые зади после запуска через Start меняют свои свойства как hControl так и тип отображения)
                        TextPasteWindow.WaitStartWindowApp("osk");
                    }

                    // Устанавливаем нужные размеры - не работает разработка
                    if (TextPasteWindow.SetFocusWindow("osk"))
                    {
                        // Отладка
                        //SystemConecto.ErorDebag("Test Key /" + HWND.TOPMOST.ToString() + "/" + TextPasteWindow.hControl.ToString() + "/", 1);
                        int Ypix = Convert.ToInt32(SystemConecto.WorkAreaDisplayDefault[2] / 2) - 400;
                        int Xpix = Convert.ToInt32(SystemConecto.WorkAreaDisplayDefault[3] - 240);
                        if (!TextPasteWindow.SetWindowPos(TextPasteWindow.hControl, HWND.TOPMOST, Ypix, Xpix, 800, 240, SWP.NOSIZE)) //
                        {
                            SystemConecto.ErorDebag("Ошибка работы функции: SetWindowPos");
                        }//SWP.NOSIZE | SWP.NOMOVE
                    }
                }
                // Установить нужную раскладку
                // Определить как параметры
                uint KLF_ACTIVATE = 0x00000001; // параметр уакзывает если раскладки нет то ее загрузить в ОС
                TextPasteWindow.LoadKeyboardLayout("00000409", KLF_ACTIVATE);
            }
        }



        private void ClearVvodLogin_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            TabPolevvoda_(0, 1);

            ClearSimLogin();
        }


        #region Включение дополнительных элементов
        private void AddElements()
        {
            // Изображение спарава
            if (SystemConecto.Autirize[2] == "")
            {
                this.ICOButtAut.Visibility = Visibility.Collapsed;
            }
            else
            {
                var uriSource = new Uri(SystemConecto.Autirize[2], UriKind.Relative);
                this.ICOButtAut_.Source = new BitmapImage(uriSource);
            }
            // Текст под изображением спарава
            if (SystemConecto.Autirize[4] == "")
            {
                this.textPic.Visibility = Visibility.Collapsed;
            }
            else
            {
                this.textPic.Text = SystemConecto.Autirize[4];
                // Регулировка высоты стартовая высота 140 x 140
                this.ICOButtAut.Margin = new Thickness(this.ICOButtAut.Margin.Left, this.ICOButtAut.Margin.Top +  (70-this.ICOButtAut.Height/2), 0, 0);
               // this.textPic.Margin = new Thickness(this.textPic.Margin.Left, this.textPic.Margin.Top, 0, 0);
            }
            // Изображение Сверху
            if (SystemConecto.Autirize[3] == "")
            {
                this.LogoB52.Visibility = Visibility.Collapsed;
            }
            else
            {
                var uriSource = new Uri(SystemConecto.Autirize[3], UriKind.Relative);
                this.LogoB52.Source = new BitmapImage(uriSource);
            }
            // Название окна
            this.Site.Visibility = Visibility.Collapsed;
            this.textBlock1.Visibility = Visibility.Collapsed;
            this.textBlock2.Visibility = Visibility.Collapsed;
            this.textBlock3.Visibility = Visibility.Collapsed;
            
            switch(SystemConecto.Autirize[0]){
                case "BackOfficeB52":
                    this.Site.Visibility = Visibility.Visible;
                    this.textBlock1.Visibility = Visibility.Visible;
                    this.textBlock2.Visibility = Visibility.Visible;
                    this.textBlock3.Visibility = Visibility.Visible;
                    break;
                case "B52FrontOffice7":
                    this.Site.Visibility = Visibility.Visible;
                    this.textBlock1.Visibility = Visibility.Visible;
                    this.textBlock2.Visibility = Visibility.Visible;
                    this.textBlock3.Visibility = Visibility.Visible;
                    break;
                default:
                    break;
            }
        }
        #endregion


    }
}
