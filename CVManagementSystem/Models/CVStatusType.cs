using System.ComponentModel.DataAnnotations;

namespace CVManagementSystem.Models
{
    public class CVStatusType
    {
        [Key]
        public int ID { get; set; }
        public string Name { get; set; }
    }
}
