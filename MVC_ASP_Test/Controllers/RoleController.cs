using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MVC_ASP_Test.Controllers
{
    public class RoleController : Controller
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        public RoleController(RoleManager<IdentityRole> roleManager)
        {
         this._roleManager = roleManager;
        }
        public async Task<IActionResult> Index()
        {
            var roles = await _roleManager.Roles.ToListAsync();
            return View(roles);

        }

        public IActionResult Create()
        { 
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(IdentityRole role)
        {
            if (ModelState.IsValid)
            {
                if((await _roleManager.CreateAsync(role)).Succeeded)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    foreach (var error in (await _roleManager.CreateAsync(role)).Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                    return View(role);
                }
            }
            else
            {
                foreach (var error in (await _roleManager.CreateAsync(role)).Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
                return View(role);
            }
        }
        [HttpGet]
        public async Task<IActionResult> Edit(string id)
        {
            var role = await _roleManager.FindByIdAsync(id);

            if (role == null)
            {
                ViewBag.ErrorMessage = $"Role with Id = {id} cannot be found";
                return View("NotFound");
            }
            else
            {
                return View(role);
            }
        }
        [HttpPost]
        public async Task<IActionResult> Edit(string id, IdentityRole Role)
        {
            var role = await _roleManager.FindByIdAsync(id);

            if (role == null)
            {
                ViewBag.ErrorMessage = $"Role with Id = {id} cannot be found";
            }
            else
            {
                role.Name = Role.Name;
                role.NormalizedName = Role.Name.ToUpper();
                this._roleManager.UpdateAsync(role);
                return RedirectToAction("Index");
            }
            return View(Role);
        }
    }
}
