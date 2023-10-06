using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;


namespace QLSV
{
    public class QuanLySinhVien
    {
        public QuanLySinhVien(string filePath)
        {
            this.filePath = filePath;
            List = new List<SinhVien>();
            using (StreamReader reader = new StreamReader(filePath))
            {
                string line;
                
                while ((line = reader.ReadLine()) != null)
                {
                    string[] words = line.Split(' ');
                    SinhVien sinhvien = new SinhVien(words[1],words[2],words[0],float.Parse(words[3]),float.Parse(words[4]),float.Parse(words[5]),float.Parse(words[6]));
                    List.Add(sinhvien);
                }
            }
        }
        
        public List<SinhVien> List { get; }
        private string filePath; 
        public void Add(SinhVien sinhvien)
        {
            using (FileStream fs = new FileStream(filePath,FileMode.Append, FileAccess.Write))
            {
                StreamWriter w = new StreamWriter(fs);
                
                long endPoint=fs.Length;
                fs.Seek(endPoint, SeekOrigin.Begin);
                w.WriteLine(sinhvien);
                w.Flush();
            }
        }

        public void Search(string input1, string input2 = null )
        {
            List<SinhVien> result = new List<SinhVien>();
            foreach (SinhVien sinhvien in List)
            {
                if (sinhvien.Mssv == input1 ||
                    sinhvien.Ten.Replace(' ','_')  == input1 ||
                    sinhvien.Mssv == input2 ||
                    sinhvien.Ten.Replace(' ','_')  == input2)
                {
                    result.Add(sinhvien);
                }
            }

            printList(result);
        }
        
        public void Remove(string mssv)
        {

            string tempFile = Path.GetTempFileName();
            using (var sw = new StreamWriter(tempFile))
            {
                foreach (SinhVien sinhvien in List)
                {
                    if(sinhvien.Mssv != mssv)
                        sw.WriteLine(sinhvien.ToString());
                }
            }
            File.Delete(filePath);
            File.Move(tempFile,filePath);
        }
        
        private void TemplatePrint()
        {
            Console.WriteLine("{0,5} {1,10} {2,10} {3,20} {4,10} {5,10} {6,10} {7,10}", 
                "STT", "Malop", "MSSV", "Ten", "Toan", "Anh", "Van", "DTB");
            Console.WriteLine(new string('-', 92));
            
        }
        
        public void Top(int n,bool order)
        {
            TemplatePrint();
            if (order)
                List.Sort((x, y) => y.Dtb.CompareTo(x.Dtb));
            else
                List.Sort();
            int length = List.Count;
            if (length == 0) return;
            int count = 1;
                for(int i = 0 ; i < length - 1 ;i++)
            {
                Console.WriteLine("{0,5} {1,10} {2,10} {3,20} {4,10} {5,10} {6,10} {7,10}",
                    count, List[i].Malop, List[i].Mssv, List[i].Ten, List[i].DiemToan,
                    List[i].DiemAnh, List[i].DiemVan, List[i].Dtb);
                if (i != length - 1 && List[i].Dtb != List[i + 1].Dtb )
                    count++;
                if (count > n)
                    break;
            }
        }

        public void Export(string order, string destPath) 
        {
            List<SinhVien> tempList;
  
            if (order == "all") {
                tempList = List; 
            }
            else {
                tempList = List.Where(sv => sv.Malop == order).ToList();
            }

            string tempFile = Path.GetTempFileName();

            using (var sw = new StreamWriter(tempFile)) {
                foreach (var sv in tempList) {
                    sw.WriteLine(sv);
                }
            }

            try
            {
                File.Move(tempFile, destPath);
                Console.WriteLine("Create " + destPath);
            }
            catch (Exception)
            {
                File.Copy(tempFile, destPath, true);
                Console.WriteLine("Overwrite " + destPath);
            }
        }

        private bool IsInClass(string maLop, SinhVien sinhVien)
        {
            if (sinhVien.Malop == maLop)
                return true;
            return false;
        }
        private void printList(List<SinhVien> list)
        {
            TemplatePrint();
            for(int i = 0 ; i < list.Count; i++)
            {
                Console.WriteLine("{0,5} {1,10} {2,10} {3,20} {4,10} {5,10} {6,10} {7,10}",
                    i + 1 , list[i].Malop, list[i].Mssv, list[i].Ten, list[i].DiemToan, list[i].DiemAnh, list[i].DiemVan, list[i].Dtb);
            }
        }
    }
}