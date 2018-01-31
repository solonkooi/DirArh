using System;
using System.IO;
using Ionic.Zip;
using System.Text.RegularExpressions;

namespace DirArh
{
    class Program
    {
        private static string path;
        private static string pattern;
        private static string namearh;

        static void Main(string[] args)
        {
            try
            {
                GetPath();
                GetPattern();
                GetNameArh();
                DirectoryInfo dir = new DirectoryInfo(path);
                FileInfo[] files = dir.GetFiles(pattern, SearchOption.AllDirectories);
                using (ZipFile zip = new ZipFile())
                {
                    foreach (FileInfo file in files)
                    {
                        zip.AddFile(file.FullName,"");
                    }
                    if (files.Length > 0)
                    {
                        zip.Save(Path.Combine(path, namearh));
                        Console.WriteLine("-----------------------------------------------");
                        Console.WriteLine("Number of suitable criteria file - " + files.Length.ToString());
                        Console.WriteLine("Аrchive created successfully!");
                        
                    } else
                    {
                        Console.WriteLine("no files with this extensio");
                    }
                    Console.ReadLine();
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine($"Error {ex.Message}");
                Console.ReadLine();
            }
        }

        private static void GetNameArh()
        {
            const string extZip = ".zip";
            Console.WriteLine("Enter the name of the archive");
            namearh = Console.ReadLine();
            if (namearh == String.Empty)
            {
                namearh = "Default.zip";
            }
            if (!namearh.Contains(extZip))
            {
                namearh += extZip;
            }
        }

        private static void GetPattern()
        {
            Console.WriteLine("Enter the file extension:");
            pattern = Console.ReadLine();
            if (pattern == String.Empty)
            {
                pattern = "*";
            } else if (Regex.IsMatch(pattern[1].ToString(), @"[.]+$"))
            {
                pattern = string.Format("{0}{1}","*",pattern);
            } else if (Regex.IsMatch(pattern[1].ToString(), @"[а-яА-ЯёЁa-zA-Z0-9]+$"))
            {
                pattern = string.Format("{0}{1}", "*.", pattern);
            }
        }

        private static void GetPath()
        {
            do
            {
                Console.WriteLine("Enter the director:");
                path = Console.ReadLine();
            } while (!Directory.Exists(path));
        }
    }
}
