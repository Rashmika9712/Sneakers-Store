using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SneakersStore.Models
{
    public class ShoeItem
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public string Description { get; set; }

        public string Image { get; set; }

        [Range(1000,10000,ErrorMessage ="Price should be between Rs 1,000 and Rs 10,000")]
        public double Price { get; set; }

        [Display(Name ="Shoe Type")]
        public int ShoeTypeId { get; set; }
        [ForeignKey("ShoeTypeId")]
        public ShoeType ShoeType { get; set; }

        [Display(Name= "Category")]
        public int CategoryId { get; set; }
        public Category Category { get; set; }

    }
}
