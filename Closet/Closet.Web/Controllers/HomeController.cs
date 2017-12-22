using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Closet.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Closet.Services;

namespace Closet.Web.Controllers
{
    public class HomeController : Controller
    {
        private IMemeService memes;

        public HomeController(IMemeService memes)
        {
            this.memes = memes;
        }

        [AllowAnonymous]
        public async Task<IActionResult> Index()
            => this.View(await this.memes.TopThree());

        public IActionResult Error()
            => this.View(new ErrorViewModel
            {
                RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier
            });
        
    }
}
