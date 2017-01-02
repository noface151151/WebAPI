using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MODEL.DTO
{
   public class DonHangDTO
    {
        public int Id { get; set; }
        public string NguoiNhan { get; set; }
        public string DiaChiGiao { get; set; }
        public string SoDienThoai { get; set; }
        public string Email { get; set; }

        public Nullable<DateTime> NgayGiaodate { get; set; }
        public string NgayGiao { get; set; }
        public string TrangThai { get; set; }
        public int Id_KhachHang { get; set; }
        public string User { get; set; }
        public Nullable<decimal> TongTien { get; set; }
        public List<GioHangClient> giohang { get; set; }
    }
}
