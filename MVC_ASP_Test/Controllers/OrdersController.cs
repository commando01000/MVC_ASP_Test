using Company.Database.Access.Contexts;
using Company.Database.Access.Entities;
using Company.Repository;
using Company.Service.Interfaces;
using Company.Service.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
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
            // Get all employees and customers
            var employees = this._contextProcedures.SelectALLEmployeesAsync().Result.ToList();
            var customers = this._orderService.getAllCustomers().ToList();
            var shippers = this._orderService.GetShippers().ToList();
            // Create SelectLists for dropdowns
            ViewBag.Employees = new SelectList(
                   employees.Select(e => new
                   {
                       e.EmployeeID,
                       FullName = e.FirstName + " " + e.LastName
                   }),
                   "EmployeeID",
                   "FullName"
               );
            ViewBag.Shippers = new SelectList(shippers, "ShipperId", "CompanyName");
            ViewBag.Customers = new SelectList(customers, "CustomerId", "CompanyName");
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
            var order = this._orderService.GetById(id);
            return View(order);
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
                    this._orderService.Update(order, id);
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
