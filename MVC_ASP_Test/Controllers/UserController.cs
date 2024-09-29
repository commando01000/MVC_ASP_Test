using Company.Database.Access;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MVC_ASP_Test.Controllers
{
    public class UserController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public UserController(UserManager<ApplicationUser> userManager)
        {
            this._userManager = userManager;
        }

        // GET: UserController
        public async Task<IActionResult> Index(string searchInp)
        {
            List<ApplicationUser> users;
            if (string.IsNullOrEmpty(searchInp))
            {
                users = await _userManager.Users.ToListAsync();
            }
            else
            {
                users = await _userManager.Users.Where(user => user.FirstName.ToLower().Contains(searchInp.Trim().ToLower())
                || user.LastName.ToLower().Contains(searchInp.Trim().ToLower())).ToListAsync();
            }
            return View(users);
        }

        // GET: UserController/Details/5
        public async Task<IActionResult> Details(string id)
        {
            var users = await this._userManager.FindByIdAsync(id.ToString());
            return View();
        }

        // GET: UserController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: UserController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: UserController/Edit/5
        public async Task<ActionResult> Edit(string id)
        {
            var user = await this._userManager.FindByIdAsync(id.ToString());
            return View(user);
        }

        // POST: UserController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, IFormCollection collection, ApplicationUser user)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var firstName = collection["FirstName"];
                    var lastName = collection["LastName"];
                    var Email = collection["Email"];
                    var userName = collection["Email"].ToString().Split('@')[0]; // UserName
                    var normalizedEmail = collection["Email"].ToString().ToUpper();
                    var passwordHash = this._userManager.PasswordHasher.HashPassword(user, user.PasswordHash.ToString());
                    var normailizedUserName = collection["Email"].ToString().ToUpper().Split('@')[0];
                    await this._userManager.UpdateAsync(new ApplicationUser
                    {
                        Id = id.ToString(),
                        FirstName = firstName,
                        LastName = lastName,
                        Email = Email,
                        UserName = userName,
                        NormalizedEmail = normalizedEmail,
                        NormalizedUserName = normailizedUserName
                    });
                    return RedirectToAction(nameof(Index));
                }

            }
            catch
            {
                return View();
            }
            return View();
        }

        // POST: UserController/Delete/id
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(string id, IFormCollection collection)
        {
            try
            {
                var user = await this._userManager.FindByIdAsync(id.ToString());
                await this._userManager.DeleteAsync(user);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
