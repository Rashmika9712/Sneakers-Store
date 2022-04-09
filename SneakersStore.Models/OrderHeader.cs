using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SneakersStore.Models
{
    public class OrderHeader
    {
        [Key]
        public int Id { get; set; }

        [Display(Name ="User Id")]
        [Required]        
        public string UserId { get; set; }
        [ForeignKey("UserId")]
        [ValidateNever]
        public ApplicationUser ApplicationUser { get; set; }

        [Display(Name ="Order Date")]
        [Required]
        public DateTime OrderDate { get; set; }

        [Display(Name ="Order Total")]
        [DisplayFormat(DataFormatString ="{0:C}")]
        [Required]
        public double OrderTotal { get; set; }

        [Required]
        [Display(Name ="Pick Up Time")]
        public DateTime PickUpTime { get; set; }

        [Required]
        [NotMapped]
        [Display(Name ="Pick Up Date")]
        public DateTime PickUpDate { get; set; }

        public string Status { get; set; }

        public string? Comments { get; set; }
        public string? SessionId { get; set; }
        public string? PaymentIntentId { get; set; }

        [Display(Name ="Pick Up Name")]
        [Required]
        public string PickUpName { get; set; }

        [Display(Name ="Phone Number")]
        [DataType(DataType.PhoneNumber)]
        [Required]
        public string PhoneNumber { get; set; }
    }
}
