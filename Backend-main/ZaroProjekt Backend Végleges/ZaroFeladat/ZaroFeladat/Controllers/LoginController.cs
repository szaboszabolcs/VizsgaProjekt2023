﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using ZaroFeladat.Models;
using System.Linq;
using System;
using System.Web;

namespace ZaroFeladat.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        [HttpPost("SaltRequest/{nev}")]

        public IActionResult SaltRequest(string nev)
        {

            using (var context = new zaroprojektContext())
            {
                try
                {
                    List<Felhasznalok> talalat = new List<Felhasznalok>(context.Felhasznaloks.Where(f => f.FelhasznaloNev == nev));
                    if (talalat.Count > 0)
                    {
                        return Ok(talalat[0].Salt);

                    }
                    else
                    {
                        return BadRequest("Hibás felhasználónév!");
                    }
                }
                catch (Exception ex)
                {
                    return StatusCode(500, ex.Message);
                }
            }
        }

        [HttpPost]

        public IActionResult Login(string nev, string tmpHash)
        {
            using (var context = new zaroprojektContext())
            {
                try
                {
                    List<Felhasznalok> talalat = new List<Felhasznalok>(context.Felhasznaloks.Where(f => f.FelhasznaloNev == nev));
                    if (talalat.Count > 0 && talalat[0].Aktiv == 1)
                    {

                        bool talalt = false;
                        int index = 0;
                        int elemSzam = Program.LoggedInUsers.Count;
                        while (!talalt && index < elemSzam)
                        {
                            if (Program.LoggedInUsers.ElementAt(index).Value.FelhasznaloNev == nev)
                            {
                                lock (Program.LoggedInUsers)
                                {
                                    Program.LoggedInUsers.Remove(Program.LoggedInUsers.ElementAt(index).Key);
                                }
                                talalt = true;
                            }
                            index++;
                        }

                        string hash = ZaroFeladat.Program.CreateSHA256(tmpHash);
                        if (hash == talalat[0].Hash)
                        {
                            string token = Guid.NewGuid().ToString();
                            lock (Program.LoggedInUsers)
                            {
                                Program.LoggedInUsers.Add(token, talalat[0]);
                            }
                            string[] response = new string[3] { token, talalat[0].TeljesNev, talalat[0].Jogosultsag.ToString() };
                            return StatusCode(200, response);
                            
                        }
                        else
                        {
                            string[] response = new string[3] { "Hibás jelszó!", "", "-1" };
                            return Ok(response);
                        }
                    }
                    else
                    {
                        string[] response = new string[3] { "Hibás név/Inaktív felhasználó!", "", "-1" };
                        return Ok(response);
                    }
                }
                catch (Exception ex)
                {
                    string[] response = new string[3] { ex.Message, "", "-1" };
                    return Ok(response);
                }
            }
        }
    }
}
