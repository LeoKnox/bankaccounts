using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using bankAccount.Models;
using Microsoft.AspNetCore.Identity;

namespace bankAccount.Controllers
{
    public class HomeController : Controller
    {
        private bankAccountContext dbContext;

        public HomeController(bankAccountContext context)
        {
            dbContext = context;
        }

        [Route("")]
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [Route("CreateUser")]
        [HttpPost]
        public IActionResult RegisterUser(Users newUser)
        {
            if (ModelState.IsValid)
            {
                PasswordHasher<Users> Hasher = new PasswordHasher<Users>();
                newUser.Password = Hasher.HashPassword(newUser, newUser.Password);
                newUser.CreatedAt = DateTime.Now;
                newUser.UpdatedAt = DateTime.Now;
                dbContext.Add(newUser);
                dbContext.SaveChanges();
                return RedirectToAction("Account");
            }
            else
            {
                return View("Index");
            }
        }

        [Route("LogIn")]
        [HttpGet]
        public IActionResult LogIn()
        {
            return View("login");
        }

        [Route("LoggedIn")]
        [HttpPost]
        public IActionResult LoggedIn(Login newLogin)
        {
            if (ModelState.IsValid)
            {
                Users oneUser = dbContext.MyUsers.Where(log => log.Email == newLogin.Email).FirstOrDefault();
                var hasher = new PasswordHasher<Login>();
                var result = hasher.VerifyHashedPassword(newLogin, oneUser.Password, newLogin.Password);
                if (result == 0)
                {
                    ModelState.AddModelError("Password", "Password does not match");
                    return View("login");
                }
                HttpContext.Session.SetInt32("ID", oneUser.UsersId);
                return RedirectToAction("Account");
            }
            return View("login");
        }

        [Route("account")]
        [HttpGet]
        public IActionResult Account()
        {
            int? intVar = HttpContext.Session.GetInt32("ID");
            Users FN = dbContext.MyUsers.Where(u => u.UsersId == intVar).FirstOrDefault<Users>();
            ViewBag.id = FN.FirstName;
            ViewBag.id2 = FN.LastName;
            List<Transactions> acc = dbContext.MyTransactions.Where(ua => ua.UsersId == intVar).ToList();
            Decimal start = acc.Sum(s => s.Decimal);
            Decimal balance = Convert.ToDecimal(string.Format("{0:F2}", start));
            int balint = (int)balance*100;
            HttpContext.Session.SetInt32 ("bal", balint);
            ViewBag.balance = balance;
            return View("account", acc);
        }

        [Route("money")]
        [HttpPost]
        public IActionResult money(Decimal depowith)
        {   
            int? balint = HttpContext.Session.GetInt32("bal");
            if (depowith*-1 <= balint/100) {
                Transactions newTransaction = new Transactions();
                newTransaction.Decimal = depowith;
                newTransaction.CreatedAt = DateTime.Now;
                newTransaction.UsersId = 1;
                dbContext.Add(newTransaction);
                dbContext.SaveChanges();
                return RedirectToAction("account");
            }
            return RedirectToAction("account");
        }
    }
}
