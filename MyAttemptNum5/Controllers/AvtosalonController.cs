using Microsoft.AspNetCore.Mvc;
using MyAttemptNum5.Models;
using MyAttemptNum5.Controllers;
using System.Diagnostics;
using System.Text.RegularExpressions;

namespace MyAttemptNum5.Controllers
{
    public class AvtosalonController : Controller
    {
        private readonly AvtosalonContext _db;
        public AvtosalonController(AvtosalonContext db) => _db = db;

        public IActionResult AvtosalonList()
        {
            IEnumerable<Automobile> AutomobileList = _db.Automobiles.ToList();
            //List<Automobile> ClearAutomobileList = new List<Automobile>();
            //foreach (var x in AutomobileList)
            //{
            //    string temp = x.Model;
            //    bool exist = false;
            //    foreach (var y in ClearAutomobileList)
            //    {
            //        if (y.Model == x.Model)
            //            exist = true;
            //    }
            //    if (exist == false)
            //        ClearAutomobileList.Add(x);
            //}
            return View(AutomobileList);
        }

        public IActionResult ItemDetails(int id)
        {
            IEnumerable<Automobile> AutomobileList = _db.Automobiles.ToList();

            List<Automobile> ItemsList = new List<Automobile>();
            Automobile? avt = AutomobileList.FirstOrDefault(a => a.IdA == id);
            return View(avt);
        }
        public IActionResult Creator()
        {
            return Redirect("https://vk.com/id364791065");
        }

        [HttpPost]
        public IActionResult Register(RegistrationModel model) {
            if (string.IsNullOrEmpty(model.Login)) {
                ModelState.AddModelError(nameof(model.Login), "Введите логин");
            } else
                if (model.Login.Length > 50) {
                ModelState.AddModelError(nameof(model.Login),
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
            if (string.IsNullOrEmpty(model.Password2)) {
                ModelState.AddModelError(nameof(model.Password2),
              "Подтвердите пароль");
            }
            if (model.Password != model.Password2) {
                ModelState.AddModelError(nameof(model.Password2),
        "Не совпадают пароли");
            }
            if (model.Accept == false) {
                ModelState.AddModelError(nameof(model.Accept), "Необходимо согласиться с условиями");
            }
            if (ModelState.IsValid) {
                Debug.WriteLine(model.Login);
                Debug.WriteLine(model.Password);
                Debug.WriteLine(model.Password2);
                Debug.WriteLine(model.Email);
                Debug.WriteLine(model.Accept);
                return View("SuccessfulRegistration");
            } else {
                return View(model);
            }
        }
        
        [MyException]
        public IActionResult Register()
        {
            return View();
        }

        public IActionResult Agreement()
        {
            FileStream fs = System.IO.File.OpenRead("Data/TermsOfUse.pdf");
            return File(fs, "application/pdf");
        }
        public IActionResult FeedBack()
        {
            return View();

        }
    }
}
