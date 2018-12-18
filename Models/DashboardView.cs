using System.Collections.Generic;
using ECommerce.Models;

namespace ECommerce.Models
{
  public class DashboardView
    {
        public List<Product> Products {get;set;}
        public List<Product> Products2 {get;set;}
        public List<Order> Orders {get;set;}
        public List<Customer> Customers {get;set;}
    }
}