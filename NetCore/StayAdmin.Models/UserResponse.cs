using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace StayAdmin.Models
{
    public class UserResponse
    {
      
        public int Id { get; set; }           
        public string Username { get; set; }      
        public string Password { get; set; }
        
        public int? PersonId { get; set; }
        public DateTime? CreatedAt { get; set; }
        public bool IsADUser { get; set; }
        public string Token { get; set; }
        public DateTime ExpireTime { get; set; }      
        public string Identifier { get; set; }
        public int? SalesRepId { get; set; }

        [StringLength(50)]
        public string Old_IdUser { get; set; }
        public List<int> Roles { get; set; }
        public PersonResponse Person { get; set; }
        public List<string> Activities { get; set; }
    }
}
