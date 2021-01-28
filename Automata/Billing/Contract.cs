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
    
    public partial class Contract
    {
        public Contract()
        {
            this.ContractBillingPlanBinding = new HashSet<ContractBillingPlanBinding>();
        }
    
        public System.Guid Id { get; set; }
        public System.DateTime ContractStartDate { get; set; }
        public Nullable<System.DateTime> ContractCloseDate { get; set; }
    
        public virtual Client Client { get; set; }
        public virtual ICollection<ContractBillingPlanBinding> ContractBillingPlanBinding { get; set; }
        public virtual Terminal Terminal { get; set; }
    }
}
