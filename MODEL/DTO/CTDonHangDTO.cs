using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MODEL.DTO
{
    public class CTDonHangDTO
    {
        public string SanPham { get; set; }
        public int Id_DonHang { get; set; }
        public string HinhAnh { get; set; }
        public Nullable<decimal> DonGia { get; set; }
        public Nullable<decimal> GiamoiSp { get; set; }
        public Nullable<int> SoLuong { get; set; }
    }
}
