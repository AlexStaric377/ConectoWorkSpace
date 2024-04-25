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
using ConectoWorkSpace;

// Управление вводом-выводом
using System.IO;



namespace ConectoWorkSpace._1С_Export.Element
{
    /// <summary>
    /// Логика взаимодействия для configEksportDoc.xaml
    /// </summary>
    public partial class configEksportDoc : UserControl
    {
        /// <summary>
        /// Путь расположения файла
        /// </summary>
        public static string Putch1File = SystemConecto.PutchApp + @"config\user\1cshluz.xml";
        
        
        public configEksportDoc()
        {
            InitializeComponent();

            string[] ReadSql = SystemConecto.ReadFile_SqlPass(Putch1File,"",1);

            var FileText = Convert.ToInt32(ReadSql[0]) == 0 ? TextFile : ReadSql[1];


            rtxtText_opciiAll.AppendText(FileText);

        }

        /// <summary>
        /// Событие сохранить
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Toolb_config_Click(object sender, RoutedEventArgs e)
        {

            //Видимость панели инструментов
            // Пример обращения к меню MenuItem mItem = sender as MenuItem; // mItem.IsChecked   TextRange allTextRange
            string _Text = new TextRange(rtxtText_opciiAll.Document.ContentStart, rtxtText_opciiAll.Document.ContentEnd).Text;



            SystemConecto.WriteFile_SqlPass(Putch1File, ref  _Text, 1);
            // Обновление настроек
            AppPlayStory_1C.ReadMemoryConfigFile();


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

        /// <summary>
        /// Данные настроек
        /// DateExport - дата последнего експорта данных<para></para>
        /// запись структуры 1cshluz.xml <para></para>
        /// </summary>
        public static string TextFile =
                "<?xml version=\"1.0\" encoding=\"utf-16\"?><Параметры-ОбменаДанными>" + Environment.NewLine +
                "<FileConfig FileConfig_Ver-File-Config=\"0_1\" PuthFile=\"" + @"config\user\" + "\" /><AppOverall idApp=\"122\" />" + Environment.NewLine +
                " <OpciiOverall_122 BDSERVER_Alias=\"NameAliasBD\" BDSERVER_IP=\"127.0.0.1\" BDSERVER_Port=\"3055\" BDSERVER_Putch-Hide=\"\" " + Environment.NewLine + 
                " Export_PutchDefault=\"" + @"D:\Conecto\Export" + "\" " + Environment.NewLine +
                " Import_PutchBankDefault=\"" + @"D:\Conecto\ImportBank" + "\" " + Environment.NewLine +
                " TypeAutorizeSDR=\"RESTORAN\" CommentTypeAutorizeSDR=\"OTEL;03;05,RESTORAN;25,OTEL\" " + Environment.NewLine +
                " TypeAutorizeSPORG=\"RESTORAN\" CommentTypeAutorizeSPORG=\"OTEL;03;05,RESTORAN;25,OTEL\" " + Environment.NewLine +
                " TypeAutorizeSDRCach=\"RESTORAN\" CommentTypeAutorizeSDRCach=\"OTEL;03;05,RESTORAN;25,OTEL\"  " + Environment.NewLine +
                " DateExport=\"\" DateImport=\"\" " + Environment.NewLine +
                " ВключениеРежимаОтладки_настройка_модулей=\"true\"" + Environment.NewLine +
                "Период_запроса_настройка_експорта=\"45\" /> "  + Environment.NewLine +
                "</Параметры-ОбменаДанными>";




    }
}






