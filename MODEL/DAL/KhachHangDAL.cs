using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MODEL.DTO;

namespace MODEL.DAL
{
    public class KhachHangDAL
    {
         APIEntities db = null;
         public KhachHangDAL()
        {
            db = new APIEntities();
        }
         public bool ThemKhachHang(KhachHang khachhang)
         {
             if (khachhang.UserName == null || khachhang.PassWord == "")
             {
                 return false;
             }
             if (LayKhachHangTheoUserName(khachhang.UserName)!=null)
             {
                 return false;
             }
             try
             {
                 db.KhachHangs.Add(khachhang);
                 db.SaveChanges();
                 return true;
             }
             catch
             {
                 return false;
             }
         }
         public bool SuaThongTin(KhachHang khachhang)
         {
             var khachhangcanupdate = db.KhachHangs.Where(x => x.UserName.ToLower() == khachhang.UserName.ToLower()).SingleOrDefault();
             if (khachhangcanupdate == null)
             {
                 return false;
             }
             else
             {
                 try
                 {
                     khachhangcanupdate.PassWord = khachhang.PassWord;
                     db.SaveChanges();
                     return true;
                 }
                 catch
                 {
                     return false;
                 }
             }
         }
         public KhachHangDTO LayKhachHangTheoUserName(string username)
         {
             KhachHangDTO khachhang = null;
             var kiemtra = db.KhachHangs.Where(x => x.UserName.ToLower() == username.ToLower()).SingleOrDefault();
             if (kiemtra != null)
             {
                 khachhang = new KhachHangDTO
                 {
                     Id = kiemtra.Id,
                     UserName = kiemtra.UserName,
                     PassWord=kiemtra.PassWord
                 };
             }
             return khachhang;
         }
         public KhachHangDTO DangNhap(KhachHang khachhangdangnhap)
         {
             KhachHangDTO khachhang = null;
             var kiemtra = db.KhachHangs.Where(x => x.UserName.ToLower() == khachhangdangnhap.UserName.ToLower()&&x.PassWord==khachhangdangnhap.PassWord).SingleOrDefault();
             if (kiemtra != null)
             {
                 khachhang = new KhachHangDTO
                 {
                     Id = kiemtra.Id,
                     UserName = kiemtra.UserName,
                     PassWord = kiemtra.PassWord
                 };
             }
             return khachhang;
         } 
         public List<KhachHangDTO> DanhSachKhachHang()
         {
             List<KhachHangDTO> danhsachKhachHang = new List<KhachHangDTO>();
             var danhsach = db.KhachHangs.ToList();
             foreach (var item in danhsach)
             {
                 KhachHangDTO khachhang = new KhachHangDTO();
                 khachhang.Id = item.Id;
                 khachhang.UserName = item.UserName;
                 khachhang.PassWord = item.PassWord;
                 danhsachKhachHang.Add(khachhang);
             }
             return danhsachKhachHang;
         }
    }
}
