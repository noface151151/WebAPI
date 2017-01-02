using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MODEL.DTO;

namespace MODEL.DAL
{
    public class TheLoaiDAL
    {
        APIEntities db = null;
        public TheLoaiDAL()
        {
            db = new APIEntities();
        }
        public List<SanPhamTheLoaiDTO> LayTheLoaiTheoSP(int id)
        {
            List<SanPhamTheLoaiDTO> danhsachTheLoai = new List<SanPhamTheLoaiDTO>();
            var danhsach = db.SanPhamTheLoais.Where(x => x.Id_SanPham == id).ToList();
            foreach (var item in danhsach)
            {
                SanPhamTheLoaiDTO theloai = new SanPhamTheLoaiDTO();
                theloai.Id_SanPham = item.Id_SanPham;
                theloai.Id_TheLoai = item.Id_TheLoai;
                danhsachTheLoai.Add(theloai);
            }
            return danhsachTheLoai;
        }         
        public bool ThemTheLoai(TheLoai theloai)
        {
            if(theloai.TenTheLoai==null||theloai.TenTheLoai=="")
            {
                return false;
            }
            try
            {
                db.TheLoais.Add(theloai);
                db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }
        public bool SuaTheLoai(TheLoai theloai)
        {
            var theloaicanupdate = db.TheLoais.Where(x => x.Id == theloai.Id).SingleOrDefault();
            if (theloaicanupdate == null)
            {
                return false;
            }
            else
            {
                try
                {
                    theloaicanupdate.TenTheLoai = theloai.TenTheLoai;
                    db.SaveChanges();
                    return true;
                }
                catch
                {
                    return false;
                }
            }
        }
        public TheLoaiDTO LayTheLoaiTheoID(int id)
        {
            TheLoaiDTO theloai = null;
            var kiemtra = db.TheLoais.Where(x => x.Id == id).SingleOrDefault();
            if (kiemtra != null)
            {
                theloai = new TheLoaiDTO
                {
                    Id = kiemtra.Id,
                    TenTheLoai = kiemtra.TenTheLoai
                };
            }
            return theloai;
        }
        public List<TheLoaiDTO> DanhSachTheLoai()
        {
            List<TheLoaiDTO> danhsachTheLoai = new List<TheLoaiDTO>();
            var danhsach = db.TheLoais.ToList();
            foreach (var item in danhsach)
            {
                TheLoaiDTO theloai = new TheLoaiDTO();
                theloai.Id = item.Id;
                theloai.TenTheLoai = item.TenTheLoai;
                danhsachTheLoai.Add(theloai);
            }
            TheLoaiDTO theloaikhac = new TheLoaiDTO();
            theloaikhac.Id = 0;
            theloaikhac.TenTheLoai = "Khác";
            danhsachTheLoai.Add(theloaikhac);
            return danhsachTheLoai;
        }
        public List<TheLoaiDTO> DanhSachTheLoaiWinForm()
        {
            List<TheLoaiDTO> danhsachTheLoai = new List<TheLoaiDTO>();
            var danhsach = db.TheLoais.ToList();
            foreach (var item in danhsach)
            {
                TheLoaiDTO theloai = new TheLoaiDTO();
                theloai.Id = item.Id;
                theloai.TenTheLoai = item.TenTheLoai;
                danhsachTheLoai.Add(theloai);
            }
            return danhsachTheLoai;
        }
    }
}
