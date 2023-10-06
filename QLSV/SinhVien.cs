using System;

namespace QLSV
{
    public class SinhVien : IComparable
    {
        public SinhVien(){}
        
        public SinhVien(string mssv, string ten, string malop,  float diemToan, float diemAnh, float diemVan, float dtb)
        {
            this.malop = malop;
            this.mssv = mssv;
            this.ten = ten;
            this.diemToan = diemToan;
            this.diemAnh = diemAnh;
            this.diemVan = diemVan;
            this.dtb = dtb;
        }

        public string Malop
        {
            get => malop;
            set => malop = value;
        }

        public string Mssv
        {
            get => mssv;
            set => mssv = value;
        }

        public string Ten
        {
            get =>ten.Replace('_',' ');
            set => ten = value;
        }

        public float DiemToan
        {
            get => diemToan;
            set
            {
                if (value > 0 && value < 10)
                {
                    diemToan = value;
                }
                else
                {
                    throw new Exception("Diem khong hop le");
                }
            }
        }

        public float DiemAnh
        {
            get => diemAnh;
            set
            {
                if (value > 0 && value < 10 )
                {
                    diemAnh = value;
                }
                else
                {
                    throw new Exception("Diem khong hop le");
                }
            }
        }

        public float DiemVan
        {
            get => diemVan;
            set
            {
                if (value > 0 && value < 10)
                {
                    diemVan = value;
                }
                else
                {
                    throw new Exception("Diem khong hop le");
                }
            }
        }

        public float Dtb
        {
            get => dtb;
            set
            {
                if (value > 0 && value < 10)
                {
                    dtb = value;
                }
                else
                {
                    throw new Exception("Diem khong hop le");
                }
            }
        }

        private string malop;
        private string mssv;
        private string ten;
        private float diemToan;
        private float diemAnh;
        private float diemVan;
        private float dtb;


        public override string ToString()
        {
            return Malop + " " + Mssv + " " + Ten.Replace(' ','_') + " " +
                   DiemAnh + " " +DiemAnh + " " +DiemVan + " " + Dtb;
        }
        public int CompareTo(object obj)
        {
            if(obj == null) return 1;

            SinhVien other = obj as SinhVien;
            if(other != null) {
                return this.Dtb.CompareTo(other.Dtb);
            } else {
                throw new ArgumentException("Object is not a SinhVien");
            }
        }
    }
}