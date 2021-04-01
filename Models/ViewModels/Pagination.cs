using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Assignment10.Models.ViewModels
{
    public class Pagination
    {
        public int BowlersPerPage { get; set; }
        public int CurrPage { get; set; }
        public int TotalBowlers { get; set; }
        public int Pages => (int) Math.Ceiling((decimal) TotalBowlers / BowlersPerPage);
    }
}
