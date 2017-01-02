using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MODEL.DTO;
namespace MODEL.DAL
{
   public class SanPhamDeNghiDAL
    {
       APIEntities db = null;
       public SanPhamDeNghiDAL() {
           db = new APIEntities();
       }
       public bool ThemSanPhamDeNghi(SanPhamDeNghiDTO sanphamdenghi)
       {
           if(db.SanPhams.Where(x=>x.Id==sanphamdenghi.Id_SanPham).SingleOrDefault()==null)
           {
               return false;
           }
           try
           {
               if(sanphamdenghi.Id_SanPhamDeNghi!=null||sanphamdenghi.Id_SanPhamDeNghi.Count!=0)
               {
                   
                   
                   foreach(var item in sanphamdenghi.Id_SanPhamDeNghi)
                   {
                       SanPhamDeNghi spdn = new SanPhamDeNghi();
                       spdn.IdSanPham = sanphamdenghi.Id_SanPham;
                       spdn.IdSanPhamDN = item ?? default(int);
                       db.SanPhamDeNghis.Add(spdn);
                   }
                  
                   db.SaveChanges();
                   return true;
               }
               else
               {
                   return false;

               }
           }
           catch
           {
               return false;
           }
       }
       public List<SanPhamDTO> LaySPDeNghi(int id)    //cái này là lấy sản phẩm đã được đề nghị cho 1 sản phẩm nào đó
       {
           List<SanPhamDTO> ListSanPhamDTO = new List<SanPhamDTO>();
           var DanhSach = db.SanPhamDeNghis.Where(x => x.IdSanPham == id).Select(x => x.SanPham1).ToList();

           return ListSanPhamDTO = ConvertDTO(DanhSach);
       }
       public List<SanPhamDTO> LaySPChuaDeNghi(int id)  // cái này là lấy sản phẩm chưa được đề nghị cho 1 sản phẩm nào đó
       {
           List<SanPhamDTO> ListSanPhamDTO = new List<SanPhamDTO>();
           var DanhSachDaDeNghi = db.SanPhamDeNghis.Where(x => x.IdSanPham == id).Select(x => x.SanPham1).ToList();
           var DanhSachSanPham = db.SanPhams.Where(x=>x.Id!=id).ToList();
           var Ketqua = from x in DanhSachSanPham
                        where !DanhSachDaDeNghi.Any(y => (y.Id == x.Id))
                        select x;

           return ListSanPhamDTO = ConvertDTO(Ketqua.ToList());
       }

       public List<SanPhamDTO> LaySPDeNghi1()    //cái này là lấy sản phẩm đã có sản phẩm đề nghị
       {
           List<SanPhamDTO> ListSanPhamDTO = new List<SanPhamDTO>();

           var DanhSach = db.SanPhamDeNghis.Select(x => x.SanPham).ToList();
           var ketqua = DanhSach.Distinct();
           DanhSach = ketqua.ToList();
           return ListSanPhamDTO = ConvertDTO(DanhSach);
       }

       public List<SanPhamDTO> LaySPChuaDeNghi1() //cái này là lấy sản phẩm chưa có sản phẩm đề nghị
       {
           List<SanPhamDTO> ListSanPhamDTO = new List<SanPhamDTO>();
           var DanhSachSanPham = db.SanPhams.ToList();
           var Ketqua = from x in DanhSachSanPham
                        where !db.SanPhamDeNghis.Any(y => (y.IdSanPham == x.Id))
                        select x;

           return ListSanPhamDTO = ConvertDTO(Ketqua.ToList());
       }
       
       
       public bool SuaSanPhamDeNghi(SanPhamDeNghiDTO spdenghi)
       {
           var check=db.SanPhamDeNghis.Where(x=>x.IdSanPham==spdenghi.Id_SanPham).FirstOrDefault();
           if(check==null)
           {
               return false;
           }
           else
           {
               try
               {
                   var sanphamcansua = db.SanPhamDeNghis.Where(x => x.IdSanPham == spdenghi.Id_SanPham).ToList();
                   if(spdenghi.Id_SanPhamDeNghi==null||spdenghi.Id_SanPhamDeNghi.Count==0)
                   {
                       foreach(var item in sanphamcansua)
                       {
                           db.SanPhamDeNghis.Remove(db.SanPhamDeNghis.Where(x => x.IdSanPham == item.IdSanPham&&x.IdSanPhamDN==item.IdSanPhamDN).SingleOrDefault());
                       }
                   }
                   else
                   {
                       foreach (var item in sanphamcansua)
                       {
                           db.SanPhamDeNghis.Remove(db.SanPhamDeNghis.Where(x => x.IdSanPham == item.IdSanPham && x.IdSanPhamDN == item.IdSanPhamDN).SingleOrDefault());
                       }

                       foreach(var item in spdenghi.Id_SanPhamDeNghi)
                       {
                           SanPhamDeNghi spdn = new SanPhamDeNghi();
                           spdn.IdSanPham = spdenghi.Id_SanPham;
                           spdn.IdSanPhamDN = item ?? default(int);
                           db.SanPhamDeNghis.Add(spdn);
                       }
                   }
                   db.SaveChanges();
                   return true;
               }
               catch
               {
                   return false;
               }
           }
       }

       public List<SanPhamDTO> ConvertDTO(List<SanPham> list)
       {
           List<SanPhamDTO> ListSanPhamDTO = new List<SanPhamDTO>();
           foreach (var item in list)
           {
               SanPhamDTO sanpham = new SanPhamDTO
               {
                   Id = item.Id,
                   TenSanPham = item.TenSanPham,
                   Gia = item.Gia,
                   LuotXem = item.LuotXem,
                   LuotMua = item.LuotMua,
                   HinhAnh = item.HinhAnh,
                   MoTa = item.MoTa,
                   SoLuong = item.SoLuong
               };
               ListSanPhamDTO.Add(sanpham);
           }
           return ListSanPhamDTO;
       }
    }
}
