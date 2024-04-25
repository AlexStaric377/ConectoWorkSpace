#region импорт следующих имен пространств .NET:
//---- объекты ОС Windows (Реестр, {Win Api} 
using Microsoft.Win32;
using System;
using System.Collections.Generic;
// Управление Изображениями
using System.ComponentModel;
// Управление БД
using System.Data;
// --- Process
using System.Diagnostics;
using System.Drawing;
// Ссылка в проекте MSV2010 добовляется ...
using System.Drawing.Text;
// Подключение GAC for Net для Fireberd
using System.EnterpriseServices.Internal;
// локаль операционной системы
using System.Globalization;
// Управление вводом-выводом
using System.IO;
using System.IO.Compression;
using System.Linq;
// Удаленное управление компьютером
// 
using System.Management;
// Управление сетью
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
//--- для Проверки Сборок
using System.Reflection;
// Импорт библиотек Windows DllImport (управление питанием ОС, ...
using System.Runtime.InteropServices;
// шифрование данных
using System.Security.Cryptography;
using System.Security.Permissions;
using System.Text;
/// Многопоточность
using System.Threading;
// --- Timer
using System.Timers;
using System.Windows;
using System.Windows.Controls;
//--- WPF
using System.Windows.Media;
using System.Windows.Threading;
// Управление Xml
using System.Xml;
using System.Xml.Linq;
// --- Заставка
using ConectoWorkSpace.Splasher_startWindow;

#endregion

namespace ConectoWorkSpace
{
    
    //-------------------------------------- Работа с сетью
    
    /// <summary>
    ///  Разделяемый класс по файлам (ключевое слово - partial)
    /// </summary>

    public partial class SystemConecto
    {

        #region Проверка соединения с Интерентом а также с указанным WEB узлом
        /// <summary>
        /// Проверка соединения с Интерентом по 80 порту, а также с указанным WEB узлом, если отключен DNS функция выдает ошибку
        /// прокси http://spys.ru/proxys/UA/
        /// </summary>
        /// <param name="strServer">По умочанию указан www.google.com</param>
        /// <param name="OkServerNoComment">По умочанию не комментировать удачное соединение</param>
        /// <returns>Истина или Ложь</returns>
        public static bool ConnectionAvailable(string strServer = "www.google.com", bool OkServerNoComment = false)
        {
            strServer = "http://" + strServer;
            try
            {
                HttpWebRequest reqFP = (HttpWebRequest)HttpWebRequest.Create(strServer);

                HttpWebResponse rspFP = (HttpWebResponse)reqFP.GetResponse();
                if (HttpStatusCode.OK == rspFP.StatusCode)
                {
                    // HTTP = 200 - Интернет безусловно есть!
                    rspFP.Close();
                    if(OkServerNoComment)
                        ErorDebag("HTTP = 200 - Интернет безусловно есть с адресом: " + strServer, 1);
                    return true;
                }
                else
                {
                    // сервер вернул отрицательный ответ, возможно что инета нет
                    rspFP.Close();
                    ErorDebag("Cервер вернул отрицательный ответ: " + rspFP.StatusCode + "." + " Возможно, что связь с адресом: " + strServer + " отсутствует.", 1);
                    return false;
                }
            }
            catch (WebException Ex)
            {
                // Отследить сообщения Ex.Status
                if (Ex.Status.ToString() == "NameResolutionFailure")
                {
                    // Нужно проверить стсатус сети ... последней проверки
                    ErorDebag(" Связь с адресом: " + strServer + " отсутствует.", 1);
                }
                else
                {
                    // Ошибка, значит интернета у нас нет. Плачем :'(
                    // Тут нужна дополнительная аналитика например ошибка 403 тоже попадает сюда.
                    ErorDebag("Cервер вернул отрицательный ответ: " + Ex.Status.ToString() + " сообщение системы: " + Ex + "." + " Связь с адресом: " + strServer + " отсутствует.", 1);
                }

                return false;
            }
        }
        /// <summary>
        /// Быстрая проверка эхо ответа с помощью команды ping протокол ICMP
        /// </summary>
        /// <param name="IPAddress_">по умолчанию "8.8.8.8"</param>
        /// <returns></returns>
        public static bool ConnectionAvailable_ICMP(string IPAddress_ = "8.8.8.8")
        {
            IPAddress IpAdrr_ml = IPAddress.Parse(IPAddress_);
            if (PingNet(IpAdrr_ml))
            {
                return true;
            }
            return false;
        }
        #endregion



        #region Работа с FTP Server

        /// <summary>
        /// Синхронное соединение с FTP сервером
        /// </summary>
        /// <param name="strServer"></param>
        /// <param name="NameUser"></param>
        /// <param name="PasswdUser"></param>
        /// <param name="TypeCommand"></param>
        /// <param name="PutchTMPFile"></param>
        /// <returns></returns>
        public static string[] ConntecionFTP(string strServer, string NameUser, string PasswdUser, int TypeCommand = 1, string PutchTMPFile = "")
        {
            string[] aresultFTP = null; // Ответ с сервера | PutchTMPFile - полный путь к временной папке

            // TypeCommand = 1 - проверка ФТП, 2- загрузка файла с сервера, 3 - чтения списка, 7 - создание директории           // TypeCommand = 4;  Тест

            FileInfo fileInf = new FileInfo(strServer); // разбираем путь FTP
            string NameFile = fileInf.Name; // это название файла
            // Получить путь на FTP Сервере где находится файл (если файл отсутствует то результатом будет только путь к директории)
            String targetPath = strServer.Substring(0, strServer.Length - NameFile.Length);
            // проверка Uri FTP
            strServer = "ftp://" + strServer;
            Uri UriServer = new Uri(strServer);
            switch (TypeCommand)
            {
                case 3:
                    UriServer = new Uri("ftp://" + targetPath);
                    break;
                case 7:
                    UriServer = new Uri("ftp://" + targetPath);
                    break;
            }

            if (UriServer.Scheme != Uri.UriSchemeFtp)
            {
                ErorDebag("URI = " + UriServer.ToString() + ", ошибка адресса FTP. Адресс: " + strServer, 1);
                return aresultFTP;
            }


            StringBuilder result_FTP = new StringBuilder();
            WebResponse response_FTP = null;                    // Ответ Сервера FTP
            StreamReader reader_FTP = null;                     // Чтения потока с FTP

            try
            {

                // Тест соединения с сервером TypeCommand -1 (проверка осуществдяется всегда перед началом работы)
                response_FTP = CreateFtpRequest(UriServer, NameUser, PasswdUser, WebRequestMethods.Ftp.PrintWorkingDirectory).GetResponse();
                FtpWebResponse rspFTP = response_FTP as FtpWebResponse;
                reader_FTP = new StreamReader(response_FTP.GetResponseStream());
                string str1 = rspFTP.StatusDescription.ToString();
                int CodOK = str1.IndexOf("257");
                ErorDebag("Результат проверки FTP: " + str1.ToString().Replace('\n', ' '), 1);


                //if (reader_FTP != null)
                //{
                //    reader_FTP.Close();
                //}
                //if (response_FTP != null)
                //{
                //    response_FTP.Close();
                //}

                // Метод
                switch (TypeCommand)
                {
                    case 2:
                        // 2 - Чтение файла с записью в папку Temp
                        /// --------- Не работает предположительно для текстового файла
                        // response_FTP = CreateFtpRequest(UriServer, NameUser, PasswdUser, WebRequestMethods.Ftp.DownloadFile).GetResponse();

                        //// Stream strm = response_FTP.GetResponseStream();

                        // reader_FTP = new StreamReader(response_FTP.GetResponseStream());
                        //  StreamWriter writer = new StreamWriter(PutchTMPFile, false); // Path.Combine(FolderToWriteFiles, fileInf.Name)
                        //  writer.Write(reader_FTP.ReadToEnd());
                        //  writer.Close();
                        /// --------- Не работает

                        // ----- Работает только для изображений
                        //using (response_FTP = CreateFtpRequest(UriServer, NameUser, PasswdUser, WebRequestMethods.Ftp.DownloadFile).GetResponse())
                        //using (var stream = response_FTP.GetResponseStream())
                        //using (var img = Image.FromStream(stream))
                        //{
                        //    img.Save(PutchTMPFile);
                        //}

                        // --------- Пример копирования файла через webFTP 

                        // Get the object used to communicate with the server.
                        
                        WebClient request = new WebClient();
                        try
                        {


                            // This example assumes the FTP site uses anonymous logon.
                            request.Credentials = new NetworkCredential(NameUser, PasswdUser);

                            byte[] newFileData = request.DownloadData(UriServer.ToString());
                            File.WriteAllBytes(PutchTMPFile, newFileData);

                            // Для текстового файла
                            //string fileString = System.Text.Encoding.UTF8.GetString(newFileData);
                            //File.WriteAllText(PutchTMPFile, fileString);

                            aresultFTP = new string[1] { PutchTMPFile };
                        }
                        catch (WebException Ex)
                        {
                            //Console.WriteLine(e.ToString());
                            ErorDebag("Cервер вернул отрицательный ответ: " + Ex + "." + " Связь с адресом: " + strServer + " разорвалась.", 1);
                        }
                        finally
                        {
                            request.Dispose();
                        }


                        // Проверку полученного файла переделать
                        //if (File_(PutchTMPFile, 5))
                        //{
                        
                        //}
                        // return aresultFTP; // конец
                        

                        break;
                    case 3:
                        // 3 - Чтение списки директорий и файлов
                        response_FTP = CreateFtpRequest(UriServer, NameUser, PasswdUser, WebRequestMethods.Ftp.ListDirectoryDetails).GetResponse();


                        ErorDebag("Результат чтения FTP", 1);
                        //reader_FTP = new StreamReader(response_FTP.GetResponseStream()); 
                        //string line = reader_FTP.ReadLine(); 
                        //while (line != null) { 
                        //    result_FTP.Append(line); 
                        //    result_FTP.Append("\n"); 
                        //    line = reader_FTP.ReadLine(); 
                        //} 
                        //result_FTP.Remove(result_FTP.ToString().LastIndexOf('\n'), 1);

                        //ErorDebag("Результат чтения FTP" + result_FTP.ToString(), 1);

                        // return result_FTP.ToString().Split('\n');
                        break;
                    case 7:
                        // Создать директорию
                        // CreateFtpRequest(new Uri("ftp://updatework.conecto.ua/updatework.conecto.ua/test"), NameUser, PasswdUser, WebRequestMethods.Ftp.MakeDirectory).GetResponse();
                        // CreateFtpRequest(new Uri("ftp://updatework.conecto.ua/updatework.conecto.ua/pack/"), NameUser, PasswdUser, WebRequestMethods.Ftp.ListDirectory).GetResponse();

                        // ftp = CreateFtpRequest(path, WebRequestMethods.Ftp.ListDirectory);

                        break;
                }




                //// Открытие потока
                //// Stream responseStream = rspFTP.GetResponseStream();
                //string str1 = rspFTP.StatusDescription.ToString(); // rspFTP.StatusCode = "PathnameCreated"
                //int CodOK = str1.IndexOf("257");
                //if (CodOK > -1)
                //{
                //    //  - Интернет безусловно есть!
                //    if (DebagApp)
                //    {
                //        ErorDebag("FTP = " + rspFTP.BannerMessage + " " + rspFTP.WelcomeMessage + " " + rspFTP.StatusDescription + " " + rspFTP.ExitMessage  +". -Связь с адресом: " + strServer + " установленна.", 1);
                //    }
                //    // Вернуть результат если только нужно проверить соединение с адресатом
                //    if (TypeCommand == 1)
                //    {
                //        rspFTP.Close();
                //        return true;
                //    }

                //} 
                //else
                //{
                //    // сервер вернул отрицательный ответ, возможно что инета нет
                //    rspFTP.Close();
                //    if (DebagApp)
                //    {
                //        ErorDebag("Cервер вернул отрицательный ответ: " + rspFTP.StatusDescription + "." + " Возможно, что связь с адресом: " + strServer + " отсутствует.", 1);
                //    }/**/
                //    return false;
                //}

                /*
                 // Скачинвание файла средствами FTP


               

             

                 // Пример копирования файла через webFTP (не работает если ограничены права на стороне сервера)

                 // Get the object used to communicate with the server.
                 WebClient request = new WebClient();

                 // This example assumes the FTP site uses anonymous logon.
                 request.Credentials = new NetworkCredential(NameUser, PasswdUser);
                 try
                 {
                     byte[] newFileData = request.DownloadData(UriServer.ToString());
                     string fileString = System.Text.Encoding.UTF8.GetString(newFileData);
                     File.WriteAllText(PutchTMPFile, fileString);

                     //Console.WriteLine(fileString);

                 }
                 catch (WebException Ex)
                 {
                     //Console.WriteLine(e.ToString());
                     if (DebagApp)
                     {
                         ErorDebag("Cервер вернул отрицательный ответ: " + Ex + "." + " Связь с адресом: " + strServer + " отсутствует.", 1);
                     }
                 } 
  */

                /*string ftpServerIP = FTPServer; 
string ftpUserID = FTPUser; 
string ftpPassword = FTPPwd; 
FileInfo fileInf = new FileInfo(FileName); 
string uri = "ftp://" + ftpServerIP + "/" + fileInf.Name; 
FtpWebRequest reqFTP = (FtpWebRequest)FtpWebRequest.Create(uri); //new Uri("ftp://" + ftpServerIP + DestinationFolder + fileInf.Name)); reqFTP.Credentials = new NetworkCredential(ftpUserID, ftpPassword); reqFTP.EnableSsl = true; reqFTP.KeepAlive = false; reqFTP.UseBinary = true; //reqFTP.UsePassive = true; reqFTP.Method = WebRequestMethods.Ftp.DownloadFile; ServicePointManager.ServerCertificateValidationCallback = new System.Net.Security.RemoteCertificateValidationCallback(AcceptAllCertifications); //Stream strm = reqFTP.GetRequestStream(); StreamReader reader = new StreamReader(reqFTP.GetResponse().GetResponseStream()); StreamWriter writer = new StreamWriter(Path.Combine(FolderToWriteFiles, FileName), false); writer.Write(reader.ReadToEnd()); return true;
*/
                //if (reader_FTP != null)
                //{
                //    reader_FTP.Close();
                //}
                //if (response_FTP != null)
                //{
                //    response_FTP.Close();
                //}

                //return aresultFTP;


            }
            catch (WebException ExNet)
            {
                // Запись последней ошибки FTP соединения (передача объекта в глобальную переменную)
                NetFTPResponse = ExNet.Response as FtpWebResponse;
                // Обрыв соединения
                if (NetFTPResponse == null)
                {
                    SystemConecto.ErorDebag("При обращении к FTP + {" + strServer + "}, произошел обрыв соединения: " + Environment.NewLine +
                    " === Message: " + ExNet.Message.ToString() + Environment.NewLine +
                    " === Exception: " + ExNet.ToString());


                }
                else
                {
                    SystemConecto.ErorDebag("При обращении к FTP + {" + strServer + "}, сервер вернул отрицательный ответ:  " + Environment.NewLine +
                   " === Message: " + ExNet.Message.ToString() + Environment.NewLine +
                   " === Exception: " + ExNet.ToString() + Environment.NewLine +
                   "Сервер вернул код ошибки: {idcodeftp=" + NetFTPResponse.StatusCode.ToString() + "}" + Environment.NewLine +
                   "Связь с адресом " + strServer + " прервана.");

                    NetFTPResponse.Close();
                }


                // return NetFTPResponse.StatusCode != FtpStatusCode.ActionNotTakenFileUnavailable;


            }
            finally
            {
                if (reader_FTP != null)
                {
                    reader_FTP.Close();
                }
                if (response_FTP != null)
                {
                    response_FTP.Close();
                }
            }

            return aresultFTP;
        }

        private static FtpWebRequest CreateFtpRequest(Uri Uripath, string NameUser, string PasswdUser, string method, string[] aParamFTP = null)
        {
            // Проверка параметров
            if (aParamFTP == null)
            {
                // KeepAlive - 0 | UseBinary - 1 | Timeout - 2 | UsePassive - 3
                aParamFTP = new string[6] { "false", "true", "3000", "true", null, "" };
            }
            try
            {
                var ftp = FtpWebRequest.Create(Uripath) as FtpWebRequest;
                // FtpWebRequest reqFTP = (FtpWebRequest)FtpWebRequest.Create(Uripath);
                // FtpWebRequest reqFTP = FtpWebRequest.Create(Uripath) as FtpWebRequest;
                // FtpWebRequest reqFTP = (FtpWebRequest)FtpWebRequest.Create(new Uri(Uripath)); //new Uri("ftp://172.29.200.158/")
                ftp.Credentials = new NetworkCredential(NameUser, PasswdUser);
                ftp.KeepAlive = Convert.ToBoolean(aParamFTP[0]);
                ftp.UseBinary = Convert.ToBoolean(aParamFTP[1]);
                ftp.Timeout = Convert.ToInt32(aParamFTP[2]);
                ftp.Proxy = null;
                // ftp.UsePassive = Convert.ToBoolean(aParamFTP[3]);
                // reqFTP.EnableSsl = true; 
                // ServicePointManager.ServerCertificateValidationCallback = new System.Net.Security.RemoteCertificateValidationCallback(AcceptAllCertifications); 
                ftp.Method = method;
                return ftp;
            }
            catch (WebException ExNet)
            {
                // Запись последней ошибки FTP соединения (передача объекта в глобальную переменную)
                NetFTPResponse = ExNet.Response as FtpWebResponse;
                NetFTPResponse.Close();
                // return resp.StatusCode != FtpStatusCode.ActionNotTakenFileUnavailable;
                return null;
            }

        }

        //private bool FtpDirectoryExists(string path)
        //{
        //    try
        //    {
        //        var ftp = CreateFtpRequest(path, WebRequestMethods.Ftp.ListDirectory);
        //        ftp.GetResponse().Close();
        //        return true;
        //    }
        //    catch (WebException exc)
        //    {
        //        var resp = exc.Response as FtpWebResponse;
        //        resp.Close();
        //        return resp.StatusCode != FtpStatusCode.ActionNotTakenFileUnavailable;
        //    }
        //}

        //private void btnGo_Click(object sender, EventArgs e)
        //{
        //    string[] files = GetFileList();
        //    foreach (string file in files)
        //    {
        //        if (file.Length >= 5)
        //        {
        //            string uri = "ftp://" + ftpServerIP + "/" + remoteDirectory + "/" + file;
        //            Uri serverUri = new Uri(uri);
        //            CheckFile(file);
        //        }
        //    }
        //    this.Close();
        //}

        //private void CheckFile(string file) 
        //{ 
        //    string dFile = file; 
        //    string[] splitDownloadFile = Regex.Split(dFile, " "); 
        //    string fSize = splitDownloadFile[13]; 
        //    string fMonth = splitDownloadFile[14]; 
        //    string fDate = splitDownloadFile[15]; 
        //    string fTime = splitDownloadFile[16]; 
        //    string fName = splitDownloadFile[17]; 
        //    string dateModified = fDate + "/" + fMonth+ "/" + fYear; 
        //    DateTime lastModifiedDF = Convert.ToDateTime(dateModified); 
        //    string[] filePaths = Directory.GetFiles(localDirectory); 
        //    // if there is a file in filePaths that is the same as on the server compare them and then download 
        //    if file on server is newer foreach (string ff in filePaths)
        //    { 
        //        string[] splitFile = Regex.Split(ff, @"\\"); 
        //        string fileName = splitFile[2]; 
        //        FileInfo fouFile = new FileInfo(ff); 
        //        DateTime lastChangedFF = fouFile.LastAccessTime; 
        //        if (lastModifiedDF > lastChangedFF) Download(fileName); 
        //    } 
        // }
        //public string[] GetFileList() { 
        //    string[] downloadFiles; 
        //    StringBuilder result = new StringBuilder();
        //    WebResponse response = null; 
        //    StreamReader reader = null; 
        //    try { FtpWebRequest reqFTP = (FtpWebRequest)FtpWebRequest.Create(new Uri("ftp://" + ftpServerIP + "/" + remoteDirectory)); 
        //        reqFTP.UseBinary = true; 
        //        reqFTP.Credentials = new NetworkCredential(ftpUserID, ftpPassword); 
        //        reqFTP.Method = WebRequestMethods.Ftp.ListDirectoryDetails; 
        //        reqFTP.Proxy = null; 
        //        reqFTP.KeepAlive = false;
        //        reqFTP.UsePassive = false;
        //        response = reqFTP.GetResponse(); 
        //        reader = new StreamReader(response.GetResponseStream()); 
        //        string line = reader.ReadLine(); 
        //        while (line != null) { 
        //            result.Append(line); 
        //            result.Append("\n"); 
        //            line = reader.ReadLine(); 
        //        } result.Remove(result.ToString().LastIndexOf('\n'), 1);
        //        return result.ToString().Split('\n'); 
        //    } 
        //    catch 
        //    { 
        //        if (reader != null) 
        //        { 
        //            reader.Close(); 
        //        } 
        //        if (response != null) 
        //        { 
        //            response.Close(); 
        //        } 
        //        downloadFiles = null; 
        //        return downloadFiles; 
        //    }
        //}

        // Проверка значений реестра если ключа нет, тут будет null – значит приложение не установлено

        #endregion



    }

    public partial class SystemConectoNetwork
    {




    }

}