using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BasicTestFarmer.Models
{
    [MetadataType(typeof(MemberMetadata))]
    public partial class Member
    {
        public class MemberMetadata
        {
            [DisplayName("會員編號")]
            public int UserID { get; set; }

            [DisplayName("會員帳號")]
            [Required]
            public string UserAccount { get; set; }

            [DisplayName("會員密碼")]
            [Required]
            public string UsePass { get; set; }
            public string GUID { get; set; }

            [DisplayName("會員姓名")]
            [Required]
            public string UserName { get; set; }

            [DisplayName("會員電話")]
            [Required]
            public string UserPhone { get; set; }

            [DisplayName("性別")]
            [Required]
            public bool UserSex { get; set; }

            [DisplayName("城市")]
            [Required]
            public string City { get; set; }

            [DisplayName("地區")]
            [Required]
            public string Region { get; set; }

            [DisplayName("地址")]
            [Required]
            public string Adress { get; set; }

            [DisplayName("Email")]
            [Required]
            public string EMail { get; set; }

            public Nullable<System.DateTime> RegistetedDate { get; set; }
            public Nullable<bool> Permission { get; set; }
            public Nullable<int> SourceInformationID { get; set; }
            public Nullable<int> IdentityID { get; set; }
        }
    }
}