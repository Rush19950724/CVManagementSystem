using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CVManagementSystem.Models
{
    public class CVCollection
    {
        [Key]
        public int ID { get; set; }
        public string CVIDs { get; set; }
        [ForeignKey("User")]
        public int CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }

    }
}
