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
    
    public partial class Product
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Product()
        {
            this.ActivitIDetail = new HashSet<ActivitIDetail>();
            this.OrderDetail = new HashSet<OrderDetail>();
            this.ProductCommen = new HashSet<ProductCommen>();
            this.ProductSaleslist = new HashSet<ProductSaleslist>();
            this.Shopping = new HashSet<Shopping>();
            this.SubProductCategories = new HashSet<SubProductCategories>();
        }
    
        public int ProductID { get; set; }
        public string ProductName { get; set; }
        public string Description { get; set; }
        public Nullable<int> Price { get; set; }
        public System.DateTime PublishDate { get; set; }
        public Nullable<int> Quantity { get; set; }
        public string ProductPositioning { get; set; }
        public bool Shelves { get; set; }
        public byte[] ProducltImage { get; set; }
        public Nullable<int> supplierID { get; set; }
        public byte[] QRcode { get; set; }
        public Nullable<int> ShelvesStateID { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ActivitIDetail> ActivitIDetail { get; set; }
        public virtual Member Member { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<OrderDetail> OrderDetail { get; set; }
        public virtual ShelvesState ShelvesState { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ProductCommen> ProductCommen { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ProductSaleslist> ProductSaleslist { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Shopping> Shopping { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SubProductCategories> SubProductCategories { get; set; }
    }
}
