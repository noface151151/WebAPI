using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MODEL.DTO
{
    public class TimeBaseDTO
    {
        public int Id { get; set; }
        public Nullable<System.DateTime> ThoiGianTu { get; set; }
        public Nullable<System.DateTime> ThoiGianDen { get; set; }
        public string ThuTrongTuan { get; set; } 
        public Nullable<System.TimeSpan> GioTu { get; set; }
        public Nullable<System.TimeSpan> GioDen { get; set; }
        public Nullable<bool> Show { get; set; }
    }
}
