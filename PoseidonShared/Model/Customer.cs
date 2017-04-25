//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace PoseidonShared.Model
{
    using System;
    using System.Collections.Generic;
    
    public partial class Customer
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Customer()
        {
            this.Invoices = new HashSet<Invoice>();
        }
    
        public int Id { get; set; }
        public string CustomerName { get; set; }
        public Nullable<int> Age { get; set; }
        public string Sex { get; set; }
        public string Phone { get; set; }
        public string OrganizationName { get; set; }
        public Nullable<bool> MedicalStore { get; set; }
        public string Cell { get; set; }
        public Nullable<bool> Dou { get; set; }
        public string BimaNo { get; set; }
        public string Email { get; set; }
        public Nullable<bool> DefaultCustomer { get; set; }
        public Nullable<System.DateTime> PDate { get; set; }
        public Nullable<System.DateTime> RegDate { get; set; }
        public string Category { get; set; }
        public string Address { get; set; }
        public Nullable<decimal> Debit { get; set; }
        public Nullable<decimal> Credit { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Invoice> Invoices { get; set; }
    }
}
