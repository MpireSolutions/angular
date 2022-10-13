using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace StayAdmin.Models
{
    public class UserRequest
    {
        public int Id { get; set; }

        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        public string FirstName { get; set; }
        public string LastName { get; set; }

        [Required]
        public string Email { get; set; }
        public int? PersonId { get; set; }
        public DateTime? CreatedAt { get; set; }
        public bool IsADUser { get; set; }
        public string Token { get; set; }
        public DateTime? ExpireTime { get; set; }

        [StringLength(50)]
        public string Old_IdUser { get; set; }

        [Required]
        public List<int> Roles { get; set; }
        public PersonRequest Person { get; set; }
    }
}
