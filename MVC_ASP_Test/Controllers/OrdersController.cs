﻿using Company.Database.Access.Contexts;
using Company.Database.Access.Entities;
using Company.Service.Interfaces;
using Company.Service.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static NuGet.Packaging.PackagingConstants;

namespace MVC_ASP_Test.Controllers
{
    public class OrdersController : Controller
    {
        // GET: OrdersController
        private readonly IOrderService _orderService;
        private readonly NorthwindContextProcedures _contextProcedures;

        public OrdersController(IOrderService orderService, NorthwindContextProcedures contextProcedures)
        {
            _orderService = orderService;
            _contextProcedures = contextProcedures;
        }
        public ActionResult Index()
        {
            var orders = _orderService.GetAll();
            return View(orders);
        }

        // GET: OrdersController/Details/5
        public ActionResult Cust_Orders(string id)
        {
            if(id == null)
            {
                return NotFound();
            }
            else
            {
                var orders = this._contextProcedures.CustOrdersOrdersAsync(id).Result;
                return View(orders);
            }
        }

        public ActionResult Order_Details(int id)
        {
            var order_details = this._contextProcedures.CustOrdersDetailAsync(id).Result;
            return View(order_details);
        }

        // GET: OrdersController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: OrdersController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Order order)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    this._orderService.Add(order);
                    return RedirectToAction("Index");
                }
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: OrdersController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: OrdersController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Order order)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    this._orderService.Update(order);
                    return RedirectToAction("Index");
                }
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: OrdersController/Delete/5
        public ActionResult Delete(int id)
        {
            this._orderService.Delete(id);
            return RedirectToAction("Index");
        }
    }
}
