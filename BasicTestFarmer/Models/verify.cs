//------------------------------------------------------------------------------
// <auto-generated>
//     這個程式碼是由範本產生。
//
//     對這個檔案進行手動變更可能導致您的應用程式產生未預期的行為。
//     如果重新產生程式碼，將會覆寫對這個檔案的手動變更。
// </auto-generated>
//------------------------------------------------------------------------------

namespace BasicTestFarmer.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class verify
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public verify()
        {
            this.VerifyDetial = new HashSet<VerifyDetial>();
        }
    
        public int VerifyID { get; set; }
        public Nullable<int> UserID { get; set; }
        public Nullable<int> VerifyCheckID { get; set; }
        public string Remarks { get; set; }
    
        public virtual Member Member { get; set; }
        public virtual VerifyCheckID VerifyCheckID1 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<VerifyDetial> VerifyDetial { get; set; }
    }
}
