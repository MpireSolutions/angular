using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace StayAdmin.Data.Entities
{
    [Table("AppUser")]
    
    public class User : Trackable
    {
        public User()
        {
            UserRoles = new List<UserRole>();
            UserTokens = new HashSet<UserToken>();
        }

        [Key]
        public new int Id { get; set; }

        [Required]
        [StringLength(255)]
        public string Username { get; set; }

        [Required]
        public byte[] PasswordHash { get; set; }

        [Required]
        public byte[] PasswordSalt { get; set; }

        public int? PersonId { get; set; }       
        public bool? IsADUser { get; set; }
        public bool IsActive { get; set; }

        [StringLength(255)]
        
        public string Identifier { get; set; }
    

        [StringLength(50)]
        public string Old_IdUser { get; set; }

        public List<UserRole> UserRoles { get; set; }
        public ICollection<UserToken>  UserTokens { get; set; }        
        public Person Person { get; set; }

    }
}
