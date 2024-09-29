using Company.Database.Access;
using Company.Service.Helper;
using Company.Service.Interfaces;
using Company.Service.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MVC_ASP_Test.Models;

namespace MVC_ASP_Test.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ISMSService _smsService;
        public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, ISMSService smsService)
        {
            this._userManager = userManager;
            this._signInManager = signInManager;
            this._smsService = smsService;
        }

        public IActionResult SignUp()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SignUp(SignUpViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser
                {
                    UserName = model.Email.Split('@')[0],
                    Email = model.Email,
                    FirstName = model.FirstName,
                    PhoneNumber = model.PhoneNumber,
                    LastName = model.LastName,
                    isActive = model.isAgree
                };
                var result = await this._userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    await this._signInManager
                           .SignInAsync(user, model.isAgree);
                    var sms = new SMS
                    {
                        Body = $"Your Account Have Been Created Successfully + {DateTime.Now.ToString()}",
                        ToPhone = model.PhoneNumber
                    };
                    _smsService.SendSMS(sms);
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                }
            }
            return View();
        }
        public IActionResult SignIn()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SignIn(SignInViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await this._userManager.FindByEmailAsync(model.Email);
                if (user != null)
                {
                    var result = await this._userManager
                        .CheckPasswordAsync(user, model.Password);
                    if (result)
                    {
                        await this._signInManager
                            .SignInAsync(user, model.RememberMe);
                        var sms = new SMS
                        {
                            Body = $"You have been logged in successfully. + {DateTime.Now.ToString()}",
                            ToPhone = user.PhoneNumber
                        };
                        //_smsService.SendSMS(sms);
                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        ModelState.AddModelError("", "Email or Password is not correct.");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Email or Password is not correct.");
                }
            }
            return View();
        }
        public async Task<IActionResult> SignOut()
        {
            await this._signInManager.SignOutAsync();
            return RedirectToAction("SignIn", "Account");
        }

        public IActionResult ForgetPassword()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> ForgetPassword(ForgetPasswordViewModel forgetPasswordViewModel)
        {
            if (ModelState.IsValid)
            {
                var user = await this._userManager.FindByEmailAsync(forgetPasswordViewModel.Email);
                if (user != null)
                {
                    var token = await this._userManager
                        .GeneratePasswordResetTokenAsync(user);
                    var passwordResetLink = Url.Action("ResetPassword", "Account",
                        new { email = forgetPasswordViewModel.Email, token = token }, Request.Scheme);

                    var email = new Email
                    {
                        To = forgetPasswordViewModel.Email,
                        Subject = "Reset Password",
                        Body = passwordResetLink
                    };
                    // send email
                    EmailSettings.SendEmail(email);
                    return RedirectToAction("CheckYourInbox");
                }
                else
                {
                    ModelState.AddModelError("", "Email is not correct.");
                }
                return View();
            }
            return View();
        }
        public IActionResult CheckYourInbox()
        {
            return View();
        }
        public IActionResult ResetPassword(string Email, string Token)
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> ResetPassword(ResetPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(model.Email);
                if (user is not null)
                {
                    var result = await _userManager.ResetPasswordAsync(user, model.Token, model.Password);
                    if (result.Succeeded)
                    {
                        return RedirectToAction("SignIn", "Account");
                    }
                    else
                    {
                        foreach (var error in result.Errors)
                        {
                            ModelState.AddModelError("", error.Description);
                        }
                    }
                }
            }
            return View();
        }
    }
}
