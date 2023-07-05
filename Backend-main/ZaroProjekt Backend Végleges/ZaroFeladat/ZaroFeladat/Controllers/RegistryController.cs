using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ZaroFeladat.Models;
using System;
using System.Linq;
using System.Net.Mail;

namespace ZaroFeladat.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class RegistryController : ControllerBase
    {

        [HttpPost]

        public IActionResult Post(Registry registry)
        {
            using (var context = new zaroprojektContext())
            {
                try
                {
                    if (context.Felhasznaloks.Where(f => f.FelhasznaloNev == registry.FelhasznaloNev).ToList().Count != 0)
                    {
                        return StatusCode(210, "Létezik ilyen felhasználónév!");
                    }
                    if (context.Felhasznaloks.Where(f => f.Email == registry.Email).ToList().Count != 0)
                    {
                        return StatusCode(211, "Erről az e-mail címről már regisztráltak!");
                    }
                    if (registry.FelhasznaloNev != "" && registry.Email != "")
                    {
                        registry.Key = Program.GenerateSalt();
                        context.Add(registry);
                        context.SaveChanges();
                        Program.SendEmail(registry.Email, "Regisztráció", "A regisztrációhoz kattints a következő linkre: " + "https://localhost:5001/Registry/" + registry.Key);
                        return StatusCode(200, "Sikeres regisztráció. Az e-mail címére küldött utasítások alapján befejezheti azt.");
                    }
                    else
                    {
                        return StatusCode(200, "Ellenőrzés lefuttatva.");
                    }
                }
                catch (Exception ex)
                {
                    return StatusCode(200, ex.Message);
                }
            }
        }

        [HttpGet("{Key}")]

        public IActionResult Get(string Key)
        {
            using (var context = new zaroprojektContext())
            {
                try
                {
                    var registryUser = context.Registries.Where(c => c.Key == Key).ToList();
                    if (registryUser.Count != 0)
                    {
                        Felhasznalok felhasznalo = new Felhasznalok();
                        felhasznalo.FelhasznaloNev = registryUser[0].FelhasznaloNev;
                        felhasznalo.TeljesNev = registryUser[0].TeljesNev;
                        felhasznalo.Salt = registryUser[0].Salt;
                        felhasznalo.Hash = registryUser[0].Hash;
                        felhasznalo.Email = registryUser[0].Email;
                        felhasznalo.Jogosultsag = 0;
                        felhasznalo.Aktiv = 1;
                        context.Felhasznaloks.Add(felhasznalo);
                        context.Registries.Remove(registryUser[0]);
                        context.SaveChanges();
                        return Ok("Regisztráció befejezve.");
                    }
                    else
                    {
                        return BadRequest("A regisztráció már megtörtént vagy hibás kulcs került megadásra!");
                    }
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }
        }
    }
}
