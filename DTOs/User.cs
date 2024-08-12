using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs
{
    public class User : BaseDTO
    {
        [Required]
        public string Identity { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string LastName1 { get; set; }
        [Required]
        public string LastName2 { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string PhoneNumber { get; set; }
        [Required]
        public int NumDpt { get; set; }
        [Required]
        public DateTime Hour { get; set; }
        public string Condominium { get; set; }
        public string EntryMethod { get; set; }
        public string Role { get; set; }
        public string Password { get; set; }
    }
}
