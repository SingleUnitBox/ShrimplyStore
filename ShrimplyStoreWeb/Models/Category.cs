using System.ComponentModel.DataAnnotations;

namespace ShrimplyStoreWeb.Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Species { get; set; }
        public int DisplayOrder { get; set; }
    }
}
