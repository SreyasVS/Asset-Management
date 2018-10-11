using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AssetMgmt.Models
{
    public class Employee
    {
        public int ID { get; set; }
        [Required]
        public string EmployeeName { get; set; }
        public int DesignationID { get; set; }

        //Navigation Property
        public virtual ICollection<AssetManagement> AssetManagements { get; set; }
        public virtual Designation Designation { get; set; }

    }
}