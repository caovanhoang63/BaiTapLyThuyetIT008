using System;


namespace QLSV
{
    internal class Program
    {
        private static QuanLySinhVien _quanLySinhVien = new QuanLySinhVien("../../Resource/QLSV.txt");
        static void Main(string[] args)
        {
            switch (args[0])
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
                default:
                    Console.WriteLine("This function don't exist!!!");
                    break;
            }
        }
        private static void Search(string[] args)
        {
            string input1 = args[1];
            string input2 = null;
            if (args.Length == 3)
                input2 = args[2];
            _quanLySinhVien.Search(input1,input2);
        }
        
        private static void Export(string[] args)
        {
            if (!CheckNumberOfArgumentException(args,3))
                return;
            _quanLySinhVien.Export(args[1],args[2]);
        }
        private static void Add(string[] args)
        {
            if (!CheckNumberOfArgumentException(args,8))
                return;
            SinhVien sinhvien = new SinhVien(args[1], args[2], args[3], float.Parse(args[4])
                , float.Parse(args[5]), float.Parse(args[6]), float.Parse(args[7]));
            _quanLySinhVien.Add(sinhvien);
        }

        private static void Remove(string[] args)
        {
            if (!CheckNumberOfArgumentException(args, 2))
                return;
            _quanLySinhVien.Remove(args[1]);
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
                return false;
            }
            if (args.Length > num)
            {
                Console.WriteLine("too much argument");
                return false;
            }
            return true;
        }
    }
}