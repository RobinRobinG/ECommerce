using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using ECommerce.Models;
using System.Linq;

namespace ECommerce.Controllers
{
  public class CustomerController: Controller
    {
        private ProjectContext DbContext;
        public CustomerController(ProjectContext context)
        {
            DbContext = context;
        }

        [HttpGet]
        [Route("customers")]
        public IActionResult Customers()
        {
            if(!IsUserLoggedIn())
            {
                return RedirectToAction("Index", "LoginReg");
            }
            User CurrentUser = setCurrentUser();
            int userid = CurrentUser.UserId;
            List<Customer> AllCustomers = DbContext.Customers
                .Include(u => u.CustomerUser)
                .Include(u => u.SupplierUser)
                .Where(c => c.SupplierUserId == CurrentUser.UserId).ToList();
            List<User> NotCurrentCustomers2 = DbContext.Users
                .Where(u => u.UserId != CurrentUser.UserId)
                .Where(u => !AllCustomers.Any(c => c.CustomerUserId == u.UserId))
                .ToList();
            ViewBag.NotCurrentCustomers = NotCurrentCustomers2;
            return View(AllCustomers);
        }

        [HttpPost]
        [Route("addcustomer")]
        public IActionResult AddCustomer(int userid)
        {
            if(!IsUserLoggedIn())
            {
                return RedirectToAction("Index", "LoginReg");
            }
            User CurrentUser = setCurrentUser();
            Customer NewData = new Customer();
            NewData.CustomerUserId = userid;
            NewData.SupplierUserId = CurrentUser.UserId;
            DbContext.Customers.Add(NewData);
            DbContext.SaveChanges();
            return RedirectToAction("Customers");
        }

        [HttpGet]
        [Route("deletecustomer/{customerid}")]
        public IActionResult DeleteCustomer(int customerid)
        {
            if(!IsUserLoggedIn())
            {
                return RedirectToAction("Index", "LoginReg");
            }
            User CurrentUser = setCurrentUser();
            Customer DeleteData = DbContext.Customers.SingleOrDefault(c => c.CustomerId == customerid);
            DbContext.Customers.Remove(DeleteData);
            DbContext.SaveChanges();
            return RedirectToAction("Customers");
        }

        public bool IsUserLoggedIn()
        {
            if (HttpContext.Session.GetInt32("UserID") == null)
            {
                return false;
            }
            return true;
        }
        public User setCurrentUser()
        {
            User CurrentUser = DbContext.Users
                .SingleOrDefault(u => u.UserId == HttpContext.Session.GetInt32("UserID"));
            return CurrentUser;
        }


    }
}