using System.Security.Claims;
using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyAttemptNum5.ViewModels;
using MyAttemptNum5.Models;

namespace MyAttemptNum5.Controllers;

public class AccountController : Controller
    {
        private AvtosalonContext _db;
        public AccountController(AvtosalonContext context)
        {
            _db = context;
        }
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                Buyer buyer = await _db.Buyers.FirstOrDefaultAsync(b => b.Email == model.Email && b.Password == model.Password);
                if (buyer != null)
                {
                    await Authenticate(model.Email); // аутентификация
 
                    return RedirectToAction("Index", "Home");
                }
                ModelState.AddModelError("", "Некорректные логин и(или) пароль");
            }
            return View(model);
        }
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterModel model)
        {
            if (string.IsNullOrEmpty(model.Email)) {
                ModelState.AddModelError(nameof(model.Email), "Введите логин");
            } else
            if (model.Email.Length > 50) {
                ModelState.AddModelError(nameof(model.Email),
                    "Значение не должно превышать 50 символов");
            }
            if (string.IsNullOrEmpty(model.Email)) {
                ModelState.AddModelError(nameof(model.Email), "Введите email");
            } else
            if (!Regex.IsMatch(model.Email, @"^(?![_.-])((?![_.-][_.-])[a-zA-Z\d_.-]){0,63}[a-zA-Z\d]@((?!-)((?!--)[a-zA-Z\d-]){0,63}[a-zA-Z\d]\.){1,2}([a-zA-Z]{2,14}\.)?[a-zA-Z]{2,14}$")) {
                ModelState.AddModelError(nameof(model.Email),"Неверный формат адреса");
            }
            if (string.IsNullOrEmpty(model.Password)) {
                ModelState.AddModelError(nameof(model.Password),
                    "Введите пароль");
            }
            if (string.IsNullOrEmpty(model.ConfirmPassword)) {
                ModelState.AddModelError(nameof(model.ConfirmPassword),
                    "Подтвердите пароль");
            }
            if (model.Password != model.ConfirmPassword) {
                ModelState.AddModelError(nameof(model.ConfirmPassword),
                    "Не совпадают пароли");
            }
           
            if (ModelState.IsValid)
            {
                Buyer buyer = await _db.Buyers.FirstOrDefaultAsync(b => b.Email == model.Email);
                if (buyer == null)
                {
                    // добавляем пользователя в бд
                    _db.Buyers.Add(new Buyer { Fio = model.Fio, PhoneNumber = model.PhoneNumber, Email = model.Email, Password = model.Password });
                    await _db.SaveChangesAsync();
 
                    await Authenticate(model.Email); // аутентификация
 
                    return RedirectToAction("Index", "Home");
                }
                else
                    ModelState.AddModelError("", "Некорректные логин и(или) пароль");
            }
            return View(model);
        }
 
        private async Task Authenticate(string userName)
        {
            // создаем один claim
            var claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, userName)
            };
            // создаем объект ClaimsIdentity
            ClaimsIdentity id = new ClaimsIdentity(claims, "ApplicationCookie", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);
            // установка аутентификационных куки
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(id));
        }
 
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login", "Account");
        }
        
        
    }