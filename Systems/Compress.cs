using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
// Управление вводом-выводом
using System.IO;
using System.Text;
// Сжатие
using System.IO.Compression;
// шифрование данных
using System.Security.Cryptography;

using System.Runtime.InteropServices;
// ==================================== Используем функции ядра SystemConecto


namespace ConectoWorkSpace
{
    #region Сжатие данных   Разработка!
    class Compress
    {

        #region Пример использования
        // Архивирование
        // Упаковать файлы в архив test.zip из директории DBsevsa
        //Compress.AddDirectory(@"D:\!Project\Temp\DBsevsa");
        //Compress.ToCompressZip(@"D:\!Project\Temp\1\test.zip");

        // Извлечь файлы из архива 26_sevas09.rar в Директорию D:\!Project\Temp\2
        // Compress.ToDecompressFile(@"D:\!Project\Temp\26_sevas09.rar", @"D:\!Project\Temp\2");
     
        // Compress.AddCompressDirectory(@"D:\!Project\Temp\DBsevsa\1");

        #endregion
        /// <summary>
        /// коментарий при жатии в архивном файле.
        /// </summary>
        public static string Comment_ { get; set; }

        /// <summary>
        /// Путь к последнему упакованому архиву (с названием файла)
        /// </summary>
        public static FileInfo PuthFileComp_ { get; set; }

        /// <summary>
        /// Список путей файлов сжатия AddFile
        /// </summary>
        private static List<string> LFiles_ = new List<string>();
        //  LFiles.Add("C:\readme.txt");

        /// <summary>
        /// Список путей сжатых файлов CompressFile, для распоковки
        /// </summary>
        private static List<string> LFilesComp_ = new List<string>();
        //  LFiles.Add("C:\readme.txt.gz");


        /// установка уровня сжатия CompressionLevel
        /// // устанавливаем пароль к архиву Password

        /// <summary>
        /// Формирует список файлов для архирования
        /// </summary>
        /// <param name="Putch">путь к файлу для сжатия</param>
        public static void AddFile(string Puth)
        {
            // Проверка файла на существование используем класс SystemConecto
            
            LFiles_.Add(Puth);
            // Удаляем по индексу строку "C:\readme.txt".
            // LFiles_.RemoveAt(0);
            // Перебор масива
            // foreach (var item in LFiles_) { }

        }

        /// <summary>
        /// Формирует список файлов для извлечения
        /// </summary>
        /// <param name="Putch">путь к сжатому файлу</param>
        public static void AddCompressFile(string Puth)
        {
            // Проверка файла на существование используем класс SystemConecto

            LFilesComp_.Add(Puth);
            // Удаляем по индексу строку "C:\readme.txt".
            // LFilesComp_.RemoveAt(0);
            // Перебор масива
            // foreach (var item in LFilesComp_) { }

        }



        /// <summary>
        /// Формирует список из файлов в директории исключая все поддиректории
        /// </summary>
        /// <param name="Putch">Путь сжимаемой директории</param>
        public static void AddDirectory(string directoryPath)
        {

            DirectoryInfo directorySelected = new DirectoryInfo(directoryPath);
            // Только файлы
            foreach (FileInfo fileToCompress in directorySelected.GetFiles())
            {
                

                // Уточнение пути 
                // Предотвращение сжатия уже сжатых файлов.
                if (fileToCompress.Extension != ".gz")
                {
                    LFiles_.Add(fileToCompress.FullName);
                
                }
                // Предотвращение сжатия скрытых и уже сжатых файлов.  
                //if ((File.GetAttributes(fileToCompress.FullName)
                //    & FileAttributes.Hidden)
                //    != FileAttributes.Hidden & fileToCompress.Extension != ".gz")
                //{
                //}
            }

        }

        /// <summary>
        /// Формирует список из упакованных файлов в директории исключая все поддиректории
        /// </summary>
        /// <param name="Putch">Путь директории в которой лежат упакованные файлы</param>
        /// <param name="ExteCompress">Расширение типа сжатия, по умолчанию - пток .gz</param>
        public static void AddCompressDirectory(string directoryPath, string ExteCompress = ".gz")
        {

            DirectoryInfo directorySelected = new DirectoryInfo(directoryPath);
            // Только файлы
            foreach (FileInfo fileToCompress in directorySelected.GetFiles())
            {

                // Уточнение пути 
                // Определения сжатых файлов.
                if (fileToCompress.Extension == ExteCompress)
                {
                    LFilesComp_.Add(fileToCompress.FullName);

                }
            }

        }



        /// <summary>
        /// Упаковка файлов в потоке
        /// </summary>
        /// <param name="NameFileCom">Имя файла сжатого файла</param>
        /// <param name="TypeCompress">тип компресии: Def- по умалчанию компресия файлов</param>
        public static void ToCompressStrem(string DirectoryFileCom = "", string TypeCompress = "Def")   // FileInfo fi
        {
            var PathFileCom = "";
            // Читаем список файлов
            foreach (var item in LFiles_)
            {
                FileInfo Comfile = new FileInfo(item);
                // Проверка названия сжимаемого файла
                if (DirectoryFileCom == "")
                {
                    PathFileCom = Comfile.FullName + ".gz";
                }
                else
                {
                    // Положить в другую директорию
                    // Проверка директории используем класс SystemConecto

                    PathFileCom = DirectoryFileCom  + @"\" + Comfile.Name + ".gz";
                }


                // Ошибки файловой структуры
                try
                {
              
                    // Получить поток исходного файла.
                    using (FileStream inFile = Comfile.OpenRead())
                    {

                        // cоздать сжатый файл.
                        using (FileStream outFile = File.Create(PathFileCom))
                            {
                                // записать в файл
                                using (GZipStream Compress = new GZipStream(outFile, CompressionMode.Compress))
                                {
                                    // Скопируйте исходный файл в поток сжатия.
                                    inFile.CopyTo(Compress);
                                }
                            }
                        // Последний упакованный
                        PuthFileComp_ = new FileInfo(PathFileCom);
                    }
                }
                catch (Exception Ex) 
                {
                    // Ошибка, значит интернета у нас нет. Плачем :'(
                    if (SystemConecto.DebagApp)
                    {
                        // Тут нужна дополнительная аналитика например ошибка 403 тоже попадает сюда.
                        SystemConecto.ErorDebag("Ошибка сжатия файла: " + Ex + "." + " Путь к файлу: [" + Comfile.FullName + "]", 1);
                    }
                }
           }
            
        }


        /// <summary>
        /// Распоковка файла в потоке
        /// </summary>
        /// <param name="PuthFile">Путь к сжатому файлу</param>
        /// <param name="PuthFileTo">Путь распоковки</param>
        /// <param name="EncryptTextToFile">Тип шифрование и дешифрование файла</param>
        public static void ToDecompressStrem(string PuthFile="", string PuthFileTo ="", int TypeEncryptDecriptTextToFile = 0)
        {

            if (PuthFile == "")
            {
                 // Последний упакованный файл (как альтернатива пути) LFilesComp_.Count == 0
                if (PuthFileComp_.ToString().Length > 0){
                    Compress.AddCompressFile(PuthFileComp_.ToString());
                    // FileInfo fi = PuthFileComp_;
                }
                else
                {
                    return;
                }

            }else{
                // Проверка директории используем класс SystemConecto
                Compress.AddCompressFile(PuthFile);
            }

          

            // Читаем список файлов
            foreach (var item in LFilesComp_)
            {

                FileStream inFile = null;
                //Создать распаковать файл.
                try{
                    
                    FileInfo fi = new FileInfo(item);
                    
                    // Получить поток исходного файла.
                    inFile = fi.OpenRead();
                    //using (inFile = fi.OpenRead())
                    //{

                        // Оригинальное расширение файла, например,
                        // "doc" из report.doc.gz.
                        string curFile = fi.FullName;
                        string origName = curFile.Remove(curFile.Length - fi.Extension.Length);
                        // Путь размещения распаковки
                        if (PuthFileTo != "")
                        {
                            // Проверка директории используем класс SystemConecto
                            var FiorigName = new FileInfo(origName);
                            origName = PuthFileTo + @"\" + FiorigName.Name;
                        }
                        
                        switch(TypeEncryptDecriptTextToFile){
                            case 1: 
                                    // Шифрование данных полученных из упакованого файла (без шифрования)
                                    using (MemoryStream outFile = new MemoryStream())
                                    {
                                        using (GZipStream Decompress = new GZipStream(inFile, CompressionMode.Decompress))
                                        {
                                            // Декомпрессия потока в выходной файл.
                                            Decompress.CopyTo(outFile);
                                        }
                                        // Шифруем
                                        SystemConecto.EncryptTextToFile(origName, outFile.ToString());
                                        outFile.Dispose();
                                    }
                                    
                                break;
                            default:
                                using (FileStream outFile = File.Create(origName))
                                {
                                    using (GZipStream Decompress = new GZipStream(inFile, CompressionMode.Decompress))
                                    {
                                        // Декомпрессия потока в выходной файл.
                                        Decompress.CopyTo(outFile);
                                    }
                                }
                                break;
                        }

                    //}

                }
                finally
                {
                    if (inFile != null)
                        inFile.Dispose();
                }

            }
        }


        /// <summary>
        /// Упаковка файлов zip (на включена в версию Framework 4.0)
        /// </summary>
        /// <param name="NameFileCom">Имя файла сжатого файла</param>
        /// <param name="TypeCompress">тип компресии: Def- по умалчанию компресия файлов</param>
        public static void ToCompressZip(string PathFileCom = "", string TypeCompress = "Def")   // FileInfo fi
        {

            // Проверка соответсвия переменной PathFileCom используем класс SystemConecto

            // Окрыть сжимаемы й файл и наполнить его сфайлами
            // Определяем имя архива
            // Читаем список файлов
            foreach (var item in LFiles_)
            {
                FileInfo Comfile = new FileInfo(item);
                // Проверка названия сжимаемого файла
                if (PathFileCom == "")
                {
                    PathFileCom = NameFileCompress(Comfile.DirectoryName.ToString());
                    // Нет пути выход
                    if (PathFileCom == "")
                    {
                        return;
                    }
                }
                break;
            }
            // Ошибки файловой структуры
            try
            {
                // cоздать сжатый файл.
                using (FileStream zipToOpen = new FileStream(PathFileCom, FileMode.OpenOrCreate))
                // using (FileStream outFile = File.Create(PathFileCom))
                {
                    // Только для Framework 4.5
                    // Открыть сжатый файл для обновления данными (записать в файл)
                    //using (var archive = new ZipArchive(zipToOpen, ZipArchiveMode.Update))
                    //// using (GZipStream Compress = new GZipStream(outFile, CompressionMode.Compress))
                    //{
                    //    // Упаковка файлов

                    //    // Читаем список файлов
                    //    foreach (var item in LFiles_)
                    //    {

                    //        // Ошибки файловой структуры
                    //        try
                    //        {
                    //            var Comfile = new FileInfo(item);
                    //            var item_com = item;
                    //            if (TypeCompress == "Def")
                    //            {
                    //                item_com = Comfile.Name;
                    //            }

                    //            // ==== Разработка потокового переноса файлов в архив
                                
                    //            //ZipArchiveEntry fileEntry = archive.CreateEntry(item_com);

                    //            //// Получить поток исходного файла.  - using (FileStream inFile = Comfile.OpenRead())
                    //            //using (StreamReader inFile = new StreamReader(item))
                    //            //{
                    //            //    var File = inFile.ReadToEnd(); // Есть сомнения по поводу памяти Компьютера и буфера
                    //            //    // Запись данных
                    //            //    using (StreamWriter writer = new StreamWriter(fileEntry.Open()))
                    //            //    {
                    //            //        writer.Write(File); // fileEntry.Open()
                    //            //    }
                    //            //}

                    //            //============== Для ленивых  - нужна библиотека - Microsoft.NET\Framework\v4.0.30319\System.IO.Compression.FileSystem.dll
                    //            archive.CreateEntryFromFile(item, item_com); 


                    //            // ============== Можно использовать для создания текстовых файлов
                    //            // c необходимыми записями внем.

                    //            // Создать пустой файл
                    //            // ZipArchiveEntry readmeEntry = archive.CreateEntry(item_com);
                        
                    //            // Получить поток исходного файла.  - using (FileStream inFile = Comfile.OpenRead())
                    //            //using (StreamWriter writer = new StreamWriter(readmeEntry.Open()))
                    //            //{
                    //            //    writer.WriteLine("Information about this package.");
                    //            //    writer.WriteLine("========================");
                    //            //}

                                

                    //            // Последний упакованный
                    //            PuthFileComp_ = new FileInfo(PathFileCom);

                    //        }
                    //        catch (Exception Ex)
                    //        {
                    //            // Ошибка, значит интернета у нас нет. Плачем :'(
                    //            if (SystemConecto.DebagApp)
                    //            {
                    //                // Тут нужна дополнительная аналитика например ошибка 403 тоже попадает сюда.
                    //                SystemConecto.ErorDebag("Ошибка сжатия файла: " + Ex + "." + " Путь к файлу: [" + item + "]", 1);
                    //            }
                    //        }
                    //    }
                    //}
                }

            }
            catch (Exception ExArh)
            {
                // Ошибка, значит интернета у нас нет. Плачем :'(
                if (SystemConecto.DebagApp)
                {
                    // Тут нужна дополнительная аналитика например ошибка 403 тоже попадает сюда.
                    SystemConecto.ErorDebag("Ошибка файла сжатия : " + ExArh + "." + " Путь к файлу: [" + PathFileCom + "]", 1);
                }
            }

        }


        /// <summary>
        /// Распоковка файла из архивов
        /// </summary>
        /// <param name="PuthFile">Путь к сжатому файлу</param>
        /// <param name="PuthFileTo">Путь распоковки</param>
        public static void ToDecompressFile(string PuthFile = "", string PuthFileTo = "")
        {

            if (PuthFile != "")
            {
                // Проверка директории используем класс SystemConecto
                Compress.AddCompressFile(PuthFile);
            }

            // Последний упакованный файл (как альтернатива пути)
            if (LFilesComp_.Count == 0)
            {
                Compress.AddCompressFile(PuthFileComp_.ToString());
                // FileInfo fi = PuthFileComp_;
            }

            // Читаем список файлов
            foreach (var item in LFilesComp_)
            {

                FileInfo fi = new FileInfo(item);
                var origNameExt = "";
                // Путь размещения распаковки
                if (PuthFileTo != "")
                {
                    // Проверка директории используем класс SystemConecto + @"\"
                    origNameExt = PuthFileTo;
                }
                else
                {
                    // Путь архива по умолчанию и он всегда есть
                    origNameExt = fi.DirectoryName.ToString();

                }


                try
                {
                    // Проверка типа файла
                    switch (fi.Extension)
                    {
                        case ".zip":
                                //using (ZipArchive archive = ZipFile.OpenRead(item))
                                //{
                                //    foreach (ZipArchiveEntry entry in archive.Entries)
                                //    {
                                //        entry.ExtractToFile(Path.Combine(origNameExt, entry.FullName));

                                //        // ================= Разархивирование по условию
                                //        //if (entry.FullName.EndsWith(".txt", StringComparison.OrdinalIgnoreCase))
                                //        //{
                                //        //    entry.ExtractToFile(Path.Combine(extractPath, entry.FullName));
                                //        //}
                                //    }
                                //}
                            break;
                        case ".rar":

                            // Создайте новый класс Unrar и приложить обработчиков событий
                            // Прогресс, недостающие объемы и пароль
                            var unrar=new Unrar();
                            //unrar.ExtractionProgress+=new ExtractionProgressHandler(unrar_ExtractionProgress);
                            //unrar.MissingVolume+=new MissingVolumeHandler(unrar_MissingVolume);
                            //unrar.PasswordRequired+=new PasswordRequiredHandler(unrar_PasswordRequired);
                            
                            // Установить путь назначения для всех файлов
                            unrar.DestinationPath = origNameExt;

                            // Открыть архив для извлечения
                            unrar.Open(item, Unrar.OpenMode.Extract);

                            // Извлечение каждого найденного файла в хэш-таблице
                            while (unrar.ReadHeader())
                            {
                                unrar.Extract();
                                // ================= Разархивирование по условию
                                //if (selectedFiles.ContainsKey(unrar.CurrentFile.FileName))
                                //{
                                //    unrar.Extract();
                                //}
                                //else
                                //{
                                //    unrar.Skip();
                                //}
                            }
                            // SystemConecto.ErorDebag("Отладка: " + item, 1);

                            break;
                    }

                }
                catch (Exception ExArh)
                {
                    // Ошибка, значит интернета у нас нет. Плачем :'(
                    if (SystemConecto.DebagApp)
                    {
                        // Тут нужна дополнительная аналитика например ошибка 403 тоже попадает сюда.
                        SystemConecto.ErorDebag("Ошибка распоковки файла : " + ExArh + "." + " Путь к файлу: [" + item + "]", 1);
                    }
                }

            }
        }

        /// <summary>
        /// Формирования название файла сжатия
        /// </summary>
        /// <returns>возвращает название сжатого файла</returns>
        private static string NameFileCompress(string Name)
        {

            DirectoryInfo directorySelected = new DirectoryInfo(Name);
            return Name + @"\" + directorySelected.Name.ToString() + ".zip";
        }



        /// <summary>
        /// Шифрует и сжимает вход. Возвращает сжатый и зашифрованный контент и ключ и IV вектора используется.
        /// </summary>
        /// <param name="input">Массив данных</param>
        /// <param name="key">Ключ, который был использован в алгоритме.</param>
        /// <param name="iv">Вектор IV, который был использован в алгоритме.</param>
        /// <returns></returns>
        public byte[] EncryptAndCompress(byte[] input, out byte[] key, out byte[] iv)
        {
            if (input == null)
                throw new ArgumentNullException("input");

            // Сжатие массива данных в данный поток памяти.
            MemoryStream stream = new MemoryStream();
            using (GZipStream zip = new GZipStream(stream, CompressionMode.Compress, true))
            {
                // Запись данных в поток памяти с помощью ZIP потока.
                zip.Write(input, 0, input.Length);
            }

            // Создание ключей и инициализировать the rijndael.
            RijndaelManaged r = new RijndaelManaged();
            r.GenerateKey();
            r.GenerateIV();
            // Установить сгенерированный ключ и IV вектор.
            key = r.Key;
            iv = r.IV;

            // Шифрование сжатого потока памяти в зашифрованный поток памяти.
            MemoryStream encrypted = new MemoryStream();
            using (CryptoStream cryptor = new CryptoStream(encrypted, r.CreateEncryptor(), CryptoStreamMode.Write))
            {
                // Записать поток в зашифрованный поток памяти.
                cryptor.Write(stream.ToArray(), 0, (int)stream.Length);
                cryptor.FlushFinalBlock();
                // Вернуть результат
                return encrypted.ToArray();
            }
        }

        /// <summary>
        /// Decrypts and decompresses the input. Returns the decompressed and decrypted content.
        /// </summary>
        /// <param name="input">The input array.</param>
        /// <param name="key">The key used for decrypt.</param>
        /// <param name="iv">The iv vector used for decrypt.</param>
        /// <returns></returns>
        public byte[] DecryptAndDecompress(byte[] input, byte[] key, byte[] iv)
        {
            if (input == null)
                throw new ArgumentNullException("input");
            if (key == null)
                throw new ArgumentNullException("key");
            if (iv == null)
                throw new ArgumentNullException("iv");

            // Initialize the rijndael
            RijndaelManaged r = new RijndaelManaged();
            // Create the array that holds the result.
            byte[] decrypted = new byte[input.Length];
            // Create the crypto stream that is used for decrypt. The first argument holds the input as memory stream.
            using (CryptoStream decryptor = new CryptoStream(new MemoryStream(input), r.CreateDecryptor(key, iv), CryptoStreamMode.Read))
            {
                // Read the encrypted values into the decrypted stream. Decrypts the content.
                decryptor.Read(decrypted, 0, decrypted.Length);
            }

            // Создать сжатый поток для распаковки.
            using (GZipStream zip = new GZipStream(new MemoryStream(decrypted), CompressionMode.Decompress, false))
            {
                // Читать все байты в сжатый поток и вернуть их.
                return ReadAllBytes(zip);
            }
        }

        /// <summary>
        /// Читает все байты в данном сжатом потоке и возвращает их.
        /// </summary>
        /// <param name="zip">Сжатый поток, который обрабатывается.</param>
        /// <returns></returns>
        private byte[] ReadAllBytes(GZipStream zip)
        {
            if (zip == null)
                throw new ArgumentNullException("zip");

            int buffersize = 100;
            byte[] buffer = new byte[buffersize];
            int offset = 0, read = 0, size = 0;
            do
            {
                // Если буфер не предлагает достаточно места, создаем новый массив двойного размера
                // Скопируем текущее содержание буфера в этот массив и используем его как новый буфер
                if (buffer.Length < size + buffersize)
                {
                    byte[] tmp = new byte[buffer.Length * 2];
                    Array.Copy(buffer, tmp, buffer.Length);
                    buffer = tmp;
                }

                // Читает число распакованных данных.
                read = zip.Read(buffer, offset, buffersize);

                // Прирост смещение на размер прочитаного.
                offset += buffersize;
                size += read;
            } while (read == buffersize); // Прекращается, если мы читаем меньше, чем размер буфера.

            // Копируем только то, количество данных, которое на самом деле было прочитано
            byte[] result = new byte[size];
            Array.Copy(buffer, result, size);
            return result;
        }
    }
    /*
     * Пример использования
     * byte[] key = null;
     * byte[] iv = null;
     * byte[] input = File.ReadAllBytes(“C:\\Users\\Chris\\Documents\\UNI\\Untitled.bmp”);
     * byte[] result = EncryptAndCompressUtility.EncryptAndCompress(input, out key, out iv);
     * File.WriteAllBytes(“C:\\Users\\Chris\\Documents\\UNI\\foo compressed.bmp”, result);
     * byte[] result2 = EncryptAndCompressUtility.DecryptAndDecompress(File.ReadAllBytes(“C:\\Users\\Chris\\Documents\\UNI\\foo compressed.bmp”), key, iv);
     * File.WriteAllBytes(“C:\\Users\\Chris\\Documents\\UNI\\foo.bmp”, result2);
    */

    /* -------------------------------- Изучение других примеров
     * 1. http://msdn.microsoft.com/en-us/library/system.io.compression.gzipstream.aspx
     * 2. http://7-zip.org.ua/ru/sdk.html    (http://sevenzipsharp.codeplex.com/)
     * 5. 
     * 4. using System.IO;  
            using System.IO.Compression;  
            using(FileStream sourceFile = File.OpenRead(@"D:\MyFile.xls"))  
            using(FileStream targetFile = File.Create(@"D:\MyFile.zip"))  
            using (GZipStream gzipStream = new GZipStream(targetFile, CompressionMode.Compress, false))  
            {  
                 try  
                 {  
                      int posByte = sourceFile.ReadByte();  
                      
                      while (posByte != -1)  
                      {  
                           gzipStream.WriteByte((byte)posByte);  
                           posByte = sourceFile.ReadByte();  
                      }  
                 }  
                 catch  
                 {  
                      //  
                 }  
             }   
     * 
     * 
     * 
     * 
     * 
     * 
     * 
     */


    // =================================== Дополнения к файловым манипуляциям
    // Наследник класса FileStream класс IsolatedStorageFileSystem (пространство имен System.IO.IsolatedStorage) дополняет класс FileStream методами создания, чтения и записи файлов в изолированном хранилище - тоесть файлы в виртуальной файловой системе, в которой данные недоступны извне (изоляция данных на уровне пользователя - сборки или домена приложения). 


    // ============================ UNrar Дополнение от 
    /*  Author:  Michael A. McCloskey
     *  Company: Schematrix
     *  Version: 20040714
     *  
     *  Personal Comments:
     *  I created this unrar wrapper class for personal use 
     *  after running into a number of issues trying to use
     *  another COM unrar product via COM interop.  I hope it 
     *  proves as useful to you as it has to me and saves you
     *  some time in building your own products.
     */

        //Я создал этот Unrar класс-оболочку для личного пользования
        //После запуска на ряд вопросов, пытаются использовать
        //Другой продукт COM Unrar через COM-взаимодействия. Я надеюсь, что
        //Доказывает, как полезно для вас, как это имеет ко мне и экономит
    //Некоторое время в строительстве собственных продуктов.







    #region Определение делегата события

    /// <summary>
    /// Представляет метод, который будет обрабатывать данные события
    /// </summary>
    public delegate void DataAvailableHandler(object sender, DataAvailableEventArgs e);
    /// <summary>
    /// Представляет метод, который будет обрабатывать события добычи прогресса
    /// </summary>
    public delegate void ExtractionProgressHandler(object sender, ExtractionProgressEventArgs e);
    /// <summary>
    /// Представляет метод, который будет обрабатывать события отсутствуют тома архива
    /// </summary>
    public delegate void MissingVolumeHandler(object sender, MissingVolumeEventArgs e);
    /// <summary>
    /// Представляет метод, который будет работать с новыми событиями объеме
    /// </summary>
    public delegate void NewVolumeHandler(object sender, NewVolumeEventArgs e);
    /// <summary>
    /// Представляет метод, который будет обрабатывать уведомления о новых файлов
    /// </summary>
    public delegate void NewFileHandler(object sender, NewFileEventArgs e);
    /// <summary>
    /// Представляет метод, который будет обрабатывать пароль необходимые события
    /// </summary>
    public delegate void PasswordRequiredHandler(object sender, PasswordRequiredEventArgs e);

    #endregion

    /// <summary>
    /// Способ открытия внешних библиотек для обеспечения надежности работы приложения<para></para>
    /// http://msdn.microsoft.com/ru-ru/library/ms182161.aspx
    /// </summary>
    internal static class NativeMethods
    {
        #region Unrar декларации функции

        [DllImport("unrar.dll")]
        public static extern IntPtr RAROpenArchive(ref Unrar.RAROpenArchiveData archiveData);

        [DllImport("UNRAR.DLL")]
        public static extern IntPtr RAROpenArchiveEx(ref Unrar.RAROpenArchiveDataEx archiveData);

        [DllImport("unrar.dll")]
        public static extern int RARCloseArchive(IntPtr hArcData);

        [DllImport("unrar.dll")]
        public static extern int RARReadHeader(IntPtr hArcData, ref Unrar.RARHeaderData headerData);

        [DllImport("unrar.dll")]
        public static extern int RARReadHeaderEx(IntPtr hArcData, ref Unrar.RARHeaderDataEx headerData);

        [DllImport("unrar.dll")]
        public static extern int RARProcessFile(IntPtr hArcData, int operation,
            [MarshalAs(UnmanagedType.LPStr)] string destPath,
            [MarshalAs(UnmanagedType.LPStr)] string destName);

        [DllImport("unrar.dll")]
        public static extern void RARSetCallback(IntPtr hArcData, Unrar.UNRARCallback callback, int userData);

        [DllImport("unrar.dll")]
        public static extern void RARSetPassword(IntPtr hArcData,
            [MarshalAs(UnmanagedType.LPStr)] string password);


        #endregion
    }

    /// <summary>
    /// Класс-оболочку для Unrar DLL поставляется RARSoft.
    /// Вызовы Unrar DLL с помощью платформы вызова службы (PInvoke).
    /// DLL доступна на http://www.rarlab.com/rar_add.htm (2012 году)
    /// IDisposable - Определяет методы высвобождения распределенных ресурсов. (На изучение)
    /// </summary>
    public class Unrar : IDisposable
    {


        // Unrar делегат обратного вызова подпись
        public delegate int UNRARCallback(uint msg, int UserData, IntPtr p1, int p2);
        
        #region Перечисления Unrar DLL

        /// <summary>
        /// Режим, в котором архив должн быть открыт для обработки.
        /// </summary>
        public enum OpenMode
        {
            /// <summary>
            /// Открыть архив для включения в перечень содержания только
            /// </summary>
            List = 0,
            /// <summary>
            /// Открыть архив для тестирования или извлечения содержимого
            /// </summary>
            Extract = 1
        }

        private enum RarError : uint
        {
            EndOfArchive = 10,
            InsufficientMemory = 11,
            BadData = 12,
            BadArchive = 13,
            UnknownFormat = 14,
            OpenError = 15,
            CreateError = 16,
            CloseError = 17,
            ReadError = 18,
            WriteError = 19,
            BufferTooSmall = 20,
            UnknownError = 21
        }

        private enum Operation : uint
        {
            Skip = 0,
            Test = 1,
            Extract = 2
        }

        private enum VolumeMessage : uint
        {
            Ask = 0,
            Notify = 1
        }

        [Flags]
        private enum ArchiveFlags : uint
        {
            Volume = 0x1,										// Volume attribute (archive volume)
            CommentPresent = 0x2,						// Archive comment present
            Lock = 0x4,											// Archive lock attribute
            SolidArchive = 0x8,							// Solid attribute (solid archive)
            NewNamingScheme = 0x10,					// New volume naming scheme ('volname.partN.rar')
            AuthenticityPresent = 0x20,			// Authenticity information present
            RecoveryRecordPresent = 0x40,		// Recovery record present
            EncryptedHeaders = 0x80,				// Block headers are encrypted
            FirstVolume = 0x100							// 0x0100  - First volume (set only by RAR 3.0 and later)
        }

        private enum CallbackMessages : uint
        {
            VolumeChange = 0,
            ProcessData = 1,
            NeedPassword = 2
        }

        #endregion

        #region Unrar определения структуры DLL

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
        public struct RARHeaderData
        {
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 260)]
            public string ArcName;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 260)]
            public string FileName;
            public uint Flags;
            public uint PackSize;
            public uint UnpSize;
            public uint HostOS;
            public uint FileCRC;
            public uint FileTime;
            public uint UnpVer;
            public uint Method;
            public uint FileAttr;
            [MarshalAs(UnmanagedType.LPStr)]
            public string CmtBuf;
            public uint CmtBufSize;
            public uint CmtSize;
            public uint CmtState;

            public void Initialize()
            {
                this.CmtBuf = new string((char)0, 65536);
                this.CmtBufSize = 65536;
            }
        }

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
        public struct RARHeaderDataEx
        {
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 512)]
            public string ArcName;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 1024)]
            public string ArcNameW;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 512)]
            public string FileName;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 1024)]
            public string FileNameW;
            public uint Flags;
            public uint PackSize;
            public uint PackSizeHigh;
            public uint UnpSize;
            public uint UnpSizeHigh;
            public uint HostOS;
            public uint FileCRC;
            public uint FileTime;
            public uint UnpVer;
            public uint Method;
            public uint FileAttr;
            [MarshalAs(UnmanagedType.LPStr)]
            public string CmtBuf;
            public uint CmtBufSize;
            public uint CmtSize;
            public uint CmtState;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1024)]
            public uint[] Reserved;

            public void Initialize()
            {
                this.CmtBuf = new string((char)0, 65536);
                this.CmtBufSize = 65536;
            }
        }

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
        public struct RAROpenArchiveData
        {
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 260)]
            public string ArcName;
            public uint OpenMode;
            public uint OpenResult;
            [MarshalAs(UnmanagedType.LPStr)]
            public string CmtBuf;
            public uint CmtBufSize;
            public uint CmtSize;
            public uint CmtState;

            public void Initialize()
            {
                this.CmtBuf = new string((char)0, 65536);
                this.CmtBufSize = 65536;
            }
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct RAROpenArchiveDataEx
        {
            [MarshalAs(UnmanagedType.LPStr)]
            public string ArcName;
            [MarshalAs(UnmanagedType.LPWStr)]
            public string ArcNameW;
            public uint OpenMode;
            public uint OpenResult;
            [MarshalAs(UnmanagedType.LPStr)]
            public string CmtBuf;
            public uint CmtBufSize;
            public uint CmtSize;
            public uint CmtState;
            public uint Flags;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 32)]
            public uint[] Reserved;

            public void Initialize()
            {
                this.CmtBuf = new string((char)0, 65536);
                this.CmtBufSize = 65536;
                this.Reserved = new uint[32];
            }
        }

        #endregion

       

        #region Общии декларации событий

        /// <summary>
        /// Event that is raised when a new chunk of data has been extracted
        /// </summary>
        public event DataAvailableHandler DataAvailable;
        /// <summary>
        /// Событие, которое возникает, чтобы указать прогресс распаковки
        /// </summary>
        public event ExtractionProgressHandler ExtractionProgress;
        /// <summary>
        /// Event that is raised when a required archive volume is missing
        /// </summary>
        public event MissingVolumeHandler MissingVolume;
        /// <summary>
        /// Event that is raised when a new file is encountered during processing
        /// </summary>
        public event NewFileHandler NewFile;
        /// <summary>
        /// Event that is raised when a new archive volume is opened for processing
        /// </summary>
        public event NewVolumeHandler NewVolume;
        /// <summary>
        /// Event that is raised when a password is required before continuing
        /// </summary>
        public event PasswordRequiredHandler PasswordRequired;

        #endregion

        #region Часные поля

        private string archivePathName = string.Empty;
        private IntPtr archiveHandle = new IntPtr(0);
        private bool retrieveComment = true;
        private string password = string.Empty;
        private string comment = string.Empty;
        private ArchiveFlags archiveFlags = 0;
        private RARHeaderDataEx header = new RARHeaderDataEx();
        private string destinationPath = string.Empty;
        private RARFileInfo currentFile = null;
        private UNRARCallback callback = null;

        #endregion

        #region Object lifetime procedures

        public Unrar()
        {
            this.callback = new UNRARCallback(RARCallback);
        }

        public Unrar(string archivePathName)
            : this()
        {
            this.archivePathName = archivePathName;
        }

        ~Unrar()
        {
            if (this.archiveHandle != IntPtr.Zero)
            {
                NativeMethods.RARCloseArchive(this.archiveHandle);
                this.archiveHandle = IntPtr.Zero;
            }
        }

        // Очистка памяти
        public void Dispose()
        {
            if (this.archiveHandle != IntPtr.Zero)
            {
                NativeMethods.RARCloseArchive(this.archiveHandle);
                this.archiveHandle = IntPtr.Zero;
            }
        }

        #endregion

        #region Общие свойства

        /// <summary>
        /// Путь и имя RAR архива, чтобы открыть
        /// </summary>
        public string ArchivePathName
        {
            get
            {
                return this.archivePathName;
            }
            set
            {
                this.archivePathName = value;
            }
        }

        /// <summary>
        /// Комментарий архива
        /// </summary>
        public string Comment
        {
            get
            {
                return (this.comment);
            }
        }

        /// <summary>
        /// Процесс текущего файла
        /// </summary>
        public RARFileInfo CurrentFile
        {
            get
            {
                return (this.currentFile);
            }
        }

        /// <summary>
        /// Путь по умолчанию для распаковки
        /// </summary>
        public string DestinationPath
        {
            get
            {
                return this.destinationPath;
            }
            set
            {
                this.destinationPath = value;
            }
        }

        /// <summary>
        /// ПАроль для зашифрованного файла
        /// </summary>
        public string Password
        {
            get
            {
                return (this.password);
            }
            set
            {
                this.password = value;
                if (this.archiveHandle != IntPtr.Zero)
                    NativeMethods.RARSetPassword(this.archiveHandle, value);
            }
        }

        #endregion

        #region Общие методы

        /// <summary>
        /// Закрытие открытого архива
        /// </summary>
        /// <returns></returns>
        public void Close()
        {
            // Выход без исключения, если нет открытого архива
            if (this.archiveHandle == IntPtr.Zero)
                return;

            // закрыть архив
            int result = NativeMethods.RARCloseArchive(this.archiveHandle);

            // Проверка результата
            if (result != 0)
            {
                ProcessFileError(result);
            }
            else
            {
                this.archiveHandle = IntPtr.Zero;
            }
        }

        /// <summary>
        /// Открытие архивов указанных в ArchivePathName для тестирования или извлечения
        /// </summary>
        public void Open()
        {
            if (this.ArchivePathName.Length == 0)
                throw new IOException("Archive name has not been set.");
            this.Open(this.ArchivePathName, OpenMode.Extract);
        }

        /// <summary>
        /// Открытие архивов указанных в ArchivePathName с указанным режимом
        /// </summary>
        /// <param name="openMode">Режим, в котором архив должн быть открыт</param>
        public void Open(OpenMode openMode)
        {
            if (this.ArchivePathName.Length == 0)
                throw new IOException("Archive name has not been set.");
            this.Open(this.ArchivePathName, openMode);
        }

        /// <summary>
        /// Открывает указанный архив с помощью заданного режима.  
        /// </summary>
        /// <param name="archivePathName">Путь архива, чтобы открыть</param>
        /// <param name="openMode">Режим, в котором, чтобы открыть архив</param>
        public void Open(string archivePathName, OpenMode openMode)
        {
            IntPtr handle = IntPtr.Zero;

            // Close any previously open archives
            if (this.archiveHandle != IntPtr.Zero)
                this.Close();

            // Prepare extended open archive struct
            this.ArchivePathName = archivePathName;
            RAROpenArchiveDataEx openStruct = new RAROpenArchiveDataEx();
            openStruct.Initialize();
            openStruct.ArcName = this.archivePathName + "\0";
            openStruct.ArcNameW = this.archivePathName + "\0";
            openStruct.OpenMode = (uint)openMode;
            if (this.retrieveComment)
            {
                openStruct.CmtBuf = new string((char)0, 65536);
                openStruct.CmtBufSize = 65536;
            }
            else
            {
                openStruct.CmtBuf = null;
                openStruct.CmtBufSize = 0;
            }

            // Открыть архив
            handle = NativeMethods.RAROpenArchiveEx(ref openStruct);

            // Проверка успеха
            if (openStruct.OpenResult != 0)
            {
                switch ((RarError)openStruct.OpenResult)
                {
                    case RarError.InsufficientMemory:
                        throw new OutOfMemoryException("Insufficient memory to perform operation.");

                    case RarError.BadData:
                        throw new IOException("Archive header broken");

                    case RarError.BadArchive:
                        throw new IOException("File is not a valid archive.");

                    case RarError.OpenError:
                        throw new IOException("File could not be opened.");
                }
            }

            // Save handle and flags
            this.archiveHandle = handle;
            this.archiveFlags = (ArchiveFlags)openStruct.Flags;

            // Set callback
            NativeMethods.RARSetCallback(this.archiveHandle, this.callback, this.GetHashCode());

            // If comment retrieved, save it
            if (openStruct.CmtState == 1)
                this.comment = openStruct.CmtBuf.ToString();

            // If password supplied, set it
            if (this.password.Length != 0)
                NativeMethods.RARSetPassword(this.archiveHandle, this.password);

            // Fire NewVolume event for first volume
            this.OnNewVolume(this.archivePathName);
        }

        /// <summary>
        /// Читает следующий заголовок архива и заполняет CurrentFile данным свойством
        /// </summary>
        /// <returns></returns>
        public bool ReadHeader()
        {
            // Throw exception if archive not open
            if (this.archiveHandle == IntPtr.Zero)
                throw new IOException("Archive is not open.");

            // Initialize header struct
            this.header = new RARHeaderDataEx();
            header.Initialize();

            // Read next entry
            currentFile = null;
            int result = NativeMethods.RARReadHeaderEx(this.archiveHandle, ref this.header);

            // Check for error or end of archive
            if ((RarError)result == RarError.EndOfArchive)
                return false;
            else if ((RarError)result == RarError.BadData)
                throw new IOException("Archive data is corrupt.");

            // Determine if new file
            if (((header.Flags & 0x01) != 0) && currentFile != null)
                currentFile.ContinuedFromPrevious = true;
            else
            {
                // New file, prepare header
                currentFile = new RARFileInfo();
                currentFile.FileName = header.FileNameW.ToString();
                if ((header.Flags & 0x02) != 0)
                    currentFile.ContinuedOnNext = true;
                if (header.PackSizeHigh != 0)
                    currentFile.PackedSize = (header.PackSizeHigh * 0x100000000) + header.PackSize;
                else
                    currentFile.PackedSize = header.PackSize;
                if (header.UnpSizeHigh != 0)
                    currentFile.UnpackedSize = (header.UnpSizeHigh * 0x100000000) + header.UnpSize;
                else
                    currentFile.UnpackedSize = header.UnpSize;
                currentFile.HostOS = (int)header.HostOS;
                currentFile.FileCRC = header.FileCRC;
                currentFile.FileTime = FromMSDOSTime(header.FileTime);
                currentFile.VersionToUnpack = (int)header.UnpVer;
                currentFile.Method = (int)header.Method;
                currentFile.FileAttributes = (int)header.FileAttr;
                currentFile.BytesExtracted = 0;
                if ((header.Flags & 0xE0) == 0xE0)
                    currentFile.IsDirectory = true;
                this.OnNewFile();
            }

            // Return success
            return true;
        }

        /// <summary>
        /// Возвращает массив имен файлов, содержащихся в архиве
        /// </summary>
        /// <returns></returns>
        public string[] ListFiles()
        {
            ArrayList fileNames = new ArrayList();
            while (this.ReadHeader())
            {
                if (!currentFile.IsDirectory)
                    fileNames.Add(currentFile.FileName);
                this.Skip();
            }
            string[] files = new string[fileNames.Count];
            fileNames.CopyTo(files);
            return files;
        }

        /// <summary>
        /// Перемещает текущую позицию архиве к следующему заголовка
        /// </summary>
        /// <returns></returns>
        public void Skip()
        {
            int result = NativeMethods.RARProcessFile(this.archiveHandle, (int)Operation.Skip, string.Empty, string.Empty);

            // Проверка результата
            if (result != 0)
            {
                ProcessFileError(result);
            }
        }

        /// <summary>
        /// Тесты возможность извлечения текущего файла без сохранения извлеченных данных на диске
        /// </summary>
        /// <returns></returns>
        public void Test()
        {
            int result = NativeMethods.RARProcessFile(this.archiveHandle, (int)Operation.Test, string.Empty, string.Empty);

            // проверка результата
            if (result != 0)
            {
                ProcessFileError(result);
            }
        }

        /// <summary>
        /// Извлекает текущий файл в папку назначения по умолчанию
        /// </summary>
        /// <returns></returns>
        public void Extract()
        {
            this.Extract(this.destinationPath, string.Empty);
        }

        /// <summary>
        /// Извлечение текущего файла в указанную папку назначения и имя файла
        /// </summary>
        /// <param name="destinationName">Путь и имя распаковываемого файла</param>
        /// <returns></returns>
        public void Extract(string destinationName)
        {
            this.Extract(string.Empty, destinationName);
        }

        /// <summary>
        /// Извлечение текущего файла в указанную директорию без переименования файлов
        /// </summary>
        /// <param name="destinationPath"></param>
        /// <returns></returns>
        public void ExtractToDirectory(string destinationPath)
        {
            this.Extract(destinationPath, string.Empty);
        }

        #endregion

        #region Частные методы

        private void Extract(string destinationPath, string destinationName)
        {
            int result = NativeMethods.RARProcessFile(this.archiveHandle, (int)Operation.Extract, destinationPath, destinationName);

            // проверка результата
            if (result != 0)
            {
                ProcessFileError(result);
            }
        }

        private DateTime FromMSDOSTime(uint dosTime)
        {
            int day = 0;
            int month = 0;
            int year = 0;
            int second = 0;
            int hour = 0;
            int minute = 0;
            ushort hiWord;
            ushort loWord;
            hiWord = (ushort)((dosTime & 0xFFFF0000) >> 16);
            loWord = (ushort)(dosTime & 0xFFFF);
            year = ((hiWord & 0xFE00) >> 9) + 1980;
            month = (hiWord & 0x01E0) >> 5;
            day = hiWord & 0x1F;
            hour = (loWord & 0xF800) >> 11;
            minute = (loWord & 0x07E0) >> 5;
            second = (loWord & 0x1F) << 1;
            return new DateTime(year, month, day, hour, minute, second);
        }

        private void ProcessFileError(int result)
        {
            switch ((RarError)result)
            {
                case RarError.UnknownFormat:
                    throw new OutOfMemoryException("Unknown archive format.");

                case RarError.BadData:
                    throw new IOException("File CRC Error");

                case RarError.BadArchive:
                    throw new IOException("File is not a valid archive.");

                case RarError.OpenError:
                    throw new IOException("File could not be opened.");

                case RarError.CreateError:
                    throw new IOException("File could not be created.");

                case RarError.CloseError:
                    throw new IOException("File close error.");

                case RarError.ReadError:
                    throw new IOException("File read error.");

                case RarError.WriteError:
                    throw new IOException("File write error.");
            }
        }

        private int RARCallback(uint msg, int UserData, IntPtr p1, int p2)
        {
            string volume = string.Empty;
            string newVolume = string.Empty;
            int result = -1;

            switch ((CallbackMessages)msg)
            {
                case CallbackMessages.VolumeChange:
                    volume = Marshal.PtrToStringAnsi(p1);
                    if ((VolumeMessage)p2 == VolumeMessage.Notify)
                        result = OnNewVolume(volume);
                    else if ((VolumeMessage)p2 == VolumeMessage.Ask)
                    {
                        newVolume = OnMissingVolume(volume);
                        if (newVolume.Length == 0)
                            result = -1;
                        else
                        {
                            if (newVolume != volume)
                            {
                                for (int i = 0; i < newVolume.Length; i++)
                                {
                                    Marshal.WriteByte(p1, i, (byte)newVolume[i]);
                                }
                                Marshal.WriteByte(p1, newVolume.Length, (byte)0);
                            }
                            result = 1;
                        }
                    }
                    break;

                case CallbackMessages.ProcessData:
                    result = OnDataAvailable(p1, p2);
                    break;

                case CallbackMessages.NeedPassword:
                    result = OnPasswordRequired(p1, p2);
                    break;
            }
            return result;
        }

        #endregion

        #region Protected Virtual (Overridable) Methods

        protected virtual void OnNewFile()
        {
            if (this.NewFile != null)
            {
                NewFileEventArgs e = new NewFileEventArgs(this.currentFile);
                this.NewFile(this, e);
            }
        }

        protected virtual int OnPasswordRequired(IntPtr p1, int p2)
        {
            int result = -1;
            if (this.PasswordRequired != null)
            {
                PasswordRequiredEventArgs e = new PasswordRequiredEventArgs();
                this.PasswordRequired(this, e);
                if (e.ContinueOperation && e.Password.Length > 0)
                {
                    for (int i = 0; (i < e.Password.Length) && (i < p2); i++)
                        Marshal.WriteByte(p1, i, (byte)e.Password[i]);
                    Marshal.WriteByte(p1, e.Password.Length, (byte)0);
                    result = 1;
                }
            }
            else
            {
                throw new IOException("Password is required for extraction.");
            }
            return result;
        }

        protected virtual int OnDataAvailable(IntPtr p1, int p2)
        {
            int result = 1;
            if (this.currentFile != null)
                this.currentFile.BytesExtracted += p2;
            if (this.DataAvailable != null)
            {
                byte[] data = new byte[p2];
                Marshal.Copy(p1, data, 0, p2);
                DataAvailableEventArgs e = new DataAvailableEventArgs(data);
                this.DataAvailable(this, e);
                if (!e.ContinueOperation)
                    result = -1;
            }
            if ((this.ExtractionProgress != null) && (this.currentFile != null))
            {
                ExtractionProgressEventArgs e = new ExtractionProgressEventArgs();
                e.FileName = this.currentFile.FileName;
                e.FileSize = this.currentFile.UnpackedSize;
                e.BytesExtracted = this.currentFile.BytesExtracted;
                e.PercentComplete = this.currentFile.PercentComplete;
                this.ExtractionProgress(this, e);
                if (!e.ContinueOperation)
                    result = -1;
            }
            return result;
        }

        protected virtual int OnNewVolume(string volume)
        {
            int result = 1;
            if (this.NewVolume != null)
            {
                NewVolumeEventArgs e = new NewVolumeEventArgs(volume);
                this.NewVolume(this, e);
                if (!e.ContinueOperation)
                    result = -1;
            }
            return result;
        }

        protected virtual string OnMissingVolume(string volume)
        {
            string result = string.Empty;
            if (this.MissingVolume != null)
            {
                MissingVolumeEventArgs e = new MissingVolumeEventArgs(volume);
                this.MissingVolume(this, e);
                if (e.ContinueOperation)
                    result = e.VolumeName;
            }
            return result;
        }

        #endregion
    }

    #region Классы аргументы события

    public class NewVolumeEventArgs
    {
        public string VolumeName;
        public bool ContinueOperation = true;

        public NewVolumeEventArgs(string volumeName)
        {
            this.VolumeName = volumeName;
        }
    }

    public class MissingVolumeEventArgs
    {
        public string VolumeName;
        public bool ContinueOperation = false;

        public MissingVolumeEventArgs(string volumeName)
        {
            this.VolumeName = volumeName;
        }
    }

    public class DataAvailableEventArgs
    {
        public readonly byte[] Data;
        public bool ContinueOperation = true;

        public DataAvailableEventArgs(byte[] data)
        {
            this.Data = data;
        }
    }

    public class PasswordRequiredEventArgs
    {
        public string Password = string.Empty;
        public bool ContinueOperation = true;
    }

    public class NewFileEventArgs
    {
        public RARFileInfo fileInfo;
        public NewFileEventArgs(RARFileInfo fileInfo)
        {
            this.fileInfo = fileInfo;
        }
    }

    public class ExtractionProgressEventArgs
    {
        public string FileName;
        public long FileSize;
        public long BytesExtracted;
        public double PercentComplete;
        public bool ContinueOperation = true;
    }

    public class RARFileInfo
    {
        public string FileName;
        public bool ContinuedFromPrevious = false;
        public bool ContinuedOnNext = false;
        public bool IsDirectory = false;
        public long PackedSize = 0;
        public long UnpackedSize = 0;
        public int HostOS = 0;
        public long FileCRC = 0;
        public DateTime FileTime;
        public int VersionToUnpack = 0;
        public int Method = 0;
        public int FileAttributes = 0;
        public long BytesExtracted = 0;

        public double PercentComplete
        {
            get
            {
                if (this.UnpackedSize != 0)
                    return (((double)this.BytesExtracted / (double)this.UnpackedSize) * (double)100.0);
                else
                    return (double)0;
            }
        }
    }

    #endregion




    #endregion
}
