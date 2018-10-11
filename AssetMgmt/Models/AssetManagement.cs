using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AssetMgmt.Models
{
    public class AssetManagement
    {
        public int ID { get; set; }
        public int EmployeeID { get; set; }
        public int AssetID { get; set; }
        [Required]
        public string modelNumber { get; set; }

        //Navigation Property
        public virtual Employee Employee { get; set; }
        public virtual Asset Asset { get; set; }

    }
}