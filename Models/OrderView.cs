using System.Collections.Generic;
using ECommerce.Models;

namespace ECommerce.Models
{
  public class OrderView
    {
        public Order Order {get;set;}
        public List<Order> Orders {get;set;}
    }
}