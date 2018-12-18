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
  public class ProductController: Controller
    {
        private ProjectContext DbContext;
        public ProductController(ProjectContext context)
        {
            DbContext = context;
        }

        [HttpGet]
        [Route("products")]
        public IActionResult Products()
        {
            if (HttpContext.Session.GetInt32("UserID") == null)
            {
                return RedirectToAction("Index", "LoginReg");
            }
            User CurrentUser = DbContext.Users
                .SingleOrDefault(u => u.UserId == HttpContext.Session.GetInt32("UserID"));
            List<Product> AllProducts = DbContext.Products
                .Include(p => p.User).ToList();
            ProductView Products = new ProductView 
            {
                Product = new Product{},
                Products = AllProducts
            };
            return View(Products);
        }

        [HttpPost]
        [Route("addproduct")]
        public IActionResult AddProduct(Product newproduct)
        {
            if (HttpContext.Session.GetInt32("UserID") == null)
            {
                return RedirectToAction("Index", "LoginReg");
            }
            User CurrentUser = DbContext.Users
                .SingleOrDefault(u => u.UserId == HttpContext.Session.GetInt32("UserID"));
            if(ModelState.IsValid)
            {
                newproduct.CreatedById = CurrentUser.UserId;
                DbContext.Products.Add(newproduct);
                DbContext.SaveChanges();
                return RedirectToAction("Products", "Product");
            }
            List<Product> AllProducts = DbContext.Products
                .Include(p => p.User).ToList();
            ProductView Products = new ProductView 
            {
                Product = new Product{},
                Products = AllProducts
            };
            return View("Products", Products); 
        }
        [HttpGet]
        [Route("deleteproduct/{productid}")]
        public IActionResult DeleteOrder(int productid)
        {
            if (HttpContext.Session.GetInt32("UserID") == null)
            {
                return RedirectToAction("Index", "LoginReg");
            }
            User CurrentUser = DbContext.Users.SingleOrDefault(u => u.UserId == HttpContext.Session.GetInt32("UserID"));
            Product DeleteData = DbContext.Products.SingleOrDefault(p => p.ProductId == productid);
            DbContext.Products.Remove(DeleteData);
            DbContext.SaveChanges();
            return RedirectToAction("Products");
        }

    }
}