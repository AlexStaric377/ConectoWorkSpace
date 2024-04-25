using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

// Управление вводом-выводом
using System.IO;
using System.Text;

namespace ConectoWorkSpace.Administrator
{
    /// <summary>
    /// Логика взаимодействия для write_act_real.xaml
    /// </summary>
    public partial class write_config_appplay : UserControl
    {
        private string Putch1FileSQL = SystemConecto.PutchApp + @"appplay.xml";

        public write_config_appplay()
        {
            InitializeComponent();

            string[] ReadSql = SystemConecto.ReadFile_SqlPass(Putch1FileSQL);

            var FileText = Convert.ToInt32(ReadSql[0]) == 0 ? Properties.Resources.appplay : ReadSql[1];


            rtxtText_act_real.AppendText(FileText);

        }

        /// <summary>
        /// Событие сохранить
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Toolb_act_real_Click(object sender, RoutedEventArgs e)
        {

            //Видимость панели инструментов
            // Пример обращения к меню MenuItem mItem = sender as MenuItem; // mItem.IsChecked  TextRange allTextRange = 
            string _Text = new TextRange(rtxtText_act_real.Document.ContentStart, rtxtText_act_real.Document.ContentEnd).Text;
            SystemConecto.WriteFile_SqlPass(Putch1FileSQL, ref _Text);
            //if (!AppStart.Load_appplay())
            //{
            //    SystemConecto.ErorDebag("Ошибка загрузки файла конфигураций приложений", 2, 2);
            //}


        }

        /// <summary>
        /// Печать кода
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Print_act_real(object sender, RoutedEventArgs e)
        {

            PrintDialog pd = new PrintDialog();
            if ((pd.ShowDialog() == true))
            {
                //use either one of the below      
                //pd.PrintVisual(rtxtText_act_real as Visual, "printing as visual");
                //pd.PrintDocument((((IDocumentPaginatorSource)rtxtText_act_real.Document).DocumentPaginator), "printing as paginator");
            }

        }
        


    }
}
