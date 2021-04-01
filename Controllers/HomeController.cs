using Assignment10.Models;
using Assignment10.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore.Sqlite;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Assignment10.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private BowlingLeagueContext _context { get; set; } //create the context to access the sqlite database
        public int NumBowlersPerPage = 5; //create a public integer to dictate the bowlers per page

        public HomeController(ILogger<HomeController> logger, BowlingLeagueContext ctx)
        {
            _logger = logger;
            _context = ctx; //pass in a context variable to set the context
        }

        public IActionResult Index(long? teamId, string teamName, int pageNum=1)
        {
            int pagenum = pageNum;
            ViewBag.SelectedTeam = teamName;

            //when we get a request, we'll make sure to pass in the team id, the team name, and the page number
            return View(new IndexViewModel
            {
                //create a new view model that has the list of bowlers, the pagination, and the selected team name
                Bowlers = _context.Bowlers
                    .FromSqlInterpolated($"SELECT * FROM Bowlers WHERE TeamId = {teamId} OR {teamId} IS NULL") //this lets us select only the bowlers that have that team id or if the team id is null then that's always true so we select everything
                    .Skip((pagenum - 1) * NumBowlersPerPage) //you can skip values depending on the page number
                    .Take(NumBowlersPerPage) //take only the number we want
                    .ToList(), //convert it to a list
                Paging = new Pagination
                {
                    BowlersPerPage = NumBowlersPerPage,
                    CurrPage = pagenum,
                    TotalBowlers = (teamId == null) ? _context.Bowlers.Count() : _context.Bowlers.Where(b => b.TeamId == teamId).Count()
                },
                SelectedTeam = teamName
            }); ;
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
