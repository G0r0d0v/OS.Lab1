using System;
using System.IO;
using System.Text.Json;
using System.Xml;
using System.Text;
using System.IO.Compression;

//РАБОТУ ВЫПОЛНИЛ СТУДЕНТ ГРУППЫ БББО-07-19 ГОРОДОВ М.А.
//2021г.

namespace ОС_лаб_1
{
    class Program
    {
        public static void TextFile()
        {
            string path = @"D:\SomeDir2\\";  // создаем каталог для файла
            DirectoryInfo dirInfo = new DirectoryInfo(path);
            if (!dirInfo.Exists)
            {
                dirInfo.Create();
            }
            Console.WriteLine("Введите строку для записи в файл:");
            string text = Console.ReadLine();
            using (FileStream fstream = new FileStream($"{path}note.txt", FileMode.OpenOrCreate))  //создание и запись в файл
            {
                byte[] array = System.Text.Encoding.Default.GetBytes(text);
                fstream.Write(array, 0, array.Length);
                Console.WriteLine("Текст записан в файл");
                Console.ReadKey();
            }
            using (FileStream fstream = File.OpenRead($"{path}note.txt"))//чтение из файла
            {
                byte[] array = new byte[fstream.Length];
                fstream.Read(array, 0, array.Length);
                string textFromFile = System.Text.Encoding.Default.GetString(array);
                Console.WriteLine($"Текст из файла: {textFromFile}");
            }
            Console.ReadLine();
            Console.ReadKey();
            File.Delete($"{path}note.txt"); //удаление файла
            Console.WriteLine("Файл удален");
        }
        class Person
        {
            public string Name { get; set; }
            public int Age { get; set; }
        }
        public static async void JsonFile()
        {
            string path = @"D:\\user\\";
            DirectoryInfo dirInfo = new DirectoryInfo(path);
            if (!dirInfo.Exists)
            {
                dirInfo.Create();                               //создали каталог для файла json
            }
            Person tom = new Person() { Name = "Abraham", Age = 120 };
            string json = JsonSerializer.Serialize<Person>(tom);
            Console.WriteLine(json);
            using (FileStream fs = new FileStream($"{path}user.json", FileMode.OpenOrCreate))       // сохранение данных
            {
                Person restoredPerson = JsonSerializer.Deserialize<Person>(json);
                await JsonSerializer.SerializeAsync<Person>(fs, tom);
                Console.WriteLine("Запись в файле сохранена");
                Console.ReadKey();
            }
            using (FileStream fs = new FileStream($"{path}user.json", FileMode.OpenOrCreate))              //чтение файла
            {
                Person restoredPerson = await JsonSerializer.DeserializeAsync<Person>(fs);
                Console.WriteLine($"Name: {restoredPerson.Name}  Age: {restoredPerson.Age}");
                Console.ReadKey();
            }
            Console.ReadKey();
            File.Delete($"{path}user.json");                                                            //удаление файла
            Console.WriteLine($"Файл удален");
        }
        class Presidents
        {
            public string LastName { get; set; }
            public int Age { get; set; }
            public string Country { get; set; }
            public int RulingAges { get; set; }
        }
        public static void XmlFile()
        {
            string path = @"D:\\xml-файл\\"; //создаем каталог
            if (File.Exists($"{path}Presidents.xml"))
            {
                Console.WriteLine("Ошибка! Файл уже существует!\n");
            }
            using FileStream fileStream3 = new FileStream($"{path}\\Presidents.xml", FileMode.OpenOrCreate);
            byte[] byti = Encoding.Default.GetBytes("<?xml version=\"1.0\" encoding=\"utf-8\"?>\n<Presidents>\n</Presidents>"); //создаем xml-файл
            fileStream3.Write(byti, 0, byti.Length);
            fileStream3.Close();
            XmlDocument xmlDocument = new XmlDocument();
            bool k = true;  //вводим условия для ввода более чем одного президента
            while (k)
            {
                xmlDocument.Load($"{path}\\Presidents.xml");
                XmlElement documentElement = xmlDocument.DocumentElement;
                Console.WriteLine("Enter tha Last Name");
                string text1 = Console.ReadLine();
                XmlElement element1 = xmlDocument.CreateElement("President");
                XmlAttribute attribute = xmlDocument.CreateAttribute("LastName");
                XmlElement element2 = xmlDocument.CreateElement("Age");
                XmlElement element3 = xmlDocument.CreateElement("Country");
                XmlElement element4 = xmlDocument.CreateElement("RulingAges");
                XmlText textNode1 = xmlDocument.CreateTextNode(text1);
                Console.WriteLine("Enter the Age");
                string text2 = Console.ReadLine();
                XmlText textNode2 = xmlDocument.CreateTextNode(text2);
                Console.WriteLine("Enter the Country");
                string text3 = Console.ReadLine();
                XmlText textNode3 = xmlDocument.CreateTextNode(text3);
                Console.WriteLine("Enter the number of Ruling Ages");
                string text4 = Console.ReadLine();
                XmlText textNode4 = xmlDocument.CreateTextNode(text4);
                attribute.AppendChild((XmlNode)textNode1);
                element2.AppendChild((XmlNode)textNode2);
                element3.AppendChild((XmlNode)textNode3);
                element4.AppendChild((XmlNode)textNode4);
                element1.Attributes.Append(attribute);
                element1.AppendChild((XmlNode)element2);
                element1.AppendChild((XmlNode)element3);
                element1.AppendChild((XmlNode)element4);
                documentElement.AppendChild((XmlNode)element1);
                xmlDocument.Save($"{path}\\Presidents.xml");
                Console.WriteLine("Do you want to continue adding? 1/0");
                if (!(Console.ReadLine() == "1"))
                    k = false;
            }
            Console.ReadKey();
            xmlDocument.Load($"{path}\\Presidents.xml");    //чтение файла
            XmlElement xRoot = xmlDocument.DocumentElement; // получим корневой элемент
            foreach (XmlNode xnode in xRoot)        // обход всех узлов в корневом элементе
            {
                if (xnode.Attributes.Count > 0)// получаем атрибут name
                {
                    XmlNode attr = xnode.Attributes.GetNamedItem("LastName");
                    if (attr != null)
                        Console.WriteLine("Last Name: " + attr.Value);
                }
                foreach (XmlNode childnode in xnode.ChildNodes)// обходим все дочерние узлы элемента president
                {
                    if (childnode.Name == "Country")             // если узел - country
                    {
                        Console.WriteLine($"Country: {childnode.InnerText}");
                    }
                    if (childnode.Name == "Age")            // если узел - age
                    {
                        Console.WriteLine($"Age: {childnode.InnerText}");
                    }
                    if (childnode.Name == "RulingAges")            // если узел - rulingages
                    {
                        Console.WriteLine($"Age as President: {childnode.InnerText}");
                    }
                }
            }
            Console.ReadKey();
            File.Delete($"{path}\\Presidents.xml");
            Console.WriteLine("Файл удален");
        }
        public static void ZipWork()
        {

            string startPath = @"D:\\zipFile";      //расположение папки для архивации
            string zipPath = @"D:\\result.zip";     //куда сохранять архив
            string extractPath = @"D:\\extract";    //куда извлекать архив
            ZipFile.CreateFromDirectory(startPath, zipPath);
            Console.WriteLine("Zip-архив создан");
            Console.ReadKey();
            using (FileStream zipToOpen = new FileStream(@"D:\\result.zip", FileMode.Open)) //добавляем файл Readme.txt в созданный нами архив
            {
                using ZipArchive archive = new ZipArchive(zipToOpen, ZipArchiveMode.Update);
                ZipArchiveEntry readmeEntry = archive.CreateEntry("Readme.txt");
            }
            Console.WriteLine("Файл Readme.txt добавлен в Zip-архив");
            Console.ReadKey();
            ZipFile.ExtractToDirectory(zipPath, extractPath); //извлекаем все файлы из архива
            Console.WriteLine("Восстановлен файл: {0}", (object)(extractPath));
            Console.ReadKey();
            File.Delete(zipPath);
            Console.WriteLine("ZIP-архив удален");
            Console.ReadKey();
            Directory.Delete(extractPath, true);
            Console.WriteLine("Извлеченные файлы удалены");
        }
        static void Main()
        {
            bool j = true;
            string main;
            while (j != false)
            {
                Console.WriteLine("Введите номер задания:\n" +
                    "1-Вывести информацию  о логических дисках\n" +
                    "2-Работа с текстовыми файлами\n" +
                    "3-Работа с форматом JSON\n" +
                    "4-Работа с форматом XML\n" +
                    "5-Работа с zip-архивом\n" +
                    "6-Выход\n"
                    );
                main = Console.ReadLine();
                switch (main)
                {
                    case "1":
                        {
                            DriveInfo[] drives = DriveInfo.GetDrives();
                            foreach (DriveInfo drive in drives)
                            {                                                       //выводим информацию о диске:
                                Console.WriteLine($"Название: {drive.Name}");       //название диска
                                Console.WriteLine($"Тип: {drive.DriveType}");       //тип диска
                                if (drive.IsReady)
                                {
                                    Console.WriteLine($"Объем диска: {drive.TotalSize}");                   //объем диска
                                    Console.WriteLine($"Свободное пространство: {drive.TotalFreeSpace}");   //свободное пространство на диске
                                    Console.WriteLine($"Метка: {drive.VolumeLabel}");                       //метку диска
                                }
                                Console.WriteLine();
                            }
                            break;
                        }
                    case "2":
                        {
                            TextFile();
                            break;
                        }
                    case "3":
                        {
                            JsonFile();
                            break;
                        }
                    case "4":
                        {
                            XmlFile();
                            break;
                        }
                    case "5":
                        {
                            ZipWork();
                            break;
                        }
                    case "6":
                        {
                            j = false;
                            break;
                        }
                }
            }
        }
    }
}