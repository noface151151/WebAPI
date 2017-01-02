using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MODEL.DTO;

namespace MODEL.DAL
{
   public  class SubTheLoaiDAL
    {
       APIEntities db = null;
       public SubTheLoaiDAL()
        {
            db = new APIEntities();
        }
       public List<SanPhamSubTheLoaiDTO> LaySubTheLoaiTheoSP(int id)
       {
           List<SanPhamSubTheLoaiDTO> danhsachSubTheLoai = new List<SanPhamSubTheLoaiDTO>();
           var danhsach = db.SanPhamSubTheLoais.Where(x => x.Id_SanPham == id).ToList();
           foreach (var item in danhsach)
           {
               SanPhamSubTheLoaiDTO subtheloai = new SanPhamSubTheLoaiDTO();
               subtheloai.Id_SanPham = item.Id_SanPham;
               subtheloai.Id_SubTheLoai = item.Id_SubTheLoai;
               danhsachSubTheLoai.Add(subtheloai);
           }
           return danhsachSubTheLoai;
       }
       public int LaySubTheLoaiTheoSP1(int id)
       {
           var Subtheloai = db.SanPhamSubTheLoais.Where(x => x.Id_SanPham == id).FirstOrDefault();
           if (Subtheloai != null)
           {
               return Subtheloai.Id_SubTheLoai;
           }
           return 0;
       }
       public List<SubTheLoaiDTO> DanhSachSubTheLoai()
       {
           List<SubTheLoaiDTO> danhsachSubTheLoai = new List<SubTheLoaiDTO>();
           var danhsach = db.SubTheLoais.ToList();
           foreach (var item in danhsach)
           {
               SubTheLoaiDTO Subtheloai = new SubTheLoaiDTO();
               Subtheloai.Id = item.Id;
               Subtheloai.TenTheLoaiSub = item.TenTheLoaiSub;
               Subtheloai.Id_TheLoai = item.Id_TheLoai;
               danhsachSubTheLoai.Add(Subtheloai);
           }
           return danhsachSubTheLoai;
       }
       public SubTheLoaiDTO LaySubTheLoaiTheoID(int id)
       {
           SubTheLoaiDTO Subtheloai = null;
           var kiemtra = db.SubTheLoais.Where(x => x.Id == id).SingleOrDefault();
           if (kiemtra != null)
           {
               Subtheloai = new SubTheLoaiDTO
               {
                   Id = kiemtra.Id,
                   TenTheLoaiSub = kiemtra.TenTheLoaiSub
               };
           }
           return Subtheloai;
       }
       public bool ThemSubTheLoai(SubTheLoai subtheloai)
       {
           if (subtheloai.TenTheLoaiSub == null||subtheloai.TenTheLoaiSub=="")
           {
               return false;
           }
           try
           {
               db.SubTheLoais.Add(subtheloai);
               db.SaveChanges();
               return true;
           }
           catch
           {
               return false;
           }
       }
       public bool SuaSubTheLoai(SubTheLoai subtheloai)
       {
           if (subtheloai.TenTheLoaiSub == null || subtheloai.TenTheLoaiSub == "")
           {
               return false;
           }
           var Subtheloaicansua = db.SubTheLoais.Where(x => x.Id == subtheloai.Id).SingleOrDefault();
           if (Subtheloaicansua != null)
           {
               try
               {
                   Subtheloaicansua.TenTheLoaiSub = subtheloai.TenTheLoaiSub;
                   Subtheloaicansua.Id_TheLoai = subtheloai.Id_TheLoai;
                   db.SaveChanges();
                   return true;
               }
               catch
               {
                   return false;
               }
           }
           return false;
       }
       public List<SubTheLoaiDTO> SubTheLoaiTheoTheLoai(int matheloai)
       {
           List<SubTheLoaiDTO> danhsachSubTheLoai = new List<SubTheLoaiDTO>();
           if (matheloai == 0)
           {
               var danhsach = db.SubTheLoais.Where(x => x.Id_TheLoai == null).ToList();
               foreach (var item in danhsach)
               {
                   SubTheLoaiDTO Subtheloai = new SubTheLoaiDTO();
                   Subtheloai.Id = item.Id;
                   Subtheloai.TenTheLoaiSub = item.TenTheLoaiSub;
                   danhsachSubTheLoai.Add(Subtheloai);
               }
           }
           else
           {
               var danhsach = db.SubTheLoais.Where(x => x.Id_TheLoai == matheloai).ToList();
               foreach (var item in danhsach)
               {
                   SubTheLoaiDTO Subtheloai = new SubTheLoaiDTO();
                   Subtheloai.Id = item.Id;
                   Subtheloai.TenTheLoaiSub = item.TenTheLoaiSub;
                   danhsachSubTheLoai.Add(Subtheloai);
               }
           }
           return danhsachSubTheLoai;
       }
       public List<SubTheLoaiDTO> SubTheLoaiKhac()
       {
           List<SubTheLoaiDTO> danhsachSubTheLoai = new List<SubTheLoaiDTO>();
           var danhsach = db.SubTheLoais.Where(x => x.Id_TheLoai == null).ToList();
           foreach (var item in danhsach)
           {
               SubTheLoaiDTO Subtheloai = new SubTheLoaiDTO();
               Subtheloai.Id = item.Id;
               Subtheloai.TenTheLoaiSub = item.TenTheLoaiSub;
               danhsachSubTheLoai.Add(Subtheloai);
           }
           return danhsachSubTheLoai;
       }
    }
}
