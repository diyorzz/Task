using System.ComponentModel.DataAnnotations;

namespace CLOUPARD_Test_task.Entities
{
    public class Product
    {
        [Key]
        public Guid Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
