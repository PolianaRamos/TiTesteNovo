﻿using SportsStore.Domain.Abstract;
using SportsStore.Domain.Entities;
using SportsStore.WebUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SportsStore.WebUI.Controllers
{
    public class CartController : Controller
    {
        // GET: Cart
        private IProductRepository repository;

        public CartController(IProductRepository repo)
        {
            repository = repo;
        }

        public ViewResult Index(Cart cart, string returnUrl)
        {
            return View(new CartIndexViewModel
            {

                ReturnUrl = returnUrl,
                //Cart = GetCart(),
                Cart = cart
            });
        }

        public RedirectToRouteResult AddToCart(Cart cart, int Id, string returnUrl)
        {
            Product product = repository.Products
                .FirstOrDefault(p => p.Id == Id);
            if(product != null)
            {
                //GetCart().AddItem(product,1);
                cart.AddItem(product,1);
            }
            return RedirectToAction("Index",new { returnUrl});
        }

        public RedirectToRouteResult RemoveFromCart(Cart cart,int Id, string returnUrl)
        {
            Product product = repository.Products
                .FirstOrDefault(p => p.Id == Id);

            if(product != null)
            {
                //GetCart().RemoveLine(product);
                cart.RemoveLine(product);
            }

            return RedirectToAction("Index",new { returnUrl });
        }

        private Cart GetCart()
        {
            Cart cart = (Cart)Session["Cart"];
            if(cart == null)
            {
                cart = new Cart();
                Session["Cart"] = cart;
            }
            return cart;
        }



    }
}