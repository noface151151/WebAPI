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
    
    public partial class SanPham_Timebase
    {
        public int Id_SanPham { get; set; }
        public int Id_TimeBase { get; set; }
        public Nullable<int> temp { get; set; }
    
        public virtual SanPham SanPham { get; set; }
        public virtual TimeBase TimeBase { get; set; }
    }
}
