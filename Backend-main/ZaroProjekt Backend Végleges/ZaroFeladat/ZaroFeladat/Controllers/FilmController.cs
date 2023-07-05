using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ZaroFeladat.Models;
using System;
using System.Collections.Generic;
using ZaroFeladat.Interfaces;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace ZaroFeladat.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class FilmController : ControllerBase
    {
        [Route("basic")]
        [HttpGet]

        public IActionResult GetBasic()
        {

            using (var context = new zaroprojektContext())
            {
                try
                {
                    var response = context.Filmeks.ToList();
                    return Ok(response);
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }
        }

        [Route("ByCategory")]
        [HttpGet]

        public IActionResult GetByCategory(string Kategoria)
        {

            using (var context = new zaroprojektContext())
            {
                try
                {
                    return Ok(context.Filmeks.Where(f => f.Kategoria == Kategoria).ToList());
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }
        }

        [Route("all")]
        [HttpGet]

        public IActionResult GetAll()
        {
            List<Filmek> list = new List<Filmek>();
            using(var context=new zaroprojektContext())
            {
                try
                {
                    return StatusCode(200, context.Filmeks.ToList());
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }
        }

        [Route("stepbystep")]
        [HttpGet]

        public IActionResult GetStepByStep(int Id)
        {

            using (var context = new zaroprojektContext())
            {
                try
                {
                    return Ok(context.Filmeks.Where(f=>f.Id==Id).ToList());
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }
        }
                
        [HttpPost("{uId}")]

        public IActionResult Post(string uId, Filmek film)
        {
            //var result = uId == "hello" ? "igen" : "nem";
            //felhasznalo ??= new Felhasznalok();

            if (Program.LoggedInUsers.ContainsKey(uId) && Program.LoggedInUsers[uId].Jogosultsag == 9)
            {
                using (var context = new zaroprojektContext())
                {
                    try
                    {
                        context.Filmeks.Add(film);
                        context.SaveChanges();
                        return Ok("Az új film adatai rögzítésre kerültek.");
                    }
                    catch (Exception ex)
                    {
                        return BadRequest(ex.Message);
                    }
                }
            }
            else
            {
                return BadRequest("Nincs bejelentkezve/jogosultsága!");
            }
        }

        [HttpPut("{uId}")]

        public IActionResult Put(string uId, Filmek film)
        {
            if (Program.LoggedInUsers.ContainsKey(uId) && Program.LoggedInUsers[uId].Jogosultsag == 9)
            {
                using (var context = new zaroprojektContext())
                {
                    try
                    {
                        context.Filmeks.Update(film);
                        context.SaveChanges();
                        return Ok("A film adatai módosításra kerültek.");
                    }
                    catch (Exception ex)
                    {
                        return BadRequest(ex.Message);
                    }
                }
            }
            else
            {
                return BadRequest("Nincs bejelentkezve/jogosultsága!");
            }

        }


        [HttpDelete("{uId}")]

        public IActionResult Delete(string uId, int id)
        {
            if (Program.LoggedInUsers.ContainsKey(uId) && Program.LoggedInUsers[uId].Jogosultsag == 9)
            {
                using (var context = new zaroprojektContext())
                {
                    try
                    {
                        Filmek film = new Filmek();
                        film.Id = id;
                        context.Filmeks.Remove(film);
                        context.SaveChanges();
                        return Ok("A film törlésre került.");
                    }
                    catch (Exception ex)
                    {
                        return BadRequest(ex.Message);
                    }
                }
            }
            else
            {
                return BadRequest("Nincs bejelentkezve/jogosultsága!");
            }
        }
    }
}