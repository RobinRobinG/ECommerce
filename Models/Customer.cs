using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ECommerce.Models
{
    public class Customer
    {
        [Key]
        public int CustomerId {get;set;}
        public int CustomerUserId {get;set;}
        public User CustomerUser {get;set;}
        public int SupplierUserId {get;set;}
        public User SupplierUser {get;set;}
        public DateTime CreatedAt {get;set;} = DateTime.Now;
        public DateTime UpdatedAt {get;set;} = DateTime.Now;
    }
}