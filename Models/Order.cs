using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ECommerce.Models
{
    public class Order
    {
        [Key]
        public int OrderId {get;set;}

        [Required]
        [Display(Name = "Customer:")]
        public int CustomerId {get;set;}
        public Customer Customer {get;set;}
        public int CreatedById {get;set;}
        public User CreatedBy {get;set;}
        
        [Required]
        [Display(Name = "Quantity:")]
        public int Quantity {get;set;}

        [Required]
        [Display(Name = "Product:")]
        public int ProductId {get;set;}
        public Product Product {get;set;}

        [Display(Name = "Order Description:")]
        [MinLength(10, ErrorMessage = "Description must be 10 characters or longer.")]
        public string OrderDescription {get;set;}
        public DateTime CreatedAt {get;set;} = DateTime.Now;
        public DateTime UpdatedAt {get;set;} = DateTime.Now;
    }
}