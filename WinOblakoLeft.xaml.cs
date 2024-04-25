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
// Анимированный гиф
using WpfAnimatedGif;

namespace ConectoWorkSpace
{
    /// <summary>
    /// Логика взаимодействия для Window1.xaml
    /// </summary>
    public partial class WinOblakoLeft : Window
    {
        System.Windows.Threading.DispatcherTimer CloseAuto;

        public WinOblakoLeft(string TextWindows=null, int AutoClose = 0, int InfoWindow = 0)
        {

            InitializeComponent();
            // Змінити фон Grida
            if(InfoWindow ==1)
            {
                this.WinGrid.Background = new ImageBrush(new BitmapImage(new Uri(@"pack://application:,,,/Conecto®%20WorkSpace;component/Images/podskazka2.png")));
                this.WinGrid.Height = 156;
                this.Height = 195;
            }

            if (InfoWindow == 2)
            {
                this.grid2.Margin = new Thickness(32, 32, 0, 0); 
                this.grid2.Height = 92;
                this.WinGrid.Background = new ImageBrush(new BitmapImage(new Uri(@"pack://application:,,,/Conecto®%20WorkSpace;component/Images/podskazka2.png")));
                this.WinGrid.Height = 156;
                this.Height = 195;
                this.Topmost = true;
                Close_F.Visibility = Visibility.Collapsed;

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
                catch (Exception ex)
                {
                    SystemConecto.ErorDebag("Библиотека WpfAnimatedGif вызвала исключение: \r\n === Файл: " + image.UriSource.ToString() + "\r\n === Message: " + ex.Message.ToString() + "\r\n === Exception: " + ex.ToString(), 1);
                }

            }

            // Автозакрытие окна
            if (AutoClose == 1)
            {

                CloseAuto = new System.Windows.Threading.DispatcherTimer();
                CloseAuto.Tick += new EventHandler(CloseAutoTick);
                CloseAuto.Interval = new TimeSpan(0, 0, 7);
                CloseAuto.Start();
                
                
                //this.CloseAuto.Enabled = true;
            }

            if (TextWindows != null)
            {
                // Определить высоту окна по количеству \n в многострочном тексте
                var TextWindows_a = TextWindows.Split('\n');
                if (TextWindows_a.Count() > 2)
                {

                    this.MessageText.Margin = new Thickness(this.MessageText.Margin.Left, 32, 0, 0); ;
                    this.MessageText.Height = 88;
                    var Height = 17 * TextWindows_a.Count();
                    // Предусматреть плавающий текст окно не должно быть выше 88 px
                    if (Height > 88)
                    {

                        // Обрезать строки, первые три строки
                        for (int i = 3; i < TextWindows_a.Count(); i++)
                        {

                            TextWindows += TextWindows_a[i];

                        }
                        // Пересчитать количество строк
                        // TextWindows_a = TextWindows.Split('\n');
                        // this.Height = this.Height + 22 * (TextWindows_a.Count() - 2);
                        this.MessageText.Text = TextWindows != null ? TextWindows : "Сообщение отсутствует!";

                    }
                    else
                    {
                        this.MessageText.Text = TextWindows != null ? TextWindows : "Сообщение отсутствует!";
                    }

                }
                else
                {
                    this.MessageText.Text = TextWindows != null ? TextWindows : "Сообщение отсутствует!";
                }
            }

        }

        

        #region События Click (Клик) функцилнальных клавиш рабочего стола

        /// <summary>
        /// Закрыть окно через пять секунд
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CloseAutoTick(object sender, EventArgs e)
        {
            CloseMessage();
        }

        private void Close_F_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

            CloseMessage();
        }

        /// <summary>
        /// Закрыть окно сообщение
        /// </summary>
        public void CloseMessage()
        {
            this.Visibility = Visibility.Hidden;

            //this.Owner.Focus();
            this.Owner = null;

            this.Close();
        }

        #endregion


        #region Изображения как клавиши - Визуализация интерфейса 
        private void Close_F_MouseMove(object sender, MouseEventArgs e)
        {
            var uriSource = new Uri(@"/Conecto®%20WorkSpace;component/Images/x2.png", UriKind.Relative);
            this.Close_F.Source = new BitmapImage(uriSource);
        }

        private void Close_F_MouseLeave(object sender, MouseEventArgs e)
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
               

            }
            else
            {

                // Нет отказаться
                if (e.Key == Key.Escape)
                {
                    CloseMessage();

                }

            }
            

           // Отладка
           // MessageBox.Show(e.Key.ToString());

        }
        #endregion



    }
}
