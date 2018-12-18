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
  public class DashboardController: Controller
    {
        private ProjectContext DbContext;
        public DashboardController(ProjectContext context)
        {
            DbContext = context;
        }

        [HttpGet]
        [Route("dashboard")]
        public IActionResult Dashboard()
        {
            if (HttpContext.Session.GetInt32("UserID") == null)
            {
                return RedirectToAction("Index", "LoginReg");
            }
            User CurrentUser = DbContext.Users
                .SingleOrDefault(u => u.UserId == HttpContext.Session.GetInt32("UserID"));
            List<Product> AllProducts = DbContext.Products.ToList();
            DashboardView Products = new DashboardView 
            {
                Products = AllProducts
            };
            return View(Products);
        }

        [HttpGet]
        [Route("search/{input}")]
        public IActionResult searchProducts(string input)
        {
            // if (HttpContext.Session.GetInt32("UserID") == null)
            // {
            //     return RedirectToAction("Index", "LoginReg");
            // }
            // User CurrentUser = DbContext.Users
            //     .SingleOrDefault(u => u.UserId == HttpContext.Session.GetInt32("UserID"));

            List<Product> FilteredProducts = DbContext.Products
                .Where(p => p.Name.Contains(input) || p.Description.Contains(input))
                .Take(6).ToList();
            DashboardView Products = new DashboardView 
            {
                Products2 = FilteredProducts
            };
            return View("Dashboard", Products);
        }

        [HttpPost]
        public string Index(string searchString, bool notUsed)
        {
            return "From [HttpPost]Index: filter on " + searchString;
        }


    }
}