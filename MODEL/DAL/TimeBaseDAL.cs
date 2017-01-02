using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MODEL.DTO;

namespace MODEL.DAL
{
   public class TimeBaseDAL
    {
       APIEntities db = null;

       public TimeBaseDAL() {
           db = new APIEntities(); 
       }
     /*  public bool ThemTimeBaseChoSP(TimeBaseDTO timebase)
       {
           var sp = db.SanPhams.Where(x => x.Id == timebase.Id_SanPham).SingleOrDefault();
           if(sp==null)
           {
               return false;
           }
           List<TimeBase> listtb = new List<TimeBase>();
           try
           {
               
               foreach (var item in timebase.ThuTrongTuanList)
               {
                   TimeBase tb = new TimeBase();
                   tb.ThoiGianTu = timebase.ThoiGianTu;
                   tb.ThoiGianDen = timebase.ThoiGianDen;
                   tb.ThuTrongTuan = item;
                   tb.GioTu = timebase.GioTu;
                   tb.GioDen = timebase.GioDen;
                   tb.Show = timebase.Show;
                   db.TimeBases.Add(tb);
                   listtb.Add(tb);
               }
               db.SaveChanges();
               foreach (var item in listtb)
               {                 
                       SanPham_Timebase sptb = new SanPham_Timebase();
                       sptb.Id_SanPham = timebase.Id_SanPham;
                       sptb.Id_TimeBase = item.Id;                      
                       db.SanPham_Timebase.Add(sptb);
                   
               }
               db.SaveChanges();
               return true;
           }
           catch
           {
               foreach (var item in listtb)
               {
                   var itemdelete = db.TimeBases.Where(x => x.Id == item.Id).SingleOrDefault();
                   db.TimeBases.Remove(itemdelete);
               }
               db.SaveChanges();
               return false;
           }
       }*/
       public List<TimeBaseDTO> DanhSachTimeBase()
       {
           List<TimeBaseDTO> kq = new List<TimeBaseDTO>();
           var list = db.TimeBases.ToList();
           foreach(var item in list)
           {
               TimeBaseDTO tb = new TimeBaseDTO();
               tb.Id = item.Id;
               tb.ThoiGianTu = item.ThoiGianTu;
               tb.ThoiGianDen = item.ThoiGianDen;
               tb.ThuTrongTuan = item.ThuTrongTuan;
               tb.GioTu = item.GioTu;
               tb.GioDen = item.GioDen;
               tb.Show = item.Show;
               kq.Add(tb);
           }
           return kq;
       }
       public bool ThemTimeBase(TimeBase timebase)
       {
           try
           {
               db.TimeBases.Add(timebase);
               db.SaveChanges();
               return true;
           }
           catch
           {
               return false;
           }         
       }
       public bool SuaTimeBase(TimeBaseDTO timebase)
       {
           var timebaseupdate = db.TimeBases.Where(x => x.Id == timebase.Id).SingleOrDefault();
           if (timebaseupdate == null)
           {
               return false;
           }
           else
           {
               try
               {
                   timebaseupdate.ThoiGianTu = timebase.ThoiGianTu;
                   timebaseupdate.ThoiGianDen = timebase.ThoiGianDen;
                   timebaseupdate.ThuTrongTuan = timebase.ThuTrongTuan;
                   timebaseupdate.GioTu = timebase.GioTu;
                   timebaseupdate.GioDen = timebase.GioDen;
                   timebaseupdate.Show = timebase.Show;
                   db.SaveChanges();
                   return true;
               }
               catch
               {
                   return false;
               }
           }
       }
       public List<TimeBaseDTO> TimeBaseTheoSP(int id)
        {
            List<TimeBaseDTO> list = new List<TimeBaseDTO>();
            var timebase = db.SanPham_Timebase.Where(x => x.Id_SanPham == id).Select(x => x.TimeBase).ToList();
           foreach(var item in timebase)
           {
               TimeBaseDTO tb = new TimeBaseDTO();              
               tb.Id = item.Id;
               tb.ThoiGianTu = item.ThoiGianTu;
               tb.ThoiGianDen = item.ThoiGianDen;
               tb.ThuTrongTuan = item.ThuTrongTuan;
               tb.GioTu = item.GioTu;
               tb.GioDen = item.GioDen;
               tb.Show = item.Show;
               list.Add(tb);
           }
           return list;
        }


       public bool ThemTimebaseSP(SanPhamDTO sanpham)
       {
           var sp = db.SanPhams.Where(x => x.Id == sanpham.Id).SingleOrDefault();
           if(sp==null)
           {
               return false;
           }
           try
           {
                foreach(var item in sanpham.Id_TimeBase)
                {
                    SanPham_Timebase sptb = new SanPham_Timebase();
                    sptb.Id_SanPham = sanpham.Id;
                    sptb.Id_TimeBase = item ?? default(int);
                    db.SanPham_Timebase.Add(sptb);
                }
                db.SaveChanges();
                return true;
           }
           catch
           {
               return false;
           }
       }
       public bool SuaTimebaseSP(SanPhamDTO sanpham)
       {
           var sp = db.SanPhams.Where(x => x.Id == sanpham.Id).SingleOrDefault();
           if (sp == null)
           {
               return false;
           }
           try
           {
               var oldlist = TimeBaseTheoSP(sanpham.Id);
               foreach(var item in oldlist)
               {
                   var oldval = db.SanPham_Timebase.Where(x => x.Id_SanPham == sanpham.Id && x.Id_TimeBase == item.Id).SingleOrDefault();
                   db.SanPham_Timebase.Remove(oldval);
               }
               foreach (var item in sanpham.Id_TimeBase)
               {
                   SanPham_Timebase sptb = new SanPham_Timebase();
                   sptb.Id_SanPham = sanpham.Id;
                   sptb.Id_TimeBase = item ?? default(int);
                   db.SanPham_Timebase.Add(sptb);
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
}
