using System;
using System.IO;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {

            int count, stadies = 0, pos = 0, rezerv = 0, rezerv_pos = 0;
            DirectoryInfo focus_dirs = null;
            DirectoryInfo rezerv_focus_dirs = null;
            DirectoryInfo[] dirs = null;
            DriveInfo[] drives = DriveInfo.GetDrives();
            DriveInfo focus_driver;
            FileInfo focus_files = null;
            FileInfo[] files = null;
            ConsoleKeyInfo key;
            string path = null;

            while(true)
            {
                if(stadies == 0)
                {
                    focus_driver = drives[pos];

                    Console.WriteLine("Выберите диск...");
                    Console.WriteLine("F8 -- Вывести информацию о данном диске.");
                    Console.WriteLine();

                    for (count = 0; count < drives.Length; count++)
                    {
                        if(count == pos)
                        {
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.WriteLine(drives[count]);
                        }
                        else
                        {
                            Console.ResetColor();
                            Console.WriteLine(drives[count]);
                        }
                        
                    }

                    Console.ResetColor();

                    key = Console.ReadKey(true);

                    if ((key.Key == ConsoleKey.UpArrow) && (pos != 0))
                    {
                        pos--;
                    }

                    if ((key.Key == ConsoleKey.DownArrow) && (pos != drives.Length - 1))
                    {
                        pos++;
                    }

                    if (key.Key == ConsoleKey.Enter)
                    {

                        Console.ResetColor();

                        stadies++;
                        rezerv = pos;
                        pos = 0;

                        try
                        {
                            path = focus_driver.Name;
                            dirs = new DirectoryInfo(focus_driver.Name).GetDirectories();
                            rezerv_focus_dirs = new DirectoryInfo(focus_driver.Name);
                        }
                        catch (Exception e)
                        {
                            Console.Clear();
                            stadies--;
                            pos = 0;
                            dirs = null;
                            Console.WriteLine(e.Message);
                            Console.ReadKey();
                            
                        }
                    }

                    if(key.Key == ConsoleKey.F8)
                    {
                        Console.Clear();
                        Console.ResetColor();

                        if (focus_driver.IsReady) 
                        {
                            Console.WriteLine("Имя диска: " + focus_driver.Name);
                            Console.WriteLine("Тип файловой системы: " + focus_driver.DriveFormat);
                            Console.WriteLine("Общий размер диска: " + focus_driver.TotalSize + " МБ.");
                            Console.WriteLine("Свободное место на диске: " + focus_driver.TotalFreeSpace + " МБ.");
                            Console.ReadKey();
                        }
                        else
                        {
                            Console.WriteLine("Устройство еще не готово.");
                            Console.ReadKey();
                        }

                    }

                    Console.Clear();

                }

                if (stadies == 1)
                {

                    Console.WriteLine("Выберите папку....");
                    Console.WriteLine("Enter -- Перейти в выбранный каталог.");
                    Console.WriteLine("Escape -- Вернуться к приведущему каталогу.");
                    Console.WriteLine("F1 -- Вернуться к выбору дисков.");
                    Console.WriteLine("F2 -- Режим работы с файлами этой директории.");
                    Console.WriteLine("F3 -- Создание папки.");
                    Console.WriteLine("F4 -- Удаление папки.");
                    Console.WriteLine("F5 -- Переименование папки или перенос ее в поддиректории данной директории.");
                    Console.WriteLine("F6 -- Перенос папки в другую директорию.");
                    Console.WriteLine();

                    if (dirs.Length > 0)
                    {

                        for (count = 0; count < dirs.Length; count++)
                        {
                            if (count == pos)
                            {
                                Console.ForegroundColor = ConsoleColor.Green;
                                Console.WriteLine(dirs[count]);
                            }
                            else
                            {
                                Console.ResetColor();
                                Console.WriteLine(dirs[count]);
                            }
                        }

                        Console.ResetColor();

                        try {
                            focus_dirs = dirs[pos];
                        }
                        catch
                        {

                        }
                        

                        key = Console.ReadKey();

                        if (key.Key == ConsoleKey.F1)
                        {
                            stadies--;
                            pos = 0;
                        }

                        if ((key.Key == ConsoleKey.UpArrow) && (pos != 0))
                        {
                            pos--;
                            focus_dirs = dirs[pos];
                            rezerv_focus_dirs = dirs[pos];
                        }

                        if ((key.Key == ConsoleKey.DownArrow) && (pos != dirs.Length - 1))
                        {
                            pos++;
                            focus_dirs = dirs[pos];
                            rezerv_focus_dirs = dirs[pos];
                        }

                        if (key.Key == ConsoleKey.Enter)
                        {
                            try
                            {
                                path = focus_dirs.FullName;
                                rezerv = pos;
                                pos = 0;
                                dirs = new DirectoryInfo(focus_dirs.FullName).GetDirectories();
                            }
                            catch(Exception e)
                            {
                                Console.Clear();
                                Console.WriteLine(e.Message);
                                pos = rezerv;
                                dirs = new DirectoryInfo(focus_dirs.Parent.FullName).GetDirectories();
                                Console.ReadKey();
                            }
                            
                        }

                        if(key.Key == ConsoleKey.F8)
                        {
                            Console.Clear();
                            Console.ResetColor();
                            Console.WriteLine("Имя папки: " + focus_dirs.Name);
                            Console.WriteLine("Полный путь до папки: " + focus_dirs.FullName);
                            Console.WriteLine("Атрибуты папки: " + focus_dirs.Attributes);
                            Console.WriteLine("Время создания папки: " + focus_dirs.CreationTime);
                            Console.WriteLine("Последний доступ к папке: " + focus_dirs.LastAccessTime);
                            Console.ReadKey();
                        }

                        if ((key.Key == ConsoleKey.Escape) && (focus_dirs.Parent.Parent != null))
                        {
                            dirs = new DirectoryInfo(focus_dirs.Parent.Parent.FullName).GetDirectories();
                            focus_dirs = dirs[pos];
                            path = focus_dirs.Parent.FullName;
                            //Console.WriteLine(rezerv_focus_dirs.FullName);
                            //Console.ReadKey();
                        }

                        if(key.Key == ConsoleKey.F2)
                        {
                            Console.Clear();
                            rezerv_pos = pos;
                            pos = 0;
                            stadies++;
                        }

                        if(key.Key == ConsoleKey.F3)
                        {
                            Console.Clear();
                            Console.WriteLine("Введите имя папки, которая будет создан...");
                            try
                            {
                                Directory.CreateDirectory(path + Console.ReadLine());
                                dirs = new DirectoryInfo(path).GetDirectories();
                                
                            }
                            catch(Exception e)
                            {
                                Console.WriteLine(e.Message);
                                Console.ReadKey();
                            }

                        }

                        if(key.Key == ConsoleKey.F4)
                        {
                            path = focus_dirs.Parent.FullName;
                            try
                            {
                                focus_dirs.Delete(true);

                            }
                            catch(Exception e)
                            {
                                Console.WriteLine(e.Message);
                                Console.ReadKey();
                            }

                            dirs = new DirectoryInfo(path).GetDirectories();
                            
                        }

                        if(key.Key == ConsoleKey.F5)
                        {
                            Console.Clear();
                            Console.WriteLine("Введите новое имя папки или новое раположение папки(в подкаталогах, если такие есть)...");
                            try
                            {
                                focus_dirs.MoveTo(focus_dirs.Parent.FullName + Console.ReadLine());
                            }
                            catch(Exception e)
                            {
                                Console.WriteLine(e.Message);
                                Console.ReadKey();
                            }
                            
                        }

                        if(key.Key == ConsoleKey.F6)
                        {

                            Console.Clear();
                            Console.WriteLine("Введите новый путь до папки...");

                            try
                            {
                                path = focus_dirs.Parent.Name;
                                focus_dirs.MoveTo(Console.ReadLine());
                                dirs = new DirectoryInfo(path).GetDirectories();
                            }
                            catch (Exception e)
                            {
                                Console.WriteLine(e.Message);
                                Console.ReadKey();
                            }
                        }

                        Console.Clear();
                        Console.ResetColor();

                    }
                    else
                    {
                        Console.WriteLine("Нет папок в данном каталоге.");
                        key = Console.ReadKey();

                        if(key.Key == ConsoleKey.Escape)
                        {
                            Console.Clear();
                            dirs = new DirectoryInfo(focus_dirs.Parent.FullName).GetDirectories();
                            path = focus_dirs.Parent.FullName;
                        }

                        if (key.Key == ConsoleKey.F2)
                        {
                            Console.Clear();
                            rezerv_pos = pos;
                            pos = 0;
                            stadies++;
                        }

                        if (key.Key == ConsoleKey.F3)
                        {
                            Console.Clear();
                            Console.WriteLine("Введите имя папки, которая будет создан...");
                            try
                            {

                                Directory.CreateDirectory(focus_dirs.FullName + @"\" + Console.ReadLine());
                                dirs = new DirectoryInfo(focus_dirs.FullName).GetDirectories();

                            }
                            catch (Exception e)
                            {
                                Console.WriteLine(e.Message);
                                Console.ReadKey();
                            }

                        }

                        if (key.Key == ConsoleKey.F4)
                        {
                            path = focus_dirs.Parent.FullName;
                            try
                            {
                                focus_dirs.Delete(true);

                            }
                            catch (Exception e)
                            {
                                Console.WriteLine(e.Message);
                                Console.ReadKey();
                            }

                            dirs = new DirectoryInfo(path).GetDirectories();

                        }

                        if (key.Key == ConsoleKey.F5)
                        {
                            Console.Clear();
                            Console.WriteLine("Введите новое имя папки или новое раположение папки(в подкаталогах, если такие есть)...");
                            try
                            {
                                focus_dirs.MoveTo(Console.ReadLine());
                            }
                            catch (Exception e)
                            {
                                Console.WriteLine(e.Message);
                                Console.ReadKey();
                            }

                        }

                        if (key.Key == ConsoleKey.F6)
                        {

                            Console.Clear();
                            Console.WriteLine("Введите новый путь до папки...");

                            try
                            {
                                path = focus_dirs.Parent.Name;
                                focus_dirs.MoveTo(Console.ReadLine());
                                dirs = new DirectoryInfo(path).GetDirectories();
                            }
                            catch (Exception e)
                            {
                                Console.WriteLine(e.Message);
                                Console.ReadKey();
                            }
                        }

                    }

                }

                if (stadies == 2)
                {

                    Console.WriteLine("Выберите файл....");
                    Console.WriteLine("F2 -- Вернуться к выбору папок.");
                    Console.WriteLine("F3 -- Создание файла.");
                    Console.WriteLine("F4 -- Удаление файла.");
                    Console.WriteLine("F5 -- Переименование файла или перенос файла в поддиректории данной директории.");
                    Console.WriteLine("F6 -- Перенос файла в другую директорию.");
                    Console.WriteLine();

                    files = new DirectoryInfo(path).GetFiles();

                    if (files.Length > 0)
                    {

                        for (count = 0; count < files.Length; count++)
                        {
                            if (count == pos)
                            {
                                Console.ForegroundColor = ConsoleColor.Green;
                                Console.WriteLine(files[count]);
                            }
                            else
                            {
                                Console.ForegroundColor = ConsoleColor.White;
                                Console.WriteLine(files[count]);
                            }
                        }

                    focus_files = files[pos];
                    Console.ResetColor();

                    key = Console.ReadKey();

                    if ((key.Key == ConsoleKey.UpArrow) && (pos != 0))
                    {
                        pos--;
                    }

                    if ((key.Key == ConsoleKey.DownArrow) && (pos != files.Length - 1))
                    {
                        pos++;
                    }

                    if (key.Key == ConsoleKey.F2)
                    {
                        Console.Clear();
                        pos = rezerv_pos;
                        stadies--;
                    }

                    if (key.Key == ConsoleKey.Enter)
                    {

                    }

                    if (key.Key == ConsoleKey.F3)
                    {
                        Console.Clear();
                        Console.WriteLine("Введите имя файла, который будет создан....");
                        try
                        {
                          File.Create(focus_files.DirectoryName + @"\" + Console.ReadLine()).Close();
                          files = new DirectoryInfo(focus_files.DirectoryName).GetFiles();
                        }
                        catch (Exception e)
                        {
                                Console.WriteLine(e.Message);
                                Console.ReadKey();
                        }
                        
                    }

                    if (key.Key == ConsoleKey.F4)
                    {
                        try
                        {
                            path = focus_files.DirectoryName;
                            focus_files.Delete();
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine(e.Message);
                            Console.ReadKey();
                        }

                        files = new DirectoryInfo(path).GetFiles();

                    }

                    if (key.Key == ConsoleKey.F5)
                    {

                        Console.Clear();
                        Console.WriteLine("Введите новый путь до файла(существующие подкаталоги, если такие есть) или новое имя файла...");

                        try
                        {
                            path = focus_files.DirectoryName;
                            focus_files.MoveTo(focus_files.DirectoryName + Console.ReadLine());
                            files = new DirectoryInfo(path).GetFiles();
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine(e.Message);
                            Console.ReadKey();
                        }
                    }

                    if (key.Key == ConsoleKey.F6)
                    {

                        Console.Clear();
                        Console.WriteLine("Введите новое положение файла...");

                        try
                        {
                            path = focus_files.DirectoryName;
                            focus_files.MoveTo(Console.ReadLine());
                            files = new DirectoryInfo(path).GetFiles();
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine(e.Message);
                            Console.ReadKey();
                        }
                    }

                    if (key.Key == ConsoleKey.F7)
                    {

                        Console.Clear();
                        Console.WriteLine("Введите куда нужно скопировать файл...");

                        try
                        {
                            focus_files.CopyTo(Console.ReadLine(), true);
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine(e.Message);
                            Console.ReadKey();
                        }
                    }

                    if (key.Key == ConsoleKey.F8)
                    {
                        Console.Clear();
                        Console.WriteLine("Имя файла: " + focus_files.Name);
                        Console.WriteLine("Имя директории, в которой содержится файл: " + focus_files.DirectoryName);
                        Console.WriteLine("Полный путь до файла: " + focus_files.FullName);
                        Console.WriteLine("Атрибуты файла: " + focus_files.Attributes);
                        Console.WriteLine("Дата создания файла: " + focus_files.LastAccessTime);
                        Console.WriteLine("Дата последнего измения файла: " + focus_files.LastWriteTime);
                        Console.ReadKey();
                    }

                    //if()

                        Console.Clear();
                    }
                    else
                    {
                        Console.WriteLine("Файлов нет в данной директории.");

                        key = Console.ReadKey();

                        if (key.Key == ConsoleKey.F2)
                        {
                            Console.Clear();
                            pos = rezerv_pos;
                            stadies--;
                        }

                        if (key.Key == ConsoleKey.F3)
                        {
                            Console.Clear();
                            //Console.WriteLine("ZZZZZ");
                            Console.WriteLine("Введите имя файла, который будет создан....");
                            //Console.WriteLine(path);
                            //Console.ReadKey();
                            File.Create(path + @"\" + Console.ReadLine()).Close();
                            files = new DirectoryInfo(path).GetFiles();
                            //Console.WriteLine(files.Length);
                            
                        }

                        Console.Clear();

                    }
                }

                //ПОШЛО ОНО НА ХУЙ БЛЯТЬ

            }
        }
    }
}
