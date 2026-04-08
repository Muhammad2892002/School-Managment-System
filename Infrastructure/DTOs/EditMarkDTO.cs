using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.DTOs
{
    public class EditMarkDTO
    {
        public long StudentId { get; set; }
        public string? StdName { get; set; }
        public string? SubjectName { get; set; }

        public int SubjectId { get; set; }

        public decimal? OldMark { get; set; } = null;
        [Required(ErrorMessage ="Enter valid mark or press cancel")]
        public decimal? NewMark { get; set; } = null;



    }
}
