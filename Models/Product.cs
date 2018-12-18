using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ECommerce.Models
{
    public class Product
    {
        [Key]
        public int ProductId {get;set;}

        [Required]
        [MinLength(3, ErrorMessage = "Name must be 3 characters or longer.")]
        [Display(Name = "Product Name:")]
        public string Name {get;set;}
        
        [MinLength(10, ErrorMessage = "Description must be 10 characters or longer.")]
        [Display(Name = "Description:")]
        public string Description {get;set;}

        [Display(Name = "Image(url):")]
        public string Image {get;set;}

        [Required]
        [Display(Name = "Quantity:")]
        public int Quantity {get;set;}

        [Required]
        [Display(Name = "Price:")]
        public int Price {get;set;}
        public List<Order> Orders {get;set;}
        public int CreatedById {get;set;}
        public User User {get;set;}
        public DateTime CreatedAt {get;set;} = DateTime.Now;
        public DateTime UpdatedAt {get;set;} = DateTime.Now;
    }
}