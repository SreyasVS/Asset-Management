using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AssetMgmt.Models
{
    public class Asset
    {
        public int ID { get; set; }
        [Required]
        public string AssetName { get; set; }

        //Navigation Property
        public virtual ICollection<AssetManagement> AssetManagements { get; set; }

    }
}