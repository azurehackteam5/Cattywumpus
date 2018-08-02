using System;
using System.ComponentModel.DataAnnotations;

namespace Lollygag.Models
{
    public class LGCatalog
    {
        [Key]
        public Int64 CatalogID { get; set; }
        public string CatalogName { get; set; }
        public string CompanyID { get; set; }
        public string IsActive { get; set; }
    }
}
