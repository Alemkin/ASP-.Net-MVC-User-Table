using System.Web.Mvc;

namespace FirstDemoProjectForRPbyAL.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    
    public partial class User
    {
        [Display(Name = "User ID")]
        public int UserId { get; set; }

        public int CustomerId { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = "First name cannot be longer than 50 characters.")]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = "Last name cannot be longer than 50 characters.")]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Required]
        [Display(Name= "Email Address")]
        public string EmailAddress { get; set; }

        [Required]
        [DataType(DataType.Password, ErrorMessage = "Password is invalid")]
        public string Password { get; set; }
        
        public System.DateTimeOffset AddedOn { get; set; }
        public System.DateTimeOffset ChangedOn { get; set; }
        public bool Inactive { get; set; }
        public bool IsAdministrator { get; set; }
    }
}
