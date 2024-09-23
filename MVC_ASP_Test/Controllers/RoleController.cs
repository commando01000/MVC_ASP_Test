using Company.Database.Access;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MVC_ASP_Test.Models;

namespace MVC_ASP_Test.Controllers
{
    // authorize for admin and Super Admin
    [Authorize(Roles = "Admin, Super Admin")]
    public class RoleController : Controller
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<ApplicationUser> _userManager;
        public RoleController(RoleManager<IdentityRole> roleManager, UserManager<ApplicationUser> userManager)
        {
            this._roleManager = roleManager;
            this._userManager = userManager;
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
                if ((await _roleManager.CreateAsync(role)).Succeeded)
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
                await this._roleManager.UpdateAsync(role);
                return RedirectToAction("Index");
            }
            return View(Role);
        }
        public async Task<IActionResult> AddOrRemoveUser(string id)
        {
            var role = await this._roleManager.FindByIdAsync(id);

            var users = await this._userManager.Users.ToListAsync();

            var usersInRole = new List<UserRoleViewModel>();

            foreach (var user in users)
            {
                var userRole = new UserRoleViewModel
                {
                    UserId = user.Id,
                    UserName = user.UserName
                };
                if (await this._userManager.IsInRoleAsync(user, role.Name))
                {
                    userRole.IsSelected = true;
                }
                else
                {
                    userRole.IsSelected = false;
                }
                usersInRole.Add(userRole);
            }
            ViewBag.RoleId = role.Id;
            return View(usersInRole);
        }
        [HttpPost]
        public async Task<IActionResult> AddOrRemoveUser(string id, IEnumerable<UserRoleViewModel> users)
        {
            var role = await _roleManager.FindByIdAsync(id);

            if (ModelState.IsValid)
            {
                foreach (var user in users)
                {
                    var appUser = await _userManager.FindByIdAsync(user.UserId);
                    if (appUser != null)
                    {
                        if (user.IsSelected && !(await _userManager.IsInRoleAsync(appUser, id)))
                        {
                            await _userManager.AddToRoleAsync(appUser, role.Name);
                        }
                        else
                        {
                            await _userManager.RemoveFromRoleAsync(appUser, role.Name);
                        }
                    }
                }
            }
            return RedirectToAction(nameof(Edit), new { id = role.Id });
        }
    }
}
