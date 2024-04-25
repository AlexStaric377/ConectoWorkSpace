using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Net;
// Управление вводом-выводом
using System.IO;
using System.IO.Compression;
// BD
using FirebirdSql.Data.FirebirdClient;  // http://www.sql.ru/forum/actualthread.aspx?tid=133383   http://www.firebirdsql.org/en/net-examples-of-use/
using FirebirdSql.Data.Isql;

using System.Data;              // Содержит типы, независимые от провайдеров, например DataSet и DataTable.
using System.Data.SqlClient;    // Содержит типы SQL Server .NET Data Provider

/// Многопоточность
using System.Threading;
using System.Windows.Threading;
using ConectoWorkSpace.Systems;

using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

using Microsoft.Win32;
//using System.Windows.Forms;
// --- Process 
using System.Diagnostics;
using System.ComponentModel;
using System.Management;

using System.Net.Sockets;
// Анимированный гиф
using WpfAnimatedGif;

using mshtml;



namespace ConectoWorkSpace
{
    public  class LibButton
    {

        public   void StrokaLabel()
        {

            Label foo = new Label();
            foo.Width = 705;
            foo.Height = 28;
            foo.Name = "SetDey";
            foo.HorizontalAlignment = HorizontalAlignment.Left;
            foo.VerticalAlignment = VerticalAlignment.Top;
            foo.HorizontalContentAlignment = HorizontalAlignment.Center;
            foo.Margin = new Thickness(50, 50, 0, 0);
            foo.Content = "Настройка расписания времени создания резервной копии базы данных";
            foo.FontFamily = new FontFamily("Courier New"); ;
            foo.FontSize = 15;
            foo.FontWeight = FontWeights.Normal;
            foo.Foreground = Brushes.Black;
            foo.Background = Brushes.DarkGray;
            foo.IsEnabled = true;
            //foo.MouseLeftButtonUp += new MouseButtonEventHandler(ConectoWorkSpace.Administrator.AdminPanels.LocDisk);
            //foo.FontStyle = FontStyles.Italic;
            foo.Visibility = Visibility.Visible;
            AddLabel(foo);

            //TextBlock textBlock = new TextBlock(new Run("A bit of text content..."));

            //textBlock.Background = Brushes.AntiqueWhite;
            //textBlock.Foreground = Brushes.Navy;

            //textBlock.FontFamily = new FontFamily("Century Gothic");
            //textBlock.FontSize = 12;
            //textBlock.FontStretch = FontStretches.UltraExpanded;
            //textBlock.FontStyle = FontStyles.Italic;
            //textBlock.FontWeight = FontWeights.UltraBold;

            //textBlock.LineHeight = Double.NaN;
            //textBlock.Padding = new Thickness(5, 10, 5, 10);
            //textBlock.TextAlignment = TextAlignment.Center;
            //textBlock.TextWrapping = TextWrapping.Wrap;

            //textBlock.Typography.NumeralStyle = FontNumeralStyle.OldStyle;
            //textBlock.Typography.SlashedZero = true;


        }
        public   void AddLabel(UIElement label)
        {
            Grid.SetColumn(label, 0);
            Grid.SetRow(label, 1);
            ConectoWorkSpace.Administrator.AdminPanels ConectoWorkSpace_InW = AppStart.LinkMainWindow("WAdminPanels");
            ConectoWorkSpace_InW.GridFbd25.Children.Add(label);

        }



    }

}