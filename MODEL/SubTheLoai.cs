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
    
    public partial class SubTheLoai
    {
        public SubTheLoai()
        {
            this.SanPhamSubTheLoais = new HashSet<SanPhamSubTheLoai>();
        }
    
        public int Id { get; set; }
        public string TenTheLoaiSub { get; set; }
        public Nullable<int> Id_TheLoai { get; set; }
    
        public virtual TheLoai TheLoai { get; set; }
        public virtual ICollection<SanPhamSubTheLoai> SanPhamSubTheLoais { get; set; }
    }
}