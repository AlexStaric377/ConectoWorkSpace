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
/// Многопоточность
using System.Threading;
using System.Windows.Threading;
// Анимированный гиф
using WpfAnimatedGif;

using mshtml;


namespace ConectoWorkSpace
{
    /// <summary>
    /// Логика взаимодействия для SpecPred.xaml
    /// </summary>
    public partial class SpecPred : Window
    {

        WebBrowserOverlay wbo = null; //WebBrowserOverlay

        public SpecPred()
        {
            InitializeComponent();

            ResolutionDisplay();

          
            WaitMessageShow("Соединение с сервером conecto.ua", 11);

            // Для браузера при старте навигации
            // System.Windows.Deployment.Current.Dispatcher.BeginInvoke(startNavigate);

            // Размер браузера _webBrowserPlacementTarget.Height = _webBrowserPlacementTarget.Width = 
            //SpecPredlog.Height = this.Height - 100 - 96 - 69;
            //SpecPredlog.Width = this.WinGrid.Width - 100 - 20;
            _webBrowserPlacementTarget.Height = this.Height - 100 - 96 - 69;
            _webBrowserPlacementTarget.Width = this.WinGrid.Width - 100 - 20;

            // Проверка доступа к интернету с помощью кеша
            //if (SystemConecto.TickMemory_a[1] == 1) // ConnectionAvailable("conecto.ua")) SystemConecto.ConnectionAvailable_ICMP("178.20.156.66")
            //{
                wbo = new WebBrowserOverlay(_webBrowserPlacementTarget, this.Height - 100 - 96 - 69, this.WinGrid.Width - 100 - 20);

                RenderInfo ri = new RenderInfo() { Html = "http://conecto.ua/market/index.php?information_id=8" };  // , Result = null
                //RenderInfo ri = new RenderInfo() { Html = "http://conecto.ua/market/" };




                // System.Threading.Thread th = new System.Threading.Thread(new System.Threading.ParameterizedThreadStart(Renderer));
                Thread thStartWEB = new Thread(Renderer);
                thStartWEB.SetApartmentState(ApartmentState.STA);
                //thStartWEB.IsBackground = true; // Фоновый поток
                thStartWEB.Start(ri);
                //Ждем окончания загрузки данных в др. потоке
           // }
            //else
            //{
             //   WaitMessageShow("Отсутствует соединение с интернет сервером conecto.ua");
           // }
            

          

        }

        #region Разрешение экрана
        public void ResolutionDisplay()
        {
            // 3. Установка размеров окна для того чтобы игнорировать нажатие клавиши раскрыть на весь экран
            // Рабочего стола ([0] - Top; [1] - Left; [2] - Width; [3] - Height; [4] - Right;

            this.Top = SystemConecto.WorkAreaDisplayDefault[0] + 10;
            this.Left = SystemConecto.WorkAreaDisplayDefault[1] + 10;
            this.WinGrid.Width = this.Width = SystemConecto.WorkAreaDisplayDefault[2] - (277 + 5 + 10);
            this.Height = SystemConecto.WorkAreaDisplayDefault[3] - 45;
            this.Okno.Height = this.Height - 100;

            // Вікна які знаходятся у цьоу вікні
            WiknoConecto.Height = this.Okno.Height;

            // Отцентрировать сообщение
            Message1.Margin = new Thickness(0, 0, 0, 0);

        }
        #endregion

        // Структура параметров потока обращения к веб серверу
        struct RenderInfo
        {
            public string Html { get; set; }
            // public Bitmap Result { get; set; }
        }

        //public VideoUroki()
        //{
        //    InitializeComponent();

        //    RenderInfo ri = new RenderInfo() { Html = "http:conecto.ua", Result = null };
        //    System.Threading.Thread th = new System.Threading.Thread(new System.Threading.ParameterizedThreadStart(Renderer));
        //    th.SetApartmentState(System.Threading.ApartmentState.STA);
        //    th.Start(ri);
        //    //Ждем окончания
        //    if (!th.Join(55000))
        //    {
        //        //Пишем в лог, о проблемах. Возвращается null
        //        //LogWriter(ex, QRPRErrorLevel.ErrorM2); 
        //    }
        //    //Thread t = new Thread(WriteY);
        //    //t.Start(); 

        //    // ----------------------------------------------------- Пример создания объекта програмным путем
        //    //var textBox2 = new System.Windows.Forms.TextBox();
        //    //Controls.Add(textBox2);
        //    //this.SuspendLayout();

        //    //// textBox1
        //    //// 
        //    //this.textBox1.Location = new System.Drawing.Point(631, 389);
        //    //this.textBox1.Name = "textBox2";
        //    //this.textBox1.Size = new System.Drawing.Size(100, 20);
        //    //this.textBox1.TabIndex = 0;

        //    //this.PerformLayout();

        //}

        //private void VideoUroki_Load(object sender, EventArgs e)
        //{


        //}

        private void Renderer(object obj)
        {
            RenderInfo ri = (RenderInfo)obj;
            try
            {


                // Уже создан
                //WebBrowser webBrowser = new WebBrowser();
                //SpecPredlog
                System.Windows.Application.Current.Dispatcher.Invoke(new Action(delegate()
                 {
                   // var Window_ = SystemConecto.ListWindowMain("SpecPred_");
                     
                     wbo.Visibility = Visibility.Collapsed;
                    
                     Uri ui = new Uri(ri.Html, UriKind.RelativeOrAbsolute);
                     ////this.SpecPredlog.Navigate(ui);
                     WebBrowser wb = wbo.WebBrowser;
                     wb.LoadCompleted += new System.Windows.Navigation.LoadCompletedEventHandler(wb_LoadCompleted);
                     wb.Navigated += new System.Windows.Navigation.NavigatedEventHandler(wb_Navigated);
                     //wb.Navigated += new EventHandler<System.Windows.Navigation.NavigationEventArgs>(wb_Navigated);
                     wb.Navigate(ui);

                    //_webBrowserPlacementTarget.Height = this.Height - 100 - 96 - 69;
                    //_webBrowserPlacementTarget.Width  = this.WinGrid.Width - 100 - 20;

                 }));
                // Controls.Add(webBrowser);
                //SpecPredlog.ScrollBarsEnabled = true;

                //SpecPredlog.DocumentText = "<html><body style=\"margin: 0; padding: 0;\">" + ri.Html + "</body></html>";
                //while (webBrowser.ReadyState != WebBrowserReadyState.Complete)
                //{
                //    Application.DoEvents();
                //}
                ////Выставляем ... Web-броузер чтобы правильно отображать.
                //webBrowser.Size = new Size(webBrowser.Document.Body.ScrollRectangle.Width, webBrowser.Document.Body.ScrollRectangle.Height);
                //while (webBrowser.ReadyState != WebBrowserReadyState.Complete)
                //{
                //    Application.DoEvents();
                //}
                //webBrowser.ScrollBarsEnabled = false;
                //Rectangle rec = webBrowser.Document.Body.ClientRectangle;
                //ri.Result = GetBitmap(webBrowser, rec.Size.Width, rec.Size.Height);
            }
            catch (Exception ex)
            {
                SystemConecto.ErorDebag("Браузер вызвал исключение: " + Environment.NewLine +
                    " === Адресс: " + ri.Html + Environment.NewLine + 
                    " === Message: " + ex.Message.ToString() + Environment.NewLine + 
                    " === Exception: " + ex.ToString(), 1);
                // LogWriter(ex,QRPRErrorLevel.ErrorM2);                
            }
        }


        #region События обработки браузера
        /// <summary>
        /// Документ загружен
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void wb_LoadCompleted(object sender, EventArgs e)
        {
            if (sender is WebBrowser)
            {
                // Пример кода
                //MessageBox.Show("Test");
                WebBrowser currentBrowser = (WebBrowser)sender;
                HTMLDocument doc = currentBrowser.Document as HTMLDocument;
                // Если сервер вернул ничего то протокол отобразит Низвестный протокол Нам нужен HyperText Transfer Protocol
                if (doc.protocol.ToString() == "HyperText Transfer Protocol")
                {
                    WaitMessageShow("", 9);
                    wbo.Visibility = Visibility.Visible;
                }
                else
                {
                    WaitMessageShow("", 9);
                    wbo.Visibility = Visibility.Collapsed;
                    WaitMessageShow("Отсутствует соединение с интернет сервером conecto.ua");
                }

                
                // Отладка событий
                // SystemConecto.ErorDebag("Сервер вернул: " + doc.protocol.ToString(), 2);

                //doc.title
                //WebBrowser.HtmlDocument tempDoc = (System.Windows.Controls. Controls.HtmlDocument)currentBrowser.Document;
                //tempDoc.
                //MessageBox.Show(tempDoc.Title);
                
                
            }
        }

        /// <summary>
        /// Документ загружен
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void wb_Navigated(object sender, EventArgs e)
        {
            if (sender is WebBrowser)
            {
                // Пример кода
                //MessageBox.Show("Test");
                WebBrowser currentBrowser = (WebBrowser)sender;
                HTMLDocument doc = currentBrowser.Document as HTMLDocument;
                // Если сервер вернул ничего то протокол отобразит Низвестный протокол Нам нужен HyperText Transfer Protocol
                if (doc.protocol.ToString() == "HyperText Transfer Protocol")
                {
                    WaitMessageShow("Загрузка данных ...", 11);
                    
                }
                else
                {
                    WaitMessageShow("", 9);
                    wbo.Visibility = Visibility.Collapsed;
                    WaitMessageShow("Отсутствует соединение с интернет сервером conecto.ua");
                }


                // Отладка событий
                // SystemConecto.ErorDebag("Сервер вернул: " + doc.protocol.ToString(), 2);

                //doc.title
                //WebBrowser.HtmlDocument tempDoc = (System.Windows.Controls. Controls.HtmlDocument)currentBrowser.Document;
                //tempDoc.
                //MessageBox.Show(tempDoc.Title);


            }
        }
        #endregion



        #region События Click (Клик) функцилнальных клавиш рабочего стола
        private void Close__MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            
        }
        private void ImClose__MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            this.Close_();
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

                    this.Close_();

                }

            }


            // Отладка
            // MessageBox.Show(e.Key.ToString());

        }

        

        #endregion


        #region Выход из окна
        private void Close_()
        {
            //SystemConecto.ErorDebag("Close", 1);
            //this.Visibility = Visibility.Hidden;

            

            // Закрыть браузер
            // SpecPredlog.Dispose();

            //var Window = SystemConecto.ListWindowMain("WaitFonW");
            //if (Window != null)
            //{

            //    Window.Close();
               
            //}
            //var Window_ = SystemConecto.ListWindowMain("WaitFonW");
            //if (Window_ != null)
            //{

            //    Window_.Visibility = Visibility.Hidden;
            //    Window_.Hide();
            //    while (Window_.Visibility == Visibility.Visible)
            //    {
            //        //SystemConecto.ErorDebag("Не скріто", 1);
            //        Window_.Visibility = Visibility.Hidden;
            //    }
            //    //SystemConecto.ErorDebag("Скрыл", 1);
            //}
            
            // Скрыть

            this.Visibility = Visibility.Hidden;

            this.Owner.Focus();
            this.Owner = null;

            this.Close();


        }



        #endregion

        #region Вспомогательное окно внутри окна для вывода сообщений 
        /// <summary>
        /// Вывод окна WaitMesage Желтое окно<para></para>
        /// TypeWin: 1 - Отобразить сообщение по центру экрана
        /// 3 - Отобразить сообщение по центру экрана c анимированным баром
        /// 9 - закрыть все окна
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void WaitMessageShow(string Message, int TypeWin = 0)
        {
            // Закрытие другого открытого сообщение во внешнем окне
            dynamic WinActivePanelLeft = SystemConecto.ListWindowMain("SpecPredMessage");
            if (WinActivePanelLeft != null)
            {
                WinActivePanelLeft.CloseMessage();
            }
            
            
            // Отобразить сообщение по центру экрана
            if (TypeWin == 0)
            {
                MessageText.Text = Message; // "Отсутствует соединение с интернет сервером conecto.ua";
                Message1.Visibility = Visibility.Visible;
                WaitGif.Visibility = Visibility.Collapsed;
            }

            if (TypeWin == 3)
            {
                MessageText.Text = Message; // "Отсутствует соединение с интернет сервером www.conecto.ua";
                Message1.Visibility = Visibility.Visible;

                BitmapImage image = new BitmapImage();
                try
                {
                    // анимационный gif
                    
                    image.BeginInit();
                    image.UriSource = new Uri(@"/Conecto®%20WorkSpace;component/Images/waitmessage_013.gif", UriKind.Relative); //new Uri(fileName);
                    image.EndInit();

                    ImageBehavior.SetAnimatedSource(WaitGifIm, image);
                    WaitGif.Visibility = Visibility.Visible;
                }
                catch(Exception ex)
                {
                    SystemConecto.ErorDebag("Библиотека WpfAnimatedGif вызвала исключение: \r\n === Файл: " + image.UriSource.ToString() + "\r\n === Message: " + ex.Message.ToString() + "\r\n === Exception: " + ex.ToString(), 1);
                }
            }



            // Скрыть все окна
            if (TypeWin == 9)
            {
                
                Message1.Visibility = Visibility.Hidden;

            }

            // Вывести внешнее окно

            if (TypeWin == 11)
            {
                // Дял привязки окна к основному окну
                //MainWindow ConectoWorkSpace_InW = (MainWindow)Application.Current.MainWindow;
                MainWindow ConectoWorkSpace_InW = AppStart.LinkMainWindow();
                // Створюємо інше вікно з повідомленням
                WinOblakoVerh WinOblakoVerh_Info = new WinOblakoVerh(Message, 0, 2); // создаем AutoClose
                WinOblakoVerh_Info.Name = "SpecPredMessage";
                WinOblakoVerh_Info.Owner = ConectoWorkSpace_InW; //ConectoWorkSpace_InW;
                // Размеры - констаны окна нужно установить.
                WinOblakoVerh_Info.Top = this.Top + this.Height / 2 - 195/2; // отнять высоту окна и промежуток межу окнами
                WinOblakoVerh_Info.Left = this.Left + this.Width/2 - 274/2;
                // Не активировать окно - не передавать клавиатурный фокус
                WinOblakoVerh_Info.ShowActivated = false;
                WinOblakoVerh_Info.Show();
                // Разместить...
            }
        }

        #endregion


        #region Разработка

        // Перемещать окно мышью (разработка) работает с браузером WB
        //protected override void OnMouseMove(MouseEventArgs e)
        //{
        //    base.OnMouseMove(e);

        //    if (e.LeftButton == MouseButtonState.Pressed &&
        //        // For some reason Slider doesn't seem to mark MouseMove events as handled.
        //        //  По некоторым причинам Slider не кажется, чтобы отметить события MouseMove как обработанное.
        //  XML <Slider Name="_opacitySlider" Minimum="0.05" Maximum="1" SmallChange="0.01" Value="0.7"  Height="30" Width="150" Margin="5 -2 5 0"  />
        //        VisualTreeHelper.HitTest(_opacitySlider, e.GetPosition(_opacitySlider)) == null)
        //    {
        //        DragMove();
        //    }
        //}


        #endregion


    }
}
