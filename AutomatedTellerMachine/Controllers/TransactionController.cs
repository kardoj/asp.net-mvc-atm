using AutomatedTellerMachine.Models;
using AutomatedTellerMachine.Services;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AutomatedTellerMachine.Controllers
{
    [Authorize]
    public class TransactionController : Controller
    {
        // Kasutame liidest selleks, et oleks võimalik
        // andmebaasiobjekti testides mock'ida
        private IApplicationDbContext db;

        public TransactionController() {
            db = new ApplicationDbContext();
        }

        public TransactionController(IApplicationDbContext dbContext) {
            db = new ApplicationDbContext();
        }

        // GET: Transaction/Deposit
        public ActionResult Deposit(int checkingAccountId)
        {
            return View();
        }

        // POST: Transaction/Deposit
        [HttpPost]
        public ActionResult Deposit(Transaction transaction)
        {
            // Kui sisestati negatiivne summa
            if (transaction.Amount < 0)
            {
                ModelState.AddModelError("Amount", "Sisestatud summa peab olema positiivne!");
            }

            if (ModelState.IsValid)
            {
                // Uus raha lisamine
                db.Transactions.Add(transaction);
                db.SaveChanges();
                var service = new CheckingAccountService(db);
                service.UpdateBalance(transaction.CheckingAccountId);
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        // GET: Transaction/Withdraw
        public ActionResult Withdraw(int checkingAccountId)
        {
            return View();
        }

        // POST: Transaction/Withdraw
        [HttpPost]
        public ActionResult Withdraw(Transaction transaction)
        {
            // Võtan konto, et kontrollida, kas on piisavalt vahendeid.
            var checkingAccount = db.CheckingAccounts.Find(transaction.CheckingAccountId);

            if (transaction.Amount > checkingAccount.Balance) {
                ModelState.AddModelError("Amount", "Kontol ei ole piisavalt vahendeid!");
            }

            if (ModelState.IsValid)
            {
                // Muudan summa negatiivseks, et lisamise asemel maha lahutada
                transaction.Amount = -transaction.Amount;

                // Uus raha lisamine
                db.Transactions.Add(transaction);
                db.SaveChanges();
                var service = new CheckingAccountService(db);
                service.UpdateBalance(transaction.CheckingAccountId);
                return RedirectToAction("Index", "Home");
            }
            return View();
        }
    }
}
