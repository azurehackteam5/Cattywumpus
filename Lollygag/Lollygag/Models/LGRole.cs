using System;
using System.ComponentModel.DataAnnotations;

namespace Lollygag.Models
{
    public class LGRole
    {
        [Key]
        public Int64 RoleID { get; set; }
        public string RoleName { get; set; }
        public string IsActive { get; set; }
    }
}
