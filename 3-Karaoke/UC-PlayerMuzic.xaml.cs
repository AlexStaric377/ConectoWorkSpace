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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ConectoWorkSpace._3_Karaoke
{
    /// <summary>
    /// Логика взаимодействия для UC_PlayerMuzic.xaml
    /// </summary>
    public partial class UC_PlayerMuzic : UserControl
    {
        public double a;
        public double b;
        public int c;

        public UC_PlayerMuzic()
        {
            InitializeComponent();
        }







        private void textBox6_KeyDown(object sender, KeyEventArgs e)
        {

            // процедура определения оценки за песню --> будет с TextBlock
            /*  string path = Environment.CurrentDirectory;
              images.Add(new Image(new Uri(string.Format(@"{0}\Images\starG\01.jpg"))));  */
            #region  оценка песни 1
            if (e.Key == Key.Enter)
            {
                a = Convert.ToDouble(textBox6.Text);
                b = Math.Truncate(a);
                a = a - b;
                c = Convert.ToInt32(b);
                switch (c)
                {
                    case 0:
                        image3.Visibility = Visibility.Hidden;
                        image4.Visibility = Visibility.Hidden;
                        image5.Visibility = Visibility.Hidden;
                        image6.Visibility = Visibility.Hidden;
                        image7.Visibility = Visibility.Hidden;
                        if (a > 0.1)
                        {
                            //image8.Visibility = Visibility.Visible;
                            //image8.Margin = Thickness
                            //image3.Height = Height;
                            // image3.Height = image3.Height / 8.6;

                        }
                        break;
                    case 1:
                        image3.Visibility = Visibility.Visible;
                        image4.Visibility = Visibility.Hidden;
                        image5.Visibility = Visibility.Hidden;
                        image6.Visibility = Visibility.Hidden;
                        image7.Visibility = Visibility.Hidden;
                        break;
                    case 2:
                        image3.Visibility = Visibility.Visible;
                        image4.Visibility = Visibility.Visible;
                        image5.Visibility = Visibility.Hidden;
                        image6.Visibility = Visibility.Hidden;
                        image7.Visibility = Visibility.Hidden;
                        break;
                    case 3:
                        image3.Visibility = Visibility.Visible;
                        image4.Visibility = Visibility.Visible;
                        image5.Visibility = Visibility.Visible;
                        image6.Visibility = Visibility.Hidden;
                        image7.Visibility = Visibility.Hidden;
                        break;

                    case 4:
                        image3.Visibility = Visibility.Visible;
                        image4.Visibility = Visibility.Visible;
                        image5.Visibility = Visibility.Visible;
                        image6.Visibility = Visibility.Visible;
                        image7.Visibility = Visibility.Hidden;
                        break;

                    case 5:
                        image3.Visibility = Visibility.Visible;
                        image4.Visibility = Visibility.Visible;
                        image5.Visibility = Visibility.Visible;
                        image6.Visibility = Visibility.Visible;
                        image7.Visibility = Visibility.Visible;
                        break;
                }

                if (a > 0.2)
                { }
                if (a > 0.2)
                { }
                if (a > 0.2)
                { }
                if (a > 0.2)
                { }
                if (a > 0.2)
                { }
                if (a > 0.2)
                { }
                if (a > 0.2)
                { }
                if (a > 0.2)
                { }

            }
            #endregion
            /*  
            #region оценка песни 2
            if (e.Key == Key.Enter)
            {
                a = Convert.ToDouble(textBox6.Text);
                b = Math.Truncate(a);
                a = a - b;  //b = 0.****
                c = Convert.ToInt32(b);
                    for (int i = 0; i < a; i++)
                        {
                            image3.Height = Height / 86;
                        }

            }
            #endregion  */
        }

        private void bt11_MouseUp(object sender, MouseButtonEventArgs e)
        {
            /*  var uri = new Uri(Parent.BaseUri Path);
              var result = new BitmapImage (uriSource = uri);
              return result;  */
            //bt11.Content = new Image() {Source ="C:\btstop.jpg"} ;
            // bt11.Content = new ImageBrush() { ImageSource = "C:\btstop.jpg" };
        }




        /* private void button1_MouseUp(object sender, MouseButtonEventArgs e)
         {
           //  button1.Background = Foreground.SetValue(Set);
            // Background.Equals("Red");
         }  */







    }
}
