using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CarService.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CarService.API.Controllers
{
    /// <summary>
    /// Felhasználókezelést biztosító vezérlő.
    /// </summary>
    [Route("api/[controller]")]
    public class AccountController : Controller
    {
        /// <summary>
        /// Authentikációs szolgáltatás.
        /// </summary>
        private readonly SignInManager<CarServiceUser> _signInManager;

        /// <summary>
        /// Vezérlő példányosítása.
        /// </summary>
        public AccountController(SignInManager<CarServiceUser> signInManager)
        {
            _signInManager = signInManager;
        }

        /// <summary>
        /// Bejelentkezés.
        /// </summary>
        /// <param name="userName">Felhasználónév.</param>
        /// <param name="userPassword">Jelszó.</param>
        [HttpGet("login/{userName}/{userPassword}")]
        public async Task<IActionResult> Login(String userName, String userPassword)
        {
            try
            {
                // bejelentkeztetjük a felhasználót
                var result = await _signInManager.PasswordSignInAsync(userName, userPassword, false, false);
                if (!result.Succeeded) // ha nem sikerült, akkor nincs bejelentkeztetés
                    return Forbid();

                // ha sikeres volt az ellenőrzés
                return Ok();
            }
            catch
            {
                // Internal Server Error
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        /// <summary>
        /// Kijelentkezés.
        /// </summary>
        [HttpGet("logout")]
        [Authorize] // csak bejelentklezett felhasználóknak
        public async Task<IActionResult> Logout()
        {
            try
            {
                // kijelentkeztetjük az aktuális felhasználót
                await _signInManager.SignOutAsync();
                return Ok();
            }
            catch
            {
                // Internal Server Error
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}