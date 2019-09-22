using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BasicTestFarmer.Models
{
    [MetadataType(typeof(ProductMetadata))]
    public partial class Product
    {
        public class ProductMetadata
        {
            [DisplayName("產品編號")]
            public int ProductID { get; set; }

            [DisplayName("產品名稱")]
            public string ProductName { get; set; }

            [DisplayName("產品說明")]
            [DataType(DataType.MultilineText)]
            public string Description { get; set; }

            [DisplayName("產品單價")]
            [DisplayFormat(DataFormatString ="{0:c0}")]
            public Nullable<int> Price { get; set; }

            [DisplayName("建立日期")]
            public System.DateTime PublishDate { get; set; }

            [DisplayName("產品庫存")]
            public Nullable<int> Quantity { get; set; }

            [DisplayName("產品定位")]
            public string ProductPositioning { get; set; }

            [DisplayName("產品狀態")]
            public bool Shelves { get; set; }

            [DisplayName("產品圖片")]
            public byte[] ProducltImage { get; set; }

            [DisplayName("供應商編號")]
            public Nullable<int> supplierID { get; set; }

            [DisplayName("QRcode")]
            public byte[] QRcode { get; set; }

            
        }
    }
}