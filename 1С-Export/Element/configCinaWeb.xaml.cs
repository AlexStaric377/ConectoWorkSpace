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

namespace ConectoWorkSpace._1С_Export.Element
{
    /// <summary>
    /// Логика взаимодействия для configCinaWeb.xaml
    /// </summary>
    public partial class configCinaWeb : UserControl
    {
        /// <summary>
        /// Путь расположения файла
        /// </summary>
        public static string Putch1File = SystemConecto.PutchApp + @"config\user\cinaweb.xml";


        public configCinaWeb()
        {
            InitializeComponent();

            string[] ReadSql = SystemConecto.ReadFile_SqlPass(Putch1File, "", 1);

            var FileText = Convert.ToInt32(ReadSql[0]) == 0 ? TextFile : ReadSql[1];


            rtxtText_act_real.AppendText(FileText);


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
            string _Text = new TextRange(rtxtText_act_real.Document.ContentStart, rtxtText_act_real.Document.ContentEnd).Text;



            SystemConecto.WriteFile_SqlPass(Putch1File, ref  _Text, 1);


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
        /// Данные настроек TypeOper - тип операций bank - банковские выписки; kassa - кассвые ордера; avans - авансовый отчет о деньгах, разновидность расшифровки ордера<para></para>
        /// ColumnSchemaIm - название колонок  или порядковый номер в структуре данных, а также выражение подставляемые в поле в косых /тут выражение/
        /// Type_OTPR_POLU - тип определения получателя и отправителя 1-признак PRIZNAK (1-отправитель,2-получатель), получатель основной по умолчанию, поля COLUM_NAME (соответсвие названий полей), знак числа ZNAK_CHISLA (-,+)
        /// 
        /// ColumnNameNumRow - Номер строки в которой содержится название колонок, для xls, csv
        /// 
        /// AutoDefaultNameFile - имя файла который ищется если не указан другой, отменил использование так как система использует все файлы из директории будто .xls .dbf .csv (автораспознание)<para></para>
        /// NameGroupOrgani - название группы для организаций, карточки которых создаются в этой группе при условии не нахождения ЕРДПО в справочнике.<para></para>
        /// IDGroupOrgani - код группы<para></para>
        /// (устарела) Import_PutchFileSchema=\"\" Import_PutchFileSchema - путь к файлу из которого взята структура импорта   <para></para>
        /// (устарела) UNICColumn1=\"column_data\" UNICColumn2=\"nomer\" UNICColumn1 - название уникального поля номер 1 (по данному полю проверяются записи на совпадение)<para></para>
        /// </summary>
        public static string TextFile =
                "<?xml version=\"1.0\" encoding=\"utf-16\"?><Параметры-ИмпортаБанковскихОпераций>" + Environment.NewLine +
                "<FileConfig FileConfig_Ver-File-Config=\"0_1\" PuthFile=\"" + @"config\user\" + "\" /><AppOverall idApp=\"122\" />" + Environment.NewLine +
                "  <OpciiOverall_1 CountSchem=\"2\"  />" + Environment.NewLine +

                "  <SchemaImporta_1 onoff=\"true\" idschema=\"1\"  NameSchema=\"Укрсоцбанк DBF\" TypeOper=\"bank_dbf\"  " + Environment.NewLine +
                "  NameGroupOrgani=\"Новые организации из Импорта\"  " + Environment.NewLine +
                "  IDGroupOrgani=\"100000\"  " + Environment.NewLine +
                "  BaseColumnSchemaIm =\"NUM_DOC, DATE_DOC, TYPE_OTPR_POLU, CODE_OTPROV, NAME_OTPR, RR_OTPROV, CODE_BANK_OTPROV, NAMEBANK_OTPR, " +
                "  CODE_POLUCH, NAME_POLUCH, RR_POLUCH, CODE_BANK_POLUCH, NAMEBANK_POLUCH, summa, tp_in_out, curr, prim, dop_info\" " + Environment.NewLine +
                "  ColumnSchemaIm =    \"     ND, DATA_S,             DK,      KL_OKP,     KL_NM,    KL_CHK,              MFO,        MFO_NM, " +
                "     KL_OKP_K,     KL_NM_K,  KL_CHK_K,            MFO_K,        MFO_NM_K,     S,          ,CUR_ID, N_P, /AUTOORDER/\" " + Environment.NewLine +
                "  Type_OTPR_POLU =\"PRIZNAK\" " + Environment.NewLine +
                "  Opcii_OTPR_POLU =\"1;2,,-;+\" " + Environment.NewLine +
                "  BaseCurr =\"UA_980,USD_840,EVRO_978,RUB_643\" " + Environment.NewLine +
                "  CurrImport =\"980\" " + Environment.NewLine +
                "  CurrDef =\"980\" " + Environment.NewLine +
                "  КодПодразделенияКассы =\"4405********0701;09106\" " + Environment.NewLine +
                "  Префикс_Документа_Приход_Расход =\"П;Р\" " + Environment.NewLine +
                "  Банковские_услуги =\";\" " + Environment.NewLine +
                "  CashUSERID =\"\" " + Environment.NewLine +
                "  РазделительСуммы =\".\" " + Environment.NewLine +
                "  ColumnNameNumRow =\"1\" " + Environment.NewLine +
                " ArticlesPeremechenija-1=\"Тут я был\" " + Environment.NewLine +
                " ArticlesPeremechenija-2=\"Тут я был44\" " + Environment.NewLine +
                "/>" + Environment.NewLine +

                " <SchemaImporta_2 onoff=\"true\" idschema=\"2\" NameSchema=\"Приват24 Xls\" TypeOper=\"kassa_xls\"  " + Environment.NewLine +
                "  NameGroupOrgani=\"Новые организации из Импорта\"  " + Environment.NewLine +
                "  IDGroupOrgani=\"100000\"  " + Environment.NewLine +
                "  BaseColumnSchemaIm =\"NUM_DOC, DATE_DOC, TYPE_OTPR_POLU, CODE_OTPROV, CASH_PODR, SOTR, summa, bank_posluga, tp_in_out, " +
                "  curr, prim\" " + Environment.NewLine +
                "  ColumnSchemaIm =    \"     ,         1,                ,            ,         2,     ,     4,            6,   , " +
                "     5, 3  /AUTOORDER/  \" " + Environment.NewLine +
                "  Type_OTPR_POLU =\"ZNAK_CHISLA\" " + Environment.NewLine +
                "  Opcii_OTPR_POLU =\"1;2,,-;+\" " + Environment.NewLine +
                "  BaseCurr =\"UA_980,USD_840,EVRO_978,RUB_643\" " + Environment.NewLine +
                "  CurrImport =\"UAH\" " + Environment.NewLine +
                "  CurrDef =\"UAH\" " + Environment.NewLine +
                "  КодПодразделенияКассы =\"4405********0701;09106\" " + Environment.NewLine +
                "  Префикс_Документа_Приход_Расход =\"П;Р\" " + Environment.NewLine +
                "  Банковские_услуги =\"RAZNICA;0702056;За банковские услуги\" " + Environment.NewLine +
                "  CashUSERID =\"\" " + Environment.NewLine +
                "  РазделительСуммы =\".\" " + Environment.NewLine +
                "  ColumnNameNumRow =\"1\" " + Environment.NewLine +
                " ArticlesPeremechenija-=\"Тут я был 2\" " + Environment.NewLine +
                " ArticlesPeremechenija-2=\"Тут я был55\" " + Environment.NewLine +
                "/>" + Environment.NewLine +

                "<!-- CUR_ID UA 980 код валюты -->" + Environment.NewLine +
                "<!-- CurrDef код валюты по умолчанию -->" + Environment.NewLine +
                "<!-- КодПодразделенияКассы - код соответсвий для TYPE_OTPR_POLU -> ZNAK_CHISLA : код карты или авансового отчета ; код подразделения , новый код ; код подр. -->" + Environment.NewLine +
                "<!-- CashUSERID - код МОЛ 1010 кассира который ответственный за расходы и доходы по кассе -->" + Environment.NewLine +
                "<!-- BaseColumnSchemaIm - параметр не редактируется только для чтения -->" + Environment.NewLine +
                "<!-- Articles.... - параметр статьии движения денежжных средств {параметры определения статьии: 1, 2;код движения денежных средств;код организации если статья уникально привязана} -->" + Environment.NewLine +
                "<!-- Банковские_услуги - 1 тип расчета (RAZNICA - от колонки bank_posluga - summa; COLUMN_SUMMA - сумма услуги за банковскую операцию указанна в колонке);2 статья движения код; 3 Комментарий  -->" + Environment.NewLine +
                "</Параметры-ИмпортаБанковскихОпераций>";

    }
}
