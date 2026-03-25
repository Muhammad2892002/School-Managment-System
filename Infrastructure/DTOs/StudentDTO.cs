using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.DTOs
{
    public class StudentDTO
    {

        public long Id { get; set; }

        [Required(ErrorMessage ="First Name required")]
        public string FirstName { get; set; } = null!;
        public string? GenderName { get; set; }
        [Required(ErrorMessage = "Social Number is required")]

        public long NationalId { get; set; }
        [Required(ErrorMessage = "Birth Date is required")]

        public DateTime BirthDate { get; set; }

        public DateTime? CreateDate { get; set; }

        public DateTime? UpdateDate { get; set; }
        [Required(ErrorMessage = "Birth place is required")]
        public int Governorate { get; set; }
        public string ? GovernorateName { get; set; }
        [Required(ErrorMessage = "Gender is required")]

        public bool  Gender { get; set; }
        [Required(ErrorMessage = "Last Name required")]
        public string LastName { get; set; } 
    }
}
