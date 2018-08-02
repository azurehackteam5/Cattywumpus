using System;
using System.ComponentModel.DataAnnotations;

namespace Lollygag.Models
{
    public class LGCompany
    {
        [Key]
        public Int64 CompanyID { get; set; }
        public string CatalogName { get; set; }
        public string IsActive { get; set; }
    }
}
