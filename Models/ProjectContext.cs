using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
 
namespace ECommerce.Models
{
    public class ProjectContext : DbContext
    {
        public ProjectContext(DbContextOptions<ProjectContext> options): base(options) { }
        public DbSet<User> Users { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Customer> Customers { get; set; }

    // public bool IsUserLoggedIn(HttpContext context)
    //     {
    //         if (context.Session.GetInt32("UserID") == null)
    //         {
    //             return false;
    //         }
    //         return true;
    //     }
    
        // public List<Order> getAllOrdersBuCurrentUser(User CurrentUser)
        // {
        //     List<Order> AllOrders = DbContext.Orders
        //         .Where(c => c.CreatedById == CurrentUser.UserId)
        //         .Include(u => u.CreatedBy)
        //         .Include(u => u.Product)
        //         .Include(u => u.Customer)
        //         .ThenInclude(u => u.CustomerUser).ToList();
        //     return AllOrders;
        // }

    }
}   