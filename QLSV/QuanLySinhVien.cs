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
                    string ten = string.Empty;
                    int length = words.Length;
                    for (int i = 2; i <= length - 5; i++)
                    {
                        ten += words[i] + " ";
                    }
                    ten = ten.TrimEnd();
                    SinhVien sinhvien = new SinhVien(words[1],ten,words[0],float.Parse(words[length-4]),float.Parse(words[length-3]),float.Parse(words[length-2]),float.Parse(words[length-1]));
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

        public void Search(string[] criteria)
        {
            List<SinhVien> results = new List<SinhVien>();

            string studentId1 = criteria[0];
            string studentId2 = criteria[criteria.Length-1];

            string studentName1 = String.Join(" ", criteria, 1, criteria.Length - 2);
            string studentName2 = String.Join(" ", criteria, 2, criteria.Length - 2);
            string studentName3 = String.Join(" ", criteria, 1, criteria.Length - 1);
            string Id = "";
            string Name = "";
            foreach (SinhVien student in List)
            {
                Id = student.Mssv;
                Name = student.Ten;
                
                bool idMatched = Id == studentId1 || Id == studentId2;
                bool nameMatched = Name == studentName1 || Name == studentName2|| Name == studentName3 ;
    
                if (idMatched || nameMatched) {
                    results.Add(student);
                }
            }

            if (results.Count == 0) {
                Console.WriteLine("No suitable students were found.");
                return;
            }

            printList(results);
        }
        
        public bool Remove(string mssv)
        {
            bool flag = false;
            string tempFile = Path.GetTempFileName();
            using (var sw = new StreamWriter(tempFile))
            {
                foreach (SinhVien sinhvien in List)
                {
                    if (sinhvien.Mssv != mssv)
                        sw.WriteLine(sinhvien.ToString());
                    else
                        flag = true;
                }
            }
            File.Delete(filePath);
            File.Move(tempFile,filePath);
            return flag;
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
                if ((i != length - 1) && (List[i].Dtb != List[i + 1].Dtb) )
                    count++;
                if (count > n)
                    break;
            }
        }

        public void Export(string option, string destPath) 
        {
            List<SinhVien> tempList;
            string directory = Path.GetDirectoryName(destPath);
            if (string.IsNullOrEmpty(directory))
            {
                directory = AppDomain.CurrentDomain.BaseDirectory + destPath;
                Console.WriteLine("File will be saved to default directory: " + directory);
            }

            if (option == "all") {
                tempList = List; 
            }
            else {
                tempList = List.Where(sv => sv.Malop == option).ToList();
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
            catch (DirectoryNotFoundException )
            {
                Console.WriteLine("Could not find a part of the path " + destPath);
            }
            catch (IOException)
            {
                try
                {
                    File.Copy(tempFile, destPath, true);
                    Console.WriteLine("Overwrite " + destPath);
                }
                catch (Exception)
                {
                    Console.WriteLine("Could not find a part of the path " + destPath);
                }
            }
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