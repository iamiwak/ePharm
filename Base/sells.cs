//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ePharm.Base
{
    using System;
    using System.Collections.Generic;
    
    public partial class sells
    {
        public int id { get; set; }
        public int drugId { get; set; }
        public int userId { get; set; }
        public System.DateTime date { get; set; }
    
        public virtual drugTypes drugTypes { get; set; }
        public virtual users users { get; set; }
        public virtual drugs drugs { get; set; }
    }
}
