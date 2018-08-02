using System;
using System.ComponentModel.DataAnnotations;

namespace Lollygag.Models
{
    public class LGUser
    {
        [Key]
        public Int64 UserID { get; set; }
        public Int64 CompanyID { get; set; }
        public string EmailAddress { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Int64 RoleID { get; set; }
        public string IsActive { get; set; }
        public DateTime AuditDate { get; set; }
        public Int64 AuditUserID { get; set; }
    }
}
