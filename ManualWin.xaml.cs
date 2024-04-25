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
//---- объекты ОС Windows (Реестр, {Win Api} 
using Microsoft.Win32;
// Импорт библиотек Windows DllImport (управление питанием ОС, ...
using System.Runtime.InteropServices;
// Управление вводом-выводом
using System.IO;
/// Многопоточность
using System.Threading;
using System.Windows.Threading;

namespace ConectoWorkSpace
{
    /// <summary>
    /// Логика взаимодействия для Window1.xaml
    /// </summary>
    public partial class ManualWin : Window
    {
        public static Dictionary<string, Process> ProcessAdobeFile = new Dictionary<string, Process>(); // Открытые файлы адоба документации
        
        public ManualWin()
        {
            InitializeComponent();

            ResolutionDisplay();
        }

        #region Построение интерфейса
        /// <summary>
        /// Отрисовка изменений размера экрана
        /// </summary>
        public void ResolutionDisplay()
        {

            // 3. Установка размеров окна для того чтобы игнорировать нажатие клавиши раскрыть на весь экран
            // Рабочего стола ([0] - Top; [1] - Left; [2] - Width; [3] - Height; [4] - Right;
            this.Top = SystemConecto.WorkAreaDisplayDefault[0] + 10;
            this.Left = SystemConecto.WorkAreaDisplayDefault[1] + 10;
            this.WindGrid.Width = this.Width = SystemConecto.WorkAreaDisplayDefault[2] - (277 + 5 + 10);
            this.Height = SystemConecto.WorkAreaDisplayDefault[3] - 35;
            this.Okno.Height = this.Height - 100;
            //this.Location = new System.Drawing.Point(SizeDWArea_a[1], SizeDWArea_a[0]);
            //this.ClientSize = new System.Drawing.Size(SizeDWArea_a[2], SizeDWArea_a[3]);

            label1.Visibility = Visibility.Collapsed;
            MenuIt1_1.Visibility = Visibility.Collapsed;
            MenuIt1_2.Visibility = Visibility.Collapsed;


        }
        #endregion

        #region События Click (Клик) функцилнальных клавиш рабочего стола
        private void Close__MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            
            this.Visibility = Visibility.Hidden;

            this.Owner.Focus();
            this.Owner = null;
            this.Close();

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
                    this.Visibility = Visibility.Hidden;

                    this.Owner.Focus();
                    this.Owner = null;
                    this.Close();
                    //this.Close();

                }

            }
            

           // Отладка
           // MessageBox.Show(e.Key.ToString());

        }
        #endregion

        private void ColumnDefinition_ImageFailed(object sender, ExceptionRoutedEventArgs e)
        {

        }
        #region Нажатие на ПДФ
        private void Pdf1_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var uriSource = new Uri(@"/Conecto®%20WorkSpace;component/Images/doc2.png", UriKind.Relative);
            this.Pdf1.Source = new BitmapImage(uriSource);

        }

        private void Pdf1_MouseLeave(object sender, MouseEventArgs e)
        {
            var uriSource = new Uri(@"/Conecto®%20WorkSpace;component/Images/doc1.png", UriKind.Relative);
            this.Pdf1.Source = new BitmapImage(uriSource);
        }

        private void Pdf1_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            var uriSource = new Uri(@"/Conecto®%20WorkSpace;component/Images/doc1.png", UriKind.Relative);
            this.Pdf1.Source = new BitmapImage(uriSource);
            
            
            Pdf_MouseLeftButtonUp("Conecto.pdf");
         
        }

        private bool Pdf_MouseLeftButtonUp(string NameFileOpen)
        {
            
            
            string PutchFileOpenPdf = SystemConecto.PutchApp + @"Multimedia\pdf\" + NameFileOpen; // @"restoran.pdf";


            // Проверка открытия или закрытия окна
            if (ProcessAdobeFile.ContainsKey(NameFileOpen) && ProcessAdobeFile[NameFileOpen].HasExited==false)
            {
               // SystemConecto.ErorDebag(ProcessAdobeFile[NameFileOpen].MainWindowHandle.ToString(), 2);
                AppStart.SetFocusWindow("HandleID", ProcessAdobeFile[NameFileOpen].Id);
                return true;
            }


            // Проверка файлов Сервера БД
            Dictionary<string, string> fbembedList = new Dictionary<string, string>();
            fbembedList.Add(SystemConecto.PutchApp + @"Multimedia\pdf\" + NameFileOpen, "");

            // Проверить наличие файла
            if (SystemConecto.IsFilesPRG(fbembedList, -1, "- Проверка файлов во время чтения документации") == "True")  //if (SystemConecto.File_(PutchFileOpenPdf, 5))
            {



            }
            else
            {
                // Закачать с инета если есть доступ или из папки Conecto

                
                // Вывод сообщения
                SystemConecto.ErorDebag("Отсутствует документ для чтения.", 2);
                return false;

            }
            switch(NameFileOpen){
                case "Conecto.pdf":
                    // HKLM\Software\Wow6432Node\Adobe\Reader
                    // Проверить наличие программы
                    if (AppStart.CHEKREG_(1, "Software", "Adobe", "Acrobat Reader"))
                    {

                    }
                    else
                    {
                        // Вывод сообщения
                        SystemConecto.ErorDebag("Не установлен Acrobat Reader для чтения документа.", 2);
                        return false;

                    }
                break;
            }

            // Определить программу и путь к ней для чтения файла
            // SystemConecto.ErorDebag(string.Format("{0} - Application: {1}",  PutchFileOpenPdf, FindExecutable(PutchFileOpenPdf)), 2);

            // return false;
            try
            {
                ProcessStartInfo psiOpt = new ProcessStartInfo(FindExecutable(PutchFileOpenPdf), " " + NameFileOpen); // @"AcroRd32.exe", mlc_un7z
                //psiOpt.CreateNoWindow = true;
                psiOpt.WorkingDirectory = System.IO.Path.GetDirectoryName(PutchFileOpenPdf);
                //psiOpt.WindowStyle = ProcessWindowStyle.Hidden;
                //SystemConecto.ErorDebag(PutchFileOpenPdf, 2);


                // запускаем процесс
                var pr = new Process();
                // pr.StartInfo.Arguments = "-r";

                pr.StartInfo = psiOpt;
                pr.Start();
                pr.WaitForInputIdle(2000);

                #region Определения Хендл Окна - MainWindowHandle, через ожидание
                //for (int ix = 0; ix < 50; ++ix)
                //{
                //    System.Threading.Thread.Sleep(100);
                //    pr.Refresh();
                //    if (pr.MainWindowHandle != IntPtr.Zero) break;
                //}


                //while (pr.MainWindowTitle == "")
                //{
                //    Thread.Sleep(100);
                //    pr.Refresh();
                //}
                #endregion

                //SystemConecto.ErorDebag(pr.MainWindowHandle.ToString(), 2);
                if (ProcessAdobeFile.ContainsKey(NameFileOpen))
                {
                    ProcessAdobeFile[NameFileOpen] = pr;
                }
                else {
                    ProcessAdobeFile.Add(NameFileOpen, pr);
                }


                //pr.WaitForExit();

                #region Загружаемые модули этим процессом

                //// Get all the modules associated with 'myProcess'.
                //ProcessModuleCollection myProcessModuleCollection = pr.Modules;

                //// Display the properties of each of the modules.
                //for (int i = 0; i < myProcessModuleCollection.Count; i++)
                //{
                    
                    
                //    var myProcessModule = myProcessModuleCollection[i];
                //    SystemConecto.ErorDebag("The moduleName is "
                //       + myProcessModule.ModuleName, 2);
                //    SystemConecto.ErorDebag("The " + myProcessModule.ModuleName + "'s base address is: "
                //       + myProcessModule.BaseAddress, 2);
                //    SystemConecto.ErorDebag("The " + myProcessModule.ModuleName + "'s Entry point address is: "
                //       + myProcessModule.EntryPointAddress, 2);
                //    SystemConecto.ErorDebag("The " + myProcessModule.ModuleName + "'s File name is: "
                //       + myProcessModule.FileName, 2);
                //}
                //// Get the main module associated with 'myProcess'.
                //var myProcessModule_ = pr.MainModule;
                //// Display the properties of the main module.
                //SystemConecto.ErorDebag("The process's main moduleName is:  "
                //   + myProcessModule_.ModuleName, 2);
                //SystemConecto.ErorDebag("The process's main module's base address is: "
                //   + myProcessModule_.BaseAddress, 2);
                //SystemConecto.ErorDebag("The process's main module's Entry point address is: "
                //   + myProcessModule_.EntryPointAddress, 2);
                //SystemConecto.ErorDebag("The process's main module's File name is: "
                //   + myProcessModule_.FileName, 2);
                #endregion



            }
            catch
            {

            }
            //SystemConecto.ErorDebag("Установлен", 2);

            // ================================================== Еще вариант
            //Проверка GUID для инсталлятора, если GUID известны:

            //bool installed = codes.Any(guid =>
            //{
            //     var code = "{" + guid.ToString().ToUpper() + "}";
            //     var state = MsiQueryProductState(code);

            //     return state == 3 || state == 5);
            //});

            //Здесь codes - это guid для версий Acrobat Reader.


            return true;

        }


        [DllImport("shell32.dll", EntryPoint = "FindExecutable")]
        public static extern long FindExecutableA(
          string lpFile, string lpDirectory, StringBuilder lpResult);

        public static string FindExecutable(
          string pv_strFilename)
        {
            StringBuilder objResultBuffer =
              new StringBuilder(1024);
            long lngResult = 0;

            lngResult =
              FindExecutableA(pv_strFilename,
                string.Empty, objResultBuffer);

            if (lngResult >= 32)
            {
                return objResultBuffer.ToString();
            }

            // Ни одна программа не определенаы
            string Err_ = string.Format(
              "Error: ({0})", lngResult);

            SystemConecto.ErorDebag(Err_);

            return Err_;
        }


        #endregion

      








    }
}
