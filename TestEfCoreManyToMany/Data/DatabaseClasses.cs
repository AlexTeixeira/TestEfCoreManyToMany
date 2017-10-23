using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace TestEfCoreManyToMany.Data
{
    public abstract class BaseDao
    {
        [Key]
        public int Id { get; set; }
        public DateTime LastUpdate { get; set; }
    }

    public class Hobbie : BaseDao
    {
        [Required]
        [StringLengthAttribute(26)]
        public string Name { get; set; }

        public ICollection<HobbieUsersHobbies> HobbieUsers { get; set; }
        public ICollection<FacebookUsersHobbies> FacebookUsers { get; set; }
    }

    public class FacebookUser : User
    {
        [Required]
        public string FacebookId { get; set; }

        [Required]
        public ICollection<FacebookUsersHobbies> Hobbies { get; set; }
    }


    public abstract class User : BaseDao
    {
        [Required]
        [MaxLength(30)]
        public string UserName { get; set; }

        public string Language { get; set; }

        public int? LocationId { get; set; }
        public Location Location { get; set; }

        [ForeignKey("Parent")]
        public int? ParentId { get; set; }
        public User Parent { get; set; }
        [InverseProperty("Parent")]
        public ICollection<User> Children { get; set; } = new HashSet<User>();

    }

    public class FacebookUsersHobbies
    {
        public int UserId { get; set; }
        public FacebookUser User { get; set; }

        public int HobbieId { get; set; }
        public Hobbie Hobbie { get; set; }
    }

    public class HobbieUser : User
    {
        [Required]
        public byte[] Password { get; set; }

        /// <summary>
        /// This is the salt secret hash to compare password
        /// </summary>
        [Required]
        public byte[] Guid { get; set; }

        [Required]
        [EmailAddress]
        [MaxLength(255)]
        public string Email { get; set; }
        [MaxLength(255)]
        public string ProfilePicture { get; set; }

        [Required]
        public ICollection<HobbieUsersHobbies> HobbieUsersHobbies { get; set; }



    }

    public class HobbieUsersHobbies
    {

        public int HobbieUserId { get; set; }
        public HobbieUser HobbieUser { get; set; }

        public int HobbieId { get; set; }
        public Hobbie Hobbie { get; set; }
    }

    /*ublic class Follower : BaseDao
    {
        public int? UserId { get; set; }
        public User User
        {
            get;
            set;
        }
    }*/

    public class Location : BaseDao
    {
        [Required]
        [Range(-90, 90)]
        public double Longitude { get; set; }
        [Required]
        [Range(-90, 90)]
        public double Latitude { get; set; }
        [MaxLength(50)]

        public string City { get; set; }
        [MaxLength(50)]

        public string Country { get; set; }
        [MaxLength(255)]

        public string Address { get; set; }
    }
}
