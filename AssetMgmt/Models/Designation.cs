using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AssetMgmt.Models
{
    public class Designation
    {
        public int ID { get; set; }
        [Required]
        public string DesignationName { get; set; }

        //Naviagtion Property
        public virtual ICollection<Employee> Employees { get; set; }

    }
}