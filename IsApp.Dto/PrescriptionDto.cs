using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISApp.Dto
{
    public class PrescriptionDto
    {       
        public int Id { get; set; }

        [Required]
        [Display(Name = "Expiration Date")]
        public DateTime ExpirationDate { get; set; }

        [Required]
        [Display(Name = "Product Name")]
        public string ProductName { get; set; }

        [Required]
        [Display(Name = "Uses Left")]
        public int UsesLeft { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        [Display(Name = "Active")]
        public bool IsActive { get; set; }

        [Required]
        [Display(Name = "Patient Id")]
        public int PatientId { get; set; }

        public string PatientName { get; set; }
    }

}
