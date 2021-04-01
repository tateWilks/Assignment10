using Assignment10.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Assignment10.Components
{
    public class TeamViewComponent : ViewComponent
    {
        //must create context for the database and set it to the private variable

        private BowlingLeagueContext _context;
        public TeamViewComponent(BowlingLeagueContext ctx)
        {
            _context = ctx;
        }

        //create a pseudoconstructor that returns a view with just the items from the context that you want --> this wil be accessed in the defaul.cshtml component view
        public IViewComponentResult Invoke()
        {
            return View(
                _context.Teams.Distinct().OrderBy(t => t.TeamName)
            );
        }
    }
}
