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

namespace ConectoWorkSpace.Splasher_startWindow
{
    /// <summary>
    /// Логика взаимодействия для SplashScreenCash.xaml
    /// </summary>
    public partial class SplashScreenCash : Window
    {
        public SplashScreenCash(string NameCaption = "")
        {
            InitializeComponent();
            this.CaptionPRG.Content = (NameCaption == "" ? this.CaptionPRG.Content : NameCaption);
        }
    }
}
