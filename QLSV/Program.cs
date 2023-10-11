using System;
using System.IO;


namespace QLSV
{
    internal class Program
    {
        private static QuanLySinhVien _quanLySinhVien = new QuanLySinhVien("../../Resource/QLSV.txt");
        static void Main(string[] args)
        {
            switch (args[0].ToLower())
            {
                case "add":
                    Add(args);
                    break;
                case "search":
                    Search(args);
                    break;
                case "remove":
                    Remove(args);
                    break;
                case "export":
                    Export(args);
                    break;
                case "top":
                    Top(args);
                    break;
                case "help":
                    Help();
                    break;
                default:
                    Console.WriteLine("This function don't exist!!!");
                    break;
            }
        }
        private static void Search(string[] args)
        {
            if (args.Length < 2)
            {
                Console.WriteLine("too few argument");
                PrintHelp();
                return;
            }
            _quanLySinhVien.Search(args);
        }
        
        private static void Export(string[] args)
        {
            if (!CheckNumberOfArgumentException(args,3))
                return;
            
            _quanLySinhVien.Export(args[1],args[2]);
        }
        private static void Add(string[] args)
        {
            if (args.Length < 8)
            {
                Console.WriteLine("too few argument");
                PrintHelp();
                return;
            }
            int length = args.Length;
            string ten = String.Empty;
            for (int i = 2; i <= length - 6; i++)
            {
                ten += args[i] + " ";
            }
            SinhVien sinhvien = null;
            try
            {
                sinhvien = new SinhVien(args[1], ten, args[length - 5], float.Parse(args[length - 4])
                    , float.Parse(args[length - 3]), float.Parse(args[length - 2]), float.Parse(args[length - 1]));
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);

                Console.WriteLine("Cannot add new student");
                Console.WriteLine(
                    "Information of student must include: id [name] class_id math english literature avg");
                PrintHelp();
                return;
            }
            
            _quanLySinhVien.Add(sinhvien);
        }

        private static void Remove(string[] args)
        {
            if (!CheckNumberOfArgumentException(args, 2))
                return;
            if (_quanLySinhVien.Remove(args[1]))
            {
                Console.WriteLine("Complete remove {0}",args[1]);
            }
            else
            {
                Console.WriteLine("No student with id {0} was found",args[1]);
            }
        }

        private static void Top(string[] args)
        {
            if (!CheckNumberOfArgumentException(args, 3))
                return;
            int n = int.Parse(args[1]);
            if (args[2] =="true")
                _quanLySinhVien.Top(n,true);
            else if (args[2] == "false")
                _quanLySinhVien.Top(n,false);
            else 
                Console.WriteLine("args[2] must 'true' for descending or 'false' for ascending");
        }

        private static bool CheckNumberOfArgumentException(string[] args,int num)
        {
            if (args.Length < num)
            {
                Console.WriteLine("too few argument");
                PrintHelp();
                return false;
            }
            if (args.Length > num)
            {
                Console.WriteLine("too much argument");
                PrintHelp();
                return false;
            }
            return true;
        }
        
        private static void Help() 
        {
            Console.WriteLine("Usage:");
            Console.WriteLine("  add [id] [name] [class_id] [math] [english] [literature] [avg] - Add a new student");
            Console.WriteLine("  remove [id] - Remove a student by id");  
            Console.WriteLine("  search [value] - Search students");
            Console.WriteLine("    value: id, name or both");
            Console.WriteLine("    ex: search id201 Nguyen Van A");
            Console.WriteLine("    ex: search Nguyen Van A id201 ");
            Console.WriteLine("    ex: search id201 ");
            Console.WriteLine("    ex: search Nguyen Van A");
            Console.WriteLine("  top [numStudents] [ordering] - Show top students");
            Console.WriteLine("    ordering: true for descending, false for ascending");
            Console.WriteLine("  export [option] [filePath] - Export students to file");
            Console.WriteLine("    option: all, classId");
            Console.WriteLine("  help - Show this help information");
        }

        private static void PrintHelp()
        {
            Console.WriteLine("use 'help' for more in formation.");
        }
    }
}