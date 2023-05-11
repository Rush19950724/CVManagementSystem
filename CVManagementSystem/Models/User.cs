using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CVManagementSystem.Models
{
    public class User
    {
        [Key]
        public int ID { get; set; }
        public string Email { get; set; }
        public string HashValue { get; set; }
        public string SaltValue { get; set; }
        public DateTime CreatedDate { get; set; }
        [ForeignKey("Role")]
        public int RoleID { get; set; }
        public DateTime LastModifiedDate { get; set;}
        [ForeignKey("UserStatus")]
        public int? StatusID { get; set; }
    }
}
