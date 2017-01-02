using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MODEL.DTO;

namespace MODEL.DAL
{
    public class DonHangDAL
    {
         APIEntities db = null;
         public DonHangDAL()
        {
            db = new APIEntities();
        }
         public List<DonHangDTO> DanhSachDonHang()
         {
             return ConvertDTODonHang(db.DonHangs.OrderBy(x=>x.NgayGiao).ToList());
         }
         public bool ThemDonHang(DonHangDTO donhang)
         {
             try
             {
                 DonHang dh=new DonHang();
                 dh.NguoiNhan=donhang.NguoiNhan;
                 dh.DiaChiGiao=donhang.DiaChiGiao;
                 dh.SoDienThoai=donhang.SoDienThoai;
                 dh.Email=donhang.Email;
                 dh.NgayGiao=Convert.ToDateTime(donhang.NgayGiao);
                 dh.TrangThai="Chưa xử lý";
                 dh.Id_KhachHang=donhang.Id_KhachHang;
                 dh.TongTien=donhang.TongTien;
                 db.DonHangs.Add(dh);
                 foreach(var item in donhang.giohang)
                 {
                     var sp = db.SanPhams.Where(x => x.Id == item.Id_SanPham).SingleOrDefault();
                     sp.SoLuong -= item.SoLuong;
                     sp.LuotMua += item.SoLuong;
                     CTDonHang ctdh = new CTDonHang();
                     ctdh.Id_SanPham = item.Id_SanPham;
                     ctdh.Id_DonHang = dh.Id;
                     ctdh.SoLuong = item.SoLuong;
                     db.CTDonHangs.Add(ctdh);
                 }
                 db.SaveChanges();
                 return true;
             }
             catch
             {
                 return false;
             }
         }
         public bool DoiTrangThai(DonHangDTO donhang)
         {
             var donhangcandoi = db.DonHangs.Where(x => x.Id == donhang.Id).SingleOrDefault();
             if(donhangcandoi==null)
             {
                 return false;
             }
             try
             {
                 donhangcandoi.TrangThai = "Đã xử lý";
                 db.SaveChanges();
                 return true;
             }
             catch
             {
                 return false;
             }
         }
         public List<DonHangDTO> DanhSachDonHangKhachHang(int id)
         {
             return ConvertDTODonHang(db.DonHangs.Where(x => x.Id_KhachHang == id).OrderBy(x => x.NgayGiao).ToList());
         }
         public List<CTDonHangDTO> DanhSachCTDonHang(int id)
         {
             return ConvertDTOctDonHang(db.CTDonHangs.Where(x => x.Id_DonHang == id).ToList());
         }



        public List<DonHangDTO> ConvertDTODonHang(List<DonHang> list)
         {
             List<DonHangDTO> ListDonHangDTO = new List<DonHangDTO>();
             foreach (var item in list)
             {
                 DonHangDTO donhang = new DonHangDTO
                 {
                     Id = item.Id,
                     NguoiNhan = item.NguoiNhan,
                     DiaChiGiao = item.DiaChiGiao,
                     SoDienThoai = item.SoDienThoai,
                     Email = item.Email,
                     NgayGiaodate = item.NgayGiao,
                     TrangThai = item.TrangThai,
                     TongTien=item.TongTien,
                     User = item.KhachHang.UserName
                 };
                 ListDonHangDTO.Add(donhang);
             }
             return ListDonHangDTO;
         }

        public List<CTDonHangDTO> ConvertDTOctDonHang(List<CTDonHang> list)
        {
            List<CTDonHangDTO> ListCTDonHangDTO = new List<CTDonHangDTO>();
            foreach (var item in list)
            {
                CTDonHangDTO ctdonhang = new CTDonHangDTO
                {
                    Id_DonHang = item.Id_DonHang,
                    SanPham = item.SanPham.TenSanPham,
                    SoLuong = item.SoLuong,
                    DonGia=item.SanPham.Gia,
                    HinhAnh=item.SanPham.HinhAnh,
                    GiamoiSp=item.SanPham.Gia*item.SoLuong 
                };
                ListCTDonHangDTO.Add(ctdonhang);
            }
            return ListCTDonHangDTO;
        }
    }
}
