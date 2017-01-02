using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MODEL.DTO
{
    public class SanPhamDTO
    {
        public int Id { get; set; }
        public string TenSanPham { get; set; }
        public Nullable<decimal> Gia { get; set; }
        public Nullable<int> LuotXem { get; set; }
        public Nullable<int> LuotMua { get; set; }
        public string HinhAnh { get; set; }
        public string MoTa { get; set; }
        public Nullable<int> SoLuong { get; set; }
        public List<int?> Id_TheLoai { get; set; }
        public List<int?> Id_SubTheLoai { get; set; }
        public List<int?> Id_TimeBase { get; set; }

    }
}
