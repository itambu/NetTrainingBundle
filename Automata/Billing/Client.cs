//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Billing.Model
{
    using System;
    using System.Collections.Generic;
    
    public partial class Client
    {
        public Client()
        {
            this.Contracts = new HashSet<Contract>();
        }
    
        public System.Guid Id { get; set; }
        public string FullName { get; set; }
    
        public virtual ICollection<Contract> Contracts { get; set; }
    }
}
