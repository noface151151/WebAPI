//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace MODEL
{
    using System;
    using System.Collections.Generic;
    
    public partial class TimeBase
    {
        public TimeBase()
        {
            this.SanPham_Timebase = new HashSet<SanPham_Timebase>();
        }
    
        public int Id { get; set; }
        public Nullable<System.DateTime> ThoiGianTu { get; set; }
        public Nullable<System.DateTime> ThoiGianDen { get; set; }
        public string ThuTrongTuan { get; set; }
        public Nullable<System.TimeSpan> GioTu { get; set; }
        public Nullable<System.TimeSpan> GioDen { get; set; }
        public Nullable<bool> Show { get; set; }
    
        public virtual ICollection<SanPham_Timebase> SanPham_Timebase { get; set; }
    }
}
