using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MODEL.DTO;


namespace MODEL.DAL
{
    public class SanPhamDAL
    {
        APIEntities db = null;
        TheLoaiDAL theloaiDAL = new TheLoaiDAL();
        SubTheLoaiDAL subtheloaiDAL = new SubTheLoaiDAL();
        public SanPhamDAL()
        {
            db = new APIEntities();
        }
        public List<SanPhamDTO> DanhSachSanPham()
        {
            List<SanPhamDTO> ListSanPhamDTO = new List<SanPhamDTO>();
            var DanhSach = db.SanPhams.OrderByDescending(x=>x.Id).ToList();

            return ListSanPhamDTO = ConvertDTO(DanhSach);
        }
        public List<SanPhamDTO> DanhSachSanPhamHot()
        {
            List<SanPhamDTO> ListSanPhamDTO = new List<SanPhamDTO>();
            var DanhSach = db.SanPhams.Where(x=>x.LuotXem!=null).Take(5).ToList();

            return ListSanPhamDTO = ConvertDTO(DanhSach);
        }
        public List<SanPhamDTO> SanPhamKhac()
        {
            List<SanPhamDTO> ListSanPhamDTO = new List<SanPhamDTO>();
            var DanhSach = db.SanPhams.Where(x => !x.SanPhamTheLoais.Any(m => m.Id_SanPham == x.Id) && !x.SanPhamSubTheLoais.Any(n => n.Id_SanPham == x.Id)).ToList();

            return ListSanPhamDTO = ConvertDTO(DanhSach);
        }
        public bool ThemSanPham(SanPhamDTO sanpham)
        {
            SanPham sp = new SanPham();
            if (sanpham.TenSanPham == null || sanpham.Gia == null || sanpham.HinhAnh == null)
            {
                return false;
            }

            try
            {
                if (sanpham.Id_TheLoai == null && sanpham.Id_SubTheLoai == null)
                {
                    sp.TenSanPham = sanpham.TenSanPham;
                    sp.Gia = sanpham.Gia;
                    sp.HinhAnh = sanpham.HinhAnh;
                    sp.MoTa = sanpham.MoTa;
                    sp.SoLuong = sanpham.SoLuong;
                    sp.LuotMua = 0;
                    sp.LuotXem = 0;
                    db.SanPhams.Add(sp);
                    db.SaveChanges();
                    return true;
                }
                else if (sanpham.Id_TheLoai != null && sanpham.Id_SubTheLoai == null)
                {
                    sp.TenSanPham = sanpham.TenSanPham;
                    sp.Gia = sanpham.Gia;
                    sp.HinhAnh = sanpham.HinhAnh;
                    sp.MoTa = sanpham.MoTa;
                    sp.SoLuong = sanpham.SoLuong;
                    sp.LuotMua = 0;
                    sp.LuotXem = 0;
                    db.SanPhams.Add(sp);

                    foreach (var item in sanpham.Id_TheLoai)
                    {
                        SanPhamTheLoai sptl = new SanPhamTheLoai();
                        sptl.Id_SanPham = sp.Id;
                        sptl.Id_TheLoai = item ?? default(int);
                        db.SanPhamTheLoais.Add(sptl);

                    }
                    db.SaveChanges();
                    return true;
                }
                else if (sanpham.Id_TheLoai == null && sanpham.Id_SubTheLoai != null)
                {
                    sp.TenSanPham = sanpham.TenSanPham;
                    sp.Gia = sanpham.Gia;
                    sp.HinhAnh = sanpham.HinhAnh;
                    sp.MoTa = sanpham.MoTa;
                    sp.SoLuong = sanpham.SoLuong;
                    sp.LuotMua = 0;
                    sp.LuotXem = 0;
                    db.SanPhams.Add(sp);


                    foreach (var item in sanpham.Id_SubTheLoai)
                    {
                        SanPhamSubTheLoai spstl = new SanPhamSubTheLoai();
                        spstl.Id_SanPham = sp.Id;
                        spstl.Id_SubTheLoai = item ?? default(int);
                        db.SanPhamSubTheLoais.Add(spstl);
                    }
                    db.SaveChanges();
                    return true;
                }
                else
                {
                    sp.TenSanPham = sanpham.TenSanPham;
                    sp.Gia = sanpham.Gia;
                    sp.HinhAnh = sanpham.HinhAnh;
                    sp.MoTa = sanpham.MoTa;
                    sp.SoLuong = sanpham.SoLuong;
                    sp.LuotMua = 0;
                    sp.LuotXem = 0;
                    db.SanPhams.Add(sp);

                    foreach (var item in sanpham.Id_TheLoai)
                    {
                        SanPhamTheLoai sptl = new SanPhamTheLoai();
                        sptl.Id_SanPham = sp.Id;
                        sptl.Id_TheLoai = item ?? default(int);
                        db.SanPhamTheLoais.Add(sptl);
                    }

                    foreach (var item in sanpham.Id_SubTheLoai)
                    {
                        SanPhamSubTheLoai spstl = new SanPhamSubTheLoai();
                        spstl.Id_SanPham = sp.Id;
                        spstl.Id_SubTheLoai = item ?? default(int);
                        db.SanPhamSubTheLoais.Add(spstl);
                    }
                    db.SaveChanges();
                    return true;
                }

            }
            catch
            {
                return false;
            }
        }
        public bool SuaSanPham(SanPhamDTO sanpham)
        {

            var sanphamcanupdate = db.SanPhams.Where(x => x.Id == sanpham.Id).SingleOrDefault();
            if (sanphamcanupdate == null)
            {
                return false;
            }
            else
            {
                try
                {
                    sanphamcanupdate.Gia = sanpham.Gia;
                    sanphamcanupdate.HinhAnh = sanpham.HinhAnh;
                    sanphamcanupdate.MoTa = sanpham.MoTa;
                    sanphamcanupdate.SoLuong = sanpham.SoLuong;
                    sanphamcanupdate.TenSanPham = sanpham.TenSanPham;

                    var TheLoaiSanPham = theloaiDAL.LayTheLoaiTheoSP(sanphamcanupdate.Id);
                    var SubTheLoaiSanPham = subtheloaiDAL.LaySubTheLoaiTheoSP(sanphamcanupdate.Id);

                    if (sanpham.Id_TheLoai == null && sanpham.Id_SubTheLoai == null)
                    {
                        if (TheLoaiSanPham != null)
                        {
                            foreach (var item in TheLoaiSanPham)
                            {
                                var theloaisanpham = db.SanPhamTheLoais.Where(x => x.Id_SanPham == item.Id_SanPham && x.Id_TheLoai == item.Id_TheLoai).SingleOrDefault();
                                db.SanPhamTheLoais.Remove(theloaisanpham);

                            }
                        }
                        if (SubTheLoaiSanPham != null)
                        {
                            foreach (var item in SubTheLoaiSanPham)
                            {
                                var Subtheloaisanpham = db.SanPhamSubTheLoais.Where(x => x.Id_SanPham == item.Id_SanPham && x.Id_SubTheLoai == item.Id_SubTheLoai).SingleOrDefault();
                                db.SanPhamSubTheLoais.Remove(Subtheloaisanpham);
                            }
                        }
                        db.SaveChanges();
                        return true;
                    }
                    else if (sanpham.Id_TheLoai != null && sanpham.Id_SubTheLoai == null)
                    {
                        if (TheLoaiSanPham != null)
                        {
                            foreach (var item in TheLoaiSanPham)
                            {
                                var theloaisanpham = db.SanPhamTheLoais.Where(x => x.Id_SanPham == item.Id_SanPham && x.Id_TheLoai == item.Id_TheLoai).SingleOrDefault();
                                db.SanPhamTheLoais.Remove(theloaisanpham);
                            }
                            foreach (var item in sanpham.Id_TheLoai)
                            {
                                SanPhamTheLoai sptl = new SanPhamTheLoai();
                                sptl.Id_SanPham = sanphamcanupdate.Id;
                                sptl.Id_TheLoai = item ?? default(int);
                                db.SanPhamTheLoais.Add(sptl);
                            }
                        }
                        if (TheLoaiSanPham == null)
                        {
                            foreach (var item in sanpham.Id_TheLoai)
                            {
                                SanPhamTheLoai sptl = new SanPhamTheLoai();
                                sptl.Id_SanPham = sanphamcanupdate.Id;
                                sptl.Id_TheLoai = item ?? default(int);
                                db.SanPhamTheLoais.Add(sptl);
                            }
                        }
                        if (SubTheLoaiSanPham != null)
                        {
                            foreach (var item in SubTheLoaiSanPham)
                            {
                                var Subtheloaisanpham = db.SanPhamSubTheLoais.Where(x => x.Id_SanPham == item.Id_SanPham && x.Id_SubTheLoai == item.Id_SubTheLoai).SingleOrDefault();
                                db.SanPhamSubTheLoais.Remove(Subtheloaisanpham);
                            }
                        }
                        db.SaveChanges();
                        return true;
                    }
                    else if (sanpham.Id_TheLoai == null && sanpham.Id_SubTheLoai != null)
                    {
                        if (TheLoaiSanPham != null)
                        {
                            foreach (var item in TheLoaiSanPham)
                            {
                                var theloaisanpham = db.SanPhamTheLoais.Where(x => x.Id_SanPham == item.Id_SanPham && x.Id_TheLoai == item.Id_TheLoai).SingleOrDefault();
                                db.SanPhamTheLoais.Remove(theloaisanpham);
                            }
                        }
                        if (SubTheLoaiSanPham != null)
                        {
                            foreach (var item in SubTheLoaiSanPham)
                            {
                                var Subtheloaisanpham = db.SanPhamSubTheLoais.Where(x => x.Id_SanPham == item.Id_SanPham && x.Id_SubTheLoai == item.Id_SubTheLoai).SingleOrDefault();
                                db.SanPhamSubTheLoais.Remove(Subtheloaisanpham);
                            }
                            foreach (var item in sanpham.Id_SubTheLoai)
                            {
                                SanPhamSubTheLoai spstl = new SanPhamSubTheLoai();
                                spstl.Id_SanPham = sanphamcanupdate.Id;
                                spstl.Id_SubTheLoai = item ?? default(int);
                                db.SanPhamSubTheLoais.Add(spstl);
                            }
                        }
                        if (SubTheLoaiSanPham == null)
                        {
                            foreach (var item in sanpham.Id_SubTheLoai)
                            {
                                SanPhamSubTheLoai spstl = new SanPhamSubTheLoai();
                                spstl.Id_SanPham = sanphamcanupdate.Id;
                                spstl.Id_SubTheLoai = item ?? default(int);
                                db.SanPhamSubTheLoais.Add(spstl);
                            }
                        }
                        db.SaveChanges();
                        return true;
                    }
                    else
                    {
                        if (TheLoaiSanPham != null)
                        {
                            foreach (var item in TheLoaiSanPham)
                            {
                                var theloaisanpham = db.SanPhamTheLoais.Where(x => x.Id_SanPham == item.Id_SanPham && x.Id_TheLoai == item.Id_TheLoai).SingleOrDefault();
                                db.SanPhamTheLoais.Remove(theloaisanpham);

                            }
                            foreach (var item in sanpham.Id_TheLoai)
                            {
                                SanPhamTheLoai sptl = new SanPhamTheLoai();
                                sptl.Id_SanPham = sanphamcanupdate.Id;
                                sptl.Id_TheLoai = item ?? default(int);
                                db.SanPhamTheLoais.Add(sptl);
                            }
                        }
                        if (SubTheLoaiSanPham != null)
                        {
                            foreach (var item in SubTheLoaiSanPham)
                            {
                                var Subtheloaisanpham = db.SanPhamSubTheLoais.Where(x => x.Id_SanPham == item.Id_SanPham && x.Id_SubTheLoai == item.Id_SubTheLoai).SingleOrDefault();
                                db.SanPhamSubTheLoais.Remove(Subtheloaisanpham);
                            }
                            foreach (var item in sanpham.Id_SubTheLoai)
                            {
                                SanPhamSubTheLoai spstl = new SanPhamSubTheLoai();
                                spstl.Id_SanPham = sanphamcanupdate.Id;
                                spstl.Id_SubTheLoai = item ?? default(int);
                                db.SanPhamSubTheLoais.Add(spstl);
                            }
                        }
                        if (SubTheLoaiSanPham == null)
                        {
                            foreach (var item in sanpham.Id_SubTheLoai)
                            {
                                SanPhamSubTheLoai spstl = new SanPhamSubTheLoai();
                                spstl.Id_SanPham = sanphamcanupdate.Id;
                                spstl.Id_SubTheLoai = item ?? default(int);
                                db.SanPhamSubTheLoais.Add(spstl);
                            }
                        }
                        if (TheLoaiSanPham == null)
                        {
                            foreach (var item in sanpham.Id_TheLoai)
                            {
                                SanPhamTheLoai sptl = new SanPhamTheLoai();
                                sptl.Id_SanPham = sanphamcanupdate.Id;
                                sptl.Id_TheLoai = item ?? default(int);
                                db.SanPhamTheLoais.Add(sptl);
                            }
                        }
                        db.SaveChanges();
                        return true;
                    }
                }
                catch
                {
                    return false;
                }
            }
        }


        public List<SanPhamDTO> LaySanPhamTheoSubTheLoai(int maSubtheloai)
        {
            List<SanPhamDTO> ListSanPhamDTO = new List<SanPhamDTO>();
            return ListSanPhamDTO = ConvertDTO(SanPhamTheoSubTheLoai(maSubtheloai));
        }
        public List<SanPhamDTO> LaySanPhamTheoTheLoai(int matheloai)
        {
            List<SanPhamDTO> ListSanPhamDTO = new List<SanPhamDTO>();
            return ListSanPhamDTO = ConvertDTO(SanPhamTheoTheLoai(matheloai));
        }
        public List<SanPhamDTO> LaySanPhamlienquanSubTheLoai(int maSubtheloai)
        {
            List<SanPhamDTO> ListSanPhamDTO = new List<SanPhamDTO>();
            return ListSanPhamDTO = ConvertDTO(SanPhamLienQuanSubTheLoai(maSubtheloai));
        }

        public SanPhamDTO LaySanPhamTheoId(int id)
        {
            SanPhamDTO sanpham = null;
            var SP = db.SanPhams.Where(x => x.Id == id).SingleOrDefault();
            if (SP != null)
            {
                sanpham = new SanPhamDTO
                {
                    Id = SP.Id,
                    TenSanPham = SP.TenSanPham,
                    Gia = SP.Gia,
                    LuotXem = SP.LuotXem,
                    LuotMua = SP.LuotMua,
                    HinhAnh = SP.HinhAnh,
                    MoTa = SP.MoTa,
                    SoLuong = SP.SoLuong
                };
            }

            return sanpham;
        }

        public bool TangLuotXem(int id)
        {
            try
            {
                var sp = db.SanPhams.Where(x => x.Id == id).SingleOrDefault();
                if(sp==null)
                {
                    return false;
                }
                else
                {
                    sp.LuotXem = sp.LuotXem+1;
                    db.SaveChanges();
                    return true;
                }
            }
            catch
            {
                return false;
            }
        }
        public List<SanPham> SanPhamTheoTheLoai(int? id)
        {
            if (id == 0)
            {
                return db.SanPhams.Where(x => !x.SanPhamTheLoais.Any(m => m.Id_SanPham == x.Id) && !x.SanPhamSubTheLoais.Any(n => n.Id_SanPham == x.Id)).ToList();
            }
            else
            {
                return db.SanPhamTheLoais.Where(x => x.Id_TheLoai == id).Select(x => x.SanPham).ToList();
            }
        }
        public List<SanPham> SanPhamTheoSubTheLoai(int? id)
        {
            return db.SanPhamSubTheLoais.Where(x => x.Id_SubTheLoai == id).Select(x => x.SanPham).ToList();
        }
        public List<SanPham> SanPhamLienQuanSubTheLoai(int? id)
        {
            return db.SanPhamSubTheLoais.Where(x => x.Id_SubTheLoai == id).Select(x => x.SanPham).Take(8).ToList();
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




        //dành cho time-base

        

        public List<SanPhamDTO> GetSPTimeBase()
        {
            List<SanPham> list = new List<SanPham>();
            DateTime date = DateTime.Now.Date;
            TimeSpan time = DateTime.Now.TimeOfDay;
            string day = date.DayOfWeek.ToString();
            var a = db.TimeBases.Where(x => (x.ThoiGianTu <= date && x.ThoiGianDen >= date && x.GioTu <= time && x.GioDen >= time && x.ThuTrongTuan == day && x.Show == true) || ((x.ThoiGianTu >= date || x.ThoiGianDen <= date || x.GioTu >= time || x.GioDen <= time || x.ThuTrongTuan != day) && x.Show == false)).Select(x => x.SanPham_Timebase.Select(y => y.SanPham).Where(z => z.SoLuong > 0)).ToList();
            foreach(var item in a)
            {
                foreach(var i in item)
                {
                    list.Add(i);
                }
            }
            var ketqua = list.Distinct();
            list = ketqua.ToList();
            var DanhSachSanPham = db.SanPhams.ToList();
            var SPKoTimebase = from x in DanhSachSanPham
                         where !db.SanPham_Timebase.Any(y => (y.Id_SanPham == x.Id))
                         select x;
            foreach (var item in SPKoTimebase)
            {
                list.Add(item);
            }
            list = list.OrderBy(x => x.Id).ToList();
            return ConvertDTO(list);
        }


        public List<SanPhamDTO> SanPhamKhacTimeBase()
        {
          /*  List<SanPhamDTO> ListSanPhamDTO = new List<SanPhamDTO>();
            var DanhSach = db.SanPhams.Where(x => !x.SanPhamTheLoais.Any(m => m.Id_SanPham == x.Id) && !x.SanPhamSubTheLoais.Any(n => n.Id_SanPham == x.Id)).ToList();

            return ListSanPhamDTO = ConvertDTO(DanhSach);*/
            List<SanPhamDTO> ListSanPhamDTO = new List<SanPhamDTO>();
            List<SanPhamDTO> ListTimeBase = new List<SanPhamDTO>();
            List<SanPhamDTO> KetQua = new List<SanPhamDTO>();
            ListSanPhamDTO = SanPhamKhac();
            ListTimeBase = GetSPTimeBase();
            foreach (var item1 in ListSanPhamDTO)
            {
                foreach (var item2 in ListTimeBase)
                {
                    if (item1.Id == item2.Id)
                    {
                        KetQua.Add(item2);
                    }
                }
            }
            return KetQua;
        }
        public List<SanPhamDTO> LaySanPhamTheoSubTheLoaiTimebase(int maSubtheloai)
        {
             List<SanPhamDTO> ListSanPhamDTO = new List<SanPhamDTO>();
             List<SanPhamDTO> ListTimeBase = new List<SanPhamDTO>();
             List<SanPhamDTO> KetQua = new List<SanPhamDTO>();
             ListSanPhamDTO = ConvertDTO(SanPhamTheoSubTheLoai(maSubtheloai));
             ListTimeBase = GetSPTimeBase();
            foreach(var item1 in ListSanPhamDTO)
            {
                foreach(var item2 in ListTimeBase)
                {
                    if(item1.Id==item2.Id)
                    {
                        KetQua.Add(item2);
                    }
                }
            }
            return KetQua;
        }
        public List<SanPhamDTO> LaySanPhamTheoTheLoaiTimebase(int matheloai)
        {
            List<SanPhamDTO> ListSanPhamDTO = new List<SanPhamDTO>();
            List<SanPhamDTO> ListTimeBase = new List<SanPhamDTO>();
            List<SanPhamDTO> KetQua = new List<SanPhamDTO>();
            ListSanPhamDTO = ConvertDTO(SanPhamTheoTheLoai(matheloai));
            ListTimeBase = GetSPTimeBase();
            foreach (var item1 in ListSanPhamDTO)
            {
                foreach (var item2 in ListTimeBase)
                {
                    if (item1.Id == item2.Id)
                    {
                        KetQua.Add(item2);
                    }
                }
            }
            return KetQua;
        }
        public List<SanPhamDTO> LaySanPhamlienquanSubTheLoaiTimebase(int maSubtheloai)
        {
            List<SanPhamDTO> ListSanPhamDTO = new List<SanPhamDTO>();
            List<SanPhamDTO> ListTimeBase = new List<SanPhamDTO>();
            List<SanPhamDTO> KetQua = new List<SanPhamDTO>();
            ListSanPhamDTO = ConvertDTO(SanPhamLienQuanSubTheLoai(maSubtheloai));
            ListTimeBase = GetSPTimeBase();
            foreach (var item1 in ListSanPhamDTO)
            {
                foreach (var item2 in ListTimeBase)
                {
                    if (item1.Id == item2.Id)
                    {
                        KetQua.Add(item2);
                    }
                }
            }
            return KetQua;
        }
    }
}


