using System.ComponentModel.DataAnnotations;

namespace SneakersStore.Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Range(1,100,ErrorMessage ="Display Order must be in range 1 - 100")]
        [Display(Name ="Display Order")]
        public int DisplayOrder { get; set; }
    }
}
