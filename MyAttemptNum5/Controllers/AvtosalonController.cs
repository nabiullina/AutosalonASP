using Microsoft.AspNetCore.Mvc;
using MyAttemptNum5.Models;
using MyAttemptNum5.Controllers;
using System.Diagnostics;
using System.Runtime.InteropServices.JavaScript;
using System.Text.RegularExpressions;
using Microsoft.EntityFrameworkCore;

namespace MyAttemptNum5.Controllers
{
    public class AvtosalonController : Controller
    {
        private readonly AvtosalonContext _db;

        public AvtosalonController(AvtosalonContext context)
        {
            _db = context;
        }

        [Route("Avtosalon/BuyerInfo/{email}")]
        public async Task<IActionResult> BuyerInfo(string email)
        {
            var buyer = await _db.Buyers.Include("Dogovors.Ekzemplyar.IdANavigation").Include("Dogovors.Ekzemplyar.KomplektaciyaEkzemplyars.Komplektaciya").Where(b=>b.Email==email).FirstOrDefaultAsync();
            return View(buyer);
        }
            
        public IActionResult AvtosalonList()
        {
            IEnumerable<Automobile> AutomobileList = _db.Automobiles.ToList();
            
            return View(AutomobileList);
        }

        public IActionResult ItemDetails(long id)
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
        
        public IActionResult FeedBack()
        {
            return View();

        }

        public async Task<IActionResult> SelectEkzemplyar(long IdA)
        {
            var ekzemplyars = await _db.Ekzemplyars.Include("KomplektaciyaEkzemplyars.Komplektaciya").Include("IdANavigation").Where(e=> !e.IdD.HasValue).Where(e => e.IdA == IdA).ToListAsync();
            
            var model = new List<MultiModel>();
            foreach (var item in ekzemplyars)
            {
                model.Add(new MultiModel {Ekzemplyar = item});
            }
            
            return View(model);
        }
        
        // GET
        public async Task<IActionResult> Purchase(string VinKod)
        {
            var buyer = await _db.Buyers.Where(b => b.Email == User.Identity.Name).FirstOrDefaultAsync();
            var ekzemplyar = await _db.Ekzemplyars.Include("IdANavigation").Include("KomplektaciyaEkzemplyars.Komplektaciya").Where(e=>e.VinKod==VinKod).FirstOrDefaultAsync();
            var model = new MultiModel
            {
                Buyer = buyer,
                Ekzemplyar = ekzemplyar
            };
            return View(model);
        }
        
        // POST
        [HttpPost, ActionName("Purchase")]
        public async Task<IActionResult> PurchaseConfirmed(string VinKod) 
        {
            var buyer = await _db.Buyers.Include("Dogovors.Ekzemplyar.IdANavigation").Include("Dogovors.Ekzemplyar.KomplektaciyaEkzemplyars.Komplektaciya").Where(b => b.Email == User.Identity.Name).FirstOrDefaultAsync();
            var dogovor = new Dogovor()
            {
                DateOfExecution = DateTime.Now,
                IdB = buyer.IdB,
                VinKod = VinKod
            };
            _db.Add(dogovor);
            _db.SaveChanges();
            var ekzemplyar = await _db.Ekzemplyars.FindAsync(VinKod);
            var dogovorFromDb = await _db.Dogovors.Where(d => d.VinKod == VinKod).FirstOrDefaultAsync();
            ekzemplyar.IdD = dogovorFromDb.IdD;
            _db.Update(ekzemplyar);
            _db.SaveChanges();
            return Redirect($"BuyerInfo/{User.Identity.Name}");
        }
    }
}
