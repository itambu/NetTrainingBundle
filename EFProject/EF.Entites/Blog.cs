//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace EF.Entites
{
    using System;
    using System.Collections.Generic;
    
    public partial class Blog
    {
        public Blog()
        {
            this.Comments = new HashSet<Comment>();
        }
    
        public int Id { get; set; }
        public string Title { get; set; }
        public string Text { get; set; }
    
        public virtual User Author { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }
    }
}
