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
    /// Логика взаимодействия для SplashScreenConecto.xaml
    /// </summary>
    public partial class SplashScreenConecto : Window
    {
        public SplashScreenConecto(string NameCaption = "")
        {
            InitializeComponentnet(NameCaption);


        }
        public void InitializeComponentnet(string @NameCaption)
        {
            if (_contentLoaded)
            {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/Conecto® WorkSpace;component/splasher-startwindow/splashscreenConecto.xaml", System.UriKind.Relative);

            #line 1 "..\..\..\..\Splasher-startWindow\SplashScreenConecto.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            this.CaptionPRG.Content = (NameCaption == "" ? this.CaptionPRG.Content : NameCaption);
            #line default
            #line hidden
        }

    }
}
