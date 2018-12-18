using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ECommerce.Models
{
  public class User
    {
        [Key]
        public int UserId {get;set;}

        [Required]
        [MinLength(3)]
        [Display(Name = "First Name:")]
        public string FirstName {get;set;}

        [Required]
        [MinLength(3)]
        [Display(Name = "Last Name:")]
        public string LastName {get;set;}

        [Required]
        [EmailAddress]
        [Display(Name = "Email:")]
        public string Email {get;set;}


        [Required]
        [DataType(DataType.Password)]
        [MinLength(8, ErrorMessage="Password must be 8 characters or longer!")]
        [Display(Name = "Password:")]
        public string Password {get;set;}
        public DateTime CreatedAt {get;set;} = DateTime.Now;
        public DateTime UpdatedAt {get;set;} = DateTime.Now;

        [NotMapped]
        [Compare("Password")]
        [DataType(DataType.Password)]
        [Display(Name = "Confirm Password:")]
        public string Confirm {get;set;}

        [InverseProperty("SupplierUser")]
        public List<Customer> Customers { get; set; }
 
        [InverseProperty("CustomerUser")]
        public List<Customer> Suppliers { get; set; }
    }
}