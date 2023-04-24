using Microsoft.AspNetCore.Mvc;
using MaturitaPVACSharp.Models;
using MaturitaPVACSharp.Data;
using System.Linq;

namespace MaturitaPVACSharp.Controllers
{
    public class UzivatelController : Controller
    {
        private readonly MaturitaPVACSharpContext _context;

        public UzivatelController(MaturitaPVACSharpContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Prihlasit()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Registrovat()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Registrovat(string jmeno, string heslo, string heslo_kontrola)
        {
            if (jmeno == null || jmeno.Trim().Length == 0)
                return RedirectToAction("Registrovat");
            if (heslo == null || heslo.Trim().Length == 0)
                return RedirectToAction("Registrovat");
            if (heslo != heslo_kontrola)
                return RedirectToAction("Registrovat");

            if (_context.Uzivatele.Where(u => u.Jmeno == jmeno).FirstOrDefault() != null)
                return RedirectToAction("Registrovat");

            string hash = BCrypt.Net.BCrypt.HashPassword(heslo);

            Uzivatel novyUzivatel = new Uzivatel { Jmeno = jmeno, Heslo = hash };

            _context.Uzivatele.Add(novyUzivatel);
            _context.SaveChanges();

            return RedirectToAction("Prihlasit");
        }

        [HttpPost]
        public IActionResult Prihlasit(string jmeno, string heslo)
        {
            Uzivatel hledany = _context.Uzivatele.Where(u => u.Jmeno == jmeno).FirstOrDefault();

            if (jmeno == null || heslo == null)
                return RedirectToAction("Prihlasit");
            if (hledany == null)
                return RedirectToAction("Prihlasit");
            if (!BCrypt.Net.BCrypt.Verify(heslo, hledany.Heslo))
                return RedirectToAction("Prihlasit");

            HttpContext.Session.SetString("Uzivatel", hledany.Jmeno);

            return RedirectToAction("Profil");
        }

        public IActionResult Profil()
        {
            if (HttpContext.Session.GetString("Uzivatel") == null)
                return RedirectToAction("Prihlasit");

            Uzivatel prihlaseny = _context.Uzivatele.Where(u => u.Jmeno == HttpContext.Session.GetString("Uzivatel")).FirstOrDefault();

            if (prihlaseny == null)
                return RedirectToAction("Odhlasit");

            return View(prihlaseny);
        }

        public IActionResult Odhlasit()
        {
            HttpContext.Session.Clear();

            return RedirectToAction("Index", "Home");
        }
    }
}
