using Microsoft.AspNetCore.Mvc;
using MaturitaPVACSharp.Data;
using MaturitaPVACSharp.Models;

namespace CSharpMaturita.Controllers
{
    public class ClanekController : Controller
    {
        private readonly MaturitaPVACSharpContext _context;

        public ClanekController(MaturitaPVACSharpContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Pridat()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Pridat(string nadpis, string telo)
        {
            if (nadpis == null || nadpis.Trim().Length == 0)
                return RedirectToAction("Pridat");
            if (telo == null || telo.Trim().Length == 0)
                return RedirectToAction("Pridat");

            Uzivatel prihlaseny = _context.Uzivatele
                .Where(u => u.Jmeno == HttpContext.Session.GetString("Uzivatel"))
            .First();

            Clanek novy = new Clanek { Nadpis = nadpis, Datum = DateTime.Now.ToString("dd.MM.yyyy"), Telo = telo, Autor = prihlaseny };

            _context.Clanky.Add(novy);
            _context.SaveChanges();

            return Redirect("/Clanek/Detail/" + novy.Id);
        }

        public IActionResult Detail(int id)
        {
            Clanek nacteny = _context.Clanky
                .Where(c => c.Id == id)
                .First();

            return View(nacteny);
        }

        public IActionResult Vsechny()
        {
            Uzivatel prihlaseny = _context.Uzivatele
                .Where(u => u.Jmeno == HttpContext.Session.GetString("Uzivatel"))
            .First();

            List<Clanek> clanky = prihlaseny.Clanky;

            return View(clanky);
        }
    }
}
