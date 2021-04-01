using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Assignment10.Models.ViewModels
{
    //this view model will pass in everything we need for the index page to render correctly and have the right URLs
    //this will include paging info, the bowler class, and a string that says what team is currently selected
    public class IndexViewModel
    {
        public List<Bowler> Bowlers { get; set; }
        public Pagination Paging { get; set; }
        public string SelectedTeam { get; set; }
    }
}
