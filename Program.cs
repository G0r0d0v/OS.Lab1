using System;
using System.IO;
using System.Xml;
using System.Threading.Tasks;
using System.Text.Json;
using System.IO.Compression;



namespace Lab1
{
    class Person
    {
        public string Name { get; set; }
        public int Age { get; set; }
    }
    class Program
    {
        static void Main(string[] args)
        {
            string main;
            string main1;
            string main2;
            string main3;
            string main4;
            bool flag;
            while (flag = true)
            {
                Console.WriteLine("Введите номер задания:\n" +
                    "1-Вывести информацию  о логических дисках, именах, метке тома, размере типе файловой системы\n" +
                    "2-Работа с файлами\n" +
                    "3-Работа с форматом JSON\n" +
                    "4-Работа с форматом XML\n" +
                    "5-Создание zip архива, добавление туда файла, определение размера архива \n"
                    );
                main = Console.ReadLine();
                switch (main)
                {
                    case "1":
                        DriveInfo[] drives = DriveInfo.GetDrives();

                        foreach (DriveInfo drive in drives)
                        {
                            Console.WriteLine($"Название: {drive.Name}");
                            Console.WriteLine($"Тип: {drive.DriveType}");
                            if (drive.IsReady)
                            {
                                Console.WriteLine($"Объем диска: {drive.TotalSize}");
                                Console.WriteLine($"Свободное пространство: {drive.TotalFreeSpace}");
                                Console.WriteLine($"Метка: {drive.VolumeLabel}");
                            }
                            Console.WriteLine();
                        }
                        break;
                    case "2":

                        string path = @"C:\\SomeDir2.txt";
                        DirectoryInfo dirInfo = new DirectoryInfo(path);
                        if (!dirInfo.Exists)
                        {
                            dirInfo.Create();
                        }
                        Console.WriteLine("Введите строку для записи в файл:");
                        string text = Console.ReadLine();

                        using (FileStream fstream = new FileStream($"{path}note.txt", FileMode.OpenOrCreate))
                        {

                            byte[] array = System.Text.Encoding.Default.GetBytes(text);

                            fstream.Write(array, 0, array.Length);
                            Console.WriteLine("Текст записан в файл");
                        }
                        Console.WriteLine(
                            "1-прочитать файл\n" +
                            "2-удалить файл\n" +
                            "3-вернуться в меню\n");
                        main2 = Console.ReadLine();
                        switch (main2)
                        {
                            case "1":
                                using (FileStream fstream = File.OpenRead($"{path}note.txt"))
                                {

                                    byte[] array = new byte[fstream.Length];

                                    fstream.Read(array, 0, array.Length);

                                    string textFromFile = System.Text.Encoding.Default.GetString(array);
                                    Console.WriteLine($"Текст из файла: {textFromFile}");
                                }
                                break;

                            case "2":
                                System.IO.File.Delete(@"C:\\SomeDir2.txtnote.txt");
                                break;

                        }



                        break;
                    case "3":

                        Console.WriteLine(
                        "1-создать файл и сюреализировать его\n" +
                           "2-прочитать файл\n" +
                             "3-удалить в файл\n" +
                           "4-вернуться в меню\n");
                        main3 = Console.ReadLine();
                        switch (main3)
                        {
                            case "1":
                                string path1 = @"C:\\user";
                                DirectoryInfo dirInfo1 = new DirectoryInfo(path1);
                                if (!dirInfo1.Exists)
                                {
                                    dirInfo1.Create();
                                }

                                Person tom = new Person { Name = "Tom", Age = 35 };
                                string json = JsonSerializer.Serialize<Person>(tom);
                                Console.WriteLine(json);
                                Person restoredPerson = JsonSerializer.Deserialize<Person>(json);
                                Console.WriteLine(restoredPerson.Name);

                                using (FileStream fstream = new FileStream($"{path1}note.json", FileMode.OpenOrCreate))
                                {

                                    byte[] array = System.Text.Encoding.Default.GetBytes(restoredPerson.Name);

                                    fstream.Write(array, 0, array.Length);
                                    Console.WriteLine("Текст записан в файл");
                                }

                                break;
                            case "2":
                                using (FileStream fstream = File.OpenRead($"C:\\user.jsonnote.json"))
                                {

                                    byte[] array = new byte[fstream.Length];

                                    fstream.Read(array, 0, array.Length);

                                    string textFromFile = System.Text.Encoding.Default.GetString(array);
                                    Console.WriteLine($"Текст из файла: {textFromFile}");
                                }
                                break;
                            case "3":

                                System.IO.File.Delete("C:\\usernote.json");
                                break;
                        }
                        break;
                    case "4":
                        XmlDocument xDoc = new XmlDocument();
                        xDoc.Load("C://users.xml");

                        XmlElement xRoot = xDoc.DocumentElement;

                        foreach (XmlNode xnode in xRoot)
                        {

                            if (xnode.Attributes.Count > 0)
                            {
                                XmlNode attr = xnode.Attributes.GetNamedItem("name");
                                if (attr != null)
                                    Console.WriteLine(attr.Value);
                            }

                            foreach (XmlNode childnode in xnode.ChildNodes)
                            {

                                if (childnode.Name == "company")
                                {
                                    Console.WriteLine($"Компания: {childnode.InnerText}");
                                }

                                if (childnode.Name == "age")
                                {
                                    Console.WriteLine($"Возраст: {childnode.InnerText}");
                                }
                            }
                        }
                        Console.WriteLine("Хотите внести изменения в XML файл?\n" +
                            "1-внести изменения\n" +
                            "2-удалить\n" +
                            "3-вернуться в меню\n");
                        main1 = Console.ReadLine();
                        switch (main1)
                        {
                            case "1":
                                xDoc.Load("C://users.xml");
                                XmlElement xZoot = xDoc.DocumentElement;

                                XmlElement userElem = xDoc.CreateElement("user");

                                XmlAttribute nameAttr = xDoc.CreateAttribute("name");

                                XmlElement companyElem = xDoc.CreateElement("company");
                                XmlElement ageElem = xDoc.CreateElement("age");

                                XmlText nameText = xDoc.CreateTextNode("Mark Zuckerberg");
                                XmlText companyText = xDoc.CreateTextNode("Facebook");
                                XmlText ageText = xDoc.CreateTextNode("30");


                                nameAttr.AppendChild(nameText);
                                companyElem.AppendChild(companyText);
                                ageElem.AppendChild(ageText);
                                userElem.Attributes.Append(nameAttr);
                                userElem.AppendChild(companyElem);
                                userElem.AppendChild(ageElem);
                                xZoot.AppendChild(userElem);
                                xDoc.Save("C://users.xml");
                                break;
                            case "2":
                                xDoc.Load("C://users.xml");
                                XmlNode firstNode = xRoot.FirstChild;
                                xRoot.RemoveChild(firstNode);
                                xDoc.Save("C://users.xml");
                                break;

                        }

                        break;
                    case "5":
                        string sourceFile = "C://book.pdf";
                        string compressedFile = "C://book.gz";
                        string targetFile = "C://book_new.pdf";

                        Console.WriteLine(
                      "1-создать zip и поместить в него файл .pdf\n" +
                         "2-разархивировать файл\n" +
                           "3-удалить в файл\n" +
                         "4-вернуться в меню\n");
                        main4 = Console.ReadLine();
                        switch (main4)
                        {
                            case "1":

                                Compress(sourceFile, compressedFile);
                                break;
                            case "2":
                                Decompress(compressedFile, targetFile);
                                break;

                            case "3":
                                System.IO.File.Delete(@"C:\\book.pdf");
                                System.IO.File.Delete(@"C:\\book.gz");
                                System.IO.File.Delete(@"C:\\book_new.pdf");
                                break;

                        }
                        break;
                }
            }

        }
        public static void Compress(string sourceFile, string compressedFile)
        {

            using (FileStream sourceStream = new FileStream(sourceFile, FileMode.OpenOrCreate))
            {

                using (FileStream targetStream = File.Create(compressedFile))
                {

                    using (GZipStream compressionStream = new GZipStream(targetStream, CompressionMode.Compress))
                    {
                        sourceStream.CopyTo(compressionStream);
                        Console.WriteLine("Сжатие файла {0} завершено. Исходный размер: {1}  сжатый размер: {2}.",
                            sourceFile, sourceStream.Length.ToString(), targetStream.Length.ToString());
                    }
                }
            }
        }

        public static void Decompress(string compressedFile, string targetFile)
        {

            using (FileStream sourceStream = new FileStream(compressedFile, FileMode.OpenOrCreate))
            {

                using (FileStream targetStream = File.Create(targetFile))
                {

                    using (GZipStream decompressionStream = new GZipStream(sourceStream, CompressionMode.Decompress))
                    {
                        decompressionStream.CopyTo(targetStream);
                        Console.WriteLine("Восстановлен файл: {0} ", targetFile);
                    }
                }
            }
        }
    }
}