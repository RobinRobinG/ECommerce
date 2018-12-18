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
  public class OrderController: Controller
    {
        private ProjectContext DbContext;
        public OrderController(ProjectContext context)
        {
            DbContext = context;
        }

        [HttpGet]
        [Route("orders")]
        public IActionResult Orders()
        {
            if(!IsUserLoggedIn())
            {
                return RedirectToAction("Index", "LoginReg");
            }
            User CurrentUser = setCurrentUser();
            List<Order> AllOrders = getAllOrdersBuCurrentUser(CurrentUser);
            List<Customer> AllCustomers = getAllCustomers(CurrentUser);
            List<Product> AllProducts = getAllProductsByCurrentUser(CurrentUser);
            OrderView orderviewdata = setOrderViewModel(AllOrders);
            ViewBag.Customers = AllCustomers;
            ViewBag.Products = AllProducts;
            return View(orderviewdata);
        }

        [HttpPost]
        [Route("addorder")]
        public IActionResult AddOrder(Order neworder)
        {

            if(!IsUserLoggedIn())
            {
                return RedirectToAction("Index", "LoginReg");
            }
            User CurrentUser = setCurrentUser();
            if(!ModelState.IsValid)
            {
                List<Customer> AllCustomers = getAllCustomers(CurrentUser);
                List<Product> AllProducts = getAllProductsByCurrentUser(CurrentUser);
                List<Order> AllOrders = getAllOrdersBuCurrentUser(CurrentUser);
                OrderView orderviewdata = setOrderViewModel(AllOrders);
                ViewBag.Customers = AllCustomers;
                ViewBag.Products = AllProducts;
                return View("Orders", orderviewdata); 
            }
            Product updateProduct = DbContext.Products.SingleOrDefault(p => p.ProductId == neworder.ProductId);
            if(updateProduct.Quantity < neworder.Quantity)
            {
                ModelState.AddModelError("Quantity", "Order quantity exceeds product inventory!");
            }
            neworder.CreatedById = CurrentUser.UserId;
            DbContext.Orders.Add(neworder);
            updateProduct.Quantity -= neworder.Quantity;
            DbContext.SaveChanges();
            return RedirectToAction("Orders", "Order");
        }

        [HttpGet]
        [Route("deleteorder/{orderid}/{productid}")]
        public IActionResult DeleteOrder(int orderid, int productid)
        {
            if(!IsUserLoggedIn())
            {
                return RedirectToAction("Index", "LoginReg");
            }
            User CurrentUser = setCurrentUser();
            Order DeleteData = DbContext.Orders.SingleOrDefault(o => o.OrderId == orderid);
            DbContext.Orders.Remove(DeleteData);
            Product updateProduct = DbContext.Products.SingleOrDefault(p => p.ProductId == productid);
            updateProduct.Quantity += DeleteData.Quantity;
            DbContext.SaveChanges();
            return RedirectToAction("Orders", "Order");
        }

        //General methods
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
        public List<Order> getAllOrdersBuCurrentUser(User CurrentUser)
        {
            List<Order> AllOrders = DbContext.Orders
                .Where(c => c.CreatedById == CurrentUser.UserId)
                .Include(u => u.CreatedBy)
                .Include(u => u.Product)
                .Include(u => u.Customer)
                .ThenInclude(u => u.CustomerUser).ToList();
            return AllOrders;
        }
        public List<Product> getAllProductsByCurrentUser(User CurrentUser)
        {
            List<Product> AllProducts = DbContext.Products
                .Where(p => p.CreatedById == CurrentUser.UserId && p.Quantity > 0)
                .Include(p => p.User)
                .ToList();
            return AllProducts;
        }
        public List<Customer> getAllCustomers(User CurrentUser)
        {
            List<Customer> AllCustomers = DbContext.Customers
                .Where(c => c.SupplierUserId == CurrentUser.UserId)
                .Include(u => u.CustomerUser)
                .Include(u => u.SupplierUser)
                .ToList();
            return AllCustomers;
        }
        public OrderView setOrderViewModel(List<Order> AllOrders)
        {
            OrderView orderview = new OrderView 
            {
                Order = new Order{},
                Orders = AllOrders
            };
            return orderview;
        }
    }
}